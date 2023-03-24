Imports System.Windows.Forms

Public Class wReceive

    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wReceiveItem
    Private _DataInfo As DataTable
    Private _ProcLoad As Boolean = False
    Private _GenBarcode As wGenerateBarcodeInven
    Private _Multiple As wReceiveMultiple
    Private _ReceiveUnitRawMat As wReceiveUnitMaterial
    Private _AutoTransferToCenter As wReceiveAutoTransferToCenter
    Private _AutoTransferToWH As wReceiveAutoTransferToWH
    Private _AutoIssue As wReceiveAutoIssue
    Private _AutoGenerateBarcodeGrp As wReceiveGenerateBarcodeGrp
    Private _FormLoad As Boolean = True

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _FormLoad = True
        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        _AddItemPopup = New wReceiveItem
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try

        _GenBarcode = New wGenerateBarcodeInven
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenBarcode.Name.ToString.Trim, _GenBarcode)
        Catch ex As Exception
        Finally
        End Try

        _Multiple = New wReceiveMultiple
        HI.TL.HandlerControl.AddHandlerObj(_Multiple)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Multiple.Name.ToString.Trim, _Multiple)
        Catch ex As Exception
        Finally
        End Try

        _AutoTransferToCenter = New wReceiveAutoTransferToCenter
        HI.TL.HandlerControl.AddHandlerObj(_AutoTransferToCenter)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoTransferToCenter.Name.ToString.Trim, _AutoTransferToCenter)
        Catch ex As Exception
        Finally
        End Try

        _AutoTransferToWH = New wReceiveAutoTransferToWH
        HI.TL.HandlerControl.AddHandlerObj(_AutoTransferToWH)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoTransferToWH.Name.ToString.Trim, _AutoTransferToWH)
        Catch ex As Exception
        Finally
        End Try

        _AutoIssue = New wReceiveAutoIssue
        HI.TL.HandlerControl.AddHandlerObj(_AutoIssue)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoIssue.Name.ToString.Trim, _AutoIssue)
        Catch ex As Exception
        Finally
        End Try

        _ReceiveUnitRawMat = New wReceiveUnitMaterial
        HI.TL.HandlerControl.AddHandlerObj(_ReceiveUnitRawMat)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ReceiveUnitRawMat.Name.ToString.Trim, _ReceiveUnitRawMat)
        Catch ex As Exception
        Finally
        End Try


        _AutoGenerateBarcodeGrp = New wReceiveGenerateBarcodeGrp
        HI.TL.HandlerControl.AddHandlerObj(_AutoGenerateBarcodeGrp)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoGenerateBarcodeGrp.Name.ToString.Trim, _AutoGenerateBarcodeGrp)
        Catch ex As Exception
        Finally
        End Try


        Call TabChanged()

        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSumAmt As String = "FNDisAmt|FNNetAmt|FNVatAmt|FNNetVatAmt"
        Dim sFieldSumQty As String = "FNQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvbarcode
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

        With ogvdetail
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

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit.ToString & "}"
                End If
            Next

            For Each Str As String In sFieldSumQty.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region
