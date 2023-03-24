
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wReportPO

    Private _LstReport As HI.RP.ListReport


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Me.Name = _SysFormName


        PoCondition1.PrePareData()


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





            '*************Purchase
            If Me.FTPurchaseNo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TPURTPurchase.FTPurchaseNo}  >= '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            End If

            If Me.FTPurchaseNoTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TPURTPurchase.FTPurchaseNo}  <= '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "'"
            End If


            '************Date Receive
            If Me.FDPODate.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TPURTPurchase.FDPurchaseDate} >= '" & HI.UL.ULDate.ConvertEnDB(Me.FDPODate.Text) & "'"
            End If

            If Me.FDPODateTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= " {TPURTPurchase.FDPurchaseDate} <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDPODateTo.Text) & "'"
            End If

            '*************User create receive
            If Me.PoCondition1.otpUser.PageVisible Then

                If Me.PoCondition1.FTUser.Text <> "" Then
                    _FN &= IIf(_FN.Trim <> "", " AND ", "")
                    _FN &= " {TPURTPurchase.FTPurchaseBy} >= '" & HI.UL.ULF.rpQuoted(Me.PoCondition1.FTUser.Text) & "'"
                End If

                If Me.PoCondition1.FTUserTo.Text <> "" Then
                    _FN &= IIf(_FN.Trim <> "", " AND ", "")
                    _FN &= " {TPURTPurchase.FTPurchaseBy} <= '" & HI.UL.ULF.rpQuoted(Me.PoCondition1.FTUserTo.Text) & "'"
                End If
            End If

            '***********Delivery Place
            If Me.FTDeliveryPlace.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "{vwPOHeader.DeliveryPlace}  >='" & HI.UL.ULF.rpQuoted(Me.FTDeliveryPlace.Text) & "'"
            End If

            If Me.FTDeliveryPlaceTo.Text <> "" Then
                _FN &= IIf(_FN.Trim <> "", " AND ", "")
                _FN &= "{vwPOHeader.DeliveryPlace}  <='" & HI.UL.ULF.rpQuoted(Me.FTDeliveryPlaceTo.Text) & "'"
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
                        .AddParameter("FDDateStart", Me.FDPODate.Text)
                        .AddParameter("FDDateEnd", Me.FDPODateTo.Text)
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


        If PoCondition1.otpWhNo.PageVisible Then

            Select Case PoCondition1.FNWHCon.SelectedIndex
                Case 1
                    If PoCondition1.FNHSysWHId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} >='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysWHId.Text) & "' "
                    End If

                    If PoCondition1.FNHSysWHIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Warehouse.FTWHCode} <='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysWHIdTo.Text) & "' "
                    End If

                Case 2

                    tText = ""

                    For Each oRow As DataRow In PoCondition1.DbDtWHNo.Rows
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

        If (PoCondition1.otpSupl.PageVisible) Then
            Select Case PoCondition1.FNSuplCon.SelectedIndex
                Case 1
                    If PoCondition1.FNHSysSuplId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMSupplier.FTSuplCode} >='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysSuplId.Text) & "' "
                    End If

                    If PoCondition1.FNHSysSuplIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMSupplier.FTSuplCode} <='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysSuplIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In PoCondition1.DbDtSupl.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {TCNMSupplier.FTSuplCode} IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '*******Item********
        If (PoCondition1.otpItemCode.PageVisible) Then
            Select Case PoCondition1.FNItemCon.SelectedIndex
                Case 1
                    If PoCondition1.FNHSysRawMatId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} >='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysRawMatId.Text) & "' "
                    End If

                    If PoCondition1.FNHSysRawMatIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatCode} <='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysRawMatIdTo.Text) & "' "
                    End If

                Case 2
                    tText = ""

                    For Each oRow As DataRow In PoCondition1.DbDtItemCode.Rows
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
        If (PoCondition1.otpColorCode.PageVisible) Then
            Select Case PoCondition1.FNColorCon.SelectedIndex
                Case 1
                    If PoCondition1.FNHSysRawMatColorId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode}  >='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysRawMatColorId.Text) & "' "
                    End If

                    If PoCondition1.FNHSysRawMatColorIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode}  <='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysRawMatColorIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In PoCondition1.DbDtColorCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatColorCode}  IN['" & tText.Replace("|", "','") & "'] "
                    End If
            End Select
        End If

        '******Size***********
        If (PoCondition1.otpSizeCode.PageVisible) Then
            Select Case PoCondition1.FNSizeCon.SelectedIndex
                Case 1
                    If PoCondition1.FNHSysRawMatSizeId.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode}  >='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysRawMatSizeId.Text) & "' "
                    End If

                    If PoCondition1.FNHSysRawMatSizeIdTo.Text <> "" Then
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode}  <='" & HI.UL.ULF.rpQuoted(PoCondition1.FNHSysRawMatSizeIdTo.Text) & "' "
                    End If
                Case 2
                    tText = ""

                    For Each oRow As DataRow In PoCondition1.DbDtSizeCode.Rows
                        tText &= oRow("FTCode") & "|"
                    Next

                    If tText.Trim <> "" Then
                        tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                        _Criteria &= IIf(_Criteria.Trim <> "", " AND ", "")
                        _Criteria &= "  {V_Material.FTRawMatSizeCode}  IN['" & tText.Replace("|", "','") & "'] "
                    End If

            End Select
        End If

        Return _Criteria
    End Function

    Private Sub wReportPO_Load(sender As Object, e As EventArgs) Handles Me.Load
        RemoveHandler FDPODate.GotFocus, AddressOf HI.TL.HandlerControl.DateEdit_GotFocus
        RemoveHandler FDPODateTo.GotFocus, AddressOf HI.TL.HandlerControl.DateEdit_GotFocus
    End Sub
End Class