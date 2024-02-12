Imports DevExpress.XtraGrid.Views.Grid

Public Class wPatternMasterPlan_New

    Private GridDataBefore As String = ""
    Private _FocusedColumn As DevExpress.XtraGrid.Columns.GridColumn
    Private _FocusedRowHendle As Integer = 0
    Private _PatternChanged As List(Of PatternChanged) = New List(Of PatternChanged)

    Sub New()

        InitializeComponent()

        With ReposPatternType
            AddHandler .Click, AddressOf ItemString_GotFocus
            AddHandler .Leave, AddressOf ItemString_Leave
        End With

        With ReposDate
            AddHandler .Click, AddressOf ItemString_GotFocus
            AddHandler .Leave, AddressOf ItemString_Leave
        End With

        With ReposFTPtnNote
            AddHandler .Click, AddressOf ItemString_GotFocus
            AddHandler .Leave, AddressOf ItemString_Leave
        End With

        InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        Dim sFieldSum As String = "FNQuantity"

        With ogvPattern
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        '------End Add Summary Grid-------------
    End Sub
#End Region


#Region "Custom summaries"

#End Region

#Region "Property"

#End Region

#Region "Procedure"

#End Region

#Region "General"

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
        'If _PatternChanged.Count = 0 Then
        '    Me.Close()
        'Else
        '    If HI.MG.ShowMsg.mConfirmProcess("มีการแก้ไขข้อมูล ต้องการบันทึกข้อมูลหรือไม่ ?", 1000000001) = True Then
        '        Exit Sub
        '    Else
        '        Me.Close()
        '    End If
        'End If

    End Sub


    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        With Me.ogvPattern
            .OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
            .OptionsSelection.MultiSelect = False
            .ClearColumnsFilter()

        End With
        RemoveHandler ReposDate.Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
        RemoveHandler ReposDate.Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Call LoadOrderProdDetail()
        _Spls.Close()

    End Sub

    Private Sub LoadOrderProdDetail()
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogcPattern.DataSource = Nothing

        Dim DataFrom As String = ""

        cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_GET_DATAFORPATTERN "

        If chkSample.Checked And chkProd.Checked Then
            cmd &= vbCrLf & "@DataFrom = 'B'"
        Else
            If chkSample.Checked Then
                cmd &= vbCrLf & "@DataFrom = 'S'"
            End If
            If chkProd.Checked Then
                cmd &= vbCrLf & "@DataFrom = 'P'"
            End If
        End If

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmd &= vbCrLf & ", @Lang = '" & ST.Lang.eLang.TH & "' "
        Else
            cmd &= vbCrLf & ", @Lang = '" & ST.Lang.eLang.EN & "' "
        End If

        cmd &= vbCrLf & ", @FNHSysCmpID = '" + (HI.ST.SysInfo.CmpID).ToString + "'"

        If (FTPayYear.Text <> "") Then
            cmd &= vbCrLf & ", @YearStart = '" + FTPayYear.Text + "/01/01' , @YearEnd = '" + FTPayYear.Text + "/12/31' "
        End If

        If (FNHSysStyleId.Text <> "") Then
            'If (FNHSysStyleId.Text <> "" And FNHSysStyleIdTo.Text <> "") Then
            cmd &= vbCrLf & ", @FNHSysStyleId = '" & Val(FNHSysStyleId.Properties.Tag.ToString) & "' "
            cmd &= vbCrLf & ", @FNHSysStyleIdTo = '" & Val(FNHSysStyleId.Properties.Tag.ToString) & "' "
        End If

        If (FNHSysSeasonId.Text <> "") Then
            'If (FNHSysSeasonId.Text <> "" And FNHSysSeasonIdTo.Text <> "") Then
            cmd &= vbCrLf & ", @FNHSysSeasonId = '" & Val(FNHSysSeasonId.Properties.Tag.ToString) & "' "
            cmd &= vbCrLf & ", @FNHSysSeasonIdTo = '" & Val(FNHSysSeasonId.Properties.Tag.ToString) & "' "
        End If

        If (FTUserName.Text <> "") Then
            'If (FNHSysMerTeamId.Text <> "" And FNHSysMerTeamIdTo.Text <> "") Then
            cmd &= vbCrLf & ", @FTUserName = '" & FTUserName.Text & "' "
            cmd &= vbCrLf & ", @FTUserNameTo = '" & FTUserName.Text & "' "
        End If

        If (FNHSysBuyId.Text <> "") Then
            'If (FNHSysBuyIdFrom.Text <> "" And FNHSysBuyIdTo.Text <> "") Then
            cmd &= vbCrLf & ", @FNHSysBuyIdFrom = '" & FNHSysBuyId.Properties.Tag.ToString & "' "
            cmd &= vbCrLf & ", @FNHSysBuyIdTo = '" & FNHSysBuyId.Properties.Tag.ToString & "' "
        End If


        If (FTStartOrderDate.Text <> "" And FTEndOrderDate.Text <> "") Then
            cmd &= vbCrLf & ", @OrderDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "' "
            cmd &= vbCrLf & ", @OrderDateTo = '" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
        End If

        If (FTStartReqDate.Text <> "" And FTEndReqDate.Text <> "") Then
            cmd &= vbCrLf & ", @FTPatternDate = '" & HI.UL.ULDate.ConvertEnDB(FTStartReqDate.Text)
            cmd &= vbCrLf & "', @FTPatternDateTo = '" & HI.UL.ULDate.ConvertEnDB(FTEndReqDate.Text) & "' "
        End If


        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcPattern.DataSource = _dtprod.Copy

        _dtprod.Dispose()
    End Sub


    Private Sub ItemString_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                GridDataBefore = (.GetFocusedRowCellValue(.FocusedColumn))
            End With

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub


    Private Sub ItemString_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                If .FocusedRowHandle < -1 Then Exit Sub

                Dim FieldName As String = .FocusedColumn.Name.ToString
                Dim _PatternType As String = ""
                Dim _FTPtnNote As String = ""
                Dim _FTPtnDate As String = ""
                Dim NewData As String = ""

                If FieldName = "FTPatternType" Then
                    _PatternType = CType(sender, DevExpress.XtraEditors.ButtonEdit).Text
                    _FTPtnNote = .GetRowCellValue(.FocusedRowHandle, "FTPtnNote").ToString()
                    If (ReposDate.ToString <> Nothing) Then
                        If (ReposDate.ToString <> "01-01-0001") Then
                            _FTPtnDate = ReposDate.ToString
                        End If
                    Else
                        _FTPtnDate = .GetRowCellValue(.FocusedRowHandle, "FTPTNDate").ToString()
                    End If

                    NewData = _PatternType
                End If

                If FieldName = "FTPtnNote" Then
                    _PatternType = ReposPatternType.ToString()
                    _FTPtnNote = CType(sender, DevExpress.XtraEditors.TextEdit).Text
                    If (ReposDate.ToString <> Nothing) Then
                        If (ReposDate.ToString <> "01-01-0001") Then
                            _FTPtnDate = ReposDate.ToString
                        End If
                    Else
                        _FTPtnDate = .GetRowCellValue(.FocusedRowHandle, "FTPTNDate").ToString()
                    End If
                    NewData = _FTPtnNote
                End If

                If FieldName = "FTPTNDate" Then
                    _PatternType = ReposPatternType.ToString()
                    _FTPtnNote = ReposFTPtnNote.ToString()
                    _FTPtnDate = (HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime))

                    If (_FTPtnDate = "01-01-0001" Or _FTPtnDate = "12:00:00 AM" Or _FTPtnDate = "0001/01/01" Or
                        _FTPtnDate = "" Or _FTPtnDate = Nothing) Then
                        Exit Sub
                    End If
                    NewData = _FTPtnDate
                End If


                If NewData <> GridDataBefore Then
                    If (_PatternChanged.Exists(Function(p) p.Job = .GetRowCellValue(.FocusedRowHandle, "Job").ToString())) Then
                        For Each p As PatternChanged In _PatternChanged
                            p.Job = .GetRowCellValue(.FocusedRowHandle, "Job").ToString()
                            _PatternChanged.Remove(p)
                            Exit For
                        Next
                    End If

                    Dim _p As PatternChanged = New PatternChanged
                    _p.Job = .GetRowCellValue(.FocusedRowHandle, "Job").ToString()
                    _p.PTTypeId = .GetRowCellValue(.FocusedRowHandle, "FTPatternType_Hide").ToString()
                    _p.GrpTypeId = .GetRowCellValue(.FocusedRowHandle, "FTPtnGrpName_Hide").ToString()
                    _p.Leadtime = .GetRowCellValue(.FocusedRowHandle, "FTLeadTime").ToString()
                    If (_FTPtnDate = "01-01-0001" Or _FTPtnDate = "12:00:00 AM" Or _FTPtnDate = "0001/01/01" Or
                        _FTPtnDate = "" Or _FTPtnDate = Nothing) Then
                        _p.PTDate = _FTPtnDate
                    Else
                        _p.PTDate = Nothing
                    End If
                    _p.FTPtnNote = _FTPtnNote

                    _PatternChanged.Add(_p)

                End If

            End With

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub ogvoperation_CellMerge(sender As Object, e As CellMergeEventArgs)
        Try
            With Me.ogvPattern
                Select Case e.Column.FieldName
                    Case "FTSMPOrderBy", "FTMerTeamCode", "FTCustName", "FTCustCode", "FTGenderCode", "FTCustomerTeam", "FNOrderSampleType", "FNSMPPrototypeNo", "FNSMPSam", "FNSMPOrderType", "FTSeasonCode", "FTStyleName", "FTStyleCode", "FTStateReceiptDate", "FDSMPOrderDate"
                        If ("" & .GetRowCellValue(e.RowHandle1, "Job").ToString = "" & .GetRowCellValue(e.RowHandle2, "Job").ToString) _
                             And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "Job"
                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "MerRemark"
                        e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                    Case Else
                        e.Merge = False
                        e.Handled = True

                End Select

            End With

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Sub



    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        Dim i As Integer = 0
        If _PatternChanged.Count > 0 Then
            For Each _p As PatternChanged In _PatternChanged
                SavePatternData(_p)
                i = i + 1
            Next
            If (i = _PatternChanged.Count) Then
                HI.MG.ShowMsg.mInfo("บันทึกข้อมูล Pattern เรียบร้อย", 1000000003, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Information)
                _PatternChanged.Clear()
                Call LoadOrderProdDetail()
            End If
        Else
            HI.MG.ShowMsg.mInfo("ไม่มีข้อมูลแก้ไข !!!", 1000000005, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Warning)
        End If
        _Spls.Close()

    End Sub

    Private Function SavePatternData(p As PatternChanged)
        Dim cmdstring As String = ""
        cmdstring = "BEGIN"
        cmdstring &= vbCrLf & " If EXISTS(SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder WHERE FTOrderNo = '" & p.Job & "')  "
        cmdstring &= vbCrLf
        cmdstring &= vbCrLf & " BEGIN"
        cmdstring &= vbCrLf & "   UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder Set "
        cmdstring &= vbCrLf & "   FNHSysPTNTypeId ='" & p.PTTypeId & "'"
        cmdstring &= vbCrLf & "   , FNHSysPTNGrpTypeId = '" & p.GrpTypeId & "'"
        cmdstring &= vbCrLf & "   , TPTNOrderBy = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        cmdstring &= vbCrLf & "   , FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        cmdstring &= vbCrLf & "   , FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
        cmdstring &= vbCrLf & "   , FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & " "
        If (p.PTDate.ToString() <> "01-01-0001" Or p.PTDate.ToString() <> "12:00:00 AM" Or p.PTDate.ToString() <> "" Or p.PTDate.ToString() <> "0001/01/01") Then
            cmdstring &= vbCrLf & "   , FTPTNDate = '" & HI.UL.ULDate.ConvertEnDB(p.PTDate) & "'"
        End If
        cmdstring &= vbCrLf & "   , FTPtnNote = '" & p.FTPtnNote & "' "
        cmdstring &= vbCrLf & "   WHERE FTOrderNo = '" & p.Job & "' "
        cmdstring &= vbCrLf & " END"
        cmdstring &= vbCrLf
        cmdstring &= vbCrLf & "ELSE"
        cmdstring &= vbCrLf
        cmdstring &= vbCrLf & " BEGIN"
        cmdstring &= vbCrLf & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPTNOrder  "
        If (p.PTDate.ToString() <> "01-01-0001" Or p.PTDate.ToString() <> "12:00:00 AM" Or p.PTDate.ToString() <> "" Or p.PTDate.ToString() <> "0001/01/01") Then
            cmdstring &= vbCrLf & "   (FTOrderNo, TPTNOrderBy, FNHSysPTNTypeId, FNHSysPTNGrpTypeId, FTPtnNote, FTPTNDate, FTInsUser, FDInsDate, FTInsTime) "
        Else
            cmdstring &= vbCrLf & "   (FTOrderNo, TPTNOrderBy, FNHSysPTNTypeId, FNHSysPTNGrpTypeId, FTPtnNote, FTInsUser, FDInsDate, FTInsTime) "
        End If

        cmdstring &= vbCrLf & "   VALUES "
        cmdstring &= vbCrLf & "   ( '" & p.Job & "'"
        cmdstring &= vbCrLf & "   , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        cmdstring &= vbCrLf & "   , '" & p.PTTypeId & "'"
        cmdstring &= vbCrLf & "   , '" & p.GrpTypeId & "'"
        cmdstring &= vbCrLf & "   , '" & p.FTPtnNote & "'"
        If (p.PTDate.ToString() <> "01-01-0001" Or p.PTDate.ToString() <> "12:00:00 AM" Or p.PTDate.ToString() <> "" Or p.PTDate.ToString() <> "0001/01/01") Then
            cmdstring &= vbCrLf & "   , '" & HI.UL.ULDate.ConvertEnDB(p.PTDate) & "'"
        End If
        cmdstring &= vbCrLf & "   , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        cmdstring &= vbCrLf & "   , " & HI.UL.ULDate.FormatDateDB
        cmdstring &= vbCrLf & "   , " & HI.UL.ULDate.FormatTimeDB & " )"
        cmdstring &= vbCrLf & " END"
        cmdstring &= vbCrLf
        cmdstring &= vbCrLf & "END"

        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) = False Then

        End If
    End Function



    Private Sub ogvPattern_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvPattern.RowStyle
        Try
            With Me.ogvPattern
                If (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 2)) Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                ElseIf (Val(.GetRowCellValue(e.RowHandle, "PTStatus") = "F")) Then
                    e.Appearance.BackColor = System.Drawing.Color.LightGreen
                    'ElseIf (Val(.GetRowCellValue(e.RowHandle, "OrderStatus_Hide") = 0)) Then
                    '    e.Appearance.ForeColor = System.Drawing.Color.Blue
                End If
            End With

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

End Class



Public Class PatternChanged
    Public Property Job As String
    Public Property PTTypeId As String
    Public Property GrpTypeId As String
    Public Property Leadtime As String
    Public Property PTDate As String
    Public Property FTPtnNote As String


    Public Sub New()

    End Sub

    Public Sub New(ByVal Job As String, ByVal PTTypeId As String, ByVal GrpTypeId As String, ByVal Leadtime As String, ByVal PTDate As String, ByVal Note As String)
        Job = Job
        PTTypeId = PTTypeId
        GrpTypeId = GrpTypeId
        Leadtime = Leadtime
        PTDate = PTDate
        FTPtnNote = FTPtnNote
    End Sub
End Class