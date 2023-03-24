Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraTab
Imports System.IO

Public Class wSMPCreateOrderSample

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_PUR
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _CopyOrder As wCopySMPOrder
    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _AddPart As wSMPOrderAddPart
    Private _AddFile As wSMPOrderAddFile

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()
        _CopyOrder = New wCopySMPOrder()

        HI.TL.HandlerControl.AddHandlerObj(_CopyOrder)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyOrder.Name.ToString.Trim, _CopyOrder)
        Catch ex As Exception
        End Try

        _AddPart = New wSMPOrderAddPart

        HI.TL.HandlerControl.AddHandlerObj(_AddPart)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _AddPart.Name.ToString.Trim, _AddPart)
        Catch ex As Exception
        End Try


        _AddFile = New wSMPOrderAddFile

        HI.TL.HandlerControl.AddHandlerObj(_AddFile)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _AddFile.Name.ToString.Trim, _AddFile)
        Catch ex As Exception
        End Try


        With RepFTDeliveryDate

            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            ' AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave

        End With
        Call LoadSizeBreakdown()
    End Sub

#Region "Grid"

    Private Shared Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String

                Try

                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)

                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If

                Catch ex As Exception
                    _TDate = ""
                End Try

                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate

                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception
                End Try

                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadSizeBreakdown()
        Dim cmd As String
        Dim _dtprod As DataTable

        cmd = "  Select    A.FNSeq,A.FTSizeBreakDown"

        cmd &= vbCrLf & " FROM (Select FNListIndex + 1 As FNSeq, FTNameEN As FTSizeBreakDown"
        cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData AS X WITH(NOLOCK)"
        cmd &= vbCrLf & "  Where (FTListName = N'FNSMPOrderSize')"
        cmd &= vbCrLf & " ) As A "
        cmd &= vbCrLf & "   GROUP BY  A.FTSizeBreakDown"
        cmd &= vbCrLf & " ,A.FNSeq "
        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SYSTEM)

        RepositoryFTSizeBreakDown.DataSource = _dtprod.Copy
    End Sub
