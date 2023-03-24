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
Imports DevExpress.XtraEditors.Controls

Public Class wCalculateOrderCarton


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Dim AppConfig As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)

    Private Inited As Boolean
    Private _Clear As Boolean = False
    Dim FirstLoad As Boolean = True

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

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FirstLoad = False


            DeclareDataTable()

            'lastSelectedPath =""
            LoadMaster()

            RepositoryItemGridLookUpEditFTMainMatCode.View.OptionsView.ShowAutoFilterRow = True
        Catch ex As Exception

        End Try



    End Sub

    Private Sub DeclareDataTable()
        Dim dt As New DataTable()
        dt.Columns.Add("FNSeq", GetType(Integer))
        dt.Columns.Add("FTMainMatCode", GetType(String))
        dt.Columns.Add("FNStartQty", GetType(Integer))
        dt.Columns.Add("FNEndQty", GetType(Integer))
        dt.Columns.Add("FTStateMain", GetType(String))

        ogcfabric.DataSource = dt.Copy
    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub LoadMaster()
        Dim cmd As String = ""
        Dim dt As DataTable



        cmd = "SELECT   FTMainMatCode ,  (LEFT(FTMainMatNameEN,200)) AS FTMainMatName from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat WITH(NOLOCK) WHERE FTStateActive='1' AND FNHSysMatGrpId=1310210006 AND FNHSysMatTypeId=1310210003    Order by FTMainMatCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditFTMainMatCode.DataSource = dt.Copy


        dt.Dispose()

    End Sub

    Private Sub LoadOrderListingInfo()
        Dim Qry As String = ""
        Dim dt As New DataTable


        Dim _Spls As New HI.TL.SplashScreen("Loading...data please wait")
        Dim dtins As New DataTable
        With CType(Me.ogcfabric.DataSource, DataTable)
            .AcceptChanges()
            dtins = .Copy
        End With


        HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMP_CARTON_CALCULATE", "@tbl", dtins, Conn.DB.DataBaseName.DB_MERCHAN)


        Dim ds As New DataSet
        Dim dtmain As New DataTable

        Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GETDATACALCULATE_CARTON '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Val(FNHSysCmpId.Properties.Tag.ToString) & "," & Val(FNHSysBuyId.Properties.Tag.ToString) & "," & Val(FNHSysCustId.Properties.Tag.ToString) & "," & Val(FNHSysStyleId.Properties.Tag.ToString) & "," & Val(FNHSysSeasonId.Properties.Tag.ToString) & "," & Val(FNHSysContinentId.Properties.Tag.ToString) & " ," & Val(FNHSysCountryId.Properties.Tag.ToString) & " ," & Val(FNHSysProvinceId.Properties.Tag.ToString) & " ," & Val(FNHSysPlantId.Properties.Tag.ToString) & " ," & Val(FNOrderCartonCalType.SelectedIndex) & "  "
        HI.Conn.SQLConn.GetDataSet(Qry, Conn.DB.DataBaseName.DB_MERCHAN, ds)

        Try

            dtmain = ds.Tables(0).Copy
            Call SetNewColumn(ds.Tables(1))

        Catch ex As Exception
        End Try

        Me.ogdmain.DataSource = dtmain

        Call SetFilerColumn()

        _Spls.Close()





    End Sub

    Private Sub SetFilerColumn()
        Try

            For Each c As GridColumn In ogvmain.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next


        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click

        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            _Clear = True
            HI.TL.HandlerControl.ClearControl(Me)

            DeclareDataTable()
            LoadMaster()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        _Clear = False
    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean

        If FNHSysBuyId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()
            Return False
        End If

        With CType(Me.ogcfabric.DataSource, DataTable)
            .AcceptChanges()

            If .Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการกำหนดกล่องให้ถูกต้อง !!!", 215544717, Me.Text,, MessageBoxIcon.Warning)
                Return False
            End If


            If .Select("FTMainMatCode<>''").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการกำหนดกล่องให้ถูกต้อง !!!", 215544717, Me.Text,, MessageBoxIcon.Warning)
                Return False
            End If


            If .Select("FTMainMatCode<>'' AND FTStateMain='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการกำหนดกล่อง หลัก !!!", 215544718, Me.Text,, MessageBoxIcon.Warning)
                Return False
            End If


        End With


        Return True
    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalculate.Click

        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub



        Call LoadOrderListingInfo()

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


#End Region

