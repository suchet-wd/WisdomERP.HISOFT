Imports System.IO
Imports DevExpress.Spreadsheet
Imports Microsoft.Win32

Public Class wImportFileRMDS

    Private MappIngSupl As wListRMDSMappingSupl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        MappIngSupl = New wListRMDSMappingSupl
        HI.TL.HandlerControl.AddHandlerObj(MappIngSupl)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, MappIngSupl.Name.ToString.Trim, MappIngSupl)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click


    End Sub

    Private Sub ocmimportoptiplan_Click(sender As Object, e As EventArgs) Handles ocmimportexcel.Click
        Try
            Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
            opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            opFileDialog.ShowDialog()

            Try
                If opFileDialog.FileName <> "" Then
                    Dim _Pls As New HI.TL.SplashScreen("Importing...File Please Wait...")
                    Dim _FileName As String = opFileDialog.FileName
                    Dim FTFileDataDate As String = HI.Conn.SQLConn.GetField("select  convert(nvarchar(10),getdate(),112)", Conn.DB.DataBaseName.DB_SYSTEM, "")

                    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                    System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                    System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                    System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"


                    Try

                        Dim oDBdtExcel As DataTable = HI.UL.ReadExcel.Read(_FileName, "", 0)
                        Dim cmdstring As String = ""
                        Dim IMCode As String = ""
                        Dim IMCodeBF As String = ""
                        Dim SuplCode As String = ""
                        Dim SuplLocation As String = ""
                        Dim SupName As String = ""
                        Dim MatDes As String = ""
                        Dim FTRMDSEFFStartDt As String = ""
                        Dim FTRMDSEFFEndDt As String = ""
                        Dim FTRMDSUpdated As String = ""
                        Dim EffDate As String = ""
                        Dim Season As String = ""


                        Dim SellingUnit As String = ""
                        Dim SellingQty As Decimal = 0

                        Dim SDate As String = ""
                        Dim EDate As String = ""
                        Dim RMDSCur As String = ""
                        Dim ExcRate As Decimal = 0
                        Dim Price As Decimal = 0
                        Dim PriceUsd As Decimal = 0

                        Dim FixQuotesPrice As Decimal = 0
                        Dim FixQuotesUSDPrice As Decimal = 0
                        Dim TotalProceQuoteCurr As Decimal = 0
                        Dim PriceSTDUnit As String = ""

                        Dim PriceSTD As Decimal = 0


                        Dim Seq As Integer = 0
                        Dim RowSeq As Integer = 0


                        Dim FNPurchasingLT As Integer = 0
                        Dim FNDyeFinishPackLT As Integer = 0
                        Dim FNKnitWeaveLT As Integer = 0
                        Dim FNYarnLT As Integer = 0
                        Dim FNSalesSampleLT As Integer = 0

                        Dim FTLTAdHoc As String = ""
                        Dim FTLTComments As String = ""

                        Dim FTMINUOM As String = ""
                        Dim FNProdMinQTY As Integer = 0
                        Dim FNProdMinColorQTY As Integer = 0
                        Dim FNSampleMinQTY, FNSampleMinColorQTY As Integer

                        Dim FTMATERIALTYPE As String = ""
                        Dim FTSUPPLIEDMATERIALYARNDESCRIPTION As String = ""
                        Dim FTMANUFACTURINGCRTYOFORIGIN As String = ""
                        Dim FTPriceGroup As String = ""
                        Dim FTColorsAssigned As String = ""
                        Dim FTUOMDesc As String = ""
                        Dim FTRMDSSESNCD As String = ""
                        Dim FTSMState, FTSMStatus, pFTMatColor As String

                        Dim FTParentFactory, FTFcty, FTSupplierLocationName, FTLocatedinCountry As String

                        For Each R As DataRow In oDBdtExcel.Rows
                            RowSeq = RowSeq + 1

                            If RowSeq > 1 Then


                                FTParentFactory = ""
                                FTFcty = ""
                                FTSupplierLocationName = ""
                                FTLocatedinCountry = ""

                                IMCode = R!F5.ToString().Trim()
                                MatDes = R!F39.ToString().Trim()
                                FTSUPPLIEDMATERIALYARNDESCRIPTION = R!F40.ToString().Trim()
                                FTMANUFACTURINGCRTYOFORIGIN = R!F41.ToString().Trim()

                                SuplLocation = R!F1.ToString().Trim()

                                FTPriceGroup = R!F11.ToString().Trim()
                                FTColorsAssigned = R!F12.ToString().Trim()
                                FTUOMDesc = R!F15.ToString().Trim()

                                FTSMState = R!F6.ToString().Trim()
                                FTSMStatus = R!F7.ToString().Trim()

                                Try
                                    SuplCode = R!F2.ToString().Trim().Split("-")(0).Trim()

                                    SupName = R!F2.ToString().Trim().Split("-")(1).Trim()



                                Catch ex As Exception
                                    SuplCode = ""
                                    SupName = ""
                                End Try

                                FTSupplierLocationName = SupName

                                FTRMDSUpdated = HI.UL.ULDate.ConvertEnDB(R!F43.ToString().Trim())


                                ExcRate = Val(R!F9.ToString().Trim())

                                RMDSCur = R!F10.ToString().Trim()

                                pFTMatColor = R!F12.ToString().Trim()

                                SellingUnit = R!F13.ToString().Trim()
                                SellingQty = Val(R!F14.ToString().Trim())

                                Price = Val(R!F16.ToString().Trim())
                                PriceUsd = Val(R!F17.ToString().Trim())


                                FixQuotesPrice = Val(R!F18.ToString().Trim())
                                FixQuotesUSDPrice = Val(R!F19.ToString().Trim())
                                TotalProceQuoteCurr = Val(R!F20.ToString().Trim())
                                PriceSTDUnit = R!F21.ToString().Trim()
                                PriceSTD = Val(R!F22.ToString().Trim())

                                FNPurchasingLT = Val(R!F26.ToString().Trim())
                                FNDyeFinishPackLT = Val(R!F27.ToString().Trim())
                                FNKnitWeaveLT = Val(R!F28.ToString().Trim())
                                FNYarnLT = Val(R!F29.ToString().Trim())
                                FNSalesSampleLT = Val(R!F30.ToString().Trim())

                                FTLTAdHoc = R!F31.ToString().Trim()
                                FTLTComments = R!F32.ToString().Trim()

                                PriceSTDUnit = R!F33.ToString().Trim()
                                FNProdMinQTY = Val(R!F34.ToString().Trim())
                                FNProdMinColorQTY = Val(R!F35.ToString().Trim())

                                FNSampleMinQTY = Val(R!F36.ToString().Trim())
                                FNSampleMinColorQTY = Val(R!F37.ToString().Trim())


                                FTMATERIALTYPE = R!F38.ToString().Trim()

                                Try
                                    Season = R!F8.ToString().Trim().Split("-")(0).Trim()
                                Catch ex As Exception
                                    Season = ""
                                End Try

                                Try
                                    EffDate = R!F8.ToString().Trim().Split("-")(2).Trim()

                                    SDate = EffDate.Split(" ")(0).Trim
                                    EDate = EffDate.Split(" ")(2).Trim

                                Catch ex As Exception
                                    EffDate = ""
                                End Try


                                Try
                                    FTRMDSEFFStartDt = Microsoft.VisualBasic.Right(SDate, 4) & "/" & Microsoft.VisualBasic.Left(SDate, 2) & "/" & Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(SDate, 5), 2)
                                Catch ex As Exception
                                    FTRMDSEFFStartDt = ""
                                End Try

                                Try
                                    FTRMDSEFFEndDt = Microsoft.VisualBasic.Right(EDate, 4) & "/" & Microsoft.VisualBasic.Left(EDate, 2) & "/" & Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(EDate, 5), 2)

                                Catch ex As Exception
                                    FTRMDSEFFEndDt = ""
                                End Try

                                If IMCode = "" Then
                                    Exit For

                                Else

                                    If FTRMDSEFFStartDt <> "" And FTRMDSEFFEndDt <> "" Then

                                    End If
                                    If IMCode <> IMCodeBF Then
                                        IMCodeBF = IMCode

                                        'cmdstring = " delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile where FTMat='" & HI.UL.ULF.rpQuoted(IMCode) & "' AND FTFileDataDateRef <>'" & HI.UL.ULF.rpQuoted(FTFileDataDate) & "'  AND FTRMDSEFFStartDt='" & FTRMDSEFFStartDt & "' AND FTRMDSEFFEndDt='" & FTRMDSEFFEndDt & "'  "
                                        cmdstring = " delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile where FTMat='" & HI.UL.ULF.rpQuoted(IMCode) & "' AND FTRMDSEFFStartDt='" & FTRMDSEFFStartDt & "' AND FTRMDSEFFEndDt='" & FTRMDSEFFEndDt & "'  "
                                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                    End If

                                    Seq = Seq + 1

                                    cmdstring = " insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile (  FTFileDataDateRef, FTImportUser, FTImportDate, FTImportTime, FNDataSeq, FTMat, FTClass, FTSM, FTMaterialDescription, FTSMState, FTSMStatus"
                                    cmdstring &= vbCrLf & " , FTRMDSUpdated, FTQuotedExchangeRate, FTEuroExchangeRate, FTLiaisonOfficeCode, "
                                    cmdstring &= vbCrLf & "  FTSupplierLocationCode, FTParentFactory, FTFcty, FTSupplierLocationName, FTLocatedinCountry, FTRMDSSESNCD, FTRMDSEFFStartDt, FTRMDSEFFEndDt, FTMinStatus, FTMINUOM, FNProdMinQTY, FNProdMinColorQTY,"
                                    cmdstring &= vbCrLf & " FNSampleMinQTY, FNSampleMinColorQTY, FTLTStatus, FNPurchasingLT, FNDyeFinishPackLT, FNKnitWeaveLT, FNYarnLT, FNSalesSampleLT, FTPricingStatus, FTSellingUOM, FNQty, FTUOMDesc, FTRMDSComments,"
                                    cmdstring &= vbCrLf & " FTPriceGroup, FTColorsAssigned, FTQuotedCrncy, FNQuotedVarPRC, FNUSDVARPRC,FTRMDSCur,FNRMDSPrice, FNUSDLY, FNQuotedFixedPRC, FNUSDFixedPRC, FTSurchargeDesc, FTSurchargeOther, FNSurchargeAMT, FTSurchargeType, FNWeightGM,"
                                    cmdstring &= vbCrLf & " FNWidthCM,FNTOTALPRICEQUOTEDCURRENCY, FTPRICINGSTARDARDUOM,FTLTAdHoc,FTLTComments,FTMATERIALTYPE,FTSUPPLIEDMATERIALYARNDESCRIPTION,FTMANUFACTURINGCRTYOFORIGIN,FNSTDPrice,FTMatColor)"

                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(FTFileDataDate) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                                    cmdstring &= vbCrLf & " ," & Seq & ""
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(IMCode) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(MatDes) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMState) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMStatus) & "'"
                                    cmdstring &= vbCrLf & " ,'" & FTRMDSUpdated & "'"  'Dattetime
                                    cmdstring &= vbCrLf & " ,'" & ExcRate & "'"
                                    cmdstring &= vbCrLf & " ,'" & ExcRate & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(SuplLocation) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(SuplCode) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTParentFactory) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTFcty) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSupplierLocationName) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTLocatedinCountry) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Season) & "'"
                                    cmdstring &= vbCrLf & " ,'" & FTRMDSEFFStartDt & "'"  'Dattetime
                                    cmdstring &= vbCrLf & " ,'" & FTRMDSEFFEndDt & "'"  'Dattetime
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTMINUOM) & "'"
                                    cmdstring &= vbCrLf & " ," & Val(FNProdMinQTY) & ""
                                    cmdstring &= vbCrLf & " ," & Val(FNProdMinColorQTY) & ""
                                    cmdstring &= vbCrLf & " ," & Val(FNSampleMinQTY) & ""
                                    cmdstring &= vbCrLf & " ," & Val(FNSampleMinColorQTY) & ""
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ," & FNPurchasingLT & ""
                                    cmdstring &= vbCrLf & " ," & FNDyeFinishPackLT & ""
                                    cmdstring &= vbCrLf & " ," & FNKnitWeaveLT & ""
                                    cmdstring &= vbCrLf & " ," & FNYarnLT & ""
                                    cmdstring &= vbCrLf & " ," & FNSalesSampleLT & ""
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"

                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(SellingUnit) & "'"
                                    cmdstring &= vbCrLf & " ," & SellingQty & ""

                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTUOMDesc) & "'"


                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"


                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTPriceGroup) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTColorsAssigned) & "'"


                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(RMDSCur) & "'"


                                    cmdstring &= vbCrLf & " ," & Val("") & ""
                                    cmdstring &= vbCrLf & " ," & Val("") & ""


                                    cmdstring &= vbCrLf & " ,'" & RMDSCur & "'"
                                    cmdstring &= vbCrLf & " ," & Val(Price) & ""
                                    cmdstring &= vbCrLf & " ," & Val(PriceUsd) & ""

                                    cmdstring &= vbCrLf & " ," & Val(FixQuotesPrice) & ""
                                    cmdstring &= vbCrLf & " ," & Val(FixQuotesUsdPrice) & ""


                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ," & Val(0) & ""
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted("") & "'"
                                    cmdstring &= vbCrLf & " ," & Val("") & ""
                                    cmdstring &= vbCrLf & " ," & Val("") & ""

                                    cmdstring &= vbCrLf & " ," & TotalProceQuoteCurr & ""
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(PriceSTDUnit) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTLTAdHoc) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTLTComments) & "'"

                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTMATERIALTYPE) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSUPPLIEDMATERIALYARNDESCRIPTION) & "'"
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTMANUFACTURINGCRTYOFORIGIN) & "'"
                                    cmdstring &= vbCrLf & " ," & Val(PriceSTD) & ""
                                    cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(pFTMatColor) & "'"


                                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                End If

                            End If

                        Next

                    Catch ex As Exception
                        Dim msg As String = ex.Message
                    End Try

                    Call LoadData()

                    _Pls.Close()

                    Call MappingSuplierData(FTFileDataDate)
                End If
            Catch ex As Exception
            End Try

        Catch ex As Exception
            Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
        End Try
    End Sub

    Private Sub wImportOptiplan_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub wImportOptiplan_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub ocmdeleteoptiplan_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadData()
    End Sub

    Private Sub LoadData()

        Me.ogcDetail.DataSource = Nothing

        If FTStartDate.Text <> "" Then
            Dim cmdstring As String = ""
            cmdstring = "  Select X.*,ISNULL(SS.FTSuplCode,'') AS FTSuplCode "
            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile  AS X WITH(NOLOCK)"
            cmdstring &= vbCrLf & " OUTER APPLY (select top 1 FTSuplCode  from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileSuplMap as z with(nolock) WHERE z.FTSupplierLocationCode =X.FTSupplierLocationCode  )  AS SS "
            cmdstring &= vbCrLf & " WHERE  X.FTFileDataDateRef<>''  "

            If FTStartDate.Text <> "" Then
                cmdstring &= vbCrLf & " AND  X.FTRMDSEFFStartDt <='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' AND  X.FTRMDSEFFEndDt >='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "'  "

            End If

            Dim dt As DataTable
            dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogcDetail.DataSource = dt.Copy
            dt.Dispose()
        End If

    End Sub

    Private Sub ocmmappingsupl_Click(sender As Object, e As EventArgs) Handles ocmmappingsupl.Click
        Call MappingSuplierData()
    End Sub

    Private Sub MappingSuplierData(Optional FileRef As String = "")

        Dim cmdstring As String = ""
        Dim dt As New DataTable
        Dim dtunit As New DataTable

        cmdstring = "  Select  A.FTSupplierLocationCode,MAX(A.FTSupplierLocationName) AS FTSupplierLocationName "
        cmdstring &= vbCrLf & " 	,MAX( ISNULL(S.FTSuplCode,'')) AS FTSuplCode"
        cmdstring &= vbCrLf & " , MAX( ISNULL(S.FTSuplNameEN,''))  AS FTSuplName"
        cmdstring &= vbCrLf & " , MAX( ISNULL(S.FNHSysSuplId,0))  As FNHSysSuplId"
        cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) RIGHT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileSuplMap AS B WITH(NOLOCK)  ON S.FNHSysSuplId = B.FNHSysSuplId RIGHT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile AS A WITH(NOLOCK)  ON B.FTSupplierLocationCode = A.FTSupplierLocationCode"

        If FileRef <> "" Then
            cmdstring &= vbCrLf & " WHERE A.FTFileDataDateRef='" & HI.UL.ULF.rpQuoted(FileRef) & "' "
            cmdstring &= vbCrLf & " AND B.FTSupplierLocationCode IS NULL "
        End If

        cmdstring &= vbCrLf & " Group By A.FTSupplierLocationCode"
        cmdstring &= vbCrLf & "   Order By A.FTSupplierLocationCode"

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        cmdstring = "  Select  A.FTSellingUOM,A.FNQty,A.FTUOMDesc "
        cmdstring &= vbCrLf & " 	,MAX( ISNULL(S.FTUnitCode,'')) AS FTUnitCode"
        cmdstring &= vbCrLf & " , MAX( ISNULL(S.FTUnitNameEN,''))  AS FTUnitName"
        cmdstring &= vbCrLf & " , MAX( ISNULL(S.FNHSysUnitId,0))  As FNHSysUnitId"
        cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS S WITH(NOLOCK) RIGHT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFileUnitMap AS B WITH(NOLOCK)  ON S.FNHSysUnitId = B.FNHSysUnitId RIGHT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile AS A WITH(NOLOCK)  ON B.FTSellingUOM = A.FTSellingUOM AND B.FNQty = A.FNQty AND B.FTUOMDesc = A.FTUOMDesc"

        If FileRef <> "" Then
            cmdstring &= vbCrLf & " WHERE A.FTFileDataDateRef='" & HI.UL.ULF.rpQuoted(FileRef) & "' "
            cmdstring &= vbCrLf & " AND B.FTSellingUOM IS NULL "
        End If

        cmdstring &= vbCrLf & " Group By A.FTSellingUOM,A.FNQty,A.FTUOMDesc"
        cmdstring &= vbCrLf & "   Order By A.FTSellingUOM,A.FNQty,A.FTUOMDesc"
        dtunit = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


        If dt.Rows.Count > 0 Or dtunit.Rows.Count > 0 Then

            With MappIngSupl

                .LoadSuplier()
                .LoadUnit()
                .ogclist.DataSource = dt.Copy()
                .ogclistunit.DataSource = dtunit.Copy
                .ShowDialog()

            End With

        End If

    End Sub

End Class