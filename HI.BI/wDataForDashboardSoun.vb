Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class wDataForDashboardSoun


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT   '0' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameTH"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        Else

            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameEN"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub


#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub

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

        ogc1.DataSource = Nothing
        ogc2.DataSource = Nothing
        ogc3.DataSource = Nothing
        ogc4.DataSource = Nothing
        ogc5.DataSource = Nothing
        ogc6.DataSource = Nothing

        Dim dts As New DataSet
        Dim Ridx As Integer = 0

        Dim dt1show As New DataTable
        Dim dt2show As New DataTable
        Dim dt3show As New DataTable
        Dim dt4show As New DataTable
        Dim dt5show As New DataTable
        Dim dt6show As New DataTable

        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt4 As New DataTable
        Dim dt5 As New DataTable
        Dim dt6 As New DataTable

        Dim _StartDate As String = Microsoft.VisualBasic.Right(FDDate.Text, 4) + "/" + Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(FDDate.Text, 7), 2)
        Dim _EndDate As String = Microsoft.VisualBasic.Right(FDDateTo.Text, 4) + "/" + Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(FDDateTo.Text, 7), 2)
        Dim _CmpCode As String
        Dim _Qry As String = ""

        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            If _dtcmp.Select("FTSelect='1'").Length > 0 Then
                _dtcmp.Dispose()

                Dim _ServerName, _UID, _PWS, _DBName As String
                Dim _ConnectString As String = ""
                Dim _FNHSysCmpId As Integer = 0

                For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

                    _CmpCode = R!FTCmpCode.ToString

                    If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then
                        Ridx = Ridx + 1

                        _ServerName = R!FTIPServer.ToString
                        _UID = HI.Conn.DB.UIDName
                        _PWS = HI.Conn.DB.PWDName
                        _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                        _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                        _Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")
                        Try

                            If CheckEdit0.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt1 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt1show = dt1.Clone
                                End If

                                dt1show.Merge(dt1)
                            End If

                            If CheckEdit1.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER_RATIO '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt2 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt2show = dt2.Clone
                                End If

                                dt2show.Merge(dt2)
                            End If

                            If CheckEdit2.Checked Then

                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt3 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt3show = dt3.Clone
                                End If

                                dt3show.Merge(dt3)

                            End If

                            If CheckEdit3.Checked Then

                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_MANPOWER_RESIGN '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt4 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt4show = dt4.Clone
                                End If

                                dt4show.Merge(dt4)
                            End If

                            If CheckEdit4.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_HR_MONEY '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt5 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt5show = dt5.Clone
                                End If

                                dt5show.Merge(dt5)
                            End If

                            If CheckEdit5.Checked Then
                                _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_EXPORT_DATA_PRODUCT_DEFECT '" & HI.UL.ULF.rpQuoted(_StartDate) & "','" & HI.UL.ULF.rpQuoted(_EndDate) & "','" & HI.UL.ULF.rpQuoted(_CmpCode) & "' "
                                dt6 = GetSewFGData(_Qry, _ConnectString)

                                If Ridx = 1 Then
                                    dt6show = dt6.Clone
                                End If

                                dt6show.Merge(dt6)

                            End If

                        Catch ex22 As Exception
                            ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                        End Try

                    End If

                Next

            End If

            _dtcmp.Dispose()

            ogc1.DataSource = dt1show
            Me.ogv1.BestFitColumns()

            ogc2.DataSource = dt2show
            Me.ogv2.BestFitColumns()

            ogc3.DataSource = dt3show
            Me.ogv3.BestFitColumns()

            ogc4.DataSource = dt4show
            Me.ogv4.BestFitColumns()

            ogc5.DataSource = dt5show
            Me.ogv5.BestFitColumns()


            ogc6.DataSource = dt6show
            Me.ogv6.BestFitColumns()


        Catch ex As Exception
        End Try

        _Spls.Close()

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FDDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FDDateTo.Text <> "" Then
            _Pass = True
        End If

        Dim _dtcmp As DataTable
        With CType(Me.ogccmp.DataSource, DataTable)
            .AcceptChanges()
            _dtcmp = .Copy
        End With

        If _dtcmp.Select("FTSelect='1'").Length > 0 Then
            _Pass = True
        End If

        If CheckEdit0.Checked = False And CheckEdit1.Checked = False And CheckEdit2.Checked = False And CheckEdit5.Checked = False And CheckEdit4.Checked = False And CheckEdit3.Checked = False Then
            _Pass = False
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

            Call LoadCompany()


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then

            Call LoadData()
            Me.otbdetail.SelectedTabPageIndex = 0

        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        Call LoadCompany()

    End Sub

    Private Function GetSewFGData(cmsstring As String, connstring As String) As DataTable
        Dim _Cnn = New System.Data.SqlClient.SqlConnection()
        Dim _Cmd = New System.Data.SqlClient.SqlCommand()
        Dim objDataSet As New DataTable
        Try

            If _Cnn.State = ConnectionState.Open Then
                _Cnn.Close()
            End If
            _Cnn.ConnectionString = connstring
            _Cnn.Open()
            _Cmd = _Cnn.CreateCommand

            Dim _Adepter As New System.Data.SqlClient.SqlDataAdapter(_Cmd)
            _Adepter.SelectCommand.CommandTimeout = 0
            _Adepter.SelectCommand.CommandType = CommandType.Text
            _Adepter.SelectCommand.CommandText = cmsstring
            _Adepter.Fill(objDataSet)
            _Adepter.Dispose()

            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        End Try
        Return objDataSet
    End Function
#End Region


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub





End Class