Imports DevExpress.XtraEditors.Controls

Public Class wRDSamOperation

    Private _Copy As wCopyRDSamOperation
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Copy = New wCopyRDSamOperation

        HI.TL.HandlerControl.AddHandlerObj(_Copy)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _Copy.Name.ToString.Trim, _Copy)
        Catch ex As Exception
        End Try

    End Sub

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub LoadOperation()
        Dim cmd As String
        cmd = "SELECT        O.FNHSysRDOperationId, O.FTRDOperationCode,OMC.FNHSysRDMachineTypeId, OMC.FTRDMachineTypeCode,OMP.FNHSysRDPositionPartId, OMP.FTRDPositionPartCode  "
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            cmd &= vbCrLf & " , O.FTRDOperationTH AS FTRDOperation "
            cmd &= vbCrLf & " , OMC.FTRDMachineTypeNameTH AS FTRDMachineTypeName "
            cmd &= vbCrLf & " , OMP.FTRDPositionPartTH AS FTRDPositionPartName "

        Else
            cmd &= vbCrLf & " , O.FTRDOperationEN AS FTRDOperation "
            cmd &= vbCrLf & " , OMC.FTRDMachineTypeNameEN AS FTRDMachineTypeName "
            cmd &= vbCrLf & " , OMP.FTRDPositionPartEN AS FTRDPositionPartName "

        End If
        cmd &= vbCrLf & "  , O.FTRemark, O.FTStateActive,ISNULL(O.FTStateNoSew,'0') AS FTStateNoSew,ISNULL(O.FNSam,0) AS FNSam "
        cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation As O With(NOLOCK) "
        cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDMachineType As OMC With(NOLOCK) ON O.FNHSysRDMachineTypeId = OMC.FNHSysRDMachineTypeId "
        cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDPositionPart As OMP With(NOLOCK) ON  O.FNHSysRDPositionPartId = OMP.FNHSysRDPositionPartId "


        cmd &= vbCrLf & " WHERE ISNULL(O.FTStateActive,'') ='1' "
        cmd &= vbCrLf & " ORDER  BY O.FTRDOperationCode "

        Dim dt As New DataTable
        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)
        ReposFTRDOperationCode.DataSource = dt.Copy

        dt.Dispose()

        ' Call LoadRDMachineType()


        FNMinuteHour.Value = 60
        FNWorkingTimeMinuteDay.Value = 480

    End Sub


    'Private Sub LoadRDMachineType()
    '    Dim cmd As String
    '    cmd = "SELECT        FNHSysRDMachineTypeId, FTRDMachineTypeCode "
    '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

    '        cmd &= vbCrLf & " , FTRDMachineTypeNameTH AS FTRDMachineTypeName "
    '    Else
    '        cmd &= vbCrLf & " , FTRDMachineTypeNameEN AS FTRDMachineTypeName "
    '    End If
    '    cmd &= vbCrLf & "  , FTRemark, FTStateActive "
    '    cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDMachineType As O With(NOLOCK) "
    '    cmd &= vbCrLf & " WHERE ISNULL(O.FTStateActive,'') ='1' "
    '    cmd &= vbCrLf & " ORDER  BY FTRDMachineTypeCode "

    '    Dim dt As New DataTable
    '    dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)
    '    ReposFTRDMachineTypeCode.DataSource = dt.Copy

    '    dt.Dispose()

    'End Sub


    Public Sub LoadDataInfo(ByVal Key As Object)

        Dim _Qry As String = ""
        _Qry = " Select     Row_Number() Over (order By A.FNSeq) AS FNSeq, A.FNHSysRDOperationId, D.FTRDOperationCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Qry &= vbCrLf & "  , D.FTRDOperationTH AS FTRDOperationName"
            _Qry &= vbCrLf & "  , DMH.FTRDMachineTypeNameTH AS FTRDMachineTypeName"
            _Qry &= vbCrLf & "  , OMP.FTRDPositionPartTH AS FTRDPositionPartName "

        Else

            _Qry &= vbCrLf & "  , D.FTRDOperationEN AS FTRDOperationName"
            _Qry &= vbCrLf & "  , DMH.FTRDMachineTypeNameEN AS FTRDMachineTypeName"
            _Qry &= vbCrLf & "  , OMP.FTRDPositionPartEN AS FTRDPositionPartName "

        End If

        _Qry &= vbCrLf & " , A.FNSam, A.FTRemark,A.FNHSysRDMachineTypeId,DMH.FTRDMachineTypeCode,A.FNOperater, A.FNCost, A.FNOutputPer1Hour, A.FNOutputPer8Hour,ISNULL(A.FTStateNoSew,'0') AS FTStateNoSew "
        _Qry &= vbCrLf & " , A.FNHSysRDPositionPartId,OMP.FTRDPositionPartCode,A.FTLenght,A.FTAdditionalInfo,A.FTAttachment,A.FNSamBF"

        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDOperation AS D WITH(NOLOCK) ON A.FNHSysRDOperationId = D.FNHSysRDOperationId"
        _Qry &= vbCrLf & "           LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDMachineType AS DMH WITH(NOLOCK) ON A.FNHSysRDMachineTypeId = DMH.FNHSysRDMachineTypeId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMRDPositionPart As OMP With(NOLOCK) ON  A.FNHSysRDPositionPartId = OMP.FNHSysRDPositionPartId "

        _Qry &= vbCrLf & "   WHERE A.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
        _Qry &= vbCrLf & "  ORDER BY A.FNSeq  ASC"

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

    End Sub

