Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Public Class HButtonDropDown

    Sub New(BrwdataID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitForm(BrwdataID)
    End Sub

#Region "Init"
    Private Sub InitForm(BrwdataID As Integer)
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _QryQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _BrwObjID As Integer = 0

        _Qry = "   SELECT  TOP 1  *  "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE BrwID='" & BrwdataID & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        With Me
            If _dt.Rows.Count > 0 Then
                _BrwObjID = Val(_dt.Rows(0)!BrwObjID.ToString)

                If Val(_dt.Rows(0)!FNFormBrwWidth.ToString) > 50 Then
                    .Width = Integer.Parse(Val(_dt.Rows(0)!FNFormBrwWidth.ToString))
                Else
                    .Width = 300
                End If
            Else
                .Width = 300
                .Height = 200
            End If
        End With

        '------ Get Form Object ID-------------------
        _Qry = "SELECT * "
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseObj WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE BrwObjID='" & _BrwObjID & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        With ogvbrowse
            .ClearColumnsFilter()
            .ActiveFilter.Clear()
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False

            For Each R As DataRow In _dt.Rows
                Dim GridCol As New DevExpress.XtraGrid.Columns.GridColumn
                With GridCol

                    .Name = R!FTBrwField.ToString
                    .FieldName = R!FTBrwField.ToString
                    .Width = Integer.Parse(Val(R!FNColWidth.ToString))

                    .AppearanceCell.Options.UseTextOptions = True
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = VertAlignment.Center
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                    .OptionsColumn.ShowInCustomizationForm = False
                    .OptionsColumn.ShowInCustomizationForm = False
                    .OptionsColumn.ShowInExpressionEditor = False
                    .OptionsColumn.AllowSort = DefaultBoolean.True

                    .OptionsColumn.AllowSize = True
                    .VisibleIndex = _ColCount
                    .OptionsColumn.AllowEdit = False

                    .Visible = (R!FTStateHide.ToString <> "Y")

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        .Caption = R!FTCaptionTH.ToString
                    Else
                        .Caption = R!FTCaptionEN.ToString
                    End If

                    Select Case R!FTFieldType.ToString
                        Case "T"
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                        Case "D"
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        Case "N"
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        Case Else
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    End Select

                End With

                .Columns.Add(GridCol)

                _ColCount = _ColCount + 1

            Next

            .OptionsCustomization.AllowSort = True
            .OptionsCustomization.AllowColumnMoving = False
            .OptionsView.ShowAutoFilterRow = False
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
    End Sub
#End Region
#Region "Property"

    Private _BrwID As Integer = 0
    Public Property BrwID As Integer
        Get
            Return _BrwID
        End Get
        Set(value As Integer)
            _BrwID = value
        End Set
    End Property

    Private _OldBrwID As Integer = 0
    Public Property OldBrwID As Integer
        Get
            Return _OldBrwID
        End Get
        Set(value As Integer)
            _OldBrwID = value
        End Set
    End Property

    Private _FormParent As Object = Nothing
    Public Property FormParent As Object
        Get
            Return _FormParent
        End Get
        Set(value As Object)
            _FormParent = value
        End Set
    End Property

    Private _ParentControl As Object = Nothing
    Public Property ParentControl As Object
        Get
            Return _ParentControl
        End Get
        Set(value As Object)
            _ParentControl = value
        End Set
    End Property

    Private _FieldFilter As String = ""
    Public Property FieldFilter As String
        Get
            Return _FieldFilter
        End Get
        Set(value As String)
            _FieldFilter = value
        End Set
    End Property

    Public ReadOnly Property CheckGotFofus() As Boolean
        Get
            Return ogcbrowse.Focused
        End Get
    End Property

    Public Sub FocusRowUp()
        With ogvbrowse
            If .FocusedRowHandle <= 0 Then Exit Sub
            .FocusedRowHandle = .FocusedRowHandle - 1
        End With
    End Sub

    Public Sub FocusRowDown()
        With ogvbrowse
            If .FocusedRowHandle >= .RowCount - 1 Then Exit Sub
            .FocusedRowHandle = .FocusedRowHandle + 1
        End With
    End Sub

    Public Function FocusRowData() As String
        With ogvbrowse
            Try
                Return "" & .GetRowCellValue(.FocusedRowHandle, FieldFilter).ToString
            Catch ex As Exception
                Return ""
            End Try
        End With
    End Function

    Public Sub SetFocusRowData(TextData As String)
        With ogvbrowse
            Try
                If TextData = "" Then
                    .FocusedRowHandle = 0
                Else
                    .FocusedRowHandle = .LocateByValue(FieldFilter, TextData)
                End If

            Catch ex As Exception
                Try
                    .FocusedRowHandle = 0
                Catch ex2 As Exception
                End Try
            End Try
        End With
    End Sub
#End Region


#Region "Procedure"

    Private _Data As DataTable
    Public Property Data() As DataTable
        Get
            Return _Data
        End Get
        Set(value As DataTable)
            _Data = value.Copy
        End Set
    End Property

    Private _DataRetField As DataTable
    Public Property DataRetField() As DataTable
        Get
            Return _DataRetField
        End Get
        Set(value As DataTable)
            _DataRetField = value.Copy
        End Set
    End Property

    Public Sub LoadBrowsd(BrwID As Object)


        Dim _form As Object = Me.FormParent
        Dim _Qrysql As String
        Dim _dtbrw As New DataTable
        Dim _dtret As New DataTable

        Dim _BrowseCmd As String = ""
        Dim _BrowseSortCmd As String = ""
        Dim _BrowseWhereCmd As String = ""
        Dim _FTBrwCmdField As String = ""
        Dim _FTBrwCmdFieldOptional As String = ""
        Dim FTBrwCmdGroupBy As String = ""

        With ogvbrowse
            .ClearColumnsFilter()
            .ActiveFilter.Clear()
        End With

        _Qrysql = " SELECT  TOP 1    BrwID, "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
        Else
            _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
        End If

        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy "
        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
        _Qrysql &= vbCrLf & " WHERE BrwID=" & BrwID & " "

        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
        _Qrysql = ""

        For Each Row As DataRow In _dtbrw.Rows

            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,ISNULL(FTStatePropertyTag,'') AS FTStatePropertyTag "
            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

            _dtret = New DataTable
            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)


            _BrowseCmd = Row!FTBrwCmd.ToString
            _BrowseSortCmd = Row!FTBrwCmdSort.ToString
            _BrowseWhereCmd = Row!FTBrwCmdWhere.ToString

            _FTBrwCmdField = Row!FTBrwCmdField.ToString
            _FTBrwCmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                FTBrwCmdGroupBy = Row!FTBrwCmdTHGroupBy.ToString
            Else
                FTBrwCmdGroupBy = Row!FTBrwCmdENGroupBy.ToString
            End If

            _Qrysql = Row!FTBrwCmd.ToString
        Next

        If _Qrysql = "" Then Exit Sub


        Dim _Where As String = ""

        Dim I As Integer = 0
        '------------Browse Where Require Field---------------
        If _FTBrwCmdField <> "" Then
            For Each _QryCon As String In _FTBrwCmdField.Split(",")

                Dim _DataCon As String = ""
                For Each ctrl As Object In _form.Controls.Find(_QryCon.Trim(), True)
                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                        Case ENM.Control.ControlType.TextEdit
                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                _DataCon = .Text
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(ctrl, DevExpress.XtraEditors.CalcEdit)
                                _DataCon = .Value
                            End With
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                If Microsoft.VisualBasic.Left(.Name.ToString, 6).ToUpper = "FNHSys".ToUpper And .Text <> "" Then
                                    _DataCon = "" & .Properties.Tag.ToString
                                Else
                                    _DataCon = .Text
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit
                            With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                _DataCon = .Text
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                Select Case .Properties.DisplayFormat.ToString
                                    Case "dd/MM/yyyy"
                                        _DataCon = HI.UL.ULDate.ConvertEnDB(.Text)
                                    Case "dd/MM"
                                        _DataCon = Microsoft.VisualBasic.Right(.Text, 2) & "/" & Microsoft.VisualBasic.Left(.Text, 2)
                                    Case "MM/yyyy"
                                        _DataCon = Microsoft.VisualBasic.Right(.Text, 4) & "/" & Microsoft.VisualBasic.Left(.Text, 2)
                                    Case Else
                                        _DataCon = .Text
                                End Select
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(ctrl, DevExpress.XtraEditors.CheckEdit)
                                _DataCon = IIf(.Checked, "1", "0")
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)

                                If ctrl.name.ToString.ToUpper = "FTPayYear".ToUpper Then
                                    _DataCon = .Text
                                Else
                                    _DataCon = .SelectedIndex.ToString
                                End If

                            End With
                    End Select

                    If _Where = "" Then
                        _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                    Else
                        _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                    End If


                Next
            Next

        End If

        '------------Browse Where Require Field---------------

        '------------Browse Where Optional Field---------------
        If _FTBrwCmdFieldOptional <> "" Then
            For Each _QryCon As String In _FTBrwCmdFieldOptional.Split(",")
                Dim _DataCon As String = ""
                For Each ctrl As Object In _form.Controls.Find(_QryCon.Trim(), True)
                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                        Case ENM.Control.ControlType.TextEdit
                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                _DataCon = .Text
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(ctrl, DevExpress.XtraEditors.CalcEdit)
                                _DataCon = .Value
                            End With
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                If Microsoft.VisualBasic.Left(.Name.ToString, 6).ToUpper = "FNHSys".ToUpper And .Text <> "" Then
                                    _DataCon = "" & .Properties.Tag.ToString
                                Else
                                    _DataCon = .Text
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit
                            With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                _DataCon = .Text
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                Select Case .Properties.DisplayFormat.ToString
                                    Case "dd/MM/yyyy"
                                        _DataCon = HI.UL.ULDate.ConvertEnDB(.Text)
                                    Case "dd/MM"
                                        _DataCon = Microsoft.VisualBasic.Right(.Text, 2) & "/" & Microsoft.VisualBasic.Left(.Text, 2)
                                    Case "MM/yyyy"
                                        _DataCon = Microsoft.VisualBasic.Right(.Text, 4) & "/" & Microsoft.VisualBasic.Left(.Text, 2)
                                    Case Else
                                        _DataCon = .Text
                                End Select

                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(ctrl, DevExpress.XtraEditors.CheckEdit)
                                _DataCon = IIf(.Checked, "1", "0")
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)
                                If ctrl.name.ToString.ToUpper = "FTPayYear".ToUpper Then
                                    _DataCon = .Text
                                Else
                                    _DataCon = .SelectedIndex.ToString
                                End If
                            End With
                    End Select

                    If _DataCon <> "" Then
                        If _Where = "" Then
                            _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                        Else
                            _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                        End If
                    End If
                Next
            Next
        End If

        '------------Browse Where Optional Field---------------
        If _Where <> "" Then
            If _BrowseWhereCmd = "" Then
                _Where = "   WHERE  " & _Where
            Else
                _Where = "   AND  " & _Where
            End If

        End If

        If Not (HI.ST.SysInfo.Admin) Then
            If Microsoft.VisualBasic.Left(Me.ParentControl.name.ToString.ToUpper, 11) = "FNTSysEmpID".ToUpper Then
                _Where = HI.ST.Security.PermissionEmpData(_Where)
            ElseIf Microsoft.VisualBasic.Left(Me.ParentControl.name.ToString.ToUpper, 15) = "FNTSysEmpTypeId".ToUpper Then
                _Where = HI.ST.Security.PermissionEmpType(_Where)
            ElseIf Microsoft.VisualBasic.Left(Me.ParentControl.name.ToString.ToUpper, 9) = "FTOrderNo".ToUpper Or Microsoft.VisualBasic.Left(Me.ParentControl.name.ToString.ToUpper, 11) = "FTOrderNoTo".ToUpper Then
                _Where = HI.ST.Security.PermissionOrderCmpData(_Where)
            End If
        End If


        _dtbrw = New DataTable
        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

        Me.Data = _dtbrw.Copy
        Me.DataRetField = _dtret.Copy

        'BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag
        For Each R As DataRow In Me.DataRetField.Select("FTRetField='" & Me.ParentControl.name.ToString & "' AND FTStatePropertyTag<>'Y' ")
            FieldFilter = R!FTBrwField.ToString
            Exit For
        Next

        Me.ogcbrowse.DataSource = Me.Data
        _dtbrw.Dispose()
        _dtret.Dispose()


        Me.OldBrwID = BrwID
    End Sub

    Public Sub CreateFilter(Filter As String)
        ogvbrowse.ClearColumnsFilter()
        ogvbrowse.ActiveFilter.Clear()
        ogvbrowse.ActiveFilter.Add(ogvbrowse.Columns(FieldFilter), New ColumnFilterInfo("[" & FieldFilter & "] Like '%" & Filter & "%'", ""))
    End Sub

    Public Sub ClearFilter()
        ogvbrowse.ClearColumnsFilter()
        ogvbrowse.ActiveFilter.Clear()
        'ogvbrowse.ActiveFilter.Add(ogvbrowse.Columns(FieldFilter), New ColumnFilterInfo("[" & FieldFilter & "] Like '%" & Filter & "%'", ""))
    End Sub

