Imports System.Windows.Forms

Public Class wConversion

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _GenBarcode As wGenerateBarcodeInven
    Private _ConAddItem As wConversionAddItem
    Private _AutoTransferToWH As wReceiveAutoTransferToWH
    Private _AutoIssue As wAutoIssue
    Private _ProcLoad As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        Dim oSysLang As New ST.SysLanguage
        _GenBarcode = New wGenerateBarcodeInven

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenBarcode.Name.ToString.Trim, _GenBarcode)
        Catch ex As Exception
        Finally
        End Try

        _ConAddItem = New wConversionAddItem
        HI.TL.HandlerControl.AddHandlerObj(_ConAddItem)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ConAddItem.Name.ToString.Trim, _ConAddItem)
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

        _AutoIssue = New wAutoIssue
        HI.TL.HandlerControl.AddHandlerObj(_AutoIssue)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoIssue.Name.ToString.Trim, _AutoIssue)
        Catch ex As Exception
        Finally
        End Try

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

        Call InitGridFinish()
        Call InitGridBarcode()

    End Sub

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

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _mPriceClosed1 As Double = -1
    Public Property PriceClosed1 As Double
        Get
            Return _mPriceClosed1
        End Get
        Set(value As Double)
            _mPriceClosed1 = value
        End Set
    End Property


    Private _mPriceClosed2 As Double = -1
    Public Property PriceClosed2 As Double
        Get
            Return _mPriceClosed2
        End Get
        Set(value As Double)
            _mPriceClosed2 = value
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

