Imports System.Data.SqlClient
Imports System.IO

Public Class wHRReportOverTimeRequest

    Private _LstReport As HI.RP.ListReport

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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

        If Me.SFTDateTrans.Text = "" Or Me.EFTDateTrans.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005200001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        '*****วันที่ทำงาน*********
        If Me.SFTDateTrans.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRTDailyOTRequest.FTDateRequest}>='" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "' "
        End If

        If Me.EFTDateTrans.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRTDailyOTRequest.FTDateRequest}<='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "' "
        End If

        '*****วันที่เริ่มงาน*********
        If Me.FDWorkStart.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateStart}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDWorkStart.Text) & "' "
        End If

        If Me.FDWorkEnd.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateStart}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDWorkEnd.Text) & "' "
        End If

        '*****วันที่ลาออก*********
        If Me.FDResignStart.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateEnd}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDResignStart.Text) & "' "
        End If

        If Me.FDResignEnd.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateEnd}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDResignEnd.Text) & "' "
        End If

        '*****วันเกิด*********
        If Me.FDBirthStart.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDBirthDate}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDBirthStart.Text) & "' "
        End If

        If Me.FDBirthEnd.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDBirthDate}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDBirthEnd.Text) & "' "
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

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report

                    '*****วันที่ทำงาน*********
                    If Me.SFTDateTrans.Text <> "" Then
                        .AddParameter("SFTDateTrans", Me.SFTDateTrans.Text)
                    End If

                    If Me.EFTDateTrans.Text <> "" Then
                        .AddParameter("EFTDateTrans", Me.EFTDateTrans.Text)
                    End If

                    '*****วันที่เริ่มงาน*********
                    If Me.FDWorkStart.Text <> "" Then
                        .AddParameter("SFDDateStart", Me.FDWorkStart.Text)
                    End If

                    If Me.FDWorkEnd.Text <> "" Then
                        .AddParameter("EFDDateStart", Me.FDWorkEnd.Text)
                    End If

                    '*****วันที่ลาออก*********
                    If Me.FDResignStart.Text <> "" Then
                        .AddParameter("SFDDateEnd", Me.FDResignStart.Text)
                    End If

                    If Me.FDResignEnd.Text <> "" Then
                        .AddParameter("EFDDateEnd", Me.FDResignEnd.Text)
                    End If

                    '*****วันเกิด*********
                    If Me.FDBirthStart.Text <> "" Then
                        .AddParameter("SFDBirthDate", Me.FDBirthStart.Text)
                    End If

                    If Me.FDBirthEnd.Text <> "" Then
                        .AddParameter("EFDBirthDate", Me.FDBirthEnd.Text)
                    End If

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

    Private Sub wReportHROTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class