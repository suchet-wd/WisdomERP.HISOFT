Public Class wMaterialMappingSuplRefAddItem

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub

    Enum wProcType As Integer
        [New] = 0
        [Edit] = 1
    End Enum

    Private _StateProcType As wProcType = wProcType.New
    Public Property StateProcType As wProcType
        Get
            Return _StateProcType
        End Get
        Set(value As wProcType)
            _StateProcType = value
        End Set
    End Property

    Private _SuplID As Integer = 0
    Public Property SuplID As Integer
        Get
            Return _SuplID
        End Get
        Set(value As Integer)
            _SuplID = value
        End Set
    End Property


    Private _AddMat As Boolean = False
    Public Property AddMat As Boolean
        Get
            Return _AddMat
        End Get
        Set(value As Boolean)
            _AddMat = value
        End Set
    End Property

    Private _MainObject As Object = Nothing
    Public Property MainObject As Object
        Get
            Return _MainObject
        End Get
        Set(value As Object)
            _MainObject = value
        End Set
    End Property

    Private Sub wAddItemPO_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If FNHSysRawMatId.Text = "" Then
            FNHSysRawMatId.Focus()
        End If
    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysRawMatId.Text <> "" And Me.FNHSysRawMatId.Properties.Tag.ToString <> "" Then
            If Me.FTSuplRawMatCodeRef.Text.Trim <> "" Then

                _Pass = True

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTSuplRawMatCodeRef_lbl.Text)
                FTSuplRawMatCodeRef.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysRawMatId_lbl.Text)
            FNHSysRawMatId.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click

        If ValidateData() Then
            Dim _Qry As String = ""


            _Qry = "   SELECT   TOP 1    FTSuplRawMatCodeRef "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMaterialMappingSuplRef AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Me.SuplID) & ""
            _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(Me.FNHSysRawMatId.Properties.Tag.ToString)) & ""

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
                _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMaterialMappingSuplRef "
                _Qry &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FNHSysSuplId, FNHSysRawMatId, FTSuplRawMatCodeRef"
                _Qry &= vbCrLf & ", FTSuplColorCodeRef, FTSuplSizeCodeRef, FTRemark)"
                _Qry &= vbCrLf & " SELECT'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Me.SuplID) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysRawMatId.Properties.Tag.ToString)) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSuplRawMatCodeRef.Text.Trim) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSuplColorCodeRef.Text.Trim) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSuplSizeCodeRef.Text.Trim) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            Else
                _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMaterialMappingSuplRef "
                _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ", FTSuplRawMatCodeRef='" & HI.UL.ULF.rpQuoted(FTSuplRawMatCodeRef.Text.Trim) & "'"
                _Qry &= vbCrLf & ", FTSuplColorCodeRef='" & HI.UL.ULF.rpQuoted(FTSuplColorCodeRef.Text.Trim) & "'"
                _Qry &= vbCrLf & ", FTSuplSizeCodeRef='" & HI.UL.ULF.rpQuoted(FTSuplSizeCodeRef.Text.Trim) & "'"
                _Qry &= vbCrLf & ", FTRemark='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
                _Qry &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Me.SuplID) & ""
                _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(Me.FNHSysRawMatId.Properties.Tag.ToString)) & ""

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)
            End If

            Try
                Call CallByName(Me.MainObject, "Loadata", CallType.Method, {})
            Catch ex As Exception
            End Try

            Select Case StateProcType
                Case wProcType.Edit
                    Me.Close()
                Case wProcType.New
                    HI.TL.HandlerControl.ClearControl(Me)

            End Select
        End If

    End Sub

End Class