
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO


Public Class wExportExcelFilePOImport

    Private Sub LoadListData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable


        _Qry = " SELECT MAX(AA.FTSelect) AS FTSelect,AA.FTName,MAX(AA.FTUserDescriptionEN) AS FTUserDescriptionEN "
        _Qry &= vbCrLf & " FROM ( SELECT  CASE WHEN  Uall.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' THEN '1' ELSE '0' END  AS FTSelect"
        _Qry &= vbCrLf & ", Uall.FTUserName AS FTName, Uall.FTUserDescriptionEN "
        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp As TGrp  WITH(NOLOCK)  On A.FNHSysTeamGrpId = TGrp.FNHSysTeamGrpId INNER Join "
        _Qry &= vbCrLf & "  TSEUserLogin As Uall On TGrp.FNHSysTeamGrpId = Uall.FNHSysTeamGrpId "
        _Qry &= vbCrLf & " Where (A.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')  AND TGrp.FTStatePurchase='1' "

        _Qry &= vbCrLf & " UNION SELECT  CASE WHEN  Uall.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' THEN '1' ELSE '0' END  AS FTSelect"
        _Qry &= vbCrLf & ", Uall.FTUserName AS FTName, Uall.FTUserDescriptionEN "
        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp As TGrp  WITH(NOLOCK)  On A.FNHSysTeamGrpIdTo = TGrp.FNHSysTeamGrpId INNER Join "
        _Qry &= vbCrLf & "  TSEUserLogin As Uall On TGrp.FNHSysTeamGrpId = Uall.FNHSysTeamGrpId "
        _Qry &= vbCrLf & " Where (A.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')   AND TGrp.FTStatePurchase='1' "

        _Qry &= vbCrLf & " ) AS AA "
        _Qry &= vbCrLf & " GROUP BY AA.FTName "
        _Qry &= vbCrLf & " Order By AA.FTName "

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
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกประเภทวัตถุดิบ !!!", 1613052478, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            _dtmattype = .Copy
        End With

        Dim _FNAllUser As String = ""

        For Each Rxm As DataRow In _dtmattype.Select("FTSelect='1'")

            If _FNAllUser = "" Then
                _FNAllUser = Rxm!FTName.ToString
            Else
                _FNAllUser = _FNAllUser & "','" & Rxm!FTName.ToString
            End If

        Next

        Try

            _Qry = " Exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_GETDATA_PO_IMPORT " & Val(Me.FNHSysBuyId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(_FNAllUser) & "'," & FNPoState.SelectedIndex & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "'," & Val(FNHSysSuplId.Properties.Tag.ToString) & "," & Val(FNHSysCustId.Properties.Tag.ToString) & " "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.ogcdetail.DataSource = dt.Copy
            Me.ogvdetail.BestFitColumns()

            dt.Dispose()

        Catch ex As Exception
        End Try

        _Spls.Close()

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If (Me.FNHSysBuyId.Text <> "") OrElse (FTEndPurchaseDate.Text <> "" And FTStartPurchaseDate.Text <> "") Then
            Call LoadDataInfo()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()
        End If

    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged

    End Sub
End Class