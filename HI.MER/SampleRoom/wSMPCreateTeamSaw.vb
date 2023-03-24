Imports System.Drawing
Imports DevExpress.XtraEditors.Controls
Imports System.ComponentModel

Public Class wSMPCreateTeamSaw

    Private _GenJobProd As wSMPCreateTeamSawAdd


    Private _StateSubNew As Boolean = False
    Private _TFNMarkSpare As Double = 2.0

    Sub New()
        _StateSubNew = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _GenJobProd = New wSMPCreateTeamSawAdd
        HI.TL.HandlerControl.AddHandlerObj(_GenJobProd)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenJobProd.Name.ToString.Trim, _GenJobProd)
        Catch ex As Exception
        Finally
        End Try

        Call InitEmpData()

        _StateSubNew = False
    End Sub


#Region "Property"

#End Region

#Region "Procedure"

    Public Sub SetInfo(ByVal Key As Object)
        '...call by another form name zzz...
        FTSMPOrderNo.Text = Key.ToString
    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)
        If (_StateSubNew) Then Exit Sub
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        otbjobprod.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = "SELECT FTTeam "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Order By FTTeam  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTTeam.ToString
                .Text = R!FTTeam.ToString
            End With

            otbjobprod.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbjobprod.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()



        _Spls.Close()
    End Sub

    Private Sub InitGrid()


        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvBreakdown.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next


        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvemp.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next

        With ogvBreakdown
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvemp
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
    End Sub
    Private Sub InitEmpData()
        'Dim cmd As String = ""
        'Dim dtemp As DataTable

        'cmd = "  Select   B.FNHSysEmpID "
        'cmd &= vbCrLf & "  , B.FTEmpCode "

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    cmd &= vbCrLf & "  , B.FTEmpNameTH + ' ' + B.FTEmpSurnameTH AS FTEmpName  "
        'Else
        '    cmd &= vbCrLf & "  , B.FTEmpNameEN + ' ' +   B.FTEmpSurnameEN AS FTEmpName  "
        'End If

        'cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As B WITH(NOLOCK) "
        'cmd &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U WITH(NOLOCK) ON B.FNHSysUnitSectId = U.FNHSysUnitSectId "
        'cmd &= vbCrLf & "   Where U.FTStateSampleRoom ='1' AND U.FTStateSew ='1' "
        'cmd &= vbCrLf & "   AND (ISNULL(B.FDDateEnd,'') ='' OR ISNULL(B.FDDateEnd,'') >" & HI.UL.ULDate.FormatDateDB & ") "
        'cmd &= vbCrLf & " ORDER BY  B.FTEmpCode "

        'dtemp = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_HR)

        'RepFTEmpCode.DataSource = dtemp.Copy
        'RepFTEmpName.DataSource = dtemp.Copy

        'dtemp.Dispose()

    End Sub

#End Region

