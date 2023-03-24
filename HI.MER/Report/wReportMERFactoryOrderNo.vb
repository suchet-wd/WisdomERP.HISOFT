Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Data.SqlClient
Imports System.IO

Public Class wReportMERFactoryOrderNo

#Region "Variable Declaration"
    Private tSql As String
    Private _tFTOrderNo As String
#End Region

#Region "Procedure And Function"
    Public Sub New(ByVal ptFTOrderNo As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _tFTOrderNo = ptFTOrderNo
    End Sub

    Private Sub W_PRCxPreviewReport(ByVal ptFTOrderNo As String)
        Dim nFNHSysCmpId As Integer
        Dim tUserLogin As String
        Dim tFTSubOrderNo As String
        Dim nFNSewSeq As Integer
        Dim nFNPackSeq As Integer
        Try
            '================================================ TMERTOrder ========================================================
            Dim oDBdtOrderNo As System.Data.DataTable
            Dim tFTImage1 As String, tFTImage2 As String, tFTImage3 As String, tFTImage4 As String
            Dim tPath As String
            Dim tPathRefFTOrderNoImage1 As String, tPathRefFTOrderNoImage2 As String, tPathRefFTOrderNoImage3 As String, tPathRefFTOrderNoImage4 As String
            Dim tGetMainMaterialDesc As String

            tPath = ""
            tPath = HI.ST.SysInfo.SysPath & "Order\"


            tSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..USP_GETORDER_COMBINATION  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "' "
            HI.Conn.SQLConn.ExecuteOnly(tSql, Conn.DB.DataBaseName.DB_MERCHAN)


            tSql = ""
            tSql = "SELECT A.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "';"

            nFNHSysCmpId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            tSql = ""
            tSql = "SELECT A.FTOrderNo, ISNULL(A.FTImage1,'') AS FTImage1, ISNULL(A.FTImage2,'') AS FTImage2, ISNULL(A.FTImage3,'') AS FTImage3, ISNULL(A.FTImage4,'') AS FTImage4"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "';"

            oDBdtOrderNo = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdtOrderNo.Rows.Count > 0 Then

                For Each oCol As DataColumn In oDBdtOrderNo.Columns
                    Select Case oCol.ColumnName
                        Case "FTImage1"
                            If oDBdtOrderNo.Rows(0).Item("FTImage1").ToString() Like "*.*" Or oDBdtOrderNo.Rows(0).Item("FTImage1").ToString() = "" Then
                                tPathRefFTOrderNoImage1 = tPath & oDBdtOrderNo.Rows(0).Item("FTImage1").ToString()
                                tFTImage1 = oDBdtOrderNo.Rows(0).Item("FTImage1").ToString()
                            End If
                        Case "FTImage2"
                            If oDBdtOrderNo.Rows(0).Item("FTImage2").ToString() Like "*.*" Or oDBdtOrderNo.Rows(0).Item("FTImage2").ToString() = "" Then
                                tPathRefFTOrderNoImage2 = tPath & oDBdtOrderNo.Rows(0).Item("FTImage2").ToString()
                                tFTImage2 = oDBdtOrderNo.Rows(0).Item("FTImage2").ToString()
                            End If
                        Case "FTImage3"
                            If oDBdtOrderNo.Rows(0).Item("FTImage3").ToString() Like "*.*" Or oDBdtOrderNo.Rows(0).Item("FTImage3").ToString() = "" Then
                                tPathRefFTOrderNoImage3 = tPath & oDBdtOrderNo.Rows(0).Item("FTImage3").ToString()
                                tFTImage3 = oDBdtOrderNo.Rows(0).Item("FTImage3").ToString()
                            End If
                        Case "FTImage4"
                            If oDBdtOrderNo.Rows(0).Item("FTImage4").ToString() Like "*.*" Or oDBdtOrderNo.Rows(0).Item("FTImage4").ToString() = "" Then
                                tPathRefFTOrderNoImage4 = tPath & oDBdtOrderNo.Rows(0).Item("FTImage4").ToString()
                                tFTImage4 = oDBdtOrderNo.Rows(0).Item("FTImage4").ToString()
                            End If
                        Case Else
                            '...do nothing
                    End Select
                Next

            End If

            tSql = ""
            tSql = "DELETE A"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderTemp] AS A"
            tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "';"

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            tSql = ""
            tSql = "DELETE A"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderCombinationTemp] AS A"
            tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "';"

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            tGetMainMaterialDesc = ""
            tGetMainMaterialDesc = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GETMAINMATERIALDESC_FACTORY_ORDER] @FTOrderNo, "

            '...prepare TRPTTMEROrderTemp
            tSql = ""
            tSql = "DECLARE @FTOrderNo NVARCHAR(500);"
            tSql &= Environment.NewLine & "DECLARE @tblMainMaterial AS TABLE(FTOrderNo NVARCHAR(30), FTMainMaterial NVARCHAR(MAX));"
            tSql &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "';"
            tSql &= Environment.NewLine & "INSERT INTO @tblMainMaterial(FTMainMaterial)"
            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    tSql &= Environment.NewLine & tGetMainMaterialDesc & "N'EN';"
                Case HI.ST.Lang.eLang.TH
                    tSql &= Environment.NewLine & tGetMainMaterialDesc & "N'TH';"
                Case HI.ST.Lang.eLang.KM
                    tSql &= Environment.NewLine & tGetMainMaterialDesc & "N'EN';"
                Case HI.ST.Lang.eLang.VT
                    tSql &= Environment.NewLine & tGetMainMaterialDesc & "N'EN';"
            End Select
            tSql &= Environment.NewLine & "SELECT A.* FROM @tblMainMaterial AS A;"
            tSql &= Environment.NewLine & "IF (@@ROWCOUNT > 0)"
            tSql &= Environment.NewLine & "BEGIN"
            tSql &= Environment.NewLine & "  UPDATE A"
            tSql &= Environment.NewLine & "  SET A.FTOrderNo = @FTOrderNo"
            tSql &= Environment.NewLine & "  FROM @tblMainMaterial AS A"
            tSql &= Environment.NewLine & "  SELECT A.* FROM @tblMainMaterial AS A"
            tSql &= Environment.NewLine & "END;"
            tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TRPTTMERTOrderTemp] ([UserLogin], [FTOrderNo], [FTOrderNoImage1], [FTOrderNoImage2], [FTOrderNoImage3], [FTOrderNoImage4], [FTMainMaterial])"
            tSql &= Environment.NewLine & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', @FTOrderNo, NULL, NULL, NULL, NULL, A.FTMainMaterial"
            tSql &= Environment.NewLine & "FROM @tblMainMaterial AS A;"

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '...update HITECH_MERCHAN..TRPTTMEROrderTemp with parameter
            '==================================================================================================================================================================
            Dim FTOrderNoImage1 As Byte(), FTOrderNoImage2 As Byte(), FTOrderNoImage3 As Byte(), FTOrderNoImage4 As Byte()

            Dim pParamFTOrderNoImage1 As New SqlParameter("@FTOrderNoImage1", SqlDbType.VarBinary)
            Dim pParamFTOrderNoImage2 As New SqlParameter("@FTOrderNoImage2", SqlDbType.VarBinary)
            Dim pParamFTOrderNoImage3 As New SqlParameter("@FTOrderNoImage3", SqlDbType.VarBinary)
            Dim pParamFTOrderNoImage4 As New SqlParameter("@FTOrderNoImage4", SqlDbType.VarBinary)

            If tPathRefFTOrderNoImage1 <> "" AndAlso File.Exists(tPathRefFTOrderNoImage1) Then
                FTOrderNoImage1 = HI.UL.ULImage.ConvertImageToByteArray(tPathRefFTOrderNoImage1)
                pParamFTOrderNoImage1.Value = FTOrderNoImage1
            Else
                pParamFTOrderNoImage1.Value = Nothing
            End If

            If tPathRefFTOrderNoImage2 <> "" AndAlso File.Exists(tPathRefFTOrderNoImage2) Then
                FTOrderNoImage2 = HI.UL.ULImage.ConvertImageToByteArray(tPathRefFTOrderNoImage2)
                pParamFTOrderNoImage2.Value = FTOrderNoImage2
            Else
                pParamFTOrderNoImage2.Value = Nothing
            End If

            If tPathRefFTOrderNoImage3 <> "" AndAlso File.Exists(tPathRefFTOrderNoImage3) Then
                FTOrderNoImage3 = HI.UL.ULImage.ConvertImageToByteArray(tPathRefFTOrderNoImage3)
                pParamFTOrderNoImage3.Value = FTOrderNoImage3
            Else
                pParamFTOrderNoImage3.Value = Nothing
            End If

            If tPathRefFTOrderNoImage4 <> "" AndAlso File.Exists(tPathRefFTOrderNoImage4) Then
                FTOrderNoImage4 = HI.UL.ULImage.ConvertImageToByteArray(tPathRefFTOrderNoImage4)
                pParamFTOrderNoImage4.Value = FTOrderNoImage4
            Else
                pParamFTOrderNoImage4.Value = Nothing
            End If

            If tPathRefFTOrderNoImage1 <> "" Or tPathRefFTOrderNoImage2 <> "" Or tPathRefFTOrderNoImage3 <> "" Or tPathRefFTOrderNoImage4 <> "" Then
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()

                Dim oCmd As SqlCommand

                For nLoopCmd As Integer = 1 To 2
                    Select Case nLoopCmd
                        Case 1
                            Dim tSqlUpdateParam As String
                            tSqlUpdateParam = ""

                            tSql = ""
                            tSql = "UPDATE A SET"

                            If Not pParamFTOrderNoImage1.Value Is Nothing Then
                                If tSqlUpdateParam <> "" Then tSqlUpdateParam &= "          ,"
                                tSqlUpdateParam &= Environment.NewLine & "         A.FTOrderNoImage1 = @FTOrderNoImage1"
                            End If

                            If Not pParamFTOrderNoImage2.Value Is Nothing Then
                                If tSqlUpdateParam <> "" Then tSqlUpdateParam &= "          ,"
                                tSqlUpdateParam &= Environment.NewLine & "         A.FTOrderNoImage2 = @FTOrderNoImage2"
                            End If

                            If Not pParamFTOrderNoImage3.Value Is Nothing Then
                                If tSqlUpdateParam <> "" Then tSqlUpdateParam &= "          ,"
                                tSqlUpdateParam &= Environment.NewLine & "         A.FTOrderNoImage3 = @FTOrderNoImage3"
                            End If

                            If Not pParamFTOrderNoImage4.Value Is Nothing Then
                                If tSqlUpdateParam <> "" Then tSqlUpdateParam &= "          ,"
                                tSqlUpdateParam &= Environment.NewLine & "         A.FTOrderNoImage4 = @FTOrderNoImage4"
                            End If

                            If tSqlUpdateParam <> "" Then
                                tSql &= tSqlUpdateParam
                                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderTemp] AS A"
                                tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"

                                oCmd = New SqlCommand(tSql, HI.Conn.SQLConn.Cnn)

                            End If

                        Case 2
                            If Not pParamFTOrderNoImage1.Value Is Nothing Then
                                oCmd.Parameters.Add(pParamFTOrderNoImage1)
                            End If

                            If Not pParamFTOrderNoImage2.Value Is Nothing Then
                                oCmd.Parameters.Add(pParamFTOrderNoImage2)
                            End If

                            If Not pParamFTOrderNoImage3.Value Is Nothing Then
                                oCmd.Parameters.Add(pParamFTOrderNoImage3)
                            End If

                            If Not pParamFTOrderNoImage4.Value Is Nothing Then
                                oCmd.Parameters.Add(pParamFTOrderNoImage4)
                            End If

                    End Select

                Next nLoopCmd

                If Not pParamFTOrderNoImage1.Value Is Nothing Or Not pParamFTOrderNoImage2.Value Is Nothing Or Not pParamFTOrderNoImage3.Value Is Nothing Or Not pParamFTOrderNoImage4.Value Is Nothing Then
                    oCmd.ExecuteNonQuery()
                End If

                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

            End If

            '...cross-tab TMERTOrder Combination
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTOrderNo AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FNHSysStyleId AS INT;")
            oStrBuilder.AppendLine("DECLARE @FTUserLogin AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FTUserLogin = N'{0}';", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
            oStrBuilder.AppendLine(String.Format("SET @FTOrderNo = N'{0}';", ptFTOrderNo))
            oStrBuilder.AppendLine("SELECT TOP 1 @FNHSysStyleId = A.FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK) WHERE A.FTOrderNo = @FTOrderNo;")
            oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TRPTTMERTOrderCombinationTemp]([UserLogin],[FTOrderNo],[FNSeq],[FNHSysMatColorId],[FNHSysRawMatColorId],[FTRawMatColorDesc],[FTPositionPartName])")

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.TH
                    oStrBuilder.AppendLine("SELECT DISTINCT @FTUserLogin, @FTOrderNo, x.FNSeq, x.FNHSysMatColorId, x.FNHSysRawMatColorId, x.FTRawMatColorCode + ' ' + x.FTRawMatColorNameTH, y.FTPositionPartName")
                Case Else
                    oStrBuilder.AppendLine("SELECT DISTINCT @FTUserLogin, @FTOrderNo, x.FNSeq, x.FNHSysMatColorId, x.FNHSysRawMatColorId, x.FTRawMatColorCode + ' ' + x.FTRawMatColorNameEN, y.FTPositionPartName")

            End Select

            oStrBuilder.AppendLine("FROM (SELECT A.FNHSysStyleId, A.FNSeq, A.FNMerMatSeq, A.FNColorWaySeq,")
            oStrBuilder.AppendLine("			 A.FTRunColor, A.FNHSysRawMatColorId, C.FTRawMatColorCode, C.FTRawMatColorNameEN, C.FTRawMatColorNameTH, A.FNHSysMatColorId, B.FTMatColorCode")
            oStrBuilder.AppendLine("	  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTStyle_ColorWay] AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS B WITH(NOLOCK) ON A.FNHSysMatColorId = B.FNHSysMatColorId")
            oStrBuilder.AppendLine("				  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TINVENMMatColor] AS C WITH(NOLOCK) ON A.FNHSysRawMatColorId = C.FNHSysRawMatColorId")
            oStrBuilder.AppendLine("	  WHERE A.FNHSysStyleId = @FNHSysStyleId) AS x INNER JOIN (SELECT L1.FNHSysStyleId, L1.FNSeq, L1.FTPositionPartName")
            oStrBuilder.AppendLine("															   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTStyle_Mat] AS L1 WITH(NOLOCK)")
            oStrBuilder.AppendLine("															   WHERE L1.FNHSysStyleId = @FNHSysStyleId")
            oStrBuilder.AppendLine("																	 AND L1.FTStateCombination = N'1'")
            oStrBuilder.AppendLine("																	 AND ISNULL(L1.FTPositionPartName, N'') <> '') AS y ON x.FNHSysStyleId = y.FNHSysStyleId AND x.FNSeq = y.FNSeq")
            oStrBuilder.AppendLine("												   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS z WITH(NOLOCK) ON x.FNHSysMatColorId = z.FNHSysMatColorId")
            oStrBuilder.AppendLine("--ORDER BY z.FNMatColorSeq ASC, x.FNSeq ASC;")

            tSql = ""
            tSql = oStrBuilder.ToString()

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '====================================================================================================================

            '=================================================== TMERTOrderSub_Sew ==============================================
            If Me.ockIncludeSubOrderNoSewing.Checked = True Then
                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TRPTTMERTOrderSubSewingTemp AS A"
                tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage AS FTSewImage"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "ORDER BY A.FTSubOrderNo ASC, A.FNSewSeq ASC;"

                Dim oDBdtSew As DataTable
                oDBdtSew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If oDBdtSew.Rows.Count > 0 Then
                    tPath = ""
                    tPath = HI.ST.SysInfo.SysPath & "Order\OrderNo\SubOrderNo\Sewing\"

                    Dim tFTSewImage As String
                    Dim tPathRefFTSewImage As String
                    Dim FTSewImage As Byte()

                    Dim nLoopSew As Integer
                    For nLoopSew = 0 To oDBdtSew.Rows.Count - 1
                        tFTSubOrderNo = oDBdtSew.Rows(nLoopSew).Item("FTSubOrderNO").ToString()
                        nFNSewSeq = oDBdtSew.Rows(nLoopSew).Item("FNSewSeq")

                        tPathRefFTSewImage = ""
                        tFTSewImage = ""
                        FTSewImage = Nothing

                        If oDBdtSew.Rows(nLoopSew).Item("FTSewImage").ToString() Like "*.*" Or oDBdtSew.Rows(nLoopSew).Item("FTSewImage").ToString() = "" Then
                            tPathRefFTSewImage = tPath & oDBdtSew.Rows(nLoopSew).Item("FTSewImage").ToString()
                            tFTSewImage = oDBdtSew.Rows(nLoopSew).Item("FTSewImage").ToString()
                        End If

                        If tPathRefFTSewImage <> "" AndAlso File.Exists(tPathRefFTSewImage) Then
                            FTSewImage = HI.UL.ULImage.ConvertImageToByteArray(tPathRefFTSewImage)

                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                            HI.Conn.SQLConn.SqlConnectionOpen()

                            Dim oCmdSew As SqlCommand

                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderSubSewingTemp] (UserLogin, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewImage)"
                            tSql &= Environment.NewLine & "VALUES (@UserLogin, @FTOrderNo, @FTSubOrderNo, @FNSewSeq, @FTSewImage)"

                            oCmdSew = New SqlCommand(tSql, HI.Conn.SQLConn.Cnn)

                            oCmdSew.Parameters.AddWithValue("@UserLogin", HI.ST.UserInfo.UserName)
                            oCmdSew.Parameters.AddWithValue("@FTOrderNo", ptFTOrderNo)
                            oCmdSew.Parameters.AddWithValue("@FTSubOrderNo", tFTSubOrderNo)
                            oCmdSew.Parameters.AddWithValue("@FNSewSeq", nFNSewSeq)

                            Dim pParamFTSewImage As New SqlParameter("@FTSewImage", SqlDbType.VarBinary)
                            pParamFTSewImage.Value = FTSewImage

                            If Not pParamFTSewImage Is Nothing Then
                                oCmdSew.Parameters.Add(pParamFTSewImage)
                            End If

                            oCmdSew.ExecuteNonQuery()

                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                        Else
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderSubSewingTemp] (UserLogin, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewImage)"
                            tSql &= Environment.NewLine & "VALUES ('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "','" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "'," & nFNSewSeq & ", NULL)"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                        End If

                    Next nLoopSew

                End If

            End If

            '=================================================== TMERTOrderSub_Pack ==============================================
            If Me.ockIncludeSubOrderNoPacking.Checked = True Then
                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TRPTTMERTOrderSubPackingTemp AS A"
                tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage AS FTPackImage"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "ORDER BY A.FTSubOrderNo ASC, A.FNPackSeq ASC;"

                Dim oDBdtPack As DataTable
                oDBdtPack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If oDBdtPack.Rows.Count > 0 Then
                    tPath = ""
                    tPath = HI.ST.SysInfo.SysPath & "Order\OrderNo\SubOrderNo\Packing\"

                    Dim tFTPackImage As String
                    Dim tPathRefFTPackImage As String
                    Dim FTPackImage As Byte()

                    Dim nLoopPack As Integer
                    For nLoopPack = 0 To oDBdtPack.Rows.Count - 1
                        tFTSubOrderNo = oDBdtPack.Rows(nLoopPack).Item("FTSubOrderNO").ToString()
                        nFNPackSeq = oDBdtPack.Rows(nLoopPack).Item("FNPackSeq")

                        tPathRefFTPackImage = ""
                        tFTPackImage = ""
                        tFTPackImage = Nothing

                        If oDBdtPack.Rows(nLoopPack).Item("FTPackImage").ToString() Like "*.*" Or oDBdtPack.Rows(nLoopPack).Item("FTPackImage").ToString() = "" Then
                            tPathRefFTPackImage = tPath & oDBdtPack.Rows(nLoopPack).Item("FTPackImage").ToString()
                            tFTPackImage = oDBdtPack.Rows(nLoopPack).Item("FTPackImage").ToString()
                        End If

                        If tPathRefFTPackImage <> "" AndAlso File.Exists(tPathRefFTPackImage) Then
                            FTPackImage = HI.UL.ULImage.ConvertImageToByteArray(tPathRefFTPackImage)

                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                            HI.Conn.SQLConn.SqlConnectionOpen()

                            Dim oCmdPack As SqlCommand

                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderSubPackingTemp] (UserLogin, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackImage)"
                            tSql &= Environment.NewLine & "VALUES (@UserLogin, @FTOrderNo, @FTSubOrderNo, @FNPackSeq, @FTPackImage)"

                            oCmdPack = New SqlCommand(tSql, HI.Conn.SQLConn.Cnn)

                            oCmdPack.Parameters.AddWithValue("@UserLogin", HI.ST.UserInfo.UserName)
                            oCmdPack.Parameters.AddWithValue("@FTOrderNo", ptFTOrderNo)
                            oCmdPack.Parameters.AddWithValue("@FTSubOrderNo", tFTSubOrderNo)
                            oCmdPack.Parameters.AddWithValue("@FNPackSeq", nFNPackSeq)

                            Dim pParamFTPackImage As New SqlParameter("@FTPackImage", SqlDbType.VarBinary)
                            pParamFTPackImage.Value = FTPackImage

                            If Not pParamFTPackImage Is Nothing Then
                                oCmdPack.Parameters.Add(pParamFTPackImage)
                            End If

                            oCmdPack.ExecuteNonQuery()

                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                        Else
                            tSql = ""
                            tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderSubPackingTemp] (UserLogin, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackImage)"
                            tSql &= Environment.NewLine & "VALUES ('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "','" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "'," & nFNPackSeq & ", NULL)"

                            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                        End If

                    Next nLoopPack

                End If

            End If

            '=================================================== TRPTTMERTOrderSubSizeSpecTemp ====================================================
            If Me.ockIncludeSubOrderNoSizeSpec.Checked = True Then
                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TRPTTMERTOrderSubSizeSpecTemp] AS A"
                tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSeq, A.FNHSysMatSizeId, A.FTSizeSpecDesc, A.FTSizeSpecExtension"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                tSql &= Environment.NewLine & "ORDER BY A.FTSubOrderNo ASC;"

                Dim oDBdtSizeSpec As System.Data.DataTable

                oDBdtSizeSpec = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If oDBdtSizeSpec.Rows.Count > 0 Then
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TRPTTMERTOrderSubSizeSpecTemp] ([UserLogin],[FTOrderNo],[FTSubOrderNo],[FNSeq],[FNHSysMatSizeId],[FTSizeSpecDesc],[FTSizeSpecExtension])"
                    tSql &= Environment.NewLine & "SELECT N'" & HI.ST.UserInfo.UserName & "', A.FTOrderNo, A.FTSubOrderNo, A.FNSeq, A.FNHSysMatSizeId, A.FTSizeSpecDesc, A.FTSizeSpecExtension"
                    tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                End If

            End If

            Dim tReportPath As String = "Merchandise Report\"
            Dim tReportName As String = ""
            Dim tFormular As String = ""

            '-------------------------------------------------MERFactoryOrder Report/MERFactoryOrderBreakdown Report ---------------------------------------------------------------------------
            tReportName = "MERFactoryOrder.rpt"

            If ptFTOrderNo <> "" Then
                If tFormular <> "" Then tFormular &= " AND "
                tFormular &= "{TRPTTMERTOrderTemp.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
            End If

            If HI.ST.UserInfo.UserName <> "" Then
                If tFormular <> "" Then tFormular &= " AND "
                tFormular &= "{TRPTTMERTOrderTemp.UserLogin} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName.ToString()) & "'"
            End If

            '...validate path
            If Not IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                MsgBox("Unable to locate report file : " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            End If

            With New HI.RP.Report
                .ReportFolderName = tReportPath
                .ReportName = tReportName
                .Formular = tFormular

                Dim tLang As String = ""
                Dim tReportTitle As String = ""

                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        tReportTitle = "รายงานเลขที่ใบสั่งผลิตโรงงาน"
                        tLang = "TH"
                    Case Else
                        tReportTitle = "Report Factory Order No."
                        tLang = "EN"
                End Select

                .AddParameter("pmTitle", tReportTitle)
                .AddParameter("pmLang", tLang)

                .AddParameter("pmFNHSysCmpId", nFNHSysCmpId)
                .AddParameter("pmUserLogin", HI.ST.UserInfo.UserName.ToString())

                .Preview()

            End With
            '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            '----------------------------------------------------- MERFactoryOrderSub Report/MERFactoryOrderSub Breakdown Report ----------------------------------------------------------------
            If Me.ockIncludeSubOrderNo.Checked = True Then
                tReportName = ""
                tReportName = "MERFactorySubOrder.rpt"

                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TMERTOrderSub.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                    End If

                    With New HI.RP.Report
                        .ReportFolderName = tReportPath
                        .ReportName = tReportName
                        .Formular = tFormular

                        Dim tLang As String = ""
                        Dim tReportTitle As String = ""

                        Select Case HI.ST.Lang.Language

                            Case HI.ST.Lang.eLang.TH
                                tReportTitle = "รายงานเลขที่ใบสั่งผลิตย่อยโรงงาน"
                                tLang = "TH"
                            Case Else
                                tReportTitle = "Report Factory Sub Order No."
                                tLang = "EN"
                        End Select

                        .AddParameter("pmTitle", tReportTitle)
                        .AddParameter("pmLang", tLang)

                        .AddParameter("pmFNHSysCmpId", nFNHSysCmpId)
                        .AddParameter("pmUserLogin", HI.ST.UserInfo.UserName.ToString())

                        .Preview()

                    End With

                Else
                    MsgBox("Unable to locate report file : " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                End If

            End If
            '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            '--------------------------------------------------------------------MERFactorySubOrderSew Report ----------------------------------------------------------------------------------
            If Me.ockIncludeSubOrderNoSewing.Checked = True Then
                tReportName = ""
                tReportName = "MERFactorySubOrderSew.rpt"

                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TMERTOrderSub_Sew.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                    End If

                    If HI.ST.UserInfo.UserName <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TRPTTMERTOrderSubSewingTemp.UserLogin} = '" & HI.ST.UserInfo.UserName & "'"
                    End If

                    With New HI.RP.Report
                        .ReportFolderName = tReportPath
                        .ReportName = tReportName
                        .Formular = tFormular

                        Dim tLang As String = ""
                        Dim tReportTitle As String = ""

                        Select Case HI.ST.Lang.Language

                            Case HI.ST.Lang.eLang.TH
                                tReportTitle = "รายงานเลขที่ใบสั่งผลิตย่อยโรงงาน ขั้นตอนงานเย็บ"
                                tLang = "TH"
                            Case Else
                                tReportTitle = "Report Factory Sub Order No. Sewing"
                                tLang = "EN"
                        End Select

                        .AddParameter("pmTitle", tReportTitle)
                        .AddParameter("pmLang", tLang)

                        .AddParameter("pmFNHSysCmpId", nFNHSysCmpId)
                        .AddParameter("pmUserLogin", HI.ST.UserInfo.UserName.ToString())

                        .Preview()

                    End With

                Else
                    MsgBox("Unable to locate report file: " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                End If

            End If
            '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            '-------------------------------------------------------------------------MERFactorySubOrderPack Report ----------------------------------------------------------------------------
            If Me.ockIncludeSubOrderNoPacking.Checked = True Then
                tReportName = ""
                tReportName = "MERFactorySubOrderPack.rpt"

                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TMERTOrderSub_Pack.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                    End If

                    If HI.ST.UserInfo.UserName <> "" Then
                        If tFormular <> "" Then tFormular &= "  AND "
                        tFormular &= "{TRPTTMERTOrderSubPackingTemp.UserLogin} = '" & HI.ST.UserInfo.UserName & "'"
                    End If

                    With New HI.RP.Report
                        .ReportFolderName = tReportPath
                        .ReportName = tReportName
                        .Formular = tFormular

                        Dim tLang As String = ""
                        Dim tReportTitle As String = ""

                        Select Case HI.ST.Lang.Language
                            Case HI.ST.Lang.eLang.TH
                                tReportTitle = "รายงานเลขที่ใบสั่งผลิตย่อยโรงงาน ขั้นตอนงานแพ็ค"
                                tLang = "TH"
                            Case Else
                                tReportTitle = "Report Factory Sub Order No. Packing"
                                tLang = "EN"
                        End Select

                        .AddParameter("pmTitle", tReportTitle)
                        .AddParameter("pmLang", tLang)

                        .AddParameter("pmFNHSysCmpId", nFNHSysCmpId)
                        .AddParameter("pmUserLogin", HI.ST.UserInfo.UserName.ToString())

                        .Preview()

                    End With

                Else
                    MsgBox("Unable to locate report file : " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                End If

            End If
            '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            '--------------------------------------------------------------MERFactorySubOrderSizeSpec Report -----------------------------------------------------------------------------------
            If Me.ockIncludeSubOrderNoSizeSpec.Checked = True Then
                tReportName = ""
                tReportName = "MERFactorySubOrderSizeSpec.rpt"
                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TRPTTMERTOrderSubSizeSpecTemp.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                    End If

                    If HI.ST.UserInfo.UserName <> "" Then
                        If tFormular <> "" Then tFormular &= "  AND "
                        tFormular &= "{TRPTTMERTOrderSubSizeSpecTemp.UserLogin} = '" & HI.ST.UserInfo.UserName & "'"
                    End If

                    With New HI.RP.Report
                        .ReportFolderName = tReportPath
                        .ReportName = tReportName
                        .Formular = tFormular

                        Dim tLang As String = ""
                        Dim tReportTitle As String = ""

                        Select Case HI.ST.Lang.Language

                            Case HI.ST.Lang.eLang.TH
                                tReportTitle = "รายงานเลขที่ใบสั่งผลิตย่อยโรงงาน ข้อมูลเฉพาะขนาดผลิตภัณฑ์"
                                tLang = "TH"

                            Case Else
                                tReportTitle = "Report Factory Sub Order No. Size Spec Information"
                                tLang = "EN"
                        End Select

                        .AddParameter("pmTitle", tReportTitle)
                        .AddParameter("pmLang", tLang)

                        .AddParameter("pmFNHSysCmpId", nFNHSysCmpId)
                        .AddParameter("pmUserLogin", HI.ST.UserInfo.UserName.ToString())

                        .Preview()

                    End With

                Else
                    MsgBox("Unable to locate report file : " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                End If

            End If
            '----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            If Me.ockIncludeSubOrderNoComponent.Checked = True Then
                tReportName = ""
                tReportName = "MERFactorySubComponent.rpt"
                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        REM tFormular &= "{TMERTOrderSub_Component.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                        tFormular &= "{vwTMERTOrderSub_Component.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                    End If

                    With New HI.RP.Report
                        .ReportFolderName = tReportPath
                        .ReportName = tReportName
                        .Formular = tFormular

                        .Preview()

                    End With

                Else
                    MsgBox("Unable to locate report file : " & tReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

    End Sub

#End Region

    Private Sub ocmCancel_Click(sender As Object, e As EventArgs) Handles ocmCancel.Click
        Me.Close()
    End Sub

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click
        If _tFTOrderNo <> "" Then
            Call W_PRCxPreviewReport(_tFTOrderNo)
        End If
    End Sub

    Private Sub wReportMERFactoryOrderNo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _tFTOrderNo <> "" Then
            Me.ocmOK.Focus()
        End If
    End Sub

End Class