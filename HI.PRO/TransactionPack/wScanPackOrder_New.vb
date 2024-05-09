Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraTreeList

Imports System.Runtime.InteropServices
Imports System.IO

Public Class wScanPackOrder_New

    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _StateSubNew As Boolean = False
    Private _ListPackOrderNo As wListPackOrderNo
    Private _ListPackOrderNoBayCarton As wListPackOrderNo
    Private _PFTOrderNo As String = ""
    Private _PFTSubOrderNo As String = ""
    Private _PFTColorway As String = ""
    Private _PFTSizeBreakDown As String = ""
    Private _FTPackOfOrderNo As String = ""
    Private _PFTAssortState As Boolean = False
    Private _PAFTColorway As String = ""
    Private _PDataNode As DataTable
    Private _PStateBarCust As Boolean = False
    Private _PSCartonNo As Integer = 0
    Private Scan_Process_Store As Boolean = False
    Dim _NewColorway As String = ""

    Private _Table() As String = {"TPACKOrderPack_Carton_Barcode", "TPRODTBundle"}
    Private Enum Table
        TPACKOrderPack_Carton_Barcode
        TPRODTBundle
    End Enum

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ListPackOrderNo = New wListPackOrderNo
        HI.TL.HandlerControl.AddHandlerObj(_ListPackOrderNo)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListPackOrderNo.Name.ToString.Trim, _ListPackOrderNo)
        Catch ex As Exception
        Finally
        End Try

        _ListPackOrderNoBayCarton = New wListPackOrderNo
        HI.TL.HandlerControl.AddHandlerObj(_ListPackOrderNoBayCarton)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListPackOrderNoBayCarton.Name.ToString.Trim, _ListPackOrderNoBayCarton)
        Catch ex As Exception
        Finally
        End Try

        Me.PrepareForm()
        Call InitGrid()
        Call TabChenge()
        Call Default_Grid_Scan()
        Me.ogvScan.OptionsView.ShowAutoFilterRow = False

        Call SetImagePack()

        Dim cmdstring As String = "SELECT TOP 1 FTCfgData FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE  (FTCfgName = N'Scan_Process_Store')"

        Scan_Process_Store = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

    End Sub

    Private Sub SetImagePack()

        If IO.File.Exists(_SystemFilePath & "\Pack\Packing.jpg") And IO.File.Exists(_SystemFilePath & "\Pack\FullCarton.jpg") _
            And IO.File.Exists(_SystemFilePath & "\Pack\CartonAssort.jpg") And IO.File.Exists(_SystemFilePath & "\Pack\Scrap.jpg") Then

            imagepackList.Images.Clear()
            Dim tPathImgDis As String = _SystemFilePath & "\Pack\Packing.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))

            End If

            tPathImgDis = _SystemFilePath & "\Pack\FullCarton.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))

            End If

            tPathImgDis = _SystemFilePath & "\Pack\CartonAssort.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))

            End If

            tPathImgDis = _SystemFilePath & "\Pack\Scrap.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Pack\Scrap2.png"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Pack\Scrap4.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Pack\carton-1.png"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If


            tPathImgDis = _SystemFilePath & "\Pack\ScrapFull.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

            tPathImgDis = _SystemFilePath & "\Pack\ScrappFull.png"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If

        End If

    End Sub

    Private Sub Default_Grid_Scan()
        Try
            With ogvScan
                .OptionsView.ShowAutoFilterRow = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "FTNikePOLineItem".ToUpper
                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next
            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub TabChenge()

        'ocmaddsuborder.Visible = (otbdetail.SelectedTabPage.Name = otppackdetail.Name)
        'ocmdeletesuborder.Visible = (otbdetail.SelectedTabPage.Name = otppackdetail.Name)
        'ocmsaveweightpack.Visible = (otbdetail.SelectedTabPage.Name = otppackdetail.Name)

        'ocmgeneratecarton.Visible = (otbdetail.SelectedTabPage.Name = otppackdetailcarton.Name)
        'ocmdeletecarton.Visible = (otbdetail.SelectedTabPage.Name = otppackdetailcarton.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub InitGrid()
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

        With ogvScanset
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
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

    Private Sub LoadrderPackBreakDownCarton(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _NewColorway = ""
        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
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

        For Each R As DataRow In _dt.Select("Total > 0")
            _NewColorway = R!FTColorway.ToString
            _PFTOrderNo = R!FTOrderNo.ToString
            _PFTSubOrderNo = R!FTSubOrderNo.ToString
        Next
        _PAFTColorway = ""
        _PFTAssortState = _dt.Select("Total > 0").Length > 1
        If _PFTAssortState Then
            For Each R As DataRow In _dt.Select("Total > 0")
                If _PAFTColorway <> "" Then _PAFTColorway &= "|"
                _PAFTColorway &= R!FTColorway.ToString
            Next
        End If

        With Me.ogvppercarton

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

        _dt.Dispose()
        _dtpack.Dispose()

        LoadrderPackBreakDownCartonSet(Key, CartonNo)
    End Sub

    Private Sub LoadrderPackBreakDownCartonSet(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _NewColorway = ""
        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_CartonSet '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _dtpack = _dt.Copy

        For Each R As DataRow In _dt.Select("Total > 0")
            _NewColorway = R!FTColorway.ToString
            _PFTOrderNo = R!FTOrderNo.ToString
            _PFTSubOrderNo = R!FTSubOrderNo.ToString
        Next
        _PAFTColorway = ""
        _PFTAssortState = _dt.Select("Total > 0").Length > 1
        If _PFTAssortState Then
            For Each R As DataRow In _dt.Select("Total > 0")
                If _PAFTColorway <> "" Then _PAFTColorway &= "|"
                _PAFTColorway &= R!FTColorway.ToString
            Next
        End If

        With Me.ogvppercartonset

            For I As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTSubOrderNo".ToUpper
                        .Columns(I).Visible = False
                    Case "FTOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
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
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
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
        Dim _Cmd As String = ""
        _Cmd = "Select * From(SELECT     P.FTPackNo,  P.FNOrderPackType, P.FNPackSetValue, P.FTRemark, S.FTStyleCode as FNHSysStyleId , S.FTStyleNameTH, S.FTStyleNameEN"
        _Cmd &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS P LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S ON P.FNHSysStyleId = S.FNHSysStyleId ) AS M"
        Dim _Dt As DataTable
        Dim _Str As String = _Cmd & "  WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "

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

        Call InitNodeCarton(Me.otlpack, Nothing)
        Call CreateTreeCarton()
        Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)

        _LoadDataScan()

        Me.XtraTabControl1.SelectedTabPageIndex = Me.FNOrderPackType.SelectedIndex
        Me.XtraTabControl2.SelectedTabPageIndex = Me.FNOrderPackType.SelectedIndex
        If Me.FNOrderPackType.SelectedIndex = 0 Then
            Me.XtraTabPage2.Enabled = False
            Me.XtraTabPage4.Enabled = False
        End If




        _ProcLoad = False
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

        ockmanual.Visible = True
        ockmanual.Checked = False
        ' ogbbarcodecarton.Visible = False
        FTCartonBarcodeNo.Text = ""


    End Sub

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
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
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTPackNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "PackOrderSlip_Pack.rpt"
                .Formular = "{TPACKOrderPack.FTPackNo}='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPackNo_lbl.Text)
            FTPackNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
#End Region

#Region " Proc "

    Private Sub CreateTreeCarton()
        With Me.otlpack
            .ClearNodes()
            .Columns.Clear()

            .Columns.Add() : .Columns.Add()
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

            With .Columns.Item(9)
                .Name = "FNScanQuantity"
                .Caption = "FNScanQuantity"
                .FieldName = "FNScanQuantity"
                .Visible = False
            End With

            With .Columns.Item(10)
                .Name = "FTBarCodeCarton"
                .Caption = "FTBarCodeCarton"
                .FieldName = "FTBarCodeCarton"
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

        Call InitNodeCarton(Me.otlpack, Nothing)
        Me.otlpack.ExpandAll()

    End Sub

    Private Sub InitNodeCarton(ByVal _Lst As DevExpress.XtraTreeList.TreeList, ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode)
        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try
            If (_Node Is Nothing) Then
                node = _Lst.AppendNode(New Object() {Me.FNCartonNo3_lbl.Text & "", "-1", "", "", "", "", "", "", "", "", ""}, _Node)
            End If

            If (_Node Is Nothing) Then
                node.ImageIndex = 0
                Try
                    node.HasChildren = True
                    node.Tag = True
                    Dim dt As DataTable
                    Dim _oDt As DataTable
                    Dim _QtyCarton As Integer = 0
                    Dim _PackPerQty As Integer = 0
                    Dim _Qry As String = ""
                    _Qry = " SELECT A.FTPackNo, A.FNCartonNo"
                    _Qry &= vbCrLf & "  , sum(A.FNQuantity) AS FNQuantity"
                    _Qry &= vbCrLf & "   ,Max(Convert(numeric(18,3),A.FNQuantity*B.FNWeight)) AS FNNetWeight "
                    _Qry &= vbCrLf & "   ,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"

                    _Qry &= vbCrLf & " , sum(isnull(Z.FNScanQuantity,0)) AS FNScanQuantity"
                    _Qry &= vbCrLf & "   ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.fn_Get_Carton_Info(A.FTPackNo,A.FNCartonNo) AS FTCartonInfo"

                    _Qry &= vbCrLf & " , ISNULL(("
                    _Qry &= vbCrLf & " SELECT TOP 1 FTBarCodeCarton"
                    _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS AX WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE AX.FTPackNo= A.FTPackNo"
                    _Qry &= vbCrLf & "  AND AX.FNCartonNo= A.FNCartonNo"
                    _Qry &= vbCrLf & " ),'') AS FTBarCodeCarton "

                    _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK) INNER JOIN "
                    _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS B WITH(NOLOCK) "
                    _Qry &= vbCrLf & "    ON A.FTPackNo = B.FTPackNo "
                    _Qry &= vbCrLf & "    AND A.FTOrderNo=B.FTOrderNo"
                    _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                    _Qry &= vbCrLf & "    AND A.FTColorway = B.FTColorway"
                    _Qry &= vbCrLf & "    AND A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN "
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS CT WITH(NOLOCK)"
                    _Qry &= vbCrLf & "    ON A.FNHSysCartonId = CT.FNHSysCartonId "

                    _Qry &= vbCrLf & "       LEFT OUTER JOIN ( SELECT     FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId"
                    _Qry &= vbCrLf & ",   sum(FNScanQuantity) AS FNScanQuantity"
                    _Qry &= vbCrLf & "FROM         TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                    _Qry &= vbCrLf & "group by  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId ) AS Z ON A.FTPackNo = Z.FTPackNo"
                    _Qry &= vbCrLf & "and A.FNCartonNo = Z.FNCartonNo and A.FTOrderNo = Z.FTOrderNo and A.FTSubOrderNo = Z.FTSubOrderNo and A.FTColorway = Z.FTColorway"
                    _Qry &= vbCrLf & "and A.FTSizeBreakDown = Z.FTSizeBreakDown"


                    _Qry &= vbCrLf & "   WHERE  A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    'If _PSCartonNo > 0 Then
                    '    _Qry &= vbCrLf & " And  A.FNCartonNo=" & _PSCartonNo
                    'End If
                    _Qry &= vbCrLf & "   GROUP BY  A.FTPackNo, A.FNCartonNo,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"
                    _Qry &= vbCrLf & "   ORDER BY A.FNCartonNo"

                    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    _PDataNode = dt
                    For Each R As DataRow In dt.Rows
                        nodeChild = _Lst.AppendNode(New Object() {Me.FNCartonNo2_lbl.Text & "" & R!FNCartonNo.ToString & " (" & R!FTCartonInfo.ToString & ")", R!FNCartonNo.ToString, _
                                                                  R!FNQuantity.ToString, R!FNNetWeight.ToString, R!FNHSysCartonId.ToString, R!FTCartonCode.ToString, R!FNWeight.ToString, _
                                                                  R!FNPackCartonSubType.ToString, R!FNPackPerCarton.ToString, R!FNScanQuantity.ToString, R!FTBarCodeCarton.ToString}, node)
                        nodeChild.HasChildren = False

                        _QtyCarton = Integer.Parse(Val(R!FNQuantity))
                        _PackPerQty = Integer.Parse(Val(R!FNPackPerCarton))

                        Select Case True
                            Case (CDbl(R!FNScanQuantity) = _QtyCarton)
                                If _QtyCarton = _PackPerQty Then
                                    nodeChild.ImageIndex = 1
                                    nodeChild.SelectImageIndex = 1
                                Else
                                    nodeChild.ImageIndex = 8
                                    nodeChild.SelectImageIndex = 8
                                End If

                            'If _PSCartonNo = Integer.Parse(R!FNCartonNo.ToString) Then
                            '    Call ScanCarton(Integer.Parse("0" & R!FNCartonNo.ToString), "1")
                            'End If

                            Case Else
                                If _QtyCarton = _PackPerQty Then
                                    nodeChild.ImageIndex = 4
                                    nodeChild.SelectImageIndex = 4
                                Else
                                    nodeChild.ImageIndex = 6
                                    nodeChild.SelectImageIndex = 6
                                End If
                                'Call ScanCarton(Integer.Parse("0" & R!FNCartonNo.ToString), "0")

                                'If Me.FTStateDeleteBarcode.Checked = True Then
                                '    Call DeleteBarcode(Me.FTPackNo.Text, Integer.Parse("0" & R!FNCartonNo.ToString))
                                'End If
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

    Private Function ScanCarton(ByVal _CartanNo As String, ByVal _State As String) As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton"
            _Cmd &= vbCrLf & "SET FTState='" & _State & "'"
            _Cmd &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                If _State = "1" Then
                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton(FTInsUser, FDInsDate, FTInsTime, FTPackNo, FNCartonNo, FTState)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & "," & CInt(_CartanNo)
                    _Cmd &= vbCrLf & ",'1'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            End If

            Try
                Dim _Qry As String = ""
                _Qry = "   UPDATE A SET FTStatePack ='" & _State & "'	 "
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderStatus AS A"
                _Qry &= vbCrLf & "      INNER Join"
                _Qry &= vbCrLf & "  ("
                _Qry &= vbCrLf & " SELECT D.FTOrderNo, D.FTSubOrderNo"
                _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS H INNER JOIN"
                _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS D ON H.FTPackNo = D.FTPackNo AND H.FNCartonNo = D.FNCartonNo"
                _Qry &= vbCrLf & " WHERE H.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Qry &= vbCrLf & " AND  H.FNCartonNo=" & CInt(_CartanNo)
                _Qry &= vbCrLf & " GROUP BY D.FTOrderNo, D.FTSubOrderNo"
                _Qry &= vbCrLf & "   ) AS B ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "    AND A.FTSubOrderNo = B.FTSubOrderNo"
                _Qry &= vbCrLf & "     WHERE A.FTStatePack <>'1'"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Catch ex As Exception
            End Try

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

    Private Function DeleteBarcode(ByVal _PackNo As String, ByVal _CartonNo As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _ScanQty As Integer = 0
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton"
            _Cmd &= vbCrLf & " WHERE FTPackNo  in(Select Top 1 C.FTPackNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S ON C.FTPackNo = S.FTPackNo"
            _Cmd &= vbCrLf & " and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE C.FTPackNo = '" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " and C.FNCartonNo = " & CInt(_CartonNo)


            _Cmd &= vbCrLf & "  )"
            _Cmd &= vbCrLf & " and FNCartonNo in (Select Top 1 C.FNCartonNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S ON C.FTPackNo = S.FTPackNo"
            _Cmd &= vbCrLf & " and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE C.FTPackNo = '" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " and C.FNCartonNo =" & CInt(_CartonNo)


            _Cmd &= vbCrLf & " )"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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
            Return False
        End Try
    End Function



    Private Function SaveWeight() As Boolean

        'With CType(Me.ogcpackdetailWeight.DataSource, DataTable)

        '    Dim _FTOrderNo As String = ""
        '    Dim _FTSubOrderNo As String = ""
        '    Dim _FTColorway As String = ""
        '    Dim _Qry As String = ""

        '    For Each R As DataRow In .Rows

        '        _FTOrderNo = R!FTOrderNo.ToString()
        '        _FTSubOrderNo = R!FTSubOrderNo.ToString()
        '        _FTColorway = R!FTColorway.ToString()

        '        For Each Col As DataColumn In .Columns
        '            Select Case Col.ColumnName.ToUpper
        '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper

        '                Case "Total"
        '                Case Else
        '                    If _FTSubOrderNo <> "" Then
        '                        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail"
        '                        _Qry &= vbCrLf & " SET FNWeight=" & Val(R.Item(Col)) & ""
        '                        _Qry &= vbCrLf & " WHERE  (FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
        '                        _Qry &= vbCrLf & "  AND (FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(_FTSubOrderNo) & "') "
        '                        _Qry &= vbCrLf & "  AND (FTColorway = '" & HI.UL.ULF.rpQuoted(_FTColorway) & "') "
        '                        _Qry &= vbCrLf & "  AND (FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(Col.ColumnName) & "') "

        '                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

        '                    End If
        '            End Select
        '        Next
        '    Next
        'End With


        Return True
    End Function


