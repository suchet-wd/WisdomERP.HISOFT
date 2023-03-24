﻿Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports HI.TL

Public Class wAddEditTrainTopic

#Region "Vaiable"

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Public _KeyFiled As New List(Of HI.TL.PKFiled)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    Private _DefaultsData As New List(Of HI.TL.DefaultsData)()
    Private _DataInfo As DataTable
    Private _SysPathImge As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

#End Region

    Sub New(ByVal FormName As String, ByVal Title As String, ByVal ObjId As Integer, ByVal AssemblyPath As String, ByVal tImage As String, ByVal tParentForm As Object)
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

                Me.Icon = Icon.FromHandle(DirectCast(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))), Bitmap).GetHicon())
            End If
        End If

    End Sub

#Region "Property"
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
#End Region

#Region "MAIN PROC"

#End Region

#Region "Proc"

    Private Sub Preform()
        Me.LoadData()
    End Sub

    Private Sub PrepareInfo()
    End Sub

#End Region

#Region "General"
    Private Sub LoadData()
        If Me.Query = "" Then Exit Sub
        _DataInfo = HI.Conn.SQLConn.GetDataTable(Me.Query, _DBEnum)
    End Sub

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


        _Str = "SELECT TOP 1 FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.FormName) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then
            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            Me.Width = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpWidth.ToString))
            Me.Height = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpHeight.ToString))

            _SortField = _dt.Rows(0)!FTSortField.ToString

            '------ Get Form Object Gen Grid-------------------
            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
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

                    If Row!FTStaCheckDup.ToString = "Y" And Row!FTValidate.ToString = "Y" Then
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

                        .Control = Ctrl
                        .CustomizationFormText = Row!FTFiledName.ToString
                        .Location = New System.Drawing.Point(_StartX, _StartY)
                        .Name = Row!FTFiledName.ToString

                        .Size = New System.Drawing.Size(Val(Row!FNCtrlWidth.ToString), Val(Row!FNCtrlHeight.ToString))
                        .Text = Row!FTFiledName.ToString

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


                    Me.lyg.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {_obj})
                    If Row!FTState.ToString = "N" Then
                        Me.lyg.Item(Me.lyg.Items.Count - 1).HideToCustomization()
                    End If

                    _ColCount = _ColCount + 1

                    _StartX = _StartX + Val(Row!FNCtrlWidth.ToString)
                    _CtrHeight = Val(Row!FNCtrlHeight.ToString)

                Next


                _StrQuery &= vbCrLf & " FROM   " & Me.TableName & " As M WITH(NOLOCK) "
            End If

        End If

        Me.Query = _StrQuery

        If File.Exists(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML") And Not (HI.ST.SysInfo.DevelopMode) Then
            olymain.RestoreLayoutFromXml(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
        Else

            _Str = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream  FROM HSysLayOut With(NOLOCK) WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "
            Dim _dtLayOut As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

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

    Private Sub wAddEditHoliday_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If Not (Me.ObjectFocus Is Nothing) Then
            Me.ObjectFocus.Focus()
            Me.ObjectFocus.SelectAll()
        End If
    End Sub

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
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                        End Select

                    Next
                Next

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

        Dim _Str As String = ""

        If (HI.ST.SysInfo.DevelopMode) Then


            _Str = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream  FROM HSysLayOut With(NOLOCK) WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()

                _Str = "INSERT INTO HSysLayOut(FNGrpObjID, FTLayOutStream) VALUES(@FNGrpObjID,@FTLayOutStream)"
                Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.FormObjID.ToString)

                Dim ms As New MemoryStream()
                olymain.SaveLayoutToStream(ms)

                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.VarBinary)
                p.Value = data

                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()

                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

            Else

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()

                _Str = " UPDATE HSysLayOut SET FTLayOutStream=@FTLayOutStream WHERE  FNGrpObjID=@FNGrpObjID "
                Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.FormObjID.ToString)

                Dim ms As New MemoryStream()
                olymain.SaveLayoutToStream(ms)

                Dim data As Byte() = ms.GetBuffer()
                Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.VarBinary)
                p.Value = data

                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()

                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

            End If

        Else
            Me.olymain.SaveLayoutToXml(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
        End If


        _Str = "UPDATE HSysTableObjForm SET FNFormPopUpWidth=" & Me.Width & ",FNFormPopUpHeight=" & Me.Height & ""
        _Str &= " WHERE FNFormObjID=" & Me.FormObjID & " "
        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

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
            Dim _Str As String
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
                                        _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                    Else
                                        _Val = .SelectedIndex.ToString
                                    End If
                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    _Val = .EditValue.ToString
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
                                        _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                    Else
                                        _Val = .SelectedIndex.ToString
                                    End If
                                End With
                            Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    _Val = .EditValue.ToString
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

                                _Values &= "N'" & HI.UL.ULF.rpQuoted(_Val) & "'"

                        End Select
                End Select
            Next

            _Str = " INSERT INTO   " & Me.TableName & "(" & _Fields & ") VALUES (" & _Values & ")"

            If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM) Then
                Me.ProcComplete = True

                HI.TL.HandlerControl.ClearControl(Me)

                Parent_Form.Preform()
                Call DefaultsData()

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

        _Val = ""
        Dim _StrCheckDupl As String = ""
        For I As Integer = 0 To _CheckDuplFiled.ToArray.Count - 1
            _FieldName = _CheckDuplFiled(I).FiledName.ToString

            If _StrCheckDupl <> "" Then _StrCheckDupl &= " AND "


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
                            _Val &= _Val & "  " & _Caption & " = " & .Properties.Tag.ToString
                            _StrCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.Properties.Tag.ToString) & "' "
                        End With
                    Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            _Val &= _Val & "  " & _Caption & " = " & .Value.ToString
                            _StrCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(.Value) & " "
                        End With
                    Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            If "" & .Properties.Tag.ToString <> "" Then
                                _StrCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)) & " "
                            Else
                                _StrCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(.SelectedIndex) & " "
                            End If
                        End With
                    Case "DevExpress.XtraEditors.CheckEdit".ToUpper

                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                            _StrCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.EditValue) & "' "
                        End With
                    Case "DevExpress.XtraEditors.DateEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            _Val &= _Val & "  " & _Caption & " = " & .Text
                            Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                Case "FD"
                                    _StrCheckDupl &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(.Text) & "' "
                                Case Else
                                    _StrCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                            End Select
                        End With
                    Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                        _Val &= _Val & "  " & _Caption & " = " & Obj.Text
                        _StrCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(Obj.Text) & "' "
                End Select

            Next

        Next

        Dim _Str As String = ""

        If _StrCheckDupl <> "" Then

            Dim _StrWhere As String = ""
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _StrWhere = "" Then
                    _StrWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "<>'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                Else
                    _StrWhere = "  AND  " & _KeyFiled(I).FiledName.ToString & "<>'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                End If
            Next


            _Str = " Select Top 1 " & Me.MainKey & ""
            _Str &= vbCrLf & "  From " & Me.TableName & " WITH (NOLOCK) " & " "
            _Str &= vbCrLf & " " & _StrWhere
            _Str &= vbCrLf & "  AND   " & _StrCheckDupl

            If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") <> "" Then

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
            Dim _Str As String
            Dim _Val As String = ""
            Dim _StrWhere As String = ""

            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _StrWhere = "" Then
                    _StrWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                Else
                    _StrWhere = "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                End If
            Next

            If _StrWhere = "" Then Exit Sub

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
                                    _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                Else
                                    _Val = .SelectedIndex.ToString
                                End If

                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                _Val = .EditValue.ToString
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

                                _Values &= _FieldName & "=N'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                        End Select
                End Select
            Next

            _Str = " Update  " & Me.TableName & " Set " & _Values & "   " & _StrWhere

            If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM) Then

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
        Dim _Key As String = Me.MainKeyID
        'If _Key = "" Then Exit Sub
        Dim JK As Integer = 0
        Dim _QryWhere As String = ""

        For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
            If _QryWhere = "" Then
                _Key = _KeyFiled(I).FiledValue.ToString
                _QryWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
            Else
                _QryWhere = "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
            End If
        Next
        If _QryWhere = "" Then Exit Sub
        Dim _Qry As String = " Delete From " & Me.TableName & " " & "   " & _QryWhere

        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete) = True Then

            Dim _FieldName As String = ""
            For I As Integer = 0 To _BaseFiled.ToArray.Count - 1
                _FieldName = _BaseFiled(I).FiledName.ToString
                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    Dim _Path As String = "" & .Properties.Tag.ToString & _Key & "_" & .Name.ToString
                                    If File.Exists(_Path) = True Then
                                        File.Delete(_Path)
                                    End If
                                Catch ex As Exception
                                End Try
                            End With
                    End Select
                Next
            Next

            If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, _DBEnum)) Then
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

            Else
                Me.ProcComplete = True

                HI.TL.HandlerControl.ClearControl(Me)

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.Close()
            End If
        End If

    End Sub

    Private Sub LoadDataEdit(ByVal HSysId As String)

        Dim _StrWhere As String = ""
        For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
            If _StrWhere = "" Then
                _StrWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
            Else
                _StrWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
            End If
        Next

        Dim _Str As String = Me.Query & "   " & _StrWhere

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
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
                                    .SelectedIndex = Val(R.Item(Col).ToString)

                                    If "" & .Properties.Tag.ToString <> "" Then
                                        .SelectedIndex = HI.TL.CboList.GetIndexByValue(.Properties.Tag.ToString, R.Item(Col).ToString)
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
                                    If .Properties.DisplayFormat.FormatString = "dd/MM/yyyy" Then
                                        .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
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
        Dim _Str As String = ""
        _Str = "SELECT TOP 1 'SELECT TOP 1  ' + FTColumnKeyCode  + ' FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=M.' + '" & FilesName & "'"
        _Str &= vbCrLf & "  FROM  HSysTTablePK WITH (NOLOCK)  "
        _Str &= vbCrLf & "  WHERE (FTColumnName IN (SELECT TOP 1 FTColumnName FROM HSysTTablePKRef WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(FilesName) & "'))"
        _Str &= vbCrLf & "    AND (FTColumnKeyCode <> '')"

        SubQuery = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "")
        Return SubQuery
    End Function

    Private Sub ocmdeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdeletelayout.Click

        If (HI.ST.SysInfo.DevelopMode) Then
            Dim _Str As String = " Delete FROM HSysLayOut WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            MsgBox("Delete Complete....")
        Else

            If File.Exists(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML") Then
                File.Delete(Application.StartupPath & "\LayoutForm\" & Me.FormObjID.ToString & ".XML")
                MsgBox("Delete Complete....")
            End If

        End If

    End Sub

    Private Function CheckNotUsed(ByVal Key As String) As Boolean
        Dim _Str As String = ""

        For I As Integer = 0 To _CheckDelFiled.ToArray.Count - 1
            _Str = _CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                Return False
            End If

        Next

        Return True
    End Function
#End Region

End Class