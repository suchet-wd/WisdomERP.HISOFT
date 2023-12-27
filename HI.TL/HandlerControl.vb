Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports System.Data
Imports System.Windows.Forms.Control
Imports System.Reflection
Imports System.Globalization
Imports System.Threading
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.Export

Public Class HandlerControl

    Private Shared _StateProc As Boolean = False
    Private Shared _StateResponProc As Boolean = False
    Private Shared _StateResponProcNormal As Boolean = False
    Private Shared _StateProcClear As Boolean = False
    Private Shared _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip
    Private Shared _ContextMenuStripPicture As System.Windows.Forms.ContextMenuStrip
    Private Shared _ContextMenuStripPivotGrid As System.Windows.Forms.ContextMenuStrip
    Private Shared _ContextMenuStripChart As System.Windows.Forms.ContextMenuStrip
    Private Declare Function EmptyWorkingSet Lib "psapi.dll" (ByVal hProcess As IntPtr) As Long
    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32

    Private Shared ExportStart As Boolean = False

    Public Shared Property SetStateProcClear As Boolean
        Get
            Return _StateProcClear
        End Get
        Set(value As Boolean)
            _StateProcClear = value
        End Set
    End Property

    Public Shared Sub ClearControl(ByVal ObjParent As Object, Optional ByVal StateLoop As Boolean = False, Optional ByVal NotClearObject() As String = Nothing)
        If ObjParent Is Nothing Then Exit Sub

        If StateLoop = False Then
            SetStateProcClear = True
        End If

        On Error Resume Next
        'If ObjParent.tag.ToString() = "999" Then
        '    Exit Sub
        'End If

        For Each Obj As Object In ObjParent.Controls
            Dim tText As String = ""
            tText = Microsoft.VisualBasic.Left(Obj.Tag.ToString.Trim & " ", 1)

            If tText = "2" And Not (NotClearObject Is Nothing) Then
                If Array.IndexOf(NotClearObject, Obj.name.ToString) >= 0 Then
                    tText = ""
                End If
            End If

            If tText = "2" Or tText = "3" Then
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.HButtonDropDown
                        With CType(Obj, HI.UCTR.HButtonDropDown)
                            If (.Visible) Then
                                .DisposeObject()
                            End If
                        End With
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Text = ""
                            .Properties.Tag = ""
                        End With

                    Case ENM.Control.ControlType.MemoEdit
                        CType(Obj, DevExpress.XtraEditors.MemoEdit).Text = ""

                    Case ENM.Control.ControlType.ComboBoxEdit
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            If .Properties.Items.Count > 0 Then
                                .SelectedIndex = 0
                            Else
                                .SelectedIndex = -1
                            End If
                        End With
                    Case ENM.Control.ControlType.TextEdit
                        CType(Obj, DevExpress.XtraEditors.TextEdit).Text = ""

                    Case ENM.Control.ControlType.DateEdit

                        Select Case Obj.name.ToString.ToUpper
                            Case "odedocdate".ToUpper
                                CType(Obj, DevExpress.XtraEditors.DateEdit).DateTime = Now
                            Case Else
                                CType(Obj, DevExpress.XtraEditors.DateEdit).Text = ""
                        End Select
                    Case ENM.Control.ControlType.TimeEdit
                        CType(Obj, DevExpress.XtraEditors.TimeEdit).Text = ""

                    Case ENM.Control.ControlType.CalcEdit
                        CType(Obj, DevExpress.XtraEditors.CalcEdit).Value = 0


                    Case ENM.Control.ControlType.RadioGroup
                        With CType(Obj, DevExpress.XtraEditors.RadioGroup)
                            If .Properties.Items.Count > 0 Then
                                .SelectedIndex = 0
                            Else
                                .SelectedIndex = -1
                            End If
                        End With

                    Case ENM.Control.ControlType.CheckEdit
                        CType(Obj, DevExpress.XtraEditors.CheckEdit).Checked = False

                    Case ENM.Control.ControlType.GridControl
                        With CType(Obj, DevExpress.XtraGrid.GridControl)
                            .DataSource = Nothing
                        End With

                    Case ENM.Control.ControlType.PictureEdit
                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            .Image = Nothing
                        End With

                    Case ENM.Control.ControlType.GridLookUpEdit
                        With CType(Obj, DevExpress.XtraEditors.GridLookUpEdit)
                            .EditValue = Nothing
                        End With
                    Case ENM.Control.ControlType.RichEditControl
                        With CType(Obj, DevExpress.XtraRichEdit.RichEditControl)
                            .HtmlText = ""
                        End With
                End Select
            End If

            If Obj.Controls.count > 0 Then
                Call ClearControl(Obj, True, NotClearObject)
            End If

        Next

        If StateLoop = False Then
            SetStateProcClear = False
        End If


    End Sub

