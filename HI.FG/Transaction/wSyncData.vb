Imports System.IO
Imports System.Data.SqlClient

Public Class wSyncData
    Private _SetServerPopUp As wPOSPopUpSetServerName
    Private _SetPathFile As wFormSetPathFile
    Private _SetFileBackup As wFormSet_FileName
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _SetServerPopUp = New wPOSPopUpSetServerName
        HI.TL.HandlerControl.AddHandlerObj(_SetServerPopUp)
        _SetPathFile = New wFormSetPathFile
        HI.TL.HandlerControl.AddHandlerObj(_SetPathFile)
        _SetFileBackup = New wFormSet_FileName
        HI.TL.HandlerControl.AddHandlerObj(_SetFileBackup)


        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _SetServerPopUp.Name.ToString.Trim, _SetServerPopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _SetServerPopUp.Name.ToString.Trim, _SetServerPopUp)
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _SetPathFile.Name.ToString.Trim, _SetPathFile)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _SetPathFile.Name.ToString.Trim, _SetPathFile)
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _SetFileBackup.Name.ToString.Trim, _SetFileBackup)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _SetFileBackup.Name.ToString.Trim, _SetFileBackup)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wSyncData_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogvdetail.OptionsView.ShowAutoFilterRow = False
            Me.ProgressBarControl1.Text = 0
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Function checkServername() As Boolean
        Try
            If HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_ACCOUNT).ToUpper Like "SVR_HISOFT*" Then
                Return False
            End If
            If HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_MERCHAN).ToUpper Like "*SVR*" Then
                Return False
            End If

            If HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_MERCHAN).ToUpper Like "SVR_HISOFT*" Then
                Return False
            End If

            If HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_PROD).ToUpper Like "SVR_HISOFT*" Then
                Return False
            End If
            Return True
        Catch ex As Exception
        End Try
    End Function
    Private Sub ocmsyncdatafromserver_Click(sender As Object, e As EventArgs) Handles ocmsyncdatafromserver.Click
        Try
            If Not checkServername() Then
                HI.MG.ShowMsg.mInfo("เซิฟเวอร์ Login  ไม่สามารถถ่ายโอนนข้อมูลได้ กรุณาใช้งาน exe Ofline  Thk for you .....", 1702271551, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Error)
                Exit Sub
            End If

            HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)
            Dim _ServerName As String = ""

            Try

                If HI.ST.UserInfo.UserName.ToUpper = "ADMIN" Then

                    With _SetServerPopUp
                        .Text = .Text & ocmsyncdatafromserver.Text
                        .ShowDialog()

                        If (.Proc) Then
                            _ServerName = .FTComputerName.Text

                            If _ServerName = HI.Conn.DB.SerVerName Then
                                HI.MG.ShowMsg.mInfo("ชื่อเซิฟเวอร์ตรงกัน ไม่สามารถถ่ายโอนนข้อมูลได้.....", 1508261338, Me.Text)
                                Exit Sub
                            End If

                        Else
                            Exit Sub
                        End If

                    End With

                Else
                    _ServerName = "SVR_HISOFT"
                End If

                If Not My.Computer.Network.Ping(_ServerName.ToString) Then
                    HI.MG.ShowMsg.mInfo("connecting Server Problems..", 1508031711, Me.Text)
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
            Dim _Spls As New HI.TL.SplashScreen("Syncing Data... Please Wait... ")
            If PSyncData(_ServerName, True, _Spls) Then
                ' HI.MG.ShowMsg.mInfo("synced data succussfuly...", 1508031702, Me.Text)
            Else
                '  HI.MG.ShowMsg.mInfo("synced data failed...", 1508031703, Me.Text)
            End If
            _Spls.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Function PSyncData(_ServerName As String, State As Boolean, _Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FTDescription", GetType(String))
                .Columns.Add("FTStatus", GetType(Boolean))
            End With
            With _dt
                If (State) Then
                    .Rows.Add("TPRODTBarcodeScanFG", False)
                    .Rows.Add("TPRODTOrder_CustBarcode", False)
                    .Rows.Add("TACCTStylePrice", False)
                    .Rows.Add("TMERMProductType", False)
                    .Rows.Add("TMERTOrder", False)
                    .Rows.Add("Restore Database Finish Goods", False)
                Else
                    .Rows.Add("TACCTSale", False)
                    .Rows.Add("TACCTSale_Detail", False)
                End If
            End With

            If HI.Conn.DB.UIDName Is Nothing Or HI.Conn.DB.UIDName = "" Then
                HI.Conn.DB.UIDName = "sa"
            End If

            If HI.Conn.DB.PWDName Is Nothing Or HI.Conn.DB.PWDName = "" Then
                HI.Conn.DB.PWDName = "5k,mew,"
            End If


            Me.ogcdetail.DataSource = _dt
            Me.ProgressBarControl1.Text = 0
            If (State) Then
                ogvdetail.SetRowCellValue(0, "FTStatus", _TPRODTBarcodeScanFG(_ServerName))
                'CType(ogcdetail.DataSource, DataTable).AcceptChanges()
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(20)

                ogvdetail.SetRowCellValue(1, "FTStatus", _TPRODTOrder_CustBarcode(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(30)

                ogvdetail.SetRowCellValue(2, "FTStatus", _TACCTStylePrice(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(40)

                ogvdetail.SetRowCellValue(3, "FTStatus", _TMERMProductType(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(50)

                ogvdetail.SetRowCellValue(4, "FTStatus", _TMERTOrder(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(60)

                ogvdetail.SetRowCellValue(0, "FTStatus", _FromTACCTSale(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(70)

                ogvdetail.SetRowCellValue(1, "FTStatus", _FromTACCTSale_Detail(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(80)

                ogvdetail.SetRowCellValue(5, "FTStatus", ReStoreDBFG(_ServerName, _Spls))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(100)
            Else
                ogvdetail.SetRowCellValue(0, "FTStatus", _TACCTSale(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(50)

                ogvdetail.SetRowCellValue(1, "FTStatus", _TACCTSale_Detail(_ServerName))
                ogcdetail.RefreshDataSource()
                _Spls.UpdateProgress(100)
            End If
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Function

#Region "SyncDataFromServer"
    Private Function _TPRODTBarcodeScanFG(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity)"
            _Cmd &= vbCrLf & "SELECT  F.FTInsUser, F.FDInsDate, F.FTInsTime, F.FTUpdUser, F.FDUpdDate, F.FTUpdTime, F.FTBarCodeCarton, F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, F.FNQuantity"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS G ON F.FTOrderNo = G.FTOrderNo AND F.FTSizeBreakDown = G.FTSizeBreakDown AND F.FTColorWay = G.FTColorWay AND "
            _Cmd &= vbCrLf & "F.FNHSysWHFGId = G.FNHSysWHFGId And F.FTBarCodeCarton = G.FTBarCodeCarton"
            _Cmd &= vbCrLf & " WHERE G.FTBarCodeCarton Is null"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _TPRODTOrder_CustBarcode(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo)"
            _Cmd &= vbCrLf & "SELECT     B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime, B.FTOrderNo, B.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, B.FTCustBarcodeNo"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).HITECH_PRODUCTION.dbo.TPRODTOrder_CustBarcode AS B LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS C ON B.FTOrderNo = C.FTOrderNo AND B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway = C.FTColorway AND "
            _Cmd &= vbCrLf & "B.FTSizeBreakDown = C.FTSizeBreakDown And B.FTCustBarcodeNo = C.FTCustBarcodeNo"
            _Cmd &= vbCrLf & "Where C.FTCustBarcodeNo Is null"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _TACCTStylePrice(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""

            _Cmd = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice "
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNHSysCmpId, FNPrice, FTColorway, FTSizeBreakDown)"
            _Cmd &= vbCrLf & "SELECT     P.FTInsUser, P.FDInsDate, P.FTInsTime, P.FTUpdUser, P.FDUpdDate, P.FTUpdTime, P.FNHSysStyleId, P.FNHSysCmpId, P.FNPrice, P.FTColorway, P.FTSizeBreakDown"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS P LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS S ON P.FTSizeBreakDown = S.FTSizeBreakDown AND P.FTColorway = S.FTColorway AND P.FNHSysCmpId = S.FNHSysCmpId AND P.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & "WHERE S.FNHSysStyleId Is null"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _TMERMProductType(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysProdTypeId, FTProdTypeCode, FTProdTypeNameTH, FTProdTypeNameEN, FTRemark, FTStateActive)"
            _Cmd &= vbCrLf & "SELECT     T.FTInsUser, T.FDInsDate, T.FTInsTime, T.FTUpdUser, T.FDUpdDate, T.FTUpdTime, T.FNHSysProdTypeId, T.FTProdTypeCode, T.FTProdTypeNameTH, T.FTProdTypeNameEN, T.FTRemark, "
            _Cmd &= vbCrLf & "T.FTStateActive  "
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS T LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS P ON T.FNHSysProdTypeId = P.FNHSysProdTypeId"
            _Cmd &= vbCrLf & " WHERE P.FNHSysProdTypeId Is null"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _TMERTOrder(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

            _Cmd = " INSERT INTO      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, "
            _Cmd &= vbCrLf & "FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTStateOrderApp, FTAppBy, FDAppDate, FTAppTime, FNJobState, FTStateBy, "
            _Cmd &= vbCrLf & "FDStateDate, FTStateTime, FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTCancelAppBy, FDCancelAppDate, FDCancelAppTime, FTCancelAppRemark, "
            _Cmd &= vbCrLf & " FTPOTradingCo, FTPOItem, FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, "
            _Cmd &= vbCrLf & " FDImportDate, FTImportTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, "
            _Cmd &= vbCrLf & " FTUserChangeOrderImage1, FDDateChangeOrderImage2, FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, "
            _Cmd &= vbCrLf & " FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, FTUserChangeOrderImage4, FTOrderNoRef, FTStateSendDirectorApp, FTStateSendDirectorBy, "
            _Cmd &= vbCrLf & " FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, "
            _Cmd &= vbCrLf & " FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, "
            _Cmd &= vbCrLf & " FDStateFactoryRejectDate, FTStateFactoryRejectTime, FTChangeCmpBy, FDChangeCmpDate, FTChangeCmpTime)"

            _Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime, O.FTOrderNo, O.FDOrderDate, O.FTOrderBy, O.FNOrderType, O.FNHSysCmpId, O.FNHSysCmpRunId, "
            _Cmd &= vbCrLf & " O.FNHSysStyleId, O.FTPORef, O.FNHSysCustId, O.FNHSysAgencyId, O.FNHSysProdTypeId, O.FNHSysBuyerId, O.FTMainMaterial, O.FTCombination, O.FTRemark, O.FTStateOrderApp, O.FTAppBy,"
            _Cmd &= vbCrLf & " O.FDAppDate, O.FTAppTime, O.FNJobState, O.FTStateBy, O.FDStateDate, O.FTStateTime, O.FTImage1, O.FTImage2, O.FTImage3, O.FTImage4, O.FNHSysBrandId, O.FNHSysBuyId,"
            _Cmd &= vbCrLf & " O.FTCancelAppBy, O.FDCancelAppDate, O.FDCancelAppTime, O.FTCancelAppRemark, O.FTPOTradingCo, O.FTPOItem, O.FTPOCreateDate, O.FNHSysMerTeamId, O.FNHSysPlantId,"
            _Cmd &= vbCrLf & " O.FNHSysBuyGrpId, O.FNHSysMainCategoryId, O.FNHSysVenderPramId, O.FTOrderCreateStatus, O.FTImportUser, O.FDImportDate, O.FTImportTime, O.FPOrderImage1, O.FPOrderImage2,"
            _Cmd &= vbCrLf & " O.FPOrderImage3, O.FPOrderImage4, O.FNHSysSeasonId, O.FDDateChangeOrderImage1, O.FTTimeChangeOrderImage1, O.FTUserChangeOrderImage1, O.FDDateChangeOrderImage2,"
            _Cmd &= vbCrLf & "O.FTTimeChangeOrderImage2, O.FTUserChangeOrderImage2, O.FDDateChangeOrderImage3, O.FTTimeChangeOrderImage3, O.FTUserChangeOrderImage3, O.FDDateChangeOrderImage4,"
            _Cmd &= vbCrLf & "O.FTTimeChangeOrderImage4, O.FTUserChangeOrderImage4, O.FTOrderNoRef,   O.FTStateSendDirectorApp, O.FTStateSendDirectorBy, O.FDStateSendDirectorDate,"
            _Cmd &= vbCrLf & " O.FTStateSendDirectorTime, O.FTStateDirectorApp, O.FTStateDirectorAppBy, O.FDStateDirectorAppDate, O.FTStateDirectorAppTime, O.FTStateDirectorReject, O.FTStateDirectorRejectBy,"
            _Cmd &= vbCrLf & " O.FDStateDirectorRejectDate, O.FTStateDirectorRejectTime, O.FTStateFactoryApp, O.FTStateFactoryAppBy, O.FDStateFactoryAppDate, O.FTStateFactoryAppTime, O.FTStateFactoryReject,"
            _Cmd &= vbCrLf & " O.FTStateFactoryRejectBy, O.FDStateFactoryRejectDate, O.FTStateFactoryRejectTime, O.FTChangeCmpBy, O.FDChangeCmpDate, O.FTChangeCmpTime"
            _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ). [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS T ON O.FTOrderNo = T.FTOrderNo"
            _Cmd &= vbCrLf & " where T.FTOrderNo Is null"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _FromTACCTSale(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FDInvoiceDate, FTInvoiceBy, FTRemark, FNInvAmt, FNDisCountPer, FNDisCountAmt, FNInvNetAmt, FNVatPer, "
            _Cmd &= vbCrLf & " FNVatAmt, FNSurcharge, FNInvGrandAmt, FTInvGrandAmtTH, FTInvGrandAmtEN, FNHSysCmpId, FTCustomerName, FTCustAddr)"
            _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FDInvoiceDate, S.FTInvoiceBy, S.FTRemark, S.FNInvAmt, S.FNDisCountPer, S.FNDisCountAmt, "
            _Cmd &= vbCrLf & "S.FNInvNetAmt, S.FNVatPer, S.FNVatAmt, S.FNSurcharge, S.FNInvGrandAmt, S.FTInvGrandAmtTH, S.FTInvGrandAmtEN, S.FNHSysCmpId, S.FTCustomerName, S.FTCustAddr"
            _Cmd &= vbCrLf & "FROM  OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS T ON S.FTInvoiceNo = T.FTInvoiceNo"
            _Cmd &= vbCrLf & " WHERE(T.FTInvoiceNo Is NULL)"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _FromTACCTSale_Detail(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            'Detail
            _Cmd = " Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail"
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail"
            _Cmd &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FTBarcodeCustNo, FNQuantity, FNPrice, FNHSysWHFGId, FTOrderNo, FTColorway, FTSizeBreakDown ,FTDocumentRefNo)"
            _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FTBarcodeCustNo, S.FNQuantity, S.FNPrice, S.FNHSysWHFGId, S.FTOrderNo, S.FTColorway, S.FTSizeBreakDown , S.FTDocumentRefNo"
            _Cmd &= vbCrLf & "FROM   OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS S LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS T ON S.FTBarcodeCustNo = T.FTBarcodeCustNo AND "
            _Cmd &= vbCrLf & " S.FTInvoiceNo = T.FTInvoiceNo"
            _Cmd &= vbCrLf & "WHERE     (T.FTInvoiceNo IS NULL) AND (T.FTBarcodeCustNo IS NULL)"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ReStoreDBFG(_ServerName As String, _Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _PathServer As String = "" : Dim _PathCopy As String = ""
            If _ServerName.ToUpper = "SVR_HISOFT" Then
                _PathServer = "E:\WISDOM SYSTEM\DATABE_BACK_FG\"
                _PathCopy = "\\" & _ServerName & "\WISDOM SYSTEM\DATABE_BACK_FG\"
            Else
                _PathServer = "E:\HI SOFT SYSTEM\DATABE_BACK_FG\"
                _PathCopy = "\\" & _ServerName & "\HI SOFT SYSTEM\DATABE_BACK_FG\"
            End If
            Dim _Cmd As String = ""
            Dim _Path As String = "D:\WISDOMDBase\"
            Dim _PathLocal As String = "D:\WISDOMDBase\"
            If Not (Directory.Exists(_Path)) Then
                My.Computer.FileSystem.CreateDirectory(_Path)
            Else
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(_Path, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.bak")
                    My.Computer.FileSystem.DeleteFile(foundFile, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently)
                Next
            End If
            Try
                If (Directory.Exists(_PathCopy)) Then
                    For Each foundFile As String In My.Computer.FileSystem.GetFiles(_PathCopy, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.bak")
                        My.Computer.FileSystem.DeleteFile(foundFile, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently)
                    Next
                End If
            Catch ex As Exception
            End Try
            _Cmd = "Exec master.dbo.SP_BACKUP_FG '" & _PathServer & "'"
            If connectSqlServer(_ServerName, HI.Conn.DB.UIDName, HI.Conn.DB.PWDName, _Cmd) Then
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(_PathLocal, Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.bak")
                    My.Computer.FileSystem.DeleteFile(foundFile, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently)
                Next
                Dim dir As New DirectoryInfo(_PathCopy)
                _Spls.Close()
                With _SetPathFile
                    .DefalPath = _PathCopy.ToString
                    .DefalPathTo = _Path.ToString
                    .ShowDialog()
                    If (.State) Then
                        _PathCopy = .FTPathName.Text
                        _Path = .FTPathNameTo.Text
                    Else
                        Return False
                    End If
                End With
                For Each foundFile As FileInfo In dir.GetFiles("*.bak", SearchOption.AllDirectories)
                    With _SetFileBackup
                        .DefalPath = _PathCopy & "\" & foundFile.Name.ToString
                        .ShowDialog()
                        If Not (.State) Then
                            Return False
                        End If
                        Try
                            'System.IO.File.Copy(_PathCopy & "\" & foundFile.Name.ToString, _Path & "\" & foundFile.Name.ToString)
                            System.IO.File.Copy(.FTPathName.Text, _Path & "\" & foundFile.Name.ToString)
                        Catch ex As Exception
                            'My.Computer.FileSystem.CopyFile(_PathCopy & "\" & foundFile.Name.ToString, _Path & "\" & foundFile.Name.ToString)
                            My.Computer.FileSystem.CopyFile(.FTPathName.Text, _Path & "\" & foundFile.Name.ToString)
                        Finally
                            '  MsgBox(_PathCopy & "\" & foundFile.Name.ToString & " TO " & _Path & "\" & foundFile.Name.ToString & " succsess", MsgBoxStyle.OkOnly)
                        End Try
                    End With
                Next foundFile
            End If
            _Spls = New HI.TL.SplashScreen()
            _Spls.UpdateInformation("Syncing Data... Please Wait... ")
            Dim rdir As New DirectoryInfo(_Path)
            If rdir.GetFiles().Length > 0 Then
                _Cmd = "Exec master.dbo.SP_RESTORE_FG '" & _Path & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
            Else
                HI.MG.ShowMsg.mInfo("can't restore database , is not found file  .bak !!", 1510101354, Me.Text)
            End If
            _Spls.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function connectSqlServer(_ServerName As String, _Username As String, _Pass As String, sql As String) As Boolean
        Try
            Dim connetionString As String
            Dim connection As SqlConnection
            Dim command As SqlCommand
            connetionString = "Data Source=" & _ServerName & ";Initial Catalog=master;User ID=" & _Username & ";Password=" & _Pass & ""
            connection = New SqlConnection(connetionString)
            Try
                connection.Open()
                command = New SqlCommand(sql, connection)
                command.CommandTimeout = 0
                command.ExecuteNonQuery()
                command.Dispose()
                connection.Close()
                Return True
            Catch ex As Exception
                command.Dispose()
                connection.Close()
                Return False
            End Try
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region


    'Private Function SyncDataFromServer(_ServerName As String) As Boolean
    '    Dim _Spls As New HI.TL.SplashScreen("being synced sysnc Data.... Please Wait")
    '    Try
    '        Dim _Cmd As String = ""
    '        HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)

    '        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity)"
    '        _Cmd &= vbCrLf & "SELECT  F.FTInsUser, F.FDInsDate, F.FTInsTime, F.FTUpdUser, F.FDUpdDate, F.FTUpdTime, F.FTBarCodeCarton, F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, F.FNQuantity"
    '        _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS F LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS G ON F.FTOrderNo = G.FTOrderNo AND F.FTSizeBreakDown = G.FTSizeBreakDown AND F.FTColorWay = G.FTColorWay AND "
    '        _Cmd &= vbCrLf & "F.FNHSysWHFGId = G.FNHSysWHFGId And F.FTBarCodeCarton = G.FTBarCodeCarton"
    '        _Cmd &= vbCrLf & " WHERE G.FTBarCodeCarton Is null"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

    '        _Cmd = "INSERT INTO HITECH_PRODUCTION.dbo.TPRODTOrder_CustBarcode "
    '        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo)"
    '        _Cmd &= vbCrLf & "SELECT     B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime, B.FTOrderNo, B.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, B.FTCustBarcodeNo"
    '        _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).HITECH_PRODUCTION.dbo.TPRODTOrder_CustBarcode AS B LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS C ON B.FTOrderNo = C.FTOrderNo AND B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway = C.FTColorway AND "
    '        _Cmd &= vbCrLf & "B.FTSizeBreakDown = C.FTSizeBreakDown And B.FTCustBarcodeNo = C.FTCustBarcodeNo"
    '        _Cmd &= vbCrLf & "Where C.FTCustBarcodeNo Is null"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

    '        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice"
    '        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNHSysCmpId, FNPrice, FTColorway, FTSizeBreakDown)"
    '        _Cmd &= vbCrLf & "SELECT     P.FTInsUser, P.FDInsDate, P.FTInsTime, P.FTUpdUser, P.FDUpdDate, P.FTUpdTime, P.FNHSysStyleId, P.FNHSysCmpId, P.FNPrice, P.FTColorway, P.FTSizeBreakDown"
    '        _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS P LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice AS S ON P.FTSizeBreakDown = S.FTSizeBreakDown AND P.FTColorway = S.FTColorway AND P.FNHSysCmpId = S.FNHSysCmpId AND P.FNHSysStyleId = S.FNHSysStyleId"
    '        _Cmd &= vbCrLf & "WHERE S.FNHSysStyleId Is null"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

    '        _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType"
    '        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysProdTypeId, FTProdTypeCode, FTProdTypeNameTH, FTProdTypeNameEN, FTRemark, FTStateActive)"
    '        _Cmd &= vbCrLf & "SELECT     T.FTInsUser, T.FDInsDate, T.FTInsTime, T.FTUpdUser, T.FDUpdDate, T.FTUpdTime, T.FNHSysProdTypeId, T.FTProdTypeCode, T.FTProdTypeNameTH, T.FTProdTypeNameEN, T.FTRemark, "
    '        _Cmd &= vbCrLf & "T.FTStateActive  "
    '        _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ).[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS T LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS P ON T.FNHSysProdTypeId = P.FNHSysProdTypeId"
    '        _Cmd &= vbCrLf & " WHERE P.FNHSysProdTypeId Is null"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

    '        _Cmd = " INSERT INTO      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FDOrderDate, FTOrderBy, FNOrderType, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, FTPORef, "
    '        _Cmd &= vbCrLf & "FNHSysCustId, FNHSysAgencyId, FNHSysProdTypeId, FNHSysBuyerId, FTMainMaterial, FTCombination, FTRemark, FTStateOrderApp, FTAppBy, FDAppDate, FTAppTime, FNJobState, FTStateBy, "
    '        _Cmd &= vbCrLf & "FDStateDate, FTStateTime, FTImage1, FTImage2, FTImage3, FTImage4, FNHSysBrandId, FNHSysBuyId, FTCancelAppBy, FDCancelAppDate, FDCancelAppTime, FTCancelAppRemark, "
    '        _Cmd &= vbCrLf & " FTPOTradingCo, FTPOItem, FTPOCreateDate, FNHSysMerTeamId, FNHSysPlantId, FNHSysBuyGrpId, FNHSysMainCategoryId, FNHSysVenderPramId, FTOrderCreateStatus, FTImportUser, "
    '        _Cmd &= vbCrLf & " FDImportDate, FTImportTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FNHSysSeasonId, FDDateChangeOrderImage1, FTTimeChangeOrderImage1, "
    '        _Cmd &= vbCrLf & " FTUserChangeOrderImage1, FDDateChangeOrderImage2, FTTimeChangeOrderImage2, FTUserChangeOrderImage2, FDDateChangeOrderImage3, FTTimeChangeOrderImage3, "
    '        _Cmd &= vbCrLf & " FTUserChangeOrderImage3, FDDateChangeOrderImage4, FTTimeChangeOrderImage4, FTUserChangeOrderImage4, FTOrderNoRef, FTStateSendDirectorApp, FTStateSendDirectorBy, "
    '        _Cmd &= vbCrLf & " FDStateSendDirectorDate, FTStateSendDirectorTime, FTStateDirectorApp, FTStateDirectorAppBy, FDStateDirectorAppDate, FTStateDirectorAppTime, FTStateDirectorReject, FTStateDirectorRejectBy, "
    '        _Cmd &= vbCrLf & " FDStateDirectorRejectDate, FTStateDirectorRejectTime, FTStateFactoryApp, FTStateFactoryAppBy, FDStateFactoryAppDate, FTStateFactoryAppTime, FTStateFactoryReject, FTStateFactoryRejectBy, "
    '        _Cmd &= vbCrLf & " FDStateFactoryRejectDate, FTStateFactoryRejectTime, FTChangeCmpBy, FDChangeCmpDate, FTChangeCmpTime)"

    '        _Cmd &= vbCrLf & "SELECT     O.FTInsUser, O.FDInsDate, O.FTInsTime, O.FTUpdUser, O.FDUpdDate, O.FTUpdTime, O.FTOrderNo, O.FDOrderDate, O.FTOrderBy, O.FNOrderType, O.FNHSysCmpId, O.FNHSysCmpRunId, "
    '        _Cmd &= vbCrLf & " O.FNHSysStyleId, O.FTPORef, O.FNHSysCustId, O.FNHSysAgencyId, O.FNHSysProdTypeId, O.FNHSysBuyerId, O.FTMainMaterial, O.FTCombination, O.FTRemark, O.FTStateOrderApp, O.FTAppBy,"
    '        _Cmd &= vbCrLf & " O.FDAppDate, O.FTAppTime, O.FNJobState, O.FTStateBy, O.FDStateDate, O.FTStateTime, O.FTImage1, O.FTImage2, O.FTImage3, O.FTImage4, O.FNHSysBrandId, O.FNHSysBuyId,"
    '        _Cmd &= vbCrLf & " O.FTCancelAppBy, O.FDCancelAppDate, O.FDCancelAppTime, O.FTCancelAppRemark, O.FTPOTradingCo, O.FTPOItem, O.FTPOCreateDate, O.FNHSysMerTeamId, O.FNHSysPlantId,"
    '        _Cmd &= vbCrLf & " O.FNHSysBuyGrpId, O.FNHSysMainCategoryId, O.FNHSysVenderPramId, O.FTOrderCreateStatus, O.FTImportUser, O.FDImportDate, O.FTImportTime, O.FPOrderImage1, O.FPOrderImage2,"
    '        _Cmd &= vbCrLf & " O.FPOrderImage3, O.FPOrderImage4, O.FNHSysSeasonId, O.FDDateChangeOrderImage1, O.FTTimeChangeOrderImage1, O.FTUserChangeOrderImage1, O.FDDateChangeOrderImage2,"
    '        _Cmd &= vbCrLf & "O.FTTimeChangeOrderImage2, O.FTUserChangeOrderImage2, O.FDDateChangeOrderImage3, O.FTTimeChangeOrderImage3, O.FTUserChangeOrderImage3, O.FDDateChangeOrderImage4,"
    '        _Cmd &= vbCrLf & "O.FTTimeChangeOrderImage4, O.FTUserChangeOrderImage4, O.FTOrderNoRef, O.rowguid, O.FTStateSendDirectorApp, O.FTStateSendDirectorBy, O.FDStateSendDirectorDate,"
    '        _Cmd &= vbCrLf & " O.FTStateSendDirectorTime, O.FTStateDirectorApp, O.FTStateDirectorAppBy, O.FDStateDirectorAppDate, O.FTStateDirectorAppTime, O.FTStateDirectorReject, O.FTStateDirectorRejectBy,"
    '        _Cmd &= vbCrLf & " O.FDStateDirectorRejectDate, O.FTStateDirectorRejectTime, O.FTStateFactoryApp, O.FTStateFactoryAppBy, O.FDStateFactoryAppDate, O.FTStateFactoryAppTime, O.FTStateFactoryReject,"
    '        _Cmd &= vbCrLf & " O.FTStateFactoryRejectBy, O.FDStateFactoryRejectDate, O.FTStateFactoryRejectTime, O.FTChangeCmpBy, O.FDChangeCmpDate, O.FTChangeCmpTime"
    '        _Cmd &= vbCrLf & "FROM         OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "' ). [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS T ON O.FTOrderNo = T.FTOrderNo"
    '        _Cmd &= vbCrLf & " where T.FTOrderNo Is null"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
    '        _Spls.Close()
    '        Return True
    '    Catch ex As Exception
    '        _Spls.Close()
    '        Return False
    '    End Try
    'End Function

#Region "SysncDataToServer"
    Private Function _TACCTSale(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            'Header
            'HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)


            _Cmd = "INSERT INTO  OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale"
            _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FDInvoiceDate, FTInvoiceBy, FTRemark, FNInvAmt, FNDisCountPer, FNDisCountAmt, FNInvNetAmt, FNVatPer, "
            _Cmd &= vbCrLf & " FNVatAmt, FNSurcharge, FNInvGrandAmt, FTInvGrandAmtTH, FTInvGrandAmtEN, FNHSysCmpId, FTCustomerName, FTCustAddr)"
            _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FDInvoiceDate, S.FTInvoiceBy, S.FTRemark, S.FNInvAmt, S.FNDisCountPer, S.FNDisCountAmt, "
            _Cmd &= vbCrLf & "S.FNInvNetAmt, S.FNVatPer, S.FNVatAmt, S.FNSurcharge, S.FNInvGrandAmt, S.FTInvGrandAmtTH, S.FTInvGrandAmtEN, S.FNHSysCmpId, S.FTCustomerName, S.FTCustAddr"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS T ON S.FTInvoiceNo = T.FTInvoiceNo"
            _Cmd &= vbCrLf & " WHERE(T.FTInvoiceNo Is NULL)"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function _TACCTSale_Detail(_ServerName As String) As Boolean
        Try
            Dim _Cmd As String = ""
            'Detail
            _Cmd = "INSERT INTO OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail"
            _Cmd &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FTBarcodeCustNo, FNQuantity, FNPrice, FNHSysWHFGId, FTOrderNo, FTColorway, FTSizeBreakDown ,FTDocumentRefNo)"
            _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FTBarcodeCustNo, S.FNQuantity, S.FNPrice, S.FNHSysWHFGId, S.FTOrderNo, S.FTColorway, S.FTSizeBreakDown , S.FTDocumentRefNo"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS S LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS T ON S.FTBarcodeCustNo = T.FTBarcodeCustNo AND "
            _Cmd &= vbCrLf & " S.FTInvoiceNo = T.FTInvoiceNo"
            _Cmd &= vbCrLf & "WHERE     (T.FTInvoiceNo IS NULL) AND (T.FTBarcodeCustNo IS NULL)"
            Return HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

    Private Sub ocmsyncdatatoserver_Click(sender As Object, e As EventArgs) Handles ocmsyncdatatoserver.Click
        Try
            Dim _ServerName As String = ""
            With _SetServerPopUp
                .Text = .Text & ocmsyncdatatoserver.Text
                .ShowDialog()
                If (.Proc) Then
                    _ServerName = .FTComputerName.Text
                Else
                    Exit Sub
                End If
            End With
            Try
                If Not My.Computer.Network.Ping(_ServerName.ToString) Then
                    HI.MG.ShowMsg.mInfo("connecting Server Problems..", 1508031711, Me.Text)
                    Exit Sub
                End If
            Catch ex As Exception
            End Try
            Dim _Spls As New HI.TL.SplashScreen("Syncing Data... Please Wait... ")
            If PSyncData(_ServerName, False, _Spls) Then
                '   HI.MG.ShowMsg.mInfo("synced data succussfuly...", 1508031702, Me.Text)
            Else
                '   HI.MG.ShowMsg.mInfo("synced data failed...", 1508031703, Me.Text)
            End If
            _Spls.Close()
        Catch ex As Exception
        End Try
    End Sub

    'Private Function SyncDataToServer(_ServerName As String) As Boolean
    '    Dim _Spls As New HI.TL.SplashScreen("being synced sysnc Data.... Please Wait")
    '    Try
    '        Dim _Cmd As String = ""
    '        'Header
    '        HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_ACCOUNT)
    '        _Cmd = "INSERT INTO  OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale"
    '        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FDInvoiceDate, FTInvoiceBy, FTRemark, FNInvAmt, FNDisCountPer, FNDisCountAmt, FNInvNetAmt, FNVatPer, "
    '        _Cmd &= vbCrLf & " FNVatAmt, FNSurcharge, FNInvGrandAmt, FTInvGrandAmtTH, FTInvGrandAmtEN, FNHSysCmpId, FTCustomerName, FTCustAddr)"
    '        _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FDInvoiceDate, S.FTInvoiceBy, S.FTRemark, S.FNInvAmt, S.FNDisCountPer, S.FNDisCountAmt, "
    '        _Cmd &= vbCrLf & "S.FNInvNetAmt, S.FNVatPer, S.FNVatAmt, S.FNSurcharge, S.FNInvGrandAmt, S.FTInvGrandAmtTH, S.FTInvGrandAmtEN, S.FNHSysCmpId, S.FTCustomerName, S.FTCustAddr"
    '        _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS S LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & "OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale AS T ON S.FTInvoiceNo = T.FTInvoiceNo"
    '        _Cmd &= vbCrLf & " WHERE(T.FTInvoiceNo Is NULL)"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
    '        'Detail
    '        _Cmd = "INSERT INTO OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail"
    '        _Cmd &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTInvoiceNo, FTBarcodeCustNo, FNQuantity, FNPrice)"
    '        _Cmd &= vbCrLf & "SELECT     S.FTInsUser, S.FDInsDate, S.FTInsTime, S.FTUpdUser, S.FDUpdDate, S.FTUpdTime, S.FTInvoiceNo, S.FTBarcodeCustNo, S.FNQuantity, S.FNPrice"
    '        _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS S LEFT OUTER JOIN"
    '        _Cmd &= vbCrLf & " OPENDATASOURCE ('SQLOLEDB', 'Data Source=" & _ServerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "').[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTSale_Detail AS T ON S.FTBarcodeCustNo = T.FTBarcodeCustNo AND "
    '        _Cmd &= vbCrLf & " S.FTInvoiceNo = T.FTInvoiceNo"
    '        _Cmd &= vbCrLf & "WHERE     (T.FTInvoiceNo IS NULL) AND (T.FTBarcodeCustNo IS NULL)"
    '        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
    '        _Spls.Close()
    '        Return True
    '    Catch ex As Exception
    '        _Spls.Close()
    '        Return False
    '    End Try
    'End Function
    'Private Sub ocmSyncDataManual_Click(sender As Object, e As EventArgs) Handles ocmSyncDataManual.Click
    '    Try
    '        With New wPopUpSyncDataManual
    '            .ShowDialog()
    '        End With



    '    Catch ex As Exception
    '    End Try
    'End Sub
End Class