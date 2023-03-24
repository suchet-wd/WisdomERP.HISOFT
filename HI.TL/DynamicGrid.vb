Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports System.Drawing
Imports DevExpress.XtraEditors

Public Class DynamicGrid

    Sub New(GrpObjID As Integer, ObjID As Integer, Data As DataTable, oGridView As DevExpress.XtraGrid.Views.Grid.GridView, Optional Filter As Boolean = False)
        Dim _FieldName As String = ""
        Dim _ColCount As Integer = 0
        Dim _Str As String = ""
        Dim _dt As DataTable
        Dim _SortField As String = ""
        Dim _StateShowSum As Boolean = False

        _Str = "SELECT FNGrpObjID,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField  "
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FNFormObjID=" & ObjID & " "
        _Str &= vbCrLf & " ORDER BY FNGrpObjSeq ASC  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then
            Me.TableName = _dt.Rows(0)!FTTableName.ToString
            _SortField = _dt.Rows(0)!FTSortField.ToString

            Me.GridView = oGridView

            With oGridView
                _StrQuery = "  Select  "
                For Each Row As DataRow In Data.Select("FTType='G' AND  FNObjID=" & ObjID & " ", "FNSeq,FNCtrlLevel,FNCtrlLevelSeq")
                    _FieldName = Row!FTFiledName.ToString

                    If Row!FTStaNoneBase.ToString <> "Y" Or Row!FTStaNoneBase.ToString = "Y" Then

                        If _ColCount = 0 Then
                            _StrQuery &= vbCrLf & "" & _FieldName
                        Else
                            _StrQuery &= vbCrLf & "," & _FieldName
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

                                    _Str = "   SELECT  DISTINCT      D.FTFiledName, D.FTBaseName + '.' + D.FTPrefix + '.' + D.FTTableName AS FTTableName"
                                    _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjDynamic_D AS D WITH (NOLOCK),[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysObjDynamic_H AS H WITH(NOLOCK)"
                                    _Str &= vbCrLf & "  WHERE    D.FTFiledName IN (SELECT Distinct  FTColumnNameRef FROM HSysTTablePKRef AS P WITH(NOLOCK)  WHERE FTColumnName ='" & Row!FTFiledName.ToString & "') "
                                    _Str &= vbCrLf & "  AND      (ISNULL(D.FTStaNoneBase, '') <> 'Y') "
                                    _Str &= vbCrLf & "  AND      D.FNObjID =H.FNObjID  AND  H.FNGrpObjID <>'" & ObjID & "'"

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

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

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

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                        .Precision = Val(Row!FNNumericScale.ToString)
                                        .DisplayFormat.FormatType = FormatType.Numeric
                                        .DisplayFormat.FormatString = "N" & Val(Row!FNNumericScale.ToString).ToString
                                        AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave

                                    End With

                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                Case "MemoEdit".ToUpper
                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit

                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit)

                                        .Name = Row!FTFiledName.ToString

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue
                                        .MaxLength = Val(Row!FNMaxLenght.ToString)
                                    End With
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                Case "DateEdit".ToUpper
                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)
                                        .Name = Row!FTFiledName.ToString

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                        .AllowNullInput = DefaultBoolean.False
                                        .DisplayFormat.FormatString = "d"
                                        .DisplayFormat.FormatType = FormatType.Custom
                                        .EditFormat.FormatString = "d"
                                        .EditFormat.FormatType = FormatType.Custom
                                        .Mask.EditMask = "d"

                                        .ShowClear = True

                                        AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
                                        AddHandler .Click, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus

                                    End With
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                Case "CheckEdit".ToUpper

                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)


                                        .Name = Row!FTFiledName.ToString

                                        .ValueChecked = "1"
                                        .ValueUnchecked = "0"

                                    End With

                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                Case "ButtonEdit".ToUpper

                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                        .Name = Row!FTFiledName.ToString
                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                        .Buttons.Item(0).Tag = Row!FNButtonEditBrwID.ToString
                                        .MaxLength = Val(Row!FNMaxLenght.ToString)

                                        If Row!FTStaTextUpper.ToString = "Y" Then
                                            .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                        End If

                                        If .Buttons.Item(0).Tag <> "" Then
                                            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
                                            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
                                        End If

                                    End With
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                                Case "ComboBoxEdit".ToUpper

                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox
                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                                        .Name = Row!FTFiledName.ToString

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue
                                        .Items.Clear()

                                        If Row!FTComboListName.ToString <> "" Then
                                            .Items.AddRange(HI.TL.CboList.SetList(Row!FTComboListName.ToString))
                                        End If


                                    End With

                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

                                Case Else

                                    Ctrl = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                                    With CType(Ctrl, DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)
                                        .Name = Row!FTFiledName.ToString


                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue
                                        .MaxLength = Val(Row!FNMaxLenght.ToString)

                                        If Row!FTStaTextUpper.ToString = "Y" Then
                                            .CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
                                        End If

                                    End With

                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near

                            End Select

                            .ColumnEdit = Ctrl

                            If Row!FTStateGridSumType.ToString <> "" Then
                                _StateShowSum = True

                                Select Case Row!FTStateGridSumType.ToString
                                    Case "0"
                                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count
                                    Case "1"
                                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                    Case "2"
                                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Average
                                    Case "3"
                                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max
                                    Case "4"
                                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min
                                    Case Else
                                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                End Select

                                .SummaryItem.DisplayFormat = "{0:n" & Val(Row!FTStateGridSumFmtDigit.ToString) & "}"

                            End If
                        End With

                        .Columns.Add(GridCol)
                        _ColCount = _ColCount + 1

                    End If
                Next

                .OptionsCustomization.AllowSort = True
                .OptionsCustomization.AllowColumnMoving = True

                .OptionsView.ShowAutoFilterRow = Filter
                .OptionsView.ShowFooter = _StateShowSum 'Show Footer Page กรณี Summary

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

                AddHandler .RowCountChanged, AddressOf Gridview_RowCountChanged
                AddHandler .RowCellStyle, AddressOf Gridview_RowCellStyle

            End With

            _StrQuery &= vbCrLf & " FROM   " & Me.TableName & " AS M WITH(NOLOCK) "
            _StrQuery &= vbCrLf & " " & IIf(_SortField = "", "", " ORDER BY  " & _SortField)

            If _ColCount <= 0 Then
                _StrQuery = ""
            End If
        End If
    End Sub

    Private _GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Public Property GridView As DevExpress.XtraGrid.Views.Grid.GridView
        Get
            Return _GridView
        End Get
        Set(ByVal value As DevExpress.XtraGrid.Views.Grid.GridView)
            _GridView = value
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


    Public Sub Dispose()
        _KeyFiled = Nothing
        _CheckFiled = Nothing
        _CheckDuplFiled = Nothing
        _BaseFiled = Nothing
        _CheckDelFiled = Nothing
        _DefaultsData = Nothing
    End Sub

    Private Sub Gridview_RowCountChanged(sender As Object, e As System.EventArgs)
        With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If .RowCount = 0 Then

            End If
        End With
    End Sub

    Private Shared Sub Gridview_RowCellStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs)
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

End Class
