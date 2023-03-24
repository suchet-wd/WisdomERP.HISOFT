Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports System.IO

Public Class wScanCartonRepack

    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
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

    Private _ScanPoup As wScanCartonPopUp
    Private _PDtPack As DataTable

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
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenPackCarton.Name.ToString.Trim, _GenPackCarton)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ScanPoup.Name.ToString.Trim, _ScanPoup)
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

            tPathImgDis = _SystemFilePath & "\Pack\Barcode-icon.jpg"
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
        For Each R As DataRow In _dt.Rows
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

            _Cmd = "Select Top 1  FTPORef "
            _Cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTOrderNo='" & _PFTOrderNo & "'"

            Me.FTPORef.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")

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



        ' Call InitNodeCarton(Me.otlpack, Nothing)
        Call CreateTreeCarton()
        ' Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)
        ' Call _LoadDataScan()
        'Call SetStateScan()

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

    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs)
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

                    _Qry &= vbCrLf & " , ISNULL(("

                    _Qry &= vbCrLf & " SELECT TOP 1 FTPackNo"
                    _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack AS AX WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE AX.FTPackNo= A.FTPackNo"
                    _Qry &= vbCrLf & "  AND AX.FNCartonNo= A.FNCartonNo"
                    _Qry &= vbCrLf & " ),'') AS FTStateRepack "

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
                    _Qry &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS CC WITH(NOLOCK) ON A.FNCartonNo = CC.FNCartonNo and A.FTPackNo = CC.FTPackNo "

                    _Qry &= vbCrLf & "   WHERE  A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' and CC.FTState = '1'"
                    _Qry &= vbCrLf & "   GROUP BY  A.FTPackNo, A.FNCartonNo,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"
                    _Qry &= vbCrLf & "   ORDER BY A.FNCartonNo"

                    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                    For Each R As DataRow In dt.Rows
                        _Lst.OptionsView.ShowCheckBoxes = True
                        nodeChild = _Lst.AppendNode(New Object() {Me.FNCartonNo2_lbl.Text & "" & R!FNCartonNo.ToString & " (" & R!FTCartonInfo.ToString & ")", R!FNCartonNo.ToString, R!FNQuantity.ToString, R!FNNetWeight.ToString, R!FNHSysCartonId.ToString, R!FTCartonCode.ToString, R!FNWeight.ToString, R!FNPackCartonSubType.ToString, R!FNPackPerCarton.ToString, R!FTBarCodeCarton.ToString}, node)
                        nodeChild.HasChildren = False

                        If R!FTStateRepack.ToString <> "" Then
                            nodeChild.CheckState = CheckState.Checked
                        Else
                            nodeChild.CheckState = CheckState.Unchecked
                        End If

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
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0 Then
                _Cmd = "	SELECT  distinct   FTBarcodeNo "
                _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE        (FTPackNo = N'" & Me.FTPackNo.Text & "')"
                _Cmd &= vbCrLf & "and FNCartonNo =" & Integer.Parse("0" & _CartonNo)
                _Cmd &= vbCrLf & " UNION "
                _Cmd &= vbCrLf & "	SELECT  distinct    FTBarcodeNo "
                _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan_Repack WITH(NOLOCK) "
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

    'Private Sub SetStateScan()
    '    Try
    '        Dim _Cmd As String = ""
    '        Dim _oDt As DataTable
    '        _Cmd = "  SELECT       C.FTPackNo, C.FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, "
    '        _Cmd &= vbCrLf & "      FNScanQuantity , Isnull(S.FTState,'0') as FTState"
    '        _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH (NOLOCK) LEFT OUTER JOIN "
    '        _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
    '        _Cmd &= vbCrLf & "WHERE     (C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
    '        _Cmd &= vbCrLf & "ORDER BY  C.FNCartonNo ASC "
    '        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
    '        For Each R As DataRow In _oDt.Rows
    '            otlpack.SetNodeCheckState(otlpack.FindNodeByFieldValue("FNCartonNo", (Integer.Parse("0" & R!FNCartonNo.ToString)).ToString), IIf(R!FTState.ToString = "1", CheckState.Checked, CheckState.Unchecked))
    '        Next
    '    Catch ex As Exception
    '    End Try
    'End Sub

