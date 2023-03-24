Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports System.IO

Public Class wScanCarton

    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _GenPackOrder As wGeneratePackOrder
    Private _GenPackCarton As wGenerateCarton
    Private _StateSubNew As Boolean = False
    Private _StateBarCodeCarton As Boolean = False

    Private _PFTOrderNo As String = ""
    Private _PFTSubOrderNo As String = ""
    Private _PFTColorway As String = ""
    Private _PFTSizeBreakDown As String = ""
    Private _StateScanAuto As Boolean = False
    Private _ScanPoup As wScanCartonPopUp
    Private _PDtPack As DataTable
    Private _ListClose As wListCarton
    Private _PDataNode As DataTable

    Private _PStateInsertAuto As Boolean = False

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _GenPackOrder = New wGeneratePackOrder
        HI.TL.HandlerControl.AddHandlerObj(_GenPackOrder)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenPackOrder.Name.ToString.Trim, _GenPackOrder)
        Catch ex As Exception
        Finally
        End Try

        _GenPackCarton = New wGenerateCarton
        HI.TL.HandlerControl.AddHandlerObj(_GenPackCarton)

        _ScanPoup = New wScanCartonPopUp
        HI.TL.HandlerControl.AddHandlerObj(_ScanPoup)

        _ListClose = New wListCarton
        HI.TL.HandlerControl.AddHandlerObj(_ListClose)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenPackCarton.Name.ToString.Trim, _GenPackCarton)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ScanPoup.Name.ToString.Trim, _ScanPoup)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListClose.Name.ToString.Trim, _ListClose)
        Catch ex As Exception
        Finally
        End Try

        Me.PrepareForm()
        Call InitGrid()
        Call TabChenge()
        Call Default_Grid_Scan()
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

            tPathImgDis = tW_SysPath & "\Pack\Barcode-icon.jpg"
            If IO.File.Exists(tPathImgDis) Then
                imagepackList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
            End If
        End If
    End Sub

    Private Sub Default_Grid_Scan()
        Try
            With ogvScan
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
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
        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub InitGrid()

        'With ogvsubpackdetail
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvpackdetail
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvpackdetailWeight
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvpweight
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvptotalweight
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvptotalpack
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        'With ogvpgrandpack
        '    .OptionsView.ShowAutoFilterRow = False
        '    .OptionsSelection.MultiSelect = False
        '    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        'End With

        With ogvppercarton
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub

    Private Sub ClearGrid()

        'With Me.ogvsubpackdetail
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next
        'End With

        'With Me.ogvpackdetail
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next
        'End With

        'With Me.ogvpackdetailWeight
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next
        'End With


        'ogcsubpackdetail.DataSource = Nothing
        'ogcpackdetail.DataSource = Nothing
        'ogcpackdetailWeight.DataSource = Nothing

        'Me.otbsuborder.TabPages.Clear()

    End Sub

    Private Sub LoadrderPackBreakDownCarton(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dtpack = _dt.Copy
        _PDtPack = _dtpack

        _PFTColorway = ""
        For Each R As DataRow In _dt.Select("Total > 0", "FTOrderNo")
            _PFTOrderNo = R!FTOrderNo.ToString
            _PFTSubOrderNo = R!FTSubOrderNo.ToString
            If _PFTColorway <> "" Then _PFTColorway &= ","
            _PFTColorway &= R!FTColorway.ToString
        Next

        Me.FTOrderNo.Text = _PFTOrderNo
        Call GetPOCuts()

        With Me.ogvppercarton
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
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
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
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
    End Sub

    Private Sub GetPOCuts()

        Try

            Dim _Cmd As String = ""

            _Cmd = "Select Top 1  FTCustomerPO "
            _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTPackNo='" & FTPackNo.Text.Trim() & "'"

            Me.FTPORef.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")

        Catch ex As Exception
        End Try

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
        Dim _Str As String = _Cmd & "  WHERE  FTPackNo='" & Key.ToString & "' "

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
        Call _LoadDataScan()
        Call SetStateScan()
        Call GetTotalCartonScan()
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
    End Sub
#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs)

        If Me.VerrifyData Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs)
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
        FTProductBarcodeNo.Text = ""
        lblQtyScan.Text = "000"
        lblQtyScanSet.Text = "000"
        olbtotalcartonscan_lbl.Text = "0 / 0"
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
            .OptionsView.ShowCheckBoxes = True
        End With

        Call InitNodeCarton(Me.otlpack, Nothing)
        Me.otlpack.ExpandAll()

    End Sub

    Private _oCheckbok As DevExpress.XtraEditors.CheckEdit
    Private _PMaxCarton As Integer = 0

    Private Sub InitNodeCarton(ByVal _Lst As DevExpress.XtraTreeList.TreeList, ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode)

        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Dim _nodeChild As DevExpress.XtraTreeList.Nodes.TreeListNode
        Try
            If (_Node Is Nothing) Then

                _Lst.OptionsView.ShowCheckBoxes = False
                node = _Lst.AppendNode(New Object() {Me.FNCartonNo3_lbl.Text & "", "-1", "", "", "", "", "", "", "", ""}, _Node)

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
                    _Qry &= vbCrLf & "   ,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton " ', '' AS StateChec

                    _Qry &= vbCrLf & " , ISNULL(("

                    _Qry &= vbCrLf & " SELECT TOP 1 FTBarCodeCarton"
                    _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS AX WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE AX.FTPackNo= A.FTPackNo"
                    _Qry &= vbCrLf & "  AND AX.FNCartonNo= A.FNCartonNo"
                    _Qry &= vbCrLf & " ),'') AS FTBarCodeCarton "

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
                    _PDataNode = dt
                    For Each R As DataRow In dt.Rows
                        _Lst.OptionsView.ShowCheckBoxes = True
                        nodeChild = _Lst.AppendNode(New Object() {Me.FNCartonNo2_lbl.Text & "" & R!FNCartonNo.ToString & " (" & R!FTCartonInfo.ToString & ")", R!FNCartonNo.ToString, R!FNQuantity.ToString, R!FNNetWeight.ToString, R!FNHSysCartonId.ToString, R!FTCartonCode.ToString, R!FNWeight.ToString, R!FNPackCartonSubType.ToString, R!FNPackPerCarton.ToString, R!FTBarCodeCarton.ToString}, node)
                        nodeChild.HasChildren = False
                        _PMaxCarton = CInt("0" & R!FNCartonNo.ToString)
                    Next
                Catch ex As Exception
                End Try
            Else
                node.HasChildren = False
            End If
        Catch
        End Try
    End Sub

    Private Function GetBarcode(_CartonNo As String) As String
        Try
            Dim _Cmd As String = "" : Dim _retrun As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT  top 1 FTBarCodeCarton FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode WITH(NOLOCK) WHERE  (FTPackNo = N'" & Me.FTPackNo.Text & "')"
            _Cmd &= vbCrLf & "and FNCartonNo =" & _PFNCartonNo
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0 Then
                _Cmd = "	SELECT  distinct   FTPackNo, FNCartonNo,  FTBarcodeNo "
                _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE        (FTPackNo = N'" & Me.FTPackNo.Text & "')"
                _Cmd &= vbCrLf & "and FNCartonNo =" & Integer.Parse("0" & _CartonNo)
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDt.Rows
                    If _retrun <> "" Then _retrun &= ","
                    _retrun &= "" & R!FTBarcodeNo.ToString
                Next
            End If
            Return _retrun
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub SetStateScan()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "  SELECT       C.FTPackNo, C.FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, "
            _Cmd &= vbCrLf & "      FNScanQuantity , Isnull(S.FTState,'0') as FTState"
            _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & "WHERE     (C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
            _Cmd &= vbCrLf & "ORDER BY  C.FNCartonNo ASC "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                otlpack.SetNodeCheckState(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse("0" & R!FNCartonNo.ToString)).ToString), IIf(R!FTState.ToString = "1", CheckState.Checked, CheckState.Unchecked))
            Next
        Catch ex As Exception
        End Try
    End Sub

