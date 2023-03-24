Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Controls

Public Class wReceiveAsset


    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wReceiveItemAsset
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _DataInfo As DataTable

    Private _ProcLoad As Boolean = False
    Private _GenBarcode As wGenerateBarcodeAsset
    Private _Multiple As wReceiveMultipleAsset
    Private _AddMasterpop As wAdditemMasterPop
    Private _wReceiveUnitAsset As wReceiveUnitAsset
    Private _EditCell As Boolean = False
    Private _FormLoad As Boolean = True
    Private _InitialPopupMaster As Boolean = False
    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        _AddItemPopup = New wReceiveItemAsset
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wReceiveUnitAsset.Name.ToString.Trim, _wReceiveUnitAsset)
        Catch ex As Exception
        Finally
        End Try

        _wReceiveUnitAsset = New wReceiveUnitAsset
        HI.TL.HandlerControl.AddHandlerObj(_wReceiveUnitAsset)
        _GenBarcode = New wGenerateBarcodeAsset
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenBarcode.Name.ToString.Trim, _GenBarcode)
        Catch ex As Exception
        Finally
        End Try

        _Multiple = New wReceiveMultipleAsset
        HI.TL.HandlerControl.AddHandlerObj(_Multiple)
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Multiple.Name.ToString.Trim, _Multiple)
        Catch ex As Exception
        Finally
        End Try

        _AddMasterpop = New wAdditemMasterPop
        'TL.HandlerControl.AddHandlerObj(_AddMasterpop)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddMasterpop.Name.ToString.Trim, _AddMasterpop)
        Catch ex As Exception

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

    Private Sub PrepareForm()

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

        Call LoadPOInfo(Key.ToString)
        Call LoadRcvDetail(Key.ToString)

        Call LoadBarcode(Key.ToString)

        Me.oxtb.SelectedTabPageIndex = 0



        FDReceiveDate.Properties.Buttons(0).Visible = False
        FDReceiveDate.Properties.ReadOnly = True

        _ProcLoad = False

    End Sub

    Private Sub LoadPOInfo(PoKey As String, Optional LoadRcv As Boolean = False)
        Dim _Str As String = ""
        Dim Dt As DataTable

        _Str = " SELECT        H.FTPurchaseNo, H.FNExchangeRate, S.FTSuplCode , C.FTCurCode "
        _Str &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS H WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON H.FNHSysSuplId = S.FNHSysSuplId INNER JOIN"
        _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH (NOLOCK) ON H.FNHSysCurId = C.FNHSysCurId"
        _Str &= vbCrLf & "  WHERE H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)

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


        '  Me.ogcdetail.DataSource = Dt.Copy

        '   Dt.Dispose()

    End Sub
    Private Sub LoadRcvDetail(PoKey As String)

        Dim _VatPer As Double = 0

        Dim _Str As String = ""
        _Str = "SELECT TOP 1 FNVatPer "
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS PH WITH(NOLOCK) "
        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

        _VatPer = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")))

        If _VatPer = 0 Then
            _Str = "  SELECT TOP 1  FTStateRcvVat"
            _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH(NOLOCK)"
            _Str &= vbCrLf & "  WHERE (FNHSysSuplId = " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ")"

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then
                _VatPer = 7
            End If

        End If
        _Str = " SELECT        D.FNHSysFixedAssetId, isnull(M.FTAssetCode,AP.FTAssetPartCode) as FTAssetCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , isnull(M.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName"
        Else
            _Str &= vbCrLf & " , isnull(M.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName"
        End If

        _Str &= vbCrLf & "  , D.FNHSysUnitId, U.FTUnitAssetCode as FTUnitCode, D.FNPrice, D.FNDisPer, MO.FTAssetModelCode,MO.FNHSysAssetModelId,M.FNHSysAssetBrandId,"
        _Str &= vbCrLf & "  D.FNDisAmt, D.FNNetPrice, D.FNQuantity, D.FNNetAmt, '' AS FTRemark  ,M.FTProductCode"
        _Str &= vbCrLf & " ,Convert(numeric(18,2),(D.FNNetAmt * " & _VatPer & ")/100.00) AS FNVatAmt"
        _Str &= vbCrLf & " ,D.FNNetAmt + Convert(numeric(18,2),(D.FNNetAmt * " & _VatPer & ")/100.00) AS FNNetVatAmt"

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS D WITH (NOLOCK) LEFT OUTER jOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS M WITH (NOLOCK) ON D.FNHSysFixedAssetId = M.FNHSysFixedAssetId LEFT OUTER jOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart as AP WITH(NOLOCK ) ON  D.FNHSysFixedAssetId = AP.FNHSysAssetPartId  Left OUTER JOIN "
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitAssetId LEFT OUTER jOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS MO ON M.FNHSysAssetModelId=MO.FNHSysAssetModelId"
        _Str &= vbCrLf & " WHERE        (D.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        _Str &= vbCrLf & " ORDER BY M.FNHSysFixedAssetId"

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        _Str = " SELECT        D.FNHSysFixedAssetId, isnull(M.FTAssetCode,AP.FTAssetPartCode) as aFTAssetCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , isnull(M.FTAssetNameTH,AP.FTAssetPartNameTH) AS aFTAssetName"
        Else
            _Str &= vbCrLf & " , isnull(M.FTAssetNameTH,AP.FTAssetPartNameEN) AS aFTAssetName"
        End If


        _Str &= vbCrLf & ", D.FNHSysUnitId, U.FTUnitAssetCode as FTUnitCode, D.FNPrice, D.FNDisPer, MO.FTAssetModelCode As aModel,isnull(M.FTProductCode,AP.FTProductCode) AS FTProductCode, "
        _Str &= vbCrLf & "  D.FNDisAmt, D.FNNetPrice, D.FNQuantity, D.FNNetAmt, '' AS FTRemark"

        _Str &= vbCrLf & "  ,ISNULL(PD.FNQuantity,0) AS FNPOQuantity"
        _Str &= vbCrLf & "  ,ISNULL(TRC.FNQuantity,0) AS FNTCQuantity"
        _Str &= vbCrLf & "  ,ISNULL(RTS.FNQuantity,0) AS FNRTSQuantity"
        _Str &= vbCrLf & "  ,ISNULL(PD.FNQuantity,0)  - (ISNULL(TRC.FNQuantity,0)-ISNULL(RTS.FNQuantity,0)) AS FNPOBALQuantity"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS D WITH (NOLOCK) LEFT OUTER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS M WITH (NOLOCK) ON D.FNHSysFixedAssetId = M.FNHSysFixedAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart as AP WITH(NOLOCK ) ON  D.FNHSysFixedAssetId = AP.FNHSysAssetPartId  Left OUTER JOIN "
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitAssetId"
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS MO WITH (NOLOCK) ON M.FNHSysAssetModelId = MO.FNHSysAssetModelId"

        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS H WITH (NOLOCK) ON D.FTReceiveNo = H.FTReceiveNo "

        _Str &= vbCrLf & " LEFT OUTER JOIN      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD"
        _Str &= vbCrLf & " ON H.FTPurchaseNo = PD.FTPurchaseNo AND D.FNHSysFixedAssetId = PD.FNHSysFixedAssetId "

        _Str &= vbCrLf & " LEFT OUTER JOIN    ("
        _Str &= vbCrLf & " SELECT        RH.FTPurchaseNo, RD.FNHSysFixedAssetId, SUM(RD.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, RD.FNHSysFixedAssetId"
        _Str &= vbCrLf & " ) AS TRC ON H.FTPurchaseNo = TRC.FTPurchaseNo AND D.FNHSysFixedAssetId = TRC.FNHSysFixedAssetId "

        _Str &= vbCrLf & " LEFT OUTER JOIN    ("
        _Str &= vbCrLf & " SELECT RH.FTPurchaseNo, B.FNHSysFixedAssetId, SUM(RD.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier  AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS RD WITH (NOLOCK) ON RH.FTReturnSuplNo = RD.FTDocumentNo  INNER JOIN "
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH (NOLOCK) ON RD.FTBarcodeNo = B.FTBarcodeNo"
        _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, B.FNHSysFixedAssetId"
        _Str &= vbCrLf & " ) AS RTS ON H.FTPurchaseNo = RTS.FTPurchaseNo AND D.FNHSysFixedAssetId = RTS.FNHSysFixedAssetId "

        _Str &= vbCrLf & " WHERE        (D.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        _Str &= vbCrLf & " ORDER BY M.FTAssetCode"

        Me.ogcmuljob.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)


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


            Call UpdateAssetUnit(FTPurchaseNo.Text)

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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)



            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)





            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'")
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
                    HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280901, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub


        If Me.VerrifyData Then
            If Me.SaveData() Then

                FDReceiveDate.Properties.Buttons(0).Visible = False
                FDReceiveDate.Properties.ReadOnly = True
                'Call Barcode()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If

    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub



        If BarcodeAsset.CheckDucumentCreateBar(FTReceiveNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTReceiveNo.Text, Me.Text) = True Then



            If Me.DeleteData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)

                Me.DefaultsData()
                Me.FTPurchaseNo.Focus()

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTReceiveNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "InventoryAsset\"
                .ReportName = "ReceiveSlipAsset.rpt"
                .Formular = "{TFIXEDTReceive.FTReceiveNo}='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "InventoryAsset\"
                .ReportName = "ReceiveSlipAsset_Barcode.rpt"
                '   .Formular = "{TFIXEDTReceive.FTReceiveNo}='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                .Formular = "{TFIXEDTBarcode_IN.FTDocumentNo}='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
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

    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        _FormLoad = False
        Try
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
            _FormLoad = False
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
                _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS PH WITH(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

                _VatPer = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")))

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
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "


                FNExchangeRate.Properties.ReadOnly = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FIXED, "") <> "")
            End If

        End If
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With Me.ogvdetail
                'FTStateSendApp
                Select Case True
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTStateSendApp").ToString = "1")
                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue
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

            Me.FNHSysWHAssetId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysWHAssetId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FNExchangeRate.Properties.ReadOnly = (dt.Rows.Count > 0)

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


    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        With ogvdetail
            If CheckOwner() = False Then Exit Sub

            If BarcodeAsset.CheckDucumentCreateBar(FTReceiveNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
                Exit Sub
            End If



            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            '  If (CheckReceive(Me.FTPoNo.Text) = False) Then Exit Sub

            Dim _AssetID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString
            Dim _Str As String = ""

            Try
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail   WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' AND  FNHSysFixedAssetId=" & Val(_AssetID) & " "
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub
                End If

                If EqualizeJob(Me.FTReceiveNo.Text, Me.FTPurchaseNo.Text, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, Val(_AssetID)) = False Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Exit Sub
                End If

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                '_Str = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver"
                '_Str &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                '_Str &= vbCrLf & "  AND  FNHSysRawMatId=" & Val(_AssetID) & " "
                '_Str &= vbCrLf & " AND ISNULL(FTStateApprove,'') ='' "
                'HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_INVEN)

                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail   WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' AND  FNHSysFixedAssetId=" & Val(_AssetID) & " ")

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
    Private Sub UpdateAssetUnit(PoNo As String)
        Dim _Qry As String


        _Qry = " UPDATE B SET FNHSysUnitSectId = ISNULL((SELECT MAX(FNHSysUnitSectId) FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS C WITH(NOLOCK)  WHERE C.FTAssetCode =B.FTAssetCode),0)"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B  ON A.FNHSysFixedAssetId = B.FNHSysFixedAssetId"
        _Qry &= vbCrLf & "  WHERE A.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(PoNo) & "' "
        _Qry &= vbCrLf & "  AND  B.FNHSysUnitSectId = 0 "

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FIXED)

        'FNHSysUnitId
    End Sub
    Private Sub TabChanged()
        Try
            ocmgenbarcode.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmdeletebarcodegen.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            '  ocmautotrw.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            'ocmautotrwWH.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)
            ocmadd.Visible = (oxtb.SelectedTabPage.Name = otpdetailitem.Name)
            ocmremove.Visible = (oxtb.SelectedTabPage.Name = otpdetailitem.Name)
            ocmmanagercvdetail.Visible = (oxtb.SelectedTabPage.Name = XtraTabPage1.Name)
            ocmaddmaster.Visible = (oxtb.SelectedTabPage.Name = otpreceivebarcode.Name)

        Catch ex As Exception
        End Try
        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub oxtb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles oxtb.SelectedPageChanged
        Call TabChanged()
    End Sub

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        If CheckOwner() = False Then Exit Sub

        If BarcodeAsset.CheckDucumentCreateBar(FTReceiveNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If


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

        _Str = "SELECT FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase"
        _Str &= vbCrLf & "  WHERE      (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
        Dim _AssetType As Integer = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED)
        If _AssetType <> 1 Then


            _Str = " Select M.FTAssetCode"
            _Str &= vbCrLf & "  ,MAX(M.FTAssetName) AS FTAssetName"
            _Str &= vbCrLf & "  ,MAX(M.FTUnitAssetCode) AS FTUnitAssetCode "
            _Str &= vbCrLf & "  ,MAX(ISNULL(U.FTUnitAssetCode,'')) AS FNHSysUnitAssetId"
            _Str &= vbCrLf & "  ,MAX(ISNULL(U.FNHSysUnitAssetId,0)) AS  FNHSysUnitAssetId_Hide"
            _Str &= vbCrLf & "  FROM"
            _Str &= vbCrLf & "  (SELECT   P.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  ,P.FNHSysUnitId AS FNHSysUnitAssetId"
            _Str &= vbCrLf & "  ,IU.FTUnitAssetCode "
            _Str &= vbCrLf & "  ,IM.FNHSysUnitAssetId  AS FNHSysUnitAssetIdIM"
            _Str &= vbCrLf & "  ,IM.FTAssetCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & " ,IM.FTAssetNameTH AS FTAssetName"
            Else
                _Str &= vbCrLf & " ,IM.FTAssetNameEN AS FTAssetName"
            End If

            _Str &= vbCrLf & "  ,ISNULL(("
            _Str &= vbCrLf & "     SELECT      TOP 1   ISNULL(B.FNHSysUnitAssetId,0) as FNHSysUnitAssetId"
            _Str &= vbCrLf & "   FROM  [HITECH_MASTER].dbo.TASMAsset AS B  WITH(NOLOCK)  INNER JOIN"
            _Str &= vbCrLf & " 	 [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS A  WITH(NOLOCK)  ON B.FNHSysFixedAssetId = A.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  WHERE ISNULL(B.FNHSysFixedAssetId,'') =IM.FNHSysFixedAssetId  "
            _Str &= vbCrLf & "  ),0) AS FNHSysUnitIdRCV"
            _Str &= vbCrLf & "  ,ISNULL(("
            _Str &= vbCrLf & "    SELECT      TOP 1   ISNULL(B.FNHSysUnitAssetId,0) as FNHSysUnitAssetId"
            _Str &= vbCrLf & "   FROM  [HITECH_MASTER].dbo.TASMAsset AS B  WITH(NOLOCK)  INNER JOIN"
            _Str &= vbCrLf & " 	  [HITECH_FIXEDASSET].dbo.TFIXEDTBarcode AS A  WITH(NOLOCK)  ON B.FNHSysFixedAssetId = A.FNHSysFixedAssetId"
            _Str &= vbCrLf & "   WHERE ISNULL(B.FTAssetCode,'') =IM.FTAssetCode  "
            _Str &= vbCrLf & "  ),0) AS FNHSysUnitIdADJ"
            _Str &= vbCrLf & " FROM    [HITECH_FIXEDASSET].dbo.TFIXEDTPurchase_Detail AS P WITH (NOLOCK)"
            _Str &= vbCrLf & "   INNER JOIN [HITECH_MASTER].dbo.TASMAsset AS IM WITH(NOLOCK)"
            _Str &= vbCrLf & "  ON P.FNHSysFixedAssetId = IM.FNHSysFixedAssetId"
            _Str &= vbCrLf & "   INNER JOIN [HITECH_MASTER].dbo.TCNMUnitAsset AS IU WITH(NOLOCK)  ON"
            _Str &= vbCrLf & "      P.FNHSysUnitId = IU.FNHSysUnitAssetId"
            _Str &= vbCrLf & "  WHERE      (P.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
            _Str &= vbCrLf & "  ) AS M LEFT OUTER JOIN"

            _Str &= vbCrLf & "  ("
            _Str &= vbCrLf & "  SELECT FNHSysUnitAssetId, FTUnitAssetCode"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK)"
            _Str &= vbCrLf & "  WHERE  (FTStateUnitStock = '1')"
            _Str &= vbCrLf & "  ) AS U ON M.FNHSysUnitAssetIdIM = U.FNHSysUnitAssetId "
            _Str &= vbCrLf & "  WHERE M.FNHSysUnitIdRCV =0 AND M.FNHSysUnitIdADJ=0"
            _Str &= vbCrLf & "  GROUP BY   M.FTAssetCode "
            _Str &= vbCrLf & "  Order By M.FTAssetCode"
        Else

            _Str = " Select M.FTAssetPartCode As FTAssetCode"
            _Str &= vbCrLf & "  ,MAX(M.FTAssetName) AS FTAssetName"
            _Str &= vbCrLf & "  ,MAX(M.FTUnitAssetCode) As FTUnitAssetCode "
            _Str &= vbCrLf & "  ,MAX(ISNULL(U.FTUnitAssetCode,'')) AS FNHSysUnitAssetId"
            _Str &= vbCrLf & "  ,MAX(ISNULL(U.FNHSysUnitAssetId,0)) As  FNHSysUnitAssetId_Hide"
            _Str &= vbCrLf & "  FROM"
            _Str &= vbCrLf & "(SELECT   P.FNHSysFixedAssetId"
            _Str &= vbCrLf & " ,P.FNHSysUnitId As FNHSysUnitAssetId"
            _Str &= vbCrLf & "  ,IU.FTUnitAssetCode "
            _Str &= vbCrLf & "  ,IM.FNHSysUnitAssetId  As FNHSysUnitAssetIdIM"
            _Str &= vbCrLf & "  ,IM.FTAssetPartCode "
            _Str &= vbCrLf & "  ,IM.FTAssetPartNameTH As FTAssetName"
            _Str &= vbCrLf & "  ,ISNULL(("
            _Str &= vbCrLf & "  Select Top 1   ISNULL(B.FNHSysUnitAssetId,0) As FNHSysUnitAssetId"
            _Str &= vbCrLf & "  From [HITECH_MASTER].dbo.TASMAssetPart As B  With(NOLOCK)  INNER Join"
            _Str &= vbCrLf & "  [HITECH_FIXEDASSET].dbo.TFIXEDTReceive_Detail As A  With(NOLOCK)  On B.FNHSysAssetPartId = A.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  Where ISNULL(B.FNHSysAssetPartId,'') =IM.FNHSysAssetPartId  "
            _Str &= vbCrLf & "  ),0) As FNHSysUnitIdRCV"
            _Str &= vbCrLf & "  ,ISNULL(("
            _Str &= vbCrLf & "  Select  Top 1   ISNULL(B.FNHSysUnitAssetId,0) As FNHSysUnitAssetId"
            _Str &= vbCrLf & "  From [HITECH_MASTER].dbo.TASMAssetPart AS B  WITH(NOLOCK)  INNER Join"
            _Str &= vbCrLf & "  [HITECH_FIXEDASSET].dbo.TFIXEDTBarcode As A  With(NOLOCK)  On B.FNHSysAssetPartId = A.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  Where ISNULL(B.FTAssetPartCode,'') =IM.FTAssetPartCode  "
            _Str &= vbCrLf & "  ),0) As FNHSysUnitIdADJ"
            _Str &= vbCrLf & "  From [HITECH_FIXEDASSET].dbo.TFIXEDTPurchase_Detail AS P WITH (NOLOCK)"
            _Str &= vbCrLf & "  INNER Join [HITECH_MASTER].dbo.TASMAssetPart As IM With(NOLOCK)"
            _Str &= vbCrLf & "  On P.FNHSysFixedAssetId = IM.FNHSysAssetPartId"
            _Str &= vbCrLf & "  INNER Join [HITECH_MASTER].dbo.TCNMUnitAsset As IU With(NOLOCK)  On"
            _Str &= vbCrLf & "  P.FNHSysUnitId = IU.FNHSysUnitAssetId"
            _Str &= vbCrLf & "  WHERE(P.FTPurchaseNo = N'91AE17ASSETD100002')"
            _Str &= vbCrLf & "  ) As M LEFT OUTER JOIN"
            _Str &= vbCrLf & "  ("
            _Str &= vbCrLf & "  Select  FNHSysUnitAssetId, FTUnitAssetCode"
            _Str &= vbCrLf & "  From [HITECH_MASTER].dbo.TCNMUnitAsset As U With(NOLOCK)"
            _Str &= vbCrLf & "  Where (FTStateUnitStock = '1')"
            _Str &= vbCrLf & "  ) As U On M.FNHSysUnitAssetIdIM = U.FNHSysUnitAssetId "
            _Str &= vbCrLf & "  Where M.FNHSysUnitIdRCV = 0 And M.FNHSysUnitIdADJ = 0"
            _Str &= vbCrLf & "  Group By M.FTAssetPartCode"
            _Str &= vbCrLf & "  Order By M.FTAssetPartCode"
        End If
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MASTER)

        If _dt.Rows.Count > 0 Then
            HI.MG.ShowMsg.mInfo("พบข้อมูล สินทรัพย์ ที่ยังไม่มีการ เดิน Transaction ใน Stock กรุณาทำการระบุหน่วยที่ใช้ในการจัดเก็บ !!!", 1711091036, Me.Text, , MessageBoxIcon.Warning)

            HI.TL.HandlerControl.ClearControl(_wReceiveUnitAsset)

            With _wReceiveUnitAsset
                ' Call HI.ST.Lang.SP_SETxLanguage(_wReceiveUnitAsset)

                '.ocmreceive.Enabled = True
                '.ocmcancel.Enabled = True
                Call HI.ST.Lang.SP_SETxLanguage(_wReceiveUnitAsset)




                .ogvrcv.ClearColumnsFilter()
                .ogvrcv.ActiveFilter.Clear()
                .ogcrcv.DataSource = _dt
                '.ProcessProc = False
                .ShowDialog()

                If .ProcessProc = False Then
                    Exit Sub
                End If

            End With
        End If
        'popup barcode

        If _AssetType <> 1 Then


            _Str = "Select P.FNHSysFixedAssetId,A.FTAssetCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & "  ,isnull(A.FTAssetNameTH,A.FTAssetNameTH)  As FTAssetName"
            Else
                _Str &= vbCrLf & "  ,isnull(A.FTAssetNameEN,A.FTAssetNameEN)  As FTAssetName"
            End If
            _Str &= vbCrLf & ",U.FTUnitAssetCode AS FTUnitAssetCode"
            _Str &= vbCrLf & ",'' AS FNHSysUnitAssetId"
            _Str &= vbCrLf & ",0 AS  FNHSysUnitAssetId_Hide"
            _Str &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS P WITH(NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) ON P.FNHSysFixedAssetId = A.FNHSysFixedAssetId LEFT OUTER JOIN"
            _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitAssetId"
            _Str &= vbCrLf & "WHERE P.FTPurchaseNo = '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "' and A.FNHSysUnitAssetId IS NULL"
        Else

            _Str = "Select P.FNHSysFixedAssetId,A.FTAssetPartCode AS FTAssetCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & "  ,isnull(A.FTAssetPartNameTH,A.FTAssetPartNameTH)  As FTAssetName"
            Else
                _Str &= vbCrLf & "  ,isnull(A.FTAssetPartNameEN,A.FTAssetPartNameEN)  As FTAssetName"
            End If
            _Str &= vbCrLf & ",U.FTUnitAssetCode AS FTUnitAssetCode"
            _Str &= vbCrLf & ",'' AS FNHSysUnitAssetId"
            _Str &= vbCrLf & ",0 AS  FNHSysUnitAssetId_Hide"
            _Str &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS P WITH(NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS A WITH(NOLOCK) ON P.FNHSysFixedAssetId = A.FNHSysAssetPartId LEFT OUTER JOIN"
            _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitAssetId"
            _Str &= vbCrLf & "WHERE P.FTPurchaseNo = '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "' and A.FNHSysUnitAssetId IS NULL"
        End If
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MASTER)

        If _dt.Rows.Count > 0 Then
            HI.MG.ShowMsg.mInfo("พบสินทรัพย์ยังไม่ได้ทำการกำหนดหน่วยนับ กรุณากำหนดหน่วยนับที่ต้องการจัดเก็บ !!!", 1711090953, Me.Text, , MessageBoxIcon.Warning)

            HI.TL.HandlerControl.ClearControl(_wReceiveUnitAsset)

            With _wReceiveUnitAsset
                ' Call HI.ST.Lang.SP_SETxLanguage(_wReceiveUnitAsset)

                '.ocmreceive.Enabled = True
                '.ocmcancel.Enabled = True
                Call HI.ST.Lang.SP_SETxLanguage(_wReceiveUnitAsset)




                .ogvrcv.ClearColumnsFilter()
                .ogvrcv.ActiveFilter.Clear()
                .ogcrcv.DataSource = _dt
                '.ProcessProc = False
                .ShowDialog()

                If .ProcessProc = False Then
                    Exit Sub
                End If

            End With
        End If


        If _AssetType <> 1 Then
            _Str = " Select  (Case When ISNULL(FNRcvQty, 0) > 0 Then '1' ELSE '0' END) AS FTStateSelect"
            _Str &= vbCrLf & "    ,P.FNHSysFixedAssetId,"

            _Str &= vbCrLf & "  IM.FTAssetCode AS FTAssetCode ,MO.FTAssetModelCode AS Model,"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & "  IM.FTAssetNameTH AS FTAssetName,"
            Else
                _Str &= vbCrLf & "  IM.FTAssetNameEN  AS FTAssetName,"
            End If


            _Str &= vbCrLf & "  P.FNHSysUnitId,"
            _Str &= vbCrLf & "  U.FTUnitAssetCode as FTUnitCode ,"
            _Str &= vbCrLf & "  P.FNPrice,"
            _Str &= vbCrLf & "  P.FNDisPer,"
            _Str &= vbCrLf & "  P.FNDisAmt,"
            _Str &= vbCrLf & "  ((P.FNPrice- P.FNDisAmt ) ) AS FNNetPrice,"
            _Str &= vbCrLf & "   P.FNQuantity,"
            _Str &= vbCrLf & "  ISNULL(RCV.FNRcvHisQty,0) As FNRcvHisQty,"
            _Str &= vbCrLf & "  (P.FNQuantity-ISNULL(RCV.FNRcvHisQty,0)) AS FNPOBalQty,"
            _Str &= vbCrLf & "  Convert(numeric(18,4),ISNULL(FNRcvQty,0)) AS FNRcvQty"
            _Str &= vbCrLf & ",'' As FTStateRcvOver"
            _Str &= vbCrLf & ",'0' As FTStateSendAppRcv"
            _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyPass"
            _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyOver"
            _Str &= vbCrLf & "   FROM "
            _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FNHSysFixedAssetId, FNHSysUnitId"

            If FNRceceiveType.SelectedIndex = 2 Then
                _Str &= vbCrLf & " ,0 AS  FNPrice,0 AS  FNDisPer, 0 As FNDisAmt "
            Else
                _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt  "
            End If

            _Str &= vbCrLf & ", SUM(FNQuantity) AS FNQuantity"

            _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS P WITH (NOLOCK)"
            _Str &= vbCrLf & " WHERE        (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
            _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysFixedAssetId, FNHSysUnitId,FNPrice, FNDisPer, FNDisAmt) AS P "
            _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset as IM WITH(NOLOCK ) ON  P.FNHSysFixedAssetId = IM.FNHSysFixedAssetId "
            ' _Str &= vbCrLf & "   Left OUTER JOIN [HITECH_MASTER].dbo.TASMAssetPart as AP WITH(NOLOCK ) ON  P.FNHSysFixedAssetId = AP.FNHSysAssetPartId"
            _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset as U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitAssetId "
            _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS MO WITH (NOLOCK) ON IM.FNHSysAssetModelId = MO.FNHSysAssetModelId"
            _Str &= vbCrLf & " LEFT OUTER JOIN (SELECT        RH.FTPurchaseNo, RD.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  ,SUM(CASE WHEN RH.FTReceiveNo<>N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Properties.Tag.ToString) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvHisQty ,"
            _Str &= vbCrLf & " SUM(CASE WHEN RH.FTReceiveNo=N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Properties.Tag.ToString) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvQty "
            _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS RH WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
            _Str &= vbCrLf & " WHERE        (RH.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
            _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, RD.FNHSysFixedAssetId ) As RCV"
            _Str &= vbCrLf & " ON P.FTPurchaseNo = Rcv.FTPurchaseNo AND P.FNHSysFixedAssetId = Rcv.FNHSysFixedAssetId "
            _Str &= vbCrLf & " ORDER BY   IM.FTAssetCode "
        Else
            _Str = " Select  (Case When ISNULL(FNRcvQty, 0) > 0 Then '1' ELSE '0' END) AS FTStateSelect"
            _Str &= vbCrLf & "    ,P.FNHSysFixedAssetId,"

            _Str &= vbCrLf & "  AP.FTAssetPartCode AS FTAssetCode ,'' AS Model,"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & "  AP.FTAssetPartNameTH  AS FTAssetName,"
            Else
                _Str &= vbCrLf & "  AP.FTAssetPartNameEN AS FTAssetName,"
            End If


            _Str &= vbCrLf & "  P.FNHSysUnitId,"
            _Str &= vbCrLf & "  U.FTUnitAssetCode as FTUnitCode ,"
            _Str &= vbCrLf & "  P.FNPrice,"
            _Str &= vbCrLf & "  P.FNDisPer,"
            _Str &= vbCrLf & "  P.FNDisAmt,"
            _Str &= vbCrLf & "  ((P.FNPrice- P.FNDisAmt ) ) AS FNNetPrice,"
            _Str &= vbCrLf & "   P.FNQuantity,"
            _Str &= vbCrLf & "  ISNULL(RCV.FNRcvHisQty,0) As FNRcvHisQty,"
            _Str &= vbCrLf & "  (P.FNQuantity-ISNULL(RCV.FNRcvHisQty,0)) AS FNPOBalQty,"
            _Str &= vbCrLf & "  Convert(numeric(18,4),ISNULL(FNRcvQty,0)) AS FNRcvQty"
            _Str &= vbCrLf & ",'' As FTStateRcvOver"
            _Str &= vbCrLf & ",'0' As FTStateSendAppRcv"
            _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyPass"
            _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyOver"
            _Str &= vbCrLf & "   FROM "
            _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FNHSysFixedAssetId, FNHSysUnitId"

            If FNRceceiveType.SelectedIndex = 2 Then
                _Str &= vbCrLf & " ,0 AS  FNPrice,0 AS  FNDisPer, 0 As FNDisAmt "
            Else
                _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt  "
            End If

            _Str &= vbCrLf & ", SUM(FNQuantity) AS FNQuantity"

            _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS P WITH (NOLOCK)"
            _Str &= vbCrLf & " WHERE        (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
            _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysFixedAssetId, FNHSysUnitId,FNPrice, FNDisPer, FNDisAmt) AS P "
            _Str &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset as IM WITH(NOLOCK ) ON  P.FNHSysFixedAssetId = IM.FNHSysFixedAssetId "
            _Str &= vbCrLf & "   Left OUTER JOIN [HITECH_MASTER].dbo.TASMAssetPart as AP WITH(NOLOCK ) ON  P.FNHSysFixedAssetId = AP.FNHSysAssetPartId"
            _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset as U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitAssetId "
            '  _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS MO WITH (NOLOCK) ON IM.FNHSysAssetModelId = MO.FNHSysAssetModelId"
            _Str &= vbCrLf & " LEFT OUTER JOIN (SELECT        RH.FTPurchaseNo, RD.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  ,SUM(CASE WHEN RH.FTReceiveNo<>N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Properties.Tag.ToString) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvHisQty ,"
            _Str &= vbCrLf & " SUM(CASE WHEN RH.FTReceiveNo=N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Properties.Tag.ToString) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvQty "
            _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS RH WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
            _Str &= vbCrLf & " WHERE        (RH.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
            _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, RD.FNHSysFixedAssetId ) As RCV"
            _Str &= vbCrLf & " ON P.FTPurchaseNo = Rcv.FTPurchaseNo AND P.FNHSysFixedAssetId = Rcv.FNHSysFixedAssetId "
            _Str &= vbCrLf & " ORDER BY   AP.FTAssetPartCode "
        End If
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)
        HI.TL.HandlerControl.ClearControl(_AddItemPopup)

        With _AddItemPopup
            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)

            Try
                .ReceiveType = Me.FNRceceiveType.SelectedIndex
            Catch ex As Exception
                .ReceiveType = wReceiveItemAsset.RcvType.RcvNormal
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

                'check list เฉพาะที่เป็น เครื่องจักร


                'end  'check list เฉพาะที่เป็น เครื่องจักร

                'Dim _Spls As New HI.TL.SplashScreen("Saving Receive Detail...   Please Wait   ")

                Try
                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    For Each R As DataRow In _dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv<>'1' ")

                        '_Spls.UpdateInformation("Receive Detail....   Please Wait    ")

                        Dim _SysUnitStock As Integer
                        Dim _TsysAssetId As Integer
                        Dim _SysUnitPo As Integer
                        Dim _SysAssetCode As String = R!FTAssetCode.ToString
                        _TsysAssetId = R!FNHSysFixedAssetId
                        _SysUnitPo = R!FNHSysUnitId
                        Dim _TsysAsset As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & _TsysAssetId & " AND FTAssetCode = '" & _SysAssetCode & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                        If _TsysAsset <> "" Then
                            _SysUnitStock = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysUnitAssetId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WITH(NOLOCK) WHERE   FNHSysFixedAssetId =" & _TsysAssetId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0")))
                        Else
                            _SysUnitStock = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysUnitAssetId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WITH(NOLOCK) WHERE   FNHSysAssetPartId =" & _TsysAssetId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0")))
                        End If


                        _Str = " SELECT      TOP 1  FNHSysUnitAssetId  "
                        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAssetConvert  WITH (NOLOCK) "
                        _Str &= vbCrLf & "  WHERE FNHSysUnitAssetId =" & Integer.Parse(_SysUnitPo) & " "
                        _Str &= vbCrLf & "  AND FNHSysUnitAssetIdTo =" & Integer.Parse(_SysUnitStock) & " "




                        _Str = "   SELECT   TOP 1    FTReceiveNo, FNHSysFixedAssetId, FNHSysUnitId"
                        _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity"
                        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail "
                        _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & Val(R!FNHSysFixedAssetId.ToString) & " "

                        If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_FIXED, "") = "" Then

                            _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail ( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FNHSysFixedAssetId,FNHSysUnitId,  FNPrice, FNDisPer, FNDisAmt,FNNetPrice,FNNetAmt, FNQuantity) "
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & "," & Val(R!FNHSysFixedAssetId.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & ""
                            _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                            _Str &= vbCrLf & "," & CDbl(Format(Val(R!FNRcvQty.ToString) * Val(R!FNPrice.ToString), HI.ST.Config.AmtFormat)) & " "
                            _Str &= vbCrLf & "," & Val(R!FNRcvQty.ToString) & " "

                        Else

                            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail SET "
                            _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",FNHSysUnitId=" & Val(R!FNHSysUnitId.ToString) & " "
                            _Str &= vbCrLf & ", FNPrice=" & Val(R!FNPrice.ToString) & " "
                            _Str &= vbCrLf & ", FNDisPer=" & Val(R!FNDisPer.ToString) & " "
                            _Str &= vbCrLf & ", FNDisAmt=" & Val(R!FNDisAmt.ToString) & " "
                            _Str &= vbCrLf & ", FNNetPrice=" & Val(R!FNNetPrice.ToString) & " "
                            _Str &= vbCrLf & ",FNNetAmt=" & CDbl(Format(Val(R!FNRcvQty.ToString) * Val(R!FNNetPrice.ToString), HI.ST.Config.AmtFormat)) & " "
                            _Str &= vbCrLf & ", FNQuantity=" & Val(R!FNRcvQty.ToString) & " "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & Val(R!FNHSysFixedAssetId.ToString) & " "

                        End If

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            '_Spls.Close()
                            Exit Sub
                        End If

                        '_Spls.UpdateInformation("Equalize Job....   Please Wait    ")

                        If EqualizeJob(Me.FTReceiveNo.Text, Me.FTPurchaseNo.Text, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, Val(R!FNHSysFixedAssetId.ToString)) = False Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            '_Spls.Close()
                            Exit Sub
                        End If

                    Next

                    For Each R As DataRow In _dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv='1' ")

                        '_Spls.UpdateInformation("Receive Detail....   Please Wait    ")

                        _Str = "   SELECT   TOP 1    FTReceiveNo, FNHSysFixedAssetId, FNHSysUnitId"
                        _Str &= vbCrLf & " , FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity"
                        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail "
                        _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & Val(R!FNHSysFixedAssetId.ToString) & " "

                        If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_FIXED, "") = "" Then

                            _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail ( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FNHSysFixedAssetId,FNHSysUnitId,  FNPrice, FNDisPer, FNDisAmt,FNNetPrice,FNNetAmt, FNQuantity) "
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & "," & Val(R!FNHSysFixedAssetId.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & ""
                            _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                            _Str &= vbCrLf & ",0 "
                            _Str &= vbCrLf & ",0 "

                        Else

                            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail SET "
                            _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",FNHSysUnitId=" & Val(R!FNHSysUnitId.ToString) & " "
                            _Str &= vbCrLf & ", FNPrice=" & Val(R!FNPrice.ToString) & " "
                            _Str &= vbCrLf & ", FNDisPer=" & Val(R!FNDisPer.ToString) & " "
                            _Str &= vbCrLf & ", FNDisAmt=" & Val(R!FNDisAmt.ToString) & " "
                            _Str &= vbCrLf & ", FNNetPrice=" & Val(R!FNNetPrice.ToString) & " "
                            _Str &= vbCrLf & ",FNNetAmt=0"
                            _Str &= vbCrLf & ", FNQuantity=0 "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & Val(R!FNHSysFixedAssetId.ToString) & " "

                        End If

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            '_Spls.Close()
                            Exit Sub
                        End If

                    Next

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    If _dt.Select(" FTStateSelect='1'  AND FNRcvQty > 0 AND FTStateSendAppRcv='1' ").Length > 0 Then
                        '_Spls.UpdateInformation("Sending Approve....   Please Wait    ")
                        'Call SendMailAppRcvOver(_dt)
                    End If

                    '_Spls.Close()

                    ' Exit Sub

                Catch ex As Exception
                    '_Spls.Close()

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    ' Exit Sub
                End Try
                Me.LoadRcvDetail(Me.FTReceiveNo.Text)
                PrepareListBarcode()
                'Me.Barcode(Me.FTReceiveNo.Text)

            End If

        End With

        _dt.Dispose()

    End Sub

    Private Sub PrepareListBarcode()
        Dim Qry As String = ""
        Dim dt As New DataTable
        Qry = "SELECT FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase"
        Qry &= vbCrLf & "  WHERE      (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
        Dim _AssetType As Integer = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

        Try
            With dt
                .Columns.Add("FTBarcodeNo") : .Columns.Add("FTAssetCode") : .Columns.Add("FTAssetName") : .Columns.Add("FTUnitCode") : .Columns.Add("FTPurchaseNo") : .Columns.Add("FNQuantity") : .Columns.Add("FNHSysFixedAssetId")
                For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Rows
                    Qry = "Select isnull(A.FNFixedAssetType, 6) As FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail As P With(NOLOCK) Left outer join"
                    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As A WITH(NOLOCK) ON P.FNHSysFixedAssetId = A.FNHSysFixedAssetId LEFT OUTER JOIN"
                    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON P.FNHSysFixedAssetId = AP.FNHSysAssetPartId where P.FNHSysFixedAssetId =" & R!FNHSysFixedAssetId & ""

                    '   If HI.Conn.SQLConn.GetField("select A.FTAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) where FNFixedAssetType>=6 and FNFixedAssetType<=7  and FNHSysFixedAssetId=" & R!FNHSysFixedAssetId & "", Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                    If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "6" Or HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "7" Or HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "3" Then
                        If _AssetType <> 1 Then
                            Qry = "Select isnull(FTSerialNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & R!FNHSysFixedAssetId & ""
                        Else
                            Qry = "Select isnull(FTSerialNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WHERE FNHSysAssetPartId = " & R!FNHSysFixedAssetId & ""
                        End If
                        Dim _SerialNo As String = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
                        _IDasset = R!FNHSysFixedAssetId.ToString
                        _SysUnitCode = R!FTUnitCode
                        _QuantityConvert = R!FNQuantity
                        _SysUnit = R!FNHSysUnitId
                        Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                        .Rows.Add(_SerialNo, R!FTAssetCode.ToString, R!FTAssetName.ToString, _SysUnitCode, Me.FTPurchaseNo.Text, _QuantityConvert, R!FNHSysFixedAssetId)
                        .AcceptChanges()
                        _DT = dt
                        If _SerialNo <> "" Then SaveBar()
                    End If
                Next
                '.Rows.Add("KKOOLOFKGLE", 5555, "JOKER", "Box", "PO", 10, 1668630001)
            End With
            ogcbarcode.DataSource = dt


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    'Public Shared Function CheckCloseStock(FNHSysWHId As Integer, FTDateTrans As String) As Boolean
    '    Dim _State As Boolean = False
    '    Dim _Qry As String = ""

    '    If FNHSysWHId > 0 Then
    '        _Qry = " SELECT TOP 1  FTYear + '/' + FTMonth + '/31' AS FTCloseMonthYear"
    '        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENStockLastMonthly AS A WITH(NOLOCK)"
    '        _Qry &= vbCrLf & " WHERE FNHSysWHId=" & FNHSysWHId & " "
    '        _Qry &= vbCrLf & "  AND  FTYear + '/' + FTMonth + '/31'>='" & HI.UL.ULDate.ConvertEnDB(FTDateTrans) & "' "

    '        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
    '            HI.MG.ShowMsg.mInfo("ช่วงเวลานี้ ของ คลังนี้ได้มีการ ปิด สต๊อกไปแล้ว ไม่สามารถทำการแก้ไขรายการใดๆเพิ่มได้อีก !!!", 1496160701, "", , Windows.Forms.MessageBoxIcon.Warning)
    '            _State = True
    '        End If

    '    Else

    '        HI.MG.ShowMsg.mInfo("ข้องมูลคลังสินค้าไม่ถูกต้อง !!!", 1496160781, "", , Windows.Forms.MessageBoxIcon.Warning)
    '        _State = True

    '    End If

    '    Return _State
    'End Function
    Public Shared Function CheckDucumentCreateBar(DocKey As String, Optional FNHSysFixedAssetId As Integer = 0, Optional SeqRefer As Integer = -1, Optional FOKey As String = "") As Boolean
        Dim Str As String = ""
        Str = "SELECT TOP 1 FTDocumentNo "
        Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode  AS A WITH (NOLOCK) "
        Str &= vbCrLf & " WHERE FTDocumentNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        If FNHSysFixedAssetId > 0 Then
            Str &= vbCrLf & " AND FNHSysFixedAssetId =" & FNHSysFixedAssetId & " "
        End If

        'If SeqRefer > -1 Then
        'Str &= vbCrLf & " AND FNSeqRef =" & SeqRefer & " "
        'End If

        'If FOKey <> "" Then
        'Str &= vbCrLf & " AND FTOrderNo ='" & HI.UL.ULF.rpQuoted(FOKey) & "' "
        'End If

        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "")
    End Function
    Public Shared Function EqualizeJob(FTReceiveNo As String, FTPurchaseNo As String, _cmd As System.Data.SqlClient.SqlCommand, _Trans As System.Data.SqlClient.SqlTransaction, _TsysMatId As Integer) As Boolean
        Try
            Dim _Str As String
            Dim _dtRcv As DataTable
            Dim _DtPoJob As DataTable
            Dim _TotalJob As Double
            Dim _Rind As Double
            Dim _TotalRcvQty As Double
            Dim _TotalRcvPOQty As Double
            Dim _TotalRcvStockQty As Double
            Dim _JobRcvQty As Double
            Dim _JobRcvPOQty As Double
            Dim _JobPOQty As Double
            Dim _SumRcvBFQty As Double
            Dim _SysUnitStock As Double
            Dim _FNConvRatio As Double = 1
            Dim _RtsQty As Double = 0
            Dim _Exc As Double = 0

            _Exc = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans("SELECt TOP 1 FNExchangeRate FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "0"))
            If _Exc <= 0 Then
                _Exc = 1
            End If
            _SysUnitStock = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans("SELECt TOP 1 FNHSysUnitSectId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE   FNHSysFixedAssetId =" & _TsysMatId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0"))

            _Str = "SELECT TOP 1 FTReceiveNo, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNNetPrice,FNNetAmt, FNQuantity "
            _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail"
            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
            _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & _TsysMatId & " "
            _dtRcv = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Str)

            If _dtRcv.Rows.Count > 0 Then

                If Integer.Parse(_SysUnitStock) <> Integer.Parse(Val(_dtRcv.Rows(0)!FNHSysUnitId.ToString)) Then

                    _Str = " SELECT      TOP 1   Convert(numeric(18,5),FNRateFrom * FNRateTo)  As  FNConvRatio "
                    _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                    _Str &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse(Val(_dtRcv.Rows(0)!FNHSysUnitId.ToString)) & " "
                    _Str &= vbCrLf & "  AND FNHSysUnitIdTo =" & Integer.Parse(_SysUnitStock) & " "



                    _FNConvRatio = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_INVEN, "1"))
                End If

                _Str = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail "
                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & _TsysMatId & " "

                HI.Conn.SQLConn.Execute_Tran(_Str, _cmd, _Trans)

                For Each R As DataRow In _dtRcv.Rows

                    _TotalRcvQty = Val(R!FNQuantity.ToString)
                    _TotalRcvPOQty = _TotalRcvQty
                    _TotalRcvQty = CDbl(Format(_TotalRcvQty * _FNConvRatio, HI.ST.Config.QtyFormat))
                    _TotalRcvStockQty = _TotalRcvQty

                    _Str = "   SELECT  D.FTPurchaseNo,  D.FNHSysFixedAssetId, D.FNQuantity "
                    _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS D "
                    ' _Str &= vbCrLf & " LEFT OUTER JOIN ("
                    '_Str &= vbCrLf & " SELECT        A.FTOrderNo"
                    '_Str &= vbCrLf & " ,(CASE WHEN ISNULL(XX2.FNProcessSortDate,'') ='' THEN ( CASE WHEN A.FNOrderType =6 THEN '0000/00/00' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =7 THEN '0000/00/01' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =8 THEN '0000/00/02' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =9 THEN '0000/00/03' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =10 THEN '0000/00/04' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =11 THEN '0000/00/05' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =12 THEN '0000/00/06' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =14 THEN '0000/00/07' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =13 THEN '0000/00/08' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =1 THEN '0000/00/09' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =5 THEN '0000/00/10' "

                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =3 THEN '9999/00/00' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =4 THEN '9999/00/01' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =2 THEN '9999/00/02' "
                    '_Str &= vbCrLf & "  WHEN A.FNOrderType =15 THEN '9999/00/03' "

                    '_Str &= vbCrLf & " ELSE  ISNULL"
                    '_Str &= vbCrLf & "    ((SELECT        MIN(FDShipDate) AS FDShipDate"
                    '_Str &= vbCrLf & "   FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Su WITH (NOLOCK)"
                    '_Str &= vbCrLf & "   WHERE        (FTOrderNo = A.FTOrderNo)), '') END) ELSE ISNULL(XX2.FNProcessSortDate,'') END) AS FDShipDate"
                    '_Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A  WITH (NOLOCK) "
                    '_Str &= vbCrLf & " LEFT OUTER JOIN  ("
                    '_Str &= vbCrLf & " SELECT  FTListName, FNListIndex AS FNOrderType , FTNameTH, FTNameEN"
                    '_Str &= vbCrLf & " , FTReferCode, FTCallMnuName, FTCallMethodName"
                    '_Str &= vbCrLf & " , FNProcessSortSeq, FNProcessSortDate"
                    '_Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS ZX WITH(NOLOCK)"
                    '_Str &= vbCrLf & " WHERE  (FTListName = N'FNOrderType') "
                    '_Str &= vbCrLf & " ) AS XX2 ON A.FNOrderType=XX2.FNOrderType"
                    '_Str &= vbCrLf & " ) AS O ON D.FTOrderNo = O.FTOrderNo "
                    _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "') "
                    _Str &= vbCrLf & "  AND (D.FNHSysFixedAssetId =" & _TsysMatId & ") "
                    '_Str &= vbCrLf & " ORDER BY  CASE WHEN ISNULL(O.FDShipDate,'' ) ='' THEN '9999/99/99' ELSE ISNULL(O.FDShipDate,'' ) END "
                    _DtPoJob = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Str)

                    If _DtPoJob.Rows.Count > 0 Then

                        _TotalJob = _DtPoJob.Rows.Count

                        _Rind = 0
                        For Each RPO As DataRow In _DtPoJob.Rows

                            _JobPOQty = Val(RPO!FNQuantity.ToString)
                            _JobPOQty = CDbl(Format(_JobPOQty * _FNConvRatio, HI.ST.Config.QtyFormat))

                            _Rind = _Rind + 1

                            If _TotalRcvQty > 0 Then

                                _SumRcvBFQty = 0
                                _JobRcvQty = 0
                                _JobRcvPOQty = 0
                                _RtsQty = 0

                                '-------------------ยอด Return To Supplier
                                _Str = " SELECT TOP 1 Convert(numeric(18," & HI.ST.Config.QtyDigit & " ),SUM(FNQuantity )) AS  RTSFNQuantity"
                                _Str &= vbCrLf & "     FROM"
                                _Str &= vbCrLf & " (SELECT        H.FTPurchaseNo, B.FNHSysFixedAssetId, SUM(BO.FNQuantity) AS FNQuantity" ', RC.FNConvRatio, B.FTOrderNo"
                                _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH(NOLOCK) INNER JOIN"
                                _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH(NOLOCK)  ON B.FTBarcodeNo = BO.FTBarcodeNo INNER JOIN"
                                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RC WITH(NOLOCK) ON B.FTDocumentNo = RC.FTReceiveNo  AND B.FNHSysFixedAssetId = RC.FNHSysFixedAssetId INNER JOIN"
                                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier AS H  WITH (NOLOCK) ON BO.FTDocumentNo = H.FTReturnSuplNo"
                                _Str &= vbCrLf & " WHERE    B.FNHSysFixedAssetId =" & _TsysMatId & " "
                                _Str &= vbCrLf & " AND    H.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "'"
                                '  _Str &= vbCrLf & " AND    B.FTOrderNo ='" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "'"
                                _Str &= vbCrLf & "  GROUP BY H.FTPurchaseNo, B.FNHSysFixedAssetId) AS RTS" ', RC.FNConvRatio, B.FTOrderNo)
                                _RtsQty = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")
                                '-------------------ยอด Return To Supplier

                                '-------------------ยอด รับก่อนหน้า
                                _Str = " SELECT SUM(FNQuantity) As FNQuantity FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail As D ,[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive As H "
                                _Str &= vbCrLf & " WHERE H.FTReceiveNo=D.FTReceiveNo  "
                                _Str &= vbCrLf & " AND  H.FTReceiveNo <>'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                _Str &= vbCrLf & " AND    D.FNHSysFixedAssetId =" & _TsysMatId & " "
                                _Str &= vbCrLf & " AND    H.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(FTPurchaseNo) & "'"
                                ' _Str &= vbCrLf & " AND    D.FTOrderNo ='" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "'"
                                '-------------------ยอด รับก่อนหน้า

                                _SumRcvBFQty = Val(HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_FIXED, "0"))
                                _SumRcvBFQty = CDbl(Format(_SumRcvBFQty * _FNConvRatio, HI.ST.Config.QtyFormat))

                                _SumRcvBFQty = _SumRcvBFQty - _RtsQty

                                If _SumRcvBFQty <= 0 Then
                                    _SumRcvBFQty = 0
                                End If

                                If _Rind = _TotalJob Or (_JobPOQty - _SumRcvBFQty) > 0 Then

                                    If _Rind = _TotalJob Then
                                        _JobRcvQty = _TotalRcvQty
                                    Else
                                        If _JobPOQty - _SumRcvBFQty > 0 Then
                                            If _TotalRcvQty > (_JobPOQty - _SumRcvBFQty) Then
                                                _JobRcvQty = (_JobPOQty - _SumRcvBFQty)
                                            Else
                                                _JobRcvQty = _TotalRcvQty
                                            End If

                                        Else
                                            _JobRcvQty = _TotalRcvQty
                                        End If
                                    End If

                                    _JobRcvPOQty = CDbl(Format((_JobRcvQty * _TotalRcvPOQty) / _TotalRcvStockQty, HI.ST.Config.QtyFormat))

                                    _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail( FTInsUser, FDInsDate, FTInsTime,FTReceiveNo, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, "
                                    _Str &= vbCrLf & " FNNetPrice, FNNetAmt, FNQuantity)" ', FNQuantityStock, FNHSysUnitIdStock, FNPricePerStock, FNConvRatio, FNNetStockAmt,FTFabricFrontSize,FNSurchangePerUnit) "
                                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                                    '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(RPO!FTOrderNo.ToString) & "' "
                                    _Str &= vbCrLf & "," & Val(R!FNHSysFixedAssetId.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNDisPer.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNDisAmt.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(R!FNNetPrice.ToString) & " "
                                    _Str &= vbCrLf & "," & CDbl(Format(Val(R!FNPrice.ToString) * Val(_JobRcvPOQty), HI.ST.Config.AmtFormat)) & " "
                                    _Str &= vbCrLf & "," & _JobRcvPOQty & " "
                                    '_Str &= vbCrLf & "," & _JobRcvQty & " "
                                    ' _Str &= vbCrLf & "," & _SysUnitStock & " "
                                    ' _Str &= vbCrLf & "," & CDbl(Format((Val(R!FNNetPrice.ToString) * _Exc) / Val(_FNConvRatio), HI.ST.Config.PriceFormat)) & " "
                                    '_Str &= vbCrLf & "," & _FNConvRatio & " "
                                    '_Str &= vbCrLf & "," & CDbl(Format(CDbl(Format((Val(R!FNNetPrice.ToString) * _Exc) / Val(_FNConvRatio), HI.ST.Config.PriceFormat)) * Val(_JobRcvQty), HI.ST.Config.AmtFormat)) & " "
                                    '_Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                    '_Str &= vbCrLf & "," & Val(R!FNSurchangePerUnit.ToString) & " "

                                    If HI.Conn.SQLConn.ExecuteTran(_Str, _cmd, _Trans) = False Then
                                        Return False
                                    End If

                                End If

                                _TotalRcvQty = _TotalRcvQty - _JobRcvQty

                            End If
                        Next
                    End If
                Next

            Else

                _Str = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail "
                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & _TsysMatId & " "

                HI.Conn.SQLConn.ExecuteTran(_Str, _cmd, _Trans)

            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

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

    Private Sub ocmmanagercvdetail_Click(sender As Object, e As EventArgs) Handles ocmmanagercvdetail.Click
        If CheckOwner() = False Then Exit Sub


        With ogvmuljob

            If BarcodeAsset.CheckDucumentCreateBar(FTReceiveNo.Text) Then
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

            Dim _AssetID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString
            Dim _Code As String = "" & .GetRowCellValue(.FocusedRowHandle, "aFTAssetCode").ToString
            Dim _aDescription As String = "" & .GetRowCellValue(.FocusedRowHandle, "aFTDescription").ToString
            Dim _MOdel As String = "" & .GetRowCellValue(.FocusedRowHandle, "aModel").ToString
            ' Dim _FTRawMatSizeCode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTRawMatSizeCode").ToString
            '  Dim _FTFabricFrontSize As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTFabricFrontSize").ToString
            Dim CFNQuantity As Double = 0
            Dim _Str As String = ""

            _Str = " SELECT        D.FNHSysFixedAssetId, M.FTAssetCode AS aFTAssetCode ,M.FTProductCode As FTProductNo"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & " , M.FTAssetNameTH AS aFTDescription"
            Else
                _Str &= vbCrLf & " , M.FTAssetNameEN AS aFTDescription"
            End If

            ' _Str &= vbCrLf & "  , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode,D.FTFabricFrontSize"
            _Str &= vbCrLf & "  ,S.FTAssetModelCode AS aModel,M.FTProductCode, D.FNHSysUnitId, U.FTUnitAssetCode as FTUnitCode, D.FNPrice, D.FNDisPer, "
            _Str &= vbCrLf & "   D.FNDisAmt, D.FNNetPrice, D.FNQuantity, D.FNNetAmt"
            '  _Str &= vbCrLf & " ,PD.FTOrderNo"
            _Str &= vbCrLf & " ,CASE WHEN ISDATE(ISnull(O.FDShipDate,'')) = 1 THEN Convert(varchar(10),Convert(datetime,ISnull(O.FDShipDate,'')),103)  ELSE '' END AS FDShipDate "
            _Str &= vbCrLf & " ,ISNULL(ORD.FNUsedQuantity,0) As FNUsedQuantity"
            _Str &= vbCrLf & " ,ISNULL(PD.FNQuantity,0) AS FNPOQuantity"
            _Str &= vbCrLf & "  ,ISNULL(TRC.FNQuantity,0) AS FNTCQuantity"
            _Str &= vbCrLf & "  ,ISNULL(RTS.FNQuantity,0) AS FNRTSQuantity"
            _Str &= vbCrLf & "  ,ISNULL(RD.FNQuantity,0)   AS FNOrderRcvQuantity"
            ' _Str &= vbCrLf & "  ,RC.FNPricePerStock"
            '_Str &= vbCrLf & "  ,RC.FNConvRatio,RC.FNHSysUnitIdStock"
            _Str &= vbCrLf & "    FROM"
            _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FNHSysFixedAssetId, FNQuantity"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail WITH(NOLOCK)"
            _Str &= vbCrLf & "   WHERE FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            _Str &= vbCrLf & " 	 AND FNHSysFixedAssetId=" & Integer.Parse(Val(_AssetID)) & "  "
            _Str &= vbCrLf & "   ) AS PD INNER JOIN (SELECT TOP 1 H.FTReceiveNo,H.FTPurchaseNo"
            _Str &= vbCrLf & "    ,D.FNHSysFixedAssetId, D.FNHSysUnitId"
            _Str &= vbCrLf & " ,D.FNPrice,D.FNDisPer,D.FNDisAmt,D.FNNetPrice"
            _Str &= vbCrLf & " ,D.FNQuantity,D.FNNetAmt" ',D.FNQuantityStock,D.FNHSysUnitIdStock"
            ' _Str &= vbCrLf & " 	,D.FNPricePerStock,D.FNConvRatio,D.FNNetStockAmt"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS H WITH(NOLOCK) "
            _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS D WITH(NOLOCK) ON H.FTReceiveNo = D.FTReceiveNo"
            _Str &= vbCrLf & "  WHERE  (H.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "')	"
            _Str &= vbCrLf & " 	 AND D.FNHSysFixedAssetId=" & Integer.Parse(Val(_AssetID)) & "  "
            _Str &= vbCrLf & " 	) AS RC ON PD.FTPurchaseNo = RC.FTPurchaseNo"

            _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS D WITH (NOLOCK)"
            _Str &= vbCrLf & "  ON PD.FNHSysFixedAssetId =D.FNHSysFixedAssetId AND RC.FTReceiveNo = D.FTReceiveNo "
            _Str &= vbCrLf & "    INNER Join"
            _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS M WITH (NOLOCK) ON D.FNHSysFixedAssetId = M.FNHSysFixedAssetId INNER JOIN"
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitAssetId"
            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS C WITH (NOLOCK) ON PD.FTPurchaseNo = C.FTPurchaseNo"
            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS S WITH (NOLOCK) ON M.FNHSysAssetModelId = S.FNHSysAssetModelId"

            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) ON RC.FTReceiveNo = RD.FTReceiveNo "
            _Str &= vbCrLf & "   AND PD.FNHSysFixedAssetId = RD.FNHSysFixedAssetId" ' AND PD.FTOrderNo = RD.FTOrderNo "

            '_Str &= vbCrLf & "  LEFT OUTER JOIN (SELECT       A.FNHSysCmpRunId,A.FNHSysCmpId, ISNULL"
            '_Str &= vbCrLf & "   ((SELECT        MIN(FDShipDate) AS FDShipDate"
            '_Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Su WITH (NOLOCK)"
            '_Str &= vbCrLf & "  WHERE        (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate"
            '_Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A ) AS O ON C.FNHSysCmpId=O.FNHSysCmpId and C.FNHSysCmpRunId=O.FNHSysCmpId "

            '_Str &= vbCrLf & "  LEFT OUTER JOIN    (SELECT       FNHSysUnitId, SUM((ISNULL(FNUsedQuantity,0) + ISNULL(FNUsedPlusQuantity,0))) AS FNUsedQuantity"
            '_Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS O WITH(NOLOCK)"
            '_Str &= vbCrLf & "   GROUP BY FNHSysUnitId"
            '_Str &= vbCrLf & "  ) AS ORD ON RD.FNHSysUnitId=ORD.FNHSysUnitId "

            _Str &= vbCrLf & "  LEFT OUTER JOIN    ("
            _Str &= vbCrLf & "  SELECT        RH.FTPurchaseNo, RD.FNHSysFixedAssetId, SUM(RD.FNQuantity) AS FNQuantity"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS RH WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
            _Str &= vbCrLf & "   WHERE        (RH.FTReceiveNo <> N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "') "
            _Str &= vbCrLf & "  AND RH.FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            _Str &= vbCrLf & "  GROUP BY RH.FTPurchaseNo, RD.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  ) AS TRC ON PD.FTPurchaseNo = TRC.FTPurchaseNo AND D.FNHSysFixedAssetId = TRC.FNHSysFixedAssetId "
            _Str &= vbCrLf & "  LEFT OUTER JOIN    ("
            _Str &= vbCrLf & "  SELECT         RH.FTPurchaseNo,  B.FNHSysFixedAssetId, SUM(RD.FNQuantity) AS FNQuantity"
            _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier  AS RH WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS RD WITH (NOLOCK) ON RH.FTReturnSuplNo = RD.FTDocumentNo  INNER JOIN "
            _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH (NOLOCK) ON RD.FTBarcodeNo = B.FTBarcodeNo"
            _Str &= vbCrLf & "  GROUP BY RH.FTPurchaseNo, B.FNHSysFixedAssetId"
            _Str &= vbCrLf & "  ) AS RTS ON PD.FTPurchaseNo = RTS.FTPurchaseNo AND D.FNHSysFixedAssetId = RTS.FNHSysFixedAssetId "
            _Str &= vbCrLf & "  AND   D.FNHSysFixedAssetId=" & Integer.Parse(Val(_AssetID)) & "  "
            _Str &= vbCrLf & " ORDER BY M.FTAssetCode" ', C.FTRawMatColorCode, S.FTRawMatSizeCode "
            _Str &= vbCrLf & " ,ISnull(O.FDShipDate,''),ISNULL(ORD.FNUsedQuantity,0)"

            Dim _dt As DataTable
            _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)

            If _dt.Rows.Count > 0 Then
                CFNQuantity = Double.Parse(Val(_dt.Rows(0)!FNQuantity.ToString))
            End If

            HI.TL.HandlerControl.ClearControl(_Multiple)

            With _Multiple
                Call HI.ST.Lang.SP_SETxLanguage(_Multiple)
                .PurchaseNo = Me.FTPurchaseNo.Text
                .ReceiveNo = Me.FTReceiveNo.Text
                .FNHSysFixedAssetId.Text = _Code
                .FNHSysFixedAssetId_None.Text = _aDescription
                .FTAssetModelCode.Text = _MOdel
                ' .TFTRawMatSizeCode.Text = _FTRawMatSizeCode
                ' .TFTFabricFrontSize.Text = _FTFabricFrontSize
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
                            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail "
                            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Str &= vbCrLf & " AND    FNHSysFixedAssetId =" & Integer.Parse(Val(_AssetID)) & " "
                            '  _Str &= vbCrLf & " AND    FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
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
    'Private Sub ocm_Click(sender As System.Object, e As System.EventArgs)

    'Public Sub Barcode(Key As String)
    '    If CheckOwner() = False Then Exit Sub
    '    If FTReceiveNo.Text.Trim <> "" Then
    '        If FTReceiveNo.Properties.Tag.ToString <> "" Then
    '            Dim _Str As String = ""

    '            '  Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)

    '            Call GenBarcode()

    '            LoadBarcode(FTReceiveNo.Text)


    '        End If
    '    End If

    'End Sub
    Public Sub LoadBarcode(Key As String)
        Dim dt As DataTable = ASSET.BarcodeAsset.GetBarcode(Key)
        'Me.ogcbarcode.DataSource = HI.ASSET.BarcodeAsset.GetBarcode(Key)
        If dt.Rows.Count > 0 Then
            Me.ogcbarcode.DataSource = dt
        Else
            Call PrepareListBarcode()
        End If
    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcodegen.Click
        If CheckOwner() = False Then Exit Sub
        Call DeleteBarcodeGen()
    End Sub
    Private Sub DeleteBarcodeGen()
        With ogvbarcode

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _Str As String = ""
            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _BarCode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString

                If BarcodeAsset.CheckTransactionOUT(_BarCode, FTReceiveNo.Text, FNHSysWHAssetId.Properties.Tag.ToString) Then
                    HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text, _BarCode)
                Else
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  "
                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Continue For
                        End If

                        _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  "
                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Continue For
                        End If

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Dim _ID As String = "" : Dim _IDMaster As String = ""
                        _ID = .GetRowCellValue(i, "FNHSysFixedAssetId").ToString
                        _Str = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType<>6 and B.FNFixedAssetType<>7 and B.FNFixedAssetType<>3 and B.FNHSysFixedAssetId=" & _ID & ""
                        _IDMaster = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER)
                        If _IDMaster = "" Then
                            .SetRowCellValue(i, "FTBarcodeNo", "")
                        Else
                            _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId=" & _IDMaster & ""
                            If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_FIXED) Then
                                CType(ogcbarcode.DataSource, DataTable).Rows(i).Delete()
                                CType(ogcbarcode.DataSource, DataTable).AcceptChanges()
                            End If
                        End If


                        HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  ")

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

            'If (_StateDelete) Then
            '    Me.LoadBarcode(Me.FTReceiveNo.Text)
            'End If

        End With
    End Sub
    Public Shared Function CheckTransactionOUT(BarKey As String, DocKey As String, WHKey As String, OrderKey As String) As Boolean
        Dim Str As String

        Str = "SELECT TOP 1 FTBarcodeNo"
        Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT  AS A WITH (NOLOCK)"
        Str &= vbCrLf & "  WHERE FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
        Str &= vbCrLf & "  AND FTDocumentRefNo ='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        If Val(WHKey) > 0 Then
            Str &= vbCrLf & "  AND FNHSysWHId =" & Val(WHKey) & " "
        End If



        Return (HI.Conn.SQLConn.GetField(Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "")
    End Function


    Private Sub ogvbarcode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvbarcode.KeyDown

        Select Case e.KeyCode
            Case Keys.Delete
                If CheckOwner() = False Then Exit Sub
                Call DeleteBarcodeGen()
            Case Keys.Enter
                With ogvbarcode
                    If .FocusedColumn.FieldName = "FTBarcodeNo" Then
                        _EditCell = True
                        SaveBarcode()
                        .FocusedRowHandle = .FocusedRowHandle + 1
                        .FocusedColumn = .VisibleColumns(0)
                        .UnselectRow(.FocusedRowHandle - 1)
                        .SelectRow(.FocusedRowHandle)
                    End If
                End With


        End Select
    End Sub

    Private Function Barcode() As Boolean
        Dim _Str As String = ""
        Dim FTDocumentNo As String = Me.FTReceiveNo.Text
        Dim _dt As DataTable
        Dim _AssCode As String = ""
        Dim _Newbargen As String = ""
        Dim _PO As String = HI.Conn.SQLConn.GetField("Select P.FTPurchaseNo,P.FNFixedAssetType from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase As P INNER JOIN (Select FTListName,FNListIndex ,FTNameTH from (Select  FTListName,FNListIndex ,FTNameTH  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L where FTListName='FNFixedAssetType')AS A Where   FNListIndex='0'  or FNListIndex='3' or FNListIndex='5'  )AS L ON P.FNFixedAssetType=L.FNListIndex where P.FTPurchaseNo='" & Me.FTPurchaseNo.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")



        _Str = "SELECT  (CASE WHEN ISNULL(FNQuantity,0) > 0 THEN '1' ELSE '0' END) AS FTStateSelect,C.FTAssetCode,C.FTSerialNo,R.FTReceiveNo AS FTDocumentNo ,R.FTPurchaseNo,RD.FNHSysFixedAssetId,R.FNHSysWHAssetId,RD.FNHSysUnitId AS FNHSysUnitIdStock"
        _Str &= vbCrLf & ",RD.FNPrice AS FNPricePerStock ,RD.FNQuantity AS FNQuantityStock 	 "
        _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive  AS  R WITH(NOLOCK) ON RD.FTReceiveNo=R.FTReceiveNo INNER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS C WITh(NOLOCK) ON RD.FNHSysFixedAssetId=C.FNHSysFixedAssetId"
        _Str &= vbCrLf & "  WHERE R.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTDocumentNo) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)

        With DirectCast(ogcbarcode.DataSource, DataTable)
            .AcceptChanges()
            _dt = .Copy
        End With


        Try


            For Each R As DataRow In _dt.Rows '.Select(" FTStateSelect='1' ")

                Try

                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                    '    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_FIXED, "") = "" Then

                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode SET "
                    _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ",FTBarcodeNo= '" & R!FTBarcodeNo.ToString & "' "
                    _Str &= vbCrLf & " WHERE   FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "

                    '   End If
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return True
                    End If



                    _Str = "SELECT  (CASE WHEN ISNULL(FNQuantity,0) > 0 THEN '1' ELSE '0' END) AS FTStateSelect,C.FTAssetCode,C.FTSerialNo,R.FTReceiveNo AS FTDocumentNo ,R.FTPurchaseNo,RD.FNHSysFixedAssetId,R.FNHSysWHAssetId,RD.FNHSysUnitId AS FNHSysUnitIdStock"
                    _Str &= vbCrLf & ",RD.FNPrice AS FNPricePerStock ,RD.FNQuantity AS FNQuantityStock 	 "
                    _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) INNER JOIN"
                    _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive  AS  R WITH(NOLOCK) ON RD.FTReceiveNo=R.FTReceiveNo INNER JOIN"
                    _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS C WITh(NOLOCK) ON RD.FNHSysFixedAssetId=C.FNHSysFixedAssetId"
                    _Str &= vbCrLf & "  WHERE R.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTDocumentNo) & "'"


                    '  If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, HI.Conn.DB.DataBaseName.DB_FIXED, "") = "" Then

                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN SET "
                    _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ",FTBarcodeNo='" & R!FTBarcodeNo.ToString & "' "
                    _Str &= vbCrLf & " WHERE   FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "


                    '  End If
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return True
                    End If

                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET "
                    _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & ",FTSerialNo='" & R!FTBarcodeNo.ToString & "' "
                    _Str &= vbCrLf & " WHERE   FTAssetCode ='" & R!FTAssetCode.ToString & "' "
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return True
                    End If
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return True
                End Try
                ' End If

            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmaddmaster_Click(sender As Object, e As EventArgs) Handles ocmaddmaster.Click
        If VerifyInsertBarcode() Then
            Dim Qry As String = ""
            Dim dt As DataTable
            Dim dtTemp As New DataTable("DatatableTemp")
            Dim _IDAsset As String = ""
            Dim _CodeConfig As String = ""
            Dim _RunNumber As String = ""
            Dim _ColCount As Integer = 0
            Dim _PicByte As Byte() = Nothing
            Dim _Arridx As Integer = 0
            Dim _Price As Double = 0

            Dim _Qry As String = ""
            Dim _FTempcode As String = ""
            Dim _FTUnitSec As String = ""
            Dim _FNHSysEmpID As Integer = 0
            Dim _FNHSysUnitSectID As Integer = 0


            '_Qry = "   SELECT TOP 1 E.FTEmpCode AS FTEmpCode,E.FNHSysEmpID AS FNHSysEmpID,U.FNHSysUnitSectId AS FNHSysUnitSectId,U.FTUnitSectCode AS FTUnitSectCode "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) ON A.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN"
            '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH(NOLOCK) ON E.FNHSysUnitSectId = U.FNHSysUnitSectID"
            '_Qry &= vbCrLf & "  WHERE A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            'dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

            'If dt.Rows.Count > 0 Then
            '    For Each R As DataRow In dt.Rows
            '        _FTempcode = R!FTEmpCode
            '        _FTUnitSec = R!FTUnitSectCode
            '        _FNHSysEmpID = R!FNHSysEmpId
            '        _FNHSysUnitSectID = R!FNHSysUnitSectId
            '    Next
            'End If

            Try
                With CType(ogcdetail.DataSource, DataTable)
                    If .Rows.Count > 0 Then
                        Qry = "select A.FTReceiveNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTReceiveNo='" & Me.FTReceiveNo.Text & "'"
                        If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "" Then
                            For i As Integer = 0 To .Rows.Count - 1 Step 1
                                _IDAsset = ogvdetail.GetRowCellValue(i, "FNHSysFixedAssetId").ToString
                                _Price = ogvdetail.GetRowCellValue(i, "FNPrice")
                                Qry = "select B.FTAssetBrandNameTH as FTAssetBrandName"
                                Qry &= vbCrLf & ",M.FTAssetModelNameTH as FTAssetModelName,List.FTNameTH AS FTTypeName,'' AS FTSerialNo ,'' as FTProductCode"
                                Qry &= vbCrLf & ",'" & FDReceiveDate.Text & "'as FDDateUsed,0 as FNLifetime"
                                Qry &= vbCrLf & ",'" & FDReceiveDate.Text & "' AS FDDateStartWarranty,'" & FDReceiveDate.Text & "' as FDDateEndWarranty"
                                Qry &= vbCrLf & ",'" & _FTempcode & "' as FNHSysEmpID"
                                Qry &= vbCrLf & ",'" & _FTUnitSec & "' as FNHSysUnitSectId,'' AS FNHSysSuplId"
                                Qry &= vbCrLf & ",'" & Me.FTPurchaseNo.Text & "' as FTPurchaseNo,'" & Me.FDPurchaseDate.Text & "' as FDPurchaseDate,'" & Me.FTPurchaseBy.Text & "' as FTPurchaseBy"
                                Qry &= vbCrLf & ",'" & Me.FTReceiveNo.Text & "' as FTReceiveNo ,'" & FDReceiveDate.Text & "' as FDReceiveDate,'" & Me.FTReceiveBy.Text & "' as FTReceiveBy"
                                Qry &= vbCrLf & ",'" & Me.FTInvoiceNo.Text & "' as FTInvoiceNo,'" & Me.FDInvoiceDate.Text & "' as FDInvoiceDate"
                                Qry &= vbCrLf & ",1 as FNMaximumStock,1 as FNMinimumStock"
                                Qry &= vbCrLf & ",'' as FTRemark"

                                Qry &= vbCrLf & ",A.FTAssetNameTH,A.FTAssetNameEN"
                                Qry &= vbCrLf & ",A.FNHSysAssetTyped"
                                Qry &= vbCrLf & "," & _Price & " AS FNPrice,A.FNHSysCurId"
                                Qry &= vbCrLf & ",A.FNMaxPower,A.FNLifetimeType,'1' as FTStateActive"
                                Qry &= vbCrLf & "," & _FNHSysEmpID & " as HFNHSysEmpID," & _FNHSysUnitSectID & " as HFNHSysUnitSectId," & Me.FNHSysSuplId.Properties.Tag & " as HFNHSysSuplId"

                                Qry &= vbCrLf & ",A.FNHSysAssetBrandId,A.FNFixedAssetType,A.FNHSysAssetGrpId"
                                Qry &= vbCrLf & ",Grp.FTAssetGrpCode,T.FTAssetTypeCode"
                                Qry &= vbCrLf & ",A.FNHSysAssetModelId,A.FPImage,A.FTStateCritical"

                                Qry &= vbCrLf & "," & ogvdetail.GetRowCellValue(i, "FNHSysUnitId") & " As FNHSysUnitId"
                                Qry &= vbCrLf & "," & _IDAsset & " AS FNHSysFixedAssetId"

                                Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As A With(NOLOCK) LEFT OUtER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand As B With(NOLOCK) On A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUtER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel As M With(NOLOCK) On A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As AT With(NOLOCK) On A.FNHSysAssetTyped=AT.FNHSysAssetTyped LEFT OUTER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U With(NOLOCK) On A.FNHSysUnitSectId=U.FNHSysUnitSectId LEFT OUtER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetGrp As Grp With(NOLOCK) On A.FNHSysAssetGrpId=Grp.FNHSysAssetGrpId LEFT OUTER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As T With(NOLOCK) On A.FNHSysAssetTyped=T.FNHSysAssetTyped LEFT OUTER jOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As Cur With(NOLOCK) On A.FNHSysCurId=Cur.FNHSysCurId"
                                Qry &= vbCrLf & "LEFT OUTER JOIN (Select A.FNListIndex,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As A With(NOLOCK)"
                                Qry &= vbCrLf & "where A.FTListName='FNFixedAssetType') AS List ON A.FNFixedAssetType=List.FNListIndex"
                                Qry &= vbCrLf & "where A.FNFixedAssetType<>6 and  A.FNFixedAssetType<>7 and  A.FNFixedAssetType<>3 and A.FNHSysFixedAssetId=" & Val(_IDAsset) & ""

                                dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
                                If dt.Rows.Count > 0 Then

                                    If dtTemp.Columns.Count = 0 Then
                                        For Each R As DataRow In dt.Rows
                                            For Each C As DataColumn In dt.Columns
                                                dtTemp.Columns.Add(C.ColumnName)
                                                _ColCount += 1
                                                dtTemp.AcceptChanges()
                                            Next
                                        Next
                                        Dim _ArrRow(_ColCount - 1)
                                        For k As Integer = 1 To ogvdetail.GetRowCellValue(i, "FNQuantity") Step 1
                                            For Each R As DataRow In dt.Rows
                                                If R!FPImage Is DBNull.Value Then
                                                    _PicByte = Nothing
                                                Else
                                                    _PicByte = R!FPImage
                                                End If
                                                For Each C As DataColumn In dt.Columns
                                                    _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                    _Arridx += 1
                                                Next
                                                _Arridx = 0
                                                dtTemp.Rows.Add(_ArrRow)
                                                dtTemp.AcceptChanges()
                                            Next
                                        Next
                                    Else
                                        Dim _ArrRow(_ColCount - 1)
                                        For k As Integer = 1 To ogvdetail.GetRowCellValue(i, "FNQuantity") Step 1
                                            For Each R As DataRow In dt.Rows
                                                If R!FPImage Is DBNull.Value Then
                                                    _PicByte = Nothing
                                                Else
                                                    _PicByte = R!FPImage
                                                End If

                                                For Each C As DataColumn In dt.Columns
                                                    _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                    _Arridx += 1
                                                Next
                                                _Arridx = 0
                                                dtTemp.Rows.Add(_ArrRow)
                                                dtTemp.AcceptChanges()
                                            Next
                                        Next
                                    End If
                                End If
                            Next
                            If dtTemp.Rows.Count > 0 Then
                                TL.HandlerControl.ClearControl(_AddMasterpop)
                                With _AddMasterpop
                                    .Confirm = False
                                    If _InitialPopupMaster = False Then
                                        With .ogv
                                            .Columns.ColumnByName("FTPurchaseNo").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FDPurchaseDate").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTPurchaseBy").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTReceiveNo").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FDReceiveDate").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTReceiveBy").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTInvoiceNo").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FDInvoiceDate").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("cFNHSysSuplId").VisibleIndex = -1
                                            .Columns.ColumnByName("FNPrice").OptionsColumn.AllowEdit = False
                                        End With
                                        TL.HandlerControl.AddHandlerObj(_AddMasterpop)
                                        _InitialPopupMaster = True
                                    End If

                                    .ogc.DataSource = dtTemp
                                    .ShowDialog()
                                    If .Confirm Then
                                        'insert data to table assetmaster
                                        Try
                                            For Each R As DataRow In CType(.ogc.DataSource, DataTable).Select("FTSerialNo<>''")
                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                                                _CodeConfig = HI.Conn.SQLConn.GetField("select L.FTReferCode   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and FNListIndex = " & R!FNFixedAssetType & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                                '_IDAsset = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TASMAsset", "FNHSysFixedAssetId", Conn.DB.DataBaseName.DB_MASTER)
                                                _IDAsset = R!FNHSYSFixedAssetId
                                                _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTAssetCode) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTAssetCode like '%" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & "%'", Conn.DB.DataBaseName.DB_MASTER)
                                                If _RunNumber = "" Then
                                                    _RunNumber = "00001"
                                                Else
                                                    _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 5)
                                                    _RunNumber = Format(Val(_RunNumber) + 1, "00000")
                                                End If
                                                If R!FTSerialNo = Nothing Then
                                                    Exit For
                                                End If
                                                Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FTSerialNo = '" & R!FTSerialNo & "'"
                                                If HI.Conn.SQLConn.GetField(Qry, HI.Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                                                    MG.ShowMsg.mInfo("ไม่สามารถทำรายการได้ เนื่องจากมีเลขอนุกรมซ้ำ", 201709200904, Me.Text, " SerialNo " + R!FTSerialNo + " ", MessageBoxIcon.Information)
                                                    Exit For
                                                End If

                                                '' insert in master
                                                'Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset"
                                                'Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime, FNHSysFixedAssetId, FNHSysCmpId, FNFixedAssetType, FTAssetCode, FTAssetNameTH, FTAssetNameEN"
                                                'Qry &= vbCrLf & ",FNHSysAssetModelId, FNHSysAssetBrandId, FTSerialNo, FNHSysAssetGrpId, FNHSysAssetTyped, FTProductCode, FNHSysSuplId, FNPrice, FDDateAdd, FDDateUsed, FNLifetime, FNLifetimeType"
                                                'Qry &= vbCrLf & ",FDDateStartWarranty, FDDateEndWarranty, FNMaxPower, FNHSysUnitSectId, FNHSysEmpID, FNHSysCurId, FTPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTInvoiceNo, FDInvoiceDate, FTReceiveNo"
                                                'Qry &= vbCrLf & ",FDReceiveDate, FTReceiveBy, FNMinimumStock, FNMaximumStock, FTRemark, FTStateActive, FTStateCritical, FNHSysUnitAssetId)"
                                                'Qry &= vbCrLf & "Select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                'Qry &= vbCrLf & "," & Val(_IDAsset) & "," & ST.SysInfo.CmpID & "," & R!FNFixedAssetType & ",'" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & _RunNumber & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTAssetNameTH.ToString & "','" & R!FTAssetNameEN.ToString & "'," & R!FNHSysAssetModelId & "," & R!FNHSysAssetBrandId & ""
                                                'Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "'," & R!FNHSysAssetGrpId & "," & R!FNHSysAssetTyped & ",'" & R!FTProductCode.ToString & "'"
                                                ''Qry &= vbCrLf & "," & R!HFNHSysSuplId & "," & R!FNPrice & ",'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"
                                                'If R!FNPrice = Nothing Then
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & ",0,'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"

                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & "," & R!FNPrice & ",'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"

                                                'End If
                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateUsed.ToString) & "'," & R!FNLifetime & ",'" & R!FNLifetimeType & "','" & UL.ULDate.ConvertEnDB(R!FDDateStartWarranty.ToString) & "'"
                                                ''Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "'," & R!FNMaxPower & "," & R!HFNHSysUnitSectId & "," & R!HFNHSysEmpID & ""

                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "','" & R!FNMaxPower & "'"
                                                'If R!HFNHSysUnitSectId = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysUnitSectId & ""
                                                'End If
                                                'If R!HFNHSysEmpID = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysEmpID & ""
                                                'End If

                                                'Qry &= vbCrLf & "," & R!FNHSysCurId & ",'" & R!FTPurchaseNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDPurchaseDate.ToString) & "','" & R!FTPurchaseBy.ToString & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
                                                'Qry &= vbCrLf & ",'" & Me.FTReceiveNo.Text & "','" & UL.ULDate.ConvertEnDB(R!FDReceiveDate.ToString) & "','" & R!FTReceiveBy.ToString & "'," & R!FNMinimumStock & "," & R!FNMaximumStock & ""
                                                'Qry &= vbCrLf & ",'" & R!FTRemark.ToString & "','" & R!FTStateActive.ToString & "','" & R!FTStateCritical.ToString & "', " & R!FNHSysUnitId & ""

                                                'If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                '    HI.Conn.SQLConn.Tran.Rollback()
                                                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                '    Exit Sub
                                                'End If

                                                '' end insert in master

                                                ' insert barcode
                                                Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                                                Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & Val(_IDAsset) & "," & R!FNHSysUnitId & ""
                                                Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & 1 & ",'" & Me.FTReceiveNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                End If
                                                'insert barcode IN
                                                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "','" & Me.FTReceiveNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & 1 & ",'" & Me.FTReceiveNo.Text & "'," & ST.SysInfo.CmpID & ""

                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Exit Sub
                                                End If
                                                HI.Conn.SQLConn.Tran.Commit()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                Qry = "update[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET FPImage=@FPImage where FNHSysFixedAssetId=@ID"
                                                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                                                cmd.Parameters.AddWithValue("@ID", Val(_IDAsset))

                                                Dim p As New SqlParameter("@FPImage", SqlDbType.Image)

                                                If _PicByte Is Nothing Then
                                                    cmd.Parameters.Add(p).Value = DBNull.Value
                                                Else
                                                    cmd.Parameters.Add(p).Value = _PicByte
                                                End If
                                                cmd.ExecuteNonQuery()
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
                                            Next
                                            LoadBarcode(Me.FTReceiveNo.Text)
                                        Catch ex As Exception
                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                            MsgBox(ex.Message)
                                            Exit Sub
                                        End Try
                                    End If
                                End With
                            Else
                                MG.ShowMsg.mInfo("ไม่พบรายการสินทรัพย์ที่เป็นประเภทอื่น กรุณาตรวจสอบ", 201701091108, Me.Text, "", MessageBoxIcon.Information)
                            End If
                        Else
                            'check amount Asset when delete from transaction and retry add item
                            Dim _QtyAssetList As Integer = 0 : Dim _QtyDB As Integer = 0 : Dim _QtyDiffer As Integer = 0
                            For k As Integer = 0 To .Rows.Count - 1 Step 1
                                _IDAsset = ogvdetail.GetRowCellValue(k, "FNHSysFixedAssetId").ToString
                                _Price = ogvdetail.GetRowCellValue(k, "FNPrice")
                                Qry = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType<>6 and B.FNFixedAssetType <>7 AND B.FNFixedAssetType<>3 and B.FNHSysFixedAssetId=" & _IDAsset & ""

                                If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                                    _QtyAssetList = ogvdetail.GetRowCellValue(k, "FNQuantity")

                                    Qry = "select count(A.FNHSysFixedAssetId) FROM HITECH_MASTER.dbo.TASMAsset AS A WITH(NOLOCK) "
                                    Qry &= vbCrLf & "where A.FNHSysAssetBrandId=" & ogvdetail.GetRowCellValue(k, "FNHSysAssetBrandId") & " and A.FNHSysAssetModelId=" & ogvdetail.GetRowCellValue(k, "FNHSysAssetModelId") & ""
                                    Qry &= vbCrLf & "and A.FTReceiveNo='" & Me.FTReceiveNo.Text & "' and A.FTPurchaseNo='" & Me.FTPurchaseNo.Text & "' and A.FNFixedAssetType<>6 and A.FNfixedAssetType<>7 and A.FNfixedAssetType<>3"
                                    _QtyDB = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
                                    _QtyDiffer = _QtyAssetList - _QtyDB

                                    If _QtyDiffer > 0 Then
                                        Qry = "select B.FTAssetBrandNameTH as FTAssetBrandName"
                                        Qry &= vbCrLf & ",M.FTAssetModelNameTH as FTAssetModelName,List.FTNameTH AS FTTypeName,'' AS FTSerialNo ,'' as FTProductCode"
                                        Qry &= vbCrLf & ",'" & FDReceiveDate.Text & "'as FDDateUsed,0 as FNLifetime"
                                        Qry &= vbCrLf & ",'" & FDReceiveDate.Text & "' AS FDDateStartWarranty,'" & FDReceiveDate.Text & "' as FDDateEndWarranty"
                                        Qry &= vbCrLf & ",'" & _FTempcode & "' as FNHSysEmpID"
                                        Qry &= vbCrLf & ",'" & _FTUnitSec & "' as FNHSysUnitSectId,'' AS FNHSysSuplId"
                                        Qry &= vbCrLf & ",'" & Me.FTPurchaseNo.Text & "' as FTPurchaseNo,'" & Me.FDPurchaseDate.Text & "' as FDPurchaseDate,'" & Me.FTPurchaseBy.Text & "' as FTPurchaseBy"
                                        Qry &= vbCrLf & ",'" & Me.FTReceiveNo.Text & "' as FTReceiveNo ,'" & FDReceiveDate.Text & "' as FDReceiveDate,'" & Me.FTReceiveBy.Text & "' as FTReceiveBy"
                                        Qry &= vbCrLf & ",'" & Me.FTInvoiceNo.Text & "' as FTInvoiceNo,'" & Me.FDInvoiceDate.Text & "' as FDInvoiceDate"
                                        Qry &= vbCrLf & ",1 as FNMaximumStock,1 as FNMinimumStock"
                                        Qry &= vbCrLf & ",'' as FTRemark"

                                        Qry &= vbCrLf & ",A.FTAssetNameTH,A.FTAssetNameEN"
                                        Qry &= vbCrLf & ",A.FNHSysAssetTyped"
                                        Qry &= vbCrLf & "," & _Price & " AS FNPrice,A.FNHSysCurId"
                                        Qry &= vbCrLf & ",A.FNMaxPower,A.FNLifetimeType,'1' as FTStateActive"
                                        Qry &= vbCrLf & "," & _FNHSysEmpID & " as HFNHSysEmpID," & _FNHSysUnitSectID & " as HFNHSysUnitSectId," & Me.FNHSysSuplId.Properties.Tag & " as HFNHSysSuplId"

                                        Qry &= vbCrLf & ",A.FNHSysAssetBrandId,A.FNFixedAssetType,A.FNHSysAssetGrpId"
                                        Qry &= vbCrLf & ",Grp.FTAssetGrpCode,T.FTAssetTypeCode"
                                        Qry &= vbCrLf & ",A.FNHSysAssetModelId,A.FPImage,A.FTStateCritical"

                                        Qry &= vbCrLf & "," & ogvdetail.GetRowCellValue(k, "FNHSysUnitId") & " As FNHSysUnitId"
                                        Qry &= vbCrLf & "," & _IDAsset & " AS FNHSysFixedAssetId"

                                        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As A With(NOLOCK) LEFT OUtER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand As B With(NOLOCK) On A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUtER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel As M With(NOLOCK) On A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As AT With(NOLOCK) On A.FNHSysAssetTyped=AT.FNHSysAssetTyped LEFT OUTER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U With(NOLOCK) On A.FNHSysUnitSectId=U.FNHSysUnitSectId LEFT OUtER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetGrp As Grp With(NOLOCK) On A.FNHSysAssetGrpId=Grp.FNHSysAssetGrpId LEFT OUTER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As T With(NOLOCK) On A.FNHSysAssetTyped=T.FNHSysAssetTyped LEFT OUTER jOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As Cur With(NOLOCK) On A.FNHSysCurId=Cur.FNHSysCurId"
                                        Qry &= vbCrLf & "LEFT OUTER JOIN (Select A.FNListIndex,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As A With(NOLOCK)"
                                        Qry &= vbCrLf & "where A.FTListName='FNFixedAssetType') AS List ON A.FNFixedAssetType=List.FNListIndex"
                                        Qry &= vbCrLf & "where A.FNFixedAssetType<>6 and A.FNFixedAssetType<>7  and A.FNfixedAssetType<>3 and A.FNHSysFixedAssetId=" & Val(_IDAsset) & ""
                                        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)

                                        If dt.Rows.Count > 0 Then

                                            If dtTemp.Columns.Count = 0 Then
                                                For Each R As DataRow In dt.Rows
                                                    For Each C As DataColumn In dt.Columns
                                                        dtTemp.Columns.Add(C.ColumnName)
                                                        _ColCount += 1
                                                        dtTemp.AcceptChanges()
                                                    Next
                                                Next
                                                Dim _ArrRow(_ColCount - 1)
                                                For p As Integer = 1 To _QtyDiffer Step 1
                                                    For Each R As DataRow In dt.Rows
                                                        If R!FPImage Is DBNull.Value Then
                                                            _PicByte = Nothing
                                                        Else
                                                            _PicByte = R!FPImage
                                                        End If
                                                        For Each C As DataColumn In dt.Columns
                                                            _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                            _Arridx += 1
                                                        Next
                                                        _Arridx = 0
                                                        dtTemp.Rows.Add(_ArrRow)
                                                        dtTemp.AcceptChanges()
                                                    Next
                                                Next
                                            Else
                                                Dim _ArrRow(_ColCount - 1)
                                                For p As Integer = 1 To _QtyDiffer Step 1
                                                    For Each R As DataRow In dt.Rows
                                                        If R!FPImage Is DBNull.Value Then
                                                            _PicByte = Nothing
                                                        Else
                                                            _PicByte = R!FPImage
                                                        End If

                                                        For Each C As DataColumn In dt.Columns
                                                            _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                            _Arridx += 1
                                                        Next
                                                        _Arridx = 0
                                                        dtTemp.Rows.Add(_ArrRow)
                                                        dtTemp.AcceptChanges()
                                                    Next
                                                Next
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                            If dtTemp.Rows.Count > 0 Then
                                TL.HandlerControl.ClearControl(_AddMasterpop)
                                With _AddMasterpop
                                    .Confirm = False
                                    If _InitialPopupMaster = False Then
                                        With .ogv
                                            .Columns.ColumnByName("FTPurchaseNo").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FDPurchaseDate").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTPurchaseBy").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTReceiveNo").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FDReceiveDate").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTReceiveBy").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FTInvoiceNo").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("FDInvoiceDate").OptionsColumn.AllowEdit = False
                                            .Columns.ColumnByName("cFNHSysSuplId").VisibleIndex = -1
                                        End With
                                        TL.HandlerControl.AddHandlerObj(_AddMasterpop)
                                        _InitialPopupMaster = True
                                    End If
                                    .ogc.DataSource = dtTemp
                                    .ShowDialog()
                                    If .Confirm Then
                                        'insert data to table assetmaster

                                        Try
                                            '   For Each R As DataRow In CType(.ogc.DataSource, DataTable).Rows
                                            For Each R As DataRow In CType(.ogc.DataSource, DataTable).Select("FTSerialNo<>''")
                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                                                _CodeConfig = HI.Conn.SQLConn.GetField("select L.FTReferCode   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and FNListIndex='" & R!FNFixedAssetType & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                                '_IDAsset = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TASMAsset", "FNHSysFixedAssetId", Conn.DB.DataBaseName.DB_MASTER)
                                                _IDAsset = R!FNHSYSFixedAssetId
                                                _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTAssetCode) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTAssetCode like '%" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & "%'", Conn.DB.DataBaseName.DB_MASTER)

                                                If _RunNumber = "" Then
                                                    _RunNumber = "00001"
                                                Else
                                                    _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 5)
                                                    _RunNumber = Format(Val(_RunNumber) + 1, "00000")
                                                End If

                                                '' insert in master

                                                'If R!FTSerialNo = Nothing Then
                                                '    Exit For
                                                'End If
                                                'Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FTSerialNo = '" & R!FTSerialNo & "'"
                                                'If HI.Conn.SQLConn.GetField(Qry, HI.Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                                                '    MG.ShowMsg.mInfo("ไม่สามารถทำรายการได้ เนื่องจากมีเลขอนุกรมซ้ำ", 201709200904, Me.Text, " SerialNo " + R!FTSerialNo + " ", MessageBoxIcon.Information)
                                                '    Exit For
                                                'End If
                                                'Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset"
                                                'Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime, FNHSysFixedAssetId, FNHSysCmpId, FNFixedAssetType, FTAssetCode, FTAssetNameTH, FTAssetNameEN"
                                                'Qry &= vbCrLf & ",FNHSysAssetModelId, FNHSysAssetBrandId, FTSerialNo, FNHSysAssetGrpId, FNHSysAssetTyped, FTProductCode, FNHSysSuplId, FNPrice, FDDateAdd, FDDateUsed, FNLifetime, FNLifetimeType"
                                                'Qry &= vbCrLf & ",FDDateStartWarranty, FDDateEndWarranty, FNMaxPower, FNHSysUnitSectId, FNHSysEmpID, FNHSysCurId, FTPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTInvoiceNo, FDInvoiceDate, FTReceiveNo"
                                                'Qry &= vbCrLf & ",FDReceiveDate, FTReceiveBy, FNMinimumStock, FNMaximumStock, FTRemark, FTStateActive, FTStateCritical, FNHSysUnitAssetId)"
                                                'Qry &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                'Qry &= vbCrLf & "," & Val(_IDAsset) & "," & ST.SysInfo.CmpID & "," & R!FNFixedAssetType & ",'" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & _RunNumber & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTAssetNameTH.ToString & "','" & R!FTAssetNameEN.ToString & "'," & R!FNHSysAssetModelId & "," & R!FNHSysAssetBrandId & ""
                                                'Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "'," & R!FNHSysAssetGrpId & "," & R!FNHSysAssetTyped & ",'" & R!FTProductCode.ToString & "'"
                                                'If R!FNPrice = Nothing Then
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & ",0,'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"

                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & "," & R!FNPrice & ",'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"

                                                'End If

                                                ''For Each P As DataRow In CType(ogcdetail.DataSource, DataTable).Select("FNHSysFixedAssetId='" & R!BFNHSysFixedAssetId & "'")

                                                ''Next
                                                ''Qry &= vbCrLf & "," & R!HFNHSysSuplId & "," & R!FNPrice & ",'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"
                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateUsed.ToString) & "'," & R!FNLifetime & ",'" & R!FNLifetimeType & "','" & UL.ULDate.ConvertEnDB(R!FDDateStartWarranty.ToString) & "'"
                                                ''Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "'," & R!FNMaxPower & "," & R!HFNHSysUnitSectId & "," & R!HFNHSysEmpID & ""
                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "','" & R!FNMaxPower & "'"
                                                'If R!HFNHSysUnitSectId = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysUnitSectId & ""
                                                'End If
                                                'If R!HFNHSysEmpID = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysEmpID & ""
                                                'End If
                                                'Qry &= vbCrLf & "," & R!FNHSysCurId & ",'" & R!FTPurchaseNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDPurchaseDate.ToString) & "','" & R!FTPurchaseBy.ToString & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
                                                'Qry &= vbCrLf & ",'" & Me.FTReceiveNo.Text & "','" & UL.ULDate.ConvertEnDB(R!FDReceiveDate.ToString) & "','" & R!FTReceiveBy.ToString & "'," & R!FNMinimumStock & "," & R!FNMaximumStock & ""
                                                'Qry &= vbCrLf & ",'" & R!FTRemark.ToString & "','" & R!FTStateActive.ToString & "','" & R!FTStateCritical.ToString & "', " & R!FNHSysUnitId & ""

                                                'If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                '    HI.Conn.SQLConn.Tran.Rollback()
                                                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                '    Exit Sub
                                                'End If

                                                '' end insert in master

                                                ' insert barcode
                                                Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                                                Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & Val(_IDAsset) & "," & R!FNHSysUnitId & ""
                                                Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & 1 & ",'" & Me.FTReceiveNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                End If
                                                'insert barcode IN
                                                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "','" & Me.FTReceiveNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & 1 & ",'" & Me.FTReceiveNo.Text & "'," & ST.SysInfo.CmpID & ""

                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Exit Sub
                                                End If
                                                HI.Conn.SQLConn.Tran.Commit()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                Qry = "update[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET FPImage=@FPImage where FNHSysFixedAssetId=@ID"
                                                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                                                cmd.Parameters.AddWithValue("@ID", Val(_IDAsset))

                                                Dim p As New SqlParameter("@FPImage", SqlDbType.Image)

                                                If _PicByte Is Nothing Then
                                                    cmd.Parameters.Add(p).Value = DBNull.Value
                                                Else
                                                    cmd.Parameters.Add(p).Value = _PicByte
                                                End If
                                                cmd.ExecuteNonQuery()
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
                                            Next
                                            LoadBarcode(Me.FTReceiveNo.Text)
                                        Catch ex As Exception
                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                            MsgBox(ex.Message)
                                            Exit Sub
                                        End Try
                                    End If
                                End With
                            Else
                                MG.ShowMsg.mInfo("ไม่สามารถทำรายการได้ เนื่องจากมีรายการนี้อยู่แล้ว", 201701121621, Me.Text, "", MessageBoxIcon.Information)
                            End If
                        End If
                    Else
                        MG.ShowMsg.mInfo("กรุณาเพิ่มรายการก่อน", 201701121617, Me.Text, "", MessageBoxIcon.Information)
                    End If
                End With

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MG.ShowMsg.mInfo("กรุณาบันทึกข้อมูลบาร์โค๊ดก่อน !!!", 201701191508, Me.Text, "", MessageBoxIcon.Warning)
            oxtb.SelectedTabPageIndex = 2
        End If


    End Sub

    Private Sub ogvbarcode_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles ogvbarcode.FocusedRowChanged
        Try
            With ogvbarcode
                If .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo").ToString = "" And .FocusedColumn.FieldName = "FTBarcodeNo" Then
                    Me.FTBarcodeNo.OptionsColumn.AllowEdit = True
                Else
                    Dim Qry As String = "" : Dim _val As String = ""
                    _val = .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString
                    Qry = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType<>6 and B.FNFixedAssetType<>7 and B.FNFixedAssetType<>3 and B.FNHSysFixedAssetId=" & _val & ""

                    If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) = "" Then
                        Me.FTBarcodeNo.OptionsColumn.AllowEdit = True
                    Else
                        Me.FTBarcodeNo.OptionsColumn.AllowEdit = False
                    End If
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    'check insert Barcode except of mechanism
    Private Function VerifyInsertBarcode()
        Dim _Pass As Boolean = False
        Try
            If CType(ogcbarcode.DataSource, DataTable).Rows.Count > 0 Then
                For Each R As DataRow In CType(ogcbarcode.DataSource, DataTable).Rows
                    If R!FTBarcodeNo.ToString <> "" Then
                        _Pass = True
                    Else
                        _Pass = False
                        Exit For
                    End If
                Next
            Else
                _Pass = True
            End If

        Catch ex As Exception

        End Try
        Return _Pass
    End Function

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadPOInfo("")
        Call LoadRcvDetail(Me.FTReceiveNo.Text)
        Call LoadBarcode(Me.FTReceiveNo.Text)
        Me.oxtb.SelectedTabPageIndex = 0
    End Sub

    Private Sub SaveBarcode()
        If _FormLoad Then Exit Sub
        If _EditCell Then
            Dim Qry As String = ""
            Dim _ID As String = "" : Dim _BarCodeNo As String = ""
            With ogvbarcode
                _ID = .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId")
                _BarCodeNo = .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo")
                _BarCodeNo = _BarCodes
                Qry = "SELECT FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase"
                Qry &= vbCrLf & "  WHERE      (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
                Dim _AssetType As Integer = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

                Qry = "select top 1 B.FTBarcodeNo from HITECH_FIXEDASSET..TFIXEDTBarcode AS B WITH(NOLOcK) INNER JOIN"
                Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "]..TFIXEDTBarcode_IN AS BI WITH(NOLOcK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                Qry &= vbCrLf & "where B.FTBarcodeNo = '" & UL.ULF.rpQuoted(_BarCodeNo) & "' and B.FTDocumentNo='" & UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
                If Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) = "" And _BarCodeNo <> "" Then

                    Qry = "select  B.FTBarcodeNo from HITECH_FIXEDASSET..TFIXEDTBarcode AS B WITH(NOLOcK) INNER JOIN"
                    Qry &= vbCrLf & "HITECH_FIXEDASSET..TFIXEDTBarcode_IN AS BI WITH(NOLOcK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                    Qry &= vbCrLf & "where B.FTDocumentNo = '" & UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
                    If Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED).Rows.Count < CType(ogcdetail.DataSource, DataTable).Rows.Count Then
                        Try
                            For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Select("FNHSysFixedAssetId='" & _ID & "'")
                                Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                                Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                                Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(_BarCodeNo) & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & Val(_ID) & "," & _SysUnit & ""
                                Qry &= vbCrLf & "," & _PriceConvert & "," & _QuantityConvert & ",'" & Me.FTReceiveNo.Text & "','" & Me.FTPurchaseNo.Text & "'," & ST.SysInfo.CmpID & ""
                                HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(_BarCodeNo) & "','" & Me.FTReceiveNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNQuantity & ",'" & Me.FTReceiveNo.Text & "'," & ST.SysInfo.CmpID & ""
                                HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                                If _AssetType <> 1 Then

                                    Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET "
                                    Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                    Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                    Qry &= vbCrLf & ",FTSerialNo='" & UL.ULF.rpQuoted(_BarCodeNo) & "'' "
                                    Qry &= vbCrLf & "WHERE FNHSysFixedAssetId = " & _ID & ""
                                    HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                                Else
                                    Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart SET "
                                    Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                    Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                    Qry &= vbCrLf & ",FTSerialNo='" & UL.ULF.rpQuoted(_BarCodeNo) & "' "
                                    Qry &= vbCrLf & "WHERE FNHSysAssetPartId = " & _ID & ""
                                    HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                                End If
                            Next
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try
                    Else
                        'มีบาร์โค๊ดครบจำนวนที่รับมาแล้ว ไม่สามารถเพิ่มใหม่ได้
                    End If
                Else
                    'มีบาร์โค็ดอยู่แล้ว
                End If
            End With
            _EditCell = False
        End If

    End Sub
    Private _BarCodes As String = ""
    Private Sub RepFTBarcodeNo_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTBarcodeNo.EditValueChanging
        _BarCodes = e.NewValue
    End Sub
    Private _BarCodeGen As String = ""
    Private _DT As DataTable
    Private Sub SaveBar()
        Dim Qry As String = ""
        Dim _ID As String = "" : Dim _BarCodeNo As String = ""

        For Each B As DataRow In _DT.Select("FNHSysFixedAssetId='" & _IDasset & "'")
            Qry = "select top 1 B.FTBarcodeNo from HITECH_FIXEDASSET..TFIXEDTBarcode AS B WITH(NOLOcK) INNER JOIN"
            Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "]..TFIXEDTBarcode_IN AS BI WITH(NOLOcK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
            Qry &= vbCrLf & "where B.FTBarcodeNo = '" & B!FTBarcodeNo & "' and B.FTDocumentNo='" & UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
            If Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) = "" And B!FTBarcodeNo <> "" Then
                Qry = "select  B.FTBarcodeNo from HITECH_FIXEDASSET..TFIXEDTBarcode AS B WITH(NOLOcK) INNER JOIN"
                Qry &= vbCrLf & "HITECH_FIXEDASSET..TFIXEDTBarcode_IN AS BI WITH(NOLOcK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                Qry &= vbCrLf & "where B.FTDocumentNo = '" & UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'"
                If Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED).Rows.Count < CType(ogcdetail.DataSource, DataTable).Rows.Count Then
                    Try
                        For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Select("FNHSysFixedAssetId='" & B!FNHSysFixedAssetId & "'")
                            Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                            Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                            Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                            Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                            Qry &= vbCrLf & ",'" & B!FTBarcodeNo & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNHSysFixedAssetId & "," & _SysUnit & ""
                            Qry &= vbCrLf & "," & _PriceConvert & "," & _QuantityConvert & ",'" & Me.FTReceiveNo.Text & "','" & Me.FTPurchaseNo.Text & "'," & ST.SysInfo.CmpID & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                            Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                            Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                            Qry &= vbCrLf & ",'" & B!FTBarcodeNo & "','" & Me.FTReceiveNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNQuantity & ",'" & Me.FTReceiveNo.Text & "'," & ST.SysInfo.CmpID & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        Next

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Else
                    'มีบาร์โค๊ดครบจำนวนที่รับมาแล้ว ไม่สามารถเพิ่มใหม่ได้
                End If
            Else
                'มีบาร์โค็ดอยู่แล้ว
            End If
        Next

    End Sub
    Dim _IDasset As String = ""

    Private Sub Genbarcode()
        Dim sDate As String = Year(Date.Today) & "" & Month(Date.Today).ToString.PadLeft(2, "0") & "" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
        If Year(Date.Today) > 2400 Then
            sDate = Year(Date.Today) - 543 & "" & Month(Date.Today).ToString.PadLeft(2, "0") & "" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
        End If
        sDate = sDate.Substring(2)
        Dim Qry As String = "select B.FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNHSysFixedAssetId=" & Val(_IDasset) & ""
        Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
        Dim _RunNumber As String = ""

        Qry = "SELECT G.FTAssetPartGroupCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS G WITH(NOLOCK) ON P.FNHSysAssetPartGrpId = G.FNHSysAssetPartGrpId WHERE P.FNHSysAssetPartId = " & Val(_IDasset) & ""
        Dim _AssetPart As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
        _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTBarcodeNo) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS A WITH(NOLOCK) where A.FTBarcodeNo like '%" & _AssetType & sDate & "%'", Conn.DB.DataBaseName.DB_FIXED)
        If _AssetPart = "" Then _AssetPart = "1"
        If _AssetType = "" Then _AssetType = _AssetPart
        If _RunNumber = "" Then
            _RunNumber = "0001"
        Else
            _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 4)
            _RunNumber = Format(Val(_RunNumber) + 1, "0000")
        End If
        _BarCodeGen = _AssetType + sDate + _RunNumber



    End Sub

    Private Sub ocmgenbarcode_Click(sender As Object, e As EventArgs) Handles ocmgenbarcode.Click
        'If CheckOwner() = False Then Exit Sub
        'If FTReceiveNo.Text.Trim <> "" Then
        '    If FTReceiveNo.Properties.Tag.ToString <> "" Then
        '        Dim _Str As String = ""

        '        Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)

        '        With _GenBarcode
        '            Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)
        '            .BarType = wGenerateBarcodeAsset.BarCodeType.Receive
        '            .ProcGen = False
        '            .DocumentNo = FTReceiveNo.Text
        '            .LoadGenbarcode()
        '            .MainObject = Me
        '            .ShowDialog()

        '            If (.ProcGen) Then
        '                LoadBarcode(FTReceiveNo.Text)
        '            End If

        '        End With
        '    End If
        'End If
        If CheckOwner() = False Then Exit Sub

        For Each B As DataRow In CType(ogcbarcode.DataSource, DataTable).Select(" FTBarcodeNo = ''")
            _IDasset = B!FNHSysFixedAssetId
            Genbarcode()
            Dim Qry As String = ""
            Qry = "SELECT FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase"
            Qry &= vbCrLf & "  WHERE      (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Properties.Tag.ToString) & "')"
            Dim _AssetType As Integer = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
            If _AssetType <> 1 Then
                Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & _IDasset & ""
            Else
                Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WHERE FNHSysAssetPartId = " & _IDasset & ""
            End If
            If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "") <> "" Then
                _BarCodeGen = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "")
            End If

            For Each R As DataRow In CType(ogcdetail.DataSource, DataTable).Select("FNHSysFixedAssetId='" & B!FNHSysFixedAssetId & "'")
                Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                Qry &= vbCrLf & ", FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo, FNHSysCmpId)"
                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & ",'" & _BarCodeGen & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNHSysFixedAssetId & "," & _SysUnit & ""
                Qry &= vbCrLf & "," & _PriceConvert & "," & _QuantityConvert & ",'" & Me.FTReceiveNo.Text & "','" & Me.FTPurchaseNo.Text & "'," & ST.SysInfo.CmpID & ""
                HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                Qry &= vbCrLf & ",'" & _BarCodeGen & "','" & Me.FTReceiveNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & _PriceConvert & ",'" & Me.FTReceiveNo.Text & "'," & ST.SysInfo.CmpID & ""
                HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                If _AssetType <> 1 Then

                    Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET "
                    Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    Qry &= vbCrLf & ",FTSerialNo='" & _BarCodeGen & "' "
                    Qry &= vbCrLf & "WHERE FNHSysFixedAssetId = " & _IDasset & ""
                    HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                Else
                    Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart SET "
                    Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    Qry &= vbCrLf & ",FTSerialNo='" & _BarCodeGen & "' "
                    Qry &= vbCrLf & "WHERE FNHSysAssetPartId = " & _IDasset & ""
                    HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                End If
            Next
        Next

        PrepareListBarcode()


    End Sub
    Dim _SysUnit As Integer
    Dim _QuantityConvert As Integer
    Dim _PriceConvert As Double
    Dim _SysUnitCode As String = ""
    Public Sub UnitConvert(Key As Integer, Quantity As Integer, Price As Integer)
        Dim Qry As String = ""
        Qry = "Select  TOP 1 A.FNHSysUnitAssetId,A.FNHSysUnitAssetIdTo,A.FNRateFrom,A.FNRateTo,B.FNHSysFixedAssetId,b.FTAssetCode,d.FTUnitAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAssetConvert As A "
        Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TASMAsset As B On A.FNHSysUnitAssetIdTo = B.FNHSysUnitAssetId "
        Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS C ON A.FNHSysUnitAssetId = C.FNHSysUnitId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset As D On A.FNHSysUnitAssetIdTo = D.FNHSysUnitAssetId"
        Qry &= vbCrLf & "WHERE B.FNHSysFixedAssetId = " & Key & " And C.FNHSysFixedAssetId = " & Key & " and C.FTReceiveNo= '" & Me.FTReceiveNo.Text & "'"
        'Qry &= vbCrLf & "union all"
        'Qry &= vbCrLf & "Select  A.FNHSysUnitAssetId,A.FNHSysUnitAssetIdTo,A.FNRateFrom,A.FNRateTo,B.FNHSysAssetPartId,b.FTAssetPartCode,d.FTUnitAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAssetConvert As A "
        'Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS B ON A.FNHSysUnitAssetIdTo = B.FNHSysUnitAssetId "
        'Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail As C On A.FNHSysUnitAssetId = C.FNHSysUnitId"
        'Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset As D On A.FNHSysUnitAssetIdTo = D.FNHSysUnitAssetId"
        'Qry &= vbCrLf & "WHERE B.FNHSysAssetPartId = " & Key & " And C.FNHSysFixedAssetId = " & Key & ""
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        If _dt.Rows.Count = 0 Then
            Exit Sub
        End If
        For Each U As DataRow In _dt.Rows
            _SysUnit = U!FNHSysUnitAssetIdTo
            _QuantityConvert = (Quantity / U!FNRateFrom) * U!FNRateTo
            _PriceConvert = Price * (U!FNRateFrom / U!FNRateTo)
            _SysUnitCode = U!FTUnitAssetCode
        Next
    End Sub

    Private Sub FTReceiveNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTReceiveNo.EditValueChanged

    End Sub
End Class