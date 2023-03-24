Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Public Class wQCAcc

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
   


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

#Region "Command"
    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTQCAccBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTQCAccBy.Text) & "' "
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
        If Me.FTQCAccNo.Text <> "" Then

            If DeleteData() Then

                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)

            Else

                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

            End If

        End If

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub
#End Region


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        Dim oSysLang As New ST.SysLanguage
        'Try

        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        'Catch ex As Exception
        'Finally
        'End Try

        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenBarcode.Name.ToString.Trim, _GenBarcode)
        'Catch ex As Exception
        'Finally
        'End Try

        '_Multiple = New wReceiveMultiple
        'HI.TL.HandlerControl.AddHandlerObj(_Multiple)
        'Try

        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Multiple.Name.ToString.Trim, _Multiple)
        'Catch ex As Exception
        'Finally
        'End Try

        '_AutoTransferToCenter = New wReceiveAutoTransferToCenter
        'HI.TL.HandlerControl.AddHandlerObj(_AutoTransferToCenter)
        'Try

        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AutoTransferToCenter.Name.ToString.Trim, _AutoTransferToCenter)
        'Catch ex As Exception
        'Finally
        'End Try

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

    Private Sub LoadDataHeadder(ByVal _RcvNo As String, ByVal _PoNo As String)
        Dim _Qry As String = ""
        Dim _oDt As DataTable
        '_Qry = "SELECT  Top 1    H.FTReceiveNo, H.FDReceiveDate, H.FTReceiveBy, H.FTPurchaseNo, H.FNHSysWHId, H.FNExchangeRate, H.FTInvoiceNo, H.FDInvoiceDate, H.FTRemark, H.FNRceceiveType, H.FNHSysCmpId, "
        '_Qry &= vbCrLf & "  H.FTStateImport, H.FTImportBy, H.FTImportDate, H.FTImportTime, D.FNHSysRawMatId, D.FNHSysUnitId, D.FTFabricFrontSize, D.FNPrice, D.FNDisPer, D.FNDisAmt, D.FNNetPrice, D.FNQuantity,"
        '_Qry &= vbCrLf & " D.FNNetAmt "
        '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS H LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS D ON H.FTReceiveNo = D.FTReceiveNo"
        '_Qry &= vbCrLf & " WHERE  H.FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNo) & "'"
        '_Qry &= vbCrLf & " AND  H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"

        _Qry = "SELECT  Top 1   P.FNHSysSuplId , S.FTSuplCode, S.FTSuplNameTH, S.FTSuplNameEN  FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS H LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS P ON H.FTPurchaseNo = P.FTPurchaseNo"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S ON P.FNHSysSuplId = S.FNHSysSuplId  "
        _Qry &= vbCrLf & " WHERE  H.FTReceiveNo='" & HI.UL.ULF.rpQuoted(_RcvNo) & "'"
        '  _Qry &= vbCrLf & " AND  H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"
        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        If _oDt.Rows.Count > 0 Then

            'Me.FNHSysSuplId.Properties.Tag = Val(_oDt.Rows(0)!FNHSysSuplId.ToString)
            RemoveHandler FNHSysSuplId_New.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            Me.FNHSysSuplId_New.Text = HI.UL.ULF.rpQuoted("" & _oDt.Rows(0)!FTSuplCode.ToString)
            RemoveHandler FNHSysSuplId_New.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                Me.FNHSysSuplId_None.Text = HI.UL.ULF.rpQuoted("" & _oDt.Rows(0)!FTSuplNameTH.ToString)
            Else
                Me.FNHSysSuplId_None.Text = HI.UL.ULF.rpQuoted("" & _oDt.Rows(0)!FTSuplNameEN.ToString)
            End If

        Else
            HI.TL.HandlerControl.ClearControl(Me.FNHSysSuplId_New)
            HI.TL.HandlerControl.ClearControl(Me.FNHSysSuplId_None)
        End If


    End Sub

    Private Sub LoadDataDetail(ByVal _RcvNo As String, ByVal _PoNo As String)
        Dim _Qry As String = ""
        Dim _oDt As DataTable
        Dim _StateNew As Boolean = False


        If Me.FTQCAccNo.Text <> "" Then
            _Qry = " SELECT  Top 1 FTQCAccNo FROM   [HITECH_INVENTORY].dbo.TINVENQCAcc  WHERE FTQCAccNo = '" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"
            Me.FTQCAccNo.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "")
        End If
 

        _Qry = "SELECT   U.FTUnitCode,  H.FTReceiveNo, H.FNHSysRawMatId, H.FNHSysUnitId, H.FTFabricFrontSize, H.FNPrice, H.FNDisPer, H.FNDisAmt, H.FNNetPrice, H.FNQuantity, H.FNNetAmt,   " 'H.FNQCQty,
        _Qry &= vbCrLf & " H.FNStateQC  AS FNStateQC_Hide, Isnull(H.FNDefectQty,0) AS FNDefectQty , Isnull(H.FTQCAccNo,'') AS FTQCAccNo , M.FTRawMatCode,  S.FTRawMatSizeCode,"
        _Qry &= vbCrLf & " C.FTRawMatColorCode"
        _Qry &= vbCrLf & ", Isnull(H.FNQCQty,( H.FNQuantity * (SELECT   top 1   FNQCPercent FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMConfigQCAccPer WHERE FNStartRcvQty <= H.FNQuantity and FNEndRcvQty >= H.FNQuantity) /100 )) as  FNQCQty "
        _Qry &= vbCrLf & ",Isnull(H.FNQCActualQty, ( H.FNQuantity * (SELECT   top 1   FNQCPercent FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMConfigQCAccPer WHERE FNStartRcvQty <= H.FNQuantity and FNEndRcvQty >= H.FNQuantity) /100 )) as  FNQCActualQty "
        _Qry &= vbCrLf & ",ISNULL(  H.FNDefectQty,( SELECT top 1      FNDefectPercent FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMConfigQCAccPer WHERE FNStartRcvQty <= H.FNQuantity and FNEndRcvQty >= H.FNQuantity))  AS FNDefectActualQty "
            '_Qry &= vbCrLf & ",(SELECT     FNListIndex, FTNameTH, FTNameEN  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  WHERE     (FTListName = 'FNStateQC') and FNListIndex = H.FNStateQC  )"


        'FNQCDefectQty
        _Qry &= vbCrLf & ", Isnull(H.FNQCDefectQty, (( H.FNQuantity * (SELECT   top 1   FNQCPercent FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMConfigQCAccPer WHERE FNStartRcvQty <= H.FNQuantity and FNEndRcvQty >= H.FNQuantity) /100 )  * "
        _Qry &= "  ( SELECT top 1      FNDefectPercent FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMConfigQCAccPer WHERE FNStartRcvQty <= H.FNQuantity and FNEndRcvQty >= H.FNQuantity)  / 100  ))  as FNQCDefectQty  "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",U.FTUnitNameTH AS FTUnitName , M.FTRawMatNameTH as FTRawMatName , C.FTRawMatColorNameTH as FTMatColorName , S.FTRawMatSizeNameTH as FTRawMatSizeName "
            _Qry &= vbCrLf & ",(SELECT    FTNameTH  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  WHERE     (FTListName = 'FNStateQC') and FNListIndex = H.FNStateQC  ) AS FNStateQC "
        Else
            _Qry &= vbCrLf & ",U.FTUnitNameEN AS FTUnitName , M.FTRawMatNameEN as FTRawMatName , C.FTRawMatColorNameEN as FTMatColorName , S.FTRawMatSizeNameEN as FTRawMatSizeName "
            _Qry &= vbCrLf & ",(SELECT   FTNameEN  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData  WHERE     (FTListName = 'FNStateQC') and FNListIndex = H.FNStateQC ) AS FNStateQC "
        End If

        _Qry &= vbCrLf & "FROM         (SELECT     a.FTReceiveNo, a.FNHSysRawMatId, a.FNHSysUnitId, a.FTFabricFrontSize, a.FNPrice, a.FNDisPer, a.FNDisAmt, a.FNNetPrice, a.FNQuantity, a.FNNetAmt, t.FNQCQty, t.FNQCActualQty, "
        _Qry &= vbCrLf & "t.FNStateQC, t.FNDefectQty, t.FTQCAccNo,t.FNQCDefectQty"
        _Qry &= vbCrLf & "          FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS a LEFT OUTER JOIN"
        _Qry &= vbCrLf & "                                     (SELECT     TINVENQCAcc.FTPurchaseNo, TINVENQCAcc.FTReceiveNo, TINVENQCAcc_Detail.FTQCAccNo, TINVENQCAcc_Detail.FNHSysRawMatId, TINVENQCAcc_Detail.FNQCQty, "
        _Qry &= vbCrLf & "                                                               TINVENQCAcc_Detail.FNQCActualQty, TINVENQCAcc_Detail.FNStateQC, TINVENQCAcc_Detail.FNDefectQty, TINVENQCAcc.FTQCAccNo AS Expr1,TINVENQCAcc_Detail.FNQCDefectQty"
        _Qry &= vbCrLf & "                                        FROM          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc INNER JOIN"
        _Qry &= vbCrLf & "                                                                 [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc_Detail ON TINVENQCAcc.FTQCAccNo = TINVENQCAcc_Detail.FTQCAccNo"
        '_Qry &= vbCrLf & "  WHERE TINVENQCAcc_Detail.FTQCAccNo = N'" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"
        _Qry &= vbCrLf & "  ) AS t ON a.FTReceiveNo = t.FTReceiveNo AND "
        _Qry &= vbCrLf & " a.FNHSysRawMatId = t.FNHSysRawMatId"
        _Qry &= vbCrLf & "               WHERE      (a.FTReceiveNo = N'" & HI.UL.ULF.rpQuoted(_RcvNo) & "')"  'and (a.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "')

        _Qry &= vbCrLf & " and (Isnull(t.FTQCAccNo,'') = N'" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Properties.Tag.ToString) & "' OR Isnull(t.FTQCAccNo,'') = '')"

        _Qry &= vbCrLf & "  ) AS H LEFT OUTER JOIN "
        _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M ON H.FNHSysRawMatId = M.FNHSysRawMatId "
        _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS TM ON M.FTRawMatCode = TM.FTMainMatCode "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "               [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"

        _Qry &= vbCrLf & "   LEFT OUTER JOIN     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U ON H.FNHSysUnitId = U.FNHSysUnitId "

        _Qry &= vbCrLf & " WHERE  TM.FNMerMatType = 1"
        _Qry &= vbCrLf & " ORDER BY M.FTRawMatCode,C.FTRawMatColorCode,S.FNRawMatSizeSeq"

        _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        ogcDetail.DataSource = _oDt

    End Sub

    Private Sub FTReceiveNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTReceiveNo.EditValueChanged, FTPurchaseNo.EditValueChanged
        Try

            If Me.FTReceiveNo.Text <> "" And Me.FTPurchaseNo.Text <> "" Then
                Me.LoadDataHeadder(HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text), HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text))
                Me.LoadDataDetail(HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text), HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text))
            End If

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

            If SaveDetail(_Key) = False Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
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
            Dim _Qty As String = ""

            Dim i As Integer = 0
            For i = 0 To ogvDetail.RowCount - 1
                With ogvDetail
                    If .GetRowCellValue(i, "FNStateQC").ToString() <> "" Then
                        _Qty = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc_Detail"
                        _Qty &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qty &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                        _Qty &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qty &= vbCrLf & ",FNQCQty=" & CDbl("0" & .GetRowCellValue(i, "FNQCQty").ToString())
                        _Qty &= vbCrLf & ",FNQCActualQty=" & CDbl("0" & .GetRowCellValue(i, "FNQCActualQty").ToString())
                        _Qty &= vbCrLf & ",FNDefectQty=" & CDbl("0" & .GetRowCellValue(i, "FNDefectQty").ToString())
                        _Qty &= vbCrLf & ",FNStateQC=" & CDbl("0" & .GetRowCellValue(i, "FNStateQC_Hide").ToString())
                        _Qty &= vbCrLf & ",FNQCDefectQty=" & CDbl("0" & .GetRowCellValue(i, "FNQCDefectQty").ToString())
                        _Qty &= vbCrLf & " WHERE  FTQCAccNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qty &= vbCrLf & " AND FNHSysRawMatId=" & CInt("0" & .GetRowCellValue(i, "FNHSysRawMatId").ToString())
                        If HI.Conn.SQLConn.Execute_Tran(_Qty, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qty = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc_Detail (FTInsUser, FDInsDate, FTInsTime, FTQCAccNo, FNHSysRawMatId, FNQCQty, FNQCActualQty, FNStateQC, FNDefectQty,FNQCDefectQty)"
                            _Qty &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qty &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qty &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qty &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qty &= vbCrLf & "," & CInt("0" & .GetRowCellValue(i, "FNHSysRawMatId").ToString())
                            _Qty &= vbCrLf & "," & CDbl("0" & .GetRowCellValue(i, "FNQCQty").ToString())
                            _Qty &= vbCrLf & "," & CDbl("0" & .GetRowCellValue(i, "FNQCActualQty").ToString())
                            _Qty &= vbCrLf & "," & CInt("0" & .GetRowCellValue(i, "FNStateQC_Hide").ToString())
                            _Qty &= vbCrLf & "," & CDbl("0" & .GetRowCellValue(i, "FNDefectQty").ToString())
                            _Qty &= vbCrLf & "," & CDbl("0" & .GetRowCellValue(i, "FNQCDefectQty").ToString())
                            If HI.Conn.SQLConn.Execute_Tran(_Qty, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                Return False
                            End If
                        End If
                    End If



                End With


            Next



            Return True
        Catch ex As Exception

            Return False
        End Try

    End Function


    Public Sub LoadDataInfo(Key As Object)
        _ProcLoad = True
        'HI.TL.HandlerControl.ClearControl(ogbh)
        'HI.TL.HandlerControl.ClearControl(ogbpayment)
        'HI.TL.HandlerControl.ClearControl(ogbsuplcfm)
        'HI.TL.HandlerControl.ClearControl(ogbpoamt)
        'HI.TL.HandlerControl.ClearControl(ogbnote)
        'HI.TL.HandlerControl.ClearControl(ogbdocdetail)

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
        If Me.FTReceiveNo.Text <> "" And Me.FTPurchaseNo.Text <> "" Then
            Me.LoadDataHeadder(HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text), HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text))
            Me.LoadDataDetail(HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text), HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text))
        End If

        Dim _Qry As String = ""
        Dim _dttmp As DataTable
        FTUserState.Text = ""
        FTUserStateDate.Text = ""

        '_Qry = " SELECT TOP 1 CASE WHEN ISNULL(FTRejectUser,'')='' THEN ISNULL(FTAcceptUser,'') ELSE ISNULL(FTRejectUser,'') END AS  FTUserState "
        _Qry = " SELECT TOP 1  ISNULL(FTAppName,'') AS  FTUserState "
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(FTAppDate) = 1 THEN Convert(nvarchar(10),Convert(Datetime,FTAppDate),103) +'  ' +  FTAppTime  ELSE '' END AS  FDAcceptDate "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc AS X WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"

        _dttmp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In _dttmp.Rows
            FTUserState.Text = R!FTUserState.ToString
            FTUserStateDate.Text = R!FDAcceptDate.ToString
            Exit For
        Next

        If Me.FTQCAccNo.Properties.Tag.ToString <> "" Then
            Me.FTPurchaseNo.Enabled = False
            Me.FTReceiveNo.Enabled = False
        Else
            Me.FTPurchaseNo.Enabled = True
            Me.FTReceiveNo.Enabled = True
        End If

        _ProcLoad = False
    End Sub

    'Private Function SaveDataNew() As Boolean
    '    Try
    '        Dim _Str As String = ""

    '        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        _Str = "UPDATE  " & _FormHeader(cind).TableName & ""
    '        _Str &= vbCrLf & " SET "



    '        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            HI.Conn.SQLConn.Tran.Rollback()
    '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            Return False
    '        End If


    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


    '        Return True
    '    Catch ex As Exception
    '        HI.Conn.SQLConn.Tran.Rollback()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '        Return False
    '    End Try

    'End Function

    Private Function VerrifyData() As Boolean

        If Me.FTQCAccNo.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTQCAccNo_lbl.Text)
            FTQCAccNo.Focus()
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

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

 

    Private Sub ogvDetail_BeforeLeaveRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs)
        Dim _State As Integer = 0

        With ogvDetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            'FNDefectQty
            If CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) > CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity")) Then
                .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity")))
            End If

            If CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                .SetRowCellValue(.FocusedRowHandle, "FNDefectQty", CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")))
            End If

            ''check Defect 


            If CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString) > 0 Then
                If CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl(CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100) Then
                    'reject
                    _State = 2
                Else
                    'pass
                    _State = 1
                End If

                Dim _Qry As String = ""
                _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
                _Qry &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
                _Qry &= vbCrLf & "WHERE     (FTListName = 'FNStateQC')"
                _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_State)

                Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
                If _oDt.Rows.Count > 0 Then
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameTH.ToString)
                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameEN.ToString)

                    End If

                End If
                .SetRowCellValue(.FocusedRowHandle, "FNStateQC_Hide", _State)

            End If


            ''check Defect

        End With

    End Sub

  
 

    Private Sub ogvDetail_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvDetail.FocusedColumnChanged
        With ogvDetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            If .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty").ToString <> "" And .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString <> "" Then
                If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) > 0 Then
                    cFNStateQC.OptionsColumn.AllowEdit = True
                End If
            Else
                cFNStateQC.OptionsColumn.AllowEdit = False
            End If
        End With
    End Sub


    Private Sub ogvDetail_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ogvDetail.FocusedRowChanged
        With ogvDetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            If .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty").ToString <> "" And .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString <> "" Then
                If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) > 0 Then
                    cFNStateQC.OptionsColumn.AllowEdit = True
                End If
            Else
                cFNStateQC.OptionsColumn.AllowEdit = False
            End If
        End With
    End Sub

    Private Sub ogvDetail_HiddenEditor(sender As Object, e As EventArgs) Handles ogvDetail.HiddenEditor
        Dim _State As Integer = 0


        With ogvDetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            'FNDefectQty
            If .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty").ToString = "" Then Exit Sub
            If .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString = "" Then Exit Sub


            If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) Then
                .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")))
            End If

            If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCQty")))
            End If


            If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString = "" Then Exit Sub
            If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                .SetRowCellValue(.FocusedRowHandle, "FNDefectQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")))
            End If
            If .GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty").ToString = "" Then Exit Sub
            .SetRowCellValue(.FocusedRowHandle, "FNQCDefectQty", CDbl(CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100))



            ' ''check Defect 
            'If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString <> "" Then
            '    If CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl(CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100) Then
            '        'reject
            '        _State = 2
            '    Else
            '        'pass
            '        _State = 1
            '    End If

            '    Dim _Qry As String = ""
            '    _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
            '    _Qry &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
            '    _Qry &= vbCrLf & "WHERE     (FTListName = 'FNStateQC')"
            '    _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_State)

            '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
            '    If _oDt.Rows.Count > 0 Then
            '        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            '            .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameTH.ToString)
            '        Else
            '            .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameEN.ToString)

            '        End If

            '    End If
            '    .SetRowCellValue(.FocusedRowHandle, "FNStateQC_Hide", _State)

            'End If
            ''check Defect

        End With
    End Sub


    Private Sub ogvDetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvDetail.RowStyle
        Dim View As GridView = sender
        If (e.RowHandle >= 0) Then
            With ogvDetail
                If View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNStateQC_Hide")).ToString <> "" Then
                    If CInt(View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNStateQC_Hide"))) = 1 Then
                        e.Appearance.BackColor = Color.LightGreen
                        e.Appearance.BackColor2 = Color.LightGreen
                    ElseIf CInt(View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNStateQC_Hide"))) = 2 Then
                        e.Appearance.BackColor = Color.LightGray
                        e.Appearance.BackColor2 = Color.LightGray
                    End If
                End If

            End With
        End If

    End Sub

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc_Detail WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'")

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
        RemoveHandler FNHSysSuplId_New.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
    End Sub
 

    Private Sub ReposDefectQty_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposDefectQty.EditValueChanging
        Try
            Dim _State As Integer = 0


            With ogvDetail
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                'FNDefectQty
                If .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty").ToString = "" Then Exit Sub
                If .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString = "" Then Exit Sub

                If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) Then
                    .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")))
                End If

                If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                    .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCQty")))
                End If

                If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString = "" Then Exit Sub
                If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                    .SetRowCellValue(.FocusedRowHandle, "FNDefectQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")))
                End If

                If .GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty").ToString = "" Then Exit Sub
                .SetRowCellValue(.FocusedRowHandle, "FNQCDefectQty", CDbl(CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100))
                ''check Defect 

                ' If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString <> "" Then
                If IsNothing(e) Then
                   

                    _State = 1
                    Dim _Qry As String = ""
                    _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
                    _Qry &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
                    _Qry &= vbCrLf & "WHERE     (FTListName = 'FNStateQC')"
                    _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_State)

                    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
                    If _oDt.Rows.Count > 0 Then
                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameTH.ToString)
                        Else
                            .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameEN.ToString)

                        End If

                    End If
                    .SetRowCellValue(.FocusedRowHandle, "FNStateQC_Hide", _State)
                Else
                    If e.NewValue.ToString <> "" Then
                        If CDbl(e.NewValue.ToString) > CDbl(CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100) Then
                            'reject
                            _State = 2
                        Else
                            'pass
                            _State = 1
                        End If

                        Dim _Qry As String = ""
                        _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
                        _Qry &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
                        _Qry &= vbCrLf & "WHERE     (FTListName = 'FNStateQC')"
                        _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_State)

                        Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
                        If _oDt.Rows.Count > 0 Then
                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameTH.ToString)
                            Else
                                .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameEN.ToString)

                            End If

                        End If
                        .SetRowCellValue(.FocusedRowHandle, "FNStateQC_Hide", _State)

                    End If
                End If
                'If e.NewValue.ToString <> "" Then
                '    If CDbl(e.NewValue.ToString) > CDbl(CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100) Then
                '        'reject
                '        _State = 2
                '    Else
                '        'pass
                '        _State = 1
                '    End If

                '    Dim _Qry As String = ""
                '    _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
                '    _Qry &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
                '    _Qry &= vbCrLf & "WHERE     (FTListName = 'FNStateQC')"
                '    _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_State)

                '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
                '    If _oDt.Rows.Count > 0 Then
                '        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                '            .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameTH.ToString)
                '        Else
                '            .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameEN.ToString)

                '        End If

                '    End If
                '    .SetRowCellValue(.FocusedRowHandle, "FNStateQC_Hide", _State)

                'End If
                ''check Defect

            End With
        Catch ex As Exception

        End Try
    End Sub
 
    Private Sub FTQCAccNo_TextChanged(sender As Object, e As EventArgs) Handles FTQCAccNo.TextChanged
        If Me.FTQCAccNo.Text = "" Or Microsoft.VisualBasic.Right(Me.FTQCAccNo.Text, 4) = "####" Then
            Me.FTPurchaseNo.Enabled = True
            Me.FTReceiveNo.Enabled = True

        Else
            Me.FTPurchaseNo.Enabled = False
            Me.FTReceiveNo.Enabled = False
        End If
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTQCAccNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "rptQCAccSlip.rpt"
                .Formular = "{TINVENQCAcc.FTQCAccNo}='" & HI.UL.ULF.rpQuoted(FTQCAccNo.Text) & "' "
                .Preview()
            End With
        End If

    End Sub
 
   
   
    Private Sub RepositoryFNQCActual_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryFNQCActual.EditValueChanging
        Try
            With ogvDetail
                'If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                'If CDbl(e.NewValue.ToString) > CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity")) Then
                '    .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNQuantity")))

                'End If

                'If CDbl("0" & .GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl(e.NewValue.ToString) Then
                '    .SetRowCellValue(.FocusedRowHandle, "FNDefectQty", CDbl(e.NewValue.ToString))
                'End If

                If e.NewValue.ToString = "" Then Exit Sub
                If .GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty").ToString = "" Then Exit Sub

                .SetRowCellValue(.FocusedRowHandle, "FNQCDefectQty", CDbl(CDbl(e.NewValue.ToString) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100))
            End With


        Catch ex As Exception

        End Try
    End Sub

   
    Private Sub ReposDefectQty_KeyDown(sender As Object, e As KeyEventArgs) Handles ReposDefectQty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'ReposDefectQty_EditValueChanging(ReposDefectQty, New DevExpress.XtraEditors.Controls.ChangingEventArgs)
                Dim _State As Integer = 0
                With ogvDetail
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                    'FNDefectQty
                    If .GetRowCellValue(.FocusedRowHandle, "FNQCActualQty").ToString = "" Then Exit Sub
                    If .GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString = "" Then Exit Sub

                    If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")) Then
                        .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQuantity")))
                    End If

                    If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                        .SetRowCellValue(.FocusedRowHandle, "FNQCActualQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCQty")))
                    End If

                    If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString = "" Then Exit Sub
                    If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectQty")) > CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) Then
                        .SetRowCellValue(.FocusedRowHandle, "FNDefectQty", CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")))
                    End If

                    If .GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty").ToString = "" Then Exit Sub
                    .SetRowCellValue(.FocusedRowHandle, "FNQCDefectQty", CDbl(CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100))
                    ''check Defect 

                    ' If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString <> "" Then
                    
                    If .GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString <> "" Then
                        If CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectQty").ToString) > CDbl(CDbl(.GetRowCellValue(.FocusedRowHandle, "FNQCActualQty")) * CDbl(.GetRowCellValue(.FocusedRowHandle, "FNDefectActualQty")) / 100) Then
                            'reject
                            _State = 2
                        Else
                            'pass
                            _State = 1
                        End If

                        Dim _Qry As String = ""
                        _Qry = "SELECT  top 1    FNListIndex, FTNameTH, FTNameEN"
                        _Qry &= vbCrLf & "FROM HITECH_SYSTEM.dbo.HSysListData"
                        _Qry &= vbCrLf & "WHERE     (FTListName = 'FNStateQC')"
                        _Qry &= vbCrLf & " AND FNListIndex=" & CInt(_State)

                        Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
                        If _oDt.Rows.Count > 0 Then

                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameTH.ToString)
                            Else
                                .SetRowCellValue(.FocusedRowHandle, "FNStateQC", _oDt.Rows(0)!FTNameEN.ToString)

                            End If

                        End If

                        .SetRowCellValue(.FocusedRowHandle, "FNStateQC_Hide", _State)

                    End If

                End With
                 
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTQCAccNo.Text <> "" And Me.FTQCAccNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateApp='0' "
                _Qry &= vbCrLf & "  ,FTAppName='' "
                _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                Call SendMailApp()
            End If
            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTQCAccNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub SendMailApp()
        Dim _Qry As String = ""
        Dim _UserMailTo As String = ""

        _Qry = " SELECT TOP 1 Tm.FTUserName"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS Tm WITH(NOLOCK)  ON U.FNHSysTeamGrpId = Tm.FNHSysTeamGrpId"
        _Qry &= vbCrLf & " WHERE  (U.FTUserName = N'" & HI.UL.ULF.rpQuoted(Me.FTQCAccBy.Text) & "')"

        _UserMailTo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        If _UserMailTo <> "" Then

            Dim tmpsubject As String = ""
            Dim tmpmessage As String = ""

            tmpsubject = "Send Approve QC. Accessories No " & Me.FTQCAccNo.Text & "  From Purchase No " & FTPurchaseNo.Text & "   "
            tmpmessage = "Send Approve QC. Accessories No " & Me.FTQCAccNo.Text & "  From Purchase No " & FTPurchaseNo.Text & "   "
            tmpmessage &= vbCrLf & "Date :" & Me.FDQCAccDate.Text
            tmpmessage &= vbCrLf & "By :" & Me.FTQCAccBy.Text
            tmpmessage &= vbCrLf & "Note :" & Me.FTRemark.Text

            If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 5, Me.FTQCAccNo.Text) Then

            End If

        End If
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        If CheckOwner() = False Then Exit Sub

        If Me.FTQCAccNo.Text <> "" And Me.FTQCAccNo.Properties.Tag.ToString <> "" Then

            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc "
                _Qry &= vbCrLf & "  SET FTStateApp='1' "
                _Qry &= vbCrLf & ", FTAppName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & ", FTAppTime=" & HI.UL.ULDate.FormatTimeDB & " "

                _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

            End If

            FTStateApp.Checked = True

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTQCAccNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo(FTQCAccNo.Properties.Tag.ToString)
    End Sub

    Private Sub FTQCAccNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTQCAccNo.EditValueChanged

    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Try

            If CheckOwner() = False Then Exit Sub
            If Me.FTQCAccNo.Text <> "" And Me.FTQCAccNo.Properties.Tag.ToString <> "" Then

                Dim _Qry As String = ""
                _Qry = "Select  TOP  1  FTStateApp  "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'  "

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "") <> "0" Then

                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENQCAcc "
                    _Qry &= vbCrLf & "  SET FTStateApp='0' "
                    _Qry &= vbCrLf & ", FTAppName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ", FTAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & ", FTAppTime=" & HI.UL.ULDate.FormatTimeDB & " "

                    _Qry &= vbCrLf & " WHERE FTQCAccNo='" & HI.UL.ULF.rpQuoted(Me.FTQCAccNo.Text) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PUR)

                End If

                FTStateApp.Checked = False

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTQCAccNo_lbl.Text)
                FTPurchaseNo.Focus()
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class