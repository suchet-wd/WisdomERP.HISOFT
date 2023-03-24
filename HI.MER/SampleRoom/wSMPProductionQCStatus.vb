Imports DevExpress.XtraEditors.Controls

Public Class wSMPProductionQCStatus


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



    Public Sub LoadDataInfo(ByVal Key As String)

        Dim _Qry As String = ""
        _Qry = " SELECT     SOP.FNSeq"

        _Qry &= vbCrLf & "  , Case When ISDATE(ISNULL( SOP.FTDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime, SOP.FTDate),103) ELSE '' END AS  FTDate "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNQuantity,0) As FNQuantity"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) As FNPass"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNNotPass,0) As FNNotPass"
        _Qry &= vbCrLf & " ,  ISNULL(SOP.FTRemark,'') AS FTRemark"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) +  ISNULL(SOP.FNNotPass,0) As FNTotalQC"

        _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'') As FTSizeBreakDown"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTColorway,'') As FTColorway"
        _Qry &= vbCrLf & "  ,ISNULL(SOP.FTSizeBreakDown,'')  +'-'+ISNULL(SOP.FTColorway,'') AS FTKey"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC AS SOP WITH (NOLOCK)"
        _Qry &= vbCrLf & "   WHERE SOP.FTTeam='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

        Call InitNewRow()
    End Sub


    Private Sub InitNewRow()



        If Not (Me.ogcoperation.DataSource Is Nothing) Then

            Dim dtemp As DataTable

            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()
                dtemp = .Copy

            End With

            If dtemp.Select("FNQuantity<=0 ").Length <= 0 Then

                If dtemp.Select("FNQuantity<>FNTotalQC ").Length <= 0 Then

                    With CType(Me.ogcoperation.DataSource, DataTable)
                        .Rows.Add()
                        .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
                        .Rows(.Rows.Count - 1)!FTDate = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                        .Rows(.Rows.Count - 1)!FNQuantity = 0
                        .Rows(.Rows.Count - 1)!FNPass = 0
                        .Rows(.Rows.Count - 1)!FNNotPass = 0
                        .Rows(.Rows.Count - 1)!FTRemark = ""
                        .Rows(.Rows.Count - 1)!FNTotalQC = 0
                        .Rows(.Rows.Count - 1)!FTSizeBreakDown = ""
                        .Rows(.Rows.Count - 1)!FTColorway = ""
                        .Rows(.Rows.Count - 1)!FTKey = ""




                        .AcceptChanges()

                    End With

                End If


            End If

            dtemp.Dispose()
        End If

    End Sub
