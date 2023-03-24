Option Explicit On

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports System.Data
Imports System.Data.SqlClient
Imports HI.TL.RunID



Public Class ClsService


#Region "ตัวแปร"

    Friend Shared _CountApp As Integer = 0
    Friend Shared _CountAppSMP As Integer = 0
    Friend Shared _CountAppSMPMGR As Integer = 0
    Friend Shared _CountAppDirector As Integer = 0
    Friend Shared _CountAppFacManager As Integer = 0
    Friend Shared _CountAppFacManagerCM As Integer = 0
    Friend Shared _CountAppMerManager As Integer = 0
    Friend Shared _CountAppRDSam As Integer = 0


    Friend Shared _CountAppAsset As Integer = 0
    Friend Shared _CountAppAssetPR As Integer = 0
    Friend Shared _CountAppAssetPRSa As Integer = 0
    Friend Shared _CountAppDirectorAsset As Integer = 0
    Friend Shared _CountAppDirectorAssetPR As Integer = 0


    Private DTFac As DataTable
    Private DTFacManager As DataTable
    Private DTFacManagerCM As DataTable
    Private DTMerManager As DataTable

#End Region

#Region " Property "

    Friend Shared DTPurchaseNo As DataTable
    Friend Property Data_DTPurchaseNo As DataTable
        Get
            Return DTPurchaseNo
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNo = value
        End Set
    End Property

    Friend Shared DTPRPurchaseNo As DataTable
    Friend Property Data_DTPRPurchaseNo As DataTable
        Get
            Return DTPRPurchaseNo
        End Get
        Set(ByVal value As DataTable)
            DTPRPurchaseNo = value
        End Set
    End Property


    Friend Shared DTSMP As DataTable
    Friend Property Data_DTSMP As DataTable
        Get
            Return DTSMP
        End Get
        Set(ByVal value As DataTable)
            DTSMP = value
        End Set
    End Property


    Friend Shared DTSMPMGR As DataTable
    Friend Property Data_DTSMPMGR As DataTable
        Get
            Return DTSMPMGR
        End Get
        Set(ByVal value As DataTable)
            DTSMPMGR = value
        End Set
    End Property


    Friend Shared DTAPPRDSAM As DataTable
    Friend Property Data_DTAPPRDSAM As DataTable
        Get
            Return DTAPPRDSAM
        End Get
        Set(ByVal value As DataTable)
            DTAPPRDSAM = value
        End Set
    End Property


    Friend Shared DTPurchaseAssetNo As DataTable
    Friend Property Data_DTPurchaseAssetNo As DataTable
        Get
            Return DTPurchaseAssetNo
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNo = value
        End Set
    End Property

    Friend Shared DTPurchaseNoDirector As DataTable
    Friend Property Data_DTPurchaseNoDirector As DataTable
        Get
            Return DTPurchaseNoDirector
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNoDirector = value
        End Set
    End Property

    Friend Shared DTAppFactory As DataTable
    Friend Property Data_DTAppFactory As DataTable
        Get
            Return DTAppFactory
        End Get
        Set(ByVal value As DataTable)
            DTAppFactory = value
        End Set
    End Property

    Friend Shared DTAppFactoryManager As DataTable
    Friend Property Data_DTAppFactoryManager As DataTable
        Get
            Return DTAppFactoryManager
        End Get
        Set(ByVal value As DataTable)
            DTAppFactoryManager = value
        End Set
    End Property

    Friend Shared DTAppFactoryManagerCM As DataTable
    Friend Property Data_DTAppFactoryManagerCM As DataTable
        Get
            Return DTAppFactoryManagerCM
        End Get
        Set(ByVal value As DataTable)
            DTAppFactoryManagerCM = value
        End Set
    End Property

    Friend Shared DTMerMangener As DataTable
    Friend Property Data_DTMerMangener As DataTable
        Get
            Return DTMerMangener
        End Get
        Set(ByVal value As DataTable)
            DTMerMangener = value
        End Set
    End Property
    Private Shared _StateShow As Boolean = False
    Public Shared Property StateShow As Boolean
        Get
            Return _StateShow
        End Get
        Set(value As Boolean)
            _StateShow = value
        End Set
    End Property


    Private Shared _StateShowSMP As Boolean = False
    Public Shared Property StateShowSMP As Boolean
        Get
            Return _StateShowSMP
        End Get
        Set(value As Boolean)
            _StateShowSMP = value
        End Set
    End Property


    Private Shared _StateShowSMPMGR As Boolean = False
    Public Shared Property StateShowSMPMGR As Boolean
        Get
            Return _StateShowSMPMGR
        End Get
        Set(value As Boolean)
            _StateShowSMPMGR = value
        End Set
    End Property


    Private Shared _StateShowRDSam As Boolean = False
    Public Shared Property StateShowRDSam As Boolean
        Get
            Return _StateShowRDSam
        End Get
        Set(value As Boolean)
            _StateShowRDSam = value
        End Set
    End Property

    Private Shared _StateDirectorShow As Boolean = False
    Public Shared Property StateDirectorShow As Boolean
        Get
            Return _StateDirectorShow
        End Get
        Set(value As Boolean)
            _StateDirectorShow = value
        End Set
    End Property

    Private Shared _StateDocument As Boolean = False
    Public Shared Property StateDocument As Boolean
        Get
            Return _StateDocument
        End Get
        Set(value As Boolean)
            _StateDocument = value
        End Set
    End Property

    Private Shared _StateFactoryManagerShow As Boolean = False
    Public Shared Property StateFactoryManagerShow As Boolean
        Get
            Return _StateFactoryManagerShow
        End Get
        Set(value As Boolean)
            _StateFactoryManagerShow = value
        End Set
    End Property

    Private Shared _StateLineLeaderShow As Boolean = False
    Public Shared Property StateLineLeaderShow As Boolean
        Get
            Return _StateLineLeaderShow
        End Get
        Set(value As Boolean)
            _StateLineLeaderShow = value
        End Set
    End Property

    Private Shared _StateQAFLeaderShow As Boolean = False
    Public Shared Property StateQAFLineLeaderShow As Boolean
        Get
            Return _StateQAFLeaderShow
        End Get
        Set(value As Boolean)
            _StateQAFLeaderShow = value
        End Set
    End Property

    Private Shared _StateWHCM As Boolean = False
    Public Shared Property StateWHCM As Boolean
        Get
            Return _StateWHCM
        End Get
        Set(value As Boolean)
            _StateWHCM = value
        End Set
    End Property

    Private Shared _StateMerChandiseManegerShow As Boolean = False
    Public Shared Property StateMerChandiseManegerShow As Boolean
        Get
            Return _StateMerChandiseManegerShow
        End Get
        Set(value As Boolean)
            _StateMerChandiseManegerShow = value
        End Set
    End Property

    Private Shared _StateAppPR As Boolean = False
    Public Shared Property StateAppPR As Boolean
        Get
            Return _StateAppPR
        End Get
        Set(value As Boolean)
            _StateAppPR = value
        End Set
    End Property


    Friend Shared DTPRPurchaseAssetNo As DataTable
    Friend Property Data_DTPRPurchaseAssetNo As DataTable
        Get
            Return DTPRPurchaseAssetNo
        End Get
        Set(ByVal value As DataTable)
            DTPRPurchaseAssetNo = value
        End Set
    End Property
    Friend Shared DTPRPurchaseAssetNoSafety As DataTable
    Friend Property Data_DTPRPurchaseAssetNoSafety As DataTable
        Get
            Return DTPRPurchaseAssetNoSafety
        End Get
        Set(ByVal value As DataTable)
            DTPRPurchaseAssetNoSafety = value
        End Set
    End Property
    Friend Shared DTPurchaseAssetNoDirector As DataTable
    Friend Property Data_DTPurchaseAssetNoDirector As DataTable
        Get
            Return DTPurchaseAssetNoDirector
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseAssetNoDirector = value
        End Set
    End Property
    Friend Shared DTPRPurchaseAssetNoDirector As DataTable
    Friend Property Data_DTPRPurchaseAssetNoDirector As DataTable
        Get
            Return DTPRPurchaseAssetNoDirector
        End Get
        Set(ByVal value As DataTable)
            DTPRPurchaseAssetNoDirector = value
        End Set
    End Property

    Private Shared _StateAssetShow As Boolean = False
    Public Shared Property StateAssetShow As Boolean
        Get
            Return _StateAssetShow
        End Get
        Set(value As Boolean)
            _StateAssetShow = value
        End Set
    End Property
    Private Shared _StateAssetPRShow As Boolean = False
    Public Shared Property StateAssetPRShow As Boolean
        Get
            Return _StateAssetPRShow
        End Get
        Set(value As Boolean)
            _StateAssetPRShow = value
        End Set
    End Property
    Private Shared _StateAssetPRShowSa As Boolean = False
    Public Shared Property StateAssetPRShowSa As Boolean
        Get
            Return _StateAssetPRShowSa
        End Get
        Set(value As Boolean)
            _StateAssetPRShowSa = value
        End Set
    End Property
    Private Shared _StateAssetDirectorShow As Boolean = False
    Public Shared Property StateAssetDirectorShow As Boolean
        Get
            Return _StateAssetDirectorShow
        End Get
        Set(value As Boolean)
            _StateAssetDirectorShow = value
        End Set
    End Property
    Private Shared _StateAssetPRDirectorShow As Boolean = False
    Public Shared Property StateAssetPRDirectorShow As Boolean
        Get
            Return _StateAssetPRDirectorShow
        End Get
        Set(value As Boolean)
            _StateAssetPRDirectorShow = value
        End Set
    End Property

#End Region

