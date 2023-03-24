Public Class wOperationByOrderProd

    Private _Copy As wCopyOperationByStyle
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Copy = New wCopyOperationByStyle
        HI.TL.HandlerControl.AddHandlerObj(_Copy)

        Dim _SystemLang As New ST.SysLanguage

        Try

            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Copy.Name.ToString.Trim, _Copy)

        Catch ex As Exception
        Finally
        End Try

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

    Public Sub LoadCodeBySysIDInfo(ByVal Key As Object)

        Dim _Qry As String = "SELECT TOP 1 FTOrderNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERTOrder AS MS WITH (NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        FTOrderNo.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

    End Sub

    Public Sub LoadDataInfo(ByVal Key As Object)

        Dim _Qry As String = ""
        _Qry = " SELECT     SOP.FNSeq"

        _Qry &= vbCrLf & "  , SOP.FNHSysOperationId AS FNHSysOperationId_Hide"
        _Qry &= vbCrLf & "  , O1.FTOperationCode AS FNHSysOperationId"
        _Qry &= vbCrLf & "  , SOP.FNHSysMarkId AS FNHSysMarkId_Hide"
        _Qry &= vbCrLf & "  , M.FTMarkCode AS FNHSysMarkId"
        _Qry &= vbCrLf & "  , SOP.FNOperationState AS FNOperationState_Hide"
        _Qry &= vbCrLf & "  , SOP.FNHSysOperationIdTo AS FNHSysOperationIdTo_Hide"
        _Qry &= vbCrLf & "  , O2.FTOperationCode AS FNHSysOperationIdTo"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , O1.FTOperationNameTH AS FNHSysOperationId_None"
            _Qry &= vbCrLf & "  , M.FTMarkNameTH AS FNHSysMarkId_None"
            _Qry &= vbCrLf & "  ,L.FTNameTH AS FNOperationState"
            _Qry &= vbCrLf & "  ,O2.FTOperationNameTH AS FNHSysOperationIdTo_None"
        Else
            _Qry &= vbCrLf & "  ,  O1.FTOperationNameEN  AS FNHSysOperationId_None "
            _Qry &= vbCrLf & "  ,  M.FTMarkNameEN AS FNHSysMarkId_None"
            _Qry &= vbCrLf & "  , L.FTNameEN AS FNOperationState"
            _Qry &= vbCrLf & "  ,O2.FTOperationNameEN AS FNHSysOperationIdTo_None"
        End If
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPrice,0) AS FNPrice"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS SOP WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS O1 WITH (NOLOCK)  ON SOP.FNHSysOperationId = O1.FNHSysOperationId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS O2 WITH (NOLOCK)  ON SOP.FNHSysOperationIdTo = O2.FNHSysOperationId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M WITH (NOLOCK)  ON SOP.FNHSysMarkId = M.FNHSysMarkId"
        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN (SELECT        FNListIndex, FTNameTH, FTNameEN, FTReferCode"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
        _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNOperationState')) AS L ON SOP.FNOperationState =L.FNListIndex"
        _Qry &= vbCrLf & "   WHERE SOP.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

        If Me.ocmsave.Enabled Then
            Call InitNewRow()
        End If

    End Sub


    Private _GenTab As Boolean = False
    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)
        otbjobprod.TabPages.Clear()
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        _GenTab = True
        Me.ogcoperation.DataSource = Nothing
        Dim _Qry As String = ""
        Dim _dtprod As New DataTable

        Try
            _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTOrderProdNo  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P With(Nolock)"
            _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
            _Qry &= vbCrLf & "  Order By FTOrderProdNo  "

            _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _dtprod.Rows

                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                With Otp
                    .Name = R!FTOrderProdNo.ToString
                    .Text = R!FTOrderProdNo.ToString
                End With

                otbjobprod.TabPages.Add(Otp)

            Next
        Catch ex As Exception
        End Try
       
        If _dtprod.Rows.Count > 0 Then
            otbjobprod.SelectedTabPageIndex = 0
            Call LoadDataInfo(otbjobprod.SelectedTabPage.Name.ToString)
        End If

        _GenTab = False

        _dtprod.Dispose()
        _Spls.Close()
    End Sub

    Private Sub InitNewRow()


        Dim _Qry As String = ""
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry = "   SELECT       TOP 1  FTNameTH"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNOperationState') ORDER BY  FNListIndex "
        Else
            _Qry = "   SELECT       TOP 1  FTNameEN"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WITH (NOLOCK)"
            _Qry &= vbCrLf & "  WHERE        (FTListName = N'FNOperationState') ORDER BY  FNListIndex "

        End If

        With CType(Me.ogcoperation.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
            .Rows(.Rows.Count - 1)!FNHSysOperationId_Hide = 0
            .Rows(.Rows.Count - 1)!FNHSysOperationId = ""
            .Rows(.Rows.Count - 1)!FNHSysMarkId_Hide = 0
            .Rows(.Rows.Count - 1)!FNHSysMarkId = ""
            .Rows(.Rows.Count - 1)!FNOperationState_Hide = 0
            .Rows(.Rows.Count - 1)!FNHSysOperationIdTo_Hide = 0
            .Rows(.Rows.Count - 1)!FNHSysOperationIdTo = ""
            .Rows(.Rows.Count - 1)!FNHSysOperationId_None = ""
            .Rows(.Rows.Count - 1)!FNHSysMarkId_None = ""
            .Rows(.Rows.Count - 1)!FNOperationState = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
            .Rows(.Rows.Count - 1)!FNHSysOperationIdTo_None = ""
            .Rows(.Rows.Count - 1)!FNPrice = 0

            .AcceptChanges()

        End With
    End Sub
#End Region

#Region "General"

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDataInfo(FTOrderNo.Text)
        End If
    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try
            If _GenTab = True Then Exit Sub
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbjobprod.SelectedTabPage Is Nothing) Then
                    Call LoadDataInfo(otbjobprod.SelectedTabPage.Name.ToString)
                Else
                    Me.ogcoperation.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvoperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvoperation.KeyDown

        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then

            If Me.ogcoperation.DataSource Is Nothing Then
                Exit Sub
            End If

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Down

                    If Me.ocmsave.Enabled = False Then
                        Exit Sub
                    End If

                    With CType(Me.ogcoperation.DataSource, DataTable)

                        .AcceptChanges()

                        If .Select("FNHSysOperationId=''  OR FNOperationState=''").Length <= 0 Then

                            Call InitNewRow()

                            .AcceptChanges()

                            Me.ogvoperation.ClearSelection()
                            Me.ogvoperation.SelectRow(.Rows.Count - 1)
                            Me.ogvoperation.FocusedRowHandle = .Rows.Count - 1
                            Me.ogvoperation.FocusedColumn = Me.ogvoperation.Columns.ColumnByFieldName("FNHSysOperationId")

                        End If

                    End With
                Case System.Windows.Forms.Keys.Delete
                    If Me.ocmsave.Enabled = False Then
                        Exit Sub
                    End If

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
        End If

    End Sub
    Private Function CheckUseOperation(Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_ChekUseOperationByStyle " & Key & ",'1'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Not (CheckUseOperation(HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text))) Then
            HI.MG.ShowMsg.mInfo("มีการใช้ขั้นตอน Order นี้แล้ว กรุณาตรวจสอบ !!! ", 1611100932, Me.Text)
            Exit Sub
        End If

        If FTOrderNo.Properties.Tag.ToString <> "" AndAlso FTOrderNo.Text <> "" Then
            If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
                With CType(Me.ogcoperation.DataSource, DataTable)
                    .AcceptChanges()
                    Dim _FNSeq As Integer = 0
                    Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)
                    Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    Try

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        End If

                        For Each R As DataRow In .Select("FNHSysOperationId_Hide>0", "FNSeq")

                            _Qry = "SELECT TOP 1 FTOrderProdNo "
                            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd"
                            _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                            _Qry &= vbCrLf & " AND  FNHSysOperationId=" & Integer.Parse(Val(R!FNHSysOperationId_Hide.ToString)) & " "

                            If HI.Conn.SQLConn.GetFieldByNameOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                                _FNSeq = _FNSeq + 1

                                _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd"
                                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FTOrderProdNo, FNSeq, FNHSysOperationId, FNHSysMarkId, FNOperationState, FNHSysOperationIdTo,FNPrice)"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                                _Qry &= vbCrLf & " ," & _FNSeq & " "
                                _Qry &= vbCrLf & " ," & Val(R!FNHSysOperationId_Hide.ToString) & " "
                                _Qry &= vbCrLf & " ," & Val(R!FNHSysMarkId_Hide.ToString) & " "
                                _Qry &= vbCrLf & " ," & Val(R!FNOperationState_Hide.ToString) & " "
                                _Qry &= vbCrLf & " ," & Val(R!FNHSysOperationIdTo_Hide.ToString) & " "
                                _Qry &= vbCrLf & " ," & Val(R!FNPrice.ToString) & " "

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                    _Spls.Close()
                                    Exit Sub
                                End If
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
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Order Producttion No...!!!", 1404220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        End If

    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs)
        'If FNHSysStyleId.Properties.Tag.ToString <> "" And FNHSysStyleId.Text <> "" Then
        '    If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
        '        With _Copy
        '            HI.ST.Lang.SP_SETxLanguage(_Copy)
        '            .Process = False
        '            .FNHSysStyleId.Text = ""
        '            .FNHSysStyleId_None.Text = ""
        '            .FNHSysStyleIdTo.Text = FNHSysStyleId.Text
        '            .FNHSysStyleIdTo_None.Text = FNHSysStyleId_None.Text
        '            .ShowDialog()

        '            If (.Process) Then


        '                Dim _Spls As New HI.TL.SplashScreen("Copy...Data Please Wait.", Me.Text)
        '                Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
        '                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        '                HI.Conn.SQLConn.SqlConnectionOpen()
        '                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        '                Try

        '                    _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd"
        '                    _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FTOrderProdNo, FNSeq, FNHSysOperationId, FNHSysMarkId, FNOperationState, FNHSysOperationIdTo)"
        '                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        '                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
        '                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
        '                    _Qry &= vbCrLf & " ,  " & Val(Me.otbjobprod.SelectedTabPage.Name.ToString) & ", FNSeq, FNHSysOperationId, FNHSysMarkId, FNOperationState, FNHSysOperationIdTo"
        '                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd"
        '                    _Qry &= vbCrLf & "   WHERE FNHSysStyleId =" & Val(.FNHSysStyleId.Properties.Tag.ToString) & " "

        '                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '                        HI.Conn.SQLConn.Tran.Rollback()
        '                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        '                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                        _Spls.Close()
        '                        Exit Sub
        '                    End If


        '                    HI.Conn.SQLConn.Tran.Commit()
        '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                    _Spls.Close()

        '                    Me.LoadDataInfo(Me.otbjobprod.SelectedTabPage.Name.ToString)

        '                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                Catch ex As Exception
        '                    HI.Conn.SQLConn.Tran.Rollback()
        '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '                    _Spls.Close()
        '                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '                End Try


        '            End If

        '        End With
        '    Else
        '        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Order Producttion No...!!!", 1404220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    End If

        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
        '    FTOrderNo.Focus()
        'End If
    End Sub

    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvoperation.OptionsView.ShowAutoFilterRow = False
        ogvoperation.OptionsSelection.MultiSelect = False
        ogvoperation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

    End Sub

    Private Sub ocmimportoperationbystyle_Click(sender As Object, e As EventArgs) Handles ocmimportoperationbystyle.Click
        If FTOrderNo.Properties.Tag.ToString <> "" AndAlso FTOrderNo.Text <> "" Then
            If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Import Operation จาก Style ใช่หรือไม่ ", 1405300012) Then
                    Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)
                    Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "


                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    Try

                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FTOrderProdNo, FNSeq, FNHSysOperationId, FNHSysMarkId, FNOperationState, FNHSysOperationIdTo,FNPrice)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                        _Qry &= vbCrLf & " ,FNSeq "
                        _Qry &= vbCrLf & " ,FNHSysOperationId "
                        _Qry &= vbCrLf & " ,FNHSysMarkId"
                        _Qry &= vbCrLf & " ,FNOperationState "
                        _Qry &= vbCrLf & " ,FNHSysOperationIdTo"
                        _Qry &= vbCrLf & " ,FNPrice"
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle WITH(NOLOCK)"
                        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Val(HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Properties.Tag.ToString)) & " "

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
                        Call LoadDataInfo(otbjobprod.SelectedTabPage.Name.ToString)

                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End Try
                End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Order Producttion No...!!!", 1404220001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        End If
    End Sub

#End Region

   
    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDataInfo(FTOrderNo.Text)
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        FTOrderNo.Focus()
        FTOrderNo.SelectAll()
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub
End Class