#End Region

    Private Sub FNOrderPackType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOrderPackType.SelectedIndexChanged
        Try

            FNPackSetValue_lbl.Visible = (FNOrderPackType.SelectedIndex = 1)
            FNPackSetValue.Visible = (FNOrderPackType.SelectedIndex = 1)

            If (FNOrderPackType.SelectedIndex = 0) Then
                FNPackSetValue.Value = 0
                ogrpQtyScanset.Visible = False

            Else
                ogrpQtyScanset.Visible = True


            End If

            XtraTabPage2.Visible = (FNOrderPackType.SelectedIndex = 1)
            XtraTabPage4.Visible = (FNOrderPackType.SelectedIndex = 1)
            XtraTabPage2.Enabled = (FNOrderPackType.SelectedIndex = 1)
            XtraTabPage4.Enabled = (FNOrderPackType.SelectedIndex = 1)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub wCreatePackOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _PSCartonNo = 0
            Call InitGrid()
            Me.FTProductBarcodeNo.EnterMoveNextControl = False
            Me.FTCartonBarcodeNo.EnterMoveNextControl = False
            Me.ogvScan.OptionsView.ShowAutoFilterRow = False
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
                Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)
        End Select
    End Sub

    Private Sub ocmgeneratecarton_Click(sender As Object, e As EventArgs)
        Call CreateTreeCarton()
    End Sub

    Private _StateSumGrid As Boolean
    Private Sub SumGrid()
        _StateSumGrid = True

        _StateSumGrid = False
    End Sub

    Private Sub ReposCaleditWeight_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)

    End Sub

    Private Sub ocmsaveweightpack_Click(sender As Object, e As EventArgs) Handles ocmsaveweightpack.Click

    End Sub

    Private _PFNCartonNo As String = "0"

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
                            Dim _FNScanQty As String = .GetValue(9).ToString


                            _PFNCartonNo = _FNCartonNo

                            FNHSysCartonId.Text = _FTCartonCode

                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNNW.Value = Val(_FNWeight)
                            FNCartonNo.Text = _FNCartonNo

                            Dim _Qry As String = ""

                            _Qry = " Select TOP 1  A.FTUnitSectCode"
                            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS P WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS A WITH(NOLOCK) ON P.FNHSysUnitSectId = A.FNHSysUnitSectId"
                            _Qry &= vbCrLf & "  WHERE  (P.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
                            _Qry &= vbCrLf & "   AND (P.FNCartonNo =" & Integer.Parse(Val(_FNCartonNo)) & ")"

                            'FNHSysUnitSectId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")


                            Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))

                            Call _LoadDataScan()
                            Me.FTCartonBarcodeNo.Text = ""
                            FTCartonBarcodeNo.Text = GetBarcode(FNCartonNo.Text)

                            If FTCartonBarcodeNo.Text <> "" Then
                                FTCartonBarcodeNo.Properties.Tag = FTCartonBarcodeNo.Text
                            End If


                            Me.ockmanual.Checked = False

                        End If
                    End With
                End If
            End With


        Catch ex As Exception
        End Try
    End Sub

    Private Function GetBarcode(_CartonNo As String) As String
        Try
            Dim _Cmd As String = "" : Dim _retrun As String = ""
            Dim _oDt As DataTable

            _Cmd = "	SELECT  distinct   FTBarCodeCarton "
            _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE        (FTPackNo = N'" & Me.FTPackNo.Text & "')"
            _Cmd &= vbCrLf & "and FNCartonNo =" & Integer.Parse("0" & _CartonNo)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _oDt.Rows
                If _retrun <> "" Then _retrun &= ","
                _retrun &= "" & R!FTBarCodeCarton.ToString
            Next

            Return _retrun
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Function Save_Scan(ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String, _
                               ByVal _SizeBreakDown As String, ByVal _BarcodeNo As String, ByVal _UnitSectId As Integer, ByVal _ScanQty As Double) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
            _Qry &= vbCrLf & " SET FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",FNScanQuantity=FNScanQuantity+" & CDbl(_ScanQty)
            _Qry &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Qry &= vbCrLf & "and FNCartonNo=" & CInt("0" & _PFNCartonNo)
            _Qry &= vbCrLf & "and FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Qry &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Qry &= vbCrLf & "and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Qry &= vbCrLf & "and FNHSysUnitSectId=" & CInt(_UnitSectId)
            _Qry &= vbCrLf & "and FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                _Qry &= "(FTInsUser, FDInsDate, FTInsTime,   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId,FTBarcodeNo, FNScanQuantity)"
                _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Qry &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Qry &= vbCrLf & "," & CInt(_UnitSectId)
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _Qry &= vbCrLf & "," & CDbl(_ScanQty)

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            _Qry = " Declare @Time varchar(5) , @Date varchar(10)"
            _Qry &= vbCrLf & " SELECT @Time =Convert(varchar(5),Getdate(),114) , @Date =" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
            _Qry &= vbCrLf & " Set FNScanQuantity =FNScanQuantity+" & CDbl(_ScanQty)
            _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Qry &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Qry &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Qry &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Qry &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
            _Qry &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _Qry &= vbCrLf & " AND FDScanDate=@Date"
            _Qry &= vbCrLf & " AND FDScanTime=@Time"
            _Qry &= vbCrLf & " IF @@ROWCOUNT = 0"
            _Qry &= vbCrLf & "  BEGIN"
            _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail (FTInsUser, FDInsDate, FTInsTime,"
            _Qry &= "  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,  FDScanDate, FDScanTime, FNScanQuantity)"
            _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Qry &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Qry &= vbCrLf & "," & CInt(_UnitSectId)
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _Qry &= vbCrLf & ",@Date"
            _Qry &= vbCrLf & ",@Time"
            _Qry &= vbCrLf & "," & CDbl(_ScanQty)
            _Qry &= vbCrLf & "  END"

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


    Private Function Check_Barcode(ByVal _Barcode As String, ByRef _OrderNo As String, ByRef _ColorWay As String, ByRef _SizeBreakDown As String, ByRef _SubOrderNo As String, ByRef _CartonQty As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim NewCW As String = ""
            Dim _Qry As String = ""
            Dim _AssortState As Boolean

            _Cmd = " SELECT  Top 1     OC.FTOrderNo, OC.FTColorway, OC.FTSizeBreakDown, OC.FTCustBarcodeNo, OP.FTSubOrderNo, OP.FTOrderProdNo, OP.FNQuantity"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS OC WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS OP WITH (NOLOCK) ON OC.FTOrderNo = OP.FTOrderNo AND OC.FTColorway = OP.FTColorway AND OC.FTSizeBreakDown = OP.FTSizeBreakDown"
            _Cmd &= vbCrLf & "WHERE OC.FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(FTProductBarcodeNo.Text) & "'"
            '_Cmd &= vbCrLf & "AND OC.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_PFTSizeBreakDown) & "'"
            _Cmd &= vbCrLf & "AND OC.FTColorway='" & HI.UL.ULF.rpQuoted(_PFTColorway) & "'"
            _Cmd &= vbCrLf & "AND OP.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            If _oDt.Rows.Count <= 0 Then

                _Cmd = "  SELECT     B.FTBarcodeBundleNo, B.FTColorway, B.FTSizeBreakDown,  D.FTOrderNo, D.FTSubOrderNo, D.FTOrderProdNo, D.FNQuantity, B.FTColorwayNew "
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D WITH (NOLOCK) ON B.FTOrderProdNo = D.FTOrderProdNo "
                _Cmd &= vbCrLf & "           AND B.FTColorway = D.FTColorway AND B.FTSizeBreakDown = D.FTSizeBreakDown"
                _Cmd &= vbCrLf & " WHERE B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_Barcode) & "'"
                _Cmd &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_PFTColorway) & "'"
                _Cmd &= vbCrLf & " AND D.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "'"

                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            End If

            _SizeBreakDown = _PFTSizeBreakDown
            For Each R As DataRow In _oDt.Rows

                _OrderNo = R!FTOrderNo.ToString
                _ColorWay = R!FTColorway.ToString
                '_SizeBreakDown = R!FTSizeBreakDown.ToString
                _SubOrderNo = R!FTSubOrderNo.ToString
                _CartonQty = CInt(R!FNQuantity)

                If (_NewColorway <> "") Then
                    _ColorWay = _NewColorway
                End If

            Next

            _PFTOrderNo = _OrderNo
            _PFTSubOrderNo = _SubOrderNo
            _PFTColorway = _ColorWay
            _PFTSizeBreakDown = _SizeBreakDown

            Return (_oDt.Rows.Count > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetBarcodeCust(BarcodeKey As String) As String
        Try
            Dim _FTProductBarcodeNo As String = BarcodeKey
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _FTOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _FTSizeBreakDown As String = ""

            _Qry = "  SELECT TOP  1  P.FTOrderNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P WITH(NOLOCK) ON A.FTOrderNo = P.FTOrderNo AND A.FTColorway = P.FTColorway AND A.FTSizeBreakDown = P.FTSizeBreakDown"
            _Qry &= vbCrLf & " WHERE (A.FTCustBarcodeNo = N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"
            _Qry &= vbCrLf & " AND (P.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Qry &= vbCrLf & " GROUP BY A.FTCustBarcodeNo, P.FTPackNo, P.FTOrderNo"

            _FTPackOfOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

            _Qry = "SELECT  TOP 1  FTOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "' "

            If _FTPackOfOrderNo <> "" Then
                _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTPackOfOrderNo) & "' "
            End If

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows

                    _FTOrderNo = R!FTOrderNo.ToString
                    _FTColorway = R!FTColorway.ToString
                    _FTSizeBreakDown = R!FTSizeBreakDown.ToString

                    _Qry = "   SELECT TOP 1 FTBarcodeBundleNo"
                    _Qry &= vbCrLf & "   FROM "
                    _Qry &= vbCrLf & " (SELECT B.FTColorway, B.FTSizeBreakDown, B.FTOrderProdNo, B.FNQuantity, B.FTBarcodeBundleNo,ISNULL((SELECT SUM(FNScanQuantity) AS Qty"
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPACKOrderPack_Carton_Scan(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  (FTBarcodeNo =B.FTBarcodeBundleNo) ),0) AS Qty"
                    _Qry &= vbCrLf & " FROM     (SELECT DISTINCT FTBarcodeNo"
                    _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail WITH (NOLOCK)"
                    _Qry &= vbCrLf & "  ) AS A INNER JOIN"  'WHERE FNHSysUnitSectId =" & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & "
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & " WHERE LEFT(B.FTOrderProdNo," & _FTOrderNo.Length & ") ='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
                    _Qry &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'"
                    _Qry &= vbCrLf & " AND  B.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "'"
                    _Qry &= vbCrLf & "  ) AS A"
                    _Qry &= vbCrLf & " WHERE FNQuantity > Qty"

                    _FTProductBarcodeNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                Next
            End If

            Return _FTProductBarcodeNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function CheckBarcodeOutline(BarCodeKey As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     FTBarcodeNo, FNHSysUnitSectId,   sum(FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline  WITH(NOLOCK) "

            _Cmd &= vbCrLf & "WHERE  FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarCodeKey) & "' OR FTBarcodeCustRef='" & HI.UL.ULF.rpQuoted(BarCodeKey) & "' "

                _Cmd &= vbCrLf & " group by FTBarcodeNo, FNHSysUnitSectId"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Return _oDt.Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckBarcodeCarton(Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTBarCodeCarton  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTBarCodeCarton ='" & Key & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> ""
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetStateHanger() As Boolean
        Try
            Dim _Qry As String = ""
            _Qry = "   SELECT TOP 1 FTPackNo"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE     (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Qry &= vbCrLf & " and Isnull(FTStateHanger,'') = '1'"
            Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkBarcodeCorton(BarcodeCarton As String, OrderNo As String, SubOrderNo As String, Colorway As String, BreakDown As String, CartonNo As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT Top  1 CB.FTPackNo, CB.FNCartonNo, CD.FTOrderNo, CD.FTSubOrderNo, CD.FTColorway, CD.FTSizeBreakDown, CD.FNQuantity, CD.FNPackPerCarton"
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode As CB LEFT OUTER Join"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail As CD On CB.FNCartonNo = CD.FNCartonNo And CB.FTPackNo = CD.FTPackNo"
            _Cmd &= vbCrLf & " Where(CB.FTBarCodeCarton = '" & BarcodeCarton & "')"
            _Cmd &= vbCrLf & " and CD.FTOrderNo='" & OrderNo & "'"
            _Cmd &= vbCrLf & " and CD.FTSubOrderNo='" & SubOrderNo & "'"
            If Colorway <> "" Then
                _Cmd &= vbCrLf & " and CD.FTColorway='" & Colorway & "'"
            End If
            If BreakDown <> "" Then
                _Cmd &= vbCrLf & " and CD.FTSizeBreakDown='" & BreakDown & "'"
            End If
            _Cmd &= vbCrLf & " and CB.FNCartonNo=" & CartonNo
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count >= 1
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckBarcodeCust(BarcodeKey As String, Optional StateDelete As Boolean = False, Optional CartonNo As Integer = 0) As String
        Dim _FTProductBarcodeNo As String = BarcodeKey
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _FTOrderNo As String = ""
        Dim _FTColorway As String = ""
        Dim _FTSizeBreakDown As String = "" : Dim _StateOutline As Boolean = False
        Dim _POLineItemNo As String = ""

        If Me.FTCartonBarcodeNo.Text <> "" And _FTProductBarcodeNo <> "" Then

            _Qry = "Select Top 1 B.FTBarcodeBundleNo ,CASE WHEN ISNULL(B.FTColorwayNew,'') ='' THEN  B.FTColorway ELSE ISNULL(B.FTColorwayNew,'') END AS FTColorway"
            _Qry &= vbCrLf & ",B.FTSizeBreakDown"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(B.FTChangeToLineItemNo,'') ='' THEN  B.FTPOLineItemNo ELSE ISNULL(B.FTChangeToLineItemNo,'') END AS  FTPOLineItemNo"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) "
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd As P With(NOLOCK)  On B.FTOrderProdNo = P.FTOrderProdNo "
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrder_CustBarcode As A With(NOLOCK) On P.FTOrderNo = A.FTOrderNo "
            _Qry &= vbCrLf & "And B.FTColorway = A.FTColorway And B.FTSizeBreakDown = A.FTSizeBreakDown "
            _Qry &= vbCrLf & "Where B.FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "' "
            _Qry &= vbCrLf & " and A.FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "' "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            If _dt.Rows.Count <= 0 Then

                If CheckBarcodeCarton(HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text)) Then
                    GoTo L0
                End If

                HI.MG.ShowMsg.mInfo("ไม่มีข้อมูลการบรรจุกล่อง ", 1610121501, Me.Text, "", MessageBoxIcon.Warning)
                Return ""

            End If

            For Each R As DataRow In _dt.Rows

                _PFTColorway = R!FTColorway.ToString
                _PFTSizeBreakDown = R!FTSizeBreakDown.ToString

                Return Me.FTCartonBarcodeNo.Text

            Next

        End If
L0:

        _Qry = "  Select TOP  1  P.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode As A With(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail As P With(NOLOCK) On A.FTOrderNo = P.FTOrderNo And A.FTColorway = P.FTColorway And A.FTSizeBreakDown = P.FTSizeBreakDown"
        _Qry &= vbCrLf & " WHERE (A.FTCustBarcodeNo = N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"
        _Qry &= vbCrLf & " AND (P.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
        _Qry &= vbCrLf & " GROUP BY A.FTCustBarcodeNo, P.FTPackNo, P.FTOrderNo"
        _FTPackOfOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

        _Qry = "SELECT  TOP 1   FTOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo ,FTSubOrderNo"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "' "
        If _PFTSubOrderNo <> "" Then
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "'"
        End If
        If _FTPackOfOrderNo <> "" Then
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTPackOfOrderNo) & "' "
        End If
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows

                _FTProductBarcodeNo = ""
                _FTOrderNo = R!FTOrderNo.ToString
                _FTColorway = R!FTColorway.ToString
                _FTSizeBreakDown = R!FTSizeBreakDown.ToString
                _POLineItemNo = GetFTNikePOLineItem(R!FTSubOrderNo.ToString, R!FTColorway.ToString)

                _Qry = "SELECT  Top 1  FTBarcodeBundleNo"
                _Qry &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode WITH(NOLOCK)   "
                _Qry &= vbCrLf & " where  FTBarCodeEAN13='" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "' "
                _Qry &= vbCrLf & " and FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                _FTProductBarcodeNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                If StateDelete = False Then
                    _StateOutline = CheckBarcodeOutline(BarcodeKey)
                    Dim _PQty As Integer = 0 : Dim _StateCarton As Boolean = False

                    For Each _row As DataRow In _PDataNode.Select("FNCartonNo=" & _PFNCartonNo)
                        _StateCarton = (Integer.Parse("0" & _row!FNPackPerCarton.ToString) = Integer.Parse("0" & _row!FNQuantity.ToString))
                        _PQty = Integer.Parse("0" & _row!FNPackPerCarton.ToString)
                        Exit For
                    Next

                    _Qry = "   SELECT TOP 1 FTBarcodeBundleNo"
                    _Qry &= vbCrLf & "   FROM "
                    _Qry &= vbCrLf & " (SELECT B.FTColorway, B.FTSizeBreakDown, B.FTOrderProdNo, B.FNQuantity, B.FTBarcodeBundleNo,ISNULL((SELECT SUM(FNScanQuantity) AS Qty"
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPACKOrderPack_Carton_Scan(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  (FTBarcodeNo =B.FTBarcodeBundleNo) ),0) AS Qty"
                    _Qry &= vbCrLf & " FROM     (SELECT DISTINCT FTBarcodeNo"

                    _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline WITH (NOLOCK)"

                    _Qry &= vbCrLf & "  ) AS A INNER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
                    _Qry &= vbCrLf & " WHERE "
                    If (Not GetStateHanger()) Then
                        If (CheckPackType(Me.FTPackNo.Text)) Then
                            If (_PQty > 0) And (_StateCarton) Then
                                _Qry &= vbCrLf & " ( B.FNQuantity =" & _PQty & ") and "
                            Else
                                _Qry &= vbCrLf & " ( B.FNQuantity <" & _PQty & ") and "
                            End If
                        End If
                    End If
                    _Qry &= vbCrLf & " LEFT(B.FTOrderProdNo," & _FTOrderNo.Length & ") ='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
                    _Qry &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'"
                    _Qry &= vbCrLf & " AND  B.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "'"
                    _Qry &= vbCrLf & " AND  B.FTPOLineItemNo ='" & _POLineItemNo & "'"
                    _Qry &= vbCrLf & " AND B.FTOrderProdNo +'|' + B.FTColorway + '|' + B.FTSizeBreakDown IN ("
                    _Qry &= vbCrLf & " SELECT B.FTOrderProdNo +'|' + B.FTColorway + '|' + B.FTSizeBreakDown "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail  AS B WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE  B.FTSubOrderNo IN (SELECT  B.FTSubOrderNo"
                    _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS A LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS B ON A.FTPackNo = B.FTPackNo"
                    _Qry &= vbCrLf & "WHERE     (A.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'))"
                    _Qry &= vbCrLf & "  ) "
                    If _FTProductBarcodeNo <> "" Then
                        _Qry &= vbCrLf & "   and B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "' "
                    End If
                    _Qry &= vbCrLf & "  ) AS A"

                    '   _Qry &= vbCrLf & "INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline   AS B WITH(NOLOCK)   ON A.FTBarcodeBundleNo = B.FTBarcodeNo "  ' เปลี่ยนจากเช็ค มันงาน เป็น เช็คงานออก ไลน์ผลิต 20170824 Noh
                    _Qry &= vbCrLf & "INNER JOIN (Select min(FDUpdDate) as FDUpdDate , min(FDInsDate) as FDInsDate , min(FTUpdTime) as FTUpdTime , min(FTInsTime) AS FTInsTime ,  sum(FNQuantity) as  FNQuantity , FTBarcodeNo   from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline  WITH(NOLOCK) group by FTBarcodeNo  )  AS B     ON A.FTBarcodeBundleNo = B.FTBarcodeNo  "
                    If FTStateDeleteBarcode.Checked = False Then
                        '_Qry &= vbCrLf & " WHERE A.FNQuantity > Qty"  ' เปลี่ยนจากเช็ค มันงาน เป็น เช็คงานออก ไลน์ผลิต 20170824 Noh


                        _Qry &= vbCrLf & " WHERE B.FNQuantity > Qty"
                    End If

                    _Qry &= vbCrLf & "ORDER BY Isnull(B.FDUpdDate, B.FDInsDate ), Isnull(B.FTUpdTime, B.FTInsTime) ASC "
                    _FTProductBarcodeNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")
                Else
                    If CartonNo > 0 Then
                        _Qry = "   SELECT TOP 1 FTBarcodeNo "
                        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS A WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                        _Qry &= vbCrLf & " AND (FNCartonNo = " & CartonNo & ") "
                        '_Qry &= vbCrLf & " AND (FNHSysUnitSectId = " & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & ") "
                        _Qry &= vbCrLf & " AND (FTOrderNo ='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "') "
                        _Qry &= vbCrLf & " AND (FTColorway ='" & HI.UL.ULF.rpQuoted(_FTColorway) & "') "
                        _Qry &= vbCrLf & " AND (FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "')"
                        _Qry &= vbCrLf & " ORDER BY FDScanDate +' '+  FDScanTime DESC  "

                        _FTProductBarcodeNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")
                    End If
                End If
                Exit For
            Next
        End If

        _PFTOrderNo = ""
        _PFTSubOrderNo = ""
        _PFTColorway = ""
        _PFTSizeBreakDown = ""
        _PAFTColorway = ""
        _PFTAssortState = False

        If _FTProductBarcodeNo <> "" Then
            _Qry = "    SELECT TOP  1  A.FTColorway, A.FTSizeBreakDown, A.FTOrderProdNo, B.FTOrderNo, B.FTSubOrderNo "
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail  AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & " ON  A.FTOrderProdNo = B.FTOrderProdNo AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            _Qry &= vbCrLf & "INNER  JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS BD WITH (NOLOCK) ON A.FTPOLineItemNo = BD.FTNikePOLineItem AND B.FTOrderNo = BD.FTOrderNo AND B.FTSubOrderNo = BD.FTSubOrderNo AND B.FTColorway = BD.FTColorway AND B.FTSizeBreakDown = BD.FTSizeBreakDown "
            _Qry &= vbCrLf & "  WHERE  (A.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "' )"
            _Qry &= vbCrLf & "    and B.FTSubOrderNo IN (SELECT  B.FTSubOrderNo"
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS A LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS B ON A.FTPackNo = B.FTPackNo"
            _Qry &= vbCrLf & "WHERE     (A.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'))"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            If _dt.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ลูกค้า  !!!", 1502130111, Me.Text, , MessageBoxIcon.Warning)
            End If

            For Each R As DataRow In _dt.Rows
                _PFTOrderNo = R!FTOrderNo.ToString
                _PFTSubOrderNo = R!FTSubOrderNo.ToString
                _PFTColorway = R!FTColorway.ToString
                _PFTSizeBreakDown = R!FTSizeBreakDown.ToString

            Next
        Else

            '_Qry = "SELECT Top 1  FTBarcodeNo"
            '_Qry &= vbCrLf & "  FROM     (SELECT DISTINCT FTBarcodeNo FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail WITH (NOLOCK)"
            '_Qry &= vbCrLf & "   ) AS A INNER JOIN"
            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & " WHERE LEFT(B.FTOrderProdNo," & _FTOrderNo.Length & ") ='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
            '_Qry &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'"
            '_Qry &= vbCrLf & " AND  B.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "'"
            '_Qry &= vbCrLf & "   "



            _Qry = "SELECT Top 1  FTBarcodeNo"
            _Qry &= vbCrLf & "  FROM     (SELECT DISTINCT FTBarcodeNo FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline WITH (NOLOCK)"
            _Qry &= vbCrLf & "   ) AS A INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " WHERE LEFT(B.FTOrderProdNo," & _FTOrderNo.Length & ") ='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
            _Qry &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'"
            _Qry &= vbCrLf & " AND  B.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "'"
            _Qry &= vbCrLf & "   "


            Dim _FTBarcodeNo As String = HI.UL.ULF.rpQuoted(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, ""))
            If _FTBarcodeNo = "" Then
                HI.MG.ShowMsg.mInfo("สแกนเต็มกล่องแล้ว !!!", 1502170110, Me.Text, , MessageBoxIcon.Question)
            Else

                HI.MG.ShowMsg.mInfo("ไม่ได้ทำการ Scan เข้าไลน์ หรือ สแกนเต็มกล่องแล้ว !!!", 1502130110, Me.Text, , MessageBoxIcon.Warning)
            End If
        End If

        Return _FTProductBarcodeNo
    End Function

    Private Function CheckPackType(_FTPackNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTPackNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "Where FNOrderPackType=0 and FTPackNo='" & _FTPackNo & "'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function CheckBarcodeCust(BarcodeKey As String, CartonNo As String) As String
        Dim _FTProductBarcodeNo As String = BarcodeKey
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _FTOrderNo As String = ""
        Dim _FTColorway As String = ""
        Dim _FTSizeBreakDown As String = ""

        _Qry = "  SELECT TOP  1  P.FTOrderNo"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P WITH(NOLOCK) ON A.FTOrderNo = P.FTOrderNo AND A.FTColorway = P.FTColorway AND A.FTSizeBreakDown = P.FTSizeBreakDown"
        _Qry &= vbCrLf & " WHERE (A.FTCustBarcodeNo = N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"
        _Qry &= vbCrLf & " AND (P.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
        _Qry &= vbCrLf & " GROUP BY A.FTCustBarcodeNo, P.FTPackNo, P.FTOrderNo"

        _FTPackOfOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

        _Qry = "SELECT  TOP 1  FTOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "' "

        If _FTPackOfOrderNo <> "" Then
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTPackOfOrderNo) & "' "
        End If
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                _FTOrderNo = R!FTOrderNo.ToString
                _FTColorway = R!FTColorway.ToString
                _FTSizeBreakDown = R!FTSizeBreakDown.ToString

                _Qry = "   SELECT TOP 1 FTBarcodeBundleNo"
                _Qry &= vbCrLf & "   FROM "
                _Qry &= vbCrLf & " (SELECT B.FTColorway, B.FTSizeBreakDown, B.FTOrderProdNo, B.FNQuantity, B.FTBarcodeBundleNo,ISNULL((SELECT SUM(FNScanQuantity) AS Qty"
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPACKOrderPack_Carton_Scan(NOLOCK)"
                _Qry &= vbCrLf & " WHERE  (FTBarcodeNo =B.FTBarcodeBundleNo) ),0) AS Qty ,CS.FNCartonNo   "
                ',ISNULL((SELECT  Top 1  FNCartonNo  "
                '                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPACKOrderPack_Carton_Scan(NOLOCK)"
                '                _Qry &= vbCrLf & " WHERE  (FTBarcodeNo =B.FTBarcodeBundleNo) ),0) AS FNCartonNo "


                _Qry &= vbCrLf & " FROM     (SELECT DISTINCT FTBarcodeNo"
                _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail WITH (NOLOCK)"
                '_Qry &= vbCrLf & " WHERE FNHSysUnitSectId =" & Integer.Parse(Val(FNHSysUnitSectId.Properties.Tag.ToString)) & " ) AS A INNER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK)  ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
                _Qry &= vbCrLf & "INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS CS  WITH (NOLOCK) ON"
                _Qry &= vbCrLf & "  B.FTBarcodeBundleNo = CS.FTBarcodeNo"
                _Qry &= vbCrLf & "               and B.FTColorway = CS.FTColorway"
                _Qry &= vbCrLf & "              and B.FTSizeBreakDown = CS.FTSizeBreakDown"
                _Qry &= vbCrLf & " WHERE LEFT(B.FTOrderProdNo," & _FTOrderNo.Length & ") ='" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "'"
                _Qry &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_FTColorway) & "'"
                _Qry &= vbCrLf & " AND  B.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_FTSizeBreakDown) & "'"
                _Qry &= vbCrLf & "  ) AS A"
                _Qry &= vbCrLf & " WHERE FNCartonNo  = " & CInt("0" & CartonNo)

                _FTProductBarcodeNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

            Next
        End If

        _PFTOrderNo = ""
        _PFTSubOrderNo = ""
        _PFTColorway = ""
        _PFTSizeBreakDown = ""
        _PAFTColorway = ""
        _PFTAssortState = False

        If _FTProductBarcodeNo <> "" Then

            _Qry = "    SELECT TOP  1  A.FTColorway, A.FTSizeBreakDown, A.FTOrderProdNo, B.FTOrderNo, B.FTSubOrderNo "
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail  AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & " ON  A.FTOrderProdNo = B.FTOrderProdNo AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown "
            _Qry &= vbCrLf & "  WHERE  (A.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "' )"
            _Qry &= vbCrLf & " and B.FTSubOrderNo in "
            _Qry &= vbCrLf & " (SELECT   Top 1  B.FTSubOrderNo"
            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS A LEFT OUTER JOIN"
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS B ON A.FTPackNo = B.FTPackNo"
            _Qry &= vbCrLf & "WHERE     (A.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'))"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _dt.Rows
                _PFTOrderNo = R!FTOrderNo.ToString
                _PFTSubOrderNo = R!FTSubOrderNo.ToString
                _PFTColorway = R!FTColorway.ToString
                _PFTSizeBreakDown = R!FTSizeBreakDown.ToString

            Next
        End If

        Return _FTProductBarcodeNo
    End Function

    Private Function GetPackNo(ByVal BarcodeKey As String) As String
        Try

            Dim _FTProductBarcodeNo As String = BarcodeKey
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _dtPackNo As DataTable

            Dim _FTOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _FTSizeBreakDown As String = ""
            Dim _oDt As DataTable
            Dim _UnitSectId As Integer = 0
            Dim _PackNo As String = ""
            Dim _Statebarcodeset As Boolean = False
            _FTPackOfOrderNo = ""
            'Dim _StateOutline As Boolean = False
            '_StateOutline = CheckBarcodeOutline(BarcodeKey)




            If Me.FTPackNo.Text = "" Then
                _Qry = "  SELECT P.FTPackNo, P.FTOrderNo"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P WITH(NOLOCK) ON A.FTOrderNo = P.FTOrderNo AND A.FTColorway = P.FTColorway AND A.FTSizeBreakDown = P.FTSizeBreakDown"
                _Qry &= vbCrLf & " WHERE (A.FTCustBarcodeNo = N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"
                _Qry &= vbCrLf & " GROUP BY A.FTCustBarcodeNo, P.FTPackNo, P.FTOrderNo"
                _Qry &= vbCrLf & " ORDER BY  P.FTPackNo, P.FTOrderNo "
                _dtPackNo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                If _dtPackNo.Rows.Count <= 0 Then

                    'check barcode set 
                    _Qry = "  SELECT P.FTPackNo, P.FTOrderNo"
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet AS A WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P WITH(NOLOCK) ON A.FTPackNo = P.FTPackNo "
                    _Qry &= vbCrLf & " WHERE (A.FTBarcodeSetNo = N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"
                    _Qry &= vbCrLf & " GROUP BY P.FTPackNo, P.FTPackNo"
                    _Qry &= vbCrLf & " ORDER BY  P.FTPackNo, P.FTOrderNo "
                    _dtPackNo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    _Statebarcodeset = (_dtPackNo.Rows.Count > 0)

                End If

                Select Case _dtPackNo.Rows.Count
                    Case 1
                        _PackNo = _dtPackNo.Rows(0)!FTPackNo.ToString
                        _FTPackOfOrderNo = _dtPackNo.Rows(0)!FTOrderNo.ToString
                    Case Is >= 2

                        With _ListPackOrderNo
                            .ogclist.DataSource = _dtPackNo.Copy
                            .OrderNo = ""
                            .PackOrderNo = ""
                            .ShowDialog()
                            _PackNo = .PackOrderNo
                            _FTPackOfOrderNo = .OrderNo
                        End With

                End Select




            Else
                _Qry = "  SELECT TOP  1  P.FTOrderNo"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P WITH(NOLOCK) ON A.FTOrderNo = P.FTOrderNo AND A.FTColorway = P.FTColorway AND A.FTSizeBreakDown = P.FTSizeBreakDown"
                _Qry &= vbCrLf & " WHERE (A.FTCustBarcodeNo = N'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "')"
                _Qry &= vbCrLf & " AND (P.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
                _Qry &= vbCrLf & " GROUP BY A.FTCustBarcodeNo, P.FTPackNo, P.FTOrderNo"
                _FTPackOfOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "")
            End If

            _Qry = "SELECT  TOP 1  FTOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo , FTSubOrderNo "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "' "

            If _FTPackOfOrderNo <> "" Then
                _Qry &= vbCrLf & "  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTPackOfOrderNo) & "' "
            End If
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            _PFTOrderNo = ""
            _PFTSubOrderNo = ""
            _PFTColorway = ""
            _PFTSizeBreakDown = ""
            _PAFTColorway = ""
            _PFTAssortState = False

            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows
                    _PFTOrderNo = R!FTOrderNo.ToString
                    _PFTSubOrderNo = R!FTSubOrderNo.ToString
                    _PFTColorway = R!FTColorway.ToString
                    _PFTSizeBreakDown = R!FTSizeBreakDown.ToString

                    _Qry = " Select TOP 1  FTBarcodeNo as FTBarcodeBundleNo ,FNQuantity , FTBarcodeCustRef ,FNQty"
                    _Qry &= vbCrLf & "From (SELECT        B.FTBarcodeNo,  sum(B.FNQuantity) AS FNQuantity , B.FTBarcodeCustRef , C.FTOrderNo , C.FTSubOrderNo , C.FTSizeBreakDown , C.FTColorway "
                    _Qry &= vbCrLf & " ,Isnull(C.FNScanQuantity,0) as  FNQty"
                    _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline as B WITH(NOLOCK) "
                    _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH(NOLOCK) ON B.FTBarcodeNo = C.FTBarcodeNo "
                    If (_PStateBarCust) Then
                        _Qry &= vbCrLf & " WHERE B.FTBarcodeCustRef='" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'"
                    Else
                        _Qry &= vbCrLf & " WHERE B.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'"
                    End If
                    _Qry &= vbCrLf & " and C.FTOrderNo='" & _PFTOrderNo & "'"
                    _Qry &= vbCrLf & " and C.FTSubOrderNo='" & _PFTSubOrderNo & "'"
                    _Qry &= vbCrLf & " and C.FTSizeBreakDown='" & _PFTSizeBreakDown & "'"
                    _Qry &= vbCrLf & " and C.FTColorway='" & _PFTColorway & "'"

                    _Qry &= vbCrLf & "group by  B.FTBarcodeNo,  B.FTBarcodeCustRef , C.FTOrderNo , C.FTSubOrderNo , C.FTSizeBreakDown , C.FTColorway ) AS T "
                    _Qry &= vbCrLf & "where  FNQuantity > FNQty"


                    _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    For Each Row As DataRow In _oDt.Rows
                        _FTProductBarcodeNo = Row!FTBarcodeBundleNo.ToString
                        _UnitSectId = 0
                    Next

                Next
            End If

            If _FTProductBarcodeNo = "" Then
                _PFTOrderNo = ""
                _PFTSubOrderNo = ""
                _PFTColorway = ""
                _PFTSizeBreakDown = ""
                _PAFTColorway = ""
                _PFTAssortState = False
            End If

            Return _PackNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ChekBarcodeCust(BarCode As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTOrderNo    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrder_CustBarcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(BarCode) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> ""
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function ChkBreakTime() As Boolean
        Try
            If Me.FNHSysUnitSectId.Text = "" Then
                HI.MG.ShowMsg.mInfo("กรุณเลือกสังกัด......", 1711211727, Me.Text, "", MessageBoxIcon.Stop)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If
            Dim _Cmd As String = ""
            Dim _State As Boolean = False




            '_Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '_Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '_Cmd &= vbCrLf & "  Select  Top 1  FNHSysShiftID From  ( "
            '_Cmd &= vbCrLf & "  Select isnull( M.FNHSysShiftID ,  E.FNHSysShiftID) as FNHSysShiftID    , E.FNHSysUnitSectId    "
            '_Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock)  LEFT OUTER JOIN  "
            '_Cmd &= vbCrLf & "  ( SELECT S.FNHSysEmpID , s.FNHSysShiftID   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift as  S  WITH (NOLOCK)   "
            '_Cmd &= vbCrLf & " WHERE FDShiftDate =Convert(varchar(10),Getdate(),111) ) AS M ON e.FNHSysEmpID = M.FNHSysEmpID  "
            '_Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '_Cmd &= vbCrLf & "  ) AS T "
            '_Cmd &= vbCrLf & "  order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc )  "
            '_Cmd &= vbCrLf & " and FTStartTime <   Convert(varchar(5),Getdate(),114) and FTEndTime >   Convert(varchar(5),Getdate(),114)  "
            '_Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '1' and ISNULL(FTStateActive,'0') = '1' "

            '_State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 0

            'If _State Then

            '    _State = False
            '    _Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '    _Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '    _Cmd &= vbCrLf & "  Select  Top 1  FNHSysShiftID From  ( "
            '    _Cmd &= vbCrLf & "  Select isnull( M.FNHSysShiftID ,  E.FNHSysShiftID) as FNHSysShiftID     , E.FNHSysUnitSectId   "
            '    _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock)  LEFT OUTER JOIN  "
            '    _Cmd &= vbCrLf & "  ( SELECT S.FNHSysEmpID , s.FNHSysShiftID   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift as  S  WITH (NOLOCK)   "
            '    _Cmd &= vbCrLf & " WHERE FDShiftDate =Convert(varchar(10),Getdate(),111) ) AS M ON e.FNHSysEmpID = M.FNHSysEmpID  "
            '    _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '    _Cmd &= vbCrLf & "  ) AS T "
            '    _Cmd &= vbCrLf & "  order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc )  "
            '    _Cmd &= vbCrLf & " and  FTStartTime   <   Convert(varchar(5),Getdate(),114) and   FTEndTime   >   Convert(varchar(5),Getdate(),114)  "
            '    _Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '0' and ISNULL(FTStateActive,'0') = '1' "

            '    _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count > 0

            '    If Not (_State) Then

            '        _Cmd = "Select top 1  r.FTOtMIn , r.FTOtMOut , r.FTOtIn , r.FTOtOut  "
            '        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock) "
            '        _Cmd &= vbCrLf & " left outer join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest as r with(Nolock) on e.FNHSysEmpID = r.FNHSysEmpID "
            '        _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '        _Cmd &= vbCrLf & "  and  FTDateRequest = Convert(varchar(10),Getdate(),111)"
            '        _Cmd &= vbCrLf & "   and ( (r.FTOtMIn <= Convert(varchar(8),Getdate(),114) and r.FTOtMOut  >= Convert(varchar(8),Getdate(),114) ) OR  "
            '        _Cmd &= vbCrLf & "  ( r.FTOtIn <= Convert(varchar(8),Getdate(),114) and  r.FTOtOut  >= Convert(varchar(8),Getdate(),114) )) "
            '        _Cmd &= vbCrLf & "  order by r.FNOtNetTime desc "

            '        _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 1

            '    End If

            'Else

            '    _Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '    _Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '    _Cmd &= vbCrLf & "  Select  Top 1  FNHSysShiftID From  ( "
            '    _Cmd &= vbCrLf & "  Select isnull( M.FNHSysShiftID ,  E.FNHSysShiftID) as FNHSysShiftID    , E.FNHSysUnitSectId    "
            '    _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock)  LEFT OUTER JOIN  "
            '    _Cmd &= vbCrLf & "  ( SELECT S.FNHSysEmpID , s.FNHSysShiftID   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift as  S  WITH (NOLOCK)   "
            '    _Cmd &= vbCrLf & " WHERE FDShiftDate =Convert(varchar(10),Getdate(),111) ) AS M ON e.FNHSysEmpID = M.FNHSysEmpID  "
            '    _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '    _Cmd &= vbCrLf & "  ) AS T "
            '    _Cmd &= vbCrLf & "  order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc )  "
            '    _Cmd &= vbCrLf & " and   convert(nvarchar(5) , convert(datetime ,  dateadd(minute ,FNScanAfterBreakMin    , FTStartTime ) ) ,114)   <  Convert(varchar(5),Getdate(),114)     and FTEndTime >   Convert(varchar(5),Getdate(),114)  "
            '    _Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '1' and ISNULL(FTStateActive,'0') = '1' "

            '    _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 0

            '    If Not (_State) Then

            '        _Cmd = "Select top 1  r.FTOtMIn , r.FTOtMOut , r.FTOtIn , r.FTOtOut  "
            '        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock) "
            '        _Cmd &= vbCrLf & " left outer join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest as r with(Nolock) on e.FNHSysEmpID = r.FNHSysEmpID "
            '        _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '        _Cmd &= vbCrLf & "  and  FTDateRequest = Convert(varchar(10),Getdate(),111)"
            '        _Cmd &= vbCrLf & "   and ( (r.FTOtMIn <= Convert(varchar(8),Getdate(),114) and r.FTOtMOut  >= Convert(varchar(8),Getdate(),114) ) OR  "
            '        _Cmd &= vbCrLf & "  ( r.FTOtIn <= Convert(varchar(8),Getdate(),114) and  r.FTOtOut  >= Convert(varchar(8),Getdate(),114) )) "
            '        _Cmd &= vbCrLf & "  order by r.FNOtNetTime desc "

            '        _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 1

            '    End If

            'End If

            _Cmd = "  EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_CHECKTIME_SCANOUTLINE " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
            _State = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "1"

            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTProductBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter And Me.FTProductBarcodeNo.Text <> "" Then

                If Me.FNHSysUnitSectId.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysUnitSectId_lbl.Text)
                    Me.FNHSysUnitSectId.Focus()
                    Exit Sub
                End If

                If ChkBreakTime() = False Then
                    HI.MG.ShowMsg.mInfo("หมดเวลาแสถนงาน กรุณาไปพักตามอัตยาศัยครับ......", 1711211728, Me.Text, "", MessageBoxIcon.Stop)
                    Exit Sub
                End If

                If FTCartonBarcodeNo.Text.Trim() <> FTCartonBarcodeNo.Properties.Tag.ToString() Then
                    FTCartonBarcodeNo_KeyDown(FTCartonBarcodeNo, New System.EventArgs)
                End If

                If Me.FTPackNo.Text = "" Then

                    Me.FTPackNo.Text = GetPackNo(Me.FTProductBarcodeNo.Text)

                    If Me.ockmanual.Checked = False Then

                        SetNewCarton()

                    End If

                    Exit Sub

                End If

                If Me.ogbbarcodecarton.Visible Then

                    If Me.FTCartonBarcodeNo.Text = "" Then

                        HI.MG.ShowMsg.mInfo("Plase Scan Barcode Carton......", 1505190025, Me.Text, , MessageBoxIcon.Warning)
                        Me.FTCartonBarcodeNo.Text = ""
                        Me.FTCartonBarcodeNo.Focus()
                        Me.FTCartonBarcodeNo.SelectAll()
                        Exit Sub

                    End If

                End If

                If Not CheckBarcodeCarton(HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text)) Then

                    'If Not (ChkBarcodeCorton(Me.FTCartonBarcodeNo.Text, _PFTOrderNo, _PFTSubOrderNo, _PFTColorway, _PFTSizeBreakDown, _PFNCartonNo)) Then

                    HI.MG.ShowMsg.mInfo("Barcode customer   ไม่ตรงกับกล่อง กรุณาตรวจสอบ !!!!!", 1610121605, "", "", MessageBoxIcon.Error)
                    Me.FTProductBarcodeNo.Focus()
                    Me.FTProductBarcodeNo.SelectAll()
                    Exit Sub

                    'End If

                End If

                If CheckScanCloseCarton(Me.FTPackNo.Text, _PFNCartonNo) Then Exit Sub

                Dim BarCodeSet As Boolean = False
                BarCodeSet = (FNOrderPackType.SelectedIndex = 1)


                If Scan_Process_Store = False Then

                    Call AddBarCode()

                Else

                    If BarCodeSet = False Then

                        Dim cmdstring As String = ""

                        If Me.FTStateDeleteBarcode.Checked = False Then
                            cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_SCAN_DATA_PACK '" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTCartonBarcodeNo.Text) & "'," & _PFNCartonNo & ",'" & HI.UL.ULF.rpQuoted(FTProductBarcodeNo.Text) & "'," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        Else
                            cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_SCAN_DATA_PACK_DELETE '" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTCartonBarcodeNo.Text) & "'," & _PFNCartonNo & ",'" & HI.UL.ULF.rpQuoted(FTProductBarcodeNo.Text) & "'," & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        End If

                        Dim MsgType As Integer = 0

                        MsgType = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PROD, "0"))

                        Select Case MsgType
                            Case 1

                                Me._LoadDataScan()
                                Me.FTProductBarcodeNo.Focus()
                                Me.FTProductBarcodeNo.SelectAll()

                            Case Else

                                Select Case MsgType
                                    Case 10 'ไม่พบข้อมูบ Barcode ลูกค้า

                                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลบาร์โค๊ดลูกค้า !!! ", 1411210003, Me.Text, "", MessageBoxIcon.Warning)

                                    Case 11 'ไม่พบข้อมูบ Barcode ลูกค้า

                                        HI.MG.ShowMsg.mInfo("สแกนครบจำนวนออกไลน์แล้ว !!! ", 1411210008, Me.Text, "", MessageBoxIcon.Warning)

                                    Case 20 'ไม่พบข้อมูลกล่อง

                                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลกล่อง !!!", 1411210004, Me.Text, "", MessageBoxIcon.Warning)

                                    Case 21 'Sacn ครบกล่องแล้ว
                                        If Me.FTStateDeleteBarcode.Checked = False Then
                                            HI.MG.ShowMsg.mInfo("กล่องนี้ scan ยอดครบแล้ว !!! ", 1411210005, Me.Text, "", MessageBoxIcon.Warning)
                                        End If


                                    Case 30 'ไม่พบข้อมูล Barcode มัดงาน

                                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลมัดงาน !!! ", 1411210006, Me.Text, "", MessageBoxIcon.Warning)

                                    Case 31 'Sacn ครบหมดแล้ว

                                        HI.MG.ShowMsg.mInfo("Over Scan Barcode....   " & FTProductBarcodeNo.Text.Trim(), 1412040001, Me.Text, "", MessageBoxIcon.Warning)

                                    Case 40 'ไม่ได้ Pack ในกล่องนี้

                                        HI.MG.ShowMsg.mInfo("ไม่มีข้อมูลการบรรจุกล่อง ", 1411210002, Me.Text, "", MessageBoxIcon.Warning)

                                    Case -99, -1 'Error Save ไม่ได้

                                        If Me.FTStateDeleteBarcode.Checked And MsgType = -1 Then
                                            HI.MG.ShowMsg.mInfo("ไม่สามารถทำการลบได้ กรุณาทำการตรวจสอบข้อมูล !!! ", 1411218899, Me.Text, "", MessageBoxIcon.Warning)
                                        Else
                                            HI.MG.ShowMsg.mInfo("ไม่สามารถแสกนได้ ", 1411210099, Me.Text, "", MessageBoxIcon.Warning)
                                        End If

                                End Select

                                Me.FTProductBarcodeNo.Focus()
                                Me.FTProductBarcodeNo.SelectAll()

                        End Select

                    Else

                        If checkTracksuit(Me.FTPackNo.Text, FTProductBarcodeNo.Text) Then
                            Call ProcessBarcodeSetTracksuit(Me.FTPackNo.Text, FTProductBarcodeNo.Text)
                        Else
                            Call ProcessBarcodeSet(Me.FTPackNo.Text, FTProductBarcodeNo.Text)
                        End If




                    End If

                    End If

            Else
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub AddBarCode()

        Dim _TotalCarton As Integer = GetTotalCarton()
        Dim _FTOrderNo As String = ""
        Dim _FTColorWay As String = ""
        Dim _FTSizeBreakDown As String = ""
        Dim _FTSubOrderNo As String = ""
        Dim _CartonQty As Integer = 0
        Dim _FTProductBarcodeNo As String = ""
        'Get PackNo
        _FTPackOfOrderNo = ""
        Dim BarCodeSet As Boolean = False

        '  _PStateBarCust = ChekBarcodeCust(Me.FTProductBarcodeNo.Text)

        BarCodeSet = (FNOrderPackType.SelectedIndex = 1)



        If BarCodeSet = False Then

            If _PFNCartonNo <> "" And Me.FTStateDeleteBarcode.Checked Then
                _FTProductBarcodeNo = CheckBarcodeCust(FTProductBarcodeNo.Text, Me.FTStateDeleteBarcode.Checked, Integer.Parse(Val(_PFNCartonNo)))
            Else

                _FTProductBarcodeNo = CheckBarcodeCust(FTProductBarcodeNo.Text)
            End If

            FTBarcodeBundleNo.Text = _FTProductBarcodeNo
            If _FTProductBarcodeNo = "" Then
                'HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ลูกค้า หรืออาจยังไม่ได้ทำการ Scan เข้าไลน์ !!!", 1412100101, Me.Text, , MessageBoxIcon.Warning)
                Me.FTProductBarcodeNo.Focus()
                Me.FTProductBarcodeNo.SelectAll()
                Exit Sub
            End If

            If Me.FTStateDeleteBarcode.Checked = False Then



                If CheckBarcodeBundle(_FTProductBarcodeNo) Then

                    'If Not CheckBarcodeCarton(HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text)) Then
                    '    'If Not (ChkBarcodeCorton(Me.FTCartonBarcodeNo.Text, _PFTOrderNo, _PFTSubOrderNo, _PFTColorway, _PFTSizeBreakDown, _PFNCartonNo)) Then
                    '    HI.MG.ShowMsg.mInfo("Barcode customer   ไม่ตรงกับกล่อง กรุณาตรวจสอบ !!!!!", 1610121605, "", "", MessageBoxIcon.Error)
                    '    Me.FTProductBarcodeNo.Focus()
                    '    Me.FTProductBarcodeNo.SelectAll()
                    '    Exit Sub
                    '    'End If
                    'End If

                    'If CheckQtyCarton(_FTProductBarcodeNo, _PFNCartonNo, _PFTOrderNo, _PFTSubOrderNo, _PFTColorway, _PFTSizeBreakDown) = False Then
                    '    HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลกล่อง Pack สีนี้ ไซส์นี้ หรือ Scan ครบกล่องแล้ว !! ", 1412100119, Me.Text)
                    '    Me.FTProductBarcodeNo.Focus()
                    '    Me.FTProductBarcodeNo.SelectAll()
                    '    Exit Sub
                    'End If

                    If _FTProductBarcodeNo <> "" Then

                        Me.FTProductBarcodeNo.Focus()
                        Me.FTProductBarcodeNo.SelectAll()
                        'Me._LoadDataScan()

                        If Check_Barcode(HI.UL.ULF.rpQuoted(_FTProductBarcodeNo), _FTOrderNo, _FTColorWay, _FTSizeBreakDown, _FTSubOrderNo, _CartonQty) Then
                            'Dim _getScanQty As Double = GetScanQty(_FTOrderNo, _FTSubOrderNo, _FTColorWay, _FTSizeBreakDown, _FTProductBarcodeNo, 0)
                            'Dim _GetCartonQty As Double = GetCartonQty(_FTOrderNo, _FTSubOrderNo, _FTColorWay, _FTSizeBreakDown, _FTProductBarcodeNo, 0)


                            Dim _getScanQty As Double = 0
                            Dim _GetCartonQty As Double = 0


                            CheckCartonScanQty(_FTOrderNo, _FTSubOrderNo, _FTColorWay, _FTSizeBreakDown, _FTProductBarcodeNo, 0, _getScanQty, _GetCartonQty)
                            If _TotalCarton >= _PFNCartonNo Then
                                If _getScanQty < _GetCartonQty Then
                                    ' If CheckScanCloseCarton(Me.FTPackNo.Text, _PFNCartonNo) Then Exit Sub
                                    If Save_Scan(_FTOrderNo, _FTSubOrderNo, _FTColorWay, _FTSizeBreakDown, _FTProductBarcodeNo, Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString), 1) Then
                                        Me._LoadDataScan()
                                        Me.FTProductBarcodeNo.Focus()
                                        Me.FTProductBarcodeNo.SelectAll()
                                    Else
                                        Me.FTProductBarcodeNo.Focus()
                                        Me.FTProductBarcodeNo.SelectAll()
                                    End If

                                Else
                                    Me._LoadDataScan()
                                    'modify line start 20170711
                                    'If _TotalCarton > _PFNCartonNo Then
                                    '    Dim _Qry As String = ""
                                    '    Dim _TmpFNCartonNo As Integer = -1

                                    '    _Qry = " SELECT TOP 1 FNCartonNo "
                                    '    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
                                    '    _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                                    '    _Qry &= vbCrLf & " AND FNCartonNo>" & Integer.Parse(Val(_PFNCartonNo)) & " "
                                    '    _Qry &= vbCrLf & " ORDER BY FNCartonNo ASC "

                                    '    _TmpFNCartonNo = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, -1))

                                    '    If Me.ockmanual.Checked = False Then
                                    '        If _TmpFNCartonNo > 0 Then
                                    '            _PFNCartonNo = _TmpFNCartonNo
                                    '        End If
                                    '    End If


                                    'Else

                                    '    If _getScanQty < _GetCartonQty Then

                                    '        ' If CheckScanCloseCarton(Me.FTPackNo.Text, _PFNCartonNo) Then Exit Sub
                                    '        If Save_Scan(_FTOrderNo, _FTSubOrderNo, _FTColorWay, _FTSizeBreakDown, _FTProductBarcodeNo, 0, 1) Then
                                    '            Me._LoadDataScan()
                                    '            Me.FTProductBarcodeNo.Focus()
                                    '            Me.FTProductBarcodeNo.SelectAll()
                                    '        Else
                                    '            Me.FTProductBarcodeNo.Focus()
                                    '            Me.FTProductBarcodeNo.SelectAll()
                                    '        End If
                                    '    Else
                                    '        HI.MG.ShowMsg.mInfo("Over Scan Quantity !!!! ", 1411210001, Me.Text, "", MessageBoxIcon.Stop)
                                    '    End If

                                    'End If

                                    '' SetNewCarton()
                                    ''otlpack_BeforeFocusNode(otlpack, New BeforeFocusNodeEventArgs)
                                    ''SetNewCarton()
                                    'If _getScanQty < _GetCartonQty Then

                                    '    ' If CheckScanCloseCarton(Me.FTPackNo.Text, _PFNCartonNo) Then Exit Sub

                                    '    If Save_Scan(_FTOrderNo, _FTSubOrderNo, _FTColorWay, _FTSizeBreakDown, _FTProductBarcodeNo, 0, 1) Then
                                    '        Me._LoadDataScan()
                                    '        Me.FTProductBarcodeNo.Focus()
                                    '        Me.FTProductBarcodeNo.SelectAll()
                                    '    Else
                                    '        Me.FTProductBarcodeNo.Focus()
                                    '        Me.FTProductBarcodeNo.SelectAll()
                                    '    End If
                                    'End If
                                    'modify line end 20170711

                                    HI.MG.ShowMsg.mInfo("Over Scan Quantity !!!! ", 1411210001, Me.Text, "", MessageBoxIcon.Warning)
                                    Me.FTProductBarcodeNo.Focus()
                                    Me.FTProductBarcodeNo.SelectAll()
                                End If
                                ' Me.SetNewCarton()
                            End If


                        Else

                            HI.MG.ShowMsg.mInfo("ไม่มีข้อมูลการบรรจุกล่อง ", 1411210002, Me.Text, "", MessageBoxIcon.Warning)

                            '  Me.SetNewCarton()
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        End If

                    End If
                Else
                    ' Me.SetNewCarton()

                    HI.MG.ShowMsg.mInfo("Over Scan Barcode....   " & _FTProductBarcodeNo, 1412040001, Me.Text, "", MessageBoxIcon.Stop)
                End If

            Else
                'Del

                ' _FTProductBarcodeNo = CheckBarcodeCust(FTProductBarcodeNo.Text, True, Integer.Parse(Val(_PFNCartonNo)))
                If Check_Barcode(HI.UL.ULF.rpQuoted(_FTProductBarcodeNo), _FTOrderNo, _FTColorWay, _FTSizeBreakDown, _FTSubOrderNo, _CartonQty) Then

                    'If CheckScanCloseCarton(Me.FTPackNo.Text, _PFNCartonNo) Then Exit Sub

                    If DeleteBarcode(Me.FTPackNo.Text, _PFNCartonNo, _PFTOrderNo, _PFTSubOrderNo, _PFTColorway, _PFTSizeBreakDown, Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString),
                                                                      _FTProductBarcodeNo) = False Then
                        'If _PFNCartonNo > 0 Then
                        '    _PFNCartonNo = _PFNCartonNo - 1
                        '    SetNewCarton()
                        'End If

                    End If

                End If

                '  Me.SetNewCarton()
                Me._LoadDataScan()
                Me.FTProductBarcodeNo.Focus()
                Me.FTProductBarcodeNo.SelectAll()

            End If

            Me.FTProductBarcodeNo.Focus()
            Me.FTProductBarcodeNo.SelectAll()
        Else
            Call ProcessBarcodeSet(Me.FTPackNo.Text, FTProductBarcodeNo.Text)
        End If

    End Sub

    Private Function checkTracksuit(packno As String, barcodesetno As String) As Boolean
        Try
            Dim cmdstring As String = ""
            Dim dtbarset As DataTable

            cmdstring = "select  FTPackNo, FTBarcodeSetNo, FTColorway, FTSizeBreakDown, FNQuantity,0 AS FNPass"
            cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet AS X WITH(NOLOCK)"
            cmdstring &= vbCrLf & " WHERE FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
            cmdstring &= vbCrLf & " AND FTPackNo ='" & HI.UL.ULF.rpQuoted(packno) & "'"
            dtbarset = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)
            If dtbarset.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckBarcodeSet(packno As String, barcodesetno As String, ByRef dtbarset As DataTable) As Boolean

        Dim cmdstring As String = ""





        cmdstring = "select  FTPackNo, FTBarcodeSetNo, FTColorway, FTSizeBreakDown, FNQuantity,0 AS FNPass"
        cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet AS X WITH(NOLOCK)"
        cmdstring &= vbCrLf & " WHERE FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
        cmdstring &= vbCrLf & " AND FTPackNo ='" & HI.UL.ULF.rpQuoted(packno) & "'"
        dtbarset = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

        If dtbarset.Rows.Count > 0 Then
            Return True
        Else




            HI.MG.ShowMsg.mInfo("ข้อมูล Barcode Set ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1707091427, Me.Text,, MessageBoxIcon.Warning)
            FTProductBarcodeNo.Focus()
            FTProductBarcodeNo.SelectAll()
            Return False
        End If
    End Function

    Private Sub ProcessBarcodeSet(packno As String, barcodesetno As String)
        Dim cmdstring As String = ""
        Dim dtbarset As New DataTable
        If (CheckBarcodeSet(packno, barcodesetno, dtbarset)) Then

            If FTStateDeleteBarcode.Checked = False Then

                Dim dtpack As DataTable
                Dim checkmatching As Boolean = True

                cmdstring = "SELECT  A.FTPackNo, A.FNCartonNo, A.FTBarCodeCarton, A.FTBarCodeEAN13, B.FTColorway, B.FTSizeBreakDown, B.FNQuantity,B.FTOrderNo, B.FTSubOrderNo, "
                cmdstring &= vbCrLf & "  C.FNScanQuantity,B.FNQuantity-  ISNULL(C.FNScanQuantity,0) AS FNBal "
                cmdstring &= vbCrLf & "  From     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A INNER JOIN "
                cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As B WITH(NOLOCK) On A.FTPackNo = B.FTPackNo And A.FNCartonNo = B.FNCartonNo LEFT OUTER JOIN "
                cmdstring &= vbCrLf & "	( "
                cmdstring &= vbCrLf & "	 Select FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown,SUM(FNScanQuantity) As FNScanQuantity "
                cmdstring &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS C  WITH(NOLOCK) "
                cmdstring &= vbCrLf & "  WHERE  C.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND C.FNCartonNo =" & Val(FNCartonNo.Value) & " "
                cmdstring &= vbCrLf & "	 GROUP BY FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown "
                cmdstring &= vbCrLf & " ) AS C "
                cmdstring &= vbCrLf & "  On B.FTPackNo = C.FTPackNo And B.FNCartonNo = C.FNCartonNo And B.FTOrderNo = C.FTOrderNo And B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway=C.FTColorway AND B.FTSizeBreakDown=C.FTSizeBreakDown "
                cmdstring &= vbCrLf & "  WHERE  A.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND A.FNCartonNo =" & Val(FNCartonNo.Value) & " "

                dtpack = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)


                For Each R As DataRow In dtbarset.Rows

                    If dtpack.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'").Length > 0 Then
                        If dtpack.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=" & Val(R!FNQuantity.ToString) & "").Length > 0 Then

                        Else
                            HI.MG.ShowMsg.mInfo("ข้อมูล จำนวน Barcode Set มากกว่าจำนวนตัวในกล่องที่สามารถทำการ Pack ได้ กรุณาทำการตรวจสอบ !!!", 1707091479, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                            FTCartonBarcodeNo.Focus()
                            FTCartonBarcodeNo.SelectAll()
                            checkmatching = False
                            Exit For

                        End If
                    Else

                        HI.MG.ShowMsg.mInfo("ข้อมูล  Barcode Set ไม่ Match กับรายละเอียดการ Pack ของกล่อง กรุณาทำการตรวจสอบ !!!", 1707091480, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                        FTCartonBarcodeNo.Focus()
                        FTCartonBarcodeNo.SelectAll()
                        checkmatching = False
                        Exit For

                    End If

                Next

                If checkmatching Then
                    Dim dtbarcodeoutline As DataTable
                    cmdstring = "SELECT FTBarcodeNo,FTOrderNo,FTSubOrderNo,FTColorway,FTSizeBreakDown,FNQuantity,FTTimeRef,FNPackQty,FNQuantity-FNPackQty AS FNBal,0 AS FNQtyCreatePack "
                    cmdstring &= vbCrLf & " FROM (Select A.FTBarcodeNo, A.FTOrderNo, A.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, SUM(A.FNQuantity) As FNQuantity, MAX(A.FDDate + '-'+ A.FTTime) As FTTimeRef, ISNULL "
                    cmdstring &= vbCrLf & "    ((SELECT SUM(FNScanQuantity) AS Expr1 "
                    cmdstring &= vbCrLf & "   FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail As X "
                    cmdstring &= vbCrLf & "  WHERE   (FTBarcodeNo = A.FTBarcodeNo)), 0) AS FNPackQty "
                    cmdstring &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As A With (NOLOCK) INNER JOIN "
                    cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN "
                    cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As CT WITH (NOLOCK) On A.FTOrderNo = CT.FTOrderNo And A.FTSubOrderNo = CT.FTSubOrderNo And B.FTColorway = CT.FTColorway And B.FTSizeBreakDown = CT.FTSizeBreakDown "
                    cmdstring &= vbCrLf & "  WHERE  CT.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND CT.FNCartonNo =" & Val(FNCartonNo.Value) & " "
                    cmdstring &= vbCrLf & " GROUP BY A.FTBarcodeNo, A.FTOrderNo, A.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                    cmdstring &= vbCrLf & " ) AS X "
                    cmdstring &= vbCrLf & " WHERE FNQuantity-FNPackQty> 0 "

                    dtbarcodeoutline = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                    Dim SetBDQty As Integer = 0
                    For Each R As DataRow In dtbarset.Rows
                        SetBDQty = Val(R!FNQuantity.ToString)

                        If dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'").Length > 0 Then
                            If dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=" & SetBDQty & "").Length > 0 Then


                                For Each Rxd As DataRow In dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=" & SetBDQty & "")
                                    Rxd!FNQtyCreatePack = Val(R!FNQuantity.ToString)
                                    Exit For
                                Next

                            Else

                                Dim checkQty As Integer = SetBDQty
                                Dim StateLoop As Boolean = True
                                Do While checkQty > 0 And StateLoop


                                    For Each Rxd As DataRow In dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=1 AND FNQtyCreatePack=0")

                                        If Val(Rxd!FNBal) < checkQty Then
                                            Rxd!FNQtyCreatePack = Val(Rxd!FNBal)

                                            checkQty = checkQty - Val(Rxd!FNBal)
                                        Else
                                            Rxd!FNQtyCreatePack = checkQty

                                            checkQty = 0
                                        End If

                                        Exit For
                                    Next


                                    If checkQty > 0 Then
                                        StateLoop = (dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=1 AND FNQtyCreatePack=0").Length > 0)
                                    End If
                                Loop


                                If checkQty > 0 Then
                                    R!FNPass = 1
                                    HI.MG.ShowMsg.mInfo("ข้อมูล Scan ออกท้ายไลน์ ไม่พอสำหรับการ Pack กรุณาทำการตรวจสอบ !!!", 1707091482, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                                    FTProductBarcodeNo.Focus()
                                    FTProductBarcodeNo.SelectAll()

                                    checkmatching = False
                                    Exit For
                                End If


                            End If
                        Else
                            R!FNPass = 1
                            HI.MG.ShowMsg.mInfo("ข้อมูล Scan ออกท้ายไลน์ ไม่พอสำหรับการ Pack กรุณาทำการตรวจสอบ !!!", 1707091482, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                            FTProductBarcodeNo.Focus()
                            FTProductBarcodeNo.SelectAll()

                            checkmatching = False
                            Exit For

                        End If

                    Next

                    If checkmatching And dtbarset.Select("FNPass=1").Length <= 0 Then

                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        Try

                            For Each R As DataRow In dtbarcodeoutline.Select("FNQtyCreatePack>0")

                                cmdstring = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                                cmdstring &= vbCrLf & " SET FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                cmdstring &= vbCrLf & ",FNScanQuantity=FNScanQuantity+" & Val(R!FNQtyCreatePack.ToString)
                                cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                cmdstring &= vbCrLf & "and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                                cmdstring &= vbCrLf & "and FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                cmdstring &= vbCrLf & "and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                cmdstring &= vbCrLf & "and FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                cmdstring &= vbCrLf & "and FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"

                                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    cmdstring = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                                    cmdstring &= "(FTInsUser, FDInsDate, FTInsTime,   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId,FTBarcodeNo, FNScanQuantity)"
                                    cmdstring &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                    cmdstring &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                    cmdstring &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                                    cmdstring &= vbCrLf & "," & Val(R!FNQtyCreatePack.ToString)

                                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Exit Sub
                                    End If

                                End If

                                cmdstring = " Declare @Time varchar(5) , @Date varchar(10)"
                                cmdstring &= vbCrLf & " SELECT @Time =Convert(varchar(5),Getdate(),114) , @Date =" & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                                cmdstring &= vbCrLf & " Set FNScanQuantity =FNScanQuantity+" & Val(R!FNQtyCreatePack.ToString)
                                cmdstring &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                                cmdstring &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                cmdstring &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                                cmdstring &= vbCrLf & " AND FDScanDate=@Date"
                                cmdstring &= vbCrLf & " AND FDScanTime=@Time"
                                cmdstring &= vbCrLf & " IF @@ROWCOUNT = 0"
                                cmdstring &= vbCrLf & "  BEGIN"
                                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail (FTInsUser, FDInsDate, FTInsTime,"
                                cmdstring &= "  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,  FDScanDate, FDScanTime, FNScanQuantity,FTBarcodeSetNo)"
                                cmdstring &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                cmdstring &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                cmdstring &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                                cmdstring &= vbCrLf & ",@Date"
                                cmdstring &= vbCrLf & ",@Time"
                                cmdstring &= vbCrLf & "," & Val(R!FNQtyCreatePack.ToString)
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                                cmdstring &= vbCrLf & "  END"

                                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Exit Sub
                                End If



                            Next
                            'For Each R As DataRow In dtbarset.Rows


                            '    If dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=" & Val(R!FNQuantity.ToString) & "").Length > 0 Then

                            '        For Each Rx As DataRow In dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=" & Val(R!FNQuantity.ToString) & "", "FTTimeRef")

                            '            cmdstring = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                            '            cmdstring &= vbCrLf & " SET FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '            cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            '            cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            '            cmdstring &= vbCrLf & ",FNScanQuantity=FNScanQuantity+" & Val(R!FNQuantity.ToString)
                            '            cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                            '            cmdstring &= vbCrLf & "and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                            '            cmdstring &= vbCrLf & "and FTOrderNo='" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Rx!FTSubOrderNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "'"
                            '            cmdstring &= vbCrLf & "and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Rx!FTSizeBreakDown.ToString) & "'"
                            '            cmdstring &= vbCrLf & "and FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                            '            cmdstring &= vbCrLf & "and FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"


                            '            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            '                cmdstring = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                            '                cmdstring &= "(FTInsUser, FDInsDate, FTInsTime,   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId,FTBarcodeNo, FNScanQuantity)"
                            '                cmdstring &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            '                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            '                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                            '                cmdstring &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                            '                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                            '                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTSubOrderNo.ToString) & "'"
                            '                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "'"
                            '                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTSizeBreakDown.ToString) & "'"
                            '                cmdstring &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                            '                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                            '                cmdstring &= vbCrLf & "," & Val(R!FNQuantity.ToString)

                            '                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            '                    HI.Conn.SQLConn.Tran.Rollback()
                            '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            '                    Exit Sub
                            '                End If

                            '            End If

                            '            cmdstring = " Declare @Time varchar(5) , @Date varchar(10)"
                            '            cmdstring &= vbCrLf & " SELECT @Time =Convert(varchar(5),Getdate(),114) , @Date =" & HI.UL.ULDate.FormatDateDB
                            '            cmdstring &= vbCrLf & " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                            '            cmdstring &= vbCrLf & " Set FNScanQuantity =FNScanQuantity+" & Val(R!FNQuantity.ToString)
                            '            cmdstring &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '            cmdstring &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            '            cmdstring &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            '            cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                            '            cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                            '            cmdstring &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Rx!FTSubOrderNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "'"
                            '            cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Rx!FTSizeBreakDown.ToString) & "'"
                            '            cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                            '            cmdstring &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & " AND FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                            '            cmdstring &= vbCrLf & " AND FDScanDate=@Date"
                            '            cmdstring &= vbCrLf & " AND FDScanTime=@Time"
                            '            cmdstring &= vbCrLf & " IF @@ROWCOUNT = 0"
                            '            cmdstring &= vbCrLf & "  BEGIN"
                            '            cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail (FTInsUser, FDInsDate, FTInsTime,"
                            '            cmdstring &= "  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,  FDScanDate, FDScanTime, FNScanQuantity,FTBarcodeSetNo)"
                            '            cmdstring &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            '            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                            '            cmdstring &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTSubOrderNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTColorway.ToString) & "'"
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTSizeBreakDown.ToString) & "'"
                            '            cmdstring &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeNo.ToString) & "'"
                            '            cmdstring &= vbCrLf & ",@Date"
                            '            cmdstring &= vbCrLf & ",@Time"
                            '            cmdstring &= vbCrLf & "," & Val(R!FNQuantity.ToString)
                            '            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                            '            cmdstring &= vbCrLf & "  END"

                            '            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            '                HI.Conn.SQLConn.Tran.Rollback()
                            '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            '                Exit Sub
                            '            End If

                            '            Exit For
                            '        Next

                            '    End If

                            'Next

                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Me._LoadDataScan()
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()

                        Catch ex As Exception
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Exit Sub

                        End Try

                    End If

                    dtbarcodeoutline.Dispose()
                End If

                dtpack.Dispose()
            Else
                Dim dtdelate As New DataTable
                Dim dtdataCheckdelate As New DataTable
                Dim StateCheckDelete As Boolean = True
                Dim SetBDQty As Integer = 0
                Dim RowIdx As Integer = 0
                Dim _dtpack As New DataTable
                cmdstring = "SELECT  A.FTPackNo, A.FNCartonNo, A.FTBarCodeCarton, A.FTBarCodeEAN13, B.FTColorway, B.FTSizeBreakDown, B.FNQuantity,B.FTOrderNo, B.FTSubOrderNo, "
                cmdstring &= vbCrLf & "  C.FNScanQuantity,B.FNQuantity-  ISNULL(C.FNScanQuantity,0) AS FNBal "
                cmdstring &= vbCrLf & "  From     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A INNER JOIN "
                cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As B WITH(NOLOCK) On A.FTPackNo = B.FTPackNo And A.FNCartonNo = B.FNCartonNo LEFT OUTER JOIN "
                cmdstring &= vbCrLf & "	( "
                cmdstring &= vbCrLf & "	 Select FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown,SUM(FNScanQuantity) As FNScanQuantity "
                cmdstring &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS C  WITH(NOLOCK) "
                cmdstring &= vbCrLf & "  WHERE  C.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND C.FNCartonNo =" & Val(FNCartonNo.Value) & " "
                cmdstring &= vbCrLf & "	 GROUP BY FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown "
                cmdstring &= vbCrLf & " ) AS C "
                cmdstring &= vbCrLf & "  On B.FTPackNo = C.FTPackNo And B.FNCartonNo = C.FNCartonNo And B.FTOrderNo = C.FTOrderNo And B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway=C.FTColorway AND B.FTSizeBreakDown=C.FTSizeBreakDown "
                cmdstring &= vbCrLf & "  WHERE  A.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND A.FNCartonNo =" & Val(FNCartonNo.Value) & " "

                _dtpack = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)



                For Each R As DataRow In dtbarset.Rows
                    SetBDQty = Val(R!FNQuantity.ToString)
                    RowIdx = RowIdx + 1

                    cmdstring = " SELECT FTColorway,FTSizeBreakDown,FNHSysUnitSectId,FDScanDate,FDScanTime,FNScanQuantity, 0 AS FNDelete "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail  "
                    cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                    cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                    cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                    cmdstring &= vbCrLf & " and FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                    cmdstring &= vbCrLf & " ORDER BY FDScanDate, FDScanTime "


                    dtdataCheckdelate = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                    Dim checkQty As Integer = SetBDQty
                    Dim StateLoop As Boolean = True
                    Do While checkQty > 0 And StateLoop


                        For Each Rxd As DataRow In dtdataCheckdelate.Select("FNScanQuantity>0  AND FNDelete=0", "FDScanDate,FDScanTime")

                            If Val(Rxd!FNScanQuantity) < checkQty Then
                                Rxd!FNDelete = Val(Rxd!FNScanQuantity)

                                checkQty = checkQty - Val(Rxd!FNScanQuantity)
                            Else
                                Rxd!FNDelete = checkQty

                                checkQty = 0
                            End If

                            Exit For
                        Next

                        If checkQty > 0 Then
                            StateLoop = (dtdataCheckdelate.Select("FNScanQuantity>0  AND FNDelete=0", "FDScanDate,FDScanTime").Length > 0)
                        End If

                    Loop


                    If checkQty > 0 Then
                        R!FNPass = 1

                        StateCheckDelete = False
                        Exit For
                    End If

                    If RowIdx = 1 Then
                        dtdelate = dtdataCheckdelate.Copy
                    Else
                        dtdelate.Merge(dtdataCheckdelate.Copy)
                    End If
                Next

                If StateCheckDelete = False Then
                    dtdelate.Dispose()
                    dtdataCheckdelate.Dispose()
                    Exit Sub
                End If

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    For Each R As DataRow In dtdelate.Select("FNDelete>0")


                        cmdstring = " UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail Set FNScanQuantity=FNScanQuantity-" & Val(R!FNDelete.ToString) & " "
                        cmdstring &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                        cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                        cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                        cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                        cmdstring &= vbCrLf & " and FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                        cmdstring &= vbCrLf & " and FNScanQuantity =" & Val(R!FNScanQuantity.ToString) & ""
                        cmdstring &= vbCrLf & " and  FDScanDate='" & HI.UL.ULF.rpQuoted(R!FDScanDate.ToString) & "' "
                        cmdstring &= vbCrLf & " and FDScanTime='" & HI.UL.ULF.rpQuoted(R!FDScanTime.ToString) & "'"


                        If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Exit Sub
                        End If

                    Next

                    cmdstring = " UPDATE  A "
                    cmdstring &= vbCrLf & " SET FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & ",FNScanQuantity= ISNULL(( "
                    cmdstring &= vbCrLf & " SELECT SUM(FNScanQuantity) AS FNScanQuantity "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS X"
                    cmdstring &= vbCrLf & " WHERE X.FTPackNo=A.FTPackNo AND X.FNCartonNo =A.FNCartonNo AND X.FTBarcodeNo=A.FTBarcodeNo "
                    cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                    cmdstring &= vbCrLf & " ),0) "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A"
                    cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & " and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Exit Sub
                    End If


                    cmdstring = " DELETE  A "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A"
                    cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & " and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & " and FNScanQuantity <=0"
                    HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    cmdstring = " DELETE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail  "
                    cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & "       AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & "       AND FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                    cmdstring &= vbCrLf & "       AND FNScanQuantity<=0"
                    HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Me._LoadDataScan()
                    Me.FTProductBarcodeNo.Focus()
                    Me.FTProductBarcodeNo.SelectAll()

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub

                End Try


            End If

        End If
        dtbarset.Dispose()
    End Sub


    Private Function CheckBarcodeSetTrack(packno As String, barcodesetno As String, ByRef dtbarset As DataTable) As Boolean

        Dim cmdstring As String = ""





        cmdstring = "select  FTPackNo, FTBarcodeSetNo, FTColorway, FTSizeBreakDown, FNQuantity,0 AS FNPass"
        cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_BarcodeSet AS X WITH(NOLOCK)"
        cmdstring &= vbCrLf & " WHERE FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
        cmdstring &= vbCrLf & " AND FTPackNo ='" & HI.UL.ULF.rpQuoted(packno) & "'"
        dtbarset = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

        If dtbarset.Rows.Count > 0 Then
            Return True
        Else
            If Me.FNOrderPackType.SelectedIndex = 1 Then
                cmdstring = "select top 2  '" & HI.UL.ULF.rpQuoted(packno) & "' FTPackNo, FTCustBarcodeNo FTBarcodeSetNo, x.FTColorway, x. FTSizeBreakDown,1 FNQuantity,0 AS FNPass    , x.FTSubOrderNo"
                cmdstring &= vbCrLf & " from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS X WITH(NOLOCK)"
                cmdstring &= vbCrLf & "  left join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "] .dbo.TPACKOrderPack_Carton_Detail AS C  WITH(NOLOCK)    "
                cmdstring &= vbCrLf & "  on     x.FTOrderNo = c.FTOrderNo  and x.FTColorway  = c.FTColorway and x.FTSizeBreakDown = c.FTSizeBreakDown and x.FTSubOrderNo = c.FTSubOrderNo "
                cmdstring &= vbCrLf & " where x.FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                cmdstring &= vbCrLf & "	and   C.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND C.FNCartonNo =" & Val(FNCartonNo.Value) & "   "
                cmdstring &= vbCrLf & "	   "

                dtbarset = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                Return True
            End If



            HI.MG.ShowMsg.mInfo("ข้อมูล Barcode Set ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1707091427, Me.Text,, MessageBoxIcon.Warning)
            FTProductBarcodeNo.Focus()
            FTProductBarcodeNo.SelectAll()
            Return False
        End If
    End Function

    Private Sub ProcessBarcodeSetTracksuit(packno As String, barcodesetno As String)
        Dim cmdstring As String = ""
        Dim dtbarset As New DataTable
        If (CheckBarcodeSetTrack(packno, barcodesetno, dtbarset)) Then

            If FTStateDeleteBarcode.Checked = False Then

                Dim dtpack As DataTable
                Dim checkmatching As Boolean = True

                cmdstring = "SELECT  A.FTPackNo, A.FNCartonNo, A.FTBarCodeCarton, A.FTBarCodeEAN13, B.FTColorway, B.FTSizeBreakDown, B.FNQuantity,B.FTOrderNo, B.FTSubOrderNo, "
                cmdstring &= vbCrLf & "  C.FNScanQuantity,B.FNQuantity-  ISNULL(C.FNScanQuantity,0) AS FNBal "
                cmdstring &= vbCrLf & "  From     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A INNER JOIN "
                cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As B WITH(NOLOCK) On A.FTPackNo = B.FTPackNo And A.FNCartonNo = B.FNCartonNo LEFT OUTER JOIN "
                cmdstring &= vbCrLf & "	( "
                cmdstring &= vbCrLf & "	 Select FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown,SUM(FNScanQuantity) As FNScanQuantity "
                cmdstring &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS C  WITH(NOLOCK) "
                cmdstring &= vbCrLf & "  WHERE  C.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND C.FNCartonNo =" & Val(FNCartonNo.Value) & " "
                cmdstring &= vbCrLf & "	 GROUP BY FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown "
                cmdstring &= vbCrLf & " ) AS C "
                cmdstring &= vbCrLf & "  On B.FTPackNo = C.FTPackNo And B.FNCartonNo = C.FNCartonNo And B.FTOrderNo = C.FTOrderNo And B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway=C.FTColorway AND B.FTSizeBreakDown=C.FTSizeBreakDown "
                cmdstring &= vbCrLf & "  WHERE  A.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND A.FNCartonNo =" & Val(FNCartonNo.Value) & " "

                dtpack = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)


                For Each R As DataRow In dtbarset.Rows

                    If dtpack.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' ").Length > 0 Then
                        If dtpack.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND FNBal>=" & Val(R!FNQuantity.ToString) & "").Length > 0 Then

                        Else
                            HI.MG.ShowMsg.mInfo("ข้อมูล จำนวน Barcode Set มากกว่าจำนวนตัวในกล่องที่สามารถทำการ Pack ได้ กรุณาทำการตรวจสอบ !!!", 1707091479, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                            FTCartonBarcodeNo.Focus()
                            FTCartonBarcodeNo.SelectAll()
                            checkmatching = False
                            Exit For

                        End If
                    Else

                        HI.MG.ShowMsg.mInfo("ข้อมูล  Barcode Set ไม่ Match กับรายละเอียดการ Pack ของกล่อง กรุณาทำการตรวจสอบ !!!", 1707091480, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                        FTCartonBarcodeNo.Focus()
                        FTCartonBarcodeNo.SelectAll()
                        checkmatching = False
                        Exit For

                    End If

                Next

                If checkmatching Then
                    Dim dtbarcodeoutline As DataTable
                    cmdstring = "SELECT FTBarcodeNo,FTOrderNo,FTSubOrderNo,FTColorway,FTSizeBreakDown,FNQuantity,FTTimeRef,FNPackQty,FNQuantity-FNPackQty AS FNBal,0 AS FNQtyCreatePack "
                    cmdstring &= vbCrLf & " FROM (Select A.FTBarcodeNo, A.FTOrderNo, A.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, SUM(A.FNQuantity) As FNQuantity, MAX(A.FDDate + '-'+ A.FTTime) As FTTimeRef, ISNULL "
                    cmdstring &= vbCrLf & "    ((SELECT SUM(FNScanQuantity) AS Expr1 "
                    cmdstring &= vbCrLf & "   FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail As X "
                    cmdstring &= vbCrLf & "  WHERE   (FTBarcodeNo = A.FTBarcodeNo)), 0) AS FNPackQty "
                    cmdstring &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As A With (NOLOCK) INNER JOIN "
                    cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN "
                    cmdstring &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As CT WITH (NOLOCK) On A.FTOrderNo = CT.FTOrderNo And A.FTSubOrderNo = CT.FTSubOrderNo And B.FTColorway = CT.FTColorway And B.FTSizeBreakDown = CT.FTSizeBreakDown "
                    cmdstring &= vbCrLf & "  WHERE  CT.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND CT.FNCartonNo =" & Val(FNCartonNo.Value) & " "
                    cmdstring &= vbCrLf & " GROUP BY A.FTBarcodeNo, A.FTOrderNo, A.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown"
                    cmdstring &= vbCrLf & " ) AS X "
                    cmdstring &= vbCrLf & " WHERE FNQuantity-FNPackQty> 0 "

                    dtbarcodeoutline = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                    Dim SetBDQty As Integer = 0
                    For Each R As DataRow In dtbarset.Rows
                        SetBDQty = Val(R!FNQuantity.ToString)

                        If dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' ").Length > 0 Then
                            If dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND FNBal>=" & SetBDQty & "").Length > 0 Then


                                For Each Rxd As DataRow In dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND FNBal>=" & SetBDQty & "")
                                    Rxd!FNQtyCreatePack = Val(R!FNQuantity.ToString)
                                    Exit For
                                Next

                            Else

                                Dim checkQty As Integer = SetBDQty
                                Dim StateLoop As Boolean = True
                                Do While checkQty > 0 And StateLoop


                                    For Each Rxd As DataRow In dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=1 AND FNQtyCreatePack=0")

                                        If Val(Rxd!FNBal) < checkQty Then
                                            Rxd!FNQtyCreatePack = Val(Rxd!FNBal)

                                            checkQty = checkQty - Val(Rxd!FNBal)
                                        Else
                                            Rxd!FNQtyCreatePack = checkQty

                                            checkQty = 0
                                        End If

                                        Exit For
                                    Next


                                    If checkQty > 0 Then
                                        StateLoop = (dtbarcodeoutline.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FNBal>=1 AND FNQtyCreatePack=0").Length > 0)
                                    End If
                                Loop


                                If checkQty > 0 Then
                                    R!FNPass = 1
                                    HI.MG.ShowMsg.mInfo("ข้อมูล Scan ออกท้ายไลน์ ไม่พอสำหรับการ Pack กรุณาทำการตรวจสอบ !!!", 1707091482, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                                    FTProductBarcodeNo.Focus()
                                    FTProductBarcodeNo.SelectAll()

                                    checkmatching = False
                                    Exit For
                                End If


                            End If
                        Else
                            R!FNPass = 1
                            HI.MG.ShowMsg.mInfo("ข้อมูล Scan ออกท้ายไลน์ ไม่พอสำหรับการ Pack กรุณาทำการตรวจสอบ !!!", 1707091482, Me.Text, " Colorway : " & R!FTColorway.ToString & " AND Size : " & R!FTSizeBreakDown.ToString & "", MessageBoxIcon.Warning)
                            FTProductBarcodeNo.Focus()
                            FTProductBarcodeNo.SelectAll()

                            checkmatching = False
                            Exit For

                        End If

                    Next

                    If checkmatching And dtbarset.Select("FNPass=1").Length <= 0 Then

                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        Try

                            For Each R As DataRow In dtbarcodeoutline.Select("FNQtyCreatePack>0")

                                cmdstring = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                                cmdstring &= vbCrLf & " SET FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                cmdstring &= vbCrLf & ",FNScanQuantity=FNScanQuantity+" & Val(R!FNQtyCreatePack.ToString)
                                cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                cmdstring &= vbCrLf & "and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                                cmdstring &= vbCrLf & "and FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                cmdstring &= vbCrLf & "and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                cmdstring &= vbCrLf & "and FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                cmdstring &= vbCrLf & "and FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"

                                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    cmdstring = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                                    cmdstring &= "(FTInsUser, FDInsDate, FTInsTime,   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId,FTBarcodeNo, FNScanQuantity)"
                                    cmdstring &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                    cmdstring &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                    cmdstring &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                                    cmdstring &= vbCrLf & "," & Val(R!FNQtyCreatePack.ToString)

                                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Exit Sub
                                    End If

                                End If

                                cmdstring = " Declare @Time varchar(5) , @Date varchar(10)"
                                cmdstring &= vbCrLf & " SELECT @Time =Convert(varchar(5),Getdate(),114) , @Date =" & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                                cmdstring &= vbCrLf & " Set FNScanQuantity =FNScanQuantity+" & Val(R!FNQtyCreatePack.ToString)
                                cmdstring &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                                cmdstring &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                cmdstring &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                                cmdstring &= vbCrLf & " AND FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                                cmdstring &= vbCrLf & " AND FDScanDate=@Date"
                                cmdstring &= vbCrLf & " AND FDScanTime=@Time"
                                cmdstring &= vbCrLf & " IF @@ROWCOUNT = 0"
                                cmdstring &= vbCrLf & "  BEGIN"
                                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail (FTInsUser, FDInsDate, FTInsTime,"
                                cmdstring &= "  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,  FDScanDate, FDScanTime, FNScanQuantity,FTBarcodeSetNo)"
                                cmdstring &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                                cmdstring &= vbCrLf & "," & CInt("0" & _PFNCartonNo)
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                                cmdstring &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "'"
                                cmdstring &= vbCrLf & ",@Date"
                                cmdstring &= vbCrLf & ",@Time"
                                cmdstring &= vbCrLf & "," & Val(R!FNQtyCreatePack.ToString)
                                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                                cmdstring &= vbCrLf & "  END"

                                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Exit Sub
                                End If



                            Next


                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Me._LoadDataScan()
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()

                        Catch ex As Exception
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Exit Sub

                        End Try

                    End If

                    dtbarcodeoutline.Dispose()
                End If

                dtpack.Dispose()
            Else
                Dim dtdelate As New DataTable
                Dim dtdataCheckdelate As New DataTable
                Dim StateCheckDelete As Boolean = True
                Dim SetBDQty As Integer = 0
                Dim RowIdx As Integer = 0
                Dim _dtpack As New DataTable
                cmdstring = "SELECT  A.FTPackNo, A.FNCartonNo, A.FTBarCodeCarton, A.FTBarCodeEAN13, B.FTColorway, B.FTSizeBreakDown, B.FNQuantity,B.FTOrderNo, B.FTSubOrderNo, "
                cmdstring &= vbCrLf & "  C.FNScanQuantity,B.FNQuantity-  ISNULL(C.FNScanQuantity,0) AS FNBal "
                cmdstring &= vbCrLf & "  From     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A INNER JOIN "
                cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As B WITH(NOLOCK) On A.FTPackNo = B.FTPackNo And A.FNCartonNo = B.FNCartonNo LEFT OUTER JOIN "
                cmdstring &= vbCrLf & "	( "
                cmdstring &= vbCrLf & "	 Select FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown,SUM(FNScanQuantity) As FNScanQuantity "
                cmdstring &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS C  WITH(NOLOCK) "
                cmdstring &= vbCrLf & "  WHERE  C.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND C.FNCartonNo =" & Val(FNCartonNo.Value) & " "
                cmdstring &= vbCrLf & "	 GROUP BY FTPackNo,FNCartonNo,FTOrderNo,FTSubOrderNo,FTColorway, FTSizeBreakDown "
                cmdstring &= vbCrLf & " ) AS C "
                cmdstring &= vbCrLf & "  On B.FTPackNo = C.FTPackNo And B.FNCartonNo = C.FNCartonNo And B.FTOrderNo = C.FTOrderNo And B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway=C.FTColorway AND B.FTSizeBreakDown=C.FTSizeBreakDown "
                cmdstring &= vbCrLf & "  WHERE  A.FTPackNo ='" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "' AND A.FNCartonNo =" & Val(FNCartonNo.Value) & " "

                _dtpack = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)



                For Each R As DataRow In dtbarset.Rows
                    SetBDQty = Val(R!FNQuantity.ToString)
                    RowIdx = RowIdx + 1

                    cmdstring = " SELECT FTColorway,FTSizeBreakDown,FNHSysUnitSectId,FDScanDate,FDScanTime,FNScanQuantity, 0 AS FNDelete , FTSubOrderNo  "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail  "
                    cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                    cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                    cmdstring &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                    cmdstring &= vbCrLf & " and FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                    cmdstring &= vbCrLf & " ORDER BY FDScanDate, FDScanTime "


                    dtdataCheckdelate = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

                    Dim checkQty As Integer = SetBDQty
                    Dim StateLoop As Boolean = True
                    Do While checkQty > 0 And StateLoop


                        For Each Rxd As DataRow In dtdataCheckdelate.Select("FNScanQuantity>0  AND FNDelete=0", "FDScanDate,FDScanTime")

                            If Val(Rxd!FNScanQuantity) < checkQty Then
                                Rxd!FNDelete = Val(Rxd!FNScanQuantity)

                                checkQty = checkQty - Val(Rxd!FNScanQuantity)
                            Else
                                Rxd!FNDelete = checkQty

                                checkQty = 0
                            End If

                            Exit For
                        Next

                        If checkQty > 0 Then
                            StateLoop = (dtdataCheckdelate.Select("FNScanQuantity>0  AND FNDelete=0", "FDScanDate,FDScanTime").Length > 0)
                        End If

                    Loop


                    If checkQty > 0 Then
                        R!FNPass = 1

                        StateCheckDelete = False
                        Exit For
                    End If

                    If RowIdx = 1 Then
                        dtdelate = dtdataCheckdelate.Copy
                    Else
                        dtdelate.Merge(dtdataCheckdelate.Copy)
                    End If
                Next

                If StateCheckDelete = False Then
                    dtdelate.Dispose()
                    dtdataCheckdelate.Dispose()
                    Exit Sub
                End If

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    For Each R As DataRow In dtdelate.Select("FNDelete>0")


                        cmdstring = " UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail Set FNScanQuantity=FNScanQuantity-" & Val(R!FNDelete.ToString) & " "
                        cmdstring &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                        cmdstring &= vbCrLf & " AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                        cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                        cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                        cmdstring &= vbCrLf & " and FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                        cmdstring &= vbCrLf & " and FNScanQuantity =" & Val(R!FNScanQuantity.ToString) & ""
                        cmdstring &= vbCrLf & " and  FDScanDate='" & HI.UL.ULF.rpQuoted(R!FDScanDate.ToString) & "' "
                        cmdstring &= vbCrLf & " and FDScanTime='" & HI.UL.ULF.rpQuoted(R!FDScanTime.ToString) & "'"
                        cmdstring &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"


                        If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Exit Sub
                        End If

                    Next

                    cmdstring = " UPDATE  A "
                    cmdstring &= vbCrLf & " SET FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    cmdstring &= vbCrLf & ",FNScanQuantity= ISNULL(( "
                    cmdstring &= vbCrLf & " SELECT SUM(FNScanQuantity) AS FNScanQuantity "
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail AS X"
                    cmdstring &= vbCrLf & " WHERE X.FTPackNo=A.FTPackNo AND X.FNCartonNo =A.FNCartonNo AND X.FTBarcodeNo=A.FTBarcodeNo and X.FTSubOrderNo = A.FTSubOrderNo "
                    cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)
                    cmdstring &= vbCrLf & " ),0) "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A"
                    cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & " and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse("0" & Me.FNHSysUnitSectId.Properties.Tag.ToString)

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Exit Sub
                    End If


                    cmdstring = " DELETE  A "
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A"
                    cmdstring &= vbCrLf & " WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & " and FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & " and FNScanQuantity <=0"
                    HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    cmdstring = " DELETE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail  "
                    cmdstring &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    cmdstring &= vbCrLf & "       AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
                    cmdstring &= vbCrLf & "       AND FTBarcodeSetNo='" & HI.UL.ULF.rpQuoted(barcodesetno) & "'"
                    cmdstring &= vbCrLf & "       AND FNScanQuantity<=0"
                    HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Me._LoadDataScan()
                    Me.FTProductBarcodeNo.Focus()
                    Me.FTProductBarcodeNo.SelectAll()

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub

                End Try


            End If

        End If
        dtbarset.Dispose()
    End Sub



    Private Function CheckScanCloseCarton(_FTPackNo As String, _PFNCartonNo As Integer) As Boolean
        Dim _Qry As String = ""

        _Qry = "Select TOP 1 FTPackNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton As P With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_FTPackNo) & "'"
        _Qry &= vbCrLf & "  AND FNCartonNo=" & Integer.Parse(Val(_PFNCartonNo)) & ""

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
            HI.MG.ShowMsg.mInfo("พบข้อมูล การ Scan ปิด กล่อง แล้ว ไม่สามารถทำการลบหรือแก้ไขข้อมูลของกล่องนี้ได้ !!!", 15110204587, Me.Text, , MessageBoxIcon.Warning)
            Return True
        Else
            Return False
        End If

    End Function

    Private Function GetScanQty(ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String, _
                               ByVal _SizeBreakDown As String, ByVal _BarcodeNo As String, ByVal _UnitSectId As Integer) As Integer
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select  Sum(FNScanQuantity) AS FNScanQuantity   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan  WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & "and FNCartonNo=" & CInt("0" & _PFNCartonNo)
            _Cmd &= vbCrLf & "and FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Cmd &= vbCrLf & "and FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & "and FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & "and FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            ' _Cmd &= vbCrLf & "and FNHSysUnitSectId=" & CInt(_UnitSectId)
            '_Cmd &= vbCrLf & "and FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            ' _Cmd &= vbCrLf & "Group By   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown " ',FTBarcodeNo

            Return CInt(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetCartonQty(ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String,
                               ByVal _SizeBreakDown As String, ByVal _BarcodeNo As String, ByVal _UnitSectId As Integer) As Double
        Try
            Dim _Cmd As String = ""

            _Cmd = "Select TOP 1 FNQuantity  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
            _Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Cmd &= vbCrLf & "AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & "AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & "AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"

            Return CInt("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

        Catch ex As Exception
            Return 0
        End Try
    End Function


    Private Sub CheckCartonScanQty(ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String,
                               ByVal _SizeBreakDown As String, ByVal _BarcodeNo As String, ByVal _UnitSectId As Integer, ByRef _getScanQty As Integer, ByRef _GetCartonQty As Integer)
        Try
            Dim _Cmd As String = ""

            '_Cmd = "Select TOP 1 FNQuantity  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            '_Cmd &= vbCrLf & "AND FNCartonNo=" & CInt("0" & _PFNCartonNo)
            '_Cmd &= vbCrLf & "AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            '_Cmd &= vbCrLf & "AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            '_Cmd &= vbCrLf & "AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            '_Cmd &= vbCrLf & "AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"

            Dim dt As DataTable
            _Cmd = "   Select  FNQuantity AS FNPackQuantity"
            _Cmd &= vbCrLf & " ,ISNULL(("
            _Cmd &= vbCrLf & " Select  Sum(FNScanQuantity) As FNScanQuantity "
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As X WITH(NOLOCK)"
            _Cmd &= vbCrLf & " Where X.FTPackNo = A.FTPackNo"
            _Cmd &= vbCrLf & "  And X.FNCartonNo = A.FNCartonNo"
            _Cmd &= vbCrLf & "  And X.FTOrderNo = A.FTOrderNo"
            _Cmd &= vbCrLf & "  And X.FTSubOrderNo = A.FTSubOrderNo"
            _Cmd &= vbCrLf & " And X.FTColorway = A.FTColorway"
            _Cmd &= vbCrLf & "  And X.FTSizeBreakDown = A.FTSizeBreakDown"
            _Cmd &= vbCrLf & " ),0) AS FNScanQuantity"

            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As A WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & "AND A.FNCartonNo=" & CInt("0" & _PFNCartonNo)
            _Cmd &= vbCrLf & "AND A.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Cmd &= vbCrLf & "AND A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & "AND A.FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & "AND A.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"

            dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In dt.Rows
                _GetCartonQty = Val(R!FNPackQuantity.ToString)
                _getScanQty = Val(R!FNScanQuantity.ToString)
            Next

            dt.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckQtyCarton(ByVal _BarcodeNo As String, ByVal _CartonNo As Integer, ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String, _
                                ByVal _SizeBreakDown As String) As Boolean
        Try
            Dim _oDt, _oDt2 As DataTable
            Dim _Cmd As String = ""

            If _CartonNo = 0 Then
                _CartonNo = _CartonNo + 1
            End If

            _Cmd = "Select Isnull(C.FNQuantity, 0) As FNQuantity , Sum(Isnull(S.FNScanQuantity,0)) As FNScanQuantity ,C.FNCartonNo "
            _Cmd &= vbCrLf & ",dbo.Fn_GetUnitSect_Scan(C.FTPackNo, C.FTColorway"
            _Cmd &= ",C.FTOrderNo,C.FTSubOrderNo,C.FTSizeBreakDown"
            _Cmd &= ",C.FNCartonNo) As FNHSysUnitSectId "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As C With(NOLOCK) "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As S With(NOLOCK)  "
            _Cmd &= vbCrLf & "On C.FTPackNo = S.FTPackNo And C.FTColorway = S.FTColorway And C.FTOrderNo = S.FTOrderNo "
            _Cmd &= vbCrLf & "And C.FTSubOrderNo = S.FTSubOrderNo And C.FTSizeBreakDown = S.FTSizeBreakDown  And C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE C.FTPackNo + '|' + C.FTColorway  + '|' + C.FTSizeBreakDown  + '|' + C.FTOrderNo  + '|' +  C.FTSubOrderNo IN ("
            _Cmd &= vbCrLf & " SELECT C.FTPackNo + '|' + C.FTColorway  + '|' + C.FTSizeBreakDown  + '|' + C.FTOrderNo  + '|' +  C.FTSubOrderNo"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS C WITH(NOLOCK)"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S WITH(NOLOCK) "
            _Cmd &= vbCrLf & "ON C.FTPackNo = S.FTPackNo and C.FTColorway = S.FTColorway and C.FTOrderNo = S.FTOrderNo "
            _Cmd &= vbCrLf & "and C.FTSubOrderNo = S.FTSubOrderNo and C.FTSizeBreakDown = S.FTSizeBreakDown  and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & "where C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & "and C.FTColorway = '" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & "and C.FTOrderNo = '" & HI.UL.ULF.rpQuoted(_PFTOrderNo) & "'"
            ' _Cmd &= vbCrLf & "and C.FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "'"
            _Cmd &= vbCrLf & "and C.FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Cmd &= vbCrLf & " )  "
            _Cmd &= vbCrLf & " GROUP BY Isnull(C.FNQuantity,0),C.FNCartonNo , C.FTPackNo,C.FTColorway ,C.FTOrderNo,C.FTSubOrderNo,C.FTSizeBreakDown "
            _Cmd &= vbCrLf & " ORDER BY C.FNCartonNo ASC "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            If _oDt.Rows.Count > 0 Then
                For Each R As DataRow In _oDt.Select("FNQuantity>FNScanQuantity AND FNCartonNo=" & _PFNCartonNo & " ", "FNCartonNo")
                    If CInt(R!FNScanQuantity) > 0 Then
                        If CInt(R!FNQuantity) > CInt(R!FNScanQuantity) Then
                            If Me.ockmanual.Checked = False Then
                                _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                                SetNewCarton()
                            End If
                            Return True
                            Exit For
                        End If
                    End If
                Next

                For Each R As DataRow In _oDt.Select("FNQuantity>FNScanQuantity AND FNCartonNo=" & _PFNCartonNo & "", "FNCartonNo")
                    Return True
                    Exit For
                Next

                For Each R As DataRow In _oDt.Select("FNQuantity>FNScanQuantity AND FNCartonNo<>" & _PFNCartonNo & " AND FNCartonNo > " & _PFNCartonNo & "   ", "FNCartonNo")
                    If CInt(R!FNScanQuantity) > 0 Then
                        If CInt(R!FNQuantity) > CInt(R!FNScanQuantity) Then
                            If Me.ockmanual.Checked = False Then
                                _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                                SetNewCarton()
                            End If
                            Return True
                            Exit For
                        End If
                    End If
                Next

                For Each R As DataRow In _oDt.Select("FNQuantity>FNScanQuantity AND FNCartonNo<>" & _PFNCartonNo & " AND FNCartonNo < " & _PFNCartonNo & "   ", "FNCartonNo")
                    If CInt(R!FNScanQuantity) > 0 Then
                        If CInt(R!FNQuantity) > CInt(R!FNScanQuantity) Then
                            If Me.ockmanual.Checked = False Then
                                _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                                SetNewCarton()
                            End If
                            Return True
                            Exit For
                        End If
                    End If
                Next

                'ถ้ายังไม่มีการสแกน หรือยอดสแกนเป็น 0 และ ไลน์เท่ากับค่าว่าง
                For Each R As DataRow In _oDt.Select("FNScanQuantity=0 and FNHSysUnitSectId='' ", "FNCartonNo")
                    If Me.ockmanual.Checked = False Then
                        _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                        SetNewCarton()
                    End If
                    Return True
                    Exit For
                Next

                For Each R As DataRow In _oDt.Select("FNQuantity>FNScanQuantity AND FNCartonNo<>" & _PFNCartonNo & "  and  FNHSysUnitSectId ='' ", "FNCartonNo")
                    'If CInt(R!FNScanQuantity) > 0 Then
                    If CInt(R!FNQuantity) > CInt(R!FNScanQuantity) Then
                        If Me.ockmanual.Checked = False Then
                            _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                            SetNewCarton()
                        End If
                        Return True
                        Exit For
                    End If
                    'End If
                Next

                For Each R As DataRow In _oDt.Select("FNQuantity>0 and FNScanQuantity <= 0 AND FNCartonNo<>" & _PFNCartonNo & " ", "FNCartonNo")
                    If CInt(R!FNQuantity) > CInt(R!FNScanQuantity) Then
                        If Me.ockmanual.Checked = False Then
                            _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                            SetNewCarton()
                        End If
                        Return True
                        Exit For
                    End If
                Next
                Return False
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub _LoadDataScan()
        Try
            Dim _colcount As Integer
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyScan As Integer = 0

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_ScanQty '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "','" & CInt("0" & _PFNCartonNo) & "','" & HI.UL.ULF.rpQuoted(_PFTOrderNo) & "'"
            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "','" & CInt("0") & "','" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "','" & IIf(_PFTAssortState = True, HI.UL.ULF.rpQuoted(_PAFTColorway), HI.UL.ULF.rpQuoted(_PFTColorway)) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            'For Each R As DataRow In _oDt.Rows
            '    _QtyScan += +R!Total.ToString
            'Next
            'lblQtyScan.Text = _QtyScan
            'End If
            Try
                _QtyScan = Val(_oDt.Compute("sum(Total)", "Total>0").ToString)
                'For Each R As DataRow In _oDt.Rows
                '    _QtyScan += +R!Total.ToString
                'Next

                lblQtyScan.Text = _QtyScan
                If Me.FNOrderPackType.SelectedIndex = 1 Then
                    Me.lblQtyScanSet.Text = (Val(_QtyScan) / Me.FNPackSetValue.Value).ToString
                End If
            Catch ex As Exception
                lblQtyScan.Text = 0
                Me.lblQtyScanSet.Text = 0
            End Try
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

                    'For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    '    With GridCol
                    '        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    '    End With
                    'Next
                End If
                .OptionsView.ShowAutoFilterRow = False
            End With
            Me.ogcScan.DataSource = _oDt.Copy
            _LoadDataScanSet()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub _LoadDataScanSet()
        Try
            Dim _colcount As Integer
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyScan As Integer = 0

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_ScanQtySet '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "','" & CInt("0" & _PFNCartonNo) & "','" & HI.UL.ULF.rpQuoted(_PFTOrderNo) & "'"
            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "','" & CInt("0") & "','" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "','" & IIf(_PFTAssortState = True, HI.UL.ULF.rpQuoted(_PAFTColorway), HI.UL.ULF.rpQuoted(_PFTColorway)) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _oDt.Rows
                _QtyScan += +R!Total.ToString
            Next
            'lblQtyScan.Text = _QtyScan
            'End If

            With Me.ogvScanset
                .OptionsView.ShowAutoFilterRow = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FTSubOrderNo".ToUpper
                            .Columns(I).Visible = False
                        Case "FTOrderNo".ToUpper, "FTNikePOLineItem".ToUpper, "FTColorway".ToUpper
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

                    'For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    '    With GridCol
                    '        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    '    End With
                    'Next
                End If
                .OptionsView.ShowAutoFilterRow = False
            End With
            Me.ogcScanset.DataSource = _oDt.Copy
        Catch ex As Exception
        End Try
    End Sub



    Private Function DeleteBarcode(ByVal _PackNo As String, ByVal _CartonNo As Integer, ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String, ByVal _SizeBreakDown As String _
                                   , ByVal _UnitSectId As Integer, ByVal _BarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _ScanQty As Integer = 0
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Select top 1  FNScanQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
            _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _ScanQty = CInt(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            If _ScanQty <= 0 Then

                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Else

                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
                _Cmd &= vbCrLf & "SET FNScanQuantity=FNScanQuantity-1"
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                _Cmd = "Select top 1  FNScanQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _ScanQty = CInt(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

                If _ScanQty <= 0 Then

                    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                    _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                    _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                    _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                    _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            End If

            ''New
            _Cmd = "Select top 1  FNScanQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
            _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _Cmd &= vbCrLf & " and  FDScanDate +'|'+FDScanTime in("
            _Cmd &= vbCrLf & "Select top 1 FDScanDate+'|'+FDScanTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
            _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
            _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
            _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
            _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc)"
            _ScanQty = CInt(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            If _ScanQty <= 0 Then

                _Cmd = "Delete  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                '_Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc"
                _Cmd &= vbCrLf & " and  FDScanDate +'|'+FDScanTime in("
                _Cmd &= vbCrLf & "Select top 1 FDScanDate+'|'+FDScanTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc)"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Else

                _Cmd = "UPDATE     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail"
                _Cmd &= vbCrLf & "SET FNScanQuantity=FNScanQuantity-1"
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _Cmd &= vbCrLf & " and  FDScanDate +'|'+FDScanTime in("
                _Cmd &= vbCrLf & "Select top 1 FDScanDate+'|'+FDScanTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc)"

                '_Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc"
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                _Cmd = "Select top 1  FNScanQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _Cmd &= vbCrLf & " and  FDScanDate +'|'+FDScanTime in("
                _Cmd &= vbCrLf & "Select top 1 FDScanDate+'|'+FDScanTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                _Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc)"
                _ScanQty = CInt(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

                If _ScanQty <= 0 Then

                    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                    _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                    _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                    _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                    _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                    _Cmd &= vbCrLf & " and  FDScanDate +'|'+FDScanTime in("
                    _Cmd &= vbCrLf & "Select top 1 FDScanDate+'|'+FDScanTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)
                    _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                    _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                    _Cmd &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                    _Cmd &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                    _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & CInt(_UnitSectId)
                    _Cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
                    _Cmd &= vbCrLf & "Order by FDScanDate Desc , FDScanTime Desc)"


                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            End If
            ''New
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GetTotalCarton() As Integer
        Try
            Dim _Cmd As String = ""

            '_Cmd = "SELECT  *"
            '_Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "WHERE     (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            'Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count


            _Cmd = "SELECT  Max(FNCartonNo) AS TotalCarton "
            _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE     (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            Return Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, 0)))

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub SetNewCarton()
        Try
            'otlpack.FocusedNode = otlpack.Nodes(0)
            _PSCartonNo = _PFNCartonNo
            Call CreateTreeCarton()
            'Me.otlpack.ExpandAll()
            ' otlpack.SetFocusedNode(otlpack.GetNodeByVisibleIndex(Integer.Parse(Val(_PFNCartonNo))))

            otlpack.SetFocusedNode(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse(Val(_PFNCartonNo))).ToString))

            With otlpack.FocusedNode

                Dim _FNCartonNo As String = .GetValue(1).ToString
                Dim _FNQuantity As String = .GetValue(2).ToString
                Dim _FNNetWeight As String = .GetValue(3).ToString
                Dim _FNHSysCartonId As String = .GetValue(4).ToString
                Dim _FTCartonCode As String = .GetValue(5).ToString
                Dim _FNWeight As String = .GetValue(6).ToString
                Dim _FNPackCartonSubType As String = .GetValue(7).ToString
                Dim _FNPackPerCarton As String = .GetValue(8).ToString
                Dim _FNScanQty As String = .GetValue(9).ToString

                _PFNCartonNo = _FNCartonNo

                FNHSysCartonId.Text = _FTCartonCode
                FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                FNNW.Value = Val(_FNWeight)
                FNCartonNo.Text = _FNCartonNo

                Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))

                Call _LoadDataScan()

            End With

            ' otlpack_Click(otlpack, New EventArgs)

        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckBarcodeBundle(ByVal _BarCode As String) As Boolean
        Try
            Dim _Cmd As String = ""

            Dim _QtyBarCodeBundle, _QtyScan As Double

            '_Cmd = "select SUM(FNScanQuantity) as FNScanQuantity    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
            '_QtyScan = CDbl(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            '_Cmd = "Select  sum(FNQuantity) as  FNQuantity  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "where FTBarcodeNo  = '" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
            '_QtyBarCodeBundle = CDbl(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))

            'Return (_QtyBarCodeBundle > _QtyScan)

            _Cmd = "  Select  FNQuantity - FNQuantityScan "
            _Cmd &= vbCrLf & "  FROM(SELECT A.FTBarcodeNo, SUM(FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "	,ISNULL(("
            _Cmd &= vbCrLf & "	Select  SUM(FNScanQuantity)"
            _Cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As X With(NOLOCK)"
            _Cmd &= vbCrLf & "	Where X.FTBarcodeNo = A.FTBarcodeNo"
            _Cmd &= vbCrLf & "	),0) As FNQuantityScan"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As A WITH(NOLOCK)"
            _Cmd &= vbCrLf & "Where (FTBarcodeNo = N'" & HI.UL.ULF.rpQuoted(_BarCode) & "')"
            _Cmd &= vbCrLf & "Group By A.FTBarcodeNo"
            _Cmd &= vbCrLf & " ) As X "

            _QtyBarCodeBundle = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))
            Return (_QtyBarCodeBundle > 0)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function CheckBarCodeScan(ByVal _BarCode As String, ByVal _UnitSect As Integer) As Boolean
        Try
            Dim _Cmd As String = ""


            _Cmd = "Select Top 1 FTBarcodeNo "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail With(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectId=" & _UnitSect

            Return (HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0)

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FuncToScanPack_Click(sender As Object, e As EventArgs) Handles ocmtoqaprocess.Click
        Me.CheckExe()
    End Sub

    Private Sub CheckExe()
        Try

            Dim p() As System.Diagnostics.Process
            p = System.Diagnostics.Process.GetProcessesByName("QAProcess")
            If p.Count > 0 Then
                p = System.Diagnostics.Process.GetProcessesByName("QAProcess")
                Dim L As String = p(0).MainWindowTitle
                AppActivate(L)
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Function GetPackNoByCarton(ByVal BarcodeKey As String, ByRef _CartonNo As String) As String
        Try
            Dim _FTProductBarcodeNo As String = BarcodeKey
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _dtPackNo As DataTable

            Dim _FTOrderNo As String = ""
            Dim _FTColorway As String = ""
            Dim _FTSizeBreakDown As String = ""
            Dim _oDt As DataTable
            Dim _UnitSectId As Integer = 0
            Dim _PackNo As String = ""
            _FTPackOfOrderNo = ""
            Dim _Cmd As String = ""

            If Me.FTPackNo.Text = "" Then
                _Cmd = "SELECT TOP 1 P.FTPackNo  "
                _Cmd &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS P  WITH(NOLOCK)  INNER JOIN"
                _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B  WITH(NOLOCK)  ON P.FTColorway = B.FTColorway AND P.FTSizeBreakDown = B.FTSizeBreakDown AND P.FNPackPerCarton = B.FNQuantity INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C WITH(NOLOCK) ON P.FTOrderNo = C.FTOrderNo AND B.FTOrderProdNo = C.FTOrderProdNo"
                _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS Z WITH(NOLOCK) ON P.FTPackNo = Z.FTPackNo and P.FNCartonNo = Z.FNCartonNo "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail AS SD WITH(NOLOCK) ON P.FTPackNo = SD.FTPackNo and P.FNCartonNo = SD.FNCartonNo  "
                _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS BD WITH (NOLOCK) ON P.FTSizeBreakDown = BD.FTSizeBreakDown AND P.FTColorway = BD.FTColorway AND P.FTSubOrderNo = BD.FTSubOrderNo "
                _Cmd &= vbCrLf & " AND P.FTOrderNo = BD.FTOrderNo AND B.FTPOLineItemNo = BD.FTNikePOLineItem  "
                _Cmd &= vbCrLf & " WHERE (B.FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "')   and Isnull(Z.FTState,'') <> '1' and Isnull(SD.FTPackNo,'') = '' "
                _Cmd &= vbCrLf & "GROUP BY P.FTPackNo "

                _PackNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")

            End If

            Return _PackNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub FTCartonBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTCartonBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                _PSCartonNo = 0 'set carton update close.

                FTCartonBarcodeNo.Properties.Tag = FTCartonBarcodeNo.Text.Trim()
                Me.ockmanual.Checked = True

                'If Me.FTPackNo.Text = "" Then
                Dim _barcode As String = Me.FTCartonBarcodeNo.Text
                Me.FTPackNo.Text = ""
                Me.FTPackNo.Text = GetPackNoByCarton(Me.FTCartonBarcodeNo.Text, "")
                Me.FTCartonBarcodeNo.Text = _barcode
                'End If

                Dim _CartonNo As Integer = -1
                Dim _Qry As String = ""
                Dim _Cmd As String = ""

                If Me.FTPackNo.Text <> "" Then

                    _Qry = " Select  TOP 1  FNCartonNo"
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE    (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "')"
                    _Qry &= vbCrLf & "  AND  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "') "

                    _CartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "-1")))

                Else
                    _Qry = " Select  TOP 1  FNCartonNo ,FTPackNo "
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE    (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "')"

                    For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD).Rows
                        _CartonNo = R!FNCartonNo.ToString
                        Me.FTPackNo.Text = R!FTPackNo.ToString
                    Next

                End If

                If _CartonNo > 0 Then

                    _PFNCartonNo = _CartonNo
                    Call SetNewCarton()
                    FTProductBarcodeNo.Focus()
                    FTProductBarcodeNo.SelectAll()

                Else

                    _Cmd = " SELECT Top 1 P.FNCartonNo"
                    _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                    _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                    _Cmd &= vbCrLf & " INNER JOIN"
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                    _Cmd &= vbCrLf & " B.FTBarcodeBundleNo = PD.FTBarcodeNo and P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                    _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"
                    _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                    _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "') --AND (PD.FTPackNo IS NULL) "
                    _Cmd &= vbCrLf & "and (Isnull(PD.FNScanQuantity,0) < P.FNPackPerCarton) "
                    _Cmd &= vbCrLf & "And (P.FNCartonNo  not in (SELECT FNCartonNo"
                    _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                    _Cmd &= vbCrLf & "WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')))"
                    _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "

                    _CartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "-1")))

                    If _CartonNo < 0 Then

                        _Cmd = " SELECT Top 1 P.FNCartonNo"
                        _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                        _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                        _Cmd &= vbCrLf & " LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                        _Cmd &= vbCrLf & "   P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"
                        _Cmd &= vbCrLf & "	 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS SB WITH(NOLOCK) ON B.FTPOLineItemNo = SB.FTNikePOLineItem"
                        _Cmd &= vbCrLf & " and B.FTColorway = SB.FTColorway and B.FTSizeBreakDown = SB.FTSizeBreakDown and P.FTOrderNo = SB.FTOrderNo  and P.FTSubOrderNo = SB.FTSubOrderNo"
                        _Cmd &= vbCrLf & "inner join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS PDO with(nolock)  ON   B.FTOrderProdNo = PDO.FTOrderProdNo  and P.FTSubOrderNo = PDO.FTSubOrderNo   and B.FTColorway = PDO.FTColorway And B.FTSizeBreakDown = PDO.FTSizeBreakDown  And P.FTOrderNo = PDO.FTOrderNo  "
                        _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                        _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "')  AND (PD.FTPackNo IS NULL) "
                        _Cmd &= vbCrLf & "and (Isnull(PD.FNScanQuantity,0) < P.FNPackPerCarton) "
                        _Cmd &= vbCrLf & "And (P.FNCartonNo  not in (SELECT FNCartonNo"
                        _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                        _Cmd &= vbCrLf & "WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')))"
                        _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "

                        _CartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "-1")))

                    End If

                    If _CartonNo > 0 Then

                        _PFNCartonNo = _CartonNo
                        Call SetNewCarton()
                        FTProductBarcodeNo.Focus()
                        FTProductBarcodeNo.SelectAll()

                    Else

                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode Carton นี้ในเอกสารใบ Pack นี้ กรุณาทำการตรวจสอบ ", 1505190023, Me.Text, , MessageBoxIcon.Warning)
                        Me.FTCartonBarcodeNo.Focus()
                        Me.FTCartonBarcodeNo.Text = ""
                        Me.FTCartonBarcodeNo.SelectAll()

                    End If
                End If

        End Select
    End Sub

    Private Sub otlpack_NodeCellStyle(sender As Object, e As GetCustomNodeCellStyleEventArgs) Handles otlpack.NodeCellStyle
        Try
            Select Case e.Column.FieldName.ToString
                Case "FTCartonName"
                    If e.Node.GetValue(10).ToString <> "" Then
                        e.Appearance.ForeColor = Color.Blue
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetPackNoCarton(_FTBarcodeNo As String) As String
        Try
            Dim _PackNo As String = ""
            Dim _Cmd As String = ""
            If FTPackNo.Text = "" Then
                _Cmd = "SELECT TOP 1 P.FTPackNo"
                _Cmd &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS P  WITH(NOLOCK)  INNER JOIN"
                _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B  WITH(NOLOCK)  ON P.FTColorway = B.FTColorway AND P.FTSizeBreakDown = B.FTSizeBreakDown AND P.FNPackPerCarton = B.FNQuantity INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C WITH(NOLOCK) ON P.FTOrderNo = C.FTOrderNo AND B.FTOrderProdNo = C.FTOrderProdNo"
                _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS Z WITH(NOLOCK) ON P.FTPackNo = Z.FTPackNo and P.FNCartonNo = Z.FNCartonNo "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail AS SD WITH(NOLOCK) ON P.FTPackNo = SD.FTPackNo and P.FNCartonNo = SD.FNCartonNo  "
                _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS BD WITH (NOLOCK) ON P.FTSizeBreakDown = BD.FTSizeBreakDown AND P.FTColorway = BD.FTColorway AND P.FTSubOrderNo = BD.FTSubOrderNo "
                _Cmd &= vbCrLf & " AND P.FTOrderNo = BD.FTOrderNo AND B.FTPOLineItemNo = BD.FTNikePOLineItem  "
                _Cmd &= vbCrLf & " WHERE (B.FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')   and Isnull(Z.FTState,'') <> '1' and Isnull(SD.FTPackNo,'') = '' "
                _Cmd &= vbCrLf & "GROUP BY P.FTPackNo "

                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    _PackNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")
                End If
            End If

            Return _PackNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function CheckBundle(BundleNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = Command(Table.TPRODTBundle)
            _Cmd &= vbCrLf & "Where FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(BundleNo) & "'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function Command(e As Integer) As String
        Try
            Return "Select Top 1 * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].." & _Table(e) & " WITH(NOLOCK) "
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub FTCartonBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTCartonBarcodeNo.EditValueChanged

    End Sub

    Private Sub FTProductBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTProductBarcodeNo.EditValueChanged

    End Sub
End Class