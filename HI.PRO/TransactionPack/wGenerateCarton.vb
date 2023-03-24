Imports System.Drawing

Public Class wGenerateCarton


    Private _ListDataPackOrg As New List(Of DataTable)
    Private _ListDataSubOrderPack As New List(Of DataTable)
    Private _ListDataSubOrderBal As New List(Of DataTable)
    Private _ListDataSubOrderBlank As New List(Of DataTable)

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()
    End Sub


#Region "Property"

    Private _FTPackNo As String = ""
    Property FTPackNo As String
        Get
            Return _FTPackNo
        End Get
        Set(value As String)
            _FTPackNo = value
        End Set
    End Property

    Private _FNHSysStyleId As Integer = 0
    Property FNHSysStyleId As Integer
        Get
            Return _FNHSysStyleId
        End Get
        Set(value As Integer)
            _FNHSysStyleId = value
        End Set
    End Property

    Private _FNPackSetValue As Integer = 0
    Property FNPackSetValue As Integer
        Get
            Return _FNPackSetValue
        End Get
        Set(value As Integer)
            _FNPackSetValue = value
        End Set
    End Property

    Private _Process As Boolean = False
    Property Process As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property

    Property ListDataPackOrg As DataTable
        Get
            Return _ListDataPackOrg(0)
        End Get
        Set(value As DataTable)
            _ListDataPackOrg.Clear()
            _ListDataPackOrg.Add(value.Copy)
        End Set
    End Property

    Private _ObjectParent As Object = Nothing
    Public Property ObjectParent As Object
        Get
            Return _ObjectParent
        End Get
        Set(value As Object)
            _ObjectParent = value
        End Set
    End Property
#End Region

