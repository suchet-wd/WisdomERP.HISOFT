Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class wProdEaringTracking_Incentive

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _StateQtyBySPM As Boolean = False  ' get Qty by Super market

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Call InitGrid()
    End Sub



#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
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
            .Columns("FTCmpCode").Group()
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


#Region "Procedure"

    Private Sub loaddata()
        Dim _Qry As String
        Dim _dt As DataTable
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.EaringIncentiveActual " & Integer.Parse(Me.FNHSysCmpId.Properties.Tag.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' ,'" & HI.UL.ULDate.ConvertEnDB(FTToDate.Text) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcdetail.DataSource = _dt
            Me.ogvdetail.ExpandAllGroups()
            ' Call InitialGridMergCell()
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub InitialGridMergCell()
        For Each c As GridColumn In ogvdetail.Columns
            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTPORef", "FTCmpCode", "FTSectCode", "FTUnitSectCode", "FTCustCode", "FTSeasonCode", "FNAVEFF", "EmpCount", "FNTimeMin", "FNOT1Min", "FNNetCM", "FNBudget", "Earningperline", "FNLostEarnning", "FNLostEarnningPer"

                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysCmpId.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartDate.Text <> "" Then
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
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)
            'StateCal = False
            '_StateQtyBySPM = StateQtyBySPM()

        Catch ex As Exception
        End Try
    End Sub

    Private Function StateQtyBySPM() As Boolean
        Try
            Dim _Cmd As String = " Select  Top 1  isnull(FTStateProdSMKToCutQty,0) As FTStateProdSMKToCutQty  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEConfig With(NOLOCK)"
            Return Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY, "0")) = 1
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Dim _Qry As String = ""
            '_Qry = "Select TOP 1 FTStateProdSMKToCutQty "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig As S With(NOLOCK)"
            '_Qry &= vbCrLf & " WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) & "'"
            '_FTStateProdSMKToCutQty = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")
            Call loaddata()
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



    'Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
    '    Try
    '        If VerifyData() Then
    '            If SaveData() Then
    '                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '            Else
    '                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

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
                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFEarnning "
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FNEEFQty=" & Double.Parse("0" & R!FNAVEFF.ToString)
                    _Cmd &= vbCrLf & "where FNHSysUnitSectId=" & Integer.Parse(R!FNHSysUnitSectId.ToString)
                    _Cmd &= vbCrLf & "and FTCalDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFEarnning "
                        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTCalDate, FNHSysUnitSectId, FNEEFQty)"
                        _Cmd &= vbCrLf & "Select  '" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                        _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysUnitSectId.ToString)
                        _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAVEFF.ToString)
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

    'Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
    '    Try
    '        If VerifyData() Then
    '            If Delete() Then
    '                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
    '                Call loaddata()
    '            Else
    '                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

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
                    _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFEarnning WHERE FTCalDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'   And FNHSysUnitSectId =" & Integer.Parse("0" & R!FNHSysUnitSectId.ToString)
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                Next

            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THREFFEarnning WHERE date '" & Me.FTStartDate.Text & "',',cmpcode=" & Me.FNHSysCmpId.Text & ", user =" & HI.ST.UserInfo.UserName)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerifyData() Then
                If CalsBonus() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Function CalsBonus() As Boolean
        Try
            Dim _Cmd As String = ""
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                'HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
                'HI.Conn.SQLConn.SqlConnectionOpen()
                'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                For Each R As DataRow In .Rows

                    '_Cmd = "SELECT T.FNHSysEmpID, T.FTDateTrans,  (T.FNTimeMin+ T.FNOT1Min) AS FNTime , max( (T.FNTimeMin+ T.FNOT1Min) ) over(partition by E.FNHSysUnitSectId ) AS  FNMaxTimeUnitSect "
                    '_Cmd &= vbCrLf & " , convert(numeric(18,2) , ((T.FNTimeMin+ T.FNOT1Min) * " & Double.Parse("0" & R!FNIncentiveAmtPaid.ToString) & ") / max( (T.FNTimeMin+ T.FNOT1Min)  ) over(partition by E.FNHSysUnitSectId ))  as FNBonusAmt "
                    '_Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN"
                    '_Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID "
                    '_Cmd &= vbCrLf & "WHERE  (T.FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "') AND (E.FNHSysUnitSectId = " & Integer.Parse("0" & R!FNHSysUnitSectId.ToString) & ")"

                    _Cmd = "Exec   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[EarnigIncentiveActual_GETTime] " & Integer.Parse("0" & R!FNHSysUnitSectId.ToString) & "  , '" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "'"

                    For Each X As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows
                        Dim _FNIncentiveAmtPaid As Double = 0
                        _FNIncentiveAmtPaid = (Double.Parse("0" & R!FNIncentiveAmtPaid.ToString) * Double.Parse("0" & X!FNTime.ToString)) / Double.Parse("0" & X!FNMaxTimeUnitSect.ToString)

                        _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily_Bonus "
                        _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & ",FTUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FNAmt=" & _FNIncentiveAmtPaid
                        _Cmd &= vbCrLf & "where FNHSysEmpID=" & Integer.Parse(X!FNHSysEmpID.ToString)
                        _Cmd &= vbCrLf & "and FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "'"

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR) = False Then

                            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily_Bonus"
                            _Cmd &= vbCrLf & "(FTInsUser, FTInsDate, FTInsTime, FNHSysEmpID, FTDateTrans, FNAmt)"
                            _Cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & "," & Integer.Parse(X!FNHSysEmpID.ToString)
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTCalDate.ToString) & "'"
                            _Cmd &= vbCrLf & "," & _FNIncentiveAmtPaid

                            If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR) = False Then
                                'HI.Conn.SQLConn.Tran.Rollback()
                                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            End If

                        End If

                    Next
                Next

            End With

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception

            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False

        End Try

    End Function
End Class