#Region "Function "

    Friend Function Update_SupervisorApproved(ByVal TempGrid As GridView, ByVal TempStatus As String, Optional remark As String = "") As Boolean

        Dim _Str As String = String.Empty
        Dim _IntCount As Integer = 0

        Try

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_UPDATEPO_SP_APP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()) & "','" & HI.UL.ULF.rpQuoted(TempStatus) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString()) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)


                End If

            Next


            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Friend Function Update_ManagerApproved(ByVal TempGrid As GridView, ByVal TempStatus As String, Optional remark As String = "") As Boolean

        Dim _Str As String = String.Empty
        Dim _IntCount As Integer = 0

        Try

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_UPDATEPO_MANAGER_APP '" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()) & "','" & HI.UL.ULF.rpQuoted(TempStatus) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString()) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                End If

            Next

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Friend Function Update_ManagerFactoryApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        'Dim _aPurchaseBy() As String
        'Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            Dim _dtupdate As New DataTable
            _dtupdate.Columns.Add("FTOrderNo", GetType(String))

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    If _dtupdate.Select("FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'").Length <= 0 Then

                        _dtupdate.Rows.Add(R!FTOrderNo.ToString())

                        _Str = ""
                        _Str = "UPDATE A  "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppBy] = '" & HI.ST.SysInfo.DirectorName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]  AS A "
                        _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"


                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                        If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                            For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                                Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                                Exit For
                            Next
                        Else
                            _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                        End If

                    End If


                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Approved Order Factory  ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.SysInfo.DirectorName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "',0)"

            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.SysInfo.DirectorName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Approved Order Factory  ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.SysInfo.DirectorName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "',1)"

            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If
            'Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_ManagerFactoryApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        'Dim _aPurchaseBy() As String
        'Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            Dim _dtUpdate As New DataTable
            _dtUpdate.Columns.Add("FTOrderNo", GetType(String))

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    If _dtUpdate.Select("FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'").Length <= 0 Then

                        _dtUpdate.Rows.Add(R!FTOrderNo.ToString())

                        _Str = ""
                        _Str = "UPDATE A  "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectBy] = '" & HI.ST.SysInfo.DirectorName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]  AS A "
                        _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"



                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                        If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                            For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                                Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                                Exit For
                            Next
                        Else
                            _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                        End If

                    End If


                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Reject Order Factory',0,1,0,0,0,0,"
            '    _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "',0)"

            '    'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark


            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If


            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Reject Order Factory' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "',1)"

            '    'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If

            'Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Friend Function Update_FactoryApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            Dim _dtupdate As New DataTable
            _dtupdate.Columns.Add("FTOrderNo", GetType(String))
            _dtupdate.Columns.Add("FTPORef", GetType(String))

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    If _dtupdate.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "'").Length <= 0 Then

                        _dtupdate.Rows.Add(R!FTOrderNo.ToString(), R!FTPORef.ToString())

                        _Str = ""
                        _Str = "UPDATE A  "
                        _Str &= Environment.NewLine & "SET  [FTStateFactoryApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTStateFactoryAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateFactoryAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateFactoryAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]  AS A "
                        _Str &= Environment.NewLine & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'  AND FTPORef = '" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' AND ISNULL(FTPORef,'') <> ''"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        '    HI.Conn.SQLConn.Tran.Rollback()
                        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        '    Return False
                        'End If

                        _Str = "UPDATE  A "
                        _Str &= vbCrLf & " SET  [FTStateFactoryApp] = '" & TempStatus & "'"
                        _Str &= vbCrLf & " , [FTStateFactoryAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= vbCrLf & ", [FDStateFactoryAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & ", [FTStateFactoryAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"


                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        _Str = "UPDATE  A "
                        _Str &= vbCrLf & " SET  [FTStateFactoryApp] = '" & TempStatus & "'"
                        _Str &= vbCrLf & " , [FTStateFactoryAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= vbCrLf & ", [FDStateFactoryAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & ", [FTStateFactoryAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND (A.FTSubOrderNo + '-D' +Convert(nvarchar(30),A.FNDivertSeq ))=B.FTSubOrderNo"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                            For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                                Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " PO :" & R!FTPORef.ToString & " (" & R!FTCmpCodeTo.ToString & ")"
                                Exit For
                            Next
                        Else
                            _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " PO :" & R!FTPORef.ToString & " (" & R!FTCmpCodeTo.ToString & ")")
                        End If

                    End If



                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Factory  Approved Order ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Factory  Approved Order ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If
            'Next


            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_LineLeaderApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then
                    _Str = ""
                    _Str = "UPDATE A  "
                    _Str &= Environment.NewLine & "SET  [FTStateLineLeaderApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTLineLeaderAppName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTLineLeaderAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTLineLeaderAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPROTQAPreFinalNikeAudit]  AS A "
                    _Str &= Environment.NewLine & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditNo.ToString()) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PROD)

                    If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'").Length > 0 Then

                        For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'")
                            Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTQANikeAuditNo.ToString()
                            Exit For
                        Next

                    Else
                        _dtMail.Rows.Add(R!FTQANikeAuditBy.ToString(), R!FTQANikeAuditNo.ToString())
                    End If

                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Line Leader Approved  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Line Leader Approved  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            'Next

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_LineLeaderReject(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, Reason As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then
                    _Str = ""
                    _Str = "UPDATE A  "
                    _Str &= Environment.NewLine & "SET  [FTStateLineLeaderReject] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTLineLeaderRejectName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTLineLeaderRejectDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTLineLeaderRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPROTQAPreFinalNikeAudit]  AS A "
                    _Str &= Environment.NewLine & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditNo.ToString()) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PROD)

                    If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'").Length > 0 Then

                        For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'")
                            Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTQANikeAuditNo.ToString()
                            Exit For
                        Next

                    Else
                        _dtMail.Rows.Add(R!FTQANikeAuditBy.ToString(), R!FTQANikeAuditNo.ToString())
                    End If

                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Line Leader Rejected  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString & vbCrLf & Reason) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Line Leader Rejected  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString & vbCrLf & Reason) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            'Next

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_QAFLeaderApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then
                    _Str = ""
                    _Str = "UPDATE A  "
                    _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPROTQAPreFinalNikeAudit]  AS A "
                    _Str &= Environment.NewLine & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditNo.ToString()) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PROD)

                    If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'").Length > 0 Then

                        For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'")
                            Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTQANikeAuditNo.ToString()
                            Exit For
                        Next

                    Else
                        _dtMail.Rows.Add(R!FTQANikeAuditBy.ToString(), R!FTQANikeAuditNo.ToString())
                    End If

                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)

            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Line Leader Approved  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Line Leader Approved  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            'Next

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Friend Function Update_QAFLeaderReject(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, Reason As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then
                    _Str = ""
                    _Str = "UPDATE A  "
                    _Str &= Environment.NewLine & "SET  [FTStateReject] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTRejectName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTRejectDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TPROTQAPreFinalNikeAudit]  AS A "
                    _Str &= Environment.NewLine & " WHERE FTQANikeAuditNo='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditNo.ToString()) & "' "

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PROD)

                    If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'").Length > 0 Then

                        For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTQANikeAuditBy.ToString()) & "'")
                            Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTQANikeAuditNo.ToString()
                            Exit For
                        Next

                    Else
                        _dtMail.Rows.Add(R!FTQANikeAuditBy.ToString(), R!FTQANikeAuditNo.ToString())
                    End If

                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'QA Final Leader Rejected  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString & vbCrLf & Reason) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'QA Final Leader Rejected  QA Final ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString & vbCrLf & Reason) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            'Next

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_CMInvApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, Optional ByVal Reson As String = "", Optional ByVal StateWH As String = "0") As Boolean
        'comment State 'TempStatus' 1 = Approved , 0 = Reject
        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _IntCount As Integer = 0
        Dim _dt As DataTable
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))
            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length
                _dt = .Copy
            End With

            Dim _dtupdate As New DataTable
            _dtupdate.Columns.Add("FTCustomerPO", GetType(String))
            _dtupdate.Columns.Add("FTInvoiceNo", GetType(String))


            Select Case TempStatus
                Case "1" ' Approved
                    For Each R As DataRow In _dt.Select("FTSelect='1'")
                        If R!FTSelect.ToString = "1" Then

                            If _dtupdate.Select("FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString()) & "' AND  FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString()) & "'").Length <= 0 Then
                                _Str = ""
                                _Str = "UPDATE A  "
                                Select Case StateWH
                                    Case "0" ' Factory Manager
                                        _Str &= Environment.NewLine & "SET  FTStateApp = '1'"
                                        _Str &= Environment.NewLine & ", FTStateAppBy = '" & HI.ST.UserInfo.UserName & "'"
                                        _Str &= Environment.NewLine & ", FDStateAppDate = " & HI.UL.ULDate.FormatDateDB
                                        _Str &= Environment.NewLine & ", FTStateAppTime = " & HI.UL.ULDate.FormatTimeDB
                                    Case "1" ' WareHouse Manager
                                        _Str &= Environment.NewLine & "SET  FTStateWHApp = '1'"
                                        _Str &= Environment.NewLine & ", FTStateWHAppBy = '" & HI.ST.UserInfo.UserName & "'"
                                        _Str &= Environment.NewLine & ", FDStateWHAppDate = " & HI.UL.ULDate.FormatDateDB
                                        _Str &= Environment.NewLine & ", FTStateWHAppTime = " & HI.UL.ULDate.FormatTimeDB
                                End Select
                                _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTFactoryCMInvoice]  AS A "
                                _Str &= Environment.NewLine & " WHERE FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString()) & "'"
                                _Str &= Environment.NewLine & " AND FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString()) & "'"
                                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                                If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(IIf(StateWH = "0", R!FTStateSendBy.ToString(), R!FTStateAppBy.ToString())) & "'").Length > 0 Then
                                    For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(IIf(StateWH = "0", R!FTStateSendBy.ToString(), R!FTStateAppBy.ToString())) & "'")
                                        Rx!FTMessage = Rx!FTMessage.ToString & "," & vbLf & " Customer PO No. " & R!FTCustomerPO.ToString() & " Invoice NO." & R!FTInvoiceNO.ToString & ")"
                                        Exit For
                                    Next
                                Else
                                    _dtMail.Rows.Add(IIf(StateWH = "0", R!FTStateSendBy.ToString(), R!FTStateAppBy.ToString()), " Customer PO No. " & R!FTCustomerPO.ToString() & " Invoice NO." & R!FTInvoiceNO.ToString & ")")
                                End If
                            End If
                        End If


                    Next
                    'For Each R As DataRow In _dtMail.Rows
                    '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    '    _Str = ""
                    '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
                    '    _Str &= ",'" & IIf(StateWH = "0", "Factory", "Ware House") & " Manager  Approved','Factory Manager  Approved  Successfully " & vbCr & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
                    '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                    '    HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    'Next
                Case "0"  ' Reject
                    For Each R As DataRow In _dt.Select("FTSelect='1'")

                        If R!FTSelect.ToString = "1" Then

                            If _dtupdate.Select("FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString()) & "' AND  FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString()) & "'").Length <= 0 Then
                                _Str = ""
                                _Str = "UPDATE A  "
                                Select Case StateWH
                                    Case "0"
                                        _Str &= Environment.NewLine & "SET  FTStateReject = '1'"
                                        _Str &= Environment.NewLine & ", FTStateRejectBy = '" & HI.ST.UserInfo.UserName & "'"
                                        _Str &= Environment.NewLine & ", FDStateRejectDate = " & HI.UL.ULDate.FormatDateDB
                                        _Str &= Environment.NewLine & ", FTStateRejectTime = " & HI.UL.ULDate.FormatTimeDB
                                        _Str &= Environment.NewLine & ",FTStateApp = '0'"
                                        _Str &= Environment.NewLine & ", FTStateAppBy = ''"
                                    Case "1"
                                        _Str &= Environment.NewLine & "SET  FTStateWHReject = '1'"
                                        _Str &= Environment.NewLine & ", FTStateWHRejectBy = '" & HI.ST.UserInfo.UserName & "'"
                                        _Str &= Environment.NewLine & ", FDStateWHRejectDate = " & HI.UL.ULDate.FormatDateDB
                                        _Str &= Environment.NewLine & ", FTStateWHRejectTime = " & HI.UL.ULDate.FormatTimeDB
                                        _Str &= Environment.NewLine & ",FTStateWHApp = '0'"
                                        _Str &= Environment.NewLine & ", FTStateWHAppBy = ''"
                                End Select

                                _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTFactoryCMInvoice]  AS A "
                                _Str &= Environment.NewLine & " WHERE FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString()) & "'"
                                _Str &= Environment.NewLine & " AND FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString()) & "'"

                                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If

                                If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(IIf(StateWH = "0", R!FTStateRejectBy.ToString(), R!FTStateWHRejectBy.ToString())) & "'").Length > 0 Then
                                    For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(IIf(StateWH = "0", R!FTStateRejectBy.ToString(), R!FTStateWHRejectBy.ToString())) & "'")
                                        Rx!FTMessage = Rx!FTMessage.ToString & "," & vbLf & " Customer PO No. " & R!FTCustomerPO.ToString() & " Invoice NO." & R!FTInvoiceNO.ToString & ")"
                                        Exit For
                                    Next
                                Else
                                    _dtMail.Rows.Add(IIf(StateWH = "0", R!FTStateSendBy.ToString(), R!FTStateAppBy.ToString()), " Customer PO No. " & R!FTCustomerPO.ToString() & " Invoice NO." & R!FTInvoiceNO.ToString & ")")
                                End If
                            End If


                        End If

                    Next

                    'For Each R As DataRow In _dtMail.Rows
                    '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    '    _Str = ""
                    '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
                    '    _Str &= ",'" & IIf(StateWH = "0", "Factory", "Ware House") & " Manager Reject','Factory Manager  Reject" & vbCr & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & vbCr & "  Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                    '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                    '    HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    'Next

            End Select
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Friend Function Update_FactoryCMApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, Optional ByVal Reson As String = "", Optional ByVal StateWH As String = "0") As Boolean
        'comment State 'TempStatus' 1 = Approved , 0 = Reject
        Dim _Str As String = String.Empty
        '' Dim _FTMailId As Long
        Dim _IntCount As Integer = 0
        Dim _dt As DataTable
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            'Dim _dtMail As New DataTable
            '_dtMail.Columns.Add("FTUser", GetType(String))
            '_dtMail.Columns.Add("FTMessage", GetType(String))
            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length
                _dt = .Copy
            End With

            'Dim _dtupdate As New DataTable
            '_dtupdate.Columns.Add("FTCustomerPO", GetType(String))
            '_dtupdate.Columns.Add("FTInvoiceNo", GetType(String))


            Select Case TempStatus
                Case "1" ' Approved
                    For Each R As DataRow In _dt.Select("FTSelect='1'")
                        If R!FTSelect.ToString = "1" Then


                            _Str = "UPDATE A  "
                                Select Case StateWH
                                    Case "0" ' Factory Manager
                                    _Str &= Environment.NewLine & "SET  FTStateFactoryApp = '1'"
                                    _Str &= Environment.NewLine & ", FTStateFactoryAppBy = '" & HI.ST.UserInfo.UserName & "'"
                                    _Str &= Environment.NewLine & ", FDStateFactoryAppDate = " & HI.UL.ULDate.FormatDateDB
                                    _Str &= Environment.NewLine & ", FTStateFactoryAppTime = " & HI.UL.ULDate.FormatTimeDB


                                    'Case "1" ' WareHouse Manager
                                    '    _Str &= Environment.NewLine & "SET  FTStateWHApp = '1'"
                                    '    _Str &= Environment.NewLine & ", FTStateWHAppBy = '" & HI.ST.UserInfo.UserName & "'"
                                    '    _Str &= Environment.NewLine & ", FDStateWHAppDate = " & HI.UL.ULDate.FormatDateDB
                                    '    _Str &= Environment.NewLine & ", FTStateWHAppTime = " & HI.UL.ULDate.FormatTimeDB
                            End Select
                            _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder_Change_Factory_CM]  AS A "
                            _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                            _Str &= Environment.NewLine & " AND FNSeq = '" & HI.UL.ULF.rpQuoted(R!FNSeq.ToString()) & "'"
                            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If


                        End If


                    Next

                Case "0"  ' Reject
                    For Each R As DataRow In _dt.Select("FTSelect='1'")

                        If R!FTSelect.ToString = "1" Then

                            _Str = ""
                            _Str = "UPDATE A  "
                                Select Case StateWH
                                    Case "0"
                                    _Str &= Environment.NewLine & "SET  FTStateFactoryReject = '1'"
                                    _Str &= Environment.NewLine & ", FTStateFactoryRejectBy = '" & HI.ST.UserInfo.UserName & "'"
                                    _Str &= Environment.NewLine & ", FDStateFactoryRejectDate = " & HI.UL.ULDate.FormatDateDB
                                    _Str &= Environment.NewLine & ", FTStateFactoryRejectTime = " & HI.UL.ULDate.FormatTimeDB
                                    _Str &= Environment.NewLine & ",FTStateFactoryApp = '0'"
                                    _Str &= Environment.NewLine & ", FTStateFactoryAppBy = ''"
                                    _Str &= Environment.NewLine & " , FTReason = '" & HI.UL.ULF.rpQuoted(Reson) & "'"
                            End Select

                            _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder_Change_Factory_CM]  AS A "
                            _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                            _Str &= Environment.NewLine & " AND FNSeq = '" & HI.UL.ULF.rpQuoted(R!FNSeq.ToString()) & "'"

                            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If


                        End If



                    Next


            End Select
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


    Friend Function Update_TVWApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then
                    _Str = ""
                    _Str = "UPDATE A  "
                    _Str &= Environment.NewLine & "SET  [FTStateMerApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTStateMerAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FDStateMerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTStateMerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTFactoryCMInvoice]  AS A "
                    _Str &= Environment.NewLine & " WHERE FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString()) & "'"


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    'If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                    '    For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                    '        Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                    '        Exit For
                    '    Next
                    'Else
                    '    _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                    'End If

                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Factory  Approved Order ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If

            '    ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Factory  Approved Order ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If
            'Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_TVWRejected(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        'Dim _aPurchaseBy() As String
        'Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then
                    _Str = ""
                    _Str = "UPDATE A  "
                    _Str &= Environment.NewLine & "SET  [FTStateMerReject] = '1'"
                    _Str &= Environment.NewLine & ", [FTStateMerRejectBy] = '" & HI.ST.SysInfo.DirectorName & "'"
                    _Str &= Environment.NewLine & ", [FDStateMerRejectDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTStateMerRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[TACCTFactoryCMInvoice]  AS A "
                    _Str &= Environment.NewLine & " WHERE FTInvoiceNo = '" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString()) & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    'If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                    '    For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                    '        Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                    '        Exit For
                    '    Next
                    'Else
                    '    _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                    'End If

                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Reject Order Factory',0,1,0,0,0,0,"
            '    _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "',0)"

            '    'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark


            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If


            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Reject Order Factory' ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "',1)"

            '    'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    End If

            'Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Friend Function Update_FactoryApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try

            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            Dim _dtupdate As New DataTable
            _dtupdate.Columns.Add("FTOrderNo", GetType(String))
            _dtupdate.Columns.Add("FTPORef", GetType(String))

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    If _dtupdate.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "'").Length <= 0 Then

                        _dtupdate.Rows.Add(R!FTOrderNo.ToString(), R!FTPORef.ToString())

                        _Str = ""
                        _Str = "UPDATE A  "
                        _Str &= Environment.NewLine & "SET  [FTStateFactoryReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateFactoryRejectBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateFactoryRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateFactoryRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]  AS A "
                        _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'  AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        '    HI.Conn.SQLConn.Tran.Rollback()
                        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        '    Return False
                        'End If

                        _Str = "UPDATE  A "
                        _Str &= Environment.NewLine & "SET  [FTStateFactoryReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateFactoryRejectBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateFactoryRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateFactoryRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        _Str = "UPDATE  A "
                        _Str &= Environment.NewLine & "SET  [FTStateFactoryReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateFactoryRejectBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateFactoryRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateFactoryRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND (A.FTSubOrderNo + '-D' +Convert(nvarchar(30),A.FNDivertSeq ))=B.FTSubOrderNo"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                            For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                                Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " PO : " & R!FTPOref.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                                Exit For
                            Next

                        Else
                            _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " PO : " & R!FTPOref.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                        End If

                    End If


                End If
            Next

            'For Each R As DataRow In _dtMail.Rows
            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Factory Reject Order ',0,1,0,0,0,0,"
            '    _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

            '    'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If


            '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
            '    _Str = ""
            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
            '    _Str &= ",'Factory Reject Order  ,0,1,0,0,0,0,"
            '    _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

            '    'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark
            '    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
            '    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            '    'End If

            'Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try
                _dt.Dispose()
                _dtMail.Dispose()
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function


    Friend Function Update_SectApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                If R!FTApproveType.ToString = "0" Then
                    _Cmd &= vbCrLf & " Set FTStateMNGDepApp='" & StateApprove & "'"
                    _Cmd &= vbCrLf & ", FTMNGDepAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDMNGDepAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTMNGDepAppTime=" & HI.UL.ULDate.FormatTimeDB
                ElseIf R!FTApproveType.ToString = "1" Then
                    _Cmd &= vbCrLf & " Set FTStateManagerApp='" & StateApprove & "'"
                    _Cmd &= vbCrLf & ", FTManagerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                End If
                _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                '_FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                'If StateApprove = "1" Then
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                '    _Cmd &= ",'Manager approve','Dear " & R!FTSandApproveBy.ToString & " Manager " & vbCr & "Manager Approve document Successfully.  " & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'Else
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                '    _Cmd &= ",'Manager Reject','Dear " & R!FTSandApproveBy.ToString & " Manager " & vbCr & "I'm Reject document   Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'End If

            Next
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


    Public Shared Function LoadogcTPURTPurchase() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str &= " SELECT *  "
            _str &= " FROM  ( SELECT  isnull(A.FTStateSuperVisorApp,0) as FTStateApproved, A.FTPurchaseNo,"
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
            _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode AS FTDeliveryCode2,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
                Case HI.ST.Lang.eLang.TH
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

            _str &= Environment.NewLine & "  ,A.FTPoTypeState,A.FTStateFree"
            '  _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A   INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTPurchaseBy = b.FTUserName INNER JOIN"
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
            _str &= Environment.NewLine & " WHERE (C.FTUserName = '" & HI.ST.UserInfo.UserName & "') AND A.FTPoTypeState <> '3'"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '0')"


            _str &= " UNION "
            _str &= "SELECT  isnull(A.FTStateSuperVisorApp,0) as FTStateApproved, A.FTPurchaseNo,"
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
            _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode  AS FTDeliveryCode2,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
                Case HI.ST.Lang.eLang.TH
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

            _str &= Environment.NewLine & "  ,A.FTPoTypeState,A.FTStateFree"
            '  _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A   INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTPurchaseBy = b.FTUserName INNER JOIN"
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
            _str &= Environment.NewLine & " WHERE (C.FTUserNameTo = '" & HI.ST.UserInfo.UserName & "') AND A.FTPoTypeState = '3'"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '0')"

            _str &= Environment.NewLine & " ) AS X "

            _str &= Environment.NewLine & " Order by X.FTPurchaseBy, X.FDPurchaseDate"


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
            MsgBox(ex.Message)
        End Try

    End Function


    Public Shared Function LoadogcSMP() As DataTable
        Try
            Dim _Qry As String = String.Empty
            Dim _dt As New DataTable

            _Qry = " SELECT TOP 1 '0' AS FTSelect,FTStateFinishDate AS FTStateFinishDateOrg"


            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam  As A With(NOLOCK)"

            _Qry &= vbCrLf & " WHERE   (ISNULL(A.FTStateSendApp,'') = '1') AND (ISNULL(A.FTStateApprove,'') <> '1' )  AND (ISNULL(A.FTStateApprove,'') <> '2' )"

            _Qry &= vbCrLf & " UNION "


            _Qry &= vbCrLf & " SELECT  '0' AS FTSelect,FTCalculateDate AS FTStateFinishDateOrg"


            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut  As A With(NOLOCK)"


            _Qry &= vbCrLf & " WHERE   (ISNULL(A.FTStateSendApp,'') = '1') AND (ISNULL(A.FTStateApprove,'') <> '1' )  AND (ISNULL(A.FTStateApprove,'') <> '2' )"



            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountAppSMP = _dt.Rows.Count
                Return _dt
            Else
                _CountAppSMP = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    Public Shared Function LoadogcSMPMGR() As DataTable
        Try
            Dim _Qry As String = String.Empty
            Dim _dt As New DataTable



            _Qry = " SELECT TOP 1 '0' AS FTSelect,FTStateFinishDate AS FTStateFinishDateOrg"


            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam  As A With(NOLOCK)"

            _Qry &= vbCrLf & " WHERE   (ISNULL(A.FTStateSendApp,'') = '1') AND  (ISNULL(A.FTStateApprove,'') = '1') AND (ISNULL(A.FTStateManagerApprove,'') <> '1' )  AND (ISNULL(A.FTStateManagerApprove,'') <> '2' )"

            _Qry &= vbCrLf & " UNION "


            _Qry &= vbCrLf & " SELECT  '0' AS FTSelect,FTCalculateDate AS FTStateFinishDateOrg"


            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPCalculateCut  As A With(NOLOCK)"


            _Qry &= vbCrLf & " WHERE   (ISNULL(A.FTStateSendApp,'') = '1') AND  (ISNULL(A.FTStateApprove,'') = '1') AND (ISNULL(A.FTStateManagerApprove,'') <> '1' )  AND (ISNULL(A.FTStateManagerApprove,'') <> '2' )"



            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountAppSMPMGR = _dt.Rows.Count
                Return _dt
            Else
                _CountAppSMPMGR = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function



    Public Shared Function LoadAppRDSam() As DataTable
        Try
            Dim _Qry As String = String.Empty
            Dim _dt As New DataTable



            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.USP_CHECKAPP_RDSAM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserPassword) & "','N'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountAppRDSam = _dt.Rows.Count
                Return _dt
            Else
                _CountAppRDSam = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function



    Public Shared Function LoadfactoryApprove() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LIST_DIRECTOR_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountAppDirector = _dt.Rows.Count
                Return _dt
            Else
                _CountAppDirector = 0
                Return Nothing
            End If

            _dt.Dispose()

        Catch ex As Exception
        End Try

    End Function

    'ขึ้นรายการกรณี เช็คสถานะแล้วเป็น ผจก โรงงาน
    Public Shared Function LoadogcTFIXEDTPurchase_RequestFA() As DataTable


        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = "SELECT  isnull(A.FTStateApp,0) as FTStateApproved, A.FTPRPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPRPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPRPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPRPurchaseDate,1,4) as FDPRPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPRPurchaseBy,'') as FTPRPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTAppName,'') as FTAppName, "
            _str &= Environment.NewLine & " isnull(LD.FTNameTH,'') as FNPRState,"
            _str &= Environment.NewLine & " ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL(Convert(numeric(18,2),A.FNNetAmt),0) as FNNetAmt, ISNULL(Convert(numeric(18,2),A.FNQuantity),0) as FNQuantity,"
            _str &= Environment.NewLine & " ISNULL(L.FTUserName,'') as FTUserName, A.FNFixedAssetType"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS A  with (nolock) LEFT OUTER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with (nolock) ON A.FNHSysCmpId=C.FNHSysCmpId   LEFT OUTER JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel AS L with (nolock) ON A.FNFixedAssetType=L.FNFixedAssetType AND A.FNNetAmt>=L.FNStartQty AND C.FTUserName=L.FTUserName   AND A.FNHSysCmpId=L.FNHSysCmpId "
            _str &= Environment.NewLine & "LEFT OUTER JOIN  (select LD.FTNameTH,LD.FNListIndex"
            _str &= Environment.NewLine & " from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD with (nolock) "
            _str &= Environment.NewLine & " where LD.FTListName='FNPRState'  )as LD ON A.FNPRState=LD.FNListIndex"
            _str &= Environment.NewLine & " WHERE (L.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateApp = '0')" '  AND L.FTStateFactory='1'" ' or (a.FTStateApp = '3'))"
            _str &= Environment.NewLine & "group by A.FTStateApp,A.FTPRPurchaseNo,A.FDPRPurchaseDate,A.FTPRPurchaseBy,A.FTAppName,LD.FTNameTH,A.FTRemark,A.FNQuantity,L.FTUserName, A.FNFixedAssetType,A.FNNetAmt"
            _str &= Environment.NewLine & " Order by A.FTPRPurchaseBy, a.FDPRPurchaseDate"


            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MASTER)

            If _dt.Rows.Count > 0 Then
                _CountAppAssetPR = _dt.Rows.Count
                Return _dt
            Else
                _CountAppAssetPR = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function




    Public Shared Function LoadFactoryManagerApprove() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LIST_FACTORY_MANAGER_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)


            ''Dim _oDt As DataTable = wFactoryManagerApproved.LoadCommercialInvoice

            If _dt.Rows.Count > 0 Then
                _CountAppFacManager = _dt.Rows.Count
                Return _dt
            Else
                'If _oDt.Rows.Count > 0 Then
                '    _CountAppFacManager = _oDt.Rows.Count
                '    Return _dt
                'Else
                '    _CountAppFacManager = 0
                '    Return Nothing
                'End If
                _CountAppFacManager = 0
                Return Nothing

            End If

            _dt.Dispose()

        Catch ex As Exception
        End Try

    End Function

    Public Shared Function LoadFactoryManagerApproveCM() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LISTING_CHANGE_FACTORY_CM_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MERCHAN)


            Dim _oDt As DataTable = wFactoryManagerApproved.LoadCommercialInvoice

            If _dt.Rows.Count > 0 Then
                _CountAppFacManager = _dt.Rows.Count
                Return _dt
            Else
                If _oDt.Rows.Count > 0 Then
                    _CountAppFacManager = _oDt.Rows.Count
                    Return _dt
                Else
                    _CountAppFacManager = 0
                    Return Nothing
                End If
            End If

            _dt.Dispose()

        Catch ex As Exception
        End Try

    End Function

    Public Shared Function LoadWareHouseManagerApproved() As Integer
        Try
            Dim _oDt As DataTable = wWHManegerApproved.LoadCommercialInvoice
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Public Shared Function LoadSewingLineLeaderApproved() As Integer
        Try
            Dim _oDt As DataTable = wLineLeaderApproved.LoadDatainfoApprove
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function LoadQAFinalLeaderApproved() As Integer
        Try
            Dim _oDt As DataTable = wQAFinalLeaderApproved.LoadDatainfoApprove
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function LoadMerManagerApproved() As Integer
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            '_Cmd = "  Select A.FTSelect"
            '_Cmd &= vbCrLf & " ,A.FTCustomerPO"
            '_Cmd &= vbCrLf & " ,A.FTStyleCode"
            '_Cmd &= vbCrLf & "  ,A.FTInvoiceNo"
            '_Cmd &= vbCrLf & "  ,CASE WHEN ISDATE(A.FDInvoiceDate) = 1 THEN Convert(Datetime,A.FDInvoiceDate) ELSE NULL END  FDInvoiceDate"
            '_Cmd &= vbCrLf & "  ,A.FTCmpCode"
            '_Cmd &= vbCrLf & "  ,A.FNHSysCustId"
            '_Cmd &= vbCrLf & "  ,A.FTInvoiceExportNo"
            '_Cmd &= vbCrLf & "  ,CASE WHEN ISDATE(A.FDInvoiceExportDate) = 1 THEN Convert(Datetime,A.FDInvoiceExportDate) ELSE NULL END FDInvoiceExportDate"
            '_Cmd &= vbCrLf & "  ,A.FTInvoiceExportNote"
            '_Cmd &= vbCrLf & "  ,A.FNQuantity"
            '_Cmd &= vbCrLf & "  ,A.FNCM"
            '_Cmd &= vbCrLf & "  ,Convert(numeric(18,2),A.FNQuantity * A.FNCM) AS FNCMAmt"
            '_Cmd &= vbCrLf & " ,A.FNNetCM"
            '_Cmd &= vbCrLf & "  ,A.FNFirstPrice"
            '_Cmd &= vbCrLf & "  ,Convert(numeric(18,2),A.FNQuantity * A.FNFirstPrice) AS FNFirstPriceAmt"
            '_Cmd &= vbCrLf & " FROM("
            '_Cmd &= vbCrLf & " SELECT TOP 1  '0' AS FTSelect, I.FTCustomerPO, I.FTInvoiceNo,  SUM(ISNULL(D.FNQuantity, 0)) AS FNQuantity,  S.FTStyleCode, "
            '_Cmd &= vbCrLf & "C.FTCmpCode"
            '_Cmd &= vbCrLf & "  ,  I.FDInvoiceDate "
            '_Cmd &= vbCrLf & "   , I.FTInvoiceExportNo, "
            '_Cmd &= vbCrLf & "   I.FDInvoiceExportDate, I.FTInvoiceExportNote"
            '_Cmd &= vbCrLf & "  ,S.FNCM "
            '_Cmd &= vbCrLf & "  ,S.FNNetCM "
            '_Cmd &= vbCrLf & "	,ISNULL(("
            '_Cmd &= vbCrLf & "	 SELECT TOP 1 FNNetFirstSale "
            '_Cmd &= vbCrLf & "  FROM   HITECH_ACCOUNT.dbo.TACCTTransactionValueWorksheet AS XCA WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "    WHERE XCA.FTCustomerPO = I.FTCustomerPO"
            '_Cmd &= vbCrLf & "	 ),0) AS FNFirstPrice"
            '_Cmd &= vbCrLf & "	,Max(O.FNHSysCustId) AS FNHSysCustId "
            '_Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTCustomerPO = D.FTCustomerPO AND I.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH (NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON I.FNHsysCmpID = C.FNHSysCmpId"
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "  ( SELECT OSB.FTPORef, OSB.FTColorway, OSB.FTSizeBreakDown, OSB.FNPrice AS FNPriceFOB, MAX(OSB.FNNetPrice) AS FNNetPrice"
            '_Cmd &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS OSB INNER JOIN"
            '_Cmd &= vbCrLf & "	         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON OSB.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & "	GROUP BY OSB.FTPORef, OSB.FTColorway, OSB.FTSizeBreakDown, OSB.FNPrice ) AS OBS "
            '_Cmd &= vbCrLf & " ON I.FTCustomerPO = OBS.FTPORef AND D.FTColorway = OBS.FTColorway AND D.FTSizeBreakDown = OBS.FTSizeBreakDown "
            '_Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS MRT WITH(NOLOCK) ON O.FNHSysMerTeamId=MRT.FNHSysMerTeamId "
            '_Cmd &= vbCrLf & "   WHERE ISNULL(I.FTInvoiceExportNo,'')<>''"
            '_Cmd &= vbCrLf & "   AND MRT.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & "   AND ISNULL(I.FTStateMerApp,'0')  = '0' "
            '_Cmd &= vbCrLf & "   AND ISNULL(I.FTStateMerReject,'0')  = '0' "
            '_Cmd &= vbCrLf & " GROUP BY I.FTCustomerPO, I.FTInvoiceNo, I.FDInvoiceDate,  S.FTStyleCode, C.FTCmpCode, I.FTInvoiceExportNo, I.FDInvoiceExportDate, I.FTInvoiceExportNote"
            '_Cmd &= vbCrLf & "  ,S.FNCM "
            '_Cmd &= vbCrLf & "   ,S.FNNetCM"
            '_Cmd &= vbCrLf & "	 ) AS A "
            '_Cmd &= vbCrLf & "	 ORDER BY A.FTCustomerPO ,A.FTInvoiceNo  "

            Dim _Lang As String = ""
            If HI.ST.Lang.Language = HI.ST.Lang.eLang.TH Then
                _Lang = "TH"
            Else
                _Lang = "EN"
            End If
     
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GET_MERAPP_TVW '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_Lang) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Public Shared Function LoadogcTPURTPurchaseDirector() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str &= " SELECT * FROM "
            _str &= " ( SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
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
            _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode AS FTDeliveryCode2,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"
            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
                Case HI.ST.Lang.eLang.TH
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

            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN "
            '  _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName LEFT JOIN"
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


            _str &= Environment.NewLine & "   INNER JOIN (SELECT U.FTUserName, T.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId"
            '_str &= Environment.NewLine & " WHERE ISNULL(T.FNHSysDirectorGrpId,0) =0 OR T.FNHSysDirectorGrpId IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"

            _str &= Environment.NewLine & " WHERE  T.FNHSysDirectorGrpId IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrpUser AS DGU WITH(NOLOCK)  INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS DG  WITH(NOLOCK) ON DGU.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	WHERE  (DGU.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "'))"
            _str &= Environment.NewLine & " ) AS MU ON a.FTPurchaseBy = MU.FTUserName"


            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0') AND A.FTPoTypeState <> '3'"


            _str &= " UNION "
            _str &= "  SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
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
            _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode  AS FTDeliveryCode2,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"
            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
                Case HI.ST.Lang.eLang.TH
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

            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN "
            '  _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName LEFT JOIN"
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


            _str &= Environment.NewLine & "   INNER JOIN (SELECT U.FTUserName, T.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId"
            '_str &= Environment.NewLine & " WHERE ISNULL(T.FNHSysDirectorGrpId,0) =0 OR T.FNHSysDirectorGrpIdTo IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & " WHERE  T.FNHSysDirectorGrpIdTo IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrpUser AS DGU WITH(NOLOCK)  INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS DG  WITH(NOLOCK) ON DGU.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	WHERE  (DGU.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.DirectorName) & "'))"
            _str &= Environment.NewLine & " ) AS MU ON a.FTPurchaseBy = MU.FTUserName"


            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0') AND A.FTPoTypeState = '3'"


            _str &= " ) AS X "
            _str &= Environment.NewLine & " Order by X.FTPurchaseBy, X.FDPurchaseDate"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)


            If _dt.Rows.Count > 0 Then
                _CountAppDirector = _dt.Rows.Count
                Return _dt
            Else
                _CountAppDirector = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        _CountAppDirector = 0
        Return Nothing
    End Function


