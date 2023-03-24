Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Columns
Imports System.IO
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports System.Globalization

Public Class wEmployeeTrainning

    Private _FormAddEmp As wAddEmployeeTrainning
    Private _Timstr As String
    Private _FormLoad As Boolean = True
    Private _T1 As Integer
    Private _TimeGrid As Integer
    Private ChkBtn As Integer = 1
    Private _Time1 As String = ""
    Private _Time2 As String = ""
    Private GTrainNameActiveRemoveList As Boolean
    Private GEmpNameActiveRemoveList As Boolean
    Private GBMultiActiveRemoveList As Boolean
    Private _ColCount As Integer = 0
    Private _ColCountMulti As Integer = 0
    Private _LoadLang As Boolean = False
    Private _ChkDatatableMaster As Boolean = False
    Private _ChkAddEmpMulti As Boolean = False
    Private _Gencol As Boolean = False
    Private _colfocus As String = ""
    Private _colSkip As String = ""
    Private _colSkipTopic As String = ""
    Private dtHead As DataTable
    Private dtcol As DataTable
    Private dtRank As DataTable
    Private _dtMulti As DataTable
    Private _StatePass As Integer
    Private _DateLec As String = ""
    Private _FormLoadDt As Boolean
    Private _Transac As Boolean = False
    Private _SectType As Boolean = False


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _FormLoad = True
        Call SetGridIMG()
        ' Add any initialization after the InitializeComponent() call.

        With RepFDStartDate
            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Leave, AddressOf Date_Leave
            'AddHandler .EditValueChanged, AddressOf Date_Changed

            'AddHandler .Click, AddressOf Date_GotFocused
        End With

        With RepFDEndDate
            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            AddHandler .Leave, AddressOf Date_Leave
            'AddHandler .Click, AddressOf Date_GotFocused
        End With
        'Call LoadEmpTrainning(FTDocNo.Text)
        Call LoadTrainName()

        _FormAddEmp = New wAddEmployeeTrainning
        _FormAddEmp.Tag = "|" & _FormAddEmp.Name & "|" & _FormAddEmp.Name

        HI.TL.HandlerControl.AddHandlerObj(_FormAddEmp)


        Dim _SystemLang As New ST.SysLanguage
        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _FormAddEmp.Name.ToString.Trim, _FormAddEmp)
        Catch ex As Exception
        Finally
        End Try
        _FormLoadDt = True



    End Sub

#Region "Property"

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

