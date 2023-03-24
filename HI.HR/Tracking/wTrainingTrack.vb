Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Views.Grid

Public Class wTrainingTrack
    Private dtTrainType As DataTable
    Private dtPurpose As DataTable
    Private _LoadForm As Boolean = True
    Private _CountTrainType As Integer = 0
    Private _CountRowAll As Integer = 0
    Private _HoldValue As String = ""
    Private _NetCost As Double
    Private dtDefault As DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitialGridTraining()
    End Sub

#Region "CreateGridBand ShowTraining"

    Private Sub InitialGridTraining()
        Call CreateColBand()
        Call CreateGridBand()
    End Sub

    Private Function GetDataTrainType() As DataTable
        Dim Qry As String
        Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GetListTypeTraining " & 0 & ""
        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
    End Function

    Private Function GetDataPurpose() As DataTable
        Dim Qry As String
        Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GetListTypeTraining " & 1 & ""
        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
    End Function

    Private Sub CreateColBand()
        Dim _ColN As Integer = 0
        'Dim dtTrainType As DataTable
        'Dim dtPurpose As DataTable
        Try
            With ogbv
                With .OptionsView
                    .AllowCellMerge = True
                    .ShowFooter = True
                    .ShowColumnHeaders = False
                End With
                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FTEmpCode"
                    .FieldName = "FTEmpCode"
                    .Caption = "FTEmpCode"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .SummaryItem.SummaryType = SummaryItemType.None
                    '.Width = 55
                    .Group()
                    .SortIndex = 0
                    .SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FTDocNo"
                    .FieldName = "FTDocNo"
                    .Caption = "FTDocNo"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowMerge = DevExpress.Utils.DefaultBoolean.True
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 140
                    _ColN += 1
                End With


                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "cFTTrainCode"
                    .FieldName = "cFTTrainCode"
                    .Caption = "cFTTrainCode"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowMerge = DevExpress.Utils.DefaultBoolean.True
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 130
                    _ColN += 1
                End With


                dtTrainType = GetDataTrainType()
                For Each R As DataRow In dtTrainType.Rows
                    .Columns.Add()
                    With .Columns(_ColN)
                        .Name = R!NameType.ToString
                        .FieldName = R!NameType.ToString
                        .Caption = R!NameType.ToString
                        .Visible = True
                        With .OptionsColumn
                            .AllowEdit = False
                            .AllowMove = False
                            .AllowMerge = DevExpress.Utils.DefaultBoolean.True
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .ReadOnly = True
                        End With
                        .Width = 75
                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
                        .SummaryItem.DisplayFormat = "{0:n2} %"
                        _ColN += 1
                    End With
                    Dim item1 As New GridGroupSummaryItem
                    item1.FieldName = R!NameType.ToString
                    item1.SummaryType = SummaryItemType.Custom
                    item1.DisplayFormat = "{0:n2} %"
                    item1.ShowInGroupColumnFooter = .Columns(R!NameType.ToString)
                    .GroupSummary.Add(item1)
                Next

                dtPurpose = GetDataPurpose()
                For Each X As DataRow In dtPurpose.Rows
                    .Columns.Add()
                    With .Columns(_ColN)
                        .Name = X!NameType.ToString
                        .FieldName = X!NameType.ToString
                        .Caption = X!NameType.ToString
                        .Visible = True
                        With .OptionsColumn
                            .AllowEdit = False
                            .AllowMove = False
                            .AllowMerge = DevExpress.Utils.DefaultBoolean.True
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .ReadOnly = True
                        End With
                        .Width = 75
                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
                        .SummaryItem.DisplayFormat = "{0:n2} %"
                        _ColN += 1
                    End With
                    Dim item2 As New GridGroupSummaryItem
                    item2.FieldName = X!NameType.ToString
                    item2.SummaryType = SummaryItemType.Custom
                    item2.DisplayFormat = "{0:n2} %"
                    item2.ShowInGroupColumnFooter = .Columns(X!NameType.ToString)
                    .GroupSummary.Add(item2)
                Next


                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "cFTTrainer"
                    .FieldName = "cFTTrainer"
                    .Caption = "cFTTrainer"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 250
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FDDateBegin"
                    .FieldName = "FDDateBegin"
                    .Caption = "FDDateBegin"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 90
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FDDateEnd"
                    .FieldName = "FDDateEnd"
                    .Caption = "FDDateEnd"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 90
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FTStartTime"
                    .FieldName = "FTStartTime"
                    .Caption = "FTStartTime"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 100
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FTEndTime"
                    .FieldName = "FTEndTime"
                    .Caption = "FTEndTime"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 100
                    _ColN += 1
                End With

                .Columns.Add()



                With .Columns(_ColN)
                    .Name = "cFTTotalHour"
                    .FieldName = "cFTTotalHour"
                    .Caption = "cFTTotalHour"
                    .Visible = True
                    .Width = 55
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
                    .SummaryItem.DisplayFormat = "{0:n2}"
                    .Width = 100
                    _ColN += 1
                End With
                Dim item4 As New GridGroupSummaryItem
                item4.FieldName = "cFTTotalHour"
                item4.SummaryType = SummaryItemType.Custom
                item4.DisplayFormat = "{0:n2}"
                item4.ShowInGroupColumnFooter = .Columns("cFTTotalHour")
                .GroupSummary.Add(item4)
                .Columns.Add()

                With .Columns(_ColN)
                    .Name = "FTLocation"
                    .FieldName = "FTLocation"
                    .Caption = "FTLocation"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 270
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FTEvaluate"
                    .FieldName = "FTEvaluate"
                    .Caption = "FTEvaluate"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With

                    .Width = 80
                    _ColN += 1
                End With

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "FCCostPerEmp"
                    .FieldName = "FCCostPerEmp"
                    .Caption = "FCCostPerEmp"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "{0:n2}"
                    .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
                    .SummaryItem.DisplayFormat = "{0:n2}"
                    .Width = 80
                    _ColN += 1
                End With
                Dim item3 As New GridGroupSummaryItem
                item3.FieldName = "FCCostPerEmp"
                item3.SummaryType = SummaryItemType.Custom
                item3.DisplayFormat = "{0:n2}"
                item3.ShowInGroupColumnFooter = .Columns("FCCostPerEmp")
                .GroupSummary.Add(item3)

                .Columns.Add()
                With .Columns(_ColN)
                    .Name = "cFTTrainNote"
                    .FieldName = "cFTTrainNote"
                    .Caption = "cFTTrainNote"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowMerge = DevExpress.Utils.DefaultBoolean.True
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 260
                    _ColN += 1
                End With
                .ExpandAllGroups()

            End With



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub CreateGridBand()
        Dim _StateCreGridHeadFTT As Boolean = False
        Dim _StateCreGridHeadFix As Boolean = False
        Dim _StateChilFTT As Boolean = False
        Dim _StateChilFix As Boolean = False
        Try
            'With ogb
            For i As Integer = ogbv.Bands.Count - 1 To 0 Step -1
                ogbv.Bands.RemoveAt(i)
            Next
            For Each _item As GridColumn In ogbv.Columns
                Dim ogbHead As New GridBand
                With ogbHead
                    Select Case Microsoft.VisualBasic.Left(_item.FieldName.ToString, 3)
                        Case "FTT"
                            If Not (_StateCreGridHeadFTT) Then
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = "gbHeadFTTType"
                                .Name = "gbHeadFTTType"
                                .RowCount = 1
                                .Visible = True
                                ogbv.Bands.Add(ogbHead)
                                _StateCreGridHeadFTT = True
                            End If
                            If Not (_StateChilFTT) Then
                                For Each K As DataRow In dtTrainType.Rows
                                    Dim ogbChilFTT As New GridBand
                                    With ogbChilFTT
                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = K!NameType.ToString
                                        .Columns.Add(ogbv.Columns.ColumnByFieldName(K!NameType.ToString))
                                        .Name = "gbChil" & K!NameType.ToString
                                        .RowCount = 1
                                        .Visible = True
                                    End With
                                    ogbHead.Children.Add(ogbChilFTT)
                                Next
                                _StateChilFTT = True
                            End If

                        Case "Fix"

                            If Not (_StateCreGridHeadFix) Then
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = "gbHeadFix"
                                .Name = "gbHeadFix"
                                .RowCount = 1
                                .Visible = True
                                ogbv.Bands.Add(ogbHead)
                                _StateCreGridHeadFix = True
                            End If

                            If Not (_StateChilFix) Then
                                For Each O As DataRow In dtPurpose.Rows
                                    Dim ogbChilFix As New GridBand
                                    With ogbChilFix
                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = O!NameType.ToString
                                        .Columns.Add(ogbv.Columns.ColumnByFieldName(O!NameType.ToString))
                                        .Name = "gbChil" & O!NameType.ToString
                                        .RowCount = 1
                                        .Visible = True
                                    End With
                                    ogbHead.Children.Add(ogbChilFix)
                                Next
                                _StateChilFix = True
                            End If


                        Case Else
                            If _item.FieldName <> "FTEmpCode" Then
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = _item.FieldName.ToString
                                .Columns.Add(ogbv.Columns.ColumnByFieldName(_item.FieldName.ToString))
                                .Name = "gbHead" & _item.FieldName.ToString
                                .RowCount = 2
                                .Visible = True
                                ogbv.Bands.Add(ogbHead)
                            End If
                    End Select
                End With

            Next


        Catch ex As Exception

        End Try

    End Sub

