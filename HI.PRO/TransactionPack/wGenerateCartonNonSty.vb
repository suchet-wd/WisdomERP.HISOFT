Imports System.Drawing

Public Class wGenerateCartonNonSty


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

    Private _SetPackType As Integer = 0
    Property SetPackType As Integer
        Get
            Return _SetPackType
        End Get
        Set(value As Integer)
            _SetPackType = value
        End Set
    End Property


    Private _SetPackValue As Integer = 0
    Property SetPackValue As Integer
        Get
            Return _SetPackValue
        End Get
        Set(value As Integer)
            _SetPackValue = value
        End Set
    End Property



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

        With ogvsubprodpackmerge
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

    Private Sub CreateBreakDownForPack(dt As DataTable, _dtpackmerge As DataTable)
        Dim _colcount As Integer = 0



        With dt
            For Each R As DataRow In .Rows
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                        Case Else
                            R.Item(Col) = 0
                    End Select
                Next
            Next
        End With

        With _dtpackmerge
            For Each R As DataRow In .Rows
                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToUpper
                        Case "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case Else
                            R.Item(Col) = 0
                    End Select
                Next
            Next
        End With


        With Me.ogvsubprodpack

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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

        With Me.ogvsubprodpackmerge

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
        Me.ogcsubprodpackmerge.DataSource = _dtpackmerge.Copy
    End Sub

    Private Sub LoadOrderPackBreakDownCreateCarton(Key As Object)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _dtpackmerge As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        If SetPackType = 1 Then
            Try
                _dt.Columns.Add("FTSelect", GetType(String))

                For Each R As DataRow In _dt.Rows
                    R!FTSelect = "0"
                Next


                With Me.ogvsubprod
                    For I As Integer = .Columns.Count - 1 To 0 Step -1
                        If .Columns(I).FieldName = "FTSelect" Then
                            .Columns(I).OptionsColumn.AllowEdit = True
                        End If
                    Next
                End With


            Catch ex As Exception
            End Try

        Else
            Try
                _dt.Columns.Add("FTSelect", GetType(String))

                For Each R As DataRow In _dt.Rows
                    R!FTSelect = "1"
                Next




            Catch ex As Exception
            End Try
        End If




        _dtpack = _dt.Copy
        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal_Merge '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dtpackmerge = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.ogvsubprod

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                        .Columns(I).AppearanceCell.BackColor = Color.White
                        .Columns(I).AppearanceCell.ForeColor = Color.Black
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

                    Case "FTSelect".ToUpper
                        .Columns(I).AppearanceCell.BackColor = Color.White
                        .Columns(I).AppearanceCell.ForeColor = Color.Black
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .Columns(I).OptionsColumn.AllowEdit = True
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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

        Call CreateBreakDownForPack(_dtpack, _dtpackmerge)

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
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
        Dim _cmd As String = ""

        If SetPackType = 1 Then


            If CType(Me.ogcsubprod.DataSource, DataTable).Select("FTSelect='1'").Length < SetPackValue Then
                With DirectCast(Me.ogcsubprod.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")
                        _cmd = "Select top 1  isnull( FTStateStyleSet ,'0') as FTStateStyleSet  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.tmermstyle with(nolock) "
                        _cmd &= vbCrLf & " where exists ( SELECT TOP 1 V.FNHSysStyleId "
                        _cmd &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPACKOrderPack_Detail AS D INNER JOIN"
                        _cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS V ON D.FTOrderNo = V.FTOrderNo --AND D.FTSubOrderNo = V.FTSubOrderNo  "
                        _cmd &= vbCrLf & " WHERE  (D.FTPackNo = N'" & FTPackNo.ToString & "')"
                        _cmd &= vbCrLf & " ) "
                        _cmd &= vbCrLf & " "


                        Exit For
                    Next
                End With
                If HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_MASTER, "").ToString <> "0" Then
                    HI.MG.ShowMsg.mInfo("กรุณาเลือก sub order ให้ครบ Set  !!! !!!", 2206171724, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

                    Return False

                End If




            End If

        End If

        If Me.FNHSysCartonId.Text <> "" And Me.FNHSysCartonId.Properties.Tag.ToString <> "" Then
            If FNPackPerCaton.Value > 0 Then

                Dim _PackValue As Integer = FNPackPerCaton.Value

                Select Case FNPackCartonSubType.SelectedIndex
                    Case 0
                        _Pass = True
                    Case 1

                        If FTStateMerge.Checked Then

                            With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
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
                        Else
                            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                                .AcceptChanges()

                                Dim R1() As DataRow = .Select("Total>0")
                                Dim R2() As DataRow = .Select("sum(Total)=" & _PackValue & "")

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
                        End If



                    Case 2
                        If FTStateMerge.Checked Then
                            With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
                                .AcceptChanges()

                                Dim _TotalPack As Integer = 0
                                Dim _SumPack As Integer = 0
                                Dim _StatePack As Boolean = True

                                For Each Col As DataColumn In .Columns
                                    _SumPack = 0
                                    Select Case Col.ColumnName.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        Else

                            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                                .AcceptChanges()

                                Dim _TotalPack As Integer = 0
                                Dim _SumPack As Integer = 0
                                Dim _StatePack As Boolean = True

                                For Each Col As DataColumn In .Columns
                                    _SumPack = 0
                                    Select Case Col.ColumnName.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        End If

                    Case 3
                        If FTStateMerge.Checked Then


                            With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
                                .AcceptChanges()
                                Dim _TotalPack As Integer = 0
                                For Each R As DataRow In .Rows
                                    For Each Col As DataColumn In .Columns

                                        Select Case Col.ColumnName.ToUpper
                                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        Else

                            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                                .AcceptChanges()
                                Dim _TotalPack As Integer = 0
                                For Each R As DataRow In .Rows
                                    For Each Col As DataColumn In .Columns

                                        Select Case Col.ColumnName.ToUpper
                                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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

                        End If

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
        Dim _LastCartonNo As Integer = 0
        Dim _Qry As String = ""


        _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
        _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))


        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try



            Dim _dt As DataTable
            Dim _SizeQty As Integer = 0
            Dim _PackQty As Integer = Me.FNPackPerCaton.Value
            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _POLine As String = ""
            Dim _dtMerge As DataTable

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy


            'If FTStateMerge.Checked = True Then
            '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal_Merge '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.TrimEnd) & "' "
            '    _dtMerge = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            'End If
            Dim _dtgenrow = _dt.Copy
            _dtgenrow.Rows.Clear()

            For Each row As DataRow In _dt.Select("", "FTPOLine asc ")
                _dtgenrow.Rows.Clear()
                _dtgenrow.ImportRow(row)





                With _dtgenrow

                    .BeginInit()

                    Dim _PackBal As Integer = _PackQty
                    For Each Col As DataColumn In .Columns

                        Select Case Col.ColumnName.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                            Case Else
                                For Each R As DataRow In .Select("FTOrderNo <> ''", "FTColorway asc ,  FTSubOrderNo asc")


                                    _FTOrderNo = R!FTOrderNo.ToString()
                                    _FTSubOrderNo = R!FTSubOrderNo.ToString()
                                    _FTColorway = R!FTColorway.ToString()
                                    _POLine = R!FTPOLine.ToString

                                    'For Each Col As DataColumn In .Columns
                                    '    Select Case Col.ColumnName.ToUpper
                                    '        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                    '        Case Else
                                    _SizeQty = Integer.Parse(Val(R.Item(Col)))

                                    If _SizeQty >= _PackQty Then



                                        Do While _SizeQty >= _PackQty

                                            If _PackBal < _PackQty Then

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
                                                _Qry &= vbCrLf & "," & _PackBal & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                '   HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Return False
                                                End If

                                                _SizeQty = _SizeQty - _PackBal
                                                _PackBal = 0
                                                If _SizeQty <= 0 Then
                                                    _SizeQty = 0
                                                End If
                                                R.Item(Col) = _SizeQty

                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "] >= 0 and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    If Me.FNPackCartonScrapType.SelectedIndex = 1 Then
                                                        GoTo 97
                                                    Else
                                                        GoTo 9
                                                    End If

                                                End If



                                                If _PackBal <= 0 Then
                                                    _PackBal = _PackQty
                                                End If

                                            Else

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
                                                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Return False
                                                End If
                                                _SizeQty = _SizeQty - _PackQty
                                                If _SizeQty <= 0 Then
                                                    _SizeQty = 0
                                                End If
                                                R.Item(Col) = _SizeQty '999
                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "]  >= 0  and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    If Me.FNPackCartonScrapType.SelectedIndex = 1 Then
                                                        GoTo 97
                                                    Else
                                                        GoTo 9
                                                    End If
                                                End If


                                                _PackBal = _PackBal - _PackQty
                                                If _PackBal <= 0 Then
                                                    _PackBal = _PackQty
                                                End If




                                            End If
                                        Loop

                                        If _SizeQty > 0 Then
                                            GoTo 12
                                        End If



                                    Else
