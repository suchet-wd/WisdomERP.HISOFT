Imports System.Windows.Forms

Public Class wFormSet_FileName

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub wFormSetPathFile_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FTPathName.Text = _DefualPath 
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
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg 
                .Filter = "File Database Backup" & "|*.bak"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Return .FileName
                Else
                    Return ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Function

    Private Sub FTPathName_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTPathName.ButtonClick
        Try
            Me.FTPathName.Text = SetPathFoder()
        Catch ex As Exception
        End Try
    End Sub

     
End Class