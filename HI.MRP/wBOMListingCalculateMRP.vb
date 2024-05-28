Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraGrid.Filter
Imports DevExpress.XtraPrinting
Imports System.Data.Common
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraBars.Docking2010

Imports System.IO
Imports Microsoft.Win32

Public Class wBOMListingCalculateMRP

    Public BOMID As Integer = 0
    Public StateBomOrder As Integer = 0
    Public StyleID As Integer = 0
    Public SeasonID As Integer = 0

    Private Shared _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip
    Private _wlistDataExportError As wExportOptiplanListOrder

    Private _DefailtPath As String

    Private pBuyCode As String = ""
    Private pStyleCode As String = ""
    Private pOrderNo As String = ""
    Private pPath As String = ""
    Public StateCal As Boolean

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()


        Call CreateManuStripGrid()

        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try


        Dim oSysLang As New ST.SysLanguage
        _wlistDataExportError = New wExportOptiplanListOrder

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wlistDataExportError.Name.ToString.Trim, _wlistDataExportError)
        Catch ex As Exception
        Finally
        End Try

        HI.TL.HandlerControl.AddHandlerObj(_wlistDataExportError)

        ' Add any initialization after the InitializeComponent() call.

        Dim Indx As Integer = 0
        Try
            Indx = Val(HI.UL.AppRegistry.ReadRegistry("BOMCalType" & Me.Name))
        Catch ex As Exception
        End Try
        Try
            FNCaltype.SelectedIndex = Indx

        Catch ex As Exception

        End Try

    End Sub
#End Region

    Private Sub CreateManuStripGrid()

        _MContextMenuStripGrid = New System.Windows.Forms.ContextMenuStrip
        Dim _ExportToExcel As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToCsv As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToText As New System.Windows.Forms.ToolStripMenuItem

        With _ExportToExcel
            .Name = "ocmExportToExcel"
            .Text = "Export To Excel"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToExcel_Click
        End With

        With _ExportToCsv
            .Name = "ocmExportToCsv"
            .Text = "Export To CSV"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToCSV.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToCSV_Click
        End With

        With _ExportToPDF
            .Name = "ocmExportToPDF"
            .Text = "Export To PDF"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToPDF.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToPDF_Click
        End With

        With _ExportToText
            .Name = "ocmExportToText"
            .Text = "Export To Text"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToText.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToText_Click
        End With

        With _MContextMenuStripGrid
            .Name = "ContextMenuGrid"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ExportToExcel, _ExportToCsv, _ExportToPDF, _ExportToText})
        End With

    End Sub


    Public Shared Function ReadRegistry() As String
        Dim regKey As RegistryKey
        Dim valreturn As String = ""

        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExportOptiplan", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathExportOptiplan", value.ToString)
        regKey.Close()

    End Sub


    Private Sub InidGridMRPReport()

        With ogvmrp

        End With

    End Sub

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub wGenerateMRP_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            ogvmatcode.FocusedRowHandle = 0

            'ogcmatcode.ViewCollection.Add(View)
            'ogcmatcode.MainView = View

            ogvmatcode.GridControl = ogcmatcode
            ogvmatcode.OptionsView.ShowAutoFilterRow = False
            ogvmatcode.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            ogvmatcode.OptionsNavigation.AutoFocusNewRow = True
            ogvmatcode.OptionsBehavior.AllowAddRows = True
            ogvmatcode.OptionsBehavior.AllowDeleteRows = True
            ogvmatcode.OptionsBehavior.Editable = True
            ogvmatcode.OptionsView.ShowFilterPanelMode = True
            ogvmatcode.BestFitColumns()


        Catch ex As Exception
        End Try
    End Sub