#End Region
    Private Sub RetuenValue()
        Try
            With ogvbrowse
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                For Each Row As DataRow In Me.DataRetField.Rows

                    If Not (.Columns.ColumnByName(Row!FTBrwField.ToString) Is Nothing) Then
                        Row!ValuesRet = "" & .GetRowCellValue(.FocusedRowHandle, Row!FTBrwField.ToString).ToString
                    Else
                        Row!ValuesRet = ""
                    End If
                Next

                If Not (Me.Data.Rows(.FocusedRowHandle) Is Nothing) Then

                    For Each Row As DataRow In Me.DataRetField.Select("NOT(FTRetField IS NULL)", "FNSeq")
                        For Each ctrl As Object In Me.FormParent.Controls.Find(Row!FTRetField.ToString.Trim(), True)
                            Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                Case ENM.Control.ControlType.TextEdit
                                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                        .Text = Row!ValuesRet.ToString
                                    End With
                                Case ENM.Control.ControlType.CalcEdit
                                    With CType(ctrl, DevExpress.XtraEditors.CalcEdit)
                                        .Value = Val(Row!ValuesRet.ToString)
                                    End With
                                Case ENM.Control.ControlType.ButtonEdit

                                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            .Properties.Tag = Row!ValuesRet.ToString
                                        Else
                                            .Text = Row!ValuesRet.ToString
                                        End If
                                    End With
                                Case ENM.Control.ControlType.MemoEdit
                                    With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                        .Text = Row!ValuesRet.ToString
                                    End With
                                Case ENM.Control.ControlType.DateEdit
                                    With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                        .Text = Row!ValuesRet.ToString
                                    End With
                                Case ENM.Control.ControlType.CheckEdit
                                    With CType(ctrl, DevExpress.XtraEditors.CheckEdit)
                                        .Checked = Val(Row!ValuesRet.ToString)
                                    End With
                                Case ENM.Control.ControlType.ComboBoxEdit
                                    With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)
                                        Try
                                            .SelectedIndex = Val(Row!ValuesRet.ToString)
                                        Catch ex As Exception
                                            .SelectedIndex = -1
                                        End Try
                                    End With
                            End Select
                        Next
                    Next
                End If

                Call ClearFilter()

                Me.Visible = False
                Me.ParentControl.Focus()

            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ogvbrowse_Click(sender As Object, e As EventArgs) Handles ogvbrowse.Click
        Call RetuenValue()
    End Sub

    Public Sub DisposeObject()
        Me.ogcbrowse.DataSource = Nothing
        Me._Data = Nothing
        Me._DataRetField = Nothing
        Me.Visible = False

    End Sub

    'Private Sub ogvbrowse_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles ogvbrowse.KeyDown
    '    Select Case e.KeyCode
    '        Case Windows.Forms.Keys.Enter
    '            Call RetuenValue()
    '    End Select
    'End Sub
End Class
