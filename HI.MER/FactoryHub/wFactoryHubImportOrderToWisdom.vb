Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
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
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Net
Imports Microsoft.Win32
Imports System.Web
Imports System.Runtime.Serialization
Imports System.ComponentModel
Imports System.Xml

Public Class wFactoryHubImportOrderToWisdom

    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip



    ''' Used Data Adapter to control database

    Private Inited As Boolean
    Private _Clear As Boolean = False
    Dim FirstLoad As Boolean = True

    Private StrAllPgm As String = "HIT,HIC,HIG,HTV,HIP,HSC"
    Private PathFileXML As String = System.Windows.Forms.Application.StartupPath & "\FHSXML"


#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


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



    End Sub




    Private Sub ShowData()

        Me.ogdmain.DataSource = Nothing

        Dim cmd As String = ""
        Call ClearColumnGrid()
        Dim OrderHeaders As New DataTable
        Dim OrderItems As New DataTable
        Dim OrderSizes As New DataTable
        Dim OrderItemsVas As New DataTable
        Dim OrderSizesVas As New DataTable
        Dim OrderItemsText As New DataTable

        cmd = " Select X.* from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders_Wisdom AS X WITH(NOLOCK)"

        If FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "" Then
            cmd &= vbCrLf & " INNER JOIN (SELECT DISTINCT PO_Number "
            cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems_Wisdom AS Z WITH(NOLOCK) "
            cmd &= vbCrLf & "  WHERE Z.GAC_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
            cmd &= vbCrLf & "  AND Z.GAC_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
            cmd &= vbCrLf & " ) AS Z ON X.PO_Number=Z.PO_Number "
        End If

        If FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "" Then
            cmd &= vbCrLf & "  WHERE X.PO_Doc_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "'"
            cmd &= vbCrLf & "  AND X.PO_Doc_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'"
        End If

        OrderHeaders = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt1.DataSource = OrderHeaders
        mogvdt1.BestFitColumns()


        cmd = " Select X.* from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems_Wisdom AS X WITH(NOLOCK)"
        If FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "" Then
            cmd &= vbCrLf & " INNER JOIN (SELECT DISTINCT PO_Number "
            cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders_Wisdom AS Z WITH(NOLOCK) "
            cmd &= vbCrLf & "  WHERE Z.PO_Doc_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "'"
            cmd &= vbCrLf & "  AND Z.PO_Doc_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'"
            cmd &= vbCrLf & " ) AS Z ON X.PO_Number=Z.PO_Number "
        End If

        If FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "" Then
            cmd &= vbCrLf & "  WHERE X.GAC_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
            cmd &= vbCrLf & "  AND X.GAC_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text)).Replace("/", "") & "'"
        End If

        OrderItems = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt2.DataSource = OrderItems
        mogvdt2.BestFitColumns()

        cmd = " Select X.* from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizes_Wisdom AS X WITH(NOLOCK)"

        If (FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "") Or (FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "") Then


            cmd &= vbCrLf & "  INNER JOIN  ( Select DISTINCT X2.PO_Number,X2.PO_Item  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems_Wisdom AS X2 WITH(NOLOCK)"
            If FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "" Then
                cmd &= vbCrLf & " INNER JOIN (SELECT DISTINCT PO_Number "
                cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders_Wisdom AS Z WITH(NOLOCK) "
                cmd &= vbCrLf & "  WHERE Z.PO_Doc_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND Z.PO_Doc_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & " ) AS Z ON X2.PO_Number=Z.PO_Number "
            End If

            If FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "" Then
                cmd &= vbCrLf & "  WHERE X2.GAC_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND X2.GAC_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text)).Replace("/", "") & "'"
            End If

            cmd &= vbCrLf & "  ) AS XZ ON   X.PO_Number = XZ.PO_Number AND X.PO_Item= XZ.PO_Item  "
        End If


        OrderSizes = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt3.DataSource = OrderSizes
        mogvdt3.BestFitColumns()

        cmd = " Select X.* from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsVas_Wisdom AS X WITH(NOLOCK)"

        If (FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "") Or (FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "") Then


            cmd &= vbCrLf & "  INNER JOIN  ( Select DISTINCT X2.PO_Number,X2.PO_Item  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems_Wisdom AS X2 WITH(NOLOCK)"
            If FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "" Then
                cmd &= vbCrLf & " INNER JOIN (SELECT DISTINCT PO_Number "
                cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders_Wisdom AS Z WITH(NOLOCK) "
                cmd &= vbCrLf & "  WHERE Z.PO_Doc_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND Z.PO_Doc_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & " ) AS Z ON X2.PO_Number=Z.PO_Number "
            End If

            If FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "" Then
                cmd &= vbCrLf & "  WHERE X2.GAC_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND X2.GAC_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text)).Replace("/", "") & "'"
            End If

            cmd &= vbCrLf & "  ) AS XZ ON   X.PO_Number = XZ.PO_Number AND X.PO_Item= XZ.PO_Item  "
        End If

        OrderItemsVas = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt4.DataSource = OrderItemsVas
        mogvdt4.BestFitColumns()

        cmd = " Select X.* from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizesVas_Wisdom AS X WITH(NOLOCK)"

        If (FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "") Or (FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "") Then


            cmd &= vbCrLf & "  INNER JOIN  ( Select  DISTINCT X2.PO_Number,X2.PO_Item  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems_Wisdom AS X2 WITH(NOLOCK)"
            If FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "" Then
                cmd &= vbCrLf & " INNER JOIN (SELECT DISTINCT PO_Number "
                cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders_Wisdom AS Z WITH(NOLOCK) "
                cmd &= vbCrLf & "  WHERE Z.PO_Doc_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND Z.PO_Doc_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & " ) AS Z ON X2.PO_Number=Z.PO_Number "
            End If

            If FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "" Then
                cmd &= vbCrLf & "  WHERE X2.GAC_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND X2.GAC_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text)).Replace("/", "") & "'"
            End If

            cmd &= vbCrLf & "  ) AS XZ ON   X.PO_Number = XZ.PO_Number AND X.PO_Item= XZ.PO_Item  "
        End If

        OrderSizesVas = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt5.DataSource = OrderSizesVas
        mogvdt5.BestFitColumns()

        cmd = " Select X.* from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsText_Wisdom AS X WITH(NOLOCK)"

        If (FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "") Or (FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "") Then


            cmd &= vbCrLf & "  INNER JOIN  ( Select DISTINCT X2.PO_Number,X2.PO_Item  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems_Wisdom AS X2 WITH(NOLOCK)"
            If FTStartDocDate.Text <> "" And FTEndDocDate.Text <> "" Then
                cmd &= vbCrLf & " INNER JOIN (SELECT DISTINCT PO_Number "
                cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders_Wisdom AS Z WITH(NOLOCK) "
                cmd &= vbCrLf & "  WHERE Z.PO_Doc_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND Z.PO_Doc_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & " ) AS Z ON X2.PO_Number=Z.PO_Number "
            End If

            If FTStartGacDate.Text <> "" And FTEndGacDate.Text <> "" Then
                cmd &= vbCrLf & "  WHERE X2.GAC_Date >= '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "'"
                cmd &= vbCrLf & "  AND X2.GAC_Date <= '" & (HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text)).Replace("/", "") & "'"
            End If

            cmd &= vbCrLf & "  ) AS XZ ON   X.PO_Number = XZ.PO_Number AND X.PO_Item= XZ.PO_Item  "
        End If

        OrderItemsText = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt6.DataSource = OrderItemsText
        mogvdt6.BestFitColumns()


        Dim ds As New DataSet
        Dim dtmain As New DataTable

        cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_GETFHS_DATA_FROM_WISDOM '" & (HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text)).Replace("/", "") & "','" & (HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text)).Replace("/", "") & "','" & (HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text)).Replace("/", "") & "' ,'" & (HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text)).Replace("/", "") & "'  "
        HI.Conn.SQLConn.GetDataSet(cmd, Conn.DB.DataBaseName.DB_FHS, ds)

        Try

            dtmain = ds.Tables(0).Copy
            Call SetNewColumn(ds.Tables(1))

        Catch ex As Exception
        End Try

        Me.ogdmain.DataSource = dtmain

        Call SetFilerColumn()

    End Sub


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
                            If R.Contains("Price") Then
                                .DisplayFormat.FormatString = "{0:n2}"
                            Else

                                .DisplayFormat.FormatString = "{0:n0}"
                            End If



                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = False
                                .ReadOnly = True
                            End With


                            If R.Contains("Price") Then
                                .Width = 70
                            Else
                                .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .SummaryItem.DisplayFormat = "{0:n0}"
                                .Width = 50
                            End If


                        End With

                        .Columns.Add(ColG)
                    Next

                End If


                Dim ColG2 As New DevExpress.XtraGrid.Columns.GridColumn
                With ColG2
                    .FieldName = "FTItemNote"
                    .Name = "FTItemNote"
                    .Caption = "Item Note"
                    .Visible = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                    With .OptionsColumn
                        .AllowMove = False
                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .AllowEdit = False
                        .ReadOnly = True
                    End With
                    .Width = 300
                End With

                .Columns.Add(ColG2)
                .EndInit()
            End With





        Catch ex As Exception

        End Try



    End Sub
    Private Sub SetFilerColumn()
        Try

            For Each c As GridColumn In ogvmain.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt1.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt2.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt3.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt4.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt5.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt6.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click

    End Sub
    Private Sub ClearColumnGrid()

        Try
            With ogvmain
                .BeginInit()


                For Each c As GridColumn In .Columns
                    Select Case c.FieldName
                        Case "FTSelect", "FTNikePO", "PO_Doc_Date", "PO_Number", "PO_Ref", "PO_Group", "Currency_Type", "Ship_Via_Instructions", "BUY_SEASON", "BUY_YEAR", "BUY_GROUP", "Factory_Vendor_Code", "Vendor_Location_Code_MCO", "PO_Item", "Material_Number" _
                            , "Material_Description", "Plant", "Nike_Division_Code", "UOM", "Mode_Code", "Mode_Code_Description", "OGAC_Date", "GAC_Date", "GAC_Reason_Code", "Material_Dev_Code", "Silhhouette_Code", "Gender_Age_Code", "SO_NUMBER", "SO_ITEM", "Color_Combo_Name", "Color_Combo_ShortName", "MSRP_US" _
                            , "FTStatenew", "FTStateAdd", "FTStateDeduct", "FTStateinfo", "Quantity", "FTOrderNo"

                        Case Else
                            .Columns.Remove(c)
                    End Select

                Next


                .EndInit()
            End With
        Catch ex As Exception

        End Try

    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click


        Dim Spls As New HI.TL.SplashScreen("Loading.... please wait")

            Me.ogdmain.DataSource = Nothing
            mogcdt1.DataSource = Nothing
            mogcdt2.DataSource = Nothing
            mogcdt3.DataSource = Nothing
            mogcdt4.DataSource = Nothing
            mogcdt5.DataSource = Nothing
            mogcdt6.DataSource = Nothing



            ' GetTokenData()
            Call ShowData()

            Me.otb.SelectedTabPageIndex = 0
            Spls.Close()


    End Sub


    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub






#End Region

End Class
