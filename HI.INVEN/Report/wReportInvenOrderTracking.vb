Public Class wReportInvenOrderTracking 

    Private _LstReport As HI.RP.ListReport
    Sub New(Optional _SysFormName As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _SysFormName <> "" Then
            Me.Name = _SysFormName
        End If

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

        If Me.FTOrderNo.Text = "" Or Me.FTOrderNoTo.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1406180071, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        Dim _Qry As String = ""
        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Order_Tracking_Report '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0,0,'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "','',''"
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Formular = "{TMERTOrder_Resource.FTUserLogIn} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {TMERTOrder.FTOrderNo}>='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {TMERTOrder.FTOrderNo}<='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "' "


        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        If _AllReportName <> "" Then

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

    Private Sub wReportHRByPayRoll_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

End Class