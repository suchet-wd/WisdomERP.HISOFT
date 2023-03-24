Imports System.Windows.Forms

Public Class wStockCardAsset
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub LoadData()
        Dim Qry As String = ""
        Dim dt As DataTable

        Qry = "select isnull(A.RcvQty,0)+isnull(A.RetStQty,0)+isnull(A.AjINQty,0)+isnull(A.TranINQty,0) AS QtyIN"
        Qry &= vbCrLf & ",+isnull(A.AjOUTQty,0)+isnull(A.IsueQty,0)+isnull(A.TranOUTQty,0)+isnull(A.RetSuplQty,0) AS QtyOUT"
        Qry &= vbCrLf & ",A.FTDocNoIN,A.FTDocOUT AS FTDocNoOUT"
        Qry &= vbCrLf & ",W.FTWHAssetCode,w.FNHSysWHAssetId,ISNULL(Ass.FTAssetCode,AP.FTAssetPartCode) AS FTAssetCode,isnull(Br.FTAssetBrandCode,PB.FTAssetBrandCode) AS FTAssetBrandCode,M.FTAssetModelCode"
        Qry &= vbCrLf & ",case when isdate(A.FDDate) = 1 then CONVERT(varchar(10),convert(datetime,A.FDDate),103) else '' end AS FDDate"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",ISNULL(Ass.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,ISNULL(Br.FTAssetBrandNameTH,PB.FTAssetBrandNameTH) AS FTAssetBrandName,M.FTAssetModelNameTH AS FTAssetModelName"
        Else
            Qry &= vbCrLf & ",ISNULL(Ass.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,ISNULL(Br.FTAssetBrandNameTH,PB.FTAssetBrandNameEN) AS FTAssetBrandName,M.FTAssetModelNameEN AS FTAssetModelName"
        End If

        '"--Rcv IN" 
        Qry &= vbCrLf & "FROM (select (BI.FNQuantity) AS RcvQty,0 AS RetStQty,0 AS AjINQty,0 AS TranINQty,0 AS AjOUTQty,0 AS IsueQty,0 AS TranOUTQty,0 AS RetSuplQty,R.FTReceiveNo AS FTDocNoIN,'' AS FTDocOUT,R.FDReceiveDate AS FDDate,R.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BI WITH(NOLOCK) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BI.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS R WItH(NOLOCK) ON BI.FTDocumentNo=R.FTReceiveNo"
        Qry &= vbCrLf & "where BI.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"
        '" --RetStock In" 
        Qry &= vbCrLf & "Select 0 As RcvQty,(BI.FNQuantity) As RetStQty,0 As AjINQty,0 As TranINQty,0 As AjOUTQty,0 As IsueQty,0 As TranOUTQty,0 As RetSuplQty,Ret.FTReturnStockNo As FTDocNoIN,'' AS FTDocOUT,Ret.FDReturnStockDate AS FDDate,Ret.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BI WITH(NOLOCK) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BI.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToStock AS Ret WItH(NOLOCK) ON BI.FTDocumentNo=Ret.FTReturnStockNo "
        Qry &= vbCrLf & "where BI.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"
        '"--Adjust IN" 
        Qry &= vbCrLf & "select 0 AS RcvQty,0 AS RetStQty,(BI.FNQuantity) AS AjINQty,0 AS TranINQty,0 AS AjOUTQty,0 AS IsueQty,0 AS TranOUTQty,0 AS RetSuplQty,AJ.FTAdjustStockNo AS FTDocNoIN,'' AS FTDocOUT,AJ.FDAdjustStockDate AS FDDate,AJ.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BI WIth(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BI.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust AS AJ WItH(NOLOCK) ON BI.FTDocumentNo=AJ.FTAdjustStockNo "
        Qry &= vbCrLf & "where BI.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"

        '"--Tranfer IN" 
        Qry &= vbCrLf & "select 0 AS RcvQty,0 AS RetStQty,0 AS AjINQty,(BI.FNQuantity) AS TranINQty,0 AS AjOUTQty,0 AS IsueQty,0 AS TranOUTQty,0 AS RetSuplQty,TF.FTTransferWHNo AS FTDocNoIN,'' AS FTDocOUT,TF.FDTransferWHDate AS FDDate,TF.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BI WITH(NOLOCK) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BI.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTTransferWH AS TF WITH(NOLOCK) ON BI.FTDocumentNo=TF.FTTransferWHNo"
        Qry &= vbCrLf & "where BI.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"

        '"--Adjust OUT" 
        Qry &= vbCrLf & "select 0 AS RcvQty,0 AS RetStQty,0 AS AjINQty,0 AS TranINQty,(BO.FNQuantity) AS AjOUTQty,0 AS IsueQty,0 AS TranOUTQty,0 AS RetSuplQty,'' AS FTDocNoIN,AJ.FTAdjustStockNo AS FTDocOUT,AJ.FDAdjustStockDate AS FDDate,AJ.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WIth(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BO.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust AS AJ WItH(NOLOCK) ON BO.FTDocumentNo=AJ.FTAdjustStockNo "
        Qry &= vbCrLf & "where BO.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"
        Qry &= vbCrLf & ""
        '"--Issue OUT" 
        Qry &= vbCrLf & "select 0 AS RcvQty,0 AS RetStQty,0 AS AjINQty,0 AS TranINQty,0 AS AjOUTQty,(BO.FNQuantity) AS IsueQty,0 AS TranOUTQty,0 AS RetSuplQty,'' AS FTDocNoIN,ISe.FTIssueNo AS FTDocOUT,ISe.FDIssueDate AS FDDate,ISe.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH(NOLOCK) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BO.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTIssue AS ISe WItH(NOLOCK) ON BO.FTDocumentNo=Ise.FTIssueNo"
        Qry &= vbCrLf & "where BO.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"
        '"--Tranfer OUT" 
        Qry &= vbCrLf & "select 0 AS RcvQty,0 AS RetStQty,0 AS AjINQty,0 AS TranINQty,0 AS AjOUTQty,0 AS IsueQty,(BO.FNQuantity) AS TranOUTQty,0 AS RetSuplQty,'' AS FTDocNoIN,TF.FTTransferWHNo AS FTDocOUT,TF.FDTransferWHDate AS FDDate,TF.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH(NOLOCK) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BO.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTTransferWH AS TF WITH(NOLOCK) ON BO.FTDocumentNo=TF.FTTransferWHNo"
        Qry &= vbCrLf & "where BO.FTBarcodeNo <>''"

        Qry &= vbCrLf & "UNION"
        '"--RetSupl OUT"
        Qry &= vbCrLf & "select 0 AS RcvQty,0 AS RetStQty,0 AS AjINQty,0 AS TranINQty,0 AS AjOUTQty,0 AS IsueQty,0 AS TranOUTQty,(BO.FNQuantity) AS RetSuplQty,'' AS FTDocNoIN,RetSupl.FTReturnSuplNo AS FTDocOUT,RetSupl.FDReturnSuplDate AS FDDate,B.FNHSysWHAssetId,B.FNHSysFixedAssetId from"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH(NOLOCK) inner join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WItH(NOLOCK) ON BO.FTBarcodeNo=B.FTBarcodeNo Left Outer Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier AS RetSupl WITH(NOLOCK) ON BO.FTDocumentNo=RetSupl.FTReturnSuplNo"
        Qry &= vbCrLf & "where BO.FTBarcodeNo <>''"
        Qry &= vbCrLf & ") AS A LEFT OUTER JOIN"

        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS Ass WItH(NOLOCK) ON A.FNHSysFixedAssetId=ASS.FNHSysFixedAssetId"
        Qry &= vbCrLf & "LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS Br WITH(NOLOCK) ON Ass.FNHSysAssetBrandId=Br.FNHSysAssetBrandId"
        Qry &= vbCrLf & "LEFT OUTER JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WItH(NOLOCK) ON Ass.FNHSysAssetModelId=M.FNHSysAssetModelId"
        Qry &= vbCrLf & "LEFT OUTER JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseAsset AS W WITH(NOLOCK) ON A.FNHSysWHAssetId=W.FNHSysWHAssetId"
        Qry &= vbCrLf & "LEFT OUTER JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON A.FNHSysFixedAssetId=AP.FNHSysAssetPartId  LEFT OUTER  JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS UNA WITH(NOLOCK) ON ASS.FNHSysUnitAssetId=UNA.FNHSysUnitAssetId  LEFT OUTER  JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS UNP WITH(NOLOCK) ON AP.FNHSysUnitAssetId=UNP.FNHSysUnitAssetId  LEFT OUTER  JOIN "
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType AS AType WITH(NOLOCK) ON AP.FNHSysAssetPartTyped=AType.FNHSysAssetTyped LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS PG WITH(NOLOCK) ON AP.FNHSysAssetPartGrpId = PG.FNHSysAssetPartGrpId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartType AS PT WITH(NOLOCK) ON AP.FNHSysAssetPartTyped = PT.FNHSysAssetPartTyped LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS PB WITH(NOLOCK) ON AP.FNHSysAssetBrandId=PB.FNHSysAssetBrandId "
        Qry &= vbCrLf & "WHERE A.FNHSysWHAssetId <>''"
        If Me.FNHSysWHAssetId.Text <> "" Then
            Qry &= vbCrLf & "AND A.FNHSysWHAssetId>=" & Me.FNHSysWHAssetId.Properties.Tag & ""
        End If

        If Me.FNHSysWHAssetIdTo.Text <> "" Then
            Qry &= vbCrLf & "and A.FNHSysWHAssetId<=" & Me.FNHSysWHAssetIdTo.Properties.Tag & ""
        End If
        If Me.FTAssetCode.Text <> "" Then
            Qry &= vbCrLf & "and A.FNHSysFixedAssetId>=" & Me.FTAssetCode.Properties.Tag & ""
        End If
        If Me.FNHSysFixedAssetIdTo.Text <> "" Then
            Qry &= vbCrLf & "and  A.FNHSysFixedAssetId<=" & Me.FNHSysFixedAssetIdTo.Properties.Tag & ""
        End If
        If Me.FDDocNo.Text <> "" Then
            Qry &= vbCrLf & "and A.FDDate>='" & UL.ULDate.ConvertEnDB(Me.FDDocNo.Text) & "'"
        End If
        If Me.FDDocNoTo.Text <> "" Then
            Qry &= vbCrLf & "and A.FDDate<='" & UL.ULDate.ConvertEnDB(Me.FDDocNoTo.Text) & "'"
        End If




        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
        ogcdetail.DataSource = dt

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        TL.HandlerControl.ClearControl(Me)
        Me.FDDocNoTo.Text = ""
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysWHAssetId.Text <> "" And FNHSysWHAssetIdTo.Text <> "" Then
            _Pass = True
        End If
        If Me.FTAssetCode.Text <> "" And FNHSysFixedAssetIdTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If
        If Me.FDDocNo.Text <> "" And Me.FDDocNoTo.Text <> "" Then
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
End Class