<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing


#Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.documentManager1 = New DevExpress.XtraBars.Docking2010.DocumentManager(Me.components)
        Me.windowsUIView1 = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.WindowsUIView(Me.components)
        Me.flyout1 = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Flyout(Me.components)
        Me.ucUserLoginDocument = New DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document(Me.components)
        CType(Me.documentManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.windowsUIView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flyout1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ucUserLoginDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'documentManager1
        '
        Me.documentManager1.ContainerControl = Me
        Me.documentManager1.ShowThumbnailsInTaskBar = DevExpress.Utils.DefaultBoolean.[False]
        Me.documentManager1.View = Me.windowsUIView1
        Me.documentManager1.ViewCollection.AddRange(New DevExpress.XtraBars.Docking2010.Views.BaseView() {Me.windowsUIView1})
        '
        'windowsUIView1
        '
        Me.windowsUIView1.ContentContainers.AddRange(New DevExpress.XtraBars.Docking2010.Views.WindowsUI.IContentContainer() {Me.flyout1})
        Me.windowsUIView1.Documents.AddRange(New DevExpress.XtraBars.Docking2010.Views.BaseDocument() {Me.ucUserLoginDocument})
        '
        'flyout1
        '
        Me.flyout1.Document = Me.ucUserLoginDocument
        Me.flyout1.Name = "flyout1"
        '
        'ucUserLoginDocument
        '
        Me.ucUserLoginDocument.Caption = "ucUserLogin"
        Me.ucUserLoginDocument.ControlName = "ucUserLogin"
        Me.ucUserLoginDocument.ControlTypeName = "HI.APP.ucUserLogin"
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1593, 708)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.IsMdiContainer = True
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "Login"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.documentManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.windowsUIView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flyout1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ucUserLoginDocument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private documentManager1 As DevExpress.XtraBars.Docking2010.DocumentManager
    Private windowsUIView1 As DevExpress.XtraBars.Docking2010.Views.WindowsUI.WindowsUIView
    Private flyout1 As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Flyout
    Friend WithEvents ucUserLoginDocument As DevExpress.XtraBars.Docking2010.Views.WindowsUI.Document
End Class
