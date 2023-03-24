Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic

Public Class wCopyGTMSamOperation

#Region "Variable Declaration"
    Private Const _nTotalFactorySubOrderNo As Integer = 26
    Private _tSysDBName As String
    Private _tSysTableName As String
    Private __SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private _tFTOrderNo As String       '...เลขที่ใบสั่งผลิต
    Private _nFNHSysCmpId As Integer
    Private _nFNHSysCmpRunId As Integer
    Private _nFNHSysStyleId As Integer
    Private _tFNHSysCmpId As String     '...รหัสโรงงาน/บริษัท สาขา : Code
    Private _tFNHSysCmpRunId As String  '...รหัสเลข run เอกสาร : Code
    Private _tFNHSysStyleId As String   '...รหัสสไตล์ : Code
    Private tSql As String

    Private _wListCompleteCopyOrder As wListCompleteCopyOrder

    Private _oImage1 As System.Drawing.Image
    Private _oImage2 As System.Drawing.Image
    Private _oImage3 As System.Drawing.Image
    Private _oImage4 As System.Drawing.Image

    Private _FTImage1 As String
    Private _FTImage2 As String
    Private _FTImage3 As String
    Private _FTImage4 As String

#End Region

#Region "Property"


    Private _DescStyleNo As String = ""
    Public Property DescStyleNo As String
        Get
            Return _DescStyleNo
        End Get
        Set(value As String)
            _DescStyleNo = value
        End Set
    End Property

    Private _DescSeasonNo As String = ""
    Public Property DescSeasonNo As String
        Get
            Return _DescSeasonNo
        End Get
        Set(value As String)
            _DescSeasonNo = value
        End Set
    End Property

    Private _StyleId As Integer = 0
    Public Property StyleId As Integer
        Get
            Return _StyleId
        End Get
        Set(value As Integer)
            _StyleId = value
        End Set
    End Property

    Private _SeasonId As Integer = 0
    Public Property SeasonId As Integer
        Get
            Return _SeasonId
        End Get
        Set(value As Integer)
            _SeasonId = value
        End Set
    End Property


    Private _Process As Boolean = False
    Public Property Process As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property

#End Region