#Region "Property Private"
    Private _Combo As Object
    Private Property PComBo As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
        Get
            Return _Combo
        End Get
        Set(value As DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
            _Combo = value
        End Set
    End Property

    Private _QryEmp As String
    Private Property QryEmp As String
        Get
            Return _QryEmp
        End Get
        Set(value As String)
            _QryEmp = value
        End Set
    End Property
#End Region


#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.VerrifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If FNTrainType.SelectedIndex = 2 Then
                If SaveData() Then
                    If SaveEmpMulti() Then
                        SaveTrainName()
                        LoadTrainInfo(Me.FTDocNo.Text)
                        LoadDataMultiSkill()
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                        'LoadDataDetailMulti
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

            Else
                If Me.SaveData() Then
                    If Me.SaveTrainName Then
                        If Me.SaveEmpdata Then
                            If Me.SaveImage Then
                                _Spls.Close()
                                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                _Transac = True
                                Me.LoadTrainInfo(Me.FTDocNo.Text)
                                Me.LoadTrainName()
                                'Me.LoadEmpTrainning(Me.FTDocNo.Text)
                                Me.LoadImg()
                            Else
                                _Spls.Close()
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, "เซฟรูปไม่ได้")
                            End If
                        Else
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        End If
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If

        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then
                    'HI.TL.HandlerControl.ClearControl(Me)
                    'For i As Integer = ogbvSkill.RowCount - 1 To 0 Step -1
                    '    CType(ogcSkill.DataSource, DataTable).Rows.RemoveAt(i)
                    'Next
                    Me.ocmclear.PerformClick()
                    _Spls.Close()


                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    'Me.LoadTrainInfo(Me.FTDocNo.Text)
                    'Me.LoadTrainName()
                    'Me.RemoveColTopicEmp()
                    'Me.GenGridBannedTopicEmp()
                    'Me.LoadEmpTrainning(Me.FTDocNo.Text)
                    'Me.LoadImg()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        'Me.FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)
        TotalTime.Text = "00:00"
        'For i As Integer = ogv.RowCount - 1 To 0 Step -1
        '    CType(Me.ogc.DataSource, DataTable).Rows.RemoveAt(i)
        'Next
        ogcSkill.DataSource = Nothing
        ogcTopicEmp.DataSource = Nothing
        'ogcTrainer.DataSource = Nothing
        Try
            For i As Integer = ogvTrainer.RowCount - 1 To 0 Step -1
                CType(Me.ogcTrainer.DataSource, DataTable).Rows.RemoveAt(i)
            Next
            'For i As Integer = ogbvSkill.RowCount - 1 To 0 Step -1
            '    CType(Me.ogcSkill.DataSource, DataTable).Rows.RemoveAt(i)
            'Next

            'RemoveColTopicEmp()
            With ogvTopicEmp
                'For i As Integer = .RowCount - 1 To 0 Step -1
                '    CType(Me.ogcTopicEmp.DataSource, DataTable).Rows.RemoveAt(i)
                'Next
                For i As Integer = .Bands.Count - 1 To 0 Step -1
                    .Bands.RemoveAt(i)
                Next
            End With

            Me.ListNameIMG.Items.Clear()
        Catch ex As Exception

        End Try


        'Me.otb.SelectedTabPageIndex = 0
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "Procedure"

    Private Function SaveData() As Boolean
        Try
            Dim StrLastEffDate As String = ""
            Dim _CurrenAmt As Double = 0
            Dim _NewAmt As Double = 0
            Dim tSeqNo As String = "0"

            Dim _Qry As String = ""


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrain SET "
                _Qry &= vbCrLf & " FDDocDate='" & HI.UL.ULDate.ConvertEnDB(FDDocDate.Text) & "' "
                _Qry &= vbCrLf & ", FTTrainCode='" & HI.UL.ULF.rpQuoted(Me.FTTrainCode.Text) & "' "
                _Qry &= vbCrLf & ", FNTrainType='" & HI.UL.ULF.rpQuoted(Me.FNTrainType.SelectedIndex.ToString) & "'  "
                _Qry &= vbCrLf & ", FTTrainer='" & HI.UL.ULF.rpQuoted(FTTrainer.Text) & "'"
                _Qry &= vbCrLf & ", FDDateBegin='" & HI.UL.ULDate.ConvertEnDB(FDDateBegin.Text) & "' "
                _Qry &= vbCrLf & ", FDDateEnd='" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "' "
                _Qry &= vbCrLf & ", FTLocation='" & HI.UL.ULF.rpQuoted(FTLocation.Text) & "' "
                _Qry &= vbCrLf & ", FCCostPerEmp=" & FCCostPerEmp.Value & " "
                _Qry &= vbCrLf & ", FTTrainNote='" & HI.UL.ULF.rpQuoted(FTTrainNote.Text) & "' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",FTStartTime ='" & HI.UL.ULF.rpQuoted(FTStartTime.Text) & "'"
                _Qry &= vbCrLf & ",FTEndTime='" & HI.UL.ULF.rpQuoted(FTEndTime.Text) & "'"
                _Qry &= vbCrLf & ",FTTotalHour='" & HI.UL.ULF.rpQuoted(TotalTime.Text) & "'"
                _Qry &= vbCrLf & ",FNTotalMinute=" & _T1 & ""
                _Qry &= vbCrLf & ",FNFixTrainTrainning=" & Me.FNFixTrainTrainning.SelectedIndex & ""
                _Qry &= vbCrLf & " WHERE  FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "' "

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrain(FTDocNo, FDDocDate, FTTrainCode, FNTrainType, FTTrainer, FDDateBegin, FDDateEnd,FTTrainNote, FCCostPerEmp,  FTLocation"
                    _Qry &= vbCrLf & ",FTInsUser, FDInsDate, FTInsTime,FTStartTime,FTEndTime,FTTotalHour,FNTotalMinute,FNFixTrainTrainning)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FDDocDate.Text) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTTrainCode.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FNTrainType.SelectedIndex.ToString) & "'  "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTrainer.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FDDateBegin.Text) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTTrainNote.Text) & "'," & FCCostPerEmp.Value & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTLocation.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTStartTime.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTEndTime.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(TotalTime.Text) & "'"
                    _Qry &= vbCrLf & "," & _T1 & ""
                    _Qry &= vbCrLf & "," & Me.FNFixTrainTrainning.SelectedIndex & ""

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

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
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveEmpdata() As Boolean
        Dim QryFix As String = ""
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try

            QryFix = "DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp WHERE FTDocNo='" & Me.FTDocNo.Text & "'"
            HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In CType(ogcTopicEmp.DataSource, DataTable).Rows
                For Each Col As DataColumn In CType(ogcTopicEmp.DataSource, DataTable).Columns
                    Select Case Col.ColumnName.ToString
                        Case "FNHSysEmpID", "FTEmpCode", "FTEmpName"
                        Case "FNTotalPre"
                            Try


                                'QryFix = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp"
                                'QryFix &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "',FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                'QryFix &= vbCrLf & ",FTEvaluate='" & R!FTState.ToString & "',FTTrainNote='" & R!FTTrainNote.ToString & "',FNPreTest=" & Val(R!FNTotalPre.ToString) & ""
                                'QryFix &= vbCrLf & ",FNPostTest=" & Val(R!FNTotalPost.ToString) & " WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""

                                'If HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                QryFix = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp"
                                    QryFix &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNHSysEmpID,FTEvaluate,FTTrainNote,FNPreTest,FNPostTest)"
                                    QryFix &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    QryFix &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDocNo.Text) & "'," & Val(R!FNHSysEmpID.ToString) & ""
                                    QryFix &= vbCrLf & ",'" & R!FTState.ToString & "','" & R!FTTrainNote.ToString & "'," & Val(R!FNTotalPre.ToString) & "," & Val(R!FNTotalPost.ToString) & ""
                                    If HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Return False
                                    End If
                                'End If
                                Exit For
                            Catch ex As Exception
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End Try

                            'Case "FNTotalPre"
                            '    QryFix = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp"
                            '    QryFix &= vbCrLf & "SET FNPostTest=" & Val(R!FNTotalPost.ToString) & " WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(Me.FTDocNo.Text) & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""
                            '    HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                        Case Else
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 3) = "Pre" Then

                                QryFix = "DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & " AND FNHSysTrainTopicId=" & Col.ColumnName.ToString.Substring(3) & " "
                                HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                                'QryFix = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic"
                                'QryFix &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                                'QryFix &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                'QryFix &= vbCrLf & ",FNPrePoint=" & Val(R.Item(Col.ColumnName)) & ""
                                'QryFix &= vbCrLf & "WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & " AND FNHSysTrainTopicId=" & Col.ColumnName.ToString.Substring(3) & " "
                                'If HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                QryFix = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic"
                                QryFix &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNHSysEmpID,FNHSysTrainTopicId,FNPrePoint)"
                                QryFix &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'"
                                QryFix &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                QryFix &= vbCrLf & ",'" & Me.FTDocNo.Text & "'"
                                QryFix &= vbCrLf & "," & Val(R!FNHSysEmpID.ToString) & ""
                                QryFix &= vbCrLf & "," & Val(Col.ColumnName.ToString.Substring(3)) & ""
                                QryFix &= vbCrLf & "," & Val(R.Item(Col.ColumnName.ToString)) & ""
                                If HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                                'End If
                            End If
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 4) = "Post" Then
                                QryFix = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic"
                                QryFix &= vbCrLf & "SET FNPostPoint=" & Val(R.Item(Col.ColumnName.ToString)) & ""
                                QryFix &= vbCrLf & "WHERE FNHSysTrainTopicId=" & Col.ColumnName.ToString.Substring(4) & " AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & " AND FTDocNo='" & Me.FTDocNo.Text & "'"
                                If HI.Conn.SQLConn.Execute_Tran(QryFix, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                            End If

                    End Select
                Next
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
            'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
            'HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End Try
    End Function

    Private Function SaveEmpMulti() As Boolean
        Dim Qry As String = ""
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            For Each R As DataRow In CType(ogcSkill.DataSource, DataTable).Rows
                For Each Col As DataColumn In CType(ogcSkill.DataSource, DataTable).Columns
                    Select Case Col.ColumnName.ToString
                        Case "FNHSysEmpID", "FTEmpName", "FTYearWork", "FTLeaveFlowPlan", "FTLeaveOutFlowPlan", "FTAbsent", "FTLate", "FTSumWorkStop",
                            "FNSummerySkillUP75s", "FTSummerySkillEMP25s", "FTSummerySkillEMP50s", "FTSummerySkillEMP100s", "FNSum0", "FNSum1", "FNSum2",
                            "cFNExcellence", "cFNGood", "cFNFair", "cFNBeginner"
                        Case Else

                            Qry = "DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix"
                            Qry &= vbCrLf & "WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & " AND FNHSysSkillMatrixId=" & Val(Col.ColumnName.ToString.Substring(0, 10)) & ""
                            HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                            'Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix"
                            'Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                            'Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                            'Qry &= vbCrLf & ",FNPoint=" & R.Item(Col.ColumnName.ToString) & ""
                            'Qry &= vbCrLf & "WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & " AND FNHSysSkillMatrixId=" & Val(Col.ColumnName.ToString.Substring(0, 10)) & ""
                            'If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix"
                                Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNHSysEmpID,FNHSysSkillMatrixId,FNPoint)"
                                Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'"
                                Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                Qry &= vbCrLf & ",'" & Me.FTDocNo.Text & "'," & Val(R!FNHSysEmpID.ToString) & "," & Val(Col.ColumnName.ToString.Substring(0, 10)) & "," & R.Item(Col.ColumnName.ToString) & ""
                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                            'End If
                    End Select
                Next
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            Return False
        End Try
    End Function


    Private Function SaveTrainName() As Boolean
        Dim Qry As String = ""
        Dim dt As DataTable = CType(ogcTrainer.DataSource, DataTable)

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            Qry = "DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer WHERE  FTDocNo='" & Me.FTDocNo.Text & "'"
            HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In dt.Rows
                Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer SET"
                Qry &= vbCrLf & "FTUpdUser='" & HI.ST.UserInfo.UserName & "',FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & ",FTTrainer='" & R!FTTrainerName.ToString & "',FDDateBegin='" & HI.UL.ULDate.ConvertEnDB(R!FDStartDate.ToString) & "'"
                Qry &= vbCrLf & ",FDDateEnd='" & HI.UL.ULDate.ConvertEnDB(R!FDEndDate.ToString) & "',FTStartTime='" & R!FTStartTime & "',FTEndTime='" & R!FTEndTime.ToString & "'"
                Qry &= vbCrLf & ",FTTotalHour='" & R!FTTotalHour.ToString & "',FNTotalMinute=" & Val(R!FNTotalMinute.ToString) & ""
                Qry &= vbCrLf & "WHERE FNSeq=" & Val(R!FNSeq.ToString) & " AND FTDocNo='" & Me.FTDocNo.Text & "'"

                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer"
                    Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNSeq,FTTrainer,FDDateBegin,FDDateEnd,FTStartTime,FTEndTime,FTTotalHour,FNTotalMinute)"
                    Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'"
                    Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    Qry &= vbCrLf & ",'" & Me.FTDocNo.Text & "'," & Val(R!FNSeq.ToString) & ",'" & R!FTTrainerName.ToString & "'"
                    Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDStartDate.ToString) & "','" & HI.UL.ULDate.ConvertEnDB(R!FDEndDate.ToString) & "'"
                    Qry &= vbCrLf & ",'" & R!FTStartTime & "','" & R!FTEndTime.ToString & "','" & R!FTTotalHour.ToString & "'," & Val(R!FNTotalMinute.ToString) & ""
                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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

        Dim StrLastEffDate As String = ""
        Dim _CurrenAmt As Double = 0
        Dim _NewAmt As Double = 0

        Dim _Qry As String = ""

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            If FNTrainType.SelectedIndex = 2 Then
                _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrain  "
                _Qry &= vbCrLf & " WHERE  FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "' "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainLecturer"
                _Qry &= vbCrLf & "WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix"
                _Qry &= vbCrLf & "WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            Else
                _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrain  "
                _Qry &= vbCrLf & " WHERE  FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "' "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainLecturer"
                _Qry &= vbCrLf & "WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainEmp  "
                _Qry &= vbCrLf & " WHERE  FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "' "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainImage"
                _Qry &= vbCrLf & " WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Qry = "DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainEmpTopic"
                _Qry &= vbCrLf & "WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
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

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTDocNo.Text <> "" Then
            If HI.UL.ULDate.CheckDate(FDDocDate.Text) <> "" Then
                If Me.FTTrainCode.Text <> "" And FTTrainCode.Properties.Tag.ToString <> "" Then
                    If Me.FNTrainType.SelectedIndex <> 2 Then
                        If HI.UL.ULDate.CheckDate(FDDateBegin.Text) <> "" Then
                            If HI.UL.ULDate.CheckDate(FDDateEnd.Text) <> "" Then
                                If Me.FTStartTime.Text <> "" Then
                                    If Me.FTEndTime.Text <> "" Then
                                        If CType(ogcTrainer.DataSource, DataTable).Rows.Count - 1 >= 0 Then
                                            _Pass = True
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, ogcTrainer.Name)
                                        End If
                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTEndTime_lbl.Text)
                                        FTEndTime.Focus()
                                    End If
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTStartTime_lbl.Text)
                                    FTStartTime.Focus()
                                End If
                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateEnd_lbl.Text)
                                FDDateEnd.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateBegin_lbl.Text)
                            FDDateBegin.Focus()
                        End If
                    Else
                        _Pass = True
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTTrainCode_lbl.Text)
                    FTTrainCode.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDocDate_lbl.Text)
                FDDocDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDocNo_lbl.Text)
            FTDocNo.Focus()
        End If
        Return _Pass
    End Function

    Private Sub LoadTrainInfo(ByVal Key As String)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = " SELECT     TOP 1    FTDocNo, FDDocDate, FTTrainCode, FNTrainType, FTTrainer, FDDateBegin, FDDateEnd, FTLocation, FCCostPerEmp, FTTrainNote,FTStartTime,FTEndTime,FTTotalHour,FNFixTrainTrainning"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrain "
        _Qry &= vbCrLf & "  WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(Key) & "' "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                FDDocDate.Text = HI.UL.ULDate.ConvertEN(R!FDDocDate.ToString)
                FNFixTrainTrainning.SelectedIndex = Val(R!FNFixTrainTrainning.ToString)
                FNTrainType.SelectedIndex = Val(R!FNTrainType.ToString)
                FTTrainCode.Text = R!FTTrainCode.ToString

                FTTrainer.Text = R!FTTrainer.ToString
                If FNTrainType.SelectedIndex <> 2 Then
                    Try

                        If R!FTTrainer.ToString <> "" Then
                            If CType(ogcTrainer.DataSource, DataTable).Rows.Count > 0 Then
                                For i As Integer = CType(ogcTrainer.DataSource, DataTable).Rows.Count - 1 To 0 Step -1
                                    CType(ogcTrainer.DataSource, DataTable).Rows.RemoveAt(i)
                                Next
                            End If
                            Call GenrowTrainName()
                            With ogvTrainer
                                .SetRowCellValue(0, "FTTrainerName", R!FTTrainer.ToString)
                            End With
                        Else
                            If Me.FTTrainCode.Text = "" Then
                                Call GenrowTrainName()
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                End If

                FDDateBegin.Text = ""
                FDDateEnd.Text = ""

                FDDateBegin.Text = HI.UL.ULDate.ConvertEN(R!FDDateBegin.ToString)
                FDDateEnd.Text = HI.UL.ULDate.ConvertEN(R!FDDateEnd.ToString)
                FTLocation.Text = R!FTLocation.ToString
                FCCostPerEmp.Value = Val(R!FCCostPerEmp.ToString)
                FTTrainNote.Text = R!FTTrainNote.ToString
                FTStartTime.Text = R!FTStartTime.ToString
                FTEndTime.Text = R!FTEndTime.ToString
                TotalTime.Text = R!FTTotalHour.ToString
            Next
        Else
            FDDocDate.Text = ""
            FNTrainType.SelectedIndex = 0
            FTTrainCode.Text = ""
            FTTrainer.Text = ""
            FDDateBegin.Text = ""
            FDDateEnd.Text = ""
            FTLocation.Text = ""
            FCCostPerEmp.Value = 0
            FTTrainNote.Text = ""
            FTStartTime.Text = ""
            FTEndTime.Text = ""
            TotalTime.Text = "00:00"
        End If
    End Sub

    'Private Sub LoadEmpTrainning(ByVal Key As String)
    '    Dim dt As DataTable
    '    Dim _Qry As String = ""
    '    Dim _calperPre As Double
    '    Dim _calperPost As Double
    '    Dim _row As Integer = 0

    '    _Qry = " SELECT        T.FNHSysEmpID"
    '    _Qry &= vbCrLf & "  , E.FTEmpCode"

    '    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
    '        _Qry &= vbCrLf & "  , E.FTEmpNameTH + '  '+ E.FTEmpSurnameTH AS FTEmpName"

    '    Else
    '        _Qry &= vbCrLf & "  , E.FTEmpNameEN + '  '+ E.FTEmpSurnameEN AS FTEmpName "
    '    End If

    '    _Qry &= vbCrLf & "   , T.FTEvaluate AS FTEvaluateName_Hide"
    '    _Qry &= vbCrLf & "   ,VE.FTNameTH AS FTEvaluateName"
    '    _Qry &= vbCrLf & "   , T.FTTrainNote,FNPreTest,FNPostTest,0.0 as FNPercenPre,0.0 as FNPercenPost"
    '    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp AS T WITH (NOLOCK) INNER JOIN"
    '    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..V_TrainEvaluate As VE ON ISNULL(T.FTEvaluate,'0')=VE.FNListIndex"
    '    _Qry &= vbCrLf & "  WHERE T.FTDocNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
    '    _Qry &= vbCrLf & "  ORDER BY  E.FTEmpCode "
    '    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
    '    ogc.DataSource = dt
    '    If dt.Rows.Count > 0 Then
    '        For Each R As DataRow In dt.Rows
    '            With ogv

    '                .SetRowCellValue(_row, "FNHSysEmpID", R!FNHSysEmpID.ToString)
    '                .SetRowCellValue(_row, "FTEmpCode", R!FTEmpCode.ToString)
    '                .SetRowCellValue(_row, "FTEmpName", R!FTEmpName.ToString)
    '                .SetRowCellValue(_row, "FTEvaluateName", R!FTEvaluateName.ToString)
    '                .SetRowCellValue(_row, "FTTrainNote", R!FTTrainNote.ToString)

    '                _calperPre = R!FNPreTest
    '                _calperPost = R!FNPostTest

    '                .SetRowCellValue(_row, "FNPercenPre", (_calperPre / Me.PointMaster.Value) * 100)
    '                .SetRowCellValue(_row, "FNPercenPost", (_calperPost / Me.PointMaster.Value) * 100)

    '                .SetRowCellValue(_row, "FTEvaluateName_Hide", R!FTEvaluateName_Hide.ToString)
    '                .SetRowCellValue(_row, "FNPostTest", R!FNPostTest.ToString)
    '                .SetRowCellValue(_row, "FNPreTest", R!FNPreTest.ToString)
    '                _row += 1

    '            End With
    '        Next
    '    End If
    'End Sub

    Private Sub LoadDataEmpTopic()
        Dim Qry As String = ""
        Dim dtCountMas As DataTable
        Dim dtCountEmp As DataTable
        Dim dtData As DataTable
        Dim _EmpID As String = ""
        Dim _EmpCode As String = ""
        Dim _EmpName As String = ""
        Dim i As Integer = 0
        Dim x As Integer = 0
        Dim ArrPre((_ColCount / 2) - 1)
        Dim ArrPost((_ColCount / 2) - 1)
        'Dim ArrTotal(1)

        Try
            Qry = "select distinct TE.FNHSysEmpID,isnull(TE.FNPreTest,0) AS FNPreTest,isnull(TE.FNPostTest,0) AS FNPostTest,TE.FTEvaluate as FTState,TE.FTTrainNote,E.FTEmpCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                Qry &= vbCrLf & ",P.FTPreNameNameTH+' '+E.FTEmpNameTH +'   '+ E.FTEmpSurnameTH as FTEmpName"
                Qry &= vbCrLf & ",List.FTNameTH AS FTEvaluate"
            Else
                Qry &= vbCrLf & ",P.FTPreNameNameEN+' '+E.FTEmpNameEN +'   '+ E.FTEmpSurnameEN as FTEmpName"
                Qry &= vbCrLf & ",List.FTNameEN AS FTEvaluate"
            End If
            Qry &= vbCrLf & "FROM"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp AS TE with(nolock) LEFT OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic AS T with(nolock) ON TE.FTDocNo=T.FTDocNo AND TE.FNHSysEmpID=T.FNHSysEmpID LEFT OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee as E with(nolock) ON TE.FNHSysEmpID=E.FNHSysEmpID LEFT OUtER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMPrename AS P ON E.FNHSysPreNameId=P.FNHSysPreNameId LEFT OUTER JOIN"
            Qry &= vbCrLf & "(SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='TrainEvaluate') AS List"
            Qry &= vbCrLf & "ON TE.FTEvaluate=List.FNListIndex"
            Qry &= vbCrLf & "WHERE TE.FTDocNo='" & Me.FTDocNo.Text & "'"
            Qry &= vbCrLf & "order by FNHSysEmpID asc"
            dtCountEmp = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

            Qry = "select T.FNHSysEmpID,ISNULL(T.FNPrePoint,0) AS FNPrePoint, ISNULL(T.FNPostPoint,0) AS FNPostPoint from "
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmp AS TE with(nolock) LEFT OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic AS T with(nolock) ON TE.FNHSysEmpID=T.FNHSysEmpID AND TE.FTDocNo=T.FTDocNo"
            Qry &= vbCrLf & "WHERE TE.FTDocNo='" & Me.FTDocNo.Text & "'"
            dtData = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

            Qry = "SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTrainTopic AS TT with(nolock) WHERE FTTrainCode='" & Me.FTTrainCode.Text & "'"
            dtCountMas = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
            For Each Rcount As DataRow In dtCountEmp.Rows
                Dim ArrFix() = {Rcount!FNHSysEmpID, Rcount!FTEmpCode.ToString, Rcount!FTEmpName.ToString}
                Dim ArrTotal() = {Rcount!FNPreTest, Rcount!FNPostTest}
                Dim ArrEvaNote() = {Rcount!FTEvaluate.ToString, Rcount!FTTrainNote.ToString}
                Dim ArrState() = {Rcount!FTState.ToString}

                If dtData.Select("FNHSysEmpID=" & Rcount!FNHSysEmpID & "").Length <= 0 Then
                    For j As Integer = 0 To dtCountMas.Rows.Count - 1 Step 1
                        ArrPre(j) = 0
                    Next
                Else
                    For Each RPre As DataRow In dtData.Select("FNHSysEmpID=" & Rcount!FNHSysEmpID & "")
                        ArrPre(i) = RPre!FNPrePoint
                        i += 1
                    Next
                End If

                If dtData.Select("FNHSysEmpID=" & Rcount!FNHSysEmpID & "").Length <= 0 Then
                    For j As Integer = 0 To dtCountMas.Rows.Count - 1 Step 1
                        ArrPost(j) = 0
                    Next
                Else
                    For Each RPost As DataRow In dtData.Select("FNHSysEmpID=" & Rcount!FNHSysEmpID & "")

                        ArrPost(x) = RPost!FNPostPoint
                        x += 1
                    Next
                End If


                Dim ArrFull((ArrFix.Length) + (ArrPre.Length) + (ArrPost.Length) + (ArrTotal.Length) + (ArrEvaNote.Length) + (ArrState.Length - 1))
                ArrFix.CopyTo(ArrFull, 0)
                ArrPre.CopyTo(ArrFull, ArrFix.Length)
                ArrPost.CopyTo(ArrFull, ArrPre.Length + ArrFix.Length)
                ArrTotal.CopyTo(ArrFull, ArrPre.Length + ArrFix.Length + ArrPost.Length)
                ArrEvaNote.CopyTo(ArrFull, ArrPre.Length + ArrFix.Length + ArrPost.Length + ArrTotal.Length)
                ArrState.CopyTo(ArrFull, ArrPre.Length + ArrFix.Length + ArrPost.Length + ArrTotal.Length + ArrEvaNote.Length)
                CType(ogcTopicEmp.DataSource, DataTable).Rows.Add(ArrFull)
                i = 0
                x = 0
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataMultiSkill()
        Dim Qry As String = ""
        Dim dtCountEmp As DataTable
        Dim dtDataEmpMulti As DataTable
        Dim dtList As DataTable
        Dim dtListRank As DataTable
        Dim ArrDy(_ColCountMulti - 1)
        'Dim i As Integer = 0
        Dim Indx As Integer = 0
        Dim _Val As Integer = 0
        Dim _colDivi As Integer = 0
        Dim _cal As Integer = 0
        Dim _Sum As Integer = 0
        Dim _NetSum As Integer = 0
        Dim _SumAll As Integer = 0
        Dim _NetAll As Integer = 0
        Dim _colDiviAll As Integer = 0
        'Dim _Str As String = ""
        Dim _Val25 As Integer = 0
        Dim _Val50 As Integer = 0
        Dim _Val75 As Integer = 0
        Dim _Val100 As Integer = 0
        Dim _ChkValCount As Integer = 0
        Dim _PositMaster As Integer = 0
        Dim _PositEmp As Integer = 0
        'Dim _Val25 As Integer = 0 : Dim _Val50 As Integer = 0 : Dim _Val75 As Integer = 0 : Dim _Val100 As Integer = 0
        Dim _C25 As Integer = 0 : Dim _C50 As Integer = 0 : Dim _C75 As Integer = 0 : Dim _C100 As Integer = 0
        Dim _V25 As Integer = 0 : Dim _V50 As Integer = 0 : Dim _V75 As Integer = 0 : Dim _V100 As Integer = 0
        Try

            Qry = "select JJ.FTDocNo,JJ.FNHSysEmpID,JJ.FTEmpName,JJ.WorkingAge,JJ.LeavePlan,JJ.LeaveOutPlan,JJ.FNAbsent,JJ.Late,JJ.TotalStopWorking from"
            Qry &= vbCrLf & "(select YY.FTSelect,YY.FNHSysEmpID,YY.FTEmpCode,YY.FTEmpName,YY.Working as WorkingAge"
            Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiOutPlanHrs<10 and YY.LeaveBusiOutPlanHrs>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanHrs) ELSE"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanHrs=0 THEn '00' Else convert(varchar(5),YY.LeaveBusiOutPlanHrs)END END+':'+"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin<10 and YY.LeaveBusiOutPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanMin) ELSE"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin=0 THEN '00' Else convert(varchar(2),YY.LeaveBusiOutPlanMin) END END AS LeaveOutPlan"
            Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiPlanHrs<10 and YY.LeaveBusiPlanHrs>0 THEN '0'+convert(varchar(5),LeaveBusiPlanHrs)else "
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanHrs=0 THEN '00' ELSE convert(varchar(5),YY.LeaveBusiPlanHrs)END END+':'+"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin<10 and YY.LeaveBusiPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiPlanMin)else"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin=0 THEN '00' ELSE convert(varchar(2),YY.LeaveBusiPlanMin) END END AS LeavePlan"
            Qry &= vbCrLf & ",Case WHEn YY.LateHrs<10 and YY.LateHrs>0  THEN '0'+convert(varchar(2),YY.LateHrs) else Case WHEN YY.LateHrs=0 THEn '00' Else convert(varchar(5),YY.LateHrs)end  end+':'+"
            Qry &= vbCrLf & "Case WHEN YY.LateMin<10 and YY.LateMin>0 THEN '0'+convert(varchar(2),YY.LateMin)else Case WHEN YY.LateMin=0 THEN '00' Else convert(varchar(2),YY.LateMin) END END AS Late"

            Qry &= vbCrLf & ",Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs<10 and YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs>0"
            Qry &= vbCrLf & "then'0'+convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs)else "
            Qry &= vbCrLf & "Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs) END END +':'+"

            Qry &= vbCrLf & "Case WHEN (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 <10 and (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 >0"
            Qry &= vbCrLf & "then'0'+convert(varchar(5),((YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60))else "
            Qry &= vbCrLf & "Case WHEN (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 =0 THEN '00' else convert(varchar(5),((YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60)) END END AS TotalStopWorking"

            Qry &= vbCrLf & ",Case WHEN YY.FNAbsentHrs<10 and YY.FNAbsentHrs>0 THEN '0'+convert(varchar(2),YY.FNAbsentHrs) else Case WHEN YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.FNAbsentHrs)END ENd+':'+"
            Qry &= vbCrLf & "Case WHEN YY.FNAbsentMin<10 and YY.FNAbsentMin>0 THEN '0'+convert(varchar(2),YY.FNAbsentMin)else Case WHEN YY.FNAbsentMin=0 THEN '00' Else convert(varchar(2),YY.FNAbsentMin) END END AS FNAbsent"
            Qry &= vbCrLf & ",ZZ.FTDocNo"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "(select XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.FTEmpName,XX.Working"
            Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)/60) AS LateHrs"
            Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)%60) AS LateMin"
            Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)/60) AS LeaveBusiPlanHrs"
            Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)%60) AS LeaveBusiPlanMin"
            Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)/60) AS LeaveBusiOutPlanHrs"
            Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)%60) AS LeaveBusiOutPlanMin"
            Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)/60) AS FNAbsentHrs"
            Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)%60) AS FNAbsentMin"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "(select  KK.FTSelect,KK.FNHSysEmpID,KK.FTEmpCode,KK.FTEmpName"
            Qry &= vbCrLf & ",convert(varchar(2),KK.FNEmpWorkAgeYear)+':'+convert(varchar(2),KK.FNEmpWorkAgeMoth) AS Working"
            Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)<=0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveVacation+KK.LeaveMaternity  AS LeaveBusiPlan"
            Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)>0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveSick + KK.FNAbsent AS LeaveBusiOutPlan"
            Qry &= vbCrLf & ",KK.FNLateNormalMin"
            Qry &= vbCrLf & ",KK.FNAbsent"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "(Select distinct '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"
            Qry &= vbCrLf & ", P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)/12) AS FNEmpWorkAgeYear"
            Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)%12) AS FNEmpWorkAgeMoth"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='1') ,0) AS LeaveBusi"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='98'),0) AS LeaveVacation"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='97'),0) AS LeaveMaternity"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='0'),0) AS LeaveSick"
            Qry &= vbCrLf & ",T.FNTimeMin,T.FNAbsent,T.FNLateNormalMin"
            Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) ON M.FNHSysEmpID=T.FNHSysEmpID"
            Qry &= vbCrLf & "WHERE  M.FTEmpCode <> '' AND M.FNEmpStatus <> '2' "
            Qry &= vbCrLf & ") as KK"
            Qry &= vbCrLf & ") AS XX"
            Qry &= vbCrLf & "group by XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.Working,XX.FTEmpName ) AS YY INNER JOIN"
            Qry &= vbCrLf & "(select distinct Multi.FNHSysEmpID,Multi.FTDocNo"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix AS Multi with(nolock) "

            Qry &= vbCrLf & "WHERE Multi.FTDocNo='" & Me.FTDocNo.Text & "') AS ZZ ON YY.FNHSysEmpID=ZZ.FNHSysEmpID"
            Qry &= vbCrLf & ") AS JJ"
            dtCountEmp = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

            Qry = "select  Mul.FNHSysEmpID,Mul.FTDocNo,Mul.FNPoint,Mas.FNSkillMatrix,Mul.FNHSysSkillMatrixId from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS Mas with(nolock) Left OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix AS Mul with(nolock) ON Mas.FNHSysSkillMatrixId=Mul.FNHSysSkillMatrixId"
            Qry &= vbCrLf & "WHERE Mul.FTDocNo='" & Me.FTDocNo.Text & "'"
            dtDataEmpMulti = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

            Qry = "SELECT DISTINCT FNSkillMatrix AS FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix"
            'Qry = "Select FNListIndex"
            'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNSkillMatrix'"
            dtList = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            Qry = "SELECT FNListIndex"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNRankingMultiSkill'"
            dtListRank = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            If _dtMulti.Rows.Count <= 0 Then
                For Each R As DataRow In dtCountEmp.Rows
                    Dim ArrFix() = {R!FNHSysEmpID, R!FTEmpName.ToString, R!WorkingAge.ToString, R!LeavePlan.ToString, R!LeaveOutPlan.ToString, R!FNAbsent.ToString, R!Late.ToString, R!TotalStopWorking.ToString}
                    For Each X As DataRow In dtList.Rows
                        For Each Z As DataRow In dtcol.Select("FNSkillMatrix=" & X!FNListIndex & "")
                            _PositEmp = 0
                            _PositMaster = 0
                            _PositMaster += 1
                            For Each Rs As DataRow In dtDataEmpMulti.Select("FTDocNo='" & R!FTDocNo.ToString & "' AND FNHSysEmpID=" & R!FNHSysEmpID & "AND FNSkillMatrix=" & X!FNListIndex & " AND FNHSysSkillMatrixId=" & Val(Z!FNHSysSkillMatrixId.ToString.Substring(0, 10)) & "")
                                ArrDy(Indx) = Rs!FNPoint
                                Indx += 1
                                _PositEmp += 1
                                Exit For
                            Next
                            If _PositEmp <> _PositMaster Then
                                ArrDy(Indx) = 0
                                Indx += 1
                            End If
                        Next
                        ArrDy(Indx) = 0
                        Indx += 1
                    Next
                    Indx -= 1

                    For i As Integer = 0 To dtListRank.Rows.Count - 1 Step 1
                        ArrDy(Indx) = 0
                        Indx += 1
                    Next
                    For Each Y As DataColumn In CType(ogcSkill.DataSource, DataTable).Columns
                        Select Case Y.ColumnName.ToString
                            Case "FTSummerySkillEMP25s", "FTSummerySkillEMP50s", "FNSummerySkillUP75s", "FTSummerySkillEMP100s"
                                ArrDy(Indx) = 0
                                Indx += 1
                        End Select
                    Next
                    Dim LoadDataEmpMultiArrFulls((ArrFix.Length) + (ArrDy.Length - 1))
                    ArrFix.CopyTo(LoadDataEmpMultiArrFulls, 0)
                    ArrDy.CopyTo(LoadDataEmpMultiArrFulls, ArrFix.Length)
                    _dtMulti.Rows.Add(LoadDataEmpMultiArrFulls)
                    Indx = 0
                Next
                ogcSkill.DataSource = _dtMulti
            End If


            With ogbvSkill
                For Row As Integer = 0 To .RowCount - 1 Step 1
                    _C100 = 0 : _C25 = 0 : _C50 = 0 : _C75 = 0 : _V100 = 0 : _V25 = 0 : _V50 = 0 : _V75 = 0
                    For a As Integer = 0 To dtList.Rows.Count - 1 Step 1
                        _Val = 0
                        _colDivi = 0
                        For k As Integer = 0 To .Columns.Count - 1 Step 1
                            If Microsoft.VisualBasic.Left(.Columns(k).Name.ToString, 2) = "FN" And (Microsoft.VisualBasic.Left(.Columns(k).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(k).FieldName.ToString, 5)) <> "FTSum" And
                                Microsoft.VisualBasic.Right(.Columns(k).FieldName.ToString, 1) = a.ToString Then
                                If .Columns(k).Name.ToString <> "FNSum" & a.ToString Then
                                    '_Val += .GetRowCellValue(Row, .Columns(k).FieldName.ToString)
                                    '_colDivi += 1
                                    If .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 25 Then
                                        _C25 += 1
                                    ElseIf .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 50 Then
                                        _C50 += 1
                                    ElseIf .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 75 Then
                                        _C75 += 1
                                    ElseIf .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 100 Then
                                        _C100 += 1
                                    End If
                                Else
                                End If
                            End If
                            If .Columns(k).Name.ToString = "FNSum" & a.ToString Then
                                If _C25 > 0 Then
                                    _V25 = _C25 * 1
                                End If
                                If _C50 > 0 Then
                                    _V50 = _C50 * 2
                                End If
                                If _C75 > 0 Then
                                    _V75 = _C75 * 3
                                End If
                                If _C100 > 0 Then
                                    _V100 = _C100 * 4
                                End If
                                If a.ToString = "0" Then
                                    _NetSum = (_V25 + _V50 + _V75 + _V100) * 3
                                ElseIf a.ToString = "1" Then
                                    _NetSum = (_V25 + _V50 + _V75 + _V100) * 2
                                ElseIf a.ToString = "2" Then
                                    _NetSum = (_V25 + _V50 + _V75 + _V100) * 1
                                End If
                                .SetRowCellValue(Row, "FNSum" & a.ToString, _NetSum)
                                _C100 = 0 : _C25 = 0 : _C50 = 0 : _C75 = 0 : _V100 = 0 : _V25 = 0 : _V50 = 0 : _V75 = 0
                                Exit For
                            End If
                        Next
                        '_Sum = _Val / _colDivi

                        'If _Sum >= 0 And _Sum < 25 Then
                        '    _NetSum = 0
                        'ElseIf _Sum >= 25 And _Sum <= 49 Then
                        '    _NetSum = 25
                        'ElseIf _Sum >= 50 And _Sum <= 74 Then
                        '    _NetSum = 50
                        'ElseIf _Sum >= 75 And _Sum <= 99 Then
                        '    _NetSum = 75
                        'Else
                        '    _NetSum = 100
                        'End If

                    Next

                    _SumAll = 0
                    _colDiviAll = 0
                    For Each R As DataRow In dtList.Rows
                        For c As Integer = .Columns.Count - 1 To 0 Step -1
                            If Microsoft.VisualBasic.Right(.Columns(c).FieldName.ToString, 1) = R!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(c).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(c).FieldName.ToString, 5)) <> "FTSum" Then
                                If .Columns(c).Name.ToString <> "FNSum" & R!FNListIndex.ToString Then
                                    _SumAll += .GetRowCellValue(Row, .Columns(c).FieldName.ToString)
                                    _colDiviAll += 1
                                End If
                            End If
                        Next
                    Next
                    _NetAll = _SumAll / _colDiviAll
                    If _NetAll >= 76 And _NetAll <= 100 Then
                        .SetRowCellValue(Row, "cFNExcellence", "1")
                        .SetRowCellValue(Row, "cFNGood", "0")
                        .SetRowCellValue(Row, "cFNFair", "0")
                        .SetRowCellValue(Row, "cFNBeginner", "0")
                    ElseIf _NetAll >= 51 And _NetAll <= 75 Then
                        .SetRowCellValue(Row, "cFNGood", "1")
                        .SetRowCellValue(Row, "cFNExcellence", "0")
                        .SetRowCellValue(Row, "cFNFair", "0")
                        .SetRowCellValue(Row, "cFNBeginner", "0")
                    ElseIf _NetAll >= 26 And _NetAll <= 50 Then
                        .SetRowCellValue(Row, "cFNFair", "1")
                        .SetRowCellValue(Row, "cFNGood", "0")
                        .SetRowCellValue(Row, "cFNExcellence", "0")
                        .SetRowCellValue(Row, "cFNBeginner", "0")
                    ElseIf _NetAll >= 0 And _NetAll <= 25 Then
                        .SetRowCellValue(Row, "cFNBeginner", "1")
                        .SetRowCellValue(Row, "cFNFair", "0")
                        .SetRowCellValue(Row, "cFNGood", "0")
                        .SetRowCellValue(Row, "cFNExcellence", "0")
                    End If
                    _Val25 = 0
                    _Val50 = 0
                    _Val75 = 0
                    _Val100 = 0
                    For Each RR As DataRow In dtList.Rows
                        For i As Integer = .Columns.Count - 1 To 0 Step -1
                            If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = RR!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" _
                                And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                                _ChkValCount = .GetRowCellValue(Row, .Columns(i).FieldName.ToString)
                                If _ChkValCount = 25 Then
                                    _Val25 += 1
                                ElseIf _ChkValCount = 50 Then
                                    _Val50 += 1
                                ElseIf _ChkValCount = 75 Then
                                    _Val75 += 1
                                ElseIf _ChkValCount = 100 Then
                                    _Val100 += 1
                                End If
                            End If
                        Next
                    Next
                    .SetRowCellValue(Row, "FTSummerySkillEMP25s", _Val25)
                    .SetRowCellValue(Row, "FTSummerySkillEMP50s", _Val50)
                    .SetRowCellValue(Row, "FNSummerySkillUP75s", _Val75)
                    .SetRowCellValue(Row, "FTSummerySkillEMP100s", _Val100)
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadTrainName()
        Dim Qry As String = ""
        Dim dt As DataTable
        Dim JK As Integer
        Dim bol As Boolean

        Try
            Qry = "select L.FNSeq,L.FTTrainer AS FTTrainerName"
            Qry &= vbCrLf & ",CASE WHEN L.FDDateBegin=''THEN ''ELSE convert(nvarchar(10),convert(datetime,L.FDDateBegin),103) END AS FDStartDate"
            Qry &= vbCrLf & ",CASE WHEN L.FDDateEnd ='' THEN ''ELSE convert(nvarchar(10),convert(datetime,L.FDDateEnd),103)END AS FDEndDate,L.FTStartTime,L.FTEndTime,L.FTTotalHour,L.FNTotalMinute "
            Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer AS L"
            Qry &= vbCrLf & "WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(Me.FTDocNo.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
            'If dt.Rows.Count <= 0 Then
            If dt.Rows.Count > 0 Then
                If ogvTrainer.RowCount > 0 Then
                    If FNTrainType.SelectedIndex <> 2 Then

                        If Me.FTTrainer.Text = "" Then
                            For i As Integer = ogvTrainer.RowCount - 1 To 0 Step -1
                                CType(ogcTrainer.DataSource, DataTable).Rows.RemoveAt(i)
                            Next
                            Me.ogcTrainer.DataSource = dt
                        Else
                            'If _Transac Then
                            '    Me.ogcTrainer.DataSource = dt
                            '    _Transac = False
                            'Else
                            For Each R As DataRow In dt.Rows
                                CType(ogcTrainer.DataSource, DataTable).Rows.Add(Val(R!FNSeq.ToString), R!FTTrainerName.ToString, HI.UL.ULDate.ConvertEN(R!FDStartDate.ToString), HI.UL.ULDate.ConvertEN(R!FDEndDate.ToString), R!FTStartTime.ToString, R!FTEndTime.ToString, R!FTTotalHour.ToString, Val(R!FNTotalMinute.ToString))
                            Next
                            'End If
                        End If

                    Else
                        Me.ogcTrainer.DataSource = dt
                    End If
                End If
            Else
                If _FormLoadDt Then
                    Me.ogcTrainer.DataSource = dt
                    _FormLoadDt = False
                End If

            End If

            'Else
            'For Each R As DataRow In dt.Rows
            '    CType(ogcTrainer.DataSource, DataTable).Rows.Add(Val(R!FNSeq.ToString), R!FTTrainerName.ToString, HI.UL.ULDate.ConvertEN(R!FDStartDate.ToString), HI.UL.ULDate.ConvertEN(R!FDEndDate.ToString), R!FTStartTime.ToString, R!FTEndTime.ToString, R!FTTotalHour.ToString, Val(R!FNTotalMinute.ToString))
            'Next
            'End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub LoadTotalpointMaster()
    '    Dim Qry As String = ""
    '    Try
    '        If Me.FTTrainCode.Text <> "" Then
    '            Qry = "select TOP 1 isnull(FNTotalPoint,0) as FNTotalPoint from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMTrain where FTTrainCode='" & HI.UL.ULF.rpQuoted(Me.FTTrainCode.Text) & "' "
    '            PointMaster.Value = Double.Parse(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_HR))
    '        End If
    '    Catch ex As Exception

    '    End Try


    'End Sub

    Private Sub LoadImg()
        Dim dt As New DataTable
        Dim Qry As String
        If Me.FTDocNo.Text <> "" Then
            Try
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_HR)
                HI.Conn.SQLConn.SqlConnectionOpen()

                Qry = "select FPImage,FNSeq,FTDocNo from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "..THRTTrainImage where FTDocNo='" & Me.FTDocNo.Text & "'"
                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)

                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                ogcImg.DataSource = dt

                HI.Conn.SQLConn.DisposeSqlConnection(Conn.SQLConn.Cnn)

            Catch ex As Exception

            End Try
        End If
        RepImg.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze

    End Sub

    'Private Function PrepareGridEmpTopic() As DataTable
    '    Dim dt As New DataTable
    '    Dim Qry As String = ""

    '    Qry = "SELECT FTTrainTopicNameTH,FTTrainTopicNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMTrainTopic WHERE FTTrainCode='" & Me.FTTrainCode.Text & "'"
    '    dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
    '    Return dt
    'End Function
