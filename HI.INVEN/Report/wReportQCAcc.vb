﻿
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wReportQCAcc

    Private _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Me.Name = _SysFormName

        IcCondition.PrePareData()

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
            '************QC Acc No
            '{TINVENQCAcc.FTQCAccNo}

            If Me.FTQCAccNo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "  {TINVENQCAcc.FTQCAccNo} >= '" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"
            End If
            If Me.FTQCAccNoTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "  {TINVENQCAcc.FTQCAccNo} <= '" & HI.UL.ULF.rpQuoted(Me.FTQCAccNoTo.Text) & "'"
            End If


            '************ReceiveNo
            If Me.FTReceiveNo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "  {TINVENQCAcc.FTReceiveNo}  >= '" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            End If

            If Me.FTReceiveNoTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENQCAcc.FTReceiveNo}  <= '" & HI.UL.ULF.rpQuoted(Me.FTReceiveNoTo.Text) & "'"
            End If


            '*************Purchase
            If Me.FTPurchaseNo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENQCAcc.FTPurchaseNo}  >= '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            End If

            If Me.FTPurchaseNoTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENQCAcc.FTPurchaseNo}  <= '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "'"
            End If


            '************Date  
            If Me.FDQCDate.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENQCAcc.FDQCAccDate} >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDQCDate.Text) & "'"
            End If

            If Me.FDQCDateTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TINVENQCAcc.FDQCAccDate} <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDQCDateTo.Text) & "'"
            End If

            '*************User create receive
            'If Me.IcCondition.otpUser.PageVisible Then
            '    If Me.IcCondition.FTUser.Text <> "" Then
            '        _FN &= IIf(_FN.Trim <> "", " AND ", "")
            '        _FN &= " {TINVENReceive.FTReceiveBy} >= '" & HI.UL.ULF.rpQuoted(Me.IcCondition.FTUser.Text) & "'"
            '    End If

            '    If Me.IcCondition.FTUserTo.Text <> "" Then
            '        _FN &= IIf(_FN.Trim <> "", " AND ", "")
            '        _FN &= " {TINVENReceive.FTReceiveBy} <= '" & HI.UL.ULF.rpQuoted(Me.IcCondition.FTUserTo.Text) & "'"
            '    End If
            'End If


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
                        .AddParameter("FDDateStart", Me.FDQCDate.Text)
                        .AddParameter("FDDateEnd", Me.FDQCDateTo.Text)
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
        If IcCondition.otpWhNo.PageVisible Then

            Select Case IcCondition.FNWHCon.SelectedIndex
                Case 1
                    If IcCondition.FNHSysWHId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} >='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysWHId.Text) & "' "
                    End If

                    If IcCondition.FNHSysWHIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} <='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysWHIdTo.Text) & "' "
                    End If

                Case 2

                    tText = ""

                    For Each oRow As DataRow In IcCondition.DbDtWHNo.Rows
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
        If (IcCondition.otpSupl.PageVisible) Then
            Select Case IcCondition.FNSuplCon.SelectedIndex
                Case 1
                    If IcCondition.FNHSysSuplId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_TCNMSupplier.FTSuplCode} >='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysSuplId.Text) & "' "
                    End If

                    If IcCondition.FNHSysSuplIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_TCNMSupplier.FTSuplCode} <='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysSuplIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In IcCondition.DbDtSupl.Rows
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
        If (IcCondition.otpItemCode.PageVisible) Then
            Select Case IcCondition.FNItemCon.SelectedIndex
                Case 1
                    If IcCondition.FNHSysRawMatId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} >='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysRawMatId.Text) & "' "
                    End If

                    If IcCondition.FNHSysRawMatIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} <='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysRawMatIdTo.Text) & "' "
                    End If

                Case 2
                    tText = ""

                    For Each oRow As DataRow In IcCondition.DbDtItemCode.Rows
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
        If (IcCondition.otpColorCode.PageVisible) Then
            Select Case IcCondition.FNColorCon.SelectedIndex
                Case 1
                    If IcCondition.FNHSysRawMatColorId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode} >='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysRawMatColorId.Text) & "' "
                    End If

                    If IcCondition.FNHSysRawMatColorIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode} <='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysRawMatColorIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In IcCondition.DbDtColorCode.Rows
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
        If (IcCondition.otpSizeCode.PageVisible) Then
            Select Case IcCondition.FNSizeCon.SelectedIndex
                Case 1
                    If IcCondition.FNHSysRawMatSizeId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode} >='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysRawMatSizeId.Text) & "' "
                    End If

                    If IcCondition.FNHSysRawMatSizeIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode} <='" & HI.UL.ULF.rpQuoted(IcCondition.FNHSysRawMatSizeIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In IcCondition.DbDtSizeCode.Rows
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

    Private Sub wReportReceive_Load(sender As Object, e As EventArgs) Handles Me.Load
        RemoveHandler FDQCDate.GotFocus, AddressOf HI.TL.HandlerControl.DateEdit_GotFocus
        RemoveHandler FDQCDateTo.GotFocus, AddressOf HI.TL.HandlerControl.DateEdit_GotFocus
    End Sub
End Class