Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.IO
Imports System.Drawing
Imports System
Imports System.Data
Imports HI.TL
Public Class wAssetModel
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MASTER

    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _KeyFiled As New List(Of HI.TL.PKFiled)()
    Private _CheckFiled As New List(Of HI.TL.CheckFiled)()
    Private _CheckDuplFiled As New List(Of HI.TL.DuplFiled)()
    Private _CheckDelFiled As New List(Of HI.TL.CheckDelFiled)()

    Private _DataInfo As DataTable
    Private objForm As wAddEditAssetModel
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"

    Public _UPdatedataByte As Byte() = Nothing

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        FormName = Me.Name
        Me.FormName = FormName
        Me.AssPath = HI.ST.SysInfo.PathFileDLL


        _KeyFiled.Clear()
        _CheckFiled.Clear()
        _CheckDuplFiled.Clear()
        _CheckDelFiled.Clear()

        Me.PrepareForm()
        'Me.Preform()

        Me.ogbmainprocbutton.Width = 0


        objForm = New wAddEditAssetModel(FormName, Me.Text, Me.FormObjID, HI.ST.SysInfo.PathFileDLL, "", Me)
        objForm.Tag = "|" & objForm.Name & "|" & objForm.Name

        HI.TL.HandlerControl.AddHandlerObj(objForm)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, objForm.Name.ToString.Trim, objForm)
        Catch ex As Exception
        Finally
        End Try

    End Sub

#Region "Property"
    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
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

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
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
    Private _PDFv As DevExpress.XtraPdfViewer.PdfViewer
    Public Property _PDFs As DevExpress.XtraPdfViewer.PdfViewer
        Get
            Return _PDFv
        End Get
        Set(value As DevExpress.XtraPdfViewer.PdfViewer)
            _PDFv = value
        End Set
    End Property

    Private _FormSortField As String = ""
#End Region

#Region "Proc"
    Private Sub PrepareForm()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0


        _Str = "SELECT FNGrpObjID,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.FormName) & "' "
        _Str &= vbCrLf & " ORDER BY FNGrpObjSeq ASC  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then
            _objId = Integer.Parse(_dt.Rows(0)!FNGrpObjID.ToString)

            Me.FormObjID = _objId
            Me.TableName = _dt.Rows(0)!FTTableName.ToString
            _SortField = _dt.Rows(0)!FTSortField.ToString

            '------ Get Form Object Gen Grid-------------------
            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
            If _dt.Rows.Count > 0 Then
                _StrQuery = "  Select  "
                With Me.ogvAssetModel
                    .Columns.Clear()
                    Dim _FieldName As String = ""


                    For Each Row As DataRow In _dt.Select("FTType='G' AND FTBaseName + '.' + FTPrefix + '.' + FTTableName='" & Me.TableName & "'", "FNSeq")

                        _FieldName = Row!FTFiledName.ToString

                        If Row!FTStaNoneBase.ToString <> "Y" Then

                            If Row!FTFormControlType.ToString.ToUpper = "ButtonEdit".ToUpper And Val(Row!FNButtonEditBrwID.ToString) > 0 Then
                                Dim _SubQuery As String = HI.TL.HSysField.GetSysSubQuery(_FieldName)
                                If _SubQuery <> "" Then
                                    _FieldName = "ISNULL((" & _SubQuery & "),'') AS " & _FieldName
                                End If
                            ElseIf Row!FTFormControlType.ToString.ToUpper = "ComboBoxEdit".ToUpper And Row!FTComboListName.ToString <> "" Then
                                Dim _SubQuery As String = HI.TL.HSysField.GetSysSubQueryCombolist(_FieldName, Row!FTComboListName.ToString)
                                If _SubQuery <> "" Then
                                    _FieldName = "ISNULL((" & _SubQuery & "),'') AS " & _FieldName
                                End If
                            End If

                            If _ColCount = 0 Then
                                If Microsoft.VisualBasic.Left(_FieldName, 2) = "FD" Then
                                    _StrQuery &= vbCrLf & "  Convert(varchar(10),Convert(Datetime," & _FieldName & " ),103)  AS " & _FieldName
                                Else
                                    _StrQuery &= vbCrLf & "" & _FieldName
                                End If
                            Else
                                If Microsoft.VisualBasic.Left(_FieldName, 2) = "FD" Then
                                    _StrQuery &= vbCrLf & " ,  Convert(varchar(10),Convert(Datetime," & _FieldName & " ),103)  AS " & _FieldName
                                Else
                                    _StrQuery &= vbCrLf & "," & _FieldName
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
                                .OptionsColumn.ReadOnly = True
                                .OptionsColumn.AllowEdit = False
                                .Caption = Row!FTFiledName.ToString

                                If Row!FTPK.ToString = "Y" Then
                                    Dim _m As New PKFiled
                                    _m.FiledName = Row!FTFiledName.ToString
                                    _KeyFiled.Add(_m)

                                    If Me.MainKey = "" Then
                                        Me.MainKey = Row!FTFiledName.ToString


                                        _Str = "   SELECT  DISTINCT      D.FTFiledName, D.FTBaseName + '.' + D.FTPrefix + '.' + D.FTTableName AS FTTableName"
                                        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjDynamic_D AS D WITH (NOLOCK),HSysObjDynamic_H AS H WITH(NOLOCK)"
                                        _Str &= vbCrLf & "  WHERE    D.FTFiledName IN (SELECT Distinct  FTColumnNameRef FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTTablePKRef AS P WITH(NOLOCK)  WHERE FTColumnName ='" & Row!FTFiledName.ToString & "') "
                                        _Str &= vbCrLf & "  AND      (ISNULL(D.FTStaNoneBase, '') <> 'Y') "
                                        _Str &= vbCrLf & "  AND      D.FNObjID =H.FNObjID  AND  H.FNGrpObjID <>'" & _objId & "'"

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

                _StrQuery &= vbCrLf & " FROM   " & Me.TableName & " AS M WITH(NOLOCK) "

                _FormSortField = _SortField

                Me.Query = _StrQuery

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

    Public Sub LoadData()
        If Me.Query = "" Then Exit Sub

        Dim _Str As String = ""

        _Str = Me.Query

        '_Str &= " Order By   FNHSysTrainTopicId  "


        _DataInfo = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
    End Sub

    Public Sub Preform()

        Me.LoadData()
        Me.VerifyData()

    End Sub

    Private Sub PrepareInfo()
        Me.VerifyData()
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

    Private Sub VerifyData()
        If _DataInfo Is Nothing Then Exit Sub

        'If _DataInfo.Select(Me.MainKey & " IS NULL ").Length <= 0 Then
        '    _DataInfo.Rows.Add()

        Me.ogcAssetModel.DataSource = _DataInfo
    End Sub
