Imports System.Data
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views
Imports DevExpress.Utils
Imports System.Globalization
Imports System.Threading
Imports System.Globalization.DateTimeFormatInfo
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wDynamicBrowseSelectInfo

    Sub New(BrwID As Object, Optional ByVal _SpaWhere As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.InitFormControl(BrwID, _SpaWhere)
    End Sub
    

    Private Sub InitFormControl(BrwID As Object, Optional ByVal _SpaWhere As String = "")


        Dim _Qry As String = ""

        Dim _dt As DataTable
        Dim _QryQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _BrwObjID As Integer = 0

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        Finally
        End Try

        Call GetData(BrwID, _SpaWhere)

        _Qry = "   SELECT  TOP 1  *  "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE BrwID='" & BrwID & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        With Me
            If _dt.Rows.Count > 0 Then
                _BrwObjID = Val(_dt.Rows(0)!BrwObjID.ToString)
                .Text = "Browse Data"

                If Val(_dt.Rows(0)!FNFormBrwHeight.ToString) > 50 Then
                    .Height = Integer.Parse(Val(_dt.Rows(0)!FNFormBrwHeight.ToString))
                Else
                    .Height = 200
                End If

                If Val(_dt.Rows(0)!FNFormBrwWidth.ToString) > 50 Then
                    .Width = Integer.Parse(Val(_dt.Rows(0)!FNFormBrwWidth.ToString))
                Else
                    .Width = 300
                End If

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then

                    If HI.ST.SysInfo.Admin Then
                        .Text = _dt.Rows(0)!FTBrwCaptionTH.ToString & " (  Browse No .  " & BrwID.ToString & "  )"
                    Else
                        .Text = _dt.Rows(0)!FTBrwCaptionTH.ToString
                    End If

                Else
                    If HI.ST.SysInfo.Admin Then
                        .Text = _dt.Rows(0)!FTBrwCaptionEN.ToString & " (  Browse No .  " & BrwID.ToString & "  )"
                    Else
                        .Text = _dt.Rows(0)!FTBrwCaptionEN.ToString
                    End If

                End If

            Else
                If HI.ST.SysInfo.Admin Then
                    .Text = "Browse Data" & " (  Browse No .  " & BrwID.ToString & "  )"
                Else
                    .Text = "Browse Data"
                End If

                .Width = 300
                .Height = 200
            End If
        End With


        _Qry = "SELECT * "
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseObj WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE BrwObjID='" & _BrwObjID & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        With ogvbrowse

            Dim GridCol2 As New DevExpress.XtraGrid.Columns.GridColumn
            With GridCol2

                .Name = "FTSelect"
                .FieldName = "FTSelect"
                .Width = 50

                .AppearanceCell.Options.UseTextOptions = True
                .AppearanceHeader.Options.UseTextOptions = True
                .AppearanceHeader.TextOptions.HAlignment = VertAlignment.Center
                .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                .OptionsColumn.ShowInCustomizationForm = False
                .OptionsColumn.AllowSize = True
                .VisibleIndex = _ColCount
                .OptionsColumn.AllowEdit = True

                .Visible = True

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    .Caption = " "
                Else
                    .Caption = " "
                End If

                .ColumnEdit = RepositoryFTSelect

                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near


            End With

            .Columns.Add(GridCol2)
            _ColCount = _ColCount + 1
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
                    .OptionsColumn.AllowSize = True
                    .VisibleIndex = _ColCount
                    .OptionsColumn.AllowEdit = False

                    .Visible = (R!FTStateHide.ToString <> "Y")

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
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
                            .DisplayFormat.FormatType = FormatType.Numeric
                            .DisplayFormat.FormatString = "{0:n" & (Integer.Parse(Val(R!FNNumericScale.ToString))).ToString & "}"
                        Case Else
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    End Select

                End With

                .Columns.Add(GridCol)

                _ColCount = _ColCount + 1

            Next

            .OptionsCustomization.AllowSort = True
            .OptionsCustomization.AllowColumnMoving = False
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

    End Sub

    Private Sub GetData(BrwID As Object, Optional ByVal _SpaWhere As String = "")
        Try
            Dim _Qrysql As String = ""

            Dim _dtbrw As DataTable
            Dim _dtret As DataTable
            Dim _BrowseCmd As String = ""
            Dim _BrowseSortCmd As String = ""
            Dim _BrowseWhereCmd As String = ""
            Dim _FTBrwCmdField As String = ""
            Dim _FTBrwCmdFieldOptional As String = ""
            Dim FTBrwCmdGroupBy As String = ""

            _Qrysql = " SELECT  TOP 1    BrwID, "

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
            Else
                _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
            End If

            _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy "
            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
            _Qrysql &= vbCrLf & " WHERE BrwID=" & BrwID & " "

            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
            _Qrysql = ""
            For Each Row As DataRow In _dtbrw.Rows
                _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

                _dtret = New DataTable
                _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                _BrowseCmd = Row!FTBrwCmd.ToString
                _BrowseSortCmd = Row!FTBrwCmdSort.ToString
                _BrowseWhereCmd = Row!FTBrwCmdWhere.ToString

                _FTBrwCmdField = Row!FTBrwCmdField.ToString
                _FTBrwCmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    FTBrwCmdGroupBy = Row!FTBrwCmdTHGroupBy.ToString
                Else
                    FTBrwCmdGroupBy = Row!FTBrwCmdENGroupBy.ToString
                End If

                _Qrysql = Row!FTBrwCmd.ToString
            Next

            If _Qrysql = "" Then Exit Sub

            Dim _Where As String = ""


            Dim I As Integer = 0

            If _SpaWhere <> "" Then
                If _Where = "" Then
                    _Where &= " WHERE " & _SpaWhere
                Else
                    _Where &= " AND " & _SpaWhere
                End If
            End If

            _dtbrw = New DataTable
            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)
            _dtbrw.Columns.Add("FTSelect", GetType(String))
            With _dtbrw
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTSelect = "0"
                Next
                .AcceptChanges()
            End With
            Me.Data = _dtbrw
        Catch ex As Exception

        End Try
    End Sub

