Public Class wRcvToAccTracking 

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

            If Me.SFTDateDoc.Text <> "" Then
                _Pass = True
            End If
            If Me.EFTDateDoc.Text <> "" Then
                _Pass = True
            End If
            If Me.SFTDateInvoice.Text <> "" Then
                _Pass = True
            End If
            If Me.EFTDateInvoice.Text <> "" Then
                _Pass = True
            End If
            If Me.SFTDateRcv.Text <> "" Then
                _Pass = True
            End If
            If Me.EFTDateRcv.Text <> "" Then
                _Pass = True
            End If
            If Me.FTPurchaseNo.Text <> "" Then
                _Pass = True
            End If
            If Me.FTPurchaseNoTo.Text <> "" Then
                _Pass = True
            End If
            If Not (_Pass) Then
                HI.MG.ShowMsg.mInfo("Pls. Enter Key Search...... ", 15052800011, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                Me.SFTDateDoc.Focus()
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
            _Cmd = "SELECT     H.FTRcvToAccNo,CASE WHEN ISDATE(H.FDRcvToAccDate) = 1 THEN CONVERT(varchar(10),convert(datetime,H.FDRcvToAccDate),103) ELSE '' END AS FDRcvToAccDate"
            _Cmd &= vbCrLf & " , H.FTRcvToAccBy, H.FTRemark, H.FNHSysCmpId, D.FTReceiveNo"
            _Cmd &= vbCrLf & ", CASE WHEN ISDATE(D.FDReceiveDate) = 1 Then CONVERT(varchar(10),CONVERT(datetime,D.FDReceiveDate),103) ELSE '' END AS FDReceiveDate"
            _Cmd &= vbCrLf & ", H.FTMailToAccountBy,case when ISDATE(H.FTMailToAccountDate) = 1 Then CONVERT(nvarchar(10),convert(datetime,H.FTMailToAccountDate),103) Else '' END AS FTMailToAccountDate"
            _Cmd &= vbCrLf & ", H.FTMailToStockBy,Case When ISDATE(H.FTMailToStockDate) = 1 Then CONVERT(nvarchar(10),convert(datetime,H.FTMailToStockDate),103) Else '' End AS FTMailToStockDate"
            _Cmd &= vbCrLf & ",case when ISDATE(D.FDInvoiceDate) = 1 Then CONVERT(varchar(10),convert(datetime,D.FDInvoiceDate),103) ELSE '' END AS FDInvoiceDate"
            _Cmd &= vbCrLf & ", D.FTInvoiceNo, D.FNHSysSuplId, D.FTPurchaseNo, P.FTSuplCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", P.FTSuplNameTH AS FTSuplName"
            Else
                _Cmd &= vbCrLf & ", P.FTSuplNameEN AS FTSuplName"
            End If
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail AS D WITH (NOLOCK) ON H.FTRcvToAccNo = D.FTRcvToAccNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS P WITH (NOLOCK) ON D.FNHSysSuplId = P.FNHSysSuplId"
            _Cmd &= vbCrLf & "WHERE D.FTReceiveNo <> ''"
            If Me.SFTDateDoc.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FDRcvToAccDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateDoc.Text) & "'"
            End If
            If Me.EFTDateDoc.Text <> "" Then
                _Cmd &= vbCrLf & "AND H.FDRcvToAccDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateDoc.Text) & "'"
            End If
            If Me.SFTDateInvoice.Text <> "" Then
                _Cmd &= vbCrLf & "AND D.FDInvoiceDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateInvoice.Text) & "'"
            End If
            If Me.EFTDateInvoice.Text <> "" Then
                _Cmd &= vbCrLf & " AND D.FDInvoiceDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateInvoice.Text) & "'"
            End If
            If Me.SFTDateRcv.Text <> "" Then
                _Cmd &= vbCrLf & "AND D.FDReceiveDate >='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateRcv.Text) & "'"
            End If
            If Me.EFTDateRcv.Text <> "" Then
                _Cmd &= vbCrLf & "AND D.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateRcv.Text) & "'"
            End If
            If Me.FTPurchaseNo.Text <> "" Then
                _Cmd &= vbCrLf & "AND D.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            End If
            If Me.FTPurchaseNoTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND D.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "'"
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
End Class