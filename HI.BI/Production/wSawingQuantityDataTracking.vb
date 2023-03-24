Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wSawingQuantityDataTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT   '0' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "

        Else
            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub

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


    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPO.Text <> "" And FTCustomerPO.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPOTo.Text <> "" And FTCustomerPOTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartDateScan.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndDateScan.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

    Private Function LoadDataInfo(Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Qry As String = ""

            Spls.UpdateInformation("Loading.... Data Company   Please wait....")

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            Dim dtdata As DataTable = Nothing
            Dim dtdatadetail As DataTable = Nothing

            Dim _ServerName, _UID, _PWS, _DBName As String
            Dim _ConnectString As String = ""
            Dim _FNHSysCmpId As Integer = 0
            Dim CmpCode As String = ""
            For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

                _FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
                CmpCode = R!FTCmpCode.ToString()

                If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then

                    _ServerName = R!FTIPServer.ToString
                    _UID = HI.Conn.DB.UIDName
                    _PWS = HI.Conn.DB.PWDName
                    _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                    _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                    Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")

                    Try

                        Dim _dtdetail As New DataTable

                        _Qry = "  "
                        _Qry &= vbCrLf & "  delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabData  where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "  delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataBD  where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                        _Qry &= vbCrLf & "  delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataSewOut  where FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                        _Qry &= vbCrLf & "   DECLARE @TabOrder AS TABLE([FTOrderNo] [nvarchar](30) NULL  Unique nonclustered (FTOrderNo) ) "


                        '_Qry &= vbCrLf & " DECLARE @TabDataBD AS TABLE([FTOrderNo] [nvarchar](30) NULL,[FTSubOrderNo] [nvarchar](30) NULL,	"
                        '_Qry &= vbCrLf & "  [FTColorway] [varchar](30) NULL,"
                        '_Qry &= vbCrLf & "  [FTSizeBreakDown] [varchar](30) NULL,"
                        '_Qry &= vbCrLf & "  [FNScanQuantity] [Int] NULL,  "
                        '_Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & "  [FTCustPO] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & "  [FTBarcodeNo] [nvarchar](30) NULL,  [Indx] [Int] NULL Unique nonclustered ([FTOrderNo],FTSubOrderNo,[FTColorway],[FTSizeBreakDown],FTPOLineItemNo,FTBarcodeNo,Indx) "
                        '_Qry &= vbCrLf & "  ) "

                        '_Qry &= vbCrLf & " DECLARE @TabData AS TABLE([FTOrderNo] [nvarchar](30) NULL,[FTSubOrderNo] [nvarchar](30) NULL,				 "
                        '_Qry &= vbCrLf & "   [FTUnitSectCode] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
                        '_Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
                        '_Qry &= vbCrLf & "   [FNScanQuantity] [Int] NULL,               "
                        '_Qry &= vbCrLf & "   [FTPOLineItemNo] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,  [Indx] [Int] NULL Unique nonclustered ([FTOrderNo],FTSubOrderNo,[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo],Indx) "
                        '_Qry &= vbCrLf & "   ) "


                        '_Qry &= vbCrLf & " DECLARE @TabDataSewOut AS TABLE([FTOrderNo] [nvarchar](30) NULL,[FTSubOrderNo] [nvarchar](30) NULL,			 "
                        '_Qry &= vbCrLf & "  [FTUnitSectCode] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & "   [FTColorway] [varchar](30) NULL,"
                        '_Qry &= vbCrLf & "   [FTSizeBreakDown] [varchar](30) NULL,"
                        '_Qry &= vbCrLf & "   [FNScanQuantity] [Int] NULL,               "
                        '_Qry &= vbCrLf & "  [FTPOLineItemNo] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & "  [FTCustPO] [nvarchar](30) NULL,"
                        '_Qry &= vbCrLf & " [FTBarcodeNo] [nvarchar](30) NULL,  [Indx] [Int] NULL Unique nonclustered ([FTOrderNo],FTSubOrderNo,[FTUnitSectCode],[FTColorway],[FTSizeBreakDown],[FTPOLineItemNo],Indx) "
                        '_Qry &= vbCrLf & "   ) "

                        _Qry &= vbCrLf & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabData (FTUserLogIn,FTOrderNo,FTSubOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNScanQuantity, FTPOLineItemNo, FTBarcodeNo,Indx )"

                        Select Case True


                            Case (FTStartDateScan.Text <> "" Or FTEndDateScan.Text <> "")
                                _Qry &= vbCrLf & " Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', T.FTOrderNo,T.FTSubOrderNo,T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,T.FTPOLineItemNo ,T.FTBarcodeNo ,ROW_NUMBER() Over (Order by  T.FTOrderNo) AS Indx   "

                                _Qry &= vbCrLf & " From("
                                _Qry &= vbCrLf & "  Select  SC.FTOrderNo,SC.FTSubOrderNo,'' FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) As FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeNo"
                                _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS SC WITH(NOLOCK)"
                                _Qry &= vbCrLf & "       outer apply (select top 1  CASE WHEN ISNULL(B.FTChangeToLineItemNo,'') ='' THEN  B.FTPOLineItemNo ELSE  ISNULL(B.FTChangeToLineItemNo,'') END AS FTPOLineItemNo,B.FTColorway,B.FTSizeBreakDown from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  where   B.FTBarcodeBundleNo = SC.FTBarcodeNo ) AS B "

                                _Qry &= vbCrLf & "   INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Ox WITH(NOLOCK) ON SC.FTOrderNo=Ox.FTOrderNo "
                                _Qry &= vbCrLf & "  WHERE  Ox.FNHSysCmpId = " & Val(R!FNHSysCmpId.ToString) & "  AND  SC.FTOrderNo <>'' "


                                If FTStartDateScan.Text <> "" Then
                                    _Qry &= vbCrLf & "   AND SC.FDDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScan.Text) & "' "
                                End If

                                If FTEndDateScan.Text <> "" Then
                                    _Qry &= vbCrLf & "   AND SC.FDDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScan.Text) & "' "
                                End If

                                _Qry &= vbCrLf & "  GROUP BY SC.FTOrderNo,SC.FTSubOrderNo,  B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItemNo , SC.FTBarcodeNo) As T "
                                _Qry &= vbCrLf & "  GROUP BY T.FTOrderNo,T.FTSubOrderNo, T.FTUnitSectCode, T.FTColorway, T.FTSizeBreakDown, T.FTPOLineItemNo, T.FTBarcodeNo  "


                            Case Else

                                _Qry &= vbCrLf & " Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Z.FTOrderNo,Z.FTSubOrderNo,'' FTUnitSectCode, Z.FTColorway, Z.FTSizeBreakDown ,0 AS  FNScanQuantity ,Z.FTNikePOLineItem ,'' FTBarcodeNo  ,ROW_NUMBER() Over (Order by  z.FTOrderNo) AS Indx  "

                                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination As Z INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Ox WITH(NOLOCK) ON Z.FTOrderNo=Ox.FTOrderNo  "

                                _Qry &= vbCrLf & " WHERE Ox.FNHSysCmpId = " & Val(R!FNHSysCmpId.ToString) & "  AND Z.FTOrderNo <> '' "

                                If FNHSysBuyId.Text <> "" Then
                                    _Qry &= vbCrLf & " And Z.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
                                End If

                                If FNHSysStyleId.Text <> "" Then
                                    _Qry &= vbCrLf & " And Z.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
                                End If

                                If FTOrderNo.Text <> "" Then
                                    _Qry &= vbCrLf & " And Z.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
                                End If

                                If FTOrderNoTo.Text <> "" Then
                                    _Qry &= vbCrLf & " AND Z.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
                                End If


                                If FTCustomerPO.Text <> "" Then
                                    _Qry &= vbCrLf & " AND Z.FTPOref >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
                                End If

                                If FTCustomerPOTo.Text <> "" Then
                                    _Qry &= vbCrLf & " AND Z.FTPOref <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
                                End If

                                If FTStartShipment.Text <> "" Then
                                    _Qry &= vbCrLf & " AND Z.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                                End If

                                If FTEndShipment.Text <> "" Then

                                    _Qry &= vbCrLf & " AND Z.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "

                                End If

                        End Select


                        _Qry &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataBD (FTUserLogIn,FTOrderNo,FTSubOrderNo, FTColorway, FTSizeBreakDown, FTPOLineItemNo,FTCustPO,Indx)"
                        _Qry &= vbCrLf & " Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', T.FTOrderNo,T.FTSubOrderNo, T.FTColorway, T.FTSizeBreakDown  ,T.FTPOLineItemNo,Z.FTPOref ,ROW_NUMBER() Over (Order by  T.FTOrderNo) AS Indx"
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabData   AS  T WITH(NOLOCK)  INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination As Z On T.FTOrderNo = Z.FTOrderNo "
                        _Qry &= vbCrLf & " And  T.FTSubOrderNo = Z.FTSubOrderNo AND T.[FTPOLineItemNo]=Z.FTNikePOLineItem"
                        _Qry &= vbCrLf & " And T.FTColorway = Z.FTColorway"
                        _Qry &= vbCrLf & " And  T.FTSizeBreakDown = Z.FTSizeBreakDown"

                        _Qry &= vbCrLf & " WHERE T.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        If FNHSysBuyId.Text <> "" Then
                            _Qry &= vbCrLf & " And Z.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
                        End If

                        If FNHSysStyleId.Text <> "" Then
                            _Qry &= vbCrLf & " And Z.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
                        End If

                        If FTOrderNo.Text <> "" Then
                            _Qry &= vbCrLf & " And T.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
                        End If

                        If FTOrderNoTo.Text <> "" Then
                            _Qry &= vbCrLf & " AND T.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
                        End If


                        If FTCustomerPO.Text <> "" Then
                            _Qry &= vbCrLf & " AND Z.FTPOref >='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'  "
                        End If

                        If FTCustomerPOTo.Text <> "" Then
                            _Qry &= vbCrLf & " AND Z.FTPOref <='" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'  "
                        End If

                        If FTStartShipment.Text <> "" Then
                            _Qry &= vbCrLf & " AND Z.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
                        End If

                        If FTEndShipment.Text <> "" Then

                            _Qry &= vbCrLf & " AND Z.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "

                        End If
                        _Qry &= vbCrLf & " GROUP BY   T.FTOrderNo,T.FTSubOrderNo, T.FTColorway, T.FTSizeBreakDown,T.FTPOLineItemNo,Z.FTPOref "

                        _Qry &= vbCrLf & "  INSERT INTO @TabOrder ([FTOrderNo]) "
                        _Qry &= vbCrLf & "  SELECT DISTINCT A.FTOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataBD  AS A WITH(NOLOCK) INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Ox WITH(NOLOCK) ON A.FTOrderNo=Ox.FTOrderNo "
                        _Qry &= vbCrLf & "  WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND Ox.FNHSysCmpId = " & Val(R!FNHSysCmpId.ToString) & " "

                        _Qry &= vbCrLf & " DELETE  A  "
                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataBD AS A "
                        _Qry &= vbCrLf & " OUTER APPLY(SELECT TOP 1 '1' AS FTState FROM @TabOrder AS B WHERE B.FTOrderNo =A.FTOrderNo) AS B"
                        _Qry &= vbCrLf & " WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND  ISNULL(B.FTState,'') <>'1' "


                        _Qry &= vbCrLf & " DELETE  A  "
                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabData AS A "
                        _Qry &= vbCrLf & " OUTER APPLY(SELECT TOP 1 '1' AS FTState FROM @TabOrder AS B WHERE B.FTOrderNo =A.FTOrderNo) AS B"
                        _Qry &= vbCrLf & " WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND  ISNULL(B.FTState,'') <>'1' "

                        '----------Start Sew Out
                        _Qry &= vbCrLf & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataSewOut (FTUserLogIn,FTOrderNo,FTSubOrderNo, FTUnitSectCode, FTColorway, FTSizeBreakDown, FNScanQuantity, FTPOLineItemNo, FTBarcodeNo ,FTCustPO,Indx)"
                        _Qry &= vbCrLf & "  Select   '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTOrderNo,FTSubOrderNo,FTUnitSectCode, FTColorway, FTSizeBreakDown ,sum( FNScanQuantity) As FNScanQuantity ,FTPOLineItemNo ,FTBarcodeNo ,FTCustPO,ROW_NUMBER() Over (Order by  T.FTOrderNo) AS Indx"
                        _Qry &= vbCrLf & "  From( "

                        _Qry &= vbCrLf & "   SELECT SC.FTOrderNo,SC.FTSubOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown, SUM(SC.FNQuantity) AS FNScanQuantity , B.FTPOLineItemNo  , SC.FTBarcodeNo,MAX(X.FTCustPO) As FTCustPO"


                        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As SC With(NOLOCK)"
                        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With(NOLOCK) On SC.FNHSysUnitSectId = US.FNHSysUnitSectId "
                        '  _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) On SC.FTBarcodeNo = B.FTBarcodeBundleNo"

                        _Qry &= vbCrLf & "       outer apply (select top 1  CASE WHEN ISNULL(B.FTChangeToLineItemNo,'') ='' THEN  B.FTPOLineItemNo ELSE  ISNULL(B.FTChangeToLineItemNo,'') END AS FTPOLineItemNo,B.FTColorway,B.FTSizeBreakDown from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK)  where   B.FTBarcodeBundleNo = SC.FTBarcodeNo ) AS B "


                        _Qry &= vbCrLf & "      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataBD    As X  WITH(NOLOCK) On SC.FTOrderNo = X.FTOrderNo AND SC.FTSubOrderNo=X.FTSubOrderNo  And B.FTColorway=X.FTColorway And B.FTSizeBreakDown=X.FTSizeBreakDown  AND B.FTPOLineItemNo =  X.FTPOLineItemNo    "
                        _Qry &= vbCrLf & " INNER JOIN @TabOrder AS Ox ON  SC.FTOrderNo =  Ox.FTOrderNo"
                        _Qry &= vbCrLf & "   WHERE X.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND SC.FTOrderNo <>''  "

                        'If FTStartDateScan.Text <> "" Or FTEndDateScan.Text <> "" Then

                        '    If FTStartDateScan.Text <> "" Then
                        '        _Qry &= vbCrLf & "   AND SC.FDDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDateScan.Text) & "' "
                        '    End If

                        '    If FTEndDateScan.Text <> "" Then
                        '        _Qry &= vbCrLf & "   AND SC.FDDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScan.Text) & "' "
                        '    End If

                        'End If


                        If FTEndDateScan.Text <> "" Then

                            If FTEndDateScan.Text <> "" Then
                                _Qry &= vbCrLf & "   AND SC.FDDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDateScan.Text) & "' "
                            End If

                        End If

                        _Qry &= vbCrLf & "   GROUP BY SC.FTOrderNo,SC.FTSubOrderNo,US.FTUnitSectCode,B.FTColorway,B.FTSizeBreakDown , B.FTPOLineItemNo , SC.FTBarcodeNo) AS T "
                        _Qry &= vbCrLf & "  GROUP BY FTOrderNo,FTSubOrderNo,FTUnitSectCode,FTColorway,FTSizeBreakDown , FTPOLineItemNo ,FTBarcodeNo,FTCustPO "


                        '----------End  Sew Out

                        _Qry &= vbCrLf & "  "

                        _Qry &= vbCrLf & " SELECT MX.FTCustPO,MX.FTPOLineItemNo,Sum(MX.FNScanQuantity) AS FNScanQuantity,Max(MX.FTStyleCode) As FTStyleCode,Max(MX.FTCmpCode) AS FTCmpCode,Max(MX.FTCmpName) AS FTCmpName"
                        _Qry &= vbCrLf & ",MAX(ISNULL(Od.FTSubOrderNo,'')) AS FTSubOrderNo ,MAX(ISNULL(Ship.FNGrandQuantityShip,0)) AS FNGrandQuantityShip,MAX(CASE WHEN ISDATE(Ship.FDShipDate) = 1 THEN  Convert(varchar(10), Convert(date,Ship.FDShipDate)  ,103)  ELSE '' END ) AS FDShipDate"
                        _Qry &= vbCrLf & " ,MAX(CASE WHEN ISDATE(Ship.FDORShipDate) = 1 THEN  Convert(varchar(10), Convert(date,Ship.FDORShipDate)  ,103)  ELSE '' END ) AS FDORShipDate,MAX(Ship.FTBuyCode) AS FTBuyCode"
                        _Qry &= vbCrLf & " FROM (SELECT SBD.FTOrderNo, SBD.FTUnitSectCode, SBD.FTColorway, SBD.FTSizeBreakDown, SBD.FNScanQuantity, SBD.FTPOLineItemNo, SBD.FTBarcodeNo, SBD.FTCustPO , ST.FTStyleCode,Cmp.FTCmpCode"

                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            _Qry &= vbCrLf & " ,Cmp.FTCmpNameTH AS FTCmpName"
                        Else
                            _Qry &= vbCrLf & " ,Cmp.FTCmpNameEN AS FTCmpName"
                        End If

                        _Qry &= vbCrLf & "    FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TEMPDB) & "].dbo.TTEMPSewingTabDataSewOut  AS SBD WITH(NOLOCK) INNER JOIN  "
                        _Qry &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) ON SBD.FTOrderNo =  A.FTOrderNo "
                        _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId=ST.FNHSysStyleId "
                        _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON A.FNHSysCmpId = Cmp.FNHSysCmpId "
                        _Qry &= vbCrLf & "      WHERE SBD.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "  ) AS MX"
                        _Qry &= vbCrLf & "  OUTER APPLY (SELECT  SUM(ZZ.FNGrandQuantity) AS FNGrandQuantityShip,MIN(ZZ.FDShipDate) AS FDShipDate,MAX(ISNULL(MM2.FDORShipDate,'')) AS FDORShipDate ,MAX(MMB2.FTBuyCode) As FTBuyCode "

                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo. V_OrderSub_BreakDown_ShipDestination AS ZZ "
                        _Qry &= vbCrLf & " Outer Apply (select TOP 1  MM2.FDORShipDate  "
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo. TMERTOrderGACDateCfm  AS MM2 WITH(NOLOCK) "

                        _Qry &= vbCrLf & "  WHERE MM2.FTSubOrderNo = ZZ.FTSubOrderNo "
                        _Qry &= vbCrLf & "  AND MM2.FTNikePOLineItem = ZZ.FTNikePOLineItem "
                        _Qry &= vbCrLf & "  ORDER BY MM2.FNSeq DESC "
                        _Qry &= vbCrLf & "  ) MM2"



                        _Qry &= vbCrLf & " Outer Apply (select TOP 1  MMB2.FTBuyCode  "
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo. TMERMBuy  AS MMB2 WITH(NOLOCK) "

                        _Qry &= vbCrLf & "  WHERE MMB2.FNHSysBuyId = ZZ.FNHSysBuyId "

                        _Qry &= vbCrLf & "  ) MMB2"


                        _Qry &= vbCrLf & "  WHERE ZZ.FTPOref = MX.FTCustPO "
                        _Qry &= vbCrLf & "  AND ZZ.FTNikePOLineItem = MX.FTPOLineItemNo "

                        _Qry &= vbCrLf & "     )  AS Ship"



                        _Qry &= vbCrLf & "      OUTER APPLY(select  STUFF((SELECT  ',' + FTSubOrderNo   "
                        _Qry &= vbCrLf & "    	From(SELECT DISTINCT P.FTSubOrderNo  "
                        _Qry &= vbCrLf & "    FROM       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo. V_OrderSub_BreakDown_ShipDestination  As P With(NOLOCK)   "


                        _Qry &= vbCrLf & "  WHERE P.FTPOref = MX.FTCustPO "
                        _Qry &= vbCrLf & "  AND P.FTNikePOLineItem = MX.FTPOLineItemNo "
                        _Qry &= vbCrLf & "      ) AS T  "
                        _Qry &= vbCrLf & "    For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTSubOrderNo ) AS Od "


                        _Qry &= vbCrLf & " GROUP BY MX.FTCustPO,MX.FTPOLineItemNo "
                        _Qry &= vbCrLf & " ORDER BY  MX.FTCustPO,MX.FTPOLineItemNo "

                        _dtdetail = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                        If dtdatadetail Is Nothing Then
                            dtdatadetail = _dtdetail.Copy
                        Else
                            dtdatadetail.Merge(_dtdetail.Copy)

                        End If

                        _dtdetail.Dispose()
                    Catch ex22 As Exception
                        ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                    End Try

                End If

            Next

            Me.ogddetail.DataSource = dtdatadetail.Copy

            dtdata.Dispose()
            dtdatadetail.Dispose()
        Catch ex As Exception
        End Try

        Return True
    End Function


#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LoadCompany()

            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            If _dtcmp.Select("FTSelect='1'").Length > 0 Then
                _dtcmp.Dispose()

                Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")

                Try
                    Me.LoadDataInfo(_Spls)
                    Me.otbdata.SelectedTabPageIndex = 0
                Catch ex As Exception
                    ' System.Windows.Forms.MessageBox.Show(ex.Message())
                End Try

                _Spls.Close()

            Else
                _dtcmp.Dispose()

                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Company !!!", 15120508456, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If

        End If

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



End Class