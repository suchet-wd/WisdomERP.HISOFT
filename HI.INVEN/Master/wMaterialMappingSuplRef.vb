Public Class wMaterialMappingSuplRef

    Private _AddItem As wMaterialMappingSuplRefAddItem
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _AddItem = New wMaterialMappingSuplRefAddItem
        HI.TL.HandlerControl.AddHandlerObj(_AddItem)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItem.Name.ToString.Trim, _AddItem)
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Public Sub Loadata()
        Dim _Qry As String
        _Qry = "SELECT        A.FNHSysSuplId, A.FNHSysRawMatId, IM.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", IM.FTRawMatNameTH AS FTRawMatName"
        Else
            _Qry &= vbCrLf & ", IM.FTRawMatNameEN AS FTRawMatName"
        End If

        _Qry &= vbCrLf & ", ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & " , ISNULL(IMZ.FTRawMatSizeCode,'') AS FTRawMatSizeCode "
        _Qry &= vbCrLf & "  , A.FTSuplRawMatCodeRef, A.FTSuplColorCodeRef,A.FTSuplSizeCodeRef,A.FTRemark"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMaterialMappingSuplRef AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMZ WITH(NOLOCK)  ON IM.FNHSysRawMatSizeId = IMZ.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)  ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
        _Qry &= vbCrLf & " WHERE  A.FNHSysSuplId=" & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & " "
        _Qry &= vbCrLf & " ORDER BY IM.FTRawMatCode,ISNULL(IMC.FTRawMatColorCode,''),ISNULL(IMZ.FTRawMatSizeCode,'') "

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysSuplId_EditValueChanged), New Object() {sender, e})
        Else

            If FNHSysSuplId.Text <> "" Then
                Dim _Qry As String = "SELECT TOP 1 FNHSysSuplId  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK) WHERE FTSuplCode ='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "' "
                FNHSysSuplId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
            End If

            Call Loadata()

        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.Loadata()
    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click
        Try
            If Me.FNHSysSuplId.Text <> "" And Me.FNHSysSuplId.Properties.Tag.ToString <> "" Then

                With ogvdetail
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    If Not (ocmedit.Enabled) Then Exit Sub
                    Dim FNHSysRawMatId, FTSuplRawMatCodeRef, FTSuplColorCodeRef, FTSuplSizeCodeRef, FTRemark As String
                    Dim MatCode, MatColor, MatSize As String
                    FNHSysRawMatId = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysRawMatId").ToString
                    FTSuplRawMatCodeRef = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSuplRawMatCodeRef").ToString
                    FTSuplColorCodeRef = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSuplColorCodeRef").ToString
                    FTSuplSizeCodeRef = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSuplSizeCodeRef").ToString
                    FTRemark = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRemark").ToString
                    MatCode = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRawMatCode").ToString
                    MatColor = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRawMatColorCode").ToString
                    MatSize = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRawMatSizeCode").ToString


                      With _AddItem
                        HI.TL.HandlerControl.ClearControl(_AddItem)
                        .StateProcType = wMaterialMappingSuplRefAddItem.wProcType.Edit
                        .MainObject = Me
                        .SuplID = Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString))
                        .FTRawMatColorCode.Text = MatColor
                        .FTRawMatSizeCode.Text = MatSize
                        .FNHSysRawMatId.Text = MatCode
                        .FNHSysRawMatId.Properties.Tag = FNHSysRawMatId
                        .FTSuplRawMatCodeRef.Text = FTSuplRawMatCodeRef
                        .FTSuplColorCodeRef.Text = FTSuplColorCodeRef
                        .FTSuplSizeCodeRef.Text = FTSuplSizeCodeRef
                        .FTRemark.Text = FTRemark
                        .FNHSysRawMatId.Properties.Buttons(0).Enabled = False
                        .ShowDialog()
                    End With
                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplId_lbl.Text)
                FNHSysSuplId.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click


        Try
            If Me.FNHSysSuplId.Text <> "" And Me.FNHSysSuplId.Properties.Tag.ToString <> "" Then

                With ogvdetail
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    If Not (ocmedit.Enabled) Then Exit Sub
                    Dim FNHSysRawMatId, FTSuplRawMatCodeRef, FTSuplColorCodeRef, FTSuplSizeCodeRef, FTRemark As String
                    Dim MatCode, MatColor, MatSize As String
                    FNHSysRawMatId = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysRawMatId").ToString
                    FTSuplRawMatCodeRef = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSuplRawMatCodeRef").ToString
                    FTSuplColorCodeRef = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSuplColorCodeRef").ToString
                    FTSuplSizeCodeRef = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTSuplSizeCodeRef").ToString
                    FTRemark = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRemark").ToString
                    MatCode = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRawMatCode").ToString
                    MatColor = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRawMatColorCode").ToString
                    MatSize = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRawMatSizeCode").ToString

                    If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, MatCode & "   " & MatColor & "   " & MatSize) = True Then

                        Dim _Qry As String
                        _Qry = " Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMaterialMappingSuplRef "
                        _Qry &= vbCrLf & "  WHERE  FNHSysSuplId=" & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & " "
                        _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(FNHSysRawMatId)) & " "

                        If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)) Then
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                        Else

                            Me.Loadata()

                        End If
                    End If

                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplId_lbl.Text)
                FNHSysSuplId.Focus()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Try
            If Me.FNHSysSuplId.Text <> "" And Me.FNHSysSuplId.Properties.Tag.ToString <> "" Then
                With _AddItem
                    HI.TL.HandlerControl.ClearControl(_AddItem)
                    .StateProcType = wMaterialMappingSuplRefAddItem.wProcType.New
                    .MainObject = Me
                    .SuplID = Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString))
                    .FNHSysRawMatId.Properties.Buttons(0).Enabled = True
                    .ShowDialog()
                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplId_lbl.Text)
                FNHSysSuplId.Focus()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        If Me.ocmedit.Enabled Then
            ocmedit_Click(Me.ocmedit, New System.EventArgs)
        End If
    End Sub
End Class