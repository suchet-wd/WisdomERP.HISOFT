Imports System.Windows.Forms

Public Class wMEDTreat

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MEDC
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()


    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _AddItemPopup As wMECRemedyPopup
    Private _AddItemAccident As wAddAccident
    Private _AddItemConsul As wAddConsul

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _AddItemPopup = New wMECRemedyPopup
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        End Try

        _AddItemAccident = New wAddAccident
        HI.TL.HandlerControl.AddHandlerObj(_AddItemAccident)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemAccident.Name.ToString.Trim, _AddItemAccident)
        Catch ex As Exception
        End Try

        _AddItemConsul = New wAddConsul
        HI.TL.HandlerControl.AddHandlerObj(_AddItemConsul)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemConsul.Name.ToString.Trim, _AddItemConsul)
        Catch ex As Exception
        End Try

        Call InitGrid()

        Me.FNHSysCmpId.Properties.Tag = HI.ST.SysInfo.CmpID

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNLiveMin"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""
        Dim _gv As Object


        With ogvgeneral

            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True


        End With

        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Main"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wMEDTreat_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNHSysEmpID_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysEmpID.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysEmpID.Text <> "" Then
                    FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysEmpID  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' ", Conn.DB.DataBaseName.DB_HR, "")
                    Call LoadEmpInfo(FNHSysEmpID.Properties.Tag.ToString)
                Else
                    ogcgeneral.DataSource = Nothing


                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadEmpInfo(ByVal FNHSysEmpID As String)
        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")


        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode , H.FNHSysBldId , Isnull(B.FTBldCode, BB.FTBldCode) AS  FTBldCode "
        _Qry &= vbCrLf & ", Isnull(H.FCGenWeight,M.FCWeight) AS FCGenWeight, Isnull(H.FCGenHigh,M.FCHeight) AS FCGenHigh  , HH.FTHospitalCode "
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", HH.FTHospitalNameTH AS FTHospitalName"
        Else
            _Qry &= vbCrLf & ", HH.FTHospitalNameEN AS FTHospitalName"
        End If

        _Qry &= vbCrLf & "  FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHospital AS HH WITH(NOLOCK) ON M.FNHSysHospitalId = HH.FNHSysHospitalId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN THRTEmployeeHealthHistory AS H  WITH(NOLOCK)  ON M.FNHSysEmpID = H.FNHSysEmpID "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMBlood AS B WITH(NOLOCK) ON H.FNHSysBldId = B.FNHSysBldId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN (Select * From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMBlood  WITH(NOLOCK) ) AS BB  ON M.FNHSysBldId = BB.FNHSysBldId "
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString
                FCGenWeight.Value = Double.Parse(R!FCGenWeight.ToString)
                FCGenHigh.Value = Double.Parse(R!FCGenHigh.ToString)
                FNHSysBldId.Text = R!FTBldCode.ToString
                FNHSysHospitalId.Text = R!FTHospitalCode.ToString


            Next
        Else
            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""
            FCGenWeight.Value = 0
            FCGenHigh.Value = 0
            FNHSysBldId.Text = ""
            FNHSysHospitalId.Text = ""
        End If

        Call LoadDataGeneral()
        Call LoadAccident()
        Call LoadDataConsul()
        'Call LoadHealthHistoryExpense()

        Me.otbDetail.SelectedTabPageIndex = 0
    End Sub

    

#End Region

