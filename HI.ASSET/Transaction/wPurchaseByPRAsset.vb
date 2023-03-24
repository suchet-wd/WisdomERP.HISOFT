Public Class wPurchaseByPRAsset
    Private _Popup As wPurchaserByPRPopupAsset
    Private _PopupS As wPurchaserByPRPopupServiceAsset
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        LoadPopup()
        LoadPopupS()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub LoadPopup()
        _Popup = New wPurchaserByPRPopupAsset
        TL.HandlerControl.AddHandlerObj(_Popup)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Popup.Name.ToString.Trim, _Popup)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadPopupS()
        _PopupS = New wPurchaserByPRPopupServiceAsset
        TL.HandlerControl.AddHandlerObj(_PopupS)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PopupS.Name.ToString.Trim, _PopupS)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadDetail()
        Dim Qry As String = ""

        Qry = "Select '0' AS FTSelect,RD.FTPRPurchaseNo,RD.FNQuantity,RD.FTPurchaseRefNo,RD.FTRemark"
        Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,R.FDPRRequestDate),103) AS FDPRRequestDate"
        Qry &= vbCrLf & ",isnull(Ass.FTAssetCode,AP.FTAssetPartCode) as FTAssetCode,RD.FNHSysFixedAssetId,RD.FNHSysUnitId"
        Qry &= vbCrLf & ",RD.FNNetAmt,R.FTPRPurchaseBy,RD.FNPrice,isnull(RD.FTDescription,'') AS FTDescription"
        Qry &= vbCrLf & ",(SELECT FTNameTH FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WHERE FTListName = 'FNPRState' AND FNListIndex = R.FNPRState) AS FNPRState"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",isnull(Ass.FTAssetNameTH,AP.FTAssetPartNameTH) As FTAssetName,B.FTAssetBrandNameTH As FTAssetBrandName,M.FTAssetModelNameTH As FTAssetModelName,U.FTUnitAssetNameTH As FTUnitCode,L.FTNameTH AS FNFixedAssetType"
        Else
            Qry &= vbCrLf & ",isnull(Ass.FTAssetNameEN,AP.FTAssetPartNameEN) As FTAssetName,B.FTAssetBrandNameEN As FTAssetBrandName,M.FTAssetModelNameEN As FTAssetModelName,U.FTUnitAssetNameEN As FTUnitCode,L.FTNameEN AS FNFixedAssetType"
        End If
        Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail As RD With(NOLOCK) INNER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As R With(NOLOCK) On RD.FTPRPurchaseNo=R.FTPRPurchaseNo LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As Ass With(NOLOCK) On RD.FNHSysFixedAssetId=Ass.FNHSysFixedAssetId and RD.FNFixedAssetType=Ass.FNFixedAssetType LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand As B With(NOLOCK) On Ass.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel As M With(NOLOCK) On Ass.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUtER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart As AP  With(NOLOCK) On RD.FNHSysFixedAssetId =AP.FNHSysAssetPartId and RD.FNFixedAssetType='1' LEFT OUtER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset As U With(NOLOCK) On RD.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUtER Join"
        Qry &= vbCrLf & "(SELECT L.FTNameTH ,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName ='FNFixedAssetType')AS L   ON RD.FNFixedAssetType=L.FNListIndex"
        Qry &= vbCrLf & "where R.FTStateApp='1'  and R.FTStateManagerApp='1'   and RD.FTPurchaseRefNo Is null Or RD.FTPurchaseRefNo=''"
        Qry &= vbCrLf & "Order by RD.FNSeq"
        ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)




    End Sub

    Private Sub ocmopenPO_Click(sender As Object, e As EventArgs) Handles ocmopenPO.Click
        Dim dt As DataTable : Dim dtPupoup As DataTable : Dim _AssetId As String = "" : Dim _PRNo As String = ""
        Dim Qry As String = "" : Dim _NewPO As String = ""
        dt = CType(ogcDetail.DataSource, DataTable)
        If dt.Select("FTSelect='1'").Length > 0 Then
            For Each R As DataRow In dt.Select("FTSelect='1'")
                If _AssetId <> "" Then
                    _AssetId &= ","
                End If
                _AssetId &= R!FNHSysFixedAssetId.ToString
                If _PRNo <> "" Then
                    _PRNo &= ","
                End If
                _PRNo &= "'" & R!FTPRPurchaseNo.ToString & "'"
            Next

                   Qry = "Select ROW_NUMBER() over (ORDER BY RD.FNSeq) AS cNewFNSeq ,RD.FNSeq AS FNSeq,RD.FNQuantity,RD.FTPRPurchaseNo,RD.FTPurchaseRefNo,RD.FTRemark"
            Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,R.FDPRRequestDate),103) AS FDPRRequestDate"
            Qry &= vbCrLf & ",isnull(Ass.FTAssetCode,AP.FTAssetPartCode) as FTAssetCode,U.FTUnitAssetCode AS FTUnitCode,RD.FNHSysFixedAssetId,RD.FNHSysUnitId,S.FTSuplCode,ISNULL(ASS.FTProductCode,AP.FTProductCode)AS FTProductCode"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                Qry &= vbCrLf & ",isnull(Ass.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,B.FTAssetBrandNameTH AS FTAssetBrandName,M.FTAssetModelNameTH AS FTAssetModelName,L.FTNameTH as FNFixedAssetType,L.FTNameTH as cFNFixedAssetType"
            Else
                Qry &= vbCrLf & ",isnull(Ass.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,B.FTAssetBrandNameEN AS FTAssetBrandName,M.FTAssetModelNameEN AS FTAssetModelName,L.FTNameEN as FNFixedAssetType,L.FTNameEN as cFNFixedAssetType"
            End If

            Qry &= vbCrLf & ",case when RD.FNPrice =0 then 0.00 else RD.FNPrice end AS FNPrice,case when RD.FNDisPer =0 then 0.00 else RD.FNDisPer end AS FNDisPer,case when RD.FNDisAmt =0 then 0.00 else RD.FNDisAmt end AS FNDisAmt"
            Qry &= vbCrLf & ",case when RD.FNNetAmt =0 then 0.00 else RD.FNNetAmt end AS FNAmount,case when RD.FNNetAmt=0 then 0.00 else RD.FNNetAmt end AS FNGrandNetAmt"

            Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail AS RD WItH(NOLOCK) INNER Join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS R WITH(NOLOCK) ON RD.FTPRPurchaseNo=R.FTPRPurchaseNo LEFT OUTER Join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS Ass WITH(NOLOCK) ON RD.FNHSysFixedAssetId=Ass.FNHSysFixedAssetId and RD.FNFixedAssetType=Ass.FNFixedAssetType  LEFT OUTER Join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WITH(NOLOCK) ON Ass.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER Join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH(NOLOCK) ON Ass.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUtER Join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP  WITH(NOLOCK) ON RD.FNHSysFixedAssetId =AP.FNHSysAssetPartId and RD.FNFixedAssetType='1' LEFT OUtER Join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON RD.FNHSysUnitId=U.FNHSysUnitAssetId left outer join"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier as S WITH(NOLOCK) ON R.FNHSysSuplId = S.FNHSysSuplId left outer join"
            Qry &= vbCrLf & "(select FTNameTH,FTNameEN,FNListIndex from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  WITH(NOLOCK) where FTListName ='FNFixedAssetType') as L ON  RD.FNFixedAssetType =L.FNListIndex"
            Qry &= vbCrLf & "where  RD.FNHSysFixedAssetId in (" & _AssetId & ") and RD.FTPRPurchaseNo in (" & _PRNo & ") and (RD.FTPurchaseRefNo is null or RD.FTPurchaseRefNo='')" 'R.FTStateApp='1'  and
            Qry &= vbCrLf & "order by  RD.FNSeq"
            dtPupoup = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
            Dim _PRState As Integer = 0
            Qry = "SELECT FNPRState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request WHERE FTPRPurchaseNo in (" & _PRNo & ") "
            _PRState = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
            Dim FNHSysSuplId As String = ""
            Qry = "SELECT FTSuplCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS A WITH(NOLOCK)"
            Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS B WITH(NOLOCK) ON A.FNHSysSuplId = B.FNHSysSuplId WHERE A.FTPRPurchaseNo in (" & _PRNo & ")"
            FNHSysSuplId = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)


            'With _Popup
            '    .State = False
            '    TL.HandlerControl.ClearControl(_Popup)
            '    .oDtRef = dt.Select("FTSelect='1'").CopyToDataTable
            '    .ogcsum.DataSource = dtPupoup
            '    .PRState = _PRState
            '    If _PRState = 1 Then
            '        .FNTaxPer.Value = 3
            '    End If
            '    .FNHSysSuplId.Text = FNHSysSuplId
            '    .ShowDialog()
            '    If .State Then
            '        _NewPO = .FTPurchaseNo.Text
            '    End If
            'End With
            If _PRState = 0 Then
                With _Popup
                    .State = False
                    TL.HandlerControl.ClearControl(_Popup)
                    .oDtRef = dt.Select("FTSelect='1'").CopyToDataTable
                    .ogcsum.DataSource = dtPupoup
                    .PRState = _PRState
                    LoadPopup()
                    For Each R As DataRow In dtPupoup.Rows
                        .FNHSysSuplId.Text = R!FTSuplCode.ToString
                        '.FNFixedAssetType.Text = R!FNFixedAssetType.ToString
                    Next
                    .ShowDialog()
                    If .State Then
                        _NewPO = .FTPurchaseNo.Text

                    End If
                End With

            Else
                With _PopupS
                    .State = False
                    TL.HandlerControl.ClearControl(_PopupS)
                    .oDtRef = dt.Select("FTSelect='1'").CopyToDataTable
                    .ogcdetail.DataSource = dtPupoup
                    .PRState = _PRState
                    LoadPopupS()
                    For Each R As DataRow In dtPupoup.Rows
                        .FNHSysSuplId.Text = R!FTSuplCode.ToString
                        .FNFixedAssetType.Text = R!FNFixedAssetType.ToString
                    Next
                    .ShowDialog()
                    If .State Then
                        _NewPO = .FTPurchaseNo.Text

                    End If
                End With
            End If
        End If
        Me.ockSelectAll.Checked = False
        Call LoadDetail()
    End Sub

    Private Sub ockSelectAll_EditValueChanged(sender As Object, e As EventArgs) Handles ockSelectAll.EditValueChanged
        Dim _State As String = "0"
        Try
            With ogvDetail
                If .RowCount > 0 Then
                    If Me.ockSelectAll.Checked = True Then
                        _State = "1"
                        For i As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(i, "FTSelect", _State)
                        Next
                    Else
                        For i As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(i, "FTSelect", _State)
                        Next
                    End If
                    CType(ogcDetail.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadDetail()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Dim dt As DataTable
        dt = CType(ogcDetail.DataSource, DataTable)
        Dim QRY As String = ""
        QRY = "SELECT FTPRPurchaseNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request"
        Dim Num As Boolean = True
        If dt.Select("FTSelect='1'").Length > 0 Then
            For Each R As DataRow In dt.Select("FTSelect='1'")
                If Num = True Then
                    QRY &= vbCrLf & " WHERE "
                Else
                    QRY &= vbCrLf & " OR "
                End If
                QRY &= vbCrLf & "FTPRPurchaseNo = '" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'"
                Num = False
            Next
        End If
        dt = HI.Conn.SQLConn.GetDataTable(QRY, Conn.DB.DataBaseName.DB_FIXED)

        For Each R As DataRow In dt.Rows
            If R!FTPRPurchaseNo.ToString <> "" Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "PurchaseAsset\"
                    .ReportName = "PurchaseRequestAsset.rpt"
                    '.AddParameter("Draft", "DRAFT")
                    .Formular = "{TFIXEDTPurchase_Request.FTPRPurchaseNo}='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'"
                    .Preview()
                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, R!FTPRPurchaseNo.ToString)

            End If
        Next



    End Sub

    'Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
    '    Dim _Str As String = "" : Dim _FNSeq As Integer = 0
    '    Dim dt As DataTable : Dim dtPupoup As DataTable : Dim _AssetId As String = "" : Dim _PRNo As String = ""
    '    Dim Qry As String = "" : Dim _NewPO As String = ""
    '    dt = CType(ogcDetail.DataSource, DataTable)
    '    _Str = "select top 1 FTPRPurchaseNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseByPRAsset_Detail"
    '    If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED) <> "" Then
    '        _Str = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseByPRAsset"
    '        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_FIXED)
    '        _Str = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseByPRAsset_Detail"
    '        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_FIXED)
    '    End If


    '    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
    '    HI.Conn.SQLConn.SqlConnectionOpen()
    '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
    '    Dim _FTPur As String = "0"

    '    _Str = "INSERT into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseByPRAsset (FTInsUser, FDInsDate, FTInsTime"
    '    _Str &= vbCrLf & " ,FTPurchaseNo,FTPurchaseBy,FNHSysCmpId )"
    '    _Str &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
    '    _Str &= vbCrLf & ",'" & _FTPur & "','" & ST.UserInfo.UserName & "'," & Val(HI.ST.SysInfo.CmpID.ToString) & ""

    '    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '    Else
    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '    End If
    '    If dt.Select("FTSelect='1'").Length > 0 Then
    '        For Each R As DataRow In dt.Select("FTSelect='1'")
    '            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
    '            HI.Conn.SQLConn.SqlConnectionOpen()
    '            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '            _FNSeq += 1
    '            _Str = "INSERT into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseByPRAsset_Detail (FTInsUser, FDInsDate, FTInsTime"
    '            _Str &= vbCrLf & " ,FTPurchaseNo,FTPRPurchaseNo, FDPRRequestDate, FTPRPurchaseBy, FNSeq, FNHSysFixedAssetId, FNQuantity, FNNetAmt,FTRemark)"
    '            _Str &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
    '            _Str &= vbCrLf & ",'" & _FTPur & "','" & R!FTPRPurchaseNo.ToString & "','" & R!FDPRRequestDate & "','" & R!FTPRPurchaseBy & "'"
    '            _Str &= vbCrLf & "," & _FNSeq & "," & R!FNHSysFixedAssetId & "," & R!FNQuantity & "," & R!FNNetAmt & ",'" & R!FTRemark & "'"

    '            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Else
    '                HI.Conn.SQLConn.Tran.Commit()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '            End If
    '        Next
    '    End If



    '    With New HI.RP.Report
    '        .FormTitle = Me.Text
    '        .ReportFolderName = "PurchaseAsset\"
    '        .ReportName = "PurchaseByPRAsset.rpt"
    '        '.AddParameter("Draft", "DRAFT")
    '        .Formular = "{TFIXEDTPurchaseByPRAsset.FTPurchaseNo} = '" & _FTPur & "'"
    '        .Preview()
    '    End With

    'End Sub
End Class