#Region "Initial Grid"

    Private Sub SetNewColumn(dt As DataTable)
        Try
            Dim StrCol As String = dt.Rows(0)!FTColumn.ToString

            With Me.ogvmain
                .BeginInit()



                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                        Case "CFIX".ToUpper

                        Case Else

                            Dim FName As String = .Columns(I).FieldName

                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                If StrCol <> "" Then

                    For Each R As String In StrCol.Split(",")


                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG

                            .FieldName = R.Replace("[", "").Replace("]", "")
                            .Name = "Size" & R.Replace(" ", "_").Replace("[", "").Replace("]", "")
                            .Caption = R.Replace("[", "").Replace("]", "")
                            .Visible = True

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


                            .DisplayFormat.FormatString = "{0:n0}"


                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = False
                                .ReadOnly = True
                            End With


                            .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .SummaryItem.DisplayFormat = "{0:n0}"
                            .Width = 60


                        End With

                        .Columns.Add(ColG)
                    Next

                End If


                Dim ColG2 As New DevExpress.XtraGrid.Columns.GridColumn
                With ColG2
                    .FieldName = "FNTotalOrderQuantity"
                    .Name = "xxFNTotalOrderQuantity"
                    .Caption = "Order Quantity"
                    .Visible = True

                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


                    .DisplayFormat.FormatString = "{0:n0}"


                    With .OptionsColumn
                        .AllowMove = False
                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False

                        .AllowEdit = False
                        .ReadOnly = True
                    End With


                    .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                    .SummaryItem.DisplayFormat = "{0:n0}"
                    .Width = 60

                End With

                .Columns.Add(ColG2)
                Dim dtItem As DataTable

                With CType(Me.ogcfabric.DataSource, DataTable)
                    .AcceptChanges()
                    dtItem = .Copy



                End With


                For Each Rx As DataRow In dtItem.Select("FTMainMatCode<>''", "FNSeq")



                    Dim ColG3 As New DevExpress.XtraGrid.Columns.GridColumn
                    With ColG3

                        .FieldName = Rx!FTMainMatCode.ToString.Replace("[", "").Replace("]", "").Replace(" ", "")
                        .Name = "Item" & Rx!FTMainMatCode.ToString.Replace("[", "").Replace("]", "").Replace(" ", "")
                        .Caption = Rx!FTMainMatCode.ToString.Replace("[", "").Replace("]", "").Replace(" ", "")
                        .Visible = True

                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

                        .DisplayFormat.FormatString = "{0:n0}"

                        With .OptionsColumn
                            .AllowMove = False
                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False

                            .AllowEdit = False
                            .ReadOnly = True
                        End With


                        .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                        .SummaryItem.DisplayFormat = "{0:n0}"
                        .Width = 120


                    End With

                    .Columns.Add(ColG3)

                Next

                .EndInit()
            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogcfabric_Click(sender As Object, e As EventArgs) Handles ogcfabric.Click

    End Sub

    Private Sub ogcfabric_EmbeddedNavigator_ButtonClick(sender As Object, e As NavigatorButtonClickEventArgs) Handles ogcfabric.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvfabric
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcfabric.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNSeq>0", "FNSeq")
                        R!FNSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridMat()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridMat()

            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub InitGridMat()


        If Not (Me.ogcfabric.DataSource Is Nothing) Then

            Dim dtemp As DataTable


            With CType(Me.ogcfabric.DataSource, DataTable)
                .AcceptChanges()


                If .Select("FTMainMatCode=''").Length <= 0 Then
                    .Rows.Add(.Rows.Count + 1, "", 0, 0, "0")
                End If



                ' dtemp = .Copy


            End With

            'If dtemp.Select("FTEmpCode=''").Length <= 0 Then
            '    dtemp.Rows.Add(dtemp.Rows.Count + 1, 0, "", "")
            'End If

            'Me.ogcemp.DataSource = dtemp.Copy

            'dtemp.Dispose()
        End If


    End Sub

    Private Sub RepositoryItemCheckEditFTStateMain_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEditFTStateMain.EditValueChanging
        Try

            Dim State As String = "0"
            If e.NewValue.ToString = "1" Then
                State = "1"
            End If


            With CType(ogcfabric.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    R!FTStateMain = "0"
                Next

                .AcceptChanges()
            End With

            ogvfabric.SetFocusedRowCellValue("FTStateMain", State)
        Catch ex As Exception

        End Try
    End Sub

#End Region



End Class