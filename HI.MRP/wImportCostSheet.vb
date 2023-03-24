Imports System.IO
Imports DevExpress.Spreadsheet
Imports Microsoft.Win32

Public Class wImportCostSheet

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

                            Me.FTStyleCode.Text = ""
                            Me.FTSeasonCode.Text = ""
                            Me.FNFabricCost.Value = 0
                            Me.FNAccCost.Value = 0
                            Me.FNFabricImportCost.Value = 0
                            Me.FNAccImportCost.Value = 0

                            FTFilePath.Text = opFileDialog.FileName

                            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                            opshet.LoadDocument(FTFilePath.Text)

                            opshet_ActiveSheetChanging(opshet, New DevExpress.Spreadsheet.ActiveSheetChangingEventArgs(0, opshet.ActiveWorksheet.Name))
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



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FTFilePath.Text = ""
        Dim I As Integer = 0
        Try
            opshet.Document.Worksheets.Insert(0)

            For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets

                If I = 0 Then
                Else
                    opshet.Document.Worksheets.Remove(wk)
                End If

                I = I + 1

            Next

        Catch ex As Exception
        End Try

        Try
            opshet.Document.Worksheets(0).Name = "Sheet1"
        Catch ex As Exception
        End Try

        Me.FTStyleCode.Text = ""
        Me.FTSeasonCode.Text = ""
        Me.FNFabricCost.Value = 0
        Me.FNAccCost.Value = 0
        Me.FNFabricImportCost.Value = 0
        Me.FNAccImportCost.Value = 0
        FNCmp.Value = 0
        Me.FTUserImport.Text = ""
        Me.FTUserImporttime.Text = ""

    End Sub

    Private Function ImportData() As Boolean
        If Me.FTFilePath.Text <> "" Then

            If Me.FTStyleCode.Text.Trim <> "" And Me.FTSeasonCode.Text.Trim <> "" Then
                Dim _FNCmp As Double = 0

                Dim _Spls As New HI.TL.SplashScreen("Saving Data......")

                Try

                    Dim _Qry As String = ""
                    Dim _FNHSysStyleId As Integer = 0
                    Dim _FNHSysSeasonId As Integer = 0

                    _Qry = "SELECT TOP 1 FNHSysStyleId "
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"

                    _FNHSysStyleId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))


                    _Qry = "SELECT TOP 1 FNHSysSeasonId "
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text.Trim) & "'"

                    _FNHSysSeasonId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))


                    _Qry = "SELECT TOP 1 FTStyleCode "
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"


                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "" Then
                        Dim nFNHSysStyleId As Integer
                        Dim nFNHSysCustId As Integer
                        Dim tStyleImport As String
                        Dim tStyleImportDesc As String

                        _Qry = "SELECT TOP 1 A.FNHSysCustId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS A WITH(NOLOCK) WHERE A.FTCustCode = N'NI';"
                        nFNHSysCustId = Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_MASTER, "0"))

                        nFNHSysStyleId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]([FTInsUser]"
                        _Qry &= vbCrLf & "											  ,[FDInsDate]"
                        _Qry &= vbCrLf & "										  ,[FTInsTime]"
                        _Qry &= vbCrLf & "										  ,[FNHSysStyleId]"
                        _Qry &= vbCrLf & "											  ,[FTStyleCode]"
                        _Qry &= vbCrLf & "											  ,[FTStyleNameTH]"
                        _Qry &= vbCrLf & "									  ,[FTStyleNameEN]"
                        _Qry &= vbCrLf & "										  ,[FTRemark]"
                        _Qry &= vbCrLf & "										  ,[FTStateActive],[FNCMDisPer]"
                        _Qry &= vbCrLf & "											  ,[FNHSysCustId],[FNCM])"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "      ," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "      ," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & "      ," & nFNHSysStyleId & ""
                        _Qry &= vbCrLf & "      ,'" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                        _Qry &= vbCrLf & "      ,'" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                        _Qry &= vbCrLf & "      ,'" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                        _Qry &= vbCrLf & "      ,''"
                        _Qry &= vbCrLf & "      ,'1'"
                        _Qry &= vbCrLf & "      ,5.0"
                        _Qry &= vbCrLf & "      ," & nFNHSysCustId & ""
                        _Qry &= vbCrLf & "      ," & Me.FNCmp.Value & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    Else

                        _Qry = "UPDATE X SET  [FNCM] =" & Me.FNCmp.Value & ",FNNetCM=(" & Me.FNCmp.Value & " - ISNULL(FNCMDisAmt,0)) "
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS X"
                        _Qry &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                        _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) <=0 "
                        '_Qry &= vbCrLf & " AND ISNULL( [FNCM],0) > " & Me.FNCmp.Value & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                        _Qry = "UPDATE X SET  [FNCM] =" & Me.FNCmp.Value & ",FNNetCM=(" & Me.FNCmp.Value & " - ISNULL(FNCMDisAmt,0)) "
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS X"
                        _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & ""
                        _Qry &= vbCrLf & " AND FNHSysSeasonId=" & _FNHSysSeasonId & ""
                        _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) <=0 "
                        ' _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) > " & Me.FNCmp.Value & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    End If

                    _Qry = "UPDATE X SET  [FNCM] =" & Me.FNCmp.Value & ",FNNetCM=(" & Me.FNCmp.Value & " - ISNULL(FNCMDisAmt,0)) "
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS X"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleIdTo=" & _FNHSysStyleId & ""
                    _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) <=0 "
                    '  _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) > " & Me.FNCmp.Value & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    _Qry = " SELECT TOP 1 FNHSysSeasonId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS X"
                    _Qry &= vbCrLf & " WHERE FNHSysStyleId =" & _FNHSysStyleId & " "
                    _Qry &= vbCrLf & " AND FNHSysSeasonId=" & _FNHSysSeasonId & ""

                    If Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")) > 0 Then
                        _Qry = "UPDATE X SET  [FNCM] =" & Me.FNCmp.Value & ",FNNetCM=(" & Me.FNCmp.Value & " - ISNULL(FNCMDisAmt,0)) "
                        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS X"
                        _Qry &= vbCrLf & " WHERE FNHSysStyleId =" & _FNHSysStyleId & " "
                        _Qry &= vbCrLf & " AND FNHSysSeasonId=" & _FNHSysSeasonId & ""
                        _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) <=0 "
                        '  _Qry &= vbCrLf & " AND ISNULL( [FNCM],0) > " & Me.FNCmp.Value & ""

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    Else

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice ("
                        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FNHSysSeasonId, FNCM, FNCMDisPer, FNCMDisAmt, FNNetCM, FNCostTransport "
                        _Qry &= vbCrLf & " ) "
                        _Qry &= vbCrLf & " SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", " & _FNHSysStyleId
                        _Qry &= vbCrLf & ", " & _FNHSysSeasonId
                        _Qry &= vbCrLf & ", " & Me.FNCmp.Value
                        _Qry &= vbCrLf & ", 5"
                        _Qry &= vbCrLf & ", 0"
                        _Qry &= vbCrLf & ", " & Me.FNCmp.Value
                        _Qry &= vbCrLf & ", 0"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    End If


                    '_Qry = " UPDATE A SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '_Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    '_Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    '_Qry &= vbCrLf & ", FNFabricAmt=" & FNFabricCost.Value & ""
                    '_Qry &= vbCrLf & ", FNAccessoryAmt=" & FNAccCost.Value & ""
                    '_Qry &= vbCrLf & ", FNImportFabricAmt=" & FNFabricImportCost.Value & ""
                    '_Qry &= vbCrLf & ", FNImportAccessoryAmt=" & FNAccImportCost.Value & ""
                    '_Qry &= vbCrLf & ", FNImportCMP=" & FNCmp.Value & ""
                    '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS A"
                    '_Qry &= vbCrLf & "  WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                    '_Qry &= vbCrLf & "        AND FTSeason='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text.Trim) & "'"

                    _Qry = " UPDATE A SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ", FNFabricAmt=CASE WHEN " & FNFabricCost.Value & ">0 THEN CASE WHEN " & FNFabricCost.Value & "< FNFabricAmt THEN " & FNFabricCost.Value & "  ELSE FNFabricAmt END ELSE FNFabricAmt END"
                    _Qry &= vbCrLf & ", FNAccessoryAmt=CASE WHEN " & FNAccCost.Value & ">0 THEN CASE WHEN " & FNAccCost.Value & "< FNAccessoryAmt THEN " & FNAccCost.Value & "  ELSE FNAccessoryAmt END ELSE FNAccessoryAmt END"
                    _Qry &= vbCrLf & ", FNImportFabricAmt=CASE WHEN " & FNFabricImportCost.Value & ">0 THEN CASE WHEN " & FNFabricImportCost.Value & "< FNImportFabricAmt THEN " & FNFabricImportCost.Value & "  ELSE FNImportFabricAmt END ELSE FNImportFabricAmt END"
                    _Qry &= vbCrLf & ", FNImportAccessoryAmt=CASE WHEN " & FNAccImportCost.Value & ">0 THEN CASE WHEN " & FNAccImportCost.Value & "< FNImportAccessoryAmt THEN " & FNAccImportCost.Value & "  ELSE FNImportAccessoryAmt END ELSE FNImportAccessoryAmt END"
                    _Qry &= vbCrLf & ", FNImportCMP=CASE WHEN " & FNCmp.Value & ">0 THEN CASE WHEN " & FNCmp.Value & "< FNImportCMP THEN " & FNCmp.Value & "  ELSE FNImportCMP END ELSE FNImportCMP END"
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS A"
                    _Qry &= vbCrLf & "  WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                    _Qry &= vbCrLf & "        AND FTSeason='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text.Trim) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT) = False Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale "
                        _Qry &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime,  FTStyleCode, FTSeason, FNFabricAmt, FNAccessoryAmt, FNImportFabricAmt, FNImportAccessoryAmt,FNImportCMP)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTStyleCode.Text.Trim) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text.Trim) & "'"
                        _Qry &= vbCrLf & "," & FNFabricCost.Value & ""
                        _Qry &= vbCrLf & "," & FNAccCost.Value & ""
                        _Qry &= vbCrLf & "," & FNFabricImportCost.Value & ""
                        _Qry &= vbCrLf & "," & FNAccImportCost.Value & ""
                        _Qry &= vbCrLf & "," & FNCmp.Value & ""

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT) = False Then

                            _Spls.Close()
                            Return False

                        End If

                    End If

                Catch ex As Exception
                    _Spls.Close()
                    Return False
                End Try

                _Spls.Close()
                Return True
            Else

                HI.MG.ShowMsg.mInfo("ข้อมูลใน Sheet ไม่ถูกต้องกรุณาทำการตรวจสอบ !!!", 1602149077, Me.Text, , Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If

        Else

            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก File !!!", 1606140078, Me.Text, , Windows.Forms.MessageBoxIcon.Warning)
            Return False

        End If
    End Function

    Private Sub ocmimportoptiplan_Click(sender As Object, e As EventArgs) Handles ocmImportOrder.Click

        If ImportData() Then

            opshet_ActiveSheetChanging(opshet, New DevExpress.Spreadsheet.ActiveSheetChangingEventArgs(0, opshet.ActiveWorksheet.Name))
            HI.MG.ShowMsg.mInfo("Save Data Complete !!!", 1602110547, Me.Text, , Windows.Forms.MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub wImportOptiplan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        FTFilePath.Focus()
    End Sub

    Private Sub wImportOptiplan_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub opshet_ActiveSheetChanging(sender As Object, e As ActiveSheetChangingEventArgs) Handles opshet.ActiveSheetChanging
        Try

            Dim _dt As DataTable = HI.UL.ReadExcel.Read(Me.FTFilePath.Text.Trim, e.NewActiveSheetName)

            If _dt.Columns.Count < 26 Then
                For I As Integer = 26 To (_dt.Columns.Count + 1) Step -1
                    _dt.BeginInit()
                    _dt.Columns.Add("F" & (_dt.Columns.Count + 1).ToString, GetType(String))
                    _dt.EndInit()
                Next

                For Each R As DataRow In _dt.Rows
                    R!F26 = ""
                Next
            End If

            Dim _FTStyleCode As String = ""
            Dim _FTSeasonCode As String = ""
            Dim _Fabric As Double = 0
            Dim _FabricImport As Double = 0
            Dim _Accessory As Double = 0
            Dim _AccessoryImport As Double = 0
            Dim _FNCmp As Double = 0
            Me.FTUserImport.Text = ""
            Me.FTUserImporttime.Text = ""
            FNCmp.Value = 0
            Me.FTStyleCode.Text = ""
            Me.FTSeasonCode.Text = ""
            Me.FNFabricCost.Value = 0
            Me.FNAccCost.Value = 0
            Me.FNFabricImportCost.Value = 0
            Me.FNAccImportCost.Value = 0
            Dim _RowIdx As Integer = 0

            If _dt.Rows.Count > 0 Then
                If _dt.Select("F1='ITEM#'").Length > 0 Then
                    If Microsoft.VisualBasic.Left(_dt.Rows(0)!F1.ToString, 5).ToUpper = "STYLE" Then
                        _FTStyleCode = _dt.Rows(0)!F2.ToString
                        _FTSeasonCode = _dt.Rows(0)!F4.ToString.Replace("'", "")
                    End If

                    If _FTStyleCode <> "" And _FTSeasonCode <> "" Then
                        Dim _FoundFabric = False
                        Dim _FoundAcc = False
                        For Each R As DataRow In _dt.Rows
                            _RowIdx = _RowIdx + 1

                            If _FoundFabric = False And _FoundAcc = False Then

                                If R!F1.ToString = "ITEM#" Then
                                    _FoundFabric = True
                                End If

                            Else
                                If R!F1.ToString <> "ITEM#" And (_FoundFabric = True Or _FoundAcc = True) Then
                                    If R!F2.ToString.ToUpper <> "DESCRIPTION".ToUpper Then

                                        If _FoundFabric = True Then

                                            If R!F2.ToString <> "" Then

                                                If Microsoft.VisualBasic.Left(R!F2.ToString.ToUpper, 5) <> "TOTAL" Then

                                                    If R!F26.ToString = "0" Then
                                                        _FNCmp = _FNCmp + Val(R!F17.ToString.Replace("$", ""))

                                                    Else

                                                        _Fabric = _Fabric + Val(R!F17.ToString.Replace("$", ""))
                                                        _FabricImport = _FabricImport + Val(R!F19.ToString.Replace("$", ""))

                                                    End If
                                                  
                                                Else

                                                    _FoundFabric = False
                                                    _FoundAcc = True

                                                End If
                                            End If
                                        Else
                                            If R!F2.ToString <> "" Then
                                                If Microsoft.VisualBasic.Left(R!F2.ToString.ToUpper, 5) <> "TOTAL" Then
                                                    If R!F1.ToString.ToUpper <> "STOP".ToUpper Then

                                                        If Microsoft.VisualBasic.Left(R!F1.ToString.ToUpper, 7) <> "Comment".ToUpper Then

                                                            If R!F26.ToString = "0" Then

                                                                _FNCmp = _FNCmp + Val(R!F17.ToString.Replace("$", ""))

                                                            Else

                                                                _Accessory = _Accessory + Val(R!F17.ToString.Replace("$", ""))
                                                                _AccessoryImport = _AccessoryImport + Val(R!F19.ToString.Replace("$", ""))

                                                            End If

                                                        Else
                                                            Exit For
                                                        End If
                                                    End If
                                                End If

                                            End If

                                        End If
                                    End If

                                End If
                            End If
                        Next

                        If _dt.Select("F12='NOSEW C0ST'").Length > 0 Then

                            For Each R As DataRow In _dt.Select("F12='NOSEW C0ST'")

                                _FNCmp = _FNCmp + Val(R!F16.ToString.Replace("$", "").Trim())
                                Exit For

                            Next

                        Else

                            For Each R As DataRow In _dt.Select("F13='NOSEW C0ST'")

                                _FNCmp = _FNCmp + Val(R!F16.ToString.Replace("$", "").Trim())
                                Exit For

                            Next

                        End If
                       
                        If _dt.Select("F12='CMT'").Length > 0 Or _dt.Select("F13='CMT'").Length Then

                            If _dt.Select("F12='CMT'").Length > 0 Then

                                For Each R As DataRow In _dt.Select("F12='CMT'")

                                    _FNCmp = _FNCmp + Val(R!F16.ToString.Replace("$", "").Trim())

                                    Exit For

                                Next

                            Else

                                For Each R As DataRow In _dt.Select("F13='CMT'")

                                    _FNCmp = _FNCmp + Val(R!F16.ToString.Replace("$", "").Trim())

                                    Exit For

                                Next

                            End If
                          
                        Else

                            If _dt.Select("F12='CMP'").Length > 0 Then

                                For Each R As DataRow In _dt.Select("F12='CMP'")

                                    _FNCmp = _FNCmp + Val(R!F16.ToString.Replace("$", "").Trim())
                                    Exit For

                                Next

                            Else

                                For Each R As DataRow In _dt.Select("F13='CMP'")

                                    _FNCmp = _FNCmp + Val(R!F16.ToString.Replace("$", "").Trim())

                                    Exit For

                                Next

                            End If

                        End If

                    End If

                    If _FTStyleCode <> "" And _FTSeasonCode <> "" Then

                        Me.FTStyleCode.Text = _FTStyleCode
                        Me.FTSeasonCode.Text = _FTSeasonCode
                        Me.FNFabricCost.Value = _Fabric
                        Me.FNAccCost.Value = _Accessory
                        Me.FNFabricImportCost.Value = _FabricImport
                        Me.FNAccImportCost.Value = _AccessoryImport
                        Me.FNCmp.Value = _FNCmp

                        Dim _Qry As String = ""
                        Dim _dtcsm As DataTable

                        _Qry = "    SELECT  CASE WHEN FTUpdUser IS NULL THEN FTInsUser ELSE FTUpdUser END AS FTUser"
                        _Qry &= vbCrLf & "  ,CASE WHEN FTUpdUser IS NULL THEN FDInsDate +'  '+ FTInsTime ELSE FDUpdDate +'  '+ FTUpdTime END AS FTTime"
                        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheetFirstSale AS X WITH(NOLOCK)"
                        _Qry &= vbCrLf & "   WHERE  FTStyleCode='" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "'"
                        _Qry &= vbCrLf & "   AND FTSeason='" & HI.UL.ULF.rpQuoted(_FTSeasonCode) & "'"

                        _dtcsm = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                        For Each Rcx As DataRow In _dtcsm.Rows

                            Me.FTUserImport.Text = Rcx!FTUser.ToString
                            Me.FTUserImporttime.Text = Rcx!FTTime.ToString

                            Exit For

                        Next

                    End If

                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class