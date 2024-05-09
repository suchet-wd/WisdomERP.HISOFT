Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wAddGenProdJob
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

    Private _ODTRatio As DataTable
    Public Property ODTRatio As DataTable
        Get
            Return _ODTRatio
        End Get
        Set(value As DataTable)
            _ODTRatio = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _odtConfig As DataTable
    Public Property odtConfig As DataTable
        Get
            Return _odtConfig
        End Get
        Set(value As DataTable)
            _odtConfig = value
        End Set
    End Property

    Private _Totalent As Double = 0.0
    Public Property Totalent As Double
        Get
            Return _Totalent
        End Get
        Set(value As Double)
            _Totalent = value
        End Set
    End Property
    Private Sub wAddCutItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub InitNewRow(ByVal dataTable As System.Data.DataTable) 'add new row in gridview
        Try

        Catch ex As Exception
        End Try
    End Sub

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

            '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
            '_Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            '_Qry &= vbCrLf & " WHERE  FTOrderNo='" & FTOrderNo.Text & "' AND FTOrderProdNo ='" & sFTOrderProdNo & "' "
            '_Qry &= vbCrLf & "AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)

            'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord "
            '    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo, FNHSysUnitSectId, FNHSysCmpId) "
            '    _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '    _Qry &= vbCrLf & ",'" & FTOrderNo.Text & "'"
            '    '_Qry &= vbCrLf & ",'" & _FTSubOrderNo & "'"
            '    _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "' "
            '    '_Qry &= vbCrLf & ",'" & _FTColorway & "'"
            '    _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
            '    _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID)

            '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '        HI.Conn.SQLConn.Tran.Rollback()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        Return False
            '    End If
            'End If

            'Call SaveDetail(FTOrderNo.Text)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function CreateNewJobProducttion() As Boolean



        'Dim _Spls As New HI.TL.SplashScreen("Generating Job Production...Please Wait")
        Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _dtjobprod As DataTable
        Dim _tmpOrderProd As String = ""
        Dim _dt As DataTable
        Dim _groupNo As String = ""
        _Qry = "Select top 1  FTGroupNo  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan with(nolock)  "
        _Qry &= vbCrLf & " where  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "
        _groupNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_MU_OrderBreakDown  @FTGroupNo ='" & HI.UL.ULF.rpQuoted(_groupNo) & "' "
        _Qry &= vbCrLf & " , @FTDocumentNo ='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)




        Dim I As Integer = 0
        Try

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _dt.Rows


                _OrderNo = R!FTOrderNo.ToString
                _SubOrderNo = R!FTSubOrderNo.ToString
                _ColorWay = R!FTColorWay.ToString

                For Each Col As DataColumn In _dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FNHSysStyleId".ToUpper, "FTCustomerPO".ToUpper, "FTPOLine".ToUpper,
                               "FNHSysStyleId_Hide".ToUpper
                        Case Else

                            If (Double.Parse("0" & R.Item(Col.ColumnName.ToString))) > 0 Then
                                _Qry = "Insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd "
                                _Qry &= vbCrLf & " ( FTUserLogIn, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorWay) & "' "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                _Qry &= vbCrLf & "," & Double.Parse(R.Item(Col.ColumnName.ToString)) & " "

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                    HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
                                    '_Spls.Close()
                                    Return False

                                End If

                            End If

                    End Select
                Next


                I = I + 1
            Next




            '_Qry = " SELECT        T.FTOrderNo"
            '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
            '_Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
            '_Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Qry &= vbCrLf & "  GROUP BY T.FTOrderNo"
            '_dtjobprod = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)


            'Dim _dtmain As DataTable
            '_Qry = "Select  distinct  FTGroupNo , FTDocumentNo , FTColorWay , FTOrderNo  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUGroupPlan  where FTDocumentNo = '" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "
            '_dtmain = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            'For Each R As DataRow In _dtjobprod.Rows
            _tmpOrderProd = ""

            _Qry = "   SELECT  TOP 1 FTOrderProdNo"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd"
            _Qry &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' AND LEN(FTOrderProdNo) = LEN('" & HI.UL.ULF.rpQuoted(FTOrderProdNo & "-P001") & "') "
            _Qry &= vbCrLf & "  ORDER BY FTOrderProdNo DESC "
            _tmpOrderProd = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

            If _tmpOrderProd = "" Then
                _tmpOrderProd = HI.UL.ULF.rpQuoted(FTOrderProdNo) & "-P001"
            Else
                _tmpOrderProd = HI.UL.ULF.rpQuoted(FTOrderProdNo) & "-P" & Microsoft.VisualBasic.Right("0000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderProd, 3)) + 1, "0"), 3)
            End If



            'For Each M As DataRow In _dtmain.Select("FTColorWay='" & R!FTColorway.ToString & "'")
            _Qry = ""
            _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd "
            _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTOrderProdNo,FNHSysCmpId,FTDocumentNo)"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "


            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail "
            _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTOrderProdNo, FTSubOrderNo,FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCmpId,FTDocumentNo)"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",T.FTOrderNo " '& HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "' "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
            _Qry &= vbCrLf & ",T.FTSubOrderNo"
            _Qry &= vbCrLf & ",T.FTColorway"
            _Qry &= vbCrLf & ",T.FTSizeBreakDown"
            _Qry &= vbCrLf & ",T.FNQuantity"
            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "

            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd AS T INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S ON T.FTOrderNo = S.FTOrderNo AND T.FTSubOrderNo = S.FTSubOrderNo"
            _Qry &= vbCrLf & " WHERE T.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Qry &= vbCrLf & " AND T.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
            '_Qry &= vbCrLf & " AND T.FTOrderNo='" & HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            'Next


            'Next






            Try

                _Qry = "Select  distinct  FTOrderNo from TPRODMUGroupPlan  "
                _Qry &= vbCrLf & " where FTGroupNo='" & HI.UL.ULF.rpQuoted(_groupNo) & "' "
                _Qry &= vbCrLf & " and  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "' "

                For Each M As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows

                    _Qry = "   UPDATE A SET FTStateCut ='1'	 "
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                    _Qry &= vbCrLf & "      INNER Join"
                    _Qry &= vbCrLf & "  ("
                    _Qry &= vbCrLf & "   SELECT FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
                    _Qry &= vbCrLf & "   WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "')"
                    _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                    _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                    _Qry &= vbCrLf & "     WHERE A.FTStateCut <>'1'"
                    _Qry &= vbCrLf & "   IF @@ROWCOUNT <=0"
                    _Qry &= vbCrLf & "    BEGIN"
                    _Qry &= vbCrLf & "      INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus("
                    _Qry &= vbCrLf & "      FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "      	,FTStateCut, FTStateSew, FTStatePack)"
                    _Qry &= vbCrLf & "      SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "      ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "      ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "      ,B.FTOrderNo"
                    _Qry &= vbCrLf & "      ,B.FTSubOrderNo "
                    _Qry &= vbCrLf & "      ,'1'"
                    _Qry &= vbCrLf & "      ,'0'"
                    _Qry &= vbCrLf & "      ,'0'"
                    _Qry &= vbCrLf & "       FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                    _Qry &= vbCrLf & "       RIGHT OUTER JOIN"
                    _Qry &= vbCrLf & "       ("
                    _Qry &= vbCrLf & "       SELECT FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "        FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_Detail"
                    _Qry &= vbCrLf & "       WHERE (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(M!FTOrderNo.ToString) & "')"

                    _Qry &= vbCrLf & "       GROUP BY FTOrderNo, FTSubOrderNo"
                    _Qry &= vbCrLf & "       ) AS B ON A.FTOrderNo = B.FTOrderNo"
                    _Qry &= vbCrLf & "        AND A.FTSubOrderNo = B.FTSubOrderNo"
                    _Qry &= vbCrLf & "        WHERE A.FTOrderNo Is NULL"
                    _Qry &= vbCrLf & "    END "

                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                Next


            Catch ex As Exception
            End Try




            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                SaveTableCutting(_tmpOrderProd)


            Catch ex As Exception

            End Try

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
            '_Spls.Close()
            Return False
        End Try
        HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)
        '_Spls.Close()
        Return True
    End Function



    Private Function SaveDetail(ByVal _Key As String) As Boolean 'save detail in gridview to DB
        'Try
        '    Dim _Qry As String = ""
        '    Dim _Seq As Integer = 0


        '    If Not (ogcpart.DataSource Is Nothing) Then
        '        _Seq = 0
        '        Dim dt As DataTable
        '        With CType(ogcpart.DataSource, DataTable)
        '            .AcceptChanges()
        '            dt = .Copy
        '        End With

        '        '_Qry = "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPRODTCutOrderRecord_Detail]    "
        '        '_Qry &= vbCrLf & " WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
        '        '_Qry &= vbCrLf & "       AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        '        '_Qry &= vbCrLf & "       AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & "" & "' AND FNHSysPartId = " & R!FNHSysPartId

        '        'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


        '        If (_UpdateState) Then
        '            ' Update 
        '            For Each R As DataRow In dt.Rows
        '                '_Seq += +1
        '                If (R!FNHSysPartId <> "") Then
        '                    For Each Col As DataColumn In dt.Columns
        '                        Select Case Col.ColumnName.ToString.ToUpper
        '                            Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
        '                            Case Else
        '                                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '                                _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                                _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
        '                                _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
        '                                _Qry &= vbCrLf & ",FNQuantity=" & CInt("0" & R(Col.ColumnName.ToString).ToString)

        '                                _Qry &= vbCrLf & "WHERE FTOrderProdNo='" & sFTOrderProdNo & "' AND FNHSysPartId = " & R!FNHSysPartId & " AND FDSaveDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        '                                _Qry &= vbCrLf & " AND FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
        '                                _Qry &= vbCrLf & " and FNSeq =" & Integer.Parse(R!FNSeq.ToString)

        '                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '                                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FDSaveDate, FNHSysUnitSectId, FTColorway, FNHSysPartId, FTSizeBreakDown, FNQuantity , FNSeq"
        '                                    _Qry &= vbCrLf & ")"
        '                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
        '                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
        '                                    _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "'"
        '                                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        '                                    _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
        '                                    _Qry &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
        '                                    _Qry &= vbCrLf & "," & R!FNHSysPartId
        '                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
        '                                    _Qry &= vbCrLf & "," & CInt("0" & R(Col.ColumnName.ToString).ToString)
        '                                    _Qry &= vbCrLf & "," & Integer.Parse(R!FNSeq.ToString)
        '                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                                        HI.Conn.SQLConn.Tran.Rollback()
        '                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                                        Return False
        '                                    End If
        '                                End If
        '                        End Select
        '                    Next
        '                End If
        '            Next

        '        Else
        '            'Insert 
        '            _Qry = "  Select Max(FNSeq) AS FNSeq  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPRODTCutOrderRecord_Detail]    "
        '            _Qry &= vbCrLf & " WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
        '            _Qry &= vbCrLf & "       AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        '            _Qry &= vbCrLf & "       AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & "" ' & "' AND FNHSysPartId = " & R!FNHSysPartId

        '            _Seq = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, 0) + 1

        '            For Each R As DataRow In dt.Rows
        '                '_Seq += +1
        '                If (R!FNHSysPartId <> "") Then
        '                    For Each Col As DataColumn In dt.Columns
        '                        Select Case Col.ColumnName.ToString.ToUpper
        '                            Case "FTOrderNo".ToUpper, "FTOrderProdNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOref".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysPartId".ToUpper, "FTPartCode".ToUpper, "FTPartName".ToUpper, "Total".ToUpper, "FNSeq".ToUpper
        '                            Case Else
        '                                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '                                _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                                _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
        '                                _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
        '                                _Qry &= vbCrLf & ",FNQuantity=" & CInt("0" & R(Col.ColumnName.ToString).ToString)

        '                                _Qry &= vbCrLf & "WHERE FTOrderProdNo='" & sFTOrderProdNo & "' AND FNHSysPartId = " & R!FNHSysPartId & " AND FDSaveDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        '                                _Qry &= vbCrLf & " AND FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' AND FNHSysUnitSectId = " & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
        '                                _Qry &= vbCrLf & " and FNSeq =" & Integer.Parse(_Seq)

        '                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '                                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FDSaveDate, FNHSysUnitSectId, FTColorway, FNHSysPartId, FTSizeBreakDown, FNQuantity , FNSeq"
        '                                    _Qry &= vbCrLf & ")"
        '                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
        '                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
        '                                    _Qry &= vbCrLf & ",'" & sFTOrderProdNo & "'"
        '                                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
        '                                    _Qry &= vbCrLf & "," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString)
        '                                    _Qry &= vbCrLf & ",'" & R!FTColorway.ToString & "'"
        '                                    _Qry &= vbCrLf & "," & R!FNHSysPartId
        '                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
        '                                    _Qry &= vbCrLf & "," & CInt("0" & R(Col.ColumnName.ToString).ToString)
        '                                    _Qry &= vbCrLf & "," & Integer.Parse(_Seq)
        '                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                                        HI.Conn.SQLConn.Tran.Rollback()
        '                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                                        Return False
        '                                    End If
        '                                End If
        '                        End Select
        '                    Next
        '                End If
        '            Next

        '        End If



        '        '_Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
        '        '_Qry &= vbCrLf & "WHERE FTOrderProdNo='" & _Key & "' "
        '        'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
        '    End If
        'Catch ex As Exception
        'End Try
        'Return True
    End Function



    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        Try
            Dim _Qry As String = ""
            Dim _dtprod As DataTable

            _Qry = "SELECT distinct    FTOrderProdNo ,LEN(FTOrderProdNo)  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd AS P With(Nolock)"
            _Qry &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderProdNo) & "'  "
            _Qry &= vbCrLf & "  Order By LEN(FTOrderProdNo),FTOrderProdNo  "

            _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)



            If _dtprod.Rows.Count > 0 Then
                Dim _ProdState As Boolean = False
                For Each R As DataRow In _dtprod.Rows
                    If SaveTableCutting(R!FTOrderProdNo.ToString) Then

                        _ProdState = True
                        'HI.MG.ShowMsg.mInfo("Save Data Complete !!!", 1404210006, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                    Else
                        _ProdState = False
                        ' HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึกข้อมูลโต๊ะตัดได้ !!!", 1404210005, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    End If

                Next

                If _ProdState Then
                    _Qry = "Exec dbo.SP_GET_MUGroupProdToOrderProd_CREATETableOnly @User='" & HI.ST.UserInfo.UserName & "' , @DocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    _Qry = "Exec dbo.[SP_GET_MUGroupProdToOrderProd_CREATETableOnly_AVG_Allowance] @User='" & HI.ST.UserInfo.UserName & "' , @DocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("Save Data Complete !!!", 1404210006, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึกข้อมูลโต๊ะตัดได้ !!!", 1404210005, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

                End If

            Else
                If CreateNewJobProducttion() Then
                    _Qry = "Exec dbo.SP_GET_MUGroupProdToOrderProd @User='" & HI.ST.UserInfo.UserName & "' , @DocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Qry = "Exec dbo.SP_GET_MUGroupProdToOrderProd_CREATETableOnly @User='" & HI.ST.UserInfo.UserName & "' , @DocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    _Qry = "Exec dbo.[SP_GET_MUGroupProdToOrderProd_CREATETableOnly_AVG_Allowance] @User='" & HI.ST.UserInfo.UserName & "' , @DocumentNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("Save Data Complete !!!", 1404210006, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึกข้อมูลโต๊ะตัดได้ !!!", 1404210005, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

                End If

            End If

            _dtprod.Dispose()







            'If CreateNewJobProducttion() Then
            '    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            'Else
            '    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            'End If

        Catch ex As Exception
            _Spls.Close()
        End Try

        Me.Close()
    End Sub

    Private Function SaveTableCutting(_OrderProd As String) As Boolean

        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            'Dim _oDtBD As DataTable
            '_Qry = "exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderProdBreakDown_MU_ProdBD  '" & _OrderProd & "'"
            '_oDtBD = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)
            With DirectCast(Me.ogcpart.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSelect='1'")

                    Dim _MarkId As Integer = 0
                    _Qry = "Select top 1   *  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark where FTMarkCode='" & Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 10) & "'"
                    Dim _odt As DataTable = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)
                    If _odt.Rows.Count > 0 Then
                        _MarkId = Val("0" & _odt.Rows(0).Item("FNHSysMarkId").ToString)


                        If (Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 10), 1).ToUpper = "A") Then
                            ' MsgBox(Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 3), 1).ToUpper)

                            _Qry = " select Top 1  *  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain T "
                            _Qry &= vbCrLf & " WHERE T.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "'  "
                            _Qry &= vbCrLf & " AND T.FNHSysMarkId=" & Val(_MarkId) & "  "
                            If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry).Rows.Count <= 0 Then
                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain"
                                _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FTNote,FNHSysCmpId)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderProd) & "' "
                                _Qry &= vbCrLf & "," & Val(_MarkId) & " "
                                _Qry &= vbCrLf & ",'' "
                                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            End If




                            '  MsgBox(Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 3), 1).ToUpper)
                        Else

                            _Qry = "  Select top 1 *  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub T"
                            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "'  "
                            _Qry &= vbCrLf & " AND FNHSysMarkId= isnull( (select top 1 FNHSysMarkId from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain  where FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "' ) , 0)"
                            _Qry &= vbCrLf & " AND FNHSysSubMarkId=" & Val(_MarkId) & "  "
                            If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry).Rows.Count <= 0 Then
                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub"
                                _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FNHSysSubMarkId,FTNote,FNHSysCmpId)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderProd) & "' "
                                _Qry &= vbCrLf & ",isnull( (select top 1 FNHSysMarkId from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain  where FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "' ) , 0)  "
                                _Qry &= vbCrLf & "," & Val(_MarkId) & " "
                                _Qry &= vbCrLf & ",'' "
                                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            End If


                        End If

                    Else
                        Dim _Id As Integer = 0
                        _Id = HI.SE.RunID.GetRunNoID("TPRODMMark", "FNHSysMarkId", Conn.DB.DataBaseName.DB_MASTER)


                        _Qry = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark"
                        _Qry &= vbCrLf & " ( FNHSysMarkId, FTMarkCode, FTMarkNameTH, FTMarkNameEN, FTNote, FTStateMainMark, FTStateActive )"
                        _Qry &= vbCrLf & "Select " & _Id
                        _Qry &= vbCrLf & ",'" & Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 10) & "'"
                        _Qry &= vbCrLf & ",'" & Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 10) & "'"
                        _Qry &= vbCrLf & ",'" & Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 10) & "'"
                        _Qry &= vbCrLf & ",'','','1'"


                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        If (Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Left(R!FTMarkCode.ToString, Len(R!FTMarkCode.ToString) - 10), 1).ToUpper.ToString = "A") Then

                            _Qry = "  select top 1 * From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain T "
                            _Qry &= vbCrLf & " WHERE T.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "'  "
                            _Qry &= vbCrLf & " AND T.FNHSysMarkId=" & Val(_Id) & "  "
                            If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry).Rows.Count <= 0 Then
                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain"
                                _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FTNote,FNHSysCmpId)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderProd) & "' "
                                _Qry &= vbCrLf & "," & Val(_Id) & " "
                                _Qry &= vbCrLf & ",'' "
                                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                            End If

                        Else

                            _Qry = "  Select top 1  *  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub T"
                            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "'  "
                            _Qry &= vbCrLf & " AND FNHSysMarkId= isnull( (select top 1 FNHSysMarkId from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain  where FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "' ) , 0)"
                            _Qry &= vbCrLf & " AND FNHSysSubMarkId=" & Val(_Id) & "  "
                            If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry).Rows.Count <= 0 Then
                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkSub"
                                _Qry &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime, FTOrderProdNo, FNHSysMarkId,FNHSysSubMarkId,FTNote,FNHSysCmpId)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderProd) & "' "
                                _Qry &= vbCrLf & ",isnull( (select top 1 FNHSysMarkId from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_MarkMain  where FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_OrderProd) & "' ) , 0)  "
                                _Qry &= vbCrLf & "," & Val(_Id) & " "
                                _Qry &= vbCrLf & ",'' "
                                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            End If
                        End If
                    End If

                    '_Qry = " Select TOp 1 FTOrderProdNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut "
                    '_Qry &= vbCrLf & " WHERE FTOrderProdNo='" & _OrderProd & "' "
                    '_Qry &= vbCrLf & " AND  FNHSysMarkId=" & _MarkId & " "
                    '_Qry &= vbCrLf & " AND FNTableNo=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "

                    Dim _layerTotal As Integer = 0
                    Dim _layerPer As Integer = 0
                    Dim _TableGen As Integer = 0
                    Dim _layerTotalUse As Integer = 0
                    Dim _layerUse As Integer = 0


                    _layerTotal = Integer.Parse("0" & R!FNLayerQty.ToString)
                    _layerPer = Integer.Parse("0" & R!FNLayerPerTable.ToString)
                    _TableGen = Integer.Parse("0" & R!FNTotalTable.ToString)


                    For i As Integer = 1 To _TableGen
                        If (_layerPer * i) > _layerTotal Then
                            ' เศษ
                            _layerUse = _layerTotal - _layerTotalUse

                            _layerTotalUse += +_layerUse

                        Else
                            'เต็ม
                            _layerUse = _layerPer


                            _layerTotalUse += +_layerUse
                        End If


                        Dim _TableMaxNo As Integer = 0
                        _TableMaxNo = HI.Conn.SQLConn.GetFieldOnBeginTrans("  Select TOp 1 max(FNTableNo) as FNTableNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut WHERE FTOrderProdNo='" & _OrderProd & "' AND  FNHSysMarkId=" & _MarkId & "       ", Conn.DB.DataBaseName.DB_PROD, "0")
                        _TableMaxNo += +1



                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMUTOrderProd_TableCut "
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysUnitSectId,FTStateRepair,FNHSysCmpId"
                        _Qry &= vbCrLf & ", FNFabricFrontSize, FNMarkYRD,FNMarkINC, FNMarkSpare, FNMarkTotal,FTRef,FTStateBundle,FTStateCutchange,FTStatTableScrap ,FNFabricUseQuantity  ) "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_OrderProd) & "' "
                        _Qry &= vbCrLf & " ," & Val(_MarkId) & " "
                        _Qry &= vbCrLf & " , " & _TableMaxNo
                        _Qry &= vbCrLf & " ,'' "
                        _Qry &= vbCrLf & " ," & 0 & " "
                        _Qry &= vbCrLf & " ,'0' "
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNActuallong.ToString) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNYard.ToString) & ""
                        _Qry &= vbCrLf & "," & Val(R!FNInc.ToString) & ""
                        _Qry &= vbCrLf & "," & Totalent & ""
                        _Qry &= vbCrLf & "," & Val(R!FNYard.ToString) + CDbl(Format(((Val(R!FNInc.ToString) + Totalent) / 36), "0.00")) & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTMarkCode.ToString) & "' "
                        _Qry &= vbCrLf & " ,'' "
                        _Qry &= vbCrLf & " ,'' "
                        _Qry &= vbCrLf & " ,'' "
                        _Qry &= vbCrLf & " , " & _layerUse * (Val(R!FNYard.ToString) + CDbl(Format(((Val(R!FNInc.ToString) + Totalent) / 36), "0.00")))

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False

                        End If


                        For Each Rx As DataRow In ODTRatio.Select("FNHSysMarkId='" & HI.UL.ULF.rpQuoted(R!FTMarkCode.ToString) & "'")

                            For Each Col As DataColumn In ODTRatio.Columns
                                Select Case Col.ColumnName.ToString.ToUpper
                                    Case "FTColorway".ToUpper, "FNHSysMarkId".ToUpper,
                                     "MarkSeq".ToUpper, "FNSeq".ToUpper, "FNHSysMarkId_Hide".ToUpper, "FNActuallong".ToUpper, "FNInc".ToUpper, "FNQuantityUse".ToUpper,
                                      "Total".ToUpper, "FNTotalQty".ToUpper, "FNOrderQty".ToUpper, "FNLayerQty".ToUpper, "FNQuantity".ToUpper, "FNYard".ToUpper, "FNEfficency".ToUpper
                                    Case Else
                                        If IsNumeric(Rx.Item(Col.ColumnName.ToString)) Then

                                            If Integer.Parse(Rx.Item(Col.ColumnName.ToString)) > 0 Then 'AndAlso R!FTColorway.ToString <> ""

                                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODMUTOrderProd_TableCut_Detail "
                                                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity,FNHSysCmpId) "
                                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_OrderProd) & "' "
                                                _Qry &= vbCrLf & " ," & Val(_MarkId) & " "
                                                _Qry &= vbCrLf & " ," & _TableMaxNo & " "
                                                _Qry &= vbCrLf & " ,'' "  '" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "
                                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                                _Qry &= vbCrLf & " ," & _layerUse & " " 'FNLayerQty
                                                _Qry &= vbCrLf & " ," & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) & " "
                                                _Qry &= vbCrLf & " ," & Integer.Parse(Rx.Item(Col.ColumnName.ToString)) * _layerUse & " "
                                                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Return False
                                                End If

                                            End If

                                        End If
                                End Select

                            Next

                        Next




                    Next





                Next

            End With





            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function






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

    Private Sub ocmRowDel_Click(sender As Object, e As EventArgs)
        Try
            'If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบการบันทึกงานตัด ใช่หรือไม่ !!!!!", 1905061707, Me.Text) = False Then Exit Sub
            'With Me.ogvpart
            '    If .FocusedRowHandle < -1 Or .RowCount < 0 Then Exit Sub
            '    Dim _Cmd As String = ""

            '    Dim _PartIds As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString)
            '    Dim _Seq As Integer = Integer.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)

            '    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCutOrderRecord_Detail"
            '    _Cmd &= vbCrLf & "  WHERE FTOrderProdNo = N'" & sFTOrderProdNo & "'"
            '    _Cmd &= vbCrLf & "  AND FDSaveDate = N'" & HI.UL.ULDate.ConvertEnDB(Me.FDSaveDate.Text) & "'"
            '    _Cmd &= vbCrLf & "  AND FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
            '    _Cmd &= vbCrLf & "  AND FNHSysPartId =" & Val(_PartIds) & ""
            '    _Cmd &= vbCrLf & "  AND FNSeq=" & _Seq
            '    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            '    .DeleteRow(.FocusedRowHandle)
            'End With
            'LoadOrderCutItemUnitSect()
        Catch ex As Exception

        End Try
    End Sub

    Private _LayerPerTable As Integer = 0




    Private _StateCheck As Boolean = False
    Private Sub ockTableAuto_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ockTableAuto.EditValueChanging
        If Not _StateCheck Then
            _StateCheck = True
            If e.NewValue = "1" Then
                ockTableManual.Checked = False
                _LayerPerTable = Val("0" & _odtConfig.Rows(0).Item("FNCutAuto").ToString)

            End If
            _StateCheck = False
        End If


    End Sub

    Private Sub ockTableManual_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ockTableManual.EditValueChanging
        If Not _StateCheck Then
            _StateCheck = True
            If e.NewValue = "1" Then
                ockTableAuto.Checked = False
                _LayerPerTable = Val("0" & _odtConfig.Rows(0).Item("FNCutManual").ToString)
            End If
            _StateCheck = False
        End If


    End Sub

    Private Sub RepositoryItemCheckEditFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEditFTSelect.EditValueChanging
        Try
            Try
                With Me.ogvpart
                    If .RowCount < 0 And .FocusedRowHandle < -1 Then Exit Sub
                    If e.NewValue = "1" Then

                        If _LayerPerTable <= 0 Then
                            If ockTableAuto.Checked Then
                                _LayerPerTable = Val("0" & _odtConfig.Rows(0).Item("FNCutAuto").ToString)
                            Else
                                _LayerPerTable = Val("0" & _odtConfig.Rows(0).Item("FNCutManual").ToString)
                            End If
                            If _LayerPerTable <= 0 Then
                                _LayerPerTable = 1
                            End If
                        End If


                        .SetRowCellValue(.FocusedRowHandle, "FNLayerPerTable", _LayerPerTable)
                        Dim _total As Integer = 0
                        _total = Format(Math.Ceiling(Val(Val("0" & .GetRowCellValue(.FocusedRowHandle, "FNLayerQty").ToString) / _LayerPerTable)), "0")

                        .SetRowCellValue(.FocusedRowHandle, "FNTotalTable", _total)

                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FNLayerPerTable", 0)
                        .SetRowCellValue(.FocusedRowHandle, "FNTotalTable", 0)

                    End If

                End With
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private _StateCals As Boolean = False
    Private Sub RepositoryItemCalcEditFNLayerPerTable_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcEditFNLayerPerTable.EditValueChanging
        Try
            With Me.ogvpart
                If .RowCount < 0 And .FocusedRowHandle < -1 Then Exit Sub
                If Not (_StateCals) Then
                    _StateCals = True

                    Dim _layer As Integer = 0 : Dim _tableTotal As Integer = 0
                    _layer = .GetRowCellValue(.FocusedRowHandle, "FNLayerQty")
                    _tableTotal = Math.Ceiling(_layer / e.NewValue)

                    .SetRowCellValue(.FocusedRowHandle, "FNTotalTable", _tableTotal)

                    _StateCals = False

                End If


            End With

        Catch ex As Exception
            _StateCals = False
        End Try
    End Sub




    Private Sub RepositoryItemCalcEditFNTotalTable_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcEditFNTotalTable.EditValueChanging
        Try
            With Me.ogvpart
                If .RowCount < 0 And .FocusedRowHandle < -1 Then Exit Sub
                If Not (_StateCals) Then
                    _StateCals = True

                    Dim _layer As Integer = 0 : Dim _tableTotal As Integer = 0
                    _layer = .GetRowCellValue(.FocusedRowHandle, "FNLayerQty")
                    _tableTotal = Math.Ceiling(_layer / e.NewValue)

                    .SetRowCellValue(.FocusedRowHandle, "FNLayerPerTable", _tableTotal)

                    _StateCals = False

                End If


            End With
        Catch ex As Exception
            _StateCals = False
        End Try
    End Sub
End Class