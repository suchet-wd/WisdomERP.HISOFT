Public Class wPopUpSyncDataManual
    Private _Pass As Boolean
    Public Property Pass As Boolean
        Get
            Return _Pass
        End Get
        Set(value As Boolean)
            _Pass = value
        End Set
    End Property
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub


    Private Sub wPopUpSyncDataManual_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Function GetTableNameByDBName(DBName As HI.Conn.DB.DataBaseName) As DataTable
        Try
            Dim _Cmd As String = ""
            _Cmd = " Select '0' AS FTState , name AS FTTableName From sys.tables   where lob_data_space_id = 0"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, DBName)
        Catch ex As Exception

        End Try
    End Function

    Private Sub FNDBName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNDBName.SelectedIndexChanged
        Try
            Dim _Db As Integer = HI.TL.CboList.GetListValue(FNDBName.Properties.Tag, FNDBName.SelectedIndex)
            Me.ogcdetail.DataSource = GetTableNameByDBName(_Db)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        _Pass = False
        Me.Close()
    End Sub

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click
        Try
            _Pass = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class