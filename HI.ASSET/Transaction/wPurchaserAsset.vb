Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

Public Class wPurchaserAsset
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wAddItemPOAsset
    Private _RevisePopup As wPurchaserAssetRevise

    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True

    Sub New()
        _FormLoad = True

        ' This call is required by the designer.
        InitializeComponent()
        Call PrepareForm()
        _AddItemPopup = New wAddItemPOAsset
        _RevisePopup = New wPurchaserAssetRevise
        TL.HandlerControl.AddHandlerObj(_AddItemPopup)
        TL.HandlerControl.AddHandlerObj(_RevisePopup)
        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
            Call oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, _RevisePopup.Name.ToString.Trim, _RevisePopup)
        Catch ex As Exception
        Finally
        End Try

        ' Add any initialization after the InitializeComponent() call.

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

#Region "Proceducre"
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

        Call LoadPoDetail(Key.ToString)
        Call LoadDataTop10Price()
        Call LoadCmpRun()
        Call LoadDeliveryCode()
        Me.oxtb.SelectedTabPageIndex = 0


        _Dt.Dispose()
        _ProcLoad = False
    End Sub

    Private Sub LoadPoDetail(PoKey As String)
        Dim Qry As String = ""
        'Dim _dtdetail As DataTable

        Qry = "SELECT   PD.FNHSysFixedAssetId,PD.FNSeq,isnull(A.FTAssetCode,AP.FTAssetPartCode) AS FTAssetCode,A.FTSerialNo"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",isnull(A.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,isnull(B.FTAssetBrandNameTH,'-') AS FTAssetBrandName,isnull(M.FTAssetModelNameTH,'-') AS FTAssetModelName,L.FTNameTH AS FNFixedAssetType"
        Else
            Qry &= vbCrLf & ",isnull(A.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,isnull(B.FTAssetBrandNameEN,'-') AS FTAssetBrandName,isnull(M.FTAssetModelNameEN,'-') AS FTAssetModelName,L.FTNameEN AS FNFixedAssetType"
        End If
        Qry &= vbCrLf & ",PD.FNPrice,PD.FNDisPer,PD.FNDisAmt,A.FTProductCode"
        Qry &= vbCrLf & ",PD.FNQuantity,PD.FNNetAmt,PD.FTRemark,U.FTUnitAssetCode AS FTUnitCode"
        Qry &= vbCrLf & ",isnull((select top 1 '1' AS FTStateRcv"
        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS R WItH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH(NOLOCK) ON R.FTReceiveNo=RD.FTReceiveNo"
        Qry &= vbCrLf & "where R.FTPurchaseNo=PD.FTPurchaseNo),'') AS FTStateRcv"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS P WITH(NOLOCK) ON PD.FTPurchaseNo = P.FTPurchaseNo LEFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) ON PD.FNHSysFixedAssetId=A.FNHSysFixedAssetId and PD.FNFixedAssetType=A.FNFixedAssetType LEFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON PD.FNHSysFixedAssetId=AP.FNHSysAssetPartId LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WItH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH(NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON PD.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUtER JOIN"
        Qry &= vbCrLf & "(SELECT L.FTNameTH ,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName ='FNFixedAssetType')AS L   ON PD.FNFixedAssetType=L.FNListIndex"
        Qry &= vbCrLf & "where PD.FTPurchaseNo='" & UL.ULF.rpQuoted(PoKey) & "'"

        Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)

        Qry = "SELECT   PD.FNSeq,isnull(A.FTAssetCode,AP.FTAssetPartCode) AS FTAssetCode"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",isnull(A.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,isnull(B.FTAssetBrandNameTH,'-') AS FTAssetBrandName,isnull(M.FTAssetModelNameTH,'-') AS FTAssetModelName,L.FTNameTH AS cFNFixedAssetType"
        Else
            Qry &= vbCrLf & ",isnull(A.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,isnull(B.FTAssetBrandNameEN,'-') AS FTAssetBrandName,isnull(M.FTAssetModelNameEN,'-') AS FTAssetModelName,L.FTNameEN AS cFNFixedAssetType"
        End If
        Qry &= vbCrLf & ",PD.FNPrice,PD.FNDisPer,PD.FNDisAmt,A.FTProductCode"
        Qry &= vbCrLf & ",sum(PD.FNQuantity) as FNQuantity,PD.FNNetAmt"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS P WITH(NOLOCK) ON PD.FTPurchaseNo = P.FTPurchaseNo LEFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) ON PD.FNHSysFixedAssetId=A.FNHSysFixedAssetId and PD.FNFixedAssetType=A.FNFixedAssetType LEFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON PD.FNHSysFixedAssetId=AP.FNHSysAssetPartId LEFT OUTER Join"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand AS B WItH(NOLOCK) ON A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUTER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel AS M WITH(NOLOCK) ON A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUtER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON PD.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUtER JOIN"
        Qry &= vbCrLf & "(SELECT L.FTNameTH ,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName ='FNFixedAssetType')AS L   ON PD.FNFixedAssetType=L.FNListIndex"
        Qry &= vbCrLf & "where PD.FTPurchaseNo='" & UL.ULF.rpQuoted(PoKey) & "'"
        Qry &= vbCrLf & "group by PD.FNSeq,A.FTAssetCode,PD.FNPrice,PD.FNDisPer,PD.FNDisAmt,PD.FNNetAmt,AP.FTAssetPartCode,A.FTProductCode"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",B.FTAssetBrandNameTH,M.FTAssetModelNameTH, A.FTAssetNameTH,AP.FTAssetPartNameTH,L.FTNameTH"
        Else
            Qry &= vbCrLf & ",B.FTAssetBrandNameEN,M.FTAssetModelNameEN, A.FTAssetNameEN,AP.FTAssetPartNameEN,L.FTNameEN"
        End If

        Me.ogcsum.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)

    End Sub

    Private Sub LoadDataTop10Price()
        Dim Qry As String = ""

        Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.[SP_PurchaserAsset_History] '" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & ST.Lang.Language & " "
        Me.ogcpricehistory.DataSource = Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)

    End Sub
    Private Sub LoadCmpRun()
        Dim Qry As String = ""
        Dim _DT As DataTable
        Qry = "SELECT  R.FTCmpRunCode,R.FNHSysCmpRunId"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",R.FTCmpRunNameTH AS FTCmpRunName"
        Else
            Qry &= vbCrLf & ",R.FTCmpRunNameEN AS FTCmpRunName"
        End If
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS  P WITH(NOLOCK)  INNER JOIN"
        Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRunAsset AS R  WITH(NOLOCK) ON P.FNHSysCmpRunId = R.FNHSysCmpRunId"
        Qry &= vbCrLf & "WHERE P.FTPurchaseNo = '" & FTPurchaseNo.Text & "'"

        _DT = Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
        For Each R As DataRow In _DT.Rows
            FNHSysCmpRunId.Text = R!FTCmpRunCode
            FNHSysCmpRunId.Properties.Tag = R!FNHSysCmpRunId
            FNHSysCmpRunId_None.Text = R!FTCmpRunName
        Next
    End Sub

    Private Sub LoadDeliveryCode()
        Dim Qry As String = ""
        Dim _DT As DataTable
        Qry = "SELECT D.FTDeliveryCode,D.FTDescription,D.FNHSysDeliveryId"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS  P WITH(NOLOCK) LEFT OUTER JOIN"
        Qry &= vbCrLf & "(SELECT   FTDeliveryCode ,FNHSysDeliveryId  "
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",FTDeliveryDescTH AS FTDescription"
        Else
            Qry &= vbCrLf & ",FTDeliveryDescEN AS FTDescription"
        End If
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery WITH ( NOLOCK )   "
        Qry &= vbCrLf & "union all"
        Qry &= vbCrLf & "select FTWHAssetCode as FTDeliveryCode,FNHSysWHAssetId as FNHSysDeliveryId"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",FTWHAssetNameTH AS FTDescription"
        Else
            Qry &= vbCrLf & ",FTWHAssetNameEN AS FTDescription"
        End If
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseAsset WITH ( NOLOCK )  )AS D ON P.FNHSysDeliveryId=D.FNHSysDeliveryId"
        Qry &= vbCrLf & "WHERE P.FTPurchaseNo = '" & FTPurchaseNo.Text & "'"

        _DT = Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
        For Each R As DataRow In _DT.Rows
            FNHSysDeliveryId.Text = R!FTDeliveryCode
            FNHSysDeliveryId.Properties.Tag = R!FNHSysDeliveryId
            FNHSysDeliveryId_None.Text = R!FTDescription
        Next
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
        _FormLoad = False
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTPurchaseBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
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

    Private Function SaveData(Optional RevisedRemark As String = "", Optional StateRcv As Boolean = False) As Boolean
        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Str As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _CmpH As String = ""
        'Dim _StateUpdate As Boolean = False

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
            ' Dim _type As String = HI.Conn.SQLConn.GetField("Select L.FTReferCode from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH = '" & Me.FNFixedAssetType.Text & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH & FNHSysCmpRunId.Text & _Year & HI.TL.CboList.GetListRefer(FNPoState.Properties.Tag.ToString, FNPoState.SelectedIndex) & _Month).ToString
            Me.FTPurchaseState.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & " MANUAL " & HI.UL.ULDate.ConvertEN(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)) & " " & Format(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM), "HH:mm:ss")
        End If



        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
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
                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Str = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "
                    '_StateUpdate = True
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

            If (StateRcv) Then

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase "
                _Str &= vbCrLf & "  SET FTStateSendApp='0' "
                _Str &= vbCrLf & "  ,FTSendAppBy='' "
                _Str &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Str &= vbCrLf & "  ,FTSuperVisorName='' "
                _Str &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Str &= vbCrLf & "  ,FTManagerName='' "
                _Str &= vbCrLf & "  ,FTStatePDF='0' "
                _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                FTStateSendApp.Checked = False
                'FTStateSuperVisorApp.Checked = False
                FTStateManagerApp.Checked = False

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
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
#End Region