#Region "Handler Control"

    Public Shared Sub AddHandlerObj(ByVal ObjForm As Object)
        Try

            Try
                If ObjForm.tag.ToString() = "999" Then
                    Exit Sub
                End If
            Catch ex As Exception
            End Try


            For Each Obj As Object In ObjForm.Controls
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                            If .Properties.Buttons.Item(0).Visible Then
                                AddHandler .KeyDown, AddressOf ButtonEdit_KeyDown
                            End If

                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow
                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue
                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue
                            .Properties.CharacterCasing = CharacterCasing.Upper

                            If .Properties.Buttons.Count > 0 Then

                                If "" & .Properties.Buttons(0).Tag <> "" Then
                                    AddHandler .ButtonClick, AddressOf DynamicButtone_ButtonClick
                                    AddHandler .EditValueChanged, AddressOf DynamicButtonedit_EditValueChanged
                                    AddHandler .Leave, AddressOf DynamicButtonedit_LeaveOnly
                                    AddHandler .KeyDown, AddressOf DynamicButtonedit_KeyDown


                                    If .Properties.ReadOnly = False And .Enabled Then
                                        Dim _BrwDropDown As New HI.UCTR.HButtonDropDown(Val(.Properties.Buttons(0).Tag.ToString))
                                        _BrwDropDown.Name = .Name.ToString & "_Browse"

                                        Dim _Form As Object = (.Parent.FindForm)
                                        _BrwDropDown.FormParent = _Form
                                        _BrwDropDown.ParentControl = Obj
                                        '_BrwDropDown.Location = New System.Drawing.Point(.Location.X + .Height + 5, .Location.Y)
                                        _BrwDropDown.Location = New System.Drawing.Point(.Location.X, .Location.Y + .Height + 3)
                                        _BrwDropDown.BrwID = .Properties.Buttons(0).Tag.ToString
                                        _BrwDropDown.Visible = False
                                        _BrwDropDown.Tag = "2|"
                                        _BrwDropDown.BringToFront()
                                        ' .Parent.Controls.Add(_BrwDropDown)
                                        _Form.Controls.Add(_BrwDropDown)
                                        AddHandler .KeyPress, AddressOf DynamicButtonEdit_KeyPress
                                        AddHandler .EditValueChanging, AddressOf ButtonEdit_EditValueChanging

                                        If HI.ST.SysInfo.FTOptionMouseScoll = "1" Then

                                            AddHandler .Spin, AddressOf ButtonEdit_Spin

                                        End If

                                    End If

                                End If

                            End If

                            .EnterMoveNextControl = True

                            If (.Properties.ReadOnly) Then
                                .TabStop = False
                            Else
                                .TabStop = True
                            End If

                        End With
                    Case ENM.Control.ControlType.MemoEdit

                        With CType(Obj, DevExpress.XtraEditors.MemoEdit)

                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow

                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue

                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue
                            .EnterMoveNextControl = False

                            If (.Properties.ReadOnly) Then
                                .TabStop = False
                            Else
                                .TabStop = True
                            End If

                        End With

                    Case ENM.Control.ControlType.ComboBoxEdit
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)

                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow

                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue

                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue

                            Try
                                If "" & .Properties.Tag.ToString <> "" Then
                                    .Properties.Items.Clear()
                                    .Properties.Items.AddRange(HI.TL.CboList.SetList("" & .Properties.Tag.ToString))
                                End If
                            Catch ex As Exception
                            End Try

                            If .Properties.Items.Count > 0 Then
                                .SelectedIndex = 0
                            Else
                                .SelectedIndex = -1
                            End If

                            .EnterMoveNextControl = True

                            If Obj.Name.ToString.ToUpper = "FNIssueBarType".ToUpper Then
                                .TabStop = True
                            Else
                                If (.Properties.ReadOnly) Then
                                    .TabStop = False
                                Else
                                    .TabStop = True
                                End If
                            End If

                        End With

                    Case ENM.Control.ControlType.CheckedComboBoxEdit
                        With CType(Obj, DevExpress.XtraEditors.CheckedComboBoxEdit)

                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow

                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue

                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue

                            Try
                                If "" & .Properties.Tag.ToString <> "" Then
                                    .Properties.Items.Clear()
                                    .Properties.Items.AddRange(HI.TL.CboList.SetList("" & .Properties.Tag.ToString))
                                End If
                            Catch ex As Exception
                            End Try


                            .EnterMoveNextControl = True

                            If (.Properties.ReadOnly) Then
                                    .TabStop = False
                                Else
                                    .TabStop = True
                                End If


                        End With

                    Case ENM.Control.ControlType.TextEdit
                        With CType(Obj, DevExpress.XtraEditors.TextEdit)
                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow
                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue
                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue

                            If Obj.Name.ToString.ToUpper = "FTBarcodeNo".ToUpper Then
                                .EnterMoveNextControl = False
                            Else
                                .EnterMoveNextControl = True
                            End If

                            If (.Properties.ReadOnly) Then
                                .TabStop = False
                            Else
                                .TabStop = True

                            End If

                            AddHandler .Leave, AddressOf Textedit_Leave

                        End With

                    Case ENM.Control.ControlType.CalcEdit
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)

                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow

                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue

                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue

                            Select Case .Name.ToString.ToUpper
                                Case .Name.ToString.ToUpper Like "*QTY*"
                                Case .Name.ToString.ToUpper Like "*QUANTITY*"
                                    .Properties.Precision = HI.ST.Config.QtyDigit
                                Case .Name.ToString.ToUpper Like "*AMT*"
                                    .Properties.Precision = HI.ST.Config.AmtDigit
                                Case .Name.ToString.ToUpper Like "*PRICE*"
                                    .Properties.Precision = HI.ST.Config.PriceDigit
                                Case .Name.ToString.ToUpper Like "*EXC*"
                                    .Properties.Precision = HI.ST.Config.ExcDigit
                            End Select

                            .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .Properties.DisplayFormat.FormatString = "N" & .Properties.Precision.ToString

                            AddHandler .Leave, AddressOf CalEdit_Leave
                            AddHandler .Spin, AddressOf Caledit_Spin

                            .EnterMoveNextControl = True
                            If (.Properties.ReadOnly) Then
                                .TabStop = False
                            Else
                                .TabStop = True
                            End If

                        End With


                    Case ENM.Control.ControlType.CheckEdit

                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                            AddHandler .CheckedChanged, AddressOf Checkbox_CheckedChanged
                            .TabStop = False

                            If HI.ST.SysInfo.Admin Then
                                .ToolTip = .Name.ToString
                            End If

                        End With

                    Case ENM.Control.ControlType.PictureEdit

                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            If .Properties.ReadOnly = False Then
                                .Properties.ContextMenuStrip = _ContextMenuStripPicture
                                .TabStop = False
                            End If

                            Dim _SuperToolTip As New DevExpress.Utils.SuperToolTip()
                            Dim _ToolTipTitleItem As New DevExpress.Utils.ToolTipTitleItem()

                            _ToolTipTitleItem.Appearance.Image = .Image
                            _ToolTipTitleItem.Appearance.Options.UseImage = True
                            _ToolTipTitleItem.Image = .Image
                            _ToolTipTitleItem.Text = ""

                            With _SuperToolTip
                                .Items.Add(_ToolTipTitleItem)
                            End With

                            .SuperTip = _SuperToolTip

                            AddHandler .ImageChanged, AddressOf HI.TL.HandlerControl.PictureClear_ImageChanged
                        End With

                    Case ENM.Control.ControlType.DateEdit

                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow
                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue
                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue
                            .Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True
                            .Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret

                            If .Properties.DisplayFormat.FormatString.ToUpper = "d".ToUpper Or .Properties.DisplayFormat.FormatString.ToUpper = "dd/MM/yyyy".ToUpper Then
                                Dim _Culture As New CultureInfo("en-US", True)
                                _Culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                                _Culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                                System.Threading.Thread.CurrentThread.CurrentCulture = _Culture
                                System.Threading.Thread.CurrentThread.CurrentUICulture = _Culture

                                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
                                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"

                                .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                .Properties.DisplayFormat.FormatString = "d"
                                .Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                .Properties.EditFormat.FormatString = "d"
                                .Properties.EditMask = "d"
                                .Properties.DisplayFormat.FormatString = ("d")
                                .Properties.Mask.Culture = _Culture
                                .Properties.Mask.UseMaskAsDisplayFormat = False

                                AddHandler .GotFocus, AddressOf DateEdit_GotFocus
                                AddHandler .Spin, AddressOf Caledit_Spin
                            End If

                        End With
                    Case ENM.Control.ControlType.TimeEdit

                        With CType(Obj, DevExpress.XtraEditors.TimeEdit)
                            .Properties.AppearanceFocused.ForeColor = Color.Blue
                            .Properties.AppearanceFocused.BackColor = Color.GreenYellow
                            .Properties.AppearanceReadOnly.BackColor = Color.LightCyan
                            .Properties.AppearanceReadOnly.ForeColor = Color.Blue
                            .Properties.AppearanceDisabled.BackColor = Color.LightCyan
                            .Properties.AppearanceDisabled.ForeColor = Color.Blue
                            .Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True
                            .Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                            AddHandler .Spin, AddressOf Caledit_Spin
                        End With
                    Case ENM.Control.ControlType.GridControl

                        With CType(Obj, DevExpress.XtraGrid.GridControl)
                            Dim _FoundProperty As Boolean = False
                            Dim _FormShow As Object = .FindForm()


                            If Not (_FormShow Is Nothing) Then
                                Dim T As System.Type = _FormShow.GetType()
                                Dim _pdbnameinfo As PropertyInfo = T.GetProperty("CallMenuName")

                                If Not (_pdbnameinfo Is Nothing) Then
                                    _FoundProperty = True

                                End If
                            End If

                            Dim _ObjMainView As Object = .MainView

                            Select Case HI.ENM.Control.GeTypeControl(_ObjMainView)
                                Case ENM.Control.ControlType.BandedGridView
                                    Dim _BandedGridView As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView = .MainView
                                    With _BandedGridView

                                        .OptionsCustomization.AllowGroup = True
                                        .OptionsSelection.MultiSelect = True
                                        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
                                        .OptionsMenu.ShowGroupSummaryEditorItem = False
                                        .OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False
                                        .OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = False

                                        If .Columns.Count > 0 Then
                                            .OptionsNavigation.EnterMoveNextColumn = True
                                        End If

                                        .OptionsPrint.AutoWidth = False
                                        .OptionsView.ShowAutoFilterRow = True
                                        .BestFitColumns()

                                        Dim _TagGrid As String = ""

                                        Try
                                            _TagGrid = Microsoft.VisualBasic.Left("" & Obj.Tag.ToString.Trim & " ", 1)
                                        Catch ex As Exception
                                        End Try

                                        For Each oGridCol As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In .Columns

                                            With oGridCol
                                                .OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

                                                'If (.OptionsColumn.AllowEdit) And (.OptionsColumn.ReadOnly = False) Then
                                                '    .AppearanceCell.BackColor = Color.LightCyan
                                                '    .OptionsColumn.AllowShowHide = False

                                                'ElseIf .OptionsColumn.ReadOnly = True And .OptionsColumn.AllowEdit = False Then
                                                '    .OptionsColumn.AllowEdit = True
                                                'End If

                                                'If Not (.Visible) Then
                                                '    .OptionsColumn.AllowMove = False
                                                '    .OptionsColumn.ShowInCustomizationForm = False
                                                'Else
                                                '    .OptionsColumn.AllowMove = True
                                                '    .OptionsColumn.ShowInCustomizationForm = True
                                                'End If

                                                If (.OptionsColumn.AllowEdit) Then
                                                    .AppearanceCell.BackColor = Color.LightCyan
                                                    .OptionsColumn.AllowShowHide = False
                                                End If

                                                If Not (.Visible) Then
                                                    .OptionsColumn.AllowMove = False
                                                    .OptionsColumn.ShowInCustomizationForm = False
                                                Else
                                                    .OptionsColumn.AllowMove = True
                                                    .OptionsColumn.ShowInCustomizationForm = True
                                                End If

                                                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                                                .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                                                .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                                .BestFit()

                                                If HI.ST.SysInfo.Admin Then
                                                    .ToolTip = .Name.ToString
                                                End If

                                            End With
                                        Next

                                        For Each oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In .Bands
                                            If HI.ST.SysInfo.Admin Then
                                                oBand.ToolTip = oBand.Name.ToString

                                                If oBand.HasChildren = True Then
                                                    Call SetBandedChildToolTip(oBand)
                                                End If

                                            End If
                                        Next

                                        If (_FoundProperty) Then
                                            AddHandler .DoubleClick, AddressOf GridView_DoubleClick
                                        End If

                                    End With
                                Case ENM.Control.ControlType.AdvBandedGridView
                                    Dim _AdvBandedGridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView = .MainView
                                    With _AdvBandedGridView

                                        .OptionsCustomization.AllowGroup = True
                                        .OptionsSelection.MultiSelect = True
                                        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
                                        .OptionsMenu.ShowGroupSummaryEditorItem = False
                                        .OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False
                                        .OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = False

                                        If .Columns.Count > 0 Then
                                            .OptionsNavigation.EnterMoveNextColumn = True
                                        End If

                                        .OptionsPrint.AutoWidth = False
                                        .OptionsView.ShowAutoFilterRow = True
                                        .BestFitColumns()

                                        Dim _TagGrid As String = ""

                                        Try
                                            _TagGrid = Microsoft.VisualBasic.Left("" & Obj.Tag.ToString.Trim & " ", 1)
                                        Catch ex As Exception
                                        End Try

                                        For Each oGridCol As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In .Columns

                                            With oGridCol
                                                .OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

                                                If (.OptionsColumn.AllowEdit) Then
                                                    .AppearanceCell.BackColor = Color.LightCyan
                                                    .OptionsColumn.AllowShowHide = False
                                                End If

                                                If Not (.Visible) Then
                                                    .OptionsColumn.AllowMove = False
                                                    .OptionsColumn.ShowInCustomizationForm = False
                                                Else
                                                    .OptionsColumn.AllowMove = True
                                                    .OptionsColumn.ShowInCustomizationForm = True
                                                End If

                                                'If (.OptionsColumn.AllowEdit) And (.OptionsColumn.ReadOnly = False) Then
                                                '    .AppearanceCell.BackColor = Color.LightCyan
                                                '    .OptionsColumn.AllowShowHide = False
                                                'ElseIf .OptionsColumn.ReadOnly = True And .OptionsColumn.AllowEdit = False Then
                                                '    .OptionsColumn.AllowEdit = True
                                                'End If

                                                'If Not (.Visible) Then
                                                '    .OptionsColumn.AllowMove = False
                                                '    .OptionsColumn.ShowInCustomizationForm = False
                                                'Else
                                                '    .OptionsColumn.AllowMove = True
                                                '    .OptionsColumn.ShowInCustomizationForm = True
                                                'End If

                                                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                                                .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                                                .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                                .BestFit()

                                                If HI.ST.SysInfo.Admin Then
                                                    .ToolTip = .Name.ToString
                                                End If

                                            End With
                                        Next

                                        For Each oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In .Bands
                                            If HI.ST.SysInfo.Admin Then
                                                oBand.ToolTip = oBand.Name.ToString

                                                If oBand.HasChildren = True Then
                                                    Call SetBandedChildToolTip(oBand)
                                                End If

                                            End If
                                        Next

                                        If (_FoundProperty) Then
                                            AddHandler .DoubleClick, AddressOf GridView_DoubleClick
                                        End If

                                    End With
                                Case ENM.Control.ControlType.GridView
                                    Dim _GridView As DevExpress.XtraGrid.Views.Grid.GridView = .MainView
                                    With _GridView

                                        .OptionsCustomization.AllowGroup = True
                                        .OptionsSelection.MultiSelect = True
                                        .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
                                        .OptionsMenu.ShowGroupSummaryEditorItem = False
                                        .OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False

                                        .OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default

                                        .OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = False
                                        ' .OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.False

                                        If .Columns.Count > 0 Then
                                            .OptionsNavigation.EnterMoveNextColumn = True
                                        End If

                                        .OptionsPrint.AutoWidth = False
                                        .OptionsView.ShowAutoFilterRow = True
                                        .BestFitColumns()

                                        Dim _TagGrid As String = ""

                                        Try
                                            _TagGrid = Microsoft.VisualBasic.Left("" & Obj.Tag.ToString.Trim & " ", 1)
                                        Catch ex As Exception
                                        End Try

                                        If _TagGrid = "3" Then
                                            Call AddHandlerGridColumnEdit(_GridView)
                                        Else
                                            For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                                                With oGridCol
                                                    .OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

                                                    If Not (.ColumnEdit Is Nothing) Then
                                                        Select Case .ColumnEdit.GetType.FullName
                                                            Case (New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit).GetType.FullName
                                                                With CType(.ColumnEdit, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                                    .CharacterCasing = CharacterCasing.Upper
                                                                End With
                                                        End Select
                                                    End If

                                                    'If (.OptionsColumn.AllowEdit) And (.OptionsColumn.ReadOnly = False) Then
                                                    '    .AppearanceCell.BackColor = Color.LightCyan
                                                    '    .OptionsColumn.AllowShowHide = False
                                                    'ElseIf .OptionsColumn.ReadOnly = True And .OptionsColumn.AllowEdit = False Then
                                                    '    .OptionsColumn.AllowEdit = True
                                                    'End If

                                                    'If Not (.Visible) Then
                                                    '    .OptionsColumn.AllowMove = False
                                                    '    .OptionsColumn.ShowInCustomizationForm = False
                                                    'Else
                                                    '    .OptionsColumn.AllowMove = True
                                                    '    .OptionsColumn.ShowInCustomizationForm = True
                                                    'End If


                                                    If (.OptionsColumn.AllowEdit) Then
                                                        .AppearanceCell.BackColor = Color.LightCyan
                                                        .OptionsColumn.AllowShowHide = False
                                                    End If


                                                    If Not (.Visible) Then
                                                        .OptionsColumn.AllowMove = False
                                                        .OptionsColumn.ShowInCustomizationForm = False
                                                    Else
                                                        .OptionsColumn.AllowMove = True
                                                        .OptionsColumn.ShowInCustomizationForm = True
                                                    End If

                                                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                                                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                                    .BestFit()

                                                    If HI.ST.SysInfo.Admin Then
                                                        .ToolTip = .Name.ToString
                                                    End If

                                                End With
                                            Next
                                        End If

                                        If (_FoundProperty) Then
                                            AddHandler .DoubleClick, AddressOf GridView_DoubleClick
                                        End If

                                    End With
                            End Select

                            .ContextMenuStrip = _MContextMenuStripGrid

                        End With

                    Case ENM.Control.ControlType.XtraTabControl

                        With CType(Obj, DevExpress.XtraTab.XtraTabControl)

                            If HI.ST.SysInfo.Admin Then
                                For Each oTabPage As DevExpress.XtraTab.XtraTabPage In .TabPages
                                    oTabPage.Tooltip = oTabPage.Name.ToString
                                Next
                            End If
                        End With

                    Case ENM.Control.ControlType.PanelControl
                        With CType(Obj, DevExpress.XtraEditors.PanelControl)
                            Select Case .Name.ToString.ToUpper
                                Case "ogbmainprocbutton".ToUpper
                                    .Height = 0
                                    .Width = 0

                            End Select
                        End With
                    Case ENM.Control.ControlType.LabelControl
                        With CType(Obj, DevExpress.XtraEditors.LabelControl)
                            If HI.ST.SysInfo.Admin Then
                                .ToolTip = .Name.ToString
                            End If
                        End With
                    Case ENM.Control.ControlType.PivotGridControl
                        With CType(Obj, DevExpress.XtraPivotGrid.PivotGridControl)
                            .OptionsData.FilterByVisibleFieldsOnly = True
                            .OptionsFilterPopup.ShowOnlyAvailableItems = True

                            If HI.ST.SysInfo.Admin Then
                                For Each ObjItem As DevExpress.XtraPivotGrid.PivotGridField In .Fields
                                    Try
                                        ObjItem.ToolTips.HeaderText = ObjItem.Name.ToString
                                    Catch ex As Exception
                                    End Try
                                Next
                            End If

                            .ContextMenuStrip = _ContextMenuStripPivotGrid
                        End With
                    Case ENM.Control.ControlType.ChartControl
                        With CType(Obj, DevExpress.XtraCharts.ChartControl)
                            .ContextMenuStrip = _ContextMenuStripChart
                        End With
                    Case ENM.Control.ControlType.GridLookUpEdit
                        With CType(Obj, DevExpress.XtraEditors.GridLookUpEdit)
                            If .Properties.Buttons.Count > 1 Then
                                If .Properties.Buttons(1).Kind = ButtonPredefines.Clear Then
                                    AddHandler .ButtonClick, AddressOf GridLookUpEdit_ButtonClick
                                End If
                            End If
                        End With
                End Select

                If Obj.Controls.count > 0 Then
                    Call AddHandlerObj(Obj)
                End If

            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub SetBandedChildToolTip(oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand)
        Try

            For Each oBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand In oBand.Children
                oBand2.ToolTip = oBand2.Name.ToString

                If oBand2.Children.Count > 0 Then
                    Call SetBandedChildToolTip(oBand2)
                End If
            Next

        Catch ex As Exception
        End Try

    End Sub

#Region "ToolTip"

    Public Shared Sub CreateManuStripPictureEdit()
        _ContextMenuStripPicture = New System.Windows.Forms.ContextMenuStrip
        Dim _AddPicture As New System.Windows.Forms.ToolStripMenuItem
        Dim _ClearPicture As New System.Windows.Forms.ToolStripMenuItem

        With _AddPicture
            .Name = "ocmAddPicture"
            .Text = "Add Picture"
            AddHandler .Click, AddressOf PictureAdd_Click
        End With

        With _ClearPicture
            .Name = "ocmClearPicture"
            .Text = "Clear Picture"
            AddHandler .Click, AddressOf PictureClear_Click
        End With

        With _ContextMenuStripPicture
            .Name = "ContextMenuPicture"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_AddPicture, _ClearPicture})
        End With

    End Sub

    Public Shared Sub CreateManuStripGrid()

        _MContextMenuStripGrid = New System.Windows.Forms.ContextMenuStrip
        Dim _ExportToExcel As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToExcelData As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToCsv As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToText As New System.Windows.Forms.ToolStripMenuItem

        With _ExportToExcel
            .Name = "ocmExportToExcel"
            .Text = "Export To Excel"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf ExportToExcel_Click
        End With


        With _ExportToExcelData
            .Name = "ocmExportToExcelData"
            .Text = "Export To Excel (Data)"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf ExportToExcelData_Click
        End With

        With _ExportToCsv
            .Name = "ocmExportToCsv"
            .Text = "Export To CSV"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToCSV.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ExportToCSV_Click
        End With

        With _ExportToPDF
            .Name = "ocmExportToPDF"
            .Text = "Export To PDF"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToPDF.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ExportToPDF_Click
        End With

        With _ExportToText
            .Name = "ocmExportToText"
            .Text = "Export To Text"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToText.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ExportToText_Click
        End With

        With _MContextMenuStripGrid
            .Name = "ContextExportDataGridControl"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ExportToExcel, _ExportToExcelData, _ExportToCsv, _ExportToPDF, _ExportToText})
        End With

        Call CreateManuStripChart()
        Call CreateManuStripPivotGrid()

    End Sub

    Public Shared Sub CreateManuStripChart()
        _ContextMenuStripChart = New System.Windows.Forms.ContextMenuStrip
        Dim _ExportToImage As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToExcel As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToHtml As New System.Windows.Forms.ToolStripMenuItem

        With _ExportToExcel
            .Name = "ocmChartExportToExcel"
            .Text = "Export To Excel"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf ChartExportToExcel_Click
        End With

        With _ExportToPDF
            .Name = "ocmChartExportToPDF"
            .Text = "Export To PDF"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToPDF.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ChartExportToPDF_Click
        End With

        With _ExportToImage
            .Name = "ocmChartExportToImage"
            .Text = "Export To Image"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToImage.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ChartExportToImage_Click
        End With

        With _ExportToHtml
            .Name = "ocmChartExportToHtml"
            .Text = "Export To Html"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToHtml.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ChartExportToHtml_Click
        End With

        With _ContextMenuStripChart
            .Name = "ContextExportPivotGrid"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ExportToImage, _ExportToHtml, _ExportToExcel, _ExportToPDF})
        End With
    End Sub

    Public Shared Sub CreateManuStripPivotGrid()
        _ContextMenuStripPivotGrid = New System.Windows.Forms.ContextMenuStrip
        Dim _ExportToImage As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToExcel As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToExcelData As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToCsv As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToHtml As New System.Windows.Forms.ToolStripMenuItem

        With _ExportToExcel
            .Name = "ocmPivotGridExportToExcel"
            .Text = "Export To Excel"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf PivotGridExportToExcel_Click
        End With

        With _ExportToExcelData
            .Name = "ocmPivotGridExportToExcelData"
            .Text = "Export To Excel (Data)"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf PivotGridExportToExcelData_Click
        End With

        With _ExportToCsv
            .Name = "ocmPivotGridExportToCsv"
            .Text = "Export To CSV"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToCSV.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf PivotGridExportToCSV_Click
        End With

        With _ExportToPDF
            .Name = "ocmPivotGridExportToPDF"
            .Text = "Export To PDF"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToPDF.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf PivotGridExportToPDF_Click
        End With

        With _ExportToImage
            .Name = "ocmPivotGridExportToImage"
            .Text = "Export To Image"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToImage.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf PivotGridExportToImage_Click
        End With

        With _ExportToHtml
            .Name = "ocmPivotGridExportToHtml"
            .Text = "Export To Html"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToHtml.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf PivotGridExportToHtml_Click
        End With

        With _ContextMenuStripPivotGrid
            .Name = "ContextExportPivotGrid"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ExportToImage, _ExportToHtml, _ExportToExcel, _ExportToExcelData, _ExportToCsv, _ExportToPDF})
        End With
    End Sub

#End Region

