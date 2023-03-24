Imports System.Windows.Forms
Imports DevExpress.XtraGrid.GridControl
Imports DevExpress.XtraGrid.Columns
Public Class PurchasingTrackingForDepartment
    Sub New()
        InitializeComponent()
        Call InitialGridMergeCell()
        Call DisableFoucusedColumn()

    End Sub
#Region "Proc"

    Private Sub LoadDataHeader()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "SELECT ST.FTStyleCode"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(Conti.FTContinentNameTH,'') AS FTContiName"
        Else
            _Qry &= vbCrLf & ",ISNULL(Conti.FTContinentNameEN,'') AS FTContiName"
        End If
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(C.FTCountryNameTH,'') AS FTCountryName"
        Else
            _Qry &= vbCrLf & ",ISNULL(C.FTCountryNameEN,'') AS FTCountryName"
        End If
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(Pro.FTProvinceNameTH,'') AS ProvinceName"
        Else
            _Qry &= vbCrLf & ",ISNULL(Pro.FTProvinceNameEN,'') AS ProvinceName"
        End If
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS S WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON S.FTOrderNo=O.FTOrderNo LEFT OUtER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle AS ST WITH(NOLOCK) ON O.FNHSysStyleId=ST.FNHSysStyleId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS C WITH(NOLOCK) ON S.FNHSysCountryId=C.FNHSysCountryId LEFT OUtER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS Conti WITH(NOLOCK) ON S.FNHSysContinentId=Conti.FNHSysContinentId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS Pro WITH(NOLOCK) ON S.FNHSysProvinceId=Pro.FNHSysProvinceId"

        _Qry &= vbCrLf & "WHERE S.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows
            FTStyleCode.Text = R!FTStyleCode.ToString
            FTContiName.Text = R!FTContiName.ToString
            FTCountryName.Text = R!FTCountryName.ToString
            ProvinceName.Text = R!ProvinceName.ToString

        Next
        _dt.Dispose()
    End Sub

    Private Sub LoadDataGridDetail(_Spls As HI.TL.SplashScreen)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "]..SP_GET_POTRACK_FOR_EXPORT_DEPT_FOREIGN_TRade '" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "','" & HI.ST.Lang.Language & "','" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        ogcTrackingDept.DataSource = _dt
        _dt.Dispose()
    End Sub

#End Region

    Private Sub DisableFoucusedColumn()
        Try
            For Each C As GridColumn In ogvTrackingDept.Columns
                Select Case C.FieldName.ToString
                    Case "FTPurchaseNo"
                        C.OptionsColumn.AllowFocus = True
                    Case Else
                        C.OptionsColumn.AllowFocus = False
                End Select

            Next
        Catch ex As Exception

        End Try
        
    End Sub


    Private Sub ClearTextBoxHeader()
        FTStyleCode.Text = ""
        FTContiName.Text = ""
        FTCountryName.Text = ""
        FTContiName.Text = ""
        FTCustomerPO.Text = ""
        FTCustomerPO.Properties.Tag = ""
        FTSubOrderNo.Text = ""
        FTSubOrderNo.Properties.Tag = ""
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

        Dim _FTPurchaseNo As String = ogvTrackingDept.GetFocusedDisplayText()
        Dim _fFTPurchaseNo As String = ""
        Dim Check1 As String = ""
        Dim Check2 As String = ""
        Dim _FM As String = ""
        Dim _Dt As DataTable = ogcTrackingDept.DataSource
        Dim Stage As Boolean = True
        Dim Confirm As Boolean = True

        If Not (_Dt Is Nothing) Then
            If ochkselectall.Checked = True Then

                For Each R As DataRow In _Dt.Rows
                    Confirm = True
                    If Check1 = "" Then
                        Check1 = R!FTPurchaseNo.ToString
                        Stage = True
                    Else
                        Stage = False
                    End If
                    If Not Stage Then
                        Check2 = R!FTPurchaseNo.ToString
                        If Check1 = Check2 Then
                            Confirm = True
                        Else
                            Confirm = False
                        End If
                    End If
                    If Not Confirm Then
                        _fFTPurchaseNo = Check1
                        _FM = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_fFTPurchaseNo) & "' "
                        With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = "PurchaseOrder\"
                            .ReportName = "PurchaseOrder.rpt"
                            .Formular = _FM
                            .Preview()
                        End With
                        Check1 = Check2
                    End If
                Next
                _fFTPurchaseNo = Check1
                _FM = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_fFTPurchaseNo) & "' "
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "PurchaseOrder\"
                    .ReportName = "PurchaseOrder.rpt"
                    .Formular = _FM
                    .Preview()
                End With
            Else
                If _FTPurchaseNo <> "" Then

                    _FM = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_FTPurchaseNo) & "' "
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "PurchaseOrder\"
                        .ReportName = "PurchaseOrder.rpt"
                        .Formular = _FM
                        .Preview()
                    End With
                Else
                    Exit Sub
                End If
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If FTCustomerPO.Text <> "" Then
            Dim _Spls As New HI.TL.SplashScreen("Please Wait Loading Data....")
            Call LoadDataGridDetail(_Spls)
            _Spls.Close()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
            FTCustomerPO.Focus()
        End If
    End Sub


    Private Sub FTSubOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSubOrderNo.EditValueChanged
        Call LoadDataHeader()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Call ClearTextBoxHeader()
        ogcTrackingDept.DataSource = Nothing

    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogvTrackingDept
                If Not (.DataSource Is Nothing) And ogvTrackingDept.RowCount > 0 Then

                    With ogvTrackingDept
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitialGridMergeCell()
        For Each C As GridColumn In ogvTrackingDept.Columns
            Select Case C.FieldName.ToString
                Case "FTSelect"
                    C.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    C.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    C.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    C.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            End Select

        Next
    End Sub

    Private Sub ogvTrackingDept_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvTrackingDept.CellMerge
        Try
            With ogvTrackingDept
                If .GetRowCellValue(e.RowHandle1, "FTPurchaseNo").ToString = .GetRowCellValue(e.RowHandle2, "FTPurchaseNo").ToString Then
                    If e.Column.FieldName = "FNQuantity" Then
                        If .GetRowCellValue(e.RowHandle1, "FTInvoiceNo").ToString = .GetRowCellValue(e.RowHandle2, "FTInvoiceNo").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTSuplName").ToString = .GetRowCellValue(e.RowHandle2, "FTSuplName").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTRawMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatColorCode").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTUnitCode").ToString = .GetRowCellValue(e.RowHandle2, "FTUnitCode").ToString And
                            .GetRowCellValue(e.RowHandle1, "FTRawMatSizeCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatSizeCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    End If
                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class