12:
                                        Do While _SizeQty >= 1

                                            R.Item(Col) = _SizeQty
                                            'If .Compute("Sum(" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & ")", "" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "  >= 0") < _PackQty Then
                                            '    _PackBal = _PackQty
                                            '    GoTo 9
                                            'End If
                                            If _PackBal = _PackQty Then
                                                Try

                                                    If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "]  >= 0 and FTColorway = '" & HI.UL.ULF.rpQuoted(_FTColorway) & "'  and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                        _PackBal = _PackQty
                                                        If Me.FNPackCartonScrapType.SelectedIndex = 1 Then
                                                            GoTo 97
                                                        Else
                                                            GoTo 9
                                                        End If
                                                    End If
                                                Catch ex As Exception
                                                    MsgBox(ex)
                                                End Try

                                                _LastCartonNo = _LastCartonNo + 1
                                            End If

                                            If _PackBal <= _SizeQty Then

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
                                                _Qry &= vbCrLf & "," & _PackBal & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Return False
                                                End If
                                                '_PackBal = _PackBal - _SizeQty

                                                _SizeQty = _SizeQty - _PackBal
                                                _PackBal = 0

                                            Else

                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "]  >= 0 and FTColorway = '" & HI.UL.ULF.rpQuoted(_FTColorway) & "'  and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    If Me.FNPackCartonScrapType.SelectedIndex = 1 Then
                                                        GoTo 97
                                                    Else
                                                        GoTo 9
                                                    End If
                                                End If

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
                                                _Qry &= vbCrLf & "," & _SizeQty & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                ' HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Return False
                                                End If
                                                _PackBal = _PackBal - _SizeQty
                                                _SizeQty = 0

                                            End If


                                            If _SizeQty <= 0 Then
                                                _SizeQty = 0
                                            End If
                                            R.Item(Col) = _SizeQty
                                            'If .Compute("Sum(" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & ")", "" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "  >= 0") < _PackQty Then
                                            '    _PackBal = _PackQty
                                            '    GoTo 9
                                            'End If
                                            If _PackBal <= 0 Then
                                                _PackBal = _PackQty
                                            End If

                                        Loop
                                    End If


97:
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


                                            ' HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                HI.Conn.SQLConn.Tran.Rollback()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                Return False
                                            End If
                                            _SizeQty = 0
                                        End If
                                    End If

                                    R.Item(Col) = _SizeQty

9:

                                    '    End Select
                                    'Next
                                Next
                        End Select
                    Next

                    If Me.FNPackCartonScrapType.SelectedIndex = 0 Then
                        For Each R As DataRow In .Select("FTOrderNo <> ''", "FTColorway asc ,  FTSubOrderNo asc")

                            _FTOrderNo = R!FTOrderNo.ToString()
                            _FTSubOrderNo = R!FTSubOrderNo.ToString()
                            _FTColorway = R!FTColorway.ToString()
                            _POLine = R!FTPOLine.ToString

                            For Each Col As DataColumn In .Columns
                                Select Case Col.ColumnName.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                                    Case Else
                                        _SizeQty = Integer.Parse(Val(R.Item(Col)))
                                        If _SizeQty > 0 Then

                                            Dim _PackQtyUse As Integer = _PackQty
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

                                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                HI.Conn.SQLConn.Tran.Rollback()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                Return False
                                            End If
                                            R.Item(Col) = 0
                                            If _PackQty > _SizeQty Then
                                                _PackQtyUse = _PackQty - _SizeQty
                                                For Each rowx As DataRow In .Select("FTPOLine='" & HI.UL.ULF.rpQuoted(_POLine) & "' and  FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'  ")
                                                    _SizeQty = Integer.Parse(Val(rowx.Item(Col)))
                                                    If _SizeQty > 0 Then



                                                        _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                                        _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                                        _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton ,FTPOLine)"
                                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                                        _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(rowx!FTOrderNo.ToString()) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(rowx!FTSubOrderNo.ToString()) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                                        _Qry &= vbCrLf & "," & _SizeQty & " "
                                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                        _Qry &= vbCrLf & "," & _PackQty & " "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "

                                                        ' HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                            HI.Conn.SQLConn.Tran.Rollback()
                                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                            Return False
                                                        End If
                                                        rowx.Item(Col) = 0
                                                    End If


                                                Next


                                            End If

                                        End If

                                End Select
                            Next
                        Next
                    End If


                    .EndInit()

                End With
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)
        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            MsgBox(ex.Message)
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function


    Private Function CreateCartonSet() As Boolean

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
            Dim _dtMerge As DataTable

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Select("FTSelect='1'").CopyToDataTable

            'Dim _oDtSet As New DataTable
            'Dim _dtView As DataView
            'If _SetPackType = 1 And _SetPackValue > 1 Then
            '    _dtView = New DataView(_dt)
            '    _oDtSet = _dtView.ToTable(True, "FTOrderNo", "FTColorway", "FTPOLine")

            'End If


            'If FTStateMerge.Checked = True Then
            '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal_Merge '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.TrimEnd) & "' "
            '    _dtMerge = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            'End If



            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _LastCartonNoStart As Integer = _LastCartonNo

            For Each Rx As DataRow In _dt.Rows
                Dim _dtn As New DataTable
                _dtn = _dt.Select("FTOrderNo='" & Rx!FTOrderNo.ToString & "' and FTSubOrderNo='" & Rx!FTSubOrderNo.ToString & "' and FTColorway='" & Rx!FTColorway.ToString & "' and FTPOLine='" & Rx!FTPOLine.ToString & "'").CopyToDataTable
                _LastCartonNo = _LastCartonNoStart
                With _dtn

                    .BeginInit()

                    Dim _PackBal As Integer = _PackQty
                    For Each Col As DataColumn In .Columns

                        Select Case Col.ColumnName.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                            Case Else
                                For Each R As DataRow In .Select("FTOrderNo <> ''", "FTColorway asc ,  FTSubOrderNo asc")


                                    _FTOrderNo = R!FTOrderNo.ToString()
                                    _FTSubOrderNo = R!FTSubOrderNo.ToString()
                                    _FTColorway = R!FTColorway.ToString()
                                    _POLine = R!FTPOLine.ToString


                                    'For Each Col As DataColumn In .Columns
                                    '    Select Case Col.ColumnName.ToUpper
                                    '        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                                    '        Case Else
                                    _SizeQty = Integer.Parse(Val(R.Item(Col)))

                                    If _SizeQty >= _PackQty Then



                                        Do While _SizeQty >= _PackQty

                                            If _PackBal < _PackQty Then

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
                                                _Qry &= vbCrLf & "," & _PackBal & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


                                                _SizeQty = _SizeQty - _PackBal
                                                _PackBal = 0
                                                If _SizeQty <= 0 Then
                                                    _SizeQty = 0
                                                End If
                                                R.Item(Col) = _SizeQty
                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "] >= 0 and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    GoTo 9
                                                End If

                                                If _PackBal <= 0 Then
                                                    _PackBal = _PackQty
                                                End If

                                            Else

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
                                                R.Item(Col) = _SizeQty '999
                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "]  >= 0  and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    GoTo 9
                                                End If

                                                _PackBal = _PackBal - _PackQty
                                                If _PackBal <= 0 Then
                                                    _PackBal = _PackQty
                                                End If



                                            End If
                                        Loop

                                        If _SizeQty > 0 Then
                                            GoTo 12
                                        End If



                                    Else
