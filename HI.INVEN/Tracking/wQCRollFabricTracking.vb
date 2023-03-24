Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid

Public Class wQCRollFabricTracking

    Dim oSysLang As New ST.SysLanguage
    Dim _UCrlOrg As New UQCFabricSummay("", "XX", "0", Nothing, True)
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ogborg.Controls.Add(_UCrlOrg)
        HI.TL.HandlerControl.AddHandlerObj(_UCrlOrg)


        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _UCrlOrg.Name.ToString.Trim, _UCrlOrg)
        Catch ex As Exception
        Finally
        End Try

        _UCrlOrg.Dock = System.Windows.Forms.DockStyle.Fill

        otb.TabPages.Clear()
    End Sub


#Region "Procedure"
    Private _ListData As New DataTable
    Private Sub LoadInfo()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & "    FTNameTH as FTName"
            Else
                _Cmd &= vbCrLf & "    FTNameEN as FTName"
            End If
            _Cmd &= vbCrLf & "   From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData with(nolock)"
            _Cmd &= vbCrLf & " where FTListName = N'FNQCFabricRollStatus' "
            _ListData = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SYSTEM)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dts As New DataSet
        Dim dt As New DataTable
        Dim dtsum As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Dim DateDate As String = Me.FTStartDate.Text & " - " & Me.FTEndDate.Text
        otb.TabPages.Clear()
        Try
            Call LoadInfo()
            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & ".dbo.SP_GetDataQCFabric 0,'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "','" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "','" & HI.UL.ULF.rpQuoted(FNHSysSuplIdTo.Text) & "' "
            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_INVEN, dts)

            dt = dts.Tables(0)

            Try
                dtsum = dts.Tables(1)
            Catch ex As Exception
                dtsum = dt.Clone()
            End Try


            If dt.Rows.Count > 0 Then

                Dim grp As List(Of String) = (dt.Select("FTItemRef<>''", "FTItemRef").CopyToDataTable).AsEnumerable() _
                                          .Select(Function(r) r.Field(Of String)("FTItemRef")) _
                                                      .Distinct() _
                                                      .ToList()

                For Each Str As String In grp


                    Dim grpsupl As List(Of String) = (dt.Select("FTItemRef='" & HI.UL.ULF.rpQuoted(Str) & "'", "FTSupplier").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTSupplier")) _
                                                      .Distinct() _
                                                      .ToList()

                    For Each Strsupl As String In grpsupl
                        Try
                            Dim dtdata As DataTable = dt.Select("FTItemRef = '" & HI.UL.ULF.rpQuoted(Str) & "' AND FTSupplier='" & HI.UL.ULF.rpQuoted(Strsupl) & "'").CopyToDataTable()

                            Dim dtdatasum As DataTable = dtsum.Select("FTItemRef = '" & HI.UL.ULF.rpQuoted(Str) & "' AND FTSupplier='" & HI.UL.ULF.rpQuoted(Strsupl) & "' ").CopyToDataTable()
                            dtdatasum.Columns.Add("FNActQuantity", GetType(Double))
                            dtdatasum.Columns.Add("FNTotalPoint", GetType(Double))

                            Dim TotalQty As Double = 0
                            Dim TotalPoint As Double = 0

                            Try
                                For Each R As DataRow In dtdatasum.Rows
                                    TotalQty = 0
                                    TotalPoint = 0

                                    For Each RX As DataRow In dtdata.Select("FTState='" & R!FTState.ToString & "'")
                                        TotalQty = TotalQty + Val(RX!FNActQuantity)
                                        TotalPoint = TotalPoint + Val(RX!FNTotalPoint)

                                    Next

                                    R!FNActQuantity = TotalQty
                                    R!FNTotalPoint = TotalPoint

                                Next

                                For Each R As DataRow In dtdata.Rows
                                    R!FTStateReject = _ListData.Rows(Val(R!FTStateReject)).Item(0).ToString()
                                Next

                            Catch ex As Exception
                            End Try

                            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                            With Otp
                                .Name = Strsupl & "_" & Str
                                .Text = Strsupl & " - " & Str
                            End With
                            otb.TabPages.Add(Otp)

                            Dim _UCrl As New UQCFabricSummay(DateDate, Strsupl, Str, dtdatasum)

                            Otp.Controls.Add(_UCrl)
                            _UCrl.Dock = System.Windows.Forms.DockStyle.Fill

                            Dim dtshow As New DataTable

                            If dtdata.Select("FTState='1'").Length > 0 Then
                                dtshow = dtdata.Select("FTState='1'").CopyToDataTable
                            Else
                                dtshow = dtdata.Clone()

                            End If

                            _UCrl.ogcdetail.DataSource = dtshow.Copy
                            _UCrl.pivotGridControl.Refresh()
                            ' _UCrl.chartControl.Refresh()
                            ' AddHandler _UCrl.ogvdetail.CustomDrawColumnHeader, AddressOf ogvdetail_CustomDrawColumnHeader

                            Call SetLang(_UCrlOrg.ogvdetail, _UCrl.ogvdetail, Str)
                            ' _UCrl.ogvdetail.InvalidateColumnHeader(Nothing)

                        Catch ex As Exception

                        End Try
                    Next

                Next
            End If


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

    Private Sub SetLang(Org As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, SetObj As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, MatId As String)
        For Each oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In Org.Bands

            Try
                Dim Str1 As String = oBand.Name.ToString.Split("_")(0)
                Dim Str2 As String = oBand.Name.ToString.Split("_")(1)

                With SetObj.Bands.Item(Str1.Replace("0", MatId.ToString) & "_" & Str2)
                    .Caption = oBand.Caption
                    .Tag = oBand.Tag
                End With

            Catch ex As Exception
            End Try

            If oBand.Collection.Count > 0 Then
                SP_SETxLanguageSubGridBand(oBand, SetObj, MatId)
            End If
        Next
        For Each R As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn In Org.Columns
            R.Caption = ""
        Next
    End Sub

    Private Sub SP_SETxLanguageSubGridBand(oMBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand, SetObj As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, MatId As String)

        For Each oBand As DevExpress.XtraGrid.Views.BandedGrid.GridBand In oMBand.Children

            Try
                Dim Str1 As String = oBand.Name.ToString.Split("_")(0)
                Dim Str2 As String = oBand.Name.ToString.Split("_")(1)

                With SetObj.Bands.Item(Str1.Replace("0", MatId.ToString) & "_" & Str2)
                    .Caption = oBand.Caption
                    .Tag = oBand.Tag
                End With

            Catch ex As Exception
            End Try

            If (oBand.HasChildren = True) Then
                SP_SETxLanguageSubGridBand(oBand, SetObj, MatId)
            End If

        Next

    End Sub

End Class