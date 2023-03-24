Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors.Controls

Public Class wProdCostCharge
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True


    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldSumAmt As String = "FNNetAmt|FNAmount"
        Dim sFieldGrpSumAmt As String = "FNNetAmt"

        'With ogvallocate
        '    .ClearGrouping()
        '    .ClearDocument()
        '    .Columns("FTPurchaseNo").Group()

        '    For Each Str As String In sFieldSumAmt.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSumAmt.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .OptionsView.ShowAutoFilterRow = False
        '    .ExpandAllGroups()
        '    .RefreshData()

        'End With

        'With ogvallocate
        '    .ClearGrouping()
        '    .ClearDocument()
        '    '  .Columns("FTPurchaseNo").Group()

        '    .Columns("FNNetAmt").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNNetAmt")
        '    .Columns("FNNetAmt").SummaryItem.DisplayFormat = "{0:n2}"
        '    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNNetAmt", Nothing, "(Summary by " & .Columns.ColumnByFieldName("FNNetAmt").Caption & "={0:n2})")
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    ' .OptionsView.ShowFooter = False
        '    '.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .OptionsView.ShowGroupPanel = True
        '    .OptionsView.ShowAutoFilterRow = False

        '    ' Make the group footers always visible.
        '    '  .GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
        '    ' Create and setup the first summary item.
        '    'Dim item As GridGroupSummaryItem = New GridGroupSummaryItem()
        '    'item.FieldName = "FNNetAmt"
        '    'item.SummaryType = DevExpress.Data.SummaryItemType.Count
        '    '.GroupSummary.Add(item)
        '    ' Create and setup the second summary item.

        '    For Each Str As String In sFieldSumAmt.Split("|")
        '        If Str <> "" Then
        '            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
        '            item1.FieldName = Str
        '            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '            item1.DisplayFormat = "Summary by " & .Columns.ColumnByFieldName(Str).Caption & " {0:n2}"
        '            item1.ShowInGroupColumnFooter = .Columns(Str)
        '            .GroupSummary.Add(item1)
        '        End If
        '    Next
        '    .ExpandAllGroups()
        '    .RefreshData()
        'End With

        ''------End Add Summary Grid-------------


    End Sub
#End Region

