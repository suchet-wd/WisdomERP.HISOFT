Public Class wCompensationFundCmpAdjust



    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

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

#End Region

#Region "Procedure"

  
    Private Function LoadDataInfo(Spls As HI.TL.SplashScreen) As Boolean
        Dim _Qry As String = ""

        Dim _FTYear As String = Me.FTDateStart.Text

        _Qry = "   Select X.FNMonthSeq"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  ,X.FTNameTH AS FTMonthName "
        Else
            _Qry &= vbCrLf & "  ,X.FTNameEN	 AS FTMonthName "
        End If

        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNTotalEmp,0)) AS FNTotalEmp"
        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpDMinSalary,0)) AS FNEmpDMinSalary"
        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpMMinSalary,0)) AS FNEmpMMinSalary"
        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpDTotalSalary,0)) AS FNEmpDTotalSalary"
        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpMTotalSalary,0)) AS FNEmpMTotalSalary"
        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpDTotalSalary,0) +ISNULL(T.FNEmpMTotalSalary,0)) AS FNTotalSalary"

        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpOverTotalSalary,0)) AS FNEmpOverTotalSalary"
        _Qry &= vbCrLf & "  , SUM((ISNULL(T.FNEmpDTotalSalary,0) + ISNULL(T.FNEmpMTotalSalary,0))-ISNULL(T.FNEmpOverTotalSalary,0)) AS FNTotalPaySalary"

        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpOTTotalSalary,0)) AS FNEmpOTTotalSalary"
        _Qry &= vbCrLf & "  , SUM(ISNULL(T.FNEmpBonusTotalSalary,0))  AS FNEmpBonusTotalSalary"

        _Qry &= vbCrLf & "FROM("
        _Qry &= vbCrLf & " SELECT FNListIndex AS FNMonthSeq, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNMonth')"
        _Qry &= vbCrLf & ") AS X LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( SELECT * "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTCompensationFundCmpAdjust AS T WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE T.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
        _Qry &= vbCrLf & " AND T.FTYear='" & _FTYear & "'"
        _Qry &= vbCrLf & " ) AS T ON X.FNMonthSeq =T.FNMonth "
        _Qry &= vbCrLf & " GROUP BY X.FNMonthSeq"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  ,X.FTNameTH  "
        Else
            _Qry &= vbCrLf & "  ,X.FTNameEN	  "
        End If

        _Qry &= vbCrLf & "  ORDER BY X.FNMonthSeq"

        Dim _dtdata As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        With Me.ogc
            .DataSource = _dtdata.Copy
            ogv.ExpandAllGroups()
            ogv.RefreshData()
        End With

        _dtdata.Dispose()


        Return True
    End Function
