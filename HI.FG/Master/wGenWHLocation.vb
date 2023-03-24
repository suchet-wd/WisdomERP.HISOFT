Public Class wGenWHLocation

    Private _ProcGen As Boolean
    Public Property ProcGen As Boolean
        Get
            Return _ProcGen
        End Get
        Set(value As Boolean)
            _ProcGen = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.ProcGen = False
        Me.Close()
    End Sub

    Private Sub FNStateGenWHLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNStateGenWHLocation.SelectedIndexChanged
        Try
            Me.PNNormal.Visible = (FNStateGenWHLocation.SelectedIndex = 0)
            Me.PNRowCol.Visible = (FNStateGenWHLocation.SelectedIndex = 1 Or FNStateGenWHLocation.SelectedIndex = 2)
            Me.PNAll.Visible = (FNStateGenWHLocation.SelectedIndex = 3 Or FNStateGenWHLocation.SelectedIndex = 4)
            HI.TL.HandlerControl.ClearControl(Me, , {"FNStateGenWHLocation"})
        Catch ex As Exception
        End Try
    End Sub

    Private Function CheckSaveData() As Boolean
        If Me.FNHSysWHFGId.Text = "" Or FNHSysWHFGId.Properties.Tag.ToString = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysWHId_lbl.Text)
            FNHSysWHFGId.Focus()
            Return False
        End If
        Select Case FNStateGenWHLocation.SelectedIndex
            Case 0
                If Trim(Me.txtLocNormal.Text) = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, txtLocNormal_lbl.Text)
                    txtLocNormal.Focus()
                    Return False
                End If
            Case 1, 2

                If (Me.txtO2SRow.Value) = 0 Or (Me.txtO2SCol.Value) = 0 Or (Me.txtO2ERow.Value) = 0 Or (Me.txtO2ECol.Value) = 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  ข้อมูล  Row - Column  ให้ถูกต้อง    !!!", 1406010001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO3SRow.Focus()
                    Return False
                End If

                If (Me.txtO2ERow.Value) < Val(Me.txtO2SRow.Value) Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  ข้อมูล  Row   ให้ถูกต้อง    !!!", 1406010002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO3ERow.Focus()
                    Return False
                End If

                If (Me.txtO2ECol.Value) < (Me.txtO2SCol.Value) Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  ข้อมูล  Column   ให้ถูกต้อง    !!!", 1406010003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO2ECol.Focus()
                    Return False
                End If
            Case 3, 4
                If Trim(Me.txtO3Loc.Text) = "" Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  Location  !!!", 1406010003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO3Loc.Focus()
                    Return False
                End If

                If (Me.txtO3SRow.Value) = 0 Or (Me.txtO3SCol.Value) = 0 Or (Me.txtO3ERow.Value) = 0 Or (Me.txtO3ECol.Value) = 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  ข้อมูล  Row - Column  ให้ถูกต้อง    !!!", 1406010004, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO3SRow.Focus()
                    Return False
                End If

                If (Me.txtO3ERow.Value) < (Me.txtO3SRow.Value) Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  ข้อมูล  Row   ให้ถูกต้อง    !!!", 1406010005, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO3ERow.Focus()
                    Return False
                End If

                If (Me.txtO3ECol.Value) < (Me.txtO3SCol.Value) Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุ  ข้อมูล  Column   ให้ถูกต้อง    !!!", 1406010006, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    txtO3ECol.Focus()
                    Return False
                End If

        End Select

        Return True
    End Function

    Private Function SaveData() As Boolean
        Dim oLocation As String
        Dim HLoc As String
        Dim Strsql As String
        Dim _FNHSysWHId As Integer = Integer.Parse(Val(FNHSysWHFGId.Properties.Tag.ToString))
        Dim _FNHSysWHLocId As Integer = 0


        Try
            Select Case FNStateGenWHLocation.SelectedIndex

                Case 0
                    oLocation = Trim(Me.txtLocNormal.Text)
                    Strsql = "Select Top 1  FTWHLocCode   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG  WITH(NOLOCK)  WHERE FNHSysWHFGId=" & _FNHSysWHId & " AND FTWHLocCode='" & HI.UL.ULF.rpQuoted(oLocation) & "' "

                    If HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_MASTER) = "" Then

                        _FNHSysWHLocId = HI.SE.RunID.GetRunNoID("TCNMWarehouseLocationFG", "FNHSysWHLocId", Conn.DB.DataBaseName.DB_MASTER)

                        Strsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG ( "
                        Strsql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysWHLocId, FNHSysWHFGId"
                        Strsql &= vbCrLf & "  , FTWHLocCode, FTWHLocNameTH, FTWHLocNameEN, FTRemark, FTStateActive"
                        Strsql &= vbCrLf & " ) "
                        Strsql &= vbCrLf & "   VALUES('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        Strsql &= vbCrLf & " ," & _FNHSysWHLocId & " "
                        Strsql &= vbCrLf & " ," & _FNHSysWHId & " "
                        Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                        Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                        Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                        Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1')"


                        HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_MASTER)

                    End If

                Case 1

                    For I = CInt(Me.txtO2SRow.Text) To CInt(Me.txtO2ERow.Text)
                        For J = CInt(Me.txtO2SCol.Text) To CInt(Me.txtO2ECol.Text)
                            oLocation = Format(I, "00") & "-" & Format(J, "00")

                            Strsql = "Select Top 1  FTWHLocCode   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG  WITH(NOLOCK)  WHERE FNHSysWHFGId=" & _FNHSysWHId & " AND FTWHLocCode='" & HI.UL.ULF.rpQuoted(oLocation) & "' "

                            If HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_MASTER) = "" Then


                                _FNHSysWHLocId = HI.SE.RunID.GetRunNoID("TCNMWarehouseLocationFG", "FNHSysWHLocId", Conn.DB.DataBaseName.DB_MASTER)

                                Strsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG ( "
                                Strsql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysWHLocId, FNHSysWHFGId"
                                Strsql &= vbCrLf & "  , FTWHLocCode, FTWHLocNameTH, FTWHLocNameEN, FTRemark, FTStateActive"
                                Strsql &= vbCrLf & " ) "
                                Strsql &= vbCrLf & "   VALUES('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                Strsql &= vbCrLf & " ," & _FNHSysWHLocId & " "
                                Strsql &= vbCrLf & " ," & _FNHSysWHId & " "
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1')"

                                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_MASTER)

                            End If
                        Next
                    Next
                Case 2
                    HLoc = FNHSysWHFGId.Text & "-"
                    For I = CInt(Me.txtO2SRow.Text) To CInt(Me.txtO2ERow.Text)
                        For J = CInt(Me.txtO2SCol.Text) To CInt(Me.txtO2ECol.Text)
                            oLocation = HLoc & Format(I, "00") & "-" & Format(J, "00")

                            Strsql = "Select Top 1  FTWHLocCode   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG  WITH(NOLOCK)  WHERE FNHSysWHFGId=" & _FNHSysWHId & " AND FTWHLocCode='" & HI.UL.ULF.rpQuoted(oLocation) & "' "

                            If HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_MASTER) = "" Then

                                _FNHSysWHLocId = HI.SE.RunID.GetRunNoID("TCNMWarehouseLocationFG", "FNHSysWHLocId", Conn.DB.DataBaseName.DB_MASTER)

                                Strsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG ( "
                                Strsql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysWHLocId, FNHSysWHFGId"
                                Strsql &= vbCrLf & "  , FTWHLocCode, FTWHLocNameTH, FTWHLocNameEN, FTRemark, FTStateActive"
                                Strsql &= vbCrLf & " ) "
                                Strsql &= vbCrLf & "   VALUES('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                Strsql &= vbCrLf & " ," & _FNHSysWHLocId & " "
                                Strsql &= vbCrLf & " ," & _FNHSysWHId & " "
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1')"

                                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_MASTER)

                            End If
                        Next
                    Next
                Case 3

                    HLoc = Trim(Me.txtO3Loc.Text) & "-"
                    For I = CInt(Me.txtO3SRow.Text) To CInt(Me.txtO3ERow.Text)
                        For J = CInt(Me.txtO3SCol.Text) To CInt(Me.txtO3ECol.Text)
                            oLocation = HLoc & Format(I, "00") & "-" & Format(J, "00")

                            Strsql = "Select Top 1  FTWHLocCode   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG  WITH(NOLOCK)  WHERE FNHSysWHFGId=" & _FNHSysWHId & " AND FTWHLocCode='" & HI.UL.ULF.rpQuoted(oLocation) & "' "

                            If HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_MASTER) = "" Then

                                _FNHSysWHLocId = HI.SE.RunID.GetRunNoID("TCNMWarehouseLocationFG", "FNHSysWHLocId", Conn.DB.DataBaseName.DB_MASTER)

                                Strsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG ( "
                                Strsql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysWHLocId, FNHSysWHFGId"
                                Strsql &= vbCrLf & "  , FTWHLocCode, FTWHLocNameTH, FTWHLocNameEN, FTRemark, FTStateActive"
                                Strsql &= vbCrLf & " ) "
                                Strsql &= vbCrLf & "   VALUES('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                Strsql &= vbCrLf & " ," & _FNHSysWHLocId & " "
                                Strsql &= vbCrLf & " ," & _FNHSysWHId & " "
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1')"

                                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_MASTER)

                            End If
                        Next
                    Next
                Case 4

                    HLoc = FNHSysWHFGId.Text & "-" & Trim(Me.txtO3Loc.Text) & "-"
                    For I = CInt(Me.txtO3SRow.Text) To CInt(Me.txtO3ERow.Text)
                        For J = CInt(Me.txtO3SCol.Text) To CInt(Me.txtO3ECol.Text)
                            oLocation = HLoc & Format(I, "00") & "-" & Format(J, "00")
                            Strsql = "Select Top 1  FTWHLocCode   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG  WITH(NOLOCK)  WHERE FNHSysWHFGId=" & _FNHSysWHId & " AND FTWHLocCode='" & HI.UL.ULF.rpQuoted(oLocation) & "' "

                            If HI.Conn.SQLConn.GetField(Strsql, Conn.DB.DataBaseName.DB_MASTER) = "" Then

                                _FNHSysWHLocId = HI.SE.RunID.GetRunNoID("TCNMWarehouseLocationFG", "FNHSysWHLocId", Conn.DB.DataBaseName.DB_MASTER)

                                Strsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG ( "
                                Strsql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysWHLocId, FNHSysWHFGId"
                                Strsql &= vbCrLf & "  , FTWHLocCode, FTWHLocNameTH, FTWHLocNameEN, FTRemark, FTStateActive"
                                Strsql &= vbCrLf & " ) "
                                Strsql &= vbCrLf & "   VALUES('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                Strsql &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                Strsql &= vbCrLf & " ," & _FNHSysWHLocId & " "
                                Strsql &= vbCrLf & " ," & _FNHSysWHId & " "
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(oLocation) & "'"
                                Strsql &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1')"

                                HI.Conn.SQLConn.ExecuteOnly(Strsql, Conn.DB.DataBaseName.DB_MASTER)

                            End If
                        Next
                    Next
            End Select

            Return True

        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Sub ocmgenbarcode_Click(sender As Object, e As EventArgs) Handles ocmgenbarcode.Click
        If Me.CheckSaveData Then
            If Me.SaveData Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcGen = True
                Me.Close()

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If

    End Sub
End Class