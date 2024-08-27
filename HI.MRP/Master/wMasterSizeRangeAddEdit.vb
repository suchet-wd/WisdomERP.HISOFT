Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class wMasterSizeRangeAddEdit
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER

    Private _Bindgrid As Boolean = False
    Private _RowDataChange As Boolean = False
    Public _KeyFiled As New List(Of HI.TL.PKFiled)()
    Public _LockFiled As New List(Of HI.TL.LockEditField)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    Private _InitData As New List(Of HI.TL.DefaultsData)()
    Private _CheckCopyField As New List(Of HI.TL.CopyFromFiled)()
    Private _GenAutoByField As New List(Of HI.TL.GenAutoByFiled)()
    Private _ReadOnlyField As New List(Of HI.TL.ReadOnlyField)()
    Private _TableField As New List(Of HI.TL.TableField)()

    Public _StateProcCopy As Boolean = False
    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Sub New(ByVal FormName As String, ByVal Title As String, ByVal ObjId As Integer, ByVal tParentForm As Object)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Name = "wMerSizeRangeAddEditPopup"
        Me.FormName = FormName

        Me.Text = Title
        Me.Parent_Form = tParentForm

        _KeyFiled.Clear()
        _CheckFiled.Clear()
        _CheckDuplFiled.Clear()
        _BaseFiled.Clear()
        _CheckDelFiled.Clear()
        _ReadOnlyField.Clear()

        Me.FormObjID = ObjId
        Me.InitFormControl()

        Me.ocmsavelayout.Visible = HI.ST.SysInfo.DevelopMode
        Me.sbCustomization.Visible = HI.ST.SysInfo.DevelopMode
        Me.ocmdeletelayout.Visible = HI.ST.SysInfo.DevelopMode
        Me.olymain.AllowCustomizationMenu = HI.ST.SysInfo.DevelopMode

        If HI.ST.SysInfo.DevelopMode Then
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
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

    Private _TableNameOrg As String = ""
    Public Property TableNameOrg As String
        Get
            Return _TableNameOrg
        End Get
        Set(ByVal value As String)
            _TableNameOrg = value
        End Set
    End Property

    Private _FTProcValidateEdit As String = ""
    Public Property FTProcValidateEdit As String
        Get
            Return _FTProcValidateEdit
        End Get
        Set(ByVal value As String)
            _FTProcValidateEdit = value
        End Set
    End Property

    Private _FTBFProcSave As String = ""
    Public Property FTBFProcSave As String
        Get
            Return _FTBFProcSave
        End Get
        Set(ByVal value As String)
            _FTBFProcSave = value
        End Set
    End Property


    Private _FTNotCopyField As String = ""
    Public Property FTNotCopyField As String
        Get
            Return _FTNotCopyField
        End Get
        Set(ByVal value As String)
            _FTNotCopyField = value
        End Set
    End Property




    Private _FTProcSave As String = ""
    Public Property FTProcSave As String
        Get
            Return _FTProcSave
        End Get
        Set(ByVal value As String)
            _FTProcSave = value
        End Set
    End Property

    Private _FTBFProcDelete As String = ""
    Public Property FTBFProcDelete As String
        Get
            Return _FTBFProcDelete
        End Get
        Set(ByVal value As String)
            _FTBFProcDelete = value
        End Set
    End Property

    Private _FTProcDelete As String = ""
    Public Property FTProcDelete As String
        Get
            Return _FTProcDelete
        End Get
        Set(ByVal value As String)
            _FTProcDelete = value
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

#End Region

