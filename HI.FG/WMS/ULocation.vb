Public Class ULocation

    Property LocationNo As String
        Get
            Return olbloc.Text
        End Get
        Set(value As String)
            olbloc.Text = value
        End Set
    End Property

    Property DataInfo As String
        Get
            Return olbdesc.Text
        End Get
        Set(value As String)
            olbdesc.Text = value
        End Set
    End Property


    Private SystemId As Integer
    Property LocationSystemId As Integer
        Get
            Return SystemId
        End Get
        Set(value As Integer)
            SystemId = value
        End Set
    End Property

    Private StateDataFill As Boolean
    Property DataFill As Boolean
        Get
            Return StateDataFill
        End Get
        Set(value As Boolean)
            StateDataFill = value

            If StateDataFill Then
                opn.BackColor = System.Drawing.Color.FromArgb(255, 128, 128)
            Else
                opn.BackColor = System.Drawing.Color.FromArgb(0, 192, 0)
            End If

        End Set

    End Property



End Class
