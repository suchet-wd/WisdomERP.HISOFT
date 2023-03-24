Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls

Public Class wSMPProductionCutStatusBar

    Private _GenBreakDown As wSelectBreakDown
    Private _ListSendSuplBundleInfo As New List(Of DataTable)
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _GenBreakDown = New wSelectBreakDown
        HI.TL.HandlerControl.AddHandlerObj(_GenBreakDown)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenBreakDown.Name.ToString.Trim, _GenBreakDown)
        Catch ex As Exception
        Finally
        End Try

        ' Add any initialization after the InitializeComponent() call.





        Me.StateSeason = True

        RepFTSuplName.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        RepFTOperationNameS.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        RepFTOperationNameT.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
    End Sub

    Private _StateSeason As Boolean = False
    Property StateSeason As Boolean
        Get
            Return _StateSeason
        End Get
        Set(value As Boolean)
            _StateSeason = value
        End Set
    End Property

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
        _Qry &= vbCrLf & "  , isnull(Bundle. FNQuantityBundle,0)   as FNBundleQty"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleProcess As SOP With (NOLOCK) "
        _Qry &= vbCrLf & " 	                 LEFT OUTER JOIN (Select        FNListIndex, FTNameTH, FTNameEN, FTReferCode "
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "

        _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNSampleCutState')) AS L ON SOP.FNSampleState =L.FNListIndex "
        _Qry &= vbCrLf & " OUTER APPLY (SELECT sum(B.FNQuantity) as FNQuantityBundle From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBundle_Detail AS Bundle WITH(NOLOCK) "
        _Qry &= vbCrLf & "  inner join  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBundle as B with(nolock) on Bundle.FTBarcodeBundleNo = b.FTBarcodeBundleNo  "
        _Qry &= vbCrLf & " WHERE B.FTOrderProdNo = SOP.FTSMPOrderNo And Bundle.FTSizeBreakDown = SOP.FTSizeBreakDown And Bundle.FTColorway = SOP.FTColorway  and  Bundle.FNRunLayCutSeq  = SOP.FNSeq) AS Bundle "

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
                    .Rows(.Rows.Count - 1)!FNBundleQty = 0
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
        Call LoadSendSuplInfo()
        Call LoadOrderProdDetailInfo(Me.FTSMPOrderNo.Text)
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
            Call LoadSendSuplInfo()
            Call LoadOrderProdDetailInfo(Me.FTSMPOrderNo.Text)
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
        'cmd &= vbCrLf & "  , isnull(Bundle. FNQuantityBundle,0)   as FNBundleQty"
        cmd &= vbCrLf & " FROM (SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X2.FNQuantity,X2.FTDeliveryDate,X2.FTRemark ,X2.FTSMPOrderNo"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & " Where X2.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Key) & "'"
        cmd &= vbCrLf & ") AS B"
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq,X32.FTMatSizeCode AS FTSizeBreakDown From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "
        ' cmd &= vbCrLf & "OUTER APPLY (SELECT sum(FNQuantity) as FNQuantityBundle From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBundle AS Bundle WITH(NOLOCK) WHERE Bundle.FTOrderProdNo=B.FTSMPOrderNo and  Bundle.FTSizeBreakDown=B.FTSizeBreakDown and Bundle.FTColorway=B.FTColorway ) AS Bundle "

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

    Private Sub ocmgeneratebarcodewip_Click(sender As Object, e As EventArgs) Handles ocmgeneratebarcodewip.Click
        Try
            If Me.FTSMPOrderNo.Text = "" Then
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTSMPOrderNo_lbl.Text)
                Exit Sub
            End If
            If DirectCast(Me.ogdBreakdown.DataSource, DataTable).Rows.Count <= 0 Then
                Exit Sub
            End If
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Generate Barcode ใช่หรือไม่ ?", 2206200953) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Generating....Barcode , Please Wait...")
                If Me.GenerateBarcode() Then
                    Call LoadOrderProdDetailInfo(Me.FTSMPOrderNo.Text)
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("Generate Barcode Complete..", 1405300002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Generate Barcode ได้ กรุณาทำการติดต่อ Admin...", 1405300003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Function GenerateBundle() As Boolean
        Try
            Dim _Qry As String = ""
            Dim dt As DataTable
            Dim _BarcodeHeat As String = ""
            Dim _BarcodeSupl As String = ""
            Dim dtBundel As New DataTable
            Dim _Seq As Integer = 0


            With DirectCast(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With
            If dt.Rows.Count > 0 Then
                dt.Columns.Add("FTSelect", GetType(String))
                For Each R As DataRow In dt.Rows
                    R!FTSelect = "0"
                    R!FNQuantity = R!FNQuantity - R!FNBundleQty
                Next

                With _GenBreakDown

                    .ogdBreakdown.DataSource = dt
                    .ShowDialog()

                    If (.ProcessSave) Then


                        _Qry = "   SELECT   max(FNBunbleSeq)    FNBunbleSeq"
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS A WITH(NOLOCK) "
                        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                        _Seq = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "0")



                        With DirectCast(.ogdBreakdown.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTSelect='1'")
                                _Seq += +1
                                _Qry = "exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_GEN_Bundle_Sampleroom @FTUsername='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & " ,@FNHSysCmpId= " & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                                _Qry &= vbCrLf & " ,@CmpPrefix='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpRunID) & "' "
                                _Qry &= vbCrLf & " ,@OrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                                _Qry &= vbCrLf & " ,@SizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' "
                                _Qry &= vbCrLf & " ,@ColorWay='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' "
                                _Qry &= vbCrLf & " ,@Qantity=" & Val(R!FNQuantity.ToString) & ""
                                _Qry &= vbCrLf & " ,@Seq=" & Val(_Seq) & " "
                                _Qry &= vbCrLf & ", @LaySeq=" & Val(R!FNSeq.ToString) & " "
                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)



                            Next
                        End With
                    End If
                End With

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function GenerateBarcode() As Boolean

        Dim _Qry As String = ""
        Dim dt As DataTable
        Dim _BarcodeHeat As String = ""
        Dim _BarcodeSupl As String = ""
        Dim dtBundel As New DataTable
        Dim _Seq As Integer = 0



        Try


            _Qry = "Select FTBarcodeBundleNo , FTColorway, FTSizeBreakDown  "
            _Qry &= vbCrLf & " from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle "
            _Qry &= vbCrLf & "where  FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
            _Qry &= vbCrLf & " ORder by FTBarcodeBundleNo desc  "
            dtBundel = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
            'dtBundel = _ListSendSuplBundleInfo(0).Copy
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



            For Each R As DataRow In dtBundel.Rows


                With DirectCast(Me.ogcoperation.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With
                _Seq = 0
                For Each Rx As DataRow In dt.Select("FTColorway='" & R!FTColorway.ToString & "' and FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'", "FNSampleState_Hide asc")
                    _BarcodeHeat = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SingleCon", "", False, HI.ST.SysInfo.CmpRunID & "H")
                    _Seq += +1
                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SingleCon(FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ", FTBarcodeHeatNo, FNOperationSeq, FTBarcodeBundleNo, FNHSysCmpId, FNHSysOperationId,FTOrderProdNo)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarcodeHeat) & "'"
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(_Seq)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNSampleState_Hide.ToString)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next

                _Qry = "   SELECT        FTOrderProdNo, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo,FTSendSuplRef  "
                _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail AS A  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "  WHERE  (FTOrderProdNo =  N'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "') "
                _Qry &= vbCrLf & "  AND   (FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "') "
                _Qry &= vbCrLf & "  ORDER BY FNSendSuplType "

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                For Each Rx As DataRow In dt.Rows

                    Select Case Integer.Parse(Val(Rx!FNSendSuplType.ToString))
                        Case 0
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "E")
                        Case 1
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "P")
                        Case 2
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "H")
                        Case 3
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "L")
                        Case 4
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "D")
                        Case 5
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "W")
                        Case 6
                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "N")
                        Case Else

                            _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "O")
                    End Select

                    ' _BarcodeSupl = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE), "TSMPTBarcode_SendSupl", "", False, HI.ST.SysInfo.CmpRunID & "O")

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl(FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ",FTBarcodeSendSuplNo, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo, FNHSysCmpId,FTOrderProdNo,FTSendSuplRef)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarcodeSupl) & "'"
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNHSysPartId.ToString)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNSendSuplType.ToString)) & ""
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Rx!FNHSysSuplId.ToString)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx!FTSendSuplRef.ToString) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle "
                _Qry &= vbCrLf & " SET  FTStateGenBarcode='1'"
                _Qry &= vbCrLf & ",FTGenBarcodeBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & ",FDGenBarcodeDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTGenBarcodeTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'  "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
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


    Private Sub LoadOrderProdDetailInfo(ByVal Key As Object)
        Dim _Qry As String
        Dim dt As DataTable

        '_Qry = "SELECT       '1' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq, A.FTColorway, A.FTSizeBreakDown, A.FNQuantity"
        '_Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A  WITH(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' AND ISNULL(A.FTStateGenBarcode,'') <>'1'  "
        '_Qry &= vbCrLf & " Order By  A.FNBunbleSeq "

        'dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        'Me.ogclaycut.DataSource = dt.Copy

        _Qry = "SELECT  '0' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq,  CASE WHEN ISNULL(A.FTColorwayNew,'') ='' THEN A.FTColorway ELSE ISNULL(A.FTColorwayNew,'') END AS FTColorway, A.FTSizeBreakDown, A.FNQuantity, CASE WHEN ISNULL(A.FTColorwayNew,'') <> '' OR ISNULL(A.FTChangeToLineItemNo,'') <>'' THEN '1' ELSE '0' END AS FTStateChange"
        _Qry &= vbCrLf & ", '' as FTMarkName"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & " , CASE WHEN ISNULL(A.FTChangeToLineItemNo,'') ='' THEN A.FTPOLineItemNo ELSE ISNULL(A.FTChangeToLineItemNo,'') END AS  FTPOLineItemNo"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS A  WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' AND ISNULL(A.FTStateGenBarcode,'') ='1'  "
        _Qry &= vbCrLf & " Order By  A.FNBunbleSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Me.ogcbarcode.DataSource = dt.Copy

        _Qry = "SELECT  '0' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq,  CASE WHEN ISNULL(A.FTColorwayNew,'') ='' THEN A.FTColorway ELSE ISNULL(A.FTColorwayNew,'') END AS FTColorway, A.FTSizeBreakDown, A.FNQuantity, CASE WHEN ISNULL(A.FTColorwayNew,'') <> '' OR ISNULL(A.FTChangeToLineItemNo,'') <>'' THEN '1' ELSE '0' END AS FTStateChange"
        _Qry &= vbCrLf & ", '' as FTMarkName"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & " , CASE WHEN ISNULL(A.FTChangeToLineItemNo,'') ='' THEN A.FTPOLineItemNo ELSE ISNULL(A.FTChangeToLineItemNo,'') END AS  FTPOLineItemNo"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS A  WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'  "
        _Qry &= vbCrLf & " Order By  A.FNBunbleSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Me.ogcbundle.DataSource = dt.Copy

        _Qry = "  SELECT  '0' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq"
        _Qry &= vbCrLf & " , A.FTColorway, A.FTSizeBreakDown, A.FNQuantity"
        _Qry &= vbCrLf & " 	, B.FTBarcodeSendSuplNo, P.FTPartCode "
        _Qry &= vbCrLf & "   , S.FTSuplCode"
        _Qry &= vbCrLf & " ,B.FNSendSuplType  "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , P.FTPartNameTH AS FTPartName "
            _Qry &= vbCrLf & " , S.FTSuplNameTH AS FTSuplName "
            _Qry &= vbCrLf & " , LN.FTNameTH AS FTSendSuplName "
        Else
            _Qry &= vbCrLf & " , P.FTPartNameEN AS FTPartName "
            _Qry &= vbCrLf & " , S.FTSuplNameEN AS FTSuplName "
            _Qry &= vbCrLf & " , LN.FTNameEN AS FTSendSuplName "
        End If
        _Qry &= vbCrLf & " ,A.FTPOLineItemNo"

        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS B WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK) ON B.FNHSysPartId = P.FNHSysPartId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & " 	ON  B.FNSendSuplType = LN.FNListIndex "


        _Qry &= vbCrLf & "  outer apply(select  AX.FTMainBarcodeBundleNo AS FTBarcodeBundleNo ,AX.FTPOLineItemNo,MIN(AX.FNBunbleSeq) As FNBunbleSeq, AX.FTColorway, AX.FTSizeBreakDown, SUM(AX.FNQuantity) AS FNQuantity "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS AX WITH(NOLOCK) WHERE AX.FTMainBarcodeBundleNo = B.FTBarcodeBundleNo "
        _Qry &= vbCrLf & "  Group BY AX.FTMainBarcodeBundleNo,AX.FTPOLineItemNo,AX.FTColorway, AX.FTSizeBreakDown  "
        _Qry &= vbCrLf & "  ) AS A"


        _Qry &= vbCrLf & " WHERE B.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        _Qry &= vbCrLf & "  ORDER BY  B.FTBarcodeSendSuplNo"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Me.ogcbrcodesupl.DataSource = dt.Copy

        _Qry = "     SELECT   '0' AS FTSelect,  A.FTBarcodeBundleNo, A.FNBunbleSeq, A.FTColorway"
        _Qry &= vbCrLf & "  , A.FTSizeBreakDown, A.FNQuantity, B.FTBarcodeHeatNo"
        _Qry &= vbCrLf & "  , B.FNOperationSeq, C.FTOperationCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , C.FTOperationNameTH AS FTOperationName "
        Else
            _Qry &= vbCrLf & " , C.FTOperationNameEN AS FTOperationName "
        End If
        _Qry &= vbCrLf & " ,A.FTPOLineItemNo"

        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SingleCon AS B WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS C ON B.FNHSysOperationId = C.FNHSysOperationId"

        _Qry &= vbCrLf & "  outer apply(select   AX.FTMainBarcodeBundleNo AS FTBarcodeBundleNo ,AX.FTPOLineItemNo,MIN(AX.FNBunbleSeq) As FNBunbleSeq, AX.FTColorway, AX.FTSizeBreakDown, SUM(AX.FNQuantity) AS FNQuantity "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS AX WITH(NOLOCK) WHERE AX.FTMainBarcodeBundleNo = B.FTBarcodeBundleNo "
        _Qry &= vbCrLf & "  Group BY AX.FTMainBarcodeBundleNo,AX.FTPOLineItemNo,AX.FTColorway, AX.FTSizeBreakDown  "
        _Qry &= vbCrLf & "  ) AS A"

        _Qry &= vbCrLf & " WHERE B.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        _Qry &= vbCrLf & "  ORDER BY  B.FTBarcodeHeatNo"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
        Me.ogcbrcodesingle.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadSendSuplInfo()

        Dim dt As DataTable
        Dim dtbundle As DataTable
        Dim dtbundleOrg As DataTable
        Dim _SeaSonId As Integer = 0

        Dim _Qry As String
        _Qry = "Select   Top  1  FNHSysSeasonId   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder WITH(NOLOCK)  WHERE  FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        _SeaSonId = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")



        ogcsendsupl.DataSource = Nothing
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
        Dim _FTOrderProdNo As String = Me.FTSMPOrderNo.Text



        Call LoadSupplier()
        Call LoadOperation(Me.FTSMPOrderNo.Text)
        Call LoadPart()

        _Qry = " SELECT A.FNHSysPartId"
        _Qry &= vbCrLf & " 	,P.FTPartCode "
        _Qry &= vbCrLf & " , Convert(varchar(30),A.FNHSysPartId) + '|' +P.FTPartCode + '|' + Convert(varchar(30),A.FNSendSuplType) + '|' + ISNULL(A.FTNote,'')  AS FTPartName "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            ' _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameTH,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName"
            _Qry &= vbCrLf & " , ISNULL(S.FTSuplCode,'') +' :: ' + ISNULL(S.FTSuplNameTH,'') As FTSuplName"
            _Qry &= vbCrLf & " ,ISNULL(OS.FTOperationCode,'') +' :: ' + ISNULL(OS.FTOperationNameTH,'')  AS FTOperationNameS"
            _Qry &= vbCrLf & " ,ISNULL(OT.FTOperationCode,'') +' :: ' +ISNULL(OT.FTOperationNameTH,'')  AS FTOperationNameT"

        Else

            ' _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameEN,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameEN,'') AS FNSendSuplTypeName "
            _Qry &= vbCrLf & " , ISNULL(S.FTSuplCode,'') +' :: ' +ISNULL(S.FTSuplNameEN,'') As FTSuplName"
            _Qry &= vbCrLf & " ,ISNULL(OS.FTOperationCode,'') +' :: ' +ISNULL(OS.FTOperationNameEN,'')  AS FTOperationNameS"
            _Qry &= vbCrLf & " ,ISNULL(OT.FTOperationCode,'') +' :: ' +ISNULL(OT.FTOperationNameEN,'')  AS FTOperationNameT"

        End If

        _Qry &= vbCrLf & " , A.FNSendSuplType"
        _Qry &= vbCrLf & " ,A.FNHSysSuplId"

        _Qry &= vbCrLf & " ,S.FTSuplCode "
        _Qry &= vbCrLf & " , A.FNHSysOperationId"
        _Qry &= vbCrLf & " ,OS.FTOperationCode AS FTOperationCodeS"
        _Qry &= vbCrLf & " ,A.FNHSysOperationIdTo"
        _Qry &= vbCrLf & " ,OT.FTOperationCode AS FTOperationCodeT"
        _Qry &= vbCrLf & " , A.FNQuantity,A.FTSendSuplRef"
        _Qry &= vbCrLf & " ,A.FTNote"
        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT  A.FNHSysPartId"
        _Qry &= vbCrLf & " , A.FNSendSuplType,A.FTNote"
        _Qry &= vbCrLf & " , B.FNHSysSuplId"
        _Qry &= vbCrLf & " , B.FNHSysOperationId"
        _Qry &= vbCrLf & " , B.FNHSysOperationIdTo"
        _Qry &= vbCrLf & " , ISNULL(B.FNQuantity,0) AS FNQuantity"
        _Qry &= vbCrLf & " , ISNULL(B.FTSendSuplRef,'') AS FTSendSuplRef"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (SELECT X.FNHSysPartId,X.FNSendSuplType,ISNULL(SPLN.FTNote,'') AS FTNote"
        _Qry &= vbCrLf & "   FROM ( SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' And FTStateEmb = 1 "

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' And FTStatePrint = 1 "

        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' And FTStateHeat = 1 "

        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' And FTStateLaser = 1 "

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' And FTStateWindows = 1 "





        _Qry &= vbCrLf & " ) AS X"

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.FNT_GetPartSendSuplDesc_SampleRoom( '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "')  "
        _Qry &= vbCrLf & "  ) AS SPLN "
        _Qry &= vbCrLf & "  ON X.FNHSysPartId = SPLN.FNHSysPartId AND X.FNSendSuplType = SPLN.FNSendSuplType "

        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  ("
        _Qry &= vbCrLf & "  SELECT * "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl  WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
        _Qry &= vbCrLf & "   )"
        _Qry &= vbCrLf & "  AS B"
        _Qry &= vbCrLf & "  ON A.FNHSysPartId =B.FNHSysPartId"
        _Qry &= vbCrLf & "   AND A.FNSendSuplType = B.FNSendSuplType "
        _Qry &= vbCrLf & "   AND A.FNSendSuplType = B.FNSendSuplType AND A.FTNote=B.FTNote "
        _Qry &= vbCrLf & "   ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & "  ON A.FNSendSuplType = LN.FNListIndex "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON A.FNHSysSuplId = S.FNHSysSuplId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation  AS OS WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON A.FNHSysOperationId = OS.FNHSysOperationId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation  AS OT WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON A.FNHSysOperationIdTo = OT.FNHSysOperationId "
        _Qry &= vbCrLf & " WHERE A.FNHSysPartId > 0 AND ISNULL(P.FTPartCode,'') <>'' "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcsendsupl.DataSource = dt.Copy

        _Qry = "   SELECT     '0' AS FTSelect,   FTBarcodeBundleNo, FNBunbleSeq"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
        _Qry &= vbCrLf & "  ORDER BY  FNBunbleSeq "
        dtbundleOrg = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        ogcselectbundle.DataSource = dtbundleOrg.Copy

        'dtbundle.Dispose()
        dtbundleOrg.Dispose()
        dt.Dispose()

    End Sub


    Private Sub LoadSupplier()
        Dim _Qry As String
        _Qry = "SELECT        FNHSysSuplId"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , FTSuplCode +' :: ' + FTSuplNameTH  AS FTSuplName "
        Else
            _Qry &= vbCrLf & " , FTSuplCode +' :: ' + FTSuplNameEN  AS FTSuplName "
        End If

        _Qry &= vbCrLf & " , FTSuplCode"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE        (FTStateActive = '1')"
        _Qry &= vbCrLf & "  AND (FTStateSubContact = '1')"
        _Qry &= vbCrLf & "  Order BY FTSuplName "

        Me.RepFTSuplName.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

    End Sub
    Private Sub LoadPart()

        Dim dt As DataTable
        Dim _Qry As String
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
        Dim _FTOrderProdNo As String = Me.FTSMPOrderNo.Text
        Dim _FNHSysSeasonId As Integer


        _Qry = "SELECT TOP 1 FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS X WITH(NOLOCK) WHERE  FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        _FNHSysSeasonId = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, ""))

        _Qry = " SELECT A.FNHSysPartId"
        _Qry &= vbCrLf & " 	,P.FTPartCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameTH,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName"
        Else
            _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameEN,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameEN,'') AS FNSendSuplTypeName "
        End If

        _Qry &= vbCrLf & " , A.FNSendSuplType"
        _Qry &= vbCrLf & " ,ISNULL(SPLN.FTNote,'') AS FTNote"
        _Qry &= vbCrLf & " , Convert(varchar(30),A.FNHSysPartId) + '|' +P.FTPartCode + '|' + Convert(varchar(30),A.FNSendSuplType) +'|' + ISNULL(SPLN.FTNote,'')  AS FTSenSuplDataRef "
        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"



        _Qry &= vbCrLf & "   And FTStateEmb = 1"

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"



        _Qry &= vbCrLf & "   And FTStatePrint = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"



        _Qry &= vbCrLf & "   And FTStateHeat = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"



        _Qry &= vbCrLf & "   And FTStateLaser = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTSMPOrderNo =   '" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"



        _Qry &= vbCrLf & "   And FTStateWindows = 1"

        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & "  ON A.FNSendSuplType = LN.FNListIndex "

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * "


        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.FNT_GetPartSendSuplDescBySeason_SampleROOM(" & _FNHSysStyleId & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'," & _FNHSysSeasonId & ")  "


        _Qry &= vbCrLf & "  ) AS SPLN "
        _Qry &= vbCrLf & "  ON A.FNHSysPartId = SPLN.FNHSysPartId AND A.FNSendSuplType = SPLN.FNSendSuplType "

        _Qry &= vbCrLf & " WHERE A.FNHSysPartId > 0 AND ISNULL(P.FTPartCode,'') <>'' "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.RepFTPartName.DataSource = dt.Copy
        dt.Dispose()

    End Sub


    Private Sub LoadOperation(_FTOrderProdNo As String)
        Dim _Qry As String = ""
        Dim _FNHSysStyleId As Integer


        '_Qry = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS X WITH(NOLOCK) WHERE  FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
        '_FNHSysStyleId = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, ""))

        _Qry = "   SELECT     B.FNHSysOperationId"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameTH  AS FTOperationName "
        Else
            _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameEN  AS FTOperationName "
        End If

        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) "  'ON A.FNHSysOperationId = B.FNHSysOperationId
        '  _Qry &= vbCrLf & "  WHERE FNOperationState =2 "  '  A.FNHSysStyleId=" & Integer.Parse(Val(_FNHSysStyleId)) & " AND
        '_Qry &= vbCrLf & "  ORDER BY A.FNSeq "



        Dim dt As DataTable
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.RepFTOperationNameS.DataSource = dt.Copy
        Me.RepFTOperationNameT.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Function SaveDataSendSupl() As Boolean

        Dim _Qry As String = ""





        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            Dim FTSendSuplRef As String = ""
            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()
            Dim dt As DataTable = CType(Me.ogcsendsupl.DataSource, DataTable)
            Dim dtsub As DataTable = CType(Me.ogcsendsupl.DataSource, DataTable)
            Dim dtbundle As DataTable
            Dim _FNQuantity As Integer = 0


            Try
                dtbundle = _ListSendSuplBundleInfo(0).Copy
            Catch ex As Exception
                dtbundle = New DataTable
            End Try



            _Qry = "  DELETE FROM A "
            _Qry &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl AS A  "
            _Qry &= vbCrLf & "  WHERE A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

            _Qry = "  DELETE FROM A "
            _Qry &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail AS A  "
            _Qry &= vbCrLf & "  WHERE A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)




            '_Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail "
            '_Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
            '_Qry &= vbCrLf & " AND FTSendSuplRef NOT IN ("
            '_Qry &= vbCrLf & "  SELECT DISTINCT FTSendSuplRef "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl AS T WITH(NOLOCK)"
            '_Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
            '_Qry &= vbCrLf & "  )"

            'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            'End If

            Dim RowDataInd As Integer = 0
            For Each R As DataRow In dt.Select("FNHSysPartId>0  AND FNHSysSuplId>0 AND FNHSysOperationId>0 AND FNHSysOperationIdTo>0 ")
                RowDataInd = RowDataInd + 1

                If R!FTSendSuplRef.ToString = "" Then

                    _Qry = "SELECT Replace( Convert(varchar(10),GetDate(),111),'/','') + Replace(Convert(varchar(30),GetDate(),114),':','')"
                    FTSendSuplRef = HI.Conn.SQLConn.GetFieldByNameOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                    If FTSendSuplRef <> "" Then
                        FTSendSuplRef = HI.ST.SysInfo.CmpRunID & "-" & RowDataInd.ToString & "-" & FTSendSuplRef

                        _FNQuantity = 0
                        For Each Rx As DataRow In dtbundle.Select("FNHSysPartId=" & Val(R!FNHSysPartId) & " AND FNHSysSuplId=" & Val(R!FNHSysSuplId) & " AND FNSendSuplType=" & Val(R!FNSendSuplType) & " AND FTSendSuplRef='' ")
                            _FNQuantity = _FNQuantity + 1

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Qry &= vbCrLf & " , FTSendSuplRef, FTOrderProdNo"
                            _Qry &= vbCrLf & " , FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo,FNHSysCmpId)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & ",'" & FTSendSuplRef & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                            _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeBundleNo.ToString) & "'"
                            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                MsgBox("Ex TSMPTOrderProd_SendSupl_Detail")
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        Next

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl(FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & " , FTSendSuplRef, FTOrderProdNo"
                        _Qry &= vbCrLf & " , FNHSysPartId, FNSendSuplType, FNHSysSuplId, FNHSysOperationId, FNHSysOperationIdTo,FNQuantity,FTNote,FNHSysCmpId)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",'" & FTSendSuplRef & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysOperationId.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysOperationIdTo.ToString)) & ""
                        _Qry &= vbCrLf & "," & _FNQuantity & ""
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            MsgBox("Ex TSMPTOrderProd_SendSupl")
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Else

                    FTSendSuplRef = R!FTSendSuplRef.ToString

                    If CheckCreateBarcodeSendSupl(Me.FTSMPOrderNo.Text, FTSendSuplRef, Val(R!FNHSysPartId), Val(R!FNHSysSuplId), Val(R!FNSendSuplType)) = False Then
                        _FNQuantity = 0
                        For Each Rx As DataRow In dtbundle.Select("FNHSysPartId=" & Val(R!FNHSysPartId) & " AND FNHSysSuplId=" & Val(R!FNHSysSuplId) & " AND FNSendSuplType=" & Val(R!FNSendSuplType) & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' ")
                            _FNQuantity = _FNQuantity + 1

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Qry &= vbCrLf & " , FTSendSuplRef, FTOrderProdNo"
                            _Qry &= vbCrLf & " , FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo,FNHSysCmpId)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & ",'" & FTSendSuplRef & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                            _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeBundleNo.ToString) & "'"
                            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        Next

                        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl"
                        _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                        _Qry &= vbCrLf & ",FNSendSuplType=" & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                        _Qry &= vbCrLf & ",FNHSysSuplId=" & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                        _Qry &= vbCrLf & ",FNHSysOperationId=" & Integer.Parse(Val(R!FNHSysOperationId.ToString)) & ""
                        _Qry &= vbCrLf & ",FNHSysOperationIdTo=" & Integer.Parse(Val(R!FNHSysOperationIdTo.ToString)) & ""
                        _Qry &= vbCrLf & ",FNQuantity=" & _FNQuantity & ""
                        _Qry &= vbCrLf & " WHERE FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                End If
            Next

            For Each R As DataRow In dtsub.Select("FTSendSuplRef<>''")

                FTSendSuplRef = R!FTSendSuplRef.ToString
                If dt.Select("FTSendSuplRef='" & R!FTSendSuplRef.ToString & "'").Length <= 0 Then

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail "
                    _Qry &= vbCrLf & " WHERE FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & " AND FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef NOT IN ("
                    _Qry &= vbCrLf & "  SELECT DISTINCT FTSendSuplRef "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl AS T WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & "  )"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl "
                    _Qry &= vbCrLf & " WHERE FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & " AND FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef NOT IN ("
                    _Qry &= vbCrLf & "  SELECT DISTINCT FTSendSuplRef "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl AS T WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & "  )"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If

                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            MsgBox("Ex Catch" & ex.Message.ToString)
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub ocmsavesendsupl_Click(sender As Object, e As EventArgs) Handles ocmsavesendsupl.Click
        If Me.FTSMPOrderNo.Text <> "" Then
            If SaveDataSendSupl() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Call LoadSendSuplInfo()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub


    Private Function CheckCreateBarcodeSendSupl(OrderProdKey As String, SensuplKey As String, FNHSysPartId As Integer, FNHSysSuplId As Integer, FNSendSuplType As Integer) As Boolean
        Dim _Qry As String = ""
        _Qry = "  SELECT TOP 1 FTOrderProdNo"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey) & "') "
        _Qry &= vbCrLf & " AND (FTSendSuplRef = N'" & HI.UL.ULF.rpQuoted(SensuplKey) & "')"
        _Qry &= vbCrLf & " and FNHSysPartId =  " & HI.UL.ULF.rpQuoted(FNHSysPartId) & "  "
        _Qry &= vbCrLf & " and FNHSysSuplId =  " & HI.UL.ULF.rpQuoted(FNHSysSuplId) & "  "
        _Qry &= vbCrLf & " and FNSendSuplType =  " & HI.UL.ULF.rpQuoted(FNSendSuplType) & "  "

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
    End Function


    Private Sub RepFTPositionPartNames_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepGSFNQuantity.QueryCloseUp
        Try
            Dim _FNHSysPartId As Integer
            Dim _FNSendSuplType As Integer
            Dim _FNHSysSuplId As Integer
            Dim _FTSendSuplRef As String

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub
                _FNHSysPartId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString))
                _FNSendSuplType = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSendSuplType").ToString))
                _FNHSysSuplId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                _FTSendSuplRef = "" & .GetRowCellValue(.FocusedRowHandle, "FTSendSuplRef").ToString

            End With


            Try
                ogvselectbundle.ClearColumnsFilter()
                ogvselectbundle.ActiveFilter.Clear()
            Catch ex As Exception
            End Try

            ' FTSendSuplRef, FTOrderProdNo, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo 
            Dim _Count As Integer = 0
            Dim _StrFilter As String = ""
            Dim dt As DataTable
            Dim _Qry As String = ""

            _Qry = " SELECT  FTSendSuplRef, FTOrderProdNo, FNHSysPartId"
            _Qry &= vbCrLf & " , FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail  WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Try
                If _ListSendSuplBundleInfo(0).Rows.Count > 0 Then
                    dt = _ListSendSuplBundleInfo(0).Copy
                End If
            Catch ex As Exception

            End Try

            dt.BeginInit()


            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For Each R As DataRow In .Rows
                    _StrFilter = "FNHSysPartId=" & _FNHSysPartId & " AND FNSendSuplType=" & _FNSendSuplType & " AND FNHSysSuplId=" & _FNHSysSuplId & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(_FTSendSuplRef) & "' AND FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    If R!FTSelect.ToString = "1" Then
                        _Count = _Count + 1
                        If dt.Select(_StrFilter).Length <= 0 Then
                            dt.Rows.Add(_FTSendSuplRef, Me.FTSMPOrderNo.Text, _FNHSysPartId, _FNSendSuplType, _FNHSysSuplId, R!FTBarcodeBundleNo.ToString)
                        End If
                    Else
                        For Each Rx As DataRow In dt.Select(_StrFilter)
                            Rx.Delete()
                        Next
                    End If

                Next

                .AcceptChanges()
            End With






            _ListSendSuplBundleInfo.Clear()
            _ListSendSuplBundleInfo.Add(dt.Copy)

            dt.EndInit()
            dt.Dispose()
            Me.ogvsendsupl.SetFocusedRowCellValue("FNQuantity", _Count)
            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()







        Catch ex As Exception
        End Try
    End Sub





    Private Sub RepFTPositionPartNames_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepGSFNQuantity.QueryPopUp
        Try
            Dim _FNHSysPartId As Integer
            Dim _FNSendSuplType As Integer
            Dim _FNHSysSuplId As Integer
            Dim _FTSendSuplRef As String

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub
                _FNHSysPartId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString))
                _FNSendSuplType = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSendSuplType").ToString))
                _FNHSysSuplId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                _FTSendSuplRef = "" & .GetRowCellValue(.FocusedRowHandle, "FTSendSuplRef").ToString

            End With
            ockselectsendall.Checked = False
            Try
                ogvselectbundle.ClearColumnsFilter()
                ogvselectbundle.ActiveFilter.Clear()
            Catch ex As Exception
            End Try


            FNStartBundle.Value = 0
            FNEndBundle.Value = 0
            Dim dt As DataTable
            Dim _Qry As String = ""

            _Qry = " SELECT  FTSendSuplRef, FTOrderProdNo, FNHSysPartId"
            _Qry &= vbCrLf & " , FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail  WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            Dim _StrFilter As String = ""
            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For Each R As DataRow In .Rows

                    _StrFilter = "FNHSysPartId=" & _FNHSysPartId & " AND FNSendSuplType=" & _FNSendSuplType & " AND FNHSysSuplId=" & _FNHSysSuplId & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(_FTSendSuplRef) & "' AND FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    If dt.Select(_StrFilter).Length > 0 Then
                        R!FTSelect = "1"
                    Else
                        R!FTSelect = "0"
                    End If
                Next

                .AcceptChanges()
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepFTOperationNameS_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTOperationNameS.EditValueChanged
        Try

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNHSysOperationId", obj.GetColumnValue("FNHSysOperationId").ToString)

            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTOperationNameT_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTOperationNameT.EditValueChanged
        Try

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNHSysOperationIdTo", obj.GetColumnValue("FNHSysOperationId").ToString)
            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTSuplName_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTSuplName.EditValueChanged
        Try

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNHSysSuplId", obj.GetColumnValue("FNHSysSuplId").ToString)

            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmselect_Click(sender As Object, e As EventArgs) Handles ocmselect.Click
        If IsNumeric(Me.FNStartBundle.Value) And IsNumeric(Me.FNEndBundle.Value) Then
            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For I As Integer = Me.FNStartBundle.Value To Me.FNEndBundle.Value
                    For Each R As DataRow In .Select("FNBunbleSeq=" & I & "")
                        R!FTSelect = "1"

                        Exit For
                    Next
                Next
                .AcceptChanges()
            End With
        End If
    End Sub
    Private Sub ockselectsndall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectsendall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectsendall.Checked Then
                _State = "1"
            End If

            With ogcselectbundle
                If Not (.DataSource Is Nothing) And ogvselectbundle.RowCount > 0 Then

                    With ogvselectbundle
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmgenbarcode_Click(sender As Object, e As EventArgs) Handles ocmcreatebound.Click
        Try
            If Me.FTSMPOrderNo.Text = "" Then
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTSMPOrderNo_lbl.Text)
                Exit Sub
            End If
            If DirectCast(Me.ogdBreakdown.DataSource, DataTable).Rows.Count <= 0 Then
                Exit Sub
            End If
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Generate Barcode ใช่หรือไม่ ?", 2206200953) = True Then

                Dim _Spls As New HI.TL.SplashScreen("Generating....Barcode , Please Wait...")
                If Me.GenerateBundle() Then
                    Call LoadSendSuplInfo()
                    Call LoadDataInfo(Me.FTSMPOrderNo.Text)
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("Generate Barcode Complete..", 1405300002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Generate Barcode ได้ กรุณาทำการติดต่อ Admin...", 1405300003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreviewbarcodewipall_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipall.Click

        Try
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _Qry As String = ""

            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name

                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                Case otpbarcodesendsupl.Name

                    Try

                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    Catch ex As Exception
                    End Try

                    _ReportName = "BarcodeSendSupp.rpt"
                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                Case otpbarcodeheat.Name

                    Try

                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE_SAMPLEROOM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                    Catch ex As Exception
                    End Try

                    _ReportName = "BarCodeSingleCon.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            End Select

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "SampleRoom\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()
            End With
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ocmpreviewbarcodewipselect_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipselect.Click
        If Me.FTSMPOrderNo.Text <> "" Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _RowIndex As Integer = 0
            Dim _Qry As String = ""

            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name

                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"


                    _RowIndex = 0
                    With CType(Me.ogcbarcode.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarCodeBundle.FTBarcodeBundleNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodesendsupl.Name
                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarcodeSendSupp.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                    _RowIndex = 0

                    With CType(Me.ogcbrcodesupl.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarSendSup.FTBarcodeSendSuplNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodeheat.Name




                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE_SAMPLEROOM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try



                    _ReportName = "BarCodeSingleCon.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                    With CType(Me.ogcbrcodesingle.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_Barcode_SingleCon.FTBarcodeHeatNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

            End Select

            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "SampleRoom\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()

            End With


        End If
    End Sub

    Private Sub ocmpreviewbarcodewipalla4_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipalla4.Click
        If Me.FTSMPOrderNo.Text <> "" Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _Qry As String = ""
            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name


                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode_Print.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"


                Case otpbarcodesendsupl.Name


                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarcodeSendSupp_Print.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"


                Case otpbarcodeheat.Name





                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE_SAMPLEROOM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarCodeSingleCon_Print.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            End Select

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "SampleRoom\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()
            End With


        End If
    End Sub

    Private Sub ocmpreviewbarcodewipselecta4_Click(sender As Object, e As EventArgs) Handles ocmpreviewbarcodewipselecta4.Click
        If FTSMPOrderNo.Text <> "" Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _RowIndex As Integer = 0
            Dim _Qry As String = ""

            Select Case otbdetail.SelectedTabPage.Name
                Case otpbarcodebundle.Name
                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_BUNDLE_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BundleBarCode_Print.rpt"
                    _FM = "{V_BarCodeBundle.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= " AND {V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                    _RowIndex = 0
                    With CType(Me.ogcbarcode.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarCodeBundle.FTBarcodeBundleNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodesendsupl.Name


                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.[SP_CREATE_TEMPBARCODE_SENDSUPL_SAMPLEROOM] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarcodeSendSupp_Print.rpt"

                    _FM = "{V_BarSendSup.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _FM &= "  AND {V_BarSendSup.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                    _RowIndex = 0

                    With CType(Me.ogcbrcodesupl.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_BarSendSup.FTBarcodeSendSuplNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

                Case otpbarcodeheat.Name





                    Try
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_CREATE_TEMPBARCODE_SINGLE_SAMPLEROOM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                    Catch ex As Exception

                    End Try

                    _ReportName = "BarCodeSingleCon_Print.rpt"
                    _FM = "{V_Barcode_SingleCon.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                    With CType(Me.ogcbrcodesingle.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length > 0 Then
                            _FM &= " AND {V_Barcode_SingleCon.FTBarcodeHeatNo} IN ["
                            _RowIndex = 0
                            For Each R As DataRow In .Select("FTSelect='1'")

                                If _RowIndex = 0 Then
                                    _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                Else
                                    _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeHeatNo.ToString) & "'"
                                End If

                                _RowIndex = _RowIndex + 1

                            Next

                            _FM &= "]"
                        End If

                    End With

            End Select

            With New HI.RP.Report

                .FormTitle = Me.Text
                .ReportFolderName = "SampleRoom\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()

            End With



        End If
    End Sub

    Private Sub ocmpreviewpacklist_Click(sender As Object, e As EventArgs) Handles ocmpreviewpacklist.Click
        If Me.FTSMPOrderNo.Text <> "" Then
            Dim _FM As String = ""
            Dim _ReportName As String = ""
            Dim _RowIndex As Integer = 0

            _ReportName = "Packinglist.rpt"
            _FM = "{TPRODTOrderProd.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            _RowIndex = 0
            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    _FM &= " AND {V_packinglistProduct.FTBarcodeBundleNo} IN ["
                    _RowIndex = 0
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If _RowIndex = 0 Then
                            _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                        Else
                            _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                        End If

                        _RowIndex = _RowIndex + 1

                    Next

                    _FM &= "]"
                End If

            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "SampleRoom\"
                .ReportName = _ReportName
                .Formular = _FM
                .Preview()
            End With



        End If
    End Sub

    Private Sub ocmbundlelaypreview_Click(sender As Object, e As EventArgs) Handles ocmbundlelaypreview.Click
        Try
            If FTSMPOrderNo.Text <> "" Then
                Dim _FM As String = ""
                Dim _ReportName As String = ""
                Dim _RowIndex As Integer = 0

                _ReportName = "ReportProdBundlelay.rpt"
                _FM = "{V_BarCodeBundle.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
                _RowIndex = 0
                With CType(Me.ogcbarcode.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTSelect='1'").Length > 0 Then
                        _FM &= " AND {V_BarCodeBundle.FTBarcodeBundleNo} IN ["
                        _RowIndex = 0
                        For Each R As DataRow In .Select("FTSelect='1'")

                            If _RowIndex = 0 Then
                                _FM &= "'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            Else
                                _FM &= ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            End If

                            _RowIndex = _RowIndex + 1

                        Next

                        _FM &= "]"
                    End If

                End With



                With New HI.RP.Report

                    .FormTitle = Me.Text
                    .ReportFolderName = "SampleRoom\"
                    .ReportName = "ReportProdBundlelay.rpt"
                    .Formular = _FM
                    .Preview()

                End With


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function checkbarcodesendsupl(barcodebundle) As Boolean
        Try
            Dim _Qry As String = ""
            _Qry = "  SELECT TOP 1 FTOrderProdNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(barcodebundle) & "') "

            Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
        Catch ex As Exception

        End Try
    End Function


    Private Function checkbarcodebundle(barcodebundle) As Boolean
        Try
            Dim _Qry As String = ""
            _Qry = "  SELECT TOP 1 FTOrderProdNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(barcodebundle) & "') "

            Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
        Catch ex As Exception

        End Try
    End Function


    Private Sub ocmdeletebound_Click(sender As Object, e As EventArgs) Handles ocmdeletebound.Click
        Dim _Spls As New HI.TL.SplashScreen("Checking And Deleting Data.... Please wait. ")
        Try

            Dim _Str As String = ""

            Select Case otxtabctrl.SelectedTabPage.Name
                Case otpcut.Name

                    With ogcbarcode
                        If Not (.DataSource Is Nothing) Then

                            With CType(.DataSource, DataTable)
                                .AcceptChanges()

                                For Each R As DataRow In .Select("FTSelect='1'")

                                    If checkbarcodebundle(HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString)) Then
                                        HI.MG.ShowMsg.mInfo(2206291552, "กรุณาลบบาร์โค๊ด ก่อน", Me.Text)
                                        Exit Sub
                                    End If

                                    If checkbarcodesendsupl(HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString)) Then
                                        HI.MG.ShowMsg.mInfo(2206271809, "กรุณาลบบาร์โค๊ดส่ง supl ก่อน", Me.Text)
                                        Exit Sub
                                    End If


                                    _Str = "  DELETE FROM A "
                                    _Str &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle_Detail AS A INNER JOIN"
                                    _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B ON A.FTBarcodeBundleNo = B.FTBarcodeBundleNo"
                                    _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                    _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') <>'1'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)



                                    _Str = "  DELETE FROM B "
                                    _Str &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B "
                                    _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                    _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') <>'1'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)


                                Next


                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                            'Call LoadOrderProdDetailInfo(Me.FTSMPOrderNo.Text)
                        End If
                    End With

                Case otbsendsupl.Name
                    With ogcbundle
                        If Not (.DataSource Is Nothing) Then

                            With CType(.DataSource, DataTable)
                                .AcceptChanges()

                                For Each R As DataRow In .Select("FTSelect='1'")

                                    If checkbarcodebundle(HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString)) Then
                                        HI.MG.ShowMsg.mInfo(2206291552, "กรุณาลบบาร์โค๊ด ก่อน", Me.Text)
                                        Exit Sub
                                    End If



                                    _Str = "  DELETE FROM A "
                                    _Str &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle_Detail AS A INNER JOIN"
                                    _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B ON A.FTBarcodeBundleNo = B.FTBarcodeBundleNo"
                                    _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                    _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') <>'1'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)



                                    _Str = "  DELETE FROM B "
                                    _Str &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B "
                                    _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                    _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') <>'1'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)






                                    _Str = "  DELETE FROM A "
                                    _Str &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl AS A  "
                                    _Str &= vbCrLf & "  WHERE A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"


                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)


                                    _Str = "  DELETE FROM A "
                                    _Str &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTOrderProd_SendSupl_Detail AS A  "
                                    _Str &= vbCrLf & "  WHERE A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)



                                Next


                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                            ' Call LoadOrderProdDetailInfo(Me.FTSMPOrderNo.Text)
                        End If
                    End With
                Case XtraTabBarcode.Name

                    With ogcbarcode
                        If Not (.DataSource Is Nothing) Then

                            With CType(.DataSource, DataTable)
                                .AcceptChanges()

                                For Each R As DataRow In .Select("FTSelect='1'")



                                    '_Str = "  DELETE FROM A "
                                    '_Str &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle_Detail AS A INNER JOIN"
                                    '_Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B ON A.FTBarcodeBundleNo = B.FTBarcodeBundleNo"
                                    '_Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                    '_Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') ='1'"

                                    'HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)



                                    _Str = "  update    B  set B.FTStateGenBarcode ='0'"
                                    _Str &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B "
                                    _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                                    _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') ='1'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)




                                    _Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SingleCon"
                                    _Str &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString)) & "' "
                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)

                                    _Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl"
                                    _Str &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString)) & "' "



                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_SAMPLE)
                                Next


                            End With

                            CType(.DataSource, DataTable).AcceptChanges()
                            'Call LoadOrderProdDetailInfo(Me.FTSMPOrderNo.Text)
                        End If
                    End With

            End Select


            Call ocmrefresh_Click(sender, e)
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub RepositoryItemCheckEdit4_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit4.EditValueChanging
        Try

            Dim _State As String = "0"
            _State = e.NewValue

            With ogvselectbundle
                .SetRowCellValue(.FocusedRowHandle, .Columns.ColumnByFieldName("FTSelect"), _State)
            End With

            CType(Me.ogcselectbundle.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvsendsupl_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvsendsupl.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down

                With CType(Me.ogcsendsupl.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTPartName='' OR FTPartName IS NULL").Length <= 0 Then
                        .Rows.Add(0, "", "", "", "", "", "", 0, 0, "", 0, "", 0, "", 0, "", "")
                        .AcceptChanges()
                    End If
                End With

            Case System.Windows.Forms.Keys.Delete

                If CheckCreateBarcode(False) Then
                    With Me.ogvsendsupl
                        If .FocusedRowHandle < 0 Then Exit Sub
                        If "" & .GetFocusedRowCellValue("FTSendSuplRef").ToString <> "" Then Exit Sub
                        .DeleteRow(.FocusedRowHandle)
                    End With
                Else
                    With Me.ogvsendsupl
                        If .FocusedRowHandle < 0 Then Exit Sub
                        .DeleteRow(.FocusedRowHandle)
                    End With
                End If

                CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        End Select
    End Sub

    Private Function CheckCreateBarcode(Optional StateShowMsg As Boolean = True) As Boolean
        Dim _State As Boolean = False
        Dim _Qry As String = ""

        _Qry = " Select TOP 1  FTBarcodeBundleNo "
        _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE        (FTOrderProdNo ='" & Me.FTSMPOrderNo.Text & "')"
        _Qry &= vbCrLf & "  AND  ISNULL(FTStateGenBarcode,'') ='1' "

        _State = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")

        If (_State) Then

            If (StateShowMsg) Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้าง Barcode แล้ว ไม่สามารถ ทำการ ลบ เพิ่ม หรือ แก้ไขได้ !!!!", 1505310001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return _State
    End Function
End Class