#Region "Property"

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
        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysTableObjForm WITH(NOLOCK) "
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

    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True
        'HI.TL.HandlerControl.ClearControl(ogbh)
        'HI.TL.HandlerControl.ClearControl(ogbpayment)
        'HI.TL.HandlerControl.ClearControl(ogbsuplcfm)
        'HI.TL.HandlerControl.ClearControl(ogbpoamt)
        'HI.TL.HandlerControl.ClearControl(ogbnote)
        'HI.TL.HandlerControl.ClearControl(ogbdocdetail)

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

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

        Call LoadRcvDetail(Key.ToString)
        Call LoadPOInfo(FTPurchaseNo.Text, True)
        Call Me.LoadBarcode(Key.ToString)

        _Str = "SELECT TOP 1 'TRANSACTION' "
        _Str &= " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
        _Str &= " WHERE FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "

        Me.olbtrans.Text = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "")

        Me.oxtb.SelectedTabPageIndex = 0

        FDReceiveDate.Properties.Buttons(0).Visible = False
        FDReceiveDate.Properties.ReadOnly = True

        _ProcLoad = False
    End Sub

    Private Sub LoadRcvDetail(PoKey As String)

        Dim _VatPer As Double = 0

        Dim _Str As String = ""
        _Str = "SELECT TOP 1 FNVatPer "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS PH WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

        _VatPer = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_PUR, "0")))

        If _VatPer = 0 Then

            _Str = "  SELECT TOP 1  FTStateRcvVat"
            _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK)"
            _Str &= vbCrLf & "  WHERE (FNHSysSuplId = " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ")"

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

                _VatPer = 7

            End If

        End If

        _Str = " SELECT        D.FNHSysRawMatId, M.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
        Else
            _Str &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
        End If

        _Str &= vbCrLf & " , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & "  ,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt, D.FNNetPrice, D.FNQuantity,ISNULL(D.FNSurchangePerUnit,0) AS FNSurchangePerUnit, D.FNNetAmt, '' AS FTRemark"
        _Str &= vbCrLf & " ,Convert(numeric(18,2),(D.FNNetAmt * " & _VatPer & ")/100.00) AS FNVatAmt"
        _Str &= vbCrLf & " ,D.FNNetAmt + Convert(numeric(18,2),(D.FNNetAmt * " & _VatPer & ")/100.00) AS FNNetVatAmt"

        _Str &= vbCrLf & ",ISNULL((   SELECT TOP  1  '1' AS FTStateSendApp"
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver WITH (NOLOCK)"
        _Str &= vbCrLf & " WHERE (FTReceiveNo =N'" & HI.UL.ULF.rpQuoted(PoKey) & "')  "
        _Str &= vbCrLf & " AND  (FNHSysRawMatId = D.FNHSysRawMatId ) "
        _Str &= vbCrLf & " AND  (FTStateApprove IS NULL) ),'') AS  FTStateSendApp "

        _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS D WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & " WHERE        (D.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode "
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FNRawMatColorSeq, S.FNRawMatSizeSeq "
        _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FNRawMatSizeSeq "

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        _Str = " SELECT        D.FNHSysRawMatId, M.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
        Else
            _Str &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
        End If

        _Str &= vbCrLf & " , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & " ,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt, D.FNNetPrice, D.FNQuantity, D.FNNetAmt, '' AS FTRemark,D.FTOrderNo"

        _Str &= vbCrLf & "  ,CASE WHEN ISDATE(ISnull(O.FDShipDate,'')) = 1 THEN Convert(varchar(10),Convert(datetime,ISnull(O.FDShipDate,'')),103)  ELSE '' END AS FDShipDate "
        _Str &= vbCrLf & "  ,  ISNULL(ORD.FNUsedQuantity,0) As FNUsedQuantity"
        _Str &= vbCrLf & "  ,ISNULL(PD.FNQuantity,0) AS FNPOQuantity"
        _Str &= vbCrLf & "  ,ISNULL(TRC.FNQuantity,0) AS FNTCQuantity"
        _Str &= vbCrLf & "  ,ISNULL(RTS.FNQuantity,0) AS FNRTSQuantity"
        _Str &= vbCrLf & "  ,ISNULL(PD.FNQuantity,0)  - (ISNULL(TRC.FNQuantity,0)-ISNULL(RTS.FNQuantity,0)) AS FNPOBALQuantity"
        _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"

        _Str &= vbCrLf & " LEFT OUTER JOIN (SELECT        FTOrderNo, ISNULL"
        _Str &= vbCrLf & "    ((SELECT        MIN(FDShipDate) AS FDShipDate"
        _Str &= vbCrLf & "   FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Su WITH (NOLOCK)"
        _Str &= vbCrLf & "   WHERE        (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate"
        _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A ) AS O ON D.FTOrderNo = O.FTOrderNo "

        _Str &= vbCrLf & " LEFT OUTER JOIN    (SELECT        FTOrderNo, FNHSysRawMatId, SUM((ISNULL(FNUsedQuantity,0) + ISNULL(FNUsedPlusQuantity,0))) AS FNUsedQuantity"
        _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS O WITH(NOLOCK)"
        _Str &= vbCrLf & "   GROUP BY FTOrderNo, FNHSysRawMatId"
        _Str &= vbCrLf & "  ) AS ORD ON D.FTOrderNo = ORD.FTOrderNo AND D.FNHSysRawMatId = ORD.FNHSysRawMatId "

        _Str &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS H WITH (NOLOCK) ON D.FTReceiveNo = H.FTReceiveNo "

        _Str &= vbCrLf & " LEFT OUTER JOIN      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PD"
        _Str &= vbCrLf & " ON H.FTPurchaseNo = PD.FTPurchaseNo AND D.FTOrderNo = PD.FTOrderNo AND D.FNHSysRawMatId = PD.FNHSysRawMatId "

        _Str &= vbCrLf & " LEFT OUTER JOIN    ("
        _Str &= vbCrLf & " SELECT        RH.FTPurchaseNo, RD.FTOrderNo, RD.FNHSysRawMatId, SUM(RD.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, RD.FTOrderNo, RD.FNHSysRawMatId"
        _Str &= vbCrLf & " ) AS TRC ON H.FTPurchaseNo = TRC.FTPurchaseNo AND D.FTOrderNo = TRC.FTOrderNo AND D.FNHSysRawMatId = TRC.FNHSysRawMatId "

        _Str &= vbCrLf & " LEFT OUTER JOIN    ("
        _Str &= vbCrLf & " SELECT         RH.FTPurchaseNo, B.FTOrderNo, B.FNHSysRawMatId, SUM(RD.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier  AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS RD WITH (NOLOCK) ON RH.FTReturnSuplNo = RD.FTDocumentNo  INNER JOIN "
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON RD.FTBarcodeNo = B.FTBarcodeNo"
        _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, B.FTOrderNo, B.FNHSysRawMatId"
        _Str &= vbCrLf & " ) AS RTS ON H.FTPurchaseNo = RTS.FTPurchaseNo AND D.FTOrderNo = RTS.FTOrderNo AND D.FNHSysRawMatId = RTS.FNHSysRawMatId "

        _Str &= vbCrLf & " WHERE        (D.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        ' _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode "
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FNRawMatColorSeq, S.FNRawMatSizeSeq "
        _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FNRawMatSizeSeq "
        _Str &= vbCrLf & ",ISnull(O.FDShipDate,'')"

        Me.ogcmuljob.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

    End Sub

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
                            'Case ENM.Control.ControlType.PictureEdit
                            '    With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                            '        If .Image Is Nothing Then
                            '            Pass = False
                            '        End If
                            '    End With
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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString

        FDReceiveDate.Properties.Buttons(0).Visible = Me.ocmeditdate.Enabled
        FDReceiveDate.Properties.ReadOnly = Not (Me.ocmeditdate.Enabled)

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
                            'With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                            '    If Val(.Value.ToString) <= 0 Then
                            '        Pass = False
                            '    End If
                            'End With
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

                                If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(.Text) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If

                            End If
                        End With

                End Select
            Next
        Next

        If FNExchangeRate.Value <= 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
            FNExchangeRate.Focus()
            Return False
        End If

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
        Dim _CmpH As String = ""
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


                                For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                        Case ENM.Control.ControlType.ButtonEdit
                                            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

                                    If _dt.Rows.Count <= 0 Then
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                        Obj.Focus()
                                        Return False
                                    End If
                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If (_StateNew) Then

            If Me.FDReceiveDate.Properties.ReadOnly = False Then
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH, HI.UL.ULDate.ConvertEnDB(Me.FDReceiveDate.Text)).ToString
            Else
                _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
            End If

        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _FoundControl As Boolean
            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
                    _FoundControl = False
                    If (_StateNew) Then

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else
                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            If _Values <> "" Then _Values &= ","
                            If _Fields <> "" Then _Fields &= ","

                            _Fields &= _FieldName

                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate")
                                    _Values &= HI.UL.ULDate.FormatDateDB & ""
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
                                    _Values &= "''"
                                Case UCase("FTInsTime")
                                    _Values &= HI.UL.ULDate.FormatTimeDB & ""
                                Case UCase("FTInsUser")
                                    _Values &= "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If


                    Else

                        '------Update -------------
                        For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                            _FoundControl = True
                            If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                _Val = _Key
                            Else

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.ButtonEdit
                                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                            _Val = "" & .Properties.Tag.ToString
                                        End With
                                    Case ENM.Control.ControlType.CalcEdit
                                        With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                            _Val = .Value.ToString
                                        End With
                                    Case ENM.Control.ControlType.ComboBoxEdit
                                        With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                            _Val = .SelectedIndex.ToString
                                        End With
                                    Case ENM.Control.ControlType.CheckEdit
                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            _Val = .EditValue.ToString
                                        End With
                                    Case ENM.Control.ControlType.PictureEdit
                                        With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                            _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _Key.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                        End With
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit, ENM.Control.ControlType.DateEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
                                    _FoundControl = True
                            End Select
                        End If

                        If _FoundControl Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")
                                Case Else
                                    If _Values <> "" Then _Values &= ","
                            End Select

                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FDUpdDate")

                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatDateDB & ""

                                Case UCase("FTUpdTime")

                                    _Values &= _FieldName & "=" & HI.UL.ULDate.FormatTimeDB & ""

                                Case UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser")

                                Case UCase("FTUpdUser")
                                    _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else

                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _FieldName & "='" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _FieldName & "='" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select

                            End Select
                        End If

                    End If

                Next

                If (_StateNew) Then
                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit

                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With

                End Select

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            ' Call CreatePOFactory(FTPurchaseNo.Text)
            ' Call CreatePOMultiFactory(FTPurchaseNo.Text)
            Call UpdateRawMatUnit(FTPurchaseNo.Text)

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False

        End Try

    End Function

    Private Function DeleteData(Optional StateDeleteDocImport As Boolean = False) As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            If StateDeleteDocImport Then
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver"
            _Str &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
            _Str &= vbCrLf & " AND ISNULL(FTStateApprove,'') ='' "
            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_INVEN)

            Try
                Dim cmdstring As String = ""

                cmdstring = "  Select  Top 1  ISNULL(A.FTReceiveNo,'')		"
                cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS A WITH(NOLOCK) INNER Join "
                cmdstring &= vbCrLf & " Where A.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

                cmdstring &= vbCrLf & " And A.FTReceiveNo <>'" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

                If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                    cmdstring = " DELETE  A FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  "
                    cmdstring &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase AS B ON A.FTFacPurchaseNo=B.FTFacPurchaseNo "
                    cmdstring &= vbCrLf & "  WHERE  B.FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    cmdstring &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                End If



            Catch ex As Exception
            End Try



            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'")
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    'Private Sub CreatePOFactory(pokey As String)
    '    Try

    '        Dim pofacno As String = ""
    '        Dim cmdstring = ""
    '        Dim FNHSysCmpIdTo As Integer = 0
    '        Dim FNHSysCmpId As Integer = 0


    '        cmdstring = "  Select  Top 1  ISNULL(A.FTReceiveNo,'')		"
    '        cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS A WITH(NOLOCK) INNER Join "
    '        cmdstring &= vbCrLf & " Where A.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(pokey) & "' "
    '        cmdstring &= vbCrLf & "  And A.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "' "
    '        cmdstring &= vbCrLf & " And A.FTReceiveNo <'" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

    '        If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
    '            Exit Sub
    '        End If

    '        Dim ExchangeRate As Double = FNExchangeRate.Value
    '        Dim dt As DataTable

    '        cmdstring = "Select TOP 1 FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt,FNHSysDeliveryId,FNExchangeRate "
    '        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As A "
    '        cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '        Dim dtpo As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '        Dim vatper As Double = 0

    '        Dim poamt As Double = 0
    '        Dim podisamt As Double = 0
    '        Dim ponetamt As Double = 0
    '        Dim povatamt As Double = 0
    '        Dim pograndamt As Double = 0
    '        Dim poamtth As String
    '        Dim poamten As String
    '        Dim Surcharge As Double = 0
    '        Dim FNHSysDeliveryId As Integer = 0
    '        Dim SysCurId As Integer = 1310200002
    '        Dim ExcRate As Double = FNExchangeRate.Value
    '        Dim POFacExcRate As Double = 1

    '        For Each R As DataRow In dtpo.Rows

    '            vatper = Val(R!FNVatPer.ToString)
    '            podisamt = CDbl(Format(Val(R!FNDisCountAmt.ToString) * Val(R!FNExchangeRate.ToString), "0.00")) ' Val(R!FNDisCountAmt.ToString)
    '            Surcharge = CDbl(Format(Val(R!FNSurcharge.ToString) * Val(R!FNExchangeRate.ToString), "0.00"))  'Val(R!FNSurcharge.ToString)
    '            FNHSysDeliveryId = Val(R!FNHSysDeliveryId.ToString)
    '            ' ExchangeRate = Val(R!FNExchangeRate.ToString)

    '        Next

    '        cmdstring = "SELECT TOP 1 FNHSysCmpId,FNHSysCmpIdTo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery  WITH(NOLOCK)  WHERE FNHSysDeliveryId=" & Val(FNHSysDeliveryId) & " "
    '        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '        For Each R As DataRow In dt.Rows

    '            FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
    '            FNHSysCmpIdTo = Val(R!FNHSysCmpIdTo.ToString)

    '        Next

    '        cmdstring = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(pokey) & "'"
    '        pofacno = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

    '        cmdstring = "SELECT TOP 1 FNExchangeRate  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
    '        POFacExcRate = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "-1"))

    '        If FNHSysCmpId > 0 And FNHSysCmpIdTo > 0 Then

    '            If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then

    '                vatper = 0

    '                SysCurId = 1310190001

    '                Dim _Qry As String = ""

    '                _Qry = " SELECT TOP 1 FNBuyingRate"
    '                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
    '                _Qry &= vbCrLf & "   WHERE  (FDDate ='" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "')"
    '                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
    '                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
    '                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
    '                _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
    '                _Qry &= vbCrLf & "  ))"

    '                ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

    '                If POFacExcRate = ExcRate Then
    '                    Exit Sub
    '                End If

    '            Else

    '                If FNExchangeRate.Value = 1 And pofacno <> "" Then
    '                    Exit Sub
    '                End If

    '                SysCurId = 1310200002
    '                ExcRate = 1

    '            End If

    '            If pofacno = "" Then

    '                ' pofacno = "F" & pokey

    '                Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
    '                Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
    '                Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
    '                Dim _CmpHCreate As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                Dim _CmpRunText As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 13), 2)
    '                Dim _POGrp As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 9), 2)
    '                Dim _POType As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 7), 1)

    '                pofacno = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTFacPurchase", "", False, _CmpHCreate & "F" & _CmpRunText & _Year & _POGrp & _POType & _Month).ToString
    '                ' pofacno = Microsoft.VisualBasic.Left(pokey, 2) & "FA" & Microsoft.VisualBasic.Right(pokey, pokey.Length - 2)

    '                cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
    '                cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
    '                cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
    '                cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
    '                cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
    '                cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
    '                cmdstring &= vbCrLf & ")"
    '                cmdstring &= vbCrLf & " SELECT  FTInsUser, FDInsDate, FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
    '                cmdstring &= vbCrLf & "     FNHSysCmpRunId," & FNHSysCmpId & " AS  FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId," & SysCurId & " AS FNHSysCurId," & ExcRate & " AS  FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
    '                cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt," & vatper & " FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
    '                cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,  "
    '                cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(pokey) & "' AS FTPurchaseNoRef," & FNHSysCmpIdTo & " AS  FNHSysCmpIdCreate"
    '                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '            Else

    '                cmdstring = "UPDATE A Set "
    '                cmdstring &= vbCrLf & "  FTInsUser=B.FTInsUser"
    '                cmdstring &= vbCrLf & ", FDInsDate=B.FDInsDate"
    '                cmdstring &= vbCrLf & ", FTInsTime=B.FTInsTime"
    '                cmdstring &= vbCrLf & ",  FTRefer=B.FTRefer"
    '                cmdstring &= vbCrLf & ", FNPoState=B.FNPoState"
    '                cmdstring &= vbCrLf & ", FNHSysPurGrpId=B.FNHSysPurGrpId"
    '                cmdstring &= vbCrLf & ", FNHSysCmpRunId=B.FNHSysCmpRunId"
    '                cmdstring &= vbCrLf & ", FNHSysCmpId=B.FNHSysCmpId"
    '                cmdstring &= vbCrLf & ", FDDeliveryDate=B.FDDeliveryDate"
    '                cmdstring &= vbCrLf & ", FNHSysCrTermId=B.FNHSysCrTermId"
    '                cmdstring &= vbCrLf & ", FNCreditDay=B.FNCreditDay"
    '                cmdstring &= vbCrLf & ", FNHSysTermOfPMId=B.FNHSysTermOfPMId"
    '                cmdstring &= vbCrLf & ", FNHSysCurId=" & SysCurId & ""
    '                cmdstring &= vbCrLf & ", FNExchangeRate=" & ExcRate & ""
    '                cmdstring &= vbCrLf & ", FNHSysDeliveryId=B.FNHSysDeliveryId"
    '                cmdstring &= vbCrLf & ", FTContactPerson=B.FTContactPerson"
    '                cmdstring &= vbCrLf & ", FTRemark=B.FTRemark"
    '                cmdstring &= vbCrLf & ", FNPoAmt=B.FNPoAmt"
    '                cmdstring &= vbCrLf & ", FNDisCountPer=B.FNDisCountPer"
    '                cmdstring &= vbCrLf & ", FNDisCountAmt=B.FNDisCountAmt"
    '                cmdstring &= vbCrLf & ", FNPONetAmt=B.FNPONetAmt"
    '                cmdstring &= vbCrLf & ", FNVatPer=" & vatper & ""
    '                cmdstring &= vbCrLf & ", FNVatAmt=B.FNVatAmt"
    '                cmdstring &= vbCrLf & ", FNSurcharge=B.FNSurcharge"
    '                cmdstring &= vbCrLf & ", FNPOGrandAmt=B.FNPOGrandAmt"
    '                cmdstring &= vbCrLf & ", FTPOGrandAmtTH=B.FTPOGrandAmtTH"
    '                cmdstring &= vbCrLf & ", FTPOGrandAmtEN=B.FTPOGrandAmtEN"
    '                cmdstring &= vbCrLf & ", FTStateSendApp=B.FTStateSendApp"
    '                cmdstring &= vbCrLf & ", FTSendAppBy=B.FTSendAppBy"
    '                cmdstring &= vbCrLf & ", FTSendAppDate=B.FTSendAppDate"
    '                cmdstring &= vbCrLf & ", FTSendAppTime=B.FTSendAppTime"
    '                cmdstring &= vbCrLf & ", FNPoType=B.FNPoType"
    '                cmdstring &= vbCrLf & ", FNHSysCmpIdCreate=" & FNHSysCmpIdTo & ""
    '                cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As B"
    '                cmdstring &= vbCrLf & "  WHERE A.FTPurchaseNoRef = B.FTPurchaseNo"
    '                cmdstring &= vbCrLf & "  And A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '            End If

    '            cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
    '            cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
    '            cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
    '            cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate,FTPurchaseNo"
    '            cmdstring &= vbCrLf & ")"
    '            cmdstring &= vbCrLf & " Select  A.FTInsUser, A.FDInsDate, A.FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo"
    '            cmdstring &= vbCrLf & ",  A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysUnitId"
    '            cmdstring &= vbCrLf & ", (A.FNPrice + Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))) AS FNPrice , A.FNDisPer, A.FNDisAmt, A.FNQuantity, A.FNNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity)) AS FNNetAmt"
    '            cmdstring &= vbCrLf & ", A.FTRemark, "
    '            cmdstring &= vbCrLf & "  A.FTFabricFrontSize, A.FNReservePOQuantity, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN, A.FNSurchangeAmt, A.FNSurchangePerUnit, A.FNGrandNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,2),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity))  AS FNGrandNetAmt, A.FTOGacDate,A.FTPurchaseNo"
    '            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A "

    '            cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON A.FTPurchaseNo=H.FTPurchaseNo"
    '            cmdstring &= vbCrLf & "  INNER Join "
    '            cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
    '            cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
    '            cmdstring &= vbCrLf & " (SELECT  *  "
    '            cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
    '            cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & FNHSysCmpIdTo & ""
    '            cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"
    '            cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '            cmdstring = " UPDATE B SET "
    '            cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,4),(B.FNPrice *C.FNExchangeRate)/A.FNExchangeRate )"
    '            cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,4),(B.FNDisAmt *C.FNExchangeRate)/A.FNExchangeRate )"
    '            cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,4),(B.FNNetAmt *C.FNExchangeRate)/A.FNExchangeRate )"
    '            cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,4),(B.FNSurchangeAmt *C.FNExchangeRate)/A.FNExchangeRate )"
    '            cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,4),(B.FNSurchangePerUnit *C.FNExchangeRate)/A.FNExchangeRate )"
    '            cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt *C.FNExchangeRate)/A.FNExchangeRate )"
    '            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
    '            cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo INNER Join"
    '            cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As C On A.FTPurchaseNoRef = C.FTPurchaseNo "
    '            cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '            podisamt = CDbl(Format(podisamt / ExcRate, "0.00")) ' Val(R!FNDisCountAmt.ToString)
    '            Surcharge = CDbl(Format(Surcharge / ExcRate, "0.00"))  'Val(R!FNSurcharge.ToString)

    '            cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
    '            cmdstring &= vbCrLf & "    FROM"
    '            cmdstring &= vbCrLf & " ("
    '            cmdstring &= vbCrLf & " SELECT        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
    '            cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  WITH(NOLOCK)"
    '            cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
    '            cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

    '            poamt = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
    '            ponetamt = poamt - podisamt
    '            povatamt = CDbl(Format((ponetamt * vatper) / 100.0, "0.00"))
    '            pograndamt = ponetamt + povatamt + Surcharge

    '            poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
    '            poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

    '            cmdstring = "UPDATE A Set "
    '            cmdstring &= vbCrLf & "  FNPoAmt=" & poamt & ""
    '            cmdstring &= vbCrLf & ", FNPONetAmt=" & ponetamt & ""
    '            cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
    '            cmdstring &= vbCrLf & ", FNPOGrandAmt=" & pograndamt & ""
    '            cmdstring &= vbCrLf & ", FNSurcharge=" & Surcharge & ""
    '            cmdstring &= vbCrLf & ", FNDisCountAmt=" & podisamt & ""
    '            cmdstring &= vbCrLf & ", FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
    '            cmdstring &= vbCrLf & ", FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"

    '            cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
    '            cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '        Else

    '            If pofacno <> "" Then

    '                cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""
    '                cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""

    '                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '            End If

    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub

    'Private Sub CreatePOMultiFactory(pokey As String)
    '    Try

    '        Dim pofacno As String = ""
    '        Dim cmdstring = ""
    '        Dim FNHSysCmpIdTo As Integer = 0
    '        Dim FNHSysCmpId As Integer = 0
    '        Dim FNPoState As String = "0"

    '        cmdstring = "  Select  Top 1  ISNULL(A.FTReceiveNo,'')		"
    '        cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReceive AS A WITH(NOLOCK) INNER Join "
    '        cmdstring &= vbCrLf & " Where A.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(pokey) & "' "
    '        cmdstring &= vbCrLf & "  And A.FDReceiveDate <='" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "' "
    '        cmdstring &= vbCrLf & " And A.FTReceiveNo <'" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

    '        If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
    '            Exit Sub
    '        End If

    '        cmdstring = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(pokey) & "'"
    '        If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
    '            Exit Sub
    '        End If

    '        Dim ExchangeRate As Double = FNExchangeRate.Value
    '        Dim dt As DataTable

    '        cmdstring = "Select TOP 1 FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt,FNHSysDeliveryId,FNExchangeRate "
    '        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As A "
    '        cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '        Dim dtpo As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '        Dim vatper As Double = 0

    '        Dim poamt As Double = 0
    '        Dim podisamt As Double = 0
    '        Dim ponetamt As Double = 0
    '        Dim povatamt As Double = 0
    '        Dim pograndamt As Double = 0
    '        Dim poamtth As String
    '        Dim poamten As String
    '        Dim Surcharge As Double = 0
    '        Dim FNHSysDeliveryId As Integer = 0
    '        Dim SysCurId As Integer = 1310200002
    '        Dim ExcRate As Double = FNExchangeRate.Value
    '        Dim POFacExcRate As Double = 1

    '        For Each R As DataRow In dtpo.Rows

    '            vatper = Val(R!FNVatPer.ToString)
    '            podisamt = CDbl(Format(Val(R!FNDisCountAmt.ToString) * Val(R!FNExchangeRate.ToString), "0.00")) ' Val(R!FNDisCountAmt.ToString)
    '            Surcharge = CDbl(Format(Val(R!FNSurcharge.ToString) * Val(R!FNExchangeRate.ToString), "0.00"))  'Val(R!FNSurcharge.ToString)
    '            FNHSysDeliveryId = Val(R!FNHSysDeliveryId.ToString)
    '            ' ExchangeRate = Val(R!FNExchangeRate.ToString)

    '        Next

    '        cmdstring = "SELECT TOP 1 FNHSysCmpId,FNHSysCmpIdTo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery  WITH(NOLOCK)  WHERE FNHSysDeliveryId=" & Val(FNHSysDeliveryId) & " "
    '        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '        For Each R As DataRow In dt.Rows

    '            FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
    '            FNHSysCmpIdTo = Val(R!FNHSysCmpIdTo.ToString)

    '        Next

    '        Dim dtpoordercmp As DataTable
    '        cmdstring = " SELECT O.FNHSysCmpId "
    '        cmdstring &= vbCrLf & " , P.FTPurchaseNo "
    '        cmdstring &= vbCrLf & " , P.FNPoState"
    '        cmdstring &= vbCrLf & " , Cmp.FNHSysCmpIdTo AS FNHSysCmpIdDelivery"
    '        cmdstring &= vbCrLf & " , D.FNHSysCmpIdTo As FNHSysCmpIdToDelivery"
    '        cmdstring &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK) INNER JOIN"
    '        cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On P.FTPurchaseNo = PD.FTPurchaseNo INNER JOIN"
    '        cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON PD.FTOrderNo = O.FTOrderNo INNER JOIN"
    '        cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON O.FNHSysCmpId = Cmp.FNHSysCmpId INNER JOIN"
    '        cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery As D With(NOLOCK) On Cmp.FNHSysDeliveryIdDomestic = D.FNHSysDeliveryId"
    '        cmdstring &= vbCrLf & " WHERE (P.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(pokey) & "') "
    '        cmdstring &= vbCrLf & " And O.FNOrderType <> 4 "
    '        cmdstring &= vbCrLf & " GROUP BY O.FNHSysCmpId, P.FTPurchaseNo, P.FNPoState, Cmp.FNHSysCmpIdTo, D.FNHSysCmpIdTo "
    '        dtpoordercmp = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '        If dtpoordercmp.Rows.Count <= 0 Then
    '            Exit Sub
    '        End If

    '        If dtpoordercmp.Select("FNHSysCmpIdDelivery<>" & FNHSysCmpId & " ").Length <= 0 Then

    '            cmdstring = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(pokey) & "'"
    '            pofacno = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

    '            cmdstring = "SELECT TOP 1 FNExchangeRate  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
    '            POFacExcRate = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "-1"))

    '            If FNHSysCmpId > 0 And FNHSysCmpIdTo > 0 Then

    '                FNPoState = "0"

    '                If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                    vatper = 0
    '                    SysCurId = 1310190001

    '                    Dim _Qry As String = ""

    '                    _Qry = " SELECT TOP 1 FNBuyingRate"
    '                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
    '                    _Qry &= vbCrLf & "   WHERE  (FDDate ='" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "')"
    '                    _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
    '                    _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
    '                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
    '                    _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
    '                    _Qry &= vbCrLf & "  ))"

    '                    ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

    '                    If POFacExcRate = ExcRate Then
    '                        Exit Sub
    '                    End If

    '                    FNPoState = "1"
    '                Else
    '                    FNPoState = "0"
    '                    If FNExchangeRate.Value = 1 And pofacno <> "" Then
    '                        Exit Sub
    '                    End If

    '                    SysCurId = 1310200002
    '                    ExcRate = 1

    '                End If

    '                Dim _POType As String = "D"
    '                Select Case FNHSysCmpId
    '                    Case 1311090006, 1311090005, 1410220001, 1501190001
    '                        _POType = "I"
    '                        vatper = 0

    '                        If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then

    '                            FNPoState = "1"

    '                        Else

    '                            FNPoState = "1"

    '                        End If

    '                    Case Else

    '                        If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then

    '                            _POType = "I"
    '                            vatper = 0

    '                        Else

    '                            _POType = "D"
    '                            vatper = 7

    '                        End If

    '                End Select

    '                'If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                '    vatper = 0
    '                'Else
    '                '    vatper = 7
    '                'End If

    '                If pofacno = "" Then

    '                    ' pofacno = "F" & pokey

    '                    Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
    '                    Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
    '                    Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
    '                    Dim _CmpHCreate As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(FNHSysCmpIdTo) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                    Dim _CmpRunText As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 13), 2)
    '                    Dim _POGrp As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 9), 2)


    '                    pofacno = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTFacPurchase", "", False, _CmpHCreate & "F" & _CmpRunText & _Year & _POGrp & _POType & _Month).ToString
    '                    ' pofacno = Microsoft.VisualBasic.Left(pokey, 2) & "FA" & Microsoft.VisualBasic.Right(pokey, pokey.Length - 2)

    '                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
    '                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
    '                    cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
    '                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
    '                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
    '                    cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
    '                    cmdstring &= vbCrLf & ")"
    '                    cmdstring &= vbCrLf & " SELECT  FTInsUser, FDInsDate, FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTPurchaseState, FTRefer," & FNPoState & " FNPoState, FNHSysPurGrpId,"
    '                    cmdstring &= vbCrLf & "     FNHSysCmpRunId," & FNHSysCmpId & " AS  FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId," & SysCurId & " AS FNHSysCurId," & ExcRate & " AS  FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
    '                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt," & vatper & " FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
    '                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,  "
    '                    cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(pokey) & "' AS FTPurchaseNoRef," & FNHSysCmpIdTo & " AS  FNHSysCmpIdCreate"
    '                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                Else

    '                    cmdstring = "UPDATE A Set "
    '                    cmdstring &= vbCrLf & "  FTInsUser=B.FTInsUser"
    '                    cmdstring &= vbCrLf & ", FDInsDate=B.FDInsDate"
    '                    cmdstring &= vbCrLf & ", FTInsTime=B.FTInsTime"
    '                    cmdstring &= vbCrLf & ",  FTRefer=B.FTRefer"
    '                    cmdstring &= vbCrLf & ", FNPoState=B.FNPoState"
    '                    cmdstring &= vbCrLf & ", FNHSysPurGrpId=B.FNHSysPurGrpId"
    '                    cmdstring &= vbCrLf & ", FNHSysCmpRunId=B.FNHSysCmpRunId"
    '                    cmdstring &= vbCrLf & ", FNHSysCmpId=B.FNHSysCmpId"
    '                    cmdstring &= vbCrLf & ", FDDeliveryDate=B.FDDeliveryDate"
    '                    cmdstring &= vbCrLf & ", FNHSysCrTermId=B.FNHSysCrTermId"
    '                    cmdstring &= vbCrLf & ", FNCreditDay=B.FNCreditDay"
    '                    cmdstring &= vbCrLf & ", FNHSysTermOfPMId=B.FNHSysTermOfPMId"
    '                    cmdstring &= vbCrLf & ", FNHSysCurId=" & SysCurId & ""
    '                    cmdstring &= vbCrLf & ", FNExchangeRate=" & ExcRate & ""
    '                    cmdstring &= vbCrLf & ", FNHSysDeliveryId=B.FNHSysDeliveryId"
    '                    cmdstring &= vbCrLf & ", FTContactPerson=B.FTContactPerson"
    '                    cmdstring &= vbCrLf & ", FTRemark=B.FTRemark"
    '                    cmdstring &= vbCrLf & ", FNPoAmt=B.FNPoAmt"
    '                    cmdstring &= vbCrLf & ", FNDisCountPer=B.FNDisCountPer"
    '                    cmdstring &= vbCrLf & ", FNDisCountAmt=B.FNDisCountAmt"
    '                    cmdstring &= vbCrLf & ", FNPONetAmt=B.FNPONetAmt"
    '                    cmdstring &= vbCrLf & ", FNVatPer=" & vatper & ""
    '                    cmdstring &= vbCrLf & ", FNVatAmt=B.FNVatAmt"
    '                    cmdstring &= vbCrLf & ", FNSurcharge=B.FNSurcharge"
    '                    cmdstring &= vbCrLf & ", FNPOGrandAmt=B.FNPOGrandAmt"
    '                    cmdstring &= vbCrLf & ", FTPOGrandAmtTH=B.FTPOGrandAmtTH"
    '                    cmdstring &= vbCrLf & ", FTPOGrandAmtEN=B.FTPOGrandAmtEN"
    '                    cmdstring &= vbCrLf & ", FTStateSendApp=B.FTStateSendApp"
    '                    cmdstring &= vbCrLf & ", FTSendAppBy=B.FTSendAppBy"
    '                    cmdstring &= vbCrLf & ", FTSendAppDate=B.FTSendAppDate"
    '                    cmdstring &= vbCrLf & ", FTSendAppTime=B.FTSendAppTime"
    '                    cmdstring &= vbCrLf & ", FNPoType=B.FNPoType"
    '                    cmdstring &= vbCrLf & ", FNHSysCmpIdCreate=" & FNHSysCmpIdTo & ""
    '                    cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As B"
    '                    cmdstring &= vbCrLf & "  WHERE A.FTPurchaseNoRef = B.FTPurchaseNo"
    '                    cmdstring &= vbCrLf & "  And A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                End If

    '                cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
    '                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
    '                cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
    '                cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate,FTPurchaseNo"
    '                cmdstring &= vbCrLf & ")"
    '                cmdstring &= vbCrLf & " Select  A.FTInsUser, A.FDInsDate, A.FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo"
    '                cmdstring &= vbCrLf & ",  A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysUnitId"
    '                cmdstring &= vbCrLf & ", (A.FNPrice + Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))) AS FNPrice , A.FNDisPer, A.FNDisAmt, A.FNQuantity, A.FNNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity)) AS FNNetAmt"
    '                cmdstring &= vbCrLf & ", A.FTRemark, "
    '                cmdstring &= vbCrLf & "  A.FTFabricFrontSize, A.FNReservePOQuantity, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN, A.FNSurchangeAmt, A.FNSurchangePerUnit, A.FNGrandNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,2),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity))  AS FNGrandNetAmt, A.FTOGacDate,A.FTPurchaseNo"
    '                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A "

    '                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON A.FTPurchaseNo=H.FTPurchaseNo"
    '                cmdstring &= vbCrLf & "  INNER Join "
    '                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
    '                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
    '                cmdstring &= vbCrLf & " (SELECT  *  "
    '                cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
    '                cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & FNHSysCmpIdTo & ""
    '                cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"
    '                cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                cmdstring = " UPDATE B SET "
    '                cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,4),(B.FNPrice * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,4),(B.FNDisAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,4),(B.FNNetAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,4),(B.FNSurchangeAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,4),(B.FNSurchangePerUnit * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
    '                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo INNER Join"
    '                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As C On A.FTPurchaseNoRef = C.FTPurchaseNo "
    '                cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                podisamt = CDbl(Format((podisamt * FNExchangeRate.Value) / ExcRate, "0.00")) ' Val(R!FNDisCountAmt.ToString)
    '                Surcharge = CDbl(Format((Surcharge * FNExchangeRate.Value) / ExcRate, "0.00"))  'Val(R!FNSurcharge.ToString)

    '                cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
    '                cmdstring &= vbCrLf & "    FROM"
    '                cmdstring &= vbCrLf & " ("
    '                cmdstring &= vbCrLf & " SELECT        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
    '                cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  WITH(NOLOCK)"
    '                cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
    '                cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

    '                poamt = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
    '                ponetamt = poamt - podisamt
    '                povatamt = CDbl(Format((ponetamt * vatper) / 100.0, "0.00"))
    '                pograndamt = ponetamt + povatamt + Surcharge

    '                poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
    '                poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

    '                cmdstring = "UPDATE A Set "
    '                cmdstring &= vbCrLf & "  FNPoAmt=" & poamt & ""
    '                cmdstring &= vbCrLf & ", FNPONetAmt=" & ponetamt & ""
    '                cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
    '                cmdstring &= vbCrLf & ", FNPOGrandAmt=" & pograndamt & ""
    '                cmdstring &= vbCrLf & ", FNSurcharge=" & Surcharge & ""
    '                cmdstring &= vbCrLf & ", FNDisCountAmt=" & podisamt & ""
    '                cmdstring &= vbCrLf & ", FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
    '                cmdstring &= vbCrLf & ", FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"

    '                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
    '                cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '            Else

    '                If pofacno <> "" Then

    '                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""
    '                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""

    '                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                End If

    '            End If

    '        Else

    '            Dim PoVat As Double = vatper

    '            For Each R As DataRow In dtpoordercmp.Select("FNHSysCmpIdDelivery<>" & FNHSysCmpId & "")

    '                FNHSysCmpIdTo = Val(R!FNHSysCmpIdDelivery)

    '                Select Case Val(R!FNHSysCmpId.ToString)
    '                    Case 1311090006, 1311090005, 1410220001, 1501190001
    '                        FNHSysCmpIdTo = FNHSysCmpIdTo
    '                End Select

    '                vatper = PoVat
    '                cmdstring = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(pokey) & "' AND FNHSysCmpIdCreate= " & FNHSysCmpIdTo & ""
    '                pofacno = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

    '                cmdstring = "SELECT TOP 1 FNExchangeRate  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
    '                POFacExcRate = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "-1"))

    '                If FNHSysCmpIdTo > 0 Then
    '                    FNPoState = "0"
    '                    If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                        vatper = 0
    '                        SysCurId = 1310190001

    '                        Dim _Qry As String = ""

    '                        _Qry = " SELECT TOP 1 FNBuyingRate"
    '                        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
    '                        _Qry &= vbCrLf & "   WHERE  (FDDate ='" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "')"
    '                        _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
    '                        _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
    '                        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
    '                        _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
    '                        _Qry &= vbCrLf & "  ))"

    '                        ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

    '                        If POFacExcRate = ExcRate Then
    '                            ' Exit Sub
    '                        End If
    '                        FNPoState = "1"
    '                    Else

    '                        If FNExchangeRate.Value = 1 And pofacno <> "" Then
    '                            ' Exit Sub
    '                        End If

    '                        SysCurId = 1310200002
    '                        ExcRate = 1
    '                        FNPoState = "0"
    '                    End If

    '                    Dim _POType As String = "D"
    '                    Select Case FNHSysCmpId
    '                        Case 1311090006, 1311090005, 1410220001, 1501190001
    '                            _POType = "I"
    '                            vatper = 0
    '                            If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                                FNPoState = "1"
    '                            Else
    '                                FNPoState = "1"
    '                            End If
    '                        Case Else
    '                            If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                                _POType = "I"
    '                                vatper = 0
    '                            Else
    '                                _POType = "D"
    '                                vatper = 7
    '                            End If
    '                    End Select

    '                    'If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                    '    vatper = 0
    '                    'Else
    '                    '    vatper = 7
    '                    'End If

    '                    If pofacno = "" Then

    '                        ' pofacno = "F" & pokey

    '                        Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
    '                        Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
    '                        Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
    '                        Dim _CmpHCreate As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(FNHSysCmpIdTo) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
    '                        Dim _CmpRunText As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 13), 2)
    '                        Dim _POGrp As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 9), 2)

    '                        'Dim _POType As String = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(pokey, 7), 1)
    '                        'If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
    '                        '    _POType = "I"
    '                        'End If

    '                        pofacno = HI.TL.Document.GetDocumentNo(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR), "TPURTFacPurchase", "", False, _CmpHCreate & "F" & _CmpRunText & _Year & _POGrp & _POType & _Month).ToString

    '                        ' pofacno = Microsoft.VisualBasic.Left(pokey, 2) & "FA" & Microsoft.VisualBasic.Right(pokey, pokey.Length - 2)

    '                        cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
    '                        cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
    '                        cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
    '                        cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
    '                        cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
    '                        cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
    '                        cmdstring &= vbCrLf & ")"
    '                        cmdstring &= vbCrLf & " SELECT  FTInsUser, FDInsDate, FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo,'" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "' AS FDPurchaseDate, FTPurchaseBy, FTPurchaseState, FTRefer," & FNPoState & " FNPoState, FNHSysPurGrpId,"
    '                        cmdstring &= vbCrLf & "     FNHSysCmpRunId," & FNHSysCmpId & " AS  FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId," & SysCurId & " AS FNHSysCurId," & ExcRate & " AS  FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
    '                        cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt," & vatper & " AS  FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
    '                        cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,  "
    '                        cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(pokey) & "' AS FTPurchaseNoRef," & FNHSysCmpIdTo & " AS  FNHSysCmpIdCreate"
    '                        cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

    '                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                    Else

    '                        cmdstring = "UPDATE A Set "
    '                        cmdstring &= vbCrLf & "  FTInsUser=B.FTInsUser"
    '                        cmdstring &= vbCrLf & ", FDInsDate=B.FDInsDate"
    '                        cmdstring &= vbCrLf & ", FTInsTime=B.FTInsTime"
    '                        cmdstring &= vbCrLf & ",  FTRefer=B.FTRefer"
    '                        cmdstring &= vbCrLf & ", FNPoState=B.FNPoState"
    '                        cmdstring &= vbCrLf & ", FNHSysPurGrpId=B.FNHSysPurGrpId"
    '                        cmdstring &= vbCrLf & ", FNHSysCmpRunId=B.FNHSysCmpRunId"
    '                        cmdstring &= vbCrLf & ", FNHSysCmpId=B.FNHSysCmpId"
    '                        cmdstring &= vbCrLf & ", FDDeliveryDate=B.FDDeliveryDate"
    '                        cmdstring &= vbCrLf & ", FNHSysCrTermId=B.FNHSysCrTermId"
    '                        cmdstring &= vbCrLf & ", FNCreditDay=B.FNCreditDay"
    '                        cmdstring &= vbCrLf & ", FNHSysTermOfPMId=B.FNHSysTermOfPMId"
    '                        cmdstring &= vbCrLf & ", FNHSysCurId=" & SysCurId & ""
    '                        cmdstring &= vbCrLf & ", FNExchangeRate=" & ExcRate & ""
    '                        cmdstring &= vbCrLf & ", FNHSysDeliveryId=B.FNHSysDeliveryId"
    '                        cmdstring &= vbCrLf & ", FTContactPerson=B.FTContactPerson"
    '                        cmdstring &= vbCrLf & ", FTRemark=B.FTRemark"
    '                        cmdstring &= vbCrLf & ", FNPoAmt=B.FNPoAmt"
    '                        cmdstring &= vbCrLf & ", FNDisCountPer=B.FNDisCountPer"
    '                        cmdstring &= vbCrLf & ", FNDisCountAmt=B.FNDisCountAmt"
    '                        cmdstring &= vbCrLf & ", FNPONetAmt=B.FNPONetAmt"
    '                        cmdstring &= vbCrLf & ", FNVatPer=" & vatper & ""
    '                        cmdstring &= vbCrLf & ", FNVatAmt=B.FNVatAmt"
    '                        cmdstring &= vbCrLf & ", FNSurcharge=B.FNSurcharge"
    '                        cmdstring &= vbCrLf & ", FNPOGrandAmt=B.FNPOGrandAmt"
    '                        cmdstring &= vbCrLf & ", FTPOGrandAmtTH=B.FTPOGrandAmtTH"
    '                        cmdstring &= vbCrLf & ", FTPOGrandAmtEN=B.FTPOGrandAmtEN"
    '                        cmdstring &= vbCrLf & ", FTStateSendApp=B.FTStateSendApp"
    '                        cmdstring &= vbCrLf & ", FTSendAppBy=B.FTSendAppBy"
    '                        cmdstring &= vbCrLf & ", FTSendAppDate=B.FTSendAppDate"
    '                        cmdstring &= vbCrLf & ", FTSendAppTime=B.FTSendAppTime"
    '                        cmdstring &= vbCrLf & ", FNPoType=B.FNPoType"
    '                        cmdstring &= vbCrLf & ", FNHSysCmpIdCreate=" & FNHSysCmpIdTo & ""
    '                        cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As B"
    '                        cmdstring &= vbCrLf & "  WHERE A.FTPurchaseNoRef = B.FTPurchaseNo"
    '                        cmdstring &= vbCrLf & "  And A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                    End If

    '                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
    '                    cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
    '                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
    '                    cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate,FTPurchaseNo"
    '                    cmdstring &= vbCrLf & ")"
    '                    cmdstring &= vbCrLf & " Select  A.FTInsUser, A.FDInsDate, A.FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo"
    '                    cmdstring &= vbCrLf & ",  A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysUnitId"
    '                    cmdstring &= vbCrLf & ", (A.FNPrice + Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))) AS FNPrice , A.FNDisPer, A.FNDisAmt, A.FNQuantity, A.FNNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity)) AS FNNetAmt"
    '                    cmdstring &= vbCrLf & ", A.FTRemark, "
    '                    cmdstring &= vbCrLf & "  A.FTFabricFrontSize, A.FNReservePOQuantity, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN, A.FNSurchangeAmt, A.FNSurchangePerUnit, A.FNGrandNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,2),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity))  AS FNGrandNetAmt, A.FTOGacDate,A.FTPurchaseNo"
    '                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A "

    '                    cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON A.FTPurchaseNo=H.FTPurchaseNo"
    '                    cmdstring &= vbCrLf & "  INNER Join "
    '                    cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
    '                    cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
    '                    cmdstring &= vbCrLf & " (SELECT  *  "
    '                    cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
    '                    cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & FNHSysCmpIdTo & ""
    '                    cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"

    '                    cmdstring &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON A.FTOrderNo = O.FTOrderNo INNER JOIN"
    '                    cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS Cmp WITH(NOLOCK) ON O.FNHSysCmpId = Cmp.FNHSysCmpId "


    '                    cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "' AND Cmp.FNHSysCmpIdTo=" & FNHSysCmpIdTo & " " 'AND O.FNOrderType <> 4"

    '                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                    cmdstring = " UPDATE B SET "
    '                    cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,4),(B.FNPrice * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                    cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,4),(B.FNDisAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                    cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,4),(B.FNNetAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                    cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,4),(B.FNSurchangeAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                    cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,4),(B.FNSurchangePerUnit * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                    cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt * " & FNExchangeRate.Value & ")/A.FNExchangeRate )"
    '                    cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
    '                    cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo INNER Join"
    '                    cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As C On A.FTPurchaseNoRef = C.FTPurchaseNo "
    '                    cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                    podisamt = CDbl(Format((podisamt * FNExchangeRate.Value) / ExcRate, "0.00")) ' Val(R!FNDisCountAmt.ToString)
    '                    Surcharge = CDbl(Format((Surcharge * FNExchangeRate.Value) / ExcRate, "0.00"))  'Val(R!FNSurcharge.ToString)

    '                    cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
    '                    cmdstring &= vbCrLf & "    FROM"
    '                    cmdstring &= vbCrLf & " ("
    '                    cmdstring &= vbCrLf & " SELECT        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
    '                    cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  WITH(NOLOCK)"
    '                    cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
    '                    cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

    '                    poamt = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
    '                    ponetamt = poamt - podisamt
    '                    povatamt = CDbl(Format((ponetamt * vatper) / 100.0, "0.00"))
    '                    pograndamt = ponetamt + povatamt + Surcharge

    '                    poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
    '                    poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

    '                    cmdstring = "UPDATE A Set "
    '                    cmdstring &= vbCrLf & "  FNPoAmt=" & poamt & ""
    '                    cmdstring &= vbCrLf & ", FNPONetAmt=" & ponetamt & ""
    '                    cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
    '                    cmdstring &= vbCrLf & ", FNPOGrandAmt=" & pograndamt & ""
    '                    cmdstring &= vbCrLf & ", FNSurcharge=" & Surcharge & ""
    '                    cmdstring &= vbCrLf & ", FNDisCountAmt=" & podisamt & ""
    '                    cmdstring &= vbCrLf & ", FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
    '                    cmdstring &= vbCrLf & ", FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"

    '                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
    '                    cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

    '                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                Else

    '                    'If pofacno <> "" Then

    '                    '    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""
    '                    '    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""

    '                    '    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

    '                    'End If

    '                End If

    '            Next
    '        End If

    '    Catch ex As Exception
    '    End Try

    'End Sub
    Private Sub LoadData(HSysId As String)
        Dim _Str As String = Me.Query & "  WHERE " & Me.MainKey & "='" & HSysId & "' "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
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

        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)

            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit

                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With

            End Select

        Next

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        FDReceiveDate.Properties.Buttons(0).Visible = False
        FDReceiveDate.Properties.ReadOnly = True

        _FormLoad = False
    End Sub