#End Region

#Region "SETGridTrainDynamic TrainEmp"

    'Private Function CreateDatatableCol() As DataTable
    '    Dim dt As New DataTable
    '    Dim _Dt As DataTable
    '    Dim Qry As String = ""

    '    Qry = "SELECT FTTrainTopicNameEN,FNHSysTrainTopicId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMTrainTopic WHERE FTTrainCode='" & Me.FTTrainCode.Text & "'"
    '    _Dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

    '    With dt.Columns
    '        .Add("FNHSysEmpID", GetType(Integer))
    '        .Add("FTEmpCode", GetType(String))
    '        .Add("FTEmpName", GetType(String))
    '        For Each R As DataRow In _Dt.Rows
    '            .Add(R!FNHSysTrainTopicId.ToString & R!FTTrainTopicNameEN.ToString & "Pre")
    '        Next
    '        For Each Row As DataRow In _Dt.Rows
    '            .Add(Row!FNHSysTrainTopicId.ToString & Row!FTTrainTopicNameEN.ToString & "Post")
    '        Next

    '    End With
    '    Return dt
    'End Function

    'Private Sub PrepareGridTopic()
    '    Dim dt As DataTable = CreateDatatableCol()
    '    Dim _FieldName As String = ""
    '    Try
    '        With ogvTopicEmp
    '            .OptionsView.ColumnAutoWidth = False

    '            For cc As Integer = .Columns.Count - 1 To 0 Step -1
    '                .Columns.Remove(.Columns(cc))
    '            Next
    '            If Not (dt Is Nothing) Then
    '                _ColCount = 0
    '                For Each col As DataColumn In dt.Columns
    '                    Dim ColG As New GridColumn
    '                    With ColG
    '                        .Visible = True
    '                        Select Case col.ColumnName.ToString
    '                            Case "FNHSysEmpID"
    '                                .Name = "cc" & col.ColumnName.ToString
    '                                .Caption = col.ColumnName.ToString
    '                                .FieldName = col.ColumnName.ToString
    '                            Case "FTEmpCode", "FTEmpName"
    '                                .Name = "c" & col.ColumnName.ToString
    '                                .Caption = col.ColumnName.ToString
    '                                .FieldName = col.ColumnName.ToString
    '                            Case Else
    '                                .Name = col.ColumnName.ToString.Substring(0, 10)
    '                                .Caption = col.ColumnName.ToString.Substring(10)
    '                                .FieldName = col.ColumnName.ToString
    '                                _ColCount += 1
    '                        End Select
    '                    End With
    '                    .Columns.Add(ColG)
    '                    With .Columns(col.ColumnName.ToString)
    '                        Select Case col.ColumnName.ToString
    '                            Case "FNHSysEmpID"
    '                                .VisibleIndex = -1
    '                            Case "FTEmpCode"
    '                                With .OptionsColumn
    '                                    .ReadOnly = True
    '                                    .AllowEdit = False
    '                                    .AllowMove = False
    '                                End With
    '                                .Width = 110
    '                            Case "FTEmpName"
    '                                With .OptionsColumn
    '                                    .ReadOnly = True
    '                                    .AllowEdit = False
    '                                    .AllowMove = False
    '                                End With
    '                                .Width = 250
    '                            Case Else
    '                                .AppearanceCell.BackColor = Color.LightCyan
    '                                With .OptionsColumn
    '                                    .ReadOnly = False
    '                                    .AllowEdit = True
    '                                    .AllowMove = False
    '                                End With
    '                                .Width = 80
    '                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                                .DisplayFormat.FormatString = "{0:n2}"
    '                                Dim Rep As RepositoryItemCalcEdit = New RepositoryItemCalcEdit
    '                                Rep.Name = "Rep" & ColG.Caption.ToString
    '                                .ColumnEdit = Rep
    '                                Rep.Buttons(0).Visible = False

    '                        End Select
    '                    End With
    '                Next
    '            End If
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Function CreatedatatableTopicEmp() As DataTable
        Dim Qry As String = ""
        Try
            Qry = "SELECT FTTrainTopicCode,FTTrainTopicNameEN,FTTrainTopicNameTH,FNHSysTrainTopicId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMTrainTopic WHERE FTTrainCode='" & Me.FTTrainCode.Text & "'"
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub CreateColGridBanedTopic(ByVal dt As DataTable)
        'Dim dt As DataTable = CreatedatatableTopicEmp()
        Dim _ColI As Integer = 3
        Try
            With ogvTopicEmp
                .Columns.Add() : .Columns.Add() : .Columns.Add()
                With .Columns(0)
                    .Name = "FNHSysEmpID"
                    .FieldName = "FNHSysEmpID"
                    .Caption = "FNHSysEmpID"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                End With
                With .Columns(1)
                    .Name = "FTEmpCode"
                    .FieldName = "FTEmpCode"
                    .Caption = "FTEmpCode"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 120
                End With
                With .Columns(2)
                    .Name = "FTEmpName"
                    .Caption = "FTEmpName"
                    .FieldName = "FTEmpName"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 250
                End With


                _ColCount = 0
                If Not (dt Is Nothing) Then
                    For Each R As DataRow In dt.Rows
                        .Columns.Add()
                        With .Columns(_ColI)
                            .AppearanceCell.BackColor = Color.LightCyan
                            .Name = "Pre" & R!FNHSysTrainTopicId.ToString
                            .FieldName = "Pre" & R!FNHSysTrainTopicId.ToString
                            .Caption = "PreTest" & R!FTTrainTopicNameEN.ToString
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "0:n2"

                            Dim Rep As RepositoryItemCalcEdit = New RepositoryItemCalcEdit
                            Rep.Name = "Rep" & .FieldName.ToString
                            Rep.AllowMouseWheel = False
                            Rep.AllowNullInput = DevExpress.Utils.DefaultBoolean.False
                            With Rep
                                AddHandler .EditValueChanged, AddressOf RepPre_Changed
                            End With
                            .ColumnEdit = Rep
                            Rep.Buttons(0).Visible = False
                            Rep.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            Rep.DisplayFormat.FormatString = "0:n2"
                            Rep.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                            'Rep.Mask.EditMask = "0:n2"

                            With .OptionsColumn
                                .AllowEdit = True
                                .AllowMove = False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .ReadOnly = False
                            End With
                            .Visible = True
                            .Width = 70
                        End With
                        _ColI += 1
                        _ColCount += 1
                    Next
                    For Each X As DataRow In dt.Rows
                        .Columns.Add()
                        With .Columns(_ColI)
                            .AppearanceCell.BackColor = Color.LightCyan
                            .Name = "Post" & X!FNHSysTrainTopicId.ToString
                            .FieldName = "Post" & X!FNHSysTrainTopicId.ToString
                            .Caption = "PostTest" & X!FTTrainTopicNameEN.ToString
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "0:n2"
                            Dim RepPost As RepositoryItemCalcEdit = New RepositoryItemCalcEdit
                            RepPost.Name = "Rep" & .FieldName.ToString
                            RepPost.AllowMouseWheel = False
                            RepPost.AllowNullInput = DevExpress.Utils.DefaultBoolean.False
                            With RepPost
                                AddHandler .EditValueChanged, AddressOf RepPost_Changed
                            End With
                            .ColumnEdit = RepPost
                            RepPost.Buttons(0).Visible = False
                            RepPost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            RepPost.DisplayFormat.FormatString = "0:n2"
                            RepPost.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                            'Rep.Mask.EditMask = "0:n2"

                            With .OptionsColumn
                                .AllowEdit = True
                                .AllowMove = False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .ReadOnly = False
                            End With
                            .Visible = True
                            .Width = 70
                        End With
                        _ColI += 1
                        _ColCount += 1
                    Next
                End If

                .Columns.Add()
                With .Columns(_ColI)
                    .Name = "FNTotalPre"
                    .Caption = "FNTotalPre"
                    .FieldName = "FNTotalPre"
                    .Visible = True
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "{0:n2}"
                    .AppearanceCell.Options.UseTextOptions = True
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                End With
                _ColI += 1

                .Columns.Add()
                With .Columns(_ColI)
                    .Name = "FNTotalPost"
                    .Caption = "FNTotalPost"
                    .FieldName = "FNTotalPost"
                    .Visible = True
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "{0:n2}"
                    .AppearanceCell.Options.UseTextOptions = True
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                End With
                _ColI += 1

                .Columns.Add()
                With .Columns(_ColI)
                    .Name = "FTEvaluate"
                    .Caption = "FTEvaluate"
                    .FieldName = "FTEvaluate"
                    .Visible = True
                    .AppearanceCell.BackColor = Color.LightCyan
                    .AppearanceCell.Options.UseTextOptions = True
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    With .OptionsColumn
                        .AllowEdit = True
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = False
                    End With
                    Dim ComBo As New RepositoryItemComboBox
                    ComBo.Name = "Rep" & .FieldName.ToString
                    ComBo.Buttons(0).Visible = True
                    .ColumnEdit = ComBo
                    ComBo.Items.AddRange(HI.TL.CboList.SetList("TrainEvaluate"))
                    Me.PComBo = ComBo
                    With ComBo
                        AddHandler .SelectedIndexChanged, AddressOf CbolistChagned
                    End With
                    .Width = 75
                End With
                _ColI += 1

                .Columns.Add()
                With .Columns(_ColI)
                    .Name = "FTTrainNote"
                    .Caption = "FTTrainNote"
                    .FieldName = "FTTrainNote"
                    .Visible = True
                    .AppearanceCell.Options.UseTextOptions = True
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    With .OptionsColumn
                        .AllowEdit = True
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = False
                    End With
                    .Width = 300
                End With
                _ColI += 1

                .Columns.Add()
                With .Columns(_ColI)
                    .Name = "FTState"
                    .Caption = "FTState"
                    .FieldName = "FTState"
                    .Visible = True
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GenGridBannedTopicEmp(ByVal dtCol As DataTable)
        Dim _Str As String = ""
        'Dim _GbandIndex As Integer = 0
        'Dim dtCol As DataTable = CreatedatatableTopicEmp()
        Dim StageCreateBaned1 As Boolean = False
        Dim StageCreateBaned2 As Boolean = False
        Dim STGrowCount As Boolean = False
        Dim STGBanedFix As Boolean = False
        _Str = "FNHSysEmpID|FTEmpCode|FTEmpName"

        Call CreateColGridBanedTopic(dtCol)
        Try
            With ogvTopicEmp
                If .Bands.Count > 0 Then
                    For i As Integer = .Bands.Count - 1 To 0 Step -1
                        .Bands.RemoveAt(i)
                    Next
                End If

                If Not (dtCol Is Nothing) Then
                    .BeginInit()
                    For Each Str As String In _Str.Split("|")
                        Dim _gBand As New GridBand
                        With _gBand
                            Select Case Str.ToString
                                Case "FNHSysEmpID"
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = Str
                                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName(Str))
                                    .Name = ogvTopicEmp.Name.ToString & "gb" & Str
                                    .RowCount = 2
                                    .Visible = False
                                Case "FTEmpCode"
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = Str
                                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName(Str))
                                    .Name = ogvTopicEmp.Name.ToString & "gb" & Str
                                    .RowCount = 2
                                Case "FTEmpName"
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = Str
                                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName(Str))
                                    .Name = ogvTopicEmp.Name.ToString & "gb" & Str
                                    .RowCount = 2
                            End Select
                        End With
                        .Bands.Add(_gBand)
                        '_GbandIndex += 1
                    Next
                    STGBanedFix = True
                End If

                StageCreateBaned1 = False
                STGrowCount = False
                If Not (dtCol Is Nothing) Then
                    Dim BanedType As New GridBand
                    For Each R As DataRow In dtCol.Rows
                        Dim BanedCol1 As New GridBand
                        If StageCreateBaned1 = False Then
                            With BanedType
                                With .AppearanceHeader
                                    .Options.UseTextOptions = True
                                    .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With
                                '.Name = "GbHPre" & R!FTTrainTopicCode.ToString
                                .Name = "GbHPreFIX"
                                .Caption = "PreTest"
                                .RowCount = 1
                                StageCreateBaned1 = True
                            End With
                            .Bands.Add(BanedType)
                        End If

                        With BanedCol1
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            '.Name = "Pre" & R!FTTrainTopicCode.ToString
                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                Dim QryTH As String = ""
                                Try
                                    QryTH = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "PreTH" & R!FTTrainTopicCode.ToString & "'"
                                    HI.Conn.SQLConn.ExecuteNonQuery(QryTH, Conn.DB.DataBaseName.DB_LANG)
                                Catch ex As Exception
                                End Try
                                .Name = "PreTH" & R!FTTrainTopicCode.ToString
                                .Caption = R!FTTrainTopicNameTH.ToString
                            Else
                                Dim QryEN As String = ""
                                Try
                                    QryEN = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "PreEN" & R!FTTrainTopicCode.ToString & "'"
                                    HI.Conn.SQLConn.ExecuteNonQuery(QryEN, Conn.DB.DataBaseName.DB_LANG)
                                Catch ex As Exception
                                End Try
                                .Name = "PreEN" & R!FTTrainTopicCode.ToString
                                .Caption = R!FTTrainTopicNameEN.ToString
                            End If
                            .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("Pre" & R!FNHSysTrainTopicId.ToString))
                            .RowCount = 1
                        End With
                        BanedType.Children.Add(BanedCol1)
                    Next
                End If
                StageCreateBaned2 = False
                If Not (dtCol Is Nothing) Then
                    Dim BanedType2 As New GridBand
                    For Each X As DataRow In dtCol.Rows
                        Dim BanedCol2 As New GridBand
                        If StageCreateBaned2 = False Then
                            With BanedType2
                                With .AppearanceHeader
                                    .Options.UseTextOptions = True
                                    .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With
                                '.Name = "GbHPost" & X!FTTrainTopicCode.ToString

                                .Name = "GbHPostFIX"
                                .Caption = "PostTest"

                                .RowCount = 1
                                StageCreateBaned2 = True
                            End With
                            .Bands.Add(BanedType2)
                        End If

                        With BanedCol2
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            '.Name = "Post" & X!FTTrainTopicCode.ToString
                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                Dim QryTH As String = ""
                                Try
                                    QryTH = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "PostTH" & X!FTTrainTopicCode.ToString & "'"
                                    HI.Conn.SQLConn.ExecuteNonQuery(QryTH, Conn.DB.DataBaseName.DB_LANG)
                                Catch ex As Exception
                                End Try
                                .Name = "PostTH" & X!FTTrainTopicCode.ToString
                                .Caption = X!FTTrainTopicNameTH.ToString
                            Else
                                Dim QryEN As String = ""
                                Try
                                    QryEN = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "PostEN" & X!FTTrainTopicCode.ToString & "'"
                                    HI.Conn.SQLConn.ExecuteNonQuery(QryEN, Conn.DB.DataBaseName.DB_LANG)
                                Catch ex As Exception
                                End Try
                                .Name = "PostEN" & X!FTTrainTopicCode.ToString
                                .Caption = X!FTTrainTopicNameEN.ToString
                            End If
                            .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("Post" & X!FNHSysTrainTopicId.ToString))
                            .RowCount = 1
                        End With
                        BanedType2.Children.Add(BanedCol2)
                    Next
                End If

                Dim _GridBTotalPre As New GridBand
                Dim _GridBTotalPost As New GridBand
                Dim _GridBEvaluate As New GridBand
                Dim _GridBTrainNote As New GridBand
                Dim _GridBTrainState As New GridBand

                With _GridBTotalPre
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .Caption = "TotalPre"
                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("FNTotalPre"))
                    .Name = "gbTotalPre"
                    .RowCount = 2
                End With
                .Bands.Add(_GridBTotalPre)
                With _GridBTotalPost
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .Caption = "TotalPost"
                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("FNTotalPost"))
                    .Name = "gbTotalPost"
                    .RowCount = 2
                End With
                .Bands.Add(_GridBTotalPost)

                With _GridBEvaluate
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .Caption = "FTEvaluate"
                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("FTEvaluate"))
                    .Name = "gbFTEvaluate"
                    .RowCount = 2
                End With
                .Bands.Add(_GridBEvaluate)

                With _GridBTrainNote
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .Caption = "FTTrainNote"
                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("FTTrainNote"))
                    .Name = "gbFTTrainNote"
                    .RowCount = 2
                End With
                .Bands.Add(_GridBTrainNote)

                With _GridBTrainState
                    .Caption = "FTState"
                    .Columns.Add(ogvTopicEmp.Columns.ColumnByFieldName("FTState"))
                    .Name = "gbFTState"
                    .RowCount = 2
                    .Visible = False
                End With
                .Bands.Add(_GridBTrainState)

                .EndInit()

            End With
            Dim oSysLang As New HI.ST.SysLanguage
            Try

                Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString, Me)
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)


            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveColTopicEmp()
        With ogvTopicEmp
            If .Columns.Count > 0 Then
                For i As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns.RemoveAt(0)
                Next
            End If
        End With
    End Sub

