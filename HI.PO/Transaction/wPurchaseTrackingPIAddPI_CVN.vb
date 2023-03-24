Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wPurchaseTrackingPIAddPI_CVN

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
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

    Private _PINO As String = ""
    Public Property PINO As String
        Get
            Return _PINO
        End Get
        Set(value As String)
            _PINO = value
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

    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ogvpo.OptionsView.ShowAutoFilterRow = False
        ogvpo.ClearColumnsFilter()

        For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvpo.Columns
            With oGridCol


                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            End With
        Next

        ogvsurcharge.OptionsView.ShowAutoFilterRow = False
        ogvsurcharge.ClearColumnsFilter()

        For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvsurcharge.Columns
            With oGridCol


                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            End With
        Next

        ogvCNDN.OptionsView.ShowAutoFilterRow = False
        ogvCNDN.ClearColumnsFilter()

        For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvCNDN.Columns
            With oGridCol


                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            End With
        Next


        otx.SelectedTabPageIndex = 0

        LoadDataAcc()
    End Sub

    Private Sub LoadDataAcc()
        Dim cmdstring As String = ""
        Dim dt As DataTable


        cmdstring = "  SELECT  FNHSysAccountId, FTAccountCode , FTAccountCode AS [FTPICostCode]"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            cmdstring &= vbCrLf & " ,FTAccountNameTH AS FTAccountName"
        Else
            cmdstring &= vbCrLf & " ,FTAccountNameEN AS FTAccountName"
        End If

        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMAccount"
        cmdstring &= vbCrLf & "  WHERE FTStateActive = '1'AND FTPIStateActive = '1' ORDER BY FTAccountCode  "
        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)
        repositoryFnhAccPICostID.DataSource = dt.Copy

    End Sub
    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub


    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTPINo.Text.Trim <> "" Then
            Dim CheckDocPI As String = ""

            If FTPINo.Enabled And FTPINo.ReadOnly = False Then
                Dim cmdstring As String = ""
                cmdstring = "select top 1 FTPINo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI where FTPINo='" & HI.UL.ULF.rpQuoted(FTPINo.Text.Trim()) & "'"

                CheckDocPI = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")
            End If


            'If FNPIDocType.SelectedIndex = 1 Then
            '    If FTBLNo.Text.Trim = "" Then
            '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTBLNo_lbl.Text)
            '        FTBLNo.Focus()
            '        Return False
            '    End If

            '    If FTBLDate.Text.Trim = "" Then
            '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTBLDate_lbl.Text)
            '        Return False
            '    End If

            'End If
            If CheckDocPI = "" Then

                If FTPIMatType.Text.Trim <> "" Then
                    If Me.FTPIDate.Text <> "" Then
                        If Me.FTRcvPIDate.Text <> "" Then
                            If Me.FTPISuplCFMDeliveryDate.Text <> "" Then
                                If FNPIGrandQuantity.Value > 0 Then


                                    If FNPIGrandNetAmt.Value > 0 And FNPIGrandTotalAmt.Value > 0 Then

                                        With CType(Me.ogcpo.DataSource, DataTable)
                                            .AcceptChanges()

                                            Select Case True
                                                Case .Select("FNPIPOQuantity < 0").Length > 0
                                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNPIPOQuantity.GetCaption)
                                                Case .Select("FNPIPONetAmt < 0").Length > 0
                                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNPIPONetAmt.GetCaption)
                                                Case Else
                                                    _Pass = True
                                            End Select
                                            'If .Select("FNPIPOQuantity < 0 OR FNPIPONetAmt <0  ").Length Then


                                            'Else
                                            '    _Pass = True
                                            'End If
                                        End With





                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNPIGrandNetAmt_lbl.Text)
                                        FNPIGrandNetAmt.Focus()
                                    End If



                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNPIGrandQuantity_lbl.Text)
                                    FNPIGrandQuantity.Focus()
                                End If
                            Else

                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPISuplCFMDeliveryDate_lbl.Text)
                                FTPISuplCFMDeliveryDate.Focus()

                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTRcvPIDate_lbl.Text)
                            FTRcvPIDate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPIDate_lbl.Text)
                        FTPIDate.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPIMatType_lbl.Text)
                    FTPIMatType.Focus()
                End If


            Else

                HI.MG.ShowMsg.mInfo("พบหมายเลข PI นี้ในระบบแล้ว !!!", 122100547, Me.Text,, MessageBoxIcon.Warning)
                FTPINo.Focus()
                FTPINo.SelectAll()
            End If


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPINo_lbl.Text)
            FTPINo.Focus()
        End If


        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        'If Me.FTOrderNo.Properties.ReadOnly = True And FNHSysRawMatId.Properties.Buttons(0).Enabled = False Then
        '    If (CheckReceive(Me.PONO, 0) = False) Then Exit Sub
        'Else
        '    If (CheckReceive(Me.PONO) = False) Then Exit Sub
        'End If

        'If HI.ST.ValidateData.CloseJob(Me.FTOrderNo.Text) Then
        '    HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        SumGridAmount()

        If ValidateData() Then
            Me.AddMat = True
            Me.Close()
        End If

    End Sub


    Public Sub LoadDataPI(PiNo As String)

        Dim cmdstring As String = ""
        Dim dtPo As New DataTable
        Dim dtPi As New DataTable
        Dim dtSurcharge As New DataTable
        Dim dtCNDN As New DataTable

        If PiNo <> "" Then

            cmdstring = " SELECT  TOP 1  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FTPINo, A.FTPurchaseNo, A.FTPIDate, A.FTRcvPIDate, A.FTPISuplCFMDeliveryDate, A.FNPIPayType, A.FTPIRemark, "
            cmdstring &= vbCrLf & "    A.FTPIPayTypeBy, A.FTPIPayTypeDate, A.FTPIPayTypeTime, A.FTStatePaid, A.FTPaidDate, A.FTPaidNote, A.FTStatePaidBy, A.FTStatePaidDate, A.FTStatePaidTime, A.FNPIPOQuantity, A.FNPIPONetAmt, "
            cmdstring &= vbCrLf & "    A.FNPIGrandQuantity, A.FNPIGrandNetAmt, A.FNPOQuantity, A.FNPONetAmt, A.FTPIPayTypeRemark, A.FTPIPayDate, A.FNCNAmt, A.FNDNAmt, A.FNSurchargeAmt, A.FNPOBalQuantity, A.FNPOBalGrandAmt, "
            cmdstring &= vbCrLf & "    A.FNPIGrandTotalAmt,A.FNPIDocType,A.FTPIMatType,A.FTBLNo,A.FTBLDate "
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI AS A "
            cmdstring &= vbCrLf & " where A.FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "' AND A.FNHSysSuplId =" & Val(FNHSysSuplId.Properties.Tag.ToString) & ""
            cmdstring &= vbCrLf & " ORDER BY A.FTPurchaseNo "

            dtPi = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            For Each R As DataRow In dtPi.Rows

                FNPIDocType.SelectedIndex = Val(R!FNPIDocType.ToString)

                FTPIDate.Text = HI.UL.ULDate.ConvertEN(R!FTPIDate.ToString)
                FTRcvPIDate.Text = HI.UL.ULDate.ConvertEN(R!FTRcvPIDate.ToString)
                FTPISuplCFMDeliveryDate.Text = HI.UL.ULDate.ConvertEN(R!FTPISuplCFMDeliveryDate.ToString)
                FTRemark.Text = R!FTPIRemark.ToString
                FNPIGrandQuantity.Value = Val(R!FNPIGrandQuantity.ToString)
                FNPIGrandNetAmt.Value = Val(R!FNPIGrandNetAmt.ToString)
                FNCNAmt.Value = Val(R!FNCNAmt.ToString)
                FNDNAmt.Value = Val(R!FNDNAmt.ToString)
                FNSurchargeAmt.Value = Val(R!FNSurchargeAmt.ToString)
                FNPIGrandTotalAmt.Value = Val(R!FNPIGrandTotalAmt.ToString)


                FTPIMatType.Text = R!FTPIMatType.ToString
                FTBLNo.Text = R!FTBLNo.ToString
                FTBLDate.Text = HI.UL.ULDate.ConvertEN(R!FTBLDate.ToString)
                Exit For
            Next

            cmdstring = " SELECT    FTPurchaseNo, FNPOQuantity,FNPONetAmt,FNPOBalQuantity,FNPOBalGrandAmt,FNPIPOQuantity,FNPIPONetAmt,FTUnitCode"
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI "
            cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "'  AND FNHSysSuplId =" & Val(FNHSysSuplId.Properties.Tag.ToString) & ""
            cmdstring &= vbCrLf & " ORDER BY FTPurchaseNo "

            dtPo = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


            ''best 202207
            'cmdstring = " SELECT      FNSeq, FTDescription, FNSurchargeAmt, FTRemark"
            'cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge "
            'cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "'  AND FNHSysSuplId =" & Val(FNHSysSuplId.Properties.Tag.ToString) & ""
            'cmdstring &= vbCrLf & " ORDER BY FNSeq "
            cmdstring = " SELECT      FNSeq, P.FNHSysAccountId ,FTAccountCode AS [FTPICostCode], FTAccountNameTH as FTDescription, FNSurchargeAmt, FTRemark"
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge P "
            cmdstring &= vbCrLf & " LEFT JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMAccount A ON P.FNHSysAccountId = A.FNHSysAccountId  "
            cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "'  AND FNHSysSuplId =" & Val(FNHSysSuplId.Properties.Tag.ToString) & ""
            cmdstring &= vbCrLf & " ORDER BY FNSeq "

            dtSurcharge = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


            cmdstring = " SELECT    FNSeq, FTDocRefNo, FTDocRefDate, FTDocType, FNDocAmt, FTDocRemark"
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_CNDN "
            cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "'  AND FNHSysSuplId =" & Val(FNHSysSuplId.Properties.Tag.ToString) & ""
            cmdstring &= vbCrLf & " ORDER BY FTDocType,FNSeq "

            dtCNDN = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


        Else

            cmdstring = " SELECT  TOP 0   FTPurchaseNo, FNPOQuantity,FNPONetAmt,FNPOBalQuantity,FNPOBalGrandAmt,FNPIPOQuantity,FNPIPONetAmt,FTUnitCode"
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI "
            cmdstring &= vbCrLf & " where FTPINo =''"
            cmdstring &= vbCrLf & " ORDER BY FTPurchaseNo "

            dtPo = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            ''best 202207
            'cmdstring = " SELECT    TOP 0   FNSeq, FTDescription, FNSurchargeAmt, FTRemark"
            'cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge "
            'cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "'"
            'cmdstring &= vbCrLf & " ORDER BY FNSeq "
            cmdstring = " SELECT      FNSeq, P.FNHSysAccountId ,FTAccountCode AS [FTPICostCode], FTAccountNameTH as FTDescription, FNSurchargeAmt, FTRemark"
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_Surcharge P "
            cmdstring &= vbCrLf & " LEFT JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMAccount A ON P.FNHSysAccountId = A.FNHSysAccountId  "
            cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "' "
            cmdstring &= vbCrLf & " ORDER BY FNSeq "

            dtSurcharge = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


            cmdstring = " SELECT   TOP 0  FNSeq, FTDocRefNo, FTDocRefDate, FTDocType, FNDocAmt, FTDocRemark"
            cmdstring &= vbCrLf & " FROM          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_PI_CNDN "
            cmdstring &= vbCrLf & " where FTPINo ='" & HI.UL.ULF.rpQuoted(PiNo) & "'"
            cmdstring &= vbCrLf & " ORDER BY FTDocType,FNSeq "

            dtCNDN = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        End If

        ogcpo.DataSource = dtPo.Copy
        ogcsurcharge.DataSource = dtSurcharge.Copy
        ogcCNDN.DataSource = dtCNDN.Copy

    End Sub

    Public Sub AddPo(PurchaseNo As String, POQuantity As Double, PONetAmt As Double, POBalQuantity As Double, POBalGrandAmt As Double, PIPOQuantity As Double, PIPONetAmt As Double, UnitCode As String)
        If ogcpo.DataSource Is Nothing Then
            LoadDataPI("")
        End If
        Dim dtpo As DataTable
        With CType(ogcpo.DataSource, DataTable)
            .AcceptChanges()
            dtpo = .Copy

        End With

        If dtpo.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseNo) & "'").Length > 0 Then

            For Each R As DataRow In dtpo.Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PurchaseNo) & "'")
                R!FNPOQuantity = POQuantity
                R!FNPONetAmt = PONetAmt
                R!FNPOBalQuantity = POBalQuantity
                R!FNPOBalGrandAmt = POBalGrandAmt
                R!FNPIPOQuantity = PIPOQuantity
                R!FNPIPONetAmt = PIPONetAmt
                R!FTUnitCode = UnitCode
            Next

        Else
            dtpo.Rows.Add(PurchaseNo, POQuantity, PONetAmt, POBalQuantity, POBalGrandAmt, PIPOQuantity, PIPONetAmt, UnitCode)
        End If

        ogcpo.DataSource = dtpo.Copy
    End Sub
    Private Sub FNOrderType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        'Try
        '    Select Case FNOrderType.SelectedIndex
        '        Case 0
        '            'FNHSysRawMatId.Properties.Buttons(0).Tag = "136"
        '            FNHSysRawMatId.Properties.Buttons(0).Tag = "469"

        '            If FNHSysRawMatId.Text <> "" Then
        '                Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        '            End If

        '        Case Else
        '            'FNHSysRawMatId.Properties.Buttons(0).Tag = "106"
        '            FNHSysRawMatId.Properties.Buttons(0).Tag = "469"

        '            If FNHSysRawMatId.Text <> "" Then
        '                Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        '            End If
        '    End Select
        'Catch ex As Exception

        'End Try

    End Sub



    Private Sub wAddItemPO_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs)

    End Sub


    Public Sub SumGridAmount()

        Try

            Dim Amount As Decimal = 0.0
            Dim Qty As Decimal = 0.0

            FNPIGrandQuantity.Value = 0
            FNPIGrandNetAmt.Value = 0
            With CType(Me.ogcpo.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows

                    Qty = Qty + Decimal.Parse((R!FNPIPOQuantity.ToString))
                    Amount = Amount + Decimal.Parse((R!FNPIPONetAmt.ToString))

                Next

            End With

            FNPIGrandQuantity.Value = Qty
            FNPIGrandNetAmt.Value = Amount

            Dim SurchargeAmount As Decimal = 0.0

            With CType(Me.ogcsurcharge.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows

                    If R!FTDescription.ToString.Trim <> "" Then
                        SurchargeAmount = SurchargeAmount + Decimal.Parse((R!FNSurchargeAmt.ToString))
                    End If


                Next

            End With


            Dim CNAmount As Decimal = 0.0
            Dim DNAmount As Decimal = 0.0

            With CType(Me.ogcCNDN.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows

                    If R!FTDocRefNo.ToString.Trim <> "" Then
                        If R!FTDocType.ToString.Trim = "DN" Then
                            DNAmount = DNAmount + Decimal.Parse((R!FNDocAmt.ToString))
                        Else
                            CNAmount = CNAmount + Decimal.Parse((R!FNDocAmt.ToString))
                        End If

                    End If


                Next

            End With


            FNCNAmt.Value = CNAmount
            FNDNAmt.Value = DNAmount
            FNSurchargeAmt.Value = SurchargeAmount

            FNPIGrandTotalAmt.Value = (FNPIGrandNetAmt.Value + FNSurchargeAmt.Value + FNCNAmt.Value) - FNDNAmt.Value

        Catch ex As Exception
        End Try



    End Sub

    Private Sub RepositoryQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryQuantity.EditValueChanging
        Try


            If IsNumeric(e.NewValue) Then

                If Val(e.NewValue.ToString) >= 0 Then



                    With Me.ogvpo

                        .SetRowCellValue(.FocusedRowHandle, "FNPIPOQuantity", Format(Val(e.NewValue.ToString), "0.0000"))
                    End With

                    With DirectCast(Me.ogcpo.DataSource, DataTable)
                        .AcceptChanges()
                    End With



                    Call SumGridAmount()
                Else
                    e.Cancel = True
                End If

            Else
                e.Cancel = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemAmt_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemAmt.EditValueChanging
        Try
            If IsNumeric(e.NewValue) Then

                If Val(e.NewValue.ToString) >= 0 Then
                    With Me.ogvpo

                        .SetRowCellValue(.FocusedRowHandle, "FNPIPONetAmt", Format(Val(e.NewValue.ToString), "0.00"))
                    End With

                    With DirectCast(Me.ogcpo.DataSource, DataTable)
                        .AcceptChanges()
                    End With

                    Call SumGridAmount()

                Else
                    e.Cancel = True
                End If

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
        End Try
    End Sub



    Private Sub RepositoryFNHSysAccOverHeadCostID_EditValueChanged(sender As Object, e As EventArgs) Handles repositoryFnhAccPICostID.EditValueChanged
        Try
            With Me.ogvsurcharge
                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                Dim FNHSysAccountId As Integer = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysAccountId").ToString())

                .SetFocusedRowCellValue("FNHSysAccountId", FNHSysAccountId)

                ' .SetFocusedRowCellValue("FTOverHeadCostCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTAccountCode").ToString())
                .SetFocusedRowCellValue("FTDescription", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTAccountName").ToString())
            End With
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ogcsurcharge_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcsurcharge.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvsurcharge
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                Call SumGridAmount()

                InitGridSurcharge()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridSurcharge()




            Case Else

        End Select

        e.Handled = True
    End Sub
    Private Sub InitGridSurcharge()

        Try



            If Not (Me.ogcsurcharge.DataSource Is Nothing) Then

                With CType(Me.ogcsurcharge.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTDescription=''").Length > 0 Then
                    Else

                        .Rows.Add(.Rows.Count + 1, 0, "", "", 0.00, "")
                    End If

                    Dim Seq As Integer = 0
                    For Each R As DataRow In .Select("FNSeq>=0")
                        Seq = Seq + 1

                        R!FNSeq = Seq
                    Next

                    .AcceptChanges()
                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvsurcharge_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvsurcharge.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Delete
                With Me.ogvsurcharge
                    If .FocusedRowHandle < 0 Then Exit Sub

                    .DeleteRow(.FocusedRowHandle)

                End With
                Call SumGridAmount()
                InitGridSurcharge()
            Case System.Windows.Forms.Keys.Down
                InitGridSurcharge()

        End Select
    End Sub



    Private Sub ogcCNDN_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcCNDN.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvCNDN
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With



                InitGridCNDN()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridCNDN()




            Case Else

        End Select

        e.Handled = True
    End Sub
    Private Sub InitGridCNDN()

        Try
            If Not (Me.ogcCNDN.DataSource Is Nothing) Then

                With CType(Me.ogcCNDN.DataSource, DataTable)
                    .AcceptChanges()





                    If .Select("FTDocRefNo=''").Length > 0 Then
                    Else

                        .Rows.Add(.Rows.Count + 1, "", "", "DN", 0.00, "")
                    End If

                    Dim Seq As Integer = 0
                    For Each R As DataRow In .Select("FNSeq>=0")
                        Seq = Seq + 1

                        R!FNSeq = Seq
                    Next

                    .AcceptChanges()

                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogvCNDNe_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvCNDN.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Delete
                With Me.ogvCNDN
                    If .FocusedRowHandle < 0 Then Exit Sub

                    .DeleteRow(.FocusedRowHandle)

                End With

                InitGridCNDN()
            Case System.Windows.Forms.Keys.Down
                InitGridCNDN()

        End Select
    End Sub

    Private Sub RepositoryItemCalcFNDocAmt_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcFNDocAmt.EditValueChanging
        Try
            If IsNumeric(e.NewValue) Then

                If Val(e.NewValue.ToString) >= 0 Then
                    With Me.ogvCNDN

                        .SetRowCellValue(.FocusedRowHandle, "FNDocAmt", Format(Val(e.NewValue.ToString), "0.00"))
                    End With

                    With DirectCast(Me.ogcCNDN.DataSource, DataTable)
                        .AcceptChanges()
                    End With

                    Call SumGridAmount()

                Else
                    e.Cancel = True
                End If

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemCalcFNSurchargeAmt_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcFNSurchargeAmt.EditValueChanging
        Try
            If IsNumeric(e.NewValue) Then

                If Val(e.NewValue.ToString) >= 0 Then
                    With Me.ogvsurcharge

                        .SetRowCellValue(.FocusedRowHandle, "FNSurchargeAmt", Format(Val(e.NewValue.ToString), "0.00"))
                    End With

                    With DirectCast(Me.ogcsurcharge.DataSource, DataTable)
                        .AcceptChanges()
                    End With

                    Call SumGridAmount()

                Else
                    e.Cancel = True
                End If

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryItemComboFTDocType_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemComboFTDocType.EditValueChanging
        Try

            With Me.ogvCNDN

                .SetRowCellValue(.FocusedRowHandle, "FTDocType", e.NewValue.ToString)
            End With

            With DirectCast(Me.ogcCNDN.DataSource, DataTable)
                .AcceptChanges()
            End With

            Call SumGridAmount()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNPIDocType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNPIDocType.SelectedIndexChanged
        If FNPIDocType.SelectedIndex = 1 Then
            FTBLNo.Text = ""
            FTBLDate.Text = ""
            FTBLNo.Visible = True
            FTBLDate.Visible = True
            FTBLNo_lbl.Visible = True
            FTBLDate_lbl.Visible = True
        Else
            FTBLNo.Text = ""
            FTBLDate.Text = ""
            FTBLNo.Visible = False
            FTBLDate.Visible = False
            FTBLNo_lbl.Visible = False
            FTBLDate_lbl.Visible = False
        End If
    End Sub
End Class