12:
                                        Do While _SizeQty >= 1

                                            R.Item(Col) = _SizeQty
                                            'If .Compute("Sum(" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & ")", "" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "  >= 0") < _PackQty Then
                                            '    _PackBal = _PackQty
                                            '    GoTo 9
                                            'End If
                                            If _PackBal = _PackQty Then
                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "]  >= 0 and FTColorway = '" & HI.UL.ULF.rpQuoted(_FTColorway) & "'  and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    GoTo 9
                                                End If
                                                _LastCartonNo = _LastCartonNo + 1
                                            End If

                                            If _PackBal <= _SizeQty Then

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
                                                _Qry &= vbCrLf & "," & _PackBal & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                '_PackBal = _PackBal - _SizeQty

                                                _SizeQty = _SizeQty - _PackBal
                                                _PackBal = 0

                                            Else

                                                If .Compute("Sum([" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "])", "[" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "]  >= 0 and FTColorway = '" & HI.UL.ULF.rpQuoted(_FTColorway) & "'  and FTPOLine='" & _POLine & "'") < _PackQty Then
                                                    _PackBal = _PackQty
                                                    GoTo 9
                                                End If

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
                                                _Qry &= vbCrLf & "," & _SizeQty & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & _PackQty & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                _PackBal = _PackBal - _SizeQty
                                                _SizeQty = 0

                                            End If





                                            If _SizeQty <= 0 Then
                                                _SizeQty = 0
                                            End If
                                            R.Item(Col) = _SizeQty
                                            'If .Compute("Sum(" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & ")", "" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "  >= 0") < _PackQty Then
                                            '    _PackBal = _PackQty
                                            '    GoTo 9
                                            'End If
                                            If _PackBal <= 0 Then
                                                _PackBal = _PackQty
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

9:

                                    '    End Select
                                    'Next
                                Next
                        End Select
                    Next

                    If Me.FNPackCartonScrapType.SelectedIndex = 0 Then
                        For Each R As DataRow In .Select("FTOrderNo <> ''", "FTColorway asc ,  FTSubOrderNo asc")

                            _FTOrderNo = R!FTOrderNo.ToString()
                            _FTSubOrderNo = R!FTSubOrderNo.ToString()
                            _FTColorway = R!FTColorway.ToString()
                            _POLine = R!FTPOLine.ToString

                            For Each Col As DataColumn In .Columns
                                Select Case Col.ColumnName.ToUpper
                                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                                    Case Else
                                        _SizeQty = Integer.Parse(Val(R.Item(Col)))
                                        If _SizeQty > 0 Then

                                            Dim _PackQtyUse As Integer = _PackQty
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
                                            R.Item(Col) = 0
                                            If _PackQty > _SizeQty Then
                                                _PackQtyUse = _PackQty - _SizeQty
                                                For Each rowx As DataRow In .Select("FTPOLine='" & HI.UL.ULF.rpQuoted(_POLine) & "' and  FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'  ")
                                                    _SizeQty = Integer.Parse(Val(rowx.Item(Col)))
                                                    If _SizeQty > 0 Then



                                                        _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                                                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                                                        _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                                                        _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton ,FTPOLine)"
                                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                                                        _Qry &= vbCrLf & "," & _LastCartonNo & " "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(rowx!FTOrderNo.ToString()) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(rowx!FTSubOrderNo.ToString()) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                                                        _Qry &= vbCrLf & "," & _SizeQty & " "
                                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                        _Qry &= vbCrLf & "," & _PackQty & " "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "

                                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                                                        rowx.Item(Col) = 0
                                                    End If


                                                Next


                                            End If

                                        End If

                                End Select
                            Next
                        Next
                    End If


                    .EndInit()

                End With


            Next



            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
    End Function


    Private Function CreateCartonSolidmultiSubOrder() As Boolean
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
            Dim _dtMerge As DataTable

            Dim _dtpack As DataTable
            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
            End With

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy


            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0
            Dim _SizeBalQty As Integer = 0

            Dim dtpackassort As New DataTable
            dtpackassort.Columns.Add("FTSizeCode", GetType(String))
            dtpackassort.Columns.Add("FNQuantity", GetType(Integer))




            For Each ColP As DataColumn In _dt.Columns

                Select Case ColP.ColumnName.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                    Case Else

