Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Columns

Public Class wAsset

    Private _Popup As AddEditAsset
    Private _Qry As String
    Private _Dt As DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Call InitGrid()
        ' Add any initialization after the InitializeComponent() call.
        _Popup = New AddEditAsset(Me)
        HI.TL.HandlerControl.AddHandlerObj(_Popup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Popup.Name.ToString.Trim, _Popup)
        Catch ex As Exception
        Finally
        End Try
    End Sub

#Region "Initial Grid"

    Private Sub InitGridClearSort()
        For Each c As GridColumn In ogv.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next
    End Sub

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim _sta As String = HI.Conn.SQLConn.GetField("SELECT FTStateActive FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WITH(NOLOCK) where FTStateActive='0' ", Conn.DB.DataBaseName.DB_MASTER, "")

        Dim sFieldCount As String = "FTCmpCode"
        Dim sFieldSum As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()
            '.Columns("QrderQuantity").Group()
            'If _sta = "1" Then
            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            '  End If


            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With


        '------End Add Summary Grid-------------
    End Sub

    'Private Sub InitialGridSummaryMergCell()
    '    For Each c As GridColumn In ogv.Columns
    '        Select Case c.FieldName.ToString
    '            Case "FTOrderNo", "FTWHFGCode", "FTWHFGName", "FTProdTypeCode", "FNPackPerCarton", "FTNikePOLineItem ", "FTProdTypeName", "FTStyleCode", "FTColorway", "FTSizeBreakDown", "FDShipDate", "FTPORef", "FNQuantityOrder", "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "ToTalBundle"
    '                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
    '                c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    '            Case Else
    '                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
    '        End Select
    '    Next
    'End Sub
#End Region

#Region "Property"

#End Region

#Region "Proceducre"
    Public Sub LoadData()
        Dim dt As DataTable

        Try
            _Qry = "select C.FTCmpCode,case when isdate(A.FDDateAdd)=1 then convert(varchar(10),convert(datetime,A.FDDateAdd),103) else '' end AS FDDateAdd,case when isdate(A.FDDateUsed)=1 then convert(varchar(10),convert(datetime,A.FDDateUsed),103) else '' end AS FDDateUsed"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",List.FTNameTH as AssetTypeName,A.FTAssetNameTH AS FTAssetName"
                _Qry &= vbCrLf & ",Model.FTAssetModelNameTH AS FTAssetModelName,B.FTAssetBrandNameTH AS FTAssetBrandName,AGrp.FTAssetGrpNameTH AS FTAssetGrpName"
                _Qry &= vbCrLf & ",Atype.FTAssetTypeNameTH AS FTAssetTypeName,Supl.FTSuplNameTH AS FTSuplName,E.FTEmpNameTH AS FTEmpName"
                _Qry &= vbCrLf & ",UA.FTUnitAssetNameTH AS FTUnitAssetCode"
            Else
                _Qry &= vbCrLf & ",List.FTNameEN as AssetTypeName,A.FTAssetNameEN AS FTAssetName"
                _Qry &= vbCrLf & ",Model.FTAssetModelNameEN AS FTAssetModelName,B.FTAssetBrandNameEN AS FTAssetBrandName,AGrp.FTAssetGrpNameEN AS FTAssetGrpName"
                _Qry &= vbCrLf & ",Atype.FTAssetTypeNameEN AS FTAssetTypeName,Supl.FTSuplNameEN AS FTSuplName,E.FTEmpNameEN AS FTEmpName"
                _Qry &= vbCrLf & ",UA.FTUnitAssetNameEN AS FTUnitAssetCode"

            End If
            _Qry &= vbCrLf & ",case when isdate(A.FDDateStartWarranty)=1 then convert(varchar(10),convert(datetime,A.FDDateStartWarranty),103) else '' end AS FDDateStartWarranty,case when isdate(A.FDDateEndWarranty)=1 then convert(varchar(10),convert(datetime,A.FDDateEndWarranty),103) else '' end AS FDDateEndWarranty"
            _Qry &= vbCrLf & ",case when isdate(A.FDInvoiceDate)=1 then convert(varchar(10),convert(datetime,A.FDInvoiceDate),103) else '' end AS FDInvoiceDate,case when isdate(A.FDReceiveDate)=1 then convert(varchar(10),convert(datetime,A.FDReceiveDate),103) else '' end AS FDReceiveDate"
            _Qry &= vbCrLf & ",case when isdate(A.FDPurchaseDate)=1 then convert(varchar(10),convert(datetime,A.FDPurchaseDate),103) else '' end AS FDPurchaseDate,A.FNMaxPower,U.FTUnitSectCode,Cur.FTCurCode"
            _Qry &= vbCrLf & ",A.FTPurchaseNo,A.FTPurchaseBy,A.FTInvoiceNo,A.FTAssetCode,A.FTLocationAsset"
            _Qry &= vbCrLf & ",A.FTReceiveNo,A.FTReceiveBy,A.FNMaximumStock,A.FNMinimumStock"
            _Qry &= vbCrLf & ",A.FTRemark,A.FTStateActive,A.FTStateCritical,A.FNPrice,A.FPImage,A.FTProductCode,A.FTSerialNo,A.FTRefer,A.FNHSysFixedAssetId,A.FNFixedAssetType,A.FTAssetNameTH,A.FTAssetNameEN"
            _Qry &= vbCrLf & ",Model.FTAssetModelCode,B.FTAssetBrandCode,AGrp.FTAssetGrpCode,Atype.FTAssetTypeCode,A.FNLifetimeType,U.FTUnitSectCode,E.FTEmpCode,Supl.FTSuplCode,Cur.FTCurCode,A.FNLifetime"

            _Qry &= vbCrLf & "FROM"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A with(nolock) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with(nolock) ON A.FNHSysCmpId=c.FNHSysCmpId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "(select FNListIndex,FTNameEN,FTNameTH from"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L where FTListName='FNFixedAssetType') AS List ON A.FNFixedAssetType=FNListIndex LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS Model WITH(NOLOCK) ON A.FNHSysAssetModelId=Model.FNHSysAssetModelId LEFT OUtER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WItH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetGrp AS AGrp WITH(NOLOCK) ON A.FNHSysAssetGrpId=AGrp.FNHSysAssetGrpId LeFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType AS Atype WITH(NOLOCK) ON A.FNHSysAssetTyped=Atype.FNHSysAssetTyped LEFT OUtER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON A.FNHSysUnitSectId=U.FNHSysUnitSectId LeFT OUtER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Supl WITH(NOLOCK) ON A.FNHSysSuplId=Supl.FNHSysSuplId LEFT OUtER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) ON A.FNHSysEmpID=E.FNHSysEmpID LeFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS Cur WITH(NOLOCK) ON A.FNHSysCurId=Cur.FNHSysCurId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS UA WITH(NOLOCK) ON A.FNHSysUnitAssetId=UA.FNHSysUnitAssetId"
            _Qry &= vbCrLf & "order by FDDateAdd desc"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            ogc.DataSource = dt
        Catch ex As Exception

        End Try


    End Sub


