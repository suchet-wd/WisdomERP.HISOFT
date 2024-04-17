Public Class wMatQCSpare


    Private wQCAddData As wMatQCSpareAdd


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        wQCAddData = New wMatQCSpareAdd
        HI.TL.HandlerControl.AddHandlerObj(wQCAddData)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, wQCAddData.Name.ToString.Trim, wQCAddData)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try

            Call LoadData()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading...")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT        A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysMatTypeId"
            _Cmd &= vbCrLf & "  ,B.FTMatTypeCode,MG.FTMatGrpCode "
            _Cmd &= vbCrLf & "	,B.FTMatTypeNameEN  AS FTMainMatName"
            _Cmd &= vbCrLf & "	, A.FNSpareConditionType"
            _Cmd &= vbCrLf & "	,X.FNSpareConditionTypeName "
            _Cmd &= vbCrLf & ", A.FNSpareType"
            _Cmd &= vbCrLf & ",X2.FNSpareTypeName"
            _Cmd &= vbCrLf & "	, A.FTRemark"
            _Cmd &= vbCrLf & "	, A.FTStateActive"
            _Cmd &= vbCrLf & "	,D.FNSeq "
            _Cmd &= vbCrLf & ",D.FNStartQty "
            _Cmd &= vbCrLf & ",D.FNEndQty "
            _Cmd &= vbCrLf & "	,D.FNSpare "
            _Cmd &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TMATQCSpare AS A INNER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMMatType AS B ON A.FNHSysMatTypeId = B.FNHSysMatTypeId"
            _Cmd &= vbCrLf & "	INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TMATQCSpare_Detail AS D ON A.FNHSysMatTypeId = D.FNHSysMatTypeId "
            _Cmd &= vbCrLf & "OUTER APPLY (select top 1 X.FTNameEN AS FNSpareConditionTypeName FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WHERE X.FTListName ='FNSpareConditionType' AND X.FNListIndex = A.FNSpareConditionType) AS X"
            _Cmd &= vbCrLf & "OUTER APPLY (select top 1 X2.FTNameEN  AS FNSpareTypeName FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X2 WHERE X2.FTListName ='FNSpareType' AND X2.FNListIndex = A.FNSpareType) AS X2"
            _Cmd &= vbCrLf & "OUTER APPLY (select top 1 MG.FTMatGrpCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMMatGrp AS MG WHERE  MG.FNHSysMatGrpId = B.FNHSysMatGrpId) AS MG"
            _Cmd &= vbCrLf & " ORDER BY B.FTMatTypeCode ,D.FNSeq  "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_INVEN)
            Me.ogcDetail.DataSource = _oDt
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnew.Click

        HI.TL.HandlerControl.ClearControl(wQCAddData)

        With wQCAddData
            .StateSave = False
            .btnSave.Enabled = True
            .btnExit.Enabled = True
            .LoadDataSpare(-99)
            .FTStateActive.Checked = True
            .ShowDialog()

            If .StateSave Then
                LoadData()
            End If

        End With

    End Sub

    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmedit.Click

        Dim MatCode As String = ""
        Dim MatID As Integer = 0

        With Me.ogvDetail
            If .FocusedRowHandle < 0 Then Exit Sub

            MatID = Val(.GetFocusedRowCellValue("FNHSysMatTypeId").ToString)
            MatCode = .GetFocusedRowCellValue("FTMatTypeCode").ToString
        End With

        If MatID > 0 Then
            HI.TL.HandlerControl.ClearControl(wQCAddData)

            With wQCAddData

                .btnSave.Enabled = True
                .btnExit.Enabled = True
                .StateSave = False
                .FNHSysMatTypeId.Text = MatCode
                .FNHSysMatTypeId.Properties.Tag = MatID.ToString
                .FNSpareConditionType.SelectedIndex = Val(ogvDetail.GetFocusedRowCellValue("FNSpareConditionType").ToString)
                .FNSpareType.SelectedIndex = Val(ogvDetail.GetFocusedRowCellValue("FNSpareType").ToString)
                .FTStateActive.Checked = (ogvDetail.GetFocusedRowCellValue("FTStateActive").ToString = "1")
                .FTRemark.Text = ogvDetail.GetFocusedRowCellValue("FTRemark").ToString

                .LoadDataSpare(MatID)
                .ShowDialog()

                If .StateSave Then
                    LoadData()
                End If

            End With

        End If

    End Sub

    Private Sub wMatQCSpare_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LoadData()
        Catch ex As Exception

        End Try
    End Sub
End Class