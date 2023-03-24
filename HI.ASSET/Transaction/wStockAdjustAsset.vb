Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Columns
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Base

Public Class wStockAdjustAsset
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")
    Private _AddItemPopup As wStockAdjustAddItemAsset
    Private _GenBarcode As wGenerateBarcodeAsset
    Private _AddMasterpop As wAdditemMasterPop
    Private _EditCell As Boolean = False
    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        _AddItemPopup = New wStockAdjustAddItemAsset
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        _GenBarcode = New wGenerateBarcodeAsset
        TL.HandlerControl.AddHandlerObj(_GenBarcode)

        _AddMasterpop = New wAdditemMasterPop
        TL.HandlerControl.AddHandlerObj(_AddMasterpop)


        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
            Call oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, _GenBarcode.Name.ToString.Trim, _GenBarcode)
            Call oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, _AddMasterpop.Name.ToString.Trim, _AddMasterpop)
        Catch ex As Exception
        Finally
        End Try




        '_GenBarcode = New wGenerateBarcodeInven


        'Try

        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _GenBarcode.Name.ToString.Trim, _GenBarcode)
        'Catch ex As Exception
        'Finally
        'End Try

        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

        With ogvadjdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

        With ogvbarcode
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True

        End With

        Call TabChange()
    End Sub


    Private Sub TabChange()
        ocmsave.Visible = (otbdetail.SelectedTabPage.Name = otbadjin.Name)
        ocmdelete.Visible = (otbdetail.SelectedTabPage.Name = otbadjin.Name)
        ocmadd.Visible = (otbdetail.SelectedTabPage.Name = otbadjin.Name)
        ocmremove.Visible = (otbdetail.SelectedTabPage.Name = otbadjin.Name)

        ocmdeletebarcode.Visible = (otbdetail.SelectedTabPage.Name = otbadjout.Name)

        ocmgenbarcode.Visible = (otbdetail.SelectedTabPage.Name = otbnewbarcode.Name)

        ocmdeletebarcodegen.Visible = (otbdetail.SelectedTabPage.Name = otbnewbarcode.Name)

        ' ocmaddmaster.Visible = (otbdetail.SelectedTabPage.Name = otbnewbarcode.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

#Region "Property"

    Private _WHID As Integer
    Public Property WH As Integer
        Get
            Return _WHID
        End Get
        Set(value As Integer)
            _WHID = value
        End Set
    End Property

    Private _WHIDTo As Integer
    Public Property WHTo As Integer
        Get
            Return _WHIDTo
        End Get
        Set(value As Integer)
            _WHIDTo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _OrderNoTo As String = ""
    Public Property OrderNoTo As String
        Get
            Return _OrderNoTo
        End Get
        Set(value As String)
            _OrderNoTo = value
        End Set
    End Property

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _DocRefNo As String = ""
    Public Property DocRefNo As String
        Get
            Return _DocRefNo
        End Get
        Set(value As String)
            _DocRefNo = value
        End Set
    End Property

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

        Call LoadIssueDetail(Key.ToString)
        Call LoadAdjDetail(Key.ToString)
        Call LoadBarcode(Key.ToString)
        Me.otbdetail.SelectedTabPageIndex = 0
        _ProcLoad = False
    End Sub

    Private Sub LoadIssueDetail(Key As String)

        ogcdetail.DataSource = BarcodeAsset.LoadDocumentBarcode(FTAdjustStockNo.Text, BarcodeAsset.DocType.Adjust)

    End Sub

    Private Sub LoadAdjDetail(Key As String)
        Dim _Str As String = ""
        Dim FNFixedAssetType As String = ""
        'Dim _PO As String = ""
        'If   Then
        '    With DirectCast(Me.ogcadjdetail.DataSource, DataTable)
        '        .AcceptChanges()

        '        For Each R As DataRow In .Rows
        '            If _PO <> "" Then _PO &= ","
        '            _PO &= "" & R!FNFixedAssetType.ToString & ""
        '        Next
        '    End With
        'With _AddItemPopup


        '    .AssetComplete = False
        '    .DocNo = FTAdjustStockNo.Text
        '    .FTPurchaseNo.ReadOnly = False
        '    HI.TL.HandlerControl.ClearControl(_AddItemPopup)
        '    ' .FNFixedAssetType.Text = Me.FNFixedAssetType.Text





        '    FNFixedAssetType = HI.Conn.SQLConn.GetField("select L.FNListIndex   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and L.FTNameTH='" & .FNFixedAssetType.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

        '    If FNFixedAssetType <> 1 Then
        _Str = "select D.FTPurchaseNo,D.FNPrice,D.FNQuantity,ISNULL(A.FTAssetCode,AP.FTAssetPartCode)AS FTAssetCode,D.FNHSysFixedAssetId,D.FNHSysUnitId,A.FNHSysAssetModelId,ISNULL(A.FNHSysAssetBrandId,AP.FNHSysAssetBrandId)AS FNHSysAssetBrandId"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & ",ISNULL(A.FTAssetNameTH,AP.FTAssetPartNameTH) AS FTAssetName,U.FTUnitAssetNameTH AS FTUnitCode,L.FTNameTH AS FNFixedAssetType"
        Else
            _Str &= vbCrLf & ",ISNULL(A.FTAssetNameEN,AP.FTAssetPartNameEN) AS FTAssetName,U.FTUnitAssetNameEN AS FTUnitCode,L.FTNameEN AS FNFixedAssetType"
        End If
        _Str &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail AS D WITH(NOLOCK) LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) ON D.FNHSysFixedAssetId=A.FNHSysFixedAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON D.FNHSysFixedAssetId=AP.FNHSysAssetPartId LEFT OUTER Join"
        _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON D.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUTER JOIN"
        _Str &= vbCrLf & "(SELECT L.FTNameTH ,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName ='FNFixedAssetType')AS L   ON D.FNFixedAssetType=L.FNListIndex"
        _Str &= vbCrLf & "where D.FTAdjustStockNo='" & Key & "'"
        Me.ogcadjdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        'Else
        '    _Str = "select D.FTPurchaseNo,D.FNPrice,D.FNQuantity,A.FTAssetPartCode AS FTAssetCode,D.FNHSysFixedAssetId,D.FNHSysUnitId,'-' AS FNHSysAssetModelId,A.FNHSysAssetBrandId"
        '    If ST.Lang.Language = ST.Lang.eLang.TH Then
        '        _Str &= vbCrLf & ",A.FTAssetPartNameTH AS FTAssetName,U.FTUnitAssetNameTH AS FTUnitCode,L.FTNameTH AS FNFixedAssetType"
        '    Else
        '        _Str &= vbCrLf & ",A.FTAssetPartNameEN AS FTAssetName,U.FTUnitAssetNameEN AS FTUnitCode,L.FTNameEN AS FNFixedAssetType"
        '    End If
        '    _Str &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail AS D WITH(NOLOCK) LEFT OUTER JOIN"
        '    _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS A WItH(NOLOCK) ON D.FNHSysFixedAssetId=A.FNHSysAssetPartId LEFT OUTER JOIN"
        '    _Str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset AS U WITH(NOLOCK) ON D.FNHSysUnitId=U.FNHSysUnitAssetId LEFT OUTER JOIN"
        '    _Str &= vbCrLf & "(SELECT L.FTNameTH ,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName ='FNFixedAssetType')AS L   ON D.FNFixedAssetType=L.FNListIndex"
        '    _Str &= vbCrLf & "where D.FTAdjustStockNo='" & Key & "'"
        '    Me.ogcadjdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)
        'End If

        'End With

    End Sub

    Public Sub LoadBarcode(Key As String)
        Dim dt As DataTable = ASSET.BarcodeAsset.GetBarcode(Key)
        'Me.ogcbarcode.DataSource = HI.ASSET.BarcodeAsset.GetBarcode(Key)
        If dt.Rows.Count > 0 Then
            Me.ogcbarcode.DataSource = dt
        Else
            Call PrepareListBarcode()
        End If
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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
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
                                Dim _CmpH As String = "0"
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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust WHERE FTAdjustStockNo='" & HI.UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail WHERE FTAdjustStockNo='" & HI.UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '_Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"

            'HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust WHERE FTAdjustStockNo='" & HI.UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'")

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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

#End Region

