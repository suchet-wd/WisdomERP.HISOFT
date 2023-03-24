Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.DashboardCommon
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Ribbon


Partial Public Class DesignerFormDashboard
    Inherits RibbonForm

    Private saveDashboard_Renamed As Boolean
    Private saveDashboard_Name As String
    Private saveDashboard_Idx As Integer
    Private _AddNewDashboard As Boolean = False

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

    Public Property SaveDashboardIdx() As Boolean
        Get
            Return saveDashboard_Idx
        End Get
        Set(value As Boolean)
            saveDashboard_Idx = value
        End Set
    End Property


    Public Property AddNewDashboard As Boolean
        Get
            Return _AddNewDashboard
        End Get
        Set(value As Boolean)
            _AddNewDashboard = value
        End Set
    End Property

    Public Property SaveDashboardName() As String
        Get
            Return saveDashboard_Name
        End Get
        Set(value As String)
            saveDashboard_Name = value
        End Set
    End Property

    Enum StateDashBoard As Integer
        MainDashboard = 0
        SubDashboard = 1
    End Enum

    Private _StateShowDashboard As StateDashBoard = StateDashBoard.MainDashboard
    Public Property StateShowDashboard() As StateDashBoard
        Get
            Return _StateShowDashboard
        End Get
        Set(ByVal value As StateDashBoard)
            _StateShowDashboard = value
        End Set
    End Property

    Public Sub New(ByVal dashboard As Dashboard, Optional AddNew As Boolean = False)
        InitializeComponent()
        'Icon = ResourceImageHelper.CreateIconFromResourcesEx("DashboardMainDemo.AppIcon.ico", GetType(frmMain).Assembly)
        dashboardDesigner.Dashboard = dashboard
        AddNewDashboard = AddNew
    End Sub

   

    Private _DashBoadhListName As DevExpress.XtraEditors.ComboBoxEdit
    Public Property DashBoadhListName As DevExpress.XtraEditors.ComboBoxEdit
        Get
            Return _DashBoadhListName
        End Get
        Set(value As DevExpress.XtraEditors.ComboBoxEdit)
            _DashBoadhListName = value
        End Set
    End Property

    Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
        MyBase.OnClosing(e)

        If dashboardDesigner.IsDashboardModified Then
            Dim result As DialogResult = XtraMessageBox.Show(LookAndFeel, Me, "Do you want to save changes ?", "Dashboard Designer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If result = System.Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            ElseIf result = System.Windows.Forms.DialogResult.Yes Then

                If StateShowDashboard = StateDashBoard.MainDashboard Then
                    Select Case saveDashboard_Idx
                        Case 0
                            With New wDashBoardAddName
                                .DashBoadhListName = Me.DashBoadhListName
                                .FTDashboardName.Text = Me.SaveDashboardName
                                .ShowDialog()

                                If .ProcOK Then
                                    saveDashboard_Name = .FTDashboardName.Text.Trim
                                    saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes
                                Else
                                    e.Cancel = True
                                End If

                            End With
                        Case Else
                            If AddNewDashboard Then
                                With New wDashBoardAddName
                                    .DashBoadhListName = Me.DashBoadhListName
                                    .FTDashboardName.Text = Me.SaveDashboardName
                                    .ShowDialog()

                                    If .ProcOK Then
                                        saveDashboard_Name = .FTDashboardName.Text.Trim
                                        saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes
                                    Else
                                        e.Cancel = True
                                    End If
                                End With
                            Else
                                'With New wDashBoardAddName
                                '    .DashBoadhListName = Me.DashBoadhListName
                                '    .FTDashboardName.Text = Me.SaveDashboardName
                                '    .ShowDialog()

                                '    If .ProcOK Then
                                '        saveDashboard_Name = .FTDashboardName.Text.Trim
                                '        saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes
                                '    Else
                                '        e.Cancel = True
                                '    End If
                                'End With

                                saveDashboard_Name = Me.SaveDashboardName
                                saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes

                            End If
                    End Select
                Else
                    saveDashboard_Name = Me.SaveDashboardName
                    saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes
                End If



                ' saveDashboard_Renamed = result = System.Windows.Forms.DialogResult.Yes
            End If
        End If

    End Sub

    Private Sub DashboardDesignerForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Comment out the following lines to enable the New, Open, Save and Save As buttons in the Ribbon, 
        'as well as the Backstage View that allows you to quickly reopen recent dashboards.

        fileNewBarItem1.Enabled = False
        fileOpenBarItem1.Enabled = False
        ribbonControl1.Toolbar.ItemLinks.Remove(fileSaveBarItem1)

    End Sub

End Class

