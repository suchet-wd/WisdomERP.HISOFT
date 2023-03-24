Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.IO

Public Class wReportMERFactorySubOrderNo

#Region "Variable Declaration"
    Private tSql As String
    Private _tFTOrderNo As String
    Private _tFTSubOrderNo As String
#End Region

#Region "Procedure And Function"
    Public Sub New(ByVal ptFTOrderNo As String, ByVal ptFTSubOrderNo As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _tFTOrderNo = ptFTOrderNo
        _tFTSubOrderNo = ptFTSubOrderNo
    End Sub

    Private Sub W_PRCxPreviewReport(ByVal ptFTOrderNo As String, ByVal ptFTSubOrderNo As String)
        Dim nFNHSysCmpId As Integer
        Dim tFTSubOrderNo As String
        Dim nFNSewSeq As Integer
        Dim nFNPackSeq As Integer
        Try
            '================================================ TMERTOrderSub ========================================================
            Dim tPath As String

            tPath = ""
            tPath = HI.ST.SysInfo.SysPath & "Order\"


            tSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..USP_GETORDER_COMBINATION  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "' "
            HI.Conn.SQLConn.ExecuteOnly(tSql, Conn.DB.DataBaseName.DB_MERCHAN)


            tSql = ""
            tSql = "SELECT A.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "';"

            nFNHSysCmpId = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            '=================================================== TMERTOrderSub_Sew ==============================================
            If Me.ockIncludeSubOrderNoSewing.Checked = True Then
                tSql = ""
                tSql = "DELETE A"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TRPTTMERTOrderSubSewingTemp AS A"
                tSql &= Environment.NewLine & "WHERE A.UserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "';"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage AS FTSewImage"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"
                tSql &= Environment.NewLine & "ORDER BY A.FNSewSeq ASC;"

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
                        tFTSubOrderNo = ptFTSubOrderNo 'oDBdtSew.Rows(nLoopSew).Item("FTSubOrderNO").ToString()
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
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "';"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage AS FTPackImage"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"
                tSql &= Environment.NewLine & "ORDER BY A.FNPackSeq ASC;"

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
                        tFTSubOrderNo = ptFTSubOrderNo 'oDBdtPack.Rows(nLoopPack).Item("FTSubOrderNO").ToString()
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
                tSql &= Environment.NewLine & "      AND A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "';"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSeq, A.FNHSysMatSizeId, A.FTSizeSpecDesc, A.FTSizeSpecExtension"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "';"

                Dim oDBdtSizeSpec As System.Data.DataTable

                oDBdtSizeSpec = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If oDBdtSizeSpec.Rows.Count > 0 Then
                    tSql = ""
                    tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TRPTTMERTOrderSubSizeSpecTemp] ([UserLogin],[FTOrderNo],[FTSubOrderNo],[FNSeq],[FNHSysMatSizeId],[FTSizeSpecDesc],[FTSizeSpecExtension])"
                    tSql &= Environment.NewLine & "SELECT N'" & HI.ST.UserInfo.UserName & "', A.FTOrderNo, A.FTSubOrderNo, A.FNSeq, A.FNHSysMatSizeId, A.FTSizeSpecDesc, A.FTSizeSpecExtension"
                    tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] AS A WITH(NOLOCK)"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"
                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                End If

            End If

            Dim tReportPath As String = "Merchandise Report\"
            Dim tReportName As String = ""
            Dim tFormular As String = ""

            '----------------------------------------------------- MERFactoryOrderSub Report/MERFactoryOrderSub Breakdown Report ----------------------------------------------------------------
            tReportName = ""
            tReportName = "MERFactorySubOrder.rpt"

            '...validate path
            If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                tFormular = ""

                If ptFTOrderNo <> "" Then
                    If tFormular <> "" Then tFormular &= " AND "
                    tFormular &= "{TMERTOrderSub.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                End If

                If ptFTSubOrderNo <> "" Then
                    If tFormular <> "" Then tFormular &= " AND "
                    tFormular &= "{TMERTOrderSub.FTSubOrderNo} = '" & ptFTSubOrderNo.ToString() & "'"
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
                Exit Sub
            End If

            If Me.ockIncludeSubOrderNoSewing.Checked = True Then
                '--------------------------------------------------------------------MERFactorySubOrderSew Report ----------------------------------------------------------------------------------
                tReportName = ""
                tReportName = "MERFactorySubOrderSew.rpt"

                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TMERTOrderSub_Sew.FTOrderNo} = '" & ptFTOrderNo.ToString() & "'"
                    End If

                    If ptFTSubOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TMERTOrderSub_Sew.FTSubOrderNo} = '" & ptFTSubOrderNo.ToString() & "'"
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

                    If ptFTSubOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TMERTOrderSub_Pack.FTSubOrderNo} = '" & ptFTSubOrderNo.ToString() & "'"
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

                    If ptFTSubOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        tFormular &= "{TRPTTMERTOrderSubSizeSpecTemp.FTSubOrderNo} = '" & ptFTSubOrderNo.ToString() & "'"
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


            '--------------------------------------------------------------MERFactorySubOrderSizeSpec Report -----------------------------------------------------------------------------------

            If Me.ockIncludeSubOrderNoComponent.Checked = True Then
                tReportName = ""
                tReportName = "MERFactorySubComponent.rpt"
                If IO.File.Exists(Application.StartupPath & "\Reports\" & tReportPath & tReportName) Then
                    tFormular = ""

                    If ptFTOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        REM 2014/07/10 tFormular &= "{TMERTOrderSub_Component.FTOrderNo} = '" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "'"
                        tFormular &= "{vwTMERTOrderSub_Component.FTOrderNo} = '" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "'"
                    End If

                    If ptFTSubOrderNo <> "" Then
                        If tFormular <> "" Then tFormular &= " AND "
                        REM 2014/07/10 tFormular &= "{TMERTOrderSub_Component.FTSubOrderNo} = '" & ptFTSubOrderNo.ToString() & "'"
                        tFormular &= "{vwTMERTOrderSub_Component.FTSubOrderNo} = '" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "'"
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

    Private Sub wReportMERFactorySubOrderNo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _tFTSubOrderNo <> "" Then
            Me.ocmOK.Focus()
        End If
    End Sub

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click
        If _tFTOrderNo <> "" And _tFTSubOrderNo <> "" Then
            Call W_PRCxPreviewReport(_tFTOrderNo, _tFTSubOrderNo)
        End If
    End Sub

    Private Sub ocmCancel_Click(sender As Object, e As EventArgs) Handles ocmCancel.Click
        Me.Close()
    End Sub

End Class