Public Class Control

    Enum ControlType As Integer
        [XtraForm] = 0
        [BarButtonItem] = 1
        [BarLargeButtonItem] = 2
        [ButtonEdit] = 3
        [MemoEdit] = 4
        [ComboBoxEdit] = 5
        [TextEdit] = 6
        [DateEdit] = 7
        [CalcEdit] = 8
        [RadioGroup] = 9
        [CheckEdit] = 10
        [GridControl] = 11
        [PictureEdit] = 12
        [LabelControl] = 13
        [SimpleButton] = 14
        [GroupControl] = 15
        [XtraTabControl] = 16
        [RadioButton] = 17
        [XtraTabPage] = 18
        [Bar] = 19
        [LayoutControl] = 20
        [PanelControl] = 21
        [RepositoryItemTextEdit] = 22
        [RepositoryItemMemoEdit] = 23
        [RepositoryItemButtonEdit] = 24
        [RepositoryItemCalcEdit] = 25
        [RepositoryItemComboBox] = 26
        [RepositoryItemDateEdit] = 27
        [TreeList] = 28
        [DockPanel] = 29
        [TimeEdit] = 30
        [LookUpEdit] = 31
        [RepositoryItemLookUpEdit] = 32
        [PivotGridControl] = 33
        [ChartControl] = 34
        [GridView] = 35
        [AdvBandedGridView] = 37
        [BandedGridView] = 38
        [GridLookUpEdit] = 39
        [RichEditControl] = 40
        [WindowsUIButton] = 41
        [WindowsUIButtonPanel] = 42
        [WindowsUISeparator] = 43
        [CheckedComboBoxEdit] = 44
        [HButtonDropDown] = 888
        [Nothing] = 999

    End Enum

    Public Shared Function GeTypeControl(Obj As Object) As ControlType
        Try

            Dim _TypeName As String = Obj.GetType.Name

            If TypeOf Obj Is DevExpress.XtraEditors.XtraForm Then
                Return ControlType.XtraForm
            End If

            Return CType([Enum].Parse(GetType(ControlType), _TypeName), ControlType)
        Catch ex As Exception
            Return ControlType.Nothing
        End Try
    End Function

End Class
