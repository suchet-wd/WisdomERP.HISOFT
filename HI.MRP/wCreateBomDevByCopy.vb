
Public Class wCreateBomDevByCopy

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
            Try
                Dim _version As Integer = 1
                Dim _Qry As String = ""
                'b.FNHSysStyleDevId  , b.FTStyleDevCode, b.FTSeason , b.FNBomDevType ,
                _Qry = "SELECT TOP 1 ISNULL(MAX(b.FNVersion),0) + 1  "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS b WITH(NOLOCK) "
                _Qry &= vbCrLf & "WHERE b.FTStyleDevCode = '" & FTStyleCode.Text.Trim() & "' "
                _Qry &= vbCrLf & "AND b.FTSeason = '" & FNHSysSeasonId.Text.Trim() & "' "
                _Qry &= vbCrLf & "AND b.FNBomDevType = " & Val(FNBomDevTypeTarget.SelectedIndex.ToString) & " "

                _version = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")
                FNHSysStyleDevId_Target.Text = HI.SE.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)

                Dim cmdstring As String = ""
                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[Copy_BOMDev_to_BOMDev] "
                cmdstring &= vbCrLf & "@BomSource = '" & FNHSysStyleDevId_Hide.Text.Trim() & "' "
                cmdstring &= vbCrLf & ", @BomTarget = '" & FNHSysStyleDevId_Target.Text.Trim() & "' "
                cmdstring &= vbCrLf & ", @VersionTarget = '" & _version & "' "
                cmdstring &= vbCrLf & ", @FTStyleCodeTarget = '" & FTStyleCode.Text.Trim() & "' "
                cmdstring &= vbCrLf & ", @FTSeasonTarget = '" & FNHSysSeasonId.Text.Trim() & "' "
                cmdstring &= vbCrLf & ", @FNBomDevTypeTarget = '" & Val(FNBomDevTypeTarget.SelectedIndex.ToString) & "' "
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
            HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูล Style ต้นฉบับ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            If (FTStyleCode.Text = "") Then
                FTStyleCode.Focus()
                HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูลรายละเอียด Style (Target) ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            Else
                If (FNHSysSeasonId.Text = "") Then
                    FNHSysSeasonId.Focus()
                    HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type (Target) ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                Else
                    If (FNBomDevTypeTarget.Text = "") Then
                        FNBomDevTypeTarget.Focus()
                        HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type (Target) ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
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
        ''Source
        FNHSysStyleDevId_Hide.Text = ""
        FNHSysStyleDevId.Text = ""
        FNHSysStyleDevId_None.Text = ""
        FTSeason.Text = ""
        FNVersion.Text = ""
        BomDevType.Text = ""

        '' Target
        'FNHSysStyleId.Text = ""
        FTStyleCode.Text = ""
        FNHSysSeasonId.Text = ""
        FTStyleDetailTarget.Text = ""
        FNBomDevTypeTarget.Text = ""
        FTProgram.Text = ""
        FTProductDev.Text = ""
        FTDimension.Text = ""
        FTStyleSeniorDev.Text = ""
    End Function

    'Private Sub FTStyle_Leave(sender As Object, e As EventArgs) Handles FTStyleTarget.Leave
    '    Dim _dt As DataTable
    '    Dim _Qry As String = ""
    '    If FTStyleTarget.Text <> "" Then
    '        _Qry = "SELECT ISNULL(s.FTStyleNameEN,'') AS 'FTStyleDetail', s.FTProductdeveloper "
    '        _Qry &= vbCrLf & ", s.FTSeniordeveloper, s.FTDimension, s.FTProgram "
    '        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS s WITH (NOLOCK) "
    '        _Qry &= vbCrLf & "WHERE s.FTStyleCode = '" & HI.UL.ULF.rpQuoted(FTStyleTarget.Text) & "' "
    '        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
    '        'FTStyleDetail.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")
    '        If _dt.Rows.Count > 0 Then
    '            For Each R As DataRow In _dt.Rows
    '                FTStyleDetailTarget.Text = R!FTStyleDetail.ToString
    '                FTProgram.Text = R!FTProgram.ToString
    '                FTProductDev.Text = R!FTProductdeveloper.ToString
    '                FTDimension.Text = R!FTDimension.ToString
    '                FTStyleSeniorDev.Text = R!FTSeniordeveloper.ToString
    '            Next
    '        End If
    '    End If
    'End Sub

    Private Sub FNHSysStyleDevId_Hide_TextChanged(sender As Object, e As EventArgs) Handles FNHSysStyleDevId_Hide.TextChanged
        Dim _Spls As New HI.TL.SplashScreen("Loading Data to BOM Sheet.... ,Please Wait.")

        Try

            If FNHSysStyleDevId_Hide.Text <> "" Then

                'Call LoadStyleInfo(FNHSysStyleDevId_Hide.Text, True)

            Else
                ClearForm()

            End If
            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

End Class