#End Region

#Region "SETGridBaned MultiSkill"

    Private Function CreatedataTableMasterMultiSkill() As DataTable
        Dim Qry As String = ""

        Try
            'If _SectType = False Then
            'Qry = "SELECT SK.FNSkillMatrix,SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN,Sk.FNHSysSkillMatrixId+''+SK.FNSkillMatrix AS FNHSysSkillMatrixId "
            'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            'Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            'Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex"

            'Qry = "SELECt AA.FTSkillMatrixCode+'_'+AA.FNSkillMatrix as FTSkillMatrixCode,AA.FTSkillMatrixNameTH,AA.FTSkillMatrixNameEN"
            'Qry &= vbCrLf & ",AA.FTNameTH,AA.FTNameEN,AA.FNHSysSkillMatrixId+''+AA.FNSkillMatrix AS FNHSysSkillMatrixId,AA.FNSkillMatrix"
            'Qry &= vbCrLf & "FROM"
            'Qry &= vbCrLf & "(SELECT SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN"
            'Qry &= vbCrLf & ",convert(varchar(13),Sk.FNHSysSkillMatrixId) AS FNHSysSkillMatrixId"
            'Qry &= vbCrLf & ",convert(varchar(2),SK.FNSkillMatrix) AS FNSkillMatrix"
            'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            'Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            'Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex "
            'Qry &= vbCrLf & "WHERE SK.FTStateActive='1') AS AA"


            'Else
            Qry = "SELECt AA.FTSkillMatrixCode+'_'+AA.FNSkillMatrix as FTSkillMatrixCode,AA.FTSkillMatrixNameTH,AA.FTSkillMatrixNameEN"
                Qry &= vbCrLf & ",AA.FTNameTH,AA.FTNameEN,AA.FNHSysSkillMatrixId+''+AA.FNSkillMatrix AS FNHSysSkillMatrixId,AA.FNSkillMatrix"
                Qry &= vbCrLf & "FROM"
                Qry &= vbCrLf & "(SELECT FNSectType FROM HITECH_HR.dbo.THRMTrain WHERE FTTrainCode = '" & FTTrainCode.Text & "') AS T INNER JOIN"
                Qry &= vbCrLf & "(SELECT SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN,SK.FNSectType"
                Qry &= vbCrLf & ",convert(varchar(13),Sk.FNHSysSkillMatrixId) AS FNHSysSkillMatrixId"
                Qry &= vbCrLf & ",convert(varchar(2),SK.FNSkillMatrix) AS FNSkillMatrix"
                Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
                Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
                Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex "
                Qry &= vbCrLf & "WHERE SK.FTStateActive='1') AS AA ON T.FNSectType = AA.FNSectType"
            'End If
            _SectType = False
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Private Function CreatedataTableDistinctMasMulti()
    '    Dim Qry As String = ""

    '    Try
    '        Qry = "SELECT distinct SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN,Sk.FNHSysSkillMatrixId "
    '        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
    '        Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
    '        Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex"
    '        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Private Function CreatedataTableMasterHeadgb() As DataTable
        Dim Qry As String = ""

        Try
            Qry = "SELECT distinct sys.FTNameTH,sys.FTNameEN, sys.FNListIndex "
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex"
            Qry &= vbCrLf & "order by FNListIndex asc"
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function CreatedatatableRanking() As DataTable
        Dim Qry As String = ""
        Try
            Qry = "SELECT FTListName, FNListIndex, FTNameTH, FTNameEN"
            Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNRankingMultiSkill'"
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Private Sub CreateColgbMultiSkill(ByVal dtCol As DataTable, dtSum As DataTable, dt As DataTable)
        'Dim dtCol As DataTable = CreatedataTableMasterMultiSkill()
        'Dim dtSum As DataTable = CreatedataTableMasterHeadgb()
        _Gencol = True
        Dim _coli As Integer = 0
        Dim i As Integer = 0
        _ColCountMulti = 0
        Try
            With ogbvSkill
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FNHSysEmpID"
                    .FieldName = "FNHSysEmpID"
                    .Caption = "FNHSysEmpID"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTEmpName"
                    .FieldName = "FTEmpName"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 250
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTYearWork"
                    .Caption = "FTYearWork"
                    .FieldName = "FTYearWork"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    '.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    '.DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTLeaveFlowPlan"
                    .Caption = "FTLeaveFlowPlan"
                    .FieldName = "FTLeaveFlowPlan"
                    .Visible = True
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTLeaveOutFlowPlan"
                    .Caption = "FTLeaveOutFlowPlan"
                    .FieldName = "FTLeaveOutFlowPlan"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTAbsent"
                    .Caption = "FTAbsent"
                    .FieldName = "FTAbsent"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTLate"
                    .Caption = "FTLate"
                    .FieldName = "FTLate"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSumWorkStop"
                    .Caption = "FTSumWorkStop"
                    .FieldName = "FTSumWorkStop"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With



                For Each V As DataRow In dtSum.Rows
                    For Each R As DataRow In dtCol.Select("FTNameEN='" & V!FTNameEN.ToString & "'")
                        'Dim _FieldNameStr = R!FNHSysSkillMatrixId.ToString & R!FNSkillMatrix.ToString
                        .Columns.Add()
                        With .Columns(_coli)
                            .AppearanceCell.BackColor = Color.LightCyan
                            .Name = "FN" & R!FTSkillMatrixCode.ToString
                            .Caption = "FN" & R!FTSkillMatrixCode.ToString
                            .FieldName = R!FNHSysSkillMatrixId
                            'R!FNHSysSkillMatrixId.ToString
                            '& R!FNSkillMatrix.ToString
                            .Visible = True
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "0:n0"
                            With .OptionsColumn
                                .AllowEdit = True
                                .AllowMove = False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .ReadOnly = False
                            End With
                            .Width = 75
                            Dim Rep As RepositoryItemCalcEdit = New RepositoryItemCalcEdit
                            Rep.Name = "Rep" & .FieldName.ToString
                            Rep.AllowMouseWheel = False
                            .ColumnEdit = Rep
                            Rep.Buttons(0).Visible = False
                            Rep.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            Rep.DisplayFormat.FormatString = "0:n0"
                            Rep.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                            Rep.Tag = "9"
                            With Rep
                                AddHandler .EditValueChanged, AddressOf RepChanging
                            End With
                            _coli += 1
                            _ColCountMulti += 1
                        End With
                    Next
                    If V!FTNameEN.ToString <> "Other Skill" Then
                        .Columns.Add()
                        With .Columns(_coli)
                            .Name = "FNSum" & V!FNListIndex.ToString
                            .Caption = "FNSum" & V!FNListIndex.ToString
                            .FieldName = "FNSum" & V!FNListIndex.ToString
                            .Visible = True
                            With .AppearanceCell
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            End With
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "0:n0"
                            With .OptionsColumn
                                .AllowEdit = False
                                .AllowMove = False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .ReadOnly = True
                            End With
                            .Width = 75
                            _coli += 1
                            _ColCountMulti += 1
                        End With
                    End If
                Next

                'For Each Row As DataRow In dtSum.Rows
                '    .Columns.Add()
                '    With .Columns(_coli)
                '        .Name = "FTSum" & Row!FNListIndex.ToString
                '        .Caption = "FTSum" & Row!FNListIndex.ToString
                '        .FieldName = "FTSum" & Row!FNListIndex.ToString
                '        .Visible = True
                '        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                '        .DisplayFormat.FormatString = "0:n0"
                '        With .OptionsColumn
                '            .AllowEdit = False
                '            .AllowMove = False
                '            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                '            .ReadOnly = True
                '        End With
                '        .Width = 75
                '        _coli += 1
                '        _ColCountMulti += 1
                '    End With
                'Next

                For Each Rs As DataRow In dt.Rows
                    .Columns.Add()
                    With .Columns(_coli)
                        With .AppearanceCell
                            If Rs!FNListIndex.ToString = "0" Then
                                .BackColor = Color.Gold
                            ElseIf Rs!FNListIndex.ToString = "1" Then
                                .BackColor = Color.Silver
                            ElseIf Rs!FNListIndex.ToString = "2" Then
                                .BackColor = Color.DarkRed
                                .ForeColor = Color.White
                            End If
                        End With
                        .Name = "cFN" & Rs!FTNameEN.ToString
                        If ST.Lang.Language = ST.Lang.eLang.TH Then
                            .Caption = Rs!FTNameTH.ToString
                        Else
                            .Caption = Rs!FTNameEN.ToString
                        End If
                        .FieldName = "cFN" & Rs!FTNameEN.ToString
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .DisplayFormat.FormatString = "0:n0"
                        With .OptionsColumn
                            .AllowEdit = False
                            .AllowMove = False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .ReadOnly = True
                        End With
                        .Visible = True
                        .Width = 75
                        Dim Rep As New RepositoryItemCheckEdit
                        Rep.Name = "Rep" & .FieldName.ToString
                        .ColumnEdit = Rep
                        'Rep.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
                        Rep.Caption = "Check"
                        Rep.ValueChecked = "1"
                        Rep.ValueUnchecked = "0"
                        _coli += 1
                        _ColCountMulti += 1
                    End With
                Next

                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FNSummerySkillUP75s"
                    .Caption = "FNSummerySkillUP75s"
                    .FieldName = "FNSummerySkillUP75s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSummerySkillEMP25s"
                    .Caption = "FTSummerySkillEMP25s"
                    .FieldName = "FTSummerySkillEMP25s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSummerySkillEMP50s"
                    .Caption = "FTSummerySkillEMP50s"
                    .FieldName = "FTSummerySkillEMP50s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSummerySkillEMP100s"
                    .Caption = "FTSummerySkillEMP100s"
                    .FieldName = "FTSummerySkillEMP100s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GenGridBanedMultiSkill(ByVal _dtMaster As DataTable, ByVal _dtHead As DataTable, _dtRank As DataTable)
        'Dim _dtMaster As DataTable = CreatedataTableMasterMultiSkill()
        'Dim _dtHead As DataTable = CreatedataTableMasterHeadgb()
        Dim _Str As String = "FNHSysEmpID|FTEmpName|FTYearWork|FTLeaveFlowPlan|FTLeaveOutFlowPlan|FTAbsent|FTLate|FTSumWorkStop"
        Dim StageCreateBaned1 As Boolean = False
        Dim StgGbHead As Boolean = False
        Dim _countSummary As Integer = 0
        Dim _TextckSum As String = ""

        Call CreateColgbMultiSkill(_dtMaster, _dtHead, _dtRank)
        Dim val As Integer = 0
        Try
            With ogbvSkill
                For i As Integer = .Bands.Count - 1 To 0 Step -1
                    .Bands.RemoveAt(i)
                Next

                For Each Str As String In _Str.Split("|")
                    Dim gBaned As New GridBand
                    With gBaned
                        Select Case Str.ToString
                            Case "FNHSysEmpID"
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = Str
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName(Str))
                                .Name = ogbvSkill.Name.ToString & "gb" & Str
                                .RowCount = 2
                                .Visible = False
                            Case Else
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = Str
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName(Str))
                                .Name = ogbvSkill.Name.ToString & "gb" & Str
                                .RowCount = 2
                                .Visible = True
                        End Select
                    End With
                    .Bands.Add(gBaned)
                Next

                If Not (_dtHead Is Nothing) Then
                    For Each R As DataRow In _dtHead.Rows
                        Dim gBanedHead As New GridBand
                        'If StgGbHead = False Then
                        With gBanedHead
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            .Name = "ColHead" & R!FNListIndex.ToString
                            If ST.Lang.Language = ST.Lang.eLang.TH Then
                                .Caption = R!FTNameTH.ToString
                            Else
                                .Caption = R!FTNameEN.ToString
                            End If
                            .RowCount = 1
                            .Visible = True
                        End With
                        .Bands.Add(gBanedHead)
                        'End If
                        If Not (_dtMaster Is Nothing) Then
                            For Each RowC As DataRow In _dtMaster.Select("FTNameEN='" & R!FTNameEN.ToString & "'")
                                Dim gBanedChil As New GridBand
                                With gBanedChil
                                    With .AppearanceHeader
                                        .Options.UseTextOptions = True
                                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    End With
                                    '.Name = "C" & RowC!FTSkillMatrixCode.ToString
                                    If ST.Lang.Language = ST.Lang.eLang.TH Then
                                        Dim QryTH As String = ""
                                        Try
                                            QryTH = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "CTH" & RowC!FTSkillMatrixCode.ToString & "'"
                                            HI.Conn.SQLConn.ExecuteNonQuery(QryTH, Conn.DB.DataBaseName.DB_LANG)
                                        Catch ex As Exception
                                        End Try
                                        .Name = "CTH" & RowC!FTSkillMatrixCode.ToString
                                        .Caption = RowC!FTSkillMatrixNameTH.ToString
                                    Else
                                        Dim QryEN As String = ""
                                        Try
                                            QryEN = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "CEN" & RowC!FTSkillMatrixCode.ToString & "'"
                                            HI.Conn.SQLConn.ExecuteNonQuery(QryEN, Conn.DB.DataBaseName.DB_LANG)
                                        Catch ex As Exception
                                        End Try
                                        .Name = "CEN" & RowC!FTSkillMatrixCode.ToString
                                        .Caption = RowC!FTSkillMatrixNameEN.ToString
                                    End If
                                    .Columns.Add(ogbvSkill.Columns.ColumnByFieldName(RowC!FNHSysSkillMatrixId.ToString))
                                    .RowCount = 1
                                    .Visible = True
                                End With
                                _TextckSum = R!FTNameEN.ToString
                                gBanedHead.Children.Add(gBanedChil)
                            Next

                        End If
                        If _TextckSum <> "Other Skill" Then
                            Dim gBanedChilSum As New GridBand
                            With gBanedChilSum
                                With .AppearanceHeader
                                    .Options.UseTextOptions = True
                                    .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With
                                .Name = "CSum" & _countSummary.ToString
                                .Caption = "Summary"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FNSum" & _countSummary.ToString))
                                .RowCount = 1
                                .Visible = True
                            End With
                            gBanedHead.Children.Add(gBanedChilSum)
                            _countSummary += 1
                        End If
                    Next

                    If Not (_dtRank Is Nothing) Then
                        Dim gbRankHead As New GridBand
                        With gbRankHead
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            .Name = "RankHead"
                            .Caption = "Ranking"
                            .RowCount = 1
                            .Visible = True
                        End With
                        .Bands.Add(gbRankHead)
                        For Each K As DataRow In _dtRank.Rows
                            Dim gbRank As New GridBand
                            With gbRank
                                With .AppearanceHeader
                                    .Options.UseTextOptions = True
                                    .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With
                                .Name = "gb" & K!FTNameEN.ToString
                                .Caption = "FN" & K!FTNameEN.ToString
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("cFN" & K!FTNameEN.ToString))
                                .RowCount = 1
                                .Visible = True
                            End With
                            gbRankHead.Children.Add(gbRank)
                        Next
                    End If

                    Dim gbSum75 As New GridBand
                    With gbSum75
                        With .AppearanceHeader
                            .Options.UseTextOptions = True
                            .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .TextOptions.WordWrap = True
                        End With
                        .Name = "gbFNSummerySkillUP75"
                        .Caption = "สรุปความสามารถของพนักงาน ที่ 75%"
                        .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FNSummerySkillUP75s"))
                        .RowCount = 2
                        .Visible = True
                    End With
                    .Bands.Add(gbSum75)

                    Dim gbLastCol As New GridBand
                    With gbLastCol
                        With .AppearanceHeader
                            .Options.UseTextOptions = True
                            .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                        .Name = "aggregate"
                        .Caption = "สรุปจำนวนทักษะของพนักงาน"
                        .RowCount = 1
                    End With
                    .Bands.Add(gbLastCol)
                    For i As Integer = 1 To 3 Step 1
                        Dim gbLastCol1 As New GridBand
                        With gbLastCol1
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            If i = 1 Then
                                .Name = "FTSummerySkillEMP25"
                                .Caption = "25%"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FTSummerySkillEMP25s"))
                            ElseIf i = 2 Then
                                .Name = "FTSummerySkillEMP50"
                                .Caption = "50%"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FTSummerySkillEMP50s"))
                            ElseIf i = 3 Then
                                .Name = "FTSummerySkillEMP100"
                                .Caption = "100%"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FTSummerySkillEMP100s"))
                            End If
                            .RowCount = 1
                            .Visible = True
                        End With
                        gbLastCol.Children.Add(gbLastCol1)
                    Next
                End If
            End With
            Dim oSysLang As New HI.ST.SysLanguage
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString, Me)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RemoveColMultiSkill()
        Try
            Dim x As Integer = 0
            With ogbvSkill
                For i As Integer = 0 To .Columns.Count - 1 Step 1
                    .Columns.RemoveAt(0)
                Next
            End With

        Catch ex As Exception

        End Try
    End Sub

