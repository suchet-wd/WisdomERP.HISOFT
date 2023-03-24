Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wLocationFGList
    Private ds As DataSet
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataWareHouse()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            '_Cmd = "SELECT  '0' AS FTSelect ,  F.FTBarCodeCarton, F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, F.FNQuantity , S.FTStyleCode , O.FTPORef , F.FTSubOrderNo "
            '_Cmd &= vbCrLf & "   FROM  TPRODTBarcodeScanFG AS F WITH(NOLOCK) "
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON F.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId"
            '_Cmd &= vbCrLf & " Where F.FDInsDate = " & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & " AND F.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            ds = New DataSet()

            _Cmd = " SELECT   D.FNCartonQty,F.FTWHLocCode, F.FNHSysWHLocId,   D.FNHSysWHFGId,  S.FTStyleCode,  D.FTOrderNo,   "
            _Cmd &= vbCrLf & "   D.FTPOref"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", max(F.FTWHLocNameTH) as FTWHLocName "
            Else
                _Cmd &= vbCrLf & ", max(F.FTWHLocNameEN) as FTWHLocName  "
            End If
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.V_OnhandFG_ByLocation AS D LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG as F  WITH(NOLOCK) ON D.FNHSysWHLocId = F.FNHSysWHLocId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON D.FTOrderNo = O.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId"

            _Cmd &= vbCrLf & "  Where  F.FNHSysWHFGId=" & Integer.Parse(Me.FNHSysWHFGId.Properties.Tag.ToString)
            _Cmd &= vbCrLf & " and  isnull(F.FTStateActive,'0') = '1' "
            If Me.FNHSysWHLocId.Text <> "" Then
                _Cmd &= vbCrLf & " and F.FTWHLocCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHLocId.Text) & "'"
            End If
            If Me.FNHSysWHLocIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and F.FTWHLocCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHLocIdTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & " Group by D.FNCartonQty, F.FTWHLocCode, F.FNHSysWHLocId, D.FNHSysWHFGId,  D.FTPOref,  S.FTStyleCode,D.FTOrderNo "
            _Cmd &= vbCrLf & " Order by F.FTWHLocCode ASC "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcwarehouse.DataSource = _oDt


            _Cmd = " SELECT   F.FTWHLocCode,  D.FTBarCodeCarton,  D.FTOrderNo, D.FTPackNo, D.FNCartonNo, D.FTSubOrderNo,  "
            _Cmd &= vbCrLf & "   D.FTPOref, D.FTNikePOLineItem"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", max(F.FTWHLocNameTH) as FTWHLocName "
            Else
                _Cmd &= vbCrLf & ", max(F.FTWHLocNameEN) as FTWHLocName  "
            End If
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FG) & "].dbo.V_OnhandFG_ByLocation AS D LEFT OUTER JOIN  "
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG as F  WITH(NOLOCK) ON D.FNHSysWHLocId = F.FNHSysWHLocId  "
            _Cmd &= vbCrLf & "  Where  F.FNHSysWHFGId=" & Integer.Parse(Me.FNHSysWHFGId.Properties.Tag.ToString)
            _Cmd &= vbCrLf & " and  isnull(F.FTStateActive,'0') = '1' "
            If Me.FNHSysWHLocId.Text <> "" Then
                _Cmd &= vbCrLf & " and F.FTWHLocCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHLocId.Text) & "'"
            End If
            If Me.FNHSysWHLocIdTo.Text <> "" Then
                _Cmd &= vbCrLf & " and F.FTWHLocCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHLocIdTo.Text) & "'"
            End If

            _Cmd &= vbCrLf & " Group by  F.FTWHLocCode,  D.FTBarCodeCarton, D.FTOrderNo, D.FTPackNo, D.FNCartonNo, D.FTSubOrderNo, D.FTPOref, D.FTNikePOLineItem "
            _Cmd &= vbCrLf & " Order by F.FTWHLocCode ASC  , D.FTBarCodeCarton ASC "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            ds.Tables.Add(_oDt)


        Catch ex As Exception
        End Try
    End Sub



    Private Sub FNHSysWHFGId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysWHFGId.EditValueChanged
        Try
            If (Me.FNHSysWHFGId.Properties.Tag.ToString = "0" Or Me.FNHSysWHFGId.Properties.Tag.ToString = "") And Me.FNHSysWHFGId.Text <> "" Then
                Me.FNHSysWHFGId.Properties.Tag = HI.Conn.SQLConn.GetField(" Select Top 1 FNHSysWHFGId From [HITECH_MASTER]..TCNMWarehouseFG Where FTWHFGCode = '" & Me.FNHSysWHFGId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If
            Call LoadDataWareHouse()
        Catch ex As Exception
        End Try
    End Sub


    Private Function DelData(_FTProductBarcodeNo As String) As Boolean
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            Dim _Cmd As String = ""
            _Cmd = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG   "
            _Cmd &= vbCrLf & " WHERE FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & " And FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

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

    Private Function SaveData(_FTProductBarcodeNo As String, state As Boolean, Optional ByVal _FTOrderNo As String = "", Optional ByVal _FTColorway As String = "", Optional ByVal _FTSizeBreakDown As String = "") As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            _Cmd = "UPDATE W"
            _Cmd &= vbCrLf & "Set W.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ", W.FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",W.FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",W.FNQuantity=T.FNScanQuantity"
            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W INNER JOIN "
            '_Cmd &= vbCrLf & " (SELECT    B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, B.FTBarcodeNo, B.FNScanQuantity, C.FTBarCodeCarton"
            '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) INNER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON B.FTPackNo = C.FTPackNo AND B.FNCartonNo = C.FNCartonNo ) AS T "
            '_Cmd &= vbCrLf & " ON W.FTOrderNo = T.FTOrderNo and W.FTColorway = T.FTColorway and W.FTBarCodeCarton = T.FTBarCodeCarton and  W.FTSizeBreakDown = T.FTSizeBreakDown"
            '_Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            '_Cmd &= vbCrLf & " And W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)

            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W INNER JOIN "
            _Cmd &= vbCrLf & "   (SELECT  P.FTPackNo , P.FNCartonNo,  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, sum(B.FNScanQuantity ) AS FNScanQuantity ,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo) as FTBarCodeCarton "
            '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON P.FTPackNo = C.FTPackNo AND P.FNCartonNo = C.FNCartonNo "
            _Cmd &= vbCrLf & " WHERE (C.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & " OR   B.FTBarcodeNo='" & _FTProductBarcodeNo & "')"
            If (state) Then
                _Cmd &= vbCrLf & " And  B.FTOrderNo='" & _FTOrderNo & "'"
                _Cmd &= vbCrLf & " AND B.FTColorway='" & _FTColorway & "'"
                _Cmd &= vbCrLf & " And B.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
            End If

            _Cmd &= vbCrLf & "  and  isnull(C.FTBarCodeCarton,B.FTBarcodeNo) +'|'+B.FTOrderNo+'|'+B.FTColorway+'|'+B.FTSizeBreakDown +'|'+P.FTPackNo+'|'+convert(nvarchar(18) , P.FNCartonNo) not in ("
            _Cmd &= vbCrLf & " SELECT     FTBarCodeCarton +'|'+FTOrderNo +'|'+FTColorWay +'|'+FTSizeBreakDown+'|'+FTPackNo+'|'+convert(nvarchar(18) , FNCartonNo)  "
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG WITH(NOLOCK) )"

            _Cmd &= vbCrLf & "Group by P.FTPackNo , P.FNCartonNo, B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown ,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo) ) AS T "
            _Cmd &= vbCrLf & "ON W.FTOrderNo = T.FTOrderNo and W.FTColorWay = T.FTColorway and W.FTSizeBreakDown = T.FTSizeBreakDown and W.FTBarCodeCarton = T.FTBarCodeCarton and W.FTPackNo = T.FTPackNo and W.FNCartonNo = T.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)




            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity , FTPackNo , FNCartonNo)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & _FTProductBarcodeNo & "'"
                _Cmd &= vbCrLf & "," & CInt(Me.FNHSysWHFGId.Properties.Tag)
                _Cmd &= vbCrLf & ",T.FTColorWay"
                _Cmd &= vbCrLf & ",T.FTSizeBreakDown"
                _Cmd &= vbCrLf & ",T.FTOrderNo"
                _Cmd &= vbCrLf & ",T.FNScanQuantity"
                _Cmd &= vbCrLf & ",T.FTPackNo"
                _Cmd &= vbCrLf & ",T.FNCartonNo"
                _Cmd &= vbCrLf & " From (SELECT  P.FTPackNo , P.FNCartonNo,    B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, sum(B.FNScanQuantity ) AS FNScanQuantity,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo) as FTBarCodeCarton "
                '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
                _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON B.FTPackNo = C.FTPackNo AND B.FNCartonNo = C.FNCartonNo "
                _Cmd &= vbCrLf & " WHERE (C.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
                _Cmd &= vbCrLf & " OR   B.FTBarcodeNo='" & _FTProductBarcodeNo & "')"
                If (state) Then
                    _Cmd &= vbCrLf & " And  B.FTOrderNo='" & _FTOrderNo & "'"
                    _Cmd &= vbCrLf & " AND B.FTColorway='" & _FTColorway & "'"
                    _Cmd &= vbCrLf & " And B.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
                End If
                _Cmd &= vbCrLf & "  and  isnull(C.FTBarCodeCarton,B.FTBarcodeNo) +'|'+B.FTOrderNo+'|'+B.FTColorway+'|'+B.FTSizeBreakDown +'|'+P.FTPackNo+'|'+convert(nvarchar(18) , P.FNCartonNo) not in ("
                _Cmd &= vbCrLf & " SELECT     FTBarCodeCarton +'|'+FTOrderNo +'|'+FTColorWay +'|'+FTSizeBreakDown+'|'+FTPackNo+'|'+convert(nvarchar(18) , FNCartonNo)  "
                _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG WITH(NOLOCK) )"


                _Cmd &= vbCrLf & "Group by  P.FTPackNo , P.FNCartonNo,   B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo)) AS T "
                '_Cmd &= vbCrLf & " ON W.FTOrderNo = T.FTOrderNo and W.FTColorway = T.FTColorway and W.FTBarCodeCarton = T.FTBarCodeCarton and  W.FTSizeBreakDown = T.FTSizeBreakDown"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
        End Try
    End Function


    Private Function SaveDataGrid(_FTProductBarcodeNo As String, state As Boolean, _Qty As Double, _PackNo As String, _CartonNo As Integer, Optional ByVal _FTOrderNo As String = "", Optional ByVal _FTColorway As String = "", Optional ByVal _FTSizeBreakDown As String = "", Optional ByVal _SubOrderNo As String = "") As Boolean
        Try
            Dim _Cmd As String = ""

            '_Cmd = "UPDATE W"
            '_Cmd &= vbCrLf & "Set W.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & ", W.FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & ",W.FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & ",W.FNQuantity=" & _Qty

            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W "
            '_Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            '_Cmd &= vbCrLf & " And W.FTOrderNo='" & _FTOrderNo & "'"
            '_Cmd &= vbCrLf & " AND W.FTColorway='" & _FTColorway & "'"
            '_Cmd &= vbCrLf & " And W.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
            '_Cmd &= vbCrLf & " AND W.FTPackNo='" & _PackNo & "'"
            '_Cmd &= vbCrLf & " And W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            '_Cmd &= vbCrLf & " And W.FNCartonNo=" & _CartonNo


            'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity , FTPackNo , FNCartonNo ,FTSubOrderNo ,FNHSysWHLocId)"
            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",'" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & "," & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & ",'" & _FTColorway & "'"
            _Cmd &= vbCrLf & ",'" & _FTSizeBreakDown & "'"
            _Cmd &= vbCrLf & ",'" & _FTOrderNo & "'"
            _Cmd &= vbCrLf & "," & _Qty
            _Cmd &= vbCrLf & ",'" & _PackNo & "'"
            _Cmd &= vbCrLf & "," & _CartonNo
            _Cmd &= vbCrLf & " , '" & _SubOrderNo & "'"
            _Cmd &= vbCrLf & " , " & Integer.Parse(Me.FNHSysWHLocId.Properties.Tag)
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            'End If

            Return True
        Catch ex As Exception
        End Try
    End Function


    Private Sub wScanBarcodeFG_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Call LoadData()
            'Me.FTProductBarcodeNo.EnterMoveNextControl = False
            'Me.FTProductBarcodeNo.Focus()
            'Me.FTProductBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub oFTSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            'Dim _State As String = IIf(oFTSelectAll.Checked = True, "1", "0")
            'With ogcdetail
            '    If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
            '        With ogvdetail
            '            For I As Integer = 0 To .RowCount - 1
            '                .SetRowCellValue(I, "FTSelect", _State)
            '            Next
            '        End With
            '        CType(.DataSource, DataTable).AcceptChanges()
            '    End If
            'End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub oGFTSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oGFTSelectAll.CheckedChanged
        Try
            Dim _State As String = IIf(oGFTSelectAll.Checked = True, "1", "0")
            With ogcwarehouse
                If Not (.DataSource Is Nothing) And ogvwarehouse.RowCount > 0 Then
                    With ogvwarehouse
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadDataWareHouse()
    End Sub

    Private Sub ogvwarehouse_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles ogvwarehouse.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    Private Sub ogvwarehouse_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles ogvwarehouse.MasterRowGetRelationName
        e.RelationName = "FTWHLocCode"

    End Sub

    Private Sub ogvwarehouse_MasterRowEmpty(sender As Object, e As MasterRowEmptyEventArgs) Handles ogvwarehouse.MasterRowEmpty
        e.IsEmpty = False
    End Sub

    Private Sub ogvwarehouse_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles ogvwarehouse.MasterRowGetChildList
        Try
            Dim view As GridView = DirectCast(sender, GridView)
            e.ChildList = GetDetail(ogvwarehouse.GetRowCellValue(ogvwarehouse.FocusedRowHandle, "FTWHLocCode").ToString)

        Catch ex As Exception

        End Try
    End Sub


    Private Function GetDetail(Key As String) As DataView
        Try
            Dim dt As DataTable = ds.Tables(0)
            Dim dv As DataView = New DataView(dt)

            dv.RowFilter = "FTWHLocCode='" & Key & "'"
            dv.AllowEdit = False
            dv.AllowNew = False
            dv.AllowDelete = False

            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class