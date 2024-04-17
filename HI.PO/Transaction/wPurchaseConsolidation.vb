Public Class wPurchaseConsolidation

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Procedure"
    Private _UserMerTeam As String = ""
    Property UserMerTeam As String
        Get
            Return _UserMerTeam
        End Get
        Set(value As String)
            _UserMerTeam = value
        End Set
    End Property

    Private wAutoGenPo As HI.PO.wAutoGeneratePO

    Private Function Verifydata() As Boolean

        Dim _Pass As Boolean = False

        If FNHSysBuyId.Text <> "" Then
            _Pass = True
        End If

        If FNHSysStyleId.Text <> "" Then
            _Pass = True
        End If

        If FNHSysSeasonId.Text <> "" Then
            _Pass = True
        End If

        Return _Pass

    End Function


    Private Sub LoadListData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable
        _Qry = "SELECT '1' AS FTSelect"
        _Qry &= vbCrLf & ", FNListIndex"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", FTNameTH AS FTName"
        Else
            _Qry &= vbCrLf & ", FTNameEN AS FTName "
        End If

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "WHERE  (FTListName = N'FNMerMatType')"
        _Qry &= vbCrLf & " ORDER BY FNListIndex"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        Me.ogc.DataSource = _Dt.Copy
        _Dt.Dispose()
    End Sub


    Private Sub LoadOrderTypeList()

        Dim _Qry As String = ""
        Dim _Dt As DataTable

        Dim _TmpStrEN As String = ""

        _Qry = "  SELECT  FTNameEN  ,MIN(FNListIndex) As FNSortSeq "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE FTListName IN ('FNOrderType','FNSMPOrderType') "
        _Qry &= vbCrLf & " GROUP BY FTNameEN  ORDER BY MIN(FNListIndex)  "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        For Each R As DataRow In _Dt.Rows
            If _TmpStrEN = "" Then
                _TmpStrEN = R!FTNameEN.ToString
            Else
                _TmpStrEN = _TmpStrEN & "|" & R!FTNameEN.ToString
            End If
        Next

        FNOrderType.Properties.Items.Clear()
        FNOrderType.Properties.Items.AddRange(_TmpStrEN.Split("|"))

        If FNOrderType.Properties.Items.Count > 0 Then

            FNOrderType.Properties.Items(0).CheckState = System.Windows.Forms.CheckState.Checked

        End If

    End Sub
    Private Function SaveData(_dt As DataTable) As Boolean
        Dim _Qry As String = ""
        Dim _TotalCount As Integer = 0


        Dim _FTStateTeamMer As String = "0"

        'If HI.ST.SysInfo.Admin Then
        '    _FTStateTeamMer = "3"
        'End If

        For Each R As DataRow In _dt.Select("FTStateSelect='1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FNTotalPurchaseQuantity >0 AND FTStateTeamMer<>'" & _FTStateTeamMer & "'  AND FTStateSC <>'1' ")

            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing]  "
            _Qry &= vbCrLf & " SET "
            _Qry &= vbCrLf & " FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & ",FNSCQuantity= '" & Double.Parse(Val(R!FNTotalPurchaseQuantity.ToString())) & "'"
            _Qry &= vbCrLf & ",FNSCPlusQuantity = '" & 0 & "'"
            _Qry &= vbCrLf & ",FNTotalPurchaseQuantity ='" & Double.Parse(Val(R!FNTotalPurchaseQuantity.ToString())) & "'"
            _Qry &= vbCrLf & ",FNHSysUnitIdPurchase =" & Integer.Parse(Val(R!FNHSysToUnitId_Hide.ToString)) & ""
            _Qry &= vbCrLf & ",FNPricePurchase ='" & Double.Parse(Val(R!FNPricePurchase.ToString())) & "'"
            _Qry &= vbCrLf & ",FNHSysCurId = " & Integer.Parse(Val(R!FNHSysCurId_Hide.ToString())) & ""
            _Qry &= vbCrLf & ",FNHSysSuplId = " & Integer.Parse(Val(R!FNHSysSuplId_Hide.ToString())) & ""
            _Qry &= vbCrLf & ",FTFabricFrontSize = '" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "'"
            _Qry &= vbCrLf & "    WHERE "
            _Qry &= vbCrLf & "     FNHSysRawMatId ='" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & "'"
            _Qry &= vbCrLf & "     AND FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
            _Qry &= vbCrLf & "     AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
            _Qry &= vbCrLf & "     AND FNStateChange =" & Val(R!FNStateChange.ToString) & ""

            _Qry &= vbCrLf & "     AND ISNULL(FTPurchaseNo,'') =''"

            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR) = False Then


                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] "
                _Qry &= vbCrLf & "( "
                _Qry &= vbCrLf & "FTInsUser,FDInsDate,FTInsTime,FTOrderNo,FTSubOrderNo,FNHSysRawMatId ,FNUsedQuantity,FNUsedPlusQuantity"
                _Qry &= vbCrLf & ",FNHSysUnitId,FNPrice,FTStateNominate,FDDateSC "
                _Qry &= vbCrLf & ",FTPurchaseNo,FNHSysSuplId,FNSCQuantity,FNSCPlusQuantity"
                _Qry &= vbCrLf & ",FNTotalPurchaseQuantity,FNHSysUnitIdPurchase,FNPricePurchase "
                _Qry &= vbCrLf & ",FNHSysCurId,FNStateChange,FTFabricFrontSize,FNReserveQuantity"
                _Qry &= vbCrLf & ",FNTransferQuantity,FNOptiplanQuantity "
                _Qry &= vbCrLf & " ) "
                _Qry &= vbCrLf & "VALUES "
                _Qry &= vbCrLf & "('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNUsedQuantity.ToString())) & " "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNUsedPlusQuantity.ToString())) & " "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysUnitIdMRP.ToString())) & " "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNPrice.ToString())) & " "
                _Qry &= vbCrLf & ",'" & R!FTStateNominate.ToString & "' "
                _Qry &= vbCrLf & ",'" & R!FDInsDate.ToString & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "' "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId_Hide.ToString())) & " "
                _Qry &= vbCrLf & ",'" & Double.Parse(Val(R!FNTotalPurchaseQuantity.ToString())) & "' "
                _Qry &= vbCrLf & ",'" & 0 & "' "
                _Qry &= vbCrLf & ",'" & Double.Parse(Val(R!FNTotalPurchaseQuantity.ToString())) & "' "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysToUnitId_Hide.ToString())) & " "
                _Qry &= vbCrLf & ",'" & Double.Parse(Val(R!FNPricePurchase.ToString())) & "' "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysCurId_Hide.ToString())) & " "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNStateChange.ToString())) & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNReserveQuantity.ToString())) & " "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNTransferQuantity.ToString())) & " "
                _Qry &= vbCrLf & ",'" & Double.Parse(Val(R!FNOptiplanQuantity.ToString())) & "' "
                _Qry &= vbCrLf & ")"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR) = True Then

                    _TotalCount = _TotalCount + 1

                End If

            Else
                _TotalCount = _TotalCount + 1
            End If

        Next

        Return (_TotalCount > 0)
    End Function

    Private Sub Loaddata()

        Dim _dtmattype As DataTable
        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกประเภทวัตถุดิบ !!!", 1613052478, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            _dtmattype = .Copy
        End With
        Dim _Qry As String = ""

        Dim _FNHSysBuyId As Integer = Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString()))
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString()))
        Dim _FNHSysSeasonId As Integer = Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString()))
        Dim _FNAllMattype As String = ""
        Dim _FTOrderNo As String = ""
        Dim _Lang As String = "TH"

        For Each Rxm As DataRow In _dtmattype.Select("FTSelect='1'")

            If _FNAllMattype = "" Then
                _FNAllMattype = Rxm!FNListIndex.ToString
            Else
                _FNAllMattype = _FNAllMattype & "," & Rxm!FNListIndex.ToString
            End If

        Next

        ' If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
        _Lang = "EN"
            ' End If

            Dim Spls As New HI.TL.SplashScreen("Loading...,Please Wait.")

        Try

            Dim _dt As DataTable
            Dim username As String = ""
            'username = "mlpsirikanya"
            username = HI.ST.UserInfo.UserName



            Dim icount As Integer = FNOrderType.Properties.Items.Count
            Dim StrAllType As String = ""
            Dim StrAllTextType As String = ""
            For I As Integer = 0 To icount - 1
                If FNOrderType.Properties.Items(I).CheckState Then
                    If StrAllTextType = "" Then
                        ' StrAllType = I.ToString

                        StrAllTextType = FNOrderType.Properties.Items(I).Value.ToString

                    Else
                        ' StrAllType = StrAllType & "," & I.ToString

                        StrAllTextType = StrAllTextType & "," & FNOrderType.Properties.Items(I).Value.ToString
                    End If

                End If
            Next

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_GETDATA_FOR_CONSOLIDATE " & _FNHSysBuyId & "," & _FNHSysStyleId & "," & _FNHSysSeasonId & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "','" & HI.UL.ULF.rpQuoted(username) & "','" & HI.UL.ULF.rpQuoted(_Lang) & "'," & -1 & " ,'" & _FNAllMattype & "','" & StrAllType & "','" & (StrAllTextType) & "','" & HI.UL.ULF.rpQuoted(FNHSysMerMatId.Text.Trim) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Me.ogcsc.DataSource = _dt.Copy
            ogvsc.ExpandAllGroups()

            _dt.Dispose()

        Catch ex As Exception
        End Try

        Spls.Close()

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click

        Me.Close()

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        FTStateMaterialCode.Checked = True

        If Me.Verifydata() Then
            Me.Loaddata()
        End If

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(Me)
        FTStateMaterialCode.Checked = True
        Call LoadListData()
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged

        Me.ogcsc.DataSource = Nothing

    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged

        Me.ogcsc.DataSource = Nothing

    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSeasonId.EditValueChanged

        Me.ogcsc.DataSource = Nothing

    End Sub

    Private Sub ogvsc_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvsc.CellMerge
        Try
            With Me.ogvsc
                Select Case e.Column.FieldName
                    Case "FTPurchaseNoMin"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTRawMatCode2").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTRawMatCode2").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTOrderNoMin", "FNQuantityMin", "FNQuantityMinRcv"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTRawMatCode2").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTRawMatCode2").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, "FTPurchaseNoMin").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPurchaseNoMin").ToString _
                             And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvsc_GroupRowCollapsing(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvsc.GroupRowCollapsing

    End Sub

    Private Sub ogvsc_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvsc.RowCellStyle

    End Sub

    Private Sub ogvsc_RowCountChanged(sender As Object, e As EventArgs) Handles ogvsc.RowCountChanged
        Try
            ogvsc.ExpandAllGroups()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvsc_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvsc.RowStyle
        Try
            With Me.ogvsc
                If "" & .GetRowCellValue(e.RowHandle, "FTStateSC").ToString = "1" Then

                    e.Appearance.BackColor = Drawing.Color.LightYellow
                    e.Appearance.BackColor2 = Drawing.Color.Orange

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFTStateSelect.EditValueChanging
        Dim FoundSame As Boolean = False
        Try
            With Me.ogvsc

                If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString = "1" Then


                    If "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString <> "" Then
                        e.Cancel = False

                        Dim _MatCode As String = "" & .GetFocusedRowCellValue("FTRawMatCode").ToString
                        Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                        Dim _Company As String = "" & .GetFocusedRowCellValue("FTCmpCode").ToString
                        Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                        Dim _FNRowSeq As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNRowSeq").ToString))
                        Dim _FTStateTeamMer As String = "0"


                        Dim CheckState As String = "0"

                        If e.NewValue.ToString = "1" Then
                            CheckState = "1"
                        End If

                        'With CType(Me.ogcsc.DataSource, DataTable)
                        '    .AcceptChanges()

                        '    If (Me.FTStateByCompany.Checked) Then
                        '        If .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & " AND FTPurchaseNo<>'' ").Length > 0 Then

                        '            If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                        '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1'  AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & "   AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' AND FTPurchaseNo<>'' ")

                        '                    R!FTStateSelect = CheckState


                        '                Next

                        '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & " AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' AND FTPurchaseNo<>'' ")

                        '                    R!FTStateSelect = CheckState


                        '                Next


                        '            Else
                        '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & "  AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' AND FTPurchaseNo<>'' ")
                        '                    R!FTStateSelect = CheckState


                        '                Next
                        '            End If

                        '        Else
                        '            For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & " AND FTPurchaseNo<>'' ")
                        '                R!FTStateSelect = CheckState
                        '            Next
                        '        End If
                        '    Else
                        '        If .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & "  AND FTPurchaseNo<>'' ").Length > 0 Then

                        '            If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                        '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & "  AND FTPurchaseNo<>'' ")

                        '                    R!FTStateSelect = CheckState
                        '                Next

                        '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & " AND FTPurchaseNo<>''  ")

                        '                    R!FTStateSelect = CheckState

                        '                Next
                        '            Else
                        '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & " AND FTPurchaseNo<>'' ")
                        '                    R!FTStateSelect = CheckState

                        '                Next
                        '            End If

                        '        Else
                        '            For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & " AND FTPurchaseNo<>'' ")
                        '                R!FTStateSelect = CheckState
                        '            Next
                        '        End If

                        '    End If


                        '    .AcceptChanges()

                        'End With


                        If .DataRowCount > 0 Then



                            If (Me.FTStateByCompany.Checked) Then

                                FoundSame = False
                                For I1 As Integer = 0 To .DataRowCount - 1
                                    If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                        AndAlso .GetRowCellValue(I1, "FTStateSC").ToString = "1" _
                                        AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                        AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                        AndAlso .GetRowCellValue(I1, "FTCmpCode").ToString = _Company _
                                        AndAlso Val(.GetRowCellValue(I1, "FNRowSeq").ToString) <> _FNRowSeq _
                                        AndAlso .GetRowCellValue(I1, "FTPurchaseNo").ToString <> "" Then

                                        FoundSame = True

                                        Exit For
                                    End If
                                Next


                                If FoundSame Then

                                    If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                                        For I1 As Integer = 0 To .DataRowCount - 1
                                            If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                                             AndAlso .GetRowCellValue(I1, "FTStateSC").ToString = "1" _
                                                             AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                                             AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                                             AndAlso .GetRowCellValue(I1, "FTCmpCode").ToString = _Company _
                                                             AndAlso .GetRowCellValue(I1, "FTPurchaseNo").ToString <> "" Then

                                                .SetRowCellValue(I1, "FTStateSelect", CheckState)

                                            End If
                                        Next


                                    Else
                                        .SetFocusedRowCellValue("FTStateSelect", CheckState)
                                    End If

                                Else

                                    .SetFocusedRowCellValue("FTStateSelect", CheckState)
                                End If
                            Else


                                FoundSame = False
                                For I1 As Integer = 0 To .DataRowCount - 1
                                    If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                            AndAlso .GetRowCellValue(I1, "FTStateSC").ToString = "1" _
                                            AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                            AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                            AndAlso Val(.GetRowCellValue(I1, "FNRowSeq").ToString) <> _FNRowSeq _
                                            AndAlso .GetRowCellValue(I1, "FTPurchaseNo").ToString <> "" Then

                                        FoundSame = True

                                        Exit For
                                    End If
                                Next

                                If FoundSame Then

                                    If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                                        For I1 As Integer = 0 To .DataRowCount - 1
                                            If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                            AndAlso .GetRowCellValue(I1, "FTStateSC").ToString = "1" _
                                            AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                            AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                            AndAlso .GetRowCellValue(I1, "FTPurchaseNo").ToString <> "" Then

                                                .SetRowCellValue(I1, "FTStateSelect", CheckState)

                                            End If
                                        Next
                                    Else
                                        .SetFocusedRowCellValue("FTStateSelect", CheckState)
                                    End If

                                Else
                                    .SetFocusedRowCellValue("FTStateSelect", CheckState)

                                End If

                            End If
                        End If

                        CType(Me.ogcsc.DataSource, DataTable).AcceptChanges()

                    Else
                        e.Cancel = True

                        HI.MG.ShowMsg.mInfo("ข้อมูลถูกทำการ Sourcing แล้วไม่สามารถทำการเลือกข้อมูลนี้ได้ !!!", 1602210047, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                    End If


                Else

                    If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateTeamMer").ToString <> "1" And HI.ST.SysInfo.Admin = False Then
                        e.Cancel = True
                        HI.MG.ShowMsg.mInfo("ข้อมูล เป็นของ ทีมอื่น แล้วไม่สามารถทำการเลือกข้อมูลนี้ได้ !!!", 1602210048, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                    Else
                        e.Cancel = False

                        If e.NewValue.ToString = "1" Then

                            Dim _MatCode As String = "" & .GetFocusedRowCellValue("FTRawMatCode").ToString
                            Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                            Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                            Dim _FNRowSeq As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNRowSeq").ToString))
                            Dim _Company As String = "" & .GetFocusedRowCellValue("FTCmpCode").ToString

                            Dim _FTStateTeamMer As String = "0"

                            If HI.ST.SysInfo.Admin Then
                                _FTStateTeamMer = "3"
                            End If

                            'With CType(Me.ogcsc.DataSource, DataTable)
                            '    .AcceptChanges()
                            '    If (Me.FTStateByCompany.Checked) Then


                            '        If .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & " ").Length > 0 Then

                            '            If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                            '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & "   AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' ")
                            '                    R!FTStateSelect = "1"
                            '                    If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                        R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    Else
                            '                        R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    End If
                            '                    If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                        R!FNTotalPurchaseQuantity = 0
                            '                    End If
                            '                Next

                            '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & "   AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' ")
                            '                    R!FTStateSelect = "1"
                            '                    If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                        R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    Else
                            '                        R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    End If
                            '                    If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                        R!FNTotalPurchaseQuantity = 0
                            '                    End If
                            '                Next

                            '            Else
                            '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & "  AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' ")
                            '                    R!FTStateSelect = "1"
                            '                    If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                        R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    Else
                            '                        R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    End If
                            '                    If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                        R!FNTotalPurchaseQuantity = 0
                            '                    End If

                            '                Next
                            '            End If

                            '        Else
                            '            For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & "  AND FTCmpCode='" & HI.UL.ULF.rpQuoted(_Company) & "' ")
                            '                R!FTStateSelect = "1"
                            '                If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                    R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                Else
                            '                    R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                End If

                            '                If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                    R!FNTotalPurchaseQuantity = 0
                            '                End If
                            '            Next
                            '        End If

                            '    Else
                            '        '---------------------------------------------------------------------
                            '        If .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & " ").Length > 0 Then

                            '            If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                            '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & " ")
                            '                    R!FTStateSelect = "1"
                            '                    If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                        R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    Else
                            '                        R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    End If
                            '                    If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                        R!FNTotalPurchaseQuantity = 0
                            '                    End If
                            '                Next

                            '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq<>" & _FNRowSeq & " ")
                            '                    R!FTStateSelect = "1"
                            '                    If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                        R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    Else
                            '                        R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    End If
                            '                    If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                        R!FNTotalPurchaseQuantity = 0
                            '                    End If
                            '                Next

                            '            Else
                            '                For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & " ")
                            '                    R!FTStateSelect = "1"
                            '                    If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                        R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    Else
                            '                        R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                    End If
                            '                    If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                        R!FNTotalPurchaseQuantity = 0
                            '                    End If

                            '                Next
                            '            End If

                            '        Else
                            '            For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq=" & _FNRowSeq & " ")
                            '                R!FTStateSelect = "1"
                            '                If Val(R!FNOptiplanQuantity.ToString) > 0 Then
                            '                    R!FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                Else
                            '                    R!FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            '                End If

                            '                If Val(R!FNTotalPurchaseQuantity) < 0 Then
                            '                    R!FNTotalPurchaseQuantity = 0
                            '                End If
                            '            Next
                            '        End If

                            '    End If


                            '    .AcceptChanges()
                            'End With


                            If .DataRowCount > 0 Then

                                Dim CheckState As String = "1"


                                If (Me.FTStateByCompany.Checked) Then

                                    FoundSame = False

                                    Try
                                        For I1 As Integer = 0 To .DataRowCount - 1
                                            If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                                AndAlso .GetRowCellValue(I1, "FTStateSC").ToString <> "1" _
                                                AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                                AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                                AndAlso .GetRowCellValue(I1, "FTCmpCode").ToString = _Company _
                                                AndAlso Val(.GetRowCellValue(I1, "FNRowSeq").ToString) <> _FNRowSeq Then

                                                FoundSame = True

                                                Exit For
                                            End If
                                        Next
                                    Catch ex As Exception

                                    End Try



                                    If FoundSame Then

                                        If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                                            For I1 As Integer = 0 To .DataRowCount - 1
                                                If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                                                 AndAlso .GetRowCellValue(I1, "FTStateSC").ToString <> "1" _
                                                                 AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                                                 AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                                                 AndAlso .GetRowCellValue(I1, "FTCmpCode").ToString = _Company Then

                                                    .SetRowCellValue(I1, "FTStateSelect", CheckState)




                                                    If Val(.GetRowCellValue(I1, "FNOptiplanQuantity").ToString) > 0 Then

                                                        .SetRowCellValue(I1, "FNTotalPurchaseQuantity", Val(.GetRowCellValue(I1, "FNOptiplanQuantity").ToString) - (Val(.GetRowCellValue(I1, "FNReserveQuantity").ToString) + Val(Val(.GetRowCellValue(I1, "FNTransferQuantity").ToString))))

                                                    Else

                                                        .SetRowCellValue(I1, "FNTotalPurchaseQuantity", (Val(.GetRowCellValue(I1, "FNUsedQuantity").ToString) + Val(.GetRowCellValue(I1, "FNUsedPlusQuantity").ToString)) - (Val(.GetRowCellValue(I1, "FNReserveQuantity").ToString) + Val(Val(.GetRowCellValue(I1, "FNTransferQuantity").ToString))))
                                                    End If

                                                    If Val(.GetRowCellValue(I1, "FNTotalPurchaseQuantity").ToString) < 0 Then

                                                        .SetRowCellValue(I1, "FNTotalPurchaseQuantity", 0)
                                                    End If

                                                End If
                                            Next


                                        Else
                                            .SetFocusedRowCellValue("FTStateSelect", CheckState)

                                            If Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) > 0 Then

                                                .SetFocusedRowCellValue("FNTotalPurchaseQuantity", Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))

                                            Else

                                                .SetFocusedRowCellValue("FNTotalPurchaseQuantity", (Val(.GetFocusedRowCellValue("FNUsedQuantity").ToString) + Val(.GetFocusedRowCellValue("FNUsedPlusQuantity").ToString)) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))
                                            End If

                                            If Val(.GetFocusedRowCellValue("FNTotalPurchaseQuantity").ToString) < 0 Then

                                                .SetFocusedRowCellValue("FNTotalPurchaseQuantity", 0)
                                            End If

                                        End If

                                    Else

                                        .SetFocusedRowCellValue("FTStateSelect", CheckState)


                                        If Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) > 0 Then

                                            .SetFocusedRowCellValue("FNTotalPurchaseQuantity", Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))

                                        Else

                                            .SetFocusedRowCellValue("FNTotalPurchaseQuantity", (Val(.GetFocusedRowCellValue("FNUsedQuantity").ToString) + Val(.GetFocusedRowCellValue("FNUsedPlusQuantity").ToString)) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))
                                        End If

                                        If Val(.GetFocusedRowCellValue("FNTotalPurchaseQuantity").ToString) < 0 Then

                                            .SetFocusedRowCellValue("FNTotalPurchaseQuantity", 0)
                                        End If

                                    End If
                                Else


                                    FoundSame = False

                                    Try
                                        For I1 As Integer = 0 To .DataRowCount - 1
                                            If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                                    AndAlso .GetRowCellValue(I1, "FTStateSC").ToString <> "1" _
                                                    AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                                    AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode _
                                                    AndAlso Val(.GetRowCellValue(I1, "FNRowSeq").ToString) <> _FNRowSeq Then

                                                FoundSame = True

                                                Exit For
                                            End If
                                        Next
                                    Catch ex As Exception

                                    End Try


                                    If FoundSame Then

                                        If HI.MG.ShowMsg.mConfirmProcess("พบข้อมูล Item เดียวกัน แต่ต่างรายการคุณต้องการทำการเลือกด้วยหรือไม่ ?", 1602280053) = True Then

                                            For I1 As Integer = 0 To .DataRowCount - 1
                                                If .GetRowCellValue(I1, "FTStateTeamMer").ToString <> _FTStateTeamMer _
                                                AndAlso .GetRowCellValue(I1, "FTStateSC").ToString <> "1" _
                                                AndAlso .GetRowCellValue(I1, "FTMerTeamCode").ToString = UserMerTeam _
                                                AndAlso .GetRowCellValue(I1, "FTRawMatCode").ToString = _MatCode Then

                                                    .SetRowCellValue(I1, "FTStateSelect", CheckState)



                                                    If Val(.GetRowCellValue(I1, "FNOptiplanQuantity").ToString) > 0 Then

                                                        .SetRowCellValue(I1, "FNTotalPurchaseQuantity", Val(.GetRowCellValue(I1, "FNOptiplanQuantity").ToString) - (Val(.GetRowCellValue(I1, "FNReserveQuantity").ToString) + Val(Val(.GetRowCellValue(I1, "FNTransferQuantity").ToString))))

                                                    Else


                                                        .SetRowCellValue(I1, "FNTotalPurchaseQuantity", (Val(.GetRowCellValue(I1, "FNUsedQuantity").ToString) + Val(.GetRowCellValue(I1, "FNUsedPlusQuantity").ToString)) - (Val(.GetRowCellValue(I1, "FNReserveQuantity").ToString) + Val(Val(.GetRowCellValue(I1, "FNTransferQuantity").ToString))))
                                                    End If

                                                    If Val(.GetRowCellValue(I1, "FNTotalPurchaseQuantity").ToString) < 0 Then

                                                        .SetRowCellValue(I1, "FNTotalPurchaseQuantity", 0)
                                                    End If

                                                End If
                                            Next
                                        Else
                                            .SetFocusedRowCellValue("FTStateSelect", CheckState)

                                            If Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) > 0 Then

                                                .SetFocusedRowCellValue("FNTotalPurchaseQuantity", Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))

                                            Else

                                                .SetFocusedRowCellValue("FNTotalPurchaseQuantity", (Val(.GetFocusedRowCellValue("FNUsedQuantity").ToString) + Val(.GetFocusedRowCellValue("FNUsedPlusQuantity").ToString)) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))
                                            End If

                                            If Val(.GetFocusedRowCellValue("FNTotalPurchaseQuantity").ToString) < 0 Then

                                                .SetFocusedRowCellValue("FNTotalPurchaseQuantity", 0)
                                            End If

                                        End If

                                    Else
                                        .SetFocusedRowCellValue("FTStateSelect", CheckState)

                                        If Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) > 0 Then

                                            .SetFocusedRowCellValue("FNTotalPurchaseQuantity", Val(.GetFocusedRowCellValue("FNOptiplanQuantity").ToString) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))

                                        Else

                                            .SetFocusedRowCellValue("FNTotalPurchaseQuantity", (Val(.GetFocusedRowCellValue("FNUsedQuantity").ToString) + Val(.GetFocusedRowCellValue("FNUsedPlusQuantity").ToString)) - (Val(.GetFocusedRowCellValue("FNReserveQuantity").ToString) + Val(Val(.GetFocusedRowCellValue("FNTransferQuantity").ToString))))
                                        End If

                                        If Val(.GetFocusedRowCellValue("FNTotalPurchaseQuantity").ToString) < 0 Then

                                            .SetFocusedRowCellValue("FNTotalPurchaseQuantity", 0)
                                        End If

                                    End If

                                End If


                            End If



                            CType(Me.ogcsc.DataSource, DataTable).AcceptChanges()

                        End If

                    End If

                End If

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepPurchaseQty_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepPurchaseQty.EditValueChanging
        Try
            With Me.ogvsc

                If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSelect").ToString = "1" And "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString <> "1" Then
                    e.Cancel = False
                    Try
                        .SetFocusedRowCellValue("TotalPrice", CDbl(Format(CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNPricePurchase").ToString) * e.NewValue, "0.00")))

                    Catch ex As Exception

                        Try
                            .SetFocusedRowCellValue("TotalPrice", CDbl(Format(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNPricePurchase").ToString) * e.NewValue, "0.00")))
                        Catch ex2 As Exception
                        End Try

                    End Try

                Else
                    e.Cancel = True
                End If

                'If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString = "1" Then
                '    e.Cancel = True
                '    HI.MG.ShowMsg.mInfo("ข้อมูลถูกทำการ Sourcing แล้วไม่สามารถทำการแก้ไขข้อมูลนี้ได้ !!!", 1602212047, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                'Else
                '    If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateTeamMer").ToString <> "1" Then
                '        e.Cancel = True
                '        HI.MG.ShowMsg.mInfo("ข้อมูล เป็นของ ทีมอื่น แล้วไม่สามารถทำการแก้ไขข้อมูลนี้ได้ !!!", 1602212048, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                '    Else
                '        e.Cancel = False
                '    End If
                'End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepPurchasePrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepPurchasePrice.EditValueChanging

        Try

            With Me.ogvsc

                If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSelect").ToString = "1" And "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString <> "1" Then
                    e.Cancel = False

                    Try
                        .SetFocusedRowCellValue("TotalPrice", CDbl(Format(CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNTotalPurchaseQuantity").ToString) * e.NewValue, "0.00")))

                    Catch ex As Exception

                        Try
                            .SetFocusedRowCellValue("TotalPrice", CDbl(Format(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNTotalPurchaseQuantity").ToString) * e.NewValue, "0.00")))
                        Catch ex2 As Exception

                        End Try

                    End Try

                    Dim _MatCode As String = "" & .GetFocusedRowCellValue("FTRawMatCode").ToString
                    Dim _MatColorCode As String = "" & .GetFocusedRowCellValue("FTRawMatColorCode").ToString
                    Dim _MatSizeCode As String = "" & .GetFocusedRowCellValue("FTRawMatSizeCode").ToString
                    Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                    Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                    Dim _FNRowSeq As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNRowSeq").ToString))
                    Dim _FTStateTeamMer As String = "0"


                    If HI.ST.SysInfo.Admin Then
                        _FTStateTeamMer = "3"
                    End If

                    Try

                        Select Case True
                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysRawMatId").ToString)) = _FNHSysRawMatId And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNPricePurchase", e.NewValue)
                                            .SetRowCellValue(I, "TotalPrice", Val("" & .GetRowCellValue(I, "FNTotalPurchaseQuantity")) * Val(e.NewValue))
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try


                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatColorCode").ToString = _MatColorCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNPricePurchase", e.NewValue)
                                            .SetRowCellValue(I, "TotalPrice", Val("" & .GetRowCellValue(I, "FNTotalPurchaseQuantity")) * Val(e.NewValue))
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try



                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatSizeCode").ToString = _MatSizeCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNPricePurchase", e.NewValue)
                                            .SetRowCellValue(I, "TotalPrice", Val("" & .GetRowCellValue(I, "FNTotalPurchaseQuantity")) * Val(e.NewValue))
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                            Case (Me.FTStateMaterialCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNPricePurchase", e.NewValue)
                                            .SetRowCellValue(I, "TotalPrice", Val("" & .GetRowCellValue(I, "FNTotalPurchaseQuantity")) * Val(e.NewValue))
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try


                        End Select
                        With CType(Me.ogcsc.DataSource, DataTable)
                            .AcceptChanges()
                            'Select Case True
                            '    Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                            '        For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FNHSysRawMatId=" & _FNHSysRawMatId & " AND FNRowSeq>" & _FNRowSeq & " AND FTStateSelect='1' ")
                            '            R!FNPricePurchase = e.NewValue
                            '            R!TotalPrice = Val(R!FNTotalPurchaseQuantity) * Val(e.NewValue)
                            '        Next

                            '    Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked)

                            '        For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(_MatColorCode) & "' AND FNRowSeq>" & _FNRowSeq & " AND FTStateSelect='1' ")
                            '            R!FNPricePurchase = e.NewValue
                            '            R!TotalPrice = Val(R!FNTotalPurchaseQuantity) * Val(e.NewValue)
                            '        Next

                            '    Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                            '        For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(_MatSizeCode) & "' AND FNRowSeq>" & _FNRowSeq & " AND FTStateSelect='1' ")
                            '            R!FNPricePurchase = e.NewValue
                            '            R!TotalPrice = Val(R!FNTotalPurchaseQuantity) * Val(e.NewValue)
                            '        Next

                            '    Case (Me.FTStateMaterialCode.Checked)

                            '        For Each R As DataRow In .Select("FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTStateSC<>'1' AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(_MatCode) & "' AND FNRowSeq>" & _FNRowSeq & " AND FTStateSelect='1' ")
                            '            R!FNPricePurchase = e.NewValue
                            '            R!TotalPrice = Val(R!FNTotalPurchaseQuantity) * Val(e.NewValue)
                            '        Next

                            'End Select
                            ' .AcceptChanges()
                        End With


                    Catch ex As Exception

                    End Try

                Else
                    e.Cancel = True
                End If

                'If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString = "1" Then
                '    e.Cancel = True
                '    HI.MG.ShowMsg.mInfo("ข้อมูลถูกทำการ Sourcing แล้วไม่สามารถทำการแก้ไขข้อมูลนี้ได้ !!!", 1602212047, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                'Else
                '    If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateTeamMer").ToString <> "1" Then
                '        e.Cancel = True
                '        HI.MG.ShowMsg.mInfo("ข้อมูล เป็นของ ทีมอื่น แล้วไม่สามารถทำการแก้ไขข้อมูลนี้ได้ !!!", 1602212048, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                '    Else
                '        e.Cancel = False
                '    End If
                'End If

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvsc_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvsc.ShowingEditor
        Try
            With Me.ogvsc
                Select Case .FocusedColumn.FieldName.ToString
                    Case "FTStateSelect"
                        e.Cancel = False
                    Case Else
                        If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSelect").ToString = "1" Then

                            If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString = "1" Then
                                e.Cancel = True
                            Else
                                e.Cancel = False
                            End If


                        Else
                            e.Cancel = True
                        End If
                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wPurchaseConsolidation_Load(sender As Object, e As EventArgs) Handles Me.Load
        FTStateMaterialCode.Checked = True
        Me.ogv.OptionsView.ShowAutoFilterRow = False

        Call LoadListData()

        Dim cmd As String = ""

        Dim pUserName As String = ""


        pUserName = HI.ST.UserInfo.UserName
        ' pUserName = "mlpsirikanya"

        cmd = " SELECT A.FTMerTeamCode "
        cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As B WITH(NOLOCK) INNER JOIN"
        cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam AS A  WITH(NOLOCK) ON B.FNHSysMerTeamId = A.FNHSysMerTeamId"
        cmd &= vbCrLf & " WHERE  (B.FTUserName = N'" & HI.UL.ULF.rpQuoted(pUserName) & "')"

        UserMerTeam = HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SECURITY, "")


        Call LoadOrderTypeList()

    End Sub

    Private Sub RepFNTSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles RepFNTSysCurId.EditValueChanged
        Try

            With Me.ogvsc

                Dim _CurCode As String = sender.Text
                Dim _SysCurID As Integer = 0
                Dim _Qry As String = ""

                _Qry = "Select TOP 1 FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As X With(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(_CurCode) & "'"
                _SysCurID = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _SysCurID > 0 Then


                    Dim _MatCode As String = "" & .GetFocusedRowCellValue("FTRawMatCode").ToString
                    Dim _MatColorCode As String = "" & .GetFocusedRowCellValue("FTRawMatColorCode").ToString
                    Dim _MatSizeCode As String = "" & .GetFocusedRowCellValue("FTRawMatSizeCode").ToString
                    Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                    Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                    Dim _FNRowSeq As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNRowSeq").ToString))
                    Dim _FTStateTeamMer As String = "0"


                    If HI.ST.SysInfo.Admin Then
                        _FTStateTeamMer = "3"
                    End If

                    Try

                        Select Case True
                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysRawMatId").ToString)) = _FNHSysRawMatId And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysCurId", _CurCode)
                                            .SetRowCellValue(I, "FNHSysCurId_Hide", _SysCurID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try


                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatColorCode").ToString = _MatColorCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysCurId", _CurCode)
                                            .SetRowCellValue(I, "FNHSysCurId_Hide", _SysCurID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try



                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatSizeCode").ToString = _MatSizeCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysCurId", _CurCode)
                                            .SetRowCellValue(I, "FNHSysCurId_Hide", _SysCurID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                            Case (Me.FTStateMaterialCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysCurId", _CurCode)
                                            .SetRowCellValue(I, "FNHSysCurId_Hide", _SysCurID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try


                        End Select
                        With CType(Me.ogcsc.DataSource, DataTable)
                            .AcceptChanges()

                        End With


                    Catch ex As Exception

                    End Try

                Else

                End If


            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles RepFNHSysSuplId.EditValueChanged
        Try

            With Me.ogvsc

                Dim _SuplCode As String = sender.Text
                Dim _SysSuplID As Integer = 0
                Dim _Qry As String = ""

                _Qry = "SELECT TOP 1 FNHSysSuplId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS X WITH(NOLOCK) WHERE FTSuplCode='" & HI.UL.ULF.rpQuoted(_SuplCode) & "'"
                _SysSuplID = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _SysSuplID > 0 Then


                    Dim _MatCode As String = "" & .GetFocusedRowCellValue("FTRawMatCode").ToString
                    Dim _MatColorCode As String = "" & .GetFocusedRowCellValue("FTRawMatColorCode").ToString
                    Dim _MatSizeCode As String = "" & .GetFocusedRowCellValue("FTRawMatSizeCode").ToString
                    Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                    Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                    Dim _FNRowSeq As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNRowSeq").ToString))
                    Dim _FTStateTeamMer As String = "0"


                    If HI.ST.SysInfo.Admin Then
                        _FTStateTeamMer = "3"
                    End If

                    Try

                        Select Case True
                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysRawMatId").ToString)) = _FNHSysRawMatId And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysSuplId", _SuplCode)
                                            .SetRowCellValue(I, "FNHSysSuplId_Hide", _SysSuplID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try


                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatColorCode").ToString = _MatColorCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysSuplId", _SuplCode)
                                            .SetRowCellValue(I, "FNHSysSuplId_Hide", _SysSuplID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatSizeCode").ToString = _MatSizeCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysSuplId", _SuplCode)
                                            .SetRowCellValue(I, "FNHSysSuplId_Hide", _SysSuplID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                            Case (Me.FTStateMaterialCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            .SetRowCellValue(I, "FNHSysSuplId", _SuplCode)
                                            .SetRowCellValue(I, "FNHSysSuplId_Hide", _SysSuplID)
                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                        End Select

                        With CType(Me.ogcsc.DataSource, DataTable)
                            .AcceptChanges()
                        End With

                    Catch ex As Exception

                    End Try

                Else

                End If


            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFNHSysUnitId_EditValueChanged(sender As Object, e As EventArgs) Handles RepFNHSysUnitId.EditValueChanged
        Try

            With Me.ogvsc

                Dim _UnitCode As String = sender.Text
                Dim _SysUnitID As Integer = 0
                Dim _Qry As String = ""

                _Qry = "SELECT TOP 1 FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X WITH(NOLOCK) WHERE FTUnitCode='" & HI.UL.ULF.rpQuoted(_UnitCode) & "' AND FTStateUnitPurchase='1' AND FTStateActive='1'"
                _SysUnitID = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                If _SysUnitID > 0 Then

                    Dim _MatCode As String = "" & .GetFocusedRowCellValue("FTRawMatCode").ToString
                    Dim _MatColorCode As String = "" & .GetFocusedRowCellValue("FTRawMatColorCode").ToString
                    Dim _MatSizeCode As String = "" & .GetFocusedRowCellValue("FTRawMatSizeCode").ToString
                    Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                    Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                    Dim _FNRowSeq As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNRowSeq").ToString))
                    Dim _FTStateTeamMer As String = "0"
                    Dim _FTUnitCodeMRP As Integer = 0
                    Dim _UnitConvert As Double = 0
                    Dim _FNTotalPurchaseQuantity As Double = 0
                    _FTUnitCodeMRP = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysUnitIdMRP").ToString))

                    _Qry = "  SELECT FNRateFrom * FNRateTo AS UnitConvert"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS X WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  (FNHSysUnitId = " & _FTUnitCodeMRP & ") "
                    _Qry &= vbCrLf & " AND (FNHSysUnitIdTo = " & _SysUnitID & ")"

                    _UnitConvert = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))
                    If _UnitConvert > 0 Then


                        If Double.Parse(Val("" & .GetFocusedRowCellValue("FNOptiplanQuantity").ToString)) > 0 Then
                            '_FNTotalPurchaseQuantity = Val(R!FNOptiplanQuantity.ToString) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            _FNTotalPurchaseQuantity = Double.Parse(Val("" & .GetFocusedRowCellValue("FNOptiplanQuantity").ToString)) - (Double.Parse(Val("" & .GetFocusedRowCellValue("FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetFocusedRowCellValue("FNTransferQuantity").ToString)))
                        Else
                            '_FNTotalPurchaseQuantity = (Val(R!FNUsedQuantity.ToString) + Val(R!FNUsedPlusQuantity.ToString)) - (Val(R!FNReserveQuantity.ToString) + Val(R!FNTransferQuantity.ToString))
                            _FNTotalPurchaseQuantity = (Double.Parse(Val("" & .GetFocusedRowCellValue("FNUsedQuantity").ToString)) + Double.Parse(Val("" & .GetFocusedRowCellValue("FNUsedPlusQuantity").ToString))) - (Double.Parse(Val("" & .GetFocusedRowCellValue("FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetFocusedRowCellValue("FNTransferQuantity").ToString)))
                        End If

                        If _FNTotalPurchaseQuantity < 0 Then
                            _FNTotalPurchaseQuantity = 0
                        End If
                        _FNTotalPurchaseQuantity = CDbl(Format(_FNTotalPurchaseQuantity / _UnitConvert, HI.ST.Config.QtyFormat))

                        .SetFocusedRowCellValue("FNTotalPurchaseQuantity", _FNTotalPurchaseQuantity)
                    End If


                    If HI.ST.SysInfo.Admin Then
                        _FTStateTeamMer = "3"
                    End If

                    Try

                        Select Case True
                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try

                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysRawMatId").ToString)) = _FNHSysRawMatId And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            _FTUnitCodeMRP = Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysUnitIdMRP").ToString))

                                            _Qry = "  SELECT FNRateFrom * FNRateTo AS UnitConvert"
                                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS X WITH(NOLOCK)"
                                            _Qry &= vbCrLf & " WHERE  (FNHSysUnitId = " & _FTUnitCodeMRP & ") "
                                            _Qry &= vbCrLf & " AND (FNHSysUnitIdTo = " & _SysUnitID & ")"

                                            _UnitConvert = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))
                                            If _UnitConvert > 0 Then


                                                If Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) > 0 Then

                                                    _FNTotalPurchaseQuantity = Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                Else

                                                    _FNTotalPurchaseQuantity = (Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedPlusQuantity").ToString))) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                End If

                                                If _FNTotalPurchaseQuantity < 0 Then
                                                    _FNTotalPurchaseQuantity = 0
                                                End If

                                                _FNTotalPurchaseQuantity = CDbl(Format(_FNTotalPurchaseQuantity / _UnitConvert, HI.ST.Config.QtyFormat))

                                                .SetRowCellValue(I, "FNTotalPurchaseQuantity", _FNTotalPurchaseQuantity)

                                                .SetRowCellValue(I, "FNHSysToUnitId", _UnitCode)
                                                .SetRowCellValue(I, "FNHSysToUnitId_Hide", _SysUnitID)

                                            End If


                                        End If

                                    Next

                                Catch ex As Exception
                                End Try

                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialColorCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatColorCode").ToString = _MatColorCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            _FTUnitCodeMRP = Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysUnitIdMRP").ToString))

                                            _Qry = "  SELECT FNRateFrom * FNRateTo AS UnitConvert"
                                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS X WITH(NOLOCK)"
                                            _Qry &= vbCrLf & " WHERE  (FNHSysUnitId = " & _FTUnitCodeMRP & ") "
                                            _Qry &= vbCrLf & " AND (FNHSysUnitIdTo = " & _SysUnitID & ")"

                                            _UnitConvert = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))
                                            If _UnitConvert > 0 Then


                                                If Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) > 0 Then

                                                    _FNTotalPurchaseQuantity = Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                Else

                                                    _FNTotalPurchaseQuantity = (Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedPlusQuantity").ToString))) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                End If

                                                If _FNTotalPurchaseQuantity < 0 Then
                                                    _FNTotalPurchaseQuantity = 0
                                                End If

                                                _FNTotalPurchaseQuantity = CDbl(Format(_FNTotalPurchaseQuantity / _UnitConvert, HI.ST.Config.QtyFormat))

                                                .SetRowCellValue(I, "FNTotalPurchaseQuantity", _FNTotalPurchaseQuantity)

                                                .SetRowCellValue(I, "FNHSysToUnitId", _UnitCode)
                                                .SetRowCellValue(I, "FNHSysToUnitId_Hide", _SysUnitID)

                                            End If

                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                            Case (Me.FTStateMaterialCode.Checked) And (Me.FTStateMaterialSizeCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And "" & .GetRowCellValue(I, "FTRawMatSizeCode").ToString = _MatSizeCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            _FTUnitCodeMRP = Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysUnitIdMRP").ToString))

                                            _Qry = "  SELECT FNRateFrom * FNRateTo AS UnitConvert"
                                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS X WITH(NOLOCK)"
                                            _Qry &= vbCrLf & " WHERE  (FNHSysUnitId = " & _FTUnitCodeMRP & ") "
                                            _Qry &= vbCrLf & " AND (FNHSysUnitIdTo = " & _SysUnitID & ")"

                                            _UnitConvert = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))
                                            If _UnitConvert > 0 Then


                                                If Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) > 0 Then

                                                    _FNTotalPurchaseQuantity = Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                Else

                                                    _FNTotalPurchaseQuantity = (Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedPlusQuantity").ToString))) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                End If

                                                If _FNTotalPurchaseQuantity < 0 Then
                                                    _FNTotalPurchaseQuantity = 0
                                                End If

                                                _FNTotalPurchaseQuantity = CDbl(Format(_FNTotalPurchaseQuantity / _UnitConvert, HI.ST.Config.QtyFormat))

                                                .SetRowCellValue(I, "FNTotalPurchaseQuantity", _FNTotalPurchaseQuantity)

                                                .SetRowCellValue(I, "FNHSysToUnitId", _UnitCode)
                                                .SetRowCellValue(I, "FNHSysToUnitId_Hide", _SysUnitID)

                                            End If

                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                            Case (Me.FTStateMaterialCode.Checked)

                                Try
                                    For I As Integer = .FocusedRowHandle + 1 To .DataRowCount - 1

                                        If "" & .GetRowCellValue(I, "FTStateTeamMer").ToString <> _FTStateTeamMer And "" & .GetRowCellValue(I, "FTStateSC").ToString <> "1" And "" & .GetRowCellValue(I, "FTRawMatCode").ToString = _MatCode And Integer.Parse(Val("" & .GetRowCellValue(I, "FNRowSeq").ToString)) > _FNRowSeq And "" & .GetRowCellValue(I, "FTStateSelect").ToString = "1" Then
                                            _FTUnitCodeMRP = Integer.Parse(Val("" & .GetRowCellValue(I, "FNHSysUnitIdMRP").ToString))

                                            _Qry = "  SELECT FNRateFrom * FNRateTo AS UnitConvert"
                                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS X WITH(NOLOCK)"
                                            _Qry &= vbCrLf & " WHERE  (FNHSysUnitId = " & _FTUnitCodeMRP & ") "
                                            _Qry &= vbCrLf & " AND (FNHSysUnitIdTo = " & _SysUnitID & ")"

                                            _UnitConvert = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))
                                            If _UnitConvert > 0 Then


                                                If Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) > 0 Then

                                                    _FNTotalPurchaseQuantity = Double.Parse(Val("" & .GetRowCellValue(I, "FNOptiplanQuantity").ToString)) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                Else

                                                    _FNTotalPurchaseQuantity = (Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNUsedPlusQuantity").ToString))) - (Double.Parse(Val("" & .GetRowCellValue(I, "FNReserveQuantity").ToString)) + Double.Parse(Val("" & .GetRowCellValue(I, "FNTransferQuantity").ToString)))
                                                End If

                                                If _FNTotalPurchaseQuantity < 0 Then
                                                    _FNTotalPurchaseQuantity = 0
                                                End If

                                                _FNTotalPurchaseQuantity = CDbl(Format(_FNTotalPurchaseQuantity / _UnitConvert, HI.ST.Config.QtyFormat))

                                                .SetRowCellValue(I, "FNTotalPurchaseQuantity", _FNTotalPurchaseQuantity)

                                                .SetRowCellValue(I, "FNHSysToUnitId", _UnitCode)
                                                .SetRowCellValue(I, "FNHSysToUnitId_Hide", _SysUnitID)

                                            End If

                                        End If

                                    Next
                                Catch ex As Exception

                                End Try

                        End Select

                        With CType(Me.ogcsc.DataSource, DataTable)
                            .AcceptChanges()
                        End With

                    Catch ex As Exception

                    End Try

                Else

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmgenpo_Click(sender As Object, e As EventArgs) Handles ocmgenpo.Click

        If Not (Me.ogcsc.DataSource Is Nothing) Then

            Dim _dt As DataTable
            With CType(Me.ogcsc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy

            End With

            'If "" & .GetRowCellValue(.FocusedRowHandle, "FTStateSC").ToString Then

            If _dt.Select("FTStateSelect='1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "' AND FTStateSC <>'1' ").Length > 0 Then
                If _dt.Select("FTStateSelect='1' AND FNHSysSuplId='' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateSC <>'1'  ").Length <= 0 Then
                    If _dt.Select("FTStateSelect='1' AND FNHSysToUnitId='' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateSC <>'1' ").Length <= 0 Then

                        If _dt.Select("FTStateSelect='1' AND FNHSysCurId='' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateSC <>'1'  ").Length <= 0 Then

                            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Sourcing Auto และ Auto Purchase ใช่หรือไม่ ?", 1602270359) = True Then
                                Dim _Spls As New HI.TL.SplashScreen("Saving Sourcing Data Please wait...")
                                If SaveData(_dt) Then
                                    _Spls.Close()

                                    Me.Loaddata()

                                    Dim _StrAllJob As String = ""


                                    Dim _FTStateTeamMer As String = "0"

                                    'If HI.ST.SysInfo.Admin Then
                                    '    _FTStateTeamMer = "3"
                                    'End If

                                    Dim grp As List(Of String) = (_dt.Select("FTOrderNo<>'' AND FTStateSelect='1'  AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateTeamMer<>'" & _FTStateTeamMer & "' ", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                     .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                     .Distinct() _
                                                     .ToList()

                                    For Each Ind As String In grp
                                        If _StrAllJob = "" Then
                                            _StrAllJob = Ind
                                        Else
                                            _StrAllJob = _StrAllJob & "|" & Ind
                                        End If
                                    Next

                                    If (wAutoGenPo Is Nothing) Then
                                        wAutoGenPo = New HI.PO.wAutoGeneratePO(_StrAllJob)
                                    End If

                                    Try
                                        wAutoGenPo.Show()
                                        wAutoGenPo.PrepareDataGenerate(_StrAllJob)
                                    Catch ex As Exception
                                        wAutoGenPo = New HI.PO.wAutoGeneratePO(_StrAllJob)
                                        wAutoGenPo.Show()
                                    End Try

                                    wAutoGenPo.WindowState = System.Windows.Forms.FormWindowState.Maximized

                                Else
                                    _Spls.Close()
                                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึก Sourcing ได้ เนื่องจากเกิดข้อผิดพลาดบางประการ !!!", 1603050759, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                                End If

                            End If

                        Else
                            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูล สกุลเงิน ให้ครบถ้วน  !!!", 1602270350, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        End If

                    Else
                        HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูล หน่วยซื้อ ให้ครบถ้วน  !!!", 1602270349, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    End If

                Else
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูล ผู้ขาย ให้ครบถ้วน !!!", 1602270348, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            Else

                If _dt.Select("FTStateSelect='1' AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "' AND FTStateSC ='1' AND FTPurchaseNo='' ").Length > 0 Then
                    Dim _StrAllJob As String = ""


                    Dim _FTStateTeamMer As String = "0"

                    'If HI.ST.SysInfo.Admin Then
                    '    _FTStateTeamMer = "3"
                    'End If

                    Dim grp As List(Of String) = (_dt.Select("FTOrderNo<>'' AND FTStateSelect='1'  AND FTMerTeamCode='" & HI.UL.ULF.rpQuoted(UserMerTeam) & "'  AND FTStateTeamMer<>'" & _FTStateTeamMer & "' AND FTPurchaseNo='' ", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                     .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                     .Distinct() _
                                     .ToList()

                    For Each Ind As String In grp
                        If _StrAllJob = "" Then
                            _StrAllJob = Ind
                        Else
                            _StrAllJob = _StrAllJob & "|" & Ind
                        End If
                    Next

                    If (wAutoGenPo Is Nothing) Then
                        wAutoGenPo = New HI.PO.wAutoGeneratePO(_StrAllJob)
                    End If

                    Try
                        wAutoGenPo.Show()
                        wAutoGenPo.PrepareDataGenerate(_StrAllJob)
                    Catch ex As Exception
                        wAutoGenPo = New HI.PO.wAutoGeneratePO(_StrAllJob)
                        wAutoGenPo.Show()
                    End Try

                    wAutoGenPo.WindowState = System.Windows.Forms.FormWindowState.Maximized
                Else

                    HI.MG.ShowMsg.mInfo("กรุณาทำการ เลือกข้อมูลรายการ !!!", 1602270347, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If


            End If

        Else

            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูล !!!", 1602270346, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

        End If

    End Sub

    Private Sub ogbheader_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles ogbheader.Paint

    End Sub

    Private Sub ogvsc_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ogvsc.CustomColumnDisplayText
        Try

            Select Case e.Column.FieldName
                Case "FNRepeatLengthCM", "FNTotalRepeat", "FNTotalRepeatRsv", "FNTotalRepeatPO"
                    If Conversion.Val(e.Value.ToString()) = 0 Then
                        e.DisplayText = ""
                    End If
                Case Else

            End Select
        Catch ex As Exception

        End Try



    End Sub

    Private Sub ocmopenpofrompohistory_Click(sender As Object, e As EventArgs) Handles ocmrenewsourcing.Click

        If Not (Me.ogcsc.DataSource Is Nothing) Then
            Dim _dt As New DataTable
            With CType(Me.ogcsc.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTStateSelect='1' AND   FTStateSC ='1' AND FTPurchaseNo <>'' ").Length > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Clear สถานะ Auto PO ออก ใช่หรือไม่ ?", 1602279459) = True Then



                        Dim Spls As New HI.TL.SplashScreen("Saving... Data,Please wait.")

                        Try
                            Dim cmdstring As String = ""

                            For Each R As DataRow In .Select("FTStateSelect='1' AND   FTStateSC ='1' AND FTPurchaseNo <>'' ")

                                cmdstring &= vbCrLf & " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing SET FTStateAutoPO='0', FDStateAutoDate='', FTStateAutoTime='', FTStateAutoBy='', FTPurchaseNo='' "
                                cmdstring &= vbCrLf & ",FTResetStateAutoPO='1', FDResetStateAutoDate=" & HI.UL.ULDate.FormatDateDB & ", FTResetStateAutoTime=" & HI.UL.ULDate.FormatTimeDB & ", FTResetStateAutoBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                cmdstring &= vbCrLf & " WHERE (FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "')  "
                                cmdstring &= vbCrLf & " And (FNHSysRawMatId = " & Val(R!FNHSysRawMatId.ToString) & ") "
                                cmdstring &= vbCrLf & " And (FNStateChange = " & Val(R!FNStateChange.ToString) & " ) "


                            Next

                            If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR) Then
                                For Each R As DataRow In .Select("FTStateSelect='1' AND   FTStateSC ='1' AND FTPurchaseNo <>'' ")

                                    'R!FTStateSelect = "0"
                                    R!FTPurchaseNo = ""



                                Next
                            End If

                        Catch ex As Exception

                        End Try

                        Spls.Close()
                        .AcceptChanges()
                    End If

                End If

            End With

            _dt.Dispose()

        End If


    End Sub
End Class