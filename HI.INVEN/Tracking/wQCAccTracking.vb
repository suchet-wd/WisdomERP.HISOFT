Public Class wQCAccTracking

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


    Private Function Verrify()
        Try
            Dim _Pass As Boolean = False

            If Me.SFDQCAccDate.Text <> "" Then
                _Pass = True
            End If
            If Me.EFDQCAccDate.Text <> "" Then
                _Pass = True
            End If
          
            If Me.FTPurchaseNo.Text <> "" Then
                _Pass = True
            End If
            If Me.FTPurchaseNoTo.Text <> "" Then
                _Pass = True
            End If

            If Me.FTReceiveNo.Text <> "" Then
                _Pass = True
            End If
            If Me.FTReceiveNoTo.Text <> "" Then
                _Pass = True
            End If

            If Me.FDReceiveDate.Text <> "" Then
                _Pass = True
            End If
            If Me.FDReceiveDateTo.Text <> "" Then
                _Pass = True
            End If

            If Me.FNHSysRawMatId.Text <> "" Then
                _Pass = True
            End If


            If Not (_Pass) Then
                HI.MG.ShowMsg.mInfo("Pls. Enter Key Search...... ", 15052800011, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                Me.SFDQCAccDate.Focus()
            End If
            Return _Pass
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Verrify() Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT  H.FTQCAccNo, H.FTQCAccBy, H.FTPurchaseNo, H.FTReceiveNo, H.FNHSysCmpId, isnull(H.FTStateSendApp,'0') AS FTStateSendApp , H.FTSendAppBy,  isnull(H.FTStateApp,'0') AS FTStateApp , H.FTAppName,   "
            _Cmd &= vbCrLf & "Case When isdate(H.FDQCAccDate)=1 Then convert(varchar(10),convert(datetime,H.FDQCAccDate),103) Else '' End FDQCAccDate, "
            _Cmd &= vbCrLf & "Case When isdate(H.FTAppDate)=1 Then convert(varchar(10),convert(datetime,H.FTAppDate),103) Else '' End FTAppDate, "
            _Cmd &= vbCrLf & "Case When isdate(H.FTSendAppDate)=1 Then convert(varchar(10),convert(datetime,H.FTSendAppDate),103) Else '' End  FTSendAppDate, "
            _Cmd &= vbCrLf & " D.FNHSysRawMatId, D.FNQCQty, D.FNQCActualQty, D.FNStateQC, D.FNDefectQty, D.FNQCDefectQty, R.FNHSysUnitId, U.FTUnitCode, C.FTCmpCode, M.FTRawMatCode,"
            _Cmd &= vbCrLf & "  L.FTRawMatColorCode, S.FTRawMatSizeCode, S.FNRawMatSizeSeq, R.FNQuantity "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", M.FTRawMatNameTH as FTRawMatName, C.FTCmpNameTH as FTCmpName , B.FTNameTH AS FTStateQC ,L.FTRawMatColorNameTH as FTRawMatColorName,  S.FTRawMatSizeNameTH AS FTRawMatSizeName , U.FTUnitNameTH as FTUnitName "
            Else
                _Cmd &= vbCrLf & " , M.FTRawMatNameEN as FTRawMatName , C.FTCmpNameEN as FTCmpName , B.FTNameEN AS FTStateQC ,L.FTRawMatColorNameEN as FTRawMatColorName , S.FTRawMatSizeNameEN as FTRawMatSizeName , U.FTUnitNameEN AS FTUnitName"
            End If
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc_Detail AS D WITH (NOLOCK) ON H.FTQCAccNo = D.FTQCAccNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON H.FNHSysCmpId = C.FNHSysCmpId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS L WITH (NOLOCK) ON M.FNHSysRawMatColorId = L.FNHSysRawMatColorId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON L.FNHSysRawMatColorId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS R WITH (NOLOCK) ON H.FTReceiveNo = R.FTReceiveNo AND D.FNHSysRawMatId = R.FNHSysRawMatId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RD WITH (NOLOCK) ON R.FTReceiveNo = RD.FTReceiveNo  LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " HITECH_MASTER.dbo.TCNMUnit AS U WITH (NOLOCK) ON R.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  (SELECT   FTListName, FNListIndex, FTNameEN, FTNameTH"
            _Cmd &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & "  WHERE   (FTListName = 'FNStateQC')) AS B ON D.FNStateQC = B.FNListIndex"
            _Cmd &= vbCrLf & "WHERE H.FTQCAccNo <> ''"
            If Me.SFDQCAccDate.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FDQCAccDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFDQCAccDate.Text) & "'"
            End If
            If Me.EFDQCAccDate.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FDQCAccDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFDQCAccDate.Text) & "'"
            End If
            If Me.FTPurchaseNo.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            End If
            If Me.FTPurchaseNoTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "'"
            End If
            If Me.FTReceiveNo.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FTReceiveNo >='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            End If
            If Me.FTReceiveNoTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FTReceiveNo <='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNoTo.Text) & "'"
            End If
            If Me.FDReceiveDate.Text <> "" Then
                _Cmd &= vbCrLf & "AND RD.FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDReceiveDate.Text) & "'"
            End If
            If Me.FDReceiveDateTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND RD.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDReceiveDateTo.Text) & "'"
            End If
            If Me.FNHSysRawMatId.Text <> "" Then
                _Cmd &= vbCrLf & "AND M.FTRawMatCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatId.Text) & "'"
            End If


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
            Me.ogcDetail.DataSource = _oDt
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvDetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvDetail.RowStyle
        Try
            With ogvDetail
                If .GetRowCellValue(e.RowHandle, "FTStateApp").ToString = "0" Then
                    e.Appearance.BackColor = Drawing.Color.LightSalmon
                    e.Appearance.BackColor2 = Drawing.Color.WhiteSmoke
                End If
                If .GetRowCellValue(e.RowHandle, "FTStateSendApp").ToString = "0" Then
                    e.Appearance.BackColor = Drawing.Color.LightGray
                    e.Appearance.BackColor2 = Drawing.Color.WhiteSmoke
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
End Class