Public Class wDCPrepareCFMToCarAppWH

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub


    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Sub wReserveItemPopup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click


        Try
            With CType(Me.ogclistdetail.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FNHSysWHIdTo_Hide=0").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาระบุคลังปลายทาง !!!", 112444875, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)

                    Exit Sub
                End If


                For Each R As DataRow In .Rows

                    Dim CmpId As Integer = Val(R!FNHSysCmpId.ToString)
                    Dim WHCode As String = R!FNHSysWHIdTo.ToString

                    Dim CmpWHId As Integer = 0

                    Dim cmdstring As String = "select top 1 FNHSysCmpId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X with(nolock) where FTWHCode='" & HI.UL.ULF.rpQuoted(WHCode) & "'"

                    CmpWHId = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "0"))

                    If CmpWHId = 0 Then
                        HI.MG.ShowMsg.mInfo("ข้อมูลคลังปลายทางไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 112444878, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If CmpWHId <> CmpId Then
                        HI.MG.ShowMsg.mInfo("ข้อมูลคลังปลายทางไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 112444879, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                Next

                Me.ProcessProc = True
                Me.Close()

            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

End Class