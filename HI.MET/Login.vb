Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports System.Windows.Forms
Imports DevExpress.XtraBars.Docking2010.Views

Public Class Login
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        flyout1.Properties.Style = DevExpress.XtraBars.Docking2010.Views.WindowsUI.FlyoutStyle.MessageBox

        addCustomerAction.Caption = " "

        flyout1.Action = addCustomerAction

        AddHandler windowsUIView1.FlyoutHidden, AddressOf windowsUIView1_FlyoutHidden
        AddHandler windowsUIView1.QueryControl, AddressOf windowsUIView_QueryControl
    End Sub

    Private addCustomerAction As FlyoutAction = New FlyoutAction()

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        HI.ST.UserInfo.UserName = ""
        windowsUIView1.Controller.Activate(flyout1)
    End Sub


    Private Sub windowsUIView_QueryControl(ByVal sender As Object, ByVal e As QueryControlEventArgs)
        e.Control = New ucUserLogin()
    End Sub

    Private Sub windowsUIView1_FlyoutHidden(sender As Object, e As FlyoutResultEventArgs)
        Me.Close()
    End Sub

End Class