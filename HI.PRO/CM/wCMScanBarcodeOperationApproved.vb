
Imports System.Windows.Forms
Public Class wCMScanBarcodeOperationApproved
    Private _DBEnum As HI.Conn.DB.DataBaseName = Conn.DB.DataBaseName.DB_PROD
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
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
        Dim sFieldCount As String = "FTBarcodeBundleNo"
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

        Call LoadDucumentDetail(Key.ToString)
        ' HI.TL.HandlerControl.ClearControl(ogbbarcodeinfo)
        ' olberror.Text = ""
        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable

            _Qry = " SELECT   BB.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, BB.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, BB.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, B.FTCMDocScanNo"
            _Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	,CASE WHEN Isnull( BB.FTColorwayNew, '') <>'' THEN Isnull( BB.FTColorwayNew, '')  ELSE BB.FTColorway END AS  FTColorway"
            _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, BB.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	, MUS.FTUnitSectCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If
            _Qry &= vbCrLf & " 	,B.FNHSysOperationId"
            _Qry &= vbCrLf & " 	,CASE WHEN ISDATE(ISnull(B.FDUpdDate,B.FDInsDate)) = 1 THEN Convert(varchar(10),Convert(datetime,ISnull(B.FDUpdDate,B.FDInsDate)),103)  ELSE '' END AS FDInsDate"
            _Qry &= vbCrLf & "  ,isnull(B.FTUpdTime,B.FTInsTime) AS FTInsTime,isnull(B.FTUpdUser,B.FTInsUser) AS FTInsUser"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "   SELECT        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BB.FTBarcodeBundleNo)"
            _Qry &= vbCrLf & "   ),'') AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON ODP.FTOrderProdNo = BB.FTOrderProdNo "
            _Qry &= vbCrLf & " 	INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail  AS B  WITH(NOLOCK) "
            _Qry &= vbCrLf & "         ON BB.FTBarcodeBundleNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & " 	       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId  = MPP.FNHSysOperationId"
            _Qry &= vbCrLf & " 	       LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS MUS WITH (NOLOCK)  ON  B.FNHSysUnitSectId  = MUS.FNHSysUnitSectId"
            _Qry &= vbCrLf & "   WHERE B.FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            '_Qry &= vbCrLf & "  ORDER BY FDInsDate DESC,FTInsTime DESC,BB.FTBarcodeBundleNo "

            _Qry &= vbCrLf & "UNION "
            _Qry &= vbCrLf & "  SELECT   BS.FTBarcodeSendSuplNo as FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, BB.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, BB.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, B.FTCMDocScanNo"
            _Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	,CASE WHEN Isnull( BB.FTColorwayNew, '') <>'' THEN Isnull( BB.FTColorwayNew, '')  ELSE BB.FTColorway END AS  FTColorway"
            _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, BB.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	, MUS.FTUnitSectCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If
            _Qry &= vbCrLf & " 	,B.FNHSysOperationId"
            _Qry &= vbCrLf & " 	,CASE WHEN ISDATE(ISnull(B.FDUpdDate,B.FDInsDate)) = 1 THEN Convert(varchar(10),Convert(datetime,ISnull(B.FDUpdDate,B.FDInsDate)),103)  ELSE '' END AS FDInsDate"
            _Qry &= vbCrLf & "  ,isnull(B.FTUpdTime,B.FTInsTime) AS FTInsTime,isnull(B.FTUpdUser,B.FTInsUser) AS FTInsUser"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "   SELECT        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"

            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BB.FTBarcodeBundleNo)"
            _Qry &= vbCrLf & "   ),'') AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON ODP.FTOrderProdNo = BB.FTOrderProdNo "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS BS WITH (NOLOCK)  ON BB.FTBarcodeBundleNo = BS.FTBarcodeBundleNo "
            _Qry &= vbCrLf & " 	INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail  AS B  WITH(NOLOCK) "
            _Qry &= vbCrLf & "  ON BS.FTBarcodeSendSuplNo  = B.FTBarcodeNo"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId  = MPP.FNHSysOperationId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS MUS WITH (NOLOCK)  ON  B.FNHSysUnitSectId  = MUS.FNHSysUnitSectId"
            _Qry &= vbCrLf & "   WHERE B.FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "  ORDER BY FDInsDate DESC,FTInsTime DESC,BB.FTBarcodeBundleNo "


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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan WHERE FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(Me.FTCMDocScanNo.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail WHERE FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(Me.FTCMDocScanNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan WHERE FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(Me.FTCMDocScanNo.Text) & "'")

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

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTCMDocScanBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
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
        If Me.FTCMDocScanNo.Text.Trim <> "" And Me.FTCMDocScanNo.Properties.Tag.ToString <> "" Then
            Dim _Qry As String = ""

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    Dim _BarKey As String = "" & R!FTBarcodeBundleNo.ToString

                    _Qry = "Select Top 1   FTBarcodeNo"
                    _Qry &= vbCrLf & " From TPRODBarcodeScanOutline WITH(NOLOCK)"
                    _Qry &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
                    _Qry &= vbCrLf & "UNION  "
                    _Qry &= vbCrLf & "Select Top 1 FTBarcodeNo"
                    _Qry &= vbCrLf & "From TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
                    _Qry &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                        olberror.ForeColor = Drawing.Color.Red
                        olberror.Text = HI.MG.ShowMsg.GetMessage("Barcode ถูก Scan ออกไลน์ไปแล้ว กรุณาตรวจสอบ", 1611011651)

                        Exit Sub
                    End If

                Next
            End With

            Dim _StateDelete As Boolean = True
            With (CType(Me.ogcdetail.DataSource, DataTable))
                .AcceptChanges()

                For Each R As DataRow In .Rows
                    Dim _Barcode As String = R!FTBarcodeBundleNo.ToString
                    Dim _Operation As String = R!FNHSysOperationId.ToString
                    Dim _OderProdNo As String = R!FTOrderProdNo.ToString
                    Dim _OderNo As String = R!FTOrderNo.ToString
                    Dim _StyleCode As String = ""
                    Dim _Quantity As Double = Double.Parse(Val(R!FNQuantity.ToString))

                    If _Barcode <> "" Then

                        With New PROD
                            _StyleCode = .GetStyleCodeByOrderNo(_OderNo)
                            If .CheckOperationAfter(_StyleCode, _OderProdNo, _Barcode, Integer.Parse(_Operation), _Quantity) = False Then
                                _StateDelete = False
                                Exit For
                            End If

                        End With
                    End If
                Next

            End With

            If _StateDelete Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.FTCMDocScanNo.Text, Me.Text) = True Then
                    If Me.DeleteData() Then
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        HI.TL.HandlerControl.ClearControl(Me)
                        Me.DefaultsData()
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่สามารถ ลบได้  เนื่องจากพบ การ Scan ขั้นตอนถัดไปแล้ว !!!", 1409251104, Me.Text, , MessageBoxIcon.Warning)
            End If


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDocScanNo_lbl.Text)
            Me.FTCMDocScanNo.Focus()
        End If


    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        olberror.Text = ""
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTCMDocScanNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                '.ReportName = "SendSuplSlip.rpt"
                .ReportName = "SuperMarket.rpt"
                .Formular = "{TPRODTCMBarcodeScan_Detail.FTCMDocScanNo}='" & HI.UL.ULF.rpQuoted(FTCMDocScanNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTDocScanNo_lbl.Text)
            FTCMDocScanNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Function SaveBarcode(Key As String) As Boolean

        Dim _Str As String = ""
        Dim _BarCode As String = Key
        Dim _StateNew As Boolean = False

        Try

            _Str = " SELECT TOP 1 FTCMDocScanNo  "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail WITH(NOLOCK)"
            _Str &= vbCrLf & "  WHERE FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(FTCMDocScanNo.Text) & "'"
            _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            _Str &= vbCrLf & "  AND FNHSysOperationId=" & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & " "
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail("
                _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                _Str &= vbCrLf & ", FTCMDocScanNo, FTBarcodeNo,FNHSysOperationId"
                _Str &= vbCrLf & ")  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTCMDocScanNo.Text) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "," & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & ""

            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & "  WHERE FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(FTCMDocScanNo.Text) & "'"
                _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & "  AND FNHSysOperationId=" & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & " "

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

    Private Function DeleteBarcode(BarcodeKey As String, _Operation As Integer) As Boolean
        Dim _Str As String
        Dim _BarCode As String = BarcodeKey

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail"
            _Str &= vbCrLf & "   WHERE FTCMDocScanNo='" & HI.UL.ULF.rpQuoted(FTCMDocScanNo.Text) & "' "
            _Str &= vbCrLf & "  AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            _Str &= vbCrLf & "  AND FNHSysOperationId=" & _Operation & " "

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

    Private Sub LoadBarcodeInfo(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable
            Dim _StatePass As Boolean = False


            'If 1 = 1 Then

            _Qry = " SELECT  TOP 1 BB.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, BB.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, BB.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, B.FTCMDocScanNo"
            _Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, CASE WHEN ISNULL(BB.FTColorwayNew,'') <>'' THEN  ISNULL(BB.FTColorwayNew,'') ELSE  BB.FTColorway END AS FTColorway"
            _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, BB.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "   SELECT        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BB.FTBarcodeBundleNo)"
            _Qry &= vbCrLf & "   ),'') AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON ODP.FTOrderProdNo = BB.FTOrderProdNo "
            _Qry &= vbCrLf & " 	LEFT OUTER JOIN   ( SELECT TOP 1  FTCMDocScanNo,FTBarcodeNo,FNHSysOperationId,FNHSysUnitSectId"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail WITH (NOLOCK)  "
            _Qry &= vbCrLf & "   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysOperationId=" & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & " "
            _Qry &= vbCrLf & "  ) AS B  "
            _Qry &= vbCrLf & "  ON BB.FTBarcodeBundleNo = B.FTBarcodeNo"

            _Qry &= vbCrLf & " INNER JOIN ("
            _Qry &= vbCrLf & "    SELECT A.FTBarcodeBundleNo"
            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B WITH(NOLOCK)  ON A.FTLayCutNo = B.FTLayCutNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS C WITH(NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo AND B.FNHSysMarkId = C.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE A.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "   GROUP BY A.FTBarcodeBundleNo"
            _Qry &= vbCrLf & "    ) AS BMM ON BB.FTBarcodeBundleNo = BMM.FTBarcodeBundleNo "

            _Qry &= vbCrLf & "   WHERE BB.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Key) & "' AND ISNULL(BB.FTStateGenBarcode,'') ='1' "
            _Qry &= vbCrLf & " and  BB.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
            'Else
            _Qry &= vbCrLf & " UNION "

            _Qry &= vbCrLf & "    Select  TOP 1 BS.FTBarcodeSendSuplNo As FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, BB.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, BB.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, B.FTCMDocScanNo"
            _Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	,CASE WHEN ISNULL(BB.FTColorwayNew,'') <>'' THEN  ISNULL(BB.FTColorwayNew,'') ELSE  BB.FTColorway END AS FTColorway"
            _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, BB.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "   Select        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  C.FTMarkNameTH As FTMarkName  "
            Else
                _Qry &= vbCrLf & "  C.FTMarkNameEN As FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail As AA  With (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut As B  With (NOLOCK) On AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark As C  With (NOLOCK)  On B.FNHSysMarkId = C.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = BB.FTBarcodeBundleNo)"
            _Qry &= vbCrLf & "   ),'') AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON ODP.FTOrderProdNo = BB.FTOrderProdNo "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl  BS ON BB.FTBarcodeBundleNo = BS.FTBarcodeBundleNo "
            _Qry &= vbCrLf & " 	LEFT OUTER JOIN   ( SELECT TOP 1  FTCMDocScanNo,FTBarcodeNo,FNHSysOperationId,FNHSysUnitSectId"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTCMBarcodeScan_Detail WITH (NOLOCK)  "
            _Qry &= vbCrLf & "   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "  AND FNHSysOperationId=" & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & " "
            _Qry &= vbCrLf & "  ) AS B  "
            _Qry &= vbCrLf & "  ON BB.FTBarcodeBundleNo = B.FTBarcodeNo or Bs.FTBarcodeSendSuplNo = B.FTBarcodeNo "

            '_Qry &= vbCrLf & " INNER JOIN ("
            '_Qry &= vbCrLf & "    SELECT A.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS A WITH(NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B WITH(NOLOCK)  ON A.FTLayCutNo = B.FTLayCutNo INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_MarkMain AS C WITH(NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo AND B.FNHSysMarkId = C.FNHSysMarkId"
            '_Qry &= vbCrLf & "   WHERE A.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            '_Qry &= vbCrLf & "   GROUP BY A.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & "    ) AS BMM ON BB.FTBarcodeBundleNo = BMM.FTBarcodeBundleNo "

            _Qry &= vbCrLf & "   WHERE BS.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' AND ISNULL(BB.FTStateGenBarcode,'') ='1' "
            _Qry &= vbCrLf & " and  BB.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)
            'End If



            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

            Dim tmpFTStyleCode As String = FTStyleCode.Text
            Dim tmpFTOrderProdNo As String = FTOrderProdNo.Text
            Dim StateClear As Boolean = True

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)!FTStyleCode.ToString = tmpFTStyleCode And dt.Rows(0)!FTOrderProdNo.ToString = tmpFTOrderProdNo Then
                    StateClear = False
                End If

            End If

            If StateClear Then
                FTStyleCode.Text = ""
                FTOrderProdNo.Text = ""
            End If

            FTOrderNo.Text = ""
            FTColorway.Text = ""
            FTSizeBreakDown.Text = ""
            FNHSysMarkId.Text = ""
            FTBarcodeBundleNo.Text = ""
            olberror.Text = ""
            olberror.ForeColor = Drawing.Color.Red

            If dt.Rows.Count > 0 Then

                For Each R As DataRow In dt.Rows

                    FTStyleCode.Text = R!FTStyleCode.ToString
                    FTOrderNo.Text = R!FTOrderNo.ToString
                    FTOrderProdNo.Text = R!FTOrderProdNo.ToString
                    FTColorway.Text = R!FTColorway.ToString
                    FTSizeBreakDown.Text = R!FTSizeBreakDown.ToString
                    FNHSysMarkId.Text = R!FNHSysMarkId.ToString
                    FTBarcodeBundleNo.Text = R!FNBunbleSeq.ToString

                    Exit For

                Next



                If FNHSysOperationId.EditValue = -1 Then
                    FNHSysOperationId.Focus()
                    Exit Sub
                End If

                If dt.Rows(0)!FTCMDocScanNo.ToString = "" Or dt.Rows(0)!FTCMDocScanNo.ToString = Me.FTCMDocScanNo.Text Then

                    Dim _CheckOperBefore As Integer = 0

                    _Qry = "SELECT TOP 1 FTOrderProdNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo.Text) & "' "

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                        _Qry = "  SELECT  TOP 1    A.FNHSysOperationIdTo"


                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS A WITH(NOLOCK) INNER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
                        _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleId=ISNULL((SELECT TOP 1 FNHSysStyleId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStylecode='" & HI.UL.ULF.rpQuoted(FTStyleCode.Text) & "'),0) "
                        _Qry &= vbCrLf & "   AND A.FNHSysOperationId=" & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & " "

                    Else
                        _Qry = " SELECT TOP 1    A.FNHSysOperationIdTo"
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) INNER JOIN"
                        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
                        _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(FTOrderProdNo.Text) & "'  "
                        _Qry &= vbCrLf & "   AND A.FNHSysOperationId=" & Integer.Parse(Val(FNHSysOperationId.EditValue.ToString)) & " "

                    End If

                    _CheckOperBefore = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

                    If Integer.Parse(Val(_CheckOperBefore)) > 0 Then

                        Dim dtcheck As DataTable
                        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CheckOperationBefore '" & HI.UL.ULF.rpQuoted(FTOrderProdNo.Text) & "'," & Integer.Parse(Val(_CheckOperBefore)) & ",'" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
                        dtcheck = (HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD))

                        If dtcheck.Rows.Count > 0 Then

                            If Val(dtcheck.Rows(0)!FNQuantity.ToString) >= Val(dt.Rows(0)!FNQuantity.ToString) Then
                                _StatePass = True
                                olberror.ForeColor = Drawing.Color.Green

                            Else
                                olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่สามารถ Scan เกินยอดขั้นตอนก่อนหน้าได้ !!!", 1407311104) & "   " & Format((Val(dtcheck.Rows(0)!FNQuantity.ToString)), "#,#0")
                            End If

                        Else
                            Dim _OperationName As String = ""
                            With New PROD
                                _OperationName = .GetOpertionName(Integer.Parse(Val(_CheckOperBefore)))
                            End With

                            olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูลขั้นตอนก่อนหน้า หรือยังไม่ได้ทำการ Scan กรุณาทำการตรวจสอบ !!!", 1407311102) & " ( " & _OperationName & " )"
                        End If

                        dtcheck.Dispose()
                    Else
                        _StatePass = True
                        olberror.ForeColor = Drawing.Color.Green
                    End If
                Else

                    olberror.Text = HI.MG.ShowMsg.GetMessage("Barcode ถูก Scan ด้วยหมายเลขอื่นไปแล้ว", 1407311101) & " (" & dt.Rows(0)!FTCMDocScanNo.ToString & ")"

                End If


            Else
                olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูล Barcode !!!", 1407311103)
            End If

            dt.Dispose()

            If (_StatePass) Then

                '*****check scan


                '****Check State Outline 
                _Qry = "Select Top 1   FTBarcodeNo"
                _Qry &= vbCrLf & " From TPRODBarcodeScanOutline WITH(NOLOCK)"
                _Qry &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
                _Qry &= vbCrLf & "UNION  "
                _Qry &= vbCrLf & "Select Top 1 FTBarcodeNo"
                _Qry &= vbCrLf & "From TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK)"
                _Qry &= vbCrLf & "where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                    olberror.ForeColor = Drawing.Color.Red
                    olberror.Text = HI.MG.ShowMsg.GetMessage("Barcode ถูก Scan ออกไลน์ไปแล้ว กรุณาตรวจสอบ", 1610261133) & " (" & dt.Rows(0)!FTCMDocScanNo.ToString & ")"
                    Me.FTBarcodeNo.Focus()
                    Me.FTBarcodeNo.SelectAll()
                    Exit Sub
                End If

                'HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
                '****End Check State Outline 




                If Me.FTCMDocScanNo.Text = "" Then
                    Try
                        If FTCMDocScanNo.Properties.Buttons.Count > 1 Then
                            If (FTCMDocScanNo.Properties.Buttons.Item(1).Visible) Then
                                FTCMDocScanNo.PerformClick(FTCMDocScanNo.Properties.Buttons.Item(1))

                                If Me.VerrifyData Then
                                    If Me.SaveData Then

                                    Else
                                        Exit Sub
                                    End If
                                Else
                                    Exit Sub
                                End If
                            End If
                        End If
                    Catch ex222 As Exception
                    End Try
                Else
                    If FTCMDocScanNo.Properties.Tag.ToString = "" Then

                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If

                    Else
                        If Me.FTCMDocScanNo.Text = "" Then Exit Sub
                    End If
                End If

                Dim tmpval As String = FNHSysOperationId.Text
                Dim tmpval2 As String = FNHSysOperationId.EditValue

                If tmpval2 Is Nothing Then
                    FNHSysOperationId.Text = ""
                    FNHSysOperationId.EditValue = -1
                Else
                    Try
                        FNHSysOperationId.EditValue = tmpval2
                    Catch ex As Exception
                        Try
                            FNHSysOperationId.Text = ""
                            FNHSysOperationId.EditValue = -1
                        Catch ex2 As Exception
                            FNHSysOperationId.Text = ""
                        End Try
                    End Try

                End If

                If Me.FNHSysOperationId.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysOperationId_lbl.Text, Me.Text)
                    Me.FNHSysOperationId.Focus()
                    Me.FNHSysOperationId.SelectAll()
                    Exit Sub
                End If

                Call LoadDataInfo(Me.FTCMDocScanNo.Text)
                Call SaveBarcode(Key)



                Call LoadDucumentDetail(Me.FTCMDocScanNo.Text)
                olberror.Text = HI.MG.ShowMsg.GetMessage("Scan Complete", 1407310005)
            Else
                Dim tmpval As String = FNHSysOperationId.Text
                Dim tmpval2 As String = FNHSysOperationId.EditValue

                If tmpval2 Is Nothing Then
                    FNHSysOperationId.Text = ""
                    FNHSysOperationId.EditValue = -1
                Else
                    Try
                        FNHSysOperationId.EditValue = tmpval2
                    Catch ex As Exception
                        Try
                            FNHSysOperationId.Text = ""
                            FNHSysOperationId.EditValue = -1
                        Catch ex2 As Exception
                            FNHSysOperationId.Text = ""
                        End Try
                    End Try

                End If
            End If

            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadOperation(_FTStyleCode As String, _FTOrderProdNo As String)
        Dim _Qry As String = ""
        Try

            If _FTStyleCode = "" Or _FTOrderProdNo = "" Then
                _Qry = "SELECT FNHSysOperationId,FTOperationName  FROM ( SELECT -1 AS FNHSysOperationId ,'' AS  FTOperationName,-1 AS FNSeq )"
                _Qry &= vbCrLf & "  ) AS A ORDER BY A.FNSeq "

            Else
                _Qry = "SELECT TOP 1 FTOrderProdNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) WHERE FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "' "

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                    _Qry = "SELECT FNHSysOperationId,FTOperationName  FROM ( SELECT -1 AS FNHSysOperationId ,'' AS  FTOperationName,-1 AS FNSeq  UNION "
                    _Qry &= vbCrLf & "  SELECT     A.FNHSysOperationId"

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameTH  AS FTOperationName "
                    Else
                        _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameEN  AS FTOperationName "
                    End If
                    _Qry &= vbCrLf & " ,  A.FNSeq"
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS A WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
                    _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleId=ISNULL((SELECT TOP 1 FNHSysStyleId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStylecode='" & HI.UL.ULF.rpQuoted(_FTStyleCode) & "'),0) AND FNOperationState =0 "
                    _Qry &= vbCrLf & "  ) AS A ORDER BY A.FNSeq "
                Else
                    _Qry = "SELECT FNHSysOperationId,FTOperationName  FROM ( SELECT -1 AS FNHSysOperationId ,'' AS  FTOperationName,-1 AS FNSeq  UNION "
                    _Qry &= vbCrLf & "  SELECT     A.FNHSysOperationId"

                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameTH  AS FTOperationName "
                    Else
                        _Qry &= vbCrLf & " , B.FTOperationCode +' :: ' + B.FTOperationNameEN  AS FTOperationName "
                    End If
                    _Qry &= vbCrLf & " ,  A.FNSeq"
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS A WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS B WITH(NOLOCK) ON A.FNHSysOperationId = B.FNHSysOperationId"
                    _Qry &= vbCrLf & "  WHERE  A.FTOrderProdNo='" & HI.UL.ULF.rpQuoted(_FTOrderProdNo) & "' AND FNOperationState =0  "
                    _Qry &= vbCrLf & "  ) AS A ORDER BY A.FNSeq "

                End If
            End If

            Dim dt As DataTable
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            Dim tmpval As String = FNHSysOperationId.Text
            Dim tmpval2 As String = FNHSysOperationId.EditValue

            Me.FNHSysOperationId.Properties.DataSource = dt.Copy

            If tmpval2 Is Nothing Then
                ' FNHSysOperationId.Text = ""
                FNHSysOperationId.EditValue = -1
            Else
                Try
                    FNHSysOperationId.EditValue = tmpval2
                Catch ex As Exception
                    Try
                        ' FNHSysOperationId.Text = ""
                        FNHSysOperationId.EditValue = -1
                    Catch ex2 As Exception
                        FNHSysOperationId.Text = ""
                    End Try
                End Try

            End If



            dt.Dispose()
        Catch ex As Exception

        End Try


    End Sub

