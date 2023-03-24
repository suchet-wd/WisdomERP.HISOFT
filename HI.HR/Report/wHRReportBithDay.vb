Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.XtraEditors


Public Class wHRReportBithDay

    Private _LstReport As HI.RP.ListReport
    Sub New(_SysFormName As String)

        ' This call is required by the designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call.

        Me.Name = _SysFormName

        Condition.PrePareData()
        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If
        LoadListData()
        LoadListData2()

    End Sub

    Private Sub LoadListData()
        Try
            Dim _Qry As String
            Dim Dt As DataTable

            '_Qry = "SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN, FTReferCode, FTCallMnuName, FTCallMethodName"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry = "SELECT  FTNameTH  as   FTName"
            Else
                _Qry = "SELECT  FTNameEN  as   FTName"
            End If
            _Qry &= vbCrLf & "  FROM    HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNMonth')"

            Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
            FNMonthS.Properties.Items.Clear()
            FNMonthS.Properties.Items.Add("")
            For Each r As DataRow In Dt.Rows
                FNMonthS.Properties.Items.Add(r!FTName.ToString)
            Next
            FNMonthS.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadListData2()
        Try
            Dim _Qry As String
            Dim Dt As DataTable

            '_Qry = "SELECT        FTListName, FNListIndex, FTNameTH, FTNameEN, FTReferCode, FTCallMnuName, FTCallMethodName"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry = "SELECT  FTNameTH  as   FTName"
            Else
                _Qry = "SELECT  FTNameEN  as   FTName"
            End If
            _Qry &= vbCrLf & "  FROM    HSysListData"
            _Qry &= vbCrLf & "WHERE        (FTListName = N'FNMonth')"

            Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
            FNMonthE.Properties.Items.Clear()
            FNMonthE.Properties.Items.Add("")
            For Each r As DataRow In Dt.Rows
                FNMonthE.Properties.Items.Add(r!FTName.ToString)
            Next
            FNMonthE.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub
   

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""
        Dim tText As String = ""
        tText = Condition.GetCriteria

        If Me.FNMonthS.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FDBirthStart_lbl.Text)
            Me.FNMonthS.Focus()
            Exit Sub
        End If

        If Me.FNMonthE.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FDBirthEnd_lbl.Text)
            Me.FNMonthE.Focus()
            Exit Sub
        End If

        If FNMonthS.SelectedIndex > FNMonthE.SelectedIndex Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FDBirthStart_lbl.Text)
            Me.FNMonthS.Focus()
            Exit Sub
        End If

        Dim _SMonth As String = "" & Microsoft.VisualBasic.Right("0" & FNMonthS.SelectedIndex.ToString, 2) & "/"
        Dim _EMonth As String = "" & Microsoft.VisualBasic.Right("0" & FNMonthE.SelectedIndex.ToString, 2) & "/"


        If Me.FDay.Text <> "" Then
            _Formular &= "  mid({THRMEmployee.FDBirthDate},6,5)  >= '" & _SMonth & Microsoft.VisualBasic.Right("0" & FDay.SelectedIndex.ToString, 2) & "'"
        Else
            _Formular &= "  mid({THRMEmployee.FDBirthDate},6,5)  >= '" & _SMonth & "01" & "'"
        End If

        If Me.EDay.Text <> "" Then
            _Formular &= " and mid({THRMEmployee.FDBirthDate},6,5)<= '" & _EMonth & Microsoft.VisualBasic.Right("0" & EDay.SelectedIndex.ToString, 2) & "'"

        Else
            _Formular &= " and mid({THRMEmployee.FDBirthDate},6,5) <= '" & _EMonth & "31" & "'"
        End If

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

                    '*****วันเกิด*********

                    .AddParameter("fday", Me.FDay.Text)
                    .AddParameter("eday", Me.EDay.Text)
                    .AddParameter("SDate", Me.FNMonthS.Text)
                    .AddParameter("EDate", Me.FNMonthE.Text)

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

    Private Sub wReportHRTrans_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FDay_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles FDay.MouseClick

    End Sub
End Class