#End Region

    Private Shared _frmApp As wSupervisorApproved = Nothing
    Private Shared _frmAppSMP As wSMPIncentiveApproved = Nothing
    Private Shared _frmAppSMPMGR As wSMPIncentiveManagerApproved = Nothing
    Private Shared _frmAppRDSam As wSMPIncentiveManagerApproved = Nothing
    Private Shared _frmAppDirector As wDirectorApproved = Nothing
    Private Shared _frmAppFacManager As wFactoryManagerApproved = Nothing
    Private Shared _frmAppWHManager As wWHManegerApproved = Nothing
    Private Shared _frmMerManager As wMerChandiserManagerApproved = Nothing
    Private Shared _frmSeingLineLeaderApp As wLineLeaderApproved = Nothing
    Private Shared _frmQAFinalLeaderApp As wQAFinalLeaderApproved = Nothing

    Private Shared _frmAppAssetPO As wSupervisorApprovedAsset = Nothing
    Private Shared _frmAppAssetDirectorPO As wDirectorApprovedAsset = Nothing
    Private Shared _frmAppAssetDirectorPR As wDirectorApprovedAssetPR = Nothing
    Private Shared _frmAppAssetPR As wSupervisorApprovedAssetPR = Nothing
    Private Shared _frmAppAssetPRSa As wSafetyApprovedAssetPR = Nothing

    Public Shared Sub ValidateApp(Optional currentIndex As Integer = 0)

        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager


        If Not _frmApp Is Nothing Then
            Try


                If Not _frmApp.ogSupervisorApproved.DataSource Is Nothing Then
                    With CType(_frmApp.ogSupervisorApproved.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTStateApproved='1'").Length > 0 Then
                            Exit Sub
                        End If

                    End With

                End If

            Catch ex As Exception

            End Try
        End If
        _CountApp = 0
        If ClsService.StateShow = False Then
            DTPurchaseNo = Nothing
            DTPurchaseNo = LoadogcTPURTPurchase()
        End If


        ' MessageBox.Show("มี PO  " & _CountApp & " ใบ")

        If _CountApp > 0 Then

            If ClsService.StateShow = False Then
                If _frmApp Is Nothing Then
                    '_frmApp = New wSupervisorApproved
                    _frmApp = New wSupervisorApproved
                ElseIf _frmApp.IsDisposed Then
                    _frmApp = New wSupervisorApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmApp)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmApp.Name.ToString.Trim, _frmApp)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                ' _frmApp.StartPosition = FormStartPosition.CenterScreen

                Try
                    ClsService.StateShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmApp.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmApp.Show()
                    _frmApp.BringToFront()

                Catch ex As Exception

                End Try
                _frmApp = Nothing
            End If


        End If

        Try
            DTPurchaseNo = Nothing
        Catch ex As Exception

        End Try
        ' MessageBox.Show("ไม่มี PO")
        '  Call HI.Service.ClsConvertPDF.Validate_PDF()

    End Sub


    Public Shared Sub ValidateAppDirector(Optional currentIndex As Integer = 0)

        DTPurchaseNoDirector = Nothing
        DTPurchaseNoDirector = LoadogcTPURTPurchaseDirector()

        If _CountAppDirector > 0 Then

            If ClsService.StateDirectorShow = False Then
                If _frmAppDirector Is Nothing Then
                    _frmAppDirector = New wDirectorApproved
                ElseIf _frmAppDirector.IsDisposed Then
                    _frmAppDirector = New wDirectorApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppDirector)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppDirector.Name.ToString.Trim, _frmAppDirector)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try
                    ClsService.StateDirectorShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppDirector.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppDirector.Show()
                    _frmAppDirector.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppDirector = Nothing
            End If
        Else

            DTAppFactory = Nothing
            DTAppFactory = LoadfactoryApprove()

            If _CountAppDirector > 0 Then

                If ClsService.StateDirectorShow = False Then
                    If _frmAppDirector Is Nothing Then
                        _frmAppDirector = New wDirectorApproved
                    ElseIf _frmAppDirector.IsDisposed Then
                        _frmAppDirector = New wDirectorApproved
                    End If

                    HI.TL.HandlerControl.AddHandlerObj(_frmAppDirector)

                    Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                    HI.ST.SysInfo.MenuName = "mnuSecurity"
                    Dim oSysLang As New HI.ST.SysLanguage
                    Try
                        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppDirector.Name.ToString.Trim, _frmAppDirector)
                    Catch ex As Exception
                    Finally
                    End Try

                    HI.ST.SysInfo.MenuName = _TmpMenu

                    Try
                        ClsService.StateDirectorShow = True

                        Try

                            If currentIndex > 0 Then
                                _frmAppDirector.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                            End If

                        Catch ex As Exception
                        End Try

                        _frmAppDirector.Show()
                        _frmAppDirector.BringToFront()

                    Catch ex As Exception

                    End Try
                    _frmAppDirector = Nothing
                End If

            End If
        End If

        Try
            DTPurchaseNoDirector = Nothing
            DTAppFactory = Nothing
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub ValidateAppFactoryManager(Optional currentIndex As Integer = 0)

        DTAppFactoryManager = Nothing
        DTAppFactoryManager = LoadFactoryManagerApprove()

        DTAppFactoryManagerCM = Nothing
        DTAppFactoryManagerCM = LoadFactoryManagerApproveCM()

        If _CountAppFacManager > 0 Or _CountAppFacManagerCM > 0 Then

            If ClsService.StateFactoryManagerShow = False Then
                If _frmAppFacManager Is Nothing Then
                    _frmAppFacManager = New wFactoryManagerApproved
                ElseIf _frmAppFacManager.IsDisposed Then
                    _frmAppFacManager = New wFactoryManagerApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppFacManager)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppFacManager.Name.ToString.Trim, _frmAppFacManager)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try

                    ClsService.StateFactoryManagerShow = True

                    Try

                        If currentIndex > 0 Then

                            _frmAppFacManager.Bounds = Screen.AllScreens(currentIndex).WorkingArea

                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppFacManager.Show()
                    _frmAppFacManager.BringToFront()

                Catch ex As Exception
                End Try

                _frmAppFacManager = Nothing

            End If

        End If

        Try
            DTAppFactoryManager = Nothing
        Catch ex As Exception

        End Try


    End Sub

    Public Shared Sub ValidateAppWHCM(Optional currentIndex As Integer = 0)

        If LoadWareHouseManagerApproved() > 0 Then
            If ClsService.StateWHCM = False Then
                If _frmAppWHManager Is Nothing Then
                    _frmAppWHManager = New wWHManegerApproved
                ElseIf _frmAppWHManager.IsDisposed Then
                    _frmAppWHManager = New wWHManegerApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppWHManager)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppWHManager.Name.ToString.Trim, _frmAppWHManager)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try

                    ClsService.StateWHCM = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppWHManager.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppWHManager.Show()
                    _frmAppWHManager.BringToFront()

                Catch ex As Exception
                End Try

                _frmAppWHManager = Nothing

            End If

        End If


    End Sub

    Public Shared Sub ValidateMerManagerApp(Optional currentIndex As Integer = 0)

        If LoadMerManagerApproved() > 0 Then
            If ClsService.StateMerChandiseManegerShow = False Then
                If _frmMerManager Is Nothing Then
                    _frmMerManager = New wMerChandiserManagerApproved
                ElseIf _frmMerManager.IsDisposed Then
                    _frmMerManager = New wMerChandiserManagerApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmMerManager)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmMerManager.Name.ToString.Trim, _frmMerManager)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try

                    ClsService.StateMerChandiseManegerShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmMerManager.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmMerManager.Show()
                    _frmMerManager.BringToFront()

                Catch ex As Exception
                End Try

                _frmMerManager = Nothing

            End If

        End If


    End Sub


    Public Shared Sub ValidateLineLeaderApp(Optional currentIndex As Integer = 0)

        If LoadSewingLineLeaderApproved() > 0 Then
            If ClsService.StateLineLeaderShow = False Then

                If _frmSeingLineLeaderApp Is Nothing Then
                    _frmSeingLineLeaderApp = New wLineLeaderApproved
                ElseIf _frmSeingLineLeaderApp.IsDisposed Then
                    _frmSeingLineLeaderApp = New wLineLeaderApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmSeingLineLeaderApp)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmSeingLineLeaderApp.Name.ToString.Trim, _frmSeingLineLeaderApp)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try

                    ClsService.StateLineLeaderShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmSeingLineLeaderApp.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmSeingLineLeaderApp.Show()
                    _frmSeingLineLeaderApp.BringToFront()

                Catch ex As Exception
                End Try

                _frmSeingLineLeaderApp = Nothing

            End If

        End If


    End Sub

    Public Shared Sub ValidateQAFinalLeaderApp(Optional currentIndex As Integer = 0)

        If LoadQAFinalLeaderApproved() > 0 Then
            If ClsService.StateQAFLineLeaderShow = False Then

                If _frmQAFinalLeaderApp Is Nothing Then
                    _frmQAFinalLeaderApp = New wQAFinalLeaderApproved
                ElseIf _frmQAFinalLeaderApp.IsDisposed Then
                    _frmQAFinalLeaderApp = New wQAFinalLeaderApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmQAFinalLeaderApp)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmQAFinalLeaderApp.Name.ToString.Trim, _frmQAFinalLeaderApp)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try

                    ClsService.StateQAFLineLeaderShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmQAFinalLeaderApp.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmQAFinalLeaderApp.Show()
                    _frmQAFinalLeaderApp.BringToFront()

                Catch ex As Exception
                End Try

                _frmQAFinalLeaderApp = Nothing

            End If

        End If


    End Sub

    Public Shared Function LoadDcoumentationApprove() As Integer
        Try
            Dim _oDt As DataTable = wDCFactoryApproved.LoadDcoumentation
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared _frmAppDocumentation As wDCFactoryApproved = Nothing
    Public Shared Sub ValidateAppDocumentation(Optional currentIndex As Integer = 0)

        If LoadDcoumentationApprove() > 0 Then
            If ClsService.StateDocument = False Then
                If _frmAppDocumentation Is Nothing Then
                    _frmAppDocumentation = New wDCFactoryApproved
                ElseIf _frmAppDocumentation.IsDisposed Then
                    _frmAppDocumentation = New wDCFactoryApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppDocumentation)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppDocumentation.Name.ToString.Trim, _frmAppDocumentation)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try

                    ClsService.StateDocument = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppDocumentation.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppDocumentation.Show()
                    _frmAppDocumentation.BringToFront()

                Catch ex As Exception
                End Try

                _frmAppDocumentation = Nothing

            End If

        End If

    End Sub

    Friend Function Update_DocApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With
            _Cmd = "Select FTCmpCode From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTDCUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd)
            Dim _Qtr As String = GetCmpApp(_oDt, StateApprove)

            _Cmd = "Select Top 1 FNHSysCmpId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTDCUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            Dim UserCmpId As Integer = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_DOC, 0)


            For Each R As DataRow In _dt.Select("FTSelect='1'")
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                _Cmd &= vbCrLf & " Set FTInsTime=FTInsTime"
                _Cmd &= vbCrLf & "" & _Qtr
                If CInt("0" & R!FNHSysCmpId.ToString) = UserCmpId Then
                    _Cmd &= vbCrLf & ",FTOwnerStateApprove='" & StateApprove & "'"
                    _Cmd &= vbCrLf & ", FTOwnerApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDOwnerApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTOwnerApproveTime=" & HI.UL.ULDate.FormatTimeDB
                End If
                _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"
                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
                '_FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                'If StateApprove = "1" Then
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                '    _Cmd &= ",'Document Control approve','Dear " & R!FTSandApproveBy.ToString & " Document Control Factory " & vbCr & "Document Control Factory Approve document Successfully.  " & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'Else
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                '    _Cmd &= ",'Document Control Reject','Dear " & R!FTSandApproveBy.ToString & " Document Control Factory " & vbCr & "I'm Reject document   Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'End If
            Next
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

    Private Function Update_ManagerAppprove_Document(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                _Cmd &= vbCrLf & " Set FTInsTime=FTInsTime"
                _Cmd &= vbCrLf & " ,  FTStateManagerApp='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FDManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ", FTManagerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"
                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                If StateApprove = "1" Then

                    _Cmd = "UPDATE A "
                    _Cmd &= vbCrLf & "Set A.FBDocument = T.FBDocument "
                    _Cmd &= vbCrLf & ",A.FTStateManagerApp='" & StateApprove & "'"
                    _Cmd &= vbCrLf & ",A.FDManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",A.FTManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",A.FTManagerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle AS A INNER JOIN "
                    _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument AS T ON A.FNHSysDocNameId = T.FNHSysDocNameId "
                    _Cmd &= vbCrLf & "Where  T.FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

                '_FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                'If StateApprove = "1" Then
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                '    _Cmd &= ",'Director approve','Dear " & R!FTSandApproveBy.ToString & " Director Factory " & vbCr & "Director  Approve document Successfully.  " & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'Else
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                '    _Cmd &= ",'Director Reject','Dear " & R!FTSandApproveBy.ToString & " Director Factory " & vbCr & " Reject document   Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function GetCmpApp(_oDt As DataTable, ByVal State As String) As String
        Try
            Dim _Cmd As String = ""
            For Each R As DataRow In _oDt.Rows
                Select Case R!FTCmpCode.ToString.ToUpper
                    Case "HT91".ToUpper
                        _Cmd &= vbCrLf & ",FT91StateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FT91ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FD91ApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FT91ApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HT70".ToUpper
                        _Cmd &= vbCrLf & ",FT70StateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FT70ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FD70ApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FT70ApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTSP".ToUpper
                        _Cmd &= vbCrLf & ",FTSPStateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTSPApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDSPApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTSPApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTC1".ToUpper
                        _Cmd &= vbCrLf & ",FTC1StateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTC1ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDC1ApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTC1ApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTC2".ToUpper
                        _Cmd &= vbCrLf & ",FTC2StateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTC2ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDC2ApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTC2ApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTC3".ToUpper
                        _Cmd &= vbCrLf & ",FTC3StateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTC3ApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDC3ApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTC3ApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTSR".ToUpper
                        _Cmd &= vbCrLf & ",FTSRStateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTSRApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDSRApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTSRApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTVN".ToUpper
                        _Cmd &= vbCrLf & ",FTVNStateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTVNApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDVNApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTVNApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTFG".ToUpper
                        _Cmd &= vbCrLf & ",FTFGStateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTFGApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDFGApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTFGApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    Case "HTCD".ToUpper
                        _Cmd &= vbCrLf & ",FTCDStateApprove='" & State & "'"
                        _Cmd &= vbCrLf & ",FTCDApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDCDApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTCDApproveTime=" & HI.UL.ULDate.FormatTimeDB
                End Select
            Next
            Return _Cmd
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function LoadDataAppPRInfo() As Integer
        Try
            Dim _oDt As DataTable = wPRFactoryApproved.LoadDataApproveInfo
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared _frmAppPR As wPRFactoryApproved = Nothing
    Public Shared Sub ValidateAppPR(Optional currentIndex As Integer = 0)

        If LoadDataAppPRInfo() > 0 Then
            If ClsService.StateAppPR = False Then
                If _frmAppPR Is Nothing Then
                    _frmAppPR = New wPRFactoryApproved
                ElseIf _frmAppPR.IsDisposed Then
                    _frmAppPR = New wPRFactoryApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppPR)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppPR.Name.ToString.Trim, _frmAppPR)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try
                    ClsService.StateAppPR = True
                    Try
                        If currentIndex > 0 Then
                            _frmAppPR.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If
                    Catch ex As Exception
                    End Try

                    _frmAppPR.Show()
                    _frmAppPR.BringToFront()

                Catch ex As Exception
                End Try

                _frmAppPR = Nothing

            End If

        End If

    End Sub

    Friend Function Update_PRApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With


            For Each R As DataRow In _dt.Select("FTSelect='1'")
                _Cmd = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request"
                _Cmd &= vbCrLf & " Set FTStateApp='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FTAppName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FTAppDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTAppTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseNo.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                '_FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                'If StateApprove = "1" Then
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseBy.ToString) & "'"
                '    _Cmd &= ",'PR Approved',' " & R!FTPRPurchaseBy.ToString & "  " & vbCr & "Document Control Factory Approve document Successfully.  " & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'Else
                '    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages"
                '    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                '    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                '    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTPRPurchaseBy.ToString) & "'"
                '    _Cmd &= ",'PR Rejected',' " & R!FTPRPurchaseBy.ToString & "  " & vbCr & "I'm Reject document   Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                '    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                '    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                'End If

            Next


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



