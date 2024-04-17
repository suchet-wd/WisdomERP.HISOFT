Imports DevExpress.Data
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Export
Imports DevExpress.XtraGrid.Views.Grid
Imports System.ComponentModel

Public Class wFabricflowsSetup

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Private Declare Function EmptyWorkingSet Lib "psapi.dll" (ByVal hProcess As IntPtr) As Long
    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal hProcess As IntPtr, ByVal dwMinimumWorkingSetSize As Int32, ByVal dwMaximumWorkingSetSize As Int32) As Int32

    Private ExportStart As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        StateCal = False

        Dim pSuplCode As String = ""
        Dim VendorCode As String = pSuplCode.Replace(",", "','")

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        '_Qry = " EXEC   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETFABRICFLOWS_PO '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(VendorCode) & "'," & FNFabricFlowsListType.SelectedIndex.ToString & " "

        '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)



        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.ogdpo.DataSource = Nothing
            Me.ogdwpo.DataSource = Nothing
            Me.ogdpopayment.DataSource = Nothing


            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvpo)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvwpo)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvpopayment)

            StateCal = False
        Catch ex As Exception
        End Try


    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click

        Call LoadData()

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvpo)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvwpo)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvpopayment)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs)

    End Sub


    'Private Sub ogvtime_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvpo.RowStyle
    '    Try
    '        With Me.ogvpo
    '            If "" & .GetRowCellValue(e.RowHandle, "FTStateExport").ToString = "1" Then

    '                e.Appearance.BackColor = System.Drawing.Color.FromArgb(192, 255, 192)
    '                e.Appearance.ForeColor = System.Drawing.Color.Blue

    '            End If
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub RepCheckEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepCheckEdit.EditValueChanging
    '    Try
    '        Select Case Me.ogvpo.FocusedColumn.FieldName
    '            Case "FTSelect"
    '                Try

    '                    Dim _State As String = "0"
    '                    If e.NewValue.ToString = "1" Then
    '                        _State = "1"
    '                    End If

    '                    Dim pVendor As String = ""
    '                    Dim pinvno As String = ""


    '                    pVendor = ogvpo.GetFocusedRowCellValue("FTVenderCode").ToString()
    '                    pinvno = ogvpo.GetFocusedRowCellValue("invno").ToString()
    '                    With CType(Me.ogdpo.DataSource, DataTable)


    '                        For Each Rx As DataRow In .Select("FTVenderCode='" & HI.UL.ULF.rpQuoted(pVendor) & "' AND invno='" & HI.UL.ULF.rpQuoted(pinvno) & "'")

    '                            Rx!FTSelect = _State

    '                        Next


    '                        .AcceptChanges()
    '                    End With


    '                Catch ex As Exception

    '                End Try

    '        End Select
    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub RepCheckEdit_EditValueChanging(sender As Object, e As ChangingEventArgs)
        Try

            Dim _State As String = "0"
            If e.NewValue.ToString = "1" Then
                _State = "1"
            End If
            Dim pPoNO As String = ""

            Select Case otbmain.SelectedTabPage.Name
                Case otpnew.Name
                    Select Case Me.ogvpo.FocusedColumn.FieldName
                        Case "FTSelect"

                            Try
                                pPoNO = ogvpo.GetFocusedRowCellValue("PONo").ToString()

                                With CType(Me.ogdpo.DataSource, DataTable)

                                    For Each Rx As DataRow In .Select("PONo='" & HI.UL.ULF.rpQuoted(pPoNO) & "' ")

                                        Rx!FTSelect = _State

                                    Next

                                    .AcceptChanges()

                                End With


                            Catch ex As Exception

                            End Try

                    End Select

                Case otpworking.Name

                    Select Case Me.ogvwpo.FocusedColumn.FieldName
                        Case "FTSelect"
                            Try
                                pPoNO = ogvwpo.GetFocusedRowCellValue("PONo").ToString()

                                With CType(Me.ogdwpo.DataSource, DataTable)

                                    For Each Rx As DataRow In .Select("PONo='" & HI.UL.ULF.rpQuoted(pPoNO) & "' ")

                                        Rx!FTSelect = _State

                                    Next

                                    .AcceptChanges()
                                End With


                            Catch ex As Exception

                            End Try

                    End Select

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdownloadpopdf_Click(sender As Object, e As EventArgs)
        Try

            Dim dtpo As New DataTable

            Select Case otbmain.SelectedTabPage.Name
                Case otpnew.Name
                    With CType(Me.ogdpo.DataSource, DataTable)


                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                        dtpo = .Copy
                    End With
                Case otpworking.Name

                    With CType(Me.ogdwpo.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                        dtpo = .Copy
                    End With

            End Select

            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "PDF Files(*.pdf)|*.pdf"

            Op.ShowDialog()
            Try

                If Op.FileName <> "" Then

                    Dim pSpls As New HI.TL.SplashScreen("Loadig...")
                    Dim pFileName As String = Op.FileName
                    If dtpo IsNot Nothing Then

                        Dim RIndx As Integer = 0
                        Dim cmdstring As String = ""
                        Dim dtpoList As DataTable = dtpo.Select("FTSelect='1' ").CopyToDataTable
                        Dim grp As List(Of String) = (dtpoList.Select("FTSelect='1'", "PONo").CopyToDataTable).AsEnumerable() _
                                                              .Select(Function(r) r.Field(Of String)("PONo")) _
                                                              .Distinct() _
                                                              .ToList()


                        For Each pPoNo As String In grp

                            RIndx = RIndx + 1

                            If RIndx = 1 Then
                                cmdstring = " select top 1 FPFile from [WSM-HT-HQ].[HITECH_PURCHASE].[dbo].[TPURTPurchase_PDF]  WITH(NOLOCK)   WHERE FTPurchaseNo='" + HI.UL.ULF.rpQuoted(pPoNo) + "' AND FTStateFIle='1' "
                            Else
                                cmdstring += Constants.vbCrLf + " UNION "
                                cmdstring += Constants.vbCrLf + " select top 1 FPFile from [WSM-HT-HQ].[HITECH_PURCHASE].[dbo].[TPURTPurchase_PDF]  WITH(NOLOCK)   WHERE FTPurchaseNo='" + HI.UL.ULF.rpQuoted(pPoNo) + "' AND FTStateFIle='1' "
                            End If

                        Next


                        Dim dtpdf As New DataTable
                        dtpdf = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                        Dim pdfDocumentProcessor As New DevExpress.Pdf.PdfDocumentProcessor()
                        pdfDocumentProcessor.CreateEmptyDocument(pFileName)

                        For Each R As DataRow In dtpdf.Rows

                            Dim myByteArray As Byte() = CType(R("FPFile"), Byte())
                            Dim Stream As New MemoryStream()
                            Stream.Write(myByteArray, 0, myByteArray.Length)
                            pdfDocumentProcessor.AppendDocument(Stream)

                        Next

                        pdfDocumentProcessor.SaveDocument(pFileName)

                        pSpls.Close()

                        Try
                            Process.Start(pFileName)
                        Catch ex3 As Exception
                            MsgBox(ex3.Message)
                        End Try

                    End If
                End If

            Catch ex2 As Exception
                MsgBox(ex2.Message)
            End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class