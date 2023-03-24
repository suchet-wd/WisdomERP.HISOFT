Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraTreeList
Imports System.IO

Public Class wCreatePackOrderCustPONonSty

    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _GenPackOrder As wGeneratePackOrderCustPONonSty
    Private _GenPackCarton As wGenerateCartonNonSty
    Private _StateSubNew As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _GenPackOrder = New wGeneratePackOrderCustPONonSty
        HI.TL.HandlerControl.AddHandlerObj(_GenPackOrder)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenPackOrder.Name.ToString.Trim, _GenPackOrder)
        Catch ex As Exception
        Finally
        End Try

        _GenPackCarton = New wGenerateCartonNonSty
        HI.TL.HandlerControl.AddHandlerObj(_GenPackCarton)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenPackCarton.Name.ToString.Trim, _GenPackCarton)
        Catch ex As Exception
        Finally
        End Try

        Me.PrepareForm()
        Call InitGrid()
        Call TabChenge()
        Call SetImagePack()
    End Sub

    Private Sub SetImagePack()

        If IO.File.Exists(tW_SysPath & "\Pack\Packing.jpg") And IO.File.Exists(tW_SysPath & "\Pack\FullCarton.jpg") _
            And IO.File.Exists(tW_SysPath & "\Pack\CartonAssort.jpg") And IO.File.Exists(tW_SysPath & "\Pack\Scrap.jpg") Then

            imagepackList.Images.Clear()
            Dim tPathImgDis As String = tW_SysPath & "\Pack\Packing.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))

            End If

            tPathImgDis = tW_SysPath & "\Pack\FullCarton.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))

            End If

            tPathImgDis = tW_SysPath & "\Pack\CartonAssort.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))

            End If

            tPathImgDis = tW_SysPath & "\Pack\Scrap.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

        End If

    End Sub

    Private Sub TabChenge()

        ocmaddsuborder.Visible = (otbdetail.SelectedTabPage.Name = otppackdetail.Name)
        ocmdeletesuborder.Visible = (otbdetail.SelectedTabPage.Name = otppackdetail.Name)
        ocmsaveweightpack.Visible = (otbdetail.SelectedTabPage.Name = otppackdetail.Name)

        ocmgeneratecarton.Visible = (otbdetail.SelectedTabPage.Name = otppackdetailcarton.Name)
        ocmdeletecarton.Visible = (otbdetail.SelectedTabPage.Name = otppackdetailcarton.Name)
        ocmdeleteallcarton.Visible = (otbdetail.SelectedTabPage.Name = otppackdetailcarton.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub InitGrid()

        With ogvsubpackdetail
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvpackdetail
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvpackdetailWeight
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvpweight
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvptotalweight
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvptotalpack
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
        With ogvptotalpackset
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvpgrandpack
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvpgrandpackset
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvppercarton
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvppercartonset
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub

    Private Sub ClearGrid()

        With Me.ogvsubpackdetail
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

        With Me.ogvpackdetail
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With

        With Me.ogvpackdetailWeight
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With


        ogcsubpackdetail.DataSource = Nothing
        ogcpackdetail.DataSource = Nothing
        ogcpackdetailWeight.DataSource = Nothing

        Me.otbsuborder.TabPages.Clear()

    End Sub

    Private Sub LoadTotalOrderPackBreakDownCreateCarton(Key As Object)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_TotalOrderPackBreakDown_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        With Me.ogvptotalpack

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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

        Me.ogcptotalpack.DataSource = _dt.Copy


        LoadTotalOrderPackBreakDownCreateCartonSet(Key)


        _dt.Dispose()
        _dtpack.Dispose()

    End Sub


    Private Sub LoadTotalOrderPackBreakDownCreateCartonSet(Key As Object)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_TotalOrderPackBreakDown_NonStySet '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        With Me.ogvptotalpackset

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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



        Me.ogcptotalpackset.DataSource = _dt.Copy


        _dt.Dispose()
        _dtpack.Dispose()

    End Sub




    Private Sub LoadrderPackBreakDownCartonBefore(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton_Before_CustPO_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        With Me.ogvpgrandpack

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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

        Me.ogcpgrandpack.DataSource = _dt.Copy
        LoadrderPackBreakDownCartonBeforeset(Key, CartonNo)

        _dt.Dispose()
        _dtpack.Dispose()

    End Sub


    Private Sub LoadrderPackBreakDownCartonBeforeset(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton_Before_CustPO_NonStySet '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        With Me.ogvpgrandpackset

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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


        Me.ogcpgrandpackset.DataSource = _dt.Copy

        _dt.Dispose()
        _dtpack.Dispose()

    End Sub




    Private Sub LoadrderPackBreakDownCarton(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        _dt = Me.ogcppercarton.DataSource
        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        With Me.ogvppercarton

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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

        Me.ogcppercarton.DataSource = _dt.Copy
        LoadrderPackBreakDownCartonset(Key, CartonNo)

        _dt.Dispose()
        _dtpack.Dispose()

    End Sub

    Private Sub LoadrderPackBreakDownCartonset(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        _dt = Me.ogcppercarton.DataSource
        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton_NonStySet '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        With Me.ogvppercartonset
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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

        Me.ogcppercartonset.DataSource = _dt.Copy
        _dt.Dispose()
        _dtpack.Dispose()

    End Sub


    Private Sub LoadrderPackBreakDownCartonWeight(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton_Weight_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With _dt
        '    .Columns.Add("FTNikePOLineItem", GetType(String))
        'End With
        '_dt.BeginInit()
        'For Each R As DataRow In _dt.Rows
        '    R!FTNikePOLineItem = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        'Next
        '_dt.EndInit()

        _dtpack = _dt.Copy

        With Me.ogvpweight

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                                .DisplayFormat.FormatString = "{0:n3}"
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
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n3}"

                    End Select

                Next

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next

            End If

        End With

        Me.ogcpweight.DataSource = _dt.Copy

        _dt.Dispose()
        _dtpack.Dispose()

    End Sub

    Private Sub LoadrderPackBreakDownCartonTotalWeight(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton_TotalWeight_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dtpack = _dt.Copy
        'With _dt
        '    .Columns.Add("FTNikePOLineItem", GetType(String))
        'End With
        '_dt.BeginInit()
        'For Each R As DataRow In _dt.Rows
        '    R!FTNikePOLineItem = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        'Next
        '_dt.EndInit()

        With Me.ogvptotalweight

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
                                .DisplayFormat.FormatString = "{0:n3}"
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
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n3}"

                    End Select

                Next

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                Next

            End If

        End With

        Me.ogcptotalweight.DataSource = _dt.Copy

        _dt.Dispose()
        _dtpack.Dispose()

    End Sub

#Region "Property"


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

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
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

    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
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

    Private Sub PrepareForm()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1
        Dim _CtrHeight As Double = 0
        Dim _dtgrpobj As New DataTable

        '------ Get Form Object ID-------------------
        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='wCreatePackOrder' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

            '------ Get Form Object Gen Grid-------------------
            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then

                For Each Row As DataRow In _dtgrpobj.Rows
                    Select Case Row!FNGenFormObj.ToString
                        Case "H"
                            Dim _ch As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
                            _ch.SysObjID = Val(Row!FNFormObjID.ToString)
                            _ch.SysTableName = Row!FTTableName.ToString
                            _ch.SysDBName = Row!FTBaseName.ToString
                            _FormHeader.Add(_ch)

                    End Select
                Next
            End If

        End If

        _dt.Dispose()
        _dtgrpobj.Dispose()

    End Sub

    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        Dim _FieldName As String = ""
        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With

                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try

                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next

        'Call LoadOrderPackBreakDown(Key.ToString)
        Call LoadOrderPackDetail(Key.ToString)

        _ProcLoad = False
    End Sub

    Private Function GetFTNikePOLineItem(_SubOrderNo As String, _Colorway As String) As String
        Try
            Dim dt As DataTable
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 isnull(FTNikePOLineItem,'') AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"

            If (HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "") = "") Then
                _Cmd = "Select  Top 1 isnull(FTPOLine,'') AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "Where FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")
            End If
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub LoadOrderPackBreakDown(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CustPO_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        'With _dt
        '    .Columns.Add("FTNikePOLineItem", GetType(String))
        'End With
        '_Qry = "SELECT Top 1 FTSubOrderNo"
        '_Qry &= vbCrLf & "  FROM   TPACKOrderPack_Detail WITH(NOLOCK) "
        '_Qry &= vbCrLf & " WHERE        (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
        'Dim _SubOrderNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")
        '_dt.BeginInit()
        'For Each R As DataRow In _dt.Rows
        '    Dim _POLine As String = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        '    If _POLine <> "" Then
        '        R!FTNikePOLineItem = _POLine
        '    End If

        'Next
        '_dt.EndInit()

        With Me.ogvpackdetail

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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

        Me.ogcpackdetail.DataSource = _dt.Copy

        Dim dtbarcodeset As DataTable
        dtbarcodeset = _dt.Copy

        With Me.ogvbreakdownbarcodeset
            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (dtbarcodeset Is Nothing) Then
                For Each Col As DataColumn In dtbarcodeset.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        Case Else
                            _colcount = _colcount + 1

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "GBarcodeSet" & Col.ColumnName.ToString
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

                                .ColumnEdit = RepCalSet

                                With .OptionsColumn

                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = True
                                    .ReadOnly = False

                                End With
                                .AppearanceCell.BackColor = Color.LightCyan
                                .AppearanceCell.ForeColor = Color.Blue

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 60
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

            For Each Row As DataRow In dtbarcodeset.Rows
                For Each Col As DataColumn In dtbarcodeset.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        Case Else
                            Row.Item(Col.ColumnName) = 0
                    End Select
                Next
            Next

        End With


        Me.ogcbreakdownbarcodeset.DataSource = dtbarcodeset.Copy

    End Sub

    Private Sub LoadOrderPackBreakDownWeight(Key As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDownWeight_CustPO_NonSty '" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With _dt
        '    .Columns.Add("FTNikePOLineItem", GetType(String))
        'End With
        '_dt.BeginInit()
        'For Each R As DataRow In _dt.Rows
        '    R!FTNikePOLineItem = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        'Next
        '_dt.EndInit()

        With Me.ogvpackdetailWeight

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                                If Not (Col.ColumnName.ToString = "Total") Then

                                    .ColumnEdit = ReposCaleditWeight
                                Else

                                End If

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n4}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                    If Not (Col.ColumnName.ToString = "Total") Then
                                        .AllowEdit = True
                                        .ReadOnly = False
                                    Else
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End If


                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 60
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n4}"

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

        Me.ogcpackdetailWeight.DataSource = _dt.Copy

    End Sub

    Public Sub DefaultsData()
        Dim _FieldName As String
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString

                                HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)

                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)

                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .SelectedIndex = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Checked = (_FormHeader(cind).DefaultsData(I).DataDefaults.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                Try
                                    .DateTime = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                Catch ex As Exception
                                    .Text = ""
                                End Try

                            End With
                        Case ENM.Control.ControlType.MemoEdit
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case ENM.Control.ControlType.TextEdit
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case Else
                    End Select
                Next
            Next
        Next
        Me.otbsuborder.TabPages.Clear()
    End Sub

    Private Function CheckNotUsed(Key As String) As Boolean
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckDelFiled.ToArray.Count - 1
                _Str = _FormHeader(cind).CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                    HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                    Return False
                End If

            Next
        Next
        Return True
    End Function

    Private Function VerrifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
                _Caption = ""

                For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)

                    If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
                        _Caption = ObjCaption.Text
                        Exit For
                    End If
                Next

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
                                    End If
                                End If
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                If Val(.Value.ToString) <= 0 Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If .SelectedIndex < 0 Then Pass = False
                            End With
                        Case ENM.Control.ControlType.CheckEdit

                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If .Image Is Nothing Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            If Obj.Text = "" Then
                                Pass = False
                            End If
                        Case Else
                            Pass = False
                    End Select

                    If Pass = False Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                        Obj.Focus()
                        Return False
                    End If
                Next
            Next
        Next


        '---------- Validate Document ---------------------
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else
                                Dim _CmpH
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If FTCustomerPO.Text.Trim() = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FTCustomerPO_lbl.Text)
            FTCustomerPO.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function SaveData() As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _CmpH As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else


                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString

        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
                    _FoundControl = False
                    If (_StateNew) Then

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else
                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            If _Values <> "" Then _Values &= ","
                            If _Fields <> "" Then _Fields &= ","

                            _Fields &= _FieldName

                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate")
                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
                                    _Values &= "''"
                                Case UCase("FTInsTime")
                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FTInsUser")
                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If


                    Else

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case Else
                                    If _Values <> "" Then _Values &= ","
                            End Select

                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FTUpdTime")
                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case UCase("FTUpdUser")
                                    _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If

                    End If

                Next

                If (_StateNew) Then
                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next

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

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

    Private Sub LoadData(HSysId As String)
        Dim _Str As String = Me.Query & "  WHERE " & Me.MainKey & "='" & HSysId & "' "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                '.Properties.Tag = R.Item(Col).ToString
                                .Text = R.Item(Col).ToString
                                Call HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                Try
                                    .DateTime = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Text = ""
                                End Try
                            End With

                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next

    End Sub

    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)
        otbsuborder.TabPages.Clear()
        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
    End Sub

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        'If Me.CheckCreateCarton(Me.FTPackNo.Text) Then

        If Me.VerrifyData Then
            If Me.SaveData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If

        'Else
        '    HI.MG.ShowMsg.mInfo("พบการสร้างกล่องแล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1411200113, Me.Text, , MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If Me.CheckCreateCarton(Me.FTPackNo.Text) Then
            If Me.FTPackNo.Text.Trim <> "" And Me.FTPackNo.Properties.Tag.ToString <> "" Then
                Dim _Qry As String = ""

                _Qry = "   Select TOP 1  B.FTPackNo"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH(NOLOCK)  ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo"
                _Qry &= vbCrLf & "  WHERE   B.FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.FTPackNo.Text, Me.Text) = True Then
                        If Me.DeleteData() Then
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                            HI.TL.HandlerControl.ClearControl(Me)
                            Me.DefaultsData()
                        Else
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        End If
                    End If

                Else

                    HI.MG.ShowMsg.mInfo("เอกสารถูกนำไป Scam รับ แล้ว ไม่สามารถทำการลบได้ !!!", 1408040001, Me.Text, , MessageBoxIcon.Warning)
                    FTPackNo.Focus()
                    FTPackNo.SelectAll()

                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTPackNo_lbl.Text)
                Me.FTPackNo.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInfo("พบการสร้างกล่องแล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1411200113, Me.Text, , MessageBoxIcon.Warning)
        End If



    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTPackNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "PackOrderSlip.rpt"
                .Formular = "{TPACKOrderPack.FTPackNo}='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
            FTPackNo.Focus()
        End If
    End Sub

    Private Sub ocmpreviewbarcodeutc_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodeutc.Click
        Try
            If Me.FTPackNo.Text <> "" Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Production\"
                    .ReportName = "PackOrderSlip_BarcodeUtc.rpt"
                    .Formular = "{TPACKOrderPack.FTPackNo}='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "
                    .Preview()
                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
                FTPackNo.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Sub LoadOrderPackDetail(Key As Object)
        Dim _Qry As String = ""
        Dim _dtprod As DataTable
        otbsuborder.TabPages.Clear()

        _Qry = "SELECT  FTSubOrderNo   "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Group By  FTSubOrderNo  "
        _Qry &= vbCrLf & "  Order By  FTSubOrderNo  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTSubOrderNo.ToString
                .Text = R!FTSubOrderNo.ToString
            End With

            otbsuborder.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbsuborder.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()

        Call LoadOrderPackBreakDown(Key)
        Call LoadOrderPackBreakDownWeight(Key.ToString)

    End Sub

    Private Sub LoadOrderPackSubBreakDown(SubOrderNo As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackSubBreakDown_CustPO_NonSty '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "','" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With _dt
        '    .Columns.Add("FTNikePOLineItem", GetType(String))
        'End With
        '_dt.BeginInit()
        'For Each R As DataRow In _dt.Rows
        '    R!FTNikePOLineItem = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)
        'Next
        '_dt.EndInit()

        With Me.ogvsubpackdetail

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
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
        otbdetail.SelectedTabPageIndex = 0
        Me.ogcsubpackdetail.DataSource = _dt
    End Sub

    Private Function DeleteSubOrder(Optional StateAll As Boolean = False) As Boolean

        ' CType(ogcmark.DataSource, DataTable).AcceptChanges()

        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail "
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "

            If Not (StateAll) Then
                _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.otbsuborder.SelectedTabPage.Name.ToString) & "' "
            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

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

    Public Sub CreateTreeCarton()
        With Me.otlpack
            .ClearNodes()
            .Columns.Clear()

            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add() : .Columns.Add()
            .Columns.Add()

            With .Columns.Item(0)
                .Name = "ColKey"
                .Caption = "FTCartonName"
                .FieldName = "FTCartonName"
                .Visible = True
            End With

            With .Columns.Item(1)
                .Name = "FNCartonNo"
                .Caption = "FNCartonNo"
                .FieldName = "FNCartonNo"
                .Visible = False
            End With

            With .Columns.Item(2)
                .Name = "FNQuantity"
                .Caption = "FNQuantity"
                .FieldName = "FNQuantity"
                .Visible = False
            End With

            With .Columns.Item(3)
                .Name = "FNNetWeight"
                .Caption = "FNNetWeight"
                .FieldName = "FNNetWeight"
                .Visible = False
            End With

            With .Columns.Item(4)
                .Name = "FNHSysCartonId"
                .Caption = "FNHSysCartonId"
                .FieldName = "FNHSysCartonId"
                .Visible = False
            End With

            With .Columns.Item(5)
                .Name = "FTCartonCode"
                .Caption = "FTCartonCode"
                .FieldName = "FTCartonCode"
                .Visible = False
            End With

            With .Columns.Item(6)
                .Name = "FNWeight"
                .Caption = "FNWeight"
                .FieldName = "FNWeight"
                .Visible = False
            End With

            With .Columns.Item(7)
                .Name = "FNPackCartonSubType"
                .Caption = "FNPackCartonSubType"
                .FieldName = "FNPackCartonSubType"
                .Visible = False
            End With

            With .Columns.Item(8)
                .Name = "FNPackPerCarton"
                .Caption = "FNPackPerCarton"
                .FieldName = "FNPackPerCarton"
                .Visible = False
            End With

            With .OptionsView
                .ShowColumns = False
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None

        End With

        FNCartonNo.Value = 0
        HI.TL.HandlerControl.ClearControl(ogbcarton)
        Call InitNodeCarton(Me.otlpack, Nothing)
        Me.otlpack.ExpandAll()

    End Sub

    Private Sub InitNodeCarton(ByVal _Lst As DevExpress.XtraTreeList.TreeList, ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode)

        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try
            If (_Node Is Nothing) Then
                node = _Lst.AppendNode(New Object() {Me.FNCartonNo3_lbl.Text & "", "-1", "", "", "", "", "", "", ""}, _Node)
            End If

            If (_Node Is Nothing) Then
                node.ImageIndex = 0
                Try
                    node.HasChildren = True
                    node.Tag = True

                    Dim dt As DataTable

                    Dim _Qry As String = ""
                    _Qry = " SELECT A.FTPackNo, A.FNCartonNo"
                    _Qry &= vbCrLf & "  , Sum(A.FNQuantity) AS FNQuantity"
                    _Qry &= vbCrLf & "   ,SUM(Convert(numeric(18,3),A.FNQuantity*B.FNWeight)) AS FNNetWeight "
                    _Qry &= vbCrLf & "   ,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"

                    _Qry &= vbCrLf & "   ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.fn_Get_Carton_Info(A.FTPackNo,A.FNCartonNo) AS FTCartonInfo"
                    _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK) INNER JOIN "
                    _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS B WITH(NOLOCK) "
                    _Qry &= vbCrLf & "    ON A.FTPackNo = B.FTPackNo "
                    _Qry &= vbCrLf & "    AND A.FTOrderNo=B.FTOrderNo"
                    _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                    _Qry &= vbCrLf & "    AND A.FTColorway = B.FTColorway"
                    _Qry &= vbCrLf & "    AND A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN "
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS CT WITH(NOLOCK)"
                    _Qry &= vbCrLf & "    ON A.FNHSysCartonId = CT.FNHSysCartonId "
                    _Qry &= vbCrLf & "   WHERE  A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Qry &= vbCrLf & "   GROUP BY  A.FTPackNo, A.FNCartonNo,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"
                    _Qry &= vbCrLf & "   ORDER BY A.FNCartonNo"

                    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    For Each R As DataRow In dt.Rows

                        nodeChild = _Lst.AppendNode(New Object() {Me.FNCartonNo2_lbl.Text & "" & R!FNCartonNo.ToString & " (" & R!FTCartonInfo.ToString & ")", R!FNCartonNo.ToString, R!FNQuantity.ToString, R!FNNetWeight.ToString, R!FNHSysCartonId.ToString, R!FTCartonCode.ToString, R!FNWeight.ToString, R!FNPackCartonSubType.ToString, R!FNPackPerCarton.ToString}, node)
                        nodeChild.HasChildren = False

                        Select Case True
                            Case Val(R!FNQuantity.ToString) < Val(R!FNPackPerCarton.ToString)
                                nodeChild.ImageIndex = 3
                            Case Else
                                If Val(R!FNPackCartonSubType) = 0 Then
                                    nodeChild.ImageIndex = 1
                                Else
                                    nodeChild.ImageIndex = 2
                                End If

                        End Select
                    Next

                Catch ex As Exception
                End Try

            Else
                node.HasChildren = False
            End If

        Catch
        End Try
        '_Lst.EndUnboundLoad()
    End Sub

    Private Function SaveWeight() As Boolean

        With CType(Me.ogcpackdetailWeight.DataSource, DataTable)

            Dim _FTOrderNo As String = ""
            Dim _FTSubOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _Qry As String = ""

            For Each R As DataRow In .Rows

                _FTOrderNo = R!FTOrderNo.ToString()
                _FTSubOrderNo = R!FTSubOrderNo.ToString()
                _FTColorway = R!FTColorway.ToString()

                For Each Col As DataColumn In .Columns
                    Select Case Col.ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper

                        Case "Total"
                        Case Else
                            If _FTSubOrderNo <> "" Then
                                _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail"
                                _Qry &= vbCrLf & " SET FNWeight=" & Val(R.Item(Col)) & ""
                                _Qry &= vbCrLf & " WHERE  (FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                                _Qry &= vbCrLf & "  AND (FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "') "
                                _Qry &= vbCrLf & "  AND (FTColorway = '" & HI.UL.ULF.rpQuoted(_FTColorway) & "') "
                                _Qry &= vbCrLf & "  AND (FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "') "

                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            End If
                    End Select
                Next
            Next
        End With

        Return True
    End Function

    Private Function CheckCreateCarton(PackNoKey As String) As Boolean
        Dim _Qry As String = ""

        _Qry = "  SELECT TOP 1 FTPackNo "
        _Qry &= vbCrLf & "   FROM TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(PackNoKey) & "') "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "")
    End Function

    Private Function CheckScan(PackNoKey As String, CartonNoKey As Integer) As Boolean
        Dim _Qry As String = ""

        _Qry = "  SELECT TOP 1 FTPackNo "
        _Qry &= vbCrLf & "   FROM TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(PackNoKey) & "') "
        _Qry &= vbCrLf & "  AND (FNCartonNo =" & CartonNoKey & ")"

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "")
    End Function

    Private Function DeleteCarton(PackNoKey As String, CartonNoKey As Integer) As Boolean
        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            'If Not UpdateDeleteCarton(HI.UL.ULF.rpQuoted(PackNoKey), Integer.Parse(Val(CartonNoKey))) Then
            '    HI.Conn.SQLConn.Tran.Rollback()
            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '    Return False
            'End If

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail "
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(PackNoKey) & "' "
            _Qry &= vbCrLf & " AND FNCartonNo=" & Integer.Parse(Val(CartonNoKey)) & " "


            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode "
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(PackNoKey) & "' "
            _Qry &= vbCrLf & " AND FNCartonNo=" & Integer.Parse(Val(CartonNoKey)) & " "


            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


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


    Private Function UpdateDeleteCarton(PackNoKey As String, CartonNoKey As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Update A "
            _Cmd &= vbCrLf & " set A.FNQuantity =  A.FNQuantity - B.FNQuantity "
            _Cmd &= vbCrLf & "  from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack_Detail A  inner join  "
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack_Carton_Detail B on a.FTPackNo = b.FTPackNo"
            _Cmd &= vbCrLf & " and a.FTColorway = b.FTColorway"
            _Cmd &= vbCrLf & " and a.FTSizeBreakDown = b.FTSizeBreakDown"
            _Cmd &= vbCrLf & " and a.FTOrderNo = b.FTOrderNo"
            _Cmd &= vbCrLf & " and a.FTSubOrderNo = b.FTSubOrderNo"
            _Cmd &= vbCrLf & " and a.FTPOLine = b.FTPOLine"
            _Cmd &= vbCrLf & " where a.FTPackNo = '" & PackNoKey & "' "
            _Cmd &= vbCrLf & " and b.FNCartonNo = " & CartonNoKey

            _Cmd &= vbCrLf & "Delete from  A "
            _Cmd &= vbCrLf & "  from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".. TPACKOrderPack_Detail A  inner join  "
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack_Carton_Detail B on a.FTPackNo = b.FTPackNo"
            _Cmd &= vbCrLf & " and a.FTColorway = b.FTColorway"
            _Cmd &= vbCrLf & " and a.FTSizeBreakDown = b.FTSizeBreakDown"
            _Cmd &= vbCrLf & " and a.FTOrderNo = b.FTOrderNo"
            _Cmd &= vbCrLf & " and a.FTSubOrderNo = b.FTSubOrderNo"
            _Cmd &= vbCrLf & " and a.FTPOLine = b.FTPOLine"
            _Cmd &= vbCrLf & " where a.FTPackNo = '" & PackNoKey & "' "
            _Cmd &= vbCrLf & " and b.FNCartonNo = " & CartonNoKey
            _Cmd &= vbCrLf & ""
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteAllCarton(PackNoKey As String) As Boolean
        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE A "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B  "
            _Qry &= vbCrLf & "  ON A.FTPackNo = B.FTPackNo "
            _Qry &= vbCrLf & "  AND A.FNCartonNo = B.FNCartonNo "
            _Qry &= vbCrLf & "  AND A.FTOrderNo = B.FTOrderNo "
            _Qry &= vbCrLf & "  AND A.FTSubOrderNo = B.FTSubOrderNo "
            _Qry &= vbCrLf & "  AND A.FTColorway = B.FTColorway "
            _Qry &= vbCrLf & "  AND A.FTSizeBreakDown = B.FTSizeBreakDown"
            _Qry &= vbCrLf & " WHERE A.FTPackNo='" & HI.UL.ULF.rpQuoted(PackNoKey) & "' "
            _Qry &= vbCrLf & " AND B.FTPackNo IS NULL"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If


            _Qry = " DELETE A "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A  "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B  "
            _Qry &= vbCrLf & "  ON A.FTPackNo = B.FTPackNo "
            _Qry &= vbCrLf & "  AND A.FNCartonNo = B.FNCartonNo "
            _Qry &= vbCrLf & " WHERE A.FTPackNo='" & HI.UL.ULF.rpQuoted(PackNoKey) & "' "
            _Qry &= vbCrLf & " AND B.FTPackNo IS NULL"

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

#End Region

    Private Sub FNOrderPackType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOrderPackType.SelectedIndexChanged
        Try

            FNPackSetValue_lbl.Visible = (FNOrderPackType.SelectedIndex = 1)
            FNPackSetValue.Visible = (FNOrderPackType.SelectedIndex = 1)

            If (FNOrderPackType.SelectedIndex = 0) Then
                FNPackSetValue.Value = 0
            End If

            otpbarcodeset.PageVisible = (FNOrderPackType.SelectedIndex = 1)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbsuborder_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbsuborder.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbsuborder_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbsuborder.SelectedTabPage Is Nothing) Then
                    Call LoadOrderPackSubBreakDown(otbsuborder.SelectedTabPage.Name.ToString)
                Else
                    Me.ogcsubpackdetail.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
        End Try

        Try

            'Me.FNHSysStyleId.Properties.ReadOnly = Not (otbsuborder.SelectedTabPage Is Nothing)
            'Me.FNHSysStyleId.Properties.Buttons(0).Enabled = (otbsuborder.SelectedTabPage Is Nothing)

            Me.FTCustomerPO.Properties.ReadOnly = Not (otbsuborder.SelectedTabPage Is Nothing)
            Me.FTCustomerPO.Properties.Buttons(0).Enabled = (otbsuborder.SelectedTabPage Is Nothing)

            Me.FNOrderPackType.Properties.ReadOnly = Not (otbsuborder.SelectedTabPage Is Nothing)
            Me.FNPackSetValue.Properties.ReadOnly = Not (otbsuborder.SelectedTabPage Is Nothing)

        Catch ex As Exception
        End Try

    End Sub

    Private Function CheckOverShip() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.FN_GET_OverShip('" & Me.FTPackNo.Text & "')"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmaddsuborder_Click(sender As Object, e As EventArgs) Handles ocmaddsuborder.Click

        If Me.CheckCreateCarton(Me.FTPackNo.Text) Then
            addSubJob()
        Else
            If CheckOverShip() Then
                If HI.MG.ShowMsg.mConfirmProcess("พบการสร้างกล่องแล้ว คุณต้องการเพิ่มจำนวนสร้างกล่อง ใช่หรือไม่...", 2009190908) = True Then
                    addSubJob()
                End If
            Else
                HI.MG.ShowMsg.mInfo("พบการสร้างกล่องแล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1411200113, Me.Text, , MessageBoxIcon.Warning)
            End If

        End If

    End Sub

    Private Sub addSubJob()
        Try
            If Me.FTCustomerPO.Text <> "" Then
                If FTPackNo.Properties.Tag.ToString = "" Then
                    If Me.VerrifyData() Then
                        If Me.SaveData Then
                        Else
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    If Me.FTPackNo.Text = "" Or Me.FTPackNo.Properties.Tag.ToString = "" Then
                        Exit Sub
                    Else
                        'If FNHSysStyleId.Properties.ReadOnly = False Then
                        '    If Me.SaveData Then
                        '    Else
                        '        Exit Sub
                        '    End If
                        'End If
                    End If
                End If

                With _GenPackOrder

                    ' .FNHSysStyleId = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
                    .packSetValue = Me.FNPackSetValue.Value
                    .PackSetType = Me.FNOrderPackType.SelectedIndex

                    .CustPONo = FTCustomerPO.Text
                    .PackOrderNo = Me.FTPackNo.Text
                    Call HI.ST.Lang.SP_SETxLanguage(_GenPackOrder)
                    .ShowDialog()

                    If (.Process) Then
                        Call LoadOrderPackDetail(Me.FTPackNo.Text)

                    End If

                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTCustomerPO_lbl.Text)
                FTCustomerPO.Focus()
                FTCustomerPO.SelectAll()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdeletesuborder_Click(sender As Object, e As EventArgs) Handles ocmdeletesuborder.Click
        If Not (Me.otbsuborder.SelectedTabPage Is Nothing) Then
            If Me.CheckCreateCarton(Me.FTPackNo.Text) Then
                If Me.DeleteSubOrder(False) Then
                    Call LoadOrderPackDetail(Me.FTPackNo.Text)

                Else

                End If
            Else
                HI.MG.ShowMsg.mInfo("พบการสร้างกล่องแล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1411200113, Me.Text, , MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub wCreatePackOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call InitGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged
        Call TabChenge()

    End Sub

    Private Sub otbdetail_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otbdetail.SelectedPageChanging
        Select Case e.Page.Name
            Case otppackdetailcarton.Name

                Call CreateTreeCarton()
                Me.XtraTabControl1.SelectedTabPageIndex = Me.FNOrderPackType.SelectedIndex
                Me.XtraTabControl2.SelectedTabPageIndex = Me.FNOrderPackType.SelectedIndex
                Me.XtraTabControl3.SelectedTabPageIndex = Me.FNOrderPackType.SelectedIndex
                If Me.FNOrderPackType.SelectedIndex = 0 Then
                    Me.ogbpackalltotaltabSet.Enabled = False
                    Me.ogbpacktotalset.Enabled = False
                    Me.XtraTabPage2.Enabled = False
                End If




                Call LoadTotalOrderPackBreakDownCreateCarton(Me.FTPackNo.Text)
                Call LoadrderPackBreakDownCartonBefore(Me.FTPackNo.Text, 0)
                Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)
                Call LoadrderPackBreakDownCartonWeight(Me.FTPackNo.Text, 0)
                Call LoadrderPackBreakDownCartonTotalWeight(Me.FTPackNo.Text, 0)
            Case otpbarcodeset.Name
                Call CreateTreeBarcodeSet()
        End Select
    End Sub



    Private Sub ocmgeneratecarton_Click(sender As Object, e As EventArgs) Handles ocmgeneratecarton.Click
        If FTPackNo.Text <> "" And FTPackNo.Properties.Tag.ToString() <> "" Then
            If Me.otbsuborder.SelectedTabPage IsNot Nothing Then

                With _GenPackCarton
                    .Process = False
                    '.FNHSysStyleId = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
                    .FTPackNo = Me.FTPackNo.Text
                    .SetPackType = Me.FNOrderPackType.SelectedIndex
                    .SetPackValue = Me.FNPackSetValue.Value

                    'Call HI.ST.Lang.SP_SETxLanguage(_GenPackCarton)
                    .ocmcreate.Enabled = True
                    .ocmcancel.Enabled = True
                    .FNPackPerCaton.Value = 0
                    .ObjectParent = Me
                    .ShowDialog()

                    '  If (.Process) Then
                    'Call CreateTreeCarton()
                    '  End If

                End With

            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายละเอียดการ Pack กรุณาทำการตรวยสอบ !!! ", 1411010019, Me.Text, , MessageBoxIcon.Warning)
            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTPackNo_lbl.Text)
            FTPackNo.Focus()
            FTPackNo.SelectAll()

        End If

    End Sub

    Private _StateSumGrid As Boolean
    Private Sub SumGrid()
        _StateSumGrid = True
        CType(ogcpackdetailWeight.DataSource, DataTable).AcceptChanges()
        Try
            Dim _Total As Double = 0
            _Total = 0
            With Me.ogvpackdetailWeight

                For I As Integer = 0 To .RowCount - 1
                    _Total = 0
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                        Select Case GridCol.FieldName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "Total".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper

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

        CType(ogcpackdetailWeight.DataSource, DataTable).AcceptChanges()

        _StateSumGrid = False
    End Sub

    Private Sub ReposCaleditWeight_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCaleditWeight.EditValueChanging
        Try
            Dim _NewValue As Double = e.NewValue
            Dim _OrgValue As Double = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Size = .FocusedColumn.FieldName.ToString()
                    _FTColorway = .GetFocusedRowCellValue("FTColorway")

                    If Not (_StateSumGrid) Then
                        Dim _ColName As String = .FocusedColumn.FieldName.ToString
                        With CType(ogcpackdetailWeight.DataSource, DataTable)
                            .AcceptChanges()

                            For Each R As DataRow In .Rows
                                R.Item(_ColName) = _NewValue
                            Next

                        End With

                        ' ogvpackdetailWeight.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, (_NewValue))

                        If Not (ogcpackdetailWeight.DataSource Is Nothing) Then
                            Call SumGrid()
                        End If

                    End If

                End With
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub ocmsaveweightpack_Click(sender As Object, e As EventArgs) Handles ocmsaveweightpack.Click
        Try
            If FTPackNo.Text <> "" And FTPackNo.Properties.Tag.ToString() <> "" Then
                If Not (Me.otbsuborder.SelectedTabPage Is Nothing) Then
                    CType(Me.ogcpackdetailWeight.DataSource, DataTable).AcceptChanges()
                    If CType(Me.ogcpackdetailWeight.DataSource, DataTable).Rows.Count > 0 Then
                        If SaveWeight() = True Then
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Else
                            HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการบันทึก ได้ กรุณาติดต่อ Admin !!!", 1411150022, Me.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายละเอียดการ Pack กรุณาทำการตรวจสอบ !!!", 1411150021, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายละเอียดการ Pack กรุณาทำการตรวจสอบ !!!", 1411150021, Me.Text)
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
                FTPackNo.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub otlpack_Click(sender As Object, e As EventArgs) Handles otlpack.Click
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        If Convert.ToBoolean(.Tag) = False Then

                            Dim _FNCartonNo As String = .GetValue(1).ToString
                            Dim _FNQuantity As String = .GetValue(2).ToString
                            Dim _FNNetWeight As String = .GetValue(3).ToString
                            Dim _FNHSysCartonId As String = .GetValue(4).ToString
                            Dim _FTCartonCode As String = .GetValue(5).ToString
                            Dim _FNWeight As String = .GetValue(6).ToString
                            Dim _FNPackCartonSubType As String = .GetValue(7).ToString
                            Dim _FNPackPerCarton As String = .GetValue(8).ToString

                            FNCartonNo.Value = Val(_FNCartonNo)
                            FNHSysCartonId.Text = _FTCartonCode
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNNW.Value = Val(_FNWeight)

                            Call LoadrderPackBreakDownCartonBefore(Me.FTPackNo.Text, Val(_FNCartonNo))
                            Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))
                            Call LoadrderPackBreakDownCartonWeight(Me.FTPackNo.Text, Val(_FNCartonNo))
                            Call LoadrderPackBreakDownCartonTotalWeight(Me.FTPackNo.Text, Val(_FNCartonNo))


                            Dim _Qry As String = ""

                            _Qry = "  SELECT SUM(A.FNQuantity * isnull( B.FNWeight,wt.FNWeight)) AS FNNeteight    , SUM(A.FNQuantity * isnull(wt.FNNetNetWeight,0)) as FNNetNetWeight "
                            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK) LEFT OUTER JOIN"
                            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS B WITH(NOLOCK) ON A.FTPackNo = B.FTPackNo "
                            _Qry &= vbCrLf & "  AND A.FTOrderNo = B.FTOrderNo "
                            _Qry &= vbCrLf & "  AND A.FTSubOrderNo = B.FTSubOrderNo"
                            _Qry &= vbCrLf & "   AND A.FTColorway = B.FTColorway "
                            _Qry &= vbCrLf & "   AND   A.FTSizeBreakDown = B.FTSizeBreakDown"
                            _Qry &= vbCrLf & "outer apply (select top 1 wt.FNWeight , wt.FNNetNetWeight   "
                            _Qry &= vbCrLf & "  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMNetWeight wt "
                            _Qry &= vbCrLf & " inner join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination ex on wt.FNHSysStyleId = ex.FNHSysStyleId"
                            _Qry &= vbCrLf & " and wt.FTColorWay = ex.FTColorway and wt.FTSizeBreakDown = ex.FTSizeBreakDown"
                            _Qry &= vbCrLf & " where  ex.FTOrderNo = B.FTOrderNo "
                            _Qry &= vbCrLf & "   AND ex.FTSubOrderNo = B.FTSubOrderNo"
                            _Qry &= vbCrLf & "   AND wt.FTColorway = B.FTColorway "
                            _Qry &= vbCrLf & "   AND wt.FTSizeBreakDown = B.FTSizeBreakDown"
                            _Qry &= vbCrLf & "   ) wt"
                            _Qry &= vbCrLf & " "
                            _Qry &= vbCrLf & " "

                            _Qry &= vbCrLf & " WHERE  (A.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
                            _Qry &= vbCrLf & "  AND (A.FNCartonNo = " & Val(_FNCartonNo) & ")"
                            Dim _odt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            FNNW.Value = _odt.Rows(0).Item("FNNeteight") ' Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))
                            FNNNW.Value = _odt.Rows(0).Item("FNNetNetWeight") 'FNNW.Value


                        End If
                    End With
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdeletecarton_Click(sender As Object, e As EventArgs) Handles ocmdeletecarton.Click
        If Me.FTPackNo.Text <> "" And Me.FTPackNo.Properties.Tag.ToString <> "" Then
            If Me.FNCartonNo.Value > 0 Then
                If CheckScan(Me.FTPackNo.Text, Me.FNCartonNo.Value) = True Then
                    If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, " Carton No :" & Me.FNCartonNo.Value.ToString) = True Then
                        If Me.DeleteCarton(Me.FTPackNo.Text, Me.FNCartonNo.Value) = True Then

                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                            Call CreateTreeCarton()
                            Call LoadTotalOrderPackBreakDownCreateCarton(Me.FTPackNo.Text)
                            Call LoadrderPackBreakDownCartonBefore(Me.FTPackNo.Text, 0)
                            Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)
                            Call LoadrderPackBreakDownCartonWeight(Me.FTPackNo.Text, 0)
                            Call LoadrderPackBreakDownCartonTotalWeight(Me.FTPackNo.Text, 0)

                        Else
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        End If
                    End If
                Else
                    HI.MG.ShowMsg.mInfo("ของนี้มีการ Scan แล้วไม่สามารถทำการลบได้ !!!", 1411200105, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNCartonNo_lbl.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
            Me.FTPackNo.Focus()
        End If

    End Sub

    Private Sub ocmdeleteallcarton_Click(sender As Object, e As EventArgs) Handles ocmdeleteallcarton.Click
        If Me.FTPackNo.Text <> "" And Me.FTPackNo.Properties.Tag.ToString <> "" Then
            If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการทำการลบทุก Carton ที่ยังไม่ได้ทำการ Scan ใช่หรือไม่ ?", 1411210018) = True Then
                If Me.DeleteAllCarton(Me.FTPackNo.Text) = True Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Call CreateTreeCarton()
                    Call LoadTotalOrderPackBreakDownCreateCarton(Me.FTPackNo.Text)
                    Call LoadrderPackBreakDownCartonBefore(Me.FTPackNo.Text, 0)
                    Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)
                    Call LoadrderPackBreakDownCartonWeight(Me.FTPackNo.Text, 0)
                    Call LoadrderPackBreakDownCartonTotalWeight(Me.FTPackNo.Text, 0)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
            Me.FTPackNo.Focus()
        End If
    End Sub

    Private Sub Cal_EditValueChanged(sender As Object, e As EventArgs) Handles FNCTNW.EditValueChanged, FNNW.EditValueChanged

        Static _Proc As Boolean
        If Not (_Proc) Then
            _Proc = True
            Try
                FNGW.Value = (FNCTNW.Value + FNNW.Value)
            Catch ex As Exception
            End Try
            _Proc = False
        End If
    End Sub


    Private Sub ocmpreviewpackassort_Click(sender As Object, e As EventArgs) Handles ocmpreviewpackassort.Click
        If Me.FTPackNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "PackOrderSlip_Assort.rpt"
                .Formular = "{TPACKOrderPack.FTPackNo}='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
            FTPackNo.Focus()
        End If
    End Sub


    Public Sub CreateTreeBarcodeSet()
        With Me.otlbarcodeset
            .ClearNodes()
            .Columns.Clear()

            .Columns.Add() : .Columns.Add()
            .Columns.Add()

            With .Columns.Item(0)
                .Name = "SetColKey"
                .Caption = "FTCartonName"
                .FieldName = "FTBarcodeSetNo"
                .Visible = True
            End With

            With .Columns.Item(1)
                .Name = "SetFNCartonNo"
                .Caption = "FNCartonNo"
                .FieldName = "FTBarcodeSetNo"
                .Visible = False
            End With

            With .Columns.Item(2)
                .Name = "SetFNQuantity"
                .Caption = "FNQuantity"
                .FieldName = "FTBarcodeSetNo"
                .Visible = False
            End With

            With .OptionsView
                .ShowColumns = False
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None

        End With

        Call InitNodeBarCodeSet(Me.otlbarcodeset, Nothing)
        Me.otlbarcodeset.ExpandAll()

    End Sub

    Private Sub InitNodeBarCodeSet(ByVal _Lst As DevExpress.XtraTreeList.TreeList, ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode)

        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try
            If (_Node Is Nothing) Then
                node = _Lst.AppendNode(New Object() {"Barcode Set No", "-1", "", "", "", "", "", "", ""}, _Node)
            End If

            If (_Node Is Nothing) Then

                Try
                    node.HasChildren = True
                    node.Tag = True

                    Dim dt As DataTable

                    Dim _Qry As String = ""


                    _Qry = "   Select  FTBarcodeSetNo "
                    _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet AS X WITH(NOLOCK) "
                    _Qry &= vbCrLf & " Where (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
                    _Qry &= vbCrLf & " Group By FTBarcodeSetNo"
                    _Qry &= vbCrLf & " ORDER By FTBarcodeSetNo"



                    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    For Each R As DataRow In dt.Rows

                        nodeChild = _Lst.AppendNode(New Object() {R!FTBarcodeSetNo.ToString, R!FTBarcodeSetNo.ToString, R!FTBarcodeSetNo.ToString}, node)
                        nodeChild.HasChildren = False

                    Next

                Catch ex As Exception
                End Try

            Else
                node.HasChildren = False
            End If

        Catch
        End Try
        '_Lst.EndUnboundLoad()
    End Sub

    Private Function CheckScanBarcodeSetNo(packno As String, barcodesetno As String) As Boolean
        Dim cmdstring As String = ""
        Dim state As Boolean = False

        cmdstring = "   Select TOP 1 FTPackNo "
        cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS X WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  Where (FTPackNo = N'" & HI.UL.ULF.rpQuoted(packno) & "') "
        cmdstring &= vbCrLf & " And (FTBarcodeSetNo = N'" & HI.UL.ULF.rpQuoted(barcodesetno) & "') "
        state = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PROD, "") <> "")

        Return state

    End Function

    Private Function SumDataGridBarcodeSetNo() As Integer
        Dim dt As DataTable
        Dim totalqty As Integer = 0

        With CType(ogcbreakdownbarcodeset.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With

        For Each Row As DataRow In dt.Rows
            For Each Col As DataColumn In dt.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                    Case Else
                        totalqty = totalqty + Val(Row.Item(Col.ColumnName))
                End Select
            Next
        Next

        Return totalqty

    End Function

    Private Sub ClearDataGridBarcodeSetNo()
        Dim dt As DataTable
        With CType(ogcbreakdownbarcodeset.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With

        For Each Row As DataRow In dt.Rows
            For Each Col As DataColumn In dt.Columns
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                    Case Else
                        Row.Item(Col.ColumnName) = 0
                End Select
            Next
        Next
        ogcbreakdownbarcodeset.DataSource = dt.Copy
        ogcbreakdownbarcodeset.Refresh()
    End Sub

    Private Sub otlbarcodeset_Click(sender As Object, e As EventArgs) Handles otlbarcodeset.Click
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        If Convert.ToBoolean(.Tag) = False Then

                            Dim _barcodesetno As String = .GetValue(1).ToString
                            ClearDataGridBarcodeSetNo()


                            Dim cmdstring As String = ""
                            Dim dtbarset As DataTable

                            cmdstring = "  Select  FTBarcodeSetNo, FTPackNo, FTColorway, FTSizeBreakDown, FNQuantity "
                            cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet "
                            cmdstring &= vbCrLf & "   Where (FTPackNo = N'" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "') "
                            cmdstring &= vbCrLf & "   AND (FTBarcodeSetNo = N'" & HI.UL.ULF.rpQuoted(_barcodesetno) & "') "
                            dtbarset = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                            If dtbarset.Rows.Count > 0 Then
                                Dim dt As DataTable
                                With CType(ogcbreakdownbarcodeset.DataSource, DataTable)
                                    .AcceptChanges()
                                    dt = .Copy
                                End With

                                For Each Row As DataRow In dt.Rows

                                    For Each Col As DataColumn In dt.Columns
                                        Select Case Col.ColumnName.ToString.ToUpper
                                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                                            Case Else

                                                For Each Rx2 As DataRow In dtbarset.Select("FTColorway='" & HI.UL.ULF.rpQuoted(Row.Item("FTColorway")) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "'")
                                                    Row.Item(Col.ColumnName) = Val(Rx2!FNQuantity)
                                                Next

                                        End Select
                                    Next

                                Next

                                ogcbreakdownbarcodeset.DataSource = dt.Copy
                                ogcbreakdownbarcodeset.Refresh()

                            End If

                            FTBarcodeSetNo.Text = _barcodesetno
                            FTBarcodeSetNo.Focus()
                            FTBarcodeSetNo.SelectAll()

                        Else
                            ClearDataGridBarcodeSetNo()
                        End If
                    End With
                Else
                    ClearDataGridBarcodeSetNo()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CalSet_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepCalSet.EditValueChanging
        Try
            Dim _NewValue As Double = e.NewValue
            Dim _OrgValue As Double = 0
            Dim _Size As String = ""
            Dim _FTColorway As String = ""

            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)

                End With

                CType(ogcbreakdownbarcodeset.DataSource, DataTable).AcceptChanges()
            End If
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub FTBarcodeSetNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeSetNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If FTBarcodeSetNo.Text.Trim <> "" Then

                    Dim SmalSetQty As Integer = SumDataGridBarcodeSetNo()
                    Dim CartonPackQty As Integer = 0

                    If SmalSetQty = FNPackSetValue.Value And SmalSetQty > 0 Then

                        Dim cmdstring As String = ""

                        cmdstring = "  Select Top 1   FNPackPerCarton "
                        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS X WITH(NOLOCK) "
                        cmdstring &= vbCrLf & "  Where (FTPackNo =N'" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "') "
                        cmdstring &= vbCrLf & "  Group By FNPackPerCarton "
                        cmdstring &= vbCrLf & "  Order By COUNT(1)  DESC "

                        CartonPackQty = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PROD, "0"))

                        If CartonPackQty <= 0 Then

                            HI.MG.ShowMsg.mInfo("ไม่พบจำนวน Pack ต่อกล่อง !!!", 1707484543, Me.Text,, MessageBoxIcon.Warning)
                            Exit Sub

                        End If


                        If (CartonPackQty \ SmalSetQty) <= 1 Then

                            HI.MG.ShowMsg.mInfo("จำนวน Set เล็ก ไม่ถูกต้อง จำนวน Pack Set เล็กในกล่องใหญ่ ต้องมากกว่า 1 !!!", 1707484548, Me.Text,, MessageBoxIcon.Warning)
                            Exit Sub

                        End If


                        If (CheckScanBarcodeSetNo(FTPackNo.Text, FTBarcodeSetNo.Text.Trim) = True) Then

                            HI.MG.ShowMsg.mInfo("ข้อมูล Barcode ถูกนำไปใช้ง่รแล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1707080543, Me.Text,, MessageBoxIcon.Warning)
                            Exit Sub

                        End If


                        Dim StateSave As Boolean = False
                        Dim dt As DataTable
                        With CType(ogcbreakdownbarcodeset.DataSource, DataTable)
                            .AcceptChanges()
                            dt = .Copy
                        End With

                        Dim _barcodesetno As String = FTBarcodeSetNo.Text.Trim


                        cmdstring = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet"
                        cmdstring &= vbCrLf & "   Where (FTPackNo = N'" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "') "
                        cmdstring &= vbCrLf & "   AND (FTBarcodeSetNo = N'" & HI.UL.ULF.rpQuoted(_barcodesetno) & "') "
                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                        If FTStateDeleteBarcode.Checked = False Then
                            For Each Row As DataRow In dt.Rows

                                For Each Col As DataColumn In dt.Columns
                                    Select Case Col.ColumnName.ToString.ToUpper
                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper, "FNHSysStyleId".ToUpper, "FNHSysStyleId_Hide".ToUpper
                                        Case Else

                                            If Val(Row.Item(Col.ColumnName)) > 0 Then

                                                cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet"
                                                cmdstring &= vbCrLf & " ( "
                                                cmdstring &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime,FTPackNo,FTBarcodeSetNo, FTColorway, FTSizeBreakDown, FNQuantity "
                                                cmdstring &= vbCrLf & ")"
                                                cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "'"
                                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_barcodesetno) & "'"
                                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Row.Item("FTColorway").ToString) & "'"
                                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "'"
                                                cmdstring &= vbCrLf & "," & Val(Row.Item(Col.ColumnName)) & ""

                                                If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PROD) Then
                                                    StateSave = True
                                                End If

                                            End If

                                    End Select

                                Next

                            Next

                        Else
                            StateSave = True
                        End If

                        If StateSave Then

                            CreateTreeBarcodeSet()
                            ClearDataGridBarcodeSetNo()

                            FTBarcodeSetNo.Text = ""
                            FTBarcodeSetNo.Focus()
                            FTBarcodeSetNo.SelectAll()

                        End If

                    Else

                        HI.MG.ShowMsg.mInfo("จำนวน Breakdown ไม่ครบตามจำนวน Set กรุณาทำการตรวจสอบ !!!", 1707080542, Me.Text,, MessageBoxIcon.Warning)

                    End If
                End If

        End Select

    End Sub
    Private _PDtCartonCount As DataTable

    Private Sub ocmgenbarcodesscc_Click(sender As Object, e As EventArgs) Handles ocmgenbarcodesscc.Click
        Try
            Dim _Count As Integer = _PDtCartonCount.Rows.Count
            Dim _Cmd As String = "" : Dim _Seq As Integer = 0
            Dim _oDt As DataTable


            _Cmd = "SELECT  D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown, D.FNQuantity, D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine,   P.FTCustomerPO, P.FNHSysStyleId "
            _Cmd &= vbCrLf & ", convert(nvarchar(30) , convert(int ,PL.FTSerialFrom ) + ROW_NUMBER() "
            _Cmd &= vbCrLf & "  Over (partition by   D.FTPOLine ,P.FTPackNo,D.FTColorway, D.FTSizeBreakDown ,P.FTCustomerPO ,D.FNQuantity "
            _Cmd &= vbCrLf & " ORder by  P.FTPackNo,D.FNCartonNo) -1 )AS FNCartonSeq  ,isnull(PL.FTSerialFrom,'') as FTSerialFrom "

            _Cmd &= vbCrLf & " FROM "
            _Cmd &= vbCrLf & " (  select  D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown, sum(D.FNQuantity) as FNQuantity "
            _Cmd &= vbCrLf & ", D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine "
            _Cmd &= vbCrLf & "from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Carton_Detail D  with(nolock)"
            _Cmd &= vbCrLf & " where  (D.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Cmd &= vbCrLf & " group by    D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown , D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine) AS D INNER JOIN  "

            _Cmd &= vbCrLf & "     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack AS P with(nolock) ON D.FTPackNo = P.FTPackNo "
            _Cmd &= vbCrLf & "   INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo. TEXPTPackPlan_D as PL ON P.FTCustomerPO = PL.FTPORef "
            _Cmd &= vbCrLf & "   and D.FTPOLine  = convert(nvarchar(30), convert(int, PL.FTPOLineNo)) "
            _Cmd &= vbCrLf & "     and D.FTSizeBreakDown = PL.FTSizeBreakDown  "
            _Cmd &= vbCrLf & " and D.FTColorway= replace(replace(PL.FTShortDescription,PL.FTStyleCode,''),'-','')   "
            _Cmd &= vbCrLf & "  and D.FNQuantity = PL.FNQtyPerPack "
            _Cmd &= vbCrLf & "   INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo. TEXPTPackPlan  as PD ON PL.FTPckPlanNo = PD.FTPckPlanNo AND PL.FTPORef = PD.FTPORef AND PL.FTPORefNo = PD.FTPORefNo "
            _Cmd &= vbCrLf & "WHERE  (D.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Cmd &= vbCrLf & "  and PD.FTApproveState = '1' "
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""
            _Cmd &= vbCrLf & ""
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            '_Cmd = "Select Max(FNCartonNo) AS FNCartonNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "Where FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            '_Cmd &= vbCrLf & "and isnull(FTBarCodeEAN13,'') <> ''"
            '_Seq = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            '_Count = _Count - _Seq
            If _Count = 0 Then Exit Sub

            Dim _Popup As wPopUpBarcode
            _Popup = New wPopUpBarcode
            HI.TL.HandlerControl.ClearControl(_Popup)
            If _oDt.Rows.Count > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("มีข้อมูล Import Packing plan แล้ว ต้องการให้ระบบ สร้างบาร์โค๊ดให้ อัตโนมัติ หรือไม่ !!", 1908191610, Me.Text) = False Then
                    With _Popup
                        .QtyCarton = _Count
                        .FNCartonNoBegin.Value = 1
                        .FNQtyCarton.Value = _Count

                        .ShowDialog()
                        If (.Poss) Then

                            Dim _FNCTNQty As Integer = (.FNQtyCarton.Value - .FNCartonNoBegin.Value)
                            Dim _FTCTNS As String = .FTCTNS.Text
                            Dim _FTCTNE As String = CStr(CInt(_FTCTNS) + _FNCTNQty) '(.FNQtyCarton.Value - 1))
                            If GenerateBarcodeSSCC(_FTCTNS, _FTCTNE, .FNCartonNoBegin.Value) Then
                                HI.MG.ShowMsg.mInfo("Generate Barcode Saccess....", 1512141511, Me.Text, "", MessageBoxIcon.Information)
                                Call LoadDataInfo(Me.FTPackNo.Text)
                            End If
                        End If
                    End With
                Else
                    If Me.GenBarcodeEN13(_oDt) Then
                        HI.MG.ShowMsg.mInfo("Generate Barcode Saccess....", 1512141511, Me.Text, "", MessageBoxIcon.Information)
                        Call LoadDataInfo(Me.FTPackNo.Text)
                    End If

                End If

            Else
                With _Popup
                    .QtyCarton = _Count
                    .FNCartonNoBegin.Value = 1
                    .FNQtyCarton.Value = _Count

                    .ShowDialog()
                    If (.Poss) Then

                        Dim _FNCTNQty As Integer = (.FNQtyCarton.Value - .FNCartonNoBegin.Value)
                        Dim _FTCTNS As String = .FTCTNS.Text
                        Dim _FTCTNE As String = CStr(CInt(_FTCTNS) + _FNCTNQty) '(.FNQtyCarton.Value - 1))
                        If GenerateBarcodeSSCC(_FTCTNS, _FTCTNE, .FNCartonNoBegin.Value) Then
                            HI.MG.ShowMsg.mInfo("Generate Barcode Saccess....", 1512141511, Me.Text, "", MessageBoxIcon.Information)
                            Call LoadDataInfo(Me.FTPackNo.Text)
                        End If
                    End If
                End With
            End If






        Catch ex As Exception
        End Try
    End Sub



    Private Function GenerateBarcodeSSCC(_SBarcodeNo As Integer, _EBarcodeNo As Integer, _BeginCarton As Integer) As Boolean
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

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

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

                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode "
                _Cmd &= vbCrLf & "set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ", FTCartonNo='" & HI.UL.ULF.rpQuoted(I) & "'"
                _Cmd &= vbCrLf & ",FTBarCodeEAN13='" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
                _Cmd &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
                _Cmd &= vbCrLf & "Where FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & "and FNCartonNo=" & Integer.Parse(_Seq)

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                    _Cmd &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,  FTPackNo, FNCartonNo, FTBarCodeCarton, FTCartonNo, FTBarCodeEAN13)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(_Seq)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(I) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

                ' End If
                _Seq += +1
            Next


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


    Private Function GenBarcodeEN13(_dt As DataTable) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _EN13 As String = "" : Dim _CartonNO As String = ""
            Dim _oDt As System.Data.DataTable
            _oDt = _dt
            '_Cmd = "SELECT   C.FTColorway, C.FTSizeBreakDown, C.FTOrderNo, C.FTPackNo,  C.FTPOLine  , C.FTSubOrderNo, PK.FTCustomerPO, C.FNCartonNo, D.FTSerialFrom , D.FTSerialTo  , D.FNFrom , D.FNTo ,C.FNQuantity"
            '_Cmd &= vbCrLf & " , convert(nvarchar(30) , convert(int ,D.FTSerialFrom ) + ROW_NUMBER() Over (partition by C.FTOrderNo , C.FTSubOrderNo, C.FTPOLine ,C.FTPackNo,C.FTColorway, C.FTSizeBreakDown ,PK.FTCustomerPO ,C.FNQuantity ORder by  C.FTPackNo,C.FNCartonNo) -1 )AS FNCartonSeq  "
            '_Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS C  LEFT OUTER JOIN "
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS  PK WITH(NOLOCK)    ON C.FTPackNo = PK.FTPackNo INNER JOIN    "
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. TEXPTPackPlan_D as D  ON PK.FTCustomerPO = D.FTPORef and C.FTPOLine  = convert(nvarchar(30), convert(int, D.FTPOLineNo)) "
            '_Cmd &= vbCrLf & " and C.FTSizeBreakDown = D.FTSizeBreakDown and    C.FTColorway= replace(replace(D.FTShortDescription,D.FTStyleCode,''),'-','')  and C.FNQuantity = D.FNQtyPerPack "

            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS  T WITH(NOLOCK)    ON  C.FTPackNo = T.FTPackNo AND C.FNCartonNo = T.FNCartonNo     "

            '_Cmd &= vbCrLf & " where  (T.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)


            For Each R As DataRow In _oDt.Rows
