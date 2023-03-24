Public Class wGenerateBarcodeAsset

    Enum BarCodeType As Integer
        Adjust = 0
        Receive = 1
        Scrap = 2
        Conversion = 3
    End Enum
#Region "Property"
    Private _ProcGen As Boolean
    Public Property ProcGen As Boolean
        Get
            Return _ProcGen
        End Get
        Set(value As Boolean)
            _ProcGen = value
        End Set
    End Property

    Private _BarType As BarCodeType = BarCodeType.Receive
    Public Property BarType As BarCodeType
        Get
            Return _BarType
        End Get
        Set(value As BarCodeType)
            _BarType = value
        End Set
    End Property

    Private _DocumentNo As String = ""
    Public Property DocumentNo As String
        Get
            Return _DocumentNo
        End Get
        Set(value As String)
            _DocumentNo = value
        End Set
    End Property

    Private _MainObject As Object = Nothing
    Public Property MainObject As Object
        Get
            Return _MainObject
        End Get
        Set(value As Object)
            _MainObject = value
        End Set
    End Property

#End Region


    Private Function LoadDataTable() As DataTable

        Dim _Str As String = ""
        Dim _FoundOrderType As Boolean = False
        Select Case BarType
            Case BarCodeType.Receive
                _Str &= vbCrLf & "SELECT C.FTAssetCode,R.FTReceiveNo AS FTDocumentNo ,R.FTPurchaseNo,RD.FNHSysFixedAssetId,R.FNHSysWHId,RD.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & ",RD.FNPrice AS FNPricePerStock ,RD.FNQuantity AS FNQuantityStock 	 "
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive  AS  R WITH(NOLOCK) ON RD.FTReceiveNo=R.FTReceiveNo INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS C WITh(NOLOCK) ON RD.FNHSysFixedAssetId=C.FNHSysFixedAssetId"
            Case BarCodeType.Adjust

                _Str &= vbCrLf & "  SELECT C.FTAssetCode,H.FTAdjustStockNo AS FTDocumentNo ,A.FTPurchaseNo,A.FNHSysFixedAssetId"
                _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
                _Str &= vbCrLf & " 	  , A.FNHSysUnitId AS FNHSysUnitIdStock"
                _Str &= vbCrLf & " 	, A.FNPrice AS FNPricePerStock"
                _Str &= vbCrLf & " 	,A.FNQuantity AS FNQuantityStock"
                _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail AS A WITH (NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust  AS  H WITH(NOLOCK) ON A.FTAdjustStockNo = H.FTAdjustStockNo INNER JOIN"
                _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS C WITh(NOLOCK) ON A.FNHSysFixedAssetId=C.FNHSysFixedAssetId"
                _Str &= vbCrLf & "  WHERE A.FTAdjustStockNo ='" & HI.UL.ULF.rpQuoted(DocumentNo) & "'"

            Case BarCodeType.Scrap
            Case BarCodeType.Conversion
        End Select

        Return HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)

    End Function

    Public Sub LoadGenbarcode()
        Me.ogcdetail.DataSource = LoadDataTable()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.ProcGen = False
        Me.Close()
    End Sub

    Private Sub ocmgenbarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmgenbarcode.Click

        If VerifyBarcode() Then
            Me.ProcGen = Me.GenBarcode
            If Me.ProcGen Then

                Dim _dt As DataTable = LoadDataTable()

                If _dt.Rows.Count > 0 Then
                    Me.Close()
                Else
                    Me.ogcdetail.DataSource = _dt.Copy
                    Me.ogcdetail.RefreshDataSource()

                    Try
                        Call CallByName(Me.MainObject, "LoadBarcode", CallType.Method, {DocumentNo})
                    Catch ex As Exception
                    End Try

                End If

            End If
        End If
    End Sub

    Private Function GenBarcode() As Boolean
        Dim _Str As String = ""
        Try
            For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Rows
                Try
                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    _Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ",FTBarcodeNo, FNHSysWHId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                    _Str &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",'" & R!FTAssetCode.ToString & "'," & Val(R!FNHSysWHId.ToString) & "," & Val(R!FNHSysFixedAssetId.ToString) & "," & Val(R!FNHSysUnitIdStock.ToString) & ""
                    _Str &= vbCrLf & "," & Val(R!FNPricePerStock.ToString) & "," & Val(R!FNQuantityStock.ToString) & ",'" & R!FTDocumentNo.ToString & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return False
                    End If

                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                    _Str &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",'" & R!FTAssetCode.ToString & "','" & R!FTDocumentNo.ToString & "'," & Val(R!FNHSysWHId.ToString) & "," & Val(R!FNQuantityStock.ToString) & ",'" & R!FTDocumentNo.ToString & "'," & ST.SysInfo.CmpID & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return False
                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Catch ex As Exception
                    MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End Try
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function VerifyBarcode() As Boolean
        Dim Qry As String = ""
        Try
            For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Rows
                Qry = "select B.FTBarcodeNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH(NOLOCK)"
                Qry &= vbCrLf & "where B.FTBarcodeNo='" & R!FTAssetCode.ToString & "'"
                If (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) <> "") Then
                    MG.ShowMsg.mInfo("มีบาร์โค๊ดนี้อยู่แล้ว!!!", 201610201137, Me.Text)
                    Return False
                    Exit For
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class