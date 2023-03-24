Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors

Public Class wDivertOrderSub

#Region "Variable Declaration"
    Private sSQL As String
    Private _bScreenLoad As Boolean = True
    Private oGridViewBreakdownDivertSrc As DevExpress.XtraGrid.Views.Grid.GridView
    Private oGridViewBreakdownDivertBalance As DevExpress.XtraGrid.Views.Grid.GridView
    Private oGridViewBreakdownDivertDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents oRepositoryFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
#End Region

#Region "Enumaration"
    Enum eDivertTabPage
        eDivertSrc = 0
        eDivertBalance = 1
        eDivertDetail = 2
    End Enum

#End Region

#Region "Property"

    Private _FTOrderNoSrc As String
    Public Property FTOrderNoSrc As String
        Get
            Return _FTOrderNoSrc
        End Get
        Set(value As String)
            _FTOrderNoSrc = value
        End Set
    End Property

    Private _TmpFTSubOrderSrc As String
    Public Property TmpFTSubOrderSrc As String
        Get
            Return _FTSubOrderSrc
        End Get
        Set(value As String)
            _TmpFTSubOrderSrc = value
        End Set
    End Property

    Private _FTSubOrderSrc As String
    Public Property FTSubOrderSrc As String
        Get
            Return _FTSubOrderSrc
        End Get
        Set(value As String)
            _FTSubOrderSrc = value
        End Set
    End Property

    Private Shared _DTStateOrderSub As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property LoadStateOrderNoSub(ByVal paramFTOrderNo As String, ByVal paramFTOrderNoSub As String) As System.Data.DataTable
        Get
            _DTStateOrderSub = Nothing

            Dim sSQL As String
            sSQL = ""

            '...State Sub Factory Order No
            'sSQL = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubMRP"
            'sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubProduction"
            'sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            'sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
            'sSQL &= Environment.NewLine & "                                                                                                                      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS C (NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "												                                                                                                                                                                                                     AND B.FTOrderProdNo = C.FTOrderProdNo"
            'sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            'sSQL &= Environment.NewLine & "	               AND C.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubCutting"
            'sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            'sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            'sSQL &= Environment.NewLine & "                                                                                                                                 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
            'sSQL &= Environment.NewLine & "																		                                                           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D (NOLOCK) ON C.FTOrderNo = D.FTOrderNo"
            'sSQL &= Environment.NewLine & "																		                                                                                                                                                                                        AND C.FTOrderProdNo = D.FTOrderProdNo"
            'sSQL &= Environment.NewLine & "																		                                                           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS E (NOLOCK) ON A.FNHSysUnitSectId = E.FNHSysUnitSectId"
            'sSQL &= Environment.NewLine & "           WHERE E.FTStateActive = N'1'"
            'sSQL &= Environment.NewLine & "                 AND E.FTStateSew = N'1'"
            'sSQL &= Environment.NewLine & "                 AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            'sSQL &= Environment.NewLine & "			       AND D.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubSewing"
            'sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            'sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
            'sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo.Trim) & "'"
            'sSQL &= Environment.NewLine & "	               AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubPacking"

            sSQL = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubMRP"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubProduction"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
            sSQL &= Environment.NewLine & "                                                                                                                                         AND A.FTOrderNo = B.FTOrderNo"
            sSQL &= Environment.NewLine & "           WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "		           AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'"
            sSQL &= Environment.NewLine & "	         ), '0') AS FTStateSubCutting"

            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            sSQL &= Environment.NewLine & "                                                                                                                                 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
            sSQL &= Environment.NewLine & "																		                                                           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D (NOLOCK) ON C.FTOrderNo = D.FTOrderNo"
            sSQL &= Environment.NewLine & "																		                                                                                                                                                                                        AND C.FTOrderProdNo = D.FTOrderProdNo"
            sSQL &= Environment.NewLine & "																		                                                           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS E (NOLOCK) ON A.FNHSysUnitSectId = E.FNHSysUnitSectId"
            sSQL &= Environment.NewLine & "           WHERE E.FTStateActive = N'1'"
            sSQL &= Environment.NewLine & "                 AND E.FTStateSew = N'1'"
            sSQL &= Environment.NewLine & "                 AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "			       AND D.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubSewing"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
            sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "	               AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubPacking"

            _DTStateOrderSub = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Return _DTStateOrderSub

        End Get

    End Property

#End Region

