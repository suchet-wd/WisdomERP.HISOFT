Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraTreeList

Imports System.Runtime.InteropServices
Imports System.IO
'Imports OnBarcode.Barcode

Public Class wPackOrderGenerateBarcodeCarton

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

    Private _Barcodecarton As wPopUpBarcodeCarton

    Private _PFTOrderNo As String = ""
    Private _PFTSubOrderNo As String = ""
    Private _PFTColorway As String = ""
    Private _PFTSizeBreakDown As String = ""
    Private _FTPackOfOrderNo As String = ""
    Private _PDtCartonCount As DataTable

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ListPackOrderNo = New wListPackOrderNo
        HI.TL.HandlerControl.AddHandlerObj(_ListPackOrderNo)

        _Barcodecarton = New wPopUpBarcodeCarton
        HI.TL.HandlerControl.AddHandlerObj(_Barcodecarton)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListPackOrderNo.Name.ToString.Trim, _ListPackOrderNo)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Barcodecarton.Name.ToString.Trim, _Barcodecarton)
        Catch ex As Exception
        Finally
        End Try

        Me.PrepareForm()
        Call InitGrid()
        Call TabChenge()
     

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


    Private Sub TabChenge()
        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub InitGrid()
        With ogvppercarton
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub

    Private Sub ClearGrid()

       

    End Sub



    Private Sub LoadrderPackBreakDownCarton(Key As Object, CartonNo As Integer)
        Dim _dt As DataTable
        Dim _dtpack As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackBreakDown_Carton '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'," & CartonNo & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _dtpack = _dt.Copy

        For Each R As DataRow In _dt.Select("Total > 0")
            _PFTOrderNo = R!FTOrderNo.ToString
            _PFTSubOrderNo = R!FTSubOrderNo.ToString
            _PFTColorway = R!FTColorway.ToString


        Next



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
                                If Col.ColumnName.ToString.ToUpper = "FNHSysStyleId_Hide".ToUpper Then
                                    .Visible = False
                                Else

                                    .Visible = True
                                End If

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


        Call LoadOrderPackDetail(Key.ToString)

        Call InitNodeCarton(Me.otlpack, Nothing)
        Call CreateTreeCarton()
        Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, 0)

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

  
    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Qry As String
            _Qry = "Delete A "
            _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A "
            _Qry &= vbCrLf & "  INNER JOIN (SELECT	  XXA.FTPackNo, XXA.FNCartonNo"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS XXA WITH(NOLOCK)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS XXB WITH(NOLOCK) ON XXA.FTPackNo =  XXB.FTPackNo AND XXA.FNCartonNo=XXB.FNCartonNo"
            _Qry &= vbCrLf & "  WHERE  XXA.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' AND XXB.FNCartonNo IS NULL"
            _Qry &= vbCrLf & "  GROUP BY XXA.FTPackNo, XXA.FNCartonNo) AS B ON A.FTPackNo = B.FTPackNo AND A.FNCartonNo = B.FNCartonNo "
            _Qry &= vbCrLf & " WHERE A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"

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
        'otbsuborder.TabPages.Clear()
        'For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
        '    Select Case HI.ENM.Control.GeTypeControl(Obj)
        '        Case ENM.Control.ControlType.ButtonEdit
        '            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
        '                .Focus()
        '            End With
        '    End Select
        'Next
    End Sub

#End Region

