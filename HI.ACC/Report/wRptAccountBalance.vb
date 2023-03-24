Public Class wRptAccountBalance 

    Private _LstReport As HI.RP.ListReport
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        IcCondition.PrePareData()

        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If

        Me.AsOfDate.DateTime = Date.Now

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If VerrifyData() Then
                Me.Preview()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Preview()
        Try
            Dim _spls As New HI.TL.SplashScreen("Loading... Report.Please Wait.", "Preview Report")

            Dim tText As String = ""
            Dim _FM As String = ""
            Dim SDate As String = ""
            Dim EDate As String = ""
            Dim _Cmd As String = ""
            _Cmd = "Select "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= "FTNameTH"
            Else
                _Cmd &= "FTNameEN"
            End If
            _Cmd &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTListName='FNMonth'  AND FNListIndex="


            EDate = HI.UL.ULDate.ConvertEnDB(Me.AsOfDate.Text)
            SDate = Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB(Me.AsOfDate.Text), 8) & "01"
            Dim _Month As String = HI.Conn.SQLConn.GetField(_Cmd & CType(EDate, Date).Month.ToString, Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _AsOfDate As String = CType(EDate, Date).Day.ToString & " " & _Month & " " & (CInt(CType(EDate, Date).Year) + 543).ToString

            Dim _Qry As String = ""
            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Stock_Onhand_Acc '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','','ZZZZZZ','','ZZZZZZZ','','','" & SDate & "','" & EDate & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            Select Case IcCondition.FNWHCon.SelectedIndex
                Case 1
                    If IcCondition.FNHSysWHId.Text <> "" Then
                        _FM &= IIf(_FM.Trim <> "", " AND ", "")
                        _FM &= "  {V_Rpt_Balance_Account.FTWHCode}  >='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysWHId.Text) & "' "
                    End If

                    If IcCondition.FNHSysWHIdTo.Text <> "" Then
                        _FM &= IIf(_FM.Trim <> "", " AND ", "")
                        _FM &= "  {V_Rpt_Balance_Account.FTWHCode} <='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysWHIdTo.Text) & "' "
                    End If

                Case 2

                    tText = ""

                    For Each oRow As DataRow In IcCondition.DbDtWHNo.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _FM &= IIf(_FM.Trim <> "", " AND ", "")
                        _FM &= "  {V_Rpt_Balance_Account.FTWHCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select


            If Me.FNHSysMatTypeId.Text <> "" Then
                _FM &= IIf(_FM.Trim <> "", " AND ", "")
                _FM &= "  {V_Rpt_Balance_Account.FTMatTypeCode}  >='" & HI.UL.ULF.rpQuoted(Me.FNHSysMatTypeId.Text) & "' "
            End If

            If Me.FNHSysMatTypeIdTo.Text <> "" Then
                _FM &= IIf(_FM.Trim <> "", " AND ", "")
                _FM &= "  {V_Rpt_Balance_Account.FTMatTypeCode}  <='" & HI.UL.ULF.rpQuoted(Me.FNHSysMatTypeIdTo.Text) & "' "
            End If

            If Me.FNHSysMatTypeIdTo.Text <> "" Then
                _FM &= IIf(_FM.Trim <> "", " AND ", "")
                _FM &= "  {V_Rpt_Balance_Account.FTMatTypeCode}  <='" & HI.UL.ULF.rpQuoted(Me.FNHSysMatTypeIdTo.Text) & "' "
            End If
            _FM &= IIf(_FM.Trim <> "", " AND ", "")
            _FM &= "  {V_Rpt_Balance_Account.FTUserLogIn}  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)
            If _AllReportName <> "" Then
                For Each _ReportName As String In _AllReportName.Split(",")
                    With New HI.RP.Report
                        _spls.Close()
                        .FormTitle = Me.Text
                        .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                        .Formular = _FM
                        .AddParameter("AsOfDate", _AsOfDate.ToString)

                        .ReportName = _ReportName
                        .Preview()
                    End With
                Next
            Else
                HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.AsOfDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, AsOfDate_lbl.Text)
                Me.AsOfDate.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

  
    Private Sub wRptAccountBalance_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class