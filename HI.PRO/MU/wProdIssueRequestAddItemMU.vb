Public Class wProdIssueRequestAddItemMU

    Private SpareFabricSize As Decimal = 0

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()


        Dim _Qry As String = ""
        _Qry = "SELECT TOP 1 FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='IssueReqSpareFabricSize'"

        SpareFabricSize = CDbl(Format(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "0.5")), "0.50"))


        ' Add any initialization after the InitializeComponent() call
    End Sub

#Region "Property"

    Private _DocumentNo As String = ""
    Public Property DocumentNo As String
        Get
            Return _DocumentNo
        End Get
        Set(value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _Mattype As Integer
    Public Property Mattype As Integer
        Get
            Return _Mattype
        End Get
        Set(value As Integer)
            _Mattype = value
        End Set
    End Property

    Private _Proc As Boolean
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private _OrderNo As String
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

#End Region

#Region "Procudure"
    Private Sub LoadTable()
        Dim _Qry As String = ""
        Dim _dt As DataTable
        _Qry = " SELECT '0' AS FTSelect, A.FTOrderNo, A.FTOrderProdNo, M.FTMarkCode"
        _Qry &= vbCrLf & ", B.FNTableNo"
        _Qry &= vbCrLf & ", B.FNHSysMarkId"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",M.FTMarkCode + '::'+ M.FTMarkNameTH AS FTMarkName "
        Else
            _Qry &= vbCrLf & ",M.FTMarkCode + '::'+ M.FTMarkNameEN AS FTMarkName"
        End If

        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut AS B WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd AS A WITH(NOLOCK) ON B.FTOrderProdNo = A.FTOrderProdNo INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M WITH(NOLOCK) ON B.FNHSysMarkId = M.FNHSysMarkId"
        _Qry &= vbCrLf & "  WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo, A.FTOrderProdNo, M.FTMarkCode, B.FNTableNo"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogctablebound.DataSource = _dt.Copy

    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Proc = False
        Me.Close()
    End Sub

    Private Sub wProdIssueRequestAddItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            HI.TL.HandlerControl.ClearControl(Me)

            ogvtablebound.ClearColumnsFilter()
            ogvtablebound.ActiveFilter.Clear()

            ogvdetail.ClearColumnsFilter()
            ogvdetail.ActiveFilter.Clear()

            Call LoadTable()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        Dim _MsgNotFoundOnhand As Boolean = False

        With CType(Me.ogctablebound.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุโต๊ะที่ต้องการทำการขอเบิก !!!", 1412180094, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim _Spls As New HI.TL.SplashScreen("Creating.. List Material Request  Please wait.", "Creating...")
            Try
                Dim _Qry As String = ""

                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateIssueReq "
                _Qry &= vbCrLf & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                With CType(Me.ogctablebound.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Select("FTSelect='1' ")

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateIssueReq "
                        _Qry &= vbCrLf & "(FTUserLogIn,FTOrderProdNo,FNHSysMarkId,FNTableNo)"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                        _Qry &= vbCrLf & ",'" & Val(R!FNHSysMarkId.ToString) & "'"
                        _Qry &= vbCrLf & ",'" & Val(R!FNTableNo.ToString) & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    Next

                End With

                Dim _dt As DataTable

                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..SP_GETPROD_REQ_ISSUE_FromAX " & HI.ST.SysInfo.CmpID & "," & Integer.Parse(HI.ST.Lang.Language) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'," & Me.Mattype & ""
                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateIssueReq "
                _Qry &= vbCrLf & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                If _dt.Select("FNOnhandQuantity>0").Length <= 0 Then
                    _MsgNotFoundOnhand = True
                End If

                Dim _dt2 As DataTable
                _dt2 = _dt.Select("FNOnhandQuantity>0").CopyToDataTable()
                Me.ogcdetail.DataSource = _dt2.Copy

            Catch ex As Exception
            End Try

            _Spls.Close()

            If (_MsgNotFoundOnhand) Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลวัตถุดิบ คงเหลือ ที่สามารถทำการ Request ได้ !!!", 1412230749, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End With
    End Sub

    Private Sub ockselectalltable_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectalltable.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectalltable.Checked Then
                _State = "1"
            End If

            With ogctablebound
                If Not (.DataSource Is Nothing) And ogvtablebound.RowCount > 0 Then

                    With ogvtablebound
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

    Private Sub ockselectalldetail_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectalldetail.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectalldetail.Checked Then
                _State = "1"
            End If

            Dim _OnhandQty As Double = 0
            Dim _RecomendQty As Double = 0
            Dim _ReqBFQty As Double = 0
            Dim _ReqQty As Double = 0

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then

                    With ogvdetail
                        For I As Integer = 0 To .RowCount - 1

                            If _State = "1" Then

                                _OnhandQty = Double.Parse(.GetRowCellValue(I, "FNOnhandQuantity"))
                                _RecomendQty = Double.Parse(.GetRowCellValue(I, "FNRecomQuantity"))
                                _ReqBFQty = Double.Parse(.GetRowCellValue(I, "FNReqQuantityBF"))

                                If _OnhandQty > 0 Then
                                    _ReqQty = (_RecomendQty - _ReqBFQty)

                                    If _ReqQty <= 0 Then
                                        _ReqQty = 0
                                    End If

                                    'If _OnhandQty > _ReqQty Then
                                    '    _ReqQty = _ReqQty

                                    '    'If _ReqQty <= 0 Then
                                    '    '    _ReqQty = _OnhandQty
                                    '    'End If
                                    'Else
                                    _ReqQty = _OnhandQty
                                    'End If

                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                    .SetRowCellValue(I, "FNReqQuantity", _ReqQty)

                                End If

                            Else
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                .SetRowCellValue(I, "FNReqQuantity", 0)
                            End If

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposRawmatFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposRawmatFTStateSelect.EditValueChanging
        With Me.ogvdetail
            If .FocusedRowHandle < 0 Then Exit Sub

            If Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNOnhandQuantity")) <= 0 Then
                e.Cancel = True
            Else
                Dim _State As String = "0"
                If e.NewValue = "1" Then
                    _State = "1"
                End If

                Dim _OnhandQty As Double = 0
                Dim _RecomendQty As Double = 0
                Dim _ReqBFQty As Double = 0
                Dim _ReqQty As Double = 0

                With ogvdetail

                    If _State = "1" Then

                        _OnhandQty = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNOnhandQuantity"))
                        _RecomendQty = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNRecomQuantity"))
                        _ReqBFQty = Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNReqQuantityBF"))

                        If _OnhandQty > 0 Then
                            _ReqQty = (_RecomendQty - _ReqBFQty)

                            If _ReqQty <= 0 Then
                                _ReqQty = 0
                            End If

                            'If _OnhandQty > _ReqQty Then
                            '    _ReqQty = _ReqQty

                            '    'If _ReqQty <= 0 Then
                            '    '    _ReqQty = _OnhandQty
                            '    'End If
                            'Else
                            _ReqQty = _OnhandQty
                            'End If

                            .SetRowCellValue(.FocusedRowHandle, .Columns.ColumnByFieldName("FTSelect"), _State)
                            .SetRowCellValue(.FocusedRowHandle, "FNReqQuantity", _ReqQty)

                        End If

                    Else
                        .SetRowCellValue(.FocusedRowHandle, .Columns.ColumnByFieldName("FTSelect"), _State)
                        .SetRowCellValue(.FocusedRowHandle, "FNReqQuantity", 0)
                    End If

                End With

                CType(Me.ogcdetail.DataSource, DataTable).AcceptChanges()

            End If

        End With
    End Sub

    Private Sub ReposFNIssueQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNIssueQuantity.EditValueChanging
        Try
            Dim _NewValue As Integer = e.NewValue

            If Double.Parse(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNOnhandQuantity")) < _NewValue Then

                e.Cancel = True

            Else

                If ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSelect") = "0" Then
                    ogvdetail.SetRowCellValue(ogvdetail.FocusedRowHandle, "FTSelect", "1")

                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean

        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail "
            _Qry &= vbCrLf & " WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
            _Qry &= vbCrLf & " WHERE FTIssueReqNo='" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            With CType(Me.ogcdetail.DataSource, DataTable)

                For Each R As DataRow In .Select("FTSelect='1' AND FNReqQuantity>0")

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail "
                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo, FNHSysRawMatId, FNRecomQuantity, FNReqQuantityBF, FNOnhandQuantity, FNReqQuantity)"
                    _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNRecomQuantity.ToString)) & ""
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantityBF.ToString)) & ""
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNOnhandQuantity.ToString)) & ""
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next

            End With


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub CalculateFabric(Spls As HI.TL.SplashScreen)
        Try

            Spls.UpdateInformation("Recomened Fabric Pealse Wait...")

            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _dtitem As DataTable
            Dim _dtBalitem As DataTable
            Dim _FNMarkTotal As Double = 0
            Dim _FNReqQuantity As Double = 0
            Dim _Seq As Integer = 0
            Dim _Layer As Integer = 0
            Dim dtshades As New DataTable
            Dim dtbatch As New DataTable
            Dim TableFabricFrontSize As Decimal = 0
            Dim _FTStatTableScrap As String = ""
            Dim _UsedTotal As Decimal = 0

            dtshades.Columns.Add("FTShades", GetType(String))
            dtbatch.Columns.Add("FTBatchNo", GetType(String))

            _Qry = "  SELECT A.FNHSysRawMatId"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  (A.FTIssueReqNo = N'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "')"
            _Qry &= vbCrLf & "  GROUP BY  A.FNHSysRawMatId"
            _dtitem = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            _Qry = "  SELECT A.FTOrderProdNo, A.FNHSysMarkId, A.FNTableNo, A.FNHSysRawMatId, SUM(A.FNReqQuantity) AS FNReqQuantity, B.FNMarkTotal"
            _Qry &= vbCrLf & " ,MAX(B.FNFabricFrontSize) AS FNFabricFrontSize"
            _Qry &= vbCrLf & " ,MAX(B.FNMarkYRD) AS FNMarkYRD"
            _Qry &= vbCrLf & " ,MAX(B.FNMarkINC) AS FNMarkINC"
            _Qry &= vbCrLf & " ,MAX(B.FNMarkSpare) AS FNMarkSpare"
            _Qry &= vbCrLf & " ,MAX(B.FNMarkTotal) AS FNMarkTotal"
            _Qry &= vbCrLf & " ,MAX(ISNULL(B.FTStatTableScrap,'0')) AS FTStatTableScrap"

            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTIssueReq_Detail AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut AS B WITH(NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo "
            _Qry &= vbCrLf & "  And A.FNHSysMarkId = B.FNHSysMarkId And A.FNTableNo = B.FNTableNo"
            _Qry &= vbCrLf & "  WHERE  (A.FTIssueReqNo = N'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "')"
            _Qry &= vbCrLf & "  GROUP BY A.FTOrderProdNo, A.FNHSysMarkId, A.FNTableNo, A.FNHSysRawMatId, B.FNMarkTotal"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each Ritem As DataRow In _dtitem.Rows

                'FTBarcodeNo()
                ',FNHSysWHId
                ',FTOrderNo
                ',FNQuantity
                ',FNQuantityBal
                ',FTPurchaseNo
                ',FTDocumentNo
                ',0 AS FNTotalLayer
                ',FNQuantityBal AS FNTotalBal

                TableFabricFrontSize = 0  'Val(Ritem!FNFabricFrontSize)
                dtshades.Rows.Clear()
                dtbatch.Rows.Clear()

                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GETPROD_REQ_RAWMAT_FABRIC_FROMAX " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'," & Val(Ritem!FNHSysRawMatId) & ""
                _dtBalitem = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)



                '-------Start  For Each Table Full--------

                For Each R As DataRow In _dt.Select("FNHSysRawMatId=" & Val(Ritem!FNHSysRawMatId) & " AND FTStatTableScrap<>'1'", "FNTableNo")
                    _Seq = 0
                    _FNMarkTotal = Val(R!FNMarkTotal)

                    TableFabricFrontSize = Val(R!FNFabricFrontSize)
                    _FTStatTableScrap = R!FTStatTableScrap.ToString

                    If _FNMarkTotal > 0 Then

                        _FNReqQuantity = Val(R!FNReqQuantity)

                        _dtBalitem.BeginInit()


                        For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FNQuantityBal >0  ")

                            _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                            Rx!FNTotalLayer = _Layer
                            Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                            If dtshades.Select("FTShades='" & HI.UL.ULF.rpQuoted(Rx!FTShades.ToString) & "'").Length <= 0 Then
                                dtshades.Rows.Add(Rx!FTShades.ToString)
                            End If

                            If dtbatch.Select("FTBatchNo='" & HI.UL.ULF.rpQuoted(Rx!FTBatchNo.ToString) & "'").Length <= 0 Then

                                dtbatch.Rows.Add(Rx!FTBatchNo.ToString)

                            End If

                        Next

                        _dtBalitem.EndInit()

                        Dim RawMatShades As String = ""
                        Dim RawMatBatch As String = ""

                        For Each Rsd As DataRow In dtshades.Select("FTShades<>'8888888-XXX'", "FTShades")

                            RawMatShades = Rsd!FTShades.ToString


                            For Each Rsb As DataRow In dtbatch.Select("FTBatchNo<>''", "FTBatchNo")

                                RawMatBatch = Rsb!FTBatchNo.ToString

                                While _FNReqQuantity > 0 And _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "' AND FNQuantityBal >0 AND FNTotalLayer>0   AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal").Length > 0



                                    For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0 AND FNTotalLayer>0   AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & " ", "FNTotalBal,FNQuantityBal")

                                        _dtBalitem.BeginInit()



                                        If Rx!FTBarcodeNo.ToString = "1000000152" Then
                                            Dim DataBar As String = ""
                                            DataBar = "1000000152"
                                        End If

                                        If _FNReqQuantity >= Val(Rx!FNQuantityBal) Then
                                            _Seq = _Seq + 1

                                            _UsedTotal = Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))
                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNTotalLayer.ToString)) & ""
                                            '_Qry &= vbCrLf & ",Convert(numeric(18,4)," & (Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal) & ")"
                                            _Qry &= vbCrLf & "," & _UsedTotal & ""

                                            _FNReqQuantity = (_FNReqQuantity - (Val(Rx!FNQuantityBal) - Val(Rx!FNTotalBal)))
                                            Rx!FNQuantityBal = Val(Rx!FNQuantityBal) - _UsedTotal
                                            'Rx!FNQuantityBal = Val(Rx!FNQuantityBal) - Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            If Rx!FNQuantityBal <= 0 Then
                                                Rx!FNQuantityBal = 0.0
                                            End If

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            'Rx!FNQuantityBal = 0
                                            Rx!FTStateUsed = "1"

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        Else

                                            _Seq = _Seq + 1
                                            _Layer = (_FNReqQuantity * 100.0) \ (_FNMarkTotal * 100.0)

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(_Layer) & ""
                                            _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (_Layer * _FNMarkTotal) & ")"

                                            Rx!FNQuantityBal = (((Val(Rx!FNQuantityBal))) - _FNReqQuantity)
                                            Rx!FTStateUsed = "1"

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            _FNReqQuantity = 0

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        End If

                                        _dtBalitem.EndInit()

                                        '_dtBalitem.BeginInit()

                                        'For Each Rxi2 As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0")

                                        '    Dim _LayerNew As Integer = (Val(Rxi2!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        '    Rxi2!FNTotalLayer = _LayerNew
                                        '    Rxi2!FNTotalBal = CDbl(Format(Val(Rxi2!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))


                                        'Next

                                        '_dtBalitem.EndInit()

                                        'If _FNReqQuantity <= 0 Then
                                        '    Exit For
                                        'End If

                                        Exit For
                                    Next

                                End While

                                _dtBalitem.BeginInit()

                                For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal >0")

                                    _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                    Rx!FNTotalLayer = _Layer
                                    Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                Next

                                _dtBalitem.EndInit()

                                While _FNReqQuantity > 0 And _dtBalitem.Select("FNQuantityBal<" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0 AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & " ", "FNTotalBal,FNQuantityBal").Length > 0

                                    For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal<" & _FNMarkTotal & "  AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0 AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal")

                                        _dtBalitem.BeginInit()

                                        If Rx!FTBarcodeNo.ToString = "1000000152" Then
                                            Dim DataBar As String = ""
                                            DataBar = "1000000152"
                                        End If
                                        If _FNReqQuantity >= Val(Rx!FNQuantityBal) Then

                                            _Seq = _Seq + 1

                                            _UsedTotal = Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNTotalLayer.ToString)) & ""
                                            '_Qry &= vbCrLf & ",Convert(numeric(18,4)," & (Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal) & ")"
                                            _Qry &= vbCrLf & "," & _UsedTotal & ""


                                            _FNReqQuantity = (_FNReqQuantity - (Val(Rx!FNQuantityBal) - Val(Rx!FNTotalBal)))
                                            ' Rx!FNQuantityBal = 0
                                            Rx!FNQuantityBal = Val(Rx!FNQuantityBal) - _UsedTotal 'Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            If Rx!FNQuantityBal <= 0 Then
                                                Rx!FNQuantityBal = 0.0
                                            End If

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            Rx!FTStateUsed = "1"
                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        Else

                                            _Seq = _Seq + 1
                                            _Layer = (_FNReqQuantity * 100.0) \ (_FNMarkTotal * 100.0)

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(_Layer) & ""
                                            _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (_Layer * _FNMarkTotal) & ")"

                                            Rx!FNQuantityBal = (((Val(Rx!FNQuantityBal))) - _FNReqQuantity)
                                            Rx!FTStateUsed = "1"

                                            ' _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            _FNReqQuantity = 0

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        End If

                                        _dtBalitem.EndInit()

                                        '_dtBalitem.BeginInit()

                                        'For Each Rxi2 As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & "  AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0")

                                        '    Dim _LayerNew As Integer = (Val(Rxi2!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        '    Rxi2!FNTotalLayer = _LayerNew
                                        '    Rxi2!FNTotalBal = CDbl(Format(Val(Rxi2!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                        'Next

                                        '_dtBalitem.EndInit()

                                        'If _FNReqQuantity <= 0 Then
                                        '    Exit For
                                        'End If

                                        Exit For
                                    Next

                                    _dtBalitem.BeginInit()

                                    For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal >0")

                                        _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        Rx!FNTotalLayer = _Layer
                                        Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                    Next

                                    _dtBalitem.EndInit()

                                End While

                                If _FNReqQuantity <= 0 Then
                                    Exit For
                                End If

                            Next

                            If _FNReqQuantity <= 0 Then
                                Exit For
                            End If

                        Next

                    End If

                Next

                '-------End  For Each Table Full--------


                '-------Start  For Each Table Scarp--------

                For Each R As DataRow In _dt.Select("FNHSysRawMatId=" & Val(Ritem!FNHSysRawMatId) & " AND FTStatTableScrap='1'", "FNTableNo")
                    _Seq = 0
                    _FNMarkTotal = Val(R!FNMarkTotal)

                    TableFabricFrontSize = Val(R!FNFabricFrontSize)
                    _FTStatTableScrap = R!FTStatTableScrap.ToString

                    If _FNMarkTotal > 0 Then

                        _FNReqQuantity = Val(R!FNReqQuantity)

                        _dtBalitem.BeginInit()

                        For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FNQuantityBal >0  AND FTStateUsed='1'")

                            _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                            Rx!FNTotalLayer = _Layer
                            Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                            If dtshades.Select("FTShades='" & HI.UL.ULF.rpQuoted(Rx!FTShades.ToString) & "'").Length <= 0 Then
                                dtshades.Rows.Add(Rx!FTShades.ToString)
                            End If

                            If dtbatch.Select("FTBatchNo='" & HI.UL.ULF.rpQuoted(Rx!FTBatchNo.ToString) & "'").Length <= 0 Then

                                dtbatch.Rows.Add(Rx!FTBatchNo.ToString)

                            End If

                        Next

                        _dtBalitem.EndInit()

                        Dim RawMatShades As String = ""
                        Dim RawMatBatch As String = ""

                        For Each Rsd As DataRow In dtshades.Select("FTShades<>'8888888-XXX'", "FTShades")

                            RawMatShades = Rsd!FTShades.ToString


                            While _FNReqQuantity > 0 And _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FNQuantityBal >0  AND FTStateUsed='1' AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal").Length > 0

                                For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "'  AND FNQuantityBal >0  AND FTStateUsed='1' AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal")

                                    _dtBalitem.BeginInit()

                                    If _FNReqQuantity >= Val(Rx!FNQuantityBal) Then
                                        _Seq = _Seq + 1

                                        _UsedTotal = Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                        _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                        _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                        _Qry &= vbCrLf & "," & _Seq & ""
                                        _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNTotalLayer.ToString)) & ""
                                        _Qry &= vbCrLf & "," & _UsedTotal & ""
                                        ' _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal) & ")"

                                        _FNReqQuantity = (_FNReqQuantity - (Val(Rx!FNQuantityBal) - Val(Rx!FNTotalBal)))
                                        Rx!FNQuantityBal = Val(Rx!FNQuantityBal) - _UsedTotal 'Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                        If Rx!FNQuantityBal <= 0 Then
                                            Rx!FNQuantityBal = 0.0
                                        End If

                                        '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        'Rx!FNTotalLayer = _Layer
                                        'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                        Rx!FTStateUsed = "2"
                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Else

                                        _Seq = _Seq + 1
                                        _Layer = (_FNReqQuantity * 100.0) \ (_FNMarkTotal * 100.0)

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                        _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                        _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                        _Qry &= vbCrLf & "," & _Seq & ""
                                        _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                        _Qry &= vbCrLf & "," & Integer.Parse(_Layer) & ""
                                        _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (_Layer * _FNMarkTotal) & ")"

                                        Rx!FNQuantityBal = (((Val(Rx!FNQuantityBal))) - _FNReqQuantity)
                                        _FNReqQuantity = 0

                                        '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        'Rx!FNTotalLayer = _Layer
                                        'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                        Rx!FTStateUsed = "2"
                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    End If

                                    _dtBalitem.EndInit()

                                    '_dtBalitem.BeginInit()

                                    'For Each Rxi2 As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0 AND FTStateUsed='1'")

                                    '    Dim _LayerNew As Integer = (Val(Rxi2!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                    '    Rxi2!FNTotalLayer = _LayerNew
                                    '    Rxi2!FNTotalBal = CDbl(Format(Val(Rxi2!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                    'Next

                                    '_dtBalitem.EndInit()

                                    'If _FNReqQuantity <= 0 Then
                                    '    Exit For
                                    'End If

                                    Exit For
                                Next

                            End While

                            If _FNReqQuantity <= 0 Then
                                Exit For
                            End If

                        Next

                        _dtBalitem.BeginInit()

                        For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal >0")

                            _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                            Rx!FNTotalLayer = _Layer
                            Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                        Next

                        _dtBalitem.EndInit()

                        R!FNReqQuantity = _FNReqQuantity

                    End If

                Next


                For Each R As DataRow In _dt.Select("FNHSysRawMatId=" & Val(Ritem!FNHSysRawMatId) & " AND FTStatTableScrap='1' AND FNReqQuantity >0", "FNTableNo")
                    _Seq = 0
                    _FNMarkTotal = Val(R!FNMarkTotal)

                    TableFabricFrontSize = Val(R!FNFabricFrontSize)
                    _FTStatTableScrap = R!FTStatTableScrap.ToString

                    If _FNMarkTotal > 0 Then

                        _FNReqQuantity = Val(R!FNReqQuantity)

                        _dtBalitem.BeginInit()

                        For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FNQuantityBal >0")

                            _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                            Rx!FNTotalLayer = _Layer
                            Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                            If dtshades.Select("FTShades='" & HI.UL.ULF.rpQuoted(Rx!FTShades.ToString) & "'").Length <= 0 Then
                                dtshades.Rows.Add(Rx!FTShades.ToString)
                            End If

                            If dtbatch.Select("FTBatchNo='" & HI.UL.ULF.rpQuoted(Rx!FTBatchNo.ToString) & "'").Length <= 0 Then

                                dtbatch.Rows.Add(Rx!FTBatchNo.ToString)

                            End If

                        Next

                        _dtBalitem.EndInit()

                        Dim RawMatShades As String = ""
                        Dim RawMatBatch As String = ""

                        For Each Rsd As DataRow In dtshades.Select("FTShades<>'8888888-XXX'", "FTShades")

                            RawMatShades = Rsd!FTShades.ToString

                            For Each Rsb As DataRow In dtbatch.Select("FTBatchNo<>''", "FTBatchNo")

                                RawMatBatch = Rsb!FTBatchNo.ToString


                                While _FNReqQuantity > 0 And _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "' AND FNQuantityBal >0 AND FTStateUsed<>'2' AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal").Length > 0

                                    For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0    AND FTStateUsed<>'2' AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal")

                                        _dtBalitem.BeginInit()

                                        If _FNReqQuantity >= Val(Rx!FNQuantityBal) Then
                                            _Seq = _Seq + 1

                                            _UsedTotal = Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNTotalLayer.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _UsedTotal & ""
                                            '_Qry &= vbCrLf & ",Convert(numeric(18,4)," & (Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal) & ")"

                                            _FNReqQuantity = (_FNReqQuantity - (Val(Rx!FNQuantityBal) - Val(Rx!FNTotalBal)))
                                            ' Rx!FNQuantityBal = 0

                                            Rx!FNQuantityBal = Val(Rx!FNQuantityBal) - _UsedTotal 'Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            If Rx!FNQuantityBal <= 0 Then
                                                Rx!FNQuantityBal = 0.0
                                            End If

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        Else

                                            _Seq = _Seq + 1
                                            _Layer = (_FNReqQuantity * 100.0) \ (_FNMarkTotal * 100.0)

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(_Layer) & ""
                                            _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (_Layer * _FNMarkTotal) & ")"

                                            Rx!FNQuantityBal = (((Val(Rx!FNQuantityBal))) - _FNReqQuantity)
                                            _FNReqQuantity = 0

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        End If

                                        _dtBalitem.EndInit()

                                        '_dtBalitem.BeginInit()

                                        'For Each Rxi2 As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0 AND FTStateUsed <>'2'")

                                        '    Dim _LayerNew As Integer = (Val(Rxi2!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        '    Rxi2!FNTotalLayer = _LayerNew
                                        '    Rxi2!FNTotalBal = CDbl(Format(Val(Rxi2!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                        'Next

                                        '_dtBalitem.EndInit()

                                        'If _FNReqQuantity <= 0 Then
                                        '    Exit For
                                        'End If

                                        Exit For
                                    Next

                                End While

                                _dtBalitem.BeginInit()

                                For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal >0")

                                    _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                    Rx!FNTotalLayer = _Layer
                                    Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                Next

                                _dtBalitem.EndInit()


                                While _FNReqQuantity > 0 And _dtBalitem.Select("FNQuantityBal<" & _FNMarkTotal & " AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0  AND FTStateUsed<>'2' AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal").Length > 0

                                    For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal<" & _FNMarkTotal & "  AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0   AND FTStateUsed<>'2' AND FNTotalLayer>0  AND FTFabricFrontSize>=" & TableFabricFrontSize & " AND FTFabricFrontSize <= " & (TableFabricFrontSize + SpareFabricSize) & "  ", "FNTotalBal,FNQuantityBal")

                                        _dtBalitem.BeginInit()

                                        If _FNReqQuantity >= Val(Rx!FNQuantityBal) Then

                                            _Seq = _Seq + 1

                                            _UsedTotal = Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNTotalLayer.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _UsedTotal & ""
                                            ' _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal) & ")"

                                            _FNReqQuantity = (_FNReqQuantity - (Val(Rx!FNQuantityBal) - Val(Rx!FNTotalBal)))
                                            ' Rx!FNQuantityBal = 0
                                            Rx!FNQuantityBal = Val(Rx!FNQuantityBal) - _UsedTotal 'Decimal.Parse(Format((Integer.Parse(Val(Rx!FNTotalLayer.ToString)) * _FNMarkTotal), "0.0000"))

                                            If Rx!FNQuantityBal <= 0 Then
                                                Rx!FNQuantityBal = 0.0
                                            End If

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        Else

                                            _Seq = _Seq + 1
                                            _Layer = (_FNReqQuantity * 100.0) \ (_FNMarkTotal * 100.0)

                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTIssueReq_Detail_Barcode "
                                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTIssueReqNo, FTOrderProdNo, FNHSysMarkId, FNTableNo"
                                            _Qry &= vbCrLf & " , FNHSysRawMatId, FTBarcodeNo, FNReqQuantity, FNSeq, FNQuantity, FNLayer, FNTotalUsed)"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.DocumentNo) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderProdNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysMarkId.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNTableNo.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & ""
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReqQuantity.ToString)) & ""
                                            _Qry &= vbCrLf & "," & _Seq & ""
                                            _Qry &= vbCrLf & "," & Double.Parse(Val(Rx!FNQuantityBal.ToString)) & ""
                                            _Qry &= vbCrLf & "," & Integer.Parse(_Layer) & ""
                                            _Qry &= vbCrLf & ",Convert(numeric(18,4)," & (_Layer * _FNMarkTotal) & ")"

                                            Rx!FNQuantityBal = (((Val(Rx!FNQuantityBal))) - _FNReqQuantity)

                                            '_Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                            'Rx!FNTotalLayer = _Layer
                                            'Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                            _FNReqQuantity = 0

                                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        End If

                                        _dtBalitem.EndInit()

                                        '_dtBalitem.BeginInit()

                                        'For Each Rxi2 As DataRow In _dtBalitem.Select("FNQuantityBal>=" & _FNMarkTotal & "  AND FTShades='" & HI.UL.ULF.rpQuoted(RawMatShades) & "' AND FTBatchNo='" & HI.UL.ULF.rpQuoted(RawMatBatch) & "'  AND FNQuantityBal >0  AND FTStateUsed<>'2'")

                                        '    Dim _LayerNew As Integer = (Val(Rxi2!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        '    Rxi2!FNTotalLayer = _LayerNew
                                        '    Rxi2!FNTotalBal = CDbl(Format(Val(Rxi2!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                        'Next

                                        '_dtBalitem.EndInit()

                                        'If _FNReqQuantity <= 0 Then
                                        '    Exit For
                                        'End If

                                        Exit For
                                    Next

                                    _dtBalitem.BeginInit()

                                    For Each Rx As DataRow In _dtBalitem.Select("FNQuantityBal >0")

                                        _Layer = (Val(Rx!FNQuantityBal) * 100.0) \ (Double.Parse(_FNMarkTotal) * 100.0)

                                        Rx!FNTotalLayer = _Layer
                                        Rx!FNTotalBal = CDbl(Format(Val(Rx!FNQuantityBal) Mod Double.Parse(_FNMarkTotal), "0.0000"))

                                    Next

                                    _dtBalitem.EndInit()

                                End While

                                If _FNReqQuantity <= 0 Then
                                    Exit For
                                End If

                            Next

                            If _FNReqQuantity <= 0 Then
                                Exit For
                            End If

                        Next

                    End If

                Next

                '-------End  For Each Table Scarp--------

            Next

            Try
                dtshades.Dispose()
                dtbatch.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1' AND FNReqQuantity>0").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายการที่ต้องการทำการของเบิก !!!", 1412190218, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim _Spls As New HI.TL.SplashScreen("Saving.. Material Request  Please wait.", "Saving...")
                Try
                    If Me.SaveData Then

                        If Me.Mattype = 0 Then
                            Call CalculateFabric(_Spls)

                            Dim _Qry As String = ""
                            _Qry = " Exec dbo.SP_GET_MUGroupProdPOMat '" & HI.ST.UserInfo.UserName & "' , '" & Me.DocumentNo.ToString & "'"
                            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                        End If

                        _Spls.Close()
                        Me.Proc = True
                        Me.Close()
                    Else
                        _Spls.Close()
                    End If
                Catch ex As Exception
                    _Spls.Close()
                End Try

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemCalcEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEdit1.EditValueChanging

    End Sub

End Class