#End Region

    Private Sub Loaddata()
        Dim _Qry As String = ""

        _Qry = "select X.FTEmpCode AS FTEmpCode,T.FTDocNo,T.FTTrainCode AS cFTTrainCode,T.FCCostPerEmp"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",case when TT.FTNameTH= 'ภายใน' then TT.FTNameTH else ''  end AS FTTypeIN"
            _Qry &= vbCrLf & ",case when TT.FTNameTH= 'ภายนอก' then TT.FTNameTH else '' end AS FTTypeOUT"
            _Qry &= vbCrLf & ",case when TT.FTNameTH= 'เพิ่มทักษะ' then TT.FTNameTH else '' end  AS FTTypeUP"
            _Qry &= vbCrLf & ",case when TF.FTNameTH= 'Hard Skill' then TF.FTNameTH else '' end AS FixHard"
            _Qry &= vbCrLf & ",case when TF.FTNameTH= 'Soft Skill' then TF.FTNameTH else '' end AS FixSoft"
            _Qry &= vbCrLf & ",case when TF.FTNameTH= 'ตามกฎหมายกำหนด' then TF.FTNameTH else '' end AS FixLegal"
            _Qry &= vbCrLf & ",case when TF.FTNameTH= 'ทบทวนประจำปี' then TF.FTNameTH else '' end AS FixAnnual"
            _Qry &= vbCrLf & ",case when TF.FTNameTH= 'ปฐมนิเทศ' then TF.FTNameTH else '' end AS FixOrein"
            _Qry &= vbCrLf & ",case when TF.FTNameTH= 'อื่นๆ' then TF.FTNameTH else '' end AS FixOther"
            _Qry &= vbCrLf & ",TEE.FTNameTH AS FTEvaluate"
        Else
            _Qry &= vbCrLf & ",case when TT.FTNameEN= 'In House Training' then TT.FTNameEN else ''  end AS FTTypeIN"
            _Qry &= vbCrLf & ",case when TT.FTNameEN= 'Public Training' then TT.FTNameEN else '' end AS FTTypeOUT"
            _Qry &= vbCrLf & ",case when TT.FTNameEN= 'Up Skilling' then TT.FTNameEN else '' end  AS FTTypeUP"
            _Qry &= vbCrLf & ",case when TF.FTNameEN= 'Hard Skill' then TF.FTNameEN else '' end AS FixHard"
            _Qry &= vbCrLf & ",case when TF.FTNameEN= 'Soft Skill' then TF.FTNameEN else '' end AS FixSoft"
            _Qry &= vbCrLf & ",case when TF.FTNameEN= 'Legal Provision' then TF.FTNameEN else '' end AS FixLegal"
            _Qry &= vbCrLf & ",case when TF.FTNameEN= 'Annual Review' then TF.FTNameEN else '' end AS FixAnnual"
            _Qry &= vbCrLf & ",case when TF.FTNameEN= 'Oreintation' then TF.FTNameEN else '' end AS FixOrein"
            _Qry &= vbCrLf & ",case when TF.FTNameEN= 'Other' then TF.FTNameEN else '' end AS FixOther"
            _Qry &= vbCrLf & ",TEE.FTNameEN AS FTEvaluate"
        End If
        _Qry &= vbCrLf & ",case when isnull(TL.FTTrainer,'')= '' then T.FTTrainer else TL.FTTrainer end AS cFTTrainer"
        _Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,T.FDDateBegin),103) AS FDDateBegin"

        _Qry &= vbCrLf & ",convert(varchar(10),convert(datetime,T.FDDateEnd),103) AS FDDateEnd"
        _Qry &= vbCrLf & ",T.FTStartTime,T.FTEndTime"
        _Qry &= vbCrLf & ",T.FTTotalHour AS cFTTotalHour"
        _Qry &= vbCrLf & ",t.FTLocation,Case When T.FTTrainNote<>'' then T.FTTrainNote else TE.FTTrainNote end AS cFTTrainNote"
        _Qry &= vbCrLf & ",X.FNHSysSectId"
        _Qry &= vbCrLf & " from"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainEmp AS TE WItH(NOLOCK)  LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrain AS T WITH(NOLOCK) ON T.FTDocNo=TE.FTDocNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrainLecturer AS TL WItH(NOLOCK) ON T.FTDocNo=TL.FTDocNo LEFT OUtER JOIN"
        _Qry &= vbCrLf & "(select E.FNHSysEmpID,U.FNHSysUnitSectId,E.FTEmpCode"
        _Qry &= vbCrLf & ",S.FNHSysSectId,D.FNHSysDeptId, E.FNHSysCmpId"
        _Qry &= vbCrLf & "from"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITh(NOLOCK) LEFT OUtER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON E.FNHSysUnitSectId=U.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON E.FNHSysSectId=S.FNHSysSectId LEFT OUTER jOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WiTH(NOLOCK) ON E.FNHSysDeptId=D.FNHSysDeptId) AS X ON TE.FNHSysEmpID=X.FNHSysEmpID"
        _Qry &= vbCrLf & "LEFT OUtER jOIN"
        _Qry &= vbCrLf & "(select A.FNListIndex,A.FTListName,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "WHERE A.FTListName='FNTrainType') AS TT ON T.FNTrainType=TT.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FNListIndex,A.FTListName,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "WHERE A.FTListName='FNFixTrainTrainning') AS TF ON T.FNFixTrainTrainning=TF.FNListIndex  LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FNListIndex,A.FTListName,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "WHERE A.FTListName='TrainEvaluate') AS TEE ON TE.FTEvaluate=TEE.FNListIndex  "
        _Qry &= vbCrLf & "where TE.FNHSysEmpID<>0"
        If Me.FNHSysEmpID.Text <> "" Then
            _Qry &= vbCrLf & "and X.FTEmpCode>= '" & Me.FNHSysEmpID.Text & "'"
        End If
        If Me.FNHSysEmpIDTo.Text <> "" Then
            _Qry &= vbCrLf & "and X.FTEmpCode<= '" & Me.FNHSysEmpIDTo.Text & "'"
        End If

        If Me.FNHSysDeptId.Text <> "" Then
            _Qry &= vbCrLf & "and X.FNHSysDeptId>=" & Me.FNHSysDeptId.Properties.Tag & ""
        End If
        If Me.FNHSysDeptIdTo.Text <> "" Then
            _Qry &= vbCrLf & "and X.FNHSysDeptId<= " & Me.FNHSysDeptIdTo.Properties.Tag & ""
        End If
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & "and X.FNHSysSectId>= " & Me.FNHSysSectId.Properties.Tag & ""
        End If
        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & "and X.FNHSysSectId<= " & Me.FNHSysSectIdTo.Properties.Tag & ""
        End If
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & "and X.FNHSysUnitSectId>= " & Me.FNHSysUnitSectId.Properties.Tag & ""
        End If
        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & "and X.FNHSysUnitSectId<= " & Me.FNHSysUnitSectIdTo.Properties.Tag & ""
        End If
        If Me.FTStartDateOp.Text <> "" Then
            _Qry &= vbCrLf & "and T.FDDateBegin>= '" & UL.ULDate.ConvertEnDB(Me.FTStartDateOp.Text) & "'"

        End If
        If Me.FTEndDateOp.Text <> "" Then
            _Qry &= vbCrLf & "and T.FDDateEnd<= '" & UL.ULDate.ConvertEnDB(Me.FTEndDateOp.Text) & "'"
        End If

        _Qry &= vbCrLf & " AND X.FNHSysCmpId = '" & Val(HI.ST.SysInfo.CmpID) & "' "

        _Qry &= vbCrLf & "order by T.FDDateBegin"


        dtDefault = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogctrain.DataSource = dtDefault.Copy


    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call Loaddata()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        TL.HandlerControl.ClearControl(Me)
        'With ogb
        '    With .OptionsView
        '        .ShowFooter = False
        '        .ShowColumnHeaders = False
        '    End With
        'End With

    End Sub

    Private Sub ocmcaldate_Click(sender As Object, e As EventArgs) Handles ocmcaldate.Click
        Dim _RowCount As Integer = 0

        If FTStartDateOp.Text = "" Then
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            Me.FTStartDateOp.Focus()
            Exit Sub
        End If

        Try
            If Me.FTStartDateOp.Text <> "" And Me.FTEndDateOp.Text = "" Then
                For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
                    If CDate(R!FDDateBegin.ToString) < CDate(Me.FTStartDateOp.Text) Then
                        CType(ogctrain.DataSource, DataTable).BeginInit()
                        CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
                        CType(ogctrain.DataSource, DataTable).EndInit()
                    End If
                    _RowCount += 1
                Next
                CType(ogctrain.DataSource, DataTable).AcceptChanges()
                If CType(ogctrain.DataSource, DataTable).Rows.Count > 0 Then
                Else
                    MG.ShowMsg.mInfo("ไม่สามารถคำนวณได้ เนื่องจากไม่มีข้อมูลตามเงื่อนไขที่กำหนด", 201701071006, Me.Text, "", MessageBoxIcon.Information)
                    'Call ShowTrainning(Me.FNHSysEmpID.Properties.Tag)
                    Me.ogctrain.DataSource = dtDefault.Copy
                End If
            Else
                For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
                    If CDate(R!FDDateBegin.ToString) < CDate(Me.FTStartDateOp.Text) Then
                        CType(ogctrain.DataSource, DataTable).BeginInit()
                        CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
                        CType(ogctrain.DataSource, DataTable).EndInit()
                    End If
                    _RowCount += 1
                Next
                CType(ogctrain.DataSource, DataTable).AcceptChanges()
                _RowCount = 0
                For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
                    If CDate(R!FDDateBegin.ToString) > CDate(Me.FTEndDateOp.Text) Then
                        CType(ogctrain.DataSource, DataTable).BeginInit()
                        CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
                        CType(ogctrain.DataSource, DataTable).EndInit()
                    End If
                    _RowCount += 1
                Next
                CType(ogctrain.DataSource, DataTable).AcceptChanges()
                If CType(ogctrain.DataSource, DataTable).Rows.Count > 0 Then
                Else
                    MG.ShowMsg.mInfo("ไม่สามารถคำนวณได้ เนื่องจากไม่มีข้อมูลตามเงื่อนไขที่กำหนด", 201701071006, Me.Text, "", MessageBoxIcon.Information)
                    'Call ShowTrainning(Me.FNHSysEmpID.Properties.Tag)
                    Me.ogctrain.DataSource = dtDefault.Copy
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FNOptionCal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOptionCal.SelectedIndexChanged
        Me.ocmcaldate.Enabled = True
        If _LoadForm = False Then Exit Sub
        Dim _RowCount As Integer = 0
        Dim _GetCurDate As String

        Try
            If (CType(ogctrain.DataSource, DataTable) Is Nothing) Then
                Select Case FNOptionCal.SelectedIndex
                    Case 2
                        Me.FTStartDateOp.Enabled = True : Me.FTEndDateOp.Enabled = True : Me.ocmcaldate.Enabled = True

                    Case Else
                        Me.FTStartDateOp.Enabled = False : Me.FTEndDateOp.Enabled = False : Me.ocmcaldate.Enabled = False
                        Me.FTStartDateOp.Text = "" : Me.FTEndDateOp.Text = ""
                End Select
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try
            If Not (CType(ogctrain.DataSource, DataTable) Is Nothing) Then
                Select Case FNOptionCal.SelectedIndex
                    Case 2
                        Me.FTStartDateOp.Enabled = True : Me.FTEndDateOp.Enabled = True : Me.ocmcaldate.Enabled = True

                    Case Else
                        Me.FTStartDateOp.Enabled = False : Me.FTEndDateOp.Enabled = False : Me.ocmcaldate.Enabled = False
                        Me.FTStartDateOp.Text = "" : Me.FTEndDateOp.Text = ""
                End Select

                If Me.FNOptionCal.SelectedIndex = 0 Then
                    'Call ShowTrainning(Me.FNHSysEmpID.Properties.Tag)
                    Me.ogctrain.DataSource = dtDefault.Copy
                ElseIf Me.FNOptionCal.SelectedIndex = 1 Then
                    _GetCurDate = HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS DateCal", Conn.DB.DataBaseName.DB_HR)
                    For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
                        If Microsoft.VisualBasic.Left(UL.ULDate.ConvertEnDB(R!FDDateBegin.ToString), 4) <> Microsoft.VisualBasic.Left(_GetCurDate, 4) Then
                            CType(ogctrain.DataSource, DataTable).BeginInit()
                            CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
                            CType(ogctrain.DataSource, DataTable).EndInit()
                        End If
                        _RowCount += 1
                    Next
                    CType(ogctrain.DataSource, DataTable).AcceptChanges()
                    If CType(ogctrain.DataSource, DataTable).Rows.Count > 0 Then
                    Else
                        MG.ShowMsg.mInfo("ไม่สามารถคำนวณได้ เนื่องจากไม่มีข้อมูลตามเงื่อนไขที่กำหนด", 201701071006, Me.Text, "", MessageBoxIcon.Information)
                        Me.ogctrain.DataSource = dtDefault.Copy
                        Me.FNOptionCal.SelectedIndex = 0
                    End If
                End If
            End If
            Me.ocmcaldate.Enabled = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub wTrainingTrack_Load(sender As Object, e As EventArgs) Handles Me.Load
        _LoadForm = True
    End Sub

    Private Sub ogbv_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogbv.CellMerge
        Try
            With ogbv
                Select Case e.Column.FieldName
                    Case "FTEvaluate"
                        If .GetRowCellValue(e.RowHandle1, "cFTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "cFTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTStartTime"
                        If .GetRowCellValue(e.RowHandle1, "FTDocNo").ToString = .GetRowCellValue(e.RowHandle2, "FTDocNo") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            '  e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTEndTime"
                        If .GetRowCellValue(e.RowHandle1, "FTDocNo").ToString = .GetRowCellValue(e.RowHandle2, "FTDocNo") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            '  e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "cFTTotalHour"
                        If .GetRowCellValue(e.RowHandle1, "FTDocNo").ToString = .GetRowCellValue(e.RowHandle2, "FTDocNo") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            '  e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FCCostPerEmp"
                        If .GetRowCellValue(e.RowHandle1, "cFTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "cFTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "cFTTrainNote"
                        If .GetRowCellValue(e.RowHandle1, "cFTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "cFTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "cFTTrainer"
                        If .GetRowCellValue(e.RowHandle1, "cFTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "cFTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else
                        For Each R As DataRow In GetDataTrainType.Select("NameType='" & e.Column.FieldName & "'")
                            If .GetRowCellValue(e.RowHandle1, "cFTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "cFTTrainCode") Then
                                e.Merge = (e.CellValue1 = e.CellValue2)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If
                        Next
                        For Each R As DataRow In GetDataPurpose.Select("NameType='" & e.Column.FieldName & "'")
                            If .GetRowCellValue(e.RowHandle1, "cFTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "cFTTrainCode") Then
                                e.Merge = (e.CellValue1 = e.CellValue2)
                                e.Handled = True
                                e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            Else
                                e.Merge = False
                                e.Handled = True
                            End If
                        Next
                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub InitSummaryStartValue()
        _CountTrainType = 0
        _CountRowAll = 0
        _HoldValue = ""
        _NetCost = 0
    End Sub

    Private Sub ogbv_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles ogbv.CustomSummaryCalculate
        Dim _Number As Double
        Dim _valcost As Double
        Try

            If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
                InitSummaryStartValue()
                InitStartValue()
            End If


            With ogbv
                If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                    '_HoldValue = .GetRowCellValue(e.RowHandle, CType(e.Item, GridSummaryItem).FieldName)
                    If _HoldValue <> .GetRowCellValue(e.RowHandle, "FTDocNo") Then
                        _HoldValue = .GetRowCellValue(e.RowHandle, "FTDocNo")
                        _CountRowAll += 1
                        _NetCost += .GetRowCellValue(e.RowHandle, "FCCostPerEmp")

                        Dim Seq As Integer = 1
                        For Each Str As String In .GetRowCellValue(e.RowHandle, "cFTTotalHour").ToString.Split(":")
                            Select Case Seq
                                Case 1
                                    totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
                                Case Else
                                    totalSum = totalSum + Integer.Parse(Val(Str))
                            End Select
                            Seq = Seq + 1
                        Next
                        If .GetRowCellValue(e.RowHandle, CType(e.Item, GridSummaryItem).FieldName) <> "" Then
                            _CountTrainType += 1
                        End If
                    End If




                    If CType(e.Item, GridSummaryItem).FieldName <> "FCCostPerEmp" Then
                        If CType(e.Item, GridSummaryItem).FieldName <> "cFTTotalHour" Then
                            _Number = (_CountTrainType * 100) / _CountRowAll
                            If _Number > 0 Then
                                e.TotalValue = _Number
                            Else
                                e.TotalValue = 0.00
                            End If
                        Else
                            Dim NetDisplay As String = ""
                            NetDisplay = Format(((totalSum) \ 60), "00") & " ช. " & Format(((totalSum) Mod 60), "00" & " น.")
                            e.TotalValue = NetDisplay
                        End If
                    Else
                        e.TotalValue = _NetCost
                    End If
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub




End Class