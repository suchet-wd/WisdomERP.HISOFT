
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wReportOrderPack

    Private _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

     

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
            Dim _Qry As String = ""
            If Me.FDDate.Text <> "" Then
                _FN = "{V_RptScanCarton.FDInsDate} >='" & HI.UL.ULDate.ConvertEnDB(Me.FDDate.Text) & "'"
            End If
            If Me.FDDateTo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FDInsDate} <='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTo.Text) & "'"
            End If

            If Me.FTOrderNo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTOrderNo} >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTOrderNo} <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If

            If Me.FTSubOrderNo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTSubOrderNo} >='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
            End If
            If Me.FTSubOrderNoTo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTSubOrderNo} <='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoTo.Text) & "'"
            End If

            If Me.FNHSysStyleId.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTStyleCode}  >='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FNHSysStyleIdTo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTStyleCode}  <='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleIdTo.Text) & "'"
            End If

            If Me.FNHSysPOID.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTPORef} >= '" & HI.UL.ULF.rpQuoted(Me.FNHSysPOID.Text) & "'"
            End If
            If Me.FNHSysPOIDTo.Text <> "" Then
                If _FN <> "" Then _FN &= "  AND "
                _FN &= "{V_RptScanCarton.FTPORef} <= '" & HI.UL.ULF.rpQuoted(Me.FNHSysPOIDTo.Text) & "'"
            End If

            Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)
            If _AllReportName <> "" Then
                For Each _ReportName As String In _AllReportName.Split(",")
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                        .Formular = _FN
                        .AddParameter("FDDateStart", Me.FDDate.Text)
                        .AddParameter("FDDateEnd", Me.FDDateTo.Text) 
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


  

    Private Sub wReportStockOnhand_Load(sender As Object, e As EventArgs) Handles Me.Load
        RemoveHandler FDDate.GotFocus, AddressOf HI.TL.HandlerControl.DateEdit_GotFocus
        RemoveHandler FDDateTo.GotFocus, AddressOf HI.TL.HandlerControl.DateEdit_GotFocus
    End Sub
End Class