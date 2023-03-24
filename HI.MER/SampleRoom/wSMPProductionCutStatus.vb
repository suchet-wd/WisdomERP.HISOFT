Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls

Public Class wSMPProductionCutStatus


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

        _Qry = " SELECT    ROW_NUMBER() Over (Order By SOP.FNSeq) AS FNSeq"
        _Qry &= vbCrLf & "  , Case When ISDATE(ISNULL( SOP.FTDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime, SOP.FTDate),103) ELSE '' END AS  FTDate "
        _Qry &= vbCrLf & "  , SOP.FNSampleState As FNSampleState_Hide"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Qry &= vbCrLf & "  ,L.FTNameTH As FNSampleState"
            _Qry &= vbCrLf & ",  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].[dbo].FN_GetEmpCutName(1,  SOP.FTEmp) AS FTEmpName"
        Else

            _Qry &= vbCrLf & "  , L.FTNameEN As FNSampleState"
            _Qry &= vbCrLf & ",  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].[dbo].FN_GetEmpCutName(0,  SOP.FTEmp) AS FTEmpName"

        End If

        _Qry &= vbCrLf & "  , ISNULL(SOP.FNQuantity,0) As FNQuantity,ISNULL(SOP.FTRemark,'') AS FTRemark "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'') As FTSizeBreakDown "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTColorway,'') As FTColorway "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'')  "
        _Qry &= vbCrLf & "  +'|'+  ISNULL(SOP.FTColorway,'') As FTDataKey "

        _Qry &= vbCrLf & ",SOP.FTEmp"

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As SOP With (NOLOCK) "
        _Qry &= vbCrLf & " 	                 LEFT OUTER JOIN (Select        FNListIndex, FTNameTH, FTNameEN, FTReferCode "
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNSampleCutState')) AS L ON SOP.FNSampleState =L.FNListIndex "
        _Qry &= vbCrLf & "  Where SOP.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Key) & "'"
        _Qry &= vbCrLf & "        AND SOP.FTTeam='' "
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

            If dtemp.Select("FNQuantity<=0").Length <= 0 Then

                Dim _Qry As String = ""
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Qry = "   SELECT       TOP 1  FTNameTH"
                    _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNSampleCutState') ORDER BY  FNListIndex "
                Else
                    _Qry = "   SELECT       TOP 1  FTNameEN"
                    _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNSampleCutState') ORDER BY  FNListIndex "

                End If

                With CType(Me.ogcoperation.DataSource, DataTable)
                    .Rows.Add()
                    .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
                    .Rows(.Rows.Count - 1)!FTDate = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                    .Rows(.Rows.Count - 1)!FNSampleState_Hide = 0
                    .Rows(.Rows.Count - 1)!FNSampleState = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
                    .Rows(.Rows.Count - 1)!FNQuantity = 0
                    .Rows(.Rows.Count - 1)!FTRemark = ""
                    .Rows(.Rows.Count - 1)!FTSizeBreakDown = ""
                    .Rows(.Rows.Count - 1)!FTColorway = ""
                    .Rows(.Rows.Count - 1)!FTDataKey = ""
                    .Rows(.Rows.Count - 1)!FTEmp = ""
                    .Rows(.Rows.Count - 1)!FTEmpName = ""
                    .AcceptChanges()

                End With

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

    Private Function CheckProcess(key As String) As Boolean
        Dim stateprocess As Boolean = False

        'Dim cmd As String = ""

        'cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTTeam='" & HI.UL.ULF.rpQuoted(key) & "'"

        'stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        'If stateprocess Then
        '    HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
        'End If

        Return stateprocess
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click

        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        If FTSMPOrderNo.Text <> "" Then

            Dim key As String = FTSMPOrderNo.Text.Trim
            If (CheckProcess(key)) Then
                Exit Sub
            End If


            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)

                Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess WHERE FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(key.ToString) & "' AND FTTeam='' "
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    For Each R As DataRow In .Select("FNQuantity>0", "FNSeq")

                        _FNSeq = _FNSeq + 1

                        _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTTeam, FTDate, FNSeq, FNSampleState, FTSizeBreakDown,FTColorway,FNQuantity, FTRemark,FTEmp)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                        _Qry &= vbCrLf & " ,''"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FTDate.ToString) & "'"
                        _Qry &= vbCrLf & " ," & _FNSeq & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNSampleState_Hide.ToString) & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        _Qry &= vbCrLf & " ," & Val(R!FNQuantity.ToString) & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTEmp.ToString) & "'"

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
        Call LoadOrderProdDetail(FTSMPOrderNo.Text)
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub



    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDetail(FTSMPOrderNo.Text)


        End If
    End Sub


    Private Sub LoadOrderProdDetail(Key As Object)
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogdBreakdown.DataSource = Nothing
        ogcoperation.DataSource = Nothing


        cmd = "  Select    A.FNSeq,B.FTSizeBreakDown"
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway"
        cmd &= vbCrLf & "  ,B.FTSizeBreakDown+'|'+ ISNULL(B.FTColorway,'') AS FTDataKeyRef"
        cmd &= vbCrLf & " FROM (SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X2.FNQuantity,X2.FTDeliveryDate,X2.FTRemark"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & " Where X2.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Key) & "'"
        cmd &= vbCrLf & ") AS B "
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq,X32.FTMatSizeCode AS FTSizeBreakDown From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "

        cmd &= vbCrLf & "   GROUP BY  B.FTSizeBreakDown"
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,''),A.FNSeq "
        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        RepositoryFTSizeBreakDown.DataSource = _dtprod.Copy


        cmd = "  Select   B.FTSizeBreakDown"
        cmd &= vbCrLf & "  ,ISNULL(B.FNQuantity,0) As FNQuantity "
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTDeliveryDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTDeliveryDate),103) ELSE '' END AS FTDeliveryDate"
        cmd &= vbCrLf & " ,ISNULL(B.FTRemark,'') AS FTRemark,A.FNSeq"
        cmd &= vbCrLf & "  ,B.FTSizeBreakDown+'|'+ ISNULL(B.FTColorway,'') AS FTDataKey"
        cmd &= vbCrLf & " FROM (SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X2.FNQuantity,X2.FTDeliveryDate,X2.FTRemark"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & " Where X2.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Key) & "'"
        cmd &= vbCrLf & ") AS B"
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq,X32.FTMatSizeCode AS FTSizeBreakDown From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "

        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        Me.ogdBreakdown.DataSource = _dtprod.Copy


        _dtprod.Dispose()


        LoadDataInfo(Key)


    End Sub

    Private Sub RepositoryFTSizeBreakDown_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFTSizeBreakDown.EditValueChanged
        Try

            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                Dim _Obj As System.Data.DataRowView = obj.GetSelectedDataRow()

                Dim _DataKey As String = _Obj.Item("FTDataKeyRef").ToString()
                Dim _Colorway As String = _Obj.Item("FTColorway").ToString()
                Dim _SizeBreakDown As String = _Obj.Item("FTSizeBreakDown").ToString()

                .SetFocusedRowCellValue("FTColorway", _Colorway)
                .SetFocusedRowCellValue("FTSizeBreakDown", _SizeBreakDown)
                ' .SetFocusedRowCellValue("FTDataKey", _DataKey)
                .SetFocusedRowCellValue("FNQuantity", 0)

            End With

            CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFNPrice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNPrice.EditValueChanging
        Try
            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                If .GetFocusedRowCellValue("FTSizeBreakDown").ToString <> "" Then


                    If Val(e.NewValue) >= 0 Then
                        e.Cancel = False
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


    Private Sub RepFTPositionPartName_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ReposEmp.QueryCloseUp
        Try
            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                Dim _PartName As String = ""
                Dim _PartIDKey As String = ""

                With CType(Me.ogcemp.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Select("FTSelect='1'")

                        If _PartName = "" Then
                            _PartName = R!FTEmpName.ToString
                            _PartIDKey = R!FNHSysEmpID.ToString
                        Else
                            _PartName = _PartName & "," & R!FTEmpName.ToString
                            _PartIDKey = _PartIDKey & "|" & R!FNHSysEmpID.ToString
                        End If

                    Next

                End With

                .SetFocusedRowCellValue("FTEmpName", _PartName)
                .SetFocusedRowCellValue("FTEmp", _PartIDKey)

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTPositionPartName_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ReposEmp.QueryPopUp
        Try

            Dim dt As DataTable
            Dim cmd As String = ""
            Dim _RowIndex As Integer = 0


            cmd = "  Select   '0' AS FTSelect,B.FNHSysEmpID "
            cmd &= vbCrLf & "  , B.FTEmpCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmd &= vbCrLf & "  , B.FTEmpNameTH + ' ' + B.FTEmpSurnameTH AS FTEmpName  "
            Else
                cmd &= vbCrLf & "  , B.FTEmpNameEN + ' ' +   B.FTEmpSurnameEN AS FTEmpName  "
            End If

            cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As B WITH(NOLOCK) "
            cmd &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U WITH(NOLOCK) ON B.FNHSysUnitSectId = U.FNHSysUnitSectId "
            cmd &= vbCrLf & "   Where U.FTStateSampleRoom ='1' AND U.FTStateCut ='1' "
            cmd &= vbCrLf & "   AND (ISNULL(B.FDDateEnd,'') ='' OR ISNULL(B.FDDateEnd,'') >" & HI.UL.ULDate.FormatDateDB & ") "
            cmd &= vbCrLf & " ORDER BY  B.FTEmpCode "

            dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)


            Dim _PartKey As String = ""
            Dim _PartIDKey As String = ""
            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If
                _PartIDKey = "" & .GetFocusedRowCellValue("FTEmp").ToString
            End With

            ogvemp.ClearColumnsFilter()
            ogvemp.ActiveFilter.Clear()

            If _PartIDKey <> "" Then
                With dt
                    For Each Str As String In _PartIDKey.Split("|")
                        For Each R As DataRow In .Select("FNHSysEmpID=" & Integer.Parse(Val(Str)) & "")
                            R!FTSelect = "1"
                            Exit For
                        Next
                    Next
                    .AcceptChanges()
                End With
            End If


            Me.ogcemp.DataSource = dt.Copy
            ogvemp.Columns.ColumnByFieldName("FTSelect").Width = 40
            ogvemp.Columns.ColumnByFieldName("FTEmpName").Width = 150


        Catch ex As Exception
        End Try

    End Sub

End Class