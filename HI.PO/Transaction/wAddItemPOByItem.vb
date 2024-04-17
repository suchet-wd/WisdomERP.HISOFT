Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wAddItemPOByItem
    Private _AddItemPopup As wAddItemPOByItemListOrder
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)

        FNHSysRawmatId.Properties.Tag = ""

        _AddItemPopup = New wAddItemPOByItemListOrder
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try


    End Sub

    Private _PONO As String = ""
    Public Property PONO As String
        Get
            Return _PONO
        End Get
        Set(value As String)
            _PONO = value
        End Set
    End Property

    Private _FNSuplID As Integer = 0
    Public Property FNSuplID As Integer
        Get
            Return _FNSuplID
        End Get
        Set(value As Integer)
            _FNSuplID = value
        End Set
    End Property

    Private _AddMat As Boolean = False
    Public Property AddMat As Boolean
        Get
            Return _AddMat
        End Get
        Set(value As Boolean)
            _AddMat = value
        End Set
    End Property

    Private Sub wAddItemPO_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If FNHSysRawmatId.Text = "" Then
            FNHSysRawmatId.Focus()
        Else
            FNPOQuantity.Focus()
            FNPOQuantity.SelectAll()
        End If
    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNPOPrice.EditValueChanged, FNDisPer.EditValueChanged, FNPOQuantity.EditValueChanged
        Static _Proc As Boolean

        If Not (_Proc) Then
            _Proc = True

            Dim _Qty As Double = FNPOQuantity.Value
            Dim _Price As Double = FNPOPrice.Value
            Dim _DisPer As Double = FNDisPer.Value
            Dim _DisAmt As Double = FNDisAmt.Value

            Select Case sender.Name.ToString.ToUpper
                Case "FNPrice".ToUpper, "FNPOPrice".ToUpper
                    If _DisPer > 0 Then
                        _DisAmt = CDbl(Format((_Price * _DisPer) / 100, HI.ST.Config.PriceFormat))
                    Else
                        _DisAmt = 0
                    End If
                Case "FNDisPer".ToString.ToUpper

                    If _Price > 0 Then
                        _DisAmt = CDbl(Format((_Price * _DisPer) / 100.0, HI.ST.Config.PriceFormat))
                    Else
                        _DisAmt = 0
                    End If

            End Select

            FNDisAmt.Value = _DisAmt
            FNNetAmt.Value = CDbl(Format(_Qty * (_Price - _DisAmt), HI.ST.Config.AmtFormat))


            Try
                With CType(ogcjob.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows

                        If R!FTOrderNo.ToString <> "" Then
                            R!FNNetAmt = CDbl(Format(Val(R!FNQuantity.ToString) * (_Price - _DisAmt), HI.ST.Config.AmtFormat))
                        End If
                    Next

                End With
            Catch ex As Exception

            End Try


            _Proc = False
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Function CheckItemProd() As Boolean
        Dim cmd As String = ""
        If FNOrderType.SelectedIndex = 0 Then

            If HI.ST.SysInfo.Admin = False Then

                cmd = "  Select TOP 1  B.FTStatePurchase "
                cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As A INNER Join "
                cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp As B On A.FNHSysTeamGrpId = B.FNHSysTeamGrpId "
                cmd &= vbCrLf & " Where A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL( B.FTStatePurchase,'') ='1'"

                If HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SECURITY, "") <> "1" Then

                    cmd = "  Select TOP 1  B.FTStateNotCheckResuorce "
                    cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As A With(NOLOCK) INNER Join "
                    cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As B With(NOLOCK)   On A.FTRawMatCode = B.FTMainMatCode"
                    cmd &= vbCrLf & " Where (A.FNHSysRawMatId = " & Val(FNHSysRawmatId.EditValue.ToString) & ") "

                    If HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

                        cmd = "    Select top 1 FTOrderNo"
                        cmd &= vbCrLf & " From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A With(NOLOCK)"
                        cmd &= vbCrLf & " Where (FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "') "
                        cmd &= vbCrLf & " And (FNHSysCmpId = " & HI.ST.SysInfo.CmpID.ToString & ")"

                        If HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

                            HI.MG.ShowMsg.mInfo("หมายเลขใบสั่งผลิตนี้ไม่ได้ทำการผลิตที่สาขานี้ ไม่สามารถทำการเปิดสั่งซื้อ Item ด้าย ได้ !!!", 1705090345, Me.Text,, MessageBoxIcon.Warning)
                            Return False

                        Else

                            Return True

                        End If

                    Else
                        Return True
                    End If
                Else
                    Return True
                End If
            Else
                Return True
            End If


        Else
            Return True
        End If
    End Function
    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysRawmatId.Text <> "" And Val(FNHSysRawmatId.EditValue.ToString()) > 0 Then
            If Me.FNHSysUnitIdPO.Text <> "" And Me.FNHSysUnitIdPO.Properties.Tag.ToString <> "" Then
                If (Me.FTOrderNo.Text <> "" And Me.FTOrderNo.Properties.Tag.ToString <> "") Or FTOrderNo.Visible = False Then
                    If FNPOQuantity.Value > 0 Then
                        If (CheckItemProd()) Then
                            _Pass = True
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNQuantity_lbl.Text)
                        FNPOQuantity.Focus()
                    End If
                Else
                    If FTOrderNo.Visible Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTOrderNo_lbl.Text)
                        FTOrderNo.Focus()
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysUnitId_lbl.Text)
                FNHSysUnitIdPO.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysRawMatId_lbl.Text)
            FNHSysRawmatId.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        If Me.FTOrderNo.Properties.ReadOnly = True And FNHSysRawmatId.Enabled = False Then
            If (CheckReceive(Me.PONO, Val(FNHSysRawmatId.EditValue.ToString)) = False) Then Exit Sub
        Else
            If (CheckReceive(Me.PONO, Val(FNHSysRawmatId.EditValue.ToString)) = False) Then Exit Sub
        End If

        If HI.ST.ValidateData.CloseJob(Me.FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If

        If ValidateData() Then
            Me.AddMat = True
            Me.Close()
        End If

    End Sub

    Private Function CheckReceive(POKey As String, Optional SysMatId As Integer = 0) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""

        If SysMatId = 0 Then
            _Str = "Select TOP 1 H.FTPurchaseNo "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H WITH(NOLOCK), [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D "
            _Str &= vbCrLf & " WHERE H.FTReceiveNo= D.FTReceiveNo AND H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                If FNHSysRawmatId.Enabled = False And Me.FTOrderNo.Properties.ReadOnly Then
                    If HI.MG.ShowMsg.mConfirmProcess("หมายเลข PO นี้ มีการรับบาง Item แล้ว คุณต้องการแก้ไขรายการนี้ใช่หรือไม่ !!!", 1377150001) = True Then
                        _Pass = True
                    Else
                        _Pass = False
                    End If
                Else
                    HI.MG.ShowMsg.mProcessError(1303150001, "หมายเลข PO นี้ มีการรับแล้วไม่สามารถทำการแก้ไขหรือเพิ่มเติมได้ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                    _Pass = False
                End If

            End If

        Else

            _Str = "Select TOP 1 H.FTPurchaseNo "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H WITH(NOLOCK), [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D "
            _Str &= vbCrLf & " WHERE H.FTReceiveNo= D.FTReceiveNo AND H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "
            _Str &= vbCrLf & " AND FNHSysRawMatId=" & SysMatId & ""

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                HI.MG.ShowMsg.mProcessError(1401260001, "พบการรับ Item นี้แล้ว ", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                _Pass = False
            End If
        End If

        Return _Pass
    End Function

    Private Sub FNOrderType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNOrderType.SelectedIndexChanged

    End Sub


    Private Sub wAddItemPO_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FNSurchangeAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNSurchangeAmt.EditValueChanged

    End Sub


    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
            Else

                Static _Proc As Boolean

                If Not (_Proc) Then
                    _Proc = True

                    Try

                        Dim _Str As String = "SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..[V_OrderProdAndSMPPurchase]  WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "' "


                        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then

                            LoadMaterial(FTOrderNo.Text, FNMerMatTypePOX.SelectedIndex)


                        Else
                            FNHSysRawmatId.EditValue = Nothing
                            FNHSysRawmatId.Properties.DataSource = Nothing
                            ogcjob.DataSource = Nothing
                        End If


                    Catch ex As Exception

                    End Try

                    _Proc = False
                End If





            End If


        Catch ex As Exception

        End Try


    End Sub

    Private Sub FNHSysRawmatId_EditValueChanged_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub LoadOrderNo(Optional RawmatId As Integer = 0)
        Dim cmdstring As String = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_PURCHASE_GETORDERBY_MAT " & RawmatId & ""
        Dim dtorder As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        RepositoryFTOrderNo.DataSource = dtorder.Copy
    End Sub

    Private Sub ogcacc_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcjob.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvjob
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcjob.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FTOrderNo>0", "FNMatSeq")
                        R!FNMatSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridDataOrder()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridDataOrder()

            Case DevExpress.XtraEditors.NavigatorButtonType.Edit
                Dim dt As DataTable = CType(RepositoryFTOrderNo.DataSource, DataTable).Copy


                With CType(ogcjob.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows

                        If R!FTOrderNo.ToString <> "" Then
                            For Each Rc In dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'")
                                Rc!FTStateSelect = "1"
                                Rc!FNQuantity = Val(R!FNQuantity.ToString)
                            Next
                        End If
                    Next

                End With

                With _AddItemPopup
                    .AddMat = False
                    .ogcjob.DataSource = dt.Copy
                    .ocmadd.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()
                    If .AddMat Then

                        dt = CType(.ogcjob.DataSource, DataTable).Copy

                        Dim _Price As Double = FNPOPrice.Value
                        Dim _DisPer As Double = FNDisPer.Value
                        Dim _DisAmt As Double = FNDisAmt.Value


                        With CType(Me.ogcjob.DataSource, DataTable)
                            .Rows.Clear()

                            For Each R As DataRow In dt.Select("FTStateSelect='1'")
                                .Rows.Add(R!FTOrderNo.ToString, Val(R!FNQuantity), CDbl(Format(Val(R!FNQuantity) * (_Price - _DisAmt), HI.ST.Config.AmtFormat)), "")

                            Next

                            .AcceptChanges()
                        End With

                    End If

                End With
            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub InitGridDataOrder()

        Try
            If Not (Me.ogcjob.DataSource Is Nothing) Then

                With CType(Me.ogcjob.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTOrderNo=''").Length > 0 Then
                    Else

                        .Rows.Add("", 0, 0)
                    End If
                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RepositoryFTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFTOrderNo.EditValueChanged

    End Sub

    Private Sub RepositoryFTOrderNo_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFTOrderNo.EditValueChanging
        Try
            If e.NewValue.ToString <> "" Then
                If e.NewValue.ToString = FTOrderNo.Text Then

                    e.Cancel = True

                Else

                    With CType(ogcjob.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(e.NewValue.ToString) & "'").Length > 0 Then
                            e.Cancel = True
                        Else
                            e.Cancel = False
                        End If

                    End With

                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FNMerMatTypePOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNMerMatTypePOX.SelectedIndexChanged
        If FTOrderNo.Text <> "" Then
            LoadMaterial(FTOrderNo.Text, FNMerMatTypePOX.SelectedIndex)
        End If
    End Sub

    Public Sub LoadPOOrderNo(po As String, matid As Integer, OrderNo As String)

        Dim cmdstring As String = ""

        cmdstring = "   Select  FTOrderNo, FNQuantity, FNNetAmt  "
        cmdstring &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS X WITH(NOLOCK)"
        cmdstring &= vbCrLf & "   Where (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(po) & "') AND (FNHSysRawMatId =" & Val(matid) & ") AND FTOrderNo<>'" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
        cmdstring &= vbCrLf & " ORDER BY FTOrderNo "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        ogcjob.DataSource = dt.Copy

    End Sub
    Public Sub LoadMaterial(orederno As String, Optional Indx As Integer = 0)

        If FNHSysRawmatId.Properties.Tag.ToString() = orederno & "|" & Indx.ToString() Then

        Else
            Dim cmdstring As String = ""

            cmdstring = "  Select FNHSysRawmatId,FNHSysRawmatId AS FNHSysRawMatId_Hide "
            cmdstring &= vbCrLf & "  , FTRawMatCode "
            cmdstring &= vbCrLf & "  , FNHSysRawMatColorId "
            cmdstring &= vbCrLf & "  , FNHSysRawMatSizeId "
            cmdstring &= vbCrLf & "  , FTRawMatColorCode "


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmdstring &= vbCrLf & "  , FTRawMatNameTH As  FTDescription "
                cmdstring &= vbCrLf & "  , FTRawMatColorNameTH  As  FTColorDescription "
            Else
                cmdstring &= vbCrLf & "  , FTRawMatNameEN As  FTDescription "
                cmdstring &= vbCrLf & "  , FTRawMatColorNameEN  As  FTColorDescription "
            End If

            cmdstring &= vbCrLf & "  , FTRawMatColorNameTH  "
            cmdstring &= vbCrLf & "  , FTRawMatColorNameEN  "
            cmdstring &= vbCrLf & "  , FTRawMatSizeCode "
            cmdstring &= vbCrLf & "  , FNHSysUnitId "
            cmdstring &= vbCrLf & "  , FTUnitCode "
            cmdstring &= vbCrLf & "  , FTFabricFrontSize "
            cmdstring &= vbCrLf & "  , FNUsedQuantity "
            cmdstring &= vbCrLf & "  , FNPrice "

            If Indx = 0 Then
                cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.FT_GET_ROWMAT_ADD_POFABRIC ('" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') AS A  "

            Else
                cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.FT_GET_ROWMAT_ADD_POACC ('" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "') AS A  "
            End If

            cmdstring &= vbCrLf & "   ORDER BY FTRawMatCode, FTRawMatColorCode, FTRawMatSizeCode   "

            Dim dt As New DataTable
            dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)

            FNHSysRawmatId.Properties.DataSource = dt.Copy
            FNHSysRawmatId.Properties.DisplayMember = "FTRawMatCode"
            FNHSysRawmatId.Properties.ValueMember = "FNHSysRawmatId"

            FNHSysRawmatId.Properties.Tag = orederno & "|" & Indx.ToString()
        End If

    End Sub

    Private Sub RepositoryQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryQuantity.EditValueChanging
        Try
            If e.NewValue.ToString <> "" Then
                If Val(e.NewValue.ToString) >= 0 Then
                    Dim _Price As Double = FNPOPrice.Value
                    Dim _DisPer As Double = FNDisPer.Value
                    Dim _DisAmt As Double = FNDisAmt.Value


                    With ogvjob
                        .SetFocusedRowCellValue("FNNetAmt", CDbl(Format(Val(e.NewValue.ToString) * (_Price - _DisAmt), HI.ST.Config.AmtFormat)))
                    End With
                Else
                    e.Cancel = True
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNHSysRawmatIdB_EditValueChanged(sender As Object, e As EventArgs)
        Try

            Dim RawMatID As Integer = 0
            If sender.Text <> "" Then
                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                FNHSysRawMatId_None.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                FTRawMatColorCode.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                FTRawMatSizeCode.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                FTFabricFrontSize.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTFabricFrontSize").ToString()
                FTRawMatColorDesc.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTColorDescription").ToString()
                FTRawMatColorNameTH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorNameTH").ToString()
                FTRawMatColorNameEN.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorNameEN").ToString()
                FNPOPrice.Value = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNUsedQuantity").ToString())
                FNHSysUnitIdPO.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                RawMatID = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRawMatId_Hide").ToString())
                CXFNHSysRawmatId_Hide.Value = RawMatID
            Else
                CXFNHSysRawmatId_Hide.Value = 0
            End If



            If Integer.Parse(RawMatID) > 0 Then
                Dim _Qry As String
                Dim _dt As DataTable

                If FTOrderNo.Properties.ReadOnly = False Then

                    _Qry = "Select TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode,ISNULL(A.FNSurchangeAmt,0) As FNSurchangeAmt,A.FTOGacDate "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As A With(NOLOCK) "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As B With(NOLOCK) On A.FNHSysUnitId = B.FNHSysUnitId "
                    _Qry &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.PONO) & "'"
                    _Qry &= vbCrLf & " AND A.FNHSysRawMatId=" & Integer.Parse(RawMatID) & ""

                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                    If _dt.Rows.Count > 0 Then

                        For Each R As DataRow In _dt.Rows
                            FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                            FNPOPrice.Value = Val(R!FNPrice.ToString)
                            FNSurchangeAmt.Value = Val(R!FNSurchangeAmt.ToString)
                            FTOGacDate.Text = R!FTOGacDate.ToString()
                            Exit For
                        Next

                    Else

                        _Qry = "SELECT TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK) INNER JOIN "
                        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS B WITH(NOLOCK) ON A.FNHSysUnitId = B.FNHSysUnitId "
                        _Qry &= vbCrLf & " WHERE A.FNHSysRawMatId=" & Integer.Parse(RawMatID) & ""
                        _Qry &= vbCrLf & "  AND P.FNHSysSuplId=" & Integer.Parse(Me.FNSuplID) & ""
                        _Qry &= vbCrLf & " ORDER BY P.FDPurchaseDate DESC "

                        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
                        For Each R As DataRow In _dt.Rows
                            'FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                            FNPOPrice.Value = Val(R!FNPrice.ToString)

                            Exit For
                        Next

                        _dt.Dispose()

                    End If

                    _dt.Dispose()

                    Try
                        If FNHSysUnitIdPO.Text <> "" Then
                            HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysUnitIdPO, New System.EventArgs)
                        End If
                    Catch ex As Exception
                    End Try

                Else
                    _Qry = "SELECT TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK) INNER JOIN "
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS B WITH(NOLOCK) ON A.FNHSysUnitId = B.FNHSysUnitId "
                    _Qry &= vbCrLf & " WHERE A.FNHSysRawMatId=" & Integer.Parse(RawMatID) & ""
                    _Qry &= vbCrLf & "  AND P.FNHSysSuplId=" & Integer.Parse(Me.FNSuplID) & ""
                    _Qry &= vbCrLf & " ORDER BY P.FDPurchaseDate DESC "

                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
                    For Each R As DataRow In _dt.Rows
                        'FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                        FNPOPrice.Value = Val(R!FNPrice.ToString)
                        Exit For
                    Next

                    _dt.Dispose()
                End If

                Call LoadOrderNo(RawMatID)
                Call LoadPOOrderNo(FTPurchaseNo.Text, RawMatID, FTOrderNo.Text)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub SetSelectItem(RawId As Integer)
        FNHSysRawmatId.EditValue = Nothing
        FNHSysRawmatId.EditValue = RawId
    End Sub
    Public Sub FNHSysRawmatId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysRawmatId.EditValueChanged
        Try

            Dim RawMatID As Integer = Val(FNHSysRawmatId.EditValue.ToString)
            If sender.Text <> "" Then
                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                Try


                    With CType(FNHSysRawmatId.Properties.DataSource, DataTable)
                        For Each Rx As DataRow In .Select("FNHSysRawmatId=" & RawMatID & "")
                            FNHSysRawMatId_None.Text = Rx!FTDescription.ToString()
                            FTRawMatColorCode.Text = Rx!FTRawMatColorCode.ToString()
                            FTRawMatSizeCode.Text = Rx!FTRawMatSizeCode.ToString()
                            FTFabricFrontSize.Text = Rx!FTFabricFrontSize.ToString()
                            FTRawMatColorDesc.Text = Rx!FTColorDescription.ToString()
                            FTRawMatColorNameTH.Text = Rx!FTRawMatColorNameTH.ToString()
                            FTRawMatColorNameEN.Text = Rx!FTRawMatColorNameEN.ToString()
                            FNPOPrice.Value = Val(Rx!FNPrice.ToString())
                            FNPOQuantity.Value = Val(Rx!FNUsedQuantity.ToString())
                            FNHSysUnitIdPO.Text = Rx!FTUnitCode.ToString()
                        Next
                    End With


                    'FNHSysRawMatId_None.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                    'FTRawMatColorCode.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                    'FTRawMatSizeCode.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                    'FTFabricFrontSize.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTFabricFrontSize").ToString()
                    'FTRawMatColorDesc.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTColorDescription").ToString()
                    'FTRawMatColorNameTH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorNameTH").ToString()
                    'FTRawMatColorNameEN.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorNameEN").ToString()
                    'FNPOPrice.Value = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNUsedQuantity").ToString())
                    'FNHSysUnitIdPO.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    ' RawMatID = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRawMatId_Hide").ToString())
                Catch ex As Exception

                End Try

                CXFNHSysRawmatId_Hide.Value = RawMatID
            Else
                CXFNHSysRawmatId_Hide.Value = 0
            End If

            If Integer.Parse(RawMatID) > 0 Then
                Dim _Qry As String
                Dim _dt As DataTable

                If FTOrderNo.Properties.ReadOnly = False Then

                    _Qry = "Select TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode,ISNULL(A.FNSurchangeAmt,0) As FNSurchangeAmt,A.FTOGacDate "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As A With(NOLOCK) "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As B With(NOLOCK) On A.FNHSysUnitId = B.FNHSysUnitId "
                    _Qry &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.PONO) & "'"
                    _Qry &= vbCrLf & " AND A.FNHSysRawMatId=" & Integer.Parse(RawMatID) & ""

                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

                    If _dt.Rows.Count > 0 Then

                        For Each R As DataRow In _dt.Rows
                            FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                            FNPOPrice.Value = Val(R!FNPrice.ToString)
                            FNSurchangeAmt.Value = Val(R!FNSurchangeAmt.ToString)
                            FTOGacDate.Text = R!FTOGacDate.ToString()
                            Exit For
                        Next

                    Else

                        _Qry = "SELECT TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK) INNER JOIN "
                        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS B WITH(NOLOCK) ON A.FNHSysUnitId = B.FNHSysUnitId "
                        _Qry &= vbCrLf & " WHERE A.FNHSysRawMatId=" & Integer.Parse(RawMatID) & ""
                        _Qry &= vbCrLf & "  AND P.FNHSysSuplId=" & Integer.Parse(Me.FNSuplID) & ""
                        _Qry &= vbCrLf & " ORDER BY P.FDPurchaseDate DESC "

                        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
                        For Each R As DataRow In _dt.Rows
                            'FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                            FNPOPrice.Value = Val(R!FNPrice.ToString)

                            Exit For
                        Next

                        _dt.Dispose()

                    End If

                    _dt.Dispose()

                    Try
                        If FNHSysUnitIdPO.Text <> "" Then
                            HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysUnitIdPO, New System.EventArgs)
                        End If
                    Catch ex As Exception
                    End Try

                Else
                    _Qry = "SELECT TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK) INNER JOIN "
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS B WITH(NOLOCK) ON A.FNHSysUnitId = B.FNHSysUnitId "
                    _Qry &= vbCrLf & " WHERE A.FNHSysRawMatId=" & Integer.Parse(RawMatID) & ""
                    _Qry &= vbCrLf & "  AND P.FNHSysSuplId=" & Integer.Parse(Me.FNSuplID) & ""
                    _Qry &= vbCrLf & " ORDER BY P.FDPurchaseDate DESC "

                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
                    For Each R As DataRow In _dt.Rows
                        'FNHSysUnitIdPO.Text = R!FTUnitCode.ToString()
                        FNPOPrice.Value = Val(R!FNPrice.ToString)
                        Exit For
                    Next

                    _dt.Dispose()
                End If

                Call LoadOrderNo(RawMatID)
                Call LoadPOOrderNo(FTPurchaseNo.Text, RawMatID, FTOrderNo.Text)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvjob_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvjob.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Delete
                With Me.ogvjob
                    If .FocusedRowHandle < 0 Then Exit Sub

                    .DeleteRow(.FocusedRowHandle)

                End With

                InitGridDataOrder()
            Case System.Windows.Forms.Keys.Down
                InitGridDataOrder()

        End Select
    End Sub
End Class