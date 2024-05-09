Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

Public Class wProdMUGroup

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        Try
            With ogvtime
                .ClearGrouping()
                .ClearDocument()
                '.Columns("FTDateTrans").Group()

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
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
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
                        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                .ExpandAllGroups()
                .RefreshData()

            End With

        Catch ex As Exception

        End Try



        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub

#End Region

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

#Region "Procedure"

    Private Sub LoadData()

        ogdtime.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            _Qry = "Exec  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_MUGroupPlan  @CmpCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text) & "' , @BuyGroupCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text) & "' , @FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            _Qry &= vbCrLf & " ,@FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' , @FTOrderNoTo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "' , @FTCustomerPO='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPO.Text) & "' , @FTCustomerPOTo ='" & HI.UL.ULF.rpQuoted(Me.FTCustomerPOTo.Text) & "'"
            _Qry &= vbCrLf & " ,@FTRawmatCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysMerMatId.Text) & "' ,@FTRawmatCodeTo='" & HI.UL.ULF.rpQuoted(Me.FNHSysMerMatIdTo.Text) & "' "
            _Qry &= vbCrLf & " ,@FTRawmatColorCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatColorId.Text) & "' ,@FTRawmatColorCodeTo='" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatColorId.Text) & "' "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            'Dim _Cmd As String = ""

            '_Cmd = ""
            '_Cmd &= vbCrLf & "select * from dbo.FN_GET_MUGroupPlan(@CmpCode , @BuyGroupCode , @FTStyleCode , @FTOrderNo , @FTOrderNoTo , @FTCustomerPO , @FTCustomerPOTo , @FTRawmatCode , @FTRawmatCodeTo , @FTRawmatColorCode , @FTRawmatColorCodeTo) A "

            '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Me.ogdtime.DataSource = _dt

            ' Call LoaddataDetailColorSize()

        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub



    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        If Me.FTCustomerPO.Text <> "" And FTCustomerPO.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTCustomerPOTo.Text <> "" And FTCustomerPOTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

#End Region