#Region "MAIN PROC"

    Private Function GenerateBarcodeCarton() As Boolean
        Dim _Qry As String = ""
        Dim _CartonBarCode As String = ""

        Dim _CmpH As String
        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

        _CartonBarCode = HI.TL.Document.GetDocumentNo("HITECH_PRODUCTION", "TPACKOrderPack_Carton_Barcode", "", False, _CmpH).ToString


        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,  FTPackNo, FNCartonNo, FTBarCodeCarton)"
        _Qry &= vbCrLf & "  Select "
        _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
        _Qry &= vbCrLf & ",A.FTPackNo "
        _Qry &= vbCrLf & ",A.FNCartonNo"
        _Qry &= vbCrLf & "   ,LEFT('" & _CartonBarCode & "',Len('" & _CartonBarCode & "')-5) + RIGHT('00000' +  Convert(varchar(30),Convert(Int,RIGHT('" & _CartonBarCode & "',5)) +  ((  ROW_NUMBER() Over (Order By A.FNCartonNo) )-1)),5) AS FTBarCodeCarton "
        _Qry &= vbCrLf & "  FROM ("
        _Qry &= vbCrLf & "  SELECT	  XXA.FTPackNo, XXA.FNCartonNo"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS XXA WITH(NOLOCK)"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS XXB WITH(NOLOCK) ON XXA.FTPackNo =  XXB.FTPackNo AND XXA.FNCartonNo=XXB.FNCartonNo"
        _Qry &= vbCrLf & "  WHERE  XXA.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' AND XXB.FNCartonNo IS NULL"
        _Qry &= vbCrLf & "  GROUP BY XXA.FTPackNo, XXA.FNCartonNo"
        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS B ON A.FTPackNo = B.FTPackNo AND A.FNCartonNo =B.FNCartonNo "
        _Qry &= vbCrLf & "  WHERE  A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
        If (StatePra) Then
            _Qry &= vbCrLf & "   AND A.FNCartonNo in (" & SelectCartonNO & ")"
        End If
        _Qry &= vbCrLf & "   AND B.FTBarCodeCarton IS NULL"
        _Qry &= vbCrLf & "  ORDER BY A.FNCartonNo ASC"


        Return HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

    End Function

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
        Dim _Spls As New HI.TL.SplashScreen("Generate BarodeCode Carton Please Wait....")
        Try
            Dim _Cmd As String = "" : Dim _EN13 As String = "" : Dim _CartonNO As String = ""
            Dim _oDt As System.Data.DataTable
            _oDt = _dt
            Dim _MaxCarton As Integer = 0
            '_Cmd = "SELECT   C.FTColorway, C.FTSizeBreakDown, C.FTOrderNo, C.FTPackNo,  C.FTPOLine  , C.FTSubOrderNo, PK.FTCustomerPO, C.FNCartonNo, D.FTSerialFrom , D.FTSerialTo  , D.FNFrom , D.FNTo ,C.FNQuantity"
            '_Cmd &= vbCrLf & " , convert(nvarchar(30) , convert(int ,D.FTSerialFrom ) + ROW_NUMBER() Over (partition by C.FTOrderNo , C.FTSubOrderNo, C.FTPOLine ,C.FTPackNo,C.FTColorway, C.FTSizeBreakDown ,PK.FTCustomerPO ,C.FNQuantity ORder by  C.FTPackNo,C.FNCartonNo) -1 )AS FNCartonSeq  "
            '_Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail AS C  LEFT OUTER JOIN "
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS  PK WITH(NOLOCK)    ON C.FTPackNo = PK.FTPackNo INNER JOIN    "
            '_Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. TEXPTPackPlan_D as D  ON PK.FTCustomerPO = D.FTPORef and C.FTPOLine  = convert(nvarchar(30), convert(int, D.FTPOLineNo)) "
            '_Cmd &= vbCrLf & " and C.FTSizeBreakDown = D.FTSizeBreakDown and    C.FTColorway= replace(replace(D.FTShortDescription,D.FTStyleCode,''),'-','')  and C.FNQuantity = D.FNQtyPerPack "

            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS  T WITH(NOLOCK)    ON  C.FTPackNo = T.FTPackNo AND C.FNCartonNo = T.FNCartonNo     "

            '_Cmd &= vbCrLf & " where  (T.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            _Cmd = " Delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].. TPACKOrderPack_Carton_Barcode   "
            _Cmd &= vbCrLf & " where   FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"

            _Cmd &= vbCrLf & "Select distinct    D.FNCartonNo "
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Detail D  with(nolock) "
            _Cmd &= vbCrLf & " where  D.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
            _Cmd &= vbCrLf & "Order by  D.FNCartonNo asc "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Try
                _MaxCarton = Val(_oDt.Compute("max(FNCartonNo)", "FNCartonNo> 0"))
            Catch ex As Exception

            End Try
            For Each R As DataRow In _oDt.Rows
                _Spls.UpdateInformation("Generate BarodeCode Carton " & R!FNCartonNo.ToString & " / " & _MaxCarton.ToString & "'")
                _Cmd = " exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.sp_genbarcodeucc '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'," & Val(R!FNCartonNo) & " , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Next



            '            For Each R As DataRow In _oDt.Rows


            '                _EN13 = HI.UL.ULF.rpQuoted(GenerateBarcodeSSCCEN13(R!FNCartonSeq.ToString, R!FNCartonSeq.ToString, R!FNCartonNo.ToString, R!FTPONo.ToString))
            '                '_Cmd = " Select  FTBarCodeEAN13 From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode  "
            '                '_Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
            '                '_Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
            '                '_Cmd &= vbCrLf & " and isnull(FTBarCodeEAN13,'') <>'' "
            '                'If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows.Count <= 0 Then
            '                _Cmd = "Select   *   from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TmpDataFromMecury "
            '                _Cmd &= vbCrLf & "  where  RefPO='" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString) & "'"
            '                _Cmd &= vbCrLf & "  and   POItem='" & HI.UL.ULF.rpQuoted(R!FTPOLine.ToString) & "'"
            '                _Cmd &= vbCrLf & "  and   SizeDescription='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
            '                '_Cmd &= vbCrLf & "  and   SizeDescription='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"


            '                For Each x As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT).Rows
            '                    _EN13 = x!CartonBarcode.ToString
            '                    If checkbarcodedupl(_EN13) Then

            '                        _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode"
            '                        _Cmd &= vbCrLf & "Set  FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '                        _Cmd &= vbCrLf & ",FTCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
            '                        _Cmd &= vbCrLf & ",FTBarCodeEAN13='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
            '                        _Cmd &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_EN13) & "'"
            '                        _Cmd &= vbCrLf & " where  FTPackNo='" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
            '                        _Cmd &= vbCrLf & " and   FNCartonNo='" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
            '                        If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then
            '                            _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode (FTInsUser,FDInsDate,FTUpdTime,FTCartonNo,FTBarCodeEAN13,FTBarCodeCarton,FTPackNo,FNCartonNo)"
            '                            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonSeq.ToString) & "'"
            '                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
            '                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_EN13) & "'"
            '                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNo.ToString) & "'"
            '                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FNCartonNo.ToString) & "'"
            '                            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            '                            GoTo 1
            '                        Else
            '                            GoTo 1
            '                        End If

            '                    End If
            '                Next

            '1:
            '                'End If
            '            Next


            _Spls.Close()
            Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            _Spls.Close()
            Return False
        End Try
    End Function


    Private Function checkbarcodedupl(_barcode As String) As Boolean
        Try
            Dim _cmd As String = ""
            _cmd = "select top  1  FTBarCodeEAN13  from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode "
            _cmd &= vbCrLf & " where FTBarCodeEAN13 = '" & HI.UL.ULF.rpQuoted(_barcode) & "'"

            Return HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count = 0
        Catch ex As Exception

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

    Private Sub ocmgenbarcode_Click(sender As Object, e As EventArgs) Handles ocmgenbarcodesscc.Click
        Try
            Dim _Count As Integer = _PDtCartonCount.Rows.Count
            Dim _Cmd As String = "" : Dim _Seq As Integer = 0
            Dim _oDt As DataTable

            _Cmd = "SELECT PL.FTPONo,   D.FTPackNo, D.FNCartonNo,   D.FTColorway, D.FTSizeBreakDown, D.FNQuantity, D.FNHSysCartonId, D.FNPackCartonSubType, D.FNPackPerCarton, D.FTPOLine,   P.FTCustomerPO, P.FNHSysStyleId "
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
            If Me.FNOrderPackType.SelectedIndex = 0 Then
                _Cmd &= vbCrLf & "  and  (D.FNQuantity ) =  PL.FNQtyPerPack * case when  pl.FNInnerPackCount <=0 then 1  else pl.FNInnerPackCount end  "
            Else
                _Cmd &= vbCrLf & "  and  (D.FNQuantity /" & Me.FNPackSetValue.Value & ") =  PL.FNQtyPerPack * case when  pl.FNInnerPackCount <=0 then 1  else pl.FNInnerPackCount end  "
            End If

            _Cmd &= vbCrLf & "   INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo. TEXPTPackPlan  as PD ON PL.FTPckPlanNo = PD.FTPckPlanNo AND PL.FTPORef = PD.FTPORef AND PL.FTPORefNo = PD.FTPORefNo "
            _Cmd &= vbCrLf & "WHERE  (D.FTPackNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "')"
            _Cmd &= vbCrLf & "  and PD.FTApproveState = '1' "

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
                                ' Call LoadDataInfo(Me.FTPackNo.Text)
                            End If
                        End If
                    End With
                Else
                    If Me.GenBarcodeEN13(_oDt) Then
                        HI.MG.ShowMsg.mInfo("Generate Barcode Saccess....", 1512141511, Me.Text, "", MessageBoxIcon.Information)
                        '  Call LoadDataInfo(Me.FTPackNo.Text)
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

                        End If
                    End If
                End With
            End If


            _Cmd = "exec  dbo.sp_updatebarcodeucc '" & Me.FTPackNo.Text & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Call LoadDataInfo(Me.FTPackNo.Text)

        Catch ex As Exception
        End Try
    End Sub

    Private SelectCartonNO As String = ""
    Private StatePra As Boolean = False
    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmgeneratebarcodewip.Click
        If Me.FTPackNo.Text.Trim <> "" And Me.FTPackNo.Properties.Tag.ToString <> "" Then
            '  If Me.VerrifyData Then

            Dim _Qry As String = ""
            Dim _dtcheck As DataTable
            '_Qry = "   Select TOP 1  A.FTPackNo"
            '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK) "
            '_Qry &= vbCrLf & "  WHERE   A.FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "

            _Qry &= vbCrLf & "  SELECT	  XXA.FTPackNo, XXA.FNCartonNo"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS XXA WITH(NOLOCK)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS XXB WITH(NOLOCK) ON XXA.FTPackNo =  XXB.FTPackNo AND XXA.FNCartonNo=XXB.FNCartonNo"
            _Qry &= vbCrLf & "  WHERE  XXA.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' AND XXB.FNCartonNo IS NULL"
            _Qry &= vbCrLf & "  GROUP BY XXA.FTPackNo, XXA.FNCartonNo"
            _dtcheck = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            '  If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
            If _dtcheck.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("พบข้อมูล Scan ท้ายลายน์แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1598542311, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Generate Barcode Carton ใช่หรือไม่ ?", 1505180917) = True Then

                With _Barcodecarton
                    .FNCartonNo2_lbl = Me.FNCartonNo2_lbl.Text
                    .FNCartonNo3_lbl = Me.FNCartonNo3_lbl.Text
                    .oDt = _PDtCartonCount
                    .ShowDialog()
                    If (.Poss) Then
                        SelectCartonNO = .SelectCartonNo
                        StatePra = True
                    Else
                        StatePra = False
                        Exit Sub
                    End If

                End With

                If Me.GenerateBarcodeCarton() Then
                    Call CreateTreeCarton()

                    HI.MG.ShowMsg.mInfo("Generate Barcode Carton Complete..", 1497389982, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)

                Else
                    HI.MG.ShowMsg.mInfo("Generate Barcode Carton Not Complete..", 1497389984, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            End If



        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTPackNo_lbl.Text)
            Me.FTPackNo.Focus()
        End If

    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcodeprod.Click

        If Me.FTPackNo.Text.Trim <> "" And Me.FTPackNo.Properties.Tag.ToString <> "" Then
            Dim _Qry As String = ""
            Dim _dtcheck As DataTable
            '_Qry = "   Select TOP 1  A.FTPackNo"
            '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK) "
            '_Qry &= vbCrLf & "  WHERE   A.FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "


            _Qry &= vbCrLf & "  SELECT	  XXA.FTPackNo, XXA.FNCartonNo"
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS XXA WITH(NOLOCK)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS XXB WITH(NOLOCK) ON XXA.FTPackNo =  XXB.FTPackNo AND XXA.FNCartonNo=XXB.FNCartonNo"
            _Qry &= vbCrLf & "  WHERE  XXA.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' AND XXB.FNCartonNo IS NULL"
            _Qry &= vbCrLf & "  GROUP BY XXA.FTPackNo, XXA.FNCartonNo"
            _dtcheck = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            '  If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
            If _dtcheck.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("พบข้อมูล Scan ท้ายลายน์แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1598542311, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ ลบ Barcode ใช่หรือไม่ ?", 1399890001) = True Then
                If Me.DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(ogbcarton)
                    HI.TL.HandlerControl.ClearControl(ogdCartonBarcode)
                    HI.TL.HandlerControl.ClearControl(ogbcarton)

                    Call CreateTreeCarton()
                    FTCartonBarcodeNo.Text = ""

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If

            'Else

            '    HI.MG.ShowMsg.mInfo("พบข้อมูล Scan ท้ายลายน์แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1598542311, Me.Text, , MessageBoxIcon.Warning)
            '    FTPackNo.Focus()
            '    FTPackNo.SelectAll()

            'End If
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
                .ReportName = "BarCodeCartonSlip.rpt"
                .Formular = "{TPACKOrderPack_Carton_Barcode.FTPackNo}='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "
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

    Private Sub LoadOrderPackDetail(Key As Object)
        Dim _Qry As String = ""
        Dim _dtprod As DataTable
        'otbsuborder.TabPages.Clear()

        '_Qry = "SELECT  FTSubOrderNo   "
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail AS P With(Nolock)"
        '_Qry &= vbCrLf & "  WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        '_Qry &= vbCrLf & "  Group By  FTSubOrderNo  "
        '_Qry &= vbCrLf & "  Order By  FTSubOrderNo  "

        '_dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'For Each R As DataRow In _dtprod.Rows

        '    Dim Otp As New DevExpress.XtraTab.XtraTabPage()
        '    With Otp
        '        .Name = R!FTSubOrderNo.ToString
        '        .Text = R!FTSubOrderNo.ToString
        '    End With

        '    otbsuborder.TabPages.Add(Otp)

        'Next

        'If _dtprod.Rows.Count > 0 Then
        '    otbsuborder.SelectedTabPageIndex = 0
        'End If

        '_dtprod.Dispose()

        'Call LoadOrderPackBreakDown(Key)
    End Sub

    Private Sub LoadOrderPackSubBreakDown(SubOrderNo As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_Get_OrderPackSubBreakDown '" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "','" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'With Me.ogvsubpackdetail

        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next

        '    If Not (_dt Is Nothing) Then
        '        For Each Col As DataColumn In _dt.Columns

        '            Select Case Col.ColumnName.ToString.ToUpper
        '                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
        '                Case Else
        '                    _colcount = _colcount + 1
        '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
        '                    With ColG
        '                        .Visible = True
        '                        .FieldName = Col.ColumnName.ToString
        '                        .Name = "FTSubOrderNo" & Col.ColumnName.ToString
        '                        .Caption = Col.ColumnName.ToString

        '                    End With

        '                    .Columns.Add(ColG)

        '                    With .Columns(Col.ColumnName.ToString)

        '                        .OptionsFilter.AllowAutoFilter = False
        '                        .OptionsFilter.AllowFilter = False
        '                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '                        .DisplayFormat.FormatString = "{0:n0}"
        '                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

        '                        With .OptionsColumn
        '                            .AllowMove = False
        '                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowEdit = False
        '                            .ReadOnly = True
        '                        End With

        '                    End With

        '                    .Columns(Col.ColumnName.ToString).Width = 45
        '                    .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
        '                    .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

        '            End Select

        '        Next

        '        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
        '            With GridCol
        '                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '            End With
        '        Next
        '    End If

        'End With

        'Me.ogcsubpackdetail.DataSource = _dt
    End Sub

    Private Function DeleteSubOrder(Optional StateAll As Boolean = False) As Boolean

        ' CType(ogcmark.DataSource, DataTable).AcceptChanges()

        'Dim _Qry As String = ""
        'HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        'Try

        '    _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Detail "
        '    _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(FTPackNo.Text) & "' "

        '    If Not (StateAll) Then
        '        _Qry &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.otbsuborder.SelectedTabPage.Name.ToString) & "' "
        '    End If

        '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

        '        HI.Conn.SQLConn.Tran.Rollback()
        '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '        Return False

        '    End If

        '    HI.Conn.SQLConn.Tran.Commit()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return True
        'Catch ex As Exception
        '    HI.Conn.SQLConn.Tran.Rollback()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '    Return False
        'End Try
    End Function

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
                    _Qry &= vbCrLf & "  , max(A.FNQuantity) AS FNQuantity"
                    _Qry &= vbCrLf & "   ,Max(Convert(numeric(18,3),A.FNQuantity*B.FNWeight)) AS FNNetWeight "
                    _Qry &= vbCrLf & "   ,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"

                    _Qry &= vbCrLf & " , sum(isnull(Z.FNScanQuantity,0)) AS FNScanQuantity"

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

                    _Qry &= vbCrLf & "       LEFT OUTER JOIN ( SELECT     FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId"
                    _Qry &= vbCrLf & ",   sum(FNScanQuantity) AS FNScanQuantity"
                    _Qry &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan WITH(NOLOCK) "
                    _Qry &= vbCrLf & "group by  FTPackNo, FNCartonNo, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNHSysUnitSectId ) AS Z ON A.FTPackNo = Z.FTPackNo"
                    _Qry &= vbCrLf & "and A.FNCartonNo = Z.FNCartonNo and A.FTOrderNo = Z.FTOrderNo and A.FTSubOrderNo = Z.FTSubOrderNo and A.FTColorway = Z.FTColorway"
                    _Qry &= vbCrLf & "and A.FTSizeBreakDown = Z.FTSizeBreakDown"


                    _Qry &= vbCrLf & "   WHERE  A.FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "'"
                    _Qry &= vbCrLf & "   GROUP BY  A.FTPackNo, A.FNCartonNo,A.FNHSysCartonId,CT.FTCartonCode ,CT.FNWeight ,A.FNPackCartonSubType,A.FNPackPerCarton"
                    _Qry &= vbCrLf & "   ORDER BY A.FNCartonNo"

                    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    _PDtCartonCount = dt
                    For Each R As DataRow In dt.Rows

                        nodeChild = _Lst.AppendNode(New Object() {Me.FNCartonNo2_lbl.Text & "" & R!FNCartonNo.ToString & " (" & R!FTCartonInfo.ToString & ")", R!FNCartonNo.ToString, R!FNQuantity.ToString, R!FNNetWeight.ToString, R!FNHSysCartonId.ToString, R!FTCartonCode.ToString, R!FNWeight.ToString, R!FNPackCartonSubType.ToString, R!FNPackPerCarton.ToString, R!FNScanQuantity.ToString, R!FTBarCodeCarton.ToString}, node)
                        nodeChild.HasChildren = False

                        _QtyCarton = Integer.Parse(Val(R!FNQuantity))
                        _PackPerQty = Integer.Parse(Val(R!FNPackPerCarton))

                        Select Case True
                            Case (CDbl(R!FNScanQuantity) = CDbl(R!FNQuantity))
                                If _QtyCarton = _PackPerQty Then
                                    nodeChild.ImageIndex = 1
                                    nodeChild.SelectImageIndex = 1

                                Else
                                    nodeChild.ImageIndex = 8
                                    nodeChild.SelectImageIndex = 8
                                End If
                            Case Else
                                If _QtyCarton = _PackPerQty Then
                                    nodeChild.ImageIndex = 4
                                    nodeChild.SelectImageIndex = 4
                                Else
                                    nodeChild.ImageIndex = 6
                                    nodeChild.SelectImageIndex = 6
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
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub wCreatePackOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call InitGrid()
            Me.FTCartonBarcodeNo.EnterMoveNextControl = False

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged
        Call TabChenge()

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

    Private Sub ocmsaveweightpack_Click(sender As Object, e As EventArgs)
    End Sub

    Private _PFNCartonNo As Integer = 0

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
                            Dim _BarcodeNo As String = .GetValue(10).ToString

                            _PFNCartonNo = Integer.Parse(Val(_FNCartonNo))

                            FNHSysCartonId.Text = _FTCartonCode
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                            FNCTNW.Value = Val(_FNWeight)
                            FNNW.Value = Val(_FNNetWeight)
                            FNGW.Value = Val(_FNWeight) + Val(_FNNetWeight)


                            FNCartonNo.Text = _FNCartonNo

                            Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))
                          
                            FTCartonBarcodeNo.Text = _BarcodeNo

                            FTCartonBarcodeNo.Focus()
                            FTCartonBarcodeNo.SelectAll()

                        End If
                    End With
                End If
            End With


        Catch ex As Exception
        End Try
    End Sub

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
            _FTPackOfOrderNo = ""

            If Me.FTPackNo.Text = "" Then

                _Qry = "  SELECT P.FTPackNo, dbo.fn_Pack_OrderNo(P.FTPackNo) AS FTOrderNo"
                _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS P WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE ( P.FTPackNo  IN  ("
                _Qry &= vbCrLf & " SELECT DISTINCT FTPackNo"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS X WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text) & "'"
                _Qry &= vbCrLf & " ))"
                _Qry &= vbCrLf & " ORDER BY  P.FTPackNo"

                _dtPackNo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

                If _dtPackNo.Rows.Count > 0 Then
                    If _dtPackNo.Rows.Count > 1 Then
                        With _ListPackOrderNo
                            .ogclist.DataSource = _dtPackNo.Copy
                            .OrderNo = ""
                            .PackOrderNo = ""
                            .ShowDialog()

                            _PackNo = .PackOrderNo
                            _FTPackOfOrderNo = .OrderNo

                        End With
                    Else

                        _PackNo = _dtPackNo.Rows(0)!FTPackNo.ToString
                        _FTPackOfOrderNo = _dtPackNo.Rows(0)!FTOrderNo.ToString

                    End If

                End If

            End If

            Return _PackNo
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTCartonBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                Dim _FTOrderNo As String = ""
                Dim _FTColorWay As String = ""
                Dim _FTSizeBreakDown As String = ""
                Dim _FTSubOrderNo As String = ""
                Dim _CartonQty As Integer = 0
                Dim _FTProductBarcodeNo As String = ""
                'Get PackNo
                _FTPackOfOrderNo = ""

                _PFNCartonNo = FNCartonNo.Value

                If Me.FTPackNo.Text = "" Then
                    Me.FTPackNo.Text = GetPackNo(Me.FTCartonBarcodeNo.Text)

                    Exit Sub
                End If

                _FTProductBarcodeNo = FTCartonBarcodeNo.Text

                If _FTProductBarcodeNo = "" Then
                    'HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ลูกค้า หรืออาจยังไม่ได้ทำการ Scan เข้าไลน์ !!!", 1412100101, Me.Text, , MessageBoxIcon.Warning)
                    Me.FTCartonBarcodeNo.Focus()
                    Me.FTCartonBarcodeNo.SelectAll()
                    Exit Sub
                End If


                Dim _Qry As String = ""

                _Qry = ""
                _Qry = "SELECT TOP 1 FTBarCodeCarton FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTPackNo<>'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                _Qry &= vbCrLf & " AND FNCartonNo<>" & Integer.Parse(Val(_PFNCartonNo)) & " "
                _Qry &= vbCrLf & " AND FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text.Trim) & "'"

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    HI.MG.ShowMsg.mInfo("Barcodeouh ถูกนำไปผูกกับกล่องอื่นแล้ว !!!", 1598542781, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                _Qry = "   Select TOP 1  A.FTPackNo"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS A WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE   A.FTPackNo ='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                _Qry &= vbCrLf & "  WHERE   A.FNCartonNo=" & Integer.Parse(Val(_PFNCartonNo)) & ""

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    HI.MG.ShowMsg.mInfo("พบข้อมูล Scan ท้ายลายน์แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1598542311, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If Me.FTCartonBarcodeNo.Text <> "" And Integer.Parse(Val(_PFNCartonNo)) > 0 Then

                    _Qry = ""
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode "
                    _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text.Trim) & "'"
                    _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FNCartonNo=" & Integer.Parse(Val(_PFNCartonNo)) & " "

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD) = False Then
                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode "
                        _Qry &= vbCrLf & " (FTInsUser,FDInsDate,FTInsTime,FTPackNo,FNCartonNo,FTBarCodeCarton)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                        _Qry &= vbCrLf & ",FNCartonNo=" & Integer.Parse(Val(_PFNCartonNo)) & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTCartonBarcodeNo.Text.Trim) & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    End If

                    Dim _TmpFNCartonNo As Integer = -1

                    _Qry = " SELECT TOP 1 FNCartonNo "
                    _Qry = " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTPackNo='" & HI.UL.ULF.rpQuoted(Me.FTPackNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FNCartonNo>" & Integer.Parse(Val(_PFNCartonNo)) & " "
                    _Qry &= vbCrLf & " ORDER BY FNCartonNo ASC "

                    _TmpFNCartonNo = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, -1))

                    If _TmpFNCartonNo > 0 Then
                        _PFNCartonNo = _TmpFNCartonNo
                    End If

                    Call CreateTreeCarton()

                    Me.SetNewCarton()

                End If


                Me.FTCartonBarcodeNo.Focus()
                Me.FTCartonBarcodeNo.SelectAll()

            End If
        Catch ex As Exception

        End Try

    End Sub



    Private Function New_VerrifyData() As Boolean
        Try
           

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


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
                Dim _BarcodeNo As String = .GetValue(10).ToString

                _PFNCartonNo = Integer.Parse(Val(_FNCartonNo))

                FNHSysCartonId.Text = _FTCartonCode
                FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                FNPackCartonSubType.SelectedIndex = Val(_FNPackCartonSubType)
                FNNW.Value = Val(_FNWeight)
                FNCartonNo.Text = _FNCartonNo

                Call LoadrderPackBreakDownCarton(Me.FTPackNo.Text, Val(_FNCartonNo))
              
                FTCartonBarcodeNo.Text = _BarcodeNo

                FTCartonBarcodeNo.Focus()
                FTCartonBarcodeNo.SelectAll()

            End With

            ' otlpack_Click(otlpack, New EventArgs)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTProductBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTCartonBarcodeNo.EditValueChanged

    End Sub

  
    Private Sub otlpack_NodeCellStyle(sender As Object, e As GetCustomNodeCellStyleEventArgs) Handles otlpack.NodeCellStyle
        Try
            Select Case e.Column.FieldName.ToString
                Case "FTCartonName"
                    If e.Node.GetValue(10).ToString <> "" Then
                        'e.Appearance.BackColor = Color.FromArgb(80, 255, 0, 255)
                        'e.Appearance.ForeColor = Color.White
                        'e.Appearance.Font = New Font(e.Appearance.Font, FontStyle.Bold)

                        e.Appearance.ForeColor = Color.Blue
                    End If

            End Select
        Catch ex As Exception

        End Try

        
    End Sub

   
End Class