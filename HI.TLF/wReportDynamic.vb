Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports HI.TL

Public Class wReportDynamic
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER

    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Public _KeyFiled As New List(Of HI.TL.PKFiled)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    Private _DefaultsData As New List(Of HI.TL.DefaultsData)()
    Private _CheckCopyField As New List(Of HI.TL.CopyFromFiled)()
    Private _GenAutoByField As New List(Of HI.TL.GenAutoByFiled)()

    Public _StateProcCopy As Boolean = False
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Sub New(ByVal FormName As String, ByVal Title As String, ByVal ObjId As Integer, ByVal AssemblyPath As String, ByVal tImage As String, ByVal tParentForm As Object)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Name = FormName
        Me.FormName = FormName
        Me.AssPath = AssemblyPath
        Me.Text = Title
        Me.Parent_Form = tParentForm

        _KeyFiled.Clear()
        _CheckFiled.Clear()
        _CheckDuplFiled.Clear()
        _BaseFiled.Clear()
        _CheckDelFiled.Clear()

        Me.FormObjID = ObjId
        Me.PrepareForm()

        Me.ocmsavelayuot.Visible = HI.ST.SysInfo.DevelopMode
        Me.sbCustomization.Visible = HI.ST.SysInfo.DevelopMode
        Me.ocmdeletelayout.Visible = HI.ST.SysInfo.DevelopMode
        Me.olymain.AllowCustomizationMenu = HI.ST.SysInfo.DevelopMode

        If HI.ST.SysInfo.DevelopMode Then
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        End If

        If tImage <> "" Then
            Dim tPathImgDis As String = _SystemFilePath & "\Menu\" & tImage
            If IO.File.Exists(tPathImgDis) Then
                ' Me.Icon = Icon.FromHandle(DirectCast(Image.FromFile(tPathImgDis), Bitmap).GetHicon()) 'Icon.FromHandle(hIcon)
                Me.Icon = Icon.FromHandle(DirectCast(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))), Bitmap).GetHicon())
            End If
        End If

    End Sub

    Private _ActiveLang As HI.ST.Lang.eLang = -1
    Public Property ActiveLang As HI.ST.Lang.eLang
        Get
            Return _ActiveLang
        End Get
        Set(ByVal value As HI.ST.Lang.eLang)
            _ActiveLang = value
        End Set
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(ByVal value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(ByVal value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(ByVal value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(ByVal value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(ByVal value As String)
            _MainKeyID = value
        End Set
    End Property

    Private _MainKey As String = ""
    Public Property MainKey As String
        Get
            Return _MainKey
        End Get
        Set(ByVal value As String)
            _MainKey = value
        End Set
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(ByVal value As String)
            _RequireField = value
        End Set
    End Property

    Private _Query As String = ""
    Public Property Query As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(ByVal value As Object)
            _Parent_Form = value
        End Set
    End Property

    Private _LabalWidth As Integer = 140
    Public Property LabalWidth As Integer
        Get
            Return _LabalWidth
        End Get
        Set(ByVal value As Integer)
            _LabalWidth = value
        End Set
    End Property



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

        With olymain
            .Clear()
            .Dock = DockStyle.Fill
            .OptionsCustomizationForm.ShowSaveButton = False
            .OptionsCustomizationForm.ShowLoadButton = False
        End With

        '------ Get Form Object ID-------------------
        _Str = "SELECT TOP 1 FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight,ISNULL(FNLabelFormDynamicWidth,130) AS FNLabelFormDynamicWidth  "
        _Str &= vbCrLf & "   FROM HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.FormName) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then
            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            Me.Width = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpWidth.ToString))
            Me.Height = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpHeight.ToString))
            Me.LabalWidth = Integer.Parse(Val(_dt.Rows(0)!FNLabelFormDynamicWidth.ToString))

            _SortField = _dt.Rows(0)!FTSortField.ToString

            '------ Get Form Object Gen Grid-------------------
            _Str = " EXEC SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then
                _StrQuery = "  Select TOP 1  "

                Dim _FieldName As String = ""
                For Each Row As DataRow In _dt.Select("FTType='G' ", "FNCtrlLevel,FNCtrlLevelSeq")

                    If _CtrLv <> Val(Row!FNCtrlLevel.ToString) Then

                        If _CtrLv <> -1 Then
                            _StartY = _StartY + _CtrHeight
                        End If

                        _CtrLv = Val(Row!FNCtrlLevel.ToString)

                        _StartX = 0

                    End If

                    _FieldName = Row!FTFiledName.ToString
                    If Row!FTStaNoneBase.ToString <> "Y" Then
                        If Row!FTFormControlType.ToString.ToUpper = "ButtonEdit".ToUpper And Val(Row!FNButtonEditBrwID.ToString) > 0 Then
                            Dim _SubQuery As String = HI.TL.HSysField.GetSysSubQuery(_FieldName)
                            If _SubQuery <> "" Then
                                _FieldName = "ISNULL((" & _SubQuery & "),'') AS " & _FieldName
                            End If
                        End If

                        If _ColCount = 0 Then
                            _StrQuery &= vbCrLf & "" & _FieldName
                        Else
                            _StrQuery &= vbCrLf & "," & _FieldName
                        End If

                    End If

                    If Row!FTDefaultsData.ToString <> "" Then
                        Dim _md As New HI.TL.DefaultsData
                        _md.FiledName = Row!FTFiledName.ToString
                        _md.QueryDefaults = False
                        Select Case UCase(Row!FTDefaultsData.ToString)
                            Case "@USER".ToUpper
                                _md.DataDefaults = HI.ST.UserInfo.UserName
                            Case "@DATE".ToUpper
                                _md.DataDefaults = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                            Case "@CMPID".ToUpper
                                _md.DataDefaults = HI.ST.SysInfo.CmpID.ToString
                            Case "@MaxRun".ToUpper
                                _md.DataDefaults = "SELECT MAX(" & Row!FTFiledName.ToString & ") +1 FROM  " & Me.TableName & "  WITH(NOLOCK) "
                                _md.QueryDefaults = True
                            Case Else
                                _md.DataDefaults = Row!FTDefaultsData.ToString()
                        End Select

                        _DefaultsData.Add(_md)
                    End If

                    If Row!FTStaNoneBase.ToString <> "Y" Then
                        Dim _m As New DataBaseFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _m.ControlType = Row!FTFormControlType.ToString()
                        _BaseFiled.Add(_m)
                    End If

                    If Row!FTPK.ToString = "Y" Then
                        Dim _m As New PKFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _KeyFiled.Add(_m)

                        If Me.MainKey = "" Then
                            Me.MainKey = Row!FTFiledName.ToString

                            _Str = "  SELECT        FTFiledName, FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName"
                            _Str &= vbCrLf & " FROM            HSysObjDynamic_D AS D WITH (NOLOCK)"
                            _Str &= vbCrLf & " WHERE    (LEFT(FTFiledName, LEN('" & Row!FTFiledName.ToString & "')) = '" & Row!FTFiledName.ToString & "')"
                            _Str &= vbCrLf & "  AND      (ISNULL(FTStaNoneBase, '') <> 'Y') "
                            _Str &= vbCrLf & "  AND  FTBaseName + '.' + FTPrefix + '.' + FTTableName <> '" & Me.TableName & "' "

                            Dim _dtchk As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

                            For Each R As DataRow In _dtchk.Rows
                                Dim _m2 As New CheckDelFiled
                                _m2.Query = "SELECT TOP 1 " & R!FTFiledName.ToString & " FROM  " & R!FTTableName.ToString & "  AS C WITH(NOLOCK)  WHERE " & R!FTFiledName.ToString & "="
                                _CheckDelFiled.Add(_m2)
                            Next

                        End If
                    End If

                    'If Row!FTStaCheckDup.ToString = "Y" And Row!FTValidate.ToString = "Y" Then
                    If Row!FTStaCheckDup.ToString = "Y" Then
                        Dim _m As New DuplFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _CheckDuplFiled.Add(_m)

                        If Me.RequireField = "" Then Me.RequireField = Row!FTFiledName.ToString
                    End If

                    If Row!FTValidate.ToString = "Y" And Row!FTPK.ToString <> "Y" Then
                        Dim _m As New CheckFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _CheckFiled.Add(_m)
                    End If

                    If Row!FTStateCopyNotChange.ToString = "Y" Then
                        Dim _m As New CopyFromFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _CheckCopyField.Add(_m)
                    End If

                    If Row!FTGenAutoByField.ToString <> "" Then
                        Dim _m As New GenAutoByFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _m.GenByFiledName = Row!FTGenAutoByField.ToString
                        _GenAutoByField.Add(_m)
                    End If

                    Dim emptySpaceItem As New DevExpress.XtraLayout.EmptySpaceItem
                    Dim Ctrl As New Object

                    Select Case Row!FTFormControlType.ToString.ToUpper
                        Case "TextEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.TextEdit
                            With CType(Ctrl, DevExpress.XtraEditors.TextEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                If Row!FTStaTextUpper.ToString = "Y" Then
                                    .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                End If
                                .EnterMoveNextControl = True
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)
                                '.Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                                '.Properties.AppearanceReadOnly.ForeColor = Color.Blue
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"

                            End With
                        Case "CalcEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.CalcEdit
                            With CType(Ctrl, DevExpress.XtraEditors.CalcEdit)
                                .Name = Row!FTFiledName.ToString
                                .Value = 0
                                .Properties.Precision = Val(Row!FNNumericScale.ToString)
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                .Properties.DisplayFormat.FormatType = FormatType.Numeric
                                .Properties.DisplayFormat.FormatString = "N" & Val(Row!FNNumericScale.ToString).ToString
                                .EnterMoveNextControl = True
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)
                                .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                                .Properties.AppearanceReadOnly.ForeColor = Color.Blue
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With
                        Case "MemoEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.MemoEdit
                            With CType(Ctrl, DevExpress.XtraEditors.MemoEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)

                                '.Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                                '.Properties.AppearanceReadOnly.ForeColor = Color.Blue
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With
                        Case "DateEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.DateEdit
                            With CType(Ctrl, DevExpress.XtraEditors.DateEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.AllowNullInput = DefaultBoolean.False




                                If Row!FTDateControlFormat.ToString.ToUpper = "".ToUpper Or Row!FTDateControlFormat.ToString.ToUpper = "dd/MM/yyyy".ToUpper Then
                                    .Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
                                    .Properties.DisplayFormat.FormatType = FormatType.Custom
                                    .Properties.EditFormat.FormatString = "dd/MM/yyyy"
                                    .Properties.EditFormat.FormatType = FormatType.Custom
                                    .Properties.Mask.EditMask = "dd/MM/yyyy"
                                    .Properties.ShowClear = True
                                Else
                                    .Properties.DisplayFormat.FormatString = Row!FTDateControlFormat.ToString
                                    .Properties.DisplayFormat.FormatType = FormatType.Custom
                                    .Properties.EditFormat.FormatString = Row!FTDateControlFormat.ToString
                                    .Properties.EditFormat.FormatType = FormatType.Custom
                                    .Properties.Mask.EditMask = Row!FTDateControlFormat.ToString
                                    .Properties.ShowClear = False
                                    .Properties.Buttons(0).Visible = False
                                End If


                                .EnterMoveNextControl = True
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)

                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With
                        Case "CheckEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.CheckEdit
                            With CType(Ctrl, DevExpress.XtraEditors.CheckEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.ValueChecked = "1"
                                .Properties.ValueUnchecked = "0"
                                .Properties.Caption = Row!FTFiledName.ToString
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                                .TabStop = False
                            End With
                        Case "PictureEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.PictureEdit

                            With CType(Ctrl, DevExpress.XtraEditors.PictureEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = False
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Properties.Tag = _SysPath & Row!FTFolderImgName.ToString
                                .Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
                                '.Properties.ContextMenuStrip = _ContextMenuStripPicture
                                .Tag = "2|"
                            End With

                        Case "ButtonEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.ButtonEdit
                            With CType(Ctrl, DevExpress.XtraEditors.ButtonEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.Buttons.Item(0).Tag = Row!FNButtonEditBrwID.ToString

                                If Row!FTStaTextUpper.ToString = "Y" Then
                                    .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                End If

                                .EnterMoveNextControl = True

                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)

                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With

                        Case "ComboBoxEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.ComboBoxEdit
                            With CType(Ctrl, DevExpress.XtraEditors.ComboBoxEdit)
                                .Name = Row!FTFiledName.ToString
                                .EnterMoveNextControl = True
                                .Properties.Items.Clear()

                                If Row!FTComboListName.ToString <> "" Then
                                    .Properties.Tag = Row!FTComboListName.ToString
                                    .Properties.Items.AddRange(HI.TL.CboList.SetList(Row!FTComboListName.ToString))
                                End If

                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)

                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With

                        Case Else
                            Ctrl = New DevExpress.XtraEditors.TextEdit
                            With CType(Ctrl, DevExpress.XtraEditors.TextEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                If Row!FTStaTextUpper.ToString = "Y" Then
                                    .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                End If
                                .EnterMoveNextControl = True

                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)

                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With

                    End Select

                    If Row!FTStateDFFocus.ToString = "Y" Then
                        Me.ObjectFocus = Ctrl
                    End If

                    Dim _obj As New DevExpress.XtraLayout.LayoutControlItem

                    With _obj
                        '.Parent = Me.olymain.Root
                        .Control = Ctrl
                        .CustomizationFormText = Row!FTFiledName.ToString
                        .Location = New System.Drawing.Point(_StartX, _StartY)
                        .Name = Row!FTFiledName.ToString
                        ' .Padding = New DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5)
                        ' .Size = New System.Drawing.Size(305, 30)
                        .Size = New System.Drawing.Size(Val(Row!FNCtrlWidth.ToString), Val(Row!FNCtrlHeight.ToString))
                        .Text = Row!FTFiledName.ToString
                        '.TextSize = New System.Drawing.Size(65, 13)
                        .Padding = New DevExpress.XtraLayout.Utils.Padding(2)
                        .TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
                        .TextSize = New Size(Me.LabalWidth, 13)
                        .TextVisible = Not (Row!FTFormControlType.ToString.ToUpper = "CheckEdit".ToUpper)
                        ' .AppearanceItemCaption.TextOptions.WordWrap = WordWrap.Wrap

                        If Row!FTState.ToString = "N" Then
                            .ShowInCustomizationForm = False
                            .HideToCustomization()
                        Else
                            If Row!FTValidate.ToString = "Y" Then
                                .AppearanceItemCaption.ForeColor = Color.Blue
                                .AllowHide = False
                            Else
                                .ShowInCustomizationForm = True
                                .AllowHide = True
                            End If
                        End If

                        If Row!FTFormControlType.ToString.ToUpper = "MemoEdit".ToUpper Then
                            .AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
                        Else
                            .AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                        End If

                    End With


                    Me.lyg.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {_obj})
                    If Row!FTState.ToString = "N" Then
                        Me.lyg.Item(Me.lyg.Items.Count - 1).HideToCustomization()
                    End If
                    ' End If

                    _ColCount = _ColCount + 1

                    _StartX = _StartX + Val(Row!FNCtrlWidth.ToString)
                    _CtrHeight = Val(Row!FNCtrlHeight.ToString)

                Next

                _StrQuery &= vbCrLf & " FROM   " & Me.TableName & " As M WITH(NOLOCK) "
            End If

        End If

        Me.Query = _StrQuery


        _Str = " SELECT  TOP 1   FTLayOutStream,FNLabelFormDynamicWidth  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReportLayOut With(NOLOCK) WHERE FTFormName ='" & HI.UL.ULF.rpQuoted(Me.Name.ToString) & "' "
        Dim _dtLayOut As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dtLayOut.Rows.Count > 0 Then
            Try
                olymain.RestoreLayoutFromStream(New MemoryStream(DirectCast(_dtLayOut.Rows(0)!FTLayOutStream, Byte())))
            Catch ex As Exception
            End Try

            If Integer.Parse(Val(_dtLayOut.Rows(0)!FNLabelFormDynamicWidth.ToString)) <> Me.LabalWidth Then
                Call SetLabelSize(Me, Me.LabalWidth)
            End If
        End If

        '  Call SetNewTextSize(Me)
        _dtLayOut.Dispose()
        'End If

    End Sub

    Private Sub SetLabelSize(ByVal Obj As Object, ByVal NewSize As Integer)

        For Each pObj As Object In Obj.Controls
            If TypeOf pObj Is DevExpress.XtraLayout.LayoutControl Then
                For Each ObjItem As DevExpress.XtraLayout.LayoutControlItem In CType(pObj, DevExpress.XtraLayout.LayoutControl).Root.Items
                    Select Case True
                        Case TypeOf ObjItem Is DevExpress.XtraLayout.LayoutControlItem
                            With CType(ObjItem, DevExpress.XtraLayout.LayoutControlItem)
                                .TextSize = New Size(NewSize, 13)
                            End With

                    End Select
                Next

            End If

            If pObj.Controls.Count > 0 Then

                SetLabelSize(pObj, NewSize)

            End If
        Next

    End Sub

    Public Sub DefaultsData()
        Dim _FieldName As String

        For I As Integer = 0 To _DefaultsData.ToArray.Count - 1
            _FieldName = _DefaultsData(I).FiledName.ToString

            Dim Pass As Boolean = True
            Dim _Default As String = ""
            _Default = _DefaultsData(I).DataDefaults.ToString

            If (_DefaultsData(I).QueryDefaults) Then
                _Default = HI.Conn.SQLConn.GetField(_Default, _DBEnum, "1")
            End If

            For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Text = _Default
                        End With
                    Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            .Value = Val(_Default)

                        End With
                    Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            .SelectedIndex = Val(_Default)
                        End With
                    Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                            .Checked = (_Default = "1")
                        End With
                    Case "DevExpress.XtraEditors.DateEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            .Text = _Default
                        End With

                    Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                            .Text = _Default
                        End With
                    Case "DevExpress.XtraEditors.TextEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.TextEdit)
                            .Text = _Default
                        End With
                    Case Else
                End Select
            Next
        Next


    End Sub

