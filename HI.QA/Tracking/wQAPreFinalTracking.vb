Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wQAPreFinalTracking
    Private _tmpPg As UIQAPreFinalTrackingList
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _tmpPg = New UIQAPreFinalTrackingList(Nothing)

        otpdata.Controls.Add(_tmpPg)
        _tmpPg.Dock = System.Windows.Forms.DockStyle.Fill
    End Sub


#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try
            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QAPreFinal_Tracking  " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            _tmpPg.EmpData = dt.Copy
            _tmpPg.RefreshData()
            Dim oSysLang As New ST.SysLanguage
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FTStartDate.Text <> "" And Me.FTEndDate.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1496730001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub pivotGridControl_CustomCellDisplayText(sender As Object, e As DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs)
        Try
            If (e.Value = 0) Then e.DisplayText = ""

        Catch ex As Exception

        End Try
    End Sub
End Class