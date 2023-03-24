Public Class wExchangeRateAddItem

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

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysCurId.Text <> "" And Me.FNHSysCurId.Properties.Tag.ToString <> "" Then
            If Me.SFTDate.Text <> "" Then
                If EFTDate.Text <> "" Then

                    Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(Me.SFTDate.Text)
                    Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(Me.EFTDate.Text)
                    If _EDate >= _SDate Then
                        If FNBuyingRate.Value > 0 Then
                            If FNSellingRate.Value > 0 Then
                                _Pass = True
                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNSellingRate_lbl.Text)
                                FNSellingRate.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNBuyingRate_lbl.Text)
                            FNBuyingRate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, EFTDate_lbl.Text)
                        EFTDate.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, EFTDate_lbl.Text)
                    EFTDate.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, SFTDate_lbl.Text)
                SFTDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCurId_lbl.Text)
            FNHSysCurId.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click

        If ValidateData() Then
            Dim _Qry As String = ""
            Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(Me.SFTDate.Text)
            Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(Me.EFTDate.Text)

            While _SDate <= _EDate
                _Qry = "   SELECT   TOP 1    FDDate "
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FNHSysCurId=" & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ""
                _Qry &= vbCrLf & " AND FDDate='" & _SDate & "'"

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "" Then
                    _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate "
                    _Qry &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FDDate, FNHSysCurId, FNBuyingRate, FNSellingRate, FTRemark )"
                    _Qry &= vbCrLf & " SELECT'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",'" & _SDate & "'"
                    _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ""
                    _Qry &= vbCrLf & ", " & FNBuyingRate.Value & ""
                    _Qry &= vbCrLf & ", " & FNSellingRate.Value & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                Else
                    _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate "
                    _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", FNBuyingRate=" & FNBuyingRate.Value & ""
                    _Qry &= vbCrLf & ", FNSellingRate=" & FNSellingRate.Value & ""
                    _Qry &= vbCrLf & ", FTRemark='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"
                    _Qry &= vbCrLf & " WHERE FNHSysCurId=" & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ""
                    _Qry &= vbCrLf & " AND FDDate='" & _SDate & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)
                End If

                _SDate = HI.UL.ULDate.AddDay(_SDate, 1)
            End While
            

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