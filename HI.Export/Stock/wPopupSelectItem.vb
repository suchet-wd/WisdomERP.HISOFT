Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wPopupSelectItem

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()
        Call LoadLayout(Me, Me)
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            '.OptionsSelection.MultiSelect = True
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .ExpandAllGroups()
            .RefreshData()


        End With




    End Sub
#End Region
    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property

    Private _WhId As Integer = 0
    Public Property WhId As Integer
        Get
            Return _WhId
        End Get
        Set(value As Integer)
            _WhId = value
        End Set
    End Property

    Public _CmpId As Integer = 0
    Public Property CmpId As Integer
        Get
            Return _CmpId
        End Get
        Set(value As Integer)
            _CmpId = value
        End Set
    End Property

    Private _StateSetSelectAll As Boolean = True
    Private Sub oChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oChkSelectAll.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectAll = False
            '    Me.oChkSelectAll.Checked = False

            Dim _State As String = "0"
            If Me.oChkSelectAll.Checked Then
                _State = "1"
            End If
            Dim _oDt As New DataTable
            Select Case Me.otb.SelectedTabPage.Name.ToString.ToUpper
                Case "otabporef".ToUpper
                    With ogcsum
                        If Not (.DataSource Is Nothing) And ogvsum.RowCount > 0 Then
                            With ogvsum
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With
                            Dim _Total As Integer = 0
                            Dim _TotalCBM As Double = 0
                            With DirectCast(Me.ogcsum.DataSource, DataTable)
                                .AcceptChanges()
                                For Each R As DataRow In .Select("FTSelect='1'")
                                    _Total += +Val(R!FNTotalCarton.ToString)
                                    _TotalCBM += +Val(R!TotalCbm.ToString)
                                Next
                                .AcceptChanges()
                                _oDt = .Select("FTSelect='" & _State & "'").CopyToDataTable
                                Me.FNTotalCarton.Value = _Total
                                Me.FNTotalCBM.Value = _TotalCBM


                            End With
                        End If
                    End With
                    With ogc
                        If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                            'With DirectCast(Me.ogc.DataSource, DataTable)
                            '    .AcceptChanges()

                            '    For Each Rx As DataRow In _oDt.Rows
                            '        For Each R As DataRow In .Select("FTCustomerPO='" & Rx!FTCustomerPO.ToString & "'  and FTPOLine='" & Rx!FTPOLine.ToString & "'")
                            '            R!FTSelect = _State
                            '        Next
                            '    Next

                            '    .AcceptChanges()

                            'End With

                        End If
                    End With

                Case "otpdetail".ToUpper


                    With ogc
                        If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                            With ogv
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            Dim _Total As Integer = 0
                            Dim _TotalCBM As Double = 0
                            With DirectCast(Me.ogc.DataSource, DataTable)
                                .AcceptChanges()
                                For Each R As DataRow In .Select("FTSelect='1'")
                                    _Total += +Val(R!FNTotalCarton.ToString)
                                    _TotalCBM += +Val(R!TotalCbm.ToString)
                                Next
                                _oDt = .Select("FTSelect='1'").CopyToDataTable
                                Me.FNTotalCarton.Value = _Total
                                Me.FNTotalCBM.Value = _TotalCBM


                            End With

                        End If
                    End With

                    With ogcsum
                        If Not (.DataSource Is Nothing) And ogvsum.RowCount > 0 Then
                            With DirectCast(Me.ogcsum.DataSource, DataTable)
                                .AcceptChanges()
                                For Each Rx As DataRow In _oDt.Rows
                                    For Each R As DataRow In .Select("FTCustomerPO='" & Rx!FTCustomerPO.ToString & "'  and FTPOLine='" & Rx!FTPOLine.ToString & "'")
                                        R!FTSelect = _State
                                    Next
                                Next
                                .AcceptChanges()
                            End With
                        End If
                    End With
            End Select





        Catch ex As Exception
        End Try
        _StateSetSelectAll = True
    End Sub


