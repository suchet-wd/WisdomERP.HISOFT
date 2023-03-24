Imports System.IO
Imports System.Windows.Forms

Public Class wTerminalTime

#Region "Property"

    Private _CallMenuName As String = ""
    Private _CallServerParm As String = ""
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

    Public Property CallTimeSeverParm As String
        Get
            Return _CallServerParm
        End Get
        Set(value As String)
            _CallServerParm = value
        End Set
    End Property

#End Region

#Region "Procedure"


    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub


#End Region

#Region "General"
    Private Sub wChangePosition_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'textinput.Focus()
        'textinput.SelectAll()

        '_CallServerParm = "Y"
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub



    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs)
        Me.Close()
    End Sub



    Private _Count As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        Dim CurrentTime As String = ""
        Dim _Date As String = ""


        CurrentTime = TimeOfDay.ToString("HHmmss")
        _Date = Date.Today().ToString("yyyyMMdd")

        LabelControl1.Text = CurrentTime
        BarCodeControl1.Text = _Date & CurrentTime & Environment.MachineName


        CurrentTime = TimeOfDay.ToString("HH:mm:ss")

        LabelControl1.Text = CurrentTime

        _Count += +1
    End Sub


End Class