T1:

                        Dim _Str As String = ""

                        '_dtpack.Compute("sum([3-6]) ", "Total> 0")

                        For Each F1 As DataRow In _dtpack.Select(" sum([" & ColP.ColumnName.ToString & "]) = " & FNPackPerCaton.Value & "  ")

                            _LastCartonNo = _LastCartonNo + 1
                            For Each R As DataRow In _dtpack.Select("[" & ColP.ColumnName.ToString & "] > 0")

                                _FTColorway = R!FTColorway.ToString()
                                _POLine = R!FTPOLine.ToString
                                _FTSubOrderNo = R!FTSubOrderNo.ToString()
                                _SizePackQty = Val(R.Item(ColP.ColumnName))
                                _FTOrderNo = R!FTOrderNo.ToString()

                                _Str = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' and FTSubOrderNo='" & _FTSubOrderNo & "'  "

                                If _Str = "" Then
                                    _Str = "[" & ColP.ColumnName.ToString & "]>=" & Integer.Parse(Val(R.Item(ColP.ColumnName))) & ""
                                Else
                                    _Str &= " AND [" & ColP.ColumnName.ToString & "]>=" & Integer.Parse(Val(R.Item(ColP.ColumnName))) & ""
                                End If

                                For Each R2 As DataRow In _dt.Select(_Str)




                                    If _SizePackQty > 0 Then
                                        _SizeQty = _SizePackQty

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
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColP.ColumnName.ToString) & "' "
                                        _Qry &= vbCrLf & "," & _SizePackQty & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                        _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "


                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)


                                        R2.Item(ColP) = R2.Item(ColP) - _SizePackQty


                                        'If Val(_SizePackQty) > 0 Then
                                        '    '_SizePackQty = _SizeBalQty
                                        '    GoTo T1
                                        'End If

                                    End If
                                    Exit For

                                Next



                            Next
                            If _dt.Select(_Str).Length > 0 Then
                                GoTo T1
                            Else
                                Exit For
                            End If


                        Next








                End Select

            Next




            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)

            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
    End Function


    Private Function CreateCartonAssortOneSizeMultiColor(d As String) As Boolean
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
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToString, "FTSelect".ToUpper
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
            Dim _dtMerge As DataTable

            Dim _dtpack As DataTable
            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
            End With



            Dim _dtpackMerge As DataTable
            With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
                .AcceptChanges()
                _dtpackMerge = .Copy
            End With

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy

            If FTStateMerge.Checked = True Then
                _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal_Merge '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.TrimEnd) & "' "
                _dtMerge = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            End If




            Dim dtpackassort As New DataTable
            dtpackassort.Columns.Add("FTSizeCode", GetType(String))
            dtpackassort.Columns.Add("FNQuantity", GetType(Integer))

            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0
            Dim _SizeBalQty As Integer = 0
            For Each Row As DataRow In _dtpackMerge.Select("Total=" & FNPackPerCaton.Value & "")

                '_FTOrderNo = Row!FTOrderNo.ToString()
                '_FTSubOrderNo = Row!FTSubOrderNo.ToString()
                _FTColorway = Row!FTColorway.ToString()
                _POLine = Row!FTPOLine.ToString


                dtpackassort.Rows.Clear()


                For Each ColP As DataColumn In _dtpackMerge.Columns
                    Select Case ColP.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                        Case Else
                            _SizePackQty = Integer.Parse(Val(Row.Item(ColP)))
                            If _SizePackQty > 0 Then
                                dtpackassort.Rows.Add(ColP.ColumnName.ToString, _SizePackQty)
                            End If
                    End Select
                Next


                If dtpackassort.Rows.Count > 0 Then
                    Dim _Str As String = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                    For Each R As DataRow In dtpackassort.Rows

                        If _Str = "" Then
                            _Str = "[" & R!FTSizeCode.ToString & "]>=" & R!FNQuantity.ToString & ""
                        Else
                            _Str &= " AND [" & R!FTSizeCode.ToString & "]>=" & R!FNQuantity.ToString & ""
                        End If

                    Next

                    With _dtMerge
                        .BeginInit()
                        Do While .Select(_Str).Length > 0
                            For Each Rx As DataRow In .Select(_Str)
                                _LastCartonNo = _LastCartonNo + 1
                                For Each Col As DataColumn In .Columns
                                    Select Case Col.ColumnName.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                                        Case Else
                                            _SizePackQty = 0
                                            _SizeBalQty = 0

                                            For Each Rxp As DataRow In dtpackassort.Select("  FTSizeCode='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "'")
                                                _SizePackQty = Integer.Parse(Val(Rxp!FNQuantity.ToString))
                                            Next
                                            _SizeBalQty = _SizePackQty
1:

                                            If _dt.Select("FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND [" & Col.ColumnName & "] >=" & _SizePackQty & "  AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' ").Length >= 1 Then
                                                For Each rd As DataRow In _dt.Select("FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND [" & Col.ColumnName & "]>=" & _SizePackQty & "  AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' ")
                                                    _FTOrderNo = rd!FTOrderNo.ToString()
                                                    _FTSubOrderNo = rd!FTSubOrderNo.ToString()
                                                    rd.Item(Col.ColumnName) = rd.Item(Col.ColumnName) - _SizePackQty

                                                    Exit For
                                                Next
                                            Else
                                                For Each rd As DataRow In _dt.Select("FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND [" & Col.ColumnName & "]>0  AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'")
                                                    _FTOrderNo = rd!FTOrderNo.ToString()
                                                    _FTSubOrderNo = rd!FTSubOrderNo.ToString()
                                                    _SizePackQty = rd.Item(Col.ColumnName)
                                                    rd.Item(Col.ColumnName) = rd.Item(Col.ColumnName) - _SizePackQty
                                                    Exit For
                                                Next
                                            End If


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
                                                _SizeBalQty = _SizeBalQty - _SizePackQty
                                                _SizeQty = _SizeQty - _SizePackQty
                                                If _SizeQty <= 0 Then
                                                    _SizeQty = 0
                                                End If

                                                Rx.Item(Col) = _SizeQty

                                                If _SizeBalQty > 0 Then
                                                    _SizePackQty = _SizeBalQty
                                                    GoTo 1
                                                End If

                                            End If

                                    End Select

                                Next


                            Next


                        Loop

                        .EndInit()
                    End With

                    'With _dt
                    '    .BeginInit()

                    '    Do While .Select(_Str).Length > 0


                    '        For Each Rx As DataRow In .Select(_Str)
                    '            _LastCartonNo = _LastCartonNo + 1
                    '            For Each Col As DataColumn In .Columns
                    '                Select Case Col.ColumnName.ToUpper
                    '                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper
                    '                    Case Else
                    '                        _SizePackQty = 0
                    '                        For Each Rxp As DataRow In dtpackassort.Select("FTSizeCode='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "'")
                    '                            _SizePackQty = Integer.Parse(Val(Rxp!FNQuantity.ToString))
                    '                        Next

                    '                        If _SizePackQty > 0 Then
                    '                            _SizeQty = Integer.Parse(Val(Rx.Item(Col)))
                    '                            _FTOrderNo = Rx!FTOrderNo.ToString()
                    '                            _FTSubOrderNo = Rx!FTSubOrderNo.ToString()


                    '                            _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
                    '                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTPackNo"
                    '                            _Qry &= vbCrLf & " , FNCartonNo, FTOrderNo, FTSubOrderNo"
                    '                            _Qry &= vbCrLf & "  , FTColorway, FTSizeBreakDown, FNQuantity,FNHSysCartonId,FNPackCartonSubType,FNPackPerCarton,FTPOLine)"
                    '                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    '                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    '                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "' "
                    '                            _Qry &= vbCrLf & "," & _LastCartonNo & " "
                    '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "' "
                    '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' "
                    '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                    '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "' "
                    '                            _Qry &= vbCrLf & "," & _SizePackQty & " "
                    '                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                    '                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                    '                            _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                    '                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "


                    '                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    '                            _SizeQty = _SizeQty - _SizePackQty
                    '                            If _SizeQty <= 0 Then
                    '                                _SizeQty = 0
                    '                            End If

                    '                            Rx.Item(Col) = _SizeQty
                    '                        End If

                    '                End Select

                    '            Next

                    '        Next
                    '    Loop

                    '    .EndInit()
                    'End With
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



    Private Function CreateCartonAssortOneColorMultiSizeSet() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Creating...Carton Please Wait....")

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

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
            Dim _dtMerge As DataTable


            Dim _dtpackMerge As DataTable
            With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
                .AcceptChanges()
                _dtpackMerge = .Copy
            End With

            CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
            _dt = CType(Me.ogcsubprod.DataSource, DataTable).Copy

            'If FTStateMerge.Checked = True Then
            '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CreateCarton_Bal_Merge '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.TrimEnd) & "' "
            '    _dtMerge = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            'End If

            Dim dtpackassort As New DataTable
            dtpackassort.Columns.Add("FTSizeCode", GetType(String))
            dtpackassort.Columns.Add("FNQuantity", GetType(Integer))

            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _LastCartonNoStart As Integer = _LastCartonNo



            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0
            Dim _SizeBalQty As Integer = 0




            For Each Row As DataRow In _dtpackMerge.Select("Total=" & FNPackPerCaton.Value & "")
                '_FTOrderNo = Row!FTOrderNo.ToString()
                '_FTSubOrderNo = Row!FTSubOrderNo.ToString()
                _FTColorway = Row!FTColorway.ToString()
                _POLine = Row!FTPOLine.ToString
                dtpackassort.Rows.Clear()
                For Each ColP As DataColumn In _dtpackMerge.Columns
                    Select Case ColP.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                        Case Else
                            _SizePackQty = Integer.Parse(Val(Row.Item(ColP)))
                            If _SizePackQty > 0 Then
                                dtpackassort.Rows.Add(ColP.ColumnName.ToString, _SizePackQty)
                            End If
                    End Select
                Next


                If dtpackassort.Rows.Count > 0 Then
                    Dim _Str As String = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "
                    For Each R As DataRow In dtpackassort.Rows

                        If _Str = "" Then
                            _Str = "[" & R!FTSizeCode.ToString & "]>=" & R!FNQuantity.ToString & ""
                        Else
                            _Str &= " AND [" & R!FTSizeCode.ToString & "]>=" & R!FNQuantity.ToString & ""
                        End If

                    Next

                    Dim _dtSet As New DataTable
                    Try
                        Dim _Stasub As String = ""
                        For Each R As DataRow In _dt.Select(_Str)
                            _Stasub = R!FTSubOrderNo.ToString
                            Exit For
                        Next
                        _dtSet = _dt.Select("FTSubOrderNo='" & _Stasub & "'").CopyToDataTable
                    Catch ex As Exception
                    End Try

                    With _dtSet
                        .BeginInit()
                        Do While .Select(_Str).Length > 0
                            For Each Rx As DataRow In .Select(_Str)