#Region "Initial Grid"

    Private Sub InitGridFinish()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity"


        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvfinishrawmat
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


    Private Sub InitGridBarcode()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity"


        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvbarcode
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


        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

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

        Call LoadConversionDetail(Key.ToString)
        Call LoadBarcode(Key.ToString)
        Call LoadFinishRawmatDetail(Key.ToString)

        Me.otbdetail.SelectedTabPageIndex = 0

        _ProcLoad = False
    End Sub

    Private Sub LoadConversionDetail(PoKey As String)
        ogcdetail.DataSource = HI.INVEN.Barcode.LoadDocumentBarcode(PoKey, Barcode.DocType.Conversion)


    End Sub

    Public Sub LoadBarcode(Key As String)
        Me.ogcbarcode.DataSource = HI.INVEN.Barcode.GetBarcode(Key)
    End Sub

    Public Sub LoadFinishRawmatDetail(Key As String)
        Dim _Str As String = ""
        Dim _dt As DataTable

        _Str = "  SELECT    ISNULL(W.FTWHCode,'') AS FTWHCode"
        _Str &= vbCrLf & ",ROW_NUMBER() OVER(ORDER BY B.FNSeq) AS FNRowSeq "
        _Str &= vbCrLf & ",CASE WHEN  ISDATE(B.FDInsDate) = 1 THEN Convert(nvarchar(10), Convert(datetime,B.FDInsDate) ,103)  ELSE '' END AS FDInsDate"
        _Str &= vbCrLf & ", B.FTOrderNo"
        _Str &= vbCrLf & ",B.FNHSysRawMatId"
        _Str &= vbCrLf & ", IM.FTRawMatCode"
        _Str &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & ", U.FTUnitCode"
        _Str &= vbCrLf & ", B.FTPurchaseNo"
        _Str &= vbCrLf & ", B.FNQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameTH,'') AS  FTRawMatName"
        Else
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameEN,'') AS  FTRawMatName "
        End If

        _Str &= vbCrLf & ",ISNULL(B.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Str &= vbCrLf & ",A.FNHSysWHId,B.FNSeq,B.FNPrice "
        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion AS A WITH(NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish AS B WITH (NOLOCK) ON A.FTConversionNo = B.FTConversionNo INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN "
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH (NOLOCK) ON A.FNHSysWHId = W.FNHSysWHId"
        _Str &= vbCrLf & " WHERE A.FTConversionNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
        _Str &= vbCrLf & " ORDER BY B.FNSeq"

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        Me.ogcfinishrawmat.DataSource = _dt.Copy
        Me.ogcfinishrawmat.Refresh()

    End Sub

    Private Sub LoadResourceFinishRawmatDetail(Key As String, SeqIndex As Integer)
        Dim _Str As String = ""
        Dim _dt As DataTable

        _Str = "  SELECT    ISNULL(W.FTWHCode,'') AS FTWHCode"
        _Str &= vbCrLf & ",ROW_NUMBER() OVER(ORDER BY B.FNSeq) AS FNRowSeq "
        _Str &= vbCrLf & ", B.FTOrderNo"
        _Str &= vbCrLf & ",B.FNHSysRawMatId"
        _Str &= vbCrLf & ", IM.FTRawMatCode"
        _Str &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
        _Str &= vbCrLf & ", U.FTUnitCode"
        _Str &= vbCrLf & ", B.FTPurchaseNo"
        _Str &= vbCrLf & ", B.FNQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameTH,'') AS  FTRawMatName"
        Else
            _Str &= vbCrLf & ",ISNULL(IM.FTRawMatNameEN,'') AS  FTRawMatName "
        End If

        _Str &= vbCrLf & ",ISNULL(B.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Str &= vbCrLf & ",A.FNHSysWHId,B.FNSeq,B.FNPrice,B.FNWage,B.FNWageCost "
        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion AS A WITH(NOLOCK)"
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail AS B WITH (NOLOCK) ON A.FTConversionNo = B.FTConversionNo INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN "
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH (NOLOCK) ON A.FNHSysWHId = W.FNHSysWHId"
        _Str &= vbCrLf & " WHERE A.FTConversionNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
        _Str &= vbCrLf & " AND B.FNSeq=" & SeqIndex & " "
        _Str &= vbCrLf & "  ORDER BY "
        _Str &= vbCrLf & " IM.FTRawMatCode"
        _Str &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') "
        _Str &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        Me.ogcresourcerawmat.DataSource = _dt.Copy
        Me.ogcresourcerawmat.Refresh()

    End Sub

    Public Sub DefaultsData()
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
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

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

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    _Key = .Text

                                    _Str = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "='" & HI.UL.ULF.rpQuoted(_Key) & "' "
                                    Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString

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
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion WHERE FTConversionNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion WHERE FTConversionNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "'")

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
    End Sub

    Private Sub TabChange()
        ocmsave.Visible = (otbdetail.SelectedTabPage.Name = otpresource.Name)
        ocmdelete.Visible = (otbdetail.SelectedTabPage.Name = otpresource.Name)
        ocmdeletebarcode.Visible = (otbdetail.SelectedTabPage.Name = otpresource.Name)

        ocmadd.Visible = (otbdetail.SelectedTabPage.Name = otpconversion.Name)
        ocmremove.Visible = (otbdetail.SelectedTabPage.Name = otpconversion.Name)

        ocmgenbarcode.Visible = (otbdetail.SelectedTabPage.Name = otpbarcode.Name)
        ocmdeletebarcodegen.Visible = (otbdetail.SelectedTabPage.Name = otpbarcode.Name)
        ocmautotrwWH.Visible = (otbdetail.SelectedTabPage.Name = otpbarcode.Name)
        ocmautoissue.Visible = (otbdetail.SelectedTabPage.Name = otpbarcode.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

#End Region

#Region "MAIN PROC"
    Private Function CheckConversion(Optional _FNHSysRawMatId As Integer = 0) As Boolean
        Dim _Qry As String = ""

        _Qry = "SELECT TOP 1  FTConversionNo "
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE FTConversionNo='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text.Trim) & "'"
        If _FNHSysRawMatId > 0 Then
            _Qry &= vbCrLf & "   AND FNHSysRawMatId=" & _FNHSysRawMatId & ""
        End If

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1509240911, "พบข้อมูลการแปรสภาพแล้วไม่สามารถทำการลบหรือแก้ไขได้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

    End Function

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTConversionBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTConversionBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If CheckOwner() = False Then Exit Sub
        If CheckConversion() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDConversionDate.Text) = True Then
            Exit Sub
        End If


        If Barcode.CheckDucumentCreateBar(FTConversionNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If

        If Me.VerrifyData Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub
        If CheckConversion() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDConversionDate.Text) = True Then
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTConversionNo.Text, Me.Text) = False Then
            Exit Sub
        End If


        If Barcode.CheckDucumentCreateBar(FTConversionNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If

        If Me.DeleteData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()

        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        Me.otbdetail.SelectedTabPageIndex = 0
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTConversionNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "ConversionNoSlip.rpt"
                .Formular = "{TINVENConversion.FTConversionNo}='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "ConversionNoSlip_Barcode.rpt"
                .Formular = "{TINVENConversion.FTConversionNo}='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTConversionNo_lbl.Text)
            FTConversionNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Function SaveBarcode() As Boolean
        Dim _Str As String
        Dim _BarCode As String = FTBarcodeNo.Text
        Dim _StateNew As Boolean
        Try

            _Str = " SELECT TOP 1 FTBarcodeNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK) WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "' "
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' "
                _Str &= vbCrLf & "," & Val("" & FNHSysWHId.Properties.Tag.ToString) & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
                _Str &= vbCrLf & "," & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                _Str &= vbCrLf & ",CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "

            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & FNHSysWHId.Properties.Tag.ToString) & " "
                _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
                _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",FTStateReserve='' "
                _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
                _Str &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' "
                _Str &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & " AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'"


                If _Str = "" Then

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT "
                    _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ",FNHSysWHId=" & Val("" & FNHSysWHId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & ",FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
                    _Str &= vbCrLf & ",FTStateReserve='' "
                    _Str &= vbCrLf & ",FNPriceTrans=CASE WHEN " & Me.FNPriceTrans & "<0 THEN NULL ELSE " & Me.FNPriceTrans & "  END "
                    _Str &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' "
                    _Str &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                    _Str &= vbCrLf & " AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.DocRefNo) & "'"

                End If

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

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

    Private Function DeleteBarcode(BarcodeKey As String) As Boolean
        Dim _Str As String
        Dim _BarCode As String = BarcodeKey

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTConversionNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Str)

            Return True
        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

