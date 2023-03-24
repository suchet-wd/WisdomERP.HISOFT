Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Public Class wScanCartonPopUp
    Private Shared _Reason As String
    ' Private Shared _frmReject As wShowReject = Nothing
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)
        Catch ex As Exception
        Finally
        End Try
    End Sub

#Region "Property"
    Private _PDtpercarton As DataTable
    Public Property PDtpercarton As DataTable
        Get
            Return _PDtpercarton
        End Get
        Set(value As DataTable)
            _PDtpercarton = value
        End Set
    End Property

    Private _Poss As Boolean = False
    Public Property Poss As Boolean
        Get
            Return _Poss
        End Get
        Set(value As Boolean)
            _Poss = value
        End Set
    End Property

    Private _QtyCarton As Integer = 0
    Public Property QtyCarton As Integer
        Get
            Return _QtyCarton
        End Get
        Set(value As Integer)
            _QtyCarton = value
        End Set
    End Property

    Private _CartonNo As Integer = 0
    Public Property CartonNo As Integer
        Get
            Return _CartonNo
        End Get
        Set(value As Integer)
            _CartonNo = value
        End Set
    End Property

    Private _PackNo As String = ""
    Public Property PackNo As String
        Get
            Return _PackNo
        End Get
        Set(value As String)
            _PackNo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _SubOrderNo As String = ""
    Public Property SubOrderNo As String
        Get
            Return _SubOrderNo
        End Get
        Set(value As String)
            _SubOrderNo = value
        End Set
    End Property

    Private _BarcodeNo As String = ""
    Public Property BarcodeNo As String
        Get
            Return _BarcodeNo
        End Get
        Set(value As String)
            _BarcodeNo = value
        End Set
    End Property

    Private _Colorway As String = ""
    Public Property Colorway As String
        Get
            Return _Colorway
        End Get
        Set(value As String)
            _Colorway = value
        End Set
    End Property

    Private _UnitSectId As Integer = 0
    Public Property UnitSectId As Integer
        Get
            Return _UnitSectId
        End Get
        Set(value As Integer)
            _UnitSectId = value
        End Set
    End Property