#Region "Procedure"
    Private Sub InitGrid()

        Try
            With Me.ogvsubprodpack
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowEdit = False
                    .Columns(I).AppearanceCell.BackColor = Color.White
                    .Columns(I).AppearanceCell.ForeColor = Color.Black
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception

        End Try

        Try
            With Me.ogvsubprod
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(I).OptionsColumn.AllowEdit = False
                    .Columns(I).AppearanceCell.BackColor = Color.White
                    .Columns(I).AppearanceCell.ForeColor = Color.Black
                    .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        With ogvsubprodpack
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        With ogvsubprod
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

    End Sub

    Private Sub LoadPackDefault()

    End Sub

    Private Sub CreateBreakDownForPack(dt As DataTable)
        Dim _colcount As Integer = 0

     

        With dt
            For Each R As DataRow In .Rows
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            R.Item(Col) = 0
                    End Select
                Next
            Next
        End With

        With Me.ogvsubprodpack

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        .Columns(I).AppearanceCell.BackColor = Color.White
                        .Columns(I).AppearanceCell.ForeColor = Color.Black
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (dt Is Nothing) Then
                For Each Col As DataColumn In dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG

 
                                If Col.ColumnName.ToString.ToUpper = "Total".ToUpper Then
                                    .Visible = False
                                Else
                                    .Visible = True
                                End If

                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                                .ColumnEdit = RepSize
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

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next

            End If

        End With

        Me.ogcsubprodpack.DataSource = dt.Copy
    End Sub

    Private Sub LoadOrderPackBreakDownCreateCarton(Key As Object)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dtpack = _dt.Copy

        With Me.ogvsubprod

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
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

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next

            End If


        End With

        Me.ogcsubprod.DataSource = _dt.Copy

        Call CreateBreakDownForPack(_dtpack)

        _dt.Dispose()
        _dtpack.Dispose()

        FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)
        Call SetDefaultSetPack()

        ' FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)

    End Sub

    Private Sub SetDefaultSetPack()

        Try

            Dim _Qry As String = ""
            Dim _PackPerCarton As Integer
            Dim _SubOrder As String = ""
            Dim _dt As DataTable
            Dim _dt2 As DataTable

            _Qry = "SELECT TOP 1 FTSubOrderNo "
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  (A.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "')"
            _SubOrder = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")


            _Qry = "SELECT TOP 1 B.FNPackPerCarton,B.FNPackCartonSubType"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  "
            _Qry &= vbCrLf & "  WHERE  (B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_SubOrder) & "')"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            _PackPerCarton = -1
            For Each R As DataRow In _dt.Rows

                FNPackCartonSubType.SelectedIndex = Integer.Parse(Val(R!FNPackCartonSubType.ToString))
                FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)
                _PackPerCarton = Integer.Parse(Val(R!FNPackPerCarton.ToString))
                Me.FNPackPerCaton.Value = _PackPerCarton
                If Integer.Parse(Val(R!FNPackCartonSubType.ToString)) = 0 Then
                    ' _PackPerCarton = Integer.Parse(Val(R!FNPackPerCarton.ToString))
                Else

                    _Qry = " SELECT FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity "
                    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Bundle AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE  (FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_SubOrder) & "')"

                    _dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    With CType(ogcsubprodpack.DataSource, DataTable)
                        .AcceptChanges()
                        Dim dtpack As DataTable = .Copy
                        For Each R2 In _dt2.Rows
                            For Each R3 As DataRow In dtpack.Select("FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R2!FTSubOrderNo.ToString) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(R2!FTColorway.ToString) & "'")
                                Try
                                    R3.Item(R2!FTSizeBreakDown.ToString) = Integer.Parse(Val(R2!FNQuantity.ToString))
                                Catch ex As Exception
                                End Try
                            Next
                        Next
                        Dim _Total As Integer = 0
                        For Each R3 As DataRow In dtpack.Rows
                            _Total = 0

                            For Each Col As DataColumn In dtpack.Columns
                                Select Case Col.ColumnName.ToString.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                    Case Else
                                        _Total = _Total + Integer.Parse(Val(R3.Item(Col).ToString))
                                End Select

                            Next

                            R3!Total = _Total
                        Next
                       

                        ogcsubprodpack.DataSource = dtpack
                        ' .AcceptChanges()
                    End With

                End If
            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysCartonId.Text <> "" And Me.FNHSysCartonId.Properties.Tag.ToString <> "" Then
            If FNPackPerCaton.Value > 0 Then

                Dim _PackValue As Integer = FNPackPerCaton.Value

                Select Case FNPackCartonSubType.SelectedIndex
                    Case 0
                        _Pass = True
                    Case 1
                        With CType(Me.ogcsubprodpack.DataSource, DataTable)
                            .AcceptChanges()

                            Dim R1() As DataRow = .Select("Total>0")
                            Dim R2() As DataRow = .Select("Total=" & _PackValue & "")

                            If R2.Length > 0 Then

                                If R2.Length = R1.Length Then
                                    _Pass = True
                                Else
                                    HI.MG.ShowMsg.mInfo("คุณทำการระบุ สีไซส์ ไม่ครบตามจำนวน Pack ต่อ กล่อง กรุณาทำการตรวจสอบ !!!", 1411150139, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                                End If

                            Else
                                HI.MG.ShowMsg.mInfo("คุณทำการระบุ สีไซส์ ไม่ครบตามจำนวน Pack ต่อ กล่อง กรุณาทำการตรวจสอบ !!!", 1411150139, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                            End If

                        End With
                    Case 2
                        With CType(Me.ogcsubprodpack.DataSource, DataTable)
                            .AcceptChanges()

                            Dim _TotalPack As Integer = 0
                            Dim _SumPack As Integer = 0
                            Dim _StatePack As Boolean = True

                            For Each Col As DataColumn In .Columns
                                _SumPack = 0
                                Select Case Col.ColumnName.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                    Case Else
                                        For Each R As DataRow In .Rows
                                            _TotalPack = _TotalPack + Val(R.Item(Col))
                                            _SumPack = _SumPack + Val(R.Item(Col))
                                        Next

                                        If _SumPack > 0 Then
                                            If _SumPack <> _PackValue Then
                                                _StatePack = False
                                            End If
                                        End If

                                End Select


                            Next
 
                            If _TotalPack > 0 Then
                                If (_StatePack) Then
                                    _Pass = True
                                Else
                                    HI.MG.ShowMsg.mInfo("คุณทำการระบุ สีไซส์ ไม่ครบตามจำนวน Pack ต่อ กล่อง กรุณาทำการตรวจสอบ !!!", 1411150139, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                                End If

                            Else
                                HI.MG.ShowMsg.mInfo("คุณทำการระบุ สีไซส์ ไม่ครบตามจำนวน Pack ต่อ กล่อง กรุณาทำการตรวจสอบ !!!", 1411150139, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                            End If

                        End With
                    Case 3
                        With CType(Me.ogcsubprodpack.DataSource, DataTable)
                            .AcceptChanges()
                            Dim _TotalPack As Integer = 0
                            For Each R As DataRow In .Rows
                                For Each Col As DataColumn In .Columns

                                    Select Case Col.ColumnName.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                        Case Else
                                            If Val(Val(R.Item(Col))) > 0 Then
                                                _TotalPack = _TotalPack + Val(R.Item(Col))
                                            End If

                                    End Select

                                Next
                            Next

                            If _TotalPack > 0 Then
                                If (_TotalPack = _PackValue) Then
                                    _Pass = True
                                Else
                                    HI.MG.ShowMsg.mInfo("คุณทำการระบุ สีไซส์ ไม่ครบตามจำนวน Pack ต่อ กล่อง กรุณาทำการตรวจสอบ !!!", 1411150139, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                                End If

                            Else
                                HI.MG.ShowMsg.mInfo("คุณทำการระบุ สีไซส์ ไม่ครบตามจำนวน Pack ต่อ กล่อง กรุณาทำการตรวจสอบ !!!", 1411150139, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                            End If

                        End With

                End Select

            Else
                HI.MG.ShowMsg.mInfo("กรุณมทำการระบุจำนวน Pack ต่อ กล่อง !!!", 1411150129, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysCartonId_lbl.Text)
            FNHSysCartonId.Focus()
        End If
        Return _Pass
    End Function

    Private Function CreateCarton() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")
        Try


            Dim _LastCartonNo As Integer = 0
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _POLine As String = ""

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy

            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            With _dt

                .BeginInit()

                For Each R As DataRow In .Rows

                    _FTOrderNo = R!FTOrderNo.ToString()
                    _FTSubOrderNo = R!FTSubOrderNo.ToString()
                    _FTColorway = R!FTColorway.ToString()
                    _POLine = R!FTPOLine.ToString

                    For Each Col As DataColumn In .Columns
                        Select Case Col.ColumnName.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                            Case Else
                                _SizeQty = Integer.Parse(Val(R.Item(Col)))
                                If _SizeQty >= _PackQty Then

                                    Do While _SizeQty >= _PackQty
                                        _LastCartonNo = _LastCartonNo + 1

                                        _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                        _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                        _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton , FTPOLine)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                        _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & _PackQty & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                        _Qry &= vbCrLf & "," & _PackQty & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        _SizeQty = _SizeQty - _PackQty
                                        If _SizeQty <= 0 Then
                                            _SizeQty = 0
                                        End If

                                    Loop
                                End If


                                If Me.FNPackCartonScrapType.SelectedIndex = 1 Then
                                    If _SizeQty > 0 Then
                                        _LastCartonNo = _LastCartonNo + 1

                                        _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                        _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                        _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton, FTPOLine)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                        _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & _SizeQty & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                        _Qry &= vbCrLf & "," & _SizeQty & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "


                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        _SizeQty = 0
                                    End If
                                End If
                                R.Item(Col) = _SizeQty
                        End Select
                    Next
                Next

                If Me.FNPackCartonScrapType.SelectedIndex = 0 Then
                    For Each R As DataRow In .Rows

                        _FTOrderNo = R!FTOrderNo.ToString()
                        _FTSubOrderNo = R!FTSubOrderNo.ToString()
                        _FTColorway = R!FTColorway.ToString()
                        _POLine = R!FTPOLine.ToString

                        For Each Col As DataColumn In .Columns
                            Select Case Col.ColumnName.ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                Case Else
                                    _SizeQty = Integer.Parse(Val(R.Item(Col)))
                                    If _SizeQty > 0 Then

                                        _LastCartonNo = _LastCartonNo + 1

                                        _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                        _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                        _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton ,FTPOLine)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                        _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & _SizeQty & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                        _Qry &= vbCrLf & "," & _PackQty & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "

                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    End If
                                    R.Item(Col) = 0
                            End Select
                        Next
                    Next
                End If


                .EndInit()

            End With

            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function

    Private Function CreateCartonAssortOneColorMultiSize() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")
        Try


            Dim _LastCartonNo As Integer = 0
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _POLine As String = ""

            Dim _dtpack As DataTable
            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
            End With

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy

            Dim dtpackassort As New DataTable
            dtpackassort.Columns.Add("FTSizeCode", GetType(String))
            dtpackassort.Columns.Add("FNQuantity", GetType(Integer))

            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0
            For Each Row As DataRow In _dtpack.Select("Total=" & FNPackPerCaton.Value & "")

                _FTOrderNo = Row!FTOrderNo.ToString()
                _FTSubOrderNo = Row!FTSubOrderNo.ToString()
                _FTColorway = Row!FTColorway.ToString()
                _POLine = Row!FTPOLine.ToString


                dtpackassort.Rows.Clear()

                For Each ColP As DataColumn In _dtpack.Columns

                    Select Case ColP.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            _SizePackQty = Integer.Parse(Val(Row.Item(ColP)))

                            If _SizePackQty > 0 Then
                                dtpackassort.Rows.Add(ColP.ColumnName.ToString, _SizePackQty)
                            End If

                    End Select
                Next

                If dtpackassort.Rows.Count > 0 Then
                    Dim _Str As String = " FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                    For Each R As DataRow In dtpackassort.Rows

                        If _Str = "" Then
                            _Str = "[" & R!FTSizeCode.ToString & "]>=" & R!FNQuantity.ToString & ""
                        Else
                            _Str &= " AND [" & R!FTSizeCode.ToString & "]>=" & R!FNQuantity.ToString & ""
                        End If

                    Next

                    With _dt
                        .BeginInit()

                        Do While .Select(_Str).Length > 0
                            For Each Rx As DataRow In .Select(_Str)
                                _LastCartonNo = _LastCartonNo + 1

                                For Each Col As DataColumn In .Columns
                                    Select Case Col.ColumnName.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                        Case Else

                                            _SizePackQty = 0
                                            For Each Rxp As DataRow In dtpackassort.Select("FTSizeCode='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "'")
                                                _SizePackQty = Integer.Parse(Val(Rxp!FNQuantity.ToString))
                                            Next

                                            If _SizePackQty > 0 Then
                                                _SizeQty = Integer.Parse(Val(Rx.Item(Col)))

                                                _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                                _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                                _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton,FTPOLine)"
                                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                                _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                                _Qry &= vbCrLf & "," & _SizePackQty & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "


                                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                                _SizeQty = _SizeQty - _SizePackQty
                                                If _SizeQty <= 0 Then
                                                    _SizeQty = 0
                                                End If

                                                Rx.Item(Col) = _SizeQty
                                            End If

                                    End Select

                                Next

                            Next
                        Loop

                        .EndInit()
                    End With
                End If
            Next

            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)

        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function

    Private Function PivotTable(oldTable As DataTable,
                            Optional pivotColumnOrdinal As Integer = 0
                           ) As DataTable
        Dim newTable As New DataTable
        Dim dr As DataRow

        ' add pivot column name
        newTable.Columns.Add(oldTable.Columns(pivotColumnOrdinal).ColumnName)

        ' add pivot column values in each row as column headers to new Table
        For Each row In oldTable.Rows
            newTable.Columns.Add(row(pivotColumnOrdinal))
        Next

        ' loop through columns
        For col = 0 To oldTable.Columns.Count - 1
            'pivot column doen't get it's own row (it is already a header)
            If col = pivotColumnOrdinal Then Continue For

            ' each column becomes a new row
            dr = newTable.NewRow()

            ' add the Column Name in the first Column
            dr(0) = oldTable.Columns(col).ColumnName

            ' add data from every row to the pivoted row
            For row = 0 To oldTable.Rows.Count - 1
                dr(row + 1) = oldTable.Rows(row)(col)
            Next

            'add the DataRow to the new table
            newTable.Rows.Add(dr)
        Next

        Return newTable
    End Function

    Private Function CreateCartonAssortOneSizeMultiColor() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")
        Try


            Dim _LastCartonNo As Integer = 0
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _POLine As String = ""
            Dim _dtpack As DataTable
            Dim _dtpacktmp As DataTable
            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
                _dtpacktmp = .Copy
            End With

            Dim _TotalPack As Integer = 0
            Dim _SumPack As Integer = 0

            Try
                With _dtpacktmp

                    _dtpack.Columns.Remove("Total")

                    For Each Col As DataColumn In .Columns
                        _SumPack = 0
                        Select Case Col.ColumnName.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                            Case Else
                                For Each R As DataRow In .Rows
                                    _TotalPack = _TotalPack + Val(R.Item(Col))
                                    _SumPack = _SumPack + Val(R.Item(Col))
                                Next

                                If _SumPack > 0 Then
                                    If _SumPack <> _PackQty Then
                                        _dtpack.Columns.Remove(Col.ColumnName)
                                    End If
                                Else
                                    _dtpack.Columns.Remove(Col.ColumnName)
                                End If

                        End Select

                    Next
                End With
            Catch ex As Exception
            End Try

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy

            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))
            _LastCartonNo = _LastCartonNo + 1
            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0
            Dim _StateIns As Boolean = True
            For Each Col As DataColumn In _dtpack.Columns
                _SumPack = 0
                _StateIns = True

                Do While (_StateIns)
                    _Qry = ""

                    Select Case Col.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToString
                        Case Else

                            For Each R As DataRow In _dtpack.Rows

                                _FTOrderNo = R!FTOrderNo.ToString()
                                _FTSubOrderNo = R!FTSubOrderNo.ToString()
                                _FTColorway = R!FTColorway.ToString()
                                _POLine = R!FTPOLine.ToString

                                If Val(R.Item(Col)) > 0 Then
                                    _PackQty = Val(R.Item(Col))
                                    Dim _Str As String = " FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' AND [" & Col.ColumnName.ToString & "]>=" & _PackQty & ""
                                    Dim tmpRow() As DataRow = _dt.Select(_Str)
                                    If tmpRow.Length > 0 Then
                                        For Each Rxp As DataRow In tmpRow
                                            _SizePackQty = Integer.Parse(Val(Rxp.Item(Col.ColumnName)))
                                            If _SizePackQty >= _PackQty Then

                                                _SizeQty = Integer.Parse(Val(Rxp.Item(Col.ColumnName)))

                                                _Qry &= vbCrLf & " Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                                _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                                _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton,FTPOLine)"
                                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                                _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                _SizeQty = _SizeQty - _PackQty
                                                If _SizeQty <= 0 Then
                                                    _SizeQty = 0
                                                End If

                                                Rxp.Item(Col.ColumnName) = _SizeQty

                                            End If

                                        Next
                                    Else
                                        _StateIns = False
                                    End If

                                End If

                            Next

                    End Select

                    If _Qry = "" Then
                        _StateIns = False
                    End If

                    If (_StateIns) And _Qry <> "" Then
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                        _LastCartonNo = _LastCartonNo + 1

                    End If
                Loop
            Next

            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function

    Private Function CreateCartonAssortMultiColorMultiSize() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")
        Try


            Dim _LastCartonNo As Integer = 0
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""

            Dim _POLine As String = ""
            Dim _dtpack As DataTable
            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
            End With

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy

            Dim dtpackassort As New DataTable
            dtpackassort.Columns.Add("FTOrderNo", GetType(String))
            dtpackassort.Columns.Add("FTSubOrderNo", GetType(String))
            dtpackassort.Columns.Add("FTColorCode", GetType(String))
            dtpackassort.Columns.Add("FTPOLine", GetType(String))
            dtpackassort.Columns.Add("FTSizeCode", GetType(String))
            dtpackassort.Columns.Add("FNQuantity", GetType(Integer))

            dtpackassort.Rows.Clear()
            Dim _SizePackQty As Integer = 0
            For Each Row As DataRow In _dtpack.Rows
                _FTOrderNo = Row!FTOrderNo.ToString()
                _FTSubOrderNo = Row!FTSubOrderNo.ToString()
                _FTColorway = Row!FTColorway.ToString()
                _POLine = Row!FTPOLine.ToString

                For Each ColP As DataColumn In _dtpack.Columns

                    Select Case ColP.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            _SizePackQty = Integer.Parse(Val(Row.Item(ColP)))

                            If _SizePackQty > 0 Then
                                dtpackassort.Rows.Add(_FTOrderNo, _FTSubOrderNo, _FTColorway, _POLine, ColP.ColumnName.ToString, _SizePackQty)
                            End If

                    End Select
                Next
            Next
            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            _LastCartonNo = _LastCartonNo + 1
            Dim _StateIns As Boolean = True
            Dim _SumPack As Integer = 0
            Dim _FTSizeCode As String = ""

            If dtpackassort.Rows.Count > 0 Then
                _SumPack = 0
                _StateIns = True

                Do While (_StateIns)
                    _Qry = ""
                    _SumPack = 0

                    For Each R As DataRow In dtpackassort.Rows
                        _FTOrderNo = R!FTOrderNo.ToString()
                        _FTSubOrderNo = R!FTSubOrderNo.ToString()
                        _FTColorway = R!FTColorCode.ToString()
                        _FTSizeCode = R!FTSizeCode.ToString
                        _POLine = R!FTPOLine.ToString

                        If Val(R!FNQuantity.ToString) > 0 Then
                            _PackQty = Val(R!FNQuantity.ToString)


                            Dim _Str As String = " FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' AND [" & _FTSizeCode & "]>=" & _PackQty & ""
                            Dim tmpRow() As DataRow = _dt.Select(_Str)
                            If tmpRow.Length > 0 Then
                                For Each Rxp As DataRow In tmpRow

                                    _SizePackQty = Integer.Parse(Val(Rxp.Item(_FTSizeCode)))
                                    If _SizePackQty >= _PackQty Then

                                        _SumPack = _SumPack + _PackQty

                                        _SizeQty = Integer.Parse(Val(Rxp.Item(_FTSizeCode)))

                                        _Qry &= vbCrLf & " Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                        _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                        _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton , FTPOLine)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                        _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSizeCode) & "' "
                                        _Qry &= vbCrLf & "," & _PackQty & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                        _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "'"

                                        _SizeQty = _SizeQty - _PackQty
                                        If _SizeQty <= 0 Then
                                            _SizeQty = 0
                                        End If

                                        Rxp.Item(_FTSizeCode) = _SizeQty


                                    End If

                                Next
                            Else
                                _StateIns = False
                            End If

                        End If
                    Next

                    If _SumPack <> FNPackPerCaton.Value Then
                        _StateIns = False
                    End If

                    If _Qry = "" Then
                        _StateIns = False
                    End If

                    If (_StateIns) And _Qry <> "" Then
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                        _LastCartonNo = _LastCartonNo + 1
                    End If

                Loop

            End If

            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)

        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function

