
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid

Public Class wWIPTemplate

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        pivotGridControl.OptionsChartDataSource.SelectionOnly = False
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = True
        pivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = False
        chartControl.CrosshairOptions.ShowArgumentLine = False
        chartControl.DataSource = pivotGridControl

    End Sub

    Public Enum StateWIP As Integer

        Cutting = 0
        Laser = 1
        ScreenPrinting = 2
        HeatTransfer = 3
        Embrodeiry = 4
        Supermarket = 5
        SewingPacking = 6

    End Enum

#Region "Property"

    Private _TemplateWIP As StateWIP = StateWIP.Cutting
    Property TemplateWIP As StateWIP

        Get
            Return _TemplateWIP
        End Get
        Set(value As StateWIP)
            _TemplateWIP = value
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

#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs)

        HI.TL.HandlerControl.ClearControl(Me)


    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' C1FlexGrid1.SaveExcel("Text.xls", C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells, C1.Win.C1FlexGrid.FileFlags.VisibleOnly)
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Sub LoadDataCut()

        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTMarkCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTUnitSectCode"
            _C1FTUnitSectCode.Name = "C1FTUnitSectCode"
            '
            'CFTMarkCode
            '
            _CFTMarkCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTMarkCode.AreaIndex = 0
            _CFTMarkCode.FieldName = "FTMarkCode"
            _CFTMarkCode.Name = "CFTMarkCode"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFTMarkCode, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = True
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_CUT "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()
        Catch ex As Exception

        End Try
      

    End Sub

    Private Sub LoadDataLaser()
        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTMarkCode"
            _C1FTUnitSectCode.Name = "C1FTMarkCode"
            '
            '_CFNStateType
            '
            _CFNStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFNStateType.AreaIndex = 0
            _CFNStateType.FieldName = "FTState"
            _CFNStateType.Name = "CFNStateType"
            ' _CFNStateType.Visible = False
            '
            '_CFNStateType
            '
            _CFTStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTStateType.AreaIndex = 1
            _CFTStateType.FieldName = "FTStateName"
            _CFTStateType.Name = "CFTStateType"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFNStateType, _CFTStateType, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_LASER "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadDataPrint()
        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTMarkCode"
            _C1FTUnitSectCode.Name = "C1FTMarkCode"
            '
            '_CFNStateType
            '
            _CFNStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFNStateType.AreaIndex = 0
            _CFNStateType.FieldName = "FTState"
            _CFNStateType.Name = "CFNStateType"
            ' _CFNStateType.Visible = False
            '
            '_CFNStateType
            '
            _CFTStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTStateType.AreaIndex = 1
            _CFTStateType.FieldName = "FTStateName"
            _CFTStateType.Name = "CFTStateType"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFNStateType, _CFTStateType, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_PRINT "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadDataEmb()
        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTMarkCode"
            _C1FTUnitSectCode.Name = "C1FTMarkCode"
            '
            '_CFNStateType
            '
            _CFNStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFNStateType.AreaIndex = 0
            _CFNStateType.FieldName = "FTState"
            _CFNStateType.Name = "CFNStateType"
            ' _CFNStateType.Visible = False
            '
            '_CFNStateType
            '
            _CFTStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTStateType.AreaIndex = 1
            _CFTStateType.FieldName = "FTStateName"
            _CFTStateType.Name = "CFTStateType"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFNStateType, _CFTStateType, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_EMB "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadDataHeat()
        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTMarkCode"
            _C1FTUnitSectCode.Name = "C1FTMarkCode"
            '
            '_CFNStateType
            '
            _CFNStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFNStateType.AreaIndex = 0
            _CFNStateType.FieldName = "FTState"
            _CFNStateType.Name = "CFNStateType"
            ' _CFNStateType.Visible = False
            '
            '_CFNStateType
            '
            _CFTStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTStateType.AreaIndex = 1
            _CFTStateType.FieldName = "FTStateName"
            _CFTStateType.Name = "CFTStateType"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFNStateType, _CFTStateType, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_HEAT "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadDataSupermarket()
        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTUnitSectCode"
            _C1FTUnitSectCode.Name = "C1FTUnitSectCode"
            '
            '_CFNStateType
            '
            _CFNStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFNStateType.AreaIndex = 0
            _CFNStateType.FieldName = "FTDataSeq"
            _CFNStateType.Name = "CFNStateType"
            ' _CFNStateType.Visible = False
            '
            '_CFNStateType
            '
            _CFTStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTStateType.AreaIndex = 1
            _CFTStateType.FieldName = "FTDataType"
            _CFTStateType.Name = "CFTStateType"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFNStateType, _CFTStateType, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_SUPERMARKET "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()
        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub LoadDataSewingPacking()
        Try
            If Me.pivotGridControl.Fields.Count > 0 Then
                Me.pivotGridControl.Fields.Clear()
            End If

            Dim _C1FTUnitSectCode As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFTStateType As New DevExpress.XtraPivotGrid.PivotGridField()
            Dim _CFNQuantity As New DevExpress.XtraPivotGrid.PivotGridField()

            'C1FTUnitSectCode
            '
            _C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            _C1FTUnitSectCode.AreaIndex = 0
            _C1FTUnitSectCode.FieldName = "FTUnitSectCode"
            _C1FTUnitSectCode.Name = "C1FTUnitSectCode"
            '
            '_CFNStateType
            '
            _CFNStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFNStateType.AreaIndex = 0
            _CFNStateType.FieldName = "FNStateType"
            _CFNStateType.Name = "CFNStateType"
            ' _CFNStateType.Visible = False
            '
            '_CFNStateType
            '
            _CFTStateType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            _CFTStateType.AreaIndex = 1
            _CFTStateType.FieldName = "FTStateType"
            _CFTStateType.Name = "CFTStateType"
            '
            'CFNQuantity
            '
            _CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
            _CFNQuantity.AreaIndex = 0
            _CFNQuantity.CellFormat.FormatString = "{0:n0}"
            _CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            _CFNQuantity.FieldName = "FNQuantity"
            _CFNQuantity.Name = "CFNQuantity"

            Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {_C1FTUnitSectCode, _CFNStateType, _CFTStateType, _CFNQuantity})
            pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_SEWING "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

  

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wWIPTemplate_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Call LoadDataSewingPacking()
        olbtitle.Text = "Supermarket"
        Call LoadDataSupermarket()
        TemplateWIP = StateWIP.Cutting
        otmtime.Enabled = True

    End Sub

    Private Sub otmtime_Tick(sender As Object, e As EventArgs) Handles otmtime.Tick
        otmtime.Enabled = False
        Select Case TemplateWIP
            Case StateWIP.Cutting

                olbtitle.Text = "Cutting"

                Call LoadDataCut()
                TemplateWIP = StateWIP.Laser

            Case StateWIP.Laser

                olbtitle.Text = "Laser"

                Call LoadDataLaser()
                TemplateWIP = StateWIP.ScreenPrinting

            Case StateWIP.ScreenPrinting

                olbtitle.Text = "Screen Printing"

                Call LoadDataPrint()
                TemplateWIP = StateWIP.HeatTransfer

            Case StateWIP.HeatTransfer

                olbtitle.Text = "Heat Transfer"

                Call LoadDataHeat()
                TemplateWIP = StateWIP.Embrodeiry

            Case StateWIP.Embrodeiry

                olbtitle.Text = "Embrodeiry"

                Call LoadDataEmb()
                TemplateWIP = StateWIP.Supermarket

            Case StateWIP.Supermarket

                olbtitle.Text = "Supermarket"

                Call LoadDataSupermarket()
                TemplateWIP = StateWIP.SewingPacking

            Case StateWIP.SewingPacking

                olbtitle.Text = "Sewing-Packing"
                Call LoadDataSewingPacking()

                TemplateWIP = StateWIP.Cutting
        End Select

        otmtime.Enabled = True
    End Sub
End Class