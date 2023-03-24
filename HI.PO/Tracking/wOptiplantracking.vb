Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.Data
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Data.SqlClient
Public Class wOptiplantracking

    Private _RowDataChange As Boolean
    Private StateCal As Boolean = False
  
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call W_GETbInitialGrid()
        Call InitGrid()
        Call InitialGridMergeCell()

        With ogv

            .Columns("LectraUsage").Summary.Add(DevExpress.Data.SummaryItemType.None, "LectraUsage")
            .Columns("LectraUsage").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("ReworkRecut").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            ' .Columns("QTY").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("Standard").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("VarianceYard").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("VariancePercent").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("TotalCutVariance").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("TotalCutAVERAGE").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("THB").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("FabricSaving").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("TotalAetRecut").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"


            .OptionsView.ShowFooter = True
        End With


    End Sub
#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Initial Grid"

    Private Sub InitGridClearSort()
        For Each c As GridColumn In ogv.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next


    End Sub

    Private Sub InitGrid()

        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "QTY|ReworkRecut|LectraUsage|Standard|VarianceYard|TotalAetRecut|VariancePercent|TotalCutVariance|TotalCutAVERAGE|THB|FabricSaving"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

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

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With
    End Sub

    Private Sub InitialGridMergeCell()
        For Each C As GridColumn In ogv.Columns
            Select Case C.FieldName.ToString
                Case "FTLaycutDate", "FTBuyCode", "FTStyleCode", "FTOrderNo", "FTColorway", "ToTal", "FTMarkCode", "QTY", "LectraUsage", "Standard", "VariancePercent", "TotalCutVariance", "TotalCutAVERAGE", "THB", "FabricSaving", "ReworkRecut", "VarianceYard", "TotalAetRecut"
                    C.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    C.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    C.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    C.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            End Select

        Next
    End Sub

#End Region

#Region "Custom summaries"



    '  Private totalSum2 As Integer = 0
    ' Private GrpSum2 As Integer = 0
    ' Private _RowHandleHold2 As Integer = 0


    ' Private Sub InitSummaryStartValue()
    ' totalSum2 = 0
    'GrpSum2 = 0
    ' _RowHandleHold2 = 0
    'End Sub





    ' Private Sub ogv_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogv.CustomSummaryCalculate
    ' Try
    '   If e.SummaryProcess = CustomSummaryProcess.Start Then
    '     InitSummaryStartValue()
    ' End If

    ' With ogv
    '  Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
    '    Case "QTY", "ReworkRecut", "LectraUsage", "Standard", "VarianceYard", "VariancePercent", "TotalCutVariance", "TotalCutAVERAGE", "THB", "FabricSaving"
    '        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
    '           If e.IsTotalSummary Then
    '               If e.RowHandle <> _RowHandleHold2 Or e.RowHandle = 0 Then
    '                   If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold2, "FTOrderNo").ToString Or _
    '                                             .GetRowCellValue(e.RowHandle, "FTColorway").ToString <> .GetRowCellValue(_RowHandleHold2, "FTColorway").ToString Or _
    '                                     .GetRowCellValue(e.RowHandle, "FTMarkCode").ToString <> .GetRowCellValue(_RowHandleHold2, "FTMarkCode").ToString) Or e.RowHandle = _RowHandleHold2 Then
    '             totalSum2 = totalSum2 + Integer.Parse(Val(e.FieldValue.ToString))
    '        End If
    '     End If
    '     _RowHandleHold2 = e.RowHandle
    '   End If
    '     e.TotalValue = totalSum2
    '    End If
    ' End Select
    '   End With
    'Catch ex As Exception
    ' End Try
    ' End Sub


#End Region

