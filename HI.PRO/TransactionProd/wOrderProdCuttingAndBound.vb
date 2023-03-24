Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Columns

Public Class wOrderProdCuttingAndBound


    Private _LoadTableCutInfoD As New List(Of DataTable)
    Private _ListSendSuplInfo As New List(Of DataTable)

    Private _ListSendSuplBundleInfo As New List(Of DataTable)
    Private _StateMergeBundleScrap As Boolean = False


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call TabChenge()

        Dim _Qry As String
        _Qry = " SELECT TOP 1 ISNULL(FTStateMergeBundleScrap,'') AS FTStateMergeBundleScrap"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpCode) & "'"

        _StateMergeBundleScrap = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") <> "1")

        Dim _cmd As String = ""
        _cmd = " SELECT TOP 1 [COLUMN_NAME]"
        _cmd &= vbCrLf & " FROM INFORMATION_SCHEMA.COLUMNS"
        _cmd &= vbCrLf & "   WHERE [TABLE_NAME] = 'TMERTStyle_Part'"
        _cmd &= vbCrLf & "   AND [COLUMN_NAME] = 'FNHSysSeasonId'"

        Me.StateSeason = (HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_PROD) <> "")

        RepFTSuplName.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
    End Sub

    Private _StateSeason As Boolean = False
    Property StateSeason As Boolean
        Get
            Return _StateSeason
        End Get
        Set(value As Boolean)
            _StateSeason = value
        End Set
    End Property

#Region "Init Control"
    Private Sub InitGrid()

        With ogvcut
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvlayer
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvtablebound
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvbound
            .OptionsView.ShowAutoFilterRow = True
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub
#End Region