#End Region

#Region "MAIN PROC"

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTReceiveBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then

            Return True

        Else

            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTReceiveBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then

                    Return True

                Else

                    HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False

                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If

        End If

    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDReceiveDate.Text) = True Then
            Exit Sub
        End If

        If Me.VerrifyData Then
            If Me.SaveData() Then

                FDReceiveDate.Properties.Buttons(0).Visible = False
                FDReceiveDate.Properties.ReadOnly = True

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If

    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDReceiveDate.Text) = True Then
            Exit Sub
        End If


        If FTStateImport.Checked = False Then

            If Barcode.CheckDucumentCreateBar(FTReceiveNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
                Exit Sub
            End If

        Else

            If Barcode.CheckDocumentRefOut(FTReceiveNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ไม่สามารถลบหรือแก้ไขได้ !!!", 1311240006, Me.Text)
                Exit Sub
            End If

        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTReceiveNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        If Me.DeleteData(FTStateImport.Checked) Then

            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)

            Me.DefaultsData()
            Me.FTPurchaseNo.Focus()

        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTReceiveNo.Text <> "" Then

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "ReceiveSlip.rpt"
                .Formular = "{TINVENReceive.FTReceiveNo}='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "ReceiveSlip_Barcode.rpt"
                .Formular = "{TINVENReceive.FTReceiveNo}='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Sub LoadPOInfo(PoKey As String, Optional LoadRcv As Boolean = False)
        Dim _Str As String = ""
        Dim Dt As DataTable

        _Str = " SELECT        H.FTPurchaseNo, H.FNExchangeRate, S.FTSuplCode, C.FTCurCode"
        _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON H.FNHSysSuplId = S.FNHSysSuplId INNER JOIN"
        _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH (NOLOCK) ON H.FNHSysCurId = C.FNHSysCurId"
        _Str &= vbCrLf & "  WHERE H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
        Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PUR)

        If Dt.Rows.Count > 0 Then

            For Each R As DataRow In Dt.Rows
                FNHSysSuplId.Text = R!FTSuplCode.ToString
                FNHSysCurId.Text = R!FTCurCode.ToString

                If Not (LoadRcv) Then
                    'FNExchangeRate.Value = Val(R!FNExchangeRate.ToString)
                End If

                Exit For
            Next

        Else
            FNHSysSuplId.Text = ""
            FNHSysCurId.Text = ""

            If Not (LoadRcv) Then
                FNExchangeRate.Value = 0
            End If

        End If

        If Not (LoadRcv) Then
            If Not (_ProcLoad) Then
                Call SetDefaultWH(PoKey)
            End If
        End If

    End Sub

