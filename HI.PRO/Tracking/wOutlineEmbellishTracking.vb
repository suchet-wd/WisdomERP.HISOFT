
Imports System.Windows.Forms
Public Class wOutlineEmbellishTracking
    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.InitFormControl()

        Call InitGrid()

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
            _Qry = "  SELECT   B.FTBarcodeNo, B.FNHSysUnitSectId,   sum(B.FNQuantity) AS FNQuantity  , U.FTUnitSectCode,   D.FTColorway, D.FTSizeBreakDown, P.FTOrderNo , O.FTPORef , D.FNBunbleSeq"
            _Qry &= vbCrLf & ",CASE WHEN Isdate(B.FDDate)=1 Then convert(nvarchar(10),convert(datetime,B.FDDate),103) Else '' END AS  FDDate"
            '_Qry &= vbCrLf & ",Isnull((Select min(FTNikePOLineItem) AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown WITH(NOLOCK) where FTOrderNo =P.FTOrderNo and FTColorway = D.FTColorway) , '')  AS FTNikePOLineItem"
            _Qry &= vbCrLf & ",Isnull( D.FTPOLineItemNo, '') AS FTNikePOLineItem  ,TS.FTStyleCode  ,PT.FTPartCode, PBS.FTNote "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", max(U.FTUnitSectNameTH) AS FTUnitSectName  , max(TS.FTStyleNameTH) AS FTStyleName  , max(FTNameTH) as FNStateSewPack , max(FTPartNameTH) as FTPartName "
            Else
                _Qry &= vbCrLf & ", max(U.FTUnitSectNameEN) AS FTUnitSectName  , max(TS.FTStyleNameEN) AS FTStyleName  , max(FTNameEN) as FNStateSewPack , max(FTPartNameEN) as FTPartName   "
            End If
            _Qry &= vbCrLf & ",(select top 1 Convert(varchar(10),Convert(datetime,Isnull(FDUpdDate,FDInsDate)),103) FROM [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo  Order by  FDInsDate asc,FTInsTime asc) AS FDInsDate"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdTime,FTInsTime) FROM [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo  Order by FDInsDate asc,FTInsTime asc) AS FTInsTime"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdUser,FTInsUser) FROM [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo  Order by FDInsDate asc,FTInsTime asc) AS FTInsUser"
            _Qry &= vbCrLf & ",(select top 1 Convert(varchar(10),Convert(datetime,Isnull(FDUpdDate,FDInsDate)),103) FROM [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo and FDDate = B.FDDate Order by FDInsDate desc,FTInsTime desc) AS FDUpdDate"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdTime,FTInsTime) FROM [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo and FDDate = B.FDDate Order by FDInsDate desc,FTInsTime desc) AS FTUpdTime"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdUser,FTInsUser) FROM [HITECH_PRODUCTION].dbo.TPRODBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo and FDDate = B.FDDate Order by FDInsDate desc,FTInsTime desc) AS FTUpdUser"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH (NOLOCK) ON B.FNHSysUnitSectId = U.FNHSysUnitSectId INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH (NOLOCK) ON B.FTBarcodeNo = BS.FTBarcodeSendSuplNo INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS D WITH (NOLOCK) ON BS.FTBarcodeBundleNo = D.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON D.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON P.FTOrderNo = O.FTOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS TS WITH(NOLOCK) ON O.FNHSysStyleId = TS.FNHSysStyleId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN  (Select FNListIndex , FTNameEN , FTNameTH From [HITECH_SYSTEM].dbo.HSysListData with(nolock)  where  FTListName = 'FNStateSewPack'   ) AS LL ON isnull(B.FNStateSewPack,0) = LL.FNListIndex "
            _Qry &= vbCrLf & " LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS PT ON BS.FNHSysPartId = PT.FNHSysPartId "
            _Qry &= vbCrLf & " outer apply ( SELECT top 1   PBS.FTNote  "
            _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS PBS LEFT OUTER JOIN "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl_Detail AS BS1 ON PBS.FTSendSuplRef = BS1.FTSendSuplRef AND PBS.FNHSysPartId = BS1.FNHSysPartId AND PBS.FNSendSuplType = BS1.FNSendSuplType AND    "
            _Qry &= vbCrLf & "     PBS.FNHSysSuplId = BS1.FNHSysSuplId AND PBS.FTOrderProdNo = BS1.FTOrderProdNo LEFT OUTER JOIN   "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS2 ON BS1.FTBarcodeBundleNo = BS2.FTBarcodeBundleNo AND BS1.FTOrderProdNo = BS2.FTOrderProdNo AND BS1.FTSendSuplRef = BS2.FTSendSuplRef AND  "
            _Qry &= vbCrLf & "    BS1.FNHSysPartId = BS2.FNHSysPartId AND BS1.FNSendSuplType = BS2.FNSendSuplType AND BS1.FNHSysSuplId = BS2.FNHSysSuplId"
            _Qry &= vbCrLf & "  WHERE  (BS2.FTBarcodeSendSuplNo = B.FTBarcodeNo)"
            _Qry &= vbCrLf & " ) PBS"
            _Qry &= vbCrLf & "WHERE  isnull(B.FTBarcodeCustRef,'') = ''"
            _Qry &= vbCrLf & "and B.FDDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "'    "
            _Qry &= vbCrLf & "and B.FDDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTransTo.Text) & "'    "


            If Me.FNHSysUnitSectId.Text <> "" Then
                ' _Qry &= vbCrLf & "And U.FTUnitSectCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
                _Qry &= vbCrLf & "And U.FTUnitSectCode  >='" & (Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then

                _Qry &= vbCrLf & "And U.FTUnitSectCode <='" & (Me.FNHSysUnitSectId.Text) & "'"
            End If

            If Me.FTOrderNo.Text <> "" Then

                _Qry &= vbCrLf & "And  P.FTOrderNo >='" & (Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Qry &= vbCrLf & "And  P.FTOrderNo <='" & (Me.FTOrderNo.Text) & "'"


            End If


            _Qry &= vbCrLf & "Group by  B.FTBarcodeNo, B.FNHSysUnitSectId,PT.FTPartCode, PBS.FTNote,    U.FTUnitSectCode,   D.FTColorway, D.FTSizeBreakDown, P.FTOrderNo , O.FTPORef , D.FNBunbleSeq,B.FDDate , D.FTPOLineItemNo,TS.FTStyleCode , isnull(B.FNStateSewPack,0)"
            _Qry &= vbCrLf & "Order by FDUpdDate DESC,FTUpdTime DESC ,FDInsDate DESC, FTInsTime DESC"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcdetail.DataSource = dt.Copy
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

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs)
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub wScanBarcodeSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        FNHSysUnitSectId.TabStop = False
        'Me.FDDateTrans.Text = HI.Conn.SQLConn.GetField("Select  convert(nvarchar(10),Convert(varchar(10),Getdate(),103),103) ", Conn.DB.DataBaseName.DB_PROD, Date.Now)
    End Sub


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If VerrifyData() Then
            Call LoadDucumentDetail()
        End If

    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.FDDateTrans.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDateTrans_lbl.Text)
                Me.FDDateTrans.Focus()
                Return False
            End If
            If Me.FDDateTransTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDateTransTo_lbl.Text)
                Me.FDDateTransTo.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return True
        End Try
    End Function
    Private Function UpdateBundleToBox(_BundleNo As String) As Boolean
        Try
            Dim _Cmd As String = ""

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTBarcodeNo ='" & HI.UL.ULF.rpQuoted(_BundleNo) & "'")


                    _Cmd = "Select top 1  * From      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode with(nolock)  "
                    _Cmd &= vbCrLf & " where    ISNULL (FTBarcodeBundleNo, '' ) = '" & HI.UL.ULF.rpQuoted(_BundleNo) & "'   "
                    If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count <= 0 Then
                        _Cmd = "UPDATE   TOP (1)   C "
                        _Cmd &= vbCrLf & "Set C.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(_BundleNo) & "'"
                        _Cmd &= vbCrLf & " FROM   "
                        _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C LEFT OUTER JOIN "
                        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS B ON C.FTPackNo = B.FTPackNo AND C.FNCartonNo = B.FNCartonNo "
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS D ON C.FTPackNo = D.FTPackNo "
                        _Cmd &= vbCrLf & "  LEFT OUTER JOIN       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKCarton AS A   ON C.FTPackNo = A.FTPackNo AND C.FNCartonNo = A.FNCartonNo  "
                        _Cmd &= vbCrLf & "where  B.FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        '_Cmd &= vbCrLf & "and B.FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "''"
                        _Cmd &= vbCrLf & "and  B.FTColorway = '" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                        _Cmd &= vbCrLf & "and  B.FTSizeBreakDown = '" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                        _Cmd &= vbCrLf & "and  D.FTCustomerPO = '" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'"
                        _Cmd &= vbCrLf & "and  B.FTPOLine = '" & HI.UL.ULF.rpQuoted(R!FTNikePOLineItem.ToString) & "'"
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
            If Me.FNHSysUnitSectId.Text = "" Then
                HI.MG.ShowMsg.mInfo("กรุณเลือกสังกัด......", 1711211727, Me.Text, "", MessageBoxIcon.Stop)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If
            Dim _Cmd As String = ""
            Dim _State As Boolean = False

            '_Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '_Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '_Cmd &= vbCrLf & " select top 1    FNHSysShiftID From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee with(nolock) "
            '_Cmd &= vbCrLf & "where FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '_Cmd &= vbCrLf & " order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc ) "
            '_Cmd &= vbCrLf & " and convert(nvarchar(5) , convert(datetime ,  dateadd(minute ,FNScanAfterBreakMin    , FTStartTime) ) ,114)  <   Convert(varchar(5),Getdate(),114) and FTEndTime >   Convert(varchar(5),Getdate(),114)  "
            '_Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '1' and ISNULL(FTStateActive,'0') = '1' "


            '_Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '_Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '_Cmd &= vbCrLf & "  Select  Top 1  FNHSysShiftID From  ( "
            '_Cmd &= vbCrLf & "  Select isnull( M.FNHSysShiftID ,  E.FNHSysShiftID) as FNHSysShiftID    , E.FNHSysUnitSectId    "
            '_Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock)  LEFT OUTER JOIN  "
            '_Cmd &= vbCrLf & "  ( SELECT S.FNHSysEmpID , s.FNHSysShiftID   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift as  S  WITH (NOLOCK)   "
            '_Cmd &= vbCrLf & " WHERE FDShiftDate =Convert(varchar(10),Getdate(),111) ) AS M ON e.FNHSysEmpID = M.FNHSysEmpID  "
            '_Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '_Cmd &= vbCrLf & "  ) AS T "
            '_Cmd &= vbCrLf & "  order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc )  "
            '_Cmd &= vbCrLf & " and FTStartTime <=   Convert(varchar(5),Getdate(),114) and FTEndTime >=   Convert(varchar(5),Getdate(),114)  "
            '_Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '1' and ISNULL(FTStateActive,'0') = '1' "

            '_State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 0

            'If _State Then


            '    _State = False
            '    _Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '    _Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '    _Cmd &= vbCrLf & "  Select  Top 1  FNHSysShiftID From  ( "
            '    _Cmd &= vbCrLf & "  Select isnull( M.FNHSysShiftID ,  E.FNHSysShiftID) as FNHSysShiftID     , E.FNHSysUnitSectId   "
            '    _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock)  LEFT OUTER JOIN  "
            '    _Cmd &= vbCrLf & "  ( SELECT S.FNHSysEmpID , s.FNHSysShiftID   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift as  S  WITH (NOLOCK)   "
            '    _Cmd &= vbCrLf & " WHERE FDShiftDate =Convert(varchar(10),Getdate(),111) ) AS M ON e.FNHSysEmpID = M.FNHSysEmpID  "
            '    _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '    _Cmd &= vbCrLf & "  ) AS T "
            '    _Cmd &= vbCrLf & "  order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc )  "
            '    _Cmd &= vbCrLf & " and  FTStartTime   <=   Convert(varchar(5),Getdate(),114) and   FTEndTime   >=   Convert(varchar(5),Getdate(),114)  "
            '    _Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '0' and ISNULL(FTStateActive,'0') = '1' "

            '    _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count > 0

            '    If Not (_State) Then

            '        _Cmd = "Select top 1  r.FTOtMIn , r.FTOtMOut , r.FTOtIn , r.FTOtOut  "
            '        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock) "
            '        _Cmd &= vbCrLf & " left outer join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest as r with(Nolock) on e.FNHSysEmpID = r.FNHSysEmpID "
            '        _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '        _Cmd &= vbCrLf & "  and  FTDateRequest = Convert(varchar(10),Getdate(),111)"
            '        _Cmd &= vbCrLf & "   and ( (r.FTOtMIn <= Convert(varchar(8),Getdate(),114) and r.FTOtMOut  >= Convert(varchar(8),Getdate(),114) ) OR  "
            '        _Cmd &= vbCrLf & "  ( r.FTOtIn <= Convert(varchar(8),Getdate(),114) and  r.FTOtOut  >= Convert(varchar(8),Getdate(),114) )) "
            '        _Cmd &= vbCrLf & "  order by r.FNOtNetTime desc "

            '        _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 1
            '    End If

            'Else

            '    _Cmd = "Select * From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMShiftPeriodTime  with(nolock)    "
            '    _Cmd &= vbCrLf & " where FNHSysShiftID in ( "
            '    _Cmd &= vbCrLf & "  Select  Top 1  FNHSysShiftID From  ( "
            '    _Cmd &= vbCrLf & "  Select isnull( M.FNHSysShiftID ,  E.FNHSysShiftID) as FNHSysShiftID    , E.FNHSysUnitSectId    "
            '    _Cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock)  LEFT OUTER JOIN  "
            '    _Cmd &= vbCrLf & "  ( SELECT S.FNHSysEmpID , s.FNHSysShiftID   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift as  S  WITH (NOLOCK)   "
            '    _Cmd &= vbCrLf & " WHERE FDShiftDate =Convert(varchar(10),Getdate(),111) ) AS M ON e.FNHSysEmpID = M.FNHSysEmpID  "
            '    _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '    _Cmd &= vbCrLf & "  ) AS T "
            '    _Cmd &= vbCrLf & "  order by count( FNHSysShiftID) over(partition by FNHSysUnitSectId  ) desc )  "
            '    _Cmd &= vbCrLf & " and   convert(nvarchar(5) , convert(datetime ,  dateadd(minute ,FNScanAfterBreakMin    , FTStartTime ) ) ,114)   <=  Convert(varchar(5),Getdate(),114)     and FTEndTime >=   Convert(varchar(5),Getdate(),114)  "
            '    _Cmd &= vbCrLf & " and isnull( FTStateBreak,'0') = '1' and ISNULL(FTStateActive,'0') = '1' "

            '    _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 0

            '    If Not (_State) Then

            '        _Cmd = "Select top 1  r.FTOtMIn , r.FTOtMOut , r.FTOtIn , r.FTOtOut  "
            '        _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee as E  with(nolock) "
            '        _Cmd &= vbCrLf & " left outer join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest as r with(Nolock) on e.FNHSysEmpID = r.FNHSysEmpID "
            '        _Cmd &= vbCrLf & "   where E.FNHSysUnitSectId = " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
            '        _Cmd &= vbCrLf & "  and  FTDateRequest = Convert(varchar(10),Getdate(),111)"
            '        _Cmd &= vbCrLf & "   and ( (r.FTOtMIn <= Convert(varchar(8),Getdate(),114) and r.FTOtMOut  >= Convert(varchar(8),Getdate(),114) ) OR  "
            '        _Cmd &= vbCrLf & "  ( r.FTOtIn <= Convert(varchar(8),Getdate(),114) and  r.FTOtOut  >= Convert(varchar(8),Getdate(),114) )) "
            '        _Cmd &= vbCrLf & "  order by r.FNOtNetTime desc "

            '        _State = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR).Rows.Count = 1
            '    End If

            'End If

            _Cmd = "  EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_CHECKTIME_SCANOUTLINE " & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag.ToString) & ""
            _State = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "1"

            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkBarcodeInlineBal(BarCode As String, OrderNo As String, UnitSectId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyIn As Double = 0 : Dim _QtyOut As Double = 0
            _Cmd = "SELECT     A.FTBarcodeNo, P.FTOrderNo, US.FTUnitSectCode AS FTUnitSectCodeSew, B.FTColorway, B.FTSizeBreakDown, SUM(B.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId INNER JOIN"

            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH (NOLOCK) ON A.FTBarcodeNo = BS.FTBarcodeSendSuplNo  INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH (NOLOCK) ON BS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan AS HA WITH (NOLOCK) ON A.FTDocScanNo = HA.FTDocScanNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OP WITH(NOLOCK) ON A.FNHSysOperationId = OP.FNHSysOperationId"
            _Cmd &= vbCrLf & "WHERE     (US.FTStateSew = '1')  and (isnull(OP.FTStateEmbellish,'0') = '1') "
            _Cmd &= vbCrLf & "AND (BS.FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId=" & Integer.Parse(UnitSectId) & ")"
            _Cmd &= vbCrLf & " AND (Isnull(OP.FTStateSPMK,'0') <> '1')"
            '_Cmd &= vbCrLf & "AND "
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo, P.FTOrderNo, US.FTUnitSectCode, B.FTColorway, B.FTSizeBreakDown"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)


            For Each R As DataRow In _oDt.Rows
                _QtyIn = Double.Parse(R!FNQuantity.ToString)
                Exit For
            Next


            _Cmd = "SELECT     A.FTBarcodeNo, A.FNHSysUnitSectId , P.FTOrderNo, SUM(A.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline AS A WITH (NOLOCK) LEFT OUTER JOIN"

            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH (NOLOCK) ON A.FTBarcodeNo = BS.FTBarcodeSendSuplNo INNER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS B WITH (NOLOCK) ON BS.FTBarcodeBundleNo = B.FTBarcodeBundleNo INNER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "WHERE (BS.FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            _Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId=" & Integer.Parse(UnitSectId) & ")"
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo, A.FNHSysUnitSectId, P.FTOrderNo "
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

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs)

    End Sub
End Class