#Region "MAIN PROC"

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysBuyId_EditValueChanged), New Object() {sender, e})
        Else

            If (_StateLoadOrder) Then Exit Sub

            Dim _Str As String = ""
            FNHSysStyleId.Text = ""
            FNHSysStyleId.Properties.Tag = ""

            ogcmatcode.DataSource = Nothing
            GridOrderList.DataSource = Nothing
            GridCalculated.DataSource = Nothing


            FNHSysStyleId.Text = ""

            If FNHSysStyleId.Text <> "" Then

                _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

                Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")

                'Try


                'Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

                'Catch ex As Exception
                'End Try

                _Spls.Close()

            End If

            Me.otbmrp.SelectedTabPageIndex = 0

            'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
            'Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

        End If
    End Sub


    Private Sub GetBOMInfo(Optional Calculated As Boolean = False, Optional _Spl As HI.TL.SplashScreen = Nothing)



        With CType(GridOrderList.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim dtins As New DataTable
            dtins = .Select("FTSelect='1'").CopyToDataTable()
            If dtins Is Nothing Then

                dtins.Columns.Add("FTOrderNo", GetType(String))

            Else

                dtins.BeginInit()
                For Each Col As DataColumn In .Columns
                    If Col.ColumnName = "FTOrderNo" Then
                    Else
                        dtins.Columns.Remove(Col.ColumnName)
                    End If
                Next
                dtins.BeginInit()

            End If

            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)

        End With

        Dim view As ColumnView = ogvmatcode
        Dim FilterString As String = ""
        Dim FilterStringAll As String = ""
        Dim FilterStringIn As String = ""


        Dim K As String = ""
        Dim KValue As String = ""

        For Each colX As GridColumn In ogvmatcode.Columns

            K = ""
            KValue = ""

            Try
                K = colX.FilterInfo.FilterString
            Catch ex As Exception
            End Try

            Try
                KValue = colX.FilterInfo.Value.ToString
            Catch ex As Exception
            End Try

            Select Case colX.FieldName
                Case "FNHSysMerMatId_None"
            End Select
            Try

                Select Case colX.FieldName
                    Case "FNHSysMerMatId_None"
                    Case Else

                        'Dim xValue As String = view.ActiveFilter.Item(colX).Filter.Value
                        ''If xValue.Contains("*") Then
                        'FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(xValue, "*", "%%%") & "%')" & vbCrLf
                        ''Else
                        ''    FilterString += " AND " & colX.FieldName & " = '" & xValue & "'" & vbCrLf
                        ''End If

                        If K <> "" Then

                            If FilterStringIn = "" Then
                                ' FilterStringIn = " AND A." & colX.FieldName & " IN ('" & HI.UL.ULF.rpQuoted(KValue) & "'"
                                FilterStringIn = "  AND  A." & K.Replace("Or [", "Or A.[")
                            Else
                                'FilterStringIn = FilterStringIn & ",'" & HI.UL.ULF.rpQuoted(KValue) & "'"
                                FilterStringIn = FilterStringIn & "  AND  A." & K
                            End If

                        End If

                        If KValue <> "" Then
                            FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(KValue, "*", "%%%") & "%')" & vbCrLf
                        End If

                End Select

            Catch ex As Exception
            End Try

        Next

        If FilterStringIn <> "" Then
            'FilterStringIn = FilterStringIn & ""
        End If

        FilterStringAll = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(ogvmatcode.ActiveFilterCriteria)
        ' FilterStringAll = FilterString & FilterStringIn

        If FilterStringAll <> "" Then

            FilterStringAll = " AND " & FilterStringAll
        End If

        'MsgBox(FilterString)
        Dim _Str As String

        _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
        FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")


        Me.otbmrp.SelectedTabPageIndex = 0
    End Sub

    Private Function GetAllOrderArr() As String
        Dim OrderNoArr As String = ""
        Dim dt As DataTable ' = CType(GridOrderList.DataSource, DataTable)
        With CType(GridOrderList.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy()
        End With


        Try

            If Not dt Is Nothing Then
                If dt.Select("FTSelect='1'").Length > 0 Then
                    For Each r As DataRow In dt.Select("FTSelect='1'")
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    Next
                Else
                    For Each r As DataRow In dt.Rows
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    Next
                End If
            Else
                OrderNoArr = "---"
            End If




            Dim dtins As New DataTable
            dtins = dt.Select("FTSelect='1'").CopyToDataTable()
            If dtins Is Nothing Then
                dtins.Columns.Add("FTOrderNo", GetType(String))

            Else

                dtins.BeginInit()
                For Each Col As DataColumn In dt.Columns
                    If Col.ColumnName = "FTOrderNo" Then
                    Else
                        dtins.Columns.Remove(Col.ColumnName)
                    End If
                Next
                dtins.BeginInit()

            End If

            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)


        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    'Private Function GetOrderArr() As String
    '    Dim OrderNoArr As String = ""
    '    Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

    '    Try

    '        If Not dt Is Nothing Then

    '            For Each r As DataRow In dt.Select("FTSelect='1'")
    '                If OrderNoArr <> "" Then OrderNoArr += ","
    '                OrderNoArr += r!FTOrderNo.ToString
    '            Next


    '            Dim dtins As New DataTable
    '            dtins = dt.Select("FTSelect='1'").CopyToDataTable()
    '            If dtins Is Nothing Then
    '                dtins.Columns.Add("FTOrderNo", GetType(String))

    '            Else

    '                dtins.BeginInit()
    '                For Each Col As DataColumn In dt.Columns
    '                    If Col.ColumnName = "FTOrderNo" Then
    '                    Else
    '                        dtins.Columns.Remove(Col.ColumnName)
    '                    End If
    '                Next
    '                dtins.BeginInit()

    '            End If

    '            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)

    '        End If

    '    Catch ex As Exception
    '        OrderNoArr = ""
    '    End Try

    '    Return OrderNoArr

    'End Function

    Private Function GetOrderArrForReport() As String
        Dim OrderNoArr As String = ""
        CType(GridOrderList.DataSource, DataTable).AcceptChanges()
        Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

        Try
            If Not dt Is Nothing Then

                For Each r As DataRow In dt.Select("FTSelect='1'")

                    If OrderNoArr = "" Then
                        OrderNoArr = "  ( {TMERTMPR.FTOrderNo}   in ['" & HI.UL.ULF.rpQuoted(r!FTOrderNo.ToString) & "'"
                    Else
                        OrderNoArr &= ",'" & r!FTOrderNo.ToString & "'"
                    End If

                Next

                If OrderNoArr <> "" Then
                    OrderNoArr &= " ] )"
                End If

            End If

        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    Private _StateLoadOrder As Boolean = False
    Public Sub LoadOrderInfo()
        _StateLoadOrder = True
        Me.ochkselectall.Checked = False

        Dim dtStyleDetail As DataTable

        Dim cmdstring As String = ""

        cmdstring = "Select '1' AS FTSelect ,A.FTOrderNo"
        cmdstring &= vbCrLf & " ,SEAS.FTSeasonCode  "
        cmdstring &= vbCrLf & " , A.FNHSysStyleId ,A.FDOrderDate ,A.FDShipDate,A.FNHSysSeasonId,A.FNHSysBuyId,C.FTCustCode, C.FTCustNameEN AS FTCustName,BUY.FTBuyCode"


        cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_OrderProdAndSMPAll AS A With(NOLOCK) "

        If StateBomOrder = 1 Then

            cmdstring &= vbCrLf & " INNER JOIN ( SELECT DISTINCT FTOrderNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo. TMERTBOM_SpecialOrder AS SP WITH(NOLOCK)  WHERE  SP.FNHSysBomId =" & BOMID & " )  AS SP  ON A.FTOrderNo =SP.FTOrderNo "

        End If


        Select Case FNSelectOrderType.SelectedIndex
            Case 0
                cmdstring &= vbCrLf & " INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrder  AS ORD  WITH(NOLOCK)  ON A.FTOrderNo =ORD.FTOrderNo "
            Case 1
                cmdstring &= vbCrLf & " INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder  AS ORD  WITH(NOLOCK)  ON A.FTOrderNo =ORD.FTSMPOrderNo "
        End Select

        cmdstring &= vbCrLf & " LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId "
        cmdstring &= vbCrLf & " LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId   "
        cmdstring &= vbCrLf & " LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON A.FNHSysSeasonId  = SEAS.FNHSysSeasonId   "
        cmdstring &= vbCrLf & " LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMBuy  AS BUY WITH(NOLOCK)  ON A.FNHSysBuyId  = BUY.FNHSysBuyId   "

        cmdstring &= vbCrLf & " WHERE  A.FNHSysStyleId =" & StyleID & " AND A.FNHSysSeasonId =" & SeasonID & "   "

        Select Case FNSelectOrderType.SelectedIndex
            Case 3

                cmdstring &= vbCrLf & "  AND A.FNJobState = 2 "
            Case Else
                cmdstring &= vbCrLf & "  AND A.FNJobState <> 2 "

        End Select

        cmdstring &= vbCrLf & " ORDER BY A.FTOrderNo "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


        Me.GridOrderList.DataSource = dt.Copy
        dt.Dispose()
        Dim view As GridView
        view = GridOrderList.Views(0)
        view.OptionsView.ShowAutoFilterRow = True

        Me.GridOrderList = view.GridControl
        Me.GridOrderList.Refresh()
        _StateLoadOrder = False
    End Sub

    Private Sub LoadMRPReport()



        Dim _Qry As String = ""
        Dim dtMPRInfo As New DataTable

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].USP_GETBOMGARMENT_GETMPR_REPORT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogcmrp.DataSource = dtMPRInfo.Copy
        Me.ogcmrp.Refresh()

        Call InitialGridMergCell()

        ' Me.ogvmrp.OptionsView.AllowCellMerge = True
        dtMPRInfo.Dispose()

    End Sub

    Private Sub LoadBOMInfo(ByVal Calculated As Boolean, _Spl As HI.TL.SplashScreen)


        Dim dtBomInfo As New DataTable
        Dim dtitem As New DataTable
        Dim dtsetBomInfo As New DataSet

        Dim cmdstring As String = ""

        Dim _Qry As String = ""

        If Not (_Spl Is Nothing) Then

            Application.DoEvents()
            _Spl.UpdateInformation("Calculating MPR Please wait...")

        End If

        Dim Filter As String = "" ' DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(ogvmatcode.ActiveFilterCriteria)


        'If Filter <> "" Then

        '    Filter = "  AND  " & Filter
        'End If


        If Calculated Then


            cmdstring = " "

            'Filter = Filter.Replace("[", "[A.")
            'Filter = Filter.Replace("[", "").Replace("]", "")


            cmdstring &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].[dbo].TMERTMPR_History (  FTUserLogin, FTDateCreate, FNCaltype,FTStyleCode, FTSeasonCode, FTOrderNo, FTMainMatCode, FTRawMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  , FTRawMatSizeCode,  FNQuantity, FNHSysRawMatId, FNHSysStyleId, FNHSysSeasonId,FTFilterItem ,FTCalTypeName,FTCalDate,FTCalTime,FNRowSeq"
            cmdstring &= vbCrLf & "  , FTSubOrderNo,FNUsedQuantity,FNUsedPlusQuantity,FNPRQuantity,FDShipDate,FDOGacDate,FTBOmRef)"
            cmdstring &= vbCrLf & "   Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Getdate(),44, ST.FTStyleCode "
            cmdstring &= vbCrLf & "  , SS.FTSeasonCode  "
            cmdstring &= vbCrLf & "  , A.FTOrderNo "
            cmdstring &= vbCrLf & "  , A.FTMainMatCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameEN"
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameTH"
            cmdstring &= vbCrLf & " , A.FTRawMatSizeCode"
            cmdstring &= vbCrLf & " , SUM(A.FNPRQuantity) AS FNQuantity	"
            cmdstring &= vbCrLf & " , A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " , A.FNHSysStyleId "
            cmdstring &= vbCrLf & " , A.FNHSysSeasonId	"
            cmdstring &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(Filter) & "'	"
            cmdstring &= vbCrLf & " , 'Delete Auto Before Calculate'	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & "	"

            cmdstring &= vbCrLf & " ,Row_Number() Over (Partition by A.FTOrderNo,A.FTMainMatCode,A.FTRawMatColorCode,A.FTRawMatSizeCode Order by A.FTSubOrderNo,A.FTMainMatCode,A.FTRawMatColorCode ) AS FNRowSeq	"
            cmdstring &= vbCrLf & "  ,A.FTSubOrderNo,SUM(A.FNUsedQuantity) AS FNUsedQuantity,SUM(A.FNUsedPlusQuantity) AS FNUsedPlusQuantity,SUM(A.FNPRQuantity) AS FNPRQuantity "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDate,'') ) AS FDShipDate "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDateOrginal,'') ) AS FDOGacDate "
            cmdstring &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text.Trim & "-" & FNHSysSeasonId.Text.Trim & "-" & FNBomDevType.Text & " V." & FNVersion.Text) & "'"

            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A WITH(NOLOCK) "
            cmdstring &= vbCrLf & " 	    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR AS B  WITH(NOLOCK) ON A.FNHSysMerMatId =B.FNHSysMerMatId AND  A.FTOrderNo=B.FTOrderNo "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 ST.FTStyleCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMStyle AS ST WITH(NOLOCK) WHERE ST.FNHSysStyleId =A.FNHSysStyleId ) AS ST  "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 SS.FTSeasonCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMSeason  AS SS WITH(NOLOCK) WHERE SS.FNHSysSeasonId =A.FNHSysSeasonId ) AS SS "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 Sub.FDShipDate,Sub.FDShipDateOrginal  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrderSub  AS Sub WITH(NOLOCK) WHERE Sub.FTSubOrderNo =A.FTSubOrderNo ) AS Sub "
            cmdstring &= vbCrLf & " 	  WHERE A.FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'   "

            cmdstring &= vbCrLf & "   GROUP BY ST.FTStyleCode  "
            cmdstring &= vbCrLf & "	 ,SS.FTSeasonCode "
            cmdstring &= vbCrLf & " ,A.FTOrderNo ,A.FTSubOrderNo"
            cmdstring &= vbCrLf & " ,A.FTRawMatColorCode, A.FTRawMatSizeCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH, A.FTMainMatCode, A.FNHSysRawMatId, A.FNHSysStyleId, A.FNHSysSeasonId "

            cmdstring &= vbCrLf & " DELETE A "
            cmdstring &= vbCrLf & " 	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS B  ON A.FNHSysMerMatId=B.FNHSysMerMatId AND A.FTOrderNo = B.FTOrderNo "
            cmdstring &= vbCrLf & " 	WHERE	B.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            cmdstring &= vbCrLf & "   INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR]"
            cmdstring &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNSeq, FNMerMatSeq, FNHSysMerMatId, FNHSysRawMatId, FNColorWaySeq, FNHSysRawMatColorId,"
            cmdstring &= vbCrLf & "    FNRawMatColorSeq, FNHSysMatColorId, FNMatColorSeq, FTRawMatColorCode, FTMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH,"
            cmdstring &= vbCrLf & "    FNSieBreakDownSeq, FNHSysRawMatSizeId, FNRawMatSizeSeq, FNHSysMatSizeId, FNMatSizeSeq, FTRawMatSizeCode, FTMatSizeCode,"
            cmdstring &= vbCrLf & "    FTOrderNo1, FTSubOrderNo1, FNConSmp, FNConSmpPlus, FTMainMatCode, FTFabricFrontSize, FTStyleCode, FNHSysUnitId, FTUnitCode,"
            cmdstring &= vbCrLf & "   FNHSysBuyId, FTOrderNo, FTSubOrderNo, FNQuantity, FNQuantityExtra, FNQuantityTest, FNUsedQuantity, FNUsedPlusQuantity, FNPRQuantity, FNPRTotalPrice, FNHSysSuplId, FTStateNominate, FNPrice,"
            cmdstring &= vbCrLf & "   FNHSysCurId, FTSuplCode, FTCurCode, FNHSysCustId, FDOrderDate, FDShipDate, FTPositionPartId, FTPart, FTComponent, FNRawMatSparePer, FNStateChange, FTStateFree, FNHSysSeasonId, FNSeqRef"
            cmdstring &= vbCrLf & "  , FTStateCombination"
            cmdstring &= vbCrLf & "  ,FTStateMainMaterial,FTPositionPartName,FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNTotalRepeat ,FNHSysBomId,FDOGacDate,FTStateDTM,FTDTMNote,FTColorNote,FNOrderSetType,FNPart,FTOrderNoRef"
            cmdstring &= vbCrLf & " 	) "
            cmdstring &= vbCrLf & "    Select  DISTINCT"
            cmdstring &= vbCrLf & "    A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleId, A.FNSeq, A.FNMerMatSeq, A.FNHSysMerMatId, A.FNHSysRawMatId, A.FNColorWaySeq, A.FNHSysRawMatColorId,"
            cmdstring &= vbCrLf & "    A.FNRawMatColorSeq, A.FNHSysMatColorId, A.FNMatColorSeq, A.FTRawMatColorCode, A.FTMatColorCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH,"
            cmdstring &= vbCrLf & "    A.FNSieBreakDownSeq, A.FNHSysRawMatSizeId, A.FNRawMatSizeSeq, A.FNHSysMatSizeId, A.FNMatSizeSeq, A.FTRawMatSizeCode, A.FTMatSizeCode,"
            cmdstring &= vbCrLf & "    A.FTOrderNo1, A.FTSubOrderNo1, A.FNConSmp, A.FNConSmpPlus, A.FTMainMatCode, A.FTFabricFrontSize, A.FTStyleCode, A.FNHSysUnitId, A.FTUnitCode,"
            cmdstring &= vbCrLf & "    A.FNHSysBuyId, A.FTOrderNo, A.FTSubOrderNo, A.FNQuantity, A.FNQuantityExtra, A.FNGarmentQtyTest, A.FNUsedQuantity, A.FNUsedPlusQuantity, A.FNPRQuantity, A.FNPRTotalPrice, A.FNHSysSuplId, A.FTStateNominate, A.FNPrice,"
            cmdstring &= vbCrLf & "    A.FNHSysCurId, A.FTSuplCode, A.FTCurCode, A.FNHSysCustId, A.FDOrderDate, A.FDShipDate, A.FTPositionPartId, A.FTPart, A.FTComponent"
            cmdstring &= vbCrLf & "   ,A.FNRawMatSparePer, '0' AS FNStateChange,A.FTStateFree  ,A.FNHSysSeasonId,A.FNSeqRef"
            cmdstring &= vbCrLf & "   ,A.FTStateCombination"
            cmdstring &= vbCrLf & "   ,A.FTStateMainMaterial"
            cmdstring &= vbCrLf & "   ,A.FTPositionPartName,A.FTStateHemNotOptiplan,A.FNHemNotOptiplan,A.FNRepeatLengthCM ,A.FNRepeatConvert,CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING((A.[FNPRQuantity] * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END AS FNTotalRepeat   "

            cmdstring &= vbCrLf & "  ," & BOMID & " AS FNHSysBomId,A.FDOGacDate,A.FTStateDTM,A.FTDTMNote,A.FTColorNote,A.FNOrderSetType,A.FNPart,A.FTOrderNoRef"
            cmdstring &= vbCrLf & " 	 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP As A With(NOLOCK) "
            cmdstring &= vbCrLf & " 	  WHERE A.FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  And A.FNHSysRawMatId > 0 "

            cmdstring &= vbCrLf & "   Declare @TMERTOrder_MatColor AS TABLE( "
            cmdstring &= vbCrLf & "   [FNHSysStyleId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "    [FTOrderNo] [nvarchar](30) Not NULL, "
            cmdstring &= vbCrLf & "    [FNHSysMainMatId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "     [FNHSysRawMatColorId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "   [FTRawMatColorNameTH] [nvarchar](200) NULL, "
            cmdstring &= vbCrLf & "    [FTRawMatColorNameEN] [nvarchar](200) NULL,[FNRowSeq] [int] NULL, "

            cmdstring &= vbCrLf & "   UNIQUE  NONCLUSTERED(FNHSysStyleId, FTOrderNo, FNHSysMainMatId, FNHSysRawMatColorId, [FNRowSeq]) "
            cmdstring &= vbCrLf & "  	) "
            cmdstring &= vbCrLf & "    INSERT INTO  @TMERTOrder_MatColor([FNHSysStyleId] , [FTOrderNo], [FNHSysMainMatId], [FNHSysRawMatColorId], [FTRawMatColorNameTH], [FTRawMatColorNameEN],FNRowSeq )"
            cmdstring &= vbCrLf & "  	Select  FNHSysStyleId "
            cmdstring &= vbCrLf & "  	,FTOrderNo"
            cmdstring &= vbCrLf & "  	,FNHSysMerMatId"
            cmdstring &= vbCrLf & "  ,FNHSysRawMatColorId"
            cmdstring &= vbCrLf & "  	,MAX(FTRawMatColorNameTH) AS FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  ,MAX(FTRawMatColorNameEN) As FTRawMatColorNameEN"

            cmdstring &= vbCrLf & " ,ROW_NUMBER() Over (Order By [FNHSysStyleId] , [FTOrderNo]) AS [FNRowSeq] "

            cmdstring &= vbCrLf & " 	 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A  With(NOLOCK) "
            cmdstring &= vbCrLf & " 	  WHERE FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            cmdstring &= vbCrLf & "  	And FNHSysRawMatColorId <>0"

            cmdstring &= vbCrLf & "   GROUP BY FNHSysStyleId"
            cmdstring &= vbCrLf & "  ,FTOrderNo"
            cmdstring &= vbCrLf & "  	,FNHSysMerMatId"
            cmdstring &= vbCrLf & "  	,FNHSysRawMatColorId "

            cmdstring &= vbCrLf & "    DELETE From BBD "
            cmdstring &= vbCrLf & "    From     @TMERTOrder_MatColor As MMD  INNER Join"
            cmdstring &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_Mat_Color AS BBD "
            cmdstring &= vbCrLf & "    On  MMD.FTOrderNo = BBD.FTOrderNo "

            cmdstring &= vbCrLf & "    And MMD.FNHSysMainMatId = BBD.FNHSysMainMatId"


            cmdstring &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].[dbo].TMERTMPR_History (  FTUserLogin, FTDateCreate, FNCaltype,FTStyleCode, FTSeasonCode, FTOrderNo, FTMainMatCode, FTRawMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  , FTRawMatSizeCode,  FNQuantity, FNHSysRawMatId, FNHSysStyleId, FNHSysSeasonId,FTFilterItem ,FTCalTypeName,FTCalDate,FTCalTime,FNRowSeq"
            cmdstring &= vbCrLf & "  , FTSubOrderNo,FNUsedQuantity,FNUsedPlusQuantity,FNPRQuantity,FDShipDate,FDOGacDate,FTBOmRef)"
            cmdstring &= vbCrLf & "   Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Getdate(),1, ST.FTStyleCode "
            cmdstring &= vbCrLf & "  , SS.FTSeasonCode  "
            cmdstring &= vbCrLf & "  , A.FTOrderNo "
            cmdstring &= vbCrLf & "  , A.FTMainMatCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameEN"
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameTH"
            cmdstring &= vbCrLf & " , A.FTRawMatSizeCode"
            cmdstring &= vbCrLf & " , SUM(A.FNPRQuantity) AS FNQuantity	"
            cmdstring &= vbCrLf & " , A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " , A.FNHSysStyleId "
            cmdstring &= vbCrLf & " , A.FNHSysSeasonId	"
            cmdstring &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(Filter) & "'	"
            cmdstring &= vbCrLf & " , 'Calculate'	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & "	"

            cmdstring &= vbCrLf & " ,Row_Number() Over (Partition by A.FTOrderNo,A.FTMainMatCode,A.FTRawMatColorCode,A.FTRawMatSizeCode Order by A.FTSubOrderNo,A.FTMainMatCode,A.FTRawMatColorCode ) AS FNRowSeq	"
            cmdstring &= vbCrLf & "  ,A.FTSubOrderNo,SUM(A.FNUsedQuantity) AS FNUsedQuantity,SUM(A.FNUsedPlusQuantity) AS FNUsedPlusQuantity,SUM(A.FNPRQuantity) AS FNPRQuantity "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDate,'') ) AS FDShipDate "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDateOrginal,'') ) AS FDOGacDate "
            cmdstring &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text.Trim & "-" & FNHSysSeasonId.Text.Trim & "-" & FNBomDevType.Text & " V." & FNVersion.Text) & "'"

            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A WITH(NOLOCK) "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 ST.FTStyleCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMStyle AS ST WITH(NOLOCK) WHERE ST.FNHSysStyleId =A.FNHSysStyleId ) AS ST  "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 SS.FTSeasonCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMSeason  AS SS WITH(NOLOCK) WHERE SS.FNHSysSeasonId =A.FNHSysSeasonId ) AS SS "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 Sub.FDShipDate,Sub.FDShipDateOrginal  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrderSub  AS Sub WITH(NOLOCK) WHERE Sub.FTSubOrderNo =A.FTSubOrderNo ) AS Sub "
            cmdstring &= vbCrLf & " 	  WHERE A.FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  And A.FNHSysRawMatId > 0 "



            cmdstring &= vbCrLf & "   GROUP BY ST.FTStyleCode  "
            cmdstring &= vbCrLf & "	 ,SS.FTSeasonCode "
            cmdstring &= vbCrLf & " ,A.FTOrderNo ,A.FTSubOrderNo"
            cmdstring &= vbCrLf & " ,A.FTRawMatColorCode, A.FTRawMatSizeCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH, A.FTMainMatCode, A.FNHSysRawMatId, A.FNHSysStyleId, A.FNHSysSeasonId "

            cmdstring &= vbCrLf & "    INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_Mat_Color( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTOrderNo"
            cmdstring &= vbCrLf & "  , FNHSysMainMatId, FNHSysRawMatColorId, FTRawMatColorNameTH, FTRawMatColorNameEN)	"
            cmdstring &= vbCrLf & "  	Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            cmdstring &= vbCrLf & "     ,Convert(varchar(10),GetDate(),111)"
            cmdstring &= vbCrLf & "     ,Convert(varchar(8),GetDate(),114)"
            cmdstring &= vbCrLf & "    , MM.FNHSysStyleId"
            cmdstring &= vbCrLf & "    , MM.FTOrderNo"
            cmdstring &= vbCrLf & "    , MM.FNHSysMainMatId"
            cmdstring &= vbCrLf & "    , MM.FNHSysRawMatColorId"
            cmdstring &= vbCrLf & "   , MM.FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "    , MM.FTRawMatColorNameEN "
            cmdstring &= vbCrLf & "   FROM     @TMERTOrder_MatColor As MM  "

            cmdstring &= vbCrLf & " Declare @TabOrder AS Table (OrderNo nvarchar(30)  UNIQUE  NONCLUSTERED  (OrderNo) )"

            cmdstring &= vbCrLf & "    INSERT INTO @TabOrder(OrderNo) "
            cmdstring &= vbCrLf & "   Select Distinct FTOrderNo "
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR_TMP] AS A WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            cmdstring &= vbCrLf & " Declare @TabRC AS TABLE ("
            cmdstring &= vbCrLf & " [FTUserLogIn] [varchar](50) NOT NULL,"
            cmdstring &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL,"
            cmdstring &= vbCrLf & " [FTSubOrderNo] [nvarchar](30) NOT NULL,"
            cmdstring &= vbCrLf & " [FNHSysRawMatId] [int] NOT NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantity] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedPlusQuantity] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantityDIFF] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantityPlusDIFF] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNHSysUnitId] [int] NULL,"
            cmdstring &= vbCrLf & " [FNPrice] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNHSysCurId] [int] NULL,"
            cmdstring &= vbCrLf & " [FNHSysSuplId] [int] NULL,"
            cmdstring &= vbCrLf & " [FTStateNominate] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNStateChange] [int]  NULL,"
            cmdstring &= vbCrLf & " [FTFabricFrontSize] [nvarchar](50) NULL,"
            cmdstring &= vbCrLf & " [FTStateFree] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNStateNew] [int] NULL,"
            cmdstring &= vbCrLf & " [FNRowSeq] [int] NULL,"
            cmdstring &= vbCrLf & " [FTStateHemNotOptiplan] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNHemNotOptiplan] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNRepeatLengthCM] numeric(18,5) NULL,"
            cmdstring &= vbCrLf & " [FNRepeatConvert] numeric(18,10) NULL,"
            cmdstring &= vbCrLf & " [FNTotalRepeat] numeric(18,5) NULL,"
            cmdstring &= vbCrLf & " [FTMainMatCoide] [nvarchar](50) NULL,"
            cmdstring &= vbCrLf & " [FNHSysBomId] [int] NULL,"
            cmdstring &= vbCrLf & " [FTOrderNoRef] [nvarchar](50) NULL,"


            cmdstring &= vbCrLf & "  UNIQUE  NONCLUSTERED  ([FTUserLogIn],[FTOrderNo],[FTSubOrderNo],[FNHSysRawMatId],[FNStateChange],[FNRowSeq],[FTMainMatCoide])"
            cmdstring &= vbCrLf & "  ) "

            cmdstring &= vbCrLf & "    INSERT INTO @TabRC(FTUserLogIn, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity "
            cmdstring &= vbCrLf & "  , FNUsedQuantityDIFF, FNUsedQuantityPlusDIFF, FNHSysUnitId, FNPrice, FNHSysCurId"
            cmdstring &= vbCrLf & "  , FNHSysSuplId, FTStateNominate, FNStateChange,FTFabricFrontSize,FTStateFree,FNStateNew,[FNRowSeq],FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNTotalRepeat,[FTMainMatCoide],FNHSysBomId,FTOrderNoRef)"

            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTOrderNo, FTSubOrderNo, FNHSysRawMatId"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantity) AS FNUsedQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedPlusQuantity) AS FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantityDIFF) AS FNUsedQuantityDIFF"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantityPlusDIFF) AS FNUsedQuantityPlusDIFF"
            cmdstring &= vbCrLf & " 	, FNHSysUnitId, FNPrice, FNHSysCurId, FNHSysSuplId, FTStateNominate"
            cmdstring &= vbCrLf & " 	, FNStateChange"
            cmdstring &= vbCrLf & " 	, FTFabricFrontSize"
            cmdstring &= vbCrLf & " ,Max(FTStateFree) AS FTStateFree"
            cmdstring &= vbCrLf & " 	,FNStateNew"
            cmdstring &= vbCrLf & " 	,ROW_NUMBER() Over (Order By FTOrderNo) AS [FNRowSeq]"
            cmdstring &= vbCrLf & " ,MAX(FTStateHemNotOptiplan) AS FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & " 	,MAX(FNHemNotOptiplan) As FNHemNotOptiplan"
            cmdstring &= vbCrLf & " 	,MAX(FNRepeatLengthCM) AS FNRepeatLengthCM"
            cmdstring &= vbCrLf & " 	,MAX([FNRepeatConvert]) AS FNRepeatConvert"
            cmdstring &= vbCrLf & " 	,0 AS FNTotalRepeat,MAX(FTMainMatCode) As [FTMainMatCoide],Max(FNHSysBomId) AS FNHSysBomId,MAX(FTOrderNoRef) As FTOrderNoRef"
            cmdstring &= vbCrLf & " 	FROM("
            cmdstring &= vbCrLf & "  SELECT A.FTOrderNo, '' AS FTSubOrderNo, A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "    , CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END AS FNUsedQuantity"
            cmdstring &= vbCrLf & " 	,CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE  SUM(FNUsedPlusQuantity) END AS FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysUnitId) AS FNHSysUnitId"
            cmdstring &= vbCrLf & " 	, MAX(FNPrice) AS FNPrice"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysCurId) AS FNHSysCurId"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysSuplId) AS FNHSysSuplId"
            cmdstring &= vbCrLf & " 	, MAX(FTStateNominate) AS FTStateNominate"
            cmdstring &= vbCrLf & " 	, (SUM(FNUsedQuantity)) - (MAX(ISNULL(RES.FNUsedQuantity2, 0))) AS FNUsedQuantityDIFF"
            cmdstring &= vbCrLf & " 	, ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) - (MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) AS FNUsedQuantityPlusDIFF"
            cmdstring &= vbCrLf & " 	, ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END  + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN CEILING(SUM(FNUsedPlusQuantity)) ELSE SUM(FNUsedPlusQuantity) END) - (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) AS DIFFQTY"
            cmdstring &= vbCrLf & " 	, CASE WHEN ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END  + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) > "
            cmdstring &= vbCrLf & " 	(MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0)))"
            cmdstring &= vbCrLf & " 	THEN CASE WHEN (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) = 0 "
            cmdstring &= vbCrLf & " 	THEN 0 ELSE MAX(RES.FNStateChange) + 1 END"
            cmdstring &= vbCrLf & " 	Else MAX(ISNULL(Res.FNStateChange, 0))"
            cmdstring &= vbCrLf & "    End As FNStateChange	"
            cmdstring &= vbCrLf & "   ,MAX(FTFabricFrontSize) AS FTFabricFrontSize	"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL(FTStateFree,'0')) AS FTStateFree"

            cmdstring &= vbCrLf & "   , CASE WHEN (SUM(FNUsedQuantity) + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) > "
            cmdstring &= vbCrLf & " 		(MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) And (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) > 0"
            cmdstring &= vbCrLf & " 	THEN 1"
            cmdstring &= vbCrLf & " 	Else 0"
            cmdstring &= vbCrLf & "   End As FNStateNew	"
            cmdstring &= vbCrLf & "   ,MAX(FTStateHemNotOptiplan) AS FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & "   ,SUM(ISNULL(A.FNHemNotOptiplan,0)) AS FNHemNotOptiplan"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL(FNRepeatLengthCM,0)) AS FNRepeatLengthCM"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL([FNRepeatConvert],0)) AS FNRepeatConvert"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL([FTMainMatCode],'')) AS FTMainMatCode,MAX(FNHSysBomId) As FNHSysBomId,MAX(FTOrderNoRef) AS FTOrderNoRef"
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR_TMP] AS A WITH(NOLOCK)"

            cmdstring &= vbCrLf & " 	OUTER APPLY(SELECT FTOrderNo, FNHSysRawMatId, MAX(FNStateChange) AS FNStateChange"
            cmdstring &= vbCrLf & " 	, SUM(FNUsedQuantity) AS FNUsedQuantity2, SUM(FNUsedPlusQuantity) AS FNUsedPlusQuantity2"
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing]  AS X  WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	Where X.FTOrderNo =A.FTOrderNo "
            cmdstring &= vbCrLf & " 	AND X.FNHSysRawMatId =A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " 	GROUP BY FTOrderNo, FNHSysRawMatId) AS RES"

            cmdstring &= vbCrLf & " 	WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & "    And A.FNHSysRawMatId > 0    "


            cmdstring &= vbCrLf & " 	GROUP BY A.FTOrderNo,  A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "  ) AS A "
            cmdstring &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice"
            cmdstring &= vbCrLf & " 	, FNHSysCurId, FNHSysSuplId, FTStateNominate, DIFFQTY, FNStateChange,FTFabricFrontSize,FNStateNew "
            cmdstring &= vbCrLf & "   delete from @TabRC where FNStateNew <> 0 And [FNUsedQuantityDIFF] =0 And FNUsedQuantityPlusDIFF =0 AND ISNULL(FNStateChange,0) >0 "
            cmdstring &= vbCrLf & " update @TabRC set FNStateChange = isnull(FNStateChange,0)"


            cmdstring &= vbCrLf & "    DELETE A "
            cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS A INNER Join @TabOrder As B On A.FTOrderNo = B.OrderNo "
            cmdstring &= vbCrLf & "    CROSS APPLY (SELECT TOP 1 MMM.FTRawMatCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TINVENMMaterial] AS MMM WITH(NOLOCK) WHERE MMM.FNHSysRawMatId =A.FNHSysRawMatId ) AS MMM "
            cmdstring &= vbCrLf & " 	Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] AS C WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	On A.FTOrderNo = C.FTOrderNo"
            cmdstring &= vbCrLf & " 	And A.FNHSysRawMatId=C.FNHSysRawMatId"
            cmdstring &= vbCrLf & " And A.FNStateChange = C.FNStateChange"
            cmdstring &= vbCrLf & "           INNER Join @TabRC AS RC ON A.FTOrderNo = RC.FTOrderNo  And MMM.FTRawMatCode=RC.FTMainMatCoide"
            cmdstring &= vbCrLf & "  WHERE  RC.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' And C.FTOrderNo Is NULL"

            cmdstring &= vbCrLf & "     Update A"
            cmdstring &= vbCrLf & "  SET [FNUsedQuantity] = CASE WHEN TMP.FNStateNew = 0 THEN TMP.[FNUsedQuantity] ELSE TMP.[FNUsedQuantityDIFF] END"
            cmdstring &= vbCrLf & "    ,[FNUsedPlusQuantity] =CASE WHEN TMP.FNStateNew = 0 THEN TMP.FNUsedPlusQuantity ELSE TMP.FNUsedQuantityPlusDIFF END "
            cmdstring &= vbCrLf & "    ,[FNPrice] = TMP.[FNPrice] "
            cmdstring &= vbCrLf & "    ,FTStateHemNotOptiplan =TMP.FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & "    ,FNHemNotOptiplan=TMP.FNHemNotOptiplan"
            cmdstring &= vbCrLf & "    ,FNRepeatLengthCM = TMP.FNRepeatLengthCM"
            cmdstring &= vbCrLf & "    ,FNRepeatConvert  = TMP.FNRepeatConvert"
            cmdstring &= vbCrLf & "    ,FNHSysSuplId  = TMP.FNHSysSuplId"
            cmdstring &= vbCrLf & "    ,FNHSysCurId  = TMP.FNHSysCurId"
            cmdstring &= vbCrLf & "    ,FTFabricFrontSize  = TMP.FTFabricFrontSize"
            cmdstring &= vbCrLf & "    ,FNHSysBomId  = TMP.FNHSysBomId"
            cmdstring &= vbCrLf & "    ,FTOrderNoRef  = TMP.FTOrderNoRef"


            cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] As A INNER Join  @TabRC As TMP On"
            cmdstring &= vbCrLf & "    TMP.FNHSysRawMatId = A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "   And TMP.FTOrderNo =A.FTOrderNo"
            cmdstring &= vbCrLf & "   And TMP.FTSubOrderNo =A.FTSubOrderNo"
            cmdstring &= vbCrLf & "   And TMP.FNStateChange =A.FNStateChange"
            cmdstring &= vbCrLf & "   INNER Join @TabOrder AS XA ON A.FTOrderNo = XA.OrderNo"
            cmdstring &= vbCrLf & "    WHERE  TMP.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            '/* INSERT New RESOURCE */
            cmdstring &= vbCrLf & "  INSERT INTO [TMERTOrder_Resource] ([FTInsUser], [FDInsDate], [FTInsTime], [FTUpdUser], [FDUpdDate], [FTUpdTime] "
            cmdstring &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FNHSysRawMatId"
            cmdstring &= vbCrLf & " 	, FNUsedQuantity"
            cmdstring &= vbCrLf & " 	, FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, FNHSysUnitId, FNPrice, FNHSysCurId, FNHSysSuplId, FTStateNominate"
            cmdstring &= vbCrLf & " , FNStateChange,FTFabricFrontSize,FTStateFree,FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNHSysBomId,FTOrderNoRef)"
            cmdstring &= vbCrLf & " 	Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AS [FTInsUser], CONVERT(VARCHAR(10), GETDATE(), 111) AS [FDInsDate], CONVERT(VARCHAR(8), GETDATE(), 108) AS [FTInsTime]"
            cmdstring &= vbCrLf & " 	, '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS [FTUpdUser], CONVERT(VARCHAR(10), GETDATE(), 111) AS [FDUpdDate], CONVERT(VARCHAR(8), GETDATE(), 108) AS [FTUpdTime]"
            cmdstring &= vbCrLf & ", TMP.FTOrderNo, TMP.FTSubOrderNo, TMP.FNHSysRawMatId"
            cmdstring &= vbCrLf & "	, CASE WHEN TMP.FNStateNew = 0 THEN TMP.[FNUsedQuantity] ELSE TMP.[FNUsedQuantityDIFF] END"
            cmdstring &= vbCrLf & "	, CASE WHEN TMP.FNStateNew = 0 THEN TMP.FNUsedPlusQuantity ELSE TMP.FNUsedQuantityPlusDIFF END "
            cmdstring &= vbCrLf & "	, TMP.FNHSysUnitId, TMP.FNPrice, TMP.FNHSysCurId, TMP.FNHSysSuplId, TMP.FTStateNominate"
            cmdstring &= vbCrLf & "	, TMP.FNStateChange"
            cmdstring &= vbCrLf & "	, TMP.FTFabricFrontSize"
            cmdstring &= vbCrLf & "	, TMP.FTStateFree"
            cmdstring &= vbCrLf & "	,TMP.FTStateHemNotOptiplan,TMP.FNHemNotOptiplan,TMP.FNRepeatLengthCM,TMP.FNRepeatConvert,TMP.FNHSysBomId,TMP.FTOrderNoRef"
            cmdstring &= vbCrLf & "FROM @TabRC AS TMP LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS B WITH (NOLOCK)"
            cmdstring &= vbCrLf & " On TMP.FNHSysRawMatId = B.FNHSysRawMatId"
            cmdstring &= vbCrLf & " And TMP.FTOrderNo =B.FTOrderNo"
            cmdstring &= vbCrLf & " And TMP.FTSubOrderNo =B.FTSubOrderNo"
            cmdstring &= vbCrLf & " And TMP.FNStateChange =B.FNStateChange"
            cmdstring &= vbCrLf & "	WHERE  TMP.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  And B.FTOrderNo Is NULL"

            cmdstring &= vbCrLf & "	 Update A Set FNOptiplanQuantity = CEILING(ISNULL(Case When a.FNHSysUnitId = MZ.FNHSysUnitId Then MZ.FNTotal  Else Convert(numeric(18, 4),(MZ.FNTotal  * ISNULL(UCV.FNRateTo,1) ) / ISNULL(UCV.FNRateFrom,1))  END ,0) + CASE WHEN ISNULL(MZ.FNTotal,0) > 0 THEN ISNULL(A.FNHemNotOptiplan,0) ELSE 0.0 END)"
            cmdstring &= vbCrLf & " , FNOptiplanQuantityOrg = CEILING(ISNULL(CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * ISNULL(UCV.FNRateTo,1) ) / ISNULL(UCV.FNRateFrom,1))  END ,0) )"
            cmdstring &= vbCrLf & "  ,FTStateFree = CC.FTStateFree"
            cmdstring &= vbCrLf & "   ,FNTotalRepeat = CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING(((A.FNUsedQuantity + A.FNUsedPlusQuantity) * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END"
            cmdstring &= vbCrLf & "   ,FNTotalRepeatOptiplan = CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING((( ISNULL(CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * ISNULL(UCV.FNRateTo,1) ) / ISNULL(UCV.FNRateFrom,1))  END ,0) ) * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END"
            cmdstring &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A INNER Join"
            cmdstring &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B  WITH (NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER Join"
            cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK)  ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId"

            cmdstring &= vbCrLf & "  INNER Join @TabRC  AS CC ON A.FTOrderNo=CC.FTOrderNo And A.FNHSysRawMatId = CC.FNHSysRawMatId"

            cmdstring &= vbCrLf & "   OUTER APPLY("

            cmdstring &= vbCrLf & "   Select  FTOrderNo, FTRawMatCode, FTRawMatColorCode, SUM(FNTotal) As FNTotal, MAX(FNHSysUnitId) As FNHSysUnitId"
            cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan AS O WITH (NOLOCK)"
            cmdstring &= vbCrLf & "   Where O.FTOrderNo = A.FTOrderNo And O.FTRawMatCode = ISNULL(B.FTRawMatCode,'')  AND O.FTRawMatColorCode  = ISNULL(C.FTRawMatColorCode,'')"
            cmdstring &= vbCrLf & "   GROUP BY FTOrderNo, FTRawMatCode, FTRawMatColorCode"

            cmdstring &= vbCrLf & "  ) AS MZ"

            cmdstring &= vbCrLf & "  OUTER APPLY("
            cmdstring &= vbCrLf & "	SELECT TOP 1 UCV.FNRateTo , UCV.FNRateFrom"
            cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UCV "
            cmdstring &= vbCrLf & "   Where UCV.FNHSysUnitId = MZ.FNHSysUnitId And UCV.FNHSysUnitIdTo = A.FNHSysUnitId"
            cmdstring &= vbCrLf & "  ) As UCV "

            cmdstring &= vbCrLf & "  WHERE CC.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            cmdstring &= vbCrLf & "  UPDATE BM  SET FTStateCalMRP ='1'  "
            cmdstring &= vbCrLf & " , FTStateFirstCalMRPUser= CASE WHEN ISNULL(FTStateFirstCalMRPUser,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE  FTStateFirstCalMRPUser END  "
            cmdstring &= vbCrLf & " , FDStateFirstCalMRPDate= CASE WHEN ISNULL(FTStateFirstCalMRPUser,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE  FDStateFirstCalMRPDate END  "
            cmdstring &= vbCrLf & " , FTStateFirstCalMRPTime= CASE WHEN ISNULL(FTStateFirstCalMRPUser,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE  FTStateFirstCalMRPTime END  "
            cmdstring &= vbCrLf & " , FTStateLastCalMRPUser=CASE WHEN ISNULL(FTStateFirstCalMRPUser,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE  '' END  "
            cmdstring &= vbCrLf & " , FDStateLastCalMRPDate=CASE WHEN ISNULL(FTStateFirstCalMRPUser,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE  '' END  "
            cmdstring &= vbCrLf & " , FTStateLastCalMRPTime=CASE WHEN ISNULL(FTStateFirstCalMRPUser,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE  '' END  "
            cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTBOM_Mat AS BM "
            cmdstring &= vbCrLf & "   WHERE     BM.FNHSysBomId = " & BOMID & " And BM.FTStateActive ='1' AND BM.FTStateMatConfirm ='1' AND BM.FTStateCalMRP <>'1'  AND BM.FTRunColor ='1' "

            cmdstring &= vbCrLf & "  EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[USP_GETBOMGARMENT_UPDATE_ORDERMATINFO] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMID & ""


            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        Else

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].USP_GETBOMGARMENT_CALMRP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMID & ",''"

            Application.DoEvents()
            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, dtsetBomInfo)

            dtBomInfo = dtsetBomInfo.Tables(0).Copy

            If Calculated = False Then
                dtitem = dtsetBomInfo.Tables(1).Copy
            End If

            Me.ogcmatcode.DataSource = dtBomInfo

        End If

        Try

            If Calculated Then

                'If Not (_Spl Is Nothing) Then
                '    Application.DoEvents()
                '    _Spl.UpdateInformation("Checking And Create New Material Data Please wait...")
                'End If

                'Application.DoEvents()
                'If CreateNewRawMat(_allOrder, _FilterString) = False Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างรายการ วัตถุดิบใหม่ได้", 1091990011, Me.Text, , MessageBoxIcon.Warning)
                'End If

                'If CreateNewRawMatColor() = False Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างรายการ สีวัตถุดิบใหม่ได้", 1091990012, Me.Text, , MessageBoxIcon.Warning)
                'End If

                'If CreateNewRawMatSize() = False Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างรายการ ขนาดวัตถุดิบใหม่ได้", 1091990013, Me.Text, , MessageBoxIcon.Warning)
                'End If



                ' If _FilterString = "" Then

                If Not (_Spl Is Nothing) Then
                    Application.DoEvents()
                    _Spl.UpdateInformation("Creating Data For Sourcing Please wait...")
                    Application.DoEvents()
                End If

                Application.DoEvents()

                '''''''''  Generate_Resource(0, _allOrder, Filter)
                '''
                'End If

                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Calculate MPR (Style " & FNHSysStyleId.Text & " ) ")

            Else

                If Not dtitem Is Nothing Then

                    If dtitem.Rows.Count > 0 Then
                        For Each R As DataRow In dtitem.Rows

                            Dim DataRawMatId As Long = HI.SE.RunID.GetRunNoID("TINVENMMaterial", "FNHSysRawMatId", Conn.DB.DataBaseName.DB_MASTER)

                            If DataRawMatId > 0 Then

                                _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial "
                                _Qry &= vbCrLf & "	(FTInsUser, FDInsDate, FTInsTime, FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FNHSysRawMatColorId, FTRawMatColorNameTH, "
                                _Qry &= vbCrLf & "FTRawMatColorNameEN, FNHSysRawMatSizeId, FNHSysUnitId, FTFabricFrontSize, FTRemark, FPImageRawMat, FTStateActive)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "  ," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & "  ," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & "  ," & DataRawMatId & ""
                                _Qry &= vbCrLf & "   ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                                _Qry &= vbCrLf & "   ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatNameTH.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatNameEN.ToString) & "' "
                                _Qry &= vbCrLf & "  ," & Val(R!FNHSysRawMatColorId.ToString) & " "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatColorNameTH.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatColorNameEN.ToString) & "' "
                                _Qry &= vbCrLf & "   ," & Val(R!FNHSysRawMatSizeId.ToString) & " "
                                _Qry &= vbCrLf & "    ,isnull((SELECT  TOP 1  ISNULL(IB.FNHSysUnitId,0) as FNHSysUnitId "
                                _Qry &= vbCrLf & " 	 FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IB  With(NOLOCK)  INNER JOIN "
                                _Qry &= vbCrLf & " 	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RA  WITH(NOLOCK)  ON IB.FNHSysRawMatId = RA.FNHSysRawMatId "
                                _Qry &= vbCrLf & "     WHERE ISNULL(IB.FTRawMatCode,'') ='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                                _Qry &= vbCrLf & "  ),ISNULL(( "
                                _Qry &= vbCrLf & "   Select  TOP 1  ISNULL(IB.FNHSysUnitId,0) As FNHSysUnitId "
                                _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IB  WITH(NOLOCK)  INNER JOIN "
                                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail As RA  With(NOLOCK)  On IB.FNHSysRawMatId = RA.FNHSysRawMatId "
                                _Qry &= vbCrLf & "    WHERE ISNULL(IB.FTRawMatCode,'') ='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                                _Qry &= vbCrLf & " 	 )," & Val(R!FNHSysUnitId.ToString) & ")) As FNHSysUnitId "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'' AS FPImageRawMat"
                                _Qry &= vbCrLf & "  ,'1'"

                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                            End If

                        Next

                        Call LoadBOMInfo(Calculated, _Spl)

                    End If

                End If


            End If

        Catch ex As Exception
        End Try

        Dim view As GridView
        view = ogcmatcode.Views(0)
        view.OptionsView.ShowAutoFilterRow = True
        view.BestFitColumns()

        ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
        GridViewCalculated.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200

        Me.ogcmatcode = view.GridControl
        Me.ogcmatcode.Refresh()

        ' HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    End Sub


    Private Sub ClearTab()
        Me.otb.TabPages.Clear()
    End Sub

    Private Function GenerateOrderList(_pBuyId As Integer, _pSeasonId As Integer) As String
        Dim _OrderList As String = ""
        With CType(Me.GridOrderList.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FTSelect='1' AND FNHSysBuyId =" & _pBuyId & " AND FNHSysSeasonId=" & _pSeasonId & "")

                If _OrderList = "" Then
                    _OrderList = R!FTOrderNo.ToString
                Else
                    _OrderList &= "|" & R!FTOrderNo.ToString
                End If
            Next

        End With

        Return _OrderList
    End Function

    Private Function GenerateOrderList(_FNHSysSeasonId As Integer) As String
        Dim _OrderList As String = ""
        With CType(Me.GridOrderList.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FTSelect='1' AND  FNHSysSeasonId=" & _FNHSysSeasonId & "")

                If _OrderList = "" Then
                    _OrderList = R!FTOrderNo.ToString
                Else
                    _OrderList &= "," & R!FTOrderNo.ToString
                End If
            Next

        End With

        Return _OrderList
    End Function


    Private Sub LoadOptiplanData(_Spls As HI.TL.SplashScreen)
        Call ClearTab()
        _Spls.UpdateInformation("Generating Data.. Please Wait ")

        Try

            Dim _Rec As Integer = 0
            Dim _TotalRec As Integer = 1

            Dim pSysStyleId As Integer
            Dim FTStyleCode As String
            Dim _Qry As String = ""
            Dim dt As New DataTable
            Dim dttmp As New DataTable
            Dim dtcheck As New DataTable
            Dim dts As New DataSet
            Dim _OrderList As String = ""
            Dim _dtorder As DataTable
            Dim _FNHSysSeasonId As Integer = 0
            Dim _FNSTQuantity As Integer = 0
            Dim Supl As String = ""
            'Dim pStyleCode As String = ""
            'Dim pOrderNo As String = ""
            'Dim pBuyCode As String = ""

            Dim pCutCM As Decimal = 0.0
            Dim pBuyData As Integer = 0

            With CType(GridOrderList.DataSource, DataTable)
                .AcceptChanges()
                _dtorder = .Copy
            End With

            dtcheck = Nothing

            pStyleCode = ""
            pOrderNo = ""
            pBuyCode = ""

            pSysStyleId = StyleID
            FTStyleCode = FNHSysStyleId.Text.Trim()

            If pStyleCode = "" Then
                pStyleCode = FTStyleCode
            Else
                pStyleCode = pStyleCode & "," & FTStyleCode
            End If

            If _dtorder.Select("FNHSysStyleId=" & pSysStyleId & " AND FTSelect='1'").Length > 0 Then


                Dim grpbycode As List(Of String) = (_dtorder.Select("FNHSysStyleId=" & pSysStyleId & " AND FTSelect='1'", "FTBuyCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTBuyCode")) _
                                                   .Distinct() _
                                                   .ToList()

                For Each Indgrpbycode As String In grpbycode

                    pBuyData = pBuyData + 1

                    If pBuyCode = "" Then
                        pBuyCode = Indgrpbycode
                    Else
                        pBuyCode = pBuyCode & "," & Indgrpbycode
                    End If



                    Dim grp As List(Of String) = (_dtorder.Select("FNHSysStyleId=" & pSysStyleId & " AND FTSelect='1' AND FTBuyCode='" & HI.UL.ULF.rpQuoted(pBuyCode) & "'", "FTSeasonCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTSeasonCode")) _
                                                   .Distinct() _
                                                   .ToList()

                    _TotalRec = grp.Count

                    For Each IndSeasonCode As String In grp


                        _FNHSysSeasonId = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS X WITH(NOLOCK) WHERE FTSeasonCode='" & HI.UL.ULF.rpQuoted(IndSeasonCode) & "'", Conn.DB.DataBaseName.DB_MASTER, "0")))


                        _Rec = _Rec + 1
                        _Spls.UpdateInformation("Generating Style " & FTStyleCode & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")

                        _OrderList = GenerateOrderList(_FNHSysSeasonId)

                        If pOrderNo = "" Then
                            pOrderNo = _OrderList + "(" & IndSeasonCode & ")"
                        Else
                            pOrderNo = pOrderNo & "," & _OrderList + "(" & IndSeasonCode & ")"
                        End If


                        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GETBOMGARMENT_EXPORTOPTOPLAN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMID & "," & pCutCM & ",'" & HI.UL.ULF.rpQuoted(pStyleCode) & "','" & HI.UL.ULF.rpQuoted(pBuyCode) & "'"

                        dts = New DataSet
                        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, dts)

                        If dts.Tables.Count >= 2 Then

                            dt = dts.Tables(0).Copy

                            If dts.Tables(1).Rows.Count > 0 Then
                                dttmp = dts.Tables(1).Copy

                                dttmp.Columns.Add("FTStyleCode", GetType(String))

                                For Each Rtdata As DataRow In dttmp.Rows
                                    Rtdata!FTStyleCode = FTStyleCode
                                Next

                                If dtcheck Is Nothing Then
                                    dtcheck = dttmp.Copy
                                Else
                                    dtcheck.Merge(dttmp.Copy)
                                End If

                            End If

                            Try
                                _FNSTQuantity = Val(dts.Tables(2).Rows(0).Item(0).ToString())
                            Catch ex As Exception
                                _FNSTQuantity = 0
                            End Try

                            Try
                                Supl = dts.Tables(3).Rows(0).Item(0).ToString()

                                If Supl <> "" Then
                                    Supl = "-" & Supl
                                End If
                            Catch ex As Exception
                                Supl = ""
                            End Try

                            If dt.Rows.Count > 0 Then

                                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                                Dim ogcn As New DevExpress.XtraGrid.GridControl
                                Dim ogvn As New DevExpress.XtraGrid.Views.Grid.GridView
                                Dim Colg As New DevExpress.XtraGrid.Columns.GridColumn()

                                With Colg
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = "Data"
                                    .FieldName = "FTData"
                                    .Name = "TGCOL" & FNHSysStyleId.ToString & "Buy" & pBuyData.ToString
                                    .OptionsColumn.AllowEdit = False
                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
                                    .OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
                                    .OptionsColumn.AllowMove = False
                                    .OptionsColumn.AllowShowHide = False
                                    .OptionsColumn.ReadOnly = True
                                    .Visible = True
                                    .VisibleIndex = 0
                                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .BestFit()
                                    .Width = 500
                                End With

                                With Otp
                                    .Name = "T" & FNHSysStyleId.ToString & _FNHSysSeasonId.ToString & "Buy" & pBuyData.ToString
                                    .Text = FTStyleCode & Supl & " ( " & IndSeasonCode & ") ( QTY  " & Format(_FNSTQuantity, "#,0") & " ) " & Indgrpbycode
                                End With

                                With ogvn
                                    .Name = "TGV" & FNHSysStyleId.ToString & _FNHSysSeasonId.ToString & "Buy" & pBuyData.ToString
                                    .Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Colg})
                                    .GridControl = ogcn
                                    .OptionsCustomization.AllowGroup = False
                                    .OptionsCustomization.AllowQuickHideColumns = False
                                    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
                                    .OptionsView.ColumnAutoWidth = False
                                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                                    .OptionsView.ShowGroupPanel = False
                                    .OptionsView.ShowAutoFilterRow = False
                                    .OptionsPrint.AutoWidth = False
                                    .OptionsPrint.PrintHeader = False
                                    .BestFitColumns()
                                End With

                                With ogcn
                                    .Name = "TG" & FNHSysStyleId.ToString & _FNHSysSeasonId.ToString & "Buy" & pBuyData.ToString
                                    ' .Dock = System.Windows.Forms.DockStyle.Fill
                                    .MainView = ogvn
                                    .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {ogvn})
                                    .ContextMenuStrip = _MContextMenuStripGrid
                                End With
                                Otp.Controls.Add(ogcn)

                                otb.TabPages.Add(Otp)
                                ogcn.Dock = DockStyle.Fill

                                ogcn.DataSource = dt.Copy

                            End If
                        End If

                    Next


                Next


            End If


            dt.Dispose()
            _Spls.Close()

            If Not (dtcheck Is Nothing) Then

                Try
                    If dtcheck.Rows.Count > 0 Then

                        HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการ ที่ Breakdown มาไม่ครบ กรุณาทำการตรวจสอบ ข้อมูล Boom Sheet !!!", 1602035471, Me.Text, , MessageBoxIcon.Warning)

                        With _wlistDataExportError
                            .ogvlist.ActiveFilter.Clear()
                            .ogclist.DataSource = dtcheck.Copy
                            .ogclist.Refresh()
                            .ShowDialog()
                        End With

                    End If

                Catch ex As Exception
                End Try

            End If

            Try
                dtcheck.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ExportOptiplan()
        If Me.otb.TabPages.Count > 0 Then
            Try



                Dim Op As New System.Windows.Forms.FolderBrowserDialog

                If _DefailtPath <> "" Then
                    Op.SelectedPath = _DefailtPath
                End If

                Try
                    If Op.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        If _DefailtPath <> Op.SelectedPath Then

                            WriteRegistry(Op.SelectedPath)
                            _DefailtPath = Op.SelectedPath

                        End If

                        pPath = _DefailtPath

                        Dim Indgrpbycode As String = ""

                        For Each T As DevExpress.XtraTab.XtraTabPage In Me.otb.TabPages
                            Dim FileName As String = Op.SelectedPath & "\" & T.Text.Replace("/", "_").Replace("\", "_").Replace("%", "_").Replace("(", "").Replace(")", "").Replace(",", "") & "_" & Indgrpbycode.Replace("/", "_").Replace("\", "_").Replace("%", "_").Replace("(", "").Replace(")", "") & ".txt"

                            For Each Obj As Object In T.Controls.Find("TG" & Microsoft.VisualBasic.Right(T.Name.ToString, T.Name.ToString.Length - 1), True)

                                Try

                                    With CType(Obj, DevExpress.XtraGrid.GridControl)
                                        .ExportToText(FileName)
                                    End With

                                Catch ex As Exception
                                End Try

                                Exit For

                            Next

                        Next

                        Call CreateLogExportOptiplan()

                        Dim cmdstring As String = ""

                        cmdstring = "  UPDATE BM  SET FTStateExportOptiplan ='1'  "
                        cmdstring &= vbCrLf & " , FTStateFirstExportOptiplanUser= CASE WHEN ISNULL(FTStateFirstExportOptiplanUser,'') ='' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE  FTStateFirstExportOptiplanUser END  "
                        cmdstring &= vbCrLf & " , FDStateFirstExportOptiplanDate= CASE WHEN ISNULL(FTStateFirstExportOptiplanUser,'') ='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE  FDStateFirstExportOptiplanDate END  "
                        cmdstring &= vbCrLf & " , FTStateFirstExportOptiplanTime= CASE WHEN ISNULL(FTStateFirstExportOptiplanUser,'') ='' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE  FTStateFirstExportOptiplanTime END  "
                        cmdstring &= vbCrLf & " , FTStateLastExportOptiplanUser=CASE WHEN ISNULL(FTStateFirstExportOptiplanUser,'') <>'' THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE  '' END  "
                        cmdstring &= vbCrLf & " , FDStateLastExportOptiplanDate=CASE WHEN ISNULL(FTStateFirstExportOptiplanUser,'') <>'' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE  '' END  "
                        cmdstring &= vbCrLf & " , FTStateLastExportOptiplanTime=CASE WHEN ISNULL(FTStateFirstExportOptiplanUser,'') <>'' THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE  '' END  "
                        cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTBOM_Mat AS BM "
                        cmdstring &= vbCrLf & "   WHERE     BM.FNHSysBomId = " & BOMID & " And BM.FTStateActive ='1' AND BM.FTStateMatConfirm ='1' AND ISNULL(BM.FTStateExportOptiplan,'') <>'1'  AND BM.FTRunColor ='1'  AND  BM.FTPart <>'' AND ISNULL(BM.FTStateHemNotOptiplan,'') <>'1' "


                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        ' HI.MG.ShowMsg.mInfo("Export Data Complete..", 1406120400, Me.Text, , MessageBoxIcon.Information)

                    End If

                Catch ex As Exception
                End Try

            Catch ex As Exception
            End Try
        Else
            'HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export ", 1406120399, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub CreateLogExportOptiplan()
        Dim StrSql As String = ""
        StrSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].dbo.HSysAuditLogExportOptiplan  "
        StrSql &= vbCrLf & "  (FTExportOpUser, FDExportOpDate, FTExportOpTime, FTExportOpBuy, FTExportOpStyle, FTExportOpOrder, FTExportOpPath)  "
        StrSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        StrSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
        StrSql &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pBuyCode) & "'"
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pStyleCode) & "'"
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pOrderNo) & "'"
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPath) & "'"

        HI.Conn.SQLConn.ExecuteOnly(StrSql, Conn.DB.DataBaseName.DB_LOG)

    End Sub

    Private Sub LoadMPRInfo()


        Dim _Qry As String = ""

        Dim dtMPRInfo As New DataTable

        Try

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[USP_GETBOMGARMENT_MRPINFO] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.GridCalculated.DataSource = dtMPRInfo

            '  GridView3 = GridCalculated.Views(0)

            GridViewCalculated.OptionsView.ShowAutoFilterRow = True
            GridViewCalculated.BestFitColumns()
            ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridViewCalculated.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200

            'Me.GridCalculated = GridView3.GridControl
            'Me.GridCalculated.Refresh()

        Catch ex As Exception
        End Try

        If dtMPRInfo.Rows.Count >= 1 Then

        Else

        End If

    End Sub
    Private Sub LoadMPRInfoNotIn()

        Dim _Qry As String = ""

        Dim dtMPRInfo As New DataTable
        Try


            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[USP_GETBOMGARMENT_GETMPR_NOTIN] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            Me.ogcmrpnot.DataSource = dtMPRInfo
            '  Dim view2 As GridView

            'view2 = ogcmrpnot.Views(0)
            ogvmrpnot.OptionsView.ShowAutoFilterRow = True
            ogvmrpnot.BestFitColumns()
            ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            ogvmrpnot.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            '  Me.GridCalculated = view2.GridControl
            'Me.GridCalculated.Refresh()

        Catch ex As Exception
        End Try



    End Sub

    Private Sub GridView1_PrintInitialize(sender As System.Object,
            e As DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs) _
            Handles ogvmatcode.PrintInitialize
        Dim pb As PrintingSystemBase = CType(e.PrintingSystem, PrintingSystemBase)
        pb.PageSettings.Landscape = True
    End Sub

    Private Sub GenCrystalReport(ByVal strReportPath As String, ByVal strReportName As String)
        Try
            Dim dataView1 As DataView
            Dim op As CriteriaOperator = GridViewCalculated.ActiveFilterCriteria
            Dim filterString As String = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(op)
            Dim _Qry As String = ""

            dataView1 = GridViewCalculated.DataSource
            Try
                dataView1.RowFilter = filterString
            Catch ex As Exception
            End Try

            'Dim dt As DataTable = CType(GridCalculated.DataSource, DataTable)

            'Dim ds As DataSet = New DataSet()
            'Dim dt As DataTable = New DataTable()
            'dt = dataView1.ToTable("Table")
            'ds.Tables.Add(dt)

            '' Check file exists
            If Not IO.File.Exists(Application.StartupPath & "\Reports\" & strReportPath & strReportName) Then
                'Throw (New Exception("Unable to locate report file:" & strReportName))
                MsgBox("Unable to locate report file: " & strReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
                Exit Sub
            End If

            ''Assign the datasource and set the properties for Report viewer


            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GETMPR_TEMP_FOR_REPORT " & StyleID & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim strFoumalar As String = ""

            strFoumalar += "{TMERTMPR.UserLogin} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"



            If strFoumalar <> "" Then strFoumalar += " AND "
                REM 2014/05/15 strFoumalar += "{TMERTMPR.FNHSysBuyId} = " & Val(FNHSysBuyId.Properties.Tag.ToString()) & ""
                strFoumalar += "{TMERTMPR.FNHSysStyleId} = " & StyleID & ""

            Dim OrderNoArr As String = GetOrderArrForReport()

            If OrderNoArr <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += OrderNoArr
            End If

            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In GridViewCalculated.Columns
                Dim K As String = ""
                Dim KValue As String = ""
                Try
                    K = GridCol.FilterInfo.FilterString
                Catch ex As Exception
                End Try

                Try
                    KValue = GridCol.FilterInfo.Value.ToString
                Catch ex As Exception
                End Try

                Select Case True 'GridCol.FieldName.ToString
                    Case GridCol.FieldName.ToString = "FTOrderNo" And (K <> "") And (KValue = "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTStyleCode" And (K <> "") And (KValue = "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If
                    Case GridCol.FieldName.ToString = "FTMainMatCode" And (K <> "") And (KValue = "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTOrderNo" And (KValue <> "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TMERTMPR.FTOrderNo} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TMERTMPR.FTOrderNo} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If
                    Case GridCol.FieldName.ToString = "FTStyleCode" And (KValue <> "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TMERTMPR.FTStyleCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TMERTMPR.FTStyleCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If
                    Case GridCol.FieldName.ToString = "FTMainMatCode" And (KValue <> "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TMERTMPR.FTMainMatCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TMERTMPR.FTMainMatCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If
                    Case Else
                End Select
            Next

            With New HI.RP.Report
                .ReportFolderName = strReportPath
                .ReportName = strReportName
                .AddParameter("FTStateSplitFabric", "0")
                .Formular = strFoumalar
                .ReportTitle = "PR-" & FNHSysStyleId.Text.Trim & "-" & FNHSysSeasonId.Text.Trim
                .Preview()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

#End Region


    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvmrp.Columns

            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTMainMatCode", "FTMainMatName", "FTPositionPartName", "FTMatColorCode", "FTRawMatColorName", "FTOrderNo", "FTRawMatSizeName", "FTGACDAte", "FTMatSizeCode"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Sub ogvmrp_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvmrp.CellMerge
        Try
            With Me.ogvmrp

                Select Case e.Column.FieldName.ToString
                    Case "FTStyleCode".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTMainMatCode".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTMainMatName".ToString, "FTPositionPartName".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "FTMatColorCode".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                              And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTRawMatColorName".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMatColorCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTRawMatSizeName".ToString, "FTMatSizeCode".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMatColorCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTOrderNo".ToString

                        If .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTGACDAte".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMatColorCode").ToString _
                             And .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case Else

                        e.Merge = False
                        e.Handled = True

                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub otb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmrp.SelectedPageChanged
        Try


            For Each bt As Object In UIButtonPanel.Buttons

                Select Case HI.ENM.Control.GeTypeControl(bt)
                    Case ENM.Control.ControlType.WindowsUIButton
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUIButton)

                            If .Tag.ToString = "exit" Then
                                .Visible = True

                            Else

                                Select Case .Tag.ToString
                                    'Case "load"
                                    '    .Visible = (e.Page Is otpmatcode)

                                    Case "load", "calculate"
                                        .Visible = (e.Page Is otpmatcode Or e.Page Is otpoptiplan)
                                    Case "delete", "preview"
                                        .Visible = (e.Page Is CalcMPR)

                                End Select

                            End If


                        End With

                    Case ENM.Control.ControlType.WindowsUISeparator
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUISeparator)

                            .Visible = Not (e.Page Is CalcMPR Or e.Page Is CalcMPRNotIn)

                        End With
                End Select


            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otb_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otbmrp.SelectedPageChanging

        Dim _Str As String = ""



        Try



            With CType(GridOrderList.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                    e.Cancel = True
                    Exit Sub

                Else
                    e.Cancel = False
                End If


                Dim Spls As New HI.TL.SplashScreen("Loading... Data Please wait.")

                Try

                    Select Case e.Page.Name
                        Case CalcMPR.Name, tabmrpreport.Name, CalcMPRNotIn.Name
                            With CType(GridOrderList.DataSource, DataTable)
                                .AcceptChanges()

                                If .Select("FTSelect='1'").Length <= 0 Then
                                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                                    Exit Sub
                                End If


                                Dim dtins As New DataTable
                                dtins = .Select("FTSelect='1'").CopyToDataTable()
                                If dtins Is Nothing Then
                                    dtins.Columns.Add("FTOrderNo", GetType(String))

                                Else

                                    dtins.BeginInit()
                                    For Each Col As DataColumn In .Columns
                                        If Col.ColumnName = "FTOrderNo" Then
                                        Else
                                            dtins.Columns.Remove(Col.ColumnName)
                                        End If
                                    Next
                                    dtins.BeginInit()

                                End If

                                HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)


                            End With
                    End Select



                    Select Case e.Page.Name
                        Case CalcMPR.Name
                            Call LoadMPRInfo()
                        Case tabmrpreport.Name
                            Call LoadMRPReport()
                        Case CalcMPRNotIn.Name
                            Call LoadMPRInfoNotIn()
                    End Select

                Catch ex As Exception

                End Try

                Spls.Close()

            End With



        Catch ex As Exception
        End Try



    End Sub

    Private Sub RepositoryItemCheckEditFNSelect_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEditFNSelect.EditValueChanged
        'Try

        '    If Not (GridOrderList.DataSource Is Nothing) Then
        '        CType(GridOrderList.DataSource, DataTable).AcceptChanges()
        '        GridOrderList.Refresh()
        '    End If

        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try
            If _StateLoadOrder = True Then Exit Sub

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With GridOrderList
                If Not (.DataSource Is Nothing) And ogvorderlist.RowCount > 0 Then

                    With ogvorderlist

                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)

                        Next

                    End With

                    CType(.DataSource, DataTable).AcceptChanges()

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otb_Click(sender As Object, e As EventArgs) Handles otbmrp.Click

    End Sub

    Private Sub ocmautosc_Click(sender As Object, e As EventArgs)
        If GridCalculated.DataSource Is Nothing Then
            Exit Sub
        End If

        Try
            If GridViewCalculated.RowCount <= 0 Then Exit Sub

            Dim StateAcc As Boolean = False

            Dim cmdstring As String = ""
            cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempAutoSourcing where UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            With GridViewCalculated
                For i As Integer = 0 To .RowCount - 1

                    If .GetRowCellValue(i, .Columns.ColumnByFieldName("FTStateAcc")).ToString = "1" Then
                        StateAcc = True

                        cmdstring = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempAutoSourcing "
                        cmdstring &= vbCrLf & " SET FTSubOrderNo ='' "
                        cmdstring &= vbCrLf & " WHERE  UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "        AND FNHSysRawMatId=" & Val(.GetRowCellValue(i, .Columns.ColumnByFieldName("FNHSysRawMatId")).ToString) & ""
                        cmdstring &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(i, .Columns.ColumnByFieldName("FTOrderNo")).ToString) & "'"

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) = False Then

                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempAutoSourcing(UserLogin, FNHSysRawMatId, FTOrderNo, FTSubOrderNo,FTCusItemCodeRef) "
                            cmdstring &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & Val(.GetRowCellValue(i, .Columns.ColumnByFieldName("FNHSysRawMatId")).ToString) & ""
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.GetRowCellValue(i, .Columns.ColumnByFieldName("FTOrderNo")).ToString) & "'"
                            cmdstring &= vbCrLf & ",''"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.GetRowCellValue(i, .Columns.ColumnByFieldName("FTCusItemCodeRef")).ToString) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    End If

                Next
            End With


            If StateAcc = True Then
                Dim dtData As New DataSet
                Dim dtData1 As New DataTable
                Dim dtData2 As New DataTable

                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_AUTOGEN_SOURCING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, dtData)


                Try
                    dtData1 = dtData.Tables(0)
                    dtData2 = dtData.Tables(1)

                    Dim CompleteData As Integer = Val(dtData1.Rows(0)!FNComplete.ToString())


                    If CompleteData > 0 Then
                        HI.MG.ShowMsg.mInfo("Auto Sourcing Data Complete ...", 190055471, Me.Text, " จำนวน " & CompleteData.ToString() & " รายการ", MessageBoxIcon.Warning)
                    End If

                    If dtData2.Select("FTStateMapSupl<>'1'").Length > 0 Then

                    End If

                Catch ex As Exception

                End Try
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการ Acc กรุณาทำการตรวจสอบ !!!", 121345748, Me.Text,, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvmatcode_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvmatcode.CustomColumnDisplayText

        Try
            If e.Column.FieldName = "FNRepeatLengthCM" Then

                If Val(e.Value) = 0 Then
                    e.DisplayText = ""
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView3_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)

        Try
            If e.Column.FieldName = "FNRepeatLengthCM" Then

                If Val(e.Value) = 0 Then
                    e.DisplayText = ""
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub RepositoryItemCheckFTStateAcc_EditValueChanging(sender As Object, e As ChangingEventArgs)
    '    Try
    '        With Me.GridViewCalculated
    '            If .GetFocusedRowCellValue("FNStateSourcing").ToString = "0" Then
    '                e.Cancel = False
    '            Else
    '                e.Cancel = True
    '            End If

    '        End With
    '    Catch ex As Exception
    '        e.Cancel = True
    '    End Try
    'End Sub

    Private Sub FNSelectOrderType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNSelectOrderType.SelectedIndexChanged
        Try
            If FNHSysStyleId.Text <> "" Then

                Call LoadOrderInfo()

                ogcmatcode.DataSource = Nothing
                otb.TabPages.Clear()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogdorder_EmbeddedNavigator_ButtonClick(sender As Object, e As NavigatorButtonClickEventArgs)

    End Sub

    Private Sub GroupControl1_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl1.Paint

    End Sub

    Private Sub FNCaltype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNCaltype.SelectedIndexChanged
        Try
            'otpoptiplan.PageVisible = (FNCaltype.SelectedIndex = 0 Or FNCaltype.SelectedIndex = 2)
            'otpmatcode.PageVisible = (FNCaltype.SelectedIndex = 0 Or FNCaltype.SelectedIndex = 1)


            Select Case FNCaltype.SelectedIndex
                Case 0
                    otpmatcode.PageVisible = True
                    otpoptiplan.PageVisible = True

                    otbmrp.SelectedTabPage = otpmatcode
                Case 1
                    otpmatcode.PageVisible = True
                    otpoptiplan.PageVisible = False

                    otbmrp.SelectedTabPage = otpmatcode
                Case 2
                    otpmatcode.PageVisible = False
                    otpoptiplan.PageVisible = True

                    otbmrp.SelectedTabPage = otpoptiplan
            End Select


            Call HI.UL.AppRegistry.WriteRegistry("BOMCalType" & Me.Name, FNCaltype.SelectedIndex.ToString)


        Catch ex As Exception

        End Try


    End Sub


    Private Sub LoadMRPData()


        If FNSelectOrderType.SelectedIndex = 3 Then
            Exit Sub
        End If

        With CType(GridOrderList.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If



            Dim dtins As New DataTable
            dtins = .Select("FTSelect='1'").CopyToDataTable()
            If dtins Is Nothing Then
                dtins.Columns.Add("FTOrderNo", GetType(String))

            Else

                dtins.BeginInit()
                For Each Col As DataColumn In .Columns
                    If Col.ColumnName = "FTOrderNo" Then
                    Else
                        dtins.Columns.Remove(Col.ColumnName)
                    End If
                Next
                dtins.BeginInit()

            End If

            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)


        End With

        Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")

        Try

            Dim _Str As String = ""
            '  Dim OrderNo As String = GetOrderArr()
            ogcmatcode.DataSource = Nothing
            ' GridOrderList.DataSource = Nothing
            GridCalculated.DataSource = Nothing

            Select Case FNCaltype.SelectedIndex
                Case 0

                    Call LoadBOMInfo(False, _Spls)
                    Call LoadOptiplanData(_Spls)

                Case 1

                    Call LoadBOMInfo(False, _Spls)

                Case 2
                    Call LoadOptiplanData(_Spls)

            End Select

        Catch ex As Exception
        End Try

        Me.otbmrp.SelectedTabPageIndex = 0

        _Spls.Close()

    End Sub

    Private Sub CalMRPData()

        If Me.ogcmatcode.DataSource Is Nothing Then
            Exit Sub
        End If

        If FNSelectOrderType.SelectedIndex = 3 Then
            Exit Sub
        End If

        Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")


        Select Case FNCaltype.SelectedIndex
            Case 0

                Call LoadBOMInfo(True, _Spls)
                Call ExportOptiplan()

            Case 1

                Call LoadBOMInfo(True, _Spls)

            Case 2
                Call ExportOptiplan()

        End Select

        otbmrp.SelectedTabPage = CalcMPR
        StateCal = True
        _Spls.Close()

    End Sub


    Private Sub DeleteMRPData()
        With CType(GridCalculated.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายการ MRP ที่ต้องการลบ !!!", 1511255471, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If



        End With



        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then

            Dim Spls As New HI.TL.SplashScreen("Deleting Data...")


            Dim cmdstring As String = ""
            Dim StateComplete As Boolean = False
            Dim Rdx As Integer = 0

            With CType(GridCalculated.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSelect='1'")

                    Rdx = Rdx + 1

                    cmdstring &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].[dbo].TMERTMPR_History (  FTUserLogin, FTDateCreate, FNCaltype,FTStyleCode, FTSeasonCode, FTOrderNo, FTMainMatCode, FTRawMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH"
                    cmdstring &= vbCrLf & "  , FTRawMatSizeCode,  FNQuantity, FNHSysRawMatId, FNHSysStyleId, FNHSysSeasonId,FTFilterItem ,FTCalTypeName,FTCalDate,FTCalTime,FNRowSeq"
                    cmdstring &= vbCrLf & "  , FTSubOrderNo,FNUsedQuantity,FNUsedPlusQuantity,FNPRQuantity,FDShipDate,FDOGacDate)"
                    cmdstring &= vbCrLf & "   Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Getdate(),1, ST.FTStyleCode "
                    cmdstring &= vbCrLf & "  , SS.FTSeasonCode  "
                    cmdstring &= vbCrLf & "  , A.FTOrderNo "
                    cmdstring &= vbCrLf & "  , A.FTMainMatCode "
                    cmdstring &= vbCrLf & "  , A.FTRawMatColorCode "
                    cmdstring &= vbCrLf & "  , A.FTRawMatColorNameEN"
                    cmdstring &= vbCrLf & "  , A.FTRawMatColorNameTH"
                    cmdstring &= vbCrLf & " , A.FTRawMatSizeCode"
                    cmdstring &= vbCrLf & " , SUM(A.FNPRQuantity) AS FNQuantity	"
                    cmdstring &= vbCrLf & " , A.FNHSysRawMatId "
                    cmdstring &= vbCrLf & " , A.FNHSysStyleId "
                    cmdstring &= vbCrLf & " , A.FNHSysSeasonId	"
                    cmdstring &= vbCrLf & " , ''	"
                    cmdstring &= vbCrLf & " , 'Delete'	"
                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "	"
                    cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & "	"

                    cmdstring &= vbCrLf & " , " & (Rdx * 1000) & "  +  Row_Number() Over (Partition by A.FTOrderNo,A.FTMainMatCode,A.FTRawMatColorCode,A.FTRawMatSizeCode Order by A.FTSubOrderNo,A.FTMainMatCode,A.FTRawMatColorCode ) AS FNRowSeq	"
                    cmdstring &= vbCrLf & "  ,A.FTSubOrderNo,SUM(A.FNUsedQuantity) AS FNUsedQuantity,SUM(A.FNUsedPlusQuantity) AS FNUsedPlusQuantity,SUM(A.FNPRQuantity) AS FNPRQuantity "
                    cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDate,'') ) AS FDShipDate "
                    cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDateOrginal,'') ) AS FDOGacDate "

                    cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR AS A WITH(NOLOCK) "
                    cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 ST.FTStyleCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMStyle AS ST WITH(NOLOCK) WHERE ST.FNHSysStyleId =A.FNHSysStyleId ) AS ST  "
                    cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 SS.FTSeasonCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMSeason  AS SS WITH(NOLOCK) WHERE SS.FNHSysSeasonId =A.FNHSysSeasonId ) AS SS "
                    cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 Sub.FDShipDate,Sub.FDShipDateOrginal  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrderSub  AS Sub WITH(NOLOCK) WHERE Sub.FTSubOrderNo =A.FTSubOrderNo ) AS Sub "
                    cmdstring &= vbCrLf & "  WHERE A.FTOrderNo  ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'  "
                    cmdstring &= vbCrLf & "  AND A.FTSubOrderNo  ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'  "
                    cmdstring &= vbCrLf & "  AND A.FNHSysMerMatId  =" & Val(R!FNHSysMerMatId.ToString) & "  "
                    cmdstring &= vbCrLf & "  AND A.FTMatColorCode  ='" & HI.UL.ULF.rpQuoted(R!FTMatColorCode.ToString) & "'  "
                    cmdstring &= vbCrLf & "  AND A.FTMatSizeCode  ='" & HI.UL.ULF.rpQuoted(R!FTMatSizeCode.ToString) & "'  "

                    cmdstring &= vbCrLf & "   GROUP BY ST.FTStyleCode  "
                    cmdstring &= vbCrLf & "	 ,SS.FTSeasonCode "
                    cmdstring &= vbCrLf & " ,A.FTOrderNo ,A.FTSubOrderNo"
                    cmdstring &= vbCrLf & " ,A.FTRawMatColorCode, A.FTRawMatSizeCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH, A.FTMainMatCode, A.FNHSysRawMatId, A.FNHSysStyleId, A.FNHSysSeasonId "


                    cmdstring &= vbCrLf & "   Delete A"
                    cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR As A "
                    cmdstring &= vbCrLf & "  WHERE A.FTOrderNo  ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'  "
                    cmdstring &= vbCrLf & "  AND A.FTSubOrderNo  ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'  "
                    cmdstring &= vbCrLf & "  AND A.FNHSysMerMatId  =" & Val(R!FNHSysMerMatId.ToString) & "  "
                    cmdstring &= vbCrLf & "  AND A.FTMatColorCode  ='" & HI.UL.ULF.rpQuoted(R!FTMatColorCode.ToString) & "'  "
                    cmdstring &= vbCrLf & "  AND A.FTMatSizeCode  ='" & HI.UL.ULF.rpQuoted(R!FTMatSizeCode.ToString) & "'  "
                    cmdstring &= vbCrLf & "    DELETE A "
                    cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS A "
                    cmdstring &= vbCrLf & " 	Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] AS C WITH(NOLOCK)"
                    cmdstring &= vbCrLf & " 	On A.FTOrderNo = C.FTOrderNo"
                    cmdstring &= vbCrLf & " 	And A.FNHSysRawMatId=C.FNHSysRawMatId"
                    cmdstring &= vbCrLf & "  WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'    AND A.FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & " And C.FTOrderNo Is NULL "


                Next

            End With


            If HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) Then

                Call LoadMPRInfo()

                StateComplete = True
            End If

            Spls.Close()

            If StateComplete Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If


        End If
    End Sub


    Private Sub PreviewMRPData()

        Try
            With CType(GridOrderList.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                Dim dtins As New DataTable
                dtins = .Select("FTSelect='1'").CopyToDataTable()
                If dtins Is Nothing Then
                    dtins.Columns.Add("FTOrderNo", GetType(String))

                Else

                    dtins.BeginInit()
                    For Each Col As DataColumn In .Columns
                        If Col.ColumnName = "FTOrderNo" Then
                        Else
                            dtins.Columns.Remove(Col.ColumnName)
                        End If
                    Next
                    dtins.BeginInit()

                End If

                HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)

            End With



            Dim currentCursor As Cursor = Cursor.Current
            Cursor.Current = Cursors.WaitCursor


            GenCrystalReport("Merchandise Report\", "Mrpreport_CreateTemp.rpt")
            Cursor.Current = currentCursor
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub UIButtonPanel_ButtonClick(sender As Object, e As ButtonEventArgs) Handles UIButtonPanel.ButtonClick
        Select Case e.Button.Properties.Tag
            Case "load"
                LoadMRPData()
            Case "calculate"
                CalMRPData()
            Case "delete"
                DeleteMRPData()
            Case "preview"
                PreviewMRPData()
            Case "exit"
                Me.Close()
        End Select
    End Sub

    Private Sub UIButtonPanel_Click(sender As Object, e As EventArgs) Handles UIButtonPanel.Click

    End Sub

    Private Sub RepositoryItemCheckEditFNSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEditFNSelect.EditValueChanging
        Try

            Dim pValue As String = "0"
            If e.NewValue.ToString = "1" Then
                pValue = "1"
            End If

            With ogvorderlist
                .SetFocusedRowCellValue("FTSelect", pValue)
            End With

            e.Cancel = False


        Catch ex As Exception
            e.Cancel = True

        End Try

    End Sub

    Private Sub RepositoryItemCheckFTStateAcc_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckFTStateAcc.EditValueChanging
        Try

            Dim pValue As String = "0"
            If e.NewValue.ToString = "1" Then
                pValue = "1"
            End If

            Dim pItemCode As String = ""
            Dim pOrder As String = ""

            With GridViewCalculated
                pOrder = .GetFocusedRowCellValue("FTOrderNo").ToString


                pItemCode = .GetFocusedRowCellValue("FTMainMatCode").ToString
            End With


            With CType(GridCalculated.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTOrderNo='" & pOrder & "' AND FTMainMatCode='" & HI.UL.ULF.rpQuoted(pItemCode) & "'")
                    R!FTSelect = pValue
                Next

                .AcceptChanges()

            End With

            e.Cancel = False


        Catch ex As Exception
            e.Cancel = True

        End Try


    End Sub


    Private Sub GridViewCalculated_KeyDown(sender As Object, e As KeyEventArgs) Handles GridViewCalculated.KeyDown
        Try


            If (GridCalculated.DataSource Is Nothing) Then

                Exit Sub
            End If


            With Me.GridViewCalculated
                If .RowCount <= 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F1


                        Dim pSeq As Integer = 0

                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTSelect").ToString = "0" Then
                                .SetRowCellValue(I, "FTSelect", "1")

                            Else
                                .SetRowCellValue(I, "FTSelect", "0")

                            End If

                        Next



                End Select
            End With


            CType(GridCalculated.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try

    End Sub
End Class