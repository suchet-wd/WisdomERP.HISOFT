Imports System.Data.SqlClient
Imports System.IO

Public Class wHRReportPayRollYear

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

        If Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        If Me.FNHSysEmpTypeId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
            Exit Sub
        End If

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= "  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRMEmployee.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

        If Me.FNEmpStatusReport.SelectedIndex <> 0 Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "  {THRMEmployee.FNEmpStatus} = " & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
        End If


        Dim tText As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "" & tText
        End If

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
                    .Formular = _Formular
                    .ReportName = _ReportName
                    .Preview()
                End With
            Next
        Else
            HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wReportHRByPayRollByYear_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class