#End Region

#Region "Function"
    Private Function GetDataAddEdit() As DataTable
        Dim Qry As String = ""
        Dim _Key As String = ""
        Try

            With ogv
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                    Return Nothing
                End If
                _Key = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString.Trim
            End With
            If Not (ocmedit.Enabled) Then Return Nothing

            Qry = ""



        Catch ex As Exception
            Return Nothing
        End Try
        Return _Dt
    End Function
#End Region

#Region "Event"
    Private Sub AddClick(sender As Object, e As EventArgs) Handles ocmaddnew.Click
        Dim _ListIdx As Integer = 0
        TL.HandlerControl.ClearControl(_Popup)
        With _Popup
            .ProcComplete = False
            .ocmaddnew.Visible = True
            .ocmaddnew.Enabled = Me.ocmaddnew.Enabled
            .ocmedit.Visible = False
            .ocmedit.Enabled = Me.ocmedit.Enabled
            .ocmdelete.Visible = False
            .ocmdelete.Enabled = Me.ocmdelete.Enabled
            '.FNFixedAssetType.Properties.Items.Clear()
            'For Each _str As String In TL.CboList.SetList("FNFixedAssetType")
            '    If _ListIdx <> 1 Then
            '        .FNFixedAssetType.Properties.Items.Add(_str)
            '    End If
            '    _ListIdx += 1
            'Next
            .ShowDialog()
            If .ProcComplete Then
                Call LoadData()
                .ProcComplete = False
            End If
        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click
        Dim Qry As String = ""
        Dim _ID As String = ""
        Dim dt As DataTable
        Dim _ListIdx As Integer = 0
        Try
            TL.HandlerControl.ClearControl(_Popup)
            With _Popup
                .ProcComplete = False
                .ocmaddnew.Visible = False
                .ocmaddnew.Enabled = Me.ocmaddnew.Enabled
                .ocmedit.Visible = True
                .ocmedit.Enabled = Me.ocmedit.Enabled
                .ocmdelete.Visible = Me.ocmdelete.Visible
                .ocmdelete.Enabled = Me.ocmdelete.Enabled
                'โหลดรายละเอียดลงใน ฟิลaddedit
                With ogv
                    _ID = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString.Trim
                End With

                '.FNFixedAssetType.Properties.Items.Clear()
                'For Each _str As String In TL.CboList.SetList("FNFixedAssetType")
                '    If _ListIdx <> 1 Then
                '        .FNFixedAssetType.Properties.Items.Add(_str)
                '    End If
                '    _ListIdx += 1
                'Next
                Qry = _Qry.Substring(0, _Qry.Length - 23) & " Where FNHSysFixedAssetId=" & Val(_ID) & ""
                dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
                For Each R As DataRow In dt.Rows
                    .FNHSysCmpId.Text = R!FTCmpCode.ToString

                    'If Val(R!FNFixedAssetType.ToString) = 0 Then
                    '    .FNFixedAssetType.SelectedIndex = Val(R!FNFixedAssetType.ToString)
                    'Else
                    '    .FNFixedAssetType.SelectedIndex = Val(R!FNFixedAssetType.ToString) - 1
                    'End If
                    .FNFixedAssetType.SelectedIndex = Val(R!FNFixedAssetType.ToString)



                    If R!FTStateActive.ToString = "1" Then
                        .FTStateActive.Checked = True
                    Else
                        .FTStateActive.Checked = False
                    End If
                    If R!FTStateCritical.ToString = "1" Then
                        .FTStateCritical.Checked = True
                    Else
                        .FTStateCritical.Checked = False
                    End If
                    .FTAssetCode.Text = R!FTAssetCode.ToString
                    .FTAssetCode.Properties.Tag = Val(R!FNHSysFixedAssetId.ToString)
                    .FDDateAdd.Text = R!FDDateAdd.ToString
                    .FNHSysAssetModelId.Text = R!FTAssetModelCode.ToString
                    .FNHSysAssetBrandId.Text = R!FTAssetBrandCode.ToString
                    .FTSerialNo.Text = R!FTSerialNo.ToString
                    .FTRefer.Text = R!FTRefer.ToString
                    .FNHSysAssetGrpId.Text = R!FTAssetGrpCode.ToString
                    .FNHSysAssetTyped.Text = R!FTAssetTypeCode.ToString
                    .FDDateUsed.Text = R!FDDateUsed.ToString
                    .FNLifetime.Value = Val(R!FNLifetime.ToString)
                    .FNLifetimeType.SelectedIndex = Val(R!FNLifetimeType.ToString)
                    .FDDateStartWarranty.Text = R!FDDateStartWarranty.ToString
                    .FDDateEndWarranty.Text = R!FDDateEndWarranty.ToString
                    .FTProductCode.Text = R!FTProductCode.ToString
                    .FNMaxPower.Value = Val(R!FNMaxPower.ToString)
                    .FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                    .FNHSysEmpID.Text = R!FTEmpCode.ToString
                    .FNHSysSuplId.Text = R!FTSuplCode.ToString
                    .FTAssetNameEN.Text = R!FTAssetNameEN.ToString
                    .FTAssetNameTH.Text = R!FTAssetNameTH.ToString
                    .FTRemark.Text = R!FTRemark.ToString
                    .FTPurchaseNo.Text = R!FTPurchaseNo.ToString
                    .FTPurchaseBy.Text = R!FTPurchaseBy.ToString
                    .FDPurchaseDate.Text = R!FDPurchaseDate.ToString
                    .FTInvoiceNo.Text = R!FTInvoiceNo.ToString
                    .FDInvoiceDate.Text = R!FDInvoiceDate.ToString
                    .FTReceiveNo.Text = R!FTReceiveNo.ToString
                    .FTReceiveBy.Text = R!FTReceiveBy.ToString
                    .FDReceiveDate.Text = R!FDReceiveDate.ToString
                    .FNMaximumStock.Value = Val(R!FNMaximumStock.ToString)
                    .FNMinimumStock.Value = Val(R!FNMinimumStock.ToString)
                    .FNPrice.Value = Val(R!FNPrice.ToString)
                    .FNHSysCurId.Text = R!FTCurCode.ToString
                    .FPImage.Image = UL.ULImage.ConvertByteArrayToImmage(R!FPImage)
                    .FNHSysUnitAssetId.Text = R!FTUnitAssetCode.ToString
                    .FTLocationAsset.Text = R!FTLocationAsset.ToString
                Next
                .ShowDialog()
                If .ProcComplete Then
                    Call LoadData()
                    .ProcComplete = False
                End If
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Try
            ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception

        End Try

    End Sub