#End Region

    Private Sub FNOrderPackType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOrderPackType.SelectedIndexChanged
        Try
            FNPackSetValue_lbl.Visible = (FNOrderPackType.SelectedIndex = 1)
            FNPackSetValue.Visible = (FNOrderPackType.SelectedIndex = 1)
            If (FNOrderPackType.SelectedIndex = 0) Then
                FNPackSetValue.Value = 0
                ogrpQtyScanSet.Visible = False
                '        Me.ogrpQtyScan.Size = New Size(450, 137)
                '        Me.ogrpQtyScan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                'Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)

            Else
                ogrpQtyScanSet.Visible = True
                'Me.ogrpQtyScan.Size = New Size(236, 137)
                'Me.ogrpQtyScan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles)

            End If



        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddsuborder_Click(sender As Object, e As EventArgs)
        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then

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
                    If FNHSysStyleId.Properties.ReadOnly = False Then
                        If Me.SaveData Then
                        Else
                            Exit Sub
                        End If
                    End If
                End If
            End If

            With _GenPackOrder
                .FNHSysStyleId = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
                .PackOrderNo = Me.FTPackNo.Text
                Call HI.ST.Lang.SP_SETxLanguage(_GenPackOrder)
                .ShowDialog()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
            FNHSysStyleId.SelectAll()
        End If
    End Sub

    Private Sub wCreatePackOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call InitGrid()
            Me.FTProductBarcodeNo.EnterMoveNextControl = False
            olbtotalcartonscan_lbl.Text = "0 / 0"
            _StateScanAuto = ChkScanAuto()
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

    Private _PFNCartonNo As String = ""
    Private Sub otlpack_BeforeCheckNode(sender As Object, e As CheckNodeEventArgs) Handles otlpack.BeforeCheckNode
        e.CanCheck = False
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
                            Dim _BarcodeNo As String = .GetValue(9).ToString

                            _PFNCartonNo = _FNCartonNo

                            FNHSysCartonId.Text = _FTCartonCode
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNNW.Value = Val(_FNWeight)
                            FNCartonNo.Text = _FNCartonNo


                            Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))
                            FTProductBarcodeNo.Text = _BarcodeNo
                            If Me.FTProductBarcodeNo.Text = "" And _BarcodeNo.ToString = "" Then
                                FTProductBarcodeNo.Text = GetBarcode(FNCartonNo.Text)
                            End If
                            Call _LoadDataScan()
                            'Call ChkSelectUnitSect()

                        End If
                    End With
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChkSelectUnitSect()
        Try
            Dim _Cmd As String = ""
            _Cmd = "   Select Top 1 S.FTPackNo   "
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S "
            _Cmd &= vbCrLf & " WHERE S.FTPackNo = '" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "'"
            _Cmd &= vbCrLf & " and S.FNCartonNo =" & CInt(_PFNCartonNo)
            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                Me.FNHSysUnitSectId.Properties.Buttons(0).Visible = True
                Me.FNHSysUnitSectId.Properties.ReadOnly = False
                Me.FNHSysUnitSectId.Enabled = True
                Me.FNHSysUnitSectId.Focus()
            Else
                Me.FNHSysUnitSectId.Properties.Buttons(1).Visible = False
                Me.FNHSysUnitSectId.Properties.ReadOnly = True
                Me.FNHSysUnitSectId.Enabled = Enabled
            End If
        Catch ex As Exception

        End Try
    End Sub

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

            _Cmd = " SELECT  Top 1     OC.FTOrderNo, OC.FTColorway, OC.FTSizeBreakDown, OC.FTCustBarcodeNo, OP.FTSubOrderNo, OP.FTOrderProdNo, OP.FNQuantity"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS OC WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS OP WITH (NOLOCK) ON OC.FTOrderNo = OP.FTOrderNo AND OC.FTColorway = OP.FTColorway AND OC.FTSizeBreakDown = OP.FTSizeBreakDown"
            _Cmd &= vbCrLf & "WHERE OC.FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(_Barcode) & "'"
            '_Cmd &= vbCrLf & "AND OC.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(_PFTSizeBreakDown) & "'"
            _Cmd &= vbCrLf & "AND OC.FTColorway='" & HI.UL.ULF.rpQuoted(_PFTColorway) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            If _oDt.Rows.Count <= 0 Then

                _Cmd = "  SELECT     B.FTBarcodeBundleNo, B.FTColorway, B.FTSizeBreakDown,  D.FTOrderNo, D.FTSubOrderNo, D.FTOrderProdNo, D.FNQuantity "
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH (NOLOCK) INNER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D WITH (NOLOCK) ON B.FTOrderProdNo = D.FTOrderProdNo "
                _Cmd &= vbCrLf & "           AND B.FTColorway = D.FTColorway AND B.FTSizeBreakDown = D.FTSizeBreakDown"
                _Cmd &= vbCrLf & " WHERE B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_Barcode) & "'"
                _Cmd &= vbCrLf & " AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(_PFTColorway) & "'"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            End If
            _Cmd = "Select Top 1 FTSizeBreakDown"
            _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "  WHERE FTPackNo='" & Me.FTPackNo.Text & "'"
            _Cmd &= vbCrLf & "  AND FNCartonNo=" & _PFNCartonNo
            _Cmd &= vbCrLf & " AND FTOrderNo='" & _PFTOrderNo & "'"
            _Cmd &= vbCrLf & " AND FTSubOrderNo='" & _PFTSubOrderNo & "'"
            _Cmd &= vbCrLf & " AND FTColorway='" & _PFTColorway & "'"
            _SizeBreakDown = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")

            For Each R As DataRow In _oDt.Rows
                _OrderNo = R!FTOrderNo.ToString
                _ColorWay = R!FTColorway.ToString
                '_SizeBreakDown = R!FTSizeBreakDown.ToString
                _SubOrderNo = R!FTSubOrderNo.ToString
                _CartonQty = CInt(R!FNQuantity)
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

    Private Function ChkOnhandScan(_Barcode As String, _OrderNo As String, _Colorway As String, _SizeBreakDown As String, ByRef dt As DataTable) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _State As Boolean = False
            _Cmd = "SELECT     B.FTPackNo, B.FNCartonNo, B.FTBarCodeCarton, D.FTOrderNo, D.FTSubOrderNo, D.FTColorway, D.FTSizeBreakDown, D.FNQuantity"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS D WITH (NOLOCK) ON B.FTPackNo = D.FTPackNo AND B.FNCartonNo = D.FNCartonNo"
            _Cmd &= vbCrLf & "Where B.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_Barcode) & "'"
            _Cmd &= vbCrLf & "and D.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _oDt.Rows
                _Cmd = "  SELECT       sum(B.FNQuantity) AS FNQuantity   "
                _Cmd &= vbCrLf & "FROM     (Select TT.FTBarcodeNo , TT.FNQuantity , O.FNHSysUnitSectId "
                _Cmd &= vbCrLf & "From ("
                _Cmd &= vbCrLf & "Select  sum(FNQuantity) AS FNQuantity , FTBarcodeNo"
                _Cmd &= vbCrLf & "From (SELECT  sum(O.FNQuantity) AS FNQuantity ,  O.FTBarcodeNo "
                _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline AS O WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS A WITH (NOLOCK) ON O.FTBarcodeNo = A.FTBarcodeBundleNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS P WITH (NOLOCK) ON A.FTOrderProdNo = P.FTOrderProdNo"
                _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and  A.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and   CASE WHEN ISNULL(A.FTColorwayNew,'') ='' THEN A.FTColorway ELSE ISNULL(A.FTColorwayNew,'') END='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "Group by  O.FTBarcodeNo  "
                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "Select  -sum(FNScanQuantity) AS FNQuantity , FTBarcodeNo"
                _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "Where  FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "group by FTBarcodeNo ) AS T "
                _Cmd &= vbCrLf & "group by FTBarcodeNo ) AS TT INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline AS O WITH(NOLOCK) ON TT.FTBarcodeNo = O.FTBarcodeNo"
                _Cmd &= vbCrLf & "Where TT.FNQuantity > 0 "
                _Cmd &= vbCrLf & "group by TT.FTBarcodeNo , TT.FNQuantity , O.FNHSysUnitSectId) AS B LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " (SELECT        H.FTBarcodeBundleNo,   CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END FTColorway, H.FTSizeBreakDown, P.FTOrderNo"
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS H WITH (NOLOCK) LEFT OUTER JOIN"
                ' _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle_Detail AS D WITH (NOLOCK) ON H.FTBarcodeBundleNo = D.FTBarcodeBundleNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS P WITH (NOLOCK) ON H.FTOrderProdNo = P.FTOrderProdNo"
                _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and H.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and  CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "  Group by    H.FTBarcodeBundleNo, CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END, H.FTSizeBreakDown, P.FTOrderNo"
                _Cmd &= vbCrLf & " ) AS D ON B.FTBarcodeNo = D.FTBarcodeBundleNo"
                _Cmd &= vbCrLf & "Where  D.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and D.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and D.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "group by D.FTColorway, D.FTSizeBreakDown , D.FTOrderNo "
                If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, 0)) >= Integer.Parse(R!FNQuantity.ToString) Then
                    _State = True
                Else
                    _State = False
                End If
                If Not (_State) Then
                    Exit For
                End If
            Next


            dt = _oDt
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkBarcodeCartonFromOrderPack(_Barcode As String, _OrderNo As String, _Colorway As String, _SizeBreakDown As String, _CartonNo As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable = Nothing : Dim _oDt2 As DataTable
            Dim _Qty As Integer = 0 : Dim _QtyBal As Integer = 0 : Dim _QtyPack As Integer = 0

            If Not (ChkOnhandScan(_Barcode, _OrderNo, _Colorway, _SizeBreakDown, _oDt)) Then
                HI.MG.ShowMsg.mInfo("ยอดสแกนออกไลน์ไม่พอสำหรับการแพ็คกล่องนี้...กรุณาตรวจสอบ!!!!", 1512091623, Me.Text)
                With _ScanPoup
                    .OrderNo = _OrderNo
                    .SubOrderNo = _PFTSubOrderNo
                    .PackNo = Me.FTPackNo.Text
                    '.UnitSectId = Me.FNHSysUnitSectId.Properties.Tag
                    .CartonNo = _CartonNo
                    .BarcodeNo = _Barcode
                    .Colorway = _Colorway
                    .PDtpercarton = _PDtPack
                    .ShowDialog()
                End With
                Return False
            End If

            _Cmd = "Select Top 1  Isnull(FTState,'0') AS FTState From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & " AND FNCartonNo=" & Integer.Parse(_CartonNo)
            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0") = "1" Then
                '     My.Computer.Audio.Play("Sound\wu-tang_clan-sound4-qbh.wav")
                HI.MG.ShowMsg.mInfo("กล่องนี้มีการสแกนไปแล้ว....", 1512101058, Me.Text)
                Return False
            End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "SELECT    B.FTBarcodeBundleNo  ,  B.FTPackNo, B.FNCartonNo, B.FTBarCodeCarton, D.FTOrderNo, D.FTSubOrderNo, D.FTColorway, D.FTSizeBreakDown, D.FNQuantity"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS D WITH (NOLOCK) ON B.FTPackNo = D.FTPackNo AND B.FNCartonNo = D.FNCartonNo"
            _Cmd &= vbCrLf & "  inner join HITECH_PRODUCTION..TPACKOrderPack P on B.FTPackNo = P.FTPackNo "
            _Cmd &= vbCrLf & "Where B.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_Barcode) & "'    AND B.FDInsDate >=Convert(varchar(10),DateAdd(month,-16,GetDate()),111)  "
            _Cmd &= vbCrLf & "   and p.FNHSysCmpId=" & HI.ST.SysInfo.CmpID


            '_Cmd &= vbCrLf & "and D.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'" 'multiscan ,create carton by ton 20200330

            _oDt = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Dim _PQty As Integer = 0 : Dim _StateCarton As Boolean = False
            For Each _row As DataRow In _PDataNode.Select("FNCartonNo=" & _PFNCartonNo)
                _StateCarton = (Integer.Parse("0" & _row!FNPackPerCarton.ToString) = Integer.Parse("0" & _row!FNQuantity.ToString))
                _PQty = Integer.Parse("0" & _row!FNPackPerCarton.ToString)
                Exit For
            Next


            For Each R As DataRow In _oDt.Rows
                _Qty = 0
                _QtyBal = Integer.Parse(R!FNQuantity.ToString)
                _Cmd = "SELECT TZ.* "
                _Cmd &= vbCrLf & " FROM ( SELECT  B.FTBarcodeNo, B.FNHSysUnitSectId, sum(B.FNQuantity) as FNQuantity, D.FTColorway, D.FTSizeBreakDown , D.FTOrderNo ,min(B.FTTime) AS FTTime , min(B.FDDate) AS FDDate  "
                _Cmd &= vbCrLf & "FROM    (Select TT.FTBarcodeNo , TT.FNQuantity , O.FNHSysUnitSectId , min(O.FTTime) AS FTTime , min( O.FDDate) AS FDDate"
                _Cmd &= vbCrLf & "From ("
                _Cmd &= vbCrLf & "Select sum(FNQuantity) AS FNQuantity , FTBarcodeNo"
                _Cmd &= vbCrLf & "From (SELECT  sum(O.FNQuantity) AS FNQuantity ,  O.FTBarcodeNo "
                _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline AS O WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS A WITH (NOLOCK) ON O.FTBarcodeNo = A.FTBarcodeBundleNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS P WITH (NOLOCK) ON A.FTOrderProdNo = P.FTOrderProdNo"
                _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and  A.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and  CASE WHEN ISNULL(A.FTColorwayNew,'') ='' THEN A.FTColorway ELSE ISNULL(A.FTColorwayNew,'') END='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "Group by O.FTBarcodeNo "
                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "Select  -sum(FNScanQuantity) AS FNQuantity , FTBarcodeNo"
                _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "Where  FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "group by FTBarcodeNo ) AS T "
                _Cmd &= vbCrLf & "group by FTBarcodeNo ) AS TT INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline AS O WITH(NOLOCK) ON TT.FTBarcodeNo = O.FTBarcodeNo"
                _Cmd &= vbCrLf & "Where TT.FNQuantity > 0 "
                _Cmd &= vbCrLf & " group by  TT.FTBarcodeNo, TT.FNQuantity, O.FNHSysUnitSectId ) AS B LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " (SELECT        H.FTBarcodeBundleNo,   CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END AS  FTColorway, H.FTSizeBreakDown, P.FTOrderNo"
                _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS H WITH (NOLOCK) LEFT OUTER JOIN"
                '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle_Detail AS D WITH (NOLOCK) ON H.FTBarcodeBundleNo = D.FTBarcodeBundleNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS P WITH (NOLOCK) ON H.FTOrderProdNo = P.FTOrderProdNo"
                _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination AS B WITH(NOLOCK) ON P.FTOrderNo = B.FTOrderNo and P.FTSubOrderNo = B.FTSubOrderNo "
                _Cmd &= vbCrLf & "   And H.FTSizeBreakDown = B.FTSizeBreakDown and CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END = B.FTColorway    " 'and  H.FTPOLineItemNo = B.FTNikePOLineItem"
                _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and H.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "	and B.FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "Group by    H.FTBarcodeBundleNo, CASE WHEN ISNULL(H.FTColorwayNew,'') ='' THEN H.FTColorway ELSE ISNULL(H.FTColorwayNew,'') END , H.FTSizeBreakDown, P.FTOrderNo "
                _Cmd &= vbCrLf & " ) AS D ON B.FTBarcodeNo = D.FTBarcodeBundleNo"
                _Cmd &= vbCrLf & "Where  D.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and D.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and D.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                'If HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) <> "" Then
                '    _Cmd &= vbCrLf & "and B.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                'End If
                _Cmd &= vbCrLf & "Group by  B.FTBarcodeNo, B.FNHSysUnitSectId,  D.FTColorway, D.FTSizeBreakDown , D.FTOrderNo   ) AS TZ "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS B WITH (NOLOCK) ON  TZ.FTBarcodeNo =  B.FTBarcodeBundleNo"

                If (Not GetStateHanger()) Then
                    If (CheckPackType(Me.FTPackNo.Text)) Then
                        If (_PQty > 0) And (_StateCarton) Then
                            _Cmd &= vbCrLf & " WHERE ( B.FNQuantity =" & _PQty & ")   "
                        Else
                            _Cmd &= vbCrLf & " WHERE ( B.FNQuantity <" & _PQty & ")   "
                        End If
                    End If
                End If

                _Cmd &= vbCrLf & "Order by  TZ.FDDate, TZ.FTTime asc "
                _oDt2 = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                If _oDt2.Rows.Count <= 0 Then Return False

                For Each x As DataRow In _oDt2.Rows
                    _Qty = Integer.Parse(x!FNQuantity.ToString)
                    If _QtyBal <= _Qty Then
                        _QtyPack = _QtyBal
                    Else
                        _QtyPack = _Qty
                    End If
                    _QtyBal = _QtyBal - _QtyPack

                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan "
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,FNScanQuantity,FTStateScanAuto)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & "," & _CartonNo
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTColorway.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTSizeBreakDown.ToString) & "'"
                    _Cmd &= vbCrLf & ",0" '& Integer.Parse(x!FNHSysUnitSectId.ToString)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTBarcodeNo.ToString) & "'"
                    _Cmd &= vbCrLf & "," & _QtyPack
                    _Cmd &= vbCrLf & ",'1'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail "
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,FDScanDate, FDScanTime, FNScanQuantity,FTStateScanAuto)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & "," & _CartonNo
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTColorway.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTSizeBreakDown.ToString) & "'"
                    _Cmd &= vbCrLf & ",0" '& Integer.Parse(x!FNHSysUnitSectId.ToString)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTBarcodeNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FDDate.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(x!FTTime.ToString) & "'"
                    _Cmd &= vbCrLf & "," & _QtyPack
                    _Cmd &= vbCrLf & ",'1'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _QtyBal <= 0 Then
                        Exit For
                    End If
                Next
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function CheckPackType(_FTPackNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 FTPackNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "Where FNOrderPackType=0 and FTPackNo='" & _FTPackNo & "'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkScanAuto() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  Isnull(FTStateCloseCartonAutoScan,'0') AS FTStateCloseCartonAutoScan "
            _Cmd &= vbCrLf & "   FROM  TSEConfig WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FTCmpCode='" & HI.ST.SysInfo.CmpCode & "'"
            Return IIf(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "0") = "1", True, False)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTProductBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'If (Me.FNHSysUnitSectId.Enabled) Then
                '    If Me.FNHSysUnitSectId.Text = "" Then
                '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysUnitSectId_lbl.Text)
                '        Me.FNHSysUnitSectId.Focus()
                '        Exit Sub
                '    End If
                'End If

                Dim _Cmd As String = ""
                Dim _StateInsertAuto As Boolean = False
                Dim _LoopPackNo As Integer = 0
                If Me.FTProductBarcodeNo.Text <> "" Then
                    _StateBarCodeCarton = Me.FTStateSearchAuto.Checked
