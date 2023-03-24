Imports System.Data.SqlClient
Imports System.IO
Public Class wHRReportVisitor
    Private _LstReport As HI.RP.ListReport

    Sub New()

        InitializeComponent()

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
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMVisitor"
        _Qry &= vbCrLf & " where (FTNumber<>0) "

        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
        HI.Conn.SQLConn.Tran.Commit()
        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        If Me.StartSeq.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, SeqStart_lb.Text)
            Me.StartSeq.Focus()
            Exit Sub
        End If
        If Me.EndSeq.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, SeqEnd_lbl.Text)
            Me.EndSeq.Focus()
            Exit Sub
           
        Else

            For Sequence As Integer = Convert.ToDecimal(StartSeq.Text) To Convert.ToDecimal(EndSeq.Text)
                Debug.Write(Sequence.ToString)

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMVisitor"
                _Qry &= vbCrLf & " (FTNumber) "
                _Qry &= vbCrLf & "values(" + Sequence.ToString + ")"

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Next
            Debug.WriteLine("")
        End If



        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        If _AllReportName <> "" Then
            Call HI.ST.Security.CreateTempEmpMaster(components)

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(components)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report




                    If Me.StartSeq.Text <> "" Then
                        .AddParameter("f", Me.StartSeq.Text)
                    End If
                    If Me.EndSeq.Text <> "" Then
                        .AddParameter("e", Me.EndSeq.Text)
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

    'Private Sub wReportHRTrans_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    Try
    '        If FNReportname.Properties.Items.Count < 0 Then
    '            MsgBox("ไม่พบการกำหนด File Report !!!")
    '            Me.Close()
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

End Class