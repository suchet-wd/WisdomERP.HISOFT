Public Class wAddRequest

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub
#Region "Property"
    Private _PRNO As String = ""
    Public Property PRNO As String
        Get
            Return _PRNO
        End Get
        Set(value As String)
            _PRNO = value
        End Set
    End Property

    Private _FNSuplID As Integer = 0
    Public Property FNSuplID As Integer
        Get
            Return _FNSuplID
        End Get
        Set(value As Integer)
            _FNSuplID = value
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
#End Region

#Region "Function"
    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysRawMatId.Text <> "" And Me.FNHSysRawMatId.Properties.Tag.ToString <> "" Then
            If Me.FNHSysUnitIdPO.Text <> "" And Me.FNHSysUnitIdPO.Properties.Tag.ToString <> "" Then
                If (Me.FTOrderNo.Text <> "" And Me.FTOrderNo.Properties.Tag.ToString <> "") Or FTOrderNo.Visible = False Then
                    If FNPOQuantity.Value > 0 Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNQuantity_lbl.Text)
                        FNPOQuantity.Focus()
                    End If
                Else
                    If FTOrderNo.Visible Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTOrderNo_lbl.Text)
                        FTOrderNo.Focus()
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysUnitId_lbl.Text)
                FNHSysUnitIdPO.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysRawMatId_lbl.Text)
            FNHSysRawMatId.Focus()
        End If

        Return _Pass
    End Function

#End Region


    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Sub wAddRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If ValidateData() Then
            Me.AddMat = True
            Me.Close()
        End If
    End Sub

    Private Sub FNHSysRawMatId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysRawMatId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.DynamicButtonedit_ValueChanged(AddressOf FNHSysRawMatId_EditValueChanged), New Object() {sender, e})
            Else
                Dim _Qry As String
                Dim _dt As DataTable

                _Qry = "SELECT TOP 1 A.FTPRPurchaseNo, A.FNHSysRawMatId,B.FTUnitCode "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS A WITH(NOLOCK) "
                _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS B WITH(NOLOCK) ON A.FNHSysUnitId = B.FNHSysUnitId "
                _Qry &= vbCrLf & " WHERE A.FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.PRNO) & "'"
                _Qry &= vbCrLf & " AND A.FNHSysRawMatId=" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & ""

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                If _dt.Rows.Count > 0 Then
                    For Each R As DataRow In _dt.Rows
                        FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                        Exit For
                    Next
                    _dt.Dispose()
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class