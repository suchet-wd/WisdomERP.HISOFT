Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wAddCutItem
    Private sFTOrderNo As String
    Private sFTOrderProdNo As String

    Dim dtStyleDetail As DataTable
    Private _PartId As Integer = 0
    Public Property PartId As Integer
        Get
            Return _PartId
        End Get
        Set(value As Integer)
            _PartId = value
        End Set
    End Property

    Private _UpdateState As Boolean = False
    Public Property UpdateState As Boolean
        Get
            Return _UpdateState
        End Get
        Set(value As Boolean)
            _UpdateState = value
        End Set
    End Property
    Private _FTOrderProdNo As String = ""
    Public Property FTOrderProdNo As String
        Get
            Return _FTOrderProdNo
        End Get
        Set(value As String)
            _FTOrderProdNo = value
        End Set
    End Property
    Public Sub New(ByVal srcFTOrderNo As String, ByVal srcFTOrderProdNo As String)

        ' This call is required by the designer.
        InitializeComponent()

        sFTOrderNo = srcFTOrderNo
        sFTOrderProdNo = srcFTOrderProdNo
        Me.FTOrderNo.Text = srcFTOrderNo
        FTOrderProdNo = srcFTOrderProdNo



        FDSaveDate.Text = Date.Today
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wAddCutItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler ReposPartCode.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        AddHandler ReposColorway.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
    End Sub

    Private Sub InitNewRow(ByVal dataTable As System.Data.DataTable) 'add new row in gridview
        Try
            If dataTable.Select("Total <= 0").Length > 0 Then
                Exit Sub
            End If

            Dim _ogc As Object
            Dim _ct As Integer = 0
            Dim dr As DataRow
            dtStyleDetail = dataTable
            dr = dataTable.NewRow()

            Dim LastMatSeq As Integer = dtStyleDetail.Rows.Count + 1

            _ogc = ogcpart

            dr.Item("FTOrderNo") = FTOrderNo.Text
            dr.Item("FTOrderProdNo") = FTOrderProdNo
            dr.Item("FTColorway") = ""
            dr.Item("FNHSysPartId") = ""
            dr.Item("FTPartCode") = ""
            dr.Item("FTPartName") = ""
            dr.Item("FNSeq") = 0

            dtStyleDetail.Rows.Add(dr)

            For Each _R As DataRow In dtStyleDetail.Select("FTColorway = ''")
                For Each Col As DataColumn In dtStyleDetail.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FNSeq".ToUpper
                        Case Else
                            _R(Col.ColumnName.ToString) = 0
                    End Select
                Next
            Next

            ogcpart.DataSource = dtStyleDetail
            ogcpart.Refresh()

        Catch ex As Exception
        End Try
    End Sub

    'Private Sub CreateNewTable(ByVal dataTable As System.Data.DataTable) 'add new row in gridview
    '    Try
    '        'If dataTable.Select("FTPartCode=''").Length > 0 Then
    '        '    Exit Sub
    '        'End If
    '        Dim NTable As DataTable
    '        Dim _ogc As Object
    '        Dim _ct As Integer = 0
    '        Dim dr As DataRow

    '        NTable = New System.Data.DataTable()
    '        dtStyleDetail = dataTable
    '        dr = dataTable.NewRow()

    '        Dim oColColorWay As DataColumn
    '        oColColorWay = New DataColumn("FTColorway", GetType(String))
    '        NTable.Columns.Add(oColColorWay.ColumnName, oColColorWay.DataType)

    '        Dim oColPartId As DataColumn = New DataColumn("FNHSysPartId", GetType(String))
    '        NTable.Columns.Add(oColPartId.ColumnName, oColPartId.DataType)

    '        Dim oColPartCode As DataColumn = New DataColumn("FTPartCode", GetType(String))
    '        NTable.Columns.Add(oColPartCode.ColumnName, oColPartCode.DataType)

    '        Dim oColFTPartName As DataColumn
    '        oColFTPartName = New DataColumn("FTPartName", System.Type.GetType("System.String"))
    '        NTable.Columns.Add(oColFTPartName.ColumnName, oColFTPartName.DataType)

    '        _ogc = ogcpart

    '        dr.Item("FTColorway") = ""
    '        dr.Item("FNHSysPartId") = ""
    '        dr.Item("FTPartCode") = ""
    '        dr.Item("FTPartName") = ""


    '        dtStyleDetail.Rows.Add(dr)

    '        ogcpart.DataSource = dtStyleDetail
    '        ogcpart.Refresh()

    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private _StateSumGrid As Boolean
    Private Sub SumGrid()
        _StateSumGrid = True
        CType(ogcpart.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            _Total = 0
            With Me.ogvpart

                For I As Integer = 0 To .RowCount - 1
                    _Total = 0
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
                            Case Else
                                If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                    _Total = _Total + CDbl(.GetRowCellValue(I, GridCol))
                                Else
                                    _Total = _Total + 0
                                End If
                        End Select

                    Next

                    .SetRowCellValue(I, "Total", _Total)
                Next

            End With

        Catch ex As Exception
        End Try

        CType(ogcpart.DataSource, DataTable).AcceptChanges()

        _StateSumGrid = False
    End Sub

    Public Sub LoadOrderCutItemUnitSect()
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _dtprod As DataTable

        _Qry = " SELECT FTOrderProdNo, FTColorway, FNHSysUnitSectId ,   FNSeq"
        _Qry &= vbCrLf & " , CD.FNHSysPartId, P.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,P.FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,P.FTPartNameEN AS FTPartName"
        End If

        _Qry &= vbCrLf & " , FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail AS CD WITH(NOLOCK)"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON CD.FNHSysPartId = P.FNHSysPartId"
        _Qry &= vbCrLf & "   WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
        _Qry &= vbCrLf & "       AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"

        _Qry &= vbCrLf & "       AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
        'If _PartId > 0 Then
        _Qry &= vbCrLf & "       AND CD.FNHSysPartId =" & Val(_PartId) & ""
        'End If
        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _Qry = " SELECT FTColorway"
        _Qry &= vbCrLf & " , CD.FNHSysPartId, P.FTPartCode , FNHSysUnitSectId , FNSeq"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,P.FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & " ,P.FTPartNameEN AS FTPartName"
        End If

        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail AS CD WITH(NOLOCK)"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON CD.FNHSysPartId = P.FNHSysPartId"
        _Qry &= vbCrLf & "   WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
        _Qry &= vbCrLf & "       AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        _Qry &= vbCrLf & "       AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
        'If _PartId > 0 Then
        _Qry &= vbCrLf & "       AND CD.FNHSysPartId =" & Val(_PartId) & ""
        'End If

        _Qry &= vbCrLf & "       GROUP BY FTColorway, CD.FNHSysPartId, P.FTPartCode ,FNHSysUnitSectId , FNSeq"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,P.FTPartNameTH"
        Else
            _Qry &= vbCrLf & " ,P.FTPartNameEN"
        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        CType(ogcpart.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            Dim _balDt As DataTable
            Dim _partDt As DataTable
            Dim _jobbalDt As DataTable
            _Total = 0

            'With CType(ogcbalance.DataSource, DataTable)
            '    .AcceptChanges()
            '    _balDt = .Copy
            'End With

            _partDt = ogcpart.DataSource
            '_jobbalDt = ogcjobprodbal.DataSource

            With CType(Me.ogcpart.DataSource, DataTable)
                .AcceptChanges()
                If _dt.Rows.Count >= 1 And .Rows.Count <= 0 Then
                    .Rows.Add(.NewRow())
                End If

                If _dt.Rows.Count > 1 Then
                    For i As Integer = 2 To _dt.Rows.Count
                        .Rows.Add(.NewRow())
                    Next

                End If
                Dim _row As Integer = 0
                For Each _R As DataRow In _dt.Select("FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " ")


                    For Each _Col As DataColumn In .Columns
                        Select Case _Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper

                                .Rows(_row).Item(_Col.ColumnName.ToString) = FTOrderNo.Text
                            Case "FTOrderProdNo".ToUpper
                                .Rows(_row).Item(_Col.ColumnName.ToString) = FTOrderProdNo
                            Case "FTColorway".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FNHSysUnitSectId".ToUpper
                                Try
                                    If (.Rows(_row).Item(_Col.ColumnName.ToString) = "") Then
                                        .Rows(_row).Item(_Col.ColumnName.ToString) = _R.Item(_Col.ColumnName.ToString).ToString
                                    End If
                                Catch ex As Exception
                                    .Rows(_row).Item(_Col.ColumnName.ToString) = _R.Item(_Col.ColumnName.ToString).ToString
                                End Try

                            Case "FNSeq".ToUpper

                                .Rows(_row).Item(_Col.ColumnName.ToString) = _R.Item(_Col.ColumnName.ToString).ToString

                            Case "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper
                            Case Else
                        End Select
                    Next


                    _row += +1

                Next


                For Each _Rbal As DataRow In .Rows
                    For Each _Col As DataColumn In .Columns
                        Select Case _Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper
                                .Rows(_row).Item(_Col.ColumnName.ToString) = FTOrderNo.Text
                            Case "FTOrderProdNo".ToUpper
                                .Rows(_row).Item(_Col.ColumnName.ToString) = FTOrderProdNo
                            Case "FTColorway".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
                            Case Else
                                Try
                                    For Each _mR As DataRow In _dtprod.Select("FNHSysPartId='" & _Rbal!FNHSysPartId.ToString & "' AND FTSizeBreakDown ='" & _Col.ColumnName.ToString & "' and FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & " and FNSeq=" & Integer.Parse("0" & _Rbal!FNSeq.ToString) & "")
                                        _Rbal.Item(_Col.ColumnName.ToString) = _mR!FNQuantity
                                    Next
                                Catch ex As Exception

                                End Try

                        End Select
                    Next
                Next
                Call SumGrid()
                CType(ogcpart.DataSource, DataTable).AcceptChanges()
                With DirectCast(Me.ogcpart.DataSource, DataTable)
                    .AcceptChanges()
                    If (.Select("FTColorway = ''").Length <= 0) And _PartId = 0 Then
                        Call InitNewRow(CType(ogcpart.DataSource, DataTable))
                    End If
                End With


            End With
            Call SumGrid()
        Catch ex As Exception
        End Try

        CType(ogcpart.DataSource, DataTable).AcceptChanges()

    End Sub

    Private Function PROC_SAVECutOrder() As Boolean
        Dim _Qry As String = ""
        Dim _QryCheckSeq As String = ""
        Dim bRet As Boolean = False
        Dim Maxleng As String = ""

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
            _Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            _Qry &= vbCrLf & " WHERE  FTOrderNo='" & FTOrderNo.Text & "' AND FTOrderProdNo ='" & sFTOrderProdNo & "' "
            _Qry &= vbCrLf & "AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo, FNHSysUnitSectId, FNHSysCmpId) "
                _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & FTOrderNo.Text & "'"
                '_Qry &= vbCrLf & ",'" & _FTSubOrderNo & "'"
                _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "' "
                '_Qry &= vbCrLf & ",'" & _FTColorway & "'"
                _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID)

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If

            Call SaveDetail(FTOrderNo.Text)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveDetail(ByVal _Key As String) As Boolean 'save detail in gridview to DB
        Try
            Dim _Qry As String = ""
            Dim _Seq As Integer = 0


            If Not (ogcpart.DataSource Is Nothing) Then
                _Seq = 0
                Dim dt As DataTable
                With CType(ogcpart.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                '_Qry = "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPRODTCutOrderRecord_Detail]    "
                '_Qry &= vbCrLf & " WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
                '_Qry &= vbCrLf & "       AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                '_Qry &= vbCrLf & "       AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & "" & "' AND FNHSysPartId = " & R!FNHSysPartId

                'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                If (_UpdateState) Then
                    ' Update 
                    For Each R As DataRow In dt.Rows
                        '_Seq += +1
                        If (R!FNHSysPartId <> "") Then
                            For Each Col As DataColumn In dt.Columns
                                Select Case Col.ColumnName.ToString.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
                                    Case Else
                                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
                                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                        _Qry &= vbCrLf & ",FNQuantity=" & CInt("0" & R(Col.ColumnName.ToString).ToString)

                                        _Qry &= vbCrLf & "WHERE FTOrderProdNo='" & sFTOrderProdNo & "' AND FNHSysPartId = " & R!FNHSysPartId & " AND FDSaveDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                                        _Qry &= vbCrLf & " AND FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                        _Qry &= vbCrLf & " and FNSeq =" & Integer.Parse(R!FNSeq.ToString)

                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
                                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FDSaveDate, FNHSysUnitSectId, FTColorway, FNHSysPartId, FTSizeBreakDown, FNQuantity , FNSeq"
                                            _Qry &= vbCrLf & ")"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                            _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                                            _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                            _Qry &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
                                            _Qry &= vbCrLf & "," & R!FNHSysPartId
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
                                            _Qry &= vbCrLf & "," & CInt("0" & R(Col.ColumnName.ToString).ToString)
                                            _Qry &= vbCrLf & "," & Integer.Parse(R!FNSeq.ToString)
                                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                HI.Conn.SQLConn.Tran.Rollback()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                Return False
                                            End If
                                        End If
                                End Select
                            Next
                        End If
                    Next

                Else
                    'Insert 
                    _Qry = "  Select Max(FNSeq) AS FNSeq  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPRODTCutOrderRecord_Detail]    "
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
                    _Qry &= vbCrLf & "       AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                    _Qry &= vbCrLf & "       AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & "" ' & "' AND FNHSysPartId = " & R!FNHSysPartId

                    _Seq = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, 0) + 1

                    For Each R As DataRow In dt.Rows
                        '_Seq += +1
                        If (R!FNHSysPartId <> "") Then
                            For Each Col As DataColumn In dt.Columns
                                Select Case Col.ColumnName.ToString.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
                                    Case Else
                                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
                                        _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                        _Qry &= vbCrLf & ",FNQuantity=" & CInt("0" & R(Col.ColumnName.ToString).ToString)

                                        _Qry &= vbCrLf & "WHERE FTOrderProdNo='" & sFTOrderProdNo & "' AND FNHSysPartId = " & R!FNHSysPartId & " AND FDSaveDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                                        _Qry &= vbCrLf & " AND FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                        _Qry &= vbCrLf & " and FNSeq =" & Integer.Parse(_Seq)

                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
                                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FDSaveDate, FNHSysUnitSectId, FTColorway, FNHSysPartId, FTSizeBreakDown, FNQuantity , FNSeq"
                                            _Qry &= vbCrLf & ")"
                                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                            _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                                            _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                            _Qry &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
                                            _Qry &= vbCrLf & "," & R!FNHSysPartId
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
                                            _Qry &= vbCrLf & "," & CInt("0" & R(Col.ColumnName.ToString).ToString)
                                            _Qry &= vbCrLf & "," & Integer.Parse(_Seq)
                                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                HI.Conn.SQLConn.Tran.Rollback()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                Return False
                                            End If
                                        End If
                                End Select
                            Next
                        End If
                    Next

                End If



                '_Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
                '_Qry &= vbCrLf & "WHERE FTOrderProdNo='" & _Key & "' "
                'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If
        Catch ex As Exception
        End Try
        Return True
    End Function


    Public Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        Dim _Col As String
        Dim _dtprod As DataTable
        Dim NTable As DataTable

        NTable = New System.Data.DataTable()

        Dim oColCFTOrder As DataColumn
        oColCFTOrder = New DataColumn("FTOrderNo", GetType(String))
        NTable.Columns.Add(oColCFTOrder.ColumnName, oColCFTOrder.DataType)

        Dim oColCFTOrderProd As DataColumn
        oColCFTOrderProd = New DataColumn("FTOrderProdNo", GetType(String))
        NTable.Columns.Add(oColCFTOrderProd.ColumnName, oColCFTOrderProd.DataType)


        Dim oColColorWay As DataColumn
        oColColorWay = New DataColumn("FTColorway", GetType(String))
        NTable.Columns.Add(oColColorWay.ColumnName, oColColorWay.DataType)

        'Dim oColPartId As DataColumn = New DataColumn("FNHSysPartId", System.Type.GetType("System.Int32"))
        Dim oColPartId As DataColumn = New DataColumn("FNHSysPartId", GetType(String))
        NTable.Columns.Add(oColPartId.ColumnName, oColPartId.DataType)

        Dim oColPartCode As DataColumn = New DataColumn("FTPartCode", GetType(String))
        NTable.Columns.Add(oColPartCode.ColumnName, oColPartCode.DataType)

        Dim oColFTPartName As DataColumn
        oColFTPartName = New DataColumn("FTPartName", System.Type.GetType("System.String"))
        NTable.Columns.Add(oColFTPartName.ColumnName, oColFTPartName.DataType)

        Dim oColFNSeq As DataColumn
        oColFNSeq = New DataColumn("FNSeq", GetType(Integer))
        NTable.Columns.Add(oColFNSeq.ColumnName, oColFNSeq.DataType)

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown '" & sFTOrderProdNo & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dt.Columns.Add("FTOrderProdNo", GetType(String))
        For Each R As DataRow In _dt.Rows
            R!FTOrderProdNo = sFTOrderProdNo
        Next

        _colcount = 0
        With Me.ogvpart

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FNSeq".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FNSeq".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                                _Col = Col.ColumnName.ToString
                                .DisplayFormat.FormatString = "{0:n0}"
                                If Not (Col.ColumnName.ToString = "Total") Then
                                    ReposCaleditWeight.Buttons(0).Visible = False

                                    .ColumnEdit = ReposCaleditWeight
                                Else

                                End If

                            End With

                            '_dtprod.Columns.Add(_Col)
                            NTable.Columns.Add(_Col)
                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far


                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    '.AllowEdit = True
                                    '.ReadOnly = False

                                    If Not (Col.ColumnName.ToString = "Total") Then
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    Else
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End If
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center


                        If (.OptionsColumn.AllowEdit) Then
                            .AppearanceCell.BackColor = Color.LightCyan
                        End If
                    End With
                Next

            End If
        End With

        For Each _R As DataRow In NTable.Rows
            For Each Col As DataColumn In NTable.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "FNSeq".ToUpper
                    Case Else
                        _R(Col.ColumnName.ToString) = 0
                End Select
            Next
        Next

        ogcpart.DataSource = NTable

        CType(ogcpart.DataSource, DataTable).AcceptChanges()
        If Not _UpdateState Then
            Call InitNewRow(CType(ogcpart.DataSource, DataTable))
        End If

    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        'CType(ogcpart.DataSource, DataTable).AcceptChanges()
        'Call InitNewRow(CType(ogcpart.DataSource, DataTable))
        Try
            If (FNHSysUnitSectId.Text <> "") Then
                If PROC_SAVECutOrder() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mProcessError(1711211727, "กรุณาใส่สังกัด ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Me.FNHSysUnitSectId.Focus()
            End If
        Catch ex As Exception

        End Try

        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ReposCaleditWeight_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCaleditWeight.EditValueChanging
        Try
            Dim _NewValue As Double = e.NewValue
            Dim _OrgValue As Double = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""
            Dim _Dt As DataTable = ogcpart.DataSource
            Dim _PartId As String
            Dim _Seq As Integer = 0

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")
                    _PartId = .GetFocusedRowCellValue("FNHSysPartId")
                    _Seq = Integer.Parse("0" & .GetFocusedRowCellValue("FNSeq"))

                    If Not (_StateSumGrid) Then
                        Dim _ColName As String = .FocusedColumn.FieldName.ToString
                        With CType(ogcpart.DataSource, DataTable)
                            .AcceptChanges()

                            For Each R As DataRow In .Select("FNSeq=" & _Seq)
                                If (R!FNHSysPartId = _PartId) Then
                                    R.Item(_ColName) = _NewValue
                                    Exit For
                                End If
                            Next

                        End With

                        ' ogvpackdetailWeight.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))

                        If Not (ogcpart.DataSource Is Nothing) Then
                            Call SumGrid()
                        End If

                    End If

                End With
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub ReposPartCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposPartCode.EditValueChanged
        CType(ogcpart.DataSource, DataTable).AcceptChanges()
        Call InitNewRow(CType(ogcpart.DataSource, DataTable))
    End Sub

    Private Sub ogvpart_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvpart.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        If (.RowCount = 0) Then
                            Call InitNewRow(CType(ogcpart.DataSource, DataTable))
                        End If
                        'With CType(ogcfabric.DataSource, DataTable)
                        '    .AcceptChanges()
                        '    Dim x As Integer = 0
                        '    For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                        '        x += +1
                        '        r!FNSeq = x
                        '    Next
                        '    .AcceptChanges()
                        'End With
                    End With
                    'SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        FTOrderNo_EditValueChanged(sender, e)
        LoadOrderCutItemUnitSect()
    End Sub

    Private Sub ogvpart_CustomDrawRowIndicator(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs) Handles ogvpart.CustomDrawRowIndicator
        If e.RowHandle >= 0 Then
            e.Info.DisplayText = Val("0" & e.RowHandle.ToString() + 1).ToString

        End If
    End Sub

    Private Sub ogvpart_RowCountChanged(sender As Object, e As EventArgs) Handles ogvpart.RowCountChanged
        Try
            Dim grid As GridView = DirectCast(sender, GridView)

            grid.IndicatorWidth = 30
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmRowDel_Click(sender As Object, e As EventArgs) Handles ocmRowDel.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบการบันทึกงานตัด ใช่หรือไม่ !!!!!", 1905061707, Me.Text) = False Then Exit Sub
            With Me.ogvpart
                If .FocusedRowHandle < -1 Or .RowCount < 0 Then Exit Sub
                Dim _Cmd As String = ""

                Dim _PartIds As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString)
                Dim _Seq As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)

                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
                _Cmd &= vbCrLf & "  WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
                _Cmd &= vbCrLf & "  AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
                _Cmd &= vbCrLf & "  AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
                _Cmd &= vbCrLf & "  AND FNHSysPartId =" & Val(_PartIds) & ""
                _Cmd &= vbCrLf & "  AND FNSeq=" & _Seq
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                .DeleteRow(.FocusedRowHandle)
            End With
            LoadOrderCutItemUnitSect()
        Catch ex As Exception

        End Try
    End Sub
End Class