#End Region

    Private Shared Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String
                Try
                    _TDate = CType(sender, DevExpress.XtraEditors.TimeEdit).Text
                Catch ex As Exception
                    _TDate = ""
                End Try
                CType(sender, DevExpress.XtraEditors.TimeEdit).Text = _TDate

                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, _TDate)
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Date_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                Dim _date As String = ""
                Try
                    _date = CType(sender, DevExpress.XtraEditors.DateEdit).DateTime
                Catch ex As Exception
                    _date = ""
                End Try
                If _date <> "" Then
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, _date)
                Else
                    _date = .GetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString)
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, Microsoft.VisualBasic.Left(_date, 10))
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Date_Changed(sender As Object, e As System.EventArgs)
        Try
            With ogvTrainer
                Try
                    _DateLec = CType(sender, DevExpress.XtraEditors.DateEdit).Text
                    .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_DateLec))
                Catch ex As Exception

                End Try
            End With

        Catch ex As Exception

        End Try
    End Sub

    'Private Shared Sub Date_GotFocused(sender As Object, e As System.EventArgs)
    '    Try
    '        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '            Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
    '            If _TDate = "" Then
    '                Beep()
    '            End If
    '            Try
    '                'CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
    '                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertTH(_TDate))
    '            Catch ex As Exception
    '                CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
    '            End Try

    '            If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
    '                Beep()
    '            End If1
    '        End With
    '    Catch ex As Exception
    '        HI.MG.Msg.Show(ex.Message, "WISDOM SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub wEmployeeTrainning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RemoveHandler FTDocNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler FTDocNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RepFTEvaluateName.Items.AddRange(HI.TL.CboList.SetList("TrainEvaluate"))
        ogvImg.OptionsView.ShowAutoFilterRow = False
        _FormLoad = False
        _LoadLang = True
        Me.TabMulti.PageVisible = False
        Me.ocmDeletePicture.Visible = False
        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Function SaveImage() As Boolean
        Dim Qry As String = ""
        Dim data As Byte() = Nothing
        Dim MaxSeq As Integer = 0

        Try
            'Qry = "delete " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "..THRTTrainImage where FTDocNo='" & Me.FTDocNo.Text & "'"
            'HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_HR)


            For Each strImg As String In Me.ListNameIMG.Items
                Qry = "select Max(FNSeq) FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "..THRTTrainImage WHERE FTDocNo='" & HI.UL.ULF.rpQuoted(FTDocNo.Text) & "'"
                MaxSeq = Integer.Parse(Val(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_HR, "0"))) + 1

                data = HI.UL.ULImage.ConvertImageToByteArray(strImg, UL.ULImage.PicType.Employee)

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_HR)
                HI.Conn.SQLConn.SqlConnectionOpen()

                Dim _Date As DateTime = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)
                Dim StrDate = Format(_Date, "yyyy/MM/dd")
                Dim StrTime = Format(_Date, "HH:mm:ss")

                Qry = "insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTrainImage (FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNSeq,FPImage)"
                Qry &= vbCrLf & "values(@FTInsUser,@FDInsDate,@FTInsTime,@FTDocNo,@FNSeq,@FPImage)"

                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FTInsUser", HI.ST.UserInfo.UserName).SourceColumn = "FTInsUser"
                cmd.Parameters.AddWithValue("@FDInsDate", StrDate).SourceColumn = "FDInsDate"
                cmd.Parameters.AddWithValue("@FTInsTime", StrTime).SourceColumn = "FTInsTime"
                cmd.Parameters.AddWithValue("@FTDocNo", HI.UL.ULF.rpQuoted(Me.FTDocNo.Text)).SourceColumn = "FTDocNo"
                cmd.Parameters.AddWithValue("@FNSeq", MaxSeq).SourceColumn = "FNSeq"
                cmd.Parameters.AddWithValue("@FPImage", data).SourceColumn = "FPImage"

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteImg(_Seq As Integer, _DocNo As String) As Boolean
        Dim Qry As String = ""
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Qry = "DELETE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainImage WHERE FTDocNo='" & _DocNo & "' AND FNSeq=" & _Seq & ""
            If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            End If
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainImage SET FNSeq=FNSeq-1 WHERE FNSeq>" & _Seq & " AND FTDocNo='" & _DocNo & "'"
            If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return True
            Else
                HI.Conn.SQLConn.Tran.Dispose()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return True
            End If
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub SwitchTab()
        Try
            If (TabTopicEmp.PageVisible) And (TabImg.PageVisible) Then
                Me.ocmDeletePicture.Visible = (Me.otb.SelectedTabPage.Name = TabImg.Name)
                HI.TL.METHOD.CallActiveToolBarFunction(Me)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmaddemp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmadd.Click
        Dim _RowTrain As Integer = 0
        Dim _Rec As Integer = 0
        If FTTrainCode.Text <> "" Or FNTrainType.SelectedIndex = 2 Then
            If _ChkDatatableMaster Then
                Try
                    With _FormAddEmp
                        HI.TL.HandlerControl.ClearControl(_FormAddEmp)
                        .ochkselectall.CheckState = CheckState.Unchecked
                        .ProdAdd = False
                        .ShowDialog()

                        If (.ProdAdd) Then
                            Dim _DtAdd As DataTable = CType(.ogc.DataSource, DataTable)
                            Dim _EmpSysID As String = ""
                            Dim _EmpCode As String = ""
                            Dim _EmpName As String = ""
                            Dim _Seq As Integer = 0
                            Dim _EmpNameTrain As String = ""
                            Dim Arr2(_ColCount - 1) As Integer
                            Dim ArrMultiCol(_ColCountMulti - 1)

                            If _ChkAddEmpMulti Then
                                Try
                                    For i As Integer = 0 To ArrMultiCol.Length - 1 Step 1
                                        ArrMultiCol(i) = 0
                                    Next
                                    If Not (_DtAdd Is Nothing) Then
                                        Dim _Spls As New HI.TL.SplashScreen
                                        Try
                                            Dim _TotalEmp As Integer = _DtAdd.Select("FTSelect='1'").Length
                                            For Each Ros As DataRow In _DtAdd.Select("FTSelect='1'")
                                                _Rec += 1
                                                _Spls.UpdateInformation("Save Data Employee " & Ros!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _TotalEmp.ToString & "  (" & Format((_Rec * 100.0) / _TotalEmp, "0.00") & " % ) ")
                                                Dim QryStr = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[SP_WorkMultiSkill] " & HI.ST.SysInfo.CmpID & "," & Val(Ros!FNHSysEmpID) & ""
                                                Dim _Dt As DataTable = HI.Conn.SQLConn.GetDataTable(QryStr, Conn.DB.DataBaseName.DB_HR)
                                                For Each W As DataRow In _Dt.Rows
                                                    Dim ArrP1() = {Ros!FNHSysEmpID, Ros!FTEmpName, W!WorkingAge.ToString, W!LeavePlan.ToString, W!LeaveOutPlan.ToString, W!FNAbsent, W!Late.ToString, W!TotalStopWorking.ToString}

                                                    Dim ArrFulls((ArrMultiCol.Length) + (ArrP1.Length - 1))
                                                    ArrP1.CopyTo(ArrFulls, 0)
                                                    ArrMultiCol.CopyTo(ArrFulls, ArrP1.Length)
                                                    If CType(ogcSkill.DataSource, DataTable).Select("FNHSysEmpID=" & Ros!FNHSysEmpID & "").Length <= 0 Then
                                                        CType(ogcSkill.DataSource, DataTable).Rows.Add(ArrFulls)
                                                    End If
                                                    Exit For
                                                Next
                                            Next
                                            _Rec = 0
                                            _Gencol = False
                                            _Spls.Close()
                                        Catch ex As Exception
                                            _Spls.Close()
                                        End Try
                                    End If
                                Catch ex As Exception

                                End Try
                            Else
                                Try
                                    For i As Integer = 0 To Arr2.Length - 1 Step 1
                                        Arr2(i) = 0
                                    Next
                                Catch ex As Exception

                                End Try

                                Try
                                    If Not (_DtAdd Is Nothing) Then
                                        For Each R As DataRow In _DtAdd.Select("FTSelect='1'")
                                            _EmpSysID = R!FNHSysEmpID.ToString
                                            _EmpCode = R!FTEmpCode.ToString
                                            _EmpName = R!FTEmpName.ToString
                                            Dim Arr1() As String = {_EmpCode, _EmpName}
                                            Dim Arr0() As Integer = {_EmpSysID}
                                            Dim AF((Arr0.Length) + (Arr1.Length) + (Arr2.Length - 1))

                                            Arr0.CopyTo(AF, 0)
                                            Arr1.CopyTo(AF, Arr0.Length)
                                            Arr2.CopyTo(AF, Arr1.Length + 1)

                                            If CType(Me.ogcTopicEmp.DataSource, DataTable).Select("FNHSysEmpID=" & Val(_EmpSysID) & "").Length <= 0 Then
                                                CType(Me.ogcTopicEmp.DataSource, DataTable).Rows.Add(AF)
                                            End If
                                        Next
                                    End If
                                Catch ex As Exception

                                End Try
                            End If
                        End If

                    End With
                Catch ex As Exception

                End Try
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTTrainCode_lbl.Text)
                Me.FTTrainCode.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTTrainCode_lbl.Text)
            Me.FTTrainCode.Focus()
        End If

    End Sub

    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        Dim Qry As String = ""
        Dim _WhereId As Integer = 0
        Try
            If GTrainNameActiveRemoveList Then
                With ogvTrainer
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                    '_WhereId = .GetRowCellValue(.FocusedRowHandle, "FNSeq")
                    'Qry = "delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNSeq=" & _WhereId & ""
                    'If HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_HR) Then
                    CType(Me.ogcTrainer.DataSource, DataTable).Rows.RemoveAt(.FocusedRowHandle)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    'Else
                    'CType'(Me.ogcTrainer.DataSource, DataTable).Rows.RemoveAt(.FocusedRowHandle)
                    'End If

                End With
            ElseIf GEmpNameActiveRemoveList Then
                With ogvTopicEmp
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                    '_WhereId = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")
                    ' Qry = "delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & _WhereId & ""
                    'If HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_HR) Then
                    CType(Me.ogcTopicEmp.DataSource, DataTable).Rows.RemoveAt(.FocusedRowHandle)
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    'Else
                    CType(Me.ogcTopicEmp.DataSource, DataTable).Rows.RemoveAt(.FocusedRowHandle)
                    'End If
                End With
            ElseIf GBMultiActiveRemoveList Then
                With ogbvSkill
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

                    ' _WhereId = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")
                    'Qry = "delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & _WhereId & ""
                    'If HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_HR) Then
                    CType(Me.ogcSkill.DataSource, DataTable).Rows.RemoveAt(.FocusedRowHandle)
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    'Else
                    'CType(Me.ogcSkill.DataSource, DataTable).Rows.RemoveAt(.FocusedRowHandle)
                    'End If

                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Time_EditvalueChanged(sender As Object, e As EventArgs) Handles FTStartTime.EditValueChanged, FTEndTime.EditValueChanged
        Dim _TimeHr As Integer = 0
        Dim _TimeMin As Double = 0.0
        Dim Morrning As String = "12:00"
        Dim AFF As String = "13:00"
        Dim Affter As String = "17:00"
        Dim _TotalDays As Integer = 0
        Try
            If FTStartTime.Text <> "" And FTEndTime.Text <> "" And FTStartTime.Text < FTEndTime.Text Then

                If FTStartTime.Text <> "" And FTEndTime.Text <> "" And FTStartTime.Text <= Morrning And FTEndTime.Text <= Morrning Then
                    '08.00-12.00
                    _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & " " & FTStartTime.Text), CDate(Me.ActualDate & "  " & FTEndTime.Text))
                ElseIf FTStartTime.Text <> "" And FTEndTime.Text <> "" And FTStartTime.Text >= Morrning And FTEndTime.Text <= Affter Then
                    '13.00-17.00
                    _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & " " & FTStartTime.Text), CDate(Me.ActualDate & "  " & FTEndTime.Text))
                ElseIf FTStartTime.Text <> "" And FTEndTime.Text <> "" And FTStartTime.Text <= Morrning And FTEndTime.Text <= Affter And FTEndTime.Text >= Morrning And FTEndTime.Text >= AFF Then
                    '8.00-17.00
                    _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & " " & FTStartTime.Text), CDate(Me.ActualDate & "  " & FTEndTime.Text))
                    _T1 -= 60
                Else
                    _T1 = -1
                End If
                If _T1 > 0 Then
                    _TimeHr = Math.Floor(_T1 / 60)
                    _TimeMin = _T1 Mod 60
                    _TotalDays = (Me.FDDateEnd.DateTime - Me.FDDateBegin.DateTime).Days + 1
                    If _TotalDays >= 2 Then
                        _TimeHr *= _TotalDays
                        _T1 *= _TotalDays
                        _Timstr = String.Format("{0:00}", _TimeHr) & ":" & String.Format("{0:00}", _TimeMin)
                        Me.TotalTime.Text = _Timstr
                    Else
                        _Timstr = String.Format("{0:00}", _TimeHr) & ":" & String.Format("{0:00}", _TimeMin)
                        Me.TotalTime.Text = _Timstr
                    End If

                Else
                    Me.TotalTime.Text = "00:00"
                End If
            Else
                Me.TotalTime.Text = "00:00"
            End If
        Catch ex As Exception
            Me.TotalTime.Text = "00:00"
        End Try

    End Sub

    'Private Sub _Values_EditValueChanged(sender As Object, e As EventArgs) Handles _Values.EditValueChanged
    '    If _Values.Text = "0:0" Then
    '        TotalTime.Text = "0:00"
    '    Else
    '        TotalTime.Text = _Timstr
    '    End If
    'End Sub

    Private Sub RepPreTest_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepPreTest.EditValueChanging
        Dim _pre As Double
        Dim _PercenPre As Double
        Try
            With ogv
                _pre = Double.Parse(Val(e.NewValue))
                If _pre > 0 Then
                    If Me.PointMaster.Value <= 0 Then
                        .SetRowCellValue(.FocusedRowHandle, "FNPercenPre", 0.0)
                    Else
                        _PercenPre = (_pre / Me.PointMaster.Value) * 100
                        .SetRowCellValue(.FocusedRowHandle, "FNPercenPre", _PercenPre)
                    End If
                Else
                    .SetRowCellValue(.FocusedRowHandle, "FNPercenPre", 0.0)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepPostTest_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepPostTest.EditValueChanging
        Dim _post As Double
        Dim _PercenPost As Double
        Dim _Stage As Integer
        Dim _Qry As String = ""
        Try
            With ogv
                _post = Double.Parse(Val(e.NewValue))
                If _post > 0 Then
                    If PointMaster.Value <= 0 Then
                        .SetRowCellValue(.FocusedRowHandle, "FNPercenPost", 0.0)
                    Else
                        _PercenPost = (_post / Me.PointMaster.Value) * 100
                        .SetRowCellValue(.FocusedRowHandle, "FNPercenPost", _PercenPost)
                    End If
                Else
                    .SetRowCellValue(.FocusedRowHandle, "FNPercenPost", 0.0)
                End If

                If _PercenPost >= Me.FNRegulation.Value Then
                    _Stage = 1
                Else
                    _Stage = 2
                End If
                _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
                _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData"
                _Qry &= vbCrLf & "WHERE     (FTListName = 'TrainEvaluate')"
                _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_Stage)
                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
                If dt.Rows.Count > 0 Then
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        .SetRowCellValue(.FocusedRowHandle, "FTEvaluateName", dt.Rows(0)!FTNameTH.ToString)
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FTEvaluateName", dt.Rows(0)!FTNameEN.ToString)
                    End If
                End If
                .SetRowCellValue(.FocusedRowHandle, "FTEvaluateName_Hide", _Stage)
            End With
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub FPDialog_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FPDialog.ButtonClick
    '    Dim _FileImg As String = ""
    '    Select Case e.Button.Index
    '        Case 0
    '            Dim OPN As New OpenFileDialog

    '            With OPN
    '                .Filter = "JPG Image|*.jpg|Bitmap Image|*.bmp|Png Image|*.png"
    '                .Multiselect = True
    '                .RestoreDirectory = True
    '                .ShowDialog()
    '            End With
    '            Try
    '                If System.Windows.Forms.DialogResult.OK Then
    '                    Me.ListNameIMG.Items.Clear()
    '                    Me.FPDialog.Text = ""
    '                    For Each i As String In OPN.FileNames
    '                        Me.ListNameIMG.Items.Add(i)
    '                    Next
    '                    For Each filesImg As String In ListNameIMG.Items
    '                        filesImg = filesImg.Replace(filesImg.Substring(0, filesImg.LastIndexOf("\") + 1), "")
    '                        _FileImg += filesImg & "|"
    '                    Next
    '                    Me.FPDialog.Text = _FileImg
    '                End If
    '            Catch ex As Exception

    '            End Try

    '    End Select
    'End Sub
    Private Sub SetGridIMG()
        With ogvImg
            .OptionsCustomization.AllowRowSizing = True
            .RowHeight = 150
        End With
    End Sub

    Private Sub otb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otb.SelectedPageChanged
        'If (_FormLoad) Then Exit Sub
        'If Not (TabTopicEmp.PageVisible) Then Exit Sub
        'If Not (TabImg.PageVisible) Then Exit Sub
        'If (TabMulti.PageVisible) Then Exit Sub
        SwitchTab()
    End Sub

    Private Sub ocmAddPicture_Click(sender As Object, e As EventArgs) Handles ocmAddPicture.Click
        'Dim _FileImg As String = ""
        Dim OPN As New OpenFileDialog
        With OPN
            .Filter = "JPG Image|*.jpg|Bitmap Image|*.bmp|Png Image|*.png"
            .Multiselect = True
            .RestoreDirectory = True
            .ShowDialog()
        End With
        Try
            If System.Windows.Forms.DialogResult.OK Then
                Me.ListNameIMG.Items.Clear()
                For Each i As String In OPN.FileNames
                    Me.ListNameIMG.Items.Add(i)
                Next
                'For Each filesImg As String In ListNameIMG.Items
                '    filesImg = filesImg.Replace(filesImg.Substring(0, filesImg.LastIndexOf("\") + 1), "")
                '    _FileImg += filesImg & "|"
                'Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmDeletePicture_Click(sender As Object, e As EventArgs) Handles ocmDeletePicture.Click
        Dim _Seq As Integer = 0
        Dim _DocNo As String = ""

        With ogvImg
            If .FocusedRowHandle >= 0 Then
                _Seq = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq")))
                _DocNo = "" & .GetRowCellValue(.FocusedRowHandle, "FTDocNo").ToString
                If (DeleteImg(_Seq, _DocNo)) Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    LoadImg()
                End If
            End If
        End With
    End Sub

    Private Sub RepFTStartTime_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTStartTime.EditValueChanging
        _Time1 = e.NewValue
    End Sub

    Private Sub RepFTEndTime_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTEndTime.EditValueChanging
        _Time2 = e.NewValue
    End Sub

    Private Sub ogvTrainer_Click(sender As Object, e As EventArgs) Handles ogvTrainer.Click
        If _FormLoad Then Exit Sub
        GTrainNameActiveRemoveList = True
        GEmpNameActiveRemoveList = False
        GBMultiActiveRemoveList = False
    End Sub

    Private Sub ogv_Click(sender As Object, e As EventArgs) Handles ogv.Click
        If _FormLoad Then Exit Sub
        GTrainNameActiveRemoveList = False
        GBMultiActiveRemoveList = False
        GEmpNameActiveRemoveList = True
    End Sub

    Private Sub ogbvSkill_Click(sender As Object, e As EventArgs) Handles ogbvSkill.Click
        If _FormLoad Then Exit Sub
        GTrainNameActiveRemoveList = False
        GBMultiActiveRemoveList = True
        GEmpNameActiveRemoveList = False
    End Sub

    Private Sub ogvTrainer_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvTrainer.FocusedColumnChanged
        If _FormLoad Then Exit Sub

        Dim _TimeHr As Double = 0.0
        Dim _TimeMin As Double = 0.0
        Dim stTime As String = ""
        Dim edTime As String = ""
        Dim Morrning As String = "12:00"
        Dim AFF As String = "13:00"
        Dim Affter As String = "17:00"

        Select Case e.PrevFocusedColumn.FieldName.ToString
            Case "FTStartTime", "FTEndTime"
                Try
                    With ogvTrainer
                        If _Time1 <> "" Then
                            .SetRowCellValue(.FocusedRowHandle, e.PrevFocusedColumn, Format(CDate(_Time1), "HH:mm"))
                            '_Time1 = ""
                        End If
                        If _Time2 <> "" Then
                            .SetRowCellValue(.FocusedRowHandle, e.PrevFocusedColumn, Format(CDate(_Time2), "HH:mm"))
                            '_Time2 = ""
                        End If

                        If _Time1 = Nothing Then
                            _Time1 = "" & .GetRowCellValue(.FocusedRowHandle, "FTStartTime")
                        Else
                            '_Time1 = "" & .GetRowCellValue(.FocusedRowHandle, "FTStartTime")
                        End If
                        If _Time2 = Nothing Then
                            _Time2 = "" & .GetRowCellValue(.FocusedRowHandle, "FTEndTime")
                        Else
                            '_Time2 = "" & .GetRowCellValue(.FocusedRowHandle, "FTEndTime")
                        End If

                    End With
                    If _Time1 <> "" And _Time2 <> "" Then
                        stTime = Format(CDate(_Time1), "HH:mm")
                        edTime = Format(CDate(_Time2), "HH:mm")

                        If stTime > edTime Then
                            ogvTrainer.SetRowCellValue(ogvTrainer.FocusedRowHandle, "FTTotalHour", "00:00")
                            Exit Sub
                        Else
                            If stTime <= Morrning And edTime <= Morrning Then
                                _TimeGrid = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & " " & stTime), CDate(Me.ActualDate & "  " & edTime))
                            End If
                            If stTime >= Morrning And edTime <= Affter Then
                                _TimeGrid = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & " " & stTime), CDate(Me.ActualDate & "  " & edTime))
                            End If
                            If stTime <= Morrning And edTime <= Affter And edTime > Morrning And edTime >= AFF Then
                                _TimeGrid = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & " " & stTime), CDate(Me.ActualDate & "  " & edTime))
                                _TimeGrid -= 60
                            End If

                        End If
                        If _TimeGrid > 0 Then
                            Me.FNTotalMinute.Value = _TimeGrid
                            _TimeHr = Microsoft.VisualBasic.Left(_TimeGrid / 60, 2)
                            _TimeMin = _TimeGrid Mod 60
                            _Timstr = String.Format("{0:00}", _TimeHr) & ":" & String.Format("{0:00}", _TimeMin)
                            Me._TG.Text = _Timstr

                            ogvTrainer.SetRowCellValue(ogvTrainer.FocusedRowHandle, "FTTotalHour", Me._TG.Text)
                            ogvTrainer.SetRowCellValue(ogvTrainer.FocusedRowHandle, "FNTotalMinute", Me.FNTotalMinute.Value)
                            _Time1 = ""
                            _Time2 = ""
                            _TimeGrid = 0
                        Else
                            ogvTrainer.SetRowCellValue(ogvTrainer.FocusedRowHandle, "FTTotalHour", "0:00")
                            ogvTrainer.SetRowCellValue(ogvTrainer.FocusedRowHandle, "FNTotalMinute", 0)
                        End If

                    End If
                Catch ex As Exception
                End Try
        End Select
    End Sub

    Private Sub FTDocNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTDocNo.EditValueChanged
        Call LoadTrainInfo(FTDocNo.Text)
        If FNTrainType.SelectedIndex = 2 Then
            If CType(ogcSkill.DataSource, DataTable).Rows.Count > 0 Then
                For i As Integer = CType(ogcSkill.DataSource, DataTable).Rows.Count - 1 To 0 Step -1
                    CType(ogcSkill.DataSource, DataTable).Rows.RemoveAt(i)
                Next
            End If
            LoadDataMultiSkill()
            Call LoadTrainName()
        Else
            'Call LoadTotalpointMaster()
            Try
                If CType(ogcTopicEmp.DataSource, DataTable).Rows.Count > 0 Then
                    For i As Integer = ogvTopicEmp.RowCount - 1 To 0 Step -1
                        CType(ogcTopicEmp.DataSource, DataTable).Rows.RemoveAt(i)
                    Next
                End If
            Catch ex As Exception

            End Try

            Call LoadDataEmpTopic()
            Call LoadTrainName()
            Call LoadImg()
        End If

        'Me.otb.SelectedTabPageIndex = 0
    End Sub

    Private Sub FTTrainCode_EditValueChanged(sender As Object, e As EventArgs) Handles FTTrainCode.EditValueChanged
        If FNTrainType.SelectedIndex <> 2 Then

            'Call LoadTotalpointMaster()
            Call RemoveColTopicEmp()
            Dim _DtColEmpTopic As DataTable = CreatedatatableTopicEmp()

            If _DtColEmpTopic.Rows.Count > 0 Then
                _ChkDatatableMaster = True
                If ogvTrainer.RowCount <= 0 Then
                    Call GenrowTrainName()
                End If
                Call GenGridBannedTopicEmp(_DtColEmpTopic)
                Dim dt As DataTable = LoadDatatable()
                ogcTopicEmp.DataSource = Nothing
                ogcTopicEmp.DataSource = dt
            Else
                _ChkDatatableMaster = False
                With ogvTopicEmp
                    For i As Integer = .Bands.Count - 1 To 0 Step -1
                        .Bands.RemoveAt(i)
                    Next
                End With
                Exit Sub
            End If
        Else
            _SectType = True
            Call FNTrainType_SelectedIndexChanged(FNTrainType, New System.EventArgs)

        End If
    End Sub

    Private Sub FNTrainType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNTrainType.SelectedIndexChanged
        If (_FormLoad) Then Exit Sub

        If Me.FNTrainType.SelectedIndex = 2 Then
            _dtMulti = Nothing
            Me.ocmAddPicture.Visible = False
            Me.ocmDeletePicture.Visible = False
            Me.TabTopicEmp.PageVisible = False
            Me.TabImg.PageVisible = False
            Me.TabMulti.PageVisible = True
            HI.TL.METHOD.CallActiveToolBarFunction(Me)
            ChkBtn = 0
            _ChkAddEmpMulti = True
            'Call LoadTrainName()
            Call RemoveColMultiSkill()
            dtHead = CreatedataTableMasterHeadgb()
            dtcol = CreatedataTableMasterMultiSkill()
            dtRank = CreatedatatableRanking()
            If dtcol.Rows.Count > 0 Then
                _ChkDatatableMaster = True
            Else
                _ChkDatatableMaster = False
                Exit Sub
            End If
            Call GenGridBanedMultiSkill(dtcol, dtHead, dtRank)
            _Gencol = False
            _dtMulti = LoadDatatablePrepareMultiSkill()
            ogcSkill.DataSource = _dtMulti
            'LoadDataMultiSkill()
            If CType(ogcTrainer.DataSource, DataTable).Rows.Count <= 0 Then
                Call GenrowTrainName()
            End If
        Else
            If ChkBtn = 0 Then
                _dtMulti = Nothing
                Me.ocmAddPicture.Visible = True
                Me.TabTopicEmp.PageVisible = True
                Me.TabImg.PageVisible = True
                Me.TabMulti.PageVisible = False
                HI.TL.METHOD.CallActiveToolBarFunction(Me)
                ChkBtn = 1
                _ChkAddEmpMulti = False
            End If
        End If
    End Sub

    Private Function LoadDatatable() As DataTable
        Dim Qry As String = ""
        Dim dt As New DataTable
        Dim FieldName As String = ""
        Dim Fields As String = ""

        With ogvTopicEmp
            For i As Integer = 0 To .Columns.Count - 1 Step 1
                dt.Columns.Add(.Columns(i).FieldName.ToString)
            Next
        End With
        Return dt

    End Function

    Private Function LoadDatatablePrepareMultiSkill() As DataTable
        Dim Qry As String = ""
        Dim dt As New DataTable

        With ogbvSkill
            For i As Integer = 0 To .Columns.Count - 1 Step 1
                dt.Columns.Add(.Columns(i).FieldName.ToString)
            Next
        End With
        Return dt
    End Function

    'Private Sub ogcTopicEmp_KeyDown(sender As Object, e As KeyEventArgs) Handles ogcTopicEmp.KeyDown
    '    Dim col As Integer
    '    Try
    '        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
    '            col = ogvTopicEmp.FocusedColumn.AbsoluteIndex
    '            ogvTopicEmp.FocusedColumn = ogvTopicEmp.VisibleColumns(col)
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub

    Private Sub ogvTopicEmp_Click(sender As Object, e As EventArgs) Handles ogvTopicEmp.Click
        If _FormLoad Then Exit Sub
        GTrainNameActiveRemoveList = False
        GEmpNameActiveRemoveList = True
    End Sub

    'ปุ่มทดลอง โค้ด
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        Dim totalDays = ((Me.FDDateEnd.DateTime - Me.FDDateBegin.DateTime).Days) + 1
        Dim _Str As String = ""
        _Str = "fjkd;ajf;a"
        'GenGridBannedTopicEmp()
        'GenGridBanedMultiSkill()

        'SaveEmpMulti()
        'Dim _Val As Integer = 0
        'Dim _colDivi As Integer = 0
        'Dim _cal As Integer = 0
        'Dim _Sum As Integer = 0
        'Dim _NetSum As Integer = 0
        'Dim _SumAll As Integer = 0
        'Dim _NetAll As Integer = 0
        'Dim _colDiviAll As Integer = 0
        'Dim Qry As String = ""
        'Dim dtList As DataTable
        'Dim _Val25 As Integer = 0
        'Dim _Val50 As Integer = 0
        'Dim _Val75 As Integer = 0
        'Dim _Val100 As Integer = 0
        'Dim _ChkValCount As Integer = 0

        'Qry = "Select FNListIndex"
        'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNSkillMatrix'"
        'dtList = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        'With ogbvSkill
        '    For Row As Integer = 0 To .RowCount - 1 Step 1

        '        For a As Integer = 0 To dtList.Rows.Count - 1 Step 1
        '            _Val = 0
        '            _colDivi = 0
        '            For k As Integer = .Columns.Count - 1 To 0 Step -1
        '                If Microsoft.VisualBasic.Left(.Columns(k).Name.ToString, 2) = "FN" And (Microsoft.VisualBasic.Left(.Columns(k).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(k).FieldName.ToString, 5)) <> "FTSum" And _
        '                    Microsoft.VisualBasic.Right(.Columns(k).FieldName.ToString, 1) = a.ToString Then
        '                    If .Columns(k).Name.ToString <> "FNSum" & a.ToString Then
        '                        _Val += .GetRowCellValue(Row, .Columns(k).FieldName.ToString)
        '                        _colDivi += 1
        '                    End If
        '                End If
        '            Next
        '            _Sum = _Val / _colDivi

        '            If _Sum > 0 And _Sum < 25 Then
        '                _NetSum = 0
        '            ElseIf _Sum >= 25 And _Sum <= 49 Then
        '                _NetSum = 25
        '            ElseIf _Sum >= 50 And _Sum <= 74 Then
        '                _NetSum = 50
        '            ElseIf _Sum >= 75 And _Sum <= 99 Then
        '                _NetSum = 75
        '            Else
        '                _NetSum = 100
        '            End If
        '            .SetRowCellValue(Row, "FNSum" & a.ToString, _NetSum)
        '        Next

        '        For Each R As DataRow In dtHead.Rows
        '            For c As Integer = .Columns.Count - 1 To 0 Step -1
        '                If Microsoft.VisualBasic.Right(.Columns(c).FieldName.ToString, 1) = R!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(c).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(c).FieldName.ToString, 5)) <> "FTSum" Then
        '                    If .Columns(c).Name.ToString <> "FNSum" & R!FNListIndex.ToString Then
        '                        _SumAll += .GetRowCellValue(Row, .Columns(c).FieldName.ToString)
        '                        _colDiviAll += 1
        '                    End If
        '                End If
        '            Next
        '        Next
        '        _NetAll = _SumAll / _colDiviAll
        '        If _NetAll >= 81 Then
        '            .SetRowCellValue(Row, "cFNExcellence", "1")
        '            .SetRowCellValue(Row, "cFNGood", "0")
        '            .SetRowCellValue(Row, "cFNFair", "0")
        '            .SetRowCellValue(Row, "cFNBeginner", "0")
        '        ElseIf _NetAll >= 71 And _NetAll <= 80 Then
        '            .SetRowCellValue(Row, "cFNGood", "1")
        '            .SetRowCellValue(Row, "cFNExcellence", "0")
        '            .SetRowCellValue(Row, "cFNFair", "0")
        '            .SetRowCellValue(Row, "cFNBeginner", "0")
        '        ElseIf _NetAll >= 69 And _NetAll <= 70 Then
        '            .SetRowCellValue(Row, "cFNFair", "1")
        '            .SetRowCellValue(Row, "cFNGood", "0")
        '            .SetRowCellValue(Row, "cFNExcellence", "0")
        '            .SetRowCellValue(Row, "cFNBeginner", "0")
        '        ElseIf _NetAll >= 0 And _NetAll <= 60 Then
        '            .SetRowCellValue(Row, "cFNBeginner", "1")
        '            .SetRowCellValue(Row, "cFNFair", "0")
        '            .SetRowCellValue(Row, "cFNGood", "0")
        '            .SetRowCellValue(Row, "cFNExcellence", "0")
        '        End If

        '        For Each RR As DataRow In dtHead.Rows
        '            For i As Integer = .Columns.Count - 1 To 0 Step -1
        '                If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = RR!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
        '                    _ChkValCount = .GetRowCellValue(Row, .Columns(i).FieldName.ToString)
        '                    If _ChkValCount = 25 Then
        '                        _Val25 += 1
        '                    ElseIf _ChkValCount = 50 Then
        '                        _Val50 += 1
        '                    ElseIf _ChkValCount = 75 Then
        '                        _Val75 += 1
        '                    ElseIf _ChkValCount = 100 Then
        '                        _Val100 += 1
        '                    End If
        '                End If
        '            Next
        '        Next
        '        .SetRowCellValue(Row, "FTSummerySkillEMP25s", _Val25)
        '        .SetRowCellValue(Row, "FTSummerySkillEMP50s", _Val50)
        '        .SetRowCellValue(Row, "FNSummerySkillUP75s", _Val75)
        '        .SetRowCellValue(Row, "FTSummerySkillEMP100s", _Val100)

        '    Next

        'End With




        'LoadDataEmpTopic()
        'If SaveEmpdata() Then
        '    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        'Else
        '    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        'End If
        'Dim QryFix As String = ""

        'Try
        '    For Each R As DataRow In CType(ogcTopicEmp.DataSource, DataTable).Rows
        '        For Each Col As DataColumn In CType(ogcTopicEmp.DataSource, DataTable).Columns
        '            Select Case Col.ColumnName.ToString
        '                Case "FNHSysEmpID", "FTEmpCode", "FTEmpName"
        '                Case Else
        '                    If Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 3) = "Pre" Then
        '                        QryFix = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic"
        '                        QryFix &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
        '                        QryFix &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
        '                        QryFix &= vbCrLf & ",FNPrePoint=" & Val(R.Item(Col.ColumnName)) & ""
        '                        QryFix &= vbCrLf & "WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & " AND FNHSysTrainTopicId=" & Col.ColumnName.ToString.Substring(0, 10) & " "
        '                        If HI.Conn.SQLConn.ExecuteNonQuery(QryFix, Conn.DB.DataBaseName.DB_HR) <= 0 Then
        '                            QryFix = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic"
        '                            QryFix &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNHSysEmpID,FNHSysTrainTopicId,FNPrePoint)"
        '                            QryFix &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'"
        '                            QryFix &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        '                            QryFix &= vbCrLf & ",'" & Me.FTDocNo.Text & "'"
        '                            QryFix &= vbCrLf & "," & Val(R!FNHSysEmpID.ToString) & ""
        '                            QryFix &= vbCrLf & "," & Val(Col.ColumnName.ToString.Substring(0, 10)) & ""
        '                            QryFix &= vbCrLf & "," & Val(R.Item(Col.ColumnName.ToString)) & ""
        '                        End If
        '                    End If
        '                    If Microsoft.VisualBasic.Right(Col.ColumnName.ToString, 4) = "Post" Then
        '                        QryFix = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpTopic"
        '                        QryFix &= vbCrLf & "SET FNPostPoint=" & Val(R.Item(Col.ColumnName.ToString)) & ""
        '                        QryFix &= vbCrLf & "WHERE FNHSysTrainTopicId=" & Col.ColumnName.ToString.Substring(0, 10) & " AND FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""

        '                    End If
        '            End Select
        '            HI.Conn.SQLConn.ExecuteNonQuery(QryFix, Conn.DB.DataBaseName.DB_HR)
        '        Next
        '    Next
        '    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        'Catch ex As Exception
        '    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        'End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        'LoadDataMultiSkill()
        'Dim _Qry As String = ""
        '_Qry = "select YY.FNHSysEmpID,YY.FTEmpName,YY.Working as WorkingAge"
        '_Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiOutPlanHrs<10 and YY.LeaveBusiOutPlanHrs>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanHrs) ELSE"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanHrs=0 THEn '00' Else convert(varchar(5),YY.LeaveBusiOutPlanHrs)END END+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin<10 and YY.LeaveBusiOutPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanMin) ELSE"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin=0 THEN '00' Else convert(varchar(2),YY.LeaveBusiOutPlanMin) END END AS LeaveOutPlan"

        '_Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiPlanHrs<10 and YY.LeaveBusiPlanHrs>0 THEN '0'+convert(varchar(5),LeaveBusiPlanHrs)else "
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanHrs=0 THEN '00' ELSE convert(varchar(5),YY.LeaveBusiPlanHrs)END END+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin<10 and YY.LeaveBusiPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiPlanMin)else"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin=0 THEN '00' ELSE convert(varchar(2),YY.LeaveBusiPlanMin) END END AS LeavePlan"

        '_Qry &= vbCrLf & ",Case WHEn YY.LateHrs<10 and YY.LateHrs>0  THEN '0'+convert(varchar(2),YY.LateHrs) else Case WHEN YY.LateHrs=0 THEn '00' Else convert(varchar(5),YY.LateHrs)end  end+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LateMin<10 and YY.LateMin>0 THEN '0'+convert(varchar(2),YY.LateMin)else Case WHEN YY.LateMin=0 THEN '00' Else convert(varchar(2),YY.LateMin) END END AS Late"


        '_Qry &= vbCrLf & ",Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs<10 and YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs>0"
        '_Qry &= vbCrLf & "then'0'+convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs)else "
        '_Qry &= vbCrLf & "Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs) END END +':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin<10 and YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin>0"
        '_Qry &= vbCrLf & "then'0'+convert(varchar(5),YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)else "
        '_Qry &= vbCrLf & "Case WHEN YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin=0 THEN '00' else convert(varchar(5),YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin) END END AS TotalStopWorking"

        '_Qry &= vbCrLf & ",Case WHEN YY.FNAbsentHrs<10 and YY.FNAbsentHrs>0 THEN '0'+convert(varchar(2),YY.FNAbsentHrs) else Case WHEN YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.FNAbsentHrs)END ENd+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.FNAbsentMin<10 and YY.FNAbsentMin>0 THEN '0'+convert(varchar(2),YY.FNAbsentMin)else Case WHEN YY.FNAbsentMin=0 THEN '00' Else convert(varchar(2),YY.FNAbsentMin) END END AS FNAbsent"

        '_Qry &= vbCrLf & "from"
        '_Qry &= vbCrLf & "(select XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.FTEmpName,XX.Working"
        '_Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)/60) AS LateHrs"
        '_Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)%60) AS LateMin"
        '_Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)/60) AS LeaveBusiPlanHrs"
        '_Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)%60) AS LeaveBusiPlanMin"
        '_Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)/60) AS LeaveBusiOutPlanHrs"
        '_Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)%60) AS LeaveBusiOutPlanMin"
        '_Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)/60) AS FNAbsentHrs"
        '_Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)%60) AS FNAbsentMin"

        '_Qry &= vbCrLf & "from "
        '_Qry &= vbCrLf & "(select  KK.FTSelect,KK.FNHSysEmpID,KK.FTEmpCode,KK.FTEmpName"
        '_Qry &= vbCrLf & ",convert(varchar(2),KK.FNEmpWorkAgeYear)+':'+convert(varchar(2),KK.FNEmpWorkAgeMoth) AS Working"
        '_Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)<=0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveVacation+KK.LeaveMaternity  AS LeaveBusiPlan"
        '_Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)>0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveSick + KK.FNAbsent AS LeaveBusiOutPlan"
        '_Qry &= vbCrLf & ",KK.FNLateNormalMin"
        '_Qry &= vbCrLf & ",KK.FNAbsent"


        '_Qry &= vbCrLf & "from"
        '_Qry &= vbCrLf & "(Select distinct '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        'Else
        '    _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        'End If
        '_Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)/12) AS FNEmpWorkAgeYear"
        '_Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)%12) AS FNEmpWorkAgeMoth"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='1') ,0) AS LeaveBusi"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='98'),0) AS LeaveVacation"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='97'),0) AS LeaveMaternity"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='0'),0) AS LeaveSick"
        '_Qry &= vbCrLf & ",T.FNTimeMin,T.FNAbsent,T.FNLateNormalMin"

        '_Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix AS Multi with(nolock) LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON Multi.FNHSysEmpID=M.FNHSysEmpID"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) ON M.FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "WHERE Multi.FTDocNo=''"
        '_Qry &= vbCrLf & ") as KK"
        '_Qry &= vbCrLf & ") AS XX"
        '_Qry &= vbCrLf & "group by XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.Working,XX.FTEmpName ) AS YY"
        '_Qry &= vbCrLf & "  ORDER BY YY.FNHSysEmpID ASC "

    End Sub

    'Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
    '    Dim Qry As String = ""

    '    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '    Try
    '        For Each R As DataRow In CType(ogcTrainer.DataSource, DataTable).Rows
    '            Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer"
    '            Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
    '            Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
    '            Qry &= vbCrLf & "FTTrainer='" & R!FTTrainerName.ToString & "',FDDateBegin='" & HI.UL.ULDate.ConvertEnDB(R!FDStartDate.ToString) & "',FDDateEnd='" & HI.UL.ULDate.ConvertEnDB(R!FDEndDate.ToString) & "'"
    '            Qry &= vbCrLf & "FTStartTime='" & R!FTStartTime.ToString & "',FTEndTime='" & R!FTEndTime.ToString & "',FTTotalHour='" & R!FTTotalHour.ToString & "',FNTotalMinute=" & R!FNTotalMinute.ToString & ""
    '            Qry &= vbCrLf & "WHERE FTDocNo='" & Me.FTDocNo.Text & "' AND FNSeq=" & Val(R!FNSeq.ToString) & ""
    '            If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainLecturer"
    '                Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FTDocNo,FNSeq,FTTrainer,FDDateBegin,FDDateEnd,FTStartTime,FTEndTime,FTTotalHour,FNTotalMinute)"
    '                Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'"
    '                Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
    '                Qry &= vbCrLf & ",'" & Me.FTDocNo.Text & "'," & Val(R!FNSeq.ToString) & ",'" & R!FTTrainerName.ToString & "'"
    '                Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDStartDate.ToString) & "','" & HI.UL.ULDate.ConvertEnDB(R!FDEndDate.ToString) & "'"
    '                Qry &= vbCrLf & ",'" & R!FTStartTime.ToString & "','" & R!FTEndTime.ToString & "','" & R!FTTotalHour.ToString & "'," & R!FNTotalMinute.ToString & ""
    '            End If
    '            If HI.Conn.SQLConn.ExecuteTran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                'Return False
    '            End If
    '        Next
    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        'Return True
    '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
    '    End Try
    'End Sub

    'Private Sub ogvTrainer_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ogvTrainer.ShowingEditor
    '    Try
    '        With ogvTrainer
    '            Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
    '            If .FocusedColumn.FieldName.ToString = "FDStartDate" Then
    '                If _TDate = "" Then
    '                    Beep()
    '                End If
    '                Try

    '                    Dim _obj As Object = .Columns.ColumnByFieldName("FDStartDate").ColumnEdit
    '                    DirectCast(_obj, DevExpress.XtraEditors.DateEdit).DateTime = _TDate

    '                    'Dim SD As DevExpress.XtraEditors.DateEdit = CType(Convert.ChangeType(RepFDStartDate, GetType(DevExpress.XtraEditors.DateEdit)), DevExpress.XtraEditors.DateEdit)
    '                    'SD.DateTime = _TDate

    '                    '.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertTH(_TDate))
    '                Catch ex As Exception
    '                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
    '                End Try

    '                If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
    '                    Beep()
    '                End If
    '            End If

    '            If .FocusedColumn.FieldName.ToString = "FDEndDate" Then
    '                If _TDate = "" Then
    '                    Beep()
    '                End If
    '                Try
    '                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
    '                    '.SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertTH(_TDate))
    '                Catch ex As Exception
    '                    CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
    '                End Try

    '                If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
    '                    Beep()
    '                End If
    '            End If
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub RepFDStartDate_Click(sender As Object, e As EventArgs) Handles RepFDStartDate.Click
    '    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '        Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
    '        'If _TDate = "" Then
    '        '    Beep()
    '        'End If
    '        Try
    '            CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
    '        Catch ex As Exception
    '            CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
    '        End Try

    '        'If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
    '        '    Beep()
    '        'End If
    '    End With

    'End Sub

    'Private Sub RepFDEndDate_Click(sender As Object, e As EventArgs) Handles RepFDEndDate.Click
    '    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

    '        Dim _TDate As String = HI.UL.ULDate.ConvertEnDB(.GetFocusedRowCellValue(.FocusedColumn))
    '        'If _TDate = "" Then
    '        '    Beep()
    '        'End If
    '        Try
    '            CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
    '        Catch ex As Exception
    '            CType(sender, DevExpress.XtraEditors.DateEdit).Text = ""
    '        End Try

    '        'If CType(sender, DevExpress.XtraEditors.DateEdit).Text = "" Then
    '        '    Beep()
    '        'End If
    '    End With
    'End Sub

    Private Sub GenrowTrainName()
        Try
            With CType(ogcTrainer.DataSource, DataTable)
                .Rows.Add()
                .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
                .Rows(.Rows.Count - 1)!FTTrainerName = ""
                .Rows(.Rows.Count - 1)!FDStartDate = ""
                .Rows(.Rows.Count - 1)!FDEndDate = ""
                .Rows(.Rows.Count - 1)!FTStartTime = ""
                .Rows(.Rows.Count - 1)!FTEndTime = ""
                .Rows(.Rows.Count - 1)!FTTotalHour = ""
                .Rows(.Rows.Count - 1)!FNTotalMinute = 0
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvTrainer_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvTrainer.KeyDown
        Dim JK As Integer = 0
        Dim FK As Integer = 1
        Dim _focus As Integer = 0
        Dim _PositCol As Integer = 0
        Try
            Select Case e.KeyCode
                Case Keys.Down, Keys.Enter
                    With CType(ogcTrainer.DataSource, DataTable)
                        .AcceptChanges()
                        JK = .Select("FTTrainerName<>'' AND FDStartDate<>'' AND FDEndDate<>'' AND FTStartTime<>'' AND FTEndTime<>''").Length
                        FK = .Rows.Count
                        _PositCol = ogvTrainer.Columns.Count - 2
                        _focus = ogvTrainer.FocusedColumn.AbsoluteIndex
                        If .Select("FTTrainerName<>'' AND FDStartDate<>'' AND FDEndDate<>'' AND FTStartTime<>'' AND FTEndTime<>''").Length = FK And _PositCol = _focus Then
                            Call GenrowTrainName()
                            .AcceptChanges()
                            With ogvTrainer
                                .ClearSelection()
                                .SelectRow(.RowCount - 1)
                                .FocusedRowHandle = .RowCount - 1
                                .FocusedColumn = .Columns.ColumnByFieldName("FTTrainerName")
                            End With
                        End If
                    End With
                Case Keys.Delete
                    Me.ogvTrainer.DeleteRow(Me.ogvTrainer.FocusedRowHandle)
                    With CType(ogcTrainer.DataSource, DataTable)
                        .AcceptChanges()
                        Dim _RIndx As Integer = 0
                        For Each R As DataRow In .Rows
                            _RIndx += 1
                            R!FNSeq = _RIndx
                        Next
                        .AcceptChanges()

                    End With
                Case Keys.Insert
                    If CType(ogcTrainer.DataSource, DataTable).Rows.Count <= 0 Then
                        Call GenrowTrainName()
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogbvSkill_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogbvSkill.FocusedColumnChanged
        Dim _Val25 As Integer = 0
        Dim _Val50 As Integer = 0
        Dim _Val75 As Integer = 0
        Dim _Val100 As Integer = 0
        Dim _ChkValCount As Integer = 0
        If Not (_Gencol) Then
            Try
                With ogbvSkill
                    _colfocus = Microsoft.VisualBasic.Right(e.FocusedColumn.ToString, 1)
                    _colSkip = e.FocusedColumn.ToString


                    For Each RR As DataRow In dtHead.Rows
                        For i As Integer = .Columns.Count - 1 To 0 Step -1
                            If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = RR!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                                _ChkValCount = .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)
                                If _ChkValCount = 25 Then
                                    _Val25 += 1
                                ElseIf _ChkValCount = 50 Then
                                    _Val50 += 1
                                ElseIf _ChkValCount = 75 Then
                                    _Val75 += 1
                                ElseIf _ChkValCount = 100 Then
                                    _Val100 += 1
                                End If
                            End If
                        Next
                    Next
                    .SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP25s", _Val25)
                    .SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP50s", _Val50)
                    .SetRowCellValue(.FocusedRowHandle, "FNSummerySkillUP75s", _Val75)
                    .SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP100s", _Val100)
                End With

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub RepChanging(sender As Object, e As System.EventArgs)
        Dim _Str As String = ""
        Dim _cal As Integer = 0
        'Dim _Val As Integer = 0
        Dim _Val25 As Integer = 0 : Dim _Val50 As Integer = 0 : Dim _Val75 As Integer = 0 : Dim _Val100 As Integer = 0
        Dim _C25 As Integer = 0 : Dim _C50 As Integer = 0 : Dim _C75 As Integer = 0 : Dim _C100 As Integer = 0
        Dim _Sum As Integer = 0
        Dim _colDivi As Integer = 0
        Dim _colDiviAll As Integer = 0
        Dim _SumAll As Integer = 0
        Dim _NetSum As Integer = 0
        Dim _NetAll As Integer = 0
        Dim _Listindex As Integer = 0
        Try
            With ogbvSkill
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    _cal = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                    _Str = Microsoft.VisualBasic.Right(CType(sender, DevExpress.XtraEditors.CalcEdit).Properties.Name, 1)

                    For i As Integer = .Columns.Count - 1 To 0 Step -1
                        If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = _colfocus And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                            If .Columns(i).Name.ToString <> _colSkip Then
                                '_Val += .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)

                                If .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 25 Then
                                    _C25 += 1
                                ElseIf .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 50 Then
                                    _C50 += 1
                                ElseIf .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 75 Then
                                    _C75 += 1
                                ElseIf .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 100 Then
                                    _C100 += 1
                                End If
                            End If
                            '_colDivi += 1
                        End If
                    Next
                    If _cal = 25 Then
                        _C25 += 1
                    ElseIf _cal = 50 Then
                        _C50 += 1
                    ElseIf _cal = 75 Then
                        _C75 += 1
                    ElseIf _cal = 100 Then
                        _C100 += 1
                    End If
                End With
                If _C25 > 0 Then
                    _Val25 = _C25 * 1
                End If
                If _C50 > 0 Then
                    _Val50 = _C50 * 2
                End If
                If _C75 > 0 Then
                    _Val75 = _C75 * 3
                End If
                If _C100 > 0 Then
                    _Val100 = _C100 * 4
                End If
                If _Str = "0" Then
                    _NetSum = (_Val25 + _Val50 + _Val75 + _Val100) * 3
                ElseIf _Str = "1" Then
                    _NetSum = (_Val25 + _Val50 + _Val75 + _Val100) * 2
                ElseIf _Str = "2" Then
                    _NetSum = (_Val25 + _Val50 + _Val75 + _Val100) * 1
                End If
                '_Sum = (_cal + _Val) / _colDivi

                'If _Sum >= 0 And _Sum < 25 Then
                '    _NetSum = 0
                'ElseIf _Sum >= 25 And _Sum <= 49 Then
                '    _NetSum = 25
                'ElseIf _Sum >= 50 And _Sum <= 74 Then
                '    _NetSum = 50
                'ElseIf _Sum >= 75 And _Sum <= 99 Then
                '    _NetSum = 75
                'Else
                '    _NetSum = 100
                'End If
                .SetRowCellValue(.FocusedRowHandle, "FNSum" & _Str, _NetSum)
                For Each R As DataRow In dtHead.Rows
                    For i As Integer = .Columns.Count - 1 To 0 Step -1
                        'If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = R!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                        If .Columns(i).FieldName.ToString = "FNSum" & R!FNListIndex.ToString Then
                            'If .Columns(i).Name.ToString <> _colSkip Then
                            _SumAll += .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)
                            'End If
                        End If
                        '_colDiviAll += 1
                        'End If
                    Next
                Next
                '_NetAll = (_SumAll + _cal) / _colDiviAll
                If _SumAll = 0 Then
                    .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                    .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                    .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                    .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                Else
                    _NetAll = (_SumAll / 148) * 100

                    If _NetAll >= 76 And _NetAll <= 100 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                    ElseIf _NetAll >= 51 And _NetAll <= 75 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                    ElseIf _NetAll >= 26 And _NetAll <= 50 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                    ElseIf _NetAll >= 0 And _NetAll <= 25 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                    End If
                End If


                'For Each RR As DataRow In dtHead.Rows
                '    For i As Integer = .Columns.Count - 1 To 0 Step -1
                '        If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = RR!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                '            _ChkValCount = .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)
                '            If _ChkValCount = 25 Or _cal = 25 Then
                '                _Val25 += 1
                '            ElseIf _ChkValCount = 50 Or _cal = 50 Then
                '                _Val50 += 1
                '            ElseIf _ChkValCount = 75 Or _cal = 75 Then
                '                _Val75 += 1
                '            ElseIf _ChkValCount = 100 Or _cal = 100 Then
                '                _Val100 += 1
                '            End If
                '        End If
                '    Next
                'Next
                '.SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP25s", _Val25)
                '.SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP50s", _Val50)
                '.SetRowCellValue(.FocusedRowHandle, "FNSummerySkillUP75s", _Val75)
                '.SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP100s", _Val100)

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvTopicEmp_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvTopicEmp.FocusedColumnChanged
        Try
            With ogvTopicEmp
                _colSkipTopic = .Columns(e.FocusedColumn.AbsoluteIndex).FieldName.ToString
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepPre_Changed(sender As Object, e As System.EventArgs)
        Dim _NetvalPre As Double = 0.0
        Dim _Val As Double = 0.0
        Dim _countPre As Integer = 0
        Dim _cal As Double = 0.0
        Dim Qry As String = ""
        Dim _Point As Integer = 0
        Try
            Qry = "select top 1 FNPoint From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTrainTopic where FTTrainCode='" & Me.FTTrainCode.Text & "'"
            _Point = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_HR)
            With ogvTopicEmp
                _cal = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                _Val = 0.0
                _countPre = 0.0
                For x As Integer = .Columns.Count - 1 To 0 Step -1
                    If (Microsoft.VisualBasic.Left(.Columns(x).FieldName.ToString, 3)) = "Pre" Then
                        If .Columns(x).FieldName.ToString <> _colSkipTopic Then
                            _Val += .GetRowCellValue(.FocusedRowHandle, .Columns(x).FieldName.ToString)
                        End If
                        _countPre += 1
                    End If
                Next
                If _Val <= 0 And _cal <= 0 Then
                    _NetvalPre = 0
                Else
                    _NetvalPre = ((_cal + _Val) / _Point) * 100
                End If
                '.SetRowCellValue(.FocusedRowHandle, "FNTotalPre", _NetvalPre)
                .SetRowCellValue(.FocusedRowHandle, "FNTotalPre", String.Format(CultureInfo.InvariantCulture, "{0:0.00}", _NetvalPre))

            End With
        Catch ex As Exception

        End Try


    End Sub

    Private Sub RepPost_Changed(sender As Object, e As System.EventArgs)
        Dim _NetvalPost As Double = 0.0
        Dim _Val As Double = 0.0
        Dim _countPost As Integer = 0
        Dim _cal As Double = 0.0
        Dim Qry As String = ""
        Dim _Point As Integer = 0
        Try
            Qry = "select top 1 FNPoint From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTrainTopic where FTTrainCode='" & Me.FTTrainCode.Text & "'"
            _Point = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_HR)
            With ogvTopicEmp
                _cal = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                _Val = 0.0
                _countPost = 0.0
                For x As Integer = .Columns.Count - 1 To 0 Step -1
                    If (Microsoft.VisualBasic.Left(.Columns(x).FieldName.ToString, 4)) = "Post" Then
                        If .Columns(x).FieldName.ToString <> _colSkipTopic Then
                            _Val += .GetRowCellValue(.FocusedRowHandle, .Columns(x).FieldName.ToString)
                        End If
                        _countPost += 1
                    End If
                Next
                If _Val <= 0 And _cal <= 0 Then
                    _NetvalPost = 0
                Else
                    _NetvalPost = ((_cal + _Val) / _Point) * 100
                End If
                .SetRowCellValue(.FocusedRowHandle, "FNTotalPost", String.Format(CultureInfo.InvariantCulture, "{0:0.00}", _NetvalPost))
                If _NetvalPost >= Me.FNRegulation.Value Then
                    _StatePass = 1

                Else
                    _StatePass = 2
                End If
                Qry = "SELECT"
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    Qry &= vbCrLf & "FTNameTH AS Name"
                Else
                    Qry &= vbCrLf & "FTNameEN AS Name"
                End If
                Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WHERE FTListName='TrainEvaluate' AND FNListIndex=" & _StatePass & ""
                Dim _Str As String = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_SYSTEM)


                If _StatePass = 2 Then
                    .SetRowCellValue(.FocusedRowHandle, "FTEvaluate", _Str)
                    .SetRowCellValue(.FocusedRowHandle, "FTState", _StatePass.ToString)
                ElseIf _StatePass = 1 Then
                    .SetRowCellValue(.FocusedRowHandle, "FTEvaluate", _Str)
                    .SetRowCellValue(.FocusedRowHandle, "FTState", _StatePass.ToString)
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CbolistChagned(sender As Object, e As System.EventArgs)
        Dim _Stage As String = ""
        Try
            With ogvTopicEmp
                'CType(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex = _StatePass
                _Stage = CType(sender, DevExpress.XtraEditors.ComboBoxEdit).SelectedIndex
                .SetRowCellValue(.FocusedRowHandle, "FTState", _Stage)
            End With
            CType(ogcTopicEmp.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FDDateEnd_EditValueChanged(sender As Object, e As EventArgs) Handles FDDateEnd.EditValueChanged
        If _FormLoad Then Exit Sub
        If Me.FDDateEnd.DateTime < Me.FDDateBegin.DateTime And Me.FDDateBegin.Text <> "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกวันที่ให้ถูกต้อง", 1607121010, Me.Text, Me.FDDateEnd_lbl.Text)
            If Me.FDDateBegin.Text <> "" Then
                Me.FDDateEnd.Text = Me.FDDateBegin.Text
            End If
            Me.FDDateEnd.Focus()
        End If

    End Sub

    Private Sub FDDateBegin_EditValueChanged(sender As Object, e As EventArgs) Handles FDDateBegin.EditValueChanged
        If _FormLoad Then Exit Sub
        If Me.FDDateBegin.DateTime > Me.FDDateEnd.DateTime And Me.FDDateEnd.Text <> "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกวันที่ให้ถูกต้อง", 1607121017, Me.Text, Me.FDDateBegin_lbl.Text)
            If Me.FDDateBegin.Text <> "" Then
                Me.FDDateBegin.Text = Me.FDDateEnd.Text
            End If
            Me.FDDateBegin.Focus()
        End If

    End Sub

    Private Sub otb_Click(sender As Object, e As EventArgs) Handles otb.Click

    End Sub
End Class