#Region "Procedure"
    Private _ds As New DataSet
    Public Sub LoadData(ByVal _oDt As DataTable)
        Dim _Qry As String = ""
        Dim _Cmd As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            If (_WhId <> 1567590012) Then

                _Qry = " Select  FTSelect , FTBarCodeEAN13 ,FTCustomerPO , FTPOLine ,FTPackNo , FNCartonNo ,    FNQuantity "
                _Qry &= vbCrLf & " ,FTCartonCode , FDShipDate ,TotalCbm , FTStyleCode , FTStyleName "

                _Qry &= vbCrLf & "  , HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(FTPackNo,FNCartonNo) AS FTCartonInfo   From ( SELECT distinct '0' as   FTSelect , Z.FTBarCodeEAN13 ,A.FTCustomerPO , B.FTPOLine ,A.FTPackNo,C.FNCartonNo"
                '_Qry &= vbCrLf & " ,HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(A.FTPackNo,C.FNCartonNo) AS FTCartonInfo "
                _Qry &= vbCrLf & " ,    sum(B.FNQuantity)over(partition by  Z.FTBarCodeEAN13)    as FNQuantity,  T.FTCartonCode,convert(nvarchar(10) ,convert(date, max( ZT.FDShipDate	)over(partition by A.FTPackNo,C.FNCartonNo) 	),103) as FDShipDate	 "
                _Qry &= vbCrLf & " ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , A.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName  "
                _Qry &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack AS A WITH (NOLOCK) LEFT OUTER JOIN "
                _Qry &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKCarton AS C WITH(NOLOCK) ON A.FTPackNo = C.FTPackNo and B.FNCartonNo = C.FNCartonNo "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON B.FNHSysCartonId = T.FNHSysCartonId		 "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Barcode  AS Z   with(nolock) ON   A.FTPackNo = Z.FTPackNo and B.FNCartonNo = Z.FNCartonNo	 "
                _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON A.FNHSysStyleId = S.FNHSysStyleId "
                _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination AS ZT WITH(NOLOCK) ON A.FTCustomerPO = ZT.FTPOref and  B.FTPOLine =  ZT.FTNikePOLineItem	"
                _Qry &= vbCrLf & "   and B.FTSizeBreakDown = ZT.FTSizeBreakDown and B.FTColorway = ZT.FTColorway and B.FTOrderNo = ZT.FTOrderNo and B.FTSubOrderNo = ZT.FTSubOrderNo "
                _Qry &= vbCrLf & " where  ZT.FNGrandQuantity > 0 and  isnull( B.FTPOLine,'') <> '' and isnull( A.FTCustomerPO,'') <> '' and  A.FNHSysCmpId=" & Val(CmpId)
                _Qry &= vbCrLf & " and isnull(C.FTStateOut,'0') = '0' and Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-7, GETDATE()),111)" ' and ISNULL(C.FTState ,'0')  ='1'
                _Qry &= vbCrLf & " and isnull (Z.FTBarCodeEAN13 ,'' )  <> '' "
                _Qry &= vbCrLf & " and   Z.FTBarCodeEAN13  not in (   Select  FTBarcodeNo  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D with(nolock) where FTTruckSheetNo <> '" & _DocNo & "' ) "
                _Qry &= vbCrLf & " ) AS T "


                '_Cmd = " Select  '0' as   FTSelect ,FTCustomerPO , FTPOLine, FDShipDate , FTStyleCode , FTStyleName , sum(TotalCbm) as TotalCbm  ,sum(FNQuantity) as FNQuantity , count(FTBarCodeEAN13) as FNTotalCarton " '

                '_Cmd &= vbCrLf & "   ,FTCartonInfo   ,isnull(( Select sum(FNPackCount) as   FNPackCount    From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan_D as D  with(nolock) where D.FTPORef  = T.FTCustomerPO and convert(int ,  D.FTPOLineNo) =T.FTPOLine ),0) as FNPackCount "
                ''_Cmd &= vbCrLf & " ,isnull(( Select (max(FNTo) -  MIN(FNFrom)) + 1 as   FNPackCount         From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan_D as D  with(nolock) where D.FTPORef  = T.FTCustomerPO and convert(int ,  D.FTPOLineNo) =T.FTPOLine ),0) as FNTotalCarton "
                '_Cmd &= vbCrLf & "From ( SELECT distinct  '0' as   FTSelect , Z.FTBarCodeEAN13 ,A.FTCustomerPO , B.FTPOLine ,A.FTPackNo,C.FNCartonNo "
                '_Cmd &= vbCrLf & " ,   sum(B.FNQuantity)over(partition by  Z.FTBarCodeEAN13)  as FNQuantity, T.FTCartonCode,convert(nvarchar(10) ,convert(date, max( ZT.FDShipDate	)over(partition by A.FTPackNo,C.FNCartonNo) 	),103) as FDShipDate	 "
                '_Cmd &= vbCrLf & " ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , A.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName  "
                '_Cmd &= vbCrLf & " ,HITECH_FG.dbo.fn_Get_Carton_Color(A.FTPackNo,Z.FNCartonNo) AS FTCartonInfo "
                '_Cmd &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack AS A WITH (NOLOCK) LEFT OUTER JOIN "
                '_Cmd &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo "
                '_Cmd &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKCarton AS C WITH(NOLOCK) ON A.FTPackNo = C.FTPackNo and B.FNCartonNo = C.FNCartonNo "
                '_Cmd &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON B.FNHSysCartonId = T.FNHSysCartonId		 "
                '_Cmd &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Barcode  AS Z   with(nolock) ON   A.FTPackNo = Z.FTPackNo and B.FNCartonNo = Z.FNCartonNo	 "
                '_Cmd &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON A.FNHSysStyleId = S.FNHSysStyleId "
                ''_Cmd &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination AS ZT WITH(NOLOCK) ON A.FTCustomerPO = ZT.FTPOref and  B.FTPOLine =  ZT.FTNikePOLineItem	"
                '_Cmd &= vbCrLf & "  LEFT OUTER JOIN  (Select  Distinct FDShipDate ,FTPOref , FTNikePOLineItem From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination where   FNGrandQuantity > 0     ) AS ZT   ON A.FTCustomerPO = ZT.FTPOref and  B.FTPOLine =  ZT.FTNikePOLineItem	"

                '_Cmd &= vbCrLf & " where isnull( B.FTPOLine,'') <> '' and isnull( A.FTCustomerPO,'') <> '' and  A.FNHSysCmpId=" & Val(CmpId)
                '_Cmd &= vbCrLf & " and isnull(C.FTStateOut,'0') = '0' and  Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-7, GETDATE()),111) "
                '_Cmd &= vbCrLf & " and isnull (Z.FTBarCodeEAN13 ,'' )  <> '' "
                '_Cmd &= vbCrLf & " and   Z.FTBarCodeEAN13  not in (   Select  FTBarcodeNo  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D with(nolock) where FTTruckSheetNo <> '" & _DocNo & "' ) "
                ''_Cmd &= vbCrLf & "  where A.FTTruckSheetNo <> '" & _DocNo & "'"
                '_Cmd &= vbCrLf & " ) AS T "
                '_Cmd &= vbCrLf & " group by FTCustomerPO , FTPOLine, FDShipDate , FTStyleCode , FTStyleName ,FTCartonInfo"
                '_Cmd &= vbCrLf & "  order by FTCustomerPO asc"

                _Cmd = "Select  *  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo. GET_DataForTrucksheet (" & Val(CmpId) & ", '" & _DocNo & "') "

            Else
                _Qry = "  SELECT distinct  '0' as   FTSelect  ,convert(nvarchar(10) ,convert(date, max( ZT .FDShipDate	)over(partition by Z.FTPackNo,Z.FNCartonNo) 	),103) as FDShipDate   , D.FTBarcodeNo as FTBarCodeEAN13 ,Z.FTCustomerPO , Z.FTPOLine  "

                _Qry &= vbCrLf & "    ,HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(Z.FTPackNo,Z.FNCartonNo) AS FTCartonInfo  "
                _Qry &= vbCrLf & "  ,   sum(Z.FNQuantity)over(partition by D.FTBarcodeNo)  as FNQuantity, T.FTCartonCode	 "
                _Qry &= vbCrLf & "  ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , D.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName   "
                _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheetApproved AS A with(nolock) "
                _Qry &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheetApproved_D AS D WITH(NOLOCK) ON A.FTTruckSheetNo = D.FTTruckSheetNo"
                _Qry &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBarcodeScanFG  AS Z   with(nolock) ON     D.FTBarcodeNo = Z.FTBarCodeCarton"
                _Qry &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON D.FNHSysStyleId = S.FNHSysStyleId  "
                '_Qry &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo. TPACKOrderPack_Carton_Barcode AS Y with(nolock) ON D.FTBarcodeNo = Y.FTBarCodeEAN13"
                _Qry &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS PD With(nolock) ON Z.FTPackNo = PD.FTPackNo and Z.FNCartonNo = PD.FNCartonNo"
                _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON PD.FNHSysCartonId = T.FNHSysCartonId	"
                _Qry &= vbCrLf & "  LEFT OUTER JOIN    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination  AS ZT   ON Z.FTCustomerPO = ZT.FTPOref and  Z.FTPOLine =  ZT.FTNikePOLineItem	"
                _Qry &= vbCrLf & " and PD.FTSizeBreakDown = ZT.FTSizeBreakDown and PD.FTColorway = ZT.FTColorway --and Z.FTOrderNo = ZT.FTOrderNo  "
                _Qry &= vbCrLf & " where   ZT.FNGrandQuantity > 0  Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-30, GETDATE()),111)  and  D.FTBarcodeNo not in ( SELECT   isnull(D.FTBarcodeNo ,'') " 'and  A.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
                _Qry &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet AS A WITH(NOLOCK) "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D AS D with(nolock) ON A.FTTruckSheetNo = D.FTTruckSheetNo and A.FNHSysWHFGId =" & _WhId
                _Qry &= vbCrLf & "  where A.FTTruckSheetNo <> '" & _DocNo & "'"
                _Qry &= vbCrLf & "  ) and Z.FNHSysWHFGId =" & _WhId


                _Cmd = "Select  * From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo. GET_TruckSheetToWH_new ( '" & _DocNo & "') Order by FTCustomerPO asc "

                '_Cmd = " Select FTSelect , FDShipDate ,FTCustomerPO ,FTPOLine , FTStyleCode , FTStyleName ,sum(TotalCbm) as TotalCbm , COUNT(FTBarCodeEAN13) AS FNTotalCarton ,sum(FNQuantity) as FNQuantity"
                '_Cmd &= vbCrLf & " ,isnull(( Select sum(FNPackCount) as   FNPackCount         From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan_D as D  with(nolock) where D.FTPORef  = T.FTCustomerPO and convert(int ,  D.FTPOLineNo) =T.FTPOLine ),0) as FNPackCount "
                '_Cmd &= vbCrLf & " From ( SELECT  distinct  '0' as   FTSelect ,convert(nvarchar(10) ,convert(date, max( ZT.FDShipDate	)over(partition by Z.FTPackNo,Z.FNCartonNo) 	),103) as FDShipDate , D.FTBarcodeNo as FTBarCodeEAN13 ,Z.FTCustomerPO , Z.FTPOLine "
                ''_Cmd &= vbCrLf & "   ,HITECH_FG.dbo.fn_Get_Carton_Color(Z.FTPackNo,Z.FNCartonNo) AS FTCartonInfo "
                '_Cmd &= vbCrLf & "  ,  sum(Z.FNQuantity)over(partition by D.FTBarcodeNo)  as FNQuantity, T.FTCartonCode	 "
                '_Cmd &= vbCrLf & "  ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , D.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName   "
                '_Cmd &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheetApproved AS A with(nolock) "
                '_Cmd &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheetApproved_D AS D WITH(NOLOCK) ON A.FTTruckSheetNo = D.FTTruckSheetNo"
                '_Cmd &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBarcodeScanFG  AS Z   with(nolock) ON     D.FTBarcodeNo = Z.FTBarCodeCarton"
                '_Cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON D.FNHSysStyleId = S.FNHSysStyleId  "
                ''_Cmd &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo. TPACKOrderPack_Carton_Barcode AS Y with(nolock) ON D.FTBarcodeNo = Y.FTBarCodeEAN13"
                '_Cmd &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS PD With(nolock) ON Z.FTPackNo = PD.FTPackNo and Z.FNCartonNo = PD.FNCartonNo"
                '_Cmd &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON PD.FNHSysCartonId = T.FNHSysCartonId	"
                ''_Cmd &= vbCrLf & "  LEFT OUTER JOIN   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination   AS ZT   ON Z.FTCustomerPO = ZT.FTPOref and  Z.FTPOLine =  ZT.FTNikePOLineItem	"
                ''_Cmd &= vbCrLf & "  and z.FTColorway = ZT.FTColorway  " ' z.FTSizeBreakDown = ZT.FTSizeBreakDown and

                '_Cmd &= vbCrLf & " outer apply ("
                ''_Cmd &= vbCrLf & "Select top  1  FDShipDate  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.getV_OrderSub_BreakDown_ShipDestination(Z.FTCustomerPO , Z.FTPOLine)   "
                '_Cmd &= vbCrLf & " SELECT  top  1    aa.FDShipDate  "
                '_Cmd &= vbCrLf & "  FROM      " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan AS aa inner JOIN "
                '_Cmd &= vbCrLf & "     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan_D AS bb ON aa.FTPORef = bb.FTPORef AND aa.FTPckPlanNo = bb.FTPckPlanNo and aa.FTPORefNo = bb.FTPORefNo "
                '_Cmd &= vbCrLf & "where aa.FTPORef = Z.FTCustomerPO"
                '_Cmd &= vbCrLf & "and convert(varchar(10) ,convert(int, bb.FTPOLineNo)) = z.FTPOLine "
                '_Cmd &= vbCrLf & ") zt "


                '_Cmd &= vbCrLf & " where   Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-7, GETDATE()),111)  and   D.FTBarcodeNo not in ( SELECT   isnull(D.FTBarcodeNo ,'') " 'and  A.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
                '_Cmd &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet AS A WITH(NOLOCK) "
                '_Cmd &= vbCrLf & "   LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D AS D with(nolock) ON A.FTTruckSheetNo = D.FTTruckSheetNo and A.FNHSysWHFGId =" & _WhId
                '_Cmd &= vbCrLf & "  where A.FTTruckSheetNo <> '" & _DocNo & "'"
                '_Cmd &= vbCrLf & "  ) and Z.FNHSysWHFGId =" & _WhId
                '_Cmd &= vbCrLf & ") AS T "
                '_Cmd &= vbCrLf & "   group by FTSelect , FDShipDate ,FTCustomerPO ,FTPOLine , FTStyleCode , FTStyleName"
                '_Cmd &= vbCrLf & "  order by FTCustomerPO asc "

            End If






            '_Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.SP_GET_PREFG " & Val(HI.ST.SysInfo.CmpID)
            'dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
            'pDt = dt
            '_Qry = "    Select  FTBarcodeNo  as FTBarCodeEAN13 FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D with(nolock) where FTTruckSheetNo='" & DocNo & "'   "
            Dim _oWDb As DataTable
            '_oWDb = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
            'If _oWDb.Rows.Count > 0 Then
            '    _oDt = _oWDb.Copy
            'End If

            'For Each X As DataRow In _oDt.Rows
            '    For Each R As DataRow In dt.Select("FTBarCodeEAN13='" & X!FTBarCodeEAN13.ToString & "'")
            '        R!FTSelect = "1"
            '    Next
            'Next
            'Me.ogc.DataSource = dt.Copy




            dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            _Qry = "    Select  FTPORef as FTCustomerPO , FTNikePOLineItem as FTPOLine FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_DocRef with(nolock) where FTTruckSheetNo='" & DocNo & "'   "
            _oWDb = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
            If _oWDb.Rows.Count > 0 Then
                _oDt = _oWDb.Copy
            End If

            Dim _Total As Integer = 0
            Dim _TotalCBM As Double = 0

            For Each X As DataRow In _oDt.Rows
                For Each R As DataRow In dt.Select("FTCustomerPO='" & X!FTCustomerPO.ToString & "' and FTPOLine='" & X!FTPOLine.ToString & "'")
                    If R!FTSelect = "1" Then Continue For
                    R!FTSelect = "1"
                    _Total += +Val(R!FNTotalCarton)
                    _TotalCBM += +Val(R!TotalCbm.ToString)
                Next
                'With DirectCast(Me.ogc.DataSource, DataTable)
                '    .AcceptChanges()
                '    For Each R As DataRow In .Select("FTCustomerPO='" & X!FTPORef.ToString & "' and FTPOLine='" & X!FTNikePOLineItem.ToString & "'")
                '        R!FTSelect = "1"
                '        '_Total += +1
                '        '_TotalCBM += +Val(R!TotalCbm.ToString)
                '    Next
                '    .AcceptChanges()
                '    'dt = .Copy
                'End With

            Next
            'Me.ogc.DataSource = dt.Select("FTSelect='1'").CopyToDataTable
            Me.FNTotalCarton.Value = _Total
            Me.FNTotalCBM.Value = _TotalCBM
            'Me.ogc.DataSource = dt.Copy

            Me.ogcsum.DataSource = dt.Copy

            '  Call LoadDataHistory()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub


    Private Function SaveGacDate() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")

                _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TMERTOrderGACDateCfm"
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc ,FTNikePOLineItem)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & ",ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & ".dbo.TMERTOrderGACDateCfm"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                _Qry &= vbCrLf & "),0) + 1 "
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginal.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If


                '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " and  FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                '_Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan"
                '_Qry &= vbCrLf & "SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub"
                _Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



                _Qry = "UPDATE A  set A.FDShipDate ='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan AS A"
                _Qry &= vbCrLf & "LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTPackPlan_D AS B WITH(NOLOCK) ON A.FTPckPlanNo = B.FTPckPlanNo and A.FTPORef = B.FTPORef and A.FTPORefNo = A.FTPORefNo"
                _Qry &= vbCrLf & " where A.FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Qry &= vbCrLf & "and convert(nvarchar(5) ,convert(int,  B.FTPOLineNo)) ='" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    'Return False
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
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


    Private Function UpdateCfmDate() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Saving....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderGACDate"
                _Qry &= vbCrLf & " SET FDCfmShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",FDORShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                _Qry &= vbCrLf & ",FTReasonDesc='" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"
                _Qry &= vbCrLf & "where  FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & "and FNSeq = ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderGACDate"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                _Qry &= vbCrLf & "),0)  "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderGACDate"
                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FDShipDate, FDShipDateTo,FDOShipDate,FDOShipDateTo,FDCfmShipDate,FDORShipDate,FTReasonDesc)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & ",ISNULL(("
                    _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                    _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderGACDate"
                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ORDER BY FNSeq DESC "
                    _Qry &= vbCrLf & "),0) + 1 "
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",''"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDCfmShipDate.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDORShipDate.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTReasonDesc.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If


                End If

                '_Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub"
                '_Qry &= vbCrLf & " SET FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateTo.ToString) & "'"
                '_Qry &= vbCrLf & " ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDateOrginalTo.ToString) & "'"

                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND   FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '    HI.Conn.SQLConn.Tran.Rollback()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    _Spls.Close()
                '    Return False
                'End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()
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


    Private Sub SendMailToProductin()
        Dim _Spls As New HI.TL.SplashScreen("Sending Mail....Please Wait.")
        Dim _dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy()
        End With
        Dim _Qry As String = ""
        Dim dtcmp As New DataTable
        Dim dtmailto As DataTable
        dtcmp.Columns.Add("FNHSysCmpId", GetType(Integer))
        dtcmp.Columns.Add("FTCmpCode", GetType(String))
        Try
            For Each R As DataRow In _dt.Select("FTStateChange='1' OR FTStateChangeO='1'")
                If dtcmp.Select("").Length <= 0 Then

                    dtcmp.Rows.Add(Integer.Parse(Val(R!FNHSysCmpId.ToString)), R!FTCmpCode.ToString)

                End If
            Next

            For Each Rcmp As DataRow In dtcmp.Rows

                _Qry = " SELECT DISTINCT MM.FTUserName "
                _Qry &= Environment.NewLine & "FROM (SELECT A.FTUserName, A.FNHSysPermissionID"
                _Qry &= Environment.NewLine & "      FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEUserLoginPermission AS A (NOLOCK)"
                _Qry &= Environment.NewLine & "      WHERE A.FNHSysPermissionID > 0"
                _Qry &= Environment.NewLine & "            AND EXISTS (SELECT 'T'"
                _Qry &= Environment.NewLine & "                        FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEPermissionCmp AS LL (NOLOCK) INNER JOIN " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp AS NN (NOLOCK) ON LL.FNHSysCmpId = NN.FNHSysCmpId"
                _Qry &= Environment.NewLine & "                        WHERE LL.FNHSysCmpId = " & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " "
                _Qry &= Environment.NewLine & "  			                 AND A.FNHSysPermissionID = LL.FNHSysPermissionID"
                _Qry &= Environment.NewLine & " 		               )"
                _Qry &= Environment.NewLine & "      ) AS MM INNER JOIN " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEUserLogin AS KK (NOLOCK) ON MM.FTUserName = KK.FTUserName"
                _Qry &= Environment.NewLine & "              LEFT JOIN " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMTeamGrp AS PP (NOLOCK) ON KK.FNHSysTeamGrpId = PP.FNHSysTeamGrpId"
                _Qry &= Environment.NewLine & "WHERE KK.FTStateActive = N'1'  AND (PP. FTStatePurchase='1' OR PP.FTStateProd='1' OR PP.FTStateQA='1' OR PP.FTStateQAFinal='1') "

                dtmailto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

                If dtmailto.Rows.Count > 0 Then
                    Dim tmpsubject As String = ""
                    Dim tmpmessage As String = ""
                    Dim _UserMailTo As String = ""

                    tmpsubject = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString
                    tmpmessage = "Update GAC Date For Factory " & Rcmp!FTCmpCode.ToString

                    For Each R As DataRow In _dt.Select(" ( FTStateChange='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & " ) OR (FTStateChangeO='1' AND FNHSysCmpId=" & Integer.Parse(Val(Rcmp!FNHSysCmpId.ToString)) & "  )  ")

                        tmpmessage &= vbCrLf & "Factory Order No : " & R!FTOrderNo.ToString & " Sub Order No : " & R!FTSubOrderNo.ToString
                        If R!FTStateChange.ToString = "1" Then
                            tmpmessage &= vbCrLf & "       Change Shipment From " & HI.UL.ULDate.ConvertEN(R!FDShipDate.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateTo.ToString)
                        End If

                        If R!FTStateChangeO.ToString = "1" Then
                            tmpmessage &= vbCrLf & "       Change O GAC Date From " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginal.ToString) & " To " & HI.UL.ULDate.ConvertEN(R!FDShipDateOrginalTo.ToString)
                        End If

                    Next

                    For Each Rm As DataRow In dtmailto.Rows

                        _UserMailTo = Rm!FTUserName.ToString
                        HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, -1, "")

                    Next

                End If

            Next
        Catch ex As Exception

        End Try
        _Spls.Close()
    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        _State = False
        Me.Close()
    End Sub



    Private Sub ReposFDShipDateTo_EditValueChanging(sender As Object, e As ChangingEventArgs)
        Try
            With Me.ogv
                Select Case .FocusedColumn.FieldName.ToString.ToLower
                    Case "FDShipDateTo".ToLower
                        If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) = "" & .GetFocusedRowCellValue("FDShipDateT").ToString Then
                            .SetFocusedRowCellValue("FTStateChange", "0")
                        Else
                            .SetFocusedRowCellValue("FTStateChange", "1")
                        End If
                    Case "FDShipDateOrginalTo".ToLower
                        If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) = "" & .GetFocusedRowCellValue("FDShipDateOrginalT").ToString Then
                            .SetFocusedRowCellValue("FTStateChangeO", "0")
                        Else
                            .SetFocusedRowCellValue("FTStateChangeO", "1")
                        End If
                    Case "FDCfmShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChange", "1")


                    Case "FDORShipDate".ToLower
                        .SetFocusedRowCellValue("FTStateChangeO", "1")
                        .SetRowCellValue(.FocusedRowHandle, "FDCfmShipDate", e.NewValue)

                        'Dim selectedRowHandles As Int32() = .GetSelectedRows()
                        'Dim I As Integer
                        'For I = 0 To selectedRowHandles.Length - 1
                        '    Dim selectedRowHandle As Int32 = selectedRowHandles(I)
                        '    If (selectedRowHandle >= 0) Then
                        '        .SetRowCellValue(selectedRowHandle, "FDCfmShipDate", e.NewValue)
                        '    End If
                        'Next
                        For Ix As Integer = 0 To .RowCount Step 1
                            If .GetRowCellValue(Ix, "FTSelect").ToString = "1" Then
                                .SetRowCellValue(Ix, "FDORShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTStateChangeO", "1")
                                .SetRowCellValue(Ix, "FDCfmShipDate", e.NewValue)
                                .SetRowCellValue(Ix, "FTSelect", "0")
                            End If
                        Next


                End Select

            End With

            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With Me.ogv
            Try

                Select Case True
                    Case (("" & .GetRowCellValue(e.RowHandle, "FTStateChange").ToString = "1") Or ("" & .GetRowCellValue(e.RowHandle, "FTStateChangeO").ToString = "1"))
                        Try
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTSelect").ToString = "1")
                        e.Appearance.BackColor = System.Drawing.Color.CornflowerBlue
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTSelect").ToString = "0")
                        e.Appearance.BackColor = System.Drawing.Color.Transparent
                        If ("" & .GetRowCellValue(e.RowHandle, "FNCartonQty").ToString) = ("" & .GetRowCellValue(e.RowHandle, "FTStateCartonClose").ToString) Then
                            e.Appearance.BackColor = System.Drawing.Color.Salmon
                        End If

                End Select

            Catch ex As Exception
            End Try
        End With
    End Sub

    Private Sub ReposFDShipDateTo_Spin(sender As Object, e As SpinEventArgs)
        e.Handled = True
    End Sub

    'Private Sub ocmsave_Click(sender As Object, e As EventArgs)
    '    If Not (Me.ogc.DataSource Is Nothing) Then
    '        Dim _dt As DataTable
    '        With CType(Me.ogc.DataSource, DataTable)
    '            .AcceptChanges()
    '            _dt = .Copy()
    '        End With

    '        If _dt.Select("FTStateChange='1' OR FTStateChangeO='1'").Length > 0 Then
    '            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล GAC Date ใช่หรือไม่ ?", 1513170001) Then

    '                If Me.SaveGacDate() Then
    '                    '  Call SendMailToProductin()
    '                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '                    Me.LoadData()
    '                Else
    '                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '                End If

    '            End If
    '        Else
    '            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่มีการแก้ไข GAC Date กรุณาทำการตรวจสอบ !!!", 1513170002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
    '        End If

    '    End If
    'End Sub


    Private _oDtselect As DataTable = Nothing
    Public Property oDtselect As DataTable
        Get
            Return _oDtselect
        End Get
        Set(value As DataTable)
            _oDtselect = value
        End Set
    End Property
    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property
    Private Sub ocmsavedocument_Click(sender As Object, e As EventArgs) Handles ocmselect.Click
        If Not (Me.ogcsum.DataSource Is Nothing) Then
            Dim _dt As DataTable
            Dim _PORef As String = "" : Dim _POLine As String = ""
            Dim _Where As String = "" : Dim _WhereMuti As String = ""
            With DirectCast(Me.ogcsum.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTSelect='1'")
                    Dim _last As Integer = 0
                    Try
                        _last = (R!FTPOLine.ToString).LastIndexOf(",")
                    Catch ex As Exception

                    End Try
                    If _Where <> "" Then _Where &= " OR "

                    If _last < 0 Then
                        If (_WhId <> 1567590012) Then
                            _Where &= " ( A.FTCustomerPO ='" & R!FTCustomerPO.ToString & "' and B.FTPOLine ='" & R!FTPOLine.ToString & "')"
                        Else
                            _Where &= " ( A.FTCustomerPO ='" & R!FTCustomerPO.ToString & "' and B.FTPOLine ='" & R!FTPOLine.ToString & "')"
                        End If
                    Else
                        For Each str As String In (R!FTPOLine.ToString).Split(",")
                            If _WhereMuti <> "" Then _WhereMuti &= " OR "
                            If (_WhId <> 1567590012) Then
                                _WhereMuti &= " ( A.FTCustomerPO ='" & R!FTCustomerPO.ToString & "' and B.FTPOLine ='" & str & "')"
                            Else
                                _WhereMuti &= " (A.FTCustomerPO ='" & R!FTCustomerPO.ToString & "' and B.FTPOLine ='" & str & "')"
                            End If
                        Next
                        _Where &= _WhereMuti
                        _WhereMuti = ""

                    End If


                Next

            End With




            Dim _Qry As String = ""

            If (_WhId <> 1567590012) Then

                If HI.ST.SysInfo.CmpCode = "HTWH" Then

                    _Qry = " Select  FTSelect , FTBarCodeEAN13 ,FTCustomerPO , FTPOLine ,FTPackNo , FNCartonNo ,    FNQuantity "
                    _Qry &= vbCrLf & " ,FTCartonCode , FDShipDate ,TotalCbm , FTStyleCode , FTStyleName "

                    _Qry &= vbCrLf & "  , HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(FTPackNo,FNCartonNo) AS FTCartonInfo   From ( SELECT distinct '1' as   FTSelect , Z.FTBarCodeEAN13 ,A.FTCustomerPO , B.FTPOLine ,A.FTPackNo,C.FNCartonNo"

                    _Qry &= vbCrLf & " ,    sum(B.FNQuantity)over(partition by  Z.FTBarCodeEAN13)    as FNQuantity,  T.FTCartonCode,convert(nvarchar(10) ,convert(date, max( ZT.FDShipDate	)over(partition by A.FTPackNo,C.FNCartonNo) 	),103) as FDShipDate	 "
                    _Qry &= vbCrLf & " ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , A.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName  "
                    _Qry &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.V_FG_MergePackOrder AS A WITH (NOLOCK) LEFT OUTER JOIN "
                    _Qry &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKCarton AS C WITH(NOLOCK) ON A.FTPackNo = C.FTPackNo and B.FNCartonNo = C.FNCartonNo "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON B.FNHSysCartonId = T.FNHSysCartonId		 "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Barcode  AS Z   with(nolock) ON   A.FTPackNo = Z.FTPackNo and B.FNCartonNo = Z.FNCartonNo	 "
                    _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON A.FNHSysStyleId = S.FNHSysStyleId "
                    _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination AS ZT WITH(NOLOCK) ON A.FTCustomerPO = ZT.FTPOref and  B.FTPOLine =  ZT.FTNikePOLineItem	"
                    _Qry &= vbCrLf & "   and B.FTSizeBreakDown = ZT.FTSizeBreakDown and B.FTColorway = ZT.FTColorway and B.FTOrderNo = ZT.FTOrderNo and B.FTSubOrderNo = ZT.FTSubOrderNo "
                    _Qry &= vbCrLf & " where  ZT.FNGrandQuantity > 0 and  isnull( B.FTPOLine,'') <> '' and isnull( A.FTCustomerPO,'') <> '' " 'and  A.FNHSysCmpId=" & Val(CmpId)
                    _Qry &= vbCrLf & " and Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-30, GETDATE()),111)" 'and isnull(C.FTStateOut,'0') = '0' 
                    '_Qry &= vbCrLf & " and isnull (Z.FTBarCodeEAN13 ,'' )  <> '' "
                    _Qry &= vbCrLf & " and (" & _Where & ")"
                    '_Qry &= vbCrLf & " and   Z.FTBarCodeEAN13  not in (   Select  FTBarcodeNo  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D with(nolock) where FTTruckSheetNo <> '" & _DocNo & "' ) "
                    _Qry &= vbCrLf & " ) AS T "



                Else

                    _Qry = " Select  FTSelect , FTBarCodeEAN13 ,FTCustomerPO , FTPOLine ,FTPackNo , FNCartonNo ,    FNQuantity "
                    _Qry &= vbCrLf & " ,FTCartonCode , FDShipDate ,TotalCbm , FTStyleCode , FTStyleName "

                    _Qry &= vbCrLf & "  , HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(FTPackNo,FNCartonNo) AS FTCartonInfo   From ( SELECT distinct '1' as   FTSelect , Z.FTBarCodeEAN13 ,A.FTCustomerPO , B.FTPOLine ,A.FTPackNo,C.FNCartonNo"

                    _Qry &= vbCrLf & " ,    sum(B.FNQuantity)over(partition by  Z.FTBarCodeEAN13)    as FNQuantity,  T.FTCartonCode,convert(nvarchar(10) ,convert(date, max( ZT.FDShipDate	)over(partition by A.FTPackNo,C.FNCartonNo) 	),103) as FDShipDate	 "
                    _Qry &= vbCrLf & " ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , A.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName  "
                    _Qry &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack AS A WITH (NOLOCK) LEFT OUTER JOIN "
                    _Qry &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKCarton AS C WITH(NOLOCK) ON A.FTPackNo = C.FTPackNo and B.FNCartonNo = C.FNCartonNo "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON B.FNHSysCartonId = T.FNHSysCartonId		 "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Barcode  AS Z   with(nolock) ON   A.FTPackNo = Z.FTPackNo and B.FNCartonNo = Z.FNCartonNo	 "
                    _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON A.FNHSysStyleId = S.FNHSysStyleId "
                    _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination AS ZT WITH(NOLOCK) ON A.FTCustomerPO = ZT.FTPOref and  B.FTPOLine =  ZT.FTNikePOLineItem	"
                    _Qry &= vbCrLf & "   and B.FTSizeBreakDown = ZT.FTSizeBreakDown and B.FTColorway = ZT.FTColorway and B.FTOrderNo = ZT.FTOrderNo and B.FTSubOrderNo = ZT.FTSubOrderNo "
                    _Qry &= vbCrLf & " where  ZT.FNGrandQuantity > 0 and  isnull( B.FTPOLine,'') <> '' and isnull( A.FTCustomerPO,'') <> '' and  A.FNHSysCmpId=" & Val(CmpId)
                    _Qry &= vbCrLf & " and Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-30, GETDATE()),111)" 'and isnull(C.FTStateOut,'0') = '0' 
                    '_Qry &= vbCrLf & " and isnull (Z.FTBarCodeEAN13 ,'' )  <> '' "
                    _Qry &= vbCrLf & " and (" & _Where & ")"
                    _Qry &= vbCrLf & " and   Z.FTBarCodeEAN13  not in (   Select  a.FTBarcodeNo  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D a   with(nolock) "
                    _Qry &= vbCrLf & " inner join    TFGTTruckSheet b on a.FTTruckSheetNo = b.FTTruckSheetNo  where a.FTTruckSheetNo <> '" & _DocNo & "' and b.FNHSysCmpId = " & Val(CmpId) & " ) "
                    _Qry &= vbCrLf & " ) AS T "
                End If



            Else
                '_Qry = "  SELECT distinct  '1' as   FTSelect  ,convert(nvarchar(10) ,convert(date, max( ZT .FDShipDate	)over(partition by Z.FTPackNo,Z.FNCartonNo) 	),103) as FDShipDate   , D.FTBarcodeNo as FTBarCodeEAN13 ,Z.FTCustomerPO , Z.FTPOLine  "

                '_Qry &= vbCrLf & "    ,HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(Z.FTPackNo,Z.FNCartonNo) AS FTCartonInfo  "
                '_Qry &= vbCrLf & "  ,   sum(Z.FNQuantity)over(partition by D.FTBarcodeNo)  as FNQuantity, T.FTCartonCode	 "
                '_Qry &= vbCrLf & "  ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , D.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName   "
                '_Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheetApproved AS A with(nolock) "
                '_Qry &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheetApproved_D AS D WITH(NOLOCK) ON A.FTTruckSheetNo = D.FTTruckSheetNo"
                '_Qry &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBarcodeScanFG  AS Z   with(nolock) ON     D.FTBarcodeNo = Z.FTBarCodeCarton"

                ''_Qry &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo. TPACKOrderPack_Carton_Barcode AS Y with(nolock) ON D.FTBarcodeNo = Y.FTBarCodeEAN13"
                '_Qry &= vbCrLf & " LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS PD With(nolock) ON Z.FTPackNo = PD.FTPackNo and Z.FNCartonNo = PD.FNCartonNo"
                '_Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON PD.FNHSysCartonId = T.FNHSysCartonId	"

                '_Qry &= vbCrLf & "  LEFT OUTER JOIN    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination  AS ZT   ON Z.FTCustomerPO = ZT.FTPOref and  Z.FTPOLine =  ZT.FTNikePOLineItem	"
                '_Qry &= vbCrLf & " and   z.FTColorway = ZT.FTColorway --and Z.FTOrderNo = ZT.FTOrderNo  "
                '_Qry &= vbCrLf & "LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON ZT.FNHSysStyleId = S.FNHSysStyleId  "
                '_Qry &= vbCrLf & " where   ZT.FNGrandQuantity > 0 --and  Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-30, GETDATE()),111)    " 'and  A.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
                '_Qry &= vbCrLf & "  and (" & _Where & ")"
                '_Qry &= vbCrLf & "   and Z.FNHSysWHFGId =" & _WhId


                _Qry = " Select  FTSelect , FTBarCodeEAN13 ,FTCustomerPO , FTPOLine ,FTPackNo , FNCartonNo ,    FNQuantity "
                _Qry &= vbCrLf & " ,FTCartonCode , FDShipDate ,TotalCbm , FTStyleCode , FTStyleName "

                _Qry &= vbCrLf & "  , HITECH_PRODUCTION.dbo.fn_Get_Carton_Info(FTPackNo,FNCartonNo) AS FTCartonInfo   From ( SELECT distinct '1' as   FTSelect , Z.FTBarCodeEAN13 ,A.FTCustomerPO , B.FTPOLine ,A.FTPackNo,Z.FNCartonNo"

                _Qry &= vbCrLf & " ,    sum(B.FNQuantity)over(partition by  Z.FTBarCodeEAN13)    as FNQuantity,  T.FTCartonCode,convert(nvarchar(10) ,convert(date, max( ZT.FDShipDate	)over(partition by A.FTPackNo,C.FNCartonNo) 	),103) as FDShipDate	 "
                _Qry &= vbCrLf & " ,  (((T.FNWidth /1000) * (T.FNLength /1000) ) * (T.FNHeight /1000))  AS TotalCbm  , A.FNHSysStyleId , S.FTStyleCode , S.FTStyleNameEN as FTStyleName  "
                _Qry &= vbCrLf & "  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.V_FG_MergePackOrder AS A WITH (NOLOCK) LEFT OUTER JOIN "
                _Qry &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKCarton AS C WITH(NOLOCK) ON A.FTPackNo = C.FTPackNo and B.FNCartonNo = C.FNCartonNo "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS T WITH(NOLOCK) ON B.FNHSysCartonId = T.FNHSysCartonId		 "
                _Qry &= vbCrLf & "    LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Barcode  AS Z   with(nolock) ON   A.FTPackNo = Z.FTPackNo and B.FNCartonNo = Z.FNCartonNo	 "
                _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS S WITH(NOLOCK) ON A.FNHSysStyleId = S.FNHSysStyleId "
                _Qry &= vbCrLf & "  LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination AS ZT WITH(NOLOCK) ON A.FTCustomerPO = ZT.FTPOref and  B.FTPOLine =  ZT.FTNikePOLineItem	"
                _Qry &= vbCrLf & "   and B.FTSizeBreakDown = ZT.FTSizeBreakDown and B.FTColorway = ZT.FTColorway and B.FTOrderNo = ZT.FTOrderNo and B.FTSubOrderNo = ZT.FTSubOrderNo "
                _Qry &= vbCrLf & " where  ZT.FNGrandQuantity > 0 and  isnull( B.FTPOLine,'') <> '' and isnull( A.FTCustomerPO,'') <> '' " 'and  A.FNHSysCmpId=" & Val(CmpId)
                _Qry &= vbCrLf & " and Zt.FDShipDate >=  convert(varchar(10),  DATEADD(day,-30, GETDATE()),111)" 'and isnull(C.FTStateOut,'0') = '0' 
                '_Qry &= vbCrLf & " and isnull (Z.FTBarCodeEAN13 ,'' )  <> '' "
                _Qry &= vbCrLf & " and (" & _Where & ")"
                '_Qry &= vbCrLf & " and   Z.FTBarCodeEAN13  not in (   Select  FTBarcodeNo  FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo.TFGTTruckSheet_D with(nolock) where FTTruckSheetNo <> '" & _DocNo & "' ) "
                _Qry &= vbCrLf & " ) AS T "



            End If
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
            _oDtselect = Nothing
            If _dt.Select("FTSelect = '1'").Length > 0 Then
                _oDtselect = _dt.Select("FTSelect = '1'").CopyToDataTable()
            End If


            _State = True
            Me.Close()
        End If
    End Sub





    Private Sub RepositoryItemsFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemsFTSelect.EditValueChanging
        Try

            With ogv
                If .FocusedRowHandle < -1 Then Exit Sub
                .SetRowCellValue(.FocusedRowHandle, "FTSelect", e.NewValue)
                Dim _Total As Integer = 0
                Dim _TotalCBM As Double = 0
                With DirectCast(Me.ogc.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")
                        _Total += +1
                        _TotalCBM += +Val(R!TotalCbm.ToString)
                    Next
                    Me.FNTotalCarton.Value = _Total
                    Me.FNTotalCBM.Value = _TotalCBM


                End With
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private pDt As New DataTable
    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        Try

            With ogvsum
                If .FocusedRowHandle < -1 Then Exit Sub
                .SetRowCellValue(.FocusedRowHandle, "FTSelect", e.NewValue)
                If e.NewValue = "1" Then
                    Dim _PORef As String = .GetRowCellValue(.FocusedRowHandle, "FTCustomerPO")
                    Dim _POLine As String = .GetRowCellValue(.FocusedRowHandle, "FTPOLine")

                    Dim dt As DataTable
                    Dim _Total As Integer = 0
                    Dim _TotalCBM As Double = 0
                    With DirectCast(Me.ogcsum.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Select("FTSelect='1'")
                            _Total += +Val(R!FNTotalCarton.ToString)
                            _TotalCBM += +Val(R!TotalCbm.ToString)
                        Next
                        Me.FNTotalCarton.Value = _Total
                        Me.FNTotalCBM.Value = _TotalCBM
                        Me.ogc.DataSource = pDt
                        With DirectCast(Me.ogc.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTCustomerPO='" & _PORef & "' and FTPOLine='" & _POLine & "'")
                                R!FTSelect = "1"
                                _Total += +1
                                _TotalCBM += +Val(R!TotalCbm.ToString)
                            Next
                            .AcceptChanges()
                            dt = .Copy
                        End With
                        Me.ogc.DataSource = dt.Select("FTSelect='1'").CopyToDataTable

                    End With
                Else
                    Dim _PORef As String = .GetRowCellValue(.FocusedRowHandle, "FTCustomerPO")
                    Dim _POLine As String = .GetRowCellValue(.FocusedRowHandle, "FTPOLine")

                    Dim dt As DataTable
                    Dim _Total As Integer = 0
                    Dim _TotalCBM As Double = 0
                    With DirectCast(Me.ogcsum.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Select("FTCustomerPO='" & _PORef & "' and FTPOLine='" & _POLine & "' and FTSelect='0'")
                            _Total += +Val(R!FNTotalCarton.ToString)
                            _TotalCBM += +Val(R!TotalCbm.ToString)
                        Next
                        Me.FNTotalCarton.Value = Me.FNTotalCarton.Value - _Total
                        Me.FNTotalCBM.Value = Me.FNTotalCBM.Value - _TotalCBM
                        If Me.FNTotalCarton.Value <= 0 Then
                            Me.FNTotalCarton.Value = 0
                        End If
                        If Me.FNTotalCBM.Value <= 0 Then
                            Me.FNTotalCBM.Value = 0
                        End If
                        Me.ogc.DataSource = pDt
                        With DirectCast(Me.ogc.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTCustomerPO='" & _PORef & "' and FTPOLine='" & _POLine & "' and FTSelect='1'")
                                R!FTSelect = "0"
                                '_Total += +1
                                '_TotalCBM += +Val(R!TotalCbm.ToString)
                            Next
                            .AcceptChanges()
                            dt = .Copy
                        End With
                        Me.ogc.DataSource = dt.Select("FTSelect='1'").CopyToDataTable

                    End With
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub



    'Private Sub ogv_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles ogv.SelectionChanged
    '    Try
    '        'With Me.ogv
    '        '    If .RowCount > 0 And .FocusedRowHandle > -1 Then
    '        '        For Each i As Integer In .GetSelectedRows()
    '        '            If .GetRowCellValue(i, "FTSelect") = "0" Then
    '        '                .SetRowCellValue(i, "FTSelect", "1")
    '        '            Else
    '        '                .SetRowCellValue(i, "FTSelect", "0")
    '        '            End If
    '        '        Next
    '        '    End If
    '        'End With
    '        Dim _Total As Integer = 0
    '        Dim _TotalCBM As Double = 0
    '        With DirectCast(Me.ogc.DataSource, DataTable)
    '            .AcceptChanges()
    '            For Each R As DataRow In .Select("FTSelect='1'")
    '                _Total += +1
    '                _TotalCBM += +Val(R!TotalCbm.ToString)
    '            Next
    '            Me.FNTotalCarton.Value = _Total
    '            Me.FNTotalCBM.Value = _TotalCBM


    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub ogv_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles ogv.MasterRowGetRelationCount
    '    e.RelationCount = 1
    'End Sub

    'Private Sub ogv_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles ogv.MasterRowGetRelationName

    '    e.RelationName = "FTCustomerPO"

    'End Sub

    'Private Sub ogv_MasterRowEmpty(sender As Object, e As MasterRowEmptyEventArgs) Handles ogv.MasterRowEmpty
    '    e.IsEmpty = False
    'End Sub

    'Private Sub ogv_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles ogv.MasterRowGetChildList
    '    Try
    '        Dim view As GridView = DirectCast(sender, GridView)
    '        e.ChildList = GetDetail(ogv.GetRowCellValue(ogv.FocusedRowHandle, "FTPORef").ToString, ogv.GetRowCellValue(ogv.FocusedRowHandle, "FTNikePOLineItem").ToString)

    '        'GridView1.Columns.ColumnByFieldName("CSSaleOrderNo").Caption = "ใบ"
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Function GetDetail(Key As String, lineNo As String) As DataView
    '    Try
    '        Dim dt As DataTable = _ds.Tables(1)
    '        Dim dv As DataView = New DataView(dt)


    '        dv.RowFilter = "FTCustomerPO='" & Key & "' and FTPOLine='" & lineNo & "'"
    '        dv.AllowEdit = False
    '        dv.AllowNew = False
    '        dv.AllowDelete = False

    '        Return dv
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function



    Private Sub SaveLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)
                    HI.UL.AppRegistry.SaveLayoutGridToRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)
                Case False
            End Select

            If Obj.Controls.count > 0 Then
                SaveLayout(Obj, MainParent)
            End If
        Next


    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        Try
            SaveLayout(Me, Me)
            HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)

                    Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)

                Case False
            End Select

            If Obj.Controls.count > 0 Then
                LoadLayout(Obj, MainParent)
            End If
        Next


    End Sub

End Class