#Region "MAIN PROC"

#End Region

#Region "Proc"



#End Region

    Private Sub wAddEditDynamic_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If Not (Me.ObjectFocus Is Nothing) Then
            Me.ObjectFocus.Focus()
            Me.ObjectFocus.SelectAll()
        End If



    End Sub

    Private Sub wDynamicMaster_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub wAddEditDynamic_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub sbCustomization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sbCustomization.Click
        Me.olymain.ShowCustomizationForm()
    End Sub

    Private Sub ocmsavelayuot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsavelayuot.Click

        Dim _Str As String = ""

        If (HI.ST.SysInfo.DevelopMode) Then

            'FTLayOutStream
            _Str = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReportLayOut  With(NOLOCK) WHERE FTFormName ='" & HI.UL.ULF.rpQuoted(Me.Name.ToString) & "' "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()

                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReportLayOut (FTFormName, FTLayOutStream,FNLabelFormDynamicWidth) VALUES(@FNGrpObjID,@FTLayOutStream,@FNWidth)"
                Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.Name.ToString)

                Dim ms As New MemoryStream()
                olymain.SaveLayoutToStream(ms)

                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.VarBinary)
                p.Value = data

                Dim p2 As New SqlParameter("@FNWidth", SqlDbType.Int)
                p2.Value = Me.LabalWidth

                cmd.Parameters.Add(p)
                cmd.Parameters.Add(p2)
                cmd.ExecuteNonQuery()

                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

            Else

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReportLayOut  SET FTLayOutStream=@FTLayOutStream,FNLabelFormDynamicWidth=@FNWidth WHERE  FTFormName=@FNGrpObjID "
                Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.Name.ToString)

                Dim ms As New MemoryStream()
                olymain.SaveLayoutToStream(ms)

                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.VarBinary)
                p.Value = data

                Dim p2 As New SqlParameter("@FNWidth", SqlDbType.Int)
                p2.Value = Me.LabalWidth

                cmd.Parameters.Add(p)
                cmd.Parameters.Add(p2)
                cmd.ExecuteNonQuery()

                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

            End If

        Else
            Me.olymain.SaveLayoutToXml(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
        End If

        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReport SET FNFormWidth=" & Me.Width & ",FNFormHeight=" & Me.Height & ""
        _Str &= " WHERE FTFormName='" & HI.UL.ULF.rpQuoted(Me.Name.ToString) & "' "
        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        MsgBox("Save Layout Complete ....")

    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Function VerrifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""

        For I As Integer = 0 To _CheckFiled.ToArray.Count - 1
            _FieldName = _CheckFiled(I).FiledName.ToString

            Dim Pass As Boolean = True

            For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                With DirectCast(Obj.Parent, DevExpress.XtraLayout.LayoutControl)
                    Dim _lay As Object = .Items.FindByName(_FieldName)
                    If Not (_lay Is Nothing) Then
                        _Caption = _lay.Text
                    End If
                End With

                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                Pass = False
                            End If
                        End With
                    Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            If Val(.Value.ToString) <= 0 Then
                                Pass = False
                            End If
                        End With
                    Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            If .SelectedIndex < 0 Then Pass = False
                        End With
                    Case "DevExpress.XtraEditors.CheckEdit".ToUpper

                    Case "DevExpress.XtraEditors.DateEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                Pass = False
                            End If
                        End With
                    Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            If .Image Is Nothing Then
                                Pass = False
                            End If
                        End With
                    Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
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

        Return True
    End Function

    Private Sub ocmdeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdeletelayout.Click

        If (HI.ST.SysInfo.DevelopMode) Then
            Dim _Str As String = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysReportLayOut WHERE FTFormName ='" & HI.UL.ULF.rpQuoted(Me.Name.ToString) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            MsgBox("Delete Complete....")
        Else
            If File.Exists(Application.StartupPath & "\LayoutReportForm\" & Me.Name.ToString & ".XML") Then
                File.Delete(Application.StartupPath & "\LayoutReportForm\" & Me.Name.ToString & ".XML")
                MsgBox("Delete Complete....")
            End If
        End If

    End Sub

    Private Sub ocmpreview_Click(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click

    End Sub

End Class