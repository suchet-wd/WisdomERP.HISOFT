Imports System.IO
Imports System.Data.SqlClient

Public Class wTransferList
    
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

#Region "Property"
    Private _WHCode As String = ""
    Public Property WHCode As String
        Get
            Return _WHCode
        End Get
        Set(value As String)
            _WHCode = value
        End Set
    End Property

    Private _Pass As Boolean = False
    Public Property Pass As Boolean
        Get
            Return _Pass
        End Get
        Set(value As Boolean)
            _Pass = value
        End Set
    End Property

    Private _TransferFGNo As String = ""
    Public Property TransferFGNo As String
        Get
            Return _TransferFGNo
        End Get
        Set(value As String)
            _TransferFGNo = value
        End Set
    End Property


#End Region

    Private Sub wSyncData_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call LoadData()
            Me.ogrpdetail.Text = ""
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select '0' AS FTSelect , '" & _TransferFGNo & "' AS FTTransferFGNo  ,  T.FTBarCodeCarton  AS FTBarCodeCarton ,  T.FNHSysWHFGId, T.FTOrderNo,sum(T.FNQuantity) AS FNQuantity ,T.FTPackNo , T.FNCartonNo , OD.FTPORef , ST.FTStyleCode"

            _Cmd &= vbCrLf & ",(SELECT        TOP 1 STUFF" ', T.FTColorWay as FTColorway,T.FTSizeBreakDown
            _Cmd &= vbCrLf & " ((SELECT        ', ' + t2.FTColorWay"
            _Cmd &= vbCrLf & "  FROM  (SELECT FTBarCodeCarton, FTColorWay"
            _Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS Z WITH(NOLOCK) "
            _Cmd &= vbCrLf & "   GROUP BY FTBarCodeCarton, FTColorWay) t2"
            _Cmd &= vbCrLf & "  WHERE        t2.FTBarCodeCarton =  T.FTBarCodeCarton FOR XML PATH('')), 1, 2, '') AS TX) AS FTColorway"

            _Cmd &= vbCrLf & ",(SELECT        TOP 1 STUFF"
            _Cmd &= vbCrLf & " ((SELECT        ', ' + t2.FTSizeBreakDown"
            _Cmd &= vbCrLf & "  FROM  (SELECT  FTBarCodeCarton, FTSizeBreakDown"
            _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS Z WITH(NOLOCK) "
            _Cmd &= vbCrLf & "  GROUP BY FTBarCodeCarton, FTSizeBreakDown) t2"
            _Cmd &= vbCrLf & " WHERE        t2.FTBarCodeCarton =  T.FTBarCodeCarton FOR XML PATH('')), 1, 2, '') AS TX) AS FTSizeBreakDown"


            _Cmd &= vbCrLf & " From (SELECT      F.FTBarCodeCarton ,  F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, SUM(Isnull(F.FNQuantity,0)) AS FNQuantity  , F.FTPackNo , F.FNCartonNo"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F WITH (NOLOCK)   "
            _Cmd &= vbCrLf & "GROUP BY  F.FTBarCodeCarton , F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo , F.FTPackNo , F.FNCartonNo"
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT  D.FTBarCodeCarton,  T.FNHSysWHIdFGTo,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo,   SUM(FG.FNQuantity) AS FNQuantity , FG.FTPackNo , FG.FNCartonNo "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
            _Cmd &= vbCrLf & "where Isnull(T.FTStateApprove,'0') = '1' "
            _Cmd &= vbCrLf & "GROUP BY D.FTBarCodeCarton, T.FNHSysWHIdFGTo,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo , FG.FTPackNo , FG.FNCartonNo "
            _Cmd &= vbCrLf & " UNION ALL"
            _Cmd &= vbCrLf & "SELECT  D.FTBarCodeCarton,  T.FNHSysWHIdFG,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo,   -SUM(FG.FNQuantity) AS FNQuantity , FG.FTPackNo , FG.FNCartonNo "
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG AS T WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "].dbo.TFGTransferFG_Detail AS D WITH (NOLOCK) ON T.FTTransferFGNo = D.FTTransferFGNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS FG WITH (NOLOCK) ON D.FTBarCodeCarton = FG.FTBarCodeCarton"
            '_Cmd &= vbCrLf & "where Isnull(T.FTStateApprove,'0') = '1' "
            _Cmd &= vbCrLf & "GROUP BY D.FTBarCodeCarton, T.FNHSysWHIdFG,  FG.FTColorWay,  FG.FTSizeBreakDown, FG.FTOrderNo , FG.FTPackNo , FG.FNCartonNo  ) AS T "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [HITECH_MASTER].dbo.TCNMWarehouseFG AS WF WITH (NOLOCK) ON T.FNHSysWHFGId = WF.FNHSysWHFGId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD WITH (NOLOCK) ON T.FTOrderNo = OD.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS PT WITH (NOLOCK) ON OD.FNHSysProdTypeId = PT.FNHSysProdTypeId LEFT OUTER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK) ON OD.FNHSysStyleId = ST.FNHSysStyleId"
            _Cmd &= vbCrLf & " WHERE T.FNHSysWHFGId Is Not null"
            _Cmd &= vbCrLf & "And WF.FTWHFGCode ='" & HI.UL.ULF.rpQuoted(_WHCode) & "' "
            _Cmd &= vbCrLf & "group by T.FTBarCodeCarton ,  T.FNHSysWHFGId, T.FTOrderNo, T.FTPackNo , T.FNCartonNo , OD.FTPORef , ST.FTStyleCode" ' T.FTColorWay,T.FTSizeBreakDown,
            _Cmd &= vbCrLf & "Having sum(T.FNQuantity) > 0"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
            Me.ogcdetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub
   
 
    Private Sub obtselect_Click(sender As Object, e As EventArgs) Handles obtselect.Click
        Try
            _Pass = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub obtclose_Click(sender As Object, e As EventArgs) Handles obtclose.Click
        Try
            _Pass = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ockSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles ockSelectAll.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ockSelectAll.Checked Then
                _State = "1"
            End If
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
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
End Class