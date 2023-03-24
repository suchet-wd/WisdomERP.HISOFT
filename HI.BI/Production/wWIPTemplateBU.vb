
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid

Public Class wWIPTemplateBU

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
        pivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
        chartControl.CrosshairOptions.ShowArgumentLine = False
        chartControl.DataSource = pivotGridControl



        pivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = False
        pivotGridControl2.OptionsChartDataSource.SelectionOnly = False
        pivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = True
        pivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = False
        pivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = False
        chartControl2.CrosshairOptions.ShowArgumentLine = False
        chartControl2.DataSource = pivotGridControl2

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


    Private _UnitSectID As Integer = 0
    Property UnitSectID As Integer

        Get
            Return _UnitSectID
        End Get
        Set(value As Integer)
            _UnitSectID = value
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

    Private Sub LoadDataBU()
        Try

            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_BU " & UnitSectID & " "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl.DataSource = _dt.Copy
            _dt.Dispose()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadDataBUSewing()
        Try
            
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_WIPTEMPLATE_BU_SEWING " & UnitSectID & " "
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Me.pivotGridControl2.DataSource = _dt.Copy

            _dt.Dispose()

        Catch ex As Exception
        End Try

    End Sub

  
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub Form_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub wWIPTemplate_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim _Spls As New HI.TL.SplashScreen("Loading.....Data Plase wait.")
        Dim _cmd As String = ""
        Dim _dt As DataTable

        _cmd = "  SELECT A.FTComputerName, A.FNHSysUnitSectId, B.FTUnitSectCode"
        _cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigWIPTemplateCom AS A WITH(NOLOCK) INNER JOIN"
        _cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH(NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
        _cmd &= vbCrLf & " WHERE A.FTComputerName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_PROD)

        If _dt.Rows.Count <= 0 Then

            _cmd = "  SELECT A.FTComputerName, A.FNHSysUnitSectId, B.FTUnitSectCode"
            _cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigWIPTemplateCom AS A WITH(NOLOCK) INNER JOIN"
            _cmd &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH(NOLOCK) ON A.FNHSysUnitSectId = B.FNHSysUnitSectId"
            _cmd &= vbCrLf & " WHERE A.FTComputerName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_PROD)

        End If

        Dim _UnitID As Integer = 0
        Dim _UnitCode As String = ""

        For Each R As DataRow In _dt.Rows

            _UnitID = Val(R!FNHSysUnitSectId.ToString)
            _UnitCode = R!FTUnitSectCode.ToString

        Next

        UnitSectID = _UnitID
        olbtitle.Text = _UnitCode

        Call LoadDataBU()
        Call LoadDataBUSewing()
        TemplateWIP = StateWIP.Cutting
        otmtime.Enabled = True
        Timer2.Enabled = True
        _Spls.Close()

    End Sub

    Private Sub otmtime_Tick(sender As Object, e As EventArgs) Handles otmtime.Tick
        otmtime.Enabled = False
        Call LoadDataBU()
        otmtime.Enabled = True
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        Call LoadDataBUSewing()
        Timer2.Enabled = True
    End Sub

    Private Sub chartControl2_KeyDown(sender As Object, e As KeyEventArgs) Handles chartControl2.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub chartControl_KeyDown(sender As Object, e As KeyEventArgs) Handles chartControl.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub
End Class