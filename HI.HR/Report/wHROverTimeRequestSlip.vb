Imports System.Data.SqlClient
Imports System.IO

Public Class wHROverTimeRequestSlip

    Private _LstReport As HI.RP.ListReport

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Condition.PrePareData()
        _LstReport = New HI.RP.ListReport(Me.Name.ToString)

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""

        If Me.SFTDateTrans.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005200001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        _Formular = " {THRTDailyOTRequest.FTDateRequest}='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "' "

        Dim tText As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "" & tText
        End If

        _Formular &= "  AND  {THRMEmployee.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
   
        Call HI.ST.Security.CreateTempEmpMaster(Condition)

        With New HI.RP.Report

            '*****วันที่ทำงาน*********
            If Me.SFTDateTrans.Text <> "" Then
                .AddParameter("SFTDateTrans", Me.SFTDateTrans.Text)
            End If

            .FormTitle = Me.Text
            .ReportFolderName = "Human Report\"
            .ReportName = "OTRequestSlip.rpt"
            .Formular = _Formular
            .Preview()
        End With

    End Sub

    Private Sub wReportHROTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       
    End Sub

End Class