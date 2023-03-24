Imports System.Data.SqlClient

Public Class CopyPermission

    Private FromRoleID As Integer = 0
    Property FRID As Integer
        Get
            Return FromRoleID
        End Get
        Set(value As Integer)
            FromRoleID = value
        End Set
    End Property


    Private _SaveProcess As Boolean = False
    Property SaveProces As Boolean
        Get
            Return _SaveProcess
        End Get
        Set(value As Boolean)
            _SaveProcess = value
        End Set
    End Property


    Private Function VerifyData()
        Dim _Pass As Boolean = False
        If otboldpermissionname.Text <> "" Then
            If FTPermissionCode.Text <> "" Then
                If Me.FTPermissionNameTH.Text <> "" Then
                    If Me.FTPermissionNameEN.Text <> "" Then

                        Dim cmdstring As String = ""
                        cmdstring = "SELECT TOP 1 FTPermissionCode FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission WHERE FTPermissionCode='" & HI.UL.ULF.rpQuoted(FTPermissionCode.Text.Trim) & "'"
                        If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "") = "" Then
                            _Pass = True
                        Else
                            FTPermissionCode.Focus()
                            FTPermissionCode.SelectAll()
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPermissionNameEN_lbl.Text)
                        Me.FTPermissionNameEN.Focus()
                        Me.FTPermissionNameEN.SelectAll()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPermissionNameTH_lbl.Text)
                    Me.FTPermissionNameTH.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPermissionCode_lbl.Text & " Invalid !!! ")
                Me.FTPermissionCode.Focus()
                Me.FTPermissionCode.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, otbpldpassword_lbl.Text)
            Me.otboldpermissionname.Focus()
            Me.otboldpermissionname.SelectAll()
        End If

        Return _Pass
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.SaveProces = False
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData() Then
            If Saveata() Then
                Me.SaveProces = True
                Me.Close()
            End If
        End If
    End Sub


    Private Function Saveata() As Boolean
        Try

            Dim MSysID As Integer = 0

            MSysID = HI.SE.RunID.GetRunNoID("TSEPermission", "FNHSysPermissionID", Conn.DB.DataBaseName.DB_SECURITY)

            Dim cmdstring As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SECURITY)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission(FTInsUser, FDInsDate, FTInsTime,  FNHSysPermissionID, FTPermissionCode, FTPermissionNameTH, FTPermissionNameEN) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                cmdstring &= vbCrLf & " ," & MSysID
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPermissionCode.Text.Trim) & "' "
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPermissionNameTH.Text.Trim) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPermissionNameEN.Text.Trim) & "'"

                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If


                cmdstring = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysModuleID) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & " ,FNHSysModuleID "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysCmpId) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & " ,FNHSysCmpId "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FTMnuName) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & " ,FTMnuName "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FTMnuName,FTFormName,FTObjName) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & " , FTMnuName,FTFormName,FTObjName "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysEmpTypeId,FTStateSalary,FTStateAll,FTStateAllUnit) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & "  , FNHSysEmpTypeId,FTStateSalary,FTStateAll,FTStateAllUnit "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysSectId,FTStateAll,FTStateSalary) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & "  ,FNHSysSectId,FTStateAll,FTStateSalary "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                cmdstring &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeUnitSect(FTInsUser, FDInsDate, FTInsTime, FNHSysPermissionID, FNHSysUnitSectId) "
                cmdstring &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " ," & MSysID & " "
                cmdstring &= vbCrLf & "  , FNHSysUnitSectId "
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeUnitSect AS X WITH(NOLOCK) "
                cmdstring &= vbCrLf & " WHERE X.FNHSysPermissionID=" & FRID & ""

                If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub wUserChangePassword_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

End Class