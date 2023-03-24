Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports DevExpress.DataAccess
Imports DevExpress.DashboardCommon

Namespace Win_Dashboards
    Partial Public Class AbsentDashboard
        Inherits DevExpress.DashboardCommon.Dashboard
        Public Sub New()
            InitializeComponent()
        End Sub
    End Class


    'Namespace Dashboard_DataLoading
    '    Partial Public Class Form1
    '        Inherits XtraForm

    '        Public Sub New()
    '            InitializeComponent()
    '        End Sub

    '        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
    '            Dim dashboard As New Dashboard()
    '            Dim objectDataSource As New DashboardObjectDataSource()
    '            objectDataSource.DataSource = GetData()
    '            objectDataSource.Fill()
    '            dashboard.DataSources.Add(objectDataSource)
    '            dashboardViewer1.Dashboard = dashboard

    '            ' Creates a new grid dashboard item with two columns that display car models and prices.
    '            Dim grid1 As New GridDashboardItem()
    '            grid1.DataSource = dashboard.DataSources(0)
    '            grid1.Columns.Add(New GridDimensionColumn(New Dimension("Model")))
    '            grid1.Columns.Add(New GridMeasureColumn(New Measure("Price")))
    '            dashboard.Items.Add(grid1)
    '        End Sub

    '        ' Handles the DashboardViewer.DataLoading event to provide the dashboard with new data.
    '        Private Sub dashboardViewer1_DataLoading(ByVal sender As Object, _
    '                                                 ByVal e As DataLoadingEventArgs) _
    '                                             Handles dashboardViewer1.DataLoading
    '            If e.DataSourceName = "Object Data Source 1" Then
    '                e.Data = UpdateData()
    '            End If
    '        End Sub

    '        Public Function GetData() As DataTable
    '            Dim xmlDataSet As New DataSet()
    '            xmlDataSet.ReadXml("..\..\Data\Cars_1.xml")
    '            Return xmlDataSet.Tables("Cars")
    '        End Function

    '        Public Function UpdateData() As DataTable
    '            Dim xmlDataSet As New DataSet()
    '            xmlDataSet.ReadXml("..\..\Data\Cars_2.xml")
    '            Return xmlDataSet.Tables("Cars")
    '        End Function

    '        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
    '            ' Reloads data in data sources.
    '            dashboardViewer1.ReloadData()
    '        End Sub
    '    End Class
    'End Namespace
End Namespace