1:
                _EN13 = HI.UL.ULF.rpQuoted(GenerateBarcodeSSCCEN13(R!FNCartonSeq.ToString, R!FNCartonSeq.ToString, R!FNCartonNo.ToString))

                _Cmd = "Select Top 1 FNCartonNo "
                _Cmd &= vbCrLf & " From TPACKOrderPack_Carton_Barcode "
                _Cmd &= vbCrLf & " where FDInsDate > convert(varchar(10) , DATEadd(year , -1 , getdate()) , 111) "
                _Cmd &= vbCrLf & " and  isnull(FTBarCodeEAN13,'')  ='" & _EN13 & "'"
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    GoTo 1
                End If
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


    Private Function GenerateBarcodeSSCCEN13(_SBarcodeNo As Integer, _EBarcodeNo As Integer, _BeginCarton As Integer) As String
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

    Private Sub ocmreviedsuborder_Click(sender As Object, e As EventArgs) Handles ocmreviedsuborder.Click
        Try
            If Not HI.MG.ShowMsg.mConfirmProcess("คุณต้องการแก้ไขยอด แพ็คใบสั่งผลิตย่อย ใช่หรือไม่ ", 1912111523, Me.Text) Then
                Exit Sub
            End If
            If Not Me.VerrifyData() Then
                Exit Sub
            End If
            Dim _Qry As String = "" : Dim _dtSub As DataTable
            '_Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_SubOrderBreakDown_Pack_ByCustPO_Partial '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "','" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
            '  _dtSub = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


            _Qry = "Select distinct FTOrderNo , FTSubOrderNo,FTPOLine "
            _Qry &= vbCrLf & "  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKOrderPack_Detail with(nolock)"
            _Qry &= vbCrLf & " where  FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            Dim _dtPack As DataTable
            _dtPack = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each r As DataRow In _dtPack.Rows

                _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_SubOrderBreakDown_Pack_ByCustPO_Partial '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "','" & HI.UL.ULF.rpQuoted(r!FTOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(r!FTSubOrderNo.ToString) & "' "
                _dtSub = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                Dim _TotalQty As Integer = 0
                _Qry = "Select  sum(FNQuantity) as FNQuantity"
                _Qry &= vbCrLf & "   from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack_Carton_Detail  "
                _Qry &= vbCrLf & "   where FTPackNo =  '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                _Qry &= vbCrLf & "   and FTPOLine='" & r!FTPOLine.ToString & "'"
                _TotalQty = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, 0)

                For Each x As DataRow In _dtSub.Select("FTPOLine='" & r!FTPOLine.ToString & "'")
                    If _TotalQty > Val(x!Total.ToString) Then

                        HI.MG.ShowMsg.mInfo("มีจำนวนสร้างกล่องเกิน กรุณาตรวจสอบ รายการสร้างกล่อง !", 1912111512, Me.Text, " Line Item : " & r!FTPOLine.ToString)
                        Exit Sub
                    End If

                    _Qry = "update a"
                    _Qry &= vbCrLf & " set a.FNQuantity = isnull( b.FNGrandQuantity ,0) "
                    _Qry &= vbCrLf & "  from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..TPACKOrderPack_Detail a  "
                    _Qry &= vbCrLf & "   outer apply (   Select Top 1  FNGrandQuantity "
                    _Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.V_OrderSub_BreakDown_ShipDestination  b where  a.FTOrderNo = b.FTOrderNo"
                    _Qry &= vbCrLf & " and a.FTSubOrderNo = b.FTSubOrderNo  "
                    _Qry &= vbCrLf & " and   a.FTPOLine = b.ftnikepolineItem"
                    _Qry &= vbCrLf & "  and a.FTColorway = b.ftcolorway"
                    _Qry &= vbCrLf & " and a.ftsizebreakdown = b.ftsizebreakdown"
                    _Qry &= vbCrLf & "  and b.FNGrandQuantity > 0  "
                    _Qry &= vbCrLf & ") as b "

                    _Qry &= vbCrLf & "   where  FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "


                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
                Next



            Next
            Call LoadOrderPackDetail(HI.UL.ULF.rpQuoted(Me.FTPackNo.Text))
            HI.MG.ShowMsg.mInfo("แก้ไขยอด เรียบร้อย.", 1912111516, Me.Text)
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End Try
    End Sub


End Class