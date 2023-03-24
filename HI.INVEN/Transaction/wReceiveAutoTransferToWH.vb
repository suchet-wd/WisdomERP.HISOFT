Imports DevExpress.XtraEditors.Controls

Public Class wReceiveAutoTransferToWH

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


    Private _PONo As String = ""
    Public Property PONo() As String
        Get
            Return _PONo
        End Get
        Set(value As String)
            _PONo = value
        End Set
    End Property

    Private _POFactoryNo As String = ""
    Public Property POFactoryNo() As String
        Get
            Return _POFactoryNo
        End Get
        Set(value As String)
            _POFactoryNo = value
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

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Function AutoTransfer() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Auto Transfer To WH.. Please Wait.... ")
        Try

            Dim _Qry As String = ""
            Dim _DocNo As String = ""
            Dim FTDocType As String = ""
            Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim DataPoNo As String = ""
            Dim DataPoNoTmp As String = ""
            Dim _DocDate As String = ""
            'If Me.POFactoryNo <> "" Then

            '    With CType(Me.ogcbarcode.DataSource, DataTable)
            '        .AcceptChanges()

            '        For Each R As DataRow In .Select("FTSelect='1'  AND FNQuantity>0")

            '            _Qry = "  Select Top 1 H.FTFacPurchaseNo  "
            '            _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].TPURTFacPurchase As H WITH(NOLOCK) INNER Join "
            '            _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].TPURTFacPurchase_OrderNo As D WITH(NOLOCK) On H.FTFacPurchaseNo = D.FTFacPurchaseNo "
            '            _Qry &= vbCrLf & " WHERE H.FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(PONo) & "' AND D.FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"

            '            DataPoNoTmp = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "")

            '            If DataPoNoTmp <> "" Then
            '                DataPoNo = DataPoNoTmp
            '                Exit For
            '            End If

            '        Next

            '    End With

            'End If

            'If DataPoNo <> "" Then
            '    _DocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENTransferWH", "1", False, _CmpH & "A").ToString()
            '    FTDocType = "1"
            'Else
            _DocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENTransferWH", "", False, _CmpH & "A").ToString()
                FTDocType = ""
                ' End If

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].[TINVENTransferWH]"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTTransferWHNo, FDTransferWHDate, FTTransferWHBy, FNHSysWHId, FNHSysWHIdTo, FTRemark, FNHSysCmpId,FTFacPurchaseNo,FTDocType "
            _Qry &= vbCrLf & "  )"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & " ," & Me.WHID & " "
            _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.FNHSysWHIdTo.Properties.Tag.ToString)) & " "
            _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "' "
            _Qry &= vbCrLf & " ," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " "
            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(DataPoNo) & "' "
            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDocType) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _Spls.Close()
                Return False
            End If

            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSelect='1'  AND FNQuantity>0")

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId)  "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                    _Qry &= vbCrLf & "," & Val(Me.WHID) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNQuantity.ToString)) & " "
                    _Qry &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.ReceiveNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        _Spls.Close()
                        Return False

                    End If

                    '_Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId)  "
                    '_Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    '_Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    '_Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "' "
                    '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                    '_Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysWHIdTo.Properties.Tag.ToString)) & " "
                    '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    '_Qry &= vbCrLf & "," & Double.Parse(Val(R!FNQuantity.ToString)) & " "
                    '_Qry &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.ReceiveNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "

                    'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '    HI.Conn.SQLConn.Tran.Rollback()
                    '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '    _Spls.Close()
                    '    Return False
                    'End If

                Next

            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Dim cmd As String = ""
            Dim SysCmpId As Integer = 0
            Dim SysWHCmpId As Integer = 0

            cmd = "select top 1 C.FNHSysCmpIdTo "
            cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X with(nolock) "
            cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON X.FNHSysCmpId=C.FNHSysCmpId  "
            cmd &= vbCrLf & "  where X.FTWHCode='" & HI.UL.ULF.rpQuoted(FNHSysWHIdTo.Text) & "'"
            SysCmpId = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            cmd = "select top 1 C.FNHSysCmpIdTo "
            cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X with(nolock) "
            cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON X.FNHSysCmpId=C.FNHSysCmpId  "
            cmd &= vbCrLf & "  where X.FTWHCode='" & HI.UL.ULF.rpQuoted(Me.WHID) & "'"
            SysWHCmpId = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))


            If SysCmpId > 0 And SysWHCmpId > 0 Then
                If SysCmpId = SysWHCmpId Then


                    cmd = "UPDATE A Set "
                    cmd &= vbCrLf & "  FTFacPurchaseNo=''"
                    cmd &= vbCrLf & ", FTDocumentState='1'"
                    cmd &= vbCrLf & " ,FTInvoiceNo=''"
                    cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A "
                    cmd &= vbCrLf & "WHERE  A.FTTransferWHNo='" & HI.UL.ULF.rpQuoted(_DocNo) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = True Then

                    End If

                    GoTo L224
                Else

                    Dim dtcheck As New DataTable
                    cmd = " SELECT TOP 1 FTFacPurchaseNo,FTStateSuperVisorApp  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(_DocNo) & "'"
                    dtcheck = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PUR)

                    Dim pokey As String = ""
                    Dim poapp As String = ""
                    '  pokey = HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PUR, "")

                    For Each R As DataRow In dtcheck.Rows
                        pokey = R!FTFacPurchaseNo.ToString
                        poapp = R!FTStateSuperVisorApp.ToString
                    Next

                    dtcheck.Dispose()

                    If pokey <> "" And poapp = "1" Then
                        GoTo L224
                    End If


                    cmd = "     Select  MAX(BB.FTPurchaseNo) As FTPurchaseNo "
                    cmd &= vbCrLf & "   ,MIN(ISNULL(RCD.FDReceiveDate,'')) AS FDReceiveDate "
                    cmd &= vbCrLf & "      From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB WITH(NOLOCK) INNER Join"
                    cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As B With(NOLOCK)  On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join"
                    cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH As A WITH(NOLOCK)  On B.FTDocumentNo = A.FTTransferWHNo"
                    cmd &= vbCrLf & "    OUTER APPLY(Select TOP 1 FDReceiveDate FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As X With(NOLOCK) WHERE X.FTReceiveNo =B.FTDocumentRefNo ) As RCD"
                    cmd &= vbCrLf & "    Where(A.FTTransferWHNo ='" & HI.UL.ULF.rpQuoted(_DocNo) & "') "

                    Dim dtpo As DataTable
                    dtpo = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_INVEN)

                    If dtpo.Rows.Count > 0 Then

                        Dim PurNo As String()
                        For Each Rp As DataRow In dtpo.Rows
                            PurNo = HI.INVEN.StockValidate.AutoPurchase(_DocNo, _DocDate, SysCmpId, SysWHCmpId, Rp!FDReceiveDate.ToString, Rp!FTPurchaseNo.ToString, pokey)
                            Exit For
                        Next



                    End If

                End If

            End If