#Region "Approved Order Costing...."

    Private Shared _StateOrderCost As Boolean = False
    Public Shared Property StateOrderCost As Boolean
        Get
            Return _StateOrderCost
        End Get
        Set(value As Boolean)
            _StateOrderCost = value
        End Set
    End Property


    Public Shared Function LoadOrderCostApprove() As Integer
        Try
            Dim _oDt As DataTable = wOrderCostApproved.LoadDcoumentation
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared _frmAppOrderCosting As wOrderCostApproved = Nothing
    Public Shared Sub ValidateAppOrderCost(Optional currentIndex As Integer = 0)

        If Application.OpenForms.OfType(Of wOrderCostApproved).Any Then
            Exit Sub
        End If

        If LoadOrderCostApprove() > 0 Then
            If ClsService.StateOrderCost = False Then
                If _frmAppOrderCosting Is Nothing Then
                    _frmAppOrderCosting = New wOrderCostApproved
                ElseIf _frmAppOrderCosting.IsDisposed Then
                    _frmAppOrderCosting = New wOrderCostApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppOrderCosting)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppOrderCosting.Name.ToString.Trim, _frmAppOrderCosting)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu
                Try

                    ClsService.StateOrderCost = True

                    Try
                        If currentIndex > 0 Then
                            _frmAppOrderCosting.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If
                    Catch ex As Exception
                    End Try
                    _frmAppOrderCosting.Show()
                    _frmAppOrderCosting.BringToFront()
                Catch ex As Exception
                End Try
                _frmAppOrderCosting = Nothing
            End If
        End If

    End Sub

    Friend Function Update_OrderCostApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _MailTo As String = ""
            Dim _FTMailId As Long

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                _Cmd = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost "
                Select Case R!FTStateApp.ToString
                    Case "1"

                        _Cmd &= vbCrLf & " Set FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTInspectorName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTInspectorAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTInspectorAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                    Case "2"

                        _Cmd &= vbCrLf & " Set FTStateFactoryManagerApp='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTFactoryManagerName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTFactoryManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTFactoryManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                    Case "3"

                        _Cmd &= vbCrLf & " Set FTStateApprovedApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTApprovedName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTApprovedAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTApprovedAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " , FTStateFactoryManagerApp='" & StateApprove & "'"
                        _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                    Case "4"

                        _Cmd &= vbCrLf & " Set FTStateDirectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTDirectorName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTDirectorAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTDirectorAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " , FTStateApprovedApp ='" & StateApprove & "'"
                        '_Cmd &= vbCrLf & ", FTApprovedName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '_Cmd &= vbCrLf & ", FTApprovedAppDate=" & HI.UL.ULDate.FormatDateDB
                        '_Cmd &= vbCrLf & ", FTApprovedAppTime=" & HI.UL.ULDate.FormatTimeDB

                        _Cmd &= vbCrLf & " , FTStateFactoryManagerApp='" & StateApprove & "'"
                        '_Cmd &= vbCrLf & ", FTFactoryManagerName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '_Cmd &= vbCrLf & ", FTFactoryManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                        '_Cmd &= vbCrLf & ", FTFactoryManagerAppTime=" & HI.UL.ULDate.FormatTimeDB

                        _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                End Select
                _Cmd &= vbCrLf & " WHERE FTOrderNo  in (Select FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON O.FNHSysCmpId = C.FNHSysCmpId  "
                _Cmd &= vbCrLf & " Where C.FTCmpCode = '" & R!FTCmpName.ToString & "' ) "
                _Cmd &= vbCrLf & " and FTOrderNo in (Select FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice where LEFT( FDInvoiceDate ,7) ='" & Right(R!FDInvoiceDate.ToString, 4) & "/" & Left(R!FDInvoiceDate.ToString, 2) & "')"
                HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Cmd = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice "
                Select Case R!FTStateApp.ToString
                    Case "1"
                        _Cmd &= vbCrLf & " Set FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTInspectorName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTInspectorAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTInspectorAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                    Case "2"
                        _Cmd &= vbCrLf & " Set FTStateFactoryManagerApp='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTFactoryManagerName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTFactoryManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTFactoryManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                    Case "3"
                        _Cmd &= vbCrLf & " Set FTStateApprovedApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTApprovedName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTApprovedAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTApprovedAppTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & " , FTStateFactoryManagerApp='" & StateApprove & "'"
                        _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                    Case "4"
                        _Cmd &= vbCrLf & " Set FTStateDirectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ", FTDirectorName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ", FTDirectorAppDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ", FTDirectorAppTime=" & HI.UL.ULDate.FormatTimeDB

                        _Cmd &= vbCrLf & " , FTStateApprovedApp ='" & StateApprove & "'"
                        '_Cmd &= vbCrLf & ", FTApprovedName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '_Cmd &= vbCrLf & ", FTApprovedAppDate=" & HI.UL.ULDate.FormatDateDB
                        '_Cmd &= vbCrLf & ", FTApprovedAppTime=" & HI.UL.ULDate.FormatTimeDB

                        _Cmd &= vbCrLf & " , FTStateFactoryManagerApp='" & StateApprove & "'"
                        '_Cmd &= vbCrLf & ", FTFactoryManagerName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        '_Cmd &= vbCrLf & ", FTFactoryManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                        '_Cmd &= vbCrLf & ", FTFactoryManagerAppTime=" & HI.UL.ULDate.FormatTimeDB

                        _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                        _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                End Select
                _Cmd &= vbCrLf & " WHERE case when isnull(FTOrderNoRef,'') = '' then  FTOrderNo   else FTOrderNoRef end  in (Select FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON O.FNHSysCmpId = C.FNHSysCmpId  "
                _Cmd &= vbCrLf & " Where C.FTCmpCode = '" & R!FTCmpName.ToString & "' ) "
                _Cmd &= vbCrLf & " and  LEFT( FDInvoiceDate ,7) ='" & Right(R!FDInvoiceDate.ToString, 4) & "/" & Left(R!FDInvoiceDate.ToString, 2) & "'"

                HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            Next

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




