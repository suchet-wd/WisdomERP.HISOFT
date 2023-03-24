Imports System.Drawing
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wActualShip

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        IniGrid()
    End Sub
#Region "Object"
    Private _ColExport As String = "FTTransportNo|FDDateChk|FTBOI|FTCmpCode|FTBookingNo|FTBillOfLading|FTExportInvoiceUser|FTRemark|FTIssBankName|FNExportAmountBaht|FNExportExchangeRate|FDSellingDate|FDExFacDate"
    Private _ColTrans As String = "FTShipInvNo|FDShipInvDate|FNShipAmount|FDShipDate|FTTransInvNo|FDTransInvDate|FNTransAmount|FDTransDate|FTOT|FTOther|FTAirAuthor"
    Private _ColAcc As String = "FNAccExchageRate|FNAccTicketFee|FNAccBankFee|FNAccInterest|FNAccClemson|FNAccTransferOtherFee"
#End Region

#Region "Method"
    Private Sub IniGrid()
        Try
            Dim _Col As String = ""
            If (ocmsave.Enabled) Then _Col &= _ColExport
            If _Col <> "" Then _Col &= "|"
            If (ocmsavewh.Enabled) Then _Col &= _ColTrans
            If _Col <> "" Then _Col &= "|"
            If (ocmsaveacc.Enabled) Then _Col &= _ColAcc

            With Me.AdvBandedGridView1
                For Each _Field As String In _Col.Split("|")
                    .Columns.ColumnByFieldName(_Field).OptionsColumn.AllowEdit = True
                    .Columns.ColumnByFieldName(_Field).OptionsColumn.ReadOnly = False
                    .Columns.ColumnByFieldName(_Field).OptionsColumn.AllowFocus = True
                    .Columns.ColumnByFieldName(_Field).OptionsColumn.AllowMove = True
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Transaction"
    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("ระบบกำลังโหลดข้อมูล กรุณารอซักครู่......")
        Try
            Dim _Cmd As String = ""
            '_Cmd = "SELECT T.FTExportInvoiceNo, T.FTInvoiceNo, T.FNSeq, T.FTCustomerPO, T.FTStyleCode, T.FTStyleNameEN, T.FNGrandQuantity, T.FNNetPrice, T.FNAmount, T.FTCurCode, T.FTProvinceNameEN, "
            '_Cmd &= vbCrLf & " T.FTShipModeCode, T.FTCustNameEN, T.FTETD, T.FTCmpCode, T.FTBookingNo, X.FTLetterOfCredit, X.FTTransportNo, X.FTBOI, X.FTBillOfLading, X.FTRemark, X.FTShipInvNo, "
            '_Cmd &= vbCrLf & " X.FNShipAmount, X.FDShipDate, X.FTTransInvNo, X.FDTransInvDate, X.FNTransAmount, X.FDTransDate, X.FTOT, X.FTOther, X.FTAirAuthor, X.FNAccExchageRate, X.FNAccTicketFee, X.FNAccBankFee, X.FNAccInterest, "
            '_Cmd &= vbCrLf & " X.FNAccClemson, X.FNAccTransferOtherFee"
            '_Cmd &= vbCrLf & " ,T.FTIssBankName ,T.FTExportInvoiceUser , T.FTTermOfPMCode,X.FNExportAmountBaht , X.FNExportExchangeRate , X.FDSellingDate , X.FDExFacDate"
            '_Cmd &= vbCrLf & ",CASE WHEN Isdate(T.FDExportInvoiceDate) = 1 Then convert(varchar(10),convert(date,T.FDExportInvoiceDate),103) Else '' END AS FDExportInvoiceDate"
            '_Cmd &= vbCrLf & ",CASE WHEN Isdate(X.FDDateChk) = 1 Then convert(varchar(10),convert(date,X.FDDateChk),103) Else '' END AS FDDateChk"
            '_Cmd &= vbCrLf & ",CASE WHEN Isdate( X.FDShipInvDate) = 1 Then convert(varchar(10),convert( date ,X.FDShipInvDate),103) Else '' END AS FDShipInvDate "
            '_Cmd &= vbCrLf & " , T.FTColorway , T.FTSizeBreakDown"
            '_Cmd &= vbCrLf & "FROM     (Select I.FTIssBankName, I.FTExportInvoiceUser, PT.FTTermOfPMCode , S.FNHSysStyleId,  I.FTExportInvoiceNo, I.FDExportInvoiceDate, ID.FTInvoiceNo, ID.FNSeq"
            '_Cmd &= vbCrLf & ", CM.FTCustomerPO, S.FTStyleCode, S.FTStyleNameEN,  MI.FNQuantity AS  FNGrandQuantity, IP.FNPrice AS  FNNetPrice ,IP.FTColorway , IP.FTSizeBreakDown , "
            '_Cmd &= vbCrLf & "   MI.FNQuantity * IP.FNPrice AS FNAmount, MC.FTCurCode, PV.FTProvinceNameEN, SM.FTShipModeCode, CS.FTCustNameEN, I.FTETD, CP.FTCmpCode, I.FTBookingNo"
            '_Cmd &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice As I With (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Detail AS ID WITH (NOLOCK) ON I.FTExportInvoiceNo = ID.FTExportInvoiceNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTFactoryCMInvoice As CM With (NOLOCK) On ID.FTInvoiceNo = CM.FTInvoiceNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON CM.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As S With (NOLOCK) On O.FNHSysStyleId = S.FNHSysStyleId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMPaymentTerm AS PT WITH(NOLOCK) ON I.FNHSysTermOfPMId = PT.FNHSysTermOfPMId  LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExportInvoice_Price AS IP WITH(NOLOCK) ON I.FTExportInvoiceNo = IP.FTExportInvoiceNo and ID.FTInvoiceNo =  IP.FTInvoiceNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTFactoryCMInvoice_D AS MI   WITH(NOLOCK) ON IP.FTInvoiceNo = MI.FTInvoiceNo and IP.FTColorway = MI.FTColorway and IP.FTSizeBreakDown = MI.FTSizeBreakDown and IP.FTCustomerPO = MI.FTCustomerPO LEFT OUTER JOIN "
            ''  _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS VS ON CM.FTCustomerPO = VS.FTPOref AND O.FTOrderNo = VS.FTOrderNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As OS With (NOLOCK) On O.FTOrderNo = OS.FTOrderNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS MC WITH (NOLOCK) ON OS.FNHSysCurId = MC.FNHSysCurId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode As SM With (NOLOCK) On OS.FNHSysShipModeId = SM.FNHSysShipModeId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CP WITH (NOLOCK) ON O.FNHSysCmpId = CP.FNHSysCmpId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As CS With (NOLOCK) On O.FNHSysCustId = CS.FNHSysCustId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS PV WITH (NOLOCK) ON OS.FNHSysProvinceId = PV.FNHSysProvinceId"
            '_Cmd &= vbCrLf & "  WHERE   (O.FNJobState = 1)) As T LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTActualShipment As X With (NOLOCK) On T.FTInvoiceNo = X.FTInvoiceNo And T.FDExportInvoiceDate = X.FDInvoiceDate And T.FTCustomerPO = X.FTPORef And T.FNHSysStyleId = X.FNHSysStyleId"
            '_Cmd &= vbCrLf & " WHERE T.FTExportInvoiceNo <> ''"

            _Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].. V_TExpAcualShip where FTInvoiceNo <> ''  "
            If Me.FTInvoiceNo.Text <> "" Then
                _Cmd &= vbCrLf & " And FTInvoiceNo >='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            End If
            If Me.FTInvoiceNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " And FTInvoiceNo <='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNoTo.Text) & "'"
            End If
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "And FTStyleCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "And FTStyleCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FTCustomerPO.Text <> "" Then
                _Cmd &= vbCrLf & "And FTPORef >='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "'"
            End If
            If Me.FTCustomerPOTo.Text <> "" Then
                _Cmd &= vbCrLf & "And FTPORef <='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPOTo.Text) & "'"
            End If
            If Me.InvDate.Text <> "" Then
                _Cmd &= vbCrLf & "And FDInvDateEN >='" & HI.UL.ULDate.ConvertEnDB(Me.InvDate.Text) & "'"
            End If
            If Me.InvDateTo.Text <> "" Then
                _Cmd &= vbCrLf & "And FDInvDateEN <='" & HI.UL.ULDate.ConvertEnDB(Me.InvDateTo.Text) & "'"
            End If
            If Me.FTShipDate.Text <> "" Then
                _Cmd &= vbCrLf & " And FDShipDateOrg >='" & HI.UL.ULDate.ConvertEnDB(Me.FTShipDate.Text) & "'"
            End If
            If Me.FTShipDateTo.Text <> "" Then
                _Cmd &= vbCrLf & " And FDShipDateOrg <='" & HI.UL.ULDate.ConvertEnDB(Me.FTShipDateTo.Text) & "'"
            End If

            '_Cmd &= vbCrLf & "GROUP BY T.FTExportInvoiceNo, T.FTInvoiceNo, T.FNSeq, T.FTCustomerPO, T.FTStyleCode, T.FTStyleNameEN, T.FNGrandQuantity, T.FNNetPrice, T.FNAmount, T.FTCurCode, T.FTProvinceNameEN, T.FTShipModeCode, "
            '_Cmd &= vbCrLf & " T.FTCustNameEN, T.FTETD, T.FTCmpCode, T.FTBookingNo, X.FTLetterOfCredit, X.FTTransportNo, X.FTBOI, X.FTBillOfLading, X.FTRemark, X.FTShipInvNo, X.FNShipAmount, X.FDShipDate, X.FTTransInvNo, "
            '_Cmd &= vbCrLf & " X.FDTransInvDate, X.FNTransAmount, X.FDTransDate, X.FTOT, X.FTOther, X.FTAirAuthor, X.FNAccExchageRate, X.FNAccTicketFee, X.FNAccBankFee, X.FNAccInterest, X.FNAccClemson, X.FNAccTransferOtherFee, "
            '_Cmd &= vbCrLf & " T.FTIssBankName, T.FTExportInvoiceUser, T.FTTermOfPMCode, X.FNExportAmountBaht, X.FNExportExchangeRate, X.FDSellingDate, X.FDExFacDate ,T .FDExportInvoiceDate ,X.FDDateChk ,X.FDShipInvDate"
            '_Cmd &= vbCrLf & " , T.FTColorway , T.FTSizeBreakDown"
            Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Function SaveData(Permiss As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As System.Data.DataTable
            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With

            Dim _pColExport As String = "FTTransportNo|FDDateChk|FTBOI|FTBillOfLading|FTOther|FNExportAmountBaht|FDExFacDate|FTBookingNo"
            Dim _pColTrans As String = "FTShipInvNo|FDShipInvDate|FNShipAmount|FDShipDate|FTTransInvNo|FDTransInvDate|FNTransAmount|FDTransDate|FTOT|FTRemark|FTAirAuthor"
            Dim _pColAcc As String = "FNAccExchageRate|FNAccTicketFee|FNAccBankFee|FNAccInterest|FNAccClemson|FNAccTransferOtherFee|FNExportExchangeRate|FDSellingDate"

            Dim _Col As String = ""
            Select Case Permiss
                Case 1
                    If (ocmsave.Enabled) Then _Col = _pColExport
                Case 2
                    If (ocmsavewh.Enabled) Then _Col = _pColTrans
                Case 3
                    If (ocmsaveacc.Enabled) Then _Col = _pColAcc
            End Select

            _Col = Replace(_Col, "|", ",")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _oDt.Rows
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTActualShipment"
                _Cmd &= vbCrLf & "Set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                Select Case True
                    Case Permiss = 1
                        _Cmd &= vbCrLf & ",FTTransportNo='" & R!FTTransportNo.ToString & "'"
                        _Cmd &= vbCrLf & ",FDDateChk='" & HI.UL.ULDate.ConvertEnDB(R!FDDateChk.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTBOI='" & HI.UL.ULF.rpQuoted(R!FTBOI.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTBillOfLading='" & HI.UL.ULF.rpQuoted(R!FTBillOfLading.ToString) & "'"

                        _Cmd &= vbCrLf & ",FTOther='" & HI.UL.ULF.rpQuoted(R!FTOther.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTBookingNo='" & HI.UL.ULF.rpQuoted(R!FTBookingNo.ToString) & "'"
                    ' _Cmd &= vbCrLf & ",FNExportAmountBaht=" & Double.Parse("0" & R!FNExportAmountBaht.ToString)
                    ' _Cmd &= vbCrLf & ",FNExportExchangeRate=" & Double.Parse("0" & R!FNExportExchangeRate.ToString)
                    ' _Cmd &= vbCrLf & ",FDSellingDate='" & HI.UL.ULDate.ConvertEnDB(R!FDSellingDate.ToString) & "'"
                    ' _Cmd &= vbCrLf & ",FDExFacDate='" & HI.UL.ULDate.ConvertEnDB(R!FDExFacDate.ToString) & "'"
                    Case Permiss = 2
                        _Cmd &= vbCrLf & ",FTShipInvNo='" & R!FTShipInvNo.ToString & "'"
                        _Cmd &= vbCrLf & ",FDShipInvDate='" & HI.UL.ULDate.ConvertEnDB(R!FDShipInvDate.ToString) & "'"
                        _Cmd &= vbCrLf & ",FNShipAmount=" & Double.Parse("0" & R!FNShipAmount.ToString)
                        _Cmd &= vbCrLf & ",FDShipDate='" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTTransInvNo='" & R!FTTransInvNo.ToString & "'"
                        _Cmd &= vbCrLf & ",FDTransInvDate='" & HI.UL.ULDate.ConvertEnDB(R!FDTransInvDate.ToString) & "'"
                        _Cmd &= vbCrLf & ",FNTransAmount=" & Double.Parse("0" & R!FNTransAmount.ToString)
                        _Cmd &= vbCrLf & ",FDTransDate='" & HI.UL.ULDate.ConvertEnDB(R!FDTransDate.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTOT='" & HI.UL.ULF.rpQuoted(R!FTOT.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                        _Cmd &= vbCrLf & ",FTAirAuthor='" & HI.UL.ULF.rpQuoted(R!FTAirAuthor.ToString) & "'"
                    Case Permiss = 3
                        _Cmd &= vbCrLf & ",FNAccExchageRate=" & Double.Parse("0" & R!FNAccExchageRate.ToString)
                        _Cmd &= vbCrLf & ",FNExportExchangeRate=" & Double.Parse("0" & R!FNExportExchangeRate.ToString)

                        '_Cmd &= vbCrLf & ",FNAccTicketFee=" & Double.Parse("0" & R!FNAccTicketFee.ToString)
                        '_Cmd &= vbCrLf & ",FNAccBankFee=" & Double.Parse("0" & R!FNAccBankFee.ToString)
                        '_Cmd &= vbCrLf & ",FNAccInterest=" & Double.Parse("0" & R!FNAccInterest.ToString)
                        '_Cmd &= vbCrLf & ",FNAccClemson=" & Double.Parse("0" & R!FNAccClemson.ToString)
                        _Cmd &= vbCrLf & ",FNAccTransferOtherFee=" & Double.Parse("0" & R!FNAccTransferOtherFee.ToString)
                        _Cmd &= vbCrLf & ",FDSellingDate='" & HI.UL.ULDate.ConvertEnDB(R!FDSellingDate.ToString) & "'"
                End Select

                _Cmd &= vbCrLf & " where  FTInvoiceNo ='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                _Cmd &= vbCrLf & " and   FTPORef ='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                _Cmd &= vbCrLf & " and   FNHSysStyleId =" & Integer.Parse(R!FNHSysStyleId.ToString)

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTActualShipment (FTInsUser, FDInsDate, FTInsTime,FTInvoiceNo,FTPORef,FNHSysStyleId," & _Col & ")"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysStyleId.ToString)

                    Select Case True
                        Case Permiss = 1
                            _Cmd &= vbCrLf & ",'" & R!FTTransportNo.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDDateChk.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBOI.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBillOfLading.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOther.ToString) & "'"
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNExportAmountBaht.ToString)
                            '_Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNExportExchangeRate.ToString)
                            '_Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDSellingDate.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDExFacDate.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBookingNo.ToString) & "'"
                        Case Permiss = 2
                            _Cmd &= vbCrLf & ",'" & R!FTShipInvNo.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipInvDate.ToString) & "'"
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNShipAmount.ToString)
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDShipDate.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & R!FTTransInvNo.ToString & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDTransInvDate.ToString) & "'"
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNTransAmount.ToString)
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDTransDate.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOT.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTAirAuthor.ToString) & "'"
                        Case Permiss = 3
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAccExchageRate.ToString)
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAccTicketFee.ToString)
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAccBankFee.ToString)
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAccInterest.ToString)
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAccClemson.ToString)
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNAccTransferOtherFee.ToString)
                            _Cmd &= vbCrLf & "," & Double.Parse("0" & R!FNExportExchangeRate.ToString)
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FDSellingDate.ToString) & "'"
                    End Select


                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If


                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TEXPTCMInvoice_Post"
                _Cmd &= vbCrLf & "set FTBookingNo ='" & HI.UL.ULF.rpQuoted(R!FTBookingNo.ToString) & "'"
                _Cmd &= vbCrLf & " where  FTInvoiceNo ='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


    Private Function VerrifyData() As Boolean
        Try
            Dim Pass As Boolean = False
            Dim _FieldName As String = ""
            Dim _Field As String = "FNHSysStyleId|FNHSysStyleIdTo|FTInvoiceNo|FTInvoiceNoTo|InvDate|InvDateTo|FTCustomerPO|FTCustomerPOTo|FTShipDate|FTShipDateTo"

            For Each _FieldName In _Field.Split("|")
                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If Not (.Text.Trim() = "") Then
                                        Pass = True
                                    End If
                                End If
                            End With

                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If Not (.SelectedIndex < 0) Then Pass = True
                            End With

                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If Not (HI.UL.ULDate.CheckDate(.Text) = "") Then
                                    Pass = True
                                End If
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If Not (.Image Is Nothing) Then
                                    Pass = True
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            If Not (Obj.Text = "") Then
                                Pass = True
                            End If
                        Case Else
                            Pass = False
                    End Select
                Next
            Next
            If Pass = False Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTInvoiceNo.Text)
                FTInvoiceNo.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region "Handles"
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerrifyData() Then
                Call LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private _State As Integer = 0
    Public Property State As Integer
        Get
            Return _State
        End Get
        Set(value As Integer)
            _State = value
        End Set
    End Property
    Private Sub Save_Click(sender As Object, e As EventArgs)
        Try
            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If (.Rows.Count <= 0) Then
                    HI.MG.ShowMsg.mInfo("Pls Select Data Transaction !!", 1610171054, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                    Exit Sub
                End If
            End With
            If (SaveData(State)) Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Call LoadData()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        _State = 1
        Call Save_Click(sender, e)
    End Sub

    Private Sub ocmsaveacc_Click(sender As Object, e As EventArgs) Handles ocmsaveacc.Click
        _State = 2
        Call Save_Click(sender, e)
    End Sub

    Private Sub ocmsavewh_Click(sender As Object, e As EventArgs) Handles ocmsavewh.Click
        _State = 3
        Call Save_Click(sender, e)
    End Sub

    Private Sub RepositoryFNExportExchangeRate_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFNExportExchangeRate.EditValueChanging
        Try
            With Me.AdvBandedGridView1
                If .RowCount < 0 Or .FocusedRowHandle <= -1 Then Exit Sub
                Dim _Amt As Double = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAmount"))
                Dim _AmtBaht As Double = 0
                _AmtBaht = _Amt * e.NewValue
                .SetRowCellValue(.FocusedRowHandle, "FNExportAmountBaht", _AmtBaht)

            End With
        Catch ex As Exception

        End Try
    End Sub
    Private _handle As Integer = 0
    Private Sub AdvBandedGridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles AdvBandedGridView1.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then

                Dim category As String = Microsoft.VisualBasic.Right(View.GetRowCellValue(e.RowHandle, View.Columns("FTInvoiceNo")), 4)

                If category Mod 2 = 0 Then
                    e.Appearance.BackColor = Color.SkyBlue
                    'e.Appearance.BackColor2 = Color.SeaShell

                    '   _handle = e.RowHandle
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AdvBandedGridView1_CellMerge(sender As Object, e As CellMergeEventArgs) Handles AdvBandedGridView1.CellMerge
        Try
            Dim View As GridView = sender
            If (e.Column.FieldName = "FTInvoiceNo") Then
                Dim _value1 As String = View.GetRowCellValue(e.RowHandle1, e.Column)
                Dim _value2 As String = View.GetRowCellValue(e.RowHandle2, e.Column)
                e.Merge = _value1 = _value2
                e.Handled = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmcl_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception

        End Try
    End Sub





#End Region

End Class