Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class wScanBarcodeMergeTracking
    Private ds As DataSet
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            ds = New DataSet()

            _Cmd = "Select Distinct  B.FTBarCodePallet , isnull(B.FTCustomerPO,'') as FTCustomerPO  , isnull(B.FTPOLine,'') as FTPOLine"
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG AS B with(nolock)"
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK) ON B.FNHSysCartonId = C.FNHSysCartonId"
            _Cmd &= vbCrLf & "where isnull(B.FTBarCodePallet,'') <> ''"
            _Cmd &= vbCrLf & "and FNHSysWHFGId =" & Integer.Parse(Val(Me.FNHSysWHFGId.Properties.Tag))
            _Cmd &= vbCrLf & "and FNHSysWHLocId=" & Integer.Parse(Val(Me.FNHSysWHLocId.Properties.Tag))
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            ogcwarehouse.DataSource = _oDt
            ds.Tables.Add(_oDt)


            _Cmd = "Select  B.FTBarCodePallet , B.FTBarCodeCarton , B.FTColorWay , B.FTOrderNo , B.FTSubOrderNo , isnull(B.FTCustomerPO,'') as FTCustomerPO  , isnull(B.FTPOLine,'') as FTPOLine , C.FTCartonCode"
            _Cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG AS B with(nolock)"
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK) ON B.FNHSysCartonId = C.FNHSysCartonId"
            _Cmd &= vbCrLf & "where isnull(B.FTBarCodePallet,'') <> ''"
            _Cmd &= vbCrLf & "and FNHSysWHFGId =" & Integer.Parse(Val(Me.FNHSysWHFGId.Properties.Tag))
            _Cmd &= vbCrLf & "and FNHSysWHLocId=" & Integer.Parse(Val(Me.FNHSysWHLocId.Properties.Tag))

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD, "DataTable2")
            ds.Tables.Add(_oDt)


        Catch ex As Exception
        End Try
    End Sub

#Region "Master"
    Private Sub ogvwarehouse_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles ogvwarehouse.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    Private Sub ogvwarehouse_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles ogvwarehouse.MasterRowGetRelationName
        e.RelationName = "FTBarCodePallet"
    End Sub

    Private Sub ogvwarehouse_MasterRowEmpty(sender As Object, e As MasterRowEmptyEventArgs) Handles ogvwarehouse.MasterRowEmpty
        e.IsEmpty = False
    End Sub

    Private Sub ogvwarehouse_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles ogvwarehouse.MasterRowGetChildList
        Try
            Dim view As GridView = DirectCast(sender, GridView)
            e.ChildList = GetDetail(ogvwarehouse.GetRowCellValue(ogvwarehouse.FocusedRowHandle, "FTBarCodePallet").ToString)
        Catch ex As Exception

        End Try
    End Sub

#End Region

    Private Function GetDetail(Key As String) As DataView
        Try
            Dim dt As DataTable = ds.Tables(1)
            Dim dv As DataView = New DataView(dt)

            dv.RowFilter = "FTBarCodePallet='" & Key & "'"
            dv.AllowEdit = False
            dv.AllowNew = False
            dv.AllowDelete = False
            'For Each c As GridColumn In dt.Columns
            '    Select Case True
            '        Case "FTBarCodePallet"
            '            c.Visible = False
            '        Case "FTBarCodeCarton"
            '            c.Caption = "BarCode Carton"
            '        Case "FTCustomerPO"
            '            c.Caption = "Customer PO."
            '        Case "FTOrderNo"
            '            c.Caption = "Order No."
            '        Case "FTSubOrderNo"
            '            c.Caption = "SubOrder No."
            '        Case "FTColorWay"
            '            c.Caption = "ColorWay"
            '        Case "FTPOLine"
            '            c.Caption = "PO/Line"
            '        Case "FTCartonCode"
            '            c.Caption = "Carton Code"
            '    End Select

            'Next

            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FNHSysWHFGId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysWHFGId_lbl.Text)
                Me.FNHSysWHFGId.Focus()
                Exit Sub
            End If
            If Me.FNHSysWHLocId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysWHLocId_lbl.Text)
                Me.FNHSysWHLocId.Focus()
                Exit Sub
            End If
            Call LoadData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogcwarehouse_ViewRegistered(sender As Object, e As ViewOperationEventArgs) Handles ogcwarehouse.ViewRegistered
        Try

        Catch ex As Exception

        End Try
    End Sub
End Class