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
Imports System.Globalization
Imports System.Configuration
Imports System.Diagnostics
Imports DevExpress.XtraPrintingLinks
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO
Imports Microsoft.Win32

Public Class wExportFacricDataToIntelecut


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Private Inited As Boolean
    Private _Clear As Boolean = False
    Dim FirstLoad As Boolean = True
    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip
    Private Shared _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _DefailtPath As String


#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'With RepositoryFTMainMatCode
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        'End With
        Call CreateManuStripGrid()
        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Function ReadRegistry() As String
        Dim regKey As RegistryKey
        Dim valreturn As String = ""

        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExportTelecut", "")
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

        regKey.SetValue("PathExportTelecut", value.ToString)
        regKey.Close()

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

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

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

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub LoadOrderListingInfo()

        Me.otb.TabPages.Clear()

        Dim Qry As String = ""
        Dim dt As New DataTable
        Dim dtdata As New DataTable

        Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..[SP_Get_Data_Fabric_For_Intelecut] '" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)


        For Each R As DataRow In dt.Rows


            Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_Data_Fabric_Roll_For_Intelecut " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'," & Val(R!FNHSysRawMatId) & ""
            dtdata = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            Dim ogcn As New DevExpress.XtraGrid.GridControl
            Dim ogvn As New DevExpress.XtraGrid.Views.Grid.GridView

            With ogvn
                .Name = "TGV" & FTOrderNo.Text & R!FNHSysRawMatId.ToString
                .GridControl = ogcn
                .OptionsCustomization.AllowGroup = False
                .OptionsCustomization.AllowQuickHideColumns = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
                .OptionsView.ColumnAutoWidth = False
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowGroupPanel = False
                .OptionsView.ShowAutoFilterRow = False
                .OptionsPrint.AutoWidth = False
                .OptionsPrint.PrintHeader = True
            End With

            For I As Integer = 1 To 10

                Dim Colg As New DevExpress.XtraGrid.Columns.GridColumn()

                With Colg
                    .AppearanceHeader.Options.UseTextOptions = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                    Select Case I
                        Case 1

                            .Caption = "Roll Number"
                            .FieldName = "FTRollNo"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 2

                            .Caption = "Length"
                            .FieldName = "FNTotalBal"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 3

                            .Caption = "Width"
                            .FieldName = "FTFabricFrontSize"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 4

                            .Caption = "Weight"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 5

                            .Caption = "Shade"
                            .FieldName = "FTShades"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 6

                            .Caption = "Warp Shrinkage"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 7
                            .Caption = "Weft Shrinkage"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 8

                            .Caption = "GSM"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 9

                            .Caption = "Supplier"
                            .FieldName = "FTSuplier"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                        Case 10

                            .Caption = "Location"
                            .FieldName = "FTData"
                            .Name = "TGCOL" & R!FNHSysRawMatId.ToString

                    End Select

                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
                    .OptionsColumn.AllowMove = False
                    .OptionsColumn.AllowShowHide = False
                    .OptionsColumn.ReadOnly = True
                    .Visible = True
                    .VisibleIndex = I - 1
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .BestFit()
                    .Width = 150
                End With

                ogvn.Columns.Add(Colg)
            Next

            ogvn.BestFitColumns()

            With Otp
                .Name = "T" & FTOrderNo.Text & R!FNHSysRawMatId.ToString
                .Text = R!FTCusItemCodeRef.ToString & "  (  " & R!FTRawMatColorCode.ToString() & "  )"
            End With

            With ogcn
                .Name = "TG" & FTOrderNo.Text & R!FNHSysRawMatId.ToString
                .MainView = ogvn
                .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {ogvn})
                .ContextMenuStrip = _MContextMenuStripGrid
            End With
            Otp.Controls.Add(ogcn)

            otb.TabPages.Add(Otp)
            ogcn.Dock = DockStyle.Fill

            ogcn.DataSource = dtdata.Copy

        Next

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.otb.TabPages.Clear()

        Try
            _Clear = True
            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception

        End Try
        _Clear = False
    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean

        If Me.FTOrderNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTOrderNo_lbl.Text)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub

        Dim spl As New HI.TL.SplashScreen("Loading.....")
        Call LoadOrderListingInfo()
        spl.Close()
    End Sub

    Private Sub FuncExcel_Click(sender As System.Object, e As System.EventArgs) Handles FuncExcel.Click
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

                        For Each T As DevExpress.XtraTab.XtraTabPage In Me.otb.TabPages
                            Dim FileName As String = Op.SelectedPath & "\" & Me.FTOrderNo.Text.Replace("/", "_").Replace("\", "_").Replace("%", "_").Replace("(", "").Replace(")", "") & "_" & T.Text.Replace("/", "_").Replace("\", "_").Replace("%", "_").Replace("(", "").Replace(")", "").Replace(",", "") & ".xlsx"

                            For Each Obj As Object In T.Controls.Find("TG" & Microsoft.VisualBasic.Right(T.Name.ToString, T.Name.ToString.Length - 1), True)

                                Try

                                    With CType(Obj, DevExpress.XtraGrid.GridControl)
                                        .ExportToXlsx(FileName)
                                    End With

                                Catch ex As Exception
                                End Try

                                Exit For

                            Next

                        Next

                        HI.MG.ShowMsg.mInfo("Export Data Complete..", 1406120400, Me.Text, , MessageBoxIcon.Information)

                    End If

                Catch ex As Exception
                End Try

            Catch ex As Exception
            End Try
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export ", 1406120399, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region "Initial Grid"



#End Region


End Class