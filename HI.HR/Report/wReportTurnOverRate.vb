Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wReportTurnOverRate


    Private _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Me.Name = _SysFormName

        Condition.PrePareData()
        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
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
        If Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        Select Case FNReportname.SelectedIndex
            Case 0
                If Me.FTPayYear.Text = "" Then
                    HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                    Exit Sub
                End If
        End Select

        Dim tText As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "" & tText
        End If

        Select Case FNReportname.SelectedIndex
            Case 0
                HI.HRCAL.GenTempData.GenTempWorkingTimeAttendance(HI.ST.UserInfo.UserName, _Formular, Me.FTPayYear.Text & "/" & Microsoft.VisualBasic.Right("00" & Format(FNMonth.SelectedIndex + 1, "0"), 2))
                _Formular = " {THRTTempWorkingTimeAttendance.UserLogin}='" & HI.ST.UserInfo.UserName & "' "
            Case 1
                HI.HRCAL.GenTempData.GetTempTRPTTempTWorkingTimernOver(HI.ST.UserInfo.UserName, _Formular, Me.FTPayYear.Text)
                _Formular = " {TRPTTempTWorkingTimernOver.UserLogin}='" & HI.ST.UserInfo.UserName & "' "
        End Select

        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        If _AllReportName <> "" Then
            Call HI.ST.Security.CreateTempEmpMaster(Condition)

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report

                    If Me.FTPayYear.Text <> "" Then
                        .AddParameter("FTPayYear", Me.FTPayYear.Text)
                    End If

                    If Me.FNMonth.Text <> "" Then
                        .AddParameter("FNMonth", Me.FNMonth.Text)
                    End If

                    .AddParameter("StartDate", Me.FTPayYear.Text & "/" & Microsoft.VisualBasic.Right("00" & Format(FNMonth.SelectedIndex + 1, "0"), 2) & "/01")
                    .FormTitle = Me.Text
                    .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                    .Formular = _Formular
                    .ReportName = _ReportName
                    .Preview()

                End With
            Next
        Else
            HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub wReportTurnOverRate_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class