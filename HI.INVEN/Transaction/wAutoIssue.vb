Public Class wAutoIssue

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _DocRefNo As String = ""
    Public Property DocRefNo() As String
        Get
            Return _DocRefNo
        End Get
        Set(value As String)
            _DocRefNo = value
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

    Private Function AutoIssue() As Boolean

        Dim _AllDocNo As String = ""

        Dim _Spls As New HI.TL.SplashScreen("Auto Issue.. Please Wait.... ")
        Try
            Dim dt As DataTable
            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With


            Dim grp As List(Of String) = (dt.Select("FTSelect='1'", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                      .Distinct() _
                                                      .ToList()

            Dim _Qry As String = ""
            Dim _DocNo As String = ""
            Dim _FTOrderNo As String = ""
            Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            _AllDocNo = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each Ind As String In grp

                _FTOrderNo = Ind
                _DocNo = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENIssue", "", False, _CmpH & "A").ToString()

                If _AllDocNo = "" Then
                    _AllDocNo = _DocNo
                Else
                    _AllDocNo &= "," & _DocNo
                End If

                _Qry = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].[TINVENIssue]"
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime "
                _Qry &= vbCrLf & ",FTIssueNo, FDIssueDate, FTIssueBy, FNHSysIssueSectId, FNHSysWHId, FTOrderNo, FTRemark, FNHSysCmpId, FTIssueReqNo"
                _Qry &= vbCrLf & "  )"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " ," & Me.WHID & " "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.FNHSysWHIdTo.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "' "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " "
                _Qry &= vbCrLf & " ,N'' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False

                End If



                For Each R As DataRow In dt.Select("FTSelect='1' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'")

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId)  "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                    _Qry &= vbCrLf & "," & Val(Me.WHID) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNQuantity.ToString)) & " "
                    _Qry &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()

                        Return False
                    End If

                Next

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()

            HI.MG.ShowMsg.mInfo("Issue Auto Complete... ", 1509287415, Me.Text, _AllDocNo, System.Windows.Forms.MessageBoxIcon.Information)

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
            If Me.FNHSysIssueSectId.Text <> "" And FNHSysIssueSectId.Properties.Tag.ToString <> "" Then
                With CType(ogcbarcode.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTSelect='1'").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Barcode ที่ต้องการทำการ Issue !!!", 1509287411, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
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

                    If .Select("FTSelect='1' AND FNOrderType=0 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการจ่ายได้กรุณาทำการตรวจสอบ !!!", 1509287412, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1' AND FNOrderType=4  AND FTOrderNoRef<>'' AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการ จ่ายได้กรุณาทำการตรวจสอบ !!!", 1509287412, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1' AND FNOrderType=2 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการจ่ายด้กรุณาทำการตรวจสอบ !!!", 1509287412, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1' AND FNOrderType=3 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการจ่ายได้กรุณาทำการตรวจสอบ !!!", 1509287412, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If .Select("FTSelect='1' AND FNOrderType=13 AND FNHSysCmpIdTo<>" & _FNHSysCmpIdTo & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายการ FO No บางรายการไม่ได้ผลิตที่ปลายทาง ไม่สามารถทำการจ่ายได้กรุณาทำการตรวจสอบ !!!", 1509287412, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End With

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Auto Issue ใช่หรือไม่ ?", 1509287413) = True Then
                    If Me.AutoIssue Then
                        Me.Close()
                    Else
                        HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Auto Transfer ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1509287414, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysIssueSectId_lbl.Text)
                FNHSysIssueSectId.Focus()
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