#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Call InitGrid()

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click

        If VerifyData() Then



            Call LoadData()

        End If

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)

        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles ocmsavetopage.Click
        Try


            Dim _MemuName As String = "MnuMUSetRatio"
            Dim _MethodName As String = "LoadData"
            Dim _MethodParmName As String = "FTDocumentNo"
            Dim myList As New ArrayList


            Try
                Dim _Docno As String = ""
                Dim _GroupNo As String = ""
                With DirectCast(Me.ogdtime.DataSource, DataTable)
                    .AcceptChanges()
                    If .Select("FTSelect = '1' and FTDocumentNo <> '' ").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างเอกสารได้ เนื่องจากมีการทำไปแล้ว กรุณาตรวจสอบ ", 2203031744, Me.Text)
                        Exit Sub
                    End If

                End With


                If SaveData(_Docno, _GroupNo) Then
                    LoadData()
                End If


                myList.Add(_Docno)
                myList.Add(_GroupNo)

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                HI.ST.SysInfo.MenuName = _MemuName
                Call CallByName(Me.Parent.Parent, "CallWindowForm", CallType.Method, {_MemuName, _MethodName, myList.ToArray(GetType(String))})
                HI.ST.SysInfo.MenuName = _TmpMenu

            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Dim _Docno As String = ""
            Dim _GroupNo As String = ""
            With DirectCast(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1' and FTDocumentNo <> '' ").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างเอกสารได้ เนื่องจากมีการทำไปแล้ว กรุณาตรวจสอบ ", 2203031744, Me.Text)
                    Exit Sub
                End If

            End With
            If SaveData(_Docno, _GroupNo) Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If
            LoadData()
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveData(ByRef DocumentNo As String, ByRef _GroupNo As String) As Boolean

        Try
            Dim _Cmd As String = ""
            Dim _DocNo As String = ""
            _Cmd = ""
            _DocNo = HI.TL.Document.GetDocumentNo("HITECH_PRODUCTION", "TPRODMUGroupPlan", "", "0", HI.ST.SysInfo.CmpRunID)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSelect = '1' and FTDocumentNo = '' ")

                    _GroupNo = HI.UL.ULF.rpQuoted(R!FTGroupNo.ToString)

                    _Cmd = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].. TPRODMUGroupPlan  (FTInsUser, FDInsDate, FTInsTime, FNHSysCmpId, FTCmpCode, FTFabricType "
                    _Cmd &= vbCrLf & "  , FTGroupState, FTGroupNo, FNGroupType, FTBuyMonth, FTCustomerPO, FTPOLine, FTOrderNo, "
                    _Cmd &= vbCrLf & "   FTSubOrderNo, FTStyleCode, FTSeaSonCode, FTColorWay, FNQuantity, FTRawMatCode, FTColorCode, FTSizeBreakDown, FTFabricFrontSize, FTPartCode, FTPartName, FNPurchaseRawmat, FNReserveRawmat, "
                    _Cmd &= vbCrLf & "   FNActualNewPurcchaseOrder, FNConsumtion, FNOptiplanYards,  FTDocumentNo)"

                    _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " , " & Val(Me.FNHSysCmpId.Properties.Tag.ToString)
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FNHSysCmpId.Text) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTFabricType.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTGroupState.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTGroupNo.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FNGroupType.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTBuyMonth.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPOLine.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSeaSonCode.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "'"
                    _Cmd &= vbCrLf & " ," & Val(R!FNQuantity.ToString)
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorCode.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPartCode.ToString) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPartName.ToString) & "'"
                    _Cmd &= vbCrLf & " ," & Val(R!FNPurchaseRawmat.ToString)
                    _Cmd &= vbCrLf & " ," & Val(R!FNReserveRawmat.ToString)
                    _Cmd &= vbCrLf & " ," & Val(R!FNActualNewPurcchaseOrder.ToString)
                    _Cmd &= vbCrLf & " ," & Val(R!FNConsumtion.ToString)
                    _Cmd &= vbCrLf & " ," & Val(R!FNOptiplanYards.ToString)
                    _Cmd &= vbCrLf & " ,'" & _DocNo & "'"


                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next
            End With

            DocumentNo = _DocNo



            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
            Return False
        End Try
    End Function


    Private _StateSetSelectAll As Boolean = True
    Private Sub oChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oChkSelectAll.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectAll = False
            '    Me.oChkSelectAll.Checked = False

            Dim _State As String = "0"
            If Me.oChkSelectAll.Checked Then
                _State = "1"
            End If

            With Me.ogdtime
                If Not (.DataSource Is Nothing) And Me.ogvtime.RowCount > 0 Then

                    With ogvtime
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                    CType(.DataSource, System.Data.DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
        _StateSetSelectAll = True
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If DeleteData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

            End If
            LoadData()
        Catch ex As Exception

        End Try
    End Sub


    Private Function DeleteData() As Boolean

        Try
            Dim _Cmd As String = ""
            Dim _DocNo As String = ""
            _Cmd = ""


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1' and FTDocumentNo <> '' ").Length = 0 Then
                    HI.MG.ShowMsg.mInfo("ยังไม่ได้เลือกข้อมูล กรุณาตรวจสอบ !!!!!", 2202231216, Me.Text)

                    Return False
                End If
                For Each R As DataRow In .Select("FTSelect = '1' and FTDocumentNo <> '' ")



                    _Cmd = " delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODMUGroupPlan "
                    _Cmd &= vbCrLf & "where FNHSysCmpId=" & Val(Me.FNHSysCmpId.Properties.Tag.ToString)
                    _Cmd &= vbCrLf & " and FTGroupNo='" & HI.UL.ULF.rpQuoted(R!FTGroupNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTCustomerPO.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTPOLine =  '" & HI.UL.ULF.rpQuoted(R!FTPOLine.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTOrderNo =  '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTSubOrderNo =  '" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTStyleCode =  '" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTColorWay =  '" & HI.UL.ULF.rpQuoted(R!FTColorWay.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTSizeBreakDown =  '" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                    _Cmd &= vbCrLf & " and FTPartCode = '" & HI.UL.ULF.rpQuoted(R!FTPartCode.ToString) & "'"


                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next
            End With


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function



    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        Try
            With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                If e.RowHandle <> .FocusedRowHandle OrElse e.Column.AbsoluteIndex = .FocusedColumn.AbsoluteIndex Then
                    If (e.RowHandle Mod 2 = 1) Then
                        e.Appearance.BackColor = System.Drawing.Color.LightSteelBlue
                    Else
                        e.Appearance.BackColor = System.Drawing.Color.White
                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvtime_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvtime.CellMerge
        If (e.Column.FieldName = "FNOptiplanYards") Then
            Dim view As GridView = CType(sender, GridView)
            Dim val1 As String = view.GetRowCellValue(e.RowHandle1, e.Column)
            Dim val2 As String = view.GetRowCellValue(e.RowHandle2, e.Column)

            Dim val11 As String = view.GetRowCellValue(e.RowHandle1, "FTOrderNo")
            Dim val12 As String = view.GetRowCellValue(e.RowHandle2, "FTOrderNo")
            Dim val21 As String = view.GetRowCellValue(e.RowHandle1, "FTColorWay")
            Dim val22 As String = view.GetRowCellValue(e.RowHandle2, "FTColorWay")
            Dim val31 As String = view.GetRowCellValue(e.RowHandle1, "FTRawMatCode")
            Dim val32 As String = view.GetRowCellValue(e.RowHandle2, "FTRawMatCode")
            Dim val41 As String = view.GetRowCellValue(e.RowHandle1, "FTColorCode")
            Dim val42 As String = view.GetRowCellValue(e.RowHandle2, "FTColorCode")
            Dim val51 As String = view.GetRowCellValue(e.RowHandle1, "FTPartCode")
            Dim val52 As String = view.GetRowCellValue(e.RowHandle2, "FTPartCode")

            e.Merge = (val1 = val2) And (val11 = val12) And (val21 = val22) And (val31 = val32) And (val41 = val42) And (val51 = val52)
            e.Handled = True
        End If
    End Sub

    Dim _totalOptiplanYard As Double = 0
    Dim _RowHandleHold As Integer = 0
    Private Sub ogvtime_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        Try
            Dim view As GridView = DirectCast(sender, GridView)
            Select Case e.SummaryProcess
                Case CustomSummaryProcess.Start
                    _totalOptiplanYard = 0
                Case CustomSummaryProcess.Calculate

                    If (e.RowHandle <> _RowHandleHold) And e.RowHandle > 0 Then

                        Dim RowCurent As String = (view.GetRowCellValue(e.RowHandle, "FTOrderNo") & "|" & view.GetRowCellValue(e.RowHandle, "FTPartCode") & "|" & view.GetRowCellValue(e.RowHandle, "FTRawMatCode") &
                            "|" & view.GetRowCellValue(e.RowHandle, "FTColorWay") & "|" & view.GetRowCellValue(e.RowHandle, "FTColorCode"))
                        Dim RowHold As String = (view.GetRowCellValue(_RowHandleHold, "FTOrderNo") & "|" & view.GetRowCellValue(_RowHandleHold, "FTPartCode") & "|" & view.GetRowCellValue(_RowHandleHold, "FTRawMatCode") &
                            "|" & view.GetRowCellValue(_RowHandleHold, "FTColorWay") & "|" & view.GetRowCellValue(_RowHandleHold, "FTColorCode"))

                        If RowCurent <> RowHold Then

                            _totalOptiplanYard += +Double.Parse(Val(e.FieldValue.ToString))
                        End If

                    End If
                    If (e.RowHandle = 0) Then
                        _totalOptiplanYard += +Double.Parse(Val(e.FieldValue.ToString))
                    End If
                    _RowHandleHold = e.RowHandle

                Case CustomSummaryProcess.Finalize

                        e.TotalValue = _totalOptiplanYard
            End Select

        Catch ex As Exception

        End Try
    End Sub
End Class