#Region "Init Data"
    Private Sub InitCutNewRow()
        With CType(Me.ogccut.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
            .Rows(.Rows.Count - 1)!FTSizeBreakDown = ""
            .Rows(.Rows.Count - 1)!FNQuantity = 0


            'FNSeq, FTSizeBreakDown, FNQuantity
            .AcceptChanges()
        End With
    End Sub

    Private Sub InitLayerNewRow()

        With CType(Me.ogclayer.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
            .Rows(.Rows.Count - 1)!FTColorway = ""
            .Rows(.Rows.Count - 1)!FTRawMatColorCode = ""
            .Rows(.Rows.Count - 1)!FTRawMatColorName = ""
            .Rows(.Rows.Count - 1)!FNLayerQuantity = 0
            .Rows(.Rows.Count - 1)!FNReqQuantity = 0
            .Rows(.Rows.Count - 1)!FNActualQuantity = 0

            Try
                .Rows(.Rows.Count - 1)!FNQuantityPerBundle = .Rows(.Rows.Count - 2)!FNQuantityPerBundle
            Catch ex As Exception
            End Try

            .AcceptChanges()
        End With
    End Sub
#End Region

#Region "Property"

#End Region

#Region "Procedure"

    Private Sub TabChenge()

        ocmpreviewpacklist.Visible = (otbdetail.SelectedTabPage.Name = otptable.Name)
        ocmsave.Visible = (otbdetail.SelectedTabPage.Name = otptable.Name)
        ocmdelete.Visible = (otbdetail.SelectedTabPage.Name = otptable.Name)
        ocmpreview.Visible = (otbdetail.SelectedTabPage.Name = otptable.Name)
        ocmaddnewlaycut.Visible = (otbdetail.SelectedTabPage.Name = otptable.Name)
        ocmcreatebound.Visible = (otbdetail.SelectedTabPage.Name = otpbundle.Name)
        ocmdeletebound.Visible = (otbdetail.SelectedTabPage.Name = otpbundle.Name)
        ocmsavesendsupl.Visible = (otbdetail.SelectedTabPage.Name = otpsendsupl.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Public Sub SetInfo(ByVal Key As Object)
        '...call by another form name zzz...
        FTOrderNo.Text = Key.ToString
    End Sub

    Public Sub LoadOrderProdDataInfo(ByVal Key As Object)

        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        Try
            otbdetail.SelectedTabPageIndex = 0

            otbjobprod.TabPages.Clear()
            Dim _Qry As String = ""
            Dim _dtprod As DataTable

            _Qry = "SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTOrderProdNo  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P With(Nolock)"
            _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "'  "
            _Qry &= vbCrLf & "  Order By FTOrderProdNo  "

            _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            For Each R As DataRow In _dtprod.Rows

                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                With Otp
                    .Name = R!FTOrderProdNo.ToString
                    .Text = R!FTOrderProdNo.ToString
                End With

                otbjobprod.TabPages.Add(Otp)

            Next

            If _dtprod.Rows.Count > 0 Then
                otbjobprod.SelectedTabPageIndex = 0
            End If

            _dtprod.Dispose()

        Catch ex As Exception

        End Try


        _Spls.Close()
    End Sub

#End Region

#Region "Order Prod Mark Cutting"

    Private Sub LoadOrderProdMarkCutting(OrderProdKey As Object)
        ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        otbmarkcutting.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = " SELECT A.FNHSysMarkId,B.FTMarkCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,B.FTMarkNameTH AS FTMarkName"
        Else
            _Qry &= vbCrLf & " ,B.FTMarkNameEN AS FTMarkName"
        End If

        _Qry &= vbCrLf & "    FROM"
        _Qry &= vbCrLf & "  (SELECT     1 AS FNSeq, FTOrderProdNo, FNHSysMarkId"
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT     2 AS FNSeq, FTOrderProdNo, FNHSysSubMarkId AS FNHSysMarkId"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub  WITH(NOLOCK)   "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & " ) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS B   WITH(NOLOCK)  ON A.FNHSysMarkId = B.FNHSysMarkId"
        _Qry &= vbCrLf & " INNER JOIN  ( SELECT DISTINCT  FNHSysMarkId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut WITH(NOLOCK) WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "'   ) AS C ON A.FNHSysMarkId = C.FNHSysMarkId"
        _Qry &= vbCrLf & "  Order BY A.FNSeq,B.FTMarkCode"

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FNHSysMarkId.ToString
                .Text = R!FTMarkCode.ToString & " ( " & R!FTMarkName.ToString & " ) "
            End With

            otbmarkcutting.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbmarkcutting.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()
        ' _Spls.Close()

    End Sub

    Private Function ValidateLaycut(Optional StateSave As Boolean = True) As Boolean
        If Not (Me.otbtable.SelectedTabPage Is Nothing) Then

            If (StateSave) Then

                If Me.FTLaycutDate.Text <> "" Then
                    Return True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTLaycutDate_lbl.Text)
                    FTLaycutDate.Focus()
                    FTLaycutDate.SelectAll()
                    Return False
                End If

            Else
                Return True
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Table Cutting", 1404210001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function SaveLayCut() As Boolean

        Try
            CType(ogccut.DataSource, DataTable).AcceptChanges()
            CType(ogclayer.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try

        Dim _FTLayCutNo As String = ""
        Dim _Qry As String = ""

        Select Case otbcutbound.SelectedTabPage.Name.ToString.ToUpper
            Case "NEW".ToUpper
                _FTLayCutNo = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD), "TPRODTLayCut", "", False, HI.ST.SysInfo.CmpRunID)
            Case Else
                _FTLayCutNo = otbcutbound.SelectedTabPage.Name.ToString
        End Select

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            _Qry = " Select TOp 1 FTOrderProdNo  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut "
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTLayCutNo, FTLaycutDate, FTOrderProdNo, FNHSysMarkId"
                _Qry &= vbCrLf & " , FNTableNo, FTNote, FNHSysCmpId, FNPrice, FNY1, FNY2, FNY3"
                _Qry &= vbCrLf & " , FNY4, FNY5, FNY6, FNY7, FNY8, FNKG1, FNKG2, FNKG3, FNKG4, FNM1,FNCuttingUnit) "
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FTLaycutDate.Text) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                _Qry &= vbCrLf & "," & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
                _Qry &= vbCrLf & "," & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Name.ToString)) & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                _Qry &= vbCrLf & "," & FNPrice.Value & ""
                _Qry &= vbCrLf & "," & oceY1.Value & "  "
                _Qry &= vbCrLf & "," & oceY2.Value & "  "
                _Qry &= vbCrLf & "," & oceY3.Value & "  "
                _Qry &= vbCrLf & "," & oceY4.Value & "  "
                _Qry &= vbCrLf & "," & oceY5.Value & "  "
                _Qry &= vbCrLf & "," & oceY6.Value & "  "
                _Qry &= vbCrLf & "," & oceY7.Value & "  "
                _Qry &= vbCrLf & "," & oceY8.Value & "  "
                _Qry &= vbCrLf & "," & oceKG1.Value & "  "
                _Qry &= vbCrLf & "," & oceKG2.Value & "  "
                _Qry &= vbCrLf & "," & oceKG3.Value & "  "
                _Qry &= vbCrLf & "," & oceKG4.Value & "  "
                _Qry &= vbCrLf & "," & oceM1.Value & "  "
                _Qry &= vbCrLf & "," & FNCuttingUnit.SelectedIndex & "  "

            Else

                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut "
                _Qry &= vbCrLf & " SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,FTNote='" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
                _Qry &= vbCrLf & ",FNPrice=" & FNPrice.Value & ""
                _Qry &= vbCrLf & ",FNY1=" & oceY1.Value & "  "
                _Qry &= vbCrLf & ",FNY2=" & oceY2.Value & "  "
                _Qry &= vbCrLf & ",FNY3=" & oceY3.Value & "  "
                _Qry &= vbCrLf & ",FNY4=" & oceY4.Value & "  "
                _Qry &= vbCrLf & ",FNY5=" & oceY5.Value & "  "
                _Qry &= vbCrLf & ",FNY6=" & oceY6.Value & "  "
                _Qry &= vbCrLf & ",FNY7=" & oceY7.Value & "  "
                _Qry &= vbCrLf & ",FNY8=" & oceY8.Value & "  "
                _Qry &= vbCrLf & ",FNKG1=" & oceKG1.Value & "  "
                _Qry &= vbCrLf & ",FNKG2=" & oceKG2.Value & "  "
                _Qry &= vbCrLf & ",FNKG3=" & oceKG3.Value & "  "
                _Qry &= vbCrLf & ",FNKG4=" & oceKG4.Value & "  "
                _Qry &= vbCrLf & ",FNM1=" & oceM1.Value & "  "
                _Qry &= vbCrLf & ",FNCuttingUnit=" & FNCuttingUnit.SelectedIndex & "  "
                _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            Dim Rind As Integer = 0
            With CType(ogccut.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTSizeBreakDown<>'' AND  FNQuantity > 0 ", "FNSeq")
                    Rind = Rind + 1

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio "
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTLayCutNo, FNSeq, FTSizeBreakDown, FNQuantity,FNHSysCmpId) "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "
                    _Qry &= vbCrLf & " ," & Rind & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNQuantity.ToString)) & " "
                    _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Next
            End With

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            Rind = 0
            With CType(ogclayer.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTColorway<>'' AND  FNLayerQuantity > 0  ", "FNSeq")
                    Rind = Rind + 1

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D"
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTLayCutNo, FNSeq, FTColorway, FNLayerQuantity, FNReqQuantity, FNActualQuantity,FNQuantityPerBundle,FTNikePOLineItem,FNHSysCmpId) "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "
                    _Qry &= vbCrLf & " ," & Rind & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorwayData.ToString) & "' "
                    _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNLayerQuantity.ToString)) & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNReqQuantity.ToString) & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNActualQuantity.ToString) & " "
                    _Qry &= vbCrLf & " ," & Val(R!FNQuantityPerBundle.ToString) & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "' "
                    _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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


    Private Function DeleteLayCut() As Boolean

        Try
            CType(ogccut.DataSource, DataTable).AcceptChanges()
            CType(ogclayer.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try

        Dim _FTLayCutNo As String = ""
        Dim _Qry As String = ""


        _FTLayCutNo = otbcutbound.SelectedTabPage.Name.ToString


        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            _Qry = " Delete  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False

            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Qry = " Delete  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut "
            _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(_FTLayCutNo) & "' "

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


#End Region

#Region "Table Cutting"

    Private Sub LoadOrderProdMarkTableCutting(OrderProdKey As Object, MarkID As Object)
        ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        otbtable.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = " SELECT FNTableNo "
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTOrderProd_TableCut  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & "  AND FNHSysMarkId =" & Val(MarkID.ToString) & " "
        _Qry &= vbCrLf & "  Order BY FNTableNo"

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FNTableNo.ToString
                .Text = R!FNTableNo.ToString
            End With

            otbtable.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbtable.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()
        ' _Spls.Close()

    End Sub

    Private Sub LoadTableCuttingLayCut(TableKey As Object)
        Dim _Qry As String = ""

        Dim _dttmp As DataTable

        _Qry = " Select TOp 1 A.FTNote, A.FNHSysUnitSectId,ISNULL(B.FTUnitSectCode,'') AS FTUnitSectCode,ISNULL(A.FTStateRepair,'') AS FTStateRepair  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS A WITH(NOLOCK)  "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS B WITH(NOLOCK) "
        _Qry &= vbCrLf & " ON A.FNHSysUnitSectId=B.FNHSysUnitSectId "
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
        _Qry &= vbCrLf & " AND  A.FNHSysMarkId=" & Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString) & " "
        _Qry &= vbCrLf & " AND A.FNTableNo=" & Integer.Parse(Val(TableKey.ToString)) & " "

        _dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        FTStateRepair.Checked = False

        For Each R As DataRow In _dttmp.Rows

            FTStateRepair.Checked = (R!FTStateRepair.ToString = "1")
            Exit For
        Next

        _dttmp.Dispose()

        Call InitDataBraekDown()
        Call InitDataColorWay()

        Call LoadTableCutInfoD()
        Call LoadLayCutNo(Me.otbjobprod.SelectedTabPage.Name.ToString, Val(Me.otbmarkcutting.SelectedTabPage.Name.ToString), Integer.Parse(Val(TableKey.ToString)))
    End Sub

    Private Sub LoadLayCutNo(OrderProdKey As Object, MarkID As Object, TableKey As Object)
        ' Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")
        otbcutbound.TabPages.Clear()
        Dim _Qry As String = ""
        Dim _dtprod As DataTable

        _Qry = " SELECT FTLayCutNo "
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(OrderProdKey.ToString) & "' "
        _Qry &= vbCrLf & "  AND FNHSysMarkId =" & Val(MarkID.ToString) & " "
        _Qry &= vbCrLf & "  AND FNTableNo =" & Val(TableKey.ToString) & " "

        _Qry &= vbCrLf & "  Order BY FTLayCutNo"

        _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In _dtprod.Rows

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = R!FTLayCutNo.ToString
                .Text = R!FTLayCutNo.ToString
            End With

            otbcutbound.TabPages.Add(Otp)

        Next

        If _dtprod.Rows.Count > 0 Then
            otbcutbound.SelectedTabPageIndex = 0
        End If

        _dtprod.Dispose()
        ' _Spls.Close()

    End Sub

    Private Sub LoadLayCutNoInfo(LayCutKey As Object)
        Dim _Qry As String = ""

        Dim _dttmp As DataTable

        _Qry = " Select Top 1 FTLayCutNo, FTLaycutDate, FTOrderProdNo, FNHSysMarkId, FNTableNo, FTNote, FNHSysCmpId, FNPrice, FNY1, FNY2, FNY3, "
        _Qry &= vbCrLf & "  FNY4, FNY5, FNY6, FNY7, FNY8, FNKG1, FNKG2, FNKG3, FNKG4 ,FNCuttingUnit,FNM1"
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK)  "
        _Qry &= vbCrLf & " WHERE A.FTLayCutNo='" & HI.UL.ULF.rpQuoted(LayCutKey.ToString) & "' "

        _dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Call SetDefaultValue()
        Me.FTLaycutDate.Text = ""

        For Each R As DataRow In _dttmp.Rows

            Try
                Me.FTLaycutDate.DateTime = HI.UL.ULDate.ConvertEnDB(R!FTLaycutDate.ToString)
            Catch ex As Exception
                Me.FTLaycutDate.Text = HI.UL.ULDate.ConvertEN(R!FTLaycutDate.ToString)
            End Try

            FTRemark.Text = R!FTNote.ToString
            FNCuttingUnit.SelectedIndex = Val(R!FNCuttingUnit.ToString)
            FNPrice.Value = Val(R!FNPrice.ToString)
            oceM1.Value = Val(R!FNM1.ToString)

            oceKG1.Value = Val(R!FNKG1.ToString)
            oceKG2.Value = Val(R!FNKG2.ToString)
            oceKG3.Value = Val(R!FNKG3.ToString)
            oceKG4.Value = Val(R!FNKG4.ToString)

            oceY1.Value = Val(R!FNY1.ToString)
            oceY2.Value = Val(R!FNY2.ToString)
            oceY3.Value = 36
            oceY4.Value = Val(R!FNY4.ToString)
            oceY5.Value = Val(R!FNY5.ToString)
            oceY6.Value = Val(R!FNY6.ToString)
            oceY7.Value = Val(R!FNY7.ToString)
            ' oceY8.Value = Val(R!FNY8.ToString)

            Exit For
        Next


        If LayCutKey.ToString.ToUpper = "NEW" Then
            Try
                Me.FTLaycutDate.DateTime = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Catch ex As Exception
            End Try
        End If

        Call LoadLayCutNoInfoD(LayCutKey)

        _dttmp.Dispose()

    End Sub


    Private Sub InitDataBraekDown()
        Dim dt As DataTable
        Dim _Qry As String = ""

        _Qry = "   SELECT        A.FTSizeBreakDown,  B.FNMatSizeSeq"
        _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS A  WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS B WITH(NOLOCK) ON A.FTSizeBreakDown = B.FTMatSizeCode"
        _Qry &= vbCrLf & " WHERE        (A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "') "
        _Qry &= vbCrLf & " AND (A.FNHSysMarkId =" & Val(otbmarkcutting.SelectedTabPage.Name.ToString) & ") "
        _Qry &= vbCrLf & " AND (A.FNTableNo =" & Val(otbtable.SelectedTabPage.Name.ToString) & ")"
        _Qry &= vbCrLf & " GROUP BY A.FTSizeBreakDown, B.FNMatSizeSeq"
        _Qry &= vbCrLf & " ORDER BY ISNULL(B.FNMatSizeSeq, 0)"
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.RepFTSizeBreakDown
            .DataSource = dt.Copy
        End With

    End Sub

    Private Sub InitDataColorWay()

        Dim dt As DataTable
        Dim _Qry As String = ""

        '_Qry = "  SELECT     A.FTColorway, MC.FTRawMatColorCode"

        'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '    _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameTH,'') AS FTRawMatColorName"
        'Else
        '    _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameEN,'') AS FTRawMatColorName "
        'End If

        '_Qry &= vbCrLf & " , ISNULL((Select TOP 1 Z.FNLayer"
        '_Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS Z WITH(NOLOCK)"
        '_Qry &= vbCrLf & "  WHERE        (Z.FTOrderProdNo = A.FTOrderProdNo )"
        '_Qry &= vbCrLf & "   AND (Z.FNHSysMarkId = A.FNHSysMarkId)"
        '_Qry &= vbCrLf & "   AND (Z.FTColorway = A.FTColorway) "
        '_Qry &= vbCrLf & "  AND (FNTableNo = A.FNTableNo)),0) AS FNLayer "
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS A WITH(NOLOCK)  INNER JOIN"
        '_Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK)  ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON B.FNHSysRawMatColorId = MC.FNHSysRawMatColorId"
        '_Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' "
        '_Qry &= vbCrLf & "  AND  A.FNHSysMarkId=" & Val(otbmarkcutting.SelectedTabPage.Name.ToString) & " "
        '_Qry &= vbCrLf & "  AND  A.FNTableNo=" & Val(otbtable.SelectedTabPage.Name.ToString) & " "


        _Qry = "  SELECT     A.FTColorway, MC.FTRawMatColorCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameTH,'') AS FTRawMatColorName"
        Else
            _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameEN,'') AS FTRawMatColorName "
        End If

        _Qry &= vbCrLf & " , ISNULL((Select TOP 1 Z.FNLayer"
        _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS Z WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE        (Z.FTOrderProdNo = A.FTOrderProdNo )"
        _Qry &= vbCrLf & "   AND (Z.FNHSysMarkId = A.FNHSysMarkId)"
        _Qry &= vbCrLf & "   AND (Z.FTColorway = A.FTColorway) "
        _Qry &= vbCrLf & "  AND (FNTableNo = A.FNTableNo)),0) AS FNLayer "
        _Qry &= vbCrLf & "  ,ISNULL(QCN.FTNikePOLineItem,'') AS FTNikePOLineItem, (A.FTColorway +ISNULL(QCN.FTNikePOLineItem,'')  ) AS FTColorwayPOItem"

        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK)  ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC WITH(NOLOCK) ON B.FNHSysRawMatColorId = MC.FNHSysRawMatColorId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN ("

        _Qry &= vbCrLf & "  SELECT A.FTColorway, C.FTNikePOLineItem"
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS B WITH(NOLOCK) "
        _Qry &= vbCrLf & " ON A.FTOrderProdNo = B.FTOrderProdNo AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS C WITH(NOLOCK) "
        _Qry &= vbCrLf & " ON B.FTSubOrderNo = C.FTSubOrderNo AND B.FTColorway = C.FTColorway AND B.FTSizeBreakDown = C.FTSizeBreakDown"
        _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' "
        _Qry &= vbCrLf & "  AND  A.FNHSysMarkId=" & Val(otbmarkcutting.SelectedTabPage.Name.ToString) & " "
        _Qry &= vbCrLf & "  AND  A.FNTableNo=" & Val(otbtable.SelectedTabPage.Name.ToString) & " "
        _Qry &= vbCrLf & " GROUP BY A.FTColorway, C.FTNikePOLineItem"

        _Qry &= vbCrLf & ") AS QCN ON A.FTColorway = QCN.FTColorway"


        _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' "
        _Qry &= vbCrLf & "  AND  A.FNHSysMarkId=" & Val(otbmarkcutting.SelectedTabPage.Name.ToString) & " "
        _Qry &= vbCrLf & "  AND  A.FNTableNo=" & Val(otbtable.SelectedTabPage.Name.ToString) & " "
        _Qry &= vbCrLf & "  ORDER BY A.FTColorway,ISNULL(QCN.FTNikePOLineItem,'')"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        With Me.RepFTColorway
            .DataSource = dt.Copy
        End With

    End Sub

    Private Sub LoadTableCutInfoD()

        _LoadTableCutInfoD.Clear()
        Dim dtnew As DataTable
        Dim dt As DataTable
        Dim _Qry As String = ""
        Dim _RowIndex As Integer = 0
        _Qry = "Select TOP 0  FNSeq, FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTLayCutNo='' "
        _Qry &= vbCrLf & " ORDER BY FNSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


        _Qry = "   SELECT        A.FTSizeBreakDown, Max(A.FNAssort) AS FNAssort, B.FNMatSizeSeq"
        _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS A  WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS B WITH(NOLOCK) ON A.FTSizeBreakDown = B.FTMatSizeCode"
        _Qry &= vbCrLf & " WHERE        (A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "') "
        _Qry &= vbCrLf & " AND (A.FNHSysMarkId =" & Val(otbmarkcutting.SelectedTabPage.Name.ToString) & ") "
        _Qry &= vbCrLf & " AND (A.FNTableNo =" & Val(otbtable.SelectedTabPage.Name.ToString) & ")"
        _Qry &= vbCrLf & " GROUP BY A.FTSizeBreakDown, B.FNMatSizeSeq"
        _Qry &= vbCrLf & " ORDER BY ISNULL(B.FNMatSizeSeq, 0)"

        dtnew = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        dt.Rows.Clear()

        For Each R As DataRow In dtnew.Rows

            For I As Integer = 1 To Integer.Parse(Val(R!FNAssort))

                _RowIndex = _RowIndex + 1
                dt.Rows.Add(_RowIndex, R!FTSizeBreakDown.ToString, 1)

            Next

        Next

        _LoadTableCutInfoD.Add(dt.Copy)

        dtnew.Dispose()
        dt.Dispose()
    End Sub


    Private Sub LoadLayCutNoInfoD(LayCutKey As Object)
        Me.ogccut.DataSource = Nothing
        Me.ogclayer.DataSource = Nothing

        Dim dt As DataTable
        Dim _Qry As String = ""

        _Qry = "Select  FNSeq, FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTLayCutNo='" & HI.UL.ULF.rpQuoted(LayCutKey.ToString) & "' "
        _Qry &= vbCrLf & " ORDER BY FNSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Select Case LayCutKey.ToString.ToUpper
            Case "NEW"

                dt = _LoadTableCutInfoD(0).Copy

            Case Else

        End Select

        Me.ogccut.DataSource = dt.Copy

        _Qry = "  SELECT   A.FNSeq, (A.FTColorway +ISNULL(A.FTNikePOLineItem,'') ) AS FTColorway, MC.FTRawMatColorCode, A.FNLayerQuantity, A.FNReqQuantity, A.FNActualQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameTH,'') AS FTRawMatColorName"
        Else
            _Qry &= vbCrLf & " , ISNULL(MC.FTRawMatColorNameEN,'') AS FTRawMatColorName "
        End If

        _Qry &= vbCrLf & " , A.FNQuantityPerBundle,ISNULL(A.FTNikePOLineItem,'') AS FTNikePOLineItem,A.FTColorway AS FTColorwayData "
        _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS H WITH(NOLOCK) ON A.FTLayCutNo = H.FTLayCutNo INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS PC WITH(NOLOCK) ON H.FTOrderProdNo = PC.FTOrderProdNo AND H.FNHSysMarkId = PC.FNHSysMarkId AND H.FNTableNo = PC.FNTableNo AND "
        _Qry &= vbCrLf & "   A.FTColorway = PC.FTColorway INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM ON PC.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS MC ON IM.FNHSysRawMatColorId = MC.FNHSysRawMatColorId"
        _Qry &= vbCrLf & " WHERE A.FTLayCutNo='" & HI.UL.ULF.rpQuoted(LayCutKey.ToString) & "' "
        _Qry &= vbCrLf & " Order By  A.FNSeq  "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogclayer.DataSource = dt.Copy

        Call InitCutNewRow()
        Call InitLayerNewRow()

        dt.Dispose()

    End Sub

#End Region

#Region "Bundle"

    Private Sub LoadLayCutCreateBundle()

        Dim _Qry As String = ""
        Dim dt As DataTable
        ogctablebound.DataSource = Nothing
        If otbjobprod.SelectedTabPage Is Nothing Then
            Exit Sub
        End If

        _Qry = "   SELECT        '1' AS FTSelect"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , C.FTMarkCode + ' ('+ C.FTMarkNameTH +')' AS FNHSysMarkId "
        Else
            _Qry &= vbCrLf & " , C.FTMarkCode + ' ('+ C.FTMarkNameEN +')' AS FNHSysMarkId "
        End If

        _Qry &= vbCrLf & " , A.FNHSysMarkId As FNHSysMarkId_Hide, A.FNTableNo"
        _Qry &= vbCrLf & " , ISNULL(Cxx2.FTShades,'') AS FTShades"
        _Qry &= vbCrLf & "  FROM ( "
        _Qry &= vbCrLf & "  SELECT A.FTOrderProdNo,A.FNHSysMarkId,A.FNTableNo "
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS B WITH(NOLOCK)  ON A.FTOrderProdNo = B.FTOrderProdNo AND A.FNHSysMarkId = B.FNHSysMarkId "
        _Qry &= vbCrLf & "  WHERE    A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' "
        _Qry &= vbCrLf & "  UNION"
        _Qry &= vbCrLf & "  SELECT A.FTOrderProdNo,A.FNHSysMarkId,A.FNTableNo "
        _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut AS TC WITH (NOLOCK) ON A.FTOrderProdNo = TC.FTOrderProdNo AND A.FNHSysMarkId = TC.FNHSysMarkId AND A.FNTableNo = TC.FNTableNo INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkSub AS B WITH (NOLOCK) ON TC.FNHSysMarkId = B.FNHSysSubMarkId AND TC.FTOrderProdNo = B.FTOrderProdNo"
        _Qry &= vbCrLf & "  WHERE    A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "' "
        _Qry &= vbCrLf & "  AND ( ISNULL(TC.FTStateRepair,'') ='1' OR ISNULL(TC.FTStateBundle,'') ='1'  ) "
        _Qry &= vbCrLf & "  ) AS A"
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C WITH(NOLOCK)  ON A.FNHSysMarkId = C.FNHSysMarkId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN  "
        _Qry &= vbCrLf & "  ("
        _Qry &= vbCrLf & "   SELECT        A.FTOrderProdNo, A.FNHSysMarkId, A.FNTableNo"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS B WITH(NOLOCK) ON A.FTLayCutNo = B.FTLayCutNo"
        _Qry &= vbCrLf & " GROUP BY A.FTOrderProdNo, A.FNHSysMarkId, A.FNTableNo"
        _Qry &= vbCrLf & "  ) AS Z"
        _Qry &= vbCrLf & "  ON A.FTOrderProdNo = Z.FTOrderProdNo"
        _Qry &= vbCrLf & "  AND A.FNHSysMarkId = Z.FNHSysMarkId"
        _Qry &= vbCrLf & "  AND A.FNTableNo = Z.FNTableNo"

        _Qry &= vbCrLf & " Outer apply (select top 1  Cxx2.FTShades from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS Cxx2 WITH(NOLOCK) WHERE Cxx2.FTOrderProdNo =A.FTOrderProdNo AND Cxx2.FNHSysMarkId =A.FNHSysMarkId AND Cxx2.FNTableNo =A.FNTableNo    ) AS Cxx2 "

        _Qry &= vbCrLf & " WHERE        (A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "')"
        _Qry &= vbCrLf & "  AND Z.FNTableNo IS NULL "
        _Qry &= vbCrLf & " GROUP BY C.FTMarkCode, C.FTMarkNameTH, C.FTMarkNameEN, A.FNHSysMarkId, A.FNTableNo, ISNULL(Cxx2.FTShades,'')"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        ogctablebound.DataSource = dt.Copy
        dt.Dispose()

        Call LoadBundleData()

    End Sub

    Private Sub CreateBundle()
        Dim _Qry As String = ""

        CType(ogctablebound.DataSource, DataTable).AcceptChanges()

        If CType(ogctablebound.DataSource, DataTable).Select("FTSelect='1'").Length <= 0 Then
            Exit Sub
        End If

        Dim _Spls As New HI.TL.SplashScreen("Creating......   Please Wait...")

        Try

            Dim _RowInd As Integer = 0

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateBundle WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateBundle(FTUserLogIn, FTLayCutNo, FNLayCutSeq, FTColorway, FTSizeBreakDown, FNQuantity ,FNQuantityPerBundle,FNRatioSeq,FTPOLineItemNo)"
            _Qry &= vbCrLf & " SELECT    '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTUserLogIn,   B.FTLayCutNo, B.FNSeq, B.FTColorway, C.FTSizeBreakDown,Convert(numeric(18,0),B.FNLayerQuantity* C.FNQuantity) AS FNQuantity,B.FNQuantityPerBundle,C.FNSeq,ISNULL(B.FTNikePOLineItem,'') AS FTPOLineItemNo"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_D AS B WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A WITH(NOLOCK)  ON B.FTLayCutNo = A.FTLayCutNo INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut_Ratio AS C  WITH(NOLOCK) ON B.FTLayCutNo = C.FTLayCutNo"
            _Qry &= vbCrLf & " WHERE B.FTLayCutNo <> ''  "
            _Qry &= vbCrLf & "  AND ( "

            For Each R As DataRow In CType(ogctablebound.DataSource, DataTable).Select("FTSelect='1'")

                If _RowInd > 0 Then
                    _Qry &= vbCrLf & " OR "
                End If

                _Qry &= vbCrLf & " ( "
                _Qry &= vbCrLf & " A.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "'"
                _Qry &= vbCrLf & " AND A.FNHSysMarkId =" & Val(R!FNHSysMarkId_Hide.ToString) & ""
                _Qry &= vbCrLf & " AND A.FNTableNo =" & Val(R!FNTableNo.ToString) & ""
                _Qry &= vbCrLf & " ) "

                _RowInd = _RowInd + 1

            Next

            _Qry &= vbCrLf & "  ) "
            _Qry &= vbCrLf & " GROUP BY B.FTLayCutNo, B.FNSeq, B.FTColorway, C.FTSizeBreakDown,B.FNLayerQuantity,  C.FNQuantity,C.FNSeq,B.FNQuantityPerBundle,ISNULL(B.FTNikePOLineItem,'') "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Select Case FNLayCutBoundType.SelectedIndex
                Case 0
                    _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_BUNDLE_AUTO_BY_LINE_ITEM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpRunID) & "' "

                Case 1
                    _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_BUNDLE_AUTO_MERGE_BY_LINE_ITEM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpRunID) & "' "
            End Select

            'Select Case FNLayCutBoundType.SelectedIndex
            '    Case 0
            '        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_BUNDLE_AUTO_BY_LINE_ITEM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpRunID) & "' "
            '    Case 1
            '        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_BUNDLE_AUTO_NOTMERGE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpRunID) & "' "
            '    Case 2
            '        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CREATE_BUNDLE_AUTO '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.CmpRunID) & "' "
            'End Select

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Call LoadLayCutCreateBundle()

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

    End Sub

    Private Sub LoadBundleData()
        Dim dt As DataTable
        Dim _Qry As String = ""

        Me.ogcbound.DataSource = Nothing

        _Qry = "SELECT      '0' AS FTSelect,  A.FTBarcodeBundleNo, A.FNBunbleSeq, A.FTColorway,ISNULL(A.FTPOLineItemNo,'') AS FTPOLineItemNo, A.FTSizeBreakDown, A.FNQuantity, B.FTLayCutNo"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , M.FTMarkCode + ' ('+ M.FTMarkNameTH +')' AS FTMarkName "
            _Qry &= vbCrLf & " , IMC.FTRawMatColorNameTH AS FTRawMatColorName "
        Else
            _Qry &= vbCrLf & " , M.FTMarkCode + ' ('+ M.FTMarkNameEN +')' AS FTMarkName "
            _Qry &= vbCrLf & " , IMC.FTRawMatColorNameEN AS FTRawMatColorName "
        End If

        _Qry &= vbCrLf & " 	, B.FNLayCutSeq, C.FNHSysMarkId, C.FNTableNo,B.FNQuantity AS FNQuantityLayCut "
        ' _Qry &= vbCrLf & "  ,  IMC.FTRawMatColorCode, IMC.FNRawMatColorSeq"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A  WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS B WITH(NOLOCK)  ON A.FTBarcodeBundleNo = B.FTBarcodeBundleNo AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS C WITH(NOLOCK)  ON B.FTLayCutNo = C.FTLayCutNo INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS M WITH(NOLOCK)  ON C.FNHSysMarkId = M.FNHSysMarkId INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_PO_Rawmat AS D WITH(NOLOCK)  ON C.FTOrderProdNo = D.FTOrderProdNo AND C.FNHSysMarkId = D.FNHSysMarkId AND C.FNTableNo = D.FNTableNo AND "
        _Qry &= vbCrLf & "    B.FTColorway = D.FTColorway INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON D.FNHSysRawMatId = IM.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)  ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"
        _Qry &= vbCrLf & " WHERE A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "'"
        _Qry &= vbCrLf & " Order By  A.FNBunbleSeq "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        ogcbound.DataSource = dt.Copy
        Call InitialGridMergCell()
        dt.Dispose()
    End Sub