#End Region

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        HI.TL.HandlerControl.ClearControl(objForm)
        With objForm
            '.DefaultsData()
            .MainKeyID = ""

            For I As Integer = 0 To ._KeyFiled.ToArray.Count - 1
                ._KeyFiled(I).FiledValue = ""
            Next
            .ogcModelPartDetail.DataSource = Nothing
            .CheckTab = True
            .otbModel.SelectedTabPageIndex = 0
            .otbModel.SelectedTabPage.TabControl.TabPages(1).PageEnabled = False
            .gbFPImage.Controls.Clear()
            .ProcComplete = False
            .ocmaddnew.Visible = True
            .ocmaddnew.Enabled = Me.ocmaddnew.Enabled
            .ocmedit.Visible = False
            .ocmedit.Enabled = Me.ocmedit.Enabled
            .ocmdelete.Visible = False
            .ocmdelete.Enabled = Me.ocmdelete.Enabled
            .ShowDialog()

            If .ProcComplete Then
                Call LoadData()
                Me.VerifyData()
                .ProcComplete = False
            End If

        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Dim _Str As String = ""
        Try
            With ogvAssetModel
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _Key As String = "" & .GetRowCellValue(.FocusedRowHandle, Me.MainKey).ToString.Trim
                If _Key = "" Then Exit Sub
                If Not (ocmdelete.Enabled) Then Exit Sub

                If Me.CheckNotUsed(_Key) = False Then Exit Sub

                If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, .GetRowCellValue(.FocusedRowHandle, Me.RequireField).ToString.Trim) = True Then
                    Try
                        _Str = " Delete From " & Me.TableName & " " & "  WHERE  " & Me.MainKey & "='" & _Key.ToString & "' "
                        _Str &= vbCrLf & "Delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModelPart WHERE FNHSysAssetModelId=" & _Key & ""
                        If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Str, _DBEnum)) Then
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                            .SelectCell(.FocusedRowHandle, .Columns.ColumnByName(Me.RequireField))
                        Else
                            _DataInfo.BeginInit()
                            For Each R As DataRow In _DataInfo.Select(Me.MainKey & "=" & _Key)
                                R.Delete()
                            Next
                            _DataInfo.EndInit()
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                            '.DeleteRow(.FocusedRowHandle)
                        End If
                    Catch ex As Exception

                    End Try

                End If
            End With
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.Preform()
    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click
        Dim Qry As String = ""
        Dim _KeyValue As String = ""
        Dim dt As DataTable
        Dim dtTabPart As DataTable
        Dim _Pdf As New DevExpress.XtraPdfViewer.PdfViewer
        'Dim dataByte As Byte()

        With ogvAssetModel
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _Key As String = "" & .GetRowCellValue(.FocusedRowHandle, Me.MainKey).ToString.Trim

            If Not (ocmedit.Enabled) Then Exit Sub
            HI.TL.HandlerControl.ClearControl(objForm)
            With objForm
                .CheckTab = False
                .otbModel.SelectedTabPage.TabControl.TabPages(1).PageEnabled = True
                .otbModel.SelectedTabPageIndex = 0
                .gbFPImage.Controls.Clear()
                .ProcComplete = False
                .ocmaddnew.Visible = False
                .ocmaddnew.Enabled = Me.ocmaddnew.Enabled
                .ocmedit.Visible = True
                .ocmedit.Enabled = Me.ocmedit.Enabled
                .ocmdelete.Visible = Me.ocmdelete.Visible
                .ocmdelete.Enabled = Me.ocmdelete.Enabled
                .FNHSysAssetModelId.Properties.Tag = Val(_Key)
                Try
                    Qry = "SELECT top 1 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel WHERE FNHSysAssetModelId=" & _Key & ""
                    dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)

                    Qry = "SELeCT MP.FNSeq,MO.FTAssetModelCode,MO.FTAssetModelNameTH,MO.FTAssetModelNameEN,P.FTAssetPartNameTH,P.FTAssetPartNameEN,P.FTAssetPartCode,MP.FNQuantity"
                    Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModelPart AS MP WITH(NOLOCK)  INNER JOIN"
                    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS MO WITH(NOLOCK) ON MP.FNHSysAssetModelId=MO.FNHSysAssetModelId LEFT OUTER JOIN"
                    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) ON MP.FNHSysAssetPartId=P.FNHSysAssetPartId"
                    Qry &= vbCrLf & "WHERE MP.FNHSysAssetModelId=" & _Key & " order by FNSeq asc"
                    dtTabPart = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
                    'For Each RR As DataRow In dtTabPart.Rows
                    '    .FNHSysAssetPartId.Text = RR!FTAssetPartCode.ToString
                    '    .FNSeq.Value = Val(RR!FNSeq.ToString)
                    '    .FNQuantity.Value = Val(RR!FNQuantity.ToString)
                    '    Exit For
                    'Next
                    .ogcModelPartDetail.DataSource = dtTabPart

                    For Each R As DataRow In dt.Rows
                        If R!FTStateActive.ToString = "1" Then
                            .FTStateActive.Checked = True
                        Else
                            .FTStateActive.Checked = False
                        End If
                        .MainKeyID = Val(R!FNHSysAssetModelId.ToString)
                        .MainKey = Me.RequireField
                        .TableName = Me.TableName
                        .FTAssetModelCode.Text = R!FTAssetModelCode.ToString
                        .FTAssetModelNameEN.Text = R!FTAssetModelNameEN.ToString
                        .FTAssetModelNameTH.Text = R!FTAssetModelNameTH.ToString
                        .FTRemark.Text = R!FTRemark.ToString
                        With _Pdf
                            .Dock = DockStyle.Fill
                            _UPdatedataByte = CType(R!FPImage, Byte())
                            .LoadDocument(New MemoryStream(_UPdatedataByte))
                            _PDFv = _Pdf
                        End With
                        .gbFPImage.Controls.Add(_Pdf)
                    Next
                Catch ex As Exception

                End Try

                .ShowDialog()
                If .ProcComplete Then
                    Call LoadData()
                    Me.VerifyData()
                    .ProcComplete = False
                End If
            End With
        End With
    End Sub

    Private Sub ogvAssetModel_DoubleClick(sender As Object, e As EventArgs) Handles ogvAssetModel.DoubleClick
        If Me.ocmedit.Enabled Then
            ocmedit_Click(Me.ocmedit, New System.EventArgs)
        End If
    End Sub

    Private Sub ogvAssetModel_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvAssetModel.KeyDown
        With ogvAssetModel
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Select Case e.KeyCode
                Case Keys.Delete
                    ocmdelete.PerformClick()
            End Select
        End With
    End Sub

    Private Sub ogvAssetModel_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvAssetModel.RowCellStyle
        Try
            With ogvAssetModel
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

    Private Sub wAssetModel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _DataInfo.Dispose()
        _KeyFiled = Nothing
        _CheckFiled = Nothing
        _CheckDuplFiled = Nothing
    End Sub

    Private Sub wAssetModel_Load(sender As Object, e As EventArgs) Handles Me.Load
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
End Class