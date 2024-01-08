Public Class csOrder
    Public Shared Sub ConfirmSizeSpecToOrder(StyleId As Integer, SeasonId As Integer)

        Dim _Cmd As String = ""

        Dim StyleCode As String = ""
        Dim SeasonCode As String = ""

        _Cmd = " Select  Top 1 FTStyleCode  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle With(NOLOCK) WHERE FNHSysStyleId =" & StyleId & " "
        StyleCode = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "")

        _Cmd = "  Select  Top 1 FTSeasonCode From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason With(NOLOCK) WHERE FNHsysSeaSonId = " & SeasonId & " "
        SeasonCode = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "")

        _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_SIZESPEC_POST '" & HI.UL.ULF.rpQuoted(StyleCode) & "','" & HI.UL.ULF.rpQuoted(SeasonCode) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

    End Sub

    Public Shared Function ImportFactoryOrder(_Spls As HI.TL.SplashScreen, pImportOrderType As Integer, pCustId As Integer, pCmpRunId As Integer, CmpRunIdText As String, pBuyId As Integer, pMerteamId As Integer, pcust As String, pmerteam As String, pbuy As String, Optional StateKepepData As Boolean = False) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""
        Dim tSql As String = ""
        Try
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer

            Dim _FNImportOrderType As Integer = 0

            Select Case pImportOrderType
                Case 1
                    _FNImportOrderType = 22
                Case 2
                    _FNImportOrderType = 26
                Case Else
                    _FNImportOrderType = 0
            End Select

            Dim _Prefix As String = HI.TL.CboList.GetListRefer("FNImportOrderType", pImportOrderType)
            nFNHSysCustId = pCustId : nFNHSysCmpRunId = pCmpRunId : nFNHSysBuyId = pBuyId

            Dim oDBdtImport As System.Data.DataTable

            tSql = ""
            tSql = "SELECT  A.FNHSysMerTeamId, A.FTPONo, A.FTPOTrading, A.FTPOCreateDate, A.FTOrderDate"
            tSql &= Environment.NewLine & "         , " & nFNHSysBuyId & " AS FNHSysBuyId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId, " & nFNHSysCustId & " AS FNHSysCustId"
            tSql &= Environment.NewLine & "         , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTOrderBy, 0 AS FNOrderType, 0 AS FNJobState"
            tSql &= Environment.NewLine & "         , A.FTStyle, A.FDShipDate, A.FDShipDateOrginal, A.FNHSysBuyGrpId, A.FNHSysGenderId, A.FNHSysProdTypeId"
            tSql &= Environment.NewLine & "         , A.FNHSysPlantId, A.FTYear, A.FNHSysMainCategoryId, A.FTPlanningSeason, A.FNHSysCountryId, A.FNHSysBuyerId"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "      AND A.FNRowImport = (SELECT MAX(L1.FNRowImport)"
            tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
            tSql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
            tSql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
            tSql &= Environment.NewLine & "ORDER BY B.FNHSysMerTeamId ASC, A.FNRowImport ASC;"

            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then
                '...generate facotry order no.
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction





                Try

                    tSql = ""
                    tSql = " DECLARE @DBName NVARCHAR(30);"
                    tSql &= Environment.NewLine & " DECLARE @TblName NVARCHAR(30);"
                    tSql &= Environment.NewLine & " DECLARE @DocType NVARCHAR(30);"
                    tSql &= Environment.NewLine & " DECLARE @GetFotmat NVARCHAR(30);"
                    tSql &= Environment.NewLine & " DECLARE @AddPrefix NVARCHAR(30);"
                    tSql &= Environment.NewLine & " SET @DBName = N'" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "';"
                    tSql &= Environment.NewLine & " SET @TblName = N'TMERTOrder';"
                    tSql &= Environment.NewLine & " SET @DocType = '" & _FNImportOrderType.ToString & "';"
                    tSql &= Environment.NewLine & " SET @GetFotmat = '';"
                    tSql &= Environment.NewLine & " SET @AddPrefix = N'" & HI.UL.ULF.rpQuoted(_Prefix & CmpRunIdText) & "';"
                    tSql &= Environment.NewLine & " DECLARE @tblSrcConfigDoc AS TABLE(FTRunNo NVARCHAR(30), FTRunStr NVARCHAR(30), FNRunning INT, FNRunningNoMax INT);"
                    tSql &= Environment.NewLine & " INSERT INTO @tblSrcConfigDoc(FTRunNo, FTRunStr, FNRunning, FNRunningNoMax)"
                    tSql &= Environment.NewLine & " EXEC SP_GET_FACTORY_ORDERNO_MAX @DBName, @TblName, @DocType, @GetFotmat,@AddPrefix;"
                    tSql &= Environment.NewLine & " DECLARE @FTRunNo        AS NVARCHAR(30);"
                    tSql &= Environment.NewLine & " DECLARE @FTRunStr       AS NVARCHAR(30);"
                    tSql &= Environment.NewLine & " DECLARE @FNRunning      AS INT;"
                    tSql &= Environment.NewLine & " DECLARE @FNRunningNoMax AS INT;"
                    tSql &= Environment.NewLine & " SELECT @FTRunNo = A.FTRunNo, @FTRunStr = A.FTRunStr, @FNRunning = A.FNRunning, @FNRunningNoMax = A.FNRunningNoMax"
                    tSql &= Environment.NewLine & " FROM @tblSrcConfigDoc AS A;"
                    tSql &= Environment.NewLine & " --SET @FNRunningNoMax = @FNRunningNoMax + 1;"
                    tSql &= Environment.NewLine & " Declare @Tab AS table("
                    tSql &= Environment.NewLine & " [FTInsUser] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FDInsDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FTInsTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & " [FTUpdUser] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FDUpdDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FTUpdTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & " [FTOrderNo] [nvarchar](30) NOT NULL,"
                    tSql &= Environment.NewLine & " [FDOrderDate] [nvarchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FTOrderBy] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FNOrderType] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysCmpId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysCmpRunId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysStyleId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTPORef] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FNHSysCustId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysAgencyId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysProdTypeId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysBuyerId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTMainMaterial] [nvarchar](500) NULL,"
                    tSql &= Environment.NewLine & " [FTCombination] [nvarchar](500) NULL,"
                    tSql &= Environment.NewLine & " [FTRemark] [nvarchar](1000) NULL,"
                    tSql &= Environment.NewLine & " [FTStateOrderApp] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & " [FTAppBy] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FDAppDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FTAppTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & " [FNJobState] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTStateBy] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FDStateDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FTStateTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & " [FTImage1] [nvarchar](100) NULL,"
                    tSql &= Environment.NewLine & " [FTImage2] [nvarchar](100) NULL,"
                    tSql &= Environment.NewLine & " [FTImage3] [nvarchar](100) NULL,"
                    tSql &= Environment.NewLine & " [FTImage4] [nvarchar](100) NULL,"
                    tSql &= Environment.NewLine & " [FNHSysBrandId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysBuyId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTCancelAppBy] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FDCancelAppDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FDCancelAppTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & " [FTCancelAppRemark] [nvarchar](500) NULL,"
                    tSql &= Environment.NewLine & " [FTPOTradingCo] [nvarchar](30) NULL,"
                    tSql &= Environment.NewLine & " [FTPOItem] [nvarchar](30) NULL,"
                    tSql &= Environment.NewLine & " [FTPOCreateDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FNHSysMerTeamId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysPlantId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysBuyGrpId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysMainCategoryId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysVenderPramId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTOrderCreateStatus] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & " [FTImportUser] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & " [FDImportDate] [nvarchar](10) NULL,"
                    tSql &= Environment.NewLine & " [FTImportTime] [nvarchar](8) NULL,"
                    tSql &= Environment.NewLine & " [FNRowImport] [Int],"
                    tSql &= Environment.NewLine & " [FTStyle] [nvarchar](30) NULL,"
                    tSql &= Environment.NewLine & " [FNHSysSeasonId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysStyleIdRef] [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysProvinceId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTSubPgm] [nvarchar](200) NULL,"
                    tSql &= Environment.NewLine & " [FTStateSet] [varchar](1) NULL,"
                    tSql &= Environment.NewLine & " [FTVenderPramCode] [nvarchar](30) NULL"



                    tSql &= Environment.NewLine & "  )"
                    tSql &= Environment.NewLine & "INSERT INTO  @Tab"
                    tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                    tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                    tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                    tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                    tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                    tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                    tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                    tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                    tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNRowImport],[FTStyle],[FNHSysSeasonId],[FNHSysStyleIdRef],[FNHSysProvinceId],[FTSubPgm],[FTStateSet],FTVenderPramCode)"
                    tSql &= Environment.NewLine & "SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                    tSql &= Environment.NewLine & "     , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                    tSql &= Environment.NewLine & "     , @FTRunNo + (RIGHT(@FTRunStr, @FNRunning - LEN(CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY A.FTVenderPramCode,B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax)))) + CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY A.FTVenderPramCode,B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax))) AS FTOrderNo"
                    tSql &= Environment.NewLine & "     , A.FTOrderDate, NULL AS FTOrderBy, " & _FNImportOrderType.ToString & " AS FNOrderType"
                    tSql &= Environment.NewLine & "     , A.FTCmpId AS FNHSysCmpId"
                    tSql &= Environment.NewLine & ", " & nFNHSysCmpRunId & " AS FNHSysCmpRunId"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE L1.FTStyleCode = A.FTStyle), NULL) AS FNHSysStyleId"
                    tSql &= Environment.NewLine & "     , A.FTPONo, " & nFNHSysCustId & " AS FNHSysCustId"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysAgencyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS L1 WITH(NOLOCK) WHERE L1.FTAgencyCode = N'-'),NULL) AS FNHSysAgencyId"
                    tSql &= Environment.NewLine & "     , A.FNHSysProdTypeId, A.FNHSysBuyerId, NULL AS FTMainMaterial, NULL AS FTCombination, NULL AS FTRemark"
                    tSql &= Environment.NewLine & "     , NULL AS FTStateOrderApp, NULL AS FTAppBy, NULL AS FDAppDate, NULL AS FTAppTime"
                    tSql &= Environment.NewLine & "     , 0 AS FNJobState, NULL AS FTStateBy, NULL AS FDStateDate, NULL AS FTStateTime"
                    tSql &= Environment.NewLine & "     , NULL AS FTImage1, NULL AS FTImage2, NULL AS FTImage3, NULL AS FTImage4"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysBrandId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS L1 WITH(NOLOCK) WHERE L1.FTBrandCode = N'-'),NULL) AS FNHSysBrandId"
                    tSql &= Environment.NewLine & "     , " & nFNHSysBuyId & " AS FNHSysBuyId"
                    tSql &= Environment.NewLine & "     , NULL AS FTCancelAppBy, NULL AS FDCancelAppDate, NULL AS FDCancelAppTime, NULL AS FTCancelAppRemark"
                    tSql &= Environment.NewLine & "     , A.FTPOTrading, NULL AS FTPOItem, A.FTPOCreateDate"
                    tSql &= Environment.NewLine & "     ," & pMerteamId & " AS FNHSysMerTeamId, A.FNHSysPlantId, A.FNHSysBuyGrpId"
                    tSql &= Environment.NewLine & "     , A.FNHSysMainCategoryId, A.FNHSysVenderPramId AS FNHSysVenderPramId"
                    tSql &= Environment.NewLine & "     , N'Y' AS FTOrderCreateStatus, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTImportUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDImportDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTImportTime,A.FNRowImport,A.FTStyle "
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT  TOP 1  L1.FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS L1 WITH(NOLOCK) WHERE L1.FTSeasonCode = A.FTPlanningSeason + RIGHT(A.FTYear,2)), 0) AS FNHSysSeasonId"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT  TOP 1 L1.FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE LEFT(L1.FTStyleCode,LEN(A.FTStyle)) = A.FTStyle), 0) AS FNHSysStyleIdRef,A.FNHSysProvinceId" ',A.FTSubPgm


                    tSql &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 FTSubPgm  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS XA2 WHERE  XA2.FTUserLogin = A.FTUserLogin AND  XA2.FTPONo = A.FTPONO  AND XA2.FTStyle = A.FTStyle AND ISNULL(XA2.FTSubPgm,'') <>'' ORDER BY XA2.FTSubPgmSeq),'') AS FTSubPgm "
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT  TOP 1 L1.FTStateStyleSet FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE L1.FTStyleCode = A.FTStyle), '0') AS FTStateSet"
                    tSql &= Environment.NewLine & "     ,A.FTVenderPramCode "
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A "
                    tSql &= Environment.NewLine & " OUTER APPLY ( SELECT TOP 1  B.FNHSysMerTeamId ,B.FTMerTeamCode  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) WHERE  B.FNHSysMerTeamId =" & pMerteamId & " )  As B "

                    tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= Environment.NewLine & "      AND A.FNRowImport = (SELECT MAX(L1.FNRowImport)"
                    tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
                    tSql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
                    tSql &= Environment.NewLine & "                                 AND L1.FTStyle = A.FTStyle"
                    tSql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
                    tSql &= Environment.NewLine & " ORDER BY A.FTVenderPramCode,B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC;"
                    tSql &= Environment.NewLine & " UPDATE A"
                    tSql &= Environment.NewLine & " SET FTGenerateOrderNo = ISNULL((SELECT TOP 1 ISNULL(FTOrderNo,'')"
                    tSql &= Environment.NewLine & "                                FROM @Tab"
                    tSql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyle), '')"
                    tSql &= Environment.NewLine & ",FTStateSet = ISNULL((SELECT TOP 1 ISNULL(FTStateSet,'')"
                    tSql &= Environment.NewLine & "                                FROM @Tab"
                    tSql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyle), '')"
                    tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                    tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "


                    ' tSql &= Environment.NewLine & "  ORDER BY  A.FTVenderPramCode, B.FTMerTeamCode , A.FTStyle , A.FTPONO "

                    'tSql &= Environment.NewLine & "      AND A.FNRowImport IN ( "
                    'tSql &= Environment.NewLine & "                              SELECT MAX(L1.FNRowImport) "
                    'tSql &= Environment.NewLine & "                              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK) "
                    'tSql &= Environment.NewLine & "                              WHERE L1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    'tSql &= Environment.NewLine & "                              GROUP BY L1.FTPONo,FDShipDate,L1.FTStyle "
                    'tSql &= Environment.NewLine & " 	                       )"
                    tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]"
                    tSql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                    tSql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                    tSql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                    tSql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                    tSql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                    tSql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                    tSql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                    tSql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                    tSql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                    tSql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],[FNHSysCmpIdCreate],[FTSubPgm],[FNHSysCmpIdTo])"
                    tSql &= Environment.NewLine & " SELECT [FTInsUser],[FDInsDate],[FTInsTime]"
                    tSql &= Environment.NewLine & "       ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                    tSql &= Environment.NewLine & "       ,[FTOrderNo],[FDOrderDate]"
                    tSql &= Environment.NewLine & "       ,ISNULL(("
                    tSql &= Environment.NewLine & "   SELECT TOP 1 FTUserName"
                    tSql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMMerTeam AS AX WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "   WHERE   (AX.FNHSysMerTeamId =" & pMerteamId & ")"
                    tSql &= Environment.NewLine & "        ),'')  AS FTOrderBy"
                    tSql &= Environment.NewLine & ",[FNOrderType]"
                    ' tSql &= Environment.NewLine & "       ,[FNHSysCmpId]"
                    'tSql &= Environment.NewLine & "       ,CASE WHEN  " & Val(_FixCmpByProgram) & " > 0 THEN " & Val(_FixCmpByProgram) & " ELSE ISNULL(("
                    'tSql &= Environment.NewLine & "   SELECT TOP 1 FNHSysCmpId"
                    'tSql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS AX WITH(NOLOCK)"
                    'tSql &= Environment.NewLine & "   WHERE  (AX.FNOrderType = 13) "
                    'tSql &= Environment.NewLine & "           AND (AX.FNHSysStyleId = M.FNHSysStyleId)"
                    'tSql &= Environment.NewLine & "   ORDER BY FTOrderNo DESC"
                    'tSql &= Environment.NewLine & "        ),"
                    'tSql &= Environment.NewLine & " ISNULL(("
                    'tSql &= Environment.NewLine & "   SELECT TOP 1 FNHSysCmpId"
                    'tSql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS AX WITH(NOLOCK)"
                    'tSql &= Environment.NewLine & "   WHERE  (AX.FNHSysStyleId = M.FNHSysStyleId) "
                    'tSql &= Environment.NewLine & "   ORDER BY FTOrderNo DESC"
                    'tSql &= Environment.NewLine & "        ),NULL)"
                    'tSql &= Environment.NewLine & " ) END AS FNHSysCmpId"

                    tSql &= Environment.NewLine & "       ,CASE WHEN  ISNULL(ZCfg.FixCmpByProgram,0) > 0 THEN  ISNULL(ZCfg.FixCmpByProgram,0)  ELSE ISNULL(M.FNHSysCmpId,ISNULL(Z.FNHSysCmpId,NULL))  END AS FNHSysCmpId"

                    tSql &= Environment.NewLine & "       ,[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                    tSql &= Environment.NewLine & "       ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                    tSql &= Environment.NewLine & "       ,[FTMainMaterial],[FTCombination],[FTRemark]"
                    tSql &= Environment.NewLine & "       ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                    tSql &= Environment.NewLine & "       ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                    tSql &= Environment.NewLine & "       ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                    tSql &= Environment.NewLine & "       ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                    tSql &= Environment.NewLine & "       ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                    tSql &= Environment.NewLine & "       ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                    tSql &= Environment.NewLine & "       ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId]," & Val(HI.ST.SysInfo.CmpID) & ",FTSubPgm"


                    tSql &= Environment.NewLine & "  ,ISNULL(Cmpc.FNHSysCmpPOId,0) "
                    tSql &= Environment.NewLine & " FROM @Tab AS M "



                    tSql &= Environment.NewLine & "  Outer apply (Select top 1  CASE  WHEN ISNUMERIC(ZCfg.FTCfgData) = 1 THEN Convert(int,ZCfg.FTCfgData)  ELse 0 END      AS   FixCmpByProgram  "
                    tSql &= Environment.NewLine & " from     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].TSESystemConfig As ZCfg With(NOLOCK)  "
                    tSql &= Environment.NewLine & "   WHERE  ZCfg.FTCfgName = M.FTVenderPramCode  "
                    tSql &= Environment.NewLine & "     ) As ZCfg "



                    tSql &= Environment.NewLine & "  Outer apply (Select top 1  Z.FNHSysCmpId  "
                    tSql &= Environment.NewLine & " from     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder As Z With(NOLOCK)  "
                    tSql &= Environment.NewLine & "   WHERE  (Z.FNHSysStyleId = M.FNHSysStyleId)  "
                    tSql &= Environment.NewLine & "   ORDER BY Case When Z.FNOrderType = 13  Then 1 Else 2 End ,Z.FTOrderNo DESC  ) As Z "
                    tSql &= Environment.NewLine & "  outer apply (Select top 1 Cmpc.FNHSysCmpPOId from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) where Cmpc.FNHSysCmpId = Case When   ISNULL(ZCfg.FixCmpByProgram,0) > 0 Then  ISNULL(ZCfg.FixCmpByProgram,0) Else ISNULL(M.FNHSysCmpId,ISNULL(Z.FNHSysCmpId,NULL))  End ) As Cmpc "

                    '  tSql &= Environment.NewLine & " DROP TABLE #Tab;"

                    '...Merge Transaction TMERTOrderSub And TMERTOrderSub_BreakDown
                    '========================================================================================================================================================================

                    tSql &= Environment.NewLine & " Declare @Tabsuborder As Table("
                    tSql &= Environment.NewLine & "  [FTInsUser] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FDInsDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & "[FTInsTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & "[FTUpdUser] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FDUpdDate] [varchar](10) NULL,"
                    tSql &= Environment.NewLine & "[FTUpdTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) Not NULL,"
                    tSql &= Environment.NewLine & "[FTSubOrderNo] [nvarchar](30) Not NULL,"
                    tSql &= Environment.NewLine & "[FDSubOrderDate] [nvarchar](10) NULL,"
                    tSql &= Environment.NewLine & "[FTSubOrderBy] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FDProDate] [nvarchar](10) NULL,"
                    tSql &= Environment.NewLine & "[FDShipDate] [nvarchar](10) NULL,"
                    tSql &= Environment.NewLine & "	[FNHSysBuyId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysContinentId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysCountryId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysProvinceId] [int] NULL,"
                    tSql &= Environment.NewLine & "	[FNHSysShipModeId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysCurId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysGenderId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysUnitId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNSubOrderState] [int] NULL,"
                    tSql &= Environment.NewLine & "[FTStateEmb] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTStatePrint] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTStateHeat] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTStateLaser] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTStateWindows] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTStateSewOnly] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTStateOther1] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTOther1Note] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FTStateOther2] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTOther2Note] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FTStateOther3] [nvarchar](1) NULL,"
                    tSql &= Environment.NewLine & "[FTOther3Note1] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FTRemark] [nvarchar](1000) NULL,"
                    tSql &= Environment.NewLine & "[FNHSysShipPortId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FDShipDateOrginal] [nvarchar](10) NULL,"
                    tSql &= Environment.NewLine & "[FTCustRef] [nvarchar](200) NULL,"
                    tSql &= Environment.NewLine & "[FTPOTrading] [nvarchar](50) NULL,"
                    tSql &= Environment.NewLine & "[FNGrpSeq] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysPlantId] [int] NULL,"
                    tSql &= Environment.NewLine & "[FNHSysBuyGrpId] [int] NULL,"
                    tSql &= Environment.NewLine & " [FTStateSet]  [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysShipToAccountId]  [int] NULL,"
                    tSql &= Environment.NewLine & " [FNHSysAFSId]  [int] NULL"

                    tSql &= Environment.NewLine & "  )"
                    tSql &= Environment.NewLine & "INSERT INTO @Tabsuborder"
                    tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                    tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                    tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                    tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                    tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                    tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                    tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateSewOnly],[FTStateOther1],[FTOther1Note]"
                    tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                    tSql &= Environment.NewLine & "         ,[FTRemark]"
                    tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                    tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNGrpSeq],[FNHSysPlantId],[FNHSysBuyGrpId],[FTStateSet] ,[FTPOTrading],FNHSysShipToAccountId,FNHSysAFSId)"

                    'tSql &= Environment.NewLine & " Select A.FTInsUser,A.FDInsDate,A.FTInsTime,A.FTUpdUser ,A.FDUpdDate,A.FTUpdTime "
                    'tSql &= Environment.NewLine & " ,FTOrderNo "
                    'tSql &= Environment.NewLine & ", (A.FTOrderNo + '-' +   CASE WHEN A.FTSubOrderNoSeq <=26 THEN  CHAR(64 + A.FTSubOrderNoSeq) ELSE 'A' + CHAR(64 + (A.FTSubOrderNoSeq-26))   END) AS FTSubOrderNo"
                    'tSql &= Environment.NewLine & "     ,A.FDSubOrderDate"
                    'tSql &= Environment.NewLine & "     ,A.FTSubOrderBy"
                    'tSql &= Environment.NewLine & "     , A.FDProdDate"
                    'tSql &= Environment.NewLine & "     , A.FDShipDate"
                    'tSql &= Environment.NewLine & "     , A.FNHSysBuyId"
                    'tSql &= Environment.NewLine & "     , A.FNHSysContinentId"
                    'tSql &= Environment.NewLine & "     , A.FNHSysCountryId"
                    'tSql &= Environment.NewLine & "     , A.FNHSysProvinceId"
                    'tSql &= Environment.NewLine & "     ,A.FNHSysShipModeId"
                    'tSql &= Environment.NewLine & "     , A.FNHSysCurId"
                    'tSql &= Environment.NewLine & "     , A.FNHSysGenderId"
                    'tSql &= Environment.NewLine & "     , A.FNHSysUnitId"
                    'tSql &= Environment.NewLine & "     , A.FNSubOrderState"
                    'tSql &= Environment.NewLine & "     , A.FTStateEmb"
                    'tSql &= Environment.NewLine & "     , A.FTStatePrint"
                    'tSql &= Environment.NewLine & "     , A.FTStateHeat"
                    'tSql &= Environment.NewLine & "     , A.FTStateLaser"
                    'tSql &= Environment.NewLine & "     , A.FTStateWindows"
                    'tSql &= Environment.NewLine & "     , A.FTOther1Note,A.FTOther1Note,A.FTStateOther2,A.FTOther2Note,A.FTStateOther3,A.FTOther3Note1,A.FTRemark"
                    'tSql &= Environment.NewLine & "     , A.FNHSysShipPortId"
                    'tSql &= Environment.NewLine & "     , A.FDShipDateOrginal"
                    'tSql &= Environment.NewLine & "     , A.FTCustRef,A.FNGrpSeq,A.FNHSysPlantId,A.FNHSysBuyGrpId"

                    'tSql &= Environment.NewLine & " FROM ( "
                    tSql &= Environment.NewLine & " SELECT DISTINCT  NULL AS FTInsUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                    tSql &= Environment.NewLine & "     , NULL AS FTUpdUser , NULL AS FDUpdDate, NULL AS FTUpdTime"
                    tSql &= Environment.NewLine & "     , A.FTOrderNo"
                    'tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' + CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate))) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)

                    ' tSql &= Environment.NewLine & "     ,  ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate) AS FTSubOrderNoSeq"

                    tSql &= Environment.NewLine & "     , (A.FTOrderNo + '-' +   CASE WHEN ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,A.FNHSysPlantId,A.FTPOItem) <=26 THEN  CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,A.FNHSysPlantId,A.FTPOItem)) ELSE 'A' + CHAR(64 + ((ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate,A.FNHSysPlantId,A.FTPOItem)) - 26))   END  ) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)

                    tSql &= Environment.NewLine & "     , A.FDOrderDate AS FDSubOrderDate"
                    tSql &= Environment.NewLine & "     , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[FN_GET_PRODDATE_IMPORT] (A.FDShipDate, " & nFNHSysCustId & ", NULL)), NULL) AS FDProdDate"
                    tSql &= Environment.NewLine & "     , A.FDShipDate AS FDShipDate"
                    tSql &= Environment.NewLine & "     , NULL AS FNHSysBuyId"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysContinentId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCountryId =  A.FNHSysCountryId),0) AS FNHSysContinentId"
                    tSql &= Environment.NewLine & "     , A.FNHSysCountryId"
                    'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysProvinceId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FNHSysPlantId =  A.FNHSysPlantId ), 0) AS FNHSysProvinceId"
                    tSql &= Environment.NewLine & "     , A.FNHSysProvinceId AS FNHSysProvinceId"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode), 0) AS FNHSysShipModeId"
                    tSql &= Environment.NewLine & "     , A.FNHSysCurId AS FNHSysCurId"
                    tSql &= Environment.NewLine & "     , A.FNHSysGenderId"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L1 WITH(NOLOCK) WHERE L1.FTUnitCode = B.FTUom), 0) AS FNHSysUnitId"
                    tSql &= Environment.NewLine & "     , NULL AS FNSubOrderState"
                    tSql &= Environment.NewLine & "     , C.FTStateEmb AS FTStateEmb"
                    tSql &= Environment.NewLine & "     , C.FTStatePrint AS FTStatePrint"
                    tSql &= Environment.NewLine & "     , C.FTStateHeat AS FTStateHeat"
                    tSql &= Environment.NewLine & "     , C.FTStateLaser AS FTStateLaser"
                    tSql &= Environment.NewLine & "     , C.FTStateWindows AS FTStateWindows,C.FTStateSewOnly"
                    tSql &= Environment.NewLine & "     , NULL AS FTOther1Note, NULL AS FTOther1Note, NULL AS FTStateOther2, NULL AS FTOther2Note, NULL AS FTStateOther3, NULL AS FTOther3Note1, NULL AS FTRemark"
                    tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipPortId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS L1 WITH(NOLOCK) WHERE L1.FTShipPortCode = N'-'),NULL) AS FNHSysShipPortId"
                    tSql &= Environment.NewLine & "     , A.FDShipDateOrginal AS FDShipDateOrginal"
                    tSql &= Environment.NewLine & "     , dbo.FN_GetCustomer_Refer(A.FNHSysPlantId, A.FNHSysBuyerId) AS FTCustRef,A.FNGrpSeq,A.FNHSysPlantId,A.FNHSysBuyGrpId,A.FTStateSet,A.FTPOTrading,A.FNHSysShipToAccountId,A.FNHSysAFSId"
                    tSql &= Environment.NewLine & " FROM( "

                    '-----Order Normal Data 
                    tSql &= Environment.NewLine & " SELECT  CASE WHEN ISNULL(A.FTStateSet,'0') ='1' THEN 1 ELSE 0 END AS FTStateSet,A.FTPONo, C.FDOrderDate, A.FTPOTrading, A.FTPOItem, A.FNRowImport, A.FTStyle, C.FTOrderNo,A.FNGrpSeq"
                    tSql &= Environment.NewLine & "          , A.FDShipDate, A.FDShipDateOrginal, A.FNHSysGenderId, A.FNHSysCountryId,A.FNHSysPlantId,A.FNHSysBuyGrpId,A.FNHSysBuyerId,A.FNHSysProvinceId"
                    'tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                    tSql &= Environment.NewLine & "          , CASE WHEN ISNULL(FTCurrency,'')='' THEN ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) "
                    tSql &= Environment.NewLine & "    ELSE  ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TFINMCurrency] AS L1 WITH(NOLOCK) WHERE L1.FTCurCode = A.FTCurrency), NULL)  END AS FNHSysCurId"

                    tSql &= Environment.NewLine & "  ,C.FTPOTradingCo AS FTPOTrading2,A.FNHSysShipToAccountId,A.FNHSysAFSId"
                    tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                    tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                    tSql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                    tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                    tSql &= Environment.NewLine & "           AND A.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tSql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                    tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "
                    '-----Order Normal Data 

                    '-----Order Normal Data Set 
                    tSql &= Environment.NewLine & " UNION  "
                    tSql &= Environment.NewLine & " SELECT 2 AS FTStateSet, A.FTPONo, C.FDOrderDate, A.FTPOTrading, A.FTPOItem, A.FNRowImport, A.FTStyle, C.FTOrderNo,A.FNGrpSeq"
                    tSql &= Environment.NewLine & "          , A.FDShipDate, A.FDShipDateOrginal, A.FNHSysGenderId, A.FNHSysCountryId,A.FNHSysPlantId,A.FNHSysBuyGrpId,A.FNHSysBuyerId,A.FNHSysProvinceId"
                    'tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                    tSql &= Environment.NewLine & "          , CASE WHEN ISNULL(FTCurrency,'')='' THEN ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) "
                    tSql &= Environment.NewLine & "    ELSE  ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TFINMCurrency] AS L1 WITH(NOLOCK) WHERE L1.FTCurCode = A.FTCurrency), NULL)  END AS FNHSysCurId"

                    tSql &= Environment.NewLine & "  ,C.FTPOTradingCo AS FTPOTrading2,A.FNHSysShipToAccountId,A.FNHSysAFSId"
                    tSql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                    tSql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                    tSql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                    tSql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                    tSql &= Environment.NewLine & "           AND A.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tSql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                    tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "
                    tSql &= Environment.NewLine & "           AND ISNULL(A.FTStateSet,'0') ='1'"
                    '-----Order Normal Data Set 


                    tSql &= Environment.NewLine & " ) AS A INNER JOIN "
                    tSql &= Environment.NewLine & "     (SELECT FNRowImport, FTPONo, FTPOTrading, FTStyle, FTShipDate, MAX(FTMode) AS FTMode, MAX(FTUom) AS FTUom "
                    tSql &= Environment.NewLine & "      FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp WITH(NOLOCK) GROUP BY FNRowImport, FTPONo, FTPOTrading, FTStyle, FTShipDate"
                    tSql &= Environment.NewLine & "     ) AS B ON A.FTPONo = B.FTPONo"
                    tSql &= Environment.NewLine & "               And A.FTPOTrading = B.FTPOTrading"
                    tSql &= Environment.NewLine & "               And A.FTStyle = B.FTStyle"
                    tSql &= Environment.NewLine & "               And A.FNRowImport = B.FNRowImport"
                    tSql &= Environment.NewLine & "       LEFT JOIN (SELECT L4.FNHSysStyleId, L4.FTStyleCode, ISNULL(MAX(L3.FTStateEmb),0) AS FTStateEmb, ISNULL(MAX(L3.FTStatePrint),0) AS FTStatePrint, ISNULL(MAX(L3.FTStateHeat),0) AS FTStateHeat, ISNULL(MAX(L3.FTStateLaser),0) AS FTStateLaser, ISNULL(MAX(L3.FTStateWindows),0) AS FTStateWindows,ISNULL(MAX(L3.FTStateSewOnly),'0') AS FTStateSewOnly"
                    tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTStyle_Part] AS L3 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L4 WITH(NOLOCK) ON L3.FNHSysStyleId = L4.FNHSysStyleId"
                    tSql &= Environment.NewLine & "                  GROUP BY L4.FNHSysStyleId, L4.FTStyleCode ) AS C ON A.FTStyle = C.FTStyleCode"

                    'tSql &= Environment.NewLine & " ) AS A "

                    tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"


                    tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                    tSql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                    tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                    tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                    tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                    tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                    tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                    tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateSewOnly],[FTStateOther1],[FTOther1Note]"
                    tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                    tSql &= Environment.NewLine & "         ,[FTRemark]"
                    tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                    tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FNOrderSetType],FTPOTrading,FNHSysShipToAccountId,FNHSysAFSId)"
                    tSql &= Environment.NewLine & "         SELECT [FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                    tSql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                    tSql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                    tSql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                    tSql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                    tSql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                    tSql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateSewOnly],[FTStateOther1],[FTOther1Note]"
                    tSql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                    tSql &= Environment.NewLine & "         ,[FTRemark]"
                    tSql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                    tSql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],FTStateSet,FTPOTrading,FNHSysShipToAccountId,FNHSysAFSId"
                    tSql &= Environment.NewLine & " FROM @Tabsuborder"

                    tSql &= Environment.NewLine & " UPDATE A"

                    tSql &= Environment.NewLine & " SET FTGenerateOrderSubNo = ISNULL((SELECT TOP 1 ISNULL(FTSubOrderNo,'')"
                    tSql &= Environment.NewLine & "                                FROM @Tabsuborder"
                    tSql &= Environment.NewLine & "                                WHERE FTOrderNo = A.FTGenerateOrderNo AND FDShipDate = A.FDShipDate AND FNGrpSeq=A.FNGrpSeq AND FNHSysBuyGrpId=A.FNHSysBuyGrpId  AND FTStateSet<>2 ), '')"
                    tSql &= Environment.NewLine & " ,FTGenerateOrderSubNo2 = ISNULL((SELECT TOP 1 ISNULL(FTSubOrderNo,'')"
                    tSql &= Environment.NewLine & "                                FROM @Tabsuborder"
                    tSql &= Environment.NewLine & "                                WHERE FTOrderNo = A.FTGenerateOrderNo AND FDShipDate = A.FDShipDate AND FNGrpSeq=A.FNGrpSeq AND FNHSysBuyGrpId=A.FNHSysBuyGrpId AND FTStateSet=2), '')"

                    tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                    tSql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    ' tSql &= Environment.NewLine & " DROP TABLE @Tabsuborder;"

                    tSql &= Environment.NewLine & " DECLARE @TMERTOrderSub_BreakDown_Import AS TABLE ([FTInsUser] [nvarchar](50), [FDInsDate] [varchar](10) NULL, [FTInsTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & "                              [FTUpdUser] [nvarchar](50) NULL, [FDUpdDate] [varchar](10) NULL, [FTUpdTime] [varchar](8) NULL,"
                    tSql &= Environment.NewLine & "                              [FTOrderNo] [nvarchar](30) NOT NULL, [FTSubOrderNo] [nvarchar](30) NOT NULL,"
                    tSql &= Environment.NewLine & "                              [FTColorway] [nvarchar](30) NOT NULL, [FTSizeBreakDown] [nvarchar](30) NOT NULL,"
                    tSql &= Environment.NewLine & "                              [FNPrice] [numeric](18, 5) NULL, [FNQuantity] [int] NULL, [FNAmt] [numeric](18, 5) NULL,"
                    tSql &= Environment.NewLine & "                              [FNHSysMatColorId] [int] NULL, [FNHSysMatSizeId] [int] NULL,"
                    tSql &= Environment.NewLine & "                              [FNExtraQty] [numeric](18, 5) NULL, [FNQuantityExtra] [numeric](18, 5) NULL,"
                    tSql &= Environment.NewLine & "                              [FNGrandQuantity] [numeric](18, 5) NULL, [FNAmntExtra] [numeric](18, 5) NULL,"
                    tSql &= Environment.NewLine & "                              [FNGrandAmnt] [numeric](18, 5) NULL, [FNGarmentQtyTest] [int] NULL,[FNAmntQtyTest] [numeric](18, 5) NULL,[FTPOItem] [varchar](30) NULL,[FNCMDisPer]  [numeric](18, 5) NULL,[FNOperateFee]  [numeric](18, 5) NULL )"

                    '-----Order Normal Data 
                    tSql &= Environment.NewLine & " INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                    tSql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                    tSql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                    tSql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                    tSql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                    tSql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                    tSql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt],[FTPOItem],[FNCMDisPer],[FNOperateFee])"
                    tSql &= Environment.NewLine & " SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                    tSql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                    tSql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                    tSql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                    tSql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"
                    tSql &= Environment.NewLine & "       ordImport.FNExtraQty, ordImport.FNQuantityExtra, ordImport.FNGrandQuantity,"
                    tSql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt,ordImport.FTPOItem,ordImport.FNCMDisPer,ordImport.FNOperateFee"
                    tSql &= Environment.NewLine & " FROM (SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                    tSql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                    tSql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                    tSql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                    tSql &= Environment.NewLine & "           , A.FTColorwayCode AS FTColorway"
                    tSql &= Environment.NewLine & "           , A.FTSizeBreakdownCode AS FTSizeBreakDown"
                    tSql &= Environment.NewLine & "           , ISNULL(MAX(A.FNPrice),0) AS FNPrice"
                    tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                    tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(A.FNPrice)), NULL) AS FNAmt"
                    tSql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                    tSql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                    tSql &= Environment.NewLine & "           , NULL AS FNExtraQty"
                    tSql &= Environment.NewLine & "           , NULL AS FNQuantityExtra"
                    tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNGrandQuantity"
                    tSql &= Environment.NewLine & "           , NULL AS FNAmntExtra"
                    tSql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(A.FNPrice)), NULL) AS FNGrandAmnt,MAX(ISNULL(A.FTPOItem,'')) AS FTPOItem,MAX(ISNULL(STM.FNCMDisPer,0)) AS FNCMDisPer"
                    tSql &= Environment.NewLine & "           ,MAX(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END   ) AS FNOperateFee"
                    tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS AA "

                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] As MM3  On AA.FTGenerateOrderNo = MM3.FTOrderNo And  AA.FDShipDate = MM3.FDShipDate And AA.FTGenerateOrderSubNo = MM3.FTSubOrderNo"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] As A   On  AA.FTUserLogin = A.FTUserLogin And AA.FTPONo = A.FTPONo And AA.FDShipDate = A.FTShipDate And AA.FTStyle = A.FTStyle And MM3.FDShipDate = A.FTShipDate And AA.FTPOItem=A.FTPOItem"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As B  On AA.FTGenerateOrderNo = B.FTOrderNo"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] As C (NOLOCK) On A.FTColorwayCode = C.FTMatColorCode"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] As D (NOLOCK) On A.FTSizeBreakdownCode = D.FTMatSizeCode"
                    tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] As STM With(NOLOCK) On B.FNHSysStyleId = STM.FNHSysStyleId"
                    tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
                    tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"
                    tSql &= Environment.NewLine & " WHERE AA.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tSql &= Environment.NewLine & "      AND A.FNQuantity > 0 "
                    tSql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo, A.FTPOTrading, A.FTStyle, MM3.FTSubOrderNo, A.FTColorwayCode, A.FTSizeBreakdownCode, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                    tSql &= Environment.NewLine & "      ) As ordImport"
                    '-----Order Normal Data


                    '-----Order Normal Data  Set
                    tSql &= Environment.NewLine & " INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                    tSql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                    tSql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                    tSql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                    tSql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                    tSql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                    tSql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt],[FTPOItem],[FNCMDisPer],[FNOperateFee])"
                    tSql &= Environment.NewLine & " SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                    tSql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                    tSql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                    tSql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                    tSql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"
                    tSql &= Environment.NewLine & "       ordImport.FNExtraQty, ordImport.FNQuantityExtra, ordImport.FNGrandQuantity,"
                    tSql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt,ordImport.FTPOItem,ordImport.FNCMDisPer,ordImport.FNOperateFee"
                    tSql &= Environment.NewLine & " FROM (SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                    tSql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                    tSql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                    tSql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                    tSql &= Environment.NewLine & "           , A.FTColorwayCode AS FTColorway"
                    tSql &= Environment.NewLine & "           , A.FTSizeBreakdownCode AS FTSizeBreakDown"
                    tSql &= Environment.NewLine & "           , 0 AS FNPrice"
                    tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                    tSql &= Environment.NewLine & "           , 0 AS FNAmt"
                    tSql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                    tSql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                    tSql &= Environment.NewLine & "           , NULL AS FNExtraQty"
                    tSql &= Environment.NewLine & "           , NULL AS FNQuantityExtra"
                    tSql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNGrandQuantity"
                    tSql &= Environment.NewLine & "           , NULL AS FNAmntExtra"
                    tSql &= Environment.NewLine & "           , 0 AS FNGrandAmnt,MAX(ISNULL(A.FTPOItem,'')) AS FTPOItem,MAX(ISNULL(STM.FNCMDisPer,0)) AS FNCMDisPer"
                    tSql &= Environment.NewLine & "           ,MAX(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END   ) AS FNOperateFee"
                    tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS AA WITH(NOLOCK)"

                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] As MM3  On AA.FTGenerateOrderNo = MM3.FTOrderNo And  AA.FDShipDate = MM3.FDShipDate And AA.FTGenerateOrderSubNo2 = MM3.FTSubOrderNo"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] As A With(NOLOCK)  On  AA.FTUserLogin = A.FTUserLogin And AA.FTPONo = A.FTPONo And AA.FDShipDate = A.FTShipDate And AA.FTStyle = A.FTStyle And MM3.FDShipDate = A.FTShipDate And AA.FTPOItem=A.FTPOItem"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As B  On AA.FTGenerateOrderNo = B.FTOrderNo"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] As C (NOLOCK) On A.FTColorwayCode = C.FTMatColorCode"
                    tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] As D (NOLOCK) On A.FTSizeBreakdownCode = D.FTMatSizeCode"
                    tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] As STM With(NOLOCK) On B.FNHSysStyleId = STM.FNHSysStyleId"
                    tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
                    tSql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"
                    tSql &= Environment.NewLine & " WHERE AA.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tSql &= Environment.NewLine & "      AND A.FNQuantity > 0 "
                    tSql &= Environment.NewLine & "      AND ISNULL(AA.FTGenerateOrderSubNo2,'') <> ''"
                    tSql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo, A.FTPOTrading, A.FTStyle, MM3.FTSubOrderNo, A.FTColorwayCode, A.FTSizeBreakdownCode, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                    tSql &= Environment.NewLine & "      ) As ordImport"
                    '-----Order Normal Set

                    tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                    tSql &= Environment.NewLine & "                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                    tSql &= Environment.NewLine & "                 ,[FTOrderNo],[FTSubOrderNo]"
                    tSql &= Environment.NewLine & "                 ,[FTColorway],[FTSizeBreakDown]"
                    tSql &= Environment.NewLine & "                 ,[FNPrice],[FNQuantity],[FNAmt]"
                    tSql &= Environment.NewLine & "                 ,[FNHSysMatColorId],[FNHSysMatSizeId]"
                    tSql &= Environment.NewLine & "                 ,[FNExtraQty],[FNQuantityExtra]"
                    tSql &= Environment.NewLine & "                 ,[FNGrandQuantity]"
                    tSql &= Environment.NewLine & "                 ,[FNAmntExtra]"
                    tSql &= Environment.NewLine & "                 ,[FNGrandAmnt],[FNPriceOrg],[FTNikePOLineItem],[FNCMDisPer],[FNCMDisAmt],[FNOperateFee],[FNOperateFeeAmt],[FNNetFOB],FNNetPriceOperateFee,FNNetPriceOperateFeeAmt,FNNetNetPrice)"
                    tSql &= Environment.NewLine & " Select aa.[FTInsUser], aa.[FDInsDate], aa.[FTInsTime],"
                    tSql &= Environment.NewLine & "       aa.[FTUpdUser], aa.[FDUpdDate], aa.[FTUpdTime],"
                    tSql &= Environment.NewLine & "       aa.[FTOrderNo], aa.[FTSubOrderNo], aa.[FTColorway],"
                    tSql &= Environment.NewLine & "       aa.[FTSizeBreakDown], aa.[FNPrice],"
                    tSql &= Environment.NewLine & "       aa.[FNQuantity], aa.[FNAmt], aa.[FNHSysMatColorId], aa.[FNHSysMatSizeId],"
                    tSql &= Environment.NewLine & "       aa.[FNExtraQty], aa.[FNQuantityExtra], aa.[FNGrandQuantity],"
                    tSql &= Environment.NewLine & "       aa.[FNAmntExtra], aa.[FNGrandAmnt],aa.[FNPrice],aa.[FTPOItem],aa.FNCMDisPer,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNCMDisPer,0))/100.00))"
                    tSql &= Environment.NewLine & "     ,aa.FNOperateFee,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) AS FNOperateFeeAmt,(aa.[FNPrice] - Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) ) AS FNNetFOB "
                    tSql &= Environment.NewLine & "     ,aa.FNOperateFee,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) AS FNOperateFeeAmt,(aa.[FNPrice] - Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) ) AS FNNetFOB "
                    tSql &= Environment.NewLine & "FROM @TMERTOrderSub_BreakDown_Import As aa"
                    tSql &= Environment.NewLine & "WHERE Not EXISTS (Select 'T'"
                    tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS bb (NOLOCK)"
                    tSql &= Environment.NewLine & "                  WHERE bb.FTOrderNo = aa.FTOrderNo"
                    tSql &= Environment.NewLine & "                        AND bb.FTSubOrderNo = aa.FTSubOrderNo"
                    tSql &= Environment.NewLine & "                        AND bb.FTColorway = aa.FTColorway"
                    tSql &= Environment.NewLine & "                        AND bb.FTSizeBreakDown = aa.FTSizeBreakDown"
                    tSql &= Environment.NewLine & "                  );"

                    tSql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack( "
                    tSql &= Environment.NewLine & "   FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage, FBImage "
                    tSql &= Environment.NewLine & " )"
                    tSql &= Environment.NewLine & "  Select  A.FTOrderNo,A.FTSubOrderNo,P.FNPackSeq, P.FTPackDescription, P.FTPackNote,'' AS FTImage, P.FBImage "
                    tSql &= Environment.NewLine & "  FROM( Select DISTINCT B.FTOrderNo , B4.FTSubOrderNo, B.FNHSysStyleId, B.FNHSysSeasonId "
                    tSql &= Environment.NewLine & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] As AA  "
                    tSql &= Environment.NewLine & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As B  On AA.FTGenerateOrderNo = B.FTOrderNo     "
                    tSql &= Environment.NewLine & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] As B4  On B.FTOrderNo = B4.FTOrderNo "
                    tSql &= Environment.NewLine & "     WHERE AA.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= Environment.NewLine & " ) As A "
                    tSql &= Environment.NewLine & "   INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle_Packing As P With(NOLOCK) On A.FNHSysStyleId = P.FNHSysStyleId And A.FNHSysSeasonId = P.FNHSysSeasonId "



                    '...represent record sub order no / size breakdown use for compute extra quantity garment by vendor programme/main catetory BB:Basketball, FB:Football/plant/buy group
                    tSql &= Environment.NewLine & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] ([FTUserLogin]"
                    tSql &= Environment.NewLine & "             ,[FTOrderNo]"
                    tSql &= Environment.NewLine & "             ,[FTPORef]"
                    tSql &= Environment.NewLine & "             ,[FTSubOrderNo]"
                    tSql &= Environment.NewLine & "             ,[FNHSysMatColorId]"
                    tSql &= Environment.NewLine & "             ,[FNHSysMatSizeId]"
                    tSql &= Environment.NewLine & "             ,[FNQuantity]"
                    tSql &= Environment.NewLine & "             ,[FNPrice]"
                    tSql &= Environment.NewLine & "             ,[FNAmt]"
                    tSql &= Environment.NewLine & "             ,[FNExtraQty]"
                    tSql &= Environment.NewLine & "             ,[FNQuantityExtra]"
                    tSql &= Environment.NewLine & "             ,[FNGrandQuantity]"
                    tSql &= Environment.NewLine & "             ,[FNAmntExtra]"
                    tSql &= Environment.NewLine & "             ,[FNGrandAmnt]"
                    tSql &= Environment.NewLine & "             ,[FNGarmentQtyTest],[FTOrderSubNo2],[FTShipDate],[FNExtraType],[FNHSysStyleId],[FNHSysSeasonId],FNRowSeq)"
                    tSql &= Environment.NewLine & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTUserLogin,"
                    tSql &= Environment.NewLine & "       M2.FTOrderNo, M2.FTPORef, M3.FTSubOrderNo, "
                    tSql &= Environment.NewLine & "       M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                    tSql &= Environment.NewLine & "       M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                    tSql &= Environment.NewLine & "       M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,ISNULL(M1.FTGenerateOrderSubNo2,'') AS FTGenerateOrderSubNo2,M3.FDShipDate,M1.FNExtraType,M2.FNHSysStyleId,M2.FNHSysSeasonId,Row_Number() Over (Order By  M2.FTOrderNo) AS FNRowSeq"
                    tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2  ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 ON M2.FTOrderNo = M3.FTOrderNo AND M1.FTGenerateOrderSubNo = M3.FTSubOrderNo"
                    tSql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4  ON M3.FTOrderNo = M4.FTOrderNo"
                    tSql &= Environment.NewLine & "     AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                    tSql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                    tSql &= Environment.NewLine & " WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(M3.FNOrderSetType,0) <=1"
                    tSql &= Environment.NewLine & "           AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tSql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                    tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "

                    tSql &= Environment.NewLine & "       AND M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                    tSql &= Environment.NewLine & "ORDER BY M2.FTOrderNo ASC, M3.FTSubOrderNo ASC, M5.FNMatColorSeq ASC, M6.FNMatSizeSeq ASC;"

                    tSql &= Environment.NewLine & " Declare @SwapOrder As Table("
                    tSql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) Not NULL,"
                    tSql &= Environment.NewLine & "[FTOrderNoSwap] [nvarchar](30) Not NULL,"
                    tSql &= Environment.NewLine & "[FTState] [nvarchar](1) Not NULL)"
                    tSql &= Environment.NewLine & " INSERT INTO @SwapOrder  (FTOrderNo,FTOrderNoSwap,FTState) "
                    tSql &= Environment.NewLine & " SELECT  M1.FTGenerateOrderNo,M1.SwapOrderNo,'0'"
                    tSql &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] As M1  "

                    tSql &= Environment.NewLine & "  OUTER APPLY (SELECT TOP 1 '1' AS FTState  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Cancel] As M21 WITH(NOLOCK) WHERE M21.FTOrderNo=M1.FTGenerateOrderNo ) AS M21 "

                    tSql &= Environment.NewLine & "  OUTER APPLY (SELECT TOP 1 '1' AS FTState  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Divert] As M22 WITH(NOLOCK) WHERE M22.FTOrderNo=M1.FTGenerateOrderNo ) AS M22 "


                    tSql &= Environment.NewLine & "  WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(M1.SwapOrderNo,'') <> '' AND ISNULL(M1.FTGenerateOrderNo,'') <> '' AND ISNULL(M21.FTState,'') ='' AND ISNULL(M22.FTState,'') =''  "
                    tSql &= Environment.NewLine & " GROUP BY M1.FTGenerateOrderNo,M1.SwapOrderNo"
                    tSql &= Environment.NewLine & " IF ISNULL((SELECT TOP 1 '1' AS FTState FROM  @SwapOrder ),'') <>'' "
                    tSql &= Environment.NewLine & "      BEGIN "
                    tSql &= Environment.NewLine & "               INSERT INTO @SwapOrder  (FTOrderNo,FTOrderNoSwap,FTState)  "
                    tSql &= Environment.NewLine & "               SELECT FTOrderNoSwap,FTOrderNo,'1' "
                    tSql &= Environment.NewLine & "               FROM  @SwapOrder  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSwapFromOrderNo =  CASE WHEN FTState ='0' THEN B.FTOrderNo ELSE '' END ,A.FTSwapToOrderNo =CASE WHEN FTState ='1' THEN B.FTOrderNo ELSE '' END,A.FNJobState=CASE WHEN FTState ='1' THEN 2 ELSE A.FNJobState END "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap)  "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap) "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap) "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap) "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap) "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap) "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Bundle] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "


                    tSql &= Environment.NewLine & "               UPDATE A SET A.FTOrderNo = B.FTOrderNoSwap  ,A.FTSubOrderNo =  Replace( A.FTSubOrderNo,A.FTOrderNo,B.FTOrderNoSwap) "
                    tSql &= Environment.NewLine & "               FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] AS A INNER JOIN @SwapOrder AS B ON A.FTOrderNo = B.FTOrderNo  "

                    tSql &= Environment.NewLine & "               UPDATE A SET M1.FTGenerateOrderNo =M1.SwapOrderNo  "
                    tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] As M1   INNER JOIN @SwapOrder AS B ON M1.FTGenerateOrderNo = B.FTOrderNo "
                    tSql &= Environment.NewLine & "                WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(M1.SwapOrderNo,'') <> '' AND ISNULL(M1.FTGenerateOrderNo,'') <> ''  "
                    tSql &= Environment.NewLine & "      END "

                    If StateKepepData Then

                        tSql &= Environment.NewLine & " EXEC   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_TEMPDB) & "]..USP_IMPORTFILEEXCELNIKEPO_CFM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0,'" & HI.UL.ULF.rpQuoted(pcust) & "','" & HI.UL.ULF.rpQuoted(pmerteam) & "','" & HI.UL.ULF.rpQuoted(pbuy) & "' "

                    End If

                    '========================================================================================================================================================================
                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'MsgBox("Step Append New ImportOrderUpdExtraQtyTemp !!!")
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _bImportComplete = False
                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _bImportComplete = True
                    End If

                    'If HI.Conn.SQLConn.ExecuteOnly(tSql, Conn.DB.DataBaseName.DB_MERCHAN) <= 0 Then
                    '    'MsgBox("Step Append New Order No !!!")
                    '    HI.Conn.SQLConn.Tran.Rollback()
                    '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '    _bImportComplete = False
                    'Else
                    '    'HI.Conn.SQLConn.Tran.Commit()
                    '    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '    _bImportComplete = True
                    'End If

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = False
                End Try


            Else
                'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If

            'If _bImportComplete = True Then




            '    _Spls.UpdateInformation("Generate Factory Sub Order Extra And Test Quantity .....Please Wait")



            'End If

            If _bImportComplete = True Then

                If _bImportComplete = True Then


                    Dim Cmdstring As String = ""
                    Dim dtstss As New DataTable

                    Cmdstring = "  SELECT  DISTINCT C.FNHSysStyleId,C.FNHSysSeasonId   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A "
                    Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                    Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                    dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                    For Each R As DataRow In dtstss.Rows
                        csOrder.ConfirmSizeSpecToOrder(Val(R!FNHSysStyleId.ToString), Val(R!FNHSysSeasonId.ToString))
                    Next



                    Cmdstring = "  SELECT  DISTINCT C.FTOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A "
                    Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                    Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                    dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each R As DataRow In dtstss.Rows

                        Cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    Next




                    '==============================================================================================================================================================================
                    '...when execute import order complete next process update test garment and update (%) extra qty by vendor programme/main category type basketball, football/plant/buy group
                    '...called sproc execute for update extra garment qty/ test qty garment


                    tSql = ""
                    'tSql = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_UPDATE_IMPORT_ORDER_QTY_EXTRA_GARMENT_NEW N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_UPDATEIMPORTORDER_QTY_EXTRAGARMENT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                    If HI.Conn.SQLConn.ExecuteOnly(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                        tSql = ""
                        tSql = " UPDATE X"
                        tSql &= Environment.NewLine & " SET X.FNGrandQuantity = ISNULL(X.FNQuantity,0) + ISNULL(Y.FNQuantityExtra, 0)+ ISNULL(X.FNGarmentQtyTest, 0)+ ISNULL(X.FNExternalQtyTest, 0),"
                        tSql &= Environment.NewLine & "    X.FNAmntExtra = ISNULL(Y.FNQuantityExtra,0) * X.FNPrice,"
                        tSql &= Environment.NewLine & "    X.FNGrandAmnt = ISNULL(X.FNAmt, 0) + (ISNULL(Y.FNQuantityExtra, 0) * X.FNPrice) + (ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice),"
                        tSql &= Environment.NewLine & "    X.FNAmntQtyTest = ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice"
                        tSql &= Environment.NewLine & "   , X.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(X.FNExternalQtyTest,0) * X.FNPrice))"
                        tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS X INNER JOIN (SELECT M2.FTOrderNo, M3.FTSubOrderNo,"
                        tSql &= Environment.NewLine & "                                                                        M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                        tSql &= Environment.NewLine & "                                                                        M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                        tSql &= Environment.NewLine & "                                                                        M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,M4.FNExternalQtyTest"
                        tSql &= Environment.NewLine & "                                                                 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                        tSql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M3.FTOrderNo  AND M1.FTGenerateOrderSubNo =M3.FTSubOrderNo"
                        tSql &= Environment.NewLine & "  																     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
                        tSql &= Environment.NewLine & " 																               AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                        tSql &= Environment.NewLine & "   																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                        tSql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                        tSql &= Environment.NewLine & "                                                                 WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        tSql &= Environment.NewLine & "           AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                        tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                        tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        tSql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                        tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "

                        tSql &= Environment.NewLine & "                                                                       AND  M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                        tSql &= Environment.NewLine & " 															    ) AS Y ON X.FTOrderNo = Y.FTOrderNo"
                        tSql &= Environment.NewLine & " AND  X.FTSubOrderNo = Y.FTSubOrderNo"
                        tSql &= Environment.NewLine & "        AND X.FNHSysMatColorId = Y.FNHSysMatColorId"
                        tSql &= Environment.NewLine & "        AND X.FNHSysMatSizeId = Y.FNHSysMatSizeId"
                        tSql &= Environment.NewLine & "  UPDATE X"
                        tSql &= Environment.NewLine & " SET X.FNGrandQuantity = ISNULL(X.FNQuantity,0) + ISNULL(Y.FNQuantityExtra, 0)+ ISNULL(X.FNGarmentQtyTest, 0)+ ISNULL(X.FNExternalQtyTest, 0),"
                        tSql &= Environment.NewLine & "    X.FNAmntExtra = ISNULL(Y.FNQuantityExtra,0) * X.FNPrice,"
                        tSql &= Environment.NewLine & "    X.FNGrandAmnt = ISNULL(X.FNAmt, 0) + Convert(numeric(18,2),(ISNULL(Y.FNQuantityExtra, 0) * X.FNPrice)) + Convert(numeric(18,2),(ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice)),"
                        tSql &= Environment.NewLine & "    X.FNAmntQtyTest = Convert(numeric(18,2),(ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice))"
                        tSql &= Environment.NewLine & "   , X.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(X.FNExternalQtyTest,0) * X.FNPrice))"
                        tSql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS X INNER JOIN (SELECT M2.FTOrderNo, M3.FTSubOrderNo,"
                        tSql &= Environment.NewLine & "                                                                        M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                        tSql &= Environment.NewLine & "                                                                        M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                        tSql &= Environment.NewLine & "                                                                        M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,M4.FNExternalQtyTest"
                        tSql &= Environment.NewLine & "                                                                 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                        tSql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M3.FTOrderNo  AND M1.FTGenerateOrderSubNo2 =M3.FTSubOrderNo "
                        tSql &= Environment.NewLine & "  																     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
                        tSql &= Environment.NewLine & " 																               AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                        tSql &= Environment.NewLine & "   																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                        tSql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                        tSql &= Environment.NewLine & "                                                                 WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        tSql &= Environment.NewLine & "                                                                       AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                        tSql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                        tSql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        tSql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                        tSql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "

                        tSql &= Environment.NewLine & "                                                                       AND  M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                        tSql &= Environment.NewLine & " 															    ) AS Y ON X.FTOrderNo = Y.FTOrderNo"
                        tSql &= Environment.NewLine & " AND  X.FTSubOrderNo = Y.FTSubOrderNo"
                        tSql &= Environment.NewLine & "        AND X.FNHSysMatColorId = Y.FNHSysMatColorId"
                        tSql &= Environment.NewLine & "        AND X.FNHSysMatSizeId = Y.FNHSysMatSizeId"

                        If HI.Conn.SQLConn.ExecuteOnly(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then


                            _bImportComplete = True

                        Else

                            'MsgBox("Step Append New TMERTOrderSub_BreakDown !!!")
                            _bImportComplete = False

                        End If

                    End If
                    '==============================================================================================================================================================================
                Else
                    'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            End If

        Catch ex As Exception
            '  If Not System.Diagnostics.Debugger.IsAttached = True Then
            ' _Spls.Close()
            ' Else
            ' MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return _bImportComplete

    End Function


    Public Shared Function ImportFactoryOrderNetPrice(_Spls As HI.TL.SplashScreen, pImportOrderType As Integer, pCustId As Integer, pCmpRunId As Integer, CmpRunIdText As String, pBuyId As Integer, pMerteamId As Integer, ByRef pTTCompalte As Integer, ByRef pTTnotCompalte As Integer, Optional StateKepepData As Boolean = False) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub

        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""
        Dim _Qry As String = ""


        '_Qry = "   UPDATE OB SET FNNetPrice = A.FNPrice"
        '_Qry &= vbCrLf & " ,FNNetPriceOperateFee=(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END )"
        '_Qry &= vbCrLf & " ,FNNetPriceOperateFeeAmt=Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))"
        '_Qry &= vbCrLf & " ,FNNetNetPrice = A.FNPrice - (Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))) "
        '_Qry &= vbCrLf & " , FTNikePOLineItem=CASE WHEN ISNULL(OB.FTNikePOLineItem,'') ='' THEN ISNULL(A.FTPOItem,'')   ELSE  ISNULL(OB.FTNikePOLineItem,'')  END "
        '_Qry &= vbCrLf & " ,FTStateImportNetPrice='1'"
        '_Qry &= vbCrLf & " ,FTImportNetPriceBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '_Qry &= vbCrLf & " ,FDImportNetPriceDate=" & HI.UL.ULDate.FormatDateDB & ""
        '_Qry &= vbCrLf & " ,FTImportNetPriceTime=" & HI.UL.ULDate.FormatTimeDB & ""

        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As B On ST.FNHSysStyleId = B.FNHSysStyleId INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As ODS On B.FTOrderNo = ODS.FTOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As OB On ODS.FTOrderNo = OB.FTOrderNo And  ODS.FTSubOrderNo = OB.FTSubOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp As A On B.FTPORef = A.FTPONo And OB.FTColorway = A.FTColorwayCode"
        '_Qry &= vbCrLf & "           And OB.FTSizeBreakDown = A.FTSizeBreakdownCode And LEFT(ST.FTStyleCode,len(A.FTStyle)) = A.FTStyle And OB.FTNikePOLineItem =A.FTPOItem"

        '_Qry &= vbCrLf & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
        '_Qry &= vbCrLf & "          LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"

        '_Qry &= vbCrLf & " LEFT OUTER  JOIN ( Select A.FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItem"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice As A With(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D As B With(NOLOCK) On A.FTCustomerPO = B.FTCustomerPO And A.FTInvoiceNo = B.FTInvoiceNo"
        '_Qry &= vbCrLf & " WHERE ISNULL(A.FTPostUser,'') <>'' "
        '_Qry &= vbCrLf & " ) AS XM"
        '_Qry &= vbCrLf & " ON A.FTPONo = XM.FTCustomerPO"
        '_Qry &= vbCrLf & "	AND A.FTColorwayCode = XM.FTColorway"
        '_Qry &= vbCrLf & "	AND A.FTSizeBreakdownCode = XM.FTSizeBreakDown"
        '_Qry &= vbCrLf & "	AND A.FTPOItem = XM.FTPOLineItem "

        '_Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(ODS.FTPORef,'') ='' AND XM.FTCustomerPO IS NULL"


        '_Qry &= vbCrLf & "  UPDATE OB SET FNNetPrice = A.FNPrice"
        '_Qry &= vbCrLf & " ,FNNetPriceOperateFee=(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END )"
        '_Qry &= vbCrLf & " ,FNNetPriceOperateFeeAmt=Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))"
        '_Qry &= vbCrLf & " ,FNNetNetPrice = A.FNPrice - (Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))) "
        '_Qry &= vbCrLf & " , FTNikePOLineItem=CASE WHEN ISNULL(OB.FTNikePOLineItem,'') ='' THEN ISNULL(A.FTPOItem,'')   ELSE  ISNULL(OB.FTNikePOLineItem,'')  END "
        '_Qry &= vbCrLf & " ,FTStateImportNetPrice='1'"
        '_Qry &= vbCrLf & " ,FTImportNetPriceBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '_Qry &= vbCrLf & " ,FDImportNetPriceDate=" & HI.UL.ULDate.FormatDateDB & ""
        '_Qry &= vbCrLf & " ,FTImportNetPriceTime=" & HI.UL.ULDate.FormatTimeDB & ""

        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B ON ST.FNHSysStyleId = B.FNHSysStyleId INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS ODS ON B.FTOrderNo = ODS.FTOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OB ON ODS.FTOrderNo = OB.FTOrderNo AND  ODS.FTSubOrderNo = OB.FTSubOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp AS A ON ODS.FTPORef = A.FTPONo AND OB.FTColorway = A.FTColorwayCode"
        '_Qry &= vbCrLf & "           AND OB.FTSizeBreakDown = A.FTSizeBreakdownCode AND LEFT(ST.FTStyleCode,len(A.FTStyle)) = A.FTStyle AND OB.FTNikePOLineItem =A.FTPOItem"

        '_Qry &= vbCrLf & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
        '_Qry &= vbCrLf & "          LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"

        '_Qry &= vbCrLf & " LEFT OUTER  JOIN ( SELECT A.FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItem"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS B WITH(NOLOCK) ON A.FTCustomerPO = B.FTCustomerPO AND A.FTInvoiceNo = B.FTInvoiceNo"
        '_Qry &= vbCrLf & " WHERE ISNULL(A.FTPostUser,'') <>'' "
        '_Qry &= vbCrLf & " ) AS XM"
        '_Qry &= vbCrLf & "   ON  A.FTPONo = XM.FTCustomerPO "
        '_Qry &= vbCrLf & "	     AND A.FTColorwayCode = XM.FTColorway "
        '_Qry &= vbCrLf & "	     AND A.FTSizeBreakdownCode = XM.FTSizeBreakDown"
        '_Qry &= vbCrLf & "	     AND A.FTPOItem = XM.FTPOLineItem "
        '_Qry &= vbCrLf & "   WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND XM.FTCustomerPO IS NULL "


        '_Qry &= vbCrLf & "    UPDATE OB SET FNNetPrice = A.FNPrice"
        '_Qry &= vbCrLf & " ,FNNetPriceOperateFee=(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END )"
        '_Qry &= vbCrLf & ",FNNetPriceOperateFeeAmt=Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))"
        '_Qry &= vbCrLf & ",FNNetNetPrice = A.FNPrice - (Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))) "
        '_Qry &= vbCrLf & " , FTNikePOLineItem=CASE WHEN ISNULL(OB.FTNikePOLineItem,'') ='' THEN ISNULL(A.FTPOItem,'')   ELSE  ISNULL(OB.FTNikePOLineItem,'')  END "
        '_Qry &= vbCrLf & " ,FTStateImportNetPrice='1'"
        '_Qry &= vbCrLf & " ,FTImportNetPriceBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '_Qry &= vbCrLf & " ,FDImportNetPriceDate=" & HI.UL.ULDate.FormatDateDB & ""
        '_Qry &= vbCrLf & " ,FTImportNetPriceTime=" & HI.UL.ULDate.FormatTimeDB & ""
        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B ON ST.FNHSysStyleId = B.FNHSysStyleId INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS ODS ON B.FTOrderNo = ODS.FTOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS OB ON ODS.FTOrderNo = OB.FTOrderNo AND  ODS.FTSubOrderNo = OB.FTSubOrderNo AND   ODS.FNDivertSeq = OB.FNDivertSeq INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp AS A ON ODS.FTPORef = A.FTPONo AND CASE WHEN ISNULL(OB.FTColorwayNew,'') ='' THEN OB.FTColorway ELSE ISNULL(OB.FTColorwayNew,'') END  = A.FTColorwayCode"
        '_Qry &= vbCrLf & "           AND OB.FTSizeBreakDown = A.FTSizeBreakdownCode AND LEFT(ST.FTStyleCode,len(A.FTStyle)) = A.FTStyle AND OB.FTNikePOLineItem =A.FTPOItem"

        '_Qry &= vbCrLf & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
        '_Qry &= vbCrLf & "          LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"

        '_Qry &= vbCrLf & " LEFT OUTER  JOIN ( SELECT A.FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItem"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS B WITH(NOLOCK) ON A.FTCustomerPO = B.FTCustomerPO AND A.FTInvoiceNo = B.FTInvoiceNo"
        '_Qry &= vbCrLf & " WHERE ISNULL(A.FTPostUser,'') <>'' "
        '_Qry &= vbCrLf & " ) AS XM"
        '_Qry &= vbCrLf & " ON A.FTPONo = XM.FTCustomerPO"
        '_Qry &= vbCrLf & "	AND A.FTColorwayCode = XM.FTColorway"

        '_Qry &= vbCrLf & "	AND A.FTSizeBreakdownCode = XM.FTSizeBreakDown"
        '_Qry &= vbCrLf & "	AND A.FTPOItem = XM.FTPOLineItem "
        '_Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND XM.FTCustomerPO IS NULL "


        '_Qry &= vbCrLf & "   UPDATE OB SET FNNetPrice = A.FNPrice"
        '_Qry &= vbCrLf & " ,FNNetPriceOperateFee=(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END )"
        '_Qry &= vbCrLf & ",FNNetPriceOperateFeeAmt=Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))"
        '_Qry &= vbCrLf & ",FNNetNetPrice = A.FNPrice - (Convert(numeric(18,4),((A.FNPrice* ISNULL((CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END ),0))/100.00))) "
        '_Qry &= vbCrLf & " , FTNikePOLineItem=CASE WHEN ISNULL(OB.FTNikePOLineItem,'') ='' THEN ISNULL(A.FTPOItem,'')   ELSE  ISNULL(OB.FTNikePOLineItem,'')  END "
        '_Qry &= vbCrLf & " ,FTStateImportNetPrice='1'"
        '_Qry &= vbCrLf & " ,FTImportNetPriceBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        '_Qry &= vbCrLf & " ,FDImportNetPriceDate=" & HI.UL.ULDate.FormatDateDB & ""
        '_Qry &= vbCrLf & " ,FTImportNetPriceTime=" & HI.UL.ULDate.FormatTimeDB & ""
        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B ON ST.FNHSysStyleId = B.FNHSysStyleId INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS ODS ON B.FTOrderNo = ODS.FTOrderNo INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown AS OB ON ODS.FTOrderNo = OB.FTOrderNo AND  ODS.FTSubOrderNo = OB.FTSubOrderNo AND   ODS.FNDivertSeq = OB.FNDivertSeq INNER JOIN"
        '_Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderSizeBreakdownTemp AS A ON B.FTPORef = A.FTPONo AND CASE WHEN ISNULL(OB.FTColorwayNew,'') ='' THEN OB.FTColorway ELSE ISNULL(OB.FTColorwayNew,'') END = A.FTColorwayCode"
        '_Qry &= vbCrLf & "           AND OB.FTSizeBreakDown = A.FTSizeBreakdownCode AND LEFT(ST.FTStyleCode,len(A.FTStyle)) = A.FTStyle AND OB.FTNikePOLineItem =A.FTPOItem"

        '_Qry &= vbCrLf & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
        '_Qry &= vbCrLf & "          LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"

        '_Qry &= vbCrLf & " LEFT OUTER  JOIN ( SELECT A.FTCustomerPO, B.FTColorway, B.FTSizeBreakDown, B.FTPOLineItem"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice AS A WITH(NOLOCK) INNER JOIN"
        '_Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTXMLCreateInvoice_D AS B WITH(NOLOCK) ON A.FTCustomerPO = B.FTCustomerPO AND A.FTInvoiceNo = B.FTInvoiceNo"
        '_Qry &= vbCrLf & "        WHERE ISNULL(A.FTPostUser,'') <>'' "
        '_Qry &= vbCrLf & " ) AS XM"
        '_Qry &= vbCrLf & " ON A.FTPONo = XM.FTCustomerPO"
        '_Qry &= vbCrLf & "	AND A.FTColorwayCode = XM.FTColorway"
        '_Qry &= vbCrLf & "	AND A.FTSizeBreakdownCode = XM.FTSizeBreakDown"
        '_Qry &= vbCrLf & "	AND A.FTPOItem = XM.FTPOLineItem "

        '_Qry &= vbCrLf & " WHERE  (A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') AND ISNULL(ODS.FTPORef,'') =''  AND XM.FTCustomerPO IS NULL  "
        '_Qry &= vbCrLf & "  EXEC   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_TEMPDB) & "]..USP_IMPORTFILEEXCELNIKEPO_CFM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1 "

        _Qry = "  EXEC   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_TEMPDB) & "]..USP_IMPORTFILEEXCELNIKEPO_CFM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1 "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim TTCompalte As Integer = 0
        Dim TTNotCompalte As Integer = 0
        Try

            If dt.Rows.Count > 0 Then
                If dt.Columns.IndexOf("FNCmpplate") > 0 Then

                    TTCompalte = Val(dt.Rows(0)!FNCmpplate.ToString)
                    TTNotCompalte = Val(dt.Rows(0)!FNNotCmpplate.ToString)

                End If
            End If
        Catch ex As Exception

        End Try

        pTTCompalte = TTCompalte
        pTTnotCompalte = TTNotCompalte

        Return True

    End Function


End Class
