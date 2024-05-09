Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.Export

Public Class wInterfaceToAx

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _StateQtyBySPM As Boolean = False  ' get Qty by Super market

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Initial Grid"
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub


#End Region
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

        Me.ogcmonthly.DataSource = Nothing
        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        StateCal = False
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            Call LoaddataDetailColorSizeByLine()
            'Call LoaddataDetailColorSizeByLine_byline()
        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoaddataDetailColorSizeByLine()
        Dim _Qry As String
        Dim _dt As System.Data.DataTable

        Try
            Select Case Me.ogcdetailcolorsizeline.SelectedTabPageIndex
                Case 0
                    ogcmonthly.DataSource = Nothing
                    _Qry = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..SP_GET_DataToAx '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' , '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'," & Val(HI.ST.SysInfo.CmpID)
                    _Qry &= vbCrLf & " ,'%" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "%' " ', '" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'"
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    Me.ogcmonthly.DataSource = _dt


                Case Else

                    ogcbomjournal.DataSource = Nothing
                    _Qry = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..SP_GET_DataToAx_BomJournal '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "' , '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'," & Val(HI.ST.SysInfo.CmpID)
                    _Qry &= vbCrLf & " ,'%" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "%' " ' , '" & HI.UL.ULF.rpQuoted(FTCustomerPOTo.Text) & "'"
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


                    Me.ogcbomjournal.DataSource = _dt

            End Select





        Catch ex As Exception
        End Try

    End Sub



    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        'If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        If Me.FTOrderNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" Then
            _Pass = True
        End If

        'If Me.FTCustomerPO.Text <> "" And FTCustomerPO.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTCustomerPOTo.Text <> "" And FTCustomerPOTo.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSSendSuplDate.Text <> "" And FTESendSuplDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSRcvSuplDate.Text <> "" And FTERcvSuplDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSSMKDate.Text <> "" And FTESMKDate.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTStartShipment.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTEndShipment.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTStartDateScanIn.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTEndDateScanIn.Text <> "" Then
        '    _Pass = True
        'End If

        If Me.FTEndDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPO.Text <> "" Then
            _Pass = True
        End If



        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvmonthly)
            StateCal = False

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then

            Call LoadData()
            Me.ogvmonthly.BestFitColumns()

        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvmonthly)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try

            Dim _FileName As String = ""
            Dim folderDlg As New SaveFileDialog
            With folderDlg
                .Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"
                .FilterIndex = 1
                Dim dr As DialogResult = .ShowDialog()
                If (dr = System.Windows.Forms.DialogResult.OK) Then
                    Dim _Spls As New HI.TL.SplashScreen("Please Wait.....", "Export Data From File ")

                    Dim path As String = .FileNames(0).ToString
                    Dim _Strm As Stream = New MemoryStream()
                    DevExpress.Export.ExportSettings.DefaultExportType = ExportType.WYSIWYG

                    Select Case Me.ogcdetailcolorsizeline.SelectedTabPageIndex
                        Case 0
                            ogcmonthly.ExportToXlsx(path)
                        Case Else
                            ogvbomjournal.ExportToXlsx(path)

                    End Select


                    Process.Start(path)
                    _Spls.Close()
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ogvmonthly_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvmonthly.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = e.RowHandle
                If category Mod 2 = 1 Then
                    e.Appearance.BackColor = Color.White
                    e.Appearance.BackColor2 = Color.AliceBlue
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvmonthly_RowStyles(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvbomjournal.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = e.RowHandle
                If category Mod 2 = 1 Then
                    e.Appearance.BackColor = Color.White
                    e.Appearance.BackColor2 = Color.AliceBlue
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class