#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        With ogv

            .Columns("FNEmpDTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpDTotalSalary")
            .Columns("FNEmpDTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNEmpMTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpMTotalSalary")
            .Columns("FNEmpMTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalSalary")
            .Columns("FNTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNEmpOverTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpOverTotalSalary")
            .Columns("FNEmpOverTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNTotalPaySalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalPaySalary")
            .Columns("FNTotalPaySalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNEmpOTTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpOTTotalSalary")
            .Columns("FNEmpOTTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNEmpBonusTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpBonusTotalSalary")
            .Columns("FNEmpBonusTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"


            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = False
            .ExpandAllGroups()

            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub

    Private Function SaveData() As Boolean

        Dim dt As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String

         

            For Each R As DataRow In dt.Select("FNMonthSeq>0 ", "FNMonthSeq")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTCompensationFundCmpAdjust"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNTotalEmp=" & Val(R!FNTotalEmp.ToString) & ""
                _Str &= vbCrLf & " , FNEmpDMinSalary=" & Val(R!FNEmpDMinSalary.ToString) & ""
                _Str &= vbCrLf & " , FNEmpMMinSalary=" & Val(R!FNEmpMMinSalary.ToString) & ""
                _Str &= vbCrLf & " , FNEmpDTotalSalary=" & Val(R!FNEmpDTotalSalary.ToString) & ""
                _Str &= vbCrLf & " , FNEmpMTotalSalary=" & Val(R!FNEmpMTotalSalary.ToString) & ""
                _Str &= vbCrLf & " , FNEmpOverTotalSalary=" & Val(R!FNEmpOverTotalSalary.ToString) & ""
                _Str &= vbCrLf & " , FNEmpOTTotalSalary=" & Val(R!FNEmpOTTotalSalary.ToString) & ""
                _Str &= vbCrLf & " , FNEmpBonusTotalSalary=" & Val(R!FNEmpBonusTotalSalary.ToString) & ""
                _Str &= vbCrLf & "   WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & "       AND FTYear='" & HI.UL.ULF.rpQuoted(Me.FTDateStart.Text) & "'"
                _Str &= vbCrLf & "       AND FNMonth=" & Val(R!FNMonthSeq.ToString) & ""

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTCompensationFundCmpAdjust"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysCmpId,FTYear, FNMonth, FNTotalEmp, FNEmpDMinSalary, FNEmpMMinSalary,FNEmpDTotalSalary,FNEmpMTotalSalary,FNEmpOverTotalSalary,FNEmpOTTotalSalary,FNEmpBonusTotalSalary"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"
                    _Str &= vbCrLf & " ," & Val(R!FNMonthSeq.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNTotalEmp.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpDMinSalary.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpMMinSalary.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpDTotalSalary.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpMTotalSalary.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpOverTotalSalary.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpOTTotalSalary.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNEmpBonusTotalSalary.ToString) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

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

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTCompensationFundCmpAdjust WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND FTYear='" & HI.UL.ULF.rpQuoted(Me.FTDateStart.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

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

#End Region

#Region "General"
    Private Sub wEmployeeLeaveOfYear_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()

        ogv.OptionsView.ShowAutoFilterRow = False
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)

    End Sub

    Private Function VerifyData() As Boolean
        If Me.FTDateStart.Text <> "" Then


            Return True

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุปีที่ต้องการสรุปข้อมูล !!!", 1512130574, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click


        HI.TL.HandlerControl.ClearControl(Me)
        'HI.TL.HandlerControl.ClearControl(ogc)
        ogc.DataSource = Nothing
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then


                Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")
                Me.LoadDataInfo(_Spls)
                Me.otbmain.SelectedTabPageIndex = 0
                _Spls.Close()


        End If

    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub FTDateStart_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FTDateStart.EditValueChanging
        Try
            Me.ogc.DataSource = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTDateStart_TextChanged(sender As Object, e As EventArgs) Handles FTDateStart.TextChanged
        Try
            Me.ogc.DataSource = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If VerifyData() Then
            If Not Me.ogc.DataSource Is Nothing Then
                If Me.ogv.RowCount > 0 Then

                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then
                        Dim _Spls As New HI.TL.SplashScreen("Saving data...   Please Wait  ")
                        If Me.SaveData() Then
                            Me.LoadDataInfo(_Spls)
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Else
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        End If
                        Me.otbmain.SelectedTabPageIndex = 0

                    End If

                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

           


        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If VerifyData() Then
            If Not Me.ogc.DataSource Is Nothing Then
                If Me.ogv.RowCount > 0 Then

                    If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete) = True Then
                        Dim _Spls As New HI.TL.SplashScreen("Deleting data...   Please Wait  ")
                        If Me.DeleteData() Then
                            Me.LoadDataInfo(_Spls)
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Else
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        End If
                        Me.otbmain.SelectedTabPageIndex = 0

                    End If

                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        End If

    End Sub

    Private Sub ReposSalary_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposSalary.EditValueChanging
        Try

            Dim _Val As Double = Val(e.NewValue)
            With Me.ogv
                .SetFocusedRowCellValue(.FocusedColumn.FieldName, _Val)

            End With

            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With

            With Me.ogv
                .SetFocusedRowCellValue("FNTotalSalary", Val("" & .GetFocusedRowCellValue("FNEmpMTotalSalary")) + Val("" & .GetFocusedRowCellValue("FNEmpDTotalSalary")))
                .SetFocusedRowCellValue("FNTotalPaySalary", Val("" & .GetFocusedRowCellValue("FNTotalSalary")) - Val("" & .GetFocusedRowCellValue("FNEmpOverTotalSalary")))
            End With
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposOverTotalSalary_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposOverTotalSalary.EditValueChanging
        Try
            Dim _Val As Double = Val(e.NewValue)
            With Me.ogv
                .SetFocusedRowCellValue(.FocusedColumn.FieldName, _Val)

            End With

            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With

            With Me.ogv

                .SetFocusedRowCellValue("FNTotalPaySalary", Val("" & .GetFocusedRowCellValue("FNTotalSalary")) - Val("" & .GetFocusedRowCellValue("FNEmpOverTotalSalary")))
            End With
            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTDateStart_EditValueChanged(sender As Object, e As EventArgs) Handles FTDateStart.EditValueChanged

    End Sub
End Class