#Region "Property"
    Private _WHID As Integer
    Public Property WH As Integer
        Get
            Return _WHID
        End Get
        Set(value As Integer)
            _WHID = value
        End Set
    End Property

    Private _WHIDTo As Integer
    Public Property WHTo As Integer
        Get
            Return _WHIDTo
        End Get
        Set(value As Integer)
            _WHIDTo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _OrderNoTo As String = ""
    Public Property OrderNoTo As String
        Get
            Return _OrderNoTo
        End Get
        Set(value As String)
            _OrderNoTo = value
        End Set
    End Property

    Private _DocRefNo As String = ""
    Public Property DocRefNo As String
        Get
            Return _DocRefNo
        End Get
        Set(value As String)
            _DocRefNo = value
        End Set
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(value As String)
            _MainKeyID = value
        End Set
    End Property

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property

    Private _RequireField As String = ""
    Public Property RequireField As String
        Get
            Return _RequireField
        End Get
        Set(value As String)
            _RequireField = value
        End Set
    End Property

    Public ReadOnly Property Query As String
        Get
            Return _FormHeader(0).Query
        End Get
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub InitFormControl()

        Dim _Str As String = ""
        Dim _objId As Integer
        Dim _dt As DataTable
        Dim _StrQuery As String = ""
        Dim _SortField As String = ""
        Dim _ColCount As Integer = 0
        Dim _StartX As Double = 0
        Dim _StartY As Double = 0
        Dim _CtrLv As Double = -1
        Dim _CtrHeight As Double = 0
        Dim _dtgrpobj As New DataTable


        _Str = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTDynamicFormName='" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Str = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Str &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

            _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

            If _dt.Rows.Count > 0 Then

                For Each Row As DataRow In _dtgrpobj.Rows
                    Select Case Row!FNGenFormObj.ToString
                        Case "H"
                            Dim _DMF As New HI.TL.DynamicForm(_objId, Val(Row!FNFormObjID.ToString), _dt, Me)
                            _DMF.SysObjID = Val(Row!FNFormObjID.ToString)
                            _DMF.SysTableName = Row!FTTableName.ToString
                            _DMF.SysDBName = Row!FTBaseName.ToString
                            _FormHeader.Add(_DMF)

                    End Select
                Next
            End If

        End If

        _dt.Dispose()
        _dtgrpobj.Dispose()

    End Sub
    Public Sub LoadDataInfo(Key As Object, SuplKey As Object)
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        HI.TL.HandlerControl.ClearControl(Me.ogbinvoicechargedetail)
        HI.TL.HandlerControl.ClearControl(Me.ogbnote)



        Dim _FieldName As String = ""
        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With

                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try

                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next

        If _Dt.Rows.Count <= 0 Then
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        End If




        LoadCharge()





        _ProcLoad = False
    End Sub



    Private Sub LoadCharge()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select C.FNHSysPIChageId  , C.FNPIType ,A.FNAmount as FNAmount ,  A.FTRemark as FTRemark "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", C.FTChageNameTH as FTChargeDesc"
            Else
                _Cmd &= vbCrLf & ", C.FTChageNameEN as FTChargeDesc"
            End If
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TEXPChage AS C  With(NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "(Select * From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTProdCost_Charge WITH(NOLOCK) "
            _Cmd &= vbCrLf & " Where FTProdCostNo ='" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "' )  As A On C.FNHSysPIChageId = A.FNHSysPIChageId"
            _Cmd &= vbCrLf & "where isnull(C.FTStateActive,'0') = '1' and C.FTStateProdCost='1'"
            '_Cmd &= vbCrLf & " and C.FNPIType=" & Integer.Parse("0" & Me.FNPIChargeType.SelectedIndex)

            Me.ogcCharge.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub LoadDataDetail(Key As String)
    '    Dim _Qry As String = ""
    '    Dim dt As DataTable
    '    Dim _PO As String = ""
    '    Dim Str As String = ""
    '    Dim _Inv As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTInvoiceNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Invoice_detail WITH(NOLOCK) WHERE FTInvoiceBankNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "")

    '    If _Inv <> "" Or Me.FTInvoiceNo.Text <> "" Then
    '        With CType(ogcinvoice.DataSource, DataTable)
    '            .AcceptChanges()

    '            For Each R As DataRow In .Rows
    '                If Str <> "" Then Str &= ","
    '                Str &= "'" & R!FTInvoiceNo.ToString & "'"
    '            Next
    '        End With
    '    End If

    '    With DirectCast(Me.ogcpurchase.DataSource, DataTable)
    '        .AcceptChanges()

    '        For Each R As DataRow In .Rows
    '            If _PO <> "" Then _PO &= ","
    '            _PO &= "'" & R!FTPurchaseNo.ToString & "'"
    '        Next
    '    End With

    '    'Dim _CIn As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTInvoiceNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WITH(NOLOCK) WHERE FTPurchaseNo in (" & _PO & ") ", Conn.DB.DataBaseName.DB_MASTER, "")
    '    Select Case Me.FNPIChargeType.SelectedIndex
    '        Case 0
    '            '  If _Inv = "" And Me.FTInvoiceNo.Text = "" Then

    '            If _Inv = "" And Me.FTInvoiceNo.Text = "" Then
    '                _Qry = "    Select Max(FTCurCode) AS FTCurCode,SUM(FNNetAmt) AS FNNetAmtCur ,SUM(Convert(numeric(18, 2), FNExchangeRate * FNNetAmt)) AS FNNetAmt"
    '                _Qry &= vbCrLf & " FROM (SELECT P.FNExchangeRate , SUM(P.FNPOGrandAmt) AS FNNetAmt,MAX(ISNULL(CU.FTCurCode,'')) AS FTCurCode"
    '                _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK)  "
    '                _Qry &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON P.FNHSysCurId = CU.FNHSysCurId"
    '                _Qry &= vbCrLf & " WHERE  (P.FTPurchaseNo in (" & _PO & ")) "
    '                _Qry &= vbCrLf & "  group by P.FNExchangeRate  ) AS A"
    '            Else
    '                _Qry = "    Select Max(FTCurCode) AS FTCurCode,SUM(FNNetAmt) AS FNNetAmtCur ,SUM(Convert(numeric(18, 2), FNExchangeRate * FNNetAmt)) AS FNNetAmt,FNDebitCreditGrandAmt as FNDebitCredit"
    '                _Qry &= vbCrLf & " FROM (SELECT P.FNExchangeRate , isnull(SUM(RD.FNNetAmt)+((SUM(RD.FNNetAmt)*P.FNVatPer)/100),0) AS FNNetAmt,MAX(ISNULL(CU.FTCurCode,'')) AS FTCurCode,isnull(D.FNDebitCreditGrandAmt,0)as FNDebitCreditGrandAmt"
    '                _Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK)  "
    '                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit_Purchase AS DP ON P.FTPurchaseNo =DP.FTPurchaseNo " 'and D.FTDebitCreditNo=DP.FTDebitCreditNo"
    '                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit AS D ON P.FNHSysSuplId =D.FNHSysSuplId and DP.FTDebitCreditNo=D.FTDebitCreditNo "
    '                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit_Invoice AS DI ON DP.FTDebitCreditNo=DI.FTDebitCreditNo"
    '                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS RC WITH(NOLOCK) ON  DP.FTPurchaseNo = RC. FTPurchaseNo  and DI.FTInvoiceNo =RC.FTInvoiceNo or P.FTPurchaseNo = RC. FTPurchaseNo   "
    '                _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive_Detail AS RD WITH(NOLOCK) ON  RC.FTReceiveNo=RD.FTReceiveNo   "
    '                _Qry &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON P.FNHSysCurId = CU.FNHSysCurId"
    '                ' _Qry &= vbCrLf & " WHERE  (P.FTPurchaseNo in (" & Key.ToString & ")) "
    '                _Qry &= vbCrLf & " WHERE  (RC.FTInvoiceNo in (" & Str.ToString & ")) and RC.FTPurchaseNo in (" & _PO & ") "
    '                _Qry &= vbCrLf & "  group by P.FNExchangeRate,P.FNVatPer,D.FNDebitCreditGrandAmt    ) AS A"
    '                _Qry &= vbCrLf & "  group by FNDebitCreditGrandAmt"
    '            End If
    '            'Else
    '            '_Qry = "    Select Max(FTCurCode) AS FTCurCode,SUM(FNNetAmt) AS FNNetAmtCur ,SUM(Convert(numeric(18, 2), FNExchangeRate * FNNetAmt)) AS FNNetAmt,FNDebitCreditGrandAmt as FNDebitCredit"
    '            '_Qry &= vbCrLf & "  FROM (SELECT P.FNExchangeRate , isnull(SUM(RD.FNNetAmount),0) AS FNNetAmt,MAX(ISNULL(CU.FTCurCode,'')) AS FTCurCode,D.FNDebitCreditGrandAmt"
    '            '_Qry &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK)  "
    '            '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Purchase_detail AS RC WITH(NOLOCK) ON  P.FTPurchaseNo = RC. FTPurchaseNo  "
    '            '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge AS RD WITH(NOLOCK) ON  RC.FTInvoiceBankNo=RD.FTInvoiceBankNo"
    '            '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTInvoiceBankCharge_Invoice_detail AS RI WITH(NOLOCK) ON  RC.FTInvoiceBankNo=RI.FTInvoiceBankNo"
    '            '_Qry &= vbCrLf & "       LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CU WITH(NOLOCK) ON P.FNHSysCurId = CU.FNHSysCurId"
    '            '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit_Purchase AS DP ON RC.FTPurchaseNo =DP.FTPurchaseNo"
    '            '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit_Invoice AS DI ON RI.FTInvoiceNo =DI.FTInvoiceNo "
    '            '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit AS D ON P.FNHSysSuplId =D.FNHSysSuplId and DP.FTDebitCreditNo=D.FTDebitCreditNo "
    '            '' _Qry &= vbCrLf & " WHERE  (P.FTPurchaseNo in (" & Key.ToString & ")) "
    '            '_Qry &= vbCrLf & " WHERE  (RI.FTInvoiceNo in (" & Str.ToString & ")) and RC.FTPurchaseNo in (" & _PO & ") "
    '            '_Qry &= vbCrLf & "  group by P.FNExchangeRate,D.FNDebitCreditGrandAmt    ) AS A"
    '            '_Qry &= vbCrLf & "  group by FNDebitCreditGrandAmt"
    '            'End If
    '        Case 1

    '            _Qry = "Select Sum(FNAmt) AS FNNetAmtCur , Max(C.FTCurCode) AS FTCurCode From  (Select sum(S.FNPrice * S.FNGrandQuantity) AS FNAmt , B.FNHSysCurId From    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS B WITH(NOLOCK) ON S.FTOrderNo = B.FTOrderNo and S.FTSubOrderNo = B.FTSubOrderNo "

    '            _Qry &= vbCrLf & "Where  S.FTPOref in (" & Key.ToString & ")"
    '            _Qry &= vbCrLf & "group by S.FTPOref ,S.FNPrice , B.FNHSysCurId "
    '            _Qry &= vbCrLf & ") AS T "
    '            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS C WITH(NOLOCK) ON T.FNHSysCurId = C.FNHSysCurId "
    '        Case 2

    '            _Qry = "Select  Sum(FNAmt) AS FNNetAmtCur , Max(C.FTCurCode) AS FTCurCode,T.FNDebitCredit  FRom ( Select O.FNHSysCmpId , sum(S.FNPrice * S.FNGrandQuantity ) AS FNAmt ,S.FTPOref, B.FNHSysCurId,D.FNDebitCreditGrandAmt as FNDebitCredit From [HITECH_MERCHAN]..TMERTOrder AS O WITH(NOLOCK) "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination As S On O.FTOrderNo = S.FTOrderNo "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WITH(NOLOCK) ON O.FNHSysCmpId = C.FNHSysCmpId "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS B WITH(NOLOCK) ON S.FTOrderNo = B.FTOrderNo and S.FTSubOrderNo = B.FTSubOrderNo "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit AS D ON C.FNHSysCmpId=D.FNHSysCmpIdTo"
    '            _Qry &= vbCrLf & " where C.FTCmpCode in (" & Key.ToString & ")"
    '            _Qry &= vbCrLf & " and S.FDShipDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FDShipDate.Text) & "'"
    '            _Qry &= vbCrLf & "group by O.FNHSysCmpId , S.FNPrice ,S.FTPOref , B.FNHSysCurId ,D.FNDebitCreditGrandAmt) AS T "
    '            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS C WITH(NOLOCK) ON T.FNHSysCurId = C.FNHSysCurId "
    '            _Qry &= vbCrLf & "group by  T.FNDebitCredit"
    '        Case 3
    '            _Qry = "Select  Sum(FNAmt) AS FNNetAmtCur , Max(C.FTCurCode ) AS FTCurCode,T.FNDebitCredit  FRom ( Select O.FNHSysCmpId , sum(S.FNPrice * S.FNGrandQuantity ) AS FNAmt ,S.FTPOref, B.FNHSysCurId,D.FNDebitCreditGrandAmt as FNDebitCredit From [HITECH_MERCHAN]..TMERTOrder AS O WITH(NOLOCK) "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination As S On O.FTOrderNo = S.FTOrderNo "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS C WITH(NOLOCK) ON O.FNHSysCustId = C.FNHSysCustId "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS B WITH(NOLOCK) ON S.FTOrderNo = B.FTOrderNo and S.FTSubOrderNo = B.FTSubOrderNo "
    '            _Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTDebitCredit AS D ON C.FNHSysCustId =D.FNHSysCmpIdTo "
    '            _Qry &= vbCrLf & " where C.FTCustCode in (" & Key.ToString & ")"
    '            _Qry &= vbCrLf & " and S.FNHSysProvinceId = " & Integer.Parse("0" & Me.FNHSysProvinceId.Properties.Tag)
    '            _Qry &= vbCrLf & "group by O.FNHSysCmpId , S.FNPrice ,S.FTPOref , B.FNHSysCurId ,D.FNDebitCreditGrandAmt) AS T "
    '            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency AS C WITH(NOLOCK) ON T.FNHSysCurId = C.FNHSysCurId "
    '            _Qry &= vbCrLf & "group by  T.FNDebitCredit"
    '    End Select


    '    ' FNNetAmount.Value = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "0"))

    '    FNNetRcvAmount.Value = 0
    '    FNDebitCredit.Value = 0
    '    FTCurCode.Text = ""
    '    dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

    '    For Each Rx As DataRow In dt.Rows
    '        Try
    '            FNNetRcvAmount.Value = Val(Rx!FNNetAmtCur.ToString())
    '            FNDebitCredit.Value = Val(Rx!FNDebitCredit.ToString())
    '            FTCurCode.Text = Rx!FTCurCode.ToString
    '        Catch ex As Exception

    '        End Try
    '        Exit For
    '    Next
    'End Sub

    Public Sub DefaultsData()
        _FormLoad = True
        Dim _FieldName As String
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString

                                HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)

                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)

                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                .SelectedIndex = Val(_FormHeader(cind).DefaultsData(I).DataDefaults.ToString)
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .Checked = (_FormHeader(cind).DefaultsData(I).DataDefaults.ToString = "1")
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)

                                Try
                                    .DateTime = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                Catch ex As Exception
                                    .Text = ""
                                End Try

                            End With

                        Case ENM.Control.ControlType.MemoEdit
                            With CType(Obj, DevExpress.XtraEditors.MemoEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case ENM.Control.ControlType.TextEdit
                            With CType(Obj, DevExpress.XtraEditors.TextEdit)
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                            End With
                        Case Else
                    End Select
                Next
            Next
        Next
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

        _FormLoad = False
    End Sub

    Private Function CheckNotUsed(Key As String) As Boolean
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckDelFiled.ToArray.Count - 1
                _Str = _FormHeader(cind).CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                    HI.MG.ShowMsg.mProcessError(1301180001, "", Me.Text, MessageBoxIcon.Warning)
                    Return False
                End If

            Next
        Next
        Return True
    End Function

    Private Function VerrifyData() As Boolean
        Dim _FieldName As String
        Dim _Val As String = ""
        Dim _Caption As String = ""
        Dim _Str As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString
                _Caption = ""

                For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)
                    If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
                        _Caption = ObjCaption.Text
                        Exit For
                    End If
                Next

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Properties.Buttons.Count <= 1 Then
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
                                    End If
                                End If
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                If Val(.Value.ToString) <= 0 Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                If .SelectedIndex < 0 Then Pass = False
                            End With
                        Case ENM.Control.ControlType.CheckEdit

                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                If .Image Is Nothing Then
                                    Pass = False
                                End If
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            If Obj.Text = "" Then
                                Pass = False
                            End If
                        Case Else
                            Pass = False
                    End Select

                    If Pass = False Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                        Obj.Focus()
                        Return False
                    End If
                Next
            Next
        Next

        '---------- Validate Document ---------------------
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1

            _FieldName = _FormHeader(cind).MainKey
            _Caption = ""

            For Each ObjCaption As Object In Me.Controls.Find(_FieldName & "_lbl", True)
                If HI.ENM.Control.GeTypeControl(ObjCaption) = ENM.Control.ControlType.LabelControl Then
                    _Caption = ObjCaption.Text
                    Exit For
                End If
            Next

            Dim Pass As Boolean = True

            For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Properties.Buttons.Count <= 1 Then
                                If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                    Pass = False
                                End If
                            End If
                        End With
                    Case ENM.Control.ControlType.CalcEdit
                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            If Val(.Value.ToString) <= 0 Then
                                Pass = False
                            End If
                        End With
                    Case ENM.Control.ControlType.ComboBoxEdit
                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                            If .SelectedIndex < 0 Then Pass = False
                        End With
                    Case ENM.Control.ControlType.CheckEdit

                    Case ENM.Control.ControlType.DateEdit
                        With CType(Obj, DevExpress.XtraEditors.DateEdit)
                            If HI.UL.ULDate.CheckDate(.Text) = "" Then
                                Pass = False
                            End If
                        End With
                    Case ENM.Control.ControlType.PictureEdit
                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            If .Image Is Nothing Then
                                Pass = False
                            End If
                        End With
                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                        If Obj.Text = "" Then
                            Pass = False
                        End If
                    Case Else
                        Pass = False
                End Select

                If Pass = False Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, _Caption)
                    Obj.Focus()
                    Return False
                End If
            Next

        Next

        Return True
    End Function

    Private Function SaveData() As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False

        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For Each Obj As Object In Me.Controls.Find(_FormHeader(cind).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            If .Text.Trim() = "" Then
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                Obj.Focus()
                                Return False
                            Else

                                Dim _CmpH As String = ""
                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                            End If

                            _Key = .Text
                        End With

                End Select

            Next

        Next

        _Str = "SELECT TOP 1 FTProdCostNo "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost AS A WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTProdCostNo='" & HI.UL.ULF.rpQuoted(FTProdCostNo.Text) & "'"

        _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "")

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean



            If (_StateNew) Then
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost  "
                _Str &= "( FTInsUser, FDInsDate, FTInsTime,FTProdCostNo, FDProdCostDate, FDProdCostBy,  FNHSysCmpId, FTMonthYear, FTRemark)"
                _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTProdCostNo.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDProdCostDate.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FDProdCostBy.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.ST.SysInfo.CmpID.ToString & "'"
                '_Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTMonthYear.Text) & "'"
                _Str &= vbCrLf & ",'" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FDMonthUse.Text), 7) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"

            Else
                ''------Update -------------
                _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost "
                _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",FNHSysCmpId='" & HI.ST.SysInfo.CmpID.ToString & "'"
                _Str &= vbCrLf & ",FTMonthYear='" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FDMonthUse.Text), 7) & "'"
                _Str &= vbCrLf & "WHERE FTProdCostNo= '" & HI.UL.ULF.rpQuoted(FTProdCostNo.Text) & "'"

            End If


            'If (_StateNew) Then
            '    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
            'Else
            '    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
            'End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            Call SaveDetail(_Key)

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

    Private Function SaveDetail(ByVal _Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With DirectCast(Me.ogcCharge.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            For Each R As DataRow In _oDt.Rows

                Dim _AMT As String = ""
                _AMT = R!FNAmount.ToString
                If _AMT <> "" Then
                    _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost_Charge "
                    _Cmd &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FNAmount='" & R!FNAmount.ToString & "'"
                    _Cmd &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                    _Cmd &= vbCrLf & " ,FNHSysPIChageId='" & R!FNHSysPIChageId.ToString & "'"
                    _Cmd &= vbCrLf & " WHERE FTProdCostNo ='" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"
                    _Cmd &= vbCrLf & " And FNHSysPIChageId='" & R!FNHSysPIChageId.ToString & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Cmd = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost_Charge "
                        _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTProdCostNo, FNHSysPIChageId, FNAmount, FTRemark)"
                        _Cmd &= vbCrLf & "Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"
                        _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysPIChageId.ToString)
                        '_Cmd &= vbCrLf & ",'" & Double.Parse(R!FNAmount.ToString) & "'"
                        _Cmd &= vbCrLf & ",'" & _AMT & "'"
                        _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                End If
            Next

        Catch ex As Exception
        End Try
        Return True
    End Function



    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost "
            _Str &= vbCrLf & " WHERE FTProdCostNo='" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If



            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost   WHERE FTProdCostNo='" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'")


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

    Private Sub LoadData(HSysId As String)
        Dim _Str As String = Me.Query & "  WHERE " & Me.MainKey & "='" & HSysId & "' "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString


                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                .Text = R.Item(Col).ToString
                                Call HI.TL.HandlerControl.DynamicButtonedit_Leave(Obj, New System.EventArgs)
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                Try
                                    .DateTime = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Text = ""
                                End Try
                            End With

                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next
            Exit For
        Next
    End Sub

    Private Sub FormRefresh()
        _FormLoad = True
        HI.TL.HandlerControl.ClearControl(Me)
        HI.TL.HandlerControl.ClearControl(ogbinvoicechargedetail)

        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
        _FormLoad = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FDProdCostBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1411070101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข รายการ นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If CheckOwner() = False Then Exit Sub
        'If Not (CheckSendApp()) Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then
                If Allocate() Then
                    GetDataBySupl(Me.FNHSysCmpId.Properties.Tag, "", True)
                End If
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If CheckOwner() = False Then Exit Sub
        ' If Not (CheckSendApp()) Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTProdCostNo.Text, Me.Text) = True Then
            If Me.DeleteData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)
                Me.DefaultsData()
                Me.FTProdCostNo.Focus()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If

    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click

        '   HI.TL.HandlerControl.ClearControl(Me)
        Me.FTProdCostNo.Text = ""
        Me.FDProdCostDate.Text = ""
        Me.FDProdCostBy.Text = ""

    End Sub


    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub wInvoiceCharge_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.FDMonthUse.Text = Date.Now


        _FormLoad = False

        Call InitGrid()
        RemoveHandler FTProdCostNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTProdCostNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler FTProdCostNo.KeyDown, AddressOf HI.TL.HandlerControl.DynamicButtonedit_KeyDown
    End Sub


    Private Sub FTProdCostNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTProdCostNo.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FTProdCostNo_EditValueChanged), New Object() {sender, e})
            Else
                Call LoadDataInfo(FTProdCostNo.Text, Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))
                GetDataBySupl(Me.FNHSysCmpId.Properties.Tag, "", True)
                Call Loadyear()
                If Allocate() Then
                    GetDataBySupl(Me.FNHSysCmpId.Properties.Tag, "", True)
                    Call Loadyear()
                    'End If
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs)
    '    Try
    '        If Me.InvokeRequired Then
    '            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysSuplId_EditValueChanged), New Object() {sender, e})
    '        Else
    '            Dim _Qry As String = ""

    '            _Qry = "SELECT TOP 1 FNHSysSuplId "
    '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)"
    '            _Qry &= vbCrLf & " WHERE FTSuplCode='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "'"
    '            FNHSysSuplId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

    '            Call LoadDataInfo(FTInvoiceBankNo.Text, Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)))
    '            '   Call GetDataBySupl(Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)), "")
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub FNChargeNetAmount_EditValueChanged(sender As Object, e As EventArgs)
    '    ' Dim _Sup As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 B.FNHSysSuplId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge as B WITH(NOLOCK) WHERE  B.FTInvoiceBankNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
    '    'Dim _CT As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 isnull(C.FNHSysCmpId,CT.FNHSysCustId)as AA  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Purchase_detail AS BP  WITH(NOLOCK) LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp as C ON BP.FTPurchaseNo=C.FTCmpCode  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CT ON BP.FTPurchaseNo=CT.FTCustCode WHERE BP.FNPIChargeType <>'0' and  BP.FTInvoiceBankNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
    '    Dim Str As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 B.FTInvoiceNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTInvoiceBankCharge_Invoice_detail as B WITH(NOLOCK) WHERE  B.FTInvoiceBankNo ='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
    '    'If FNPIChargeType.Text = "เลขที่ใบสั่งซื้อ" Then
    '    '    With CType(ogcinvoice.DataSource, DataTable)
    '    '        .AcceptChanges()
    '    '        Str = ""
    '    '        For Each R As DataRow In .Rows
    '    '            If Str <> "" Then Str &= ","
    '    '            Str &= "'" & R!FTInvoiceNo.ToString & "'"
    '    '        Next
    '    '    End With
    '    'End If


    '    Dim _Cre As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 D.FNDocDebitCreditState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit as D WITH(NOLOCK) LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTDebitCredit_Invoice AS DP ON D.FTDebitCreditNo=DP.FTDebitCreditNo WHERE  DP.FTInvoiceNo in (" & Str & ") ", Conn.DB.DataBaseName.DB_MASTER, "")

    '    If _Cre = "1" Then
    '        FNGAmount.Value = (FNChargeNetAmount.Value + FNNetRcvAmount.Value) - FNDebitCredit.Value
    '    Else
    '        FNGAmount.Value = (FNChargeNetAmount.Value + FNNetRcvAmount.Value) + FNDebitCredit.Value
    '    End If

    'End Sub
    'Private Sub FNChargeNetAmount_EditValueChanged(sender As Object, e As EventArgs) Handles FNChargeNetAmount.EditValueChanged, FNNetRcvAmount.EditValueChanged
    '    FNGAmount.Value = FNChargeNetAmount.Value + FNNetRcvAmount.Value
    'End Sub
    Private Sub GetDataBySupl(_SuplId As Integer, Str As String, Optional ByVal _State As Boolean = False)
        Try
            Dim _Cmd As String = ""
            Dim _PO As String = ""

            'End If


            _Cmd = " Select J.FTOrderNo ,SUM(JI.FNExportAmtTHB) AS FNNetAmt,C.FTCmpCode,JI.FDInvoiceDate,O.FNAmount "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost AS J WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS JI  WITH(NOLOCK) ON J.FTOrderNo=JI.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS Od WITH(NOLOCK) ON J.FTOrderNo=Od.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK) ON Od.FNHSysCmpId=C.FNHSysCmpId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTProdCost_Order WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where  FTProdCostNo='" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"
            _Cmd &= vbCrLf & ")  AS O ON od.FTOrderNo=O.FTOrderNo"
            _Cmd &= vbCrLf & "  where C.FTCmpCode='" & Me.FNHSysCmpId.Text & "'"
            _Cmd &= vbCrLf & " and (JI.FDInvoiceDate LIKE '%" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FDMonthUse.Text), 7) & "%')"
            _Cmd &= vbCrLf & "GROUP BY J.FTOrderNo ,C.FTCmpCode,JI.FDInvoiceDate,O.FNAmount"

            Me.ogcallocate.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogvallocate.ExpandAllGroups()
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub ocmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
    '    Try
    '        If (Me.FTStateSendApp.Checked) Then Exit Sub
    '        If Me.VerrifyData Then
    '            ' If Me.SaveData() Then
    '            If Allocate() Then
    '                GetDataBySupl(Me.FNHSysSuplId.Properties.Tag, "", True)
    '            End If
    '            'End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Function Allocate() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _Dt As DataTable

            Dim _Amt As Double = 0

            'Dim _AmtGA As Integer = 0
            Dim _Amtch As Integer = 0
            Dim _AmtGA2 As Double = 0


            With DirectCast(Me.ogcallocate.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            With DirectCast(Me.ogcCharge.DataSource, DataTable)
                .AcceptChanges()
                _Dt = .Copy
            End With
            For Each R As DataRow In _oDt.Rows
                _Amt += +Double.Parse("0" & R!FNNetAmt.ToString)
            Next
            For Each R As DataRow In _Dt.Rows
                _Amtch += +Double.Parse("0" & R!FNAmount.ToString)
            Next
            Dim _date As String = HI.Conn.SQLConn.GetField("select FTMonthYear from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost  WHERE FTProdCostNo='" & Me.FTProdCostNo.Text & "'", Conn.DB.DataBaseName.DB_ACCOUNT, "")
            Dim _AmtGA As String = HI.Conn.SQLConn.GetField("select sum(a.job) as AMTT  FROM (select s.FTOrderNo,s.job,j.FDInvoiceDate FROM (select s.FNNetFOB,s.FTOrderNo,sum(s.FNQuantity)as FNQuantity,sum(s.FNNetFOB*S.FNQuantity) as job,s.FDUpdDate FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown as s group by s.FNNetFOB,s.FTOrderNo,s.FDUpdDate )as s left outer join (select j.FTOrderNo,ji.FDInvoiceDate  ,ji.FNExportQuantity FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost as j left outer join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS ji on j.FTOrderNo=ji.FTOrderNo)as j on s.FTOrderNo=j.FTOrderNo  WHERE (J.FDInvoiceDate LIKE '%" & Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB("01/" & Me.FDMonthUse.Text), 7) & "%' ))as a ", Conn.DB.DataBaseName.DB_ACCOUNT, "")





            '_AmtGA = Me.FNChargeNetAmount.Text
            '_AmtGA1 = Me.FNNetRcvAmount.Text
            _AmtGA2 = _Amtch / _AmtGA

         


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _oDt.Rows

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost_Order"
                _Cmd &= vbCrLf & "Set FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNAmount=" & (_AmtGA2 * Double.Parse("0" & R!FNNetAmt.ToString))
                _Cmd &= vbCrLf & "Where FTProdCostNo='" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"
                _Cmd &= vbCrLf & "and FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost_Order"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTProdCostNo, FTOrderNo, FNAmount)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & "," & (_AmtGA2 * Double.Parse("0" & R!FNNetAmt.ToString))

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            Next





            '_Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTProdCost "
            '_Cmd &= vbCrLf & "SET FTStateSendApp='1'"
            '_Cmd &= vbCrLf & " WHERE FTInvoiceBankNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceBankNo.Text) & "'"
            '_Cmd &= vbCrLf & " AND FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""

            'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) Then
            '    ' Me.FTStateSendApp.Checked = True
            '    ' HI.MG.ShowMsg.mInfo("Send Approve Success..", 1609231623, Me.Text)
            'End If

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




    'Private Sub FTStateSendApp_CheckedChanged(sender As Object, e As EventArgs)
    '    Try
    '        If Me.FTStateSendApp.Checked = True Then
    '            '  ogvpurchase.OptionsVi()

    '        End If
    '        Me.FTPurchaseNo.Properties.ReadOnly = (Me.FTStateSendApp.Checked)
    '        Me.FTPurchaseNo.Enabled = Not (Me.FTStateSendApp.Checked)
    '        'Me.FTInvoiceNo.Properties.ReadOnly = (Me.FTStateSendApp.Checked)
    '        'Me.FTInvoiceNo.Enabled = Not (Me.FTStateSendApp.Checked)
    '    Catch ex As Exception
    '    End Try
    'End Sub



    Private Sub RepositoryFNAmount_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFNAmount.EditValueChanging
        Try
            Dim _AmtCharge As Double = 0
            Dim _oDt As DataTable
            Me.ogvCharge.SetFocusedRowCellValue("FNAmount", Double.Parse("0" & e.NewValue))
            With DirectCast(Me.ogcCharge.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            _AmtCharge = Double.Parse("0" & _oDt.Compute("SUM(FNAmount)", "FNAmount > 0"))
        Catch ex As Exception
        End Try
    End Sub

    'Private Function CheckSendApp() As Boolean
    '    Try
    '        If Me.FTStateSendApp.Checked Then
    '            HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการส่งอนุมัติแล้ว กรุณาตรวจสอบ !!!!", 1611111414, Me.Text, MessageBoxIcon.Warning)
    '            Return False
    '        End If
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Private Sub FTProdCostNo_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then
                Try
                    If Me.InvokeRequired Then
                        Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FTProdCostNo_KeyDown), New Object() {sender, e})
                    Else
                        Call LoadDataInfo(FTProdCostNo.Text, Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))
                    End If
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FDMonthUse_EditValueChanged(sender As Object, e As EventArgs) Handles FDMonthUse.EditValueChanged
        GetDataBySupl(Me.FNHSysCmpId.Properties.Tag, "", True)
    End Sub
    Private Sub Loadyear()
        Dim _Qry As String = ""
        Dim Str As String = ""
       

        _Qry = " SELECT FTMonthYear"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTProdCost  WITH(NOLOCK)  "
        _Qry &= vbCrLf & "   WHERE  FTProdCostNo = '" & HI.UL.ULF.rpQuoted(Me.FTProdCostNo.Text) & "'"

        FDMonthUse.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")
    End Sub

    Private Sub ogbdocinfo_Paint(sender As Object, e As PaintEventArgs) Handles ogbdocinfo.Paint

    End Sub

 
End Class