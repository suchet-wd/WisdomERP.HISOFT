Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wScanBarcodeReceiveSuplApprove

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
        '  Me.InitFormControl()

        '    Call InitGrid()

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

            _objId = Integer.Parse(_dt.Rows(0) !FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0) !FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0) !FHSysTableName.ToString
            Me.TableName = _dt.Rows(0) !FTTableName.ToString

            _SortField = _dt.Rows(0) !FTSortField.ToString

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
        Dim _Str As String = "" ' Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "
        _Str = "  Select TOP 1  "
        _Str &= vbCrLf & "FTInsUser,FDInsDate,FTInsTime,FTUpdUser,FDUpdDate,FTUpdTime,FTRcvSuplNo,FTInvoiceDate"
        _Str &= vbCrLf & ",ISNULL((SELECT TOP 1  FTSuplCode FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier WITH ( NOLOCK ) WHERE FNHSysSuplId=M.FNHSysSuplId),'') AS FNHSysSuplId"
        _Str &= vbCrLf & ",FNSendSuplState,FTRemark,FNHSysCmpId ,FNRcvSuplType ,FTInvoiceNo ,FTRcvSuplBy ,FDRcvSuplDate"
        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl As M WITH(NOLOCK)  "
        _Str &= vbCrLf & "WHERE  FTRcvSuplNo='" & Key.ToString & "' "
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

        Call LoadStateApp(Key.ToString)
        Call LoadDucumentDetail(Key.ToString)

        'HI.TL.HandlerControl.ClearControl(ogbbarcodeinfo)
        'olberror.Text = ""

        _ProcLoad = False
    End Sub

    Private Sub LoadStateApp(Key As String)
        Try
            Dim _Cmd As String = ""
            Me.FTStateAccept.Checked = False
            Me.FDApproveDate.Text = ""
            Me.FTApproveTime.Text = ""
            Me.FTApproveBy.Text = ""

            '_Cmd = "SELECT Top 1 FTRcvSuplNo, Case WHEN isnull(FTStateBranchAccept,'0') = '1' Then '1' Else '0' END as  FTStateApprove"
            '_Cmd &= vbCrLf & " ,Case When isnull(FTStateBranchAccept,'0') = '1' Then FTStateBranchAcceptBy Else FTStateBranchCancelAcceptBy END as FTApproveBy"
            '_Cmd &= vbCrLf & " ,Case When isnull(FTStateBranchAccept,'0') = '1' Then Convert(varchar(10),convert(date,FTStateBranchAcceptDate),103) Else Convert(varchar(10),convert(date,FTStateBranchCancelAcceptDate),103) END as FDApproveDate"
            '_Cmd &= vbCrLf & " ,Case When isnull(FTStateBranchAccept,'0') = '1' Then FTStateBranchAcceptTime Else FTStateBranchCancelAcceptTime END as FTApproveTime"
            _Cmd = "SELECT Top 1 FTRcvSuplNo, Case WHEN isnull(FTStateBranchAccept,'0') = '1' Then '1' Else '0' END as  FTStateApprove"
            _Cmd &= vbCrLf & " ,   FTStateBranchAcceptBy  as FTApproveBy"
            _Cmd &= vbCrLf & " , case when ISDATE(FTStateBranchAcceptDate) = 1 Then   Convert(varchar(10),convert(date,FTStateBranchAcceptDate),103) ELse '' END as FDApproveDate"
            _Cmd &= vbCrLf & " ,  FTStateBranchAcceptTime  as FTApproveTime"
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTReceiveSuplToBranch WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "'"
            For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows
                Me.FTStateAccept.Checked = IIf(R!FTStateApprove.ToString = "1", True, False)
                Me.FDApproveDate.Text = R!FDApproveDate.ToString
                Me.FTApproveTime.Time = R!FTApproveTime.ToString
                Me.FTApproveBy.Text = R!FTApproveBy.ToString
            Next
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadDucumentDetail(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable

            '_Qry = " SELECT  A.FTBarcodeSendSuplNo,BB.FNBunbleSeq"
            '_Qry &= vbCrLf & " 	, A.FNHSysPartId"
            '_Qry &= vbCrLf & " 	, A.FNSendSuplType"
            '_Qry &= vbCrLf & " 	, A.FNHSysSuplId"
            '_Qry &= vbCrLf & " 	, A.FTBarcodeBundleNo"
            '_Qry &= vbCrLf & " 	, O.FTOrderNo "
            '_Qry &= vbCrLf & " 	, A.FTOrderProdNo"
            '_Qry &= vbCrLf & " 	, A.FTSendSuplRef"
            '_Qry &= vbCrLf & " 	, A.FNHSysCmpId"
            '_Qry &= vbCrLf & " 	, S.FTSuplCode"
            '_Qry &= vbCrLf & " 	, B.FTRcvSuplNo"
            '_Qry &= vbCrLf & " 	, BB.FNBunbleSeq"
            '_Qry &= vbCrLf & " 	, BB.FTColorway"
            '_Qry &= vbCrLf & " 	, BB.FTSizeBreakDown"
            '_Qry &= vbCrLf & " 	, BB.FNQuantity"
            '_Qry &= vbCrLf & " 	, ST.FTStyleCode"
            '_Qry &= vbCrLf & " 	,MP.FTPartCode "
            '_Qry &= vbCrLf & " 	,ISNULL(OPP.FNHSysOperationId,OPS.FNHSysOperationId) AS FNHSysOperationId"
            '_Qry &= vbCrLf & " 	,ISNULL(OPP.FNSeq,OPS.FNSeq) AS FNSeq"
            '_Qry &= vbCrLf & " 	,ISNULL(OPP.FNHSysOperationIdTo,OPS.FNHSysOperationIdTo) AS FNHSysOperationIdTo"
            '_Qry &= vbCrLf & " 	,Mpp.FTOperationCode "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
            '    _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            'Else
            '    _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
            '    _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            'End If

            '_Qry &= vbCrLf & " ,ISNULL(("

            '_Qry &= vbCrLf & "   SELECT        TOP 1  "

            'If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '    _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            'Else
            '    _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            'End If

            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            '_Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"

            '_Qry &= vbCrLf & " ),'') AS FNHSysMarkId"

            '_Qry &= vbCrLf & " ,ISNULL(("

            '_Qry &= vbCrLf & "   SELECT        TOP 1  C.FNHSysMarkId "



            '_Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            '_Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"

            '_Qry &= vbCrLf & " ),'') AS FFNHSysMarkId"


            '_Qry &= vbCrLf & " 	, SSB.FTSendSuplNo "

            '_Qry &= vbCrLf & " 	 FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) INNER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId RIGHT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo RIGHT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  ON  ODP.FTOrderProdNo =  A.FTOrderProdNo  INNER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_SendSupl AS SD WITH (NOLOCK)  ON A.FTSendSuplRef = SD.FTSendSuplRef AND ODP.FTOrderProdNo = SD.FTOrderProdNo and A.FNHSysPartId = SD.FNHSysPartId and A.FNSendSuplType = SD.FNSendSuplType LEFT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationByStyle AS OPS WITH (NOLOCK)  ON O.FNHSysStyleId = OPS.FNHSysStyleId AND SD.FNHSysOperationIdTo = OPS.FNHSysOperationId LEFT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId  LEFT OUTER JOIN "
            '_Qry &= vbCrLf & " 	        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
            '_Qry &= vbCrLf & " 	        LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOperationByOrderProd AS OPP WITH (NOLOCK)  ON ODP.FTOrderProdNo = OPP.FTOrderProdNo AND SD.FNHSysOperationIdTo = OPP.FNHSysOperationId "
            '_Qry &= vbCrLf & " 	        LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  SD.FNHSysOperationIdTo  = MPP.FNHSysOperationId"
            '_Qry &= vbCrLf & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS SSB  WITH (NOLOCK)   ON  B.FTBarcodeSendSuplNo = SSB.FTBarcodeSendSuplNo "
            '_Qry &= vbCrLf & "          WHERE B.FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            '_Qry &= vbCrLf & "  ORDER BY A.FTBarcodeSendSuplNo "


            _Qry = " Select  case when  Isnull(RCVB.FNBalQuantity,0) > 0 then '1' else '0' end as FTSelect ,   A.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "	, A.FNHSysPartId"
            _Qry &= vbCrLf & "	, A.FNSendSuplType"
            _Qry &= vbCrLf & "	, A.FNHSysSuplId"
            _Qry &= vbCrLf & "	, A.FTBarcodeBundleNo "
            _Qry &= vbCrLf & "	, A.FTOrderNo "
            _Qry &= vbCrLf & "	, A.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, A.FTSendSuplRef"
            _Qry &= vbCrLf & "  , A.FNHSysCmpId"
            _Qry &= vbCrLf & "	, S.FTSuplCode"
            _Qry &= vbCrLf & "	, A.FTSendSuplNo"
            _Qry &= vbCrLf & "  , A.FNBunbleSeq"
            _Qry &= vbCrLf & "  , A.FTColorway"
            _Qry &= vbCrLf & "  , A.FTSizeBreakDown"
            _Qry &= vbCrLf & "  , A.FNQuantity"
            _Qry &= vbCrLf & "	, ST.FTStyleCode"
            _Qry &= vbCrLf & "	,MP.FTPartCode "
            _Qry &= vbCrLf & "	,A.FNHSysOperationId"
            _Qry &= vbCrLf & "  ,A.FNSeq"
            _Qry &= vbCrLf & "  ,A.FNHSysOperationIdTo"
            _Qry &= vbCrLf & "	,Mpp.FTOperationCode"
            _Qry &= vbCrLf & " , Isnull(RCVB.FNBalQuantity,0) AS FNBalQuantity"
            _Qry &= vbCrLf & ", Isnull(RCVB.FNBalQuantity,0) AS FNBalOrign"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(("
            _Qry &= vbCrLf & "   SELECT        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            End If



            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
            _Qry &= vbCrLf & " ),'') AS FNHSysMarkId"

            _Qry &= vbCrLf & "	, RCVB.FTRcvSuplNo"
            _Qry &= vbCrLf & " ,convert(varchar(10) , convert( date , RCVB.FDInsDate) , 103 ) as FDInsDate "
            _Qry &= vbCrLf & " ,RCVB.FTInsTime  "

            _Qry &= vbCrLf & "	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode AS A WITH (NOLOCK)  "
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON A.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  A.FNHSysOperationId  = MPP.FNHSysOperationId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode AS RCVB  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = RCVB.FTBarcodeSendSuplNo "
            _Qry &= vbCrLf & " where RCVB.FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            '_Qry &= vbCrLf & " and isnull(RCVB.FTStateBranchAccept,'0') ='1'   "
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

            If Me.FNRcvSuplType.SelectedIndex = 1 Then
                Call SaveDataSendBranch(_Key)
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

    Private Function SaveDataSendBranch(ByVal _Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            '_Cmd = " SELECT  TOP (1) S.FNHSysCmpId  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS S WITH (NOLOCK) INNER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B WITH (NOLOCK) ON S.FTSendSuplNo = B.FTSendSuplNo"
            '_Cmd &= vbCrLf & "Where B.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeBundleNo.Text) & "'"
            Dim _CmpId As Integer = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0")
            'SELECT        FTInsUser, FDInsDate, FTInsTime, FTRcvSuplNo, FDRcvSuplDate, FTRcvSuplBy, FNHSysSuplId, FNSendSuplState, FTRemark, FNHSysCmpId, FTInvoiceNo, FTInvoiceDate, FNRcvSuplType, FNHSysCmpIdTo
            ' FROM TPRODTReceiveSuplToBranch



            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch "
            _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
            _Cmd &= vbCrLf & ",FTInvoiceNo='" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
            _Cmd &= vbCrLf & ",FTInvoiceDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTInvoiceDate.Text) & "'"
            _Cmd &= vbCrLf & ",FNHSysCmpIdTo=" & _CmpId
            _Cmd &= vbCrLf & "Where FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch "
                _Cmd &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTRcvSuplNo, FDRcvSuplDate, FTRcvSuplBy, FNHSysSuplId, FNSendSuplState"
                _Cmd &= vbCrLf & ", FTRemark, FNHSysCmpId, FTInvoiceNo, FTInvoiceDate, FNRcvSuplType, FNHSysCmpIdTo)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDRcvSuplDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplBy.Text) & "'"
                _Cmd &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysSuplId.Properties.Tag)
                _Cmd &= vbCrLf & "," & HI.TL.CboList.GetListValue("FNSendSuplState", Me.FNSendSuplState.SelectedIndex)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Cmd &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTInvoiceNo.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTInvoiceDate.Text) & "'"
                _Cmd &= vbCrLf & "," & HI.TL.CboList.GetListValue("FNRcvSuplType", Me.FNRcvSuplType.SelectedIndex)
                _Cmd &= vbCrLf & "," & _CmpId

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If

        Catch ex As Exception
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
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'")

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

#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs)
        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs)
        If CheckOwner() = False Then Exit Sub
        If Me.FTRcvSuplNo.Text.Trim <> "" And Me.FTRcvSuplNo.Properties.Tag.ToString <> "" Then


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
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.FTRcvSuplNo.Text, Me.Text) = True Then
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
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTRcvSuplNo_lbl.Text)
            Me.FTRcvSuplNo.Focus()
        End If

    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs)
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTRcvSuplNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "ReceiveSuplSlip.rpt"
                .Formular = "{TPRODTReceiveSupl.FTRcvSuplNo}='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTRcvSuplNo_lbl.Text)
            FTRcvSuplNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTRcvSuplBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function


