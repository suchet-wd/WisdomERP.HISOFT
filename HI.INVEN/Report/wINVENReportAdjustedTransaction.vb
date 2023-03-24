
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wINVENReportAdjustedTransaction


    Private _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Me.Name = _SysFormName

        ICCondition.PrePareData()

        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Me._Preview()
    End Sub

    Private Sub _Preview()
        Try
            Dim _FN As String = ""

            '************ReceiveNo
            If Me.FTAdjustStockNo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "  {TINVENAdjustStock.FTAdjustStockNo} >= '" & HI.UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"
            End If

            If Me.FTAdjustStockNoTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENAdjustStock.FTAdjustStockNo} <= '" & HI.UL.ULF.rpQuoted(Me.FTAdjustStockNoTo.Text) & "'"
            End If

            '************Date Receive
            If Me.FDSAdjustStockDate.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENAdjustStock.FDAdjustStockDate} >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDSAdjustStockDate.Text) & "'"
            End If

            If Me.FDEAdjustStockDateTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENAdjustStock.FDAdjustStockDate} <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEAdjustStockDateTo.Text) & "'"
            End If

            '*************User create receive
            If Me.IcCondition.otpUser.PageVisible Then
                If Me.IcCondition.FTUser.Text <> "" Then
                    _FN &= IIf(_FN.Trim <> "", " AND ", "")
                    _FN &= " {TINVENAdjustStock.FDAdjustStockBy} >= '" & HI.UL.ULF.rpQuoted(Me.IcCondition.FTUser.Text) & "'"
                End If

                If Me.IcCondition.FTUserTo.Text <> "" Then
                    _FN &= IIf(_FN.Trim <> "", " AND ", "")
                    _FN &= " {TINVENAdjustStock.FDAdjustStockBy} <= '" & HI.UL.ULF.rpQuoted(Me.IcCondition.FTUserTo.Text) & "'"
                End If
            End If


            Dim tText As String = ""
            tText = GetCri()

            If tText <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "" & tText
            End If

            Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)
            If _AllReportName <> "" Then
                For Each _ReportName As String In _AllReportName.Split(",")
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                        .Formular = _FN
                        .AddParameter("RcvDate", Me.FDSAdjustStockDate.Text)
                        .AddParameter("RcvDateTo", Me.FDEAdjustStockDateTo.Text)
                        '.Formular = " {TRPTTmpCauseResign.UserLogin}='" & HI.ST.UserInfo.UserName & "'  "
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


    Private Function GetCri() As String
        Dim _Criteria As String = ""
        Dim tText As String = ""
        '*********Ware House*********
        If ICCondition.otpWhNo.PageVisible Then

            Select Case ICCondition.FNWHCon.SelectedIndex
                Case 1
                    If ICCondition.FNHSysWHId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} >='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysWHId.Text) & "' "
                    End If

                    If ICCondition.FNHSysWHIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} <='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysWHIdTo.Text) & "' "
                    End If

                Case 2

                    tText = ""

                    For Each oRow As DataRow In ICCondition.DbDtWHNo.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select

        End If

        '********Supl*********
        If (ICCondition.otpSupl.PageVisible) Then
            Select Case ICCondition.FNSuplCon.SelectedIndex
                Case 1
                    If ICCondition.FNHSysSuplId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_TCNMSupplier.FTSuplCode} >='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysSuplId.Text) & "' "
                    End If

                    If ICCondition.FNHSysSuplIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_TCNMSupplier.FTSuplCode} <='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysSuplIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In ICCondition.DbDtSupl.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_TCNMSupplier.FTSuplCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '*******Item********
        If (ICCondition.otpItemCode.PageVisible) Then
            Select Case ICCondition.FNItemCon.SelectedIndex
                Case 1
                    If ICCondition.FNHSysRawMatId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} >='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysRawMatId.Text) & "' "
                    End If

                    If ICCondition.FNHSysRawMatIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} <='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysRawMatIdTo.Text) & "' "
                    End If

                Case 2
                    tText = ""

                    For Each oRow As DataRow In ICCondition.DbDtItemCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '******Color*********
        If (ICCondition.otpColorCode.PageVisible) Then
            Select Case ICCondition.FNColorCon.SelectedIndex
                Case 1
                    If ICCondition.FNHSysRawMatColorId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode} >='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysRawMatColorId.Text) & "' "
                    End If

                    If ICCondition.FNHSysRawMatColorIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode} <='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysRawMatColorIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In ICCondition.DbDtColorCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '******Size***********
        If (ICCondition.otpSizeCode.PageVisible) Then
            Select Case ICCondition.FNSizeCon.SelectedIndex
                Case 1
                    If ICCondition.FNHSysRawMatSizeId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode} >='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysRawMatSizeId.Text) & "' "
                    End If

                    If ICCondition.FNHSysRawMatSizeIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode} <='" & HI.UL.ULF.rpQuoted(ICCondition.FNHSysRawMatSizeIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In ICCondition.DbDtSizeCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If

            End Select
        End If

        Return _Criteria
    End Function


End Class