#Region "Export From Grid"
    Public Shared Sub ExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)

                        'Dim _XlsxExportOption As New DevExpress.XtraPrinting.XlsxExportOptions()
                        '_XlsxExportOption.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text

                        '.ExportToXlsx(Op.FileName, _XlsxExportOption)


                        ExportStart = True
                        Dim Clearmemory As New Threading.Thread(AddressOf ClearmemoryStart)
                        Clearmemory.IsBackground = True
                        Clearmemory.Start()

                        Try
                            .ExportToXlsx(Op.FileName)
                        Catch ex As Exception
                        End Try

                        Clearmemory.Abort()

                        ExportStart = False

                        '.ExportToXlsx(Op.FileName)

                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try

                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub


    Public Shared Sub ExportToExcelData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.DataAware

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)

                        'Dim _XlsxExportOption As New DevExpress.XtraPrinting.XlsxExportOptions()
                        '_XlsxExportOption.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text

                        '.ExportToXlsx(Op.FileName, _XlsxExportOption)


                        ExportStart = True
                        Dim Clearmemory As New Threading.Thread(AddressOf ClearmemoryStart)
                        Clearmemory.IsBackground = True
                        Clearmemory.Start()

                        Try
                            .ExportToXlsx(Op.FileName)
                        Catch ex As Exception
                        End Try

                        Clearmemory.Abort()

                        ExportStart = False

                        '.ExportToXlsx(Op.FileName)

                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try

                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ClearmemoryStart()

        While (ExportStart = True)

            GC.WaitForPendingFinalizers()
            GC.RemoveMemoryPressure(GC.GetTotalMemory(True))
            EmptyWorkingSet(Process.GetCurrentProcess.Handle)

            SetProcessWorkingSetSize(Process.GetCurrentProcess.Handle, -1, -1)

            System.Threading.Thread.Sleep(1000)

        End While

    End Sub

    Public Shared Sub ExportToCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "CSV Files|*.csv"
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)
                        .ExportToCsv(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ExportToPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "PDF Files(*.pdf)|*.pdf"

            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    Dim StateLandscape As Boolean = False

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)
                        '.ExportToPdf(Op.FileName)
                        'Try
                        '    Process.Start(Op.FileName)
                        'Catch ex As Exception

                        'End Try

                        Dim _ObjMainView As Object = .MainView

                        Select Case HI.ENM.Control.GeTypeControl(_ObjMainView)
                            Case ENM.Control.ControlType.BandedGridView

                                With CType(_ObjMainView, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)
                                    StateLandscape = (.VisibleColumns.Count > 8)
                                End With


                            Case ENM.Control.ControlType.AdvBandedGridView

                                With CType(_ObjMainView, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView)
                                    StateLandscape = (.VisibleColumns.Count > 8)
                                End With

                            Case ENM.Control.ControlType.GridView

                                Dim _GridView As DevExpress.XtraGrid.Views.Grid.GridView = .MainView

                                With CType(_ObjMainView, DevExpress.XtraGrid.Views.Grid.GridView)
                                    StateLandscape = (.VisibleColumns.Count > 8)
                                End With
                        End Select

                    End With




                    ExportStart = True
                    Dim Clearmemory As New Threading.Thread(AddressOf ClearmemoryStart)
                    Clearmemory.IsBackground = True
                    Clearmemory.Start()

                    Try

                        Dim ps = New DevExpress.XtraPrinting.PrintingSystem()
                        Dim link As New DevExpress.XtraPrinting.PrintableComponentLink(ps)
                        ps.Links.Add(link)
                        link.Component = CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)
                        link.Landscape = StateLandscape
                        link.CreateDocument(False)
                        link.PrintingSystem.PageSettings.TopMargin = 40
                        link.PrintingSystem.PageSettings.LeftMargin = 20
                        link.PrintingSystem.PageSettings.RightMargin = 20
                        link.PrintingSystem.PageSettings.BottomMargin = 20
                        link.PrintingSystem.Document.AutoFitToPagesWidth = 1
                        link.ExportToPdf(Op.FileName)
                    Catch ex As Exception
                    End Try

                    Clearmemory.Abort()

                    ExportStart = False




                    Try
                        Process.Start(Op.FileName)
                    Catch ex As Exception
                    End Try

                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub


    'Public Shared Sub ogridview_PrintInitialize(sender As Object, e As DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs)
    '    Try
    '        Dim pb As DevExpress.XtraPrinting.PrintingSystemBase = CType(e.PrintingSystem, DevExpress.XtraPrinting.PrintingSystemBase)
    '        pb.PageSettings.Landscape = True
    '        pb.PageSettings.TopMargin = 5
    '        pb.PageSettings.LeftMargin = 5
    '        pb.PageSettings.RightMargin = 5
    '        pb.PageSettings.BottomMargin = 5
    '        pb.PrintingDocument.AutoFitToPagesWidth = 1

    '    Catch ex As Exception

    '    End Try

    'End Sub

    Public Shared Sub ExportToText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Text Files(*.txt)|*.txt"

            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)
                        .ExportToText(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Try
                Dim _Form As Object

                With CType(CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Form = .GridControl.FindForm

                End With

                HI.UL.AppRegistry.SaveLayoutGridToRegistry(_Form, CType(sender, DevExpress.XtraGrid.Views.Grid.GridView))
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ClearLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Try
                Dim _Form As Object

                With CType(CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Form = .GridControl.FindForm

                End With

                HI.UL.AppRegistry.DeleteLayoutGridToRegistry(_Form, CType(sender, DevExpress.XtraGrid.Views.Grid.GridView))
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Export From Pivot Grid"

    Public Shared Sub PivotGridExportToImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Jpg Image|*.jpg|Bmp Image|*.bmp|Png Image|*.png"

            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)
                        .ExportToImage(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub PivotGridExportToHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Html Files(.html)|*.html"

            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)
                        .ExportToHtml(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub PivotGridExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)

                        '.BeginUpdate()
                        '.ExportToXlsx(Op.FileName)



                        ExportStart = True
                        Dim Clearmemory As New Threading.Thread(AddressOf ClearmemoryStart)
                        Clearmemory.IsBackground = True
                        Clearmemory.Start()

                        Try
                            .ExportToXlsx(Op.FileName)
                        Catch ex As Exception
                        End Try

                        Clearmemory.Abort()

                        ExportStart = False


                        ' .EndUpdate()

                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub PivotGridExportToExcelData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.DataAware

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)

                        '.BeginUpdate()
                        '.ExportToXlsx(Op.FileName)



                        ExportStart = True
                        Dim Clearmemory As New Threading.Thread(AddressOf ClearmemoryStart)
                        Clearmemory.IsBackground = True
                        Clearmemory.Start()

                        Try
                            .ExportToXlsx(Op.FileName)
                        Catch ex As Exception
                        End Try

                        Clearmemory.Abort()

                        ExportStart = False


                        ' .EndUpdate()

                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub PivotGridExportToCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "CSV Files|*.csv"
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)
                        .ExportToCsv(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub PivotGridExportToPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "PDF Files(*.pdf)|*.pdf"

            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    'With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)
                    '    .ExportToPdf(Op.FileName)
                    '    Try
                    '        Process.Start(Op.FileName)
                    '    Catch ex As Exception
                    '    End Try
                    'End With


                    Dim ps = New DevExpress.XtraPrinting.PrintingSystem()
                    Dim link As New DevExpress.XtraPrinting.PrintableComponentLink(ps)
                    ps.Links.Add(link)
                    link.Component = CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraPivotGrid.PivotGridControl)
                    link.Landscape = True
                    link.CreateDocument(False)
                    link.PrintingSystem.PageSettings.TopMargin = 40
                    link.PrintingSystem.PageSettings.LeftMargin = 20
                    link.PrintingSystem.PageSettings.RightMargin = 20
                    link.PrintingSystem.PageSettings.BottomMargin = 20
                    link.PrintingSystem.Document.AutoFitToPagesWidth = 1
                    link.ExportToPdf(Op.FileName)

                    Try
                        Process.Start(Op.FileName)
                    Catch ex As Exception
                    End Try

                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub


#End Region

#Region "Export From Chart"

    Public Shared Sub ChartExportToImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Png Image(.png)|*.png"

            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraCharts.ChartControl)
                        .ExportToImage(Op.FileName, System.Drawing.Imaging.ImageFormat.Png)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ChartExportToHtml_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Html Files(.html)|*.html"

            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraCharts.ChartControl)
                        .ExportToHtml(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ChartExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Files(.xlsx)|*.xlsx"
            '         "Excel Files(.xls)|*.xls| 
            'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then

                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraCharts.ChartControl)
                       

                        .ExportToXlsx(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ChartExportToPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "PDF Files(*.pdf)|*.pdf"

            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraCharts.ChartControl)
                        .ExportToPdf(Op.FileName)
                        Try
                            Process.Start(Op.FileName)
                        Catch ex As Exception
                        End Try
                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub


#End Region

    Public Shared Sub DisableGridViewSortColumn(ByVal ObjGridView As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            With ObjGridView
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        .OptionsColumn.AllowSort = False
                    End With
                Next
            End With
        Catch ex As Exception

        End Try

    End Sub
    Public Shared Sub AddHandlerGridColumnEdit(ByVal ObjGrid As DevExpress.XtraGrid.Views.Grid.GridView)
        Try
            With ObjGrid

                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                    With GridCol
                        .OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True
                        .BestFit()

                        'If HI.ST.SysInfo.Admin Then
                        '    .ToolTip = .Name.ToString
                        'End If

                        'If (.OptionsColumn.AllowEdit) And (.OptionsColumn.ReadOnly = False) Then
                        '    .AppearanceCell.BackColor = Color.LightCyan
                        '    .OptionsColumn.AllowShowHide = False
                        'ElseIf .OptionsColumn.ReadOnly = True And .OptionsColumn.AllowEdit = False Then
                        '    .OptionsColumn.AllowEdit = True
                        'End If

                        'If Not (.Visible) Then
                        '    .OptionsColumn.AllowMove = False
                        '    .OptionsColumn.ShowInCustomizationForm = False
                        'Else
                        '    .OptionsColumn.AllowMove = True
                        '    .OptionsColumn.ShowInCustomizationForm = True
                        'End If

                        If HI.ST.SysInfo.Admin Then
                            .ToolTip = .Name.ToString
                        End If

                        If (.OptionsColumn.AllowEdit) Then
                            .AppearanceCell.BackColor = Color.LightCyan
                            .OptionsColumn.AllowShowHide = False
                        End If

                        If Not (.Visible) Then
                            .OptionsColumn.AllowMove = False
                            .OptionsColumn.ShowInCustomizationForm = False
                        Else
                            .OptionsColumn.AllowMove = True
                            .OptionsColumn.ShowInCustomizationForm = True
                        End If

                        If Not (.ColumnEdit) Is Nothing Then
                            Dim Obj As Object = .ColumnEdit
                            Select Case HI.ENM.Control.GeTypeControl(Obj)
                                Case ENM.Control.ControlType.RepositoryItemTextEdit

                                    With CType(Obj, DevExpress.XtraEditors.Repository.RepositoryItemTextEdit)

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                    End With

                                Case ENM.Control.ControlType.RepositoryItemMemoEdit
                                    With CType(Obj, DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit)
                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow

                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue

                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                    End With
                                Case ENM.Control.ControlType.RepositoryItemButtonEdit
                                    With CType(Obj, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)

                                        If .Buttons.Item(0).Visible Then
                                            AddHandler .KeyDown, AddressOf ButtonEdit_KeyDown
                                        End If

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue
                                        .CharacterCasing = CharacterCasing.Upper

                                        If .Buttons.Count > 0 Then


                                            If "" & .Buttons(0).Tag.ToString() <> "" Then
                                                AddHandler .KeyDown, AddressOf DynamicButtonedit_KeyDown
                                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHide_ButtonClick
                                                AddHandler .EditValueChanged, AddressOf DynamicResponButtoneditSysHide_EditValueChanged
                                                AddHandler .Leave, AddressOf DynamicResponButtoneditSysHide_Leave
                                                AddHandler .Click, AddressOf DynamicResponButtone_Gotocus
                                            End If

                                        End If

                                    End With
                                Case ENM.Control.ControlType.RepositoryItemCalcEdit

                                    With CType(Obj, DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit)

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow

                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue

                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                        AddHandler .Leave, AddressOf CalEdit_Leave
                                        AddHandler .Spin, AddressOf Caledit_Spin

                                    End With

                                Case ENM.Control.ControlType.RepositoryItemComboBox

                                    With CType(Obj, DevExpress.XtraEditors.Repository.RepositoryItemComboBox)

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow

                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue

                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                        Try
                                            If "" & .Tag.ToString <> "" Then
                                                .Items.Clear()
                                                .Items.AddRange(HI.TL.CboList.SetList("" & .Tag.ToString))
                                            End If
                                        Catch ex As Exception
                                        End Try

                                        AddHandler .SelectedIndexChanged, AddressOf RepositoryComboBoxSysHide_SelectedIndexChanged

                                    End With

                                Case ENM.Control.ControlType.RepositoryItemDateEdit

                                    With CType(Obj, DevExpress.XtraEditors.Repository.RepositoryItemDateEdit)

                                        .AppearanceFocused.ForeColor = Color.Blue
                                        .AppearanceFocused.BackColor = Color.GreenYellow
                                        .AppearanceReadOnly.BackColor = Color.LightCyan
                                        .AppearanceReadOnly.ForeColor = Color.Blue
                                        .AppearanceDisabled.BackColor = Color.LightCyan
                                        .AppearanceDisabled.ForeColor = Color.Blue

                                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                        .DisplayFormat.FormatString = "d"

                                        .EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                                        .EditFormat.FormatString = "d"

                                        .EditMask = "d"
                                        .DisplayFormat.FormatString = ("d")

                                        AddHandler .Leave, AddressOf RepositoryItemDate_Leave
                                        AddHandler .Click, AddressOf RepositoryItemDate_GotFocus

                                    End With

                            End Select

                        End If
                    End With
                Next
            End With

        Catch ex As Exception

        End Try
    End Sub

#Region " Button Edit KeyPress "
    Public Shared Sub DateEdit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.DateEdit)
            If .Text <> "" OrElse HI.UL.ULDate.CheckDate(.DateTime.ToString) <> "" Then
                Try
                    If .Text <> "" Then

                        Dim _QryDate As String = HI.UL.ULDate.ConvertEnDB(.DateTime.ToString)
                        .DateTime = HI.UL.ULDate.ConvertEnDB(_QryDate)
                    Else
                        Dim _QryDate As String = HI.UL.ULDate.ConvertEnDB(.DateTime.ToString)
                        If _QryDate <= "1800/01/01" Then
                            .Text = ""
                        Else
                            .DateTime = HI.UL.ULDate.ConvertEnDB(_QryDate)
                        End If
                    End If
                Catch ex As Exception
                    .Text = ""
                End Try
            End If
        End With
    End Sub

#End Region

#Region "Date Edit"

#End Region

#Region "Calculate Edit"

    Public Shared Sub CalEdit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.CalcEdit)
            .Value = Format(.Value, "0" & IIf(.Properties.Precision > 0, ".".PadRight(.Properties.Precision + 1, "0"), ""))
        End With
    End Sub

    Public Shared Sub CalEditExchangeRate_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.CalcEdit)
            .Value = Format(IIf(.Value <= 0, 1, .Value), "0" & IIf(.Properties.Precision > 0, ".".PadRight(.Properties.Precision + 1, "0"), ""))
        End With
    End Sub

#End Region

#Region "Checkbox Edit"
    Private Shared Sub Checkbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _State As Boolean = False
        If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
            _State = True
        Else
            _State = False
        End If

    End Sub
#End Region

#Region "Gridview Respon Control"

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
                'HI.Conn.oDate.Convert_Date_En_To_DB(CType(sender, DevExpress.XtraEditors.DateEdit).Text)
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With

        Catch ex As Exception
            HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub RepositoryCheckEdit_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Public Shared Sub RepositoryItemCalcEdit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Static _Proc As Boolean

            If Not (_Proc) Then
                _Proc = True

                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    With CType(sender, DevExpress.XtraEditors.CalcEdit)
                        .Value = Format(.Value, "0" & IIf(.Properties.Precision > 0, ".".PadRight(.Properties.Precision + 1, "0"), ""))
                    End With

                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.Name.ToString, CType(sender, DevExpress.XtraEditors.CalcEdit).Value)
                End With

                _Proc = False
            End If

        Catch ex As Exception
            HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub RepositoryItemCalcEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Static _Proc As Boolean

            If Not (_Proc) Then
                _Proc = True

                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                End With

                _Proc = False
            End If

        Catch ex As Exception
            HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Shared Sub RepositoryItemCheckEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.Name.ToString, IIf(CType(sender, DevExpress.XtraEditors.CheckEdit).Checked, 1, 0))

            End With
        Catch ex As Exception
            HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Sub RepositoryItemCheckEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.Name.ToString, IIf(CType(sender, DevExpress.XtraEditors.CheckEdit).Checked, 1, 0))
            End With
        Catch ex As Exception
            HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Shared Sub RepositoryButtonEdit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then
                If ("" & .Properties.Buttons.Item(0).Tag).ToString = "" Then
                    ' HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm.Text, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).FocusedColumn.Caption)
                    .Text = ""
                    ': If .Enabled Then .Focus()
                End If
            End If
        End With
    End Sub

    Public Shared Sub RepositoryButtonEdit_Gotocus(ByVal sender As Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            .Properties.Buttons.Item(0).Tag = .Text
        End With

    End Sub

#End Region

#End Region

#Region "TextEdit"
    Public Shared Sub Textedit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With DirectCast(sender, DevExpress.XtraEditors.TextEdit)
            .Text = .Text.Trim()
        End With
    End Sub

#End Region

#Region "Caledit"
    Public Shared Sub Caledit_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs)
        e.Handled = True
    End Sub
#End Region