#End Region


#Region "Leave Approve"


    Private Shared _StateLeaveApproved As Boolean = False
    Public Shared Property StateLeaveApproved As Boolean
        Get
            Return _StateLeaveApproved
        End Get
        Set(value As Boolean)
            _StateLeaveApproved = value
        End Set
    End Property


    Public Shared Function LoadLeaveApprove() As Integer
        Try
            Dim _oDt As DataTable = wLeaveApproved.LoadLeaveWaitApp
            Return _oDt.Rows.Count
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared _frmAppLeaveApproved As wLeaveApproved = Nothing
    Public Shared Sub ValidateAppEmpleave(Optional currentIndex As Integer = 0)
        If LoadLeaveApprove() > 0 Then
            If ClsService.StateLeaveApproved = False Then
                If _frmAppLeaveApproved Is Nothing Then
                    _frmAppLeaveApproved = New wLeaveApproved
                ElseIf _frmAppLeaveApproved.IsDisposed Then
                    _frmAppLeaveApproved = New wLeaveApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppLeaveApproved)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppLeaveApproved.Name.ToString.Trim, _frmAppLeaveApproved)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu
                Try
                    ClsService.StateLeaveApproved = True
                    Try
                        If currentIndex > 0 Then
                            _frmAppLeaveApproved.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If
                    Catch ex As Exception
                    End Try
                    _frmAppLeaveApproved.Show()
                    _frmAppLeaveApproved.BringToFront()
                Catch ex As Exception
                End Try
                _frmAppLeaveApproved = Nothing
            End If
        End If
    End Sub

    Friend Function Update_SectApproveEmpLeave(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long

            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1' and FTStateType='0'")

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily"
                _Cmd &= vbCrLf & " Set FTDirApproveState='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FTDirApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDDirApproveDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTDirApproveTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " WHERE FNHSysEmpId='" & HI.UL.ULF.rpQuoted(R!FNHSysEmpId.ToString) & "'"
                _Cmd &= vbCrLf & "and FTStartDate='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR) = False Then
                    Return False
                End If

                'If StateApprove = "1" Then
                '    _Cmd = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
                '    _Cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpId.ToString) & " "
                '    _Cmd &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                '    _Cmd &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"
                '    _Cmd &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("FNLeaveDay", R!FTLeaveType.ToString) & "'"
                '    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)

                '    ApproveDataLeave(R!FTEndDate.ToString, R!FTStartDate.ToString, R!FTLeaveType.ToString, R!FNLeaveTotalTime.ToString, R!FNLeaveTotalTimeMin.ToString, R!FTLeavePay.ToString _
                '                     , Val(R!FNHSysEmpId.ToString), R!FTStateMedicalCertificate.ToString, R!FTStaLeaveDay.ToString, R!FTLeaveStartTime.ToString, R!FTLeaveEndTime.ToString, R!FTStaCalSSO.ToString _
                '                     , R!FNLeaveTotalDay.ToString, R!FTStateDeductVacation.ToString)
                'End If

            Next

            For Each R As DataRow In _dt.Select("FTSelect='1' and FTStateType='1'")

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily"
                _Cmd &= vbCrLf & " Set FTMngApproveState='" & StateApprove & "'"

                _Cmd &= vbCrLf & ", FTMngApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDMngApproveDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTMngApproveTime=" & HI.UL.ULDate.FormatTimeDB

                If R!FTUserNameMngFac.ToString = "" Then
                    _Cmd &= vbCrLf & ", FTDirApproveState='" & StateApprove & "'"
                    _Cmd &= vbCrLf & ", FTDirApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FDDirApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTDirApproveTime=" & HI.UL.ULDate.FormatTimeDB
                End If

                _Cmd &= vbCrLf & " WHERE FNHSysEmpId='" & HI.UL.ULF.rpQuoted(R!FNHSysEmpId.ToString) & "'"
                _Cmd &= vbCrLf & "and FTStartDate='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR) = False Then
                    Return False
                End If

            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ApproveDataLeave(ByVal _EndDate As String, _StartDate As String, _LeaveType As String, _NetTime As Double, _Totaltime As Double,
                                      _LeavePay As String, _EmpId As Integer, _StateMedicalCertificate As String, _LeaveDay As String,
                                      _StartTime As String, _EndTime As String, _StateSocial As String, _LeaveTotalDay As Integer, _StateDeductVacation As String) As Boolean
        Try
            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(_EndDate)
            Dim _NextProcDate As String = ""
            Dim nNextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(_StartDate)

            Dim _TotalHour As Double = 0
            Dim _FNTotalMonute As Double = 0
            Dim _FNTotalPayHour As Double = 0
            Dim _FNTotalPayMonute As Double = 0
            Dim _FNTotalNotPayHour As Double = 0
            Dim _FNTotalNotPayMonute As Double = 0
            Dim _TmpTotalHour As Double = 0
            Dim _TmpFNTotalMonute As Double = 0
            Dim _TmpFNTotalPayHour As Double = 0
            Dim _TmpFNTotalPayMonute As Double = 0
            Dim _TmpFNTotalNotPayHour As Double = 0
            Dim _TmpFNTotalNotPayMonute As Double = 0
            Dim _dtWeekend As DataTable
            Dim _dtHoliday As DataTable
            Dim _SkipProcess As Boolean
            Dim _Qry As String
            Dim _LeaveCode As String = HI.TL.CboList.GetListValue("FNLeaveDay", _LeaveType)
            Dim _WeekEnd As Integer
            Dim _LeavePragNentPay As Integer = 0
            Dim _LeavePragNentNotPay As Boolean = False
            Dim _EmpTypeWeekly As DataTable
            Dim _EmpTypeId As Integer = 0

            _TmpTotalHour = CDbl(Format(Val(_NetTime), "0.00"))
            _TmpFNTotalMonute = _Totaltime

            If (_LeavePay = "1") Then
                _TmpFNTotalPayHour = _TmpTotalHour
                _TmpFNTotalPayMonute = _TmpFNTotalMonute
            Else
                _TmpFNTotalNotPayHour = _TmpTotalHour
                _TmpFNTotalNotPayMonute = _TmpFNTotalMonute
            End If

            _Qry = "Select Top 1 FNHSysEmpTypeId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(_EmpId) & " "
            _EmpTypeId = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "' "
            _Qry &= vbCrLf & "  AND FNHSysEmpTypeId=" & Integer.Parse(Val(_EmpTypeId)) & " "

            _EmpTypeWeekly = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
            _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  As W WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(_EmpId) & " "
            _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtWeekend.Rows.Count <= 0 Then
            Else
                _EmpTypeWeekly.Rows.Clear()
            End If

            _Qry = "SELECt   FDHolidayDate   "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) WHERE FTStateActive='1' "
            _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            _Qry = "  SELECT   TOP 1   M.FNHSysShiftID"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
            _Qry &= vbCrLf & "   WHERE M.FNHSysEmpID=" & Val(_EmpId) & " "

            Dim _EmpOrgShift As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
            Dim _EmpShift As String = _EmpOrgShift
            Dim _EmpPgmCode As String
            Dim _TotalWorkHour As Double

            If _LeaveCode = 97 Then
                _Qry = "Select Top 1 FNLeavePay "
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId =" & Val(_EmpTypeId) & " "
                _Qry &= vbCrLf & " AND FTLeaveCode ='" & _LeaveCode & "' "
                _LeavePragNentPay = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
                _Qry = "   SELECT        COUNT(FTDateTrans) AS FNPayDay"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE        (FTLeaveType ='" & _LeaveCode & "')"
                _Qry &= vbCrLf & " AND (FNHSysEmpID =" & Val(_EmpId) & " ) "
                _Qry &= vbCrLf & " AND (FTDateTrans < N'" & _NextProcDate & "')"
                _Qry &= vbCrLf & " AND (FNTotalPayMinute > 0) "
                _LeavePragNentPay = _LeavePragNentPay - Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
            End If


            Do While _NextProcDate <= _EndProcDate
                _EmpPgmCode = ""
                _EmpPgmCode = ""
                _TotalWorkHour = 8
                _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
                _TotalHour = _TmpTotalHour
                _FNTotalMonute = _TmpFNTotalMonute

                If (_LeavePay = "1") Then
                    _FNTotalPayHour = _TotalHour
                    _FNTotalPayMonute = _FNTotalMonute
                Else
                    _FNTotalNotPayHour = _TotalHour
                    _FNTotalNotPayMonute = _FNTotalMonute
                End If

                _SkipProcess = False
                _LeavePragNentNotPay = False

                If Not (_StateMedicalCertificate = "0") Then
                Else
                    For Each Rday As DataRow In _dtWeekend.Rows

                        If Rday.Item(_WeekEnd - 1).ToString = "1" Then
                            _SkipProcess = True
                        End If
                        Exit For
                    Next
                    If _SkipProcess = False Then
                        For Each Dr As DataRow In _dtHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _SkipProcess = True
                        Next
                    End If

                    If _SkipProcess = False Then
                        For Each Dr As DataRow In _EmpTypeWeekly.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _SkipProcess = True
                        Next
                    End If
                End If

                If Not (_SkipProcess) Then

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave(FTInsUser, FTInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,FNHSysEmpID,FTDateTrans,FTLeaveType"
                    _Qry &= vbCrLf & ",FNTotalHour,FNTotalMinute,FNTotalPayHour,FNTotalPayMinute"
                    _Qry &= vbCrLf & ",FNTotalNotPayHour,FNTotalNotPayMinute,FTLeaveStartTime,FTLeaveEndTime,FTStaCalSSO,FTStaLeaveDay"
                    _Qry &= vbCrLf & ",FNLeaveTotalDay,FTStateMedicalCertificate,FTStateDeductVacation)"
                    _Qry &= vbCrLf & "  SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Val(_EmpId) & ",'" & _NextProcDate & "' "
                    _Qry &= vbCrLf & " ,'" & _LeaveCode & "'"
                    _Qry &= vbCrLf & " ," & _TotalHour & ""
                    _Qry &= vbCrLf & " ," & _FNTotalMonute & ""

                    If (_LeaveCode = "97" And (_LeavePragNentPay <= 0 Or _LeavePragNentNotPay)) And (_LeavePay = "1") Then
                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ," & _TotalHour & ""
                        _Qry &= vbCrLf & " ," & _FNTotalMonute & ""
                    Else
                        _Qry &= vbCrLf & " ," & _FNTotalPayHour & ""
                        _Qry &= vbCrLf & " ," & _FNTotalPayMonute & ""
                        _Qry &= vbCrLf & " ," & _FNTotalNotPayHour & ""
                        _Qry &= vbCrLf & " ," & _FNTotalNotPayMonute & ""
                    End If

                    _Qry &= vbCrLf & " ,'" & _StartTime & "'"
                    _Qry &= vbCrLf & " ,'" & _EndTime & "'"
                    _Qry &= vbCrLf & " ,'" & _StateSocial & "'"
                    _Qry &= vbCrLf & " ,'" & HI.TL.CboList.GetListValue("FNLeaveDay", _LeaveDay) & "'"
                    _Qry &= vbCrLf & "," & _LeaveTotalDay & " "
                    _Qry &= vbCrLf & ",'" & _StateMedicalCertificate & "'"
                    _Qry &= vbCrLf & " ,'" & _StateDeductVacation & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    If _LeaveCode = "97" And (_LeavePay = "1") Then
                        If Not (_LeavePragNentNotPay) Then
                            _LeavePragNentPay = _LeavePragNentPay - 1
                        End If
                    End If

                End If

                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, _EmpId, _NextProcDate, _NextProcDate)

                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

            Loop
            HI.HRCAL.Calculate.DisposeObject()
            _dtWeekend.Dispose()
            _dtHoliday.Dispose()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region