#Region "Property"
    Private _Proc As Boolean
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private _ChoseData As Boolean = False
    Private _Caption As String = "Browse Data"
    Public Property Caption() As String
        Get
            Return _Caption
        End Get
        Set(value As String)
            _Caption = value
        End Set
    End Property

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

    Private _FormSize As Integer = 300
    Public Property BrowseSize() As Integer

        Get
            Return _FormSize
        End Get

        Set(value As Integer)
            _FormSize = value
        End Set

    End Property

    Private _KeyReturn As String = ""
    Public Property KeyReturn() As String
        Get
            Return _KeyReturn
        End Get
        Set(value As String)
            _KeyReturn = value
        End Set
    End Property

    Private _ValuesReturn As DataRow
    Public Property ValuesReturn() As DataRow
        Get
            Return _ValuesReturn
        End Get
        Set(value As DataRow)
            _ValuesReturn = value
        End Set
    End Property

    Private _BrowseID As Integer
    Public Property BrowseID() As Integer
        Get
            Return _BrowseID
        End Get
        Set(value As Integer)
            _BrowseID = value
        End Set
    End Property

    Private _X As Integer = 0
    Public Property X As Integer
        Get
            Return _X
        End Get
        Set(value As Integer)
            _X = value
        End Set
    End Property

    Private _Y As Integer = 0
    Public Property Y As Integer
        Get
            Return _Y
        End Get
        Set(value As Integer)
            _Y = value
        End Set
    End Property