#Region "Function"
    Private Function DeleteOrderProd(OrderProdKey As Object) As Boolean

        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " Delete  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam  WHERE FTTeam='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamBreakdown"
            _Qry &= vbCrLf & " WHERE FTTeam='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp"
            _Qry &= vbCrLf & " WHERE FTTeam='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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



    Private Function SaveOrderProd(OrderTeamKey As Object) As Boolean


        Dim _Qry As String = ""
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try


            _Qry = "  UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam SET  FTRemartk ='" & HI.UL.ULF.rpQuoted(FTRemartk.Text) & "' "
            _Qry &= vbCrLf & " WHERE FTTeam='" & HI.UL.ULF.rpQuoted(OrderTeamKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If


            _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp"
            _Qry &= vbCrLf & " WHERE FTTeam='" & HI.UL.ULF.rpQuoted(OrderTeamKey.ToString) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If


            Dim dtprod As DataTable

            With CType(ogcemp.DataSource, DataTable)
                .AcceptChanges()
                dtprod = .Copy
            End With



            For Each R As DataRow In dtprod.Select("FNHSysEmpID_Hide > 0", "FNSeq")


                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp  SET "
                _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "  ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "  ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE FTTeam='" & HI.UL.ULF.rpQuoted(OrderTeamKey.ToString) & "' "
                _Qry &= vbCrLf & "  AND  FNHSysEmpID=" & Val(R!FNHSysEmpID_Hide) & " "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamEmp "
                    _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,FTSMPOrderNo, FTTeam, FNSeq, FNHSysEmpID)"
                    _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderTeamKey) & "' "
                    _Qry &= vbCrLf & "," & Val(R!FNSeq) & ""
                    _Qry &= vbCrLf & "," & Val(R!FNHSysEmpID_Hide) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return False
                    End If


                End If


            Next

            dtprod.Dispose()
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

    Private Sub wCreateJobProduction_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try


        Catch ex As Exception
        End Try


    End Sub

    Private Sub wCreateJobProduction_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        FTSMPOrderNo.Focus()
        FTSMPOrderNo.SelectAll()
        otbdetail.SelectedTabPageIndex = 0

    End Sub

    Private Sub ocmgeneratejobprod_Click(sender As Object, e As EventArgs) Handles ocmgeneratejobprod.Click
        If Me.FTSMPOrderNo.Text <> "" And FTSMPOrderNo.Properties.Tag.ToString <> "" Then
            'If (CheckProcess()) Then
            '    Exit Sub
            'End If
            With _GenJobProd
                .OrderNo = Me.FTSMPOrderNo.Text
                .JobProdNo = ""
                Call HI.ST.Lang.SP_SETxLanguage(_GenJobProd)

                .ShowDialog()

                If (.Process) Then
                    Call LoadOrderProdDataInfo(Me.FTSMPOrderNo.Text)
                    Me.otbdetail.SelectedTabPageIndex = 0
                End If

            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
            FTSMPOrderNo.SelectAll()
        End If
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDataInfo(FTSMPOrderNo.Text)
            Me.otbdetail.SelectedTabPageIndex = 0
            Call InitEmpData()

        End If
    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbjobprod.SelectedTabPage Is Nothing) Then

                    Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)


                Else
                    ogdBreakdown.DataSource = Nothing
                    ogcemp.DataSource = Nothing
                    FTRemartk.Text = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadOrderProdDetail(Key As Object)
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogdBreakdown.DataSource = Nothing
        ogcemp.DataSource = Nothing
        FTRemartk.Text = ""

        cmd = "SELECT TOP 1  FTTeam,FTRemartk   "
        cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS P With(Nolock)"
        cmd &= vbCrLf & "  WHERE FTTeam='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        For Each R As DataRow In _dtprod.Rows

            FTRemartk.Text = R!FTRemartk.ToString

        Next

        _dtprod.Dispose()

        cmd = "  Select   B.FTSizeBreakDown"
        cmd &= vbCrLf & "  ,ISNULL(B.FNQuantity,0) As FNQuantity "
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTDeliveryDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTDeliveryDate),103) ELSE '' END AS FTDeliveryDate"
        cmd &= vbCrLf & " ,ISNULL(B.FTRemark,'') AS FTRemark,A.FNSeq"
        cmd &= vbCrLf & " FROM  (SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X3.FNQuantity,X2.FTDeliveryDate,X2.FTRemark"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPSampleTeamBreakdown AS X3 WITH(NOLOCK) ON X2.FTSMPOrderNo=X3.FTSMPOrderNo"
        cmd &= vbCrLf & "  AND X2.FTColorway=X3.FTColorway"
        cmd &= vbCrLf & "  AND X2.FTSizeBreakDown=X3.FTSizeBreakDown"
        cmd &= vbCrLf & " Where X3.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        cmd &= vbCrLf & " AND X3.FTTeam ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        cmd &= vbCrLf & ") AS B "
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq,X32.FTMatSizeCode AS FTSizeBreakDown From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "

        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        Me.ogdBreakdown.DataSource = _dtprod.Copy

        cmd = "  Select  ROW_NUMBER() Over(Order BY A.FNSeq) As FNSeq "
        cmd &= vbCrLf & "  , A.FNHSysEmpID AS FNHSysEmpID_Hide "
        cmd &= vbCrLf & "  , B.FTEmpCode AS FNHSysEmpID "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmd &= vbCrLf & "  , B.FTEmpNameTH + ' ' + B.FTEmpSurnameTH AS FTEmpName  "
        Else
            cmd &= vbCrLf & "  , B.FTEmpNameEN + ' ' +   B.FTEmpSurnameEN AS FTEmpName  "
        End If

        cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPSampleTeamEmp As A WITH(NOLOCK) INNER Join "
        cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As B WITH(NOLOCK) On A.FNHSysEmpID = B.FNHSysEmpID "
        cmd &= vbCrLf & "    Where A.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
        cmd &= vbCrLf & "          And A.FTTeam ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        cmd &= vbCrLf & " ORDER BY  A.FNSeq "



        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        Me.ogcemp.DataSource = _dtprod.Copy

        Call InitGridEmp()

        _dtprod.Dispose()

    End Sub


    Private Sub InitGridEmp()


        If Not (Me.ogcemp.DataSource Is Nothing) Then

            Dim dtemp As DataTable


            With CType(Me.ogcemp.DataSource, DataTable)
                .AcceptChanges()


                If .Select("FNHSysEmpID=''").Length <= 0 Then
                    .Rows.Add(.Rows.Count + 1, 0, "", "")
                End If



                ' dtemp = .Copy


            End With

            'If dtemp.Select("FTEmpCode=''").Length <= 0 Then
            '    dtemp.Rows.Add(dtemp.Rows.Count + 1, 0, "", "")
            'End If

            'Me.ogcemp.DataSource = dtemp.Copy

            'dtemp.Dispose()
        End If


    End Sub


    Private Sub ocmdeletejobprod_Click(sender As Object, e As EventArgs) Handles ocmdeletejobprod.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            If (CheckProcess(Me.otbjobprod.SelectedTabPage.Name.ToString)) Then
                Exit Sub
            End If
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบ ทีมงานตัวอย่าง ใช่หรือไม่ ?", 1909180003, Me.otbjobprod.SelectedTabPage.Name.ToString) = True Then

                If Me.DeleteOrderProd(Me.otbjobprod.SelectedTabPage.Name.ToString) Then
                    Call LoadOrderProdDataInfo(Me.FTSMPOrderNo.Text)
                End If

            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก ทีมงานตัวอย่าง ที่ต้องการลบ ", 1809187701, Me.Text)
        End If

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click


        Call LoadOrderProdDataInfo(FTSMPOrderNo.Text)
        Me.otbdetail.SelectedTabPageIndex = 0
        Call InitEmpData()

    End Sub


    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub

    Private Sub otbsuborder_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function CheckProcess(datateam As String) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        'cmd = "select top 1 FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess AS x with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(datateam) & "'"

        cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(datateam) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then
            'HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึกกระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)

            HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึกจบกระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809214846, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)


        End If

        Return stateprocess
    End Function
    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then

            If (CheckProcess(Me.otbjobprod.SelectedTabPage.Name.ToString)) Then
                Exit Sub
            End If

            If Me.SaveOrderProd(Me.otbjobprod.SelectedTabPage.Name.ToString) Then
                Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Name.ToString)

                HI.MG.ShowMsg.mInfo("บันทึกข้อมูล ทีมงานตัวอย่าง เรียบร้อย ", 1909180903, Me.Text)
            End If


        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก ทีมงานตัวอย่าง ที่ต้องการบันทึกข้อมูล ", 1809187791, Me.Text)
        End If
    End Sub

    Private Sub RepFTEmpCode_EditValueChanged(sender As Object, e As EventArgs)
        Try

            With Me.ogvemp
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)

                With CType(Me.ogcemp.DataSource, DataTable)

                    If .Select("FTEmpCode='" & HI.UL.ULF.rpQuoted(obj.GetColumnValue("FTEmpCode").ToString) & "'").Length <= 0 Then
                        Me.ogvemp.SetFocusedRowCellValue("FTEmpName", obj.GetColumnValue("FTEmpName").ToString)
                        Me.ogvemp.SetFocusedRowCellValue("FNHSysEmpID_Hide", obj.GetColumnValue("FNHSysEmpID").ToString)
                        Me.ogvemp.SetFocusedRowCellValue("FNHSysEmpID", obj.GetColumnValue("FTEmpCode").ToString)
                        .AcceptChanges()
                        InitGridEmp()



                    Else
                        'Me.ogvemp.SetFocusedRowCellValue("FTEmpName", "")
                        'Me.ogvemp.SetFocusedRowCellValue("FNHSysEmpID", 0)
                        'Me.ogvemp.SetFocusedRowCellValue("FTEmpCode", "")
                        'CType(Me.ogcemp.DataSource, DataTable).AcceptChanges()
                    End If

                End With

            End With





        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvemp_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvemp.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Delete
                With Me.ogvemp
                    If .FocusedRowHandle < 0 Then Exit Sub

                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcemp.DataSource, DataTable)
                    .AcceptChanges()
                    .BeginInit()
                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNSeq>0", "FNSeq")
                        R!FNSeq = Ridx

                        Ridx = Ridx + 1
                    Next
                    .EndInit()
                    .AcceptChanges()
                End With
                InitGridEmp()
            Case System.Windows.Forms.Keys.Down
                InitGridEmp()
        End Select
    End Sub

    Private Sub otbjobprod_Click(sender As Object, e As EventArgs) Handles otbjobprod.Click

    End Sub

    Private Sub ogcemp_Click(sender As Object, e As EventArgs) Handles ogcemp.Click

    End Sub

    Private Sub RepFTEmpCode_PopupFilter(sender As Object, e As DevExpress.XtraEditors.Controls.PopupFilterEventArgs)
        If String.IsNullOrEmpty(e.SearchText) Then
            Return
        End If
        Dim edit As DevExpress.XtraEditors.LookUpEdit = TryCast(sender, DevExpress.XtraEditors.LookUpEdit)
        Dim propertyDescriptors As System.ComponentModel.PropertyDescriptorCollection = System.Windows.Forms.ListBindingHelper.GetListItemProperties(edit.Properties.DataSource)
        Dim operators As IEnumerable(Of DevExpress.Data.Filtering.FunctionOperator) = propertyDescriptors.OfType(Of PropertyDescriptor)().Select(Function(t) New DevExpress.Data.Filtering.FunctionOperator(DevExpress.Data.Filtering.FunctionOperatorType.Contains, New DevExpress.Data.Filtering.OperandProperty(t.Name), New DevExpress.Data.Filtering.OperandValue(e.SearchText)))
        e.Criteria = New DevExpress.Data.Filtering.GroupOperator(DevExpress.Data.Filtering.GroupOperatorType.Or, operators)
        e.SuppressSearchCriteria = True
    End Sub

    Private Sub ogcemp_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcemp.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvemp
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcemp.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNSeq>0", "FNSeq")
                        R!FNSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridEmp()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridEmp()

            Case Else

        End Select

        e.Handled = True
    End Sub

    '    Private Sub RepFTEmpName_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTEmpName.EditValueChanged, RepFTEmpCode.EditValueChanged

    '        Static _Proc As Boolean

    '        Try

    '            If Not (_Proc) Then
    '                _Proc = True

    '                Try
    '                    With Me.ogvemp
    '                        If .FocusedRowHandle < 0 Then Exit Sub

    '                        Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)

    '                        With CType(Me.ogcemp.DataSource, DataTable)

    '                            If .Select("FTEmpCode='" & HI.UL.ULF.rpQuoted(obj.GetColumnValue("FTEmpCode").ToString) & "'").Length <= 0 Then
    '                                Me.ogvemp.SetFocusedRowCellValue("FTEmpName", obj.GetColumnValue("FTEmpCode").ToString)
    '                                Me.ogvemp.SetFocusedRowCellValue("FNHSysEmpID", obj.GetColumnValue("FNHSysEmpID").ToString)
    '                                Me.ogvemp.SetFocusedRowCellValue("FTEmpCode", obj.GetColumnValue("FTEmpCode").ToString)
    '                                .AcceptChanges()
    '                                InitGridEmp()
    '                            Else
    '                                ' Me.ogvemp.SetFocusedRowCellValue("FTEmpName", "")
    '                                Me.ogvemp.SetFocusedRowCellValue("FNHSysEmpID", 0)
    '                                ' Me.ogvemp.SetFocusedRowCellValue("FTEmpCode", "")
    '                                'CType(Me.ogcemp.DataSource, DataTable).AcceptChanges()
    '                            End If

    '                        End With

    '                    End With
    '                Catch ex As Exception

    '                End Try

    '                _Proc = False
    '            End If

    '        Catch ex As Exception
    '            _Proc = False
    '        End Try
    '    End Sub
End Class