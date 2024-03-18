Public Class wTimeScanHistory
    Private oGridViewSizeSpec As DevExpress.XtraGrid.Views.Grid.GridView
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
        Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False

        'Call InitGrid()
    End Sub

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Proc And Func"
    Private Function W_PRCbRemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
        Try

            With pGridView
                For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns.Item(nLoopColGridView).Name.ToString.ToUpper
                        Case "ColFTPositname".ToString.ToUpper
                            '...Do nothing
                            Exit For
                        Case Else
                            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    End Select

                Next

            End With

            'pGridView.Columns.Clear()

        Catch ex As Exception
            ' MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

        Return pGridView
    End Function
#End Region

#Region "Procedure"

    Private Sub LoadData(Optional StateTimeError As Boolean = False)
        Me.ogdtime.DataSource = Nothing
        Me.ogdtime.Refresh()

        Call W_PRCbRemoveGridViewColumn(Me.ogvtime)
        'Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.RefreshData()

        Try
            Dim _StartDate As String = ""
            Dim _EndDate As String = ""
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _TimeDT As DataTable

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"


            Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

            _Qry = " SELECT CASE WHEN  ISDATE(H.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,H.FTDateTrans),103) ELSE '' END As FTDateTrans ,H.FTEmpBarcode ,M.FNHSysEmpID ,M.FTEmpCode"


            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then

                _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
                _Qry &= " , D.FTDeptDescTH AS FTDeptDesc , S.FTSectNameTH AS FTSectName, US.FTUnitSectNameTH AS FTUnitSectName, P.FTPositNameTH AS FTPositName"
            Else
                _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
                _Qry &= " , D.FTDeptDescEN AS FTDeptDesc , S.FTSectNameEN AS FTSectName,  US.FTUnitSectNameEN AS FTUnitSectName, P.FTPositNameEN AS FTPositName"
            End If

            _Qry &= vbCrLf & "  ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
            _Qry &= vbCrLf & "  ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode ,ISNULL(S.FTSectCode,'') AS FTSectCode"
            _Qry &= vbCrLf & "  ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
            _Qry &= vbCrLf & "  ,ISNULL(P.FTPositCode, '' ) AS FTPositCode "


            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardHistory AS H WITH (NOLOCK)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) ON H.FTEmpBarcode = M.FTEmpBarcode"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"



            _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  AND FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "' AND FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
            _Qry &= vbCrLf & "  AND  (FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "


            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  D.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  D.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If
            _Qry &= vbCrLf & "  GROUP BY H.FTEmpBarcode,FTDateTrans,M.FNHSysEmpID,FTEmpCode,FTPositCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH"
                _Qry &= ", D.FTDeptDescTH, S.FTSectNameTH , US.FTUnitSectNameTH , P.FTPositNameTH "

            Else
                _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN"
                _Qry &= ", D.FTDeptDescEN, S.FTSectNameEN , US.FTUnitSectNameEN , P.FTPositNameEN "
            End If

            _Qry &= vbCrLf & " ,FTEmpTypeCode ,FTDeptCode ,FTDivisonCode ,FTSectCode ,FTUnitSectCode "
            _Qry &= vbCrLf & "  ORDER BY FTDateTrans,M.FTEmpCode  "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            'Time DataTable
            _Qry = " SELECT CASE WHEN  ISDATE(H.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,H.FTDateTrans),103) ELSE '' END As FTDateTrans"
            _Qry &= vbCrLf & " ,FTTimeTrans ,H.FTEmpBarcode ,M.FNHSysEmpID ,M.FTEmpCode"

            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTimecardHistory AS H WITH (NOLOCK)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) ON H.FTEmpBarcode = M.FTEmpBarcode"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"


            _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  AND FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "' AND FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
            _Qry &= vbCrLf & "  AND  (FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "


            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  D.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  D.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            _Qry &= vbCrLf & "  ORDER BY FTDateTrans,M.FTEmpCode  "

            _TimeDT = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _ColCount As Integer = 0
            Dim _CountTable As DataRow()

            oGridViewSizeSpec = Me.ogdtime.Views(0)

            For Each _R As DataRow In _dt.Rows
                _CountTable = _TimeDT.Select("FTDateTrans = '" & _R!FTDateTrans.ToString & "' AND FNHSysEmpId = " & _R!FNHSysEmpId)
                If (_ColCount < _CountTable.Length) Then
                    _ColCount = _CountTable.Length
                End If
            Next

            For i As Integer = 1 To _ColCount
                Dim oColScanTime As DataColumn = New DataColumn("Time" & i, System.Type.GetType("System.String"))
                oColScanTime.Caption = "Time" & i

                oGridViewSizeSpec.Columns.AddField(oColScanTime.ColumnName)
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).FieldName = oColScanTime.ColumnName
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Name = oColScanTime.ColumnName
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Caption = oColScanTime.Caption
                'oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Visible = True
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Width = 100
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.TabStop = False
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.AllowEdit = False
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.AllowMove = False
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.AllowSort = False

                _dt.Columns.Add(oColScanTime.ColumnName, oColScanTime.DataType)
            Next

            For Each _R As DataRow In _TimeDT.Rows
                For Each _Row As DataRow In _dt.Rows
                    Dim ColName As String
                    If (_Row!FNHSysEmpId = _R!FNHSysEmpId) And (_Row!FTDateTrans = _R!FTDatetrans) Then
                        For i As Integer = 1 To _ColCount
                            ColName = "Time" & i
                            Dim _Text As String = _R!FTTimeTrans.ToString
                            _Text = _Text.Substring(0, 2) & ":" & _Text.Substring(2, 2)
                            If (IsDBNull(_Row(ColName))) Then
                                _Row(ColName) = _Text
                                Exit For
                            End If
                        Next
                    End If
                Next
            Next

            Me.ogdtime.DataSource = _dt
            Me.ogvtime.BestFitColumns()
            ogvtime.ExpandAllGroups()
            _Spls.Close()

            '_RowDataChange = False
        Catch
        End Try

    End Sub


#End Region

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FDStartDate.Text <> "" Then

            If Me.FDEndDate.Text <> "" Then
                Call LoadData()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
                FDEndDate.Focus()
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FDStartDate.Focus()
        End If
    End Sub

    Private Sub wTimeScanHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogdtime.DataSource = Nothing
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class