#Region "Proc"

    Private Sub LoadData()
        If Me.Query = "" Then Exit Sub
        _DataInfo = HI.Conn.SQLConn.GetDataTable(Me.Query, _DBEnum,, False)

        LoadGroupRange()
    End Sub

    Private Sub InitFormControl()

        Dim _Qry As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _QryQuery As String = ""
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

        _Qry = "SELECT TOP 1 FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight,ISNULL(FNLabelFormDynamicWidth,130) AS FNLabelFormDynamicWidth,ISNULL(FTProcValidateEdit,'')  AS FTProcValidateEdit,ISNULL(FTProcSave,'') AS FTProcSave  "
        _Qry &= vbCrLf & " ,FTTableName AS FTTableNameOrg,FTNotCopyField"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.FormName) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.TableName = _dt.Rows(0)!FTTableName.ToString
            Me.FTProcValidateEdit = _dt.Rows(0)!FTProcValidateEdit.ToString
            Me.FTProcSave = _dt.Rows(0)!FTProcSave.ToString
            Me.TableNameOrg = _dt.Rows(0)!FTTableNameOrg.ToString
            Me.Width = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpWidth.ToString))
            Me.Height = Integer.Parse(Val(_dt.Rows(0)!FNFormPopUpHeight.ToString))
            Me.LabalWidth = Integer.Parse(Val(_dt.Rows(0)!FNLabelFormDynamicWidth.ToString))

            Me.FTNotCopyField = _dt.Rows(0)!FTNotCopyField.ToString.Trim()

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then

                _QryQuery = "  Select TOP 1  "

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


                    Dim _mdtabf As New HI.TL.TableField
                    _mdtabf.FiledName = Row!FTFiledName.ToString
                    _TableField.Add(_mdtabf)

                    If Row!FTStaNoneBase.ToString <> "Y" Then
                        If Row!FTFormControlType.ToString.ToUpper = "ButtonEdit".ToUpper And Val(Row!FNButtonEditBrwID.ToString) > 0 Then
                            Dim _GetSysSubQuery As String = HI.TL.HSysField.GetSysSubQuery(_FieldName)
                            If _GetSysSubQuery <> "" Then
                                _FieldName = "ISNULL((" & _GetSysSubQuery & "),'') AS " & _FieldName
                            End If
                        End If

                        If _ColCount = 0 Then
                            _QryQuery &= vbCrLf & "" & _FieldName
                        Else
                            _QryQuery &= vbCrLf & "," & _FieldName
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
                            Case "@CMPCODE".ToUpper
                                _md.DataDefaults = HI.ST.SysInfo.CmpCode.ToString
                            Case "@CMPSEWCODE".ToUpper
                                _md.DataDefaults = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTSewingCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS X  WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " ", Conn.DB.DataBaseName.DB_MASTER, "")
                            Case "@MaxRun".ToUpper
                                _md.DataDefaults = "SELECT MAX(" & Row!FTFiledName.ToString & ") +1 FROM  " & Me.TableName & "  WITH(NOLOCK) "
                                _md.QueryDefaults = True
                            Case Else
                                _md.DataDefaults = Row!FTDefaultsData.ToString()
                        End Select

                        _InitData.Add(_md)

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

                            _Qry = "  SELECT        FTFiledName, FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName"
                            _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjDynamic_D AS D WITH (NOLOCK)"
                            _Qry &= vbCrLf & " WHERE    (LEFT(FTFiledName, LEN('" & Row!FTFiledName.ToString & "')) = '" & Row!FTFiledName.ToString & "')"
                            _Qry &= vbCrLf & "  AND      (ISNULL(FTStaNoneBase, '') <> 'Y') "
                            _Qry &= vbCrLf & "  AND  FTBaseName + '.' + FTPrefix + '.' + FTTableName <> '" & Me.TableName & "' "

                            Dim _dtchk As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

                            For Each R As DataRow In _dtchk.Rows
                                Dim _m2 As New HI.TL.CheckDelFiled
                                _m2.Query = "SELECT TOP 1 " & R!FTFiledName.ToString & " FROM  " & R!FTTableName.ToString & "  AS C WITH(NOLOCK)  WHERE " & R!FTFiledName.ToString & "="
                                _CheckDelFiled.Add(_m2)
                            Next

                            _Qry = "  SELECT        FTColumnName AS FTFiledName, FTDBName + '.' + FTPrefix + '.' + FTTableName AS FTTableName"
                            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePK WITH (NOLOCK)  "
                            _Qry &= vbCrLf & "  WHERE (FTColumnName IN (SELECT FTColumnName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePKRef WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(Row!FTFiledName.ToString) & "'))"
                            _Qry &= vbCrLf & "  AND FTDBName + '.' + FTPrefix + '.' + FTTableName <>'" & HI.UL.ULF.rpQuoted(Me.TableName) & "' "

                            _dtchk = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

                            For Each R As DataRow In _dtchk.Rows
                                Dim _m2 As New HI.TL.CheckDelFiled
                                _m2.Query = "SELECT TOP 1 " & R!FTFiledName.ToString & " FROM  " & R!FTTableName.ToString & "  AS C WITH(NOLOCK)  WHERE " & R!FTFiledName.ToString & "="
                                _CheckDelFiled.Add(_m2)
                            Next


                        End If
                    End If

                    If Row!FTStateLockEdit.ToString = "1" Then
                        Dim _m As New HI.TL.LockEditField
                        _m.FiledName = Row!FTFiledName.ToString
                        _m.FiledValue = ""
                        _LockFiled.Add(_m)
                    End If

                    If Row!FTStaCheckDup.ToString = "Y" Then
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

                    If Row!FTStateCopyNotChange.ToString = "Y" Then
                        Dim _m As New HI.TL.CopyFromFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _CheckCopyField.Add(_m)
                    End If

                    If Row!FTGenAutoByField.ToString <> "" Then
                        Dim _m As New HI.TL.GenAutoByFiled
                        _m.FiledName = Row!FTFiledName.ToString
                        _m.GenByFiledName = Row!FTGenAutoByField.ToString
                        _GenAutoByField.Add(_m)
                    End If

                    If Row!FTStateReadOnly.ToString <> "" Then
                        Dim _m As New HI.TL.ReadOnlyField
                        _m.FiledName = Row!FTFiledName.ToString
                        _ReadOnlyField.Add(_m)
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
                                .Properties.DisplayFormat.FormatString = "{0:n" & Val(Row!FNNumericScale.ToString).ToString & "}"
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
                        Case "TimeEdit".ToUpper
                            Ctrl = New DevExpress.XtraEditors.TimeEdit
                            With CType(Ctrl, DevExpress.XtraEditors.TimeEdit)
                                .Name = Row!FTFiledName.ToString
                                .Properties.AllowNullInput = DefaultBoolean.False

                                If Row!FTDateControlFormat.ToString.ToUpper = "".ToUpper Then
                                    .Properties.DisplayFormat.FormatString = "HH:mm"
                                    .Properties.DisplayFormat.FormatType = FormatType.DateTime
                                    .Properties.EditFormat.FormatString = "HH:mm"
                                    .Properties.EditFormat.FormatType = FormatType.DateTime
                                    .Properties.Mask.EditMask = "HH:mm"
                                    .Properties.Mask.UseMaskAsDisplayFormat = True

                                Else
                                    .Properties.DisplayFormat.FormatString = Row!FTDateControlFormat.ToString
                                    .Properties.DisplayFormat.FormatType = FormatType.DateTime
                                    .Properties.EditFormat.FormatString = Row!FTDateControlFormat.ToString
                                    .Properties.EditFormat.FormatType = FormatType.DateTime
                                    .Properties.Mask.EditMask = Row!FTDateControlFormat.ToString
                                    .Properties.Mask.EditMask = Row!FTDateControlFormat.ToString
                                    .Properties.Mask.UseMaskAsDisplayFormat = True
                                End If
                                .Properties.Buttons.Item(0).Visible = False
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
                                .Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)
                                .TabIndex = Val(Row!FNSeq.ToString)
                                .Tag = "2|"
                            End With
                        Case "RichEditControl".ToUpper

                            Ctrl = New DevExpress.XtraRichEdit.RichEditControl
                            With CType(Ctrl, DevExpress.XtraRichEdit.RichEditControl)
                                .Name = Row!FTFiledName.ToString

                                .ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.ReadOnly)
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
                        .TextSize = New Size(Me.LabalWidth, 13)
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

                _QryQuery &= vbCrLf & " FROM   " & Me.TableName & " As M WITH(NOLOCK) "
            End If

        End If

        Me.Query = _QryQuery

        _Qry = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream,FNLabelFormDynamicWidth  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysLayOut With(NOLOCK) WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "
        Dim _dtLayOut As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM,, False)

        If _dtLayOut.Rows.Count > 0 Then
            Try
                olymain.RestoreLayoutFromStream(New MemoryStream(DirectCast(_dtLayOut.Rows(0)!FTLayOutStream, Byte())))
            Catch ex As Exception
            End Try

            If Integer.Parse(Val(_dtLayOut.Rows(0)!FNLabelFormDynamicWidth.ToString)) <> Me.LabalWidth Then
                Call SetLabelSize(Me, Me.LabalWidth)
            End If
        End If

        _dtLayOut.Dispose()




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
                Call SetLabelSize(pObj, NewSize)
            End If

        Next

    End Sub

    Public Sub InitData()
        Dim _FieldName As String

        For I As Integer = 0 To _InitData.ToArray.Count - 1
            _FieldName = _InitData(I).FiledName.ToString

            Dim Pass As Boolean = True
            Dim _Default As String = ""
            _Default = _InitData(I).DataDefaults.ToString

            If (_InitData(I).QueryDefaults) Then
                _Default = HI.Conn.SQLConn.GetField(_Default, _DBEnum, "1")
            End If

            For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit

                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Text = _Default
                        End With

                    Case ENM.Control.ControlType.CalcEdit

                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            .Value = Val(_Default)
                        End With

                    Case ENM.Control.ControlType.ComboBoxEdit

                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            .SelectedIndex = Val(_Default)
                        End With

                    Case ENM.Control.ControlType.CheckEdit

                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                            .Checked = (_Default = "1")
                        End With
                    Case ENM.Control.ControlType.DateEdit
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            .Text = _Default
                        End With
                    Case ENM.Control.ControlType.MemoEdit
                        With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                            .Text = _Default
                        End With
                    Case ENM.Control.ControlType.TextEdit

                        With CType(Obj, DevExpress.XtraEditors.TextEdit)
                            .Text = _Default
                        End With

                    Case ENM.Control.ControlType.RichEditControl

                        With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                            .RtfText = _Default
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

    Private Sub UpdateImage(ByVal Key As String)
        Try

            Dim _Qry As String
            Select Case True
                Case Microsoft.VisualBasic.Right(Me.TableName.ToString.ToUpper, "TCNMCmp".Length) = "TCNMCmp".ToUpper

                    For Each Obj As Object In Me.Controls.Find("FPImageCmpLogo", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit

                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_SYSTEM)
                                    HI.Conn.SQLConn.SqlConnectionOpen()

                                    _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp SET FPCmpImage=@FTLayOutStream "
                                    _Qry &= vbCrLf & " WHERE FNHSysCmpId=@FNGrpObjID "

                                    Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
                                    cmd.Parameters.AddWithValue("@FNGrpObjID", Val(Key).ToString)

                                    Dim ms As New MemoryStream()
                                    .Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                                    Dim data As Byte() = ms.GetBuffer()

                                    Dim p As New SqlParameter("@FTLayOutStream", SqlDbType.Image)
                                    p.Value = data

                                    cmd.Parameters.Add(p)
                                    cmd.ExecuteNonQuery()

                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
                                End With
                        End Select
                    Next

                Case Microsoft.VisualBasic.Right(Me.TableName.ToString.ToUpper, "TMERMStyle".Length) = "TMERMStyle".ToUpper
                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                    HI.Conn.SQLConn.SqlConnectionOpen()

                    _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle "
                    _Qry &= vbCrLf & "  SET FPStyleImage1=@FPStyleImage1"
                    _Qry &= vbCrLf & " ,FPStyleImage2=@FPStyleImage2 "
                    _Qry &= vbCrLf & " ,FPStyleImage3=@FPStyleImage3 "
                    _Qry &= vbCrLf & " ,FPStyleImage4=@FPStyleImage4 "
                    _Qry &= vbCrLf & " ,FNNetCM=ISNULL(FNCM,0) - ISNULL(FNCMDisAmt,0)"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleId=@FNHSysStyleId "

                    Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
                    cmd.Parameters.AddWithValue("@FNHSysStyleId", Integer.Parse(Val(Key)))


                    Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                    For Each Obj As Object In Me.Controls.Find("FPStyleImage1", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                data = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next

                    Dim p As New SqlParameter("@FPStyleImage1", SqlDbType.Image)
                    p.Value = data

                    cmd.Parameters.Add(p)

                    Dim data2 As Byte() = Nothing 'HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserLicense.Image, UL.ULImage.PicType.License)

                    For Each Obj As Object In Me.Controls.Find("FPStyleImage2", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                data2 = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next

                    Dim p2 As New SqlParameter("@FPStyleImage2", SqlDbType.Image)
                    p2.Value = data2

                    cmd.Parameters.Add(p2)


                    Dim data3 As Byte() = Nothing 'HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserLicense.Image, UL.ULImage.PicType.License)

                    For Each Obj As Object In Me.Controls.Find("FPStyleImage3", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                data3 = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next

                    Dim p3 As New SqlParameter("@FPStyleImage3", SqlDbType.Image)
                    p3.Value = data3

                    cmd.Parameters.Add(p3)


                    Dim data4 As Byte() = Nothing 'HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserLicense.Image, UL.ULImage.PicType.License)

                    For Each Obj As Object In Me.Controls.Find("FPStyleImage4", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                data4 = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next

                    Dim p4 As New SqlParameter("@FPStyleImage4", SqlDbType.Image)
                    p4.Value = data4

                    cmd.Parameters.Add(p4)

                    cmd.ExecuteNonQuery()
                    cmd.Parameters.Clear()
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _FoundGen As Boolean
        Dim MapFound As Boolean = False

        If Me.ocmaddnew.Visible Then
            For I As Integer = 0 To _GenAutoByField.ToArray.Count - 1
                _FieldName = _GenAutoByField(I).FiledName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Try

                        MapFound = False

                        For Each _QryCon As String In _GenAutoByField(I).GenByFiledName.ToString.Split(",")
                            If _FieldName = _QryCon Then
                                MapFound = True
                                Exit For
                            End If
                        Next

                        If Obj.Text.trim() = "" Or (MapFound) Then

                            _FoundGen = True

                            Dim _Data As String = ""

                            For Each _QryCon As String In _GenAutoByField(I).GenByFiledName.ToString.Split(",")
                                For Each ctrl As Object In Me.Controls.Find(_QryCon.Trim(), True)

                                    With DirectCast(ctrl.Parent, DevExpress.XtraLayout.LayoutControl)
                                        Dim _lay As Object = .Items.FindByName(_FieldName)
                                        If Not (_lay Is Nothing) Then
                                            _Caption = _lay.Text
                                        End If
                                    End With

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                If .Text.Trim() = "" Then
                                                    _FoundGen = False
                                                Else
                                                    Select Case Microsoft.VisualBasic.Right(_FieldName, 6)
                                                        Case "NameEN"
                                                            _Data &= " " & .Text.Trim()
                                                        Case "NameTH"
                                                            _Data &= " " & .Text.Trim()
                                                        Case Else
                                                            _Data &= .Text.Trim()
                                                    End Select

                                                End If
                                            End With
                                        Case ENM.Control.ControlType.CalcEdit

                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                If .Text.Trim() = "" Then
                                                    _FoundGen = False
                                                Else
                                                    Select Case Microsoft.VisualBasic.Right(_FieldName, 6)
                                                        Case "NameEN"
                                                            Dim Lang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                                                            HI.ST.Lang.Language = ST.Lang.eLang.EN
                                                            Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)
                                                            For Each ctrl2 As Object In Me.Controls.Find(ctrl.Name.ToString & "_None", True)
                                                                _Data &= " " & ctrl2.Text.Trim()
                                                            Next
                                                            HI.ST.Lang.Language = Lang
                                                            Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)
                                                        Case "NameTH"
                                                            Dim Lang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                                                            HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal
                                                            Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)

                                                            For Each ctrl2 As Object In Me.Controls.Find(ctrl.Name.ToString & "_None", True)
                                                                _Data &= " " & ctrl2.Text.Trim()
                                                            Next

                                                            HI.ST.Lang.Language = Lang
                                                            Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)
                                                        Case Else
                                                            _Data &= .Text.Trim()
                                                    End Select
                                                End If
                                            End With
                                        Case ENM.Control.ControlType.MemoEdit
                                            With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                                If .Text.Trim() = "" Then
                                                    _FoundGen = False
                                                Else
                                                    _Data &= .Text.Trim()
                                                End If
                                            End With
                                        Case ENM.Control.ControlType.DateEdit
                                        Case ENM.Control.ControlType.CheckEdit
                                        Case ENM.Control.ControlType.ComboBoxEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)
                                                If .Text.Trim() = "" Then
                                                    _FoundGen = False
                                                Else
                                                    _Data &= .Text.Trim()
                                                End If
                                            End With
                                    End Select

                                    If Not (_FoundGen) Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                                        ctrl.Focus()
                                        Return False
                                    End If

                                Next
                            Next

                            If _FoundGen And _Data <> "" Then
                                ' Obj.Text = _Data.Trim()

                                Dim mGen As String = _Data.Trim()


                                Select Case True
                                    Case Microsoft.VisualBasic.Right(Me.TableName.ToString.ToUpper, "TMERMRDOperation".Length) = "TMERMRDOperation".ToUpper
                                        Dim cmdstring As String = "SELECT TOP 1 FTRDOperationCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation  WHERE FTRDOperationCode LIKE '" & HI.UL.ULF.rpQuoted(mGen) & "%' ORDER BY FTRDOperationCode DESC "
                                        Dim MaxRun As String = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "")

                                        If MaxRun = "" Then
                                            mGen = mGen & "001"
                                        Else
                                            mGen = mGen & Microsoft.VisualBasic.Right("0000" & (Val(Microsoft.VisualBasic.Right(MaxRun, 3)) + 1).ToString(), 3)
                                        End If

                                End Select

                                Obj.Text = mGen


                            End If
                        End If
                    Catch ex As Exception
                    End Try
                Next
            Next
        End If


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

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                Pass = False
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

        _Val = ""
        Dim _QryCheckDupl As String = ""
        For I As Integer = 0 To _CheckDuplFiled.ToArray.Count - 1
            _FieldName = _CheckDuplFiled(I).FiledName.ToString

            If _QryCheckDupl <> "" Then _QryCheckDupl &= " AND "
            For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                With DirectCast(Obj.Parent, DevExpress.XtraLayout.LayoutControl)
                    Dim _lay As Object = .Items.FindByName(_FieldName)
                    If Not (_lay Is Nothing) Then
                        _Caption = _lay.Text
                    End If
                End With

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            _Val &= "  " & _Caption & " = " & .Text
                            _QryCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.Properties.Tag.ToString) & "' "
                        End With
                    Case ENM.Control.ControlType.CalcEdit
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            _Val &= "  " & _Caption & " = " & .Value.ToString
                            _QryCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(.Value) & " "
                        End With
                    Case ENM.Control.ControlType.ComboBoxEdit
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            If "" & .Properties.Tag.ToString <> "" Then
                                _QryCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)) & " "
                            Else
                                _QryCheckDupl &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(.SelectedIndex) & " "
                            End If
                        End With
                    Case ENM.Control.ControlType.CheckEdit

                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                            _QryCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.EditValue) & "' "
                        End With
                    Case ENM.Control.ControlType.DateEdit
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            _Val &= "  " & _Caption & " = " & .Text
                            Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                Case "FD"
                                    _QryCheckDupl &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(.Text) & "' "
                                Case Else
                                    _QryCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                            End Select
                        End With
                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                        _Val &= "  " & _Caption & " = " & Obj.Text
                        _QryCheckDupl &= _FieldName & "='" & HI.UL.ULF.rpQuoted(Obj.Text) & "' "
                End Select

            Next

        Next

        Dim _Qry As String = ""

        If _QryCheckDupl <> "" Then

            Dim _QryWhere As String = ""
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _QryWhere = "" Then
                    _QryWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "<>'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                Else
                    _QryWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "<>'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                End If
            Next

            _Qry = " Select Top 1 " & Me.MainKey & ""
            _Qry &= vbCrLf & "  From " & Me.TableName & " WITH (NOLOCK) " & " "
            _Qry &= vbCrLf & " " & _QryWhere
            _Qry &= vbCrLf & "  AND   " & _QryCheckDupl

            If HI.Conn.SQLConn.GetField(_Qry, _DBEnum, "") <> "" Then

                If Not (Me.ObjectFocus Is Nothing) Then
                    Me.ObjectFocus.Focus()
                End If

                HI.MG.ShowMsg.mProcessError(1301110001, "", Me.Text, MessageBoxIcon.Error, _Val)


                If Me.ocmaddnew.Visible And Not (_StateProcCopy) Then
                    For I As Integer = 0 To _GenAutoByField.ToArray.Count - 1
                        _FieldName = _GenAutoByField(I).FiledName.ToString

                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            Try

                                MapFound = False

                                For Each _QryCon As String In _GenAutoByField(I).GenByFiledName.ToString.Split(",")
                                    If _FieldName = _QryCon Then
                                        MapFound = True
                                        Exit For
                                    End If
                                Next

                                If Obj.Text.trim() <> "" Or (MapFound) Then

                                    _FoundGen = True

                                    Dim _Data As String = ""

                                    For Each _QryCon As String In _GenAutoByField(I).GenByFiledName.ToString.Split(",")
                                        For Each ctrl As Object In Me.Controls.Find(_QryCon.Trim(), True)

                                            With DirectCast(ctrl.Parent, DevExpress.XtraLayout.LayoutControl)
                                                Dim _lay As Object = .Items.FindByName(_FieldName)
                                                If Not (_lay Is Nothing) Then
                                                    _Caption = _lay.Text
                                                End If
                                            End With

                                            Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                                Case ENM.Control.ControlType.TextEdit
                                                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                        If .Text.Trim() = "" Then
                                                            _FoundGen = False
                                                        Else
                                                            Select Case Microsoft.VisualBasic.Right(_FieldName, 6)
                                                                Case "NameEN"
                                                                    _Data &= " " & .Text.Trim()
                                                                Case "NameTH"
                                                                    _Data &= " " & .Text.Trim()
                                                                Case Else
                                                                    _Data &= .Text.Trim()
                                                            End Select

                                                        End If
                                                    End With
                                                Case ENM.Control.ControlType.CalcEdit

                                                Case ENM.Control.ControlType.ButtonEdit
                                                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                        If .Text.Trim() = "" Then
                                                            _FoundGen = False
                                                        Else
                                                            Select Case Microsoft.VisualBasic.Right(_FieldName, 6)
                                                                Case "NameEN"
                                                                    Dim Lang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                                                                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                                                                    Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)
                                                                    For Each ctrl2 As Object In Me.Controls.Find(ctrl.Name.ToString & "_None", True)
                                                                        _Data &= " " & ctrl2.Text.Trim()
                                                                    Next
                                                                    HI.ST.Lang.Language = Lang
                                                                    Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)
                                                                Case "NameTH"
                                                                    Dim Lang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                                                                    HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal
                                                                    Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)

                                                                    For Each ctrl2 As Object In Me.Controls.Find(ctrl.Name.ToString & "_None", True)
                                                                        _Data &= " " & ctrl2.Text.Trim()
                                                                    Next

                                                                    HI.ST.Lang.Language = Lang
                                                                    Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(ctrl, New System.EventArgs)
                                                                Case Else
                                                                    _Data &= .Text.Trim()
                                                            End Select
                                                        End If
                                                    End With
                                                Case ENM.Control.ControlType.MemoEdit

                                                Case ENM.Control.ControlType.DateEdit
                                                Case ENM.Control.ControlType.CheckEdit
                                                Case ENM.Control.ControlType.ComboBoxEdit

                                                    With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)

                                                        If .Text.Trim() = "" Then
                                                            _FoundGen = False
                                                        Else
                                                            _Data &= .Text.Trim()
                                                        End If

                                                    End With

                                            End Select

                                        Next

                                    Next

                                    If _FoundGen And _Data <> "" Then

                                        Dim mGen As String = Obj.Text.Replace(_Data.Trim(), "")

                                        Select Case True
                                            Case Microsoft.VisualBasic.Right(Me.TableName.ToString.ToUpper, "TMERMRDOperation".Length) = "TMERMRDOperation".ToUpper

                                                Dim cmdstring As String = "SELECT TOP 1 FTRDOperationCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation  WHERE FTRDOperationCode LIKE '" & HI.UL.ULF.rpQuoted(mGen) & "%' ORDER BY FTRDOperationCode DESC "
                                                Dim MaxRun As String = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "")

                                                If MaxRun = "" Then
                                                    mGen = mGen & "001"
                                                Else
                                                    mGen = mGen & Microsoft.VisualBasic.Right("0000" & (Val(Microsoft.VisualBasic.Right(MaxRun, 3)) + 1).ToString(), 3)
                                                End If

                                        End Select

                                        Obj.Text = mGen

                                    End If
                                End If
                            Catch ex As Exception
                            End Try
                        Next
                    Next
                End If

                Return False
            End If
        End If

        Return True
    End Function

    Private Sub LoadDataEdit(ByVal HSysId As String)

        Dim _QryWhere As String = ""
        For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
            If _QryWhere = "" Then
                _QryWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
            Else
                _QryWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
            End If
        Next

        Dim _Qry As String = Me.Query & "   " & _QryWhere

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, _DBEnum,, False)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
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

                                    If "" & .Properties.Tag.ToString <> "" Then
                                        .SelectedIndex = HI.TL.CboList.GetIndexByValue(.Properties.Tag.ToString, R.Item(Col).ToString)
                                    Else
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    End If

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

                        Case ENM.Control.ControlType.RichEditControl
                            With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                .HtmlText = R.Item(Col).ToString
                            End With
                        Case ENM.Control.ControlType.PictureEdit

                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)

                                Try

                                    Select Case Microsoft.VisualBasic.Left(_FieldName, "FPStyleImage".Length)
                                        Case "FPStyleImage"

                                            Try
                                                .Image = HI.UL.ULImage.ConvertByteArrayToImmage(R.Item(Col))
                                            Catch ex As Exception
                                                .Image = Nothing
                                            End Try

                                        Case Else
                                            .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                    End Select

                                Catch ex As Exception
                                    .Image = Nothing
                                End Try

                            End With

                        Case ENM.Control.ControlType.DateEdit

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

                        Case ENM.Control.ControlType.TimeEdit

                            Try
                                With CType(Obj, DevExpress.XtraEditors.TimeEdit)

                                    If R.Item(Col).ToString = "" Then
                                        .Text = ""
                                    Else
                                        .Time = R.Item(Col).ToString
                                    End If

                                End With
                            Catch ex As Exception
                            End Try

                        Case Else

                            Obj.Text = R.Item(Col).ToString

                    End Select
                Next
            Next

            If FTProcValidateEdit <> "" Then
                Call ProcValidateEdit()
            End If
            Exit For
        Next
        Me.ocmexit.Focus()
    End Sub


    Private Sub SetReadOnlyField()
        Try

            Dim _FieldName As String = ""
            For I As Integer = 0 To _ReadOnlyField.ToArray.Count - 1
                _FieldName = _ReadOnlyField(I).FiledName.ToString


                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Properties.ReadOnly = True
                                Try
                                    .Properties.Buttons(0).Enabled = False
                                Catch ex As Exception
                                End Try
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.TextEdit
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.MemoEdit
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Properties.ReadOnly = True
                            End With
                        Case ENM.Control.ControlType.RichEditControl
                            With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                .ReadOnly = True
                            End With

                    End Select
                Next
            Next



        Catch ex As Exception

        End Try
    End Sub


    Private Sub ProcValidateEdit()
        Try

            Dim _QryWhere As String = ""
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _QryWhere = "" Then
                    _QryWhere = "'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                Else
                    _QryWhere &= ",'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                End If
            Next

            Dim dt As DataTable
            Dim _Qry As String = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo." & FTProcValidateEdit & " " & _QryWhere

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            Dim _FieldName As String = ""
            For Each R As DataRow In dt.Rows

                _FieldName = R!FiledName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                                Try
                                    .Properties.Buttons(0).Enabled = Not ((R!State.ToString = "1"))
                                Catch ex As Exception
                                End Try
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.TextEdit
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.MemoEdit
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Properties.ReadOnly = (R!State.ToString = "1")
                            End With

                        Case ENM.Control.ControlType.RichEditControl
                            With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                .ReadOnly = (R!State.ToString = "1")
                            End With

                    End Select
                Next
            Next

            dt.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ProcessSaveData()
        Try

            Dim _QryWhere As String = ""
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _QryWhere = "" Then
                    _QryWhere = "'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                Else
                    _QryWhere &= ",'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                End If
            Next

            Dim _Qry As String = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo." & FTProcSave & " " & _QryWhere

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadGroupRange()
        Try
            Dim dt As DataTable
            Dim _QryWhere As String = ""
            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _QryWhere = "" Then
                    _QryWhere = "AND sd.FNHSysSizeRangeId = '" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                Else
                    _QryWhere &= "AND sd.FNHSysSizeRangeId = '" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                End If
            Next

            Dim _Qry As String = "SELECT CASE WHEN ISNULL(sd.FNHSysMatSizeId,'0') <> '0' THEN '1' ELSE '0' END AS 'FTSelect'"
            _Qry &= vbCrLf & ", s.FTMatSizeCode, s.FTMatSizeNameEN, s.FTMatSizeNameTH, s.FTStateActive, s.FNHSysMatSizeId "

            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize As s With (NOLOCK) "
            _Qry &= vbCrLf & "OUTER APPLY (SELECT sd.* FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSizeRangeDetail As sd With (NOLOCK) "
            _Qry &= vbCrLf & "WHERE sd.FNHSysMatSizeId = s.FNHSysMatSizeId " & _QryWhere & ") AS sd "


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            ogcSize.DataSource = dt

        Catch ex As Exception
        End Try
    End Sub

    Private Function CheckNotUsed(ByVal Key As String) As Boolean
        Dim _Qry As String = ""

        For I As Integer = 0 To _CheckDelFiled.ToArray.Count - 1
            _Qry = _CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                Return False
            End If

        Next

        Return True
    End Function