#Region "Event"

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub
        If CheckReceive(FTPurchaseNo.Text) Then
            Dim _RevisedRemark As String = ""
            Dim _CmpH As String = ""
            Dim _ReviseState As Boolean = False
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
            If FTPurchaseNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then
                If Me.VerrifyData() Then
                    If Me.SaveData() Then
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            Else
                If Me.FTPurchaseNo.Text = "" Then Exit Sub
                If CheckStateRevised() Then
                    With _RevisePopup
                        .FTRemark.Text = ""
                        .StateProc = False
                        .ocmok.Enabled = True
                        .ocmcancel.Enabled = True
                        .ShowDialog()

                        If .StateProc = False Then
                            Exit Sub
                        Else
                            _ReviseState = True
                            _RevisedRemark = .FTRemark.Text
                            Me.FTRemark.Text = _RevisedRemark
                        End If
                    End With
                End If
            End If

            With _AddItemPopup
                .AddComplete = False
                .PONO = FTPurchaseNo.Text
                HI.TL.HandlerControl.ClearControl(_AddItemPopup)
                ' .FNFixedAssetType.Text = Me.FNFixedAssetType.Text
                .ShowDialog()

                If (.AddComplete) Then
                    Dim Qry As String = ""
                    Dim _Str As String = ""
                    Dim _Type As String = ""
                    Dim _FNSeq As String = (HI.Conn.SQLConn.GetField("select max(FNSeq) AS FNSeq FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail WITH(NOLOCK) WHERE FTPurchaseNo='" & UL.ULF.rpQuoted(FTPurchaseNo.Text) & "'", Conn.DB.DataBaseName.DB_FIXED, "0") + 1)
                    'Dim _Asset As String = HI.Conn.SQLConn.GetField("SELECT A.FNHSysFixedAssetId FROM( SELECT FNHSysFixedAssetId ,FTProductCode,FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset  WITH(NOLOCK) UNION ALL SELECT P.FNHSysAssetPartId AS FNHSysFixedAssetId,P.FTProductCode,P.FNPrice FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) )AS A  WHERE FTProductCode='" & .FTAssetCode.Properties.Tag & "' AND FNPrice='" & .FNPrice.Value & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                    If ST.Lang.Language = ST.Lang.eLang.TH Then
                        _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH ='" & .FNFixedAssetType.Text & "'" 'Edit by joker 2017/06/30 from select top 1 To select max
                    Else
                        _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameEN ='" & .FNFixedAssetType.Text & "'"
                    End If
                    _Type = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")

                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        'Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail "
                        'Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,FTPurchaseNo, FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt,FTRemark)"
                        'Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        'Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & Val(_FNSeq) & "," & _Asset & "," & .FNHSysUnitAssetId.Properties.Tag & ""
                        'Qry &= vbCrLf & "," & .FNPrice.Value & "," & .FNDisPer.Value & "," & .FNDisAmt.Value & "," & .FNQuantity.Value & "," & .FNNetAmt.Value & ",'" & .FTRemark.Text & "'"
                        'If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail "
                        Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,FTPurchaseNo, FNSeq, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNDisPer, FNDisAmt, FNQuantity, FNNetAmt,FTRemark,FNFixedAssetType)"
                        Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & ",'" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'," & Val(_FNSeq) & "," & .FTAssetCode.Properties.Tag & "," & .FNHSysUnitAssetId.Properties.Tag & ""
                        Qry &= vbCrLf & "," & .FNPrice.Value & "," & .FNDisPer.Value & "," & .FNDisAmt.Value & "," & .FNQuantity.Value & "," & .FNNetAmt.Value & ",'" & .FTRemark.Text & "','" & _Type & "'"
                        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            Exit Sub
                        End If
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        FTStateSendApp.Checked = False
                        'FTStateSuperVisorApp.Checked = False
                        FTStateManagerApp.Checked = False

                        Qry = "select isnull(sum(convert(numeric(18,2),PD.FNNetAmt)),0) AS FNNetAmt"
                        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD WiTH(NOLOCK) where PD.FTPurchaseNo='" & FTPurchaseNo.Text & "'"
                        Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "0"))
                        Me.SaveData(_RevisedRemark, _ReviseState)

                        Me.LoadPoDetail(Me.FTPurchaseNo.Text)
                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End Try
                End If
            End With
        End If
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Dim _StateNotReState As Boolean
        Dim _Qry As String = ""
        Dim _RevisedRemark As String = ""
        If CheckOwner() = False Then Exit Sub
        If Me.FTPurchaseNo.Text <> "" Then
            If CheckReceive(Me.FTPurchaseNo.Text) = False Then Exit Sub
            If VerrifyData() Then
                If CheckStateRevised() Then
                    With _RevisePopup
                        .FTRemark.Text = ""
                        .StateProc = False
                        .ocmok.Enabled = True
                        .ocmcancel.Enabled = True
                        .ShowDialog()
                        If .StateProc Then
                            _RevisedRemark = .FTRemark.Text
                            Me.FTRemark.Text = _RevisedRemark
                        Else
                            Exit Sub
                        End If
                    End With
                End If
                If _RevisedRemark <> "" Then
                    _Qry = "  SELECT TOP 1 FTPurchaseNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS A WITH(NOLOCK) "
                    _Qry &= vbCrLf & "WHERE FTPurchaseNo=N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                    _Qry &= vbCrLf & "AND  ( FNPOGrandAmt<>" & FNPOGrandAmt.Value & ""
                    _Qry &= vbCrLf & "OR FNExchangeRate<>" & FNExchangeRate.Value & ")"
                    _StateNotReState = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FIXED, "") = "")
                End If

                If Me.SaveData(_RevisedRemark, _StateNotReState) Then
                    Me.LoadDataInfo(Me.FTPurchaseNo.Text)
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If

        End If

    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpId.EditValueChanged
        Dim joke As String = Me.FNHSysCmpId.Text
    End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged
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

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisCountPer.EditValueChanged,
                                                                                  FNPoAmt.EditValueChanged,
                                                                                  FNDisCountAmt.EditValueChanged,
                                                                                  FNVatPer.EditValueChanged,
                                                                                  FNVatAmt.EditValueChanged,
                                                                                  FNSurcharge.EditValueChanged,
                                                                                   FNTaxPer.EditValueChanged,
                                                                                  FNTaxAmt.EditValueChanged

        Static _Proc As Boolean

        'If Not (_Proc) And Not (_ProcLoad) Then
        _Proc = True
        Dim _POAmt As Double = FNPoAmt.Value

        If _POAmt = 0 Then
            FNDisCountAmt.Value = 0
            FNVatAmt.Value = 0
        End If
        Dim _FNAmt As Double = FNAmt.Value

        If _FNAmt = 0 Then
            FNTaxAmt.Value = 0
            FNVatAmt.Value = 0
        End If

        Dim _DisPer As Double = FNDisCountPer.Value
        Dim _DisAmt As Double = FNDisCountAmt.Value
        Dim _TaxPer As Double = FNTaxPer.Value
        Dim _TaxAmt As Double = FNTaxAmt.Value
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
            Case "FNTaxPer".ToUpper
                _TaxPer = FNTaxPer.Value
                _TaxAmt = Format((_FNAmt * _TaxPer) / 100, HI.ST.Config.AmtFormat)
                FNTaxAmt.Value = _TaxAmt
            Case "FNTaxAmt".ToUpper
                _TaxAmt = FNTaxAmt.Value

                If _POAmt > 0 Then
                    _TaxPer = Format((_TaxAmt * 100) / _FNAmt, HI.ST.Config.PercentFormat)
                Else
                    _TaxPer = 0
                End If
                FNTaxPer.Value = _TaxPer
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
        Me.FNAmt.Value = (_POAmt - _DisAmt)
        'Me.FNPONetAmt.Value = (_FNAmt - _TaxAmt)

        If Me.FNDisCountPer.Text = "0.00000" Then
            Me.FNPONetAmt.Value = (_POAmt - _TaxAmt)
        Else
            Me.FNPONetAmt.Value = (_FNAmt - _TaxAmt)
        End If



        Select Case sender.Name.ToString.ToUpper
            Case "FNDisCountPer".ToUpper, "FNDisCountAmt".ToUpper
                _VatPer = FNVatPer.Value
                _VatAmt = Format(((_POAmt - _DisAmt) * _VatPer) / 100, HI.ST.Config.AmtFormat)
                FNVatAmt.Value = _VatAmt
        End Select

        FNPOGrandAmt.Value = Format(Me.FNPONetAmt.Value + FNVatAmt.Value + _SurAmt, HI.ST.Config.AmtFormat)
        Me.FTPOGrandAmtEN.Text = HI.UL.ULF.Convert_Bath_EN(FNPOGrandAmt.Value)
        Me.FTPOGrandAmtTH.Text = HI.UL.ULF.Convert_Bath_TH(FNPOGrandAmt.Value)
        _Proc = False
        ' End If
    End Sub

    Private Sub wPurchaserAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _FormLoad = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If FTPurchaseNo.Text = "" Then Exit Sub
        If CheckOwner() = False Then Exit Sub
        If CheckReceive(Me.FTPurchaseNo.Text) Then
            If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTPurchaseNo.Text, Me.Text) = True Then
                If Me.DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                    Me.DefaultsData()
                    Me.FTPurchaseNo.Focus()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If

    End Sub


    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail "
            _Str &= vbCrLf & "  SET FTPurchaseRefNo='' WHERE FTPurchaseRefNo = '" & FTPurchaseNo.Text & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_FIXED)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'")
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        If ogvdetail.RowCount = 0 Or ogvdetail.FocusedRowHandle <= -1 Then Exit Sub
        If CheckReceive(FTPurchaseNo.Text, ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysFixedAssetId")) Then
            Dim _FNSeq As Integer = 0
            Dim Qry As String = ""
            Dim _RevisedRemark As String = ""
            Dim _ReviesedState As Boolean = False
            If CheckStateRevised() Then
                With _RevisePopup
                    .FTRemark.Text = ""
                    .StateProc = False
                    .ocmok.Enabled = True
                    .ocmcancel.Enabled = True
                    .ShowDialog()

                    If .StateProc = False Then
                        Exit Sub
                    Else
                        _RevisedRemark = .FTRemark.Text
                        _ReviesedState = True
                        Me.FTRemark.Text = _RevisedRemark
                    End If
                End With
            End If
            With _AddItemPopup
                .AddComplete = False
                TL.HandlerControl.ClearControl(_AddItemPopup)
                Try
                    _FNSeq = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNSeq").ToString)
                    .FTAssetCode.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetCode").ToString
                    .FNHSysFixedAssetId_None.Text = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTAssetName").ToString
                    .FNPrice.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNPrice").ToString)
                    .FNDisPer.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisPer").ToString)
                    .FNDisAmt.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNDisAmt").ToString)
                    .FTRemark.Text = (ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTRemark").ToString)
                    .FNHSysUnitAssetId.Text = (ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FTUnitCode").ToString)
                    .FNQuantity.Value = Val(ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNQuantity").ToString)
                    .FTAssetCode.Properties.Tag = ogvdetail.GetRowCellValue(ogvdetail.FocusedRowHandle, "FNHSysFixedAssetId")
                Catch ex As Exception

                End Try

                .ShowDialog()
                If .AddComplete Then
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


                        Dim _Str As String = ""
                        Dim _Type As String = ""
                        If ST.Lang.Language = ST.Lang.eLang.TH Then
                            _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH ='" & .FNFixedAssetType.Text & "'" 'Edit by joker 2017/06/30 from select top 1 To select max
                        Else
                            _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameEN ='" & .FNFixedAssetType.Text & "'"
                        End If
                        _Type = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")


                        Qry = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail "
                        Qry &= vbCrLf & "SET FTUpdUser='" & ST.UserInfo.UserName & "', FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & ", FNHSysUnitId=" & .FNHSysUnitAssetId.Properties.Tag & ", FNPrice=" & .FNPrice.Value & ", FNDisPer=" & .FNDisPer.Value & ", FNDisAmt=" & .FNDisAmt.Value & ""
                        Qry &= vbCrLf & ", FNQuantity=" & .FNQuantity.Value & ", FNNetAmt=" & .FNNetAmt.Value & ",FTRemark='" & .FTRemark.Text & "',FNFixedAssetType=" & _Type & ""
                        Qry &= vbCrLf & "where FTPurchaseNo='" & FTPurchaseNo.Text & "' and FNSeq=" & _FNSeq & " and FNHSysFixedAssetId=" & .FTAssetCode.Properties.Tag & ""
                        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Qry = "select isnull(sum(convert(numeric(18,2),PD.FNNetAmt)),0) AS FNNetAmt"
                        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD WiTH(NOLOCK) where PD.FTPurchaseNo='" & FTPurchaseNo.Text & "'"
                        Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "0"))
                        Me.SaveData(_RevisedRemark, _ReviesedState)
                        Me.LoadPoDetail(Me.FTPurchaseNo.Text)

                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End Try
                End If

            End With
        End If
    End Sub

    Private Sub ocmremove_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        If CheckOwner() = False Then Exit Sub
        Dim Qry As String = ""
        Dim _FNSeq As Integer = 0
        Dim _IDAsset As Integer = 0
        Dim _RevisedRemark As String = ""
        Dim _ReviseState As Boolean = False
        Try
            With ogvdetail
                _FNSeq = Val(.GetRowCellValue(.FocusedRowHandle, "FNSeq").ToString)
                _IDAsset = Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString)
            End With
            If CheckReceive(Me.FTPurchaseNo.Text, _IDAsset) Then
                If CheckStateRevised() Then
                    With _RevisePopup
                        .FTRemark.Text = ""
                        .StateProc = False
                        .ocmok.Enabled = True
                        .ocmcancel.Enabled = True
                        .ShowDialog()
                        If .StateProc = False Then
                            Exit Sub
                        Else
                            _ReviseState = True
                            _RevisedRemark = .FTRemark.Text
                            Me.FTRemark.Text = _RevisedRemark
                        End If
                    End With
                End If
                Qry = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail where FTPurchaseNo='" & FTPurchaseNo.Text & "' and FNSeq=" & _FNSeq & " and FNHSysFixedAssetId=" & _IDAsset & ""
                HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_FIXED)
                Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail "
                Qry &= vbCrLf & "  SET FTPurchaseRefNo='' WHERE FTPurchaseRefNo = '" & FTPurchaseNo.Text & "' AND FNHSysFixedAssetId=" & _IDAsset & ""
                HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_FIXED)
                Qry = "select isnull(sum(convert(numeric(18,2),PD.FNNetAmt)),0) AS FNNetAmt"
                Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS PD WiTH(NOLOCK) where PD.FTPurchaseNo='" & FTPurchaseNo.Text & "'"
                Me.FNPoAmt.Value = Val(HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "0"))
                Me.SaveData(_RevisedRemark, _ReviseState)
                Me.LoadPoDetail(Me.FTPurchaseNo.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click
        If CheckOwner() = False Then Exit Sub
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
            Dim _Qry As String = ""
            _Qry = "Select  TOP  1  FTStateSendApp  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' AND FTStateSuperVisorApp<>'2' AND FTStateManagerApp<>'2' "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_FIXED, "") <> "1" Then

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase "
                _Qry &= vbCrLf & "  SET FTStateSendApp='1' "
                _Qry &= vbCrLf & " , FTSendAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " , FTSendAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "  ,FTSendAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & "  ,FTStateSuperVisorApp='0' "
                _Qry &= vbCrLf & "  ,FTSuperVisorName='' "
                _Qry &= vbCrLf & "  ,FTStateManagerApp='0' "
                _Qry &= vbCrLf & "  ,FTStatePDF='0' "
                _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_FIXED)

            End If
            FTStateSendApp.Checked = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub

    Private Function CheckReceive(POKey As String, Optional _IDAsset As Integer = 0) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""
        If _IDAsset = 0 Then
            _Str = "Select TOP 1 FTPurchaseNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive As R WITH(NOLOCK) WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "
            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "" Then
                MG.ShowMsg.mProcessError(201610261712, "มีการรับสินทรัพย์แล้ว", Me.Text, MessageBoxIcon.Information)
                _Pass = False
            End If
        Else
            _Str = "Select TOP 1 H.FTPurchaseNo "
            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive As H WITH(NOLOCK), [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS D "
            _Str &= vbCrLf & " WHERE H.FTReceiveNo= D.FTReceiveNo AND H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "
            _Str &= vbCrLf & " AND FNHSysFixedAssetId=" & _IDAsset & ""
            If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "") <> "" Then
                MG.ShowMsg.mProcessError(201610261712, "มีการรับสินทรัพย์แล้ว", Me.Text, MessageBoxIcon.Information)
                _Pass = False
            End If
        End If

        Return _Pass
    End Function

    Private Function CheckStateRevised() As Boolean
        Dim Qry As String = ""
        Qry = "select top 1 FTStateManagerApp"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase AS A WITH(NOLOCK)"
        Qry &= vbCrLf & "WHERE FTPurchaseNo='" & UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
        Return (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED, "") = "1")
    End Function

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Me.FTPurchaseNo.Text <> "" And Me.FTPurchaseNo.Properties.Tag.ToString <> "" Then
            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If Me.FNPoState.SelectedIndex = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseAsset\"
                .ReportName = "PurchaseAsset.rpt"
                '.AddParameter("Draft", "DRAFT")
                .Formular = "{TFIXEDTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTPurchaseNo_lbl.Text)
            FTPurchaseNo.Focus()
        End If
    End Sub



    Private Sub ogvdetail_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle

        With ogvdetail
            If .GetRowCellValue(e.RowHandle, "FTStateRcv") = "1" Then
                e.Appearance.ForeColor = System.Drawing.Color.Green
            End If
        End With

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadDataInfo(Me.FTPurchaseNo.Text)
    End Sub

    Private Sub ogvpricehistory_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvpricehistory.RowCellStyle
        Try
            With ogvpricehistory
                If "" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString <> "" Then
                    If IsNumeric("" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString) Then
                        If Not (e.Appearance.ForeColor = System.Drawing.Color.Red) Then
                            If CDbl("" & .GetRowCellValue(e.RowHandle, "FNNetPriceBF").ToString) < CDbl("" & .GetRowCellValue(e.RowHandle, "FNNetPrice").ToString) Then
                                e.Appearance.ForeColor = System.Drawing.Color.Red
                            Else
                                e.Appearance.ForeColor = System.Drawing.Color.Green
                            End If
                        End If
                    End If
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub


  
    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSuplId.EditValueChanged

    End Sub
End Class