#Region "General"
    Private Sub LoadDataGeneral()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT     G.FNHSysMECGenId, G.FNHSysEmpId, G.FTMECTime, G.FTMECOutTime, G.FNLiveMin, G.FNHSysTypeofDiseaseId, G.FNCauseType, G.FTSymptom, G.FTRemedy, G.FNHSysOpinionId, T.FTTypeofDiseaseCode  "
            _Cmd &= vbCrLf & "     , O.FTOpinionCode" ' , P.FNQuantity
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",T.FTTypeofDiseaseNameTH AS FTTypeofDiseaseName    , O.FTOpinionNameTH as FTOpinionName , L.FTNameTH AS FNCauseDesc "
            Else
                _Cmd &= vbCrLf & ",T.FTTypeofDiseaseNameEN AS FTTypeofDiseaseName    , O.FTOpinionNameEN  as FTOpinionName ,L.FTNameEN AS FNCauseDesc "
            End If
            _Cmd &= vbCrLf & ",CASE WHEN Isdate(G.FDMECDate) = 1 Then convert(nvarchar(10),convert(datetime,G.FDMECDate),103) ELSE '' END AS FDMECDate "
            _Cmd &= vbCrLf & ",dbo.FN_GET_DrugPay(G.FNHSysMECGenId,'0') AS FTDrugCode "

            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral AS G WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay AS P WITH (NOLOCK) ON G.FNHSysMECGenId = P.FNHSysMECGenId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug AS D WITH (NOLOCK) ON P.FNHSysDrugId = D.FNHSysDrugId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMOpinion AS O WITH (NOLOCK) ON G.FNHSysOpinionId = O.FNHSysOpinionId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMTypeofDisease AS T WITH (NOLOCK) ON G.FNHSysTypeofDiseaseId = T.FNHSysTypeofDiseaseId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN (Select  FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & " WHERE     (FTListName = 'FNCauseType') ) AS L ON G.FNCauseType = L.FNListIndex  "
            _Cmd &= vbCrLf & "WHERE G.FNHSysEmpId =" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

            Me.ogcgeneral.DataSource = _oDt

            'Dim dt As DataTable

            'With CType(ogcgeneral.DataSource, DataTable)
            '    .AcceptChanges()
            '    dt = .Copy
            'End With

            'For Each R As DataRow In dt.Rows
            '    Dim _FDMECDate As String = R.Item("FDMECDate")
            '    Dim _FTMECTime As String = R.Item("FTMECTime")
            '    Dim _FTMECOutTime As String = R.Item("FTMECOutTime")
            '    Dim TimeLive As Integer = DateDiff(DateInterval.Minute, CDate(_FDMECDate & " " & _FTMECTime), CDate(_FDMECDate & " " & _FTMECOutTime))
            '    R!FNTimeLive = TimeLive
            '    dt.Columns.Add(TimeLive)
            'Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _DocRunId As Integer = HI.TL.RunID.GetRunNoID("TMECTGeneral", " FNHSysMECGenId", Conn.DB.DataBaseName.DB_MEDC)
            HI.TL.HandlerControl.ClearControl(_AddItemPopup)
            With _AddItemPopup
                ._Proc = False
                ._MECGenId = _DocRunId
                ._FTStateGen = "0"
                .FDDate.DateTime = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .FTTime.Time = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .FTOutTime.Time = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .ShowDialog()

                If ._Proc = False Then
                    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay"
                    _Cmd &= vbCrLf & "WHERE FNHSysMECGenId=" & Integer.Parse(_DocRunId)
                    _Cmd &= vbCrLf & "and Isnull(FTStateGen,'0') = '0'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                    Exit Sub
                End If

                Dim _TimeLive As Integer = DateDiff(DateInterval.Minute, .FTTime.Time, .FTOutTime.Time)

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral "
                _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysMECGenId, FNHSysEmpId, FDMECDate, FTMECTime, FTMECOutTime, FNLiveMin, FNHSysTypeofDiseaseId, FNCauseType, FTSymptom, FTRemedy, FNHSysOpinionId)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & Integer.Parse(_DocRunId)
                _Cmd &= vbCrLf & "," & Me.FNHSysEmpID.Properties.Tag
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(.FDDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & .FTTime.Text & "'"
                _Cmd &= vbCrLf & ",'" & .FTOutTime.Text & "'"
                _Cmd &= vbCrLf & "," & _TimeLive & ""
                _Cmd &= vbCrLf & "," & .FNHSysTypeofDiseaseId.Properties.Tag
                _Cmd &= vbCrLf & "," & .FNCauseType.SelectedIndex
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSymptom.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemedy.Text) & "'"
                _Cmd &= vbCrLf & "," & .FNHSysOpinionId.Properties.Tag
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '        HI.Conn.SQLConn.Tran.Rollback()
                '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '        Exit Sub
                '    End If
                '    End If

                '    HI.Conn.SQLConn.Tran.Commit()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            End With

            LoadDataGeneral()

        Catch ex As Exception
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        End Try
    End Sub

    Private Sub ogvgeneral_DoubleClick(sender As Object, e As EventArgs) Handles ogvgeneral.DoubleClick
        Try
            Dim _Cmd As String = ""
            Dim _STime As String = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _SDate As String = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
            With ogvgeneral
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _FNHSysMECGenId As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysMECGenId").ToString)
                Dim _FTTypeofDiseaseCode As String = .GetRowCellValue(.FocusedRowHandle, "FTTypeofDiseaseCode").ToString
                Dim _FTOpinionCode As String = .GetRowCellValue(.FocusedRowHandle, "FTOpinionCode").ToString
                Dim _FNCauseType As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNCauseType").ToString)
                Dim _FTSymptom As String = .GetRowCellValue(.FocusedRowHandle, "FTSymptom").ToString
                Dim _FTRemedy As String = .GetRowCellValue(.FocusedRowHandle, "FTRemedy").ToString
                Dim _FDMECDate As String = .GetRowCellValue(.FocusedRowHandle, "FDMECDate").ToString
                Dim _FTMECTime As String = .GetRowCellValue(.FocusedRowHandle, "FTMECTime").ToString
                Dim _FTMECOutTime As String = .GetRowCellValue(.FocusedRowHandle, "FTMECOutTime").ToString

                With _AddItemPopup
                    ._MECGenId = _FNHSysMECGenId
                    ._FTStateGen = "0"
                    .FNHSysTypeofDiseaseId.Text = _FTTypeofDiseaseCode
                    .FNHSysOpinionId.Text = _FTOpinionCode
                    .FNCauseType.SelectedIndex = _FNCauseType
                    .FTSymptom.Text = _FTSymptom
                    .FTRemedy.Text = _FTRemedy
                    .FDDate.Text = _FDMECDate
                    .FTTime.Time = _FTMECTime
                    If (_FTMECOutTime = "") Then
                        .FTOutTime.Time = _FTMECTime
                    Else
                        .FTOutTime.Time = _FTMECOutTime
                    End If

                    ._Proc = False
                    .ShowDialog()
                    If ._Proc = False Then
                        _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay"
                        _Cmd &= vbCrLf & "WHERE FNHSysMECGenId=" & Integer.Parse(._MECGenId)
                        _Cmd &= vbCrLf & "and FDInsDate >'" & _SDate & "'"
                        _Cmd &= vbCrLf & "and FTInsTime >'" & _STime & "'"
                        _Cmd &= vbCrLf & "and Isnull(FTStateGen,'0') = '0'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                        Exit Sub
                    End If

                    Dim _TimeLive As Integer = DateDiff(DateInterval.Minute, .FTTime.Time, .FTOutTime.Time)


                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral "
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FDMECDate='" & HI.UL.ULDate.ConvertEnDB(.FDDate.Text) & "'"
                    _Cmd &= vbCrLf & ",FTMECTime='" & .FTTime.Text & "'"
                    _Cmd &= vbCrLf & ",FTMECOutTime='" & .FTOutTime.Text & "'"
                    _Cmd &= vbCrLf & ",FNLiveMin=" & _TimeLive & ""
                    _Cmd &= vbCrLf & ",FNHSysTypeofDiseaseId=" & .FNHSysTypeofDiseaseId.Properties.Tag
                    _Cmd &= vbCrLf & ",FNCauseType=" & .FNCauseType.SelectedIndex
                    _Cmd &= vbCrLf & ",FTSymptom='" & HI.UL.ULF.rpQuoted(.FTSymptom.Text) & "'"
                    _Cmd &= vbCrLf & ",FTRemedy='" & HI.UL.ULF.rpQuoted(.FTRemedy.Text) & "'"
                    _Cmd &= vbCrLf & ",FNHSysOpinionId=" & .FNHSysOpinionId.Properties.Tag
                    _Cmd &= vbCrLf & "WHERE FNHSysMECGenId=" & Integer.Parse(._MECGenId)
                    _Cmd &= vbCrLf & "AND FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                End With
            End With
            LoadDataGeneral()
        Catch ex As Exception

        End Try
    End Sub


    Private Function DeleteGeneral() As Boolean
        Try
            Dim _Cmd As String = ""
            With ogvgeneral
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Return False
                Dim _MECGenId As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysMECGenId").ToString)
                _Cmd = "Delete From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral "
                _Cmd &= vbCrLf & "WHERE FNHSysMECGenId=" & Integer.Parse(_MECGenId)
                _Cmd &= vbCrLf & "AND FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay "
                _Cmd &= vbCrLf & "WHERE FNHSysMECGenId =" & Integer.Parse(_MECGenId)
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub ogvgeneral_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvgeneral.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If Me.DeleteGeneral Then
                    Call LoadDataGeneral()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Accident"

    Private Sub LoadAccident()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            
            _Cmd = "  SELECT     A.FNHSysAccidentId, A.FNAccidentType, A.FTTime, A.FTAccidentDesc, A.FTLocal, A.FTOrgans, A.FTSymptom, A.FTTreatment, A.FNHSysOpinionId, A.FNStopWorkDay, O.FTOpinionCode "
            _Cmd &= vbCrLf & ", CASE WHEN Isdate(A.FDDate) = 1 Then convert(nvarchar(10),convert(datetime,A.FDDate),103) Else '' END  As FDDate"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",O.FTOpinionNameTH as FTOpinionName, L.FTNameTH as FTName "
            Else
                _Cmd &= vbCrLf & ",O.FTOpinionNameEN as FTOpinionName, L.FTNameEN as FTName "
            End If
            _Cmd &= vbCrLf & ",dbo.FN_GET_DrugPay(A.FNHSysAccidentId ,'1') AS FTDrugCode"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTAccident AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMOpinion AS O WITH (NOLOCK) ON A.FNHSysOpinionId = O.FNHSysOpinionId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  (SELECT     FTListName, FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & "   WHERE      (FTListName = 'FNAccidentType')) AS L ON A.FNAccidentType = L.FNListIndex"
            _Cmd &= vbCrLf & " WHERE  A.FNHSysEmpId =" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

            Me.ogcAccident.DataSource = _oDt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmaddAccident_Click(sender As Object, e As EventArgs)
        Try
            Dim _Cmd As String = ""

            Dim _DocRunId As Integer = HI.TL.RunID.GetRunNoID("TMECTAccident", " FNHSysAccidentId", Conn.DB.DataBaseName.DB_MEDC)


            HI.TL.HandlerControl.ClearControl(_AddItemAccident)

            With _AddItemAccident
                ._Proc = False
                ._AccidentId = _DocRunId
                ._FTStateGen = "1"
                .FDDate.DateTime = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .FTTime.Time = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .ShowDialog()

                If Not (._Proc) Then
                    _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay"
                    _Cmd &= vbCrLf & "WHERE FNHSysMECGenId=" & Integer.Parse(_DocRunId)
                    _Cmd &= vbCrLf & "AND Isnull(FTStateGen,'0') ='1'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                    Exit Sub
                End If

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTAccident"
                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FNHSysAccidentId, FNAccidentType, FDDate, FTTime, FTAccidentDesc, FTLocal, FTOrgans, FTSymptom, FTTreatment, FNHSysOpinionId, FNStopWorkDay,FNHSysEmpId)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & ._AccidentId
                _Cmd &= vbCrLf & "," & .FNAccidentType.SelectedIndex
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(.FDDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & .FTTime.Text & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTAccidentDesc.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTLocal.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrgans.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSymptom.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTTreatment.Text) & "'"
                _Cmd &= vbCrLf & "," & Integer.Parse(.FNHSysOpinionId.Properties.Tag)
                _Cmd &= vbCrLf & "," & .FNStopWorkDay.Value
                _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)

                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

            End With
            Call LoadAccident()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvAccident_DoubleClick(sender As Object, e As EventArgs) Handles ogvAccident.DoubleClick
        Try
            Dim _Cmd As String = ""
            Dim _STime As String = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _SDate As String = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SYSTEM, "")

            With Me.ogvAccident
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _AccidentId As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysAccidentId").ToString)
                Dim _FNAccidentType As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNAccidentType").ToString)
                Dim _FDDate As String = HI.UL.ULDate.ConvertEnDB(.GetRowCellValue(.FocusedRowHandle, "FDDate").ToString)
                Dim _FTTime As String = .GetRowCellValue(.FocusedRowHandle, "FTTime").ToString
                Dim _FTAccidentDesc As String = .GetRowCellValue(.FocusedRowHandle, "FTAccidentDesc").ToString
                Dim _FTLocal As String = .GetRowCellValue(.FocusedRowHandle, "FTLocal").ToString
                Dim _FTOrgans As String = .GetRowCellValue(.FocusedRowHandle, "FTOrgans").ToString
                Dim _FTSymptom As String = .GetRowCellValue(.FocusedRowHandle, "FTSymptom").ToString
                Dim _FTTreatment As String = .GetRowCellValue(.FocusedRowHandle, "FTTreatment").ToString
                Dim _FNStopWorkDay As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNStopWorkDay").ToString)
                Dim _FTOpinionCode As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTOpinionCode").ToString)

                HI.TL.HandlerControl.ClearControl(_AddItemAccident)
                With _AddItemAccident
                    ._Proc = False
                    ._FTStateGen = "1"
                    ._AccidentId = _AccidentId
                    .FNAccidentType.SelectedIndex = _FNAccidentType
                    .FDDate.DateTime = _FDDate
                    .FTTime.Time = _FTTime
                    .FTAccidentDesc.Text = _FTAccidentDesc
                    .FTLocal.Text = _FTLocal
                    .FTLocal.Text = _FTLocal
                    .FTOrgans.Text = _FTOrgans
                    .FTSymptom.Text = _FTSymptom
                    .FTTreatment.Text = _FTTreatment
                    .FNStopWorkDay.Value = _FNStopWorkDay
                    .FNHSysOpinionId.Text = _FTOpinionCode


                    .ShowDialog()

                    If Not (._Proc) Then
                        _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay"
                        _Cmd &= vbCrLf & "WHERE FNHSysMECGenId=" & Integer.Parse(._AccidentId)
                        _Cmd &= vbCrLf & "and FDInsDate >'" & _SDate & "'"
                        _Cmd &= vbCrLf & "and FTInsTime >'" & _STime & "'"
                        _Cmd &= vbCrLf & "and Isnull(FTStateGen,'0') = '1'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                        Exit Sub
                    End If

                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTAccident "
                    _Cmd &= vbCrLf & "Set FTInsUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDInsDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTInsTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FDDate='" & HI.UL.ULDate.ConvertEnDB(.FDDate.Text) & "'"
                    _Cmd &= vbCrLf & ",FTTime='" & .FTTime.Text & "'"
                    _Cmd &= vbCrLf & ",FTAccidentDesc='" & HI.UL.ULF.rpQuoted(.FTAccidentDesc.Text) & "'"
                    _Cmd &= vbCrLf & ",FTLocal='" & HI.UL.ULF.rpQuoted(.FTLocal.Text) & "'"
                    _Cmd &= vbCrLf & ",FTOrgans='" & HI.UL.ULF.rpQuoted(.FTOrgans.Text) & "'"
                    _Cmd &= vbCrLf & ",FTSymptom='" & HI.UL.ULF.rpQuoted(.FTSymptom.Text) & "'"
                    _Cmd &= vbCrLf & ",FTTreatment='" & HI.UL.ULF.rpQuoted(.FTTreatment.Text) & "'"
                    _Cmd &= vbCrLf & ",FNHSysOpinionId=" & Integer.Parse(.FNHSysOpinionId.Properties.Tag)
                    _Cmd &= vbCrLf & ",FNStopWorkDay=" & Integer.Parse(.FNStopWorkDay.Value)
                    _Cmd &= vbCrLf & "WHERE FNHSysAccidentId=" & Integer.Parse(._AccidentId)
                    _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                End With
            End With
            Call LoadAccident()
        Catch ex As Exception
        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            With Me.ogvAccident
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Return False
                Dim _AccidentId As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysAccidentId").ToString)

                _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTAccident"
                _Cmd &= vbCrLf & "WHERE FNHSysAccidentId=" & Integer.Parse(_AccidentId)
                _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub ogvAccident_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvAccident.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If Me.DeleteData Then
                    Call LoadAccident()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region


