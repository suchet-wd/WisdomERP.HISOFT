﻿Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic

Public Class wCopyRDSamOperation

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


    Private _DescOrder As String = ""
    Public Property DescOrder As String
        Get
            Return _DescOrder
        End Get
        Set(value As String)
            _DescOrder = value
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


            Try

                _Cmd = " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(DescOrder) & "'"
                _Cmd &= vbCrLf & " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(DescOrder) & "'"
                _Cmd &= vbCrLf & " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam( FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, FNWorkingTimeMinuteDay, FNTargetPerDay, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack) "
                _Cmd &= vbCrLf & " select TOP 1 "
                _Cmd &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser"
                _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(DescOrder) & "' AS  FTSMPOrderNo"
                _Cmd &= vbCrLf & " ," & StyleId & " AS  FNHSysStyleId"
                _Cmd &= vbCrLf & " ," & SeasonId & " AS  FNHSysSeasonId"
                _Cmd &= vbCrLf & " , FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, FNWorkingTimeMinuteDay, FNTargetPerDay, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack"
                _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam AS X WITH(NOLOCK) "
                _Cmd &= vbCrLf & " where X.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

                _Cmd &= vbCrLf & " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail( FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour,  FNOutputPer8Hour, FTRemark,FTStateNoSew) "
                _Cmd &= vbCrLf & " select  "
                _Cmd &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser"
                _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(DescOrder) & "' AS  FTSMPOrderNo"

                _Cmd &= vbCrLf & " , FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour,  FNOutputPer8Hour, FTRemark,FTStateNoSew"
                _Cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS X WITH(NOLOCK) "
                _Cmd &= vbCrLf & " where X.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

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

        If FTSMPOrderNo.Text <> "" Then

            If FTSMPOrderNo.Text <> DescOrder Then
                Dim cmd As String = " Select SUM(1) As CountRec  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail As X With(NOLOCK) WHERE  (FTSMPOrderNo = N'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "')  "

                Dim TotalRec As Integer = Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_PLANNING, "0"))

                If TotalRec > 0 Then

                    If HI.MG.ShowMsg.mConfirmProcess("ข้อฒุลปลายทางจะถูกลบทั้งหมด คุณต้องการทำ Copy ข้อมูลใช่หรือไม่  ", 1888017744, FTSMPOrderNo.Text & "  Copy To " & DescOrder) = True Then
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



        End If

    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub wCopyOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        FTSMPOrderNo.Text = ""
        FNHSysStyleId.Text = ""
        FNHSysSeasonId.Text = ""
        FNHSysStyleId_None.Text = ""
        FNSMPOrderType.SelectedIndex = 0
        FNOrderSampleType.SelectedIndex = 0
        FNSMPPrototypeNo.Value = 0

    End Sub



#End Region

End Class