Imports System.Data.SqlClient

Public Class wCreateBomDevByBomOriginal

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
        ClearForm()
        Me.ProcComplete = False
        Me.Close()
    End Sub


    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If Verify() Then
            Try
                Dim _version As Integer = 1
                Dim _Qry As String = ""
                'b.FNHSysStyleDevId  , b.FTStyleDevCode, b.FTSeason , b.FNBomDevType ,
                _Qry = "SELECT TOP 1  ISNULL(MAX(b.FNVersion),0) + 1  "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS b WITH(NOLOCK) "
                _Qry &= vbCrLf & "WHERE b.FTStyleDevCode = '" & FTStyle.Text.Trim() & "' "
                _Qry &= vbCrLf & "AND b.FTSeason = '" & FTSeason.Text.Trim() & "' "
                _Qry &= vbCrLf & "AND b.FNBomDevType = '" & FNBomDevType_Hide.Text.Trim() & "'"

                _version = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")
                FNHSysStyleDevId_Target.Text = HI.SE.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)

                Dim cmdstring As String = ""
                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[Copy_BOMOriginal_to_BOMDev] "
                cmdstring &= vbCrLf & "@BomOriginal = '" & FNHSysStyleDevId_Hide.Text.Trim() & "' "
                cmdstring &= vbCrLf & ", @BomTarget = '" & FNHSysStyleDevId_Target.Text.Trim() & "' "
                cmdstring &= vbCrLf & ", @Version = '" & _version & "' "
                cmdstring &= vbCrLf & ", @User = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName).Trim() & "' "
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                Me.ProcComplete = True
                Me.Close()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Function Verify() As Boolean
        If (FNHSysStyleDevId.Text = "") Then
            FNHSysStyleDevId.Focus()
            HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูล Style ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ocmClear_Click(sender As Object, e As EventArgs) Handles ocmClear.Click
        ClearForm()
    End Sub

    Private Function ClearForm()
        ' BOM Source
        FNHSysStyleDevId.Text = ""
        FNHSysStyleDevId_None.Text = ""
        FNHSysStyleDevId_Hide.Text = ""
        FTStyle.Text = ""
        FTSeason.Text = ""
        FNBomDevType.Text = ""
        FNBomDevType_Hide.Text = ""
        FNVersion.Text = ""
    End Function

    Private Sub wCreateBomDevByBomOriginal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearForm()
    End Sub

    'Private Sub FNHSysStyleDevId_Hide_TextChanged(sender As Object, e As EventArgs) Handles FNHSysStyleDevId_Hide.TextChanged
    '    'Dim _Spls As New HI.TL.SplashScreen("Loading Data to BOM Sheet.... ,Please Wait.")
    '    'Try

    '    '    If FNHSysStyleDevId_Hide.Text <> "" Then

    '    '    End If

    '    '    _Spls.Close()

    '    'Catch ex As Exception
    '    '    _Spls.Close()
    '    'End Try
    'End Sub
End Class