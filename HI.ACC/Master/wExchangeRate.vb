Public Class wExchangeRate

    Private _AddItem As wExchangeRateAddItem
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _AddItem = New wExchangeRateAddItem
        HI.TL.HandlerControl.AddHandlerObj(_AddItem)

        Dim oSysLang As New ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItem.Name.ToString.Trim, _AddItem)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Public Sub Loadata()
        Dim _Qry As String

        _Qry = "  SELECT Convert(nvarchar(10),Convert(Datetime,M.FDDate),103) AS FDDate"
        _Qry &= vbCrLf & " , M.FNHSysCurId"
        _Qry &= vbCrLf & " , C.FTCurCode"
        _Qry &= vbCrLf & " , M.FNBuyingRate"
        _Qry &= vbCrLf & " , M.FNSellingRate"
        _Qry &= vbCrLf & " , M.FTRemark"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate AS M INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) ON M.FNHSysCurId = C.FNHSysCurId"
        _Qry &= vbCrLf & " ORDER BY M.FDDate DESC"

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.Loadata()
    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click
        Try

            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                If Not (ocmedit.Enabled) Then Exit Sub


                Dim FNHSysCurId, FDDate, FTCurCode, FTRemark As String
                Dim FNBuyingRate, FNSellingRate As Double

                FNHSysCurId = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysCurId").ToString
                FDDate = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FDDate").ToString
                FTCurCode = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTCurCode").ToString
                FTRemark = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRemark").ToString
                FNBuyingRate = Double.Parse("" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FNBuyingRate").ToString)
                FNSellingRate = Double.Parse("" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FNSellingRate").ToString)

                With _AddItem
                    HI.TL.HandlerControl.ClearControl(_AddItem)
                    .StateProcType = wExchangeRateAddItem.wProcType.Edit
                    .MainObject = Me
                    .FNHSysCurId.Text = FTCurCode
                    .SFTDate.Text = FDDate
                    .EFTDate.Text = FDDate
                    .FNBuyingRate.Value = FNBuyingRate
                    .FNSellingRate.Value = FNSellingRate
                    .FTRemark.Text = FTRemark

                    .FNHSysCurId.Properties.Buttons(0).Enabled = False
                    .FNHSysCurId.Properties.ReadOnly = True
                    .SFTDate.Properties.ReadOnly = True
                    .EFTDate.Properties.ReadOnly = True
                    .ShowDialog()
                End With
            End With


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

        Try

            With ogvdetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                If Not (ocmedit.Enabled) Then Exit Sub
                Dim FNHSysCurId, FDDate, FTCurCode As String

                FNHSysCurId = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysCurId").ToString
                FDDate = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FDDate").ToString
                FTCurCode = "" & .GetRowCellValue(ogvdetail.FocusedRowHandle, "FTCurCode").ToString

                If HI.MG.ShowMsg.mConfirmProcess(HI.MG.ShowMsg.ProcessType.mDelete, FTCurCode & "  Date : " & FDDate) = True Then

                    Dim _Qry As String
                    _Qry = " Delete From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate "
                    _Qry &= vbCrLf & "  WHERE  FDDate='" & HI.UL.ULDate.ConvertEnDB(FDDate) & "' "
                    _Qry &= vbCrLf & " AND FNHSysCurId=" & Integer.Parse(Val(FNHSysCurId)) & " "

                    If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)) Then
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    Else
                        Me.Loadata()
                    End If

                End If

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Try

            With _AddItem
                HI.TL.HandlerControl.ClearControl(_AddItem)
                .StateProcType = wExchangeRateAddItem.wProcType.New
                .MainObject = Me

                .FNHSysCurId.Properties.Buttons(0).Enabled = True
                .FNHSysCurId.Properties.ReadOnly = False
                .SFTDate.Properties.ReadOnly = False
                .EFTDate.Properties.ReadOnly = False
                .ShowDialog()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        If Me.ocmedit.Enabled Then
            ocmedit_Click(Me.ocmedit, New System.EventArgs)
        End If
    End Sub

    Private Sub wExchangeRate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Loadata()
    End Sub
End Class