#End Region

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
        _Str &= vbCrLf & "   FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysTableObjForm WITH(NOLOCK) "
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
            _Str &= vbCrLf & "  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Str &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Str &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)


            _Str = " EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
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


    Private Sub LoadRowMatDataFabric(Optional SetColGrid As Boolean = False)
        Dim cmdstring As String = ""
        Dim dt As DataTable


        cmdstring = "  Select  FNHSysRawMatId "
        cmdstring &= vbCrLf & " 	, FTRawMatCode "
        cmdstring &= vbCrLf & "	, FTDescription"

        cmdstring &= vbCrLf & ", FTRawMatColorCode"
        cmdstring &= vbCrLf & ", FTRawMatColorName"
        cmdstring &= vbCrLf & ", FTRawMatSizeCode"
        cmdstring &= vbCrLf & ", FTUnitCode"
        cmdstring &= vbCrLf & ", FTFabricFrontSize"
        cmdstring &= vbCrLf & ", FNHSysUnitId"
        cmdstring &= vbCrLf & ",FTRawMatCode + '|'+ FTRawMatColorCode +'|' + FTRawMatSizeCode AS FTItemDataRef"
        cmdstring &= vbCrLf & " FROM(Select        H.FNHSysRawMatId, H.FTRawMatCode, H.FTRawMatNameEN As FTDescription, H.FTRawMatColorNameEN As FTRawMatColorName, H.FNHSysRawMatColorId, H.FNHSysRawMatSizeId, ISNULL(C.FTRawMatColorCode, '') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode, '') AS FTRawMatSizeCode, H.FNHSysUnitId, U.FTUnitCode, H.FTFabricFrontSize"
        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As H With (NOLOCK) LEFT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With (NOLOCK) On H.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON H.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As C With (NOLOCK) On H.FNHSysRawMatColorId = C.FNHSysRawMatColorId "
        cmdstring &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM With (NOLOCK)  ON H.FTRawMatCode = MM.FTMainMatCode "
        cmdstring &= vbCrLf & "  WHERE H.FTStateActive='1' AND MM.FNMerMatType =0  "
        cmdstring &= vbCrLf & "  ) As A"
        cmdstring &= vbCrLf & " ORDER BY FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode"

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        FNHSysRawmatIdA.Properties.DataSource = dt.Copy
        FNHSysRawmatIdB.Properties.DataSource = dt.Copy
        FNHSysRawmatIdC.Properties.DataSource = dt.Copy
        FNHSysRawmatIdD.Properties.DataSource = dt.Copy
        FNHSysRawmatIdE.Properties.DataSource = dt.Copy
        FNHSysRawmatIdF.Properties.DataSource = dt.Copy
        FNHSysRawmatIdG.Properties.DataSource = dt.Copy
        FNHSysRawmatIdH.Properties.DataSource = dt.Copy
        FNHSysRawmatIdI.Properties.DataSource = dt.Copy

        Call LoadRowMatDataAcc(SetColGrid)

    End Sub



    Private Sub LoadRowMatDataAcc(Optional SetColGrid As Boolean = False)
        Dim cmdstring As String = ""
        Dim dt As DataTable


        cmdstring = "  Select  FNHSysRawMatId,FNHSysRawMatId AS FNHSysRawMatId_Hide "
        cmdstring &= vbCrLf & " 	, FTRawMatCode "
        cmdstring &= vbCrLf & "	, FTDescription"
        cmdstring &= vbCrLf & ", FTRawMatColorName"
        cmdstring &= vbCrLf & ", FTRawMatColorCode"
        cmdstring &= vbCrLf & ", FTRawMatSizeCode"
        cmdstring &= vbCrLf & ", FNHSysUnitId"
        cmdstring &= vbCrLf & ", FTUnitCode"
        cmdstring &= vbCrLf & ", FTFabricFrontSize"
        cmdstring &= vbCrLf & ",FTRawMatCode + '|'+ FTRawMatColorCode +'|' + FTRawMatSizeCode AS FTItemDataRef"
        cmdstring &= vbCrLf & " FROM(Select        H.FNHSysRawMatId, H.FTRawMatCode, H.FTRawMatNameEN As FTDescription, H.FTRawMatColorNameEN As FTRawMatColorName, H.FNHSysRawMatColorId, H.FNHSysRawMatSizeId, ISNULL(C.FTRawMatColorCode, '') AS FTRawMatColorCode, ISNULL(S.FTRawMatSizeCode, '') AS FTRawMatSizeCode, H.FNHSysUnitId, U.FTUnitCode, H.FTFabricFrontSize"
        cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As H With (NOLOCK) LEFT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With (NOLOCK) On H.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON H.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER Join"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As C With (NOLOCK) On H.FNHSysRawMatColorId = C.FNHSysRawMatColorId "
        cmdstring &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM With (NOLOCK)  ON H.FTRawMatCode = MM.FTMainMatCode "
        cmdstring &= vbCrLf & "  WHERE H.FTStateActive='1' AND MM.FNMerMatType <>0  "
        cmdstring &= vbCrLf & "  ) As A"
        cmdstring &= vbCrLf & " ORDER BY FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode"

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)


        RepositoryFNHSysRawmatId.DataSource = dt.Copy

    End Sub

    Public Sub LoadOrderInfo(ByVal Key As String)
        '...call by another form name zzz...
        FTSMPOrderNo.Text = Key
    End Sub

    Public Sub LoadDataInfo(Key As Object)

        _FormLoad = True
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

                        Case ENM.Control.ControlType.GridLookUpEdit

                            With CType(Obj, DevExpress.XtraEditors.GridLookUpEdit)
                                .EditValue = R.Item(Col).ToString.ToString()
                            End With

                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit

                            Obj.Text = R.Item(Col).ToString

                        Case ENM.Control.ControlType.PictureEdit

                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.ConvertByteArrayToImmage(R.Item(Col))
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

        otbdetail.SelectedTabPageIndex = 0

        FPOrderImage1.Image = Nothing
        FPOrderImage2.Image = Nothing
        FPOrderImage3.Image = Nothing
        FPOrderImage4.Image = Nothing
        FTStateApp.Checked = False
        opnrevised.Visible = False
        olbreviseno.Text = ""

        _Str = ""
        _Str = "SELECT TOP 1  A.FPOrderImage1, A.FPOrderImage2, A.FPOrderImage3, A.FPOrderImage4 ,A.FTStateApp,A.FNRevisedNo,A.FTStateReceipt,A.FTSMPOrderBy,A.FNSMPOrderStatus  "
        _Str &= vbCrLf & ",CASE WHEN ISNULL(A.FTStateReceiptBy,'') ='' THEN '' ELSE A.FTStateReceiptBy + '  ' + Convert(nvarchar(10), convert(datetime,A.FTStateReceiptDate)  ,103)  +' ' +A.FTStateReceiptTime END  AS FTStateReceiptBy"
        _Str &= vbCrLf & ",CASE WHEN ISNULL(A.FTStateAppBy,'') ='' THEN '' ELSE A.FTStateAppBy + '  ' + Convert(nvarchar(10), convert(datetime,A.FTStateAppDate)  ,103)  +' ' +A.FTStateAppTime END  AS FTStateAppDataBy"
        _Str &= vbCrLf & " ,ISNULL(UA.FTUnitCode,'') AS FNHSysUnitIdA "
        _Str &= vbCrLf & " ,ISNULL(UB.FTUnitCode,'') AS FNHSysUnitIdB "
        _Str &= vbCrLf & " ,ISNULL(UC.FTUnitCode,'') AS FNHSysUnitIdC "
        _Str &= vbCrLf & " ,ISNULL(UD.FTUnitCode,'') AS FNHSysUnitIdD "
        _Str &= vbCrLf & " ,ISNULL(UE.FTUnitCode,'') AS FNHSysUnitIdE "
        _Str &= vbCrLf & " ,ISNULL(UF.FTUnitCode,'') AS FNHSysUnitIdF "
        _Str &= vbCrLf & " ,ISNULL(UG.FTUnitCode,'') AS FNHSysUnitIdG "
        _Str &= vbCrLf & " ,ISNULL(UH.FTUnitCode,'') AS FNHSysUnitIdH "
        _Str &= vbCrLf & " ,ISNULL(UI.FTUnitCode,'') AS FNHSysUnitIdI "
        _Str &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder] AS A"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdA )  AS UA"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdB )  AS UB"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdC )  AS UC"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdD )  AS UD"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdE )  AS UE"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdF )  AS UF"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdG )  AS UG"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdH )  AS UH"
        _Str &= vbCrLf & " outer apply ( select top 1  FTUnitCode from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnit] AS U WITH(NOLOCK) WHERE U.FNHSysUnitId = A.FNMatUnitIdI )  AS UI"


        _Str &= vbCrLf & "WHERE  A.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(Key.ToString) & "'"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum,, False)

        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns

                _FieldName = Col.ColumnName.ToString

                If _FieldName.ToLower = "FNRevisedNo".ToLower Then

                    opnrevised.Visible = (Val(R.Item(Col).ToString) > 0)
                    olbreviseno.Text = R.Item(Col).ToString

                Else

                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                        Select Case HI.ENM.Control.GeTypeControl(Obj)

                            Case ENM.Control.ControlType.PictureEdit

                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)

                                    Try
                                        .Image = HI.UL.ULImage.ConvertByteArrayToImmage(R.Item(Col))
                                    Catch ex As Exception
                                        .Image = Nothing
                                    End Try

                                End With

                            Case ENM.Control.ControlType.TextEdit

                                With CType(Obj, DevExpress.XtraEditors.TextEdit)

                                    .Text = R.Item(Col).ToString

                                End With
                            Case ENM.Control.ControlType.ButtonEdit

                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                    .Text = R.Item(Col).ToString

                                End With


                            Case ENM.Control.ControlType.CheckEdit

                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)

                                    Try
                                        .Checked = (R.Item(Col).ToString = "1")
                                    Catch ex As Exception
                                    End Try

                                End With
                            Case ENM.Control.ControlType.ComboBoxEdit

                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)

                                    Try
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    Catch ex As Exception
                                    End Try

                                End With
                        End Select

                    Next
                End If

            Next

        Next

        Call LoadDetail(Key.ToString)
        oxtb.SelectedTabPageIndex = 0

        _ProcLoad = False
        _FormLoad = False

    End Sub

    'Private Function CheckOwner() As Boolean
    '    If (HI.ST.UserInfo.UserName.ToUpper = FTPurchaseBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
    '        Return True
    '    Else
    '        HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
    '        Return False
    '    End If
    'End Function

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTSMPOrderBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTSMPOrderBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry2 &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
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

        opnrevised.Visible = False
        olbreviseno.Text = ""

        FNSMPPrototypeNo.Value = 0
        FNSMPPrototypeNo.Visible = True ' (FNSMPOrderType.SelectedIndex = 0)

        FTStateApp.Checked = False
        Call LoadDetail("")
        Call LoadSizeBreakdown()
        Call LoadRowMatDataFabric()

        oxtb.SelectedTabPageIndex = 0
        otbdetail.SelectedTabPageIndex = 0
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

        '----------Validate() Document ---------------------
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
                                'For Each ctrl As Object In Me.Controls.Find("FNHSysCmpId", True)

                                '    Select Case HI.ENM.Control.GeTypeControl(ctrl)
                                '        Case ENM.Control.ControlType.ButtonEdit
                                '            With CType(ctrl, DevExpress.XtraEditors.ButtonEdit)
                                '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                '            End With

                                '            Exit For
                                '        Case ENM.Control.ControlType.TextEdit
                                '            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                '                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                '            End With

                                '            Exit For
                                '    End Select

                                'Next

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

        If FTMatA.Text <> "" Then
            If FNMatQuantityA.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityA_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 0
                FNMatQuantityA.Focus()
                Return False
            End If

            If FNMatUnitIdA.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdA_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 0
                FNMatUnitIdA.Focus()
                Return False
            End If

            If FNHSysSuplIdA.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdA_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 0
                FNHSysSuplIdA.Focus()
                Return False
            End If

            If ogcpartmata.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 0

                Return False
            End If

            With CType(ogcpartmata.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 0
                    Return False
                End If
            End With
        End If

        If FTMatB.Text <> "" Then
            If FNMatQuantityB.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityB_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 1
                FNMatQuantityB.Focus()
                Return False
            End If

            If FNMatUnitIdB.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdB_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 1
                FNMatUnitIdB.Focus()
                Return False
            End If

            If FNHSysSuplIdB.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdB_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 1
                FNHSysSuplIdB.Focus()
                Return False
            End If

            If ogcpartmatb.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 1

                Return False
            End If

            With CType(ogcpartmatb.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 1
                    Return False
                End If
            End With
        End If


        If FTMatC.Text <> "" Then
            If FNMatQuantityC.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityC_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 2
                FNMatQuantityC.Focus()
                Return False
            End If

            If FNMatUnitIdC.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdC_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 2
                FNMatUnitIdC.Focus()
                Return False
            End If

            If FNHSysSuplIdC.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdC_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 2
                FNHSysSuplIdC.Focus()
                Return False
            End If

            If ogcpartmatc.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 2

                Return False
            End If

            With CType(ogcpartmatc.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 2
                    Return False
                End If
            End With
        End If

        If FTMatD.Text <> "" Then
            If FNMatQuantityD.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityD_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 3
                FNMatQuantityD.Focus()
                Return False
            End If

            If FNMatUnitIdD.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdD_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 3
                FNMatUnitIdD.Focus()
                Return False
            End If

            If FNHSysSuplIdD.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdD_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 3
                FNHSysSuplIdD.Focus()
                Return False
            End If

            If ogcpartmatd.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 3

                Return False
            End If

            With CType(ogcpartmatd.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 3
                    Return False
                End If
            End With
        End If

        If FTMatE.Text <> "" Then
            If FNMatQuantityE.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityE_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 4
                FNMatQuantityE.Focus()
                Return False
            End If

            If FNMatUnitIdE.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdE_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 4
                FNMatUnitIdE.Focus()
                Return False
            End If

            If FNHSysSuplIdE.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdE_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 4
                FNHSysSuplIdE.Focus()
                Return False
            End If

            If ogcpartmate.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 4

                Return False
            End If

            With CType(ogcpartmate.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 4
                    Return False
                End If
            End With
        End If

        If FTMatF.Text <> "" Then
            If FNMatQuantityF.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityF_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 5
                FNMatQuantityF.Focus()
                Return False
            End If

            If FNMatUnitIdF.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdF_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 5
                FNMatUnitIdF.Focus()
                Return False
            End If

            If FNHSysSuplIdF.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdF_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 5
                FNHSysSuplIdF.Focus()
                Return False
            End If

            If ogcpartmatf.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 5

                Return False
            End If

            With CType(ogcpartmatf.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 5
                    Return False
                End If
            End With
        End If

        If FTMatG.Text <> "" Then
            If FNMatQuantityG.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityG_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 6
                FNMatQuantityG.Focus()
                Return False
            End If

            If FNMatUnitIdG.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdG_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 6
                FNMatUnitIdG.Focus()
                Return False
            End If

            If FNHSysSuplIdG.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdG_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 6
                FNHSysSuplIdG.Focus()
                Return False
            End If

            If ogcpartmatg.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 6

                Return False
            End If

            With CType(ogcpartmatg.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 6
                    Return False
                End If
            End With
        End If

        If FTMatH.Text <> "" Then
            If FNMatQuantityH.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityH_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 7
                FNMatQuantityH.Focus()
                Return False
            End If

            If FNMatUnitIdH.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdH_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 7
                FNMatUnitIdH.Focus()
                Return False
            End If

            If FNHSysSuplIdH.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdH_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 7
                FNHSysSuplIdH.Focus()
                Return False
            End If

            If ogcpartmath.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 7

                Return False
            End If

            With CType(ogcpartmath.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 7
                    Return False
                End If
            End With
        End If

        If FTMatI.Text <> "" Then
            If FNMatQuantityI.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatQuantityI_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 8
                FNMatQuantityI.Focus()
                Return False
            End If

            If FNMatUnitIdI.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMatUnitIdI_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 8
                FNMatUnitIdI.Focus()
                Return False
            End If

            If FNHSysSuplIdI.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSuplIdI_lbl.Text)
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 8
                FNHSysSuplIdI.Focus()
                Return False
            End If

            If ogcpartmati.DataSource Is Nothing Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                otbdetail.SelectedTabPageIndex = 1
                oxtb.SelectedTabPageIndex = 8

                Return False
            End If

            With CType(ogcpartmati.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, "Data Position Part")
                    otbdetail.SelectedTabPageIndex = 1
                    oxtb.SelectedTabPageIndex = 8
                    Return False
                End If
            End With
        End If




        If Not (ogcacc.DataSource Is Nothing) Then

            With CType(ogcacc.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTMat<>'' AND FNHSysRawmatId_Hide >0  AND FNMatQuantity=0 ").Length > 0 Then

                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวนให้ครบ !!!", 112547814, Me.Text,, MessageBoxIcon.Warning)
                    otbdetail.SelectedTabPageIndex = 2

                    Return False
                End If

                If .Select("FTMat<>'' AND FNHSysRawmatId_Hide >0  AND FNHSysUnitId_Hide=0 ").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุหน่วยให้ครบ !!!", 112547815, Me.Text,, MessageBoxIcon.Warning)
                    otbdetail.SelectedTabPageIndex = 2

                    Return False
                End If

            End With

        End If

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

        Dim RevisedNo As Integer = 0
        Dim cmdstring As String = ""
        If FTStateApp.Checked = True Then

            cmdstring = "Select TOP  1 FNRevisedNo from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder AS X WITH(NOLOCK) WHERE  FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

            RevisedNo = (Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "0")) + 1)
        End If

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
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select

                                Next

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, "").ToString() Then
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

            Dim PTNo As Integer = 0

            cmdstring = "Select  Sum(1) As FNPTNo from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder AS X WITH(NOLOCK) WHERE  FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " ANd FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""

            PTNo = (Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "0")) + 1)

            If PTNo <= 0 Then
                PTNo = 1
            End If

            FNSMPPrototypeNo.Value = PTNo

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

                                    Case ENM.Control.ControlType.GridLookUpEdit

                                        With CType(Obj, DevExpress.XtraEditors.GridLookUpEdit)
                                            Try
                                                _Val = .EditValue.ToString
                                            Catch ex As Exception
                                                _Val = ""
                                            End Try

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
                                    Case ENM.Control.ControlType.GridLookUpEdit
                                        With CType(Obj, DevExpress.XtraEditors.GridLookUpEdit)
                                            Try
                                                _Val = .EditValue.ToString
                                            Catch ex As Exception
                                                _Val = ""
                                            End Try
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

                    _Str = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ",FNRevisedNo) VALUES (" & _Values & "," & RevisedNo & ")"

                Else

                    _Str = ""

                    If FTStateApp.Checked Then

                        _Str &= vbCrLf & "  INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Revised_History( "
                        _Str &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSMPOrderNo, FDSMPOrderDate, FTSMPOrderBy, FNSMPOrderType, FNSMPPrototypeNo, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, "
                        _Str &= vbCrLf & "   FNHSysSeasonId, FNHSysCustId, FNHSysMerTeamId, FTRemark, FTStateSendToSMP, FTSendToSMPBy, FDSendToSMPDate, FTSendToSMPTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FTStateEmb,"
                        _Str &= vbCrLf & "   FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTFabticA, FTFabticB, FTFabticC, FTFabticD, FTBlock, FTPattern, FTStateClose, FTStateCloseBy, FTStateCloseDate, FTStateCloseTime, FTStateReopenBy,"
                        _Str &= vbCrLf & "   FTStateReopenDate, FTStateReopenTime, FNHSysCmpIdCreate, FTStateFinish, FTStateFinishBy, FTStateFinishDate, FTStateFinishTime, FTStateCal, FTStateCalBy, FTStateCalDate, FTStateCalTime, FNRevisedNo  "

                        _Str &= vbCrLf & " ,FTStateApp,"
                        _Str &= vbCrLf & " FTStateAppBy, FTStateAppDate, FTStateAppTime, FTStateReceipt, FTStateReceiptBy, FTStateReceiptDate, FTStateReceiptTime, FNOrderSampleType, FTCustomerTeam, FNHSysGenderId, FTFabticE, FTFabticF, FTFabticG,"
                        _Str &= vbCrLf & "  FTFabticH, FTFabticI, FTMatA, FTMatNameA, FTMatColorA, FTMatColorNameA, FTMatSizeA, FNMatQuantityA, FTMatB, FTMatNameB, FTMatColorB, FTMatColorNameB, FTMatSizeB, FNMatQuantityB, FTMatC, FTMatNameC,"
                        _Str &= vbCrLf & "  FTMatColorC, FTMatColorNameC, FTMatSizeC, FNMatQuantityC, FTMatD, FTMatNameD, FTMatColorD, FTMatColorNameD, FTMatSizeD, FNMatQuantityD, FTMatE, FTMatNameE, FTMatColorE, FTMatColorNameE, FTMatSizeE,"
                        _Str &= vbCrLf & "  FNMatQuantityE, FTMatF, FTMatNameF, FTMatColorF, FTMatColorNameF, FTMatSizeF, FNMatQuantityF, FTMatG, FTMatNameG, FTMatColorG, FTMatColorNameG, FTMatSizeG, FNMatQuantityG, FTMatH, FTMatNameH,"
                        _Str &= vbCrLf & "  FTMatColorH, FTMatColorNameH, FTMatSizeH, FNMatQuantityH, FTMatI, FTMatNameI, FTMatColorI, FTMatColorNameI, FTMatSizeI, FNMatQuantityI, FNMatUnitIdA, FNMatUnitIdB, FNMatUnitIdC, FNMatUnitIdD, FNMatUnitIdE,"
                        _Str &= vbCrLf & "  FNMatUnitIdF, FNMatUnitIdG, FNMatUnitIdH, FNMatUnitIdI, FNSMPPriceType, FNSMPOrderStatus, FTStatePatternHard, FTStatePatternEPT, FTStatePatternGrandenest, FTStatePatternGradelogo, FTStatePattern3D,"
                        _Str &= vbCrLf & " FTStatePatternOptiplan, FTStatePatternOthers, FTPatternOthersNote, FNHSysSuplIdA, FNHSysSuplIdB, FNHSysSuplIdC, FNHSysSuplIdD, FNHSysSuplIdE, FNHSysSuplIdF, FNHSysSuplIdG, FNHSysSuplIdH,"
                        _Str &= vbCrLf & "  FNHSysSuplIdI "

                        _Str &= vbCrLf & "   ) "
                        _Str &= vbCrLf & "   Select "
                        _Str &= vbCrLf & "   '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS  FTInsUser"
                        _Str &= vbCrLf & "  , " & HI.UL.ULDate.FormatDateDB & " AS  FDInsDate"
                        _Str &= vbCrLf & "  , " & HI.UL.ULDate.FormatTimeDB & " AS  FTInsTime"
                        _Str &= vbCrLf & "  , FTUpdUser, FDUpdDate, FTUpdTime, FTSMPOrderNo, FDSMPOrderDate, FTSMPOrderBy, FNSMPOrderType, FNSMPPrototypeNo, FNHSysCmpId, FNHSysCmpRunId, FNHSysStyleId, "
                        _Str &= vbCrLf & "    FNHSysSeasonId, FNHSysCustId, FNHSysMerTeamId, FTRemark, FTStateSendToSMP, FTSendToSMPBy, FDSendToSMPDate, FTSendToSMPTime, FPOrderImage1, FPOrderImage2, FPOrderImage3, FPOrderImage4, FTStateEmb, "
                        _Str &= vbCrLf & "    FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTFabticA, FTFabticB, FTFabticC, FTFabticD, FTBlock, FTPattern, FTStateClose, FTStateCloseBy, FTStateCloseDate, FTStateCloseTime, FTStateReopenBy, "
                        _Str &= vbCrLf & "    FTStateReopenDate, FTStateReopenTime, FNHSysCmpIdCreate, FTStateFinish, FTStateFinishBy, FTStateFinishDate, FTStateFinishTime, FTStateCal, FTStateCalBy, FTStateCalDate, FTStateCalTime, ISNULL(FNRevisedNo, 0) "

                        _Str &= vbCrLf & " ,FTStateApp,"
                        _Str &= vbCrLf & " FTStateAppBy, FTStateAppDate, FTStateAppTime, FTStateReceipt, FTStateReceiptBy, FTStateReceiptDate, FTStateReceiptTime, FNOrderSampleType, FTCustomerTeam, FNHSysGenderId, FTFabticE, FTFabticF, FTFabticG,"
                        _Str &= vbCrLf & "  FTFabticH, FTFabticI, FTMatA, FTMatNameA, FTMatColorA, FTMatColorNameA, FTMatSizeA, FNMatQuantityA, FTMatB, FTMatNameB, FTMatColorB, FTMatColorNameB, FTMatSizeB, FNMatQuantityB, FTMatC, FTMatNameC,"
                        _Str &= vbCrLf & "  FTMatColorC, FTMatColorNameC, FTMatSizeC, FNMatQuantityC, FTMatD, FTMatNameD, FTMatColorD, FTMatColorNameD, FTMatSizeD, FNMatQuantityD, FTMatE, FTMatNameE, FTMatColorE, FTMatColorNameE, FTMatSizeE,"
                        _Str &= vbCrLf & "  FNMatQuantityE, FTMatF, FTMatNameF, FTMatColorF, FTMatColorNameF, FTMatSizeF, FNMatQuantityF, FTMatG, FTMatNameG, FTMatColorG, FTMatColorNameG, FTMatSizeG, FNMatQuantityG, FTMatH, FTMatNameH,"
                        _Str &= vbCrLf & "  FTMatColorH, FTMatColorNameH, FTMatSizeH, FNMatQuantityH, FTMatI, FTMatNameI, FTMatColorI, FTMatColorNameI, FTMatSizeI, FNMatQuantityI, FNMatUnitIdA, FNMatUnitIdB, FNMatUnitIdC, FNMatUnitIdD, FNMatUnitIdE,"
                        _Str &= vbCrLf & "  FNMatUnitIdF, FNMatUnitIdG, FNMatUnitIdH, FNMatUnitIdI, FNSMPPriceType, FNSMPOrderStatus, FTStatePatternHard, FTStatePatternEPT, FTStatePatternGrandenest, FTStatePatternGradelogo, FTStatePattern3D,"
                        _Str &= vbCrLf & " FTStatePatternOptiplan, FTStatePatternOthers, FTPatternOthersNote, FNHSysSuplIdA, FNHSysSuplIdB, FNHSysSuplIdC, FNHSysSuplIdD, FNHSysSuplIdE, FNHSysSuplIdF, FNHSysSuplIdG, FNHSysSuplIdH,"
                        _Str &= vbCrLf & "  FNHSysSuplIdI "


                        _Str &= vbCrLf & "  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder  "
                        _Str &= vbCrLf & "  where FTSMPOrderNo='" & _Key.ToString & "'"
                        _Str &= vbCrLf & "  INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Revised_History_Breakdown( "
                        _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTSMPOrderNo, FNRevisedNo,FTSizeBreakDown, FNSeq, FTColorway, FNQuantity, FTDeliveryDate, FTRemark "
                        _Str &= vbCrLf & "   ) "

                        _Str &= vbCrLf & "   Select  B.FTInsUser, B.FDInsDate, B.FTInsTime, B.FTUpdUser, B.FDUpdDate, B.FTUpdTime, B.FTSMPOrderNo, ISNULL(a.FNRevisedNo,0), B.FTSizeBreakDown, B.FNSeq, B.FTColorway, B.FNQuantity, B.FTDeliveryDate, B.FTRemark"
                        _Str &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder As a INNER Join"
                        _Str &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Breakdown As B On a.FTSMPOrderNo = B.FTSMPOrderNo"
                        _Str &= vbCrLf & " where A.FTSMPOrderNo='" & _Key.ToString & "'"

                    End If

                    _Str &= vbCrLf & "  Update  " & _FormHeader(cind).TableName & " Set " & _Values & ",FNRevisedNo=" & RevisedNo & "  WHERE  " & _FormHeader(cind).MainKey & "='" & _Key.ToString & "' "

                End If

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            Dim MatCode As String = ""
            Dim MatName As String = ""
            Dim MatColor As String = ""
            Dim MatColorName As String = ""
            Dim MatSize As String = ""
            Dim MAtQty As Double = 0
            Dim MAtUnit As Integer = 0
            Dim MAtSupl As Integer = 0
            Dim SysRawmatId As Integer = 0
            For I As Integer = 0 To 8

                MatCode = ""
                SysRawmatId = 0
                Select Case I
                    Case 0
                        MatCode = FTMatA.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameA.Text.Trim()
                            MatColor = FTMatColorA.Text.Trim()
                            MatColorName = FTMatColorNameA.Text.Trim()
                            MatSize = FTMatSizeA.Text.Trim()
                            MAtQty = FNMatQuantityA.Value
                            MAtUnit = Val(FNMatUnitIdA.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdA.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdA.EditValue.ToString())

                        End If

                    Case 1

                        MatCode = FTMatB.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameB.Text.Trim()
                            MatColor = FTMatColorB.Text.Trim()
                            MatColorName = FTMatColorNameB.Text.Trim()
                            MatSize = FTMatSizeB.Text.Trim()
                            MAtQty = FNMatQuantityB.Value
                            MAtUnit = Val(FNMatUnitIdB.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdB.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdB.EditValue.ToString())
                        End If


                    Case 2

                        MatCode = FTMatC.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameC.Text.Trim()
                            MatColor = FTMatColorC.Text.Trim()
                            MatColorName = FTMatColorNameC.Text.Trim()
                            MatSize = FTMatSizeC.Text.Trim()
                            MAtQty = FNMatQuantityC.Value
                            MAtUnit = Val(FNMatUnitIdC.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdC.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdC.EditValue.ToString())
                        End If

                    Case 3
                        MatCode = FTMatD.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameD.Text.Trim()
                            MatColor = FTMatColorD.Text.Trim()
                            MatColorName = FTMatColorNameD.Text.Trim()
                            MatSize = FTMatSizeD.Text.Trim()
                            MAtQty = FNMatQuantityD.Value
                            MAtUnit = Val(FNMatUnitIdD.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdD.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdD.EditValue.ToString())
                        End If
                    Case 4
                        MatCode = FTMatE.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameE.Text.Trim()
                            MatColor = FTMatColorE.Text.Trim()
                            MatColorName = FTMatColorNameE.Text.Trim()
                            MatSize = FTMatSizeE.Text.Trim()
                            MAtQty = FNMatQuantityE.Value
                            MAtUnit = Val(FNMatUnitIdE.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdE.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdE.EditValue.ToString())
                        End If
                    Case 5
                        MatCode = FTMatF.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameF.Text.Trim()
                            MatColor = FTMatColorF.Text.Trim()
                            MatColorName = FTMatColorNameF.Text.Trim()
                            MatSize = FTMatSizeF.Text.Trim()
                            MAtQty = FNMatQuantityF.Value
                            MAtUnit = Val(FNMatUnitIdF.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdF.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdF.EditValue.ToString())
                        End If
                    Case 6
                        MatCode = FTMatG.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameG.Text.Trim()
                            MatColor = FTMatColorG.Text.Trim()
                            MatColorName = FTMatColorNameG.Text.Trim()
                            MatSize = FTMatSizeG.Text.Trim()
                            MAtQty = FNMatQuantityG.Value
                            MAtUnit = Val(FNMatUnitIdG.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdG.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdG.EditValue.ToString())
                        End If
                    Case 7
                        MatCode = FTMatH.Text.Trim()

                        If MatCode <> "" Then
                            MatName = FTMatNameH.Text.Trim()
                            MatColor = FTMatColorH.Text.Trim()
                            MatColorName = FTMatColorNameH.Text.Trim()
                            MatSize = FTMatSizeH.Text.Trim()
                            MAtQty = FNMatQuantityH.Value
                            MAtUnit = Val(FNMatUnitIdH.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdH.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdH.EditValue.ToString())
                        End If
                    Case 8
                        MatCode = FTMatI.Text.Trim()

                        If MatCode <> "" Then

                            MatName = FTMatNameI.Text.Trim()
                            MatColor = FTMatColorI.Text.Trim()
                            MatColorName = FTMatColorNameI.Text.Trim()
                            MatSize = FTMatSizeI.Text.Trim()
                            MAtQty = FNMatQuantityI.Value
                            MAtUnit = Val(FNMatUnitIdI.Properties.Tag.ToString())
                            MAtSupl = Val(FNHSysSuplIdI.Properties.Tag.ToString())
                            SysRawmatId = Val(FNHSysRawmatIdI.EditValue.ToString())

                        End If

                End Select

                If MatCode <> "" Then

                    _Str = " UPDATE " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_FabricMatList] SET "
                    _Str &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",FTMat='" & HI.UL.ULF.rpQuoted(MatCode) & "'"
                    _Str &= vbCrLf & ",FTMatName='" & HI.UL.ULF.rpQuoted(MatName) & "'"
                    _Str &= vbCrLf & ",FTMatColor='" & HI.UL.ULF.rpQuoted(MatColor) & "'"
                    _Str &= vbCrLf & ",FTMatColorName='" & HI.UL.ULF.rpQuoted(MatColorName) & "'"
                    _Str &= vbCrLf & ",FTMatSize='" & HI.UL.ULF.rpQuoted(MatSize) & "'"
                    _Str &= vbCrLf & ",FNMatQuantity=" & Val(MAtQty) & ""
                    _Str &= vbCrLf & ",FNHSysUnitId=" & Val(MAtUnit) & ""
                    _Str &= vbCrLf & ",FNHSysSuplId=" & Val(MAtSupl) & ""
                    _Str &= vbCrLf & ",FNHSysRawmatId=" & Val(SysRawmatId) & ""

                    _Str &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_Key) & "' AND  FNMatSeq = " & I & " "

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Str = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_FabricMatList ("
                        _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo,FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName,FTMatSize,FNMatQuantity,FNHSysUnitId,FTRemark,FNHSysSuplId,FNHSysRawmatId "
                        _Str &= vbCrLf & " )"
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Str &= vbCrLf & "," & I & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatCode) & "'"
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatName) & "'"
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatColor) & "'"
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatColorName) & "'"
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(MatSize) & "'"
                        _Str &= vbCrLf & "," & Val(MAtQty) & ""
                        _Str &= vbCrLf & "," & Val(MAtUnit) & ""
                        _Str &= vbCrLf & ",''"
                        _Str &= vbCrLf & "," & Val(MAtSupl) & ""
                        _Str &= vbCrLf & "," & Val(SysRawmatId) & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Else
                    _Str = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_FabricMatList]  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_Key) & "' AND  FNMatSeq = " & I & " "
                    HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If

            Next

            _Str = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_Breakdown]  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Dim dt As DataTable

            With CType(ogdBreakdown.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With
            Dim _DSeq As Integer = 0

            For Each R As DataRow In dt.Select("FTSelect='1' AND FNQuantity>0 AND FTColorway<>'' ", "FNSeq,FNDataSeq")

                _DSeq = _DSeq + 1

                _Str = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Breakdown ("
                _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTSizeBreakDown, FNSeq, FTColorway, FNQuantity, FTDeliveryDate, FTRemark,FTPatternDate,FTFabricDate,FTAccessoryDate "
                _Str &= vbCrLf & " , FNPrice, FNAmt, FNFreeQuantity, FNDebitQuantity,FTOGACDate,FTGACDate)"
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'"
                _Str &= vbCrLf & "," & _DSeq & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "'"
                _Str &= vbCrLf & "," & Val(R!FNQuantity.ToString) & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTDeliveryDate.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTPatternDate.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTFabricDate.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTAccessoryDate.ToString) & "'"
                _Str &= vbCrLf & "," & Val(R!FNPrice.ToString) & ""
                _Str &= vbCrLf & "," & Val(R!FNAmt.ToString) & ""
                _Str &= vbCrLf & "," & Val(R!FNFreeQuantity.ToString) & ""
                _Str &= vbCrLf & "," & Val(R!FNDebitQuantity.ToString) & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTOGACDate.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTGACDate.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next
            dt.Dispose()


            _Str = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_MatPart]  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = " DELETE FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder_MatList]  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Dim dtacc As DataTable

            With CType(ogcacc.DataSource, DataTable)
                .AcceptChanges()
                dtacc = .Copy
            End With
            _DSeq = 0

            Dim _FNAllPart As String = ""
            For Each R As DataRow In dtacc.Select("FTMat<>'' AND FNHSysRawmatId_Hide >0 ", "FNMatSeq")

                _DSeq = _DSeq + 1


                _FNAllPart = R!FNAllPart.ToString()

                _Str = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatList ("
                _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo,FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName,FTMatSize,FNMatQuantity,FNHSysUnitId,FTRemark,FNHSysSuplId,FNHSysRawmatId "
                _Str &= vbCrLf & " )"
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                _Str &= vbCrLf & "," & _DSeq & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMat.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMatName.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMatColor.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMatColorName.ToString) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMatSize.ToString) & "'"
                _Str &= vbCrLf & "," & Val(R!FNMatQuantity.ToString) & ""
                _Str &= vbCrLf & "," & Val(R!FNHSysUnitId_Hide.ToString) & ""
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"
                _Str &= vbCrLf & "," & Val(R!FNHSysSuplId_Hide.ToString) & ""
                _Str &= vbCrLf & "," & Val(R!FNHSysRawmatId_Hide.ToString) & ""

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If


                If _FNAllPart <> "" Then
                    For Each Str As String In _FNAllPart.Split(",")

                        _Str = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatPart ("
                        _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTSMPOrderNo, FNMatSeq, FNMat, FNHSysPartId "
                        _Str &= vbCrLf & " )"
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Str &= vbCrLf & "," & _DSeq & ""
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMat.ToString) & "' As FNMat"
                        _Str &= vbCrLf & "," & Val(Str) & ""

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Return False

                        End If

                    Next
                End If

            Next


            dtacc.Dispose()


            Dim dtmatpart As New DataTable

            For MatIndex As Integer = 0 To 8

                Select Case MatIndex
                    Case 0
                        With CType(ogcpartmata.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                    Case 1

                        With CType(ogcpartmatb.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                    Case 2

                        With CType(ogcpartmatc.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With
                    Case 3

                        With CType(ogcpartmatd.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With
                    Case 4

                        With CType(ogcpartmate.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                    Case 5

                        With CType(ogcpartmatf.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                    Case 6

                        With CType(ogcpartmatg.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                    Case 7

                        With CType(ogcpartmath.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                    Case 8

                        With CType(ogcpartmati.DataSource, DataTable)
                            .AcceptChanges()
                            dtmatpart = .Copy
                        End With

                End Select


                For Each R As DataRow In dtmatpart.Rows

                    _Str = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatPart ("
                    _Str &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime,  FTSMPOrderNo, FNMatSeq, FNMat, FNHSysPartId "
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                    _Str &= vbCrLf & ",0 As FNMatSeq"
                    _Str &= vbCrLf & ",'" & MatIndex & "' As FNMat"
                    _Str &= vbCrLf & "," & Val(R!FNHSysPartId.ToString) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Return False

                    End If

                Next

            Next

            dtmatpart.Dispose()

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Dim _Qry As String = ""
            If Not (ogc.DataSource Is Nothing) Then

                Dim dtsetpart As DataTable
                With CType(ogc.DataSource, DataTable)
                    .AcceptChanges()
                    dtsetpart = .Copy
                End With


                If dtsetpart.Rows.Count > 0 Then
                    For Each R As DataRow In dtsetpart.Rows

                        _Qry = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart "
                        _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ,FTStateEmb='" & R!FTStateEmb.ToString & "'"
                        _Qry &= vbCrLf & " ,FTStatePrint='" & R!FTStatePrint.ToString & "'"
                        _Qry &= vbCrLf & " ,FTStateHeat='" & R!FTStateHeat.ToString & "'"
                        _Qry &= vbCrLf & " ,FTStateLaser='" & R!FTStateLaser.ToString & "'"
                        _Qry &= vbCrLf & " ,FTStateWindows='" & R!FTStateWindows.ToString & "'"
                        _Qry &= vbCrLf & ", FTEmbNote='" & HI.UL.ULF.rpQuoted(R!FTEmbNote.ToString) & "'"
                        _Qry &= vbCrLf & " , FTPrintNote='" & HI.UL.ULF.rpQuoted(R!FTPrintNote.ToString) & "'"
                        _Qry &= vbCrLf & " , FTHeatNote='" & HI.UL.ULF.rpQuoted(R!FTHeatNote.ToString) & "'"
                        _Qry &= vbCrLf & " , FTLaserNote='" & HI.UL.ULF.rpQuoted(R!FTLaserNote.ToString) & "'"
                        _Qry &= vbCrLf & " , FTWindowsNote='" & HI.UL.ULF.rpQuoted(R!FTWindowsNote.ToString) & "'"
                        _Qry &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_Key) & "'"
                        _Qry &= vbCrLf & "   And FNHSysPartId =" & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""


                        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE) = False Then
                            _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart"
                            _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNHSysPartId, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser,FTStateWindows "
                            _Qry &= vbCrLf & ", FTEmbNote, FTPrintNote , FTHeatNote, FTLaserNote"
                            _Qry &= vbCrLf & ", FTWindowsNote"


                            _Qry &= vbCrLf & ")"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(R!FNHSysPartId.ToString)) & ""
                            _Qry &= vbCrLf & ",'" & R!FTStateEmb.ToString & "'"
                            _Qry &= vbCrLf & ",'" & R!FTStatePrint.ToString & "'"
                            _Qry &= vbCrLf & ",'" & R!FTStateHeat.ToString & "'"
                            _Qry &= vbCrLf & ",'" & R!FTStateLaser.ToString & "'"
                            _Qry &= vbCrLf & ",'" & R!FTStateWindows.ToString & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTEmbNote.ToString) & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPrintNote.ToString) & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTHeatNote.ToString) & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTLaserNote.ToString) & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTWindowsNote.ToString) & "'"


                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
                        End If


                    Next


                End If
            End If


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()

            _Str = ""
            _Str = "UPDATE A"
            _Str &= Environment.NewLine & "SET A.[FPOrderImage1] = @FPOrderImage1,"
            _Str &= Environment.NewLine & "    A.[FPOrderImage2] = @FPOrderImage2,"
            _Str &= Environment.NewLine & "    A.[FPOrderImage3] = @FPOrderImage3,"
            _Str &= Environment.NewLine & "    A.[FPOrderImage4] = @FPOrderImage4"
            _Str &= Environment.NewLine & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder] AS A"
            _Str &= Environment.NewLine & "WHERE  A.FTSMPOrderNo = @FTSMPOrderNo"

            Dim cmd As New System.Data.SqlClient.SqlCommand(_Str, HI.Conn.SQLConn.Cnn)

            cmd.Parameters.AddWithValue("FTSMPOrderNo", _Key)
            Dim data1 As Byte() = HI.UL.ULImage.ConvertImageToByteArray(FPOrderImage1.Image, UL.ULImage.PicType.Employee)
            Dim data2 As Byte() = HI.UL.ULImage.ConvertImageToByteArray(FPOrderImage2.Image, UL.ULImage.PicType.Employee)
            Dim data3 As Byte() = HI.UL.ULImage.ConvertImageToByteArray(FPOrderImage3.Image, UL.ULImage.PicType.Employee)
            Dim data4 As Byte() = HI.UL.ULImage.ConvertImageToByteArray(FPOrderImage4.Image, UL.ULImage.PicType.Employee)

            cmd.Parameters.AddWithValue("FPOrderImage1", data1)
            cmd.Parameters.AddWithValue("FPOrderImage2", data2)
            cmd.Parameters.AddWithValue("FPOrderImage3", data3)
            cmd.Parameters.AddWithValue("FPOrderImage4", data4)

            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)


            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select

            Next

            opnrevised.Visible = (RevisedNo > 0)
            olbreviseno.Text = RevisedNo.ToString

            If otbdetail.SelectedTabPage.Name <> otpsetpart.Name Then
                Call SetDataInfo(_Key)
            End If

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
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Breakdown WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatList WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Revised_History WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Revised_History_Breakdown WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_File WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatPart WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_SetPart WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _Str = "Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_FabricMatList WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'"

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " Delete From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text) & "'")

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


        oxtb.SelectedTabPageIndex = 0
        LoadDetail("")

        LoadRowMatDataFabric()

        _FormLoad = False

    End Sub

