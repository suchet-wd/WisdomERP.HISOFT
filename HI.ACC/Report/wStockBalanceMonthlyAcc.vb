Imports DevExpress.Data
Imports System.IO

Public Class wStockBalanceMonthlyAcc

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _wStockBalanceMonthlyAccLIstDetail As wStockBalanceMonthlyAccLIstDetail

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _wStockBalanceMonthlyAccLIstDetail = New wStockBalanceMonthlyAccLIstDetail

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wStockBalanceMonthlyAccLIstDetail.Name.ToString.Trim, _wStockBalanceMonthlyAccLIstDetail)
        Catch ex As Exception
        Finally
        End Try
        HI.TL.HandlerControl.AddHandlerObj(_wStockBalanceMonthlyAccLIstDetail)

        With _wStockBalanceMonthlyAccLIstDetail
            .ogvdetail.OptionsView.ShowAutoFilterRow = False
        End With

    End Sub

    Private _LoadYear As String
    Property LoadYear As String
        Get
            Return _LoadYear
        End Get
        Set(value As String)
            _LoadYear = value
        End Set
    End Property

    Private _LoadMonth As String
    Property LoadMonth As String
        Get
            Return _LoadMonth
        End Get
        Set(value As String)
            _LoadMonth = value
        End Set
    End Property

#Region "Initial Grid"
    Private Sub InitGrid()


        With ogvtime
            For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                With oGridCol
                    .OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False

                    If HI.ST.SysInfo.Admin Then
                        .ToolTip = .Name.ToString
                    End If

                End With
            Next
            
            .OptionsView.ShowAutoFilterRow = False

        End With
        ''------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub


