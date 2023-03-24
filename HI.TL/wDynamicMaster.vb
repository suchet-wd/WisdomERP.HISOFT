Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.IO
Imports System.Drawing
Imports System
Imports System.Data

Public Class wDynamicMaster

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER

    Private _Bindgrid As Boolean = False
    Private _RowDataChange As Boolean = False
    Private _KeyFiled As New List(Of PKFiled)()
    Private _CheckFiled As New List(Of CheckFiled)()
    Private _CheckDuplFiled As New List(Of DuplFiled)()
    Private _CheckDelFiled As New List(Of CheckDelFiled)()

    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"

    Sub New(ByVal FormName As String, ByVal Title As String, ByVal tImage As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Name = FormName
        Me.FormName = FormName
        Me.Text = Title
        Me.ogbdetail.Text = Title

        _KeyFiled.Clear()
        _CheckFiled.Clear()
        _CheckDuplFiled.Clear()
        _CheckDelFiled.Clear()

        Me.InitFormControl()


        If tImage <> "" Then
            Dim _PathImgDisable As String = _SysImgPath & "\Menu\" & tImage
            If IO.File.Exists(_PathImgDisable) Then
                'Me.Icon = Icon.FromHandle(DirectCast(Image.FromFile(_PathImgDisable), Bitmap).GetHicon()) 'Icon.FromHandle(hIcon)
                Me.Icon = Icon.FromHandle(DirectCast(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(_PathImgDisable))), Bitmap).GetHicon())
            End If
        End If
        Me.ogbmainprocbutton.Width = 0

    End Sub

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
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

    Private _TableNameORG As String = ""
    Public Property TableNameORG As String
        Get
            Return _TableNameORG
        End Get
        Set(ByVal value As String)
            _TableNameORG = value
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

    Private Sub InitFormControl()

        Dim _Qry As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _QryQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0


        _Qry = "SELECT FNGrpObjID,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FTTableName AS FTTableNameORG  "
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.FormName) & "' "
        _Qry &= vbCrLf & " ORDER BY FNGrpObjSeq ASC  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNGrpObjID.ToString)
            Me.TableName = _dt.Rows(0)!FTTableName.ToString
            Me.TableNameORG = _dt.Rows(0)!FTTableNameORG.ToString
            _SortField = _dt.Rows(0)!FTSortField.ToString


            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            If _dt.Rows.Count > 0 Then
                _QryQuery = "  Select  "
                With Me.ogvdetail
                    .Columns.Clear()
                    Dim _FieldName As String = ""

                    For Each Row As DataRow In _dt.Select("FTType='G' AND FTFormControlType<>'PictureEdit' AND FTBaseName + '.' + FTPrefix + '.' + FTTableName='" & Me.TableName & "'", "FNSeq")

                        _FieldName = Row!FTFiledName.ToString

                        If Row!FTPK.ToString = "Y" Or Row!FTShowState.ToString = "Y" Then

                            If Row!FTFormControlType.ToString.ToUpper = "ButtonEdit".ToUpper And Val(Row!FNButtonEditBrwID.ToString) > 0 Then
                                Dim _GetSysSubQuery As String = HSysField.GetSysSubQuery(_FieldName)
                                If _GetSysSubQuery <> "" Then
                                    _FieldName = "ISNULL((" & _GetSysSubQuery & "),'') AS " & _FieldName
                                End If
                            End If

                            If _ColCount = 0 Then

                                If Microsoft.VisualBasic.Left(_FieldName, 2) = "FD" Then
                                    _QryQuery &= vbCrLf & "  Convert(varchar(10),Convert(Datetime," & _FieldName & " ),103)  AS " & _FieldName
                                Else
                                    _QryQuery &= vbCrLf & "" & _FieldName
                                End If

                            Else

                                If Microsoft.VisualBasic.Left(_FieldName, 2) = "FD" Then
                                    _QryQuery &= vbCrLf & " ,  Convert(varchar(10),Convert(Datetime," & _FieldName & " ),103)  AS " & _FieldName
                                Else
                                    _QryQuery &= vbCrLf & "," & _FieldName
                                End If

                            End If

                            Dim GridCol As New DevExpress.XtraGrid.Columns.GridColumn
                            Dim Ctrl As New Object
                            With GridCol
                                .Name = Row!FTFiledName.ToString
                                .FieldName = Row!FTFiledName.ToString
                                .Width = Val(Row!FNColWidth.ToString)

                                .AppearanceCell.Options.UseTextOptions = True
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = VertAlignment.Center
                                .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                .OptionsColumn.ShowInCustomizationForm = False
                                .OptionsColumn.AllowSize = True
                                .VisibleIndex = _ColCount

                                .Visible = (Row!FTShowState.ToString = "Y")
                                .OptionsColumn.ReadOnly = (Row!FTState.ToString = "R")
                                .Caption = Row!FTFiledName.ToString

                                If Row!FTPK.ToString = "Y" Then
                                    Dim _m As New PKFiled
                                    _m.FiledName = Row!FTFiledName.ToString
                                    _KeyFiled.Add(_m)

                                    If Me.MainKey = "" Then
                                        Me.MainKey = Row!FTFiledName.ToString


                                        _Qry = "   SELECT  DISTINCT      D.FTFiledName, D.FTBaseName + '.' + D.FTPrefix + '.' + D.FTTableName AS FTTableName"
                                        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjDynamic_D AS D WITH (NOLOCK),HSysObjDynamic_H AS H WITH(NOLOCK)"
                                        _Qry &= vbCrLf & "  WHERE    D.FTFiledName IN (SELECT Distinct  FTColumnNameRef FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePKRef AS P WITH(NOLOCK)  WHERE FTColumnName ='" & Row!FTFiledName.ToString & "') "
                                        _Qry &= vbCrLf & "  AND      (ISNULL(D.FTStaNoneBase, '') <> 'Y') "
                                        _Qry &= vbCrLf & "  AND      D.FNObjID =H.FNObjID  AND  H.FNGrpObjID <>'" & _objId & "'"

                                        Dim _dtchk As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

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

                                Select Case Row!FTControlType.ToString.ToUpper
                                    Case "TextEdit".ToUpper
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                        With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)
                                            .Name = Row!FTFiledName.ToString
                                            .MaxLength = Val(Row!FNMaxLenght.ToString)

                                            If Row!FTStaTextUpper.ToString = "Y" Then
                                                .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                            End If
                                        End With
                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                    Case "CalcEdit".ToUpper

                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
                                        With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit)
                                            .Name = Row!FTFiledName.ToString

                                            .Precision = Val(Row!FNNumericScale.ToString)
                                            .DisplayFormat.FormatType = FormatType.Numeric
                                            .DisplayFormat.FormatString = "N" & Val(Row!FNNumericScale.ToString).ToString

                                        End With

                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    Case "MemoEdit".ToUpper
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
                                        With Ctrl
                                            .Name = Row!FTFiledName.ToString
                                            .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)
                                        End With

                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                    Case "DateEdit".ToUpper
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
                                        With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)
                                            .Name = Row!FTFiledName.ToString
                                            .AllowNullInput = DefaultBoolean.False
                                            .DisplayFormat.FormatString = "d"
                                            .DisplayFormat.FormatType = FormatType.Custom
                                            .EditFormat.FormatString = "d"
                                            .EditFormat.FormatType = FormatType.Custom
                                            .Mask.EditMask = "d"

                                            .ShowClear = True

                                            AddHandler .Leave, AddressOf RepositoryItemDate_Leave
                                            AddHandler .Click, AddressOf RepositoryItemDate_GotFocus

                                        End With
                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    Case "CheckEdit".ToUpper
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

                                        With Ctrl
                                            .Name = Row!FTFiledName.ToString
                                            .ValueChecked = "1"
                                            .ValueUnchecked = "0"
                                        End With

                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    Case "ButtonEdit".ToUpper
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
                                        With CType(Ctrl, DevExpress.XtraEditors.ButtonEdit)
                                            .Name = Row!FTFiledName.ToString
                                            .Properties.Buttons.Item(0).Tag = Row!FNButtonEditBrwID.ToString
                                            .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)


                                            If Row!FTStaTextUpper.ToString = "Y" Then
                                                .Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                            End If

                                        End With
                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                    Case "ComboBoxEdit".ToUpper
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
                                        With Ctrl
                                            .Name = Row!FTFiledName.ToString
                                        End With
                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                    Case Else
                                        Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                        With Ctrl
                                            .Name = Row!FTFiledName.ToString
                                            .Properties.MaxLength = Val(Row!FNMaxLenght.ToString)

                                            If Row!FTStaTextUpper.ToString = "Y" Then
                                                .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                            End If

                                        End With

                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

                                End Select

                                .ColumnEdit = Ctrl
                            End With

                            .Columns.Add(GridCol)
                            _ColCount = _ColCount + 1

                        End If

                    Next

                    .OptionsCustomization.AllowSort = True
                    .OptionsCustomization.AllowColumnMoving = True
                    .OptionsView.ShowAutoFilterRow = True
                    .OptionsView.ShowFooter = False
                    .OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
                    .OptionsView.ShowDetailButtons = False
                    .ShowButtonMode = ShowButtonModeEnum.ShowOnlyInEditor

                    .OptionsView.ShowGroupPanel = False
                    .OptionsView.ColumnAutoWidth = False
                    .OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never

                    .VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
                    .HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto

                    .OptionsNavigation.EnterMoveNextColumn = True
                    .OptionsView.ShowDetailButtons = False
                    .OptionsView.ShowDetailButtons = False

                End With

                _QryQuery &= vbCrLf & " FROM   " & Me.TableName & " AS M WITH(NOLOCK) "
                _QryQuery &= vbCrLf & " " & IIf(_SortField = "", "", " ORDER BY  " & _SortField)

                Me.Query = _QryQuery

            End If
        End If

    End Sub

    Public Shared Sub RepositoryItemDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String
                Try
                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)
                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub RepositoryItemDate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
                If _TDate = "" Then
                    Beep()
                End If
                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception
                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
                End Try

                If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
                    Beep()
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadData()
        If Me.Query = "" Then Exit Sub
        _DataInfo = HI.Conn.SQLConn.GetDataTable(Me.Query, _DBEnum,, False)
    End Sub