#End Region

#Region "MAIN PROC"

    Private Function CheckReceive(POKey As String) As Boolean
        Dim _Pass As Boolean = True
        Dim _Str As String = ""

        _Str = "Select TOP 1 FTSMPOrderNo FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPSampleTeam As R WITH(NOLOCK) WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(POKey) & "'  "

        If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SAMPLE, "") <> "" Then

            HI.MG.ShowMsg.mProcessError(1303150001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            _Pass = False

        End If

        Return _Pass

    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        ' If CheckOwner() = False Then Exit Sub

        'If FTSMPOrderNo.Text <> "" Then
        '    If (CheckReceive(Me.FTSMPOrderNo.Text) = False) Then Exit Sub
        'End If
        If CheckOwner() = False Then Exit Sub
        If Me.VerrifyData Then

            Dim dt As DataTable

            With CType(ogdBreakdown.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy

            End With


            If dt.Select("FTSelect='1' AND FNQuantity>0 AND FTColorway<>''").Length > 0 Then


                If FTStateApp.Checked = True Then

                    If HI.MG.ShowMsg.mConfirmProcess("เอกสารมีการยืนยันแล้ว หากต้องการบันทึกจะเป็นการ Revised เอกสาร คุณต้องการทำการ Revised ใช่หรือไม่ ?", 18102205547) = False Then
                        Exit Sub
                    End If
                End If

                If Me.SaveData() Then

                    Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Color way และ Delivery Date", 1809220547, Me.Text,, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

        If FTSMPOrderNo.Text <> "" Then
            If (CheckReceive(Me.FTSMPOrderNo.Text) = False) Then Exit Sub
        End If
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTSMPOrderNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        Dim smporder As String = Me.FTSMPOrderNo.Text

        If Me.DeleteData() Then

            Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(smporder) & "'"
            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()
            Me.FormRefresh()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        oxtb.SelectedTabPageIndex = 0

    End Sub

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTSMPOrderNo.Text <> "" Then

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Merchandise Report\"
                .ReportName = "OrderSampleReport.rpt"
                .Formular = "{TSMPOrder.FTSMPOrderNo} ='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Merchandise Report\"
                .ReportName = "OrderSampleReport_A.rpt"
                .Formular = "{TSMPOrder.FTSMPOrderNo} ='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

#End Region

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load



        _FormLoad = False


        ogvacc.OptionsView.ShowAutoFilterRow = False
        ogvBreakdown.OptionsView.ShowAutoFilterRow = False

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvBreakdown.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvacc.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next

        Call LoadRowMatDataFabric(True)

        LoadDetail("")

    End Sub

    Private Sub LoadDetail(ByVal _DocRefNo As String)

        Dim cmd As String = ""
        Dim dt As DataTable

        cmd = "  Select '1' AS FTSelect "
        cmd &= vbCrLf & "    , B.FTSizeBreakDown"
        cmd &= vbCrLf & "  ,ISNULL(B.FNQuantity,0) As FNQuantity "
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTDeliveryDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTDeliveryDate),103) ELSE '' END AS FTDeliveryDate"
        cmd &= vbCrLf & " ,ISNULL(B.FTRemark,'') AS FTRemark"

        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTPatternDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTPatternDate),103) ELSE '' END AS FTPatternDate"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTFabricDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTFabricDate),103) ELSE '' END AS FTFabricDate"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTAccessoryDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTAccessoryDate),103) ELSE '' END AS FTAccessoryDate"

        cmd &= vbCrLf & " ,B.FNSeq AS FNDataSeq,A.FNSeq"

        cmd &= vbCrLf & "  ,ISNULL(B.FNPrice,0) As FNPrice "
        cmd &= vbCrLf & "  ,ISNULL(B.FNAmt,0) As FNAmt "
        cmd &= vbCrLf & "  ,ISNULL(B.FNFreeQuantity,0) As FNFreeQuantity "
        cmd &= vbCrLf & "  ,ISNULL(B.FNDebitQuantity,0) As FNDebitQuantity "

        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTOGACDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTOGACDate),103) ELSE '' END AS FTOGACDate"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTGACDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTGACDate),103) ELSE '' END AS FTGACDate"

        cmd &= vbCrLf & " FROM (Select FNListIndex + 1 As FNSeq, FTNameEN As FTSizeBreakDown"
        cmd &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData AS X WITH(NOLOCK)"
        cmd &= vbCrLf & "  Where (FTListName = N'FNSMPOrderSize')"
        cmd &= vbCrLf & " ) As A INNER JOIN "
        cmd &= vbCrLf & "(SELECT '1' AS FTSelect ,X2.FTSizeBreakDown,X2.FTColorway,X2.FNQuantity,X2.FTDeliveryDate,X2.FTRemark,X2.FTPatternDate,X2.FTFabricDate,X2.FTAccessoryDate,X2.FNSeq"
        cmd &= vbCrLf & " , X2.FNPrice , X2.FNAmt , X2.FNFreeQuantity , X2.FNDebitQuantity,X2.FTOGACDate,X2.FTGACDate"
        cmd &= vbCrLf & " From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        'cmd &= vbCrLf & " LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrderMasterPlan AS X22 WITH(NOLOCK)"
        'cmd &= vbCrLf & " ON X2.FTSMPOrderNo = X22.FTSMPOrderNo AND X2.FTSizeBreakDown = X22.FTSizeBreakDown  AND X2.FTColorway = X22.FTColorway  "
        cmd &= vbCrLf & " Where X2.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "'"
        cmd &= vbCrLf & ") AS B ON A.FTSizeBreakDown=B.FTSizeBreakDown"
        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogdBreakdown.DataSource = dt.Copy

        dt.Dispose()

        Call LoadPartMat(_DocRefNo)
        Call LoadMatAccDetail(_DocRefNo)
        Call SetDataInfo(_DocRefNo)
        Call LoadFileRef(_DocRefNo)


    End Sub

    Private Sub LoadFileRef(ByVal _DocRefNo As String)

        Dim cmd As String = ""
        Dim dt As DataTable

        cmd = "select FNFileSeq, FTFileName"
        cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_File  with(nolock)"
        cmd &= vbCrLf & " Where FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "'"
        cmd &= vbCrLf & " ORDER BY FNFileSeq "


        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcfile.DataSource = dt.Copy

        dt.Dispose()

    End Sub
    Private Sub LoadMatAccDetail(ByVal _DocRefNo As String)

        Dim cmd As String = ""
        Dim dt As DataTable


        cmd = "CREATE TABLE #MainPart (FTPartCode nvarchar(30),FNHSysPartId int,FTPartName nvarchar(200),FNMat nvarchar(30))CREATE INDEX [IDX_TmpMainPart] On #MainPart(FTPartCode,FNHSysPartId) "
        cmd &= vbCrLf & "CREATE TABLE #AccPartinfo (FTMat nvarchar(30),FTPart nvarchar(2000),FNAllPart nvarchar(2000))CREATE INDEX [IDX_TmpAccPartinfo] ON #AccPartinfo(FTMat)  "
        cmd &= vbCrLf & " INSERT INTO #MainPart(FNHSysPartId, FTPartCode, FTPartName, FNMat) "
        cmd &= vbCrLf & "Select P.FNHSysPartId,A.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            cmd &= vbCrLf & ",A.FTPartNameTH As FTPartName"

        Else

            cmd &= vbCrLf & ",A.FTPartNameEN As FTPartName"

        End If

        cmd &= vbCrLf & " ,P.FNMat"
        cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatPart As P With(NOLOCK) "
        cmd &= vbCrLf & "       INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart As A With(NOLOCK) On P.FNHSysPartId= A.FNHSysPartId"
        cmd &= vbCrLf & " WHERE P.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "' AND P.FNMatSeq > 0"
        cmd &= vbCrLf & "  INSERT INTO #AccPartinfo(FTMat, FTPart, FNAllPart)  "
        cmd &= vbCrLf & " Select  FNMat "
        cmd &= vbCrLf & " , ISNULL((   "
        cmd &= vbCrLf & "  Select  STUFF((Select  ',' + FTPartName  "
        cmd &= vbCrLf & "   From(SELECT      Distinct FTPartName "
        cmd &= vbCrLf & "   From  #MainPart  As XX   With(NOLOCK) "
        cmd &= vbCrLf & " where     (XX.FNMat=A.FNMat) "
        cmd &= vbCrLf & "   ) As T  "
        cmd &= vbCrLf & "	For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  "
        cmd &= vbCrLf & "),'')    AS FTPart  "
        cmd &= vbCrLf & " , ISNULL((   "
        cmd &= vbCrLf & "  Select  STUFF((Select  ',' + FNHSysPartId  "
        cmd &= vbCrLf & "   From(SELECT      Distinct Convert(varchar(30),FNHSysPartId)  as FNHSysPartId "
        cmd &= vbCrLf & "   From  #MainPart  As XX   With(NOLOCK) "
        cmd &= vbCrLf & "    where     (XX.FNMat=A.FNMat) "
        cmd &= vbCrLf & "   ) As T  "
        cmd &= vbCrLf & "	For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  "
        cmd &= vbCrLf & "),'')    AS FNAllPart  "
        cmd &= vbCrLf & "   From #MainPart  AS A   WITH(NOLOCK) "
        cmd &= vbCrLf & " GROUP BY FNMat "

        cmd &= vbCrLf & "  Select  '0' AS FTSelect,Row_NUmber() over (Order By FNMatSeq) As FNMatSeq "
        cmd &= vbCrLf & "    , X2.FTMat"
        cmd &= vbCrLf & "    , X2.FTMatName"
        cmd &= vbCrLf & "    , X2.FTMatColor"
        cmd &= vbCrLf & "    , X2.FTMatColorName"
        cmd &= vbCrLf & "    , X2.FTMatSize"
        cmd &= vbCrLf & "    , X2.FNMatQuantity"
        cmd &= vbCrLf & "    , ISNULL(U.FTUnitCode,'') AS FNHSysUnitId"
        cmd &= vbCrLf & "    , X2.FNHSysUnitId AS FNHSysUnitId_Hide"

        cmd &= vbCrLf & "    , ISNULL(SPL.FTSuplCode,'')  AS FNHSysSuplId "
        cmd &= vbCrLf & "    , ISNULL(X2.FNHSysSuplId,0)  AS FNHSysSuplId_Hide "

        cmd &= vbCrLf & "    , ISNULL(X2.FTRemark,'') AS FTRemark "

        cmd &= vbCrLf & "    , ISNULL(Z.FTPart,'') AS FTPart "
        cmd &= vbCrLf & "    , ISNULL(Z.FNAllPart,'')  AS FNAllPart "

        cmd &= vbCrLf & "    , MMX.FTItemDataRef AS FTItemDataRef "
        cmd &= vbCrLf & "    , X2.FNHSysRawmatId  AS FNHSysRawmatId_Hide"

        cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatList AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & " LEFT Outer join " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnit AS U WITH(NOLOCK)  ON X2.FNHSysUnitId = U.FNHSysUnitId "
        cmd &= vbCrLf & "  LEFT Outer join  #AccPartinfo as Z ON X2.FTMat = Z.FTMat   "
        cmd &= vbCrLf & "  LEFT Outer join  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSupplier AS SPL WITH(NOLOCK)  ON X2.FNHSysSuplId = SPL.FNHSysSuplId "
        cmd &= vbCrLf & " OUTER APPLY (Select      H.FTRawMatCode + '|'+  ISNULL(C.FTRawMatColorCode,'') +'|' + ISNULL(S.FTRawMatSizeCode,'') AS FTItemDataRef "
        cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As H With (NOLOCK) LEFT OUTER Join"
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON H.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER Join"
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As C With (NOLOCK) On H.FNHSysRawMatColorId = C.FNHSysRawMatColorId "
        cmd &= vbCrLf & "  WHERE  H.FNHSysRawMatId =  X2.FNHSysRawmatId  ) AS MMX "
        cmd &= vbCrLf & " Where X2.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(_DocRefNo) & "'"

        cmd &= vbCrLf & " ORDER BY X2.FNMatSeq "
        cmd &= vbCrLf & "  drop table   #MainPart  drop table #AccPartinfo "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcacc.DataSource = dt.Copy

        dt.Dispose()

        Call InitGridDataAcc()

    End Sub

    Private Sub FNHSysSuplId_EditValueChanged(sender As Object, e As EventArgs)

        'Me.FNHSysCurId.Text = HI.Conn.SQLConn.GetField("Select TOP 1 FTCurCode FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TFINMCurrency With(NOLOCK) WHERE FTStateLocal='1' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Me.FNExchangeRate.Value = 1

    End Sub

    Private Sub FNExchangeRate_EditValueChanged(sender As Object, e As EventArgs)
        'Try
        '    If FNExchangeRate.Value <> 1 Then
        '        FNExchangeRate.Value = 1
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs)
        If CheckOwner() = False Then Exit Sub

        If FTSMPOrderNo.Text <> "" Then
            If (CheckReceive(Me.FTSMPOrderNo.Text) = False) Then Exit Sub
        End If

    End Sub

    Private Sub FNSMPOrderType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNSMPOrderType.SelectedIndexChanged

        FNSMPPrototypeNo.Value = 0
        FNSMPPrototypeNo.Visible = True ' (FNSMPOrderType.SelectedIndex = 0)

    End Sub

    Private Sub RepFTSelect_EditValueChanged(sender As Object, e As EventArgs) Handles RepFTSelect.EditValueChanged

    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelect.EditValueChanging
        With ogvBreakdown

            If e.NewValue.ToString = "1" Then

            Else
                .SetFocusedRowCellValue("FNQuantity", 0)
                .SetFocusedRowCellValue("FTColorway", "")
                .SetFocusedRowCellValue("FTDeliveryDate", "")
                .SetFocusedRowCellValue("FTRemark", "")
            End If
        End With
    End Sub

    Private Sub ogvBreakdown_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvBreakdown.ShowingEditor
        With ogvBreakdown

            If .FocusedColumn.FieldName = "FTSelect" Then
                e.Cancel = False
            Else
                If .GetFocusedRowCellValue("FTSelect").ToString = "1" Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If

            End If

        End With
    End Sub

    Private Sub ocmsaveconfirm_Click(sender As Object, e As EventArgs) Handles ocmconfirm.Click
        If FTSMPOrderNo.Text <> "" Then

            If FTStateApp.Checked = True Then Exit Sub

            If Me.VerrifyData Then

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ อนุมัติยืนยัน ใบขอตัวอย่างใช่หรือไม่ ?", 125587415) = False Then
                    Exit Sub
                End If

                Dim _Str As String = ""
                _Str = "UPDATE A SET FTStateApp='1',FTStateAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTStateAppDate=" & HI.UL.ULDate.FormatDateDB & ",FTStateAppTime=" & HI.UL.ULDate.FormatTimeDB & ",FNSMPOrderStatus=1 "
                _Str &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder AS A "
                _Str &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text.Trim) & "'  "


                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SAMPLE) Then
                    FTStateApp.Checked = True
                    FNSMPOrderStatus.SelectedIndex = 1
                    Dim _Dt As DataTable
                    Dim _FieldName As String = ""

                    _Str = ""
                    _Str = " Select TOP 1  CASE WHEN ISNULL(FTStateAppBy,'') ='' THEN '' ELSE FTStateAppBy + '  ' + Convert(nvarchar(10), convert(datetime,FTStateAppDate)  ,103)  +' ' +FTStateAppTime END  AS FTStateAppDataBy "
                    _Str &= Environment.NewLine & " FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder] AS A"
                    _Str &= Environment.NewLine & " WHERE  A.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text.Trim) & "'"

                    _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                    For Each R As DataRow In _Dt.Rows
                        For Each Col As DataColumn In _Dt.Columns

                            _FieldName = Col.ColumnName.ToString

                            For Each Obj As Object In Me.Controls.Find(_FieldName, True)

                                Select Case HI.ENM.Control.GeTypeControl(Obj)
                                    Case ENM.Control.ControlType.TextEdit

                                        With CType(Obj, DevExpress.XtraEditors.TextEdit)

                                            .Text = R.Item(Col).ToString

                                        End With

                                    Case ENM.Control.ControlType.CheckEdit

                                        With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                            Try
                                                .Checked = (R.Item(Col).ToString = "1")
                                            Catch ex As Exception
                                            End Try
                                        End With

                                End Select

                            Next

                        Next

                    Next

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                End If

            End If
        End If

    End Sub

    Private Sub FTSMPOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTSMPOrderNo.EditValueChanged

    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        If FNHSysStyleId.Text.Trim() <> "" Then
            Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

            Call LoadImangeStyle(Integer.Parse(Val(FNHSysStyleId.Properties.Tag)))

        End If
    End Sub

    Private Sub LoadImangeStyle(_FNHSysStyleId As Integer)
        Try

            If Me.FTSMPOrderNo.Text <> "" And Me.FTSMPOrderNo.Properties.Tag.ToString = "" Then
                Dim _Qry As String = ""
                Dim dt As DataTable
                _Qry = "SELECT  TOP 1   FNHSysStyleId,  FPStyleImage1,FPStyleImage2, FPStyleImage3, FPStyleImage4"
                _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & _FNHSysStyleId & ""
                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER,, False)

                For Each Rx As DataRow In dt.Rows

                    Try
                        Me.FPOrderImage1.Image = HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage1)
                    Catch ex As Exception
                        Me.FPOrderImage1.Image = Nothing
                    End Try

                    Try
                        Me.FPOrderImage2.Image = HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage2)
                    Catch ex As Exception
                        Me.FPOrderImage2.Image = Nothing
                    End Try

                    Try
                        Me.FPOrderImage3.Image = HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage3)
                    Catch ex As Exception
                        Me.FPOrderImage3.Image = Nothing
                    End Try

                    Try
                        Me.FPOrderImage4.Image = HI.UL.ULImage.ConvertByteArrayToImmage(Rx!FPStyleImage4)
                    Catch ex As Exception
                        Me.FPOrderImage4.Image = Nothing
                    End Try

                Next
                dt.Dispose()
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ogcemp_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogdBreakdown.EmbeddedNavigator.ButtonClick ', ogcacc.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                With Me.ogvBreakdown
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogdBreakdown.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNDataSeq>0", "FNDataSeq")
                        R!FNDataSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridData()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridData()

            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub InitGridData()

        Try
            If Not (Me.ogdBreakdown.DataSource Is Nothing) Then

                Dim dtemp As DataTable


                With CType(Me.ogdBreakdown.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTSizeBreakDown=''").Length > 0 Or .Select("FTColorway=''").Length > 0 Then
                    Else

                        .Rows.Add("1", "", 0, "", "", "", "", "", "", .Rows.Count + 1, 0, 0, 0, 0, 0, "", "")
                    End If
                End With


            End If
        Catch ex As Exception

        End Try



    End Sub



    Private Sub InitGridDataAcc()

        Try
            If Not (Me.ogcacc.DataSource Is Nothing) Then



                With CType(Me.ogcacc.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTMat=''").Length > 0 Then
                    Else

                        .Rows.Add("0", .Rows.Count + 1, "", "", "", "", "", 0, "", 0, "", 0, "", "", "", "0", 0)
                    End If
                End With


            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub RepositoryFTSizeBreakDown_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFTSizeBreakDown.EditValueChanged
        Try

            With Me.ogvBreakdown
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FNSeq", obj.GetColumnValue("FNSeq").ToString)

            End With

            CType(Me.ogdBreakdown.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvBreakdown_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvBreakdown.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogdBreakdown.DataSource, DataTable)

                    .AcceptChanges()
                    Call InitGridData()

                End With
            Case System.Windows.Forms.Keys.Delete


                With Me.ogvBreakdown
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogdBreakdown.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNDataSeq>0", "FNDataSeq")
                        R!FNDataSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridData()


            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        If FTStateApp.Checked = True Then

            If FTStateReceipt.Checked = False Then

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการยืนยันการรับเอกสาร ใช่หรือ ไม่ ?", 1812020457) Then

                    Dim _Str As String = ""
                    Dim _Dt As DataTable
                    Dim _FieldName As String = ""
                    _Str = "Update   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder]  SET "
                    _Str &= Environment.NewLine & "  FTStateReceipt='1' "
                    _Str &= Environment.NewLine & " , FTStateReceiptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Str &= Environment.NewLine & " , FTStateReceiptDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Str &= Environment.NewLine & " , FTStateReceiptTime=" & HI.UL.ULDate.FormatTimeDB & " "

                    _Str &= Environment.NewLine & " WHERE  FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text.Trim) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SAMPLE) Then

                        FTStateReceipt.Checked = True


                        _Str = ""
                        _Str = "Select TOP 1  FTStateReceipt  ,Case When ISNULL(FTStateReceiptBy,'') ='' THEN '' ELSE FTStateReceiptBy + '  ' + Convert(nvarchar(10), convert(datetime,FTStateReceiptDate)  ,103) +' ' +FTStateReceiptTime END  AS FTStateReceiptBy"
                        _Str &= Environment.NewLine & " FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder] AS A"
                        _Str &= Environment.NewLine & " WHERE  A.FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text.Trim) & "'"

                        _Dt = HI.Conn.SQLConn.GetDataTable(_Str, _DBEnum)

                        For Each R As DataRow In _Dt.Rows
                            For Each Col As DataColumn In _Dt.Columns

                                _FieldName = Col.ColumnName.ToString

                                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(Obj, DevExpress.XtraEditors.TextEdit)

                                                .Text = R.Item(Col).ToString

                                            End With
                                        Case ENM.Control.ControlType.CheckEdit
                                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                                Try
                                                    .Checked = (R.Item(Col).ToString = "1")
                                                Catch ex As Exception
                                                End Try
                                            End With

                                    End Select
                                Next
                            Next
                        Next

                    End If

                End If


            End If

        End If

    End Sub

    Private Sub ogcacc_EmbeddedNavigator_Click(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogcacc.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            Case DevExpress.XtraEditors.NavigatorButtonType.Remove

                'With Me.ogvacc
                '    If .FocusedRowHandle < 0 Then Exit Sub
                '    .DeleteRow(.FocusedRowHandle)

                'End With

                With CType(Me.ogcacc.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    For Each R As DataRow In .Select("FTSelect='1'", "FNMatSeq")
                        R.Delete()
                    Next


                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNMatSeq>0", "FNMatSeq")
                        R!FNMatSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridDataAcc()

            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                Call InitGridDataAcc()

            Case Else

        End Select

        e.Handled = True
    End Sub

    Private Sub ogvacc_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvacc.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogcacc.DataSource, DataTable)

                    .AcceptChanges()
                    Call InitGridDataAcc()

                End With

            Case System.Windows.Forms.Keys.Delete

                With Me.ogvacc
                    If .FocusedRowHandle < 0 Then Exit Sub
                    .DeleteRow(.FocusedRowHandle)

                End With

                With CType(Me.ogcacc.DataSource, DataTable)

                    .AcceptChanges()
                    .BeginInit()

                    Dim Ridx As Integer = 1
                    For Each R As DataRow In .Select("FNMatSeq>0", "FNMatSeq")
                        R!FNMatSeq = Ridx

                        Ridx = Ridx + 1
                    Next

                    .EndInit()
                    .AcceptChanges()

                End With

                InitGridDataAcc()


            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub

    Private Sub FNSMPPriceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNSMPPriceType.SelectedIndexChanged
        Try

            'With Me.ogvBreakdown
            '    .Columns.Item("FNDebitQuantity").OptionsColumn.AllowEdit = (FNSMPOrderType.SelectedIndex <> 0)
            'End With



            Select Case FNSMPPriceType.SelectedIndex
                Case 1

                    With CType(Me.ogdBreakdown.DataSource, DataTable)
                        .AcceptChanges()

                        For Each R As DataRow In .Rows

                            R!FNFreeQuantity = Val(R!FNQuantity)
                            R!FNDebitQuantity = 0
                            R!FNAmt = 0 'CDbl(Format(Val(R!FNQuantity.ToString) * Val(R!FNPrice.ToString), "0.00"))

                        Next

                        .AcceptChanges()
                    End With

                Case Else

                    With CType(Me.ogdBreakdown.DataSource, DataTable)
                        .AcceptChanges()

                        For Each R As DataRow In .Rows

                            R!FNFreeQuantity = 0
                            R!FNDebitQuantity = Val(R!FNQuantity)
                            R!FNAmt = CDbl(Format(Val(R!FNQuantity.ToString) * Val(R!FNPrice.ToString), "0.00"))

                        Next

                        .AcceptChanges()
                    End With

            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFNQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFNQuantity.EditValueChanging
        Try
            If e.NewValue >= 0 Then
                With ogvBreakdown
                    Select Case .FocusedColumn.FieldName
                        Case "FNDebitQuantity"


                            If FNSMPPriceType.SelectedIndex = 1 Then
                                e.Cancel = True

                            Else

                                If e.NewValue > Val(.GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString) Then
                                    e.Cancel = True

                                Else

                                    Dim _Amt As Double = 0
                                    Dim mQty As Integer = Val(.GetRowCellValue(.FocusedRowHandle, "FNQuantity").ToString)
                                    Dim _Qty As Integer = 0
                                    Dim _Price As Double = 0

                                    _Qty = e.NewValue


                                    _Price = Double.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNPrice").ToString))
                                    _Amt = CDbl(Format(e.NewValue * _Price, "0.00"))
                                    .SetRowCellValue(.FocusedRowHandle, "FNAmt", _Amt)
                                    .SetRowCellValue(.FocusedRowHandle, "FNFreeQuantity", mQty - _Qty)

                                    CType(Me.ogdBreakdown.DataSource, DataTable).AcceptChanges()
                                End If


                            End If


                        Case "FNQuantity"


                            Dim _Amt As Double = 0
                            Dim _Qty As Integer = e.NewValue
                            Dim _Price As Double = 0

                            _Price = Double.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNPrice").ToString))
                            _Amt = CDbl(Format(e.NewValue * _Price, "0.00"))

                            If FNSMPPriceType.SelectedIndex = 1 Then
                                .SetRowCellValue(.FocusedRowHandle, "FNDebitQuantity", 0)
                                .SetRowCellValue(.FocusedRowHandle, "FNFreeQuantity", _Qty)
                                .SetRowCellValue(.FocusedRowHandle, "FNAmt", 0)
                            Else
                                .SetRowCellValue(.FocusedRowHandle, "FNDebitQuantity", _Qty)
                                .SetRowCellValue(.FocusedRowHandle, "FNFreeQuantity", 0)
                                .SetRowCellValue(.FocusedRowHandle, "FNAmt", _Amt)
                            End If


                            CType(Me.ogdBreakdown.DataSource, DataTable).AcceptChanges()

                        Case Else

                    End Select

                End With
            Else
                e.Cancel = True
            End If



        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub RePrice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RePrice.EditValueChanging
        Try

            If e.NewValue >= 0 Then

                With Me.ogvBreakdown
                    If (.FocusedRowHandle < 0 Or .RowCount <= 0) Then Exit Sub
                    Dim _Amt As Decimal = 0
                    Dim _Qty As Integer = 0
                    Dim _Price As Decimal = 0
                    _Qty = Val(.GetRowCellValue(.FocusedRowHandle, "FNDebitQuantity").ToString)

                    _Price = e.NewValue

                    _Amt = CDbl(Format(_Qty * _Price, "0.00"))


                    If FNSMPPriceType.SelectedIndex = 1 Then
                        .SetRowCellValue(.FocusedRowHandle, "FNAmt", 0)

                    Else
                        .SetRowCellValue(.FocusedRowHandle, "FNAmt", _Amt)

                    End If




                    '.SetRowCellValue(.FocusedRowHandle, "FNFreeQuantity", _Qty - e.NewValue)

                End With

                CType(Me.ogdBreakdown.DataSource, DataTable).AcceptChanges()
            Else
                e.Cancel = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        Try
            If Me.FTSMPOrderNo.Properties.Tag.ToString().Trim() <> "" Then


                With _CopyOrder
                    .newDoc = ""
                    .FNHSysStyleId.Text = Me.FNHSysStyleId.Text
                    .FTOrderNo.Text = FTSMPOrderNo.Text.Trim
                    .FNSMPOrderType.SelectedIndex = 0
                    .FNSMPPrototypeNo.Value = 0
                    .FNOrderSampleType.SelectedIndex = 0

                    .ShowDialog()
                    If .newDoc <> "" Then
                        Me.FTSMPOrderNo.Text = .newDoc

                        LoadDataInfo(Me.FTSMPOrderNo.Text)

                        Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                    End If
                End With



            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTSMPOrderNo_lbl.Text)
                Me.FTSMPOrderNo.Focus()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadPartMaster()

        Dim dt As DataTable
        Dim _Qry As String = ""
        Dim _RowIndex As Integer = 0
        _Qry = "Select '0' AS FTSelect ,FNHSysPartId,FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & ",FTPartNameEN AS FTPartName"
        End If

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE ISNULL(FTStateActive,'')='1'  "
        _Qry &= vbCrLf & " ORDER BY FTPartCode "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
        Me.ogcpart.DataSource = dt.Copy

        dt.Dispose()
    End Sub


    Private Sub RepFTPositionPartName_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepPart.QueryCloseUp
        Try
            With Me.ogvacc
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                Dim _PartName As String = ""
                Dim _PartIDKey As String = ""

                With CType(Me.ogcpart.DataSource, DataTable)

                    .AcceptChanges()

                    For Each R As DataRow In .Select("FTSelect='1'")

                        If _PartName = "" Then

                            _PartName = R!FTPartName.ToString
                            _PartIDKey = R!FNHSysPartId.ToString

                        Else

                            _PartName = _PartName & "," & R!FTPartName.ToString
                            _PartIDKey = _PartIDKey & "," & R!FNHSysPartId.ToString

                        End If

                    Next

                End With

                .SetFocusedRowCellValue("FTPart", _PartName)
                .SetFocusedRowCellValue("FNAllPart", _PartIDKey)



            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepFTPositionPartName_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepPart.QueryPopUp
        Try

            Call LoadPartMaster()

            Dim _PartKey As String = ""
            Dim _PartIDKey As String = ""

            With Me.ogvacc
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If
                _PartKey = "" & .GetFocusedRowCellValue("FTPart").ToString
                _PartIDKey = "" & .GetFocusedRowCellValue("FNAllPart").ToString
            End With

            ogvpart.ClearColumnsFilter()
            ogvpart.ActiveFilter.Clear()


            ogvpart.Columns.ColumnByFieldName("FTSelect").Width = 40
            ogvpart.Columns.ColumnByFieldName("FTPartName").Width = 150


            With CType(Me.ogcpart.DataSource, DataTable)
                For Each Str As String In _PartIDKey.Split(",")
                    For Each R As DataRow In .Select("FNHSysPartId=" & Integer.Parse(Val(Str)) & "")
                        R!FTSelect = "1"
                        Exit For
                    Next
                Next
                .AcceptChanges()
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click

        If CheckOwner() = False Then Exit Sub

        If Me.VerrifyData Then


            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการยกเลิกเอกสาร ใช่หรือ ไม่ ?", 1112777457) Then

                Dim _Str As String = ""
                Dim _Dt As DataTable
                Dim _FieldName As String = ""

                _Str = "Update   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.[TSMPOrder]  SET "
                _Str &= Environment.NewLine & "  FNSMPOrderStatus=2 "
                '_Str &= Environment.NewLine & " , FTStateReceiptBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                '_Str &= Environment.NewLine & " , FTStateReceiptDate=" & HI.UL.ULDate.FormatDateDB & " "
                '_Str &= Environment.NewLine & " , FTStateReceiptTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= Environment.NewLine & " WHERE  FTSMPOrderNo = '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text.Trim) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SAMPLE) Then

                    Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                    FNSMPOrderStatus.SelectedIndex = 2

                End If

            End If

        End If

    End Sub

    Private Sub ocmmatARemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatARemovePart.Click
        With ogvpartmata

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub ocmmatBRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatBRemovePart.Click
        With ogvpartmatb

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub


    Private Sub ocmmatCRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatCRemovePart.Click
        With ogvpartmatc

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub ocmmatDRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatDRemovePart.Click
        With ogvpartmatd

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub ocmmatERemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatERemovePart.Click
        With ogvpartmate

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub ocmmatFRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatFRemovePart.Click
        With ogvpartmatf

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub ocmmatgRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatGRemovePart.Click
        With ogvpartmatg

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub


    Private Sub ocmmatHRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatHRemovePart.Click
        With ogvpartmath

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub ocmmatIRemovePart_Click(sender As Object, e As EventArgs) Handles ocmmatIRemovePart.Click
        With ogvpartmati

            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            .BeginInit()

            For Each i As Integer In .GetSelectedRows()
                .DeleteRow(i)

            Next
            .EndInit()
        End With
    End Sub

    Private Sub AddPart_Click(sender As Object, e As EventArgs) Handles ocmmatAAddPart.Click, ocmmatBAddPart.Click, ocmmatCAddPart.Click, ocmmatDAddPart.Click, ocmmatEAddPart.Click, ocmmatFAddPart.Click, ocmmatGAddPart.Click, ocmmatHAddPart.Click, ocmmatIAddPart.Click

        Dim dt As DataTable
        Dim _Qry As String = ""
        Dim _RowIndex As Integer = 0
        Dim StateAdd As Boolean = False


        _Qry = "Select '0' AS FTSelect ,FNHSysPartId,FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",FTPartNameTH AS FTPartName"
        Else
            _Qry &= vbCrLf & ",FTPartNameEN AS FTPartName"
        End If

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS A With(NOLOCK) "
        _Qry &= vbCrLf & " WHERE ISNULL(FTStateActive,'')='1'  "
        _Qry &= vbCrLf & " ORDER BY FTPartCode "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        With _AddPart
            .AddMat = False
            .ogcpart.DataSource = dt.Copy
            .ogvpart.ClearColumnsFilter()

            .ShowDialog()

            If .AddMat Then
                With CType(.ogcpart.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy

                End With

                If dt.Select("FTSelect='1'").Length > 0 Then
                    StateAdd = True
                End If

            End If


        End With

        If StateAdd Then
            Dim PartId As Integer = 0
            Dim PartCode As String = ""
            Dim PartName As String = ""

            For Each R As DataRow In dt.Select("FTSelect='1'")
                PartId = Val(R!FNHSysPartId.ToString)
                PartCode = R!FTPartCode.ToString
                PartName = R!FTPartName.ToString


                Select Case sender.name.ToString().ToLower()
                    Case "ocmmatAAddPart".ToLower()

                        With CType(ogcpartmata.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatBAddPart".ToLower()

                        With CType(ogcpartmatb.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatCAddPart".ToLower()

                        With CType(ogcpartmatc.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatDAddPart".ToLower()

                        With CType(ogcpartmatd.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatEAddPart".ToLower()

                        With CType(ogcpartmate.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With


                    Case "ocmmatFAddPart".ToLower()

                        With CType(ogcpartmatf.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatGAddPart".ToLower()

                        With CType(ogcpartmatg.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatHAddPart".ToLower()

                        With CType(ogcpartmath.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                    Case "ocmmatIAddPart".ToLower()

                        With CType(ogcpartmati.DataSource, DataTable)

                            If .Select("FNHSysPartId=" & PartId & "").Length <= 0 Then

                                .Rows.Add(PartId, PartCode, PartName)
                            End If

                            .AcceptChanges()

                        End With

                End Select



            Next



        End If

        dt.Dispose()

    End Sub

    Private Sub LoadPartMat(orderno As String)
        Dim cmd As String = ""
        Dim dtmain As New DataTable
        Dim dt As New DataTable

        cmd = "Select P.FNHSysPartId,A.FTPartCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            cmd &= vbCrLf & ",A.FTPartNameTH AS FTPartName"
        Else
            cmd &= vbCrLf & ",A.FTPartNameEN AS FTPartName"
        End If
        cmd &= vbCrLf & " ,P.FNMat"
        cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatPart As P With(NOLOCK) "
        cmd &= vbCrLf & "       INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart As A With(NOLOCK) On P.FNHSysPartId= A.FNHSysPartId"
        cmd &= vbCrLf & " WHERE P.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(orderno) & "' AND P.FNMatSeq =0"
        cmd &= vbCrLf & " ORDER BY A.FTPartCode "
        dtmain = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)


        For MatIndex As Integer = 0 To 8

            If dtmain.Select("FNMat='" & MatIndex & "'").Length > 0 Then
                dt = dtmain.Select("FNMat='" & MatIndex & "'").CopyToDataTable
            Else
                dt = dtmain.Clone()

            End If


            Select Case MatIndex
                Case 0
                    ogcpartmata.DataSource = dt.Copy()

                Case 1
                    ogcpartmatb.DataSource = dt.Copy()

                Case 2
                    ogcpartmatc.DataSource = dt.Copy()

                Case 3
                    ogcpartmatd.DataSource = dt.Copy()

                Case 4
                    ogcpartmate.DataSource = dt.Copy()

                Case 5
                    ogcpartmatf.DataSource = dt.Copy()

                Case 6
                    ogcpartmatg.DataSource = dt.Copy()

                Case 7
                    ogcpartmath.DataSource = dt.Copy()

                Case 8
                    ogcpartmati.DataSource = dt.Copy()

            End Select

        Next

        dtmain.Dispose()
        dt.Dispose()

    End Sub

    Private Sub SetDataInfo(OrderNoref As String)
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim _Qry As String = ""

        dt.Columns.Add("FNHSysPartId", GetType(Integer))
        dt.Columns.Add("FTPartCode", GetType(String))
        dt.Columns.Add("FTPartName", GetType(String))
        dt.Columns.Add("FTStateEmb", GetType(String))
        dt.Columns.Add("FTStatePrint", GetType(String))
        dt.Columns.Add("FTStateHeat", GetType(String))
        dt.Columns.Add("FTStateLaser", GetType(String))
        dt.Columns.Add("FTStateWindows", GetType(String))
        dt.Columns.Add("FTEmbNote", GetType(String))
        dt.Columns.Add("FTPrintNote", GetType(String))
        dt.Columns.Add("FTHeatNote", GetType(String))
        dt.Columns.Add("FTLaserNote", GetType(String))
        dt.Columns.Add("FTWindowsNote", GetType(String))

        _Qry = "   Select  FNHSysPartId"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatPart As A With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNoref) & "' "



        _Qry &= vbCrLf & " And ISNULL(FNHSysPartId,0) <> 0"
        _Qry &= vbCrLf & "   GROUP BY  FNHSysPartId"
        dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

        For Each R As DataRow In dt2.Rows

            Dim Str As String = R!FNHSysPartId.ToString

            If dt.Select("FNHSysPartId=" & Integer.Parse(Val(Str)) & "").Length <= 0 Then

                _Qry = "   SELECT    TOP 1    A.FNHSysPartId, A.FTPartCode"

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Qry &= vbCrLf & " , A.FTPartNameTH AS FTPartName"
                Else
                    _Qry &= vbCrLf & " , A.FTPartNameEN As FTPartName "
                End If

                _Qry &= vbCrLf & " , ISNULL(B.FTStateEmb,'0') AS FTStateEmb"
                _Qry &= vbCrLf & " , ISNULL(B.FTStatePrint,'0') AS FTStatePrint"
                _Qry &= vbCrLf & " , ISNULL(B.FTStateHeat,'0') AS FTStateHeat"
                _Qry &= vbCrLf & " , ISNULL(B.FTStateLaser,'0') AS FTStateLaser"
                _Qry &= vbCrLf & " , ISNULL(B.FTStateWindows,'0') AS FTStateWindows"
                _Qry &= vbCrLf & " , ISNULL(B.FTEmbNote,'') AS FTEmbNote"
                _Qry &= vbCrLf & " , ISNULL(B.FTPrintNote,'') AS FTPrintNote"
                _Qry &= vbCrLf & " , ISNULL(B.FTHeatNote,'') AS FTHeatNote"
                _Qry &= vbCrLf & " , ISNULL(B.FTLaserNote,'') AS FTLaserNote"
                _Qry &= vbCrLf & " , ISNULL(B.FTWindowsNote,'') AS FTWindowsNote"

                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS A LEFT OUTER JOIN"
                _Qry &= vbCrLf & "     (SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_SetPart WITH(NOLOCK) "
                _Qry &= vbCrLf & "      WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(OrderNoref) & "' "
                _Qry &= vbCrLf & " ) AS B ON A.FNHSysPartId = B.FNHSysPartId"
                _Qry &= vbCrLf & "   WHERE A.FNHSysPartId=" & Integer.Parse(Val(Str)) & " "

                ' If StateSeason Then



                ' End If

                dt3 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

                For Each R2 As DataRow In dt3.Rows

                    dt.Rows.Add(Integer.Parse(Val(Str)), R2!FTPartCode.ToString, R2!FTPartName.ToString, R2!FTStateEmb.ToString, R2!FTStatePrint.ToString, R2!FTStateHeat.ToString, R2!FTStateLaser.ToString, R2!FTStateWindows.ToString, R2!FTEmbNote.ToString, R2!FTPrintNote.ToString, R2!FTHeatNote.ToString, R2!FTLaserNote.ToString, R2!FTWindowsNote.ToString)

                    Exit For
                Next

            End If

        Next

        Me.ogc.DataSource = dt.Copy
        dt.Dispose()
        dt2.Dispose()
        dt3.Dispose()

    End Sub

    Private Sub otbdetail_Click(sender As Object, e As EventArgs) Handles otbdetail.Click

    End Sub

    Private Sub otbdetail_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles otbdetail.SelectedPageChanged

        'Select Case otbdetail.SelectedTabPage.Name
        '    Case otpsetpart.Name
        '        Call SetDataInfo(FTSMPOrderNo.Text)
        'End Select

    End Sub

    Private Sub FTStatePatternOthers_CheckedChanged(sender As Object, e As EventArgs) Handles FTStatePatternOthers.CheckedChanged
        FTPatternOthersNote.Visible = (FTStatePatternOthers.Checked)


        If FTStatePatternOthers.Checked = False Then
            FTPatternOthersNote.Text = ""
        End If
    End Sub

    Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmAddFile.Click
        Try
            If CheckOwner() = False Then Exit Sub
            Dim cmdstring As String = ""
            Dim AddFileName As String = ""
            Dim AddFileType As Integer = 0
            Dim FileSeq As Integer = 0

            cmdstring = "select top 1 FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS x with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
            Dim orderno As String = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "")


            If orderno <> "" Then

                With _AddFile
                    .AddFileState = False
                    .ocmReadDocumentfile.Visible = True
                    .ocmok.Visible = True
                    .FTFileName.Properties.ReadOnly = False
                    .FNFileType.Properties.ReadOnly = False
                    .FTFileName.Text = ""
                    .FNFileType.SelectedIndex = 0
                    .oGrpdetail.Controls.Clear()
                    .WindowState = FormWindowState.Maximized
                    .ShowDialog()

                    If .AddFileState Then

                        Dim datadate As String = ""
                        Dim datatime As String = ""
                        Dim dFTFileExten As String = .FileExt.ToString
                        Dim _FilePath As String = .DataFilePath
                        Dim dttime As DataTable

                        cmdstring = " select top 1 " & HI.UL.ULDate.FormatDateDB & " AS FTDate," & HI.UL.ULDate.FormatTimeDB & " AS FTTime"
                        dttime = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SYSTEM)

                        For Each r As DataRow In dttime.Rows
                            datadate = r!FTDate.ToString
                            datatime = r!FTTime.ToString
                        Next
                        dttime.Dispose()

                        cmdstring = "select MAX( FNFileSeq) AS FNFileSeq "
                        cmdstring &= " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_File AS x with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"

                        FileSeq = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "0")) + 1

                        AddFileType = .FNFileType.SelectedIndex
                        AddFileName = .FTFileName.Text.Trim()
                        Dim data() As Byte

                        Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                        data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))

                        'Select Case dFTFileExten
                        '    Case "Text", "DOC", "DOCX"
                        '        data = System.IO.File.ReadAllBytes(_FilePath)
                        '    Case Else
                        '        Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                        '        data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
                        'End Select


                        'Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                        'data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
                        'data = System.IO.File.ReadAllBytes(_FilePath)


                        cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_File"
                        cmdstring &= " (FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FNFileSeq, FTFileName, FTFileType,FTFileExten, FBFile)"
                        cmdstring &= " VALUES (@FTInsUser, @FDInsDate, @FTInsTime, @FTSMPOrderNo, @FNFileSeq, @FTFileName, @FTFileType,@FTFileExten, @FBFile)"

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_SAMPLE)
                        HI.Conn.SQLConn.SqlConnectionOpen()

                        Dim cmd As New Data.SqlClient.SqlCommand(cmdstring, HI.Conn.SQLConn.Cnn)
                        cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                        cmd.Parameters.AddWithValue("@FDInsDate", datadate)
                        cmd.Parameters.AddWithValue("@FTInsTime", datatime)
                        cmd.Parameters.AddWithValue("@FTSMPOrderNo", HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text.Trim()))
                        cmd.Parameters.AddWithValue("@FNFileSeq", FileSeq)
                        cmd.Parameters.AddWithValue("@FTFileName", HI.UL.ULF.rpQuoted(AddFileName))
                        cmd.Parameters.AddWithValue("@FTFileType", AddFileType)
                        cmd.Parameters.AddWithValue("@FTFileExten", dFTFileExten)

                        Dim p1 As New Data.SqlClient.SqlParameter("@FBFile", SqlDbType.Image)
                        p1.Value = data
                        cmd.Parameters.Add(p1)

                        cmd.ExecuteNonQuery()
                        cmd.Parameters.Clear()
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                        LoadFileRef(FTSMPOrderNo.Text.Trim())
                    End If

                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmRemoveFile_Click(sender As Object, e As EventArgs) Handles ocmRemoveFile.Click

        Try

            If CheckOwner() = False Then Exit Sub
            Dim cmdstring As String = ""
            Dim AddFileName As String = ""
            Dim AddFileType As Integer = 0
            Dim FileSeq As Integer = 0
            cmdstring = "select top 1 FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS x with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
            Dim orderno As String = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "")


            If orderno <> "" Then
                With Me.ogvfileref
                    FileSeq = Val(.GetFocusedRowCellValue("FNFileSeq").ToString())
                    AddFileName = .GetFocusedRowCellValue("FTFileName").ToString()
                End With


                If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการทำการลบ File ใช่หรือมไม่ ?", 1907025478, AddFileName) Then



                    cmdstring = " Delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_File where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' AND FNFileSeq =" & FileSeq & ""
                    cmdstring &= " Update A SET FNFileSeq = FNFileSeq -1  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_File AS A  where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' AND FNFileSeq >" & FileSeq & ""

                    If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE) Then
                        LoadFileRef(FTSMPOrderNo.Text.Trim())
                    End If
                End If

            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub ogvfileref_DoubleClick(sender As Object, e As EventArgs) Handles ogvfileref.DoubleClick
        Try

            Dim cmdstring As String = ""
            Dim AddFileName As String = ""
            Dim AddFileType As Integer = 0
            Dim FileSeq As Integer = 0
            Dim FileExt As String = ""

            cmdstring = "select top 1 FTSMPOrderNo from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS x with(nolock) where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
            Dim orderno As String = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "")


            If orderno <> "" Then

                With Me.ogvfileref

                    FileSeq = Val(.GetFocusedRowCellValue("FNFileSeq").ToString())
                    AddFileName = .GetFocusedRowCellValue("FTFileName").ToString()

                End With

                Dim dt As DataTable
                Dim dttabyte() As Byte
                cmdstring = " select top 1 *  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_File where FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "' AND FNFileSeq =" & FileSeq & ""
                dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                For Each R As DataRow In dt.Rows
                    AddFileType = Val(R!FTFileType.ToString())
                    FileExt = R!FTFileExten.ToString
                    dttabyte = CType(R!FBFile, Byte())
                Next
                dt.Dispose()

                With _AddFile

                    .AddFileState = False
                    .ocmReadDocumentfile.Visible = False
                    .ocmok.Visible = False
                    .FTFileName.Properties.ReadOnly = True
                    .FNFileType.Properties.ReadOnly = True
                    .FNFileType.SelectedIndex = AddFileType
                    .oGrpdetail.Controls.Clear()



                    .ShowFile(AddFileType, AddFileName, dttabyte, FileExt)


                    .WindowState = FormWindowState.Maximized
                    .ShowDialog()

                End With

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FNHSysRawmatIdA_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysRawmatIdA.EditValueChanged, FNHSysRawmatIdB.EditValueChanged, FNHSysRawmatIdC.EditValueChanged _
                                                                                             , FNHSysRawmatIdD.EditValueChanged, FNHSysRawmatIdE.EditValueChanged _
                                                                                             , FNHSysRawmatIdF.EditValueChanged, FNHSysRawmatIdG.EditValueChanged, FNHSysRawmatIdH.EditValueChanged, FNHSysRawmatIdI.EditValueChanged
        Try


            If sender.Text <> "" Then
                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Select Case sender.name.ToString().ToLower()
                    Case "FNHSysRawmatIdA".ToLower()
                        FTMatA.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameA.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameA.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorA.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeA.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdA.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdB".ToLower()
                        FTMatB.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameB.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameB.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorB.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeB.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdB.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdC".ToLower()
                        FTMatC.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameC.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameC.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorC.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeC.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdC.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdD".ToLower()
                        FTMatD.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameD.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameD.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorD.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeD.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdD.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdE".ToLower()
                        FTMatE.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameE.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameE.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorE.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeE.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdE.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdF".ToLower()
                        FTMatF.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameF.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameF.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorF.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeF.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdF.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdG".ToLower()
                        FTMatG.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameG.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameG.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorG.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeG.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdG.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdH".ToLower()
                        FTMatH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdH.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                    Case "FNHSysRawmatIdI".ToLower
                        FTMatI.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString()
                        FTMatNameI.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString()
                        FTMatColorNameI.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString()
                        FTMatColorI.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString()
                        FTMatSizeI.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString()
                        FNMatUnitIdI.Text = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString()
                End Select


            Else
                sender.EditValue = Nothing


                Select Case sender.name.ToString().ToLower()
                    Case "FNHSysRawmatIdA".ToLower()
                        FTMatA.Text = ""
                        FTMatNameA.Text = ""
                        FTMatColorNameA.Text = ""
                        FTMatColorA.Text = ""
                        FTMatSizeA.Text = ""
                        FNMatUnitIdA.Text = ""
                    Case "FNHSysRawmatIdB".ToLower()
                        FTMatB.Text = ""
                        FTMatNameB.Text = ""
                        FTMatColorNameB.Text = ""
                        FTMatColorB.Text = ""
                        FTMatSizeB.Text = ""
                        FNMatUnitIdB.Text = ""
                    Case "FNHSysRawmatIdC".ToLower()
                        FTMatC.Text = ""
                        FTMatNameC.Text = ""
                        FTMatColorNameC.Text = ""
                        FTMatColorC.Text = ""
                        FTMatSizeC.Text = ""
                        FNMatUnitIdC.Text = ""
                    Case "FNHSysRawmatIdD".ToLower()
                        FTMatD.Text = ""
                        FTMatNameD.Text = ""
                        FTMatColorNameD.Text = ""
                        FTMatColorD.Text = ""
                        FTMatSizeD.Text = ""
                        FNMatUnitIdD.Text = ""
                    Case "FNHSysRawmatIdE".ToLower()
                        FTMatE.Text = ""
                        FTMatNameE.Text = ""
                        FTMatColorNameE.Text = ""
                        FTMatColorE.Text = ""
                        FTMatSizeE.Text = ""
                        FNMatUnitIdE.Text = ""
                    Case "FNHSysRawmatIdF".ToLower()
                        FTMatF.Text = ""
                        FTMatNameF.Text = ""
                        FTMatColorNameF.Text = ""
                        FTMatColorF.Text = ""
                        FTMatSizeF.Text = ""
                        FNMatUnitIdF.Text = ""
                    Case "FNHSysRawmatIdG".ToLower()
                        FTMatG.Text = ""
                        FTMatNameG.Text = ""
                        FTMatColorNameG.Text = ""
                        FTMatColorG.Text = ""
                        FTMatSizeG.Text = ""
                        FNMatUnitIdG.Text = ""
                    Case "FNHSysRawmatIdH".ToLower()
                        FTMatH.Text = ""
                        FTMatNameH.Text = ""
                        FTMatColorNameH.Text = ""
                        FTMatColorH.Text = ""
                        FTMatSizeH.Text = ""
                        FNMatUnitIdH.Text = ""
                    Case "FNHSysRawmatIdI".ToLower
                        FTMatI.Text = ""
                        FTMatNameI.Text = ""
                        FTMatColorNameI.Text = ""
                        FTMatColorI.Text = ""
                        FTMatSizeI.Text = ""
                        FNMatUnitIdI.Text = ""
                End Select


            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysRawmatIdA_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles FNHSysRawmatIdA.ButtonClick

    End Sub

    Private Sub RepositoryFNHSysRawmatId_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFNHSysRawmatId.EditValueChanged
        Try
            With Me.ogvacc

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                Dim RawMatid As Integer = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRawMatId_Hide").ToString())

                .SetFocusedRowCellValue("FNHSysRawmatId_Hide", RawMatid)
                .SetFocusedRowCellValue("FTMat", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatCode").ToString())
                .SetFocusedRowCellValue("FTMatName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDescription").ToString())
                .SetFocusedRowCellValue("FTMatColor", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString())
                .SetFocusedRowCellValue("FTMatColorName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorName").ToString())
                .SetFocusedRowCellValue("FTMatSize", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString())
                .SetFocusedRowCellValue("FNHSysUnitId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString())
                .SetFocusedRowCellValue("FNHSysUnitId_Hide", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysUnitId").ToString())



            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadSizeBreakdown()
        Call LoadRowMatDataFabric(True)
    End Sub
End Class