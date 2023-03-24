
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wBIPurchaseForcastEvaluate

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.




    End Sub

#Region "Procedure"


    Private Function Verifydata() As Boolean

        Dim _Pass As Boolean = False



        If FTSDate.Text <> "" Or FTEDate.Text <> "" Then
            If HI.UL.ULDate.ConvertEnDB(FTSDate.Text) <> "" And HI.UL.ULDate.ConvertEnDB(FTEDate.Text) <> "" Then
                _Pass = True
            End If
        End If
        Return _Pass

    End Function



    Private Sub Loaddata()

        Dim _Qry As String = ""

        Dim _FNAllMattype As String = ""
        Dim _FTOrderNo As String = ""
        Dim _Lang As String = "TH"

        If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
            _Lang = "EN"
        End If

        Dim Spls As New HI.TL.SplashScreen("Loading...,Please Wait.")

        Try

            Dim _dt As DataTable
            Dim username As String = ""
            'username = "mlpsirikanya"
            username = HI.ST.UserInfo.UserName


            Dim StrSDate As String = HI.UL.ULDate.ConvertEnDB(FTSDate.Text)
            Dim StrEDate As String = HI.UL.ULDate.ConvertEnDB(FTEDate.Text)

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_FORCASTEVU '" & HI.UL.ULF.rpQuoted(username) & "','" & StrSDate & "','" & StrEDate & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.pivotGridControl.DataSource = _dt.Copy

            _dt.Dispose()

        Catch ex As Exception
        End Try

        Spls.Close()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click

        Me.Close()

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If Me.Verifydata() Then
            Me.Loaddata()
        End If

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub wBIPurchaseEvaluate_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

End Class