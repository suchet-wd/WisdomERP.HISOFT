Imports System.Windows.Forms

Public Class wMEDRemedyReport

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitGrid()

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

    Private Sub PrePareData()
        Try
            Dim oDbDt As DataTable
            Dim tSql As String = ""
            tSql = "SELECT  * FROM (SELECT  TOP 0  '' AS FTCode,'' AS FTName "
            tSql &= " FROM TCNMSect  WITH(NOLOCK) ) AS M"
            tSql &= " ORDER BY FTCode "
            oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)
            m_DbDtSect = oDbDt.Clone
            ogdsect.DataSource = m_DbDtSect


            tSql = "SELECT  * FROM (SELECT  TOP 0  '' AS FTCode,'' AS FTName "
            tSql &= " FROM TMECMTypeofDisease  WITH(NOLOCK) ) AS M"

            oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)
            m_DbDtMType = oDbDt.Clone
            ogcTypeofDisease.DataSource = m_DbDtMType


            tSql = "SELECT * FROM (SELECT  TOP 0   '' AS FTCode,'' AS FTName "
            tSql &= " FROM TCNMUnitSect  WITH(NOLOCK) ) AS M"
            tSql &= " ORDER BY FTCode "

            oDbDt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MASTER)
            m_DbDtUnitSect = oDbDt.Clone
            ogdunitsect.DataSource = m_DbDtUnitSect
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wMEDRemedyReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call PrePareData()
        Catch ex As Exception
        End Try
    End Sub