#Region "Picture Edit Handler Control"

    Public Shared Sub PictureAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.OpenFileDialog
            Op.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraEditors.PictureEdit)

                        Try
                            .Image = Nothing
                            'Dim _Image As Image
                            '_Image = System.Drawing.Image.FromStream(New System.IO.FileStream(Op.FileName, IO.FileMode.Open), True)

                            .Image = HI.UL.ULImage.LoadImage(Op.FileName) ' hImage ' Image.FromFile(Op.FileName)

                        Catch ex As Exception ': MsgBox(ex.Message)
                            .Image = Nothing
                        End Try
                        ' .SuperTip.Items(0).Appearance.Image = .Image

                    End With
                End If
            Catch ex As Exception ': MsgBox(ex.Message)
            End Try
        Catch ex As Exception ': MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub PictureClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraEditors.PictureEdit)
            Try
                .Image = Nothing
            Catch ex As Exception ': MsgBox(ex.Message)
                .Image = Nothing
            End Try
            ' .SuperTip.Items(0).Appearance.Image = .Image
        End With
    End Sub

    Public Shared Sub PictureClear_ImageChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.PictureEdit)
            Try

                Dim _SuperToolTip As New DevExpress.Utils.SuperToolTip()
                Dim _ToolTipTitleItem As New DevExpress.Utils.ToolTipTitleItem()

                _ToolTipTitleItem.Appearance.Image = .Image
                _ToolTipTitleItem.Appearance.Options.UseImage = True
                _ToolTipTitleItem.Image = .Image
                _ToolTipTitleItem.Text = ""

                With _SuperToolTip
                    .Items.Add(_ToolTipTitleItem)
                End With

                .SuperTip = _SuperToolTip

                ' .SuperTip.Items(0).Appearance.Image = .Image

            Catch ex As Exception
            End Try
        End With
    End Sub

#End Region

