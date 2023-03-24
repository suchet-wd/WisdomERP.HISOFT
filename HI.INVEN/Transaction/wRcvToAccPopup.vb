Public Class wRcvToAccPopup


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Property"
    Public _StateSave As Boolean = False
    Private Property StateSave As Boolean
        Get
            Return _StateSave
        End Get
        Set(value As Boolean)
            _StateSave = value
        End Set
    End Property

    Public _oDtSelect As DataTable
    Private Property oDtSelect As DataTable
        Get
            Return _oDtSelect
        End Get
        Set(value As DataTable)
            _oDtSelect = value
        End Set
    End Property

    Public _FNHSysCmpId As Integer
    Private Property FNHSysCmpId As Integer
        Get
            Return _FNHSysCmpId
        End Get
        Set(value As Integer)
            _FNHSysCmpId = value
        End Set
    End Property
#End Region

    Private Sub LoadData(Optional StateSelect As Boolean = False)

        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT   FTReceiveNo, CASE WHEN Isdate(FDReceiveDate) = 1 Then convert(nvarchar(10),convert(datetime,FDReceiveDate),103) Else '' END AS FDReceiveDate, FTReceiveBy , FTPurchaseNo , '' AS FTPackNo,1 AS FNDocState"
            If StateSelect Then
                _Cmd &= vbCrLf & ",'1' AS FTSelect "
            Else
                _Cmd &= vbCrLf & ",'0' AS FTSelect "
            End If

            _Cmd &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WITH(NOLOCK) "
            _Cmd &= vbCrLf & " Where  FTReceiveNo not in (Select FTReceiveNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail WITH(NOLOCK) )"
            _Cmd &= vbCrLf & " and FNHSysCmpId = " & Integer.Parse(FNHSysCmpId)

            _Cmd &= vbCrLf & "UNION ALL "


            _Cmd &= vbCrLf & " SELECT  RCV.FTReceiveNo, CASE WHEN Isdate(RCV.FDReceiveDate) = 1 Then convert(nvarchar(10),convert(datetime,RCV.FDReceiveDate),103) Else '' END AS FDReceiveDate, RCV.FTReceiveBy , RCV.FTPurchaseNo , '' AS FTPackNo,2 AS FNDocState"
            If StateSelect Then
                _Cmd &= vbCrLf & ",'1' AS FTSelect "
            Else
                _Cmd &= vbCrLf & ",'0' AS FTSelect "
            End If

            _Cmd &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPReceive as RCV WITH(NOLOCK) INNER JOIN "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase AS PO WITH(NOLOCK)   ON RCV.FTPurchaseNo = PO.FTPurchaseNo "
            _Cmd &= vbCrLf & " Where RCV.FTReceiveNo not in (Select FTReceiveNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail WITH(NOLOCK) )"
            _Cmd &= vbCrLf & " and RCV.FNHSysCmpId = " & Integer.Parse(FNHSysCmpId)

            _Cmd &= vbCrLf & "UNION ALL "


            _Cmd &= vbCrLf & "SELECT     FTPurchaseNo,  CASE WHEN Isdate(FDPurchaseDate) = 1 Then convert(nvarchar(10),convert(datetime,FDPurchaseDate),103) Else '' END AS FDPurchaseDate,  FTPurchaseBy,FTPurchaseNo , '' AS FTPackNo,3 AS FNDocState"

            If StateSelect Then
                _Cmd &= vbCrLf & ",'1' AS FTSelect "
            Else
                _Cmd &= vbCrLf & ",'0' AS FTSelect "
            End If
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService   WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "WHERE Isnull(FTInvoiceNo,'') <> ''"
            _Cmd &= vbCrLf & " and  FTPurchaseNo not in (Select FTReceiveNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENRcvToAcc_Detail WITH(NOLOCK) )"
            _Cmd &= vbCrLf & " and FNHSysCmpId = " & Integer.Parse(FNHSysCmpId)
            _Cmd &= vbCrLf & " Order by FTReceiveNo asc"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
            ogclistDoc.DataSource = _oDt.Copy


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmSelet_Click(sender As Object, e As EventArgs) Handles ocmSelect.Click
        Try
            Dim _oDt As DataTable = CType(ogclistDoc.DataSource, DataTable)
            oDtSelect = _oDt.Select("FTSelect='1'").CopyToDataTable
            StateSave = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Try
            StateSave = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wRcvToAccPopup_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Call LoadData()
            'Me.oSelectAll.Checked = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.oSelectAll.Checked Then
                _State = "1"
            End If

            With ogclistDoc
                If Not (.DataSource Is Nothing) And ogvlistDoc.RowCount > 0 Then

                    With ogvlistDoc
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

            'If oSelectAll.Checked = True Then
            '    Call LoadData(True)
            'Else
            '    Call LoadData(False)
            'End If
        Catch ex As Exception

        End Try
    End Sub
End Class