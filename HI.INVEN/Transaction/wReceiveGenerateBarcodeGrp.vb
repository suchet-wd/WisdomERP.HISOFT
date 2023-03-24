Imports System.Windows.Forms

Public Class wReceiveGenerateBarcodeGrp

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


    Private _MainObject As Object = Nothing
    Public Property MainObject As Object
        Get
            Return _MainObject
        End Get
        Set(value As Object)
            _MainObject = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Function AutoTransfer() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Creating Barcode Group.. Please Wait.... ")

        Try
            Dim dtbargrp As DataTable

            With CType(ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                dtbargrp = .Copy()

            End With
            Dim _Qry As String = ""

            Dim _BarcodeGrp As String = ""
            _BarcodeGrp = HI.Conn.SQLConn.GetField(" EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GEN_BARCODE_GROUP_NO '" & HI.ST.SysInfo.CmpRunID & "' ", Conn.DB.DataBaseName.DB_INVEN, "")


            If _BarcodeGrp <> "" Then
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                Try
                    For Each R As DataRow In dtbargrp.Select("FTSelect='1'")

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET FTBarcodeGrpNo='" & HI.UL.ULF.rpQuoted(_BarcodeGrp) & "' WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString()) & "'"

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
                    _Spls.Close()

                    HI.MG.ShowMsg.mInfo("สร้าง Barcode Group  Complete... ", 1418827775, Me.Text, _BarcodeGrp, System.Windows.Forms.MessageBoxIcon.Information)

                    With New wReceiveGenerateBarcodeGrpPrint
                        .FTBarcodeGrpNo.Text = _BarcodeGrp
                        .ShowDialog()
                    End With

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End Try

            End If

            Try
                Call CallByName(Me.MainObject, "LoadBarcode", CallType.Method, {ReceiveNo})
            Catch ex As Exception
            End Try
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

        With CType(ogcbarcode.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Barcode ที่ต้องการทำการ สร้าง Barcode Group !!!", 1410020011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

        End With

        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ สร้าง Barcode Group ใช่หรือไม่ ?", 1418827771) = True Then

            If Me.AutoTransfer Then

                Dim _Qry As String = ""
                Dim dtauto As DataTable
                Dim pofacno As String = ""

                _Qry = " Select '0' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
                _Qry &= vbCrLf & " ,M.FTBarcodeNo "
                _Qry &= vbCrLf & " ,WS.FTWHCode"
                _Qry &= vbCrLf & ",M.FTOrderNo"
                _Qry &= vbCrLf & " ,M.FNQuantity"

                _Qry &= vbCrLf & "  , IM.FTRawMatCode"

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
                Else
                    _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
                End If

                _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
                _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
                _Qry &= vbCrLf & ",M.FTFabricFrontSize"
                _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FNOrderType,M.FTOrderNoRef,M.FNHSysCmpIdToRef,M.FNHSysCmpIdTo AS FNHSysCmpIdToOrg "
                _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN M.FNHSysCmpIdToRef ELSE M.FNHSysCmpIdTo   END AS  FNHSysCmpIdTo  "
                _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN CmpToRef.FTCmpCode ELSE CmpTo.FTCmpCode   END AS  FTCmpCode  "
                _Qry &= vbCrLf & "  FROM"
                _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
                _Qry &= vbCrLf & "   ,FNHSysWHId"
                _Qry &= vbCrLf & "   ,FTOrderNo"
                _Qry &= vbCrLf & "  ,FNQuantity"
                _Qry &= vbCrLf & "  ,ISNULL(("
                _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo"
                _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"
                _Qry &= vbCrLf & "   ,FTPurchaseNo"
                _Qry &= vbCrLf & "   ,FTDocumentNo"
                _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNHSysCmpIdTo,FNOrderType,FTOrderNoRef,FNHSysCmpIdToRef"
                _Qry &= vbCrLf & "   FROM"
                _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,SUM(B.FNQuantity) AS FNQuantity"
                _Qry &= vbCrLf & "  ,B.FTPurchaseNo,B.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId AS FNHSysCmpIdTo,C.FNOrderType,ISNULL(B.FTOrderNoRef,'') AS FTOrderNoRef,ISNULL(CRef.FNHSysCmpId,0) AS FNHSysCmpIdToRef "
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
                _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON  B.FTOrderNo = C.FTOrderNo "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS CREF WITH(NOLOCK) ON  B.FTOrderNoRef = CREF.FTOrderNo "
                _Qry &= vbCrLf & "   WHERE  B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.ReceiveNo) & "' AND ISNULL(B.FTBarcodeGrpNo,'') ='' "

                _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
                _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType,ISNULL(B.FTOrderNoRef,''),ISNULL(CRef.FNHSysCmpId,0) "
                _Qry &= vbCrLf & "  ) AS A ) AS M"
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
                _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
                _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpToRef WITH (NOLOCK) ON M.FNHSysCmpIdToRef = CmpToRef.FNHSysCmpId"
                _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
                _Qry &= vbCrLf & "  ORDER BY  M.FTBarcodeNo"

                dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
                If dtauto.Rows.Count > 0 Then

                    ogcbarcode.DataSource = Nothing
                    FTStaSelectAll.Checked = False

                    ogcbarcode.DataSource = dtauto.Copy()

                Else
                    Me.Close()
                End If


            Else
                HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ สร้าง Barcode Group  เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1418827772, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

    End Sub

    Private Sub wReceiveAutoTransferToCenter_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvbarcode.OptionsSelection.MultiSelect = False
        ogvbarcode.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

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

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If FTBarcodeNo.Text.Trim() <> "" Then
                    Dim _BarRowInDex As Integer = -1
                    With Me.ogvbarcode
                        If .RowCount > 0 And FTBarcodeNo.Text.Trim() <> "" Then

                            Try
                                _BarRowInDex = .LocateByValue("FTBarcodeNo", FTBarcodeNo.Text.Trim())
                            Catch ex As Exception
                            End Try

                            Try
                                If _BarRowInDex <> -1 Then
                                    .FocusedRowHandle = _BarRowInDex
                                    .SelectRow(_BarRowInDex)
                                    .SetRowCellValue(_BarRowInDex, "FTSelect", "1")
                                End If
                            Catch ex As Exception
                            End Try

                        End If
                    End With
                    FTBarcodeNo.Focus()
                    FTBarcodeNo.SelectAll()
                End If
        End Select

    End Sub
End Class