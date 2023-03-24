Imports System.Data.SqlClient
Imports System.IO

Public Class wMerteamPermission

    Private _HSysID As Integer = 0
    Public Property HSysID() As Integer
        Get
            Return _HSysID
        End Get
        Set(value As Integer)
            _HSysID = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete() As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData() Then
            Dim _Str As String = ""
            Dim _pass As Boolean = True


            _Str = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMerAccCust WHERE FNHSysMerTeamId=" & Val(Me.HSysID) & " "
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    If Not (ogd.DataSource Is Nothing) Then
                        ogv.FocusedRowHandle = 0
                        ogv.FocusedColumn = ogv.Columns.ColumnByName("FTRoleName")

                        For Each R As DataRow In CType(ogd.DataSource, DataTable).Rows
                            If R!FTSelect.ToString = "1" Then
                        _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMerAccCust( FTInsUser, FDInsDate, FTInsTime, FNHSysMerTeamId, FNHSysCustId) "
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(Me.HSysID) & "' "
                        _Str &= vbCrLf & "," & Val(R!FNHSysCustId.ToString) & " "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                            End If
                        Next
                    End If



            ProcComplete = True
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.Close()
        End If
    End Sub

    Private Function VerifyData()
        Dim _Pass As Boolean = False
        If Me.FTUserName.Text.Trim <> "" Then
            If Me.FTUserDescriptionTH.Text.Trim <> "" Then
                If Me.FTUserDescriptionEN.Text.Trim() <> "" Then

                    _Pass = True

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTUserDescriptionEN_lbl.Text)
                    Me.FTUserDescriptionEN.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTUserDescriptionTH_lbl.Text)
                Me.FTUserDescriptionTH.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTUserName_lbl.Text)
            Me.FTUserName.Focus()
        End If

        Return _Pass
    End Function

    Private Sub wUser_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.FTStateActive.Checked = False

        Call LoadPermission()

    End Sub

    Private Sub LoadPermission()
        Dim _Str As String

        _Str = "SELECT CASE WHEN ISNULL(MX.FNHSysCustId,0) =0 THEN '0' ELSE '1' END  AS FTSelect "
        _Str &= vbCrLf & "  , R.FNHSysCustId, R.FTCustCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , R.FTCustNameTH AS FTCustName"
        Else
            _Str &= vbCrLf & " , R.FTCustNameEN As FTCustName "
        End If

        _Str &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS R"
        _Str &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMerAccCust AS X WITH(NOLOCK) WHERE FNHSysMerTeamId=" & Me.HSysID & " AND X.FNHSysCustId= R.FNHSysCustId) AS MX "
        _Str &= vbCrLf & " WHERE R.FTStateActive='1' "
        _Str &= vbCrLf & " ORDER BY R.FTCustCode "

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub

    Private Sub FTPassword_EditValueChanged(sender As Object, e As EventArgs)

    End Sub
End Class