#End Region

#Region "Function"
    Private Function CheckBoundTable() As Boolean
        Dim _State As Boolean = False
        Dim _Qry As String = ""

        _Qry = " Select TOP 1  A.FTBarcodeBundleNo"
        _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B WITH(NOLOCK)   ON A.FTLayCutNo = B.FTLayCutNo"
        _Qry &= vbCrLf & "  WHERE        (B.FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "')"
        _Qry &= vbCrLf & "   AND (B.FNHSysMarkId =" & Val(otbmarkcutting.SelectedTabPage.Name.ToString) & ") "
        _Qry &= vbCrLf & "  AND (B.FNTableNo =" & Val(otbtable.SelectedTabPage.Name.ToString) & ")"

        _State = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")

        If (_State) Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลการจัดมัดแล้ว ไม่สามารถ ทำการ ลบ เพิ่ม หรือ แก้ไขได้ !!!!", 1504150002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _State
    End Function


    Private Function CheckCreateBarcode(Optional StateShowMsg As Boolean = True) As Boolean
        Dim _State As Boolean = False
        Dim _Qry As String = ""

        _Qry = " Select TOP 1  FTBarcodeBundleNo "
        _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE        (FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(otbjobprod.SelectedTabPage.Name.ToString) & "')"
        _Qry &= vbCrLf & "  AND  ISNULL(FTStateGenBarcode,'') ='1' "

        _State = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")

        If (_State) Then

            If (StateShowMsg) Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการสร้าง Barcode แล้ว ไม่สามารถ ทำการ ลบ เพิ่ม หรือ แก้ไขได้ !!!!", 1505310001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return _State
    End Function

#End Region

#Region "Send Supl Contract"

    Private Sub LoadSupplier()
        Dim _Qry As String
        _Qry = "SELECT        FNHSysSuplId"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " , FTSuplCode +' :: ' + FTSuplNameTH  AS FTSuplName "
        Else
            _Qry &= vbCrLf & " , FTSuplCode +' :: ' + FTSuplNameEN  AS FTSuplName "
        End If

        _Qry &= vbCrLf & " , FTSuplCode"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE        (FTStateActive = '1')"
        _Qry &= vbCrLf & "  AND (FTStateSubContact = '1')"
        _Qry &= vbCrLf & "  Order BY FTSuplName "

        Me.RepFTSuplName.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

    End Sub

    Private Sub LoadOperation(_FTOrderProdNo As String)
        Dim _Qry As String = ""
        _Qry = "SELECT TOP 1 FTOrderProdNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
            _Qry = "   SELECT     A.FNHSysOperationId"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameTH  AS FTOperationName "
            Else
                _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameEN  AS FTOperationName "
            End If

            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
            _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " AND FNOperationState =2 "
            _Qry &= vbCrLf & "  ORDER BY A.FNSeq "

        Else

            _Qry = "   SELECT     A.FNHSysOperationId"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameTH  AS FTOperationName "
            Else
                _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameEN  AS FTOperationName "
            End If

            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
            _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "' AND FNOperationState =2  "
            _Qry &= vbCrLf & "  ORDER BY A.FNSeq "

        End If

        Dim dt As DataTable
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.RepFTOperationNameS.DataSource = dt.Copy
        Me.RepFTOperationNameT.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub LoadPart()

        Dim dt As DataTable
        Dim _Qry As String
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
        Dim _FTOrderProdNo As String = ""
        Dim _FNHSysSeasonId As Integer

        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            _FTOrderProdNo = Me.otbjobprod.SelectedTabPage.Name.ToString
        End If

        _Qry = "SELECT TOP 1 FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS X WITH(NOLOCK) WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
        _FNHSysSeasonId = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, ""))

        _Qry = " SELECT A.FNHSysPartId"
        _Qry &= vbCrLf & " 	,P.FTPartCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameTH,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName"
        Else
            _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameEN,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameEN,'') AS FNSendSuplTypeName "
        End If

        _Qry &= vbCrLf & " , A.FNSendSuplType"
        _Qry &= vbCrLf & " ,ISNULL(SPLN.FTNote,'') AS FTNote"
        _Qry &= vbCrLf & " , Convert(varchar(30),A.FNHSysPartId) + '|' +P.FTPartCode + '|' + Convert(varchar(30),A.FNSendSuplType) +'|' + ISNULL(SPLN.FTNote,'')  AS FTSenSuplDataRef "
        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId = " & _FNHSysStyleId & " "

        If StateSeason Then
            _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        End If

        _Qry &= vbCrLf & "   And FTStateEmb = 1"

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "    WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        If StateSeason Then
            _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        End If

        _Qry &= vbCrLf & "   And FTStatePrint = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        If StateSeason Then
            _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        End If

        _Qry &= vbCrLf & "   And FTStateHeat = 1"
        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        If StateSeason Then
            _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        End If

        _Qry &= vbCrLf & "   And FTStateLaser = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        If StateSeason Then
            _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        End If

        _Qry &= vbCrLf & "   And FTStateWindows = 1"
        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,6 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " "

        If StateSeason Then
            _Qry &= vbCrLf & "   And (FNHSysSeasonId =" & _FNHSysSeasonId & " OR ISNULL(FNHSysSeasonId,0)<=0) "
        End If



        _Qry &= vbCrLf & "   And FTStateNonEmbroidry = 1"
        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & "  ON A.FNSendSuplType = LN.FNListIndex "

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * "

        If StateSeason Then
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FNT_GetPartSendSuplDescBySeason(" & _FNHSysStyleId & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'," & _FNHSysSeasonId & ")  "
        Else
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FNT_GetPartSendSuplDesc(" & _FNHSysStyleId & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "')  "
        End If

        _Qry &= vbCrLf & "  ) AS SPLN "
        _Qry &= vbCrLf & "  ON A.FNHSysPartId = SPLN.FNHSysPartId AND A.FNSendSuplType = SPLN.FNSendSuplType "

        _Qry &= vbCrLf & " WHERE A.FNHSysPartId > 0 AND ISNULL(P.FTPartCode,'') <>'' "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.RepFTPartName.DataSource = dt.Copy
        dt.Dispose()

    End Sub

    Private Sub LoadSendSuplInfo()

        Dim dt As DataTable
        Dim dtbundle As DataTable
        Dim dtbundleOrg As DataTable
        Dim _SeaSonId As Integer = 0

        Dim _Qry As String
        _Qry = "Select   Top  1  FNHSysSeasonId   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK)  WHERE  FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
        _SeaSonId = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")

        _ListSendSuplInfo.Clear()
        _ListSendSuplBundleInfo.Clear()

        ogcsendsupl.DataSource = Nothing
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
        Dim _FTOrderProdNo As String = ""

        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            _FTOrderProdNo = Me.otbjobprod.SelectedTabPage.Name.ToString
        End If

        Call LoadSupplier()
        Call LoadOperation(_FTOrderProdNo)
        Call LoadPart()

        _Qry = " SELECT A.FNHSysPartId"
        _Qry &= vbCrLf & " 	,P.FTPartCode "
        _Qry &= vbCrLf & " , Convert(varchar(30),A.FNHSysPartId) + '|' +P.FTPartCode + '|' + Convert(varchar(30),A.FNSendSuplType) + '|' + ISNULL(A.FTNote,'')  AS FTPartName "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            ' _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameTH,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameTH,'') AS FNSendSuplTypeName"
            _Qry &= vbCrLf & " , ISNULL(S.FTSuplCode,'') +' :: ' + ISNULL(S.FTSuplNameTH,'') As FTSuplName"
            _Qry &= vbCrLf & " ,ISNULL(OS.FTOperationCode,'') +' :: ' + ISNULL(OS.FTOperationNameTH,'')  AS FTOperationNameS"
            _Qry &= vbCrLf & " ,ISNULL(OT.FTOperationCode,'') +' :: ' +ISNULL(OT.FTOperationNameTH,'')  AS FTOperationNameT"

        Else

            ' _Qry &= vbCrLf & " ,ISNULL(P.FTPartNameEN,'') AS FTPartName "
            _Qry &= vbCrLf & " ,ISNULL(LN.FTNameEN,'') AS FNSendSuplTypeName "
            _Qry &= vbCrLf & " , ISNULL(S.FTSuplCode,'') +' :: ' +ISNULL(S.FTSuplNameEN,'') As FTSuplName"
            _Qry &= vbCrLf & " ,ISNULL(OS.FTOperationCode,'') +' :: ' +ISNULL(OS.FTOperationNameEN,'')  AS FTOperationNameS"
            _Qry &= vbCrLf & " ,ISNULL(OT.FTOperationCode,'') +' :: ' +ISNULL(OT.FTOperationNameEN,'')  AS FTOperationNameT"

        End If

        _Qry &= vbCrLf & " , A.FNSendSuplType"
        _Qry &= vbCrLf & " ,A.FNHSysSuplId"

        _Qry &= vbCrLf & " ,S.FTSuplCode "
        _Qry &= vbCrLf & " , A.FNHSysOperationId"
        _Qry &= vbCrLf & " ,OS.FTOperationCode AS FTOperationCodeS"
        _Qry &= vbCrLf & " ,A.FNHSysOperationIdTo"
        _Qry &= vbCrLf & " ,OT.FTOperationCode AS FTOperationCodeT"
        _Qry &= vbCrLf & " , A.FNQuantity,A.FTSendSuplRef"
        _Qry &= vbCrLf & " ,A.FTNote"
        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & "  (SELECT  A.FNHSysPartId"
        _Qry &= vbCrLf & " , A.FNSendSuplType,A.FTNote"
        _Qry &= vbCrLf & " , B.FNHSysSuplId"
        _Qry &= vbCrLf & " , B.FNHSysOperationId"
        _Qry &= vbCrLf & " , B.FNHSysOperationIdTo"
        _Qry &= vbCrLf & " , ISNULL(B.FNQuantity,0) AS FNQuantity"
        _Qry &= vbCrLf & " , ISNULL(B.FTSendSuplRef,'') AS FTSendSuplRef"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (SELECT X.FNHSysPartId,X.FNSendSuplType,ISNULL(SPLN.FTNote,'') AS FTNote"
        _Qry &= vbCrLf & "   FROM ( SELECT    FNHSysPartId,0 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId = " & _FNHSysStyleId & " And FTStateEmb = 1 and FNHSysSeasonId   =" & _SeaSonId

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,1 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "    WHERE FNHSysStyleId =  " & _FNHSysStyleId & " And FTStatePrint = 1 and FNHSysSeasonId =" & _SeaSonId

        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,2 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " And FTStateHeat = 1 and FNHSysSeasonId =" & _SeaSonId

        _Qry &= vbCrLf & "    UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,3 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " And FTStateLaser = 1  and FNHSysSeasonId =" & _SeaSonId

        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,4 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " And FTStateWindows = 1 and FNHSysSeasonId =" & _SeaSonId


        _Qry &= vbCrLf & "   UNION"
        _Qry &= vbCrLf & "  SELECT    FNHSysPartId,6 AS FNSendSuplType "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Part WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FNHSysStyleId =  " & _FNHSysStyleId & " And FTStateNonEmbroidry = 1 and FNHSysSeasonId =" & _SeaSonId


        _Qry &= vbCrLf & " ) AS X"

        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.FNT_GetPartSendSuplDesc(" & _FNHSysStyleId & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "')  "
        _Qry &= vbCrLf & "  ) AS SPLN "
        _Qry &= vbCrLf & "  ON X.FNHSysPartId = SPLN.FNHSysPartId AND X.FNSendSuplType = SPLN.FNSendSuplType "

        _Qry &= vbCrLf & "  ) AS A LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  ("
        _Qry &= vbCrLf & "  SELECT * "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl  WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
        _Qry &= vbCrLf & "   )"
        _Qry &= vbCrLf & "  AS B"
        _Qry &= vbCrLf & "  ON A.FNHSysPartId =B.FNHSysPartId"
        _Qry &= vbCrLf & "   AND A.FNSendSuplType = B.FNSendSuplType "
        _Qry &= vbCrLf & "   AND A.FNSendSuplType = B.FNSendSuplType AND A.FTNote=B.FTNote "
        _Qry &= vbCrLf & "   ) AS A LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS P WITH(NOLOCK)"
        _Qry &= vbCrLf & "    ON A.FNHSysPartId = P.FNHSysPartId  "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN ( SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  WHERE FTListName ='FNSendSuplType') AS LN "
        _Qry &= vbCrLf & "  ON A.FNSendSuplType = LN.FNListIndex "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON A.FNHSysSuplId = S.FNHSysSuplId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation  AS OS WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON A.FNHSysOperationId = OS.FNHSysOperationId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation  AS OT WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON A.FNHSysOperationIdTo = OT.FNHSysOperationId "
        _Qry &= vbCrLf & " WHERE A.FNHSysPartId > 0 AND ISNULL(P.FTPartCode,'') <>'' "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcsendsupl.DataSource = dt.Copy
        _ListSendSuplInfo.Add(dt.Copy)

        _Qry = " SELECT  FTSendSuplRef, FTOrderProdNo, FNHSysPartId"
        _Qry &= vbCrLf & " , FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail  WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
        dtbundle = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        _ListSendSuplBundleInfo.Add(dtbundle.Copy)

        _Qry = "   SELECT     '0' AS FTSelect,   FTBarcodeBundleNo, FNBunbleSeq"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE FTOrderProdNo ='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "'"
        _Qry &= vbCrLf & "  ORDER BY  FNBunbleSeq "
        dtbundleOrg = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        ogcselectbundle.DataSource = dtbundleOrg.Copy

        dtbundle.Dispose()
        dtbundleOrg.Dispose()
        dt.Dispose()

    End Sub

    Private Function SaveDataSendSupl() As Boolean
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            Dim FTSendSuplRef As String = ""
            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()
            Dim dt As DataTable = CType(Me.ogcsendsupl.DataSource, DataTable)
            Dim dtsub As DataTable = _ListSendSuplInfo(0).Copy
            Dim dtbundle As DataTable = _ListSendSuplBundleInfo(0).Copy
            Dim _FNQuantity As Integer = 0
            Dim _Qry As String = ""

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail "
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & " AND FTSendSuplRef NOT IN ("
            _Qry &= vbCrLf & "  SELECT DISTINCT FTSendSuplRef "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS T WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
            _Qry &= vbCrLf & "  )"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            Dim RowDataInd As Integer = 0
            For Each R As DataRow In dt.Select("FNHSysPartId>0  AND FNHSysSuplId>0 AND FNHSysOperationId>0 AND FNHSysOperationIdTo>0 ")
                RowDataInd = RowDataInd + 1

                If R!FTSendSuplRef.ToString = "" Then

                    _Qry = "SELECT Replace( Convert(varchar(10),GetDate(),111),'/','') + Replace(Convert(varchar(30),GetDate(),114),':','')"
                    FTSendSuplRef = HI.Conn.SQLConn.GetFieldByNameOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "")

                    If FTSendSuplRef <> "" Then
                        FTSendSuplRef = HI.ST.SysInfo.CmpRunID & "-" & RowDataInd.ToString & "-" & FTSendSuplRef

                        _FNQuantity = 0
                        For Each Rx As DataRow In dtbundle.Select("FNHSysPartId=" & Val(R!FNHSysPartId) & " AND FNHSysSuplId=" & Val(R!FNHSysSuplId) & " AND FNSendSuplType=" & Val(R!FNSendSuplType) & " AND FTSendSuplRef='' ")
                            _FNQuantity = _FNQuantity + 1

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Qry &= vbCrLf & " , FTSendSuplRef, FTOrderProdNo"
                            _Qry &= vbCrLf & " , FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo,FNHSysCmpId)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & ",'" & FTSendSuplRef & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                            _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeBundleNo.ToString) & "'"
                            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        Next

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl(FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & " , FTSendSuplRef, FTOrderProdNo"
                        _Qry &= vbCrLf & " , FNHSysPartId, FNSendSuplType, FNHSysSuplId, FNHSysOperationId, FNHSysOperationIdTo,FNQuantity,FTNote,FNHSysCmpId)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",'" & FTSendSuplRef & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysOperationId.ToString)) & ""
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysOperationIdTo.ToString)) & ""
                        _Qry &= vbCrLf & "," & _FNQuantity & ""
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNote.ToString) & "'"
                        _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Else

                    FTSendSuplRef = R!FTSendSuplRef.ToString

                    If CheckCreateBarcodeSendSupl(Me.otbjobprod.SelectedTabPage.Name.ToString, FTSendSuplRef) = False Then
                        _FNQuantity = 0
                        For Each Rx As DataRow In dtbundle.Select("FNHSysPartId=" & Val(R!FNHSysPartId) & " AND FNHSysSuplId=" & Val(R!FNHSysSuplId) & " AND FNSendSuplType=" & Val(R!FNSendSuplType) & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' ")
                            _FNQuantity = _FNQuantity + 1

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail(FTInsUser, FDInsDate, FTInsTime"
                            _Qry &= vbCrLf & " , FTSendSuplRef, FTOrderProdNo"
                            _Qry &= vbCrLf & " , FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo,FNHSysCmpId)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & ",'" & FTSendSuplRef & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                            _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Rx!FTBarcodeBundleNo.ToString) & "'"
                            _Qry &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        Next

                        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl"
                        _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",FNHSysPartId=" & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                        _Qry &= vbCrLf & ",FNSendSuplType=" & Integer.Parse(Val(R!FNSendSuplType.ToString)) & ""
                        _Qry &= vbCrLf & ",FNHSysSuplId=" & Integer.Parse(Val(R!FNHSysSuplId.ToString)) & ""
                        _Qry &= vbCrLf & ",FNHSysOperationId=" & Integer.Parse(Val(R!FNHSysOperationId.ToString)) & ""
                        _Qry &= vbCrLf & ",FNHSysOperationIdTo=" & Integer.Parse(Val(R!FNHSysOperationIdTo.ToString)) & ""
                        _Qry &= vbCrLf & ",FNQuantity=" & _FNQuantity & ""
                        _Qry &= vbCrLf & " WHERE FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                End If
            Next

            For Each R As DataRow In dtsub.Select("FTSendSuplRef<>''")

                FTSendSuplRef = R!FTSendSuplRef.ToString
                If dt.Select("FTSendSuplRef='" & R!FTSendSuplRef.ToString & "'").Length <= 0 Then

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail "
                    _Qry &= vbCrLf & " WHERE FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & " AND FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef NOT IN ("
                    _Qry &= vbCrLf & "  SELECT DISTINCT FTSendSuplRef "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS T WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & "  )"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl "
                    _Qry &= vbCrLf & " WHERE FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & " AND FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef NOT IN ("
                    _Qry &= vbCrLf & "  SELECT DISTINCT FTSendSuplRef "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS T WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Name.ToString) & "' "
                    _Qry &= vbCrLf & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(FTSendSuplRef) & "' "
                    _Qry &= vbCrLf & "  )"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If

                End If
            Next

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
#End Region

#Region "General"

    Private Sub wCreateJobProduction_Load(sender As Object, e As EventArgs)
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTOrderNo.Focus()
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub otbjobprod_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbjobprod.SelectedPageChanged
        Try

            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbjobprod_SelectedPageChanged), New Object() {sender, e})
            Else

                If Not (otbjobprod.SelectedTabPage Is Nothing) Then

                    Call LoadOrderProdMarkCutting(otbjobprod.SelectedTabPage.Name.ToString)

                Else

                    Me.otbmarkcutting.TabPages.Clear()
                    Me.otbtable.TabPages.Clear()
                    Me.otbcutbound.TabPages.Clear()

                End If

                otbdetail.SelectedTabPageIndex = 0

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetDefaultValue(Optional ClearAll As Boolean = True)

        oceM1.Value = 0.9144
        oceKG1.Value = 0
        oceKG2.Value = 0.0232
        oceKG3.Value = 1000
        oceKG4.Value = 1600

        If Not (ClearAll) Then

            oceY1.Value = 0
            oceY2.Value = 0
            oceY3.Value = 36
            oceY4.Value = 0
            oceY5.Value = 2
            oceY6.Value = 0
            oceY7.Value = 2.5
            oceY8.Value = 0

        End If

    End Sub

    Private Sub otbmarkcutting_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmarkcutting.SelectedPageChanged
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbmarkcutting_SelectedPageChanged), New Object() {sender, e})
            Else

                If Not (otbmarkcutting.SelectedTabPage Is Nothing) Then
                    Call LoadOrderProdMarkTableCutting(otbjobprod.SelectedTabPage.Name.ToString, otbmarkcutting.SelectedTabPage.Name.ToString)
                Else
                    Me.otbtable.TabPages.Clear()
                    Me.otbcutbound.TabPages.Clear()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbtable_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbtable.SelectedPageChanged

        Try

            If (Me.InvokeRequired) Then

                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbtable_SelectedPageChanged), New Object() {sender, e})

            Else

                If Not (otbtable.SelectedTabPage Is Nothing) Then

                    Call LoadTableCuttingLayCut(otbtable.SelectedTabPage.Name.ToString)

                Else

                    Call LoadTableCuttingLayCut("0")
                    otbcutbound.TabPages.Clear()

                End If

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub wOrderProdCuttingAndBound_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call SetDefaultValue()
        Call InitGrid()

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderProdDataInfo(FTOrderNo.Text)
            Me.otbdetail.SelectedTabPageIndex = 0
        End If
    End Sub

    Private Sub FNCuttingUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNCuttingUnit.SelectedIndexChanged

        Try

            Call SetDefaultValue(False)

            Me.opnkg.Visible = (FNCuttingUnit.SelectedIndex = 1)
            Me.opnm.Visible = (FNCuttingUnit.SelectedIndex = 2)

        Catch ex As Exception

            Me.opnkg.Visible = False
            Me.opnm.Visible = False

        End Try

    End Sub

    Private Sub ocmaddnew_Click(sender As Object, e As EventArgs) Handles ocmaddnewlaycut.Click
        If Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then

            If CheckBoundTable() Then
                Exit Sub
            End If

            If Not (Me.otbcutbound.SelectedTabPage Is Nothing) And Me.ocmsave.Enabled Then

                If Me.ValidateLaycut Then
                    If Not SaveLayCut() Then
                        'HI.MG.ShowMsg.mInfo("ไม่สามารถทำการบันทึกข้อมูลโต๊ะตัดได้ !!!", 1404210007, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        'Exit Sub
                    End If
                Else
                    Exit Sub
                End If

            End If

            Call LoadTableCuttingLayCut(otbtable.SelectedTabPage.Name.ToString)
            Call SetDefaultValue()
            FNCuttingUnit.SelectedIndex = 0

            oceY3.Value = 36

            Dim Otp As New DevExpress.XtraTab.XtraTabPage()
            With Otp
                .Name = "New"
                .Text = "New"
            End With

            otbcutbound.TabPages.Add(Otp)
            otbcutbound.SelectedTabPage = otbcutbound.TabPages(otbcutbound.TabPages.Count - 1)

            Try
                Me.FTLaycutDate.DateTime = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub otbcutbound_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbcutbound.SelectedPageChanged
        Try

            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbcutbound_SelectedPageChanged), New Object() {sender, e})
            Else
                If Not (otbcutbound.SelectedTabPage Is Nothing) Then
                    Call LoadLayCutNoInfo(otbcutbound.SelectedTabPage.Name.ToString)
                Else
                    Call LoadLayCutNoInfo("0")
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTColorway_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTColorway.EditValueChanged
        Try
            With Me.ogvlayer
                If .FocusedRowHandle < 0 Then Exit Sub
                Dim edit As DevExpress.XtraEditors.LookUpEdit = CType(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FTRawMatColorCode", edit.GetColumnValue("FTRawMatColorCode").ToString)
                .SetFocusedRowCellValue("FTRawMatColorName", edit.GetColumnValue("FTRawMatColorName").ToString)
                .SetFocusedRowCellValue("FNLayerQuantity", Integer.Parse(Val(edit.GetColumnValue("FNLayer").ToString)))
                .SetFocusedRowCellValue("FNReqQuantity", Double.Parse(Val(oceY8.Value) * Integer.Parse(Val(edit.GetColumnValue("FNLayer").ToString))))
                .SetFocusedRowCellValue("FTNikePOLineItem", edit.GetColumnValue("FTNikePOLineItem").ToString)
                .SetFocusedRowCellValue("FTColorwayData", edit.GetColumnValue("FTColorway").ToString)


                Dim _PackPerCarton As Integer = 0
                Dim _Qry As String = ""
                Dim _dt As DataTable
                Dim _dt2 As DataTable

                Dim _FTColor As String = edit.GetColumnValue("FTColorway").ToString

                _Qry = "SELECT TOP 1 B.FNPackPerCarton,B.FNPackCartonSubType"
                _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"
                _Qry &= vbCrLf & "  WHERE  (A.FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"

                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                _PackPerCarton = -1
                For Each R As DataRow In _dt.Rows
                    If Integer.Parse(Val(R!FNPackCartonSubType.ToString)) = 0 Then
                        _PackPerCarton = Integer.Parse(Val(R!FNPackPerCarton.ToString))
                    Else

                        _Qry = " SELECT TOP 1 FTOrderProdNo, FNHSysMarkId, FNTableNo, FTColorway, FTSizeBreakDown, FNLayer, FNAssort, FNQuantity"
                        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_TableCut_Detail AS A WITH(NOLOCK) "
                        _Qry &= vbCrLf & "  WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
                        _Qry &= vbCrLf & "   AND (FNHSysMarkId =" & Integer.Parse(Val(otbmarkcutting.SelectedTabPage.Name.ToString())) & ") "
                        _Qry &= vbCrLf & "  AND (FNTableNo =" & Integer.Parse(Val(otbtable.SelectedTabPage.Name.ToString())) & ")"
                        _Qry &= vbCrLf & "  AND (FTColorway =N'" & HI.UL.ULF.rpQuoted(_FTColor) & "')"

                        _dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        For Each R2 As DataRow In _dt2.Rows

                            _Qry = " SELECT B.FNQuantity"
                            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A INNER JOIN"
                            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Bundle AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTColorway = B.FTColorway AND A.FTSizeBreakDown = B.FTSizeBreakDown AND "
                            _Qry &= vbCrLf & "        A.FTSubOrderNo = B.FTSubOrderNo"
                            _Qry &= vbCrLf & "  WHERE  (A.FTOrderProdNo =  N'" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "')"
                            _Qry &= vbCrLf & "   AND (B.FTColorway =N'" & HI.UL.ULF.rpQuoted(_FTColor) & "') "
                            _Qry &= vbCrLf & "  AND (B.FTSizeBreakDown =N'" & HI.UL.ULF.rpQuoted(R2!FTSizeBreakDown.ToString) & "')  "

                            _PackPerCarton = (Integer.Parse(Val(R2!FNLayer.ToString)) * Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))))

                        Next
                    End If
                Next

                If _PackPerCarton <= -0 Then
                    _PackPerCarton = Integer.Parse(Val(edit.GetColumnValue("FNLayer").ToString))
                End If

                .SetFocusedRowCellValue("FNQuantityPerBundle", _PackPerCarton)


            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvcut_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvcut.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogccut.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTSizeBreakDown='' OR  FNQuantity <=0").Length <= 0 Then
                        Call InitCutNewRow()
                    End If
                End With
            Case System.Windows.Forms.Keys.Delete
                With ogvcut
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                    Dim FNSeq As Integer = 0
                    With CType(Me.ogccut.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Rows
                            FNSeq = FNSeq + 1
                            R!FNSeq = FNSeq
                        Next
                        .AcceptChanges()
                    End With


                    If .RowCount <= 0 Then
                        Call InitCutNewRow()
                    End If

                End With
        End Select
    End Sub

    Private Sub ogvlayer_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvlayer.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogclayer.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTColorway='' OR  FNLayerQuantity <=0  OR  FNQuantityPerBundle <=0 ").Length <= 0 Then
                        Call InitLayerNewRow()
                    End If
                End With
            Case System.Windows.Forms.Keys.Delete
                With ogvlayer
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)
                    Dim FNSeq As Integer = 0
                    With CType(Me.ogclayer.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Rows
                            FNSeq = FNSeq + 1
                            R!FNSeq = FNSeq
                        Next
                        .AcceptChanges()
                    End With
                    If .RowCount <= 0 Then
                        Call InitLayerNewRow()
                    End If

                End With

        End Select
    End Sub

    Private Sub RepFNLayerQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNLayerQuantity.EditValueChanging
        With ogvlayer
            If .FocusedRowHandle < 0 Then Exit Sub
            .SetFocusedRowCellValue("FNReqQuantity", Double.Parse(Val(e.NewValue)) * oceY8.Value)
        End With
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.FTOrderNo.Text <> "" Then
            If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
                If Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then
                    If Not (Me.otbtable.SelectedTabPage Is Nothing) Then
                        If Not (Me.otbcutbound.SelectedTabPage Is Nothing) Then

                            If CheckBoundTable() Then
                                Exit Sub
                            End If

                            If Me.ValidateLaycut Then
                                If Me.SaveLayCut Then
                                    Call LoadTableCuttingLayCut(otbtable.SelectedTabPage.Name.ToString)
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                Else
                                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                End If
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, olblaycutno_lbl.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, olbtablecut.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, olbmark.Text)
                End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Order Production !!!", 1405150001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If Me.FTOrderNo.Text <> "" Then
            If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
                If Not (Me.otbmarkcutting.SelectedTabPage Is Nothing) Then
                    If Not (Me.otbtable.SelectedTabPage Is Nothing) Then
                        If Not (Me.otbcutbound.SelectedTabPage Is Nothing) Then
                            If CheckBoundTable() Then
                                Exit Sub
                            End If
                            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = True Then
                                If Me.DeleteLayCut Then
                                    Call LoadTableCuttingLayCut(otbtable.SelectedTabPage.Name.ToString)
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                                Else
                                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                                End If
                            End If


                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, olblaycutno_lbl.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, olbtablecut.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, olbmark.Text)
                End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Order Production !!!", 1405150001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            FTOrderNo.Focus()
        End If
    End Sub