L224:
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
                If .Select("FTSelect='1' AND FNQuantity>0").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Barcode ที่ต้องการทำการ Transfer !!!", 1410020011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If


                Dim _DtOrder As DataTable
                Dim _DtWarehouse As DataTable
                Dim _FNOrderType As Integer = 0
                Dim _FNHSysCmpIdTo As Integer = 0
                Dim _FTStateFreeZone As Boolean = False
                Dim _Qry As String = ""

                _Qry = "SELECT TOP 1 FNHSysCmpId,FTStateFreeZone "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTWHCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdTo.Text) & "'"
                _DtWarehouse = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                For Each R As DataRow In _DtWarehouse.Rows
                    _FTStateFreeZone = (R!FTStateFreeZone.ToString = "1")
                    _FNHSysCmpIdTo = Integer.Parse(Val(R!FNHSysCmpId.ToString))
                    Exit For
                Next

                If _FTStateFreeZone = False Then

                    If .Select("FTSelect='1'  AND FNQuantity>0 AND FNOrderType=0 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1418928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1'  AND FNQuantity>0 AND FNOrderType=4  AND FTOrderNoRef<>'' AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1418928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1'  AND FNQuantity>0 AND FNOrderType=2 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1418928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1'  AND FNQuantity>0 AND FNOrderType=3 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1418928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1'  AND FNQuantity>0 AND FNOrderType=13 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1418928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1' AND FNOrderType=22 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ โอนได้กรุณาทำการตรวจสอบ !!!", 1418928411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End If

            End With

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Auto Transfer To WH ใช่หรือไม่ ?", 1410020001) = True Then

                If Me.AutoTransfer Then
                    Me.Close()
                Else
                    HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Auto Transfer ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1410020002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
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

    Private Sub ReposFNQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNQuantity.EditValueChanging
        Try
            If Val(e.NewValue.ToString) > Val("" & ogvbarcode.GetFocusedRowCellValue("FNQuantityOrg").ToString) Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class