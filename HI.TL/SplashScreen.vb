Imports System.Windows.Forms

Public Class SplashScreen
    Inherits System.Windows.Forms.Form

    Private m_caption As String = ""
    Private title As String = ""

    Sub New()
        Me.New("Loading. Please Wait...")
    End Sub
   

    Sub New(caption As String)
        Me.New(caption, "")
    End Sub

    Sub New(caption As String, title As String)
        Me.New(caption, "", False)
    End Sub

    Sub New(caption As String, title As String, showtimer As Boolean)
        InitializeComponent()

        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = My.Resources.Wisdom_logo_Hires
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch

        Me.ClientSize = Size 'New System.Drawing.Size(267, 225)
        Me.Name = "SplashScreen"
        Me.FormBorderStyle = FormBorderStyle.None
        Me.ControlBox = False
        ' Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen

        'If Parent Is Nothing Then
        '    Me.StartPosition = FormStartPosition.CenterScreen
        'Else


        ' Get Active Screen Cursor is On, rather than assuming user on PrimaryScreen

        Me.StartPosition = FormStartPosition.Manual
        'Left = Parent.Left + (Parent.Width - Width) / 2
        'Top = Parent.Top + (Parent.Height - Height) / 2
        'End If
        Dim scr As Screen = Screen.FromPoint(Cursor.Position)
        Me.Location = New System.Drawing.Point(scr.WorkingArea.Right - Me.Width, scr.WorkingArea.Bottom - Me.Height)


        opic.Image = My.Resources.Wait

        Me.olbtitle.Text = title
        Me.olbcaption.Text = caption

        Me.FTTimeStart.Text = Format(Now, "HH:mm:ss")
        Me.FTTime.Text = ""

        'Dim allScreens As Screen() = Screen.AllScreens
        'Dim currentScreen As Screen = Screen.FromControl(Me)
        'Dim currentIndex As Integer = Array.IndexOf(allScreens, currentScreen)

        Me.TopMost = True
        Me.Show()
        Me.Refresh()

    End Sub

    Public Function GetCaption() As String
        Return Caption
    End Function

    Public Sub SetCaption(newCaption As String)
        Caption = newCaption
    End Sub

    Public Property Caption() As String
        Get
            Return Me.olbcaption.Text
        End Get
        Set(value As String)
            Me.olbcaption.Text = value
            Me.Refresh()
        End Set
    End Property

    Public Sub UpdateTitle(Titleinfo As String)
        Me.olbtitle.Text = Titleinfo
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf Refresh))
        Else
            Me.Refresh()
        End If
    End Sub

    Public Sub UpdateInformation(info As String)
        Me.olbcaption.Text = info
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf Refresh))
        Else
            Me.Refresh()
        End If
    End Sub

    Public Sub Updatetime(datatime As String)
        Me.FTTime.Text = datatime
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf Refresh))
        Else
            Me.Refresh()
        End If
    End Sub

    Public Sub StyleProgess(Optional Marquee As Boolean = True, Optional MinPgr As Integer = 0, Optional MaxPgr As Integer = 100)

        Me.pgr1.Visible = Marquee
        Me.pgr2.Visible = Not (Marquee)
        Me.pgr2.Properties.Minimum = MinPgr
        Me.pgr2.Properties.Maximum = MaxPgr

        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf Refresh))
        Else
            Me.Refresh()
        End If
    End Sub

    Public Sub UpdateProgress(val As Double)

        With Me.pgr2
            If val > .Properties.Maximum Then
                .Position = .Properties.Maximum
            Else
                .Position = val
            End If
        End With

        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf Refresh))
        Else
            Me.Refresh()
        End If
    End Sub

    Protected Overrides Sub OnClosing(e As System.ComponentModel.CancelEventArgs)
        Try
            MyBase.OnClosing(e)
        Catch ex As Exception
        End Try
    End Sub

End Class
