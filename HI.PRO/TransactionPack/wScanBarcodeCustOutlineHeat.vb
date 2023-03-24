
Imports System.Windows.Forms
Public Class wScanBarcodeCustOutlineHeat
    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _BundleNo As String = ""
    Private StateBarcodeBundle As Boolean = False
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.InitFormControl()

        Call InitGrid()
        Me.FNStateSewPackIron.SelectedIndex = 4
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        With ogvdetail
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

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

            '
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
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
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


        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail()
        Try
            Dim _Qry As String = ""
            Dim dt As DataTable


            _Qry = "  SELECT  B.FTBarcodeNo , B.FTBarcodeCustRef, B.FNHSysEmpId,    sum(B.FNQuantity) AS FNQuantity , U.FTEmpCode,   D.FTSizeBreakDown, P.FTOrderNo , ODRS.FTPORef , D.FNBunbleSeq "
            _Qry &= vbCrLf & ",CASE WHEN Isdate(B.FDDate)=1 Then convert(nvarchar(10),convert(datetime,B.FDDate),103) Else '' END AS  FDDate"
            '_Qry &= vbCrLf & ",Isnull((Select min(FTNikePOLineItem) AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown WITH(NOLOCK) where FTOrderNo =P.FTOrderNo and FTColorway = D.FTColorway) , '')  AS FTNikePOLineItem"
            _Qry &= vbCrLf & ",CASE WHEN Isnull( D.FTChangeToLineItemNo, '') <>'' THEN  Isnull( D.FTChangeToLineItemNo, '')   ELSE Isnull( D.FTPOLineItemNo, '') END AS FTNikePOLineItem "

            _Qry &= vbCrLf & ",CASE WHEN Isnull( D.FTColorwayNew, '') <>'' THEN Isnull( D.FTColorwayNew, '')  ELSE D.FTColorway END AS FTColorway "

            _Qry &= vbCrLf & ",TS.FTStyleCode    "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",max(U.FTEmpNameTH + ' ' + U.FTEmpSurnameTH) AS FTUnitSectName    , max(TS.FTStyleNameTH) AS FTStyleName  , max(FTNameTH) as FNStateSewPack "
            Else
                _Qry &= vbCrLf & ",max(U.FTEmpNameEN+ ' ' + U.FTEmpSurnameEN) AS FTUnitSectName   , max(TS.FTStyleNameEN) AS FTStyleName  , max(FTNameEN) as FNStateSewPack "
            End If

            _Qry &= vbCrLf & ",Max(ZXI.FDInsDate) AS FDInsDate"
            _Qry &= vbCrLf & ",Max(ZXI.FTInsTime) AS FTInsTime"
            _Qry &= vbCrLf & ",Max(ZXI.FTInsUser) AS FTInsUser"
            _Qry &= vbCrLf & ",Max(ZX.FDUpdDate) AS FDUpdDate"
            _Qry &= vbCrLf & ",Max(ZX.FTUpdTime) AS  FTUpdTime"
            _Qry &= vbCrLf & ",Max(ZX.FTUpdUser) AS  FTUpdUser"

            _Qry &= vbCrLf & ",isnull((Select Sum(X.FNQuantity) AS FNQuantity From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " Where X.FTBarcodeNo = B.FTBarcodeNo And X.FNHSysEmpId = B.FNHSysEmpId ),0) AS FNScanByBundleNoQty"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS U WITH (NOLOCK) ON B.FNHSysEmpId = U.FNHSysEmpId INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS D WITH (NOLOCK) ON B.FTBarcodeNo = D.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON D.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS TS WITH(NOLOCK) ON O.FNHSysStyleId = TS.FNHSysStyleId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN  (Select FNListIndex , FTNameEN , FTNameTH From [HITECH_SYSTEM].dbo.HSysListData with(nolock)  where  FTListName = 'FNStateSewPackIron'   ) AS LL ON isnull(B.FNStateSewPack,0) = LL.FNListIndex "



            _Qry &= vbCrLf & " outer apply ( "
            _Qry &= vbCrLf & " select top 1 Isnull(ZX.FTUpdTime,ZX.FTInsTime) FTUpdTime   "
            _Qry &= vbCrLf & " ,Isnull(FTUpdUser,FTInsUser) FTUpdUser   "
            _Qry &= vbCrLf & " , Convert(varchar(10),Convert(datetime,Isnull(FDUpdDate,FDInsDate)),103)  FDUpdDate   "
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single AS ZX WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE ZX.FTBarcodeNo = B.FTBarcodeNo And ZX.FDDate = B.FDDate "

            _Qry &= vbCrLf & "  Order by ZX.FDInsDate desc, ZX.FTInsTime desc "

            _Qry &= vbCrLf & " ) AS ZX "

            _Qry &= vbCrLf & " outer apply ( "
            _Qry &= vbCrLf & " select top 1 Isnull(ZX.FTUpdTime,ZX.FTInsTime) FTInsTime   "
            _Qry &= vbCrLf & " ,Isnull(FTUpdUser,FTInsUser) FTInsUser   "
            _Qry &= vbCrLf & " , Convert(varchar(10),Convert(datetime,Isnull(FDUpdDate,FDInsDate)),103)  FDInsDate   "
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single AS ZX WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE ZX.FTBarcodeNo = B.FTBarcodeNo  "

            _Qry &= vbCrLf & "  Order by ZX.FDInsDate asc, ZX.FTInsTime asc "

            _Qry &= vbCrLf & " ) AS ZXI "

            _Qry &= vbCrLf & " outer apply ( "
            _Qry &= vbCrLf & " select top 1 ZX.FTPOref  "
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS ZX WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE ZX.FTOrderNo = B.FTOrderNo  "
            _Qry &= vbCrLf & "  AND ZX.FTSubOrderNo = B.FTSubOrderNo  "
            _Qry &= vbCrLf & "  AND ZX.FTColorway = CASE WHEN Isnull( D.FTColorwayNew, '') <>'' THEN Isnull( D.FTColorwayNew, '')  ELSE D.FTColorway END  "
            _Qry &= vbCrLf & "  AND ZX.FTSizeBreakDown = D.FTSizeBreakDown  "

            _Qry &= vbCrLf & "  AND ZX.FTNikePOLineItem = CASE WHEN Isnull( D.FTChangeToLineItemNo, '') <>'' THEN  Isnull( D.FTChangeToLineItemNo, '')   ELSE Isnull( D.FTPOLineItemNo, '') END "


            _Qry &= vbCrLf & " ) AS ODRS "


            _Qry &= vbCrLf & " WHERE   B.FDDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "' "
            _Qry &= vbCrLf & " and   isnull(B.FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
            _Qry &= vbCrLf & " AND ( ISNULL(U.FNHSysCmpId,0) = 0 OR  ISNULL(U.FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & "  ) "

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & "And B.FNHSysEmpId=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString()) & ""
            End If
            _Qry &= vbCrLf & "Group by  B.FTBarcodeNo, B.FNHSysEmpId,   U.FTEmpCode,   D.FTSizeBreakDown, P.FTOrderNo , ODRS.FTPORef , D.FNBunbleSeq ,B.FDDate , B.FTBarcodeCustRef  ,TS.FTStyleCode  , isnull(B.FNStateSewPack,0)"

            _Qry &= vbCrLf & ",CASE WHEN Isnull( D.FTChangeToLineItemNo, '') <>'' THEN  Isnull( D.FTChangeToLineItemNo, '')   ELSE Isnull( D.FTPOLineItemNo, '') END  "

            _Qry &= vbCrLf & ",CASE WHEN Isnull( D.FTColorwayNew, '') <>'' THEN Isnull( D.FTColorwayNew, '')  ELSE D.FTColorway END  "

            _Qry &= vbCrLf & "Order by FDUpdDate DESC,FTUpdTime DESC ,FDInsDate DESC, FTInsTime DESC"

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Me.ogcdetail.DataSource = dt.Copy

            _Qry = "  SELECT     convert(numeric(18,0), sum(B.FNQuantity)) AS FNQuantity  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS U WITH (NOLOCK) ON B.FNHSysEmpId = U.FNHSysEmpId INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS D WITH (NOLOCK) ON B.FTBarcodeNo = D.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON D.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
            _Qry &= vbCrLf & "WHERE  B.FNStateSewPack = 4  and B.FDDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "' "
            _Qry &= vbCrLf & " AND ( ISNULL(U.FNHSysCmpId,0) = 0 OR  ISNULL(U.FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & "  ) "

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & "And B.FNHSysEmpId=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString()) & ""
            End If
            Me.lblQtyScan.Text = Format(Integer.Parse(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0")), "n0")

        Catch ex As Exception
        End Try
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

    End Sub

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
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString) ' hImage ' ' Image.FromFile("" & .Properties.Tag.ToString & R.Item(Col).ToString)
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
    End Sub

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged
        Try
            Dim dt As New DataTable
            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try
            dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wScanBarcodeSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNStateSewPackIron.SelectedIndex = 4
        FTCustBarcodeNo.EnterMoveNextControl = False
        FNHSysEmpId.TabStop = False
        Me.FDDateTrans.Text = HI.Conn.SQLConn.GetField("Select  convert(nvarchar(10),Convert(varchar(10),Getdate(),103),103) ", Conn.DB.DataBaseName.DB_PROD, Date.Now)
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadDucumentDetail()
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.FTCustBarcodeNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTBarcodeNo_lbl.Text)
                Me.FTCustBarcodeNo.Focus()
                Return False
            End If
            If Me.FTCustBarcodeNo.Text <> "" Then
                Dim _Cmd As String = ""
                _Cmd = "Select Top 1 FTCustBarcodeNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrder_CustBarcode AS C WITH(NOLOCK) "
                _Cmd &= vbCrLf & "Where FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "'"
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                    _Cmd = "Select Top 1 FTBarcodeBundleNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle AS C WITH(NOLOCK) "
                    _Cmd &= vbCrLf & "Where FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "'"
                    If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTBarcodeNo_lbl.Text)
                        HI.MG.ShowMsg.mInfo("Invalite barcode. Pls Check Barcode !!!! ", 1604021059, Me.FTBarcodeNo_lbl.Text)
                        FTCustBarcodeNo.Focus()
                        FTCustBarcodeNo.SelectAll()
                        Return False
                    End If

                End If
            End If
            If Me.FNHSysEmpId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysEmpId_lbl.Text)
                Me.FNHSysEmpId.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTCustBarcodeNo.KeyDown
        Try

            Select Case e.KeyCode
                Case Keys.Enter
                    'Dim _BundleNo As String = GetBarcodeBundle()
                    'If _BundleNo = "" Then
                    '    HI.MG.ShowMsg.mInfo("Barcode ยังไม่มีการแสกนเข้าไลน์ หรือแสกนออกไลน์หมดแล้ว กรุณาตรวจสอบ......", 1602111413, Me.Text, "", MessageBoxIcon.Stop)
                    '    FTBarcodeNo.Focus()
                    '    FTBarcodeNo.SelectAll()
                    '    Exit Sub
                    'End If
                    If Not (VerrifyData()) Then Exit Sub

                    StateBarcodeBundle = False

                    If ChkBarCode() Then

                        If ChkBreakTime() = False Then
                            HI.MG.ShowMsg.mInfo("หมดเวลาแสถนงาน กรุณาไปพักตามอัตยาศัยครับ......", 1711211728, Me.Text, "", MessageBoxIcon.Stop)
                            Exit Sub
                        End If

                        Dim cmdstring As String = "SELECT TOP 1 FTBarcodeBundleNo FROM TPRODTBundle AS X WITH(NOLOCK) WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(FTCustBarcodeNo.Text.Trim) & "'"
                        StateBarcodeBundle = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PROD, "") <> "")

                        If ScansBarcode(_BundleNo) Then
                            Call LoadDucumentDetail()
                        End If
                        If Not (Me.FTStateDeleteBarcode.Checked) Then
                            Call UpdateBundleToBox(_BundleNo)
                        End If


                        FTCustBarcodeNo.Focus()
                        FTCustBarcodeNo.SelectAll()
                    Else
                        HI.MG.ShowMsg.mInfo("Barcode ยังไม่มีการแสกนเข้าไลน์  หรือ สแกนครบจำนวนแล้ว......", 1606170853, Me.Text, "", MessageBoxIcon.Stop)
                        FTCustBarcodeNo.Focus()
                        FTCustBarcodeNo.SelectAll()
                    End If
                    FTCustBarcodeNo.Focus()
                    FTCustBarcodeNo.SelectAll()
            End Select

        Catch ex As Exception
            FTCustBarcodeNo.Focus()
            FTCustBarcodeNo.SelectAll()
        End Try
    End Sub

    Private Function UpdateBundleToBox(_BundleNo As String) As Boolean
        Try
            Dim _Cmd As String = ""

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(_BundleNo) & "'")


                    _Cmd = "Select top 1  '1' AS FTState From      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode with(nolock)  "
                    _Cmd &= vbCrLf & " where    ISNULL (FTBarcodeBundleNo, '' ) = '" & HI.UL.ULF.rpQuoted(_BundleNo) & "'   "

                    If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0 Then

                        _Cmd = "UPDATE   TOP (1)   C "
                        _Cmd &= vbCrLf & "Set C.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_BundleNo) & "'"
                        _Cmd &= vbCrLf & " FROM   "
                        _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C  LEFT OUTER JOIN "
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS B WITH(NOLOCK) ON C.FTPackNo = B.FTPackNo AND C.FNCartonNo = B.FNCartonNo "
                        _Cmd &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB  WITH(NOLOCK)  ON B.FTColorway = BB.FTColorway AND B.FTSizeBreakDown = BB.FTSizeBreakDown AND B.FNPackPerCarton = BB.FNQuantity"
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS D WITH(NOLOCK)  ON C.FTPackNo = D.FTPackNo "
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS A  WITH(NOLOCK)   ON C.FTPackNo = A.FTPackNo AND C.FNCartonNo = A.FNCartonNo  "
                        _Cmd &= vbCrLf & "where  B.FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        _Cmd &= vbCrLf & "and  B.FTColorway = '" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        _Cmd &= vbCrLf & "and  B.FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                        _Cmd &= vbCrLf & "and  D.FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                        _Cmd &= vbCrLf & "and  B.FTPOLine = '" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
                        _Cmd &= vbCrLf & "and BB.FTBarcodeBundleNo = '" & HI.UL.ULF.rpQuoted(_BundleNo) & "'"
                        _Cmd &= vbCrLf & "and isnull( C.FTBarcodeBundleNo,'') = '' "
                        _Cmd &= vbCrLf & " and isnull( A.FTState,'0')  = '0'   "

                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    End If

                    Exit For

                Next

            End With

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkBreakTime() As Boolean
        Try

            If Me.FNHSysEmpId.Text = "" Then

                HI.MG.ShowMsg.mInfo("กรุณเลือกรหัสพนักงาน......", 2209070933, Me.Text, "", MessageBoxIcon.Stop)
                Me.FNHSysEmpId.Focus()

                Return False
            End If

            Dim _Cmd As String = ""
            Dim _State As Boolean = False
            _Cmd = "  EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_CHECKTIME_SCANOUTLINE_Single " & Integer.Parse(Me.FNHSysEmpId.Properties.Tag.ToString) & ""
            _State = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "1"
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function



    Private Function ChkBarCode() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            If Not (Me.FTStateDeleteBarcode.Checked) Then
                _Cmd = "  SELECT    Top 1  O.FTSubOrderNo,  O.FTBarcodeNo, O.FNHSysUnitSectId, O.FDDate, O.FTTime, O.FNQuantity , B.FTColorway , B.FTSizeBreakDown , P.FTOrderNo , B.FTOrderProdNo , B.FNBunbleSeq , S.FTStyleCode , T.FTPORef,B.FTPOLineItemNo"
                _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS O WITH(NOLOCK) "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeBundleNo"
                _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS T WITH(NOLOCK) ON P.FTOrderNo = T.FTOrderNo"
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON T.FNHSysStyleId = S.FNHSysStyleId"
                _Cmd &= vbCrLf & " WHERE  ( O.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "'   )"

                If Me.FTBarcodeBundleNo.Text <> "" Then
                    _Cmd &= vbCrLf & " AND B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeBundleNo.Text) & "'  "
                End If

                If Me.FTOrderNo.Text <> "" Then

                    _Cmd &= vbCrLf & " And P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"

                    If Me.FTSubOrderNo.Text <> "" Then
                        _Cmd &= vbCrLf & " And O.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
                    End If

                End If

                _Cmd &= vbCrLf & "Order by isnull(O.FDUpdDate , O.FDInsDate) DESC , isnull(O.FTUpdTime , O.FTInsTime) DESC"
            Else
                _Cmd = "  SELECT    Top 1  O.FTSubOrderNo,  O.FTBarcodeNo, O.FNHSysEmpId, O.FDDate, O.FTTime, O.FNQuantity , B.FTColorway , B.FTSizeBreakDown , P.FTOrderNo , B.FTOrderProdNo , B.FNBunbleSeq , S.FTStyleCode , T.FTPORef,B.FTPOLineItemNo"
                _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single AS O WITH(NOLOCK) "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON O.FTBarcodeNo = B.FTBarcodeBundleNo"
                _Cmd &= vbCrLf & "INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS T WITH(NOLOCK) ON P.FTOrderNo = T.FTOrderNo"
                _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH(NOLOCK) ON T.FNHSysStyleId = S.FNHSysStyleId"
                _Cmd &= vbCrLf & " WHERE  ( O.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "' OR  B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "' )"
                _Cmd &= vbCrLf & "and   isnull(O.FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
                If Me.FTBarcodeBundleNo.Text <> "" Then
                    _Cmd &= vbCrLf & " AND B.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeBundleNo.Text) & "'  "
                End If

                If Me.FTOrderNo.Text <> "" Then

                    _Cmd &= vbCrLf & " And P.FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"

                    If Me.FTSubOrderNo.Text <> "" Then
                        _Cmd &= vbCrLf & " And O.FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
                    End If

                End If

                _Cmd &= vbCrLf & "And O.FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpId.Properties.Tag)
                _Cmd &= vbCrLf & "Order by isnull(O.FDUpdDate , O.FDInsDate) DESC , isnull(O.FTUpdTime , O.FTInsTime) DESC"

            End If

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows

                _BundleNo = HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString)
                Me.FTColorway.Text = HI.UL.ULF.rpQuoted(R!FTColorway.ToString)
                Me.FTSizeBreakDown.Text = HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString)
                Me.FTSubOrderNo.Text = HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString)
                Me.FNBunbleSeq.Text = HI.UL.ULF.rpQuoted(R!FNBunbleSeq.ToString)
                Me.FTStyleCode.Text = HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString)
                Me.FTOrderNo.Text = HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString)
                Me.FTPORef.Text = HI.UL.ULF.rpQuoted(R!FTPORef.ToString)

                Dim StateCheck As Boolean = True

                If FTStateDeleteBarcode.Checked = False Then

                    _Cmd = "  Select  B.FTBarcodeBundleNo "
                    _Cmd &= vbCrLf & ", C.FTSubOrderNo "
                    _Cmd &= vbCrLf & ", C.FTColorway"
                    _Cmd &= vbCrLf & ", C.FTSizeBreakDown"
                    _Cmd &= vbCrLf & ", C.FNQuantity"
                    _Cmd &= vbCrLf & ", ISNULL(SC.FNQuantity,0) AS FNScanQty"
                    _Cmd &= vbCrLf & ",ROW_NUMBER() OVER (ORDER BY C.FTSubOrderNo) As FNSubOrderSeq"
                    _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) INNER Join"
                    _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail As C With(NOLOCK) On B.FTOrderProdNo = C.FTOrderProdNo"
                    _Cmd &= vbCrLf & "  LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As C2 With(NOLOCK) On C.FTOrderNo = C2.FTOrderNo AND C.FTSubOrderNo = C2.FTSubOrderNo AND C.FTColorway = C2.FTColorway AND C.FTSizeBreakDown = C2.FTSizeBreakDown"
                    _Cmd &= vbCrLf & "  OUTER APPLY(Select SUM(X.FNQuantity)  As FNQuantity"
                    _Cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single As X With(NOLOCK) "
                    _Cmd &= vbCrLf & "         INNER  Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As XB On X. FTBarcodeNo = XB.FTBarcodeBundleNo"
                    _Cmd &= vbCrLf & "  WHERE X.FTOrderNo = C.FTOrderNo "
                    _Cmd &= vbCrLf & "       And X.FTSubOrderNo = C.FTSubOrderNo"
                    _Cmd &= vbCrLf & "       And XB.FTColorway = C.FTColorway"
                    _Cmd &= vbCrLf & "       And XB.FTSizeBreakDown =C.FTSizeBreakDown"
                    _Cmd &= vbCrLf & " and   isnull(X.FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
                    _Cmd &= vbCrLf & "  ) As SC"
                    _Cmd &= vbCrLf & " WHERE (B.FTBarcodeBundleNo = N'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "') "
                    _Cmd &= vbCrLf & "       And (C.FTSizeBreakDown = N'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "') "
                    _Cmd &= vbCrLf & "       And (C.FTColorway = N'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "')"
                    _Cmd &= vbCrLf & "       And (C2.FTNikePOLineItem = N'" & HI.UL.ULF.rpQuoted(R!FTPOLineItemNo.ToString) & "')"

                    'FTSubOrderNo
                    Dim dtcheck As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    Dim IndMax As Integer = dtcheck.Rows.Count

                    If dtcheck.Select("FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNQuantity>FNScanQty").Length > 0 Then
                    Else
                        If dtcheck.Select(" FNQuantity>FNScanQty").Length > 0 Then

                            StateCheck = False

                        Else

                            If dtcheck.Select("FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "' AND FNSubOrderSeq=" & IndMax & " ").Length > 0 Then
                            Else
                                StateCheck = False
                            End If

                        End If

                    End If

                    dtcheck.Dispose()

                End If



                Return StateCheck

            Next

            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function ScansBarcode(BarcodeKey As String) As Boolean
        Try

            If Not (Me.FTStateDeleteBarcode.Checked) Then

                If Not (ChkBarcodeInlineBal(BarcodeKey, Me.FTOrderNo.Text, FNHSysEmpId.Properties.Tag)) Then

                    HI.MG.ShowMsg.mInfo("BarCode ออกไลน์หมดแล้ว...", 1506191040, Me.Text, "", MessageBoxIcon.Stop)
                    Return False

                End If

            End If

            Dim _Cmd As String = ""
            Dim _Qty As Integer = 0
            _Cmd = "Select   Top 1  FNQuantity    from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle B "
            _Cmd &= vbCrLf & " where b.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"

            _Qty = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If Not (Me.FTStateDeleteBarcode.Checked) Then

                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single"
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNQuantity=FNQuantity+" & _Qty



                _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpId.Properties.Tag)
                _Cmd &= vbCrLf & "And FDDate =" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "And FTTime = Convert(varchar(5),Getdate(),114)"
                _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single"
                    _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime , FTBarcodeNo, FNHSysEmpId, FDDate, FTTime, FNQuantity,FTBarcodeCustRef , FTOrderNo , FTSubOrderNo,FNStateSewPack)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNHSysEmpId.Properties.Tag)
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",Convert(varchar(5),Getdate(),114)"
                    _Cmd &= vbCrLf & "," & _Qty

                    If StateBarcodeBundle Then
                        _Cmd &= vbCrLf & " ,''"
                    Else
                        _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "'"
                    End If

                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Else

                _Cmd = " Select Top 1   FTBarcodeNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                _Cmd &= vbCrLf & " UNION ALL"
                _Cmd &= vbCrLf & "Select Top 1   FTBarcodeNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack_Detail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"

                If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows.Count <= 0 Then

                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single"
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FNQuantity=FNQuantity-" & _Qty
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
                    _Cmd &= vbCrLf & " AND FNQuantity>0"

                    If Me.FTOrderNo.Text <> "" Then

                        _Cmd &= vbCrLf & " And FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"

                        If Me.FTSubOrderNo.Text <> "" Then
                            _Cmd &= vbCrLf & " And  FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
                        End If

                    End If

                    _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
                    _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single "
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"

                    'If Me.FTCustBarcodeNo.Text = Me.FTBarcodeBundleNo.Text Then

                    'Else
                    '    _Cmd &= vbCrLf & "and FTBarcodeCustRef='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text) & "'"
                    'End If

                    _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
                    If Me.FTOrderNo.Text <> "" Then
                        _Cmd &= vbCrLf & " And FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                        If Me.FTSubOrderNo.Text <> "" Then
                            _Cmd &= vbCrLf & " And FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"
                        End If
                    End If
                    _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single"
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    _Cmd &= vbCrLf & "And FNHSysEmpId=" & Integer.Parse(Me.FNHSysEmpId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
                    _Cmd &= vbCrLf & "  AND FNQuantity<=0"

                    HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")

                    _Cmd = "Select Top 1 FNQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single"
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    _Cmd &= vbCrLf & "and   isnull( FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)

                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0") <= 0 Then

                        _Cmd = "Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                        _Cmd &= vbCrLf & " set  FTBarcodeBundleNo ='' "
                        _Cmd &= vbCrLf & "WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            'HI.Conn.SQLConn.Tran.Rollback()
                            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            'Return False
                        End If

                    End If
                Else
                    HI.MG.ShowMsg.mInfo("ไม่สามารถลบข้อมูลได้ เนื่องจากมีการบรรจุกล่องแล้ว !!! ", 1605261437, Me.Text, "", MessageBoxIcon.Stop)
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

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

    Private Sub FDDateTrans_EditValueChanged(sender As Object, e As EventArgs) Handles FDDateTrans.EditValueChanged
        Try
            Call LoadDucumentDetail()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysEmpId.EditValueChanged
        Try
            Call LoadDucumentDetail()
        Catch ex As Exception
        End Try
    End Sub

    Private Function ChkBarcodeInlineBal(BarCode As String, OrderNo As String, UnitSectId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyIn As Double = 0 : Dim _QtyOut As Double = 0
            _Cmd = "SELECT     A.FTBarcodeNo, P.FTOrderNo, SUM(A.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "FROM    "
            _Cmd &= vbCrLf & " (Select FTBarcodeNo , SUM(FNQuantity) AS FNQuantity  "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline    WITH(NOLOCK) "
            _Cmd &= vbCrLf & " Group by FTBarcodeNo  "

            _Cmd &= vbCrLf & " ) AS A LEFT OUTER JOIN "

            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "WHERE (A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo,  P.FTOrderNo "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                _QtyIn = Double.Parse(R!FNQuantity.ToString)
                Exit For
            Next

            _Cmd = "SELECT     A.FTBarcodeNo, A.FNHSysEmpId , P.FTOrderNo, SUM(A.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "FROM    "
            _Cmd &= vbCrLf & " (Select FTBarcodeNo , SUM(FNQuantity) AS FNQuantity ,FNHSysEmpId"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline_Single   WITH(NOLOCK) "
            _Cmd &= vbCrLf & " where    isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPackIron.SelectedIndex)
            _Cmd &= vbCrLf & " Group by FTBarcodeNo,FNHSysEmpId "

            _Cmd &= vbCrLf & " ) AS A LEFT OUTER JOIN "

            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B WITH(NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo INNER JOIN "
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "WHERE (A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysEmpId=" & Integer.Parse(UnitSectId) & ")"
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo, A.FNHSysEmpId, P.FTOrderNo "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                _QtyOut = Double.Parse(R!FNQuantity.ToString)
                Exit For
            Next
            Return _QtyIn > _QtyOut
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FTCustBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTCustBarcodeNo.EditValueChanged

    End Sub

    Private Sub FNStateSewPack_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNStateSewPackIron.SelectedIndexChanged
        If FNStateSewPackIron.SelectedIndex > 1 Then
            FNStateSewPackIron.SelectedIndex = 4
        End If
    End Sub
End Class