#End Region

    Private Sub FNIssueBarType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNIssueBarType.SelectedIndexChanged
        FNQuantityBal.Properties.ReadOnly = (FNIssueBarType.SelectedIndex <> 1)

        If FTConversionNo.Text <> "" Then
            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
        End If

    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        If ocmsave.Enabled = False Then Exit Sub
        If CheckOwner() = False Then Exit Sub
        If CheckConversion() = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTConversionNo.Properties.Tag.ToString = "" Then
                        Call SetLoadDefaultByBarCode(FTBarcodeNo.Text)
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTConversionNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTConversionNo.Text)
                    End If

                    Call AddBarCode(FTBarcodeNo.Text, (FNQuantityBal.Properties.ReadOnly))

                End If
        End Select
    End Sub


    Private Sub AddBarCode(BarcodeNo As String, StateAdd As Boolean, Optional Qty As Double = 0)

        If CheckOwner() = False Then Exit Sub
        If CheckConversion() = False Then Exit Sub

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDConversionDate.Text) = True Then
            Exit Sub
        End If

        FNQuantityBal.Value = 0
        Dim _Dt As DataTable = Barcode.BarCodeBalance(FTBarcodeNo.Text, 0.ToString, "", Me.FTConversionNo.Text, True)
        If _Dt.Rows.Count > 0 Then

            'Dim _RawmatId As Integer = 0
            '_RawmatId = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A WITH(NOLOCK) WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'", Conn.DB.DataBaseName.DB_INVEN, "0")))
            'If HI.INVEN.StockValidate.OrderUsedRawmat(Me.FTOrderNo.Text, _RawmatId) = False Then
            '    Exit Sub
            'End If

            If _Dt.Select("FNQuantityBal >=" & Qty & " AND FNQuantityBal >0 ").Length > 0 Then
                If _Dt.Select("FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >=" & Qty & " AND FNQuantityBal >0 ").Length > 0 Then
                    If _Dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >=" & Qty & "  ").Length > 0 Then

                        'If Barcode.CheckTransactionIN(BarcodeNo, FTConversionNo.Text, FNHSysWHId.Properties.Tag.ToString, _OrderNo) Then
                        '    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312150001, Me.Text)
                        '    Exit Sub
                        'End If

                        Me.DocRefNo = ""
                        For Each R As DataRow In _Dt.Select("FNQuantityBal >=" & Qty & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & " AND FNQuantityBal >0 ")
                            If Qty <> 0 Then
                                FNQuantityBal.Value = Qty
                            Else
                                FNQuantityBal.Value = R!FNQuantityBal
                            End If

                            Me.FNPriceTrans = Val(R!FNPriceTrans)

                            Me.DocRefNo = R!FTDocumentNo.ToString
                            Exit For
                        Next

                        If (StateAdd) Then

                            If SaveBarcode() Then

                                LoadConversionDetail(Me.FTConversionNo.Text)
                                FTBarcodeNo.Focus()
                                FTBarcodeNo.SelectAll()
                                FNQuantityBal.Value = 0

                            End If
                        Else

                            If FNQuantityBal.Properties.ReadOnly = False Then
                                FNQuantityBal.Focus()
                                FNQuantityBal.SelectAll()
                            End If

                        End If
                        Me.DocRefNo = ""
                    Else
                        HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ Order นี้  !!!", 1311240009, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("Barcode ไม่ใช่ของ คลังนี้  !!!", 1311240008, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("จำนวน Balance ไม่พอ !!!", 1311240010, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("ไม่พบข้อมูลหมายเลข Barcode !!!", 1311240007, Me.Text)
        End If
        _Dt.Dispose()
    End Sub


    Private Sub SetLoadDefaultByBarCode(_Barcode As String)
        If Me.FNHSysWHId.Text <> "" And FTOrderNo.Text <> "" Then
            Exit Sub
        End If

        Dim _Dt As DataTable = Barcode.BarCodeBalance(_Barcode, 0.ToString, "", Me.FTConversionNo.Text, True)

        If _Dt.Rows.Count > 0 Then

            If _Dt.Select(" FNQuantityBal >0 ").Length > 0 Then

                For Each R As DataRow In _Dt.Select(" FNQuantityBal >0 ")

                    If Me.FNHSysWHId.Text = "" Then
                        HI.TL.HandlerControl.DynamicButtoneditSysKey_Leave(Me.FNHSysWHId, R!FNHSysWHId.ToString)
                    End If

                    If FTOrderNo.Text = "" Then
                        FTOrderNo.Text = R!FTOrderNo
                    End If

                    Exit For

                Next

            End If

        End If

        _Dt.Dispose()

    End Sub
    Private Sub FNQuantityBal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNQuantityBal.KeyDown
        If CheckOwner() = False Then Exit Sub
        If CheckConversion() = False Then Exit Sub

        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTConversionNo.Properties.Tag.ToString = "" Then

                        Call SetLoadDefaultByBarCode(FTBarcodeNo.Text)

                        If Me.VerrifyData() Then

                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If

                        Else
                            Exit Sub
                        End If

                    Else
                        If Me.FTConversionNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTConversionNo.Text)
                    End If

                    Call AddBarCode(FTBarcodeNo.Text, True, FNQuantityBal.Value)

                Else
                    FTBarcodeNo.Focus()
                    FTBarcodeNo.SelectAll()
                End If

        End Select
    End Sub


    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        If ocmdeletebarcode.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Delete
                If CheckOwner() = False Then Exit Sub
                With ogvdetail
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _Barcode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo").ToString

                    If Barcode.CheckTransactionIN(_Barcode, FTConversionNo.Text, FNHSysWHId.Properties.Tag.ToString, Me.FTOrderNo.Text) Then
                        HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน คืน Stock แล้วไม่สามารถทำการลบ หรือแก้ไขได้ !!!", 1312150001, Me.Text)
                        Exit Sub
                    End If

                    If _Barcode <> "" Then
                        If DeleteBarcode(_Barcode) Then
                            FTBarcodeNo.Focus()
                            FTBarcodeNo.SelectAll()
                            FNQuantityBal.Value = 0

                            LoadConversionDetail(Me.FTConversionNo.Text)
                        End If
                    End If

                End With
        End Select
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged

        Try

            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FTOrderNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTOrderNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNHSysWHId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcode.Click
        Call DeleteBarcode()
    End Sub

    Private Sub DeleteBarcode()

        If CheckOwner() = False Then Exit Sub


        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDConversionDate.Text) = True Then
            Exit Sub
        End If

        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString
                Dim _FNHSysRawMatId As Integer = Integer.Parse(Val("" & .GetRowCellValue(i, "FNHSysRawMatId").ToString))

                If _Barcode <> "" Then
                    If CheckConversion(_FNHSysRawMatId) = True Then
                        If DeleteBarcode(_Barcode) Then
                            _StateDelete = True
                        End If
                    End If          
                End If
          
            Next

            If _StateDelete Then
                FTBarcodeNo.Focus()
                FTBarcodeNo.SelectAll()
                FNQuantityBal.Value = 0

                LoadConversionDetail(Me.FTConversionNo.Text)
            End If

        End With
    End Sub

    Private Sub wIssue_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Call TabChange()
    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub FNQuantityBal_EditValueChanged(sender As Object, e As EventArgs) Handles FNQuantityBal.EditValueChanged

    End Sub

    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub

        Dim _FTCNSDate As String = HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))

        'If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDConversionDate.Text) = True Then
        '    Exit Sub
        'End If

        If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), _FTCNSDate) = True Then
            Exit Sub
        End If

        If Me.FTConversionNo.Text.Trim <> "" Then
            HI.TL.HandlerControl.ClearControl(_ConAddItem)

            With _ConAddItem
                Call HI.ST.Lang.SP_SETxLanguage(_ConAddItem)

                .ocmadd.Enabled = True
                .ocmcancel.Enabled = True
                .DocumentNo = Me.FTConversionNo.Text
                .OrderNo = Me.FTOrderNo.Text.Trim
                .MainObject = Me

               
              
                If .LoadData Then
                    .ClearDetail()
                    .ShowDialog()
                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการที่สามารถทำการแปรสภาพได้ กรุณาทำการตรวจสอบ !!!", 1509267013, Me.Text, , MessageBoxIcon.Warning)
                End If

            End With
        End If

    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
       
        Try
            With ogvfinishrawmat
                If CheckOwner() = False Then Exit Sub

                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                Dim _StrDate As String = "" & .GetFocusedRowCellValue("FDInsDate").ToString
                If _StrDate = "" Then Exit Sub

                If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), _StrDate) = True Then
                    Exit Sub
                End If

                Dim _MatIDSeq As Integer = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString))
                If Barcode.CheckDucumentCreateBar(FTConversionNo.Text, 0, _MatIDSeq) Then
                    HI.MG.ShowMsg.mInvalidData("ข้อมูลนี้ได้สร้าง Barcode แล้วไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1509287705, Me.Text)
                    Exit Sub
                End If

                Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
                Dim _Str As String = ""

                Try
                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail "
                    _Str &= vbCrLf & "  WHERE FTConversionNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "'"
                    _Str &= vbCrLf & "   AND  FNSeq=" & Val(_MatIDSeq) & " "
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        HI.MG.ShowMsg.mInvalidData("ไม่สามารถทำการลบข้อมูลได้เนื่องจากเกิดข้อผิดพลาดบางประการกรูณาทำการติดต่อผู้ดูแลระบบ !!!", 1509287706, Me.Text)
                        Exit Sub
                    End If

                    _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENConversion_Detail_Finish "
                    _Str &= vbCrLf & "  WHERE FTConversionNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "'"
                    _Str &= vbCrLf & "   AND  FNSeq=" & Val(_MatIDSeq) & " "
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        HI.MG.ShowMsg.mInvalidData("ไม่สามารถทำการลบข้อมูลได้เนื่องจากเกิดข้อผิดพลาดบางประการกรูณาทำการติดต่อผู้ดูแลระบบ !!!", 1509287706, Me.Text)
                        Exit Sub
                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    HI.MG.ShowMsg.mInvalidData("ไม่สามารถทำการลบข้อมูลได้เนื่องจากเกิดข้อผิดพลาดบางประการกรูณาทำการติดต่อผู้ดูแลระบบ !!!", 1509287706, Me.Text)
                    Exit Sub
                End Try

                Me.LoadFinishRawmatDetail(Me.FTConversionNo.Text)

            End With
        Catch ex As Exception

        End Try
     

    End Sub

    Private Sub ocmgenbarcode_Click(sender As Object, e As EventArgs) Handles ocmgenbarcode.Click
        If CheckOwner() = False Then Exit Sub
        If FTConversionNo.Text.Trim <> "" Then
            If FTConversionNo.Properties.Tag.ToString <> "" Then
                Dim _Str As String = ""

                Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)

                With _GenBarcode
                    Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)
                    .BarType = wGenerateBarcodeInven.BarCodeType.Conversion
                    .ProcGen = False
                    .DocumentNo = FTConversionNo.Text
                    .LoadGenbarcode()
                    .MainObject = Me
                    .ShowDialog()

                    If (.ProcGen) Then
                        LoadBarcode(FTConversionNo.Text)
                    End If

                End With
            End If
        End If
    End Sub

    Private Sub ocmdeletebarcodegen_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcodegen.Click

        Call DeleteBarcodeGen()

    End Sub

    Private Sub DeleteBarcodeGen()
        If CheckOwner() = False Then Exit Sub
        With ogvbarcode

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()

                Dim _BarCode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString
                Dim _OrderNo As String = "" & .GetRowCellValue(i, "FTOrderNo").ToString

                If Barcode.CheckTransactionOUT(_BarCode, FTConversionNo.Text, FNHSysWHId.Properties.Tag.ToString, _OrderNo) Then
                    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text, _BarCode)
                Else

                    Dim _Str As String = ""

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

            If _StateDelete Then
                Me.LoadBarcode(Me.FTConversionNo.Text)
            End If


        End With
    End Sub


    Private Sub ogvfinishrawmat_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvfinishrawmat.FocusedRowChanged

        Try

            Dim _Seq As Integer = 0
            With Me.ogvfinishrawmat
                _Seq = Val("" & .GetFocusedRowCellValue("FNSeq").ToString)
            End With

            Call LoadResourceFinishRawmatDetail(Me.FTConversionNo.Text, _Seq)

        Catch ex As Exception
            Call LoadResourceFinishRawmatDetail(Me.FTConversionNo.Text, 0)
        End Try

    End Sub

    Private Sub ocmautotrwWH_Click(sender As Object, e As EventArgs) Handles ocmautotrwWH.Click

        If CheckOwner() = False Then Exit Sub

        If Me.FTConversionNo.Text <> "" Then

            Dim _Qry As String = ""
            Dim dtauto As DataTable

            _Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
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
            _Qry &= vbCrLf & "   WHERE  B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "'"
            '  _Qry &= vbCrLf & "  AND C.FNOrderType =0"
            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType,ISNULL(B.FTOrderNoRef,''),ISNULL(CRef.FNHSysCmpId,0) "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpToRef WITH (NOLOCK) ON M.FNHSysCmpIdToRef = CmpToRef.FNHSysCmpId"
            _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
            _Qry &= vbCrLf & " ORDER BY  M.FTBarcodeNo"
            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then

                HI.TL.HandlerControl.ClearControl(_AutoTransferToWH)

                With _AutoTransferToWH

                    Call HI.ST.Lang.SP_SETxLanguage(_AutoTransferToWH)

                    .ReceiveNo = Me.FTConversionNo.Text
                    .WHID = Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString))
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                    Call LoadBarcode(Me.FTConversionNo.Text)

                End With

            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ โอนได้ กรุณาทำการตรวจสอบ !!!", 1410020009, Me.Text, , MessageBoxIcon.Warning)
            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTConversionNo_lbl.Text)
            FTConversionNo.Focus()
        End If
    End Sub

    Private Sub ocmautoissue_Click(sender As Object, e As EventArgs) Handles ocmautoissue.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTConversionNo.Text <> "" Then
            Dim _Qry As String = ""
            Dim dtauto As DataTable

            _Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
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
            _Qry &= vbCrLf & "   WHERE  B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTConversionNo.Text) & "' AND  ISNULL(C.FNHSysCmpId,0)=" & Val(HI.ST.SysInfo.CmpID) & ""
            '  _Qry &= vbCrLf & "  AND C.FNOrderType =0"
            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType,ISNULL(B.FTOrderNoRef,''),ISNULL(CRef.FNHSysCmpId,0) "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpToRef WITH (NOLOCK) ON M.FNHSysCmpIdToRef = CmpToRef.FNHSysCmpId"
            _Qry &= vbCrLf & "    WHERE FNTransactionQty <= 0"
            _Qry &= vbCrLf & " ORDER BY  M.FTBarcodeNo"
            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then

                HI.TL.HandlerControl.ClearControl(_AutoIssue)

                With _AutoIssue

                    Call HI.ST.Lang.SP_SETxLanguage(_AutoIssue)

                    .DocRefNo = Me.FTConversionNo.Text
                    .WHID = Integer.Parse(Val(Me.FNHSysWHId.Properties.Tag.ToString))
                    .FNHSysWHIdTo.Text = Me.FNHSysWHId.Text
                    .ogcbarcode.DataSource = dtauto.Copy
                    .ShowDialog()

                    Call LoadBarcode(Me.FTConversionNo.Text)

                End With

            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ AutoIssue กรุณาทำการตรวจสอบ !!!", 1509287418, Me.Text, , MessageBoxIcon.Warning)
            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTConversionNo_lbl.Text)
            FTConversionNo.Focus()
        End If
    End Sub
End Class