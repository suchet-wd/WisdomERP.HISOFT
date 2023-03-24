Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports DevExpress.XtraBars.Docking2010

Imports System.Drawing
Public Class uCQA
    Private _x As Integer = 0
    Private _y As Integer = 0
    
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetButton()
    End Sub

    Sub New(x As Integer, y As Integer, id As Integer, name As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call..
        _x = x
        _y = y


        SetButton()
        LoadData(id)
        LabelControl1.Text = name.ToString

    End Sub

    Private Sub SetButton()
        Try
            obtBack.BackColor = Drawing.Color.Transparent
        Catch ex As Exception

        End Try
    End Sub


    Private Sub uCQA_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        'If e.Button = System.Windows.Forms.MouseButtons.Right Then
        '    RadialMenu.ShowPopup(New Point(650, 300)) '_x - (e.X + 100)_y - (e.Y + 100))

        'End If
    End Sub

    Private Sub LoadData(ByVal code As Integer)
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable

            _Qry = "SELECT     FNHSysQADetailId, FNHSysQATypeId, FTQADetailCode, FTStateActive"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", FTQADetailNameTH as FTQADetailName"
            Else
                _Qry &= vbCrLf & ", FTQADetailNameEN as FTQADetailName"
            End If
            _Qry &= vbCrLf & "FROM         TQAMQADetail WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysQATypeId=" & CInt(code)
            _Qry &= vbCrLf & " and FTStateActive = '1'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            ogcDetailMater.DataSource = _oDt
        Catch ex As Exception

        End Try
    End Sub
End Class