#Region "MAIN PROC"


    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTAdjustStockBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTAdjustStockBy.Text) & "' "
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

        If BarcodeAsset.CheckDucumentCreateBar(FTAdjustStockNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTAdjustStockNo.Text, Me.Text) = False Then
            Exit Sub
        End If
        If Me.DeleteData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()

        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTAdjustStockNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "InventoryAsset\"
                .ReportName = "AdjustStockAsset.rpt"
                .Formular = "{TFIXEDTAdjust.FTAdjustStockNo}='" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "' "
                .Preview()
            End With

            'With New HI.RP.Report
            '    .FormTitle = Me.Text
            '    .ReportFolderName = "Inventrory\"
            '    .ReportName = "AdjustStockSlip_Barcode.rpt"
            '    .Formular = "{TINVENAdjustStock.FTAdjustStockNo}='" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "' "
            '    .Preview()
            'End With


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTAdjustStockNo_lbl.Text)
            FTAdjustStockNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Function SaveBarcode() As Boolean
        Dim _Str As String
        Dim _BarCode As String = FTBarcodeNo.Text
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT "
            _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
            _Str &= vbCrLf & ",FNHSysWHAssetId=" & Val("" & FNHSysWHAssetId.Properties.Tag.ToString) & " "
            _Str &= vbCrLf & ",FNQuantity=" & FNQuantityBal.Value & " "
            _Str &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "' "
            _Str &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
            _Str &= vbCrLf & " AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity, FTDocumentRefNo, FNHSysCmpId)  "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_BarCode) & "' "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "' "
                _Str &= vbCrLf & "," & Val("" & FNHSysWHAssetId.Properties.Tag.ToString) & " "
                _Str &= vbCrLf & "," & FNQuantityBal.Value & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
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

    Private Function DeleteBarcode(BarcodeKey As String) As Boolean
        Dim _Str As String
        Dim _BarCode As String = BarcodeKey
        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Str = " DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "' AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'"

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
    Private Sub Genbarcode()
        Dim sDate As String = Year(Date.Today) & "" & Month(Date.Today).ToString.PadLeft(2, "0") & "" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
        If Year(Date.Today) > 2400 Then
            sDate = Year(Date.Today) - 543 & "" & Month(Date.Today).ToString.PadLeft(2, "0") & "" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
        End If
        'Dim _PO As String = ""

        'With DirectCast(Me.ogcadjdetail.DataSource, DataTable)
        '    .AcceptChanges()

        '    For Each R As DataRow In .Rows
        '        If _PO <> "" Then _PO &= ","
        '        _PO &= "" & R!FNFixedAssetType.ToString & ""
        '    Next
        'End With
        'sDate = sDate.Substring(2)
        'If _PO <> "อะไหล่จักร" Then
        '    Dim Qry As String = "select B.FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNHSysFixedAssetId=" & Val(_IDasset) & ""
        '    Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
        '    Dim _RunNumber As String = ""

        '    Qry = "SELECT G.FTAssetPartGroupCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS G WITH(NOLOCK) ON P.FNHSysAssetPartGrpId = G.FNHSysAssetPartGrpId WHERE P.FNHSysAssetPartId = " & Val(_IDasset) & ""
        '    Dim _AssetPart As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
        '    _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTBarcodeNo) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS A WITH(NOLOCK) where A.FTBarcodeNo like '%" & _AssetType & sDate & "%'", Conn.DB.DataBaseName.DB_FIXED)
        '    If _AssetPart = "" Then _AssetPart = "1"
        '    If _AssetType = "" Then _AssetType = _AssetPart
        '    If _RunNumber = "" Then
        '        _RunNumber = "0001"
        '    Else
        '        _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 4)
        '        _RunNumber = Format(Val(_RunNumber) + 1, "0000")
        '    End If
        '    _BarCodeGen = _AssetType + sDate + _RunNumber
        'Else
        '    Dim Qry As String = "select '1'AS FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS B WITH(NOLOCK) where B.FNHSysAssetPartId=" & Val(_IDasset) & ""
        '    Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
        '    Dim _RunNumber As String = ""

        '    Qry = "SELECT G.FTAssetPartGroupCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) INNER JOIN"
        '    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS G WITH(NOLOCK) ON P.FNHSysAssetPartGrpId = G.FNHSysAssetPartGrpId WHERE P.FNHSysAssetPartId = " & Val(_IDasset) & ""
        '    Dim _AssetPart As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
        '    _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTBarcodeNo) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS A WITH(NOLOCK) where A.FTBarcodeNo like '%" & _AssetType & sDate & "%'", Conn.DB.DataBaseName.DB_FIXED)
        '    If _AssetPart = "" Then _AssetPart = "1"
        '    If _AssetType = "" Then _AssetType = _AssetPart
        '    If _RunNumber = "" Then
        '        _RunNumber = "0001"
        '    Else
        '        _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 4)
        '        _RunNumber = Format(Val(_RunNumber) + 1, "0000")
        '    End If
        '    _BarCodeGen = _AssetType + sDate + _RunNumber

        'End If
        sDate = sDate.Substring(2)
        Dim Qry As String = "select B.FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNHSysFixedAssetId=" & Val(_IDasset) & ""
        Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
        Dim _RunNumber As String = ""

        Qry = "SELECT G.FTAssetPartGroupCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS G WITH(NOLOCK) ON P.FNHSysAssetPartGrpId = G.FNHSysAssetPartGrpId WHERE P.FNHSysAssetPartId = " & Val(_IDasset) & ""
        Dim _AssetPart As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
        _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTBarcodeNo) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS A WITH(NOLOCK) where A.FTBarcodeNo like '%" & _AssetType & sDate & "%'", Conn.DB.DataBaseName.DB_FIXED)
        If _AssetPart = "" Then _AssetPart = "1"
        If _AssetType = "" Then _AssetType = _AssetPart
        If _RunNumber = "" Then
            _RunNumber = "0001"
        Else
            _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 4)
            _RunNumber = Format(Val(_RunNumber) + 1, "0000")
        End If
        _BarCodeGen = _AssetType + sDate + _RunNumber


    End Sub
    Private Function GenBarc() As Boolean
        Dim _Str As String = ""
        Try
            For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Rows
                _Str = "select top 1 A.FTAssetCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FNFixedAssetType=0 and A.FNHSysFixedAssetId=" & R!FNHSysFixedAssetId & ""
                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED) = "" Then
                    Try
                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        _Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                        _Str &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                        _Str &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & R!FTAssetCode.ToString & "'," & Me.FNHSysWHAssetId.Properties.Tag & ",'" & Val(R!FNHSysFixedAssetId.ToString) & "'," & Val(R!FNHSysUnitId.ToString) & ""
                        _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & Val(R!FNQuantity.ToString) & ",'" & Me.FTAdjustStockNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Return False
                        End If

                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                        _Str &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & R!FTAssetCode.ToString & "','" & Me.FTAdjustStockNo.Text & "','" & Me.FNHSysWHAssetId.Properties.Tag & "'," & Val(R!FNQuantity.ToString) & ",'" & Me.FTAdjustStockNo.Text & "'," & ST.SysInfo.CmpID & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Return False
                        End If
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Catch ex As Exception
                        MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End Try

                End If

            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function VerifyBarcode() As Boolean
        Dim Qry As String = ""
        Try
            For Each R As DataRow In CType(ogcbarcode.DataSource, DataTable).Rows
                Qry = "select B.FTBarcodeNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH(NOLOCK)"
                Qry &= vbCrLf & "where B.FTBarcodeNo='" & R!FTBarcodeNo.ToString & "' and B.FTDocumentNo='" & Me.FTAdjustStockNo.Text & "'"
                If (HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) <> "") Then
                Else
                    Return True
                    Exit For
                End If
            Next
            MG.ShowMsg.mInfo("มีการสร้างบาร์โค๊ดแล้ว!!!", 201709221337, Me.Text)
            Return False
        Catch ex As Exception
            MG.ShowMsg.mInfo("เกิดข้อผิดพลาดบางประการ กรุณาติดต่อ ADMIN!", 201709221404, Me.Text)
            Return False
        End Try
    End Function
