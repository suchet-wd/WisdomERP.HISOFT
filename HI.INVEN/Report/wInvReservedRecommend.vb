Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Public Class wInvReservedRecommend
    Private _PopUp As wReservedAutoPopUp
#Region "Variable Declaration"
    Private tSql As String
#End Region

#Region "Main Proc"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call W_GETbInitialGrid()
        Me.FTOrderNo.Focus()
        Me.FTOrderNo.SelectionStart = 0
        Me.FTOrderNo.SelectionLength = Len(Me.FTOrderNo.Text.Trim())


        _PopUp = New wReservedAutoPopUp
        HI.TL.HandlerControl.AddHandlerObj(_PopUp)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _PopUp.Name.ToString.Trim, _PopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PopUp.Name.ToString.Trim, _PopUp)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PROC_LOAD() Handles ocmload.Click
        If W_GETbVerifyData() = True Then

            Me.FTOrderNoTo.Text = Me.FTOrderNo.Text
            Call W_PRCbShowBrowseData()
        End If
    End Sub

    Private Sub PROC_CLEAR() Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogdReserved.DataSource = Nothing
        Me.ogdReserved.RefreshDataSource()
        Me.FTOrderNo.Focus()
        Me.FTOrderNo.SelectionStart = 0
        Me.FTOrderNo.SelectionLength = Len(Me.FTOrderNo.Text)
    End Sub

    Private Sub PROC_PREVIEW() Handles ocmpreview.Click
        If Not W_GETbVerifyData() = True Then
            Me.FTOrderNo.Focus()
            Me.FTOrderNo.SelectionStart = 0
            Me.FTOrderNo.SelectionLength = Len(Me.FTOrderNo.Text)
            Exit Sub
        Else
            If Me.ogvReserved.RowCount > 0 Then
                Call W_PRCbPreview()
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล !!!", 1407070002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub PROC_EXIT() Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "Procedure And Function"

    Private Function W_GETbInitialGrid() As Boolean
        Dim bPass As Boolean = False

        oColFNQty_MPR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        oColFNQty_MPR.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        oColFNQty_Conv.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        oColFNQty_Conv.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        oColFNQty_STK.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        oColFNQty_STK.DisplayFormat.FormatString = "{0:N" & HI.ST.Config.QtyDigit & "}"

        Return bPass

    End Function

    Private Function W_GETbVerifyData() As Boolean
        Dim bPass As Boolean = False

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            bPass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            bPass = True
        End If

        If FNHSysMerMatId.Text <> "" And FNHSysMerMatId.Properties.Tag.ToString <> "" Then
            bPass = True
        End If

        If Not (bPass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bPass

    End Function

    Private Function W_PRCbShowBrowseData_REM_20141009_1601() As Boolean
        Dim bPass As Boolean = False
        Dim oDBdtReservedRecomm As System.Data.DataTable
        REM Dim tLoadingText As String = ""

        ' If HI.ST.Lang.Language = HI.ST.Lang.eLang.TH Then
        '   tLoadingText = HI.MG.ShowMsg.GetMessage("กรุณาโปรดรอสักครู่ กำลังจัดเตรียมข้อมูลรายงานการจองวัตถุดิบ...", 1409280001)
        '' Else
        '    tLoadingText = "Loading data reserved recommend report please wait..."
        ' End If
        REM tLoadingText = HI.MG.ShowMsg.GetMessage("กรุณาโปรดรอสักครู่ กำลังจัดเตรียมข้อมูลรายงานการจองวัตถุดิบ...", 1409280001)

        tSql = ""

        'tSql = "DECLARE @FNLang AS INT;"
        'tSql &= Environment.NewLine & "DECLARE @FTOrderNo AS NVARCHAR(30);"
        'tSql &= Environment.NewLine & "DECLARE @FTOrderNoTo AS NVARCHAR(30);"
        'tSql &= Environment.NewLine & "SET @FNLang = " & HI.ST.Lang.Language & ";"
        'tSql &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "';"
        'tSql &= Environment.NewLine & "SET @FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text.Trim) & "';"
        'tSql &= Environment.NewLine & "SELECT N.FTOrderNo,"
        'tSql &= Environment.NewLine & "       N2.FNHSysRawMatId,"
        'tSql &= Environment.NewLine & "       N2.FTRawMatCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N2.FTRawMatNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N2.FTRawMatNameTH ELSE N2.FTRawMatNameEN END AS FTRawMatName,"
        'tSql &= Environment.NewLine & "       N.FNHSysWHId,"
        'tSql &= Environment.NewLine & "       N5.FTWHCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N5.FTWHNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N5.FTWHNameTH ELSE N5.FTWHNameEN END AS FTWHName,"
        'tSql &= Environment.NewLine & "       N3.FNHSysRawMatColorId,"
        'tSql &= Environment.NewLine & "       N3.FTRawMatColorCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N3.FTRawMatColorNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N3.FTRawMatColorNameTH ELSE N3.FTRawMatColorNameEN END AS FTRawMatColorName,"
        'tSql &= Environment.NewLine & "       N4.FNHSysRawMatSizeId,"
        'tSql &= Environment.NewLine & "       N4.FTRawMatSizeCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N4.FTRawMatSizeNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N4.FTRawMatSizeNameTH ELSE N4.FTRawMatSizeNameEN END AS FTRawMatSizeName,"
        'tSql &= Environment.NewLine & "       N.FNQuantity"
        'tSql &= Environment.NewLine & "FROM (SELECT M.FTOrderNo,M2.FNHSysRawMatId, M.FNHSysWHId, SUM(M.FNQuantity) AS FNQuantity"
        'tSql &= Environment.NewLine & "      FROM (SELECT A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId, SUM(A.FNQuantity) AS FNQuantity"
        'tSql &= Environment.NewLine & "            FROM (SELECT A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, A.FNQuantity"
        'tSql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_IN AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS B (NOLOCK) ON A.FNHSysWHId = B.FNHSysWHId"
        'tSql &= Environment.NewLine & "                  WHERE B.FTStateCenter = N'1'"
        'tSql &= Environment.NewLine & "                        AND A.FTOrderNo <= @FTOrderNoTo AND A.FTOrderNo >= @FTOrderNo"
        'tSql &= Environment.NewLine & "                  UNION ALL"
        'tSql &= Environment.NewLine & " 	             SELECT A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, ((-1) * (A.FNQuantity)) AS FNQuantity"
        'tSql &= Environment.NewLine & " 	             FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT AS A (NOLOCK)"
        'tSql &= Environment.NewLine & "	                 WHERE A.FTOrderNo <= @FTOrderNoTo AND A.FTOrderNo >= @FTOrderNo"
        'tSql &= Environment.NewLine & "                  ) AS A"
        'tSql &= Environment.NewLine & "            GROUP BY A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId"
        'tSql &= Environment.NewLine & "           ) AS M INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode AS M2 (NOLOCK) ON M.FTBarcodeNo = M2.FTBarcodeNo AND M.FTOrderNo = M2.FTOrderNo AND M.FNHSysWHId = M2.FNHSysWHId"
        'tSql &= Environment.NewLine & "      GROUP BY M.FTOrderNo, M2.FNHSysRawMatId, M.FNHSysWHId"
        'tSql &= Environment.NewLine & "     ) AS N INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial AS N2 (NOLOCK) ON N.FNHSysRawMatId = N2.FNHSysRawMatId"
        'tSql &= Environment.NewLine & "            CROSS APPLY (SELECT TOP 1 L1.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatColor AS L1 (NOLOCK) WHERE N2.FNHSysRawMatColorId = L1.FNHSysRawMatColorId) AS N3"
        'REM tSql &= Environment.NewLine & "            LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatSize AS N4 (NOLOCK) ON N2.FNHSysRawMatSizeId = N4.FNHSysRawMatSizeId"
        'tSql &= Environment.NewLine & "            OUTER APPLY (SELECT TOP 1 L3.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatSize AS L3 (NOLOCK) WHERE N2.FNHSysRawMatSizeId = L3.FNHSysRawMatSizeId) AS N4"
        'tSql &= Environment.NewLine & "            CROSS APPLY (SELECT TOP 1 L2.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS L2 (NOLOCK) WHERE N.FNHSysWHId = L2.FNHSysWHId) AS N5"
        'tSql &= Environment.NewLine & "ORDER BY N.FTOrderNo, N2.FNHSysRawMatId, N5.FNHSysWHId, N3.FNRawMatColorSeq, N4.FNRawMatSizeSeq;"

        'tSql = "DECLARE @FTUserLogIn AS NVARCHAR(30);"
        'tSql &= Environment.NewLine & "DECLARE @FNLang AS INT;"
        'tSql &= Environment.NewLine & "DECLARE @FTOrderNo AS NVARCHAR(30);"
        'tSql &= Environment.NewLine & "DECLARE @FTOrderNoTo AS NVARCHAR(30);"
        'tSql &= Environment.NewLine & "SET @FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
        'tSql &= Environment.NewLine & "SET @FNLang = " & HI.ST.Lang.Language & ";"
        'tSql &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "';"
        'tSql &= Environment.NewLine & "SET @FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text.Trim) & "';"
        'tSql &= Environment.NewLine & "DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] AS A WHERE A.FTUserLogin = @FTUserLogIn;"
        'tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] ([FTUserLogIn],[FTOrderNo],[FNHSysRawMatId],[FNHSysWHId],[FNQuantity])"
        'tSql &= Environment.NewLine & "SELECT @FTUserLogIn,M.FTOrderNo,M2.FNHSysRawMatId, M.FNHSysWHId, SUM(M.FNQuantity) AS FNQuantity"
        'tSql &= Environment.NewLine & "FROM (SELECT A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId, SUM(A.FNQuantity) AS FNQuantity"
        'tSql &= Environment.NewLine & "      FROM (SELECT A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, A.FNQuantity"
        'tSql &= Environment.NewLine & "            FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_IN AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS B (NOLOCK) ON A.FNHSysWHId = B.FNHSysWHId"
        'tSql &= Environment.NewLine & "            WHERE B.FTStateCenter = N'1'"
        'tSql &= Environment.NewLine & "                  AND A.FTOrderNo <= @FTOrderNoTo AND A.FTOrderNo >= @FTOrderNo"
        'tSql &= Environment.NewLine & "            UNION ALL"
        'tSql &= Environment.NewLine & " 	       SELECT A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, ((-1) * (A.FNQuantity)) AS FNQuantity"
        'tSql &= Environment.NewLine & "            FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT AS A (NOLOCK)"
        'tSql &= Environment.NewLine & "	           WHERE A.FTOrderNo <= @FTOrderNoTo AND A.FTOrderNo >= @FTOrderNo"
        'tSql &= Environment.NewLine & "           ) AS A"
        'tSql &= Environment.NewLine & "      GROUP BY A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId"
        'tSql &= Environment.NewLine & "      ) AS M INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode AS M2 (NOLOCK) ON M.FTBarcodeNo = M2.FTBarcodeNo AND M.FTOrderNo = M2.FTOrderNo AND M.FNHSysWHId = M2.FNHSysWHId"
        'tSql &= Environment.NewLine & "GROUP BY M.FTOrderNo, M2.FNHSysRawMatId, M.FNHSysWHId;"
        'tSql &= Environment.NewLine & "SELECT N.FTOrderNo,"
        'tSql &= Environment.NewLine & "       N2.FNHSysRawMatId,"
        'tSql &= Environment.NewLine & "       N2.FTRawMatCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N2.FTRawMatNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N2.FTRawMatNameTH ELSE N2.FTRawMatNameEN END AS FTRawMatName,"
        'tSql &= Environment.NewLine & "       N.FNHSysWHId,"
        'tSql &= Environment.NewLine & "       N5.FTWHCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N5.FTWHNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N5.FTWHNameTH ELSE N5.FTWHNameEN END AS FTWHName,"
        'tSql &= Environment.NewLine & "       N3.FNHSysRawMatColorId,"
        'tSql &= Environment.NewLine & "       N3.FTRawMatColorCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N3.FTRawMatColorNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N3.FTRawMatColorNameTH ELSE N3.FTRawMatColorNameEN END AS FTRawMatColorName,"
        'tSql &= Environment.NewLine & "       N4.FNHSysRawMatSizeId,"
        'tSql &= Environment.NewLine & "       N4.FTRawMatSizeCode,"
        'tSql &= Environment.NewLine & "       CASE @FNLang WHEN 1 THEN  N4.FTRawMatSizeNameEN"
        'tSql &= Environment.NewLine & "                    WHEN 2 THEN  N4.FTRawMatSizeNameTH ELSE N4.FTRawMatSizeNameEN END AS FTRawMatSizeName,"
        'tSql &= Environment.NewLine & "       N.FNQuantity"
        'tSql &= Environment.NewLine & "FROM (SELECT A.FTUserLogIn, A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysWHId, A.FNQuantity"
        'tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] AS A (NOLOCK)"
        'tSql &= Environment.NewLine & "      WHERE A.FTUserLogIn = @FTUserLogIn"
        'tSql &= Environment.NewLine & "     ) AS N INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial AS N2 (NOLOCK) ON N.FNHSysRawMatId = N2.FNHSysRawMatId"
        'tSql &= Environment.NewLine & "            CROSS APPLY (SELECT TOP 1 L1.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatColor AS L1 (NOLOCK) WHERE N2.FNHSysRawMatColorId = L1.FNHSysRawMatColorId) AS N3"
        'tSql &= Environment.NewLine & "            OUTER APPLY (SELECT TOP 1 L3.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatSize AS L3 (NOLOCK) WHERE N2.FNHSysRawMatSizeId = L3.FNHSysRawMatSizeId) AS N4"
        'tSql &= Environment.NewLine & "            CROSS APPLY (SELECT TOP 1 L2.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS L2 (NOLOCK) WHERE N.FNHSysWHId = L2.FNHSysWHId) AS N5"
        'tSql &= Environment.NewLine & "ORDER BY N.FTOrderNo, N2.FNHSysRawMatId, N5.FNHSysWHId, N3.FNRawMatColorSeq, N4.FNRawMatSizeSeq;"

        Dim oStrBuilder As New System.Text.StringBuilder()

        '        DECLARE @FTUserLogIn AS NVARCHAR(30);
        'DECLARE @FNLang AS INT;
        'DECLARE @FTOrderNo AS NVARCHAR(30);
        'DECLARE @FTOrderNoTo AS NVARCHAR(30);
        'SET @FTUserLogIn = N'admin';
        'SET @FNLang = 1;
        'SET @FTOrderNo = N'DNI148888';
        'SET @FTOrderNoTo = N'DNI148888';
        'DELETE A FROM [HITECH_INVENTORY]..[TINVENTempReservedRecommend] AS A WHERE A.FTUserLogin = @FTUserLogIn;
        '/* 1 FNHSysRawMatId To Many FTBarcodeNo */
        'INSERT INTO [HITECH_INVENTORY]..[TINVENTempReservedRecommend] ([FTUserLogIn],[FTOrderNo],[FNHSysRawMatId],[FNHSysWHId],[FNQuantity])
        'SELECT @FTUserLogIn,M.FTOrderNo,M2.FNHSysRawMatId, M.FNHSysWHId, SUM(M.FNQuantity) AS FNQuantity
        'FROM (SELECT A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId, SUM(A.FNQuantityIn + A.FNQuantityOut) AS FNQuantity
        '	  FROM (SELECT A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, ISNULL(A.FNQuantity,0) AS FNQuantityIn,
        '				  (-1)*(ISNULL((SELECT SUM(L1.FNQuantity)
        '								FROM dbo.TINVENBarcode_OUT AS L1 WITH(NOLOCK)
        '								WHERE L1.FTBarcodeNo = A.FTBarcodeNo
        '									  AND L1.FNHSysWHId = A.FNHSysWHId
        '									  AND L1.FTOrderNo = A.FTOrderNo),0)) AS FNQuantityOut
        '			FROM  [HITECH_INVENTORY]..TINVENBarcode_IN AS A (NOLOCK) CROSS APPLY (SELECT TOP 1 L1.FNHSysWHId
        '																				  FROM [HITECH_MASTER]..TCNMWarehouse AS L1 (NOLOCK)
        '																				  WHERE A.FNHSysWHId = L1.FNHSysWHId
        '																						AND L1.FTStateCenter = N'1') AS B 
        '			WHERE A.FTOrderNo <= @FTOrderNoTo AND A.FTOrderNo >= @FTOrderNo
        '		   ) AS A
        '	  GROUP BY A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId
        ') AS M INNER JOIN [HITECH_INVENTORY]..TINVENBarcode AS M2 (NOLOCK) ON M.FTBarcodeNo = M2.FTBarcodeNo
        'GROUP BY M.FTOrderNo, M2.FNHSysRawMatId, M.FNHSysWHId;
        'SELECT N.FTOrderNo,
        '       N2.FNHSysRawMatId,
        '       N2.FTRawMatCode,
        '       CASE @FNLang WHEN 1 THEN  N2.FTRawMatNameEN
        '                    WHEN 2 THEN  N2.FTRawMatNameTH ELSE N2.FTRawMatNameEN END AS FTRawMatName,
        '       N.FNHSysWHId,
        '       N5.FTWHCode,
        '       CASE @FNLang WHEN 1 THEN  N5.FTWHNameEN
        '                    WHEN 2 THEN  N5.FTWHNameTH ELSE N5.FTWHNameEN END AS FTWHName,
        '       N3.FNHSysRawMatColorId,
        '       N3.FTRawMatColorCode,
        '       CASE @FNLang WHEN 1 THEN  N3.FTRawMatColorNameEN
        '                    WHEN 2 THEN  N3.FTRawMatColorNameTH ELSE N3.FTRawMatColorNameEN END AS FTRawMatColorName,
        '       N4.FNHSysRawMatSizeId,
        '       N4.FTRawMatSizeCode,
        '       CASE @FNLang WHEN 1 THEN  N4.FTRawMatSizeNameEN
        '                    WHEN 2 THEN  N4.FTRawMatSizeNameTH ELSE N4.FTRawMatSizeNameEN END AS FTRawMatSizeName,
        '                    N.FNQuantity()
        'FROM (SELECT A.FTUserLogIn, A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysWHId, A.FNQuantity
        '      FROM [HITECH_INVENTORY]..[TINVENTempReservedRecommend] AS A (NOLOCK)
        '      WHERE A.FTUserLogIn = @FTUserLogIn
        '     ) AS N INNER JOIN [HITECH_MASTER]..TINVENMMaterial AS N2 (NOLOCK) ON N.FNHSysRawMatId = N2.FNHSysRawMatId
        '            CROSS APPLY (SELECT TOP 1 L1.* FROM [HITECH_MASTER]..TINVENMMatColor AS L1 (NOLOCK) WHERE N2.FNHSysRawMatColorId = L1.FNHSysRawMatColorId) AS N3
        '            OUTER APPLY (SELECT TOP 1 L3.* FROM [HITECH_MASTER]..TINVENMMatSize AS L3 (NOLOCK) WHERE N2.FNHSysRawMatSizeId = L3.FNHSysRawMatSizeId) AS N4
        '            CROSS APPLY (SELECT TOP 1 L2.* FROM [HITECH_MASTER]..TCNMWarehouse AS L2 (NOLOCK) WHERE N.FNHSysWHId = L2.FNHSysWHId) AS N5
        'ORDER BY N.FTOrderNo, N2.FNHSysRawMatId, N5.FNHSysWHId, N3.FNRawMatColorSeq, N4.FNRawMatSizeSeq;

        oStrBuilder.AppendLine("DECLARE @FTUserLogIn AS NVARCHAR(30);")
        oStrBuilder.AppendLine("DECLARE @FNLang AS INT;")
        oStrBuilder.AppendLine("DECLARE @FTOrderNo AS NVARCHAR(30);")
        oStrBuilder.AppendLine("DECLARE @FTOrderNoTo AS NVARCHAR(30);")
        oStrBuilder.AppendLine("SET @FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
        'oStrBuilder.AppendLine(String.Format("SET @FNLang = {0};", HI.ST.Lang.Language))
        oStrBuilder.AppendLine("SET @FNLang = " & HI.ST.Lang.Language & ";")
        oStrBuilder.AppendLine("SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "';")
        oStrBuilder.AppendLine("SET @FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text.Trim) & "';")
        oStrBuilder.AppendLine("DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] AS A WHERE A.FTUserLogin = @FTUserLogIn;")
        oStrBuilder.AppendLine("/* 1 FNHSysRawMatId To Many FTBarcodeNo */")
        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] ([FTUserLogIn],[FTOrderNo],[FNHSysRawMatId],[FNHSysWHId],[FNQuantity])")
        oStrBuilder.AppendLine("SELECT @FTUserLogIn,M.FTOrderNo,M2.FNHSysRawMatId, M.FNHSysWHId, SUM(M.FNQuantity) AS FNQuantity")
        oStrBuilder.AppendLine("FROM (SELECT A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId, SUM(A.FNQuantityIn + A.FNQuantityOut) AS FNQuantity")
        oStrBuilder.AppendLine("	  FROM (SELECT A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, ISNULL(A.FNQuantity,0) AS FNQuantityIn,")
        oStrBuilder.AppendLine("				  (-1)*(ISNULL((SELECT SUM(L1.FNQuantity)")
        oStrBuilder.AppendLine("								FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT AS L1 WITH(NOLOCK)")
        oStrBuilder.AppendLine("								WHERE L1.FTBarcodeNo = A.FTBarcodeNo")
        oStrBuilder.AppendLine("									  AND L1.FNHSysWHId = A.FNHSysWHId")
        oStrBuilder.AppendLine("									  AND L1.FTOrderNo = A.FTOrderNo),0)) AS FNQuantityOut")
        oStrBuilder.AppendLine("			FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_IN AS A (NOLOCK) CROSS APPLY (SELECT TOP 1 L1.FNHSysWHId")
        oStrBuilder.AppendLine("																				  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS L1 (NOLOCK)")
        oStrBuilder.AppendLine("																				  WHERE A.FNHSysWHId = L1.FNHSysWHId")
        oStrBuilder.AppendLine("																						AND L1.FTStateCenter = N'1') AS B")
        oStrBuilder.AppendLine("			WHERE A.FTOrderNo <= @FTOrderNoTo AND A.FTOrderNo >= @FTOrderNo")
        oStrBuilder.AppendLine("		   ) AS A")
        oStrBuilder.AppendLine("	  GROUP BY A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId")
        oStrBuilder.AppendLine("      ) AS M INNER JOIN [HITECH_INVENTORY]..TINVENBarcode AS M2 (NOLOCK) ON M.FTBarcodeNo = M2.FTBarcodeNo")
        oStrBuilder.AppendLine("GROUP BY M.FTOrderNo, M2.FNHSysRawMatId, M.FNHSysWHId;")
        oStrBuilder.AppendLine("SELECT N.FTOrderNo,")
        oStrBuilder.AppendLine("       N2.FNHSysRawMatId,")
        oStrBuilder.AppendLine("       N2.FTRawMatCode,")
        oStrBuilder.AppendLine("       CASE @FNLang WHEN 1 THEN  N2.FTRawMatNameEN")
        oStrBuilder.AppendLine("                    WHEN 2 THEN  N2.FTRawMatNameTH ELSE N2.FTRawMatNameEN END AS FTRawMatName,")
        oStrBuilder.AppendLine("       N.FNHSysWHId,")
        oStrBuilder.AppendLine("       N5.FTWHCode,")
        oStrBuilder.AppendLine("       CASE @FNLang WHEN 1 THEN  N5.FTWHNameEN")
        oStrBuilder.AppendLine("                    WHEN 2 THEN  N5.FTWHNameTH ELSE N5.FTWHNameEN END AS FTWHName,")
        oStrBuilder.AppendLine("       N3.FNHSysRawMatColorId,")
        oStrBuilder.AppendLine("       N3.FTRawMatColorCode,")
        oStrBuilder.AppendLine("       CASE @FNLang WHEN 1 THEN  N3.FTRawMatColorNameEN")
        oStrBuilder.AppendLine("                    WHEN 2 THEN  N3.FTRawMatColorNameTH ELSE N3.FTRawMatColorNameEN END AS FTRawMatColorName,")
        oStrBuilder.AppendLine("       N4.FNHSysRawMatSizeId,")
        oStrBuilder.AppendLine("       N4.FTRawMatSizeCode,")
        oStrBuilder.AppendLine("       CASE @FNLang WHEN 1 THEN  N4.FTRawMatSizeNameEN")
        oStrBuilder.AppendLine("                    WHEN 2 THEN  N4.FTRawMatSizeNameTH ELSE N4.FTRawMatSizeNameEN END AS FTRawMatSizeName,")
        oStrBuilder.AppendLine("                    N.FNQuantity")
        oStrBuilder.AppendLine("FROM (SELECT A.FTUserLogIn, A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysWHId, A.FNQuantity")
        oStrBuilder.AppendLine("      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] AS A (NOLOCK)")
        oStrBuilder.AppendLine("      WHERE A.FTUserLogIn = @FTUserLogIn")
        oStrBuilder.AppendLine("     ) AS N INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial AS N2 (NOLOCK) ON N.FNHSysRawMatId = N2.FNHSysRawMatId")
        oStrBuilder.AppendLine("            OUTER APPLY (SELECT TOP 1 L1.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatColor AS L1 (NOLOCK) WHERE N2.FNHSysRawMatColorId = L1.FNHSysRawMatColorId) AS N3")
        oStrBuilder.AppendLine("            OUTER APPLY (SELECT TOP 1 L3.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatSize AS L3 (NOLOCK) WHERE N2.FNHSysRawMatSizeId = L3.FNHSysRawMatSizeId) AS N4")
        oStrBuilder.AppendLine("            OUTER APPLY (SELECT TOP 1 L2.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS L2 (NOLOCK) WHERE N.FNHSysWHId = L2.FNHSysWHId) AS N5")
        oStrBuilder.AppendLine("ORDER BY N.FTOrderNo, N2.FNHSysRawMatId, N5.FNHSysWHId, N3.FNRawMatColorSeq, N4.FNRawMatSizeSeq;")

        If oStrBuilder.Length > 0 Then
            tSql = oStrBuilder.ToString()
        End If

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            Dim oSplsScreen As New HI.TL.SplashScreen(HI.MG.ShowMsg.GetMessage("กรุณาโปรดรอสักครู่ กำลังจัดเตรียมข้อมูลรายงานการจองวัตถุดิบ...", 1409280001))
            oDBdtReservedRecomm = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_INVEN)
            Me.ogdReserved.DataSource = oDBdtReservedRecomm
            oSplsScreen.Close()
        Else
            oDBdtReservedRecomm = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_INVEN)
            Me.ogdReserved.DataSource = oDBdtReservedRecomm
        End If

        Return bPass

    End Function

    Private Function W_PRCbShowBrowseData() As Boolean
        Dim bPass As Boolean = False
        Dim oDBdtReservedRecomm As System.Data.DataTable

        tSql = ""

        Dim oStrBuilder As New System.Text.StringBuilder()
        Dim oSplsScreen As New HI.TL.SplashScreen(HI.MG.ShowMsg.GetMessage("กรุณาโปรดรอสักครู่ กำลังจัดเตรียมข้อมูลรายงานการจองวัตถุดิบ...", 1409280001))

        Try
            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTUserLogIn AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FNLang AS INT;")
            oStrBuilder.AppendLine("DECLARE @FTOrderNo AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FTOrderNoTo AS NVARCHAR(30);")
            oStrBuilder.AppendLine("SET @FTUserLogIn = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
            oStrBuilder.AppendLine("SET @FNLang = " & HI.ST.Lang.Language & ";")
            oStrBuilder.AppendLine("SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "';")
            oStrBuilder.AppendLine("SET @FTOrderNoTo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text.Trim) & "';")
            oStrBuilder.AppendLine("DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] AS A WHERE A.FTUserLogin = @FTUserLogIn;")
            oStrBuilder.AppendLine("DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommendPOBal] AS A WHERE A.FTUserLogin = @FTUserLogIn;")

            oStrBuilder.AppendLine("  CREATE TABLE #Balinfo (FTOrderNo nvarchar(30),FNHSysRawMatId int,FNHSysWHId int,FTPurchaseNo nvarchar(MAX) , FTRawMatColorNameTH nvarchar(200), FTRawMatColorNameEN nvarchar(200),FTPORemark nvarchar(MAX) )CREATE INDEX [IDX_TmpReserveinfo] On #Balinfo(FTOrderNo,FNHSysRawMatId,FNHSysWHId) ")


            oStrBuilder.AppendLine(" INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENTempReservedRecommendPOBal(FTUserLogin,FTOrderNo, FTBarcodeNo, FNHSysWHId, FNQuantity, FTPurchaseNo, FNHSysRawMatId,FTRawMatColorNameTH,FTRawMatColorNameEN)")
            oStrBuilder.AppendLine(" Select  @FTUserLogIn,M.FTOrderNo, M.FTBarcodeNo, M.FNHSysWHId,CASE WHEN M.FNQuantity<=0 THEN 0 ELSE M.FNQuantity END FNQuantity, M2.FTPurchaseNo, M2.FNHSysRawMatId,ISNULL(PD.FTRawMatColorNameTH,''),ISNULL(PD.FTRawMatColorNameEN,'') ")
            oStrBuilder.AppendLine(" FROM")
            oStrBuilder.AppendLine("(SELECT A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId, SUM(A.FNQuantityIn + A.FNQuantityOut) AS FNQuantity")
            oStrBuilder.AppendLine("  FROM(Select A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId, ISNULL(A.FNQuantity, 0) As FNQuantityIn,")
            oStrBuilder.AppendLine("(-1) * (ISNULL((SELECT SUM(L1.FNQuantity)")
            oStrBuilder.AppendLine("  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT As L1 With(NOLOCK)")
            oStrBuilder.AppendLine("  Where L1.FTBarcodeNo = A.FTBarcodeNo")
            oStrBuilder.AppendLine(" And L1.FNHSysWHId = A.FNHSysWHId")
            oStrBuilder.AppendLine(" And L1.FTDocumentRefNo = A.FTDocumentNo")
            oStrBuilder.AppendLine(" And L1.FTOrderNo = A.FTOrderNo),0)) As FNQuantityOut")
            oStrBuilder.AppendLine("	  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_IN As A (NOLOCK) INNER Join (Select L1.FNHSysWHId")
            oStrBuilder.AppendLine("      From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse As L1 (NOLOCK)")
            oStrBuilder.AppendLine("   Where L1.FTStateCenter = N'1' ) AS B ON A.FNHSysWHId = B.FNHSysWHId ")
            oStrBuilder.AppendLine("       Where Isnull(A.FTStateReserve, 0) <> '1' ")
            oStrBuilder.AppendLine("     ) As A")
            oStrBuilder.AppendLine("  Group By A.FTBarcodeNo, A.FTOrderNo, A.FNHSysWHId")
            oStrBuilder.AppendLine(") As M INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode As M2 WITH(NOLOCK) On M.FTBarcodeNo =M2.FTBarcodeNo   ")
            oStrBuilder.AppendLine(" LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo As PD WITH(NOLOCK) On M2.FTPurchaseNo =PD.FTPurchaseNo AND M2.FTOrderNo = PD.FTOrderNo AND M2.FNHSysRawMatId =PD.FNHSysRawMatId ")
            oStrBuilder.AppendLine(" LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase As PH WITH(NOLOCK) On PD.FTPurchaseNo =PH.FTPurchaseNo ")
            oStrBuilder.AppendLine(" WHERE M.FNQuantity>0 ")


            If FNHSysSuplId.Text <> "" Then

                oStrBuilder.AppendLine("	And PH.FNHSysSuplId = " & Val(FNHSysSuplId.Properties.Tag.ToString) & "")

            End If

            oStrBuilder.AppendLine(" INSERT INTO #Balinfo(FTOrderNo, FNHSysRawMatId, FNHSysWHId, FTPurchaseNo,FTRawMatColorNameTH,FTRawMatColorNameEN,FTPORemark)")
            oStrBuilder.AppendLine("Select  FTOrderNo")
            oStrBuilder.AppendLine(", FNHSysRawMatId ")
            oStrBuilder.AppendLine(", FNHSysWHId ")
            oStrBuilder.AppendLine(", ISNULL((")
            oStrBuilder.AppendLine(" Select  STUFF((SELECT  ',' + FTPurchaseNo  ")
            oStrBuilder.AppendLine("   From(SELECT      Distinct FTPurchaseNo ")
            oStrBuilder.AppendLine("	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENTempReservedRecommendPOBal AS XX  WITH(NOLOCK) WHERE FTUserLogin= @FTUserLogIn ")
            oStrBuilder.AppendLine(" AND (XX.FTOrderNo = A.FTOrderNo)")
            oStrBuilder.AppendLine(" And (XX.FNHSysRawMatId = A.FNHSysRawMatId) ")
            oStrBuilder.AppendLine(" And (XX.FNHSysWHId = A.FNHSysWHId) ")
            oStrBuilder.AppendLine(") As T ")
            oStrBuilder.AppendLine("	For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') ")
            oStrBuilder.AppendLine("),'')    AS FTReserveNo ")
            oStrBuilder.AppendLine(", MAX(FTRawMatColorNameTH) AS FTRawMatColorNameTH ")
            oStrBuilder.AppendLine(", MAX(FTRawMatColorNameEN) AS FTRawMatColorNameEN ")

            oStrBuilder.AppendLine(", ISNULL((")
            oStrBuilder.AppendLine(" Select  STUFF((SELECT  ',' + FTRemark  ")
            oStrBuilder.AppendLine("   From(SELECT      Distinct SS.FTRemark ")
            oStrBuilder.AppendLine("	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENTempReservedRecommendPOBal AS XX  WITH(NOLOCK) ")
            oStrBuilder.AppendLine("	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..[TPURTPurchase] AS SS (NOLOCK)   On XX.FTPurchaseNo = SS.FTPurchaseNo   ")
            oStrBuilder.AppendLine(" WHERE XX.FTUserLogin= @FTUserLogIn  ")
            oStrBuilder.AppendLine(" AND (XX.FTOrderNo = A.FTOrderNo)")
            oStrBuilder.AppendLine(" And (XX.FNHSysRawMatId = A.FNHSysRawMatId) ")
            oStrBuilder.AppendLine(" And (XX.FNHSysWHId = A.FNHSysWHId) ")
            oStrBuilder.AppendLine(") As T ")
            oStrBuilder.AppendLine("	For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'') ")
            oStrBuilder.AppendLine("),'')    AS FTPORemark ")

            oStrBuilder.AppendLine(" FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENTempReservedRecommendPOBal AS A WITH(NOLOCK) WHERE FTUserLogin= @FTUserLogIn ")
            oStrBuilder.AppendLine("  GROUP BY FTOrderNo, FNHSysRawMatId, FNHSysWHId")
            oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend]([FTUserLogin], [FTOrderNo], [FNHSysRawMatId], [FNHSysWHId], ")
            oStrBuilder.AppendLine("                                                                                                                  [FNQty_STK], [FNHSysUnitId_STK], [FNQty_MPR], [FNHSysUnitId_MPR], [FNQty_Conv])")
            oStrBuilder.AppendLine("Select AA.FTUserLogin, AA.FTOrderNo, AA.FNHSysRawMatId,")
            oStrBuilder.AppendLine("AA.FNHSysWHId, AA.FNQuantity As FNQty_STK, AA.FNHSysUnitId As FNHSysUnitId_STK,")
            oStrBuilder.AppendLine("BB.FNUsedQuantity As FNQty_MPR,")
            oStrBuilder.AppendLine("BB.FNHSysUnitId As FNHSysUnitId_MPR,")
            oStrBuilder.AppendLine("CASE When AA.FNHSysUnitId = BB.FNHSysUnitId Then BB.FNUsedQuantity")
            oStrBuilder.AppendLine("                                            Else (Select CONVERT(NUMERIC(18,5), CEILING((BB.FNUsedQuantity * T1.FNRateTo)/T1.FNRateFrom))")
            oStrBuilder.AppendLine("										          FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnitConvert As T1")
            oStrBuilder.AppendLine("                                                  WHERE BB.FNHSysUnitId = T1.FNHSysUnitId")
            oStrBuilder.AppendLine("										                And AA.FNHSysUnitId = T1.FNHSysUnitIdTo) End As FNQty_Conv /*ถ้าหน่วย MRP ไม่ตรง กับ หน่วยใน STOCK ให้ทำการแปลง จำนวน จาก หน่วย MRP ให้อยู่ในรูปแบบ ของ จำนวนหน่วย STOCK*/")
            oStrBuilder.AppendLine("FROM (Select @FTUserLogIn As FTUserLogin,M.FTOrderNo,M2.FNHSysRawMatId, M4.FTRawMatCode, M.FNHSysWHId, SUM(M.FNQuantity) As FNQuantity, M2.FNHSysUnitId, M3.FTUnitCode, M3.FTUnitNameEN")
            oStrBuilder.AppendLine("      FROM (  ")

            oStrBuilder.AppendLine("   Select  A.FTOrderNo, A.FTBarcodeNo, A.FNHSysWHId, A.FNQuantity  ")
            oStrBuilder.AppendLine("  From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENTempReservedRecommendPOBal As A  With(NOLOCK)  ")
            oStrBuilder.AppendLine("   Where FTUserLogin = @FTUserLogIn  ")

            oStrBuilder.AppendLine(") As M INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode As M2 (NOLOCK) On M.FTBarcodeNo = M2.FTBarcodeNo")
            oStrBuilder.AppendLine("				   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] As M3 (NOLOCK) On M2.FNHSysUnitId = M3.FNHSysUnitId")
            oStrBuilder.AppendLine("				   LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TINVENMMaterial] As M4 (NOLOCK) On M2.FNHSysRawMatId = M4.FNHSysRawMatId")
            oStrBuilder.AppendLine("       GROUP BY M.FTOrderNo, M2.FNHSysRawMatId, M4.FTRawMatCode, M.FNHSysWHId, M2.FNHSysUnitId, M3.FTUnitCode, M3.FTUnitNameEN")
            oStrBuilder.AppendLine("      ) As AA INNER JOIN ")
            'oStrBuilder.AppendLine(" (Select FTOrderNo,FNHSysRawMatId ,Max(FNHSysUnitId) As FNHSysUnitId ,SUM(FNUsedQuantity+FNUsedPlusQuantity ) As FNUsedQuantity")
            'oStrBuilder.AppendLine("  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource As BB (NOLOCK) ")
            'oStrBuilder.AppendLine("	WHERE FTOrderNo >= @FTOrderNo And FTOrderNo <= @FTOrderNoTo")
            'oStrBuilder.AppendLine("   GROUP BY FTOrderNo,FNHSysRawMatId ")

            If Me.FTOrderNo.Text.Trim <> "" Then

                oStrBuilder.AppendLine(" (Select BB.FNHSysRawMatId ,Max(BB.FNHSysUnitId) As FNHSysUnitId ,SUM(BB.FNUsedQuantity+BB.FNUsedPlusQuantity ) As FNUsedQuantity")
                oStrBuilder.AppendLine("  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource As BB (NOLOCK) ")
                oStrBuilder.AppendLine("  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial As IM (NOLOCK) On BB.FNHSysRawMatId = IM.FNHSysRawMatId ")

                oStrBuilder.AppendLine("	WHERE BB.FTOrderNo >= @FTOrderNo And BB.FTOrderNo <= @FTOrderNoTo")

                If FNHSysMerMatId.Text <> "" Then
                    oStrBuilder.AppendLine("	And IM.FTRawMatCode = '" & HI.UL.ULF.rpQuoted(FNHSysMerMatId.Text) & "'")
                End If

                oStrBuilder.AppendLine("   GROUP BY BB.FNHSysRawMatId ")
                oStrBuilder.AppendLine(" ) AS BB ")

            Else

                oStrBuilder.AppendLine(" (SELECT IM.FNHSysRawMatId ,Max(IM.FNHSysUnitId) AS FNHSysUnitId ,SUM(0 ) AS FNUsedQuantity")
                oStrBuilder.AppendLine("  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial AS IM (NOLOCK)  ")
                oStrBuilder.AppendLine(" WHERE IM.FTRawMatCode = '" & HI.UL.ULF.rpQuoted(FNHSysMerMatId.Text) & "'")
                oStrBuilder.AppendLine("   GROUP BY IM.FNHSysRawMatId ")
                oStrBuilder.AppendLine(" ) AS BB ")

            End If

            oStrBuilder.AppendLine(" ON  AA.FNHSysRawMatId = BB.FNHSysRawMatId ")
            'oStrBuilder.AppendLine(" ON AA.FTOrderNo = BB.FTOrderNo")
            'oStrBuilder.AppendLine("	     AND AA.FNHSysRawMatId = BB.FNHSysRawMatId;")
            oStrBuilder.AppendLine("/*===================================================================Show Browse data reserved recommend==========================================================================================*/")
            oStrBuilder.AppendLine("SELECT '0' AS FTSelect , N1.FTOrderNo,SS.FTSeasonCode,ST.FTStyleCode,")
            oStrBuilder.AppendLine("       N1.FNHSysRawMatId,")
            oStrBuilder.AppendLine("       N1.FTPurchaseNo,")
            oStrBuilder.AppendLine("       N2.FTRawMatCode,")
            oStrBuilder.AppendLine("       CASE WHEN @FNLang = 1 THEN N2.FTRawMatNameEN")
            oStrBuilder.AppendLine("            WHEN @FNLang = 2 THEN N2.FTRawMatNameTH ELSE N2.FTRawMatNameEN END AS FTRawMatName,")
            oStrBuilder.AppendLine("       N2.FNHSysRawMatColorId AS FNHSysRawMatColorId,")
            oStrBuilder.AppendLine("       N3.FTRawMatColorCode AS FTRawMatColorCode,")
            oStrBuilder.AppendLine("       CASE WHEN @FNLang = 1 THEN ISNULL(N1.FTRawMatColorNameEN,N3.FTRawMatColorNameEN)")
            oStrBuilder.AppendLine("            WHEN @FNLang = 2 THEN ISNULL(N1.FTRawMatColorNameTH,N3.FTRawMatColorNameTH) ELSE ISNULL(N1.FTRawMatColorNameEN,N3.FTRawMatColorNameEN) END AS FTRawMatColorName,")
            oStrBuilder.AppendLine("       N2.FNHSysRawMatSizeId AS FNHSysMatSizeId,")
            oStrBuilder.AppendLine("       N4.FTRawMatSizeCode AS FTRawMatSizeCode,")
            oStrBuilder.AppendLine("       CASE WHEN @FNLang = 1 THEN N4.FTRawMatSizeNameEN")
            oStrBuilder.AppendLine("            WHEN @FNLang = 2 THEN N4.FTRawMatSizeNameTH ELSE N4.FTRawMatSizeNameEN END AS FTRawMatSizeName,")
            oStrBuilder.AppendLine("       N1.FNQty_MPR AS FNQty_MPR,")
            oStrBuilder.AppendLine("       N1.FNHSysUnitId_MPR AS FNHSysUnitId_MPR,")
            oStrBuilder.AppendLine("       N7.FTUnitCode AS FTUnitCode_MPR,")
            oStrBuilder.AppendLine("       CASE WHEN @FNLang = 1 THEN N7.FTUnitNameEN")
            oStrBuilder.AppendLine("            WHEN @FNLang = 2 THEN N7.FTUnitNameTH ELSE N7.FTUnitNameEN END AS FTUnitDesc_MPR,")
            oStrBuilder.AppendLine("       N1.FNQty_Conv AS FNQty_Conv,")
            oStrBuilder.AppendLine("       N1.FNQty_STK AS FNQty_STK,")
            oStrBuilder.AppendLine("  0.0000 AS FNReservedQty, ")
            oStrBuilder.AppendLine("       N1.FNHSysUnitId_STK AS FNHSysUnitId_STK,")
            oStrBuilder.AppendLine("       N6.FTUnitCode AS FTUnitCode_STK,")
            oStrBuilder.AppendLine("       CASE WHEN @FNLang = 1 THEN N6.FTUnitNameEN")
            oStrBuilder.AppendLine("            WHEN @FNLang = 2 THEN N6.FTUnitNameTH ELSE N6.FTUnitNameEN END AS FTUnitDesc_STK,")
            oStrBuilder.AppendLine("       N1.FNHSysWHId AS FNHSysWHId,")
            oStrBuilder.AppendLine("       N5.FTWHCode AS FTWHCode,")
            oStrBuilder.AppendLine("       CASE WHEN @FNLang = 1 THEN N5.FTWHNameEN")
            oStrBuilder.AppendLine("            WHEN @FNLang = 2 THEN N5.FTWHNameTH ELSE N5.FTWHNameEN END AS FTWHName,N1.FTPORemark")
            oStrBuilder.AppendLine("FROM (SELECT A.FTUserLogin, A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysWHId, A.FNQty_STK, A.FNHSysUnitId_STK, A.FNQty_MPR, A.FNHSysUnitId_MPR, A.FNQty_Conv,ISNULL(X.FTPurchaseNo,'') AS FTPurchaseNo,X.FTRawMatColorNameEN,X.FTRawMatColorNameTH,X.FTPORemark")
            oStrBuilder.AppendLine("      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..[TINVENTempReservedRecommend] AS A (NOLOCK) LEFT OUTER JOIN #Balinfo AS X ON A.FTOrderNo=X.FTOrderNo")
            oStrBuilder.AppendLine("  AND A.FNHSysWHId=X.FNHSysWHId AND A.FNHSysRawMatId=X.FNHSysRawMatId")
            oStrBuilder.AppendLine("      WHERE A.FTUserLogin = @FTUserLogIn) AS N1 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TINVENMMaterial] AS N2 (NOLOCK) ON N1.FNHSysRawMatId = N2.FNHSysRawMatId")
            oStrBuilder.AppendLine("                                                OUTER APPLY (SELECT TOP 1 L1.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TINVENMMatColor] AS L1 (NOLOCK) WHERE N2.FNHSysRawMatColorId = L1.FNHSysRawMatColorId) AS N3")
            oStrBuilder.AppendLine("										        OUTER APPLY (SELECT TOP 1 L2.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TINVENMMatSize] AS L2 (NOLOCK) WHERE N2.FNHSysRawMatSizeId = L2.FNHSysRawMatSizeId) AS N4")
            oStrBuilder.AppendLine("										        OUTER APPLY (SELECT TOP 1 L3.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMWarehouse] AS L3 (NOLOCK) WHERE N1.FNHSysWHId = L3.FNHSysWHId) AS N5")
            oStrBuilder.AppendLine("												OUTER APPLY (SELECT TOP 1 L4.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L4 (NOLOCK) WHERE N1.FNHSysUnitId_STK = L4.FNHSysUnitId) AS N6")
            oStrBuilder.AppendLine("												OUTER APPLY (SELECT TOP 1 L5.* FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L5 (NOLOCK) WHERE N1.FNHSysUnitId_MPR = L5.FNHSysUnitId) AS N7")

            oStrBuilder.AppendLine("												OUTER APPLY (SELECT TOP 1 ST.FTStyleCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS ST (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder  AS OX WITH(NOLOCK)  On ST.FNHSysStyleId = OX.FNHSysStyleId  WHERE OX.FTOrderNo =  N1.FTOrderNo  ) AS ST")
            oStrBuilder.AppendLine("												OUTER APPLY (SELECT TOP 1 SS.FTSeasonCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS SS (NOLOCK)  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder  AS OX WITH(NOLOCK)  On SS.FNHSysSeasonId = OX.FNHSysSeasonId  WHERE  OX.FTOrderNo =  N1.FTOrderNo) AS SS")

            oStrBuilder.AppendLine("WHERE N1.FNQty_STK > 0")

            oStrBuilder.AppendLine("ORDER BY N1.FTOrderNo, N1.FNHSysRawMatId, N5.FNHSysWHId, N3.FTRawMatColorCode, N4.FNRawMatSizeSeq;")
            ' oStrBuilder.AppendLine(" DROP TABLE #POBal ")
            oStrBuilder.AppendLine(" DROP TABLE #Balinfo ")

            If oStrBuilder.Length > 0 Then
                tSql = oStrBuilder.ToString()
            End If

            oDBdtReservedRecomm = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_INVEN)
            Me.ogdReserved.DataSource = oDBdtReservedRecomm

        Catch ex As Exception
        End Try

        oSplsScreen.Close()
        Return bPass

    End Function

    Private Function W_PRCbPreview() As Boolean
        Dim bPass As Boolean = False
        Dim tFormula As String = ""

        If Me.FTOrderNo.Text.Trim <> "" Then
            If tFormula <> "" Then tFormula += " AND "
            tFormula += " {TINVENTempReservedRecommend.FTOrderNo} >= '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "'"
        End If

        If Me.FTOrderNoTo.Text.Trim <> "" Then
            If tFormula <> "" Then tFormula += " AND "
            tFormula += " {TINVENTempReservedRecommend.FTOrderNo} <= '" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text.Trim) & "'"
        End If

        Dim tReportPath As String
        Dim tReportName As String

        tReportPath = "Inventrory\"
        tReportName = "ReservedRecommend.rpt"

        ''...validate path
        'If Not IO.File.Exists(System.Windows.Forms.Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
        '    REM MsgBox("Unable to locate report file : " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
        '    HI.MG.ShowMsg.mInfo("ไม่พบไฟล์รายงาน !!!", 1403160001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'Else
        '    With New HI.RP.Report
        '        .FormTitle = Me.Text
        '        .ReportFolderName = tReportPath
        '        .ReportName = tReportName
        '        .Formular = tFormula

        '        'Dim nFNLang As Integer
        '        ''Dim tReportTitle As String = ""

        '        ''Select Case HI.ST.Lang.Language
        '        ''    Case HI.ST.Lang.Lang.EN, HI.ST.Lang.Lang.KM, HI.ST.Lang.Lang.VT
        '        ''        tReportTitle = "Reserved Recommend Report"
        '        ''        nFNLang = 1
        '        ''    Case HI.ST.Lang.eLang.TH
        '        ''        tReportTitle = "รายงานการจองวัตถุดิบ"
        '        ''        nFNLang = 2
        '        ''End Select

        '        ''.AddParameter("ReportName", tReportTitle)
        '        '.AddParameter("fnLang", nFNLang)

        '        .Preview()
        '    End With

        With New HI.RP.Report
            .FormTitle = Me.Text
            .ReportFolderName = tReportPath
            .ReportName = tReportName
            .Formular = tFormula

            .Preview()

            bPass = True

        End With

        Return bPass

    End Function

#End Region

#Region "Event Handles"
    Private Sub FTOrderNo_LostFocus(sender As Object, e As EventArgs) Handles FTOrderNo.LostFocus
        If Me.FTOrderNo.Text.Trim() <> "" And Me.FTOrderNoTo.Text.Trim() = "" Then
            Me.FTOrderNoTo.Text = Me.FTOrderNo.Text
        End If
    End Sub
    Private Sub FTOrderNoTo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles FTOrderNoTo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Me.FTOrderNoTo.Text.Trim() <> "" And Me.FTOrderNo.Properties.Tag.ToString <> "" Then
                ocmload.PerformClick()
            End If
        End If
    End Sub

   
    Private Sub ogvReserved_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvReserved.RowCellStyle
        Try
            With Me.ogvReserved
                If Me.ogvReserved.RowCount > 0 Then
                    If e.RowHandle > -1 Then
                        If .GetRowCellValue(e.RowHandle, oColFNHSysUnitId_MPR).ToString() <> .GetRowCellValue(e.RowHandle, oColFNHSysUnitId_STK).ToString() Then
                            If e.Column.FieldName = "FTUnitDesc_MPR" Or e.Column.FieldName = "FTUnitDesc_STK" Then
                                e.Appearance.ForeColor = Drawing.Color.DarkRed
                            End If
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            '    MsgBox(ex.Message.ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, Me.Text)
            'End If
        End Try
    End Sub
#End Region


    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles ocmautoreserved.Click
        Try
            If Not (HI.MG.ShowMsg.mConfirmProcess("Do you want auto reserve Yes or no .!!!!", 1507231710, "")) Then
                Exit Sub
            End If

            Dim _WHCode As String = "" : Dim _Cmd As String = "" : Dim _DocNo As String = ""
            Dim _FNOrderType As Integer = 0 : Dim _dtBar As DataTable = Nothing : Dim _ReservedQtyBal As Double = 0
            Dim _ReservedQtyUse As Double = 0 : Dim _DocNoAuto As String = "" : Dim _Date As String = ""
            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FTSelect", GetType(String))
                .Columns.Add("FTDocumentNo", GetType(String))
                .Columns.Add("FTWHCode", GetType(String))
                .Columns.Add("FNHSysWHId", GetType(Integer))
                .Columns.Add("FDRSVDate", GetType(String))
            End With
            If Me.FTOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
                Me.FTOrderNo.Focus()
                Exit Sub
            End If
            With CType(ogdReserved.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1'").Length > 0 Then
                    _Date = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB & " AS Date ", Conn.DB.DataBaseName.DB_INVEN, "")
                    _Date = Mid(_Date, 9, 2) & "/" & Mid(_Date, 6, 2) & "/" & Mid(_Date, 1, 4)
                Else

                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูล ที่ต้องการทำการ Auto", 1507290784, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")


                For Each R As DataRow In .Select("FTSelect = '1'", "FTWHCode")
                    If _WHCode <> R!FTWHCode.ToString Then
                      
                        _DocNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENReserve", "", False, _CmpH & "A").ToString()

                        '_DocNo = Replace(_DocNo, "-", "A-")
                        _Cmd = "INSERT INTO TINVENReserve (FTInsUser, FDInsDate, FTInsTime, FTReserveNo, FDReserveDate, FTReserveBy, FNHSysWHId"
                        _Cmd &= ", FTOrderNo, FTRemark, FNHSysCmpId)"
                        _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & _DocNo & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & CInt(R!FNHSysWHId.ToString)
                        _Cmd &= vbCrLf & ",'" & Me.FTOrderNo.Text & "'"
                        _Cmd &= vbCrLf & ",'Auto Reserved... By  " & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "," & HI.ST.SysInfo.CmpID
                        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
                        If _DocNoAuto <> "" Then _DocNoAuto &= ", "
                        _DocNoAuto &= _DocNo
                        _dt.Rows.Add("0", _DocNo, R!FTWHCode.ToString, CInt(R!FNHSysWHId.ToString), _Date.ToString)
                    End If

                    _Cmd = " SELECT     M.FTRawMatCode, MC.FTRawMatColorCode, MZ.FTRawMatSizeCode, B_1.FTBarcodeNo, B_1.FTOrderNo, B_1.FNQuantityBal, B_1.FTDocumentNo,B_1.FNPriceTrans ,ISNULL(X.FNPriceClosed1,ISNULL(B_1.FNPriceClose1,-1))  AS FNPriceClose1,ISNULL(X.FNPriceClosed2,ISNULL(B_1.FNPriceClose2,-1))  AS FNPriceClose2 "
                    _Cmd &= vbCrLf & " FROM          "
                    _Cmd &= vbCrLf & "   (SELECT        FTBarcodeNo, FNHSysWHId, FTOrderNo, FNQuantity, FNQuantity - ISNULL"
                    _Cmd &= vbCrLf & "   ((SELECT        SUM(FNQuantity) AS FNQuantity"
                    _Cmd &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH (NOLOCK)"
                    _Cmd &= vbCrLf & "   WHERE        (FTBarcodeNo = A_1.FTBarcodeNo) AND (FNHSysWHId = A_1.FNHSysWHId) AND (FTOrderNo = A_1.FTOrderNo) AND "
                    _Cmd &= vbCrLf & "    (FTDocumentRefNo = A_1.FTDocumentNo) AND (FTDocumentNo <> '')), 0) AS FNQuantityBal, "
                    _Cmd &= vbCrLf & " FTPurchaseNo, FTDocumentNo, FNHSysRawMatId,FNPriceTrans,FTDocumentRefNo,FNPriceClose1,FNPriceClose2"
                    _Cmd &= vbCrLf & " FROM            (SELECT        BI.FTBarcodeNo, BI.FNHSysWHId, BI.FTOrderNo, SUM(BI.FNQuantity) AS FNQuantity, B.FTPurchaseNo, BI.FTDocumentNo,MAX(ISNULL(BI.FNPriceTrans,-1)) AS FNPriceTrans, "
                    _Cmd &= vbCrLf & " BI.FNHSysCmpId, B.FNHSysRawMatId,MAX(BI.FTDocumentRefNo) AS FTDocumentRefNo,MAX(ISNULL(BI.FNPriceClose1,-1)) AS FNPriceClose1,MAX(ISNULL(BI.FNPriceClose2,-1)) AS FNPriceClose2"
                    _Cmd &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH (NOLOCK) INNER JOIN"
                    _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON BI.FTBarcodeNo = B.FTBarcodeNo"
                    _Cmd &= vbCrLf & "  WHERE   (BI.FNHSysWHId =" & CInt(R!FNHSysWHId.ToString) & " ) "
                    _Cmd &= vbCrLf & "  AND (BI.FTOrderNo <>'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') AND ISNULL(BI.FTStateReserve,'') <>'1' "
                    _Cmd &= vbCrLf & "  AND   (B.FNHSysRawMatId =" & CInt(R!FNHSysRawMatId.ToString) & " ) "
                    _Cmd &= vbCrLf & "  GROUP BY BI.FTBarcodeNo, BI.FNHSysWHId, BI.FTOrderNo, B.FTPurchaseNo, BI.FTDocumentNo, BI.FNHSysCmpId, B.FNHSysRawMatId) AS A_1) "
                    _Cmd &= vbCrLf & "  AS B_1 "
                    _Cmd &= vbCrLf & "  INNER JOIN "
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON B_1.FNHSysRawMatId = M.FNHSysRawMatId LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH (NOLOCK) ON M.FNHSysRawMatColorId = MC.FNHSysRawMatColorId LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS MZ WITH (NOLOCK) ON M.FNHSysRawMatSizeId = MZ.FNHSysRawMatSizeId"

                    _Cmd &= vbCrLf & "   OUTER APPLY(SELECT TOP 1 X.FNPrice AS FNPriceX "
                    _Cmd &= vbCrLf & "  ,X.FNPriceTrans As FNPriceTransX "
                    _Cmd &= vbCrLf & "  ,X.FNPriceClosed1 "
                    _Cmd &= vbCrLf & "  ,X.FNPriceClosed2 "

                    _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENClosedOrderBarcode_Balance As X WITH(NOLOCK) "
                    _Cmd &= vbCrLf & "  Where X.FTBarcodeNo =B_1.FTBarcodeNo "
                    _Cmd &= vbCrLf & "    And X.FTOrderNo = B_1.FTOrderNo "
                    _Cmd &= vbCrLf & " And X.FTDocumentNo = B_1.FTDocumentNo "
                    _Cmd &= vbCrLf & "   And X.FNHSysWHId = B_1.FNHSysWHId "
                    _Cmd &= vbCrLf & " And X.FTDocumentRefNo =B_1.FTDocumentRefNo "
                    _Cmd &= vbCrLf & "  ) AS X "


                    _Cmd &= vbCrLf & " WHERE B_1.FNQuantityBal > 0"
                    _Cmd &= vbCrLf & " And  M.FNHSysRawMatId =" & CInt(R!FNHSysRawMatId.ToString)

                    _dtBar = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
                    _ReservedQtyBal = CDbl(R!FNReservedQty.ToString)
                    For Each x As DataRow In _dtBar.Select("FNQuantityBal=" & _ReservedQtyBal)

                        Call SaveBarcode(x!FTBarcodeNo.ToString, _ReservedQtyBal, _DocNo, x!FTDocumentNo.ToString, CInt(R!FNHSysWHId.ToString), CInt(R!FNHSysWHId.ToString), R!FTOrderNo.ToString, Val(x!FNPriceTrans.ToString), Me.FTOrderNo.Text, Val(x!FNPriceClose1.ToString), Val(x!FNPriceClose2.ToString))
                        _ReservedQtyBal = _ReservedQtyBal - CDbl(x!FNQuantityBal.ToString)

                        Exit For
                    Next
                    If _ReservedQtyBal > 0 Then
                        _ReservedQtyUse = 0
                        For Each y As DataRow In _dtBar.Select("FNQuantityBal > 0", "FNQuantityBal")
                            If _ReservedQtyBal <= CDbl(y!FNQuantityBal.ToString) Then
                                _ReservedQtyUse = _ReservedQtyBal
                            Else
                                _ReservedQtyUse = CDbl(y!FNQuantityBal.ToString)
                            End If

                            Call SaveBarcode(y!FTBarcodeNo.ToString, _ReservedQtyUse, _DocNo, y!FTDocumentNo.ToString, CInt(R!FNHSysWHId.ToString), CInt(R!FNHSysWHId.ToString), R!FTOrderNo.ToString, Val(y!FNPriceTrans.ToString), Me.FTOrderNo.Text, Val(y!FNPriceClose1.ToString), Val(y!FNPriceClose2.ToString))

                            _ReservedQtyBal = _ReservedQtyBal - _ReservedQtyUse
                            If _ReservedQtyBal = 0 Then
                                Exit For
                            End If
                        Next
                    End If
                    _WHCode = R!FTWHCode.ToString
                Next
            End With
            With _PopUp

                .oDt = _dt.Copy
                .Proc = False
                .ShowDialog()
                If (.Proc) Then
                    For Each R As DataRow In .oDt.Select("FTSelect='1'")
                        Call SendMail(R!FTDocumentNo.ToString, R!FNHSysWHId.ToString, R!FTWHCode.ToString)
                    Next
                End If
            End With
            Call W_PRCbShowBrowseData()
        Catch ex As Exception
        End Try
    End Sub

    Private Function SendMail(_FTReserveNo As String, _FNHSysWHId As Integer, _WHCode As String) As Boolean
        Try

            Dim _Qry As String = ""
            Dim _UserWareHouse As String = ""
            _Qry = "SELECT TOP 1  FTUserName FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS A WITH(NOLOCK) WHERE FNHSysWHId=" & Integer.Parse(Val(_FNHSysWHId)) & " "
            _UserWareHouse = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            If _UserWareHouse <> "" Then
                _Qry = "Select  TOP  1  FTStateMailToStock  "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTReserveNo='" & HI.UL.ULF.rpQuoted(_FTReserveNo) & "'"

                ' If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "1" Then
                _Qry = "SELECT [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.FN_GetUserStock_Mail('" & HI.UL.ULF.rpQuoted(_UserWareHouse) & "') AS FTUserWareHouse "
                _UserWareHouse = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "")
                Dim tmpsubject As String = ""
                Dim tmpmessage As String = ""

                tmpsubject = "Reserve No " & _FTReserveNo & "  From Warehouse " & _WHCode & "  For FO. " & FTOrderNo.Text

                tmpmessage = "Reserve From Warehouse " & _WHCode & ""
                tmpmessage &= vbCrLf & "For FO. " & FTOrderNo.Text
                tmpmessage &= vbCrLf & "Date : " & HI.Conn.SQLConn.GetField("Select Convert(nvarchar(10)," & HI.UL.ULDate.FormatDateDB & ",103) AS Date ", Conn.DB.DataBaseName.DB_INVEN, "")
                tmpmessage &= vbCrLf & "By :" & HI.ST.UserInfo.UserName
                tmpmessage &= vbCrLf & "Note : Auto Reserved... By" & HI.ST.UserInfo.UserName

                If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserWareHouse, tmpsubject, tmpmessage, 3, _FTReserveNo) Then
                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve "
                    _Qry &= vbCrLf & "  SET FTStateMailToStock='1' "
                    _Qry &= vbCrLf & " , FTMailToStockBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " , FTMailToStockDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "  ,FTMailToStockTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " WHERE FTReserveNo='" & HI.UL.ULF.rpQuoted(_FTReserveNo) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)
                End If

                ' End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveBarcode(FTBarcodeNo As String, BarCodeQty As Double, _DocNo As String, _DocRefNo As String, _WHId As String, _WHIdTo As String, _OrderNo As String _
                                  , _FNPriceTrans As Double, _OrderNoTo As String, Optional PriceClose1 As Double = -1, Optional PriceClose2 As Double = -1) As Boolean
        Dim _Str As String
        Dim _BarCode As String = FTBarcodeNo
        Dim _StateNew As Boolean
        Try

            _Str = " SELECT TOP 1 FTBarcodeNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK) WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(_DocNo) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "'  "
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans,FNPriceClose1,FNPriceClose2)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                _Str &= vbCrLf & "," & Val("" & _WHId) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                _Str &= vbCrLf & "," & BarCodeQty & " "
                _Str &= vbCrLf & ",'1','" & HI.UL.ULF.rpQuoted(_DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                _Str &= vbCrLf & ",CASE WHEN " & _FNPriceTrans & "<0 THEN NULL ELSE " & _FNPriceTrans & "  END "
                _Str &= vbCrLf & ",CASE WHEN " & PriceClose1 & "<0 THEN NULL ELSE " & PriceClose1 & "  END "
                _Str &= vbCrLf & ",CASE WHEN " & PriceClose2 & "<0 THEN NULL ELSE " & PriceClose2 & "  END "


            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & _WHId) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "' "
                _Str &= vbCrLf & ",FNQuantity=" & BarCodeQty & " "
                _Str &= vbCrLf & ",FTStateReserve='1' "
                _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & _FNPriceTrans & "<0 THEN NULL ELSE " & _FNPriceTrans & "  END "
                _Str &= vbCrLf & ",FNPriceClose1=CASE WHEN " & PriceClose1 & "<0 THEN NULL ELSE " & PriceClose1 & "  END "
                _Str &= vbCrLf & ",FNPriceClose2=CASE WHEN " & PriceClose2 & "<0 THEN NULL ELSE " & PriceClose2 & "  END "

                _Str &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "  AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "'  "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans,FNPriceClose1,FNPriceClose2)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                _Str &= vbCrLf & "," & Val("" & _WHIdTo) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_OrderNoTo) & "' "
                _Str &= vbCrLf & "," & BarCodeQty & " "
                _Str &= vbCrLf & ",'1','" & HI.UL.ULF.rpQuoted(_DocNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                _Str &= vbCrLf & ",CASE WHEN " & _FNPriceTrans & "<0 THEN NULL ELSE " & _FNPriceTrans & "  END "
                _Str &= vbCrLf & ",CASE WHEN " & PriceClose1 & "<0 THEN NULL ELSE " & PriceClose1 & "  END "
                _Str &= vbCrLf & ",CASE WHEN " & PriceClose2 & "<0 THEN NULL ELSE " & PriceClose2 & "  END "

            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & _WHIdTo) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNoTo) & "' "
                _Str &= vbCrLf & ",FNQuantity=" & BarCodeQty & " "
                _Str &= vbCrLf & ",FTStateReserve='1' "
                _Str &= vbCrLf & ",FNPriceClose1=CASE WHEN " & PriceClose1 & "<0 THEN NULL ELSE " & PriceClose1 & "  END "
                _Str &= vbCrLf & ",FNPriceClose2=CASE WHEN " & PriceClose2 & "<0 THEN NULL ELSE " & PriceClose2 & "  END "
                _Str &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "  AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(_DocNo) & "'  "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

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

    Private Sub RepositoryFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFTSelect.EditValueChanging
        Try
            With CType(ogdReserved.DataSource, DataTable)
                .AcceptChanges()
                With ogvReserved
                    If e.NewValue = "1" Then
                        If Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQty_Conv").ToString) < Double.Parse(.GetRowCellValue(.FocusedRowHandle, "FNQty_STK").ToString) Then
                            .SetRowCellValue(.FocusedRowHandle, "FNReservedQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQty_Conv").ToString))
                        Else
                            .SetRowCellValue(.FocusedRowHandle, "FNReservedQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQty_STK").ToString))
                        End If
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FNReservedQty", 0)
                    End If
                End With
                .AcceptChanges()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Try

            Me.FTOrderNoTo.Text = Me.FTOrderNo.Text
            Me.ogdReserved.DataSource = Nothing

        Catch ex As Exception

        End Try
    End Sub

   

    Private Sub ogvReserved_HiddenEditor(sender As Object, e As EventArgs) Handles ogvReserved.HiddenEditor
        Try
            With ogvReserved
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim NewValue As Double = CDbl(.GetRowCellValue(.FocusedRowHandle, "FNReservedQty").ToString)
                Dim Bal As Double = CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQty_STK").ToString)
                If NewValue > Bal Then
                    .SetRowCellValue(.FocusedRowHandle, "FNReservedQty", Bal)
                End If
                Dim _RawMatId As Integer = CInt(.GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString)
                Dim _ReservedQty As Double = 0 : Dim _ReservedQtyUse As Double = 0
                With CType(ogdReserved.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FNReservedQty > 0 and  FNHSysRawMatId=" & _RawMatId)
                        _ReservedQty += +CDbl(R!FNReservedQty.ToString)
                        If _ReservedQty > CDbl(R!FNQty_Conv.ToString) Then
                            _ReservedQtyUse = _ReservedQty - CDbl(R!FNQty_Conv.ToString)
                        End If
                        If _ReservedQtyUse > 0 Then
                            Exit For
                        End If
                    Next
                End With
                If _ReservedQtyUse > 0 Then
                    _ReservedQtyUse = CDbl(.GetRowCellValue(.FocusedRowHandle, "FNReservedQty").ToString) - _ReservedQtyUse
                    .SetRowCellValue(.FocusedRowHandle, "FNReservedQty", _ReservedQtyUse)
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysMerMatId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysMerMatId.EditValueChanged
        Try
            ogdReserved.DataSource = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PROC_LOAD(sender As Object, e As EventArgs) Handles ocmload.Click

    End Sub
End Class