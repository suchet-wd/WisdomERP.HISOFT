Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wReportSummaryResign


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


        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= "  LEFT(THRMEmployee.FDDateEnd,4)='" & Me.FTPayYear.Text & "' "

        Dim tText As String = "" : Dim oCriteria As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "" & tText
        End If
        oCriteria = Microsoft.VisualBasic.Replace(_Formular, "]", ")")
        oCriteria = Microsoft.VisualBasic.Replace(oCriteria, "[", "(")
        oCriteria = Microsoft.VisualBasic.Replace(oCriteria, "{", "")
        oCriteria = Microsoft.VisualBasic.Replace(oCriteria, "}", "")

        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        HI.HRCAL.GenTempData.GetCauseResign(HI.ST.UserInfo.UserName, oCriteria)

        If _AllReportName <> "" Then

            Call HI.ST.Security.CreateTempEmpMaster(Condition)

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                    .Formular = " {TRPTTmpCauseResign.UserLogin}='" & HI.ST.UserInfo.UserName & "'  "
                    .ReportName = _ReportName
                    .Preview()
                End With
            Next
        Else
            HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If



    End Sub

   
    Private Sub wReportSummaryResign_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class