#Region "MAIN PROC"

    Private Sub Proc_Delete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        With ogvdetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _Key As String = "" & .GetRowCellValue(.FocusedRowHandle, Me.MainKey).ToString.Trim
            If _Key = "" Then Exit Sub
            If Not (ocmdelete.Enabled) Then Exit Sub
            If Me.CheckNotUsed(_Key) = False Then Exit Sub

            If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, .GetRowCellValue(.FocusedRowHandle, Me.RequireField).ToString.Trim) = True Then

                Dim _Qry As String = " Delete From " & Me.TableName & " " & "  WHERE  " & Me.MainKey & "='" & _Key.ToString & "' "

                If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, _DBEnum)) Then
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    .SelectCell(.FocusedRowHandle, .Columns.ColumnByName(Me.RequireField))
                Else
                    _DataInfo.BeginInit()
                    For Each R As DataRow In _DataInfo.Select(Me.MainKey & "=" & _Key)
                        R.Delete()
                    Next
                    _DataInfo.EndInit()

                    '.DeleteRow(.FocusedRowHandle)
                End If
            End If
        End With
    End Sub

    Private Sub Proc_Clear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmrefresh.Click
        Me.Preform()
    End Sub

    Private Sub Proc_Preview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click

    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "Proc"

    Private Sub Preform()

        Me.LoadData()
        Me.VerifyData()
    End Sub

    Private Sub PrepareInfo()
        Me.VerifyData()
    End Sub

    Private Sub VerifyData()
        If _DataInfo Is Nothing Then Exit Sub

        If _DataInfo.Select(Me.MainKey & " IS NULL ").Length <= 0 Then
            _DataInfo.Rows.Add()
        End If
        Me.ogcdetail.DataSource = _DataInfo
    End Sub