#End Region

    Private Sub wAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadData()
    End Sub

    Private Sub ogv_DoubleClick(sender As Object, e As EventArgs) Handles ogv.DoubleClick
        ocmedit.PerformClick()
    End Sub

    Private Sub ogv_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogv.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                ocmdelete.PerformClick()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        Try
            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                If e.RowHandle <> .FocusedRowHandle OrElse e.Column.AbsoluteIndex = .FocusedColumn.AbsoluteIndex Then
                    If (e.Column.OptionsColumn.ReadOnly) Then
                        e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                    Else
                        e.Appearance.BackColor = System.Drawing.Color.White
                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Dim Qry As String = ""
        Dim _ID As String = ""
        If MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, "" & ogv.GetRowCellValue(ogv.FocusedRowHandle, "FTAssetCode").ToString.Trim) Then
            With ogv
                _ID = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString.Trim
            End With
            Try
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Qry = "delete [" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset where FNHSysFixedAssetId=" & Val(_ID) & ""
                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                Else
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End Try
            Try
                With CType(ogc.DataSource, DataTable)
                    .BeginInit()
                    For Each R As DataRow In .Select("FNHSysFixedAssetId=" & Val(_ID) & "")
                        R.Delete()
                    Next
                    .EndInit()
                End With
            Catch ex As Exception

            End Try
        End If
    End Sub



End Class