#End Region

    Private Sub wGenerateCarton_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call InitGrid()
        Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)
        HI.TL.HandlerControl.ClearControl(Me.ogbcarton)
        Call SetDefaultSetPack()

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Process = False
        Me.Close()
    End Sub

    Private Sub FNPackCartonSubType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNPackCartonSubType.SelectedIndexChanged
        Try
            Dim _StateEdit As Boolean = False

            Select Case FNPackCartonSubType.SelectedIndex
                Case 0
                    _StateEdit = False
                    FNPackCartonScrapType.SelectedIndex = 0
                    FNPackCartonScrapType.Enabled = True
                Case Else
                    _StateEdit = True
                    FNPackCartonScrapType.SelectedIndex = 2
                    FNPackCartonScrapType.Enabled = False
            End Select

            Try
                With CType(Me.ogcsubprodpack.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Rows
                        For Each Col As DataColumn In .Columns
                            Select Case Col.ColumnName.ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                                Case Else
                                    R.Item(Col) = 0
                            End Select
                        Next
                    Next
                    .AcceptChanges()
                End With
            Catch ex As Exception

            End Try
            With Me.ogvsubprodpack
                .BeginInit()
                For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    Select Case oGridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            With oGridCol

                                .OptionsColumn.AllowEdit = _StateEdit
                                .OptionsColumn.ReadOnly = Not (_StateEdit)

                                If _StateEdit Then
                                    .AppearanceCell.BackColor = Color.LightCyan
                                    .AppearanceCell.ForeColor = Color.Blue
                                Else
                                    .AppearanceCell.BackColor = Color.White
                                    .AppearanceCell.ForeColor = Color.Black
                                End If

                            End With

                    End Select
                Next
                .EndInit()
            End With

            ogbpackcartondetail.Enabled = _StateEdit
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click
        If Me.VerifyData Then
            Select Case FNPackCartonSubType.SelectedIndex
                Case 0
                    If Me.CreateCarton Then
                        Me.Process = True
                        Call CallLoadCarton()
                        CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                        If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                            Me.Close()
                        End If
                    End If
                Case 1
                    If Me.CreateCartonAssortOneColorMultiSize Then
                        Me.Process = True
                        Call CallLoadCarton()
                        CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                        If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                            Me.Close()
                        End If
                    End If
                Case 2
                    If Me.CreateCartonAssortOneSizeMultiColor Then
                        Me.Process = True
                        Call CallLoadCarton()
                        CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                        If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                            Me.Close()
                        End If
                    End If
                Case 3
                    If Me.CreateCartonAssortMultiColorMultiSize Then
                        Me.Process = True
                        Call CallLoadCarton()
                        CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                        If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                            Me.Close()
                        End If
                    End If
            End Select

        End If
    End Sub

    Private Sub CallLoadCarton()
        Try
            GenBarcodeEN13(FTPackNo)
            Call CallByName(_ObjectParent, "CreateTreeCarton", CallType.Method, Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private _StateSumGrid As Boolean
    Private Sub SumGrid()
        _StateSumGrid = True
        CType(ogcsubprodpack.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            _Total = 0
            With Me.ogvsubprodpack
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                    Select Case GridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "Total".ToUpper
                        Case Else
                            If IsNumeric(.GetFocusedRowCellValue(GridCol)) Then
                                _Total = _Total + CDbl(.GetFocusedRowCellValue(GridCol))
                            Else
                                _Total = _Total + 0
                            End If
                    End Select

                Next

                .SetFocusedRowCellValue("Total", _Total)
            End With
        Catch ex As Exception
        End Try

        CType(ogcsubprodpack.DataSource, DataTable).AcceptChanges()

        _StateSumGrid = False
    End Sub

    Private Sub Caledit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepSize.EditValueChanging
        Try
            Dim _NewValue As Integer = e.NewValue
            Dim _OrgValue As Integer = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")

                    If Not (_StateSumGrid) Then
                        ogvsubprodpack.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))

                        If Not (ogcsubprodpack.DataSource Is Nothing) Then
                            Call SumGrid()
                        End If

                    End If

                End With
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Function GenerateBarcodeSSCC(_SBarcodeNo As Integer, _EBarcodeNo As Integer, _BeginCarton As Integer) As String
        Try
            Dim _Cmd As String = "" : Dim _Seq As Integer = 0
            Dim _BarCodeSSS As String = "" : Dim _O, _M, _T As Integer : Dim _DemoBarcode As String = "" : Dim _BarCode As String = ""

            _Cmd = "SELECT TOP (1) FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSESystemConfig WITH(NOLOCK) WHERE (FTCfgName = N'CfManufacturerNo')"
            Dim _FacNo As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "")

            '_Cmd = "Select Max(FNCartonNo) AS FNCartonNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "Where FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            '_Cmd &= vbCrLf & "and isnull(FTBarCodeEAN13,'') <> ''"
            '_Seq = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            _Seq = _BeginCarton



            For I As Integer = _SBarcodeNo To _EBarcodeNo
                '   _Cmd = "Select Top 1 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode Where FTCartonNo='" & I & "'"
                ' If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                _BarCodeSSS = _FacNo & Microsoft.VisualBasic.Right("000000000" & CStr(I), 9)

                _O = 0 : _M = 0 : _T = 0
                For x As Integer = 1 To 16
                    _DemoBarcode = _BarCodeSSS
                    If (x Mod 2) = 0 Then
                        _M += +CInt(_DemoBarcode.Substring(x - 1, 1))
                    Else
                        _O += +CInt(_DemoBarcode.Substring(x - 1, 1))
                    End If
                Next
                _M = _M * 3 : _T = _M + _O : _T = _T Mod 10
                If _T > 0 Then
                    _T = 10 - _T
                End If
                _BarCode = "000" & _BarCodeSSS & CStr(_T)


                ' End If
                _Seq += +1
            Next
            Return _BarCode

        Catch ex As Exception
            Return ""

        End Try
    End Function

    Private Function GenBarcodeEN13(PackNo As String) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _EN13 As String = "" : Dim _CartonNO As String = ""
            Dim _CustomerPO As DataTable
            _Cmd = "Select    FTCustomerPO from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack  where FTPackNo  = '" & PackNo & "' "
            _CustomerPO = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Dim _oDt As System.Data.DataTable

            For Each Rz As DataRow In _CustomerPO.Rows
                _Cmd = "SELECT   C.FTColorway, C.FTSizeBreakDown, C.FTOrderNo, C.FTPackNo,  C.FTPOLine  , C.FTSubOrderNo, PK.FTCustomerPO, C.FNCartonNo, D.FTSerialFrom , D.FTSerialTo  , D.FNFrom , D.FNTo ,C.FNQuantity"
                _Cmd &= vbCrLf & " , convert(nvarchar(30) , convert(int ,D.FTSerialFrom ) + ROW_NUMBER() Over (partition by C.FTOrderNo , C.FTSubOrderNo, C.FTPOLine ,C.FTPackNo,C.FTColorway, C.FTSizeBreakDown ,PK.FTCustomerPO ,C.FNQuantity ORder by  C.FTPackNo,C.FNCartonNo) -1 )AS FNCartonSeq  "
                _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS C  LEFT OUTER JOIN "
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS  PK WITH(NOLOCK)    ON C.FTPackNo = PK.FTPackNo INNER JOIN    "
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. TEXPTPackPlan_D as D  ON PK.FTCustomerPO = D.FTPORef and C.FTPOLine  = convert(nvarchar(30), convert(int, D.FTPOLineNo)) "
                _Cmd &= vbCrLf & " and C.FTSizeBreakDown = D.FTSizeBreakDown and    C.FTColorway= replace(replace(D.FTShortDescription,D.FTStyleCode,''),'-','')  and C.FNQuantity = D.FNQtyPerPack "

                _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS  T WITH(NOLOCK)    ON  C.FTPackNo = T.FTPackNo AND C.FNCartonNo = T.FNCartonNo     "
                _Cmd &= vbCrLf & "WHERE  (PK.FTCustomerPO = N'" & Rz!FTCustomerPO.ToString & "')" 'ISNULL(T.FTState,'0') = '0' and 
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


                For Each R As DataRow In _oDt.Rows
                    _EN13 = HI.UL.ULF.rpQuoted(GenerateBarcodeSSCC(R!FNCartonSeq.ToString, R!FNCartonSeq.ToString, R!FNCartonNo.ToString))
                    _Cmd = " Select  FTBarCodeEAN13 From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode  "
                    _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and isnull(FTBarCodeEAN13,'') <>'' "
                    If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count <= 0 Then
                        _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode"
                        _Cmd &= vbCrLf & "Set  FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FTCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTBarCodeEAN13='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                        _Cmd &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                        _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                        _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                        If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
                            _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode (FTInsUser,FDInsDate,FTUpdTime,FTCartonNo,FTBarCodeEAN13,FTBarCodeCarton,FTPackNo,FNCartonNo)"
                            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        End If
                    End If
                Next
            Next




            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



End Class