#End Region



    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If FTCMDocScanNo.Text <> "" Then
                    Call LoadBarcodeInfo(FTBarcodeNo.Text)
                Else
                    If FTBarcodeNo.Text = "" Then Exit Sub
                    Dim _Barcode As String = FTBarcodeNo.Text
                    PROD.DynamicButtone_ButtonClick(Me.FTCMDocScanNo)
                    FTBarcodeNo.Text = _Barcode
                    Call LoadBarcodeInfo(FTBarcodeNo.Text)

                End If

        End Select
    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub

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

        FNHSysOperationId.TabStop = False

        Call LoadOperation("", "")
    End Sub

    Private Sub ocmdeletebarcode_Click(sender As Object, e As EventArgs) Handles ocmdeletebarcode.Click
        If CheckOwner() = False Then Exit Sub

        With ogvdetail
            '****Check State Outline 
            Dim _Qry As String = ""

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _BarKey As String = "" & .GetFocusedRowCellValue("FTBarcodeBundleNo").ToString

            _Qry = " Select Top 1   FTBarcodeNo"
            _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline WITH(NOLOCK)"
            _Qry &= vbCrLf & " where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarKey) & "'"
            _Qry &= vbCrLf & " UNION  "
            _Qry &= vbCrLf & " Select Top 1 FTBarcodeNo"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan_Detail WITH(NOLOCK) "
            _Qry &= vbCrLf & " where FTBarcodeNo = '" & HI.UL.ULF.rpQuoted(_BarKey) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                olberror.ForeColor = Drawing.Color.Red
                olberror.Text = HI.MG.ShowMsg.GetMessage("Barcode ถูก Scan ออกไลน์ไปแล้ว กรุณาตรวจสอบ", 1611011651)
                Me.FTBarcodeNo.Focus()
                Me.FTBarcodeNo.SelectAll()
                Exit Sub
            End If
            'HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
            '****End Check State Outline 


            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()


                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeBundleNo").ToString
                Dim _Operation As String = "" & .GetRowCellValue(i, "FNHSysOperationId").ToString
                Dim _OderProdNo As String = "" & .GetRowCellValue(i, "FTOrderProdNo").ToString
                Dim _OderNo As String = "" & .GetRowCellValue(i, "FTOrderNo").ToString
                Dim _StyleCode As String = ""
                Dim _Quantity As Double = Double.Parse("" & .GetRowCellValue(i, "FNQuantity").ToString)

                If _Barcode <> "" Then
                    With New PROD
                        _StyleCode = .GetStyleCodeByOrderNo(_OderNo)
                        If .CheckOperationAfter(_StyleCode, _OderProdNo, _Barcode, Integer.Parse(_Operation), _Quantity) Then

                            If Me.DeleteBarcode(_Barcode, Integer.Parse(_Operation)) = True Then
                                _StateDelete = True
                            End If
                        Else
                            olberror.ForeColor = Drawing.Color.Red
                            olberror.Text = .MessageCheck
                        End If
                    End With
                End If

            Next
            If (_StateDelete) Then
                LoadDucumentDetail(Me.FTCMDocScanNo.Text)
            End If
        End With
    End Sub

    Private Sub FTStyleCode_EditValueChanged(sender As Object, e As EventArgs) Handles FTStyleCode.EditValueChanged, FTOrderProdNo.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTStyleCode_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOperation(FTStyleCode.Text, FTOrderProdNo.Text)
        End If

    End Sub

End Class