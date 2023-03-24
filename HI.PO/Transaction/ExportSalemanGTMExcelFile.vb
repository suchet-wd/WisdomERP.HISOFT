
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO


Public Class ExportSalemanGTMExcelFile

    Private Sub LoadListData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable



        _Qry = "SELECT  '1' AS FTSelect, FTSuplCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & " ,FTSuplNameEN AS FTSuplName"
        End If

        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As A WITH(NOLOCK)  "

        _Qry &= vbCrLf & " WHERE  (ISNULL(FTStateTrackPO, '0') = '1') "
        _Qry &= vbCrLf & " ORDER BY FTSuplCode "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

        Me.ogc.DataSource = _Dt.Copy
        _Dt.Dispose()
    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmexportrycexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click

        If Me.FNHSysBuyId.Text <> "" Then

            If Me.ogvdetail.RowCount <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1505140001, Me.Text)
            Else
                Try

                    Dim Op As New System.Windows.Forms.SaveFileDialog
                    Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                    Op.ShowDialog()

                    Try

                        If Op.FileName <> "" Then

                            With ogcdetail
                                .ExportToXlsx(Op.FileName)

                                Try
                                    Process.Start(Op.FileName)
                                Catch ex As Exception
                                End Try

                            End With

                        End If

                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                End Try

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()

        End If

    End Sub

    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadListData()
    End Sub
    Private Sub LoadDataInfo(Optional ExportExcel As Boolean = False)
        Me.ogcdetail.DataSource = Nothing
        Dim dt As New DataTable
        Dim _Qry As String

        Dim _Spls As New HI.TL.SplashScreen("Loading...")

        Dim _dtmattype As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกผู้ขาย !!!", 1617252478, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            _dtmattype = .Copy
        End With

        Dim _FNAllUser As String = ""

        For Each Rxm As DataRow In _dtmattype.Select("FTSelect='1'")

            If _FNAllUser = "" Then
                _FNAllUser = Rxm!FTSuplCode.ToString
            Else
                _FNAllUser = _FNAllUser & "," & Rxm!FTSuplCode.ToString
            End If

        Next

        Try

            _Qry = " Exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_EXPORT_GTM_TRACKING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Val(Me.FNHSysBuyId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(_FNAllUser) & "',13 "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.ogcdetail.DataSource = dt.Copy
            Me.ogvdetail.BestFitColumns()

            dt.Dispose()

        Catch ex As Exception
        End Try

        _Spls.Close()


    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If Me.FNHSysBuyId.Text <> "" Then
            Call LoadDataInfo()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()
        End If

    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try

            If Val("" & ogvdetail.GetRowCellValue(e.RowHandle, "FNNIKELT").ToString <> "") > 3 Then
                e.Appearance.BackColor = Drawing.Color.FromArgb(255, 192, 192)
                e.Appearance.ForeColor = Color.Blue
            End If

        Catch ex As Exception
        End Try

    End Sub
End Class