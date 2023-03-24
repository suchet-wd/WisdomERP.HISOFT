Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports outlook = Microsoft.Office.Interop.Outlook
Imports DevExpress.XtraGrid.Views.Base
Imports Microsoft.Win32

Public Class wPurchaseOrder

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wAddItemPO
    Private _RevisedPopup As wPurchaseReviseRemark
    Private _AddItemPopupMultijob As wAddItemPOByItem

    Private _AddItemPopupChangePONO As wPurchaseOrderChangePONO

    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _PORunDocVat As Boolean = False

    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        _AddItemPopup = New wAddItemPO
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try

        _RevisedPopup = New wPurchaseReviseRemark
        HI.TL.HandlerControl.AddHandlerObj(_RevisedPopup)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _RevisedPopup.Name.ToString.Trim, _RevisedPopup)
        Catch ex As Exception
        Finally
        End Try

        _AddItemPopupMultijob = New wAddItemPOByItem
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopupMultijob)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopupMultijob.Name.ToString.Trim, _AddItemPopupMultijob)
        Catch ex As Exception
        Finally
        End Try

        _AddItemPopupChangePONO = New wPurchaseOrderChangePONO
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopupChangePONO)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopupChangePONO.Name.ToString.Trim, _AddItemPopupChangePONO)
        Catch ex As Exception
        Finally
        End Try

        Dim cmdstring As String = "select top 1 FTCfgData from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X With(Nolock) where FTCfgName ='CVNPORunVat'"

        _PORunDocVat = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "") = "Y")

    End Sub

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

    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True

        Try
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

            _Str = ""
            _Str &= vbCrLf & " Select  A.FTOrderNo "
            _Str &= vbCrLf & "  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As A WITH(NOLOCK) INNER Join "
            _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As B  WITH(NOLOCK) On A.FTOrderNo = B.FTOrderNo INNER Join"
            _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS BG  WITH(NOLOCK) ON B.FNHSysBuyGrpId = BG.FNHSysBuyGrpId"
            _Str &= vbCrLf & "  Where  (A.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Key.ToString) & "') AND (BG.FTBuyGrpCode = N'4') "

            lblpomo.Visible = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_PUR, "") <> "")

            Call LoadPoDetail(Key.ToString)

            Me.oxtb.SelectedTabPageIndex = 0

            _Dt.Dispose()
        Catch ex As Exception

        End Try

        _ProcLoad = False
    End Sub

    Private Sub LoadPoDetail(PoKey As String)
        Dim _Str As String = ""
        Dim _dtdetail As DataTable

        _Str = " Select        D.FNHSysRawMatId, M.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , M.FTRawMatNameTH As FTMatDesc"
            _Str &= vbCrLf & " , D.FTRawMatColorNameTH As FTRawMatColorName"
        Else
            _Str &= vbCrLf & " , M.FTRawMatNameEN As FTMatDesc"
            _Str &= vbCrLf & " , D.FTRawMatColorNameEN As FTRawMatColorName"
        End If

        _Str &= vbCrLf & "  , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt, D.FTOrderNo, D.FNQuantity, D.FNNetAmt, D.FTRemark"
        _Str &= vbCrLf & ",ISNULL(D.FNSurchangeAmt,0) AS FNSurchangeAmt"
        _Str &= vbCrLf & ",ISNULL(D.FNSurchangePerUnit,0) AS FNSurchangePerUnit"
        _Str &= vbCrLf & ",ISNULL(D.FNGrandNetAmt,D.FNNetAmt) AS FNGrandNetAmt"
        _Str &= vbCrLf & ",ISNULL(("
        _Str &= vbCrLf & " SELECT    TOP 1   CASE WHEN ISNULL(RB.FNQuantity,0) = 0 THEN  CASE WHEN ISNULL(RA.FTStateImport,'') ='1' THEN '2' ELSE '3' END ELSE  '1' END  AS FTStateRcv"
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RA WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RB  WITH(NOLOCK)  ON RA.FTReceiveNo = RB.FTReceiveNo"
        _Str &= vbCrLf & "  WHERE  RA.FTPurchaseNo= D.FTPurchaseNo"
        _Str &= vbCrLf & " AND    RB.FNHSysRawMatId= D.FNHSysRawMatId"
        _Str &= vbCrLf & ""
        _Str &= vbCrLf & "),'0') AS FTStateRcv"
        _Str &= vbCrLf & ",ISNULL(FNReservePOQuantity,0) AS FNReservePOQuantity"
        _Str &= vbCrLf & ",ISNULL(("
        _Str &= vbCrLf & " SELECT    TOP 1    '1' AS FTStateRcv"
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS AA WITH(NOLOCK) "
        _Str &= vbCrLf & "  WHERE  AA.FTPurchaseNo= D.FTPurchaseNo"
        _Str &= vbCrLf & "  AND    AA.FTOrderNo= D.FTOrderNo"
        _Str &= vbCrLf & "  AND    AA.FNHSysRawMatId= D.FNHSysRawMatId"
        _Str &= vbCrLf & ""
        _Str &= vbCrLf & "),'0') AS FTStateReserve"

        _Str &= vbCrLf & ",D.FTOGacDate,CASE WHEN ISNULL(MMX.FNMerMatType,0) <=0 THEN 0 ELSE 1 END AS FNMerMatTypePOX"

        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
        _Str &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"

        _Str &= vbCrLf & "  OUTER APPLY( SELECT TOP 1 FNMerMatType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS XXX WITH (NOLOCK) WHERE XXX.FTMainMatCode = M.FTRawMatCode ) MMX"

        _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FTRawMatSizeCode, D.FTOrderNo "
        '_Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FNRawMatColorSeq, S.FNRawMatSizeSeq, D.FTOrderNo "
        _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FNRawMatSizeSeq, D.FTOrderNo "
        _dtdetail = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        Me.ogcdetail.DataSource = _dtdetail.Copy

        _Str = " SELECT        D.FNHSysRawMatId, M.FTRawMatCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , MAX(M.FTRawMatNameTH) AS FTMatDesc"
            _Str &= vbCrLf & " , ISNULL(D.FTRawMatColorNameTH,'') AS FTRawMatColorName"
        Else
            _Str &= vbCrLf & " , MAX(M.FTRawMatNameEN) AS FTMatDesc"
            _Str &= vbCrLf & " , ISNULL(D.FTRawMatColorNameEN,'') AS FTRawMatColorName"
        End If

        _Str &= vbCrLf & " , ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt, SUM(D.FNQuantity) AS  FNQuantity"

        _Str &= vbCrLf & "  ,  Convert(numeric(18,2),SUM(D.FNQuantity) *  D.FNPrice)  AS FNNetAmt"
        _Str &= vbCrLf & "   , MAX(D.FTRemark) AS FTRemark"

        _Str &= vbCrLf & ",MAX(ISNULL(D.FNSurchangeAmt,0)) AS FNSurchangeAmt"
        _Str &= vbCrLf & ",MAX(ISNULL(D.FNSurchangePerUnit,0)) AS FNSurchangePerUnit"

        _Str &= vbCrLf & ",MAX(ISNULL(D.FNRepeatLengthCM,0)) AS FNRepeatLengthCM"
        _Str &= vbCrLf & ",MAX(ISNULL(D.FNTotalRepeat,0)) AS FNTotalRepeat"

        '_Str &= vbCrLf & ",SUM(ISNULL(D.FNGrandNetAmt,D.FNNetAmt)) AS FNGrandNetAmt"
        _Str &= vbCrLf & "  ,  (Convert(numeric(18,2),SUM(D.FNQuantity) *  D.FNPrice) + MAX(ISNULL(D.FNSurchangeAmt,0))  ) AS FNGrandNetAmt"
        _Str &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON D.FNHSysUnitId = U.FNHSysUnitId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & " WHERE        (D.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(PoKey) & "')"
        _Str &= vbCrLf & " GROUP BY     D.FNHSysRawMatId, M.FTRawMatCode,C.FTRawMatColorCode, S.FTRawMatSizeCode,D.FTFabricFrontSize, D.FNHSysUnitId, U.FTUnitCode, D.FNPrice, D.FNDisPer, "
        _Str &= vbCrLf & "  D.FNDisAmt,C.FNRawMatColorSeq, S.FNRawMatSizeSeq"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , ISNULL(D.FTRawMatColorNameTH,'') "
        Else
            _Str &= vbCrLf & " , ISNULL(D.FTRawMatColorNameEN,'') "
        End If

        _Str &= vbCrLf & " ORDER BY M.FTRawMatCode, C.FTRawMatColorCode, S.FNRawMatSizeSeq "

        _dtdetail = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        Me.ogcsum.DataSource = _dtdetail.Copy

        _dtdetail.Dispose()

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

        lblpomo.Visible = False

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

    Private Sub ClearSourcing(Optional OrderNo As String = "", Optional RawmatID As Integer = 0)
        Dim _Qry As String = ""

        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
        _Qry &= vbCrLf & " SET FTPurchaseNo='' "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "

        If OrderNo <> "" Then
            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
            _Qry &= vbCrLf & " AND FNHSysRawMatId=" & RawmatID & " "
        End If

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)


        Try
            _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTOrderThread_Resource "
            _Qry &= vbCrLf & " SET FTPurchaseNo='' "
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "

            If OrderNo <> "" Then
                _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                _Qry &= vbCrLf & " AND FNHSysRawMatId=" & RawmatID & " "
            End If

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception

        End Try

        _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "

        _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "

        If OrderNo <> "" Then

            _Qry &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
            _Qry &= vbCrLf & " AND FNHSysRawMatId=" & RawmatID & " "

        End If

        _Qry &= vbCrLf & " AND FNHSysUnitId=0 AND FNUsedQuantity =0 "

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

    End Sub

    Private Function SaveData(Optional RevisedRemark As String = "", Optional StateRcv As Boolean = False) As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _CmpH As String = ""
        Dim FNHSysCmpIdDelivery As Integer = 0

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
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK) WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
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

            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)

            Dim cmdstring As String = ""
            Dim dtdelivery As DataTable


            cmdstring = "SELECT TOP 1 FNHSysCmpId,FNHSysCmpIdTo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery  WITH(NOLOCK)  WHERE FNHSysDeliveryId=" & Val(FNHSysDeliveryId.Properties.Tag.ToString) & " "
            dtdelivery = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            For Each R As DataRow In dtdelivery.Rows

                FNHSysCmpIdDelivery = Val(R!FNHSysCmpId.ToString)

            Next

            dtdelivery.Dispose()

            If (_PORunDocVat) Then

                If FNVatPer.Value > 0 Then
                    _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, "POC-").ToString
                Else
                    _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, "POW-").ToString
                End If


            Else
                Dim cmprunpo As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPORun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun  WITH(NOLOCK)  WHERE FNHSysCmpRunId=" & Val(FNHSysCmpRunId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_MASTER, "")

                If cmprunpo = "" Then
                    cmprunpo = Microsoft.VisualBasic.Left(FNHSysCmpRunId.Text, 1)
                End If

                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp  WITH(NOLOCK)  WHERE FNHSysCmpId=" & FNHSysCmpIdDelivery.ToString & " ", Conn.DB.DataBaseName.DB_MASTER, "")

                If HI.ST.SysInfo.CmpID = 1306010001 Then
                    _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & "H" & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                Else
                    _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                End If
            End If


            Me.FTPurchaseState.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " MANUAL " & HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) & " " & Format(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM), "HH:mm:ss")

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
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select
                            End If
                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
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
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FTUpdUser")
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
                                    Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
                                        _Val = Obj.Text
                                    Case Else
                                        _Val = Obj.Text
                                End Select

                            End If

                        Next

                        If Not (_FoundControl) Then
                            Select Case UCase(_FieldName)
                                Case UCase("FDUpdDate"), UCase("FTUpdDate"), UCase("FTUpdTime"), UCase("FDInsDate"), UCase("FTInsDate"), UCase("FTInsTime"), UCase("FTInsUser"), UCase("FTUpdUser")
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
                                Case UCase("FDUpdDate"), UCase("FTUpdDate")
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
                    '_Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ",FNHSysDocCmpId) VALUES (" & _Values & "," & FNHSysCmpIdDelivery & ")"

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

                Select Case Obj.GetType.FullName.ToString.ToUpper
                    Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select

            Next

            If Not (StateRcv) Then

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                _Str &= vbCrLf & "  ,FTSendAppBy='' "
                _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                _Str &= vbCrLf & "  ,FTStatePDF='0' "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                FTStateSendApp.Checked = False
                FTStateSuperVisorApp.Checked = False
                FTStateManagerApp.Checked = False

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If

            End If

            'If RevisedRemark <> "" Then

            '    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
            '    _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
            '    _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
            '    _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
            '    _Str &= vbCrLf & ")"
            '    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            '    _Str &= vbCrLf & ", ISNULL(("
            '    _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
            '    _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
            '    _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            '    _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
            '    _Str &= vbCrLf & "),0) +1 "
            '    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '    _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(RevisedRemark) & "'"

            '    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '        HI.Conn.SQLConn.Tran.Rollback()
            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '        Return False
            '    End If

            'End If

            _Str = " UPDATE  SC SET "
            _Str &= vbCrLf & " FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN SC.FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE SC.FNHSysSuplId END)  ELSE SC.FNHSysSuplId END "
            _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P INNER JOIN"
            _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS SC ON P.FTOrderNo = SC.FTOrderNo AND P.FNHSysRawMatId = SC.FNHSysRawMatId AND P.FTPurchaseNo = SC.FTPurchaseNo"
            _Str &= vbCrLf & "  WHERE  (P.FTPurchaseNo =N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
            _Str &= vbCrLf & "  AND FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Try

                _Str = " UPDATE A SET FTPurchaseRefNo='' "
                _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS A  "
                _Str &= vbCrLf & "  WHERE FTPurchaseRefNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

            Catch ex As Exception
            End Try
            'Try
            '    Dim cmdstring As String = ""

            '    cmdstring = " DELETE  A FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo AS A  "
            '    cmdstring &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase AS B ON A.FTFacPurchaseNo=B.FTFacPurchaseNo "
            '    cmdstring &= vbCrLf & "  WHERE  B.FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            '    cmdstring &= vbCrLf & "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            '    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            'Catch ex As Exception
            'End Try

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'")
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

        _dt.Dispose()
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
        lblpomo.Visible = False
        _FormLoad = False
    End Sub

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click


        'Dim _Str As String = ""
        '_Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
        '_Str &= vbCrLf & "    FROM"
        '_Str &= vbCrLf & " ("
        '_Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
        '_Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
        '_Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        '_Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

        'Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

        '_Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
        '_Str &= vbCrLf & "  SET FTStateSendApp='0' "
        '_Str &= vbCrLf & "  ,FTSendAppBy='' "
        '_Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
        '_Str &= vbCrLf & "  ,FTSuperVisorName='' "
        '_Str &= vbCrLf & "  ,FTStateManagerApp='0' "
        '_Str &= vbCrLf & "  ,FTSuperManagerName='' "
        '_Str &= vbCrLf & "  ,FTStatePDF='0' "
        '_Str &= vbCrLf & "  ,FNPoAmt=" & FNPoAmt.Value & ""
        '_Str &= vbCrLf & "  ,FNDisCountPer=" & FNDisCountPer.Value & ""
        '_Str &= vbCrLf & "  ,FNDisCountAmt=" & FNDisCountAmt.Value & ""
        '_Str &= vbCrLf & "  ,FNPONetAmt=" & FNPONetAmt.Value & ""
        '_Str &= vbCrLf & "  ,FNVatPer=" & FNVatPer.Value & ""
        '_Str &= vbCrLf & "  ,FNVatAmt=" & FNVatAmt.Value & ""
        '_Str &= vbCrLf & "  ,FNSurcharge=" & FNSurcharge.Value & ""
        '_Str &= vbCrLf & "  ,FNPOGrandAmt=" & FNPOGrandAmt.Value & ""
        '_Str &= vbCrLf & "  ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtTH.Text) & "' "
        '_Str &= vbCrLf & "  ,FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtEN.Text) & "' "

        '_Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
        'HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

        If FTPurchaseNo.Text <> "" Then

            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub


        End If

        Dim _StateNotReState As Boolean = True
        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then
            Dim _Qry As String = ""
            Dim _RevisedRemark As String = ""

            '_Qry = "SELECT TOP 1  FTStateManagerApp "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            '_Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then

            If CheckStateRevised() Then

                With _RevisedPopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else
                        _RevisedRemark = .FTRemark.Text.Trim()
                    End If

                End With

            End If

            If _RevisedRemark <> "" Then

                _Qry = "  SELECT    TOP 1     FTPurchaseNo  "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                _Qry &= vbCrLf & "  AND  ( FNPOGrandAmt<>" & FNPOGrandAmt.Value & ""
                _Qry &= vbCrLf & "  OR   FNExchangeRate<>" & FNExchangeRate.Value & ")"
                _StateNotReState = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "")

            End If

            If Me.SaveData(_RevisedRemark, _StateNotReState) Then
                'FTStateSendApp.Checked = False
                'FTStateSuperVisorApp.Checked = False
                'FTStateManagerApp.Checked = False

                _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'  "
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                Me.LoadDataInfo(Me.FTPurchaseNo.Text)
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If

        If CheckOwner() = False Then Exit Sub

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTPurchaseNo.Text, Me.Text) = True Then
            If Me.DeleteData() Then
                Call ClearSourcing()
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
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then

            'Dim cmdstring As String = "select top 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)   WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTStateSendApp='1' AND FTStateSuperVisorApp='1' "


            'Dim pStateComplete As Boolean = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "") = "1")

            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If Me.FNPoState.SelectedIndex = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text

                '.ShowExport = pStateComplete
                '.ShowPrint = pStateComplete

                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .AddParameter("Draft", "DRAFT")
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub ocmprint_Click(sender As Object, e As EventArgs) Handles ocmprint.Click
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
            With New HI.RP.Report

                'Dim cmdstring As String = "select top 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)   WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTStateSendApp='1' AND FTStateSuperVisorApp='1' "

                'Dim pStateComplete As Boolean = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "") = "1")

                'If pStateComplete = False Then

                '    HI.MG.ShowMsg.mInfo("ลายเซ็นต์ ยังไม่ครบ 2 ลายเซ็นต์ ไม่สามารถทำการ ", 2204015497, Me.Text,, MessageBoxIcon.Warning)
                '    Exit Sub

                'End If

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If Me.FNPoState.SelectedIndex = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text

                '.ShowExport = pStateComplete
                '.ShowPrint = pStateComplete

                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang

                Dim Qry As String
                Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FTStatePrint='1'"
                Qry &= vbCrLf & ",FTPrintBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                Qry &= vbCrLf & ",FTPrintDate=" & HI.UL.ULDate.FormatDateDB & " "
                Qry &= vbCrLf & ",FTPrintTime=" & HI.UL.ULDate.FormatTimeDB & " "
                Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_PUR)

                Me.FTStatePrint.Checked = True

            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function CheckStateRevised() As Boolean
        Dim _Qry As String
        _Qry = "SELECT TOP 1  FTStateManagerApp "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

        Return (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1")
    End Function
#End Region

#Region " Proc "

    Private Sub LoadPriceHistory()
        Dim _Qry As String = ""
        Dim dt As DataTable
        _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_Purchase_History_ListPrice '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & HI.ST.Lang.Language & " "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogcpricehistory.DataSource = dt.Copy
        dt.Dispose()

        Call InitialGridMergCell()

    End Sub

    Private Sub LoadRevisedHistory()
        Dim _Qry As String = ""
        Dim dt As DataTable

        _Qry = "SELECT FNRevisedSeq, FTPurchaseRevisedBy"
        _Qry &= vbCrLf & " ,CASE WHEN ISDATE(FTRevisedDate) = 1 THEN  Convert(nvarchar(10), Convert(datetime,FTRevisedDate) ,103)  ELSE '' END AS FTRevisedDate "
        _Qry &= vbCrLf & " , FTRevisedTime, FTNote "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq ASC "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        Me.ogcrevised.DataSource = dt.Copy
        dt.Dispose()

    End Sub

    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvpricehistory.Columns

            Select Case c.FieldName.ToString
                Case "FTPurchaseNo", "FTRawmatCode", "FTRawMatName", "FTRawMatColorCode", "FTRawMatSizeCode", "FTUnitCode", "FNNetPrice", "FTCurCode"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select

        Next

    End Sub

    Private Sub ogvmrp_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvpricehistory.CellMerge
        Try
            With Me.ogvpricehistory
                If .GetRowCellValue(e.RowHandle1, "FTRawMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTRawMatCode").ToString Then

                    If e.Column.FieldName = "FNNetPrice" Then
                        If .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId2").ToString = .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Else
                        e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                        e.Handled = True
                    End If

                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With

        Catch ex As Exception
        End Try

    End Sub

#End Region

#Region " Variable "

#End Region

    Private Sub Form_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            lblpomo.Visible = False
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

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click
        AddItemMulti()
    End Sub



    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                  FNPoAmt.EditValueChanged,
                                                                                  FNDisCountAmt.EditValueChanged,
                                                                                  FNVatPer.EditValueChanged,
                                                                                  FNVatAmt.EditValueChanged,
                                                                                  FNSurcharge.EditValueChanged

        Static _Proc As Boolean

        If Not (_Proc) And Not (_ProcLoad) Then
            _Proc = True
            Dim _POAmt As Double = FNPoAmt.Value

            If _POAmt = 0 Then
                FNDisCountAmt.Value = 0
                FNVatAmt.Value = 0
            End If

            Dim _DisPer As Double = FNDisCountPer.Value
            Dim _DisAmt As Double = FNDisCountAmt.Value
            Dim _VatPer As Double = FNVatPer.Value
            Dim _VatAmt As Double = FNVatAmt.Value
            Dim _SurAmt As Double = FNSurcharge.Value

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper
                    _DisPer = FNDisCountPer.Value
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt
                Case "FNDisCountAmt".ToUpper
                    _DisAmt = FNDisCountAmt.Value

                    If _POAmt > 0 Then
                        _DisPer = Format((_DisAmt * 100) / _POAmt, HI.ST.Config.PercentFormat)
                    Else
                        _DisPer = 0
                    End If
                    FNDisCountPer.Value = _DisPer
                Case "FNVatPer".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
                Case "FNVatAmt".ToUpper
                    _VatAmt = FNVatAmt.Value

                    If (_POAmt - _DisAmt) > 0 Then
                        _VatPer = Format((_VatAmt * 100) / (_POAmt - _DisAmt), HI.ST.Config.PercentFormat)
                    Else
                        _VatPer = 0
                    End If
                    FNVatPer.Value = _VatPer
                Case Else
                    _DisAmt = Format((_POAmt * _DisPer) / 100, HI.ST.Config.AmtFormat)
                    FNDisCountAmt.Value = _DisAmt

                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            Me.FNPONetAmt.Value = (_POAmt - _DisAmt)

            Select Case sender.Name.ToString.ToUpper
                Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                    _VatPer = FNVatPer.Value
                    _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                    FNVatAmt.Value = _VatAmt
            End Select

            FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)

            _Proc = False
        End If
    End Sub

    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        If CheckOwner() = False Then Exit Sub
        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _CheckRcv As Boolean = False
            Dim _MatID As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysRawMatId").ToString
            Dim _JobNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString
            Dim _Amount As String = "" & .GetRowCellValue(.FocusedRowHandle, "FNNetAmt").ToString
            Dim _FNSurchangeAmt As Double = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSurchangeAmt").ToString)

            If (CheckReceive(Me.FTPurchaseNo.Text, _MatID) = False) Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text, , False) = False) Then
                _CheckRcv = True
            End If

            If (CheckReservePurchase(Me.FTPurchaseNo.Text, _JobNo, _MatID) = False) Then Exit Sub

            Dim _Qry As String = ""
            Dim _RevisedRemark As String = ""

            If Not (_CheckRcv) Then

                '_Qry = "SELECT TOP 1  FTStateManagerApp "
                '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
                '_Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then
                If CheckStateRevised() Then
                    With _RevisedPopup
                        .FTRemark.Text = ""
                        .StateProc = False
                        .ocmok.Enabled = True
                        .ocmcancel.Enabled = True
                        .ShowDialog()

                        If .StateProc = False Then
                            Exit Sub
                        Else
                            _RevisedRemark = .FTRemark.Text.Trim()
                        End If

                    End With
                End If

            End If

            Dim _Str As String = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_JobNo) & "' AND FNHSysRawMatId=" & Val(_MatID) & " "


            If HI.Conn.SQLConn.ExecuteOnly(_Str, _DBEnum) = True Then

                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Str)

                Try
                    _Str = "UPDATE A SET FTPurchaseRefNo='' "
                    _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Request_OrderNo AS A  "
                    _Str &= vbCrLf & "   WHERE FTPurchaseRefNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    _Str &= vbCrLf & "   AND FTOrderNo='" & HI.UL.ULF.rpQuoted(_JobNo) & "' AND FNHSysRawMatId=" & Val(_MatID) & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                Catch ex As Exception
                End Try

                Call ClearSourcing(_JobNo, Integer.Parse(Val(_MatID)))

                If _FNSurchangeAmt > 0 Then

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Str &= vbCrLf & " SET FNSurchangePerUnit = CASE WHEN " & _FNSurchangeAmt & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & _FNSurchangeAmt & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(_MatID) & " "
                    _Str &= vbCrLf & " ),1))  END "
                    _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((FNPrice - FNDisAmt) +  "
                    _Str &= vbCrLf & " ( CASE WHEN " & _FNSurchangeAmt & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & _FNSurchangeAmt & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(_MatID) & " "
                    _Str &= vbCrLf & " ),1))  END ) "
                    _Str &= vbCrLf & " ))"
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(_MatID) & " "
                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                End If


                _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                _Str &= vbCrLf & "    FROM"
                _Str &= vbCrLf & " ("
                _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))


                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Str &= vbCrLf & "  SET FNPoAmt=" & FNPoAmt.Value & ""
                _Str &= vbCrLf & "  ,FNDisCountPer=" & FNDisCountPer.Value & ""
                _Str &= vbCrLf & "  ,FNDisCountAmt=" & FNDisCountAmt.Value & ""
                _Str &= vbCrLf & "  ,FNPONetAmt=" & FNPONetAmt.Value & ""
                _Str &= vbCrLf & "  ,FNVatPer=" & FNVatPer.Value & ""
                _Str &= vbCrLf & "  ,FNVatAmt=" & FNVatAmt.Value & ""
                _Str &= vbCrLf & "  ,FNSurcharge=" & FNSurcharge.Value & ""
                _Str &= vbCrLf & "  ,FNPOGrandAmt=" & FNPOGrandAmt.Value & ""
                _Str &= vbCrLf & "  ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtTH.Text) & "' "
                _Str &= vbCrLf & "  ,FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtEN.Text) & "' "


                If Not (_CheckRcv) Then

                    _Str &= vbCrLf & "  ,FTStateSendApp='0' "
                    _Str &= vbCrLf & "  ,FTSendAppBy='' "
                    _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                    _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                    _Str &= vbCrLf & "  ,FTStatePDF='0' "


                    FTStateSendApp.Checked = False
                    FTStateSuperVisorApp.Checked = False
                    FTStateManagerApp.Checked = False


                End If

                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"


                If _RevisedRemark <> "" Then

                    _Str &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
                    _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
                    _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
                    _Str &= vbCrLf & ")"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    _Str &= vbCrLf & ", ISNULL(("
                    _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
                    _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                    _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
                    _Str &= vbCrLf & "),0) +1 "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(_RevisedRemark) & "'"


                End If

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)


                _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'  "
                HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                'Me.SaveData(_RevisedRemark, _CheckRcv)
                Me.LoadPoDetail(Me.FTPurchaseNo.Text)

            End If
        End With

    End Sub

    Private Sub FNDocNetAmt_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNPOGrandAmt.EditValueChanged

        If Not (_ProcLoad) Then
            Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
            Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)
        End If

    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpId2 As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0
            Dim _FNHSysTeamGrpIdTo2 As Integer = 0

            Dim dt As DataTable
            _Qry = "SELECT TOP 1  FNHSysTeamGrpId,FNHSysTeamGrpIdTo  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTPurchaseBy.Text) & "' "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)


            For Each R As DataRow In dt.Rows
                _FNHSysTeamGrpId = Val(R!FNHSysTeamGrpId.ToString())
                _FNHSysTeamGrpId2 = Val(R!FNHSysTeamGrpIdTo.ToString())
                Exit For
            Next

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId,FNHSysTeamGrpIdTo  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry2, Conn.DB.DataBaseName.DB_SECURITY)

            For Each R As DataRow In dt.Rows
                _FNHSysTeamGrpIdTo = Val(R!FNHSysTeamGrpId.ToString())
                _FNHSysTeamGrpIdTo2 = Val(R!FNHSysTeamGrpIdTo.ToString())
                Exit For
            Next

            If _FNHSysTeamGrpId > 0 Then

                If ((_FNHSysTeamGrpId = _FNHSysTeamGrpIdTo) OrElse (_FNHSysTeamGrpId = _FNHSysTeamGrpIdTo2)) OrElse ((_FNHSysTeamGrpId2 = _FNHSysTeamGrpIdTo And _FNHSysTeamGrpId2 > 0) OrElse (_FNHSysTeamGrpId2 = _FNHSysTeamGrpIdTo2 And _FNHSysTeamGrpId2 > 0)) Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If


        End If

    End Function

    Private Function CheckReservePurchase(POKey As String, OrderNo As String, SysMatId As Integer, Optional StateCheckDetail As Boolean = True) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""
        _Str = "SELECT TOP  1  FTPurchaseNo "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTReservePurchase AS A WITH(NOLOCK)"
        _Str &= vbCrLf & "   WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

        If StateCheckDetail Then
            _Str &= vbCrLf & "   AND FNHSysRawMatId=" & SysMatId & " "
            _Str &= vbCrLf & "   AND  (FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' OR FTOrderNoTo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' )"

        End If

        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
            HI.MG.ShowMsg.mProcessError(1403151211, "มีการจองยอดแล้วไม่สามารถทำการลบหรือแก้ไขข้อมูลได้ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            _Pass = False
        End If

        Return _Pass
    End Function

    Private Function CheckReceive(POKey As String, Optional SysMatId As Integer = 0, Optional ShowMsg As Boolean = True) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""

        If SysMatId = 0 Then
            _Str = "Select TOP 1 H.FTPurchaseNo "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H WITH(NOLOCK), [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D "
            _Str &= vbCrLf & " WHERE H.FTReceiveNo= D.FTReceiveNo AND H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "

            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                If ShowMsg Then
                    HI.MG.ShowMsg.mProcessError(1303150001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                End If

                _Pass = False
            End If

        Else
            _Str = "Select TOP 1 H.FTPurchaseNo "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive As H WITH(NOLOCK), [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS D "
            _Str &= vbCrLf & " WHERE H.FTReceiveNo= D.FTReceiveNo AND H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "
            _Str &= vbCrLf & " AND FNHSysRawMatId=" & SysMatId & ""
            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                If ShowMsg Then
                    HI.MG.ShowMsg.mProcessError(1401260001, "พบการรับ Item นี้แล้ว ", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
                End If

                _Pass = False
            End If
        End If

        Return _Pass
    End Function

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

            FNExchangeRate.Properties.ReadOnly = True
            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 0
                Dim _Qry As String = ""

                _Qry = " SELECT TOP 1 FNBuyingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDPurchaseDate.Text) & "')"
                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "'"
                _Qry &= vbCrLf & "  ))"

                FNExchangeRate.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

                If FNExchangeRate.Value <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการกำหนด Exchange Rate กรุณาทำการติดต่อ ผู้มีหน้าที่บันทึก !!!", 1410080015, Me.Text, , MessageBoxIcon.Warning)
                End If

            End If

        End If
    End Sub

    Private Sub ogvdetail_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvdetail.CellMerge
        Try
            With Me.ogvdetail
                Select Case e.Column.FieldName
                    Case "FNSurchangeAmt"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString) Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvdetail.DoubleClick
        Call EditGridDataMultijob()
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTStateSuperVisorApp<>'2' AND FTStateManagerApp<>'2' "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Qry &= vbCrLf & "  ,FTSuperVisorName='' "
                _Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Qry &= vbCrLf & "  ,FTSuperManagerName='' "
                _Qry &= vbCrLf & "  ,FTStatePDF='0' "
                _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                'Call CreatePOFactory(Me.FTPurchaseNo.Text)

            End If

            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub CreatePOFactory(pokey As String)
        Try
            Dim pofacno As String = ""
            Dim cmdstring = ""
            Dim FNHSysCmpIdTo As Integer = 0
            Dim FNHSysCmpId As Integer = 0
            Dim dt As DataTable
            Dim SysCurId As Integer = 1310200002
            Dim ExcRate As Double = 1

            cmdstring = "SELECT TOP 1 FNHSysCmpId,FNHSysCmpIdTo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery  WITH(NOLOCK)  WHERE FNHSysDeliveryId=" & Val(FNHSysDeliveryId.Properties.Tag.ToString()) & " "
            dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            For Each R As DataRow In dt.Rows
                FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
                FNHSysCmpIdTo = Val(R!FNHSysCmpIdTo.ToString)
            Next

            cmdstring = "SELECT TOP 1 FTFacPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE FTPurchaseNoRef='" & HI.UL.ULF.rpQuoted(pokey) & "'"
            pofacno = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "")

            If FNHSysCmpId > 0 And FNHSysCmpIdTo > 0 Then


                If FNHSysCmpIdTo = 1311090006 Or FNHSysCmpIdTo = 1311090005 Or FNHSysCmpIdTo = 1410220001 Or FNHSysCmpIdTo = 1501190001 Then
                    SysCurId = 1310190001

                    Dim _Qry As String = ""

                    _Qry = " SELECT TOP 1 FNBuyingRate"
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                    _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDPurchaseDate.Text) & "')"
                    _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                    _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                    _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE FTCurCode='USD'"
                    _Qry &= vbCrLf & "  ))"

                    ExcRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "1")))

                End If

                If pofacno = "" Then

                    pofacno = "F" & pokey

                    cmdstring = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase ("
                    cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo, FDFacPurchaseDate, FTFacPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
                    cmdstring &= vbCrLf & "     FNHSysCmpRunId, FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId, FNHSysCurId, FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge, FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,"
                    cmdstring &= vbCrLf & "     FNPoType, FTPurchaseNoRef, FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & ")"
                    cmdstring &= vbCrLf & " SELECT  FTInsUser, FDInsDate, FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTPurchaseState, FTRefer, FNPoState, FNHSysPurGrpId,"
                    cmdstring &= vbCrLf & "     FNHSysCmpRunId," & FNHSysCmpId & " AS  FNHSysCmpId, FDDeliveryDate, FNHSysCrTermId, FNCreditDay, FNHSysTermOfPMId," & SysCurId & " AS  FNHSysCurId, " & ExcRate & " AS FNExchangeRate, FNHSysDeliveryId, FTContactPerson, FDSampleAppDate, FDSignDate, "
                    cmdstring &= vbCrLf & "     FDBLDate, FDSuplCfmDliDate, FDCfmDate, FTRemark, FNPoAmt, FNDisCountPer, FNDisCountAmt, FNPONetAmt, FNVatPer, FNVatAmt, FNSurcharge , FNPOGrandAmt, FTPOGrandAmtTH, FTPOGrandAmtEN, "
                    cmdstring &= vbCrLf & "    FTStateSendApp, FTSendAppBy, FTSendAppDate, FTSendAppTime,  "
                    cmdstring &= vbCrLf & "     FNPoType,'" & HI.UL.ULF.rpQuoted(pokey) & "' AS FTPurchaseNoRef," & FNHSysCmpIdTo & " AS  FNHSysCmpIdCreate"
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                Else

                    cmdstring = "UPDATE A Set "
                    cmdstring &= vbCrLf & "  FTInsUser=B.FTInsUser"
                    cmdstring &= vbCrLf & ", FDInsDate=B.FDInsDate"
                    cmdstring &= vbCrLf & ", FTInsTime=B.FTInsTime"
                    cmdstring &= vbCrLf & ", FTRefer=B.FTRefer"
                    cmdstring &= vbCrLf & ", FNPoState=B.FNPoState"
                    cmdstring &= vbCrLf & ", FNHSysPurGrpId=B.FNHSysPurGrpId"
                    cmdstring &= vbCrLf & ", FNHSysCmpRunId=B.FNHSysCmpRunId"
                    cmdstring &= vbCrLf & ", FNHSysCmpId=B.FNHSysCmpId"
                    cmdstring &= vbCrLf & ", FDDeliveryDate=B.FDDeliveryDate"
                    cmdstring &= vbCrLf & ", FNHSysCrTermId=B.FNHSysCrTermId"
                    cmdstring &= vbCrLf & ", FNCreditDay=B.FNCreditDay"
                    cmdstring &= vbCrLf & ", FNHSysTermOfPMId=B.FNHSysTermOfPMId"
                    cmdstring &= vbCrLf & ", FNHSysCurId=" & SysCurId & ""
                    cmdstring &= vbCrLf & ", FNExchangeRate=" & ExcRate & ""
                    cmdstring &= vbCrLf & ", FNHSysDeliveryId=B.FNHSysDeliveryId"
                    cmdstring &= vbCrLf & ", FTContactPerson=B.FTContactPerson"
                    cmdstring &= vbCrLf & ", FTRemark=B.FTRemark"
                    cmdstring &= vbCrLf & ", FNPoAmt=B.FNPoAmt"
                    cmdstring &= vbCrLf & ", FNDisCountPer=B.FNDisCountPer"
                    cmdstring &= vbCrLf & ", FNDisCountAmt=B.FNDisCountAmt"
                    cmdstring &= vbCrLf & ", FNPONetAmt=B.FNPONetAmt"
                    cmdstring &= vbCrLf & ", FNVatPer=B.FNVatPer"
                    cmdstring &= vbCrLf & ", FNVatAmt=B.FNVatAmt"
                    cmdstring &= vbCrLf & ", FNSurcharge=B.FNSurcharge"
                    cmdstring &= vbCrLf & ", FNPOGrandAmt=B.FNPOGrandAmt"
                    cmdstring &= vbCrLf & ", FTPOGrandAmtTH=B.FTPOGrandAmtTH"
                    cmdstring &= vbCrLf & ", FTPOGrandAmtEN=B.FTPOGrandAmtEN"
                    cmdstring &= vbCrLf & ", FTStateSendApp=B.FTStateSendApp"
                    cmdstring &= vbCrLf & ", FTSendAppBy=B.FTSendAppBy"
                    cmdstring &= vbCrLf & ", FTSendAppDate=B.FTSendAppDate"
                    cmdstring &= vbCrLf & ", FTSendAppTime=B.FTSendAppTime"
                    cmdstring &= vbCrLf & ", FNPoType=B.FNPoType"
                    cmdstring &= vbCrLf & ", FNHSysCmpIdCreate=" & FNHSysCmpIdTo & ""
                    cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As B"
                    cmdstring &= vbCrLf & " WHERE A.FTPurchaseNoRef = B.FTPurchaseNo"
                    cmdstring &= vbCrLf & " And A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                End If

                cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"
                cmdstring &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo ("
                cmdstring &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTFacPurchaseNo,  FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt, FTRemark, "
                cmdstring &= vbCrLf & "    FTFabricFrontSize, FNReservePOQuantity, FTRawMatColorNameTH, FTRawMatColorNameEN, FNSurchangeAmt, FNSurchangePerUnit, FNGrandNetAmt, FTOGacDate"
                cmdstring &= vbCrLf & ")"
                cmdstring &= vbCrLf & " Select  A.FTInsUser, A.FDInsDate, A.FTInsTime,'" & HI.UL.ULF.rpQuoted(pofacno) & "' AS FTFacPurchaseNo"
                cmdstring &= vbCrLf & ",  A.FTOrderNo, A.FNHSysRawMatId, A.FNHSysUnitId"
                cmdstring &= vbCrLf & ", (A.FNPrice + Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))) AS FNPrice , A.FNDisPer, A.FNDisAmt, A.FNQuantity, A.FNNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,4),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity)) AS FNNetAmt"
                cmdstring &= vbCrLf & ", A.FTRemark, "
                cmdstring &= vbCrLf & "  A.FTFabricFrontSize, A.FNReservePOQuantity, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN, A.FNSurchangeAmt, A.FNSurchangePerUnit, A.FNGrandNetAmt +  Convert(numeric(18,2),(Convert(numeric(18,2),((A.FNPrice * ISNULL(CH.FNChargePer,0))/100.00))  *A.FNQuantity))  AS FNGrandNetAmt, A.FTOGacDate"
                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A "
                cmdstring &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  ON A.FTPurchaseNo=H.FTPurchaseNo"
                cmdstring &= vbCrLf & "  INNER Join "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN "
                cmdstring &= vbCrLf & " (SELECT  *  "
                cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpMatCharge AS X WITH(NOLOCK)		"
                cmdstring &= vbCrLf & "  Where X.FNHSysCmpId = " & FNHSysCmpIdTo & ""
                cmdstring &= vbCrLf & "   ) As CH On MM.FNMerMatType = CH.FNMerMatType AND H.FNPoState=CH.FNPoType"

                cmdstring &= vbCrLf & " WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(pokey) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                cmdstring = " UPDATE B SET "
                cmdstring &= vbCrLf & "B.FNPrice = Convert(numeric(18,2),(B.FNPrice *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNDisAmt = Convert(numeric(18,2),(B.FNDisAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & " , B.FNNetAmt = Convert(numeric(18,2),(B.FNNetAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNSurchangeAmt = Convert(numeric(18,2),(B.FNSurchangeAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNSurchangePerUnit = Convert(numeric(18,2),(B.FNSurchangePerUnit *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & ", B.FNGrandNetAmt = Convert(numeric(18,2),(B.FNGrandNetAmt *C.FNExchangeRate)/A.FNExchangeRate )"
                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A INNER Join"
                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As B On A.FTFacPurchaseNo = B.FTFacPurchaseNo INNER Join"
                cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As C On A.FTPurchaseNoRef = C.FTPurchaseNo "
                cmdstring &= vbCrLf & " WHERE A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                Dim poamt As Double = 0
                Dim podisamt As Double = CDbl(Format((FNDisCountAmt.Value * FNExchangeRate.Value) / ExcRate, "0.00"))
                Dim ponetamt As Double = 0
                Dim povatamt As Double = 0
                Dim pograndamt As Double = 0
                Dim poamtth As String
                Dim poamten As String
                Dim Surcharge As Double = CDbl(Format((FNSurcharge.Value * FNExchangeRate.Value) / ExcRate, "0.00"))

                cmdstring = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) As NETAMT"

                cmdstring &= vbCrLf & "    FROM"
                cmdstring &= vbCrLf & " ("
                cmdstring &= vbCrLf & " Select        FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) As FNQuantity,ISNULL(FNSurchangeAmt,0) As FNSurchangeAmt"
                cmdstring &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo As A  With(NOLOCK)"
                cmdstring &= vbCrLf & " WHERE FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "' "
                cmdstring &= vbCrLf & " GROUP BY FTFacPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                poamt = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0"))
                ponetamt = poamt - podisamt
                povatamt = CDbl(Format((ponetamt * FNVatPer.Value) / 100.0, "0.00"))
                pograndamt = ponetamt + povatamt + Surcharge

                poamten = HI.UL.ULF.Convert_Bath_EN(pograndamt)
                poamtth = HI.UL.ULF.Convert_Bath_TH(pograndamt)

                cmdstring = "UPDATE A Set "
                cmdstring &= vbCrLf & "  FNPoAmt=" & poamt & ""
                cmdstring &= vbCrLf & ", FNPONetAmt=" & ponetamt & ""
                cmdstring &= vbCrLf & ", FNVatAmt=" & povatamt & ""
                cmdstring &= vbCrLf & ", FNPOGrandAmt=" & pograndamt & ""
                cmdstring &= vbCrLf & ", FNSurcharge=" & Surcharge & ""
                cmdstring &= vbCrLf & ", FNDisCountAmt=" & podisamt & ""
                cmdstring &= vbCrLf & ", FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(poamtth) & "'"
                cmdstring &= vbCrLf & ", FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(poamten) & "'"

                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase As A "
                cmdstring &= vbCrLf & "WHERE  A.FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            Else

                If pofacno <> "" Then

                    cmdstring = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""
                    cmdstring &= vbCrLf & " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase_OrderNo WHERE  FTFacPurchaseNo='" & HI.UL.ULF.rpQuoted(pofacno) & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                End If

            End If
        Catch ex As Exception

        End Try



    End Sub
    Private Sub oxtb_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles oxtb.SelectedPageChanging

        ocmadd.Visible = (e.Page.Name = otpdetailitem.Name)
        ocmremove.Visible = (e.Page.Name = otpdetailitem.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

        Select Case e.Page.Name
            Case otppurchasepricehistory.Name
                Call LoadPriceHistory()
            Case otprevisedhistory.Name
                Call LoadRevisedHistory()
            Case Else

        End Select
    End Sub

    Private Sub ogvpricehistory_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvpricehistory.RowCellStyle
        Try
            With Me.ogvpricehistory
                Try
                    If "" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString <> "" Then
                        If IsNumeric("" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString) Then
                            If .GetRowCellValue(e.RowHandle, "FNHSysRawMatId2").ToString = .GetRowCellValue(e.RowHandle, "FNHSysRawMatId").ToString Then
                                If Not (e.Appearance.ForeColor = System.Drawing.Color.Red) Then
                                    If CDbl("" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString) < CDbl("" & .GetRowCellValue(e.RowHandle, "FNNetPrice").ToString) Then
                                        e.Appearance.ForeColor = System.Drawing.Color.Red
                                    Else
                                        e.Appearance.ForeColor = System.Drawing.Color.Green
                                    End If
                                End If
                            End If
                        End If
                    End If
                Catch ex As Exception

                End Try
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo(FTPurchaseNo.Properties.Tag.ToString)
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With Me.ogvdetail

                Try

                    Select Case .GetRowCellValue(e.RowHandle, "FTStateRcv").ToString
                        Case "1"
                            e.Appearance.ForeColor = System.Drawing.Color.Green
                        Case "2"
                            e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                        Case "3"
                            e.Appearance.ForeColor = System.Drawing.Color.DarkOrchid
                    End Select
                    'If .GetRowCellValue(e.RowHandle, "FTStateRcv") = "1" Then
                    '    e.Appearance.ForeColor = System.Drawing.Color.Green
                    'End If

                    If .GetRowCellValue(e.RowHandle, "FTStateReserve") = "1" Then
                        e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                    End If

                    If .GetRowCellValue(e.RowHandle, "FNReservePOQuantity") = "1" Then
                        e.Appearance.ForeColor = System.Drawing.Color.OrangeRed
                    End If

                Catch ex As Exception
                End Try

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdetail.RowCountChanged

    End Sub

    Private Sub ogbmainprocbutton_Paint(sender As Object, e As PaintEventArgs) Handles ogbmainprocbutton.Paint

    End Sub

    Private Sub ocmmail_Click(sender As Object, e As EventArgs) Handles ocmmail.Click

        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then

            Dim _FTMail As String = ""
            Dim _Sql As String = ""

            _Sql = "Select TOP 1 FTMail "
            _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier With(NOLOCK) "
            _Sql &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & ""
            _FTMail = HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_MASTER, "")

            Dim _Spls As New HI.TL.SplashScreen("Creating....Mail Please Wait.")
            Try

                Dim OutlookMessage As outlook.MailItem
                Dim AppOutlook As New outlook.Application
                Dim objNS As outlook._NameSpace = AppOutlook.Session
                Dim objFolder As outlook.MAPIFolder
                objFolder = objNS.GetDefaultFolder(outlook.OlDefaultFolders.olFolderDrafts)

                Try
                    OutlookMessage = AppOutlook.CreateItem(outlook.OlItemType.olMailItem)

                    With OutlookMessage
                        .To = _FTMail
                        .CC = ""
                        .Subject = FTPurchaseNo.Text
                        .Body = Me.FTRemark.Text

                        Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                        Try
                            With New HI.RP.Report

                                If Me.FNPoState.SelectedIndex = 0 Then
                                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                                Else
                                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                                End If

                                .FormTitle = Me.Text
                                .ExportFile = RP.Report.ExFile.PDF
                                .ExportName = FTPurchaseNo.Text


                                Try
                                    If Directory.Exists("C:\HISOFTPDF") = False Then
                                        Directory.CreateDirectory("C:\HISOFTPDF")
                                    End If
                                Catch ex As Exception

                                End Try
                                .PathExport = "C:\HISOFTPDF"
                                .ReportFolderName = "PurchaseOrder\"
                                .ReportName = "PurchaseOrder.rpt"
                                .AddParameter("Draft", "DRAFT")
                                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                                .PrevieNoSplash()

                            End With
                        Catch ex As Exception
                        End Try

                        HI.ST.Lang.Language = _tmplang

                        Try
                            .Attachments.Add("C:\HISOFTPDF\" & FTPurchaseNo.Text & ".pdf")
                        Catch ex As Exception
                        End Try
                        _Spls.Close()
                        .Display(True)

                    End With

                    Dim Qry As String
                    Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FTStateSendMail='1'"
                    Qry &= vbCrLf & ",FTSendMailBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    Qry &= vbCrLf & ",FTSendMailDate=" & HI.UL.ULDate.FormatDateDB & " "
                    Qry &= vbCrLf & ",FTSendMailTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    Qry &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_PUR)
                    Me.FTStateSendMail.Checked = True

                    Try
                        If File.Exists("C:\HISOFTPDF\" & FTPurchaseNo.Text & ".pdf") = True Then
                            File.Delete("C:\HISOFTPDF\" & FTPurchaseNo.Text & ".pdf")
                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("เนื่องจากพบข้อผิดพลาดบางประการ ระบบจึงไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280001, Me.Text, , MessageBoxIcon.Warning)
                Finally
                    OutlookMessage = Nothing
                    AppOutlook = Nothing
                End Try



            Catch ex As Exception
                _Spls.Close()
                HI.MG.ShowMsg.mInfo("ไม่พบ Microsoft Outlook ไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280002, Me.Text, , MessageBoxIcon.Warning)
            End Try


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

    Private Sub ocmcalculate_Click(sender As Object, e As EventArgs) Handles ocmcalculate.Click
        If FTPurchaseNo.Text <> "" Then
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub
        End If

        If CheckOwner() = False Then Exit Sub
        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการคำนวณเพื่อปัดยอดสั่งซื้อต่อวัตถุดิบเป็นจำนวนเต็มใช่หรือไม่?", 1502259108, Me.FTPurchaseNo.Text) = True Then
            Dim _Str As String
            Dim _dtdiff As DataTable
            Dim _FNSurchangeAmt As Double

            '_Str = " SELECt FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity,FNQuantity%1 AS FNQuantityDiff , 1-(FNQuantity%1) AS FNQuantityAdd,FNSurchangeAmt"
            '_Str &= vbCrLf & "    FROM"
            '_Str &= vbCrLf & "  (SELECT P.FTPurchaseNo, P.FNHSysRawMatId, MAX(P.FTOrderNo) AS FTOrderNo, SUM(P.FNQuantity) AS FNQuantity,Max(ISNULL(P.FNSurchangeAmt,0)) AS FNSurchangeAmt,ISNULL(Sub.FTStatePromo,'') AS FTStatePromo"
            '_Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P"
            '_Str &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 'PROMOTIONAL' AS FTStatePromo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK) WHERE Sub.FTOrderNo = P.FTOrderNo AND  Sub.FNHSysBuyGrpId = 1405080002  )  AS Sub "
            '_Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
            '_Str &= vbCrLf & "  GROUP BY P.FTPurchaseNo, P.FNHSysRawMatId,ISNULL(Sub.FTStatePromo,'')) AS A "
            '_Str &= vbCrLf & "  WHERE FNQuantity % 1 >0"

            '_Str = " SELECt FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity,FNQuantity%1 AS FNQuantityDiff , 1-(FNQuantity%1) AS FNQuantityAdd,FNSurchangeAmt"
            '_Str &= vbCrLf & "    FROM"
            '_Str &= vbCrLf & "  (SELECT P.FTPurchaseNo, P.FNHSysRawMatId, MAX(P.FTOrderNo) AS FTOrderNo, SUM(P.FNQuantity) AS FNQuantity,Max(ISNULL(P.FNSurchangeAmt,0)) AS FNSurchangeAmt,CASE WHEN ISNULL(Sub.FTStatePromo,'') ='' THEN CASE WHEN ODX.FNOrderType =5 THEN 'QRS' WHEN ODX.FNOrderType =999 THEN 'QPP' ELSE '' END  ELSE ISNULL(Sub.FTStatePromo,'') END  AS FTStatePromo"
            '_Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P"
            '_Str &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 'PROMOTIONAL' AS FTStatePromo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS Sub WITH(NOLOCK) WHERE Sub.FTOrderNo = P.FTOrderNo AND  Sub.FNHSysBuyGrpId = 1405080002  )  AS Sub "

            '_Str &= vbCrLf & "    OUTER APPLY(SELECT TOP 1 ODX.FNOrderType  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_OrderProdAndSMPPurchase AS ODX  WHERE ODX.FTOrderNo = P.FTOrderNo  )  AS ODX "

            '_Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
            '_Str &= vbCrLf & "  GROUP BY P.FTPurchaseNo, P.FNHSysRawMatId,CASE WHEN ISNULL(Sub.FTStatePromo,'') ='' THEN CASE WHEN ODX.FNOrderType =5 THEN 'QRS' WHEN ODX.FNOrderType =999 THEN 'QPP' ELSE '' END  ELSE ISNULL(Sub.FTStatePromo,'') END ) AS A "
            '_Str &= vbCrLf & "  WHERE FNQuantity % 1 >0"


            _Str = " SELECt FTPurchaseNo,FNHSysRawMatId,FTOrderNo,FNQuantity,FNQuantity%1 AS FNQuantityDiff , 1-(FNQuantity%1) AS FNQuantityAdd,FNSurchangeAmt"
            _Str &= vbCrLf & "    FROM"
            _Str &= vbCrLf & "  (SELECT P.FTPurchaseNo, P.FNHSysRawMatId, MAX(P.FTOrderNo) AS FTOrderNo, SUM(P.FNQuantity) AS FNQuantity,Max(ISNULL(P.FNSurchangeAmt,0)) AS FNSurchangeAmt,P.FTStatePromo"
            _Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURPurchase_OrderNo AS P"
            _Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
            _Str &= vbCrLf & "  GROUP BY P.FTPurchaseNo, P.FNHSysRawMatId,P.FTStatePromo ) AS A "
            _Str &= vbCrLf & "  WHERE FNQuantity % 1 >0"


            _dtdiff = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PUR)

            If _dtdiff.Rows.Count > 0 Then

                For Each R As DataRow In _dtdiff.Rows
                    _Str = "   UPDATE   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Str &= vbCrLf & " SET FNQuantity=FNQuantity+" & Double.Parse(Val(R!FNQuantityAdd.ToString)) & ""
                    _Str &= vbCrLf & ",FNNetAmt=Convert(numeric(18, 2), (FNQuantity+" & Double.Parse(Val(R!FNQuantityAdd.ToString)) & ") * (FNPrice - FNDisAmt))"
                    _Str &= vbCrLf & "  WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & "  AND FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Str &= vbCrLf & "  AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "

                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                    _FNSurchangeAmt = Val(R!FNSurchangeAmt.ToString)
                    If _FNSurchangeAmt > 0 Then

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        _Str &= vbCrLf & " SET FNSurchangePerUnit = CASE WHEN " & _FNSurchangeAmt & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & _FNSurchangeAmt & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                        _Str &= vbCrLf & " ),1))  END "
                        _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((FNPrice - FNDisAmt) +  "
                        _Str &= vbCrLf & " ( CASE WHEN " & _FNSurchangeAmt & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & _FNSurchangeAmt & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                        _Str &= vbCrLf & " ),1))  END ) "
                        _Str &= vbCrLf & " ))"
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                    End If

                Next

                _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                _Str &= vbCrLf & "    FROM"
                _Str &= vbCrLf & " ("
                _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))


                Me.SaveData()
                Call LoadDataInfo(Me.FTPurchaseNo.Text)

                HI.MG.ShowMsg.mInfo("ระบบทำการปัดยอดไเรียบร้อยแล้ว !!!", 1502259110, Me.Text, , MessageBoxIcon.Information)

            Else

                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่สามารถทำการปัดยอดได้ !!!", 1502259109, Me.Text, , MessageBoxIcon.Warning)

            End If

            _dtdiff.Dispose()

        End If
    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged

    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As Object, e As EventArgs) Handles FNExchangeRate.EditValueChanged

    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged

    End Sub

    Private Sub ogvsum_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvsum.CellMerge
        Try
            With Me.ogvsum
                Select Case e.Column.FieldName
                    Case "FNSurchangeAmt"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FNHSysRawMatId").ToString = "" & .GetRowCellValue(e.RowHandle2, "FNHSysRawMatId").ToString) Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If


                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AddItem()
        If CheckOwner() = False Then Exit Sub
        Dim _CmpH As String = ""
        Dim _RevisedRemark As String = ""

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

        If (CheckReservePurchase(Me.FTPurchaseNo.Text, "", 0, False) = False) Then Exit Sub

        If FTPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then

            If Me.VerrifyData() Then

                If Me.SaveData Then
                Else
                    Exit Sub
                End If

            Else

                Exit Sub

            End If

        Else

            If Me.FTPurchaseNo.Text = "" Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub

            Dim _Qry As String = ""

            '_Qry = "SELECT TOP 1  FTStateManagerApp "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            '_Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then
            If CheckStateRevised() Then
                With _RevisedPopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else
                        _RevisedRemark = .FTRemark.Text.Trim()
                    End If
                End With

                'If Me.SaveData(_RevisedRemark) Then
                'Else
                '    Exit Sub
                'End If

            End If



        End If

        With _AddItemPopup
            .AddMat = False
            .PONO = FTPurchaseNo.Text

            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)
            HI.TL.HandlerControl.ClearControl(_AddItemPopup)

            .FTPurchaseNo.Text = FTPurchaseNo.Text
            .FTOrderNo.Properties.ReadOnly = False
            .FTOrderNo.Properties.Buttons(0).Enabled = True
            .FNHSysRawMatId.Properties.Buttons(0).Enabled = True
            .FTOGacDate.Text = ""
            .ocmcancel.Enabled = True
            .ShowDialog()

            If (.AddMat) Then
                Dim _Str As String = ""
                Dim _FTRawMatColorNameTH As String = ""
                Dim _FTRawMatColorNameEN As String = ""
                Dim _dtRawMatColor As New DataTable

                _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
                _Str &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                _Str &= vbCrLf & " AND RM.FNHSysRawMatId =" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                Try
                    _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                    If _dtRawMatColor.Rows.Count > 0 Then

                        For Each R As DataRow In _dtRawMatColor.Rows

                            _FTRawMatColorNameTH = R!FTRawMatColorNameTH.ToString
                            _FTRawMatColorNameEN = R!FTRawMatColorNameEN.ToString

                            Exit For
                        Next

                    Else
                        _FTRawMatColorNameTH = .FTRawMatColorNameTH.Text.Trim()
                        _FTRawMatColorNameEN = .FTRawMatColorNameEN.Text.Trim()
                    End If

                Catch ex As Exception
                End Try

                _dtRawMatColor.Dispose()

                _Str = "SELECT TOP 1 FTPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") = "" Then

                    _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                    _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FNSurchangeAmt,FTOGacDate)"
                    _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & "," & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                    _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                    _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                    _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                    _Str &= vbCrLf & "," & .FNNetAmt.Value & " "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                    _Str &= vbCrLf & "," & .FNSurchangeAmt.Value & " "
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "

                Else

                    _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                    _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                    _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                    _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                    _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                    _Str &= vbCrLf & " ,FNNetAmt=" & .FNNetAmt.Value & ""
                    _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                    _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                    _Str &= vbCrLf & " ,FTRawMatColorNameTH='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                    _Str &= vbCrLf & " ,FTRawMatColorNameEN='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                    _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                    _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                End If

                Try

                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Exit Sub
                    End If

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                    _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                    _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                    _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                    _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                    _Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                    _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                    _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                    _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                    _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                    _Str &= vbCrLf & ",FNSurchangePerUnit = CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & " ),1))  END "
                    _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((" & (.FNPOPrice.Value - .FNDisAmt.Value) & ") +  "

                    _Str &= vbCrLf & " ( CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                    _Str &= vbCrLf & " ),1))  END ) "

                    _Str &= vbCrLf & " ))"

                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                    '_Str &= vbCrLf & " AND FTOrderNo<>'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        'Exit Sub
                    End If

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                    _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " ,FNTotalPurchaseQuantity=" & .FNPOQuantity.Value & " "
                    _Str &= vbCrLf & " ,FNPricePurchase=" & .FNPOPrice.Value & " "
                    _Str &= vbCrLf & " ,FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE FNHSysSuplId END)  ELSE FNHSysSuplId END "
                    _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " AND  (ISNULL(FTPurchaseNo,'') =''  OR  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  )  "

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then

                        _Str = " SELECT TOP 1 ISNULL(FTPurchaseNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing WITH(NOLOCK) "
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                        If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_PUR, "") = "" Then

                            _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                            _Str &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity, FNHSysUnitId, FNPrice, FTStateNominate, FDDateSC, FTPurchaseNo, FNHSysSuplId, "
                            _Str &= vbCrLf & "   FNSCQuantity, FNSCPlusQuantity, FNTotalPurchaseQuantity, FNHSysUnitIdPurchase"
                            _Str &= vbCrLf & ", FNPricePurchase, FNHSysCurId, FNStateChange, FTFabricFrontSize)"
                            _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "','' "
                            _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysRawMatId.Properties.Tag.ToString)) & ",0,0,0,0,'0'"
                            _Str &= vbCrLf & ",Convert(varchar(10),Getdate(),111)"
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                            _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & ",0,0," & .FNPOQuantity.Value & ""
                            _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString)) & ""
                            _Str &= vbCrLf & "," & .FNPOPrice.Value & ""
                            _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ",0,'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"

                            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        End If

                    End If


                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    FTStateSendApp.Checked = False
                    FTStateSuperVisorApp.Checked = False
                    FTStateManagerApp.Checked = False

                    _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                    _Str &= vbCrLf & "    FROM"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                    _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                    Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                    _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                    _Str &= vbCrLf & "  ,FTSendAppBy='' "
                    _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                    _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                    _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                    _Str &= vbCrLf & "  ,FTStatePDF='0' "
                    _Str &= vbCrLf & "  ,FNPoAmt=" & FNPoAmt.Value & ""
                    _Str &= vbCrLf & "  ,FNDisCountPer=" & FNDisCountPer.Value & ""
                    _Str &= vbCrLf & "  ,FNDisCountAmt=" & FNDisCountAmt.Value & ""
                    _Str &= vbCrLf & "  ,FNPONetAmt=" & FNPONetAmt.Value & ""
                    _Str &= vbCrLf & "  ,FNVatPer=" & FNVatPer.Value & ""
                    _Str &= vbCrLf & "  ,FNVatAmt=" & FNVatAmt.Value & ""
                    _Str &= vbCrLf & "  ,FNSurcharge=" & FNSurcharge.Value & ""
                    _Str &= vbCrLf & "  ,FNPOGrandAmt=" & FNPOGrandAmt.Value & ""
                    _Str &= vbCrLf & "  ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtTH.Text) & "' "
                    _Str &= vbCrLf & "  ,FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtEN.Text) & "' "

                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"


                    If _RevisedRemark <> "" Then

                        _Str &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
                        _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
                        _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
                        _Str &= vbCrLf & ")"
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        _Str &= vbCrLf & ", ISNULL(("
                        _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
                        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                        _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
                        _Str &= vbCrLf & "),0) +1 "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(_RevisedRemark) & "'"


                    End If

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)


                    '  Me.SaveData()

                    Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try
            End If
        End With
    End Sub

    Private Sub AddItemMulti(Optional StatePOHistory As Boolean = False)
        If CheckOwner() = False Then Exit Sub
        Dim _CmpH As String = ""
        Dim _RevisedRemark As String = ""
        Dim StateFirstAdd As Boolean = False


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

        If (CheckReservePurchase(Me.FTPurchaseNo.Text, "", 0, False) = False) Then Exit Sub

        If FTPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then

            If Me.VerrifyData() Then

                If Me.SaveData Then
                Else
                    Exit Sub
                End If

            Else

                Exit Sub

            End If

        Else

            If Me.FTPurchaseNo.Text = "" Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub

            Dim _Qry As String = ""

            If CheckStateRevised() Then
                With _RevisedPopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else
                        _RevisedRemark = .FTRemark.Text.Trim()
                    End If
                End With

            End If

        End If

        If StatePOHistory Then

        Else
            Dim _Str As String = ""

            If (_PORunDocVat) Then

            Else

                _Str = "select Count(1) AS FNCount FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) WHERE   FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

                StateFirstAdd = (Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_PUR, "0")) = 0)

            End If

            With _AddItemPopupMultijob
                .AddMat = False
                .PONO = FTPurchaseNo.Text
                .FTPurchaseNo.Text = FTPurchaseNo.Text


                HI.TL.HandlerControl.ClearControl(_AddItemPopupMultijob,, {"FTOrderNo", "FNMerMatTypePOX", "FNOrderType"})

                .FTPurchaseNo.Text = FTPurchaseNo.Text
                .FTOrderNo.Properties.ReadOnly = False
                .FTOrderNo.Properties.Buttons(0).Enabled = True
                .FNHSysRawmatId.Enabled = True
                .FTOGacDate.Text = ""
                .ocmcancel.Enabled = True
                .ShowDialog()

                If (.AddMat) Then

                    Dim _FTRawMatColorNameTH As String = .FTRawMatColorNameTH.Text
                    Dim _FTRawMatColorNameEN As String = .FTRawMatColorNameEN.Text
                    Dim RawMatId As Integer = Val(.CXFNHSysRawmatId_Hide.Value)

                    Try

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt=" & .FNNetAmt.Value & ""
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " ,FTRawMatColorNameTH='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                        _Str &= vbCrLf & " ,FTRawMatColorNameEN='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                        _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                        _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & RawMatId & " "


                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then


                            _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                            _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                            _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FNSurchangeAmt,FTOGacDate)"
                            _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                            _Str &= vbCrLf & "," & RawMatId & " "
                            _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                            _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                            _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                            _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                            _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                            _Str &= vbCrLf & "," & .FNNetAmt.Value & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                            _Str &= vbCrLf & "," & .FNSurchangeAmt.Value & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "


                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Exit Sub
                            End If

                        End If



                        If Not (.ogcjob.DataSource Is Nothing) Then
                            Dim dtorder As DataTable

                            With CType(.ogcjob.DataSource, DataTable)
                                .AcceptChanges()
                                dtorder = .Copy
                            End With


                            For Each Rxi As DataRow In dtorder.Select("FTOrderNo<>'' AND FNQuantity>0")


                                _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                                _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                                _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                                _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                                _Str &= vbCrLf & " ,FNQuantity=" & Val(Rxi!FNQuantity.ToString) & ""
                                _Str &= vbCrLf & " ,FNNetAmt=" & Val(Rxi!FNNetAmt.ToString) & ""
                                _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                                _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                                _Str &= vbCrLf & " ,FTRawMatColorNameTH='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                                _Str &= vbCrLf & " ,FTRawMatColorNameEN='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                                _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                                _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "' "
                                _Str &= vbCrLf & " AND FNHSysRawMatId=" & RawMatId & " "


                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then


                                    _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                                    _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                                    _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FNSurchangeAmt,FTOGacDate)"
                                    _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString)) & "' "
                                    _Str &= vbCrLf & "," & RawMatId & " "
                                    _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                                    _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                                    _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                                    _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                                    _Str &= vbCrLf & "," & Val(Rxi!FNQuantity.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(Rxi!FNNetAmt.ToString) & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                                    _Str &= vbCrLf & "," & .FNSurchangeAmt.Value & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "


                                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Exit Sub
                                    End If

                                End If

                                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & " ,FNTotalPurchaseQuantity=" & Val(Rxi!FNQuantity.ToString) & " "
                                _Str &= vbCrLf & " ,FNPricePurchase=" & .FNPOPrice.Value & " "
                                _Str &= vbCrLf & " ,FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE FNHSysSuplId END)  ELSE FNHSysSuplId END "
                                _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "' "
                                _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " AND  (ISNULL(FTPurchaseNo,'') =''  OR  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  )  "

                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then
                                    _Str = " SELECT TOP 1 ISNULL(FTPurchaseNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                    _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "' "
                                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "

                                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_PUR, "") = "" Then
                                        _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                        _Str &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity, FNHSysUnitId, FNPrice, FTStateNominate, FDDateSC, FTPurchaseNo, FNHSysSuplId, "
                                        _Str &= vbCrLf & "   FNSCQuantity, FNSCPlusQuantity, FNTotalPurchaseQuantity, FNHSysUnitIdPurchase"
                                        _Str &= vbCrLf & ", FNPricePurchase, FNHSysCurId, FNStateChange, FTFabricFrontSize)"
                                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "','' "
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(RawMatId)) & ",0,0,0,0,'0'"
                                        _Str &= vbCrLf & ",Convert(varchar(10),Getdate(),111)"
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & ",0,0," & Val(Rxi!FNQuantity.ToString) & ""
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString)) & ""
                                        _Str &= vbCrLf & "," & .FNPOPrice.Value & ""
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ",0,'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"

                                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                                    End If

                                End If

                            Next

                        End If

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                        _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                        _Str &= vbCrLf & ",FNSurchangePerUnit = CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "
                        _Str &= vbCrLf & " ),1))  END "
                        _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((" & (.FNPOPrice.Value - .FNDisAmt.Value) & ") +  "

                        _Str &= vbCrLf & " ( CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "
                        _Str &= vbCrLf & " ),1))  END ) "

                        _Str &= vbCrLf & " ))"

                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "
                        '_Str &= vbCrLf & " AND FTOrderNo<>'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            'HI.Conn.SQLConn.Tran.Rollback()
                            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            'Exit Sub
                        End If

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                        _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " ,FNTotalPurchaseQuantity=" & .FNPOQuantity.Value & " "
                        _Str &= vbCrLf & " ,FNPricePurchase=" & .FNPOPrice.Value & " "
                        _Str &= vbCrLf & " ,FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE FNHSysSuplId END)  ELSE FNHSysSuplId END "
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " AND  (ISNULL(FTPurchaseNo,'') =''  OR  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  )  "

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then

                            _Str = " SELECT TOP 1 ISNULL(FTPurchaseNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing WITH(NOLOCK) "
                            _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                            _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_PUR, "") = "" Then

                                _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                _Str &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity, FNHSysUnitId, FNPrice, FTStateNominate, FDDateSC, FTPurchaseNo, FNHSysSuplId, "
                                _Str &= vbCrLf & "   FNSCQuantity, FNSCPlusQuantity, FNTotalPurchaseQuantity, FNHSysUnitIdPurchase"
                                _Str &= vbCrLf & ", FNPricePurchase, FNHSysCurId, FNStateChange, FTFabricFrontSize)"
                                _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "','' "
                                _Str &= vbCrLf & "," & Integer.Parse(Val(RawMatId)) & ",0,0,0,0,'0'"
                                _Str &= vbCrLf & ",Convert(varchar(10),Getdate(),111)"
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & ",0,0," & .FNPOQuantity.Value & ""
                                _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString)) & ""
                                _Str &= vbCrLf & "," & .FNPOPrice.Value & ""
                                _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ",0,'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"

                                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                            End If

                        End If


                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        FTStateSendApp.Checked = False
                        FTStateSuperVisorApp.Checked = False
                        FTStateManagerApp.Checked = False

                        _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                        _Str &= vbCrLf & "    FROM"
                        _Str &= vbCrLf & " ("
                        _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                        Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                        _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                        _Str &= vbCrLf & "  ,FTSendAppBy='' "
                        _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                        _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                        _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                        _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                        _Str &= vbCrLf & "  ,FTStatePDF='0' "
                        _Str &= vbCrLf & "  ,FNPoAmt=" & FNPoAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNDisCountPer=" & FNDisCountPer.Value & ""
                        _Str &= vbCrLf & "  ,FNDisCountAmt=" & FNDisCountAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNPONetAmt=" & FNPONetAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNVatPer=" & FNVatPer.Value & ""
                        _Str &= vbCrLf & "  ,FNVatAmt=" & FNVatAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNSurcharge=" & FNSurcharge.Value & ""
                        _Str &= vbCrLf & "  ,FNPOGrandAmt=" & FNPOGrandAmt.Value & ""
                        _Str &= vbCrLf & "  ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtTH.Text) & "' "
                        _Str &= vbCrLf & "  ,FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtEN.Text) & "' "

                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"


                        If _RevisedRemark <> "" Then

                            _Str &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
                            _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                            _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
                            _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
                            _Str &= vbCrLf & ")"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & ", ISNULL(("
                            _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                            _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
                            _Str &= vbCrLf & "),0) +1 "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(_RevisedRemark) & "'"


                        End If

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)

                        '  Me.SaveData()

                        If (_PORunDocVat) Then
                            _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'  "
                            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                            Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                        Else
                            If StateFirstAdd Then
                                Dim cmdstring As String = ""
                                Dim dtdelivery As DataTable
                                Dim CmpCreateOrderId As Integer = 0

                                cmdstring = " Select Top 1 A.FTOrderNo, B.FNHSysCmpId AS FNHSysCmpIdOrder "
                                cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderProdAndSMP As A With(NOLOCK) INNER Join"
                                cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS B WITH(NOLOCK) ON A.FNHSysCmpId = B.FNHSysCmpId"
                                cmdstring &= vbCrLf & "       INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo AS D1 WITH(NOLOCK) ON A.FTOrderNo = D1.FTOrderNo"

                                cmdstring &= vbCrLf & "    Where D1.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "

                                dtdelivery = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                                For Each Rx As DataRow In dtdelivery.Rows
                                    CmpCreateOrderId = Val(Rx!FNHSysCmpIdOrder.ToString)

                                    Exit For
                                Next

                                If CmpCreateOrderId > 0 Then
                                    Dim OldPoNo As String = Me.FTPurchaseNo.Text
                                    Dim NewPoNo As String = ""

                                    Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
                                    Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
                                    Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)

                                    Dim cmprunpo As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPORun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun  WITH(NOLOCK)  WHERE FNHSysCmpRunId=" & Val(FNHSysCmpRunId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_MASTER, "")

                                    If cmprunpo = "" Then
                                        cmprunpo = Microsoft.VisualBasic.Left(FNHSysCmpRunId.Text, 1)
                                    End If


                                    If CmpCreateOrderId > 0 Then
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(CmpCreateOrderId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                                    Else
                                        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                    End If

                                    If _CmpH <> Microsoft.VisualBasic.Left(Me.FTPurchaseNo.Text, _CmpH.Length) Then

                                        If HI.ST.SysInfo.CmpID = 1306010001 Then
                                            NewPoNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & "H" & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                                        Else
                                            NewPoNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                                        End If

                                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                                        _Str &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                                        _Str &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                                        _Str &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                                        _Str &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                                        _Str &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                                        _Str &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                        _Str &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                                        _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(NewPoNo) & "'  "
                                        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                                        Me.FTPurchaseNo.Text = NewPoNo

                                    Else

                                        _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'  "
                                        HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                                        Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                                    End If

                                Else

                                    _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'  "
                                    HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                                    Me.LoadPoDetail(Me.FTPurchaseNo.Text)
                                End If



                            Else

                                _Str = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_CHECKSPPO_CMPO '" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'  "
                                HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)

                                Me.LoadPoDetail(Me.FTPurchaseNo.Text)
                            End If
                        End If



                    Catch ex As Exception

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    End Try

                End If
            End With


        End If
    End Sub


    Private Sub EditGridData()
        With ogvdetail
            Dim _RevisedRemark As String = ""
            Dim _CheckRcv As Boolean = False
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim SysMatId As Integer = Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString)
            If (CheckReceive(Me.FTPurchaseNo.Text, SysMatId) = False) Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text, , False) = False) Then
                _CheckRcv = True
            End If

            If (CheckReservePurchase(Me.FTPurchaseNo.Text, "" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString, SysMatId) = False) Then Exit Sub
            If CheckOwner() = False Then Exit Sub

            Dim _Qry As String = ""

            '_Qry = "SELECT TOP 1  FTStateManagerApp "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            '_Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then
            If CheckStateRevised() Then
                With _RevisedPopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else
                        _RevisedRemark = .FTRemark.Text.Trim()
                    End If

                End With

            End If
            ' End If

            With _AddItemPopup
                .AddMat = False

                .PONO = FTPurchaseNo.Text


                HI.TL.HandlerControl.ClearControl(_AddItemPopup)

                .FTPurchaseNo.Text = FTPurchaseNo.Text
                .FTOrderNo.Text = "" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString

                Dim _tagFNHSysRawMatId As String = "" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString
                Dim ItemCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatCode").ToString
                Dim ColorCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatColorCode").ToString
                Dim SizeCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatSizeCode").ToString

                .FNHSysRawMatId.Properties.Tag = _tagFNHSysRawMatId
                .FTRawMatColorCode.Text = ColorCode
                .FTRawMatSizeCode.Text = SizeCode
                .FNHSysRawMatId.Text = ItemCode
                .FNHSysUnitIdPO.Text = "" & ogvdetail.GetFocusedRowCellValue("FTUnitCode").ToString
                .FNHSysUnitIdPO.Properties.Tag = "" & ogvdetail.GetFocusedRowCellValue("FNHSysUnitId").ToString
                .FNPOPrice.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNPrice").ToString)
                .FNDisPer.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNDisPer").ToString)
                .FNPOQuantity.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNQuantity").ToString)
                .FTRemark.Text = "" & ogvdetail.GetFocusedRowCellValue("FTRemark").ToString
                .FTFabricFrontSize.Text = "" & ogvdetail.GetFocusedRowCellValue("FTFabricFrontSize").ToString
                .FNSurchangeAmt.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNSurchangeAmt").ToString)
                .FTOGacDate.Text = "" & ogvdetail.GetFocusedRowCellValue("FTOGacDate").ToString

                .FTOrderNo.Properties.ReadOnly = _CheckRcv
                .FTOrderNo.Properties.Buttons(0).Enabled = Not (_CheckRcv)
                .FNHSysRawMatId.Properties.Buttons(0).Enabled = Not (_CheckRcv)
                .ocmcancel.Enabled = True
                .ShowDialog()

                If (.AddMat) Then
                    Dim _Str As String = ""

                    Dim _FTRawMatColorNameTH As String = ""
                    Dim _FTRawMatColorNameEN As String = ""
                    Dim _dtRawMatColor As New DataTable

                    _Str = "  SELECT TOP 1 A.FTOrderNo, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN"
                    _Str &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Mat_Color AS A WITH(NOLOCK) INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS RM WITH(NOLOCK)  ON A.FNHSysRawMatColorId = RM.FNHSysRawMatColorId INNER JOIN"
                    _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON A.FNHSysMainMatId = MM.FNHSysMainMatId AND RM.FTRawMatCode = MM.FTMainMatCode"
                    _Str &= vbCrLf & " WHERE A.FTOrderNo ='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND RM.FNHSysRawMatId =" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                    Try
                        _dtRawMatColor = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        If _dtRawMatColor.Rows.Count > 0 Then
                            For Each R As DataRow In _dtRawMatColor.Rows

                                _FTRawMatColorNameTH = R!FTRawMatColorNameTH.ToString
                                _FTRawMatColorNameEN = R!FTRawMatColorNameEN.ToString

                                Exit For
                            Next

                        Else
                            _FTRawMatColorNameTH = .FTRawMatColorNameTH.Text.Trim()
                            _FTRawMatColorNameEN = .FTRawMatColorNameEN.Text.Trim()
                        End If

                    Catch ex As Exception
                    End Try

                    _dtRawMatColor.Dispose()

                    _Str = "SELECT TOP 1 FTPurchaseNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo WITH(NOLOCK) "
                    _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                    _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                    If HI.Conn.SQLConn.GetField(_Str, _DBEnum, "") = "" Then

                        _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                        _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FNSurchangeAmt,FTOGacDate)"
                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & "," & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                        _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                        _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                        _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                        _Str &= vbCrLf & "," & .FNNetAmt.Value & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                        _Str &= vbCrLf & "," & .FNSurchangeAmt.Value & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                    Else

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt=" & .FNNetAmt.Value & ""
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & ",FTRawMatColorNameTH='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                        _Str &= vbCrLf & ",FTRawMatColorNameEN='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                        _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                        _Str &= vbCrLf & ",FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "

                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                    End If

                    Try
                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Exit Sub
                        End If

                        '_Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        '_Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        '_Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        '_Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        '_Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        '_Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                        '_Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        '_Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        '_Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        '_Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                        _Str &= vbCrLf & ",FNSurchangePerUnit = CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & " ),1))  END "
                        _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((" & (.FNPOPrice.Value - .FNDisAmt.Value) & ") +  "

                        _Str &= vbCrLf & " ( CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        _Str &= vbCrLf & " ),1))  END ) "

                        _Str &= vbCrLf & " ))"
                        _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "
                        ' _Str &= vbCrLf & " AND FTOrderNo<>'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                        _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " ,FNTotalPurchaseQuantity=" & .FNPOQuantity.Value & " "
                        _Str &= vbCrLf & " ,FNPricePurchase=" & .FNPOPrice.Value & " "
                        _Str &= vbCrLf & " ,FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE FNHSysSuplId END)  ELSE FNHSysSuplId END "
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " AND  (ISNULL(FTPurchaseNo,'') =''  OR  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  )  "

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then
                            _Str = " SELECT TOP 1 ISNULL(FTPurchaseNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                            _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                            _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(.FNHSysRawMatId.Properties.Tag.ToString) & " "

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_PUR, "") = "" Then
                                _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                _Str &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity, FNHSysUnitId, FNPrice, FTStateNominate, FDDateSC, FTPurchaseNo, FNHSysSuplId, "
                                _Str &= vbCrLf & "   FNSCQuantity, FNSCPlusQuantity, FNTotalPurchaseQuantity, FNHSysUnitIdPurchase"
                                _Str &= vbCrLf & ", FNPricePurchase, FNHSysCurId, FNStateChange, FTFabricFrontSize)"
                                _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "','' "
                                _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysRawMatId.Properties.Tag.ToString)) & ",0,0,0,0,'0'"
                                _Str &= vbCrLf & ",Convert(varchar(10),Getdate(),111)"
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & ",0,0," & .FNPOQuantity.Value & ""
                                _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString)) & ""
                                _Str &= vbCrLf & "," & .FNPOPrice.Value & ""
                                _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ",0,'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"

                                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            End If

                        End If


                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                        _Str &= vbCrLf & "    FROM"
                        _Str &= vbCrLf & " ("
                        _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                        Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))



                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                        _Str &= vbCrLf & "  SET FNPoAmt=" & FNPoAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNDisCountPer=" & FNDisCountPer.Value & ""
                        _Str &= vbCrLf & "  ,FNDisCountAmt=" & FNDisCountAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNPONetAmt=" & FNPONetAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNVatPer=" & FNVatPer.Value & ""
                        _Str &= vbCrLf & "  ,FNVatAmt=" & FNVatAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNSurcharge=" & FNSurcharge.Value & ""
                        _Str &= vbCrLf & "  ,FNPOGrandAmt=" & FNPOGrandAmt.Value & ""
                        _Str &= vbCrLf & "  ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtTH.Text) & "' "
                        _Str &= vbCrLf & "  ,FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtEN.Text) & "' "


                        If Not (_CheckRcv) Then

                            _Str &= vbCrLf & "  ,FTStateSendApp='0' "
                            _Str &= vbCrLf & "  ,FTSendAppBy='' "
                            _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                            _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                            _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                            _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                            _Str &= vbCrLf & "  ,FTStatePDF='0' "


                            FTStateSendApp.Checked = False
                            FTStateSuperVisorApp.Checked = False
                            FTStateManagerApp.Checked = False


                        End If

                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"


                        If _RevisedRemark <> "" Then

                            _Str &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
                            _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                            _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
                            _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
                            _Str &= vbCrLf & ")"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & ", ISNULL(("
                            _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                            _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
                            _Str &= vbCrLf & "),0) +1 "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(_RevisedRemark) & "'"


                        End If

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)


                        ' Me.SaveData(_RevisedRemark, _CheckRcv)

                        Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                    Catch ex As Exception

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    End Try

                End If
            End With
        End With
    End Sub

    Private Sub EditGridDataMultijob()
        With ogvdetail
            Dim _RevisedRemark As String = ""
            Dim _CheckRcv As Boolean = False
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim SysMatId As Integer = Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString)
            If (CheckReceive(Me.FTPurchaseNo.Text, SysMatId) = False) Then Exit Sub
            If (CheckReceive(Me.FTPurchaseNo.Text, , False) = False) Then
                _CheckRcv = True
            End If

            If (CheckReservePurchase(Me.FTPurchaseNo.Text, "" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString, SysMatId) = False) Then Exit Sub
            If CheckOwner() = False Then Exit Sub

            Dim _Qry As String = ""

            '_Qry = "SELECT TOP 1  FTStateManagerApp "
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            '_Qry &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

            'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") = "1" Then
            If CheckStateRevised() Then
                With _RevisedPopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else
                        _RevisedRemark = .FTRemark.Text.Trim()
                    End If

                End With

            End If
            ' End If

            With _AddItemPopupMultijob
                .AddMat = False

                .PONO = FTPurchaseNo.Text
                .FTPurchaseNo.Text = FTPurchaseNo.Text
                ' .FTOrderNo.Text = ""
                .FNMerMatTypePOX.SelectedIndex = Val("" & ogvdetail.GetFocusedRowCellValue("FNMerMatTypePOX").ToString)
                .FTOrderNo.Text = "" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString
                ' .LoadMaterial("" & ogvdetail.GetFocusedRowCellValue("FTOrderNo").ToString, Val("" & ogvdetail.GetFocusedRowCellValue("FNMerMatTypePOX").ToString))
                Dim _tagFNHSysRawMatId As String = Val("" & ogvdetail.GetFocusedRowCellValue("FNHSysRawMatId").ToString)
                Dim ItemCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatCode").ToString
                Dim ColorCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatColorCode").ToString
                Dim SizeCode As String = "" & ogvdetail.GetFocusedRowCellValue("FTRawMatSizeCode").ToString
                '.FNHSysRawmatId.Text = ItemCode

                .FTRawMatColorCode.Text = ColorCode
                .FTRawMatSizeCode.Text = SizeCode

                .SetSelectItem(_tagFNHSysRawMatId)
                '.FNHSysRawmatId.EditValue = Nothing
                '.FNHSysRawmatId.EditValue = _tagFNHSysRawMatId

                .FNHSysUnitIdPO.Text = "" & ogvdetail.GetFocusedRowCellValue("FTUnitCode").ToString
                .FNHSysUnitIdPO.Properties.Tag = "" & ogvdetail.GetFocusedRowCellValue("FNHSysUnitId").ToString
                .FNPOPrice.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNPrice").ToString)
                .FNDisPer.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNDisPer").ToString)
                .FNPOQuantity.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNQuantity").ToString)
                .FTRemark.Text = "" & ogvdetail.GetFocusedRowCellValue("FTRemark").ToString
                .FTFabricFrontSize.Text = "" & ogvdetail.GetFocusedRowCellValue("FTFabricFrontSize").ToString
                .FNSurchangeAmt.Value = Val("" & ogvdetail.GetFocusedRowCellValue("FNSurchangeAmt").ToString)
                .FTOGacDate.Text = "" & ogvdetail.GetFocusedRowCellValue("FTOGacDate").ToString
                .ocmcancel.Enabled = True

                .ShowDialog()

                If (.AddMat) Then
                    Dim _Str As String = ""

                    Dim _FTRawMatColorNameTH As String = .FTRawMatColorNameTH.Text
                    Dim _FTRawMatColorNameEN As String = .FTRawMatColorNameEN.Text
                    Dim RawMatId As Integer = Val(.CXFNHSysRawmatId_Hide.Value)

                    Try
                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNQuantity=" & .FNPOQuantity.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt=" & .FNNetAmt.Value & ""
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " ,FTRawMatColorNameTH='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                        _Str &= vbCrLf & " ,FTRawMatColorNameEN='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                        _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                        _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & RawMatId & " "


                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then


                            _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                            _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                            _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FNSurchangeAmt,FTOGacDate)"
                            _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                            _Str &= vbCrLf & "," & RawMatId & " "
                            _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                            _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                            _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                            _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                            _Str &= vbCrLf & "," & .FNPOQuantity.Value & " "
                            _Str &= vbCrLf & "," & .FNNetAmt.Value & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                            _Str &= vbCrLf & "," & .FNSurchangeAmt.Value & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "

                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Exit Sub
                            End If

                        End If

                        If Not (.ogcjob.DataSource Is Nothing) Then
                            Dim dtorder As DataTable

                            With CType(.ogcjob.DataSource, DataTable)
                                .AcceptChanges()
                                dtorder = .Copy
                            End With


                            For Each Rxi As DataRow In dtorder.Select("FTOrderNo<>'' AND FNQuantity>0")




                                _Str = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & " ,FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                                _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                                _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                                _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                                _Str &= vbCrLf & " ,FNQuantity=" & Val(Rxi!FNQuantity.ToString) & ""
                                _Str &= vbCrLf & " ,FNNetAmt=" & Val(Rxi!FNNetAmt.ToString) & ""
                                _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                                _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                                _Str &= vbCrLf & " ,FTRawMatColorNameTH='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                                _Str &= vbCrLf & " ,FTRawMatColorNameEN='" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                                _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                                _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "' "
                                _Str &= vbCrLf & " AND FNHSysRawMatId=" & RawMatId & " "


                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then


                                    _Str = "Insert into  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo(FTInsUser, FDInsDate, FTInsTime"
                                    _Str &= vbCrLf & " , FTPurchaseNo,FTOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice, FNDisPer, "
                                    _Str &= vbCrLf & "    FNDisAmt, FNQuantity, FNNetAmt, FTRemark ,FTFabricFrontSize,FTRawMatColorNameTH,FTRawMatColorNameEN,FNSurchangeAmt,FTOGacDate)"
                                    _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString)) & "' "
                                    _Str &= vbCrLf & "," & RawMatId & " "
                                    _Str &= vbCrLf & "," & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & " "
                                    _Str &= vbCrLf & "," & .FNPOPrice.Value & " "
                                    _Str &= vbCrLf & "," & .FNDisPer.Value & " "
                                    _Str &= vbCrLf & "," & .FNDisAmt.Value & " "
                                    _Str &= vbCrLf & "," & Val(Rxi!FNQuantity.ToString) & " "
                                    _Str &= vbCrLf & "," & Val(Rxi!FNNetAmt.ToString) & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameTH) & "' "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTRawMatColorNameEN) & "' "
                                    _Str &= vbCrLf & "," & .FNSurchangeAmt.Value & " "
                                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "


                                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Exit Sub
                                    End If

                                End If


                                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & " ,FNTotalPurchaseQuantity=" & Val(Rxi!FNQuantity.ToString) & " "
                                _Str &= vbCrLf & " ,FNPricePurchase=" & .FNPOPrice.Value & " "
                                _Str &= vbCrLf & " ,FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE FNHSysSuplId END)  ELSE FNHSysSuplId END "
                                _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "' "
                                _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " AND  (ISNULL(FTPurchaseNo,'') =''  OR  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  )  "

                                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then
                                    _Str = " SELECT TOP 1 ISNULL(FTPurchaseNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                    _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "' "
                                    _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "

                                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_PUR, "") = "" Then
                                        _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                        _Str &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity, FNHSysUnitId, FNPrice, FTStateNominate, FDDateSC, FTPurchaseNo, FNHSysSuplId, "
                                        _Str &= vbCrLf & "   FNSCQuantity, FNSCPlusQuantity, FNTotalPurchaseQuantity, FNHSysUnitIdPurchase"
                                        _Str &= vbCrLf & ", FNPricePurchase, FNHSysCurId, FNStateChange, FTFabricFrontSize)"
                                        _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rxi!FTOrderNo.ToString) & "','' "
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(RawMatId)) & ",0,0,0,0,'0'"
                                        _Str &= vbCrLf & ",Convert(varchar(10),Getdate(),111)"
                                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & ",0,0," & Val(Rxi!FNQuantity.ToString) & ""
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString)) & ""
                                        _Str &= vbCrLf & "," & .FNPOPrice.Value & ""
                                        _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ",0,'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"

                                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                                    End If

                                End If

                            Next

                        End If

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo"
                        _Str &= vbCrLf & " SET FNHSysUnitId=" & Val(.FNHSysUnitIdPO.Properties.Tag.ToString) & ""
                        _Str &= vbCrLf & " ,FNPrice=" & .FNPOPrice.Value & ""
                        _Str &= vbCrLf & " ,FNDisPer=" & .FNDisPer.Value & ""
                        _Str &= vbCrLf & " ,FNDisAmt=" & .FNDisAmt.Value & ""
                        _Str &= vbCrLf & " ,FNNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * " & (.FNPOPrice.Value - .FNDisAmt.Value) & " )"
                        _Str &= vbCrLf & " ,FTRemark='" & HI.UL.ULF.rpQuoted(.FTRemark.Text) & "' "
                        _Str &= vbCrLf & " ,FTFabricFrontSize='" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "' "
                        _Str &= vbCrLf & " ,FNSurchangeAmt=" & .FNSurchangeAmt.Value & ""
                        _Str &= vbCrLf & ",FNSurchangePerUnit = CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,5)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "
                        _Str &= vbCrLf & " ),1))  END "
                        _Str &= vbCrLf & " ,FNGrandNetAmt= Convert(numeric(18," & Val(HI.ST.Config.AmtDigit) & "),FNQuantity * ((" & (.FNPOPrice.Value - .FNDisAmt.Value) & ") +  "

                        _Str &= vbCrLf & " ( CASE WHEN " & .FNSurchangeAmt.Value & " <= 0 THEN 0.0000 ELSE  Convert(numeric(18,4)," & .FNSurchangeAmt.Value & " / ISNULL(( SELECT SUM(FNQuantity) AS FNQuantity "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "
                        _Str &= vbCrLf & " ),1))  END ) "

                        _Str &= vbCrLf & " ))"
                        _Str &= vbCrLf & " ,FTOGacDate='" & HI.UL.ULF.rpQuoted(.FTOGacDate.Text) & "' "
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "
                        ' _Str &= vbCrLf & " AND FTOrderNo<>'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                        _Str &= vbCrLf & " SET FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " ,FNTotalPurchaseQuantity=" & .FNPOQuantity.Value & " "
                        _Str &= vbCrLf & " ,FNPricePurchase=" & .FNPOPrice.Value & " "
                        _Str &= vbCrLf & " ,FNHSysSuplId= CASE WHEN  " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " > 0 THEN (CASE WHEN FNHSysSuplId <> " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " THEN " & Integer.Parse(Val(FNHSysSuplId.Properties.Tag.ToString)) & " ELSE FNHSysSuplId END)  ELSE FNHSysSuplId END "
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                        _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " AND  (ISNULL(FTPurchaseNo,'') =''  OR  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'  )  "

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then

                            _Str = " SELECT TOP 1 ISNULL(FTPurchaseNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                            _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "' "
                            _Str &= vbCrLf & " AND FNHSysRawMatId=" & Val(RawMatId) & " "

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Str, Conn.DB.DataBaseName.DB_PUR, "") = "" Then

                                _Str = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                                _Str &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity, FNHSysUnitId, FNPrice, FTStateNominate, FDDateSC, FTPurchaseNo, FNHSysSuplId, "
                                _Str &= vbCrLf & "   FNSCQuantity, FNSCPlusQuantity, FNTotalPurchaseQuantity, FNHSysUnitIdPurchase"
                                _Str &= vbCrLf & ", FNPricePurchase, FNHSysCurId, FNStateChange, FTFabricFrontSize)"
                                _Str &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.FTOrderNo.Text) & "','' "
                                _Str &= vbCrLf & "," & Integer.Parse(Val(RawMatId)) & ",0,0,0,0,'0'"
                                _Str &= vbCrLf & ",Convert(varchar(10),Getdate(),111)"
                                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
                                _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysSuplId.Properties.Tag.ToString)) & ",0,0," & .FNPOQuantity.Value & ""
                                _Str &= vbCrLf & "," & Integer.Parse(Val(.FNHSysUnitIdPO.Properties.Tag.ToString)) & ""
                                _Str &= vbCrLf & "," & .FNPOPrice.Value & ""
                                _Str &= vbCrLf & "," & Integer.Parse(Val(Me.FNHSysCurId.Properties.Tag.ToString)) & ",0,'" & HI.UL.ULF.rpQuoted(.FTFabricFrontSize.Text) & "'"

                                HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            End If

                        End If

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        _Str = "      Select SUM(Convert(numeric(18, 2), FNQuantity * ((FNPrice - FNDisAmt) )) + FNSurchangeAmt ) AS NETAMT"
                        _Str &= vbCrLf & "    FROM"
                        _Str &= vbCrLf & " ("
                        _Str &= vbCrLf & " SELECT        FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt, SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"
                        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A  WITH(NOLOCK)"
                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNPrice, FNDisAmt,ISNULL(FNSurchangeAmt,0) ) AS A"

                        Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(_Str, _DBEnum, "0"))

                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                        _Str &= vbCrLf & "  SET FNPoAmt=" & FNPoAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNDisCountPer=" & FNDisCountPer.Value & ""
                        _Str &= vbCrLf & "  ,FNDisCountAmt=" & FNDisCountAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNPONetAmt=" & FNPONetAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNVatPer=" & FNVatPer.Value & ""
                        _Str &= vbCrLf & "  ,FNVatAmt=" & FNVatAmt.Value & ""
                        _Str &= vbCrLf & "  ,FNSurcharge=" & FNSurcharge.Value & ""
                        _Str &= vbCrLf & "  ,FNPOGrandAmt=" & FNPOGrandAmt.Value & ""
                        _Str &= vbCrLf & "  ,FTPOGrandAmtTH='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtTH.Text) & "' "
                        _Str &= vbCrLf & "  ,FTPOGrandAmtEN='" & HI.UL.ULF.rpQuoted(Me.FTPOGrandAmtEN.Text) & "' "

                        If Not (_CheckRcv) Then

                            _Str &= vbCrLf & "  ,FTStateSendApp='0' "
                            _Str &= vbCrLf & "  ,FTSendAppBy='' "
                            _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                            _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                            _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                            _Str &= vbCrLf & "  ,FTSuperManagerName='' "
                            _Str &= vbCrLf & "  ,FTStatePDF='0' "

                            FTStateSendApp.Checked = False
                            FTStateSuperVisorApp.Checked = False
                            FTStateManagerApp.Checked = False

                        End If

                        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                        If _RevisedRemark <> "" Then

                            _Str &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised ( "
                            _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime"
                            _Str &= vbCrLf & " , FTPurchaseNo, FNRevisedSeq, FTPurchaseRevisedBy"
                            _Str &= vbCrLf & ", FTRevisedDate, FTRevisedTime, FTNote"
                            _Str &= vbCrLf & ")"
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & ", ISNULL(("
                            _Str &= vbCrLf & "SELECT TOP 1 FNRevisedSeq "
                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                            _Str &= vbCrLf & "  WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                            _Str &= vbCrLf & " ORDER BY FNRevisedSeq DESC "
                            _Str &= vbCrLf & "),0) +1 "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(_RevisedRemark) & "'"

                        End If

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR)


                        ' Me.SaveData(_RevisedRemark, _CheckRcv)

                        Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                    Catch ex As Exception

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    End Try
                End If
            End With
        End With
    End Sub

    Private Sub ogvsum_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvsum.CustomColumnDisplayText

        Try

            Select Case e.Column.FieldName
                Case "FNRepeatLengthCM"
                    If Val(e.Value) = 0 Then
                        e.DisplayText = ""
                    End If
                Case "FNTotalRepeat"
                    If Val(e.Value) = 0 Then
                        e.DisplayText = ""
                    End If
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNListDocumentData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentData.SelectedIndex.ToString)
        End If
    End Sub

    Private Sub ocmadditemfrompohistory_Click(sender As Object, e As EventArgs) Handles ocmadditemfrompohistory.Click

    End Sub

    Private Sub ocmchengepono_Click(sender As Object, e As EventArgs) Handles ocmchengepono.Click
        If FTPurchaseNo.Text.Trim = "" Then
            Exit Sub
        End If


        If FTPurchaseNo.Text <> "" Then

            If (CheckReceive(Me.FTPurchaseNo.Text) = False) Then Exit Sub


        End If
        If CheckOwner() = False Then Exit Sub

        Dim _CmpH As String = ""

        Dim cmdstring As String = ""
        Dim dtdelivery As DataTable
        Dim CmpCreateOrderId As Integer = 0

        cmdstring = " Select Top 1 A.FTOrderNo, B.FNHSysCmpId AS FNHSysCmpIdOrder "
        cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderProdAndSMP As A With(NOLOCK) INNER Join"
        cmdstring &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS B WITH(NOLOCK) ON A.FNHSysCmpId = B.FNHSysCmpId"
        cmdstring &= vbCrLf & "       INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase_OrderNo AS D1 WITH(NOLOCK) ON A.FTOrderNo = D1.FTOrderNo"

        cmdstring &= vbCrLf & "    Where D1.FTPurchaseNo ='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        cmdstring &= vbCrLf & "    ORDER By A.FNSeq "

        dtdelivery = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

            For Each Rx As DataRow In dtdelivery.Rows
                CmpCreateOrderId = Val(Rx!FNHSysCmpIdOrder.ToString)

                Exit For
            Next

        If CmpCreateOrderId > 0 Then

            Dim OldPoNo As String = Me.FTPurchaseNo.Text
            Dim NewPoNo As String = ""

            Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
            Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
            Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)

            Dim cmprunpo As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPORun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun  WITH(NOLOCK)  WHERE FNHSysCmpRunId=" & Val(FNHSysCmpRunId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_MASTER, "")

            If cmprunpo = "" Then
                cmprunpo = Microsoft.VisualBasic.Left(FNHSysCmpRunId.Text, 1)
            End If

            If CmpCreateOrderId > 0 Then
                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(CmpCreateOrderId) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            Else
                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            End If

            If _CmpH <> Microsoft.VisualBasic.Left(Me.FTPurchaseNo.Text, _CmpH.Length) Then

                If HI.ST.SysInfo.CmpID = 1306010001 Then
                    NewPoNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH & "H" & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                Else
                    NewPoNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                End If

                Dim rNote As String = ""
                With _AddItemPopupChangePONO
                    .AddMat = False
                    .FTPurchaseNo.Text = NewPoNo
                    .ShowDialog()


                    If .AddMat = True Then

                        rNote = .FTRemark.Text.Trim

                        If HI.ST.SysInfo.CmpID = 1306010001 Then
                            NewPoNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & "H" & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                        Else
                            NewPoNo = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & cmprunpo & _Year & FNHSysPurGrpId.Text & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
                        End If

                        cmdstring = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                        cmdstring &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        cmdstring &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo "
                        cmdstring &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        cmdstring &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised "
                        cmdstring &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        cmdstring &= vbCrLf & "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing "
                        cmdstring &= vbCrLf & "  SET  FTPurchaseNo='" & HI.UL.ULF.rpQuoted(NewPoNo) & "'"
                        cmdstring &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                        cmdstring &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_ChangeNumber( FTInsUser, FDInsDate, FTInsTime, FTPurchaseNo, FTPurchaseNewNo, FTPurchaseRevisedBy, FTRevisedDate, FTRevisedTime, FTChangeNoteNote) "
                        cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(NewPoNo) & "' "
                        cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(rNote) & "' "

                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text & " ( New PO No  Is" & NewPoNo & " )")

                        Me.FTPurchaseNo.Text = NewPoNo

                    End If

                End With

            Else
                HI.MG.ShowMsg.mInfo("ข้อมูล Company ไม่มีการเปลี่ยนแปลง ไม่สามารถ Change PO No. ได้ !!!", 22016418, Me.Text, , MessageBoxIcon.Warning)
            End If

        Else


        End If

    End Sub


    'Public Shared Function ReadRegistry() As String
    '    Dim regKey As RegistryKey
    '    Dim valreturn As String = ""

    '    regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

    '    If regKey Is Nothing Then

    '        Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
    '        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

    '    End If

    '    valreturn = regKey.GetValue("POListDocumentData", "")
    '    regKey.Close()

    '    Return valreturn
    'End Function

    'Public Shared Sub WriteRegistry(ByVal value As Object)

    '    Dim regKey As RegistryKey
    '    regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

    '    If regKey Is Nothing Then

    '        Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
    '        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

    '    End If

    '    regKey.SetValue("POListDocumentData", value.ToString)
    '    regKey.Close()

    'End Sub

End Class