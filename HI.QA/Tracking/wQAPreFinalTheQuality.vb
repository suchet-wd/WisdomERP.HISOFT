Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls

Public Class wQAPreFinalTheQuality

    Private _tmpPg As UIQAPreFinalTracking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.otb.TabPages.Clear()

        Dim Otp As New DevExpress.XtraTab.XtraTabPage()
        With Otp
            .Name = "T001"
            .Text = ""
            .Tag = "2|"
        End With

        _tmpPg = New UIQAPreFinalTracking(0, Nothing, "")

        Otp.Controls.Add(_tmpPg)
        _tmpPg.Dock = System.Windows.Forms.DockStyle.Fill
        otb.TabPages.Add(Otp)
    End Sub


#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Me.otb.TabPages.Clear()

        Try
            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QAPreFinal_Tracking_Wage  " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULDate.ConvertEnDB("01/" & Me.FTMonth.Text) & "','" & HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth("01/" & Me.FTMonth.Text, 1), -1)) & "' "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Dim _dt2 As DataTable = dt.Copy

            Dim grp As List(Of Integer) = dt.AsEnumerable() _
                                     .Select(Function(r) r.Field(Of Integer)("FNHSysEmpID")) _
                                     .Distinct() _
                                     .ToList()

            For Each Ind As Integer In grp
                Dim _PageName As String = ""

                For Each R As DataRow In _dt2.Select("FNHSysEmpID=" & Ind & "")
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _PageName = R!FTEmpCode.ToString & "(" & R!FTEmpNameTH.ToString & ")"
                    Else
                        _PageName = R!FTEmpCode.ToString & "(" & R!FTEmpNameTH.ToString & ")"
                    End If
                Next

                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                With Otp
                    .Name = "T" & Ind.ToString
                    .Text = _PageName
                    .Tag = "2|"
                End With
                Dim oPg As New UIQAPreFinalTracking(Ind, _dt2.Select("FNHSysEmpID=" & Ind & "").CopyToDataTable, Me.FTMonth.Text)
             
                Otp.Controls.Add(oPg)
                oPg.Dock = System.Windows.Forms.DockStyle.Fill
                otb.TabPages.Add(Otp)
                oPg.ogvdetail.OptionsView.ShowAutoFilterRow = True
                Call SetLang(_tmpPg.ogvdetail, oPg.ogvdetail, Ind)

                'oPg.DataSource = _dt2.Select("FNHSysEmpID=" & Ind & "").CopyToDataTable
            Next

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
        If Me.FTMonth.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1496738101, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
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

    Private Sub SetLang(Org As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, SetObj As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, tFNHsysEmpID As Integer)
        For Each oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In Org.Bands

            Try
                Dim Str1 As String = oBand.Name.ToString.Split("_")(0)
                Dim Str2 As String = oBand.Name.ToString.Split("_")(1)

                With SetObj.Bands.Item(Str1.Replace("0", tFNHsysEmpID.ToString) & "_" & Str2)
                    .Caption = oBand.Caption
                    .Tag = oBand.Tag
                End With

            Catch ex As Exception
            End Try

            If oBand.Collection.Count > 0 Then
                SP_SETxLanguageSubGridBand(oBand, SetObj, tFNHsysEmpID)
            End If
        Next

    End Sub

    Private Sub SP_SETxLanguageSubGridBand(oMBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand, SetObj As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, tFNHsysEmpID As Integer)

        For Each oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In oMBand.Children

            Try
                Dim Str1 As String = oBand.Name.ToString.Split("_")(0)
                Dim Str2 As String = oBand.Name.ToString.Split("_")(1)

                With SetObj.Bands.Item(Str1.Replace("0", tFNHsysEmpID.ToString) & "_" & Str2)
                    .Caption = oBand.Caption
                    .Tag = oBand.Tag
                End With

            Catch ex As Exception
            End Try

            If (oBand.HasChildren = True) Then
                SP_SETxLanguageSubGridBand(oBand, SetObj, tFNHsysEmpID)
            End If

        Next

    End Sub
End Class