#End Region

    Private Sub wDynamicMaster_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        _DataInfo.Dispose()
        _KeyFiled = Nothing
        _CheckFiled = Nothing
        _CheckDuplFiled = Nothing


    End Sub

    Private Sub Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Me.TableName <> "" Then
                _Bindgrid = False
                Me.Preform()
            Else
                HI.MG.ShowMsg.mProcessError(1301110002, "", Me.Text)
                Me.Close()
            End If
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message, Me.Text, System.Windows.Forms.MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ogvsub_BeforeLeaveRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvdetail.BeforeLeaveRow
        With ogvdetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            If Not (_RowDataChange) Then Exit Sub
            If Not (ocmsavedetail.Enabled) Then Exit Sub

            Dim _Val As String = ""
            Dim _Pass As Boolean = True
            For I As Integer = 0 To _CheckFiled.ToArray.Count - 1
                _Val = "" & .GetRowCellValue(.FocusedRowHandle, _CheckFiled(I).FiledName).ToString.Trim

                Select Case (Microsoft.VisualBasic.Left(_CheckFiled(I).FiledName.ToString, 2)).ToUpper
                    Case "FN".ToUpper, "FC".ToUpper
                        If IsNumeric(_Val) = False Then _Val = "0"

                        If CDbl(_Val) <= 0 Then
                            _Pass = False
                            e.Allow = False
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, .Columns.ColumnByName(_CheckFiled(I).FiledName).Caption.ToString)
                            .SelectCell(.FocusedRowHandle, .Columns.ColumnByName(_CheckFiled(I).FiledName))
                            Exit Sub
                        End If
                    Case Else
                        If _Val = "" Then
                            _Pass = False
                            e.Allow = False
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, .Columns.ColumnByName(_CheckFiled(I).FiledName).Caption.ToString)
                            .SelectCell(.FocusedRowHandle, .Columns.ColumnByName(_CheckFiled(I).FiledName))
                            Exit Sub
                        End If
                End Select

                If "" & .GetRowCellValue(.FocusedRowHandle, _CheckFiled(I).FiledName).ToString.Trim = "" Then
                End If

            Next

            If (_Pass) Then
                Dim _Key As String = "" & .GetRowCellValue(.FocusedRowHandle, Me.MainKey).ToString.Trim

                If _Key = "" Then _Key = HI.TL.RunID.GetRunNoID(Me.TableName, Me.MainKey, _DBEnum).ToString
                If _Key <> "" Then


                    Dim _QryCheckDupl As String = ""
                    For I As Integer = 0 To _CheckDuplFiled.ToArray.Count - 1

                        If _QryCheckDupl <> "" Then _QryCheckDupl &= " AND "

                        _Val = "" & .GetRowCellValue(.FocusedRowHandle, _CheckDuplFiled(I).FiledName).ToString.Trim


                        Select Case (Microsoft.VisualBasic.Left(_CheckDuplFiled(I).FiledName.ToString, 2)).ToUpper
                            Case "FN".ToUpper, "FC".ToUpper
                                _QryCheckDupl &= _CheckDuplFiled(I).FiledName & "=" & HI.UL.ULF.ChkNumeric(_Val) & " "
                            Case Else
                                _QryCheckDupl &= _CheckDuplFiled(I).FiledName & "='" & HI.UL.ULF.rpQuoted(_Val) & "' "
                        End Select

                    Next

                    Dim _Qry As String = ""

                    If _QryCheckDupl <> "" Then
                        _Qry = " Select Top 1 " & Me.MainKey & ""
                        _Qry &= vbCrLf & "  From " & Me.TableName & " WITH (NOLOCK) " & " "
                        _Qry &= vbCrLf & "  WHERE  " & Me.MainKey & "<>'" & _Key.ToString & "' "
                        _Qry &= vbCrLf & "  AND   " & _QryCheckDupl

                        If HI.Conn.SQLConn.GetField(_Qry, _DBEnum, "") <> "" Then
                            e.Allow = False
                            HI.MG.ShowMsg.mProcessError(1301110001, "", Me.Text, MessageBoxIcon.Error, _Val)
                            Exit Sub
                        End If
                    End If

                    Dim _Fields As String = ""
                    Dim _Values As String = ""

                    _Qry = " Select Top 1 " & Me.MainKey & " From " & Me.TableName & " WITH (NOLOCK) " & "  WHERE  " & Me.MainKey & "='" & _Key.ToString & "' "
                    If HI.Conn.SQLConn.GetField(_Qry, _DBEnum, "") = "" Then

                        For Each _Col As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                            If UCase(_Col.Name.ToString) = Me.MainKey.ToUpper Then
                                .SetRowCellValue(.FocusedRowHandle, Me.MainKey, _Key)
                            End If

                            _Val = "" & .GetRowCellValue(.FocusedRowHandle, _Col.Name.ToString).ToString.Trim

                            If _Values <> "" Then _Values &= ","
                            If _Fields <> "" Then _Fields &= ","

                            _Fields &= _Col.Name.ToString

                            Select Case UCase(_Col.Name.ToString)
                                Case UCase("FDInsDate"), UCase("FTInsDate")
                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
                                    _Values &= "''"
                                Case UCase("FTInsTime")
                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FTInsUser")
                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_Col.Name.ToString, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        Next

                        _Qry = " INSERT INTO   " & Me.TableName & "(" & _Fields & ") VALUES (" & _Values & ")"

                    Else

                        For Each _Col As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                            If UCase(_Col.Name.ToString) = Me.MainKey.ToUpper Then
                                .SetRowCellValue(.FocusedRowHandle, Me.MainKey, _Key)
                            End If

                            _Val = "" & .GetRowCellValue(.FocusedRowHandle, _Col.Name.ToString).ToString.Trim

                            If _Values <> "" Then _Values &= ","

                            Select Case UCase(_Col.Name.ToString)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate")
                                    _Values &= _Col.Name.ToString & "=" & HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FTUpdTime")
                                    _Values &= _Col.Name.ToString & "=" & HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case UCase("FTUpdUser")
                                    _Values &= _Col.Name.ToString & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_Col.Name.ToString, 2))
                                        Case "FC", "FN"
                                            _Values &= _Col.Name.ToString & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _Col.Name.ToString & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _Col.Name.ToString & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select

                        Next
                        _Qry = " Update  " & Me.TableName & " Set " & _Values & " WHERE  " & Me.MainKey & "='" & _Key.ToString & "' "
                    End If

                    If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, _DBEnum)) Then
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        .SelectCell(.FocusedRowHandle, .Columns.ColumnByName(Me.RequireField))
                        e.Allow = False

                    Else
                        LoadData()
                        Me.VerifyData()
                        .SelectCell(.RowCount - 1, .Columns.ColumnByName(Me.RequireField))
                    End If
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        End With

    End Sub

    Private Sub ogvsub_CellValueChanging(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvdetail.CellValueChanging
        _RowDataChange = True
    End Sub

    Private Sub ogvsub_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvdetail.FocusedRowChanged
        _RowDataChange = False
    End Sub

    Private Sub ogvsub_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        With ogvdetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _Pass As Boolean = True
            For I As Integer = 0 To _CheckFiled.ToArray.Count - 1
                If "" & .GetRowCellValue(.FocusedRowHandle, _CheckFiled(I).FiledName).ToString.Trim = "" Then
                    _Pass = False

                    Exit Sub
                End If
            Next

            Select Case e.KeyCode
                Case Keys.Down

                    If .FocusedRowHandle = .RowCount - 1 And (_Pass) And (ocmsavedetail.Enabled) Then

                        If Not (_RowDataChange) Then Exit Sub
                        _DataInfo.Rows.Add()

                        .SelectCell(.FocusedRowHandle + 1, .Columns.ColumnByName(Me.RequireField))
                    ElseIf .FocusedRowHandle < .RowCount - 1 Then
                        .SelectCell(.FocusedRowHandle + 1, .Columns.ColumnByName(Me.RequireField))
                    End If

                Case Keys.Enter
                    If .FocusedColumn.AbsoluteIndex = .Columns.Count - 1 Then
                        If .FocusedRowHandle = .RowCount - 1 And (_Pass) And (ocmsavedetail.Enabled) Then

                            If Not (_RowDataChange) Then Exit Sub
                            _DataInfo.Rows.Add()

                            .SelectCell(.FocusedRowHandle + 1, .Columns.ColumnByName(Me.RequireField))
                        ElseIf .FocusedRowHandle < .RowCount - 1 Then
                            .SelectCell(.FocusedRowHandle + 1, .Columns.ColumnByName(Me.RequireField))
                        End If
                    End If

                Case Keys.Delete
                    Dim _Key As String = "" & .GetRowCellValue(.FocusedRowHandle, Me.MainKey).ToString.Trim
                    If _Key = "" Then Exit Sub
                    If Not (ocmdelete.Enabled) Then Exit Sub
                    If Me.CheckNotUsed(_Key) = False Then Exit Sub

                    If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, .GetRowCellValue(.FocusedRowHandle, Me.RequireField).ToString.Trim) = True Then

                        Dim _Qry As String = " Delete From " & Me.TableName & "  " & "  WHERE  " & Me.MainKey & "='" & _Key.ToString & "' "

                        If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, _DBEnum)) Then
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                            .SelectCell(.FocusedRowHandle, .Columns.ColumnByName(Me.RequireField))
                        Else
                            _DataInfo.BeginInit()
                            For Each R As DataRow In _DataInfo.Select(Me.MainKey & "=" & _Key)
                                R.Delete()
                            Next
                            _DataInfo.EndInit()
                        End If

                    End If
            End Select
        End With
    End Sub

    Private Shared Sub Gridview_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                If e.RowHandle <> .FocusedRowHandle OrElse e.Column.AbsoluteIndex = .FocusedColumn.AbsoluteIndex Then
                    If (e.Column.OptionsColumn.ReadOnly) Then
                        e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                    Else
                        e.Appearance.BackColor = System.Drawing.Color.White
                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Class PKFiled
        Private _FiledName As String
        Public Property FiledName() As String
            Get
                Return _FiledName
            End Get
            Set(ByVal value As String)
                _FiledName = value
            End Set
        End Property
    End Class

    Private Class CheckFiled
        Private _FiledName As String
        Public Property FiledName() As String
            Get
                Return _FiledName
            End Get
            Set(ByVal value As String)
                _FiledName = value
            End Set
        End Property

    End Class

    Private Class DuplFiled
        Private _FiledName As String
        Public Property FiledName() As String
            Get
                Return _FiledName
            End Get
            Set(ByVal value As String)
                _FiledName = value
            End Set
        End Property

    End Class

    Private Class CheckDelFiled
        Private _Query As String
        Public Property Query() As String
            Get
                Return _Query
            End Get
            Set(ByVal value As String)
                _Query = value
            End Set
        End Property
    End Class

    Private Sub ogvdetail_RowCountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvdetail.RowCountChanged
        With Me.ogvdetail
            If .RowCount = 0 Then
                If Me.ocmsavedetail.Enabled Then
                    If _DataInfo.Select(Me.MainKey & " IS NULL ").Length <= 0 Then
                        _DataInfo.Rows.Add()
                    End If
                End If
            End If
        End With
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


End Class