#End Region

    Private Sub wBrowseItemInfo_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            _Data.Dispose()
            If _ChoseData = False Then
                Me.ValuesReturn = Nothing
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wBrowseItemInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            _ChoseData = False

            Dim _BrwSize As Integer = Me.Width
            _BrwSize = _BrwSize + 50
            If _BrwSize > My.Computer.Screen.Bounds.Width Then
                _BrwSize = (My.Computer.Screen.Bounds.Width * 80) / 100
            End If

            Dim X As Integer = 0
            Dim Y As Integer = 0

            If Me.X <> -1 Then
                X = Me.X - 50
            Else
                X = MousePosition.X - 50
            End If

            If X < 0 Then X = 0
            If Me.Y <> -1 Then
                Y = Me.Y - 100
            Else
                Y = MousePosition.Y - 100
            End If

            If Y < 0 Then Y = 0

            If My.Computer.Screen.Bounds.Width < X + Me.Width Then
                X = (My.Computer.Screen.Bounds.Width - Me.Width)
            End If

            If My.Computer.Screen.Bounds.Height < Y + Me.Height + 50 Then
                Y = (My.Computer.Screen.Bounds.Height - Me.Height) - 50
            End If

            Me.ogcbrowse.DataSource = Me.Data
            Me.Location = New System.Drawing.Point(X + 300, Y)

            Me.ValuesReturn = Nothing

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    'Private Sub ogcbrowse_DoubleClick(sender As Object, e As System.EventArgs) Handles ogcbrowse.DoubleClick
    '    Try
    '        With ogvbrowse
    '            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
    '            ' If .GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString = "" Then Exit Sub

    '            For Each Row As DataRow In Me.DataRetField.Rows

    '                If Not (.Columns.ColumnByName(Row!FTBrwField.ToString) Is Nothing) Then
    '                    Row!ValuesRet = "" & .GetRowCellValue(.FocusedRowHandle, Row!FTBrwField.ToString).ToString
    '                Else
    '                    Row!ValuesRet = ""
    '                End If
    '            Next

    '            Me.ValuesReturn = Me.Data.Rows(.FocusedRowHandle) '.GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString
    '            _ChoseData = True
    '            Me.Close()
    '        End With
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub ogvbrowse_Click(sender As Object, e As System.EventArgs) Handles ogvbrowse.Click
    '    Try
    '        With ogvbrowse
    '            Dim pt As Point = ogvbrowse.GridControl.PointToClient(Control.MousePosition)
    '            Dim info As GridHitInfo = ogvbrowse.CalcHitInfo(pt)

    '            If (info.InRow Or info.InRowCell) Then
    '                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
    '                ' If .GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString = "" Then Exit Sub

    '                For Each Row As DataRow In Me.DataRetField.Rows

    '                    If Not (.Columns.ColumnByName(Row!FTBrwField.ToString) Is Nothing) Then
    '                        Row!ValuesRet = "" & .GetRowCellValue(.FocusedRowHandle, Row!FTBrwField.ToString).ToString
    '                    Else
    '                        Row!ValuesRet = ""
    '                    End If
    '                Next

    '                Me.ValuesReturn = Me.Data.Rows(.FocusedRowHandle) '.GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString
    '                _ChoseData = True
    '                Me.Close()
    '            End If

    '        End With
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub ogvbrowse_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvbrowse.KeyDown
    '    Try
    '        Select Case e.KeyCode
    '            Case System.Windows.Forms.Keys.Enter
    '                With ogvbrowse
    '                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
    '                    ' If .GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString = "" Then Exit Sub

    '                    For Each Row As DataRow In Me.DataRetField.Rows

    '                        If Not (.Columns.ColumnByName(Row!FTBrwField.ToString) Is Nothing) Then
    '                            Row!ValuesRet = "" & .GetRowCellValue(.FocusedRowHandle, Row!FTBrwField.ToString).ToString
    '                        Else
    '                            Row!ValuesRet = ""
    '                        End If
    '                    Next

    '                    Me.ValuesReturn = Me.Data.Rows(.FocusedRowHandle) '.GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString
    '                    _ChoseData = True
    '                    Me.Close()
    '                End With
    '            Case System.Windows.Forms.Keys.Escape
    '                Me.Close()
    '        End Select
    '    Catch ex As Exception
    '    End Try
    'End Sub

 

    Private Sub otbClose_Click(sender As Object, e As EventArgs) Handles otbClose.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub obtSelect_Click_1(sender As Object, e As EventArgs) Handles obtSelect.Click
        Try
            _Proc = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class