15:
                                If _dt.Select(_Str).Length <= 0 Then Exit For

                                _LastCartonNo = _LastCartonNo + 1


                                For Each Col As DataColumn In .Columns
                                    Select Case Col.ColumnName.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                                        Case Else
                                            _SizePackQty = 0
                                            _SizeBalQty = 0

                                            For Each Rxp As DataRow In dtpackassort.Select("  FTSizeCode='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "'")
                                                _SizePackQty = Integer.Parse(Val(Rxp!FNQuantity.ToString))
                                            Next
                                            _SizeBalQty = _SizePackQty



                                            For Each rd As DataRow In _dt.Select("FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND [" & Col.ColumnName & "]>=" & _SizePackQty & "  AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' ")

                                                _FTOrderNo = rd!FTOrderNo.ToString()
                                                _FTSubOrderNo = rd!FTSubOrderNo.ToString()
                                                rd.Item(Col.ColumnName) = rd.Item(Col.ColumnName) - _SizePackQty


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


                                                    '  HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                        HI.Conn.SQLConn.Tran.Rollback()
                                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                        Return False
                                                    End If



                                                    _SizeBalQty = _SizeBalQty - _SizePackQty
                                                    _SizeQty = _SizeQty - _SizePackQty
                                                    If _SizeQty <= 0 Then
                                                        _SizeQty = 0
                                                    End If

                                                    Rx.Item(Col) = _SizeQty

                                                    If _SizeBalQty > 0 Then
                                                        _SizePackQty = _SizeBalQty

                                                    End If

                                                End If


                                            Next



                                    End Select

                                Next

                                GoTo 15


                            Next


                        Loop

                        .EndInit()
                    End With

                End If
            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)
            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try



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


    Private Function CreateCartonAssortOneColorMultiSize(_d As String) As Boolean
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
            dtpackassort.Columns.Add("FTSubOrderNo", GetType(String))

            _Qry = " SELECT MAX(FNCartonNo) AS FNCartonNo "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo) & "'"
            _LastCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD)))

            Dim _ColorWay As String = ""
            Dim _SizePackQty As Integer = 0


            Dim _Str As String = ""
