Imports DevExpress.Utils
Imports System.Drawing
Imports DevExpress.XtraEditors
Imports System.Windows.Forms

Public Class DynamicForm

    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Sub New(GrpObjID As Integer, ObjID As Integer, Data As DataTable, oForm As System.Windows.Forms.Form)

        Dim _FieldName As String = ""
        Dim _ColCount As Integer = 0
        Dim _Str As String = ""

        Dim _dt As DataTable
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1

        Dim _CtrHeight As Double = 0
        Dim _SortField As String

        Me.SysForm = oForm

        _Str = "SELECT FNGrpObjID,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FNFormObjID=" & ObjID & " "
        _Str &= vbCrLf & " ORDER BY FNGrpObjSeq ASC  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            Me.TableName = _dt.Rows(0)!FTTableName.ToString
            _SortField = _dt.Rows(0)!FTSortField.ToString

            _StrQuery = "  Select TOP 1  "

            For Each Row As DataRow In Data.Select("FTType='H' AND FNObjID=" & ObjID & " ", "FNCtrlLevel,FNCtrlLevelSeq")
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
                _FieldName = Row!FTFiledName.ToString

                If _FieldName.ToLower = "FTOrderNoRef".ToLower() Then
                    Beep()
                End If

                If Row!FTDefaultsData.ToString <> "" Then
                    Dim _md As New HI.TL.DefaultsData
                    _md.FiledName = Row!FTFiledName.ToString

                    Select Case UCase(Row!FTDefaultsData.ToString)
                        Case "@USER".ToUpper
                            _md.DataDefaults = HI.ST.UserInfo.UserName
                        Case "@DATE".ToUpper
                            _md.DataDefaults = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                        Case "@CMPID".ToUpper
                            _md.DataDefaults = HI.ST.SysInfo.CmpID.ToString
                        Case "@CMP".ToUpper
                            _md.DataDefaults = HI.ST.SysInfo.CmpCode
                        Case "@USERWINDOW".ToUpper
                            _md.DataDefaults = HI.ST.UserInfo.WindowUserName
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

                        _Str = "  SELECT        FTFiledName, FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName"
                        _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjDynamic_D AS D WITH (NOLOCK)"
                        _Str &= vbCrLf & " WHERE    (LEFT(FTFiledName, LEN('" & Row!FTFiledName.ToString & "')) = '" & Row!FTFiledName.ToString & "')"
                        _Str &= vbCrLf & "  AND      (ISNULL(FTStaNoneBase, '') <> 'Y') "
                        _Str &= vbCrLf & "  AND  FTBaseName + '.' + FTPrefix + '.' + FTTableName <> '" & Me.TableName & "' "

                        Dim _dtchk As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

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
                End If

                'If Row!FTValidate.ToString = "Y" And Row!FTPK.ToString <> "Y" Then
                If Row!FTValidate.ToString = "Y" Then
                    Dim _m As New HI.TL.CheckFiled
                    _m.FiledName = Row!FTFiledName.ToString
                    _CheckFiled.Add(_m)

                    For Each ObjCaption As Object In oForm.Controls.Find(_FieldName & "_lbl", True)
                        If ObjCaption.GetType.FullName.ToString.ToUpper = "DevExpress.XtraEditors.LabelControl".ToUpper Then
                            With CType(ObjCaption, DevExpress.XtraEditors.LabelControl)
                                .ForeColor = Color.Blue
                            End With
                            Exit For
                        End If
                    Next

                End If

                For Each Obj As Object In oForm.Controls.Find(_FieldName, True)

                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit

                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                If Row!FTStaTextUpper.ToString = "Y" Then
                                    .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                End If

                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try

                                '.Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = Not (.Properties.ReadOnly)

                            End With

                        Case ENM.Control.ControlType.CalcEdit

                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = 0
                                .Properties.Precision = Val(Row!FNNumericScale.ToString)
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                .Properties.DisplayFormat.FormatType = FormatType.Numeric
                                .Properties.DisplayFormat.FormatString = "N" & Val(Row!FNNumericScale.ToString).ToString
                                '.Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try
                                .TabStop = Not (.Properties.ReadOnly)
                            End With

                        Case ENM.Control.ControlType.ComboBoxEdit

                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                    .TabStop = Not (.Properties.ReadOnly)
                                Catch ex As Exception
                                End Try
                            End With

                        Case ENM.Control.ControlType.CheckEdit

                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Properties.ValueChecked = "1"
                                .Properties.ValueUnchecked = "0"

                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try

                                .TabStop = Not (.Properties.ReadOnly)
                            End With

                        Case ENM.Control.ControlType.PictureEdit

                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                .Properties.ReadOnly = (Row!FTStateReadOnly.ToString = "Y")
                                .TabStop = False
                                .Properties.Tag = _SysPath & Row!FTFolderImgName.ToString
                                .Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch

                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try

                            End With

                        Case ENM.Control.ControlType.TextEdit

                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try
                                If Row!FTStaTextUpper.ToString = "Y" Then
                                    .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                End If


                            End With

                        Case ENM.Control.ControlType.DateEdit

                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
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
                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try

                                .TabStop = Not (.Properties.ReadOnly)

                            End With

                        Case ENM.Control.ControlType.MemoEdit

                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                Try
                                    .TabIndex = Val(Row!FNSeq.ToString)
                                Catch ex As Exception
                                End Try
                                .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                .TabStop = Not (.Properties.ReadOnly)
                            End With

                    End Select

                Next

                _ColCount = _ColCount + 1

            Next

            _StrQuery &= vbCrLf & " FROM   " & Me.TableName & " As M WITH(NOLOCK) "
        End If
    End Sub


    Private _SysForm As System.Windows.Forms.Form
    Public Property SysForm As System.Windows.Forms.Form
        Get
            Return _SysForm
        End Get
        Set(ByVal value As System.Windows.Forms.Form)
            _SysForm = value
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

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(ByVal value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(ByVal value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysObjID As Integer
    Public Property SysObjID As Integer
        Get
            Return _SysObjID
        End Get
        Set(ByVal value As Integer)
            _SysObjID = value
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

    Private _StrQuery As String = ""
    ReadOnly Property Query As String
        Get
            Return _StrQuery
        End Get
    End Property

    Private _KeyFiled As New List(Of HI.TL.PKFiled)()
    ReadOnly Property KeyFiled As List(Of HI.TL.PKFiled)
        Get
            Return _KeyFiled
        End Get
    End Property

    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    ReadOnly Property CheckFiled As List(Of HI.TL.CheckFiled)
        Get
            Return _CheckFiled
        End Get
    End Property

    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    ReadOnly Property CheckDuplFiled As List(Of HI.TL.DuplFiled)
        Get
            Return _CheckDuplFiled
        End Get
    End Property

    Private _BaseFiled As New List(Of HI.TL.DataBaseFiled)()
    ReadOnly Property BaseFiled As List(Of HI.TL.DataBaseFiled)
        Get
            Return _BaseFiled
        End Get
    End Property

    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()
    ReadOnly Property CheckDelFiled As List(Of HI.TL.CheckDelFiled)
        Get
            Return _CheckDelFiled
        End Get
    End Property

    Private _DefaultsData As New List(Of HI.TL.DefaultsData)()
    ReadOnly Property DefaultsData As List(Of HI.TL.DefaultsData)
        Get
            Return _DefaultsData
        End Get
    End Property

End Class