#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvoperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvoperation.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogcoperation.DataSource, DataTable)

                    .AcceptChanges()


                    Call InitNewRow()



                End With
            Case System.Windows.Forms.Keys.Delete

                Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)
                With CType(Me.ogcoperation.DataSource, DataTable)
                    .AcceptChanges()
                    Dim _RIndx As Integer = 0
                    For Each R In .Rows
                        _RIndx = _RIndx + 1
                        R!FNSeq = _RIndx
                    Next
                    .AcceptChanges()
                End With


            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub


    Private Function CheckProcess(key As String, Optional showmsg As Boolean = True) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTTeam='" & HI.UL.ULF.rpQuoted(key) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then

            If showmsg Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return stateprocess
    End Function



    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        If Not (otbjobprod.SelectedTabPage Is Nothing) And FTSMPOrderNo.Text <> "" Then


            Dim key As String = otbjobprod.SelectedTabPage.Name.ToString
            If (CheckProcess(key)) Then
                Exit Sub
            End If


            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)

                Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC WHERE FTTeam ='" & HI.UL.ULF.rpQuoted(key.ToString) & "'  "
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    For Each R As DataRow In .Select("FNQuantity>0", "FNSeq")

                        _FNSeq = _FNSeq + 1

                        _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTTeam, FTDate, FNSeq,FTSizeBreakDown,FTColorway, FNQuantity, FNPass,FNNotPass, FTRemark)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(key.ToString) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FTDate.ToString) & "'"
                        _Qry &= vbCrLf & " ," & _FNSeq & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        _Qry &= vbCrLf & " ," & Val(R!FNQuantity.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNPass.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNNotPass.ToString) & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            _Spls.Close()

                            Exit Sub

                        End If

                    Next

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Try
                        Call ocmrefresh_Click(ocmrefresh, New System.EventArgs)
                    Catch ex As Exception
                    End Try

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If

    End Sub


    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        ogvBreakdown.OptionsView.ShowAutoFilterRow = False
        ogvBreakdown.OptionsSelection.MultiSelect = False
        ogvBreakdown.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect


        ogvoperation.OptionsView.ShowAutoFilterRow = False
        ogvoperation.OptionsSelection.MultiSelect = False
        ogvoperation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect


        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvBreakdown.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next


        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvoperation.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next


    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDetail(otbjobprod.SelectedTabPage.Text)
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        FTStateFinish.Checked = False
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        otbjobprod.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = "SELECT FTTeam "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS P With(Nolock)"
        _Qry &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
        _Qry &= vbCrLf & "  Order By FTTeam  "

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

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

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDataInfo(FTSMPOrderNo.Text)


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
                    FTStateFinish.Checked = False
                    ogcoperation.DataSource = Nothing
                    ogdBreakdown.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadOrderProdDetail(Key As Object)
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogdBreakdown.DataSource = Nothing
        ogcoperation.DataSource = Nothing
        FTStateFinish.Checked = False


        cmd = "  Select   B.FTSizeBreakDown"
        cmd &= vbCrLf & "  ,ISNULL(B.FNQuantity,0) As FNQuantity "
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTDeliveryDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTDeliveryDate),103) ELSE '' END AS FTDeliveryDate"
        cmd &= vbCrLf & " ,ISNULL(B.FTRemark,'') AS FTRemark,A.FNSeq,B.FTSizeBreakDown+'-'+ ISNULL(B.FTColorway,'') FTKey"
        cmd &= vbCrLf & " FROM (SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X3.FNQuantity,X2.FTDeliveryDate,X2.FTRemark"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPSampleTeamBreakdown AS X3 WITH(NOLOCK) ON X2.FTSMPOrderNo=X3.FTSMPOrderNo"
        cmd &= vbCrLf & "  AND X2.FTColorway=X3.FTColorway"
        cmd &= vbCrLf & "  AND X2.FTSizeBreakDown=X3.FTSizeBreakDown"
        cmd &= vbCrLf & " Where X3.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        cmd &= vbCrLf & " AND X3.FTTeam ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        cmd &= vbCrLf & ") AS B "
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq,X32.FTMatSizeCode AS FTSizeBreakDown  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "

        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        Me.ogdBreakdown.DataSource = _dtprod.Copy



        cmd = "  Select    A.FNSeq,B.FTSizeBreakDown"
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway,B.FTSizeBreakDown+'-'+ ISNULL(B.FTColorway,'') FTKey"
        cmd &= vbCrLf & " FROM (SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X3.FNQuantity,X2.FTDeliveryDate,X2.FTRemark"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPSampleTeamBreakdown AS X3 WITH(NOLOCK) ON X2.FTSMPOrderNo=X3.FTSMPOrderNo"
        cmd &= vbCrLf & "  AND X2.FTColorway=X3.FTColorway"
        cmd &= vbCrLf & "  AND X2.FTSizeBreakDown=X3.FTSizeBreakDown"
        cmd &= vbCrLf & " Where X3.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        cmd &= vbCrLf & " AND X3.FTTeam ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"
        cmd &= vbCrLf & ") AS B "
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq,X32.FTMatSizeCode AS FTSizeBreakDown From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "

        cmd &= vbCrLf & "   GROUP BY  B.FTSizeBreakDown"
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,''),A.FNSeq "
        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        RepFTSizeBreakDown.DataSource = _dtprod.Copy

        _dtprod.Dispose()



        cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTTeam='" & HI.UL.ULF.rpQuoted(Key) & "'"

        FTStateFinish.Checked = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")



        LoadDataInfo(Key)


    End Sub

    Private Sub RepFNQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNQuantity.EditValueChanging
        Try
            With Me.ogvoperation
                .SetFocusedRowCellValue("FNPass", 0)
                .SetFocusedRowCellValue("FNNotPass", 0)
                .SetFocusedRowCellValue("FNTotalQC", 0)
            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNPass_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNPass.EditValueChanging
        Try

            If Val(e.NewValue) >= 0 Then
                Dim totalQty As Integer = 0
                Dim totalnQc As Integer = 0
                Dim FNSeq As Integer = 0
                Dim PlanQty As Integer = 0
                Dim bfqcQty As Integer = 0
                Dim SizeBreakDown As String = ""
                Dim colorway As String = ""

                With Me.ogvoperation
                    totalQty = Val(.GetFocusedRowCellValue("FNQuantity").ToString)
                    totalnQc = Val(.GetFocusedRowCellValue("FNNotPass").ToString)
                    FNSeq = Val(.GetFocusedRowCellValue("FNSeq").ToString)
                    SizeBreakDown = .GetFocusedRowCellValue("FTSizeBreakDown").ToString
                    colorway = .GetFocusedRowCellValue("FTColorway").ToString

                    Dim dt As DataTable
                    With CType(Me.ogdBreakdown.DataSource, DataTable)
                        .AcceptChanges()
                        dt = .Copy
                    End With

                    For Each R As DataRow In dt.Select("FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")
                        PlanQty = Val(R!FNQuantity)
                    Next

                    For i As Integer = 0 To .RowCount - 1
                        If Val(.GetRowCellValue(i, "FNSeq").ToString) <> FNSeq Then
                            If .GetRowCellValue(i, "FTSizeBreakDown").ToString = SizeBreakDown And .GetRowCellValue(i, "FTColorway").ToString = colorway Then
                                bfqcQty = bfqcQty + Val(.GetRowCellValue(i, "Pass").ToString)
                            End If
                        End If

                    Next

                    If totalnQc + Val(e.NewValue) > totalQty Then

                        e.Cancel = True

                    Else

                        If Val(e.NewValue) >= 0 Then

                            If Val(e.NewValue) + bfqcQty > PlanQty Then
                                e.Cancel = True
                            Else
                                e.Cancel = False

                                .SetFocusedRowCellValue("FNTotalQC", totalnQc + Val(e.NewValue))
                            End If

                        Else
                                e.Cancel = True
                        End If

                    End If

                End With

            Else
                e.Cancel = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNNotPass_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNNotPass.EditValueChanging
        Try

            If Val(e.NewValue) >= 0 Then
                Dim totalQty As Integer = 0
                Dim totalnQc As Integer = 0

                With Me.ogvoperation
                    totalQty = Val(.GetFocusedRowCellValue("FNQuantity").ToString)
                    totalnQc = Val(.GetFocusedRowCellValue("FNPass").ToString)

                    If totalnQc + Val(e.NewValue) > totalQty Then

                        e.Cancel = True

                    Else
                        If Val(e.NewValue) >= 0 Then
                            e.Cancel = False

                            .SetFocusedRowCellValue("FNTotalQC", totalnQc + Val(e.NewValue))

                        Else
                            e.Cancel = True
                        End If


                    End If

                End With



            Else
                e.Cancel = True
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        If Not (otbjobprod.SelectedTabPage Is Nothing) And FTSMPOrderNo.Text <> "" Then


            Dim key As String = otbjobprod.SelectedTabPage.Name.ToString
            If (CheckProcess(key)) Then
                Exit Sub
            End If

            Dim PlanQty As Integer = 0
            Dim QCPass As Integer = 0
            Dim StateCheckPass As Boolean = True
            Dim dt As DataTable
            Dim dtqc As DataTable

            With CType(Me.ogdBreakdown.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()
                dtqc = .Copy
            End With

            For Each R As DataRow In dt.Rows
                PlanQty = Val(R!FNQuantity)
                QCPass = 0

                For Each Rx As DataRow In dtqc.Select("FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")
                    QCPass = QCPass + Val(Rx!FNPass)
                Next

                If PlanQty > QCPass Then
                    StateCheckPass = False
                    Exit For
                End If

            Next

            dt.Dispose()

            If StateCheckPass = False Then
                HI.MG.ShowMsg.mInfo("จำนวน QC ผ่านยังไม่ครบตามจำนวน กรุณาทำการตรวจสอบ !!!", 1809210544, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)

                Dim _Qry As String = ""


                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Qry = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                    _Qry &= vbCrLf & " SET   FTStateFinish='1' "
                    _Qry &= vbCrLf & ",FTStateFinishBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & ",FTStateFinishDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & ",FTStateFinishTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & "  where FTTeam ='" & HI.UL.ULF.rpQuoted(key) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        _Spls.Close()

                        Exit Sub

                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                    _Spls.Close()
                    FTStateFinish.Checked = True
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try


                    Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmrevokeapproval_Click(sender As Object, e As EventArgs) Handles ocmrevokeapproval.Click

        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        If Not (otbjobprod.SelectedTabPage Is Nothing) And FTSMPOrderNo.Text <> "" Then


            Dim key As String = otbjobprod.SelectedTabPage.Name.ToString
            If (CheckProcess(key, False)) = False Then
                Exit Sub
            End If


            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)

                Dim _Qry As String = ""


                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Qry = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
                    _Qry &= vbCrLf & " SET   FTStateFinish='0' "
                    _Qry &= vbCrLf & ",FTStateFinishBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & ",FTStateFinishDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & ",FTStateFinishTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & "  where FTTeam ='" & HI.UL.ULF.rpQuoted(key) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        _Spls.Close()

                        Exit Sub

                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                    _Spls.Close()
                    FTStateFinish.Checked = False
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If
    End Sub


    Private Sub ReposFNPrice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNPrice.EditValueChanging
        Try
            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                If .GetFocusedRowCellValue("FTSizeBreakDown").ToString <> "" Then
                    If Val(e.NewValue) >= 0 Then

                        Dim QtySewFinish As Integer = 0
                        Dim cmdstring As String = ""


                        Dim ColorWay As String = .GetFocusedRowCellValue("FTColorway").ToString
                        Dim Size As String = .GetFocusedRowCellValue("FTSizeBreakDown").ToString

                        cmdstring = "select Sum(FNQuantity) AS FNQuantity"
                        cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess AS X WITH(NOLOCK) "
                        cmdstring &= vbCrLf & " WHERE   (FNSampleState = 1) AND (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "') AND (FTTeam = N'" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "') "
                        cmdstring &= vbCrLf & " AND ISNULL(FTTeam,'') <> '' "
                        cmdstring &= vbCrLf & " AND ISNULL(FTSizeBreakDown,'')=  N'" & HI.UL.ULF.rpQuoted(Size) & "'"
                        cmdstring &= vbCrLf & " AND ISNULL(FTColorway,'')=  N'" & HI.UL.ULF.rpQuoted(ColorWay) & "'"

                        QtySewFinish = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

                        If QtySewFinish <= 0 Then
                            e.Cancel = True
                        Else

                            Dim SumCheck As Integer = 0
                            For I As Integer = 0 To .RowCount - 1
                                If I = .FocusedRowHandle Then
                                Else
                                    If .GetRowCellValue(I, "FTColorway").ToString = ColorWay And .GetRowCellValue(I, "FTSizeBreakDown").ToString = Size Then
                                        SumCheck = SumCheck + Val(.GetRowCellValue(I, "FNQuantity").ToString())
                                    End If
                                End If
                            Next


                            If SumCheck + Val(e.NewValue) <= QtySewFinish Then
                                e.Cancel = False
                            Else
                                e.Cancel = True
                            End If

                        End If

                            Else
                        e.Cancel = True
                    End If
                Else
                    e.Cancel = True
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTSizeBreakDown_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTSizeBreakDown.EditValueChanged

        Try

            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                Dim _Obj As System.Data.DataRowView = obj.GetSelectedDataRow()

                Dim _DataKey As String = _Obj.Item("FTKey").ToString()
                Dim _Colorway As String = _Obj.Item("FTColorway").ToString()
                Dim _SizeBreakDown As String = _Obj.Item("FTSizeBreakDown").ToString()

                .SetFocusedRowCellValue("FTColorway", _Colorway)
                .SetFocusedRowCellValue("FTSizeBreakDown", _SizeBreakDown)


                .SetFocusedRowCellValue("FNQuantity", 0)
                .SetFocusedRowCellValue("FNPass", 0)
                .SetFocusedRowCellValue("FNNotPass", 0)

            End With

            CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click

        Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)

        With CType(Me.ogcoperation.DataSource, DataTable)
            .AcceptChanges()
            Dim _RIndx As Integer = 0
            For Each R In .Rows
                _RIndx = _RIndx + 1
                R!FNSeq = _RIndx
            Next
            .AcceptChanges()
        End With

    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            InitNewRow()
        Catch ex As Exception

        End Try
    End Sub

End Class