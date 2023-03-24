Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraTab

Public Class wDCPreparePackingList

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")


    Private AddForm As wDCPreparePackingListAddItems
    Private _ListDataMarkBreakDown As List(Of DataTable)
    Private _ListPurchaseOrder As New List(Of DataTable)
    Private _StateSubNew As Boolean = False
    Private _TFNMarkSpare As Double = 2.0
    Private LoadTmpJob As String = ""
    Private _ProcLoad As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        AddForm = New wDCPreparePackingListAddItems
        HI.TL.HandlerControl.AddHandlerObj(AddForm)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, AddForm.Name.ToString.Trim, AddForm)
        Catch ex As Exception
        Finally
        End Try
        LoadTmpJob = ""

    End Sub

#Region "Property"
    Private _WHID As Integer
    Public Property WH As Integer
        Get
            Return _WHID
        End Get
        Set(value As Integer)
            _WHID = value
        End Set
    End Property

    Private _WHIDTo As Integer
    Public Property WHTo As Integer
        Get
            Return _WHIDTo
        End Get
        Set(value As Integer)
            _WHIDTo = value
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

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _mPriceClosed1 As Double = -1
    Public Property PriceClosed1 As Double
        Get
            Return _mPriceClosed1
        End Get
        Set(value As Double)
            _mPriceClosed1 = value
        End Set
    End Property


    Private _mPriceClosed2 As Double = -1
    Public Property PriceClosed2 As Double
        Get
            Return _mPriceClosed2
        End Get
        Set(value As Double)
            _mPriceClosed2 = value
        End Set
    End Property

    Private _OrderNoTo As String = ""
    Public Property OrderNoTo As String
        Get
            Return _OrderNoTo
        End Get
        Set(value As String)
            _OrderNoTo = value
        End Set
    End Property

    Private _DocRefNo As String = ""
    Public Property DocRefNo As String
        Get
            Return _DocRefNo
        End Get
        Set(value As String)
            _DocRefNo = value
        End Set
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(value As String)
            _MainKeyID = value
        End Set
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(value As String)
            _RequireField = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub LoadOrderProdBreakDown(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_GET_PACKINGLIST_DETAIL '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        With Me.ogvbd

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPLLine".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPLLine".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
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

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            Select Case Col.ColumnName.ToString
                                Case "Pack/Carton"
                                    .Columns(Col.ColumnName.ToString).Width = 100
                                Case Else
                                    .Columns(Col.ColumnName.ToString).Width = 60
                            End Select

                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcbd.DataSource = _dt.Copy

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_GET_PACKINGLIST_DETAIL_PACK '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _colcount = 0
        With Me.ogvbdpl

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPLLine".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPLLine".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
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

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 60


                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcbdpl.DataSource = _dt.Copy

        Call LoadMaterial(Key.ToString)
    End Sub


    Private Sub LoadMaterial(PLKey As String)
        Dim cmdstring As String = ""

        cmdstring = "   Select  A.FTDCPLNo "
        cmdstring &= vbCrLf & " , A.FNHSysRawmatId "
        cmdstring &= vbCrLf & "	, A.FNQuantity "
        cmdstring &= vbCrLf & "	, A.FTPONo "
        cmdstring &= vbCrLf & ", A.FNPOQuantity "
        cmdstring &= vbCrLf & ", A.FTRSVNo "
        cmdstring &= vbCrLf & "	, A.FNRSVQuantity "
        cmdstring &= vbCrLf & "	, A.FTRemark "
        cmdstring &= vbCrLf & "	,IM.FTRawMatCode "
        cmdstring &= vbCrLf & "	,IM.FTRawMatNameEN AS FTRawMatName  "
        cmdstring &= vbCrLf & ",IM .FNHSysRawMatColorId "
        cmdstring &= vbCrLf & ",IM.FNHSysRawMatSizeId "
        cmdstring &= vbCrLf & ",IM.FNHSysUnitId "
        cmdstring &= vbCrLf & ",IMC.FTRawMatColorCode"
        cmdstring &= vbCrLf & ",IMS.FTRawMatSizeCode"
        cmdstring &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Rawmat AS A WITH(NOLOCK)"
        cmdstring &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With(NOLOCK) On A.FNHSysRawMatId = IM.FNHSysRawMatId"
        cmdstring &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 FTRawMatColorCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) WHERE IMC.FNHSysRawMatColorId =IM.FNHSysRawMatColorId) AS IMC"
        cmdstring &= vbCrLf & "	OUTER APPLY(Select TOP 1 FTRawMatSizeCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As IMS With(NOLOCK) WHERE IMS.FNHSysRawMatSizeId =IM.FNHSysRawMatSizeId) As IMS"
        cmdstring &= vbCrLf & " WHERE(A.FTDCPLNo = N'" & HI.UL.ULF.rpQuoted(PLKey) & "')  "
        cmdstring &= vbCrLf & " Order By IM.FTRawMatCode"

        Dim dt As New DataTable

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

        ogcrawmat.DataSource = dt.Copy

        cmdstring = "   Select '0' AS FTSelect "
        cmdstring &= vbCrLf & ", A.FNSeq "
        cmdstring &= vbCrLf & " ,A.FTPLBarcodeNo "
        cmdstring &= vbCrLf & " ,A.FTColorway  "
        cmdstring &= vbCrLf & " ,A.FTSizeBreakDown  "
        cmdstring &= vbCrLf & " ,A.FNQuantity "
        cmdstring &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcode As A WITH(NOLOCK) "
        cmdstring &= vbCrLf & " Where A.FTDCPLNo = N'" & HI.UL.ULF.rpQuoted(PLKey) & "' "
        cmdstring &= vbCrLf & " ORder BY A.FNSeq "

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_INVEN)

        ogcbarcodepl.DataSource = dt.Copy
    End Sub

    Private Sub LoadOrderProdSubBreakDown(OrderProdNo As Object, SubOrderNo As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Get_OrderProdSubBreakDown '" & HI.UL.ULF.rpQuoted(OrderProdNo.ToString) & "','" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
        With _dt
            .Columns.Add("FTNikePOLineItem", GetType(String))
        End With

        With Me.ogvbd

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
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
                                .Visible = True
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

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With

        Me.ogcbd.DataSource = _dt
    End Sub



    Private Sub InitGrid()

        With ogvbd
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.EnableAppearanceFocusedRow = False
            .OptionsSelection.EnableAppearanceFocusedCell = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        With ogvbdpl
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.EnableAppearanceFocusedRow = False
            .OptionsSelection.EnableAppearanceFocusedCell = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        'With ogvbarcodepl
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

    End Sub

    Private Sub ClearGrid(Optional Prod As Boolean = False)

        With Me.ogvbd
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

        With Me.ogvbdpl
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

        'With Me.ogvbarcodepl
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1

        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select

        '    Next
        'End With



        If Not (Prod) Then
            Me.otbjobprod.TabPages.Clear()
        End If

        Me.ogcrawmat.DataSource = Nothing
        ogcbarcodepl.DataSource = Nothing
    End Sub

#End Region

    Private Sub LoadDataOrderInfo(OrderKey As String)
        Dim cmdstring As String



    End Sub

#Region "MAIN PROC"


    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else

            Call LoadOrderProdDataInfo(FTOrderNo.Text)
            Me.otbdetail.SelectedTabPageIndex = 0
        End If
    End Sub


    Public Sub SetInfo(ByVal Key As Object)
        '...call by another form name zzz...
        FTOrderNo.Text = Key.ToString
    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)
        If (_StateSubNew) Then Exit Sub
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        Call ClearGrid()
        otbjobprod.TabPages.Clear()


        FNHSysCmpId.Text = ""
        FNHSysStyleId.Text = ""
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = "SELECT TOP 1 A.FTOrderNo,Cmp.FTCmpCode,ST.FTStyleCode "
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As  A With(NOLOCK) "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECt TOP 1 FTCmpCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) WHERE Cmp.FNHSysCmpId = A.FNHSysCmpId )	 AS Cmp "
        _Qry &= vbCrLf & "  OUTER APPLY (Select TOP 1 FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) WHERE ST.FNHSysStyleId = A.FNHSysStyleId )	 As ST "
        _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
        For Each R As DataRow In _dtprod.Rows

            FNHSysCmpId.Text = R!FTCmpCode.ToString()
            FNHSysStyleId.Text = R!FTStyleCode.ToString()

        Next


        _Qry = "Select FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTDCPLNo  "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList As P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Order By FTDCPLNo,LEN(FTDCPLNo)  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTDCPLNo.ToString
                .Text = R!FTDCPLNo.ToString
            End With

            otbjobprod.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            Try
                otbjobprod.SelectedTabPageIndex = (_dtprod.Rows.Count - 1)
            Catch ex As Exception

            End Try

        End If

        _dtprod.Dispose()

        _Spls.Close()
    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbjobprod.SelectedTabPage Is Nothing) Then


                    Call LoadOrderProdBreakDown(otbjobprod.SelectedTabPage.Name.ToString)

                Else
                    Call ClearGrid(True)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDataInfo(FTOrderNo.Text)
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub


    Private Sub ocmaddsuborder_Click(sender As Object, e As EventArgs) Handles ocmaddsuborder.Click

        If FTOrderNo.Text <> "" Then

            If LoadTmpJob <> FTOrderNo.Text Then
                Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GETTMPMRP_RAWMATPL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                LoadTmpJob = FTOrderNo.Text
            End If

            With AddForm
                .StateOK = False
                .FTSubOrderNo.Text = ""
                .FTOrderNo.Text = FTOrderNo.Text
                .ogcbarcode.DataSource = Nothing
                .ShowDialog()

                If .StateOK Then
                    Call LoadOrderProdDataInfo(FTOrderNo.Text)
                End If

            End With

        End If

    End Sub

    Private Sub wDCPreparePackingList_Load(sender As Object, e As EventArgs) Handles Me.Load
        InitGrid()
    End Sub

    Private Sub otbjobprod_Click(sender As Object, e As EventArgs) Handles otbjobprod.Click

    End Sub

    Private Sub ocmdeletesuborder_Click(sender As Object, e As EventArgs) Handles ocmdeletesuborder.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then


            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบ Order Producttion ใช่หรือไม่ ?", 1477780003, Me.otbjobprod.SelectedTabPage.Text) = True Then

                If Me.DeleteOrderPL(Me.otbjobprod.SelectedTabPage.Text) Then
                    Call LoadOrderProdDataInfo(FTOrderNo.Text)
                End If

            End If


        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Order Producttion ที่ต้องการลบ ", 1404180001, Me.Text)
        End If
    End Sub


    Private Function DeleteOrderPL(PLKey As Object) As Boolean


        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList  WHERE FTDCPLNo='" & HI.UL.ULF.rpQuoted(PLKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry &= vbCrLf & " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Breakdown"
            _Qry &= vbCrLf & " WHERE FTDCPLNo='" & HI.UL.ULF.rpQuoted(PLKey.ToString) & "' "
            _Qry &= vbCrLf & " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_Rawmat"
            _Qry &= vbCrLf & " WHERE FTDCPLNo='" & HI.UL.ULF.rpQuoted(PLKey.ToString) & "' "
            _Qry &= vbCrLf & " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcode"
            _Qry &= vbCrLf & " WHERE FTDCPLNo='" & HI.UL.ULF.rpQuoted(PLKey.ToString) & "' "
            _Qry &= vbCrLf & "  Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM"
            _Qry &= vbCrLf & " WHERE FTDCPLNo='" & HI.UL.ULF.rpQuoted(PLKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList  WHERE FTDCPLNo='" & HI.UL.ULF.rpQuoted(PLKey.ToString) & "' ")

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged

        ocmpreview.Visible = (otbdetail.SelectedTabPage.Name = otporderplbarcode.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Dim cmd As String
        Dim dtselectbar As DataTable

        With CType(ogcbarcodepl.DataSource, DataTable)
            .AcceptChanges()

            dtselectbar = .Copy
        End With

        If dtselectbar.Select("FTSelect='1'").Length > 0 Then

            cmd = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_INVEN)

            For Each R As DataRow In dtselectbar.Select("FTSelect='1'")

                cmd = " Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp "
                cmd &= vbCrLf & " SET FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTPLBarcodeNo.ToString) & "'"
                cmd &= vbCrLf & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTPLBarcodeNo.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = False Then

                    cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp (FTUserLogIn,FTBarcodeNo) "
                    cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTPLBarcodeNo.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN)

                End If

            Next

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "DCPL_Barcode.rpt"
                .Formular = " {TINVENPrintBarcode_Temp.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                .Preview()
            End With

        Else
        End If

        dtselectbar.Dispose()

    End Sub


#End Region


End Class