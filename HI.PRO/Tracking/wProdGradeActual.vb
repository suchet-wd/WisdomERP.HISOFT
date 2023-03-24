Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors.Controls

Public Class wProdGradeActual

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _StateQtyBySPM As Boolean = False  ' get Qty by Super market

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()

    End Sub
#Region "Initial Grid"

    Private Sub InitGrid()
        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"
        'Dim sFieldSum As String = "FNLateNormalMin|FNLateNormalCut|FNAbsent|FNCutAbsent|FNAbsentCut"
        'Dim sFieldGrpCount As String = "FTEmpCode"
        'Dim sFieldGrpSum As String = "FNEmpWork|FNLateNormalMin|FNLateNormalCut|FNAbsent|FNCutAbsent|FNAbsentCut"

        ''T.FNLateNormalMin, T.FNLateNormalCut
        'Dim sFieldCustomSum As String = "FNTime|FNOT1|FNOT1_5|FNOT2|FNOT3|FNOT4"
        'Dim sFieldCustomGrpSum As String = ""

        With ogvdetail
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTSectCode").Group()

            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            'For Each Str As String In sFieldCustomSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            'For Each Str As String In sFieldSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
            '    End If
            'Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
            '    End If
            'Next

            '.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            '.OptionsView.ShowFooter = True
            '.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            '.OptionsView.ShowGroupPanel = True
            .ExpandAllGroups()
            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            Dim _Cmd As String = ""
            _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.ProGradeActual " & Integer.Parse(Me.FNHSysCmpId.Properties.Tag.ToString) & " ,'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTToDate.Text) & "'"
            Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
            Me.ogvdetail.ExpandAllGroups()
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub



    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysCmpId.Text <> "" Then
            _Pass = True
        End If
        If Me.FTStartDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTToDate.Text <> "" Then
            _Pass = True
        End If


        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvdetail)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Catch ex As Exception
        End Try
    End Sub



    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerifyData() Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            ' FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTCalDate, FNHSysUnitSectId, FN5SPer, FNReworkPer, FNLeanPer, FNGradeLevel, FTStateMetal

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFIncentive_Grade "
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FN5SPer=" & Double.Parse("0" & R!FN5SPer.ToString)
                    _Cmd &= vbCrLf & ",FNReworkPer=" & Double.Parse("0" & R!FNReworkPer.ToString)
                    _Cmd &= vbCrLf & ",FNLeanPer=" & Double.Parse("0" & R!FNLeanPer.ToString)
                    _Cmd &= vbCrLf & ",FNGradeLevel='" & R!FNGradeLevel_Hide.ToString & "'"
                    _Cmd &= vbCrLf & ",FTStateMetal='" & R!FTStateMetal_Hide.ToString & "'"
                    _Cmd &= vbCrLf & "where FNHSysUnitSectId=" & Integer.Parse(R!FNHSysUnitSectId.ToString)
                    _Cmd &= vbCrLf & "and FTCalDate='" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFIncentive_Grade "
                        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FN5SPer, FNReworkPer, FNLeanPer, FNGradeLevel, FTStateMetal)"
                        _Cmd &= vbCrLf & "Select  '" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "'"
                        _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysUnitSectId.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FN5SPer.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNReworkPer.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNLeanPer.ToString)
                        _Cmd &= vbCrLf & ",'" & R!FNGradeLevel_Hide.ToString & "'"
                        _Cmd &= vbCrLf & ",'" & R!FTStateMetal_Hide.ToString & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                Next

            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If VerifyData() Then
                If Delete() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Call LoadData()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function Delete() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFIncentive_Grade WHERE FTCalDate='" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "'   And FNHSysUnitSectId =" & Integer.Parse("0" & R!FNHSysUnitSectId.ToString)
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                Next

            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFIncentive_Grade WHERE date '" & Me.FTStartDate.Text & "','" & Me.FTToDate.Text & "',cmpcode=" & Me.FNHSysCmpId.Text & ", user =" & HI.ST.UserInfo.UserName)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function CalsGrade(_StateRework As Boolean, _State5s As Boolean, _StateLean As Boolean, _StateMetal As Boolean, _Newvalue As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _reworkper As Double = 0
            Dim _Metal As String = ""
            Dim _5s As Double = 0
            Dim _Lean As Double = 0
            Dim _Grade As String = ""

            With Me.ogvdetail
                _reworkper = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNReworkPer").ToString)
                _Metal = HI.TL.CboList.GetIndexByText("FNStateMetal", .GetRowCellValue(.FocusedRowHandle, "FTStateMetal").ToString)
                _5s = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FN5SPer").ToString)
                _Lean = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNLeanPer").ToString)
                Select Case True
                    Case _StateRework
                        _reworkper = Double.Parse(_Newvalue)
                    Case _State5s
                        _5s = Double.Parse(_Newvalue)
                    Case _StateLean
                        _Lean = Double.Parse(_Newvalue)
                    Case _StateMetal
                        _Metal = HI.TL.CboList.GetIndexByText("FNStateMetal", _Newvalue)
                End Select


                _Cmd = "Select top 1  FTStateGrade "
                _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRIncentiveGrade With(nolock)"
                _Cmd &= vbCrLf & "where (FNReworkPer <= " & _reworkper & " And FNReworkPerTo >= " & _reworkper & ")"
                _Cmd &= vbCrLf & "And FTStateMetalCheck =" & Integer.Parse(_Metal)
                _Cmd &= vbCrLf & "And (FN5SPer <= " & _5s & " And FN5SPerTo > " & _5s & ") "
                _Cmd &= vbCrLf & "And (FNLeanPer <= " & _Lean & " And FNLeanPerTo > " & _Lean & " )"
                _Cmd &= vbCrLf & "And FTStateMetalCheck = 0"
                _Grade = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0")


                .SetRowCellValue(.FocusedRowHandle, "FNGradeLevel_Hide", _Grade)

                _Cmd = "Select  top 1    FNListIndex, FTNameTH, FTNameEN"
                _Cmd &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
                _Cmd &= vbCrLf & "WHERE     (FTListName = 'FNGradeLevel')"
                _Cmd &= vbCrLf & " AND FNListIndex=" & Integer.Parse(_Grade)
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SYSTEM)
                If _oDt.Rows.Count > 0 Then
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        .SetRowCellValue(.FocusedRowHandle, "FNGradeLevel", _oDt.Rows(0) !FTNameTH.ToString)
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FNGradeLevel", _oDt.Rows(0) !FTNameEN.ToString)
                    End If

                End If
            End With


        Catch ex As Exception

        End Try
    End Function

    Private Sub RepositoryItemCFNLeanPer_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCFNLeanPer.EditValueChanging
        Try
            Call CalsGrade(False, False, True, False, e.NewValue.ToString)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCFNReworkPer_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCFNReworkPer.EditValueChanging
        Try
            Call CalsGrade(True, False, False, False, e.NewValue.ToString)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemFN5SPer_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemFN5SPer.EditValueChanging
        Try
            Call CalsGrade(False, True, False, False, e.NewValue.ToString)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemFNStateMetal_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemFNStateMetal.EditValueChanging
        Try
            Call CalsGrade(False, False, False, True, e.NewValue.ToString)
        Catch ex As Exception

        End Try
    End Sub
End Class