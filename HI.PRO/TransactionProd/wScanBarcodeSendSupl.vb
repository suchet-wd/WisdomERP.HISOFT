Imports System.Windows.Forms

Public Class wScanBarcodeSendSupl

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
        Dim sFieldCount As String = "FTBarcodeSendSuplNo"
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

        _Str = "SELECT Top 1       FTStateBranchAccept, FTStateBranchAcceptBy, FTStateBranchAcceptDate  "
        _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTSendSuplToBranch AS M WITH (NOLOCK)"
        _Str &= vbCrLf & " WHERE  " & Me.MainKey & "='" & Key.ToString & "' "
        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        _FieldName = ""
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

        'HI.TL.HandlerControl.ClearControl(ogbbarcodeinfo)
        olberror.Text = ""
        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable

            _Qry = " SELECT  A.FTBarcodeSendSuplNo,BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, A.FNHSysPartId"
            _Qry &= vbCrLf & " 	, A.FNSendSuplType"
            _Qry &= vbCrLf & " 	, A.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, A.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, A.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, A.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, A.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, BB.FTColorway"
            _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, BB.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	,MP.FTPartCode "
            _Qry &= vbCrLf & " 	,ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId) AS FNHSysOperationId"
            _Qry &= vbCrLf & " 	,ISNULL(OPP.FNSeq,OPS.FNSeq) AS FNSeq"
            _Qry &= vbCrLf & " 	,ISNULL(OPP.FNHSysOperationIdTo,OPS.FNHSysOperationIdTo) AS FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	,Mpp.FTOperationCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(BBXT.FNHSysMarkId,0) AS FNHSysMarkId"
            _Qry &= vbCrLf & " ,ISNULL(BBXT.FTMarkName,'') FTMarkName"

            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  A.FTOrderProdNo  INNER JOIN"
            '   _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo and A.FNHSysPartId = SD.FNHSysPartId and A.FNSendSuplType = SD.FNSendSuplType LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationId = OPS.FNHSysOperationId LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId)  = MPP.FNHSysOperationId"


            _Qry &= vbCrLf & "  outer apply(select  AX.FTMainBarcodeBundleNo AS FTBarcodeBundleNo ,AX.FTPOLineItemNo,MIN(AX.FNBunbleSeq) As FNBunbleSeq, AX.FTColorway, AX.FTSizeBreakDown, SUM(AX.FNQuantity) AS FNQuantity "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS AX WITH(NOLOCK) WHERE AX.FTMainBarcodeBundleNo = A.FTBarcodeBundleNo "
            _Qry &= vbCrLf & "  Group BY AX.FTMainBarcodeBundleNo,AX.FTPOLineItemNo,AX.FTColorway, AX.FTSizeBreakDown  "
            _Qry &= vbCrLf & "  ) AS BB"

            _Qry &= vbCrLf & "  outer apply( "
            _Qry &= vbCrLf & "   SELECT        TOP 1  BBX.FNHSysMarkId, "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  CXZ.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  CXZ.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AAX  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS BBX  WITH (NOLOCK) ON AAX.FTLayCutNo = BBX.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS CXZ  WITH (NOLOCK)  ON BBX.FNHSysMarkId = CXZ.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AAX.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"


            _Qry &= vbCrLf & "  ) AS BBXT"




            _Qry &= vbCrLf & "   WHERE B.FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "  ORDER BY A.FTBarcodeSendSuplNo "


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
                                Dim _CmpH
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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch  WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'  "
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode  WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'  "
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'")

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
                                '.Properties.Tag = R.Item(Col).ToString
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

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTSendSuplBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function CheckCreatePurchaseSendSuplier() As Boolean

        Dim _Qry As String

        _Qry = "SELECT TOP 1  FTSendSuplNo "
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_DocSendRef AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1487789111, "พบการเปิด PO ส่ง Supplier แล้ว ไม่สามารถทำการลบหรือแก้ไขได้ !!! ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

    End Function

    Private Function CheckTransferToBranchAccept() As Boolean

        Dim _Qry As String

        _Qry = "SELECT TOP 1  FTStateBranchAccept "
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "1" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1508119111, "พบข้อมูลสาขาทำการรับแล้ว ไม่สามารถเพิ่มหรือแก้ไขได้ !!! ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

    End Function

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub
        If CheckTransferToBranchAccept() = False Then Exit Sub
        VerrifyState()
        If Me.FTStateScanSendFinish.Checked And Me.FTStateSendExcel.Checked Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถแสกนบาร์โค้ดเพิ่มในใบส่งใบนี้ได้เนื่องจากได้มีการกด finish กรุณาสร้างใบใหม่ !!!! ", 1705051447, Me.Text, "", MessageBoxIcon.Error)
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
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub
        If CheckTransferToBranchAccept() = False Then Exit Sub
        VerrifyState()
        If Me.FTStateScanSendFinish.Checked And Me.FTStateSendExcel.Checked Then
            HI.MG.ShowMsg.mInfo("ไม่สามารถแสกนบาร์โค้ดเพิ่มในใบส่งใบนี้ได้เนื่องจากได้มีการกด finish กรุณาสร้างใบใหม่ !!!! ", 1705051447, Me.Text, "", MessageBoxIcon.Error)
            Exit Sub
        End If
        If Me.FTSendSuplNo.Text.Trim <> "" And Me.FTSendSuplNo.Properties.Tag.ToString <> "" Then
            Dim _Qry As String = ""

            _Qry = "   Select TOP 1  B.FTSendSuplNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH(NOLOCK)  ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "  WHERE   B.FTSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "' "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

                Dim _StateDelete As Boolean = True
                With (CType(Me.ogcdetail.DataSource, DataTable))
                    .AcceptChanges()

                    For Each R As DataRow In .Rows

                        Dim _Barcode As String = R!FTBarcodeSendSuplNo.ToString
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

                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.FTSendSuplNo.Text, Me.Text) = True Then

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

                HI.MG.ShowMsg.mInfo("เอกสารถูกนำไป Scam รับ แล้ว ไม่สามารถทำการลบได้ !!!", 1408040001, Me.Text, , MessageBoxIcon.Warning)
                FTSendSuplNo.Focus()
                FTSendSuplNo.SelectAll()

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTSendSuplNo_lbl.Text)
            Me.FTSendSuplNo.Focus()

        End If

    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        olberror.Text = ""
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTSendSuplNo.Text <> "" Then

            Dim cmd As String = ""
            cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[SP_CREATE_TEMPSENDSUPL] '" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'"
            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PROD)

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "SendSuplSlip.rpt"
                .Formular = "{TPRODTSendSupl.FTSendSuplNo}='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTSendSuplNo_lbl.Text)
            FTSendSuplNo.Focus()
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

            _Str = " SELECT TOP 1 FTBarcodeSendSuplNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode WITH(NOLOCK) WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode(FTInsUser, FDInsDate, FTInsTime, FTSendSuplNo, FTBarcodeSendSuplNo)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & "  WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

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

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode  WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode  WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "' AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = " DELETE A "
            _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch  AS A "
            _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode  AS B ON A.FTSendSuplNo = B.FTSendSuplNo "
            _Str &= vbCrLf & "  WHERE A.FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'  AND B.FTBarcodeSendSuplNo IS NULL "
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

            _Qry = " SELECT  TOP 1 A.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & " 	, A.FNHSysPartId"
            _Qry &= vbCrLf & " 	, A.FNSendSuplType"
            _Qry &= vbCrLf & " 	, A.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, A.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTOrderNo "
            _Qry &= vbCrLf & " 	, A.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, A.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, A.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, BB.FTColorway"
            _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, BB.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	,MP.FTPartCode "
            _Qry &= vbCrLf & " 	,ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId) AS FNHSysOperationId"
            _Qry &= vbCrLf & " 	,ISNULL(OPP.FNSeq,OPS.FNSeq) AS FNSeq"
            _Qry &= vbCrLf & " 	,ISNULL(OPP.FNHSysOperationIdTo,OPS.FNHSysOperationIdTo) AS FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	,Mpp.FTOperationCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(BBXT.FNHSysMarkId,0) AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  A.FTOrderProdNo  INNER JOIN"
            ' _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationId = OPS.FNHSysOperationId LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId)  = MPP.FNHSysOperationId"

            _Qry &= vbCrLf & " 	OUTER APPLY (SELECt  MIN(BBX.FNBunbleSeq) AS FNBunbleSeq,BBX.FTColorway,BBX.FTSizeBreakDown ,SUM(BBX.FNQuantity) AS FNQuantity FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BBX WITH (NOLOCK)  WHERE BBX.FTMainBarcodeBundleNo  =  A.FTBarcodeBundleNo GROUP BY BBX.FTColorway,BBX.FTSizeBreakDown) AS BB "


            _Qry &= vbCrLf & "  outer apply( "
            _Qry &= vbCrLf & "   SELECT        TOP 1  BBX.FNHSysMarkId "

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AAX  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS BBX  WITH (NOLOCK) ON AAX.FTLayCutNo = BBX.FTLayCutNo "

            _Qry &= vbCrLf & "   WHERE        (AAX.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"


            _Qry &= vbCrLf & "  ) AS BBXT"

            _Qry &= vbCrLf & "   WHERE A.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
            FTStyleCode.Text = ""
            FTOrderNo.Text = ""
            FTOrderProdNo.Text = ""
            FTColorway.Text = ""
            FTSizeBreakDown.Text = ""
            FNHSysOperationId.Text = ""
            FNHSysMarkId.Text = ""
            FNHSysPartId.Text = ""
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
                    FNHSysOperationId.Text = R!FTOperationName.ToString
                    FNHSysMarkId.Text = R!FNHSysMarkId.ToString
                    FNHSysPartId.Text = R!FTPartName.ToString
                    FTBarcodeBundleNo.Text = R!FNBunbleSeq.ToString

                    If Me.FNHSysSuplId.Text = "" Then
                        Me.FNHSysSuplId.Text = R!FTSuplCode.ToString
                    End If

                    Exit For

                Next

                If dt.Select("FTSuplCode='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "'").Length > 0 Then
                    If dt.Rows(0)!FTSendSuplNo.ToString = "" Or dt.Rows(0)!FTSendSuplNo.ToString = Me.FTSendSuplNo.Text Then
                        If Integer.Parse(Val(dt.Rows(0)!FNHSysOperationIdTo.ToString)) > 0 Then

                            Dim dtcheck As DataTable
                            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CheckOperationBefore '" & HI.UL.ULF.rpQuoted(FTOrderProdNo.Text) & "'," & Integer.Parse(Val(dt.Rows(0)!FNHSysOperationIdTo.ToString)) & ",'" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
                            dtcheck = (HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD))

                            If dtcheck.Rows.Count > 0 Then

                                If Val(dtcheck.Rows(0)!FNQuantity.ToString) >= Val(dt.Rows(0)!FNQuantity.ToString) Then
                                    _StatePass = True
                                    olberror.ForeColor = Drawing.Color.Green

                                Else
                                    olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่สามารถ Scan เกินยอดขั้นตอนก่อนหน้าได้ !!!", 1407310004) & "   " & Format((Val(dtcheck.Rows(0)!FNQuantity.ToString)), "#,#0")
                                End If

                            Else

                                Dim _OperationName As String = ""
                                With New PROD
                                    _OperationName = .GetOpertionName(Integer.Parse(Val(dt.Rows(0)!FNHSysOperationIdTo.ToString)))
                                End With

                                olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูลขั้นตอนก่อนหน้า หรือยังไม่ได้ทำการ Scan กรุณาทำการตรวจสอบ !!!", 1407311102) & " ( " & _OperationName & " )"


                                '  olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูลขั้นตอนก่อนหน้า หรือยังไม่ได้ทำการ Scan กรุณาทำการตรวจสอบ !!!", 1407310002)
                            End If

                            dtcheck.Dispose()
                        Else
                            _StatePass = True
                            olberror.ForeColor = Drawing.Color.Green
                        End If
                    Else

                        olberror.Text = HI.MG.ShowMsg.GetMessage("Barcode ถูก Scan ส่ง Supplier ด้วยกายเลขอื่นไปแล้ว", 1407310001) & " (" & dt.Rows(0)!FTSendSuplNo.ToString & ")"

                    End If
                Else
                    olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่ใช่ Barcode ของ Supplier นี้ !!!", 1407310006)
                End If

            Else
                olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูล Barcode !!!", 1407310003)
            End If

            dt.Dispose()

            If (_StatePass) Then

                If Me.FTSendSuplNo.Text = "" Then
                    Try
                        If FTSendSuplNo.Properties.Buttons.Count > 1 Then
                            If (FTSendSuplNo.Properties.Buttons.Item(1).Visible) Then
                                FTSendSuplNo.PerformClick(FTSendSuplNo.Properties.Buttons.Item(1))

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
                    If FTSendSuplNo.Properties.Tag.ToString = "" Then

                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If

                    Else
                        If Me.FTSendSuplNo.Text = "" Then Exit Sub

                    End If
                End If

                If FNHSysSuplId.Properties.ReadOnly = False Then
                    Call LoadDataInfo(Me.FTSendSuplNo.Text)
                End If

                If Me.FTSendSuplNo.Text = "" Then Exit Sub
                If Me.FNHSysSuplId.Text = "" Then Exit Sub
                If Me.FNHSysSuplId.Properties.Tag.ToString = "" Then Exit Sub

                If SaveBarcode(Key) Then
                    Call LoadDucumentDetail(Me.FTSendSuplNo.Text)
                    Dim _Cmd As String = ""
                    If Me.FTStateScanSendFinish.Checked Then
                        _Cmd = "Update TPRODTSendSupl Set FTStateScanSendFinish='0'  "
                        _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        'HI.MG.ShowMsg.mInfo("Process Successfuly !!", 1705051408, Me.Text, "", MessageBoxIcon.Information)

                    End If


                    If Me.FNHSysCmpIdTo.Text.Trim <> "" Then
                        Call SaveSendSuplToBranch(Me.FTSendSuplNo.Text, Key)
                        If Me.FTStateScanSendFinish.Checked Then
                            _Cmd = "Update TPRODTSendSuplToBranch Set FTStateScanSendFinish='0'  "
                            _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"
                            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        End If
                    End If

                    Me.FTStateScanSendFinish.Checked = False

                End If

                olberror.Text = HI.MG.ShowMsg.GetMessage("Scan Complete", 1407310005)
            End If

            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub SaveSendSuplToBranch(DocKey As String, BarKey As String)
        Dim _Qry As String = ""

        _Qry = "SELECT TOP 1 FTSendSuplNo "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch("
            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FDSendSuplDate, FTSendSuplBy, FNHSysSuplId, FNSendSuplState, FTRemark, FNHSysCmpId, FNHSysCmpIdTo"
            _Qry &= vbCrLf & " )"
            _Qry &= vbCrLf & " SELECT FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FDSendSuplDate, FTSendSuplBy, FNHSysSuplId, FNSendSuplState, FTRemark, FNHSysCmpId," & Val(FNHSysCmpIdTo.Properties.Tag.ToString()) & ""
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSendSuplNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

        End If

        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode("
        _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSendSuplNo, FTBarcodeSendSuplNo"
        _Qry &= vbCrLf & ", FNBunbleSeq, FNHSysPartId, FNSendSuplType, FNHSysSuplId, FTBarcodeBundleNo"
        _Qry &= vbCrLf & " , FTOrderNo, FTOrderProdNo, FTSendSuplRef, FNHSysCmpId, FTColorway, FTSizeBreakDown"
        _Qry &= vbCrLf & ", FNQuantity, FNHSysOperationId, FNSeq, FNHSysOperationIdTo, FNHSysMarkId"
        _Qry &= vbCrLf & " )"
        _Qry &= vbCrLf & " SELECT  TOP 1 "
        _Qry &= vbCrLf & "    B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime"
        _Qry &= vbCrLf & "  , B.FTSendSuplNo"
        _Qry &= vbCrLf & "  , B.FTBarcodeSendSuplNo"
        _Qry &= vbCrLf & "   ,BB.FNBunbleSeq"
        _Qry &= vbCrLf & " 	, A.FNHSysPartId"
        _Qry &= vbCrLf & " 	, A.FNSendSuplType"
        _Qry &= vbCrLf & " 	, A.FNHSysSuplId"
        _Qry &= vbCrLf & " 	, A.FTBarcodeBundleNo"
        _Qry &= vbCrLf & " 	, O.FTOrderNo "
        _Qry &= vbCrLf & " 	, A.FTOrderProdNo"
        _Qry &= vbCrLf & " 	, A.FTSendSuplRef"
        _Qry &= vbCrLf & " 	, A.FNHSysCmpId"
        _Qry &= vbCrLf & " 	, BB.FTColorway"
        _Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
        _Qry &= vbCrLf & " 	, BB.FNQuantity"
        _Qry &= vbCrLf & " 	, ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId) AS FNHSysOperationId"
        _Qry &= vbCrLf & " 	, ISNULL(OPP.FNSeq,OPS.FNSeq) AS FNSeq"
        _Qry &= vbCrLf & " 	, ISNULL(OPP.FNHSysOperationIdTo,OPS.FNHSysOperationIdTo) AS FNHSysOperationIdTo"
        _Qry &= vbCrLf & " ,  ISNULL(BBXT.FNHSysMarkId,0) AS FNHSysMarkId"
        _Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  A.FTOrderProdNo  INNER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.V_TPRODTBundle_MainBarcode AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo and A.FNHSysPartId = SD.FNHSysPartId and A.FNSendSuplType = SD.FNSendSuplType LEFT OUTER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationId = OPS.FNHSysOperationId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
        _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
        _Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationId = OPP.FNHSysOperationId "
        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId)  = MPP.FNHSysOperationId"

        _Qry &= vbCrLf & "  outer apply( "
        _Qry &= vbCrLf & "   SELECT        TOP 1  BBX.FNHSysMarkId "


        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AAX  WITH (NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS BBX  WITH (NOLOCK) ON AAX.FTLayCutNo = BBX.FTLayCutNo "

        _Qry &= vbCrLf & "   WHERE        (AAX.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"


        _Qry &= vbCrLf & "  ) AS BBXT"

        _Qry &= vbCrLf & "   WHERE B.FTSendSuplNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Qry &= vbCrLf & "   AND B.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(BarKey) & "' "

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

    End Sub

#End Region

    Private Function VerrifyState() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 Isnull(FTStateScanSendFinish,'0') as FTStateScanSendFinish ,isnull( FTStateSendExcel ,'0') AS FTStateSendExcel  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl with(nolock) "
            _Cmd &= vbCrLf & "where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTSendSuplNo.Text) & "'"
            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows
                Me.FTStateScanSendFinish.Checked = R!FTStateScanSendFinish.ToString = "1"
                Me.FTStateSendExcel.Checked = R!FTStateSendExcel.ToString = "1"
            Next
        Catch ex As Exception

        End Try
    End Function

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                VerrifyState()
                If Me.FTStateScanSendFinish.Checked And Me.FTStateSendExcel.Checked Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถแสกนบาร์โค้ดเพิ่มในใบส่งใบนี้ได้เนื่องจากได้มีการกด finish กรุณาสร้างใบใหม่ !!!! ", 1705051447, Me.Text, "", MessageBoxIcon.Error)
                    Exit Sub
                End If
                If CheckCreatePurchaseSendSuplier() = False Then Exit Sub
                If CheckTransferToBranchAccept() = False Then Exit Sub

                Call LoadBarcodeInfo(FTBarcodeNo.Text)
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

            Me.FNHSysSuplId.Properties.ReadOnly = (dt.Rows.Count > 0)
            FNHSysSuplId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            Me.FNSendSuplState.Properties.ReadOnly = (dt.Rows.Count > 0)
            dt.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wScanBarcodeSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        FTBarcodeNo.EnterMoveNextControl = False
    End Sub

    Private Sub ocmdeletebarcode_Click(sender As Object, e As EventArgs) Handles ocmdeletebarcode.Click
        If CheckOwner() = False Then Exit Sub
        If CheckCreatePurchaseSendSuplier() = False Then Exit Sub
        If CheckTransferToBranchAccept() = False Then Exit Sub

        If Me.FTStateSendExcel.Checked Then Exit Sub



        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _Qry As String = ""
            Dim _BarKey As String = "" & .GetFocusedRowCellValue("FTBarcodeSendSuplNo").ToString

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeSendSuplNo").ToString
                VerrifyState()
                If Me.FTStateScanSendFinish.Checked And Me.FTStateSendExcel.Checked = False Then
                    Dim _Cmd As String = ""
                    _Cmd = "Update TPRODTSendSupl Set FTStateScanSendFinish='0'  "
                    _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    _Cmd = "Update TPRODTSendSuplToBranch Set FTStateScanSendFinish='0'  "
                    _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    Me.FTStateScanSendFinish.Checked = False
                End If
                If _Barcode <> "" Then

                    _Qry = "   Select TOP 1  B.FTSendSuplNo"
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS A WITH(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH(NOLOCK)  ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo"
                    _Qry &= vbCrLf & "  WHERE   A.FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(_Barcode) & "' "

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                        Dim _Operation As String = "" & .GetRowCellValue(i, "FNHSysOperationId").ToString
                        Dim _OderProdNo As String = "" & .GetRowCellValue(i, "FTOrderProdNo").ToString
                        Dim _OderNo As String = "" & .GetRowCellValue(i, "FTOrderNo").ToString
                        Dim _StyleCode As String = ""
                        Dim _Quantity As Double = Double.Parse("" & .GetRowCellValue(i, "FNQuantity").ToString)

                        If _Barcode <> "" Then

                            With New PROD
                                _StyleCode = .GetStyleCodeByOrderNo(_OderNo)
                                If .CheckOperationAfter(_StyleCode, _OderProdNo, _Barcode, Integer.Parse(_Operation), _Quantity) Then
                                    If DeleteBarcode(_Barcode) Then
                                        _StateDelete = True
                                    End If
                                Else
                                    olberror.ForeColor = Drawing.Color.Red
                                    olberror.Text = .MessageCheck
                                End If
                            End With
                        End If

                    End If
                End If
            Next

            If (_StateDelete) Then
                LoadDucumentDetail(Me.FTSendSuplNo.Text)
            End If

        End With

    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged

        If (Me.InvokeRequired) Then

            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysSuplId_EditValueChanged), New Object() {sender, e})

        Else

            Me.FNHSysCmpIdTo.Text = ""
            Me.FNHSysCmpIdTo_None.Text = ""

            Dim _Qry As String = ""

            _Qry = "  Select TOP 1  B.FTCmpCode"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS A WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS B WITH(NOLOCK) ON A.FNHSysCmpId = B.FNHSysCmpId"
            _Qry &= vbCrLf & "  WHERE  (A.FTSuplCode = N'" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text.Trim) & "')"

            Me.FNHSysCmpIdTo.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

        End If

    End Sub

    Private Sub ocmFinish_Click(sender As Object, e As EventArgs) Handles ocmFinish.Click
        Try
            Dim _Cmd As String = ""
            If Not (Me.FTStateScanSendFinish.Checked) Then
                If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, "แสกนบาร์ในใบส่งเสร็จสิ้น ") Then Exit Sub
                _Cmd = "Update TPRODTSendSupl Set FTStateScanSendFinish='1'  "
                _Cmd &= vbCrLf & ",FDDateSendFinish=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                _Cmd = "Update TPRODTSendSuplToBranch Set FTStateScanSendFinish='1'  "
                _Cmd &= vbCrLf & ",FDDateSendFinish=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(FTSendSuplNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                HI.MG.ShowMsg.mInfo("Process Successfuly !!", 1705051408, Me.Text, "", MessageBoxIcon.Information)
                Me.FTStateScanSendFinish.Checked = True
            End If
        Catch ex As Exception
            HI.MG.ShowMsg.mInfo("Process Fail !!!! ", 1705051408, Me.Text, "", MessageBoxIcon.Error)
            Me.FTStateScanSendFinish.Checked = False
        End Try
    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub
End Class