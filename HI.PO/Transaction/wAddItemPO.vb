Imports System.ComponentModel
Imports System.Windows.Forms

Public Class wAddItemPO

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
        If FNHSysRawMatId.Text = "" Then
            FNHSysRawMatId.Focus()
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
                    cmd &= vbCrLf & " Where (A.FNHSysRawMatId = " & Val(FNHSysRawMatId.Properties.Tag.ToString()) & ") "

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
        If Me.FNHSysRawMatId.Text <> "" And Me.FNHSysRawMatId.Properties.Tag.ToString <> "" Then
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
            FNHSysRawMatId.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        If Me.FTOrderNo.Properties.ReadOnly = True And FNHSysRawMatId.Properties.Buttons(0).Enabled = False Then
            If (CheckReceive(Me.PONO, 0) = False) Then Exit Sub
        Else
            If (CheckReceive(Me.PONO) = False) Then Exit Sub
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
                If Me.FNHSysRawMatId.Properties.Buttons(0).Enabled = False And Me.FTOrderNo.Properties.ReadOnly Then
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
        Try
            Select Case FNOrderType.SelectedIndex
                Case 0
                    'FNHSysRawMatId.Properties.Buttons(0).Tag = "136"
                    FNHSysRawMatId.Properties.Buttons(0).Tag = "317"

                    If FNHSysRawMatId.Text <> "" Then
                        Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
                    End If

                Case Else
                    'FNHSysRawMatId.Properties.Buttons(0).Tag = "106"
                    FNHSysRawMatId.Properties.Buttons(0).Tag = "317"

                    If FNHSysRawMatId.Text <> "" Then
                        Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
                    End If
            End Select
        Catch ex As Exception

        End Try

    End Sub


    Private Sub FNHSysRawMatId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysRawMatId.EditValueChanged, FTRawMatColorCode.EditValueChanged, FTRawMatSizeCode.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.DynamicButtonedit_ValueChanged(AddressOf FNHSysRawMatId_EditValueChanged), New Object() {sender, e})
            Else

                If Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) > 0 Then
                    Dim _Qry As String
                    Dim _dt As DataTable

                    If FTOrderNo.Properties.ReadOnly = False Then

                        _Qry = "SELECT TOP 1 A.FTPurchaseNo, A.FNHSysRawMatId, A.FNPrice, B.FTUnitCode,ISNULL(A.FNSurchangeAmt,0) AS FNSurchangeAmt,A.FTOGacDate "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS B WITH(NOLOCK) ON A.FNHSysUnitId = B.FNHSysUnitId "
                        _Qry &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.PONO) & "'"
                        _Qry &= vbCrLf & " AND A.FNHSysRawMatId=" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & ""

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
                            _Qry &= vbCrLf & " WHERE A.FNHSysRawMatId=" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & ""
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
                        _Qry &= vbCrLf & " WHERE A.FNHSysRawMatId=" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & ""
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

                    Dim _Str As String = ""
                    Dim _FTRawMatColorNameTH As String = ""
                    Dim _FTRawMatColorNameEN As String = ""
                    Dim _dtRawMatColor As New DataTable

                    _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                    _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
                    _Str &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND RM.FNHSysRawMatId =" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & "  AND ISNULL(A.FTRawMatColorNameEN,'') <> '' "

                    Try
                        _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        If _dtRawMatColor.Rows.Count > 0 Then
                            For Each R As DataRow In _dtRawMatColor.Rows

                                _FTRawMatColorNameTH = R!FTRawMatColorNameTH.ToString
                                _FTRawMatColorNameEN = R!FTRawMatColorNameEN.ToString

                                Exit For
                            Next
                        Else
                            _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                            _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                            _Str &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.PONO) & "' "
                            _Str &= vbCrLf & " AND A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' "
                            _Str &= vbCrLf & " AND A.FNHSysRawMatId =" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & " AND ISNULL(A.FTRawMatColorNameEN,'') <> ''"

                            _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                            If _dtRawMatColor.Rows.Count > 0 Then
                                For Each R As DataRow In _dtRawMatColor.Rows

                                    _FTRawMatColorNameTH = R!FTRawMatColorNameTH.ToString
                                    _FTRawMatColorNameEN = R!FTRawMatColorNameEN.ToString

                                    Exit For
                                Next
                            Else
                                _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                                _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) "
                                _Str &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.PONO) & "' "
                                _Str &= vbCrLf & " AND A.FNHSysRawMatId =" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & "  AND ISNULL(A.FTRawMatColorNameEN,'') <> ''"

                                _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                                If _dtRawMatColor.Rows.Count > 0 Then
                                    For Each R As DataRow In _dtRawMatColor.Rows

                                        _FTRawMatColorNameTH = R!FTRawMatColorNameTH.ToString
                                        _FTRawMatColorNameEN = R!FTRawMatColorNameEN.ToString

                                        Exit For
                                    Next
                                Else


                                    _Str = "  SELECT TOP 1  A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                                    _Str &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS A WITH(NOLOCK)  "
                                    _Str &= vbCrLf & " WHERE A.FNHSysRawMatId =" & Integer.Parse(Val(FNHSysRawMatId.Properties.Tag.ToString())) & " "

                                    _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MASTER)

                                    If _dtRawMatColor.Rows.Count > 0 Then
                                        For Each R As DataRow In _dtRawMatColor.Rows

                                            _FTRawMatColorNameTH = R!FTRawMatColorNameTH.ToString
                                            _FTRawMatColorNameEN = R!FTRawMatColorNameEN.ToString

                                            Exit For
                                        Next
                                    End If

                                End If

                            End If
                        End If

                    Catch ex As Exception
                    End Try

                    _dtRawMatColor.Dispose()

                    Select Case HI.ST.Lang.Language
                        Case ST.Lang.eLang.TH
                            Me.FTRawMatColorDesc.Text = _FTRawMatColorNameTH

                        Case Else
                            Me.FTRawMatColorDesc.Text = _FTRawMatColorNameEN
                    End Select

                    FTRawMatColorNameTH.Text = _FTRawMatColorNameTH
                    FTRawMatColorNameEN.Text = _FTRawMatColorNameEN

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Try
            FNHSysRawMatId_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wAddItemPO_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FNSurchangeAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNSurchangeAmt.EditValueChanged

    End Sub
End Class