#Region "Verrify"

    Private Function Verrify()
        Try
            Dim _Pass As Boolean = False

            If Me.FTStartDate.Text <> "" Then
                _Pass = True
            End If

            If Me.FTEndDate.Text <> "" Then
                _Pass = True
            End If

            If Me.FNHSysBuyId.Text <> "" Then
                _Pass = True
            End If

            If Me.FNHSysStyleId.Text <> "" Then
                _Pass = True
            End If


            If Me.FTOrderNo.Text <> "" Then
                _Pass = True
            End If

            If Me.FTOrderNoTo.Text <> "" Then
                _Pass = True
            End If

            If Not (_Pass) Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงือนไข อย่างน้อย 1 รายการ !!!", 14010101010101, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                '  Me.FTStartDate.Focus()
            End If
            Return _Pass
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Procedure And Function"

    Private Function W_GETbInitialGrid() As Boolean
        Dim bPass As Boolean = False


        VarianceYard.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        VarianceYard.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        Standard.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Standard.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        LectraUsage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        LectraUsage.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        VariancePercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        VariancePercent.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        TotalCutVariance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        TotalCutVariance.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        TotalCutAVERAGE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        TotalCutAVERAGE.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        QTY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        QTY.DisplayFormat.FormatString = "{0:N0}"

        ToTal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        ToTal.DisplayFormat.FormatString = "{0:N0}"

        THB.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        THB.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        FabricSaving.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        FabricSaving.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        ReworkRecut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        ReworkRecut.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        TotalAetRecut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        TotalAetRecut.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        Return bPass

    End Function