#Region "Consul"
    Private Sub LoadDataConsul()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT      FNHSysConsulId, FNHSysEmpId, FTTime, FTConsulDesc, FTSymptom, FTGuid"
            _Cmd &= vbCrLf & ", Case when isdate(FDDate) = 1 Then convert(nvarchar(10) , convert(datetime,FDDate),103) Else '' End AS  FDDate"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTConsul WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            Me.ogcConsul.DataSource = _oDt
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmAddConsul_Click(sender As Object, e As EventArgs)
        Try
            Dim _Cmd As String = ""
            Dim _DocRunId As Integer = HI.TL.RunID.GetRunNoID("TMECTConsul", " FNHSysConsulId", Conn.DB.DataBaseName.DB_MEDC)
            HI.TL.HandlerControl.ClearControl(_AddItemConsul)

            With _AddItemConsul
                ._Proc = False
                ._ConsulId = _DocRunId
                .FDDate.DateTime = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_SYSTEM, "")
                .FTTime.Time = HI.Conn.SQLConn.GetField("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_SYSTEM, "")


                .ShowDialog()

                If Not (._Proc) Then
                    Exit Sub
                End If

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTConsul"
                _Cmd &= " (FTInsUser, FDInsDate, FTInsTime,   FNHSysConsulId, FNHSysEmpId, FDDate, FTTime, FTConsulDesc, FTSymptom, FTGuid)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & "," & Integer.Parse(._ConsulId)
                _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(.FDDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTTime.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTConsulDesc.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTSymptom.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTGuid.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)


            End With
            Call LoadDataConsul()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvConsul_DoubleClick(sender As Object, e As EventArgs) Handles ogvConsul.DoubleClick
        Try
            Dim _Cmd As String = ""

            With ogvConsul
                Dim _ConsulId As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysConsulId").ToString)
                Dim _FDDate As String = HI.UL.ULDate.ConvertEnDB(.GetRowCellValue(.FocusedRowHandle, "FDDate").ToString)
                Dim _FTTime As String = .GetRowCellValue(.FocusedRowHandle, "FTTime").ToString
                Dim _FTConsulDesc As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTConsulDesc").ToString)
                Dim _FTSymptom As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTSymptom").ToString)
                Dim _FTGuid As String = HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTGuid").ToString)

                HI.TL.HandlerControl.ClearControl(_AddItemConsul)
                With _AddItemConsul
                    ._Proc = False
                    ._ConsulId = _ConsulId
                    .FDDate.DateTime = _FDDate
                    .FTTime.Text = _FTTime
                    .FTConsulDesc.Text = _FTConsulDesc
                    .FTSymptom.Text = _FTSymptom
                    .FTGuid.Text = _FTGuid

                    .ShowDialog()

                    If Not (._Proc) Then
                        Exit Sub
                    End If

                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTConsul"
                    _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FDDate='" & HI.UL.ULDate.ConvertEnDB(.FDDate.Text) & "'"
                    _Cmd &= vbCrLf & ",FTTime='" & HI.UL.ULF.rpQuoted(.FTTime.Text) & "'"
                    _Cmd &= vbCrLf & ",FTConsulDesc='" & HI.UL.ULF.rpQuoted(.FTConsulDesc.Text) & "'"
                    _Cmd &= vbCrLf & ",FTSymptom='" & HI.UL.ULF.rpQuoted(.FTSymptom.Text) & "'"
                    _Cmd &= vbCrLf & ",FTGuid='" & HI.UL.ULF.rpQuoted(.FTGuid.Text) & "'"
                    _Cmd &= vbCrLf & "WHERE FNHSysConsulId=" & Integer.Parse(._ConsulId)
                    _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

                End With

            End With
            Call LoadDataConsul()
        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteConsul() As Boolean
        Try
            Dim _Cmd As String = ""
            With ogvConsul
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Return False
                Dim _ConsulId As Integer = Integer.Parse(.GetRowCellValue(.FocusedRowHandle, "FNHSysConsulId").ToString)
                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTConsul"
                _Cmd &= vbCrLf & "WHERE FNHSysConsulId=" & Integer.Parse(_ConsulId)
                _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpID.Properties.Tag)
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ogvConsul_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvConsul.KeyDown
        Try
            If DeleteConsul() Then
                Call LoadDataConsul()
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region





    Private Sub ocmNewRecord_Click(sender As Object, e As EventArgs) Handles ocmNewRecord.Click
        Try
            Select Case True
                Case (otbDetail.SelectedTabPage.Name = otbGeneral.Name)
                    Call ocmadd_Click(sender, e)
                Case (otbDetail.SelectedTabPage.Name = otbAccident.Name)
                    Call ocmaddAccident_Click(sender, e)
                Case (otbDetail.SelectedTabPage.Name = otbConsul.Name)
                    Call ocmAddConsul_Click(sender, e)
            End Select
        Catch ex As Exception
        End Try
    End Sub

     
   
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub
End Class