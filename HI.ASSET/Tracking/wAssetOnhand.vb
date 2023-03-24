Imports System.Windows.Forms
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wAssetOnhand

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()

      
    End Sub
#Region "Initial Grid"

    Private Sub InitGrid()

        Dim sFieldSum As String = "FNQuantity"
        With ogvdetail
            .ClearGrouping()
            .ClearDocument()


            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
                End If
            Next
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With

    End Sub
#End Region

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

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region
#Region "Procedure"
    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim _Qry As String
        Dim dt As DataTable
        Try
            _Qry = "SELECT WH.FTWHAssetCode"

            _Qry &= vbCrLf & ",t.FTBarcodeNo"
            _Qry &= vbCrLf & ",isnull(Asset.FTProductCode,AP.FTProductCode) AS FTProductCode"
            _Qry &= vbCrLf & ",isnull(Asset.FTAssetCode,AP.FTAssetPartCode)AS FTAssetCode"
            _Qry &= vbCrLf & ",Md.FTAssetModelCode,"
            _Qry &= vbCrLf & "ISNULL(UNA.FTUnitAssetCode,'') AS FTUnitCode,"
            _Qry &= vbCrLf & "SUM(t.FNQuantity) AS FNQuantity,"
            _Qry &= vbCrLf & "t.FNHSysWHAssetId,"


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "isnull(Asset.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,"
                _Qry &= vbCrLf & "isnull(BRAND.FTAssetBrandNameTH,PB.FTAssetBrandNameTH) AS FTAssetBrandName,"
                _Qry &= vbCrLf & "isnull(Grp.FTAssetGrpNameTH,PG.FTAssetPartGrpNameTH) AS FTAssetGrpName,"
                _Qry &= vbCrLf & "isnull(AType.FTAssetTypeNameTH,PT.FTAssetPartTypeNameTH) AS FTAssetTypeName"
            Else
                _Qry &= vbCrLf & "isnull(Asset.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,"
                _Qry &= vbCrLf & "isnull(BRAND.FTAssetBrandNameEN,PB.FTAssetBrandNameEN) AS FTAssetBrandName,"
                _Qry &= vbCrLf & "isnull(Grp.FTAssetGrpNameEN,PG.FTAssetPartGrpNameEN) AS FTAssetGrpName,"
                _Qry &= vbCrLf & "isnull(AType.FTAssetTypeNameEN,PT.FTAssetPartTypeNameEN) AS FTAssetTypeName"
            End If

            _Qry &= vbCrLf & "FROM ("
            _Qry &= vbCrLf & "SELECT 'RCV' AS ID, RCV.FNHSysWHAssetId, BIN.FTBarcodeNo, SUM(BIN.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS RCV WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BIN WITH(NOLOCK) ON RCV.FTReceiveNo=BIN.FTDocumentNo"
            _Qry &= vbCrLf & "GROUP BY RCV.FTReceiveNo, RCV.FNHSysWHAssetId, BIN.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & "SELECT 'ISU' AS ID, ISU.FNHSysWHAssetId, BOUT.FTBarcodeNo, - SUM(BOUT.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTIssue AS ISU WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BOUT WITH(NOLOCK) ON ISU.FTIssueNo=BOUT.FTDocumentNo"
            _Qry &= vbCrLf & "GROUP BY ISU.FTIssueNo, ISU.FNHSysWHAssetId, BOUT.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & "SELECT 'WHBIN' AS ID, TRW.FNHSysWHAssetIdTo AS FNHSysWHAssetId, BIN.FTBarcodeNo, SUM(BIN.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTTransferWH AS TRW  WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BIN WITH(NOLOCK) ON TRW.FTTransferWHNo=BIN.FTDocumentNo AND TRW.FNHSysWHAssetIdTo = BIN.FNHSysWHAssetId"
            _Qry &= vbCrLf & "GROUP BY TRW.FTTransferWHNo, TRW.FNHSysWHAssetIdTo, BIN.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & "SELECT 'WHBOUT' AS ID, TRW.FNHSysWHAssetId, BOUT.FTBarcodeNo, - SUM(BOUT.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTTransferWH AS TRW  WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BOUT WITH(NOLOCK) ON TRW.FTTransferWHNo=BOUT.FTDocumentNo AND TRW.FNHSysWHAssetId = BOUT.FNHSysWHAssetId"
            _Qry &= vbCrLf & " GROUP BY TRW.FTTransferWHNo, TRW.FNHSysWHAssetId, BOUT.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & "SELECT 'ReturnToSupplier' AS ID, BOUT.FNHSysWHAssetId, BOUT.FTBarcodeNo, - SUM(BOUT.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier AS TRW WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BOUT WITH(NOLOCK) ON TRW.FTReturnSuplNo=BOUT.FTDocumentNo"
            _Qry &= vbCrLf & "GROUP BY    BOUT.FNHSysWHAssetId, BOUT.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & " SELECT 'ReturnToStock' AS ID, TRW.FNHSysWHAssetId, BIN.FTBarcodeNo, SUM(BIN.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToStock AS TRW INNER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BIN ON TRW.FTReturnStockNo = BIN.FTDocumentNo AND TRW.FNHSysWHAssetId = BIN.FNHSysWHAssetId"
            _Qry &= vbCrLf & " GROUP BY TRW.FNHSysWHAssetId, BIN.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & "SELECT 'ADJIN' AS ID, TRW.FNHSysWHAssetId , BIN.FTBarcodeNo, SUM(BIN.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust AS TRW INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BIN ON TRW.FTAdjustStockNo=BIN.FTDocumentNo AND TRW.FNHSysWHAssetId = BIN.FNHSysWHAssetId"
            _Qry &= vbCrLf & "GROUP BY TRW.FNHSysWHAssetId, BIN.FTBarcodeNo"

            _Qry &= vbCrLf & "UNION ALL"
            _Qry &= vbCrLf & "SELECT 'ADJOUT' AS ID, TRW.FNHSysWHAssetId, BOUT.FTBarcodeNo, - SUM(BOUT.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust AS TRW INNER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BOUT ON TRW.FTAdjustStockNo = BOUT.FTDocumentNo AND TRW.FNHSysWHAssetId = BOUT.FNHSysWHAssetId"
            _Qry &= vbCrLf & " GROUP BY TRW.FNHSysWHAssetId, BOUT.FTBarcodeNo"

            _Qry &= vbCrLf & ") AS t INNER JOIN"

            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS BC WITH(NOLOCK) ON t.FTBarcodeNo=BC.FTBarcodeNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseAsset AS WH WITH(NOLOCK) ON t.FNHSysWHAssetId=WH.FNHSysWHAssetId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS Asset WITH(NOLOCK) ON BC.FNHSysFixedAssetId=Asset.FNHSysFixedAssetId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS Md WITH(NOLOCK) ON Asset.FNHSysAssetModelId=Md.FNHSysAssetModelId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetGrp AS Grp WITH(NOLOCK) ON  Asset.FNHSysAssetGrpId=Grp.FNHSysAssetGrpId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS BRAND WITH(NOLOCK) ON Asset.FNHSysAssetBrandId=BRAND.FNHSysAssetBrandId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON  BC.FNHSysFixedAssetId=AP.FNHSysAssetPartId LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS UN WITH(NOLOCK) ON BC.FNHSysUnitId=UN.FNHSysUnitId  LEFT OUTER  JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS UNA WITH(NOLOCK) ON BC.FNHSysUnitId=UNA.FNHSysUnitAssetId  LEFT OUTER  JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType AS AType WITH(NOLOCK) ON Asset.FNHSysAssetTyped=AType.FNHSysAssetTyped LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS PG WITH(NOLOCK) ON AP.FNHSysAssetPartGrpId = PG.FNHSysAssetPartGrpId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartType AS PT WITH(NOLOCK) ON AP.FNHSysAssetPartTyped = PT.FNHSysAssetPartTyped LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS PB WITH(NOLOCK) ON AP.FNHSysAssetBrandId=PB.FNHSysAssetBrandId "
            _Qry &= vbCrLf & "Where"


            'ค้นหา
            'If FNHSysWHAssetId.Text <> "" Then
            '    _Qry &= vbCrLf & "WH.FTWHAssetCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHAssetId.Text) & "'"

            '    If FNHSysWHAssetIdTo.Text <> "" Then
            '        _Qry &= vbCrLf & "AND WH.FTWHAssetCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHAssetIdTo.Text) & "'"
            '    End If
            'End If

            'If FTAssetCode.Text <> "" Then
            '    If FNHSysWHAssetId.Text <> "" Then
            '        _Qry &= vbCrLf & "AND"
            '    End If
            '    _Qry &= vbCrLf & "  ASSET.FTAssetCode >='" & HI.UL.ULF.rpQuoted(Me.FTAssetCode.Text) & "'"
            '    If FNHSysFixedAssetIdTo.Text <> "" Then
            '        _Qry &= vbCrLf & "  AND ASSET.FTAssetCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysFixedAssetIdTo.Text) & "'"
            '    End If
            'End If
            If FNHSysWHAssetId.Text <> "" Then
                _Qry &= vbCrLf & "WH.FNHSysWHAssetId>=" & FNHSysWHAssetId.Properties.Tag & ""

                If FNHSysWHAssetIdTo.Text <> "" Then
                    _Qry &= vbCrLf & "AND WH.FNHSysWHAssetId<=" & FNHSysWHAssetIdTo.Properties.Tag & ""
                End If
            End If

            If FTAssetCode.Text <> "" Then
                If FNHSysWHAssetId.Text <> "" Then
                    _Qry &= vbCrLf & "AND"
                End If
                _Qry &= vbCrLf & "  BC.FNHSysFixedAssetId >=" & FTAssetCode.Properties.Tag & ""
                If FNHSysFixedAssetIdTo.Text <> "" Then
                    _Qry &= vbCrLf & "  AND BC.FNHSysFixedAssetId <=" & FNHSysFixedAssetIdTo.Properties.Tag & ""
                End If
            End If

            _Qry &= vbCrLf & "GROUP BY t.FNHSysWHAssetId,t.FTBarcodeNo,BC.FNHSysFixedAssetId,WH.FTWHAssetCode,"
            _Qry &= vbCrLf & "ASSET.FTAssetCode,AP.FTAssetPartCode,"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "Asset.FTAssetNameTH,AP.FTAssetPartNameTH,"
                _Qry &= vbCrLf & "BRAND.FTAssetBrandNameTH,"
                _Qry &= vbCrLf & "Grp.FTAssetGrpNameTH ,"
                _Qry &= vbCrLf & "AType.FTAssetTypeNameTH,PB.FTAssetBrandNameTH,PG.FTAssetPartGrpNameTH,PT.FTAssetPartTypeNameTH"
            Else
                _Qry &= vbCrLf & "Asset.FTAssetNameEN,AP.FTAssetPartNameEN,"
                _Qry &= vbCrLf & "BRAND.FTAssetBrandNameEN,"
                _Qry &= vbCrLf & "Grp.FTAssetGrpNameEN ,"
                _Qry &= vbCrLf & "AType.FTAssetTypeNameEN,PB.FTAssetBrandNameEN,PG.FTAssetPartGrpNameEN,PT.FTAssetPartTypeNameEN"
            End If
            _Qry &= vbCrLf & ",ASSET.FTProductCode,AP.FTProductCode,Md.FTAssetModelCode,UNA.FTUnitAssetCode"
            _Qry &= vbCrLf & "Order by WH.FTWHAssetCode,FTAssetGrpName,FTAssetName"


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            ogcdetail.DataSource = dt

        Catch ex As Exception
            _Spls.Close()
        End Try

        _Spls.Close()
    End Sub
    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysWHAssetId.Text <> "" And FNHSysWHAssetIdTo.Text <> "" Then
            _Pass = True
        End If
        If Me.FTAssetCode.Text <> "" And FNHSysFixedAssetIdTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            If Me.FNHSysFixedAssetIdTo.Text = "" Then Me.FNHSysFixedAssetIdTo.Focus()
            If Me.FTAssetCode.Text = "" Then Me.FTAssetCode.Focus()
            If Me.FNHSysWHAssetIdTo.Text = "" Then Me.FNHSysWHAssetIdTo.Focus()
            If Me.FNHSysWHAssetId.Text = "" Then Me.FNHSysWHAssetId.Focus()
        End If

        Return _Pass
    End Function

#End Region
#Region "General"


    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogcdetail.DataSource = Nothing
       
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick

    End Sub
    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub
#End Region

End Class