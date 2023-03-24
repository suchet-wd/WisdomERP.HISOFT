Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.DashboardCommon
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Ribbon


Partial Public Class DesignerDashboard
    Inherits RibbonForm
    Private saveDashboard_Renamed As Boolean

    Public ReadOnly Property Dashboard() As Dashboard
        Get
            Return dashboardDesigner.Dashboard
        End Get
    End Property

    Public ReadOnly Property SaveDashboard() As Boolean
        Get
            Return saveDashboard_Renamed
        End Get
    End Property

    Public Sub New(ByVal dashboard As Dashboard)
        InitializeComponent()
        'Icon = ResourceImageHelper.CreateIconFromResourcesEx("DashboardMainDemo.AppIcon.ico", GetType(frmMain).Assembly)
        dashboardDesigner.Dashboard = dashboard
    End Sub

    Private _AddNewDashboard As Boolean
    Public Property AddNewDashboard As Boolean
        Get
            Return saveDashboard_Renamed
        End Get
        Set(value As Boolean)
            _AddNewDashboard = value
        End Set
    End Property


    Private _DashBoadhListName As DevExpress.XtraEditors.ComboBoxEdit
    Public Property DashBoadhListName As DevExpress.XtraEditors.ComboBoxEdit
        Get
            Return DashBoadhListName
        End Get
        Set(value As DevExpress.XtraEditors.ComboBoxEdit)
            DashBoadhListName = value
        End Set
    End Property

    Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
        MyBase.OnClosing(e)

        If dashboardDesigner.IsDashboardModified Then
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
        fileRibbonPageGroup1.Enabled = False
        fileNewBarItem1.Enabled = True
        fileOpenBarItem1.Enabled = True
        ' ribbonControl1.Toolbar.ItemLinks.Remove(fileSaveBarItem1)
        dashboardBackstageViewControl1.Enabled = False

    End Sub

End Class

