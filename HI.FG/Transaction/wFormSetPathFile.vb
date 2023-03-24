Imports System.Windows.Forms

Public Class wFormSetPathFile

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub wFormSetPathFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FTPathName.Text = _DefualPath
        Me.FTPathNameTo.Text = _DefualPathTo
    End Sub
#Region "Property"
    Private _DefualPath As String = ""
    Public Property DefalPath As String
        Get
            Return _DefualPath
        End Get
        Set(value As String)
            _DefualPath = value
        End Set
    End Property
    Private _DefualPathTo As String = ""
    Public Property DefalPathTo As String
        Get
            Return _DefualPathTo
        End Get
        Set(value As String)
            _DefualPathTo = value
        End Set
    End Property

    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property
#End Region

    Private Sub obtcancel_Click(sender As Object, e As EventArgs) Handles obtcancel.Click
        Try
            _State = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub obtnext_Click(sender As Object, e As EventArgs) Handles obtnext.Click
        Try
            _State = True
            _DefualPath = Me.FTPathName.Text

            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Function SetPathFoder() As String
        Try
            Dim folderDlg As New FolderBrowserDialog
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                Return folderDlg.SelectedPath
            Else
                Return ""
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Sub FTPathName_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTPathName.ButtonClick
        Try
            Me.FTPathName.Text = SetPathFoder()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTPathNameTo_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTPathNameTo.ButtonClick
        Try
            Me.FTPathNameTo.Text = SetPathFoder()
        Catch ex As Exception
        End Try
    End Sub
End Class