#Region "Button Edit Dynamic Handler"


    Public Shared Sub DynamicButtonedit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F7

                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                    If .Properties.ReadOnly Then
                        Exit Sub
                    End If

                    If .Properties.Buttons.Count >= 1 Then
                        If (.Properties.Buttons.Item(0).Visible) Then
                            .PerformClick(.Properties.Buttons.Item(0))
                        End If
                    End If

                End With

            Case Keys.Enter

                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    For Each ctrl As Object In _form.Controls.Find(sender.Name.ToString & "_Browse", True)
                        With CType(ctrl, HI.UCTR.HButtonDropDown)
                            If (.Visible) Then
                                Dim data As String = .FocusRowData
                                If data <> "" Then
                                    CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = data
                                End If
                            End If
                        End With
                    Next

                    If Not (.Properties.ReadOnly) AndAlso .Properties.Buttons.Count >= 1 AndAlso (.Properties.Buttons.Item(0).Visible) Then
                        Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(sender, New System.EventArgs)
                    End If

                End With
            Case Keys.F1
                With CType(sender, DevExpress.XtraEditors.ButtonEdit)
                    If .Properties.ReadOnly Then
                        Exit Sub
                    End If

                    If .InvokeRequired Then
                        .Invoke(New HI.Delegate.Dele.ButtonEdit_KeyPress(AddressOf DynamicButtonEdit_KeyPress), New Object() {sender, e})
                    Else
                        Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                        For Each ctrl As Object In _form.Controls.Find(sender.Name.ToString & "_Browse", True)
                            With CType(ctrl, HI.UCTR.HButtonDropDown)
                                If Not (.Visible) Then
                                    Dim Ly As Integer = 3

                                    If _form.ActiveControl.GetType.FullName.ToString.ToUpper = "DevExpress.XtraBars.Docking.DockPanel".ToUpper Then
                                        Ly = 30
                                        If Not (_form.ActiveControl.Visible) Then
                                            Exit Sub
                                        End If
                                    End If

                                    Dim Obj As Object = .ParentControl
                                    Dim ActiveName As String = Obj.Name

                                    Try
                                        If Not (_form.ActiveControl.Parent.Parent Is Nothing) Then
                                            Dim Lox As Integer = 0
                                            Dim Loy As Integer = 0
                                            Dim Found As Boolean = True
                                            Dim Obj2 As Object = Obj

                                            Do
                                                If Not (Obj2.Parent Is Nothing) Then

                                                    If (Obj2.Parent.GetType.FullName.ToString.ToUpper = "System.Windows.Forms.MdiClient".ToUpper) Then
                                                        Found = False
                                                    Else
                                                        Lox = Lox + Obj2.Location.X
                                                        Loy = Loy + Obj2.Location.Y

                                                        Obj2 = Obj2.Parent

                                                        If (Obj2.Parent.GetType.FullName.ToString.ToUpper = "System.Windows.Forms.MdiClient".ToUpper) Then
                                                            Found = False
                                                        End If
                                                    End If

                                                Else
                                                    Found = False
                                                End If

                                            Loop Until (Found = False)

                                            ' .Parent = _form.ActiveControl.Parent.Parent
                                            '.PointToScreen(New Point(Obj.Location.X, Obj.Location.Y + ((Obj).Height + 3)))
                                            '.Location = New System.Drawing.Point(Obj.Location.X, Obj.Location.Y + ((Obj).Height + 3))
                                            .Location = New System.Drawing.Point(Lox, Loy + ((Obj).Height + 3))

                                            If .Data Is Nothing Then
                                                .LoadBrowsd(CType(sender, DevExpress.XtraEditors.ButtonEdit).Properties.Buttons(0).Tag.ToString)
                                            End If
                                            .CreateFilter(CType(sender, DevExpress.XtraEditors.ButtonEdit).Text)
                                            ' .Parent = _form
                                            .BringToFront()
                                            .Visible = True
                                            .Parent = _form

                                        End If

                                    Catch ex As Exception

                                    End Try

                                End If
                            End With
                        Next

                    End If
                End With
            Case Keys.Escape
                Try
                    Dim _form2 As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    For Each ctrl As Object In _form2.Controls.Find(sender.Name.ToString & "_Browse", True)
                        With CType(ctrl, HI.UCTR.HButtonDropDown)
                            If _form2.ActiveControl.Name.ToString <> sender.Name.ToString & "_Browse" Then
                                If (.Visible) Then
                                    .Visible = False
                                End If
                                .DisposeObject()
                            End If

                        End With
                    Next
                Catch ex As Exception
                End Try
        End Select
    End Sub

    Private Shared Sub DynamicButtonEdit_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        'If e.KeyChar = Chr(13) Or e.KeyChar = Chr(118) Then

        'Else
        '    With CType(sender, DevExpress.XtraEditors.ButtonEdit)
        '        If .InvokeRequired Then
        '            .Invoke(New HI.Delegate.Dele.ButtonEdit_KeyPress(AddressOf DynamicButtonEdit_KeyPress), New Object() {sender, e})
        '        Else
        '            Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
        '            For Each ctrl As Object In _form.Controls.Find(sender.Name.ToString & "_Browse", True)
        '                With CType(ctrl, HI.UCTR.HButtonDropDown)
        '                    If Not (.Visible) Then
        '                        Dim Ly As Integer = 3

        '                        If _form.ActiveControl.GetType.FullName.ToString.ToUpper = "DevExpress.XtraBars.Docking.DockPanel".ToUpper Then
        '                            Ly = 30
        '                            If Not (_form.ActiveControl.Visible) Then
        '                                Exit Sub
        '                            End If
        '                        End If

        '                        Dim Obj As Object = .ParentControl
        '                        Dim ActiveName As String = Obj.Name

        '                        Try
        '                            If Not (_form.ActiveControl.Parent.Parent Is Nothing) Then
        '                                Dim Lox As Integer = 0
        '                                Dim Loy As Integer = 0
        '                                Dim Found As Boolean = True
        '                                Dim Obj2 As Object = Obj

        '                                Do
        '                                    If Not (Obj2.Parent Is Nothing) Then


        '                                        If (Obj2.Parent.GetType.FullName.ToString.ToUpper = "System.Windows.Forms.MdiClient".ToUpper) Then
        '                                            Found = False
        '                                        Else
        '                                            Lox = Lox + Obj2.Location.X
        '                                            Loy = Loy + Obj2.Location.Y

        '                                            Obj2 = Obj2.Parent

        '                                            If (Obj2.Parent.GetType.FullName.ToString.ToUpper = "System.Windows.Forms.MdiClient".ToUpper) Then
        '                                                Found = False
        '                                            End If
        '                                        End If

        '                                    Else
        '                                        Found = False
        '                                    End If

        '                                Loop Until (Found = False)

        '                                ' .Parent = _form.ActiveControl.Parent.Parent
        '                                '.PointToScreen(New Point(Obj.Location.X, Obj.Location.Y + ((Obj).Height + 3)))
        '                                '.Location = New System.Drawing.Point(Obj.Location.X, Obj.Location.Y + ((Obj).Height + 3))
        '                                .Location = New System.Drawing.Point(Lox, Loy + ((Obj).Height + 3))
        '                                .LoadBrowsd(CType(sender, DevExpress.XtraEditors.ButtonEdit).Properties.Buttons(0).Tag.ToString)
        '                                ' .Parent = _form
        '                                .BringToFront()
        '                                .Visible = True
        '                                .Parent = _form

        '                            End If

        '                        Catch ex As Exception

        '                        End Try

        '                    End If
        '                End With
        '            Next

        '        End If
        '    End With
        'End If

    End Sub


    Public Shared Sub ButtonEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                If .InvokeRequired Then
                    .Invoke(New HI.Delegate.Dele.DynamicButtonedit_ValueChanged(AddressOf ButtonEdit_EditValueChanging), New Object() {sender, e})
                Else
                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    For Each ctrl As Object In _form.Controls.Find(sender.Name.ToString & "_Browse", True)
                        With CType(ctrl, HI.UCTR.HButtonDropDown)
                            If (.Visible) Then
                                .CreateFilter(e.NewValue.ToString)
                            End If
                        End With
                    Next
                End If

            End With

        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub ButtonEdit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    Private Shared Sub ButtonEdit_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Properties.ReadOnly Then
                Exit Sub
            End If
            If .InvokeRequired Then
                .Invoke(New HI.Delegate.Dele.ButtonEdit_Spin(AddressOf ButtonEdit_Spin), New Object() {sender, e})
            Else
                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                For Each ctrl As Object In _form.Controls.Find(sender.Name.ToString & "_Browse", True)
                    With CType(ctrl, HI.UCTR.HButtonDropDown)
                        If (.Visible) Then

                            If e.IsSpinUp Then
                                .FocusRowUp()
                            Else
                                .FocusRowDown()
                            End If
                        Else
                            .ClearFilter()
                            ' If CType(sender, DevExpress.XtraEditors.ButtonEdit).Text <> "" And CType(sender, DevExpress.XtraEditors.ButtonEdit).Properties.Tag.ToString <> "" Then
                            If CType(ctrl, HI.UCTR.HButtonDropDown).Data Is Nothing Then
                                .LoadBrowsd(CType(sender, DevExpress.XtraEditors.ButtonEdit).Properties.Buttons(0).Tag.ToString)

                                .SetFocusRowData(CType(sender, DevExpress.XtraEditors.ButtonEdit).Text)
                            Else
                                If CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = "" Then
                                    .SetFocusRowData(CType(sender, DevExpress.XtraEditors.ButtonEdit).Text)
                                End If
                            End If

                            If CType(sender, DevExpress.XtraEditors.ButtonEdit).Text <> "" Then
                                If e.IsSpinUp Then
                                    .FocusRowUp()
                                Else
                                    .FocusRowDown()
                                End If
                            End If

                            Dim data As String = .FocusRowData
                            If data <> "" Then
                                CType(sender, DevExpress.XtraEditors.ButtonEdit).Text = data
                            End If
                            ' End If
                        End If
                    End With
                Next
            End If
        End With
    End Sub

    Public Shared Sub DynamicButtone_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try
            Try
                Dim _form2 As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                For Each ctrl As Object In _form2.Controls.Find(sender.Name.ToString & "_Browse", True)
                    With CType(ctrl, HI.UCTR.HButtonDropDown)
                        If _form2.ActiveControl.Name.ToString <> sender.Name.ToString & "_Browse" Then
                            If (.Visible) Then
                                .Visible = False
                            End If
                            .DisposeObject()
                        End If

                    End With
                Next
            Catch ex As Exception
            End Try
            Dim brwsedataid As Integer = 0

            Select Case e.Button.Index
                Case 0, 2

                    Dim bttagobj As New HI.TL.ButtonEditTag
                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm

                    Try
                        If TypeOf e.Button.Tag Is HI.TL.ButtonEditTag Then

                            bttagobj = CType(e.Button.Tag, HI.TL.ButtonEditTag)
                            brwsedataid = bttagobj.BrowObjectID
                        Else
                            brwsedataid = Val(e.Button.Tag.ToString)

                            bttagobj = New HI.TL.ButtonEditTag()
                            bttagobj.Value = ""
                            bttagobj.BrowObjectID = brwsedataid
                            bttagobj.BrowseInfo = New wDynamicBrowseInfo(brwsedataid, _form)
                            bttagobj.LoadData = False

                            e.Button.Tag = bttagobj

                        End If

                    Catch ex As Exception
                    End Try

                    If brwsedataid <= 0 Then Exit Sub

                    Dim _Qrysql As String
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable

                    Dim _BrowseCmd As String = ""
                    Dim _BrowseSortCmd As String = ""
                    Dim _BrowseWhereCmd As String = ""
                    Dim _FTBrwCmdField As String = ""
                    Dim _FTBrwCmdFieldOptional As String = ""
                    Dim FTBrwCmdGroupBy As String = ""
                    Dim _FTStringFormatWhare As String = ""

                    Dim brwinfo As wDynamicBrowseInfo = bttagobj.BrowseInfo

                    If bttagobj.LoadData = False Then

                        _Qrysql = " SELECT  TOP 1    BrwID "
                        _Qrysql &= vbCrLf & " ,FTBrwCmdTH  "
                        _Qrysql &= vbCrLf & " ,FTBrwCmdEN  "
                        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy,FTStringFormatWhare,FNLenghtMinSearch,FNLenghtMaxSearch "
                        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
                        _Qrysql &= vbCrLf & " WHERE BrwID=" & brwsedataid & " "

                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        For Each Row As DataRow In _dtbrw.Rows
                            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

                            _dtret = New DataTable
                            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)


                            bttagobj.BrwRetID = Val(Row!BrwRetID.ToString)
                            bttagobj.CmdSelectTH = Row!FTBrwCmdTH.ToString
                            bttagobj.CmdSelectEN = Row!FTBrwCmdEN.ToString

                            bttagobj.CmdSortBy = Row!FTBrwCmdSort.ToString
                            bttagobj.CmdWhere = Row!FTBrwCmdWhere.ToString

                            bttagobj.MinLenght = Val(Row!FNLenghtMinSearch.ToString)
                            bttagobj.MaxLenght = Val(Row!FNLenghtMaxSearch.ToString)


                            bttagobj.CmdBrowseField = Row!FTBrwCmdField.ToString
                            bttagobj.CmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString
                            bttagobj.CmdStringFormatWhare = Row!FTStringFormatWhare.ToString
                            bttagobj.CmdGroupByTH = Row!FTBrwCmdTHGroupBy.ToString
                            bttagobj.CmdGroupByEN = Row!FTBrwCmdENGroupBy.ToString
                            bttagobj.FTConField = Row!FTConField.ToString

                            bttagobj.DataRet = _dtret.Copy()
                            bttagobj.LoadData = True
                        Next


                    End If

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        FTBrwCmdGroupBy = bttagobj.CmdGroupByTH
                        _BrowseCmd = bttagobj.CmdSelectTH
                    Else
                        FTBrwCmdGroupBy = bttagobj.CmdGroupByEN
                        _BrowseCmd = bttagobj.CmdSelectEN
                    End If

                    _BrowseSortCmd = bttagobj.CmdSortBy
                    _BrowseWhereCmd = bttagobj.CmdWhere

                    _FTBrwCmdField = bttagobj.CmdBrowseField
                    _FTBrwCmdFieldOptional = bttagobj.CmdFieldOptional
                    _FTStringFormatWhare = bttagobj.CmdStringFormatWhare

                    _dtret = New DataTable

                    Try
                        _dtret = bttagobj.DataRet.Copy()

                        If _dtret Is Nothing Then
                            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & bttagobj.BrwRetID & " "

                            _dtret = New DataTable
                            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                        End If
                    Catch ex As Exception
                        _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                        _Qrysql &= vbCrLf & " WHERE BrwRetID=" & bttagobj.BrwRetID & " "

                        _dtret = New DataTable
                        _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                    End Try


                    _Qrysql = _BrowseCmd

                    With brwinfo

                        .BrowseID = brwsedataid
                        .ogvbrowse.ClearColumnsFilter()
                        Dim position As Point = MousePosition

                        If HI.ST.SysInfo.AppActScreen > 0 Then

                            Try

                                If Not (_form.Parent Is Nothing) Then

                                    Dim _formMain As Object = _form.Parent.FindForm
                                    position = _formMain.PointToClient(MousePosition)

                                Else

                                    position = _form.PointToClient(MousePosition)

                                End If

                            Catch ex As Exception
                            End Try

                        End If

                        '.X = MousePosition.X
                        '.Y = MousePosition.Y
                        .X = position.X
                        .Y = position.Y

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
                                                Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                                Select Case Dfm
                                                    Case "dd/MM/yyyy", "d"
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
                                        _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                    Else
                                        _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
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

                                                Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                                Select Case Dfm
                                                    Case "dd/MM/yyyy", "d"
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
                                            _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                        Else
                                            _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
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

                        Else

                            If _BrowseWhereCmd <> "" Then
                                _Where = " "
                            End If

                        End If

                        'If Not (HI.ST.SysInfo.Admin) Then


                        '    '' _BrowseWhereCmd = _BrowseWhereCmd + _Where


                        '    If Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FNHSysEmpID".ToUpper Then
                        '        _Where = HI.ST.Security.PermissionEmpData(_Where)
                        '    ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 15) = "FNHSysEmpTypeId".ToUpper Then
                        '        _Where = HI.ST.Security.PermissionEmpType(_Where)
                        '    ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 9) = "FTOrderNo".ToUpper Or Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FTOrderNoTo".ToUpper Then
                        '        _Where = HI.ST.Security.PermissionOrderCmpData(_Where)
                        '    End If

                        'End If

                        Dim _AllDataQuery As String = ""
                        _AllDataQuery = _Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  "


                        If Not (HI.ST.SysInfo.Admin) Then

                            If Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FNHSysEmpID".ToUpper Then
                                _AllDataQuery = HI.ST.Security.PermissionEmpData(_AllDataQuery)
                            ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 15) = "FNHSysEmpTypeId".ToUpper Then
                                _AllDataQuery = HI.ST.Security.PermissionEmpType(_AllDataQuery)
                            ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 9) = "FTOrderNo".ToUpper Or Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FTOrderNoTo".ToUpper Then
                                _AllDataQuery = HI.ST.Security.PermissionOrderCmpData(_AllDataQuery)
                            End If

                        End If


                        _AllDataQuery = _AllDataQuery & "   " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd

                        If _FTStringFormatWhare <> "" Then

                            Dim _StrAllStringFormatWhare As String = ""

                            For Each _QryCon As String In _FTStringFormatWhare.Split(",")
                                Dim _DataCon As String = ""

                                Select Case Microsoft.VisualBasic.Left(_QryCon, 1)
                                    Case "@"

                                        _DataCon = "-"
                                        Select Case UCase(_QryCon)
                                            Case "@USER".ToUpper
                                                _DataCon = HI.ST.UserInfo.UserName
                                            Case "@DATE".ToUpper
                                                _DataCon = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                                            Case "@CMPID".ToUpper
                                                _DataCon = HI.ST.SysInfo.CmpID.ToString
                                            Case "@CMP".ToUpper
                                                _DataCon = HI.ST.SysInfo.CmpCode

                                        End Select

                                        'If _DataCon <> "" Then
                                        If _StrAllStringFormatWhare = "" Then
                                            _StrAllStringFormatWhare = _DataCon
                                        Else
                                            _StrAllStringFormatWhare = _StrAllStringFormatWhare & "|" & _DataCon
                                        End If
                                        'End If
                                    Case Else
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

                                                        Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                                        Select Case Dfm
                                                            Case "dd/MM/yyyy", "d"
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

                                            'If _DataCon <> "" Then
                                            If _StrAllStringFormatWhare = "" Then
                                                _StrAllStringFormatWhare = _DataCon
                                            Else
                                                _StrAllStringFormatWhare = _StrAllStringFormatWhare & "|" & _DataCon
                                            End If
                                            'End If
                                        Next
                                End Select

                            Next

                            If _StrAllStringFormatWhare <> "" Then
                                _AllDataQuery = String.Format(_AllDataQuery, _StrAllStringFormatWhare.Split("|"))
                            End If

                        End If

                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_AllDataQuery, Conn.DB.DataBaseName.DB_SYSTEM)

                        .Data = _dtbrw.Copy
                        .DataRetField = _dtret.Copy

                        _dtbrw.Dispose()
                        _dtret.Dispose()

                        .ShowDialog()

                        If Not (.ValuesReturn Is Nothing) Then
                            _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm

                            For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)", "FNSeq")
                                For Each ctrl As Object In _form.Controls.Find(Row!FTRetField.ToString.Trim(), True)
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

                        .Data.Dispose()
                        .DataRetField.Dispose()

                        Try
                            _dtbrw.Dispose()
                            _dtret.Dispose()
                        Catch ex As Exception
                        End Try

                    End With





                Case 1
                    Dim _Form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).FindForm

                    Dim T As System.Type = _Form.GetType()

                    Dim _CmpH As String = ""
                    For Each ctrl As Object In _Form.Controls.Find("FNHSysCmpId", True)

                        ' _CmpH = HI.ST.SysInfo.CmpRunID

                        Select Case HI.ENM.Control.GeTypeControl(ctrl)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                End With

                                Exit For
                            Case ENM.Control.ControlType.TextEdit
                                With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                    If .Text = "" Then
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    Else
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    End If

                                End With

                                Exit For
                        End Select

                    Next

                    Select Case UCase("" & e.Button.Tag.ToString)

                        Case UCase("d")

                            With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                                Dim _pdbnameinfo As PropertyInfo
                                Dim _ptablenameinfo As PropertyInfo
                                Dim _pdoctypeinfo As PropertyInfo
                                Dim _pdocclearinfo As PropertyInfo
                                Dim _minfo As MethodInfo
                                Dim _minfo2 As MethodInfo

                                _pdbnameinfo = T.GetProperty("SysDBName")
                                _ptablenameinfo = T.GetProperty("SysTableName")
                                _pdoctypeinfo = T.GetProperty("SysDocType")
                                _pdocclearinfo = T.GetProperty("SysDocClear")
                                _minfo = T.GetMethod("InitData")
                                _minfo2 = T.GetMethod("DefaultsData")

                                If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                    If Not (_pdocclearinfo Is Nothing) Then

                                        Try

                                            If _pdocclearinfo.GetValue(_Form, Nothing) = True Then
                                                HI.TL.HandlerControl.ClearControl(_Form)
                                            End If

                                        Catch ex As Exception
                                            HI.TL.HandlerControl.ClearControl(_Form)
                                        End Try

                                    Else
                                        HI.TL.HandlerControl.ClearControl(_Form)
                                    End If

                                    .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_Form, Nothing).ToString, _ptablenameinfo.GetValue(_Form, Nothing).ToString, _pdoctypeinfo.GetValue(_Form, Nothing).ToString, True, _CmpH)

                                    If Not (_minfo Is Nothing) Then
                                        _minfo.Invoke(_Form, Nothing)
                                    End If

                                    If Not (_minfo2 Is Nothing) Then
                                        _minfo2.Invoke(_Form, Nothing)
                                    End If

                                End If

                            End With
                        Case UCase("emp")

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo
                            Dim _minfo2 As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            _minfo2 = T.GetMethod("DefaultsData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then
                                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                                    If Not (_minfo Is Nothing) Then
                                        _minfo.Invoke(_Form, Nothing)
                                    End If

                                    If Not (_minfo2 Is Nothing) Then
                                        _minfo2.Invoke(_Form, Nothing)
                                    End If

                                    _CmpH = ""
                                    For Each ctrl As Object In _Form.Controls.Find("FNHSysCmpId", True)

                                        _CmpH = HI.ST.SysInfo.CmpRunID

                                        'Select Case ctrl.GetType.FullName.ToString.ToUpper
                                        '    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                        '        With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                        '            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                        '        End With

                                        '        Exit For
                                        '    Case "DevExpress.XtraEditors.TextEdit".ToUpper
                                        '        With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                        '            If .Text = "" Then
                                        '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                        '            Else
                                        '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                        '            End If

                                        '        End With

                                        '        Exit For
                                        'End Select

                                    Next

                                    .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_Form, Nothing).ToString, _ptablenameinfo.GetValue(_Form, Nothing).ToString, _pdoctypeinfo.GetValue(_Form, Nothing).ToString, True, _CmpH)

                                End With

                            End If

                    End Select
            End Select
        Catch ex As Exception

        End Try


    End Sub

    Public Shared Sub DynamicButtonedit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If (SetStateProcClear) Then Exit Sub
        Dim brwsedataid As Integer = 0
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If .InvokeRequired Then
                .Invoke(New HI.Delegate.Dele.DynamicButtonedit_ValueChanged(AddressOf DynamicButtonedit_EditValueChanged), New Object() {sender, e})
            Else

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm

                If .Name.ToString.ToUpper = "FNHSysMatId".ToUpper Then
                    Exit Sub
                End If



                If .Name.ToString.ToUpper = "FNHSysBrandId".ToUpper Or .Name.ToString.ToUpper = "FNHSysUnitId".ToUpper Then
                    Beep()
                End If

                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent


                Dim bttagobj As New HI.TL.ButtonEditTag

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                    _Name = .Name.ToString
                    _Data = .Text


                    _BrowseID = brwsedataid

                    Try
                        If TypeOf .Properties.Buttons.Item(0).Tag Is HI.TL.ButtonEditTag Then

                            bttagobj = CType(.Properties.Buttons.Item(0).Tag, HI.TL.ButtonEditTag)
                            brwsedataid = bttagobj.BrowObjectID
                        Else
                            brwsedataid = Val(.Properties.Buttons.Item(0).Tag.ToString)

                            bttagobj = New HI.TL.ButtonEditTag()
                            bttagobj.Value = ""
                            bttagobj.BrowObjectID = brwsedataid
                            bttagobj.BrowseInfo = New wDynamicBrowseInfo(brwsedataid, _form)
                            bttagobj.LoadData = False

                            .Properties.Buttons.Item(0).Tag = bttagobj

                        End If

                    Catch ex As Exception
                    End Try

                    If brwsedataid <= 0 Then Exit Sub

                    .Properties.Tag = ""
                    _BrowseID = brwsedataid

                    Dim _Qrysql As String
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _FTStringFormatWhare As String = ""

                    If bttagobj.LoadData = False Then

                        _Qrysql = " SELECT  TOP 1    BrwID "
                        _Qrysql &= vbCrLf & " ,FTBrwCmdTH  "
                        _Qrysql &= vbCrLf & " ,FTBrwCmdEN  "
                        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy,FTStringFormatWhare,FNLenghtMinSearch,FNLenghtMaxSearch "
                        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
                        _Qrysql &= vbCrLf & " WHERE BrwID=" & brwsedataid & " "

                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        For Each Row As DataRow In _dtbrw.Rows
                            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

                            _dtret = New DataTable
                            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)


                            bttagobj.BrwRetID = Val(Row!BrwRetID.ToString)
                            bttagobj.CmdSelectTH = Row!FTBrwCmdTH.ToString
                            bttagobj.CmdSelectEN = Row!FTBrwCmdEN.ToString

                            bttagobj.CmdSortBy = Row!FTBrwCmdSort.ToString
                            bttagobj.CmdWhere = Row!FTBrwCmdWhere.ToString

                            bttagobj.MinLenght = Val(Row!FNLenghtMinSearch.ToString)
                            bttagobj.MaxLenght = Val(Row!FNLenghtMaxSearch.ToString)


                            bttagobj.CmdBrowseField = Row!FTBrwCmdField.ToString
                            bttagobj.CmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString
                            bttagobj.CmdStringFormatWhare = Row!FTStringFormatWhare.ToString
                            bttagobj.CmdGroupByTH = Row!FTBrwCmdTHGroupBy.ToString
                            bttagobj.CmdGroupByEN = Row!FTBrwCmdENGroupBy.ToString
                            bttagobj.FTConField = Row!FTConField.ToString

                            bttagobj.DataRet = _dtret.Copy()
                            bttagobj.LoadData = True
                        Next


                    End If


                    Dim TextValueLenght As Integer = _Data.Length

                    If Val(bttagobj.MinLenght) > 0 And Val(bttagobj.MaxLenght) > 0 And Val(bttagobj.MinLenght) <= Val(bttagobj.MaxLenght) Then
                        If TextValueLenght < Val(bttagobj.MinLenght) Then
                            Exit Sub
                        End If
                    End If

                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Or UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("emp") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo
                            Dim _minfo As MethodInfo
                            Dim _mloadfo As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")
                            _mloadfo = T.GetMethod("LoadDataInfo")

                            Dim _CmpH As String = ""
                            For Each ctrl As Object In _form.Controls.Find("FNHSysCmpId", True)

                                ' _CmpH = HI.ST.SysInfo.CmpRunID

                                Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                    Case ENM.Control.ControlType.ButtonEdit

                                        With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                        End With
                                        Exit For
                                    Case ENM.Control.ControlType.TextEdit
                                        With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                            If .Text = "" Then
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            Else
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End If
                                        End With
                                        Exit For

                                End Select

                            Next

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True, _CmpH) Then
                                    .Properties.Tag = ""
                                    Exit Sub
                                Else
                                    If Not (_mloadfo Is Nothing) Then
                                        _mloadfo.Invoke(_form, New Object() { .Text})
                                    End If
                                End If
                            End If
                        End If
                    End If

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        FTBrwCmdGroupBy = bttagobj.CmdGroupByTH
                        _BrowseCmd = bttagobj.CmdSelectTH
                    Else
                        FTBrwCmdGroupBy = bttagobj.CmdGroupByEN
                        _BrowseCmd = bttagobj.CmdSelectEN
                    End If

                    _BrowseSortCmd = bttagobj.CmdSortBy
                    _BrowseWhereCmd = bttagobj.CmdWhere

                    _FTBrwCmdField = bttagobj.CmdBrowseField
                    _FTBrwCmdFieldOptional = bttagobj.CmdFieldOptional
                    _FTStringFormatWhare = bttagobj.CmdStringFormatWhare
                    _Name = bttagobj.FTConField

                    _dtret = New DataTable


                    Try
                        _dtret = bttagobj.DataRet.Copy()

                        If _dtret Is Nothing Then
                            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & bttagobj.BrwRetID & " "

                            _dtret = New DataTable
                            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                        End If
                    Catch ex As Exception
                        _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                        _Qrysql &= vbCrLf & " WHERE BrwRetID=" & bttagobj.BrwRetID & " "

                        _dtret = New DataTable
                        _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                    End Try

                    _Qrysql = _BrowseCmd

                    If _Qrysql = "" Then Exit Sub
                    Dim _Where As String = ""

                    Dim I As Integer = 0
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

                                            Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                            Select Case Dfm
                                                Case "dd/MM/yyyy", "d"
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



                                    _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                                Else
                                    _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                End If
                            Next
                        Next
                    End If

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
                                            Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                            Select Case Dfm
                                                Case "dd/MM/yyyy", "d"
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
                                        _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                                    Else
                                        _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                    End If
                                End If

                            Next
                        Next

                    End If

                    I = 0
                    For Each _QryCon As String In _Name.Split(",")

                        I = I + 1

                        If _QryCon.Contains("=") Then
                            If I = 1 Then
                                If _Where = "" Then
                                    _Where = "  " & _QryCon & "   "
                                Else
                                    _Where &= " AND  " & _QryCon & " "
                                End If
                            Else
                                If _Where = "" Then
                                    _Where = "  " & _QryCon & "   "
                                Else
                                    _Where &= " AND  " & _QryCon & " "
                                End If
                            End If
                        Else
                            If I = 1 Then
                                If _Where = "" Then
                                    _Where = "  " & _QryCon & " ='" & rpQuoted(_Data) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_Data) & "'),char(32),'|'))  "
                                Else
                                    _Where &= " AND  " & _QryCon & " ='" & rpQuoted(_Data) & "'  "
                                End If

                            Else

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
                                                _DataCon = .Text
                                            End With
                                        Case ENM.Control.ControlType.MemoEdit
                                            With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                                _DataCon = .Text
                                            End With
                                        Case ENM.Control.ControlType.DateEdit
                                            With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                                Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                                Select Case Dfm
                                                    Case "dd/MM/yyyy", "d"
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
                                        _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                                    Else
                                        _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                    End If

                                Next
                            End If
                        End If

                    Next

                    If _Where <> "" Then
                        If _BrowseWhereCmd = "" Then
                            _Where = "   WHERE  " & _Where
                        Else
                            _Where = "   AND  " & _Where
                        End If
                    Else
                        If _BrowseWhereCmd <> "" Then
                            _Where = " "
                        End If
                    End If

                    Dim _AllDataQuery As String = ""
                    _AllDataQuery = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & "  " & _Where

                    If Not (HI.ST.SysInfo.Admin) Then
                        If Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FNHSysEmpID".ToUpper Then
                            _AllDataQuery = HI.ST.Security.PermissionEmpData(_AllDataQuery)
                        ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 15) = "FNHSysEmpTypeId".ToUpper Then
                            _AllDataQuery = HI.ST.Security.PermissionEmpType(_AllDataQuery)
                        ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 9) = "FTOrderNo".ToUpper Or Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FTOrderNoTo".ToUpper Then
                            _AllDataQuery = HI.ST.Security.PermissionOrderCmpData(_AllDataQuery)
                        End If
                    End If

                    _AllDataQuery = _AllDataQuery & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd


                    If _FTStringFormatWhare <> "" Then

                        Dim _StrAllStringFormatWhare As String = ""
                        Dim Indx As Integer = 0
                        For Each _QryCon As String In _FTStringFormatWhare.Split(",")
                            Dim _DataCon As String = ""

                            Select Case Microsoft.VisualBasic.Left(_QryCon, 1)
                                Case "@"

                                    _DataCon = "-"
                                    Select Case UCase(_QryCon)
                                        Case "@USER".ToUpper
                                            _DataCon = HI.ST.UserInfo.UserName
                                        Case "@DATE".ToUpper
                                            _DataCon = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                                        Case "@CMPID".ToUpper
                                            _DataCon = HI.ST.SysInfo.CmpID.ToString
                                        Case "@CMP".ToUpper
                                            _DataCon = HI.ST.SysInfo.CmpCode

                                    End Select

                                    'If _DataCon <> "" Then
                                    If _StrAllStringFormatWhare = "" Then
                                        _StrAllStringFormatWhare = _DataCon
                                    Else
                                        _StrAllStringFormatWhare = _StrAllStringFormatWhare & "|" & _DataCon
                                    End If
                                    'End If
                                Case Else
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

                                                    Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                                    Select Case Dfm
                                                        Case "dd/MM/yyyy", "d"
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

                                        'If _DataCon <> "" Then
                                        If _StrAllStringFormatWhare = "" And Indx = 0 Then
                                            _StrAllStringFormatWhare = _DataCon
                                        Else
                                            _StrAllStringFormatWhare = _StrAllStringFormatWhare & "|" & _DataCon
                                        End If
                                        'End If

                                    Next

                            End Select
                            Indx = Indx + 1
                        Next

                        If _StrAllStringFormatWhare <> "" Then
                            _AllDataQuery = String.Format(_AllDataQuery, _StrAllStringFormatWhare.Split("|"))
                        End If

                    End If

                    If _Where <> "" AndAlso _Name <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_AllDataQuery, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""
                        End If

                        With _dtbrw

                            If .Rows.Count > 0 Then

                                For Each Row As DataRow In _dtret.Rows

                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select

                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select

                                    End If
                                Next

                            Else

                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"

                                            Row!ValuesRet = "0"

                                        Case Else

                                            Row!ValuesRet = ""

                                    End Select

                                Next

                            End If
                        End With

                        For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)", "FNSeq")
                            For Each ctrl As Object In _form.Controls.Find(Row!FTRetField.ToString.Trim(), True)
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

                                        If Row!FTStatePropertyTag.ToString <> "Y" And ctrl.name.ToString.ToUpper = _Name.ToUpper Then
                                            Continue For
                                        End If

                                        With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)

                                            If .Properties.Buttons.Count > 1 Then
                                                If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("m") Then
                                                    If Row!FTStatePropertyTag.ToString = "Y" Then
                                                        .Properties.Tag = Row!ValuesRet.ToString
                                                    Else
                                                        If Val("" & .Properties.Tag.ToString) = 0 Then

                                                        Else
                                                            .Text = Row!ValuesRet.ToString
                                                        End If
                                                    End If
                                                Else
                                                    If Row!FTStatePropertyTag.ToString = "Y" Then
                                                        .Properties.Tag = Row!ValuesRet.ToString
                                                    Else
                                                        .Text = Row!ValuesRet.ToString
                                                    End If
                                                End If
                                            Else
                                                If Row!FTStatePropertyTag.ToString = "Y" Then
                                                    .Properties.Tag = Row!ValuesRet.ToString
                                                Else
                                                    .Text = Row!ValuesRet.ToString
                                                End If
                                            End If
                                        End With

                                    Case ENM.Control.ControlType.MemoEdit

                                        With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                            If Row!FTStatePropertyTag.ToString = "Y" Then
                                                .Properties.Tag = Row!ValuesRet.ToString
                                            Else
                                                .Text = Row!ValuesRet.ToString
                                            End If
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

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                End With

            End If

        End With

    End Sub

    Public Shared Sub DynamicButtonedit_LeaveOnly(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).FindForm
        If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If .Properties.Buttons.Count > 1 Then

                If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Or UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("emp") Then
                    Dim T As System.Type = _form.GetType()

                    Dim _pdbnameinfo As PropertyInfo
                    Dim _ptablenameinfo As PropertyInfo
                    Dim _pdoctypeinfo As PropertyInfo

                    Dim _minfo As MethodInfo

                    _pdbnameinfo = T.GetProperty("SysDBName")
                    _ptablenameinfo = T.GetProperty("SysTableName")
                    _pdoctypeinfo = T.GetProperty("SysDocType")
                    _minfo = T.GetMethod("InitData")


                    Dim _CmpH As String = ""
                    For Each ctrl As Object In _form.Controls.Find("FNHSysCmpId", True)


                        '  _CmpH = HI.ST.SysInfo.CmpRunID

                        Select Case ctrl.GetType.FullName.ToString.ToUpper
                            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper

                                With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                    _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                End With

                                Exit For

                            Case "DevExpress.XtraEditors.TextEdit".ToUpper

                                With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                    If .Text = "" Then
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    Else
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    End If

                                End With

                                Exit For

                        End Select
                    Next

                    If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then
                        If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True, _CmpH) Then
                            .Properties.Tag = ""
                            Exit Sub
                        End If
                    End If
                End If
            End If

            Dim _CtrlName As String = .Name.ToString()
            Dim _Caption As String = ""
            For Each ctrl As Object In _form.Controls.Find(_CtrlName & "_lbl", True)
                Select Case ctrl.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.LabelControl".ToUpper
                        With CType(ctrl, DevExpress.XtraEditors.LabelControl)
                            _Caption = .Text
                        End With
                        Exit For
                End Select
            Next

            If .Text <> "" Then
                If "" & .Properties.Tag.ToString = "" Then
                    .Text = "" : If .Enabled Then .Focus()
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm.Text, _Caption)

                End If
            Else
                .Text = .Text.Trim()
            End If
        End With

        Try
            Dim _form2 As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
            For Each ctrl As Object In _form2.Controls.Find(sender.Name.ToString & "_Browse", True)
                With CType(ctrl, HI.UCTR.HButtonDropDown)
                    If _form2.ActiveControl.Name.ToString <> sender.Name.ToString & "_Browse" Then
                        If (.Visible) Then
                            .Visible = False
                        End If
                        .DisposeObject()
                    End If

                End With
            Next
        Catch ex As Exception
        End Try


    End Sub

    Public Shared Sub DynamicButtoneditSysKey_Leave(ByVal sender As System.Object, ByVal SysKey As Integer)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            Dim _Name As String
            Dim _BrowseCmd As String = ""
            Dim _BrowseSortCmd As String = ""
            Dim _BrowseWhereCmd As String = ""
            Dim _Qry As String = ""
            Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).FindForm
            If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                _Qry = "SELECT TOP 1 'SELECT TOP 1  ' + "
                _Qry &= vbCrLf & "FTColumnKeyCode"
                _Qry &= vbCrLf & "  + '  FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=' + '" & Val(SysKey) & "'"
                _Qry &= vbCrLf & "  FROM  HSysTTablePK WITH (NOLOCK)  "
                _Qry &= vbCrLf & "  WHERE (FTColumnName IN (SELECT TOP 1 FTColumnName FROM HSysTTablePKRef WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(.Name.ToString) & "'))"
                _Qry &= vbCrLf & "    AND (FTColumnKeyCode <> '')"

                _Qry = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

                _Name = .Name.ToString

                If _Qry = "" Then
                    .Text = ""
                Else
                    .Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
                End If

            End With
        End With
    End Sub

    Public Shared Sub DynamicButtonediHSysKey_Leave(ByVal sender As System.Object, ByVal SysKey As Integer)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            Dim _BrowseID As Integer
            Dim _Name As String
            Dim _Data As String
            Dim _BrowseCmd As String = ""
            Dim _BrowseSortCmd As String = ""
            Dim _BrowseWhereCmd As String = ""
            Dim _Qry As String = ""
            Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).FindForm
            If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                _Qry = "SELECT TOP 1 'SELECT TOP 1  ' + "
                _Qry &= vbCrLf & "FTColumnKeyCode"
                _Qry &= vbCrLf & "  + '  FROM  [' +FTDBName+ '].' +FTPrefix +'.' + FTTableName + ' WITH ( NOLOCK ) WHERE ' +FTColumnName + '=' + '" & Val(SysKey) & "'"
                _Qry &= vbCrLf & "  FROM  HSysTTablePK WITH (NOLOCK)  "
                _Qry &= vbCrLf & "  WHERE (FTColumnName IN (SELECT TOP 1 FTColumnName FROM HSysTTablePKRef WITH(NOLOCK) WHERE  FTColumnNameRef= '" & HI.UL.ULF.rpQuoted(.Name.ToString) & "'))"
                _Qry &= vbCrLf & "    AND (FTColumnKeyCode <> '')"

                _Qry = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

                _Name = .Name.ToString

                If _Qry = "" Then
                    .Text = ""
                Else
                    .Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
                End If

                _Data = .Text
                .Properties.Tag = ""
                _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                Dim _Qrysql As String
                Dim _dtbrw As New DataTable
                Dim _dtret As New DataTable

                _Qrysql = " SELECT  TOP 1    BrwID, "

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
                Else
                    _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
                End If

                _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere "
                _Qrysql &= vbCrLf & " FROM  HSysBrowse  With (NOLOCK) "
                _Qrysql &= vbCrLf & " WHERE BrwID=" & _BrowseID & " "

                _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                _Qrysql = ""
                For Each Row As DataRow In _dtbrw.Rows

                    _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                    _Qrysql &= vbCrLf & " FROM  HSysBrowseRet With (NOLOCK) "
                    _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

                    _dtret = New DataTable
                    _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                    _BrowseCmd = Row!FTBrwCmd.ToString
                    _BrowseSortCmd = Row!FTBrwCmdSort.ToString
                    _BrowseWhereCmd = Row!FTBrwCmdWhere.ToString

                    _Qrysql = Row!FTBrwCmd.ToString
                    _Name = Row!FTConField.ToString

                Next

                If _Qrysql = "" Then Exit Sub
                Dim _Where As String = ""

                Dim I As Integer = 0
                For Each _QryCon As String In _Name.Split(",")

                    I = I + 1

                    If I = 1 Then
                        _Where = "  " & _QryCon & " ='" & rpQuoted(_Data) & "'  "
                    Else

                        Dim _DataCon As String = ""
                        For Each ctrl As Object In _form.Controls.Find(_QryCon.Trim(), True)
                            Select Case ctrl.GetType.FullName.ToString.ToUpper
                                Case "DevExpress.XtraEditors.TextEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                        _DataCon = .Text
                                    End With
                                Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.CalcEdit)
                                        _DataCon = .Value
                                    End With
                                Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                        _DataCon = .Text
                                    End With
                                Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                        _DataCon = .Text
                                    End With
                                Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                        Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                        Select Case Dfm
                                            Case "dd/MM/yyyy", "d"
                                                _DataCon = HI.UL.ULDate.ConvertEnDB(.Text)
                                            Case "dd/MM"
                                                _DataCon = Microsoft.VisualBasic.Right(.Text, 2) & "/" & Microsoft.VisualBasic.Left(.Text, 2)
                                            Case "MM/yyyy"
                                                _DataCon = Microsoft.VisualBasic.Right(.Text, 4) & "/" & Microsoft.VisualBasic.Left(.Text, 2)
                                            Case Else
                                                _DataCon = .Text
                                        End Select
                                    End With
                                Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.CheckEdit)
                                        _DataCon = IIf(.Checked, "1", "0")
                                    End With
                                Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)
                                        _DataCon = .Text
                                    End With
                            End Select

                            _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "

                        Next
                    End If

                Next

                If _BrowseWhereCmd = "" Then
                    _Where = "   WHERE  " & _Where
                Else
                    _Where = "   AND  " & _Where
                End If

                _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT TOP 1  ") & " " & _BrowseWhereCmd & _Where & " " & _BrowseSortCmd

                If _Where <> "" AndAlso _Name <> "" Then
                    _dtbrw = New DataTable
                    _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                    If _dtbrw.Rows.Count > 0 Then
                        .Properties.Tag = _Data
                    Else
                        .Properties.Tag = ""
                    End If

                    With _dtbrw
                        If .Rows.Count > 0 Then
                            For Each Row As DataRow In _dtret.Rows
                                If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then
                                    Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                Else
                                    Row!ValuesRet = ""
                                End If
                            Next
                        Else
                            For Each Row As DataRow In _dtret.Rows
                                Row!ValuesRet = ""
                            Next
                        End If
                    End With

                    For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                        For Each ctrl As Object In _form.Controls.Find(Row!FTRetField.ToString.Trim(), True)
                            Select Case ctrl.GetType.FullName.ToString.ToUpper
                                Case "DevExpress.XtraEditors.TextEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                        .Text = Row!ValuesRet.ToString
                                    End With
                                Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.CalcEdit)
                                        .Value = Val(Row!ValuesRet.ToString)
                                    End With
                                Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            .Properties.Tag = Row!ValuesRet.ToString
                                        Else
                                            .Text = Row!ValuesRet.ToString
                                        End If
                                    End With
                                Case "DevExpress.XtraEditors.MemoEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            .Properties.Tag = Row!ValuesRet.ToString
                                        Else
                                            .Text = Row!ValuesRet.ToString
                                        End If
                                    End With
                                Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                        .Text = Row!ValuesRet.ToString
                                    End With
                                Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.CheckEdit)
                                        .Checked = Val(Row!ValuesRet.ToString)
                                    End With
                                Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                    With CType(ctrl, DevExpress.XtraEditors.ComboBoxEdit)
                                        .Text = Row!ValuesRet.ToString
                                    End With
                            End Select
                        Next
                    Next
                End If

                _dtbrw.Dispose()
                _dtret.Dispose()



            End With

            If .Text <> "" Then
                If "" & .Properties.Tag.ToString = "" Then
                    HI.MG.Msg.Show(HI.MG.Msg.Input, CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm.Text, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
                    .Text = "" : If .Enabled Then .Focus()
                End If
            End If

        End With
    End Sub

    Public Shared Sub DynamicButtonedit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (SetStateProcClear) Then Exit Sub


        Dim brwsedataid As Integer = 0
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If .InvokeRequired Then
                .Invoke(New HI.Delegate.Dele.DynamicButtonedit_ValueChanged(AddressOf DynamicButtonedit_EditValueChanged), New Object() {sender, e})
            Else

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm

                If .Name.ToString.ToUpper = "FNHSysMatId".ToUpper Then
                    Exit Sub
                End If



                If .Name.ToString.ToUpper = "FNHSysBrandId".ToUpper Or .Name.ToString.ToUpper = "FNHSysUnitId".ToUpper Then
                    Beep()
                End If

                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent


                Dim bttagobj As New HI.TL.ButtonEditTag

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                    _Name = .Name.ToString
                    _Data = .Text


                    _BrowseID = brwsedataid

                    Try
                        If TypeOf .Properties.Buttons.Item(0).Tag Is HI.TL.ButtonEditTag Then

                            bttagobj = CType(.Properties.Buttons.Item(0).Tag, HI.TL.ButtonEditTag)
                            brwsedataid = bttagobj.BrowObjectID
                        Else
                            brwsedataid = Val(.Properties.Buttons.Item(0).Tag.ToString)

                            bttagobj = New HI.TL.ButtonEditTag()
                            bttagobj.Value = ""
                            bttagobj.BrowObjectID = brwsedataid
                            bttagobj.BrowseInfo = New wDynamicBrowseInfo(brwsedataid, _form)
                            bttagobj.LoadData = False

                            .Properties.Buttons.Item(0).Tag = bttagobj

                        End If

                    Catch ex As Exception
                    End Try

                    If brwsedataid <= 0 Then Exit Sub

                    .Properties.Tag = ""
                    _BrowseID = brwsedataid

                    Dim _Qrysql As String
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _FTStringFormatWhare As String = ""

                    If bttagobj.LoadData = False Then

                        _Qrysql = " SELECT  TOP 1    BrwID "
                        _Qrysql &= vbCrLf & " ,FTBrwCmdTH  "
                        _Qrysql &= vbCrLf & " ,FTBrwCmdEN  "
                        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy,FTStringFormatWhare,FNLenghtMinSearch,FNLenghtMaxSearch "
                        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
                        _Qrysql &= vbCrLf & " WHERE BrwID=" & brwsedataid & " "

                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        For Each Row As DataRow In _dtbrw.Rows
                            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

                            _dtret = New DataTable
                            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)


                            bttagobj.BrwRetID = Val(Row!BrwRetID.ToString)
                            bttagobj.CmdSelectTH = Row!FTBrwCmdTH.ToString
                            bttagobj.CmdSelectEN = Row!FTBrwCmdEN.ToString

                            bttagobj.CmdSortBy = Row!FTBrwCmdSort.ToString
                            bttagobj.CmdWhere = Row!FTBrwCmdWhere.ToString

                            bttagobj.MinLenght = Val(Row!FNLenghtMinSearch.ToString)
                            bttagobj.MaxLenght = Val(Row!FNLenghtMaxSearch.ToString)


                            bttagobj.CmdBrowseField = Row!FTBrwCmdField.ToString
                            bttagobj.CmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString
                            bttagobj.CmdStringFormatWhare = Row!FTStringFormatWhare.ToString
                            bttagobj.CmdGroupByTH = Row!FTBrwCmdTHGroupBy.ToString
                            bttagobj.CmdGroupByEN = Row!FTBrwCmdENGroupBy.ToString
                            bttagobj.FTConField = Row!FTConField.ToString

                            bttagobj.DataRet = _dtret.Copy()
                            bttagobj.LoadData = True
                        Next


                    End If


                    Dim TextValueLenght As Integer = _Data.Length

                    If Val(bttagobj.MinLenght) > 0 And Val(bttagobj.MaxLenght) > 0 And Val(bttagobj.MinLenght) <= Val(bttagobj.MaxLenght) Then
                        If TextValueLenght < Val(bttagobj.MinLenght) Then
                            Exit Sub
                        End If
                    End If

                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Or UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("emp") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo
                            Dim _minfo As MethodInfo
                            Dim _mloadfo As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")
                            _mloadfo = T.GetMethod("LoadDataInfo")

                            Dim _CmpH As String = ""
                            For Each ctrl As Object In _form.Controls.Find("FNHSysCmpId", True)

                                _CmpH = HI.ST.SysInfo.CmpRunID

                                'Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                '    Case ENM.Control.ControlType.ButtonEdit

                                '        With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                '            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                '        End With
                                '        Exit For
                                '    Case ENM.Control.ControlType.TextEdit
                                '        With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                '            If .Text = "" Then
                                '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                '            Else
                                '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                '            End If
                                '        End With
                                '        Exit For

                                'End Select

                            Next

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True, _CmpH) Then
                                    .Properties.Tag = ""
                                    Exit Sub
                                Else
                                    If Not (_mloadfo Is Nothing) Then
                                        _mloadfo.Invoke(_form, New Object() { .Text})
                                    End If
                                End If
                            End If
                        End If
                    End If


                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        FTBrwCmdGroupBy = bttagobj.CmdGroupByTH
                        _BrowseCmd = bttagobj.CmdSelectTH
                    Else
                        FTBrwCmdGroupBy = bttagobj.CmdGroupByEN
                        _BrowseCmd = bttagobj.CmdSelectEN
                    End If

                    _BrowseSortCmd = bttagobj.CmdSortBy
                    _BrowseWhereCmd = bttagobj.CmdWhere

                    _FTBrwCmdField = bttagobj.CmdBrowseField
                    _FTBrwCmdFieldOptional = bttagobj.CmdFieldOptional
                    _FTStringFormatWhare = bttagobj.CmdStringFormatWhare
                    _Name = bttagobj.FTConField

                    _dtret = New DataTable


                    Try
                        _dtret = bttagobj.DataRet.Copy()

                        If _dtret Is Nothing Then
                            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & bttagobj.BrwRetID & " "

                            _dtret = New DataTable
                            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                        End If
                    Catch ex As Exception
                        _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
                        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
                        _Qrysql &= vbCrLf & " WHERE BrwRetID=" & bttagobj.BrwRetID & " "

                        _dtret = New DataTable
                        _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
                    End Try

                    _Qrysql = _BrowseCmd

                    If _Qrysql = "" Then Exit Sub
                    Dim _Where As String = ""

                    Dim I As Integer = 0
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

                                            Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                            Select Case Dfm
                                                Case "dd/MM/yyyy", "d"
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



                                    _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                                Else
                                    _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                End If
                            Next
                        Next
                    End If

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
                                            Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                            Select Case Dfm
                                                Case "dd/MM/yyyy", "d"
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
                                        _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                                    Else
                                        _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                    End If
                                End If

                            Next
                        Next

                    End If

                    I = 0
                    For Each _QryCon As String In _Name.Split(",")

                        I = I + 1

                        If I = 1 Then
                            If _Where = "" Then
                                _Where = "  " & _QryCon & " ='" & rpQuoted(_Data) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_Data) & "'),char(32),'|'))  "
                            Else
                                _Where &= " AND  " & _QryCon & " ='" & rpQuoted(_Data) & "'  "
                            End If

                        Else

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
                                            _DataCon = .Text
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit
                                        With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                            _DataCon = .Text
                                        End With
                                    Case ENM.Control.ControlType.DateEdit
                                        With CType(ctrl, DevExpress.XtraEditors.DateEdit)
                                            Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                            Select Case Dfm
                                                Case "dd/MM/yyyy", "d"
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
                                    _Where &= "     " & _QryCon & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                                Else
                                    _Where &= "   AND  " & _QryCon & " ='" & rpQuoted(_DataCon) & "'  "
                                End If

                            Next
                        End If
                    Next


                    If _Where <> "" Then
                        If _BrowseWhereCmd = "" Then
                            _Where = "   WHERE  " & _Where
                        Else
                            _Where = "   AND  " & _Where
                        End If
                    Else
                        If _BrowseWhereCmd <> "" Then
                            _Where = " "
                        End If
                    End If

                    Dim _AllDataQuery As String = ""
                    _AllDataQuery = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & "  " & _Where

                    If Not (HI.ST.SysInfo.Admin) Then
                        If Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FNHSysEmpID".ToUpper Then
                            _AllDataQuery = HI.ST.Security.PermissionEmpData(_AllDataQuery)
                        ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 15) = "FNHSysEmpTypeId".ToUpper Then
                            _AllDataQuery = HI.ST.Security.PermissionEmpType(_AllDataQuery)
                        ElseIf Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 9) = "FTOrderNo".ToUpper Or Microsoft.VisualBasic.Left(sender.name.ToString.ToUpper, 11) = "FTOrderNoTo".ToUpper Then
                            _AllDataQuery = HI.ST.Security.PermissionOrderCmpData(_AllDataQuery)
                        End If
                    End If


                    _AllDataQuery = _AllDataQuery & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd


                    If _FTStringFormatWhare <> "" Then

                        Dim _StrAllStringFormatWhare As String = ""

                        For Each _QryCon As String In _FTStringFormatWhare.Split(",")
                            Dim _DataCon As String = ""

                            Select Case Microsoft.VisualBasic.Left(_QryCon, 1)
                                Case "@"

                                    _DataCon = "-"
                                    Select Case UCase(_QryCon)
                                        Case "@USER".ToUpper
                                            _DataCon = HI.ST.UserInfo.UserName
                                        Case "@DATE".ToUpper
                                            _DataCon = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                                        Case "@CMPID".ToUpper
                                            _DataCon = HI.ST.SysInfo.CmpID.ToString
                                        Case "@CMP".ToUpper
                                            _DataCon = HI.ST.SysInfo.CmpCode

                                    End Select

                                    'If _DataCon <> "" Then
                                    If _StrAllStringFormatWhare = "" Then
                                        _StrAllStringFormatWhare = _DataCon
                                    Else
                                        _StrAllStringFormatWhare = _StrAllStringFormatWhare & "|" & _DataCon
                                    End If
                                    'End If
                                Case Else
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

                                                    Dim Dfm As String = .Properties.DisplayFormat.FormatString.ToString

                                                    Select Case Dfm
                                                        Case "dd/MM/yyyy", "d"
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

                                        'If _DataCon <> "" Then
                                        If _StrAllStringFormatWhare = "" Then
                                            _StrAllStringFormatWhare = _DataCon
                                        Else
                                            _StrAllStringFormatWhare = _StrAllStringFormatWhare & "|" & _DataCon
                                        End If
                                        'End If

                                    Next
                            End Select

                        Next

                        If _StrAllStringFormatWhare <> "" Then
                            _AllDataQuery = String.Format(_AllDataQuery, _StrAllStringFormatWhare.Split("|"))
                        End If

                    End If

                    If _Where <> "" AndAlso _Name <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_AllDataQuery, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""
                        End If

                        With _dtbrw

                            If .Rows.Count > 0 Then

                                For Each Row As DataRow In _dtret.Rows

                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select

                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select

                                    End If
                                Next

                            Else

                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"

                                            Row!ValuesRet = "0"

                                        Case Else

                                            Row!ValuesRet = ""

                                    End Select

                                Next

                            End If
                        End With

                        For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)", "FNSeq")
                            For Each ctrl As Object In _form.Controls.Find(Row!FTRetField.ToString.Trim(), True)
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

                                        If Row!FTStatePropertyTag.ToString <> "Y" And ctrl.name.ToString.ToUpper = _Name.ToUpper Then
                                            Continue For
                                        End If

                                        With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)

                                            If .Properties.Buttons.Count > 1 Then
                                                If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("m") Then
                                                    If Row!FTStatePropertyTag.ToString = "Y" Then
                                                        .Properties.Tag = Row!ValuesRet.ToString
                                                    Else
                                                        If Val("" & .Properties.Tag.ToString) = 0 Then

                                                        Else
                                                            .Text = Row!ValuesRet.ToString
                                                        End If
                                                    End If
                                                Else
                                                    If Row!FTStatePropertyTag.ToString = "Y" Then
                                                        .Properties.Tag = Row!ValuesRet.ToString
                                                    Else
                                                        .Text = Row!ValuesRet.ToString
                                                    End If
                                                End If
                                            Else
                                                If Row!FTStatePropertyTag.ToString = "Y" Then
                                                    .Properties.Tag = Row!ValuesRet.ToString
                                                Else
                                                    .Text = Row!ValuesRet.ToString
                                                End If
                                            End If
                                        End With

                                    Case ENM.Control.ControlType.MemoEdit

                                        With CType(ctrl, DevExpress.XtraEditors.MemoEdit)
                                            If Row!FTStatePropertyTag.ToString = "Y" Then
                                                .Properties.Tag = Row!ValuesRet.ToString
                                            Else
                                                .Text = Row!ValuesRet.ToString
                                            End If
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

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                End With

            End If


            If .Text <> "" Then
                If "" & .Properties.Tag.ToString = "" Then
                    HI.MG.Msg.Show(HI.MG.Msg.Input, CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm.Text, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning)
                    .Text = "" : If .Enabled Then .Focus()
                End If
            End If

        End With





    End Sub

