Imports System.IO
Imports DevExpress.Pdf
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports System.Text
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Export

Public Class wImportExcelMainMat

    Private WBOMFinish As wImportExcelMainMatListFinish
    Private PathFileExcel As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Dim oSysLang As New HI.ST.SysLanguage






        WBOMFinish = New wImportExcelMainMatListFinish
        HI.TL.HandlerControl.AddHandlerObj(WBOMFinish)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, WBOMFinish.Name.ToString.Trim, WBOMFinish)
        Catch ex As Exception
        End Try


    End Sub


    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx, *.xlsm)|*.xls;*.xlsx;*.xlsm"
                    opFileDialog.ShowDialog()

                    Try

                        If opFileDialog.FileName <> "" Then
                            Dim _FileName As String = opFileDialog.FileName
                            Me.FTFilePath.Text = _FileName

                            Dim Spls As New HI.TL.SplashScreen("Loading...")

                            Select Case Path.GetExtension(_FileName)
                                Case ".xls"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

                                Case ".xlsx"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                Case ".xlsm"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

                                Case Else
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                            End Select

                            Dim usedRange As DevExpress.Spreadsheet.CellRange = opshet.ActiveWorksheet.GetUsedRange()
                            usedRange.NumberFormat = "@"

                            Spls.Close()
                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)


    End Sub


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub


    Private Function CheckWriteFile() As Boolean

        Try

            If (Not System.IO.Directory.Exists(PathFileExcel)) Then
                System.IO.Directory.CreateDirectory(PathFileExcel)
            End If

            If (Not System.IO.Directory.Exists(PathFileExcel & "\TestExcel")) Then
                System.IO.Directory.CreateDirectory(PathFileExcel & "\TestExcel")
            End If
            System.IO.Directory.Delete(PathFileExcel & "\TestExcel")


            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Sub ocmimportbimpdf_Click(sender As Object, e As EventArgs) Handles ocmimportbimpdf.Click

        Dim StateImport As Boolean = False
        Dim msgshow As String = ""
        Dim cmdstring As String = ""


        If FTFilePath.Text <> "" Then



            PathFileExcel = HI.Conn.SQLConn.GetField("SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportMainmatExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

            If CheckWriteFile() = False Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)
            End If


            Dim Splsx As New HI.TL.SplashScreen("Reading Data From Excel....")
            Try
                Dim mdt As DataTable

                For CIdx As Integer = 0 To opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1
                    Try
                        'If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
                        'Else
                        '    If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                        '        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                        '    End If
                        'End If

                        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

                        Else
                            If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                                opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                            End If
                        End If

                    Catch ex As Exception

                    End Try


                Next


                Dim FileName As String = PathFileExcel & "\Mat" & HI.ST.UserInfo.UserName & ".xlsx"
                opshet.SaveDocument(PathFileExcel & "\Mat" & HI.ST.UserInfo.UserName & ".xlsx")

                Dim dts As New DataSet

                Splsx.UpdateInformation("Importing Data From Excel....")
                cmdstring = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.USP_IMPORTFILEEXCELMAINMAT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "'"


                HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_MASTER, dts)

                StateImport = False '(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MASTER, "") = "1")

                If dts.Tables.Count > 1 Then

                    StateImport = (dts.Tables(1).Rows(0)!FTStetInsert.ToString = "1")
                    msgshow = dts.Tables(1).Rows(0)!FTMessage.ToString

                Else

                    Try

                        StateImport = (dts.Tables(0).Rows(0)!FTStetInsert.ToString = "1")
                        msgshow = dts.Tables(0).Rows(0)!FTMessage.ToString

                    Catch ex As Exception
                        msgshow = "ข้อมูล Format File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                    End Try

                End If


                Try
                    System.IO.File.Delete(FileName)
                Catch ex As Exception
                End Try

                If StateImport Then

                    cmdstring = " SELECT    Seq, CASE WHEN FTStateImport ='1' THEN MATERIALCODE ELSE '' END As MATERIALCODE, MATERIALNAME_EN, MATERIALNAME_TH, MAINMATTYPE, MATGROUP, MATTYPE, FabricWidth, Customer, CustomerRefCode, Supplier, Unit, "
                    cmdstring &= vbCrLf & "     Currency, FTStateImport, FNMerMatType, FNHSysMatGrpId, FNHSysMatTypeId, FNHSysCustId, FNHSysSuplId, FNHSysUnitId, FNHSysCurId "
                    cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TTMPImportMainMatExcel As A WITH(NOLOCK) "
                    cmdstring &= vbCrLf & "   WHERE     (FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
                    cmdstring &= vbCrLf & "   ORDER BY Seq "
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

                    Splsx.Close()

                    With WBOMFinish
                        .ogclist.DataSource = mdt.Copy
                        .ocmexit.Enabled = True
                        .ShowDialog()
                    End With
                    mdt.Dispose()

                Else
                    Splsx.Close()
                    msgshow = "ข้อมูล Format File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                End If
                'Else
                '    Splsx.Close()
                '    msgshow = "ข้อมูล File Excel ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!"
                'End If

                ' End If

            Catch ex2 As Exception
                Splsx.Close()
            End Try



            If StateImport = False Then
                MessageBox.Show(msgshow)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, ogbselectfile.Text)
        End If

    End Sub

    Private Sub exporter_CellValueConversionError(ByVal sender As Object, ByVal e As CellValueConversionErrorEventArgs)
        MessageBox.Show("Error In cell " & e.Cell.GetReferenceA1())
        e.DataTableValue = Nothing
        e.Action = DataTableExporterAction.Continue
    End Sub



    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class