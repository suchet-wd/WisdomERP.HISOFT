Imports DevExpress.DashboardCommon
Imports System.ComponentModel
Imports DevExpress.DashboardWin
Imports System.Windows.Forms
Imports DevExpress.XtraEditors

Public Class DashboardDesignerForm
    Private saveDashboard_Renamed As Boolean

    Public ReadOnly Property Dashboard() As Dashboard
        Get
            Return DashboardDesigner.Dashboard
        End Get
    End Property
    Public ReadOnly Property SaveDashboard() As Boolean
        Get
            Return saveDashboard_Renamed
        End Get
    End Property

    Public Sub New(ByVal dashboard As Dashboard)
        InitializeComponent()

        DashboardDesigner.Dashboard = dashboard
    End Sub

    Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
        MyBase.OnClosing(e)
        If DashboardDesigner.IsDashboardModified Then
            Dim result As DialogResult = XtraMessageBox.Show(LookAndFeel, Me, "Do you want to save changes ?", "Dashboard Designer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = System.Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            Else
                saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes
            End If
        End If
    End Sub

    Private Sub DashboardDesignerForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Comment out the following lines to enable the New, Open, Save and Save As buttons in the Ribbon, 
        'as well as the Backstage View that allows you to quickly reopen recent dashboards.
        ' fileRibbonPageGroup1.Enabled = False
        FileNewBarItem1.Enabled = False
        FileOpenBarItem1.Enabled = False
        ' ribbonControl1.Toolbar.ItemLinks.Remove(FileSaveBarItem1)
        ' dashboardBackstageViewControl1.Enabled = False
    End Sub
End Class