#End Region

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged
        Call TabChenge()

        Select Case e.Page.Name
            Case otpbundle.Name

                If _StateMergeBundleScrap Then
                    Me.FNLayCutBoundType.SelectedIndex = 1
                Else
                    Me.FNLayCutBoundType.SelectedIndex = 0
                End If

                Call LoadLayCutCreateBundle()
            Case otpsendsupl.Name
                ocmselect.Enabled = True
                Call LoadSendSuplInfo()
        End Select

    End Sub

    Private Sub ocmcreatebound_Click(sender As Object, e As EventArgs) Handles ocmcreatebound.Click

        CType(ogctablebound.DataSource, DataTable).AcceptChanges()

        If CType(ogctablebound.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
            Call CreateBundle()
        End If

    End Sub

    Private Sub otbdetail_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otbdetail.SelectedPageChanging
        If otbjobprod.SelectedTabPage Is Nothing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RepFTPartName_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTPartName.EditValueChanged
        Try

            With Me.ogvsendsupl

                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                'Dim _PartName As String = obj.GetColumnValue("FTPartName").ToString
                'Dim _FNHSysPartId As String = obj.GetColumnValue("FNHSysPartId").ToString
                'Dim _FNSendSuplTypeName As String = obj.GetColumnValue("FNSendSuplTypeName").ToString
                'Dim _FNSendSuplType As String = obj.GetColumnValue("FNSendSuplType").ToString

                Dim _Obj As System.Data.DataRowView = obj.GetSelectedDataRow()

                Dim _PartName As String = _Obj.Item("FTSenSuplDataRef").ToString()
                Dim _FNHSysPartId As String = _Obj.Item("FNHSysPartId").ToString()
                Dim _FNSendSuplTypeName As String = _Obj.Item("FNSendSuplTypeName").ToString()
                Dim _FNSendSuplType As String = _Obj.Item("FNSendSuplType").ToString()
                Dim _FTNote As String = _Obj.Item("FTNote").ToString()

                .SetFocusedRowCellValue("FTPartName", _PartName)
                .SetFocusedRowCellValue("FNHSysPartId", _FNHSysPartId)
                .SetFocusedRowCellValue("FNSendSuplTypeName", _FNSendSuplTypeName)
                .SetFocusedRowCellValue("FNSendSuplType", _FNSendSuplType)
                .SetFocusedRowCellValue("FTNote", _FTNote)
            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTSuplName_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTSuplName.EditValueChanged
        Try

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNHSysSuplId", obj.GetColumnValue("FNHSysSuplId").ToString)

            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTOperationNameS_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTOperationNameS.EditValueChanged
        Try

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNHSysOperationId", obj.GetColumnValue("FNHSysOperationId").ToString)

            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTOperationNameT_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTOperationNameT.EditValueChanged
        Try

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNHSysOperationIdTo", obj.GetColumnValue("FNHSysOperationId").ToString)
            End With

            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogcsendsupl_Click(sender As Object, e As EventArgs) Handles ogcsendsupl.Click

    End Sub

    Private Function CheckCreateBarcodeSendSupl(OrderProdKey As String, SensuplKey As String) As Boolean
        Dim _Qry As String = ""
        _Qry = "  SELECT TOP 1 FTOrderProdNo"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTOrderProdNo = N'" & HI.UL.ULF.rpQuoted(OrderProdKey) & "') "
        _Qry &= vbCrLf & " AND (FTSendSuplRef = N'" & HI.UL.ULF.rpQuoted(SensuplKey) & "')"

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "")
    End Function

    Private Sub ogvsendsupl_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvsendsupl.CellValueChanged

    End Sub

    Private Sub ogvsendsupl_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvsendsupl.CellValueChanging

    End Sub

    Private Sub ogvsendsupl_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvsendsupl.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down

                With CType(Me.ogcsendsupl.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTPartName='' OR FTPartName IS NULL").Length <= 0 Then
                        .Rows.Add(0, "", "", "", "", "", "", 0, 0, "", 0, "", 0, "", 0, "", "")
                        .AcceptChanges()
                    End If
                End With

            Case System.Windows.Forms.Keys.Delete

                If CheckCreateBarcode(False) Then
                    With Me.ogvsendsupl
                        If .FocusedRowHandle < 0 Then Exit Sub
                        If "" & .GetFocusedRowCellValue("FTSendSuplRef").ToString <> "" Then Exit Sub
                        .DeleteRow(.FocusedRowHandle)
                    End With
                Else
                    With Me.ogvsendsupl
                        If .FocusedRowHandle < 0 Then Exit Sub
                        .DeleteRow(.FocusedRowHandle)
                    End With
                End If

                CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        End Select
    End Sub

    Private Sub ocmsavesendsupl_Click(sender As Object, e As EventArgs) Handles ocmsavesendsupl.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            'If CheckCreateBarcode(False) Then

            'Else

            'End If
            If SaveDataSendSupl() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Call LoadSendSuplInfo()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDataInfo(FTOrderNo.Text)
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub RepFTPositionPartName_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepGSFNQuantity.QueryCloseUp
        Try
            Dim _FNHSysPartId As Integer
            Dim _FNSendSuplType As Integer
            Dim _FNHSysSuplId As Integer
            Dim _FTSendSuplRef As String

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub
                _FNHSysPartId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString))
                _FNSendSuplType = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSendSuplType").ToString))
                _FNHSysSuplId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                _FTSendSuplRef = "" & .GetRowCellValue(.FocusedRowHandle, "FTSendSuplRef").ToString

            End With

            Try
                ogvselectbundle.ClearColumnsFilter()
                ogvselectbundle.ActiveFilter.Clear()
            Catch ex As Exception
            End Try

            ' FTSendSuplRef, FTOrderProdNo, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo 
            Dim _Count As Integer = 0
            Dim _StrFilter As String = ""
            Dim dt As DataTable = _ListSendSuplBundleInfo(0).Copy
            dt.BeginInit()
            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For Each R As DataRow In .Rows
                    _StrFilter = "FNHSysPartId=" & _FNHSysPartId & " AND FNSendSuplType=" & _FNSendSuplType & " AND FNHSysSuplId=" & _FNHSysSuplId & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(_FTSendSuplRef) & "' AND FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    If R!FTSelect.ToString = "1" Then
                        _Count = _Count + 1
                        If dt.Select(_StrFilter).Length <= 0 Then
                            dt.Rows.Add(_FTSendSuplRef, Me.otbjobprod.SelectedTabPage.Name.ToString, _FNHSysPartId, _FNSendSuplType, _FNHSysSuplId, R!FTBarcodeBundleNo.ToString)
                        End If
                    Else
                        For Each Rx As DataRow In dt.Select(_StrFilter)
                            Rx.Delete()
                        Next
                    End If

                Next

                .AcceptChanges()
            End With
            dt.EndInit()
            _ListSendSuplBundleInfo.Clear()
            _ListSendSuplBundleInfo.Add(dt.Copy)
            dt.Dispose()
            Me.ogvsendsupl.SetFocusedRowCellValue("FNQuantity", _Count)
            CType(Me.ogcsendsupl.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTPositionPartName_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepGSFNQuantity.QueryPopUp
        Try
            Dim _FNHSysPartId As Integer
            Dim _FNSendSuplType As Integer
            Dim _FNHSysSuplId As Integer
            Dim _FTSendSuplRef As String

            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub
                _FNHSysPartId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysPartId").ToString))
                _FNSendSuplType = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSendSuplType").ToString))
                _FNHSysSuplId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysSuplId").ToString))
                _FTSendSuplRef = "" & .GetRowCellValue(.FocusedRowHandle, "FTSendSuplRef").ToString

            End With

            Try
                ogvselectbundle.ClearColumnsFilter()
                ogvselectbundle.ActiveFilter.Clear()
            Catch ex As Exception
            End Try

            FNStartBundle.Value = 0
            FNEndBundle.Value = 0

            Dim _StrFilter As String = ""
            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For Each R As DataRow In .Rows

                    _StrFilter = "FNHSysPartId=" & _FNHSysPartId & " AND FNSendSuplType=" & _FNSendSuplType & " AND FNHSysSuplId=" & _FNHSysSuplId & " AND FTSendSuplRef='" & HI.UL.ULF.rpQuoted(_FTSendSuplRef) & "' AND FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "' "
                    If _ListSendSuplBundleInfo(0).Select(_StrFilter).Length > 0 Then
                        R!FTSelect = "1"
                    Else
                        R!FTSelect = "0"
                    End If
                Next

                .AcceptChanges()
            End With
        Catch ex As Exception
        End Try

    End Sub


    Private Sub ocmselect_Click(sender As Object, e As EventArgs) Handles ocmselect.Click
        If IsNumeric(Me.FNStartBundle.Value) And IsNumeric(Me.FNEndBundle.Value) Then
            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For I As Integer = Me.FNStartBundle.Value To Me.FNEndBundle.Value
                    For Each R As DataRow In .Select("FNBunbleSeq=" & I & "")
                        R!FTSelect = "1"

                        Exit For
                    Next
                Next
                .AcceptChanges()
            End With
        End If
    End Sub

    Private Sub ockselectallbundle_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectallbundle.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectallbundle.Checked Then
                _State = "1"
            End If

            With ogcbound
                If Not (.DataSource Is Nothing) And ogvbound.RowCount > 0 Then

                    With ogvbound
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdeletebound_Click(sender As Object, e As EventArgs) Handles ocmdeletebound.Click
        Dim _Spls As New HI.TL.SplashScreen("Checking And Deleting Data.... Please wait. ")
        Try

            Dim _Str As String = ""

            With ogcbound
                If Not (.DataSource Is Nothing) Then

                    With CType(.DataSource, DataTable)
                        .AcceptChanges()

                        For Each R As DataRow In .Select("FTSelect='1'")

                            _Str = "  DELETE FROM A "
                            _Str &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS A INNER JOIN"
                            _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B ON A.FTBarcodeBundleNo = B.FTBarcodeBundleNo"
                            _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') <>'1'"

                            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PROD)

                            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Str)

                            _Str = "  DELETE FROM B "
                            _Str &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B "
                            _Str &= vbCrLf & "  WHERE B.FTBarcodeBundleNo ='" & HI.UL.ULF.rpQuoted(R!FTBarcodeBundleNo.ToString) & "'"
                            _Str &= vbCrLf & "  AND  ISNULL(B.FTStateGenBarcode,'') <>'1'"

                            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PROD)
                            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Str)

                        Next
                        Me.ockselectallbundle.Checked = False
                        Call LoadLayCutCreateBundle()

                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub


    Private Sub Calculate_EditValueChanged(sender As Object, e As EventArgs) Handles oceY4.EditValueChanged, oceY5.EditValueChanged, oceY6.EditValueChanged, oceY7.EditValueChanged

        'Dim _Total As Double = 0
        '_Total = (oceY4.Value + oceY5.Value) * (oceY6.Value + oceY7.Value)
        'oceY8.Value = _Total

        'Try

        '    With (CType(Me.ogclayer.DataSource, DataTable))
        '        .AcceptChanges()
        '        For Each R As DataRow In .Select("FTColorway<>''")
        '            R!FNReqQuantity = _Total
        '        Next
        '        .AcceptChanges()

        '    End With
        'Catch ex As Exception
        'End Try

    End Sub

    Private Sub ocmdeselect_Click(sender As Object, e As EventArgs) Handles ocmdeselect.Click
        If IsNumeric(Me.FNStartBundle.Value) And IsNumeric(Me.FNEndBundle.Value) Then
            With CType(Me.ogcselectbundle.DataSource, DataTable)
                For I As Integer = Me.FNStartBundle.Value To Me.FNEndBundle.Value
                    For Each R As DataRow In .Select("FNBunbleSeq=" & I & "")
                        R!FTSelect = "0"

                        Exit For
                    Next
                Next
                .AcceptChanges()
            End With
        End If
    End Sub

    Private Sub ockselectsndall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectsendall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ockselectsendall.Checked Then
                _State = "1"
            End If

            With ogcselectbundle
                If Not (.DataSource Is Nothing) And ogvselectbundle.RowCount > 0 Then

                    With ogvselectbundle
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvbound.Columns

            Select Case c.FieldName.ToString.ToUpper
                Case "FTMarkName".ToUpper, "FNBunbleSeq".ToUpper, "FTColorway".ToUpper, "FTSizeBreakDown".ToUpper, "FNQuantity".ToUpper, "FTRawMatColorName".ToUpper
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Sub ogvbound_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvbound.CellMerge
        Try
            With Me.ogvbound
                Select Case e.Column.FieldName.ToString.ToUpper
                    Case "FTMarkName".ToUpper, "FNBunbleSeq".ToUpper, "FTColorway".ToUpper, "FTSizeBreakDown".ToUpper, "FNQuantity".ToUpper, "FTRawMatColorName".ToUpper
                        If .GetRowCellValue(e.RowHandle1, "FTMarkName").ToString = .GetRowCellValue(e.RowHandle2, "FTMarkName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FNBunbleSeq").ToString = .GetRowCellValue(e.RowHandle2, "FNBunbleSeq").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = .GetRowCellValue(e.RowHandle2, "FTColorway").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FNQuantity").ToString = .GetRowCellValue(e.RowHandle2, "FNQuantity").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTRawMatColorName").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatColorName").ToString Then

                            'if e.Column.OptionsColumn.AllowMerge Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                            'Else
                            '    e.Merge = False
                            '    e.Handled = True
                            'End If
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case Else
                End Select
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbtable_Click(sender As Object, e As EventArgs) Handles otbtable.Click

    End Sub

    Private Sub ReposCFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCFTSelect.EditValueChanging
        Try
            Static _Proc As Boolean
            If Not (_Proc) Then
                _Proc = True

                Dim _State As String = "0"
                Dim _FNTableNo As Integer = 0
                Dim _FTMarkName As String = ""

                If e.NewValue.ToString = "1" Then
                    _State = "1"
                End If

                With ogcbound
                    CType(.DataSource, DataTable).AcceptChanges()
                    If Not (.DataSource Is Nothing) And ogvbound.RowCount > 0 Then

                        With ogvbound

                            _FNTableNo = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNTableNo")))
                            _FTMarkName = "" & .GetRowCellValue(.FocusedRowHandle, "FTMarkName").ToString

                            For Each R As DataRow In CType(.DataSource, DataTable).Select("FNTableNo=" & _FNTableNo & " AND  FTMarkName='" & HI.UL.ULF.rpQuoted(_FTMarkName) & "'")
                                R!FTSelect = _State
                            Next

                        End With

                        CType(.DataSource, DataTable).AcceptChanges()
                    End If
                End With

                _Proc = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

    End Sub

    Private Sub oceY2_EditValueChanged(sender As Object, e As EventArgs) Handles oceY2.EditValueChanged, oceY1.EditValueChanged


        Try
            Dim _Total As Double = 0
            _Total = (oceY1.Value) + CDbl(Format((oceY2.Value / oceY3.Value), "0.00"))
            oceY8.Value = _Total
            With (CType(Me.ogclayer.DataSource, DataTable))
                .AcceptChanges()
                For Each R As DataRow In .Select("FTColorway<>''")
                    R!FNReqQuantity = _Total
                Next
                .AcceptChanges()

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvsendsupl_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvsendsupl.ShowingEditor
        Try
            With Me.ogvsendsupl
                If .FocusedRowHandle < 0 Then Exit Sub

                If CheckCreateBarcodeSendSupl(Me.otbjobprod.SelectedTabPage.Text, "" & .GetFocusedRowCellValue("FTSendSuplRef").ToString) Then
                    e.Cancel = True
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreviewpacklist_Click(sender As Object, e As EventArgs) Handles ocmpreviewpacklist.Click
        If Not (Me.otbjobprod.SelectedTabPage Is Nothing) Then
            If Not (otbtable.SelectedTabPage Is Nothing) Then
                Dim _FM As String = ""
                Dim _ReportName As String = ""

                _ReportName = "Packinglist.rpt"
                _FM = " {TPRODTOrderProd.FTOrderProdNo}='" & HI.UL.ULF.rpQuoted(Me.otbjobprod.SelectedTabPage.Text) & "'"
                _FM &= "  AND {V_packinglistProduct.FNTableNo}=" & Integer.Parse(Val(Me.otbtable.SelectedTabPage.Text)) & ""

                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Production\"
                    .ReportName = _ReportName
                    .Formular = _FM
                    .Preview()
                End With

            End If


        End If
    End Sub

    Private Sub otbcutbound_Click(sender As Object, e As EventArgs) Handles otbcutbound.Click
    End Sub

    Private Sub RepFTPartName_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTPartName.EditValueChanging
    End Sub

    Private Sub RepFTPartName_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepFTPartName.QueryPopUp
    End Sub

    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub
End Class