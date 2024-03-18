Imports System.IO
Imports System.Windows.Forms

Public Class wImportTimeCardFromSQL

#Region "Procedure"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'If HI.ST.SysInfo.Admin Then
        '    Me.Height = 372
        'Else
        '    Me.Height = 235
        'End If

    End Sub

    Private Sub LoadData()
        Dim cmd As String = ""
        Dim dttime As DataTable

        cmd = "  Select  Convert(Datetime,FTDate) AS  FTDate,sn"
        cmd &= vbCrLf & "   FROM ( "
        cmd &= vbCrLf & "  Select  B.BADGENUMBER"
        cmd &= vbCrLf & "   ,A.sn "
        cmd &= vbCrLf & "  ,Convert(nvarchar(10),CASE WHEN Convert(nvarchar(5),A.CHECKTIME,108) >='00:00' AND Convert(nvarchar(5),A.CHECKTIME,108) <='00:05' THEN  DATEADD(day,-1,A.CHECKTIME) ELSE A.CHECKTIME END ,111) AS FTDate"
        cmd &= vbCrLf & "  ,Convert(nvarchar(5),A.CHECKTIME,108) As FTTime	"
        cmd &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.CHECKINOUT AS A WITH(NOLOCK) INNER Join"
        cmd &= vbCrLf & "       " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.USERINFO As B WITH(NOLOCK)  On A.USERID = B.USERID"
        cmd &= vbCrLf & "  Where ISNULL(FTStateImport,'0') ='0'"
        cmd &= vbCrLf & "  GROUP BY  B.BADGENUMBER	  "
        cmd &= vbCrLf & "    ,A.sn "
        cmd &= vbCrLf & "   ,Convert(nvarchar(10),Case When Convert(nvarchar(5),A.CHECKTIME,108) >='00:00' AND Convert(nvarchar(5),A.CHECKTIME,108) <='00:05' THEN  DATEADD(day,-1,A.CHECKTIME) ELSE A.CHECKTIME END ,111)"
        cmd &= vbCrLf & "   ,Convert(nvarchar(5),A.CHECKTIME,108) ) AS A"
        cmd &= vbCrLf & "  GROUP BY Convert(Datetime,FTDate),sn "

        dttime = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_TIME)
        Me.ogc.DataSource = dttime.Copy()
        dttime.Dispose()

    End Sub
    Private Sub ReadDataText()
        Dim _Qry As String
        Dim cmd As String
        Dim _Spls As New HI.TL.SplashScreen("Reading time Card...   Please Wait   ", "Processing. Please Wait...")

        Try

            cmd = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTempTimeFinger WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmd &= vbCrLf & "   INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTempTimeFinger(FTUserLogin,[USERID] ,"
            cmd &= vbCrLf & "     [CHECKTIME] ,"
            cmd &= vbCrLf & "     [CHECKTYPE],"
            cmd &= vbCrLf & "     [VERIFYCODE],"
            cmd &= vbCrLf & "     [SENSORID],"
            cmd &= vbCrLf & "    [Memoinfo],"
            cmd &= vbCrLf & "    [WorkCode],"
            cmd &= vbCrLf & "    [sn],"
            cmd &= vbCrLf & "    [UserExtFmt],"
            cmd &= vbCrLf & "    [FDCreateDate],"
            cmd &= vbCrLf & "    [FTStateImport],"
            cmd &= vbCrLf & "    [FTImportBy],"
            cmd &= vbCrLf & "    [FTImportDate],"
            cmd &= vbCrLf & "    [FTImportTime] )"
            cmd &= vbCrLf & "   Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', [USERID] ,"
            cmd &= vbCrLf & "    [CHECKTIME] ,"
            cmd &= vbCrLf & "    [CHECKTYPE],"
            cmd &= vbCrLf & "    [VERIFYCODE],"
            cmd &= vbCrLf & "    [SENSORID],"
            cmd &= vbCrLf & "    [Memoinfo],"
            cmd &= vbCrLf & "    [WorkCode],"
            cmd &= vbCrLf & "     [sn],"
            cmd &= vbCrLf & "    [UserExtFmt],"
            cmd &= vbCrLf & "    [FDCreateDate],"
            cmd &= vbCrLf & "    [FTStateImport],"
            cmd &= vbCrLf & "    [FTImportBy],"
            cmd &= vbCrLf & "    [FTImportDate],"
            cmd &= vbCrLf & "   [FTImportTime] "
            cmd &= vbCrLf & "   From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.CHECKINOUT As A With(NOLOCK)"
            cmd &= vbCrLf & "   Where ISNULL(FTStateImport,'0') ='0' "

            cmd &= vbCrLf & "   DELETE  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTimecard WHERE FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'   "

            cmd &= vbCrLf & "   INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTimecard (FNHSysCmpId,FTInsUser, FTInsDate, FTInsTime "
            cmd &= vbCrLf & " ,FTUpdUser,FTUpdDate,FTUpdTime "
            cmd &= vbCrLf & " ,FTDateTrans,FTTimeTrans,FTEmpBarcode,FTIdMac,FTDateScan) "
            cmd &= vbCrLf & "  Select " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmd &= vbCrLf & " ,Convert(varchar(10),Getdate(),111) "
            cmd &= vbCrLf & " ,Convert(varchar(8),Getdate(),114) "
            cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmd &= vbCrLf & " ,Convert(varchar(10),Getdate(),111) "
            cmd &= vbCrLf & " ,Convert(varchar(8),Getdate(),114) "
            cmd &= vbCrLf & " ,Convert(nvarchar(10),Case When Convert(nvarchar(5),A.CHECKTIME,108) >='00:00' AND Convert(nvarchar(5),A.CHECKTIME,108) <='05:00' THEN  DATEADD(day,-1,A.CHECKTIME) ELSE A.CHECKTIME END ,111) AS FTDate "
            cmd &= vbCrLf & " ,Replace(Convert(nvarchar(5),A.CHECKTIME,108),':','') As FTTime  "


            If (HI.ST.SysInfo.CmpID = 2015760004) Then ' empbarcode  เพิ่มตัวอักษร นำหน้า
                cmd &= vbCrLf & " ,  B.SSN  "
                cmd &= vbCrLf & " ,   A.sn ,Convert(varchar(10),A.CHECKTIME,111)  "
                cmd &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTempTimeFinger As A WITH(NOLOCK)  INNER Join  "
                cmd &= vbCrLf & "       " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.USERINFO As B With(NOLOCK) On A.USERID = B.USERID "
                cmd &= vbCrLf & " WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmd &= vbCrLf & "   GROUP BY  B.SSN	 "
            Else
                cmd &= vbCrLf & " ,  B.BADGENUMBER  "
                cmd &= vbCrLf & " ,   A.sn ,Convert(varchar(10),A.CHECKTIME,111)  "
                cmd &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTempTimeFinger As A WITH(NOLOCK)  INNER Join  "
                cmd &= vbCrLf & "       " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.USERINFO As B With(NOLOCK) On A.USERID = B.USERID "
                cmd &= vbCrLf & " WHERE A.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmd &= vbCrLf & "   GROUP BY  B.BADGENUMBER	 "

            End If





            cmd &= vbCrLf & " ,  A.sn  "
            cmd &= vbCrLf & " ,Convert(nvarchar(10),Case When Convert(nvarchar(5),A.CHECKTIME,108) >='00:00' AND Convert(nvarchar(5),A.CHECKTIME,108) <='05:00' THEN  DATEADD(day,-1,A.CHECKTIME) ELSE A.CHECKTIME END ,111) "
            cmd &= vbCrLf & "  ,Convert(nvarchar(5),A.CHECKTIME,108),Convert(varchar(10),A.CHECKTIME,111)  "
            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_HR)

            Dim oDBdt As DataTable

            '_Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..SP_TRANSFER_DATA_TIMECARD_FROMSQL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            'oDBdt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _toatlLine As Integer
            Dim _Rec As Integer


            Dim StrCloseDate As String = ""
            Dim _StrAllDate As String = ""

            _Qry = "SELECt DISTINCT FTDateTrans "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.ST.UserInfo.UserName & "' "
            _Qry &= vbCrLf & "ORDER BY FTDateTrans"


            oDBdt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            _toatlLine = oDBdt.Rows.Count
            _Rec = 0

            For Each Row As DataRow In oDBdt.Rows
                _Rec = _Rec + 1

                'ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ 
                If HI.HRCAL.Time.CheckClosePeriod(Row!FTDateTrans.ToString) = False Then
                    HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, Row!FTDateTrans.ToString, 0, _Spls)
                Else

                    StrCloseDate &= vbCrLf & "" & HI.UL.ULDate.ConvertEN(Row!FTDateTrans.ToString)

                    Beep()
                End If

                _Qry = "  	 INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardHistory( FNHSysCmpId,FTInsUser, FTInsDate, FTInsTime,"
                _Qry &= vbCrLf & " 	 FTDateTrans, FTTimeTrans, FTEmpBarcode, FTIdMac, FTCmpCode)"
                _Qry &= vbCrLf & "  SELECT 	A.FNHSysCmpId,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',CONVERT(varchar(10),GETDATE(),111),CONVERT(varchar(8),GETDATE(),114)	"
                _Qry &= vbCrLf & " ,A.FTDateScan,A.FTTimeTrans,	A.FTEmpBarcode,A.FTIdMac,A.FTCmpCode	 "
                _Qry &= vbCrLf & "  FROM 	[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " LEFT OUTER JOIN ("
                _Qry &= vbCrLf & " SELECT FNHSysCmpId,FTDateTrans , FTTimeTrans , FTEmpBarcode "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardHistory WITH(NOLOCK)"
                _Qry &= vbCrLf & "   WHERE (FTDateTrans ='" & HI.UL.ULDate.ConvertEnDB(Row!FTDateTrans.ToString) & "') AND FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ""
                _Qry &= vbCrLf & "  ) AS BBX ON  A.FTDateTrans = BBX.FTDateTrans AND A.FTTimeTrans = BBX.FTTimeTrans AND A.FTEmpBarcode =BBX.FTEmpBarcode AND A.FNHSysCmpId=BBX.FNHSysCmpId "
                _Qry &= vbCrLf & " WHERE A.FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " AND  A.FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(Row!FTDateTrans.ToString) & "' "
                _Qry &= vbCrLf & " AND BBX.FTEmpBarcode IS NULL "
                _Qry &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard "
                _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.ST.UserInfo.UserName & "' "
                _Qry &= vbCrLf & " AND  FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(Row!FTDateTrans.ToString) & "' "

                _Qry &= vbCrLf & "  Update  A SET FTStateImport='1' "
                _Qry &= vbCrLf & ",FTImportBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & ",FTImportDate=Convert(varchar(10),Getdate(),111) "
                _Qry &= vbCrLf & ",FTImportTime=Convert(varchar(8),Getdate(),114) "
                _Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.CHECKINOUT AS A INNER Join "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTempTimeFinger  As B On A.[USERID]=B.[USERID]  "
                _Qry &= vbCrLf & "   And A.[CHECKTIME]=B.[CHECKTIME] "
                _Qry &= vbCrLf & " Where ISNULL(A.FTStateImport,'0') ='0' "
                _Qry &= vbCrLf & " AND B.FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                If _StrAllDate = "" Then
                    _StrAllDate = Row!FTDateTrans.ToString
                Else
                    _StrAllDate = _StrAllDate & " ','" & Row!FTDateTrans.ToString
                End If
            Next



            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecard "
            _Qry &= vbCrLf & " WHERE FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTempTimeFinger "
            _Qry &= vbCrLf & " WHERE FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)



            _Spls.Close()

            Dim Msg As String = "Import data Complete "

            MessageBox.Show(Msg)

            If StrCloseDate <> "" Then

                Msg = "Found Close Period Can't Import Data"
                Msg &= vbCrLf & "" & StrCloseDate

                MessageBox.Show(Msg)

            End If

            '_Qry = " UPDATE  HITECH_TIME..CHECKINOUT SET FTStateImport='1'"
            '_Qry &= vbCrLf & ",FTImportBy='" & HI.ST.UserInfo.UserName & "'"
            '_Qry &= vbCrLf & ",FTImportDate=" & HI.UL.ULDate.FormatDateDB & ""
            '_Qry &= vbCrLf & ",FTImportTime=" & HI.UL.ULDate.FormatTimeDB & ""
            '_Qry &= vbCrLf & " Where ISNULL(FTStateImport,'0') ='0' "

            'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
            _Spls.Close()

            Exit Sub
        End Try
    End Sub

    Private Function GetTimeFromFingerScan_BK() As DataTable
        Dim dt As New DataTable
        Dim cmd As String



        cmd = " CREATE TABLE #TabData ( "
        cmd &= vbCrLf & "   [USERID] [Int] Not NULL, "
        cmd &= vbCrLf & "     [CHECKTIME] [DateTime] Not NULL, "
        cmd &= vbCrLf & "      [CHECKTYPE] [varchar](1) NULL, "
        cmd &= vbCrLf & "      [VERIFYCODE] [Int] NULL, "
        cmd &= vbCrLf & "      [SENSORID] [varchar](5) NULL, "
        cmd &= vbCrLf & "     [Memoinfo] [varchar](30) NULL, "
        cmd &= vbCrLf & "      [WorkCode] [varchar](24) NULL, "
        cmd &= vbCrLf & "      [sn] [varchar](20) NULL, "
        cmd &= vbCrLf & "      [UserExtFmt] [smallint] NULL, "
        cmd &= vbCrLf & "      [FDCreateDate] [DateTime] NULL, "
        cmd &= vbCrLf & "      [FTStateImport] [varchar](1) NULL,"
        cmd &= vbCrLf & "     [FTImportBy] [varchar](50) NULL,"
        cmd &= vbCrLf & "     [FTImportDate] [varchar](10) NULL,"
        cmd &= vbCrLf & "    [FTImportTime] [varchar](10) NULL		"
        cmd &= vbCrLf & "  	)CREATE INDEX [IDX_TabData] ON #TabData(USERID,[CHECKTIME])"
        cmd &= vbCrLf & "   INSERT INTO #TabData([USERID] ,"
        cmd &= vbCrLf & "     [CHECKTIME] ,"
        cmd &= vbCrLf & "     [CHECKTYPE],"
        cmd &= vbCrLf & "     [VERIFYCODE],"
        cmd &= vbCrLf & "     [SENSORID],"
        cmd &= vbCrLf & "    [Memoinfo],"
        cmd &= vbCrLf & "    [WorkCode],"
        cmd &= vbCrLf & "    [sn],"
        cmd &= vbCrLf & "    [UserExtFmt],"
        cmd &= vbCrLf & "    [FDCreateDate],"
        cmd &= vbCrLf & "    [FTStateImport],"
        cmd &= vbCrLf & "    [FTImportBy],"
        cmd &= vbCrLf & "    [FTImportDate],"
        cmd &= vbCrLf & "    [FTImportTime] )"
        cmd &= vbCrLf & "   Select  [USERID] ,"
        cmd &= vbCrLf & "    [CHECKTIME] ,"
        cmd &= vbCrLf & "    [CHECKTYPE],"
        cmd &= vbCrLf & "    [VERIFYCODE],"
        cmd &= vbCrLf & "    [SENSORID],"
        cmd &= vbCrLf & "    [Memoinfo],"
        cmd &= vbCrLf & "    [WorkCode],"
        cmd &= vbCrLf & "     [sn],"
        cmd &= vbCrLf & "    [UserExtFmt],"
        cmd &= vbCrLf & "    [FDCreateDate],"
        cmd &= vbCrLf & "    [FTStateImport],"
        cmd &= vbCrLf & "    [FTImportBy],"
        cmd &= vbCrLf & "    [FTImportDate],"
        cmd &= vbCrLf & "   [FTImportTime] "
        cmd &= vbCrLf & "   From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.CHECKINOUT As A With(NOLOCK)"
        cmd &= vbCrLf & "   Where ISNULL(FTStateImport,'0') ='0' "

        cmd &= vbCrLf & "   DELETE  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTimecard WHERE FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'   "

        cmd &= vbCrLf & "   INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTTimecard (FNHSysCmpId,FTInsUser, FTInsDate, FTInsTime "
        cmd &= vbCrLf & " ,FTUpdUser,FTUpdDate,FTUpdTime "
        cmd &= vbCrLf & " ,FTDateTrans,FTTimeTrans,FTEmpBarcode,FTIdMac,FTDateScan) "
        cmd &= vbCrLf & "  Select " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        cmd &= vbCrLf & " ,Convert(varchar(10),Getdate(),111) "
        cmd &= vbCrLf & " ,Convert(varchar(8),Getdate(),114) "
        cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        cmd &= vbCrLf & " ,Convert(varchar(10),Getdate(),111) "
        cmd &= vbCrLf & " ,Convert(varchar(8),Getdate(),114) "
        cmd &= vbCrLf & " ,Convert(nvarchar(10),Case When Convert(nvarchar(5),A.CHECKTIME,108) >='00:00' AND Convert(nvarchar(5),A.CHECKTIME,108) <='00:05' THEN  DATEADD(day,-1,A.CHECKTIME) ELSE A.CHECKTIME END ,111) AS FTDate "
        cmd &= vbCrLf & " ,Replace(Convert(nvarchar(5),A.CHECKTIME,108),':','') As FTTime  "
        cmd &= vbCrLf & " ,  B.BADGENUMBER  "
        cmd &= vbCrLf & " ,   A.sn ,Convert(varchar(10),A.CHECKTIME,111)  "
        cmd &= vbCrLf & "  From #TabData As A  INNER Join  "
        cmd &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_TIME) & ".dbo.USERINFO As B With(NOLOCK) On A.USERID = B.USERID "
        cmd &= vbCrLf & "   GROUP BY  B.BADGENUMBER	 "
        cmd &= vbCrLf & " ,  A.sn  "
        cmd &= vbCrLf & " ,Convert(nvarchar(10),Case When Convert(nvarchar(5),A.CHECKTIME,108) >='00:00' AND Convert(nvarchar(5),A.CHECKTIME,108) <='00:05' THEN  DATEADD(day,-1,A.CHECKTIME) ELSE A.CHECKTIME END ,111) "
        cmd &= vbCrLf & "  ,Convert(nvarchar(5),A.CHECKTIME,108),Convert(varchar(10),A.CHECKTIME,111)  "


        Return dt
    End Function

#End Region

#Region "General"
    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ReadTextFile_Click(sender As System.Object, e As System.EventArgs) Handles ocmimport.Click

        If ogc.DataSource Is Nothing Then
            Call LoadData()
            Exit Sub
        End If

        With CType(ogc.DataSource, DataTable)
            .AcceptChanges()

            If .Select("sn<>''").Length > 0 Then

                Call ReadDataText()
                Call LoadData()

            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลสำหรับ Import", 1703270546, Me.Text,, MessageBoxIcon.Warning)
                Call LoadData()
            End If

        End With

    End Sub

    Private Sub wReadDataTimeCardFromFile_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call LoadData()
        ogv.OptionsView.ShowAutoFilterRow = False

    End Sub
#End Region

End Class