L0:

                    Dim _Qry As String = ""
                    _Qry = "SELECT TOP 1 FTPackNo "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
                    _Qry &= vbCrLf & " and FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "'"

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                        _StateBarCodeCarton = True
                    End If

                    GetPackNo(Me.FTProductBarcodeNo.Text, _StateBarCodeCarton)

                    'If Me.FTStateSearchAuto.Checked = True Then
                    '    Me.FTPackNo.Text = ""
                    '    Me.FTPackNo.Properties.Tag = 0
                    'End If

                    If _StateBarCodeCarton Then
                        _Cmd = "   SELECT TOP 1 FNCartonNo "
                        _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                        _Cmd &= vbCrLf & "  WHERE  (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "')"
                        _Cmd &= vbCrLf & " AND FTPackNo='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "

                        _PFNCartonNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "-1")

                        If _PFNCartonNo < 0 Then

                            Dim _oDt As DataTable
                            If Me.FTStateDeleteBarcode.Checked = False Then
                                '_Cmd = " SELECT P.FNCartonNo, P.FTPackNo, B.FTBarcodeBundleNo"
                                '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                                '_Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                                '_Cmd &= vbCrLf & " INNER JOIN"
                                '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On  P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                                '_Cmd &= vbCrLf & " B.FTBarcodeBundleNo = PD.FTBarcodeNo  and P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                                '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"

                                '_Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                                '_Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "') --AND (PD.FTPackNo IS NULL) "

                                'If Not (Me.FTStateSearchAuto.Checked) Then
                                '    _Cmd &= vbCrLf & "And P.FNCartonNo =" & Integer.Parse(Val(_PFNCartonNo))
                                'End If

                                '_Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "

                                _Cmd = " SELECT Top 1 P.FNCartonNo"
                                _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                                _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                                _Cmd &= vbCrLf & " INNER JOIN"
                                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                                _Cmd &= vbCrLf & " B.FTBarcodeBundleNo = PD.FTBarcodeNo and P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                                _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"

                                _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                                _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "') --AND (PD.FTPackNo IS NULL) "
                                _Cmd &= vbCrLf & "and (Isnull(PD.FNScanQuantity,0) < P.FNPackPerCarton) "
                                _Cmd &= vbCrLf & "And (P.FNCartonNo  not in (SELECT FNCartonNo"
                                _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                                _Cmd &= vbCrLf & "WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')))"


                                _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "
                                _PFNCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "-1")))
                                If _PFNCartonNo < 0 Then
                                    _Cmd = " SELECT Top 1 P.FNCartonNo"
                                    _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                                    _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                                    _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D WITH (nolock) ON P.FTSubOrderNo = D.FTSubOrderNo AND P.FTOrderNo = D.FTOrderNo AND P.FTColorway = D.FTColorway AND P.FTSizeBreakDown = D.FTSizeBreakDown AND  B.FTOrderProdNo = D.FTOrderProdNo"
                                    _Cmd &= vbCrLf & " LEFT OUTER JOIN"
                                    _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                                    _Cmd &= vbCrLf & "   P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                                    _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"
                                    _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination AS SB WITH(NOLOCK) ON B.FTPOLineItemNo = SB.FTNikePOLineItem"
                                    _Cmd &= vbCrLf & " and B.FTColorway = SB.FTColorway and B.FTSizeBreakDown = SB.FTSizeBreakDown and P.FTOrderNo = SB.FTOrderNo  and P.FTSubOrderNo = SB.FTSubOrderNo"

                                    _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                                    _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "')  AND (PD.FTPackNo IS NULL) "
                                    _Cmd &= vbCrLf & "and (Isnull(PD.FNScanQuantity,0) < P.FNPackPerCarton) "
                                    _Cmd &= vbCrLf & "And (P.FNCartonNo  not in (SELECT FNCartonNo"
                                    _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                                    _Cmd &= vbCrLf & "WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')))"

                                    _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "
                                    _PFNCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "-1")))

                                End If

                            Else

                                _Cmd = " SELECT P.FNCartonNo, P.FTPackNo, B.FTBarcodeBundleNo"
                                _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                                _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                                _Cmd &= vbCrLf & " LEFT OUTER JOIN"
                                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                                _Cmd &= vbCrLf & "  P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo   and B.FTBarcodeBundleNo = PD.FTBarcodeNo"
                                _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"
                                _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') = '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                                _Cmd &= vbCrLf & " AND (PD.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "')  "

                                If Not (Me.FTStateSearchAuto.Checked) Then
                                    _Cmd &= vbCrLf & "And P.FNCartonNo =" & Integer.Parse(Val(_PFNCartonNo))
                                End If
                                _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo DESC "


                            End If
                            '20160815
                            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                            For Each R As DataRow In _oDt.Rows
                                _PFNCartonNo = HI.UL.ULF.rpQuoted(R!FNCartonNo)
                                Exit For
                            Next



                        End If

                    Else

                        If CheckScanCarton(_PFNCartonNo, Me.FTProductBarcodeNo.Text) = False Then
                            If CheckScanBarcodeBundle(_PFNCartonNo, Me.FTProductBarcodeNo.Text) = False Then

                                If _LoopPackNo > 0 Then

                                    HI.MG.ShowMsg.mInfo("ข้อมูลหมายเลขกล่อง ไม่ถูกต้อง !!!", 1511020572, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                                    Me.FTProductBarcodeNo.Focus()
                                    Me.FTProductBarcodeNo.SelectAll()
                                    Exit Sub

                                Else

                                    If Me.FTStateSearchAuto.Checked Then

                                        _LoopPackNo = _LoopPackNo + 1
                                        Me.FTPackNo.Text = ""
                                        GoTo L0

                                    Else

                                        HI.MG.ShowMsg.mInfo("ข้อมูลหมายเลขกล่อง ไม่ถูกต้อง !!!", 1511020572, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                                        Me.FTProductBarcodeNo.Focus()
                                        Me.FTProductBarcodeNo.SelectAll()
                                        Exit Sub

                                    End If

                                End If
                            End If
                        End If
                    End If

                    If Val(_PFNCartonNo) <= 0 Then
                        HI.MG.ShowMsg.mInfo("ข้อมูลหมายเลขกล่อง ไม่ถูกต้อง !!!", 1511020572, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                        If _LoopPackNo > 0 Then
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub
                        Else
                            If Me.FTStateSearchAuto.Checked Then
                                _LoopPackNo = _LoopPackNo + 1
                                Me.FTPackNo.Text = ""
                                GoTo L0
                            Else
                                Me.FTProductBarcodeNo.Focus()
                                Me.FTProductBarcodeNo.SelectAll()
                                Exit Sub
                            End If
                        End If
                    End If

                    _Cmd = "   Select Top 1 S.FTPackNo   "
                    _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S with(nolock) "
                    _Cmd &= vbCrLf & " WHERE S.FTPackNo = '" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "'"
                    _Cmd &= vbCrLf & " and S.FNCartonNo =" & CInt(_PFNCartonNo)

                    If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

                        If Not (_StateScanAuto) Then
                            HI.MG.ShowMsg.mInfo("กล่องนี้ยังไม่ได้ทำการ Scan ท้าย ไลน์ ไม่สามารถทำการ Scan ปิดกล่องได้ !!!", 1511020574, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub
                        Else
                            _StateInsertAuto = True
                        End If

                    End If

                    Dim _oDDt As New DataTable
                    If Me.FTStateDeleteBarcode.Checked = False Then
                        Dim _StatePass As Boolean = True
                        If _StatePass Then
                            If _StateInsertAuto And _StateScanAuto Then
                                LoadrderPackBreakDownCarton(Me.FTPackNo.Text, _PFNCartonNo)
                                If ChkBarCodeBundle(Me.FTProductBarcodeNo.Text) Then
                                    If ChkOnhandBundle(Me.FTProductBarcodeNo.Text, _oDDt) Then
                                        If Not (SaveScanBundle(Me.FTProductBarcodeNo.Text)) Then
                                            HI.MG.ShowMsg.mInfo("ยอดงานคงเหลือตามบาร์โค้ดไม่พอปิดกล่อง กรุณาตรวจสอบ !!", 1606151339, "", "", MessageBoxIcon.Stop)
                                            Exit Sub
                                        End If
                                    Else
                                        If _oDDt.Rows.Count > 0 Then
                                            HI.MG.ShowMsg.mInfo("บาร์โค้ดมีการแสกนปิดกล่องไปแล้ว  กรุณาตรวจสอบ !! เลขที่กล่อง   ", 1703071418, Me.Text, _oDDt.Rows(0).Item("FNCartonNo").ToString, MessageBoxIcon.Stop)
                                            Exit Sub
                                        End If
                                        HI.MG.ShowMsg.mInfo("ยอดงานคงเหลือตามบาร์โค้ดไม่พอปิดกล่อง กรุณาตรวจสอบ !!", 1606151339, "", "", MessageBoxIcon.Stop)
                                        Exit Sub
                                    End If
                                Else
                                    If Not (ChkBarcodeCartonFromOrderPack(Me.FTProductBarcodeNo.Text, _PFTOrderNo, _PFTColorway, _PFTSizeBreakDown, _PFNCartonNo)) Then
                                        HI.MG.ShowMsg.mInfo("ยอดไม่พอปิดกล่อง กรุณาตรวจสอบ !!! ", 1608241005, "", "", MessageBoxIcon.Warning)
                                        Exit Sub
                                    End If
                                End If

                            End If
                            If ScanCarton(_PFNCartonNo) Then
                                Me.FTProductBarcodeNo.Focus()
                                Me.FTProductBarcodeNo.SelectAll()
                                SetNewCarton()
                            Else
                                Me.FTProductBarcodeNo.Focus()
                                Me.FTProductBarcodeNo.SelectAll()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData("Invalid Box Scan......", 1412030002, Me.Text)
                        End If
                    Else
                        Dim _Invoice As String
                        If CheckCartonInvoice(Me.FTPackNo.Text, _PFNCartonNo, _Invoice) Then

                            If DeleteBarcode(Me.FTPackNo.Text, Me.FTProductBarcodeNo.Text, _PFNCartonNo) Then

                                SetNewCarton(False)
                            Else
                                SetNewCarton(False, False)
                            End If

                        Else
                            HI.MG.ShowMsg.mInfo("ไม่สามารถลบได้ เนื่องจากมีการ ออกอินวอยไปแล้ว!!! ", 2208241005, "", _Invoice, MessageBoxIcon.Warning)
                            Exit Sub

                        End If
                    End If
                    '   GetPackNo(Me.FTProductBarcodeNo.Text)
                End If

                ' SetNewCarton()
                Me.FTProductBarcodeNo.Focus()
                Me.FTProductBarcodeNo.SelectAll()

            End If

        Catch ex As Exception
        End Try

    End Sub


    Private Function CheckCartonInvoice(ByVal _PackNo As String, ByVal _CartanNo As String, ByRef msgInvoice As String) As Boolean

        Try
            Dim _Cmd As String
            Dim _dt As DataTable
            Dim state As String
            Dim _n As String

            ''GET CONFIG
            _Cmd = "Select FTCfgData "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig  "
            _Cmd &= vbCrLf & " WHERE FTCfgName='CfgOpenCartonPackCheckInvoice'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SECURITY)

            For Each _row As DataRow In _dt.Rows
                state = _row!FTCfgData.ToString
                Exit For
            Next




            If state = "1" Then


                '' GET INVOICE

                _Cmd = "Select count(1) as n "
                _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC_Ref  "
                _Cmd &= vbCrLf & " WHERE FTPackNo='" & _PackNo & " '  and FNCartonNo = " & Val(_CartanNo)

                _n = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0")

                If Val(_n) > 0 Then

                    _Cmd = "Select FTInvoiceNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_ACC_Ref  "
                    _Cmd &= vbCrLf & " WHERE FTPackNo='" & _PackNo & " '  and FNCartonNo = " & Val(_CartanNo)

                    msgInvoice = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0")
                    Return False
                Else
                    Return True
                End If


            Else
                Return True
            End If




        Catch ex As Exception
            Return False
        End Try



    End Function

    Private Function ScanCarton(ByVal _CartanNo As String) As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton"
            _Cmd &= vbCrLf & "SET FTState='1'"
            _Cmd &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

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

            Try
                Dim _Qry As String = ""
                _Qry = "   UPDATE A SET FTStatePack ='1'	 "
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

    Private Function GetPackNo(_FTBarcodeNo As String, Optional ByVal _StateCartonBR As Boolean = False) As String
        Try
            Dim _PackNo As String = ""
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _oDtBarCodeCarton As DataTable

            If Me.FTPackNo.Text = "" Or Me.FTStateSearchAuto.Checked = True Then

                _Cmd = "   SELECT FNCartonNo "
                _Cmd &= vbCrLf & " , FTPackNo"
                _Cmd &= vbCrLf & " , FTBarCodeCarton"
                _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                _Cmd &= vbCrLf & "  WHERE  ((FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "') OR  (FTBarcodeBundleNo=N'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')) AND FDInsDate >=Convert(varchar(10),DateAdd(month,-16,GetDate()),111) "

                _oDtBarCodeCarton = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                If _oDtBarCodeCarton.Rows.Count > 0 Then

                    For Each Rx As DataRow In _oDtBarCodeCarton.Rows

                        Me.FTPackNo.Text = Rx!FTPackNo.ToString
                        _PFNCartonNo = CInt("0" & Rx!FNCartonNo.ToString)

                        Exit For

                    Next

                    _Cmd = "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                    _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                    _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"

                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    For Each R As DataRow In _oDt.Rows

                        If Me.FTStateDeleteBarcode.Checked = False Then
                            Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                            If Me.FNOrderPackType.SelectedIndex = 1 Then
                                Me.lblQtyScanSet.Text = (Val(R!FNScanQuantity.ToString) / Me.FNPackSetValue.Value).ToString
                            End If
                            _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                        End If

                    Next

                    Call GetTotalCartonScan()

                    Return _PackNo

                End If

            End If

            If Me.FTPackNo.Text = "" Or Me.FTStateSearchAuto.Checked = True Then
                _Cmd = "   SELECT FNCartonNo "
                _Cmd &= vbCrLf & " , FTPackNo"
                _Cmd &= vbCrLf & " , FTBarCodeCarton"
                _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                _Cmd &= vbCrLf & "  WHERE  (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')  AND A.FDInsDate >=Convert(varchar(10),DateAdd(month,-16,GetDate()),111)  "
                _oDtBarCodeCarton = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                If _oDtBarCodeCarton.Rows.Count > 0 Then
                    For Each Rx As DataRow In _oDtBarCodeCarton.Rows
                        Me.FTPackNo.Text = Rx!FTPackNo.ToString
                        _PFNCartonNo = CInt("0" & Rx!FNCartonNo.ToString)
                        Exit For
                    Next

                    _Cmd = "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                    _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                    _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"

                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    For Each R As DataRow In _oDt.Rows

                        If Me.FTStateDeleteBarcode.Checked = False Then
                            Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                            If Me.FNOrderPackType.SelectedIndex = 1 Then
                                Me.lblQtyScanSet.Text = (Val(R!FNScanQuantity.ToString) / Me.FNPackSetValue.Value).ToString
                            End If
                            _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                        End If
                    Next
                Else
                    _Cmd = "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "'"
                    _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"
                    _Cmd &= vbCrLf & "order by Sum(FNScanQuantity) desc"
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    For Each R As DataRow In _oDt.Rows
                        If Me.FTPackNo.Text = "" Then
                            Me.FTPackNo.Text = R!FTPackNo.ToString
                        End If
                        If Me.FTStateDeleteBarcode.Checked = False Then
                            Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                            If Me.FNOrderPackType.SelectedIndex = 1 Then
                                Me.lblQtyScanSet.Text = (Val(R!FNScanQuantity.ToString) / Me.FNPackSetValue.Value).ToString
                            End If
                            _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                        End If
                        Exit For
                    Next

                    If FTPackNo.Text = "" Or Me.FTStateSearchAuto.Checked = True Then

                        _Cmd = "SELECT  P.FTPackNo"
                        _Cmd &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS P  WITH(NOLOCK)  INNER JOIN"
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B  WITH(NOLOCK)  ON P.FTColorway = B.FTColorway AND P.FTSizeBreakDown = B.FTSizeBreakDown AND P.FNPackPerCarton = B.FNQuantity INNER JOIN"
                        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C WITH(NOLOCK) ON P.FTOrderNo = C.FTOrderNo AND B.FTOrderProdNo = C.FTOrderProdNo"
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS Z WITH(NOLOCK) ON P.FTPackNo = Z.FTPackNo and P.FNCartonNo = Z.FNCartonNo "
                        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail AS SD WITH(NOLOCK) ON P.FTPackNo = SD.FTPackNo and P.FNCartonNo = SD.FNCartonNo  "
                        _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS BD WITH (NOLOCK) ON P.FTSizeBreakDown = BD.FTSizeBreakDown AND P.FTColorway = BD.FTColorway AND P.FTSubOrderNo = BD.FTSubOrderNo "
                        _Cmd &= vbCrLf & " AND P.FTOrderNo = BD.FTOrderNo AND B.FTPOLineItemNo = BD.FTNikePOLineItem  "
                        _Cmd &= vbCrLf & " WHERE (B.FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')   and Isnull(Z.FTState,'') <> '1' and Isnull(SD.FTPackNo,'') = '' "
                        _Cmd &= vbCrLf & "GROUP BY P.FTPackNo "

                        Dim dtpacno As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        If dtpacno.Rows.Count > 0 Then

                            If FTPackNo.Text <> "" Then

                                If dtpacno.Select("FTPackNo='" & FTPackNo.Text & "'").Length > 0 Then
                                    For Each r As DataRow In dtpacno.Select("FTPackNo='" & FTPackNo.Text & "'")
                                        FTPackNo.Text = r!FTPackNo.ToString
                                        Exit For
                                    Next
                                Else
                                    FTPackNo.Text = dtpacno.Rows(0)!FTPackNo.ToString
                                End If

                            Else
                                FTPackNo.Text = dtpacno.Rows(0)!FTPackNo.ToString
                            End If

                        End If

                        dtpacno.Dispose()
                        'If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                        '    FTPackNo.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")
                        'End If

                    End If

                End If

                Dim _Qry As String = ""
                _Qry = "SELECT TOP 1 FTPackNo "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')  AND A.FDInsDate >=Convert(varchar(10),DateAdd(month,-16,GetDate()),111)  "
                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    _StateBarCodeCarton = True
                End If
            Else
                _Cmd = "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDt.Rows
                    If Me.FTPackNo.Text = "" Then
                        Me.FTPackNo.Text = R!FTPackNo.ToString
                    End If
                    If Me.FTStateDeleteBarcode.Checked = False Then
                        Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                        If Me.FNOrderPackType.SelectedIndex = 1 Then
                            Me.lblQtyScanSet.Text = (Val(R!FNScanQuantity.ToString) / Me.FNPackSetValue.Value).ToString
                        End If
                        _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                    End If
                Next
            End If

            _Cmd = "SELECT Top 1     S.FNHSysUnitSectId, U.FTUnitSectCode "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",U.FTUnitSectNameTH AS FTUnitSectName"
            Else
                _Cmd &= vbCrLf & ",U.FTUnitSectNameEN AS FTUnitSectName"
            End If
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect U WITH(NOLOCK) ON S.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Cmd &= vbCrLf & " WHERE S.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & " and S.FNCartonNo=" & _PFNCartonNo

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                Me.FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                Me.FNHSysUnitSectId_None.Text = R!FTUnitSectName.ToString
            Next
            Call GetTotalCartonScan()
            Return _PackNo
        Catch ex As Exception
        End Try
    End Function

    Private Function CheckScanCarton(ByVal _CartonNo As String, ByVal _FTBarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "  SELECT       C.FTPackNo, C.FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, "
            _Cmd &= vbCrLf & "      FNScanQuantity , Isnull(S.FTState,'0') as FTState"
            _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & "WHERE     (C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
            _Cmd &= vbCrLf & " AND (C.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')"

            If Not (Me.FTStateSearchAuto.Checked) Then
                _Cmd &= vbCrLf & "And C.FNCartonNo =" & Integer.Parse(_CartonNo)
            End If

            _Cmd &= vbCrLf & "ORDER BY  C.FNScanQuantity DESC , C.FNCartonNo ASC "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _oDt.Rows
                _PFNCartonNo = HI.UL.ULF.rpQuoted(R!FNCartonNo)
                Exit For
            Next

            'Return (HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0)

            Return (_oDt.Rows.Count > 0)

        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function CheckScanBarcodeBundle(ByVal _CartonNo As String, ByVal _FTBarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = " SELECT P.FNCartonNo, P.FTPackNo, B.FTBarcodeBundleNo"
            _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
            _Cmd &= vbCrLf & "  P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
            _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS D WITH(NOLOCK) ON P.FTOrderNo = D.FTOrderNo "
            _Cmd &= vbCrLf & " and P.FTSubOrderNo = D.FTSubOrderNo and B.FTColorway = D.FTColorway and B.FTSizeBreakDown = D.FTSizeBreakDown and B.FTPOLineItemNo = D.FTNikePOLineItem "
            _Cmd &= vbCrLf & "WHERE     (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
            _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "') AND (PD.FTPackNo IS NULL) "

            If Not (Me.FTStateSearchAuto.Checked) Then
                _Cmd &= vbCrLf & "And P.FNCartonNo =" & Integer.Parse(Val(_CartonNo))
            End If

            _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _oDt.Rows
                _PFNCartonNo = HI.UL.ULF.rpQuoted(R!FNCartonNo)
                Exit For
            Next

            'Return (HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0)

            Return (_oDt.Rows.Count > 0)

        Catch ex As Exception
            Return False
        End Try
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
            _Cmd &= vbCrLf & "and FNHSysUnitSectId=" & CInt(_UnitSectId)
            _Cmd &= vbCrLf & "and FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _Cmd &= vbCrLf & "Group By   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId,FTBarcodeNo "

            Return CInt(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetCartonQty(ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String, _
                               ByVal _SizeBreakDown As String, ByVal _BarcodeNo As String, ByVal _UnitSectId As Integer) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select FNQuantity  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
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

    Private Sub CheckQtyCarton(ByVal _BarcodeNo As String, ByVal _CartonNo As Integer, ByVal _OrderNo As String, ByVal _SubOrderNo As String, ByVal _Colorway As String, _
                                ByVal _SizeBreakDown As String)
        Try
            Dim _oDt, _oDt2 As DataTable
            Dim _Cmd As String = ""
            'Find
            _Cmd = "SELECT   Top 1   FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, FNScanQuantity"
            _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan"
            _Cmd &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "'"
            _Cmd &= vbCrLf & " AND FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _oDt2 = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            If _oDt2.Rows.Count > 0 Then

                _Cmd = "Select Isnull(C.FNQuantity,0) AS FNQuantity , Isnull(S.FNScanQuantity,0) AS FNScanQuantity ,C.FNCartonNo"
                _Cmd &= vbCrLf & "From TPACKOrderPack_Carton_Detail AS C LEFT OUTER JOIN TPACKOrderPack_Carton_Scan AS S "
                _Cmd &= vbCrLf & "ON C.FTPackNo = S.FTPackNo and C.FTColorway = S.FTColorway and C.FTOrderNo = S.FTOrderNo "
                _Cmd &= vbCrLf & "and C.FTSubOrderNo = S.FTSubOrderNo and C.FTSizeBreakDown = S.FTSizeBreakDown  and C.FNCartonNo = S.FNCartonNo"
                _Cmd &= vbCrLf & "where C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & "and C.FTColorway = '" & HI.UL.ULF.rpQuoted(_oDt2.Rows(0)!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "and C.FTOrderNo = '" & HI.UL.ULF.rpQuoted(_oDt2.Rows(0)!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and C.FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_oDt2.Rows(0)!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and C.FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(_oDt2.Rows(0)!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & " ORDER BY C.FNCartonNo ASC "

            Else

                _Cmd = "Select Isnull(C.FNQuantity,0) AS FNQuantity , Isnull(S.FNScanQuantity,0) AS FNScanQuantity ,C.FNCartonNo"
                _Cmd &= vbCrLf & "From TPACKOrderPack_Carton_Detail AS C LEFT OUTER JOIN TPACKOrderPack_Carton_Scan AS S "
                _Cmd &= vbCrLf & "ON C.FTPackNo = S.FTPackNo and C.FTColorway = S.FTColorway and C.FTOrderNo = S.FTOrderNo "
                _Cmd &= vbCrLf & "and C.FTSubOrderNo = S.FTSubOrderNo and C.FTSizeBreakDown = S.FTSizeBreakDown  and C.FNCartonNo = S.FNCartonNo"
                _Cmd &= vbCrLf & "where C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & "and C.FTColorway = '" & HI.UL.ULF.rpQuoted(_Colorway) & "'"
                _Cmd &= vbCrLf & "and C.FTOrderNo = '" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
                _Cmd &= vbCrLf & "and C.FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_SubOrderNo) & "'"
                _Cmd &= vbCrLf & "and C.FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(_SizeBreakDown) & "'"
                _Cmd &= vbCrLf & "ORDER BY  C.FNCartonNo ASC  "

            End If
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                If CInt(R!FNQuantity) > CInt(R!FNScanQuantity) Then
                    _PFNCartonNo = CInt(R!FNCartonNo.ToString)
                    SetNewCarton()
                    Exit For
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _LoadDataScan()
        Try
            Dim _colcount As Integer
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyScan As Integer = 0

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_ScanQty '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "','" & CInt("0" & _PFNCartonNo) & "','" & HI.UL.ULF.rpQuoted(_PFTOrderNo) & "'"
            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "','" & CInt(0) & "','" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "','" & HI.UL.ULF.rpQuoted(_PFTColorway) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcScan.DataSource = _oDt
            Try
                For Each R As DataRow In _oDt.Rows
                    _QtyScan += +R!Total.ToString
                Next

                lblQtyScan.Text = _QtyScan
                If Me.FNOrderPackType.SelectedIndex = 1 Then
                    Me.lblQtyScanSet.Text = (Val(_QtyScan) / Me.FNPackSetValue.Value).ToString
                End If
            Catch ex As Exception
                lblQtyScan.Text = 0
                Me.lblQtyScanSet.Text = 0
            End Try
            With Me.ogvScan
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
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
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
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
            Call GetTotalCartonScan()
        Catch ex As Exception
        End Try
    End Sub

    Private Function DeleteBarcode(ByVal _PackNo As String, ByVal _BarcodeNo As String, ByVal _CartonNo As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _ScanQty As Integer = 0
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            '_Cmd = "exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_SETDATATO_AX_IVZ_WISDOMREPORTASFINISHSTAGING_CT_CancelScan]"
            '_Cmd &= vbCrLf & " @PackNo ='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            '_Cmd &= vbCrLf & " , @CartonNo=" & CInt(_CartonNo)
            _Cmd = ""

            _Cmd &= vbCrLf & "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton"
            _Cmd &= vbCrLf & " WHERE FTPackNo  in(Select Top 1 C.FTPackNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S ON C.FTPackNo = S.FTPackNo"
            _Cmd &= vbCrLf & " and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE C.FTPackNo = '" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " and C.FNCartonNo = " & CInt(_CartonNo)

            If _StateBarCodeCarton = False Then
                _Cmd &= vbCrLf & " and S.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "' "
            End If

            _Cmd &= vbCrLf & "  )"
            _Cmd &= vbCrLf & " and FNCartonNo in (Select Top 1 C.FNCartonNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S ON C.FTPackNo = S.FTPackNo"
            _Cmd &= vbCrLf & " and C.FNCartonNo = S.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE C.FTPackNo = '" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
            _Cmd &= vbCrLf & " and C.FNCartonNo =" & CInt(_CartonNo)

            If _StateBarCodeCarton = False Then
                _Cmd &= vbCrLf & " and S.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarcodeNo) & "' "
            End If

            _Cmd &= vbCrLf & " )"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            If (_StateScanAuto) Then
                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail"
                _Cmd &= vbCrLf & "Where FTPackNo ='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & "and FNCartonNo=" & Integer.Parse(_CartonNo)
                _Cmd &= vbCrLf & "and FTStateScanAuto='1'"
                HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan"
                _Cmd &= vbCrLf & "Where FTPackNo ='" & HI.UL.ULF.rpQuoted(_PackNo) & "'"
                _Cmd &= vbCrLf & "and FNCartonNo=" & Integer.Parse(_CartonNo)
                _Cmd &= vbCrLf & "and FTStateScanAuto='1'"
                HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub GetTotalCartonScan()
        Try
            Dim _Cmd As String = ""
            _Cmd = " "
            _Cmd &= vbCrLf & "    SELECT (Convert(varchar(50),FNTotalCartonScan) + ' / ' +  Convert(varchar(50),FNTotalCarton)) AS FTTotalCartonScan"
            _Cmd &= vbCrLf & " FROM (SELECT  A.FTPackNo"
            _Cmd &= vbCrLf & ", Count(1) AS FNTotalCarton"
            _Cmd &= vbCrLf & ",SUM(CASE WHEN  ISNULL(B.FTState,'') = '1' THEN 1 ELSE 0 END)  AS FNTotalCartonScan"
            _Cmd &= vbCrLf & " FROM    (	SELECT FTPackNo, FNCartonNo"
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS XX WITH(NOLOCK)"
            _Cmd &= vbCrLf & "WHERE     (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Cmd &= vbCrLf & " GROUP BY FTPackNo, FNCartonNo"
            _Cmd &= vbCrLf & " ) AS A"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS B WITH(NOLOCK) ON A.FTPackNo = B.FTPackNo AND A.FNCartonNo = B.FNCartonNo "
            _Cmd &= vbCrLf & "WHERE     (A.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Cmd &= vbCrLf & " GROUP BY A.FTPackNo ) AS X"

            olbtotalcartonscan_lbl.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0 / 0")
        Catch ex As Exception
            olbtotalcartonscan_lbl.Text = "0 / 0"
        End Try
    End Sub

    Private Function GetTotalCarton() As Integer
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  *"
            _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE     (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub SetNewCarton(Optional _State As Boolean = True, Optional _Succ As Boolean = True)
        Try
            Dim _Qry As String = ""
            Dim _TmpFNCartonNo As Integer = -1
            'otlpack.FocusedNode = otlpack.Nodes(0)
            Dim _PFNCartonNoHold As Integer = 0
            If CInt("0" & _PFNCartonNo) < 1 Then _PFNCartonNo = 1
            If CInt("0" & _PFNCartonNo) > _PMaxCarton Then _PFNCartonNo = _PMaxCarton
            If _State Then
                otlpack.SetFocusedNode(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse(Val(_PFNCartonNo))).ToString))
                If otlpack.FocusedNode.Checked = True Then
                    _TmpFNCartonNo = -1
                    _Qry = " SELECT TOP 1 FNCartonNo "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FNCartonNo>" & Integer.Parse(Val(_PFNCartonNo)) & " "
                    _Qry &= vbCrLf & " ORDER BY FNCartonNo ASC "
                    _TmpFNCartonNo = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, -1))
                    If _TmpFNCartonNo > 0 Then
                        _PFNCartonNo = _TmpFNCartonNo
                    End If
                End If
                otlpack.SetNodeCheckState(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse(Val(_PFNCartonNo))).ToString), CheckState.Checked)
                _PFNCartonNoHold = _PFNCartonNo
                _TmpFNCartonNo = -1
                _Qry = " SELECT TOP 1 FNCartonNo "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                _Qry &= vbCrLf & " AND FNCartonNo>" & Integer.Parse(Val(_PFNCartonNo)) & " "
                _Qry &= vbCrLf & " ORDER BY FNCartonNo ASC "
                _TmpFNCartonNo = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, -1))

                If _TmpFNCartonNo > 0 Then
                    _PFNCartonNo = _TmpFNCartonNo
                End If

            Else
                If (_Succ) Then
                    otlpack.SetNodeCheckState(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse(Val(_PFNCartonNo))).ToString), CheckState.Unchecked)
                End If
            End If

            If _PFNCartonNo < 1 Then _PFNCartonNo = 1
            If _PFNCartonNo > _PMaxCarton Then _PFNCartonNo = _PMaxCarton

            '   otlpack.SetFocusedNode(otlpack.GetNodeByVisibleIndex(Integer.Parse(Val(_PFNCartonNo))))  
            'เปลี่ยนจาก*****เดิม Focus กล่องใหม่ แล้วเป็นมาให้Focus กล่องที่เต็มแล้ว
            ' otlpack.SetFocusedNode(otlpack.GetNodeByVisibleIndex(Integer.Parse(Val(_PFNCartonNoHold))))
            otlpack.SetFocusedNode(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse(Val(_PFNCartonNo))).ToString))
            Call _LoadDataScan()
            otlpack_Click(otlpack, New EventArgs)




            Me.FTProductBarcodeNo.Text = getBarcodes()
        Catch ex As Exception
            Try
                Call _LoadDataScan() 'ไม่มีจำนวนแสกนขึ้นโชว์
            Catch exx As Exception
            End Try
        End Try
    End Sub

    Private Function getBarcodes() As String
        Try
            Dim _Cmd As String
            _Cmd = "SELECT  top 1 FTBarCodeCarton FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode WITH(NOLOCK) WHERE  (FTPackNo = N'" & Me.FTPackNo.Text & "')"
            _Cmd &= vbCrLf & "and FNCartonNo =" & _PFNCartonNo

            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub otlpack_NodeCellStyle(sender As Object, e As GetCustomNodeCellStyleEventArgs) Handles otlpack.NodeCellStyle
        Try
            Select Case e.Column.FieldName.ToString
                Case "FTCartonName"
                    If e.Node.GetValue(9).ToString <> "" Then
                        e.Appearance.ForeColor = Color.Blue
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmManulClose_Click(sender As Object, e As EventArgs) Handles ocmManulClose.Click
        Try
            Dim _Cmd As String = ""
            '_Cmd = "SELECT distinct  FTPackNo, 'Carton No.'+ convert(nvarchar(4),FNCartonNo)  as FTCartonNo"
            '_Cmd &= vbCrLf & ",Isnull((Select Top 1 Isnull( FTState,'0') as FTState From [HITECH_PRODUCTION].dbo.TPACKCarton where FTPackNo =  T.FTPackNo and  FNCartonNo =  T.FNCartonNo),'0') AS FTSelect"
            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS T"
            '_Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "


            Dim _oDt As DataTable

            _Cmd = "SELECT '0' as FTSelect ,  C.FTPackNo, C.FNCartonNo as FTCartonNo , D.FTOrderNo, D.FTSubOrderNo, D.FTColorway,"
            _Cmd &= vbCrLf & "(SELECT TOP 1 STUFF"
            _Cmd &= vbCrLf & "((SELECT ', ' + t2.FTSizeBreakDown"
                                              _Cmd &= vbCrLf & "   FROM      (SELECT FTPackNo , FNCartonNo , FTSizeBreakDown "
            _Cmd &= vbCrLf & " From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS Z"
            _Cmd &= vbCrLf & "   GROUP BY FTPackNo , FNCartonNo , FTSizeBreakDown ) t2"
            _Cmd &= vbCrLf & "   WHERE   t2.FTPackNo = c.FTPackNo  AND t2.FNCartonNo = C.FNCartonNo  FOR XML PATH('')), 1, 2, '') AS FTSizeBreakDown) AS FTSizeBreakDown"

            _Cmd &= vbCrLf & ", sum(D.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & ", case when  Isnull(B.FTBarCodeEAN13,'') = '' then  isnull(B.FTBarCodeCarton,'') else  B.FTBarCodeEAN13 end as FTBarCodeCarton"
            _Cmd &= vbCrLf & ", case when isnull( OS.FTPORef,'') = '' then isnull(O.FTPORef,'') else isnull( OS.FTPORef,'')   end   AS FTPORef"
            _Cmd &= vbCrLf & ", SB.FTNikePOLineItem as FTNikePOLineItem"

            _Cmd &= vbCrLf & "   From    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS C WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS D WITH (NOLOCK) ON C.FTPackNo = D.FTPackNo AND C.FNCartonNo = D.FNCartonNo"
            _Cmd &= vbCrLf & "   Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode AS B WITH(NOLOCK) ON C.FTPackNo = B.FTPackNo and  C.FNCartonNo  = B.FNCartonNo"
            _Cmd &= vbCrLf & "   Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON D.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & "   Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OS WITH(NOLOCK) ON D.FTOrderNo = OS.FTOrderNo and D.FTSubOrderNo = OS.FTSubOrderNo"
            _Cmd &= vbCrLf & "Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SB WITH(NOLOCK) ON D.FTOrderNo = SB.FTSubOrderNo and D.FTSubOrderNo = SB.FTSubOrderNo"
            _Cmd &= vbCrLf & "WHERE  (C.FTState = '1')"
            _Cmd &= vbCrLf & "And c.FTPackNo +'|'+convert(varchar(5) , C.FNCartonNo) not in (Select  FTPackNo +'|'+convert(varchar(5) ,  FNCartonNo)   From TPRODTBarcodeScanFG )"
            _Cmd &= vbCrLf & "group by  C.FTPackNo , C.FNCartonNo, D.FTOrderNo, D.FTSubOrderNo, D.FTColorway , B.FTBarCodeCarton , B.FTBarCodeEAN13 , OS.FTPORef  , O.FTPORef, SB.FTNikePOLineItem"





            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            HI.TL.HandlerControl.ClearControl(_ListClose)

            Dim _State As Boolean = False
            If _oDt.Rows.Count > 0 Then
                With _ListClose
                    .ogvlist.ClearColumnsFilter()
                    .ogclist.DataSource = _oDt.Copy
                    .ShowDialog()
                    Dim _FNWHFGId As Integer = Integer.Parse("0" & .FNHSysWHFGId.Properties.Tag.ToString)
                    If (.State) Then
                        With DirectCast(.ogclist.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTSelect = '1'")
                                If SaveData(R!FTBarCodeCarton.ToString, _FNWHFGId, True, R!FTCartonNo.ToString, R!FTOrderNo.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString) Then

                                    _State = True
                                Else
                                    _State = False
                                    HI.MG.ShowMsg.mInfo("เกิดข้อผิดพลาดบางประการ กรุณาตรวจสอบ !!!!", 1707030855, Me.Text, "", MessageBoxIcon.Stop)
                                    Exit Sub
                                End If
                            Next


                            'Call CloseCartonManul(.Copy)
                        End With
                    End If

                End With
                If _State Then
                    HI.MG.ShowMsg.mInfo("โอนเข้าคลังสำเร็จรูปเรียบร้อย...", 1707030854, Me.Text, "", MessageBoxIcon.Information)

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Function SaveData(_FTProductBarcodeNo As String, ByVal _FNHSysWHFGId As Integer, state As Boolean, _CartonNo As Integer, Optional ByVal _FTOrderNo As String = "", Optional ByVal _FTColorway As String = "", Optional ByVal _FTSizeBreakDown As String = "") As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            _Cmd = "UPDATE W"
            _Cmd &= vbCrLf & "Set W.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ", W.FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",W.FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",W.FNQuantity=T.FNScanQuantity"
            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W INNER JOIN "
            '_Cmd &= vbCrLf & " (SELECT    B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, B.FTBarcodeNo, B.FNScanQuantity, C.FTBarCodeCarton"
            '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) INNER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON B.FTPackNo = C.FTPackNo AND B.FNCartonNo = C.FNCartonNo ) AS T "
            '_Cmd &= vbCrLf & " ON W.FTOrderNo = T.FTOrderNo and W.FTColorway = T.FTColorway and W.FTBarCodeCarton = T.FTBarCodeCarton and  W.FTSizeBreakDown = T.FTSizeBreakDown"
            '_Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            '_Cmd &= vbCrLf & " And W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)

            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W INNER JOIN "
            _Cmd &= vbCrLf & "   (SELECT  P.FTPackNo , P.FNCartonNo,  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,sum(B.FNQuantity ) AS FNScanQuantity,Isnull(C.FTBarCodeEAN13 , c.FTBarCodeCarton)  as FTBarCodeCarton "
            '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON P.FTPackNo = C.FTPackNo AND P.FNCartonNo = C.FNCartonNo "
            _Cmd &= vbCrLf & " WHERE (C.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & " OR   C.FTBarCodeEAN13='" & _FTProductBarcodeNo & "' OR P.FNCartonNo =" & _CartonNo & ")"
            If (state) Then
                _Cmd &= vbCrLf & " And  B.FTOrderNo='" & _FTOrderNo & "'"
                _Cmd &= vbCrLf & " AND B.FTColorway='" & _FTColorway & "'"
                _Cmd &= vbCrLf & " And B.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
            End If

            '_Cmd &= vbCrLf & "  and  isnull(C.FTBarCodeCarton,B.FTBarcodeNo) +'|'+B.FTOrderNo+'|'+B.FTColorway+'|'+B.FTSizeBreakDown +'|'+P.FTPackNo+'|'+convert(nvarchar(18) , P.FNCartonNo) not in ("
            '_Cmd &= vbCrLf & " SELECT     FTBarCodeCarton +'|'+FTOrderNo +'|'+FTColorWay +'|'+FTSizeBreakDown+'|'+FTPackNo+'|'+convert(nvarchar(18) , FNCartonNo)  "
            '_Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG WITH(NOLOCK) )"

            _Cmd &= vbCrLf & "Group by P.FTPackNo , P.FNCartonNo, B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown ,Isnull(C.FTBarCodeEAN13 , c.FTBarCodeCarton)  ) AS T "
            _Cmd &= vbCrLf & "ON W.FTOrderNo = T.FTOrderNo and W.FTColorWay = T.FTColorway and W.FTSizeBreakDown = T.FTSizeBreakDown and W.FTBarCodeCarton = T.FTBarCodeCarton and W.FTPackNo = T.FTPackNo and W.FNCartonNo = T.FNCartonNo"
            _Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(_FNHSysWHFGId)

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity , FTPackNo , FNCartonNo)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & _FTProductBarcodeNo & "'"
                _Cmd &= vbCrLf & "," & CInt(_FNHSysWHFGId)
                _Cmd &= vbCrLf & ",T.FTColorWay"
                _Cmd &= vbCrLf & ",T.FTSizeBreakDown"
                _Cmd &= vbCrLf & ",T.FTOrderNo"
                _Cmd &= vbCrLf & ",T.FNScanQuantity"
                _Cmd &= vbCrLf & ",T.FTPackNo"
                _Cmd &= vbCrLf & ",T.FNCartonNo"
                _Cmd &= vbCrLf & " From (SELECT  P.FTPackNo , P.FNCartonNo,    B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, sum(B.FNQuantity ) AS FNScanQuantity,Isnull(C.FTBarCodeEAN13 , c.FTBarCodeCarton) as FTBarCodeCarton "
                '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
                _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON B.FTPackNo = C.FTPackNo AND B.FNCartonNo = C.FNCartonNo "
                _Cmd &= vbCrLf & " WHERE (C.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
                _Cmd &= vbCrLf & " OR C.FTBarCodeEAN13='" & _FTProductBarcodeNo & "'  OR P.FNCartonNo =" & _CartonNo & " )"
                _Cmd &= vbCrLf & " and P.FTState = '1' "
                If (state) Then
                    _Cmd &= vbCrLf & " And  B.FTOrderNo='" & _FTOrderNo & "'"
                    _Cmd &= vbCrLf & " AND B.FTColorway='" & _FTColorway & "'"
                    _Cmd &= vbCrLf & " And B.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
                End If



                _Cmd &= vbCrLf & "Group by  P.FTPackNo , P.FNCartonNo,   B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,Isnull(C.FTBarCodeEAN13 , c.FTBarCodeCarton)) AS T "
                '_Cmd &= vbCrLf & " ON W.FTOrderNo = T.FTOrderNo and W.FTColorway = T.FTColorway and W.FTBarCodeCarton = T.FTBarCodeCarton and  W.FTSizeBreakDown = T.FTSizeBreakDown"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Function CloseCartonManul(_oDt As DataTable) As Boolean
        Try
            Dim _Cmd As String = ""
            For Each R As DataRow In _oDt.Select("FTSelect ='1'")
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton"
                _Cmd &= vbCrLf & "Set FTState='1'"
                _Cmd &= vbCrLf & "Where FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                _Cmd &= vbCrLf & "And FNCartonNo=" & Integer.Parse("0" & R!FNCartonNo.ToString)
                If Not (HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)) Then
                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton (FTInsUser, FDInsDate, FTInsTime, FTPackNo, FNCartonNo, FTState)"
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNCartonNo.ToString)
                    _Cmd &= vbCrLf & ",'1'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkBarCodeBundle(_Barcode As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1  FTBarcodeBundleNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(_Barcode) & "'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count = 1
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
    Private Function ChkOnhandBundle(_Barcode As String, ByRef dt As DataTable) As Boolean
        Try
            'check state close carton 
            Dim _Cmd As String = ""
            _Cmd = "SELECT top 1  FTBarcodeNo ,FNCartonNo , FTPackNo From[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPACKOrderPack_Carton_Scan(NOLOCK) where  (FTBarcodeNo = N'" & HI.UL.ULF.rpQuoted(_Barcode) & "') "
            dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            'ต้องผ่านการแสกน ออกไลน์ 
            Dim _Qry As String = ""
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
            _Qry &= vbCrLf & " FROM     (SELECT  FTBarcodeNo"
            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline WITH (NOLOCK)"
            _Qry &= vbCrLf & " Group by FTBarcodeNo "
            _Qry &= vbCrLf & " Having sum(FNQuantity) =" & _PQty & ""

            _Qry &= vbCrLf & " ) As A INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B (NOLOCK)  On A.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " WHERE "
            _Qry &= vbCrLf & " ( B.FNQuantity =" & _PQty & ")  And"
            _Qry &= vbCrLf & "   B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_Barcode) & "'"

            _Qry &= vbCrLf & " AND B.FTOrderProdNo +'|' + B.FTColorway + '|' + B.FTSizeBreakDown IN ("
            _Qry &= vbCrLf & " SELECT B.FTOrderProdNo +'|' + B.FTColorway + '|' + B.FTSizeBreakDown "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail  AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  B.FTSubOrderNo IN (SELECT  B.FTSubOrderNo"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS A LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS B ON A.FTPackNo = B.FTPackNo"
            _Qry &= vbCrLf & " WHERE     (A.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'))"

            _Qry &= vbCrLf & "  ) "
            _Qry &= vbCrLf & "  ) AS A"

            _Qry &= vbCrLf & "INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline   AS B WITH(NOLOCK)   ON A.FTBarcodeBundleNo = B.FTBarcodeNo "

            If FTStateDeleteBarcode.Checked = False Then
                _Qry &= vbCrLf & " WHERE A.FNQuantity > Qty AND (A.FNQuantity - Qty ) >=" & _PQty & " "
            End If

            _Qry &= vbCrLf & "ORDER BY Isnull(B.FDUpdDate, B.FDInsDate ), Isnull(B.FTUpdTime, B.FTInsTime) ASC "
            Return HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> ""
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveScanBundle(_BarCode As String) As Boolean
        Try
            Dim _Cmd As String = ""

            _Cmd = "   Select   O.FTBarcodeNo, O.FDDate, O.FTTime, O.FNQuantity,CASE WHEN ISNULL(B.FTColorwayNew,'') ='' THEN B.FTColorway ELSE ISNULL(B.FTColorwayNew,'') END AS FTColorway, B.FTSizeBreakDown, B.FTOrderProdNo, O.FTOrderNo, O.FTSubOrderNo"
            _Cmd &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODBarcodeScanOutline As O With (NOLOCK) LEFT OUTER Join"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle As B WITH (NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeBundleNo "
            ' _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail As P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo And B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown"
            _Cmd &= vbCrLf & "Where O.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  and  isnull(O.FNStateSewPack,0) = 0"
            If _PFTSubOrderNo <> "" Then
                _Cmd &= vbCrLf & "and  O.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_PFTSubOrderNo) & "'"
            End If
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            If _oDt.Rows.Count <= 0 Then Return False

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Rows

                _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail "
                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,FDScanDate, FDScanTime, FNScanQuantity,FTStateScanAuto)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & "," & _PFNCartonNo
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & ",0" '& Integer.Parse(x!FNHSysUnitSectId.ToString)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FDDate.ToString) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTTime.ToString) & "'"
                _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNQuantity.ToString)
                _Cmd &= vbCrLf & ",'1'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan "
            _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,FNScanQuantity,FTStateScanAuto)"
            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTPackNo"
            _Cmd &= vbCrLf & ",FNCartonNo"
            _Cmd &= vbCrLf & ",FTOrderNo"
            _Cmd &= vbCrLf & ",FTSubOrderNo"
            _Cmd &= vbCrLf & ",FTColorway"
            _Cmd &= vbCrLf & ",FTSizeBreakDown"
            _Cmd &= vbCrLf & ",0" '& Integer.Parse(x!FNHSysUnitSectId.ToString)
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
            _Cmd &= vbCrLf & ", Sum(FNScanQuantity) AS FNScanQuantity"
            _Cmd &= vbCrLf & ",'1'"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Detail"
            _Cmd &= vbCrLf & "Where  FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & " And FNCartonNo=" & _PFNCartonNo
            _Cmd &= vbCrLf & "Group by  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown,  FTBarcodeNo "

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




    Private Function CheckClosedCarton(_FTProductBarcodeNo As String) As Boolean
        Try

            Dim _Cmd As String = ""

            _Cmd = "   SELECT TOP 1 FNCartonNo "
            _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
            _Cmd &= vbCrLf & "  WHERE  (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "')  AND FDInsDate >=Convert(varchar(10),DateAdd(month,-16,GetDate()),111) "
            _Cmd &= vbCrLf & " AND FTPackNo='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "

            _PFNCartonNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "-1")

            If _PFNCartonNo < 0 Then

                Dim _oDt As DataTable
                If Me.FTStateDeleteBarcode.Checked = False Then
                    _Cmd = " SELECT Top 1 P.FNCartonNo"
                    _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                    _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                    _Cmd &= vbCrLf & " INNER JOIN"
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                    _Cmd &= vbCrLf & " B.FTBarcodeBundleNo = PD.FTBarcodeNo and P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                    _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"

                    _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                    _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "')   "
                    _Cmd &= vbCrLf & "and (Isnull(PD.FNScanQuantity,0) < P.FNPackPerCarton) "
                    _Cmd &= vbCrLf & "And (P.FNCartonNo  not in (SELECT FNCartonNo"
                    _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                    _Cmd &= vbCrLf & "WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')))"
                    _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "


                    _PFNCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "-1")))
                    If _PFNCartonNo < 0 Then

                        _Cmd = " SELECT Top 1 P.FNCartonNo"
                        _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                        _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D WITH (nolock) ON P.FTSubOrderNo = D.FTSubOrderNo AND P.FTOrderNo = D.FTOrderNo AND P.FTColorway = D.FTColorway AND P.FTSizeBreakDown = D.FTSizeBreakDown AND  B.FTOrderProdNo = D.FTOrderProdNo"
                        _Cmd &= vbCrLf & " LEFT OUTER JOIN"
                        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                        _Cmd &= vbCrLf & "   P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo"
                        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"
                        _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS SB WITH(NOLOCK) ON B.FTPOLineItemNo = SB.FTNikePOLineItem"
                        _Cmd &= vbCrLf & " and B.FTColorway = SB.FTColorway and B.FTSizeBreakDown = SB.FTSizeBreakDown and P.FTOrderNo = SB.FTOrderNo  and P.FTSubOrderNo = SB.FTSubOrderNo"

                        _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') <> '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                        _Cmd &= vbCrLf & " AND (B.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "')  AND (PD.FTPackNo IS NULL) "
                        _Cmd &= vbCrLf & "and (Isnull(PD.FNScanQuantity,0) < P.FNPackPerCarton) "
                        _Cmd &= vbCrLf & "And (P.FNCartonNo  not in (SELECT FNCartonNo"
                        _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                        _Cmd &= vbCrLf & "WHERE  (FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')))"

                        _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo ASC "
                        _PFNCartonNo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "-1")))

                    End If

                Else

                    _Cmd = " SELECT P.FNCartonNo, P.FTPackNo, B.FTBarcodeBundleNo"
                    _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER JOIN"
                    _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail As P With(NOLOCK) On B.FTColorway = P.FTColorway And B.FTSizeBreakDown = P.FTSizeBreakDown And B.FNQuantity = P.FNPackPerCarton "
                    _Cmd &= vbCrLf & " LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As PD With(NOLOCK) On P.FTSizeBreakDown = PD.FTSizeBreakDown And P.FTColorway = PD.FTColorway And P.FTSubOrderNo = PD.FTSubOrderNo And P.FTOrderNo = PD.FTOrderNo And "
                    _Cmd &= vbCrLf & "  P.FTPackNo = PD.FTPackNo And P.FNCartonNo = PD.FNCartonNo   and B.FTBarcodeBundleNo = PD.FTBarcodeNo"
                    _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS C WITH(NOLOCK) ON P.FNCartonNo = C.FNCartonNo and P.FTPackNo = C.FTPackNo"
                    _Cmd &= vbCrLf & "WHERE   Isnull(C.FTState,'') = '1' and   (P.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                    _Cmd &= vbCrLf & " AND (PD.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "')  "

                    If Not (Me.FTStateSearchAuto.Checked) Then
                        _Cmd &= vbCrLf & "And P.FNCartonNo =" & Integer.Parse(Val(_PFNCartonNo))
                    End If
                    _Cmd &= vbCrLf & "ORDER BY  P.FNCartonNo DESC "


                End If
                '20160815
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                For Each R As DataRow In _oDt.Rows
                    _PFNCartonNo = HI.UL.ULF.rpQuoted(R!FNCartonNo)
                    Exit For
                Next

            End If

        Catch ex As Exception

        End Try
    End Function


End Class

