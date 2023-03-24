﻿Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports HI.ST

Public Class WHRawMatLocationAddEdit

#Region "variable"

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Public _KeyFiled As New List(Of HI.TL.PKFiled)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    Private _DefaultsData As New List(Of HI.TL.DefaultsData)()
    Private _TypeEmp As String = ""
    Private _Payyaer As String = ""
    Private _DataInfo As DataTable
    Private _SysPathImge As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _ProcNew As Boolean = False

#End Region

    Sub New(ByVal FormName As String, ByVal Title As String, ByVal ObjId As Integer, ByVal AssemblyPath As String, ByVal tImage As String, ByVal tParentForm As Object)
        _ProcNew = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Name = FormName & "AddEditPopup"
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
            Dim tPathImgDis As String = _SysPathImge & "\Menu\" & tImage
            If IO.File.Exists(tPathImgDis) Then
                ' Me.Icon = Icon.FromHandle(DirectCast(Image.FromFile(tPathImgDis), Bitmap).GetHicon()) 'Icon.FromHandle(hIcon)
                Me.Icon = Icon.FromHandle(DirectCast(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))), Bitmap).GetHicon())
            End If
        End If
        _ProcNew = False
    End Sub

#Region "Property"

    Private _ActiveLang As ST.Lang.eLang = -1
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
#End Region

#Region "MAIN PROC"

#End Region

