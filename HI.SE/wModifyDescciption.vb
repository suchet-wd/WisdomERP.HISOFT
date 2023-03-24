Public Class wModifyDescciption 

    Enum Actype As Integer
        [Module] = 0
        [Menu] = 1
        [Form] = 2
        [Object] = 3
    End Enum

    Private _ProcType As Actype = Actype.Module
    Public Property ProcType As Actype
        Get
            Return _ProcType
        End Get
        Set(value As Actype)
            _ProcType = value
        End Set
    End Property

    Private _ModuleID As String = ""
    Public Property ModuleID As String
        Get
            Return _ModuleID
        End Get
        Set(value As String)
            _ModuleID = value
        End Set
    End Property

    Private _MenuID As String = ""
    Public Property MenuID As String
        Get
            Return _MenuID
        End Get
        Set(value As String)
            _MenuID = value
        End Set
    End Property

    Private _FormID As String = ""
    Public Property FormID As String
        Get
            Return _FormID
        End Get
        Set(value As String)
            _FormID = value
        End Set
    End Property

    Private _ObjectID As String = ""
    Public Property ObjectID As String
        Get
            Return _ObjectID
        End Get
        Set(value As String)
            _ObjectID = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.FTDescTH.Text.Trim <> "" Then
            If Me.FTDescEN.Text.Trim <> "" Then
                Dim _Str As String = ""
                Select Case Me.ProcType
                    Case Actype.Module
                        _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysModule SET FTModuleNameTH='" & HI.UL.ULF.rpQuoted(Me.FTDescTH.Text.Trim) & "' "
                        _Str &= vbCrLf & ",FTModuleNameEN='" & HI.UL.ULF.rpQuoted(Me.FTDescEN.Text.Trim) & "' "
                        _Str &= vbCrLf & " WHERE FNHSysModuleID=" & Val(Me.ModuleID) & " "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
                    Case Actype.Menu
                        _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysMenu SET FTCaptionTH='" & HI.UL.ULF.rpQuoted(Me.FTDescTH.Text.Trim) & "' "
                        _Str &= vbCrLf & ",FTCaptionEN='" & HI.UL.ULF.rpQuoted(Me.FTDescEN.Text.Trim) & "' "
                        _Str &= vbCrLf & " WHERE FNHSysModuleID=" & Val(Me.ModuleID) & " "
                        _Str &= vbCrLf & " AND FTMnuName='" & HI.UL.ULF.rpQuoted(Me.MenuID) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
                    Case Actype.Form
                        _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.HSysFormControl SET FTFormNameTH='" & HI.UL.ULF.rpQuoted(Me.FTDescTH.Text.Trim) & "' "
                        _Str &= vbCrLf & ",FTFormNameEN='" & HI.UL.ULF.rpQuoted(Me.FTDescEN.Text.Trim) & "' "
                        _Str &= vbCrLf & " WHERE  FTMnuName='" & HI.UL.ULF.rpQuoted(Me.MenuID) & "' "
                        _Str &= vbCrLf & " AND FTFormName='" & HI.UL.ULF.rpQuoted(Me.FormID) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
                    Case Actype.Object
                        _Str = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.HSysObjectControl SET FTObjectNameTH='" & HI.UL.ULF.rpQuoted(Me.FTDescTH.Text.Trim) & "' "
                        _Str &= vbCrLf & ",FTObjectNameEN='" & HI.UL.ULF.rpQuoted(Me.FTDescEN.Text.Trim) & "' "
                        _Str &= vbCrLf & " WHERE  FTMnuName='" & HI.UL.ULF.rpQuoted(Me.MenuID) & "' "
                        _Str &= vbCrLf & " AND FTFormName='" & HI.UL.ULF.rpQuoted(Me.FormID) & "' "
                        _Str &= vbCrLf & " AND FTObjectName='" & HI.UL.ULF.rpQuoted(Me.ObjectID) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
                End Select

                If _Str <> "" Then
                    Me.ProcComplete = True
                    Me.Close()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDescEN_lbl.Text)
                FTDescEN.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDescTH_lbl.Text)
            FTDescTH.Focus()
        End If
    End Sub
End Class