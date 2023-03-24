Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports FFS.BusinessObjects
Imports FFSReports

Public Class wImportBooking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            '.OptionsSelection.MultiSelect = True
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .ExpandAllGroups()
            .RefreshData()


        End With




    End Sub
#End Region
    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property

    Private _WhId As Integer = 0
    Public Property WhId As Integer
        Get
            Return _WhId
        End Get
        Set(value As Integer)
            _WhId = value
        End Set
    End Property

    Public _CmpId As Integer = 0
    Public Property CmpId As Integer
        Get
            Return _CmpId
        End Get
        Set(value As Integer)
            _CmpId = value
        End Set
    End Property

    Private _StateSetSelectAll As Boolean = True



    Private _oDtselect As DataTable = Nothing
    Public Property oDtselect As DataTable
        Get
            Return _oDtselect
        End Get
        Set(value As DataTable)
            _oDtselect = value
        End Set
    End Property
    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property


    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = ""
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.FTFilePath.Text = .FileName
                Else
                    Me.FTFilePath.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
            Dim _Cmd As String = ""
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = True
                Dim dr As DialogResult = .ShowDialog()
                If (dr = System.Windows.Forms.DialogResult.OK) Then

                    Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
                    For Each file In .FileNames
                        ' _FileName = .FileName

                        Call ReadXlsfile_Multiple(file, _Spls)

                    Next
                    _Spls.Close()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ReadXlsfile_Multiple(_fileName As String, ByVal _Spls As HI.TL.SplashScreen)
        Try
            Dim _TabPageSubHead As New DevExpress.XtraTab.XtraTabPage
            Dim _GridDM As New DevExpress.XtraGrid.GridControl
            Dim _GridVDM As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GridDD As New DevExpress.XtraGrid.GridControl
            Dim _GridVDD As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GrpSum As New DevExpress.XtraEditors.GroupControl
            Dim _GrpDetail As New DevExpress.XtraEditors.GroupControl
            Dim _GrpInfo As New DevExpress.XtraEditors.GroupControl



            Dim _oDt As New System.Data.DataTable
            Dim _oDtIn As New System.Data.DataTable

            Dim _Qry As String = ""
            Dim _RowDes As Integer = 0
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            _oDt = HI.UL.ReadExcel.Read(_fileName, "Sheet1", -1)

            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FTInvoiceNo", GetType(String))
                .Columns.Add("FTInvoiceRefNo", GetType(String))
                .Columns.Add("FTBookingNo", GetType(String))
                .Columns.Add("FTPORef", GetType(String))
                .Columns.Add("FTNikePOLineItem", GetType(String))
            End With
            Dim dr As DataRow



            For Each R As DataRow In _oDt.Rows
                If IsNumeric(Microsoft.VisualBasic.Left((R.Item(5).ToString), 2)) Then
                    dr = _dt.NewRow()
                    dr.Item("FTInvoiceNo") = R.Item(5).ToString
                    dr.Item("FTInvoiceRefNo") = R.Item(5).ToString
                    dr.Item("FTBookingNo") = R.Item(4).ToString
                    dr.Item("FTPORef") = R.Item(26).ToString
                    dr.Item("FTNikePOLineItem") = R.Item(21).ToString
                    _dt.Rows.Add(dr)
                End If


            Next

            Me.ogc.DataSource = _dt

            'TabSub  


        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub
    Private Sub ocmimportexcel_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Dim _Cmd As String = "" : Dim _Rowaccept As Integer = 0
            With DirectCast(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    _Cmd = "Update " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TEXPTCMInvoice_Ref "
                    _Cmd &= vbCrLf & " set FTBookingNo='" & HI.UL.ULF.rpQuoted(R!FTBookingNo.ToString) & "'"
                    _Cmd &= vbCrLf & " where FTInvoiceRefNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceRefNo.ToString) & "'"
                    If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT) Then
                        _Rowaccept += +1
                    End If
                Next
            End With
            HI.MG.ShowMsg.mInfo("บันทึกเลขที่บุ๊กกิ๊งเรียบร้อย  ทั้งหมด ", 1911051656, Me.Text, _Rowaccept.ToString)
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End Try
    End Sub
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            Me.ogc.DataSource = Nothing
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class