#End Region

#Region "Respon Button Edit"


    Public Shared Function GridViewGetBrowseRespon(ControlObject As Object, GridView As DevExpress.XtraGrid.Views.Grid.GridView, BrowseID As Integer, ByRef _dtret As DataTable, _
                                                ByRef _BrowseCmd As String, _
                                                ByRef _BrowseSortCmd As String, _
                                                ByRef _BrowseWhereCmd As String, _
                                                ByRef _FTBrwCmdField As String, _
                                                ByRef _FTBrwCmdFieldOptional As String, _
                                                ByRef FTBrwCmdGroupBy As String, _
                                                ByRef _Command As String, _
                                                 ByRef _ConFiled As String, _
                                                _Data As String, _
                                                Optional _Editvalue As Boolean = False) As String




        Dim _Qrysql As String
        Dim _ConFieldName As String = ""
        Dim _dtbrw As DataTable
        _Qrysql = " SELECT  TOP 1    BrwID, "

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
        Else
            _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
        End If

        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy "
        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
        _Qrysql &= vbCrLf & " WHERE BrwID=" & BrowseID & " "

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

            _Command = Row!FTBrwCmd.ToString

            If (_Editvalue) Then
                _ConFieldName = Row!FTConField.ToString
                _ConFiled = _ConFieldName
            End If

        Next

        Dim _Where As String = ""
        Dim _DataCon As String = ""

        Dim I As Integer = 0
        If _ConFieldName <> "" Then
            For Each _QryCon As String In _ConFieldName.Split(",")

                I = I + 1

                If I = 1 Then

                    If _Where = "" Then
                        _Where &= "     " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_Data) & "'   AND  Len(Replace((" & _QryCon.Replace("_Hide", "") & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_Data) & "'),char(32),'|'))  "
                    Else
                        _Where &= "   AND  " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_Data) & "'  "
                    End If

                Else

                    _DataCon = ""

                    With GridView
                        If Not (.Columns.ColumnByFieldName(_QryCon) Is Nothing) Then
                            _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon).ToString

                            If _Where = "" Then
                                _Where &= "     " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_DataCon) & "'  AND    Len(Replace((" & _QryCon.Replace("_Hide", "") & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                            Else
                                _Where &= "   AND  " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_DataCon) & "'  "
                            End If

                        End If
                    End With

                End If
            Next
        End If


        '------------Browse Where Require Field---------------
        If _FTBrwCmdField <> "" Then
            For Each _QryCon As String In _FTBrwCmdField.Split(",")
                _DataCon = ""
                With GridView
                    If Not (.Columns.ColumnByFieldName(_QryCon.Trim()) Is Nothing) Then
                        Select Case .Columns.ColumnByFieldName(_QryCon.Trim()).ColumnType.FullName.ToString.ToUpper
                            Case Else
                                _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon.Trim())
                        End Select

                        If _Where = "" Then
                            _Where &= "     " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_DataCon) & "'   AND  Len(Replace((" & _QryCon.Replace("_Hide", "") & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                        Else
                            _Where &= "   AND  " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_DataCon) & "'  "
                        End If
                    End If
                End With

            Next

        End If

        '------------Browse Where Require Field---------------

        '------------Browse Where Optional Field---------------
        If _FTBrwCmdFieldOptional <> "" Then
            For Each _QryCon As String In _FTBrwCmdFieldOptional.Split(",")
                _DataCon = ""
                With GridView
                    If Not (.Columns.ColumnByFieldName(_QryCon.Trim()) Is Nothing) Then
                        Select Case .Columns.ColumnByFieldName(_QryCon.Trim()).ColumnType.FullName.ToString.ToUpper
                            Case Else
                                _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon.Trim())
                        End Select

                        If _DataCon <> "" Then
                            If _Where = "" Then
                                _Where &= "     " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_DataCon) & "'    AND  Len(Replace((" & _QryCon.Replace("_Hide", "") & "),char(32),'|')) = Len(Replace(('" & rpQuoted(_DataCon) & "'),char(32),'|'))  "
                            Else
                                _Where &= "   AND  " & _QryCon.Replace("_Hide", "") & " ='" & rpQuoted(_DataCon) & "'  "
                            End If
                        End If
                    End If
                End With
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


        'If Not (HI.ST.SysInfo.Admin) Then
        '    If Microsoft.VisualBasic.Left(ControlObject.name.ToString.ToUpper, 11) = "FNHSysEmpID".ToUpper Then
        '        _Where = HI.ST.Security.PermissionEmpData(_Where)
        '    ElseIf Microsoft.VisualBasic.Left(ControlObject.name.ToString.ToUpper, 15) = "FNHSysEmpTypeId".ToUpper Then
        '        _Where = HI.ST.Security.PermissionEmpType(_Where)
        '    End If
        'End If

        _dtbrw.Dispose()

        Return _Where
    End Function

    Public Shared Sub DynamicResponButtone_Gotocus(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim _Data As String

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                _Data = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString
            End With
        Catch ex As Exception
            _Data = ""
        End Try

        Try
            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                .Text = _Data
                .Properties.Tag = .Text
            End With
        Catch ex As Exception
        End Try

    End Sub

    Public Overloads Shared Sub DynamicResponButtone_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try
            Select Case e.Button.Index
                Case 0

                    If Val(e.Button.Tag.ToString) <= 0 Then Exit Sub
                    _StateResponProcNormal = False
                    If Not (_StateResponProcNormal) Then
                        _StateResponProcNormal = True
                        Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                        With New wDynamicBrowseInfo(Val(e.Button.Tag.ToString), _form)
                            .BrowseID = Val(e.Button.Tag.ToString)


                            Dim position As Point = MousePosition

                            If HI.ST.SysInfo.AppActScreen > 0 Then

                                Try

                                    If Not (_form.Parent Is Nothing) Then
                                        Dim _formMain As Object = _form.Parent.FindForm
                                        position = _formMain.PointToClient(MousePosition)
                                    Else
                                        position = _form.PointToClient(MousePosition)
                                    End If

                                Catch ex As Exception
                                End Try

                            End If

                            '.X = MousePosition.X
                            '.Y = MousePosition.Y
                            .X = position.X
                            .Y = position.Y

                            Dim _Qrysql As String = ""
                            Dim _dtbrw As New DataTable
                            Dim _dtret As New DataTable
                            Dim _BrowseCmd As String = ""
                            Dim _BrowseSortCmd As String = ""
                            Dim _BrowseWhereCmd As String = ""
                            Dim _FTBrwCmdField As String = ""
                            Dim _FTBrwCmdFieldOptional As String = ""
                            Dim FTBrwCmdGroupBy As String = ""
                            Dim _Where As String = ""
                            Dim _ConFiledName As String = ""
                            _Where = GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), .BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, "", "")

                            If _Qrysql = "" Then
                                _StateResponProcNormal = False
                                Exit Sub
                            End If

                            _dtbrw = New DataTable
                            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

                            .Data = _dtbrw.Copy
                            .DataRetField = _dtret.Copy

                            _dtbrw.Dispose()
                            _dtret.Dispose()

                            .ShowDialog()

                            If Not (.ValuesReturn Is Nothing) Then

                                For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)")
                                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                                        Dim _ColName As String = Row!FTRetField.ToString

                                        If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        Else
                                            _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                            If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                                If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                                    _ColName = .FocusedColumn.Name.ToString
                                                End If
                                            End If
                                        End If

                                        If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                            Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                            If ctrl Is Nothing Then

                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            Else
                                                If Row!FTStatePropertyTag.ToString = "Y" Then
                                                    Try
                                                        If ctrl Is Nothing Then
                                                            Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                            End Select
                                                        Else
                                                            Try
                                                                With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                                    .Properties.Tag = Row!ValuesRet.ToString
                                                                End With
                                                            Catch ex As Exception
                                                            End Try

                                                        End If
                                                    Catch ex As Exception
                                                    End Try
                                                Else
                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select

                                                End If
                                            End If
                                        End If

                                    End With
                                Next
                            End If

                            .Data.Dispose()
                            .DataRetField.Dispose()

                        End With

                        _StateResponProcNormal = False
                        Call DynamicResponButtonedit_EditValueChanged(sender, New System.EventArgs)

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            Try
                                Dim IC As Integer = 1
                                Do While .Columns(.FocusedColumn.AbsoluteIndex + IC).Visible = False
                                    IC = IC + 1
                                Loop
                                .FocusedColumn = .Columns(.FocusedColumn.AbsoluteIndex + IC)
                            Catch ex As Exception
                            End Try
                        End With

                    End If

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub DynamicResponButtonedit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If Not (_StateResponProcNormal) Then
                _StateResponProcNormal = True

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _ConFiledName As String = ""

                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                    _Name = .Name.ToString.Replace("Res_", "")


                    _Data = .Text
                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo


                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True) Then
                                    .Properties.Tag = ""
                                    _StateResponProcNormal = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    .Properties.Tag = ""
                    _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                    Dim _Qrysql As String = ""
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _Where As String = ""

                    _Where = GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), _BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, _ConFiledName, _Data, True)

                    If _Qrysql = "" Then

                        _StateResponProcNormal = False
                        Exit Sub
                    End If

                    _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & _Where & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd

                    If _Where <> "" AndAlso _ConFiledName <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""

                        End If

                        With _dtbrw
                            If .Rows.Count > 0 Then
                                For Each Row As DataRow In _dtret.Rows
                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select

                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select

                                    End If
                                Next
                            Else
                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"
                                            Row!ValuesRet = "0"
                                        Case Else
                                            Row!ValuesRet = ""
                                    End Select

                                Next
                            End If
                        End With

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                                If .FocusedColumn.FieldName.ToString.ToUpper = Row!FTRetField.ToString.ToUpper Then
                                    Continue For
                                End If

                                Dim _ColName As String = Row!FTRetField.ToString
                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                Else
                                    _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                    If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                            _ColName = .FocusedColumn.Name.ToString
                                        End If
                                    End If
                                End If

                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then

                                    Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                    If ctrl Is Nothing Then

                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                            Case "System.Int32".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                            Case "System.String".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            Case Else
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                        End Select
                                    Else
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            Try
                                                If ctrl Is Nothing Then
                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select
                                                Else
                                                    Try
                                                        With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                            .Tag = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception

                                                    End Try


                                                End If
                                            Catch ex As Exception
                                            End Try

                                        Else


                                            If Row!FTRetField.ToString.ToUpper = _Name.ToUpper Then
                                            Else
                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If

                                        End If
                                    End If
                                End If
                            Next
                        End With


                    End If

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                End With

                _StateResponProcNormal = False
            End If

        End With
    End Sub

    Public Shared Sub DynamicResponButtonedit_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then
                Call DynamicResponButtonedit_EditValueChanged(sender, e)
                If "" & .Properties.Tag.ToString = "" Then
                    .Text = ""
                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .SetFocusedRowCellValue(.FocusedColumn, "")
                    End With
                End If
            End If
        End With


    End Sub

    Public Shared Sub DynamicResponButtoneSysHide_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Try
            Select Case e.Button.Index
                Case 0

                    If Val(e.Button.Tag.ToString) <= 0 Then Exit Sub
                    _StateResponProc = False
                    If Not (_StateResponProc) Then
                        _StateResponProc = True
                        Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                        With New wDynamicBrowseInfo(Val(e.Button.Tag.ToString), _form)
                            .BrowseID = Val(e.Button.Tag.ToString)

                            Dim position As Point = MousePosition

                            If HI.ST.SysInfo.AppActScreen > 0 Then
                                Try

                                    If Not (_form.Parent Is Nothing) Then
                                        Dim _formMain As Object = _form.Parent.FindForm
                                        position = _formMain.PointToClient(MousePosition)
                                    Else
                                        position = _form.PointToClient(MousePosition)
                                    End If

                                Catch ex As Exception

                                End Try
                            End If

                            '.X = MousePosition.X
                            '.Y = MousePosition.Y
                            .X = position.X
                            .Y = position.Y

                            Dim _Qrysql As String = ""
                            Dim _dtbrw As New DataTable
                            Dim _dtret As New DataTable
                            Dim _BrowseCmd As String = ""
                            Dim _BrowseSortCmd As String = ""
                            Dim _BrowseWhereCmd As String = ""
                            Dim _FTBrwCmdField As String = ""
                            Dim _FTBrwCmdFieldOptional As String = ""
                            Dim FTBrwCmdGroupBy As String = ""
                            Dim _Where As String = ""
                            Dim _ConFiledName As String = ""
                            _Where = GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), .BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, "", "")

                            If _Qrysql = "" Then
                                _StateResponProc = False
                                Exit Sub
                            End If

                            _dtbrw = New DataTable
                            _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

                            .Data = _dtbrw.Copy
                            .DataRetField = _dtret.Copy

                            _dtbrw.Dispose()
                            _dtret.Dispose()

                            .ShowDialog()

                            If Not (.ValuesReturn Is Nothing) Then

                                For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)")
                                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                                        Dim _ColName As String = Row!FTRetField.ToString

                                        If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        Else
                                            _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                            If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                                If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                                    _ColName = .FocusedColumn.Name.ToString
                                                End If
                                            End If
                                        End If

                                        If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                            Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                            If ctrl Is Nothing Then

                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            Else
                                                If Row!FTStatePropertyTag.ToString = "Y" Then
                                                    Try
                                                        If ctrl Is Nothing Then
                                                            Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                            End Select
                                                        Else

                                                            Try
                                                                Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                End Select
                                                            Catch ex As Exception
                                                            End Try

                                                            'Try
                                                            '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                            '        .Text = Row!ValuesRet.ToString
                                                            '    End With
                                                            'Catch ex As Exception
                                                            'End Try

                                                            Try
                                                                With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                                    .Tag = Row!ValuesRet.ToString
                                                                End With
                                                            Catch ex As Exception
                                                            End Try

                                                        End If
                                                    Catch ex As Exception
                                                    End Try
                                                Else

                                                    Try
                                                        With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                            .Text = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception
                                                    End Try

                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select
                                                End If
                                            End If
                                        End If
                                    End With
                                Next
                            End If

                            .Data.Dispose()
                            .DataRetField.Dispose()

                        End With

                        _StateResponProc = False

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            Try
                                Dim IC As Integer = 1
                                Do While .Columns(.FocusedColumn.AbsoluteIndex + IC).Visible = False
                                    IC = IC + 1
                                Loop
                                .FocusedColumn = .Columns(.FocusedColumn.AbsoluteIndex + IC)
                            Catch ex As Exception
                            End Try
                        End With

                    End If

            End Select
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub DynamicResponButtoneditSysHide_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If Not (_StateResponProc) Then
                _StateResponProc = True

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _ConFiledName As String = ""

                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                    _Name = .Name.ToString.Replace("Res_", "")


                    _Data = .Text
                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo


                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True) Then
                                    .Properties.Tag = ""
                                    _StateResponProc = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    .Properties.Tag = ""

                    _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                    Dim _Qrysql As String = ""
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _Where As String = ""

                    _Where = GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), _BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, _ConFiledName, _Data, True)

                    If _Qrysql = "" Then

                        _StateResponProc = False
                        Exit Sub
                    End If

                    _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & _Where & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd

                    If _Where <> "" AndAlso _ConFiledName <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""

                        End If

                        With _dtbrw
                            If .Rows.Count > 0 Then
                                For Each Row As DataRow In _dtret.Rows
                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select
                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select
                                    End If
                                Next
                            Else
                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"
                                            Row!ValuesRet = "0"
                                        Case Else
                                            Row!ValuesRet = ""
                                    End Select

                                Next
                            End If
                        End With

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                                If .FocusedColumn.FieldName.ToString.ToUpper = Row!FTRetField.ToString.ToUpper And Row!FTStatePropertyTag.ToString <> "Y" Then
                                    Continue For
                                End If

                                Dim _ColName As String = Row!FTRetField.ToString
                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                Else
                                    _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                    If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                            _ColName = .FocusedColumn.Name.ToString
                                        End If
                                    End If
                                End If


                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then

                                    Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                    If ctrl Is Nothing Then

                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                            Case "System.Int32".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                            Case "System.String".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            Case Else
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                        End Select
                                    Else
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            Try
                                                If ctrl Is Nothing Then
                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select
                                                Else
                                                    Try
                                                        Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                            Case "System.Int32".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                            Case "System.String".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                            Case Else
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                        End Select
                                                    Catch ex As Exception
                                                    End Try

                                                    'Try
                                                    '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                    '        .Text = Row!ValuesRet.ToString
                                                    '    End With
                                                    'Catch ex As Exception
                                                    'End Try

                                                    Try
                                                        With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                            .Tag = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                End If
                                            Catch ex As Exception
                                            End Try

                                        Else


                                            If Row!FTRetField.ToString.ToUpper = _Name.ToUpper Then
                                            Else
                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If

                                        End If
                                    End If



                                End If
                            Next
                        End With


                    End If

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                End With

                _StateResponProc = False
            End If

        End With
    End Sub

    Public Shared Sub DynamicResponButtoneditSysHide_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then
                Dim _Data As String = .Text
                Dim _value As String = ""
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    Dim _FieldName As String = .FocusedColumn.FieldName.ToString()

                    Select Case .Columns.ColumnByFieldName(.FocusedColumn.FieldName & "_Hide").ColumnType.FullName.ToString.ToUpper
                        Case "System.Int32".ToUpper
                            _value = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName & "_Hide").ToString
                        Case "System.String".ToUpper
                            _value = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName & "_Hide").ToString()
                        Case Else
                            _value = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName & "_Hide").ToString()
                    End Select

                    If _value = "0" Or _value = "" Then
                        .SetFocusedRowCellValue(.FocusedColumn, "")

                    End If

                End With
            End If
        End With

    End Sub

    Public Shared Sub RepositoryComboBoxSysHide_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            .SetFocusedRowCellValue(.FocusedColumn.FieldName & "_Hide", CType(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex)
        End With
    End Sub

#End Region

#Region "Grid Lookup Edit"
    Private Shared Sub GridLookUpEdit_ButtonClick(sender As Object, e As ButtonPressedEventArgs)

        Select Case e.Button.Index
            Case 1

                If e.Button.Kind = ButtonPredefines.Clear Then

                    With CType(sender, DevExpress.XtraEditors.GridLookUpEdit)
                        .EditValue = Nothing
                    End With

                End If

        End Select

    End Sub

#End Region

#Region "Grid View"
    Public Shared Sub GridView_DoubleClick(sender As Object, e As System.EventArgs)

        With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If .FocusedRowHandle < 0 Then Exit Sub
            Dim _Form As Object = .GridControl.FindForm

            Dim pt As Point = .GridControl.PointToClient(Control.MousePosition)
            Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = .CalcHitInfo(pt)

            If Not (info.InRow Or info.InRowCell) Then
                Exit Sub
            End If

            Try
                If .FocusedColumn.OptionsColumn.AllowEdit = True Then
                    Exit Sub
                End If
            Catch ex As Exception
                Exit Sub
            End Try


            If Not (_Form Is Nothing) Then
                Dim _Value As String = Nothing
                Dim myList As New ArrayList

                Dim T As System.Type = _Form.GetType()

                Dim _pcallmenuinfo As PropertyInfo = T.GetProperty("CallMenuName")
                Dim _pcallmathodinfo As PropertyInfo = T.GetProperty("CallMethodName")
                Dim _pcallmethodpraminfo As PropertyInfo = T.GetProperty("CallMethodParm")

                If (Not (_pcallmenuinfo Is Nothing)) AndAlso (Not (_pcallmathodinfo Is Nothing)) AndAlso (Not (_pcallmethodpraminfo Is Nothing)) Then
                    Dim _MemuName As String = _pcallmenuinfo.GetValue(_Form, Nothing).ToString()
                    Dim _MethodName As String = _pcallmathodinfo.GetValue(_Form, Nothing).ToString()
                    Dim _MethodParmName As String = _pcallmethodpraminfo.GetValue(_Form, Nothing).ToString()

                    If _MemuName <> "" And _MethodName <> "" And _MethodParmName <> "" Then


                        If _MethodParmName <> "" Then
                            For Each Str As String In _MethodParmName.Split(",")
                                If Str <> "" Then

                                    _Value = Nothing

                                    If Not (.Columns.ColumnByFieldName(Str) Is Nothing) Then
                                        _Value = .GetFocusedRowCellValue(.Columns.ColumnByFieldName(Str)).ToString()
                                    End If

                                    myList.Add(_Value)

                                End If
                            Next
                        End If

                        Try

                            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                            HI.ST.SysInfo.MenuName = _MemuName
                            Call CallByName(_Form.Parent.Parent, "CallWindowForm", CallType.Method, {_MemuName, _MethodName, myList.ToArray(GetType(String))})
                            HI.ST.SysInfo.MenuName = _TmpMenu

                        Catch ex As Exception
                        End Try
                    End If
                End If

            End If

        End With
    End Sub
#End Region

    Private Shared Function rpQuoted(ByVal Str As String) As String
        If Str <> "" Then
            rpQuoted = Replace(Str, Chr(39), Chr(39) & Chr(39))
        Else
            rpQuoted = Str
        End If
    End Function

End Class



