Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.IO
Imports DevExpress
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports System.Data.SqlClient

Public Class wDivertOrderDistination
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_MERCHAN
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private oGridViewSizeSpec As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents oRepositoryPackCartonQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit

    Private DTBeforeRevisedPackRatioHD As System.Data.DataTable
    Private DTAfterRevisedPackRatioHD As System.Data.DataTable

    Private DTBeforeRevisedPackRatioDT As System.Data.DataTable
    Private DTAfterRevisedPackRatioDT As System.Data.DataTable

    Private DTBeforeRevisedSew As System.Data.DataTable
    Private oDBdtSubOrderNoSewBF As System.Data.DataTable
    Private oDBdtSubOrderNoSewAF As System.Data.DataTable
    Private DTAfterRevisedSew As System.Data.DataTable
    Private oDBdtComponentView As DataTable
    Private oDBdtSewView As DataTable
    Private oDBdtPackView As DataTable
    Private oDBdtSizeSpecView As DataTable
    Private oDBdtSubOrderNoSizeSpecBF As System.Data.DataTable
    Private oDBdtSubOrderNoSizeSpecAF As System.Data.DataTable
    Private DTBeforeRevisedSizeSpec As System.Data.DataTable
    Private DTAfterRevisedSizeSpec As System.Data.DataTable
    Private oDBdtPostEditSizeSpec As System.Data.DataTable
    Private oDBdtAfterEditSizeSpec As System.Data.DataTable

    Private DTBeforeRevisedPack As System.Data.DataTable
    Private DTAfterRevisedPack As System.Data.DataTable
    Private oDBdtSubOrderNoPackBF As System.Data.DataTable
    Private oDBdtSubOrderNoPackAF As System.Data.DataTable

    Private oSubCompoentSchema As oSubOrerComponent

    Private oSizeSpecType As oSizeSpec
    Private _wAddDivertOrderSubComponent As wAddDivertOrderSubComponent
    Private _CopyDivert As wCopyDivertOrder
    Public Dtatb As DataTable
    Public _int As Integer
    Private Enum eTabIndexs As Integer
        FactoryOrderNo = 0
        FactorySubOrderNo = 1
        FactorySubOrderNoComponent = 2
        FactorySubOrderNoSewing = 3
        FactorySubOrderNoPacking = 4
        FactorySubOrderNoPackingCarton = 5
        FactorySubOrderSizeSpec = 6
    End Enum

#Region "Property Factory Sub Order Component"
    Private Class oSubOrerComponent

        Private _FTRemarkComponent As Integer
        Public Property FTRemarkComponent As Integer
            Get
                Return _FTRemarkComponent
            End Get
            Set(value As Integer)
                _FTRemarkComponent = value
            End Set
        End Property

    End Class

#End Region

#Region "Variable Declaration"
    Private sSQL As String
    Private _bScreenLoad As Boolean = True
    Private oGridViewBreakdownDivertSrc As DevExpress.XtraGrid.Views.Grid.GridView
    Private oGridViewBreakdownDivertBalance As DevExpress.XtraGrid.Views.Grid.GridView
    Private oGridViewBreakdownDivertDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Private WithEvents oRepositoryFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
#End Region

#Region "Property Size Specify"
    Private Class oSizeSpec

        Private _SizeSpecFTOrderNo As Integer
        Public Property SizeSpecFTOrderNo As Integer
            Get
                Return _SizeSpecFTOrderNo
            End Get
            Set(ByVal value As Integer)
                _SizeSpecFTOrderNo = value
            End Set
        End Property

        Private _SizeSpecFTSubOrderNo As Integer
        Public Property SizeSpecFTSubOrderNo As Integer
            Get
                Return _SizeSpecFTSubOrderNo
            End Get
            Set(ByVal value As Integer)
                _SizeSpecFTSubOrderNo = value
            End Set
        End Property

        Private _SizeSpecFNSeq As Integer
        Public Property SizeSpecFNSeq As Integer
            Get
                Return _SizeSpecFNSeq
            End Get
            Set(ByVal value As Integer)
                _SizeSpecFNSeq = value
            End Set
        End Property

        Private _SizeSpecFNHSysMatSizeId As Integer
        Public Property SizeSpecFNHSysMatSizeId As Integer
            Get
                Return _SizeSpecFNHSysMatSizeId
            End Get
            Set(ByVal value As Integer)
                _SizeSpecFNHSysMatSizeId = value
            End Set
        End Property

        Private _SizeSpecFTSizeSpecDesc As Integer
        Public Property SizeSpecFTSizeSpecDesc As Integer
            Get
                Return _SizeSpecFTSizeSpecDesc
            End Get
            Set(ByVal value As Integer)
                _SizeSpecFTSizeSpecDesc = value
            End Set
        End Property

        Private _SizeSpecFTSizeSpecExtension As Integer
        Public Property SizeSpecFTSizeSpecExtension As Integer
            Get
                Return _SizeSpecFTSizeSpecExtension
            End Get
            Set(ByVal value As Integer)
                _SizeSpecFTSizeSpecExtension = value
            End Set
        End Property

        Private _SizeSpecFTTolerant As Integer
        Public Property SizeSpecFTTolerant As Integer
            Get
                Return _SizeSpecFTTolerant
            End Get
            Set(value As Integer)
                _SizeSpecFTTolerant = value
            End Set
        End Property

    End Class

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

    Private _FTSubOrderSrc As String
    Public Property SubOrderSrc As String
        Get
            Return _FTSubOrderSrc
        End Get
        Set(value As String)
            _FTSubOrderSrc = value
        End Set
    End Property

    Private _FTSubOrderDivertSeq As Integer = 0
    Public Property SubOrderDivertSeq As Integer
        Get
            Return _FTSubOrderDivertSeq
        End Get
        Set(value As Integer)
            _FTSubOrderDivertSeq = value
        End Set
    End Property

    Private Shared _DTStateOrderSub As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property LoadStateOrderNoSub(ByVal paramFTOrderNo As String, ByVal paramFTOrderNoSub As String) As System.Data.DataTable
        Get
            _DTStateOrderSub = Nothing

            Dim sSQL As String
            sSQL = ""
            sSQL = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubMRP"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubProduction"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
            sSQL &= Environment.NewLine & "    AND A.FTOrderNo = B.FTOrderNo"
            sSQL &= Environment.NewLine & "           WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "		           AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'"
            sSQL &= Environment.NewLine & "	         ), '0') AS FTStateSubCutting"

            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            sSQL &= Environment.NewLine & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
            sSQL &= Environment.NewLine & "	 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D (NOLOCK) ON C.FTOrderNo = D.FTOrderNo"
            sSQL &= Environment.NewLine & "	 AND C.FTOrderProdNo = D.FTOrderProdNo"
            sSQL &= Environment.NewLine & "	 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS E (NOLOCK) ON A.FNHSysUnitSectId = E.FNHSysUnitSectId"
            sSQL &= Environment.NewLine & "  WHERE E.FTStateActive = N'1'"
            sSQL &= Environment.NewLine & "  AND E.FTStateSew = N'1'"
            sSQL &= Environment.NewLine & "  AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "	 AND D.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubSewing"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
            sSQL &= Environment.NewLine & "   WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "	  AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubPacking"

            _DTStateOrderSub = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Return _DTStateOrderSub

        End Get

    End Property

#End Region