#End Region

#Region "General"

    Private Sub Form_Activated(sender As Object, e As System.EventArgs) Handles MyBase.Activated
        If Not (Me.ObjectFocus Is Nothing) Then
            Me.ObjectFocus.Focus()
            Me.ObjectFocus.SelectAll()
        End If

    End Sub

    Private Sub Form_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.ocmclear.Enabled = True
            If Me.TableName <> "" Then
                _Bindgrid = False
                Me.Preform()

                Dim _FieldName As String = ""

                For I As Integer = 0 To _TableField.ToArray.Count - 1
                    _FieldName = _TableField(I).FiledName.ToString

                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Try
                            Obj.Properties.ReadOnly = False
                        Catch ex As Exception
                        End Try
                    Next
                Next

                For I As Integer = 0 To _CheckCopyField.ToArray.Count - 1
                    _FieldName = _CheckCopyField(I).FiledName.ToString

                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    .Properties.ReadOnly = _StateProcCopy

                                    Try
                                        .Properties.Buttons(0).Enabled = _StateProcCopy
                                    Catch ex As Exception
                                    End Try
                                End With
                            Case ENM.Control.ControlType.CalcEdit
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With
                            Case ENM.Control.ControlType.ComboBoxEdit
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With
                            Case ENM.Control.ControlType.CheckEdit
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With
                            Case ENM.Control.ControlType.PictureEdit
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With
                            Case ENM.Control.ControlType.TextEdit
                                With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With
                            Case ENM.Control.ControlType.DateEdit
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With
                            Case ENM.Control.ControlType.MemoEdit
                                With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                    .Properties.ReadOnly = _StateProcCopy
                                End With

                            Case ENM.Control.ControlType.RichEditControl
                                With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                    .ReadOnly = _StateProcCopy
                                End With
                        End Select
                    Next
                Next

                _FieldName = ""
                For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                    _FieldName = _KeyFiled(I).FiledName.ToString
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")

                                    Try
                                        .Properties.Buttons(0).Enabled = Not ((_KeyFiled(I).FiledValue.ToString <> ""))
                                    Catch ex As Exception
                                    End Try

                                End With
                            Case ENM.Control.ControlType.CalcEdit
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.ComboBoxEdit
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.CheckEdit
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.PictureEdit
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.TextEdit
                                With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.DateEdit
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.MemoEdit
                                With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                    .Properties.ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                            Case ENM.Control.ControlType.RichEditControl
                                With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                    .ReadOnly = (_KeyFiled(I).FiledValue.ToString <> "")
                                End With
                        End Select

                    Next
                Next

                _FieldName = ""

                For I As Integer = 0 To _LockFiled.ToArray.Count - 1
                    _FieldName = _LockFiled(I).FiledName.ToString
                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))

                                    Try
                                        .Properties.Buttons(0).Enabled = Not (((_LockFiled(I).FiledValue.ToString <> "")) And Not (_StateProcCopy))
                                    Catch ex As Exception
                                    End Try

                                End With
                            Case ENM.Control.ControlType.CalcEdit
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                            Case ENM.Control.ControlType.ComboBoxEdit
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                            Case ENM.Control.ControlType.CheckEdit
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                            Case ENM.Control.ControlType.PictureEdit
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                            Case ENM.Control.ControlType.TextEdit
                                With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                            Case ENM.Control.ControlType.DateEdit
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                            Case ENM.Control.ControlType.MemoEdit
                                With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                    .Properties.ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With

                            Case ENM.Control.ControlType.RichEditControl
                                With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                    .ReadOnly = ((_LockFiled(I).FiledValue.ToString <> "") And Not (_StateProcCopy))
                                End With
                        End Select

                    Next
                Next

                If _KeyFiled(0).FiledValue.ToString <> "" Then
                    Call LoadDataEdit(Me.MainKeyID)
                End If

                If (_StateProcCopy) Then
                    Me.MainKeyID = 0
                    For I As Integer = 0 To Me._KeyFiled.ToArray.Count - 1
                        Me._KeyFiled(I).FiledValue = ""
                    Next


                    If Me.FTNotCopyField <> "" Then

                        _FieldName = ""

                        For Each Sctrl As String In Me.FTNotCopyField.Split(",")

                            _FieldName = Sctrl
                            For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            .Text = ""

                                            Try
                                                .Properties.Tag = 0
                                            Catch ex As Exception
                                            End Try

                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            .Value = 0
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            .SelectedIndex = 0
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            .Checked = False
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            .Image = Nothing
                                        End With
                                    Case ENM.Control.ControlType.TextEdit
                                        With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                            .Text = ""
                                        End With
                                    Case ENM.Control.ControlType.DateEdit
                                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                            .Text = ""
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit
                                        With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                            .Text = ""
                                        End With

                                    Case ENM.Control.ControlType.RichEditControl
                                        With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                            .HtmlText = ""
                                        End With
                                End Select

                            Next
                        Next

                    End If
                End If

                If Me.ActiveLang <> HI.ST.Lang.Language Then
                    HI.ST.Lang.SP_SETxLanguage(Me)
                    Me.ActiveLang = HI.ST.Lang.Language
                End If


                Call SetReadOnlyField()

            Else
                HI.MG.ShowMsg.mProcessError(1301110002, "", Me.Text)
                Me.Close()
            End If

            'Try
            '    For Each Obj As Object In Me.Controls.Find("", True)

            '    Next

            '    For Each Obj As Object In Me.Controls.Find.OfType(Of Xtra

            '    Next
            'Catch ex As Exception

            'End Try
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message, Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub sbCustomization_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sbCustomization.Click
        Me.olymain.ShowCustomizationForm()
    End Sub

    Private Sub ocmsavelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsavelayout.Click

        Dim _Qry As String = ""

        If (HI.ST.SysInfo.DevelopMode) Then

            _Qry = " SELECT  TOP 1  FNGrpObjID, FTLayOutStream  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysLayOut With(NOLOCK) WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysLayOut(FNGrpObjID, FTLayOutStream,FNLabelFormDynamicWidth) VALUES(@FNGrpObjID,@FTLayOutStream,@FNWidth)"
                Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.FormObjID.ToString)

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

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysLayOut SET FTLayOutStream=@FTLayOutStream,FNLabelFormDynamicWidth=@FNWidth WHERE  FNGrpObjID=@FNGrpObjID "
                Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FNGrpObjID", Me.FormObjID.ToString)

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

        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm SET FNFormPopUpWidth=" & Me.Width & ",FNFormPopUpHeight=" & Me.Height & ""
        _Qry &= " WHERE FNFormObjID=" & Me.FormObjID & " "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        MsgBox("Save Object Layout Complete")

    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogcSize.Refresh()
        Me.Close()
    End Sub

    Private Sub ocmaddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddnew.Click

        If VerifyData() Then

            Dim _FieldName As String
            Dim _Fields As String = ""
            Dim _Values As String = ""
            Dim _Qry As String
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
                                    If "" & .Properties.Tag.ToString <> "" Then
                                        _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                    Else
                                        _Val = .SelectedIndex.ToString
                                    End If
                                End With
                            Case ENM.Control.ControlType.CheckEdit
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
                            Case ENM.Control.ControlType.PictureEdit
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                End With
                            Case ENM.Control.ControlType.RichEditControl
                                With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                    _Val = .HtmlText
                                End With
                            Case ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TimeEdit
                                _Val = Trim(Obj.Text)
                            Case ENM.Control.ControlType.MemoEdit
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

                Select Case Microsoft.VisualBasic.Left(_FieldName, "FPStyleImage".Length)
                    Case "FPStyleImage"
                    Case Else
                        _Val = ""
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            If UCase(_FieldName) = Me.MainKey.ToUpper And _Key <> "" Then
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
                                            If "" & .Properties.Tag.ToString <> "" Then
                                                _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                            Else
                                                _Val = .SelectedIndex.ToString
                                            End If
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
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
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _KeyFiled(0).FiledValue.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.RichEditControl
                                        With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                            _Val = .HtmlText
                                        End With
                                    Case ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TimeEdit
                                        _Val = Trim(Obj.Text)
                                    Case ENM.Control.ControlType.MemoEdit
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
                                    Case "FR"
                                        _Values &= "'" & (_Val) & "'"
                                    Case Else
                                        _Values &= "N'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                End Select
                        End Select

                End Select
            Next

            _Qry = " INSERT INTO   " & Me.TableName & "(" & _Fields & ") VALUES (" & _Values & ")"

            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SYSTEM) Then
                Me.ProcComplete = True
                Call UpdateImage(_Key)

                Call SaveGridDataSize(_Key)

                If Me.FTProcSave <> "" Then
                    Call ProcessSaveData()
                End If

                HI.TL.HandlerControl.ClearControl(Me)

                Parent_Form.Preform()

                For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                    _KeyFiled(I).FiledValue = ""
                Next

                Call InitData()

                If Not (Me.ObjectFocus Is Nothing) Then
                    Me.ObjectFocus.Focus()
                End If

                If (_StateProcCopy) Then
                    For I As Integer = 0 To _CheckCopyField.ToArray.Count - 1
                        _FieldName = _CheckCopyField(I).FiledName.ToString

                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            Select Case HI.ENM.Control.GeTypeControl(Obj)
                                Case ENM.Control.ControlType.ButtonEdit
                                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.CalcEdit
                                    With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.ComboBoxEdit
                                    With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.CheckEdit
                                    With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.PictureEdit
                                    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.TextEdit
                                    With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.DateEdit
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.MemoEdit
                                    With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                        .Properties.ReadOnly = False
                                    End With
                                Case ENM.Control.ControlType.RichEditControl
                                    With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                        .ReadOnly = False
                                    End With
                            End Select
                        Next
                    Next

                    _StateProcCopy = False
                    Me.Close()
                End If

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

            LoadGroupRange()

        End If
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        Me.Preform()
        HI.TL.HandlerControl.ClearControl(Me)
        LoadGroupRange()
    End Sub

    Private Sub ocmedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmedit.Click
        If VerifyData() Then

            Dim _FieldName As String
            Dim _Fields As String = ""
            Dim _Values As String = ""
            Dim _Qry As String
            Dim _Val As String = ""
            Dim _QryWhere As String = ""
            Dim _ID As String = ""

            For I As Integer = 0 To _KeyFiled.ToArray.Count - 1
                If _QryWhere = "" Then
                    _QryWhere = "  WHERE  " & _KeyFiled(I).FiledName.ToString & "=N'" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                    _ID = HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString)
                Else
                    _QryWhere &= "  AND  " & _KeyFiled(I).FiledName.ToString & "='" & HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString) & "' "
                    _ID = HI.UL.ULF.rpQuoted(_KeyFiled(I).FiledValue.ToString)
                End If
            Next

            ' --------------------------- Save Size --------------------------- 
            SaveGridDataSize(_ID)

            If _QryWhere = "" Then Exit Sub
            Dim _FoundKey As Boolean = False
            For I As Integer = 0 To _BaseFiled.ToArray.Count - 1


                _FieldName = _BaseFiled(I).FiledName.ToString

                Select Case Microsoft.VisualBasic.Left(_FieldName, "FPStyleImage".Length)
                    Case "FPStyleImage"

                    Case Else

                        _FoundKey = False
                        For K As Integer = 0 To _KeyFiled.ToArray.Count - 1
                            If _KeyFiled(K).FiledName.ToString = _FieldName.ToString Then
                                _Val = _KeyFiled(K).FiledValue.ToString
                                _FoundKey = True
                            End If
                        Next

                        If Not (_FoundKey) Then
                            For Each Obj As Object In Me.Controls.Find(_FieldName, True)

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
                                            If "" & .Properties.Tag.ToString <> "" Then
                                                _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                            Else
                                                _Val = .SelectedIndex.ToString
                                            End If

                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
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
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _KeyFiled(0).FiledValue.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With

                                    Case ENM.Control.ControlType.RichEditControl
                                        With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                                            _Val = .HtmlText
                                        End With

                                    Case ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Trim(Obj.Text)
                                    Case ENM.Control.ControlType.MemoEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            Next
                        End If

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
                                    Case "FR"
                                        _Values &= _FieldName & "='" & _Val & "'"
                                    Case Else

                                        _Values &= _FieldName & "=N'" & HI.UL.ULF.rpQuoted(_Val) & "'"

                                End Select
                        End Select

                End Select


            Next


            _Qry = " Update  " & Me.TableName & " Set " & _Values & "   " & _QryWhere

            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SYSTEM) Then

                Me.ProcComplete = True

                Call UpdateImage(_KeyFiled(0).FiledValue.ToString)

                If Me.FTProcSave <> "" Then
                    Call ProcessSaveData()
                End If

                HI.TL.HandlerControl.ClearControl(Me)

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.Close()

                LoadGroupRange()

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If


        End If

    End Sub


    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        Dim _Key As String = Me.MainKeyID

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


        If Me.CheckNotUsed(_Key) = False Then Exit Sub

        If Me.TableNameOrg.ToUpper = "TMERMMainMat".ToUpper Then
            Dim _Val As String = ""
            For Each Obj As Object In Me.Controls.Find("FTMainMatCode", True)

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
                            If "" & .Properties.Tag.ToString <> "" Then
                                _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                            Else
                                _Val = .SelectedIndex.ToString
                            End If

                        End With
                    Case ENM.Control.ControlType.CheckEdit
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
                    Case ENM.Control.ControlType.PictureEdit
                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _KeyFiled(0).FiledValue.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                        End With
                    Case ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                        _Val = Trim(Obj.Text)
                    Case ENM.Control.ControlType.MemoEdit
                        _Val = Obj.Text
                    Case Else
                        _Val = Obj.Text
                End Select
            Next

            Dim _QryCheck As String = "  SELECT TOP 1 FTRawMatCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WITH(NOLOCK) WHERE FTRawMatCode='" & HI.UL.ULF.rpQuoted(_Val) & "' AND (FNHSysRawMatColorId <> 0 OR FNHSysRawMatSizeId<>0 ) "

            If HI.Conn.SQLConn.GetField(_QryCheck, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                Exit Sub
            End If

        End If

        If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete) = True Then

            Dim _FieldName As String = ""
            For I As Integer = 0 To _BaseFiled.ToArray.Count - 1
                _FieldName = _BaseFiled(I).FiledName.ToString
                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.PictureEdit
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

    Private Sub ocmdeletelayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdeletelayout.Click

        If (HI.ST.SysInfo.DevelopMode) Then
            Dim _Qry As String = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysLayOut WHERE FNGrpObjID =" & Val(Me.FormObjID.ToString) & " "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
            MsgBox("Delete Object Layout Complete")
        End If
    End Sub

    Private Sub ogcacc_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcSize.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                'With Me.ogvacc
                '    If .FocusedRowHandle < 0 Then Exit Sub
                '    .DeleteRow(.FocusedRowHandle)

                'End With

                With CType(Me.ogcSize.DataSource, DataTable)
                    .AcceptChanges()
                    .BeginInit()

                    For Each R As DataRow In .Select("FTSelect='1'", "FNSeq")
                        R.Delete()
                    Next

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNSeq>0", "FNSeq")
                        R!FNSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                'InitGridDataAcc()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridDataAcc()

            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub InitGridDataAcc()
        Try
            If Not (Me.ogcSize.DataSource Is Nothing) Then

                With CType(Me.ogcSize.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTSelect='1'").Count = 0 Then
                        .Rows.Add("0", .Rows.Count + 1, "")
                    End If
                End With

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveGridDataSize(ByVal _ID As String)
        Try
            If Not (Me.ogcSize.DataSource Is Nothing) Then
                With CType(Me.ogcSize.DataSource, DataTable)
                    .AcceptChanges()
                    Dim _Qry As String = ""
                    Dim _QryWhere As String = ""

                    If .Select().Count > 0 Then
                        'For Each R As DataRow In .Select()
                        '    If _QryWhere = "" Then
                        '        _QryWhere &= vbCrLf & "(sd.FNHSysSizeRangeId = '" & _ID & "' AND sd.FNHSysMatSizeId = '" & R!FNHSysMatSizeId & "')"
                        '    Else
                        '        _QryWhere &= vbCrLf & " OR (sd.FNHSysSizeRangeId = '" & _ID & "' AND sd.FNHSysMatSizeId = '" & R!FNHSysMatSizeId & "')"
                        '    End If
                        'Next
                        _Qry = "Declare @Date varchar(10) = Convert(varchar(10), Getdate(), 111) "
                        _Qry &= vbCrLf & "Declare @Time varchar(10) = Convert(varchar(8), Getdate(), 114) "
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "If (SELECT Case WHEN (SELECT COUNT(sd.FNHSysMatSizeId) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSizeRangeDetail  AS sd WITH (NOLOCK) WHERE "
                        '_Qry &= vbCrLf & _QryWhere
                        _Qry &= vbCrLf & " sd.FNHSysSizeRangeId = '" & _ID & "'"
                        _Qry &= vbCrLf & ") = " & .Select().Count & " THEN 'TRUE' END ) = 'TRUE'"
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "BEGIN"
                        _Qry &= vbCrLf & "   Print 'MATCH (Don''t do anything...)'"
                        _Qry &= vbCrLf & "End"
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "Else"
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "BEGIN"
                        _Qry &= vbCrLf & "   Print 'NOT MATCH !!!'"
                        _Qry &= vbCrLf & "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSizeRangeDetail "
                        _Qry &= vbCrLf & "WHERE FNHSysSizeRangeId = '" & _ID & "' "
                        _Qry &= vbCrLf

                        For Each R As DataRow In .Select("FTSelect = '1'")
                            _Qry &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSizeRangeDetail "
                            _Qry &= vbCrLf & "(FNHSysSizeRangeId, FNHSysMatSizeId,  FDInsDate, FTInsTime, FTInsUser) VALUES(" & _ID & "," & R!FNHSysMatSizeId & ",@Date , @Time,'" & HI.ST.UserInfo.UserName & "') "
                            _Qry &= vbCrLf
                        Next

                        _Qry &= vbCrLf & "End"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

                    Else
                        _Qry = "Declare @Date varchar(10) = Convert(varchar(10), Getdate(), 111) "
                        _Qry &= vbCrLf & "Declare @Time varchar(10) = Convert(varchar(8), Getdate(), 114) "
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "If EXISTS(SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle_Size AS SS WITH (NOLOCK) "
                        _Qry &= vbCrLf & "WHERE SS.FNHSysStyleId = '" & _ID & "' )"
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "BEGIN"
                        _Qry &= vbCrLf & "   Print 'FOUND'"
                        _Qry &= vbCrLf & "   DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSizeRangeDetail "
                        _Qry &= vbCrLf & "   WHERE FNHSysSizeRangeId = '" & _ID & "' "
                        _Qry &= vbCrLf & "End"
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "Else"
                        _Qry &= vbCrLf
                        _Qry &= vbCrLf & "BEGIN"
                        _Qry &= vbCrLf & "   Print 'NOT FOUND!!!'"
                        _Qry &= vbCrLf & "End"
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

                    End If

                End With

            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class