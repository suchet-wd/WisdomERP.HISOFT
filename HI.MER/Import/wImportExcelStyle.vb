Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO
Imports System.Data.SqlClient

Public Class wImportExcelStyle


    Private stylepopup As wImportExcelStylePopup

    Private _CustItemRef As String = ""
    Private pPathSharedStyle As String = ""
    Private Property CustItemRef As String
        Get
            Return _CustItemRef
        End Get
        Set(value As String)
            _CustItemRef = value
        End Set
    End Property

    Private mdt As System.Data.DataTable

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        stylepopup = New wImportExcelStylePopup

        HI.TL.HandlerControl.AddHandlerObj(stylepopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, stylepopup.Name.ToString.Trim, stylepopup)
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Property DataExcel As System.Data.DataTable
        Get
            Return mdt
        End Get
        Set(value As System.Data.DataTable)
            mdt = value
        End Set
    End Property

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim cmd As String = ""

        cmd = "select top 1 FTCfgData from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS M WITH(NOLOCK) WHERE     (FTCfgName = N'PathImageStyle') "
        pPathSharedStyle = HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SECURITY, "")

    End Sub

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
                    opFileDialog.ShowDialog()

                    Try
                        If opFileDialog.FileName <> "" Then

                            Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")

                            Try

                                Dim _FileName As String = opFileDialog.FileName

                                FTFilePath.Text = _FileName

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

                            Catch ex As Exception
                            End Try

                            GetSpreedSheet()
                            _Pls.Close()

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
    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub

    Private Sub FNRceceiveType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTMapStyleCode.SelectedIndexChanged

    End Sub

    Private Sub opshet_ActiveSheetChanged(sender As Object, e As DevExpress.Spreadsheet.ActiveSheetChangedEventArgs) Handles opshet.ActiveSheetChanged

        GetSpreedSheet()
    End Sub

    Private Sub GetSpreedSheet(Optional SetList As Boolean = True)

        DataExcel = Nothing

        Try
            Dim dt As New System.Data.DataTable
            Dim ColName As String = ""

            If SetList Then
                FTMapStyleCode.Properties.Items.Clear()
                FTMapStyleName.Properties.Items.Clear()
            End If


            With opshet.ActiveWorksheet
                For C As Integer = 0 To .GetUsedRange().ColumnCount - 1
                    Try
                        ColName = .Columns(C).Heading()
                    Catch ex As Exception
                        ColName = C.ToString()
                    End Try

                    dt.Columns.Add(ColName, GetType(String))

                    If SetList Then
                        FTMapStyleCode.Properties.Items.Add(ColName)
                        FTMapStyleName.Properties.Items.Add(ColName)
                    End If

                Next

                For r As Integer = 1 To .GetUsedRange().RowCount - 1

                    Dim Rx As System.Data.DataRow = dt.NewRow()

                    For C As Integer = 0 To .GetUsedRange().ColumnCount - 1

                        ColName = .Columns(C).Heading()

                        If .Cells(r, C).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
                            Rx.Item(ColName) = HI.UL.ULDate.ConvertEN(.Cells(r, C).Value.DateTimeValue)

                        Else
                            Rx.Item(ColName) = .Cells(r, C).DisplayText
                        End If

                    Next

                    dt.Rows.Add(Rx)

                Next
            End With

            DataExcel = dt.Copy

        Catch ex As Exception

        End Try
        If SetList Then
            FTMapStyleCode.SelectedIndex = -1
            FTMapStyleName.SelectedIndex = -1
        End If


    End Sub

    Private Sub ocmImportnetprice_Click(sender As Object, e As EventArgs) Handles ocmImportnetprice.Click
        Try

            If FTMapStyleCode.Text <> "" And FTMapStyleName.Text <> "" Then
                Dim dtstyle As New System.Data.DataTable
                dtstyle.Columns.Add("FNSeq", GetType(Integer))
                dtstyle.Columns.Add("FTStyleCode", GetType(String))
                dtstyle.Columns.Add("FTStyleName", GetType(String))
                dtstyle.Columns.Add("FTStateGameDays", GetType(Integer))

                GetSpreedSheet(False)


                If Not (DataExcel Is Nothing) Then
                    If DataExcel.Rows.Count > 0 Then

                        Dim RIdx As Integer = 0
                        Dim RowSeq As Integer = 0

                        For Each R As DataRow In DataExcel.Rows
                            ' If RIdx > 0 Then

                            If R.Item(FTMapStyleCode.Text).ToString() <> "" Then

                                RowSeq = RowSeq + 1
                                dtstyle.Rows.Add(RowSeq, R.Item(FTMapStyleCode.Text).ToString(), R.Item(FTMapStyleName.Text).ToString(), FNStyleType.SelectedIndex)

                            End If


                            ' End If
                            RIdx = RIdx + 1
                        Next

                        With stylepopup
                            .ogcstyle.DataSource = dtstyle
                            .ShowDialog()
                        End With

                    End If

                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmimportimmage_Click(sender As Object, e As EventArgs) Handles ocmimportimmage.Click

        Try

            Dim Op As New System.Windows.Forms.FolderBrowserDialog


            Try
                If Op.ShowDialog() = System.Windows.Forms.DialogResult.OK Then


                    Dim ImgFileName As String = ""
                    Dim _Str As String = ""
                    Dim FileDirectory As New IO.DirectoryInfo(Op.SelectedPath)
                    Dim FileJpg As IO.FileInfo() = FileDirectory.GetFiles("*.jpg")
                    Dim FilePng As IO.FileInfo() = FileDirectory.GetFiles("*.png")
                    Dim StyleCode As String = ""

                    If FileJpg.Length > 0 Or FilePng.Length > 0 Then

                        Dim dtstyleImage As New System.Data.DataTable()
                        dtstyleImage.Columns.Add("FTStyleCode", GetType(String))
                        dtstyleImage.Columns.Add("FTFileName", GetType(String))
                        dtstyleImage.Columns.Add("FTFilePath", GetType(String))
                        dtstyleImage.Columns.Add("FTFileEx", GetType(String))

                        For Each File As IO.FileInfo In FileJpg

                            ImgFileName = File.Name
                            StyleCode = ImgFileName.Split("-")(0)

                            dtstyleImage.Rows.Add(StyleCode, ImgFileName, File.FullName, File.Extension)



                        Next

                        For Each File As IO.FileInfo In FilePng

                            ImgFileName = File.Name
                            StyleCode = ImgFileName.Split("-")(0)

                            dtstyleImage.Rows.Add(StyleCode, ImgFileName, File.FullName)

                        Next


                        Dim IndxImage As Integer = 0

                        Dim grpstyle As List(Of String) = (dtstyleImage.Select("FTStyleCode<>''", "FTStyleCode").CopyToDataTable).AsEnumerable() _
                                                           .Select(Function(r) r.Field(Of String)("FTStyleCode")) _
                                                           .Distinct() _
                                                           .ToList()

                        For Each Strstyle As String In grpstyle

                            StyleCode = Strstyle
                            IndxImage = 0

                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
                            HI.Conn.SQLConn.SqlConnectionOpen()


                            Try
                                _Str = ""
                                _Str = "UPDATE A"
                                _Str &= Environment.NewLine & "SET A.[FPStyleImage1] = @FPOrderImage1,"
                                _Str &= Environment.NewLine & "    A.[FPStyleImage2] = @FPOrderImage2,"
                                _Str &= Environment.NewLine & "    A.[FPStyleImage3] = @FPOrderImage3,"
                                _Str &= Environment.NewLine & "    A.[FPStyleImage4] = @FPOrderImage4"
                                _Str &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS A"
                                _Str &= Environment.NewLine & "WHERE  A.FTStyleCode = @FTStyleCode"


                                Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)

                                cmd.Parameters.AddWithValue("FTStyleCode", StyleCode)


                                For Each r As DataRow In dtstyleImage.Select("FTStyleCode='" & HI.UL.ULF.rpQuoted(StyleCode) & "'", "FTFileName")

                                    IndxImage = IndxImage + 1


                                    Select Case IndxImage
                                        Case 1
                                            Dim msFPOrderImage1 As New MemoryStream()
                                            Dim data As Byte() = Nothing

                                            Try
                                                data = HI.UL.ULImage.ConvertImageToByteArray(r!FTFilePath.ToString, UL.ULImage.PicType.Employee)
                                                cmd.Parameters.AddWithValue("FPOrderImage1", data)
                                            Catch ex As Exception
                                                Dim paramOrderImage1 As New SqlParameter("@FPOrderImage1", SqlDbType.Image)
                                                paramOrderImage1.Value = DBNull.Value
                                                cmd.Parameters.Add(paramOrderImage1)
                                            End Try

                                        Case 2

                                            Dim msFPOrderImage2 As New MemoryStream()
                                            Dim data2 As Byte() = Nothing

                                            Try
                                                data2 = HI.UL.ULImage.ConvertImageToByteArray(r!FTFilePath.ToString, UL.ULImage.PicType.Employee)
                                                cmd.Parameters.AddWithValue("FPOrderImage2", data2)
                                            Catch ex As Exception
                                                Dim paramOrderImage2 As New SqlParameter("@FPOrderImage2", SqlDbType.Image)
                                                paramOrderImage2.Value = DBNull.Value
                                                cmd.Parameters.Add(paramOrderImage2)
                                            End Try

                                        Case 3
                                            Dim msFPOrderImage3 As New MemoryStream()
                                            Dim data3 As Byte() = Nothing
                                            Try
                                                data3 = HI.UL.ULImage.ConvertImageToByteArray(r!FTFilePath.ToString, UL.ULImage.PicType.Employee)
                                                cmd.Parameters.AddWithValue("FPOrderImage3", data3)
                                            Catch ex As Exception
                                                Dim paramOrderImage3 As New SqlParameter("@FPOrderImage3", SqlDbType.Image)
                                                paramOrderImage3.Value = DBNull.Value
                                                cmd.Parameters.Add(paramOrderImage3)
                                            End Try

                                        Case 4
                                            Dim msFPOrderImage4 As New MemoryStream()
                                            Dim data4 As Byte() = Nothing

                                            Try
                                                data4 = HI.UL.ULImage.ConvertImageToByteArray(r!FTFilePath.ToString, UL.ULImage.PicType.Employee)
                                                cmd.Parameters.AddWithValue("FPOrderImage4", data4)
                                            Catch ex As Exception
                                                Dim paramOrderImage4 As New SqlParameter("@FPOrderImage4", SqlDbType.Image)
                                                paramOrderImage4.Value = DBNull.Value
                                                cmd.Parameters.Add(paramOrderImage4)
                                            End Try
                                    End Select


                                    If IndxImage = 4 Then
                                        Exit For
                                    End If

                                Next

                                cmd.CommandType = CommandType.Text

                                If cmd.ExecuteNonQuery() > 0 Then
                                    _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_IMPORTSTYLE_IMMAGE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(StyleCode) & "'"
                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_MERCHAN)


                                    If pPathSharedStyle <> "" Then

                                        For Each r As DataRow In dtstyleImage.Select("FTStyleCode='" & HI.UL.ULF.rpQuoted(StyleCode) & "'", "FTFileName")

                                            Try

                                                System.IO.File.Copy(r!FTFilePath.ToString, pPathSharedStyle & r!FTFileName.ToString, "." & r!FTFileEx.ToString)

                                            Catch ex As Exception
                                            End Try

                                        Next

                                    End If

                                End If


                                cmd.Parameters.Clear()

                            Catch ex As Exception

                            End Try

                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)




                        Next



                        HI.MG.ShowMsg.mInfo("Export Data Complete..", 1111120400, Me.Text, , MessageBoxIcon.Information)






                    Else


                    End If








                End If

            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try

    End Sub
End Class