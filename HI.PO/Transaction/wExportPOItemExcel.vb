Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO


Public Class wExportPOItemExcel
    Private _DefailtPath As String

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmexportrycexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click

        If Me.FNHSysBuyId.Text <> "" Then

            If Me.FNHSysSuplId.Text <> "" Then

                With CType(ogcpurchase.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    If .Select("FTStateSelect='1'").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาทำการระบุหมายเลย PO !!!", 1612813254, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End With


                Dim Op As New System.Windows.Forms.FolderBrowserDialog

                If _DefailtPath <> "" Then
                    Op.SelectedPath = _DefailtPath
                End If

                Dim FileNameRef As String = ""
                If Op.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If _DefailtPath <> Op.SelectedPath Then

                        WriteRegistry(Op.SelectedPath)
                        _DefailtPath = Op.SelectedPath

                    End If
                    Dim _Spls As New HI.TL.SplashScreen("Loading...Data Pleas wait.")

                    Dim StateExpoort As Boolean = False
                    Select Case FNExportPOSuplFormat.SelectedIndex
                        Case 0
                            StateExpoort = ExportTAGTime()
                        Case 1
                            StateExpoort = ExportMaxim()
                        Case 2
                            StateExpoort = ExportEDI(FileNameRef)
                        Case 3
                            StateExpoort = ExportNIKEVAS()
                        Case 4
                            StateExpoort = ExportPOAPL()
                    End Select

                    _Spls.Close()

                    If StateExpoort Then
                        HI.MG.ShowMsg.mInfo("Export Data Complete !!!", 1614413254, Me.Text, , MessageBoxIcon.Warning)
                    End If

                    Try
                        Select Case FNExportPOSuplFormat.SelectedIndex
                            Case 2
                                Process.Start(FileNameRef)
                            Case Else

                                Process.Start("explorer.exe", _DefailtPath)
                        End Select

                    Catch ex As Exception

                    End Try
                End If


            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysSuplId_lbl.Text)
                FNHSysSuplId.Focus()

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()

        End If

    End Sub


    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load

        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function ReadRegistry() As String
        Dim regKey As Microsoft.Win32.RegistryKey
        Dim valreturn As String = ""

        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\HI SOFT", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExportPOItem", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathExportPOItem", value.ToString)
        regKey.Close()

    End Sub

    Private Sub LoadPurchaseNo()

        Dim _UserName As String = HI.ST.UserInfo.UserName
        Dim CusItemCodeRef As String = ""
        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""

        ' _UserName = "mlpsirikanya"

        FNHSysSuplId.Properties.Tag = Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysSuplId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK) WHERE FTSuplCode='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, ""))

        Dim _Qry As String = ""
        Dim _dt As System.Data.DataTable
        _Qry = " SELECT * FROM (  Select '0' AS FTStateSelect, PH.FTPurchaseNo "
        _Qry &= vbCrLf & "		, CASE WHEN ISDATE(PH.FDDeliveryDate) = 1 THEN Convert(nvarchar(10), Convert(Datetime,FDDeliveryDate)  ,103) ELSE '' END AS  FDDeliveryDate,PH.FTPurchaseBy "

        _Qry &= vbCrLf & " ,MAX(U.FTTel) AS FTTel"
        _Qry &= vbCrLf & " ,MAX(U.FTEmail) AS FTEmail"
        _Qry &= vbCrLf & " ,MAX(U.FTFax) AS FTFax"
        _Qry &= vbCrLf & " ,MAX(U.FTNickName) AS FTNickName"

        _Qry &= vbCrLf & " ,MAX(SS.FTSuplNameEN) AS FTSuplName"
        _Qry &= vbCrLf & " ,MAX(SS.FTAddr1EN) AS FTAddr1"
        _Qry &= vbCrLf & " ,MAX(SS.FTAddr2EN) AS FTAddr2"
        _Qry &= vbCrLf & " ,MAX(SS.FTPerson1) AS FTPerson"
        _Qry &= vbCrLf & " ,MAX(SS.FTPhone) AS FTPhone"
        _Qry &= vbCrLf & " ,MAX(SS.FTMail1) AS FTMail1"

        _Qry &= vbCrLf & "		, CASE WHEN ISDATE(PH.FDPurchaseDate) = 1 THEN Convert(nvarchar(10), Convert(Datetime,FDPurchaseDate)  ,103) ELSE '' END AS  FDPurchaseDate "



        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH With(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On PH.FTPurchaseNo = PD.FTPurchaseNo  "
        _Qry &= vbCrLf & "		  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As U With(NOLOCK) On PH.FTPurchaseBy = U.FTUserName  "
        _Qry &= vbCrLf & "         INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As OD On PD.FTOrderNo = OD.FTOrderNo "
        _Qry &= vbCrLf & "		  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As SS With(NOLOCK) On PH.FNHSysSuplId = SS.FNHSysSuplId  "

        _Qry &= vbCrLf & " WHERE PH.FNHSysSuplId=" & Val(FNHSysSuplId.Properties.Tag.ToString()) & " "
        _Qry &= vbCrLf & " AND    (OD.FNHSysBuyId =" & Val(FNHSysBuyId.Properties.Tag.ToString) & ") "

        'If HI.ST.SysInfo.Admin = False Then
        '    _Qry &= vbCrLf & " 	And U.FNHSysTeamGrpId IN  (SELECT  UX.FNHSysTeamGrpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS UX WITH(NOLOCK) WHERE UX.FTUserName ='" & HI.UL.ULF.rpQuoted(_UserName) & "' ) "
        'End If

        _Qry &= vbCrLf & " GROUP BY PH.FTPurchaseNo, PH.FDDeliveryDate,PH.FTPurchaseBy,PH.FDPurchaseDate  "


        _Qry &= vbCrLf & " ) AS A "
        _Qry &= vbCrLf & " ORDER BY A.FTPurchaseNo "



        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        ogcpurchase.DataSource = _dt.Copy()

        _dt.Dispose()

    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged
        FNHSysSuplId.Text = ""
        ogcpurchase.DataSource = Nothing
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged
        If FNHSysSuplId.Text = "" Then
            ogcpurchase.DataSource = Nothing
        Else
            Call LoadPurchaseNo()
        End If


    End Sub


    Private Function ExportTAGTime() As Boolean
        Dim _Cmd As String = ""
        Dim _dt As System.Data.DataTable
        Dim _dtpurchase As System.Data.DataTable


        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""
        Dim StyleNo As String = ""
        Dim DataSeason As String = ""


        Dim ShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim ShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim ShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim ShipAddress4 As String = "10140"
        Dim ShipAddress5 As String = "THAILAND"


        Dim ShipAddress6 As String = ""
        Dim ShipAddress7 As String = ""
        Dim ShipAddress8 As String = ""
        Dim ShipAddress9 As String = ""

        With CType(ogcpurchase.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _dtpurchase = .Copy
        End With

        PurchaseNo = ""
        DeliveryDate = ""
        DataSeason = ""
        StyleNo = ""

        For Each R As System.Data.DataRow In _dtpurchase.Select("FTStateSelect='1'", "FTPurchaseNo")


            PurchaseNo = R!FTPurchaseNo.ToString

            _Cmd = " SELECT ISNULL(X.FTRawMatCode,'') AS FTRawMatCode,SUM(X.FNQuantity)  AS FNQuantity,SUM(X.FNNetAmt) AS FNNetAmt,MAX(X.FTTel) AS FTTel,MAX(X.FTEmail) AS FTEmail,MAX(X.FTFax) AS FTFax,MAX(X.FTNickName) AS FTNickName"
            _Cmd &= vbCrLf & " FROM (SELECT A.* "
            _Cmd &= vbCrLf & "  FROM (SELECT PH.FTPurchaseNo, PD.FNQuantity, PD.FNPrice, PD.FNDisPer, PD.FNDisAmt, PD.FNNetAmt, PD.FTOrderNo,U.FTTel,U.FTEmail,U.FTFax,U.FTNickName"
            _Cmd &= vbCrLf & " ,IM.FTRawMatCode + CASE WHEN ISNULL(IMC.FTRawMatColorCode,'') <>'' THEN  ('-' + ISNULL(IMC.FTRawMatColorCode,''))  ELSE '' END + CASE WHEN ISNULL(IMS.FTRawMatSizeCode,'') <>'' THEN  ('-' + ISNULL(IMS.FTRawMatSizeCode,''))  ELSE '' END AS FTRawMatCode "
            ' _Cmd &= vbCrLf & "		, [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_StyleSeason(PH.FTPurchaseNo) AS FTSeasonCode "
            _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH WITH(NOLOCK) INNER JOIN "
            _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PD WITH(NOLOCK) ON PH.FTPurchaseNo = PD.FTPurchaseNo INNER JOIN "
            _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM WITH(NOLOCK) On PD.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
            _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
            _Cmd &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
            _Cmd &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK) ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId "
            _Cmd &= vbCrLf & "          INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK)  ON PH.FTPurchaseBy = U.FTUserName "
            _Cmd &= vbCrLf & " WHERE   (PH.FTPurchaseNo = '" & PurchaseNo & "') "

            _Cmd &= vbCrLf & " ) As A "

            _Cmd &= vbCrLf & "  ) AS X "
            _Cmd &= vbCrLf & " GROUP BY ISNULL(X.FTRawMatCode,'')"

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then

                Try
                    ShipAddress6 = _dt.Rows(0)!FTNickName.ToString()
                    ShipAddress7 = _dt.Rows(0)!FTTel.ToString()
                    ShipAddress8 = _dt.Rows(0)!FTEmail.ToString()
                    ShipAddress9 = _dt.Rows(0)!FTFax.ToString()
                Catch ex As Exception
                End Try

                _Cmd = "  Select  ST.FTStyleCode,SS.FTSeasonCode  "
                _Cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH WITH(NOLOCK) INNER Join "
                _Cmd &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On PH.FTPurchaseNo = PD.FTPurchaseNo  "
                _Cmd &= vbCrLf & "    INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON PD.FTOrderNo = O.FTOrderNo  "
                _Cmd &= vbCrLf & "   INNER Join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On O.FNHSysStyleId =ST.FNHSysStyleId "
                _Cmd &= vbCrLf & "   INNER Join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON O.FNHSysSeasonId = SS.FNHSysSeasonId "
                _Cmd &= vbCrLf & "  WHERE(PH.FTPurchaseNo = '" & PurchaseNo & "')  "
                _Cmd &= vbCrLf & "  And O.FNOrderType  Not IN (1,2,3,4,5) "
                _Cmd &= vbCrLf & "      GROUP BY ST.FTStyleCode, SS.FTSeasonCode "

                Dim dtinfo As System.Data.DataTable
                dtinfo = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)
                StyleNo = ""
                DataSeason = ""

                For Each Rx As System.Data.DataRow In dtinfo.Select("FTStyleCode<>''", "FTStyleCode")


                    If Rx!FTStyleCode.ToString <> "" Then
                        If StyleNo = "" Then
                            StyleNo = Rx!FTStyleCode.ToString
                        Else
                            If StyleNo.Contains(Rx!FTStyleCode.ToString) = False Then
                                StyleNo = StyleNo & "," & Rx!FTStyleCode.ToString
                            End If

                        End If
                    End If

                    If Rx!FTSeasonCode.ToString <> "" Then
                        If DataSeason = "" Then
                            DataSeason = Rx!FTSeasonCode.ToString
                        Else
                            If DataSeason.Contains(Rx!FTSeasonCode.ToString) = False Then
                                DataSeason = DataSeason & "," & Rx!FTSeasonCode.ToString
                            End If
                        End If
                    End If

                Next

                dtinfo.Dispose()

                '  DataSeason = _dt.Rows(0)!FTSeasonCode.ToString
                opshet.BeginUpdate()

                Try
                    With opshet.ActiveWorksheet
                        .Rows(5).Item(1).Value = PurchaseNo
                        .Rows(5).Item(4).Value = DeliveryDate

                        .Rows(6).Item(1).Value = ShipAddress1
                        .Rows(7).Item(1).Value = ShipAddress2
                        .Rows(8).Item(1).Value = ShipAddress3
                        .Rows(9).Item(1).Value = ShipAddress4
                        .Rows(10).Item(1).Value = ShipAddress5

                        .Rows(11).Item(1).Value = ShipAddress6
                        .Rows(12).Item(1).Value = ShipAddress7
                        .Rows(13).Item(1).Value = ShipAddress8
                        .Rows(14).Item(1).Value = ShipAddress9

                        .Rows(6).Item(4).Value = ShipAddress1
                        .Rows(7).Item(4).Value = ShipAddress2
                        .Rows(8).Item(4).Value = ShipAddress3
                        .Rows(9).Item(4).Value = ShipAddress4
                        .Rows(10).Item(4).Value = ShipAddress5

                        .Rows(11).Item(4).Value = ShipAddress6
                        .Rows(12).Item(4).Value = ShipAddress7
                        .Rows(13).Item(4).Value = ShipAddress8
                        .Rows(14).Item(4).Value = ShipAddress9


                        Dim RowIndx As Integer = 0
                        Dim StartRow As Integer = 20
                        Dim Qty As Decimal = 0.00
                        Dim Amt As Decimal = 0.00

                        For Each RXi As System.Data.DataRow In _dt.Select("FTRawMatCode<>''", "FTRawMatCode")

                            Qty = Val(RXi!FNQuantity.ToString)
                            Amt = Val(RXi!FNNetAmt.ToString)

                            If RowIndx > 9 Then
                                .Rows(StartRow + RowIndx).Insert()
                            End If

                            If .Rows(StartRow + RowIndx).Item(0).Value.ToString.ToLower() = "SPECIAL INSTRUCTION:".ToLower() Then
                                .Rows(StartRow + RowIndx).Insert()
                                .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)
                            End If

                            .Rows(StartRow + RowIndx).Item(0).Value = (RowIndx + 1).ToString() & "."
                            .Rows(StartRow + RowIndx).Item(1).Value = RXi!FTRawMatCode.ToString
                            .Rows(StartRow + RowIndx).Item(5).Value = Qty

                            RowIndx = RowIndx + 1

                        Next

                    End With

                Catch ex As Exception
                End Try

                opshet.EndUpdate()

                Dim FileName As String = _DefailtPath & "\" & PurchaseNo & ".xls"
                opshet.SaveDocument(FileName)

                Call LoadFormatFile()

            Else
            End If

        Next
        Return True
    End Function

    Private Function ExportMaxim() As Boolean
        Dim _Cmd As String = ""
        Dim _dt As System.Data.DataTable
        Dim _dtpurchase As System.Data.DataTable


        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""
        Dim StyleNo As String = ""
        Dim DataSeason As String = ""
        Dim DeliveryPlace As String = ""

        Dim ShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim ShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim ShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim ShipAddress4 As String = "10140"
        Dim ShipAddress5 As String = "THAILAND"


        Dim ShipAddress6 As String = ""
        Dim ShipAddress7 As String = ""
        Dim ShipAddress8 As String = ""
        Dim ShipAddress9 As String = ""


        Dim VenderName As String = ""
        Dim VenderAddress1 As String = ""
        Dim VenderAddress2 As String = ""
        Dim VenderPerson As String = ""
        Dim VenderPhone As String = ""
        Dim VenderMail As String = ""

        Dim FNDataBuyMonth As Integer = 0

        With CType(ogcpurchase.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _dtpurchase = .Copy
        End With

        PurchaseNo = ""
        DeliveryDate = ""
        DataSeason = ""
        StyleNo = ""


        _Cmd = "    Select TOP 1  FNMonth "
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B WITH(NOLOCK) "
        _Cmd &= vbCrLf & " Where (FNHSysBuyId = " & Val(FNHSysBuyId.Properties.Tag) & ") "

        FNDataBuyMonth = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

        For Each R As System.Data.DataRow In _dtpurchase.Select("FTStateSelect='1'", "FTPurchaseNo")


            PurchaseNo = R!FTPurchaseNo.ToString
            DeliveryDate = R!FDDeliveryDate.ToString


            _Cmd = " SELECT [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_Company('" & HI.UL.ULF.rpQuoted(PurchaseNo) & "') as DeliveryPlace "
            DeliveryPlace = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PUR, "")


            ShipAddress6 = ""
            ShipAddress7 = ""
            ShipAddress8 = ""
            ShipAddress9 = ""

            VenderName = ""
            VenderAddress1 = ""
            VenderAddress2 = ""
            VenderPerson = ""
            VenderPhone = ""
            VenderMail = ""

            Try

                ShipAddress6 = R!FTNickName.ToString()
                ShipAddress7 = R!FTTel.ToString()
                ShipAddress8 = R!FTEmail.ToString()
                ShipAddress9 = R!FTFax.ToString()
                VenderName = R!FTSuplName.ToString()
                VenderAddress1 = R!FTAddr1.ToString()
                VenderAddress2 = R!FTAddr2.ToString()
                VenderPerson = R!FTPerson.ToString()
                VenderPhone = R!FTPhone.ToString()
                VenderMail = R!FTMail1.ToString()

            Catch ex As Exception
            End Try

            _Cmd = "  Select FTRawMatColorCode"
            _Cmd &= vbCrLf & ",FTRawMatSizeCode"
            _Cmd &= vbCrLf & ",FTCusItemCodeRef"
            _Cmd &= vbCrLf & " ,MAX(FTVenderPramCode) As FTVenderPramCode"
            _Cmd &= vbCrLf & " ,MAX(FTStyleCode) AS FTStyleCode"
            _Cmd &= vbCrLf & ",MAX(FTSeasonCode) As FTSeasonCode"
            _Cmd &= vbCrLf & ",(FTPORef) AS FTPORef"
            _Cmd &= vbCrLf & " ,CASE WHEN MIN(CASE WHEN FTOgacDate ='' THEN '9999/99/99' ELSE FTOgacDate END) ='9999/99/99' THEN '' ELSE MIN(CASE WHEN FTOgacDate ='' THEN '9999/99/99' ELSE FTOgacDate END) END AS FTOgacDate"
            _Cmd &= vbCrLf & " ,SUM(X.FNQuantity)  As FNQuantity"
            _Cmd &= vbCrLf & ",SUM(X.FNOrderQuantity)  AS FNOrderQuantity"
            _Cmd &= vbCrLf & ",Min(FNDataSeq) As FNDataSeq "

            _Cmd &= vbCrLf & ",MAX(FTItemComboRef) As FTItemComboRef"
            _Cmd &= vbCrLf & ",MAX(FTStateRFID) As FTStateRFID"
            _Cmd &= vbCrLf & " ,MAX(FTBuyCode) AS FTBuyCode"
            _Cmd &= vbCrLf & " ,MAX(FTBuyName) AS FTBuyName"
            _Cmd &= vbCrLf & "FROM(SELECT A.* "
            _Cmd &= vbCrLf & "FROM(Select PH.FTPurchaseNo"
            _Cmd &= vbCrLf & " , PD.FNQuantity"
            _Cmd &= vbCrLf & ", PD.FTOrderNo"

            _Cmd &= vbCrLf & ",IM.FTRawMatCode "
            _Cmd &= vbCrLf & ", ISNULL(IMC.FTRawMatColorCode,'')  AS FTRawMatColorCode "
            _Cmd &= vbCrLf & ", ISNULL(IMS.FTRawMatSizeCode,'')  AS FTRawMatSizeCode "
            _Cmd &= vbCrLf & ",PD.FTOGacDate As FTPOOGacDate"
            _Cmd &= vbCrLf & ",ISNULL(XO.FTVenderPramCode,'') AS FTVenderPramCode"
            _Cmd &= vbCrLf & ",ISNULL(XO.FTStyleCode,'') AS FTStyleCode"
            _Cmd &= vbCrLf & " ,ISNULL(XO.FTSeasonCode,'') AS FTSeasonCode"
            _Cmd &= vbCrLf & ",ISNULL(MRP.FNQuantity,ISNULL(JOBMRPO.FNQuantity,0)) As FNOrderQuantity"
            _Cmd &= vbCrLf & "  , CASE WHEN ISNULL(MRPX.FDShipDateOrginal,'') ='' THEN ISNULL(MRP.FDShipDateOrginal,ISNULL(JOBMRPO.FDShipDateOrginal,'')) ELSE ISNULL(MRPX.FDShipDateOrginal,'') END AS FTOgacDate"
            _Cmd &= vbCrLf & " ,MM.FTCusItemCodeRef,ISNULL(MM.FTItemComboRef,'')  AS FTItemComboRef,ISNULL(MM.FTStateRFID,'0')  AS FTStateRFID"
            _Cmd &= vbCrLf & " ,ISNULL(Xo.FTPORef,'') AS FTPORef"
            _Cmd &= vbCrLf & " ,ISNULL(Xo.FTBuyCode,'') AS FTBuyCode"
            _Cmd &= vbCrLf & " ,ISNULL(Xo.FTBuyName,'') AS FTBuyName"
            _Cmd &= vbCrLf & " ,Row_number() Over (Order By MM.FTCusItemCodeRef ,IMC.FNRawMatColorSeq,IMS.FNRawMatSizeSeq) As FNDataSeq"
            _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH WITH(NOLOCK) INNER Join"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On PH.FTPurchaseNo = PD.FTPurchaseNo INNER Join"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM WITH(NOLOCK) On PD.FNHSysRawMatId = IM.FNHSysRawMatId INNER Join"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM With(NOLOCK) On IM.FTRawMatCode = MM.FTMainMatCode "
            _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As IMC With(NOLOCK) On IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
            _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As IMS With(NOLOCK) On IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId "

            _Cmd &= vbCrLf & "	  OUTER APPLY(Select  TOP 1 ISNULL(PGM.FTVenderPramCode,'') AS FTVenderPramCode"
            _Cmd &= vbCrLf & "  , ISNULL(ST.FTStyleCode,'') AS FTStyleCode"
            _Cmd &= vbCrLf & "  , ISNULL(SS.FTSeasonCode,'') AS FTSeasonCode"
            _Cmd &= vbCrLf & "  , ISNULL(XO.FTPORef,'') AS FTPORef"
            _Cmd &= vbCrLf & "  , ISNULL(BTU.FTBuyCode,'') AS FTBuyCode"
            _Cmd &= vbCrLf & "  , ISNULL(BTU.FTBuyNameEN,'') AS FTBuyName"
            _Cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_OrderProdAndSMPAll As XO With(NOLOCK) "
            _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram AS PGM WITH(NOLOCK) ON XO.FNHSysVenderPramId = PGM.FNHSysVenderPramId "
            _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On XO.FNHSysStyleId =ST.FNHSysStyleId "
            _Cmd &= vbCrLf & "   Left OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON XO.FNHSysSeasonId = SS.FNHSysSeasonId"
            _Cmd &= vbCrLf & "   Left OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS BTU WITH(NOLOCK) ON XO.FNHSysBuyId = BTU.FNHSysBuyId"
            _Cmd &= vbCrLf & "  WHERE  XO.FTOrderNo = PD.FTOrderNo          "
            _Cmd &= vbCrLf & "	) AS XO "
            _Cmd &= vbCrLf & "	OUTER APPLY("
            _Cmd &= vbCrLf & " SELECT SUM(MRP.FNQuantityTest + MRP.FNQuantityExtra + MRP.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "	,MIN(OS.FDShipDateOrginal) As FDShipDateOrginal"
            _Cmd &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS MRP WITH(NOLOCK) INNER Join"
            _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As OS With(NOLOCK) On MRP.FTOrderNo = OS.FTOrderNo And MRP.FTSubOrderNo = OS.FTSubOrderNo"
            _Cmd &= vbCrLf & "	Where (MRP.FTOrderNo = PD.FTOrderNo) And (MRP.FNHSysRawMatId = PD.FNHSysRawMatId)"
            _Cmd &= vbCrLf & "		) As MRP"

            _Cmd &= vbCrLf & "	OUTER APPLY("
            _Cmd &= vbCrLf & " SELECT TOP 1 MIN(OS.FDShipDateOrginal) As FDShipDateOrginal"
            _Cmd &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS MRP WITH(NOLOCK) INNER Join"
            _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As OS With(NOLOCK) On MRP.FTOrderNo = OS.FTOrderNo And MRP.FTSubOrderNo = OS.FTSubOrderNo"
            _Cmd &= vbCrLf & "	Where (MRP.FTOrderNo = PD.FTOrderNo) And (MRP.FNHSysRawMatId = PD.FNHSysRawMatId)  AND RIGHT(CASE WHEN ISDATE(OS.FDShipDateOrginal) = 1 THEN  Convert(nvarchar(10),convert(datetime,OS.FDShipDateOrginal),103) ELSE '' END ,LEN(CASE WHEN  ISNULL(PD.FTOGacDate,'')='' THEN '8899/88/99' ELSE   ISNULL(PD.FTOGacDate,'') END))  = PD.FTOGacDate   "
            _Cmd &= vbCrLf & "		) As MRPX"

            _Cmd &= vbCrLf & "	OUTER APPLY("
            _Cmd &= vbCrLf & " SELECT SUM(OSBD.FNGrandQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "	,MIN(OS.FDShipDateOrginal) As FDShipDateOrginal"
            _Cmd &= vbCrLf & "	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As OS With(NOLOCK) "
            _Cmd &= vbCrLf & "	       INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As OSBD With(NOLOCK) ON OS.FTOrderNo = OSBD.FTOrderNo AND OS.FTSubOrderNo = OSBD.FTSubOrderNo  "
            _Cmd &= vbCrLf & "	Where (OS.FTOrderNo = PD.FTOrderNo) AND OSBD.FTColorway = ISNULL(IMC.FTRawMatColorCode,'')  AND OSBD.FTColorway <> '' "
            _Cmd &= vbCrLf & "		) As JOBMRPO"

            _Cmd &= vbCrLf & " WHERE(PH.FTPurchaseNo = '" & HI.UL.ULF.rpQuoted(PurchaseNo) & "') "
            _Cmd &= vbCrLf & " ) As A "

            _Cmd &= vbCrLf & " ) AS X "
            _Cmd &= vbCrLf & " GROUP BY FTRawMatColorCode"
            _Cmd &= vbCrLf & "	       ,FTRawMatSizeCode"
            _Cmd &= vbCrLf & "	       ,FTCusItemCodeRef,FTPORef "

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then

                '  DataSeason = _dt.Rows(0)!FTSeasonCode.ToString
                'VenderName = R!FTSuplName.ToString()
                'VenderAddress1 = R!FTAddr1.ToString()
                'VenderAddress2 = R!FTAddr2.ToString()
                'VenderPerson = R!FTPerson.ToString()
                'VenderPhone = R!FTPhone.ToString()
                'VenderMail = R!FTMail1.ToString()

                opshet.BeginUpdate()
                Try
                    With opshet.ActiveWorksheet
                        .Rows(7).Item(0).Value = VenderName
                        .Rows(8).Item(0).Value = VenderAddress1
                        .Rows(9).Item(0).Value = VenderAddress2

                        .Rows(11).Item(0).Value = "Attn:  " & VenderPerson
                        .Rows(12).Item(0).Value = "T   " & VenderPhone
                        .Rows(13).Item(0).Value = "E-Mail:   " & VenderMail

                        .Rows(11).Item(8).Value = "Attn : " & ShipAddress6
                        .Rows(12).Item(8).Value = "Tel :  " & ShipAddress7
                        .Rows(13).Item(8).Value = "Fax : " & ShipAddress9
                        .Rows(14).Item(8).Value = "E-Mail : " & ShipAddress8
                        .Rows(17).Item(11).Value = DeliveryPlace
                        .Rows(18).Item(11).Value = DeliveryDate

                        Dim RowIndx As Integer = 0
                        Dim StartRow As Integer = 30
                        Dim Qty As Decimal = 0.00
                        Dim Amt As Decimal = 0.00
                        Dim TotalQty As Decimal = 0.00
                        For Each RXi As System.Data.DataRow In _dt.Select("FTCusItemCodeRef<>''", "FTRawMatColorCode,FNDataSeq")

                            Qty = Val(RXi!FNQuantity.ToString)

                            TotalQty = TotalQty + Qty

                            .Rows(StartRow + RowIndx).Insert()
                            .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)

                            'If .Rows(StartRow + RowIndx).Item(0).Value.ToString.ToLower() = "SPECIAL INSTRUCTION:".ToLower() Then
                            '    .Rows(StartRow + RowIndx).Insert()
                            '    .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)
                            'End If

                            .Rows(StartRow + RowIndx).Item(0).Value = PurchaseNo
                            .Rows(StartRow + RowIndx).Item(1).Value = RXi!FTVenderPramCode.ToString

                            Select Case RXi!FTVenderPramCode.ToString.Trim.ToLower()
                                Case "hic"
                                    .Rows(StartRow + RowIndx).Item(2).Value = "MADE IN CAMBODIA"
                                Case "htv"
                                    .Rows(StartRow + RowIndx).Item(2).Value = "MADE IN VIETNAM"
                                Case Else
                                    .Rows(StartRow + RowIndx).Item(2).Value = "MADE IN THAILAND"
                            End Select

                            .Rows(StartRow + RowIndx).Item(3).Value = RXi!FTPORef.ToString
                            .Rows(StartRow + RowIndx).Item(4).Value = RXi!FTStyleCode.ToString
                            .Rows(StartRow + RowIndx).Item(5).Value = RXi!FTRawMatColorCode.ToString
                            .Rows(StartRow + RowIndx).Item(6).Value = RXi!FTSeasonCode.ToString
                            .Rows(StartRow + RowIndx).Item(7).Value = "20" & Microsoft.VisualBasic.Right(RXi!FTSeasonCode.ToString, 2)
                            .Rows(StartRow + RowIndx).Item(8).Value = FNDataBuyMonth.ToString()
                            .Rows(StartRow + RowIndx).Item(9).Value = HI.UL.ULDate.ConvertEN(RXi!FTOgacDate.ToString)
                            .Rows(StartRow + RowIndx).Item(10).Value = Val(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(RXi!FTOgacDate.ToString, 7), 2))
                            .Rows(StartRow + RowIndx).Item(11).Value = RXi!FTRawMatSizeCode.ToString
                            .Rows(StartRow + RowIndx).Item(12).Value = Val(RXi!FNOrderQuantity.ToString)

                            If RXi!FTItemComboRef.ToString() <> "" Then

                                If _dt.Select("FTItemComboRef='" & HI.UL.ULF.rpQuoted(RXi!FTItemComboRef.ToString()) & "' AND FTCusItemCodeRef<>'" & HI.UL.ULF.rpQuoted(RXi!FTCusItemCodeRef.ToString()) & "'").Length > 0 Then
                                    .Rows(StartRow + RowIndx).Item(13).Value = RXi!FTItemComboRef.ToString()
                                End If

                            End If

                            .Rows(StartRow + RowIndx).Item(14).Value = Val(RXi!FTCusItemCodeRef.ToString)

                            If RXi!FTStateRFID.ToString() = "1" Then
                                .Rows(StartRow + RowIndx).Item(15).Value = "RFID"
                            Else
                                .Rows(StartRow + RowIndx).Item(15).Value = "NON-RFID"
                            End If

                            .Rows(StartRow + RowIndx).Item(16).Value = Qty


                            .Rows(StartRow + RowIndx).Item(17).Value = RXi!FTBuyName.ToString


                            RowIndx = RowIndx + 1

                        Next
                        .Rows(StartRow + RowIndx).Delete()
                        .Rows(StartRow + RowIndx).Delete()
                        .Rows(StartRow + RowIndx).Item(16).Value = TotalQty

                    End With
                Catch ex As Exception

                End Try

                opshet.EndUpdate()
                Dim FileName As String = _DefailtPath & "\" & PurchaseNo & ".xls"
                opshet.SaveDocument(FileName)

                Call LoadFormatFile()

            Else
            End If

        Next

        Return True
    End Function

    Private Function ExportEDI(ByRef FileNameRef As String) As Boolean
        Dim _Cmd As String = ""
        Dim _dt As System.Data.DataTable
        Dim _dtpurchase As System.Data.DataTable

        Dim _DateTimeFIleName As String = "EDI_" & DateTime.Now().ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_")
        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""
        Dim StyleNo As String = ""
        Dim DataSeason As String = ""
        Dim DeliveryPlace As String = ""

        Dim ShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim ShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim ShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim ShipAddress4 As String = "10140"
        Dim ShipAddress5 As String = "THAILAND"


        Dim ShipAddress6 As String = ""
        Dim ShipAddress7 As String = ""
        Dim ShipAddress8 As String = ""
        Dim ShipAddress9 As String = ""


        Dim VenderName As String = ""
        Dim VenderAddress1 As String = ""
        Dim VenderAddress2 As String = ""
        Dim VenderPerson As String = ""
        Dim VenderPhone As String = ""
        Dim VenderMail As String = ""
        Dim StartRow As Integer = 2
        Dim FNDataBuyMonth As Integer = 0
        Dim RowIndx As Integer = 0
        With CType(ogcpurchase.DataSource, System.Data.DataTable)
            .AcceptChanges()
            _dtpurchase = .Copy
        End With

        PurchaseNo = ""
        DeliveryDate = ""
        DataSeason = ""
        StyleNo = ""

        _Cmd = "    Select TOP 1  FNMonth "
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B WITH(NOLOCK) "
        _Cmd &= vbCrLf & " Where (FNHSysBuyId = " & Val(FNHSysBuyId.Properties.Tag) & ") "

        FNDataBuyMonth = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

        opshet.BeginUpdate()
        Try
            For Each R As System.Data.DataRow In _dtpurchase.Select("FTStateSelect='1'", "FTPurchaseNo")

                RowIndx = 0
                PurchaseNo = R!FTPurchaseNo.ToString
                DeliveryDate = R!FDDeliveryDate.ToString

                ShipAddress6 = ""
                ShipAddress7 = ""
                ShipAddress8 = ""
                ShipAddress9 = ""

                VenderName = ""
                VenderAddress1 = ""
                VenderAddress2 = ""
                VenderPerson = ""
                VenderPhone = ""
                VenderMail = ""

                Try

                    ShipAddress6 = R!FTNickName.ToString()
                    ShipAddress7 = R!FTTel.ToString()
                    ShipAddress8 = R!FTEmail.ToString()
                    ShipAddress9 = R!FTFax.ToString()
                    VenderName = R!FTSuplName.ToString()
                    VenderAddress1 = R!FTAddr1.ToString()
                    VenderAddress2 = R!FTAddr2.ToString()
                    VenderPerson = R!FTPerson.ToString()
                    VenderPhone = R!FTPhone.ToString()
                    VenderMail = R!FTMail1.ToString()

                Catch ex As Exception
                End Try

                _Cmd = "  Select FTPurchaseNo,FTRawMatColorCode"
                _Cmd &= vbCrLf & ",FTRawMatSizeCode"
                _Cmd &= vbCrLf & ",FTCusItemCodeRef"
                _Cmd &= vbCrLf & " ,MAX(FTVenderPramCode) As FTVenderPramCode"
                _Cmd &= vbCrLf & " ,MAX(FTStyleCode) AS FTStyleCode"
                _Cmd &= vbCrLf & ",MAX(FTSeasonCode) As FTSeasonCode"
                _Cmd &= vbCrLf & ",MAX(FTPORef) AS FTPORef,MIN(FTOgacDate) AS FTOgacDate"
                _Cmd &= vbCrLf & " ,SUM(X.FNQuantity)  As FNQuantity"
                _Cmd &= vbCrLf & ",SUM(X.FNOrderQuantity)  AS FNOrderQuantity"
                _Cmd &= vbCrLf & ",Min(FNDataSeq) As FNDataSeq "

                _Cmd &= vbCrLf & ",MAX(FTItemComboRef) As FTItemComboRef"
                _Cmd &= vbCrLf & ",MAX(FTStateRFID) As FTStateRFID"
                _Cmd &= vbCrLf & " ,MAX(FTBuyCode) AS FTBuyCode"
                _Cmd &= vbCrLf & " ,MAX(FTBuyName) AS FTBuyName"

                _Cmd &= vbCrLf & " ,MAX(FTCusLabelIMRef) AS FTCusLabelIMRef"
                _Cmd &= vbCrLf & " ,MAX(FTCusContentIMRef) AS FTCusContentIMRef"
                _Cmd &= vbCrLf & " ,MAX(FTCusCareIMRef) AS FTCusCareIMRef"
                _Cmd &= vbCrLf & " ,MAX(FTCusSizeMatrixIMRef) AS FTCusSizeMatrixIMRef"
                _Cmd &= vbCrLf & " ,MAX(FTPOOGacDate) AS FTPOOGacDate"

                _Cmd &= vbCrLf & " ,MAX(FTImportNoRef) AS FTImportNoRef"
                _Cmd &= vbCrLf & " ,MAX(FTStateYouth) AS FTStateYouth"
                _Cmd &= vbCrLf & " ,MAX(FTPORemark) AS FTPORemark"
                _Cmd &= vbCrLf & "FROM(SELECT A.* "
                _Cmd &= vbCrLf & "FROM(Select PH.FTPurchaseNo,PH.FTRemark AS FTPORemark"
                _Cmd &= vbCrLf & " , PD.FNQuantity"
                _Cmd &= vbCrLf & ", PD.FTOrderNo"

                _Cmd &= vbCrLf & ",IM.FTRawMatCode "
                _Cmd &= vbCrLf & ", ISNULL(IMC.FTRawMatColorCode,'')  AS FTRawMatColorCode "
                _Cmd &= vbCrLf & ", ISNULL(IMS.FTRawMatSizeCode,'')  AS FTRawMatSizeCode "
                _Cmd &= vbCrLf & ",PD.FTOGacDate As FTPOOGacDate"
                _Cmd &= vbCrLf & ",ISNULL(XO.FTVenderPramCode,'') AS FTVenderPramCode"
                _Cmd &= vbCrLf & ",ISNULL(XO.FTStyleCode,'') AS FTStyleCode"
                _Cmd &= vbCrLf & " ,ISNULL(XO.FTSeasonCode,'') AS FTSeasonCode"
                _Cmd &= vbCrLf & ",ISNULL(MRP.FNQuantity,0) As FNOrderQuantity"
                _Cmd &= vbCrLf & "  ,ISNULL(MRP.FDShipDateOrginal,'') AS FTOgacDate"
                _Cmd &= vbCrLf & " ,MM.FTCusItemCodeRef,ISNULL(MM.FTItemComboRef,'')  AS FTItemComboRef,ISNULL(MM.FTStateRFID,'0')  AS FTStateRFID"
                _Cmd &= vbCrLf & " ,ISNULL(Xo.FTPORef,'') AS FTPORef"
                _Cmd &= vbCrLf & " ,ISNULL(Xo.FTBuyCode,'') AS FTBuyCode"
                _Cmd &= vbCrLf & " ,ISNULL(Xo.FTBuyName,'') AS FTBuyName"
                _Cmd &= vbCrLf & " ,Row_number() Over (Order By MM.FTCusItemCodeRef ,IMC.FNRawMatColorSeq,IMS.FNRawMatSizeSeq) As FNDataSeq,MM.FTCusLabelIMRef ,MM.FTCusContentIMRef ,MM.FTCusCareIMRef ,MM.FTCusSizeMatrixIMRef,ISNULL(PD.FTOGacDate,'') AS FTPOGacDate ,ISNULL(MRP.FTImportNoRef,'') As FTImportNoRef,ISNULL(MRP.FTStateYouth,'') As FTStateYouth "
                _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH WITH(NOLOCK) INNER Join"
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On PH.FTPurchaseNo = PD.FTPurchaseNo INNER Join"
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM WITH(NOLOCK) On PD.FNHSysRawMatId = IM.FNHSysRawMatId INNER Join"
                _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM With(NOLOCK) On IM.FTRawMatCode = MM.FTMainMatCode "
                _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As IMC With(NOLOCK) On IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
                _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As IMS With(NOLOCK) On IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId "

                _Cmd &= vbCrLf & "	  OUTER APPLY(Select  TOP 1 ISNULL(PGM.FTVenderPramCode,'') AS FTVenderPramCode"
                _Cmd &= vbCrLf & "  , ISNULL(ST.FTStyleCode,'') AS FTStyleCode"
                _Cmd &= vbCrLf & "  , ISNULL(SS.FTSeasonCode,'') AS FTSeasonCode"
                _Cmd &= vbCrLf & "  , ISNULL(XO.FTPORef,'') AS FTPORef"
                _Cmd &= vbCrLf & "  , ISNULL(BTU.FTBuyCode,'') AS FTBuyCode"
                _Cmd &= vbCrLf & "  , ISNULL(BTU.FTBuyNameEN,'') AS FTBuyName"
                _Cmd &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As XO With(NOLOCK) "
                _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram AS PGM WITH(NOLOCK) ON XO.FNHSysVenderPramId = PGM.FNHSysVenderPramId "
                _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On XO.FNHSysStyleId =ST.FNHSysStyleId "
                _Cmd &= vbCrLf & "   Left OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON XO.FNHSysSeasonId = SS.FNHSysSeasonId"
                _Cmd &= vbCrLf & "   Left OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS BTU WITH(NOLOCK) ON XO.FNHSysBuyId = BTU.FNHSysBuyId"
                _Cmd &= vbCrLf & "  WHERE  XO.FTOrderNo = PD.FTOrderNo          "
                _Cmd &= vbCrLf & "	) AS XO "
                _Cmd &= vbCrLf & "	OUTER APPLY("
                _Cmd &= vbCrLf & " SELECT SUM(MRP.FNQuantityTest + MRP.FNQuantityExtra + MRP.FNQuantity) AS FNQuantity"
                _Cmd &= vbCrLf & "	,MIN(OS.FDShipDateOrginal) As FDShipDateOrginal,MAX(ISNULL(PVT.FTImportNoRef,'')) As FTImportNoRef,MAX(ISNULL(GDT.FTStateYouth,'')) AS FTStateYouth"
                _Cmd &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS MRP WITH(NOLOCK) INNER Join"
                _Cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As OS With(NOLOCK) On MRP.FTOrderNo = OS.FTOrderNo And MRP.FTSubOrderNo = OS.FTSubOrderNo"
                _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender As GDT With(NOLOCK) On OS.FNHSysGenderId = GDT.FNHSysGenderId "

                _Cmd &= vbCrLf & "   Left OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince As PVT With(NOLOCK) On OS.FNHSysProvinceId = PVT.FNHSysProvinceId "


                _Cmd &= vbCrLf & "	Where (MRP.FTOrderNo = PD.FTOrderNo) And (MRP.FNHSysRawMatId = PD.FNHSysRawMatId)"

                _Cmd &= vbCrLf & "		) As MRP"
                _Cmd &= vbCrLf & " WHERE(PH.FTPurchaseNo = '" & HI.UL.ULF.rpQuoted(PurchaseNo) & "') "
                _Cmd &= vbCrLf & " ) As A "
                _Cmd &= vbCrLf & " ) AS X "
                _Cmd &= vbCrLf & " GROUP BY FTPurchaseNo,FTRawMatColorCode"
                _Cmd &= vbCrLf & "	       ,FTRawMatSizeCode"
                _Cmd &= vbCrLf & "	       ,FTCusItemCodeRef "

                _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

                If _dt.Rows.Count > 0 Then


                    With opshet.ActiveWorksheet



                        RowIndx = 0
                        Dim Qty As Decimal = 0.00
                        Dim Amt As Decimal = 0.00
                        Dim TotalQty As Decimal = 0.00
                        For Each RXi As System.Data.DataRow In _dt.Select("FTCusItemCodeRef<>''", "FTRawMatColorCode,FNDataSeq")

                            Qty = Val(RXi!FNQuantity.ToString)

                            TotalQty = TotalQty + Qty

                            .Rows(StartRow + RowIndx).Insert()
                            .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)

                            .Rows(StartRow + RowIndx).Item(1).Value = RXi!FTVenderPramCode.ToString
                            .Rows(StartRow + RowIndx).Item(2).Value = RXi!FTSeasonCode.ToString
                            .Rows(StartRow + RowIndx).Item(3).Value = RXi!FTStyleCode.ToString
                            .Rows(StartRow + RowIndx).Item(4).Value = RXi!FTPORef.ToString
                            .Rows(StartRow + RowIndx).Item(6).Value = PurchaseNo

                            .Rows(StartRow + RowIndx).Item(8).Value = RXi!FTCusLabelIMRef.ToString
                            .Rows(StartRow + RowIndx).Item(9).Value = RXi!FTCusContentIMRef.ToString
                            .Rows(StartRow + RowIndx).Item(10).Value = RXi!FTCusCareIMRef.ToString
                            .Rows(StartRow + RowIndx).Item(11).Value = RXi!FTCusSizeMatrixIMRef.ToString

                            Select Case RXi!FTVenderPramCode.ToString.Trim.ToLower()
                                Case "hic"
                                    .Rows(StartRow + RowIndx).Item(12).Value = "MADE IN CAMBODIA"
                                Case "htv"
                                    .Rows(StartRow + RowIndx).Item(12).Value = "MADE IN VIETNAM"
                                Case Else
                                    .Rows(StartRow + RowIndx).Item(12).Value = "MADE IN THAILAND"
                            End Select


                            .Rows(StartRow + RowIndx).Item(13).Value = RXi!FTRawMatSizeCode.ToString
                            .Rows(StartRow + RowIndx).Item(14).Value = "Bulk"
                            .Rows(StartRow + RowIndx).Item(15).Value = Val(RXi!FNQuantity.ToString)
                            .Rows(StartRow + RowIndx).Item(16).Value = RXi!FTPOOGacDate.ToString

                            .Rows(StartRow + RowIndx).Item(28).Value = DeliveryDate
                            .Rows(StartRow + RowIndx).Item(36).Value = ShipAddress1
                            .Rows(StartRow + RowIndx).Item(37).Value = ShipAddress2 & " " & ShipAddress3 & " " & ShipAddress4 & " " & ShipAddress5

                            .Rows(StartRow + RowIndx).Item(39).Value = RXi!FTImportNoRef.ToString
                            .Rows(StartRow + RowIndx).Item(40).Value = ""


                            .Rows(StartRow + RowIndx).Item(41).Value = ShipAddress1
                            .Rows(StartRow + RowIndx).Item(42).Value = ShipAddress2 & " " & ShipAddress3 & " " & ShipAddress4 & " " & ShipAddress5
                            .Rows(StartRow + RowIndx).Item(43).Value = ShipAddress8
                            'Remark
                            .Rows(StartRow + RowIndx).Item(53).Value = RXi!FTPORemark.ToString
                            .Rows(StartRow + RowIndx).Item(54).Value = ShipAddress6
                            .Rows(StartRow + RowIndx).Item(55).Value = ShipAddress8

                            If RXi!FTStateYouth.ToString = "1" Then
                                .Rows(StartRow + RowIndx).Item(67).Value = "Y"
                            Else
                                .Rows(StartRow + RowIndx).Item(67).Value = "N"
                            End If

                            .Rows(StartRow + RowIndx).Item(68).Value = "N"


                            RowIndx = RowIndx + 1

                        Next


                    End With

                Else
                End If

                StartRow = StartRow + RowIndx

            Next
        Catch ex As Exception

        End Try


        opshet.EndUpdate()

        Dim FileName As String = _DefailtPath & "\" & _DateTimeFIleName & ".xls"

        FileNameRef = FileName

        opshet.SaveDocument(FileName)

        Call LoadFormatFile()


        Return True
    End Function

    Private Function ExportNIKEVAS() As Boolean

        Dim _Cmd As String = ""
        Dim ChkItemS As Integer = 21
        Dim ChkItemE As Integer = 80
        Dim ChkItemECfg As Integer = 0

        _Cmd = "select top 1  FTCfgData from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='POExportNIKEVAS' "
        ChkItemECfg = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SECURITY))

        If ChkItemECfg > 0 And ChkItemECfg > ChkItemE Then
            ChkItemE = ChkItemECfg
        End If

        Dim WriteS As Integer = ChkItemE + 8 '86
        Dim MaxItemPerPage As Integer = 22
        Dim ItemRef As String = ""
        Dim StateFoundItem As Boolean = False


        Dim _dt As System.Data.DataTable
        Dim _dtpurchase As System.Data.DataTable

        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""
        Dim StyleNo As String = ""
        Dim DataSeason As String = ""
        Dim DeliveryPlace As String = ""

        Dim ShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim ShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim ShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim ShipAddress4 As String = "10140"
        Dim ShipAddress5 As String = "THAILAND"

        Dim invoiceShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim invoiceShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim invoiceShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim invoiceShipAddress4 As String = "10140"
        Dim invoiceShipAddress5 As String = "THAILAND"

        Dim DeliveryAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim DeliveryAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim DeliveryAddress3 As String = "THOONGKRU, BANGKOK"
        Dim DeliveryAddress4 As String = "10140"
        Dim DeliveryAddress5 As String = "THAILAND"

        Dim AdressString As String = ""

        Dim ShipAddress6 As String = ""
        Dim ShipAddress7 As String = ""
        Dim ShipAddress8 As String = ""
        Dim ShipAddress9 As String = ""
        Dim StrItemChk As String = ""

        Dim VenderName As String = ""
        Dim VenderAddress1 As String = ""
        Dim VenderAddress2 As String = ""
        Dim VenderPerson As String = ""
        Dim VenderPhone As String = ""
        Dim VenderMail As String = ""
        Dim PoDate As String = ""
        Dim FNDataBuyMonth As Integer = 0
        Dim SoRef As String = ""
        Dim PoRef As String = ""
        Dim StyleRef As String = ""

        With CType(ogcpurchase.DataSource, System.Data.DataTable)

            .AcceptChanges()
            _dtpurchase = .Copy

        End With

        PurchaseNo = ""
        DeliveryDate = ""
        DataSeason = ""
        StyleNo = ""


        _Cmd = "    Select TOP 1  FNMonth "
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B WITH(NOLOCK) "
        _Cmd &= vbCrLf & " Where (FNHSysBuyId = " & Val(FNHSysBuyId.Properties.Tag) & ") "

        FNDataBuyMonth = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

        For Each R As System.Data.DataRow In _dtpurchase.Select("FTStateSelect='1'", "FTPurchaseNo")


            PurchaseNo = R!FTPurchaseNo.ToString
            DeliveryDate = R!FDDeliveryDate.ToString



            ShipAddress6 = ""
            ShipAddress7 = ""
            ShipAddress8 = ""
            ShipAddress9 = ""

            VenderName = ""
            VenderAddress1 = ""
            VenderAddress2 = ""
            VenderPerson = ""
            VenderPhone = ""
            VenderMail = ""
            PoDate = ""

            invoiceShipAddress1 = ""
            invoiceShipAddress2 = ""
            invoiceShipAddress3 = ""
            invoiceShipAddress4 = ""
            invoiceShipAddress5 = ""

            DeliveryAddress1 = ""
            DeliveryAddress2 = ""
            DeliveryAddress3 = ""
            DeliveryAddress4 = ""
            DeliveryAddress5 = ""

            Try

                ShipAddress6 = R!FTNickName.ToString()
                ShipAddress7 = R!FTTel.ToString()
                ShipAddress8 = R!FTEmail.ToString()
                ShipAddress9 = R!FTFax.ToString()

                VenderName = R!FTSuplName.ToString()
                VenderAddress1 = R!FTAddr1.ToString()
                VenderAddress2 = R!FTAddr2.ToString()
                VenderPerson = R!FTPerson.ToString()
                VenderPhone = R!FTPhone.ToString()
                VenderMail = R!FTMail1.ToString()


                PoDate = R!FDPurchaseDate.ToString


            Catch ex As Exception
            End Try


            _Cmd = "  Select TOP 1  FTPurchaseNo "
            _Cmd &= vbCrLf & ",H.FNHSysCmpId"
            _Cmd &= vbCrLf & ",H.FNHSysCmpPOId  "
            _Cmd &= vbCrLf & ",C1.FTCmpCode "
            _Cmd &= vbCrLf & ",C1.FTCmpName "
            _Cmd &= vbCrLf & ",C1.FTAddressInvoice "
            _Cmd &= vbCrLf & ",C2.FTCmpCode2 "
            _Cmd &= vbCrLf & ",C2.FTCmpName2 "
            _Cmd &= vbCrLf & ",C2.FTAddressInvoice2 "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_POHeaderCMP As H "
            _Cmd &= vbCrLf & "  Outer apply(select top 1 C1.FTCmpNameEN AS FTCmpName  ,C1.FTCmpCode "
            _Cmd &= vbCrLf & ",C1.FTAddressInvoiceEN As FTAddressInvoice "
            _Cmd &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  AS C1 WITH(NOLOCK) Where C1.FNHSysCmpId = H.FNHSysCmpId) As C1 "
            _Cmd &= vbCrLf & "  Outer apply(select top 1 C2.FTCmpNameEN AS FTCmpName2,C2.FTCmpCode  AS FTCmpCode2 "
            _Cmd &= vbCrLf & "	,C2.FTAddressInvoiceEN As FTAddressInvoice2 "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  AS C2  WITH(NOLOCK)  Where C2.FNHSysCmpId = H.FNHSysCmpPOId) As C2 "
            _Cmd &= vbCrLf & " WHERE(FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(PurchaseNo) & "') "

            Dim dtInvoiceCmp As System.Data.DataTable

            dtInvoiceCmp = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            For Each Rsd As System.Data.DataRow In dtInvoiceCmp.rows


                If Rsd!FTCmpCode2.ToString = "HT91" Or Rsd!FTCmpCode2.ToString = "" Then
                    invoiceShipAddress1 = ShipAddress1
                    invoiceShipAddress2 = ShipAddress2
                    invoiceShipAddress3 = ShipAddress3
                    invoiceShipAddress4 = ShipAddress4
                    invoiceShipAddress5 = ShipAddress5

                Else
                    invoiceShipAddress1 = Rsd!FTCmpName2.ToString
                    AdressString = Rsd!FTAddressInvoice2.ToString

                    If AdressString <> "" Then

                        For I As Integer = 0 To AdressString.Split(",").Length - 1
                            Select Case I
                                Case 0, 1
                                    If invoiceShipAddress2 <> "" Then
                                        invoiceShipAddress2 = invoiceShipAddress2 & "," & AdressString.Split(",")(I)
                                    Else
                                        invoiceShipAddress2 = AdressString.Split(",")(I)
                                    End If

                                Case 2
                                    invoiceShipAddress3 = AdressString.Split(",")(I)
                                Case 3
                                    invoiceShipAddress4 = AdressString.Split(",")(I)
                                Case Else

                                    If invoiceShipAddress5 <> "" Then
                                        invoiceShipAddress5 = invoiceShipAddress5 & "," & AdressString.Split(",")(I)
                                    Else
                                        invoiceShipAddress5 = AdressString.Split(",")(I)
                                    End If

                            End Select
                        Next


                    Else
                        invoiceShipAddress1 = ShipAddress1
                        invoiceShipAddress2 = ShipAddress2
                        invoiceShipAddress3 = ShipAddress3
                        invoiceShipAddress4 = ShipAddress4
                        invoiceShipAddress5 = ShipAddress5
                    End If

                End If



                If Rsd!FTCmpCode.ToString = "HT91" Or Rsd!FTCmpCode.ToString = "" Then
                    DeliveryAddress1 = ShipAddress1
                    DeliveryAddress2 = ShipAddress2
                    DeliveryAddress3 = ShipAddress3
                    DeliveryAddress4 = ShipAddress4
                    DeliveryAddress5 = ShipAddress5

                Else
                    DeliveryAddress1 = Rsd!FTCmpName2.ToString
                    AdressString = Rsd!FTAddressInvoice2.ToString

                    If AdressString <> "" Then

                        For I As Integer = 0 To AdressString.Split(",").Length - 1
                            Select Case I
                                Case 0, 1
                                    If DeliveryAddress2 <> "" Then
                                        DeliveryAddress2 = DeliveryAddress2 & "," & AdressString.Split(",")(I)
                                    Else
                                        DeliveryAddress2 = AdressString.Split(",")(I)
                                    End If

                                Case 2
                                    DeliveryAddress3 = AdressString.Split(",")(I)
                                Case 3
                                    DeliveryAddress4 = AdressString.Split(",")(I)
                                Case Else

                                    If DeliveryAddress5 <> "" Then
                                        DeliveryAddress5 = DeliveryAddress5 & "," & AdressString.Split(",")(I)
                                    Else
                                        DeliveryAddress5 = AdressString.Split(",")(I)
                                    End If


                            End Select
                        Next


                    Else
                        DeliveryAddress1 = ShipAddress1
                        DeliveryAddress2 = ShipAddress2
                        DeliveryAddress3 = ShipAddress3
                        DeliveryAddress4 = ShipAddress4
                        DeliveryAddress5 = ShipAddress5
                    End If

                End If


            Next



            _Cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_EXPORT_PONIKEVAS '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(PurchaseNo) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then

                Dim grp As List(Of String) = (_dt.Select("FTPage<>''", "FTPage").CopyToDataTable).AsEnumerable() _
                                                          .Select(Function(rz) rz.Field(Of String)("FTPage")) _
                                                          .Distinct() _
                                                          .ToList()


                For Each Rpage As String In grp

                    If StateFoundItem Then
                        Call LoadFormatFile()
                    End If

                    ItemRef = ""

                    If Rpage.Split("-Page").Length > 0 Then
                        ItemRef = Rpage.Split("-Page")(0)
                    Else
                        ItemRef = Rpage
                    End If

                    StateFoundItem = False


                    opshet.BeginUpdate()
                    Try
                        With opshet.ActiveWorksheet


                            For I As Integer = ChkItemS To ChkItemE
                                StrItemChk = .Rows(I).Item(1).Value.ToString()

                                If StrItemChk = ItemRef Then


                                    .Rows(I).Item(15).Value = "X"

                                    StateFoundItem = True
                                    Exit For
                                End If
                            Next

                            If StateFoundItem Then

                                .Rows(3).Item(3).Value = PoDate
                                .Rows(5).Item(3).Value = PurchaseNo

                                'Bill To
                                .Rows(7).Item(2).Value = invoiceShipAddress1
                                .Rows(8).Item(2).Value = invoiceShipAddress2
                                .Rows(9).Item(2).Value = invoiceShipAddress3
                                .Rows(10).Item(2).Value = invoiceShipAddress4 & " " & invoiceShipAddress5
                                .Rows(11).Item(2).Value = ShipAddress6
                                .Rows(12).Item(2).Value = ShipAddress7
                                .Rows(13).Item(2).Value = ShipAddress9


                                'Ship To
                                .Rows(7).Item(13).Value = DeliveryAddress1
                                .Rows(8).Item(13).Value = DeliveryAddress2
                                .Rows(9).Item(13).Value = DeliveryAddress3
                                .Rows(10).Item(13).Value = DeliveryAddress4 & " " & DeliveryAddress5
                                .Rows(11).Item(13).Value = ShipAddress6
                                .Rows(12).Item(13).Value = ShipAddress7
                                .Rows(13).Item(13).Value = ShipAddress9

                                Dim RowIndx As Integer = 0


                                For Each RXi As System.Data.DataRow In _dt.Select("FTPage='" & Rpage & "'", "Seq")

                                    SoRef = RXi!SO_NUMBER.ToString & IIf(RXi!SO_ITEM.ToString <> "", "-" & RXi!SO_ITEM.ToString, "")
                                    PoRef = RXi!FTPOref.ToString & IIf(RXi!FTNikePOLineItem.ToString <> "", "-" & RXi!FTNikePOLineItem.ToString, "")
                                    StyleRef = RXi!FTStyleCode.ToString & "-" & RXi!FTMatColorCode.ToString

                                    .Rows(WriteS + RowIndx).Item(1).Value = SoRef
                                    .Rows(WriteS + RowIndx).Item(8).Value = PoRef
                                    .Rows(WriteS + RowIndx).Item(13).Value = RXi!FTStyleCode.ToString & "-" & RXi!FTMatColorCode.ToString
                                    .Rows(WriteS + RowIndx).Item(15).Value = RXi!FTMatSizeCode.ToString
                                    .Rows(WriteS + RowIndx).Item(17).Value = Val(RXi!FNPRQuantity.ToString)
                                    RowIndx = RowIndx + 1

                                Next

                            End If

                        End With
                    Catch ex As Exception
                        Dim Exmsg As String = ex.Message

                    End Try

                    opshet.EndUpdate()

                    If StateFoundItem Then
                        Dim FileName As String = _DefailtPath & "\" & PurchaseNo & "-" & Rpage & ".xlsx"
                        opshet.SaveDocument(FileName)
                    End If

                Next




                Call LoadFormatFile()

            Else
            End If

        Next

        Return True
    End Function
    Private Function ExportPOAPL() As Boolean

        Dim _Cmd As String = ""



        Dim ItemRef As String = ""
        Dim StateFoundItem As Boolean = False


        Dim _dt As System.Data.DataTable
        Dim _dtpurchase As System.Data.DataTable

        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""
        Dim StyleNo As String = ""
        Dim DataSeason As String = ""
        Dim DeliveryPlace As String = ""

        Dim ShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim ShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim ShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim ShipAddress4 As String = "10140"
        Dim ShipAddress5 As String = "THAILAND"

        Dim invoiceShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim invoiceShipAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim invoiceShipAddress3 As String = "THOONGKRU, BANGKOK"
        Dim invoiceShipAddress4 As String = "10140"
        Dim invoiceShipAddress5 As String = "THAILAND"

        Dim DeliveryAddress1 As String = "HI-TECH APPAREL CO., LTD"
        Dim DeliveryAddress2 As String = "328 PRACHAUTHIT RD.,"
        Dim DeliveryAddress3 As String = "THOONGKRU, BANGKOK"
        Dim DeliveryAddress4 As String = "10140"
        Dim DeliveryAddress5 As String = "THAILAND"

        Dim AdressString As String = ""

        Dim ShipAddress6 As String = ""
        Dim ShipAddress7 As String = ""
        Dim ShipAddress8 As String = ""
        Dim ShipAddress9 As String = ""
        Dim StrItemChk As String = ""

        Dim VenderName As String = ""
        Dim VenderAddress1 As String = ""
        Dim VenderAddress2 As String = ""
        Dim VenderPerson As String = ""
        Dim VenderPhone As String = ""
        Dim VenderMail As String = ""
        Dim PoDate As String = ""
        Dim FNDataBuyMonth As Integer = 0
        Dim SoRef As String = ""
        Dim PoRef As String = ""
        Dim StyleRef As String = ""

        With CType(ogcpurchase.DataSource, System.Data.DataTable)

            .AcceptChanges()
            _dtpurchase = .Copy

        End With

        PurchaseNo = ""
        DeliveryDate = ""
        DataSeason = ""
        StyleNo = ""


        _Cmd = "    Select TOP 1  FNMonth "
        _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS B WITH(NOLOCK) "
        _Cmd &= vbCrLf & " Where (FNHSysBuyId = " & Val(FNHSysBuyId.Properties.Tag) & ") "

        FNDataBuyMonth = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

        For Each R As System.Data.DataRow In _dtpurchase.Select("FTStateSelect='1'", "FTPurchaseNo")


            PurchaseNo = R!FTPurchaseNo.ToString
            DeliveryDate = R!FDDeliveryDate.ToString



            ShipAddress6 = ""
            ShipAddress7 = ""
            ShipAddress8 = ""
            ShipAddress9 = ""

            VenderName = ""
            VenderAddress1 = ""
            VenderAddress2 = ""
            VenderPerson = ""
            VenderPhone = ""
            VenderMail = ""
            PoDate = ""

            invoiceShipAddress1 = ""
            invoiceShipAddress2 = ""
            invoiceShipAddress3 = ""
            invoiceShipAddress4 = ""
            invoiceShipAddress5 = ""

            DeliveryAddress1 = ""
            DeliveryAddress2 = ""
            DeliveryAddress3 = ""
            DeliveryAddress4 = ""
            DeliveryAddress5 = ""

            Try

                ShipAddress6 = R!FTNickName.ToString()
                ShipAddress7 = R!FTTel.ToString()
                ShipAddress8 = R!FTEmail.ToString()
                ShipAddress9 = R!FTFax.ToString()

                VenderName = R!FTSuplName.ToString()
                VenderAddress1 = R!FTAddr1.ToString()
                VenderAddress2 = R!FTAddr2.ToString()
                VenderPerson = R!FTPerson.ToString()
                VenderPhone = R!FTPhone.ToString()
                VenderMail = R!FTMail1.ToString()


                PoDate = R!FDPurchaseDate.ToString


            Catch ex As Exception
            End Try


            _Cmd = "  Select TOP 1  FTPurchaseNo "
            _Cmd &= vbCrLf & ",H.FNHSysCmpId"
            _Cmd &= vbCrLf & ",H.FNHSysCmpPOId  "
            _Cmd &= vbCrLf & ",C1.FTCmpCode "
            _Cmd &= vbCrLf & ",C1.FTCmpName "
            _Cmd &= vbCrLf & ",C1.FTAddressInvoice "
            _Cmd &= vbCrLf & ",C2.FTCmpCode2 "
            _Cmd &= vbCrLf & ",C2.FTCmpName2 "
            _Cmd &= vbCrLf & ",C2.FTAddressInvoice2 "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_POHeaderCMP As H "
            _Cmd &= vbCrLf & "  Outer apply(select top 1 C1.FTCmpNameEN AS FTCmpName  ,C1.FTCmpCode "
            _Cmd &= vbCrLf & ",C1.FTAddressInvoiceEN As FTAddressInvoice "
            _Cmd &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  AS C1 WITH(NOLOCK) Where C1.FNHSysCmpId = H.FNHSysCmpId) As C1 "
            _Cmd &= vbCrLf & "  Outer apply(select top 1 C2.FTCmpNameEN AS FTCmpName2,C2.FTCmpCode  AS FTCmpCode2 "
            _Cmd &= vbCrLf & "	,C2.FTAddressInvoiceEN As FTAddressInvoice2 "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  AS C2  WITH(NOLOCK)  Where C2.FNHSysCmpId = H.FNHSysCmpPOId) As C2 "
            _Cmd &= vbCrLf & " WHERE(FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(PurchaseNo) & "') "

            Dim dtInvoiceCmp As System.Data.DataTable

            dtInvoiceCmp = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            For Each Rsd As System.Data.DataRow In dtInvoiceCmp.Rows


                If Rsd!FTCmpCode2.ToString = "HT91" Or Rsd!FTCmpCode2.ToString = "" Then
                    invoiceShipAddress1 = ShipAddress1
                    invoiceShipAddress2 = ShipAddress2
                    invoiceShipAddress3 = ShipAddress3
                    invoiceShipAddress4 = ShipAddress4
                    invoiceShipAddress5 = ShipAddress5

                Else
                    invoiceShipAddress1 = Rsd!FTCmpName2.ToString
                    AdressString = Rsd!FTAddressInvoice2.ToString

                    If AdressString <> "" Then

                        For I As Integer = 0 To AdressString.Split(",").Length - 1
                            Select Case I
                                Case 0, 1
                                    If invoiceShipAddress2 <> "" Then
                                        invoiceShipAddress2 = invoiceShipAddress2 & "," & AdressString.Split(",")(I)
                                    Else
                                        invoiceShipAddress2 = AdressString.Split(",")(I)
                                    End If

                                Case 2
                                    invoiceShipAddress3 = AdressString.Split(",")(I)
                                Case 3
                                    invoiceShipAddress4 = AdressString.Split(",")(I)
                                Case Else

                                    If invoiceShipAddress5 <> "" Then
                                        invoiceShipAddress5 = invoiceShipAddress5 & "," & AdressString.Split(",")(I)
                                    Else
                                        invoiceShipAddress5 = AdressString.Split(",")(I)
                                    End If

                            End Select
                        Next


                    Else
                        invoiceShipAddress1 = ShipAddress1
                        invoiceShipAddress2 = ShipAddress2
                        invoiceShipAddress3 = ShipAddress3
                        invoiceShipAddress4 = ShipAddress4
                        invoiceShipAddress5 = ShipAddress5
                    End If

                End If



                If Rsd!FTCmpCode.ToString = "HT91" Or Rsd!FTCmpCode.ToString = "" Then
                    DeliveryAddress1 = ShipAddress1
                    DeliveryAddress2 = ShipAddress2
                    DeliveryAddress3 = ShipAddress3
                    DeliveryAddress4 = ShipAddress4
                    DeliveryAddress5 = ShipAddress5

                Else
                    DeliveryAddress1 = Rsd!FTCmpName2.ToString
                    AdressString = Rsd!FTAddressInvoice2.ToString

                    If AdressString <> "" Then

                        For I As Integer = 0 To AdressString.Split(",").Length - 1
                            Select Case I
                                Case 0, 1
                                    If DeliveryAddress2 <> "" Then
                                        DeliveryAddress2 = DeliveryAddress2 & "," & AdressString.Split(",")(I)
                                    Else
                                        DeliveryAddress2 = AdressString.Split(",")(I)
                                    End If

                                Case 2
                                    DeliveryAddress3 = AdressString.Split(",")(I)
                                Case 3
                                    DeliveryAddress4 = AdressString.Split(",")(I)
                                Case Else

                                    If DeliveryAddress5 <> "" Then
                                        DeliveryAddress5 = DeliveryAddress5 & "," & AdressString.Split(",")(I)
                                    Else
                                        DeliveryAddress5 = AdressString.Split(",")(I)
                                    End If


                            End Select
                        Next


                    Else
                        DeliveryAddress1 = ShipAddress1
                        DeliveryAddress2 = ShipAddress2
                        DeliveryAddress3 = ShipAddress3
                        DeliveryAddress4 = ShipAddress4
                        DeliveryAddress5 = ShipAddress5
                    End If

                End If


            Next



            _Cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_EXPORT_POAPL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(PurchaseNo) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then

                Dim grp As List(Of String) = (_dt.Select("FTPage<>''", "FTPage").CopyToDataTable).AsEnumerable() _
                                                          .Select(Function(rz) rz.Field(Of String)("FTPage")) _
                                                          .Distinct() _
                                                          .ToList()

                Dim NikePOLine As String = ""
                Dim NikeCustPO As String = ""
                Dim ColorWayPO As String = ""

                For Each Rpage As String In grp

                    If StateFoundItem Then
                        Call LoadFormatFile()
                    End If

                    ItemRef = ""



                    StateFoundItem = False


                    opshet.BeginUpdate()
                    Try
                        With opshet.ActiveWorksheet

                            StateFoundItem = True

                            For Each RXi As System.Data.DataRow In _dt.Select("FTPage='" & Rpage & "'", "Seq")
                                NikePOLine = RXi!FTNikePOLineItem.ToString
                                NikeCustPO = RXi!SO_NUMBER.ToString
                                ColorWayPO = RXi!FTMatColorCode.ToString
                                Exit For
                            Next

                            If StateFoundItem Then

                                .Rows(5).Item(14).Value = PurchaseNo
                                .Rows(6).Item(14).Value = PoDate
                                .Rows(7).Item(14).Value = DeliveryDate

                                'Bill To
                                .Rows(10).Item(3).Value = invoiceShipAddress1
                                .Rows(11).Item(3).Value = invoiceShipAddress2
                                .Rows(12).Item(3).Value = invoiceShipAddress3
                                .Rows(13).Item(3).Value = invoiceShipAddress4 & " " & invoiceShipAddress5

                                .Rows(15).Item(3).Value = ShipAddress6
                                .Rows(16).Item(3).Value = ShipAddress8
                                .Rows(17).Item(3).Value = ShipAddress7
                                .Rows(18).Item(3).Value = ShipAddress9


                                'Ship To
                                .Rows(10).Item(10).Value = DeliveryAddress1
                                .Rows(11).Item(10).Value = DeliveryAddress2
                                .Rows(12).Item(10).Value = DeliveryAddress3
                                .Rows(13).Item(10).Value = DeliveryAddress4 & " " & DeliveryAddress5

                                .Rows(15).Item(10).Value = ShipAddress6
                                .Rows(16).Item(10).Value = ShipAddress8
                                .Rows(17).Item(10).Value = ShipAddress7
                                .Rows(18).Item(10).Value = ShipAddress9

                                .Rows(24).Item(12).Value = NikeCustPO

                                .Rows(29).Item(1).Value = "COL." & ColorWayPO
                                .Rows(33).Item(1).Value = "COL." & ColorWayPO
                                .Rows(37).Item(1).Value = "COL." & ColorWayPO


                                Dim FirstCount As Boolean = True
                                Dim PrintCount As Boolean = False


                                Dim MaxCol As Integer = 5
                                For Each RXi As System.Data.DataRow In _dt.Select("FTPage='" & Rpage & "' AND FNPOItemSeq =1", "FNMatSizeSeq")

                                    PrintCount = False
                                    Dim I As Integer = 0
                                    For I = 2 To MaxCol
                                        If .Rows(28).Item(I).DisplayText.ToUpper = RXi!FTMatSizeCode.ToString.Trim.ToUpper Then
                                            PrintCount = True
                                            .Rows(29).Item(I).Value = Integer.Parse(Val(RXi!FNPRQuantity.ToString))

                                        End If
                                    Next

                                    If PrintCount = False Then
                                        MaxCol = MaxCol + 1
                                        .Rows(28).Item(MaxCol).Value = RXi!FTMatSizeCode.ToString.Trim.ToUpper
                                        .Rows(29).Item(MaxCol).Value = Integer.Parse(Val(RXi!FNPRQuantity.ToString))

                                    End If

                                Next

                                FirstCount = True
                                MaxCol = 5
                                For Each RXi As System.Data.DataRow In _dt.Select("FTPage='" & Rpage & "' AND FNPOItemSeq =2", "FNMatSizeSeq")

                                    PrintCount = False
                                    Dim I As Integer = 0
                                    For I = 2 To MaxCol
                                        If .Rows(32).Item(I).DisplayText.ToUpper = RXi!FTMatSizeCode.ToString.Trim.ToUpper Then
                                            PrintCount = True
                                            .Rows(33).Item(I).Value = Integer.Parse(Val(RXi!FNPRQuantity.ToString))

                                        End If
                                    Next

                                    If PrintCount = False Then
                                        MaxCol = MaxCol + 1
                                        .Rows(32).Item(MaxCol).Value = RXi!FTMatSizeCode.ToString.Trim.ToUpper
                                        .Rows(33).Item(MaxCol).Value = Integer.Parse(Val(RXi!FNPRQuantity.ToString))

                                    End If

                                Next

                                FirstCount = True
                                MaxCol = 5
                                For Each RXi As System.Data.DataRow In _dt.Select("FTPage='" & Rpage & "' AND FNPOItemSeq =3", "FNMatSizeSeq")

                                    PrintCount = False
                                    Dim I As Integer = 0
                                    For I = 2 To MaxCol
                                        If .Rows(36).Item(I).DisplayText.ToUpper = RXi!FTMatSizeCode.ToString.Trim.ToUpper Then
                                            PrintCount = True
                                            .Rows(37).Item(I).Value = Integer.Parse(Val(RXi!FNPRQuantity.ToString))

                                        End If
                                    Next

                                    If PrintCount = False Then
                                        MaxCol = MaxCol + 1
                                        .Rows(36).Item(MaxCol).Value = RXi!FTMatSizeCode.ToString.Trim.ToUpper
                                        .Rows(37).Item(MaxCol).Value = Integer.Parse(Val(RXi!FNPRQuantity.ToString))

                                    End If

                                Next

                            End If

                        End With
                    Catch ex As Exception
                        Dim Exmsg As String = ex.Message

                    End Try

                    opshet.EndUpdate()

                    If StateFoundItem Then
                        Dim FileName As String = _DefailtPath & "\" & PurchaseNo & "-LINE " & NikePOLine & ".xlsx"
                        opshet.SaveDocument(FileName)
                    End If

                Next




                Call LoadFormatFile()

            Else
            End If

        Next

        Return True
    End Function

    Private Sub FNExportPOSuplFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNExportPOSuplFormat.SelectedIndexChanged
        Dim _Pls As New HI.TL.SplashScreen("Reading...Format File Please Wait...")

        Call LoadFormatFile()

        _Pls.Close()
    End Sub

    Private Sub LoadFormatFile()

        Dim _FileName As String = System.Windows.Forms.Application.StartupPath & "\ExportPOFormat\"

        Select Case FNExportPOSuplFormat.SelectedIndex
            Case 0
                _FileName = _FileName & "Tagtime order form.xls"
            Case 1
                _FileName = _FileName & "Maxim order form.xlsx"
            Case 2
                _FileName = _FileName & "ExportEDI.xls"
            Case 3
                _FileName = _FileName & "PONikeVasOrderForms.xlsx"
            Case 4
                _FileName = _FileName & "POAPLorderform.xlsx"
        End Select

        Try

            Dim proc = Process.GetProcessesByName("excel")

            For i As Integer = 0 To proc.Count - 1
                proc(i).Kill()
            Next i

        Catch ex As Exception
        End Try

        Select Case Path.GetExtension(_FileName)
            Case ".xls"
                opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

            Case ".xlsx"
                opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

            Case ".xlsm"
                opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

            Case Else
                opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

        End Select
    End Sub
End Class