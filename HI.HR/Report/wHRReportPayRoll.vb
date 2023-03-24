Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Charts.Native
Imports DevExpress.XtraRichEdit.Import.Html
Imports DevExpress.XtraRichEdit.Model

Public Class wHRReportPayRoll
    Private _LstReport As HI.RP.ListReport
    Sub New(Optional _SysFormName As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _SysFormName <> "" Then
            Me.Name = _SysFormName
        End If

        Condition.PrePareData()

        _LstReport = New HI.RP.ListReport(_SysFormName)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""

        If Me.FTPayTerm.Text = "" Or Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If



        Dim _Qry As String
        Dim UseEmpTypeGroup As Integer
        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'UseEmpTypeGroup')"

        UseEmpTypeGroup = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "0")


        If UseEmpTypeGroup = 1 Then

            'If Me.FNEmpTypeGroup.SelectedIndex < 0 Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeGroup_lbl.Text)
            '    Exit Sub
            'End If
            Dim a As Integer = 0

            For i As Integer = 0 To Condition.ogvemptype.DataRowCount - 1
                If Condition.ogvemptype.GetRowCellValue(i, "FTSelect") = "1" Then
                    a = a + 1
                End If
            Next

            If a = 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
                Exit Sub
            End If


            Dim _dt As DataTable
            With CType(Condition.ogdemptype.DataSource, DataTable)
                Call .AcceptChanges()
                _dt = .Copy

                Call .AcceptChanges()
            End With




            Dim grpfodata As List(Of Integer) = (_dt.Select("FTSelect='1'", "FNHSysEmpTypeId").CopyToDataTable).AsEnumerable() _
            .Select(Function(r) r.Field(Of Integer)("FNHSysEmpTypeId")) _
            .Distinct() _
            .ToList()

            If grpfodata.Count > 1 Then
                Dim _IN_FNHSysEmpTypeId As String = ""
                For Each _FNHSysEmpTypeId As Integer In grpfodata

                    If _IN_FNHSysEmpTypeId <> "" Then
                        _IN_FNHSysEmpTypeId += "," & Val(_FNHSysEmpTypeId)
                    Else
                        _IN_FNHSysEmpTypeId = Val(_FNHSysEmpTypeId)
                    End If

                Next

                _Formular &= IIf(_Formular.Trim <> "", " And ", "")
                _Formular &= "  {THRMEmpType.FNHSysEmpTypeId} IN [ " & _IN_FNHSysEmpTypeId & " ] "
            ElseIf grpfodata.Count = 1 Then
                For Each _FNHSysEmpTypeId As Integer In grpfodata

                    _Formular &= IIf(_Formular.Trim <> "", " And ", "")
                    _Formular &= "  {THRMEmpType.FNHSysEmpTypeId}=" & _FNHSysEmpTypeId & " "

                Next
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Condition.ogdemptype.Text)
                Exit Sub
            End If

            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRTPayRoll.FTPayTerm}='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "' "


        Else
            If Me.FNHSysEmpTypeId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
                Exit Sub
            End If


            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' "

            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRTPayRoll.FTPayTerm}='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "' "

        End If





        Dim tText As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " " & tText
        End If

        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        If _AllReportName <> "" Then
            Call HI.ST.Security.CreateTempEmpMaster(Condition)
            Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text, FTPayTerm.Text)

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GETPayrollFin '" & Val(HI.ST.SysInfo.CmpID) & "','" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "' , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                    .Formular = _Formular
                    .ReportName = _ReportName
                    .Preview()
                End With
            Next
        Else
            HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wReportHRByPayRoll_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If

            Dim _Qry As String
            Dim UseEmpTypeGroup As Integer
            _Qry = " SELECT TOP 1 FTCfgData"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  (FTCfgName = N'UseEmpTypeGroup')"

            UseEmpTypeGroup = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "0")


            If UseEmpTypeGroup = 1 Then
                FTPayTerm.Properties.Buttons(0).Tag = 699
                Condition.otcForm.TabPages(0).PageVisible = True
                '' Me.FNEmpTypeGroup.SelectedIndex = -1
                Condition.ogdemptype.DataSource = New DataTable()
                Me.ochkselectall.Visible = True
                FNHSysEmpTypeGroup_lbl.Visible = False
                FNEmpTypeGroup.Visible = False



                FNHSysEmpTypeId_lbl.Visible = False
                FNHSysEmpTypeId.Visible = False
                FNHSysEmpTypeId_None.Visible = False

                Call BindDataEmptypeGroup()
                Call BindDataEmptype()

            Else
                FTPayTerm.Properties.Buttons(0).Tag = 65
                Condition.otcForm.TabPages(0).PageVisible = False

                Me.ochkselectall.Visible = False
                FNHSysEmpTypeGroup_lbl.Visible = False
                FNEmpTypeGroup.Visible = False

                FNHSysEmpTypeId_lbl.Visible = True
                FNHSysEmpTypeId.Visible = True
                FNHSysEmpTypeId_None.Visible = True
                Call BindDataEmptype()

            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub BindDataEmptypeGroup()
        Try
            Dim _Qry As String = ""
            Dim _DtG As DataTable

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry = " SELECT     '0' AS [FTSelect] , E.FNEmpTypeGroup AS FNEmpTypeGroupID, L.FTNameTH AS 'FTEmpTypeGroup' "
                _Qry &= vbCrLf & ", MIN(D.FDCalDateBegin) AS 'FDCalDateBegin', MAX(D.FDCalDateEnd) As 'FDCalDateEnd', MAX(D.FDPayDate) AS 'FDPayDate' "

                _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType E ON H.FNHSysEmpTypeId= E.FNHSysEmpTypeId INNER JOIN "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm AND H.FTPayYear = D.FTPayYear AND "
                _Qry &= vbCrLf & " H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex "
                _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 20 * FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND E.FNEmpTypeGroup=FNListIndex ) L "
                _Qry &= vbCrLf & " WHERE E.FTStateActive = '1' AND E.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & " "
                _Qry &= vbCrLf & " GROUP BY FNEmpTypeGroup,L.FTNameTH "
                _Qry &= vbCrLf & " ORDER BY L.FTNameTH "

            Else
                _Qry = " SELECT     '0' AS [FTSelect] , E.FNEmpTypeGroup AS FNEmpTypeGroupID, L.FTNameEN AS 'FTEmpTypeGroup' "
                _Qry &= vbCrLf & ", MIN(D.FDCalDateBegin) AS 'FDCalDateBegin', MAX(D.FDCalDateEnd) As 'FDCalDateEnd', MAX(D.FDPayDate) AS 'FDPayDate' "

                _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType E ON H.FNHSysEmpTypeId= E.FNHSysEmpTypeId INNER JOIN "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm AND H.FTPayYear = D.FTPayYear AND "
                _Qry &= vbCrLf & " H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN "
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex "
                _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 20 * FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND E.FNEmpTypeGroup=FNListIndex ) L "
                _Qry &= vbCrLf & " WHERE E.FTStateActive = '1' AND E.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & " "
                _Qry &= vbCrLf & " GROUP BY FNEmpTypeGroup,L.FTNameEN "
                _Qry &= vbCrLf & " ORDER BY L.FTNameEN "
            End If




            _DtG = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Condition.ogdemptypegroup.DataSource = _DtG
        Catch ex As Exception

        End Try


    End Sub

    Private Sub BindDataEmptype()
        Try
            Dim _Qry As String = ""
            Dim _Dt As DataTable
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry = " SELECT '0' as [FTSelect], FTEmpTypeCode,FTEmpTypeNameTH AS FTDescription,FNHSysEmpTypeId ,  ET.FNEmpTypeGroup AS FNEmpTypeGroupID, L.FTNameTH AS 'FTEmpTypeGroup' "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType ET WITH ( NOLOCK ) "
                _Qry &= vbCrLf & "   OUTER APPLY (SELECT TOP 20 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND ET.FNEmpTypeGroup=FNListIndex ) L "
                _Qry &= vbCrLf & " WHERE  FTStateActive ='1'  AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                _Qry &= vbCrLf & " ORDER BY L.FTNameTH, FTEmpTypeCode "

            Else
                _Qry = " SELECT '0' as [FTSelect], FTEmpTypeCode,FTEmpTypeNameEN AS FTDescription,FNHSysEmpTypeId ,  ET.FNEmpTypeGroup AS FNEmpTypeGroupID, L.FTNameEN AS 'FTEmpTypeGroup' "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType ET WITH ( NOLOCK ) "
                _Qry &= vbCrLf & "   OUTER APPLY (SELECT TOP 20 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND ET.FNEmpTypeGroup=FNListIndex ) L "
                _Qry &= vbCrLf & " WHERE  FTStateActive ='1'  AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                _Qry &= vbCrLf & " ORDER BY L.FTNameEN, FTEmpTypeCode "
            End If



            'If FNEmpTypeGroup.SelectedIndex >= 0 Then
            '    _Qry &= vbCrLf & " AND  FNEmpTypeGroup =" & Val(FNEmpTypeGroup.SelectedIndex.ToString) & " "
            'End If

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Condition.ogdemptype.DataSource = _Dt
        Catch ex As Exception

        End Try


    End Sub

    Private Sub FNEmpTypeGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNEmpTypeGroup.SelectedIndexChanged
        Try

            'If FNEmpTypeGroup.SelectedIndex >= 0 Then
            '    Dim _Qry As String = ""
            '    Dim _Dt As DataTable


            '    _Qry = " SELECT '1' as [FTSelect], FTEmpTypeCode,FTEmpTypeNameEN AS FTDescription,FNHSysEmpTypeId  "
            '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType ET WITH ( NOLOCK ) "
            '    _Qry &= vbCrLf & " WHERE  FTStateActive ='1'  AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
            '    If FNEmpTypeGroup.SelectedIndex >= 0 Then
            '        _Qry &= vbCrLf & " AND  FNEmpTypeGroup =" & Val(FNEmpTypeGroup.SelectedIndex.ToString) & " "
            '    End If

            '    _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            '    Condition.ogdemptype.DataSource = _Dt
            '    '  Condition.ogdemptype.ExpandAllGroups()

            'End If


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With Condition.ogdemptypegroup
                If Not (.DataSource Is Nothing) And Condition.ogvemptypegroup.RowCount > 0 Then

                    With Condition.ogvemptypegroup
                        For I As Integer = 0 To .RowCount - 1
                            Call .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

            With Condition.ogdemptype
                If Not (.DataSource Is Nothing) And Condition.ogvemptype.RowCount > 0 Then

                    With Condition.ogvemptype
                        For I As Integer = 0 To .RowCount - 1
                            Call .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try




    End Sub


    'Private Sub ReposCheckEmpTypeGroup_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCheckEmpTypeGroup.EditValueChanging
    '    Try
    '        Dim dtT As DataTable
    '        Dim _Qry As String
    '        Dim _NewValue As String
    '        Dim _FNEmpTypeGroupID As Integer
    '        Dim _DtT As DataTable

    '        Dim _dt_c As DataTable
    '        ' Dim _Dt As DataTable

    '        _NewValue = e.NewValue.ToString()

    '        Dim _FTEmpTypeGroupName As String


    '        'With CType(ogvemptypeGroup.DataSource, DataTable)
    '        '    .AcceptChanges()
    '        '    _dt_c = .Copy()
    '        'End With

    '        With Condition.ogvemptypegroup

    '            _FTEmpTypeGroupName = .GetFocusedRowCellValue("FTEmpTypeGroup").ToString

    '        End With

    '        _FNEmpTypeGroupID = HI.TL.CboList.GetIndexByText("FNEmpTypeGroup", _FTEmpTypeGroupName)

    '        ''  Dim FNEmpTypeGroupID As String = ""
    '        'For Each R As DataRow In _dt_c.Select("FNEmpTypeGroupID=" & _FNEmpTypeGroupID)

    '        '    FNEmpTypeGroupID = R!FNEmpTypeGroupID.ToString


    '        'Next
    '        Dim _State As String = ""

    '        If _NewValue = "1" Then
    '            _State = "1"
    '        Else
    '            _State = "0"
    '        End If
    '        Dim a As String = ""
    '        Dim b As String = ""
    '        With Condition.ogvemptype
    '            If Not (.DataSource Is Nothing) And Condition.ogvemptype.RowCount > 0 Then

    '                With Condition.ogvemptype
    '                    For I As Integer = 0 To .RowCount - 1


    '                        a = .GetRowCellValue(I, .Columns.ColumnByFieldName("FTEmpTypeGroup"))
    '                        If a <> "" Then
    '                            b = a
    '                        End If
    '                        If (.GetRowCellValue(I, .Columns.ColumnByFieldName("FTEmpTypeGroup")) = _FTEmpTypeGroupName) Then
    '                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)

    '                            'ogcemptype_FTEmpTypeGroup'
    '                            'ogcemptype_FTEmpTypeGroup'
    '                        End If

    '                    Next
    '                End With

    '                CType(.DataSource, DataTable).AcceptChanges()
    '            End If

    '        End With

    '    Catch ex As Exception

    '    End Try


    'End Sub
End Class