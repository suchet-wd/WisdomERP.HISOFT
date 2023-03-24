Public Class ExportData

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
            Dim _Pass As Boolean = True


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

            _Cmd = "  Select  ST.FTStyleCode , MM.FTSeasonCode , S.FTOrderNo , S.FTSubOrderNo , S.FNSewSeq 
					,S.FTSewDescription
					
					, S.FTSewNote 
From HITECH_MERCHAN.dbo.TMERTOrder As A INNER Join
              HITECH_MASTER.dbo.TMERMStyle AS ST ON A.FNHSysStyleId = ST.FNHSysStyleId INNER Join
              HITECH_MASTER.dbo.TMERMSeason AS MM ON A.FNHSysSeasonId = MM.FNHSysSeasonId INNER Join
              HITECH_MERCHAN.dbo.TMERTOrderSub_Sew As S On A.FTOrderNo = S.FTOrderNo
Where (MM.FTSeasonCode = N'FA20') 


Or
                         (MM.FTSeasonCode = N'HO20') OR
                         (MM.FTSeasonCode = N'SP21') OR
                         (MM.FTSeasonCode = N'SU21')
ORDER BY ST.FTStyleCode, MM.FTSeasonCode, S.FNSewSeq "

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