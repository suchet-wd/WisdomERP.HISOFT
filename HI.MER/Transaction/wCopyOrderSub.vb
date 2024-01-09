Imports System.Windows.Forms
Imports System.Windows.Forms.Control

Public Class wCopyOrderSub

#Region "Variable Declaration"
    Private tSql As String
    Private _tFTOrderNo As String
    Private _tFTSubOrderNo As String
    Private __SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private _wListCompleteCopySubOrder As wListCompleteCopySubOrder
#End Region

#Region "Proceduren And Function"

    Public Sub New(ByVal ptOrderNo As String, ByVal ptSubOrderNo As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _tFTOrderNo = ptOrderNo
        _tFTSubOrderNo = ptSubOrderNo
        Me.FTSubOrderNo.Text = _tFTSubOrderNo
        Me.FNCopySubOrderNo.Value = 1

        '===================================================== _wListCompleteCopySubOrder =======================================================
        _wListCompleteCopySubOrder = New wListCompleteCopySubOrder(Nothing)

        HI.TL.HandlerControl.AddHandlerObj(_wListCompleteCopySubOrder)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wListCompleteCopySubOrder.Name.ToString.Trim, _wListCompleteCopySubOrder)
        Catch ex As Exception
            '...Nothing 
        End Try
        '==================================================================================================================================

    End Sub

    Private Function W_PRCbCopySubOrderNo_BACKUP_20150119() As Boolean
        Dim _bFlagCopy As Boolean = False
        Dim _Spls As HI.TL.SplashScreen

        Dim oDBdt As DataTable
        Dim oDBdtOrderSub As DataTable
        Dim oDBdtOrderSub_Breakdown As DataTable
        Dim oDBdtOrderSub_Sew As DataTable
        Dim oDBdtOrderSub_Pack As DataTable
        Dim oDBdtOrderSub_SizeSpec As DataTable

        Dim tFDInsDate As String, tFTInsTime As String, tFTInsUser As String
        Dim _tFTSubOrderNoSrc As String, _tFTSubOrderNoDest As String
        Dim nFNCopySubOrderNo As Integer, nLoopCopySubOrder As Integer
        Dim _tFTOrderNoDest As String = _tFTOrderNo


        Try
            nFNCopySubOrderNo = Me.FNCopySubOrderNo.Value

            If nFNCopySubOrderNo < 1 Then
                Dim tMsgTitle As String

                'Select Case HI.ST.Lang.Language
                '    Case HI.ST.Lang.Lang.EN
                '        tMsgTitle = "Copy Factory Sub Order No."
                '    Case HI.ST.Lang.Lang.TH
                '        tMsgTitle = "สำเนารายการเลขที่ใบสั่งผลิตย่อย"
                '    Case Else
                '        tMsgTitle = "Copy Factory Sub Order No."
                'End Select

                Select Case True
                    Case HI.ST.Lang.eLang.EN
                        tMsgTitle = "Copy Factory Sub Order No."
                    Case HI.ST.Lang.eLang.TH
                        tMsgTitle = "สำเนารายการเลขที่ใบสั่งผลิตย่อย"
                    Case Else
                        tMsgTitle = "Copy Factory Sub Order No."
                End Select

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, "", Me.FNCopySubOrderNo_lbl.Text)
                Me.FNCopySubOrderNo.Focus()

                Return False
            End If

            If Not HI.MG.ShowMsg.mConfirmProcess("", 1403110005, Me.FTSubOrderNo.Text.Trim()) = True Then Return False

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")
            End If

            _tFTSubOrderNoSrc = _tFTSubOrderNo

            tSql = "DECLARE @FDInsDate AS VARCHAR(10);"
            tSql &= Environment.NewLine & "DECLARE @FTInsTime AS VARCHAR(8);"
            tSql &= Environment.NewLine & "SELECT @FDInsDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTInsTime = CONVERT(VARCHAR(10), GETDATE(), 114);"
            tSql &= Environment.NewLine & "SELECT ISNULL(@FDInsDate,'') AS FDInsDate, ISNULL(@FTInsTime, '') AS FTInsTime;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdt.Rows.Count > 0 Then
                tFDInsDate = "'" & oDBdt.Rows(0).Item("FDInsDate") & "'"
                tFTInsTime = "'" & oDBdt.Rows(0).Item("FTInsTime") & "'"
            Else
                tFDInsDate = HI.UL.ULDate.FormatDateDB
                tFTInsTime = HI.UL.ULDate.FormatTimeDB
            End If

            tFTInsUser = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)


            _bFlagCopy = True
            nLoopCopySubOrder = 1

            Try

                Do While (_bFlagCopy) AndAlso (nLoopCopySubOrder <= nFNCopySubOrderNo)

                    '...generate new sub order no
                    tSql = ""
                    tSql = "EXEC SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"

                    _tFTSubOrderNoDest = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "")

                    REM 201412/18 drop field TMERTOrderSub Amount, Qty
                    'tSql = ""
                    'tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    'tSql &= vbCrLf & ", FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId"
                    'tSql &= vbCrLf & ", FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState"
                    'tSql &= vbCrLf & ", FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1"
                    'tSql &= vbCrLf & ", FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNSubOrderQty, FNSubOrderAmt, FNHSysShipPortId"
                    'tSql &= vbCrLf & ", FNSubOrderExtraQty, FNSubOrderExtraAmt,FDShipDateOrginal, FTCustRef, FNSubOrderGarmentTestQty, FNSubOrderGarmentTestAmnt, FNPackCartonSubType, FNPackPerCarton)"
                    'tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                    'tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    'tSql &= vbCrLf & " , FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId,"
                    'tSql &= vbCrLf & " FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,"
                    'tSql &= vbCrLf & " FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark,0 AS  FNSubOrderQty,0 AS  FNSubOrderAmt, FNHSysShipPortId,0 AS  FNSubOrderExtraQty,0 AS  FNSubOrderExtraAmt,"
                    'tSql &= vbCrLf & " FDShipDateOrginal, FTCustRef,0 AS  FNSubOrderGarmentTestQty,0 AS  FNSubOrderGarmentTestAmnt, FNPackCartonSubType, 0 AS FNPackPerCarton"
                    'tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] WITH(NOLOCK)"
                    'tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    tSql &= vbCrLf & ", FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId"
                    tSql &= vbCrLf & ", FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState"
                    tSql &= vbCrLf & ", FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1"
                    tSql &= vbCrLf & ", FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId"
                    tSql &= vbCrLf & ", FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton)"
                    tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & " , FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId,"
                    tSql &= vbCrLf & " FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,"
                    tSql &= vbCrLf & " FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId,"
                    tSql &= vbCrLf & " FDShipDateOrginal, FTCustRef, FNPackCartonSubType, 0 AS FNPackPerCarton"
                    tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] WITH(NOLOCK)"
                    tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_Breakdown

                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown]"
                    tSql &= vbCrLf & " ("
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    tSql &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId,"
                    tSql &= vbCrLf & "  FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest"
                    tSql &= vbCrLf & " )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                    tSql &= vbCrLf & ",FTColorway, FTSizeBreakDown, FNPrice,0 AS  FNQuantity,0 AS  FNAmt, FNHSysMatColorId, "
                    tSql &= vbCrLf & "  FNHSysMatSizeId, 0 AS  FNExtraQty, 0 AS  FNQuantityExtra,0 AS   FNGrandQuantity,0 AS  FNAmntExtra,0 AS  FNGrandAmnt,0 AS  FNGarmentQtyTest, 0 AS  FNAmntQtyTest"
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_Sew
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    tSql = ""
                    tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage"
                    tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                    tSql &= Environment.NewLine & "ORDER BY A.FNSewSeq ASC;"

                    Dim tmpDTSew As System.Data.DataTable

                    tmpDTSew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each oDataRowSew As System.Data.DataRow In tmpDTSew.Rows
                        Dim oImageSew As System.Drawing.Image
                        Dim FTImageSew As String
                        Dim FNSewSeq As Integer

                        FTImageSew = oDataRowSew!FTImage.ToString
                        FNSewSeq = Val(oDataRowSew!FNSewSeq.ToString)

                        oImageSew = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\" & FTImageSew)

                        If Not oImageSew Is Nothing Then
                            FTImageSew = _tFTSubOrderNoDest & "_" & FNSewSeq.ToString
                            FTImageSew = Microsoft.VisualBasic.Replace(FTImageSew, "-", "_")

                            tSql = ""
                            tSql = "UPDATE A"
                            tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImageSew, FTImageSew, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\") & "'"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & FNSewSeq & ";"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    Next

                    tmpDTSew.Dispose()

                    '...TMERTOrderSub_Pack
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                    tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] WITH(NOLOCK) "
                    tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    tSql = ""
                    tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage"
                    tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                    tSql &= Environment.NewLine & "ORDER BY A.FNPackSeq ASC;"

                    Dim tmpDTPack As System.Data.DataTable

                    tmpDTPack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each oDataRowPack As System.Data.DataRow In tmpDTPack.Rows
                        Dim oImagePack As System.Drawing.Image
                        Dim FTImagePack As String
                        Dim FNPackSeq As Integer

                        FTImagePack = oDataRowPack!FTImage.ToString
                        FNPackSeq = Val(oDataRowPack!FNPackSeq.ToString)
                        oImagePack = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\" & FTImagePack)

                        If Not oImagePack Is Nothing Then
                            FTImagePack = _tFTSubOrderNoDest & "_" & FNPackSeq.ToString
                            FTImagePack = Microsoft.VisualBasic.Replace(FTImagePack, "-", "_")

                            tSql = ""
                            tSql = "UPDATE A"
                            tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImagePack, FTImagePack, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "'"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A "
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & FNPackSeq & ";"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    Next

                    tmpDTPack.Dispose()

                    '...TMERTOrderSub_Bundle
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle]([FTInsUser], [FDInsDate], [FTInsTime], [FTOrderNo], [FTSubOrderNo], [FTColorway], [FTSizeBreakDown], [FNQuantity])"
                    tSql &= Environment.NewLine & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser, " & HI.UL.ULDate.FormatDateDB & " AS FDInsDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime, N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "' AS FTOrderNo, N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' AS FTSubOrderNo, A.[FTColorway], A.[FTSizeBreakDown], 0 AS FNQuantity"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle] AS A (NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNO = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "';"

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_SizeSpec
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension "
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension "
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_Component
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    nLoopCopySubOrder = nLoopCopySubOrder + 1

                    Application.DoEvents()
                Loop

                'Select Case HI.ST.Lang.Language
                '    Case HI.ST.Lang.Lang.EN
                '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                '    Case HI.ST.Lang.Lang.TH
                '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "ดำเนินการคัดลอกรายการใบสั่งผลิตย่อยเรียบร้อยแล้ว...")
                '    Case Else
                '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                'End Select

                Select Case True
                    Case HI.ST.Lang.eLang.EN
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                    Case HI.ST.Lang.eLang.TH
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "ดำเนินการคัดลอกรายการใบสั่งผลิตย่อยเรียบร้อยแล้ว...")
                    Case Else
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                End Select

                _bFlagCopy = True

            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Question, My.Application.Info.Title)
                End If
                _bFlagCopy = False

            End Try

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls.Close()
            End If

        Catch ex As Exception
            _bFlagCopy = False
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

        Return _bFlagCopy

    End Function

    Private Function W_PRCbCopySubOrderNo_Old() As Boolean
        Dim _bFlagCopy As Boolean = False
        Dim _Spls As HI.TL.SplashScreen

        Dim oDBdt As DataTable
        Dim oDBdtOrderSub As DataTable
        Dim oDBdtOrderSub_Breakdown As DataTable
        Dim oDBdtOrderSub_Sew As DataTable
        Dim oDBdtOrderSub_Pack As DataTable
        Dim oDBdtOrderSub_SizeSpec As DataTable

        Dim tFDInsDate As String, tFTInsTime As String, tFTInsUser As String
        Dim _tFTSubOrderNoSrc As String, _tFTSubOrderNoDest As String
        Dim nFNCopySubOrderNo As Integer, nLoopCopySubOrder As Integer

        Try
            nFNCopySubOrderNo = Me.FNCopySubOrderNo.Value

            If nFNCopySubOrderNo < 1 Then
                Dim tMsgTitle As String

                'Select Case HI.ST.Lang.Language
                '    Case HI.ST.Lang.Lang.EN
                '        tMsgTitle = "Copy Factory Sub Order No."
                '    Case HI.ST.Lang.Lang.TH
                '        tMsgTitle = "สำเนารายการเลขที่ใบสั่งผลิตย่อย"
                '    Case Else
                '        tMsgTitle = "Copy Factory Sub Order No."
                'End Select

                Select Case True
                    Case HI.ST.Lang.eLang.EN
                        tMsgTitle = "Copy Factory Sub Order No."
                    Case HI.ST.Lang.eLang.TH
                        tMsgTitle = "สำเนารายการเลขที่ใบสั่งผลิตย่อย"
                    Case Else
                        tMsgTitle = "Copy Factory Sub Order No."
                End Select

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, "", Me.FNCopySubOrderNo_lbl.Text)
                Me.FNCopySubOrderNo.Focus()

                Return False
            End If

            If Not HI.MG.ShowMsg.mConfirmProcess("", 1403110005, Me.FTSubOrderNo.Text.Trim()) = True Then Return False

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")
            End If

            _tFTSubOrderNoSrc = _tFTSubOrderNo

            tSql = "DECLARE @FDInsDate AS VARCHAR(10);"
            tSql &= Environment.NewLine & "DECLARE @FTInsTime AS VARCHAR(8);"
            tSql &= Environment.NewLine & "SELECT @FDInsDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTInsTime = CONVERT(VARCHAR(10), GETDATE(), 114);"
            tSql &= Environment.NewLine & "SELECT ISNULL(@FDInsDate,'') AS FDInsDate, ISNULL(@FTInsTime, '') AS FTInsTime;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdt.Rows.Count > 0 Then
                tFDInsDate = "'" & oDBdt.Rows(0).Item("FDInsDate") & "'"
                tFTInsTime = "'" & oDBdt.Rows(0).Item("FTInsTime") & "'"
            Else
                tFDInsDate = HI.UL.ULDate.FormatDateDB
                tFTInsTime = HI.UL.ULDate.FormatTimeDB
            End If

            tFTInsUser = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)

            '...TMERTOrderSub
            tSql = ""
            tSql = "SELECT A.[FTInsUser],A.[FDInsDate],A.[FTInsTime],A.[FTUpdUser]"
            tSql &= Environment.NewLine & "      ,A.[FDUpdDate],A.[FTUpdTime],A.[FTOrderNo],A.[FTSubOrderNo]"
            tSql &= Environment.NewLine & "      ,A.[FDSubOrderDate],A.[FTSubOrderBy],A.[FDProDate],A.[FDShipDate]"
            tSql &= Environment.NewLine & "      ,A.[FNHSysBuyId],A.[FNHSysContinentId],A.[FNHSysCountryId],A.[FNHSysProvinceId]"
            tSql &= Environment.NewLine & "      ,A.[FNHSysShipModeId],A.[FNHSysCurId],A.[FNHSysGenderId],A.[FNHSysUnitId]"
            tSql &= Environment.NewLine & "      ,A.[FNSubOrderState],A.[FTStateEmb],A.[FTStatePrint],A.[FTStateHeat]"
            tSql &= Environment.NewLine & "      ,A.[FTStateLaser],A.[FTStateWindows],A.[FTStateOther1],A.[FTOther1Note]"
            tSql &= Environment.NewLine & "      ,A.[FTStateOther2],A.[FTOther2Note],A.[FTStateOther3],A.[FTOther3Note1]"
            tSql &= Environment.NewLine & "      ,A.[FTRemark],A.[FNSubOrderQty],A.[FNSubOrderAmt],A.[FNHSysShipPortId]"
            tSql &= Environment.NewLine & "      ,A.[FNSubOrderExtraQty],A.[FNSubOrderExtraAmt]"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

            oDBdtOrderSub = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '...TMERTOrderSub_BreakDown
            tSql = ""
            tSql = "SELECT A.[FTInsUser],A.[FDInsDate],A.[FTInsTime],A.[FTUpdUser]"
            tSql &= Environment.NewLine & "      ,A.[FDUpdDate],A.[FTUpdTime],A.[FTOrderNo],A.[FTSubOrderNo]"
            tSql &= Environment.NewLine & "      ,A.[FTColorway],A.[FTSizeBreakDown],A.[FNPrice],A.[FNQuantity]"
            tSql &= Environment.NewLine & "      ,A.[FNAmt],A.[FNHSysMatColorId],A.[FNHSysMatSizeId],A.[FNExtraQty]"
            tSql &= Environment.NewLine & "      ,A.[FNQuantityExtra],A.[FNGrandQuantity],A.[FNAmntExtra],A.[FNGrandAmnt]"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

            oDBdtOrderSub_Breakdown = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)


            '...TMERTOrderSub_Sew
            tSql = ""
            tSql = "SELECT A.[FTInsUser],A.[FDInsDate],A.[FTInsTime],A.[FTUpdUser]"
            tSql &= Environment.NewLine & "      ,A.[FDUpdDate],A.[FTUpdTime],A.[FTOrderNo],A.[FTSubOrderNo]"
            tSql &= Environment.NewLine & "      ,A.[FNSewSeq],A.[FTSewDescription],A.[FTSewNote],A.[FTImage]"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

            oDBdtOrderSub_Sew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '...TMERTOrderSub_Pack
            tSql = ""
            tSql = "SELECT A.[FTInsUser],A.[FDInsDate],A.[FTInsTime],A.[FTUpdUser]"
            tSql &= Environment.NewLine & "      ,A.[FDUpdDate],A.[FTUpdTime],A.[FTOrderNo],A.[FTSubOrderNo]"
            tSql &= Environment.NewLine & "      ,A.[FNPackSeq],A.[FTPackDescription],A.[FTPackNote],A.[FTImage]"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
            tSql &= Environment.NewLine & "       AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

            oDBdtOrderSub_Pack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '...TMERTOrderSub_SizeSpec
            tSql = ""
            tSql = "SELECT A.[FTInsUser],A.[FDInsDate],A.[FTInsTime],A.[FTUpdUser]"
            tSql &= Environment.NewLine & "       ,A.[FDUpdDate],A.[FTUpdTime],A.[FTOrderNo],A.[FTSubOrderNo]"
            tSql &= Environment.NewLine & "       ,A.[FNSeq],A.[FNHSysMatSizeId],A.[FTSizeSpecDesc],A.[FTSizeSpecExtension]"
            tSql &= Environment.NewLine & "FROM [HITECH_MERCHAN].[dbo].[TMERTOrderSub_SizeSpec] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
            tSql &= Environment.NewLine & "       AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

            oDBdtOrderSub_SizeSpec = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            _bFlagCopy = True
            nLoopCopySubOrder = 1

            Try
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Do While (_bFlagCopy) AndAlso (nLoopCopySubOrder <= nFNCopySubOrderNo)

                    '...generate new sub order no
                    tSql = ""
                    tSql = "EXEC SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"

                    _tFTSubOrderNoDest = HI.Conn.SQLConn.GetFieldOnBeginTrans(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "")

                    tSql = ""
                    tSql = "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNo) & "'"
                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                        _bFlagCopy = False
                    Else
                        '...TMERTOrderSub
                        tSql = ""
                        tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FTStateLaser],[FTStateWindows],[FTStateOther1],[FTOther1Note]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FTRemark],[FNSubOrderQty],[FNSubOrderAmt],[FNHSysShipPortId]"
                        tSql &= Environment.NewLine & "                                                                                   ,[FNSubOrderExtraQty],[FNSubOrderExtraAmt])"
                        tSql &= Environment.NewLine & "VALUES ('" & tFTInsUser & "'," & tFDInsDate & "," & tFTInsTime & ",'" & tFTInsUser & "',"
                        tSql &= tFDInsDate & "," & tFTInsTime & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "','" & _tFTSubOrderNoDest & "',"
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FDSubOrderDate")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FDSubOrderDate") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTSubOrderBy")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTSubOrderBy") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FDProDate")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FDProDate") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FDShipDate")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FDShipDate") & "'") & ","
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysBuyId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysBuyId")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysContinentId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysContinentId")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysCountryId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysCountryId")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysProvinceId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysProvinceId")) & ","
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysShipModeId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysShipModeId")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysCurId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysCurId")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysGenderId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysGenderId")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysUnitId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysUnitId")) & ","
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNSubOrderState")), "NULL", oDBdtOrderSub.Rows(0).Item("FNSubOrderState")) & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateEmb")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateEmb") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStatePrint")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStatePrint") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateHeat")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateHeat") & "'") & ","
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateLaser")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateLaser") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateWindows")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateWindows") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateOther1")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateOther1") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTOther1Note")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTOther1Note") & "'") & ","
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateOther2")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateOther2") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTOther2Note")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTOther2Note") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTStateOther3")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTStateOther3") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTOther3Note1")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTOther3Note1") & "'") & ","
                        tSql &= IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FTRemark")), "NULL", "'" & oDBdtOrderSub.Rows(0).Item("FTRemark") & "'") & ", NULL, NULL, " & IIf(IsDBNull(oDBdtOrderSub.Rows(0).Item("FNHSysShipPortId")), "NULL", oDBdtOrderSub.Rows(0).Item("FNHSysShipPortId")) & ","
                        tSql &= "NULL, NULL)"

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                            _bFlagCopy = False
                        End If

                        '...TMERTOrderSub_Breakdown
                        If oDBdtOrderSub_Breakdown.Rows.Count > 0 Then
                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                _bFlagCopy = False
                            Else
                                Dim nLoopSubOrderBreakdown As Integer, nCntSubOrderBreakdown As Integer

                                nLoopSubOrderBreakdown = oDBdtOrderSub_Breakdown.Rows.Count - 1
                                nCntSubOrderBreakdown = 0

                                Do While (_bFlagCopy = True) AndAlso (nCntSubOrderBreakdown <= nLoopSubOrderBreakdown)
                                    tSql = ""
                                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                                    tSql &= Environment.NewLine & "                                                                                         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                                    tSql &= Environment.NewLine & "                                                                                         ,[FTColorway],[FTSizeBreakDown],[FNPrice],[FNQuantity]"
                                    tSql &= Environment.NewLine & "                                                                                         ,[FNAmt],[FNHSysMatColorId],[FNHSysMatSizeId],[FNExtraQty]"
                                    tSql &= Environment.NewLine & "                                                                                         ,[FNQuantityExtra],[FNGrandQuantity],[FNAmntExtra],[FNGrandAmnt])"
                                    tSql &= Environment.NewLine & "VALUES ('" & tFTInsUser & "'," & tFDInsDate & "," & tFTInsTime & ",'" & tFTInsUser & "',"
                                    tSql &= tFDInsDate & "," & tFTInsTime & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "','"
                                    tSql &= oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FTColorway") & "','" & oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FTSizeBreakDown") & "'," & IIf(IsDBNull(oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FNPrice")), "NULL", oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FNPrice")) & ", NULL,"
                                    tSql &= "NULL, " & IIf(IsDBNull(oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FNHSysMatColorId")), "NULL", oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FNHSysMatColorId")) & "," & IIf(IsDBNull(oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FNHSysMatSizeId")), "NULL", oDBdtOrderSub_Breakdown.Rows(nCntSubOrderBreakdown).Item("FNHSysMatSizeId")) & ", NULL,"
                                    tSql &= "NULL, NULL, NULL, NULL)"

                                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                        _bFlagCopy = False
                                    End If

                                    nCntSubOrderBreakdown = nCntSubOrderBreakdown + 1

                                    Application.DoEvents()
                                Loop

                            End If

                        End If

                        '...TMERTOrderSub_Sew
                        If oDBdtOrderSub_Sew.Rows.Count > 0 Then
                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                _bFlagCopy = False
                            Else
                                Dim nLoopSubSew As Integer, nCntSubSew As Integer

                                nLoopSubSew = oDBdtOrderSub_Sew.Rows.Count - 1
                                nCntSubSew = 0

                                Do While (_bFlagCopy = True) AndAlso (nCntSubSew <= nLoopSubSew)
                                    tSql = ""
                                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                                    tSql &= Environment.NewLine & "                                                                                   ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                                    tSql &= Environment.NewLine & "                                                                                   ,[FNSewSeq],[FTSewDescription],[FTSewNote],[FTImage])"
                                    tSql &= Environment.NewLine & "VALUES ('" & tFTInsUser & "'," & tFDInsDate & "," & tFTInsTime & ",'" & tFTInsUser & "',"
                                    tSql &= tFDInsDate & "," & tFTInsTime & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "',"
                                    tSql &= oDBdtOrderSub_Sew.Rows(nCntSubSew).Item("FNSewSeq") & "," & IIf(IsDBNull(oDBdtOrderSub_Sew.Rows(nCntSubSew).Item("FTSewDescription")), "NULL", "'" & oDBdtOrderSub_Sew.Rows(nCntSubSew).Item("FTSewDescription") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub_Sew.Rows(nCntSubSew).Item("FTSewNote")), "NULL", "'" & oDBdtOrderSub_Sew.Rows(nCntSubSew).Item("FTSewNote") & "'") & ", NULL)"

                                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                        _bFlagCopy = False
                                    End If

                                    nCntSubSew = nCntSubSew = 1

                                    Application.DoEvents()
                                Loop

                            End If

                        End If

                        '...TMERTOrderSub_Pack
                        If oDBdtOrderSub_Pack.Rows.Count > 0 Then
                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                _bFlagCopy = False
                            Else
                                Dim nLoopSubPack As Integer, nCntSubPack As Integer

                                nLoopSubPack = oDBdtOrderSub_Pack.Rows.Count - 1
                                nCntSubPack = 0

                                Do While (_bFlagCopy = True) AndAlso (nCntSubPack <= nLoopSubPack)

                                    tSql = ""
                                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                                    tSql &= Environment.NewLine & "                                                                                    ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                                    tSql &= Environment.NewLine & "                                                                                    ,[FNPackSeq],[FTPackDescription],[FTPackNote],[FTImage])"
                                    tSql &= Environment.NewLine & "VALUES ('" & tFTInsUser & "'," & tFDInsDate & "," & tFTInsTime & ",'" & tFTInsUser & "',"
                                    tSql &= tFDInsDate & "," & tFTInsTime & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "',"
                                    tSql &= oDBdtOrderSub_Pack.Rows(nCntSubPack).Item("FNPackSeq") & "," & IIf(IsDBNull(oDBdtOrderSub_Pack.Rows(nCntSubPack).Item("FTPackDescription")), "NULL", "'" & oDBdtOrderSub_Pack.Rows(nCntSubPack).Item("FTPackDescription") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub_Pack.Rows(nCntSubPack).Item("FTPackNote")), "NULL", "'" & oDBdtOrderSub_Pack.Rows(nCntSubPack).Item("FTPackNote") & "'") & ", NULL)"

                                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                        _bFlagCopy = False
                                    End If

                                    nCntSubPack = nCntSubPack + 1

                                    Application.DoEvents()
                                Loop

                            End If

                        End If

                        '...TMERTOrderSub_SizeSpec
                        If oDBdtOrderSub_SizeSpec.Rows.Count > 0 Then
                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                _bFlagCopy = False
                            Else
                                Dim nLoopSizeSpec As Integer, nCntSizeSpec As Integer

                                nLoopSizeSpec = oDBdtOrderSub_SizeSpec.Rows.Count - 1
                                nCntSizeSpec = 0

                                Do While (_bFlagCopy = True) AndAlso (nCntSizeSpec <= nLoopSizeSpec)
                                    tSql = ""
                                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                                    tSql &= Environment.NewLine & "                                                                                        ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                                    tSql &= Environment.NewLine & "                                                                                        ,[FNSeq],[FNHSysMatSizeId],[FTSizeSpecDesc],[FTSizeSpecExtension])"
                                    tSql &= Environment.NewLine & "VALUES ('" & tFTInsUser & "'," & tFDInsDate & "," & tFTInsTime & ",'" & tFTInsUser & "',"
                                    tSql &= tFDInsDate & "," & tFTInsTime & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "',"
                                    tSql &= oDBdtOrderSub_SizeSpec.Rows(nCntSizeSpec).Item("FNSeq") & "," & oDBdtOrderSub_SizeSpec.Rows(nCntSizeSpec).Item("FNHSysMatSizeId") & "," & IIf(IsDBNull(oDBdtOrderSub_SizeSpec.Rows(nCntSizeSpec).Item("FTSizeSpecDesc")), "NULL", "'" & oDBdtOrderSub_SizeSpec.Rows(nCntSizeSpec).Item("FTSizeSpecDesc") & "'") & "," & IIf(IsDBNull(oDBdtOrderSub_SizeSpec.Rows(nCntSizeSpec).Item("FTSizeSpecExtension")), "NULL", "'" & oDBdtOrderSub_SizeSpec.Rows(nCntSizeSpec).Item("FTSizeSpecExtension") & "'") & ")"

                                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) < 0 Then
                                        _bFlagCopy = False
                                    End If

                                    nCntSizeSpec = nCntSizeSpec + 1

                                    Application.DoEvents()
                                Loop

                            End If

                        End If

                    End If

                    nLoopCopySubOrder = nLoopCopySubOrder + 1

                    Application.DoEvents()
                Loop

                If _bFlagCopy = True AndAlso ((nLoopCopySubOrder - 1) = nFNCopySubOrderNo) Then
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    'Select Case HI.ST.Lang.Language
                    '    Case HI.ST.Lang.Lang.EN
                    '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                    '    Case HI.ST.Lang.Lang.TH
                    '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "ดำเนินการคัดลอกรายการใบสั่งผลิตย่อยเรียบร้อยแล้ว...")
                    '    Case Else
                    '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                    'End Select

                    Select Case True
                        Case HI.ST.Lang.eLang.EN
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                        Case HI.ST.Lang.eLang.TH
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "ดำเนินการคัดลอกรายการใบสั่งผลิตย่อยเรียบร้อยแล้ว...")
                        Case Else
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                    End Select

                Else
                    _bFlagCopy = False
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End If

            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Question, My.Application.Info.Title)
                End If
                _bFlagCopy = False
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            End Try

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls.Close()
            End If

        Catch ex As Exception
            _bFlagCopy = False
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

        Return _bFlagCopy

    End Function

    Private Function W_PRCbCopySubOrderNo() As Boolean
        Dim _bFlagCopy As Boolean = False
        Dim _Spls As HI.TL.SplashScreen

        Dim oDBdt As DataTable
        Dim oDBdtOrderSub As DataTable
        Dim oDBdtOrderSub_Breakdown As DataTable
        Dim oDBdtOrderSub_Sew As DataTable
        Dim oDBdtOrderSub_Pack As DataTable
        Dim oDBdtOrderSub_SizeSpec As DataTable

        Dim tmpDTListCopyOrderSubComplete As System.Data.DataTable

        Dim tFDInsDate As String, tFTInsTime As String, tFTInsUser As String
        Dim _tFTSubOrderNoSrc As String, _tFTSubOrderNoDest As String
        Dim nFNCopySubOrderNo As Integer, nLoopCopySubOrder As Integer
        Dim _tFTOrderNoDest As String = _tFTOrderNo

        Try
            nFNCopySubOrderNo = Me.FNCopySubOrderNo.Value

            If nFNCopySubOrderNo < 1 Then
                Dim tMsgTitle As String

                Select Case True
                    Case HI.ST.Lang.eLang.EN
                        tMsgTitle = "Copy Factory Sub Order No."
                    Case HI.ST.Lang.eLang.TH
                        tMsgTitle = "สำเนารายการเลขที่ใบสั่งผลิตย่อย"
                    Case Else
                        tMsgTitle = "Copy Factory Sub Order No."
                End Select

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, "", Me.FNCopySubOrderNo_lbl.Text)
                Me.FNCopySubOrderNo.Focus()

                Return False
            End If

            If Not HI.MG.ShowMsg.mConfirmProcess("", 1403110005, Me.FTSubOrderNo.Text.Trim()) = True Then Return False

            If Not System.Diagnostics.Debugger.IsAttached = True Then
                _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")
            End If

            _tFTSubOrderNoSrc = _tFTSubOrderNo

            tSql = "DECLARE @FDInsDate AS VARCHAR(10);"
            tSql &= Environment.NewLine & "DECLARE @FTInsTime AS VARCHAR(8);"
            tSql &= Environment.NewLine & "SELECT @FDInsDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTInsTime = CONVERT(VARCHAR(10), GETDATE(), 114);"
            tSql &= Environment.NewLine & "SELECT ISNULL(@FDInsDate,'') AS FDInsDate, ISNULL(@FTInsTime, '') AS FTInsTime;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdt.Rows.Count > 0 Then
                tFDInsDate = "'" & oDBdt.Rows(0).Item("FDInsDate") & "'"
                tFTInsTime = "'" & oDBdt.Rows(0).Item("FTInsTime") & "'"
            Else
                tFDInsDate = HI.UL.ULDate.FormatDateDB
                tFTInsTime = HI.UL.ULDate.FormatTimeDB
            End If

            tFTInsUser = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)

            tmpDTListCopyOrderSubComplete = New System.Data.DataTable

            Dim oColFTOrderNoCopy As System.Data.DataColumn
            oColFTOrderNoCopy = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNoCopy.Caption = "FTOrderNo"
            tmpDTListCopyOrderSubComplete.Columns.Add(oColFTOrderNoCopy.ColumnName, oColFTOrderNoCopy.DataType)

            Dim oColFTOrderNoSubCopy As System.Data.DataColumn
            oColFTOrderNoSubCopy = New System.Data.DataColumn("FTOrderNoSub", System.Type.GetType("System.String"))
            oColFTOrderNoSubCopy.Caption = "FTOrderNoSub"
            tmpDTListCopyOrderSubComplete.Columns.Add(oColFTOrderNoSubCopy.ColumnName, oColFTOrderNoSubCopy.DataType)

            _bFlagCopy = True
            nLoopCopySubOrder = 1

            Try

                Do While (_bFlagCopy) AndAlso (nLoopCopySubOrder <= nFNCopySubOrderNo)

                    '...generate new sub order no
                    tSql = ""
                    tSql = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"

                    _tFTSubOrderNoDest = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "")

                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    tSql &= vbCrLf & ", FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId"
                    tSql &= vbCrLf & ", FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState"
                    tSql &= vbCrLf & ", FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1"
                    tSql &= vbCrLf & ", FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId"
                    tSql &= vbCrLf & ", FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton,FNHSysPlantId,FNHSysBuyGrpId,FNOrderSetType)"
                    tSql &= vbCrLf & "SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                    tSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tSql &= vbCrLf & " , FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId,"
                    tSql &= vbCrLf & " FNHSysCountryId, FNHSysProvinceId, FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FNSubOrderState, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows,"
                    tSql &= vbCrLf & " FTStateOther1, FTOther1Note, FTStateOther2, FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId,"
                    tSql &= vbCrLf & " FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton,FNHSysPlantId,FNHSysBuyGrpId,FNOrderSetType"
                    'Modify 0 AS FNPackPerCarton -> FNPackPerCarton Bye Chet [09 Jan 2024]
                    tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] WITH(NOLOCK)"
                    tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                        Dim oNewRowCopySubOrderNo As System.Data.DataRow
                        oNewRowCopySubOrderNo = tmpDTListCopyOrderSubComplete.NewRow()
                        oNewRowCopySubOrderNo.Item("FTOrderNo") = _tFTOrderNoDest
                        oNewRowCopySubOrderNo.Item("FTOrderNoSub") = _tFTSubOrderNoDest
                        tmpDTListCopyOrderSubComplete.Rows.Add(oNewRowCopySubOrderNo)
                        tmpDTListCopyOrderSubComplete.AcceptChanges()

                    End If

                    '...TMERTOrderSub_Breakdown

                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown]"
                    tSql &= vbCrLf & " ("
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime"
                    tSql &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId,"
                    tSql &= vbCrLf & "  FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest"
                    tSql &= vbCrLf & "  , FNPriceOrg, FTNikePOLineItem, FNCMDisPer, FNCMDisAmt, FNNetPrice"
                    tSql &= vbCrLf & " )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                    tSql &= vbCrLf & ",FTColorway, FTSizeBreakDown, FNPrice,0 AS  FNQuantity,0 AS  FNAmt, FNHSysMatColorId, "
                    tSql &= vbCrLf & "  FNHSysMatSizeId, 0 AS  FNExtraQty, 0 AS  FNQuantityExtra,0 AS   FNGrandQuantity,0 AS  FNAmntExtra,0 AS  FNGrandAmnt,0 AS  FNGarmentQtyTest, 0 AS  FNAmntQtyTest"
                    tSql &= vbCrLf & "  ,0 AS FNPriceOrg, FTNikePOLineItem,0 AS FNCMDisPer,0 AS FNCMDisAmt,0 AS FNNetPrice"
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_Sew
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    tSql = ""
                    tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage"
                    tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                    tSql &= Environment.NewLine & "ORDER BY A.FNSewSeq ASC;"

                    Dim tmpDTSew As System.Data.DataTable

                    tmpDTSew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each oDataRowSew As System.Data.DataRow In tmpDTSew.Rows
                        Dim oImageSew As System.Drawing.Image
                        Dim FTImageSew As String
                        Dim FNSewSeq As Integer

                        FTImageSew = oDataRowSew!FTImage.ToString
                        FNSewSeq = Val(oDataRowSew!FNSewSeq.ToString)

                        oImageSew = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\" & FTImageSew)

                        If Not oImageSew Is Nothing Then
                            FTImageSew = _tFTSubOrderNoDest & "_" & FNSewSeq.ToString
                            FTImageSew = Microsoft.VisualBasic.Replace(FTImageSew, "-", "_")

                            tSql = ""
                            tSql = "UPDATE A"
                            tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImageSew, FTImageSew, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\") & "'"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & FNSewSeq & ";"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    Next

                    tmpDTSew.Dispose()

                    '...TMERTOrderSub_Pack
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNPackSeq, FTPackDescription, FTPackNote, FTImage "
                    tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] WITH(NOLOCK) "
                    tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    tSql = ""
                    tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage"
                    tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
                    tSql &= Environment.NewLine & "ORDER BY A.FNPackSeq ASC;"

                    Dim tmpDTPack As System.Data.DataTable

                    tmpDTPack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each oDataRowPack As System.Data.DataRow In tmpDTPack.Rows
                        Dim oImagePack As System.Drawing.Image
                        Dim FTImagePack As String
                        Dim FNPackSeq As Integer

                        FTImagePack = oDataRowPack!FTImage.ToString
                        FNPackSeq = Val(oDataRowPack!FNPackSeq.ToString)
                        oImagePack = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\" & FTImagePack)

                        If Not oImagePack Is Nothing Then
                            FTImagePack = _tFTSubOrderNoDest & "_" & FNPackSeq.ToString
                            FTImagePack = Microsoft.VisualBasic.Replace(FTImagePack, "-", "_")

                            tSql = ""
                            tSql = "UPDATE A"
                            tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImagePack, FTImagePack, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "'"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A "
                            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                            tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & FNPackSeq & ";"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    Next

                    tmpDTPack.Dispose()

                    '...TMERTOrderSub_Bundle
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle]([FTInsUser], [FDInsDate], [FTInsTime], [FTOrderNo], [FTSubOrderNo], [FTColorway], [FTSizeBreakDown], [FNQuantity])"
                    tSql &= Environment.NewLine & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser, " & HI.UL.ULDate.FormatDateDB & " AS FDInsDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime, N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "' AS FTOrderNo, N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "' AS FTSubOrderNo, A.[FTColorway], A.[FTSizeBreakDown], 0 AS FNQuantity"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle] AS A (NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNO = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "';"

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_SizeSpec
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension,FTTolerant "
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension,FTTolerant "
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    '...TMERTOrderSub_Component
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component]"
                    tSql &= vbCrLf & "  ( "
                    tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
                    tSql &= vbCrLf & "  )"
                    tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
                    tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] WITH(NOLOCK) "
                    tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                    nLoopCopySubOrder = nLoopCopySubOrder + 1

                    Application.DoEvents()
                Loop

                Select Case True
                    Case HI.ST.Lang.eLang.TH
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "ดำเนินการคัดลอกรายการใบสั่งผลิตย่อยเรียบร้อยแล้ว...")
                    Case Else
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Copy Factory Sub Order No. complete...")
                End Select

                _Spls.Close()

                If Not DBNull.Value.Equals(tmpDTListCopyOrderSubComplete) AndAlso tmpDTListCopyOrderSubComplete.Rows.Count > 0 Then
                    _wListCompleteCopySubOrder = New wListCompleteCopySubOrder(tmpDTListCopyOrderSubComplete)

                    HI.TL.HandlerControl.AddHandlerObj(_wListCompleteCopySubOrder)

                    Dim oSysLang As New HI.ST.SysLanguage

                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wListCompleteCopySubOrder.Name.ToString.Trim, _wListCompleteCopySubOrder)

                    Call HI.ST.Lang.SP_SETxLanguage(_wListCompleteCopySubOrder)

                    Try
                        _wListCompleteCopySubOrder.ShowDialog()
                    Catch ex As Exception
                        
                    End Try
                End If

                _bFlagCopy = True

            Catch ex As Exception
  
                _bFlagCopy = False

            End Try


            _Spls.Close()


        Catch ex As Exception
            _bFlagCopy = False
           
        End Try

        Return _bFlagCopy

    End Function

#End Region

#Region "Event Handle"

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If W_PRCbCopySubOrderNo() = True Then
            Me.Close()
        End If
    End Sub

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub wCopyOrderSub_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.FNCopySubOrderNo.Focus()
        Me.FNCopySubOrderNo.SelectionStart = 0
        Me.FNCopySubOrderNo.SelectionLength = Len(Me.FNCopySubOrderNo.Text)
        Me.FNCopySubOrderNo.SelectAll()
    End Sub

    Private Sub FNCopySubOrderNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FNCopySubOrderNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Me.ocmok.PerformClick()
        End If
    End Sub

#End Region

End Class