#End Region


    Private Sub LoadData()
        GridColumn.DataSource = Nothing
        Dim Spls As New HI.TL.SplashScreen("Please Wait Loading Data.....")
        Dim _Qry As String = ""
        Dim _dt As DataTable

        Try
            _Qry &= vbCrLf & "SELECT  convert(varchar(10),convert(date,L.FTLaycutDate),103) AS FTLaycutDate"
            _Qry &= vbCrLf & ", S.FTStyleCode"
            _Qry &= vbCrLf & ",B.FTBuyCode"
            _Qry &= vbCrLf & ",O.FTOrderNo"
            _Qry &= vbCrLf & ",LD.FTColorway"

            '_Qry &= vbCrLf & ",SUM(LD.FNLayerQuantity) * SUM(LR.FNQuantity) AS QTY"
            _Qry &= vbCrLf & " ,BBB.FNGrandQuantity QTY"
            _Qry &= vbCrLf & ",VV.Qty AS ToTal"
            ' _Qry &= vbCrLf & ",U.QTY AS ToTal"

            _Qry &= vbCrLf & ",M.FTMarkCode "
            _Qry &= vbCrLf & ",LU.LectraUsage "
            _Qry &= vbCrLf & ",ISNULL(PP.StandardUsage1,0) AS Standard"
            _Qry &= vbCrLf & ",ISNULL(PP.StandardUsage1,0) - LU.LectraUsage     AS VarianceYard"
            _Qry &= vbCrLf & ",(LU.LectraUsage  - ISNULL(PP.StandardUsage1,0))*100 / ISNULL(PP.StandardUsage1,0)  AS  VariancePercent"
            _Qry &= vbCrLf & ",ISNULL(SD.ReworkRecut,0) AS ReworkRecut "
            _Qry &= vbCrLf & ", ISNULL(PP.StandardUsage1,0)  + ISNULL(SD.ReworkRecut,0)  AS TotalAetRecut"
            _Qry &= vbCrLf & ",(LU.LectraUsage  + ISNULL(SD.ReworkRecut,0) - ISNULL(PP.StandardUsage1,0)) *100 / ISNULL(PP.StandardUsage1,0) AS TotalCutVariance"
            '_Qry &= vbCrLf & ",AVG(LU.LectraUsage  + ISNULL(SD.ReworkRecut,0) - ISNULL(PP.StandardUsage1,0)) *100 / ISNULL(PP.StandardUsage1,0) AS TotalCutAVERAGE"
            _Qry &= vbCrLf & ",SUM(TTV.TV) / COUNT(O.FTOrderNo ) AS TotalCutAVERAGE"
            _Qry &= vbCrLf & ", BB.THB AS THB"
            _Qry &= vbCrLf & ",( LU.LectraUsage-(ISNULL(PP.StandardUsage1,0)  + ISNULL(SD.ReworkRecut,0))) * BB.THB   AS FabricSaving"
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P  WITH(NOLOCK) "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS L      WITH(NOLOCK)   ON P.FTOrderProdNo = L.FTOrderProdNo "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK)   ON P.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B        WITH(NOLOCK)   ON O.FNHSysBuyId = B.FNHSysBuyId"
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S      WITH(NOLOCK)   ON O.FNHSysStyleId = S.FNHSysStyleId "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS LD   WITH(NOLOCK)   ON L.FTLayCutNo = LD.FTLayCutNo"
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M      WITH(NOLOCK)   ON L.FNHSysMarkId = M.FNHSysMarkId "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio AS LR WITH(NOLOCK) ON L.FTLayCutNo = LR.FTLayCutNo"

            '               หาค่า QTY                        
            _Qry &= vbCrLf & "INNER JOIN "
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT FTOrderNo"
            _Qry &= vbCrLf & ",SUM(FNGrandQuantity) AS FNGrandQuantity"
            _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SBD "
            _Qry &= vbCrLf & "GROUP BY FTOrderNo"
            _Qry &= vbCrLf & ") AS BBB ON O.FTOrderNo = BBB.FTOrderNo "
            '                   หาค่า ToTal                    

            ' _Qry &= vbCrLf & "INNER JOIN "
            '_Qry &= vbCrLf & "   ("
            '_Qry &= vbCrLf & "   SELECT SA.FTOrderNo"
            '_Qry &= vbCrLf & "   ,Sum(SA.FNQuantity) AS QTY,SA.FNHSysMarkId" ',SA.FTColorway"
            '_Qry &= vbCrLf & "   FROM    "
            '_Qry &= vbCrLf & "      ("
            '_Qry &= vbCrLf & "      SELECT O.FTOrderNo , LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown, SUM(BD.FNQuantity) AS FNQuantity"
            '_Qry &= vbCrLf & "      FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH(NOLOCK)  "
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo "
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH(NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo "
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O    ON P.FTOrderNo = O.FTOrderNo"
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OPR WITH(NOLOCK) ON A.FNHSysOperationId = OPR.FNHSysOperationId "
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS BD  WITH(NOLOCK)  ON A.FTBarcodeNo=BD.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS LC  WITH(NOLOCK)  ON BD.FTLayCutNo = LC.FTLayCutNo"
            '_Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS H WITH(NOLOCK) ON A.FTDocScanNo=H.FTDocScanNo"
            '_Qry &= vbCrLf & "     WHERE (OPR.FTStateSPMK = '1')"
            '_Qry &= vbCrLf & "     GROUP BY O.FTOrderNo,LC.FTOrderProdNo, LC.FNHSysMarkId, LC.FNTableNo, BD.FTColorway, BD.FTSizeBreakDown)AS SA "
            '_Qry &= vbCrLf & "   GROUP BY SA.FTOrderNo,SA.FNHSysMarkId)AS U ON O.FTOrderNo = U.FTOrderNo 	 and M.FNHSysMarkId = U.FNHSysMarkId  " 'and LD.FTColorway = U.FTColorway"


            '        หาค่า ToTal      
            _Qry &= vbCrLf & " INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT   O.FTOrderNo,M.FNHSysMarkId ,LD.FTColorway "
            _Qry &= vbCrLf & ", SUM(LD.FNLayerQuantity) * (LR.FNQuantity) AS Qty "
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P  WITH(NOLOCK) "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS L      WITH(NOLOCK)   ON P.FTOrderProdNo = L.FTOrderProdNo "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK)   ON P.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS LD   WITH(NOLOCK)   ON L.FTLayCutNo = LD.FTLayCutNo"
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio AS LR WITH(NOLOCK) ON L.FTLayCutNo = LR.FTLayCutNo"
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M      WITH(NOLOCK)   ON L.FNHSysMarkId = M.FNHSysMarkId "
            ' _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS MM  ON P.FTOrderProdNo = MM.FTOrderProdNo AND L.FNHSysMarkId =MM.FNHSysMarkId "
            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo ,LR.FNQuantity,M.FNHSysMarkId ,LD.FTColorway  "
            _Qry &= vbCrLf & ") AS VV ON O.FTOrderNo = VV.FTOrderNo AND LD.FTColorway = VV.FTColorway AND M.FNHSysMarkId = VV.FNHSysMarkId "

            '        หาค่า LectraUsage  จำนวนออฟติเพลน         
            _Qry &= vbCrLf & "  INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT   FTMark AS FTMarkCode "
            _Qry &= vbCrLf & ",FTMatColorCode AS FTColorway"
            _Qry &= vbCrLf & ",SUM(FNTotal) / 0.9144 AS LectraUsage "
            _Qry &= vbCrLf & ",FTOrderNo"
            _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan  AS U   "
            _Qry &= vbCrLf & "GROUP BY  FTOrderNo,FTMark,FTMatColorCode"
            _Qry &= vbCrLf & ") AS LU ON O.FTOrderNo = LU.FTOrderNo AND LD.FTColorway = LU.FTColorway AND M.FTMarkCode = LU.FTMarkCode "

            '  หาค่าในช่อง Standard()
            _Qry &= vbCrLf & "  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT O.FTOrderNo"
            _Qry &= vbCrLf & ", PC.FNHSysMarkId"
            _Qry &= vbCrLf & ", PCD.FTColorway"
            _Qry &= vbCrLf & ", sum(PC.FNMarkTotal * PCD.FNLayer)  AS StandardUsage1 "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P    "
            _Qry &= vbCrLf & " INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo "
            ' _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS L      WITH(NOLOCK)   ON P.FTOrderProdNo = L.FTOrderProdNo "
            '_Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS LD   WITH(NOLOCK)   ON L.FTLayCutNo = LD.FTLayCutNo"
            _Qry &= vbCrLf & "INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PC    ON P.FTOrderProdNo = PC.FTOrderProdNo" ' AND L.FNHSysMarkId = PC.FNHSysMarkId"
            _Qry &= vbCrLf & " INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer   "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail"
            _Qry &= vbCrLf & "GROUP BY FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer"
            _Qry &= vbCrLf & ")AS PCD   ON PC.FTOrderProdNo = PCD.FTOrderProdNo AND PC.FNTableNo = PCD.FNTableNo AND PC.FNHSysMarkId = PCD.FNHSysMarkId " ' AND LD.FTColorway  = PCD.FTColorway"
            _Qry &= vbCrLf & "WHERE (PC.FTStateRepair <> '1')"
            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo, PC.FNHSysMarkId, PCD.FTColorway"
            _Qry &= vbCrLf & " ) AS PP  ON O.FTOrderNo = PP.FTOrderNo AND  M.FNHSysMarkId = PP.FNHSysMarkId AND LD.FTColorway = PP.FTColorway  "
            '   หาค่าในช่อง ReworkRecut    
            _Qry &= vbCrLf & "  LEFT OUTER JOIN "
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT    O.FTOrderNo"
            _Qry &= vbCrLf & ", PC.FNHSysMarkId"
            _Qry &= vbCrLf & ", PCD.FTColorway"
            _Qry &= vbCrLf & ", sum(PC.FNMarkTotal * PCD.FNLayer) AS ReworkRecut "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P    "
            _Qry &= vbCrLf & "    INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK)   ON P.FTOrderNo = O.FTOrderNo "
            '_Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS L      WITH(NOLOCK)   ON P.FTOrderProdNo = L.FTOrderProdNo "
            '_Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS LD   WITH(NOLOCK)   ON L.FTLayCutNo = LD.FTLayCutNo"
            _Qry &= vbCrLf & "INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PC    ON P.FTOrderProdNo = PC.FTOrderProdNo" ' AND L.FNHSysMarkId = PC.FNHSysMarkId and L.FNTableNo =PC.FNTableNo"
            _Qry &= vbCrLf & "  INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer   "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail"
            _Qry &= vbCrLf & "GROUP BY FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer"
            _Qry &= vbCrLf & ")AS PCD   ON PC.FTOrderProdNo = PCD.FTOrderProdNo AND PC.FNTableNo = PCD.FNTableNo AND PC.FNHSysMarkId = PCD.FNHSysMarkId  " 'AND LD.FTColorway = PCD.FTColorway"
            _Qry &= vbCrLf & "WHERE (PC.FTStateRepair = '1')"
            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo, PC.FNHSysMarkId,  PCD.FTColorway"
            _Qry &= vbCrLf & " ) AS SD  ON O.FTOrderNo = SD.FTOrderNo AND  M.FNHSysMarkId = SD.FNHSysMarkId AND LD.FTColorway = SD.FTColorway "
            '            หาค่า US             
            _Qry &= vbCrLf & "  INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT    O.FTOrderNo"
            _Qry &= vbCrLf & ", MAX(BC.FNPrice) AS THB"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P    "
            _Qry &= vbCrLf & "    INNER JOIN      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK)     ON P.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & "    INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS PR WITH(NOLOCK) ON P.FTOrderProdNo = PR.FTOrderProdNo  "
            _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS BC WITH(NOLOCK)       ON PR.FTPurchaseNo = BC.FTPurchaseNo     AND PR.FNHSysRawMatId = BC.FNHSysRawMatId"
            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo"
            _Qry &= vbCrLf & " ) AS BB  ON O.FTOrderNo = BB.FTOrderNo"
            '            หาค่า ToTalAVG             

            If Me.FTStartDate.Text <> "" Or Me.FTStartDate.Text <> "" Then
                _Qry &= vbCrLf & "  INNER JOIN"
            Else
                _Qry &= vbCrLf & "  LEFT OUTER JOIN"
            End If
            ' _Qry &= vbCrLf & "  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(SELECT    O.FTOrderNo"
            _Qry &= vbCrLf & ", L.FTLaycutDate  ,M.FNHSysMarkId ,LD.FTColorway"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(PP.StandardUsage1,0) <= 0 THEN 0 ELSE ((LU.LectraUsage  + ISNULL(SD.ReworkRecut,0) - ISNULL(PP.StandardUsage1,0)) *100/ ISNULL(PP .StandardUsage1,0)) END as TV"
            'AVG((LU.LectraUsage  + ISNULL(SD.ReworkRecut,0) - ISNULL(PP.StandardUsage1,0))) *100/PP .StandardUsage1  AS TV"
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P  WITH(NOLOCK) "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS L      WITH(NOLOCK)   ON P.FTOrderProdNo = L.FTOrderProdNo "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK)   ON P.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS LD   WITH(NOLOCK)   ON L.FTLayCutNo = LD.FTLayCutNo"
            _Qry &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M      WITH(NOLOCK)   ON L.FNHSysMarkId = M.FNHSysMarkId "
            _Qry &= vbCrLf & "  INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT   FTMark AS FTMarkCode "
            _Qry &= vbCrLf & ",FTMatColorCode AS FTColorway"
            _Qry &= vbCrLf & ",SUM(FNTotal) / 0.9144 AS LectraUsage "
            _Qry &= vbCrLf & ",FTOrderNo"
            _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan  AS U   "
            _Qry &= vbCrLf & "GROUP BY  FTOrderNo,FTMark,FTMatColorCode"
            _Qry &= vbCrLf & ") AS LU ON O.FTOrderNo = LU.FTOrderNo AND LD.FTColorway = LU.FTColorway AND M.FTMarkCode = LU.FTMarkCode "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT O.FTOrderNo"
            _Qry &= vbCrLf & ", PC.FNHSysMarkId"
            _Qry &= vbCrLf & ", PCD.FTColorway"
            _Qry &= vbCrLf & ", sum(PC.FNMarkTotal * PCD.FNLayer)  AS StandardUsage1 "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P    "
            _Qry &= vbCrLf & " INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & "INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PC    ON P.FTOrderProdNo = PC.FTOrderProdNo"
            _Qry &= vbCrLf & " INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer   "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail"
            _Qry &= vbCrLf & "GROUP BY FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer"
            _Qry &= vbCrLf & ")AS PCD   ON PC.FTOrderProdNo = PCD.FTOrderProdNo AND PC.FNTableNo = PCD.FNTableNo AND PC.FNHSysMarkId = PCD.FNHSysMarkId "
            _Qry &= vbCrLf & "WHERE (PC.FTStateRepair <> '1')"
            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo, PC.FNHSysMarkId, PCD.FTColorway"
            _Qry &= vbCrLf & " ) AS PP  ON O.FTOrderNo = PP.FTOrderNo AND  M.FNHSysMarkId = PP.FNHSysMarkId AND LD.FTColorway = PP.FTColorway  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN "
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT    O.FTOrderNo"
            _Qry &= vbCrLf & ", PC.FNHSysMarkId"
            _Qry &= vbCrLf & ", PCD.FTColorway"
            _Qry &= vbCrLf & ", sum(PC.FNMarkTotal * PCD.FNLayer) AS ReworkRecut "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P    "
            _Qry &= vbCrLf & "    INNER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O     WITH(NOLOCK)   ON P.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & "INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS PC    ON P.FTOrderProdNo = PC.FTOrderProdNo"
            _Qry &= vbCrLf & "  INNER JOIN"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & "SELECT FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer   "
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail"
            _Qry &= vbCrLf & "GROUP BY FTOrderProdNo ,FNHSysMarkId,FNTableNo ,FTColorway,FNLayer"
            _Qry &= vbCrLf & ")AS PCD   ON PC.FTOrderProdNo = PCD.FTOrderProdNo AND PC.FNTableNo = PCD.FNTableNo AND PC.FNHSysMarkId = PCD.FNHSysMarkId  "
            _Qry &= vbCrLf & "WHERE (PC.FTStateRepair = '1')"
            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo, PC.FNHSysMarkId,  PCD.FTColorway"
            _Qry &= vbCrLf & " ) AS SD  ON O.FTOrderNo = SD.FTOrderNo AND  M.FNHSysMarkId = SD.FNHSysMarkId AND LD.FTColorway = SD.FTColorway "
            _Qry &= vbCrLf & "   WHERE O.FTOrderNo <> '' "
            If Me.FTStartDate.Text <> "" Then
                _Qry &= vbCrLf & "AND    L.FTLaycutDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "
            End If

            If Me.FTEndDate.Text <> "" Then
                _Qry &= vbCrLf & "AND  L.FTLaycutDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
            End If

            _Qry &= vbCrLf & "GROUP BY O.FTOrderNo,PP.StandardUsage1 ,L.FTLaycutDate,LU.LectraUsage  ,SD.ReworkRecut  ,M.FNHSysMarkId ,LD.FTColorway) AS TTV ON O.FTOrderNo =TTV.FTOrderNo "

            _Qry &= vbCrLf & "   WHERE O.FTOrderNo <> '' "
            If Me.FTStartDate.Text <> "" Then
                _Qry &= vbCrLf & "AND    L.FTLaycutDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "
            End If

            If Me.FTEndDate.Text <> "" Then
                _Qry &= vbCrLf & "AND  L.FTLaycutDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
            End If

            If FTOrderNo.Text <> "" Then
                _Qry &= vbCrLf & " AND O.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
            End If

            If FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & " AND O.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
            End If

            If Me.FNHSysBuyId.Text <> "" Then
                _Qry &= vbCrLf & "AND B.FTBuyCode >='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "'  "
            End If

            If Me.FNHSysBuyIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND B.FTBuyCode <='" & HI.UL.ULF.rpQuoted(FNHSysBuyIdTo.Text) & "'  "
            End If

            If Me.FNHSysStyleId.Text <> "" Then
                _Qry &= vbCrLf & "AND S.FTStyleCode >='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "'  "
            End If

            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Qry &= vbCrLf & "AND S.FTStyleCode <='" & HI.UL.ULF.rpQuoted(FNHSysStyleIdTo.Text) & "'  "
            End If

            _Qry &= vbCrLf & "GROUP BY L.FTLayCutDate,B.FTBuyCode , S.FTStyleCode , O.FTOrderNo , LD.FTColorway, M.FTMarkCode,LU.LectraUsage,PP.StandardUsage1,BB.THB, SD.ReworkRecut,BBB.FNGrandQuantity,VV.Qty" ',U.QTY
            _Qry &= vbCrLf & "ORDER BY O.FTOrderNo ,LD.FTColorway , M.FTMarkCode "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.GridColumn.DataSource = _dt
            _RowDataChange = False
            Spls.Close()
            Call InitialGridMergeCell()
        Catch ex As Exception



        End Try
    End Sub

    Private Sub wOptiplantracking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
            Call InitGridClearSort()
            StateCal = False
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogv_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs)
        Try
            With Me.ogv
                Select Case e.Column.FieldName
                    Case "FTOrderNo", "FTLaycutDate", "FTColorway", "ToTal", "FTMarkCode", "FTBuyCode", "FTStyleCode", "QTY", "LectraUsage", "Standard", "VariancePercent", "TotalCutVariance", "TotalCutAVERAGE", "THB", "FabricSaving", "ReworkRecut", "VarianceYard", "TotalAetRecut"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTLaycutDate", "FTOrderNo", "FTColorway", "ToTal", "FTMarkCode", "FTBuyCode", "FTStyleCode", "QTY", "LectraUsage", "Standard", "VariancePercent", "TotalCutVariance", "TotalCutAVERAGE", "THB", "FabricSaving", "ReworkRecut", "VarianceYard", "TotalAetRecut"

                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        Try
            With Me.ogv
                Select Case e.Column.FieldName
                    Case "LectraUsage"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "LectraUsage")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If

                    Case "Standard"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "Standard")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.Pink
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                    Case "VariancePercent"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "VariancePercent")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red

                        End If
                    Case "TotalAetRecut"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "LectraUsage")) > Double.Parse(.GetRowCellValue(e.RowHandle, "TotalAetRecut")) Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.Pink
                            e.Appearance.ForeColor = System.Drawing.Color.Red

                        End If

                    Case "TotalCutVariance"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "TotalCutVariance")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If

                    Case "TotalCutAVERAGE"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "TotalCutAVERAGE")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                    Case "THB"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "THB")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        End If
                    Case "FabricSaving"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FabricSaving")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        ElseIf Double.Parse(.GetRowCellValue(e.RowHandle, "FabricSaving")) < 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                    Case "ReworkRecut"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "ReworkRecut")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        ElseIf Double.Parse(.GetRowCellValue(e.RowHandle, "ReworkRecut")) < 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                    Case "VarianceYard"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "VarianceYard")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.WhiteSmoke
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                    Case "QTY"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "QTY")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.BlanchedAlmond
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                    Case "ToTal"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "ToTal")) > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.BlanchedAlmond
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Red
                        End If
                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click_1(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Verrify() Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclear_Click_1(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.GridColumn.DataSource = Nothing
    End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub



End Class