#Region "Asset Approve"

    '-----------ขา2 ไม่ได้ใช้
    Public Shared Function LoadogcTFIXEDTPurchase() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = "SELECT  isnull(A.FTStateSuperVisorApp,0) as FTStateApproved, A.FTPurchaseNo,"
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

            _str &= Environment.NewLine & " ISNULL(AP.FTManagerName,'') as FTUserName,"

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                Case HI.ST.Lang.eLang.TH
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameTH,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
                Case Else
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
            End Select

            _str &= Environment.NewLine & "  A.FNFixedAssetType"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase AS A   LEFT JOIN "

            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON A.FTPurchaseBy = B.FTUserName  LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E with (nolock) ON B.FNHSysEmpID=E.FNHSysEmpID LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TASMAssetApprovedPO AS AP with (nolock) ON A.FNFixedAssetType= AP.FNFixedAssetType LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysDeliveryId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId " 'left join"
            _str &= Environment.NewLine & " WHERE (AP.FTManagerName = '" & HI.ST.UserInfo.UserName & "')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '0')"
            _str &= Environment.NewLine & " Order by A.FTPurchaseBy, a.FDPurchaseDate"




            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_FIXED)

            If _dt.Rows.Count > 0 Then
                _CountAppAsset = _dt.Rows.Count
                Return _dt
            Else
                _CountAppAsset = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    'PO รายการ ขา2 Dir
    Public Shared Function LoadogcTFIXEDTPurchaseDi() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

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
            _str &= Environment.NewLine & " L6.FTWHAssetCode AS FTDeliveryCode,l7.FTCurCode,L1.FTWHAssetCode AS FTDeliveryCode,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " ISNULL(AP.FTUserName,'') as FTUserName,"


            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.EN
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTWHAssetNameEN,'') as FTDeliveryDesc,"

                Case HI.ST.Lang.eLang.TH
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameTH,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTWHAssetNameTH,'') as FTDeliveryDesc,"

                Case Else
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"

            End Select

            _str &= Environment.NewLine & "  A.FNFixedAssetType,ISNULL(AA.FTAssetCode,PP.FTAssetPartCode)AS FTAssetCode,ISNULL(PD.FNQuantity,PSD.FNQuantity) AS FTQuantityinfo,ISNULL(PSD.FTDescription,'')AS FTDescriptionService,A.FTPoTypeState"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase AS A   left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD ON A.FTPurchaseNo=PD.FTPurchaseNo left join "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService_Detail AS PSD ON A.FTPurchaseNo=PSD.FTPurchaseNo left join "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS AA with (nolock) ON PD.FNHSysFixedAssetId=AA.FNHSysFixedAssetId OR PSD.FNHSysFixedAssetId=AA.FNHSysFixedAssetId  left join "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS PP with (nolock) ON PD.FNHSysFixedAssetId=PP.FNHSysAssetPartId left join "

            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TASMAssetConfigLevel AS AP with (nolock) ON A.FNFixedAssetType= AP.FNFixedAssetType AND A.FNHSysCmpId=AP.FNHSysCmpId AND A.FNPONetAmt>=AP.FNStartQty AND A.FNPONetAmt<=AP.FNEndQty LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouseAsset as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysWHAssetId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRunAsset as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouseAsset as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysWHAssetId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId "
            '_str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp as D with (nolock) ON AP.FNHSysDirectorGrpId=D.FNHSysDirectorGrpId left join "
            '_str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo. TCNMDirectorGrpUser as DU with (nolock) ON D.FNHSysDirectorGrpId=DU.FNHSysDirectorGrpId"

            _str &= Environment.NewLine & " WHERE (AP.FTUserName  = '" & HI.ST.UserInfo.UserName & "')"
            _str &= Environment.NewLine & "  AND (a.FTStateSendApp = '1')   AND (a.FTStateManagerApp = '0') AND (AP.FTStateFactory='1')"
            _str &= Environment.NewLine & " Order by A.FTPurchaseBy, a.FDPurchaseDate"


            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_FIXED)

            If _dt.Rows.Count > 0 Then
                _CountAppDirector = _dt.Rows.Count
                Return _dt
            Else
                _CountAppDirector = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function



    'ขึ้นรายการPR ให้ ผู้ตรวจสอบ(ผจก)เซ็น
    Public Shared Function LoadogcTFIXEDTPurchase_Request() As DataTable


        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = "SELECT  isnull(A.FTStateApp,0) as FTStateApproved, A.FTPRPurchaseNo,A.FNHSysCmpId,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPRPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPRPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPRPurchaseDate,1,4) as FDPRPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPRPurchaseBy,'') as FTPRPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTAppName,'') as FTAppName, "
            _str &= Environment.NewLine & " A.FTSuplCode,"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _str &= Environment.NewLine & " isnull(LD.FTNameTH,'') as FNPRState, A.FTSuplNameTH AS  FTSuplName, "
            Else
                _str &= Environment.NewLine & " isnull(LD.FTNameEN,'') as FNPRState, A.FTSuplNameEN AS  FTSuplName, "
            End If
            _str &= Environment.NewLine & " ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL(Convert(numeric(18,2),A.FNNetAmt),0) as FNNetAmt, ISNULL(Convert(numeric(18,2),A.FNQuantity),0) as FNQuantity,"
            _str &= Environment.NewLine & " ISNULL(U.FTManagerUserName,'') as FTUserName, A.FNFixedAssetType"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS A  with (nolock) LEFT OUTER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as U with (nolock) ON A.FTPRPurchaseBy =U.FTUserName  "
            ' _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel AS L with (nolock) ON A.FNFixedAssetType=L.FNFixedAssetType AND A.FNNetAmt>=L.FNStartQty  AND U.FTManagerUserName=L.FTUserName AND A.FNHSysCmpId=L.FNHSysCmpId"
            '_str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON  B.FNHSysTeamGrpId = C.FNHSysTeamGrpId " 'L.FTUserName=C.FTUserName" '  "
            _str &= Environment.NewLine & "LEFT OUTER JOIN  (select LD.FTNameTH,LD.FTNameEN,LD.FNListIndex"
            _str &= Environment.NewLine & " from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD with (nolock) "
            _str &= Environment.NewLine & " where LD.FTListName='FNPRState'  )as LD ON A.FNPRState=LD.FNListIndex"
            _str &= Environment.NewLine & " WHERE (U.FTManagerUserName = '" & HI.ST.UserInfo.UserName & "')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateApp = '0') AND (a.FTStateSafety <>'0') "
            _str &= Environment.NewLine & " Order by A.FTPRPurchaseBy, a.FDPRPurchaseDate"


            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MASTER)

            If _dt.Rows.Count > 0 Then
                _CountAppAssetPR = _dt.Rows.Count
                Return _dt
            Else
                _CountAppAssetPR = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    'ขึ้นรายการให้ Dir PR เห็น
    Public Shared Function LoadogcTFIXEDTPurchase_RequestDi() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable
            _str = ""
            _str = "SELECT  '0' as FTStateApproved , A.FTPRPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPRPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPRPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPRPurchaseDate,1,4) as FDPRPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPRPurchaseBy,'') as FTPRPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTAppName,'') as FTAppName, "
            _str &= Environment.NewLine & " ISNULL(A.FTRemark,'') as FTRemark,ISNULL(A.FNNetAmt,0) as FNNetAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPRGrandAmt,0) as FNPRGrandAmt, ISNULL(sum(A.FNQuantity),0) as FNQuantity,"
            _str &= Environment.NewLine & " ISNULL(L.FTUserName,'') as FTUserName"
            _str &= Environment.NewLine & " ,A.FTSuplCode,"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _str &= Environment.NewLine & "  A.FTSuplNameTH AS  FTSuplName, isnull(LD.FTNameTH,'') as FNPRState "
            Else
                _str &= Environment.NewLine & "  A.FTSuplNameEN AS  FTSuplName, isnull(LD.FTNameEN,'') as FNPRState "
            End If
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS A   INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel AS L ON A.FNFixedAssetType=L.FNFixedAssetType   AND A.FNHSysCmpId=L.FNHSysCmpId AND A.FNNetAmt >=L.FNStartQty AND A.FNNetAmt<=L.FNEndQty"
            _str &= Environment.NewLine & "LEFT OUTER JOIN  (select LD.FTNameTH,LD.FTNameEN,LD.FNListIndex"
            _str &= Environment.NewLine & " from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD with (nolock) "
            _str &= Environment.NewLine & " where LD.FTListName='FNPRState'  )as LD ON A.FNPRState=LD.FNListIndex"
            _str &= Environment.NewLine & " WHERE (L.FTUserName = '" & HI.ST.UserInfo.UserName & "')AND (L.FTStateDirector='1')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1')   and (L.FTStateDirector='1') and (a.FTStateApp='1') and (a.FTStateManagerApp='0') "
            _str &= Environment.NewLine & "  group by A.FTPRPurchaseNo,FDPRPurchaseDate,A.FTPRPurchaseBy,A.FTAppName,A.FNPRState,A.FNNetAmt,L.FTUserName,A.FTRemark,A.FNPRGrandAmt,A.FTSuplCode"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _str &= Environment.NewLine & "  ,LD.FTNameTH, A.FTSuplNameTH"
            Else
                _str &= Environment.NewLine & "  ,LD.FTNameEN, A.FTSuplNameEN "
            End If
            _str &= Environment.NewLine & " Order by A.FTPRPurchaseBy, a.FDPRPurchaseDate"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_FIXED)

            If _dt.Rows.Count > 0 Then
                _CountAppDirector = _dt.Rows.Count
                Return _dt
            Else
                _CountAppDirector = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function




    'เงื่อนไข ขา2 PO ไม่ได้ใช้แล้ว
    Public Shared Sub ValidateAppAssetPO(Optional currentIndex As Integer = 0)



        DTPurchaseAssetNo = Nothing


        DTPurchaseAssetNo = LoadogcTFIXEDTPurchase()



        If _CountAppAsset > 0 Then

            If ClsService.StateAssetShow = False Then
                If _frmAppAssetPO Is Nothing Then
                    _frmAppAssetPO = New wSupervisorApprovedAsset
                ElseIf _frmAppAssetPO.IsDisposed Then
                    _frmAppAssetPO = New wSupervisorApprovedAsset
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppAssetPO)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppAssetPO.Name.ToString.Trim, _frmAppAssetPO)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try
                    ClsService.StateAssetShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppAssetPO.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppAssetPO.Show()
                    _frmAppAssetPO.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppAssetPO = Nothing
            End If


        End If
        Try
            DTPurchaseAssetNo = Nothing
        Catch ex As Exception
        End Try

    End Sub

    'PO ขา2 กดอนุมัติ ไม่ได้ใช้แล้ว
    Friend Function Update_SupervisorApprovedAssetPO(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStateSuperVisorApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTSuperVisorName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSuperVisorAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTSuperVisorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If


                End If



            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    'PO ขา2 กด Reject ไม่ได้ใช้แล้ว
    Friend Function Update_SupervisorApprovedAssetPO(ByVal TempGrid As GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStateSuperVisorApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTSuperVisorName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSuperVisorAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTSuperVisorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    'End If


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If


                End If

            Next



            For j = 0 To _IntCount
                ' ส่งเมลกลับกรณี Reject

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่ง Mail ให้ตัวเอง
                If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
                    _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function
    'Po Dir ขา3 กดอนุมัติ
    Friend Function Update_SupervisorApprovedAssetPODi(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    If TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString() = "2" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    Else
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                        'End If
                    End If

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If




                End If



            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"

                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function


    'PO Reject ขา3 Dir
    Friend Function Update_SupervisorApprovedAssetPODi(ByVal TempGrid As GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    If TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString() = "2" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    Else
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                        'End If
                    End If

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If




                End If


            Next



            For j = 0 To _IntCount
                ' ส่งเมลกลับกรณี Reject

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"



                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่ง Mail ให้ตัวเอง
                If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
                    _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function



 


    'กรณีที่ ผจก กดส่ง Reject
    Friend Function Update_SupervisorApprovedAssetPR(ByVal TempGrid As GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1
                Dim _Pur As String = HI.Conn.SQLConn.GetField("Select FNNetAmt from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail As P where P.FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                Dim _End As String = HI.Conn.SQLConn.GetField("Select L.FNEndQty from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel L ON P.FNFixedAssetType=L.FNFixedAssetType where P.FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    If TempGrid.GetRowCellValue(i, "FNPRState").ToString() = "1" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    Else


                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                        _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & 0 & "'"
                        _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & ", [FTStateManagerApp] = '" & 0 & "'"
                        _Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & ", [FTStateRe] = '" & 1 & "'"
                        _Str &= Environment.NewLine & ", [FTStateSendApp] = '" & 0 & "'"
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                        _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"


                    End If


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                    End If


                End If

            Next



            For j = 0 To _IntCount
                ' ส่งเมลกลับกรณี Reject


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่ง Mail ให้ตัวเอง
                If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
                    _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function


    'เงื่อนไข เด้ง ผู้ตรวจสอบ(ผจก) PR
    Public Shared Sub ValidateAppAssetPR(Optional currentIndex As Integer = 0)



        DTPRPurchaseAssetNo = Nothing

        Dim _dtcheck As DataTable
        Dim _str As String
        _str = "SELECT PR.FNFixedAssetType,PR.FTPRPurchaseNo,U.FTManagerUserName as FTUserName"
        _str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS PR WITH(NOLOCK) LEFT OUTER JOIN "
        _str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U  WITH(NOLOCK) ON  PR.FTPRPurchaseBy =U.FTUserName  "
        _str &= vbCrLf & " WHERE U.FTManagerUserName='" & HI.ST.UserInfo.UserName & "'"

        _dtcheck = HI.Conn.SQLConn.GetDataTable(_str, Conn.DB.DataBaseName.DB_MASTER)



        For Each Rx As DataRow In _dtcheck.Rows

            Dim _USER As String = Rx!FTUserName.ToString


            DTPRPurchaseAssetNo = LoadogcTFIXEDTPurchase_Request()


            Exit For
        Next

        If _CountAppAssetPR > 0 Then

            If ClsService.StateAssetPRShow = False Then
                If _frmAppAssetPR Is Nothing Then
                    _frmAppAssetPR = New wSupervisorApprovedAssetPR
                ElseIf _frmAppAssetPR.IsDisposed Then
                    _frmAppAssetPR = New wSupervisorApprovedAssetPR
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppAssetPR)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppAssetPR.Name.ToString.Trim, _frmAppAssetPR)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu
                Try
                    ClsService.StateAssetPRShow = True

                    Try

                        If currentIndex = 0 Then
                            _frmAppAssetPR.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppAssetPR.Show()
                    _frmAppAssetPR.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppAssetPR = Nothing
            End If


        End If

        Try
            DTPRPurchaseAssetNo = Nothing
        Catch ex As Exception

        End Try
    End Sub




    Public Shared Sub ValidateAppAssetPRDi(Optional currentIndex As Integer = 0)

        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager


        DTPRPurchaseAssetNoDirector = Nothing
        DTPRPurchaseAssetNoDirector = LoadogcTFIXEDTPurchase_RequestDi()

        ' MessageBox.Show("มี PO  " & _CountApp & " ใบ")

        If _CountApp > 0 Then

            If ClsService.StateAssetPRDirectorShow = False Then
                If _frmAppAssetPR Is Nothing Then
                    '_frmApp = New wSupervisorApproved
                    _frmAppAssetPR = New wSupervisorApprovedAssetPR
                ElseIf _frmAppAssetPR.IsDisposed Then
                    _frmAppAssetPR = New wSupervisorApprovedAssetPR
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppAssetPR)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppAssetPR.Name.ToString.Trim, _frmAppAssetPR)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                ' _frmApp.StartPosition = FormStartPosition.CenterScreen

                Try
                    ClsService.StateAssetPRDirectorShow = True

                    Try

                        If currentIndex = 0 Then
                            _frmAppAssetPR.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppAssetPR.Show()
                    _frmAppAssetPR.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppAssetPR = Nothing
            End If


        End If

        Try
            DTPRPurchaseAssetNoDirector = Nothing
        Catch ex As Exception

        End Try
        ' MessageBox.Show("ไม่มี PO")
        '  Call HI.Service.ClsConvertPDF.Validate_PDF()

    End Sub


    'กดส่งอนุมัติ แล้ว ยอดซื้อ ไม่เกินที่ ผจกแผนกเซ้นได้

    Friend Function Update_SupervisorApprovedAssetPR(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    If TempGrid.GetRowCellValue(i, "FNPRState").ToString() = "1" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    Else
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                        _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & ", [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                        _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"

                        'End If

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If



                        If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                            If _atPurchaseNo(_IntCount) = String.Empty Then
                                _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                            Else
                                _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                            End If

                        Else
                            _IntCount = _IntCount + 1
                            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                        End If


                    End If



                End If

            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  '
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function


    'Dir กดส่งอนุมัติ
    Friend Function Update_SupervisorApprovedAssetPRDi(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = "1" Then

                    If TempGrid.GetRowCellValue(i, "FNPRState").ToString() = "1" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    Else
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                        _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                        _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"




                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                        If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                            If _atPurchaseNo(_IntCount) = String.Empty Then
                                _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                            Else
                                _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                            End If

                        Else
                            _IntCount = _IntCount + 1
                            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                        End If
                    End If

                End If



            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function




    'Dir กด Reject
    Friend Function Update_SupervisorApprovedAssetPRDi(ByVal TempGrid As GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    If TempGrid.GetRowCellValue(i, "FNPRState").ToString() = "1" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    Else

                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                        _Str &= Environment.NewLine & "SET   [FTStateManagerApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & ", [FTStateRe] = '" & 1 & "'"
                        _Str &= Environment.NewLine & ", [FTStateSendApp] = '" & 0 & "'"
                        _Str &= Environment.NewLine & ",[FTStateManagerApp] = '" & 0 & "'"
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                        _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"


                    End If


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                    End If


                End If

            Next



            For j = 0 To _IntCount
                ' ส่งเมลกลับกรณี Reject


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่ง Mail ให้ตัวเอง
                If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
                    _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function


    'PO ขา3 เงื่อนไข Dir
    Public Shared Sub ValidateAppAssetPODirector(Optional currentIndex As Integer = 0)


        DTPurchaseAssetNoDirector = Nothing
        DTPurchaseAssetNoDirector = LoadogcTFIXEDTPurchaseDi()



        If _CountAppDirector > 0 Then

            If ClsService.StateAssetDirectorShow = False Then
                If _frmAppAssetDirectorPO Is Nothing Then
                    _frmAppAssetDirectorPO = New wDirectorApprovedAsset
                ElseIf _frmAppAssetDirectorPO.IsDisposed Then
                    _frmAppAssetDirectorPO = New wDirectorApprovedAsset
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppAssetDirectorPO)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppAssetDirectorPO.Name.ToString.Trim, _frmAppAssetDirectorPO)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu


                Try
                    ClsService.StateAssetDirectorShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppAssetDirectorPO.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppAssetDirectorPO.Show()
                    _frmAppAssetDirectorPO.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppAssetDirectorPO = Nothing
            End If


        End If

        Try
            DTPurchaseAssetNoDirector = Nothing
        Catch ex As Exception

        End Try

    End Sub


    'เงื่อนไขเด้งPR กรณี ผจกแผนกและ ผจกโรงงาน เซ็นไม่ได้ ส่งต่อให้ Director
    'เงื่อนไขเด้งPR กรณี ผจกแผนกและ ผจกโรงงาน เซ็นไม่ได้ ส่งต่อให้ Director
    Public Shared Sub ValidateAppAssetPRDirector(Optional currentIndex As Integer = 0)



        DTPRPurchaseAssetNoDirector = Nothing
        DTPRPurchaseAssetNoDirector = LoadogcTFIXEDTPurchase_RequestDi()

        If _CountAppDirector > 0 Then

            If ClsService.StateAssetPRDirectorShow = False Then
                If _frmAppAssetDirectorPR Is Nothing Then
                    _frmAppAssetDirectorPR = New wDirectorApprovedAssetPR
                ElseIf _frmAppAssetDirectorPR.IsDisposed Then
                    _frmAppAssetDirectorPR = New wDirectorApprovedAssetPR
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppAssetDirectorPR)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppAssetDirectorPR.Name.ToString.Trim, _frmAppAssetDirectorPR)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                Try
                    ClsService.StateAssetPRDirectorShow = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppAssetDirectorPR.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppAssetDirectorPR.Show()
                    _frmAppAssetDirectorPR.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppAssetDirectorPR = Nothing
            End If



        End If

        Try
            DTPRPurchaseAssetNoDirector = Nothing
        Catch ex As Exception

        End Try


    End Sub

    Public Shared Sub ValidateAppAssetPRSafety(Optional currentIndex As Integer = 0)




        DTPRPurchaseAssetNoSafety = Nothing
        DTPRPurchaseAssetNoSafety = LoadogcTFIXEDTPurchase_RequestSa()
       

        If _CountAppAssetPRSa > 0 Then

            If ClsService.StateAssetPRShowSa = False Then
                If _frmAppAssetPRSa Is Nothing Then
                    _frmAppAssetPRSa = New wSafetyApprovedAssetPR
                ElseIf _frmAppAssetPRSa.IsDisposed Then
                    _frmAppAssetPRSa = New wSafetyApprovedAssetPR
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppAssetPRSa)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppAssetPRSa.Name.ToString.Trim, _frmAppAssetPRSa)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu
                Try
                    ClsService.StateAssetPRShowSa = True

                    Try

                        If currentIndex = 0 Then
                            _frmAppAssetPRSa.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppAssetPRSa.Show()
                    _frmAppAssetPRSa.BringToFront()
                Catch ex As Exception

                End Try
                _frmAppAssetPRSa = Nothing
            End If


        End If

        Try
            DTPRPurchaseAssetNoSafety = Nothing
        Catch ex As Exception

        End Try
    End Sub

    'ขึ้นรายการPR ให้ จป เซ็น
    Public Shared Function LoadogcTFIXEDTPurchase_RequestSa() As DataTable


        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = "SELECT  isnull(A.FTStateApp,0) as FTStateApproved, A.FTPRPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPRPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPRPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPRPurchaseDate,1,4) as FDPRPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPRPurchaseBy,'') as FTPRPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTAppName,'') as FTAppName, "
            _str &= Environment.NewLine & " isnull(LD.FTNameTH,'') as FNPRState,"
            _str &= Environment.NewLine & " ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL(Convert(numeric(18,2),A.FNNetAmt),0) as FNNetAmt, ISNULL(Convert(numeric(18,2),A.FNQuantity),0) as FNQuantity,"
            _str &= Environment.NewLine & " ISNULL(S.FTUserName,'') as FTSafetyName, A.FNFixedAssetType"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetSafety as S with (nolock)  "
            _str &= Environment.NewLine & "LEFT OUTER JOIN (SELECT A.FNFixedAssetType,A.FNHSysAssetTyped,A.FNHSysFixedAssetId,A.FTAssetNameTH"
            _str &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A "
            _str &= Environment.NewLine & "UNION "
            _str &= Environment.NewLine & "SELECT '1'AS FNFixedAssetType,A.FNHSysAssetPartTyped,A.FNHSysAssetPartId,A.FTAssetPartNameTH"
            _str &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS A)AS T ON S.FNFixedAssetType=T.FNFixedAssetType AND S.FNHSysAssetTyped=T.FNHSysAssetTyped"
            _str &= Environment.NewLine & "LEFT OUTER JOIN[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS A  with (nolock) ON T.FNFixedAssetType=A.FNFixedAssetType AND T.FNHSysFixedAssetId=A.FNHSysFixedAssetId "
            ' _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as U with (nolock) ON A.FTPRPurchaseBy =U.FTUserName  "
            '_str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel AS L with (nolock) ON A.FNFixedAssetType=L.FNFixedAssetType AND A.FNNetAmt>=L.FNStartQty  AND U.FTManagerUserName=L.FTUserName AND A.FNHSysCmpId=L.FNHSysCmpId "
            _str &= Environment.NewLine & "LEFT OUTER JOIN  (select LD.FTNameTH,LD.FNListIndex"
            _str &= Environment.NewLine & " from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD with (nolock) "
            _str &= Environment.NewLine & " where LD.FTListName='FNPRState'  )as LD ON A.FNPRState=LD.FNListIndex"
            _str &= Environment.NewLine & " WHERE (S.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1')AND (A.FTStateSafety='0') "
            _str &= Environment.NewLine & " Order by A.FTPRPurchaseBy, a.FDPRPurchaseDate"


            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MASTER)

            If _dt.Rows.Count > 0 Then
                _CountAppAssetPRSa = _dt.Rows.Count
                Return _dt
            Else
                _CountAppAssetPRSa = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    'กดส่งอนุมัติ เจ้าหน้าที่รักษาความปลอดภัย
    Friend Function Update_SafetyApprovedAssetPR(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1



                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    If TempGrid.GetRowCellValue(i, "FNPRState").ToString() = "1" Then
                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchaseService "
                        _Str &= Environment.NewLine & "SET  [FTStateSuperVisorApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSuperVisorName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSuperVisorAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSuperVisorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchaseService] "
                        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    Else

                        _Str = ""
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                        _Str &= Environment.NewLine & "SET  [FTStateSafety] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTSafetyName] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FTSafetyDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTSafetyTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                        _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"



                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                        If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                            If _atPurchaseNo(_IntCount) = String.Empty Then
                                _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                            Else
                                _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                            End If

                        Else
                            _IntCount = _IntCount + 1
                            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                        End If


                    End If

                End If

            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  '
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

#End Region


#Region "Sample Room"
    Public Shared Sub ValidateAppSMP(Optional currentIndex As Integer = 0)

        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager

        DTSMP = Nothing
        DTSMP = LoadogcSMP()

        ' MessageBox.Show("มี PO  " & _CountApp & " ใบ")

        If _CountAppSMP > 0 Then

            If ClsService.StateShowSMP = False Then
                If _frmAppSMP Is Nothing Then
                    '_frmApp = New wSupervisorApproved
                    _frmAppSMP = New wSMPIncentiveApproved
                ElseIf _frmAppSMP.IsDisposed Then
                    _frmAppSMP = New wSMPIncentiveApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppSMP)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmApp.Name.ToString.Trim, _frmAppSMP)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                ' _frmApp.StartPosition = FormStartPosition.CenterScreen

                Try
                    ClsService._StateShowSMP = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppSMP.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppSMP.Show()
                    _frmAppSMP.BringToFront()

                Catch ex As Exception

                End Try

                _frmAppSMP = Nothing

            End If


        End If

        Try
            DTSMP = Nothing
        Catch ex As Exception

        End Try
        ' MessageBox.Show("ไม่มี PO")
        '  Call HI.Service.ClsConvertPDF.Validate_PDF()

    End Sub


    Public Shared Sub ValidateAppSMPMGR(Optional currentIndex As Integer = 0)

        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager

        DTSMPMGR = Nothing
        DTSMPMGR = LoadogcSMPMGR()

        ' MessageBox.Show("มี PO  " & _CountApp & " ใบ")

        If _CountAppSMPMGR > 0 Then

            If ClsService.StateShowSMPMGR = False Then
                If _frmAppSMPMGR Is Nothing Then
                    '_frmApp = New wSupervisorApproved
                    _frmAppSMPMGR = New wSMPIncentiveManagerApproved
                ElseIf _frmAppSMPMGR.IsDisposed Then
                    _frmAppSMPMGR = New wSMPIncentiveManagerApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppSMPMGR)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"
                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppSMPMGR.Name.ToString.Trim, _frmAppSMPMGR)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu

                ' _frmApp.StartPosition = FormStartPosition.CenterScreen

                Try
                    ClsService._StateShowSMPMGR = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppSMPMGR.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppSMPMGR.Show()
                    _frmAppSMPMGR.BringToFront()

                Catch ex As Exception

                End Try

                _frmAppSMPMGR = Nothing

            End If


        End If

        Try
            DTSMPMGR = Nothing
        Catch ex As Exception

        End Try
        ' MessageBox.Show("ไม่มี PO")
        '  Call HI.Service.ClsConvertPDF.Validate_PDF()

    End Sub


    Public Shared Sub ValidateAppRDSam(Optional currentIndex As Integer = 0)

        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager
        _CountAppRDSam = 0
        DTAPPRDSAM = Nothing
        DTAPPRDSAM = LoadAppRDSam()

        ' MessageBox.Show("มี PO  " & _CountApp & " ใบ")

        If _CountAppRDSam > 0 Then

            If ClsService.StateShowRDSam = False Then

                If _frmAppRDSam Is Nothing Then
                    _frmAppRDSam = New wSMPIncentiveManagerApproved
                ElseIf _frmAppRDSam.IsDisposed Then
                    _frmAppRDSam = New wSMPIncentiveManagerApproved
                End If

                HI.TL.HandlerControl.AddHandlerObj(_frmAppRDSam)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = "mnuSecurity"

                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmAppRDSam.Name.ToString.Trim, _frmAppRDSam)
                Catch ex As Exception
                Finally
                End Try

                HI.ST.SysInfo.MenuName = _TmpMenu



                Try
                    ClsService.StateShowRDSam = True

                    Try

                        If currentIndex > 0 Then
                            _frmAppRDSam.Bounds = Screen.AllScreens(currentIndex).WorkingArea
                        End If

                    Catch ex As Exception
                    End Try

                    _frmAppRDSam.Show()
                    _frmAppRDSam.BringToFront()

                Catch ex As Exception

                End Try

                _frmAppRDSam = Nothing

            End If


        End If

        Try
            DTAPPRDSAM = Nothing
        Catch ex As Exception

        End Try
        ' MessageBox.Show("ไม่มี PO")
        '  Call HI.Service.ClsConvertPDF.Validate_PDF()

    End Sub

#End Region

End Class
