Imports DevExpress.XtraEditors.Controls

Public Class wReceiveAutoIssue

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

    Private Function AutoTransfer(Optional StateSendApp As String = "0") As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Auto Issue.. Please Wait.... ")
        Try

            Dim dtBarcode As DataTable
            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                dtBarcode = .Copy()
            End With


            Dim grporder As List(Of String) = (dtBarcode.Select("FTOrderNo<>'' AND FTSelect='1'  AND FNQuantity>0 ", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                      .Distinct() _
                                                      .ToList()

            Dim _Qry As String = ""
            Dim _DocNo As String = ""
            Dim _AllDocNo As String = ""
            Dim FTDocType As String = ""
            Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim DataPoNo As String = ""
            Dim DataPoNoTmp As String = ""
            Dim _DocDate As String = ""


            For Each OrderNo As String In grporder
                _DocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENIssue", "", False, _CmpH).ToString()

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                _Qry = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue("
                _Qry &= vbCrLf & "FTInsUser, FDInsDate,FTInsTime, FTIssueNo, FDIssueDate, FTIssueBy, FNHSysIssueSectId"
                _Qry &= vbCrLf & ", FNHSysWHId, FTOrderNo, FTRemark, FNHSysCmpId, FTIssueReqNo,  FTStateSendApp)"
                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry = _Qry & vbCrLf & " ," & Val(FNHSysIssueSectId.Properties.Tag.ToString) & ", " & Me.WHID & ",'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
                _Qry = _Qry & vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                _Qry = _Qry & vbCrLf & ",'' AS FTIssueReqNo,'" & StateSendApp & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If


                For Each R As DataRow In dtBarcode.Select("FTSelect='1'  AND FNQuantity>0 AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'")

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



                Next


                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                If _AllDocNo = "" Then
                    _AllDocNo = _DocNo
                Else
                    _AllDocNo = _AllDocNo & "," & _DocNo
                End If

            Next



            _Spls.Close()

            HI.MG.ShowMsg.mInfo("Transfer Issue Complete... ", 1410027775, Me.Text, _AllDocNo, System.Windows.Forms.MessageBoxIcon.Information)

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
        If Me.FNHSysIssueSectId.Text <> "" And FNHSysIssueSectId.Properties.Tag.ToString <> "" Then
            Dim StateSendApp As String = "0"

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
                _Qry &= vbCrLf & " WHERE FNHSysWHId=" & Me.WHID & ""
                _DtWarehouse = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                For Each R As DataRow In _DtWarehouse.Rows
                    _FTStateFreeZone = (R!FTStateFreeZone.ToString = "1")
                    _FNHSysCmpIdTo = Integer.Parse(Val(R!FNHSysCmpId.ToString))
                    Exit For
                Next

                If _FNHSysCmpIdTo <> HI.ST.SysInfo.CmpID Then
                    StateSendApp = "1"
                End If

                If .Select("FTSelect='1'  AND FNQuantity>0 AND  FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ จ่ายได้กรุณาทำการตรวจสอบ !!!", 1415558411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If


            End With

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Auto Issue ใช่หรือไม่ ?", 1414420001) = True Then

                If Me.AutoTransfer(StateSendApp) Then
                    Me.Close()
                Else
                    HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ  Auto Issuer ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1414420002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysIssueSectId_lbl.Text)
            FNHSysIssueSectId.Focus()
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