#End Region



    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter

                If FTBarcodeNo.Text <> "" Then

                    If FTAdjustStockNo.Properties.Tag.ToString = "" Then
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTAdjustStockNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTAdjustStockNo.Text)
                    End If

                    Call AddBarCode(FTBarcodeNo.Text, (FNQuantityBal.Properties.ReadOnly))

                End If
                Me.otbdetail.SelectedTabPageIndex = 2
        End Select
    End Sub


    Private Sub AddBarCode(BarcodeNo As String, StateAdd As Boolean, Optional Qty As Double = 0)
        If CheckOwner() = False Then Exit Sub

        If SaveBarcode() Then
            LoadIssueDetail(Me.FTAdjustStockNo.Text)
            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
            FNQuantityBal.Value = 0

        End If

    End Sub

    Private Sub FNQuantityBal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNQuantityBal.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If FTBarcodeNo.Text <> "" Then

                    If FTAdjustStockNo.Properties.Tag.ToString = "" Then
                        If Me.VerrifyData() Then
                            If Me.SaveData Then
                            Else
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        If Me.FTAdjustStockNo.Text = "" Then Exit Sub
                        LoadDataInfo(Me.FTAdjustStockNo.Text)
                    End If

                    Call AddBarCode(FTBarcodeNo.Text, True, FNQuantityBal.Value)
                Else
                    FTBarcodeNo.Focus()
                    FTBarcodeNo.SelectAll()
                End If
                Me.otbdetail.SelectedTabPageIndex = 2
        End Select
    End Sub


    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        If ocmdeletebarcode.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Delete
                Call DeleteBarcode()
        End Select
    End Sub

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcodegen.Click
        Call DeleteBarcodeGen()
    End Sub

    Private Sub DeleteBarcodeGen()
        If CheckOwner() = False Then Exit Sub
        Try
            With ogvbarcode
                If .RowCount <= 0 Then Exit Sub
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                Dim _StateDelete As Boolean = False
                For Each i As Integer In .GetSelectedRows()

                    Dim _BarCode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString
                    Dim _WHID As Integer = Me.FNHSysWHAssetId.Properties.Tag

                    If BarcodeAsset.CheckTransactionOUT(_BarCode, Me.FTAdjustStockNo.Text, _WHID) Then
                        HI.MG.ShowMsg.mInvalidData("Barcode มีการเดิน Transaction  ลบ หรือแก้ไขได้ !!!", 1311240006, Me.Text, _BarCode)
                    Else
                        Dim _Str As String = ""
                        Try
                            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                            HI.Conn.SQLConn.SqlConnectionOpen()
                            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



                            _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  "
                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                Continue For
                            End If

                            _Str = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN   WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'  "
                            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Continue For
                            End If


                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Dim _ID As String = "" : Dim _IDMaster As String = ""
                            _ID = .GetRowCellValue(i, "FNHSysFixedAssetId").ToString
                            _Str = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType<>6 and B.FNFixedAssetType<>7 and B.FNHSysFixedAssetId=" & _ID & ""
                            _IDMaster = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER)
                            If _IDMaster = "" Then
                                .SetRowCellValue(i, "FTBarcodeNo", "")
                            Else
                                _Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId=" & _IDMaster & ""
                                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_FIXED) Then
                                    CType(ogcbarcode.DataSource, DataTable).Rows(i).Delete()
                                    CType(ogcbarcode.DataSource, DataTable).AcceptChanges()
                                End If
                            End If
                            '_Str = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset where FTSerialNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "'"
                            'HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MASTER)
                            '_StateDelete = True
                            ' Exit Sub
                        Catch ex As Exception


                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Continue For
                        End Try
                    End If
                Next

                If _StateDelete Then
                    Me.LoadBarcode(Me.FTAdjustStockNo.Text)
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DeleteBarcode()
        If CheckOwner() = False Then Exit Sub

        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTBarcodeNo").ToString

                If _Barcode <> "" Then
                    If DeleteBarcode(_Barcode) Then
                        _StateDelete = True

                    End If
                End If
            Next

            If (_StateDelete) Then
                FTBarcodeNo.Focus()
                FTBarcodeNo.SelectAll()
                FNQuantityBal.Value = 0
                LoadIssueDetail(Me.FTAdjustStockNo.Text)
            End If

        End With
    End Sub

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click
        If CheckOwner() = False Then Exit Sub


        If BarcodeAsset.CheckDucumentCreateBar(FTAdjustStockNo.Text) Then
            'HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            'Exit Sub
        End If

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
        If FTAdjustStockNo.Text = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, Me.SysDocType, True, _CmpH) Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            If Me.FTAdjustStockNo.Text = "" Then Exit Sub

        End If

        With _AddItemPopup
            .AssetComplete = False
            .DocNo = FTAdjustStockNo.Text
            .FTPurchaseNo.ReadOnly = False
            HI.TL.HandlerControl.ClearControl(_AddItemPopup)
            .FNFixedAssetType.Text = Me.FNFixedAssetType.Text
            .ShowDialog()

            If (.AssetComplete) Then

                Dim _Str As String = ""
                'Dim _FNFixedAssetType As String = ""
                '_FNFixedAssetType = HI.Conn.SQLConn.GetField("select L.FNListIndex   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and L.FTNameTH='" & .FNFixedAssetType.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                Try
                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                    _Str = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail SET"
                    _Str &= vbCrLf & "FTUpdUser='" & ST.UserInfo.UserName & "', FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ", FNHSysUnitId='" & .FNHSysUnitAssetId.Properties.Tag & "', FNPrice=" & .FNPrice.Value & ", FNQuantity=" & .FNQuantity.Value & ""
                    _Str &= vbCrLf & "where FTAdjustStockNo='" & UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "' and FNHSysFixedAssetId='" & .FTAssetCode.Properties.Tag & "' and FTPurchaseNo='" & UL.ULF.rpQuoted(.FTPurchaseNo.Text) & "'"
                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail (FTInsUser, FDInsDate, FTInsTime,FTAdjustStockNo, FNHSysFixedAssetId, FTPurchaseNo, FNHSysUnitId, FNPrice, FNQuantity)"
                        _Str &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ",'" & UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"
                        _Str &= vbCrLf & ",'" & .FTAssetCode.Properties.Tag & "','" & UL.ULF.rpQuoted(.FTPurchaseNo.Text) & "','" & .FNHSysUnitAssetId.Properties.Tag & "'," & .FNPrice.Value & "," & .FNQuantity.Value & ""
                        If HI.Conn.SQLConn.Execute_Tran(_Str, Conn.SQLConn.Cmd, Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Exit Sub
                        End If
                    End If
                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Me.LoadAdjDetail(Me.FTAdjustStockNo.Text)
                    Call PrepareListBarcode()
                    'If VerifyBarcode() Then
                    '    'If GenBarcode() Then
                    '    '    LoadBarcode(Me.FTAdjustStockNo.Text)
                    '    'End If
                    'End If

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try

            End If
        End With
    End Sub

    Private Sub ogvadjdetail_DoubleClick(sender As Object, e As System.EventArgs) Handles ogvadjdetail.DoubleClick
        With ogvadjdetail
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            If BarcodeAsset.CheckDucumentCreateBar(FTAdjustStockNo.Text) Then
                HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
                Exit Sub
            End If

            With _AddItemPopup
                .AssetComplete = False
                .DocNo = FTAdjustStockNo.Text
                .FTPurchaseNo.ReadOnly = True
                HI.TL.HandlerControl.ClearControl(_AddItemPopup)
                Try
                    .FTAssetCode.Text = ogvadjdetail.GetFocusedRowCellValue("FTAssetCode").ToString
                    .FNHSysUnitAssetId.Text = ogvadjdetail.GetFocusedRowCellValue("FTUnitCode").ToString
                    .FNPrice.Value = ogvadjdetail.GetFocusedRowCellValue("FNPrice").ToString
                    .FTPurchaseNo.Text = ogvadjdetail.GetFocusedRowCellValue("FTPurchaseNo").ToString
                    .FNQuantity.Value = ogvadjdetail.GetFocusedRowCellValue("FNQuantity").ToString
                Catch ex As Exception
                    Exit Sub
                End Try
                .ShowDialog()

                If (.AssetComplete) Then
                    Dim _Str As String = ""
                    Try
                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                        _Str = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail SET"
                        _Str &= vbCrLf & "FTUpdUser='" & ST.UserInfo.UserName & "', FDUpdDate=" & UL.ULDate.FormatDateDB & ", FTUpdTime=" & UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ", FNHSysUnitId='" & .FNHSysUnitAssetId.Properties.Tag & "', FNPrice=" & .FNPrice.Value & ", FNQuantity=" & .FNQuantity.Value & ""
                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            _Str = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail (FTInsUser, FDInsDate, FTInsTime,FTAdjustStockNo, FNHSysFixedAssetId, FTPurchaseNo, FNHSysUnitId, FNPrice, FNQuantity)"
                            _Str &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ",'" & UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"
                            _Str &= vbCrLf & ",'" & .FTAssetCode.Properties.Tag & "','" & UL.ULF.rpQuoted(.FTPurchaseNo.Text) & "'," & .FNHSysUnitAssetId.Properties.Tag & "," & .FNPrice.Value & "," & .FNQuantity.Value & ""
                            If HI.Conn.SQLConn.Execute_Tran(_Str, Conn.SQLConn.Cmd, Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Exit Sub
                            End If
                        End If
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        'HI.INVEN.Barcode.CheckUpdateUnitStock(Me.FTAdjustStockNo.Text, Val(.FNHSysRawMatId.Properties.Tag.ToString))
                        Me.LoadAdjDetail(Me.FTAdjustStockNo.Text)

                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    End Try
                End If
            End With

        End With
    End Sub

    Private Sub gridviewdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvadjdetail.RowCountChanged,
                                                                                       ogvdetail.RowCountChanged
        Try
            Dim dt As New DataTable
            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            Dim dt2 As New DataTable
            Try
                dt2 = CType(ogcadjdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try
            FNHSysWHAssetId.Properties.ReadOnly = (dt.Rows.Count > 0 Or dt2.Rows.Count > 0)
            FNHSysWHAssetId.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0 Or dt2.Rows.Count > 0)
            dt.Dispose()
            dt2.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmremove_Click(sender As System.Object, e As System.EventArgs) Handles ocmremove.Click
        If CheckOwner() = False Then Exit Sub


        If BarcodeAsset.CheckDucumentCreateBar(FTAdjustStockNo.Text) Then
            HI.MG.ShowMsg.mInvalidData("เอกสารมีการสร้าง Barcode แล้ว ไม่สามารถทำการเพิ่ม ลบ หรือแก้ไขได้ !!!", 1311240005, Me.Text)
            Exit Sub
        End If

        Dim _Str As String = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail  WHERE FTAdjustStockNo='" & HI.UL.ULF.rpQuoted(FTAdjustStockNo.Text) & "' "

        If HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_FIXED) = True Then
            Me.LoadAdjDetail(Me.FTAdjustStockNo.Text)
        End If

        'End With
    End Sub

    'Private Sub ocmgenbarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmgenbarcode.Click
    '    If CheckOwner() = False Then Exit Sub
    '    'If FTAdjustStockNo.Text.Trim <> "" Then

    '    '    Dim _Str As String = ""

    '    '    With _GenBarcode
    '    '        .BarType = wGenerateBarcodeAsset.BarCodeType.Adjust
    '    '        .ProcGen = False
    '    '        .DocumentNo = FTAdjustStockNo.Text
    '    '        .LoadGenbarcode()
    '    '        .MainObject = Me
    '    '        .ShowDialog()

    '    '        If (.ProcGen) Then
    '    '            LoadBarcode(FTAdjustStockNo.Text)
    '    '        End If

    '    '    End With

    '    'End If
    '    If VerifyBarcode() = False Then Exit Sub
    '    Dim dt As New DataTable
    '    Try
    '        With dt
    '            .Columns.Add("FTBarcodeNo") : .Columns.Add("FTAssetCode") : .Columns.Add("FTAssetName") : .Columns.Add("FTUnitCode") : .Columns.Add("FTPurchaseNo") : .Columns.Add("FNQuantity") : .Columns.Add("FNHSysFixedAssetId")
    '            'For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Rows
    '            For Each R As DataRow In CType(ogcbarcode.DataSource, DataTable).Select("FTBarcodeNo = ''")
    '                If HI.Conn.SQLConn.GetField("select A.FTAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) where FNFixedAssetType>=6 and FNFixedAssetType<=7  and FNHSysFixedAssetId=" & R!FNHSysFixedAssetId & "", Conn.DB.DataBaseName.DB_MASTER) <> "" Then
    '                    _IDasset = R!FNHSysFixedAssetId.ToString
    '                    Genbar()
    '                    .Rows.Add(_BarCodeGen, R!FTAssetCode.ToString, R!FTAssetName.ToString, R!FTUnitCode.ToString, R!FTPurchaseNo.ToString, R!FNQuantity, R!FNHSysFixedAssetId)
    '                    .AcceptChanges()
    '                    _DT = dt
    '                    SaveBar()
    '                End If
    '            Next
    '            '.Rows.Add("KKOOLOFKGLE", 5555, "JOKER", "Box", "PO", 10, 1668630001)
    '        End With
    '        Dim Qry As String = ""
    '        Qry = "SELECT B.FTBarcodeNo,A.FTAssetCode"
    '        If ST.Lang.Language = ST.Lang.eLang.TH Then
    '            Qry &= vbCrLf & ",A.FTAssetNameTH AS FTAssetName,U.FTUnitNameTH AS FTUnitCode"
    '        Else
    '            Qry &= vbCrLf & ",A.FTAssetNameEN AS FTAssetName,U.FTUnitNameEN AS FTUnitCode"
    '        End If
    '        Qry &= vbCrLf & ",B.FTPurchaseNo,B.FNQuantity,B.FNHSysFixedAssetId "
    '        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B with(nolock) INNER JOIN"
    '        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN AS BI with(nolock) ON B.FTBarcodeNo=BI.FTBarcodeNo LEFT OUTER JOIN"
    '        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A with(nolock) ON B.FNHSysFixedAssetId = A.FNHSysFixedAssetId LEFT OUTER JOIN"

    '        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U with(nolock) ON B.FNHSysUnitId = U.FNHSysUnitId"
    '        Qry &= vbCrLf & "WHERE B.FTDocumentNo = '" & Me.FTAdjustStockNo.Text & "'"

    '        ogcbarcode.DataSource = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub
    Private Sub ocmgenbarcode_Click(sender As Object, e As EventArgs) Handles ocmgenbarcode.Click
        'If CheckOwner() = False Then Exit Sub
        'If FTReceiveNo.Text.Trim <> "" Then
        '    If FTReceiveNo.Properties.Tag.ToString <> "" Then
        '        Dim _Str As String = ""

        '        Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)

        '        With _GenBarcode
        '            Call HI.ST.Lang.SP_SETxLanguage(_GenBarcode)
        '            .BarType = wGenerateBarcodeAsset.BarCodeType.Receive
        '            .ProcGen = False
        '            .DocumentNo = FTReceiveNo.Text
        '            .LoadGenbarcode()
        '            .MainObject = Me
        '            .ShowDialog()

        '            If (.ProcGen) Then
        '                LoadBarcode(FTReceiveNo.Text)
        '            End If

        '        End With
        '    End If
        'End If
        Dim _PO As String = ""

        'With DirectCast(Me.ogcadjdetail.DataSource, DataTable)
        '    .AcceptChanges()

        '    For Each X As DataRow In .Rows
        '        '    If _PO <> "" Then _PO &= ","
        _PO &= "" & Me.FNFixedAssetType.Text & ""
        '    Next


        'End With

        If _PO = "เครื่องจักร" Or _PO = "อุปกรณ์คอมพิวเตอร์" Or _PO = "เครื่องใช้ไฟฟ้า" Or _PO = "ยานพาหนะ" Then
            Call addmaster(FTAdjustStockNo.ToString)
        Else



            If CheckOwner() = False Then Exit Sub


            For Each B As DataRow In CType(ogcbarcode.DataSource, DataTable).Select(" FTBarcodeNo = ''")
                _IDasset = B!FNHSysFixedAssetId
                Genbarcode()


                For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Select("FNHSysFixedAssetId=" & B!FNHSysFixedAssetId & "")
                    ' Dim Qry As String = ""
                    If _PO <> "อะไหล่จักร" Then
                        Dim Qry As String = "select B.FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNHSysFixedAssetId=" & Val(_IDasset) & ""
                        Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

                        'Qry = "SELECT FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase"
                        'Qry &= vbCrLf & "  WHERE      (FTPurchaseNo = '" & B!FTPurchaseNo.ToString & "')"
                        'Dim _AssetType As Integer = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        If _AssetType <> 1 Then
                            Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & _IDasset & ""
                        Else
                            Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WHERE FNHSysAssetPartId = " & _IDasset & ""
                        End If
                        If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "") <> "" Then
                            _BarCodeGen = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                        End If



                        Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                        Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                        Qry &= vbCrLf & ", FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo, FNHSysCmpId)"
                        Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & ",'" & _BarCodeGen & "','" & Me.FNHSysWHAssetId.Properties.Tag & "','" & R!FNHSysFixedAssetId & "'," & _SysUnit & ""
                        Qry &= vbCrLf & "," & _PriceConvert & "," & _QuantityConvert & ",'" & Me.FTAdjustStockNo.Text & "','" & B!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                        Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & ",'" & _BarCodeGen & "','" & Me.FTAdjustStockNo.Text & "','" & Me.FNHSysWHAssetId.Properties.Tag & "'," & _PriceConvert & ",'" & B!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        If _AssetType <> 1 Then

                            Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET "
                            Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            Qry &= vbCrLf & ",FTSerialNo='" & _BarCodeGen & "' "
                            Qry &= vbCrLf & "WHERE FNHSysFixedAssetId = '" & _IDasset & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                        Else
                            Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart SET "
                            Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            Qry &= vbCrLf & ",FTSerialNo='" & _BarCodeGen & "' "
                            Qry &= vbCrLf & "WHERE FNHSysAssetPartId = " & _IDasset & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                        End If
                    Else

                        Dim Qry As String = "select '1'AS FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS B WITH(NOLOCK) where B.FNHSysAssetPartId=" & Val(_IDasset) & ""
                        Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

                        'Qry = "SELECT FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase"
                        'Qry &= vbCrLf & "  WHERE      (FTPurchaseNo = '" & B!FTPurchaseNo.ToString & "')"
                        'Dim _AssetType As Integer = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        If _AssetType <> 1 Then
                            Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & _IDasset & ""
                        Else
                            Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WHERE FNHSysAssetPartId = " & _IDasset & ""
                        End If
                        If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "") <> "" Then
                            _BarCodeGen = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                        End If



                        Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                        Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                        Qry &= vbCrLf & ", FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo, FNHSysCmpId)"
                        Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & ",'" & _BarCodeGen & "','" & Me.FNHSysWHAssetId.Properties.Tag & "','" & R!FNHSysFixedAssetId & "'," & _SysUnit & ""
                        Qry &= vbCrLf & "," & _PriceConvert & "," & _QuantityConvert & ",'" & Me.FTAdjustStockNo.Text & "','" & B!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                        Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.
                            FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & ",'" & _BarCodeGen & "','" & Me.FTAdjustStockNo.Text & "','" & Me.FNHSysWHAssetId.Properties.Tag & "'," & _PriceConvert & ",'" & B!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        If _AssetType <> 1 Then

                            Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET "
                            Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            Qry &= vbCrLf & ",FTSerialNo='" & _BarCodeGen & "' "
                            Qry &= vbCrLf & "WHERE FNHSysFixedAssetId = '" & _IDasset & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                        Else
                            Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart SET "
                            Qry &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            Qry &= vbCrLf & ",FTSerialNo='" & _BarCodeGen & "' "
                            Qry &= vbCrLf & "WHERE FNHSysAssetPartId = " & _IDasset & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_MASTER)
                        End If
                    End If
                Next
            Next
            PrepareListBarcode()
        End If
        'Next


        '    End With

    End Sub
    Private Sub ogvbarcode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvbarcode.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                Call DeleteBarcodeGen()
            Case Keys.Enter
                If ogvbarcode.FocusedColumn.FieldName = "FTBarcodeNo" Then
                    _EditCell = True
                End If
        End Select
    End Sub

    Private Sub ocmdeletebarcode_Click_1(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcode.Click
        Call DeleteBarcode()
    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub wStockAdjust_Load(sender As Object, e As EventArgs) Handles Me.Load
        _FormLoad = False
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    'Private Sub ocmaddmaster_Click(sender As Object, e As EventArgs) Handles ocmaddmaster.Click
    Public Sub addmaster(Key As String)
        If VerifyInsertBarcode() Then
            Dim Qry As String = ""
            Dim dt As DataTable
            Dim dtTemp As New DataTable("DatatableTemp")
            Dim _IDAsset As String = ""
            Dim _CodeConfig As String = ""
            Dim _RunNumber As String = ""
            Dim _ColCount As Integer = 0
            Dim _PicByte As Byte() = Nothing
            Dim _Arridx As Integer = 0


            Dim _Price As Double = 0
            Dim _Qry As String = ""
            Dim _FTempcode As String = ""
            Dim _FTUnitSec As String = ""
            Dim _FNHSysEmpID As Integer = 0
            Dim _FNHSysUnitSectID As Integer = 0


            Try
                With CType(ogcadjdetail.DataSource, DataTable)
                    If .Rows.Count > 0 Then
                        Qry = "select A.FTReceiveNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTReceiveNo='" & Me.FTAdjustStockNo.Text & "'"
                        If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "" Then
                            For i As Integer = 0 To .Rows.Count - 1 Step 1
                                _IDAsset = ogvadjdetail.GetRowCellValue(i, "FNHSysFixedAssetId").ToString
                                Qry = "select B.FTAssetBrandNameTH as FTAssetBrandName"
                                Qry &= vbCrLf & ",M.FTAssetModelNameTH as FTAssetModelName,List.FTNameTH AS FTTypeName,'' AS FTSerialNo ,'' as FTProductCode"
                                Qry &= vbCrLf & ",'" & FDAdjustStockDate.Text & "'as FDDateUsed,0 as FNLifetime"
                                Qry &= vbCrLf & ",'" & FDAdjustStockDate.Text & "' AS FDDateStartWarranty,'" & FDAdjustStockDate.Text & "' as FDDateEndWarranty"
                                Qry &= vbCrLf & ",'" & _FTempcode & "' as FNHSysEmpID"
                                Qry &= vbCrLf & ",'" & _FTUnitSec & "' as FNHSysUnitSectId,'' AS FNHSysSuplId"
                                Qry &= vbCrLf & ",'" & ogvadjdetail.GetRowCellValue(i, "FTPurchaseNo").ToString & "' as FTPurchaseNo,'' as FDPurchaseDate,'' as FTPurchaseBy"
                                Qry &= vbCrLf & ",'' as FTReceiveNo ,'' as FDReceiveDate,'' as FTReceiveBy"
                                Qry &= vbCrLf & ",'' as FTInvoiceNo,'' as FDInvoiceDate"
                                Qry &= vbCrLf & ",0 as FNMaximumStock,0 as FNMinimumStock"
                                Qry &= vbCrLf & ",'' as FTRemark"

                                Qry &= vbCrLf & ",A.FTAssetNameTH,A.FTAssetNameEN"
                                Qry &= vbCrLf & ",A.FNHSysAssetTyped"
                                Qry &= vbCrLf & "," & _Price & " AS FNPrice,A.FNHSysCurId"
                                Qry &= vbCrLf & ",A.FNMaxPower,A.FNLifetimeType,'1' as FTStateActive"
                                Qry &= vbCrLf & "," & _FNHSysEmpID & " as HFNHSysEmpID," & _FNHSysUnitSectID & " as HFNHSysUnitSectId,'0' as HFNHSysSuplId"

                                Qry &= vbCrLf & ",A.FNHSysAssetBrandId,A.FNFixedAssetType,A.FNHSysAssetGrpId"
                                Qry &= vbCrLf & ",Grp.FTAssetGrpCode,T.FTAssetTypeCode"
                                Qry &= vbCrLf & ",A.FNHSysAssetModelId,A.FPImage,A.FTStateCritical"

                                Qry &= vbCrLf & "," & ogvadjdetail.GetRowCellValue(i, "FNHSysUnitId") & " As FNHSysUnitId"
                                Qry &= vbCrLf & "," & ogvadjdetail.GetRowCellValue(i, "FNHSysFixedAssetId") & " AS FNHSysFixedAssetId"

                                Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As A With(NOLOCK) LEFT OUtER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand As B With(NOLOCK) On A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUtER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel As M With(NOLOCK) On A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As AT With(NOLOCK) On A.FNHSysAssetTyped=AT.FNHSysAssetTyped LEFT OUTER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U With(NOLOCK) On A.FNHSysUnitSectId=U.FNHSysUnitSectId LEFT OUtER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetGrp As Grp With(NOLOCK) On A.FNHSysAssetGrpId=Grp.FNHSysAssetGrpId LEFT OUTER JOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As T With(NOLOCK) On A.FNHSysAssetTyped=T.FNHSysAssetTyped LEFT OUTER jOIN"
                                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As Cur With(NOLOCK) On A.FNHSysCurId=Cur.FNHSysCurId"
                                Qry &= vbCrLf & "LEFT OUTER JOIN (Select A.FNListIndex,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As A With(NOLOCK)"
                                Qry &= vbCrLf & "where A.FTListName='FNFixedAssetType') AS List ON A.FNFixedAssetType=List.FNListIndex"
                                Qry &= vbCrLf & "where A.FNFixedAssetType<> 6 and A.FNFixedAssetType<> 7 and A.FNHSysFixedAssetId=" & Val(_IDAsset) & ""

                                dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
                                If dt.Rows.Count > 0 Then
                                    If dtTemp.Columns.Count = 0 Then
                                        For Each R As DataRow In dt.Rows
                                            For Each C As DataColumn In dt.Columns
                                                dtTemp.Columns.Add(C.ColumnName)
                                                _ColCount += 1
                                                dtTemp.AcceptChanges()
                                            Next
                                        Next
                                        Dim _ArrRow(_ColCount - 1)
                                        For k As Integer = 1 To ogvadjdetail.GetRowCellValue(i, "FNQuantity") Step 1
                                            For Each R As DataRow In dt.Rows
                                                If R!FPImage Is DBNull.Value Then
                                                    _PicByte = Nothing
                                                Else
                                                    _PicByte = R!FPImage
                                                End If

                                                For Each C As DataColumn In dt.Columns
                                                    _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                    _Arridx += 1
                                                Next
                                                _Arridx = 0
                                                dtTemp.Rows.Add(_ArrRow)
                                                dtTemp.AcceptChanges()
                                            Next
                                        Next
                                    Else
                                        Dim _ArrRow(_ColCount - 1)
                                        For k As Integer = 1 To ogvadjdetail.GetRowCellValue(i, "FNQuantity") Step 1
                                            For Each R As DataRow In dt.Rows
                                                If R!FPImage Is DBNull.Value Then
                                                    _PicByte = Nothing
                                                Else
                                                    _PicByte = R!FPImage
                                                End If

                                                For Each C As DataColumn In dt.Columns
                                                    _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                    _Arridx += 1
                                                Next
                                                _Arridx = 0
                                                dtTemp.Rows.Add(_ArrRow)
                                                dtTemp.AcceptChanges()
                                            Next
                                        Next
                                    End If
                                End If
                            Next
                            If dtTemp.Rows.Count > 0 Then
                                TL.HandlerControl.ClearControl(_AddMasterpop)
                                With _AddMasterpop
                                    .Confirm = False
                                    .ogc.DataSource = dtTemp
                                    .ShowDialog()
                                    If .Confirm Then
                                        'insert data to table assetmaster
                                        _CodeConfig = HI.Conn.SQLConn.GetField("select L.FTReferCode   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and FNListIndex=0 ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                                        Try
                                            For Each R As DataRow In CType(.ogc.DataSource, DataTable).Select("FTSerialNo<>''")
                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                                                _IDAsset = R!FNHSYSFixedAssetId
                                                '_IDAsset = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TASMAsset", "FNHSysFixedAssetId", Conn.DB.DataBaseName.DB_MASTER)
                                                _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTAssetCode) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTAssetCode like '%" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & "%'", Conn.DB.DataBaseName.DB_MASTER)
                                                If _RunNumber = "" Then
                                                    _RunNumber = "00001"
                                                Else
                                                    _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 5)
                                                    _RunNumber = Format(Val(_RunNumber) + 1, "00000")
                                                End If
                                                '' insert in master
                                                'If R!FTSerialNo = Nothing Then
                                                '    Exit For
                                                'End If
                                                'Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FTSerialNo = '" & R!FTSerialNo & "'"
                                                'If HI.Conn.SQLConn.GetField(Qry, HI.Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                                                '    MG.ShowMsg.mInfo("ไม่สามารถทำรายการได้ เนื่องจากมีเลขอนุกรมซ้ำ", 201709200904, Me.Text, " SerialNo " + R!FTSerialNo + " ", MessageBoxIcon.Information)
                                                '    Exit For
                                                'End If
                                                'Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset"
                                                'Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime, FNHSysFixedAssetId, FNHSysCmpId, FNFixedAssetType, FTAssetCode, FTAssetNameTH, FTAssetNameEN"
                                                'Qry &= vbCrLf & ",FNHSysAssetModelId, FNHSysAssetBrandId, FTSerialNo, FNHSysAssetGrpId, FNHSysAssetTyped, FTProductCode, FNHSysSuplId, FNPrice, FDDateAdd, FDDateUsed, FNLifetime, FNLifetimeType"
                                                'Qry &= vbCrLf & ",FDDateStartWarranty, FDDateEndWarranty, FNMaxPower, FNHSysUnitSectId, FNHSysEmpID, FNHSysCurId, FTPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTInvoiceNo, FDInvoiceDate, FTReceiveNo"
                                                'Qry &= vbCrLf & ",FDReceiveDate, FTReceiveBy, FNMinimumStock, FNMaximumStock, FTRemark, FTStateActive, FTStateCritical)"
                                                'Qry &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                'Qry &= vbCrLf & "," & Val(_IDAsset) & "," & ST.SysInfo.CmpID & "," & R!FNFixedAssetType & ",'" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & _RunNumber & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTAssetNameTH.ToString & "','" & R!FTAssetNameEN.ToString & "','" & R!FNHSysAssetModelId & "','" & R!FNHSysAssetBrandId & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "','" & R!FNHSysAssetGrpId & "','" & R!FNHSysAssetTyped & "','" & R!FTProductCode.ToString & "'"
                                                '' Qry &= vbCrLf & ",'" & R!HFNHSysSuplId & "',0,'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"
                                                'If R!FNPrice = Nothing Then
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & ",0,'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"

                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & "," & R!FNPrice & ",'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"

                                                'End If

                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateUsed.ToString) & "'," & R!FNLifetime & "," & R!FNLifetimeType & ",'" & UL.ULDate.ConvertEnDB(R!FDDateStartWarranty.ToString) & "'"
                                                '' Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "'," & R!FNMaxPower & ",'" & R!HFNHSysUnitSectId & "','" & R!HFNHSysEmpID & "'"
                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "','" & R!FNMaxPower & "'"
                                                'If R!HFNHSysUnitSectId = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysUnitSectId & ""
                                                'End If
                                                'If R!HFNHSysEmpID = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysEmpID & ""
                                                'End If

                                                'Qry &= vbCrLf & ",'" & R!FNHSysCurId & "','" & R!FTPurchaseNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDPurchaseDate.ToString) & "','" & R!FTPurchaseBy.ToString & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
                                                'Qry &= vbCrLf & ",'" & Me.FTAdjustStockNo.Text & "','" & UL.ULDate.ConvertEnDB(R!FDReceiveDate.ToString) & "','" & R!FTReceiveBy.ToString & "','" & R!FNMinimumStock & "','" & R!FNMaximumStock & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTRemark.ToString & "','" & R!FTStateActive.ToString & "','" & R!FTStateCritical.ToString & "'"
                                                'If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                '    HI.Conn.SQLConn.Tran.Rollback()
                                                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                '    Exit Sub
                                                'End If

                                                '' end insert in master

                                                ' insert barcode
                                                Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                                                Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & Val(_IDAsset) & "," & R!FNHSysUnitId & ""
                                                Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & 1 & ",'" & Me.FTAdjustStockNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                End If
                                                'insert barcode IN
                                                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "','" & Me.FTAdjustStockNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & 1 & ",'" & Me.FTAdjustStockNo.Text & "'," & ST.SysInfo.CmpID & ""

                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Exit Sub
                                                End If
                                                HI.Conn.SQLConn.Tran.Commit()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                Qry = "update[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET FPImage=@FPImage where FNHSysFixedAssetId=@ID"
                                                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                                                cmd.Parameters.AddWithValue("@ID", Val(_IDAsset))

                                                Dim p As New SqlParameter("@FPImage", SqlDbType.Image)
                                                p.Value = _PicByte
                                                If R!FPImage Is Nothing Then
                                                    cmd.Parameters.Add(p).Value = DBNull.Value
                                                    cmd.ExecuteNonQuery()
                                                Else
                                                    ' cmd.Parameters.Add(p).Value = DBNull.Value
                                                End If

                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
                                            Next
                                            LoadBarcode(Me.FTAdjustStockNo.Text)
                                        Catch ex As Exception
                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                            MsgBox(ex.Message)
                                            Exit Sub
                                        End Try
                                    End If
                                End With
                            Else
                                MG.ShowMsg.mInfo("ไม่พบรายการสินทรัพย์ที่เป็นเครื่องจักร กรุณาตรวจสอบ", 201701091107, Me.Text, "", MessageBoxIcon.Information)
                            End If
                        Else
                            'check amount Asset when delete from transaction and retry add item
                            Dim _QtyAssetList As Integer = 0 : Dim _QtyDB As Integer = 0 : Dim _QtyDiffer As Integer = 0
                            For k As Integer = 0 To .Rows.Count - 1 Step 1
                                _IDAsset = ogvadjdetail.GetRowCellValue(k, "FNHSysFixedAssetId").ToString
                                Qry = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType=0 and B.FNHSysFixedAssetId=" & _IDAsset & ""
                                If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                                    _QtyAssetList = ogvadjdetail.GetRowCellValue(k, "FNQuantity")
                                    Qry = "select count(A.FNHSysFixedAssetId) FROM HITECH_MASTER.dbo.TASMAsset AS A WITH(NOLOCK) "
                                    Qry &= vbCrLf & "where A.FNHSysAssetBrandId=" & ogvadjdetail.GetRowCellValue(k, "FNHSysAssetBrandId") & " and A.FNHSysAssetModelId=" & ogvadjdetail.GetRowCellValue(k, "FNHSysAssetModelId") & ""
                                    Qry &= vbCrLf & "and A.FTReceiveNo='" & Me.FTAdjustStockNo.Text & "' and A.FTPurchaseNo='" & ogvadjdetail.GetRowCellValue(k, "FTPurchaseNo").ToString & "' and A.FNFixedAssetType=0"
                                    _QtyDB = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
                                    _QtyDiffer = _QtyAssetList - _QtyDB
                                    If _QtyDiffer > 0 Then


                                        Qry = "select B.FTAssetBrandNameTH as FTAssetBrandName"
                                        Qry &= vbCrLf & ",M.FTAssetModelNameTH as FTAssetModelName,List.FTNameTH AS FTTypeName,'' AS FTSerialNo ,'' as FTProductCode"
                                        Qry &= vbCrLf & ",'" & FDAdjustStockDate.Text & "'as FDDateUsed,0 as FNLifetime"
                                        Qry &= vbCrLf & ",'" & FDAdjustStockDate.Text & "' AS FDDateStartWarranty,'" & FDAdjustStockDate.Text & "' as FDDateEndWarranty"
                                        Qry &= vbCrLf & ",'" & _FTempcode & "' as FNHSysEmpID"
                                        Qry &= vbCrLf & ",'" & _FTUnitSec & "' as FNHSysUnitSectId,'' AS FNHSysSuplId"
                                        Qry &= vbCrLf & ",'" & ogvadjdetail.GetRowCellValue(k, "FTPurchaseNo").ToString & "' as FTPurchaseNo,'' as FDPurchaseDate,'' as FTPurchaseBy"
                                        Qry &= vbCrLf & ",'' as FTReceiveNo ,'' as FDReceiveDate,'' as FTReceiveBy"
                                        Qry &= vbCrLf & ",'' as FTInvoiceNo,'' as FDInvoiceDate"
                                        Qry &= vbCrLf & ",0 as FNMaximumStock,0 as FNMinimumStock"
                                        Qry &= vbCrLf & ",'' as FTRemark"

                                        Qry &= vbCrLf & ",A.FTAssetNameTH,A.FTAssetNameEN"
                                        Qry &= vbCrLf & ",A.FNHSysAssetTyped"
                                        Qry &= vbCrLf & "," & _Price & " AS FNPrice,A.FNHSysCurId"
                                        Qry &= vbCrLf & ",A.FNMaxPower,A.FNLifetimeType,'1' as FTStateActive"
                                        Qry &= vbCrLf & "," & _FNHSysEmpID & " as HFNHSysEmpID," & _FNHSysUnitSectID & " as HFNHSysUnitSectId,'0' as HFNHSysSuplId"

                                        Qry &= vbCrLf & ",A.FNHSysAssetBrandId,A.FNFixedAssetType,A.FNHSysAssetGrpId"
                                        Qry &= vbCrLf & ",Grp.FTAssetGrpCode,T.FTAssetTypeCode"
                                        Qry &= vbCrLf & ",A.FNHSysAssetModelId,A.FPImage,A.FTStateCritical"

                                        Qry &= vbCrLf & "," & ogvadjdetail.GetRowCellValue(k, "FNHSysUnitId") & " As FNHSysUnitId"
                                        Qry &= vbCrLf & "," & ogvadjdetail.GetRowCellValue(k, "FNHSysFixedAssetId") & " AS FNHSysFixedAssetId"

                                        Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As A With(NOLOCK) LEFT OUtER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetBrand As B With(NOLOCK) On A.FNHSysAssetBrandId=B.FNHSysAssetBrandId LEFT OUtER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetModel As M With(NOLOCK) On A.FNHSysAssetModelId=M.FNHSysAssetModelId LEFT OUTER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As AT With(NOLOCK) On A.FNHSysAssetTyped=AT.FNHSysAssetTyped LEFT OUTER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U With(NOLOCK) On A.FNHSysUnitSectId=U.FNHSysUnitSectId LEFT OUtER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetGrp As Grp With(NOLOCK) On A.FNHSysAssetGrpId=Grp.FNHSysAssetGrpId LEFT OUTER JOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetType As T With(NOLOCK) On A.FNHSysAssetTyped=T.FNHSysAssetTyped LEFT OUTER jOIN"
                                        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As Cur With(NOLOCK) On A.FNHSysCurId=Cur.FNHSysCurId"
                                        Qry &= vbCrLf & "LEFT OUTER JOIN (Select A.FNListIndex,A.FTNameTH,A.FTNameEN from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As A With(NOLOCK)"
                                        Qry &= vbCrLf & "where A.FTListName='FNFixedAssetType') AS List ON A.FNFixedAssetType=List.FNListIndex"
                                        Qry &= vbCrLf & "where A.FNFixedAssetType=0 and A.FNHSysFixedAssetId=" & Val(_IDAsset) & ""
                                        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED)
                                        If dt.Rows.Count > 0 Then

                                            If dtTemp.Columns.Count = 0 Then
                                                For Each R As DataRow In dt.Rows
                                                    For Each C As DataColumn In dt.Columns
                                                        dtTemp.Columns.Add(C.ColumnName)
                                                        _ColCount += 1
                                                        dtTemp.AcceptChanges()
                                                    Next
                                                Next
                                                Dim _ArrRow(_ColCount - 1)
                                                For p As Integer = 1 To _QtyDiffer Step 1
                                                    For Each R As DataRow In dt.Rows
                                                        If R!FPImage Is DBNull.Value Then
                                                            _PicByte = Nothing
                                                        Else
                                                            _PicByte = R!FPImage
                                                        End If
                                                        For Each C As DataColumn In dt.Columns
                                                            _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                            _Arridx += 1
                                                        Next
                                                        _Arridx = 0
                                                        dtTemp.Rows.Add(_ArrRow)
                                                        dtTemp.AcceptChanges()
                                                    Next
                                                Next
                                            Else
                                                Dim _ArrRow(_ColCount - 1)
                                                For p As Integer = 1 To _QtyDiffer Step 1
                                                    For Each R As DataRow In dt.Rows
                                                        If R!FPImage Is DBNull.Value Then
                                                            _PicByte = Nothing
                                                        Else
                                                            _PicByte = R!FPImage
                                                        End If

                                                        For Each C As DataColumn In dt.Columns
                                                            _ArrRow(_Arridx) = R.Item(C.ColumnName)
                                                            _Arridx += 1
                                                        Next
                                                        _Arridx = 0
                                                        dtTemp.Rows.Add(_ArrRow)
                                                        dtTemp.AcceptChanges()
                                                    Next
                                                Next
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                            If dtTemp.Rows.Count > 0 Then
                                TL.HandlerControl.ClearControl(_AddMasterpop)
                                With _AddMasterpop
                                    .Confirm = False
                                    .ogc.DataSource = dtTemp
                                    .ShowDialog()
                                    If .Confirm Then
                                        'insert data to table assetmaster
                                        _CodeConfig = HI.Conn.SQLConn.GetField("select L.FTReferCode   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and FNListIndex=0 ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                        Try
                                            For Each R As DataRow In CType(.ogc.DataSource, DataTable).Select("FTSerialNo<>''")
                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FIXED)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                                                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                                                _IDAsset = R!FNHSYSFixedAssetId
                                                '_IDAsset = HI.TL.RunID.GetRunNoID("" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TASMAsset", "FNHSysFixedAssetId", Conn.DB.DataBaseName.DB_MASTER)
                                                _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTAssetCode) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WITH(NOLOCK) where A.FTAssetCode like '%" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & "%'", Conn.DB.DataBaseName.DB_MASTER)
                                                If _RunNumber = "" Then
                                                    _RunNumber = "00001"
                                                Else
                                                    _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 5)
                                                    _RunNumber = Format(Val(_RunNumber) + 1, "00000")
                                                End If
                                                If R!FTSerialNo = Nothing Then
                                                    Exit For
                                                End If
                                                Qry = "SELECT FTSerialNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FTSerialNo = '" & R!FTSerialNo & "'"
                                                If HI.Conn.SQLConn.GetField(Qry, HI.Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                                                    MG.ShowMsg.mInfo("ไม่สามารถทำรายการได้ เนื่องจากมีเลขอนุกรมซ้ำ", 201709200904, Me.Text, " SerialNo " + R!FTSerialNo + " ", MessageBoxIcon.Information)
                                                    Exit For
                                                End If
                                                '' insert in master

                                                'Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset"
                                                'Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime, FNHSysFixedAssetId, FNHSysCmpId, FNFixedAssetType, FTAssetCode, FTAssetNameTH, FTAssetNameEN"
                                                'Qry &= vbCrLf & ",FNHSysAssetModelId, FNHSysAssetBrandId, FTSerialNo, FNHSysAssetGrpId, FNHSysAssetTyped, FTProductCode, FNHSysSuplId, FNPrice, FDDateAdd, FDDateUsed, FNLifetime, FNLifetimeType"
                                                'Qry &= vbCrLf & ",FDDateStartWarranty, FDDateEndWarranty, FNMaxPower, FNHSysUnitSectId, FNHSysEmpID, FNHSysCurId, FTPurchaseNo, FDPurchaseDate, FTPurchaseBy, FTInvoiceNo, FDInvoiceDate, FTReceiveNo"
                                                'Qry &= vbCrLf & ",FDReceiveDate, FTReceiveBy, FNMinimumStock, FNMaximumStock, FTRemark, FTStateActive, FTStateCritical)"
                                                'Qry &= vbCrLf & "select '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                'Qry &= vbCrLf & "," & Val(_IDAsset) & "," & ST.SysInfo.CmpID & "," & R!FNFixedAssetType & ",'" & _CodeConfig & R!FTAssetGrpCode & R!FTAssetTypeCode & _RunNumber & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTAssetNameTH.ToString & "','" & R!FTAssetNameEN.ToString & "'," & R!FNHSysAssetModelId & "," & R!FNHSysAssetBrandId & ""
                                                'Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "'," & R!FNHSysAssetGrpId & "," & R!FNHSysAssetTyped & ",'" & R!FTProductCode.ToString & "'"
                                                'If R!FNPrice = Nothing Then
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & ",0,'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysSuplId & "," & R!FNPrice & ",'" & HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS JOkE", Conn.DB.DataBaseName.DB_FIXED) & "'"
                                                'End If
                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateUsed.ToString) & "'," & R!FNLifetime & "," & R!FNLifetimeType & ",'" & UL.ULDate.ConvertEnDB(R!FDDateStartWarranty.ToString) & "'"
                                                'Qry &= vbCrLf & ",'" & UL.ULDate.ConvertEnDB(R!FDDateEndWarranty.ToString) & "'," & R!FNMaxPower & ""
                                                'If R!HFNHSysUnitSectId = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysUnitSectId & ""
                                                'End If
                                                'If R!HFNHSysEmpID = Nothing Then
                                                '    Qry &= vbCrLf & ",0"
                                                'Else
                                                '    Qry &= vbCrLf & "," & R!HFNHSysEmpID & ""
                                                'End If
                                                'Qry &= vbCrLf & "," & R!FNHSysCurId & ",'" & R!FTPurchaseNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDPurchaseDate.ToString) & "','" & R!FTPurchaseBy.ToString & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTInvoiceNo.ToString & "','" & UL.ULDate.ConvertEnDB(R!FDInvoiceDate.ToString) & "'"
                                                'Qry &= vbCrLf & ",'" & Me.FTAdjustStockNo.Text & "','" & UL.ULDate.ConvertEnDB(R!FDReceiveDate.ToString) & "','" & R!FTReceiveBy.ToString & "','" & R!FNMinimumStock & "','" & R!FNMaximumStock & "'"
                                                'Qry &= vbCrLf & ",'" & R!FTRemark.ToString & "','" & R!FTStateActive.ToString & "','" & R!FTStateCritical.ToString & "'"
                                                'If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                '    HI.Conn.SQLConn.Tran.Rollback()
                                                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                '    Exit Sub
                                                'End If

                                                '' end insert in master

                                                ' insert barcode
                                                Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                                                Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "','" & Me.FNHSysWHAssetId.Properties.Tag & "'," & Val(_IDAsset) & "," & R!FNHSysUnitId & ""
                                                Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & 1 & ",'" & Me.FTAdjustStockNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                End If
                                                'insert barcode IN
                                                Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                                                Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                                                Qry &= vbCrLf & ",'" & R!FTSerialNo.ToString & "','" & Me.FTAdjustStockNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & 1 & ",'" & Me.FTAdjustStockNo.Text & "'," & ST.SysInfo.CmpID & ""

                                                If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                                    HI.Conn.SQLConn.Tran.Rollback()
                                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                                    Exit Sub
                                                End If
                                                HI.Conn.SQLConn.Tran.Commit()
                                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MASTER)
                                                HI.Conn.SQLConn.SqlConnectionOpen()
                                                Qry = "update[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset SET FPImage=@FPImage where FNHSysFixedAssetId=@ID"
                                                Dim cmd As New SqlCommand(Qry, HI.Conn.SQLConn.Cnn)
                                                cmd.Parameters.AddWithValue("@ID", Val(_IDAsset))

                                                Dim p As New SqlParameter("@FPImage", SqlDbType.Image)
                                                p.Value = _PicByte
                                                If R!FPImage Is Nothing Then
                                                    cmd.Parameters.Add(p).Value = DBNull.Value
                                                Else
                                                    cmd.Parameters.Add(p).Value = DBNull.Value
                                                End If
                                                cmd.ExecuteNonQuery()
                                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
                                            Next
                                            LoadBarcode(Me.FTAdjustStockNo.Text)
                                        Catch ex As Exception
                                            HI.Conn.SQLConn.Tran.Rollback()
                                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                            MsgBox(ex.Message)
                                            Exit Sub
                                        End Try
                                    End If
                                End With
                            Else
                                MG.ShowMsg.mInfo("ไม่สามารถทำรายการได้ เนื่องจากมีรายการนี้อยู่แล้ว", 201701121621, Me.Text, "", MessageBoxIcon.Information)
                            End If
                        End If
                    Else
                        MG.ShowMsg.mInfo("กรุณาเพิ่มรายการก่อน", 201701121617, Me.Text, "", MessageBoxIcon.Information)
                    End If
                End With

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            MG.ShowMsg.mInfo("กรุณาบันทึกข้อมูลบาร์โค๊ดก่อน !!!", 201701191508, Me.Text, "", MessageBoxIcon.Warning)
            otbdetail.SelectedTabPageIndex = 1
        End If
    End Sub

    Private Function VerifyInsertBarcode()
        Dim _Pass As Boolean = False
        Try
            If CType(ogcbarcode.DataSource, DataTable).Rows.Count > 0 Then
                For Each R As DataRow In CType(ogcbarcode.DataSource, DataTable).Rows
                    If R!FTBarcodeNo.ToString <> "" Then
                        _Pass = True
                    Else
                        _Pass = False
                        Exit For
                    End If
                Next
            Else
                _Pass = True
            End If

        Catch ex As Exception

        End Try
        Return _Pass
    End Function
    Private Sub ogvbarcode_FocusedColumnChanged(sender As Object, e As FocusedColumnChangedEventArgs) Handles ogvbarcode.FocusedColumnChanged
        If _FormLoad Then Exit Sub
        If _EditCell Then
            Dim Qry As String = ""
            Dim _ID As String = "" : Dim _BarCodeNo As String = ""
            With ogvbarcode
                _ID = .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId")
                _BarCodeNo = .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo")
                Qry = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType=0 and B.FNHSysFixedAssetId=" & _ID & ""
                If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "" And _BarCodeNo <> "" Then
                    Try
                        For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Select("FNHSysFixedAssetId='" & _ID & "'")
                            Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                            Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                            Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                            Qry &= vbCrLf & ",'" & _BarCodeNo & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & Val(_ID) & "," & R!FNHSysUnitId & ""
                            Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & R!FNQuantity & ",'" & Me.FTAdjustStockNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                            Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                            Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                            Qry &= vbCrLf & ",'" & _BarCodeNo & "','" & Me.FTAdjustStockNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNQuantity & ",'" & Me.FTAdjustStockNo.Text & "'," & ST.SysInfo.CmpID & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        Next
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
            End With
            _EditCell = False
        End If
    End Sub
    Private Sub ogvbarcode_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles ogvbarcode.FocusedRowChanged
        Try
            With ogvbarcode
                If .GetRowCellValue(.FocusedRowHandle, "FTBarcodeNo").ToString = "" And .FocusedColumn.FieldName = "FTBarcodeNo" Then
                    Me.cFTBarcodeNo.OptionsColumn.AllowEdit = True
                Else
                    Dim Qry As String = "" : Dim _val As String = ""
                    _val = .GetRowCellValue(.FocusedRowHandle, "FNHSysFixedAssetId").ToString
                    Qry = "select B.FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNFixedAssetType=0 and B.FNHSysFixedAssetId=" & _val & ""

                    If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) = "" Then
                        Me.cFTBarcodeNo.OptionsColumn.AllowEdit = True
                    Else
                        Me.cFTBarcodeNo.OptionsColumn.AllowEdit = False
                    End If
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub PrepareListBarcode()
    '    Dim dt As New DataTable
    '    Try
    '        With dt
    '            .Columns.Add("FTBarcodeNo") : .Columns.Add("FTAssetCode") : .Columns.Add("FTAssetName") : .Columns.Add("FTUnitCode") : .Columns.Add("FTPurchaseNo") : .Columns.Add("FNQuantity") : .Columns.Add("FNHSysFixedAssetId")
    '            For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Rows
    '                If HI.Conn.SQLConn.GetField("select A.FTAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) where FNFixedAssetType=0 and FNHSysFixedAssetId=" & R!FNHSysFixedAssetId & "", Conn.DB.DataBaseName.DB_MASTER) = "" Then
    '                    .Rows.Add("", R!FTAssetCode.ToString, R!FTAssetName.ToString, R!FTUnitCode.ToString, R!FTPurchaseNo.ToString, R!FNQuantity, R!FNHSysFixedAssetId)
    '                    .AcceptChanges()
    '                End If
    '            Next

    '        End With
    '        ogcbarcode.DataSource = dt
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Private Sub PrepareListBarcode()
    '    Dim dt As New DataTable
    '    Try
    '        With dt
    '            .Columns.Add("FTBarcodeNo") : .Columns.Add("FTAssetCode") : .Columns.Add("FTAssetName") : .Columns.Add("FTUnitCode") : .Columns.Add("FTPurchaseNo") : .Columns.Add("FNQuantity") : .Columns.Add("FNHSysFixedAssetId")
    '            For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Rows
    '                If HI.Conn.SQLConn.GetField("select A.FTAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) where FNFixedAssetType>=6 and FNFixedAssetType<=7  and FNHSysFixedAssetId=" & R!FNHSysFixedAssetId & "", Conn.DB.DataBaseName.DB_MASTER) <> "" Then
    '                    _IDasset = R!FNHSysFixedAssetId.ToString
    '                    .Rows.Add("", R!FTAssetCode.ToString, R!FTAssetName.ToString, R!FTUnitCode.ToString, R!FTPurchaseNo.ToString, R!FNQuantity, R!FNHSysFixedAssetId)
    '                    .AcceptChanges()
    '                End If
    '            Next
    '            '.Rows.Add("KKOOLOFKGLE", 5555, "JOKER", "Box", "PO", 10, 1668630001)
    '        End With
    '        ogcbarcode.DataSource = dt


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub PrepareListBarcode()
        Dim Qry As String = ""
        Dim dt As New DataTable


        Dim _PO As String = ""
        'If ogcadjdetail.DataSource = True Then


        '    With DirectCast(Me.ogcadjdetail.DataSource, DataTable)
        '        .AcceptChanges()

        '        For Each R As DataRow In .Rows
        '            If _PO <> "" Then _PO &= ","
        '            _PO &= "" & R!FNFixedAssetType.ToString & ""
        '        Next
        ''    End With
        'With _AddItemPopup


        '    .AssetComplete = False
        '    .DocNo = FTAdjustStockNo.Text
        '    .FTPurchaseNo.ReadOnly = False
        '    HI.TL.HandlerControl.ClearControl(_AddItemPopup)
        '    .FNFixedAssetType.Text = Me.FNFixedAssetType.Text






        '    FNFixedAssetType = HI.Conn.SQLConn.GetField("select L.FNListIndex   from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData as L where L.FTListName ='FNFixedAssetType'  and L.FTNameTH='" & .FNFixedAssetType.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")


        Try
            With dt
                .Columns.Add("FTBarcodeNo") : .Columns.Add("FTAssetCode") : .Columns.Add("FTAssetName") : .Columns.Add("FTUnitCode") : .Columns.Add("FTPurchaseNo") : .Columns.Add("FNQuantity") : .Columns.Add("FNHSysFixedAssetId")
                For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Rows
                    Qry = "SELECT A.FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust AS A WITH(NOLOCK) LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail AS AD WITH(NOLOCK) ON A.FTAdjustStockNo=AD.FTAdjustStockNo"
                    Qry &= vbCrLf & "  WHERE      (AD.FNHSysFixedAssetId = N'" & R!FNHSysFixedAssetId & "')"
                    Dim _AssetType As String = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

                    Qry = "Select isnull(A.FNFixedAssetType, '') As FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTAdjust_Detail As P With(NOLOCK) Left outer join"
                    Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As A WITH(NOLOCK) ON P.FNHSysFixedAssetId = A.FNHSysFixedAssetId where P.FNHSysFixedAssetId =" & R!FNHSysFixedAssetId & ""
                    '  Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS AP WITH(NOLOCK) ON P.FNHSysFixedAssetId = AP.FNHSysAssetPartId where P.FNHSysFixedAssetId =" & R!FNHSysFixedAssetId & ""

                    '   If HI.Conn.SQLConn.GetField("select A.FTAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A WItH(NOLOCK) where FNFixedAssetType>=6 and FNFixedAssetType<=7  and FNHSysFixedAssetId=" & R!FNHSysFixedAssetId & "", Conn.DB.DataBaseName.DB_MASTER) <> "" Then
                    If HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "6" Or HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER) = "7" Or FNFixedAssetType.SelectedIndex = 1 Then
                        If _AssetType <> 1 Then
                            Qry = "Select isnull(FTSerialNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & R!FNHSysFixedAssetId & ""
                        Else
                            Qry = "Select isnull(FTSerialNo,'') FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WHERE FNHSysAssetPartId = " & R!FNHSysFixedAssetId & ""
                        End If
                        Dim _SerialNo As String = HI.Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
                        _IDasset = R!FNHSysFixedAssetId.ToString

                        _SysUnitCode = R!FTUnitCode
                        _QuantityConvert = R!FNQuantity
                        _SysUnit = R!FNHSysUnitId
                        Call UnitConvert(_IDasset, R!FNQuantity, R!FNPrice)
                        .Rows.Add(_SerialNo, R!FTAssetCode.ToString, R!FTAssetName.ToString, _SysUnitCode, R!FTPurchaseNo, _QuantityConvert, R!FNHSysFixedAssetId)
                        .AcceptChanges()
                        _DT = dt
                        If _SerialNo <> "" Then SaveBar()
                    End If
                Next
                '.Rows.Add("KKOOLOFKGLE", 5555, "JOKER", "Box", "PO", 10, 1668630001)
            End With
            ogcbarcode.DataSource = dt


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ' End If

        'End With

    End Sub

    Dim _SysUnit As Integer
    Dim _QuantityConvert As Integer
    Dim _PriceConvert As Double
    Dim _SysUnitCode As String = ""
    Public Sub UnitConvert(Key As Integer, Quantity As Integer, Price As Integer)
        Dim Qry As String = ""
        Qry = "Select  A.FNHSysUnitAssetId,A.FNHSysUnitAssetIdTo,A.FNRateFrom,A.FNRateTo,B.FNHSysFixedAssetId,b.FTAssetCode,d.FTUnitAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAssetConvert As A "
        Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As B On A.FNHSysUnitAssetIdTo = B.FNHSysUnitAssetId "
        Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS C ON A.FNHSysUnitAssetId = C.FNHSysUnitId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset As D On A.FNHSysUnitAssetIdTo = D.FNHSysUnitAssetId"
        Qry &= vbCrLf & "WHERE B.FNHSysFixedAssetId = " & Key & " And C.FNHSysFixedAssetId = " & Key & ""
        Qry &= vbCrLf & "union all"
        Qry &= vbCrLf & "Select  A.FNHSysUnitAssetId,A.FNHSysUnitAssetIdTo,A.FNRateFrom,A.FNRateTo,B.FNHSysAssetPartId,b.FTAssetPartCode,d.FTUnitAssetCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAssetConvert As A "
        Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS B ON A.FNHSysUnitAssetIdTo = B.FNHSysUnitAssetId "
        Qry &= vbCrLf & "INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail As C On A.FNHSysUnitAssetId = C.FNHSysUnitId"
        Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAsset As D On A.FNHSysUnitAssetIdTo = D.FNHSysUnitAssetId"
        Qry &= vbCrLf & "WHERE B.FNHSysAssetPartId = " & Key & " And C.FNHSysFixedAssetId = " & Key & ""
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        If _dt.Rows.Count = 0 Then
            Exit Sub
        End If
        For Each U As DataRow In _dt.Rows
            _SysUnit = U!FNHSysUnitAssetIdTo
            _QuantityConvert = (Quantity / U!FNRateFrom) * U!FNRateTo
            _PriceConvert = Price * (U!FNRateFrom / U!FNRateTo)
            _SysUnitCode = U!FTUnitAssetCode
        Next
    End Sub

    Dim _IDasset As String = ""
    Private _BarCodeGen As String
    Private _DT As DataTable

    Private Sub SaveBar()
        Dim Qry As String = ""
        Dim _ID As String = "" : Dim _BarCodeNo As String = ""

        For Each B As DataRow In _DT.Select("FNHSysFixedAssetId='" & _IDAsset & "'")
            Qry = "select top 1 B.FTBarcodeNo from HITECH_FIXEDASSET..TFIXEDTBarcode AS B WITH(NOLOcK) INNER JOIN"
            Qry &= vbCrLf & "[" & Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "]..TFIXEDTBarcode_IN AS BI WITH(NOLOcK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
            Qry &= vbCrLf & "where B.FTBarcodeNo = '" & B!FTBarcodeNo & "' and B.FTDocumentNo='" & UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"
            If Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED) = "" And B!FTBarcodeNo <> "" Then
                Qry = "select  B.FTBarcodeNo from HITECH_FIXEDASSET..TFIXEDTBarcode AS B WITH(NOLOcK) INNER JOIN"
                Qry &= vbCrLf & "HITECH_FIXEDASSET..TFIXEDTBarcode_IN AS BI WITH(NOLOcK) ON B.FTBarcodeNo=BI.FTBarcodeNo"
                Qry &= vbCrLf & "where B.FTDocumentNo = '" & UL.ULF.rpQuoted(Me.FTAdjustStockNo.Text) & "'"
                If Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_FIXED).Rows.Count < CType(ogcadjdetail.DataSource, DataTable).Rows.Count Then
                    Try
                        For Each R As DataRow In CType(ogcadjdetail.DataSource, DataTable).Select("FNHSysFixedAssetId='" & B!FNHSysFixedAssetId & "'")
                            Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode (FTInsUser, FDInsDate, FTInsTime"
                            Qry &= vbCrLf & ",FTBarcodeNo, FNHSysWHAssetId, FNHSysFixedAssetId, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTPurchaseNo,FNHSysCmpId)"
                            Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                            Qry &= vbCrLf & ",'" & B!FTBarcodeNo & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNHSysFixedAssetId & "," & R!FNHSysUnitId & ""
                            Qry &= vbCrLf & "," & Val(R!FNPrice.ToString) & "," & R!FNQuantity & ",'" & Me.FTAdjustStockNo.Text & "','" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                            Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_IN(FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHAssetId, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                            Qry &= vbCrLf & "SELECT '" & ST.UserInfo.UserName & "'," & UL.ULDate.FormatDateDB & "," & UL.ULDate.FormatTimeDB & ""
                            Qry &= vbCrLf & ",'" & B!FTBarcodeNo & "','" & Me.FTAdjustStockNo.Text & "'," & Me.FNHSysWHAssetId.Properties.Tag & "," & R!FNQuantity & ",'" & R!FTPurchaseNo.ToString & "'," & ST.SysInfo.CmpID & ""
                            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_FIXED)
                        Next

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                Else
                    'มีบาร์โค๊ดครบจำนวนที่รับมาแล้ว ไม่สามารถเพิ่มใหม่ได้
                End If
            Else
                'มีบาร์โค็ดอยู่แล้ว
            End If
        Next

    End Sub

    Private Sub Genbar()
        Dim sDate As String = Year(Date.Today) & "" & Month(Date.Today).ToString.PadLeft(2, "0") & "" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
        If Year(Date.Today) > 2400 Then
            sDate = Year(Date.Today) - 543 & "" & Month(Date.Today).ToString.PadLeft(2, "0") & "" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
        End If
        sDate = sDate.Substring(2)


        Dim _PO As String = ""

        'With DirectCast(Me.ogcadjdetail.DataSource, DataTable)
        '    .AcceptChanges()

        '    For Each R As DataRow In .Rows
        '        If _PO <> "" Then _PO &= ","
        _PO &= "" & Me.FNFixedAssetType.Text & ""
        '    Next
        'End With

        If _PO <> "อะไหล่จักร" Then
            Dim Qry As String = "select B.FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS B WITH(NOLOCK) where B.FNHSysFixedAssetId=" & Val(_IDasset) & ""
            Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

            Dim _RunNumber As String = ""

            Qry = "SELECT G.FTAssetPartGroupCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS G WITH(NOLOCK) ON P.FNHSysAssetPartGrpId = G.FNHSysAssetPartGrpId WHERE P.FNHSysAssetPartId = " & Val(_IDasset) & ""
            Dim _AssetPart As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
            _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTBarcodeNo) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS A WITH(NOLOCK) where A.FTBarcodeNo like '%" & _AssetType & sDate & "%'", Conn.DB.DataBaseName.DB_FIXED)
            If _AssetPart = "" Then _AssetPart = "P"
            If _AssetType = "" Then _AssetType = _AssetPart
            If _RunNumber = "" Then
                _RunNumber = "0001"
            Else
                _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 4)
                _RunNumber = Format(Val(_RunNumber) + 1, "0000")
            End If
            _BarCodeGen = _AssetType + sDate + _RunNumber
        Else
            Dim Qry As String = "select  '1'AS FNFixedAssetType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS B WITH(NOLOCK) where B.FNHSysAssetPartId=" & Val(_IDasset) & ""
            Dim _AssetType As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_FIXED)

            Dim _RunNumber As String = ""

            Qry = "SELECT G.FTAssetPartGroupCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS P WITH(NOLOCK) INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPartGrp AS G WITH(NOLOCK) ON P.FNHSysAssetPartGrpId = G.FNHSysAssetPartGrpId WHERE P.FNHSysAssetPartId = " & Val(_IDasset) & ""
            Dim _AssetPart As String = Conn.SQLConn.GetField(Qry, Conn.DB.DataBaseName.DB_MASTER)
            _RunNumber = HI.Conn.SQLConn.GetField("select max(A.FTBarcodeNo) AS MaxCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS A WITH(NOLOCK) where A.FTBarcodeNo like '%" & _AssetType & sDate & "%'", Conn.DB.DataBaseName.DB_FIXED)
            If _AssetPart = "" Then _AssetPart = "P"
            If _AssetType = "" Then _AssetType = _AssetPart
            If _RunNumber = "" Then
                _RunNumber = "0001"
            Else

                _RunNumber = Microsoft.VisualBasic.Right(_RunNumber, 4)
                _RunNumber = Format(Val(_RunNumber) + 1, "0000")
            End If
            _BarCodeGen = _AssetType + sDate + _RunNumber
        End If


    End Sub

    Private Sub FTAdjustStockNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTAdjustStockNo.EditValueChanged

    End Sub
End Class