#Region "Sect"
    Private m_DbDtSect As New DataTable
    ReadOnly Property DbDtSect As DataTable
        Get
            Return m_DbDtSect
        End Get
    End Property

    Private Sub FNSectCon_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNSectCon.SelectedIndexChanged

        FNHSysSectId.Properties.ReadOnly = (FNSectCon.SelectedIndex = 0)
        FNHSysSectIdTo.Properties.ReadOnly = Not (FNSectCon.SelectedIndex = 1)

        FNHSysSectId.Properties.Buttons(0).Enabled = Not (FNHSysSectId.Properties.ReadOnly)
        FNHSysSectIdTo.Properties.Buttons(0).Enabled = Not (FNHSysSectIdTo.Properties.ReadOnly)

        FNHSysSectId.Text = ""
        FNHSysSectIdTo.Text = ""

        m_DbDtSect.Rows.Clear()
        m_DbDtSect.AcceptChanges()

    End Sub

    Private Sub FNHSysSectId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNHSysSectId.KeyDown
        Try
            Select Case Me.FNSectCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter
                            If FNHSysSectId.Text = "" Then Exit Sub
                            If FNHSysSectId.Properties.Tag.ToString = "" Then Exit Sub
                            Dim NewRow As DataRow = m_DbDtSect.NewRow
                            NewRow("FTCode") = FNHSysSectId.Text
                            NewRow("FTName") = FNHSysSectId_None.Text
                            m_DbDtSect.Rows.Add(NewRow)
                            m_DbDtSect.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvSect_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ogvsect.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvsect_DoubleClick(ogvsect, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvsect_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvsect.DoubleClick
        Try
            With ogvsect
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtSect.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

#Region "MEDType"
    Private m_DbDtMType As New DataTable
    ReadOnly Property DbDtMType As DataTable
        Get
            Return m_DbDtMType
        End Get
    End Property

    Private Sub FNMTypeCon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNMTypeCon.SelectedIndexChanged
        FNHSysTypeofDiseaseId.Properties.ReadOnly = (FNMTypeCon.SelectedIndex = 0)
        FNHSysTypeofDiseaseIdTo.Properties.ReadOnly = Not (FNMTypeCon.SelectedIndex = 1)

        FNHSysTypeofDiseaseId.Properties.Buttons(0).Enabled = Not (FNHSysTypeofDiseaseId.Properties.ReadOnly)
        FNHSysTypeofDiseaseIdTo.Properties.Buttons(0).Enabled = Not (FNHSysTypeofDiseaseIdTo.Properties.ReadOnly)

        FNHSysTypeofDiseaseId.Text = ""
        FNHSysTypeofDiseaseIdTo.Text = ""

        m_DbDtMType.Rows.Clear()
        m_DbDtMType.AcceptChanges()
    End Sub

    Private Sub FNHSysTypeofDiseaseId_KeyDown(sender As Object, e As KeyEventArgs) Handles FNHSysTypeofDiseaseId.KeyDown
        Try
            Select Case Me.FNMTypeCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter
                            If FNHSysTypeofDiseaseId.Text = "" Then Exit Sub
                            If FNHSysTypeofDiseaseId.Properties.Tag.ToString = "" Then Exit Sub
                            Dim NewRow As DataRow = m_DbDtMType.NewRow
                            NewRow("FTCode") = FNHSysTypeofDiseaseId.Text
                            NewRow("FTName") = FNHSysTypeofDiseaseId_None.Text
                            m_DbDtMType.Rows.Add(NewRow)
                            m_DbDtMType.AcceptChanges()
                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvTypeofDisease_DoubleClick(sender As Object, e As EventArgs) Handles ogvTypeofDisease.DoubleClick
        Try
            With ogvTypeofDisease
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtMType.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvTypeofDisease_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvTypeofDisease.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvTypeofDisease_DoubleClick(ogvsect, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub LoadDataDetail()
        Try
            Dim _Cmd As String = ""
            Dim _tStr As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT    ROW_NUMBER() OVER(ORDER BY E.FTEmpCode  DESC) AS FNSeq  , G.FNHSysMECGenId, G.FNHSysEmpId, G.FTMECTime, G.FTMECOutTime, G.FNLiveMin, G.FNHSysTypeofDiseaseId, G.FNCauseType, G.FTSymptom, G.FTRemedy, G.FNHSysOpinionId, T.FTTypeofDiseaseCode  "
            _Cmd &= vbCrLf & "     , O.FTOpinionCode  , E.FTEmpCode  , S.FTSectCode  ,U.FTUnitSectCode" ' , P.FNQuantity
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ",T.FTTypeofDiseaseNameTH AS FTTypeofDiseaseName    , O.FTOpinionNameTH as FTOpinionName , L.FTNameTH AS FNCauseDesc "
                _Cmd &= vbCrLf & " , P.FTPreNameNameTH +' '+E.FTEmpNameTH +'  '+FTEmpSurnameTH AS FTEmpName , S.FTSectNameTH AS FTSectName "
            Else
                _Cmd &= vbCrLf & ",T.FTTypeofDiseaseNameEN AS FTTypeofDiseaseName    , O.FTOpinionNameEN  as FTOpinionName ,L.FTNameEN AS FNCauseDesc "
                _Cmd &= vbCrLf & " , P.FTPreNameNameEN +' '+E.FTEmpNameEN +'  '+FTEmpSurnameEN AS FTEmpName , S.FTSectNameEN AS FTSectName"
            End If
            _Cmd &= vbCrLf & ",CASE WHEN Isdate(G.FDMECDate) = 1 Then convert(nvarchar(10),convert(datetime,G.FDMECDate),103) ELSE '' END AS FDMECDate "
            _Cmd &= vbCrLf & ",dbo.FN_GET_DrugPay(G.FNHSysMECGenId,'0') AS FTDrugCode "
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral AS G WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay AS P WITH (NOLOCK) ON G.FNHSysMECGenId = P.FNHSysMECGenId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug AS D WITH (NOLOCK) ON P.FNHSysDrugId = D.FNHSysDrugId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMOpinion AS O WITH (NOLOCK) ON G.FNHSysOpinionId = O.FNHSysOpinionId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMTypeofDisease AS T WITH (NOLOCK) ON G.FNHSysTypeofDiseaseId = T.FNHSysTypeofDiseaseId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) ON G.FNHSysEmpId = E.FNHSysEmpId "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON E.FNHSysSectId = S.FNHSysSectId  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH(NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId   "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON E.FNHSysUnitSectId = U.FNHSysUnitSectId   "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN (Select  FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & " WHERE     (FTListName = 'FNCauseType') ) AS L ON G.FNCauseType = L.FNListIndex  "
            _Cmd &= vbCrLf & "WHERE G.FDMECDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateStart.Text) & "'"
            _Cmd &= vbCrLf & " And G.FDMECDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateEnd.Text) & "'"
            _Cmd &= vbCrLf & " AND E.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
            Select Case Me.FNSectCon.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And S.FTSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "'"
                    _Cmd &= vbCrLf & " and S.FTSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "'"
                Case 2
                    _tStr = ""
                    For Each oRow As DataRow In Me.DbDtSect.Rows
                        _tStr &= oRow("FTCode") & "|"
                    Next

                    If _tStr.Trim <> "" Then
                        _tStr = Microsoft.VisualBasic.Left(Trim(_tStr), Len(Trim(_tStr)) - 1)
                        _Cmd &= IIf(_Cmd.Trim <> "", " AND ", "")
                        _Cmd &= " S.FTSectCode IN('" & _tStr.Replace("|", "','") & "') "
                    End If
            End Select
            Select Case Me.FNMTypeCon.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And T.FTTypeofDiseaseCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysTypeofDiseaseId.Text) & "'"
                    _Cmd &= vbCrLf & " And T.FTTypeofDiseaseCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysTypeofDiseaseIdTo.Text) & "'"
                Case 2
                    _tStr = ""
                    For Each oRow As DataRow In Me.DbDtMType.Rows
                        _tStr &= oRow("FTCode") & "|"
                    Next

                    If _tStr.Trim <> "" Then
                        _tStr = Microsoft.VisualBasic.Left(Trim(_tStr), Len(Trim(_tStr)) - 1)
                        _Cmd &= IIf(_Cmd.Trim <> "", " AND ", "")
                        _Cmd &= " T.FTTypeofDiseaseCode IN('" & _tStr.Replace("|", "','") & "') "
                    End If
            End Select
            Select Case Me.FNUnitSectCon.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And U.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
                    _Cmd &= vbCrLf & " And U.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
                Case 2
                    _tStr = ""
                    For Each oRow As DataRow In Me.m_DbDtUnitSect.Rows
                        _tStr &= oRow("FTCode") & "|"
                    Next

                    If _tStr.Trim <> "" Then
                        _tStr = Microsoft.VisualBasic.Left(Trim(_tStr), Len(Trim(_tStr)) - 1)
                        _Cmd &= IIf(_Cmd.Trim <> "", " AND ", "")
                        _Cmd &= " U.FTUnitSectCode IN('" & _tStr.Replace("|", "','") & "') "
                    End If
            End Select

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            Me.ogcgeneral.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FDDateStart.Text <> "" And Me.FDDateEnd.Text <> "" Then
                Call LoadDataDetail()
            Else
                If Me.FDDateStart.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDateStart_lbl.Text)
                    Me.FDDateStart.Focus()
                    Exit Sub
                End If
                If Me.FDDateEnd.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDateEnd_lbl.Text)
                    Me.FDDateEnd.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub
    Private m_DbDtUnitSect As New DataTable
    ReadOnly Property DbDtUnitSect As DataTable
        Get
            Return m_DbDtUnitSect
        End Get
    End Property
    Private Sub FNUnitSectCon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNUnitSectCon.SelectedIndexChanged

        FNHSysUnitSectId.Properties.ReadOnly = (FNUnitSectCon.SelectedIndex = 0)
        FNHSysUnitSectIdTo.Properties.ReadOnly = Not (FNUnitSectCon.SelectedIndex = 1)

        FNHSysUnitSectId.Properties.Buttons(0).Enabled = Not (FNHSysUnitSectId.Properties.ReadOnly)
        FNHSysUnitSectIdTo.Properties.Buttons(0).Enabled = Not (FNHSysUnitSectIdTo.Properties.ReadOnly)

        FNHSysUnitSectId.Text = ""
        FNHSysUnitSectIdTo.Text = ""

        m_DbDtUnitSect.Rows.Clear()
        m_DbDtUnitSect.AcceptChanges()
    End Sub

    Private Sub FNHSysUnitSectId_KeyDown(sender As Object, e As KeyEventArgs) Handles FNHSysUnitSectId.KeyDown
        Try
            Select Case Me.FNUnitSectCon.SelectedIndex
                Case 2
                    Select Case e.KeyCode
                        Case Keys.Enter

                            If FNHSysUnitSectId.Text = "" Then Exit Sub
                            If FNHSysUnitSectId.Properties.Tag.ToString = "" Then Exit Sub

                            Dim NewRow As DataRow = m_DbDtUnitSect.NewRow
                            NewRow("FTCode") = FNHSysUnitSectId.Text
                            NewRow("FTName") = FNHSysUnitSectId_None.Text

                            m_DbDtUnitSect.Rows.Add(NewRow)
                            m_DbDtUnitSect.AcceptChanges()

                    End Select
                Case Else
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvunitsect_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvunitsect.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    Call ogvunitsect_DoubleClick(ogvunitsect, New System.EventArgs)
            End Select
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub

    Private Sub ogvunitsect_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ogvunitsect.DoubleClick
        Try
            With ogvunitsect
                If .FocusedRowHandle < 0 Then Exit Sub
                .DeleteRow(.FocusedRowHandle)
                m_DbDtUnitSect.AcceptChanges()
            End With
        Catch ex As Exception
            HI.MG.ShowMsg.mProcessError(ex.Message)
        End Try
    End Sub
End Class