#Region "Proc And Func"
    Private __SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(Me)

        Call PROC_GETbInitGridview()

        oGridViewBreakdownDivertSrc = Me.ogdDivertSrc.Views(0)
        oGridViewBreakdownDivertBalance = Me.ogdDivertBalance.Views(0)
        oGridViewBreakdownDivertDetail = Me.ogdDivertDT.Views(0)

        Me.ogdDivertSrc.DataSource = Nothing
        Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertSrc)

        Me.ogdDivertBalance.DataSource = Nothing
        Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertBalance)

        Me.ogdDivertDT.DataSource = Nothing
        Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertDT)

        oRepositoryFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

        With oRepositoryFNQuantity
            .Buttons(0).Visible = False
            .Mask.UseMaskAsDisplayFormat = True
            .Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            .Mask.EditMask = "###,###,###"

            .TextEditStyle = XtraEditors.Controls.TextEditStyles.Standard

            .Precision = 0

            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .DisplayFormat.FormatString = "###,###,###"

        End With

        REM AddHandler oRepositoryFNQuantity.EditValueChanged, AddressOf PROC_HelperValidateRepositoryEditValueChanged

        '...AddHandler ButtonEdit ...EditValueChanged
        '==============================================================================================================
        'Dim oCtrlButtonEdit As System.Windows.Forms.Control
        'For Each oCtrlButtonEdit In Me.ogbSubOrderNoSrc.Controls
        '    If oCtrlButtonEdit.Name.ToUpper = "FTSubOrderNoSrc".ToUpper Then
        '    Else
        '        Select Case oCtrlButtonEdit.GetType.FullName.ToString.ToUpper

        '            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper

        '                HI.TL.HandlerControl.AddHandlerObj(oCtrlButtonEdit)

        '                With CType(oCtrlButtonEdit, DevExpress.XtraEditors.ButtonEdit)

        '                    'If tTextFieldName.ToUpper <> Me.FTSubOrderNoSrc.Name.ToString.ToUpper Then
        '                    '    .Text = oDataRowSubOrderNoInfo.Item(Col).ToString
        '                    'End If

        '                    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged

        '                    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick

        '                    'AddHandler .GotFocus, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus

        '                End With

        '                'Case "DevExpress.XtraEditors.MemoEdit".ToUpper
        '                'Case "DevExpress.XtraEditors.DateEdit".ToUpper
        '            Case Else
        '                HI.TL.HandlerControl.AddHandlerObj(oCtrlButtonEdit)
        '        End Select

        '    End If

        'Next

        'HI.TL.HandlerControl.AddHandlerObj(Me.ogbSubOrderNoSrc)
        'HI.TL.HandlerControl.AddHandlerObj(Me.ogbSrcBreakdownDivert)
        'HI.TL.HandlerControl.AddHandlerObj(Me.ogbTargetBreakdownDivert)
        'HI.TL.HandlerControl.AddHandlerObj(Me.ogbBalanceBreakdownAfterDivert)
        '==============================================================================================================

    End Sub

    Private Sub PROC_HelperValidateRepositoryEditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oCalEdit As DevExpress.XtraEditors.CalcEdit = CType(sender, DevExpress.XtraEditors.CalcEdit)

        'Dim tmpDTSrcBreakdownDivert As System.Data.DataTable

        'tmpDTSrcBreakdownDivert = CType(Me.ogdDivertSrc.DataSource, System.Data.DataTable)

        'If Not tmpDTSrcBreakdownDivert Is Nothing AndAlso tmpDTSrcBreakdownDivert.Rows.Count > 0 Then

        'End If

    End Sub

    Private Sub PROC_SETxClearControl()
        Dim oCtrl As System.Windows.Forms.Control

        For Each oCtrl In Me.ogbSubOrderNoSrc.Controls
            If oCtrl.Name.ToUpper = "FTSubOrderNoSrc".ToUpper Then
            Else
                Select Case oCtrl.Name.ToUpper
                    Case "FTSubOrderNoSrc".ToUpper
                        '...Nothing
                    Case Else
                        HI.TL.HandlerControl.ClearControl(oCtrl)
                End Select

            End If

        Next

    End Sub

    Private Sub SendTabKey()
        SendKeys.Send(vbTab)
    End Sub

    'Private Sub oRepositoryFNQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles oRepositoryFNQuantity.EditValueChanged
    '    'Try
    '    '    Dim tmpDTSrcBreakdownDivert As System.Data.DataTable

    '    '    tmpDTSrcBreakdownDivert = CType(Me.ogdDivertSrc.DataSource, System.Data.DataTable)

    '    '    If Not tmpDTSrcBreakdownDivert Is Nothing AndAlso tmpDTSrcBreakdownDivert.Rows.Count > 0 Then

    '    '    End If

    '    'Catch ex As Exception
    '    '    If System.Diagnostics.Debugger.IsAttached = True Then
    '    '        MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
    '    '    End If
    '    'End Try
    'End Sub

    Private Sub oRepositoryFNQuantity_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles oRepositoryFNQuantity.KeyPress

        'If e.KeyChar = "-"c Then
        '    e.Handled = True
        '    Exit Sub
        'Else
        '    e.Handled = False
        'End If

        'If e.KeyChar = vbTab Then
        '    TryCast(sender, CalcEdit).ClosePopup()
        '    Me.BeginInvoke(New MethodInvoker(AddressOf SendTabKey))
        'End If

    End Sub

    Private Sub oRepositoryFNQuantity_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles oRepositoryFNQuantity.Leave
        ''If Val(e.Value.ToString) < 0 Then
        ''    e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction
        ''    'MessageBox.Show("Enter a date within the current month.", "Error")
        ''    MessageBox.Show("กรุณาระบุจำนวนที่มากกว่าหรือเท่ากับ 0 ...", "ข้อผิดพลาด")
        ''End If
        'Try
        '    If Val(e.ToString) < 0 Then
        '        Dim tmpDTSrcBreakdownDivert As System.Data.DataTable

        '        tmpDTSrcBreakdownDivert = CType(Me.ogdDivertSrc.DataSource, System.Data.DataTable)

        '        If Not tmpDTSrcBreakdownDivert Is Nothing AndAlso tmpDTSrcBreakdownDivert.Rows.Count > 0 Then

        '        End If

        '    End If

        'Catch ex As Exception
        '    If System.Diagnostics.Debugger.IsAttached = True Then
        '        MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
        '    End If
        'End Try

    End Sub

    Private Sub oRepositoryFNQuantity_ParseEditValue(sender As Object, e As ConvertEditValueEventArgs) Handles oRepositoryFNQuantity.ParseEditValue
        Try
            Dim sEditVal As String

            If Not e.Value Is Nothing Then
                sEditVal = e.Value.ToString()
            Else
                sEditVal = "0"
            End If

            For numLoopStr As Integer = e.Value.ToString().Length - 1 To 0 Step -1
                If Char.IsDigit(sEditVal(numLoopStr)) Then
                    Exit For
                Else
                    sEditVal = sEditVal.Remove(sEditVal.Length - 1)
                End If
            Next

            Try
                e.Value = Convert.ToDecimal(sEditVal)

                If Val(e.Value.ToString) < 0 Then e.Value = 0

            Catch ex As Exception
                e.Value = 0
            End Try

            e.Handled = True

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            '    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.ToString)
            'End If
        End Try

    End Sub

    Private Sub oRepositoryFNQuantity_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles oRepositoryFNQuantity.Validating
        Try
            'Dim tmpDTSrcBreakdownDivert As System.Data.DataTable

            'tmpDTSrcBreakdownDivert = CType(Me.ogdDivertSrc.DataSource, System.Data.DataTable)

            'If Not tmpDTSrcBreakdownDivert Is Nothing AndAlso tmpDTSrcBreakdownDivert.Rows.Count > 0 Then

            'End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub

    Private Function PROC_GETbRemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
        Try
            With pGridView
                For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1
                    'Select Case .Columns.Item(nLoopColGridView).Name.ToString.ToUpper
                    '    Case "FTMatColorName".ToString.ToUpper, "FTMatColorCode".ToString.ToUpper
                    '        If pGridView.Name = "ogvDivertSrc" Or pGridView.Name = "ogvDivertBalance" Or pGridView.Name = "ogvDivertDT" Then
                    '            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    '        Else
                    '        End If

                    '    Case Else
                    '        .Columns.Remove(.Columns.Item(nLoopColGridView))
                    'End Select

                    Select Case .Columns(nLoopColGridView).FieldName.ToString.ToUpper
                        Case "FTMatColorName".ToString.ToUpper, "FTMatColorCode".ToString.ToUpper, "FTNikePOLineItem".ToString.ToUpper, "FNHSysMatColorId".ToString.ToUpper, "FNTotal".ToString.ToUpper
                            'If pGridView.Name = "ogvDivertSrc" Or pGridView.Name = "ogvDivertBalance" Or pGridView.Name = "ogvDivertDT" Then
                            '    .Columns.Remove(.Columns.Item(nLoopColGridView))
                            'Else
                            'End If

                        Case Else
                            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    End Select

                Next

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

        Return pGridView

    End Function

    Private Function PROC_GETbInitGridview() As Boolean
        Dim bRet As Boolean = False
        Try

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    Private Function PROC_GETbInitSubOrderNoSrcDivert() As Boolean
        Dim bRet As Boolean = False
        Try
            sSQL = ""
            sSQL = "SELECT A.FTOrderNo, A.FTSubOrderNo"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & Me._FTOrderNoSrc & "' AND A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
            sSQL &= Environment.NewLine & "ORDER BY A.FTSubOrderNo ASC;"

            Dim tmpSubOrderNoSrcDivert As System.Data.DataTable

            tmpSubOrderNoSrcDivert = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.FTSubOrderNoSrc.Properties.Items.Clear()

            If Not DBNull.Value.Equals(tmpSubOrderNoSrcDivert) AndAlso tmpSubOrderNoSrcDivert.Rows.Count > 0 Then
                For Each oDataRow As System.Data.DataRow In tmpSubOrderNoSrcDivert.Rows
                    Me.FTSubOrderNoSrc.Properties.Items.Add(oDataRow!FTSubOrderNo.ToString)
                Next

                If Not tmpSubOrderNoSrcDivert Is Nothing Then tmpSubOrderNoSrcDivert.Dispose()

                bRet = True

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function

    'Private Function PROC_GETbShowBrowseDataBreakdownSrcDivert(ByVal paramFTSubOrderNo As String) As Boolean
    '    Dim bRet As Boolean = False
    '    Try
    '        sSQL = ""

    '    Catch ex As Exception
    '        If System.Diagnostics.Debugger.IsAttached = True Then
    '            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
    '        End If
    '    End Try

    '    Return bRet

    'End Function

    Private Function PROC_GETbShowBrowseDataFactorySubOrderNoInfo(ByVal paramFTOrderNo As String, ByVal paramFTSubOrderNo As String) As Boolean
        Dim bRet As Boolean = False
        Dim tmpDTSubOrderNoInfo As System.Data.DataTable
        Try
            sSQL = ""
            sSQL = "SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo,"
            sSQL &= Environment.NewLine & "             A.FDSubOrderDate, A.FDProDate, A.FDShipDate,"
            sSQL &= Environment.NewLine & " 			(SELECT TOP 1 L1.FTContinentCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS L1 (NOLOCK) WHERE L1.FNHSysContinentId = A.FNHSysContinentId) AS FNHSysContinentId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L2.FTCountryCode  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS L2 (NOLOCK) WHERE L2.FNHSysCountryId = A.FNHSysCountryId) AS FNHSysCountryId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L3.FTProvinceCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS L3 (NOLOCK) WHERE L3.FNHSysProvinceId = A.FNHSysProvinceId) AS FNHSysProvinceId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L4.FTShipModeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipMode AS L4 (NOLOCK) WHERE L4.FNHSysShipModeId = A.FNHSysShipModeId) AS FNHSysShipModeId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L5.FTShipPortCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipPort AS L5 (NOLOCK) WHERE L5.FNHSysShipPortId = A.FNHSysShipPortId) AS FNHSysShipPortId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L6.FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS L6 (NOLOCK) WHERE L6.FNHSysCurId = A.FNHSysCurId) AS FNHSysCurId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L7.FTGenderCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMGender AS L7 (NOLOCK) WHERE L7.FNHSysGenderId = A.FNHSysGenderId)AS FNHSysGenderId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L8.FTUnitCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnit AS L8 (NOLOCK) WHERE L8.FNHSysUnitId = A.FNHSysUnitId)AS FNHSysUnitId,"

            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L7.FTPlantCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS L7 (NOLOCK) WHERE L7.FNHSysPlantId = A.FNHSysPlantId)AS FNHSysPlantId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L8.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS L8 (NOLOCK) WHERE L8.FNHSysBuyGrpId = A.FNHSysBuyGrpId)AS FNHSysBuyGrpId,"

            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateEmb, '0') AS FTStateEmb,"
            sSQL &= Environment.NewLine & " 			ISNULL(A.FTStatePrint, '0') AS FTStatePrint,"
            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateHeat, '0') AS FTStateHeat,"
            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateLaser, '0') AS FTStateLaser,"
            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateWindows, 0) AS FTStateWindows"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "';"

            tmpDTSubOrderNoInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not DBNull.Value.Equals(tmpDTSubOrderNoInfo) AndAlso tmpDTSubOrderNoInfo.Rows.Count > 0 Then
                'For Each oDataRowSubOrderNoInfo As System.Data.DataRow In tmpDTSubOrderNoInfo.Rows
                '    'sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateEmb, '0') AS FTStateEmb,"
                '    'sSQL &= Environment.NewLine & " 			ISNULL(A.FTStatePrint, '0') AS FTStatePrint,"
                '    'sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateHeat, '0') AS FTStateHeat,"
                '    'sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateLaser, '0') AS FTStateLaser,"
                '    'sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateWindows, 0) AS FTStateWindows"

                '    Try
                '        Me.FDSubOrderDate.DateTime = HI.UL.ULDate.ConvertEnDB(oDataRowSubOrderNoInfo!FDSubOrderDate.ToString)
                '    Catch ex As Exception
                '        Me.FDSubOrderDate.Text = ""
                '    End Try

                '    Try
                '        Me.FDProDate.DateTime = HI.UL.ULDate.ConvertEnDB(oDataRowSubOrderNoInfo!FDProDate.ToString)
                '    Catch ex As Exception
                '        Me.FDProDate.Text = ""
                '    End Try

                '    Try
                '        Me.FDShipDate.DateTime = HI.UL.ULDate.ConvertEnDB(oDataRowSubOrderNoInfo!FDShipDate.ToString)
                '    Catch ex As Exception
                '        Me.FDShipDate.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysContinentId.Text = Val(oDataRowSubOrderNoInfo!FNHSysContinentId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysContinentId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysCountryId.Text = Val(oDataRowSubOrderNoInfo!FNHSysCountryId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysCountryId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysProvinceId.Text = Val(oDataRowSubOrderNoInfo!FNHSysProvinceId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysProvinceId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysShipModeId.Text = Val(oDataRowSubOrderNoInfo!FNHSysShipModeId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysShipModeId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysShipPortId.Text = Val(oDataRowSubOrderNoInfo!FNHSysShipPortId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysShipPortId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysCurId.Text = Val(oDataRowSubOrderNoInfo!FNHSysCurId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysCurId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysGenderId.Text = Val(oDataRowSubOrderNoInfo!FNHSysGenderId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysGenderId.Text = ""
                '    End Try

                '    Try
                '        Me.FNHSysUnitId.Text = Val(oDataRowSubOrderNoInfo!FNHSysUnitId.ToString)
                '    Catch ex As Exception
                '        Me.FNHSysUnitId.Text = ""
                '    End Try

                '    Exit For

                'Next

                Dim tTextFieldName As String = ""

                For Each oDataRowSubOrderNoInfo As DataRow In tmpDTSubOrderNoInfo.Rows

                    For Each Col As DataColumn In tmpDTSubOrderNoInfo.Columns

                        tTextFieldName = Col.ColumnName.ToString

                        For Each Obj As Object In Me.Controls.Find(tTextFieldName, True)

                            Select Case Obj.GetType.FullName.ToString.ToUpper

                                Case "DevExpress.XtraEditors.ButtonEdit".ToUpper

                                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                        If tTextFieldName.ToUpper <> Me.FTSubOrderNoSrc.Name.ToString.ToUpper Then
                                            .Text = oDataRowSubOrderNoInfo.Item(Col).ToString
                                        End If

                                    End With

                                Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                                    With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                        .Value = Val(oDataRowSubOrderNoInfo.Item(Col).ToString)
                                    End With

                                Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                                    With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)

                                        If tTextFieldName.ToUpper <> Me.FTSubOrderNoSrc.Focus.ToString.ToUpper Then
                                            Try
                                                .SelectedIndex = Val(oDataRowSubOrderNoInfo.Item(Col).ToString)
                                            Catch ex As Exception
                                                .SelectedIndex = -1
                                            End Try
                                        End If

                                    End With

                                Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                                    With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                        .EditValue = (Integer.Parse(Val(oDataRowSubOrderNoInfo.Item(Col).ToString))).ToString
                                    End With
                                Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                                    If tTextFieldName.ToString = "FDUpdDate_OrderSub" Then
                                        Obj.Text = HI.UL.ULDate.ConvertEN(oDataRowSubOrderNoInfo.Item(Col))
                                    Else
                                        Obj.Text = oDataRowSubOrderNoInfo.Item(Col).ToString
                                    End If

                                Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                                    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                        Try
                                            .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & oDataRowSubOrderNoInfo.Item(Col).ToString)
                                        Catch ex As Exception
                                            .Image = Nothing
                                        End Try
                                    End With

                                Case "DevExpress.XtraEditors.DateEdit".ToUpper
                                    Try
                                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                            If .Properties.DisplayFormat.FormatString = "d" Then
                                                .DateTime = CDate(oDataRowSubOrderNoInfo.Item(Col).ToString)
                                            Else
                                                .Text = HI.UL.ULDate.ConvertEN(oDataRowSubOrderNoInfo.Item(Col).ToString)
                                            End If
                                        End With
                                    Catch ex As Exception
                                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                            .Text = ""
                                        End With
                                    End Try

                                Case Else
                                    Obj.Text = oDataRowSubOrderNoInfo.Item(Col).ToString
                            End Select

                        Next

                    Next

                    Exit For

                Next

            Else
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        If Not tmpDTSubOrderNoInfo Is Nothing Then tmpDTSubOrderNoInfo.Dispose()

        Return bRet

    End Function

  
    Private Function PROC_GETbShowBrowseDataMatrixBreakdownSrcDivert(ByVal paramFTSubOrderNo As String) As Boolean
        Dim bRet As Boolean = False
        Dim oStrBuilder As New System.Text.StringBuilder
        Dim tmpDTSrcBreakdwonDivert As System.Data.DataTable
        Dim tmpDTBalanceBreakdownDivert As System.Data.DataTable
        Dim tmpDTColorwaySizeBreakdownSrcDivert As System.Data.DataTable
        Try
            '...clear source balance breakdown
            Me.ogdDivertSrc.DataSource = Nothing
            Me.ogdDivertSrc.Refresh()
            Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertSrc)
            Me.ogvDivertSrc.OptionsView.ColumnAutoWidth = False

            '...clear view balance breakdown
            Me.ogdDivertBalance.DataSource = Nothing
            Me.ogdDivertBalance.Refresh()
            Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertBalance)
            Me.ogvDivertBalance.OptionsView.ColumnAutoWidth = False

            sSQL = ""
            sSQL = "SELECT A.FNHSysMatSizeId, A.FTMatSizeCode, A.FTMatSizeNameEN AS FTMatSizeName"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE EXISTS (SELECT 'T'"
            sSQL &= Environment.NewLine & "              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 (NOLOCK)"
            sSQL &= Environment.NewLine & "              WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            sSQL &= Environment.NewLine & "                    AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'"
            sSQL &= Environment.NewLine & "                    AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)"
            sSQL &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq ASC;"

            tmpDTColorwaySizeBreakdownSrcDivert = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MASTER)

            If Not DBNull.Value.Equals(tmpDTColorwaySizeBreakdownSrcDivert) AndAlso tmpDTColorwaySizeBreakdownSrcDivert.Rows.Count > 0 Then

                '...initital source data structure
                oGridViewBreakdownDivertSrc = Me.ogdDivertSrc.Views(0)
                Call PROC_GETbRemoveGridViewColumn(oGridViewBreakdownDivertSrc)

                Me.ogdDivertSrc.DataSource = Nothing

                '...initial balance data structure
                oGridViewBreakdownDivertBalance = Me.ogdDivertBalance.Views(0)
                Call PROC_GETbRemoveGridViewColumn(oGridViewBreakdownDivertBalance)

                Me.ogdDivertBalance.DataSource = Nothing

                '...source
                tmpDTSrcBreakdwonDivert = New System.Data.DataTable()

                '...balance
                tmpDTBalanceBreakdownDivert = New System.Data.DataTable()

                tmpDTSrcBreakdwonDivert.Columns.Add("FNHSysMatColorId", GetType(Integer))
                tmpDTSrcBreakdwonDivert.Columns.Add("FTMatColorCode", GetType(String))
                tmpDTSrcBreakdwonDivert.Columns.Add("FTMatColorName", GetType(String))
                tmpDTSrcBreakdwonDivert.Columns.Add("FTDescription", GetType(String))
                tmpDTSrcBreakdwonDivert.Columns.Add("FTNikePOLineItem", GetType(String))
                tmpDTSrcBreakdwonDivert.Columns.Add("FNTotal", GetType(Integer))

                tmpDTBalanceBreakdownDivert.Columns.Add("FNHSysMatColorId", GetType(Integer))
                tmpDTBalanceBreakdownDivert.Columns.Add("FTMatColorCode", GetType(String))
                tmpDTBalanceBreakdownDivert.Columns.Add("FTMatColorName", GetType(String))
                tmpDTBalanceBreakdownDivert.Columns.Add("FTDescription", GetType(String))
                tmpDTBalanceBreakdownDivert.Columns.Add("FTNikePOLineItem", GetType(String))
                tmpDTBalanceBreakdownDivert.Columns.Add("FNTotal", GetType(Integer))
                'Dim oColFNHSysMatColorId As System.Data.DataColumn
                'oColFNHSysMatColorId = New System.Data.DataColumn("FNHSysMatColorId", System.Type.GetType("System.Int32"))

                'With oColFNHSysMatColorId
                '    .Caption = "FNHSysMatColorId"

                '    '...source
                '    oGridViewBreakdownDivertSrc.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Name = oColFNHSysMatColorId.ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Visible = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTSrcBreakdwonDivert.Columns.Add(.ColumnName, .DataType)

                '    '...balance
                '    oGridViewBreakdownDivertBalance.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Name = oColFNHSysMatColorId.ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Visible = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTBalanceBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                'Dim oColFTMatColorCode As System.Data.DataColumn
                'oColFTMatColorCode = New System.Data.DataColumn("FTMatColorCode", System.Type.GetType("System.String"))
               

                'With oColFTMatColorCode
                '    Select Case HI.ST.Lang.Language
                '        Case HI.ST.Lang.eLang.EN
                '            .Caption = "Colorway"
                '        Case HI.ST.Lang.eLang.TH
                '            .Caption = "รหัสสี"
                '        Case HI.ST.Lang.eLang.KM
                '            .Caption = "Colorway"
                '        Case HI.ST.Lang.eLang.VT
                '            .Caption = "Colorway"
                '    End Select

                '    '...source
                '    oGridViewBreakdownDivertSrc.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Visible = True
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).VisibleIndex = 0
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTSrcBreakdwonDivert.Columns.Add(.ColumnName, .DataType)

                '    '...balance
                '    oGridViewBreakdownDivertBalance.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Visible = True
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).VisibleIndex = 0
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTBalanceBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                'Dim oColFTMatColorName As System.Data.DataColumn
                'oColFTMatColorName = New System.Data.DataColumn("FTMatColorName", System.Type.GetType("System.String"))

             

                'With oColFTMatColorName
                '    .Caption = "FTMatColorName"
                '    '...source
                '    oGridViewBreakdownDivertSrc.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Visible = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTSrcBreakdwonDivert.Columns.Add(.ColumnName, .DataType)

                '    '...balance
                '    oGridViewBreakdownDivertBalance.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Visible = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTBalanceBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                'Dim oColFTDescription As System.Data.DataColumn
                'oColFTDescription = New System.Data.DataColumn("FTDescription", System.Type.GetType("System.String"))

                'With oColFTDescription
                '    Select Case HI.ST.Lang.Language
                '        Case HI.ST.Lang.eLang.EN
                '            .Caption = "Color way / Size Breakdown"
                '        Case HI.ST.Lang.eLang.TH
                '            .Caption = "สี / ไซส์"
                '        Case Else
                '            .Caption = "Color way / Size Breakdown"
                '    End Select

                '    '...source
                '    oGridViewBreakdownDivertSrc.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Visible = True
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).VisibleIndex = 1
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSize = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSort = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSize = False

                '    tmpDTSrcBreakdwonDivert.Columns.Add(.ColumnName, .DataType)

                '    '...balance
                '    oGridViewBreakdownDivertBalance.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Visible = True
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).VisibleIndex = 1
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSize = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSort = False
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSize = False

                '    tmpDTBalanceBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With
             
                For Each oRow As DataRow In tmpDTColorwaySizeBreakdownSrcDivert.Rows

                    '...source
                    With oGridViewBreakdownDivertSrc
                        Dim oColAppendSizeId As System.Data.DataColumn = New System.Data.DataColumn("FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
                        oColAppendSizeId.Caption = "FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeId.ColumnName)
                        .Columns(oColAppendSizeId.ColumnName).FieldName = oColAppendSizeId.ColumnName
                        .Columns(oColAppendSizeId.ColumnName).Name = oColAppendSizeId.ColumnName
                        .Columns(oColAppendSizeId.ColumnName).Caption = oColAppendSizeId.Caption
                        .Columns(oColAppendSizeId.ColumnName).Visible = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendSizeId.ColumnName, oColAppendSizeId.DataType)

                        Dim oColAppendSizeCode As System.Data.DataColumn = New System.Data.DataColumn("FTMatSizeCode" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                        oColAppendSizeCode.Caption = oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeCode.ColumnName)
                        .Columns(oColAppendSizeCode.ColumnName).FieldName = oColAppendSizeCode.ColumnName
                        .Columns(oColAppendSizeCode.ColumnName).Name = oColAppendSizeCode.ColumnName
                        .Columns(oColAppendSizeCode.ColumnName).Caption = oColAppendSizeCode.Caption
                        .Columns(oColAppendSizeCode.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                        .Columns(oColAppendSizeCode.ColumnName).Visible = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendSizeCode.ColumnName, oColAppendSizeCode.DataType)

                        Dim oColAppendSizeName As System.Data.DataColumn = New System.Data.DataColumn("FTMatSizeName" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                        oColAppendSizeName.Caption = "FTMatSizeName" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeName.ColumnName)
                        .Columns(oColAppendSizeName.ColumnName).FieldName = oColAppendSizeName.ColumnName
                        .Columns(oColAppendSizeName.ColumnName).Name = oColAppendSizeName.ColumnName
                        .Columns(oColAppendSizeName.ColumnName).Caption = oColAppendSizeName.Caption
                        .Columns(oColAppendSizeName.ColumnName).Visible = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendSizeName.ColumnName, oColAppendSizeName.DataType)

                        '...keep value : FNGrandQuantity (จำนวนรวมของ Quantity + Extra Quantity)
                        Dim oColAppendAmntZZZ As System.Data.DataColumn = New System.Data.DataColumn("FNAmnt" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendAmntZZZ.Caption = oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendAmntZZZ.ColumnName)
                        .Columns(oColAppendAmntZZZ.ColumnName).FieldName = oColAppendAmntZZZ.ColumnName
                        .Columns(oColAppendAmntZZZ.ColumnName).Name = oColAppendAmntZZZ.ColumnName
                        .Columns(oColAppendAmntZZZ.ColumnName).Caption = oColAppendAmntZZZ.Caption
                        .Columns(oColAppendAmntZZZ.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Columns(oColAppendAmntZZZ.ColumnName).Visible = True

                        .Columns(oColAppendAmntZZZ.ColumnName).Fixed = FixedStyle.None
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowSize = False

                        .Columns(oColAppendAmntZZZ.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowSort = False

                        .Columns(oColAppendAmntZZZ.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(oColAppendAmntZZZ.ColumnName).DisplayFormat.FormatString = "{0:N0}"

                        .Columns(oColAppendAmntZZZ.ColumnName).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                        .Columns(oColAppendAmntZZZ.ColumnName).SummaryItem.DisplayFormat = "{0:n0}"

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendAmntZZZ.ColumnName, oColAppendAmntZZZ.DataType)

                        Dim oColAppendFNQuantity As System.Data.DataColumn = New System.Data.DataColumn("FNQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantity.Caption = "FNQuantity" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantity.ColumnName)
                        .Columns(oColAppendFNQuantity.ColumnName).FieldName = oColAppendFNQuantity.ColumnName
                        .Columns(oColAppendFNQuantity.ColumnName).Name = oColAppendFNQuantity.ColumnName
                        .Columns(oColAppendFNQuantity.ColumnName).Caption = oColAppendFNQuantity.Caption
                        .Columns(oColAppendFNQuantity.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNQuantity.ColumnName, oColAppendFNQuantity.DataType)

                        Dim oColAppendFNQuantityExtra As System.Data.DataColumn = New System.Data.DataColumn("FNQuantityExtra" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantityExtra.Caption = "FNQuantityExtra" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantityExtra.ColumnName)
                        .Columns(oColAppendFNQuantityExtra.ColumnName).FieldName = oColAppendFNQuantityExtra.ColumnName
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Name = oColAppendFNQuantityExtra.ColumnName
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Caption = oColAppendFNQuantityExtra.Caption
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNQuantityExtra.ColumnName, oColAppendFNQuantityExtra.DataType)

                        Dim oColAppendFNQuantityTest As System.Data.DataColumn = New System.Data.DataColumn("FNQuantityTest" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantityTest.Caption = "FNQuantityTest" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantityTest.ColumnName)
                        .Columns(oColAppendFNQuantityTest.ColumnName).FieldName = oColAppendFNQuantityTest.ColumnName
                        .Columns(oColAppendFNQuantityTest.ColumnName).Name = oColAppendFNQuantityTest.ColumnName
                        .Columns(oColAppendFNQuantityTest.ColumnName).Caption = oColAppendFNQuantityTest.Caption
                        .Columns(oColAppendFNQuantityTest.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowSort = DefaultBoolean.False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNQuantityTest.ColumnName, oColAppendFNQuantityTest.DataType)

                        Dim oColAppendFNGrandQuantity As System.Data.DataColumn = New System.Data.DataColumn("FNGrandQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNGrandQuantity.Caption = "FNGrandQuantity" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNGrandQuantity.ColumnName)
                        .Columns(oColAppendFNGrandQuantity.ColumnName).FieldName = oColAppendFNGrandQuantity.ColumnName
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Name = oColAppendFNGrandQuantity.ColumnName
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Caption = oColAppendFNGrandQuantity.Caption
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Visible = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNGrandQuantity.ColumnName, oColAppendFNGrandQuantity.DataType)

                        Dim oColAppendFNPrice As System.Data.DataColumn = New System.Data.DataColumn("FNPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNPrice.Caption = "FNPrice" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNPrice.ColumnName)
                        .Columns(oColAppendFNPrice.ColumnName).FieldName = oColAppendFNPrice.ColumnName
                        .Columns(oColAppendFNPrice.ColumnName).Name = oColAppendFNPrice.ColumnName
                        .Columns(oColAppendFNPrice.ColumnName).Caption = oColAppendFNPrice.Caption
                        .Columns(oColAppendFNPrice.ColumnName).Visible = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNPrice.ColumnName, oColAppendFNPrice.DataType)

                        '...Amount จาก จำนวนปริมาณที่สั่งซื้อจริง ของ รายการ Sub Order No. ตามสี ตามไซส์
                        Dim oColAppendFNValue As System.Data.DataColumn = New System.Data.DataColumn("FNValue" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNValue.Caption = "FNValue" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNValue.ColumnName)
                        .Columns(oColAppendFNValue.ColumnName).FieldName = oColAppendFNValue.ColumnName
                        .Columns(oColAppendFNValue.ColumnName).Name = oColAppendFNValue.ColumnName
                        .Columns(oColAppendFNValue.ColumnName).Caption = oColAppendFNValue.Caption
                        .Columns(oColAppendFNValue.ColumnName).Visible = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNValue.ColumnName, oColAppendFNValue.DataType)

                        Dim oColAppendFNExtraQtyPercent As System.Data.DataColumn = New System.Data.DataColumn("FNExtraQtyPercent" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNExtraQtyPercent.Caption = "FNExtraQtyPercent" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNExtraQtyPercent.ColumnName)
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).FieldName = oColAppendFNExtraQtyPercent.ColumnName
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Name = oColAppendFNExtraQtyPercent.ColumnName
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Caption = oColAppendFNExtraQtyPercent.Caption
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Visible = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTSrcBreakdwonDivert.Columns.Add(oColAppendFNExtraQtyPercent.ColumnName, oColAppendFNExtraQtyPercent.DataType)

                    End With

                    '...balance
                    With oGridViewBreakdownDivertBalance
                        Dim oColAppendSizeId As System.Data.DataColumn = New System.Data.DataColumn("FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
                        oColAppendSizeId.Caption = "FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeId.ColumnName)
                        .Columns(oColAppendSizeId.ColumnName).FieldName = oColAppendSizeId.ColumnName
                        .Columns(oColAppendSizeId.ColumnName).Name = oColAppendSizeId.ColumnName
                        .Columns(oColAppendSizeId.ColumnName).Caption = oColAppendSizeId.Caption
                        .Columns(oColAppendSizeId.ColumnName).Visible = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendSizeId.ColumnName, oColAppendSizeId.DataType)

                        Dim oColAppendSizeCode As System.Data.DataColumn = New System.Data.DataColumn("FTMatSizeCode" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                        oColAppendSizeCode.Caption = oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeCode.ColumnName)
                        .Columns(oColAppendSizeCode.ColumnName).FieldName = oColAppendSizeCode.ColumnName
                        .Columns(oColAppendSizeCode.ColumnName).Name = oColAppendSizeCode.ColumnName
                        .Columns(oColAppendSizeCode.ColumnName).Caption = oColAppendSizeCode.Caption
                        .Columns(oColAppendSizeCode.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                        .Columns(oColAppendSizeCode.ColumnName).Visible = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendSizeCode.ColumnName, oColAppendSizeCode.DataType)

                        Dim oColAppendSizeName As System.Data.DataColumn = New System.Data.DataColumn("FTMatSizeName" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                        oColAppendSizeName.Caption = "FTMatSizeName" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeName.ColumnName)
                        .Columns(oColAppendSizeName.ColumnName).FieldName = oColAppendSizeName.ColumnName
                        .Columns(oColAppendSizeName.ColumnName).Name = oColAppendSizeName.ColumnName
                        .Columns(oColAppendSizeName.ColumnName).Caption = oColAppendSizeName.Caption
                        .Columns(oColAppendSizeName.ColumnName).Visible = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendSizeName.ColumnName, oColAppendSizeName.DataType)

                        '...keep value : FNGrandQuantity (จำนวนรวมของ Quantity + Extra Quantity)
                        Dim oColAppendAmntZZZ As System.Data.DataColumn = New System.Data.DataColumn("FNAmnt" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendAmntZZZ.Caption = oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendAmntZZZ.ColumnName)
                        .Columns(oColAppendAmntZZZ.ColumnName).FieldName = oColAppendAmntZZZ.ColumnName
                        .Columns(oColAppendAmntZZZ.ColumnName).Name = oColAppendAmntZZZ.ColumnName
                        .Columns(oColAppendAmntZZZ.ColumnName).Caption = oColAppendAmntZZZ.Caption
                        .Columns(oColAppendAmntZZZ.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Columns(oColAppendAmntZZZ.ColumnName).Visible = True

                        .Columns(oColAppendAmntZZZ.ColumnName).Fixed = FixedStyle.None
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowSize = False

                        .Columns(oColAppendAmntZZZ.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowSort = False

                        .Columns(oColAppendAmntZZZ.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .Columns(oColAppendAmntZZZ.ColumnName).DisplayFormat.FormatString = "{0:N0}"

                        .Columns(oColAppendAmntZZZ.ColumnName).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                        .Columns(oColAppendAmntZZZ.ColumnName).SummaryItem.DisplayFormat = "{0:n0}"

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendAmntZZZ.ColumnName, oColAppendAmntZZZ.DataType)

                        Dim oColAppendFNQuantity As System.Data.DataColumn = New System.Data.DataColumn("FNQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantity.Caption = "FNQuantity" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantity.ColumnName)
                        .Columns(oColAppendFNQuantity.ColumnName).FieldName = oColAppendFNQuantity.ColumnName
                        .Columns(oColAppendFNQuantity.ColumnName).Name = oColAppendFNQuantity.ColumnName
                        .Columns(oColAppendFNQuantity.ColumnName).Caption = oColAppendFNQuantity.Caption
                        .Columns(oColAppendFNQuantity.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNQuantity.ColumnName, oColAppendFNQuantity.DataType)

                        Dim oColAppendFNQuantityExtra As System.Data.DataColumn = New System.Data.DataColumn("FNQuantityExtra" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantityExtra.Caption = "FNQuantityExtra" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantityExtra.ColumnName)
                        .Columns(oColAppendFNQuantityExtra.ColumnName).FieldName = oColAppendFNQuantityExtra.ColumnName
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Name = oColAppendFNQuantityExtra.ColumnName
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Caption = oColAppendFNQuantityExtra.Caption
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNQuantityExtra.ColumnName, oColAppendFNQuantityExtra.DataType)

                        Dim oColAppendFNQuantityTest As System.Data.DataColumn = New System.Data.DataColumn("FNQuantityTest" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantityTest.Caption = "FNQuantityTest" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantityTest.ColumnName)
                        .Columns(oColAppendFNQuantityTest.ColumnName).FieldName = oColAppendFNQuantityTest.ColumnName
                        .Columns(oColAppendFNQuantityTest.ColumnName).Name = oColAppendFNQuantityTest.ColumnName
                        .Columns(oColAppendFNQuantityTest.ColumnName).Caption = oColAppendFNQuantityTest.Caption
                        .Columns(oColAppendFNQuantityTest.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowSort = DefaultBoolean.False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNQuantityTest.ColumnName, oColAppendFNQuantityTest.DataType)

                        Dim oColAppendFNGrandQuantity As System.Data.DataColumn = New System.Data.DataColumn("FNGrandQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNGrandQuantity.Caption = "FNGrandQuantity" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNGrandQuantity.ColumnName)
                        .Columns(oColAppendFNGrandQuantity.ColumnName).FieldName = oColAppendFNGrandQuantity.ColumnName
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Name = oColAppendFNGrandQuantity.ColumnName
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Caption = oColAppendFNGrandQuantity.Caption
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Visible = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNGrandQuantity.ColumnName, oColAppendFNGrandQuantity.DataType)

                        Dim oColAppendFNPrice As System.Data.DataColumn = New System.Data.DataColumn("FNPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNPrice.Caption = "FNPrice" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNPrice.ColumnName)
                        .Columns(oColAppendFNPrice.ColumnName).FieldName = oColAppendFNPrice.ColumnName
                        .Columns(oColAppendFNPrice.ColumnName).Name = oColAppendFNPrice.ColumnName
                        .Columns(oColAppendFNPrice.ColumnName).Caption = oColAppendFNPrice.Caption
                        .Columns(oColAppendFNPrice.ColumnName).Visible = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNPrice.ColumnName, oColAppendFNPrice.DataType)

                        '...Amount จาก จำนวนปริมาณที่สั่งซื้อจริง ของ รายการ Sub Order No. ตามสี ตามไซส์
                        Dim oColAppendFNValue As System.Data.DataColumn = New System.Data.DataColumn("FNValue" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNValue.Caption = "FNValue" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNValue.ColumnName)
                        .Columns(oColAppendFNValue.ColumnName).FieldName = oColAppendFNValue.ColumnName
                        .Columns(oColAppendFNValue.ColumnName).Name = oColAppendFNValue.ColumnName
                        .Columns(oColAppendFNValue.ColumnName).Caption = oColAppendFNValue.Caption
                        .Columns(oColAppendFNValue.ColumnName).Visible = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNValue.ColumnName, oColAppendFNValue.DataType)

                        Dim oColAppendFNExtraQtyPercent As System.Data.DataColumn = New System.Data.DataColumn("FNExtraQtyPercent" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNExtraQtyPercent.Caption = "FNExtraQtyPercent" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNExtraQtyPercent.ColumnName)
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).FieldName = oColAppendFNExtraQtyPercent.ColumnName
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Name = oColAppendFNExtraQtyPercent.ColumnName
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Caption = oColAppendFNExtraQtyPercent.Caption
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Visible = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTBalanceBreakdownDivert.Columns.Add(oColAppendFNExtraQtyPercent.ColumnName, oColAppendFNExtraQtyPercent.DataType)

                    End With

                Next

                Dim oColAppendFNRowTotal As System.Data.DataColumn = New System.Data.DataColumn("FNRowTotal", System.Type.GetType("System.Double"))

                With oColAppendFNRowTotal
                    .Caption = "T/T"

                    '...source
                    oGridViewBreakdownDivertSrc.Columns.AddField(.ColumnName)
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).FieldName = .ColumnName
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Name = .ColumnName
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Caption = .Caption
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Visible = False

                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Fixed = FixedStyle.Right
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSize = False

                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSort = False
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    oGridViewBreakdownDivertSrc.Columns(.ColumnName).DisplayFormat.FormatString = "{0:N0}"

                    tmpDTSrcBreakdwonDivert.Columns.Add(.ColumnName, .DataType)

                    '...balance
                    oGridViewBreakdownDivertBalance.Columns.AddField(.ColumnName)
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).FieldName = .ColumnName
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Name = .ColumnName
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Caption = .Caption
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Visible = False

                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).Fixed = FixedStyle.Right
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSize = False

                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).OptionsColumn.AllowSort = False
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    oGridViewBreakdownDivertBalance.Columns(.ColumnName).DisplayFormat.FormatString = "{0:N0}"

                    tmpDTBalanceBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                End With

                '...iterate loop binding row datatable datarow to oDBdtSizeBreakdownMatrix
                oStrBuilder.Remove(0, oStrBuilder.Length)

                If HI.ST.Lang.Language = HI.ST.Lang.eLang.EN Then
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameEN AS FTMatColorName")
                ElseIf HI.ST.Lang.Language = HI.ST.Lang.eLang.TH Then
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameTH AS FTMatColorName")
                Else
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameEN AS FTMatColorName")
                End If
                oStrBuilder.AppendLine("      ,B.FTNikePOLineItem")
                oStrBuilder.AppendLine("      ,D.FNHSysMatSizeId, D.FTMatSizeCode, D.FTMatSizeNameEN AS FTMatSizeName")
                oStrBuilder.AppendLine("      ,B.FNQuantity, B.FNPrice, B.FNAmt AS FNAmnt, B.FNExtraQty AS FNExtraQtyPercent")
                oStrBuilder.AppendLine("      ,B.FNQuantityExtra, B.FNGarmentQtyTest")
                REM 2014/12/25 oStrBuilder.AppendLine("      ,(ISNULL(B.FNQuantity,0) + ISNULL(B.FNQuantityExtra,0) + ISNULL(B.FNGarmentQtyTest,0)) AS FNGrandQuantity")
                oStrBuilder.AppendLine("      ,ISNULL(B.FNQuantity,0) AS FNGrandQuantity")
                oStrBuilder.AppendLine("      ,(ISNULL(B.FNAmt,0) + ISNULL(B.FNQuantityExtra * B.FNPrice ,0) + ISNULL(B.FNGarmentQtyTest*B.FNPrice,0)) AS FNGrandAmnt ")
                oStrBuilder.AppendLine("     , (C.FTMatColorNameEN + '/' + C.FTMatColorNameTH) AS FTColorExtension")
                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS B WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FNHSysMatColorId = C.FNHSysMatColorId")
                oStrBuilder.AppendLine("     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS D WITH(NOLOCK) ON B.FNHSysMatSizeId = D.FNHSysMatSizeId")
                oStrBuilder.AppendLine("WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
                oStrBuilder.AppendLine("      AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'")
                oStrBuilder.AppendLine("ORDER BY C.FNMatColorSeq ASC, D.FNMatSizeSeq ASC;")

                sSQL = ""
                sSQL = oStrBuilder.ToString()

                Dim tmpDTSrcBreakdwonDivertInfo As System.Data.DataTable

                tmpDTSrcBreakdwonDivertInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If tmpDTSrcBreakdwonDivertInfo.Rows.Count > 0 Then

                    Dim FNHSysMatColorIdPrv As Integer
                    Dim FNHSysMatColorIdCurr As Integer

                    Dim nLoopBreakdownSeq As Integer = 0
                    Dim FNHSysMatSizeIdPrv As Integer
                    Dim FNHSysMatSizeIdCurr As Integer

                    Dim nFirstRowColorway As Integer

                    FNHSysMatColorIdPrv = -1
                    FNHSysMatColorIdCurr = tmpDTSrcBreakdwonDivertInfo.Rows(0).Item("FNHSysMatColorId")

                    FNHSysMatSizeIdPrv = -1
                    FNHSysMatSizeIdCurr = tmpDTSrcBreakdwonDivertInfo.Rows(0).Item("FNHSysMatSizeId")

                    For nLoopBreakdownInfo As Integer = 0 To tmpDTSrcBreakdwonDivertInfo.Rows.Count - 1

                        If FNHSysMatColorIdPrv <> tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId") Then

                            Dim oRowItem As DataRow

                            '...กรณีเป็นรายการไซส์แรกของแต่ละสี
                            oRowItem = tmpDTSrcBreakdwonDivert.NewRow()

                            With oRowItem
                                .Item("FNHSysMatColorId") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId")
                                .Item("FTMatColorCode") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatColorCode")
                                .Item("FTMatColorName") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatColorName")
                                .Item("FTNikePOLineItem") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTNikePOLineItem")
                                .Item("FTDescription") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTColorExtension")
                                .Item("FNAmnt" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                                .Item("FNRowTotal") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                                .Item("FNHSysMatSizeId" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")
                                .Item("FTMatSizeCode" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")
                                .Item("FTMatSizeName" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeName")
                                .Item("FNQuantity" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantity")
                                .Item("FNQuantityExtra" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantityExtra")
                                .Item("FNQuantityTest" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGarmentQtyTest")
                                .Item("FNGrandQuantity" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                                .Item("FNPrice" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNPrice")
                                .Item("FNValue" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNAmnt")
                                .Item("FNExtraQtyPercent" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNExtraQtyPercent")
                            End With

                            '...source
                            tmpDTSrcBreakdwonDivert.Rows.Add(oRowItem)

                            '...balance
                            'tmpDTBalanceBreakdownDivert.Rows.Add(oRowItem)

                            FNHSysMatColorIdPrv = FNHSysMatColorIdCurr
                            '...binding first row each of colorway/size breakdown
                            nFirstRowColorway = tmpDTSrcBreakdwonDivert.Rows.Count - 1

                        Else
                            '...สีเดียวกัน/ไซส์ต่างกัน
                            FNHSysMatColorIdCurr = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId")
                            '...Iterate loop all column size breakdown / repeat all size breakdown on colorway
                            nLoopBreakdownSeq = nFirstRowColorway

                            '...source
                            Dim nFNAmnt As Double

                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNHSysMatSizeId" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FTMatSizeCode" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FTMatSizeName" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeName")

                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNAmnt" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNQuantity" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantity")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNQuantityExtra" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantityExtra")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNQuantityTest" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGarmentQtyTest")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNGrandQuantity" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNPrice" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNPrice")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNValue" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNAmnt")
                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNExtraQtyPercent" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNExtraQtyPercent")

                            nFNAmnt = Val(tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNAmnt" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()).ToString()) + Val(tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNRowTotal").ToString())

                            tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNRowTotal") = nFNAmnt

                            tmpDTSrcBreakdwonDivert.AcceptChanges()

                            '...balance
                            'Dim nFNAmntAfterDivert As Double

                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNHSysMatSizeId" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FTMatSizeCode" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FTMatSizeName" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeName")

                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNAmnt" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNQuantity" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantity")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNQuantityExtra" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantityExtra")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNQuantityTest" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGarmentQtyTest")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNGrandQuantity" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNPrice" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNPrice")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNValue" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNAmnt")
                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNExtraQtyPercent" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNExtraQtyPercent")

                            'nFNAmntAfterDivert = Val(tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNAmnt" & tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()).ToString()) + Val(tmpDTSrcBreakdwonDivert.Rows(nLoopBreakdownSeq).Item("FNRowTotal").ToString())

                            'tmpDTBalanceBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNRowTotal") = nFNAmntAfterDivert

                            'tmpDTBalanceBreakdownDivert.AcceptChanges()

                        End If

                        FNHSysMatColorIdPrv = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId")
                        FNHSysMatSizeIdPrv = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")

                    Next nLoopBreakdownInfo

                    '...source
                    tmpDTSrcBreakdwonDivert.AcceptChanges()

                    '...balance
                    'tmpDTBalanceBreakdownDivert.AcceptChanges()

                End If

                'tmpDTBalanceBreakdownDivert = tmpDTSrcBreakdwonDivert.Clone()

                If Not DBNull.Value.Equals(tmpDTSrcBreakdwonDivert) AndAlso tmpDTSrcBreakdwonDivert.Rows.Count > 0 Then
                    Dim numLoopBreakdown As Integer
                    For numLoopBreakdown = 0 To tmpDTSrcBreakdwonDivert.Rows.Count - 1
                        tmpDTBalanceBreakdownDivert.ImportRow(tmpDTSrcBreakdwonDivert.Rows(numLoopBreakdown))
                    Next
                Else
                    tmpDTBalanceBreakdownDivert = Nothing
                End If

                If Not tmpDTSrcBreakdwonDivertInfo Is Nothing Then tmpDTSrcBreakdwonDivertInfo.Dispose()

                '...source
                oGridViewBreakdownDivertSrc.OptionsBehavior.AllowAddRows = False
                oGridViewBreakdownDivertSrc.OptionsCustomization.AllowSort = False

                oGridViewBreakdownDivertSrc.ActiveFilter.Clear()

                Dim _TotalQty As Integer = 0
                tmpDTSrcBreakdwonDivert.BeginInit()
                For Each Rx1 As DataRow In tmpDTSrcBreakdwonDivert.Rows

                    _TotalQty = 0

                    For Each Col As DataColumn In tmpDTSrcBreakdwonDivert.Columns
                        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNGrandQuantity".Length)
                            Case "FNGrandQuantity"
                                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                            Case Else

                        End Select
                    Next

                    Rx1!FNTotal = _TotalQty
                Next
                tmpDTSrcBreakdwonDivert.EndInit()

                tmpDTBalanceBreakdownDivert.BeginInit()
                For Each Rx1 As DataRow In tmpDTBalanceBreakdownDivert.Rows

                    _TotalQty = 0

                    For Each Col As DataColumn In tmpDTBalanceBreakdownDivert.Columns
                        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNGrandQuantity".Length)
                            Case "FNGrandQuantity"
                                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                            Case Else

                        End Select
                    Next

                    Rx1!FNTotal = _TotalQty
                Next
                tmpDTBalanceBreakdownDivert.EndInit()

                ogdDivertSrc.DataSource = tmpDTSrcBreakdwonDivert
                ogdDivertSrc = oGridViewBreakdownDivertSrc.GridControl
                oGridViewBreakdownDivertSrc.OptionsView.ColumnAutoWidth = False
                ogdDivertSrc.Refresh()
                oGridViewBreakdownDivertSrc.RefreshData()

                '...balance
                oGridViewBreakdownDivertBalance.OptionsBehavior.AllowAddRows = False
                oGridViewBreakdownDivertBalance.OptionsCustomization.AllowSort = False

                oGridViewBreakdownDivertBalance.ActiveFilter.Clear()

                ogdDivertBalance.DataSource = tmpDTBalanceBreakdownDivert
                ogdDivertBalance = oGridViewBreakdownDivertBalance.GridControl
                oGridViewBreakdownDivertBalance.OptionsView.ColumnAutoWidth = False
                ogdDivertBalance.Refresh()
                oGridViewBreakdownDivertBalance.RefreshData()

                bRet = True

            Else
                '...clear source
                Me.ogdDivertSrc.DataSource = Nothing
                Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertSrc)
                Me.ogvDivertSrc.OptionsView.ColumnAutoWidth = False

                '...clear balance
                Me.ogdDivertBalance.DataSource = Nothing
                Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertBalance)
                Me.ogvDivertBalance.OptionsView.ColumnAutoWidth = False
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
            '...source
            Me.ogdDivertSrc.DataSource = Nothing
            Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertSrc)
            Me.ogvDivertSrc.OptionsView.ColumnAutoWidth = False
            '...clear balance
            Me.ogdDivertBalance.DataSource = Nothing
            Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertBalance)
            Me.ogvDivertBalance.OptionsView.ColumnAutoWidth = False
        End Try

        Return bRet

    End Function

    Private Function PROC_GETbValidateSubOrderNoInfo() As Boolean
        Dim bValidate As Boolean = False

        Try
            If Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString <> "" Then

                If Me.ogvDivertSrc.RowCount > 0 Then
                    If Me.FDSubOrderDate.Text <> "" Then
                        If Me.FDProDate.Text <> "" Then
                            If Me.FDShipDate.Text <> "" Then

                                If Me.FNHSysContinentId.Text <> "" Then
                                    If Me.FNHSysCountryId.Text <> "" Then

                                        If Me.FNHSysShipModeId.Text <> "" Then
                                            If Me.FNHSysShipPortId.Text <> "" Then
                                                If Me.FNHSysCurId.Text <> "" Then
                                                    If Me.FNHSysGenderId.Text <> "" Then
                                                        If Me.FNHSysUnitId.Text <> "" Then

                                                            If Me.FNHSysPlantId.Text <> "" Then
                                                                If Me.FNHSysBuyGrpId.Text <> "" Then
                                                                    '...validate breakdown confirm divert xxx
                                                                    '==============================================================================================================================================================================================
                                                                    Dim numSumQuantityDivert As Integer

                                                                    numSumQuantityDivert = 0

                                                                    For Each oDataRow As System.Data.DataRow In CType(Me.ogdDivertDT.DataSource, System.Data.DataTable).Rows

                                                                        For Each oColGridViewDT As DevExpress.XtraGrid.Columns.GridColumn In Me.ogvDivertDT.Columns

                                                                            Dim tTextMatSizeTag As String = ""

                                                                            Select Case Microsoft.VisualBasic.Mid(oColGridViewDT.Name.ToUpper, 1, 13)

                                                                                Case "FTMatSizeCode".ToUpper

                                                                                    If Not DBNull.Value.Equals(oColGridViewDT.Tag) Then
                                                                                        tTextMatSizeTag = oColGridViewDT.Tag.ToString

                                                                                        Try
                                                                                            If Not DBNull.Value.Equals(oDataRow.Item("FNAmnt" & tTextMatSizeTag)) Then
                                                                                                numSumQuantityDivert = numSumQuantityDivert + Val(oDataRow.Item("FNAmnt" & tTextMatSizeTag).ToString)
                                                                                            End If

                                                                                        Catch ex As Exception
                                                                                            If System.Diagnostics.Debugger.IsAttached = True Then
                                                                                                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                                                                                            End If

                                                                                        End Try

                                                                                    End If

                                                                                Case Else
                                                                                    '...Nothing
                                                                            End Select

                                                                        Next

                                                                    Next

                                                                    If numSumQuantityDivert > 0 Then
                                                                        bValidate = True
                                                                    Else
                                                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, ogbTargetBreakdownDivert.Text, "divert factory sub order no. breakdown")
                                                                    End If

                                                                    '==============================================================================================================================================================================================

                                                                Else
                                                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                                                                    Me.FNHSysBuyGrpId.Focus()
                                                                End If
                                                            Else
                                                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                                                                Me.FNHSysPlantId.Focus()
                                                            End If


                                                        Else
                                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitId_lbl.Text)
                                                            Me.FNHSysUnitId.Focus()
                                                        End If
                                                    Else
                                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysGenderId_lbl.Text)
                                                        Me.FNHSysGenderId.Focus()
                                                    End If
                                                Else
                                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCurId_lbl.Text)
                                                    Me.FNHSysCurId.Focus()
                                                End If
                                            Else
                                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysShipPortId_lbl.Text)
                                                Me.FNHSysShipPortId.Focus()
                                            End If
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysShipModeId_lbl.Text)
                                            Me.FNHSysShipModeId.Focus()
                                        End If
                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCountryId_lbl.Text)
                                        Me.FNHSysCountryId.Focus()
                                    End If
                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysContinentId_lbl.Text)
                                    Me.FNHSysContinentId.Focus()
                                End If

                              
                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDShipDate_lbl.Text)
                                Me.FDShipDate.Focus()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDProDate_lbl.Text)
                            Me.FDProDate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDSubOrderDate_lbl.Text)
                        Me.FDSubOrderDate.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Factory Sub Order No. : " & Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString & Environment.NewLine & "not have exists breakdown")
                    Me.FTSubOrderNoSrc.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTSubOrderNoSrc_lbl.Text)
                Me.FTSubOrderNoSrc.Focus()
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bValidate

    End Function

    Private Function PROC_SAVEbDivertSubOrderNo(ByVal paramFTSubOrderNoSrc As String) As Boolean
        Dim oStrBuilder As New System.Text.StringBuilder
        Dim bRet As Boolean = False
        Dim _FTNewSubOrderNo As String = ""
        Dim _Qry As String = ""

        'Sub Order No ที่ทำการ Divert มานั้นจะต้องทำการ Copy รายการต่อไปนี้หรือไม่ ? : Component Info/ Sewing Info/ Packing Info/ Packing Carton Info/ Size Spec Info
        Try
            If Me.ogvDivertSrc.RowCount > 0 And Me.ogvDivertDT.RowCount > 0 Then



                oStrBuilder.AppendLine("DECLARE @TMERTOrderSubTmp AS TABLE (FTSubOrderNoAddNew NVARCHAR(30))")
                oStrBuilder.AppendLine("DECLARE @FTSubOrderNoAddNew AS NVARCHAR(30);")
                oStrBuilder.AppendLine("INSERT INTO @TMERTOrderSubTmp (FTSubOrderNoAddNew)")
                oStrBuilder.AppendLine(String.Format("EXEC SP_GEN_CHARACTER_SubOrderNo N'{0}';", HI.UL.ULF.rpQuoted(_FTOrderNoSrc)))
                oStrBuilder.AppendLine("DECLARE @FNRowCntEffect AS INT;")
                oStrBuilder.AppendLine("SELECT @FNRowCntEffect = @@ROWCOUNT;")
                oStrBuilder.AppendLine("IF (@FNRowCntEffect > 0)")

                oStrBuilder.AppendLine("BEGIN")

                oStrBuilder.AppendLine("   PRINT 'Row Effect Generate Add New Factory Sub Order No.: ' + CONVERT(VARCHAR(30), @FNRowCntEffect);")
                oStrBuilder.AppendLine("   SELECT @FTSubOrderNoAddNew = A.FTSubOrderNoAddNew FROM @TMERTOrderSubTmp AS A;")
                oStrBuilder.AppendLine("   SELECT @FTSubOrderNoAddNew ;")
                oStrBuilder.AppendLine("END;")
                _Qry = oStrBuilder.ToString()
                _FTNewSubOrderNo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "")


                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("DECLARE @TMERTOrderSubTmp AS TABLE (FTSubOrderNoAddNew NVARCHAR(30))")
                oStrBuilder.AppendLine("DECLARE @FTSubOrderNoAddNew AS NVARCHAR(30);")
                oStrBuilder.AppendLine("INSERT INTO @TMERTOrderSubTmp (FTSubOrderNoAddNew)")
                oStrBuilder.AppendLine(String.Format("EXEC SP_GEN_CHARACTER_SubOrderNo N'{0}';", HI.UL.ULF.rpQuoted(_FTOrderNoSrc)))
                oStrBuilder.AppendLine("DECLARE @FNRowCntEffect AS INT;")
                oStrBuilder.AppendLine("SELECT @FNRowCntEffect = @@ROWCOUNT;")
                oStrBuilder.AppendLine("IF (@FNRowCntEffect > 0)")

                oStrBuilder.AppendLine("BEGIN")

                oStrBuilder.AppendLine("   PRINT 'Row Effect Generate Add New Factory Sub Order No.: ' + CONVERT(VARCHAR(30), @FNRowCntEffect);")
                oStrBuilder.AppendLine("   SELECT @FTSubOrderNoAddNew = A.FTSubOrderNoAddNew FROM @TMERTOrderSubTmp AS A;")
                oStrBuilder.AppendLine("   PRINT '@FTSubOrderNo Add New : ' + @FTSubOrderNoAddNew;")

                '...TMERTOrderSub (Header Factory Sub Order No.)
                oStrBuilder.AppendLine("   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] ([FTInsUser],[FDInsDate],[FTInsTime]")
                oStrBuilder.AppendLine("              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]")
                oStrBuilder.AppendLine("              ,[FTOrderNo],[FTSubOrderNo]")
                oStrBuilder.AppendLine("              ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate],[FDShipDateOrginal]")
                oStrBuilder.AppendLine("              ,[FNHSysContinentId],[FNHSysCountryId]")
                oStrBuilder.AppendLine("              ,[FNHSysProvinceId],[FNHSysShipModeId],[FNHSysCurId]")
                oStrBuilder.AppendLine("              ,[FNHSysGenderId],[FNHSysUnitId],[FNHSysShipPortId]")
                oStrBuilder.AppendLine("              ,[FTStateEmb],[FTStatePrint],[FTStateHeat],[FTStateLaser],[FTStateWindows]")
                oStrBuilder.AppendLine("              ,[FTRemark],[FTCustRef],[FTPORef]")
                oStrBuilder.AppendLine("              ,[FNPackCartonSubType],[FNPackPerCarton]")
                oStrBuilder.AppendLine("              ,[FTSubOrderNoDivertRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FNOrderSetType])")


                oStrBuilder.AppendLine("   SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", ")
                oStrBuilder.AppendLine("          NULL, NULL, NULL, ")
                oStrBuilder.AppendLine("         N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "', @FTSubOrderNoAddNew, ")
                oStrBuilder.AppendLine("          '" & HI.UL.ULDate.ConvertEnDB(Me.FDSubOrderDate.Text) & "', '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', '" & HI.UL.ULDate.ConvertEnDB(Me.FDProDate.Text) & "', '" & HI.UL.ULDate.ConvertEnDB(Me.FDShipDate.Text) & "', [FDShipDateOrginal], ")
                oStrBuilder.AppendLine(String.Format("          {0}, {1}, ", {Val(Me.FNHSysContinentId.Properties.Tag), Val(Me.FNHSysCountryId.Properties.Tag)}))
                oStrBuilder.AppendLine(String.Format("          {0}, {1}, {2}, ", {Val(Me.FNHSysProvinceId.Properties.Tag), Val(Me.FNHSysShipModeId.Properties.Tag), Val(Me.FNHSysCurId.Properties.Tag)}))
                oStrBuilder.AppendLine(String.Format("          {0}, {1}, {2}, ", {Val(Me.FNHSysGenderId.Properties.Tag), Val(Me.FNHSysUnitId.Properties.Tag), Val(Me.FNHSysShipPortId.Properties.Tag)}))
                oStrBuilder.AppendLine("         N'" & Me.FTStateEmb.EditValue.ToString & "', N'" & Me.FTStatePrint.EditValue.ToString & "', N'" & Me.FTStateHeat.EditValue.ToString & "', N'" & Me.FTStateLaser.EditValue.ToString & "', N'" & Me.FTStateWindows.EditValue.ToString & "', ")
                oStrBuilder.AppendLine("         N'" & HI.UL.ULF.rpQuoted(Me.FTRemarkSubOrderNo.Text) & "', '" & HI.UL.ULF.rpQuoted(Me.FTCustRef.Text) & "', '" & HI.UL.ULF.rpQuoted(Me.FTPORef.Text.Trim()) & "', ")
                oStrBuilder.AppendLine("          NULL, NULL, ")
                oStrBuilder.AppendLine("         N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'," & Val(Me.FNHSysPlantId.Properties.Tag) & "," & Val(Me.FNHSysBuyGrpId.Properties.Tag) & ",FNOrderSetType")
                oStrBuilder.AppendLine("   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub] ")
                oStrBuilder.AppendLine(" WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "' ;")


                '...iterate loop save data factory sub order no breakdown
                '==========================================================================================================================================================================================================================================================================
                For nLoopDivertDT As Integer = 0 To Me.ogvDivertDT.DataRowCount - 1

                    For Each oColDivertDT As GridColumn In Me.ogvDivertDT.Columns

                        Dim tTextMatSizeTag As String = ""

                        Select Case Microsoft.VisualBasic.Mid(oColDivertDT.Name.ToUpper, 1, 13)

                            Case "FTMatSizeCode".ToUpper

                                If Not DBNull.Value.Equals(oColDivertDT.Tag) Then

                                    tTextMatSizeTag = oColDivertDT.Tag.ToString

                                    Try
                                        Dim numFNQuantityDivert As Integer

                                        numFNQuantityDivert = 0

                                        If Not DBNull.Value.Equals(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag)) Then

                                            numFNQuantityDivert = Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag))

                                            If numFNQuantityDivert > 0 Then

                                                Dim numDivertFNHSysMatColorId As Integer
                                                Dim tTextDivertFTMatColorCode As String
                                                Dim numDivertFNQuantity As Double
                                                Dim numDivertFNHSysMatSizeId As Integer
                                                Dim tTextDivertFTMatSizeCode As String
                                                Dim numDivertFNQuantityExtra As Double
                                                Dim numDivertFNQuantityTest As Double
                                                Dim numDivertFNGrandQuantity As Double
                                                Dim numDivertFNPrice As Double
                                                Dim numDivertFNValue As Double
                                                Dim numDivertFNExtraQtyPercent As Double
                                                Dim numDivertFNGarmentQtyTest As Integer
                                                Dim numDivertFNAmntQtyTest As Double
                                                Dim numDivertFNAmntExtra As Double
                                                Dim numDivertFNGrandAmnt As Double
                                                Dim _FTNikePOLineItem As String = ""

                                                numDivertFNHSysMatColorId = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNHSysMatColorId"))
                                                tTextDivertFTMatColorCode = Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTMatColorCode").ToString
                                                _FTNikePOLineItem = "" & Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTNikePOLineItem").ToString
                                                numDivertFNQuantity = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag))
                                                numDivertFNHSysMatSizeId = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNHSysMatSizeId" & tTextMatSizeTag))
                                                tTextDivertFTMatSizeCode = Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTMatSizeCode" & tTextMatSizeTag).ToString
                                                numDivertFNQuantityExtra = 0
                                                numDivertFNQuantityTest = 0
                                                numDivertFNGrandQuantity = numDivertFNQuantity
                                                numDivertFNPrice = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNPrice" & tTextMatSizeTag))
                                                numDivertFNValue = numFNQuantityDivert * numDivertFNPrice
                                                numDivertFNExtraQtyPercent = 0
                                                numDivertFNGarmentQtyTest = 0
                                                numDivertFNAmntQtyTest = 0
                                                numDivertFNAmntExtra = 0
                                                numDivertFNGrandAmnt = numDivertFNValue

                                                oStrBuilder.AppendLine("   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]")
                                                oStrBuilder.AppendLine("                                                               ,[FTUpdUser],[FDUpdDate],[FTUpdTime]")
                                                oStrBuilder.AppendLine("                                                               ,[FTOrderNo],[FTSubOrderNo]")
                                                oStrBuilder.AppendLine("                                                               ,[FTColorway],[FTSizeBreakDown]")
                                                oStrBuilder.AppendLine("                                                               ,[FNHSysMatColorId],[FNHSysMatSizeId]")
                                                oStrBuilder.AppendLine("                                                               ,[FNPrice],[FNQuantity]")
                                                oStrBuilder.AppendLine("                                                               ,[FNAmt]")
                                                oStrBuilder.AppendLine("                                                               ,[FNExtraQty],[FNQuantityExtra]")
                                                oStrBuilder.AppendLine("                                                               ,[FNGrandQuantity]")
                                                oStrBuilder.AppendLine("                                                               ,[FNAmntExtra]")
                                                oStrBuilder.AppendLine("                                                               ,[FNGrandAmnt]")
                                                oStrBuilder.AppendLine("                                                               ,[FNGarmentQtyTest]")
                                                oStrBuilder.AppendLine("                                                               ,[FNAmntQtyTest]")
                                                oStrBuilder.AppendLine("                                                               ,[FNPriceOrg]")
                                                oStrBuilder.AppendLine("                                                               ,[FNNetPrice]")
                                                oStrBuilder.AppendLine("                                                               ,[FNOperateFee]")
                                                oStrBuilder.AppendLine("                                                               ,[FNOperateFeeAmt]")
                                                oStrBuilder.AppendLine("                                                               ,[FNNetFOB]")
                                                oStrBuilder.AppendLine("                                                               ,[FTNikePOLineItem])")

                                                oStrBuilder.AppendLine("   SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", ")
                                                oStrBuilder.AppendLine("          NULL, NULL, NULL, ")
                                                oStrBuilder.AppendLine("          N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "', @FTSubOrderNoAddNew, ")
                                                oStrBuilder.AppendLine("          N'" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "', '" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "', ")
                                                oStrBuilder.AppendLine(String.Format("              {0}, {1}, ", {numDivertFNHSysMatColorId, numDivertFNHSysMatSizeId}))
                                                oStrBuilder.AppendLine(String.Format("              {0}, {1}, ", {numDivertFNPrice, numDivertFNQuantity}))
                                                oStrBuilder.AppendLine(String.Format("              {0}, ", numDivertFNValue))
                                                oStrBuilder.AppendLine(String.Format("              {0}, {1}, ", {numDivertFNExtraQtyPercent, numDivertFNQuantityExtra}))
                                                oStrBuilder.AppendLine(String.Format("              {0}, ", numDivertFNGrandQuantity))
                                                oStrBuilder.AppendLine(String.Format("              {0}, ", numDivertFNAmntExtra))
                                                oStrBuilder.AppendLine(String.Format("              {0}, ", numDivertFNGrandAmnt))
                                                oStrBuilder.AppendLine(String.Format("              {0}, ", numDivertFNGarmentQtyTest))
                                                oStrBuilder.AppendLine(String.Format("              {0}", numDivertFNAmntQtyTest))


                                                oStrBuilder.AppendLine(",ISNULL((SELECT TOP 1  FNPriceOrg FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS XA WITH(NOLOCK) WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNoSrc.Text.Trim) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "'),NULL)")
                                                oStrBuilder.AppendLine(",ISNULL((SELECT TOP 1  FNNetPrice FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS XA WITH(NOLOCK) WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNoSrc.Text.Trim) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "'),NULL)")

                                                oStrBuilder.AppendLine(",ISNULL((SELECT TOP 1  FNOperateFee FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS XA WITH(NOLOCK) WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNoSrc.Text.Trim) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "'),NULL)")
                                                oStrBuilder.AppendLine(",ISNULL((SELECT TOP 1  FNOperateFeeAmt FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS XA WITH(NOLOCK) WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNoSrc.Text.Trim) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "'),NULL)")
                                                oStrBuilder.AppendLine(",ISNULL((SELECT TOP 1  FNNetFOB FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS XA WITH(NOLOCK) WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNoSrc.Text.Trim) & "' AND FTColorway='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "'),NULL)")

                                                oStrBuilder.AppendLine(",'" & HI.UL.ULF.rpQuoted(_FTNikePOLineItem) & "' ")

                                                REM oStrBuilder.AppendLine("   UNION ALL")

                                                '...balance after divert
                                                Dim numFNQuantityBalance As Integer

                                                If DBNull.Value.Equals(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag)) Then
                                                    numFNQuantityBalance = 0
                                                Else
                                                    numFNQuantityBalance = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag))
                                                End If

                                                Dim numBalanceFNHSysMatColorId As Integer
                                                Dim tTextBalanceFTMatColorCode As String
                                                Dim numBalanceFNQuantity As Double
                                                Dim numBalanceFNHSysMatSizeId As Integer
                                                Dim tTextBalanceFTMatSizeCode As String
                                                Dim numBalanceFNQuantityExtra As Double
                                                Dim numBalanceFNQuantityTest As Double
                                                Dim numBalanceFNGrandQuantity As Double
                                                Dim numBalanceFNPrice As Double
                                                Dim numBalanceFNValue As Double
                                                Dim numBalanceFNExtraQtyPercent As Double
                                                Dim numBalanceFNAmntQtyTest As Double
                                                Dim numBalanceFNAmntExtra As Double
                                                Dim numBalanceFNGrandAmnt As Double

                                                numBalanceFNHSysMatColorId = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNHSysMatColorId"))
                                                tTextBalanceFTMatColorCode = Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FTMatColorCode").ToString
                                                numBalanceFNQuantity = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag))
                                                numBalanceFNHSysMatSizeId = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNHSysMatSizeId" & tTextMatSizeTag))
                                                tTextBalanceFTMatSizeCode = Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FTMatSizeCode" & tTextMatSizeTag).ToString
                                                numBalanceFNQuantity = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag))
                                                numBalanceFNPrice = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNPrice" & tTextMatSizeTag))

                                                numBalanceFNValue = numBalanceFNPrice * numBalanceFNQuantity

                                                If DBNull.Value.Equals(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNExtraQtyPercent" & tTextMatSizeTag)) Then
                                                    numBalanceFNExtraQtyPercent = 0
                                                    numBalanceFNQuantityExtra = 0
                                                    numBalanceFNAmntExtra = 0
                                                Else
                                                    numBalanceFNExtraQtyPercent = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNExtraQtyPercent" & tTextMatSizeTag))
                                                    numBalanceFNQuantityExtra = Microsoft.VisualBasic.Conversion.Fix((numBalanceFNQuantity) * (numBalanceFNExtraQtyPercent / 100)) + IIf(((numBalanceFNQuantity) * (numBalanceFNExtraQtyPercent / 100)) - (Fix(Val(numBalanceFNQuantity) * (numBalanceFNExtraQtyPercent / 100))) > 0, 1, 0)
                                                    numBalanceFNAmntExtra = numBalanceFNQuantityExtra * numBalanceFNPrice
                                                End If

                                                If DBNull.Value.Equals(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNQuantityTest" & tTextMatSizeTag)) Then
                                                    numBalanceFNQuantityTest = 0
                                                    numBalanceFNAmntQtyTest = 0
                                                Else
                                                    numBalanceFNQuantityTest = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertBalance.GetRowCellValue(nLoopDivertDT, "FNQuantityTest" & tTextMatSizeTag))
                                                    numBalanceFNAmntQtyTest = numBalanceFNPrice * numBalanceFNQuantityTest
                                                End If

                                                numBalanceFNGrandQuantity = numBalanceFNQuantity + numBalanceFNQuantityExtra + numBalanceFNQuantityTest

                                                numBalanceFNGrandAmnt = numBalanceFNValue + numBalanceFNAmntExtra + numBalanceFNAmntQtyTest

                                                oStrBuilder.AppendLine("   UPDATE A")
                                                oStrBuilder.AppendLine("   SET    A.[FTUpdUser] = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
                                                oStrBuilder.AppendLine("         ,A.[FDUpdDate] = " & HI.UL.ULDate.FormatDateDB)
                                                oStrBuilder.AppendLine("         ,A.[FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB)
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNPrice] = {0}", numBalanceFNPrice))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNQuantity] = {0}", numBalanceFNQuantity))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNAmt] = {0}", numBalanceFNValue))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNExtraQty] = {0}", numBalanceFNExtraQtyPercent))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNQuantityExtra] = {0}", numBalanceFNQuantityExtra))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNGrandQuantity] = {0}", numBalanceFNGrandQuantity))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNAmntExtra] = {0}", numBalanceFNAmntExtra))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNGrandAmnt] = {0}", numBalanceFNGrandAmnt))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNGarmentQtyTest] = {0}", numBalanceFNQuantityTest))
                                                oStrBuilder.AppendLine(String.Format("         ,A.[FNAmntQtyTest] = {0}", numBalanceFNAmntQtyTest))
                                                oStrBuilder.AppendLine("   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] AS A")
                                                oStrBuilder.AppendLine("   WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
                                                oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'")
                                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", numBalanceFNHSysMatColorId))
                                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", numBalanceFNHSysMatSizeId))

                                            Else
                                                '...Nothing
                                            End If

                                        Else
                                            '...Nothing
                                        End If


                                    Catch ex As Exception
                                        If System.Diagnostics.Debugger.IsAttached = True Then
                                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                                        End If

                                    End Try

                                End If

                            Case Else
                                '...Nothing
                        End Select

                    Next

                Next nLoopDivertDT

                REM oStrBuilder.Remove(oStrBuilder.Length - 10, 10) '...remove string terminate 'UNION ALL'

                REM oStrBuilder.Append(";") '...teminate insert new factory sub order no breakdown

                '==========================================================================================================================================================================================================================================================================

                oStrBuilder.AppendLine("END;")

                sSQL = ""
                sSQL = oStrBuilder.ToString()

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                If HI.Conn.SQLConn.Execute_Tran(sSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                    Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    Call CopySubOrderDetail(FTSubOrderSrc, FTOrderNoSrc, _FTNewSubOrderNo)

                    bRet = True
                Else
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End If

            End If

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If

        End Try

        Return bRet

    End Function

    Private Sub CopySubOrderDetail(_tFTSubOrderNoSrc As String, _tFTOrderNoDest As String, _tFTSubOrderNoDest As String)
        Dim tSql As String = ""

        If _tFTSubOrderNoDest = "" Then Exit Sub

        '...TMERTOrderSub_Sew
        tSql = ""
        tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]"
        tSql &= vbCrLf & "  ( "
        tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage"
        tSql &= vbCrLf & "  )"
        tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSewSeq, FTSewDescription, FTSewNote, FTImage"
        tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] WITH(NOLOCK) "
        tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        tSql = ""
        tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTImage"
        tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
        tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
        tSql &= Environment.NewLine & "ORDER BY A.FNSewSeq ASC;"

        Dim tmpDTSew As System.Data.DataTable

        tmpDTSew = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        For Each oDataRowSew As System.Data.DataRow In tmpDTSew.Rows
            Dim oImageSew As System.Drawing.Image
            Dim FTImageSew As String
            Dim FNSewSeq As Integer

            FTImageSew = oDataRowSew!FTImage.ToString
            FNSewSeq = Val(oDataRowSew!FNSewSeq.ToString)

            oImageSew = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\" & FTImageSew)

            If Not oImageSew Is Nothing Then
                FTImageSew = _tFTSubOrderNoDest & "_" & FNSewSeq.ToString
                FTImageSew = Microsoft.VisualBasic.Replace(FTImageSew, "-", "_")

                tSql = ""
                tSql = "UPDATE A"
                tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImageSew, FTImageSew, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Sewing\") & "'"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & FNSewSeq & ";"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            End If

        Next

        tmpDTSew.Dispose()

        '...TMERTOrderSub_Pack
        tSql = ""
        tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]"
        tSql &= vbCrLf & "  ( "
        tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage "
        tSql &= vbCrLf & "  )"
        tSql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNPackSeq, FTPackDescription, FTPackNote, FTImage "
        tSql &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] WITH(NOLOCK) "
        tSql &= vbCrLf & "WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "
        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        tSql = ""
        tSql = "SELECT  A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTImage"
        tSql &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
        tSql &= Environment.NewLine & "WHERE  (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "') AND (A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "')"
        tSql &= Environment.NewLine & "ORDER BY A.FNPackSeq ASC;"

        Dim tmpDTPack As System.Data.DataTable

        tmpDTPack = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        For Each oDataRowPack As System.Data.DataRow In tmpDTPack.Rows
            Dim oImagePack As System.Drawing.Image
            Dim FTImagePack As String
            Dim FNPackSeq As Integer

            FTImagePack = oDataRowPack!FTImage.ToString
            FNPackSeq = Val(oDataRowPack!FNPackSeq.ToString)
            oImagePack = HI.UL.ULImage.LoadImage("" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\" & FTImagePack)

            If Not oImagePack Is Nothing Then
                FTImagePack = _tFTSubOrderNoDest & "_" & FNPackSeq.ToString
                FTImagePack = Microsoft.VisualBasic.Replace(FTImagePack, "-", "_")

                tSql = ""
                tSql = "UPDATE A"
                tSql &= Environment.NewLine & "SET A.FTImage = '" & HI.UL.ULImage.SaveImage(oImagePack, FTImagePack, "" & __SystemFilePath & "\OrderNo\SubOrderNo\Packing\") & "'"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A "
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "'"
                tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & FNPackSeq & ";"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            End If

        Next

        tmpDTPack.Dispose()


        '...TMERTOrderSub_Bundle
        tSql = ""
        tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle]([FTInsUser], [FDInsDate], [FTInsTime], [FTOrderNo], [FTSubOrderNo], [FTColorway], [FTSizeBreakDown], [FNQuantity])"
        tSql &= Environment.NewLine & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTInsUser, " & HI.UL.ULDate.FormatDateDB & " AS FDInsDate, " & HI.UL.ULDate.FormatTimeDB & " AS FTInsTime, N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "' AS FTOrderNo, N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "' AS FTSubOrderNo, A.[FTColorway], A.[FTSizeBreakDown], 0 AS FNQuantity"
        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Bundle] AS A (NOLOCK)"
        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "'"
        tSql &= Environment.NewLine & "      AND A.FTSubOrderNO = N'" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "';"

        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        '...TMERTOrderSub_SizeSpec
        tSql = ""
        tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec]"
        tSql &= vbCrLf & "  ( "
        tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension,FTTolerant "
        tSql &= vbCrLf & "  )"
        tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension,FTTolerant "
        tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_SizeSpec] WITH(NOLOCK) "
        tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        '...TMERTOrderSub_Component
        tSql = ""
        tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component]"
        tSql &= vbCrLf & "  ( "
        tSql &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
        tSql &= vbCrLf & "  )"
        tSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
        tSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tFTOrderNoDest) & "','" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoDest) & "', FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp,FNSeq "
        tSql &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] WITH(NOLOCK) "
        tSql &= vbCrLf & "  WHERE FTSubOrderNo='" & HI.UL.ULF.rpQuoted(_tFTSubOrderNoSrc) & "' "

        HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
    End Sub

    Private Function PROC_GETbPrepareTemplateDivertBreakdown(ByVal paramFTSubOrderNo As String) As Boolean
        Dim bRet As Boolean = False
        Dim oStrBuilder As New System.Text.StringBuilder
        Dim tmpDTDetailBreakdownDivert As System.Data.DataTable
        Dim tmpColorwaySizeBreakdownDivert As System.Data.DataTable
        '...สี/ไซส์/จำนวน/ราคา/มูลค่า

        '...Default Colorway           : FNHSysMatColorId  :: From Mat Color Id FTSubOrderNo Src
        '...Default Size Breakdown     : FNHSysMatSizeId   :: From Mat Size Id FTSubOrderNo Src
        '...Default Price              : FNPrice           :: From Price FTSubOrderNo Src
        '...Default Quantity           : FNQuantity        :: 0
        '...Default Amount             : FNAmt             :: 0
        '...Default Extra Qty (Percent): FNExtraQty        :: 0
        '...Default Quantity Extra     : FNQuantityExtra   :: 0
        '...Default Grand Quantity     : FNGrandQuantity   :: 0
        '...Default Amount Extra       : FNAmntExtra       :: 0
        '...Default Grand Amount       : FNGrandAmnt       :: 0
        '...Default Garment Test       : FNGarmentQtyTest  :: 0
        '...Default Amount Qty Test    : FNAmntQtyTest     :: 0
        Try
            Me.ogdDivertDT.DataSource = Nothing
            Me.ogdDivertDT.Refresh()
            Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertDT)
            Me.ogvDivertDT.OptionsView.ColumnAutoWidth = False
            Me.ogvDivertDT.OptionsNavigation.EnterMoveNextColumn = True

            sSQL = ""
            sSQL = "SELECT A.FNHSysMatSizeId, A.FTMatSizeCode, A.FTMatSizeNameEN AS FTMatSizeName"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE EXISTS (SELECT 'T'"
            sSQL &= Environment.NewLine & "              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 (NOLOCK)"
            sSQL &= Environment.NewLine & "              WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            sSQL &= Environment.NewLine & "                    AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'"
            sSQL &= Environment.NewLine & "                    AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)"
            sSQL &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq ASC;"

            tmpColorwaySizeBreakdownDivert = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MASTER)

            If Not DBNull.Value.Equals(tmpColorwaySizeBreakdownDivert) AndAlso tmpColorwaySizeBreakdownDivert.Rows.Count > 0 Then

                oGridViewBreakdownDivertDetail = Me.ogdDivertDT.Views(0)
                Call PROC_GETbRemoveGridViewColumn(oGridViewBreakdownDivertDetail)

                Me.ogdDivertDT.DataSource = Nothing
                tmpDTDetailBreakdownDivert = New System.Data.DataTable()

                tmpDTDetailBreakdownDivert.Columns.Add("FNHSysMatColorId", GetType(Integer))
                tmpDTDetailBreakdownDivert.Columns.Add("FTMatColorCode", GetType(String))
                tmpDTDetailBreakdownDivert.Columns.Add("FTMatColorName", GetType(String))
                tmpDTDetailBreakdownDivert.Columns.Add("FTDescription", GetType(String))
                tmpDTDetailBreakdownDivert.Columns.Add("FTNikePOLineItem", GetType(String))
                tmpDTDetailBreakdownDivert.Columns.Add("FNTotal", GetType(Integer))

                'Dim oColFNHSysMatColorId As System.Data.DataColumn
                'oColFNHSysMatColorId = New System.Data.DataColumn("FNHSysMatColorId", System.Type.GetType("System.Int32"))

                'With oColFNHSysMatColorId
                '    .Caption = "FNHSysMatColorId"

                '    oGridViewBreakdownDivertDetail.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Name = oColFNHSysMatColorId.ColumnName
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Visible = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTDetailBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                'Dim oColFTMatColorCode As System.Data.DataColumn
                'oColFTMatColorCode = New System.Data.DataColumn("FTMatColorCode", System.Type.GetType("System.String"))

                'With oColFTMatColorCode
                '    Select Case HI.ST.Lang.Language
                '        Case HI.ST.Lang.eLang.EN
                '            .Caption = "Colorway"
                '        Case HI.ST.Lang.eLang.TH
                '            .Caption = "รหัสสี"
                '        Case HI.ST.Lang.eLang.KM
                '            .Caption = "Colorway"
                '        Case HI.ST.Lang.eLang.VT
                '            .Caption = "Colorway"
                '    End Select

                '    oGridViewBreakdownDivertDetail.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Visible = True
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).VisibleIndex = 0
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSort = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.TabStop = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowColumnMRUFilterList = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowFilterEditor = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowMRUFilterList = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowFilterIncrementalSearch = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False

                '    tmpDTDetailBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                'Dim oColFTMatColorName As System.Data.DataColumn
                'oColFTMatColorName = New System.Data.DataColumn("FTMatColorName", System.Type.GetType("System.String"))

                'With oColFTMatColorName
                '    .Caption = "FTMatColorName"

                '    oGridViewBreakdownDivertSrc.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).Visible = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertSrc.Columns(.ColumnName).OptionsColumn.AllowSort = False

                '    tmpDTDetailBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                'Dim oColFTDescription As System.Data.DataColumn
                'oColFTDescription = New System.Data.DataColumn("FTDescription", System.Type.GetType("System.String"))

                'With oColFTDescription
                '    Select Case HI.ST.Lang.Language
                '        Case HI.ST.Lang.eLang.EN
                '            .Caption = "Color way / Size Breakdown"
                '        Case HI.ST.Lang.eLang.TH
                '            .Caption = "สี / ไซส์"
                '        Case Else
                '            .Caption = "Color way / Size Breakdown"
                '    End Select

                '    oGridViewBreakdownDivertDetail.Columns.AddField(.ColumnName)
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).FieldName = .ColumnName
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Name = .ColumnName
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Caption = .Caption
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Visible = True
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).VisibleIndex = 1

                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSize = False

                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowMove = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSort = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Fixed = FixedStyle.Left
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSize = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.TabStop = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowColumnMRUFilterList = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowFilterEditor = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowMRUFilterList = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowFilterIncrementalSearch = False
                '    oGridViewBreakdownDivertDetail.Columns(.ColumnName).View.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False

                '    tmpDTDetailBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                'End With

                For Each oRow As DataRow In tmpColorwaySizeBreakdownDivert.Rows

                    With oGridViewBreakdownDivertDetail
                        Dim oColAppendSizeId As System.Data.DataColumn = New System.Data.DataColumn("FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
                        oColAppendSizeId.Caption = "FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeId.ColumnName)
                        .Columns(oColAppendSizeId.ColumnName).FieldName = oColAppendSizeId.ColumnName
                        .Columns(oColAppendSizeId.ColumnName).Name = oColAppendSizeId.ColumnName
                        .Columns(oColAppendSizeId.ColumnName).Caption = oColAppendSizeId.Caption
                        .Columns(oColAppendSizeId.ColumnName).Visible = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeId.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendSizeId.ColumnName, oColAppendSizeId.DataType)

                        Dim oColAppendSizeCode As System.Data.DataColumn = New System.Data.DataColumn("FTMatSizeCode" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                        oColAppendSizeCode.Caption = oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeCode.ColumnName)
                        .Columns(oColAppendSizeCode.ColumnName).FieldName = oColAppendSizeCode.ColumnName
                        .Columns(oColAppendSizeCode.ColumnName).Name = oColAppendSizeCode.ColumnName
                        .Columns(oColAppendSizeCode.ColumnName).Caption = oColAppendSizeCode.Caption
                        .Columns(oColAppendSizeCode.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                        .Columns(oColAppendSizeCode.ColumnName).Visible = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeCode.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendSizeCode.ColumnName, oColAppendSizeCode.DataType)

                        Dim oColAppendSizeName As System.Data.DataColumn = New System.Data.DataColumn("FTMatSizeName" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                        oColAppendSizeName.Caption = "FTMatSizeName" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendSizeName.ColumnName)
                        .Columns(oColAppendSizeName.ColumnName).FieldName = oColAppendSizeName.ColumnName
                        .Columns(oColAppendSizeName.ColumnName).Name = oColAppendSizeName.ColumnName
                        .Columns(oColAppendSizeName.ColumnName).Caption = oColAppendSizeName.Caption
                        .Columns(oColAppendSizeName.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                        .Columns(oColAppendSizeName.ColumnName).Visible = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendSizeName.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendSizeName.ColumnName, oColAppendSizeName.DataType)

                        Dim oColAppendAmntZZZ As System.Data.DataColumn = New System.Data.DataColumn("FNAmnt" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendAmntZZZ.Caption = oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendAmntZZZ.ColumnName)
                        .Columns(oColAppendAmntZZZ.ColumnName).FieldName = oColAppendAmntZZZ.ColumnName
                        .Columns(oColAppendAmntZZZ.ColumnName).Name = oColAppendAmntZZZ.ColumnName
                        .Columns(oColAppendAmntZZZ.ColumnName).Caption = oColAppendAmntZZZ.Caption

                        .Columns(oColAppendAmntZZZ.ColumnName).ColumnEdit = oRepositoryFNQuantity

                        .Columns(oColAppendAmntZZZ.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .Columns(oColAppendAmntZZZ.ColumnName).Visible = True

                        .Columns(oColAppendAmntZZZ.ColumnName).Fixed = FixedStyle.None
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowSize = False

                        .Columns(oColAppendAmntZZZ.ColumnName).Tag = oRow.Item("FTMatSizeCode")
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowEdit = True

                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendAmntZZZ.ColumnName).OptionsColumn.AllowSort = False


                        .Columns(oColAppendAmntZZZ.ColumnName).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                        .Columns(oColAppendAmntZZZ.ColumnName).SummaryItem.DisplayFormat = "{0:n0}"

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendAmntZZZ.ColumnName, oColAppendAmntZZZ.DataType)

                        Dim oColAppendFNQuantity As System.Data.DataColumn = New System.Data.DataColumn("FNQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantity.Caption = "FNQuantity" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantity.ColumnName)
                        .Columns(oColAppendFNQuantity.ColumnName).FieldName = oColAppendFNQuantity.ColumnName
                        .Columns(oColAppendFNQuantity.ColumnName).Name = oColAppendFNQuantity.ColumnName
                        .Columns(oColAppendFNQuantity.ColumnName).Caption = oColAppendFNQuantity.Caption
                        .Columns(oColAppendFNQuantity.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantity.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNQuantity.ColumnName, oColAppendFNQuantity.DataType)

                        Dim oColAppendFNQuantityExtra As System.Data.DataColumn = New System.Data.DataColumn("FNQuantityExtra" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantityExtra.Caption = "FNQuantityExtra" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantityExtra.ColumnName)
                        .Columns(oColAppendFNQuantityExtra.ColumnName).FieldName = oColAppendFNQuantityExtra.ColumnName
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Name = oColAppendFNQuantityExtra.ColumnName
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Caption = oColAppendFNQuantityExtra.Caption
                        .Columns(oColAppendFNQuantityExtra.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantityExtra.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNQuantityExtra.ColumnName, oColAppendFNQuantityExtra.DataType)

                        Dim oColAppendFNQuantityTest As System.Data.DataColumn = New System.Data.DataColumn("FNQuantityTest" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNQuantityTest.Caption = "FNQuantityTest" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNQuantityTest.ColumnName)
                        .Columns(oColAppendFNQuantityTest.ColumnName).FieldName = oColAppendFNQuantityTest.ColumnName
                        .Columns(oColAppendFNQuantityTest.ColumnName).Name = oColAppendFNQuantityTest.ColumnName
                        .Columns(oColAppendFNQuantityTest.ColumnName).Caption = oColAppendFNQuantityTest.Caption
                        .Columns(oColAppendFNQuantityTest.ColumnName).Visible = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNQuantityTest.ColumnName).OptionsColumn.AllowSort = DefaultBoolean.False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNQuantityTest.ColumnName, oColAppendFNQuantityTest.DataType)

                        Dim oColAppendFNGrandQuantity As System.Data.DataColumn = New System.Data.DataColumn("FNGrandQuantity" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNGrandQuantity.Caption = "FNGrandQuantity" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNGrandQuantity.ColumnName)
                        .Columns(oColAppendFNGrandQuantity.ColumnName).FieldName = oColAppendFNGrandQuantity.ColumnName
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Name = oColAppendFNGrandQuantity.ColumnName
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Caption = oColAppendFNGrandQuantity.Caption
                        .Columns(oColAppendFNGrandQuantity.ColumnName).Visible = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNGrandQuantity.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNGrandQuantity.ColumnName, oColAppendFNGrandQuantity.DataType)

                        Dim oColAppendFNPrice As System.Data.DataColumn = New System.Data.DataColumn("FNPrice" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNPrice.Caption = "FNPrice" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNPrice.ColumnName)
                        .Columns(oColAppendFNPrice.ColumnName).FieldName = oColAppendFNPrice.ColumnName
                        .Columns(oColAppendFNPrice.ColumnName).Name = oColAppendFNPrice.ColumnName
                        .Columns(oColAppendFNPrice.ColumnName).Caption = oColAppendFNPrice.Caption
                        .Columns(oColAppendFNPrice.ColumnName).Visible = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNPrice.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNPrice.ColumnName, oColAppendFNPrice.DataType)

                        Dim oColAppendFNValue As System.Data.DataColumn = New System.Data.DataColumn("FNValue" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNValue.Caption = "FNValue" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNValue.ColumnName)
                        .Columns(oColAppendFNValue.ColumnName).FieldName = oColAppendFNValue.ColumnName
                        .Columns(oColAppendFNValue.ColumnName).Name = oColAppendFNValue.ColumnName
                        .Columns(oColAppendFNValue.ColumnName).Caption = oColAppendFNValue.Caption
                        .Columns(oColAppendFNValue.ColumnName).Visible = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNValue.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNValue.ColumnName, oColAppendFNValue.DataType)

                        Dim oColAppendFNExtraQtyPercent As System.Data.DataColumn = New System.Data.DataColumn("FNExtraQtyPercent" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Double"))
                        oColAppendFNExtraQtyPercent.Caption = "FNExtraQtyPercent" & oRow.Item("FTMatSizeCode").ToString()

                        .Columns.AddField(oColAppendFNExtraQtyPercent.ColumnName)
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).FieldName = oColAppendFNExtraQtyPercent.ColumnName
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Name = oColAppendFNExtraQtyPercent.ColumnName
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Caption = oColAppendFNExtraQtyPercent.Caption
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).Visible = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowEdit = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowMove = False
                        .Columns(oColAppendFNExtraQtyPercent.ColumnName).OptionsColumn.AllowSort = False

                        tmpDTDetailBreakdownDivert.Columns.Add(oColAppendFNExtraQtyPercent.ColumnName, oColAppendFNExtraQtyPercent.DataType)

                    End With

                Next

                Dim oColAppendFNRowTotal As System.Data.DataColumn = New System.Data.DataColumn("FNRowTotal", System.Type.GetType("System.Double"))

                With oColAppendFNRowTotal
                    .Caption = "T/T"

                    oGridViewBreakdownDivertDetail.Columns.AddField(.ColumnName)
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).FieldName = .ColumnName
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Name = .ColumnName
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Caption = .Caption
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Visible = False

                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).Fixed = FixedStyle.Right
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSize = False

                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowEdit = False
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.AllowSort = False
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).DisplayFormat.FormatString = "{0:N0}"
                    oGridViewBreakdownDivertDetail.Columns(.ColumnName).OptionsColumn.TabStop = False

                    tmpDTDetailBreakdownDivert.Columns.Add(.ColumnName, .DataType)

                End With

                oStrBuilder.Remove(0, oStrBuilder.Length)

                If HI.ST.Lang.Language = HI.ST.Lang.eLang.TH Then
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameTH AS FTMatColorName")
                Else
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameEN AS FTMatColorName")
                End If
                oStrBuilder.AppendLine("      , ISNULL(B.FTNikePOLineItem,'') AS FTNikePOLineItem")
                oStrBuilder.AppendLine("      , D.FNHSysMatSizeId, D.FTMatSizeCode, D.FTMatSizeNameEN AS FTMatSizeName")
                oStrBuilder.AppendLine("      , 0 AS FNQuantity, B.FNPrice, 0 AS FNAmnt, 0 AS FNExtraQtyPercent")
                oStrBuilder.AppendLine("      , 0 AS FNQuantityExtra, 0 AS FNGarmentQtyTest")
                oStrBuilder.AppendLine("      , 0 AS FNGrandQuantity")
                oStrBuilder.AppendLine("      , 0 AS FNGrandAmnt")
                oStrBuilder.AppendLine("      , (C.FTMatColorNameEN + '/' + C.FTMatColorNameTH) AS FTColorExtension")
                oStrBuilder.AppendLine(String.Format("FROM [{0}]..[TMERTOrderSub_BreakDown] AS B WITH(NOLOCK) INNER JOIN [{1}]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FNHSysMatColorId = C.FNHSysMatColorId", {HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN), HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER)}))
                oStrBuilder.AppendLine(String.Format("                                                        INNER JOIN [{0}]..[TMERMMatSize] AS D WITH(NOLOCK) ON B.FNHSysMatSizeId = D.FNHSysMatSizeId", HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER)))
                oStrBuilder.AppendLine(String.Format("WHERE B.FTOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(_FTOrderNoSrc)))
                oStrBuilder.AppendLine(String.Format("      AND B.FTSubOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(paramFTSubOrderNo)))
                oStrBuilder.AppendLine("ORDER BY C.FNMatColorSeq ASC, D.FNMatSizeSeq ASC;")

                sSQL = ""
                sSQL = oStrBuilder.ToString()

                Dim tmpDTDetailBreakdwonDivertInfo As System.Data.DataTable

                tmpDTDetailBreakdwonDivertInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If tmpDTDetailBreakdwonDivertInfo.Rows.Count > 0 Then
                    Dim FNHSysMatColorIdPrv As Integer
                    Dim FNHSysMatColorIdCurr As Integer

                    Dim nLoopBreakdownSeq As Integer = 0
                    Dim FNHSysMatSizeIdPrv As Integer
                    Dim FNHSysMatSizeIdCurr As Integer

                    Dim nFirstRowColorway As Integer

                    FNHSysMatColorIdPrv = -1
                    FNHSysMatColorIdCurr = tmpDTDetailBreakdwonDivertInfo.Rows(0).Item("FNHSysMatColorId")

                    FNHSysMatSizeIdPrv = -1
                    FNHSysMatSizeIdCurr = tmpDTDetailBreakdwonDivertInfo.Rows(0).Item("FNHSysMatSizeId")

                    For nLoopBreakdownInfo As Integer = 0 To tmpDTDetailBreakdwonDivertInfo.Rows.Count - 1

                        If FNHSysMatColorIdPrv <> tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId") Then

                            Dim oRowItem As DataRow

                            '...กรณีเป็นรายการไซส์แรกของแต่ละสี
                            oRowItem = tmpDTDetailBreakdownDivert.NewRow()

                            With oRowItem
                                .Item("FNHSysMatColorId") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId")
                                .Item("FTMatColorCode") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatColorCode")
                                .Item("FTMatColorName") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatColorName")
                                .Item("FTNikePOLineItem") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTNikePOLineItem")
                                .Item("FTDescription") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTColorExtension")
                                .Item("FNAmnt" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                                .Item("FNRowTotal") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                                .Item("FNHSysMatSizeId" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")
                                .Item("FTMatSizeCode" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")
                                .Item("FTMatSizeName" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeName")
                                .Item("FNQuantity" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantity")
                                .Item("FNQuantityExtra" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantityExtra")
                                .Item("FNQuantityTest" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGarmentQtyTest")
                                .Item("FNGrandQuantity" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                                .Item("FNPrice" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNPrice")
                                .Item("FNValue" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNAmnt")
                                .Item("FNExtraQtyPercent" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNExtraQtyPercent")
                            End With

                            tmpDTDetailBreakdownDivert.Rows.Add(oRowItem)

                            FNHSysMatColorIdPrv = FNHSysMatColorIdCurr
                            '...binding first row each of colorway/size breakdown
                            nFirstRowColorway = tmpDTDetailBreakdownDivert.Rows.Count - 1

                        Else
                            '...สีเดียวกัน/ไซส์ต่างกัน
                            FNHSysMatColorIdCurr = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId")
                            '...Iterate loop all column size breakdown / repeat all size breakdown on colorway
                            nLoopBreakdownSeq = nFirstRowColorway

                            Dim nFNAmnt As Double

                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNHSysMatSizeId" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FTMatSizeCode" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FTMatSizeName" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode")) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeName")

                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNAmnt" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNQuantity" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantity")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNQuantityExtra" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNQuantityExtra")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNQuantityTest" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGarmentQtyTest")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNGrandQuantity" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNGrandQuantity")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNPrice" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNPrice")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNValue" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNAmnt")
                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNExtraQtyPercent" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()) = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNExtraQtyPercent")

                            nFNAmnt = Val(tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNAmnt" & tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatSizeCode").ToString()).ToString()) + Val(tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNRowTotal").ToString())

                            tmpDTDetailBreakdownDivert.Rows(nLoopBreakdownSeq).Item("FNRowTotal") = nFNAmnt

                            tmpDTDetailBreakdownDivert.AcceptChanges()

                        End If

                        FNHSysMatColorIdPrv = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatColorId")
                        FNHSysMatSizeIdPrv = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FNHSysMatSizeId")

                    Next nLoopBreakdownInfo

                    tmpDTDetailBreakdownDivert.AcceptChanges()

                End If

                If Not tmpDTDetailBreakdwonDivertInfo Is Nothing Then tmpDTDetailBreakdwonDivertInfo.Dispose()

                oGridViewBreakdownDivertDetail.OptionsBehavior.AllowAddRows = False
                oGridViewBreakdownDivertDetail.OptionsCustomization.AllowSort = False

                oGridViewBreakdownDivertDetail.ActiveFilter.Clear()

                ogdDivertDT.DataSource = tmpDTDetailBreakdownDivert
                ogdDivertDT = oGridViewBreakdownDivertDetail.GridControl
                oGridViewBreakdownDivertSrc.OptionsView.ColumnAutoWidth = False
                ogdDivertDT.Refresh()
                oGridViewBreakdownDivertDetail.RefreshData()

                bRet = True

            Else
                Me.ogdDivertDT.DataSource = Nothing
                Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertDT)
                Me.ogvDivertDT.OptionsView.ColumnAutoWidth = False
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If

            Me.ogdDivertDT.DataSource = Nothing
            Call PROC_GETbRemoveGridViewColumn(Me.ogvDivertDT)
            Me.ogvDivertDT.OptionsView.ColumnAutoWidth = False

        End Try

        Return bRet

    End Function

#End Region

    Private Sub wDivertOrderSub_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call PROC_GETbInitSubOrderNoSrcDivert()

        ogvDivertSrc.OptionsView.ShowAutoFilterRow = False
        ogvDivertDT.OptionsView.ShowAutoFilterRow = False
        ogvDivertBalance.OptionsView.ShowAutoFilterRow = False

        If Me.FTSubOrderNoSrc.Properties.Items.Count > 0 Then
            '...Defalut First Sub Order No. for divert breakdown
            Me.FTSubOrderNoSrc.SelectedIndex = 0
            Call FTSubOrderNoSrc_SelectedIndexChanged(sender, e)
        End If

    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If PROC_GETbValidateSubOrderNoInfo() = True Then

            Dim tmpDTStateOrderNo As System.Data.DataTable

            tmpDTStateOrderNo = LoadStateOrderNoSub(_FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)

            Dim tTextFTStateSubMRP As String = ""
            'Dim tTextFTStateSubReserve As String = ""
            Dim tTextFTStateSubSourcing As String = ""
            Dim tTextFTStateSubPurchase As String = ""
            Dim tTextFTStateSubReceive As String = ""
            Dim tTextFTStateSubCutting As String = ""
            Dim tTextFTStateSubSewing As String = ""
            Dim tTextFTStateSubPacking As String = ""

            tTextFTStateSubMRP = "0" : tTextFTStateSubSourcing = "0" : tTextFTStateSubPurchase = "0" : tTextFTStateSubReceive = "0" : tTextFTStateSubCutting = "0" : tTextFTStateSubSewing = "0" : tTextFTStateSubPacking = "0"

            If Not DBNull.Value.Equals(tmpDTStateOrderNo) AndAlso tmpDTStateOrderNo.Rows.Count > 0 Then
                For Each oDataRow As System.Data.DataRow In tmpDTStateOrderNo.Rows
                    tTextFTStateSubMRP = oDataRow!FTStateSubMRP.ToString
                    'tTextFTStateSubSourcing = oDataRow!FTStateSubSourcing.ToString
                    'tTextFTStateSubPurchase = oDataRow!FTStateSubPurchase.ToString
                    tTextFTStateSubCutting = oDataRow!FTStateSubCutting.ToString
                    tTextFTStateSubSewing = oDataRow!FTStateSubSewing.ToString
                    tTextFTStateSubPacking = oDataRow!FTStateSubPacking.ToString

                    Exit For

                Next

            End If

            If Not tmpDTStateOrderNo Is Nothing Then tmpDTStateOrderNo.Dispose()

            If tTextFTStateSubCutting = "1" Or tTextFTStateSubSewing = "1" Or tTextFTStateSubPacking = "1" Then
                Dim tTextState As String
                tTextState = ""
                If tTextFTStateSubCutting = "1" Then
                    tTextState = IIf(tTextState <> "", "|" & "Cutting", "Cutting")
                End If
                If tTextFTStateSubSewing = "1" Then
                    tTextState = IIf(tTextState <> "", "|" & "Sewing", "Sewing")
                End If
                If tTextFTStateSubPacking = "1" Then
                    tTextState = IIf(tTextState <> "", "|" & "Packing", "Packing")
                End If

                HI.MG.ShowMsg.mInfo("พบ Transaction ต่างๆของ FO No Order Sub ในกระบวนงานขั้นตอนการผลิต" & Microsoft.VisualBasic.vbCrLf & "ไม่สามารถทำการ Divert FO No. Sub ได้ !!!", 1501140001, Me.Text, _FTOrderNoSrc & Environment.NewLine & tTextState, MessageBoxIcon.Warning)

                'DialogResult = System.Windows.Forms.DialogResult.Cancel

            Else

                If System.Windows.Forms.MessageBox.Show("Are you sure, do you want to divert sub breakdown" & Environment.NewLine & "from Factory Order No. : " & _FTOrderNoSrc & Environment.NewLine & "Factory Sub Order No. : " & Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString, "Confirm Divert Sub Breakdown", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then
                    If PROC_SAVEbDivertSubOrderNo(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString) = True Then
                        DialogResult = System.Windows.Forms.DialogResult.OK
                        Me.Close()
                    Else
                        'DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End If
                Else
                    'DialogResult = System.Windows.Forms.DialogResult.Cancel
                End If

            End If

        Else
            'DialogResult = System.Windows.Forms.DialogResult.Cancel
        End If

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FTSubOrderNoSrc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTSubOrderNoSrc.SelectedIndexChanged
        Try
            If Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString <> "" Then
                Call PROC_SETxClearControl()
                Call PROC_GETbShowBrowseDataFactorySubOrderNoInfo(Me._FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                Call PROC_GETbShowBrowseDataMatrixBreakdownSrcDivert(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                Call PROC_GETbPrepareTemplateDivertBreakdown(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)

                If Not _bScreenLoad Then
                    'Me.otpDivertFrom.TabControl.TabPages(eDivertTabPage.eDivertSrc).Text = "Divert from Sub Order No. : " & Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString
                Else
                    If _bScreenLoad = True Then _bScreenLoad = False
                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub

    'Private Sub ogvDivertDT_CustomRowCellEdit(sender As Object, e As XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles ogvDivertDT.CustomRowCellEdit

    'End Sub

    'Private Sub ogvDivertDT_CustomRowCellEditForEditing(sender As Object, e As XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles ogvDivertDT.CustomRowCellEditForEditing
    '    Try
    '        e.RepositoryItem.EditFormat.FormatType = FormatType.Numeric
    '        e.RepositoryItem.EditFormat.FormatString = "N0"
    '    Catch ex As Exception
    '        If System.Diagnostics.Debugger.IsAttached = True Then
    '            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
    '        End If
    '    End Try
    'End Sub

    'Private Sub ogvdivertdt_keydown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvDivertDT.KeyDown
    '    If e.keycode = System.Windows.Forms.keys.decimal Or e.keycode = System.Windows.Forms.keys.oemminus Then
    '        With Me.ogvDivertDT
    '            If .FocusedRowHandle >= 0 And Microsoft.VisualBasic.Mid(.FocusedColumn.Name, 1, 6).ToUpper = "FNAMNT" Then
    '                e.Handled = True
    '            Else
    '                e.Handled = False
    '            End If
    '        End With
    '    Else
    '        e.Handled = False
    '    End If
    'End Sub

    'Private Sub ogvDivertDT_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles ogvDivertDT.KeyPress
    '    Try
    '        'If Asc(e.KeyChar) = 110 Or Asc(e.KeyChar) = 189 Then
    '        '    With Me.ogvDivertDT
    '        '        If .FocusedRowHandle >= 0 And Microsoft.VisualBasic.Mid(.FocusedColumn.Name, 1, 6).ToUpper = "FNAMNT" Then
    '        '            e.Handled = True
    '        '        End If
    '        '    End With
    '        'Else
    '        '    e.Handled = False
    '        'End If

    '        'If Asc(e.KeyChar) <> 8 Then
    '        '    If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
    '        '        e.Handled = True
    '        '    End If
    '        'End If

    '        If Asc(e.KeyChar) <> 127 And Asc(e.KeyChar) <> 8 And Asc(e.KeyChar) <> 13 Then
    '            If IsNumeric(Chr(Asc(e.KeyChar))) = False Then
    '                e.Handled = True
    '            Else
    '                e.Handled = False
    '            End If
    '        Else
    '            e.Handled = False
    '        End If

    '    Catch ex As Exception
    '        If System.Diagnostics.Debugger.IsAttached = True Then
    '            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
    '        End If
    '    End Try
    'End Sub

    Private Sub ogvDivertDT_CellValueChanged(sender As Object, e As XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvDivertDT.CellValueChanged
        Try
            If Microsoft.VisualBasic.Mid(e.Column.Name.ToString.ToUpper, 1, 6) = "FNAmnt".ToUpper Then

                Dim tmpDTSrcBreakdownDivert As System.Data.DataTable

                tmpDTSrcBreakdownDivert = CType(Me.ogdDivertSrc.DataSource, System.Data.DataTable)

                If Not tmpDTSrcBreakdownDivert Is Nothing AndAlso tmpDTSrcBreakdownDivert.Rows.Count > 0 Then
                    Dim numQuantityDivertTarget As Double
                    numQuantityDivertTarget = Val(e.Value)

                    If numQuantityDivertTarget > 0 Then
                        Dim numRowHandle As Integer
                        numRowHandle = e.RowHandle

                        If e.RowHandle > -1 Then
                            Try
                                Dim numFNQuantiryDivertSrc As Integer

                                If Not DBNull.Value.Equals(Me.ogvDivertSrc.GetRowCellValue(numRowHandle, e.Column.Name.ToString)) And Me.ogvDivertSrc.GetRowCellValue(numRowHandle, e.Column.Name.ToString).ToString <> "" Then
                                    numFNQuantiryDivertSrc = Val(Me.ogvDivertSrc.GetRowCellValue(numRowHandle, e.Column.Name.ToString))
                                Else
                                    numFNQuantiryDivertSrc = 0
                                End If

                                If numQuantityDivertTarget > numFNQuantiryDivertSrc Then
                                    MsgBox("จำนวนที่จะทำการ divert breakdown" & Environment.NewLine & "จะต้องมีค่าน้อยกว่าหรือเท่ากับจำนวน ต้นทาง !!!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
                                    Me.ogvDivertDT.SetRowCellValue(numRowHandle, e.Column.FieldName.ToString, 0)
                                    CType(Me.ogdDivertDT.DataSource, System.Data.DataTable).AcceptChanges()
                                    Me.ogvDivertDT.SetFocusedRowModified()

                                Else
                                    '...update balance sub order breakdown
                                    With Me.ogvDivertBalance
                                        Dim numQuantitySrcDivert As Double
                                        numQuantitySrcDivert = Val(Me.ogvDivertSrc.GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString))
                                        .SetRowCellValue(e.RowHandle, e.Column.FieldName.ToString, numFNQuantiryDivertSrc - numQuantityDivertTarget)
                                    End With

                                    CType(Me.ogdDivertBalance.DataSource, System.Data.DataTable).AcceptChanges()

                                End If
                                Dim _Dt As DataTable = CType(Me.ogdDivertBalance.DataSource, System.Data.DataTable).Copy
                                Dim _TotalQty As Integer = 0
                                _Dt.BeginInit()
                                For Each Rx1 As DataRow In _Dt.Rows

                                    _TotalQty = 0

                                    For Each Col As DataColumn In _Dt.Columns
                                        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                                            Case "FNAmnt"
                                                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                                            Case Else

                                        End Select
                                    Next

                                    Rx1!FNTotal = _TotalQty
                                Next
                                _Dt.EndInit()
                                Me.ogdDivertBalance.DataSource = _Dt
                                Me.ogdDivertBalance.Refresh()
                                'Dim _Dt2 As DataTable

                                'With CType(Me.ogdDivertDT.DataSource, System.Data.DataTable)
                                '    .AcceptChanges()
                                '    _Dt2 = .Copy
                                'End With

                                '_Dt2.BeginInit()
                                'For Each Rx1 As DataRow In _Dt2.Rows

                                '    _TotalQty = 0

                                '    For Each Col As DataColumn In _Dt2.Columns
                                '        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                                '            Case "FNAmnt"
                                '                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                                '            Case Else

                                '        End Select
                                '    Next

                                '    Rx1!FNTotal = _TotalQty
                                'Next
                                '_Dt2.EndInit()
                                'Me.ogdDivertDT.DataSource = _Dt2
                                'Me.ogdDivertDT.Refresh()


                                With CType(Me.ogdDivertDT.DataSource, System.Data.DataTable)
                                    .AcceptChanges()
                                    .BeginInit()
                                    For Each Rx1 As DataRow In .Rows

                                        _TotalQty = 0

                                        For Each Col As DataColumn In .Columns
                                            Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                                                Case "FNAmnt"
                                                    _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                                                Case Else

                                            End Select
                                        Next

                                        Rx1!FNTotal = _TotalQty
                                    Next
                                    .EndInit()

                                    .AcceptChanges()
                                End With




                            Catch ex As Exception
                                If System.Diagnostics.Debugger.IsAttached = True Then
                                    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
                                End If

                            End Try

                        End If

                    Else
                        '...clear divert breakdown to zero and then return quantity to source factory sub order no breakdown
                        If numQuantityDivertTarget = 0 Then
                            '...clear divert breakdown to zero and then return quantity to source factory sub order no breakdown
                            '...update balance sub order breakdown
                            With Me.ogvDivertBalance
                                Dim numQuantitySrcDivert As Double
                                numQuantitySrcDivert = Val(Me.ogvDivertSrc.GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString))
                                .SetRowCellValue(e.RowHandle, e.Column.FieldName.ToString, numQuantitySrcDivert)
                            End With

                            CType(Me.ogdDivertBalance.DataSource, System.Data.DataTable).AcceptChanges()

                            Dim _Dt As DataTable = CType(Me.ogdDivertBalance.DataSource, System.Data.DataTable).Copy
                            Dim _TotalQty As Integer = 0
                            _Dt.BeginInit()
                            For Each Rx1 As DataRow In _Dt.Rows

                                _TotalQty = 0

                                For Each Col As DataColumn In _Dt.Columns
                                    Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                                        Case "FNAmnt"
                                            _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                                        Case Else

                                    End Select
                                Next

                                Rx1!FNTotal = _TotalQty
                            Next
                            _Dt.EndInit()
                            Me.ogdDivertBalance.DataSource = _Dt
                            Me.ogdDivertBalance.Refresh()

                            'Dim _Dt2 As DataTable

                            'With CType(Me.ogdDivertDT.DataSource, System.Data.DataTable)
                            '    .AcceptChanges()
                            '    _Dt2 = .Copy
                            'End With

                            '_Dt2.BeginInit()
                            'For Each Rx1 As DataRow In _Dt2.Rows

                            '    _TotalQty = 0

                            '    For Each Col As DataColumn In _Dt2.Columns
                            '        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                            '            Case "FNAmnt"
                            '                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                            '            Case Else

                            '        End Select
                            '    Next

                            '    Rx1!FNTotal = _TotalQty
                            'Next
                            '_Dt2.EndInit()
                            'Me.ogdDivertDT.DataSource = _Dt2
                            'Me.ogdDivertDT.Refresh()


                            With CType(Me.ogdDivertDT.DataSource, System.Data.DataTable)
                                .AcceptChanges()
                                .BeginInit()
                                For Each Rx1 As DataRow In .Rows

                                    _TotalQty = 0

                                    For Each Col As DataColumn In .Columns
                                        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                                            Case "FNAmnt"
                                                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                                            Case Else

                                        End Select
                                    Next

                                    Rx1!FNTotal = _TotalQty
                                Next
                                .EndInit()

                                .AcceptChanges()
                            End With


                        End If

                    End If

                End If

                If Not tmpDTSrcBreakdownDivert Is Nothing Then tmpDTSrcBreakdownDivert.Dispose()

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvDivertSrc_CustomColumnDisplayText(sender As Object, e As XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ogvDivertSrc.CustomColumnDisplayText
        Try
            With Me.ogvDivertSrc

                If .RowCount > 0 Then

                    If e.ListSourceRowIndex > -1 Then

                        Select Case Mid(e.Column.FieldName.ToString.ToUpper, 1, 6)

                            Case "FNAmnt".ToUpper

                                Select Case True

                                    Case e.Value Is Nothing
                                        e.DisplayText = "0"
                                    Case (DBNull.Value.Equals(e.Value) = True)
                                        e.DisplayText = "0"
                                    Case Else
                                        '...Nothing
                                End Select

                            Case Else
                                '...do nothing
                        End Select

                    End If

                End If

                .ActiveFilter.Clear()

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub

    Private Sub ogvDivertSrc_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvDivertSrc.RowCellStyle
        Try
            With ogvDivertSrc

                If .RowCount > 0 Then

                    If e.RowHandle > -1 Then

                        Select Case Microsoft.VisualBasic.Mid(e.Column.FieldName.ToString.ToUpper, 1, 6)

                            Case "FNAmnt".ToUpper
                                If DBNull.Value.Equals(e.CellValue) = True Then
                                    e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                                Else
                                    If Val(e.CellValue) = 0 Then
                                        e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                                    Else
                                        '...Nothing
                                    End If

                                End If

                            Case Else
                                '...Nothing
                        End Select

                    End If

                End If

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub


    Private Sub ogvDivertBalance_CustomColumnDisplayText(sender As Object, e As XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ogvDivertBalance.CustomColumnDisplayText
        Try
            With Me.ogvDivertBalance

                If .RowCount > 0 Then

                    If e.ListSourceRowIndex > -1 Then

                        Select Case Mid(e.Column.FieldName.ToString.ToUpper, 1, 6)

                            Case "FNAmnt".ToUpper

                                Select Case True

                                    Case e.Value Is Nothing
                                        e.DisplayText = "0"
                                    Case (DBNull.Value.Equals(e.Value) = True)
                                        e.DisplayText = "0"
                                    Case Else
                                        '...Nothing
                                End Select

                            Case Else
                                '...do nothing
                        End Select

                    End If

                End If

                .ActiveFilter.Clear()

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub

    Private Sub ogvDivertBalance_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvDivertBalance.RowCellStyle
        Try
            With ogvDivertBalance

                If .RowCount > 0 Then

                    If e.RowHandle > -1 Then

                        Select Case Microsoft.VisualBasic.Mid(e.Column.FieldName.ToString.ToUpper, 1, 6)

                            Case "FNAmnt".ToUpper
                                If DBNull.Value.Equals(e.CellValue) = True Then
                                    e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                                Else
                                    If Val(e.CellValue) = 0 Then
                                        e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                                    Else
                                        '...Nothing
                                    End If

                                End If

                            Case Else
                                '...Nothing
                        End Select

                    End If

                End If

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub

    Private Sub FNHSysContinentId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysContinentId.EditValueChanged
        FNHSysCountryId.Text = ""
        FNHSysProvinceId.Text = ""
    End Sub

    Private Sub FNHSysCountryId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCountryId.EditValueChanged
        FNHSysProvinceId.Text = ""
    End Sub
End Class