#End Region

    Private Sub wScanCartonPopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call LoadrderPackBreakDownCarton(_PDtpercarton)
            Call _LoadDataScan()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SBtnExit_Click(sender As Object, e As EventArgs)
        Try
            _Poss = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SBtnOK_Click(sender As Object, e As EventArgs) Handles SBtnOK.Click
        Try
            _Poss = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadrderPackBreakDownCarton(oDt As DataTable)
        Dim _dt As DataTable = oDt
        Dim _dtpack As DataTable = oDt
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        With _dt
            If .Columns.IndexOf("FTNikePOLineItem") < 0 Then
                .Columns.Add("FTNikePOLineItem", GetType(String))
            End If
        End With
        _dt.BeginInit()
        For Each R As DataRow In _dt.Rows
            R!FTNikePOLineItem = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        Next
        _dt.EndInit()

        With Me.ogvppercarton
            .OptionsView.ShowAutoFilterRow = False
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                        .Columns(I).AppearanceCell.BackColor = Color.White
                        .Columns(I).AppearanceCell.ForeColor = Color.Black
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper

                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                If Col.ColumnName.ToString.ToUpper = "FNHSysStyleId".ToUpper Then
                                    .Visible = False
                                Else

                                    .Visible = True
                                End If

                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                            End With
                            .Columns.Add(ColG)
                            With .Columns(Col.ColumnName.ToString)
                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                .AppearanceHeader.BackColor = Color.LightGreen
                                .AppearanceHeader.BackColor2 = Color.White

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With
                            End With
                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select
                Next
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next
            End If
            .OptionsView.ShowAutoFilterRow = False
        End With
        Me.ogcppercarton.DataSource = _dt.Copy
        _dt.Dispose()
        _dtpack.Dispose()
    End Sub

    Private Function GetFTNikePOLineItem(_SubOrderNo As String, _Colorway As String) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 isnull(FTNikePOLineItem,'') AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub _LoadDataScan()
        Try
            Dim _colcount As Integer
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyScan As Integer = 0

            _oDt = GetOnhandScan()

            With Me.ogvScan
                .OptionsView.ShowAutoFilterRow = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next
                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With

                                .Columns.Add(ColG)

                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    .AppearanceHeader.BackColor = Color.Salmon
                                    .AppearanceHeader.BackColor2 = Color.LightSalmon

                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With
                                End With
                                .Columns(Col.ColumnName.ToString).Width = 45
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                        End Select
                    Next
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                    Next
                End If
                .OptionsView.ShowAutoFilterRow = False
            End With
            Me.ogcScan.DataSource = _oDt.Copy
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetOnhandScan() As DataTable
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT   Isnull(T.FNQuantity ,X.FNQuantity)  AS FNQuantity  ,isnull(T.FTColorway,X.FTColorway) AS FTColorway  , Isnull(T.FTSizeBreakDown , X.FTSizeBreakDown) AS FTSizeBreakDown "
            _Cmd &= vbCrLf & ",Isnull(T.FTOrderNo , X.FTOrderNo) AS FTOrderNo ,Isnull(T.FTNikePOLineItem,X.FTNikePOLineItem) AS FTNikePOLineItem  , Isnull(T.FTSubOrderNo,X.FTSubOrderNo) AS FTSubOrderNo"
            _Cmd &= vbCrLf & "INTO #Tmp"
            _Cmd &= vbCrLf & "From ( SELECT       sum(B.FNQuantity) AS FNQuantity  ,D.FTColorway, D.FTSizeBreakDown ,D.FTOrderNo ,W.FTNikePOLineItem  ,W.FTSubOrderNo"
            _Cmd &= vbCrLf & "FROM     (Select TT.FTBarcodeNo , TT.FNQuantity , O.FNHSysUnitSectId "
            _Cmd &= vbCrLf & "From ("
            _Cmd &= vbCrLf & "Select  sum(FNQuantity) AS FNQuantity , FTBarcodeNo"
            _Cmd &= vbCrLf & "From (SELECT  sum(O.FNQuantity) AS FNQuantity ,  O.FTBarcodeNo "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline AS O WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS A WITH (NOLOCK) ON O.FTBarcodeNo = A.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS P WITH (NOLOCK) ON A.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & _OrderNo & "'"
            _Cmd &= vbCrLf & " and  A.FTColorway in ('" & Replace(_Colorway, ",", "','") & "')"
            _Cmd &= vbCrLf & "Group by  O.FTBarcodeNo  "
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "Select  -sum(FNScanQuantity) AS FNQuantity , FTBarcodeNo"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where  FTOrderNo ='" & _OrderNo & "'"
            _Cmd &= vbCrLf & " and  FTColorway  in ('" & Replace(_Colorway, ",", "','") & "')"
            _Cmd &= vbCrLf & "group by FTBarcodeNo ) AS T "
            _Cmd &= vbCrLf & "group by FTBarcodeNo ) AS TT INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline AS O WITH(NOLOCK) ON TT.FTBarcodeNo = O.FTBarcodeNo"
            _Cmd &= vbCrLf & " Where TT.FNQuantity > 0"
            _Cmd &= vbCrLf & "group by TT.FTBarcodeNo , TT.FNQuantity , O.FNHSysUnitSectId) AS B LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(SELECT        H.FTBarcodeBundleNo,  D.FTColorway, D.FTSizeBreakDown, P.FTOrderNo"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle_Detail AS D WITH (NOLOCK) ON H.FTBarcodeBundleNo = D.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS P WITH (NOLOCK) ON H.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & _OrderNo & "'"
            _Cmd &= vbCrLf & " and D.FTColorway  in ('" & Replace(_Colorway, ",", "','") & "')"
            _Cmd &= vbCrLf & " Group by    H.FTBarcodeBundleNo, D.FTColorway, D.FTSizeBreakDown, P.FTOrderNo"
            _Cmd &= vbCrLf & ") AS D ON B.FTBarcodeNo = D.FTBarcodeBundleNo"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS W WITH(NOLOCK) ON D.FTOrderNo = W.FTOrderNo and D.FTColorway = W.FTColorway and D.FTSizeBreakDown = W.FTSizeBreakDown"
            _Cmd &= vbCrLf & "Where  D.FTOrderNo ='" & _OrderNo & "'"
            _Cmd &= vbCrLf & " and D.FTColorway  in ('" & Replace(_Colorway, ",", "','") & "')"
            _Cmd &= vbCrLf & "group by D.FTColorway, D.FTSizeBreakDown , D.FTOrderNo ,W.FTNikePOLineItem ,W.FTSubOrderNo ) AS T  "

            _Cmd &= vbCrLf & "FULL OUTER JOIN (SELECT       0 as FNQuantity, D.FTColorway , D.FTSizeBreakDown , D.FTOrderNo, B.FTNikePOLineItem, D.FTSubOrderNo "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS D WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH (NOLOCK) ON D.FTOrderNo = B.FTOrderNo AND D.FTSubOrderNo = B.FTSubOrderNo AND D.FTColorway = B.FTColorway AND "
            _Cmd &= vbCrLf & "     D.FTSizeBreakDown = B.FTSizeBreakDown"
            _Cmd &= vbCrLf & "Where   D.FTPackNo = '" & _PackNo & "'"
            _Cmd &= vbCrLf & "and  D.FNCartonNo =" & _CartonNo
            _Cmd &= vbCrLf & ") AS X ON T.FTOrderNo = X.FTOrderNo and T.FTSubOrderNo = X.FTSubOrderNo and T.FTColorway = X.FTColorway and T.FTSizeBreakDown  = X.FTSizeBreakDown"
            _Cmd &= vbCrLf & "and T.FTNikePOLineItem = X.FTNikePOLineItem"

            _Cmd &= vbCrLf & "DECLARE @cols AS NVARCHAR(MAX),@sumcols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX)"
            _Cmd &= vbCrLf & "select @cols = STUFF((SELECT ',' + QUOTENAME(FTSizeBreakDown) "
            _Cmd &= vbCrLf & "  from #Tmp AS T LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS S WITH(NOLOCK) ON T.FTSizeBreakDown = S.FTMatSizeCode"
            _Cmd &= vbCrLf & "  group by FTSizeBreakDown ,S.FNMatSizeSeq order by S.FNMatSizeSeq    "
            _Cmd &= vbCrLf & "  FOR XML PATH(''), TYPE"
            _Cmd &= vbCrLf & "  ).value('.', 'NVARCHAR(MAX)') "
            _Cmd &= vbCrLf & " ,1,1,'')"
            _Cmd &= vbCrLf & "select @sumcols = STUFF((SELECT '+  Isnull(' + QUOTENAME(FTSizeBreakDown) +',0) ' "
            _Cmd &= vbCrLf & "  from #Tmp    group by FTSizeBreakDown  "
            _Cmd &= vbCrLf & " FOR XML PATH(''), TYPE"
            _Cmd &= vbCrLf & "   ).value('.', 'NVARCHAR(MAX)')"
            _Cmd &= vbCrLf & " ,1,1,'')"
            _Cmd &= vbCrLf & "Set @query = 'Select FTSubOrderNo, FTColorway,  FTOrderNo ,FTNikePOLineItem ,'+  @cols +',('+@sumcols+') AS Total"
            _Cmd &= vbCrLf & " From #Tmp"
            _Cmd &= vbCrLf & "PIVOT(Sum(FNQuantity)"
            _Cmd &= vbCrLf & "FOR FTSizeBreakDown IN (' + @cols + ')) AS PVTTable'"
            _Cmd &= vbCrLf & "EXEC sp_executesql @query"
            _Cmd &= vbCrLf & "drop table #Tmp"

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub ogvScan_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvScan.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.White
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvppercarton_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvppercarton.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                e.Appearance.BackColor = Color.Salmon
                e.Appearance.BackColor2 = Color.SeaShell
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class