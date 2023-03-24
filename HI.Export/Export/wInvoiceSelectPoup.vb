Public Class wInvoiceSelectPoup

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private _Proc As Boolean = False
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Try
            _Proc = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            _Proc = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
 

    Private Sub RepositoryFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFTSelect.EditValueChanging
        Try
            Dim _oDt As System.Data.DataTable
            With ogvdetail
                Dim _ContinentId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysContinentId").ToString)
                Dim _CountryId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysCountryId").ToString)
                Dim _ProvinceId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysProvinceId").ToString)
                '   Dim _ShipPortId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysShipPortId").ToString)
                Dim _ShipModeId As Integer = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNHSysShipModeId").ToString)
                Dim _InvoiceNo As String = .GetRowCellValue(.FocusedRowHandle, "FTInvoiceNo").ToString
                Dim _OrderNo As String = .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString
                Dim _CustomerPO As String = .GetRowCellValue(.FocusedRowHandle, "FTCustomerPO").ToString

                If e.NewValue = "1" Then
                    With ogvdetail
                        '  If .RowCount < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

                        '" and FNHSysShipPortId=" & _ShipPortId & 
                        With TryCast(ogcdetail.DataSource, System.Data.DataTable)
                            .AcceptChanges()
                            _oDt = .Select("FNHSysContinentId=" & _ContinentId & " and FNHSysCountryId=" & _CountryId & " and FNHSysProvinceId=" & _ProvinceId & "  and FNHSysShipModeId=" & _ShipModeId).CopyToDataTable
                        End With
                        With _oDt
                            .BeginInit()
                            For Each R As DataRow In .Select("FTInvoiceNo='" & _InvoiceNo & "' and FTOrderNo='" & _OrderNo & "' and FTCustomerPO='" & _CustomerPO & "'")
                                R!FTSelect = "1"
                            Next
                            .EndInit()
                        End With

                    End With
                    ogcdetail.DataSource = _oDt
                    ogcdetail.Refresh()
                Else
                    With CType(ogcdetail.DataSource, System.Data.DataTable)
                        .AcceptChanges()
                        _oDt = .Copy
                        With _oDt
                            .BeginInit()
                            For Each R As DataRow In .Select("FTInvoiceNo='" & _InvoiceNo & "' and FTOrderNo='" & _OrderNo & "' and FTCustomerPO='" & _CustomerPO & "'")
                                R!FTSelect = "0"
                            Next
                            .EndInit()
                        End With
                        If (_oDt.Select("FTSelect='1'").Count <= 0) Then
                            ogcdetail.DataSource = GetInvoice()
                            ogcdetail.Refresh()
                        End If

                    End With
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetInvoice(Optional ByVal _State As Boolean = False, Optional _Invoice As String = "") As System.Data.DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            _Cmd = "SELECT      '0' AS FTSelect,  I.FTInvoiceNo, I.FTCustomerPO, O.FTOrderNo, S.FTStyleCode, C.FNHSysContinentId, C.FTContinentCode, Y.FNHSysCountryId, Y.FTCountryCode, isnull(P.FNHSysProvinceId,0) AS FNHSysProvinceId ,   "
            _Cmd &= vbCrLf & " M.FNHSysShipModeId, M.FTShipModeCode, T.FNHSysShipPortId, T.FTShipPortCode, I.FTStateWHApp"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH (NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS C WITH (NOLOCK) ON I.FNHSysContinentId = C.FNHSysContinentId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS Y WITH (NOLOCK) ON I.FNHSysCountryId = Y.FNHSysCountryId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS P WITH (NOLOCK) ON I.FNHSysProvinceId = P.FNHSysProvinceId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipMode AS M WITH (NOLOCK) ON I.FNHSysShipModeId = M.FNHSysShipModeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS T WITH (NOLOCK) ON I.FNHSysShipPortId = M.FNHSysShipModeId"
            _Cmd &= vbCrLf & "Where ISNULL(I.FTStateWHApp, '')  = '1'"
            If (_State) Then
                _Cmd &= vbCrLf & "and  I.FTInvoiceNo not in (Select FTInvoiceNo  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExportInvoice_Detail WITH(NOLOCK) Where FTExportInvoiceNo <> '" & HI.UL.ULF.rpQuoted(_Invoice) & "' )"
            End If
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class