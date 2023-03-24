Imports System.Data.SqlClient
Imports System.IO

Public Class wHRReportLeaveByTerm
    Private _LstReport As HI.RP.ListReport

    Sub New(Optional _SysFormName As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _SysFormName <> "" Then
            Me.Name = _SysFormName
        End If

        Condition.PrePareData()

        _LstReport = New HI.RP.ListReport(_SysFormName)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""
        Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")
        If Me.FTPayTerm.Text = "" Or Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If
        Call GetCriteria()

        Dim _Cmd As String = ""
        _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GetLeaveDay_ByTerm '" & FTPayTerm.Text.Trim & "','" & FTPayYear.Text.Trim & "','" & HI.ST.UserInfo.UserName & "'"
        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR)

        _Cmd = "  SELECT  top 1   Convert(varchar(10),Convert(datetime,FDCalDateBegin) ,103) +' - '+Convert(varchar(10),Convert(datetime,FDCalDateEnd) ,103) "
        _Cmd &= vbCrLf & "     FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH(NOLOCK) "
        _Cmd &= vbCrLf & " where FTPayTerm ='" & Me.FTPayTerm.Text & "'"
        _Cmd &= vbCrLf & " and FTPayYear ='" & Me.FTPayYear.Text & "'"

        Dim _PayDay As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "")

        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        If _AllReportName <> "" Then

            Call HI.ST.Security.CreateTempEmpMaster(Condition)
            Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text)

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report

                    .FormTitle = Me.Text
                    .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                    '.Formular = _Formular
                    .AddParameter("FTPayDay", _PayDay)
                    .AddParameter("FTPayTerm", Me.FTPayTerm.Text)
                    .ReportName = _ReportName
                    _spls.Close()
                    .Preview()
                End With
            Next
        Else
            HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wReportHRByPayRoll_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetCriteria()
        Dim _Criteria As String = ""
        Dim _Cmd As String = ""

        '***Empployee Type***
        Dim tText As String = ""

        _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTmpEmpType WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR)

        Select Case Condition.FNEmpTypeCon.SelectedIndex
            Case 1
                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTmpEmpType"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FNHSysEmpTypeId "
                _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) "
                _Cmd &= vbCrLf & " WHERE FNHSysEmpTypeId <> 0"
                If Condition.FNHSysEmpTypeId.Text <> "" Then
                    _Cmd &= vbCrLf & " AND FTEmpTypeCode >='" & Condition.FNHSysEmpTypeId.Text & "'"
                End If
                If Condition.FNHSysEmpTypeIdTo.Text <> "" Then
                    _Cmd &= vbCrLf & " AND FTEmpTypeCode <='" & Condition.FNHSysEmpTypeIdTo.Text & "'"
                End If
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR)

            Case 2
                tText = ""
                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTmpEmpType"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' , FNHSysEmpTypeId "
                _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) "
                _Cmd &= vbCrLf & " Where FNHSysEmpTypeId <> 0  and "


                For Each oRow As DataRow In Condition.DbDtEmployeeType.Rows
                    tText &= oRow("FTCode") & "|"
                Next

                If tText.Trim <> "" Then
                    tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                    '_Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                    _Cmd &= " FTEmpTypeCode IN('" & tText.Replace("|", "','") & "') "
                End If
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR)


            Case Else
        End Select



    End Sub
End Class