#Region " Proc "

    Private Function SaveBarcode(Key As String) As Boolean

        Dim _Str As String = ""
        Dim _BarCode As String = Key
        Dim _StateNew As Boolean = False

        Try

            _Str = " SELECT TOP 1 FTBarcodeSendSuplNo  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode WITH(NOLOCK) WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "")

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StateNew Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode(FTInsUser, FDInsDate, FTInsTime, FTRcvSuplNo, FTBarcodeSendSuplNo)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            Else

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode "
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & "  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                _Str &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If

            If Me.FNRcvSuplType.SelectedIndex = 1 Then
                Dim _oDt As DataTable
                With DirectCast(Me.ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    _oDt = .Copy
                End With

                For Each R As DataRow In _oDt.Select("FTBarcodeSendSuplNo='" & _BarCode & "'")

                    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                    _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Str &= vbCrLf & "  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                    _Str &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                        _Str &= "(FTInsUser, FDInsDate, FTInsTime, FTRcvSuplNo, FTBarcodeSendSuplNo   "
                        _Str &= "  )"
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "'"
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If
                Next

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

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "

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

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            If Me.FTRcvSuplNo.Text <> "" And Me.FTStateAccept.Checked = False Then
                Call Approved(FTRcvSuplNo.Text, "1")
                Call LoadStateApp(Me.FTRcvSuplNo.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Try
            If Me.FTRcvSuplNo.Text <> "" Then
                Call Approved(FTRcvSuplNo.Text, "0")
                Call LoadStateApp(Me.FTRcvSuplNo.Text)
                Call LoadDucumentDetail(FTRcvSuplNo.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function Approved(_Key As String, _StateApp As String) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _FTMailId As Long
            Dim _MailToUser As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาแสกนบาร์โค้ดมัดงานเพื่อทำการ อนุมัติรับ !!!!!", 1703021127, Me.Text, "", MessageBoxIcon.Hand)
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False

                End If

                For Each R As DataRow In .Select("FTSelect = '1'")

                    _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                    _Cmd &= vbCrLf & "Set FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ",FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTStateBranchAccept='" & _StateApp & "'"
                    If _StateApp = "1" Then
                        _Cmd &= vbCrLf & ",FNBalQuantity =" & R!FNBalQuantity.ToString
                    Else
                        _Cmd &= vbCrLf & ",FNBalQuantity =0"
                        Me.ockselectall.Checked = False
                    End If
                    _Cmd &= vbCrLf & "Where FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Cmd &= vbCrLf & "and FTBarcodeSendSuplNo='" & R!FTBarcodeSendSuplNo.ToString & "'"

                    HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                Next

            End With

            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch "
            If _StateApp = "1" Then
                _Cmd &= vbCrLf & "Set FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FTStateBranchAccept='1'"
                _Cmd &= vbCrLf & ",FTStateBranchCancelAccept='0'"
            Else
                _Cmd &= vbCrLf & "Set FTStateBranchCancelAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FTStateBranchCancelAcceptDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTStateBranchCancelAcceptTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FTStateBranchCancelAccept='1'"
                _Cmd &= vbCrLf & ",FTStateBranchAccept='0'"
                _Cmd &= vbCrLf & ",FTStateBranchAcceptDate=''"
                _Cmd &= vbCrLf & ",FTStateBranchAcceptTime=''"
                _Cmd &= vbCrLf & ", FTStateBranchAcceptBy=''"
            End If
            _Cmd &= vbCrLf & "Where FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _MailToUser = "Select Top 1  FTRcvSuplBy FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"

            If _StateApp = "1" Then
                _Cmd = "Select Top 1 FTRcvSuplNo From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl With(nolock) where FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                    _Cmd = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTRcvSuplNo, FDRcvSuplDate, FTRcvSuplBy, FNHSysSuplId, FNSendSuplState, FTRemark, FNHSysCmpId, FTInvoiceNo, FTInvoiceDate, FNRcvSuplType)"
                    _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTRcvSuplNo"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FNHSysSuplId"
                    _Cmd &= vbCrLf & ", FNSendSuplState"
                    _Cmd &= vbCrLf & ", FTRemark"
                    _Cmd &= vbCrLf & ", FNHSysCmpId"
                    _Cmd &= vbCrLf & ", FTInvoiceNo"
                    _Cmd &= vbCrLf & ", FTInvoiceDate"
                    _Cmd &= vbCrLf & ", FNRcvSuplType"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch "
                    _Cmd &= vbCrLf & " WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If



                    _Cmd = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTRcvSuplNo, FTBarcodeSendSuplNo )"
                    _Cmd &= vbCrLf & "SELECT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FTRcvSuplNo"
                    _Cmd &= vbCrLf & ",FTBarcodeSendSuplNo"
                    _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                    _Cmd &= vbCrLf & " WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            Else
                ' เด๋วต้องแก้ชุดนี้ ไม่ต้องทำการลบ ข้อมุลชุดนี้ by  joker
                '_Cmd = " Delete From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl"
                '_Cmd &= vbCrLf & " WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                'HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                '_Cmd = " Delete From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode"
                '_Cmd &= vbCrLf & " WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                'HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            If _StateApp = "1" Then
                _FTMailId = HI.TL.RunID.GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_MailToUser) & "'"
                _Cmd &= ",'Branch Approved Receive Send Supplier ','Branch Approved Receive Send Supplier Successfully   " & vbCr & "RcvSuplNo :" & HI.UL.ULF.rpQuoted(_Key.ToString) & "' ,0,1,0,0,0,0,"
                _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MAIL)

            Else
                _FTMailId = HI.TL.RunID.GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)

                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_MailToUser) & "'"
                _Cmd &= ",'Branch Reject Receive Send Supplier ','Branch Approved Reject Send Supplier Pls Check...   " & vbCr & "RcvSuplNo :" & HI.UL.ULF.rpQuoted(_Key.ToString) & "' ,0,1,0,0,0,0,"
                _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MAIL)

            End If

            'If (_StateApp = "0") Then
            '    Me.FTStateAccept.Checked = False
            '    Me.FTApproveBy.Text = ""
            '    Me.FTApproveTime.Text = ""
            '    Me.FDApproveDate.Text = ""
            'End If


            'If (_StateApp = "1") Then
            '    adjActualQty()
            'End If

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function


    Private Function CheckApprove() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "  SELECT Top 1 Isnull(FTStateBranchAccept,'') AS FTStateBranchAccept  "
            _Cmd &= vbCrLf & "FROM            TPRODTReceiveSuplToBranch WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTRcvSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "1"
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        Me.FTApproveTime.Time = Nothing
    End Sub

    Private _PBalQty As Integer
    Private _Edit As Boolean = False
    Private Sub RepositoryFNBalQuantity_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryFNBalQuantity.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            Try
                'If CheckApprove() Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลได้ เนื่องจากมีการอนุมัติรับไปแล้ว !!!!", 1604291134, Me.Text, "", MessageBoxIcon.Stop)
                '    Exit Sub
                'End If
                If CheckOwner() = False Then Exit Sub

                With ogvdetail
                    If .RowCount <= 0 Then Exit Sub
                    Dim i As Integer = .FocusedRowHandle
                    If i < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                    Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeSendSuplNo").ToString
                    Dim _BalQty As Integer = _PBalQty

                    Dim _Cmd As String = ""

                    If _BalQty > 0 And _Edit = True Then
                        .SetRowCellValue(.FocusedRowHandle, "FTSelect", "1")
                        _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode "
                        _Cmd &= vbCrLf & " SET FNBalQuantity=" & _BalQty
                        _Cmd &= vbCrLf & "  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                        _Cmd &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_Barcode) & "' "

                        '_Cmd &= vbCrLf & "  UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                        '_Cmd &= vbCrLf & " SET FNBalQuantity=" & _BalQty
                        '_Cmd &= vbCrLf & "  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                        '_Cmd &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(_Barcode) & "' "
                        _Cmd = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                        _Cmd &= vbCrLf & " SET FTStateBranchAccept='1'"
                        _Cmd &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Cmd &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Cmd &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Cmd &= vbCrLf & " , FNBalQuantity=" & _BalQty
                        _Cmd &= vbCrLf & " WHERE    FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
                        _Cmd &= vbCrLf & "and FTBarcodeSendSuplNo = '" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeSendSuplNo").ToString & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                        _Edit = False
                    End If
                    .FocusedRowHandle = .FocusedRowHandle + 1
                    .FocusedColumn = .VisibleColumns(11)
                    .UnselectRow(.FocusedRowHandle - 1)
                    .SelectRow(.FocusedRowHandle)
                End With
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub RepositoryFNBalQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFNBalQuantity.EditValueChanging
        Try
            With Me.ogvdetail
                If e.NewValue <= .GetRowCellValue(.FocusedRowHandle, "FNQuantity") Then
                    _PBalQty = e.NewValue
                    _Edit = True
                Else
                    MG.ShowMsg.mInfo("ใส่เกินจำนวนที่กำหนด กรุณาใส่ใหม่อีกครั้ง !", 1704201628, Me.Text)
                    e.Cancel = True
                    _Edit = True
                End If
                '    _Strsql = "UPDate  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                '    _Strsql &= vbCrLf & " SET FTStateBranchAccept='1'"
                '    _Strsql &= vbCrLf & " ,FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                '    _Strsql &= vbCrLf & " ,FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB & " "
                '    _Strsql &= vbCrLf & " ,FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB & " "
                '    _Strsql &= vbCrLf & " , FNBalQuantity=" & e.NewValue.ToString.Replace(".", "")
                '    _Strsql &= vbCrLf & " WHERE    FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "' -- AND ISNULL(FTStateBranchAccept,'0')<>'1' "
                '    _Strsql &= vbCrLf & "and FTBarcodeSendSuplNo = '" & .GetRowCellValue(.FocusedRowHandle, "FTBarcodeSendSuplNo").ToString & "'"
                'HI.Conn.SQLConn.ExecuteOnly(_Strsql, Conn.DB.DataBaseName.DB_PROD)
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Function adjActualQty() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy

            End With
            For Each R As DataRow In _oDt.Rows
                _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSupl_Barcode "
                _Cmd &= vbCrLf & " SET FNBalQuantity=" & Double.Parse("0" & R!FNBalQuantity.ToString)
                _Cmd &= vbCrLf & "  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                _Cmd &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "' "

                _Cmd &= vbCrLf & "  UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                _Cmd &= vbCrLf & " SET FNBalQuantity=" & Double.Parse("0" & R!FNBalQuantity.ToString)
                _Cmd &= vbCrLf & "  WHERE FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(FTRcvSuplNo.Text) & "' "
                _Cmd &= vbCrLf & "  AND FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "' "
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                LoadBarcodeInfo(Me.FTBarcodeNo.Text)
                DefalutScanChecked()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadBarcodeInfo(Key As String)

        Try
            Dim _Qry As String = ""
            Dim dt As DataTable
            Dim _StatePass As Boolean = False

            Me.FTRcvSuplNo.Text = HI.Conn.SQLConn.GetField("Select Top 1 FTRcvSuplNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode  where FTBarcodeSendSuplNo='" & Key.ToString & "'", Conn.DB.DataBaseName.DB_PROD, "")
            'LoadDucumentDetail(Me.FTRcvSuplNo.Text)



            _Qry = " Select  Top 1   A.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & "	, A.FNHSysPartId"
            _Qry &= vbCrLf & "	, A.FNSendSuplType"
            _Qry &= vbCrLf & "	, A.FNHSysSuplId"
            _Qry &= vbCrLf & "	, A.FTBarcodeBundleNo "
            _Qry &= vbCrLf & "	, A.FTOrderNo "
            _Qry &= vbCrLf & "	, A.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, A.FTSendSuplRef"
            _Qry &= vbCrLf & "  , A.FNHSysCmpId"
            _Qry &= vbCrLf & "	, S.FTSuplCode"
            _Qry &= vbCrLf & "	, A.FTSendSuplNo"
            _Qry &= vbCrLf & "  , A.FNBunbleSeq"
            _Qry &= vbCrLf & "  , A.FTColorway"
            _Qry &= vbCrLf & "  , A.FTSizeBreakDown"
            _Qry &= vbCrLf & "  , A.FNQuantity"
            _Qry &= vbCrLf & "	, ST.FTStyleCode"
            _Qry &= vbCrLf & "	,MP.FTPartCode "
            _Qry &= vbCrLf & "	,A.FNHSysOperationId"
            _Qry &= vbCrLf & "  ,A.FNSeq"
            _Qry &= vbCrLf & "  ,A.FNHSysOperationIdTo"
            _Qry &= vbCrLf & "	,Mpp.FTOperationCode"
            _Qry &= vbCrLf & " , A.FNQuantity AS FNBalQuantity"   ' Case when RCVB.FNBalQuantity is not null then  Isnull(RCVB.FNBalQuantity,0) 

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(("
            _Qry &= vbCrLf & "   SELECT        TOP 1  "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  C.FTMarkNameTH AS FTMarkName  "
            Else
                _Qry &= vbCrLf & "  C.FTMarkNameEN AS FTMarkName  "
            End If

            _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA  WITH (NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B  WITH (NOLOCK) ON AA.FTLayCutNo = B.FTLayCutNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C  WITH (NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
            _Qry &= vbCrLf & "   WHERE        (AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
            _Qry &= vbCrLf & " ),'') AS FNHSysMarkId"

            _Qry &= vbCrLf & "	, RCVB.FTRcvSuplNo"

            _Qry &= vbCrLf & "	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSuplToBranch_Barcode AS A WITH (NOLOCK)  "
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON A.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON A.FTOrderNo = O.FTOrderNo "
            _Qry &= vbCrLf & "	INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart  AS MP WITH (NOLOCK) ON A.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  A.FNHSysOperationId  = MPP.FNHSysOperationId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode AS RCVB  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = RCVB.FTBarcodeSendSuplNo "
            _Qry &= vbCrLf & " where RCVB.FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "' "
            _Qry &= vbCrLf & " and  A.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' "
            _Qry &= vbCrLf & "  ORDER BY A.FTBarcodeSendSuplNo "


            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)


            'With DirectCast(Me.ogcdetail.DataSource, DataTable)
            '    .AcceptChanges()
            '    ' _oDt = .Copy
            '    If .Select("FTBarcodeSendSuplNo = '" & HI.UL.ULF.rpQuoted(Key) & "' ").Length <= 0 Then
            '        For Each R As DataRow In dt.Rows
            '            .ImportRow(R)
            '        Next
            '        Me.olberror.Text = HI.MG.ShowMsg.GetMessage("แสกนรับเรียบร้อยแล้ว Barcode !!!", 1702161205)
            '    Else
            '        olberror.Text = HI.MG.ShowMsg.GetMessage("มีการแสกนรับไปแล้ว Barcode !!!", 1702161206)
            '    End If
            '    .AcceptChanges()
            'End With

            'olberror.ForeColor = Color.Red

            'If dt.Rows.Count > 0 Then

            '    For Each R As DataRow In dt.Rows

            '        FTStyleCode.Text = R!FTStyleCode.ToString
            '        FTOrderNo.Text = R!FTOrderNo.ToString
            '        FTOrderProdNo.Text = R!FTOrderProdNo.ToString
            '        FTColorway.Text = R!FTColorway.ToString
            '        FTSizeBreakDown.Text = R!FTSizeBreakDown.ToString
            '        FNHSysOperationId.Text = R!FTOperationName.ToString
            '        FNHSysMarkId.Text = R!FNHSysMarkId.ToString
            '        FNHSysPartId.Text = R!FTPartName.ToString
            '        FTBarcodeBundleNo.Text = R!FNBunbleSeq.ToString
            '        FTSendSuplNo.Text = R!FTSendSuplNo.ToString

            '        If Me.FNHSysSuplId.Text = "" Then
            '            Me.FNHSysSuplId.Text = R!FTSuplCode.ToString
            '        End If

            '        Exit For

            '    Next

            '    If dt.Select("FTSuplCode='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "'").Length > 0 Then
            '        If dt.Rows(0) !FTRcvSuplNo.ToString = "" Or dt.Rows(0) !FTRcvSuplNo.ToString = Me.FTRcvSuplNo.Text Then
            '            If Integer.Parse(Val(dt.Rows(0) !FNHSysOperationIdTo.ToString)) > 0 Then

            '                Dim dtcheck As DataTable
            '                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_CheckOperationBefore '" & HI.UL.ULF.rpQuoted(FTOrderProdNo.Text) & "'," & Integer.Parse(Val(dt.Rows(0) !FNHSysOperationIdTo.ToString)) & ",'" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'"
            '                dtcheck = (HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD))

            '                If dtcheck.Rows.Count > 0 Then

            '                    If Val(dtcheck.Rows(0) !FNQuantity.ToString) >= Val(dt.Rows(0) !FNQuantity.ToString) Then
            '                        _StatePass = True
            '                        olberror.ForeColor = Drawing.Color.Green

            '                    Else
            '                        olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่สามารถ Scan เกินยอดขั้นตอนก่อนหน้าได้ !!!", 1407310014) & "   " & Format((Val(dtcheck.Rows(0) !FNQuantity.ToString)), "#,#0")
            '                    End If

            '                Else
            '                    Dim _OperationName As String = ""
            '                    With New PROD
            '                        _OperationName = .GetOpertionName(Integer.Parse(Val(dt.Rows(0) !FNHSysOperationIdTo.ToString)))
            '                    End With

            '                    olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูลขั้นตอนก่อนหน้า หรือยังไม่ได้ทำการ Scan กรุณาทำการตรวจสอบ !!!", 1407311102) & " ( " & _OperationName & " )"

            '                    'olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูลขั้นตอนก่อนหน้า หรือยังไม่ได้ทำการ Scan กรุณาทำการตรวจสอบ !!!", 1407310012)
            '                End If

            '                dtcheck.Dispose()
            '            Else
            '                _StatePass = True
            '                olberror.ForeColor = Drawing.Color.Green
            '            End If
            '        Else
            '            olberror.Text = HI.MG.ShowMsg.GetMessage("Barcode ถูก Scan รับ Supplier ด้วยกายเลขอื่นไปแล้ว", 1407310011) & " (" & dt.Rows(0) !FTRcvSuplNo.ToString & ")"
            '        End If
            '    Else
            '        olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่ใช่ Barcode ของ Supplier นี้ !!!", 1407310016)
            '    End If
            'Else
            '    olberror.Text = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูล Barcode Scan ส่ง Supplier !!!", 1407310013)
            'End If

            'dt.Dispose()

            'If (_StatePass) Then

            '    If Me.FTRcvSuplNo.Text = "" Then
            '        Try
            '            If FTRcvSuplNo.Properties.Buttons.Count > 1 Then
            '                If (FTRcvSuplNo.Properties.Buttons.Item(1).Visible) Then
            '                    FTRcvSuplNo.PerformClick(FTRcvSuplNo.Properties.Buttons.Item(1))

            '                    If Me.VerrifyData Then
            '                        If Me.SaveData Then

            '                        Else
            '                            Exit Sub
            '                        End If
            '                    Else
            '                        Exit Sub
            '                    End If
            '                End If
            '            End If
            '        Catch ex222 As Exception
            '        End Try
            '    Else
            '        If FTRcvSuplNo.Properties.Tag.ToString = "" Then
            '            If Me.VerrifyData() Then
            '                If Me.SaveData Then
            '                Else
            '                    Exit Sub
            '                End If
            '            Else
            '                Exit Sub
            '            End If
            '        Else
            '            If Me.FTRcvSuplNo.Text = "" Then Exit Sub
            '        End If
            '    End If



            '    Call LoadDataInfo(Me.FTRcvSuplNo.Text)
            '    Call SaveBarcode(Key)
            '    Call LoadDucumentDetail(Me.FTRcvSuplNo.Text)

            '    olberror.Text = HI.MG.ShowMsg.GetMessage("Scan Complete", 1407310015)
            'End If

            Dim _Cmd As String = ""
            If Me.FTStateAccept.Checked Then
                With DirectCast(Me.ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    If .Rows.Count <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาแสกนบาร์โค้ดมัดงานเพื่อทำการ อนุมัติรับ !!!!!", 1703021127, Me.Text, "", MessageBoxIcon.Hand)
                        Exit Sub

                    End If

                    For Each R As DataRow In .Rows

                        _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTReceiveSuplToBranch_Barcode "
                        _Cmd &= vbCrLf & "Set FTStateBranchAcceptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & ",FTStateBranchAcceptDate=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTStateBranchAcceptTime=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FTStateBranchAccept='1'"
                        _Cmd &= vbCrLf & "Where FTRcvSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTRcvSuplNo.Text) & "'"
                        _Cmd &= vbCrLf & "and FTBarcodeSendSuplNo='" & R!FTBarcodeSendSuplNo.ToString & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                    Next

                End With
            End If


            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DefalutScanChecked()
        Try
            With ogvdetail
                For i As Integer = 0 To .RowCount - 1
                    If .GetRowCellValue(i, "FTBarcodeSendSuplNo").ToString = Me.FTBarcodeNo.Text.Trim Then
                        .SetRowCellValue(i, "FTSelect", "1")
                        .SetRowCellValue(i, "FNBalQuantity", .GetRowCellValue(i, "FNQuantity"))
                        CType(ogcdetail.DataSource, DataTable).AcceptChanges()
                    End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As String = View.GetRowCellValue(e.RowHandle, View.Columns("FTSelect"))
                If category = "1" Then
                    e.Appearance.BackColor = Color.LightGreen
                    '  e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ockselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectall.CheckedChanged
        Try
            Dim Row As Integer = 0
            LoadDucumentDetail(Me.FTRcvSuplNo.Text)
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    If Me.ockselectall.Checked Then
                        If ogvdetail.GetRowCellValue(Row, "FTSelect").ToString <> "1" Then
                            R!FTSelect = "1"
                            ogvdetail.SetRowCellValue(Row, "FNBalQuantity", ogvdetail.GetRowCellValue(Row, "FNQuantity"))
                        End If
                    Else
                        R!FTSelect = "0"
                        ogvdetail.SetRowCellValue(Row, "FNBalQuantity", 0)
                        LoadDucumentDetail(Me.FTRcvSuplNo.Text)
                    End If
                    Row += 1
                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemSelect.EditValueChanging
        Try
            Dim _Qtys As Double = 0
            Dim _Barcode As String = ""
            With ogvdetail
                _Barcode = .GetRowCellValue(.FocusedRowHandle, "FTBarcodeSendSuplNo")
                If e.NewValue = "1" Then
                    _Qtys = .GetRowCellValue(.FocusedRowHandle, "FNQuantity")
                Else
                    _Qtys = .GetRowCellValue(.FocusedRowHandle, "FNBalOrign")
                End If
            End With
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTBarcodeSendSuplNo='" & _Barcode & "'")
                    R!FNBalQuantity = _Qtys
                    Exit For
                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub
End Class