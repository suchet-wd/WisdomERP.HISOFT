
Imports System.Windows.Forms
Public Class wSMPScanBarcodeOutline
    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False

    Private ScanFullBundle As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.InitFormControl()

        Call InitGrid()
        ScanFullBundle = False
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
            _Qry = "  SELECT   B.FTBarcodeNo,   sum(B.FNQuantity) AS FNQuantity  ,    D.FTColorway, D.FTSizeBreakDown, O.FTSMPOrderNo AS 'FTOrderNo' , D.FNBunbleSeq"
            _Qry &= vbCrLf & ",CASE WHEN Isdate(B.FDDate)=1 Then convert(nvarchar(10),convert(datetime,B.FDDate),103) Else '' END AS  FDDate"
            '_Qry &= vbCrLf & ",Isnull((Select min(FTNikePOLineItem) AS FTNikePOLineItem  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown WITH(NOLOCK) where FTOrderNo =P.FTOrderNo and FTColorway = D.FTColorway) , '')  AS FTNikePOLineItem"
            _Qry &= vbCrLf & ",Isnull( D.FTPOLineItemNo, '') AS FTNikePOLineItem  ,TS.FTStyleCode   "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",  max(TS.FTStyleNameTH) AS FTStyleName  , max(FTNameTH) as FNStateSewPack  "
            Else
                _Qry &= vbCrLf & " , max(TS.FTStyleNameEN) AS FTStyleName  , max(FTNameEN) as FNStateSewPack  "
            End If
            _Qry &= vbCrLf & ",(select top 1 Convert(varchar(10),Convert(datetime,Isnull(FDUpdDate,FDInsDate)),103) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo  Order by  FDInsDate asc,FTInsTime asc) AS FDInsDate"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdTime,FTInsTime) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo  Order by FDInsDate asc,FTInsTime asc) AS FTInsTime"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdUser,FTInsUser) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo  Order by FDInsDate asc,FTInsTime asc) AS FTInsUser"
            _Qry &= vbCrLf & ",(select top 1 Convert(varchar(10),Convert(datetime,Isnull(FDUpdDate,FDInsDate)),103) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo and FDDate = B.FDDate Order by FDInsDate desc,FTInsTime desc) AS FDUpdDate"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdTime,FTInsTime) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo and FDDate = B.FDDate Order by FDInsDate desc,FTInsTime desc) AS FTUpdTime"
            _Qry &= vbCrLf & ",(select top 1 Isnull(FTUpdUser,FTInsUser) FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline WHERE FTBarcodeNo = B.FTBarcodeNo and FDDate = B.FDDate Order by FDInsDate desc,FTInsTime desc) AS FTUpdUser"

            _Qry &= vbCrLf & " ,  ISNULL(FTEmpName,'')   as FTEmpName "

            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline AS B WITH (NOLOCK)"
            ''_Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS U WITH (NOLOCK) ON B.FNHSysUnitSectId = U.FNHSysUnitSectId "
            _Qry &= vbCrLf & "   INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS D WITH (NOLOCK) ON B.FTBarcodeNo = D.FTBarcodeBundleNo "
            ''_Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON D.FTOrderProdNo = P.FTOrderProdNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O WITH(NOLOCK) ON D.FTOrderProdNo = O.FTSMPOrderNo"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS TS WITH(NOLOCK) ON O.FNHSysStyleId = TS.FNHSysStyleId  "
            _Qry &= vbCrLf & "LEFT OUTER JOIN  (Select FNListIndex , FTNameEN , FTNameTH From [HITECH_SYSTEM].dbo.HSysListData with(nolock)  where  FTListName = 'FNStateSewPack'   ) AS LL ON isnull(B.FNStateSewPack,0) = LL.FNListIndex "


            _Qry &= vbCrLf & " 	 OUTER APPLY ("
            _Qry &= vbCrLf & " SELECT TOP 1  BSE.FTDocScanNo,  BSE.FTBarcodeNo,"
            _Qry &= vbCrLf & " STUFF((SELECT ',' + ISNULL(FTEmpCode,'')  + ' ' + ISNULL(FTEmpNameTH,'') + ' ' +  ISNULL(FTEmpSurnameTH,'')  "
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Emp  BSE "
            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FTEmpCode, FTEmpNameTH,FTEmpSurnameTH, FTEmpNameEN, FTEmpSurnameEN   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WHERE FNHSysEmpID = BSE.FNHSysEmpId) EE "
            _Qry &= vbCrLf & "   "
            _Qry &= vbCrLf & "  FOR XML PATH ('')), 1, 1, '' "
            _Qry &= vbCrLf & "    )  FTEmpName "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Emp  BSE "
            _Qry &= vbCrLf & "  WHERE    B.FTBarcodeNo=BSE.FTBarcodeNo  "
            _Qry &= vbCrLf & "  ) EE "
            _Qry &= vbCrLf & " "




            _Qry &= vbCrLf & "WHERE B.FDDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FDDateTrans.Text) & "'  and isnull(B.FTBarcodeCustRef,'') = ''"

            '' _Qry &= vbCrLf & " AND ( ISNULL(U.FNHSysCmpId,0) = 0 OR  ISNULL(U.FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & "  ) "

            'If Me.FNHSysUnitSectId.Text <> "" Then
            '    ' _Qry &= vbCrLf & "And U.FTUnitSectCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            '    _Qry &= vbCrLf & "And B.FNHSysUnitSectId=" & Val(Me.FNHSysUnitSectId.Properties.Tag.ToString()) & ""
            'End If
            _Qry &= vbCrLf & "Group by  B.FTBarcodeNo,   D.FTColorway, D.FTSizeBreakDown,  O.FTSMPOrderNo  , D.FNBunbleSeq,B.FDDate , D.FTPOLineItemNo,TS.FTStyleCode , isnull(B.FNStateSewPack,0),  ISNULL(FTEmpName,'') "
            _Qry &= vbCrLf & "Order by FDUpdDate DESC,FTUpdTime DESC ,FDInsDate DESC, FTInsTime DESC"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
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
        FTBarcodeNo.EnterMoveNextControl = False
        ''FNHSysUnitSectId.TabStop = False
        Me.FDDateTrans.Text = HI.Conn.SQLConn.GetField("Select  convert(nvarchar(10),Convert(varchar(10),Getdate(),103),103) ", Conn.DB.DataBaseName.DB_PROD, Date.Now)
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadDucumentDetail()
    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter

                    If ChkBreakTime(Me.FTBarcodeNo.Text) = False Then
                        HI.MG.ShowMsg.mInfo("หมดเวลาแสถนงาน กรุณาไปพักตามอัตยาศัยครับ......", 1711211728, Me.Text, "", MessageBoxIcon.Stop)
                        Exit Sub
                    End If

                    If ChkBarCode(Me.FTBarcodeNo.Text) Then
                        If ScansBarcode(Me.FTBarcodeNo.Text) Then
                            Call LoadDucumentDetail()
                        End If
                        'If Not (Me.FTStateDeleteBarcode.Checked) Then
                        '    Call UpdateBundleToBox(Me.FTBarcodeNo.Text)
                        'End If

                        FTBarcodeNo.Focus()
                        FTBarcodeNo.SelectAll()
                    Else
                        HI.MG.ShowMsg.mInfo("Barcode ยังไม่มีการแสกนเข้าไลน์......", 1506181749, Me.Text, "", MessageBoxIcon.Stop)
                        FTBarcodeNo.Focus()
                        FTBarcodeNo.SelectAll()
                    End If
            End Select

        Catch ex As Exception
        End Try
    End Sub

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


    Private Function ChkBarCode(_BarcodeKey As String) As Boolean
        Try

            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT   Top 1  A.FTDocScanNo, A.FTBarcodeNo, A.FNHSysUnitSectId, B.FTBarcodeBundleNo, B.FNBunbleSeq, B.FTColorway, "
            _Cmd &= vbCrLf & "  B.FTSizeBreakDown, B.FTOrderProdNo, BD.FTLayCutNo, BD.FNLayCutSeq, BD.FNQuantity, T.FTStyleCode ,O.FTSMPOrderNo  as 'FTOrderNo' , '' as FTSubOrderNo "
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Detail AS A WITH (NOLOCK) "
            '' _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle_Detail AS BD WITH (NOLOCK) ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo "
            '' _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O WITH(NOLOCK) ON  B.FTOrderProdNo = O.FTSMPOrderNo"
            _Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH(NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId"
            ''_Cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS SB WITH(NOLOCK) ON "
            ''_Cmd &= vbCrLf & "  B.FTPOLineItemNo = SB.FTNikePOLineItem AND B.FTSizeBreakDown = SB.FTSizeBreakDown AND B.FTColorway = SB.FTColorway  and P.FTOrderNo = SB.FTOrderNo "
            ''_Cmd &= vbCrLf & "WHERE  A.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeKey) & "' and    (US.FTStateSew = '1')"
            _Cmd &= vbCrLf & "WHERE  A.FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarcodeKey) & "' "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            For Each R As DataRow In _oDt.Rows
                ''Me.FNHSysUnitSectId.Text = HI.UL.ULF.rpQuoted(R!FTUnitSectCode.ToString)
                Me.FTColorway.Text = HI.UL.ULF.rpQuoted(R!FTColorway.ToString)
                Me.FTSizeBreakDown.Text = HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString)
                ''Me.FTSubOrderNo.Text = HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString)
                Me.FNBunbleSeq.Text = HI.UL.ULF.rpQuoted(R!FNBunbleSeq.ToString)
                Me.FTStyleCode.Text = HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString)
                Me.FTOrderNo.Text = HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString)
                '' Me.FTPORef.Text = HI.UL.ULF.rpQuoted(R!FTPORef.ToString)
                Return True
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ScansBarcode(BarcodeKey As String) As Boolean
        Try

            Dim QtyBal As Integer = 0
            If Not (Me.FTStateDeleteBarcode.Checked) Then
                QtyBal = (ChkBarcodeInlineBal(BarcodeKey, Me.FTOrderNo.Text, ""))

                If QtyBal <= 0 Then
                    HI.MG.ShowMsg.mInfo("BarCode ออกไลน์หมดแล้ว...", 1506191040, Me.Text, "", MessageBoxIcon.Stop)
                    Return False
                End If
            End If


            If Not ScanFullBundle Then
                QtyBal = 1
            End If

            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If Not (Me.FTStateDeleteBarcode.Checked) Then

                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline"
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNQuantity=FNQuantity+" & QtyBal & ""
                _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                _Cmd &= vbCrLf & " AND FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & " AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "'"

                '_Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & "And FDDate =" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "And FTTime = Convert(varchar(5),Getdate(),114)"
                _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline"
                    _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime , FTBarcodeNo,FDDate, FTTime, FNQuantity , FTOrderNo , FTSubOrderNo,FNStateSewPack)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    ''_Cmd &= vbCrLf & "," & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",Convert(varchar(5),Getdate(),114)"
                    _Cmd &= vbCrLf & "," & QtyBal & ""
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse(Me.FNStateSewPack.SelectedIndex)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Else

                '_Cmd = " Select Top 1   FTBarcodeNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
                '_Cmd &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                '_Cmd &= vbCrLf & " UNION ALL"
                '_Cmd &= vbCrLf & "Select Top 1   FTBarcodeNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Repack_Detail WITH(NOLOCK) "
                '_Cmd &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                'If HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows.Count <= 0 Then


                If Not ScanFullBundle Then

                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline"
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FNQuantity=FNQuantity-1"
                        _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    '' _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)
                        _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
                    _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline "
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    ''_Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)
                        _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"


                    Else

                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline"
                    _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FNQuantity=0"
                        _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    ''_Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)

                    End If


                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                _Cmd = "Select SUM(FNQuantity) AS FNQuantity From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline"
                _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                '' _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)

                    If Not ScanFullBundle Then
                        _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
                    _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline "
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    ''  _Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)
                        _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"
                    End If


                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0") <= 0 Then
                    _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline"
                    _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    ''_Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                    _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)

                    If Not ScanFullBundle Then
                        _Cmd &= vbCrLf & "And FDDate +'|'+FTTime in ("
                        _Cmd &= vbCrLf & "Select top 1 FDDate +'|'+FTTime  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline "
                        _Cmd &= vbCrLf & "WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                        ''_Cmd &= vbCrLf & "And FNHSysUnitSectId=" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)
                        _Cmd &= vbCrLf & "And isnull(FNStateSewPack,0)=" & Integer.Parse(Me.FNStateSewPack.SelectedIndex)
                        _Cmd &= vbCrLf & "ORder by FDDate Desc,FTTime Desc )"
                    End If


                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If


                    '_Cmd = "Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode"
                    '_Cmd &= vbCrLf & " set  FTBarcodeBundleNo ='' "
                    '_Cmd &= vbCrLf & "WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarcodeKey) & "'"
                    'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '    'HI.Conn.SQLConn.Tran.Rollback()
                    '    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '    'Return False
                    'End If


                End If

                'End If


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

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs)
        Try
            Call LoadDucumentDetail()
        Catch ex As Exception
        End Try
    End Sub


    Private Function ChkBreakTime(_BarcodeKey As String) As Boolean
        Try
            'If Me.FNHSysUnitSectId.Text = "" Then
            '    HI.MG.ShowMsg.mInfo("กรุณเลือกสังกัด......", 1711211727, Me.Text, "", MessageBoxIcon.Stop)
            '    Me.FNHSysUnitSectId.Focus()
            '    Return False
            'End If
            Dim _Cmd As String = ""
            Dim _State As Boolean = False



            _Cmd = "  EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_GET_CHECKTIME_SCANOUTLINE_SMP  '" & HI.UL.ULF.rpQuoted(_BarcodeKey) & "'"
            _State = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1"

            ''_State = True

            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function ChkBarcodeInlineBal(BarCode As String, OrderNo As String, UnitSectId As Integer) As Integer
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _QtyIn As Double = 0 : Dim _QtyOut As Double = 0
            _Cmd = "SELECT     A.FTBarcodeNo, SUM(BD.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan_Detail AS A WITH (NOLOCK)"
            '_Cmd &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON A.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle_Detail AS BD WITH (NOLOCK) ON B.FTBarcodeBundleNo = BD.FTBarcodeBundleNo "
            '_Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo "
            _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScan AS HA WITH (NOLOCK) ON A.FTDocScanNo = HA.FTDocScanNo"

            '' _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS OP WITH(NOLOCK) ON A.FNHSysOperationId = OP.FNHSysOperationId"
            _Cmd &= vbCrLf & "WHERE   (A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            '' _Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            ''_Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId=" & Integer.Parse(UnitSectId) & ")"
            '' _Cmd &= vbCrLf & " AND (Isnull(OP.FTStateSPMK,'0') <> '1')"
            '_Cmd &= vbCrLf & "AND "
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            _QtyIn = 0
            For Each R As DataRow In _oDt.Rows

                _QtyIn = Double.Parse(R!FNQuantity.ToString)

                Exit For
            Next

            _Cmd = "SELECT     A.FTBarcodeNo, SUM(A.FNQuantity) AS FNQuantity"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcodeScanOutline AS A WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS B WITH (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo "
            '_Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS P WITH (NOLOCK) ON B.FTOrderProdNo = P.FTOrderProdNo"
            _Cmd &= vbCrLf & "WHERE (A.FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(BarCode) & "')"
            ''_Cmd &= vbCrLf & "AND (P.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
            '_Cmd &= vbCrLf & "AND (A.FNHSysUnitSectId=" & Integer.Parse(UnitSectId) & ")"
            _Cmd &= vbCrLf & "GROUP BY A.FTBarcodeNo "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            _QtyOut = 0
            For Each R As DataRow In _oDt.Rows
                _QtyOut = Double.Parse(R!FNQuantity.ToString)
                Exit For
            Next

            Return _QtyIn - _QtyOut
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        Try
            Dim cmdstring As String = "SELECT TOP 1 C.FTStateScanFullBundle FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK) INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON C.FNHSysCustId = O.FNHSysCustId WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim) & "'"
            ScanFullBundle = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "") = "1")
        Catch ex As Exception
            ScanFullBundle = False
        End Try
    End Sub

    Private Sub FTBarcodeNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles FTBarcodeNo.KeyPress

    End Sub
End Class