#End Region

#Region " Variable "

#End Region

    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
            _FormLoad = False

            Dim Indx As Integer = 0
            Try
                Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
            Catch ex As Exception
            End Try


            FNListDocumentData.SelectedIndex = Indx

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCurId.EditValueChanged
        If _FormLoad Then Exit Sub
        If FNHSysCurId.Text = "" Then
            FNExchangeRate.Value = 0
            Exit Sub
        End If
        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

            FNExchangeRate.Properties.ReadOnly = True

            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 1
            End If

        Else
            Dim _Qry As String = ""

            FNExchangeRate.Properties.ReadOnly = True
            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 0


                _Qry = " SELECT TOP 1 FNBuyingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDReceiveDate.Text) & "')"
                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "'"
                _Qry &= vbCrLf & "  ))"
                '  Dim _Exc As Double = 0
                ' _Exc = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))
                FNExchangeRate.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

                If FNExchangeRate.Value <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
                End If

                Dim _Str As String = ""
                Dim _VatPer As Double = 0

                _Str = "SELECT TOP 1 FNVatPer "
                _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS PH WITH(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

                _VatPer = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_PUR, "0")))

                If _VatPer = 0 Then
                    _Str = "  SELECT TOP 1  FTStateRcvVat"
                    _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK)"
                    _Str &= vbCrLf & "  WHERE (FTSuplCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "')"

                    If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then
                        _VatPer = 7
                    End If

                End If

                FNVatPer.Value = _VatPer

            End If

            _Qry = "  SELECT TOP 1  FTStateRcvAdjExchangeRate"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE (FTSuplCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "')"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

                _Qry = " Select TOP 1  FTReceiveNo"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "


                FNExchangeRate.Properties.ReadOnly = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "")
            End If

        End If
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With Me.ogvdetail
                'FTStateSendApp
                Select Case True
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTStateSendApp").ToString = "1")
                        e.Appearance.BackColor = Drawing.Color.LightCyan
                        e.Appearance.ForeColor = Drawing.Color.Blue
                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As System.Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged


        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            Me.FTPurchaseNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTPurchaseNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            Me.FNRceceiveType.Properties.ReadOnly = (dt.Rows.Count > 0)

            Me.FTPurchaseNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTPurchaseNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            Me.FNHSysWHId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            ' FNExchangeRate.Properties.ReadOnly = (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Delegate Sub FTPurchaseNo_ValueChange(sender As System.Object, e As System.EventArgs)
    Private Sub FTPurchaseNo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTPurchaseNo.EditValueChanged
        If Not (_ProcLoad) Then
            If FTPurchaseNo.Text <> "" Then
                If Me.InvokeRequired Then
                    Me.Invoke(New FTPurchaseNo_ValueChange(AddressOf FTPurchaseNo_EditValueChanged), New Object() {sender, e})
                Else
                    Call LoadPOInfo(FTPurchaseNo.Text)
                End If

            End If
        End If
    End Sub

    Private Sub FTReceiveNo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTReceiveNo.EditValueChanged
    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNExchangeRate.EditValueChanged
        ' If FNExchangeRate.Value <= 0 Then FNExchangeRate.Value = 1
    End Sub

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        If CheckOwner() = False Then Exit Sub
        If FTStateImport.Checked Then Exit Sub

        If Barcode.CheckDucumentCreateBar(FTReceiveNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDReceiveDate.Text) = True Then
            Exit Sub
        End If

        'Dim _CmpH As String = ""
        'For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

        '    Select Case HI.ENM.Control.GeTypeControl(ctrl)
        '        Case ENM.Control.ControlType.ButtonEdit
        '            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
        '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK) WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        '            End With

        '            Exit For
        '        Case ENM.Control.ControlType.TextEdit
        '            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
        '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        '            End With

        '            Exit For
        '    End Select

        'Next

        ' If FTReceiveNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then
        If FTReceiveNo.Properties.Tag.ToString = "" Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTReceiveNo.Text = "" Or Me.FTReceiveNo.Properties.Tag.ToString = "" Then
                Exit Sub
            Else
                If FTPurchaseNo.Properties.ReadOnly = False Then
                    If Me.SaveData Then
                    Else
                        Exit Sub
                    End If
                End If
            End If
        End If

        Dim _dt As DataTable
        Dim _Str As String = ""

        _Str = " Select M.FTRawMatCode"
        _Str &= vbCrLf & "  ,MAX(M.FTRawMatName) AS FTRawMatName"
        _Str &= vbCrLf & "  ,MAX(M.FTUnitCode) AS FTUnitCode "
        _Str &= vbCrLf & "  ,MAX(ISNULL(U.FTUnitCode,'')) AS FNHSysUnitId"
        _Str &= vbCrLf & "  ,MAX(ISNULL(U.FNHSysUnitId,0)) AS  FNHSysUnitId_Hide"
        _Str &= vbCrLf & "  FROM"
        _Str &= vbCrLf & "  (SELECT  P.FNHSysRawMatId"
        _Str &= vbCrLf & "  ,P.FNHSysUnitId AS FNHSysUnitId"
        _Str &= vbCrLf & "  ,IU.FTUnitCode "
        _Str &= vbCrLf & "  ,IM.FNHSysUnitId  AS FNHSysUnitIdIM"
        _Str &= vbCrLf & "  ,IM.FTRawMatCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & "  ,IM.FTRawMatNameTH AS FTRawMatName"
        Else
            _Str &= vbCrLf & "  ,IM.FTRawMatNameEN AS FTRawMatName"
        End If

        _Str &= vbCrLf & "  ,ISNULL(("
        _Str &= vbCrLf & "    SELECT      TOP 1   ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId"
        _Str &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
        _Str &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
        _Str &= vbCrLf & "   WHERE ISNULL(B.FTRawMatCode,'') =IM.FTRawMatCode "
        _Str &= vbCrLf & "  ),0) AS FNHSysUnitIdRCV"
        _Str &= vbCrLf & "  ,ISNULL(("
        _Str &= vbCrLf & "    SELECT      TOP 1   ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId"
        _Str &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
        _Str &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
        _Str &= vbCrLf & "   WHERE ISNULL(B.FTRawMatCode,'') =IM.FTRawMatCode "
        _Str &= vbCrLf & "  ),0) AS FNHSysUnitIdADJ"
        _Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH (NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)"
        _Str &= vbCrLf & "  ON P.FNHSysRawMatId = IM.FNHSysRawMatId"
        _Str &= vbCrLf & "   INNER JOIN [HITECH_MASTER].dbo.TCNMUnit AS IU WITH(NOLOCK)  ON"
        _Str &= vbCrLf & "    P.FNHSysUnitId = IU.FNHSysUnitId"
        _Str &= vbCrLf & "  WHERE      (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
        _Str &= vbCrLf & "  ) AS M LEFT OUTER JOIN"

        _Str &= vbCrLf & "  ("
        _Str &= vbCrLf & "  SELECT FNHSysUnitId, FTUnitCode"
        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK)"
        _Str &= vbCrLf & "  WHERE  (FTStateUnitStock = '1')"
        _Str &= vbCrLf & "  ) AS U ON M.FNHSysUnitIdIM = U.FNHSysUnitId "
        _Str &= vbCrLf & "  WHERE M.FNHSysUnitIdRCV =0 AND M.FNHSysUnitIdADJ=0"
        _Str &= vbCrLf & "  GROUP BY   M.FTRawMatCode "
        _Str &= vbCrLf & "  ORDER  BY   M.FTRawMatCode "
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        If _dt.Rows.Count > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูล วัตถุดิบ ที่ยังไม่มีการ เดิน Transaction ใน Stock กรุณาทำการระบุหน่วยที่ใช้ในการจัดเก็บ !!!", 1410020013, Me.Text, , MessageBoxIcon.Warning)

            HI.TL.HandlerControl.ClearControl(_ReceiveUnitRawMat)

            With _ReceiveUnitRawMat
                Call HI.ST.Lang.SP_SETxLanguage(_ReceiveUnitRawMat)

                .ocmreceive.Enabled = True
                .ocmcancel.Enabled = True

                .ogcrcv.DataSource = _dt
                .ProcessProc = False
                .ShowDialog()

                If .ProcessProc = False Then
                    Exit Sub
                End If

            End With

        End If


        _Str = " SELECT  (CASE WHEN ISNULL(FNRcvQty,0) > 0 THEN '1' ELSE '0' END) AS FTStateSelect"
        _Str &= vbCrLf & "    ,P.FNHSysRawMatId,"

        _Str &= vbCrLf & "  IM.FTRawMatCode ,"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & "  IM.FTRawMatNameTH AS FTMatDesc,"
        Else
            _Str &= vbCrLf & "  IM.FTRawMatNameEN AS FTMatDesc,"
        End If

        _Str &= vbCrLf & "  C.FTRawMatColorCode,"
        _Str &= vbCrLf & "  S.FTRawMatSizeCode,"
        _Str &= vbCrLf & "  ISNULL(RCV.FTFabricFrontSize,P.FTFabricFrontSize) AS FTFabricFrontSize ,"
        _Str &= vbCrLf & "  P.FNHSysUnitId,"
        _Str &= vbCrLf & "  U.FTUnitCode ,"
        _Str &= vbCrLf & "  P.FNPrice,"
        _Str &= vbCrLf & "  P.FNDisPer,"
        _Str &= vbCrLf & "  P.FNDisAmt,"
        _Str &= vbCrLf & "ISNULL(P.FNSurchangePerUnit, 0) AS FNSurchangePerUnit,"
        _Str &= vbCrLf & "ISNULL(P.FNSurchangeAmt, 0) AS FNSurchangeAmt,"
        _Str &= vbCrLf & "  ((P.FNPrice- P.FNDisAmt ) + ISNULL(P.FNSurchangePerUnit,0)) AS FNNetPrice,"
        _Str &= vbCrLf & "   P.FNQuantity,"
        _Str &= vbCrLf & "  ISNULL(RCV.FNRcvHisQty,0) As FNRcvHisQty,"
        _Str &= vbCrLf & "  (P.FNQuantity-ISNULL(RCV.FNRcvHisQty,0)) AS FNPOBalQty,"
        _Str &= vbCrLf & "  Convert(numeric(18,4),ISNULL(FNRcvQty,0)) AS FNRcvQty"
        _Str &= vbCrLf & ",'' As FTStateRcvOver"
        _Str &= vbCrLf & ",'0' As FTStateSendAppRcv"
        _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyPass"
        _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyOver"
        _Str &= vbCrLf & "   FROM "
        _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FNHSysRawMatId, FNHSysUnitId,Max(FTFabricFrontSize) AS FTFabricFrontSize"

        If FNRceceiveType.SelectedIndex = 2 Then
            _Str &= vbCrLf & " ,0 AS  FNPrice,0 AS  FNDisPer, 0 As FNDisAmt , 0 AS FNSurchangePerUnit"
        Else
            _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt  , ISNULL(FNSurchangePerUnit,0) AS FNSurchangePerUnit"
        End If

        _Str &= vbCrLf & ", SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"

        _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH (NOLOCK)"
        _Str &= vbCrLf & " WHERE        (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
        _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNHSysUnitId,FNPrice, FNDisPer, FNDisAmt, ISNULL(FNSurchangePerUnit,0),ISNULL(FNSurchangeAmt,0)) AS P "
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial as IM WITH(NOLOCK ) ON P.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit as U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitId "
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & " LEFT OUTER JOIN (SELECT        RH.FTPurchaseNo, RD.FNHSysRawMatId,MAX(RD.FTFabricFrontSize) AS FTFabricFrontSize"
        _Str &= vbCrLf & "  ,SUM(CASE WHEN RH.FTReceiveNo<>N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Properties.Tag.ToString) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvHisQty ,"
        _Str &= vbCrLf & " SUM(CASE WHEN RH.FTReceiveNo=N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Properties.Tag.ToString) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvQty "
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Str &= vbCrLf & " WHERE        (RH.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
        _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, RD.FNHSysRawMatId ) As RCV"
        _Str &= vbCrLf & " ON P.FTPurchaseNo = Rcv.FTPurchaseNo AND P.FNHSysRawMatId = Rcv.FNHSysRawMatId "
        _Str &= vbCrLf & " ORDER BY   IM.FTRawMatCode ,  C.FTRawMatColorCode, S.FTRawMatSizeCode"

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        HI.TL.HandlerControl.ClearControl(_AddItemPopup)

        With _AddItemPopup
            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)

            Try
                .ReceiveType = Me.FNRceceiveType.SelectedIndex
            Catch ex As Exception
                .ReceiveType = wReceiveItem.RcvType.RcvNormal
            End Try

            .ogvrcv.ClearColumnsFilter()
            .ogvrcv.ActiveFilter.Clear()
            .PurchaseNo = Me.FTPurchaseNo.Text
            .ReceiveNo = Me.FTReceiveNo.Text
            .ogcrcv.DataSource = _dt
            .ShowDialog()

            If .ProcessProc Then

                CType(.ogcrcv.DataSource, DataTable).AcceptChanges()

                _dt = CType(.ogcrcv.DataSource, DataTable)

                If _dt.Select(" FTStateSelect='1' AND FNRcvQty > 0 ").Length > 0 Then
                    If Me.VerrifyData() Then
                        If Me.SaveData Then
                        Else
                            Exit Sub
                        End If
                    Else
                        Exit Sub
                    End If

                    Dim _Spls As New HI.TL.SplashScreen("Saving Receive Detail...   Please Wait   ")

                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        For Each R As DataRow In _dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv<>'1' ")

                            _Spls.UpdateInformation("Receive Detail....   Please Wait    ")

                            Dim _Surchargeamt As Double = Val(R!FNSurchangeAmt.ToString)
                            Dim _Surchargeperunit As Double = 0
                            If (Val(R!FNRcvQty.ToString) > Val(R!FNPOBalQty.ToString)) And _Surchargeamt > 0 Then

                                If Val(R!FNRcvHisQty.ToString) <= 0 Then

                                    _Surchargeperunit = Double.Parse(Format(_Surchargeamt / Val(R!FNRcvQty.ToString), "0.00000"))
                                    R!FNNetPrice = (Val(R!FNNetPrice) - Val(R!FNSurchangePerUnit)) + _Surchargeperunit
                                    R!FNSurchangePerUnit = _Surchargeperunit

                                Else

                                    If Val(R!FNRcvHisQty.ToString) >= Val(R!FNPOBalQty.ToString) Then

                                        _Surchargeperunit = 0
                                        R!FNNetPrice = (Val(R!FNNetPrice) - Val(R!FNSurchangePerUnit)) + _Surchargeperunit
                                        R!FNSurchangePerUnit = _Surchargeperunit

                                    Else

                                        _Surchargeperunit = Double.Parse(Format((_Surchargeamt - (Val(R!FNRcvHisQty.ToString) * Val(R!FNSurchangePerUnit.ToString))) / Val(R!FNRcvQty.ToString), "0.00000"))
                                        R!FNNetPrice = (Val(R!FNNetPrice) - Val(R!FNSurchangePerUnit)) + _Surchargeperunit
                                        R!FNSurchangePerUnit = _Surchargeperunit

                                    End If

                                End If

                            End If

                            _Str &= vbCrLf & "   P.FNQuantity,"
                            _Str &= vbCrLf & "  ISNULL(RCV.FNRcvHisQty,0) As FNRcvHisQty,"
                            _Str &= vbCrLf & "  (P.FNQuantity-ISNULL(RCV.FNRcvHisQty,0)) AS FNPOBalQty,"
                            _Str &= vbCrLf & "  Convert(numeric(18,4),ISNULL(FNRcvQty,0)) AS FNRcvQty"

                            _Str = "   SELECT   TOP 1    FTReceiveNo, FNHSysRawMatId, FNHSysUnitId"
                            _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity"
                            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                                _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail ( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FNHSysRawMatId,FNHSysUnitId,FTFabricFrontSize,  FNPrice, FNDisPer, FNDisAmt,FNNetPrice,FNNetAmt, FNQuantity,FNSurchangePerUnit) "
                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                                _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                                _Str &= vbCrLf & "," & CDbl(Format(Val(R!FNRcvQty.ToString) * Val(R!FNNetPrice.ToString), HI.ST.Config.AmtFormat)) & " "
                                _Str &= vbCrLf & "," & Val(R!FNRcvQty.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNSurchangePerUnit.ToString) & " "

                            Else

                                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                                _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",FNHSysUnitId=" & Val(R!FNHSysUnitId.ToString) & " "
                                _Str &= vbCrLf & ", FNPrice=" & Val(R!FNPrice.ToString) & " "
                                _Str &= vbCrLf & ", FNDisPer=" & Val(R!FNDisPer.ToString) & " "
                                _Str &= vbCrLf & ", FNDisAmt=" & Val(R!FNDisAmt.ToString) & " "
                                _Str &= vbCrLf & ", FNNetPrice=" & Val(R!FNNetPrice.ToString) & " "
                                _Str &= vbCrLf & ", FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Str &= vbCrLf & ",FNNetAmt=" & CDbl(Format(Val(R!FNRcvQty.ToString) * Val(R!FNNetPrice.ToString), HI.ST.Config.AmtFormat)) & " "
                                _Str &= vbCrLf & ", FNQuantity=" & Val(R!FNRcvQty.ToString) & " "
                                _Str &= vbCrLf & ", FNSurchangePerUnit=" & Val(R!FNSurchangePerUnit.ToString) & " "
                                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "

                            End If

                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                _Spls.Close()
                                Exit Sub
                            End If

                            _Spls.UpdateInformation("Equalize Job....   Please Wait    ")

                            If StockValidate.EqualizeJob(Me.FTReceiveNo.Text, Me.FTPurchaseNo.Text, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, Val(R!FNHSysRawMatId.ToString)) = False Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                _Spls.Close()
                                Exit Sub
                            End If

                        Next

                        For Each R As DataRow In _dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv='1' ")

                            _Spls.UpdateInformation("Receive Detail....   Please Wait    ")

                            Dim _Surchargeamt As Double = Val(R!FNSurchangeAmt.ToString)
                            Dim _Surchargeperunit As Double = 0

                            If (Val(R!FNRcvQty.ToString) > Val(R!FNPOBalQty.ToString)) And _Surchargeamt > 0 Then

                                If Val(R!FNRcvHisQty.ToString) <= 0 Then

                                    _Surchargeperunit = Double.Parse(Format(_Surchargeamt / Val(R!FNRcvQty.ToString), "0.00000"))
                                    R!FNNetPrice = (Val(R!FNNetPrice) - Val(R!FNSurchangePerUnit)) + _Surchargeperunit
                                    R!FNSurchangePerUnit = _Surchargeperunit

                                Else

                                    If Val(R!FNRcvHisQty.ToString) >= Val(R!FNPOBalQty.ToString) Then

                                        _Surchargeperunit = 0

                                        R!FNNetPrice = (Val(R!FNNetPrice) - Val(R!FNSurchangePerUnit)) + _Surchargeperunit
                                        R!FNSurchangePerUnit = _Surchargeperunit

                                    Else

                                        _Surchargeperunit = Double.Parse(Format((_Surchargeamt - (Val(R!FNRcvHisQty.ToString) * Val(R!FNSurchangePerUnit.ToString))) / Val(R!FNRcvQty.ToString), "0.00000"))

                                        R!FNNetPrice = (Val(R!FNNetPrice) - Val(R!FNSurchangePerUnit)) + _Surchargeperunit
                                        R!FNSurchangePerUnit = _Surchargeperunit

                                    End If

                                End If

                            End If

                            _Str = "   SELECT   TOP 1    FTReceiveNo, FNHSysRawMatId, FNHSysUnitId"
                            _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity"
                            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                                _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail ( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FNHSysRawMatId,FNHSysUnitId,FTFabricFrontSize,  FNPrice, FNDisPer, FNDisAmt,FNNetPrice,FNNetAmt, FNQuantity,FNSurchangePerUnit) "
                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                                _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                                _Str &= vbCrLf & ",0 "
                                _Str &= vbCrLf & ",0 "
                                _Str &= vbCrLf & "," & Val(R!FNSurchangePerUnit.ToString) & " "

                            Else

                                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                                _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",FNHSysUnitId=" & Val(R!FNHSysUnitId.ToString) & " "
                                _Str &= vbCrLf & ",FNPrice=" & Val(R!FNPrice.ToString) & " "
                                _Str &= vbCrLf & ",FNDisPer=" & Val(R!FNDisPer.ToString) & " "
                                _Str &= vbCrLf & ",FNDisAmt=" & Val(R!FNDisAmt.ToString) & " "
                                _Str &= vbCrLf & ",FNNetPrice=" & Val(R!FNNetPrice.ToString) & " "
                                _Str &= vbCrLf & ",FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Str &= vbCrLf & ",FNNetAmt=0"
                                _Str &= vbCrLf & ",FNQuantity=0 "
                                _Str &= vbCrLf & ", FNSurchangePerUnit=" & Val(R!FNSurchangePerUnit.ToString) & " "
                                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "

                            End If

                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                _Spls.Close()

                                Exit Sub

                            End If

                        Next

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        If _dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv='1' ").Length > 0 Then

                            _Spls.UpdateInformation("Sending Approve....   Please Wait    ")
                            Call SendMailAppRcvOver(_dt)

                        End If

                        _Spls.Close()

                        ' Exit Sub

                    Catch ex As Exception
                        _Spls.Close()

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Exit Sub
                    End Try

                    Me.LoadRcvDetail(Me.FTReceiveNo.Text)

                End If

            End If

        End With

        _dt.Dispose()

    End Sub

    Private Sub SendMailAppRcvOver(dt As DataTable)
        Dim _Qry As String = ""
        Dim _UserMailTo As String = ""
        _Qry = "SELECT TOP 1  CASE WHEN ISNULL(U.FTStateHelp,'') ='1' THEN  CASE WHEN ISNULL(A.FTSendAppBy,'')='' THEN A.FTPurchaseBy  ELSE  A.FTSendAppBy END ELSE A.FTPurchaseBy END AS FTPurchaseBy "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) ON A.FTPurchaseBy = U.FTUserName"
        _Qry &= vbCrLf & "  WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        _UserMailTo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        If _UserMailTo <> "" Then

            Dim tmpsubject As String = ""
            Dim tmpmessage As String = ""

            tmpsubject = "Send Approve Receive Over Receive No " & Me.FTReceiveNo.Text & "  From Warehouse " & FNHSysWHId.Text & "   "
            tmpmessage = "Send Approve Receive Over Receive No " & Me.FTReceiveNo.Text & "  From Warehouse " & FNHSysWHId.Text & "   "
            tmpmessage &= vbCrLf & "Date :" & Me.FDReceiveDate.Text
            tmpmessage &= vbCrLf & "By :" & Me.FTReceiveBy.Text
            tmpmessage &= vbCrLf & "PO No :" & Me.FTPurchaseNo.Text
            tmpmessage &= vbCrLf & "Note :" & Me.FTRemark.Text

            If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 4, Me.FTReceiveNo.Text) Then


                Dim FNHSysMailAppId As Integer = HI.TL.RunID.GetRunNoID("[HITECH_INVENTORY].dbo.TINVENMailSendAppRcvOver", "FNHSysMailAppId", Conn.DB.DataBaseName.DB_INVEN).ToString()
                For Each R As DataRow In dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv='1' ")

                    _Qry = " SELECT TOP 1 FTReceiveNo "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver AS A with(nolock) "
                    _Qry &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                    _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                    _Qry &= vbCrLf & " AND NOT (ISNULL(FTStateApprove,'0') IN ('1','2')) "

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver"
                        _Qry &= vbCrLf & " (FTSendUser, FDSendDate, FTSendTime, FTToUser, FNHSysMailAppId, FTReceiveNo, FNHSysRawMatId, FNPOQuantity, FNRcvHisQuantity, FNRcvQtyPass, FNRcvQtyOver, FNTotalRcvQty)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_UserMailTo) & "' "
                        _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysMailAppId)) & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Qry &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNQuantity.ToString) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNRcvHisQty.ToString) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNRcvQtyPass.ToString) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNRcvQtyOver.ToString) & " "
                        _Qry &= vbCrLf & "," & Val(R!FNRcvQty.ToString) & " "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)
                    End If

                Next

            End If

        End If
    End Sub

    Private Sub UpdateRawMatUnit(PoNo As String)
        Dim _Qry As String


        _Qry = " UPDATE B SET FNHSysUnitId = ISNULL((SELECT MAX(FNHSysUnitId) FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS C WITH(NOLOCK)  WHERE C.FTRawMatCode =B.FTRawMatCode),0)"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B  ON A.FNHSysRawMatId = B.FNHSysRawMatId"
        _Qry &= vbCrLf & "  WHERE A.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PoNo) & "' "
        _Qry &= vbCrLf & "  AND  B.FNHSysUnitId = 0 "

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

        'FNHSysUnitId
    End Sub

 
    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        With ogvdetail
            If CheckOwner() = False Then Exit Sub
            If FTStateImport.Checked Then Exit Sub

            If Barcode.CheckDucumentCreateBar(FTReceiveNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
                Exit Sub
            End If

            If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDReceiveDate.Text) = True Then
                Exit Sub
            End If


            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            '  If (CheckReceive(Me.FTPoNo.Text) = False) Then Exit Sub

            Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
            Dim _Str As String = ""

            Try
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail   WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' AND  FNHSysRawMatId=" & Val(_MatID) & " "
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub
                End If

                If StockValidate.EqualizeJob(Me.FTReceiveNo.Text, Me.FTPurchaseNo.Text, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, Val(_MatID)) = False Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub
                End If

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                _Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver"
                _Str &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Str &= vbCrLf & "  AND  FNHSysRawMatId=" & Val(_MatID) & " "
                _Str &= vbCrLf & " AND ISNULL(FTStateApprove,'') ='' "
                HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_INVEN)

                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail   WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' AND  FNHSysRawMatId=" & Val(_MatID) & " ")

                ' Exit Sub
            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Exit Sub
            End Try

            Me.SaveData()
            Me.LoadRcvDetail(Me.FTReceiveNo.Text)

        End With
    End Sub

    Private Sub ocmgenbarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmgenbarcode.Click
        If CheckOwner() = False Then Exit Sub
        If FTStateImport.Checked Then Exit Sub
        If FTReceiveNo.Text.Trim <> "" Then
            If FTReceiveNo.Properties.Tag.ToString <> "" Then
                Dim _Str As String = ""

                Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)

                With _GenBarcode
                    Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)
                    .BarType = wGenerateBarcodeInven.BarCodeType.Receive
                    .ProcGen = False
                    .DocumentNo = FTReceiveNo.Text
                    .LoadGenbarcode()
                    .MainObject = Me
                    .ShowDialog()

                    If (.ProcGen) Then
                        LoadBarcode(FTReceiveNo.Text)
                    End If

                End With
            End If
        End If

    End Sub

    Public Sub LoadBarcode(Key As String)

        Me.ogcbarcode.DataSource = HI.INVEN.Barcode.GetBarcode(Key)

    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcodegen.Click

        Call DeleteBarcodeGen()
    End Sub

    Private Sub DeleteBarcodeGen()

        If CheckOwner() = False Then Exit Sub
        If FTStateImport.Checked Then Exit Sub

        With ogvbarcode

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _Str As String = ""
            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _BarCode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString
                Dim _OrderNo As String = "" & .GetRowCellValue(i, "FTOrderNo").ToString

                If Barcode.CheckTransactionOUT(_BarCode, FTReceiveNo.Text, FNHSysWHId.Properties.Tag.ToString, _OrderNo) Then
                    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text, _BarCode)
                Else
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  "
                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Continue For
                        End If

                        _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  "
                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Continue For
                        End If

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  ")

                        _StateDelete = True
                        ' Exit Sub
                    Catch ex As Exception


                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Continue For
                    End Try
                End If

            Next

            If (_StateDelete) Then
                Me.LoadBarcode(Me.FTReceiveNo.Text)
            End If

        End With
    End Sub

    Private Sub ogvbarcode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvbarcode.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete

                Call DeleteBarcodeGen()
        End Select
    End Sub

    Private Sub TabChanged()
        Try
            ocmgenbarcode.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmdeletebarcodegen.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmautotrw.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmautotrwWH.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmautoissue.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmadd.Visible = (oxtb.SelectedTabPage.Name = otpdetailitem.Name)
            ocmremove.Visible = (oxtb.SelectedTabPage.Name = otpdetailitem.Name)

            ocmmanagercvdetail.Visible = (oxtb.SelectedTabPage.Name = otpmuljob.Name)

            ocmcreatebarcodegroup.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmdeletebarcodegroup.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)

        Catch ex As Exception
        End Try
        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub oxtb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles oxtb.SelectedPageChanged
        Call TabChanged()
    End Sub

    Private Sub ocmmanagercvdetail_Click(sender As Object, e As EventArgs) Handles ocmmanagercvdetail.Click
        If CheckOwner() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDReceiveDate.Text) = True Then
            Exit Sub
        End If

        With ogvmuljob

            If Barcode.CheckDucumentCreateBar(FTReceiveNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
                Exit Sub
            End If

            If .RowCount <= 0 Then
                HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลรายการรับวัตถุดิบ กรุณาทำการตราวจสอบ !!!", 1408280001, Me.Text)
                Exit Sub
            End If

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกข้อมูลวัตถุดิบที่ต้องการทำการเกลี่ยยอดใหม่ !!!", 1408280002, Me.Text)
                Exit Sub
            End If

            Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
            Dim _FTRawMatCode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTRawMatCode").ToString
            Dim _FTMatDesc As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTMatDesc").ToString
            Dim _FTRawMatColorCode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTRawMatColorCode").ToString
            Dim _FTRawMatSizeCode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTRawMatSizeCode").ToString
            Dim _FTFabricFrontSize As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTFabricFrontSize").ToString
            Dim CFNQuantity As Double = 0
            Dim _Str As String = ""

            _Str = " SELECT        D.FNHSysRawMatId, M.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & " , M.FTRawMatNameTH AS FTMatDesc"
            Else
                _Str &= vbCrLf & " , M.FTRawMatNameEN AS FTMatDesc"
            End If

            _Str &= vbCrLf & "  , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            _Str &= vbCrLf & "  ,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
            _Str &= vbCrLf & "   D.FNDisAmt, D.FNNetPrice, D.FNQuantity, D.FNNetAmt"
            _Str &= vbCrLf & " ,PD.FTOrderNo"
            _Str &= vbCrLf & " ,CASE WHEN ISDATE(ISnull(O.FDShipDate,'')) = 1 THEN Convert(varchar(10),Convert(datetime,ISnull(O.FDShipDate,'')),103)  ELSE '' END AS FDShipDate "
            _Str &= vbCrLf & " ,ISNULL(ORD.FNUsedQuantity,0) As FNUsedQuantity"
            _Str &= vbCrLf & " ,ISNULL(PD.FNQuantity,0) AS FNPOQuantity"
            _Str &= vbCrLf & "  ,ISNULL(TRC.FNQuantity,0) AS FNTCQuantity"
            _Str &= vbCrLf & "  ,ISNULL(RTS.FNQuantity,0) AS FNRTSQuantity"
            _Str &= vbCrLf & "  ,ISNULL(RD.FNQuantity,0)   AS FNOrderRcvQuantity"
            _Str &= vbCrLf & "  ,RC.FNPricePerStock"
            _Str &= vbCrLf & "  ,RC.FNConvRatio,RC.FNHSysUnitIdStock"
            _Str &= vbCrLf & "    FROM"
            _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FTOrderNo, FNHSysRawMatId, FNQuantity"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK)"
            _Str &= vbCrLf & "   WHERE FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            _Str &= vbCrLf & " 	 AND FNHSysRawMatId=" & Integer.Parse(Val(_MatID)) & "  "
            _Str &= vbCrLf & "   ) AS PD INNER JOIN (SELECT TOP 1 H.FTReceiveNo,H.FTPurchaseNo"
            _Str &= vbCrLf & "    ,D.FNHSysRawMatId, D.FNHSysUnitId, D.FTFabricFrontSize"
            _Str &= vbCrLf & " ,D.FNPrice,D.FNDisPer,D.FNDisAmt,D.FNNetPrice"
            _Str &= vbCrLf & " ,D.FNQuantity,D.FNNetAmt,D.FNQuantityStock,D.FNHSysUnitIdStock"
            _Str &= vbCrLf & " 	,D.FNPricePerStock,D.FNConvRatio,D.FNNetStockAmt"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS H WITH(NOLOCK) "
            _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D WITH(NOLOCK) ON H.FTReceiveNo = D.FTReceiveNo"
            _Str &= vbCrLf & "  WHERE  (H.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "')	"
            _Str &= vbCrLf & " 	 AND D.FNHSysRawMatId=" & Integer.Parse(Val(_MatID)) & "  "
            _Str &= vbCrLf & " 	) AS RC ON PD.FTPurchaseNo = RC.FTPurchaseNo"

            _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS D WITH (NOLOCK)"
            _Str &= vbCrLf & "  ON PD.FNHSysRawMatId =D.FNHSysRawMatId AND RC.FTReceiveNo = D.FTReceiveNo "
            _Str &= vbCrLf & "    INNER Join"
            _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"

            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RD WITH (NOLOCK) ON RC.FTReceiveNo = RD.FTReceiveNo "
            _Str &= vbCrLf & "   AND PD.FNHSysRawMatId = RD.FNHSysRawMatId AND PD.FTOrderNo = RD.FTOrderNo "

            _Str &= vbCrLf & "  LEFT OUTER JOIN (SELECT        FTOrderNo, ISNULL"
            _Str &= vbCrLf & "   ((SELECT        MIN(FDShipDate) AS FDShipDate"
            _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Su WITH (NOLOCK)"
            _Str &= vbCrLf & "  WHERE        (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate"
            _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A ) AS O ON PD.FTOrderNo = O.FTOrderNo "

            _Str &= vbCrLf & "  LEFT OUTER JOIN    (SELECT        FTOrderNo, FNHSysRawMatId, SUM((ISNULL(FNUsedQuantity,0) + ISNULL(FNUsedPlusQuantity,0))) AS FNUsedQuantity"
            _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS O WITH(NOLOCK)"
            _Str &= vbCrLf & "   GROUP BY FTOrderNo, FNHSysRawMatId"
            _Str &= vbCrLf & "  ) AS ORD ON PD.FTOrderNo = ORD.FTOrderNo AND D.FNHSysRawMatId = ORD.FNHSysRawMatId "

            _Str &= vbCrLf & "  LEFT OUTER JOIN    ("
            _Str &= vbCrLf & "  SELECT        RH.FTPurchaseNo, RD.FTOrderNo, RD.FNHSysRawMatId, SUM(RD.FNQuantity) AS FNQuantity"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RH WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
            _Str &= vbCrLf & "   WHERE        (RH.FTReceiveNo <> N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "') "
            _Str &= vbCrLf & "  AND RH.FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            _Str &= vbCrLf & "  GROUP BY RH.FTPurchaseNo, RD.FTOrderNo, RD.FNHSysRawMatId"
            _Str &= vbCrLf & "  ) AS TRC ON PD.FTPurchaseNo = TRC.FTPurchaseNo AND PD.FTOrderNo = TRC.FTOrderNo AND D.FNHSysRawMatId = TRC.FNHSysRawMatId "
            _Str &= vbCrLf & "  LEFT OUTER JOIN    ("
            _Str &= vbCrLf & "  SELECT         RH.FTPurchaseNo, B.FTOrderNo, B.FNHSysRawMatId, SUM(RD.FNQuantity) AS FNQuantity"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier  AS RH WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS RD WITH (NOLOCK) ON RH.FTReturnSuplNo = RD.FTDocumentNo  INNER JOIN "
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON RD.FTBarcodeNo = B.FTBarcodeNo"
            _Str &= vbCrLf & "  GROUP BY RH.FTPurchaseNo, B.FTOrderNo, B.FNHSysRawMatId"
            _Str &= vbCrLf & "  ) AS RTS ON PD.FTPurchaseNo = RTS.FTPurchaseNo AND PD.FTOrderNo = RTS.FTOrderNo AND D.FNHSysRawMatId = RTS.FNHSysRawMatId "
            _Str &= vbCrLf & "  AND   D.FNHSysRawMatId=" & Integer.Parse(Val(_MatID)) & "  "
            _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode "
            _Str &= vbCrLf & " ,ISnull(O.FDShipDate,''),ISNULL(ORD.FNUsedQuantity,0)"

            Dim _dt As DataTable
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

            If _dt.Rows.Count > 0 Then
                CFNQuantity = Double.Parse(Val(_dt.Rows(0)!FNQuantity.ToString))
            End If

            HI.TL.HandlerControl.ClearControl(_Multiple)

            With _Multiple

                Call HI.ST.Lang.SP_SETxLanguage(_Multiple)
                .PurchaseNo = Me.FTPurchaseNo.Text
                .ReceiveNo = Me.FTReceiveNo.Text
                .TFNHSysRawMatId.Text = _FTRawMatCode
                .FNHSysRawMatId_None.Text = _FTMatDesc
                .TFTRawMatColorCode.Text = _FTRawMatColorCode
                .TFTRawMatSizeCode.Text = _FTRawMatSizeCode
                .TFTFabricFrontSize.Text = _FTFabricFrontSize
                .CFNQuantity.Value = CFNQuantity

                .ogcrcv.DataSource = _dt
                .ShowDialog()

                If .ProcessProc Then
                    CType(.ogcrcv.DataSource, DataTable).AcceptChanges()
                    _dt = CType(.ogcrcv.DataSource, DataTable)
                    Dim _JobRcvPOQty As Double = 0
                    Dim _JobRcvQty As Double = 0
                    Dim _SysUnitStock As Integer = 0
                    Dim _FNConvRatio As Double = 0

                    Dim _Spls As New HI.TL.SplashScreen("Saving Multiple...   Please Wait   ")

                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        For Each R As DataRow In _dt.Select(" FNOrderRcvQuantity <= 0 ")
                            _Str = "  DELETE "
                            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Integer.Parse(Val(_MatID)) & " "
                            _Str &= vbCrLf & " AND    FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        Next

                        For Each R As DataRow In _dt.Select("FNOrderRcvQuantity  > 0 ")

                            _FNConvRatio = Double.Parse(Val(R!FNConvRatio.ToString))
                            _JobRcvPOQty = Double.Parse(Val(R!FNOrderRcvQuantity.ToString))
                            _SysUnitStock = Integer.Parse(Val(R!FNHSysUnitIdStock.ToString))

                            _JobRcvQty = CDbl(Format(_JobRcvPOQty * _FNConvRatio, HI.ST.Config.QtyFormat))

                            _Str = "   SELECT   TOP 1    FTReceiveNo, FNHSysRawMatId, FNHSysUnitId"
                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Integer.Parse(Val(_MatID)) & " "
                            _Str &= vbCrLf & " AND    FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                                _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order( FTInsUser, FDInsDate, FTInsTime"
                                _Str &= vbCrLf & ",FTReceiveNo, FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt "
                                _Str &= vbCrLf & " ,FNNetPrice, FNNetAmt, FNQuantity, FNQuantityStock, FNHSysUnitIdStock, FNPricePerStock"
                                _Str &= vbCrLf & ", FNConvRatio, FNNetStockAmt,FTFabricFrontSize"
                                _Str &= vbCrLf & ",FTManualBy,FTManualDate,FTManualTime) "
                                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                                _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                                _Str &= vbCrLf & "," & CDbl(Format(Val(R!FNNetPrice.ToString) * Val(_JobRcvPOQty), HI.ST.Config.AmtFormat)) & " "
                                _Str &= vbCrLf & "," & _JobRcvPOQty & " "
                                _Str &= vbCrLf & "," & _JobRcvQty & " "
                                _Str &= vbCrLf & "," & _SysUnitStock & " "
                                _Str &= vbCrLf & "," & Val(R!FNPricePerStock.ToString) & " "
                                _Str &= vbCrLf & "," & _FNConvRatio & " "
                                _Str &= vbCrLf & "," & CDbl(Format(Val(R!FNPricePerStock.ToString) * Val(_JobRcvQty), HI.ST.Config.AmtFormat)) & " "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "

                            Else

                                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order SET "
                                _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",FTManualBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & ",FTManualDate=" & HI.UL.ULDate.FormatDateDB & " "
                                _Str &= vbCrLf & ",FTManualTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                _Str &= vbCrLf & ",FNNetAmt=" & CDbl(Format(Val(R!FNNetPrice.ToString) * Val(_JobRcvPOQty), HI.ST.Config.AmtFormat)) & " "
                                _Str &= vbCrLf & ", FNQuantity=" & _JobRcvPOQty & " "
                                _Str &= vbCrLf & ", FNQuantityStock=" & _JobRcvQty & " "
                                _Str &= vbCrLf & ", FNNetStockAmt=" & CDbl(Format(Val(R!FNPricePerStock.ToString) * Val(_JobRcvQty), HI.ST.Config.AmtFormat)) & " "
                                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Integer.Parse(Val(_MatID)) & " "
                                _Str &= vbCrLf & " AND    FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                            End If

                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                _Spls.Close()
                                Exit Sub
                            End If

                        Next

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()

                    Catch ex As Exception
                        _Spls.Close()

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Exit Sub
                    End Try


                    Me.LoadRcvDetail(Me.FTReceiveNo.Text)
                End If

            End With

            _dt.Dispose()


        End With

    End Sub

    Private Sub ocmautotrw_Click(sender As Object, e As EventArgs) Handles ocmautotrw.Click
        If CheckOwner() = False Then Exit Sub

        If Val(FNHSysWHId.Properties.Tag.ToString) <= 0 Then
            Exit Sub
        End If


        If Me.FTReceiveNo.Text <> "" Then
            Dim _Qry As String = ""
            Dim dtauto As DataTable
            _Qry = " Select Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
            _Qry &= vbCrLf & " ,M.FTBarcodeNo "
            _Qry &= vbCrLf & " ,WS.FTWHCode"
            _Qry &= vbCrLf & ",M.FTOrderNo"
            _Qry &= vbCrLf & " ,M.FNQuantity"

            _Qry &= vbCrLf & "  , IM.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
            Else
                _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
            End If

            _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            _Qry &= vbCrLf & ",M.FTFabricFrontSize"
            _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
            _Qry &= vbCrLf & "   ,FNHSysWHId"
            _Qry &= vbCrLf & "   ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FNQuantity"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND ISNULL(FTIssueReferNo,'')='' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND FTStateIssue='0' "
            _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"
            _Qry &= vbCrLf & "   ,FTPurchaseNo"
            _Qry &= vbCrLf & "   ,FTDocumentNo"
            _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  ,B.FTPurchaseNo,B.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & "   ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK)"
            _Qry &= vbCrLf & "   WHERE B.FTOrderNo = C.FTOrderNo"
            _Qry &= vbCrLf & "  AND B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            _Qry &= vbCrLf & "  AND C.FNOrderType =4"
            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
            _Qry &= vbCrLf & " ORDER BY  M.FTBarcodeNo"
            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then
                HI.TL.HandlerControl.ClearControl(_AutoTransferToCenter)
                With _AutoTransferToCenter
                    Call HI.ST.Lang.SP_SETxLanguage(_AutoTransferToCenter)

                    .ReceiveNo = Me.FTReceiveNo.Text
                    .WHID = Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString))
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                    Call LoadBarcode(Me.FTReceiveNo.Text)

                End With
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ โอนได้ กรุณาทำการตรวจสอบ !!!", 1409030001, Me.Text, , MessageBoxIcon.Warning)
            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
        End If
    End Sub

    Private Sub ocmautotrwWH_Click(sender As Object, e As EventArgs) Handles ocmautotrwWH.Click
        If CheckOwner() = False Then Exit Sub

        If Val(FNHSysWHId.Properties.Tag.ToString) <= 0 Then
            Exit Sub
        End If

        If Me.FTReceiveNo.Text <> "" Then
            Dim _Qry As String = ""
            Dim dtauto As DataTable
            Dim pofacno As String

            _Qry = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'"
            pofacno = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "")

            _Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
            _Qry &= vbCrLf & " ,M.FTBarcodeNo "
            _Qry &= vbCrLf & " ,WS.FTWHCode"
            _Qry &= vbCrLf & ",M.FTOrderNo"
            _Qry &= vbCrLf & " ,((M.FNQuantity - ISNULL(M.FNTransactionQty,0) )) FNQuantity,((M.FNQuantity - ISNULL(M.FNTransactionQty,0) ))  AS FNQuantityOrg "

            _Qry &= vbCrLf & "  , IM.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
            Else
                _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
            End If

            _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            _Qry &= vbCrLf & ",M.FTFabricFrontSize"
            _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FNOrderType,M.FTOrderNoRef,M.FNHSysCmpIdToRef,M.FNHSysCmpIdTo AS FNHSysCmpIdToOrg "
            _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN M.FNHSysCmpIdToRef ELSE M.FNHSysCmpIdTo   END AS  FNHSysCmpIdTo  "
            _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN CmpToRef.FTCmpCode ELSE CmpTo.FTCmpCode   END AS  FTCmpCode  "
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
            _Qry &= vbCrLf & "   ,FNHSysWHId"
            _Qry &= vbCrLf & "   ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FNQuantity"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND ISNULL(FTIssueReferNo,'')='' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND FTStateIssue='0' "
            _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"

            _Qry &= vbCrLf & "   ,FTPurchaseNo"
            _Qry &= vbCrLf & "   ,FTDocumentNo"
            _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNHSysCmpIdTo,FNOrderType,FTOrderNoRef,FNHSysCmpIdToRef"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  ,B.FTPurchaseNo,B.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId AS FNHSysCmpIdTo,C.FNOrderType,ISNULL(B.FTOrderNoRef,'') AS FTOrderNoRef,ISNULL(CRef.FNHSysCmpId,0) AS FNHSysCmpIdToRef "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON  B.FTOrderNo = C.FTOrderNo "
            _Qry &= vbCrLf & "    LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS CREF WITH(NOLOCK) ON  B.FTOrderNoRef = CREF.FTOrderNo "
            _Qry &= vbCrLf & "   WHERE  B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            _Qry &= vbCrLf & "          AND C.FNOrderType IN (0,2,3,4,8,9,13,17,19,22) "
            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType,ISNULL(B.FTOrderNoRef,''),ISNULL(CRef.FNHSysCmpId,0) "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpToRef WITH (NOLOCK) ON M.FNHSysCmpIdToRef = CmpToRef.FNHSysCmpId"
            ' _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"

            _Qry &= vbCrLf & "    WHERE (M.FNQuantity - ISNULL(M.FNTransactionQty,0) ) > 0"

            _Qry &= vbCrLf & "  ORDER BY  M.FTBarcodeNo"

            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then

                HI.TL.HandlerControl.ClearControl(_AutoTransferToWH)

                With _AutoTransferToWH

                    Call HI.ST.Lang.SP_SETxLanguage(_AutoTransferToWH)

                    .ReceiveNo = Me.FTReceiveNo.Text
                    .WHID = Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString))
                    .POFactoryNo = pofacno
                    .PONo = FTPurchaseNo.Text.Trim()
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                    Call LoadBarcode(Me.FTReceiveNo.Text)

                End With

            Else

                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ โอนได้ กรุณาทำการตรวจสอบ !!!", 1410020009, Me.Text, , MessageBoxIcon.Warning)

            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadDataInfo(Me.FTReceiveNo.Text)
    End Sub

    Private Sub ogcbarcode_Click(sender As Object, e As EventArgs) Handles ogcbarcode.Click
    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click
    End Sub

    Private Sub FDReceiveDate_EditValueChanged(sender As Object, e As EventArgs) Handles FDReceiveDate.EditValueChanged
    End Sub

    Private Sub FDReceiveDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FDReceiveDate.EditValueChanging
        Try
            If FDReceiveDate.Properties.ReadOnly = False Then

                If HI.UL.ULDate.ConvertEnDB(e.NewValue.ToString) > HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) Then

                    e.Cancel = True

                End If

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub SetDefaultWH(pokey As String)

        If (FNHSysWHId.Text.Trim() <> "") Then
            Exit Sub
        End If

        Dim cmd As String = ""
        Dim dt As DataTable
        Dim SysCmpId As Integer = 0
        Dim SysCmpIdTo As Integer = 0
        Dim MatType As Integer = 0

        cmd = "select X.FNHSysCmpId,X.FNHSysCmpIdTo "
        cmd &= vbCrLf & " ,ISNULL(("
        cmd &= vbCrLf & " SELECT TOP 1  MM.FNMerMatType "
        cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD INNER JOIN"
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM ON PD.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM On IM.FTRawMatCode = MM.FTMainMatCode"
        cmd &= vbCrLf & " WHERE  (PD.FTPurchaseNo =N'" & HI.UL.ULF.rpQuoted(pokey) & "') "
        cmd &= vbCrLf & "),0) As FNMerMatType "
        cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery As X With(nolock)"
        cmd &= vbCrLf & " inner join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As P With(NOLOCK) On X.FNHSysDeliveryId = P.FNHSysDeliveryId "
        cmd &= vbCrLf & " WHERE P.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(pokey) & "'"
        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        For Each R As DataRow In dt.Rows

            SysCmpId = Val(R!FNHSysCmpId.ToString)
            SysCmpIdTo = Val(R!FNHSysCmpIdTo.ToString)
            MatType = Val(R!FNMerMatType.ToString)

            Exit For
        Next

        If SysCmpId > 0 Then

            cmd = "SELECT TOP 1 FTWHCode  "
            cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS X WITH(NOLOCK) "
            cmd &= vbCrLf & " WHERE X.FNHSysCmpId =" & SysCmpId & " AND ISNULL(FTStateDefaultReceive,'') ='1'"
            cmd &= vbCrLf & " AND FNMerMatType=" & MatType & ""

            dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

            If dt.Rows.Count > 0 Then

                For Each R As DataRow In dt.Rows

                    FNHSysWHId.Focus()
                    FNHSysWHId.Text = R!FTWHCode.ToString
                    FTPurchaseNo.Focus()

                    Exit For

                Next

            End If

        End If

        dt.Dispose()

    End Sub

    Private Sub oxtb_Click(sender As Object, e As EventArgs) Handles oxtb.Click

    End Sub

    Private Sub ocmcreatebarcodegroup_Click(sender As Object, e As EventArgs) Handles ocmcreatebarcodegroup.Click
        If CheckOwner() = False Then Exit Sub

        If Val(FNHSysWHId.Properties.Tag.ToString) <= 0 Then
            Exit Sub
        End If

        If Me.FTReceiveNo.Text <> "" Then
            Dim _Qry As String = ""
            Dim dtauto As DataTable
            Dim pofacno As String = ""

            _Qry = " Select '0' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
            _Qry &= vbCrLf & " ,M.FTBarcodeNo "
            _Qry &= vbCrLf & " ,WS.FTWHCode"
            _Qry &= vbCrLf & ",M.FTOrderNo"
            _Qry &= vbCrLf & " ,M.FNQuantity"

            _Qry &= vbCrLf & "  , IM.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
            Else
                _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
            End If

            _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            _Qry &= vbCrLf & ",M.FTFabricFrontSize"
            _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FNOrderType,M.FTOrderNoRef,M.FNHSysCmpIdToRef,M.FNHSysCmpIdTo AS FNHSysCmpIdToOrg "
            _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN M.FNHSysCmpIdToRef ELSE M.FNHSysCmpIdTo   END AS  FNHSysCmpIdTo  "
            _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN CmpToRef.FTCmpCode ELSE CmpTo.FTCmpCode   END AS  FTCmpCode  "
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
            _Qry &= vbCrLf & "   ,FNHSysWHId"
            _Qry &= vbCrLf & "   ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FNQuantity"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo"
            _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"
            _Qry &= vbCrLf & "   ,FTPurchaseNo"
            _Qry &= vbCrLf & "   ,FTDocumentNo"
            _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNHSysCmpIdTo,FNOrderType,FTOrderNoRef,FNHSysCmpIdToRef"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  ,B.FTPurchaseNo,B.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId AS FNHSysCmpIdTo,C.FNOrderType,ISNULL(B.FTOrderNoRef,'') AS FTOrderNoRef,ISNULL(CRef.FNHSysCmpId,0) AS FNHSysCmpIdToRef "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON  B.FTOrderNo = C.FTOrderNo "
            _Qry &= vbCrLf & "    LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS CREF WITH(NOLOCK) ON  B.FTOrderNoRef = CREF.FTOrderNo "
            _Qry &= vbCrLf & "   WHERE  B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' AND ISNULL(B.FTBarcodeGrpNo,'') ='' "

            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType,ISNULL(B.FTOrderNoRef,''),ISNULL(CRef.FNHSysCmpId,0) "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpToRef WITH (NOLOCK) ON M.FNHSysCmpIdToRef = CmpToRef.FNHSysCmpId"
            _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
            _Qry &= vbCrLf & "  ORDER BY  M.FTBarcodeNo"

            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then

                HI.TL.HandlerControl.ClearControl(_AutoGenerateBarcodeGrp)

                With _AutoGenerateBarcodeGrp

                    Call HI.ST.Lang.SP_SETxLanguage(_AutoGenerateBarcodeGrp)
                    .MainObject = Me
                    .ReceiveNo = Me.FTReceiveNo.Text
                    .WHID = Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString))
                    .POFactoryNo = pofacno
                    .PONo = FTPurchaseNo.Text.Trim()
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                    Call LoadBarcode(Me.FTReceiveNo.Text)

                End With

            Else

                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ ที่สามารถทำการสร้าง Barcode Group ได้ กรุณาทำการตรวจสอบ !!!", 1410028809, Me.Text, , MessageBoxIcon.Warning)

            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
        End If
    End Sub

    Private Sub ocmautoissue_Click(sender As Object, e As EventArgs) Handles ocmautoissue.Click
        If CheckOwner() = False Then Exit Sub

        If Val(FNHSysWHId.Properties.Tag.ToString) <= 0 Then
            Exit Sub
        End If

        If Me.FTReceiveNo.Text <> "" Then
            Dim _Qry As String = ""
            Dim dtauto As DataTable
            Dim pofacno As String

            _Qry = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'"
            pofacno = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "")

            _Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
            _Qry &= vbCrLf & " ,M.FTBarcodeNo "
            _Qry &= vbCrLf & " ,WS.FTWHCode"
            _Qry &= vbCrLf & ",M.FTOrderNo"
            _Qry &= vbCrLf & " ,((M.FNQuantity - ISNULL(M.FNTransactionQty,0) )) FNQuantity,((M.FNQuantity - ISNULL(M.FNTransactionQty,0) ))  AS FNQuantityOrg "

            _Qry &= vbCrLf & "  , IM.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
            Else
                _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
            End If

            _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            _Qry &= vbCrLf & ",M.FTFabricFrontSize"
            _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FNOrderType,M.FTOrderNoRef,M.FNHSysCmpIdToRef,M.FNHSysCmpIdTo AS FNHSysCmpIdToOrg "
            _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN M.FNHSysCmpIdToRef ELSE M.FNHSysCmpIdTo   END AS  FNHSysCmpIdTo  "
            _Qry &= vbCrLf & ",CASE WHEN M.FNHSysCmpIdToRef > 0 AND M.FNOrderType = 4  THEN CmpToRef.FTCmpCode ELSE CmpTo.FTCmpCode   END AS  FTCmpCode  "
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
            _Qry &= vbCrLf & "   ,FNHSysWHId"
            _Qry &= vbCrLf & "   ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FNQuantity"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND ISNULL(FTIssueReferNo,'')='' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPackingList_PLBarcodeRefBarcodeRM WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND FTStateIssue='0' "
            _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"
            _Qry &= vbCrLf & "   ,FTPurchaseNo"
            _Qry &= vbCrLf & "   ,FTDocumentNo"
            _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNHSysCmpIdTo,FNOrderType,FTOrderNoRef,FNHSysCmpIdToRef"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  ,B.FTPurchaseNo,B.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId AS FNHSysCmpIdTo,C.FNOrderType,ISNULL(B.FTOrderNoRef,'') AS FTOrderNoRef,ISNULL(CRef.FNHSysCmpId,0) AS FNHSysCmpIdToRef "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK) ON  B.FTOrderNo = C.FTOrderNo "
            _Qry &= vbCrLf & "    LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS CREF WITH(NOLOCK) ON  B.FTOrderNoRef = CREF.FTOrderNo "
            _Qry &= vbCrLf & "   WHERE  B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            _Qry &= vbCrLf & "          AND C.FNOrderType IN (0,2,3,4,8,9,13,17,19,22) "
            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType,ISNULL(B.FTOrderNoRef,''),ISNULL(CRef.FNHSysCmpId,0) "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
            _Qry &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpToRef WITH (NOLOCK) ON M.FNHSysCmpIdToRef = CmpToRef.FNHSysCmpId"
            ' _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"


            _Qry &= vbCrLf & "    WHERE (M.FNQuantity - ISNULL(M.FNTransactionQty,0) ) > 0"

            _Qry &= vbCrLf & "  ORDER BY  M.FTBarcodeNo"

            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then

                HI.TL.HandlerControl.ClearControl(_AutoIssue)

                With _AutoIssue

                    Call HI.ST.Lang.SP_SETxLanguage(_AutoIssue)

                    .ReceiveNo = Me.FTReceiveNo.Text
                    .WHID = Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString))
                    .POFactoryNo = pofacno
                    .PONo = FTPurchaseNo.Text.Trim()
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                    Call LoadBarcode(Me.FTReceiveNo.Text)

                End With

            Else

                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ โอนได้ กรุณาทำการตรวจสอบ !!!", 1410020009, Me.Text, , MessageBoxIcon.Warning)

            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
        End If
    End Sub

    Private Sub FNListDocumentData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentData.SelectedIndex.ToString)
        End If
    End Sub
End Class