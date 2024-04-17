Imports DevExpress.Data
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Export
Imports DevExpress.XtraGrid.Views.Grid

Public Class wPurchasePackingList

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

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        _Qry = " EXEC   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_PACKINGLIST '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','','" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "',''," & FNPackingListType.SelectedIndex.ToString & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogdtime.DataSource = _dt.Copy
        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)


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
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogdtime
                If Not (.DataSource Is Nothing) And ogvtime.RowCount > 0 Then

                    Dim dtdata As New DataTable
                    dtdata.Columns.Add("Vendor", GetType(String))
                    dtdata.Columns.Add("invno", GetType(String))

                    Dim pVendor As String = ""
                    Dim pinvno As String = ""

                    With ogvtime
                        For I As Integer = 0 To .RowCount - 1

                            pVendor = .GetRowCellValue(I, "FTVenderCode").ToString()
                            pinvno = .GetRowCellValue(I, "invno").ToString()

                            If dtdata.Select("Vendor='" & HI.UL.ULF.rpQuoted(pVendor) & "' AND invno='" & HI.UL.ULF.rpQuoted(pinvno) & "'").Length <= 0 Then

                                dtdata.Rows.Add(pVendor, pinvno)

                            End If

                            ' .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)

                        Next
                    End With

                    With CType(.DataSource, DataTable)

                        For Each R As DataRow In dtdata.Rows

                            For Each Rx As DataRow In .Select("FTVenderCode='" & HI.UL.ULF.rpQuoted(R!Vendor.ToString) & "' AND invno='" & HI.UL.ULF.rpQuoted(R!invno.ToString) & "'")

                                Rx!FTSelect = _State

                            Next

                        Next

                        .AcceptChanges()
                    End With


                    ' CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            ogcexport.DataSource = Nothing
            Dim dtpoList As New DataTable
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
                    Exit Sub
                End If

                dtpoList = .Select("FTSelect='1'").CopyToDataTable
            End With


            Try
                Dim Op As New System.Windows.Forms.SaveFileDialog
                Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                '         "Excel Files(.xls)|*.xls| 
                'Excel Files(.xlsx)|*.xlsx| Excel Files(*.xlsm)|*.xlsm";
                Op.ShowDialog()
                Try
                    If Op.FileName <> "" Then



                        Dim _Spls As New HI.TL.SplashScreen("Exporting...   Please Wait   ")

                        Dim grp As List(Of String) = (dtpoList.Select("FTVenderCode<>''", "FTVenderCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTVenderCode")) _
                                                   .Distinct() _
                                                   .ToList()

                        Dim dtdata As New DataTable
                        dtdata.Columns.Add("Vendor", GetType(String))
                        dtdata.Columns.Add("invno", GetType(String))
                        For Each Ind As String In grp

                            Dim grpinvoice As List(Of String) = (dtpoList.Select("FTVenderCode='" & HI.UL.ULF.rpQuoted(Ind) & "'", "invno").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("invno")) _
                                                   .Distinct() _
                                                   .ToList()
                            For Each Indinvoice As String In grpinvoice
                                dtdata.Rows.Add(Ind, Indinvoice)
                            Next

                        Next

                        HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORT_TEMPVENDORIVOICE", "@tblOrder", dtdata, Conn.DB.DataBaseName.DB_PUR)


                        Dim _Qry As String
                        _Qry = " EXEC   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_PACKINGLIST_EXPORT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0 "
                        Dim _dt As DataTable
                        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                        Me.ogcexport.DataSource = _dt.Copy
                        _dt.Dispose()

                        Me.ogdtime.DataSource = Nothing
                        Me.ochkselectall.Checked = False
                        Me.LoadData()
                        _Spls.Close()

                        DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                        With Me.ogcexport

                            'Dim _XlsxExportOption As New DevExpress.XtraPrinting.XlsxExportOptions()
                            '_XlsxExportOption.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text

                            '.ExportToXlsx(Op.FileName, _XlsxExportOption)


                            ExportStart = True
                            Dim Clearmemory As New Threading.Thread(AddressOf ClearmemoryStart)
                            Clearmemory.IsBackground = True
                            Clearmemory.Start()

                            Try
                                .ExportToXlsx(Op.FileName)
                            Catch ex As Exception
                            End Try

                            Clearmemory.Abort()

                            ExportStart = False

                            '.ExportToXlsx(Op.FileName)

                            Try
                                Process.Start(Op.FileName)
                            Catch ex As Exception
                            End Try

                        End With
                    End If
                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try


        Catch ex As Exception

        End Try
    End Sub


    Private Sub ClearmemoryStart()

        While (ExportStart = True)

            GC.WaitForPendingFinalizers()
            GC.RemoveMemoryPressure(GC.GetTotalMemory(True))
            EmptyWorkingSet(Process.GetCurrentProcess.Handle)

            SetProcessWorkingSetSize(Process.GetCurrentProcess.Handle, -1, -1)

            System.Threading.Thread.Sleep(1000)

        End While

    End Sub

    Private Sub ogvtime_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvtime.RowStyle
        Try
            With Me.ogvtime
                If "" & .GetRowCellValue(e.RowHandle, "FTStateExport").ToString = "1" Then

                    e.Appearance.BackColor = System.Drawing.Color.FromArgb(192, 255, 192)
                    e.Appearance.ForeColor = System.Drawing.Color.Blue

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepCheckEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepCheckEdit.EditValueChanging
        Try
            Select Case Me.ogvtime.FocusedColumn.FieldName
                Case "FTSelect"
                    Try

                        Dim _State As String = "0"
                        If e.NewValue.ToString = "1" Then
                            _State = "1"
                        End If

                        Dim pVendor As String = ""
                        Dim pinvno As String = ""


                        pVendor = ogvtime.GetFocusedRowCellValue("FTVenderCode").ToString()
                        pinvno = ogvtime.GetFocusedRowCellValue("invno").ToString()
                        With CType(Me.ogdtime.DataSource, DataTable)


                            For Each Rx As DataRow In .Select("FTVenderCode='" & HI.UL.ULF.rpQuoted(pVendor) & "' AND invno='" & HI.UL.ULF.rpQuoted(pinvno) & "'")

                                Rx!FTSelect = _State

                            Next


                            .AcceptChanges()
                        End With


                    Catch ex As Exception

                    End Try

            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class