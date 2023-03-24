Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls

Public Class wQCFabricInspec

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddRawmatPopup As wQCFabricInspecAddRawmat
    Private _AddBarcodePopup As wQCFabricInspecAddBarcode
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        Dim oSysLang As New ST.SysLanguage
        _AddRawmatPopup = New wQCFabricInspecAddRawmat
        HI.TL.HandlerControl.AddHandlerObj(_AddRawmatPopup)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddRawmatPopup.Name.ToString.Trim, _AddRawmatPopup)
        Catch ex As Exception
        Finally
        End Try

        _AddBarcodePopup = New wQCFabricInspecAddBarcode
        HI.TL.HandlerControl.AddHandlerObj(_AddBarcodePopup)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddBarcodePopup.Name.ToString.Trim, _AddBarcodePopup)
        Catch ex As Exception
        Finally
        End Try

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

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTQCFabBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTQCFabBy.Text) & "' "
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

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub
        If VerrifyData() Then
            If SaveData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

        If CheckOwner() = False Then Exit Sub
        If Me.FTQCFabNo.Text <> "" Then
            Dim _Qry As String = ""

            _Qry = "SELECT TOP 1 BO.FTBarcodeNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTIssueNo = BO.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo, QD.FTRollNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
            _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Qry &= vbCrLf & "  ) AS Q"
            _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Qry &= vbCrLf & "    AND  B.FTRollNo=Q.FTRollNo"
            _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "  SELECT TOP 1 BO.FTBarcodeNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENSaleAndTerminate AS H WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTSaleAndTerminateNo = BO.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo, QD.FTRollNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
            _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Qry &= vbCrLf & "  ) AS Q"
            _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Qry &= vbCrLf & "    AND  B.FTRollNo=Q.FTRollNo"
            _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "  SELECT TOP 1 BO.FTBarcodeNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTReturnSuplNo = BO.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo, QD.FTRollNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
            _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Qry &= vbCrLf & "  ) AS Q"
            _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Qry &= vbCrLf & "    AND  B.FTRollNo=Q.FTRollNo"
            _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                HI.MG.ShowMsg.mInfo("ม้วนผ้ามีการเดิน Transaction แล้วไม่สามารถทำการลบได้ !!!", 1502250019, Me.Text, , MessageBoxIcon.Warning)
            Else

                If DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub LoadPOInfo(PoKey As String, Optional LoadRcv As Boolean = False)
        Dim _Str As String = ""
        Dim Dt As DataTable

        _Str = " SELECT  H.FTPurchaseNo, H.FNExchangeRate, S.FTSuplCode, C.FTCurCode"
        _Str &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH (NOLOCK) ON H.FNHSysSuplId = S.FNHSysSuplId INNER JOIN"
        _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH (NOLOCK) ON H.FNHSysCurId = C.FNHSysCurId"
        _Str &= vbCrLf & "  WHERE H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoKey) & "' "
        Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PUR)

        If Dt.Rows.Count > 0 Then
            For Each R As DataRow In Dt.Rows
                FNHSysSuplId.Text = R!FTSuplCode.ToString
                Exit For
            Next
        Else
            FNHSysSuplId.Text = ""
        End If
    End Sub


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

    Private Sub LoadDataDetail(ByVal _DockKey As String)
        Dim _Str As String = ""
        Dim _dt As DataTable

        _Str = " Select '' AS FTFactory,B.FTBatchNo AS FTDyeLotNo,BI.FTDocumentNo AS FTReceiveNo"
        _Str &= vbCrLf & "  , IM.FNHSysRawMatId"
        _Str &= vbCrLf & " , IM.FTRawMatCode"
        _Str &= vbCrLf & "  , MM.FTCusItemCodeRef"
        _Str &= vbCrLf & " , S.FTSuplCode"
        _Str &= vbCrLf & "  ,IMC.FTRawMatColorCode"
        _Str &= vbCrLf & "  ,IMS.FTRawMatSizeCode"
        _Str &= vbCrLf & " , U.FTUnitCode"
        _Str &= vbCrLf & "  , B.FTBatchNo"
        _Str &= vbCrLf & "  , SUM(BI.FNQuantity) AS FNTotalRcvQty"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & "  , IM.FTRawMatNameTH AS FTRawMatName"
            _Str &= vbCrLf & "  , S.FTSuplNameTH AS FTSuplName"
            _Str &= vbCrLf & "  ,CASE WHEN ISNULL(POCO.FTRawMatColorNameTH,'') ='' THEN  IM.FTRawMatColorNameTH ELSE ISNULL(POCO.FTRawMatColorNameTH,'') END AS FTRawMatColorName"

        Else

            _Str &= vbCrLf & " , IM.FTRawMatNameEN AS FTRawMatName"
            _Str &= vbCrLf & "  , S.FTSuplNameEN AS FTSuplName"
            _Str &= vbCrLf & "  ,CASE WHEN ISNULL(POCO.FTRawMatColorNameEN,'') ='' THEN  IM.FTRawMatColorNameEN ELSE ISNULL(POCO.FTRawMatColorNameEN,'') END AS FTRawMatColorName"

        End If

        _Str &= vbCrLf & " ,ISNULL(("
        _Str &= vbCrLf & "     SELECT TOP 1 '1' AS FTStateQC "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS HD WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat AS DT WITH(NOLOCK) ON HD.FTQCFabNo = DT.FTQCFabNo"
        _Str &= vbCrLf & " WHERE  (HD.FTQCFabNo <> N'" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "') "
        _Str &= vbCrLf & "   AND (HD.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "') "
        _Str &= vbCrLf & "   AND (DT.FNHSysRawMatId = IM.FNHSysRawMatId)"
        _Str &= vbCrLf & "   AND (DT.FTBatchNo =B.FTBatchNo)"
        _Str &= vbCrLf & "  ),'0') AS FTStateQC"

        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK)  ON B.FTBarcodeNo = BI.FTBarcodeNo INNER JOIN"
        ' _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS H WITH(NOLOCK)  ON BI.FTDocumentNo = H.FTReceiveNo INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK)  ON B.FTPurchaseNo = P.FTPurchaseNo INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK)  ON IM.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)   ON MM.FNHSysSuplId = S.FNHSysSuplId AND P.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)   ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)   ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_Purchase_RawMat_Color AS POCO ON BI.FTOrderNo = POCO.FTOrderNo AND B.FTPurchaseNo = POCO.FTPurchaseNo AND B.FNHSysRawMatId = POCO.FNHSysRawMatId"

        _Str &= vbCrLf & "   INNER JOIN (  SELECT FNHSysRawMatId,FTBatchNo "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS HD WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat AS DT WITH(NOLOCK) ON HD.FTQCFabNo = DT.FTQCFabNo"
        _Str &= vbCrLf & " WHERE  (HD.FTQCFabNo = N'" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "') "
        _Str &= vbCrLf & "   AND (HD.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "') "
        _Str &= vbCrLf & "  ) AS QC  ON IM.FNHSysRawMatId = QC.FNHSysRawMatId AND B.FTBatchNo=QC.FTBatchNo "

        _Str &= vbCrLf & "  WHERE BI.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
        _Str &= vbCrLf & "        AND (MM.FNMerMatType = 0)"
        _Str &= vbCrLf & "  GROUP BY BI.FTDocumentNo, IM.FNHSysRawMatId, IM.FTRawMatCode,  MM.FTCusItemCodeRef, S.FTSuplCode, IMC.FTRawMatColorCode, "
        _Str &= vbCrLf & "  U.FTUnitCode, B.FTBatchNo"
        _Str &= vbCrLf & "  ,IMS.FTRawMatSizeCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ", IM.FTRawMatNameTH "
            _Str &= vbCrLf & ", S.FTSuplNameTH "
            _Str &= vbCrLf & ",CASE WHEN ISNULL(POCO.FTRawMatColorNameTH,'') ='' THEN  IM.FTRawMatColorNameTH ELSE ISNULL(POCO.FTRawMatColorNameTH,'') END "
        Else
            _Str &= vbCrLf & ", IM.FTRawMatNameEN "
            _Str &= vbCrLf & ", S.FTSuplNameEN "
            _Str &= vbCrLf & ",CASE WHEN ISNULL(POCO.FTRawMatColorNameEN,'') ='' THEN  IM.FTRawMatColorNameEN ELSE ISNULL(POCO.FTRawMatColorNameEN,'') END "
        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        ogcrawmat.DataSource = _dt

        Dim _dtdata As DataTable
        _Str = " SELECT TOP 1 FTQCFabNo, FNHSysRawMatId, FTBatchNo, FTStateCutable, FTStateColorMatch, FTStateHandfeel, FTStateShading,FTShades"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat AS A WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE  (FTQCFabNo = N'" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "') "
        _dtdata = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        If _dtdata.Rows.Count > 0 Then

            For Each R As DataRow In _dtdata.Rows

                FTStateCutable.Checked = (R!FTStateCutable.ToString = "1")
                FTStateColorMatch.Checked = (R!FTStateColorMatch.ToString = "1")
                FTStateHandfeel.Checked = (R!FTStateHandfeel.ToString = "1")
                FTStateShading.Checked = (R!FTStateShading.ToString = "1")
                FTShades.Text = R!FTShades.ToString

            Next

        Else

            FTStateCutable.Checked = False
            FTStateColorMatch.Checked = False
            FTStateHandfeel.Checked = False
            FTStateShading.Checked = False
            FTShades.Text = ""

        End If
        _dtdata.Dispose()
    End Sub

    Private Sub LoadDataDetailBarcode(ByVal _DockKey As String)

        Dim _Str As String = ""
        Dim _dt As DataTable

        _Str = "   SELECT   FNHSysRawMatId, FTBatchNo, FTRollNo, FNQuantity, FNActQuantity"
        _Str &= vbCrLf & "	, FNYarn, FNContruction, FNDyeing, FNFinishing, FNCleanliness"
        _Str &= vbCrLf & "	, (FNYarn+ FNContruction+ FNDyeing+ FNFinishing+FNCleanliness) AS FNTotalDefect"
        _Str &= vbCrLf & "	, FTStateReject,FTFabricFrontSize,FTActFabricFrontSize,FTShades"
        _Str &= vbCrLf & "	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS D WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE  (FTQCFabNo = N'" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "') "
        _Str &= vbCrLf & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)


        Dim _FNTotalTicketed As Double = 0
        Dim _FNTotalActual As Double = 0
        Dim _FNTotalPoints As Integer = 0

        For Each R As DataRow In _dt.Rows
            _FNTotalTicketed = _FNTotalTicketed + Val(R!FNQuantity.ToString)
            _FNTotalActual = _FNTotalActual + Val(R!FNActQuantity.ToString)
            _FNTotalPoints = _FNTotalPoints + Val(R!FNTotalDefect.ToString)
        Next

        FNTotalTicketed.Value = _FNTotalTicketed
        FNTotalActual.Value = _FNTotalActual
        FNTotalPoints.Value = _FNTotalPoints
        FNTotalDefects.Value = _FNTotalPoints

        ogcDetail.DataSource = _dt

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

    End Sub

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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
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

            If Not (ogcrawmat.DataSource Is Nothing) Then
                Dim _dtd As DataTable
                With CType(ogcrawmat.DataSource, DataTable)
                    .AcceptChanges()
                    _dtd = .Copy
                End With

                If _dtd.Rows.Count > 0 Then
                    If SaveDetail(_Key) = False Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            End If

            If Not (ogcDetail.DataSource Is Nothing) Then
                Dim _dtb As DataTable
                With CType(ogcDetail.DataSource, DataTable)
                    .AcceptChanges()
                    _dtb = .Copy
                End With

                If _dtb.Rows.Count > 0 Then
                    If SaveDetailBarcode(_Key) = False Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select

            Next

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function SaveDetail(ByVal _Key As String)
        Try
            Dim _Qry As String = ""
            Dim _dtd As DataTable
            With CType(ogcrawmat.DataSource, DataTable)
                .AcceptChanges()
                _dtd = .Copy
            End With

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat"
            _Qry &= vbCrLf & " WHERE  FTQCFabNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In _dtd.Rows

                _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat "
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTQCFabNo, FNHSysRawMatId, FTBatchNo"
                _Qry &= vbCrLf & ", FTStateCutable, FTStateColorMatch, FTStateHandfeel, FTStateShading"
                _Qry &= vbCrLf & ", FNTotalTicketed,FNTotalActual, FNDifference, FNTotalPoints, FNTotalDefects, FNTotalPointsPerUOM,FTShades)"

                _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString())) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBatchNo.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTStateCutable.EditValue.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTStateColorMatch.EditValue.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTStateHandfeel.EditValue.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTStateShading.EditValue.ToString) & "' "
                _Qry &= vbCrLf & "," & Double.Parse(FNTotalTicketed.Value) & ""
                _Qry &= vbCrLf & "," & Double.Parse(FNTotalActual.Value) & ""
                _Qry &= vbCrLf & "," & Double.Parse(FNDifference.Value) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(FNTotalPoints.Value) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(FNTotalDefects.Value) & ""
                _Qry &= vbCrLf & "," & Double.Parse(FNTotalPointsPerUOM.Value) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTShades.Text.Trim) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If

                Exit For
            Next

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function SaveDetailBarcode(ByVal _Key As String)
        Try
            Dim _Qry As String = ""
            Dim _dtd As DataTable
            With CType(ogcDetail.DataSource, DataTable)
                .AcceptChanges()
                _dtd = .Copy
            End With

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail"
            _Qry &= vbCrLf & " WHERE  FTQCFabNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In _dtd.Rows

                _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail "
                _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FTQCFabNo, FNHSysRawMatId, FTBatchNo"
                _Qry &= vbCrLf & ", FTRollNo, FNQuantity, FNActQuantity, FNYarn, FNContruction, FNDyeing"
                _Qry &= vbCrLf & ", FNFinishing, FNCleanliness, FTStateReject,FTFabricFrontSize,FTActFabricFrontSize,FTShades)"
                _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysRawMatId.ToString())) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBatchNo.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRollNo.ToString) & "' "
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNQuantity.ToString())) & ""
                _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNActQuantity.ToString())) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNYarn.ToString())) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNContruction.ToString())) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNDyeing.ToString())) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNFinishing.ToString())) & ""
                _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNCleanliness.ToString())) & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStateReject.ToString()) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString()) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTActFabricFrontSize.ToString()) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTShades.ToString()) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    Return False
                End If

            Next
            _dtd.Dispose()

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String = Me.Query & "  WHERE  " & Me.MainKey & "='" & Key.ToString & "' "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

        Dim _FieldName As String = ""
        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString
                If Me.MainKey.ToUpper <> _FieldName.ToUpper Then
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
                End If

            Next

            Exit For
        Next

        Dim _Qry As String = ""
        Dim _dttmp As DataTable
        FTUserState.Text = ""
        FTUserStateDate.Text = ""

        '_Qry = " SELECT TOP 1 CASE WHEN ISNULL(FTRejectUser,'')='' THEN ISNULL(FTAcceptUser,'') ELSE ISNULL(FTRejectUser,'') END AS  FTUserState "
        _Qry = " SELECT TOP 1  ISNULL(FTAcceptUser,'') AS  FTUserState "
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(FDAcceptDate) = 1 THEN Convert(nvarchar(10),Convert(Datetime,FDAcceptDate),103) + '  ' +  FTAppTime  ELSE '' END AS  FDAcceptDate "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS X WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'"


        _dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In _dttmp.Rows
            FTUserState.Text = R!FTUserState.ToString
            FTUserStateDate.Text = R!FDAcceptDate.ToString
            Exit For
        Next

        Call LoadDataDetail(Key)
        Call LoadDataDetailBarcode(Key)

        _ProcLoad = False
    End Sub

    Private Function VerrifyData() As Boolean

        If Me.FTQCFabNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTQCFabNo_lbl.Text)
            FTQCFabNo.Focus()
            Return False
        End If

        If Me.FTPurchaseNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
            Return False
        End If

        If Me.FTReceiveNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function DeleteRoll(_FNHSysRawMatId As Integer, _FTBatchNo As String, _FTRollNo As String) As Boolean


        Dim _Str As String

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Str = "UPDATE B SET FTShades = Q.FTShades,FTStateQCAccept='0',FTStateQCReject='0',FTFabricFrontSize=CASE WHEN ISNULL(B.FTFabricFrontSizeRcv,'') <> '' THEN ISNULL(B.FTFabricFrontSizeRcv,'') ELSE B.FTFabricFrontSize END"
            _Str &= vbCrLf & " "
            _Str &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With(NOLOCK)"
            _Str &= vbCrLf & "  INNER JOIN ("
            _Str &= vbCrLf & " Select QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo,QD.FTShades,QDB.FTRollNo,QDB.FTStateReject,QDB.FTActFabricFrontSize,QDB.FTFabricFrontSize"
            _Str &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric As QH With(NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat As QD With(NOLOCK) On QH.FTQCFabNo = QD.FTQCFabNo"
            _Str &= vbCrLf & "     INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail As QDB With(NOLOCK) On QD.FTQCFabNo = QDB.FTQCFabNo  AND QD.FNHSysRawMatId=QDB.FNHSysRawMatId AND QD.FTBatchNo=QDB.FTBatchNo  "
            _Str &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Str &= vbCrLf & "  ) AS Q"
            _Str &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Str &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Str &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
            _Str &= vbCrLf & "    AND B.FTRollNo=Q.FTRollNo"


            _Str &= vbCrLf & " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail "
            _Str &= vbCrLf & " WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "' "
            _Str &= vbCrLf & "  AND FNHSysRawMatId=" & Integer.Parse(Val(_FNHSysRawMatId)) & " "
            _Str &= vbCrLf & "  AND FTBatchNo='" & HI.UL.ULF.rpQuoted(_FTBatchNo) & "' "
            _Str &= vbCrLf & "  AND FTRollNo='" & HI.UL.ULF.rpQuoted(_FTRollNo) & "' "

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

    Private Sub ProcessDeleteRoll()

        If CheckOwner() = False Then Exit Sub

        With ogvDetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _Qry As String = ""
            Dim _StateDelete As Boolean = False

            For Each i As Integer In .GetSelectedRows()
                Dim _FTRollNo As String = "" & .GetRowCellValue(i, "FTRollNo").ToString
                Dim _FTBatchNo As String = "" & .GetRowCellValue(i, "FTBatchNo").ToString
                Dim _FNHSysRawMatId As String = "" & .GetRowCellValue(i, "FNHSysRawMatId").ToString

                _Qry = "SELECT TOP 1 BO.FTBarcodeNo"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTIssueNo = BO.FTDocumentNo INNER JOIN"
                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
                _Qry &= vbCrLf & "    WHERE B.FNHSysRawMatId=" & Integer.Parse(Val(_FNHSysRawMatId)) & " "
                _Qry &= vbCrLf & "    AND B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & "    AND  B.FTRollNo='" & HI.UL.ULF.rpQuoted(_FTRollNo) & "' "
                _Qry &= vbCrLf & "    AND B.FTBatchNo='" & HI.UL.ULF.rpQuoted(_FTBatchNo) & "' "
                _Qry &= vbCrLf & " UNION "
                _Qry &= vbCrLf & "  SELECT TOP 1 BO.FTBarcodeNo"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENSaleAndTerminate AS H WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTSaleAndTerminateNo = BO.FTDocumentNo INNER JOIN"
                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
                _Qry &= vbCrLf & "    WHERE B.FNHSysRawMatId=" & Integer.Parse(Val(_FNHSysRawMatId)) & " "
                _Qry &= vbCrLf & "    AND B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & "    AND  B.FTRollNo='" & HI.UL.ULF.rpQuoted(_FTRollNo) & "' "
                _Qry &= vbCrLf & "    AND B.FTBatchNo='" & HI.UL.ULF.rpQuoted(_FTBatchNo) & "' "
                _Qry &= vbCrLf & " UNION "
                _Qry &= vbCrLf & "  SELECT TOP 1 BO.FTBarcodeNo"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH(NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTReturnSuplNo = BO.FTDocumentNo INNER JOIN"
                _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
                _Qry &= vbCrLf & "    WHERE B.FNHSysRawMatId=" & Integer.Parse(Val(_FNHSysRawMatId)) & " "
                _Qry &= vbCrLf & "    AND B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & "    AND  B.FTRollNo='" & HI.UL.ULF.rpQuoted(_FTRollNo) & "' "
                _Qry &= vbCrLf & "    AND B.FTBatchNo='" & HI.UL.ULF.rpQuoted(_FTBatchNo) & "' "

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                    HI.MG.ShowMsg.mInfo("ม้วนผ้ามีการเดิน Transaction แล้วไม่สามารถทำการลบได้ !!!", 1502250018, Me.Text, _FTRollNo, MessageBoxIcon.Warning)
                Else

                    If DeleteRoll(Integer.Parse(Val(_FNHSysRawMatId)), _FTBatchNo, _FTRollNo) Then
                        _StateDelete = True
                    Else
                        Try
                            Me.ogvDetail.DeleteRow(.FocusedRowHandle)

                            Dim _dtsource As DataTable
                            With CType(ogcDetail.DataSource, DataTable)
                                .AcceptChanges()
                                _dtsource = .Copy
                            End With

                            Dim _FNTotalTicketed As Double = 0
                            Dim _FNTotalActual As Double = 0
                            Dim _FNTotalPoints As Integer = 0

                            For Each R As DataRow In _dtsource.Rows
                                _FNTotalTicketed = _FNTotalTicketed + Val(R!FNQuantity.ToString)
                                _FNTotalActual = _FNTotalActual + Val(R!FNActQuantity.ToString)
                                _FNTotalPoints = _FNTotalPoints + Val(R!FNTotalDefect.ToString)
                            Next

                            FNTotalTicketed.Value = _FNTotalTicketed
                            FNTotalActual.Value = _FNTotalActual
                            FNTotalPoints.Value = _FNTotalPoints
                            FNTotalDefects.Value = _FNTotalPoints
                            ogcDetail.DataSource = _dtsource

                        Catch ex As Exception
                        End Try
                    End If

                End If

            Next

            If _StateDelete Then
                Call LoadDataDetailBarcode(Me.FTQCFabNo.Text)
            End If

        End With
    End Sub


#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'")


            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

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

    Private Sub wQCAcc_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvrawmat.OptionsView.ShowAutoFilterRow = False
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTQCFabNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "QCFabSlip.rpt"
                .Formular = "{TINVENQCFabric.FTQCFabNo}='" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "' "
                .Preview()
            End With
        End If
    End Sub

    Private Delegate Sub FTPurchaseNo_ValueChange(sender As System.Object, e As System.EventArgs)
    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTPurchaseNo.EditValueChanged
        If FTPurchaseNo.Text <> "" Then
            If Me.InvokeRequired Then
                Me.Invoke(New FTPurchaseNo_ValueChange(AddressOf FTPurchaseNo_EditValueChanged), New Object() {sender, e})
            Else
                Call LoadPOInfo(FTPurchaseNo.Text)
            End If

        End If
    End Sub

    Private Sub ogvrawmat_RowCountChanged(sender As Object, e As EventArgs) Handles ogvrawmat.RowCountChanged
        Try

            If ogcrawmat.DataSource Is Nothing Then
                FTPurchaseNo.Properties.ReadOnly = False
                FTPurchaseNo.Properties.Buttons(0).Enabled = True

                FTReceiveNo.Properties.ReadOnly = False
                FTReceiveNo.Properties.Buttons(0).Enabled = True
            Else
                Dim dt As DataTable
                With CType(ogcrawmat.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                FTPurchaseNo.Properties.ReadOnly = (dt.Rows.Count > 0)
                FTPurchaseNo.Properties.Buttons(0).Enabled = Not (dt.Rows.Count > 0)

                FTReceiveNo.Properties.ReadOnly = (dt.Rows.Count > 0)
                FTReceiveNo.Properties.Buttons(0).Enabled = Not (dt.Rows.Count > 0)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub

        If ogcDetail.DataSource Is Nothing Then

        Else
            Dim _dtb As DataTable
            With CType(ogcDetail.DataSource, DataTable)
                .AcceptChanges()
                _dtb = .Copy
            End With

            If _dtb.Rows.Count > 0 Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการระบุม้วนผ้าแล้ว กรุณาทำการลบม้วนผ้าก่อน !!!", 1502250017, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

        If FTQCFabNo.Properties.Tag.ToString = "" Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTQCFabNo.Text = "" Or Me.FTQCFabNo.Properties.Tag.ToString = "" Then
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

        _Str = " Select '' AS FTFactory,B.FTBatchNo AS FTDyeLotNo,BI.FTDocumentNo AS FTReceiveNo"
        _Str &= vbCrLf & "  , IM.FNHSysRawMatId"
        _Str &= vbCrLf & " , IM.FTRawMatCode"
        _Str &= vbCrLf & "  , MM.FTCusItemCodeRef"
        _Str &= vbCrLf & " , S.FTSuplCode"
        _Str &= vbCrLf & "  ,IMC.FTRawMatColorCode"
        _Str &= vbCrLf & "  ,IMS.FTRawMatSizeCode"
        _Str &= vbCrLf & " , U.FTUnitCode"
        _Str &= vbCrLf & "  , B.FTBatchNo"
        _Str &= vbCrLf & "  , SUM(BI.FNQuantity) AS FNTotalRcvQty"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & "  , IM.FTRawMatNameTH AS FTRawMatName"
            _Str &= vbCrLf & "  , S.FTSuplNameTH AS FTSuplName"
            _Str &= vbCrLf & "  ,CASE WHEN ISNULL(POCO.FTRawMatColorNameTH,'') ='' THEN  IM.FTRawMatColorNameTH ELSE ISNULL(POCO.FTRawMatColorNameTH,'') END AS FTRawMatColorName"

        Else

            _Str &= vbCrLf & " , IM.FTRawMatNameEN AS FTRawMatName"
            _Str &= vbCrLf & "  , S.FTSuplNameEN AS FTSuplName"
            _Str &= vbCrLf & "  ,CASE WHEN ISNULL(POCO.FTRawMatColorNameEN,'') ='' THEN  IM.FTRawMatColorNameEN ELSE ISNULL(POCO.FTRawMatColorNameEN,'') END AS FTRawMatColorName"

        End If

        _Str &= vbCrLf & " ,ISNULL(("
        _Str &= vbCrLf & "     SELECT TOP 1 '1' AS FTStateQC "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS HD WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat AS DT WITH(NOLOCK) ON HD.FTQCFabNo = DT.FTQCFabNo"
        _Str &= vbCrLf & " WHERE  (HD.FTQCFabNo <> N'" & HI.UL.ULF.rpQuoted(FTQCFabNo.Text) & "') "
        _Str &= vbCrLf & "   AND (HD.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "') "
        _Str &= vbCrLf & "   AND (DT.FNHSysRawMatId = IM.FNHSysRawMatId)"
        _Str &= vbCrLf & "   AND (DT.FTBatchNo =B.FTBatchNo)"
        _Str &= vbCrLf & "  ),'0') AS FTStateQC"

        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK)  ON B.FTBarcodeNo = BI.FTBarcodeNo INNER JOIN"
        ' _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS H WITH(NOLOCK)  ON BI.FTDocumentNo = H.FTReceiveNo INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH(NOLOCK)  ON B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P WITH(NOLOCK)  ON B.FTPurchaseNo = P.FTPurchaseNo INNER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK)  ON IM.FTRawMatCode = MM.FTMainMatCode LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK)  ON IM.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)   ON MM.FNHSysSuplId = S.FNHSysSuplId AND P.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK)   ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)   ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Str &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_Purchase_RawMat_Color AS POCO ON BI.FTOrderNo = POCO.FTOrderNo AND B.FTPurchaseNo = POCO.FTPurchaseNo AND B.FNHSysRawMatId = POCO.FNHSysRawMatId"
        _Str &= vbCrLf & "  WHERE BI.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
        _Str &= vbCrLf & "        AND (MM.FNMerMatType = 0)"
        _Str &= vbCrLf & "  GROUP BY BI.FTDocumentNo, IM.FNHSysRawMatId, IM.FTRawMatCode,  MM.FTCusItemCodeRef, S.FTSuplCode, IMC.FTRawMatColorCode, "
        _Str &= vbCrLf & " U.FTUnitCode, B.FTBatchNo"
        _Str &= vbCrLf & "  ,IMS.FTRawMatSizeCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ", IM.FTRawMatNameTH "
            _Str &= vbCrLf & ", S.FTSuplNameTH "
            _Str &= vbCrLf & ",CASE WHEN ISNULL(POCO.FTRawMatColorNameTH,'') ='' THEN  IM.FTRawMatColorNameTH ELSE ISNULL(POCO.FTRawMatColorNameTH,'') END "
        Else
            _Str &= vbCrLf & ", IM.FTRawMatNameEN "
            _Str &= vbCrLf & ", S.FTSuplNameEN "
            _Str &= vbCrLf & ",CASE WHEN ISNULL(POCO.FTRawMatColorNameEN,'') ='' THEN  IM.FTRawMatColorNameEN ELSE ISNULL(POCO.FTRawMatColorNameEN,'') END "
        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        With _AddRawmatPopup
            .ogcrawmat.DataSource = _dt.Copy
            .ShowDialog()

            If .StateProc Then
                Dim _dtd As DataTable
                With CType(ogcrawmat.DataSource, DataTable)
                    .AcceptChanges()
                    _dtd = .Copy
                End With
                _dtd.Rows.Clear()
                _dtd.Rows.Add()

                With CType(ogcDetail.DataSource, DataTable)
                    .AcceptChanges()
                    .Rows.Clear()
                End With

                Dim _RowHandle As Integer = .ogvrawmat.FocusedRowHandle
                Dim _GridView As DevExpress.XtraGrid.Views.Grid.GridView = .ogcrawmat.MainView
                Try

                    For Each R As DataRow In _dtd.Rows

                        For Each oGridCol As DevExpress.XtraGrid.Columns.GridColumn In _GridView.Columns

                            Try
                                R.Item(oGridCol.FieldName) = _GridView.GetRowCellValue(_RowHandle, oGridCol.FieldName.ToString)
                            Catch ex As Exception
                            End Try

                        Next

                        Exit For

                    Next

                Catch ex As Exception
                End Try

                ogcrawmat.DataSource = _dtd.Copy

            End If

        End With

    End Sub

    Private Sub ocmaddbarcod_Click(sender As Object, e As EventArgs) Handles ocmaddbarcod.Click
        Try
            If CheckOwner() = False Then Exit Sub

            Dim _dtd As DataTable
            With CType(ogcrawmat.DataSource, DataTable)
                .AcceptChanges()
                _dtd = .Copy
            End With

            Dim _Qry As String

            _Qry = "SELECT TOP 1 BO.FTBarcodeNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS H WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTIssueNo = BO.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo, QD.FTRollNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
            _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Qry &= vbCrLf & "  ) AS Q"
            _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Qry &= vbCrLf & "    AND  B.FTRollNo=Q.FTRollNo"
            _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "  SELECT TOP 1 BO.FTBarcodeNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENSaleAndTerminate AS H WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTSaleAndTerminateNo = BO.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo, QD.FTRollNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
            _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Qry &= vbCrLf & "  ) AS Q"
            _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Qry &= vbCrLf & "    AND  B.FTRollNo=Q.FTRollNo"
            _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "  SELECT TOP 1 BO.FTBarcodeNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON H.FTReturnSuplNo = BO.FTDocumentNo INNER JOIN"
            _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) ON BO.FTBarcodeNo = B.FTBarcodeNo"
            _Qry &= vbCrLf & "  INNER JOIN ("
            _Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo, QD.FTRollNo"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
            _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
            _Qry &= vbCrLf & "  ) AS Q"
            _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
            _Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
            _Qry &= vbCrLf & "    AND  B.FTRollNo=Q.FTRollNo"
            _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                HI.MG.ShowMsg.mInfo("ม้วนผ้ามีการเดิน Transaction แล้วไม่สามารถทำการลบได้ !!!", 1502250019, Me.Text, , MessageBoxIcon.Warning)
            Else

                If _dtd.Rows.Count > 0 Then

                    Dim dt As New DataTable

                    For Each R As DataRow In _dtd.Rows

                        _Qry = " SELECT '0' AS FTSelect, B.FTRollNo, B.FNHSysRawMatId"
                        _Qry &= vbCrLf & ", SUM(BI.FNQuantity) AS FNQuantity, B.FTBatchNo, BI.FTDocumentNo,MAX(CASE WHEN ISNULL(FTFabricFrontSizeRcv,'')='' THEN FTFabricFrontSize ELSE ISNULL(FTFabricFrontSizeRcv,'') END) AS FTFabricFrontSize"
                        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
                        _Qry &= vbCrLf & "  INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                        _Qry &= vbCrLf & "  WHERE  (BI.FTDocumentNo = N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "')"
                        _Qry &= vbCrLf & "  AND    B.FTBatchNo='" & HI.UL.ULF.rpQuoted(R!FTBatchNo.ToString) & "' "
                        _Qry &= vbCrLf & "  AND    B.FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                        _Qry &= vbCrLf & "  GROUP BY B.FTRollNo, B.FNHSysRawMatId, B.FTBatchNo, BI.FTDocumentNo"

                        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                        Exit For
                    Next

                    With _AddBarcodePopup
                        .StateProc = False
                        .ogcrawmat.DataSource = dt.Copy
                        .ShowDialog()

                        If (.StateProc) Then

                            Dim _dtdSelect As DataTable
                            With CType(.ogcrawmat.DataSource, DataTable)
                                .AcceptChanges()
                                _dtdSelect = .Copy
                            End With

                            Dim _dtsource As DataTable
                            With CType(ogcDetail.DataSource, DataTable)
                                .AcceptChanges()
                                _dtsource = .Copy
                            End With

                            For Each R As DataRow In _dtdSelect.Select("FTSelect='1'")

                                If _dtsource.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId) & " AND FTBatchNo='" & HI.UL.ULF.rpQuoted(R!FTBatchNo.ToString) & "' AND FTRollNo='" & HI.UL.ULF.rpQuoted(R!FTRollNo.ToString) & "'").Length <= 0 Then

                                    _dtsource.Rows.Add(R!FNHSysRawMatId,
                                                       R!FTBatchNo,
                                                       R!FTRollNo,
                                                       R!FNQuantity, 0, 0, 0, 0, 0, 0, 0, "0", R!FTFabricFrontSize.ToString, R!FTFabricFrontSize.ToString, "")

                                End If

                            Next

                            Dim _FNTotalTicketed As Double = 0
                            Dim _FNTotalActual As Double = 0
                            Dim _FNTotalPoints As Integer = 0

                            For Each R As DataRow In _dtsource.Rows

                                _FNTotalTicketed = _FNTotalTicketed + Val(R!FNQuantity.ToString)
                                _FNTotalActual = _FNTotalActual + Val(R!FNActQuantity.ToString)
                                _FNTotalPoints = _FNTotalPoints + Val(R!FNTotalDefect.ToString)

                            Next

                            FNTotalTicketed.Value = _FNTotalTicketed
                            FNTotalActual.Value = _FNTotalActual
                            FNTotalPoints.Value = _FNTotalPoints
                            FNTotalDefects.Value = _FNTotalPoints
                            ogcDetail.DataSource = _dtsource

                        End If

                    End With

                Else
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวัตถุดิบที่ต้องการทำการตรวจสอบคุณภาพ !!!", 1502240017, Me.Text, , MessageBoxIcon.Warning)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryFNQCActual_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFNQCActual.EditValueChanged
    End Sub

    Private Sub RepositoryFNQCActual_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQCActual.EditValueChanging
        Try

            With Me.ogvDetail
                .SetFocusedRowCellValue("FNActQuantity", e.NewValue)
            End With

            Dim _dtsource As DataTable
            With CType(ogcDetail.DataSource, DataTable)
                .AcceptChanges()
                _dtsource = .Copy
            End With
            Dim _FNTotalTicketed As Double = 0
            Dim _FNTotalActual As Double = 0
            Dim _FNTotalPoints As Integer = 0

            For Each R As DataRow In _dtsource.Rows
                _FNTotalTicketed = _FNTotalTicketed + Val(R!FNQuantity.ToString)
                _FNTotalActual = _FNTotalActual + Val(R!FNActQuantity.ToString)
                _FNTotalPoints = _FNTotalPoints + Val(R!FNTotalDefect.ToString)
            Next

            FNTotalTicketed.Value = _FNTotalTicketed
            FNTotalActual.Value = _FNTotalActual
            FNTotalPoints.Value = _FNTotalPoints
            FNTotalDefects.Value = _FNTotalPoints
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepCal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepCal.EditValueChanging

        Try

            Dim _TotalDefect As Integer = 0

            With Me.ogvDetail
                .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)

                _TotalDefect = _TotalDefect + Integer.Parse(Val(.GetFocusedRowCellValue("FNYarn")))
                _TotalDefect = _TotalDefect + Integer.Parse(Val(.GetFocusedRowCellValue("FNContruction")))
                _TotalDefect = _TotalDefect + Integer.Parse(Val(.GetFocusedRowCellValue("FNDyeing")))
                _TotalDefect = _TotalDefect + Integer.Parse(Val(.GetFocusedRowCellValue("FNFinishing")))
                _TotalDefect = _TotalDefect + Integer.Parse(Val(.GetFocusedRowCellValue("FNCleanliness")))

                .SetFocusedRowCellValue("FNTotalDefect", _TotalDefect)

            End With

            Dim _dtsource As DataTable
            With CType(ogcDetail.DataSource, DataTable)
                .AcceptChanges()
                _dtsource = .Copy
            End With
            Dim _FNTotalTicketed As Double = 0
            Dim _FNTotalActual As Double = 0
            Dim _FNTotalPoints As Integer = 0

            For Each R As DataRow In _dtsource.Rows
                _FNTotalTicketed = _FNTotalTicketed + Val(R!FNQuantity.ToString)
                _FNTotalActual = _FNTotalActual + Val(R!FNActQuantity.ToString)
                _FNTotalPoints = _FNTotalPoints + Val(R!FNTotalDefect.ToString)
            Next

            FNTotalTicketed.Value = _FNTotalTicketed
            FNTotalActual.Value = _FNTotalActual
            FNTotalPoints.Value = _FNTotalPoints
            FNTotalDefects.Value = _FNTotalPoints

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FNTotalActual_EditValueChanged(sender As Object, e As EventArgs) Handles FNTotalActual.EditValueChanged

        Try
            If FNTotalTicketed.Value > 0 Then
                FNDifference.Value = ((FNTotalTicketed.Value - FNTotalActual.Value) / FNTotalTicketed.Value)
            Else
                FNDifference.Value = 0
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNTotalPoints_EditValueChanged(sender As Object, e As EventArgs) Handles FNTotalPoints.EditValueChanged, FNTotalActual.EditValueChanged
        Try

            If FNTotalActual.Value > 0 Then
                FNTotalPointsPerUOM.Value = ((FNTotalPoints.Value / FNTotalActual.Value) * 100)
            Else
                FNTotalPointsPerUOM.Value = 0
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdeletebarcode_Click(sender As Object, e As EventArgs) Handles ocmdeletebarcode.Click
        Call ProcessDeleteRoll()
    End Sub
    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvDetail.KeyDown
        If ocmdeletebarcode.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Delete
                Call ProcessDeleteRoll()
        End Select
    End Sub

    Private Sub ocmaccept_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        If CheckOwner() = False Then Exit Sub

        If FNTotalTicketed.Value > 0 Then
            If FNTotalActual.Value > 0 Then
                ' If Me.SaveData() Then

                Dim _Qry As String = ""

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric "
                _Qry &= vbCrLf & " SET FTStateAccept='1'"
                _Qry &= vbCrLf & ", FTAcceptUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FDAcceptDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTAcceptTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",FTStateReject='0'"
                _Qry &= vbCrLf & " WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN) = True Then

                    '_Qry = "UPDATE B SET FTShades = Q.FTShades,FTStateQCAccept='1',FTStateQCReject=Q.FTStateReject,FTFabricFrontSize=Q.FTActFabricFrontSize,FTFabricFrontSizeRcv=Q.FTFabricFrontSize"
                    '_Qry &= vbCrLf & " "
                    '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With(NOLOCK)"
                    '_Qry &= vbCrLf & "  INNER JOIN ("
                    '_Qry &= vbCrLf & " Select QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo,QD.FTShades,QDB.FTRollNo,QDB.FTStateReject,QDB.FTActFabricFrontSize,QDB.FTFabricFrontSize"
                    '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric As QH With(NOLOCK) INNER JOIN"
                    '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat As QD With(NOLOCK) On QH.FTQCFabNo = QD.FTQCFabNo"
                    '_Qry &= vbCrLf & "     INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail As QDB With(NOLOCK) On QD.FTQCFabNo = QDB.FTQCFabNo  AND QD.FNHSysRawMatId=QDB.FNHSysRawMatId AND QD.FTBatchNo=QDB.FTBatchNo  "
                    '_Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
                    '_Qry &= vbCrLf & "  ) AS Q"
                    '_Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
                    '_Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
                    '_Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
                    '_Qry &= vbCrLf & "    AND B.FTRollNo=Q.FTRollNo"


                    _Qry = "UPDATE B SET FTShades = Q.FTShades,FTStateQCAccept='1',FTStateQCReject=Q.FTStateReject,FTFabricFrontSize=Q.FTActFabricFrontSize,FTFabricFrontSizeRcv=Q.FTFabricFrontSize"
                    _Qry &= vbCrLf & " "
                    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With(NOLOCK)"
                    _Qry &= vbCrLf & "  INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BI With(NOLOCK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                    _Qry &= vbCrLf & "  INNER JOIN ("
                    _Qry &= vbCrLf & " Select QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo,QD.FTShades,QDB.FTRollNo,QDB.FTStateReject,QDB.FTActFabricFrontSize,QDB.FTFabricFrontSize"
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric As QH With(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat As QD With(NOLOCK) On QH.FTQCFabNo = QD.FTQCFabNo"
                    _Qry &= vbCrLf & "     INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail As QDB With(NOLOCK) On QD.FTQCFabNo = QDB.FTQCFabNo  AND QD.FNHSysRawMatId=QDB.FNHSysRawMatId AND QD.FTBatchNo=QDB.FTBatchNo  "
                    _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
                    _Qry &= vbCrLf & "  ) AS Q"
                    _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
                    _Qry &= vbCrLf & "    AND BI.FTDocumentNo=Q.FTReceiveNo "
                    _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
                    _Qry &= vbCrLf & "    AND B.FTRollNo=Q.FTRollNo"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                    FTStateAccept.Checked = True
                    FTStateReject.Checked = -False

                End If

                'End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวนตรวจสอบจริง !!!", 1502250177, Me.Text, , MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวนตรวจสอบ !!!", 1502250178, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click

        If CheckOwner() = False Then Exit Sub

        If FNTotalTicketed.Value > 0 Then
            If FNTotalActual.Value > 0 Then
                'If Me.SaveData() Then

                Dim _Qry As String = ""

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric "
                _Qry &= vbCrLf & " SET FTStateReject='1'"
                _Qry &= vbCrLf & ", FTRejectUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FDRejectDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTRejectTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ", FTStateAccept='0'"
                _Qry &= vbCrLf & " WHERE FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN) = True Then

                    '_Qry = "UPDATE B SET FTShades=Q.FTShades,FTStateQCAccept='0',FTStateQCReject='1'"
                    '_Qry &= vbCrLf & " "
                    '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
                    '_Qry &= vbCrLf & "  INNER JOIN ("
                    '_Qry &= vbCrLf & " SELECT QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo,QD.FTShades"
                    '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric AS QH WITH(NOLOCK) INNER JOIN"
                    '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat AS QD WITH(NOLOCK) ON QH.FTQCFabNo = QD.FTQCFabNo"
                    '_Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
                    '_Qry &= vbCrLf & "  ) AS Q "
                    '_Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
                    '_Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
                    '_Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo "

                    '_Qry = "UPDATE B SET FTShades = Q.FTShades,FTStateQCAccept='0',FTStateQCReject='1',FTFabricFrontSize=Q.FTActFabricFrontSize,FTFabricFrontSizeRcv=Q.FTFabricFrontSize"
                    '_Qry &= vbCrLf & " "
                    '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With(NOLOCK)"
                    '_Qry &= vbCrLf & "  INNER JOIN ("
                    '_Qry &= vbCrLf & " Select QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo,QD.FTShades,QDB.FTRollNo,QDB.FTStateReject,QDB.FTActFabricFrontSize,QDB.FTFabricFrontSize"
                    '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric As QH With(NOLOCK) INNER JOIN"
                    '_Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat As QD With(NOLOCK) On QH.FTQCFabNo = QD.FTQCFabNo"
                    '_Qry &= vbCrLf & "     INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail As QDB With(NOLOCK) On QD.FTQCFabNo = QDB.FTQCFabNo  AND QD.FNHSysRawMatId=QDB.FNHSysRawMatId AND QD.FTBatchNo=QDB.FTBatchNo  "
                    '_Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
                    '_Qry &= vbCrLf & "  ) AS Q"
                    '_Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
                    '_Qry &= vbCrLf & "    AND B.FTDocumentNo=Q.FTReceiveNo "
                    '_Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
                    '_Qry &= vbCrLf & "    AND B.FTRollNo=Q.FTRollNo"


                    _Qry = "UPDATE B SET FTShades = Q.FTShades,FTStateQCAccept='0',FTStateQCReject='1',FTFabricFrontSize=Q.FTActFabricFrontSize,FTFabricFrontSizeRcv=Q.FTFabricFrontSize"
                    _Qry &= vbCrLf & " "
                    _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As B With(NOLOCK)"
                    _Qry &= vbCrLf & "  INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As BI With(NOLOCK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                    _Qry &= vbCrLf & "  INNER JOIN ("
                    _Qry &= vbCrLf & " Select QH.FTReceiveNo, QD.FNHSysRawMatId, QD.FTBatchNo,QD.FTShades,QDB.FTRollNo,QDB.FTStateReject,QDB.FTActFabricFrontSize,QDB.FTFabricFrontSize"
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric As QH With(NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat As QD With(NOLOCK) On QH.FTQCFabNo = QD.FTQCFabNo"
                    _Qry &= vbCrLf & "     INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCFabric_Rawmat_Detail As QDB With(NOLOCK) On QD.FTQCFabNo = QDB.FTQCFabNo  AND QD.FNHSysRawMatId=QDB.FNHSysRawMatId AND QD.FTBatchNo=QDB.FTBatchNo  "
                    _Qry &= vbCrLf & " WHERE QH.FTQCFabNo='" & HI.UL.ULF.rpQuoted(Me.FTQCFabNo.Text) & "' "
                    _Qry &= vbCrLf & "  ) AS Q"
                    _Qry &= vbCrLf & "    ON B.FNHSysRawMatId=Q.FNHSysRawMatId "
                    _Qry &= vbCrLf & "    AND BI.FTDocumentNo=Q.FTReceiveNo "
                    _Qry &= vbCrLf & "    AND B.FTBatchNo=Q.FTBatchNo"
                    _Qry &= vbCrLf & "    AND B.FTRollNo=Q.FTRollNo"


                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                    FTStateAccept.Checked = False
                    FTStateReject.Checked = -True
                End If

                'End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวนตรวจสอบจริง !!!", 1502250177, Me.Text, , MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวนตรวจสอบ !!!", 1502250178, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ogcrawmat_Click(sender As Object, e As EventArgs) Handles ogcrawmat.Click

    End Sub

    Private Sub FTQCFabNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTQCFabNo.EditValueChanged

    End Sub

    Private Sub FTReceiveNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTReceiveNo.EditValueChanged

    End Sub

    Private Sub FTShades_EditValueChanged(sender As Object, e As EventArgs) Handles FTShades.EditValueChanged

    End Sub

    Private Sub FTShades_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FTShades.EditValueChanging

    End Sub

    Private Sub FTShades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FTShades.SelectedIndexChanged
        Try

            Dim ShadesData As String = FTShades.Text

            With CType(ogcDetail.DataSource, DataTable)

                .AcceptChanges()

                For Each R As DataRow In .Rows

                    R!FTShades = ShadesData

                Next

                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class