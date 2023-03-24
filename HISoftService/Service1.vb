'Option Explicit On

Imports System.ServiceProcess

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils

Imports System.Data
Imports System.Data.SqlClient
Imports HI.TL.RunID

Imports System.IO


Public Class Service1

    ' Inherits System.ServiceProcess.ServiceBase
    Dim Mythread As Threading.Thread
    Friend Shared _CountApp As Integer = 0

    Friend Shared DTPurchaseNo As DataTable
    Friend Property Data_DTPurchaseNo As DataTable
        Get
            Return DTPurchaseNo
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNo = value
        End Set
    End Property


    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ก่อน OnStart")

        Call WriteLog(Date.Now.ToLongTimeString & "  ก่อน OnStart")


        Mythread = New Threading.Thread(AddressOf ValidateApp)

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ก่อน ValidateApp")

        Call WriteLog(Date.Now.ToLongTimeString & "  ก่อน ValidateApp")

        Mythread.Start()

        Call WriteLog(Date.Now.ToLongTimeString & "  หลัง ValidateApp")


        'System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  หลัง ValidateApp")

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Mythread.Abort()
    End Sub
    'Protected Sub KeepCounting()
    '    Dim I As Integer = 0
    '    Do While True
    '        I = I + 1
    '        System.IO.File.WriteAllText("E:\text.txt", I)
    '        Threading.Thread.Sleep(1000)
    '    Loop
    'End Sub


    'Friend Function Update_ManagerApproved(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

    '    Dim _Str As String = String.Empty
    '    Dim _FTMailId As Integer

    '    Try


    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



    '        For i = 0 To TempGrid.RowCount - 1

    '            If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
    '                _Str = ""
    '                _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
    '                _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
    '                _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
    '                _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If


    '                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 0

    '                _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                _Str = ""
    '                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
    '                _Str &= ",'Approved  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "  << Converter File to PDF >> ' ,0,1,0,0,0,0,"
    '                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If



    '                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

    '                _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                _Str = ""
    '                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
    '                _Str &= ",'Approved  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "  << Converter File to PDF >> ' ,0,1,0,0,0,0,"
    '                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '                ' กรณีส่งหาตัวเอง
    '                'If TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString().Trim = HI.ST.UserInfo.UserName Then
    '                '    _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                '    _Str = ""
    '                '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '                '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '                '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
    '                '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString() & "'"
    '                '    _Str &= ",'Approved  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "  << Converter File to PDF >>' ,0,0,1,0,0,0,"
    '                '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

    '                '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                '        HI.Conn.SQLConn.Tran.Rollback()
    '                '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                '        Return False
    '                '    End If

    '                'End If


    '            End If
    '        Next


    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function

    'Friend Function Update_ManagerApproved(ByVal TempGrid As GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

    '    Dim _Str As String = String.Empty
    '    Dim _FTMailId As Integer

    '    Try

    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        For i = 0 To TempGrid.RowCount - 1

    '            If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
    '                _Str = ""
    '                _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
    '                ' _Str &= Environment.NewLine & ", [FTRemark] = '" & TempRemark & "'"
    '                _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
    '                _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
    '                _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '                ' ส่งเมลกลับกรณี Reject   FNMailStateType = 0

    '                _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                _Str = ""
    '                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
    '                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
    '                _Str &= ",'Reject  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
    '                _Str &= "'" & TempRemark & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If


    '                ' ส่งเมลกลับกรณี Reject  FNMailStateType =1

    '                _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                _Str = ""
    '                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
    '                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
    '                _Str &= ",'Reject  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
    '                _Str &= "'" & TempRemark & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '                ' กรณีส่ง Mail ให้ตัวเอง
    '                'If TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString().Trim = HI.ST.UserInfo.UserName Then
    '                '    _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                '    _Str = ""
    '                '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '                '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '                '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
    '                '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString() & "'"
    '                '    _Str &= ",'Reject  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "' ,1,0,0,0,0,0,"
    '                '    _Str &= "'" & TempRemark & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

    '                '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                '        HI.Conn.SQLConn.Tran.Rollback()
    '                '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                '        Return False
    '                '    End If


    '                'End If

    '            End If

    '        Next

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try

    'End Function


    Public Shared Function LoadogcTPURTPurchase() As DataTable
        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            'ระดับ Manager

            _str = ""
            _str = "SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPurchaseDate,1,4) as FDPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPurchaseBy,'') as FTPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTSuperVisorName,'') as FTSuperVisorName, "
            _str &= Environment.NewLine & " isnull(A.FTPurchaseState,'') as FTPurchaseState,"
            _str &= Environment.NewLine & " ISNULL( l2.FTCmpRunCode,'') as FTCmpRunCode,"
            _str &= Environment.NewLine & " L3.FTSuplCode,"
            _str &= Environment.NewLine & " SUBSTRING(A.FDDeliveryDate,9,2) + '/'+ SUBSTRING(A.FDDeliveryDate,6,2) + '/' + SUBSTRING(A.FDDeliveryDate,1,4) as FDDeliveryDate,"
            _str &= Environment.NewLine & " L4.FTCrTermCode,"
            _str &= Environment.NewLine & " ISNULL( A.FNCreditDay,0) as FNCreditDay,"
            _str &= Environment.NewLine & " l5.FTTermOfPMCode,"
            _str &= Environment.NewLine & " A.FNHSysCurId,ISNULL(A.FNExchangeRate,0) as FNExchangeRate,"
            _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"
            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.Lang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
                Case HI.ST.Lang.Lang.TH
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameTH,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameTH,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameTH,'') as FTPurGrpName"
                Case Else
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
            End Select

            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName INNER JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysDeliveryId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "
            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0')"
            _str &= Environment.NewLine & " Order by a.FDPurchaseDate"

            '  _dt.Columns.Add("FTImageStatus", GetType(Object))
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)


            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            ' MsgBox(ex.Message)
        End Try

    End Function


    Private Shared Function FindComputerName(ByVal TempComName As String) As Boolean
        Dim _str As String = String.Empty
        Dim _dt As New DataTable

        Try
            _str = "SELECT  isnull(FTComputerName,'') as FTComputerName,isnull(FTUserName,'') as FTUserName  "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECinfigDirector  WITH(NOLOCK) "
            _str &= Environment.NewLine & " WHERE FTComputerName = '" & TempComName & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_SECURITY)

            ' MsgBox(" Director = " & _dt.Rows.Count)

            System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  เข้า FindComputerName มี =  " & _dt.Rows.Count)

            If _dt.Rows.Count > 0 Then
                HI.ST.UserInfo.UserName = _dt.Rows(0)!FTUserName.ToString
                Return True
            Else
                HI.ST.UserInfo.UserName = ""
                Return False
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try


    End Function


    Public Shared Sub WriteLog(ByVal Str As String)


        Try
            Dim MsgStr As String = Date.Now.ToString & " -- " & Str

            Dim fs1 As FileStream = New FileStream("E:\" & Now.Date.ToString("ddMMyyyy") & ".log", FileMode.Append, FileAccess.Write)
            Dim s1 As StreamWriter = New StreamWriter(fs1)
            s1.WriteLine(MsgStr)
            s1.Close()
            fs1.Close()
        Catch ex As Exception

        End Try
    End Sub




    Private Shared _frmApp As wDirectorApproved = Nothing

    Public Shared Sub ValidateApp()

        'System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน ValidateApp")

        Call WriteLog(Date.Now.ToLongTimeString & "  ผ่าน ValidateApp")


        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager


        '----------- Read Connecttion String From File XML
        HI.Conn.DB.GetXmlConnectionString()

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน HI.Conn.DB.GetXmlConnectionString")

        Call WriteLog(Date.Now.ToLongTimeString & "  ผ่าน HI.Conn.DB.GetXmlConnectionString")

        'MsgBox(" ผ่าน HI.Conn.DB.GetXmlConnectionString")

        ' Dim a As String = Environment.MachineName.ToString()  ชื่อเครื่อง Computer
        ' Environment.MachineName.ToString()


        If FindComputerName(Environment.MachineName.ToString()) Then

            'MsgBox("Computer Name = " & Environment.MachineName.ToString())

            ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  Computer Name = " & Environment.MachineName.ToString())

            Call WriteLog(Date.Now.ToLongTimeString & "  Computer Name = " & Environment.MachineName.ToString())

            DTPurchaseNo = Nothing
            DTPurchaseNo = LoadogcTPURTPurchase()

            If _CountApp > 0 Then

                'MsgBox("LoadogcTPURTPurchase = " & _CountApp)
                ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  LoadogcTPURTPurchase = " & _CountApp)

                Call WriteLog(Date.Now.ToLongTimeString & "  LoadogcTPURTPurchase = " & _CountApp)

                If _frmApp Is Nothing Then
                    _frmApp = New wDirectorApproved
                ElseIf _frmApp.IsDisposed Then
                    _frmApp = New wDirectorApproved
                End If


                HI.TL.HandlerControl.AddHandlerObj(_frmApp)

                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmApp.Name.ToString.Trim, _frmApp)

                    ' MsgBox(" ผ่าน oSysLang.LoadObjectLanguage")

                Catch ex As Exception
                Finally
                End Try

                Try
                    _frmApp.Show()
                    Call WriteLog(Date.Now.ToLongTimeString & "  _frmApp.Show()")
                    _frmApp.BringToFront()
                Catch ex As Exception
                End Try

            End If

        End If

    End Sub


End Class