#Region "Procedure And Function"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Function SaveCopy() As Boolean
        Try

            Dim _Cmd As String = ""
            Dim State As Boolean = False
            Dim _Spls As HI.TL.SplashScreen
            _Spls = New HI.TL.SplashScreen("Copy Data...Please Wait ")
            Dim RevisedNo As Integer = 0
            Dim _tFTOrderNoDest As String = ""

            _Cmd = " Select TOP 1 FNHSysStyleId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam As A  "
            _Cmd &= vbCrLf & "  WHERE  A.FNHSysStyleId=" & Val(StyleId) & " And A.FNHSysSeasonId=" & Val(SeasonId) & "  "
            _Cmd &= vbCrLf & "  AND A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PLANNING, "") = "" Then

                _Cmd = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam ( FNHSysCmpId, FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay,"
                _Cmd &= vbCrLf & "   FNWorkingTimeMinuteDay, FNTargetPerDay, FTStateSendApp, FTStateSendAppBy, FTStateSendAppDate, FTStateSendAppTime, FTStateApp, FTStateAppBy, FTStateAppDate, FTStateAppTime, FTStateNewFromRD,"
                _Cmd &= vbCrLf & "   FTStateFromRDBy, FTStateFromRDDate, FTStateFromRDTime, FNGTMSam, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack)"
                _Cmd &= vbCrLf & " SELECT TOP 1  " & HI.ST.SysInfo.CmpID & " AS  FNHSysCmpId, FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, "
                _Cmd &= vbCrLf & "   FNWorkingTimeMinuteDay, FNTargetPerDay, FTStateSendApp, FTStateSendAppBy, FTStateSendAppDate, FTStateSendAppTime, FTStateApp, FTStateAppBy, FTStateAppDate, FTStateAppTime, FTStateNewFromRD, "
                _Cmd &= vbCrLf & "   FTStateFromRDBy, FTStateFromRDDate, FTStateFromRDTime, FNGTMSam, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack "
                _Cmd &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam As A  "
                _Cmd &= vbCrLf & "  WHERE  A.FNHSysStyleId=" & Val(StyleId) & " And A.FNHSysSeasonId=" & Val(SeasonId) & "  "
                _Cmd &= vbCrLf & "  AND A.FNHSysCmpId=0"

                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PLANNING)

            End If

            Try

                _Cmd = " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam_Detail where FNHSysStyleId=" & Val(StyleId) & " And FNHSysSeasonId=" & Val(SeasonId) & " And FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                _Cmd &= vbCrLf & " update A Set "
                _Cmd &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Cmd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & " ,FTRemark=B.FTRemark"
                _Cmd &= vbCrLf & ", FNSam=B.FNSam"
                _Cmd &= vbCrLf & ", FNOperater=B.FNOperater"
                _Cmd &= vbCrLf & ", FNCost=B.FNCost"
                _Cmd &= vbCrLf & ", FNMinuteHour=B.FNMinuteHour"
                _Cmd &= vbCrLf & ", FNProdPersonPerDay=B.FNProdPersonPerDay"
                _Cmd &= vbCrLf & ", FNWorkingTimeMinuteDay=B.FNWorkingTimeMinuteDay"
                _Cmd &= vbCrLf & ", FNTargetPerDay=B.FNTargetPerDay"
                _Cmd &= vbCrLf & ", FNSamCut=B.FNSamCut"
                _Cmd &= vbCrLf & ", FNCostCut=B.FNCostCut"
                _Cmd &= vbCrLf & ", FNNetCostCut=B.FNNetCostCut"
                _Cmd &= vbCrLf & ", FNSamPack=B.FNSamPack"
                _Cmd &= vbCrLf & ", FNCostPack=B.FNCostPack"
                _Cmd &= vbCrLf & ", FNNetCostPack=B.FNNetCostPack"
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam As A  "
                _Cmd &= vbCrLf & " OUTER APPLY ( Select TOP 1 *  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam As B With(NOLOCK)   where FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & " And FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & "  AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "   ) As B"
                _Cmd &= vbCrLf & "  WHERE  A.FNHSysStyleId=" & Val(StyleId) & " And A.FNHSysSeasonId=" & Val(SeasonId) & "  "
                _Cmd &= vbCrLf & "  AND A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

                _Cmd &= vbCrLf & " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam_Detail( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysSeasonId, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour,FNOutputPer8Hour, FTRemark, FTStateNoSew, FNHSysRDPositionPartId) "
                _Cmd &= vbCrLf & " select  "
                _Cmd &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser"
                _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""

                _Cmd &= vbCrLf & " , " & Val(StyleId) & " AS FNHSysStyleId," & Val(SeasonId) & "  AS  FNHSysSeasonId, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour,FNOutputPer8Hour, FTRemark, FTStateNoSew, FNHSysRDPositionPartId"
                _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam_Detail AS X WITH(NOLOCK) "
                _Cmd &= vbCrLf & "  where FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & " And FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & " AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PLANNING) Then
                    State = True
                End If

            Catch ex As Exception
            End Try
            _Spls.Close()

            Return State

        Catch ex As Exception

            Return False
        End Try
    End Function


#End Region

#Region "Event Handle"

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.Process = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click

        If FNHSysStyleId.Text <> "" And FNHSysSeasonId.Text <> "" Then


            If FNHSysStyleId.Text = DescStyleNo And FNHSysSeasonId.Text = DescSeasonNo Then
                Exit Sub
            End If

            Dim cmd As String = " Select SUM(1) As CountRec  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGTMSam_Detail As X With(NOLOCK) WHERE  (FNHSysStyleId = " & Val(FNHSysStyleId.Properties.Tag.ToString()) & ")  AND  (FNHSysSeasonId = " & Val(FNHSysSeasonId.Properties.Tag.ToString()) & ")   AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "    "

            Dim TotalRec As Integer = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PLANNING, "0"))

            If TotalRec > 0 Then



                If HI.MG.ShowMsg.mConfirmProcess("ข้อฒุลปลายทางจะถูกลบทั้งหมด คุณต้องการทำ Copy ข้อมูลใช่หรือไม่  ", 1888017744, "Style No " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & "  Copy To Style No " & DescStyleNo & " Season " & DescSeasonNo & " ") = True Then
                    If SaveCopy() = True Then
                        HI.MG.ShowMsg.mInfo("Copy Data Complete ...", 1888017745, Me.Text)
                        Me.Process = True
                        Me.Close()
                    End If
                End If



            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่สามารถทำการคัดลอกได้ !!!!", 1888017741, Me.Text)
            End If


        End If

    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub wCopyOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FNHSysStyleId.Text = ""
        FNHSysSeasonId.Text = ""
        FNHSysStyleId_None.Text = ""

    End Sub



#End Region

End Class