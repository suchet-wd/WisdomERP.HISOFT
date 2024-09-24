
Public Class wCreateBomDev

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If Verify() Then
            Me.ProcComplete = True
            Me.Close()
        End If

    End Sub

    Private Function Verify() As Boolean
        If (FTStyle.Text = "") Then
            FTStyle.Focus()
            HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูล Style ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            If (FTStyleDetail.Text = "") Then
                FTStyleDetail.Focus()
                HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูลรายละเอียด Style ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            Else
                If (FTSeason.Text = "") Then
                    FTSeason.Focus()
                    HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                Else
                    If (FNBomDevType.Text = "") Then
                        FNBomDevType.Focus()
                        HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                        Return False
                    Else
                        'If (FTVersion.Text = "") Then
                        '    HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                        '    Return False
                        'Else
                        Return True
                        'End If
                    End If
                End If
            End If
        End If
    End Function

    Private Sub ocmClear_Click(sender As Object, e As EventArgs) Handles ocmClear.Click
        ClearForm()
    End Sub

    Private Sub wCreateBomDev_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearForm()
    End Sub
    Private Function ClearForm()
        FTSeason.Text = ""
        FTStyle.Text = ""
        FTStyleDetail.Text = ""
        FNBomDevType.Text = ""
        FTProgram.Text = ""
        FTProductDev.Text = ""
        FTDimension.Text = ""
        FTStyleSeniorDev.Text = ""
    End Function

    Private Sub FTStyle_Leave(sender As Object, e As EventArgs) Handles FTStyle.Leave
        Dim _dt As DataTable
        Dim _Qry As String = ""
        If FTStyle.Text <> "" Then
            _Qry = "SELECT ISNULL(s.FTStyleNameEN,'') AS 'FTStyleDetail', s.FTProductdeveloper "
            _Qry &= vbCrLf & ", s.FTSeniordeveloper, s.FTDimension, s.FTProgram "
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS s WITH (NOLOCK) "
            _Qry &= vbCrLf & "WHERE s.FTStyleCode = '" & HI.UL.ULF.rpQuoted(FTStyle.Text) & "' "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            'FTStyleDetail.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")
            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows
                    FTStyleDetail.Text = R!FTStyleDetail.ToString
                    FTProgram.Text = R!FTProgram.ToString
                    FTProductDev.Text = R!FTProductdeveloper.ToString
                    FTDimension.Text = R!FTDimension.ToString
                    FTStyleSeniorDev.Text = R!FTSeniordeveloper.ToString
                Next
            End If
        End If
    End Sub
End Class