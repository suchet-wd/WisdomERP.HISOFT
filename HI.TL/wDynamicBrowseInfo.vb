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

Public Class wDynamicBrowseInfo

    Private _AppActScreen As Integer = 0
    Sub New(BrwID As Object, FormParent As Object)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        MainFormParent = FormParent
        Me.InitFormControl(BrwID)

    End Sub

    Private _MainFormParent As Object = Nothing
    Property MainFormParent As Object
        Get
            Return _MainFormParent
        End Get
        Set(value As Object)
            _MainFormParent = value
        End Set
    End Property

    Private Sub InitFormControl(BrwID As Object)

        Dim _Qry As String = ""

        Dim _dt As DataTable
        Dim _QryQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _BrwObjID As Integer = 0

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
                    .FormHight = Integer.Parse(Val(_dt.Rows(0)!FNFormBrwHeight.ToString))
                Else
                    .Height = 200
                    .FormHight = 200
                End If

                If Val(_dt.Rows(0)!FNFormBrwWidth.ToString) > 50 Then

                    .Width = Integer.Parse(Val(_dt.Rows(0)!FNFormBrwWidth.ToString))
                    .FormWidth = Integer.Parse(Val(_dt.Rows(0)!FNFormBrwWidth.ToString))

                Else

                    .Width = 300
                    .FormWidth = 300

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

                    Select Case R!FTFieldType.ToString.ToUpper
                        Case "T".ToUpper
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                        Case "C".ToUpper
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                            .ColumnEdit = RepCheckEdit
                        Case "D".ToUpper
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        Case "N".ToUpper
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

        Try
            Dim allScreens As Screen() = Screen.AllScreens
            Dim currentScreen As Screen = Screen.FromControl(MainFormParent)
            _AppActScreen = Array.IndexOf(allScreens, currentScreen)
        Catch ex As Exception

        End Try

    End Sub

#Region "Property"

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

    Private _FormHight As Integer = 300
    Public Property FormHight() As Integer

        Get
            Return _FormHight
        End Get

        Set(value As Integer)
            _FormHight = value
        End Set

    End Property

    Private _FormWidth As Integer = 300
    Public Property FormWidth() As Integer

        Get
            Return _FormWidth
        End Get

        Set(value As Integer)
            _FormWidth = value
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

            Dim currentScreen As Screen = Screen.FromPoint(Cursor.Position)

            ' Dim currentScreen As Screen = Screen.FromControl(Me)
            Try


                'If _AppActScreen > 0 Then
                '    currentScreen = Screen.AllScreens(_AppActScreen)
                'End If

            Catch ex As Exception
            End Try

            Me.Height = Me.FormHight
            Me.Width = Me.FormWidth

            Dim _BrwSize As Integer = Me.FormWidth
            _BrwSize = _BrwSize + 50
            'If _BrwSize > My.Computer.Screen.Bounds.Width Then
            '    _BrwSize = (My.Computer.Screen.Bounds.Width * 80) / 100
            'End If

            If _BrwSize > currentScreen.Bounds.Width Then
                _BrwSize = (currentScreen.Bounds.Width * 80) / 100
            End If

            Dim X As Integer = 0
            Dim Y As Integer = 0

            If Me.X <> -1 Then

                If Me.X < 0 Then
                    'If _AppActScreen > 0 Then
                    '    X = ((Screen.AllScreens(_AppActScreen).Bounds.Width) - System.Math.Abs(Me.X)) - 50
                    'Else
                    X = System.Math.Abs(Me.X) - 50
                    'End If

                Else

                    X = System.Math.Abs(Me.X) - 50
                End If

            Else
                X = System.Math.Abs(MousePosition.X) - 50
            End If

            If X < 0 Then X = 0
            If Me.Y <> -1 Then
                Y = Me.Y - 100
            Else
                Y = MousePosition.Y - 100
            End If

            If Y < 0 Then Y = 0

            'If My.Computer.Screen.Bounds.Width < X + Me.Width Then
            '    X = (My.Computer.Screen.Bounds.Width - Me.Width)
            'End If

            'If My.Computer.Screen.Bounds.Height < Y + Me.Height + 50 Then
            '    Y = (My.Computer.Screen.Bounds.Height - Me.Height) - 50
            'End If

            If currentScreen.Bounds.Width < X + Me.Width Then
                X = (currentScreen.Bounds.Width - Me.Width)
            End If

            If currentScreen.Bounds.Height < Y + Me.Height + 50 Then
                Y = (currentScreen.Bounds.Height - Me.Height) - 50
            End If

            Me.ogcbrowse.DataSource = Me.Data

            Me.Location = currentScreen.Bounds.Location + New System.Drawing.Point(X, Y)
            Me.Height = Me.FormHight
            Me.Width = Me.FormWidth

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

    Private Sub ogvbrowse_Click(sender As Object, e As System.EventArgs) Handles ogvbrowse.Click
        Try
            With ogvbrowse
                Dim pt As Point = ogvbrowse.GridControl.PointToClient(Control.MousePosition)
                Dim info As GridHitInfo = ogvbrowse.CalcHitInfo(pt)

                If (info.InRow Or info.InRowCell) Then
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                    ' If .GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString = "" Then Exit Sub

                    For Each Row As DataRow In Me.DataRetField.Rows

                        If Not (.Columns.ColumnByName(Row!FTBrwField.ToString) Is Nothing) Then
                            Row!ValuesRet = "" & .GetRowCellValue(.FocusedRowHandle, Row!FTBrwField.ToString).ToString
                        Else
                            Row!ValuesRet = ""
                        End If
                    Next

                    Me.ValuesReturn = Me.Data.Rows(.FocusedRowHandle) '.GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString
                    _ChoseData = True
                    Me.Close()
                End If

            End With
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ogvbrowse_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvbrowse.KeyDown

        Try

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Enter
                    With ogvbrowse
                        If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                        ' If .GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString = "" Then Exit Sub

                        For Each Row As DataRow In Me.DataRetField.Rows

                            If Not (.Columns.ColumnByName(Row!FTBrwField.ToString) Is Nothing) Then
                                Row!ValuesRet = "" & .GetRowCellValue(.FocusedRowHandle, Row!FTBrwField.ToString).ToString
                            Else
                                Row!ValuesRet = ""
                            End If
                        Next

                        Me.ValuesReturn = Me.Data.Rows(.FocusedRowHandle) '.GetRowCellValue(.FocusedRowHandle, Me.KeyReturn).ToString
                        _ChoseData = True
                        Me.Close()
                    End With
                Case System.Windows.Forms.Keys.Escape
                    Me.Close()
            End Select

        Catch ex As Exception
        End Try

    End Sub


End Class