#End Region

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

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()

        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        Dim _FTWareHouse As String = ""
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        Me.LoadYear = Microsoft.VisualBasic.Right(FTYear.Text, 4)
        Me.LoadMonth = Microsoft.VisualBasic.Left(FTYear.Text, 2)

        Dim _Criteria As String = Me.IcAppCondition1.GetCriteria("WH", "", "", "", "")
        Dim _dtWH As DataTable
        _Qry = " SELECT FTWHCode "
        _Qry &= vbCrLf & "     FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WH WITH(NOLOCK)"
        _Qry &= vbCrLf & "     WHERE  FTWHCode <> '' "
        If _Criteria <> "" Then
            _Qry &= vbCrLf & "  AND  " & _Criteria
        End If

        _dtWH = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In _dtWH.Rows
            If _FTWareHouse = "" Then
                _FTWareHouse = R!FTWHCode.ToString
            Else
                _FTWareHouse = _FTWareHouse & "|" & R!FTWHCode.ToString
            End If
        Next

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_STOCKBAL_MONTHLY '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FTYear.Text), 7) & "','" & HI.UL.ULF.rpQuoted(_FTWareHouse) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Qry = " SELECT A.FNGrpSeq, A.FNSeq, A.FTDocType, SUM(A.FNMonth01) AS FNMonth01, SUM(A.FNMonth02) AS FNMonth02, SUM(A.FNMonth03) AS FNMonth03, SUM(A.FNMonth04) AS FNMonth04, SUM(A.FNMonth05) AS FNMonth05, "
        _Qry &= vbCrLf & "       SUM(A.FNMonth06) AS FNMonth06, SUM(A.FNMonth07) AS FNMonth07, SUM(A.FNMonth08) AS FNMonth08, SUM(A.FNMonth09) AS FNMonth09, SUM(A.FNMonth10) AS FNMonth10, SUM(A.FNMonth11) AS FNMonth11, "
        _Qry &= vbCrLf & "      SUM(A.FNMonth12) AS FNMonth12"
        _Qry &= vbCrLf & "  ,SUM(FNMonth01+FNMonth02+FNMonth03+FNMonth04+FNMonth05+FNMonth06+FNMonth07+FNMonth08+FNMonth09+FNMonth10+FNMonth11+FNMonth12) AS FNTotal"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockMonthly AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WH WITH(NOLOCK) ON A.FNHSysWHId = WH.FNHSysWHId"
        _Qry &= vbCrLf & " WHERE  (A.FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"

        If _Criteria <> "" Then
            _Qry &= vbCrLf & "  AND  " & _Criteria
        End If

        _Qry &= vbCrLf & " GROUP BY A.FNGrpSeq, A.FNSeq, A.FTDocType"
        _Qry &= vbCrLf & " ORDER BY A.FNGrpSeq, A.FNSeq"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockMonthly WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogdtime.DataSource = _dt
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTYear.Text <> "" Then
            _Pass = True
        End If


        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            Call InitGrid()
            IcAppCondition1.PrePareData()

            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvtime_DoubleClick(sender As Object, e As EventArgs) Handles ogvtime.DoubleClick
        Try
            With Me.ogvtime
                If Me.ogvtime.RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim _Month As String = ""
                Select Case .FocusedColumn.FieldName.ToString
                    Case "FNMonth01"
                        _Month = "01"
                    Case "FNMonth02"
                        _Month = "02"
                    Case "FNMonth03"
                        _Month = "03"
                    Case "FNMonth04"
                        _Month = "04"
                    Case "FNMonth05"
                        _Month = "05"
                    Case "FNMonth06"
                        _Month = "06"
                    Case "FNMonth07"
                        _Month = "07"
                    Case "FNMonth08"
                        _Month = "08"
                    Case "FNMonth09"
                        _Month = "09"
                    Case "FNMonth10"
                        _Month = "10"
                    Case "FNMonth11"
                        _Month = "11"
                    Case "FNMonth12"
                        _Month = "12"
                End Select

                If _Month <> "" Then
                    Dim _Qry As String = ""
                    Dim _dt As DataTable

                    _Qry = "  SELECT FTDocType,FNQuantity,FNAmount,FTYear,FTMonth"
                    _Qry &= vbCrLf & " FROM (SELECT FTDocType,Sum(FNQuantity) AS FNQuantity,Sum(FNAmount) AS FNAmount"
                    _Qry &= vbCrLf & "   ,Left(FTDocumentDate,4) AS FTYear"
                    _Qry &= vbCrLf & "   ,Right(Left(FTDocumentDate,7) ,2) AS FTMonth"
                    _Qry &= vbCrLf & "  FROM (SELECT FTDocumentNoRef "
                    _Qry &= vbCrLf & "  ,FNQuantity"
                    _Qry &= vbCrLf & "   ,FNAmount"
                    _Qry &= vbCrLf & "   ,ISNULL((SELECT TOP 1 FTDocumentDate "
                    _Qry &= vbCrLf & "   FROM     (SELECT FTReceiveNo AS FTDocumentNo,FDReceiveDate AS FTDocumentDate ,'RCV' AS FTDocType"
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK)"
                    _Qry &= vbCrLf & "    UNION"
                    _Qry &= vbCrLf & "   SELECT FTAdjustStockNo AS FTDocumentNo ,FDAdjustStockDate AS FTDocumentDate,'ADI' AS FTDocType"
                    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  ) AS X"
                    _Qry &= vbCrLf & "    WHERE X.FTDocumentNo = A.FTDocumentNoRef"
                    _Qry &= vbCrLf & "  ),'') AS FTDocumentDate"
                    _Qry &= vbCrLf & "   ,ISNULL((SELECT TOP 1 FTDocType  "
                    _Qry &= vbCrLf & "    FROM     (SELECT FTReceiveNo AS FTDocumentNo,FDReceiveDate AS FTDocumentDate ,'RCV' AS FTDocType"
                    _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS R WITH(NOLOCK)"
                    _Qry &= vbCrLf & "   UNION"
                    _Qry &= vbCrLf & "  SELECT FTAdjustStockNo AS FTDocumentNo ,FDAdjustStockDate AS FTDocumentDate,'ADI' AS FTDocType"
                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  ) AS X"
                    _Qry &= vbCrLf & "  WHERE X.FTDocumentNo = A.FTDocumentNoRef"
                    _Qry &= vbCrLf & "  ),'') AS FTDocType"
                    _Qry &= vbCrLf & "  FROM (SELECT  SUM(FNQuantity) AS FNQuantity"
                    _Qry &= vbCrLf & " ,SUM(FNAmount) AS FNAmount"
                    _Qry &= vbCrLf & " ,FTDocumentNoRef"
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockTransaction_Account_DocRef"
                    _Qry &= vbCrLf & "  WHERE FTUserLogIn=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "  AND  FTDocumentDate < '" & (Me.LoadYear & "/" & _Month & "/01") & "'"
                    _Qry &= vbCrLf & "  GROUP BY FTDocumentNoRef) AS A"
                    _Qry &= vbCrLf & "  WHERE FNQuantity <>0 OR FNAmount<>0) AS M"
                    _Qry &= vbCrLf & "  GROUP BY FTDocType"
                    _Qry &= vbCrLf & "  ,Left(FTDocumentDate,4) "
                    _Qry &= vbCrLf & "  ,Right(Left(FTDocumentDate,7) ,2) ) AS XX"
                    _Qry &= vbCrLf & " 	ORDER BY FTYear ASC ,FTMonth ASC,FTDocType ASC"

                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                    Call HI.ST.Lang.SP_SETxLanguage(_wStockBalanceMonthlyAccLIstDetail)
                    With _wStockBalanceMonthlyAccLIstDetail

                        With _wStockBalanceMonthlyAccLIstDetail
                            .ogvdetail.ActiveFilter.Clear()
                            .ogcdetail.DataSource = _dt.Copy
                            .ShowDialog()
                        End With
                    End With
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        Try
            With Me.ogvtime

                Select Case Integer.Parse(Val("" & .GetRowCellValue(e.RowHandle, "FNSeq").ToString))
                    Case 6
                        e.Appearance.ForeColor = Drawing.Color.Green
                        e.Appearance.BackColor = Drawing.Color.LightCyan
                        e.Appearance.Font = New Drawing.Font("Tahoma", 8, Drawing.FontStyle.Bold)
                    Case 18
                        e.Appearance.ForeColor = Drawing.Color.Red
                        e.Appearance.BackColor = Drawing.Color.LightCyan
                        e.Appearance.Font = New Drawing.Font("Tahoma", 8, Drawing.FontStyle.Bold)
                    Case 19
                        e.Appearance.ForeColor = Drawing.Color.Blue
                        e.Appearance.BackColor = Drawing.Color.LightCyan
                        e.Appearance.Font = New Drawing.Font("Tahoma", 8, Drawing.FontStyle.Bold)
                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class