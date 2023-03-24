Public Class KINGTEXDATA
    Private Sub KINGTEXDATA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        HI.Conn.DB.GetXmlConnectionString()

        Me.Timer1.Enabled = True
        Me.Timer2.Enabled = True
        Me.Timer3.Enabled = True
        Me.Timer5.Enabled = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Application.DoEvents()
        Dim Cmd As String = "SELECT TOP 1000  *  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType01 AS X WITH(NOLOCK) ORDER BY FDInsDate,FTInsTime"
        Dim Dt As DataTable = HI.Conn.SQLConn.GetDataTable(Cmd, HI.Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In Dt.Rows
            With New ControlMessageData
                .SpliltMessageDataType01(R!FTID.ToString, R!FDInsDate.ToString, R!FTInsTime.ToString, R!FTControlData.ToString, R!FTClientIP.ToString)
            End With
        Next

        Timer1.Enabled = True
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Enabled = False
        Application.DoEvents()
        Dim Cmd As String = "SELECT TOP 1000  *  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType02 AS X WITH(NOLOCK) ORDER BY FDInsDate,FTInsTime"
        Dim Dt As DataTable = HI.Conn.SQLConn.GetDataTable(Cmd, HI.Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In Dt.Rows
            With New ControlMessageData
                .SpliltMessageDataType02(R!FTID.ToString, R!FDInsDate.ToString, R!FTInsTime.ToString, R!FTControlData.ToString, R!FTClientIP.ToString)
            End With
        Next

        Timer2.Enabled = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Timer3.Enabled = False
        Application.DoEvents()
        Dim Cmd As String = "SELECT TOP 1000  *  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType03 AS X WITH(NOLOCK) ORDER BY FDInsDate,FTInsTime"
        Dim Dt As DataTable = HI.Conn.SQLConn.GetDataTable(Cmd, HI.Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In Dt.Rows
            With New ControlMessageData
                .SpliltMessageDataType03(R!FTID.ToString, R!FDInsDate.ToString, R!FTInsTime.ToString, R!FTControlData.ToString, R!FTClientIP.ToString)
            End With
        Next

        Timer3.Enabled = True
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        Timer5.Enabled = False
        Application.DoEvents()
        Dim Cmd As String = "SELECT TOP 1000  *  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMCHTMessageType05 AS X WITH(NOLOCK) ORDER BY FDInsDate,FTInsTime"
        Dim Dt As DataTable = HI.Conn.SQLConn.GetDataTable(Cmd, HI.Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In Dt.Rows
            With New ControlMessageData
                .SpliltMessageDataType05(R!FTID.ToString, R!FDInsDate.ToString, R!FTInsTime.ToString, R!FTControlData.ToString, R!FTClientIP.ToString)
            End With
        Next

        Timer5.Enabled = True
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick

    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles Timer7.Tick

    End Sub

    Private Sub Timer8_Tick(sender As Object, e As EventArgs) Handles Timer8.Tick

    End Sub

    Private Sub Timer9_Tick(sender As Object, e As EventArgs) Handles Timer9.Tick

    End Sub

    Private Sub Timer10_Tick(sender As Object, e As EventArgs) Handles Timer10.Tick

    End Sub
End Class