#End Region

    Private Sub FNOrderPackType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOrderPackType.SelectedIndexChanged
        Try
            FNPackSetValue_lbl.Visible = (FNOrderPackType.SelectedIndex = 1)
            FNPackSetValue.Visible = (FNOrderPackType.SelectedIndex = 1)
            If (FNOrderPackType.SelectedIndex = 0) Then
                FNPackSetValue.Value = 0
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
                ' Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)
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


                            'Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))
                            FTProductBarcodeNo.Text = _BarcodeNo
                            If Me.FTProductBarcodeNo.Text = "" And _BarcodeNo.ToString = "" Then
                                FTProductBarcodeNo.Text = GetBarcode(FNCartonNo.Text)
                            End If

                            ' Call _LoadDataScan()
                            'Call ChkSelectUnitSect()

                        End If
                    End With
                End If
            End With
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
                _Cmd &= vbCrLf & "and  A.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
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
                _Cmd &= vbCrLf & " (SELECT        H.FTBarcodeBundleNo,  D.FTColorway, D.FTSizeBreakDown, P.FTOrderNo"
                _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS H WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle_Detail AS D WITH (NOLOCK) ON H.FTBarcodeBundleNo = D.FTBarcodeBundleNo LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS P WITH (NOLOCK) ON H.FTOrderProdNo = P.FTOrderProdNo"
                _Cmd &= vbCrLf & "Where  P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & "and D.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Cmd &= vbCrLf & "and D.FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Cmd &= vbCrLf & "  Group by    H.FTBarcodeBundleNo, D.FTColorway, D.FTSizeBreakDown, P.FTOrderNo"
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

    Private Function CheckFG(_Barcode As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select top 1 FTBarCodeCarton From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTBarCodeCarton ='" & _Barcode & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> ""
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


                If Me.FTProductBarcodeNo.Text <> "" Then
                    _StateBarCodeCarton = False

                    Dim _Qry As String = ""
                    _Qry = "SELECT TOP 1 FTPackNo "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                        _StateBarCodeCarton = True
                    End If

                    'If Me.FTStateSearchAuto.Checked = True Then
                    '    Me.FTPackNo.Text = ""
                    '    Me.FTPackNo.Properties.Tag = 0
                    'End If

                    GetPackNo(Me.FTProductBarcodeNo.Text)

                    If CheckFG(Me.FTProductBarcodeNo.Text) Then
                        HI.MG.ShowMsg.mInfo("มีการโอนงานเข้าคลังสำเร็จรูปแล้ว กรุณาตรวจสอบ !!!! ", 1703140920, Me.Text, "", MessageBoxIcon.Information)
                        Exit Sub
                    End If


                    If _StateBarCodeCarton Then
                        _Cmd = "   SELECT TOP 1 FNCartonNo "
                        _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                        _Cmd &= vbCrLf & "  WHERE  (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "')"
                        _Cmd &= vbCrLf & " AND FTPackNo='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "
                        _Cmd &= vbCrLf & " UNION "
                        _Cmd &= vbCrLf & "  SELECT  top 1   C.FNCartonNo  "
                        _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH (NOLOCK) LEFT OUTER JOIN "
                        _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
                        _Cmd &= vbCrLf & "WHERE     (C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                        _Cmd &= vbCrLf & " AND (C.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "')"
                        _PFNCartonNo = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "-1")
                    Else
                        CheckScanCarton(_PFNCartonNo, Me.FTProductBarcodeNo.Text, Me.FTStateCancelRepack.Checked)
                    End If

                    If _PFNCartonNo <= 0 Then
                        HI.MG.ShowMsg.mInfo("ข้อมูลหมายเลขกล่อง ไม่ถูกต้อง !!!", 1511020572, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                        Me.FTProductBarcodeNo.Focus()
                        Me.FTProductBarcodeNo.SelectAll()
                        Exit Sub
                    End If

                    If Me.FTStateCancelRepack.Checked = False Then
                        _Cmd = "   Select Top 1 S.FTPackNo   "
                        _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS S "
                        _Cmd &= vbCrLf & " WHERE S.FTPackNo = '" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "'"
                        _Cmd &= vbCrLf & " and S.FNCartonNo =" & CInt(_PFNCartonNo)
                        If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

                            HI.MG.ShowMsg.mInfo("กล่องนี้ยังไม่ได้ทำการ Scan ท้าย ไลน์ ไม่สามารถทำการ Scan ปิดกล่องได้ !!!", 1511020574, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub

                        End If
                    Else
                        _Cmd = "   Select Top 1 S.FTPackNo   "
                        _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack AS S "
                        _Cmd &= vbCrLf & " WHERE S.FTPackNo = '" & HI.UL.ULF.rpQuoted(FTPackNo.Text.Trim()) & "'"
                        _Cmd &= vbCrLf & " and S.FNCartonNo =" & CInt(_PFNCartonNo)
                        If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

                            HI.MG.ShowMsg.mInfo("กล่องนี้ยังไม่ได้ทำการ Re - Pack ไม่สามารถทำการยกเลิกได้ !!!", 1511888574, Me.Text, _PFNCartonNo.ToString, MessageBoxIcon.Warning)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub

                        End If
                    End If
                    

                    If Me.FTStateCancelRepack.Checked = False Then
                        Dim _StatePass As Boolean = True
                        If _StatePass Then

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
                        If DeleteBarcode(Me.FTPackNo.Text, Me.FTProductBarcodeNo.Text, _PFNCartonNo) Then
                            SetNewCarton(False)
                        Else
                            SetNewCarton(False, False)
                        End If
                    End If
                    GetPackNo(Me.FTProductBarcodeNo.Text)
                End If
                ' SetNewCarton()
                Me.FTProductBarcodeNo.Focus()
                Me.FTProductBarcodeNo.SelectAll()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function ScanCarton(ByVal _CartanNo As String) As Boolean

        Dim _Cmd As String = ""
        Dim _StateNew As Boolean = True
        _Cmd = "SELECT TOP 1 FTPackNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack  WITH(NOLOCK)"
        _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
        _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

        _StateNew = (HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "")

        If (_StateNew) Then
            Try

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack("
                    _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    _Cmd &= vbCrLf & ", FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,"
                    _Cmd &= vbCrLf & " FNScanQuantity, FTStateScanAuto, FTRepackDate, FTRepackTime"
                    _Cmd &= vbCrLf & ")"
                    _Cmd &= vbCrLf & " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    _Cmd &= vbCrLf & ", FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,"
                    _Cmd &= vbCrLf & " FNScanQuantity, FTStateScanAuto, " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK)"
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack_Detail("
                    _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    _Cmd &= vbCrLf & ", FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId"
                    _Cmd &= vbCrLf & ", FTBarcodeNo, FDScanDate,FDScanTime, FNScanQuantity, FTStateScanAuto, FTRepackDate, FTRepackTime"
                    _Cmd &= vbCrLf & ")"
                    _Cmd &= vbCrLf & " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTPackNo"
                    _Cmd &= vbCrLf & "  , FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, FDScanDate, "
                    _Cmd &= vbCrLf & "   FDScanTime, FNScanQuantity, FTStateScanAuto, " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK)"
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = " Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton "
                    _Cmd &= vbCrLf & "Set FTState = '0'"
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartanNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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
        End If

        Return True

    End Function

    Private Function GetPackNo(_FTBarcodeNo As String) As String
        Try
            Dim _PackNo As String = ""
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _oDtBarCodeCarton As DataTable

            If Me.FTPackNo.Text = "" Then
                _Cmd = "   SELECT FNCartonNo "
                _Cmd &= vbCrLf & " , FTPackNo"
                _Cmd &= vbCrLf & " , FTBarCodeCarton"
                _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                _Cmd &= vbCrLf & "  WHERE  (FTBarCodeCarton = N'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')"
                _Cmd &= vbCrLf & "UNION "
                _Cmd &= vbCrLf & "  SELECT  top 1   C.FNCartonNo  ,  C.FTPackNo , C.FTBarcodeNo"
                _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH (NOLOCK) LEFT OUTER JOIN "
                _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
                _Cmd &= vbCrLf & "WHERE     (C.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(Me.FTProductBarcodeNo.Text) & "')"
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
                    _Cmd &= vbCrLf & "UNION "
                    _Cmd &= vbCrLf & "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack WITH(NOLOCK) "
                    _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                    _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)


                    For Each R As DataRow In _oDt.Rows

                        If Me.FTStateCancelRepack.Checked = False Then
                            Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                            _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                        End If
                    Next
                Else
                    _Cmd = "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "'"
                    _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"
                    _Cmd &= vbCrLf & "UNION "
                    _Cmd &= vbCrLf & "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack WITH(NOLOCK) "
                    _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                    _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"


                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    For Each R As DataRow In _oDt.Rows
                        If Me.FTPackNo.Text = "" Then
                            Me.FTPackNo.Text = R!FTPackNo.ToString
                        End If
                        If Me.FTStateCancelRepack.Checked = False Then
                            Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                            _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                        End If
                    Next
                End If

                Dim _Qry As String = ""
                _Qry = "SELECT TOP 1 FTPackNo "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  (FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    _StateBarCodeCarton = True
                End If
            Else
                _Cmd = "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"
                _Cmd &= vbCrLf & "UNION "
                _Cmd &= vbCrLf & "Select Sum(FNScanQuantity) AS FNScanQuantity,FTPackNo ,  FNCartonNo"
                _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack WITH(NOLOCK) "
                _Cmd &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                _Cmd &= vbCrLf & " and FNCartonNo=" & _PFNCartonNo
                _Cmd &= vbCrLf & "Group by FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo"

                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDt.Rows
                    If Me.FTPackNo.Text = "" Then
                        Me.FTPackNo.Text = R!FTPackNo.ToString
                    End If
                    If Me.FTStateCancelRepack.Checked = False Then
                        Me.lblQtyScan.Text = R!FNScanQuantity.ToString
                        _PFNCartonNo = CInt("0" & R!FNCartonNo.ToString)
                    End If
                Next
            End If



            Return _PackNo
        Catch ex As Exception
        End Try
    End Function

    Private Function CheckScanCarton(ByVal _CartonNo As String, ByVal _FTBarcodeNo As String, Optional _StateDelete As Boolean = False) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As New DataTable


            If _StateDelete Then
                _Cmd = "  SELECT       C.FTPackNo, C.FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, "
                _Cmd &= vbCrLf & "      FNScanQuantity , Isnull(S.FTState,'0') as FTState"
                _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack AS C  WITH (NOLOCK) LEFT OUTER JOIN "
                _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
                _Cmd &= vbCrLf & "WHERE     (C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                _Cmd &= vbCrLf & " AND (C.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')"
                If Not (Me.FTStateSearchAuto.Checked) Then
                    _Cmd &= vbCrLf & "And C.FNCartonNo =" & Integer.Parse(_CartonNo)
                End If
                _Cmd &= vbCrLf & "ORDER BY  C.FNCartonNo ASC "
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDt.Rows
                    _PFNCartonNo = HI.UL.ULF.rpQuoted(R!FNCartonNo)
                    Exit For
                Next
            Else
                _Cmd = "  SELECT       C.FTPackNo, C.FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, "
                _Cmd &= vbCrLf & "      FNScanQuantity , Isnull(S.FTState,'0') as FTState"
                _Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS C  WITH (NOLOCK) LEFT OUTER JOIN "
                _Cmd &= vbCrLf & " 		[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS S WITH (NOLOCK) ON C.FTPackNo = S.FTPackNo and C.FNCartonNo = S.FNCartonNo"
                _Cmd &= vbCrLf & "WHERE     (C.FTPackNo = '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "') "
                _Cmd &= vbCrLf & " AND (C.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "')"
                If Not (Me.FTStateSearchAuto.Checked) Then
                    _Cmd &= vbCrLf & "And C.FNCartonNo =" & Integer.Parse(_CartonNo)
                End If
                _Cmd &= vbCrLf & "ORDER BY  C.FNCartonNo ASC "
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                For Each R As DataRow In _oDt.Rows
                    _PFNCartonNo = HI.UL.ULF.rpQuoted(R!FNCartonNo)
                    Exit For
                Next
            End If
           
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
            Catch ex As Exception
                lblQtyScan.Text = 0
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

        Catch ex As Exception
        End Try
    End Sub

    Private Function DeleteBarcode(ByVal _PackNo As String, ByVal _BarcodeNo As String, ByVal _CartonNo As Integer) As Boolean
        Dim _Cmd As String = ""
        Dim _StateNew As Boolean = True
        _Cmd = "SELECT TOP 1 FTPackNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack WITH(NOLOCK)"
        _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
        _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)

        _StateNew = (HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") <> "")

        If (_StateNew) Then
            Try

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan("
                    _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    _Cmd &= vbCrLf & ", FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,"
                    _Cmd &= vbCrLf & " FNScanQuantity, FTStateScanAuto"
                    _Cmd &= vbCrLf & ")"
                    _Cmd &= vbCrLf & " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    _Cmd &= vbCrLf & ", FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo,"
                    _Cmd &= vbCrLf & " FNScanQuantity, FTStateScanAuto"
                    _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack WITH(NOLOCK)"
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail("
                    _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    _Cmd &= vbCrLf & ", FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId"
                    _Cmd &= vbCrLf & ", FTBarcodeNo, FDScanDate,FDScanTime, FNScanQuantity, FTStateScanAuto"
                    _Cmd &= vbCrLf & ")"
                    _Cmd &= vbCrLf & " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTPackNo"
                    _Cmd &= vbCrLf & "  , FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId, FTBarcodeNo, FDScanDate, "
                    _Cmd &= vbCrLf & "   FDScanTime, FNScanQuantity, FTStateScanAuto"
                    _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack_Detail WITH(NOLOCK)"
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack_Detail "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack "
                    _Cmd &= vbCrLf & "WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Cmd &= vbCrLf & " AND FNCartonNo=" & CInt(_CartonNo)

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
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End Try
        End If

        Return True
    End Function

   
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
                    _Qry = " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
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
                _Qry = " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
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
            'Call _LoadDataScan()
            otlpack_Click(otlpack, New EventArgs)
        Catch ex As Exception
        End Try
    End Sub

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

    Private Sub FTPackNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPackNo.EditValueChanged

    End Sub
End Class