#Region "Proc And Func"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _CopyDivert = New wCopyDivertOrder(Nothing, 0)

        HI.TL.HandlerControl.AddHandlerObj(_CopyDivert)

        Dim oSysLang As New HI.ST.SysLanguage

        Dim _oSysLang As New HI.ST.SysLanguage

        Try
            Call _oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyDivert.Name.ToString.Trim, _CopyDivert)
        Catch ex As Exception
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_CopyDivert)

        _wAddDivertOrderSubComponent = New wAddDivertOrderSubComponent
        HI.TL.HandlerControl.AddHandlerObj(_wAddDivertOrderSubComponent)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wAddDivertOrderSubComponent.Name.ToString.Trim, _wAddDivertOrderSubComponent)
        Catch ex As Exception
        End Try

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        End Try

        W_GETbSchemaMerchan()

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

        Call W_GETbSchemaMerchanSubOrderComponent()

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
    End Sub

    Private Function W_PRCbRemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
        Try

            With pGridView
                For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns.Item(nLoopColGridView).Name.ToString.ToUpper
                        Case "FTMatColorName".ToString.ToUpper, "FTMatColorCode".ToString.ToUpper
                            '...Do nothing
                            'MsgBox("GridView Column : " & pGridView.Columns(nLoopColGridView).Name.ToString.ToUpper, MsgBoxStyle.OkOnly, My.Application.Info.Title)
                            If pGridView.Name = "ogvSubOrdBreakdown" Or pGridView.Name = "ogvSubOrdBreakdownSummary" Then
                                .Columns.Remove(.Columns.Item(nLoopColGridView))
                            Else
                                '...do nothing
                            End If

                        Case "FNSeq".ToString.ToUpper, "FTSizeSpecDesc".ToString.ToUpper, "FNHSysMeasId".ToUpper, "FNHSysMeasId_None".ToUpper
                            '...do nothing
                        Case Else
                            'MsgBox("Column No. : " & nLoopColGridView)
                            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    End Select

                Next

            End With

            'pGridView.Columns.Clear()

        Catch ex As Exception
            ' MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

        Return pGridView

        'Try
        '    pGridView.Columns.Clear()
        'Catch ex As Exception
        '    MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        'End Try
        'Return pGridView

    End Function

    Private Function W_GETbSchemaMerchan() As Boolean
        Dim oDBdt As DataTable
        Dim tSql As String

        oSizeSpecType = New oSizeSpec

        Try
            tSql = ""
            tSql = "SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo, A.FNSeq, A.FNHSysMatSizeId, A.FTSizeSpecDesc, A.FTSizeSpecExtension, A.FTTolerant"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A"
            tSql &= Environment.NewLine & "WHERE 1 = 1;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdt Is Nothing Then

                Dim oConn As SqlConnection = New SqlConnection
                Dim oCmd As SqlCommand = New SqlCommand
                Dim oSchema As DataTable
                Dim oReader As SqlDataReader
                Try
                    oConn.ConnectionString = HI.Conn.DB.ConnectionString(_DBEnum)
                    If oConn.State = ConnectionState.Open Then oConn.Close()
                    oConn.Open()

                    oCmd.Connection = oConn
                    oCmd.CommandText = tSql
                    oReader = oCmd.ExecuteReader(CommandBehavior.KeyInfo)

                    If Not oReader Is Nothing Then
                        oSchema = oReader.GetSchemaTable()

                        For Each oRow As DataRow In oSchema.Rows
                            Select Case oRow.Item("ColumnName").ToString()
                                Case "FTSizeSpecDesc"
                                    oSizeSpecType.SizeSpecFTSizeSpecDesc = CInt(oRow.Item("ColumnSize"))
                                Case "FTSizeSpecExtension"
                                    oSizeSpecType.SizeSpecFTSizeSpecExtension = CInt(oRow.Item("ColumnSize"))
                                Case "FTTolerant"
                                    oSizeSpecType.SizeSpecFTTolerant = CInt(oRow.Item("ColumnSize"))
                            End Select
                        Next

                        oReader.Close()

                        HI.Conn.SQLConn.DisposeSqlConnection(oCmd)
                        HI.Conn.SQLConn.DisposeSqlConnection(oConn)

                        Return True
                    Else
                        oSizeSpecType.SizeSpecFTSizeSpecDesc = 500
                        oSizeSpecType.SizeSpecFTSizeSpecExtension = 30
                        oSizeSpecType.SizeSpecFTTolerant = 30

                        If Not oReader Is Nothing Then
                            oReader.Close()
                        End If

                        HI.Conn.SQLConn.DisposeSqlConnection(oCmd)
                        HI.Conn.SQLConn.DisposeSqlConnection(oConn)

                        Return False
                    End If

                Catch ex As Exception


                    If Not oReader Is Nothing Then
                        oReader.Close()
                    End If

                    HI.Conn.SQLConn.DisposeSqlConnection(oCmd)
                    HI.Conn.SQLConn.DisposeSqlConnection(oConn)

                    oSizeSpecType.SizeSpecFTSizeSpecDesc = 500
                    oSizeSpecType.SizeSpecFTSizeSpecExtension = 30
                    oSizeSpecType.SizeSpecFTTolerant = 30

                    Return False
                End Try

            Else
                oSizeSpecType.SizeSpecFTSizeSpecDesc = 500
                oSizeSpecType.SizeSpecFTSizeSpecExtension = 30
                oSizeSpecType.SizeSpecFTTolerant = 30

                Return False
            End If

            Return True
        Catch ex As Exception
            oSizeSpecType.SizeSpecFTSizeSpecDesc = 500
            oSizeSpecType.SizeSpecFTSizeSpecExtension = 30
            oSizeSpecType.SizeSpecFTTolerant = 30

            Return False
        End Try

    End Function

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
                        Case "FTMatColorName".ToString.ToUpper, "FTMatColorCode".ToString.ToUpper, "FTMatColorCodeNew".ToString.ToUpper, "FTNikePOLineItem".ToString.ToUpper, "FNHSysMatColorId".ToString.ToUpper, "FNTotal".ToString.ToUpper
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

    Private Function W_GETbSchemaMerchanSubOrderComponent() As Boolean

        Dim oDBdt As System.Data.DataTable
        Dim tSql As String

        oSubCompoentSchema = New oSubOrerComponent

        Try
            tSql = ""
            tSql = "SELECT TOP 1 A.[FTOrderNo], A.[FTSubOrderNo], A.[FNHSysMerMatId], A.[FTComponent], A.[FTRemark]"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Component] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE 1 = 1;"

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdt Is Nothing Then
                Dim oConn As SqlConnection = New SqlConnection
                Dim oCmd As SqlCommand = New SqlCommand
                Dim oSchema As DataTable
                Dim oReader As SqlDataReader
                Try
                    oConn.ConnectionString = HI.Conn.DB.ConnectionString(_DBEnum)
                    If oConn.State = ConnectionState.Open Then oConn.Close()
                    oConn.Open()

                    oCmd.Connection = oConn
                    oCmd.CommandText = tSql
                    oReader = oCmd.ExecuteReader(CommandBehavior.KeyInfo)

                    If Not oReader Is Nothing Then
                        oSchema = oReader.GetSchemaTable()

                        For Each oRow As DataRow In oSchema.Rows
                            Select Case oRow.Item("ColumnName").ToString()
                                Case "FTComponent"
                                    oSubCompoentSchema.FTRemarkComponent = CInt(oRow.Item("ColumnSize"))
                                Case Else
                                    '...do nothing
                            End Select
                        Next

                        oReader.Close()

                        HI.Conn.SQLConn.DisposeSqlConnection(oCmd)
                        HI.Conn.SQLConn.DisposeSqlConnection(oConn)

                        Return True
                    Else
                        oSubCompoentSchema.FTRemarkComponent = 500

                        If Not oReader Is Nothing Then
                            oReader.Close()
                        End If

                        HI.Conn.SQLConn.DisposeSqlConnection(oCmd)
                        HI.Conn.SQLConn.DisposeSqlConnection(oConn)

                        Return False
                    End If

                Catch ex As Exception


                    If Not oReader Is Nothing Then
                        oReader.Close()
                    End If

                    HI.Conn.SQLConn.DisposeSqlConnection(oCmd)
                    HI.Conn.SQLConn.DisposeSqlConnection(oConn)

                    oSubCompoentSchema.FTRemarkComponent = 500

                    Return False
                End Try

            Else
                oSubCompoentSchema.FTRemarkComponent = 500
                Return False
            End If

        Catch ex As Exception
            oSubCompoentSchema.FTRemarkComponent = 500



            Return False
        End Try

    End Function

    Private Function PROC_GETbInitSubOrderNoSrcDivert() As Boolean
        Dim bRet As Boolean = False
        Try
            sSQL = ""
            sSQL = "SELECT A.FTOrderNo, A.FTSubOrderNo"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & Me._FTOrderNoSrc & "'"
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


            Me.ogvDivertSrc.OptionsView.ShowAutoFilterRow = False
            Me.ogvDivertDT.OptionsView.ShowAutoFilterRow = False
            Me.ogvDivertBalance.OptionsView.ShowAutoFilterRow = False

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRet

    End Function


    Private Function PROC_GETbShowBrowseDataFactorySubOrderNoInfo(ByVal paramFTOrderNo As String, ByVal paramFTSubOrderNo As String) As Boolean
        Dim bRet As Boolean = False
        Dim tmpDTSubOrderNoInfo As System.Data.DataTable

        Try

            sSQL = ""
            sSQL = "SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo,"
            sSQL &= Environment.NewLine & "             A.FDSubOrderDate, A.FDProDate, A.FDShipDate,"
            sSQL &= Environment.NewLine & " 			(SELECT TOP 1 L1.FTContinentCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS L1 (NOLOCK) WHERE L1.FNHSysContinentId = A.FNHSysContinentId) AS FNHSysContinentId,"
            'sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L2.FTCountryCode  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS L2 (NOLOCK) WHERE L2.FNHSysCountryId = A.FNHSysCountryId) AS FNHSysCountryId,"
            'sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L3.FTProvinceCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS L3 (NOLOCK) WHERE L3.FNHSysProvinceId = A.FNHSysProvinceId) AS FNHSysProvinceId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L2.FTCountryCode  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS L2 (NOLOCK) WHERE L2.FNHSysCountryId = A.FNHSysCountryId AND L2.FNHSysContinentId = A.FNHSysContinentId) AS FNHSysCountryId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L3.FTProvinceCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS L3 (NOLOCK) WHERE L3.FNHSysProvinceId = A.FNHSysProvinceId AND  L3.FNHSysCountryId = A.FNHSysCountryId) AS FNHSysProvinceId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L4.FTShipModeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipMode AS L4 (NOLOCK) WHERE L4.FNHSysShipModeId = A.FNHSysShipModeId) AS FNHSysShipModeId,"
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L5.FTShipPortCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipPort AS L5 (NOLOCK) WHERE L5.FNHSysShipPortId = A.FNHSysShipPortId) AS FNHSysShipPortId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L6.FTCurCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS L6 (NOLOCK) WHERE L6.FNHSysCurId = A.FNHSysCurId) AS FNHSysCurId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L7.FTGenderCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMGender AS L7 (NOLOCK) WHERE L7.FNHSysGenderId = A.FNHSysGenderId)AS FNHSysGenderId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L8.FTUnitCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnit AS L8 (NOLOCK) WHERE L8.FNHSysUnitId = A.FNHSysUnitId)AS FNHSysUnitId,"

            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L7.FTPlantCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS L7 (NOLOCK) WHERE L7.FNHSysPlantId = A.FNHSysPlantId) AS FNHSysPlantId, "
            sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L8.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS L8 (NOLOCK) WHERE L8.FNHSysBuyGrpId = A.FNHSysBuyGrpId) AS FNHSysBuyGrpId,"

            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateEmb, '0') AS FTStateEmb,"
            sSQL &= Environment.NewLine & " 			ISNULL(A.FTStatePrint, '0') AS FTStatePrint,"
            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateHeat, '0') AS FTStateHeat,"
            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateLaser, '0') AS FTStateLaser,"
            sSQL &= Environment.NewLine & "			    ISNULL(A.FTStateWindows, 0) AS FTStateWindows"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'"

            tmpDTSubOrderNoInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not DBNull.Value.Equals(tmpDTSubOrderNoInfo) AndAlso tmpDTSubOrderNoInfo.Rows.Count > 0 Then

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

                'oStrBuilder.AppendLine("      ,(((B.FNQuantity)-ISNULL(sum(sdb.FNQuantity),0))-ISNULL(sum(SDBMI.FNQuantity),0)) AS FNGrandQuantity")
                ' oStrBuilder.AppendLine("      ,CASE WHEN (((B.FNQuantity)-ISNULL(sum(sdb.FNQuantity),0))-ISNULL(sum(SDBMI.FNQuantity),0)) > 0 THEN (((B.FNQuantity)-ISNULL(sum(sdb.FNQuantity),0))-ISNULL(sum(SDBMI.FNQuantity),0)) ELSE 0 END AS FNGrandQuantity")
                'oStrBuilder.AppendLine("     ,CASE WHEN (((B.FNQuantity)-(ISNULL(sum(sdb.FNQuantity),0))+ISNULL(sum(SDBMI.FNQuantity),0) + ISNULL(sum(DVT.FNQuantity),0)) ) > 0 THEN (((B.FNQuantity)-(ISNULL(sum(sdb.FNQuantity),0))+ISNULL(sum(SDBMI.FNQuantity),0) + ISNULL(sum(DVT.FNQuantity),0)) ) ELSE 0 END AS FNGrandQuantity")
                'oStrBuilder.AppendLine("       ,CASE WHEN (ISNULL(B.FNAmt,0) + ISNULL(B.FNQuantityExtra * B.FNPrice ,0) + ISNULL(B.FNGarmentQtyTest*B.FNPrice,0)) <=0 THEN 0 ELSE (ISNULL(B.FNAmt,0) + ISNULL(B.FNQuantityExtra * B.FNPrice ,0) + ISNULL(B.FNGarmentQtyTest*B.FNPrice,0))  END AS FNGrandAmnt ")

                oStrBuilder.AppendLine("     ,CASE WHEN  (B.FNQuantity) - (((ISNULL(sum(sdb.FNQuantity),0))+ISNULL(sum(SDBMI.FNQuantity),0) + ISNULL(sum(DVT.FNQuantity),0)) ) > 0 THEN  (B.FNQuantity)- (((ISNULL(sum(sdb.FNQuantity),0))+ISNULL(sum(SDBMI.FNQuantity),0) + ISNULL(sum(DVT.FNQuantity),0)) ) ELSE 0 END AS FNGrandQuantity")
                oStrBuilder.AppendLine("       ,CASE WHEN (ISNULL(B.FNAmt,0) + ISNULL(B.FNQuantityExtra * B.FNPrice ,0) + ISNULL(B.FNGarmentQtyTest*B.FNPrice,0)) <=0 THEN 0 ELSE (ISNULL(B.FNAmt,0) + ISNULL(B.FNQuantityExtra * B.FNPrice ,0) + ISNULL(B.FNGarmentQtyTest*B.FNPrice,0))  END AS FNGrandAmnt ")

                oStrBuilder.AppendLine("     , (C.FTMatColorNameEN + '/' + C.FTMatColorNameTH) AS FTColorExtension")
                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS B WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS C WITH(NOLOCK) ON B.FNHSysMatColorId = C.FNHSysMatColorId")
                oStrBuilder.AppendLine("LEFT OUTER JOIN (SELECT FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, SUM(FNQuantity) AS FNQuantity FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_BreakDown AS SDB WITH(NOLOCK) GROUP BY  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown ) As SDB ON b.FTSubOrderNo=sdb.FTSubOrderNo AND b.FTColorway=sdb.FTColorway AND b.FTSizeBreakDown=sdb.FTSizeBreakDown")
                oStrBuilder.AppendLine("     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS D WITH(NOLOCK) ON B.FNHSysMatSizeId = D.FNHSysMatSizeId")

                oStrBuilder.AppendLine("LEFT OUTER JOIN (SELECT A.FTOrderNo, A.FTSubOrderNo, A.FTColorway, A.FTSizeBreakDown, A.FTPOref, SUM(B.FNQuantity) AS FNQuantity  ")
                oStrBuilder.AppendLine(" FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTFactoryCMInvoice_D AS B WITH(NOLOCK)  ")
                oStrBuilder.AppendLine(" INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS A ON B.FTColorway = A.FTColorway AND B.FTSizeBreakDown = A.FTSizeBreakDown AND B.FTCustomerPO = A.FTPOref AND B.FTPOLineItem = A.FTNikePOLineItem ")
                oStrBuilder.AppendLine(" WHERE A.FTSubOrderNo = A.FTSubOrderNoRef ")
                oStrBuilder.AppendLine("GROUP BY A.FTOrderNo, A.FTSubOrderNo, A.FTColorway, A.FTSizeBreakDown, A.FTPOref ) As SDBMI ON b.FTSubOrderNo=SDBMI.FTSubOrderNo AND b.FTColorway=SDBMI.FTColorway AND b.FTSizeBreakDown=SDBMI.FTSizeBreakDown")

                oStrBuilder.AppendLine("  OUTER APPLY(SELECT  SUM(FNQuantity) AS FNQuantity FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Cancel_BreakDown AS SDB WITH(NOLOCK)  WHERE FTOrderNo= B.FTOrderNo And  FTSubOrderNo= B.FTSubOrderNo And FTColorway= B.FTColorway And FTSizeBreakDown= B.FTSizeBreakDown ) AS DVT ")


                oStrBuilder.AppendLine(" WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
                oStrBuilder.AppendLine("  AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'")

                oStrBuilder.AppendLine("group by c.FNHSysMatColorId,c.FTMatColorCode,c.FTMatColorNameTH,c.FTMatColorNameEN")
                oStrBuilder.AppendLine(",d.FNHSysMatSizeId,d.FTMatSizeCode,d.FTMatSizeNameEN")
                oStrBuilder.AppendLine(",b.FNQuantity,b.FNPrice,b.FNAmt,b.FNExtraQty")
                oStrBuilder.AppendLine(",b.FNQuantityExtra,b.FNGarmentQtyTest,B.FTNikePOLineItem,C.FNMatColorSeq,D.FNMatSizeSeq")
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
                                .Item("FTDescription") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTColorExtension")
                                .Item("FTNikePOLineItem") = tmpDTSrcBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTNikePOLineItem")
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
        Dim _Qry As String = ""
        Dim _QryCheckSeq As String = ""
        Dim bRet As Boolean = False
        Dim Maxleng As String = ""
        'Sub Order No ที่ทำการ Divert มานั้นจะต้องทำการ Copy รายการต่อไปนี้หรือไม่ ? : Component Info/ Sewing Info/ Packing Info/ Packing Carton Info/ Size Spec Info
        Try
            If Me.ogvDivertSrc.RowCount > 0 And Me.ogvDivertDT.RowCount > 0 Then

                If SubOrderDivertSeq <= 0 Then
                    _QryCheckSeq = "select top 1 Max(FNDivertSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert"
                    _QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    _QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"

                    Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1

                    _Qry = "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert] ([FTInsUser],[FDInsDate],[FTInsTime]"
                    _Qry &= vbCrLf & "              ,FNDivertSeq"
                    _Qry &= vbCrLf & "              ,[FTOrderNo],[FTSubOrderNo]"
                    _Qry &= vbCrLf & "              ,[FNHSysContinentId],[FNHSysCountryId]"
                    _Qry &= vbCrLf & "              ,[FNHSysProvinceId],[FNHSysShipModeId]"
                    _Qry &= vbCrLf & "              ,[FNHSysShipPortId]"
                    _Qry &= vbCrLf & "              ,FDShipDate"
                    _Qry &= vbCrLf & "              ,[FTRemark],[FTCustRef],[FTPORef],[FNHSysPlantId],[FNHSysBuyGrpId],[FNOrderSetType],FNPackCartonSubType,FNPackPerCarton,FTPOTrading)"
                    _Qry &= vbCrLf & "   SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
                    _Qry &= vbCrLf & " " & Maxleng & " ,"
                    _Qry &= vbCrLf & "         '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "',"
                    _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
                    _Qry &= vbCrLf & " " & Val(Me.FNHSysContinentId.Properties.Tag) & ", " & Val(Me.FNHSysCountryId.Properties.Tag) & ","
                    _Qry &= vbCrLf & "" & Val(Me.FNHSysProvinceId.Properties.Tag) & "," & Val(Me.FNHSysShipModeId.Properties.Tag) & ","
                    _Qry &= vbCrLf & "" & Val(Me.FNHSysShipPortId.Properties.Tag) & ","
                    _Qry &= vbCrLf & "'" & HI.UL.ULDate.ConvertEnDB(Me.FDShipDate.Text) & "',"
                    _Qry &= vbCrLf & "         N'" & HI.UL.ULF.rpQuoted(Me.FTRemarkSubOrderNo.Text) & "', '" & HI.UL.ULF.rpQuoted(Me.FTCustRef.Text) & "', '" & HI.UL.ULF.rpQuoted(Me.FTPORef.Text.Trim()) & "'"
                    _Qry &= vbCrLf & " ," & Val(Me.FNHSysPlantId.Properties.Tag) & ", " & Val(Me.FNHSysBuyGrpId.Properties.Tag) & "," & FNOrderSetType.SelectedIndex
                    _Qry &= vbCrLf & "," & Val(HI.TL.CboList.GetIndexByText(Me.FNPackCartonSubType.Properties.Tag.ToString, Me.FNPackCartonSubType.Text)) & "," & Val(Me.FNPackPerCaton.Value.ToString)
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTPOTrading.Text) & "' "


                Else
                    Maxleng = SubOrderDivertSeq

                    _Qry = "  UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert]  SET  "
                    _Qry &= vbCrLf & " FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & ",[FNHSysContinentId]=" & Val(Me.FNHSysContinentId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FNHSysCountryId]=" & Val(Me.FNHSysCountryId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FNHSysProvinceId]=" & Val(Me.FNHSysProvinceId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FNHSysShipModeId]=" & Val(Me.FNHSysShipModeId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FNHSysShipPortId]=" & Val(Me.FNHSysShipPortId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FDShipDate]='" & HI.UL.ULDate.ConvertEnDB(Me.FDShipDate.Text) & "'"
                    _Qry &= vbCrLf & ",[FTRemark]=N'" & HI.UL.ULF.rpQuoted(Me.FTRemarkSubOrderNo.Text) & "'"
                    _Qry &= vbCrLf & ",[FTCustRef]='" & HI.UL.ULF.rpQuoted(Me.FTCustRef.Text) & "'"
                    _Qry &= vbCrLf & ",[FTPORef]='" & HI.UL.ULF.rpQuoted(Me.FTPORef.Text) & "'"
                    _Qry &= vbCrLf & ",[FNHSysPlantId]=" & Val(Me.FNHSysPlantId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FNHSysBuyGrpId]=" & Val(Me.FNHSysBuyGrpId.Properties.Tag) & ""
                    _Qry &= vbCrLf & ",[FNOrderSetType]=" & FNOrderSetType.SelectedIndex & ""
                    _Qry &= vbCrLf & ",[FNPackCartonSubType]=" & Val(HI.TL.CboList.GetIndexByText(Me.FNPackCartonSubType.Properties.Tag.ToString, Me.FNPackCartonSubType.Text)) & ""
                    _Qry &= vbCrLf & ",[FNPackPerCarton]=" & Val(Me.FNPackPerCaton.Value.ToString) & ""
                    _Qry &= vbCrLf & ",[FTPOTrading]='" & HI.UL.ULF.rpQuoted(Me.FTPOTrading.Text) & "'"
                    _Qry &= vbCrLf & " WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "       AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "       AND FNDivertSeq =" & Val(Maxleng) & ""

                    _Qry &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert_BreakDown]    "
                    _Qry &= vbCrLf & " WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "       AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "       AND FNDivertSeq =" & Val(Maxleng) & ""
                End If

                '...iterate loop save data factory sub order no breakdown
                '==========================================================================================================================================================================================================================================================================
                Dim Maxleng2 As String = ""
                Dim _QryCheck2 As String = ""

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

                                        If Not DBNull.Value.Equals("" & Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag).ToString) Then

                                            numFNQuantityDivert = Val("" & Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag).ToString)

                                            If numFNQuantityDivert > 0 Then

                                                Dim numDivertFNHSysMatColorId As Integer
                                                Dim tTextDivertFTMatColorCode As String
                                                Dim tTextDivertFTMatColorCodeNew As String
                                                Dim numDivertFNQuantity As Double
                                                Dim numDivertFNHSysMatSizeId As Integer
                                                Dim tTextDivertFTMatSizeCode As String
                                                Dim _FTNikePOLineItem As String = ""

                                                'Dim numDivertFNGrandQuantity As Double

                                                numDivertFNHSysMatColorId = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNHSysMatColorId"))
                                                tTextDivertFTMatColorCode = Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTMatColorCode").ToString
                                                tTextDivertFTMatColorCodeNew = Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTMatColorCodeNew").ToString
                                                numDivertFNQuantity = Microsoft.VisualBasic.Conversion.Val(Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNAmnt" & tTextMatSizeTag).ToString)
                                                numDivertFNHSysMatSizeId = Microsoft.VisualBasic.Conversion.Val("" & Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FNHSysMatSizeId" & tTextMatSizeTag).ToString)
                                                tTextDivertFTMatSizeCode = Me.ogvDivertDT.Columns("FNAmnt" & tTextMatSizeTag).Caption.Trim()    'Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTMatSizeCode" & tTextMatSizeTag).ToString
                                                _FTNikePOLineItem = "" & Me.ogvDivertDT.GetRowCellValue(nLoopDivertDT, "FTNikePOLineItem").ToString

                                                'numDivertFNGrandQuantity = numDivertFNQuantity

                                                _Qry &= vbCrLf & "   INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                                                _Qry &= vbCrLf & ",FNDivertSeq"
                                                _Qry &= vbCrLf & ",[FTOrderNo],[FTSubOrderNo]"
                                                _Qry &= vbCrLf & ",[FTColorway],[FTSizeBreakDown]"
                                                _Qry &= vbCrLf & ",[FNQuantity]"
                                                _Qry &= vbCrLf & ",[FTNikePOLineItem]"
                                                _Qry &= vbCrLf & ",[FTColorwayNew])"
                                                _Qry &= vbCrLf & "   SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", "
                                                _Qry &= vbCrLf & "" & Maxleng & ","
                                                _Qry &= vbCrLf & "          N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "',"
                                                _Qry &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "',"
                                                _Qry &= vbCrLf & "   N'" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCode) & "', '" & HI.UL.ULF.rpQuoted(tTextDivertFTMatSizeCode) & "', "
                                                _Qry &= vbCrLf & "   " & Val(numDivertFNQuantity) & ""
                                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTNikePOLineItem) & "'"
                                                _Qry &= vbCrLf & ",   N'" & HI.UL.ULF.rpQuoted(tTextDivertFTMatColorCodeNew) & "'"

                                            End If

                                        Else
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


            End If


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then

                _Qry = " UPDATE M SET"
                _Qry &= vbCrLf & " FNPrice=BB.FNPrice"
                _Qry &= vbCrLf & " ,FNPriceOrg=BB.FNPriceOrg"
                _Qry &= vbCrLf & " ,FNCMDisPer=BB.FNCMDisPer"
                _Qry &= vbCrLf & " ,FNCMDisAmt=BB.FNCMDisAmt"
                _Qry &= vbCrLf & " ,FNNetPrice=BB.FNNetPrice"
                _Qry &= vbCrLf & " ,FNOperateFee=BB.FNOperateFee"
                _Qry &= vbCrLf & " ,FNOperateFeeAmt=BB.FNOperateFeeAmt"
                _Qry &= vbCrLf & " ,FNNetFOB=BB.FNNetFOB"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown  AS M INNER JOIN "
                _Qry &= vbCrLf & " (  SELECT B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & ", MAX(B.FNPrice) AS FNPrice, MAX(B.FNPriceOrg) AS FNPriceOrg, MAX(B.FNCMDisPer) AS FNCMDisPer"
                _Qry &= vbCrLf & ", MAX(B.FNCMDisAmt) AS FNCMDisAmt, MAX(B.FNNetPrice)    AS FNNetPrice,MAX(B.FTNikePOLineItem) AS FTNikePOLineItem,MAX(B.FNOperateFee) AS FNOperateFee,MAX(B.FNOperateFeeAmt) AS FNOperateFeeAmt,MAX(B.FNNetFOB) AS FNNetFOB"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo"
                _Qry &= vbCrLf & "  WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & "  AND B.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & " GROUP BY B.FTColorway, B.FTSizeBreakDown"
                _Qry &= vbCrLf & " ) AS BB ON M.FTColorway = BB.FTColorway AND M.FTSizeBreakDown=BB.FTSizeBreakDown"
                _Qry &= vbCrLf & "  WHERE M.FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= vbCrLf & "  AND M.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNoSrc) & "'"
                _Qry &= vbCrLf & "  AND M.FNDivertSeq=" & Maxleng & ""

                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                If SaveSubOrder_Component(_FTSubOrderSrc, Maxleng) = False Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If


                If SaveSubOrder_Sew(_FTSubOrderSrc, Maxleng) = False Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If


                If SaveSubOrder_Pack(_FTSubOrderSrc, Maxleng) = False Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If

                If SaveSubOrder_Bundle(_FTSubOrderSrc, Maxleng) = False Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If

                If SaveSubOrder_SizeSpec(_FTSubOrderSrc, Maxleng) = False Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If


                HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                bRet = True
                Else
                    HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If

        End Try

        If bRet = True Then
            Call SendMailToProductionStaff(_FTOrderNoSrc)
        End If

        Return bRet

    End Function

    Private Function SaveSubOrder_Component(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As Integer
            Dim _Dt As DataTable
            Maxleng = paraMaxleng



            If Not (ogdOrderSubComponent.DataSource Is Nothing) Then

                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
                _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
                _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                '_QryCheckSeq = "select FNDivertSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert"
                '_QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                '_QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"

                '_Dt = HI.Conn.SQLConn.GetDataTable(_QryCheckSeq, Conn.DB.DataBaseName.DB_MERCHAN)


                _Seq = 0
                Dim dt As DataTable
                With CType(ogdOrderSubComponent.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                For Each R As DataRow In dt.Select("FNSeq>=0", "FNSeq")
                    _Seq = _Seq + 1

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq, FNHSysMerMatId"
                    _Qry &= vbCrLf & ", FNPart, FTComponent, FTRemark, FNConSmp, FNSeq)"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "," & Maxleng
                    _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysMerMatId.ToString)
                    _Qry &= vbCrLf & ", 0"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTComponent.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                    _Qry &= vbCrLf & "," & CDbl("0" & R!FNConSmp.ToString)
                    _Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If


                    '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
                    '_Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    '_Qry &= vbCrLf & ",FNHSysMerMatId=" & CInt("0" & R!FNHSysMerMatId.ToString)
                    '_Qry &= vbCrLf & ",FTComponent='" & HI.UL.ULF.rpQuoted(R!FTComponent.ToString) & "'"
                    '_Qry &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                    '_Qry &= vbCrLf & ",FNConSmp=" & CDbl("0" & R!FNConSmp.ToString)
                    '_Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    '_Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' AND FNSeq ='" & R!FNSeq.ToString & "'"

                    'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    '    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
                    '    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq, FNHSysMerMatId"
                    '    _Qry &= vbCrLf & ", FNPart, FTComponent, FTRemark, FNConSmp, FNSeq)"
                    '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    '    _Qry &= vbCrLf & "," & Maxleng
                    '    _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysMerMatId.ToString)
                    '    _Qry &= vbCrLf & ", 0"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTComponent.ToString) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                    '    _Qry &= vbCrLf & "," & CDbl("0" & R!FNConSmp.ToString)
                    '    _Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)

                    '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '        'HI.Conn.SQLConn.Tran.Rollback()
                    '        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '        Return False
                    '    End If
                    'End If
                Next
                '_Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
                '_Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
                '_Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' AND FNSeq >" & _Seq
                'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function SaveSubOrder_Sew(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As Integer = 0
            Dim _Dt As DataTable

            Maxleng = paraMaxleng



            If Not (ogdOrderSubSewing.DataSource Is Nothing) Then

                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
                _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
                _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                'If Not (ogdOrderSubSewing.DataSource Is Nothing) Then
                '_QryCheckSeq = "select FNDivertSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert"
                '_QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                '_QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"

                '_Dt = HI.Conn.SQLConn.GetDataTable(_QryCheckSeq, Conn.DB.DataBaseName.DB_MERCHAN)
                'Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1

                _Seq = 0
                Dim dt As DataTable
                With CType(ogdOrderSubSewing.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                For Each R As DataRow In dt.Select("FNSewSeq>=0", "FNSewSeq")

                    ''Dim tImagePath As String = ""
                    ''With Me.ogvOrderSubSewing
                    ''    tImagePath = "" & .GetRowCellValue(Integer.Parse(R!FNSewSeq.ToString) - 1, "FTImage").ToString()
                    ''    If Not DBNull.Value.Equals(tImagePath) And tImagePath <> "" Then
                    ''        Me.FTImageSewing.Image = .GetRowCellValue(Integer.Parse(R!FNSewSeq.ToString) - 1, "FTImageNew")
                    ''    End If
                    ''End With

                    ''Dim tFTImageName$ = ""
                    ''If Not DBNull.Value.Equals(Me.FTImageSewing.Image) And Not Me.FTImageSewing.Image Is Nothing Then
                    ''    tFTImageName = Me.FTSubOrderNoSrc.Text & "_" & R!FNSewSeq.ToString()
                    ''    tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
                    ''    HI.UL.ULImage.SaveImage(Me.FTImageSewing, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Sewing\")
                    ''End If
                    _Seq += +1

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq,   FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "," & Maxleng
                    _Qry &= vbCrLf & "," & CInt("0" & R!FNSewSeq.ToString)
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSewDescription.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSewNote.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If



                    '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
                    '_Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    '_Qry &= vbCrLf & ",FTSewDescription='" & HI.UL.ULF.rpQuoted(R!FTSewDescription.ToString) & "'"
                    '_Qry &= vbCrLf & ",FTSewNote='" & HI.UL.ULF.rpQuoted(R!FTSewNote.ToString) & "'"
                    '_Qry &= vbCrLf & ",FTImage='" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                    '_Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    '_Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' AND FNSewSeq ='" & R!FNSewSeq.ToString & "'"

                    'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
                    '    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq,   FNSewSeq, FTSewDescription, FTSewNote, FTImage"
                    '    _Qry &= vbCrLf & ")"
                    '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    '    _Qry &= vbCrLf & "," & Maxleng
                    '    _Qry &= vbCrLf & "," & CInt("0" & R!FNSewSeq.ToString)
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSewDescription.ToString) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSewNote.ToString) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                    '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '        HI.Conn.SQLConn.Tran.Rollback()
                    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '        Return False
                    '    End If
                    'End If
                Next

            End If
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function SaveSubOrder_Pack(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As String = ""
            Dim _Dt As DataTable
            Maxleng = paraMaxleng



            If Not (ogdOrderSubPack.DataSource Is Nothing) Then

                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
                _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
                _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                '_QryCheckSeq = "select top 1 Max(FNDivertSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert"
                '_QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                '_QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"

                '_Dt = HI.Conn.SQLConn.GetDataTable(_QryCheckSeq, Conn.DB.DataBaseName.DB_MERCHAN)


                'Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1
                _Seq = 0
                Dim dt As DataTable
                With CType(ogdOrderSubPack.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                For Each R As DataRow In dt.Select("FNPackSeq>=0", "FNPackSeq")
                    'Dim tFTImageName$ = ""
                    'If Not DBNull.Value.Equals(Me.FTImagePacking.Image) And Not Me.FTImagePacking.Image Is Nothing Then
                    '    tFTImageName = Me.FTSubOrderNoSrc.Text & "_" & R!FNPackSeq.ToString()
                    '    tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
                    'End If
                    'HI.UL.ULImage.SaveImage(Me.FTImagePacking, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Packing\")
                    _Seq = _Seq + 1

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
                    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq,   FNPackSeq, FTPackDescription, FTPackNote, FTImage"
                    _Qry &= vbCrLf & ")"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    _Qry &= vbCrLf & "," & Maxleng
                    _Qry &= vbCrLf & "," & CInt("0" & R!FNPackSeq.ToString)
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If


                    '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
                    '_Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    '_Qry &= vbCrLf & ",FTPackDescription='" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                    '_Qry &= vbCrLf & ",FTPackNote='" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                    '_Qry &= vbCrLf & ",FTImage='" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                    '_Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    '_Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' AND FNPackSeq ='" & R!FNPackSeq.ToString & "'"

                    'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
                    '    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNDivertSeq,   FNPackSeq, FTPackDescription, FTPackNote, FTImage"
                    '    _Qry &= vbCrLf & ")"
                    '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                    '    _Qry &= vbCrLf & "," & Maxleng
                    '    _Qry &= vbCrLf & "," & CInt("0" & R!FNPackSeq.ToString)
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackDescription.ToString) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPackNote.ToString) & "'"
                    '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTImage.ToString) & "'"
                    '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '        HI.Conn.SQLConn.Tran.Rollback()
                    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '        Return False
                    '    End If
                    'End If
                Next

            End If
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function SaveSubOrder_Bundle(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As String = ""
            Dim _Dt As DataTable

            Dim oDBdtSizeBreakdown As DataTable
            Dim tSql As String
            Maxleng = paraMaxleng
            '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert"
            '_Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '_Qry &= vbCrLf & ",FNPackCartonSubType= " & Val(HI.TL.CboList.GetIndexByText(Me.FNPackCartonSubType.Properties.Tag.ToString, Me.FNPackCartonSubType.SelectedItem.ToString))
            '_Qry &= vbCrLf & ",FNPackPerCarton= " & Val(Me.FNPackPerCaton.Value.ToString)
            '_Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
            '_Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "'"

            'HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            If Not (ogdOrderSubPackCarton.DataSource Is Nothing) Then




                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Bundle"
                _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
                _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                '_QryCheckSeq = "select top 1 Max(FNDivertSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert"
                '_QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                '_QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"

                '_Dt = HI.Conn.SQLConn.GetDataTable(_QryCheckSeq, Conn.DB.DataBaseName.DB_MERCHAN)

                'Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1



                _Qry = ""
                _Qry = "UPDATE A"
                _Qry &= Environment.NewLine & "SET   A.[FNPackCartonSubType] = " & Val(HI.TL.CboList.GetIndexByText(Me.FNPackCartonSubType.Properties.Tag.ToString, Me.FNPackCartonSubType.Text)) & ","
                _Qry &= Environment.NewLine & "      A.[FNPackPerCarton] = " & Val(Me.FNPackPerCaton.Value.ToString)
                _Qry &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert] AS A"
                _Qry &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                _Qry &= Environment.NewLine & "       AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"

                _Qry &= Environment.NewLine & "       AND A.FNDivertSeq = '" & Val(Maxleng) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Seq = 0
                Dim dt As DataTable
                With CType(ogdOrderSubPackCarton.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                If HI.TL.CboList.GetIndexByText(Me.FNPackCartonSubType.Properties.Tag.ToString, Me.FNPackCartonSubType.Text) > 0 Then



                    For Each oDataRow As System.Data.DataRow In CType(Me.ogdOrderSubPackCarton.DataSource, System.Data.DataTable).Rows
                        Dim txFTOrderNo As String
                        Dim txFTSubOrderNo As String
                        Dim txFTColorway As String

                        txFTOrderNo = _FTOrderNoSrc
                        txFTSubOrderNo = paraFTSubOrderNoSrc
                        txFTColorway = oDataRow("FTColorway").ToString

                        Dim sColumnSizeBreakdown As String
                        sColumnSizeBreakdown = ""

                        Dim numFNQuantity As Integer
                        numFNQuantity = 0

                        For Each oColPackCarton As System.Data.DataColumn In CType(Me.ogdOrderSubPackCarton.DataSource, System.Data.DataTable).Columns
                            Select Case oColPackCarton.ColumnName.ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                                    '...Nothing
                                Case Else
                                    sColumnSizeBreakdown = oColPackCarton.ColumnName.ToString             '...FTSizeBreakdWon
                                    numFNQuantity = Val(oDataRow(oColPackCarton.ColumnName.ToString).ToString)   '...FNQuantity by FTColorway, FTSizeBreakdown

                                    '...add new record
                                    sSQL = ""
                                    sSQL = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_Divert_Bundle] ([FTInsUser], [FDInsDate], [FTInsTime], [FTOrderNo], [FTSubOrderNo], FNDivertSeq,[FTColorway], [FTSizeBreakDown], [FNQuantity])"
                                    sSQL &= Environment.NewLine & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ", '" & HI.UL.ULF.rpQuoted(txFTOrderNo) & "', '" & HI.UL.ULF.rpQuoted(txFTSubOrderNo) & "'," & Maxleng & ", '" & HI.UL.ULF.rpQuoted(txFTColorway) & "', '" & HI.UL.ULF.rpQuoted(sColumnSizeBreakdown) & "' ," & numFNQuantity

                                    If HI.Conn.SQLConn.Execute_Tran(sSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        'HI.Conn.SQLConn.Tran.Rollback()
                                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Return False
                                    End If


                            End Select

                        Next

                    Next

                End If

            End If
        Catch ex As Exception
        End Try
        Return True
    End Function



    Private Function SaveSubOrder_SizeSpec(ByVal paraFTSubOrderNoSrc As String, ByVal paraMaxleng As Integer) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _QryCheckSeq As String = ""
            Dim bRet As Boolean = False
            Dim _Seq As Integer = 0
            Dim _ogc As Object
            Dim Maxleng As String = ""
            Dim _Dt As DataTable

            Dim oDBdtSizeBreakdown As DataTable
            Dim tSql As String
            Maxleng = paraMaxleng



            If Not (ogdSizeSpec.DataSource Is Nothing) Then
                '_QryCheckSeq = "select top 1 Max(FNDivertSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert"
                '_QryCheckSeq &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                '_QryCheckSeq &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"

                '_Dt = HI.Conn.SQLConn.GetDataTable(_QryCheckSeq, Conn.DB.DataBaseName.DB_ACCOUNT)

                'Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_QryCheckSeq, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1



                tSql = ""
                tSql = "SELECT A.FNHSysMatSizeId, A.FTMatSizeCode, A.FTMatSizeNameEN AS FTMatSizeName"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE  EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "               WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                tSql &= Environment.NewLine & "                     AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                tSql &= Environment.NewLine & "                     AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)"
                tSql &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq ASC;"

                oDBdtSizeBreakdown = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
                _Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "' "
                _Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' "
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Seq = 0
                Dim dt As DataTable
                With CType(ogdSizeSpec.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                For Each R As DataRow In dt.Rows
                    Dim _MeasId As Integer = 0
                    _MeasId = Val(R!FNHSysMeasId_Hide.ToString)
                    For Each oRow As DataRow In oDBdtSizeBreakdown.Rows
                        _Seq = _Seq + 1
                        Dim DivertMatSizeId As Integer
                        Dim DivertSizeSpecExtension As String

                        DivertMatSizeId = oRow!FNHSysMatSizeId
                        'DivertMatSizeId = Integer.Parse(Me.ogvSizeSpec.GetRowCellValue(CInt("0" & R!FNSeq.ToString) - 1, "FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString()).ToString)
                        DivertSizeSpecExtension = Me.ogvSizeSpec.GetRowCellValue(CInt("0" & R!FNSeq.ToString) - 1, "FTSizeSpecExtension" & oRow.Item("FTMatSizeCode").ToString()).ToString

                        If (R!FTSizeSpecDesc.ToString() <> "") Then


                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNDivertSeq, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant, FNHSysMeasId"
                            _Qry &= vbCrLf & ")"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                            _Qry &= vbCrLf & "," & Maxleng
                            _Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)
                            _Qry &= vbCrLf & "," & DivertMatSizeId
                            _Qry &= vbCrLf & ",'" & R!FTSizeSpecDesc.ToString & "'"
                            _Qry &= vbCrLf & ",'" & DivertSizeSpecExtension & "'"
                            _Qry &= vbCrLf & ",'" & R!FTSizeSpecTolerant.ToString & "'"
                            _Qry &= vbCrLf & "," & _MeasId
                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                'HI.Conn.SQLConn.Tran.Rollback()
                                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                            '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
                            '_Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            ''_Qry &= vbCrLf & ",FNHSysMatSizeId=" & DivertMatSizeId
                            '_Qry &= vbCrLf & ",FTSizeSpecDesc='" & (R!FTSizeSpecDesc.ToString) & "'"
                            '_Qry &= vbCrLf & ",FTSizeSpecExtension='" & DivertSizeSpecExtension & "'"
                            '_Qry &= vbCrLf & ",FTTolerant='" & (R!FTSizeSpecTolerant.ToString) & "'"
                            '_Qry &= vbCrLf & ",FNHSysMeasId=" & _MeasId
                            '_Qry &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                            '_Qry &= vbCrLf & " AND FNDivertSeq = '" & Val(Maxleng) & "' AND FNSeq ='" & R!FNSeq.ToString & "' AND FNHSysMatSizeId ='" & DivertMatSizeId & "'"

                            '_Seq = Integer.Parse(R!FNSeq.ToString)

                            'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            '    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
                            '    _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNDivertSeq, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant, FNHSysMeasId"
                            '    _Qry &= vbCrLf & ")"
                            '    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            '    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(paraFTSubOrderNoSrc) & "'"
                            '    _Qry &= vbCrLf & "," & Maxleng
                            '    _Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)
                            '    _Qry &= vbCrLf & "," & DivertMatSizeId
                            '    _Qry &= vbCrLf & ",'" & R!FTSizeSpecDesc.ToString & "'"
                            '    _Qry &= vbCrLf & ",'" & DivertSizeSpecExtension & "'"
                            '    _Qry &= vbCrLf & ",'" & R!FTSizeSpecTolerant.ToString & "'"
                            '    _Qry &= vbCrLf & "," & _MeasId
                            '    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            '        HI.Conn.SQLConn.Tran.Rollback()
                            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            '        Return False
                            '    End If
                            'End If
                        End If
                    Next
                Next

            End If
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Sub SendMailToProductionStaff(OrderNoKey As String)
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _UserMailTo As String

        _Qry = "   SELECT DISTINCT A.FTUserName "
        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS B WITH(NOLOCK) ON A.FNHSysTeamGrpId = B.FNHSysTeamGrpId INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS C WITH(NOLOCK) ON A.FTUserName = C.FTUserName INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS D WITH(NOLOCK) ON C.FNHSysPermissionID = D.FNHSysPermissionID INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON D.FNHSysCmpId = O.FNHSysCmpId INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission AS SCUT WITH(NOLOCK) ON C.FNHSysPermissionID = SCUT.FNHSysPermissionID"
        _Qry &= vbCrLf & " WHERE    (B.FTStateProd = '1') "
        _Qry &= vbCrLf & " AND (O.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(OrderNoKey) & "') "
        _Qry &= vbCrLf & " AND (SCUT.FTStateStaff = '1')"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

        For Each R As DataRow In _dt.Rows
            _UserMailTo = R!FTUserName.ToString

            If _UserMailTo <> "" Then

                Dim tmpsubject As String = ""
                Dim tmpmessage As String = ""

                tmpsubject = "Divert Factory Order No " & OrderNoKey & " Sub Factory Order No " & FTSubOrderNoSrc.Text
                tmpmessage = "Divert Factory Order No " & OrderNoKey & " Sub Factory Order No " & FTSubOrderNoSrc.Text
                tmpmessage &= vbCrLf & "Ship Date :" & Me.FDShipDate.Text
                tmpmessage &= vbCrLf & "Continent :" & Me.FNHSysContinentId.Text
                tmpmessage &= vbCrLf & "Country :" & Me.FNHSysCountryId.Text
                tmpmessage &= vbCrLf & "Province :" & Me.FNHSysProvinceId.Text
                tmpmessage &= vbCrLf & "Ship Mode :" & Me.FNHSysShipModeId.Text
                tmpmessage &= vbCrLf & "Ship Port :" & Me.FNHSysShipPortId.Text
                tmpmessage &= vbCrLf & "Note :" & Me.FTRemarkSubOrderNo.Text

                If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 9, OrderNoKey & "|" & Me.FTSubOrderNoSrc.Text) Then


                End If

            End If
        Next

        _dt.Dispose()
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
                tmpDTDetailBreakdownDivert.Columns.Add("FTMatColorCodeNew", GetType(String))
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

                If HI.ST.Lang.Language = HI.ST.Lang.eLang.EN Then
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameEN AS FTMatColorName")
                ElseIf HI.ST.Lang.Language = HI.ST.Lang.eLang.TH Then
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameTH AS FTMatColorName")
                Else
                    oStrBuilder.AppendLine("SELECT C.FNHSysMatColorId, C.FTMatColorCode, C.FTMatColorNameEN AS FTMatColorName")
                End If

                oStrBuilder.AppendLine("      ,ISNULL(B.FTNikePOLineItem,'') AS FTNikePOLineItem")
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
                                .Item("FTMatColorCodeNew") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatColorCode")
                                .Item("FTMatColorName") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTMatColorName")
                                .Item("FTDescription") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTColorExtension")
                                .Item("FTNikePOLineItem") = tmpDTDetailBreakdwonDivertInfo.Rows(nLoopBreakdownInfo).Item("FTNikePOLineItem")
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

    Private Function PROC_GETSubOrderNoInfo(ByVal paramFTOrderNo As String, ByVal paramFTSubOrderNo As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _pDt As New DataTable

            'Load component data from Sub Order or Divert
            _Qry = "SELECT CONVERT(INT, A.FNSeq) AS FNSeq, A.FNSeq AS FNSeqOrg, A.FNPart, B.FTMainMatCode,"
            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.TH
                    _Qry &= vbCrLf & "       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameTH) AS FTMainMatDesc"
                Case Else
                    _Qry &= vbCrLf & "       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameEN) AS FTMainMatDesc"
            End Select
            _Qry &= vbCrLf & "       , A.FNConSmp"
            _Qry &= vbCrLf & "       , ISNULL(A.FTComponent, '') AS FTComponent, ISNULL(A.FTRemark, '') AS FTRemark"
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component As A With(NOLOCK) "
            _Qry &= vbCrLf & "LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TMERMMainMat] As B With(NOLOCK) On A.FNHSysMerMatId = B.FNHSysMainMatId"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)
            _Qry &= vbCrLf & "ORDER BY A.FNSeq ASC;"

            _pDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            If (_pDt.Rows.Count > 0) Then
                Me.ogdOrderSubComponent.DataSource = _pDt
                Me.ogdOrderSubComponent.Refresh()
                Me.ogvOrderSubComponent.RefreshData()
                Me.ogvOrderSubComponent.OptionsView.ColumnAutoWidth = False
            ElseIf (SubOrderDivertSeq = 0) Then
                Call W_PRCbShowBrowseDataSubOrderComponent(Me._FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
            End If

            'Load divert sew data from Sub order or divert
            _Qry = "  Select FNSewSeq, FTSewDescription, FTSewNote, FTImage"
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew With (NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)
            _Qry &= vbCrLf & "ORDER BY FNSewSeq ASC;"

            'fill gridview by datatable
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            _oDt.Columns.Add("FTImageNew", GetType(Object))
            If (_oDt.Rows.Count > 0) Then
                For Each oRow As DataRow In _oDt.Rows
                    Dim tImagePath As String = oRow.Item("FTImage").ToString()

                    Dim tPathImgDis As String = tW_SysPath & "\OrderNo\SubOrderNo\Sewing" & "\" & tImagePath
                    If IO.File.Exists(tPathImgDis) Then
                        oRow!FTImageNew = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))
                    End If
                Next

                FNSewSeq.Value = _oDt.Rows.Count + 1
                Me.ogdOrderSubSewing.DataSource = _oDt
                Me.ogvOrderSubSewing.RefreshData()
                Me.ogvOrderSubSewing.OptionsView.ColumnAutoWidth = False
                Me.ogvOrderSubSewing.BestFitColumns()
            ElseIf (SubOrderDivertSeq = 0) Then
                Call W_PRCbShowBrowseDataSubOrderSewingInfo()
            End If
            FNSewSeq.Value = _oDt.Rows.Count + 1

            'load pack data from sub order or divert
            _Qry = "  Select FNPackSeq, FTPackDescription, FTPackNote, FTImage, FBImage"
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack With (NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)
            _Qry &= vbCrLf & "ORDER BY FNPackSeq ASC;"

            _pDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            _pDt.Columns.Add("FTImageNew", GetType(Object))
            If (_pDt.Rows.Count > 0) Then
                For Each oRow As DataRow In _pDt.Rows
                    Dim tImagePath As String = oRow.Item("FTImage").ToString()
                    Dim tPathImgDis As String = tW_SysPath & "\OrderNo\SubOrderNo\Packing" & "\" & tImagePath
                    If IO.File.Exists(tPathImgDis) Then
                        oRow!FTImageNew = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))
                    End If

                Next

                FNPackSeq.Value = _pDt.Rows.Count + 1
                Me.ogdOrderSubPack.DataSource = _pDt
                Me.ogvOrderSubPack.RefreshData()
                Me.ogvOrderSubPack.OptionsView.ColumnAutoWidth = False
                Me.ogvOrderSubPack.BestFitColumns()
            ElseIf (SubOrderDivertSeq = 0) Then
                Call W_PRCbShowBrowseDataSubOrderPackInfo()
            End If
            FNPackSeq.Value = _pDt.Rows.Count + 1

            'load bundle data
            Call PROC_SETxShowBrowsePackingCartonInfo()
            Call DivertPackingCartonInfo(paramFTOrderNo, paramFTSubOrderNo)
            'load size spec data
            Call W_PRCbShowBrowseDataSubOrderDivertSizeSpecInfo(paramFTOrderNo, paramFTSubOrderNo)
            _pDt = Me.ogdSizeSpec.DataSource
            If (_pDt.Rows.Count = 0) And SubOrderDivertSeq = 0 Then
                Call W_PRCbShowBrowseDataSubOrderSizeSpecInfo(paramFTOrderNo, paramFTSubOrderNo)
            End If
            'Call DivertSizeSpecInfo(paramFTOrderNo, paramFTSubOrderNo)

        Catch ex As Exception
        End Try
    End Function

    Private Function DivertPackingCartonInfo(ByVal paramFTOrderNo As String, ByVal paramFTSubOrderNo As String)
        Dim _Dt As DataTable
        Dim _oDt As DataTable
        Dim _Qry As String = ""

        Dim sSQL As String

        sSQL = ""
        sSQL = "SELECT TOP 1 ISNULL(A.FNPackCartonSubType, 0) AS FNPackCartonSubType, ISNULL(A.FNPackPerCarton, 0) AS FNPackPerCarton"
        sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert AS A (NOLOCK)"
        sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
        sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSrc.Text.Trim) & "' AND FNDivertSeq =" & (SubOrderDivertSeq) & ";"

        Dim tmpPackCartonSubType As System.Data.DataTable

        tmpPackCartonSubType = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        'Me.FNPackCartonSubType.SelectedIndex = 0
        'Me.FNPackPerCaton.Value = 0

        For Each Rxt As DataRow In tmpPackCartonSubType.Rows
            Try
                Me.FNPackCartonSubType.SelectedIndex = Val(Rxt.Item("FNPackCartonSubType"))
                Me.FNPackPerCaton.Value = Val(Rxt.Item("FNPackPerCarton").ToString)
            Catch ex As Exception
                Me.FNPackCartonSubType.SelectedIndex = 0
                Me.FNPackPerCaton.Value = 0
            End Try
        Next

        _Qry = "  Select FTColorway, FTSizeBreakDown, FNQuantity"
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Bundle With (NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _oDt = ogdOrderSubPackCarton.DataSource
        For Each R As DataRow In _oDt.Rows
            For Each Col As DataColumn In _oDt.Columns
                'MessageBox.Show(R!FTColorway.ToString())
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                    Case Else
                        For Each _R As DataRow In _Dt.Rows
                            If (R!FTColorway = _R!FTColorway) Then
                                If (Col.ToString = _R!FTSizeBreakDown.ToString) Then
                                    R(Col) = Integer.Parse(_R!FNQuantity.ToString)
                                End If
                            End If
                        Next
                End Select
            Next
        Next
    End Function

    Private Function DivertSizeSpecInfo(ByVal paramFTOrderNo As String, ByVal paramFTSubOrderNo As String)
        Dim _Dt As DataTable
        Dim _oDt As DataTable
        Dim _Qry As String = ""
        Dim extenname As String = ""
        Dim rowcount As Integer = 0
        Dim _Seq As Integer = 0

        _Qry = "  Select FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant, FNHSysMeasId"
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec With (NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "' AND FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _oDt = ogdSizeSpec.DataSource
        rowcount = _oDt.Rows.Count
        For Each R As DataRow In _oDt.Rows
            For Each Col As DataColumn In _oDt.Columns
                Dim _size As String = ""
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNHSysMeasId_hide".ToUpper, "FNHSysMatSizeId".ToUpper, "FTSizeSpecDesc".ToUpper, "FNHSysMeasId".ToUpper, "FNSeq".ToUpper, extenname.ToUpper, "FTSizeSpecTolerant".ToUpper
                    Case Else
                        '_size.Count
                        If (Col.ToString.Count = 16) Then
                            _size = Col.ToString.ToUpper.Substring(15, 1)
                        Else
                            _size = Col.ToString.ToUpper.Substring(15, 2)
                        End If
                        'MessageBox.Show(_size)
                        extenname = "FTSizeSpecExtension" & _size
                        For Each _R As DataRow In _Dt.Rows
                            _Seq = Integer.Parse(_R!FNSeq.ToString)

                            If (_Seq > _oDt.Rows.Count And R!FNSeq.ToString = _oDt.Rows.Count) Then
                                MessageBox.Show("16")
                            End If

                            If (R!FNSeq.ToString = _oDt.Rows.Count And (Integer.Parse(_R!FNSeq.ToString) > Integer.Parse(R!FNSeq.ToString()))) Then
                                Call W_PRCxInitNewRowSizeSpec(CType(ogdSizeSpec.DataSource, System.Data.DataTable))
                            End If

                            If (R!FNSeq.ToString = _R!FNSeq.ToString) Then
                                R!FTSizeSpecDesc = _R!FTSizeSpecDesc
                                If (R("FNHSysMatSizeId" & _size).ToString = _R!FNHSysMatSizeId.ToString) Then
                                    R(extenname) = _R!FTSizeSpecExtension
                                    R!FTSizeSpecTolerant = _R!FTTolerant
                                    If (R!FNSeq.ToString <> rowcount) Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                End Select
            Next
        Next
    End Function

    Private Function W_PRCbShowBrowseDataSubOrderPackInfo() As Boolean
        Try
            HI.TL.HandlerControl.ClearControl(Me.ogbSubOrderPackInfo)

            Dim tSql As String

            tSql = ""
            tSql = "SELECT ISNULL(MAX(A.FNPackSeq),0) + 1"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"

            Dim nFNPackSeqDefault As Integer
            nFNPackSeqDefault = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            Me.FNPackSeq.Value = nFNPackSeqDefault

            Dim oDBdtSubOrderPack As DataTable

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("SELECT A.FNPackSeq, A.FTPackDescription, A.FTPackNote, A.FTImage,A.FBImage")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)")
            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'")
            oStrBuilder.AppendLine("ORDER BY A.FNPackSeq ASC;")

            oDBdtSubOrderPack = HI.Conn.SQLConn.GetDataTable(oStrBuilder.ToString(), HI.Conn.DB.DataBaseName.DB_MERCHAN)

            oDBdtSubOrderPack.Columns.Add("FTImageNew", GetType(Object))

            For Each oRow As DataRow In oDBdtSubOrderPack.Rows
                Dim tImagePath As String = oRow.Item("FTImage").ToString()
                Dim tPathImgDis As String = tW_SysPath & "\OrderNo\SubOrderNo\Packing" & "\" & tImagePath
                If IO.File.Exists(tPathImgDis) Then
                    oRow!FTImageNew = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))
                End If

            Next

            ogvOrderSubPack.ActiveFilter.Clear()

            Me.ogdOrderSubPack.DataSource = oDBdtSubOrderPack
            Me.ogvOrderSubPack.RefreshData()
            Me.ogvOrderSubPack.OptionsView.ColumnAutoWidth = False
            Me.ogvOrderSubPack.BestFitColumns()

            '...State Approved Factory Sub Order No. {Approved/Revised} Packing Sequence Information
            '============================================================================================================================================================================================================================================================================================================================================
            tSql = ""
            tSql = "DECLARE @FTStateApprovedPacking AS NVARCHAR(1);"
            tSql &= Environment.NewLine & "SELECT TOP 1 @FTStateApprovedPacking = A.FTStateApprovedPacking FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"
            tSql &= Environment.NewLine & "IF (@FTStateApprovedPacking IS NULL)"
            tSql &= Environment.NewLine & "BEGIN"
            tSql &= Environment.NewLine & "  SET @FTStateApprovedPacking = N'2' /*not approved & not revised*/"
            tSql &= Environment.NewLine & "END;"
            tSql &= Environment.NewLine & "PRINT '@FTStateApprovedPacking : ' + @FTStateApprovedPacking;"
            tSql &= Environment.NewLine & "SELECT @FTStateApprovedPacking AS FTStateApprovedPacking;"

            Dim tTextFTApprovedInfoState As String

            tTextFTApprovedInfoState = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "2")

            '============================================================================================================================================================================================================================================================================================================================================

            Return True
        Catch ex As Exception
            'Throw New Exception(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace().ToString())

            Return False
        End Try

    End Function

    Private Sub PROC_SETxShowBrowsePackingCartonInfo()

        If Me.FTSubOrderNoSrc.Text.Trim <> "" Then
            Dim sSQL As String

            sSQL = ""
            sSQL = "SELECT TOP 1 ISNULL(A.FNPackCartonSubType, 0) AS FNPackCartonSubType, ISNULL(A.FNPackPerCarton, 0) AS FNPackPerCarton"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSrc.Text.Trim) & "';"

            Dim tmpPackCartonSubType As System.Data.DataTable

            tmpPackCartonSubType = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.FNPackCartonSubType.SelectedIndex = 0
            Me.FNPackPerCaton.Value = 0

            For Each Rxt As DataRow In tmpPackCartonSubType.Rows
                Try
                    Me.FNPackCartonSubType.SelectedIndex = Val(Rxt.Item("FNPackCartonSubType"))
                    Me.FNPackPerCaton.Value = Val(Rxt.Item("FNPackPerCarton").ToString)
                Catch ex As Exception
                    Me.FNPackCartonSubType.SelectedIndex = 0
                    Me.FNPackPerCaton.Value = 0
                End Try
            Next
            'If Not DBNull.Value.Equals(tmpPackCartonSubType) AndAlso tmpPackCartonSubType.Rows.Count > 0 Then
            '    If CInt(tmpPackCartonSubType.Rows(0).Item("FNPackCartonSubType")) > -1 Then

            '        Try
            '            Me.FNPackCartonSubType.SelectedIndex = Val(tmpPackCartonSubType.Rows(0).Item("FNPackCartonSubType"))
            '            Me.FNPackPerCaton.Value = Val(tmpPackCartonSubType.Rows(0).Item("FNPackPerCarton").ToString)
            '        Catch ex As Exception
            '            Me.FNPackCartonSubType.SelectedIndex = 0
            '            Me.FNPackPerCaton.Value = 0
            '        End Try

            '    End If
            'Else
            '    Me.FNPackCartonSubType.SelectedIndex = 0
            '    Me.FNPackPerCaton.Value = 0
            'End If

            '...State Approved Factory Sub Order No. {Approved/Revised} Packing Carton Information
            '============================================================================================================================================================================================================================================================================================================================================
            sSQL = ""
            sSQL = "DECLARE @FTStateApprovedPackRatio AS NVARCHAR(1);"
            sSQL &= Environment.NewLine & "SELECT TOP 1 @FTStateApprovedPackRatio = A.FTStateApprovedPackRatio FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSrc.Text.Trim) & "';"
            sSQL &= Environment.NewLine & "IF (@FTStateApprovedPackRatio IS NULL)"
            sSQL &= Environment.NewLine & "BEGIN"
            sSQL &= Environment.NewLine & "  SET @FTStateApprovedPackRatio = N'2' /*not approved & not revised*/"
            sSQL &= Environment.NewLine & "END;"
            sSQL &= Environment.NewLine & "PRINT '@FTStateApprovedPackRatio : ' + @FTStateApprovedPackRatio;"
            sSQL &= Environment.NewLine & "SELECT @FTStateApprovedPackRatio AS FTStateApprovedPackRatio;"

            Dim tTextFTApprovedInfoState As String

            tTextFTApprovedInfoState = HI.Conn.SQLConn.GetField(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN, "2")
            '============================================================================================================================================================================================================================================================================================================================================

        Else
            '...nothing
            'Me.FNPackCartonSubType.SelectedIndex = 0
            'Me.FNPackPerCaton.Value = 0
        End If

        Call PROC_InitGridViewSubOrerPackCartonInfo()
        Call W_PRCbShowBrowseDataSubOrderPackCartonInfo(Me._FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
        'HI.TL.HandlerControl.ClearControl(Me.ogbPackCartonHD)

    End Sub

    Private Sub PROC_InitGridViewSubOrerPackCartonInfo()
        Try
            With Me.ogvOrderSubPackCarton
                For nLoopGridviewSubPackCarton As Integer = .Columns.Count - 1 To 0 Step -1
                    .Columns(nLoopGridviewSubPackCarton).OptionsColumn.AllowEdit = False
                    .Columns(nLoopGridviewSubPackCarton).AppearanceCell.BackColor = Color.White
                    .Columns(nLoopGridviewSubPackCarton).AppearanceCell.ForeColor = Color.Black
                    .Columns(nLoopGridviewSubPackCarton).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                Next
            End With
        Catch ex As Exception
        End Try

        With ogvOrderSubPackCarton
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

    End Sub

    Private Function W_PRCbShowBrowseDataSubOrderPackCartonInfo(ByVal paramFTOrderNo As String, ByVal paramFTSubOrderNo As String) As Boolean
        Dim bRet As Boolean = False

        Me.ogdOrderSubPackCarton.DataSource = Nothing

        With Me.ogvOrderSubPackCarton
            For nLoopPackCarton As Integer = .Columns.Count - 1 To 0 Step -1

                Select Case .Columns(nLoopPackCarton).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(nLoopPackCarton).AppearanceCell.BackColor = Color.White
                        .Columns(nLoopPackCarton).AppearanceCell.ForeColor = Color.Black
                        .Columns(nLoopPackCarton).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(nLoopPackCarton))
                End Select

            Next

        End With

        If paramFTOrderNo.Trim <> "" Or paramFTSubOrderNo.Trim <> "" Then

            Try
                Dim sSQL As String
                sSQL = ""

                If Me.SubOrderDivertSeq <= 0 Then
                    sSQL = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..USP_GET_SUBORDERNOBREAKDOWN_PACKCARTON N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo.Trim) & "', N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo.Trim) & "';"
                Else
                    sSQL = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..USP_GET_SUBORDERNOBREAKDOWN_PACKCARTON_Divert N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo.Trim) & "', N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo.Trim) & "'," & Me.SubOrderDivertSeq & ";"
                End If


                Dim tmpBreakdownPackCarton As System.Data.DataTable

                tmpBreakdownPackCarton = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                Dim _colcount As Integer = 0

                With Me.ogvOrderSubPackCarton

                    For nLoopGridveiwSubPack As Integer = .Columns.Count - 1 To 0 Step -1

                        Select Case .Columns(nLoopGridveiwSubPack).FieldName.ToString.ToUpper

                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                                .Columns(nLoopGridveiwSubPack).AppearanceCell.BackColor = Color.White
                                .Columns(nLoopGridveiwSubPack).AppearanceCell.ForeColor = Color.Black
                                .Columns(nLoopGridveiwSubPack).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                            Case Else
                                .Columns.Remove(.Columns(nLoopGridveiwSubPack))
                        End Select

                    Next

                    If Not (tmpBreakdownPackCarton Is Nothing) Then
                        For Each Col As DataColumn In tmpBreakdownPackCarton.Columns

                            Select Case Col.ColumnName.ToString.ToUpper
                                Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                                Case Else
                                    _colcount = _colcount + 1

                                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

                                    With ColG

                                        .FieldName = Col.ColumnName.ToString
                                        .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                        .Caption = Col.ColumnName.ToString
                                        .Tag = Col.ColumnName.ToString
                                        .Visible = True

                                        .ColumnEdit = oRepositoryPackCartonQty

                                    End With

                                    .Columns.Add(ColG)

                                    With .Columns(Col.ColumnName.ToString)

                                        .OptionsFilter.AllowAutoFilter = False
                                        .OptionsFilter.AllowFilter = False
                                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                        .DisplayFormat.FormatString = "{0:n0}"
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                        With .OptionsColumn
                                            .AllowMove = False
                                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                            .AllowEdit = True
                                            .ReadOnly = False
                                        End With

                                    End With

                                    .Columns(Col.ColumnName.ToString).Width = 45
                                    .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                    .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                            End Select

                        Next

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            With GridCol
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                        Next

                    End If

                End With

                Me.ogdOrderSubPackCarton.DataSource = tmpBreakdownPackCarton

                bRet = True

            Catch ex As Exception


                Me.ogdOrderSubPackCarton.DataSource = Nothing
            End Try

            Call FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)

        Else
            Me.FNPackCartonSubType.SelectedIndex = 0
            Call FNPackCartonSubType_SelectedIndexChanged(FNPackCartonSubType, New System.EventArgs)
        End If

        Return bRet

    End Function

    Private Function W_PRCbShowBrowseDataSubOrderSizeSpecInfo(ByVal ptFTOrderNo As String, ByVal ptFTSubOrderNo As String) As Boolean

        Me.ogdSizeSpec.DataSource = Nothing
        Me.ogdSizeSpec.Refresh()

        oDBdtSizeSpecView = Nothing

        Call W_PRCbRemoveGridViewColumn(Me.ogvSizeSpec)
        Me.ogvSizeSpec.OptionsView.ColumnAutoWidth = False
        Me.ogvSizeSpec.RefreshData()

        Dim oDBdtSizeBreakdown As DataTable
        Dim oDBdtSrc As DataTable
        Dim tSql As String

        Try
            tSql = ""
            tSql = "SELECT A.FNHSysMatSizeId, A.FTMatSizeCode, A.FTMatSizeNameEN AS FTMatSizeName"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE  EXISTS (SELECT 'T'"
            tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "               WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
            tSql &= Environment.NewLine & "                     AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"
            tSql &= Environment.NewLine & "                     AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)"
            tSql &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq ASC;"

            oDBdtSizeBreakdown = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdtSizeBreakdown.Rows.Count > 0 Then

                oGridViewSizeSpec = Me.ogdSizeSpec.Views(0)
                Call W_PRCbRemoveGridViewColumn(oGridViewSizeSpec)

                Me.ogdSizeSpec.DataSource = Nothing
                Me.ogdSizeSpec.Refresh()
                Me.ogvSizeSpec.RefreshData()

                oDBdtSrc = New System.Data.DataTable()

                '...get schema max length for column FTSizeSpecDesc, FTSizeSpecExtension
                Dim nRepositorySizeSpecDesc As Integer = IIf(oSizeSpecType Is Nothing, 500, oSizeSpecType.SizeSpecFTSizeSpecDesc)
                Dim nRepositorySizeSpecExtension As Integer = IIf(oSizeSpecType Is Nothing, 30, oSizeSpecType.SizeSpecFTSizeSpecExtension)
                Dim nRepositorySizeSpecTolerant As Integer = IIf(oSizeSpecType Is Nothing, 30, oSizeSpecType.SizeSpecFTTolerant)

                Dim oColFNSeq As DataColumn
                oColFNSeq = New DataColumn("FNSeq", System.Type.GetType("System.Int32"))
                oDBdtSrc.Columns.Add(oColFNSeq.ColumnName, oColFNSeq.DataType)

                Dim oColFTSizeSpecDesc As DataColumn
                oColFTSizeSpecDesc = New DataColumn("FTSizeSpecDesc", System.Type.GetType("System.String"))
                oDBdtSrc.Columns.Add(oColFTSizeSpecDesc.ColumnName, oColFTSizeSpecDesc.DataType)

                Dim oColMeasId As DataColumn = New DataColumn("FNHSysMeasId", GetType(String))
                oDBdtSrc.Columns.Add(oColMeasId.ColumnName, oColMeasId.DataType)

                Dim oColMeasId_None As DataColumn = New DataColumn("FNHSysMeasId_Hide", GetType(Integer))
                oDBdtSrc.Columns.Add(oColMeasId_None.ColumnName, oColMeasId_None.DataType)


                '...Repository Size Spec Description
                '==================================================================================================================================================================
                Dim oRepositoryFTSizeSpecDesc As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
                With oRepositoryFTSizeSpecDesc
                    .MaxLength = nRepositorySizeSpecDesc
                End With
                oGridViewSizeSpec.Columns.AddField(oColMeasId.ColumnName)
                oGridViewSizeSpec.Columns.Add(cFNHSysMeasId)
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).Visible = True
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).Fixed = FixedStyle.Left
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).Width = 80
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).VisibleIndex = 1

                oGridViewSizeSpec.Columns.AddField(oColMeasId_None.ColumnName)
                oGridViewSizeSpec.Columns.Add(cFNHSysMeasId_None)

                oGridViewSizeSpec.Columns("FNHSysMeasId").ColumnEdit = RepositoryItemFNHSysMeasId
                '  oGridViewSizeSpec.Columns("FTSizeSpecDesc").ColumnEdit = oRepositoryFTSizeSpecDesc
                oGridViewSizeSpec.Columns("FTSizeSpecDesc").Width = 150
                oGridViewSizeSpec.Columns("FTSizeSpecDesc").VisibleIndex = 2
                oGridViewSizeSpec.Columns("FNSeq").Width = 60
                '==================================================================================================================================================================

                '...Iterate loop add column FNHSysMatSizeIdXXX
                '...Repository Size Spec Extension for all FNHSysMatSizeId
                '==================================================================================================================================================================
                Dim oRepositoryFTSizeSpecExtension As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                For Each oRow As DataRow In oDBdtSizeBreakdown.Rows

                    Dim oColFNHSysMatSizeId As DataColumn = New DataColumn("FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
                    oColFNHSysMatSizeId.Caption = "FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString()

                    oGridViewSizeSpec.Columns.AddField(oColFNHSysMatSizeId.ColumnName)
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).FieldName = oColFNHSysMatSizeId.ColumnName
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Name = oColFNHSysMatSizeId.ColumnName
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Caption = oColFNHSysMatSizeId.Caption
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Visible = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.TabStop = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.AllowEdit = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.AllowSort = False

                    oDBdtSrc.Columns.Add(oColFNHSysMatSizeId.ColumnName, oColFNHSysMatSizeId.DataType)

                    Dim oColFTSizeSpecExtension As DataColumn = New DataColumn("FTSizeSpecExtension" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                    oColFTSizeSpecExtension.Caption = oRow.Item("FTMatSizeCode").ToString()

                    oGridViewSizeSpec.Columns.AddField(oColFTSizeSpecExtension.ColumnName)
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).FieldName = oColFTSizeSpecExtension.ColumnName
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Name = oColFTSizeSpecExtension.ColumnName
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Caption = oColFTSizeSpecExtension.Caption
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Visible = True
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Width = 80

                    oRepositoryFTSizeSpecExtension = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()

                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).ColumnEdit = oRepositoryFTSizeSpecExtension

                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).OptionsColumn.AllowEdit = True
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).OptionsColumn.AllowSort = False

                    With oRepositoryFTSizeSpecExtension
                        .MaxLength = nRepositorySizeSpecExtension
                    End With

                    oDBdtSrc.Columns.Add(oColFTSizeSpecExtension.ColumnName, oColFTSizeSpecExtension.DataType)

                Next
                '==================================================================================================================================================================

                '...Add generate dynamic Column FTTolerant +/- ได้ไม่เกินค่าที่กำหนด ที่คอลัมน์นี้
                '...Repository Size Spec Tolerant
                '==================================================================================================================================================================
                Dim oColFTSizeSpecTolerant As System.Data.DataColumn
                oColFTSizeSpecTolerant = New System.Data.DataColumn("FTSizeSpecTolerant", System.Type.GetType("System.String"))
                'oColFTSizeSpecTolerant.Caption = "+/- (Tolerant)"
                oColFTSizeSpecTolerant.Caption = "+/-"

                oGridViewSizeSpec.Columns.AddField(oColFTSizeSpecTolerant.ColumnName)
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).FieldName = oColFTSizeSpecTolerant.ColumnName
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Name = oColFTSizeSpecTolerant.ColumnName
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Caption = oColFTSizeSpecTolerant.Caption
                'oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Visible = True
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).OptionsColumn.AllowEdit = True
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).OptionsColumn.AllowMove = False
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).OptionsColumn.AllowSort = False

                Dim oRepositoryFTSizeSpecTolerant As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
                With oRepositoryFTSizeSpecTolerant
                    .MaxLength = nRepositorySizeSpecTolerant
                End With

                oGridViewSizeSpec.Columns("FTSizeSpecTolerant").ColumnEdit = oRepositoryFTSizeSpecTolerant

                oDBdtSrc.Columns.Add(oColFTSizeSpecTolerant.ColumnName, oColFTSizeSpecTolerant.DataType)
                '==================================================================================================================================================================



                oGridViewSizeSpec.OptionsBehavior.AllowAddRows = True
                oGridViewSizeSpec.OptionsCustomization.AllowSort = False

                oGridViewSizeSpec.ActiveFilter.Clear()

                '...Clear Data Source from data table oDBdtSizeSpecView
                oDBdtSizeSpecView = Nothing
                oDBdtSizeSpecView = oDBdtSrc.Clone()

                '...Loop iterate binding datatable (datasource for size specify info)
                '============================================================================================================================================
                Dim nFNMaxSeq As Integer  '...Max Sequence from Size Spec
                nFNMaxSeq = 0

                tSql = ""
                tSql = "SELECT MAX(A.FNSeq) AS FNMaxSeq"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "'"
                tSql &= Environment.NewLine & "GROUP BY A.FTOrderNo, A.FTSubOrderNo;"

                nFNMaxSeq = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, 0))

                tSql = ""
                'tSql = "SELECT A.FNHSysMatSizeId, B.FTMatSizeCode, A.FNSeq, A.FTSizeSpecDesc, A.FTSizeSpecExtension, ISNULL(FTTolerant, '+/- 0.05') AS FTSizeSpecTolerant"
                tSql = "SELECT A.FNHSysMatSizeId, B.FTMatSizeCode, A.FNSeq, A.FTSizeSpecDesc, A.FTSizeSpecExtension, ISNULL(A.FTTolerant,'') AS FTSizeSpecTolerant ,ISNULL( M.FTMeasCode,'') AS FNHSysMeasId , ISNULL(M.FNHSysMeasId,0) AS FNHSysMeasId_Hide"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS B WITH(NOLOCK) ON A.FNHSysMatSizeId = B.FNHSysMatSizeId"
                tSql &= Environment.NewLine & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMeasurements AS M WITH(NOLOCK) ON A.FNHSysMeasId = M.FNHSysMeasId "

                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "'"
                tSql &= Environment.NewLine & " AND (A.FNHSysMatSizeId IN  (SELECT A.FNHSysMatSizeId"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE  EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "               WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "                     AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"
                tSql &= Environment.NewLine & "                     AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)))"

                tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC, A.FNSeq ASC;"

                Dim oDBdtSizeSpecSrc As DataTable

                oDBdtSizeSpecSrc = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If Not oDBdtSizeSpecSrc Is Nothing AndAlso oDBdtSizeSpecSrc.Rows.Count > 0 Then

                    Dim nCntSeq As Integer = 0
                    Dim nRowIdxAppend As Integer = 0
                    Dim nFNHSysMatSizeIdCurr As Integer
                    Dim nFNHSysMatSizeIdPrv As Integer

                    nFNHSysMatSizeIdPrv = CInt(oDBdtSizeSpecSrc.Rows(0).Item("FNHSysMatSizeId"))

                    Dim nLoopRowSrc As Integer
                    For nLoopRowSrc = 0 To oDBdtSizeSpecSrc.Rows.Count - 1

                        nFNHSysMatSizeIdCurr = CInt(oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMatSizeId"))

                        Dim tFNHSysMatSizeIdXXX As String = "FNHSysMatSizeId" & oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTMatSizeCode").ToString()
                        Dim tFTSizeSpecExtensionXXX As String = "FTSizeSpecExtension" & oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTMatSizeCode").ToString()

                        Dim oDataRowItem As DataRow

                        If nCntSeq < nFNMaxSeq Then
                            oDataRowItem = oDBdtSizeSpecView.NewRow()
                            oDataRowItem.Item("FNSeq") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNSeq")
                            oDataRowItem.Item("FTSizeSpecDesc") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecDesc").ToString()
                            oDataRowItem.Item("FNHSysMeasId") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMeasId").ToString()
                            oDataRowItem.Item("FNHSysMeasId_Hide") = Val(oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMeasId_Hide").ToString())

                            oDataRowItem.Item("FTSizeSpecTolerant") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecTolerant").ToString()

                            oDBdtSizeSpecView.Rows.Add(oDataRowItem)

                            For Each oColSizeSpec As DataColumn In oDBdtSizeSpecView.Columns
                                If oColSizeSpec.ColumnName = tFNHSysMatSizeIdXXX Then
                                    oDataRowItem.Item(tFNHSysMatSizeIdXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMatSizeId")
                                    oDataRowItem.Item(tFTSizeSpecExtensionXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecExtension")

                                    oDBdtSizeSpecView.AcceptChanges()

                                    Exit For

                                End If

                            Next

                            nCntSeq = nCntSeq + 1

                            nFNHSysMatSizeIdPrv = nFNHSysMatSizeIdCurr
                        Else

                            If nFNHSysMatSizeIdCurr <> nFNHSysMatSizeIdPrv Then
                                nRowIdxAppend = 0
                            End If

                            oDBdtSizeSpecView.Rows(nRowIdxAppend).Item(tFNHSysMatSizeIdXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMatSizeId")
                            oDBdtSizeSpecView.Rows(nRowIdxAppend).Item(tFTSizeSpecExtensionXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecExtension").ToString()

                            nRowIdxAppend = nRowIdxAppend + 1

                            nFNHSysMatSizeIdPrv = nFNHSysMatSizeIdCurr
                        End If

                        oDBdtSizeSpecView.AcceptChanges()

                    Next nLoopRowSrc

                    oDBdtSizeSpecView.AcceptChanges()

                End If

                '============================================================================================================================================


                ogdSizeSpec.DataSource = oDBdtSizeSpecView
                ogdSizeSpec = oGridViewSizeSpec.GridControl
                ' HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogdSizeSpec.Views(0))
                oGridViewSizeSpec.OptionsView.ColumnAutoWidth = False
                ogdSizeSpec.Refresh()
                oGridViewSizeSpec.RefreshData()


                '...State Approved Factory Sub Order No. {Approved/Revised} Packing Carton Information
                '============================================================================================================================================================================================================================================================================================================================================
                tSql = ""
                tSql = "DECLARE @FTStateApprovedSizeSpec AS NVARCHAR(1);"
                tSql &= Environment.NewLine & "SELECT TOP 1 @FTStateApprovedSizeSpec = A.FTStateApprovedSizeSpec FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "';"
                tSql &= Environment.NewLine & "IF (@FTStateApprovedSizeSpec IS NULL)"
                tSql &= Environment.NewLine & "BEGIN"
                tSql &= Environment.NewLine & "  SET @FTStateApprovedSizeSpec = N'2' /*not approved & not revised*/"
                tSql &= Environment.NewLine & "END;"
                tSql &= Environment.NewLine & "PRINT '@FTStateApprovedSizeSpec : ' + @FTStateApprovedSizeSpec;"
                tSql &= Environment.NewLine & "SELECT @FTStateApprovedSizeSpec AS FTStateApprovedSizeSpec;"

                Dim tTextFTApprovedInfoState As String

                tTextFTApprovedInfoState = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "2")


                '============================================================================================================================================================================================================================================================================================================================================

            End If

            Return True
        Catch ex As Exception

            oDBdtSizeSpecView = Nothing
            Me.ogdSizeSpec.DataSource = Nothing
            Me.ogdSizeSpec.Refresh()
            Me.ogvSizeSpec.RefreshData()
            Call W_PRCbRemoveGridViewColumn(Me.ogvSizeSpec)
            Me.ogvSizeSpec.OptionsView.ColumnAutoWidth = False
            Return False
        End Try

    End Function

    Private Function W_PRCbShowBrowseDataSubOrderDivertSizeSpecInfo(ByVal ptFTOrderNo As String, ByVal ptFTSubOrderNo As String) As Boolean

        Me.ogdSizeSpec.DataSource = Nothing
        Me.ogdSizeSpec.Refresh()

        oDBdtSizeSpecView = Nothing

        Call W_PRCbRemoveGridViewColumn(Me.ogvSizeSpec)
        Me.ogvSizeSpec.OptionsView.ColumnAutoWidth = False
        Me.ogvSizeSpec.RefreshData()

        Dim oDBdtSizeBreakdown As DataTable
        Dim oDBdtSrc As DataTable
        Dim tSql As String

        Try
            tSql = ""
            tSql = "SELECT A.FNHSysMatSizeId, A.FTMatSizeCode, A.FTMatSizeNameEN AS FTMatSizeName"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE  EXISTS (SELECT 'T'"
            tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
            tSql &= Environment.NewLine & "               WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
            tSql &= Environment.NewLine & "                     AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"
            tSql &= Environment.NewLine & "                     AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)"
            tSql &= Environment.NewLine & "ORDER BY A.FNMatSizeSeq ASC;"

            oDBdtSizeBreakdown = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdtSizeBreakdown.Rows.Count > 0 Then

                oGridViewSizeSpec = Me.ogdSizeSpec.Views(0)
                Call W_PRCbRemoveGridViewColumn(oGridViewSizeSpec)

                Me.ogdSizeSpec.DataSource = Nothing
                Me.ogdSizeSpec.Refresh()
                Me.ogvSizeSpec.RefreshData()

                oDBdtSrc = New System.Data.DataTable()

                '...get schema max length for column FTSizeSpecDesc, FTSizeSpecExtension
                Dim nRepositorySizeSpecDesc As Integer = IIf(oSizeSpecType Is Nothing, 500, oSizeSpecType.SizeSpecFTSizeSpecDesc)
                Dim nRepositorySizeSpecExtension As Integer = IIf(oSizeSpecType Is Nothing, 30, oSizeSpecType.SizeSpecFTSizeSpecExtension)
                Dim nRepositorySizeSpecTolerant As Integer = IIf(oSizeSpecType Is Nothing, 30, oSizeSpecType.SizeSpecFTTolerant)

                Dim oColFNSeq As DataColumn
                oColFNSeq = New DataColumn("FNSeq", System.Type.GetType("System.Int32"))
                oDBdtSrc.Columns.Add(oColFNSeq.ColumnName, oColFNSeq.DataType)

                Dim oColFTSizeSpecDesc As DataColumn
                oColFTSizeSpecDesc = New DataColumn("FTSizeSpecDesc", System.Type.GetType("System.String"))
                oDBdtSrc.Columns.Add(oColFTSizeSpecDesc.ColumnName, oColFTSizeSpecDesc.DataType)

                Dim oColMeasId As DataColumn = New DataColumn("FNHSysMeasId", GetType(String))
                oDBdtSrc.Columns.Add(oColMeasId.ColumnName, oColMeasId.DataType)

                Dim oColMeasId_None As DataColumn = New DataColumn("FNHSysMeasId_Hide", GetType(Integer))
                oDBdtSrc.Columns.Add(oColMeasId_None.ColumnName, oColMeasId_None.DataType)


                '...Repository Size Spec Description
                '==================================================================================================================================================================
                Dim oRepositoryFTSizeSpecDesc As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
                With oRepositoryFTSizeSpecDesc
                    .MaxLength = nRepositorySizeSpecDesc
                End With
                oGridViewSizeSpec.Columns.AddField(oColMeasId.ColumnName)
                oGridViewSizeSpec.Columns.Add(cFNHSysMeasId)
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).Visible = True
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).Fixed = FixedStyle.Left
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).Width = 80
                oGridViewSizeSpec.Columns(oColMeasId.ColumnName).VisibleIndex = 1

                oGridViewSizeSpec.Columns.AddField(oColMeasId_None.ColumnName)
                oGridViewSizeSpec.Columns.Add(cFNHSysMeasId_None)

                oGridViewSizeSpec.Columns("FNHSysMeasId").ColumnEdit = RepositoryItemFNHSysMeasId
                '  oGridViewSizeSpec.Columns("FTSizeSpecDesc").ColumnEdit = oRepositoryFTSizeSpecDesc
                oGridViewSizeSpec.Columns("FTSizeSpecDesc").Width = 150
                oGridViewSizeSpec.Columns("FTSizeSpecDesc").VisibleIndex = 2
                oGridViewSizeSpec.Columns("FNSeq").Width = 60
                '==================================================================================================================================================================

                '...Iterate loop add column FNHSysMatSizeIdXXX
                '...Repository Size Spec Extension for all FNHSysMatSizeId
                '==================================================================================================================================================================
                Dim oRepositoryFTSizeSpecExtension As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
                For Each oRow As DataRow In oDBdtSizeBreakdown.Rows

                    Dim oColFNHSysMatSizeId As DataColumn = New DataColumn("FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.Int32"))
                    oColFNHSysMatSizeId.Caption = "FNHSysMatSizeId" & oRow.Item("FTMatSizeCode").ToString()

                    oGridViewSizeSpec.Columns.AddField(oColFNHSysMatSizeId.ColumnName)
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).FieldName = oColFNHSysMatSizeId.ColumnName
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Name = oColFNHSysMatSizeId.ColumnName
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Caption = oColFNHSysMatSizeId.Caption
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).Visible = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.TabStop = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.AllowEdit = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewSizeSpec.Columns(oColFNHSysMatSizeId.ColumnName).OptionsColumn.AllowSort = False

                    oDBdtSrc.Columns.Add(oColFNHSysMatSizeId.ColumnName, oColFNHSysMatSizeId.DataType)

                    Dim oColFTSizeSpecExtension As DataColumn = New DataColumn("FTSizeSpecExtension" & oRow.Item("FTMatSizeCode").ToString(), System.Type.GetType("System.String"))
                    oColFTSizeSpecExtension.Caption = oRow.Item("FTMatSizeCode").ToString()

                    oGridViewSizeSpec.Columns.AddField(oColFTSizeSpecExtension.ColumnName)
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).FieldName = oColFTSizeSpecExtension.ColumnName
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Name = oColFTSizeSpecExtension.ColumnName
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Caption = oColFTSizeSpecExtension.Caption
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Visible = True
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).Width = 80

                    oRepositoryFTSizeSpecExtension = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()

                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).ColumnEdit = oRepositoryFTSizeSpecExtension

                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).OptionsColumn.AllowEdit = True
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).OptionsColumn.AllowMove = False
                    oGridViewSizeSpec.Columns(oColFTSizeSpecExtension.ColumnName).OptionsColumn.AllowSort = False

                    With oRepositoryFTSizeSpecExtension
                        .MaxLength = nRepositorySizeSpecExtension
                    End With

                    oDBdtSrc.Columns.Add(oColFTSizeSpecExtension.ColumnName, oColFTSizeSpecExtension.DataType)

                Next
                '==================================================================================================================================================================

                '...Add generate dynamic Column FTTolerant +/- ได้ไม่เกินค่าที่กำหนด ที่คอลัมน์นี้
                '...Repository Size Spec Tolerant
                '==================================================================================================================================================================
                Dim oColFTSizeSpecTolerant As System.Data.DataColumn
                oColFTSizeSpecTolerant = New System.Data.DataColumn("FTSizeSpecTolerant", System.Type.GetType("System.String"))
                'oColFTSizeSpecTolerant.Caption = "+/- (Tolerant)"
                oColFTSizeSpecTolerant.Caption = "+/-"

                oGridViewSizeSpec.Columns.AddField(oColFTSizeSpecTolerant.ColumnName)
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).FieldName = oColFTSizeSpecTolerant.ColumnName
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Name = oColFTSizeSpecTolerant.ColumnName
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Caption = oColFTSizeSpecTolerant.Caption
                'oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Tag = oRow.Item("FTMatSizeCode").ToString()
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).Visible = True
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).OptionsColumn.AllowEdit = True
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).OptionsColumn.AllowMove = False
                oGridViewSizeSpec.Columns(oColFTSizeSpecTolerant.ColumnName).OptionsColumn.AllowSort = False

                Dim oRepositoryFTSizeSpecTolerant As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
                With oRepositoryFTSizeSpecTolerant
                    .MaxLength = nRepositorySizeSpecTolerant
                End With

                oGridViewSizeSpec.Columns("FTSizeSpecTolerant").ColumnEdit = oRepositoryFTSizeSpecTolerant

                oDBdtSrc.Columns.Add(oColFTSizeSpecTolerant.ColumnName, oColFTSizeSpecTolerant.DataType)
                '==================================================================================================================================================================



                oGridViewSizeSpec.OptionsBehavior.AllowAddRows = True
                oGridViewSizeSpec.OptionsCustomization.AllowSort = False

                oGridViewSizeSpec.ActiveFilter.Clear()

                '...Clear Data Source from data table oDBdtSizeSpecView
                oDBdtSizeSpecView = Nothing
                oDBdtSizeSpecView = oDBdtSrc.Clone()

                '...Loop iterate binding datatable (datasource for size specify info)
                '============================================================================================================================================
                Dim nFNMaxSeq As Integer  '...Max Sequence from Size Spec
                nFNMaxSeq = 0

                tSql = ""
                tSql = "SELECT MAX(A.FNSeq) AS FNMaxSeq"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_SizeSpec AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)
                tSql &= Environment.NewLine & "GROUP BY A.FTOrderNo, A.FTSubOrderNo;"

                nFNMaxSeq = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, 0))

                tSql = ""
                'tSql = "SELECT A.FNHSysMatSizeId, B.FTMatSizeCode, A.FNSeq, A.FTSizeSpecDesc, A.FTSizeSpecExtension, ISNULL(FTTolerant, '+/- 0.05') AS FTSizeSpecTolerant"
                tSql = "SELECT A.FNHSysMatSizeId, B.FTMatSizeCode, A.FNSeq, A.FTSizeSpecDesc, A.FTSizeSpecExtension, ISNULL(A.FTTolerant,'') AS FTSizeSpecTolerant ,ISNULL( M.FTMeasCode,'') AS FNHSysMeasId , ISNULL(M.FNHSysMeasId,0) AS FNHSysMeasId_Hide"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_SizeSpec AS A WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS B WITH(NOLOCK) ON A.FNHSysMatSizeId = B.FNHSysMatSizeId"
                tSql &= Environment.NewLine & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMeasurements AS M WITH(NOLOCK) ON A.FNHSysMeasId = M.FNHSysMeasId "

                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "' AND FNDivertSeq =" & (SubOrderDivertSeq)
                tSql &= Environment.NewLine & " AND (A.FNHSysMatSizeId IN  (SELECT A.FNHSysMatSizeId"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatSize AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE  EXISTS (SELECT 'T'"
                tSql &= Environment.NewLine & "               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS L1 WITH(NOLOCK)"
                tSql &= Environment.NewLine & "               WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo) & "'"
                tSql &= Environment.NewLine & "                     AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo) & "'"
                tSql &= Environment.NewLine & "                     AND L1.FNHSysMatSizeId = A.FNHSysMatSizeId)))"

                tSql &= Environment.NewLine & "ORDER BY B.FNMatSizeSeq ASC, A.FNSeq ASC;"

                Dim oDBdtSizeSpecSrc As DataTable

                oDBdtSizeSpecSrc = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                If Not oDBdtSizeSpecSrc Is Nothing AndAlso oDBdtSizeSpecSrc.Rows.Count > 0 Then

                    Dim nCntSeq As Integer = 0
                    Dim nRowIdxAppend As Integer = 0
                    Dim nFNHSysMatSizeIdCurr As Integer
                    Dim nFNHSysMatSizeIdPrv As Integer

                    nFNHSysMatSizeIdPrv = CInt(oDBdtSizeSpecSrc.Rows(0).Item("FNHSysMatSizeId"))

                    Dim nLoopRowSrc As Integer
                    For nLoopRowSrc = 0 To oDBdtSizeSpecSrc.Rows.Count - 1

                        nFNHSysMatSizeIdCurr = CInt(oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMatSizeId"))

                        Dim tFNHSysMatSizeIdXXX As String = "FNHSysMatSizeId" & oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTMatSizeCode").ToString()
                        Dim tFTSizeSpecExtensionXXX As String = "FTSizeSpecExtension" & oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTMatSizeCode").ToString()

                        Dim oDataRowItem As DataRow

                        If nCntSeq < nFNMaxSeq Then
                            oDataRowItem = oDBdtSizeSpecView.NewRow()
                            oDataRowItem.Item("FNSeq") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNSeq")
                            oDataRowItem.Item("FTSizeSpecDesc") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecDesc").ToString()
                            oDataRowItem.Item("FNHSysMeasId") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMeasId").ToString()
                            oDataRowItem.Item("FNHSysMeasId_Hide") = Val(oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMeasId_Hide").ToString())

                            oDataRowItem.Item("FTSizeSpecTolerant") = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecTolerant").ToString()

                            oDBdtSizeSpecView.Rows.Add(oDataRowItem)

                            For Each oColSizeSpec As DataColumn In oDBdtSizeSpecView.Columns
                                If oColSizeSpec.ColumnName = tFNHSysMatSizeIdXXX Then
                                    oDataRowItem.Item(tFNHSysMatSizeIdXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMatSizeId")
                                    oDataRowItem.Item(tFTSizeSpecExtensionXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecExtension")

                                    oDBdtSizeSpecView.AcceptChanges()

                                    Exit For

                                End If

                            Next

                            nCntSeq = nCntSeq + 1

                            nFNHSysMatSizeIdPrv = nFNHSysMatSizeIdCurr
                        Else

                            If nFNHSysMatSizeIdCurr <> nFNHSysMatSizeIdPrv Then
                                nRowIdxAppend = 0
                            End If

                            oDBdtSizeSpecView.Rows(nRowIdxAppend).Item(tFNHSysMatSizeIdXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FNHSysMatSizeId")
                            oDBdtSizeSpecView.Rows(nRowIdxAppend).Item(tFTSizeSpecExtensionXXX) = oDBdtSizeSpecSrc.Rows(nLoopRowSrc).Item("FTSizeSpecExtension").ToString()

                            nRowIdxAppend = nRowIdxAppend + 1

                            nFNHSysMatSizeIdPrv = nFNHSysMatSizeIdCurr
                        End If

                        oDBdtSizeSpecView.AcceptChanges()

                    Next nLoopRowSrc

                    oDBdtSizeSpecView.AcceptChanges()

                End If

                '============================================================================================================================================


                ogdSizeSpec.DataSource = oDBdtSizeSpecView
                ogdSizeSpec = oGridViewSizeSpec.GridControl
                ' HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogdSizeSpec.Views(0))
                oGridViewSizeSpec.OptionsView.ColumnAutoWidth = False
                ogdSizeSpec.Refresh()
                oGridViewSizeSpec.RefreshData()


                '...State Approved Factory Sub Order No. {Approved/Revised} Packing Carton Information
                '============================================================================================================================================================================================================================================================================================================================================
                tSql = ""
                tSql = "DECLARE @FTStateApprovedSizeSpec AS NVARCHAR(1);"
                tSql &= Environment.NewLine & "SELECT TOP 1 @FTStateApprovedSizeSpec = A.FTStateApprovedSizeSpec FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTOrderNo.ToString()) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptFTSubOrderNo.ToString()) & "';"
                tSql &= Environment.NewLine & "IF (@FTStateApprovedSizeSpec IS NULL)"
                tSql &= Environment.NewLine & "BEGIN"
                tSql &= Environment.NewLine & "  SET @FTStateApprovedSizeSpec = N'2' /*not approved & not revised*/"
                tSql &= Environment.NewLine & "END;"
                tSql &= Environment.NewLine & "PRINT '@FTStateApprovedSizeSpec : ' + @FTStateApprovedSizeSpec;"
                tSql &= Environment.NewLine & "SELECT @FTStateApprovedSizeSpec AS FTStateApprovedSizeSpec;"

                Dim tTextFTApprovedInfoState As String

                tTextFTApprovedInfoState = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "2")


                '============================================================================================================================================================================================================================================================================================================================================

            End If

            Return True
        Catch ex As Exception

            oDBdtSizeSpecView = Nothing
            Me.ogdSizeSpec.DataSource = Nothing
            Me.ogdSizeSpec.Refresh()
            Me.ogvSizeSpec.RefreshData()
            Call W_PRCbRemoveGridViewColumn(Me.ogvSizeSpec)
            Me.ogvSizeSpec.OptionsView.ColumnAutoWidth = False
            Return False
        End Try

    End Function

#End Region
    Private Sub LoadColorway()
        Dim cmd As String = ""
        Dim dt As DataTable

        cmd = " Select FTMatColorCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatColor As X With(NOLOCK) WHERE FTStateActive='1'  ORDER BY FTMatColorCode "
        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        ReposLEFTMatColorCodeNew.DataSource = dt.Copy
        dt.Dispose()
    End Sub
    Private Sub wDivertOrderSub_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Enable "..." button in gridview 
        AddHandler RepositoryItemFNHSysMeasId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick

        Call PROC_GETbInitSubOrderNoSrcDivert()
        ReposLEFTMatColorCodeNew.DataSource = Nothing
        Me.otab.SelectedTabPage = otpOrderBreakdown
        If Me.FTSubOrderNoSrc.Properties.Items.Count > 0 Then

            Call LoadColorway()

            '...Defalut First Sub Order No. for divert breakdown
            Me.FTSubOrderNoSrc.Text = _FTSubOrderSrc
            ' Me.FTSubOrderNoSrc.SelectedIndex = 0
            Call FTSubOrderNoSrc_SelectedIndexChanged(sender, e)
        End If
    End Sub

    Private Function ValidateData() As Boolean
        Dim pass As Boolean = True

        If Me.ogvDivertDT.RowCount <= 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลสำหรับการ Divert กรุณาทำการตวจสอบ !!!", 1702280324, Me.Text,, MessageBoxIcon.Warning)
            Return False
        End If

        Dim dtdvt As New DataTable
        With CType(Me.ogdDivertDT.DataSource, DataTable)
            dtdvt = .Copy
        End With

        If dtdvt.Select("FTMatColorCodeNew=''").Length > 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Colorway สำหรับการ Divert กรุณาทำการตวจสอบ !!!", 1702280325, Me.Text,, MessageBoxIcon.Warning)
            pass = False
        End If

        If (pass) Then
            Dim grp As List(Of String) = (dtdvt.Select("FTMatColorCodeNew<>''", "FTMatColorCodeNew").CopyToDataTable).AsEnumerable() _
                                                                 .Select(Function(r) r.Field(Of String)("FTMatColorCodeNew")) _
                                                                 .Distinct() _
                                                                 .ToList()


            For Each str As String In grp
                If dtdvt.Select("FTMatColorCodeNew='" & HI.UL.ULF.rpQuoted(str) & "'").Length > 1 Then
                    HI.MG.ShowMsg.mInfo("ข้อมูล Colorway สำหรับการ Divert ซ้ำกัน กรุณาทำการตวจสอบ !!!", 1702280326, Me.Text,, MessageBoxIcon.Warning)
                    dtdvt.Dispose()
                    Return False
                End If
            Next
        End If

        dtdvt.Dispose()
        Return pass

    End Function
    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click

        If PROC_GETbValidateSubOrderNoInfo() = True Then
            ' If ValidateData() Then

            If ValidateData() Then

                If System.Windows.Forms.MessageBox.Show("Are you sure, do you want to divert sub breakdown" & Environment.NewLine & "from Factory Order No. : " & _FTOrderNoSrc & Environment.NewLine & "Factory Sub Order No. : " & Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString, "Confirm Divert Sub Breakdown", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.Yes Then

                    If PROC_SAVEbDivertSubOrderNo(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString) = True Then
                        'If PROC_SAVEbDivertSubOrderNo(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString) = True Then
                        DialogResult = System.Windows.Forms.DialogResult.OK

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Me.Close()

                    End If

                End If

                ' End If

            End If

        End If
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LoadDataDivertSeq(paramFTOrderNo As String, paramFTSubOrderNo As String)
        Dim tmpDTSubOrderNoInfo As DataTable

        sSQL = ""
        sSQL = " SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo,"
        sSQL &= Environment.NewLine & "            A.FDShipDate,"
        sSQL &= Environment.NewLine & " 			(SELECT TOP 1 L1.FTContinentCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMContinent AS L1 (NOLOCK) WHERE L1.FNHSysContinentId = A.FNHSysContinentId) AS FNHSysContinentId,"
        sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L2.FTCountryCode  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCountry AS L2 (NOLOCK) WHERE L2.FNHSysCountryId = A.FNHSysCountryId AND L2.FNHSysContinentId = A.FNHSysContinentId) AS FNHSysCountryId,"
        sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L3.FTProvinceCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCMMProvince AS L3 (NOLOCK) WHERE L3.FNHSysProvinceId = A.FNHSysProvinceId AND  L3.FNHSysCountryId = A.FNHSysCountryId) AS FNHSysProvinceId,"
        sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L4.FTShipModeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipMode AS L4 (NOLOCK) WHERE L4.FNHSysShipModeId = A.FNHSysShipModeId) AS FNHSysShipModeId,"
        sSQL &= Environment.NewLine & "			    (SELECT TOP 1 L5.FTShipPortCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMShipPort AS L5 (NOLOCK) WHERE L5.FNHSysShipPortId = A.FNHSysShipPortId) AS FNHSysShipPortId "

        sSQL &= Environment.NewLine & "	,FTCustRef,FTPOTrading"
        sSQL &= Environment.NewLine & "	,FTPORef,FTRemark AS FTRemarkSubOrderNo"


        sSQL &= Environment.NewLine & "			  ,  (SELECT TOP 1 L7.FTPlantCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMPlant AS L7 (NOLOCK) WHERE L7.FNHSysPlantId = A.FNHSysPlantId) AS FNHSysPlantId "
        sSQL &= Environment.NewLine & "			  ,  (SELECT TOP 1 L8.FTBuyGrpCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuyGrp AS L8 (NOLOCK) WHERE L8.FNHSysBuyGrpId = A.FNHSysBuyGrpId) AS FNHSysBuyGrpId"

        sSQL &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert AS A (NOLOCK)"
        sSQL &= Environment.NewLine & " WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
        sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'"
        sSQL &= Environment.NewLine & "      AND A.FNDivertSeq =" & Val(SubOrderDivertSeq) & ""

        tmpDTSubOrderNoInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

        If Not DBNull.Value.Equals(tmpDTSubOrderNoInfo) AndAlso tmpDTSubOrderNoInfo.Rows.Count > 0 Then

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

        tmpDTSubOrderNoInfo.Dispose()

        Dim _dt As DataTable
        sSQL = " SELECT  A.FTSubOrderNo, A.FNDivertSeq, A.FTColorway, A.FTSizeBreakDown, A.FNQuantity, A.FTNikePOLineItem,  A.FNPrice, "
        sSQL &= Environment.NewLine & " A.FNPriceOrg, A.FNCMDisPer, A.FNCMDisAmt, A.FNNetPrice,ISNULL(A.FTColorwayNew,A.FTColorway) AS FTColorwayNew"

        If HI.ST.Lang.Language = HI.ST.Lang.eLang.TH Then
            sSQL &= Environment.NewLine & "  ,C.FTMatColorNameTH AS FTMatColorName"
        Else
            sSQL &= Environment.NewLine & "  ,C.FTMatColorNameEN AS FTMatColorName"
        End If

        sSQL &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_BreakDown AS A (NOLOCK)"
        sSQL &= Environment.NewLine & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMatColor AS C ON ISNULL(A.FTColorwayNew,A.FTColorway) = C.FTMatColorCode"
        sSQL &= Environment.NewLine & " WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
        sSQL &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTSubOrderNo) & "'"
        sSQL &= Environment.NewLine & "      AND A.FNDivertSeq =" & Val(SubOrderDivertSeq) & ""

        _dt = HI.Conn.SQLConn.GetDataTable(sSQL, Conn.DB.DataBaseName.DB_MERCHAN)

        If Not (_dt Is Nothing) Then
            If _dt.Rows.Count > 0 Then
                Dim _DtSource As DataTable
                Dim _DtDivert As DataTable

                With CType(ogdDivertSrc.DataSource, DataTable)
                    .AcceptChanges()
                    _DtSource = .Copy
                End With

                With CType(ogdDivertDT.DataSource, DataTable)
                    .AcceptChanges()
                    _DtDivert = .Copy
                End With

                For Each R As DataRow In _dt.Rows

                    Try
                        For Each Rx1 As DataRow In _DtSource.Select("FTMatColorCode='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

                            Rx1.Item("FNAmnt" & R!FTSizeBreakDown.ToString) = Val(Rx1.Item("FNAmnt" & R!FTSizeBreakDown.ToString)) + Val(R!FNQuantity.ToString)
                            Rx1.Item("FNQuantity" & R!FTSizeBreakDown.ToString) = Val(Rx1.Item("FNQuantity" & R!FTSizeBreakDown.ToString)) + Val(R!FNQuantity.ToString)

                            Exit For
                        Next
                    Catch ex As Exception
                    End Try

                    Try
                        For Each Rx1 As DataRow In _DtDivert.Select("FTMatColorCode='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'")

                            Rx1!FTNikePOLineItem = R!FTNikePOLineItem
                            Rx1!FTMatColorCodeNew = R!FTColorwayNew

                            Try
                                Rx1.Item("FNAmnt" & R!FTSizeBreakDown.ToString) = Val(R!FNQuantity.ToString)
                                Rx1.Item("FNQuantity" & R!FTSizeBreakDown.ToString) = Val(R!FNQuantity.ToString)
                            Catch ex As Exception
                            End Try
                            Exit For
                        Next

                    Catch ex As Exception
                    End Try

                Next

                Dim _TotalQty As Integer = 0
                _DtSource.BeginInit()
                For Each Rx1 As DataRow In _DtSource.Rows

                    _TotalQty = 0

                    For Each Col As DataColumn In _DtSource.Columns
                        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                            Case "FNAmnt"
                                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                            Case Else

                        End Select
                    Next

                    Rx1!FNTotal = _TotalQty
                Next
                _DtSource.EndInit()

                _TotalQty = 0
                _DtDivert.BeginInit()
                For Each Rx1 As DataRow In _DtDivert.Rows

                    _TotalQty = 0

                    For Each Col As DataColumn In _DtDivert.Columns

                        Select Case Microsoft.VisualBasic.Left(Col.ColumnName.ToString, "FNAmnt".Length)
                            Case "FNAmnt"
                                _TotalQty = _TotalQty + Val(Rx1.Item(Col.ColumnName.ToString).ToString)
                            Case Else

                        End Select

                    Next

                    Rx1!FNTotal = _TotalQty
                Next
                _DtDivert.EndInit()

                ogdDivertSrc.DataSource = _DtSource
                ogdDivertSrc.Refresh()

                ogdDivertDT.DataSource = _DtDivert
                ogdDivertDT.Refresh()

            End If
        End If

    End Sub

    Private Sub FTSubOrderNoSrc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTSubOrderNoSrc.SelectedIndexChanged
        Try
            If Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString <> "" Then

                Call PROC_SETxClearControl()
                Call PROC_GETbShowBrowseDataFactorySubOrderNoInfo(Me._FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                Call PROC_GETbShowBrowseDataMatrixBreakdownSrcDivert(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                Call PROC_GETbPrepareTemplateDivertBreakdown(Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                Call PROC_GETSubOrderNoInfo(Me._FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                Call W_PRCxInitNewRowSizeSpec(CType(ogdSizeSpec.DataSource, System.Data.DataTable))

                If Me.SubOrderDivertSeq > 0 Then
                    LoadDataDivertSeq(Me._FTOrderNoSrc, Me.FTSubOrderNoSrc.Properties.Items(Me.FTSubOrderNoSrc.SelectedIndex).ToString)
                End If

            End If

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            '    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            'End If
        End Try

    End Sub

    Private Function W_PRCbShowBrowseDataSubOrderComponent(ByVal ptOrderNo$, ByVal ptSubOrderNo$) As Boolean

        Dim bRetLoadSubOrdComponent As Boolean = False

        Try
            Dim tSql As String
            tSql = ""

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("SELECT CONVERT(INT, A.FNSeq) AS FNSeq, A.FNSeq AS FNSeqOrg, A.FNPart, B.FTMainMatCode,")

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.TH
                    oStrBuilder.AppendLine("       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameTH) AS FTMainMatDesc")
                Case Else
                    oStrBuilder.AppendLine("       A.FTOrderNo, A.FTSubOrderNo, A.FNHSysMerMatId, (B.FTMainMatNameEN) AS FTMainMatDesc")
            End Select

            oStrBuilder.AppendLine("       , A.FNConSmp")
            oStrBuilder.AppendLine("       , ISNULL(A.FTComponent, '') AS FTComponent, ISNULL(A.FTRemark, '') AS FTRemark")


            If Me.SubOrderDivertSeq <= 0 Then
                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Component] AS A WITH(NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainMat] AS B WITH(NOLOCK) ON A.FNHSysMerMatId = B.FNHSysMainMatId")
                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(ptOrderNo)))
                oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(ptSubOrderNo)))
            Else
                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Divert_Component] AS A WITH(NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMainMat] AS B WITH(NOLOCK) ON A.FNHSysMerMatId = B.FNHSysMainMatId")
                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(ptOrderNo)))
                oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", HI.UL.ULF.rpQuoted(ptSubOrderNo)))
                oStrBuilder.AppendLine(String.Format("      AND A.FNDivertSeq = N{0}", Me.SubOrderDivertSeq))

            End If

            oStrBuilder.AppendLine("ORDER BY A.FNSeq  ")
            tSql = ""
            tSql = oStrBuilder.ToString()

            Dim oDBdtSubOrderComponent As System.Data.DataTable

            oDBdtSubOrderComponent = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            'RepositoryItemMemoEditFTRemarkComponent
            '...get schema max length for column FTRemark
            Dim nRepositoryFTRemarkComponent As Integer = IIf(oSubCompoentSchema Is Nothing, 500, oSubCompoentSchema.FTRemarkComponent)

            '...Repository FTRemarkComponent
            '==================================================================================================================================================================
            Dim RepositoryItemMemoEditFTRemarkComponent As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
            With RepositoryItemMemoEditFTRemarkComponent
                .MaxLength = nRepositoryFTRemarkComponent
            End With

            Me.ogdOrderSubComponent.DataSource = oDBdtSubOrderComponent
            Me.ogdOrderSubComponent.Refresh()
            Me.ogvOrderSubComponent.RefreshData()
            Me.ogvOrderSubComponent.OptionsView.ColumnAutoWidth = False
            REM Me.ogvOrderSubComponent.BestFitColumns()

            '...State Approved Factory Sub Order No. {Approved/Revised} component Information
            '============================================================================================================================================================================================================================================================================================================================================
            'tSql = ""
            'tSql = "DECLARE @FTStateApprovedComponent AS NVARCHAR(1);"
            'tSql &= Environment.NewLine & "SELECT TOP 1 @FTStateApprovedComponent = A.FTStateApprovedComponent FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(ptOrderNo$) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(ptSubOrderNo$) & "';"
            'tSql &= Environment.NewLine & "IF (@FTStateApprovedComponent IS NULL)"
            'tSql &= Environment.NewLine & "BEGIN"
            'tSql &= Environment.NewLine & "  SET @FTStateApprovedComponent = N'2' /*not approved & not revised*/"
            'tSql &= Environment.NewLine & "END;"
            'tSql &= Environment.NewLine & "PRINT '@FTStateApprovedComponent : ' + @FTStateApprovedComponent;"
            'tSql &= Environment.NewLine & "SELECT @FTStateApprovedComponent AS FTStateApprovedComponent;"

            'Dim tTextFTApprovedInfoState As String

            'tTextFTApprovedInfoState = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "2")

            'If tTextFTApprovedInfoState = "0" Then
            '    Dim tRevisedTime As String = Val(HI.Conn.SQLConn.GetField("SELECT A.FNCntApprovedComponent FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text.Trim) & "'", HI.Conn.DB.DataBaseName.DB_MERCHAN, "0")).ToString

            '    Me.FTStateApprovedComponent.Visible = False
            '    Me.FTStateApprovedComponent.Checked = False
            '    'Me.FTStateApprovedComponent.Text = "Revised Information ( " & tRevisedTime & " )"
            '    'Me.FTStateApprovedComponent.ForeColor = System.Drawing.Color.DarkRed
            '    'Me.FTStateApprovedComponent.BackColor = Color.Transparent
            '    'Me.FTStateApprovedComponent.BringToFront()

            '    Me.FTStateApprovedComponentRevised.Visible = True
            '    Me.FTStateApprovedComponentRevised.Checked = True

            '    Me.FTStateApprovedComponentRevised.ForeColor = System.Drawing.Color.DarkRed
            '    Me.FTStateApprovedComponentRevised.BackColor = Color.Transparent
            '    Me.FTStateApprovedComponentRevised.BringToFront()

            'ElseIf tTextFTApprovedInfoState = "1" Then
            '    Me.FTStateApprovedComponentRevised.Visible = False
            '    Me.FTStateApprovedComponentRevised.Checked = False

            '    Me.FTStateApprovedComponent.Visible = True
            '    Me.FTStateApprovedComponent.Checked = True
            '    'Me.FTStateApprovedComponent.Text = "Approved Information"
            '    Me.FTStateApprovedComponent.ForeColor = System.Drawing.Color.Blue
            '    Me.FTStateApprovedComponent.BackColor = Color.Transparent
            '    Me.FTStateApprovedComponent.BringToFront()

            'Else
            '    Me.FTStateApprovedComponent.Visible = False
            '    Me.FTStateApprovedComponentRevised.Visible = False
            '    'Me.FTStateApprovedComponent.Text = ""
            '    'Me.FTStateApprovedComponent.ForeColor = DevExpress.XtraEditors.CheckEdit.DefaultForeColor 'Me.GroupControl2.ForeColor 'DevExpress.XtraEditors.CheckEdit.DefaultBackColor
            '    'Me.FTStateApprovedComponent.BackColor = DevExpress.XtraEditors.CheckEdit.DefaultBackColor

            'End If
            '============================================================================================================================================================================================================================================================================================================================================

            '...load data complete
            bRetLoadSubOrdComponent = True

        Catch ex As Exception

        End Try

        Return bRetLoadSubOrdComponent

    End Function

    Private Sub W_PRCxInitNewRowSew(ByVal oDataTableSrc As System.Data.DataTable)
        Try
            Dim oDataRow As DataRow
            Dim dt As DataTable
            oDBdtSewView = oDataTableSrc

            If oDBdtSewView Is Nothing Then Exit Sub

            oDataRow = oDBdtSewView.NewRow()

            Dim nMaxSeq As Integer = 1
            Dim nLastSeq As Integer = 0
            Dim NewText As String = ""
            Dim MaxRowIndex As Integer = 0
            dt = oDataTableSrc

            Dim tFTImageName$ = ""
            If Not DBNull.Value.Equals(Me.FTImageSewing.Image) And Not Me.FTImageSewing.Image Is Nothing Then
                tFTImageName = Me.FTSubOrderNoSrc.Text & "_" & Me.FNSewSeq.Value.ToString()
                tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
            End If

            If (FNSewSeq.Value <= Integer.Parse(ogvOrderSubSewing.RowCount.ToString())) Then
                For Each R As DataRow In dt.Rows
                    If (FNSewSeq.Value = R!FNSewSeq.ToString()) Then
                        'R!FTSewDescription = FTSewDescription.ToString()
                        'For i As Integer = 0 To FTSewDescription.Lines().Count - 1
                        'NewText += FTSewDescription.Lines(i).ToString() & vbCrLf
                        'Test.Replace(Environment.NewLine, "\n")
                        'R!FTSewDescription += FTSewDescription.Lines(i).ToString()
                        'Next
                        R!FTSewDescription = FTSewDescription.Text
                        R!FTSewNote = FTSewNote.Text
                        R!FTImageNew = FTImageSewing.Image
                        R!FTImage = HI.UL.ULImage.SaveImage(Me.FTImageSewing, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Sewing\")

                        FNSewSeq.Value = Integer.Parse(ogvOrderSubSewing.RowCount.ToString()) + 1
                        FTSewDescription.Text = ""
                        FTSewNote.Text = ""
                        FTImageSewing.Image = Nothing
                        Me.ogvOrderSubPack.BestFitColumns()
                        Return
                    End If
                Next
            End If

            For Each oRowSizeSpec As DataRow In oDBdtSewView.Rows
                nLastSeq = oRowSizeSpec.Item("FNSewSeq")
                If nLastSeq > nMaxSeq Then
                    nMaxSeq = nLastSeq + 1
                    MaxRowIndex = oDBdtSewView.Rows.IndexOf(oRowSizeSpec)
                End If
            Next

            If nLastSeq = nMaxSeq Then nMaxSeq = nLastSeq + 1

            oDataRow.Item("FNSewSeq") = nMaxSeq
            oDataRow.Item("FTSewDescription") = FTSewDescription.Text
            oDataRow.Item("FTSewNote") = FTSewNote.Text
            oDataRow.Item("FTImageNew") = FTImageSewing.Image
            oDataRow.Item("FTImage") = HI.UL.ULImage.SaveImage(Me.FTImageSewing, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Sewing\")

            oDBdtSewView.Rows.Add(oDataRow)

            ogdOrderSubSewing.DataSource = oDBdtSewView
            ogdOrderSubSewing.Refresh()
            ogvOrderSubSewing.RefreshData()
            Me.ogvOrderSubPack.BestFitColumns()

            FNSewSeq.Value = Integer.Parse(ogvOrderSubSewing.RowCount.ToString()) + 1
            FTSewDescription.Text = ""
            FTSewNote.Text = ""
            FTImageSewing.Image = Nothing
        Catch ex As Exception
            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

    End Sub

    Private Sub W_PRCxInitNewRowPack(ByVal oDataTableSrc As System.Data.DataTable)
        Try
            Dim oDataRow As DataRow
            Dim dt As DataTable
            oDBdtPackView = oDataTableSrc

            If oDBdtPackView Is Nothing Then Exit Sub

            oDataRow = oDBdtPackView.NewRow()

            Dim nMaxSeq As Integer = 1
            Dim nLastSeq As Integer = 0
            Dim NewText As String = ""
            Dim MaxRowIndex As Integer = 0
            dt = oDataTableSrc

            Dim tFTImageName$ = ""
            If Not DBNull.Value.Equals(Me.FTImagePacking.Image) And Not Me.FTImagePacking.Image Is Nothing Then
                tFTImageName = Me.FTSubOrderNoSrc.Text & "_" & Me.FNPackSeq.Value.ToString()
                tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
            End If

            If (FNPackSeq.Value <= Integer.Parse(ogvOrderSubPack.RowCount.ToString())) Then
                For Each R As DataRow In dt.Rows
                    If (FNPackSeq.Value = R!FNPackSeq.ToString()) Then
                        R!FTPackDescription = FTPackDescription.Text
                        R!FTPackNote = FTPackNote.Text
                        R!FTImageNew = FTImagePacking.Image
                        R!FTImage = HI.UL.ULImage.SaveImage(Me.FTImagePacking, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Packing\")
                        'R!FTImageSewing = FTImagePacking
                        FNPackSeq.Value = Integer.Parse(ogvOrderSubPack.RowCount.ToString()) + 1
                        FTPackDescription.Text = ""
                        FTPackNote.Text = ""
                        FTImagePacking.Image = Nothing
                        Me.ogvOrderSubPack.BestFitColumns()
                        Return
                    End If
                Next
            End If

            For Each oRowSizeSpec As DataRow In oDBdtPackView.Rows
                nLastSeq = oRowSizeSpec.Item("FNPackSeq")
                If nLastSeq > nMaxSeq Then
                    nMaxSeq = nLastSeq + 1
                    MaxRowIndex = oDBdtPackView.Rows.IndexOf(oRowSizeSpec)
                End If
            Next

            If nLastSeq = nMaxSeq Then nMaxSeq = nLastSeq + 1

            oDataRow.Item("FNPackSeq") = nMaxSeq
            oDataRow.Item("FTPackDescription") = FTPackDescription.Text
            oDataRow.Item("FTPackNote") = FTPackNote.Text
            oDataRow.Item("FTImageNew") = FTImagePacking.Image
            oDataRow.Item("FTImage") = HI.UL.ULImage.SaveImage(Me.FTImagePacking, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Packing\")

            oDBdtPackView.Rows.Add(oDataRow)

            ogdOrderSubPack.DataSource = oDBdtPackView
            ogdOrderSubPack.Refresh()
            ogvOrderSubPack.RefreshData()
            Me.ogvOrderSubPack.BestFitColumns()

            FNPackSeq.Value = Integer.Parse(ogvOrderSubPack.RowCount.ToString()) + 1
            FTPackDescription.Text = ""
            FTPackNote.Text = ""
            FTImagePacking.Image = Nothing
        Catch ex As Exception
            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

    End Sub

    Private Sub W_PRCxInitNewRowSizeSpec(ByVal oDataTableSrc As System.Data.DataTable)
        Try
            Dim oDataRow As DataRow
            oDBdtSizeSpecView = oDataTableSrc

            If oDBdtSizeSpecView Is Nothing Then Exit Sub

            oDataRow = oDBdtSizeSpecView.NewRow()

            Dim nMaxSeq As Integer = 1
            Dim nLastSeq As Integer = 0

            Dim MaxRowIndex As Integer = 0

            For Each oRowSizeSpec As DataRow In oDBdtSizeSpecView.Rows
                nLastSeq = oRowSizeSpec.Item("FNSeq")
                If nLastSeq > nMaxSeq Then
                    nMaxSeq = nLastSeq + 1
                    MaxRowIndex = oDBdtSizeSpecView.Rows.IndexOf(oRowSizeSpec)
                End If
            Next

            If nLastSeq = nMaxSeq Then nMaxSeq = nLastSeq + 1

            oDataRow.Item("FNSeq") = nMaxSeq
            oDataRow.Item("FTSizeSpecDesc") = ""

            For Each oCol As DataColumn In oDataTableSrc.Columns
                For Each oColGrid As GridColumn In oGridViewSizeSpec.Columns
                    If oCol.ColumnName = "FTSizeSpecExtension" & CStr(oColGrid.Tag & "").Trim() Then
                        oDataRow.Item(CStr("FNHSysMatSizeId" & CStr(oColGrid.Tag & "").Trim())) = oGridViewSizeSpec.Columns("FNHSysMatSizeId" & CStr(oColGrid.Tag & "").Trim()).Tag
                        Exit For
                    End If
                Next
            Next

            '...add row/column Tolerant (+/-)
            oDataRow.Item("FTSizeSpecTolerant") = ""

            oDBdtSizeSpecView.Rows.Add(oDataRow)

            ogdSizeSpec.DataSource = oDBdtSizeSpecView
            ogdSizeSpec.Refresh()
            ogvSizeSpec.RefreshData()

        Catch ex As Exception
            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

    End Sub

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

    Private Sub ogvOrderSubSewing_Click(sender As Object, e As EventArgs) Handles ogvOrderSubSewing.Click
        Try
            With Me.ogvOrderSubSewing
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

                Me.FNSewSeq.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSewSeq").ToString())
                Me.FTSewDescription.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTSewDescription").ToString()
                Me.FTSewNote.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTSewNote").ToString()
                'Me.FTImageSewing.Image = .GetRowCellValue(.FocusedRowHandle, "FTImageNew")
                Dim tImagePath As String = ""
                Try
                    tImagePath = "" & .GetRowCellValue(.FocusedRowHandle, "FTImage").ToString()
                    If Not DBNull.Value.Equals(tImagePath) And tImagePath <> "" Then
                        'Dim tPathImgExtend As String = _SysPath & "\" & tImagePath
                        Dim tPathImgExtend As String = tW_SysPath & "\OrderNo\SubOrderNo\Sewing" & "\" & tImagePath
                        If IO.File.Exists(tPathImgExtend) Then
                            REM Me.FTImageSewing.Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgExtend)))
                            Me.FTImageSewing.Image = HI.UL.ULImage.LoadImage("" & tPathImgExtend)
                        Else
                            Me.FTImageSewing.Image = Nothing
                        End If
                    Else
                        Me.FTImageSewing.Image = Nothing
                    End If
                Catch ex As Exception
                    Me.FTImageSewing.Image = Nothing
                End Try

                Me.FTSewDescription.Focus()
                Me.FTSewDescription.SelectionStart = 0
                Me.FTSewDescription.SelectionLength = Len(Me.FTSewDescription.Text.Trim())
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvOrderSubPack_Click(sender As Object, e As EventArgs) Handles ogvOrderSubPack.Click
        Try
            With Me.ogvOrderSubPack
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

                Me.FNPackSeq.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNPackSeq").ToString())
                Me.FTPackDescription.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPackDescription").ToString()
                Me.FTPackNote.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPackNote").ToString()

                'Try
                '    Me.FTImagePacking.Image = HI.UL.ULImage.ConvertByteArrayToImmage(.GetRowCellValue(.FocusedRowHandle, "FBImage"))
                'Catch ex As Exception
                '    Me.FTImagePacking.Image = Nothing
                'End Try
                'Dim tImagePath As String = ""
                'Try
                '    tImagePath = "" & .GetRowCellValue(.FocusedRowHandle, "FTImage").ToString()
                '    If Not DBNull.Value.Equals(tImagePath) And tImagePath <> "" Then
                '        Dim tPathImgExtend As String = tW_SysPath & "\OrderNo\SubOrderNo\Packing" & "\" & tImagePath
                '        If IO.File.Exists(tPathImgExtend) Then
                '            Me.FTImagePacking.Image = HI.UL.ULImage.LoadImage("" & tPathImgExtend)
                '        Else
                '            Me.FTImagePacking.Image = Nothing
                '        End If
                '    Else
                '        Me.FTImagePacking.Image = Nothing
                '    End If
                'Catch ex As Exception
                '    Me.FTImagePacking.Image = Nothing
                'End Try

                Dim tImagePath As String = ""
                Try
                    tImagePath = "" & .GetRowCellValue(.FocusedRowHandle, "FTImage").ToString()
                    If Not DBNull.Value.Equals(tImagePath) And tImagePath <> "" Then
                        'Dim tPathImgExtend As String = _SysPath & "\" & tImagePath
                        Dim tPathImgExtend As String = tW_SysPath & "\OrderNo\SubOrderNo\Packing" & "\" & tImagePath
                        If IO.File.Exists(tPathImgExtend) Then
                            REM Me.FTImageSewing.Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgExtend)))
                            Me.FTImagePacking.Image = HI.UL.ULImage.LoadImage("" & tPathImgExtend)
                        Else
                            Me.FTImagePacking.Image = Nothing
                        End If
                    Else
                        Me.FTImagePacking.Image = Nothing
                    End If
                Catch ex As Exception
                    Me.FTImagePacking.Image = Nothing
                End Try

                Me.FTPackDescription.Focus()
                Me.FTPackDescription.SelectionStart = 0
                Me.FTPackDescription.SelectionLength = Len(Me.FTPackDescription.Text.Trim())
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddrow_Click(sender As Object, e As EventArgs) Handles ocmaddrow.Click

        If (otab.SelectedTabPage Is otbSubOrderComponent) Then
            'Call W_PRCbSaveSubOrderSewing()
            Call W_PRCxInitNewRowComponent(CType(ogdOrderSubComponent.DataSource, System.Data.DataTable))

        End If

        If (otab.SelectedTabPage Is otpSubOrderSewInfo) Then
            'Call W_PRCbSaveSubOrderSewing()
            Call W_PRCxInitNewRowSew(CType(ogdOrderSubSewing.DataSource, System.Data.DataTable))

        End If

        If (otab.SelectedTabPage Is otpSubOrderPackInfo) Then
            'Call W_PRCbSaveSubOrderPack()
            Call W_PRCxInitNewRowPack(CType(ogdOrderSubPack.DataSource, System.Data.DataTable))
        End If

        If (otab.SelectedTabPage Is otpSubOrderSizeSpec) Then

            If HI.ST.ValidateData.CloseJob(_FTOrderNoSrc) Then
                HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

            If CheckCreateQASizeSpec() = True Then Exit Sub
            Call W_PRCxInitNewRowSizeSpec(oDBdtSizeSpecView)
        End If

    End Sub

    Private Sub W_PRCxInitNewRowComponent(ByVal oDataTableSrc As System.Data.DataTable)
        Dim oDataRow As DataRow
        Dim dt As DataTable
        oDBdtComponentView = oDataTableSrc

        If oDBdtComponentView Is Nothing Then Exit Sub

        oDataRow = oDBdtComponentView.NewRow()

        With _wAddDivertOrderSubComponent
            HI.TL.HandlerControl.ClearControl(_wAddDivertOrderSubComponent)
            .AddComponent = False
            .FNSeq.Enabled = True
            .ShowDialog()

            If (.AddComponent) Then
                'If Seq same as some row in datatable, shift old data seq +1 and insert new data
                If (oDBdtComponentView.Rows.Count > .FNSeq.Value) Then
                    For i As Integer = oDBdtComponentView.Rows.Count - 1 To .FNSeq.Value - 1 Step -1
                        oDBdtComponentView.Rows(i).Item("FNSeqOrg") = i + 2
                        oDBdtComponentView.Rows(i).Item("FNSeq") = i + 2
                    Next
                End If
                oDataRow.Item("FNSeq") = .FNSeq.Value
                oDataRow.Item("FNSeqOrg") = .FNSeq.Value
                oDataRow.Item("FNHSysMerMatId") = Integer.Parse(Val(.FNHSysMerMatId.Properties.Tag.ToString))
                oDataRow.Item("FTMainMatCode") = .FNHSysMerMatId.Text
                oDataRow.Item("FTMainMatDesc") = .FNHSysMerMatId_None.Text
                oDataRow.Item("FTComponent") = .FTComponent.Text
                oDataRow.Item("FNConSmp") = .FNConSmp.Value
                oDataRow.Item("FTRemark") = .FTRemark.Text

                oDBdtComponentView.Rows.Add(oDataRow)
                'Sort data in datatable by Seq
                oDBdtComponentView.DefaultView.Sort = "FNSeq"
                ogdOrderSubComponent.DataSource = oDBdtComponentView
                ogdOrderSubComponent.Refresh()
                ogvOrderSubComponent.RefreshData()
            End If
        End With
    End Sub

    Private Function CheckCreateQASizeSpec() As Boolean
        Dim _Qry As String = ""

        _Qry = "SELECT TOP 1 FTOrderNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลการทำ QC Size Spec แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1510270549, Me.Text, , MessageBoxIcon.Warning)
            Return True
        Else
            Return False
        End If

    End Function

    Private Function W_PRCbSaveSubOrderSewing() As Boolean
        Try
            If Not W_PRCbVerifyBF_SaveDataSubOrderSewing() Then Return False

            Dim tFTImageName$ = ""
            If Not DBNull.Value.Equals(Me.FTImageSewing.Image) And Not Me.FTImageSewing.Image Is Nothing Then
                tFTImageName = _FTOrderNoSrc & "_" & Me.FNSewSeq.Value.ToString()
                tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
            End If

            Dim tSql$, tSqlRevised$

            '...validate state revised factory sub order no sewing sequence process
            tSqlRevised = ""
            tSqlRevised = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTSewDescription, A.FTSewNote"
            tSqlRevised &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
            tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"

            DTBeforeRevisedSew = HI.Conn.SQLConn.GetDataTable(tSqlRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '------------------------------------------ Represent Before Revise History Factory Order No. Sewing ----------------------------------------
            tSql = ""
            tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTSewDescription, A.FTSewNote, A.FTImage"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & CInt(Me.FNSewSeq.Value) & ";"

            oDBdtSubOrderNoSewBF = Nothing
            oDBdtSubOrderNoSewBF = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
            '-------------------------------------------------------------------------------------------------------------------------------------------

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("UPDATE A")
            oStrBuilder.AppendLine("SET  A.[FTSewDescription] = N'" & HI.UL.ULF.rpQuoted(Me.FTSewDescription.Text) & "'")
            oStrBuilder.AppendLine("    ,A.[FTSewNote] = N'" & HI.UL.ULF.rpQuoted(Me.FTSewNote.Text) & "'")
            oStrBuilder.AppendLine("    ,A.FTImage = N'" & HI.UL.ULImage.SaveImage(Me.FTImageSewing, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Sewing\") & "'")
            oStrBuilder.AppendLine("    ,A.[FTUpdUser] = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
            oStrBuilder.AppendLine(String.Format("    ,A.[FDUpdDate] = {0}", HI.UL.ULDate.FormatDateDB))
            oStrBuilder.AppendLine(String.Format("    ,A.[FTUpdTime] = {0}", HI.UL.ULDate.FormatTimeDB))
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A")
            oStrBuilder.AppendLine("WHERE  A.[FTOrderNo] = '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("       AND A.[FTSubOrderNo] = '" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'")
            oStrBuilder.AppendLine(String.Format("       AND A.[FNSewSeq] = {0}", Me.FNSewSeq.Value))

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                '...add new sew seq / sew description / sew note
                tSql = ""
                tSql = "DECLARE @FNSewSeqMax AS INT;"
                tSql &= Environment.NewLine & "SELECT @FNSewSeqMax = (ISNULL(MAX(A.FNSewSeq),0) + 1)"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
                tSql &= Environment.NewLine & "--PRINT '@FNSewSeqMax : ' + CONVERT(VARCHAR(10), @FNSewSeqMax);"
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]([FTOrderNo],[FTSubOrderNo],[FNSewSeq],[FTSewDescription],[FTSewNote],[FTImage]"
                tSql &= Environment.NewLine & "                                                                                                         ,[FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                                                                                                         ,[FTUpdUser],[FDUpdDate],[FTUpdTime])"
                tSql &= Environment.NewLine & "VALUES ('" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "','" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "',@FNSewSeqMax,N'" & HI.UL.ULF.rpQuoted(Me.FTSewDescription.Text) & "',N'" & HI.UL.ULF.rpQuoted(Me.FTSewNote.Text) & "'"
                tSql &= Environment.NewLine & ",N'" & HI.UL.ULImage.SaveImage(Me.FTImageSewing, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Sewing\") & "'"
                tSql &= ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                tSql &= ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ")"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
            Else
                '------------------------------------------ Represent After Revise History Factory Order No. Sewing ----------------------------------------
                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNSewSeq, A.FTSewDescription, A.FTSewNote, A.FTImage"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
                tSql &= Environment.NewLine & "      AND A.FNSewSeq = " & CInt(Me.FNSewSeq.Value) & ";"

                oDBdtSubOrderNoSewAF = Nothing
                oDBdtSubOrderNoSewAF = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT TOP 1 A.FTStateApprovedSewing FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"

                If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0") = "0" Then
                    DTAfterRevisedSew = Nothing
                Else
                    DTAfterRevisedSew = HI.Conn.SQLConn.GetDataTable(tSqlRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                End If
                '-----------------------------------------------------------------------------------------------------------------------------------------

                '...Write changed log
                'Call PostSaveSubOrderNoSew("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Sew]", _FTOrderNoSrc & "|" & _FTSubOrderSrc & "|" & Me.FNHSysStyleId.Text & "|" & Me.otab.TabPages(Me.otab.SelectedTabPageIndex).Text.ToString() & "|" & Me.FNSewSeq_lbl.Text & " " & CStr(Me.FNSewSeq.Value))

            End If

            REM Not Auto Re-Sequence AS SOON AS
            '================================================================================================================================================================================================================================================================================================================================
            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("UPDATE A")
            oStrBuilder.AppendLine("SET A.FNSewSeq = B.FNReSewSeq")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A LEFT JOIN (SELECT ROW_NUMBER() OVER(ORDER BY L1.FNSewSeq ASC) AS FNReSewSeq, L1.FNSewSeq, L1.FTOrderNo, L1.FTSubOrderNo")
            oStrBuilder.AppendLine("														                                                         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS L1")
            oStrBuilder.AppendLine("														                                                         WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("																                                                       AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "') AS B ON A.FTOrderNo = B.FTSubOrderNo AND A.FTSubOrderNo = B.FTSubOrderNo")
            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'")
            oStrBuilder.AppendLine("      AND A.FNSewSeq = B.FNSewSeq;")

            tSql = ""
            tSql = oStrBuilder.ToString()

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
            '================================================================================================================================================================================================================================================================================================================================

            HI.TL.HandlerControl.ClearControl(Me.ogbSubOrderSewInfo)
            Me.FTImageSewing.Image = Nothing
            Call W_PRCbShowBrowseDataSubOrderSewingInfo()
            'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, ogbSubOrderSewInfo.Text)
            Me.FTSewDescription.Focus()

            Return True
        Catch ex As Exception

            Return False
        End Try

    End Function

    Private Function W_PRCbVerifyBF_SaveDataSubOrderSewing() As Boolean
        Dim bPass As Boolean = False
        Try
            If Me.FTSewDescription.Text.Trim() <> "" Then
                bPass = True
            Else
                HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTSewDescription_lbl.Text)
                Me.FTSewDescription.Focus()
            End If

            Return bPass

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub PostSaveSubOrderNoSew(ByVal ptTableName As String, ByVal ptRefDocKey As String)
        Try
            '...record revise history when user adjust factory sub order no. sewing process
            '...Audit log
            'HI.Auditor.CreateLog.CreateLogdata(oDBdtSubOrderNoSewBF, oDBdtSubOrderNoSewAF, Me.Name, ptTableName, ptRefDocKey)
            oDBdtSubOrderNoSewBF = Nothing
            oDBdtSubOrderNoSewAF = Nothing

            '...compare after revised sewing sequence process 
            '===================================================================================================================================================================
            Dim bFlagRevisedSew As Boolean = False
            If (Not DTBeforeRevisedSew Is Nothing) Or (Not DTAfterRevisedSew Is Nothing) Then
                If Not bFlagRevisedSew And DTBeforeRevisedSew Is Nothing And (Not DTAfterRevisedSew Is Nothing And DTAfterRevisedSew.Rows.Count > 0) Then bFlagRevisedSew = True
                If Not bFlagRevisedSew And (Not DTBeforeRevisedSew Is Nothing And DTBeforeRevisedSew.Rows.Count > 0) And DTAfterRevisedSew Is Nothing Then bFlagRevisedSew = True

                If Not bFlagRevisedSew And Not DTBeforeRevisedSew Is Nothing And DTBeforeRevisedSew.Rows.Count > 0 And Not DTAfterRevisedSew Is Nothing And DTAfterRevisedSew.Rows.Count > 0 Then
                    '...validate FTOrderNo, FTSubOrderNo, FNSewSeq ==> FTSewDescription|FTSewNote
                    Dim tFTOrderNo As String, tFTSubOrderNo As String
                    Dim nFNSewSeq As Integer
                    Dim tFTSewDescription As String, tFTSewNote As String

                    For nLoopChkRevised As Integer = 0 To DTAfterRevisedSew.Rows.Count - 1
                        tFTOrderNo = "" : tFTSubOrderNo = ""
                        nFNSewSeq = 0
                        tFTSewDescription = "" : tFTSewNote = ""

                        tFTOrderNo = DTAfterRevisedSew.Rows(nLoopChkRevised)("FTOrderNo").ToString
                        tFTSubOrderNo = DTAfterRevisedSew.Rows(nLoopChkRevised)("FTSubOrderNo").ToString

                        nFNSewSeq = Val(DTAfterRevisedSew.Rows(nLoopChkRevised)("FNSewSeq"))

                        If Not DBNull.Value.Equals(DTAfterRevisedSew.Rows(nLoopChkRevised)("FTSewDescription")) Then
                            tFTSewDescription = DTAfterRevisedSew.Rows(nLoopChkRevised)("FTSewDescription").ToString
                        Else
                            tFTSewDescription = ""
                        End If

                        If Not DBNull.Value.Equals(DTAfterRevisedSew.Rows(nLoopChkRevised)("FTSewNote")) Then
                            tFTSewNote = DTAfterRevisedSew.Rows(nLoopChkRevised)("FTSewNote").ToString
                        Else
                            tFTSewNote = ""
                        End If

                        '...lookup in DTBeforeRevisedPackRatioDT
                        For Each oDataRow As System.Data.DataRow In DTBeforeRevisedSew.Select("FTOrderNo = '" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "' AND FNSewSeq = " & nFNSewSeq)
                            '...validate FTSewDescription, FTSewNote
                            Dim tFTSewDescriptionBF As String, tFTSewNoteBF As String

                            tFTSewDescriptionBF = "" : tFTSewNoteBF = ""

                            If Not DBNull.Value.Equals(oDataRow.Item("FTSewDescription")) Then
                                tFTSewDescriptionBF = oDataRow.Item("FTSewDescription").ToString
                            Else
                                tFTSewDescriptionBF = ""
                            End If

                            If Not tFTSewDescription.Equals(tFTSewDescriptionBF) Then
                                bFlagRevisedSew = True
                                Exit For
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FTSewNote")) Then
                                tFTSewNoteBF = oDataRow.Item("FTSewNote").ToString
                            Else
                                tFTSewNoteBF = ""
                            End If

                            If Not tFTSewNote.Equals(tFTSewNoteBF) Then
                                bFlagRevisedSew = True
                                Exit For
                            End If

                        Next

                        If bFlagRevisedSew = True Then Exit For

                    Next nLoopChkRevised

                    If (Not bFlagRevisedSew) AndAlso DTBeforeRevisedSew.Rows.Count <> DTAfterRevisedPackRatioDT.Rows.Count Then
                        bFlagRevisedSew = True
                    End If

                End If

            End If

            If bFlagRevisedSew = True Then
                '...update revised factory sub order no. sewing sequence information
                Dim tSQLRevised As String
                tSQLRevised = ""
                tSQLRevised = "DECLARE @FTOrderNo AS NVARCHAR(30);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FTSubOrderNo AS NVARCHAR(30);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FDRevisedInfoDate AS NVARCHAR(10);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoTime AS NVARCHAR(8);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoBy AS NVARCHAR(50);"
                tSQLRevised &= Environment.NewLine & "SET @FTRevisedInfoBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                tSQLRevised &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "';"
                tSQLRevised &= Environment.NewLine & "SET @FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"
                tSQLRevised &= Environment.NewLine & "SELECT @FDRevisedInfoDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTRevisedInfoTime = CONVERT(VARCHAR(8), GETDATE(), 114)"
                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoBy : ' + @FTRevisedInfoBy;"
                tSQLRevised &= Environment.NewLine & "PRINT '@FDRevisedInfoDate : ' + @FDRevisedInfoDate;"
                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoTime : ' + @FTRevisedInfoTime;"
                tSQLRevised &= Environment.NewLine & "UPDATE A"
                tSQLRevised &= Environment.NewLine & "SET A.FTStateApprovedSewing = N'0'"
                tSQLRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                tSQLRevised &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                tSQLRevised &= Environment.NewLine & "      AND A.FTSubOrderNo = @FTSubOrderNo;"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSQLRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                End If

            End If

            DTBeforeRevisedSew = Nothing
            DTAfterRevisedSew = Nothing
            '===================================================================================================================================================================

        Catch ex As Exception
            '...
        End Try
    End Sub

    Private Function W_PRCbShowBrowseDataSubOrderSewingInfo() As Boolean
        Try
            HI.TL.HandlerControl.ClearControl(Me.ogbSubOrderSewInfo)

            Dim tSql As String

            tSql = ""
            tSql = "SELECT ISNULL(MAX(A.FNSewSeq),0) + 1"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"

            Dim nFNSewSeqDefault As Integer
            nFNSewSeqDefault = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))

            Me.FNSewSeq.Value = nFNSewSeqDefault

            Dim oDBdtSubOrderSew As DataTable

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("SELECT A.FNSewSeq, A.FTSewDescription, A.FTSewNote, A.FTImage")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Sew AS A (NOLOCK)")
            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'")
            oStrBuilder.AppendLine("ORDER BY A.FNSewSeq ASC;")

            oDBdtSubOrderSew = HI.Conn.SQLConn.GetDataTable(oStrBuilder.ToString(), HI.Conn.DB.DataBaseName.DB_MERCHAN)

            oDBdtSubOrderSew.Columns.Add("FTImageNew", GetType(Object))

            For Each oRow As DataRow In oDBdtSubOrderSew.Rows
                Dim tImagePath As String = oRow.Item("FTImage").ToString()

                'Dim tPathImgDis As String = tW_SysPath & "\Menu\" & tImagePath
                'Dim tPathImgDis As String = _SysPath & "\" & tImagePath
                Dim tPathImgDis As String = tW_SysPath & "\OrderNo\SubOrderNo\Sewing" & "\" & tImagePath
                If IO.File.Exists(tPathImgDis) Then
                    oRow!FTImageNew = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))
                End If

            Next

            ogvOrderSubSewing.ActiveFilter.Clear()

            Me.ogdOrderSubSewing.DataSource = oDBdtSubOrderSew

            'Dim nCntSewingItem As Integer
            'nCntSewingItem = oDBdtSubOrderSew.Rows.Count

            'For nLoop = 0 To nCntSewingItem - 1
            '    Dim rpic As New DevExpress.XtraEditors.Repository.RepositoryItemImageEdit
            '    Dim _SuperToolTip As New DevExpress.Utils.SuperToolTip()
            '    Dim _ToolTipTitleItem As New DevExpress.Utils.ToolTipTitleItem()

            '    _ToolTipTitleItem.Appearance.Image = rpic.Images
            '    _ToolTipTitleItem.Appearance.Options.UseImage = True
            '    _ToolTipTitleItem.Image = rpic.Images
            '    _ToolTipTitleItem.Text = ""

            '    With _SuperToolTip
            '        .Items.Add(_ToolTipTitleItem)
            '        ' .FixedTooltipWidth = True
            '        '.MaxWidth = 350
            '    End With

            '    rpic.s = _SuperToolTip

            'Next nLoop

            Me.ogvOrderSubSewing.RefreshData()
            Me.ogvOrderSubSewing.OptionsView.ColumnAutoWidth = False
            Me.ogvOrderSubSewing.BestFitColumns()

            '...State Approved Factory Sub Order No. {Approved/Revised} Sewing Sequence Information
            '============================================================================================================================================================================================================================================================================================================================================
            tSql = ""
            tSql = "DECLARE @FTStateApprovedSewing AS NVARCHAR(1);"
            tSql &= Environment.NewLine & "SELECT TOP 1 @FTStateApprovedSewing = A.FTStateApprovedSewing FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"
            tSql &= Environment.NewLine & "IF (@FTStateApprovedSewing IS NULL)"
            tSql &= Environment.NewLine & "BEGIN"
            tSql &= Environment.NewLine & "  SET @FTStateApprovedSewing = N'2' /*not approved & not revised*/"
            tSql &= Environment.NewLine & "END;"
            tSql &= Environment.NewLine & "PRINT '@FTStateApprovedSewing : ' + @FTStateApprovedSewing;"
            tSql &= Environment.NewLine & "SELECT @FTStateApprovedSewing AS FTStateApprovedSewing;"

            Dim tTextFTApprovedInfoState As String

            tTextFTApprovedInfoState = HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "2")


            '============================================================================================================================================================================================================================================================================================================================================

            Return True
        Catch ex As Exception
            'Throw New Exception(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace().ToString())
            Return False
        End Try

    End Function

    Private Function W_PRCbSaveSubOrderPack() As Boolean
        Try
            If Not W_PRCbVerifyBF_SaveDataSubOrderPack() Then Return False

            Dim tFTImageName$ = ""
            If Not DBNull.Value.Equals(Me.FTImagePacking.Image) And Not Me.FTImagePacking.Image Is Nothing Then
                tFTImageName = _FTOrderNoSrc & "_" & Me.FNPackSeq.Value.ToString()
                tFTImageName = Microsoft.VisualBasic.Replace(tFTImageName, "-", "_")
            End If

            Dim tSql$, tSqlRevised$

            tSqlRevised = ""
            tSqlRevised = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTPackDescription, A.FTPackNote"
            tSqlRevised &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A (NOLOCK)"
            tSqlRevised &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"

            DTBeforeRevisedPack = HI.Conn.SQLConn.GetDataTable(tSqlRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            '--------------------------------------------- Represent Before Revise History Factory Sub Order No. Packing -----------------------------------------------
            tSql = ""
            tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTPackDescription, A.FTPackNote, A.FTImage"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & CInt(Me.FNPackSeq.Value) & ";"

            oDBdtSubOrderNoPackBF = Nothing
            oDBdtSubOrderNoPackBF = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)
            '---------------------------------------------------------------------------------------------------------------------------------------------------------

            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("UPDATE A")
            oStrBuilder.AppendLine("SET  A.[FTPackDescription] = N'" & HI.UL.ULF.rpQuoted(Me.FTPackDescription.Text) & "'")
            oStrBuilder.AppendLine("    ,A.[FTPackNote] = N'" & HI.UL.ULF.rpQuoted(Me.FTPackNote.Text) & "'")
            oStrBuilder.AppendLine("    ,A.[FTUpdUser] = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")
            oStrBuilder.AppendLine("    ,A.FTImage = N'" & HI.UL.ULImage.SaveImage(Me.FTImagePacking, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Packing\") & "'")
            oStrBuilder.AppendLine(String.Format("    ,A.[FDUpdDate] = {0}", HI.UL.ULDate.FormatDateDB))
            oStrBuilder.AppendLine(String.Format("    ,A.[FTUpdTime] = {0}", HI.UL.ULDate.FormatTimeDB))
            oStrBuilder.AppendLine("FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A WITH(NOLOCK)")
            oStrBuilder.AppendLine("WHERE  A.[FTOrderNo] = '" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("       AND A.[FTSubOrderNo] = '" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'")
            oStrBuilder.AppendLine(String.Format("       AND A.[FNPackSeq] = {0}", Me.FNPackSeq.Value))

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = False Then
                '...add new pack seq / pack description / pack note
                tSql = ""
                tSql = "DECLARE @FNPackSeqMax AS INT;"
                tSql &= Environment.NewLine & "SELECT @FNPackSeqMax = (ISNULL(MAX(A.FNPackSeq),0) + 1)"
                tSql &= Environment.NewLine & "FROM HITECH_MERCHAN.dbo.TMERTOrderSub_Pack AS A"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & _FTOrderNoSrc & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & _FTSubOrderSrc & "'"
                tSql &= Environment.NewLine & "--PRINT '@FNPackSeqMax : ' + CONVERT(VARCHAR(10), @FNPackSeqMax);"
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]([FTOrderNo],[FTSubOrderNo],[FNPackSeq],[FTPackDescription],[FTPackNote],[FTImage]"
                tSql &= Environment.NewLine & "                                                                                                          ,[FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & " ,[FTUpdUser],[FDUpdDate],[FTUpdTime])"
                tSql &= Environment.NewLine & "VALUES ('" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "','" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "',@FNPackSeqMax,N'" & HI.UL.ULF.rpQuoted(Me.FTPackDescription.Text) & "',N'" & HI.UL.ULF.rpQuoted(Me.FTPackNote.Text) & "'"
                tSql &= Environment.NewLine & ",N'" & HI.UL.ULImage.SaveImage(Me.FTImagePacking, tFTImageName, "" & tW_SysPath & "\OrderNo\SubOrderNo\Packing\") & "'"
                tSql &= ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                tSql &= ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ")"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Else
                '--------------------------------------------- Represent After Revise History Factory Sub Order No. Packing -----------------------------------------------
                tSql = ""
                tSql = "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTPackDescription, A.FTPackNote, A.FTImage"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack] AS A WITH(NOLOCK)"
                tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
                tSql &= Environment.NewLine & "      AND A.FNPackSeq = " & CInt(Me.FNPackSeq.Value) & ";"

                oDBdtSubOrderNoPackAF = Nothing
                oDBdtSubOrderNoPackAF = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = ""
                tSql = "SELECT TOP 1 A.FTStateApprovedPacking FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"

                If HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0") = "0" Then
                    DTAfterRevisedPack = Nothing
                Else
                    DTAfterRevisedPack = HI.Conn.SQLConn.GetDataTable(tSqlRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                End If
                '---------------------------------------------------------------------------------------------------------------------------------------------------------

                '...Write changed log
                'Call PostSaveSubOrderNoPack("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Pack]", _FTOrderNoSrc & "|" & Me.FTSubOrderNo.Properties.Tag.ToString() & "|" & Me.FNHSysStyleId.Text & "|" & Me.otab.TabPages(Me.otab.SelectedTabPageIndex).Text.ToString() & "|" & Me.FNPackSeq_lbl.Text & " " & CStr(Me.FNPackSeq.Value))

            End If

            'start updateimage--------
            Dim _Qry As String
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
            HI.Conn.SQLConn.SqlConnectionOpen()

            Try

                _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERTOrderSub_Pack "
                _Qry &= vbCrLf & "  SET FBImage=@FBImage"
                _Qry &= vbCrLf & " WHERE FTOrderNo=@FTOrderNo "
                _Qry &= vbCrLf & " AND FTSubOrderNo=@FTSubOrderNo "
                _Qry &= vbCrLf & " AND FNPackSeq=@FNPackSeq "

                Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
                cmd.Parameters.AddWithValue("@FTOrderNo", _FTOrderNoSrc)
                cmd.Parameters.AddWithValue("@FTSubOrderNo", _FTSubOrderSrc)
                cmd.Parameters.AddWithValue("@FNPackSeq", Me.FNPackSeq.Value)

                Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)
                data = HI.UL.ULImage.ConvertImageToByteArray(FTImagePacking.Image, UL.ULImage.PicType.Employee)

                Dim p As New SqlParameter("@FBImage", SqlDbType.Image)
                p.Value = data

                cmd.Parameters.Add(p)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            Catch ex As Exception
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            End Try
            'end updateimage--------


            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("UPDATE A")
            oStrBuilder.AppendLine("SET A.FNPackSeq = B.FNRePackSeq")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS A LEFT JOIN (SELECT ROW_NUMBER() OVER(ORDER BY L1.FNPackSeq ASC) AS FNRePackSeq, L1.FNPackSeq, L1.FTOrderNo, L1.FTSubOrderNo")
            oStrBuilder.AppendLine("														                                                          FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Pack AS L1")
            oStrBuilder.AppendLine("														                                                          WHERE L1.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("																                                                        AND L1.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "') AS B ON A.FTOrderNo = B.FTSubOrderNo AND A.FTSubOrderNo = B.FTSubOrderNo")
            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'")
            oStrBuilder.AppendLine("      AND A.FNPackSeq = B.FNPackSeq;")

            tSql = ""
            tSql = oStrBuilder.ToString()

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            HI.TL.HandlerControl.ClearControl(Me.ogbSubOrderPackInfo)

            Call W_PRCbShowBrowseDataSubOrderPackInfo()

            Me.FTImagePacking.Image = Nothing

            Me.FTPackDescription.Focus()

            Return True
        Catch ex As Exception

            Return False
        End Try

    End Function

    Private Function W_PRCbVerifyBF_SaveDataSubOrderPack() As Boolean
        Dim bPass As Boolean = False
        Try
            If Me.FTPackDescription.Text.Trim() <> "" Then
                bPass = True
            Else
                HI.MG.ShowMsg.mInvalidData(HI.MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTPackDescription_lbl.Text)
                Me.FTPackDescription.Focus()
            End If

            Return bPass
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub PostSaveSubOrderNoPack(ByVal ptTableName As String, ByVal ptRefDocKey As String)
        Try
            '...record revise history when user adjust factory sub order no. packing process
            '...Audit log
            'HI.Auditor.CreateLog.CreateLogdata(oDBdtSubOrderNoPackBF, oDBdtSubOrderNoPackAF, Me.Name, ptTableName, ptRefDocKey)
            oDBdtSubOrderNoPackBF = Nothing
            oDBdtSubOrderNoPackAF = Nothing

            '...compare after revised packing sequence process 
            '===================================================================================================================================================================
            'SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNPackSeq, A.FTPackDescription, A.FTPackNote
            'FROM  [HITECH_MERCHAN]..TMERTOrderSub_Pack AS A (NOLOCK)
            'WHERE A.FTOrderNo = N'NI1402892' AND A.FTSubOrderNo = N'NI1402892-A';

            Dim bFlagRevisedPack As Boolean = False
            If (Not DTBeforeRevisedPack Is Nothing) Or (Not DTAfterRevisedPack Is Nothing) Then
                If Not bFlagRevisedPack And DTBeforeRevisedPack Is Nothing And (Not DTAfterRevisedPack Is Nothing And DTAfterRevisedPack.Rows.Count > 0) Then bFlagRevisedPack = True
                If Not bFlagRevisedPack And (Not DTBeforeRevisedPack Is Nothing And DTBeforeRevisedPack.Rows.Count > 0) And DTAfterRevisedPack Is Nothing Then bFlagRevisedPack = True

                If Not bFlagRevisedPack And Not DTBeforeRevisedPack Is Nothing And DTBeforeRevisedPack.Rows.Count > 0 And Not DTAfterRevisedPack Is Nothing And DTAfterRevisedPack.Rows.Count > 0 Then
                    '...validate FTOrderNo, FTSubOrderNo, FNPackSeq ==> FTPackDescription|FTPackNote
                    Dim tFTOrderNo As String, tFTSubOrderNo As String
                    Dim nFNPackSeq As Integer
                    Dim tFTPackDescription As String, tFTPackNote As String

                    For nLoopChkRevised As Integer = 0 To DTAfterRevisedPack.Rows.Count - 1
                        tFTOrderNo = "" : tFTSubOrderNo = ""
                        nFNPackSeq = 0
                        tFTPackDescription = "" : tFTPackNote = ""

                        tFTOrderNo = DTAfterRevisedPack.Rows(nLoopChkRevised)("FTOrderNo").ToString
                        tFTSubOrderNo = DTAfterRevisedPack.Rows(nLoopChkRevised)("FTSubOrderNo").ToString

                        nFNPackSeq = Val(DTAfterRevisedPack.Rows(nLoopChkRevised)("FNPackSeq"))

                        If Not DBNull.Value.Equals(DTAfterRevisedPack.Rows(nLoopChkRevised)("FTPackDescription")) Then
                            tFTPackDescription = DTAfterRevisedPack.Rows(nLoopChkRevised)("FTPackDescription").ToString
                        Else
                            tFTPackDescription = ""
                        End If

                        If Not DBNull.Value.Equals(DTAfterRevisedPack.Rows(nLoopChkRevised)("FTPackNote")) Then
                            tFTPackNote = DTAfterRevisedPack.Rows(nLoopChkRevised)("FTPackNote").ToString
                        Else
                            tFTPackNote = ""
                        End If

                        '...lookup in DTBeforeRevisedPack
                        For Each oDataRow As System.Data.DataRow In DTBeforeRevisedPack.Select("FTOrderNo = '" & HI.UL.ULF.rpQuoted(tFTOrderNo) & "' AND FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(tFTSubOrderNo) & "' AND FNPackSeq = " & nFNPackSeq)
                            '...validate FTPackDescription, FTPackNote
                            Dim tFTPackDescriptionBF As String, tFTPackNoteBF As String

                            tFTPackDescriptionBF = "" : tFTPackNoteBF = ""

                            If Not DBNull.Value.Equals(oDataRow.Item("FTPackDescription")) Then
                                tFTPackDescriptionBF = oDataRow.Item("FTPackDescription").ToString
                            Else
                                tFTPackDescriptionBF = ""
                            End If

                            If Not tFTPackDescription.Equals(tFTPackDescriptionBF) Then
                                bFlagRevisedPack = True
                                Exit For
                            End If

                            If Not DBNull.Value.Equals(oDataRow.Item("FTPackNote")) Then
                                tFTPackNoteBF = oDataRow.Item("FTPackNote").ToString
                            Else
                                tFTPackNoteBF = ""
                            End If

                            If Not tFTPackNote.Equals(tFTPackNoteBF) Then
                                bFlagRevisedPack = True
                                Exit For
                            End If

                        Next

                        If bFlagRevisedPack = True Then Exit For

                    Next nLoopChkRevised

                    If (Not bFlagRevisedPack) AndAlso DTBeforeRevisedPack.Rows.Count <> DTAfterRevisedPack.Rows.Count Then
                        bFlagRevisedPack = True
                    End If

                End If

            End If

            If bFlagRevisedPack = True Then
                '...update revised factory sub order no. sewing sequence information
                Dim tSQLRevised As String
                tSQLRevised = ""
                tSQLRevised = "DECLARE @FTOrderNo AS NVARCHAR(30);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FTSubOrderNo AS NVARCHAR(30);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FDRevisedInfoDate AS NVARCHAR(10);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoTime AS NVARCHAR(8);"
                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoBy AS NVARCHAR(50);"
                tSQLRevised &= Environment.NewLine & "SET @FTRevisedInfoBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                tSQLRevised &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "';"
                tSQLRevised &= Environment.NewLine & "SET @FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"
                tSQLRevised &= Environment.NewLine & "SELECT @FDRevisedInfoDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTRevisedInfoTime = CONVERT(VARCHAR(8), GETDATE(), 114)"
                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoBy : ' + @FTRevisedInfoBy;"
                tSQLRevised &= Environment.NewLine & "PRINT '@FDRevisedInfoDate : ' + @FDRevisedInfoDate;"
                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoTime : ' + @FTRevisedInfoTime;"
                tSQLRevised &= Environment.NewLine & "UPDATE A"
                tSQLRevised &= Environment.NewLine & "SET A.FTStateApprovedPacking = N'0'"
                tSQLRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                tSQLRevised &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                tSQLRevised &= Environment.NewLine & "      AND A.FTSubOrderNo = @FTSubOrderNo;"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSQLRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                End If

            End If

            DTBeforeRevisedPack = Nothing
            DTAfterRevisedPack = Nothing
            '===================================================================================================================================================================

        Catch ex As Exception
            '...
        End Try
    End Sub

    Private Sub ocmdeleterow_Click(sender As Object, e As EventArgs) Handles ocmdeleterow.Click
        If (otab.SelectedTabPage Is otbSubOrderComponent) Then
            Call DeleteDivertSubOrderComponent()
        End If

        If (otab.SelectedTabPage Is otpSubOrderSewInfo) Then
            'Call W_PRCbDeleteSubOrderSewing()
            Call DeleteDivertSubOrderSew()
        End If

        If (otab.SelectedTabPage Is otpSubOrderPackInfo) Then
            'Call W_PRCbDeleteSubOrderPack()
            Call DeleteDivertSubOrderPack()
        End If

        If (otab.SelectedTabPage Is otpSubOrderSizeSpec) Then
            If HI.ST.ValidateData.CloseJob(_FTOrderNoSrc) Then
                HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If
            If CheckCombine() = True Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถลบ เนื่องจากใบสั่งผลิตนี้ได้ มาจากการแบ่งงาน", 1511181420, Me.Text)
                Exit Sub
            End If
            If CheckCreateQASizeSpec() = True Then Exit Sub
            'Call W_PRCbDeleteDataSizeSpec()
            Call DeleteDivertSubOrderSizeSpec()
        End If

    End Sub

    Private Function DeleteDivertSubOrderComponent() As Boolean
        Try
            Dim Str As Integer
            Dim RowSelect As Integer = 0

            With Me.ogvOrderSubComponent
                If .FocusedRowHandle < 0 Then Return False

                RowSelect = .FocusedRowHandle

                Dim _StrDel As String = ""

                For Each i As Integer In .GetSelectedRows()
                    If _StrDel = "" Then
                        _StrDel = (Integer.Parse(.GetRowCellValue(i, "FNSeq"))).ToString
                        Str = (.GetRowCellValue(i, "FNSeq")).ToString
                    Else
                        _StrDel = _StrDel & "," & (Integer.Parse(.GetRowCellValue(i, "FNSeq"))).ToString
                    End If
                Next

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล Component ใช่หรือไม่ ?", 1407100001, _StrDel) = True Then
                    .DeleteSelectedRows()
                    'If Not DeleteDivertComponentRow(CType(Me.ogdOrderSubComponent.DataSource, System.Data.DataTable), RowSelect, Str) Then Return False
                End If

            End With
        Catch ex As Exception

        End Try
    End Function

    Private Function DeleteDivertComponentRow(ByVal _DTSrc As System.Data.DataTable, ByVal SelectRowIndx As Integer, ByVal Test As Integer) As Boolean
        Try
            '_DTSrc.Rows(SelectRowIndx).Delete()
            _DTSrc.AcceptChanges()


            'For LoopSeq As Integer = 0 To _DTSrc.Rows.Count - 1
            '    _DTSrc.Rows(LoopSeq).Item("FNSeqOrg") = LoopSeq + 1
            '    _DTSrc.Rows(LoopSeq).Item("FNSeq") = LoopSeq + 1
            'Next

            '_DTSrc.AcceptChanges()

            Me.ogdOrderSubComponent.DataSource = _DTSrc
            Me.ogdOrderSubComponent.Refresh()
            Me.ogvOrderSubComponent.RefreshData()

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function W_PRCbDeleteDataSizeSpec() As Boolean

        'If Me.FTSubOrderNoSrc.Properties.Tag.ToString() = "" Then Return False

        Try
            Dim oGridView As DevExpress.XtraGrid.Views.Grid.GridView
            Dim nRowIndx As Integer
            Dim nTopVisibleIndx As Integer

            oGridView = Me.ogdSizeSpec.Views(0)

            If Not oGridView Is Nothing Then
                nRowIndx = oGridView.FocusedRowHandle
                nTopVisibleIndx = oGridView.TopRowIndex
            End If

            If nRowIndx < 0 Then Return False

            If Not (oGridView.PostEditor() And oGridView.UpdateCurrentRow()) Then
                Return False
            End If

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then

                If Not W_PRCbDeleteSourceSizeSpec(CType(Me.ogdSizeSpec.DataSource, System.Data.DataTable), nRowIndx) Then Return False

                Dim oGridView1 As DevExpress.XtraGrid.Views.Grid.GridView = Me.ogdSizeSpec.Views(0)
                If nRowIndx >= oGridView1.RowCount Then nRowIndx = oGridView1.RowCount - 1
                oGridView1.FocusedRowHandle = nRowIndx

            End If

            Return True
        Catch ex As Exception
            'Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
            Return False
        End Try

    End Function

    Private Function W_PRCbDeleteSourceSizeSpec(ByVal oDBdtSrc As System.Data.DataTable, ByVal pnRowIndx As Integer) As Boolean

        If oDBdtSrc.Rows.Count = 0 Then Return True

        Try
            Dim tSql As String

            tSql = ""
            tSql = "SELECT TOP 1 A.FNSeq"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A"
            tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "'"
            tSql &= Environment.NewLine & "      AND A.FNSeq = " & pnRowIndx + 1
            tSql &= Environment.NewLine & "ORDER BY A.FNHSysMatSizeId ASC, A.FNSeq ASC; "

            Dim nCntRecord As Integer

            nCntRecord = Val(HI.Conn.SQLConn.GetField(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN, 0))

            If nCntRecord = 0 Then
                If oDBdtSrc.Rows(pnRowIndx).RowState <> DataRowState.Deleted Then
                    oDBdtSrc.Rows(pnRowIndx).Delete()

                    oDBdtSrc.AcceptChanges()

                    '...re-sequence source for datatable
                    For nLoopRowSrc As Integer = 0 To oDBdtSrc.Rows.Count - 1
                        oDBdtSrc.Rows(nLoopRowSrc).Item("FNSeq") = nLoopRowSrc + 1
                    Next nLoopRowSrc

                    oDBdtSrc.AcceptChanges()

                End If

                Me.ogdSizeSpec.DataSource = oDBdtSrc
                Me.ogdSizeSpec.Refresh()
                Me.ogvSizeSpec.RefreshData()

                Return False

            Else

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    '...delete all FNHSysMatSizeId data by sequence
                    Dim nFNSeqDel As Integer
                    Integer.TryParse(oDBdtSrc.Rows(pnRowIndx).Item("FNSeq"), nFNSeqDel)

                    tSql = ""
                    tSql = "DELETE A"
                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A"
                    tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
                    tSql &= Environment.NewLine & "      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSrc.Properties.Tag.ToString().Trim()) & "'"
                    tSql &= Environment.NewLine & "      AND A.FNSeq = " & nFNSeqDel & ";"

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    '...re-squence source from database merchan dbo.[TMERTOrderSub_SizeSpec]
                    Dim oStrBuilder As New System.Text.StringBuilder()

                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    oStrBuilder.AppendLine("UPDATE A")
                    oStrBuilder.AppendLine("SET A.FNSeq = B.FNReSeq")
                    oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A LEFT JOIN (SELECT DENSE_RANK() OVER(ORDER BY A.FNSeq) AS FNReSeq, A.FNSeq, A.FNHSysMatSizeId, A.FTSizeSpecDesc, A.FTSizeSpecExtension, A.FTTolerant, A.FTOrderNo, A.FTSubOrderNo")
                    oStrBuilder.AppendLine("													                                                                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_SizeSpec AS A")
                    oStrBuilder.AppendLine("													                                                                  WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
                    oStrBuilder.AppendLine("														                                                                    AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSrc.Properties.Tag.ToString().Trim()) & "') AS B ON A.FTOrderNo = B.FTOrderNo AND A.FTSubOrderNo = B.FTSubOrderNo")
                    oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'")
                    oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNoSrc.Properties.Tag.ToString().Trim()) & "'")
                    oStrBuilder.AppendLine("      AND A.FNSeq = B.FNSeq;")

                    tSql = ""
                    tSql = oStrBuilder.ToString()

                    If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    If oDBdtSrc.Rows(pnRowIndx).RowState <> DataRowState.Deleted Then
                        oDBdtSrc.Rows(pnRowIndx).Delete()
                    End If

                    oDBdtSrc.AcceptChanges()

                    '...re-sequence source for datatable
                    For nLoopRowSrc As Integer = 0 To oDBdtSrc.Rows.Count - 1
                        oDBdtSrc.Rows(nLoopRowSrc).Item("FNSeq") = nLoopRowSrc + 1
                    Next nLoopRowSrc

                    oDBdtSrc.AcceptChanges()

                    Me.ogdSizeSpec.DataSource = oDBdtSrc
                    Me.ogdSizeSpec.Refresh()
                    Me.ogvSizeSpec.RefreshData()

                    Return True
                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End Try

            End If

        Catch ex As Exception
            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            Return False
        End Try

    End Function

    Private Function CheckCombine() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Isnull(FTStateCombine,'0') AS FTStateCombine  From  TMERTOrder Where FTOrderNo='" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "'"
            Dim _State As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
            Return IIf(_State = "1", True, False)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteDivertSubOrderSew() As Boolean
        Try
            Dim RowSelect As Integer = 0

            With Me.ogvOrderSubSewing
                If .FocusedRowHandle < 0 Then Return False

                RowSelect = .FocusedRowHandle

                Dim _StrDel As String = ""

                For Each i As Integer In .GetSelectedRows()
                    If _StrDel = "" Then
                        _StrDel = (Integer.Parse(.GetRowCellValue(i, "FNSewSeq"))).ToString
                    Else
                        _StrDel = _StrDel & "," & (Integer.Parse(.GetRowCellValue(i, "FNSewSeq"))).ToString
                    End If
                Next

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล Sewing ใช่หรือไม่ ?", 1409100001, _StrDel) = True Then
                    .DeleteSelectedRows()
                    If Not DeleteDivertSewRow(CType(Me.ogdOrderSubSewing.DataSource, System.Data.DataTable), RowSelect) Then Return False
                End If

            End With
        Catch ex As Exception

        End Try
    End Function

    Private Function DeleteDivertSewRow(ByVal _DTSrc As System.Data.DataTable, ByVal SelectRowIndx As Integer) As Boolean
        Try
            '_DTSrc.Rows(SelectRowIndx).Delete()
            _DTSrc.AcceptChanges()

            For LoopSeq As Integer = 0 To _DTSrc.Rows.Count - 1
                _DTSrc.Rows(LoopSeq).Item("FNSewSeq") = LoopSeq + 1
            Next

            _DTSrc.AcceptChanges()

            Me.ogdOrderSubSewing.DataSource = _DTSrc
            Me.ogdOrderSubSewing.Refresh()
            Me.ogvOrderSubSewing.RefreshData()

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function DeleteDivertSubOrderPack() As Boolean
        Try
            Dim RowSelect As Integer = 0

            With Me.ogvOrderSubPack
                If .FocusedRowHandle < 0 Then Return False

                RowSelect = .FocusedRowHandle

                Dim _StrDel As String = ""

                For Each i As Integer In .GetSelectedRows()
                    If _StrDel = "" Then
                        _StrDel = (Integer.Parse(.GetRowCellValue(i, "FNPackSeq"))).ToString
                    Else
                        _StrDel = _StrDel & "," & (Integer.Parse(.GetRowCellValue(i, "FNPackSeq"))).ToString
                    End If
                Next

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล Packing ใช่หรือไม่ ?", 1409100002, _StrDel) = True Then
                    .DeleteSelectedRows()
                    If Not DeleteDivertPackRow(CType(Me.ogdOrderSubPack.DataSource, System.Data.DataTable), RowSelect) Then Return False
                End If

            End With
        Catch ex As Exception

        End Try
    End Function

    Private Function DeleteDivertPackRow(ByVal _DTSrc As System.Data.DataTable, ByVal SelectRowIndx As Integer) As Boolean
        Try
            '_DTSrc.Rows(SelectRowIndx).Delete()
            _DTSrc.AcceptChanges()

            For LoopSeq As Integer = 0 To _DTSrc.Rows.Count - 1
                _DTSrc.Rows(LoopSeq).Item("FNPackSeq") = LoopSeq + 1
            Next

            _DTSrc.AcceptChanges()

            Me.ogdOrderSubPack.DataSource = _DTSrc
            Me.ogdOrderSubPack.Refresh()
            Me.ogvOrderSubPack.RefreshData()

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function DeleteDivertSubOrderSizeSpec() As Boolean
        Try
            Dim RowSelect As Integer = 0

            With Me.ogvSizeSpec
                If .FocusedRowHandle < 0 Then Return False

                RowSelect = .FocusedRowHandle

                Dim _StrDel As String = ""

                For Each i As Integer In .GetSelectedRows()
                    If _StrDel = "" Then
                        _StrDel = (Integer.Parse(.GetRowCellValue(i, "FNSeq"))).ToString
                    Else
                        _StrDel = _StrDel & "," & (Integer.Parse(.GetRowCellValue(i, "FNSeq"))).ToString
                    End If
                Next

                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
                    .DeleteSelectedRows()
                    If Not DeleteDivertSizeSpecRow(CType(Me.ogdSizeSpec.DataSource, System.Data.DataTable), RowSelect) Then Return False
                End If

            End With
        Catch ex As Exception

        End Try
    End Function

    Private Function DeleteDivertSizeSpecRow(ByVal _DTSrc As System.Data.DataTable, ByVal SelectRowIndx As Integer) As Boolean
        Try
            '_DTSrc.Rows(SelectRowIndx).Delete()
            _DTSrc.AcceptChanges()

            For LoopSeq As Integer = 0 To _DTSrc.Rows.Count - 1
                _DTSrc.Rows(LoopSeq).Item("FNSeq") = LoopSeq + 1
            Next

            _DTSrc.AcceptChanges()

            Me.ogdSizeSpec.DataSource = _DTSrc
            Me.ogdSizeSpec.Refresh()
            Me.ogvSizeSpec.RefreshData()

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Function W_PRCbDeleteSubOrderSewing() As Boolean
        Try
            With Me.ogvOrderSubSewing

                If .FocusedRowHandle < 0 Then Return False

                Dim _StrDel As String = ""

                For Each i As Integer In .GetSelectedRows()
                    If _StrDel = "" Then
                        _StrDel = (Integer.Parse(.GetRowCellValue(i, "FNSewSeq"))).ToString
                    Else
                        _StrDel = _StrDel & "," & (Integer.Parse(.GetRowCellValue(i, "FNSewSeq"))).ToString
                    End If
                Next

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล Sewing ใช่หรือไม่ ?", 1409100001, _StrDel) = True Then

                    Dim FNSeq As Integer

                    Dim _Qry As String = ""

                    For Each i As Integer In .GetSelectedRows()

                        FNSeq = Integer.Parse(.GetRowCellValue(i, "FNSewSeq"))


                        REM ...ให้ Use ทำการ Re-Seq Manual
                        '==============================================================================================================================================
                        '_Qry &= vbCrLf & "UPDATE A SET FNSewSeq = FNNo"
                        '_Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub_Sew]  AS A  INNER JOIN "
                        '_Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSewSeq) AS FNNo,FNSewSeq,FTOrderNo,FTSubOrderNo"
                        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub_Sew]  "
                        '_Qry &= vbCrLf & " WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "' "
                        '_Qry &= vbCrLf & "       AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text.Trim) & "' "
                        '_Qry &= vbCrLf & ") T1 ON A.FNSewSeq = T1.FNSewSeq AND A.FTOrderNo=T1.FTOrderNo AND  A.FTSubOrderNo=T1.FTSubOrderNo "
                        '==============================================================================================================================================

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                            '...validate revised sewing sequence
                            '...update revised size sewing information
                            '======================================================================================================================================================================================
                            Dim tSQLRevised As String

                            tSQLRevised = ""
                            tSQLRevised = "SELECT TOP 1 A.FTStateApprovedSewing FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"

                            If HI.Conn.SQLConn.GetField(tSQLRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0") = "1" Then '...revised sewing sequence process
                                tSQLRevised = ""
                                tSQLRevised = "DECLARE @FTOrderNo AS NVARCHAR(30);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FTSubOrderNo AS NVARCHAR(30);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FDRevisedInfoDate AS NVARCHAR(10);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoTime AS NVARCHAR(8);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoBy AS NVARCHAR(50);"
                                tSQLRevised &= Environment.NewLine & "SET @FTRevisedInfoBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                                tSQLRevised &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "';"
                                tSQLRevised &= Environment.NewLine & "SET @FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"
                                tSQLRevised &= Environment.NewLine & "SELECT @FDRevisedInfoDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTRevisedInfoTime = CONVERT(VARCHAR(8), GETDATE(), 114)"
                                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoBy : ' + @FTRevisedInfoBy;"
                                tSQLRevised &= Environment.NewLine & "PRINT '@FDRevisedInfoDate : ' + @FDRevisedInfoDate;"
                                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoTime : ' + @FTRevisedInfoTime;"
                                tSQLRevised &= Environment.NewLine & "UPDATE A"
                                tSQLRevised &= Environment.NewLine & "SET A.FTStateApprovedSewing = N'0'"
                                tSQLRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                                tSQLRevised &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                                tSQLRevised &= Environment.NewLine & "      AND A.FTSubOrderNo = @FTSubOrderNo;"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSQLRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                                End If
                            End If
                            '======================================================================================================================================================================================

                        End If

                    Next

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    HI.TL.HandlerControl.ClearControl(Me.ogbSubOrderSewInfo)
                    Call W_PRCbShowBrowseDataSubOrderSewingInfo()
                    Me.FTImageSewing.Image = Nothing
                    Me.FTSewDescription.Focus()

                    Return True

                End If

            End With

        Catch ex As Exception

        End Try

        Return True
    End Function

    Private Function W_PRCbDeleteSubOrderPack() As Boolean

        Try
            With Me.ogvOrderSubPack

                If .FocusedRowHandle < 0 Then Return False

                Dim _StrDel As String = ""

                For Each i As Integer In .GetSelectedRows()
                    If _StrDel = "" Then
                        _StrDel = (Integer.Parse(.GetRowCellValue(i, "FNPackSeq"))).ToString
                    Else
                        _StrDel = _StrDel & "," & (Integer.Parse(.GetRowCellValue(i, "FNPackSeq"))).ToString
                    End If
                Next

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล Packing ใช่หรือไม่ ?", 1409100002, _StrDel) = True Then

                    Dim FNSeq As Integer

                    Dim _Qry As String = ""

                    For Each i As Integer In .GetSelectedRows()

                        FNSeq = Integer.Parse(.GetRowCellValue(i, "FNPackSeq"))

                        _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub_Pack]"
                        _Qry &= vbCrLf & "WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' "
                        _Qry &= vbCrLf & "      AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "' "
                        _Qry &= vbCrLf & "      AND FNPackSeq = " & FNSeq & "  "

                        REM ...ให้ User ทำการ Re-Sequence Manual
                        '================================================================================================================================================
                        '_Qry &= vbCrLf & "UPDATE A SET FNPackSeq = FNNo"
                        '_Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub_Pack]  AS A  INNER JOIN "
                        '_Qry &= vbCrLf & " (SELECT ROW_NUMBER() OVER(ORDER BY FNPackSeq) AS FNNo,FNPackSeq,FTOrderNo,FTSubOrderNo"
                        '_Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrderSub_Pack]  "
                        '_Qry &= vbCrLf & "  WHERE FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "' "
                        '_Qry &= vbCrLf & "        AND FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text.Trim) & "' "
                        '_Qry &= vbCrLf & ") T1 ON A.FNPackSeq=T1.FNPackSeq AND A.FTOrderNo=T1.FTOrderNo AND  A.FTSubOrderNo = T1.FTSubOrderNo "
                        '================================================================================================================================================

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                            '...validate revised packing sequence
                            '...update revised size packing information
                            '======================================================================================================================================================================================
                            Dim tSQLRevised As String

                            tSQLRevised = ""
                            tSQLRevised = "SELECT TOP 1 A.FTStateApprovedPacking FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"

                            If HI.Conn.SQLConn.GetField(tSQLRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0") = "1" Then '...revised sewing sequence process
                                tSQLRevised = ""
                                tSQLRevised = "DECLARE @FTOrderNo AS NVARCHAR(30);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FTSubOrderNo AS NVARCHAR(30);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FDRevisedInfoDate AS NVARCHAR(10);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoTime AS NVARCHAR(8);"
                                tSQLRevised &= Environment.NewLine & "DECLARE @FTRevisedInfoBy AS NVARCHAR(50);"
                                tSQLRevised &= Environment.NewLine & "SET @FTRevisedInfoBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"
                                tSQLRevised &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNoSrc) & "';"
                                tSQLRevised &= Environment.NewLine & "SET @FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTSubOrderSrc) & "';"
                                tSQLRevised &= Environment.NewLine & "SELECT @FDRevisedInfoDate = CONVERT(VARCHAR(10), GETDATE(), 111), @FTRevisedInfoTime = CONVERT(VARCHAR(8), GETDATE(), 114)"
                                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoBy : ' + @FTRevisedInfoBy;"
                                tSQLRevised &= Environment.NewLine & "PRINT '@FDRevisedInfoDate : ' + @FDRevisedInfoDate;"
                                tSQLRevised &= Environment.NewLine & "PRINT '@FTRevisedInfoTime : ' + @FTRevisedInfoTime;"
                                tSQLRevised &= Environment.NewLine & "UPDATE A"
                                tSQLRevised &= Environment.NewLine & "SET A.FTStateApprovedPacking = N'0'"
                                tSQLRevised &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A"
                                tSQLRevised &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                                tSQLRevised &= Environment.NewLine & "      AND A.FTSubOrderNo = @FTSubOrderNo;"

                                If HI.Conn.SQLConn.ExecuteNonQuery(tSQLRevised, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

                                End If
                            End If
                            '======================================================================================================================================================================================
                        End If

                    Next

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    HI.TL.HandlerControl.ClearControl(Me.ogbSubOrderSewInfo)
                    Call W_PRCbShowBrowseDataSubOrderPackInfo()
                    'Me.FTImageSewing.Image = Nothing
                    'Me.FTSewDescription.Focus()
                    Me.FTImagePacking.Image = Nothing
                    Me.FTPackDescription.Focus()

                    Return True

                End If

            End With

        Catch ex As Exception
        End Try

        Return True

    End Function

    Private Sub otab_SelectedPageChanged(sender As Object, e As XtraTab.TabPageChangedEventArgs) Handles otab.SelectedPageChanged
        If (otab.SelectedTabPage Is otbSubOrderComponent) Or (otab.SelectedTabPage Is otpSubOrderSewInfo) Or (otab.SelectedTabPage Is otpSubOrderPackInfo) Or (otab.SelectedTabPage Is otpSubOrderSizeSpec) Then
            ocmaddrow.Visible = True
            ocmdeleterow.Visible = True
            ocmcopy.Location = New Point(193, 2)
        Else
            ocmaddrow.Visible = False
            ocmdeleterow.Visible = False
            ocmcopy.Location = New Point(5, 2)
        End If

        If (otab.SelectedTabPage Is otpOrderBreakdown) Then
            ocmcopy.Visible = False
        Else
            ocmcopy.Visible = True
        End If
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        Dim _Dt As DataTable

        'If (otab.SelectedTabPage Is otpSubOrderSewInfo) Then
        Call CopyDivertTab(ogdOrderSubComponent.DataSource, ogdOrderSubSewing.DataSource, ogdOrderSubPack.DataSource, ogdSizeSpec.DataSource, 1)
            'ogdOrderSubSewing.DataSource = _Dt
            FNSewSeq.Text = ogvOrderSubSewing.RowCount + 1
            Me.ogvOrderSubSewing.RefreshData()
            Me.ogvOrderSubSewing.OptionsView.ColumnAutoWidth = False
            Me.ogvOrderSubSewing.BestFitColumns()
        'End If

        'If (otab.SelectedTabPage Is otpSubOrderPackInfo) Then
        '    Call CopyDivertTab(ogdOrderSubPack.DataSource, 2)
        FNPackSeq.Text = ogvOrderSubPack.RowCount + 1
        Me.ogvOrderSubPack.RefreshData()
        Me.ogvOrderSubPack.OptionsView.ColumnAutoWidth = False
        Me.ogvOrderSubPack.BestFitColumns()
        'End If

        'If (otab.SelectedTabPage Is otpSubOrderPakingCartonInfo) Then
        '    Call CopyDivertTab(ogdOrderSubPackCarton.DataSource, 3)
        'End If

        'If (otab.SelectedTabPage Is otpSubOrderSizeSpec) Then
        '    Call CopyDivertTab(ogdSizeSpec.DataSource, 4)
        'End If

        'If (otab.SelectedTabPage Is otbSubOrderComponent) Then
        '    Call CopyDivertTab(ogdOrderSubComponent.DataSource, 5)
        '    'ogdOrderSubSewing.DataSource = _Dt
        Me.ogdOrderSubComponent.Refresh()
        Me.ogvOrderSubComponent.RefreshData()
        Me.ogvOrderSubComponent.OptionsView.ColumnAutoWidth = False
        'End If
    End Sub

    Private Function CopyDivertTab(ByRef _CDt As DataTable, ByRef _SDt As DataTable, ByRef _PDt As DataTable, ByRef _DtSelect As DataTable, _TabSelected As Integer)
        Dim _Dt As DataTable

        _CopyDivert = New wCopyDivertOrder(_DtSelect, _TabSelected)

        HI.TL.HandlerControl.AddHandlerObj(_CopyDivert)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyDivert.Name.ToString.Trim, _CopyDivert)
        Catch ex As Exception
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_CopyDivert)

        _CopyDivert.ShowDialog()

        If Not (_CopyDivert._ComDt Is Nothing) Then

            ogdOrderSubComponent.DataSource = _CopyDivert._ComDt.Copy()

        End If

        If Not (_CopyDivert._SewDt Is Nothing) Then

            ogdOrderSubSewing.DataSource = _CopyDivert._SewDt.Copy

        End If

        If Not (_CopyDivert._PacDt Is Nothing) Then

            ogdOrderSubPack.DataSource = _CopyDivert._PacDt.Copy()

        End If

        If Not (_CopyDivert._DDt Is Nothing) Then

            _Dt = _CopyDivert._DDt
            CopySizeSpec(_DtSelect, _Dt)

        End If

        'Select Case _TabSelected
        '    Case 1, 2, 5
        '        _DtSelect = _CopyDivert._DDt
        '    Case 3
        '        _Dt = _CopyDivert._DDt
        '        CopyPackingCarton(_DtSelect, _Dt)
        '    Case 4
        '        _Dt = _CopyDivert._DDt
        '        CopySizeSpec(_DtSelect, _Dt)
        'End Select

        'If (_TabSelected = 1 Or _TabSelected = 2) Then
        '_DtSelect = _CopyDivert._DDt
        'ElseIf (_TabSelected = 3) Then
        '_Dt = _CopyDivert._DDt
        'CopyPackingCarton(_DtSelect, _Dt)
        'Else
        '_Dt = _CopyDivert._DDt
        'CopySizeSpec(_DtSelect, _Dt)
        'End If

    End Function

    Private Function CopyPackingCarton(ByRef _oDt As DataTable, _NDt As DataTable)
        For Each R As DataRow In _oDt.Rows
            For Each Col As DataColumn In _oDt.Columns
                'MessageBox.Show(R!FTColorway.ToString())
                Select Case Col.ColumnName.ToString.ToUpper
                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                    Case Else
                        If (_NDt.Rows.Count <> 0) Then
                            For Each _R As DataRow In _NDt.Rows
                                If (R!FTColorway = _R!FTColorway) Then
                                    If (Col.ToString = _R!FTSizeBreakDown.ToString) Then
                                        R(Col) = Integer.Parse(_R!FNQuantity.ToString)
                                    End If
                                End If
                            Next
                        Else
                            R(Col) = 0
                        End If
                End Select
            Next
        Next
    End Function

    Private Function CopySizeSpec(ByRef _oDt As DataTable, _NDt As DataTable)
        Try
            Dim extenname As String = ""
            Dim NewRow As Integer
            Dim OldRow As Integer = _oDt.Rows.Count

            If (_NDt.Rows.Count > 0) Then
                NewRow = Integer.Parse(_NDt.Rows(_NDt.Rows.Count - 1).Item("FNSeq").ToString())
                If (_oDt.Rows.Count < NewRow) Then
                    For row As Integer = OldRow To NewRow - 1
                        Call W_PRCxInitNewRowSizeSpec(_oDt)
                    Next
                ElseIf (_oDt.Rows.Count > NewRow) Then
                    For row As Integer = OldRow To NewRow + 1 Step -1
                        Call W_PRCbDeleteSourceSizeSpec(_oDt, row - 1)
                        If (_oDt.Rows.Count = NewRow) Then Exit For
                    Next

                End If

                For Each R As DataRow In _oDt.Rows
                    For Each Col As DataColumn In _oDt.Columns
                        Dim _size As String = ""

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FNHSysMeasId_hide".ToUpper, "FNHSysMatSizeId".ToUpper, "FTSizeSpecDesc".ToUpper, "FNHSysMeasId".ToUpper, "FNSeq".ToUpper, extenname.ToUpper, "FTSizeSpecTolerant".ToUpper
                            Case Else
                                Dim ColN As String = ""
                                '_size.Count
                                ColN = Col.ToString.ToUpper
                                _size = Mid(ColN, 16)
                                'If (Col.ToString.Count = 16) Then
                                '    '_size = Col.ToString.ToUpper.Substring(15, 1)
                                'Else
                                '    '_size = Col.ToString.ToUpper.Substring(15, 2)
                                'End If
                                'MessageBox.Show(_size)
                                extenname = "FTSizeSpecExtension" & _size
                                For Each _R As DataRow In _NDt.Rows

                                    If (R!FNSeq.ToString = _R!FNSeq.ToString) Then
                                        R!FTSizeSpecDesc = _R!FTSizeSpecDesc
                                        If (_R!FNHSysMeasId.ToString <> "0" Or IsDBNull(_R!FNHSysMeasId)) Then
                                            R!FNHSysMeasId = _R!FNHSysMeasId
                                        End If

                                        If (R("FNHSysMatSizeId" & _size).ToString = _R!FNHSysMatSizeId.ToString) Then
                                            R(extenname) = _R!FTSizeSpecExtension
                                            R!FTSizeSpecTolerant = _R!FTTolerant
                                            Exit For
                                        End If
                                    End If
                                Next
                        End Select
                    Next
                Next
                Call W_PRCxInitNewRowSizeSpec(_oDt)
            Else
                'HI.MG.ShowMsg.mProcessError(141115, "ไม่มีข้อมูลในตาราง.....", Me.Text, MessageBoxIcon.Error)
            End If
        Catch

        End Try
    End Function

    Private Sub FNPackCartonSubType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNPackCartonSubType.SelectedIndexChanged
        Try
            Dim _StateEdit As Boolean = False

            Select Case FNPackCartonSubType.SelectedIndex
                Case 0
                    _StateEdit = False
                Case Else
                    '...Assort Color / Solid Size
                    '...Solid  Color / Assort Size
                    '...Assort Color / Assort Size
                    _StateEdit = True '...สามารถระบุจำนวนตัวต่อกล่อง ตาม สี / ตาม ไซส์ ได้ ใน Grid View
            End Select

            With Me.ogvOrderSubPackCarton

                .BeginInit()

                For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    Select Case oGridCol.FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                            '...do nothing
                        Case Else
                            With oGridCol

                                .OptionsColumn.AllowEdit = _StateEdit
                                .OptionsColumn.ReadOnly = Not (_StateEdit)

                                If _StateEdit Then
                                    .AppearanceCell.BackColor = Color.LightCyan
                                    .AppearanceCell.ForeColor = Color.Blue
                                Else
                                    .AppearanceCell.BackColor = Color.White
                                    .AppearanceCell.ForeColor = Color.Black
                                End If

                            End With

                    End Select
                Next

                .EndInit()

            End With

            Me.FNPackPerCaton.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvOrderSubComponent_DoubleClick(sender As Object, e As EventArgs) Handles ogvOrderSubComponent.DoubleClick
        Dim matcode As String = ""
        Dim Matseq As Double = 0
        Dim component As String = ""
        Dim consmp As Double = 0
        Dim remark As String = ""
        Dim _Dt As DataTable

        _Dt = ogdOrderSubComponent.DataSource

        With Me.ogvOrderSubComponent
            matcode = "" & .GetFocusedRowCellValue("FTMainMatCode").ToString
            Matseq = Val("" & .GetFocusedRowCellValue("FNSeqOrg").ToString)
            component = "" & .GetFocusedRowCellValue("FTComponent").ToString
            consmp = Val("" & .GetFocusedRowCellValue("FNConSmp").ToString)
            remark = "" & .GetFocusedRowCellValue("FTRemark").ToString
        End With

        With _wAddDivertOrderSubComponent
            HI.TL.HandlerControl.ClearControl(_wAddDivertOrderSubComponent)
            .AddComponent = False
            .FNConSmp.Value = consmp
            .FNSeq.Value = Matseq
            .FNHSysMerMatId.Text = matcode
            .FTComponent.Text = component
            .FTRemark.Text = remark
            .FNSeq.Enabled = False
            .ShowDialog()

            If (.AddComponent) Then
                For Each R As DataRow In _Dt.Select("FNSeq=" & Matseq & "")
                    If (R!FNSeq.ToString = Matseq.ToString) Then
                        R!FTMainMatCode = .FNHSysMerMatId.Text
                        R!FTMainMatDesc = .FNHSysMerMatId_None.Text
                        R!FTComponent = .FTComponent.Text
                        R!FNConSmp = .FNConSmp.Value
                        R!FTRemark = .FTRemark.Text
                        R!FNHSysMerMatId = Val(.FNHSysMerMatId.Properties.Tag)
                    End If

                Next
            End If

            ogdOrderSubComponent.DataSource = _Dt
            ogdOrderSubComponent.Refresh()
            ogvOrderSubComponent.RefreshData()
        End With
    End Sub
End Class