#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub GetStateApp()
        Dim dt As New DataTable
        Dim _Qry As String = "SELECT TOP 1  ISNULL(FTStateApp,'')  as FTStateApp,ISNULL(FTStateSendApp,'')  as FTStateSendApp FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam As X WITH(NOLOCK) WHERE FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' "
        Dim StateApp As String = ""
        Dim StateSendApp As String = ""

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PLANNING)

        For Each R As DataRow In dt.Rows

            StateApp = R!FTStateApp.ToString
            StateSendApp = R!FTStateSendApp.ToString

        Next


        dt.Dispose()
        FTStateApp.Checked = (StateApp = "1")
        FTStateSendApp.Checked = (StateSendApp = "1")
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click


        Dim dtoper As New DataTable
        With CType(Me.ogcoperation.DataSource, DataTable)
            .AcceptChanges()
            dtoper = .Copy()
        End With

        If FTSMPOrderNo.Properties.Tag.ToString <> "" And FTSMPOrderNo.Text <> "" Then
            Call GetStateApp()
            If FTStateApp.Checked = False Then

                If dtoper.Select("FTRDOperationCode<>'' AND FTRDMachineTypeCode<>''", "FNSeq").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบรายการขั้นตอน กรุณาทำการตรวจสอบ !!!", 1907282245, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim _Qry As String = ""

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam SET "
                    _Qry &= vbCrLf & " FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " ,FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString()) & " "
                    _Qry &= vbCrLf & " ,FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString()) & " "
                    _Qry &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                    _Qry &= vbCrLf & " ,FNSam=" & FNSam.Value & " "
                    _Qry &= vbCrLf & " ,FNOperater=" & FNOperater.Value & " "
                    _Qry &= vbCrLf & " ,FNCost=" & FNCost.Value & " "
                    _Qry &= vbCrLf & " ,FNMinuteHour=" & FNMinuteHour.Value & " "
                    _Qry &= vbCrLf & " ,FNProdPersonPerDay=" & FNProdPersonPerDay.Value & " "
                    _Qry &= vbCrLf & " ,FNWorkingTimeMinuteDay=" & FNWorkingTimeMinuteDay.Value & " "
                    _Qry &= vbCrLf & " ,FNTargetPerDay=" & FNTargetPerDay.Value & " "
                    _Qry &= vbCrLf & " ,FNSamCut=" & FNSamCut.Value & " "
                    _Qry &= vbCrLf & " ,FNCostCut=" & FNCostCut.Value & " "
                    _Qry &= vbCrLf & " ,FNNetCostCut=" & FNNetCostCut.Value & " "
                    _Qry &= vbCrLf & " ,FNSamPack=" & FNSamPack.Value & " "
                    _Qry &= vbCrLf & " ,FNCostPack=" & FNCostPack.Value & " "
                    _Qry &= vbCrLf & " ,FNNetCostPack=" & FNNetCostPack.Value & " "
                    _Qry &= vbCrLf & " ,FTStateSendApp='0' "
                    _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam "
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam,FNOperater, FNCost,FNMinuteHour,FNProdPersonPerDay,FNWorkingTimeMinuteDay,FNTargetPerDay,FTStateSendApp,FNSamCut,FNCostCut,FNNetCostCut,FNSamPack,FNCostPack,FNNetCostPack) "
                        _Qry &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                        _Qry &= vbCrLf & " ," & Val(FNHSysStyleId.Properties.Tag.ToString()) & " "
                        _Qry &= vbCrLf & " ," & Val(FNHSysSeasonId.Properties.Tag.ToString()) & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                        _Qry &= vbCrLf & " ," & FNSam.Value & " "
                        _Qry &= vbCrLf & " ," & FNOperater.Value & " "
                        _Qry &= vbCrLf & " ," & FNCost.Value & " "
                        _Qry &= vbCrLf & " ," & FNMinuteHour.Value & " "
                        _Qry &= vbCrLf & " ," & FNProdPersonPerDay.Value & " "
                        _Qry &= vbCrLf & " ," & FNWorkingTimeMinuteDay.Value & " "
                        _Qry &= vbCrLf & " ," & FNTargetPerDay.Value & " "
                        _Qry &= vbCrLf & " ,'0' "
                        _Qry &= vbCrLf & " ," & FNSamCut.Value & " "
                        _Qry &= vbCrLf & " ," & FNCostCut.Value & " "
                        _Qry &= vbCrLf & " ," & FNNetCostCut.Value & " "
                        _Qry &= vbCrLf & " ," & FNSamPack.Value & " "
                        _Qry &= vbCrLf & " ," & FNCostPack.Value & " "
                        _Qry &= vbCrLf & " ," & FNNetCostPack.Value & " "


                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Spls.Close()

                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            Exit Sub
                        End If

                    End If

                    _FNSeq = 0

                    For Each R As DataRow In dtoper.Select("FTRDOperationCode<>'' AND FTRDMachineTypeCode<>''", "FNSeq")
                        _FNSeq = _FNSeq + 1

                        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail SET "
                        _Qry &= vbCrLf & " FTUpdUser= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,FNHSysRDOperationId=" & Val(R!FNHSysRDOperationId.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNHSysRDMachineTypeId=" & Val(R!FNHSysRDMachineTypeId.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNHSysRDPositionPartId=" & Val(R!FNHSysRDMachineTypeId.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNSam=" & Val(R!FNSam.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNOperater=" & Val(R!FNOperater.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNCost=" & Val(R!FNCost.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNOutputPer1Hour=" & Val(R!FNOutputPer1Hour.ToString()) & " "
                        _Qry &= vbCrLf & " ,FNOutputPer8Hour=" & Val(R!FNOutputPer8Hour.ToString()) & " "
                        _Qry &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString()) & "' "
                        _Qry &= vbCrLf & " ,FTStateNoSew='" & HI.UL.ULF.rpQuoted(R!FTStateNoSew.ToString()) & "' "
                        _Qry &= vbCrLf & " ,FTLenght='" & HI.UL.ULF.rpQuoted(R!FTLenght.ToString()) & "' "
                        _Qry &= vbCrLf & " ,FTAdditionalInfo='" & HI.UL.ULF.rpQuoted(R!FTAdditionalInfo.ToString()) & "' "
                        _Qry &= vbCrLf & " ,FTAttachment='" & HI.UL.ULF.rpQuoted(R!FTAttachment.ToString()) & "' "
                        _Qry &= vbCrLf & " ,FNSamBF=" & Val(R!FNSamBF.ToString()) & " "
                        _Qry &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                        _Qry &= vbCrLf & "        AND FNSeq=" & _FNSeq & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail"
                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FTSMPOrderNo, FNSeq, FNHSysRDOperationId,FNHSysRDMachineTypeId, FNSam,FNOperater,FNCost,FNOutputPer1Hour,FNOutputPer8Hour,FTRemark,FTStateNoSew,FNHSysRDPositionPartId,FTLenght,FTAdditionalInfo,FTAttachment,FNSamBF)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                            _Qry &= vbCrLf & " ," & _FNSeq & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNHSysRDOperationId.ToString()) & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNHSysRDMachineTypeId.ToString()) & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNSam.ToString()) & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNOperater.ToString()) & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNCost.ToString()) & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNOutputPer1Hour.ToString()) & " "
                            _Qry &= vbCrLf & " ," & Val(R!FNOutputPer8Hour.ToString()) & " "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString()) & "' "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTStateNoSew.ToString()) & "' "


                            _Qry &= vbCrLf & " ," & Val(R!FNHSysRDPositionPartId.ToString()) & " "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTLenght.ToString()) & "' "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTAdditionalInfo.ToString()) & "' "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTAttachment.ToString()) & "' "
                            _Qry &= vbCrLf & " ," & Val(R!FNSamBF.ToString()) & " "

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


                                _Spls.Close()
                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                Exit Sub

                            End If

                        End If

                    Next

                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail  "
                    _Qry &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                    _Qry &= vbCrLf & "     AND FNSeq > " & _FNSeq & " "

                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Call LoadSMPInfo(FTSMPOrderNo.Text)

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try

            Else
                HI.MG.ShowMsg.mInfo("ข้อมูลถูกอนุมัติแล้วไม่สามารถทำการเปลี่ยนแปลงหรือแก้ไขได้ !!!", 1907280045, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)


            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If

    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        If FTSMPOrderNo.Properties.Tag.ToString <> "" And FTSMPOrderNo.Text <> "" Then
            Call GetStateApp()
            If FTStateApp.Checked = False Then

                With _Copy
                    HI.ST.Lang.SP_SETxLanguage(_Copy)
                    .Process = False
                    .DescOrder = FTSMPOrderNo.Text
                    .FTSMPOrderNo.Text = ""
                    .FNHSysStyleId.Text = ""
                    .FNHSysStyleId_None.Text = ""
                    .FNHSysSeasonId.Text = ""
                    .ShowDialog()

                    If (.Process) Then

                        Call ClearData()
                        Call LoadSMPInfo(FTSMPOrderNo.Text)

                    End If

                End With

            Else
                HI.MG.ShowMsg.mInfo("ข้อมูลถูกอนุมัติแล้วไม่สามารถทำการเปลี่ยนแปลงหรือแก้ไขได้ !!!", 1907280045, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                FTSMPOrderNo.Focus()
            End If


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If
    End Sub

    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvoperation.OptionsView.ShowAutoFilterRow = False
        ogvoperation.OptionsSelection.MultiSelect = False
        ogvoperation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

        Call LoadOperation()

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvoperation.Columns
            With GridCol
                .OptionsColumn.AllowSort = False
            End With
        Next

        FNMinuteHour.Value = 60
        FNWorkingTimeMinuteDay.Value = 480

    End Sub

#End Region


    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(Me)
        Call LoadOperation()

        FNMinuteHour.Value = 60
        FNWorkingTimeMinuteDay.Value = 480

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvoperation.Columns
            With GridCol
                .OptionsColumn.AllowSort = False
            End With
        Next


    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub AddeGridRow()
        If ocmbomdeleterow.Enabled = False Then Exit Sub
        If Not (Me.ogcoperation.DataSource Is Nothing) Then

            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()


                If .Select("FTRDOperationCode='' OR FTRDMachineTypeCode=''").Length <= 0 Then
                    .Rows.Add(.Rows.Count + 1, 0, "", "", "", "", 0.0000, "", 0, "", 0, 0, 0, 0, "0", 0, "", "", "", "", 0)
                End If

            End With

            Me.ogvoperation.FocusedRowHandle = Me.ogvoperation.RowCount - 1
            Me.ogvoperation.FocusedColumn = ogvoperation.Columns.ColumnByFieldName("FTRDOperationCode")

        End If

    End Sub

    Private Sub RemoveGridRow()
        If ocmbomaddnew.Enabled = False Then Exit Sub
        If Not (Me.ogcoperation.DataSource Is Nothing) Then

            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
            End With

            With CType(Me.ogcoperation.DataSource, DataTable)

                .AcceptChanges()
                .BeginInit()

                Dim Ridx As Integer = 1

                For Each R As DataRow In .Select("FNSeq>0", "FNSeq")

                    R!FNSeq = Ridx
                    Ridx = Ridx + 1

                Next

                .EndInit()
                .AcceptChanges()

            End With

            Call SumSam()

            ' AddeGridRow()

        End If

    End Sub


    Private Sub insertGridRow()
        If ocmbominsertrow.Enabled = False Then Exit Sub
        If Not (Me.ogcoperation.DataSource Is Nothing) Then
            Dim CurIdx As Integer = 0
            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub
                CurIdx = Val(.GetFocusedRowCellValue("FNSeq").ToString)
            End With

            With CType(Me.ogcoperation.DataSource, DataTable)

                .AcceptChanges()

                If .Select("FTRDOperationCode='' OR FTRDMachineTypeCode=''").Length <= 0 Then
                    .BeginInit()


                    Dim Ridx As Integer = CurIdx + 1

                    For Each R As DataRow In .Select("FNSeq>=" & CurIdx & "", "FNSeq")

                        R!FNSeq = Ridx
                        Ridx = Ridx + 1

                    Next

                    .Rows.Add(CurIdx, 0, "", "", "", "", 0.0000, "", 0, "", 0, 0, 0, 0, "0", 0, "", "", "", "", 0)

                    .EndInit()

                End If


                .AcceptChanges()

            End With

            Call SumSam()

            ' AddeGridRow()

        End If

    End Sub

    Private Sub SumSam()
        Try
            Dim TotalSam As Double = 0.0
            Dim TotalOperater As Double = 0.0
            Dim TotalCost As Double = 0.0
            Dim Totaltarget As Integer = 0

            With CType(Me.ogcoperation.DataSource, DataTable)

                .AcceptChanges()

                For Each R As DataRow In .Select("FNSeq>0", "FNSeq")

                    If R!FTStateNoSew.ToString <> "1" Then

                        TotalSam = TotalSam + Val(R!FNSam.ToString())
                        TotalOperater = TotalOperater + Val(R!FNOperater.ToString())
                        TotalCost = TotalCost + Val(R!FNCost.ToString())

                    End If

                Next

            End With

            FNSam.Value = TotalSam
            FNOperater.Value = TotalOperater
            FNCost.Value = TotalCost

            If TotalSam > 0 Then
                Totaltarget = CInt(Format((TotalOperater * FNWorkingTimeMinuteDay.Value) / TotalSam, "0"))

            End If

            FNTargetPerDay.Value = Totaltarget

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogcoperation_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcoperation.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                Call RemoveGridRow()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call AddeGridRow()

            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmbomaddnew.Click
        Call AddeGridRow()
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmbomdeleterow.Click
        Call RemoveGridRow()
    End Sub

    Private Sub ClearData()

        FNSMPOrderType.SelectedIndex = 0
        FNSMPPrototypeNo.Value = 0
        FNOrderSampleType.SelectedIndex = 0
        FNHSysStyleId.Text = ""
        FNHSysStyleId_None.Text = ""
        FNHSysSeasonId.Text = ""
        FNSam.Value = 0
        FNOperater.Value = 0
        FNCost.Value = 0
        FNProdPersonPerDay.Value = 0
        FNTargetPerDay.Value = 0
        FTRemark.Text = ""
        FTInsUser.Text = ""
        FTInsTime.Text = ""

        FNMinuteHour.Value = 60
        FNWorkingTimeMinuteDay.Value = 480

        FNSamCut.Value = 0
        FNCostCut.Value = 0
        FNNetCostCut.Value = 0

        FNSamPack.Value = 0
        FNCostPack.Value = 0
        FNNetCostPack.Value = 0

        FTStateSendApp.Checked = False
        FTStateApp.Checked = False

    End Sub

    Private Sub LoadSMPInfo(orderno As String)
        Dim cmd As String = ""
        Dim dt As New DataTable

        cmd = "SELECT    TOP 1   A.FTSMPOrderNo, A.FDSMPOrderDate, A.FTSMPOrderBy, A.FNSMPOrderType, A.FNSMPPrototypeNo, A.FNHSysStyleId, A.FNHSysSeasonId, A.FNHSysMerTeamId, ST.FTStyleCode, SS.FTSeasonCode, MT.FTMerTeamCode"
        cmd &= vbCrLf & "   ,A.FNOrderSampleType, RD.FTRemark , RD.FNSam, RD.FTStateSendApp, RD.FTStateSendAppBy, RD.FTStateSendAppDate, RD.FTStateSendAppTime, RD.FTStateApp, RD.FTStateAppBy, RD.FTStateAppDate"
        cmd &= vbCrLf & "  , RD.FTStateAppTime,ISNULL(RD.FTUpdUser,RD.FTInsUser) AS FTInsUser,ISNULL(RD.FDUpdDate,ISNULL(RD.FDInsDate,'')) + '  ' + ISNULL(RD.FTUpdTime,ISNULL(RD.FTInsTime,'')) AS FTInsTime,RD.FTStateSendApp,RD.FTStateApp,RD.FNOperater,RD.FNCost,RD.FNProdPersonPerDay,RD.FNTargetPerDay,RD.FNSamCut,RD.FNCostCut,RD.FNNetCostCut,RD.FNSamPack,RD.FNCostPack,RD.FNNetCostPack"
        cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam As RD  WITH(NOLOCK) On A.FTSMPOrderNo = RD.FTSMPOrderNo LEFT OUTER JOIN"
        cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS MT  WITH(NOLOCK) ON A.FNHSysMerTeamId = MT.FNHSysMerTeamId LEFT OUTER JOIN"
        cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST  WITH(NOLOCK) On A.FNHSysStyleId = ST.FNHSysStyleId LEFT OUTER JOIN "
        cmd &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON A.FNHSysSeasonId = SS.FNHSysSeasonId"
        cmd &= vbCrLf & "    WHERE  A.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(orderno) & "'"
        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PLANNING)

        FNMinuteHour.Value = 60
        FNWorkingTimeMinuteDay.Value = 480

        For Each R As DataRow In dt.Rows

            FNSMPOrderType.SelectedIndex = Val(R!FNSMPOrderType.ToString())
            FNSMPPrototypeNo.Value = Val(R!FNSMPPrototypeNo.ToString())
            FNOrderSampleType.SelectedIndex = Val(R!FNOrderSampleType.ToString())
            FNHSysStyleId.Text = R!FTStyleCode.ToString()

            FNHSysSeasonId.Text = R!FTSeasonCode.ToString()
            FNSam.Value = Val(R!FNSam.ToString())
            FTRemark.Text = R!FTRemark.ToString()

            FTInsUser.Text = R!FTInsUser.ToString()
            FTInsTime.Text = R!FTInsTime.ToString()

            FNOperater.Value = Val(R!FNOperater.ToString())
            FNCost.Value = Val(R!FNCost.ToString())
            FNProdPersonPerDay.Value = Val(R!FNProdPersonPerDay.ToString())
            FNTargetPerDay.Value = Val(R!FNTargetPerDay.ToString())

            FNSamCut.Value = Val(R!FNSamCut.ToString())
            FNCostCut.Value = Val(R!FNCostCut.ToString())
            FNNetCostCut.Value = Val(R!FNNetCostCut.ToString())

            FNSamPack.Value = Val(R!FNSamPack.ToString())
            FNCostPack.Value = Val(R!FNCostPack.ToString())
            FNNetCostPack.Value = Val(R!FNNetCostPack.ToString())

            FTStateSendApp.Checked = (R!FTStateSendApp.ToString() = "1")
            FTStateApp.Checked = (R!FTStateApp.ToString() = "1")

        Next

        dt.Dispose()

        Call LoadDataInfo(orderno)
    End Sub

    Private Sub FTSMPOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged
        If FTSMPOrderNo.Text <> "" Then
            If Me.InvokeRequired Then

                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTSMPOrderNo_EditValueChanged), New Object() {sender, e})

            Else

                Call ClearData()
                Call LoadSMPInfo(FTSMPOrderNo.Text)

            End If

        Else

            Call ClearData()
            Me.ogcoperation.DataSource = Nothing

        End If
    End Sub

    Private Sub ReposFTRDOperationCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposFTRDOperationCode.EditValueChanged
        Try

            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                .SetFocusedRowCellValue("FNHSysRDOperationId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRDOperationId").ToString)
                .SetFocusedRowCellValue("FTRDOperationName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDOperation").ToString)
                .SetFocusedRowCellValue("FTRDOperationCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDOperationCode").ToString)

                .SetFocusedRowCellValue("FTStateNoSew", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateNoSew").ToString)


                .SetFocusedRowCellValue("FNSamBF", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNSam").ToString)

                .SetFocusedRowCellValue("FNSam", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNSam").ToString)

                .SetFocusedRowCellValue("FNHSysRDMachineTypeId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRDMachineTypeId").ToString)
                .SetFocusedRowCellValue("FTRDMachineTypeCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDMachineTypeCode").ToString)
                .SetFocusedRowCellValue("FTRDMachineTypeName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDMachineTypeName").ToString)



                .SetFocusedRowCellValue("FNHSysRDPositionPartId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRDPositionPartId").ToString)
                .SetFocusedRowCellValue("FTRDPositionPartCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDPositionPartCode").ToString)
                .SetFocusedRowCellValue("FTRDPositionPartName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDPositionPartName").ToString)



                .SetFocusedRowCellValue("FNOperater", 0)
                .SetFocusedRowCellValue("FNCost", 0)
                .SetFocusedRowCellValue("FNOutputPer1Hour", 0)
                .SetFocusedRowCellValue("FNOutputPer8Hour", 0)

                .SetFocusedRowCellValue("FTRemark", "")


                If Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNSam").ToString) >= 0 Then


                    Dim dsam As Decimal = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNSam").ToString)
                    .SetFocusedRowCellValue("FNSam", dsam)

                    Dim QtyHour As Integer = 0
                    Dim Qty8Hour As Integer = 0

                    If dsam > 0 Then
                        QtyHour = CInt(Format(60 / dsam, "0"))
                        Qty8Hour = CInt(Format(480 / dsam, "0"))
                    End If


                    .SetFocusedRowCellValue("FNOutputPer1Hour", QtyHour)
                    .SetFocusedRowCellValue("FNOutputPer8Hour", Qty8Hour)

                    Call SumSam()

                End If


                Call SumSam()

            End With

            CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvoperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvoperation.KeyDown

        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                Call AddeGridRow()
            Case System.Windows.Forms.Keys.Delete
                Call RemoveGridRow()
            Case System.Windows.Forms.Keys.Enter
                Select Case ogvoperation.FocusedColumn.FieldName
                    Case "FTRemark"
                        Call AddeGridRow()

                End Select
        End Select

    End Sub

    Private Sub ReposFNSam_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNSam.EditValueChanging
        Try
            With ogvoperation
                If .GetFocusedRowCellValue("FTRDOperationCode").ToString <> "" And .GetFocusedRowCellValue("FTRDMachineTypeCode").ToString <> "" Then

                    If Val(e.NewValue.ToString) >= 0 Then
                        e.Cancel = False

                        Dim dsam As Decimal = Val(e.NewValue.ToString)
                        .SetFocusedRowCellValue("FNSam", dsam)


                        Dim QtyHour As Integer = 0
                        Dim Qty8Hour As Integer = 0

                        If dsam > 0 Then
                            QtyHour = CInt(Format(60 / dsam, "0"))
                            Qty8Hour = CInt(Format(480 / dsam, "0"))
                        End If



                        .SetFocusedRowCellValue("FNOutputPer1Hour", QtyHour)
                        .SetFocusedRowCellValue("FNOutputPer8Hour", Qty8Hour)

                        Call SumSam()
                    Else
                        e.Cancel = True
                    End If

                Else
                    e.Cancel = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFNCost_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNCost.EditValueChanging
        Try
            With ogvoperation
                If .GetFocusedRowCellValue("FTRDOperationCode").ToString <> "" And .GetFocusedRowCellValue("FTRDMachineTypeCode").ToString <> "" Then

                    If Val(e.NewValue.ToString) >= 0 Then
                        e.Cancel = False



                        .SetFocusedRowCellValue("FNCost", Val(e.NewValue.ToString))

                        Call SumSam()
                    Else
                        e.Cancel = True
                    End If

                Else
                    e.Cancel = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFNOperater_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNOperater.EditValueChanging
        Try
            With ogvoperation
                If .GetFocusedRowCellValue("FTRDOperationCode").ToString <> "" And .GetFocusedRowCellValue("FTRDMachineTypeCode").ToString <> "" Then

                    If Val(e.NewValue.ToString) >= 0 Then
                        e.Cancel = False

                        .SetFocusedRowCellValue("FNOperater", Val(e.NewValue.ToString))

                        Call SumSam()
                    Else
                        e.Cancel = True
                    End If

                Else
                    e.Cancel = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFTRDMachineTypeCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposFTRDMachineTypeCode.EditValueChanged
        Try

            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                .SetFocusedRowCellValue("FNHSysRDMachineTypeId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRDMachineTypeId").ToString)
                .SetFocusedRowCellValue("FTRDMachineTypeName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDMachineTypeName").ToString)
                .SetFocusedRowCellValue("FTRDMachineTypeCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRDMachineTypeCode").ToString)

                '.SetFocusedRowCellValue("FNSam", 0)
                .SetFocusedRowCellValue("FNOperater", 0)
                .SetFocusedRowCellValue("FNCost", 0)
                .SetFocusedRowCellValue("FNOutputPer1Hour", 0)
                .SetFocusedRowCellValue("FNOutputPer8Hour", 0)

                Call SumSam()

            End With

            CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTSMPOrderNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Merchandise Report\"
                .ReportName = "RDSamReport.rpt"
                .Formular = "{TRDTRDSam.FTSMPOrderNo} ='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If FTSMPOrderNo.Properties.Tag.ToString <> "" And FTSMPOrderNo.Text <> "" Then
            Call GetStateApp()
            If FTStateApp.Checked = False Then

                Dim _Qry As String = ""

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam SET "
                _Qry &= vbCrLf & " FTStateSendAppBy= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " ,FTStateSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ,FTStateSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,FTStateSendApp='1' "
                _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PLANNING) Then
                    FTStateSendApp.Checked = True
                    HI.MG.ShowMsg.mInfo("Send Approve Complete !!!", 1908041546, Me.Text,, System.Windows.Forms.MessageBoxIcon.Information)
                End If

            End If

        End If

    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        If FTSMPOrderNo.Properties.Tag.ToString <> "" And FTSMPOrderNo.Text <> "" Then
            Call GetStateApp()

            If FTStateSendApp.Checked = False Then
                HI.MG.ShowMsg.mInfo("ยังไม่ได้ทำการส่ง อนุมัติ ไม่สามารถทำการอนุมัติได้ !!!", 1908041528, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If FTStateApp.Checked = False Then

                Dim _Cmd As String = ""

                _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam SET "
                _Cmd &= vbCrLf & " FTStateAppBy= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Cmd &= vbCrLf & " ,FTStateAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Cmd &= vbCrLf & " ,FTStateAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Cmd &= vbCrLf & " ,FTStateApp='1' "

                'Select Case FNSMPOrderType.SelectedIndex
                '    Case 1, 24
                _Cmd &= vbCrLf & " ,FTStateSendMerApp='1' "
                _Cmd &= vbCrLf & " ,FTStateMerApp='0' "
                '    Case Else
                '        _Cmd &= vbCrLf & " ,FTStateSendMerApp='0' "
                '        _Cmd &= vbCrLf & " ,FTStateMerApp='0' "
                'End Select

                _Cmd &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PLANNING) Then

                    _Cmd = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSam (FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNSeq, FNMockUpType, FNSam, FNCostPerMin, FNPrice, FTRemark) "
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & " ,1 "
                    _Cmd &= vbCrLf & " ," & FNSam.Value & " "
                    _Cmd &= vbCrLf & " ,0.71  "
                    _Cmd &= vbCrLf & ",Convert(numeric(18,4)," & FNSam.Value & " * 0.71) "
                    _Cmd &= vbCrLf & " ,'GSD SAM APP'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

                    FTStateApp.Checked = True
                    HI.MG.ShowMsg.mInfo("Approve Data Complete !!!", 1908041548, Me.Text,, System.Windows.Forms.MessageBoxIcon.Information)

                End If

            End If

        End If
    End Sub


    Private Sub FNSamCut_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamCut.EditValueChanging
        Try
            If Val(e.NewValue.ToString) >= 0 Then
                Dim SamCut As Double = 0
                Dim CostCut As Double = 0

                SamCut = Val(e.NewValue)
                CostCut = FNCostCut.Value

                FNNetCostCut.Value = Format(SamCut * CostCut, "0.0000")
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostCut_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostCut.EditValueChanging
        Try
            If Val(e.NewValue.ToString) >= 0 Then
                Dim SamCut As Double = 0
                Dim CostCut As Double = 0

                SamCut = FNCostCut.Value
                CostCut = Val(e.NewValue)

                FNNetCostCut.Value = Format(SamCut * CostCut, "0.0000")
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNSamPack_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNSamPack.EditValueChanging
        Try
            If Val(e.NewValue.ToString) >= 0 Then
                Dim SamPack As Double = 0
                Dim CostPack As Double = 0

                SamPack = Val(e.NewValue)
                CostPack = FNCostPack.Value

                FNNetCostPack.Value = Format(SamPack * CostPack, "0.0000")
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNCostPack_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNCostPack.EditValueChanging
        Try
            If Val(e.NewValue.ToString) >= 0 Then
                Dim SamPack As Double = 0
                Dim CostPack As Double = 0

                SamPack = FNCostPack.Value
                CostPack = Val(e.NewValue)

                FNNetCostPack.Value = Format(SamPack * CostPack, "0.0000")
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmbominsertrow_Click(sender As Object, e As EventArgs) Handles ocmbominsertrow.Click
        insertGridRow()
    End Sub
End Class