#Region "Proc"
    Private Sub LoadData()
        If Me.Query = "" Then Exit Sub
        _DataInfo = HI.Conn.SQLConn.GetDataTable(Me.Query, _DBEnum)
    End Sub

    Private Sub PrepareForm()

        Dim cmdstring As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim cmdstringQuery As String = ""
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
        cmdstring = "SELECT TOP 1 FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        cmdstring &= vbCrLf & "   FROM HSysTableObjForm WITH(NOLOCK) "
        cmdstring &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.FormName) & "' "

        _dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then
            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            Me.Width = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpWidth.ToString))
            Me.Height = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpHeight.ToString))

            _SortField = _dt.Rows(0)!FTSortField.ToString

            '------ Get Form Object Gen Grid-------------------
            cmdstring = " EXEC SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then
                cmdstringQuery = "  Select TOP 1  "

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
                            cmdstringQuery &= vbCrLf & "" & _FieldName
                        Else
                            cmdstringQuery &= vbCrLf & "," & _FieldName
                        End If
                    End If

                    If Row!FTDefaultsData.ToString <> "" Then
                        Dim _md As New HI.TL.DefaultsData
                        _md.FiledName = Row!FTFiledName.ToString

                        Select Case UCase(Row!FTDefaultsData.ToString)
                            Case "@USER"
                                _md.DataDefaults = HI.ST.UserInfo.UserName
                            Case "@DATE"
                                _md.DataDefaults = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                            Case Else
                                _md.DataDefaults = Row!FTDefaultsData.ToString()
                        End Select

                        _DefaultsData.Add(_md)
                    End If

                    If Row!FTStaNoneBase.ToString <> "Y" Then
                        Dim _m As New HI.TL.DataBaseFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _m.ControlType = Row!FTFormControlType.ToString()
                        _BaseFiled.Add(_m)
                    End If

                    If Row!FTPK.ToString = "Y" Then
                        Dim _m As New HI.TL.PKFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _KeyFiled.Add(_m)

                        If Me.MainKey = "" Then
                            Me.MainKey = Row!FTFiledName.ToString

                            cmdstring = "  SELECT        FTFiledName, FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName"
                            cmdstring &= vbCrLf & " FROM            HSysObjDynamic_D AS D WITH (NOLOCK)"
                            cmdstring &= vbCrLf & " WHERE    (LEFT(FTFiledName, LEN('" & Row!FTFiledName.ToString & "')) = '" & Row!FTFiledName.ToString & "')"
                            cmdstring &= vbCrLf & "  AND      (ISNULL(FTStaNoneBase, '') <> 'Y') "
                            cmdstring &= vbCrLf & "  AND  FTBaseName + '.' + FTPrefix + '.' + FTTableName <> '" & Me.TableName & "' "

                            Dim _dtchk As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)

                            For Each R As DataRow In _dtchk.Rows
                                Dim _m2 As New HI.TL.CheckDelFiled
                                _m2.Query = "SELECT TOP 1 " & R!FTFiledName.ToString & " FROM  " & R!FTTableName.ToString & "  AS C WITH(NOLOCK)  WHERE " & R!FTFiledName.ToString & "="
                                _CheckDelFiled.Add(_m2)
                            Next

                        End If
                    End If

                    If Row!FTStaCheckDup.ToString = "Y" And Row!FTValidate.ToString = "Y" Then
                        Dim _m As New HI.TL.DuplFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _CheckDuplFiled.Add(_m)

                        If Me.RequireField = "" Then Me.RequireField = Row!FTFiledName.ToString
                    End If

                    If Row!FTValidate.ToString = "Y" And Row!FTPK.ToString <> "Y" Then
                        Dim _m As New HI.TL.CheckFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _CheckFiled.Add(_m)
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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")


                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"

                                If Row!FTFiledName.ToString.ToUpper = "FTPayTerm".ToUpper Or Row!FTFiledName.ToString.ToUpper = "FTTermOfMonth".ToUpper Then
                                    AddHandler .Leave, AddressOf FTPayTerm_Leave
                                End If

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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")
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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")



                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With
                        Case "DateEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.DateEdit
                            With CType(Ctrl, DevExpress.XtraEditors.DateEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.AllowNullInput = DefaultBoolean.False

                                'If Row!FTDateControlFormat.ToString.ToUpper = "HH:mm".ToUpper Or Row!FTDateControlFormat.ToString.ToUpper = "HH:mm:ss".ToUpper Or Row!FTDateControlFormat.ToString.ToUpper = "yyyy".ToUpper Or Row!FTDateControlFormat.ToString.ToUpper = "MM".ToUpper Then
                                '    .Properties.DisplayFormat.FormatString = Row!FTDateControlFormat.ToString
                                '    .Properties.DisplayFormat.FormatType = FormatType.Custom
                                '    .Properties.EditFormat.FormatString = Row!FTDateControlFormat.ToString
                                '    .Properties.EditFormat.FormatType = FormatType.Custom
                                '    .Properties.Mask.EditMask = Row!FTDateControlFormat.ToString
                                '    .Properties.ShowClear = False
                                '    .Properties.Buttons(0).Visible = False

                                '    .Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
                                '    .Properties.DisplayFormat.FormatType = FormatType.Custom
                                '    .Properties.EditFormat.FormatString = "dd/MM/yyyy"
                                '    .Properties.EditFormat.FormatType = FormatType.Custom
                                '    .Properties.Mask.EditMask = "dd/MM/yyyy"
                                '    .Properties.ShowClear = True


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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")



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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                                .TabStop = False
                            End With
                        Case "PictureEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.PictureEdit

                            'Dim _ContextMenuStripPicture As New System.Windows.Forms.ContextMenuStrip
                            'Dim _AddPicture As New System.Windows.Forms.ToolStripMenuItem
                            'Dim _ClearPicture As New System.Windows.Forms.ToolStripMenuItem

                            'With _AddPicture
                            '    .Name = "ocmAddPicture" & Row!FTFiledName.ToString
                            '    .Text = "Add Picture"
                            '    AddHandler .Click, AddressOf DK.TL.HandlerControl.PictureAdd_Click


                            'With _ClearPicture
                            '    .Name = "ocmClearPicture" & Row!FTFiledName.ToString
                            '    .Text = "Clear Picture"
                            '    AddHandler .Click, AddressOf DK.TL.HandlerControl.PictureClear_Click


                            'With _ContextMenuStripPicture
                            '    .Name = "ContextMenuPicture" & Row!FTFiledName.ToString
                            '    .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_AddPicture, _ClearPicture})


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

                                'If Val(Row!FNButtonEditBrwID.ToString) > 0 Then
                                '    AddHandler .ButtonClick, AddressOf DK.TL.HandlerControl.DynamicButtone_ButtonClick
                                '    AddHandler .Leave, AddressOf DK.TL.HandlerControl.DynamicButtonedit_Leave


                                If Row!FTFiledName.ToString.ToUpper = "FNHSysEmpTypeId".ToUpper Then
                                    .Properties.Buttons(0).Visible = False
                                End If

                                If Row!FTStaTextUpper.ToString = "Y" Then
                                    .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                End If

                                .EnterMoveNextControl = True

                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")



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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")



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
                                .TabStop = Not (Row!FTStateReadOnly.ToString = "Y")



                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With

                            'Dim _Pic As New DevExpress.XtraEditors.PictureEdit
                            'Dim _PopUpMenu As New ContextMenuStrip
                            '_PopUpMenu.Items.Add(

                            'With CType(_Pic, DevExpress.XtraEditors.PictureEdit)
                            '    .Properties.ContextMenuStrip = _PopUpMenu

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
                        .TextSize = New Size(130, 13)
                        .TextVisible = Not (Row!FTFormControlType.ToString.ToUpper = "CheckEdit".ToUpper)

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

                    'If Row!FTState.ToString.ToUpper = "N".ToUpper Then
                    '    Me.lyg.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {_obj})
                    '    Me.olymain.HiddenItems.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {_obj})

                    Me.lyg.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {_obj})
                    If Row!FTState.ToString = "N" Then
                        Me.lyg.Item(Me.lyg.Items.Count - 1).HideToCustomization()
                    End If


                    _ColCount = _ColCount + 1

                    _StartX = _StartX + Val(Row!FNCtrlWidth.ToString)
                    _CtrHeight = Val(Row!FNCtrlHeight.ToString)

                Next


                cmdstringQuery &= vbCrLf & " FROM   " & Me.TableName & " As M WITH(NOLOCK) "

            End If

        End If

        Me.Query = cmdstringQuery

        If File.Exists(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML") And Not (HI.ST.SysInfo.DevelopMode) Then
            'olayout.ShowCustomizationForm()
            'olayout.HideCustomizationForm()
            olymain.RestoreLayoutFromXml(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
        Else

            cmdstring = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream  FROM HSysLayOut With(NOLOCK) WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "
            Dim _dtLayOut As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)

            If _dtLayOut.Rows.Count > 0 Then
                Try
                    olymain.RestoreLayoutFromStream(New MemoryStream(DirectCast(_dtLayOut.Rows(0)!FTLayOutStream, Byte())))
                Catch ex As Exception
                End Try
            End If

            _dtLayOut.Dispose()
        End If

    End Sub

    Public Sub DefaultsData()
        Dim _FieldName As String

        For I As Integer = 0 To _DefaultsData.ToArray.Count - 1
            _FieldName = _DefaultsData(I).FiledName.ToString

            Dim Pass As Boolean = True

            For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Text = _DefaultsData(I).DataDefaults.ToString
                        End With
                    Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            .Value = Val(_DefaultsData(I).DataDefaults.ToString)

                        End With
                    Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            .SelectedIndex = Val(_DefaultsData(I).DataDefaults.ToString)
                        End With
                    Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                            .Checked = (_DefaultsData(I).DataDefaults.ToString = "1")
                        End With
                    Case "DevExpress.XtraEditors.DateEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            .Text = _DefaultsData(I).DataDefaults.ToString
                        End With
                        'Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                        '    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                        '        If .Image Is Nothing Then
                        '            Pass = False
                        '        End If
                        '    End With
                    Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                            .Text = _DefaultsData(I).DataDefaults.ToString
                        End With
                    Case "DevExpress.XtraEditors.TextEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.TextEdit)
                            .Text = _DefaultsData(I).DataDefaults.ToString
                        End With
                    Case Else
                End Select
            Next
        Next


    End Sub
    Private Sub Preform()
        Me.LoadData()
    End Sub

    Private Sub PrepareInfo()
    End Sub

#End Region

#Region "General"
    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Me.TableName <> "" Then
                _Bindgrid = False
                Me.Preform()
                Dim _FieldName As String = ""
                For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                    _FieldName = _KeyFiled(I).FiledName.ToString
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                        Select Case Obj.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")

                                    If _FieldName.ToUpper = "FNHSysEmpTypeId".ToUpper Then
                                        _TypeEmp = _KeyFiled(I).FiledValue.ToString
                                        .Text = _KeyFiled(I).FiledValue.ToString
                                    End If

                                End With
                            Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.TextEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.TextEdit)

                                    If _FieldName.ToUpper = "FTPayYear".ToUpper Then
                                        _Payyaer = _KeyFiled(I).FiledValue.ToString
                                        .Text = _KeyFiled(I).FiledValue.ToString
                                    End If

                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    If _FieldName.ToUpper = "FTPayYear".ToUpper Then
                                        _Payyaer = _KeyFiled(I).FiledValue.ToString
                                        .Text = _KeyFiled(I).FiledValue.ToString
                                    End If

                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                        End Select
                    Next
                Next

                'If Me.MainKeyID <> "" Then
                '    Call LoadDataEdit(Me.MainKeyID)


                If _KeyFiled(0).FiledValue.ToString <> "" Then
                    Call LoadDataEdit(Me.MainKeyID)
                End If

                If Not (Me.ObjectFocus Is Nothing) Then
                    Me.ObjectFocus.Focus()
                End If

                If Me.ActiveLang <> HI.ST.Lang.Language Then
                    HI.ST.Lang.SP_SETxLanguage(Me)
                    Me.ActiveLang = HI.ST.Lang.Language
                End If

            Else
                HI.MG.ShowMsg.mProcessError(1301110002, "", Me.Text)
                Me.Close()
            End If
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message, Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub sbCustomization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sbCustomization.Click
        Me.olymain.ShowCustomizationForm()
    End Sub

    Private Sub ocmsavelayuot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsavelayuot.Click
        Dim cmdstring As String = ""

        If (HI.ST.SysInfo.DevelopMode) Then

            cmdstring = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream  FROM HSysLayOut With(NOLOCK) WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "

            If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then

                Dim Cnn As New System.Data.SqlClient.SqlConnection

                '  Dim Server As HI.Conn.DB.DataBaseName = DK.Data.Config.Server(DK.Data.Config.DataBase.MASTER)

                Cnn.ConnectionString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                Cnn.Open()

                cmdstring = "INSERT INTO HSysLayOut(FNGrpObjID, FTLayOutStream) VALUES(@FNGrpObjID,@FTLayOutStream)"
                Dim cmd As New SqlCommand(cmdstring, Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.FormObjID.ToString)

                Dim ms As New MemoryStream()
                olymain.SaveLayoutToStream(ms)

                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.VarBinary)
                p.Value = data

                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()


                HI.Conn.SQLConn.DisposeSqlConnection(Cnn)

            Else

                Dim Cnn As New System.Data.SqlClient.SqlConnection

                ' Dim Server As DK.Data.Serv = DK.Data.Config.Server(DK.Data.Config.DataBase.MASTER)
                Cnn.ConnectionString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                Cnn.Open()

                cmdstring = " UPDATE HSysLayOut SET FTLayOutStream=@FTLayOutStream WHERE  FNGrpObjID=@FNGrpObjID "
                Dim cmd As New SqlCommand(cmdstring, Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.FormObjID.ToString)

                Dim ms As New MemoryStream()
                olymain.SaveLayoutToStream(ms)

                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.VarBinary)
                p.Value = data

                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()

                HI.Conn.SQLConn.DisposeSqlConnection(Cnn)

            End If

        Else
            Me.olymain.SaveLayoutToXml(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
        End If


        cmdstring = "UPDATE HSysTableObjForm SET FNFormPopUpWidth=" & Me.Width & ",FNFormPopUpHeight=" & Me.Height & ""
        cmdstring &= " WHERE FNFormObjID=" & Me.FormObjID & " "
        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)
        MsgBox("Save Layout Complete ....")

    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmaddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddnew.Click
        If VerrifyData() Then
            Dim _FieldName As String
            Dim _Fields As String = ""
            Dim _Values As String = ""
            Dim cmdstring As String
            Dim _Key As String = ""
            Dim _Val As String = ""

            If _KeyFiled.ToArray.Count = 1 Then
                If Microsoft.VisualBasic.Left(_KeyFiled(0).FiledName.ToString, Len("FNHSys")).ToUpper = "FNHSys".ToUpper Then
                    _Key = HI.TL.RunID.GetRunNoID(Me.TableName, Me.MainKey, _DBEnum).ToString
                End If

                _KeyFiled(0).FiledValue = _Key

            Else
                For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                    _FieldName = _KeyFiled(I).FiledName.ToString
                    _Val = ""
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Select Case Obj.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    _Val = "" & .Properties.Tag.ToString
                                End With
                            Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    _Val = .Value.ToString
                                End With
                            Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    If "" & .Properties.Tag.ToString <> "" Then
                                        _Val = HI.TL.CboList.GetListValue("" & .Properties.Tag.ToString, .SelectedIndex.ToString)
                                    Else
                                        _Val = .SelectedIndex.ToString
                                    End If

                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    If .EditValue.ToString.Length > 1 Then
                                        If .Checked Then
                                            _Val = "1"
                                        Else
                                            _Val = "0"
                                        End If
                                    Else
                                        _Val = .EditValue.ToString
                                    End If
                                End With
                            Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                End With
                            Case "DevExpress.XtraEditors.TextEdit".ToUpper, "DevExpress.XtraEditors.DateEdit".ToUpper
                                _Val = Trim(Obj.Text)
                            Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                                _Val = Obj.Text
                            Case Else
                                _Val = Obj.Text
                        End Select

                        _KeyFiled(I).FiledValue = _Val
                    Next
                Next

            End If



            For I As Integer = 0 To _BaseFiled.ToArray.Count - 1
                _FieldName = _BaseFiled(I).FiledName.ToString
                _Val = ""
                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    If UCase(_FieldName) = Me.MainKey.ToUpper And _Key <> "" Then
                        _Val = _Key
                    Else
                        Select Case Obj.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    _Val = "" & .Properties.Tag.ToString
                                End With
                            Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    _Val = .Value.ToString
                                End With
                            Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    If "" & .Properties.Tag.ToString <> "" Then
                                        _Val = HI.TL.CboList.GetListValue("" & .Properties.Tag.ToString, .SelectedIndex.ToString)
                                    Else
                                        _Val = .SelectedIndex.ToString
                                    End If
                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    If .EditValue.ToString.Length > 1 Then
                                        If .Checked Then
                                            _Val = "1"
                                        Else
                                            _Val = "0"
                                        End If
                                    Else
                                        _Val = .EditValue.ToString
                                    End If
                                End With
                            Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _KeyFiled(0).FiledValue.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                End With
                            Case "DevExpress.XtraEditors.TextEdit".ToUpper, "DevExpress.XtraEditors.DateEdit".ToUpper
                                _Val = Trim(Obj.Text)
                            Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                                _Val = Obj.Text
                            Case Else
                                _Val = Obj.Text
                        End Select
                    End If
                Next

                If _Values <> "" Then _Values &= ","
                If _Fields <> "" Then _Fields &= ","

                _Fields &= _FieldName

                Select Case UCase(_FieldName)
                    Case UCase("FDInsDate"), UCase("FTInsDate")
                        _Values &= HI.UL.ULDate.FormatDateDB & ""
                    Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
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

                                '    _Values &= "'" & DK.UL.Date.ConvertEnDB(_Val) & "'"

                                _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"

                        End Select
                End Select
            Next

            cmdstring = " INSERT INTO   " & Me.TableName & "(" & _Fields & ") VALUES (" & _Values & ")"

            If HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM) Then
                Me.ProcComplete = True

                HI.TL.HandlerControl.ClearControl(Me)

                Parent_Form.Preform()
                ' Parent_Form.VerifyData()
                Call DefaultsData()


                For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                    _FieldName = _KeyFiled(I).FiledName.ToString
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Select Case Obj.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")

                                    If _FieldName.ToUpper = "FNHSysEmpTypeId".ToUpper Then
                                        .Text = _TypeEmp
                                    ElseIf _FieldName.ToUpper = "FTPayYear".ToUpper Then
                                        .Text = _Payyaer
                                    End If

                                End With
                            Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")

                                    If _FieldName.ToUpper = "FNHSysEmpTypeId".ToUpper Then
                                        .Text = _TypeEmp
                                    ElseIf _FieldName.ToUpper = "FTPayYear".ToUpper Then
                                        .Text = _Payyaer
                                    End If

                                End With
                        End Select
                    Next
                Next

                If Not (Me.ObjectFocus Is Nothing) Then
                    Me.ObjectFocus.Focus()
                End If

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
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

                        If _FieldName.ToString = "FNHSysEmpTypeId" Then
                            _Caption = "ประเภทพนักงาน"
                        Else
                            _Caption = _lay.Text
                        End If

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

        _Val = ""
        Dim cmdstringCheckDupl As String = ""
        For I As Integer = 0 To _CheckDuplFiled.ToArray.Count - 1
            _FieldName = _CheckDuplFiled(I).FiledName.ToString

            If cmdstringCheckDupl <> "" Then cmdstringCheckDupl &= " AND "

            For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                If ocmedit.Visible = False Then
                    With DirectCast(Obj.Parent, DevExpress.XtraLayout.LayoutControl)
                        Dim _lay As Object = .Items.FindByName(_FieldName)
                        If Not (_lay Is Nothing) Then
                            _Caption = _lay.Text
                        End If
                    End With

                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                _Val &= "  " & _Caption & " = " & .Text
                                cmdstringCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.Properties.Tag.ToString) & "' "
                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                _Val &= "  " & _Caption & " = " & .Value.ToString
                                cmdstringCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(.Value) & " "
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)

                                If "" & .Properties.Tag.ToString <> "" Then
                                    cmdstringCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(HI.TL.CboList.GetListValue("" & .Properties.Tag.ToString, .SelectedIndex.ToString)) & "  "
                                Else
                                    cmdstringCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(.SelectedIndex) & " "
                                End If


                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper

                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                cmdstringCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.EditValue) & "' "
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                _Val &= "  " & _Caption & " = " & .Text

                                Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                    Case "FD"
                                        cmdstringCheckDupl &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(.Text) & "' "
                                    Case Else
                                        cmdstringCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                End Select

                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                            _Val &= "  " & _Caption & " = " & Obj.Text
                            cmdstringCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(Obj.Text) & "' "
                    End Select
                End If


            Next

        Next

        Dim cmdstring As String = ""

        If cmdstringCheckDupl <> "" Then

            Dim cmdstringWhere As String = ""
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                Dim _value As String = _KeyFiled(I).FiledValue.ToString
                If _KeyFiled(I).FiledName.ToString.ToUpper = "FNHSysEmpTypeId".ToString.ToUpper Then
                    _value = HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysEmpTypeId FROM THRMEmpType WITH (NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(_value) & "' ", Conn.DB.DataBaseName.DB_MASTER, "0")
                End If

                If _value <> "" Then
                    If cmdstringWhere = "" Then
                        cmdstringWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "<>'" & HI.UL.ULF.rpQuoted(_value) & "' "
                    Else
                        cmdstringWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "<>'" & HI.UL.ULF.rpQuoted(_value) & "' "
                    End If
                End If

            Next


            cmdstring = " Select Top 1 " & Me.MainKey & ""
            cmdstring &= vbCrLf & "  From " & Me.TableName & " WITH (NOLOCK) " & " "
            cmdstring &= vbCrLf & "  WHERE    " & cmdstringCheckDupl

            If HI.Conn.SQLConn.GetField(cmdstring, _DBEnum, "") <> "" Then

                If Not (Me.ObjectFocus Is Nothing) Then
                    Me.ObjectFocus.Focus()
                End If

                HI.MG.ShowMsg.mProcessError(1301110001, "", Me.Text, MessageBoxIcon.Error, _Val)
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmedit.Click
        If VerrifyData() Then
            Dim _FieldName As String
            Dim _Fields As String = ""
            Dim _Values As String = ""
            Dim cmdstring As String
            Dim _Val As String = ""
            Dim cmdstringWhere As String = ""
            Dim _value As String
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1

                _value = _KeyFiled(I).FiledValue.ToString
                If _KeyFiled(I).FiledName.ToString.ToUpper = "FNHSysEmpTypeId".ToString.ToUpper Then
                    _value = HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysEmpTypeId FROM THRMEmpType WITH (NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(_value) & "' ", Conn.DB.DataBaseName.DB_MASTER, "0")
                End If

                If cmdstringWhere = "" Then
                    cmdstringWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_value) & "' "
                Else
                    cmdstringWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_value) & "' "
                End If
            Next

            If cmdstringWhere = "" Then Exit Sub

            For I As Integer = 0 To _BaseFiled.ToArray.Count - 1
                _FieldName = _BaseFiled(I).FiledName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                _Val = "" & .Properties.Tag.ToString
                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                _Val = .Value.ToString
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If "" & .Properties.Tag.ToString <> "" Then
                                    _Val = HI.TL.CboList.GetListValue("" & .Properties.Tag.ToString, .SelectedIndex.ToString)
                                Else
                                    _Val = .SelectedIndex.ToString
                                End If

                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                If .EditValue.ToString.Length > 1 Then
                                    If .Checked Then
                                        _Val = "1"
                                    Else
                                        _Val = "0"
                                    End If
                                Else
                                    _Val = .EditValue.ToString
                                End If
                            End With
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _KeyFiled(0).FiledValue.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                            End With
                        Case "DevExpress.XtraEditors.TextEdit".ToUpper, "DevExpress.XtraEditors.DateEdit".ToUpper
                            _Val = Trim(Obj.Text)
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                            _Val = Obj.Text
                        Case Else
                            _Val = Obj.Text
                    End Select


                Next

                Select Case UCase(_FieldName)
                    Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                    Case Else
                        If _Values <> "" Then _Values &= ","
                End Select


                Select Case UCase(_FieldName)
                    Case UCase("FDUpdDate"), UCase("FTUpdDate")
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
            Next

            cmdstring = " Update  " & Me.TableName & " Set " & _Values & "   " & cmdstringWhere

            If HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM) Then
                Me.ProcComplete = True
                HI.TL.HandlerControl.ClearControl(Me)

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.Close()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If

        End If

    End Sub

    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click

        Dim cmdstringWhere As String = ""
        Dim _value As String
        For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
            _value = _KeyFiled(I).FiledValue.ToString
            If _KeyFiled(I).FiledName.ToString.ToUpper = "FNHSysEmpTypeId".ToString.ToUpper Then
                _value = HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysEmpTypeId FROM THRMEmpType WITH (NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(_value) & "' ", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If

            If cmdstringWhere = "" Then
                cmdstringWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_value) & "' "
            Else
                cmdstringWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_value) & "' "
            End If
        Next

        If cmdstringWhere = "" Then Exit Sub
        Dim cmdstring As String = " Delete From " & Me.TableName & " " & cmdstringWhere

        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = True Then

            Dim _FieldName As String = ""
            For I As Integer = 0 To _BaseFiled.ToArray.Count - 1
                _FieldName = _BaseFiled(I).FiledName.ToString
                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try

                                Catch ex As Exception
                                End Try
                            End With
                    End Select
                Next
            Next

            If Not HI.Conn.SQLConn.ExecuteOnly(cmdstring, _DBEnum) Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Else
                Me.ProcComplete = True
                HI.TL.HandlerControl.ClearControl(Me)

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.Close()
            End If
        End If

    End Sub

    Private Sub LoadDataEdit(ByVal MSysId As String)

        Dim cmdstringWhere As String = ""
        Dim _value As String = ""

        For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
            _value = _KeyFiled(I).FiledValue.ToString
            If _KeyFiled(I).FiledName.ToString.ToUpper = "FNHSysEmpTypeId".ToString.ToUpper Then
                _value = HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysEmpTypeId FROM THRMEmpType WITH (NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(_value) & "' ", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If
            If cmdstringWhere = "" Then
                cmdstringWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_value) & "' "
            Else
                cmdstringWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_value) & "' "
            End If
        Next

        Dim cmdstring As String = Me.Query & "   " & cmdstringWhere

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, _DBEnum)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    If "" & .Properties.Tag.ToString <> "" Then
                                        .SelectedIndex = HI.TL.CboList.GetIndexByValue("" & .Properties.Tag.ToString, R.Item(Col).ToString)
                                    Else
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    End If

                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                            Obj.Text = R.Item(Col).ToString
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            Try
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    If .Properties.DisplayFormat.FormatString = "dd/MM/yyyy" Or .Properties.DisplayFormat.FormatString = "d" Then
                                        .DateTime = CDate(HI.UL.ULDate.ConvertEnDB(R.Item(Col).ToString))
                                    Else
                                        .Text = R.Item(Col).ToString
                                    End If
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
        Me.ocmexit.Focus()
    End Sub

    Private Function GenSubQuery(ByVal FilesName As String) As String
        Dim SubQuery As String = ""
        Dim cmdstring As String = ""
        cmdstring = "SELECT TOP 1 'SELECT TOP 1  ' + FTColumnKeyCode  + ' FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=M.' + '" & FilesName & "'"
        cmdstring &= vbCrLf & "  FROM  HSysTTablePK WITH (NOLOCK)  "
        cmdstring &= vbCrLf & "  WHERE (FTColumnName IN (SELECT TOP 1 FTColumnName FROM HSysTTablePKRef WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(FilesName) & "'))"
        cmdstring &= vbCrLf & "    AND (FTColumnKeyCode <> '')"

        SubQuery = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM, "")
        Return SubQuery
    End Function

    Private Sub ocmdeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdeletelayout.Click

        If (HI.ST.SysInfo.DevelopMode) Then
            Dim cmdstring As String = " Delete FROM HSysLayOut WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "
            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)

            MsgBox("Delete Complete....")
        Else
            If File.Exists(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML") Then
                File.Delete(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
                MsgBox("Delete Complete....")
            End If
        End If

    End Sub

    Private Function CheckNotUsed(ByVal Key As String) As Boolean
        Dim cmdstring As String = ""

        For I As Integer = 0 To _CheckDelFiled.ToArray.Count - 1
            cmdstring = _CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

            If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then

                HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                Return False
            End If

        Next
        Return True
    End Function

    Private Sub FTPayTerm_Leave(sender As System.Object, e As System.EventArgs)
        If (_ProcNew) Then Exit Sub
        If IsNumeric(sender.Text) Then
            sender.Text = Format(Integer.Parse(sender.Text), "00")
        Else
            sender.Text = ""
        End If
    End Sub

#End Region

End Class