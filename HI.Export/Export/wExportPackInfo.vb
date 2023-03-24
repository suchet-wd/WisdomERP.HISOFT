
Imports FFS
Imports FFSReports
Public Class wExportPackInfo

    Private Sub wExportToCustomer_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCustId.Text = "NI"


    End Sub

    Private Function VerifyData() As Boolean
        Try
            If Me.FTStartShipment.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTStartShipment_lbl.Text)
                Me.FTStartShipment.Focus()
                Return False
            End If

            If Me.FTEndShipment.Text = "" Then

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTEndShipment_lbl.Text)
                Me.FTEndShipment.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Load Data..")
        Try

        Dim _Cmd As String = ""
            _Cmd = " Select A.FTPOref , A.FTNikePOLineItem , C.FTCusItemCodeRef  , S.FNPackPerCarton  , T.FTStyleCode , convert(varchar(10) ,convert(date , A.fdshipdate) ,103) as FDShipDate "
            _Cmd &= vbCrLf & ", M.FTCustCode , P.FTCmpCode   "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",max( T.FTStyleNameTH) as FTStyleName"
                _Cmd &= vbCrLf & ",max(P.FTCmpNameTH) as FTCmpName  "
                _Cmd &= vbCrLf & ",max(  M.FTCustNameEN ) as FTCustName "
            Else
                _Cmd &= vbCrLf & ",max( T.FTStyleNameEN) as FTStyleName"
                _Cmd &= vbCrLf & ",max(P.FTCmpNameEN) as FTCmpName  "
                _Cmd &= vbCrLf & ",max(  M.FTCustNameEN ) as FTCustName "
            End If
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination A  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub S on   A.FTOrderNo = S.FTOrderNo  and A.FTSubOrderNo = S.FTSubOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component B on A.FTOrderNo = B.FTOrderNo  and A.FTSubOrderNo = B.FTSubOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat C ON B.FNHSysMerMatId = C.FNHSysMainMatId "
            _Cmd &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder o ON A.FTOrderNo = o.FTOrderNo "
            _Cmd &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle T ON A.FNHSysStyleId = T.FNHSysStyleId "
            _Cmd &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer M ON O.FNHSysCustId = M.FNHSysCustId "
            _Cmd &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp P ON  O.FNHSysCmpId = P.FNHSysCmpId"
            _Cmd &= vbCrLf & " where C.FTMainMatCode like 'PCA%'   "
            _Cmd &= vbCrLf & " and  A.FDShipDate between '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "' and '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "' "
            If Me.FNHSysCustId.Text <> "" Then
                _Cmd &= vbCrLf & "  and o.FNHSysCustId=" & Val(Me.FNHSysCustId.Properties.Tag.ToString)
            End If
            If Me.FNHSysCmpId.Text <> "" Then
                _Cmd &= vbCrLf & "  and o.FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag.ToString)
            End If

            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "  and o.FNHSysStyleId=" & Val(Me.FNHSysStyleId.Properties.Tag.ToString)
            End If



            _Cmd &= vbCrLf & "group by A.FTPOref , A.FTNikePOLineItem , C.FTCusItemCodeRef  , S.FNPackPerCarton, M.FTCustCode , P.FTCmpCode , A.fdshipdate , T.FTStyleCode  "


            Me.ogcref.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerifyData() Then
                LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub
End Class