T1:
            For Each F1H As DataRow In _dtpack.Select(" sum(Total) = " & FNPackPerCaton.Value & "  ")



                _LastCartonNo = _LastCartonNo + 1
                For Each ColP As DataColumn In _dt.Columns

                    Select Case ColP.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                        Case Else



                            If Integer.Parse(Val(F1H.Item(ColP.ColumnName))) > 0 Then


                                For Each F1 As DataRow In _dtpack.Select(" sum(Total) = " & FNPackPerCaton.Value & "  ")


                                    For Each R As DataRow In _dtpack.Rows
                                        _FTColorway = R!FTColorway.ToString()
                                        _POLine = R!FTPOLine.ToString
                                        _FTSubOrderNo = R!FTSubOrderNo.ToString()
                                        _SizePackQty = Val(R.Item(ColP.ColumnName))
                                        _FTOrderNo = R!FTOrderNo.ToString()

                                        _Str = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' and FTSubOrderNo='" & _FTSubOrderNo & "' " 'and FTSubOrderNo='" & _FTSubOrderNo & "' 

                                        If _Str = "" Then
                                            _Str = "[" & ColP.ColumnName.ToString & "]>=" & Integer.Parse(Val(R.Item(ColP.ColumnName))) & ""
                                        Else
                                            _Str &= " AND [" & ColP.ColumnName.ToString & "]>=" & Integer.Parse(Val(R.Item(ColP.ColumnName))) & ""
                                        End If

                                        For Each R2 As DataRow In _dt.Select(_Str)

                                            If _SizePackQty > 0 Then
                                                _SizeQty = _SizePackQty

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
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColP.ColumnName.ToString) & "' "
                                                _Qry &= vbCrLf & "," & _SizePackQty & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "


                                                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD) Then
                                                    R2.Item(ColP) = R2.Item(ColP) - _SizePackQty
                                                End If





                                            End If


                                        Next



                                    Next



                                Next



                            End If








                    End Select

                Next

            Next

            Dim statebal As Boolean = False
            For Each R As DataRow In _dtpack.Rows

                If _dt.Select(_Str).Length > 0 Then
                    GoTo T1
                Else
                    statebal = False
                End If
            Next



            If statebal Then
                GoTo T1
            End If

            Call LoadOrderPackBreakDownCreateCarton(Me.FTPackNo)

        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
        _Spls.Close()
        Return True
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
            Dim _dtpackmerge As DataTable
            With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
                _dtpacktmp = .Copy
                _dtpackmerge = _dtpacktmp
            End With




            Dim _TotalPack As Integer = 0
            Dim _SumPack As Integer = 0

            Try
                With _dtpacktmp

                    _dtpack.Columns.Remove("Total")

                    For Each Col As DataColumn In .Columns
                        _SumPack = 0
                        Select Case Col.ColumnName.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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


            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
                _dtpacktmp = .Copy
            End With



            For Each Col As DataColumn In _dtpack.Columns
                _SumPack = 0
                _StateIns = True

                Do While (_StateIns)
                    _Qry = ""

                    Select Case Col.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                        Case Else

                            For Each R As DataRow In _dtpackmerge.Rows

                                '_FTOrderNo = R!FTOrderNo.ToString()
                                '_FTSubOrderNo = R!FTSubOrderNo.ToString()
                                _FTColorway = R!FTColorway.ToString()
                                _POLine = R!FTPOLine.ToString

                                If Val(R.Item(Col.ColumnName.ToString)) > 0 Then
                                    _PackQty = Val(R.Item(Col.ColumnName.ToString))

                                    Dim _Str As String = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' AND [" & Col.ColumnName.ToString & "]>=" & _PackQty & ""
                                    Dim _Str2 As String = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' "


                                    'AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' 
                                    Dim _dtn As DataTable = _dt.Select(_Str2, "FTSubOrderNo ASC").CopyToDataTable

                                    Dim tmpRow() As DataRow = _dt.Select(_Str2, "FTSubOrderNo ASC")
                                    If _dtn.Compute(" sum( [" & Col.ColumnName.ToString & "])", _Str2) >= _PackQty Then


                                        If tmpRow.Length > 0 Then

                                            For Each Rxp As DataRow In tmpRow
                                                _SizePackQty = Integer.Parse(Val(Rxp.Item(Col.ColumnName)))
                                                If _PackQty > 0 And _SizePackQty > 0 Then

                                                    Dim QtyUse As Integer = 0

                                                    _SizeQty = Integer.Parse(Val(Rxp.Item(Col.ColumnName)))
                                                    QtyUse = _PackQty
                                                    If _PackQty > _SizeQty And _SizeQty > 0 Then
                                                        QtyUse = _SizeQty
                                                    End If

                                                    _FTOrderNo = Rxp!FTOrderNo.ToString()
                                                    _FTSubOrderNo = Rxp!FTSubOrderNo.ToString()
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
                                                    _Qry &= vbCrLf & "," & QtyUse & " "
                                                    _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysCartonId.Properties.Tag.ToString)) & " "
                                                    _Qry &= vbCrLf & "," & Integer.Parse(Val(FNPackCartonSubType.SelectedIndex)) & " "
                                                    _Qry &= vbCrLf & "," & Me.FNPackPerCaton.Value & " "
                                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_POLine) & "' "
                                                    _SizeQty = _SizeQty - QtyUse
                                                    If _SizeQty <= 0 Then
                                                        _SizeQty = 0
                                                    End If

                                                    Rxp.Item(Col.ColumnName) = _SizeQty
                                                    _PackQty = _PackQty - QtyUse
                                                End If

                                            Next
                                        Else


                                            '_Str = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' AND [" & Col.ColumnName.ToString & "]>=" & _PackQty & ""
                                            ''AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "' 
                                            'Dim tmpRow2() As DataRow = _dt.Select(_Str, "FTSubOrderNo ASC")



                                            _StateIns = False
                                        End If


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
            Dim _dtpackmerge As DataTable
            With CType(Me.ogcsubprodpack.DataSource, DataTable)
                .AcceptChanges()
                _dtpack = .Copy
            End With

            If FTStateMerge.Checked Then
                With CType(Me.ogcsubprodpackmerge.DataSource, DataTable)
                    .AcceptChanges()
                    _dtpackmerge = .Copy
                End With
            End If
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
            For Each Row As DataRow In _dtpackmerge.Rows
                '_FTOrderNo = Row!FTOrderNo.ToString()
                '_FTSubOrderNo = Row!FTSubOrderNo.ToString()
                _FTColorway = Row!FTColorway.ToString()
                _POLine = Row!FTPOLine.ToString

                For Each ColP As DataColumn In _dtpackmerge.Columns

                    Select Case ColP.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
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
                        '_FTOrderNo = R!FTOrderNo.ToString()
                        '_FTSubOrderNo = R!FTSubOrderNo.ToString()
                        _FTColorway = R!FTColorCode.ToString()
                        _FTSizeCode = R!FTSizeCode.ToString
                        _POLine = R!FTPOLine.ToString

                        If Val(R!FNQuantity.ToString) > 0 Then
                            _PackQty = Val(R!FNQuantity.ToString)


                            Dim _Str As String = " FTPOLine ='" & HI.UL.ULF.rpQuoted(_POLine) & "' AND FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "' AND [" & _FTSizeCode & "]>=" & _PackQty & ""
                            'FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "'
                            Dim tmpRow() As DataRow = _dt.Select(_Str)
                            If tmpRow.Length > 0 Then
                                For Each Rxp As DataRow In tmpRow

                                    _SizePackQty = Integer.Parse(Val(Rxp.Item(_FTSizeCode)))
                                    If _SizePackQty >= _PackQty Then

                                        _SumPack = _SumPack + _PackQty

                                        _FTOrderNo = Rxp!FTOrderNo.ToString()
                                        _FTSubOrderNo = Rxp!FTSubOrderNo.ToString()

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
        FTStateMerge.Checked = True
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
                    'If (Me.FTStateMerge.Checked) Then
                    '    FNPackCartonScrapType.Enabled = True
                    'End If
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
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "FTSelect".ToUpper
                                Case "FTSelect".ToUpper
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
                        Case "FTSelect".ToUpper
                            oGridCol.Visible = False

                        Case Else
                            With oGridCol
                                If Not (Me.FTStateMerge.Checked) Then
                                    'FNPackCartonScrapType.Enabled = True
                                    .OptionsColumn.AllowEdit = True
                                    .OptionsColumn.ReadOnly = False
                                Else
                                    .OptionsColumn.AllowEdit = _StateEdit
                                    .OptionsColumn.ReadOnly = Not (_StateEdit)
                                End If



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

            With Me.ogvsubprodpackmerge
                .BeginInit()
                For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    Select Case oGridCol.FieldName.ToString.ToUpper
                        Case "FTColorway".ToUpper, "FTPOLine".ToUpper
                        Case "FTSelect".ToUpper
                            oGridCol.Visible = False
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



            If (Me.FTStateMerge.Checked) Then
                ogbpackcartondetail.Enabled = _StateEdit
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click
        If Me.VerifyData Then

            If _SetPackType = 0 Then
                'pack ตัว

                Select Case FNPackCartonSubType.SelectedIndex
                    Case 0
                        If (Me.FTStateMerge.Checked) Then
                            If Me.CreateCarton Then
                                Me.Process = True
                                Call CallLoadCarton()
                                CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                                If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                                    Me.Close()
                                End If
                            End If
                        Else
                            If Me.CreateCartonSolidmultiSubOrder Then
                                Me.Process = True
                                Call CallLoadCarton()
                                CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                                If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                                    Me.Close()
                                End If
                            End If
                        End If

                    Case 1
                        If (Me.FTStateMerge.Checked) Then
                            If Me.CreateCartonAssortOneColorMultiSize Then
                                Me.Process = True
                                Call CallLoadCarton()
                                CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                                If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                                    Me.Close()
                                End If
                            End If
                        Else
                            If Me.CreateCartonAssortOneColorMultiSize(1) Then
                                Me.Process = True
                                Call CallLoadCarton()
                                CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                                If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                                    Me.Close()
                                End If
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


            Else
                'pack set

                Select Case FNPackCartonSubType.SelectedIndex
                    Case 0
                        If (Me.FTStateMerge.Checked) Then
                            If Me.CreateCartonSet Then
                                Me.Process = True
                                Call CallLoadCarton()
                                CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                                If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                                    Me.Close()
                                End If
                            End If
                        Else
                            'If Me.CreateCartonSolidmultiSubOrder Then
                            '    Me.Process = True
                            '    Call CallLoadCarton()
                            '    CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                            '    If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                            '        Me.Close()
                            '    End If
                            'End If
                        End If

                    Case 1
                        If (Me.FTStateMerge.Checked) Then
                            If Me.CreateCartonAssortOneColorMultiSizeSet Then
                                Me.Process = True
                                Call CallLoadCarton()
                                CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                                If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                                    Me.Close()
                                End If
                            End If
                        Else
                            'If Me.CreateCartonAssortOneColorMultiSize(1) Then
                            '    Me.Process = True
                            '    Call CallLoadCarton()
                            '    CType(Me.ogcsubprod.DataSource, DataTable).AcceptChanges()
                            '    If CType(Me.ogcsubprod.DataSource, DataTable).Select("Total>0").Length <= 0 Then
                            '        Me.Close()
                            '    End If
                            'End If

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
            If FTStateMerge.Checked Then
                CType(ogcsubprodpackmerge.DataSource, DataTable).AcceptChanges()
                With Me.ogvsubprodpackmerge
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "Total".ToUpper, "FTSelect".ToUpper
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
                With Me.ogvsubprodpack
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "Total".ToUpper, "FTSelect".ToUpper
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
            Else
                With Me.ogvsubprodpack
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTPOLine".ToUpper, "Total".ToUpper, "FTSelect".ToUpper
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
            End If

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
                If Me.FTStateMerge.Checked Then
                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                        _Size = .FocusedColumn.FieldName.ToString()
                        _FTColorway = .GetFocusedRowCellValue("FTColorway")

                        If Not (_StateSumGrid) Then
                            ogvsubprodpackmerge.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))

                            If Not (ogcsubprodpackmerge.DataSource Is Nothing) Then
                                Call SumGrid()
                            End If

                        End If

                    End With
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

            End If


        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    'Private Function GenerateBarcodeSSCC(_SBarcodeNo As Integer, _EBarcodeNo As Integer, _BeginCarton As Integer) As String
    '    Try
    '        Dim _Cmd As String = "" : Dim _Seq As Integer = 0
    '        Dim _BarCodeSSS As String = "" : Dim _O, _M, _T As Integer : Dim _DemoBarcode As String = "" : Dim _BarCode As String = ""

    '        _Cmd = "SELECT TOP (1) FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSESystemConfig WITH(NOLOCK) WHERE (FTCfgName = N'CfManufacturerNo')"
    '        Dim _FacNo As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "")

    '        '_Cmd = "Select Max(FNCartonNo) AS FNCartonNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
    '        '_Cmd &= vbCrLf & "Where FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
    '        '_Cmd &= vbCrLf & "and isnull(FTBarCodeEAN13,'') <> ''"
    '        '_Seq = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
    '        _Seq = _BeginCarton



    '        For I As Integer = _SBarcodeNo To _EBarcodeNo
    '            '   _Cmd = "Select Top 1 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode Where FTCartonNo='" & I & "'"
    '            ' If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

    '            _BarCodeSSS = _FacNo & Microsoft.VisualBasic.Right("000000000" & CStr(I), 9)

    '            _O = 0 : _M = 0 : _T = 0
    '            For x As Integer = 1 To 16
    '                _DemoBarcode = _BarCodeSSS
    '                If (x Mod 2) = 0 Then
    '                    _M += +CInt(_DemoBarcode.Substring(x - 1, 1))
    '                Else
    '                    _O += +CInt(_DemoBarcode.Substring(x - 1, 1))
    '                End If
    '            Next
    '            _M = _M * 3 : _T = _M + _O : _T = _T Mod 10
    '            If _T > 0 Then
    '                _T = 10 - _T
    '            End If
    '            _BarCode = "000" & _BarCodeSSS & CStr(_T)


    '            ' End If
    '            _Seq += +1
    '        Next
    '        Return _BarCode

    '    Catch ex As Exception
    '        Return ""

    '    End Try
    'End Function

    'Private Function GenBarcodeEN13(PackNo As String) As Boolean
    '    Try
    '        Dim _Cmd As String = "" : Dim _EN13 As String = "" : Dim _CartonNO As String = ""
    '        Dim _CustomerPO As DataTable
    '        _Cmd = "Select    FTCustomerPO from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack  where FTPackNo  = '" & PackNo & "' "
    '        _CustomerPO = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

    '        Dim _oDt As System.Data.DataTable

    '        For Each Rz As DataRow In _CustomerPO.Rows
    '            _Cmd = "SELECT   C.FTColorway, C.FTSizeBreakDown, C.FTOrderNo, C.FTPackNo,  C.FTPOLine  , C.FTSubOrderNo, PK.FTCustomerPO, C.FNCartonNo, D.FTSerialFrom , D.FTSerialTo  , D.FNFrom , D.FNTo ,C.FNQuantity"
    '            _Cmd &= vbCrLf & " , convert(nvarchar(30) , convert(int ,D.FTSerialFrom ) + ROW_NUMBER() Over (partition by C.FTOrderNo , C.FTSubOrderNo, C.FTPOLine ,C.FTPackNo,C.FTColorway, C.FTSizeBreakDown ,PK.FTCustomerPO ,C.FNQuantity ORder by  C.FTPackNo,C.FNCartonNo) -1 )AS FNCartonSeq  "
    '            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS C  LEFT OUTER JOIN "
    '            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS  PK WITH(NOLOCK)    ON C.FTPackNo = PK.FTPackNo INNER JOIN    "
    '            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. TEXPTPackPlan_D as D  ON PK.FTCustomerPO = D.FTPORef and C.FTPOLine  = convert(nvarchar(30), convert(int, D.FTPOLineNo)) "
    '            _Cmd &= vbCrLf & " and C.FTSizeBreakDown = D.FTSizeBreakDown and    C.FTColorway= replace(replace(D.FTShortDescription,D.FTStyleCode,''),'-','')  and C.FNQuantity = D.FNQtyPerPack "

    '            _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS  T WITH(NOLOCK)    ON  C.FTPackNo = T.FTPackNo AND C.FNCartonNo = T.FNCartonNo     "
    '            _Cmd &= vbCrLf & "WHERE  (PK.FTCustomerPO = N'" & Rz!FTCustomerPO.ToString & "')" 'ISNULL(T.FTState,'0') = '0' and 
    '            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


    '            For Each R As DataRow In _oDt.Rows
    '                _EN13 = HI.UL.ULF.rpQuoted(GenerateBarcodeSSCC(R!FNCartonSeq.ToString, R!FNCartonSeq.ToString, R!FNCartonNo.ToString))
    '                _Cmd = " Select  FTBarCodeEAN13 From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode  "
    '                _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
    '                _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
    '                _Cmd &= vbCrLf & " and isnull(FTBarCodeEAN13,'') <>'' "
    '                If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count <= 0 Then
    '                    _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode"
    '                    _Cmd &= vbCrLf & "Set  FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '                    _Cmd &= vbCrLf & ",FTCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
    '                    _Cmd &= vbCrLf & ",FTBarCodeEAN13='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
    '                    _Cmd &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
    '                    _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
    '                    _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
    '                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
    '                        _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode (FTInsUser,FDInsDate,FTUpdTime,FTCartonNo,FTBarCodeEAN13,FTBarCodeCarton,FTPackNo,FNCartonNo)"
    '                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
    '                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
    '                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
    '                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
    '                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
    '                        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)
    '                    End If
    '                End If
    '            Next
    '        Next
    '        _Cmd = "exec  dbo.sp_updatebarcodeucc '" & PackNo & "'"
    '        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)


    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function


    Private Function GenBarcodeEN13(packno As String) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _EN13 As String = "" : Dim _CartonNO As String = ""
            Dim _oDt As System.Data.DataTable



            _Cmd = "SELECT PL.FTPONo,   D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown, D.FNQuantity, D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine,   P.FTCustomerPO, P.FNHSysStyleId "
            _Cmd &= vbCrLf & ", convert(nvarchar(30) , convert(int ,PL.FTSerialFrom ) + ROW_NUMBER() "
            _Cmd &= vbCrLf & "  Over (partition by   D.FTPOLine ,P.FTPackNo,D.FTColorway, D.FTSizeBreakDown ,P.FTCustomerPO ,D.FNQuantity "
            _Cmd &= vbCrLf & " ORder by  P.FTPackNo,D.FNCartonNo) -1 )AS FNCartonSeq  ,isnull(PL.FTSerialFrom,'') as FTSerialFrom "

            _Cmd &= vbCrLf & " FROM "
            _Cmd &= vbCrLf & " (  select  D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown, sum(D.FNQuantity) as FNQuantity "
            _Cmd &= vbCrLf & ", D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine "
            _Cmd &= vbCrLf & "from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail D  with(nolock)"
            _Cmd &= vbCrLf & " where  (D.FTPackNo = N'" & HI.UL.ULF.rpQuoted(packno) & "')"
            _Cmd &= vbCrLf & " group by    D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown , D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine) AS D INNER JOIN  "

            _Cmd &= vbCrLf & "     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack AS P with(nolock) ON D.FTPackNo = P.FTPackNo "
            _Cmd &= vbCrLf & "   INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo. TEXPTPackPlan_D as PL ON P.FTCustomerPO = PL.FTPORef "
            _Cmd &= vbCrLf & "   and D.FTPOLine  = convert(nvarchar(30), convert(int, PL.FTPOLineNo)) "
            _Cmd &= vbCrLf & "     and D.FTSizeBreakDown = PL.FTSizeBreakDown  "
            _Cmd &= vbCrLf & " and D.FTColorway= replace(replace(PL.FTShortDescription,PL.FTStyleCode,''),'-','')   "
            If SetPackType = 0 Then
                _Cmd &= vbCrLf & "  and  (D.FNQuantity ) =  PL.FNQtyPerPack * case when  pl.FNInnerPackCount <=0 then 1  else pl.FNInnerPackCount end  "
            Else
                _Cmd &= vbCrLf & "  and  (D.FNQuantity /" & FNPackSetValue & ") =  PL.FNQtyPerPack * case when  pl.FNInnerPackCount <=0 then 1  else pl.FNInnerPackCount end  "
            End If

            _Cmd &= vbCrLf & "   INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo. TEXPTPackPlan  as PD ON PL.FTPckPlanNo = PD.FTPckPlanNo AND PL.FTPORef = PD.FTPORef AND PL.FTPORefNo = PD.FTPORefNo "
            _Cmd &= vbCrLf & "WHERE  (D.FTPackNo = N'" & HI.UL.ULF.rpQuoted(packno) & "')"
            _Cmd &= vbCrLf & "  and PD.FTApproveState = '1' "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            For Each R As DataRow In _oDt.Rows
                _EN13 = HI.UL.ULF.rpQuoted(GenerateBarcodeSSCCEN13(R!FNCartonSeq.ToString, R!FNCartonSeq.ToString, R!FNCartonNo.ToString, R!FTPONo.ToString))
                '_Cmd = " Select  FTBarCodeEAN13 From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode  "
                '_Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                '_Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
                '_Cmd &= vbCrLf & " and isnull(FTBarCodeEAN13,'') <>'' "
                'If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count <= 0 Then
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
                'End If
            Next



            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function GenerateBarcodeSSCCEN13(_SBarcodeNo As Integer, _EBarcodeNo As Integer, _BeginCarton As Integer, _Prefix As String) As String
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

            If _Prefix <> "" Then
                _FacNo = Str(_Prefix) & _FacNo
                _FacNo = Replace(_FacNo, " ", "")
            End If

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

                If _Prefix <> "" Then
                    _BarCode = "00" & _BarCodeSSS & CStr(_T)
                Else
                    _BarCode = "000" & _BarCodeSSS & CStr(_T)
                End If

                ' End If
                _Seq += +1
            Next



            Return _BarCode

        Catch ex As Exception
            Return ""

        End Try
    End Function


    Private Sub FTStateMerge_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateMerge.CheckedChanged
        Try
            Me.ogcsubprodpack.Visible = IIf(Me.FTStateMerge.Checked, False, True)
            Me.ogcsubprodpackmerge.Visible = IIf(Me.FTStateMerge.Checked, True, False)

            ogbpackcartondetail.Enabled = Not (Me.FTStateMerge.Checked)
            FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)
        Catch ex As Exception
        End Try
    End Sub
End Class