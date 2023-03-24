Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports HI.Auditor
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports excel = Microsoft.Office.Interop.Excel
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Spreadsheet

Public Class wCostSheet

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private _oDtPacking As DataTable
    Private RetoyCal As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Private ActState As String = "1"
    '  Private _Revise As Boolean = False
    Private _FileName As String = ""
    Private _FilePath As String

    Dim dtStyleDetail As DataTable

    Private _CopyCostSheet As wCopyCostSheet


    Private Enum TabIndexs As Integer
        FabricDetail = 0
        TrimsDetail = 1
        NoSewDetail = 2
        PackagingDetail = 3
    End Enum

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _CopyCostSheet = New wCopyCostSheet("", "", "", 0)

        HI.TL.HandlerControl.AddHandlerObj(_CopyCostSheet)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyCostSheet.Name.ToString.Trim, _CopyCostSheet)
        Catch ex As Exception
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_CopyCostSheet)

        Call PrepareForm()

        Call InitGrid()
        Call SetControl()


        RepositoryItemGridLookUpEditItemMulti.View.Columns.Clear()
        RepositoryItemGridLookUpEditItemMulti.View.BeginInit()
        RepositoryItemGridLookUpEditFTMainMatCode.View.OptionsView.ColumnAutoWidth = False
        RepositoryItemGridLookUpEditFTMainMatCode.PopupFormSize = RepositoryItemGridLookUpEditFTMainMatCode.PopupFormSize

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In RepositoryItemGridLookUpEditFTMainMatCode.View.Columns
            Dim Gcol As New DevExpress.XtraGrid.Columns.GridColumn
            With Gcol
                .Name = "GMulti" & GridCol.Name
                .Caption = GridCol.Caption
                .FieldName = GridCol.FieldName
                .Width = GridCol.Width
                .VisibleIndex = GridCol.VisibleIndex
                .Visible = GridCol.Visible
                .OptionsColumn.AllowEdit = GridCol.OptionsColumn.AllowEdit
                .OptionsColumn.AllowMerge = GridCol.OptionsColumn.AllowMerge
                .OptionsColumn.ReadOnly = GridCol.OptionsColumn.ReadOnly
                .OptionsColumn.AllowMove = GridCol.OptionsColumn.AllowMove
                .OptionsColumn.AllowSort = GridCol.OptionsColumn.AllowSort
                .OptionsFilter.AutoFilterCondition = GridCol.OptionsFilter.AutoFilterCondition
            End With

            RepositoryItemGridLookUpEditItemMulti.View.Columns.Add(Gcol)

        Next
        RepositoryItemGridLookUpEditItemMulti.View.EndInit()
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

#Region "Initial Grid"
    Private Sub InitGrid()

        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNExten|FNNetExten|FNChinaOrderCost|FNMalaysiaOrderCost|FNThailandOrderCost|FNJapanOrderCost|FNImportDuty"
        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = ""
        'Dim sFieldCustomSum As String = ""
        'Dim sFieldCustomGrpSum As String = ""
        'Dim _gv As Object

        'For _gvc As Integer = 1 To 4
        '    If (_gvc = 1) Then
        '        _gv = ogvfabric
        '    ElseIf (_gvc = 2) Then
        '        _gv = ogvtrims
        '    ElseIf (_gvc = 3) Then
        '        _gv = ogvnosew
        '    Else
        '        _gv = ogvpack
        '    End If

        '    With _gv

        '        .ClearGrouping()
        '        .ClearDocument()

        '        For Each Str As String In sFieldCount.Split("|")
        '            If Str <> "" Then
        '                .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '                .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '            End If
        '        Next

        '        For Each Str As String In sFieldCustomSum.Split("|")
        '            If Str <> "" Then
        '                .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '                .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '            End If
        '        Next

        '        For Each Str As String In sFieldSum.Split("|")
        '            If Str <> "" Then
        '                .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '                .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
        '            End If
        '        Next

        '        For Each Str As String In sFieldGrpCount.Split("|")
        '            If Str <> "" Then
        '                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '            End If
        '        Next

        '        For Each Str As String In sFieldCustomGrpSum.Split("|")
        '            If Str <> "" Then
        '                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '            End If
        '        Next

        '        For Each Str As String In sFieldGrpSum.Split("|")
        '            If Str <> "" Then
        '                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '            End If
        '        Next

        '        .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '        .OptionsView.ShowFooter = True


        '    End With
        'Next

        ''------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Procedure"

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

    Public Sub LoadDataInfo(Key As Object) 'Load data from database and fill data in form

        _FormLoad = True
        _ProcLoad = True

        Dim _Dt As DataTable
        Dim _Str As String

        _Str = Me.Query & "  WHERE  " & Me.MainKey & "='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' " ' AND FNVersion ='" & FNVersion.Value & "'"
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

                                .Text = R.Item(Col).ToString
                                'Try
                                '    .SelectedIndex = Val(R.Item(Col).ToString)
                                'Catch ex As Exception
                                '    .SelectedIndex = -1
                                'End Try
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


        _Str = " SELECT TOP 1  FNNoSawAppCostAmt, FNGarmentTreatmentAmt, FNNormalSizeAmt, FNAboveSpecialSizeAmt, FNLessThanSpecialSizeChargePerAmt, FNLessThanSpecialSizeAmt, 
                      FTRemark, FTStateActive, FNHSysCmpId, FTSamFabric, FTSamTrims, FTSamPack, FTSamNoSew, FTSamGarment, FTSamOtherCost, FTSamCMP, FNL4Country1Exc, FNL4Country1Final, 
                      FNL4Country1Extended, FNL4Country2Exc, FNL4Country2Final, FNL4Country2Extended, FNL4Country3, FNL4Country3Cur, FNL4Country3Exc, FNL4Country3Final, FNL4Country3Extended, 
                      FNTotalFabAmt, FNTotalAccAmt, FNChargeFabAmt, FNChargeAccAmt, FNProcessMatCost, FNProcessLaborCost, FNPackagingAmt, FNOtherCostAmt, FNCMP, FNGrandTotal, FNExtendedPer, 
                      FNExtendedFOB, FNTrinUsageAllowPer, FNL4LTotalFabric, FNL4LTotalTrim, FNL4LChargeFabric, FNL4LChargeTrim, FNL4LProMatCost, FNL4LProLaborCost, FNL4LPackaging, FNL4LOtherCost, 
                      FNL4LCMP, FNL4LFinalFOB, FNL4LExtendedFOB    "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' AND FTCostSheetNo <>'' "

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

                                .Text = R.Item(Col).ToString
                                'Try
                                '    .SelectedIndex = Val(R.Item(Col).ToString)
                                'Catch ex As Exception
                                '    .SelectedIndex = -1
                                'End Try
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

        Call LoadDocumentDetail(Key.ToString)

        Call LoadDataInfo2(Key.ToString)
        Call SumAmt()

        _ProcLoad = False
        _FormLoad = False
    End Sub

    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTCostSheetBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1405280911, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function



    Private Function VerifyData() As Boolean

        Dim State As Boolean = False
        If FTCostSheetNo.Text.Trim <> "" Then
            If FNHSysSeasonId.Text.Trim <> "" Then

                If FNHSysVenderPramId.Text.Trim <> "" Then

                    If FNHSysStyleId.Text.Trim <> "" Then

                        If FNISTeamMulti.Text.Trim <> "" Then
                            If FNCostSheetColor.Text.Trim <> "" Then
                                If FNCostSheetSize.Text.Trim <> "" Then

                                    If FNCostSheetBuyType.Text.Trim <> "" Then

                                        If FTLOProductDeveloper.Text.Trim <> "" Then

                                            Return True
                                        Else
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTLOProductDeveloper_lbl.Text)
                                            FTLOProductDeveloper.Focus()
                                        End If


                                    Else
                                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNCostSheetBuyType_lbl.Text)
                                        FNCostSheetBuyType.Focus()
                                    End If

                                Else
                                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNCostSheetSize_lbl.Text)
                                    FNCostSheetSize.Focus()
                                End If

                            Else
                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNCostSheetColor_lbl.Text)
                                FNCostSheetColor.Focus()
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNISTeamMulti_lbl.Text)
                            FNISTeamMulti.Focus()
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysStyleId_lbl.Text)
                        FNHSysStyleId.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysVenderPramId_lbl.Text)
                    FNHSysVenderPramId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSeasonId_lbl.Text)
                FNHSysSeasonId.Focus()
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTCostSheetNo_lbl.Text)
            FTCostSheetNo.Focus()
        End If


        Return State


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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        Me.ogcfabric.DataSource = Nothing
        Me.ogctrims.DataSource = Nothing
        Me.ogcnosew.DataSource = Nothing
        Me.ogcpack.DataSource = Nothing

        Call LoadDataInfo2(FTCostSheetNo.Text)

        Me.FNHSysStyleId.Text = ""
        Me.FNHSysSeasonId.Text = ""
        Me.FNHSysCurId.Text = ""
        Me.FTExp.Text = ""
        Me.FTDevelopmentRegion.Text = ""
        Me.FTProductDevelopmentManager.Text = ""
        Me.FTGarmentEngineer.Text = ""
        Me.FTSILH.Text = ""
        Me.FNHSysCurId.Text = ""
        Me.FTLOProductDeveloper.Text = ""
        Me.FTMSC.Text = ""
        Me.FTDESC.Text = ""
        Me.FDBomDate.Text = ""
        Me.FDSpecDate.Text = ""
        Me.FNHSysMatSizeId.Text = ""
        Me.FTMatSizeCode.Text = ""
        Me.FNHSysVenderPramId.Text = ""
        Me.FNHSysCountryId.Text = ""
        Me.FTCostingLO.Text = ""
        Me.FTLessThanSizeBreakDownSpecial.Text = ""
        Me.FNLessThanSpecialSizeChargePerAmt.Text = ""
        Me.FTAboveSizeBreakDownSpecial.Text = ""
        Me.FNAboveSpecialSizeChargePerAmt.Text = ""
        Me.FTRemark.Text = ""
        Me.FTStateActive.CheckState = True
        Me.FNExchangeRate.Value = 1
        FNVersion.Value = 1


        FNExtendedPer.Value = 10.0
        FNTrinUsageAllowPer.Value = 3.0

        LoadMaster()

        _FormLoad = False



    End Sub

    Private Sub LoadDocumentDetail(Key As String) 'load document data form database

        Dim _Str As String = ""
        Dim _Qry As String = ""
        Dim Dt As DataTable

        _Qry = "  Select FNHSysStyleId ,FNHSysSeasonId ,FTExp ,FTGarmentEngineer ,FTSILH ,FTLOProductDeveloper ,FTDevelopmentRegion"
        _Qry &= vbCrLf & " ,FTProductDevelopmentManager ,FTMSC ,FTDESC ,CASE WHEN ISDATE(FDBomDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDBomDate),103) ELSE '' END AS FDBomDate ,CASE WHEN ISDATE(FDSpecDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDSpecDate),103) ELSE '' END AS FDSpecDate ,FNHSysCurId ,FNExchangeRate ,FNHSysVenderPramId ,FNHSysCountryId ,FTCostingLO"
        _Qry &= vbCrLf & " ,FNTotalFabAmt ,FNTotalAccAmt ,FNChargeFabAmt ,FNChargeAccAmt ,FNPackagingAmt ,FNNoSawAppCostAmt ,FNGarmentTreatmentAmt ,FNOtherCostAmt ,FNCMP ,FTStartSize"
        _Qry &= vbCrLf & " ,FTEndSize ,FNNormalSizeAmt ,FTAboveSizeBreakDownSpecial ,FNAboveSpecialSizeChargePerAmt ,FNAboveSpecialSizeAmt ,FTLessThanSizeBreakDownSpecial"
        _Qry &= vbCrLf & " ,FNLessThanSpecialSizeChargePerAmt ,FNLessThanSpecialSizeAmt ,FTRemark ,FTStateActive ,FBDocument"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' " '"
        Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        If Dt.Rows.Count > 0 Then
            For Each R As DataRow In Dt.Rows
                'FTExp.Text = R!FTExp.ToString()
                'FTGarmentEngineer.Text = R!FTGarmentEngineer.ToString()
                'FTSILH.Text = R!FTSILH.ToString()
                'FTLOProductDeveloper.Text = R!FTLOProductDeveloper.ToString()
                'FTDevelopmentRegion.Text = R!FTDevelopmentRegion.ToString()
                'FTProductDevelopmentManager.Text = R!FTProductDevelopmentManager.ToString()
                'FTMSC.Text = R!FTMSC.ToString()
                'FTDESC.Text = R!FTDESC.ToString()
                'FDBomDate.Text = R!FDBomDate.ToString()
                'FDSpecDate.Text = R!FDSpecDate.ToString()
                'FTCostingLO.Text = R!FTCostingLO.ToString()
                'FNGarmentTreatmentAmt.Text = R!FNGarmentTreatmentAmt.ToString()
                'FNOtherCostAmt.Text = R!FNOtherCostAmt.ToString()
                'FNCMP.Text = R!FNCMP.ToString()
                FNHSysMatSizeId.Text = R!FTStartSize.ToString()
                FTMatSizeCode.Text = R!FTEndSize.ToString()
                'FTAboveSizeBreakDownSpecial.Text = R!FTAboveSizeBreakDownSpecial.ToString()
                'FNAboveSpecialSizeChargePerAmt.Text = R!FNAboveSpecialSizeChargePerAmt.ToString()
                'FTLessThanSizeBreakDownSpecial.Text = R!FTLessThanSizeBreakDownSpecial.ToString()
                'FNLessThanSpecialSizeChargePerAmt.Text = R!FNLessThanSpecialSizeChargePerAmt.ToString()
                'FTRemark.Text = R!FTRemark.ToString()
                'FTStateActive.Checked = R!FTStateActive.ToString()
                If Not (IsDBNull(R!FBDocument)) Then
                    With ExcelAttach
                        .ReadOnly = True
                        .Dock = DockStyle.Fill
                        .Unit = DevExpress.Office.DocumentUnit.Inch
                        .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                        .LoadDocument(New MemoryStream(CType(R!FBDocument, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xlsx)
                    End With
                End If
                Exit For
            Next
        End If

        _Qry = "  Select FNHSysStyleId ,FNHSysSeasonId ,FTExp ,FTGarmentEngineer ,FTSILH ,FTLOProductDeveloper ,FTDevelopmentRegion"
        _Qry &= vbCrLf & " ,FTProductDevelopmentManager ,FTMSC ,FTDESC ,CASE WHEN ISDATE(FDBomDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDBomDate),103) ELSE '' END AS FDBomDate ,CASE WHEN ISDATE(FDSpecDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDSpecDate),103) ELSE '' END AS FDSpecDate ,FNHSysCurId ,FNExchangeRate ,FNHSysVenderPramId ,FNHSysCountryId ,FTCostingLO"
        _Qry &= vbCrLf & " ,FNTotalFabAmt ,FNTotalAccAmt ,FNChargeFabAmt ,FNChargeAccAmt ,FNPackagingAmt ,FNNoSawAppCostAmt ,FNGarmentTreatmentAmt ,FNOtherCostAmt ,FNCMP ,FTStartSize"
        _Qry &= vbCrLf & " ,FTEndSize ,FNNormalSizeAmt ,FTAboveSizeBreakDownSpecial ,FNAboveSpecialSizeChargePerAmt ,FNAboveSpecialSizeAmt ,FTLessThanSizeBreakDownSpecial"
        _Qry &= vbCrLf & " ,FNLessThanSpecialSizeChargePerAmt ,FNLessThanSpecialSizeAmt ,FTRemark ,FTStateActive"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' " 'AND FNRevised ='" & FNRevised.Value & "'"
        Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

    End Sub

    Private Function SaveData() As Boolean 'save data form to DB

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
                                                _CmpH = HI.Conn.SQLConn.GetField("Select TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                        Case ENM.Control.ControlType.TextEdit
                                            With CType(ctrl, DevExpress.XtraEditors.TextEdit)
                                                _CmpH = HI.Conn.SQLConn.GetField("Select TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & .Text) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                                            End With

                                            Exit For
                                    End Select
                                Next
                            End If

                            _Key = .Text
                        End With
                End Select
            Next
        Next

        _Str = "Select TOP 1 FTCostSheetNo "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet As A With(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNVersion ='" & FNVersion.Value & "'"

        _StateNew = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_ACCOUNT, "") = "")

        ' If (_Revise = False) Then
        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
        End If
        ' End If

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean

            If (_StateNew) Then

                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet  "
                _Str &= "( FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, FNRevised, FDCostSheetDate, FTCostSheetBy,"
                _Str &= vbCrLf & " FNHSysStyleId, FNHSysSeasonId, FTExp, FTGarmentEngineer, FTSILH, FTLOProductDeveloper, "
                _Str &= vbCrLf & "FTDevelopmentRegion, FTProductDevelopmentManager, FTMSC, FTDESC, FDBomDate, FDSpecDate, "
                _Str &= vbCrLf & "FNHSysCurId, FNExchangeRate, FNHSysVenderPramId, FNHSysCountryId, FTCostingLO, "
                _Str &= vbCrLf & "FNTotalFabAmt, FNTotalAccAmt, FNChargeFabAmt, FNChargeAccAmt, FNPackagingAmt, FNNoSawAppCostAmt, "
                _Str &= vbCrLf & "FNGarmentTreatmentAmt, FNOtherCostAmt, FNCMP, FTStartSize, FTEndSize, FNNormalSizeAmt, "
                _Str &= vbCrLf & "FTAboveSizeBreakDownSpecial, FNAboveSpecialSizeChargePerAmt, FNAboveSpecialSizeAmt, FTLessThanSizeBreakDownSpecial, "
                _Str &= vbCrLf & "FNLessThanSpecialSizeChargePerAmt, FNLessThanSpecialSizeAmt, FTRemark, FTStateActive "
                _Str &= vbCrLf & ",FTSamFabric ,FTSamTrims ,FTSamPack ,FTSamNoSew ,FTSamGarment ,FTSamOtherCost ,FTSamCMP ,FNHSysCmpId,FNVersion "

                _Str &= vbCrLf & ",  FNISTeamMulti, FNCostSheetColor, FNCostSheetSize, FNCostSheetBuyType,  FNCostSheetQuotedType, FTDateQuoted, FNCostSheetSampleRound, FNHSysStyleIdTo, FTQuotedLog, FNL4Country1, FNL4Country1Cur,"
                _Str &= vbCrLf & "    FNL4Country1Exc, FNL4Country1Final, FNL4Country1Extended, FNL4Country2, FNL4Country2Cur, FNL4Country2Exc, FNL4Country2Final, FNL4Country2Extended, FNL4Country3, FNL4Country3Cur, FNL4Country3Exc,"
                _Str &= vbCrLf & "    FNL4Country3Final, FNL4Country3Extended, FNProcessMatCost , FNProcessLaborCost, FNGrandTotal, FNExtendedPer, FNExtendedFOB, FNTrinUsageAllowPer, FNL4LTotalFabric, FNL4LTotalTrim, FNL4LChargeFabric, FNL4LChargeTrim,"
                _Str &= vbCrLf & "    FNL4LProMatCost, FNL4LProLaborCost, FNL4LPackaging, FNL4LOtherCost, FNL4LCMP, FNL4LFinalFOB, FNL4LExtendedFOB )"

                _Str &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                '  _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
                _Str &= vbCrLf & ",'" & _Key & "'"
                _Str &= vbCrLf & ",0"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDCostSheetDate.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTCostSheetBy.Text) & "'"
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysStyleId.Properties.Tag)
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysSeasonId.Properties.Tag)
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTExp.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTGarmentEngineer.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSILH.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTLOProductDeveloper.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDevelopmentRegion.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTProductDevelopmentManager.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTMSC.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDESC.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDBomDate.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDSpecDate.Text) & "'"
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysCurId.Properties.Tag)
                _Str &= vbCrLf & "," & Me.FNExchangeRate.Value
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysVenderPramId.Properties.Tag)
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysCountryId.Properties.Tag)
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTCostingLO.Text) & "'"
                _Str &= vbCrLf & "," & Me.FNTotalFabAmt.Value
                _Str &= vbCrLf & "," & Me.FNTotalAccAmt.Value
                _Str &= vbCrLf & "," & Me.FNChargeFabAmt.Value
                _Str &= vbCrLf & "," & Me.FNChargeAccAmt.Value
                _Str &= vbCrLf & "," & Me.FNPackagingAmt.Value
                _Str &= vbCrLf & "," & Me.FNNoSewAppCostAmt.Value
                _Str &= vbCrLf & "," & Me.FNGarmentTreatmentAmt.Value
                _Str &= vbCrLf & "," & Me.FNOtherCostAmt.Value
                _Str &= vbCrLf & "," & Me.FNCMP.Value
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNHSysMatSizeId.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTMatSizeCode.Text) & "'"
                _Str &= vbCrLf & "," & Me.FNNormalSizeAmt.Value
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTAboveSizeBreakDownSpecial.Text) & "'"
                _Str &= vbCrLf & "," & Me.FNAboveSpecialSizeChargePerAmt.Value
                _Str &= vbCrLf & "," & Me.FNAboveSpecialSizeAmt.Value
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTLessThanSizeBreakDownSpecial.Text) & "'"
                _Str &= vbCrLf & "," & Me.FNLessThanSpecialSizeChargePerAmt.Value
                _Str &= vbCrLf & "," & Me.FNLessThanSpecialSizeAmt.Value
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Str &= vbCrLf & "," & Me.ActState & " "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSamFabric.Text) & "'"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSamTrims.Text) & "'"
                _Str &= vbCrLf & "," & Me.FTSamPack.Value
                _Str &= vbCrLf & "," & Me.FTSamNoSew.Value
                _Str &= vbCrLf & "," & Me.FTSamGarment.Value
                _Str &= vbCrLf & "," & Me.FTSamOtherCost.Value
                _Str &= vbCrLf & "," & Me.FTSamCMP.Value
                _Str &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                _Str &= vbCrLf & "," & FNVersion.Value & " "

                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNISTeamMulti.Text) & "' AS FNISTeamMulti"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNCostSheetColor.Text) & "' AS FNCostSheetColor"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNCostSheetSize.Text) & "' AS FNCostSheetSize "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNCostSheetBuyType.Text) & "' AS FNCostSheetBuyType"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNCostSheetQuotedType.Text) & "' AS FNCostSheetQuotedType"
                _Str &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateQuoted.Text) & "' AS FTDateQuoted"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNCostSheetSampleRound.Text) & "' AS FNCostSheetSampleRound"
                _Str &= vbCrLf & "," & Integer.Parse("0" & Me.FNHSysStyleIdTo.Properties.Tag) & " As FNHSysStyleIdTo "
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTQuotedLog.Text) & "' AS FTQuotedLog"

                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNL4Country1.Text) & "' AS FNL4Country1"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNL4Country1Cur.Text) & "' AS FNL4Country1Cur"
                _Str &= vbCrLf & "," & Me.FNL4Country1Exc.Value & " AS FNL4Country1Exc"
                _Str &= vbCrLf & "," & Me.FNL4Country1Finalm.Value & " AS FNL4Country1Final"
                _Str &= vbCrLf & "," & Me.FNL4Country1Extendedm.Value & " AS FNL4Country1Extended"

                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNL4Country2.Text) & "' AS  FNL4Country2"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNL4Country2Cur.Text) & "' AS FNL4Country2Cur "
                _Str &= vbCrLf & "," & Me.FNL4Country2Exc.Value & " AS FNL4Country2Exc"
                _Str &= vbCrLf & "," & Me.FNL4Country2Finalm.Value & " AS FNL4CountryFinal"
                _Str &= vbCrLf & "," & Me.FNL4Country2Extendedm.Value & " AS FNL4CountryExtended"

                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNL4Country3.Text) & "' AS FNL4Country3"
                _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNL4Country3Cur.Text) & "' AS FNL4Country3Cur"
                _Str &= vbCrLf & "," & Me.FNL4Country3Exc.Value & " AS FNL4Country3Exc"
                _Str &= vbCrLf & "," & Me.FNL4Country3Finalm.Value & " AS FNL4Country3Final"
                _Str &= vbCrLf & "," & Me.FNL4Country3Extendedm.Value & " AS FNL4Country3Extended"


                _Str &= vbCrLf & "," & Me.FNProcessMatCost.Value & " AS FNProcessMatCost"
                _Str &= vbCrLf & "," & Me.FNProcessLaborCost.Value & " AS FNProcessLaborCost"
                _Str &= vbCrLf & "," & Me.FNGrandTotal.Value & " AS FNGrandTotal"
                _Str &= vbCrLf & "," & Me.FNExtendedPer.Value & " AS FNExtendedPer"
                _Str &= vbCrLf & "," & Me.FNExtendedFOB.Value & " AS FNExtendedFOB"

                _Str &= vbCrLf & "," & Me.FNTrinUsageAllowPer.Value & " AS FNTrinUsageAllowPer"

                _Str &= vbCrLf & "," & Me.FNL4LTotalFabric.Value & " AS FNL4LTotalFabric"
                _Str &= vbCrLf & "," & Me.FNL4LTotalTrim.Value & " AS FNL4LTotalTrim"
                _Str &= vbCrLf & "," & Me.FNL4LChargeFabric.Value & " AS FNL4LChargeFabric"
                _Str &= vbCrLf & "," & Me.FNL4LChargeTrim.Value & " AS FNL4LChargeTrim"
                _Str &= vbCrLf & "," & Me.FNL4LProMatCost.Value & " AS FNL4LProMatCost"


                _Str &= vbCrLf & "," & Me.FNL4LProLaborCost.Value & " AS FNL4LProLaborCost"
                _Str &= vbCrLf & "," & Me.FNL4LPackaging.Value & " AS FNL4LPackaging"
                _Str &= vbCrLf & "," & Me.FNL4LOtherCost.Value & " AS FNL4LOtherCost"
                _Str &= vbCrLf & "," & Me.FNL4LCMP.Value & " AS FNL4LCMP"
                _Str &= vbCrLf & "," & Me.FNL4LFinalFOB.Value & " AS FNL4LFinalFOB"
                _Str &= vbCrLf & "," & Me.FNL4LExtendedFOB.Value & " AS FNL4LExtendedFOB"



                'If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '    HI.Conn.SQLConn.Tran.Rollback()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    Return False
                'End If

            Else

                _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet "
                _Str &= vbCrLf & "set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & ",FNHSysStyleId=" & Integer.Parse("0" & Me.FNHSysStyleId.Properties.Tag)
                _Str &= vbCrLf & ",FNHSysSeasonId=" & Integer.Parse("0" & Me.FNHSysSeasonId.Properties.Tag)
                _Str &= vbCrLf & ",FTExp='" & HI.UL.ULF.rpQuoted(Me.FTExp.Text) & "'"
                _Str &= vbCrLf & ",FTGarmentEngineer='" & HI.UL.ULF.rpQuoted(Me.FTGarmentEngineer.Text) & "'"
                _Str &= vbCrLf & ",FTSILH='" & HI.UL.ULF.rpQuoted(Me.FTSILH.Text) & "'"
                _Str &= vbCrLf & ",FTLOProductDeveloper='" & HI.UL.ULF.rpQuoted(Me.FTLOProductDeveloper.Text) & "'"
                _Str &= vbCrLf & ",FTDevelopmentRegion='" & HI.UL.ULF.rpQuoted(Me.FTDevelopmentRegion.Text) & "'"
                _Str &= vbCrLf & ",FTProductDevelopmentManager='" & HI.UL.ULF.rpQuoted(Me.FTProductDevelopmentManager.Text) & "'"
                _Str &= vbCrLf & ",FTMSC='" & HI.UL.ULF.rpQuoted(Me.FTMSC.Text) & "'"
                _Str &= vbCrLf & ",FTDESC='" & HI.UL.ULF.rpQuoted(Me.FTDESC.Text) & "'"
                _Str &= vbCrLf & ",FDBomDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDBomDate.Text) & "'"
                _Str &= vbCrLf & ",FDSpecDate='" & HI.UL.ULDate.ConvertEnDB(Me.FDSpecDate.Text) & "'"
                _Str &= vbCrLf & ",FNHSysCurId=" & Integer.Parse("0" & Me.FNHSysCurId.Properties.Tag)
                _Str &= vbCrLf & ",FNExchangeRate=" & Me.FNExchangeRate.Value
                _Str &= vbCrLf & ",FNHSysVenderPramId=" & Integer.Parse("0" & Me.FNHSysVenderPramId.Properties.Tag)
                _Str &= vbCrLf & ",FNHSysCountryId=" & Integer.Parse("0" & Me.FNHSysCountryId.Properties.Tag)
                _Str &= vbCrLf & ",FTCostingLO='" & HI.UL.ULF.rpQuoted(Me.FTCostingLO.Text) & "'"
                _Str &= vbCrLf & ",FNTotalFabAmt=" & Me.FNTotalFabAmt.Value
                _Str &= vbCrLf & ",FNTotalAccAmt=" & Me.FNTotalAccAmt.Value
                _Str &= vbCrLf & ",FNChargeFabAmt=" & Me.FNChargeFabAmt.Value
                _Str &= vbCrLf & ",FNChargeAccAmt=" & Me.FNChargeAccAmt.Value
                _Str &= vbCrLf & ",FNPackagingAmt=" & Me.FNPackagingAmt.Value
                _Str &= vbCrLf & ",FNNoSawAppCostAmt=" & Me.FNNoSewAppCostAmt.Value
                _Str &= vbCrLf & ",FNGarmentTreatmentAmt=" & Me.FNGarmentTreatmentAmt.Value
                _Str &= vbCrLf & ",FNOtherCostAmt=" & Me.FNOtherCostAmt.Value
                _Str &= vbCrLf & ",FNCMP=" & Me.FNCMP.Value
                _Str &= vbCrLf & ",FTStartSize='" & HI.UL.ULF.rpQuoted(Me.FNHSysMatSizeId.Text) & "'"
                _Str &= vbCrLf & ",FTEndSize='" & HI.UL.ULF.rpQuoted(Me.FTMatSizeCode.Text) & "'"
                _Str &= vbCrLf & ",FNNormalSizeAmt=" & Me.FNNormalSizeAmt.Value
                _Str &= vbCrLf & ",FTAboveSizeBreakDownSpecial='" & HI.UL.ULF.rpQuoted(Me.FTAboveSizeBreakDownSpecial.Text) & "'"
                _Str &= vbCrLf & ",FNAboveSpecialSizeChargePerAmt=" & Me.FNAboveSpecialSizeChargePerAmt.Value
                _Str &= vbCrLf & ",FNAboveSpecialSizeAmt=" & Me.FNAboveSpecialSizeAmt.Value
                _Str &= vbCrLf & ",FTLessThanSizeBreakDownSpecial='" & HI.UL.ULF.rpQuoted(Me.FTLessThanSizeBreakDownSpecial.Text) & "'"
                _Str &= vbCrLf & ",FNLessThanSpecialSizeChargePerAmt=" & Me.FNLessThanSpecialSizeChargePerAmt.Value
                _Str &= vbCrLf & ",FNLessThanSpecialSizeAmt=" & Me.FNLessThanSpecialSizeAmt.Value
                _Str &= vbCrLf & ",FTRemark= '" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Str &= vbCrLf & ",FTStateActive =" & Me.ActState & " "
                _Str &= vbCrLf & ",FTSamFabric = '" & HI.UL.ULF.rpQuoted(Me.FTSamFabric.Text) & "'"
                _Str &= vbCrLf & ",FTSamTrims = '" & HI.UL.ULF.rpQuoted(Me.FTSamTrims.Text) & "'"
                _Str &= vbCrLf & ",FTSamPack = " & Me.FTSamPack.Value
                _Str &= vbCrLf & ",FTSamNoSew = " & Me.FTSamNoSew.Value
                _Str &= vbCrLf & ",FTSamGarment = " & Me.FTSamGarment.Value
                _Str &= vbCrLf & ",FTSamOtherCost = " & Me.FTSamOtherCost.Value
                _Str &= vbCrLf & ",FTSamCMP = " & Me.FTSamCMP.Value
                _Str &= vbCrLf & ", FNRevised = CASE WHEN  ISNULL(FTStateApp,'') = '1' THEN FNRevised  + 1 ELSE FNRevised END "



                _Str &= vbCrLf & ",FNISTeamMulti='" & HI.UL.ULF.rpQuoted(Me.FNISTeamMulti.Text) & "'"
                _Str &= vbCrLf & ",FNCostSheetColor='" & HI.UL.ULF.rpQuoted(Me.FNCostSheetColor.Text) & "'"
                _Str &= vbCrLf & ",FNCostSheetSize='" & HI.UL.ULF.rpQuoted(Me.FNCostSheetSize.Text) & "'"
                _Str &= vbCrLf & ",FNCostSheetBuyType='" & HI.UL.ULF.rpQuoted(Me.FNCostSheetBuyType.Text) & "'"
                _Str &= vbCrLf & ",FNCostSheetQuotedType='" & HI.UL.ULF.rpQuoted(Me.FNCostSheetQuotedType.Text) & "'"
                _Str &= vbCrLf & ",FTDateQuoted='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateQuoted.Text) & "'"
                _Str &= vbCrLf & ",FNCostSheetSampleRound='" & HI.UL.ULF.rpQuoted(Me.FNCostSheetSampleRound.Text) & "'"
                _Str &= vbCrLf & ",FNHSysStyleIdTo=" & Integer.Parse("0" & Me.FNHSysStyleIdTo.Properties.Tag)
                _Str &= vbCrLf & ",FTQuotedLog='" & HI.UL.ULF.rpQuoted(Me.FTQuotedLog.Text) & "'"

                _Str &= vbCrLf & ",FNL4Country1='" & HI.UL.ULF.rpQuoted(Me.FNL4Country1.Text) & "'"
                _Str &= vbCrLf & ",FNL4Country1Cur='" & HI.UL.ULF.rpQuoted(Me.FNL4Country1Cur.Text) & "'"
                _Str &= vbCrLf & ",FNL4Country1Exc=" & Me.FNL4Country1Exc.Value
                _Str &= vbCrLf & ",FNL4Country1Final=" & Me.FNL4Country1Finalm.Value
                _Str &= vbCrLf & ",FNL4Country1Extended=" & Me.FNL4Country1Extendedm.Value

                _Str &= vbCrLf & ",FNL4Country2='" & HI.UL.ULF.rpQuoted(Me.FNL4Country2.Text) & "'"
                _Str &= vbCrLf & ",FNL4Country2Cur='" & HI.UL.ULF.rpQuoted(Me.FNL4Country2Cur.Text) & "'"
                _Str &= vbCrLf & ",FNL4Country2Exc=" & Me.FNL4Country2Exc.Value
                _Str &= vbCrLf & ",FNL4Country2Final=" & Me.FNL4Country2Finalm.Value
                _Str &= vbCrLf & ",FNL4Country2Extended=" & Me.FNL4Country2Extendedm.Value

                _Str &= vbCrLf & ",FNL4Country3='" & HI.UL.ULF.rpQuoted(Me.FNL4Country3.Text) & "'"
                _Str &= vbCrLf & ",FNL4Country3Cur='" & HI.UL.ULF.rpQuoted(Me.FNL4Country3Cur.Text) & "'"
                _Str &= vbCrLf & ",FNL4Country3Exc=" & Me.FNL4Country3Exc.Value
                _Str &= vbCrLf & ",FNL4Country3Final=" & Me.FNL4Country3Finalm.Value
                _Str &= vbCrLf & ",FNL4Country3Extended=" & Me.FNL4Country3Extendedm.Value


                _Str &= vbCrLf & ",FNProcessMatCost=" & Me.FNProcessMatCost.Value
                _Str &= vbCrLf & ",FNProcessLaborCost=" & Me.FNProcessLaborCost.Value
                _Str &= vbCrLf & ",FNGrandTotal=" & Me.FNGrandTotal.Value
                _Str &= vbCrLf & ",FNExtendedPer=" & Me.FNExtendedPer.Value
                _Str &= vbCrLf & ",FNExtendedFOB=" & Me.FNExtendedFOB.Value

                _Str &= vbCrLf & ",FNTrinUsageAllowPer=" & Me.FNTrinUsageAllowPer.Value

                _Str &= vbCrLf & ",FNL4LTotalFabric=" & Me.FNL4LTotalFabric.Value
                _Str &= vbCrLf & ",FNL4LTotalTrim=" & Me.FNL4LTotalTrim.Value
                _Str &= vbCrLf & ",FNL4LChargeFabric=" & Me.FNL4LChargeFabric.Value
                _Str &= vbCrLf & ",FNL4LChargeTrim=" & Me.FNL4LChargeTrim.Value

                _Str &= vbCrLf & ",FNL4LProMatCost=" & Me.FNL4LProMatCost.Value
                _Str &= vbCrLf & ",FNL4LProLaborCost=" & Me.FNL4LProLaborCost.Value
                _Str &= vbCrLf & ",FNL4LPackaging=" & Me.FNL4LPackaging.Value
                _Str &= vbCrLf & ",FNL4LOtherCost=" & Me.FNL4LOtherCost.Value
                _Str &= vbCrLf & ",FNL4LCMP=" & Me.FNL4LCMP.Value
                _Str &= vbCrLf & ",FNL4LFinalFOB=" & Me.FNL4LFinalFOB.Value
                _Str &= vbCrLf & ",FNL4LExtendedFOB=" & Me.FNL4LExtendedFOB.Value


                _Str &= vbCrLf & "WHERE FTCostSheetNo= '" & _Key & "' "
                '_Str &= vbCrLf & "WHERE FTCostSheetNo= '" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNRevised ='" & FNRevised.Value & "'"

            End If

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _FileName As String : Dim data As Byte()
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)

                Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))

                If Not (data Is Nothing) Then
                    Dim _Cmd As String = ""
                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet"
                    _Cmd &= " Set  FBDocument=@FBDocument"
                    _Cmd &= "  Where FTCostSheetNo=@FTCostSheetNo " ' AND FNRevised=@FNRevised"
                    Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    'cmd.Parameters.AddWithValue("@FTDocumentTitle", FTDocumentTitle)
                    Dim p6 As New SqlParameter("@FBDocument", SqlDbType.VarBinary)
                    p6.Value = data
                    'Dim p7 As New SqlParameter("@FNRevised", SqlDbType.Int)
                    'p7.Value = Me.FNRevised.Value
                    Dim p8 As New SqlParameter("@FTCostSheetNo", SqlDbType.NVarChar)
                    p8.Value = _Key
                    cmd.Parameters.Add(p6)
                    '  cmd.Parameters.Add(p7)
                    cmd.Parameters.Add(p8)
                    cmd.ExecuteNonQuery()
                End If
            End If

            Try
                Dim _Qry As String = ""
                Dim _Seq As Integer = 0
                Dim _ogc As Object

                For _tl As Integer = 1 To 8

                    _ogc = Nothing
                    _Seq = 0

                    Select Case _tl
                        Case 1
                            'Save Fabric
                            _ogc = ogcfabric
                        Case 2
                            'Save Acc
                            _ogc = ogctrims
                        Case 3
                            'Save Process Material
                            _ogc = ogcprocessmat
                        Case 4
                            'Save Process labor
                            _ogc = ogcprocesslabor
                        Case 5
                            'Save packaging
                            _ogc = ogcpack
                        Case 6
                            'Save CMP
                            _ogc = ogccmp
                        Case 7
                            'Save BEMIS
                            _ogc = ogcbemis
                        Case 8
                            'Save Team Multi

                            If Me.otpTeamMulti.PageVisible Then
                                _ogc = ogcteamMulti

                            Else


                                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
                                _Qry &= vbCrLf & "WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNCostType = '" & _tl & "' "


                                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                            End If
                    End Select





                    If Not (_ogc.DataSource Is Nothing) Then
                        _Seq = 0
                        Dim dt As DataTable
                        With CType(_ogc.DataSource, DataTable)
                            .AcceptChanges()
                            dt = .Copy
                        End With

                        For Each R As DataRow In dt.Select("FNSeq>0", "FNSeq")
                            Dim _FNHSysUnitId As String = HI.Conn.SQLConn.GetField("SELECT FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) WHERE FTUnitCode='" & R!FTUnitCode.ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                            _Seq += +1
                            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
                            _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",FNWeight=" & CDbl("0" & R!FNWeight.ToString)
                            _Qry &= vbCrLf & ",FNWidth=" & CDbl("0" & R!FNWidth.ToString)
                            _Qry &= vbCrLf & ",FNMarkerEff=" & CDbl("0" & R!FNMarkerEff.ToString)
                            _Qry &= vbCrLf & ",FNMarkerUsed=" & CDbl("0" & R!FNMarkerUsed.ToString)
                            _Qry &= vbCrLf & ",FNAllowancePer=" & CDbl("0" & R!FNAllowancePer.ToString)
                            _Qry &= vbCrLf & ",FNTotalUsed=" & CDbl("0" & R!FNTotalUsed.ToString)
                            _Qry &= vbCrLf & ",FNRMDSStatus='" & R!FNRMDSStatus.ToString & "'"
                            _Qry &= vbCrLf & ",FNHSysSuplId=" & CInt("0" & R!FNHSysSuplId.ToString)
                            _Qry &= vbCrLf & ",FNCostPerUOM=" & CDbl("0" & R!FNCostPerUOM.ToString)
                            _Qry &= vbCrLf & ",FNCostPerPiece=" & CDbl("0" & R!FNCostPerPiece.ToString)
                            _Qry &= vbCrLf & ",FNCIF=" & CDbl("0" & R!FNCIF.ToString)
                            _Qry &= vbCrLf & ",FNExten=" & CDbl("0" & R!FNExten.ToString)
                            _Qry &= vbCrLf & ",FNExtenPer=" & CDbl("0" & R!FNExtenPer.ToString)
                            _Qry &= vbCrLf & ",FNNetExten=" & CDbl("0" & R!FNNetExten.ToString)
                            _Qry &= vbCrLf & ",FNImportDuty=" & CDbl("0" & R!FNImportDuty.ToString)
                            _Qry &= vbCrLf & ",FNChinaOrderCost=" & CDbl("0" & R!FNChinaOrderCost.ToString)
                            _Qry &= vbCrLf & ",FNMalaysiaOrderCost=" & CDbl("0" & R!FNMalaysiaOrderCost.ToString)
                            _Qry &= vbCrLf & ",FNThailandOrderCost=" & CDbl("0" & R!FNThailandOrderCost.ToString)
                            _Qry &= vbCrLf & ",FNJapanOrderCost=" & CDbl("0" & R!FNJapanOrderCost.ToString)
                            _Qry &= vbCrLf & ",FNHSysUnitId=" & CDbl("0" & _FNHSysUnitId)
                            _Qry &= vbCrLf & ",FTUse='" & HI.UL.ULF.rpQuoted(R!FTUse.ToString) & "'"
                            _Qry &= vbCrLf & ",FTSize='" & HI.UL.ULF.rpQuoted(R!FTSize.ToString) & "'"
                            _Qry &= vbCrLf & ",TTLG='" & HI.UL.ULF.rpQuoted(R!TTLG.ToString) & "'"


                            _Qry &= vbCrLf & ",FTMainMatCode='" & HI.UL.ULF.rpQuoted(R!FTMainMatCode.ToString) & "'"
                            _Qry &= vbCrLf & ",FTMainMatColorCode='" & HI.UL.ULF.rpQuoted(R!FTMainMatColorCode.ToString) & "'"
                            _Qry &= vbCrLf & ",FTMainMatName='" & HI.UL.ULF.rpQuoted(R!FTMainMatName.ToString) & "'"
                            _Qry &= vbCrLf & ",FTSuplCode='" & HI.UL.ULF.rpQuoted(R!FTSuplCode.ToString) & "'"
                            _Qry &= vbCrLf & ",FTRMDSSeason='" & HI.UL.ULF.rpQuoted(R!FTRMDSSeason.ToString) & "'"
                            _Qry &= vbCrLf & ",FTUnitCode='" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"

                            _Qry &= vbCrLf & ",FNUSAGECOST=" & CDbl("0" & R!FNUSAGECOST.ToString)

                            _Qry &= vbCrLf & ",FNHANDLINGCHARGEPERCENT=" & CDbl("0" & R!FNHANDLINGCHARGEPERCENT.ToString)
                            _Qry &= vbCrLf & ",FNHANDLINGCHARGECOST=" & CDbl("0" & R!FNHANDLINGCHARGECOST.ToString)
                            _Qry &= vbCrLf & ",FNIMPORTDUTYPERCENT=" & CDbl("0" & R!FNIMPORTDUTYPERCENT.ToString)

                            _Qry &= vbCrLf & ",FTPROCESSSUBTYPE='" & HI.UL.ULF.rpQuoted(R!FTPROCESSSUBTYPE.ToString) & "'"
                            _Qry &= vbCrLf & ",FNHSysProcessMatId=0 "

                            _Qry &= vbCrLf & ",FNSTANDARDALLOWEDMINUTES=" & CDbl("0" & R!FNSTANDARDALLOWEDMINUTES.ToString)
                            _Qry &= vbCrLf & ",FNEFFICIENCYPERCENT=" & CDbl("0" & R!FNEFFICIENCYPERCENT.ToString)
                            _Qry &= vbCrLf & ",FNPROFITPERCENT=" & CDbl("0" & R!FNPROFITPERCENT.ToString)
                            _Qry &= vbCrLf & ",FNCMPCOST=" & CDbl("0" & R!FNCMPCOST.ToString)
                            _Qry &= vbCrLf & ",FTBMCCODE='" & HI.UL.ULF.rpQuoted(R!FTBMCCODE.ToString) & "'"
                            _Qry &= vbCrLf & ",FTBEMISITEM='" & HI.UL.ULF.rpQuoted(R!FTMainMatCode.ToString) & "'"

                            _Qry &= vbCrLf & ",FNFULLWIDTH=" & CDbl("0" & R!FNFULLWIDTH.ToString)
                            _Qry &= vbCrLf & ",FNSLITTINGWIDTH=" & CDbl("0" & R!FNSLITTINGWIDTH.ToString)
                            _Qry &= vbCrLf & ",FNREQUIREDLENGTH=" & CDbl("0" & R!FNREQUIREDLENGTH.ToString)
                            _Qry &= vbCrLf & ",FNNETUSAGEINFULLWIDTH=" & CDbl("0" & R!FNNETUSAGEINFULLWIDTH.ToString)
                            _Qry &= vbCrLf & ",FNPRICEINMETER=" & CDbl("0" & R!FNPRICEINMETER.ToString)
                            _Qry &= vbCrLf & ",FNBEMISSLITTINGUPCHARGE=" & CDbl("0" & R!FNBEMISSLITTINGUPCHARGE.ToString)
                            _Qry &= vbCrLf & ",FNPRICEPERSLITTINGWITDH=" & CDbl("0" & R!FNPRICEPERSLITTINGWITDH.ToString)

                            _Qry &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"

                            _Qry &= vbCrLf & ",FNTOTALUSAGECOST=" & CDbl("0" & R!FNTOTALUSAGECOST.ToString)
                            _Qry &= vbCrLf & ",FNTOTALHANDINGCHANGECOST=" & CDbl("0" & R!FNTOTALHANDINGCHANGECOST.ToString)
                            _Qry &= vbCrLf & ",FNFINALFOB=" & CDbl("0" & R!FNFINALFOB.ToString)
                            _Qry &= vbCrLf & ",FNEXTENDEDSIZEFOB=" & CDbl("0" & R!FNEXTENDEDSIZEFOB.ToString)
                            _Qry &= vbCrLf & ",FNTOTALTRIMPROCESSCOST=" & CDbl("0" & R!FNTOTALTRIMPROCESSCOST.ToString)
                            _Qry &= vbCrLf & ",FTL4LORDERCNTY1=" & CDbl("0" & R!FTL4LORDERCNTY1.ToString)

                            _Qry &= vbCrLf & ",FTL4LCURRENCYFOB1=" & CDbl("0" & R!FTL4LCURRENCYFOB1.ToString)
                            _Qry &= vbCrLf & ",FNEXTENDSIZEFOBL4L1=" & CDbl("0" & R!FNEXTENDSIZEFOBL4L1.ToString)
                            _Qry &= vbCrLf & ",FTL4LORDERCNTY2=" & CDbl("0" & R!FTL4LORDERCNTY2.ToString)
                            _Qry &= vbCrLf & ",FTL4LCURRENCYFOB2=" & CDbl("0" & R!FTL4LCURRENCYFOB2.ToString)

                            _Qry &= vbCrLf & ",FNEXTENDSIZEFOBL4L2=" & CDbl("0" & R!FNEXTENDSIZEFOBL4L2.ToString)
                            _Qry &= vbCrLf & ",FTL4LORDERCNTY3=" & CDbl("0" & R!FTL4LORDERCNTY3.ToString)
                            _Qry &= vbCrLf & ",FTL4LCURRENCYFOB3=" & CDbl("0" & R!FTL4LCURRENCYFOB3.ToString)
                            _Qry &= vbCrLf & ",FNEXTENDSIZEFOBL4L3=" & CDbl("0" & R!FNEXTENDSIZEFOBL4L3.ToString)


                            _Qry &= vbCrLf & ",FTPRODUCTDEVELOPER='" & HI.UL.ULF.rpQuoted(R!FTPRODUCTDEVELOPER.ToString) & "'"

                            _Qry &= vbCrLf & ",FNHSysMainMatId=" & CDbl("0" & R!FNHSysMainMatId.ToString)

                            _Qry &= vbCrLf & ",FTWidthUnit='" & HI.UL.ULF.rpQuoted(R!FTWidthUnit.ToString) & "'"

                            _Qry &= vbCrLf & ",FTTeamName='" & HI.UL.ULF.rpQuoted(R!FTTeamName.ToString) & "'"

                            _Qry &= vbCrLf & ",FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"

                            _Qry &= vbCrLf & "WHERE FTCostSheetNo='" & _Key & "'" ' AND FNRevised = '" & FNRevised.Value & "'"
                            '_Qry &= vbCrLf & "WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNRevised = '" & FNRevised.Value & "'"
                            '_Qry &= vbCrLf & " AND FNHSysMainMatId = '" & R!FNHSysMerMatId.ToString & "' AND FNCostType = '" & R!FNCostType.ToString & "' AND FNSeq=" & _Seq
                            _Qry &= vbCrLf & " AND FNCostType = '" & _tl & "' AND FNSeq=" & _Seq

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
                                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTCostSheetNo, FNRevised, FNCostType, "
                                _Qry &= vbCrLf & "FNSeq, FNHSysMainMatId, FNWeight, FNWidth, FNMarkerEff, FNMarkerUsed, FNAllowancePer"
                                _Qry &= vbCrLf & ", FNTotalUsed, FNRMDSStatus, FNHSysSuplId, FNCostPerUOM, FNHSysUnitId, FNCostPerPiece"
                                _Qry &= vbCrLf & ", FNCIF, FNExten, FNExtenPer, FNNetExten, FNImportDuty, FNChinaOrderCost"
                                _Qry &= vbCrLf & ", FNMalaysiaOrderCost, FNThailandOrderCost, FNJapanOrderCost"
                                _Qry &= vbCrLf & ", FTUse ,FTSize,TTLG"
                                _Qry &= vbCrLf & ",FTMainMatCode,FTMainMatColorCode,FTMainMatName,FTSuplCode,FTRMDSSeason,FTUnitCode"

                                _Qry &= vbCrLf & ",FNUSAGECOST,FNHANDLINGCHARGEPERCENT,FNHANDLINGCHARGECOST,FNIMPORTDUTYPERCENT,FTPROCESSSUBTYPE,FNHSysProcessMatId"
                                _Qry &= vbCrLf & ",FNSTANDARDALLOWEDMINUTES,FNEFFICIENCYPERCENT,FNPROFITPERCENT,FNCMPCOST,FTBMCCODE,FTBEMISITEM"
                                _Qry &= vbCrLf & ",FNFULLWIDTH,FNSLITTINGWIDTH,FNREQUIREDLENGTH,FNNETUSAGEINFULLWIDTH,FNPRICEINMETER,FNBEMISSLITTINGUPCHARGE"
                                _Qry &= vbCrLf & ",FNPRICEPERSLITTINGWITDH,FTRemark,FNTOTALUSAGECOST,FNTOTALHANDINGCHANGECOST,FNFINALFOB,FNEXTENDEDSIZEFOB"
                                _Qry &= vbCrLf & ",FNTOTALTRIMPROCESSCOST,FTL4LORDERCNTY1,FTL4LCURRENCYFOB1,FNEXTENDSIZEFOBL4L1,FTL4LORDERCNTY2,FTL4LCURRENCYFOB2"
                                _Qry &= vbCrLf & ",FNEXTENDSIZEFOBL4L2,FTL4LORDERCNTY3,FTL4LCURRENCYFOB3,FNEXTENDSIZEFOBL4L3,FTPRODUCTDEVELOPER,FNVersion,FTWidthUnit,FTTeamName,FTStyleCode"
                                _Qry &= vbCrLf & ")"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
                                _Qry &= vbCrLf & ",'" & _Key & "'"
                                _Qry &= vbCrLf & ",0"
                                _Qry &= vbCrLf & "," & _tl
                                _Qry &= vbCrLf & "," & _Seq
                                _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysMainMatId.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNWeight.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNWidth.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNMarkerEff.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNMarkerUsed.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNAllowancePer.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNTotalUsed.ToString)
                                _Qry &= vbCrLf & ",'" & R!FNRMDSStatus.ToString & "'"
                                _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysSuplId.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNCostPerUOM.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & _FNHSysUnitId)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNCostPerPiece.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNCIF.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNExten.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNExtenPer.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNNetExten.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNImportDuty.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNChinaOrderCost.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNMalaysiaOrderCost.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNThailandOrderCost.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNJapanOrderCost.ToString)
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTUse.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSize.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!TTLG.ToString) & "'"

                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatCode.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatColorCode.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatName.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSuplCode.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRMDSSeason.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTUnitCode.ToString) & "'"

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNUSAGECOST.ToString)

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNHANDLINGCHARGEPERCENT.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNHANDLINGCHARGECOST.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNIMPORTDUTYPERCENT.ToString)

                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPROCESSSUBTYPE.ToString) & "'"
                                _Qry &= vbCrLf & ",0 AS FNHSysProcessMatId"

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNSTANDARDALLOWEDMINUTES.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNEFFICIENCYPERCENT.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNPROFITPERCENT.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNCMPCOST.ToString)
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBMCCODE.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMainMatCode.ToString) & "' AS FTBEMISITEM"

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNFULLWIDTH.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNSLITTINGWIDTH.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNREQUIREDLENGTH.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNNETUSAGEINFULLWIDTH.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNPRICEINMETER.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNBEMISSLITTINGUPCHARGE.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNPRICEPERSLITTINGWITDH.ToString)

                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "'"

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNTOTALUSAGECOST.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNTOTALHANDINGCHANGECOST.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNFINALFOB.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNEXTENDEDSIZEFOB.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNTOTALTRIMPROCESSCOST.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FTL4LORDERCNTY1.ToString)

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FTL4LCURRENCYFOB1.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNEXTENDSIZEFOBL4L1.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FTL4LORDERCNTY2.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FTL4LCURRENCYFOB2.ToString)

                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNEXTENDSIZEFOBL4L2.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FTL4LORDERCNTY3.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FTL4LCURRENCYFOB3.ToString)
                                _Qry &= vbCrLf & "," & CDbl("0" & R!FNEXTENDSIZEFOBL4L3.ToString)
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPRODUCTDEVELOPER.ToString) & "'"
                                _Qry &= vbCrLf & "," & CDbl("0" & FNVersion.Value)
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTWidthUnit.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTTeamName.ToString) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                            End If
                        Next

                        _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
                        _Qry &= vbCrLf & "WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNCostType = '" & _tl & "' "
                        _Qry &= vbCrLf & "AND FNSeq >" & _Seq

                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If
                Next
            Catch ex As Exception

            End Try

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            'If (_Revise = False) Then
            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _Key
                            .Text = _Key
                        End With
                End Select
            Next
            ' End If


            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    'Private Function SaveDetail(ByVal _Key As String) As Boolean 'save detail in gridview to DB
    '    Try
    '        Dim _Qry As String = ""
    '        Dim _Seq As Integer = 0
    '        Dim _ogc As Object

    '        For _tl As Integer = 1 To 4
    '            If (_tl = 1) Then
    '                'Save Fabric
    '                _ogc = ogcfabric
    '            ElseIf (_tl = 2) Then
    '                'Save Trims
    '                _ogc = ogctrims
    '            ElseIf (_tl = 3) Then
    '                'Save No Sew App
    '                _ogc = ogcnosew
    '            Else
    '                'Save packaging
    '                _ogc = ogcpack
    '            End If

    '            If Not (_ogc.DataSource Is Nothing) Then
    '                _Seq = 0
    '                Dim dt As DataTable
    '                With CType(_ogc.DataSource, DataTable)
    '                    .AcceptChanges()
    '                    dt = .Copy
    '                End With

    '                For Each R As DataRow In dt.Select("FNHSysMerMatId_None <> '' and FNMarkerUsed >= '0' and  FTUnitCode <> ''", "FNSeq")
    '                    Dim _FNHSysUnitId As String = HI.Conn.SQLConn.GetField("SELECT FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) WHERE FTUnitCode='" & R!FTUnitCode.ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "")
    '                    _Seq += +1
    '                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
    '                    _Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
    '                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
    '                    _Qry &= vbCrLf & ",FNWeight=" & CDbl("0" & R!FNWeight.ToString)
    '                    _Qry &= vbCrLf & ",FNWidth=" & CDbl("0" & R!FNWidth.ToString)
    '                    _Qry &= vbCrLf & ",FNMarkerEff=" & CDbl("0" & R!FNMarkerEff.ToString)
    '                    _Qry &= vbCrLf & ",FNMarkerUsed=" & CDbl("0" & R!FNMarkerUsed.ToString)
    '                    _Qry &= vbCrLf & ",FNAllowancePer=" & CDbl("0" & R!FNAllowancePer.ToString)
    '                    _Qry &= vbCrLf & ",FNTotalUsed=" & CDbl("0" & R!FNTotalUsed.ToString)
    '                    _Qry &= vbCrLf & ",FNRMDSStatus='" & R!FNRMDSStatus.ToString & "'"
    '                    _Qry &= vbCrLf & ",FNHSysSuplId=" & CInt("0" & R!FNHSysSuplId.ToString)
    '                    _Qry &= vbCrLf & ",FNCostPerUOM=" & CDbl("0" & R!FNCostPerUOM.ToString)
    '                    _Qry &= vbCrLf & ",FNCostPerPiece=" & CDbl("0" & R!FNCostPerPiece.ToString)
    '                    _Qry &= vbCrLf & ",FNCIF=" & CDbl("0" & R!FNCIF.ToString)
    '                    _Qry &= vbCrLf & ",FNExten=" & CDbl("0" & R!FNExten.ToString)
    '                    _Qry &= vbCrLf & ",FNExtenPer=" & CDbl("0" & R!FNExtenPer.ToString)
    '                    _Qry &= vbCrLf & ",FNNetExten=" & CDbl("0" & R!FNNetExten.ToString)
    '                    _Qry &= vbCrLf & ",FNImportDuty=" & CDbl("0" & R!FNImportDuty.ToString)
    '                    _Qry &= vbCrLf & ",FNChinaOrderCost=" & CDbl("0" & R!FNChinaOrderCost.ToString)
    '                    _Qry &= vbCrLf & ",FNMalaysiaOrderCost=" & CDbl("0" & R!FNMalaysiaOrderCost.ToString)
    '                    _Qry &= vbCrLf & ",FNThailandOrderCost=" & CDbl("0" & R!FNThailandOrderCost.ToString)
    '                    _Qry &= vbCrLf & ",FNJapanOrderCost=" & CDbl("0" & R!FNJapanOrderCost.ToString)
    '                    _Qry &= vbCrLf & ",FNHSysUnitId=" & _FNHSysUnitId
    '                    _Qry &= vbCrLf & ",FTUse='" & HI.UL.ULF.rpQuoted(R!FTUse.ToString) & "'"
    '                    _Qry &= vbCrLf & ",FTSize='" & HI.UL.ULF.rpQuoted(R!FTSize.ToString) & "'"
    '                    _Qry &= vbCrLf & ",TTLG='" & HI.UL.ULF.rpQuoted(R!TTLG.ToString) & "'"
    '                    _Qry &= vbCrLf & "WHERE FTCostSheetNo='" & _Key & "'" ' AND FNRevised = '" & FNRevised.Value & "'"
    '                    '_Qry &= vbCrLf & "WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNRevised = '" & FNRevised.Value & "'"
    '                    '_Qry &= vbCrLf & " AND FNHSysMainMatId = '" & R!FNHSysMerMatId.ToString & "' AND FNCostType = '" & R!FNCostType.ToString & "' AND FNSeq=" & _Seq
    '                    _Qry &= vbCrLf & " AND FNCostType = '" & R!FNCostType.ToString & "' AND FNSeq=" & _Seq

    '                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
    '                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTCostSheetNo, FNRevised, FNCostType, "
    '                        _Qry &= vbCrLf & "FNSeq, FNHSysMainMatId, FNWeight, FNWidth, FNMarkerEff, FNMarkerUsed, FNAllowancePer"
    '                        _Qry &= vbCrLf & ", FNTotalUsed, FNRMDSStatus, FNHSysSuplId, FNCostPerUOM, FNHSysUnitId, FNCostPerPiece"
    '                        _Qry &= vbCrLf & ", FNCIF, FNExten, FNExtenPer, FNNetExten, FNImportDuty, FNChinaOrderCost"
    '                        _Qry &= vbCrLf & ", FNMalaysiaOrderCost, FNThailandOrderCost, FNJapanOrderCost"
    '                        _Qry &= vbCrLf & ", FTUse ,FTSize,TTLG"
    '                        _Qry &= vbCrLf & ")"
    '                        _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
    '                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
    '                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
    '                        '_Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
    '                        _Qry &= vbCrLf & ",'" & _Key & "'"
    '                        _Qry &= vbCrLf & ",0"
    '                        _Qry &= vbCrLf & "," & CInt("0" & R!FNCostType.ToString)
    '                        _Qry &= vbCrLf & "," & _Seq
    '                        _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysMerMatId.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNWeight.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNWidth.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNMarkerEff.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNMarkerUsed.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNAllowancePer.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNTotalUsed.ToString)
    '                        _Qry &= vbCrLf & ",'" & R!FNRMDSStatus.ToString & "'"
    '                        _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysSuplId.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNCostPerUOM.ToString)
    '                        _Qry &= vbCrLf & "," & _FNHSysUnitId
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNCostPerPiece.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNCIF.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNExten.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNExtenPer.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNNetExten.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNImportDuty.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNChinaOrderCost.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNMalaysiaOrderCost.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNThailandOrderCost.ToString)
    '                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNJapanOrderCost.ToString)
    '                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTUse.ToString) & "'"
    '                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSize.ToString) & "'"
    '                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!TTLG.ToString) & "'"

    '                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                            HI.Conn.SQLConn.Tran.Rollback()
    '                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                            Return False
    '                        End If
    '                    End If
    '                Next

    '                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
    '                _Qry &= vbCrLf & "WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' AND FNCostType = '" & _tl & "' "
    '                _Qry &= vbCrLf & "AND FNSeq >" & _Seq

    '                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
    '            End If
    '        Next
    '    Catch ex As Exception
    '    End Try
    '    Return True
    'End Function

    Private Function DeleteData() As Boolean 'delete data from DB
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNo.Text) & "' " 'AND FNRevised ='" & Me.FNRevised.Value & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNo.Text) & "' " 'AND FNRevised ='" & Me.FNRevised.Value & "'"
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        _FormLoad = False

    End Sub
#End Region

#Region "MAIN PROC"

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click

        If CheckOwner() = False Then Exit Sub

        'Dim _Qry As String = ""
        'Dim Dt As DataTable

        '_Qry = "  Select FNRevised ,FTCostSheetNo"
        '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
        'Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        ''  If Me.VerrifyData Then
        ''If Dt.Rows.Count >= 0 Then
        'If (FNRevised.Value = Dt.Rows.Count - 1) Or (Dt.Rows.Count = 0) Then 'check lasted revised doc or new doc
        '    If Me.SaveData() Then
        '        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '        Call SaveData()
        '        Call LoadDataInfo2(FTCostSheetNo.Text)
        '    Else
        '        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '    End If
        'Else
        '    HI.MG.ShowMsg.mProcessError(1000000005, "ไม่สามารถแก้ไขเอกสารนี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        'End If

        If VerifyData() = False Then Exit Sub

        If Me.SaveData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            ' Call SaveData()
            Call LoadDataInfo2(FTCostSheetNo.Text)
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If

    End Sub

    Private Sub Proc_Delete(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CheckOwner() = False Then Exit Sub

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTCostSheetNo.Text, Me.Text) = False Then
            Exit Sub
        End If

        'Dim _Qry As String = ""
        'Dim Dt As DataTable

        '_Qry = "  Select FNRevised ,FTCostSheetNo"
        '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
        'Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        'If FNRevised.Value = Dt.Rows.Count - 1 Then 'check lasted revised doc
        If Me.DeleteData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            HI.TL.HandlerControl.ClearControl(Me)
            Me.DefaultsData()
            Me.FormRefresh()
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
        End If
        'Else
        '    HI.MG.ShowMsg.mProcessError(1000000006, "ไม่สามารถแก้ไขเอกสารนี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Me.FormRefresh()
        ' Call Me.ClearData()


        Me.ExcelAttach.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default)
        Me.ExcelAttach.CreateNewDocument()

        Me.FNRevised.Value = 0
    End Sub

    'Private Function ClearData()

    '    Me.FTExp.Text = ""
    '    Me.FTDevelopmentRegion.Text = ""
    '    Me.FTProductDevelopmentManager.Text = ""
    '    Me.FTGarmentEngineer.Text = ""
    '    Me.FTSILH.Text = ""
    '    Me.FNHSysCurId.Text = ""
    '    Me.FTLOProductDeveloper.Text = ""
    '    Me.FTMSC.Text = ""
    '    Me.FTDESC.Text = ""
    '    Me.FNExchangeRate.Value = 1
    '    Me.FNTotalFabAmt.Text = "0.00"
    '    Me.FNTotalAccAmt.Text = "0.00"
    '    Me.FNChargeFabAmt.Text = "0.00"
    '    Me.FNChargeAccAmt.Text = "0.00"
    '    Me.FNGarmentTreatmentAmt.Text = "0.00"
    '    Me.FNPackagingAmt.Text = "0.00"
    '    Me.FNNoSewAppCostAmt.Text = "0.00"
    '    Me.FNOtherCostAmt.Text = "0.00"
    '    Me.FNCMP.Text = "0.00"
    '    Me.FNHSysStyleId.Text = ""
    '    Me.FNHSysSeasonId.Text = ""

    '    Me.FDBomDate.Text = ""
    '    Me.FDSpecDate.Text = ""
    '    Me.FNHSysMatSizeId.Text = ""
    '    Me.FTMatSizeCode.Text = ""
    '    Me.FNHSysVenderPramId.Text = ""

    '    Me.FNHSysCountryId.Text = ""
    '    Me.FNHSysCountryId_None.Text = ""
    '    Me.FTCostingLO.Text = ""
    '    Me.FTLessThanSizeBreakDownSpecial.Text = ""
    '    Me.FNLessThanSpecialSizeChargePerAmt.Text = ""
    '    Me.FTAboveSizeBreakDownSpecial.Text = ""
    '    Me.FNAboveSpecialSizeChargePerAmt.Text = ""
    '    Me.FTRemark.Text = ""
    '    Me.FTStateActive.CheckState = 1
    '    Me.FTSamFabric.Text = ""
    '    Me.FTSamTrims.Text = ""
    '    Me.FTSamPack.Value = 0
    '    Me.FTSamNoSew.Value = 0
    '    Me.FTSamGarment.Value = 0
    '    Me.FTSamOtherCost.Value = 0
    '    Me.FTSamCMP.Value = 0
    '    'Me.ExcelAttach.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default)
    '    'Me.ExcelAttach.ActiveWorksheet.Clear(ExcelAttach.ActiveWorksheet.GetDataRange())
    '    Me.ExcelAttach.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default)
    '    Me.ExcelAttach.CreateNewDocument()
    'End Function

    Private Sub Proc_Preview(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        If Me.FTCostSheetNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Merchandise Report\"
                .ReportName = "ReportCostSheet.rpt"
                .Formular = "{TACCTCostSheet.FTCostSheetNo}  ='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNo.Text) & "' " 'AND {TACCTCostSheet.FNRevised}  =" & FNRevised.Value & " "
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCostSheetNo_lbl.Text)
            FTCostSheetNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ExportExcel(_Spls As HI.TL.SplashScreen)

        Try

            Try
                Dim proc = Process.GetProcessesByName("excel")
                For ix As Integer = 0 To proc.Count - 1
                    proc(ix).Kill()
                Next ix
            Catch ex As Exception
            End Try

            'Dim stream As New FileStream(_FileName, FileMode.Open)
            'Dim length As Long = stream.Length
            'Dim data(length) As Byte 'New Byte(length)
            'stream.Read(data, 0, Integer.Parse(length))

            ' opshet.LoadDocument(data, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

            Try

                Dim proc = Process.GetProcessesByName("excel")
                For ix As Integer = 0 To proc.Count - 1
                    proc(ix).Kill()
                Next ix

            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try


        Dim oExcel As New Microsoft.Office.Interop.Excel.Application

        Dim xlBookTmp As Microsoft.Office.Interop.Excel.Workbook
        Dim st As Microsoft.Office.Interop.Excel.Worksheet

        Try
            Dim _Qry As String = ""
            Dim _Dt As System.Data.DataTable
            Dim _oDtM As System.Data.DataTable

            _Qry = "  Select CASE WHEN ISDATE(FDInsDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDInsDate),103) ELSE '' END AS FDInsDate ,CASE WHEN ISDATE(FDUpdDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDUpdDate),103) ELSE '' END AS FDUpdDate ,FTCostSheetNo ,FNRevised ,CASE WHEN ISDATE(FDCostSheetDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDCostSheetDate),103) ELSE '' END AS FDCostSheetDate"
            _Qry &= vbCrLf & " ,FNNormalSizeAmt ,FNAboveSpecialSizeAmt ,FNLessThanSpecialSizeAmt ,FNNormalSizeAmt + FNAboveSpecialSizeAmt + FNLessThanSpecialSizeAmt AS SumTotal"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim _Sheet As Integer = 0

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString



            'Dim TmpFile As String = _Path & "\ExportExcel\CostSheetExcelRptCostSheet.xlsm"

            Dim pFileName As String = _Path & "\ExportExcel\CostSheetExcelRptCostSheet.xlsm"

            '  Dim pFileName As String = _Path & "\ExportExcel\CostSheetExcelRptCostSheet2.xlsx"



            ' _FileName = _Path & "\ExportExcel\SU23-HIT-DV6613-Y-ALL_SOLID-ALL_REG_SIZE-RB-8.xlsm"


            File.Copy(pFileName, _FileName, True)
            ' opshet.Document.LoadDocument(_FileName, DocumentFormat.Xlsm)


            'Dim oExcel As New Microsoft.Office.Interop.Excel.Application

            'Dim xlBookTmp As Microsoft.Office.Interop.Excel.Workbook
            'Dim st As Microsoft.Office.Interop.Excel.Worksheet

            Dim ExcellAddRow As Integer = 1
            Dim ExcellAdd As Integer = 1
            oExcel.Visible = False
            oExcel.DisplayAlerts = False

            'xlBookTmp = oExcel.Workbooks.Add
            'Dim oldSecurity
            'oldSecurity = oExcel.Application.AutomationSecurity
            'oExcel.Application.AutomationSecurity = Microsoft.Office.Core.MsoAutomationSecurity.msoAutomationSecurityLow


            xlBookTmp = oExcel.Workbooks.Open(_FileName)

            ' oExcel.Application.AutomationSecurity = oldSecurity


            st = xlBookTmp.Worksheets("STD-PAR CBD")

            '  oSheet = xlBookTmp.ActiveSheet
            'oSheet.Cells(1, 1)
            'For _CT As Integer = 1 To 4

            '_Sheet = _CT
            Dim i As Integer = 17

            With st



                .Cells(1 + ExcellAddRow, 2 + ExcellAdd).Value = (FNHSysSeasonId.Text)
                .Cells(2 + ExcellAddRow, 2 + ExcellAdd).Value = (FNHSysVenderPramId.Text)
                .Cells(3 + ExcellAddRow, 2 + ExcellAdd).Value = (FNHSysStyleId.Text)
                .Cells(4 + ExcellAddRow, 2 + ExcellAdd).Value = (FNHSysStyleId_None.Text)
                .Cells(5 + ExcellAddRow, 2 + ExcellAdd).Value = (FNISTeamMulti.Text)
                .Cells(6 + ExcellAddRow, 2 + ExcellAdd).Value = (FNCostSheetColor.Text)
                .Cells(7 + ExcellAddRow, 2 + ExcellAdd).Value = (FNCostSheetSize.Text)
                .Cells(8 + ExcellAddRow, 2 + ExcellAdd).Value = (FNCostSheetBuyType.Text)
                .Cells(9 + ExcellAddRow, 2 + ExcellAdd).Value = (FNVersion.Text)
                .Cells(10 + ExcellAddRow, 2 + ExcellAdd).Value = (FTLOProductDeveloper.Text)
                .Cells(11 + ExcellAddRow, 2 + ExcellAddRow).Value = (FNCostSheetQuotedType.Text)
                .Cells(12 + ExcellAddRow, 2 + ExcellAddRow).Value = (FTMSC.Text)

                Dim pDate As String = HI.UL.ULDate.ConvertEnDB(FTDateQuoted.Text)


                Dim pDate2 As String = ""

                If pDate <> "" Then
                    pDate2 = Integer.Parse(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(pDate, 7), 2)).ToString + "/" + Integer.Parse(Microsoft.VisualBasic.Right(pDate, 2)).ToString + "/" + Microsoft.VisualBasic.Left(pDate, 4)
                End If

                .Cells(13 + ExcellAddRow, 2 + ExcellAdd).Value = (pDate2)
                .Cells(14 + ExcellAddRow, 2 + ExcellAdd).Value = (FNCostSheetSampleRound.Text)
                .Cells(15 + ExcellAddRow, 2 + ExcellAdd).Value = (FNHSysStyleIdTo.Text)
                .Cells(16 + ExcellAddRow, 2 + ExcellAdd).Value = (FTQuotedLog.Text)
                .Cells(17 + ExcellAddRow, 2 + ExcellAdd).Value = (FTRemark.Text)

                'FOB
                '.Cells(4,5)=(IIf(FNTotalFabAmt.Value = 0, "-", FNTotalFabAmt.Text))
                '.Cells(5,5)=(IIf(FNTotalAccAmt.Value = 0, "-", FNTotalAccAmt.Text))
                '.Cells(6,5)=(IIf(FNChargeFabAmt.Value = 0, "-", FNChargeFabAmt.Text))
                '.Cells(7,5)=(IIf(FNChargeAccAmt.Value = 0, "-", FNChargeAccAmt.Text))
                '.Cells(8,5)=(IIf(FNProcessMatCost.Value = 0, "-", FNProcessMatCost.Text))
                '.Cells(9,5)=(IIf(FNProcessLaborCost.Value = 0, "-", FNProcessLaborCost.Text))
                '.Cells(10,5)=(IIf(FNPackagingAmt.Value = 0, "-", FNPackagingAmt.Text))

                If FNOtherCostAmt.Value > 0 Then
                    .Cells(11 + ExcellAddRow, 5 + ExcellAdd).Value = FNOtherCostAmt.Value
                End If


                ' .Cells(12,5)=(IIf(FNCMP.Value = 0, "-", Decimal.Parse(Format(Val(FNCMP.Value) / 100.0, "0.00"))))
                ' .Cells(13,5)=(Decimal.Parse(Format(Val(FNGrandTotal.Value) / 100.0, "0.00")))

                Try

                    If FNExtendedPer.Value > 0 Then
                        .Cells(14 + ExcellAddRow, 5 + ExcellAdd).Value = (Decimal.Parse(Format(Val(FNExtendedPer.Value) / 100.0, "0.00")))
                    End If



                Catch ex As Exception
                End Try

                ' .Cells(15,5)=(IIf(FNExtendedFOB.Value = 0, "-", Decimal.Parse(Format(Val(FNExtendedFOB.Value) / 100.0, "0.00"))))


                'Try

                '    .Cells(16 + ExcellAddRow, 5 + ExcellAdd).Value = (Decimal.Parse(Format(Val(FNTrinUsageAllowPer.Value) / 100.0, "0.00")))

                'Catch ex As Exception

                'End Try
                '.Cells(16,5)=(FNTrinUsageAllowPer.Text)


                'L4L
                '.Cells(4,6)=(IIf(FNL4LTotalFabric.Value = 0, "-", FNL4LTotalFabric.Text))
                '.Cells(5,6)=(IIf(FNL4LTotalTrim.Value = 0, "-", FNL4LTotalTrim.Text))
                '.Cells(6,6)=(IIf(FNL4LChargeFabric.Value = 0, "-", FNL4LChargeFabric.Text))
                '.Cells(7,6)=(IIf(FNL4LChargeTrim.Value = 0, "-", FNL4LChargeTrim.Text))
                '.Cells(8,6)=(IIf(FNL4LProMatCost.Value = 0, "-", FNL4LProMatCost.Text))
                '.Cells(9,6)=(IIf(FNL4LProLaborCost.Value = 0, "-", FNL4LProLaborCost.Text))
                '.Cells(10,6)=(IIf(FNL4LPackaging.Value = 0, "-", FNL4LPackaging.Text))
                If FNL4LOtherCost.Value > 0 Then
                    .Cells(11 + ExcellAddRow, 6 + ExcellAdd).Value = FNL4LOtherCost.Value
                End If


                ' .Cells(12,6)=(IIf(FNL4LCMP.Value = 0, "-", FNL4LCMP.Text))
                '.Cells(13,6)=(IIf(FNL4LFinalFOB.Value = 0, "-", FNL4LFinalFOB.Text))
                '.Cells(15,6)=(IIf(FNL4LExtendedFOB.Value = 0, "-", FNL4LExtendedFOB.Text))


                'FOB For L4L

                If FNL4Country1.Text.Trim <> "" Then
                    .Cells(2 + ExcellAddRow, 9 + ExcellAdd).Value = (FNL4Country1.Text)
                    '.Cells(3 + ExcellAddRow, 9 + ExcellAdd).Value = (FNL4Country1Cur.Text)

                    If FNL4Country1Exc.Value > 0 Then
                        .Cells(4 + ExcellAddRow, 9 + ExcellAdd).Value = FNL4Country1Exc.Value
                    End If

                    '.Cells(5,9)=(IIf(FNL4Country1Finalm.Value = 0, "-", FNL4Country1Finalm.Text))
                    '.Cells(6,9)=(IIf(FNL4Country1Extendedm.Value = 0, "-", FNL4Country1Extendedm.Text))
                End If


                If FNL4Country2.Text.Trim <> "" Then
                    .Cells(2 + ExcellAddRow, 10 + ExcellAdd).Value = (FNL4Country2.Text)
                    ' .Cells(3 + ExcellAddRow, 10 + ExcellAdd).Value = (FNL4Country2Cur.Text)

                    If FNL4Country2Exc.Value > 0 Then

                        .Cells(4 + ExcellAddRow, 10 + ExcellAdd).Value = FNL4Country2Exc.Value
                    End If


                    '.Cells(5,10)=(IIf(FNL4Country2Finalm.Value = 0, "-", FNL4Country2Finalm.Text))
                    '.Cells(6,10)=(IIf(FNL4Country2Extendedm.Value = 0, "-", FNL4Country2Extendedm.Text))
                End If


                If FNL4Country3.Text.Trim <> "" Then
                    .Cells(2 + ExcellAddRow, 11 + ExcellAdd).Value = (FNL4Country3.Text)
                    '  .Cells(3 + ExcellAddRow, 11 + ExcellAdd).Value = (FNL4Country3Cur.Text)

                    If FNL4Country3Exc.Value > 0 Then

                        .Cells(4 + ExcellAddRow, 11 + ExcellAdd).Value = FNL4Country3Exc.Value
                    End If

                    '.Cells(5,11)=(IIf(FNL4Country3Finalm.Value = 0, "-", FNL4Country3Finalm.Text))
                    '.Cells(6,11)=(IIf(FNL4Country3Extendedm.Value = 0, "-", FNL4Country3Extendedm.Text))
                End If


                _Qry = "  Select C.* ,   ISNULL(C.FTStyleCode,'') + '|' +   ISNULL(C.FTMainMatColorCode,'') + '|' +  ISNULL(C.FTTeamName,'')  AS FTTeam"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK)  "
                _Qry &= vbCrLf & " WHERE C.FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' "

                _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                Dim dttmp As DataTable

                Dim SRowFac As Integer = 28 + ExcellAddRow
                Dim SRowAcc As Integer = 53 + ExcellAddRow
                Dim SRowProMat As Integer = 78 + ExcellAddRow
                Dim SRowProLabor As Integer = 93 + ExcellAddRow
                Dim SRowPack As Integer = 109 + ExcellAddRow
                Dim SRowCmp As Integer = 122 + ExcellAddRow
                Dim SRowBemis As Integer = 128 + ExcellAddRow
                Dim SRowMulti As Integer = 8 + ExcellAddRow

                Dim RowIdx As Integer = 0
                Dim RowData As Integer = 1


                For Idx As Integer = 1 To 7

                    Try

                        dttmp = _Dt.Select("FNCostType=" & Idx & "", "FNSeq").CopyToDataTable

                        RowIdx = 0

                        Select Case Idx
                            Case 1

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowFac + RowIdx

                                    .Cells(RowData, 0 + ExcellAdd).Value = (R!FTMainMatCode.ToString)
                                    .Cells(RowData, 1 + ExcellAdd).Value = (R!FTMainMatColorCode.ToString)
                                    .Cells(RowData, 2 + ExcellAdd).Value = (R!FTMainMatName.ToString)
                                    .Cells(RowData, 3 + ExcellAdd).Value = (R!FTSuplCode.ToString)
                                    .Cells(RowData, 4 + ExcellAdd).Value = (R!TTLG.ToString)
                                    .Cells(RowData, 5 + ExcellAdd).Value = (R!FTUse.ToString)
                                    .Cells(RowData, 6 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNWeight.ToString), "0.00")))

                                    If Val(R!FNWidth.ToString) > 0 Then
                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        .Cells(RowData, 7 + ExcellAdd).Value = (pWith)
                                    End If

                                    .Cells(RowData, 8 + ExcellAdd).Value = (R!FTWidthUnit.ToString)

                                    If Val(R!FNMarkerEff.ToString) > 0 Then

                                        Dim mMark As Decimal = (Decimal.Parse(Format(Val(R!FNMarkerEff.ToString) / 100.0, "0.00000")))

                                        .Cells(RowData, 9 + ExcellAdd).Value = (mMark)


                                    End If

                                    .Cells(RowData, 10 + ExcellAdd).Value = (Format(Val(R!FNMarkerUsed.ToString), "0.0000"))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Cells(RowData, 11 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If


                                    ' .Cells(RowData,12)=(Format(Val(R!FNTotalUsed.ToString), "0.0000"))
                                    .Cells(RowData, 13 + ExcellAdd).Value = (R!FTRMDSSeason.ToString)
                                    .Cells(RowData, 14 + ExcellAdd).Value = (R!FNRMDSStatus.ToString)
                                    .Cells(RowData, 15 + ExcellAdd).Value = (R!FTUnitCode.ToString)
                                    .Cells(RowData, 16 + ExcellAdd).Value = (Format(Val(R!FNCostPerUOM.ToString), "0.000"))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Cells(RowData, 17 + ExcellAdd).Value = (Format(Val(R!FNCIF.ToString), "0.0000"))
                                    End If

                                    '  .Cells(RowData,18)=(Format(Val(R!FNUSAGECOST.ToString), "0.0000"))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 19 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Cells(RowData,20)=(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    End If



                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 21 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    RowIdx = RowIdx + 1

                                Next

                            Case 2

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowAcc + RowIdx

                                    .Cells(RowData, 0 + ExcellAdd).Value = (R!FTMainMatCode.ToString)
                                    .Cells(RowData, 1 + ExcellAdd).Value = (R!FTMainMatColorCode.ToString)
                                    .Cells(RowData, 2 + ExcellAdd).Value = (R!FTMainMatName.ToString)
                                    .Cells(RowData, 3 + ExcellAdd).Value = (R!FTSuplCode.ToString)
                                    .Cells(RowData, 4 + ExcellAdd).Value = (R!TTLG.ToString)
                                    .Cells(RowData, 5 + ExcellAdd).Value = (R!FTUse.ToString)


                                    If Val(R!FNWidth.ToString) > 0 Then
                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        .Cells(RowData, 6 + ExcellAdd).Value = (pWith)
                                    End If

                                    .Cells(RowData, 7 + ExcellAdd).Value = (R!FTWidthUnit.ToString)
                                    .Cells(RowData, 8 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Cells(RowData, 9 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If

                                    ' .Cells(RowData,10)=(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Cells(RowData, 11 + ExcellAdd).Value = (R!FTRMDSSeason.ToString)
                                    .Cells(RowData, 12 + ExcellAdd).Value = (R!FNRMDSStatus.ToString)
                                    .Cells(RowData, 13 + ExcellAdd).Value = (R!FTUnitCode.ToString)
                                    .Cells(RowData, 14 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Cells(RowData, 15 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Cells(RowData,16)=(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 17 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Cells(RowData,18)=(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    End If

                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 19 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    RowIdx = RowIdx + 1

                                Next

                            Case 3
                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProMat + RowIdx

                                    .Cells(RowData, 0 + ExcellAdd).Value = (R!FTMainMatCode.ToString)
                                    .Cells(RowData, 1 + ExcellAdd).Value = (R!FTPROCESSSUBTYPE.ToString)
                                    .Cells(RowData, 2 + ExcellAdd).Value = (R!FTMainMatName.ToString)
                                    .Cells(RowData, 3 + ExcellAdd).Value = (R!FTSuplCode.ToString)
                                    .Cells(RowData, 4 + ExcellAdd).Value = (R!TTLG.ToString)
                                    .Cells(RowData, 5 + ExcellAdd).Value = (R!FTUse.ToString)


                                    .Cells(RowData, 6 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Cells(RowData, 7 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If

                                    ' .Cells(RowData,8)=(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))

                                    .Cells(RowData, 9 + ExcellAdd).Value = (R!FTUnitCode.ToString)
                                    .Cells(RowData, 10 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000"))))
                                    ' .Cells(RowData,11)=((Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000"))))

                                    RowIdx = RowIdx + 1

                                Next

                            Case 4

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProLabor + RowIdx

                                    .Cells(RowData, 0 + ExcellAdd).Value = (R!FTMainMatCode.ToString)
                                    .Cells(RowData, 1 + ExcellAdd).Value = (R!FTPROCESSSUBTYPE.ToString)
                                    .Cells(RowData, 2 + ExcellAdd).Value = (R!FTMainMatName.ToString)
                                    .Cells(RowData, 3 + ExcellAdd).Value = (R!FTSuplCode.ToString)
                                    .Cells(RowData, 4 + ExcellAdd).Value = (R!TTLG.ToString)
                                    .Cells(RowData, 5 + ExcellAdd).Value = (R!FTUse.ToString)
                                    .Cells(RowData, 6 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Cells(RowData, 7 + ExcellAdd).Value = (R!FTUnitCode.ToString)
                                    .Cells(RowData, 8 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))
                                    ' .Cells(RowData,9)=(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    RowIdx = RowIdx + 1

                                Next

                            Case 5

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowPack + RowIdx

                                    .Cells(RowData, 0 + ExcellAdd).Value = (R!FTMainMatCode.ToString)
                                    .Cells(RowData, 1 + ExcellAdd).Value = (R!FTMainMatName.ToString)
                                    .Cells(RowData, 2 + ExcellAdd).Value = (R!FTSuplCode.ToString)
                                    .Cells(RowData, 3 + ExcellAdd).Value = (R!TTLG.ToString)
                                    .Cells(RowData, 4 + ExcellAdd).Value = (R!FTUse.ToString)

                                    If Val(R!FNWidth.ToString) > 0 Then
                                        '  .Cells(RowData,5)=(Format(Val(R!FNWidth.ToString), "0.00"))


                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        .Cells(RowData, 5 + ExcellAdd).Value = (pWith)

                                    End If

                                    .Cells(RowData, 6 + ExcellAdd).Value = (R!FTWidthUnit.ToString)
                                    .Cells(RowData, 7 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))


                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Cells(RowData, 8 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If


                                    ' .Cells(RowData,9)=(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Cells(RowData, 10 + ExcellAdd).Value = (R!FTRMDSSeason.ToString)
                                    .Cells(RowData, 11 + ExcellAdd).Value = (R!FNRMDSStatus.ToString)
                                    .Cells(RowData, 12 + ExcellAdd).Value = (R!FTUnitCode.ToString)
                                    .Cells(RowData, 13 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Cells(RowData, 14 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Cells(RowData,15)=(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 16 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Cells(RowData,17)=(Decimal.Parse(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000")))
                                    End If



                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 18 + ExcellAdd).Value = ((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    RowIdx = RowIdx + 1

                                Next

                            Case 6

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowCmp + RowIdx
                                    If Val(R!FNCostPerUOM.ToString) > 0 Then
                                        .Cells(RowData, 0 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.0000")))
                                    End If

                                    If Val(R!FNSTANDARDALLOWEDMINUTES.ToString) > 0 Then
                                        .Cells(RowData, 1 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNSTANDARDALLOWEDMINUTES.ToString), "0.00")))
                                    End If


                                    If Val(R!FNEFFICIENCYPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 2 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNEFFICIENCYPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    If Val(R!FNPROFITPERCENT.ToString) > 0 Then
                                        .Cells(RowData, 3 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNPROFITPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    If Val(R!FNCMPCOST.ToString) > 0 Then
                                        .Cells(RowData, 4 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNCMPCOST.ToString), "0.00")))
                                    End If

                                    .Cells(RowData, 5 + ExcellAdd).Value = (R!FTBMCCODE.ToString)

                                    RowIdx = RowIdx + 1

                                Next

                            Case 7

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowBemis + RowIdx

                                    .Cells(RowData, 0 + ExcellAdd).Value = (R!FTBEMISITEM.ToString)
                                    .Cells(RowData, 1 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNFULLWIDTH.ToString), "0.0000")))
                                    .Cells(RowData, 2 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNSLITTINGWIDTH.ToString), "0.0000")))
                                    .Cells(RowData, 3 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNREQUIREDLENGTH.ToString), "0.0000")))
                                    ' .Cells(RowData,4)=(Decimal.Parse(Format(Val(R!FNNETUSAGEINFULLWIDTH.ToString), "0.0000")))
                                    .Cells(RowData, 5 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNPRICEINMETER.ToString), "0.0000")))


                                    If Val(R!FNBEMISSLITTINGUPCHARGE.ToString) > 0 Then
                                        .Cells(RowData, 6 + ExcellAdd).Value = (Decimal.Parse(Format(Val(R!FNBEMISSLITTINGUPCHARGE.ToString) / 100.0, "0.00")))
                                    End If

                                    '  .Cells(RowData,7)=(Decimal.Parse(Format(Val(R!FNPRICEPERSLITTINGWITDH.ToString), "0.0000")))

                                    RowIdx = RowIdx + 1

                                Next

                        End Select


                    Catch ex As Exception
                        Dim msg As String = ex.Message
                    End Try

                Next

                If FNISTeamMulti.SelectedIndex = 1 Then

                    Try
                        dttmp = _Dt.Select("FNCostType=8", "FNSeq").CopyToDataTable

                        Dim grp As List(Of String) = (dttmp.Select("FTTeam<>''", "FTTeam").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTTeam")) _
                                                      .Distinct() _
                                                      .ToList()

                        RowIdx = 7 + ExcellAddRow
                        Dim ColIndex As Integer = 0
                        Dim SkipCol As Integer = 12
                        Dim WriteCol As Integer = 0

                        st = xlBookTmp.Worksheets("TEAMMULTI CBD")

                        With st

                            '  .Cells(2, 4 + ExcellAdd).Value = (FNTrinUsageAllowPer.Text)

                            For Each Rxm As String In grp
                                ColIndex = 0

                                For Each Rm1 As DataRow In dttmp.Select("FTTeam='" & HI.UL.ULF.rpQuoted(Rxm) & "'", "FNSeq")

                                    .Cells(RowIdx, 0 + ExcellAdd).Value = (FTMSC.Text)
                                    '.Cells(RowIdx, 1 + ExcellAdd).Value = (FNHSysSeasonId.Text)
                                    .Cells(RowIdx, 2 + ExcellAdd).Value = (Rm1!FTStyleCode.ToString)
                                    .Cells(RowIdx, 3 + ExcellAdd).Value = (Rm1!FTMainMatColorCode.ToString)
                                    .Cells(RowIdx, 4 + ExcellAdd).Value = (Rm1!FTTeamName.ToString)
                                    '.Cells(RowIdx, 5 + ExcellAdd).Value = (FNGrandTotal.Text)

                                    .Cells(RowIdx, 184 + ExcellAdd).Value = (Rm1!FTPRODUCTDEVELOPER.ToString)


                                    Exit For

                                Next

                                ColIndex = 6
                                WriteCol = ColIndex + ExcellAdd

                                For Each Rm1 As DataRow In dttmp.Select("FTTeam='" & HI.UL.ULF.rpQuoted(Rxm) & "'", "FNSeq")


                                    .Cells(RowIdx, WriteCol).Value = (Rm1!FTMainMatCode.ToString)
                                    .Cells(RowIdx, WriteCol + 1).Value = (Rm1!FTPROCESSSUBTYPE.ToString)
                                    .Cells(RowIdx, WriteCol + 2).Value = (Rm1!FTMainMatName.ToString)
                                    .Cells(RowIdx, WriteCol + 3).Value = (Rm1!FTSuplCode.ToString)

                                    If Val(Rm1!FNCostPerUOM.ToString) > 0 Then
                                        .Cells(RowIdx, WriteCol + 4).Value = (Decimal.Parse(Format(Val(Rm1!FNCostPerUOM.ToString), "0.0000")))
                                    End If

                                    If Val(Rm1!FNCIF.ToString) > 0 Then
                                        .Cells(RowIdx, WriteCol + 5).Value = (Decimal.Parse(Format(Val(Rm1!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Cells(RowIdx,ColIndex + 6)=(Format(Val(Rm1!FNUSAGECOST.ToString), "0.00"))

                                    If Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Cells(RowIdx, WriteCol + 7).Value = (Decimal.Parse(Format(Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    ' .Cells(RowIdx,ColIndex + 7)=(Format(Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString), "0.00"))
                                    ' .Cells(RowIdx,ColIndex + 8)=(Format(Val(Rm1!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    ' .Cells(RowIdx,ColIndex + 9)=(Format(Val(Rm1!FNHANDLINGCHARGECOST.ToString) + Val(Rm1!FNUSAGECOST.ToString), "0.0000"))

                                    If Val(Rm1!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Cells(RowIdx, WriteCol + 10).Value = (Decimal.Parse(Format(Val(Rm1!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    ' .Cells(RowIdx,ColIndex + 10)=(Format(Val(Rm1!FNIMPORTDUTYPERCENT.ToString), "0.00"))

                                    WriteCol = WriteCol + SkipCol

                                Next

                                RowIdx = RowIdx + 1

                            Next


                        End With
                        'With opshet.ActiveWorksheet

                        'End With
                        'With xlBookTmp.Worksheets(2)
                        '    .Cells(1,2)=(FNHSysVenderPramId.Text
                        '    .Cells(2,2)=(FNHSysStyleId.Text
                        '    .Cells(3,2)=(FNHSysStyleId_None.Text

                        '    .Cells(5,2)=(FNHSysSeasonId.Text
                        'End With


                    Catch ex As Exception
                        Dim msg As String = ex.Message
                    End Try

                End If



            End With


            'With xlBookTmp.Worksheets(2)
            '.Cells(1, 1).Value = "Test2"
            'End With
            oExcel.DisplayAlerts = False
            xlBookTmp.Save()
            oExcel.Quit()
            '
            oExcel = Nothing
            xlBookTmp = Nothing
            st = Nothing

            ' Make sure you release object references.

            _Spls.Close()


            Try

                Try
                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix
                Catch ex As Exception
                End Try

                'Dim stream As New FileStream(_FileName, FileMode.Open)
                'Dim length As Long = stream.Length
                'Dim data(length) As Byte 'New Byte(length)
                'stream.Read(data, 0, Integer.Parse(length))

                ' opshet.LoadDocument(data, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                Try

                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix

                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try

            HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

            Process.Start(_FileName)


        Catch ex As Exception

            Try
                oExcel.Quit()

                oExcel = Nothing
                xlBookTmp = Nothing
                ST = Nothing
            Catch ex2 As Exception

            End Try

            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(141115, "Writing Excel File Error.....", Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportExcel2(_Spls As HI.TL.SplashScreen)

        Dim xlBookTmp As excel.Workbook
        Dim xlBookBak As excel.Workbook

        Try
            Dim _Qry As String = ""
            Dim _Dt As System.Data.DataTable
            Dim _oDtM As System.Data.DataTable

            _Qry = "  Select CASE WHEN ISDATE(FDInsDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDInsDate),103) ELSE '' END AS FDInsDate ,CASE WHEN ISDATE(FDUpdDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDUpdDate),103) ELSE '' END AS FDUpdDate ,FTCostSheetNo ,FNRevised ,CASE WHEN ISDATE(FDCostSheetDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDCostSheetDate),103) ELSE '' END AS FDCostSheetDate"
            _Qry &= vbCrLf & " ,FNNormalSizeAmt ,FNAboveSpecialSizeAmt ,FNLessThanSpecialSizeAmt ,FNNormalSizeAmt + FNAboveSpecialSizeAmt + FNLessThanSpecialSizeAmt AS SumTotal"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim _Sheet As Integer = 0

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString



            Dim TmpFile As String = _Path & "\ExportExcel\CostSheetExcelRptCostSheet.xlsm"
            '   Dim BakFile As String = _Path & "\ExportExcel\CostSheetBlank.xlsm"

            Dim oExcel As New Microsoft.Office.Interop.Excel.Application

            Dim oSheet As excel.Worksheet
            Dim oRng As excel.Range

            oExcel.Visible = False
            oExcel.DisplayAlerts = False

            'xlBookTmp = oExcel.Workbooks.Add
            xlBookTmp = oExcel.Workbooks.Open(TmpFile)
            'xlBookBak = oExcel.Workbooks.Open(BakFile)

            oSheet = xlBookTmp.ActiveSheet
            'oSheet.Cells(1, 1)
            'For _CT As Integer = 1 To 4

            '_Sheet = _CT
            Dim i As Integer = 17

            With xlBookTmp.Worksheets(1)

                '  .Cells(z, _WriteColData)
                .Rows(1).Item(2).SetValue(FNHSysSeasonId.Text)
                .Rows(2).Item(2).SetValue(FNHSysVenderPramId.Text)
                .Rows(3).Item(2).SetValue(FNHSysStyleId.Text)
                .Rows(4).Item(2).SetValue(FNHSysStyleId_None.Text)
                .Rows(5).Item(2).SetValue(FNISTeamMulti.Text)
                .Rows(6).Item(2).SetValue(FNCostSheetColor.Text)
                .Rows(7).Item(2).SetValue(FNCostSheetSize.Text)
                .Rows(8).Item(2).SetValue(FNCostSheetBuyType.Text)
                .Rows(9).Item(2).SetValue(FNVersion.Text)
                .Rows(10).Item(2).SetValue(FTLOProductDeveloper.Text)
                .Rows(11).Item(2).SetValue(FNCostSheetQuotedType.Text)
                .Rows(12).Item(2).SetValue(FTMSC.Text)

                Dim pDate As String = HI.UL.ULDate.ConvertEnDB(FTDateQuoted.Text)


                Dim pDate2 As String = ""

                If pDate <> "" Then
                    pDate2 = Integer.Parse(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(pDate, 7), 2)).ToString + "/" + Integer.Parse(Microsoft.VisualBasic.Right(pDate, 2)).ToString + "/" + Microsoft.VisualBasic.Left(pDate, 4)
                End If

                .Rows(13).Item(2).SetValue(FTDateQuoted.Text)
                .Rows(14).Item(2).SetValue(FNCostSheetSampleRound.Text)
                .Rows(15).Item(2).SetValue(FNHSysStyleIdTo.Text)
                .Rows(16).Item(2).SetValue(FTQuotedLog.Text)
                .Rows(17).Item(2).SetValue(FTRemark.Text)

                'FOB
                '.Rows(4).Item(5).SetValue(IIf(FNTotalFabAmt.Value = 0, "-", FNTotalFabAmt.Text))
                '.Rows(5).Item(5).SetValue(IIf(FNTotalAccAmt.Value = 0, "-", FNTotalAccAmt.Text))
                '.Rows(6).Item(5).SetValue(IIf(FNChargeFabAmt.Value = 0, "-", FNChargeFabAmt.Text))
                '.Rows(7).Item(5).SetValue(IIf(FNChargeAccAmt.Value = 0, "-", FNChargeAccAmt.Text))
                '.Rows(8).Item(5).SetValue(IIf(FNProcessMatCost.Value = 0, "-", FNProcessMatCost.Text))
                '.Rows(9).Item(5).SetValue(IIf(FNProcessLaborCost.Value = 0, "-", FNProcessLaborCost.Text))
                '.Rows(10).Item(5).SetValue(IIf(FNPackagingAmt.Value = 0, "-", FNPackagingAmt.Text))
                .Rows(11).Item(5).SetValue(IIf(FNOtherCostAmt.Value = 0, 0, FNOtherCostAmt.Text))
                ' .Rows(12).Item(5).SetValue(IIf(FNCMP.Value = 0, "-", Decimal.Parse(Format(Val(FNCMP.Value) / 100.0, "0.00"))))
                ' .Rows(13).Item(5).SetValue(Decimal.Parse(Format(Val(FNGrandTotal.Value) / 100.0, "0.00")))

                Try

                    .Rows(14).Item(5).SetValue(Decimal.Parse(Format(Val(FNExtendedPer.Value) / 100.0, "0.00")))

                Catch ex As Exception
                End Try

                ' .Rows(15).Item(5).SetValue(IIf(FNExtendedFOB.Value = 0, "-", Decimal.Parse(Format(Val(FNExtendedFOB.Value) / 100.0, "0.00"))))


                Try

                    .Rows(16).Item(5).SetValue(Decimal.Parse(Format(Val(FNTrinUsageAllowPer.Value) / 100.0, "0.00")))

                Catch ex As Exception

                End Try
                '.Rows(16).Item(5).SetValue(FNTrinUsageAllowPer.Text)


                'L4L
                '.Rows(4).Item(6).SetValue(IIf(FNL4LTotalFabric.Value = 0, "-", FNL4LTotalFabric.Text))
                '.Rows(5).Item(6).SetValue(IIf(FNL4LTotalTrim.Value = 0, "-", FNL4LTotalTrim.Text))
                '.Rows(6).Item(6).SetValue(IIf(FNL4LChargeFabric.Value = 0, "-", FNL4LChargeFabric.Text))
                '.Rows(7).Item(6).SetValue(IIf(FNL4LChargeTrim.Value = 0, "-", FNL4LChargeTrim.Text))
                '.Rows(8).Item(6).SetValue(IIf(FNL4LProMatCost.Value = 0, "-", FNL4LProMatCost.Text))
                '.Rows(9).Item(6).SetValue(IIf(FNL4LProLaborCost.Value = 0, "-", FNL4LProLaborCost.Text))
                '.Rows(10).Item(6).SetValue(IIf(FNL4LPackaging.Value = 0, "-", FNL4LPackaging.Text))
                .Rows(11).Item(6).SetValue(IIf(FNL4LOtherCost.Value = 0, 0, FNL4LOtherCost.Text))
                ' .Rows(12).Item(6).SetValue(IIf(FNL4LCMP.Value = 0, "-", FNL4LCMP.Text))
                '.Rows(13).Item(6).SetValue(IIf(FNL4LFinalFOB.Value = 0, "-", FNL4LFinalFOB.Text))
                '.Rows(15).Item(6).SetValue(IIf(FNL4LExtendedFOB.Value = 0, "-", FNL4LExtendedFOB.Text))


                'FOB For L4L
                .Rows(2).Item(9).SetValue(FNL4Country1.Text)
                .Rows(3).Item(9).SetValue(FNL4Country1Cur.Text)
                .Rows(4).Item(9).SetValue(IIf(FNL4Country1Exc.Value = 0, 0, FNL4Country1Exc.Text))
                '.Rows(5).Item(9).SetValue(IIf(FNL4Country1Finalm.Value = 0, "-", FNL4Country1Finalm.Text))
                '.Rows(6).Item(9).SetValue(IIf(FNL4Country1Extendedm.Value = 0, "-", FNL4Country1Extendedm.Text))

                .Rows(2).Item(10).SetValue(FNL4Country2.Text)
                .Rows(3).Item(10).SetValue(FNL4Country2Cur.Text)
                .Rows(4).Item(10).SetValue(IIf(FNL4Country2Exc.Value = 0, 0, FNL4Country2Exc.Text))
                '.Rows(5).Item(10).SetValue(IIf(FNL4Country2Finalm.Value = 0, "-", FNL4Country2Finalm.Text))
                '.Rows(6).Item(10).SetValue(IIf(FNL4Country2Extendedm.Value = 0, "-", FNL4Country2Extendedm.Text))

                .Rows(2).Item(11).SetValue(FNL4Country3.Text)
                .Rows(3).Item(11).SetValue(FNL4Country3Cur.Text)
                .Rows(4).Item(11).SetValue(IIf(FNL4Country3Exc.Value = 0, 0, FNL4Country3Exc.Text))
                '.Rows(5).Item(11).SetValue(IIf(FNL4Country3Finalm.Value = 0, "-", FNL4Country3Finalm.Text))
                '.Rows(6).Item(11).SetValue(IIf(FNL4Country3Extendedm.Value = 0, "-", FNL4Country3Extendedm.Text))


                _Qry = "  Select C.* , ISNULL(C.FTMainMatColorCode,'') + '|' +  ISNULL(C.FTTeamName,'')  AS FTTeam"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK)  "
                _Qry &= vbCrLf & " WHERE C.FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' "

                _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                Dim dttmp As DataTable

                Dim SRowFac As Integer = 28
                Dim SRowAcc As Integer = 53
                Dim SRowProMat As Integer = 78
                Dim SRowProLabor As Integer = 93
                Dim SRowPack As Integer = 109
                Dim SRowCmp As Integer = 122
                Dim SRowBemis As Integer = 128
                Dim SRowMulti As Integer = 8

                Dim RowIdx As Integer = 0
                Dim RowData As Integer = 0

                For Idx As Integer = 1 To 7

                    Try

                        dttmp = _Dt.Select("FNCostType=" & Idx & "", "FNSeq").CopyToDataTable

                        RowIdx = 0

                        Select Case Idx
                            Case 1

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowFac + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTMainMatColorCode.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)
                                    .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNWeight.ToString), "0.00")))

                                    If Val(R!FNWidth.ToString) > 0 Then
                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        .Rows(RowData).Item(7).SetValue(pWith)
                                    End If

                                    .Rows(RowData).Item(8).SetValue(R!FTWidthUnit.ToString)

                                    If Val(R!FNMarkerEff.ToString) > 0 Then

                                        Dim mMark As Decimal = (Decimal.Parse(Format(Val(R!FNMarkerEff.ToString) / 100.0, "0.00000")))

                                        .Rows(RowData).Item(9).SetValue(mMark)


                                    End If

                                    .Rows(RowData).Item(10).SetValue(Format(Val(R!FNMarkerUsed.ToString), "0.0000"))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(11).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If


                                    ' .Rows(RowData).Item(12).SetValue(Format(Val(R!FNTotalUsed.ToString), "0.0000"))
                                    .Rows(RowData).Item(13).SetValue(R!FTRMDSSeason.ToString)
                                    .Rows(RowData).Item(14).SetValue(R!FNRMDSStatus.ToString)
                                    .Rows(RowData).Item(15).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(16).SetValue(Format(Val(R!FNCostPerUOM.ToString), "0.000"))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Rows(RowData).Item(17).SetValue(Format(Val(R!FNCIF.ToString), "0.0000"))
                                    End If

                                    '  .Rows(RowData).Item(18).SetValue(Format(Val(R!FNUSAGECOST.ToString), "0.0000"))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(19).SetValue((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Rows(RowData).Item(20).SetValue(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    End If



                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(21).SetValue((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    RowIdx = RowIdx + 1

                                Next

                            Case 2

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowAcc + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTMainMatColorCode.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)


                                    If Val(R!FNWidth.ToString) > 0 Then
                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        .Rows(RowData).Item(6).SetValue(pWith)
                                    End If

                                    .Rows(RowData).Item(7).SetValue(R!FTWidthUnit.ToString)
                                    .Rows(RowData).Item(8).SetValue(Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(9).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If

                                    ' .Rows(RowData).Item(10).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Rows(RowData).Item(11).SetValue(R!FTRMDSSeason.ToString)
                                    .Rows(RowData).Item(12).SetValue(R!FNRMDSStatus.ToString)
                                    .Rows(RowData).Item(13).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(14).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Rows(RowData).Item(15).SetValue(Decimal.Parse(Format(Val(R!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Rows(RowData).Item(16).SetValue(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(17).SetValue((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Rows(RowData).Item(18).SetValue(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    End If

                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(19).SetValue((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    RowIdx = RowIdx + 1

                                Next

                            Case 3
                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProMat + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTPROCESSSUBTYPE.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)


                                    .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(7).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If

                                    ' .Rows(RowData).Item(8).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))

                                    .Rows(RowData).Item(9).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(10).SetValue((Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000"))))
                                    ' .Rows(RowData).Item(11).SetValue((Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000"))))

                                    RowIdx = RowIdx + 1

                                Next

                            Case 4

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProLabor + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTPROCESSSUBTYPE.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)
                                    .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Rows(RowData).Item(7).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(8).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))
                                    ' .Rows(RowData).Item(9).SetValue(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    RowIdx = RowIdx + 1

                                Next

                            Case 5

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowPack + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!FTUse.ToString)

                                    If Val(R!FNWidth.ToString) > 0 Then
                                        '  .Rows(RowData).Item(5).SetValue(Format(Val(R!FNWidth.ToString), "0.00"))


                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        .Rows(RowData).Item(5).SetValue(pWith)

                                    End If

                                    .Rows(RowData).Item(6).SetValue(R!FTWidthUnit.ToString)
                                    .Rows(RowData).Item(7).SetValue(Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))


                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(8).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If


                                    ' .Rows(RowData).Item(9).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Rows(RowData).Item(10).SetValue(R!FTRMDSSeason.ToString)
                                    .Rows(RowData).Item(11).SetValue(R!FNRMDSStatus.ToString)
                                    .Rows(RowData).Item(12).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(13).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Rows(RowData).Item(14).SetValue(Decimal.Parse(Format(Val(R!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Rows(RowData).Item(15).SetValue(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(16).SetValue((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Rows(RowData).Item(17).SetValue(Decimal.Parse(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000")))
                                    End If



                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(18).SetValue((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    RowIdx = RowIdx + 1

                                Next

                            Case 6

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowCmp + RowIdx
                                    If Val(R!FNCostPerUOM.ToString) > 0 Then
                                        .Rows(RowData).Item(0).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.0000")))
                                    End If

                                    If Val(R!FNSTANDARDALLOWEDMINUTES.ToString) > 0 Then
                                        .Rows(RowData).Item(1).SetValue(Decimal.Parse(Format(Val(R!FNSTANDARDALLOWEDMINUTES.ToString), "0.00")))
                                    End If


                                    If Val(R!FNEFFICIENCYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(2).SetValue(Decimal.Parse(Format(Val(R!FNEFFICIENCYPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    If Val(R!FNPROFITPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(3).SetValue(Decimal.Parse(Format(Val(R!FNPROFITPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    If Val(R!FNCMPCOST.ToString) > 0 Then
                                        .Rows(RowData).Item(4).SetValue(Decimal.Parse(Format(Val(R!FNCMPCOST.ToString), "0.00")))
                                    End If

                                    .Rows(RowData).Item(5).SetValue(R!FTBMCCODE.ToString)

                                    RowIdx = RowIdx + 1

                                Next

                            Case 7

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowBemis + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTBEMISITEM.ToString)
                                    .Rows(RowData).Item(1).SetValue(Decimal.Parse(Format(Val(R!FNFULLWIDTH.ToString), "0.0000")))
                                    .Rows(RowData).Item(2).SetValue(Decimal.Parse(Format(Val(R!FNSLITTINGWIDTH.ToString), "0.0000")))
                                    .Rows(RowData).Item(3).SetValue(Decimal.Parse(Format(Val(R!FNREQUIREDLENGTH.ToString), "0.0000")))
                                    ' .Rows(RowData).Item(4).SetValue(Decimal.Parse(Format(Val(R!FNNETUSAGEINFULLWIDTH.ToString), "0.0000")))
                                    .Rows(RowData).Item(5).SetValue(Decimal.Parse(Format(Val(R!FNPRICEINMETER.ToString), "0.0000")))


                                    If Val(R!FNBEMISSLITTINGUPCHARGE.ToString) > 0 Then
                                        .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNBEMISSLITTINGUPCHARGE.ToString) / 100.0, "0.00")))
                                    End If

                                    '  .Rows(RowData).Item(7).SetValue(Decimal.Parse(Format(Val(R!FNPRICEPERSLITTINGWITDH.ToString), "0.0000")))

                                    RowIdx = RowIdx + 1

                                Next

                        End Select


                    Catch ex As Exception
                    End Try

                Next

                If FNISTeamMulti.SelectedIndex = 1 Then

                    Try
                        dttmp = _Dt.Select("FNCostType=8", "FNSeq").CopyToDataTable

                        Dim grp As List(Of String) = (dttmp.Select("FTTeam<>''", "FTTeam").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTTeam")) _
                                                      .Distinct() _
                                                      .ToList()

                        RowIdx = 7
                        Dim ColIndex As Integer = 0
                        Dim SkipCol As Integer = 11




                        With xlBookTmp.Worksheets("TEAMMULTI CBD")

                            ' .Rows(2).Item(4).SetValue(FNTrinUsageAllowPer.Text)

                            For Each Rxm As String In grp
                                ColIndex = 0

                                For Each Rm1 As DataRow In dttmp.Select("FTTeam='" & HI.UL.ULF.rpQuoted(Rxm) & "'", "FNSeq")

                                    .Rows(RowIdx).Item(0).SetValue(FTMSC.Text)
                                    .Rows(RowIdx).Item(1).SetValue(FNHSysSeasonId.Text)
                                    .Rows(RowIdx).Item(2).SetValue(FNHSysStyleId.Text)
                                    .Rows(RowIdx).Item(3).SetValue(Rm1!FTMainMatColorCode.ToString)
                                    .Rows(RowIdx).Item(4).SetValue(Rm1!FTTeamName.ToString)
                                    .Rows(RowIdx).Item(5).SetValue(FNGrandTotal.Text)

                                    .Rows(RowIdx).Item(184).SetValue(Rm1!FTPRODUCTDEVELOPER.ToString)


                                    Exit For

                                Next

                                ColIndex = 8
                                For Each Rm1 As DataRow In dttmp.Select("FTTeam='" & HI.UL.ULF.rpQuoted(Rxm) & "'", "FNSeq")

                                    .Rows(RowIdx).Item(ColIndex).SetValue(Rm1!FTMainMatCode.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 1).SetValue(Rm1!FTPROCESSSUBTYPE.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 2).SetValue(Rm1!FTMainMatName.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 3).SetValue(Rm1!FTSuplCode.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 4).SetValue(Decimal.Parse(Format(Val(Rm1!FNCostPerUOM.ToString), "0.0000")))
                                    .Rows(RowIdx).Item(ColIndex + 5).SetValue(Decimal.Parse(Format(Val(Rm1!FNCIF.ToString), "0.0000")))
                                    ' .Rows(RowIdx).Item(ColIndex + 6).SetValue(Format(Val(Rm1!FNUSAGECOST.ToString), "0.00"))



                                    If Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowIdx).Item(ColIndex + 7).SetValue(Decimal.Parse(Format(Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00")))
                                    End If


                                    ' .Rows(RowIdx).Item(ColIndex + 7).SetValue(Format(Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString), "0.00"))
                                    ' .Rows(RowIdx).Item(ColIndex + 8).SetValue(Format(Val(Rm1!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    ' .Rows(RowIdx).Item(ColIndex + 9).SetValue(Format(Val(Rm1!FNHANDLINGCHARGECOST.ToString) + Val(Rm1!FNUSAGECOST.ToString), "0.0000"))

                                    If Val(Rm1!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowIdx).Item(ColIndex + 10).SetValue(Decimal.Parse(Format(Val(Rm1!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    ' .Rows(RowIdx).Item(ColIndex + 10).SetValue(Format(Val(Rm1!FNIMPORTDUTYPERCENT.ToString), "0.00"))


                                    ColIndex = ColIndex + SkipCol
                                Next

                                RowIdx = RowIdx + 1

                            Next


                        End With
                        'With opshet.ActiveWorksheet

                        'End With
                        'With xlBookTmp.Worksheets(2)
                        '    .Rows(1).Item(2).SetValue(FNHSysVenderPramId.Text
                        '    .Rows(2).Item(2).SetValue(FNHSysStyleId.Text
                        '    .Rows(3).Item(2).SetValue(FNHSysStyleId_None.Text

                        '    .Rows(5).Item(2).SetValue(FNHSysSeasonId.Text
                        'End With


                    Catch ex As Exception
                    End Try

                End If



            End With


            'With xlBookTmp.Worksheets(2)
            '.Cells(1, 1).Value = "Test2"
            'End With
            oExcel.DisplayAlerts = False
            xlBookTmp.SaveAs(_FileName)

            'xlBookTmp.Close()
            '_Spls.Close()

            ' oExcel.Visible = True
            'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)


            'oExcel.DisplayAlerts = False
            'xlBookTmp.Save()
            xlBookTmp.Close()
            _Spls.Close()


            Try

                Try
                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix
                Catch ex As Exception
                End Try

                Dim stream As New FileStream(_FileName, FileMode.Open)
                Dim length As Long = stream.Length
                Dim data(length) As Byte 'New Byte(length)
                stream.Read(data, 0, Integer.Parse(length))

                opshet.LoadDocument(data, DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                Try

                    Dim proc = Process.GetProcessesByName("excel")
                    For ix As Integer = 0 To proc.Count - 1
                        proc(ix).Kill()
                    Next ix

                Catch ex As Exception
                End Try
            Catch ex As Exception
            End Try

            HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

            Process.Start(_FileName)


        Catch ex As Exception

            Try
                xlBookTmp.Close()
            Catch ex2 As Exception

            End Try

            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(141115, "Writing Excel File Error.....", Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ExportExcelSprettSheet(_Spls As HI.TL.SplashScreen)

        Dim xlBookTmp As excel.Workbook
        Dim xlBookBak As excel.Workbook

        Try
            Dim _Qry As String = ""
            Dim _Dt As System.Data.DataTable
            Dim _oDtM As System.Data.DataTable

            _Qry = "  Select CASE WHEN ISDATE(FDInsDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDInsDate),103) ELSE '' END AS FDInsDate ,CASE WHEN ISDATE(FDUpdDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDUpdDate),103) ELSE '' END AS FDUpdDate ,FTCostSheetNo ,FNRevised ,CASE WHEN ISDATE(FDCostSheetDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDCostSheetDate),103) ELSE '' END AS FDCostSheetDate"
            _Qry &= vbCrLf & " ,FNNormalSizeAmt ,FNAboveSpecialSizeAmt ,FNLessThanSpecialSizeAmt ,FNNormalSizeAmt + FNAboveSpecialSizeAmt + FNLessThanSpecialSizeAmt AS SumTotal"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim _Sheet As Integer = 0

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString


            Dim pFileName As String = _Path & "\ExportExcel\CostSheetExcelRptCostSheet.xlsm"


            opshet.Document.LoadDocument(File.ReadAllBytes(pFileName), DocumentFormat.Xlsm)

            opshet.BeginUpdate()
            With opshet.ActiveWorksheet

                .Rows(1).Item(2).Value = FNHSysSeasonId.Text
                .Rows(2).Item(2).Value = FNHSysVenderPramId.Text
                .Rows(3).Item(2).Value = FNHSysStyleId.Text
                .Rows(4).Item(2).Value = FNHSysStyleId_None.Text
                .Rows(5).Item(2).Value = FNISTeamMulti.Text
                .Rows(6).Item(2).Value = FNCostSheetColor.Text
                .Rows(7).Item(2).Value = FNCostSheetSize.Text
                .Rows(8).Item(2).Value = FNCostSheetBuyType.Text
                .Rows(9).Item(2).Value = FNVersion.Text
                .Rows(10).Item(2).Value = FTLOProductDeveloper.Text
                .Rows(11).Item(2).Value = FNCostSheetQuotedType.Text
                .Rows(12).Item(2).Value = FTMSC.Text
                .Rows(13).Item(2).Value = FTDateQuoted.Text
                .Rows(14).Item(2).Value = FNCostSheetSampleRound.Text
                .Rows(15).Item(2).Value = FNHSysStyleIdTo.Text
                .Rows(16).Item(2).Value = FTQuotedLog.Text
                .Rows(17).Item(2).Value = FTRemark.Text


                'FOB
                .Rows(4).Item(5).Value = FNTotalFabAmt.Text
                .Rows(5).Item(5).Value = FNTotalAccAmt.Text
                .Rows(6).Item(5).Value = FNChargeFabAmt.Text
                .Rows(7).Item(5).Value = FNChargeAccAmt.Text
                .Rows(8).Item(5).Value = FNProcessMatCost.Text
                .Rows(9).Item(5).Value = FNProcessLaborCost.Text
                .Rows(10).Item(5).Value = FNPackagingAmt.Text
                .Rows(11).Item(5).Value = FNOtherCostAmt.Text
                .Rows(12).Item(5).Value = FNCMP.Text
                .Rows(13).Item(5).Value = FNGrandTotal.Text
                .Rows(14).Item(5).Value = FNExtendedPer.Text
                .Rows(15).Item(5).Value = FNExtendedFOB.Text
                .Rows(16).Item(5).Value = FNTrinUsageAllowPer.Text


                'L4L
                .Rows(4).Item(6).Value = FNL4LTotalFabric.Text
                .Rows(5).Item(6).Value = FNL4LTotalTrim.Text
                .Rows(6).Item(6).Value = FNL4LChargeFabric.Text
                .Rows(7).Item(6).Value = FNL4LChargeTrim.Text
                .Rows(8).Item(6).Value = FNL4LProMatCost.Text
                .Rows(9).Item(6).Value = FNL4LProLaborCost.Text
                .Rows(10).Item(6).Value = FNL4LPackaging.Text
                .Rows(11).Item(6).Value = FNL4LOtherCost.Text
                .Rows(12).Item(6).Value = FNL4LCMP.Text
                .Rows(13).Item(6).Value = FNL4LFinalFOB.Text
                .Rows(15).Item(6).Value = FNL4LExtendedFOB.Text


                'FOB For L4L
                .Rows(2).Item(9).Value = FNL4Country1.Text
                .Rows(3).Item(9).Value = FNL4Country1Cur.Text
                .Rows(4).Item(6).Value = FNL4Country1Exc.Text
                .Rows(5).Item(9).Value = FNL4Country1Finalm.Text
                .Rows(6).Item(9).Value = FNL4Country1Extendedm.Text

                .Rows(2).Item(10).Value = FNL4Country2.Text
                .Rows(3).Item(10).Value = FNL4Country2Cur.Text
                .Rows(4).Item(10).Value = FNL4Country2Exc.Text
                .Rows(5).Item(10).Value = FNL4Country2Finalm.Text
                .Rows(6).Item(10).Value = FNL4Country2Extendedm.Text

                .Rows(2).Item(11).Value = FNL4Country3.Text
                .Rows(3).Item(11).Value = FNL4Country3Cur.Text
                .Rows(4).Item(11).Value = FNL4Country3Exc.Text
                .Rows(5).Item(11).Value = FNL4Country3Finalm.Text
                .Rows(6).Item(11).Value = FNL4Country3Extendedm.Text


                _Qry = "  Select C.* "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK)  "
                _Qry &= vbCrLf & " WHERE C.FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' "

                _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                Dim dttmp As DataTable

                Dim SRowFac As Integer = 28
                Dim SRowAcc As Integer = 53
                Dim SRowProMat As Integer = 78
                Dim SRowProLabor As Integer = 93
                Dim SRowPack As Integer = 109
                Dim SRowCmp As Integer = 122
                Dim SRowBemis As Integer = 128
                Dim SRowMulti As Integer = 8

                Dim RowIdx As Integer = 0
                Dim RowData As Integer = 0

                For Idx As Integer = 1 To 7

                    Try

                        dttmp = _Dt.Select("FNCostType=" & Idx & "", "FNSeq").CopyToDataTable

                        RowIdx = 0

                        Select Case Idx
                            Case 1

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowFac + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FTMainMatCode.ToString
                                    .Rows(RowData).Item(1).Value = R!FTMainMatColorCode.ToString
                                    .Rows(RowData).Item(2).Value = R!FTMainMatName.ToString
                                    .Rows(RowData).Item(3).Value = R!FTSuplCode.ToString
                                    .Rows(RowData).Item(4).Value = R!TTLG.ToString
                                    .Rows(RowData).Item(5).Value = R!FTUse.ToString
                                    .Rows(RowData).Item(6).Value = R!FNWeight.ToString
                                    .Rows(RowData).Item(7).Value = R!FNWidth.ToString
                                    .Rows(RowData).Item(8).Value = R!FTWidthUnit.ToString
                                    .Rows(RowData).Item(9).Value = R!FNMarkerEff.ToString
                                    .Rows(RowData).Item(10).Value = R!FNMarkerUsed.ToString
                                    .Rows(RowData).Item(11).Value = R!FNAllowancePer.ToString
                                    .Rows(RowData).Item(12).Value = R!FNTotalUsed.ToString
                                    .Rows(RowData).Item(13).Value = R!FTRMDSSeason.ToString
                                    .Rows(RowData).Item(14).Value = R!FNRMDSStatus.ToString
                                    .Rows(RowData).Item(15).Value = R!FTUnitCode.ToString
                                    .Rows(RowData).Item(16).Value = R!FNCostPerUOM.ToString
                                    .Rows(RowData).Item(17).Value = R!FNCIF.ToString
                                    .Rows(RowData).Item(18).Value = R!FNUSAGECOST.ToString
                                    .Rows(RowData).Item(19).Value = R!FNHANDLINGCHARGEPERCENT.ToString
                                    .Rows(RowData).Item(20).Value = R!FNHANDLINGCHARGECOST.ToString
                                    .Rows(RowData).Item(21).Value = R!FNIMPORTDUTYPERCENT.ToString


                                    RowIdx = RowIdx + 1

                                Next

                            Case 2

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowAcc + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FTMainMatCode.ToString
                                    .Rows(RowData).Item(1).Value = R!FTMainMatColorCode.ToString
                                    .Rows(RowData).Item(2).Value = R!FTMainMatName.ToString
                                    .Rows(RowData).Item(3).Value = R!FTSuplCode.ToString
                                    .Rows(RowData).Item(4).Value = R!TTLG.ToString
                                    .Rows(RowData).Item(5).Value = R!FTUse.ToString
                                    .Rows(RowData).Item(6).Value = R!FNWidth.ToString
                                    .Rows(RowData).Item(7).Value = R!FTWidthUnit.ToString
                                    .Rows(RowData).Item(8).Value = R!FNMarkerUsed.ToString
                                    .Rows(RowData).Item(9).Value = R!FNAllowancePer.ToString
                                    .Rows(RowData).Item(10).Value = R!FNTotalUsed.ToString
                                    .Rows(RowData).Item(11).Value = R!FTRMDSSeason.ToString
                                    .Rows(RowData).Item(12).Value = R!FNRMDSStatus.ToString
                                    .Rows(RowData).Item(13).Value = R!FTUnitCode.ToString
                                    .Rows(RowData).Item(14).Value = R!FNCostPerUOM.ToString
                                    .Rows(RowData).Item(15).Value = R!FNCIF.ToString
                                    .Rows(RowData).Item(16).Value = R!FNUSAGECOST.ToString
                                    .Rows(RowData).Item(17).Value = R!FNHANDLINGCHARGEPERCENT.ToString
                                    .Rows(RowData).Item(18).Value = R!FNHANDLINGCHARGECOST.ToString
                                    .Rows(RowData).Item(19).Value = R!FNIMPORTDUTYPERCENT.ToString


                                    RowIdx = RowIdx + 1

                                Next

                            Case 3
                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProMat + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FTMainMatCode.ToString
                                    .Rows(RowData).Item(1).Value = R!FTPROCESSSUBTYPE.ToString
                                    .Rows(RowData).Item(2).Value = R!FTMainMatName.ToString
                                    .Rows(RowData).Item(3).Value = R!FTSuplCode.ToString
                                    .Rows(RowData).Item(4).Value = R!TTLG.ToString
                                    .Rows(RowData).Item(5).Value = R!FTUse.ToString


                                    .Rows(RowData).Item(6).Value = R!FNMarkerUsed.ToString
                                    .Rows(RowData).Item(7).Value = R!FNAllowancePer.ToString
                                    .Rows(RowData).Item(8).Value = R!FNTotalUsed.ToString

                                    .Rows(RowData).Item(9).Value = R!FTUnitCode.ToString
                                    .Rows(RowData).Item(10).Value = R!FNCostPerUOM.ToString
                                    .Rows(RowData).Item(11).Value = R!FNUSAGECOST.ToString

                                    RowIdx = RowIdx + 1

                                Next


                            Case 4
                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProLabor + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FTMainMatCode.ToString
                                    .Rows(RowData).Item(1).Value = R!FTPROCESSSUBTYPE.ToString
                                    .Rows(RowData).Item(2).Value = R!FTMainMatName.ToString
                                    .Rows(RowData).Item(3).Value = R!FTSuplCode.ToString
                                    .Rows(RowData).Item(4).Value = R!TTLG.ToString
                                    .Rows(RowData).Item(5).Value = R!FTUse.ToString
                                    .Rows(RowData).Item(6).Value = R!FNTotalUsed.ToString
                                    .Rows(RowData).Item(7).Value = R!FTUnitCode.ToString
                                    .Rows(RowData).Item(8).Value = R!FNCostPerUOM.ToString
                                    .Rows(RowData).Item(9).Value = R!FNUSAGECOST.ToString

                                    RowIdx = RowIdx + 1

                                Next

                            Case 5

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowPack + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FTMainMatCode.ToString
                                    .Rows(RowData).Item(1).Value = R!FTMainMatName.ToString
                                    .Rows(RowData).Item(2).Value = R!FTSuplCode.ToString
                                    .Rows(RowData).Item(3).Value = R!TTLG.ToString
                                    .Rows(RowData).Item(4).Value = R!FTUse.ToString
                                    .Rows(RowData).Item(5).Value = R!FNWidth.ToString
                                    .Rows(RowData).Item(6).Value = R!FTWidthUnit.ToString
                                    .Rows(RowData).Item(7).Value = R!FNMarkerUsed.ToString
                                    .Rows(RowData).Item(8).Value = R!FNAllowancePer.ToString
                                    .Rows(RowData).Item(9).Value = R!FNTotalUsed.ToString
                                    .Rows(RowData).Item(10).Value = R!FTRMDSSeason.ToString
                                    .Rows(RowData).Item(11).Value = R!FNRMDSStatus.ToString
                                    .Rows(RowData).Item(12).Value = R!FTUnitCode.ToString
                                    .Rows(RowData).Item(13).Value = R!FNCostPerUOM.ToString
                                    .Rows(RowData).Item(14).Value = R!FNCIF.ToString
                                    .Rows(RowData).Item(15).Value = R!FNUSAGECOST.ToString
                                    .Rows(RowData).Item(16).Value = R!FNHANDLINGCHARGEPERCENT.ToString
                                    .Rows(RowData).Item(17).Value = R!FNHANDLINGCHARGECOST.ToString
                                    .Rows(RowData).Item(18).Value = R!FNIMPORTDUTYPERCENT.ToString

                                    RowIdx = RowIdx + 1

                                Next

                            Case 6

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowCmp + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FNCostPerUOM.ToString
                                    .Rows(RowData).Item(1).Value = R!FNSTANDARDALLOWEDMINUTES.ToString
                                    .Rows(RowData).Item(2).Value = R!FNEFFICIENCYPERCENT.ToString
                                    .Rows(RowData).Item(3).Value = R!FNPROFITPERCENT.ToString
                                    .Rows(RowData).Item(4).Value = R!FNCMPCOST.ToString
                                    .Rows(RowData).Item(5).Value = R!FTBMCCODE.ToString

                                    RowIdx = RowIdx + 1

                                Next

                            Case 7

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowBemis + RowIdx

                                    .Rows(RowData).Item(0).Value = R!FTBEMISITEM.ToString
                                    .Rows(RowData).Item(1).Value = R!FNFULLWIDTH.ToString
                                    .Rows(RowData).Item(2).Value = R!FNSLITTINGWIDTH.ToString
                                    .Rows(RowData).Item(3).Value = R!FNREQUIREDLENGTH.ToString
                                    .Rows(RowData).Item(4).Value = R!FNNETUSAGEINFULLWIDTH.ToString
                                    .Rows(RowData).Item(5).Value = R!FNPRICEINMETER.ToString
                                    .Rows(RowData).Item(6).Value = R!FNBEMISSLITTINGUPCHARGE.ToString
                                    .Rows(RowData).Item(7).Value = R!FNPRICEPERSLITTINGWITDH.ToString

                                    RowIdx = RowIdx + 1

                                Next

                        End Select


                    Catch ex As Exception
                    End Try

                Next

                If FNISTeamMulti.SelectedIndex = 1 Then

                    Try

                        dttmp = _Dt.Select("FNCostType=8", "FNSeq").CopyToDataTable
                        With xlBookTmp.Worksheets(2)
                            .Rows(1).Item(2).Value = FNHSysVenderPramId.Text
                            .Rows(2).Item(2).Value = FNHSysStyleId.Text
                            .Rows(3).Item(2).Value = FNHSysStyleId_None.Text

                            .Rows(5).Item(2).setvalue = FNHSysSeasonId.Text
                        End With


                    Catch ex As Exception
                    End Try

                End If



            End With
            opshet.EndUpdate()
            opshet.SaveDocumentAs(_FileName)

            'xlBookTmp.Close()
            _Spls.Close()
            'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Process.Start(_FileName)
        Catch ex As Exception

            Try
                xlBookTmp.Close()
            Catch ex2 As Exception

            End Try

            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(141115, "Writing Excel File Error.....", Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ExportExcelSprettSheet2(_Spls As HI.TL.SplashScreen, Optional sfile As Boolean = True)

        Dim xlBookTmp As excel.Workbook
        Dim xlBookBak As excel.Workbook

        Try
            Dim _Qry As String = ""
            Dim _Dt As System.Data.DataTable
            Dim _oDtM As System.Data.DataTable

            _Qry = "  Select CASE WHEN ISDATE(FDInsDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDInsDate),103) ELSE '' END AS FDInsDate ,CASE WHEN ISDATE(FDUpdDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDUpdDate),103) ELSE '' END AS FDUpdDate ,FTCostSheetNo ,FNRevised ,CASE WHEN ISDATE(FDCostSheetDate) = 1 THEN Convert(varchar(10),Convert(Datetime,FDCostSheetDate),103) ELSE '' END AS FDCostSheetDate"
            _Qry &= vbCrLf & " ,FNNormalSizeAmt ,FNAboveSpecialSizeAmt ,FNLessThanSpecialSizeAmt ,FNNormalSizeAmt + FNAboveSpecialSizeAmt + FNLessThanSpecialSizeAmt AS SumTotal"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim _Sheet As Integer = 0

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString


            Dim pFileName As String = _Path & "\ExportExcel\CostSheetExcelRptCostSheet.xlsm"


            If sfile Then

                File.Copy(pFileName, _FileName, True)
                opshet.Document.LoadDocument(_FileName, DocumentFormat.Xlsm)
            Else


                _FileName = _Path & "\ExportExcel\CostSheetExcelRptCostSheet.xlsm"
                opshet.Document.LoadDocument(File.ReadAllBytes(pFileName), DocumentFormat.Xlsm)
            End If

            ' File.Copy(pFileName, _FileName, True)

            'opshet.Document.LoadDocument(File.ReadAllBytes(pFileName), DocumentFormat.Xlsm)
            ' opshet.Document.LoadDocument(_FileName, DocumentFormat.Xlsm)
            opshet.BeginUpdate()


            Dim pworkbook As IWorkbook
            pworkbook = opshet.Document
            pworkbook.Worksheets.ActiveWorksheet = pworkbook.Worksheets("STD-PAR CBD")
            With opshet.ActiveWorksheet

                .Rows(1).Item(2).SetValue(FNHSysSeasonId.Text)
                .Rows(2).Item(2).SetValue(FNHSysVenderPramId.Text)
                .Rows(3).Item(2).SetValue(FNHSysStyleId.Text)
                .Rows(4).Item(2).SetValue(FNHSysStyleId_None.Text)
                .Rows(5).Item(2).SetValue(FNISTeamMulti.Text)
                .Rows(6).Item(2).SetValue(FNCostSheetColor.Text)
                .Rows(7).Item(2).SetValue(FNCostSheetSize.Text)
                .Rows(8).Item(2).SetValue(FNCostSheetBuyType.Text)
                .Rows(9).Item(2).SetValue(FNVersion.Text)
                .Rows(10).Item(2).SetValue(FTLOProductDeveloper.Text)
                .Rows(11).Item(2).SetValue(FNCostSheetQuotedType.Text)
                .Rows(12).Item(2).SetValue(FTMSC.Text)

                Dim pDate As String = HI.UL.ULDate.ConvertEnDB(FTDateQuoted.Text)


                Dim pDate2 As String = ""

                If pDate <> "" Then
                    pDate2 = Integer.Parse(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(pDate, 7), 2)).ToString + "/" + Integer.Parse(Microsoft.VisualBasic.Right(pDate, 2)).ToString + "/" + Microsoft.VisualBasic.Left(pDate, 4)
                End If

                .Rows(13).Item(2).SetValue(pDate2)
                .Rows(14).Item(2).SetValue(FNCostSheetSampleRound.Text)
                .Rows(15).Item(2).SetValue(FNHSysStyleIdTo.Text)
                .Rows(16).Item(2).SetValue(FTQuotedLog.Text)
                .Rows(17).Item(2).SetValue(FTRemark.Text)

                'FOB
                '.Rows(4).Item(5).SetValue(IIf(FNTotalFabAmt.Value = 0, "-", FNTotalFabAmt.Text))
                '.Rows(5).Item(5).SetValue(IIf(FNTotalAccAmt.Value = 0, "-", FNTotalAccAmt.Text))
                '.Rows(6).Item(5).SetValue(IIf(FNChargeFabAmt.Value = 0, "-", FNChargeFabAmt.Text))
                '.Rows(7).Item(5).SetValue(IIf(FNChargeAccAmt.Value = 0, "-", FNChargeAccAmt.Text))
                '.Rows(8).Item(5).SetValue(IIf(FNProcessMatCost.Value = 0, "-", FNProcessMatCost.Text))
                '.Rows(9).Item(5).SetValue(IIf(FNProcessLaborCost.Value = 0, "-", FNProcessLaborCost.Text))
                '.Rows(10).Item(5).SetValue(IIf(FNPackagingAmt.Value = 0, "-", FNPackagingAmt.Text))

                If FNOtherCostAmt.Value > 0 Then
                    .Rows(11).Item(5).SetValue(FNOtherCostAmt.Value)
                End If

                ' .Rows(12).Item(5).SetValue(IIf(FNCMP.Value = 0, "-", Decimal.Parse(Format(Val(FNCMP.Value) / 100.0, "0.00"))))
                ' .Rows(13).Item(5).SetValue(Decimal.Parse(Format(Val(FNGrandTotal.Value) / 100.0, "0.00")))

                Try

                    .Rows(14).Item(5).SetValue(Decimal.Parse(Format(Val(FNExtendedPer.Value) / 100.0, "0.00")))

                Catch ex As Exception
                End Try

                ' .Rows(15).Item(5).SetValue(IIf(FNExtendedFOB.Value = 0, "-", Decimal.Parse(Format(Val(FNExtendedFOB.Value) / 100.0, "0.00"))))


                Try

                    ' .Rows(16).Item(5).SetValue(Decimal.Parse(Format(Val(FNTrinUsageAllowPer.Value) / 100.0, "0.00")))

                Catch ex As Exception

                End Try
                '.Rows(16).Item(5).SetValue(FNTrinUsageAllowPer.Text)


                'L4L
                '.Rows(4).Item(6).SetValue(IIf(FNL4LTotalFabric.Value = 0, "-", FNL4LTotalFabric.Text))
                '.Rows(5).Item(6).SetValue(IIf(FNL4LTotalTrim.Value = 0, "-", FNL4LTotalTrim.Text))
                '.Rows(6).Item(6).SetValue(IIf(FNL4LChargeFabric.Value = 0, "-", FNL4LChargeFabric.Text))
                '.Rows(7).Item(6).SetValue(IIf(FNL4LChargeTrim.Value = 0, "-", FNL4LChargeTrim.Text))
                '.Rows(8).Item(6).SetValue(IIf(FNL4LProMatCost.Value = 0, "-", FNL4LProMatCost.Text))
                '.Rows(9).Item(6).SetValue(IIf(FNL4LProLaborCost.Value = 0, "-", FNL4LProLaborCost.Text))
                '.Rows(10).Item(6).SetValue(IIf(FNL4LPackaging.Value = 0, "-", FNL4LPackaging.Text))

                If FNL4LOtherCost.Value > 0 Then
                    .Rows(11).Item(6).SetValue(FNL4LOtherCost.Value)
                End If

                ' .Rows(12).Item(6).SetValue(IIf(FNL4LCMP.Value = 0, "-", FNL4LCMP.Text))
                '.Rows(13).Item(6).SetValue(IIf(FNL4LFinalFOB.Value = 0, "-", FNL4LFinalFOB.Text))
                '.Rows(15).Item(6).SetValue(IIf(FNL4LExtendedFOB.Value = 0, "-", FNL4LExtendedFOB.Text))


                'FOB For L4L

                If FNL4Country1.Text.Trim <> "" Then
                    .Rows(2).Item(9).SetValue(FNL4Country1.Text)
                    '.Rows(3).Item(9).SetValue(FNL4Country1Cur.Text)
                    .Rows(4).Item(9).SetValue(IIf(FNL4Country1Exc.Value = 0, 0, FNL4Country1Exc.Text))
                    '.Rows(5).Item(9).SetValue(IIf(FNL4Country1Finalm.Value = 0, "-", FNL4Country1Finalm.Text))
                    '.Rows(6).Item(9).SetValue(IIf(FNL4Country1Extendedm.Value = 0, "-", FNL4Country1Extendedm.Text))
                End If

                If FNL4Country2.Text.Trim <> "" Then
                    .Rows(2).Item(10).SetValue(FNL4Country2.Text)
                    ' .Rows(3).Item(10).SetValue(FNL4Country2Cur.Text)
                    .Rows(4).Item(10).SetValue(IIf(FNL4Country2Exc.Value = 0, 0, FNL4Country2Exc.Text))
                    '.Rows(5).Item(10).SetValue(IIf(FNL4Country2Finalm.Value = 0, "-", FNL4Country2Finalm.Text))
                    '.Rows(6).Item(10).SetValue(IIf(FNL4Country2Extendedm.Value = 0, "-", FNL4Country2Extendedm.Text))
                End If

                If FNL4Country2.Text.Trim <> "" Then

                    .Rows(2).Item(11).SetValue(FNL4Country3.Text)
                    ' .Rows(3).Item(11).SetValue(FNL4Country3Cur.Text)
                    .Rows(4).Item(11).SetValue(IIf(FNL4Country3Exc.Value = 0, 0, FNL4Country3Exc.Text))
                    '.Rows(5).Item(11).SetValue(IIf(FNL4Country3Finalm.Value = 0, "-", FNL4Country3Finalm.Text))
                    '.Rows(6).Item(11).SetValue(IIf(FNL4Country3Extendedm.Value = 0, "-", FNL4Country3Extendedm.Text))
                End If




                _Qry = "  Select C.* , ISNULL(C.FTMainMatColorCode,'') + '|' +  ISNULL(C.FTTeamName,'')  AS FTTeam"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK)  "
                _Qry &= vbCrLf & " WHERE C.FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "' "

                _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                Dim dttmp As DataTable

                Dim SRowFac As Integer = 28
                Dim SRowAcc As Integer = 53
                Dim SRowProMat As Integer = 78
                Dim SRowProLabor As Integer = 93
                Dim SRowPack As Integer = 109
                Dim SRowCmp As Integer = 122
                Dim SRowBemis As Integer = 128
                Dim SRowMulti As Integer = 8

                Dim RowIdx As Integer = 0
                Dim RowData As Integer = 0

                For Idx As Integer = 1 To 7

                    Try

                        dttmp = _Dt.Select("FNCostType=" & Idx & "", "FNSeq").CopyToDataTable

                        RowIdx = 0

                        Select Case Idx
                            Case 1

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowFac + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTMainMatColorCode.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)
                                    .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNWeight.ToString), "0.00")))

                                    If Val(R!FNWidth.ToString) > 0 Then
                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        .Rows(RowData).Item(7).SetValue(pWith)
                                    End If

                                    .Rows(RowData).Item(8).SetValue(R!FTWidthUnit.ToString)

                                    If Val(R!FNMarkerEff.ToString) > 0 Then

                                        Dim mMark As Decimal = (Decimal.Parse(Format(Val(R!FNMarkerEff.ToString) / 100.0, "0.00000")))

                                        .Rows(RowData).Item(9).SetValue(mMark)


                                    End If

                                    .Rows(RowData).Item(10).SetValue(Format(Val(R!FNMarkerUsed.ToString), "0.0000"))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(11).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If


                                    ' .Rows(RowData).Item(12).SetValue(Format(Val(R!FNTotalUsed.ToString), "0.0000"))
                                    .Rows(RowData).Item(13).SetValue(R!FTRMDSSeason.ToString)
                                    .Rows(RowData).Item(14).SetValue(R!FNRMDSStatus.ToString)
                                    .Rows(RowData).Item(15).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(16).SetValue(Format(Val(R!FNCostPerUOM.ToString), "0.000"))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Rows(RowData).Item(17).SetValue(Format(Val(R!FNCIF.ToString), "0.0000"))
                                    End If

                                    '  .Rows(RowData).Item(18).SetValue(Format(Val(R!FNUSAGECOST.ToString), "0.0000"))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(19).SetValue((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Rows(RowData).Item(20).SetValue(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    End If



                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(21).SetValue((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    RowIdx = RowIdx + 1

                                Next

                            Case 2

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowAcc + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTMainMatColorCode.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)


                                    If Val(R!FNWidth.ToString) > 0 Then
                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        .Rows(RowData).Item(6).SetValue(pWith)
                                    End If

                                    .Rows(RowData).Item(7).SetValue(R!FTWidthUnit.ToString)
                                    .Rows(RowData).Item(8).SetValue(Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(9).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If

                                    ' .Rows(RowData).Item(10).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Rows(RowData).Item(11).SetValue(R!FTRMDSSeason.ToString)
                                    .Rows(RowData).Item(12).SetValue(R!FNRMDSStatus.ToString)
                                    .Rows(RowData).Item(13).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(14).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Rows(RowData).Item(15).SetValue(Decimal.Parse(Format(Val(R!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Rows(RowData).Item(16).SetValue(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(17).SetValue((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Rows(RowData).Item(18).SetValue(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    End If

                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(19).SetValue((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    RowIdx = RowIdx + 1

                                Next

                            Case 3
                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProMat + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTPROCESSSUBTYPE.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)


                                    .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))

                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(7).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If

                                    ' .Rows(RowData).Item(8).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))

                                    .Rows(RowData).Item(9).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(10).SetValue((Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000"))))
                                    ' .Rows(RowData).Item(11).SetValue((Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000"))))

                                    RowIdx = RowIdx + 1

                                Next

                            Case 4

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowProLabor + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTPROCESSSUBTYPE.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(5).SetValue(R!FTUse.ToString)
                                    .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Rows(RowData).Item(7).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(8).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))
                                    ' .Rows(RowData).Item(9).SetValue(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    RowIdx = RowIdx + 1

                                Next

                            Case 5

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowPack + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTMainMatCode.ToString)
                                    .Rows(RowData).Item(1).SetValue(R!FTMainMatName.ToString)
                                    .Rows(RowData).Item(2).SetValue(R!FTSuplCode.ToString)
                                    .Rows(RowData).Item(3).SetValue(R!TTLG.ToString)
                                    .Rows(RowData).Item(4).SetValue(R!FTUse.ToString)

                                    If Val(R!FNWidth.ToString) > 0 Then
                                        '  .Rows(RowData).Item(5).SetValue(Format(Val(R!FNWidth.ToString), "0.00"))


                                        Dim pWith As String = Format(Val(R!FNWidth.ToString), "0.00")


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        If Microsoft.VisualBasic.Right(pWith, 1) = "0" Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If


                                        If Microsoft.VisualBasic.Right(pWith, 1) = "." Then
                                            pWith = Microsoft.VisualBasic.Left(pWith, pWith.Length - 1)
                                        End If

                                        .Rows(RowData).Item(5).SetValue(pWith)

                                    End If

                                    .Rows(RowData).Item(6).SetValue(R!FTWidthUnit.ToString)
                                    .Rows(RowData).Item(7).SetValue(Decimal.Parse(Format(Val(R!FNMarkerUsed.ToString), "0.0000")))


                                    If Val(R!FNAllowancePer.ToString) > 0 Then
                                        .Rows(RowData).Item(8).SetValue((Decimal.Parse(Format(Val(R!FNAllowancePer.ToString) / 100.0, "0.00"))))
                                    End If


                                    ' .Rows(RowData).Item(9).SetValue(Decimal.Parse(Format(Val(R!FNTotalUsed.ToString), "0.0000")))
                                    .Rows(RowData).Item(10).SetValue(R!FTRMDSSeason.ToString)
                                    .Rows(RowData).Item(11).SetValue(R!FNRMDSStatus.ToString)
                                    .Rows(RowData).Item(12).SetValue(R!FTUnitCode.ToString)
                                    .Rows(RowData).Item(13).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.000")))

                                    If Val(R!FNCIF.ToString) > 0 Then
                                        .Rows(RowData).Item(14).SetValue(Decimal.Parse(Format(Val(R!FNCIF.ToString), "0.0000")))
                                    End If

                                    ' .Rows(RowData).Item(15).SetValue(Decimal.Parse(Format(Val(R!FNUSAGECOST.ToString), "0.0000")))

                                    If Val(R!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(16).SetValue((Decimal.Parse(Format(Val(R!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00"))))
                                    End If


                                    If Val(R!FNHANDLINGCHARGECOST.ToString) > 0 Then
                                        ' .Rows(RowData).Item(17).SetValue(Decimal.Parse(Format(Val(R!FNHANDLINGCHARGECOST.ToString), "0.0000")))
                                    End If



                                    If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(18).SetValue((Decimal.Parse(Format(Val(R!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00"))))
                                    End If

                                    RowIdx = RowIdx + 1

                                Next

                            Case 6

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowCmp + RowIdx
                                    If Val(R!FNCostPerUOM.ToString) > 0 Then
                                        .Rows(RowData).Item(0).SetValue(Decimal.Parse(Format(Val(R!FNCostPerUOM.ToString), "0.0000")))
                                    End If

                                    If Val(R!FNSTANDARDALLOWEDMINUTES.ToString) > 0 Then
                                        .Rows(RowData).Item(1).SetValue(Decimal.Parse(Format(Val(R!FNSTANDARDALLOWEDMINUTES.ToString), "0.00")))
                                    End If


                                    If Val(R!FNEFFICIENCYPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(2).SetValue(Decimal.Parse(Format(Val(R!FNEFFICIENCYPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    If Val(R!FNPROFITPERCENT.ToString) > 0 Then
                                        .Rows(RowData).Item(3).SetValue(Decimal.Parse(Format(Val(R!FNPROFITPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    If Val(R!FNCMPCOST.ToString) > 0 Then
                                        .Rows(RowData).Item(4).SetValue(Decimal.Parse(Format(Val(R!FNCMPCOST.ToString), "0.00")))
                                    End If

                                    .Rows(RowData).Item(5).SetValue(R!FTBMCCODE.ToString)

                                    RowIdx = RowIdx + 1

                                Next

                            Case 7

                                For Each R As DataRow In dttmp.Rows

                                    RowData = SRowBemis + RowIdx

                                    .Rows(RowData).Item(0).SetValue(R!FTBEMISITEM.ToString)
                                    .Rows(RowData).Item(1).SetValue(Decimal.Parse(Format(Val(R!FNFULLWIDTH.ToString), "0.0000")))
                                    .Rows(RowData).Item(2).SetValue(Decimal.Parse(Format(Val(R!FNSLITTINGWIDTH.ToString), "0.0000")))
                                    .Rows(RowData).Item(3).SetValue(Decimal.Parse(Format(Val(R!FNREQUIREDLENGTH.ToString), "0.0000")))
                                    ' .Rows(RowData).Item(4).SetValue(Decimal.Parse(Format(Val(R!FNNETUSAGEINFULLWIDTH.ToString), "0.0000")))
                                    .Rows(RowData).Item(5).SetValue(Decimal.Parse(Format(Val(R!FNPRICEINMETER.ToString), "0.0000")))


                                    If Val(R!FNBEMISSLITTINGUPCHARGE.ToString) > 0 Then
                                        .Rows(RowData).Item(6).SetValue(Decimal.Parse(Format(Val(R!FNBEMISSLITTINGUPCHARGE.ToString) / 100.0, "0.00")))
                                    End If

                                    '  .Rows(RowData).Item(7).SetValue(Decimal.Parse(Format(Val(R!FNPRICEPERSLITTINGWITDH.ToString), "0.0000")))

                                    RowIdx = RowIdx + 1

                                Next

                        End Select


                    Catch ex As Exception
                    End Try

                Next

                If FNISTeamMulti.SelectedIndex = 1 Then

                    Try
                        dttmp = _Dt.Select("FNCostType=8", "FNSeq").CopyToDataTable

                        Dim grp As List(Of String) = (dttmp.Select("FTTeam<>''", "FTTeam").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTTeam")) _
                                                      .Distinct() _
                                                      .ToList()

                        RowIdx = 7
                        Dim ColIndex As Integer = 0
                        Dim SkipCol As Integer = 11



                        pworkbook.Worksheets.ActiveWorksheet = pworkbook.Worksheets("TEAMMULTI CBD")
                        With opshet.ActiveWorksheet

                            .Rows(2).Item(4).SetValue(FNTrinUsageAllowPer.Text)

                            For Each Rxm As String In grp
                                ColIndex = 0

                                For Each Rm1 As DataRow In dttmp.Select("FTTeam='" & HI.UL.ULF.rpQuoted(Rxm) & "'", "FNSeq")

                                    .Rows(RowIdx).Item(0).SetValue(FTMSC.Text)
                                    .Rows(RowIdx).Item(1).SetValue(FNHSysSeasonId.Text)
                                    .Rows(RowIdx).Item(2).SetValue(FNHSysStyleId.Text)
                                    .Rows(RowIdx).Item(3).SetValue(Rm1!FTMainMatColorCode.ToString)
                                    .Rows(RowIdx).Item(4).SetValue(Rm1!FTTeamName.ToString)
                                    .Rows(RowIdx).Item(5).SetValue(FNGrandTotal.Text)

                                    .Rows(RowIdx).Item(184).SetValue(Rm1!FTPRODUCTDEVELOPER.ToString)


                                    Exit For

                                Next

                                ColIndex = 8
                                For Each Rm1 As DataRow In dttmp.Select("FTTeam='" & HI.UL.ULF.rpQuoted(Rxm) & "'", "FNSeq")

                                    .Rows(RowIdx).Item(ColIndex).SetValue(Rm1!FTMainMatCode.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 1).SetValue(Rm1!FTPROCESSSUBTYPE.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 2).SetValue(Rm1!FTMainMatName.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 3).SetValue(Rm1!FTSuplCode.ToString)
                                    .Rows(RowIdx).Item(ColIndex + 4).SetValue(Decimal.Parse(Format(Val(Rm1!FNCostPerUOM.ToString), "0.0000")))
                                    .Rows(RowIdx).Item(ColIndex + 5).SetValue(Decimal.Parse(Format(Val(Rm1!FNCIF.ToString), "0.0000")))
                                    ' .Rows(RowIdx).Item(ColIndex + 6).SetValue(Format(Val(Rm1!FNUSAGECOST.ToString), "0.00"))



                                    If Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString) > 0 Then
                                        .Rows(RowIdx).Item(ColIndex + 7).SetValue(Decimal.Parse(Format(Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString) / 100.0, "0.00")))
                                    End If


                                    ' .Rows(RowIdx).Item(ColIndex + 7).SetValue(Format(Val(Rm1!FNHANDLINGCHARGEPERCENT.ToString), "0.00"))
                                    ' .Rows(RowIdx).Item(ColIndex + 8).SetValue(Format(Val(Rm1!FNHANDLINGCHARGECOST.ToString), "0.0000"))
                                    ' .Rows(RowIdx).Item(ColIndex + 9).SetValue(Format(Val(Rm1!FNHANDLINGCHARGECOST.ToString) + Val(Rm1!FNUSAGECOST.ToString), "0.0000"))

                                    If Val(Rm1!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                                        .Rows(RowIdx).Item(ColIndex + 10).SetValue(Decimal.Parse(Format(Val(Rm1!FNIMPORTDUTYPERCENT.ToString) / 100.0, "0.00")))
                                    End If

                                    ' .Rows(RowIdx).Item(ColIndex + 10).SetValue(Format(Val(Rm1!FNIMPORTDUTYPERCENT.ToString), "0.00"))


                                    ColIndex = ColIndex + SkipCol
                                Next

                                RowIdx = RowIdx + 1

                            Next


                        End With
                        'With opshet.ActiveWorksheet

                        'End With
                        'With xlBookTmp.Worksheets(2)
                        '    .Rows(1).Item(2).SetValue(FNHSysVenderPramId.Text
                        '    .Rows(2).Item(2).SetValue(FNHSysStyleId.Text
                        '    .Rows(3).Item(2).SetValue(FNHSysStyleId_None.Text

                        '    .Rows(5).Item(2).SetValue(FNHSysSeasonId.Text
                        'End With


                    Catch ex As Exception
                    End Try

                End If


            End With

            pworkbook.Worksheets.ActiveWorksheet = pworkbook.Worksheets("STD-PAR CBD")
            opshet.EndUpdate()

            If sfile Then
                opshet.SaveDocument()
            End If

            '  opshet.SaveDocumentAs(_FileName)

            'xlBookTmp.Close()

            'HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            If sfile Then
                _Spls.Close()
                Process.Start(_FileName)
            End If

        Catch ex As Exception

            Try
                xlBookTmp.Close()
            Catch ex2 As Exception

            End Try
            If sfile Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessError(141115, "Writing Excel File Error.....", Me.Text, MessageBoxIcon.Error)
            End If

        End Try
    End Sub


#End Region

    Private Sub InitNewRow() 'add new row in gridview
        Try

            Dim mdataTable As DataTable
            Dim _ogc As Object
            Dim _ct As Integer = 0

            Select Case otb.SelectedTabPageIndex
                Case 0

                    With CType(Me.ogcfabric.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        Exit Sub
                    End If

                    'If mdataTable.Select("FNCIF=0").Length > 0 Then
                    '    HI.MG.ShowMsg.mInfo("Please Input CIF !!!", 21125487, Me.Text,, MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If

                    _ogc = ogcfabric

                Case 1

                    With CType(Me.ogctrims.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        Exit Sub
                    End If

                    'If mdataTable.Select("FNCIF=0").Length > 0 Then
                    '    HI.MG.ShowMsg.mInfo("Please Input CIF !!!", 21125487, Me.Text,, MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If

                    _ogc = ogctrims

                Case 2

                    With CType(Me.ogcprocessmat.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        'HI.MG.ShowMsg.mInfo("Please Input CIF !!!", 21125487, Me.Text,, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    _ogc = ogcprocessmat

                Case 3

                    With CType(Me.ogcprocesslabor.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        Exit Sub
                    End If

                    _ogc = ogcprocesslabor

                Case 4

                    With CType(Me.ogcpack.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        Exit Sub
                    End If

                    'If mdataTable.Select("FNCIF=0").Length > 0 Then
                    '    HI.MG.ShowMsg.mInfo("Please Input CIF !!!", 21125487, Me.Text,, MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If

                    _ogc = ogcpack

                Case 5

                    With CType(Me.ogccmp.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    'If dataTable.Select("FNCostPerUOM=0").Length > 0 Then
                    '    Exit Sub
                    'End If

                    _ogc = ogccmp

                Case 6

                    With CType(Me.ogcbemis.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        Exit Sub
                    End If

                    _ogc = ogcbemis

                Case 7

                    With CType(Me.ogcteamMulti.DataSource, DataTable)
                        .AcceptChanges()
                        mdataTable = .Copy
                    End With

                    If mdataTable.Select("FTMainMatCode=''").Length > 0 Then
                        Exit Sub
                    End If

                    'If mdataTable.Select("FNCIF=0").Length > 0 Then
                    '    HI.MG.ShowMsg.mInfo("Please Input CIF !!!", 21125487, Me.Text,, MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If


                    _ogc = ogcteamMulti

            End Select

            Dim dr As DataRow
            dtStyleDetail = mdataTable
            dr = dtStyleDetail.NewRow()

            Dim LastMatSeq As Integer = dtStyleDetail.Rows.Count + 1

            _ct = otb.SelectedTabPageIndex + 1

            For Each Col As DataColumn In dtStyleDetail.Columns

                Select Case Col.DataType.ToString
                    Case "System.String"
                        dr.Item(Col.ColumnName) = ""
                    Case Else
                        dr.Item(Col.ColumnName) = 0
                End Select

            Next

            Select Case otb.SelectedTabPageIndex
                Case 7
                    dr.Item("FTMainMatColorCode") = FNCostSheetColor.Text
                    dr.Item("FTTeamName") = FNHSysStyleId_None.Text
                    dr.Item("FNAllowancePer") = FNTrinUsageAllowPer.Value
                Case Else
            End Select

            dr.Item("FNSeq") = LastMatSeq
            dr.Item("FNCostType") = _ct
            dr.Item("FNHSysMainMatId") = 0
            dr.Item("FNRevised") = FNRevised.Value

            dtStyleDetail.Rows.Add(dr)

            _ogc.DataSource = dtStyleDetail
            _ogc.Refresh()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SumAmt()
        Try

            Dim pTotalFabAmt As Decimal = 0
            Dim pFNTotalAccAmt As Decimal = 0
            Dim pChargeFabAmt As Decimal = 0
            Dim pChargeAccAmt As Decimal = 0
            Dim pProcessMatCost As Decimal = 0
            Dim pProcessLaborCost As Decimal = 0
            Dim pPackagingAmt As Decimal = 0
            Dim pCMP As Decimal = 0


            Try
                With CType(Me.ogcfabric.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        pTotalFabAmt = pTotalFabAmt + Decimal.Parse(Format(R!FNUSAGECOST, "0.0000"))
                        pChargeFabAmt = pChargeFabAmt + Decimal.Parse(Format(R!FNHANDLINGCHARGECOST, "0.0000"))


                        If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                            pChargeFabAmt = pChargeFabAmt + Decimal.Parse(Format((Val(R!FNUSAGECOST.ToString) * Val(R!FNIMPORTDUTYPERCENT.ToString)) / 100.0, "0.0000"))
                        End If

                    Next

                End With
            Catch ex As Exception

            End Try


            Try
                With CType(Me.ogctrims.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        pFNTotalAccAmt = pFNTotalAccAmt + Decimal.Parse(Format(R!FNUSAGECOST, "0.0000"))
                        pChargeAccAmt = pChargeAccAmt + Decimal.Parse(Format(R!FNHANDLINGCHARGECOST, "0.0000"))


                        If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                            pChargeAccAmt = pChargeAccAmt + Decimal.Parse(Format((Val(R!FNUSAGECOST.ToString) * Val(R!FNIMPORTDUTYPERCENT.ToString)) / 100.0, "0.0000"))
                        End If


                    Next

                End With
            Catch ex As Exception

            End Try

            Try
                With CType(Me.ogcprocessmat.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        pProcessMatCost = pProcessMatCost + Decimal.Parse(Format((R!FNUSAGECOST), "0.0000"))



                        If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                            pProcessMatCost = pProcessMatCost + Decimal.Parse(Format((Val(R!FNUSAGECOST.ToString) * Val(R!FNIMPORTDUTYPERCENT.ToString)) / 100.0, "0.0000"))
                        End If


                    Next

                End With
            Catch ex As Exception

            End Try


            Try
                With CType(Me.ogcprocesslabor.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        pProcessLaborCost = pProcessLaborCost + Decimal.Parse(Format(R!FNUSAGECOST, "0.0000"))


                        If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                            pProcessLaborCost = pProcessLaborCost + Decimal.Parse(Format((Val(R!FNUSAGECOST.ToString) * Val(R!FNIMPORTDUTYPERCENT.ToString)) / 100.0, "0.0000"))
                        End If

                    Next

                End With
            Catch ex As Exception

            End Try

            Try
                With CType(Me.ogcpack.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        pPackagingAmt = pPackagingAmt + Decimal.Parse(Format(R!FNUSAGECOST, "0.0000"))

                        If Val(R!FNIMPORTDUTYPERCENT.ToString) > 0 Then
                            pPackagingAmt = pPackagingAmt + Decimal.Parse(Format((Val(R!FNUSAGECOST.ToString) * Val(R!FNIMPORTDUTYPERCENT.ToString)) / 100.0, "0.0000"))
                        End If

                        pPackagingAmt = pPackagingAmt + Decimal.Parse(Format(R!FNHANDLINGCHARGECOST, "0.0000"))
                    Next

                End With
            Catch ex As Exception

            End Try


            Try
                With CType(Me.ogccmp.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Rows
                        pCMP = pCMP + Decimal.Parse(Format(R!FNCMPCOST, "0.0000"))

                    Next

                End With
            Catch ex As Exception

            End Try

            FNTotalFabAmt.Value = pTotalFabAmt
            FNTotalAccAmt.Value = pFNTotalAccAmt
            FNChargeFabAmt.Value = pChargeFabAmt
            FNChargeAccAmt.Value = pChargeAccAmt
            FNProcessMatCost.Value = pProcessMatCost
            FNProcessLaborCost.Value = pProcessLaborCost
            FNPackagingAmt.Value = pPackagingAmt
            FNCMP.Value = pCMP


            Call Calculate(FNTotalFabAmt, Nothing)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNOtherCostAmt.EditValueChanged, FNLessThanSpecialSizeChargePerAmt.EditValueChanged, FNGarmentTreatmentAmt.EditValueChanged, FNCMP.EditValueChanged, FNAboveSpecialSizeChargePerAmt.EditValueChanged
        Dim _Gtotal As Double = 0
        Dim _AboveAmt As Double = 0
        Dim _LessAmt As Double = 0

        FNNormalSizeAmt.Value = FNTotalFabAmt.Value + FNTotalAccAmt.Value + FNChargeFabAmt.Value + FNChargeFabAmt.Value + FNChargeAccAmt.Value + FNProcessMatCost.Value + FNProcessLaborCost.Value + +FNPackagingAmt.Value + FNOtherCostAmt.Value + FNCMP.Value
        _Gtotal = FNNormalSizeAmt.Value



        If (FNAboveSpecialSizeChargePerAmt.Value > 0) Then
            _AboveAmt = FNNormalSizeAmt.Value * FNAboveSpecialSizeChargePerAmt.Value / 100
            FNAboveSpecialSizeAmt.Value = _AboveAmt
            _Gtotal += _AboveAmt
        End If

        If (FNLessThanSpecialSizeChargePerAmt.Value > 0) Then
            _LessAmt = FNNormalSizeAmt.Value * FNLessThanSpecialSizeChargePerAmt.Value / 100
            FNLessThanSpecialSizeAmt.Value = _LessAmt
            _Gtotal += _LessAmt
        End If

        FNGrandTotal.Value = FNNormalSizeAmt.Value


        FNExtendedFOB.Value = 0

        If FNExtendedPer.Value > 0 And FNGrandTotal.Value > 0 Then
            FNExtendedFOB.Value = FNGrandTotal.Value + Decimal.Parse(Format(((FNGrandTotal.Value * FNExtendedPer.Value) / 100.0), "0.00"))
        End If

    End Sub



    Private Sub ocmbomaddnew_Click(sender As Object, e As EventArgs) Handles ocmbomaddnew.Click
        If CheckOwner() = False Then
            Exit Sub
        End If

        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return
        CType(ogcfabric.DataSource, DataTable).AcceptChanges()


        InitNewRow()


        'If (otb.SelectedTabPage Is otpfabric) Then
        '    InitNewRow(CType(ogcfabric.DataSource, DataTable), TabIndexs.FabricDetail)

        'ElseIf (otb.SelectedTabPage Is otptrim) Then
        '    InitNewRow(CType(ogctrims.DataSource, DataTable), TabIndexs.TrimsDetail)

        'ElseIf (otb.SelectedTabPage Is otpnosew) Then
        '    InitNewRow(CType(ogcnosew.DataSource, DataTable), TabIndexs.NoSewDetail)

        'Else
        '    InitNewRow(CType(ogcpack.DataSource, DataTable), TabIndexs.PackagingDetail)

        'End If

    End Sub

    Private Sub wCostSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Enable "..." button in gridview 
        'Table Fabric
        'AddHandler RepositoryFTMainMatCode.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        'AddHandler RepositoryFNHSysSuplId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        'AddHandler RepositoryFTUnitCode.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        ''Table Trims

        ''Table No Sew
        'AddHandler RepositoryFTMainMatCode_S.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        'AddHandler RepositoryFNHSysSuplId_S.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        'AddHandler RepositoryFTUnitCode_S.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
        'Table Packaging


        RemoveHandler FTCostSheetNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTCostSheetNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString


        RepositoryItemGridLookUpEditFTMainMatCode.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTMainMatCodeTrim.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTMainMatCodePacking.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTMainMatCodeBemis.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTMainMatCodeProcessMat.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTMainMatCodeLabor.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditItemMulti.View.OptionsView.ShowAutoFilterRow = True

        RepositoryItemGridLookUpEditFTSuplCode.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTSuplCodeTrim.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTSuplCodeProcessMat.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTSuplCodeLabor.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTSuplCodePack.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTSuplCodeMulti.View.OptionsView.ShowAutoFilterRow = True



        RepositoryItemGridLookUpFTUnitCode.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTUnitCodeTrim.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTUnitCodeProcessMat.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTUnitCodeWidthunitPack.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTUnitCodeLabor.View.OptionsView.ShowAutoFilterRow = True

        RepositoryItemGridLookUpEditUOMWidth.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditUOMWidthtrim.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditUOMWidthPack.View.OptionsView.ShowAutoFilterRow = True


        RepositoryItemGridLookUpEditCFCO.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTCOFOLabor.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTCOFOProcessMat.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTCOFOTrim.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTCOFOPack.View.OptionsView.ShowAutoFilterRow = True


        RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMat.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTPROCESSSUBTYPELabor.View.OptionsView.ShowAutoFilterRow = True
        RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMulti.View.OptionsView.ShowAutoFilterRow = True


        RepositoryItemGridLookUpEditFTStyleCode.View.OptionsView.ShowAutoFilterRow = True

        LoadMaster()

        _FormLoad = False
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        End If
    End Sub

    Public Sub LoadDataInfo2(ByVal Key As Object) 'load detail in gridview from DB
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _tDt As New DataTable
            Dim _sDt As New DataTable
            Dim _pDt As New DataTable


            _Qry = "  Select   C.FTInsUser, C.FDInsDate, C.FTInsTime, C.FTUpdUser, C.FDUpdDate, C.FTUpdTime, C.FTCostSheetNo, C.FNRevised, C.FNVersion, C.FNCostType, C.FNSeq, C.FNHSysMainMatId, C.FNHSysSuplId, "
            _Qry &= vbCrLf & "     C.FNCostPerPiece, C.FNExten, C.FNExtenPer, C.FNNetExten, C.FNChinaOrderCost, C.FNMalaysiaOrderCost, C.FNThailandOrderCost, C.FNJapanOrderCost, C.FTSize, C.FTMainMatCode, "
            _Qry &= vbCrLf & "     C.FTMainMatColorCode, C.FTMainMatName, C.FTSuplCode, C.TTLG, C.FTUse, C.FNWeight, C.FNWidth, C.FTWidthUnit, C.FNMarkerEff, C.FNMarkerUsed, C.FNAllowancePer, C.FNTotalUsed, "
            _Qry &= vbCrLf & "    C.FTRMDSSeason, C.FNRMDSStatus, C.FNHSysUnitId, C.FTUnitCode, C.FNCostPerUOM, C.FNCIF, C.FNUSAGECOST, C.FNHANDLINGCHARGEPERCENT, C.FNHANDLINGCHARGECOST, "
                    _Qry &= vbCrLf & "    C.FNIMPORTDUTYPERCENT, C.FNImportDuty, C.FTPROCESSSUBTYPE, C.FNHSysProcessMatId, C.FNSTANDARDALLOWEDMINUTES, C.FNEFFICIENCYPERCENT, C.FNPROFITPERCENT, "
            _Qry &= vbCrLf & "    C.FNCMPCOST, C.FTBMCCODE, C.FTBEMISITEM, C.FNFULLWIDTH, C.FNSLITTINGWIDTH, C.FNREQUIREDLENGTH, C.FNNETUSAGEINFULLWIDTH, C.FNPRICEINMETER, "
            _Qry &= vbCrLf & "    C.FNBEMISSLITTINGUPCHARGE, C.FNPRICEPERSLITTINGWITDH, C.FTRemark, C.FNTOTALUSAGECOST, C.FNTOTALHANDINGCHANGECOST, C.FNFINALFOB, C.FNEXTENDEDSIZEFOB, "
            _Qry &= vbCrLf & "    C.FNTOTALTRIMPROCESSCOST, C.FTL4LORDERCNTY1, C.FTL4LCURRENCYFOB1, C.FNEXTENDSIZEFOBL4L1, C.FTL4LORDERCNTY2, C.FTL4LCURRENCYFOB2, C.FNEXTENDSIZEFOBL4L2, "
            _Qry &= vbCrLf & "    C.FTL4LORDERCNTY3, C.FTL4LCURRENCYFOB3, C.FNEXTENDSIZEFOBL4L3, C.FTPRODUCTDEVELOPER, C.FTTeamName,C.FTStyleCode "
            _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With(NOLOCK) On C.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S With(NOLOCK) On C.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As M With(NOLOCK) On C.FNHSysMainMatId = M.FNHSysMainMatId "
            _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNo.Text) & "' "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim dttmp As DataTable = _oDt.Clone

            Try
                Me.ogcfabric.DataSource = _oDt.Select("FNCostType='1'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogcfabric.DataSource = dttmp.Copy
            End Try

            Try
                Me.ogctrims.DataSource = _oDt.Select("FNCostType='2'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogctrims.DataSource = dttmp.Copy
            End Try

            Try
                Me.ogcprocessmat.DataSource = _oDt.Select("FNCostType='3'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogcprocessmat.DataSource = dttmp.Copy
            End Try

            Try
                Me.ogcprocesslabor.DataSource = _oDt.Select("FNCostType='4'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogcprocesslabor.DataSource = dttmp.Copy
            End Try

            Try
                Me.ogcpack.DataSource = _oDt.Select("FNCostType='5'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogcpack.DataSource = dttmp.Copy
            End Try

            Try
                Me.ogccmp.DataSource = _oDt.Select("FNCostType='6'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogccmp.DataSource = dttmp.Copy
            End Try


            Try
                Me.ogcbemis.DataSource = _oDt.Select("FNCostType='7'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogcbemis.DataSource = dttmp.Copy
            End Try

            Try
                Me.ogcteamMulti.DataSource = _oDt.Select("FNCostType='8'", "FNSeq").CopyToDataTable
            Catch ex As Exception

                Me.ogcteamMulti.DataSource = dttmp.Copy
            End Try



            'For _CT As Integer = 1 To 8
            '    _Qry = "  Select C.* "
            '    _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK) LEFT OUTER JOIN"
            '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With(NOLOCK) On C.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
            '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S With(NOLOCK) On C.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
            '    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As M With(NOLOCK) On C.FNHSysMainMatId = M.FNHSysMainMatId "
            '    _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNo.Text) & "' AND FNCostType='" & _CT & "'"

            '    'fill gridview by datatable
            '    Select Case _CT
            '        Case 1
            '            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            '            Me.ogcfabric.DataSource = _oDt
            '        Case 2
            '            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            '            Me.ogctrims.DataSource = _oDt
            '        Case 3
            '            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            '            Me.ogcnosew.DataSource = _oDt
            '        Case 4
            '            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            '            Me.ogcpack.DataSource = _oDt
            '    End Select
            'Next
        Catch ex As Exception
        End Try

    End Sub

    Private Sub FNHSysCurId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCurId.EditValueChanged   'load exchange rate from DB via select currency
        If _FormLoad Then Exit Sub
        If FNHSysCurId.Text = "" Then
            FNExchangeRate.Value = 1
            Exit Sub
        End If
        If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTStateLocal FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK) WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "") = "1" Then

            FNExchangeRate.Properties.ReadOnly = True

            If Not (_ProcLoad) Then
                FNExchangeRate.Value = 1
            End If

        Else


            If Not (_ProcLoad) Then
                FNExchangeRate.Properties.ReadOnly = False
                FNExchangeRate.Value = 0
                Dim _Qry As String = ""

                _Qry = " SELECT TOP 1 FNSellingRate"
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTExchangeRate  WITH(NOLOCK)  "
                _Qry &= vbCrLf & "   WHERE  (FDDate = N'" & HI.UL.ULDate.ConvertEnDB(FDCostSheetDate.Text) & "')"
                _Qry &= vbCrLf & "   AND (FNHSysCurId IN ("
                _Qry &= vbCrLf & "  SELECT TOP 1 FNHSysCurId "
                _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE FTCurCode='" & HI.UL.ULF.rpQuoted(FNHSysCurId.Text) & "'"
                _Qry &= vbCrLf & "  ))"

                FNExchangeRate.Value = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT, "0")))

                If FNExchangeRate.Value <= 0 Then
                    FNExchangeRate.Value = 1
                End If

            End If

        End If
    End Sub

    Private Sub Cal_Detail(_var As Double, ByVal TableIndexx As Integer)
        Try
            Dim _TotalUse As Double
            Dim _Exten As Double
            Dim _ExRate As Double
            Dim _CIF As Double

            Dim _ogv As Object
            If TableIndexx = 0 Then
                _ogv = ogvfabric

            ElseIf TableIndexx = 1 Then
                _ogv = ogvtrims

            ElseIf TableIndexx = 2 Then
                _ogv = ogvnosew

            Else
                _ogv = ogvpack
            End If

            With _ogv
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _TotalUse = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNTotalUsed").ToString)
                _CIF = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCIF").ToString)
                _Exten = (_var + _CIF) * _TotalUse
                .SetRowCellValue(.FocusedRowHandle, "FNExten", _Exten)

                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", _Exten / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", _Exten / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", _Exten / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", _Exten / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Cal_Exten(_var As Double, ByVal TableIndexx As Integer)
        Try
            Dim _TotalUse As Double
            Dim _Exten As Double
            Dim _ExRate As Double
            Dim _CIF As Double
            Dim _CostPerPiece As Double

            Dim _ogv As Object
            If TableIndexx = 0 Then
                _ogv = ogvfabric

            ElseIf TableIndexx = 1 Then
                _ogv = ogvtrims

            ElseIf TableIndexx = 2 Then
                _ogv = ogvnosew

            Else
                _ogv = ogvpack
            End If

            With _ogv
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _TotalUse = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNTotalUsed").ToString)
                _CostPerPiece = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerPiece").ToString)
                _Exten = (_var + _CostPerPiece) * _TotalUse
                .SetRowCellValue(.FocusedRowHandle, "FNExten", _Exten)

                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", _Exten / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", _Exten / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", _Exten / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", _Exten / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTCostSheetNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTCostSheetNo.EditValueChanged
        Try

            Dim _Qry As String = ""
            Dim Dt As DataTable

            '_Qry = "  Select FNRevised ,FTCostSheetNo"
            '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
            '_Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
            'Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            'If (Dt.Rows.Count = 0 And FTCostSheetNo.Text <> "") Then
            '    FNRevised.Value = 0
            'End If

            Me.ogctrims.DataSource = Nothing
            Me.ogcnosew.DataSource = Nothing
            Me.ogcpack.DataSource = Nothing
            Call LoadDataInfo(Me.FTCostSheetNo.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTStateActive_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateActive.CheckedChanged
        If (FTStateActive.Checked.ToString() = "True") Then
            ActState = "1"
        Else
            ActState = "0"
        End If
    End Sub

    Private Sub Fabcal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FabRepositoryMarkUseNetSuage.EditValueChanging, FabRepositoryPerAllowCalcEdit.EditValueChanging,
                                                                                                                                        FabRepositoryCostNetPrice.EditValueChanging, FabRepositoryItemCalcFNHANDLINGCHARGEPERCENT.EditValueChanging, FabRepositoryItemCalcFNIMPORTDUTYPERCENT.EditValueChanging, FabRepositoryCifCalcEditCIF.EditValueChanging
        Try

            With Me.ogvfabric
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim _AllowPer As Decimal = 0
                    Dim _MarkerUse As Decimal = 0
                    Dim UnitPrice As Decimal = 0
                    Dim Handlingchangeper As Decimal = 0
                    Dim importdutyper As Decimal = 0
                    Dim CiF As Decimal = 0
                    Dim Handlingchange As Decimal = 0
                    Dim UsageCost As Decimal = 0
                    Dim TotalUsed As Decimal = 0
                    Dim ImportDyty As Decimal = 0


                    _AllowPer = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAllowancePer").ToString)
                    _MarkerUse = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNMarkerUsed").ToString)
                    UnitPrice = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerUOM").ToString)
                    CiF = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCIF").ToString)
                    Handlingchangeper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGEPERCENT").ToString)
                    importdutyper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNIMPORTDUTYPERCENT").ToString)



                    TotalUsed = _MarkerUse + Decimal.Parse(Format(((_MarkerUse * _AllowPer) / 100.0), "0.0000"))
                    UsageCost = Decimal.Parse(Format((UnitPrice + CiF) * TotalUsed, "0.0000"))
                    Handlingchange = Decimal.Parse(Format(((UsageCost * Handlingchangeper) / 100.0), "0.0000"))

                    ImportDyty = Decimal.Parse(Format((((UsageCost + Handlingchange) * importdutyper) / 100.0), "0.0000"))


                    .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", TotalUsed)
                    .SetRowCellValue(.FocusedRowHandle, "FNUSAGECOST", UsageCost)
                    .SetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGECOST", Handlingchange)
                    .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", ImportDyty)

                    Call SumAmt()

                Else
                    e.Cancel = True

                End If





            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Acccal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles TrimsFNMarkerUsed.EditValueChanging, TrimsFNAllowancePer.EditValueChanging,
                                                                                                                                        TrimsFNCostPerUOM.EditValueChanging, TrimsFNHANDLINGCHARGEPERCENT.EditValueChanging, TrimsFNIMPORTDUTYPERCENT.EditValueChanging, TrimsFNCIF.EditValueChanging
        Try

            With Me.ogvtrims
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim _AllowPer As Decimal = 0
                    Dim _MarkerUse As Decimal = 0
                    Dim UnitPrice As Decimal = 0
                    Dim Handlingchangeper As Decimal = 0
                    Dim importdutyper As Decimal = 0
                    Dim CiF As Decimal = 0
                    Dim Handlingchange As Decimal = 0
                    Dim UsageCost As Decimal = 0
                    Dim TotalUsed As Decimal = 0
                    Dim ImportDyty As Decimal = 0


                    _AllowPer = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAllowancePer").ToString)
                    _MarkerUse = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNMarkerUsed").ToString)
                    UnitPrice = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerUOM").ToString)
                    CiF = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCIF").ToString)
                    Handlingchangeper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGEPERCENT").ToString)
                    importdutyper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNIMPORTDUTYPERCENT").ToString)



                    TotalUsed = _MarkerUse + Decimal.Parse(Format(((_MarkerUse * _AllowPer) / 100.0), "0.0000"))
                    UsageCost = Decimal.Parse(Format((UnitPrice + CiF) * TotalUsed, "0.0000"))
                    Handlingchange = Decimal.Parse(Format(((UsageCost * Handlingchangeper) / 100.0), "0.0000"))

                    ImportDyty = Decimal.Parse(Format((((UsageCost + Handlingchange) * importdutyper) / 100.0), "0.0000"))


                    .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", TotalUsed)
                    .SetRowCellValue(.FocusedRowHandle, "FNUSAGECOST", UsageCost)
                    .SetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGECOST", Handlingchange)
                    .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", ImportDyty)


                    Call SumAmt()
                Else
                    e.Cancel = True

                End If




                'Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub PackBemis_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEditFNSLITTINGWIDTH.EditValueChanging

        Try
            With Me.ogvbemis
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim pFULLWIDTH As Decimal = 0
                    Dim pSLITTINGWIDTH As Decimal = 0
                    Dim pREQUIREDLENGTH As Decimal = 0
                    Dim pNETUSAGEINFULLWIDTH As Decimal = 0
                    Dim pPRICEINMETER As Decimal = 0
                    Dim pBEMISSLITTINGUPCHARGE As Decimal = 0
                    Dim pPRICEPERSLITTINGWITDH As Decimal = 0



                    pFULLWIDTH = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNFULLWIDTH").ToString)
                    pSLITTINGWIDTH = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNSLITTINGWIDTH").ToString)
                    pREQUIREDLENGTH = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNREQUIREDLENGTH").ToString)


                    pPRICEINMETER = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNPRICEINMETER").ToString)
                    pBEMISSLITTINGUPCHARGE = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNBEMISSLITTINGUPCHARGE").ToString)


                    pNETUSAGEINFULLWIDTH = Decimal.Parse(Format((((pREQUIREDLENGTH * pSLITTINGWIDTH) / pFULLWIDTH) / 100.0), "0.0000"))



                    pPRICEPERSLITTINGWITDH = Decimal.Parse(Format(((((pPRICEINMETER / pFULLWIDTH) * pSLITTINGWIDTH)) * (1.0 + Decimal.Parse(Format((pBEMISSLITTINGUPCHARGE / 100.0), "0.00")))), "0.0000"))


                    .SetRowCellValue(.FocusedRowHandle, "FNNETUSAGEINFULLWIDTH", pNETUSAGEINFULLWIDTH)
                    .SetRowCellValue(.FocusedRowHandle, "FNPRICEPERSLITTINGWITDH", pPRICEPERSLITTINGWITDH)


                    Call SumAmt()

                Else
                    e.Cancel = True

                End If


            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Sub Packcal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles PackFNMarkerUsed.EditValueChanging, PackFNAllowancePer.EditValueChanging,
                                                                                                                                        PackFNCostPerUOM.EditValueChanging, PackFNHANDLINGCHARGEPERCENT.EditValueChanging, PackFNIMPORTDUTYPERCENT.EditValueChanging, PackFNCIF.EditValueChanging
        Try

            With Me.ogvpack
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim _AllowPer As Decimal = 0
                    Dim _MarkerUse As Decimal = 0
                    Dim UnitPrice As Decimal = 0
                    Dim Handlingchangeper As Decimal = 0
                    Dim importdutyper As Decimal = 0
                    Dim CiF As Decimal = 0
                    Dim Handlingchange As Decimal = 0
                    Dim UsageCost As Decimal = 0
                    Dim TotalUsed As Decimal = 0
                    Dim ImportDyty As Decimal = 0


                    _AllowPer = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAllowancePer").ToString)
                    _MarkerUse = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNMarkerUsed").ToString)
                    UnitPrice = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerUOM").ToString)
                    CiF = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCIF").ToString)
                    Handlingchangeper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGEPERCENT").ToString)
                    importdutyper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNIMPORTDUTYPERCENT").ToString)



                    TotalUsed = _MarkerUse + Decimal.Parse(Format(((_MarkerUse * _AllowPer) / 100.0), "0.0000"))
                    UsageCost = Decimal.Parse(Format((UnitPrice + CiF) * TotalUsed, "0.0000"))
                    Handlingchange = Decimal.Parse(Format(((UsageCost * Handlingchangeper) / 100.0), "0.0000"))

                    ImportDyty = Decimal.Parse(Format((((UsageCost + Handlingchange) * importdutyper) / 100.0), "0.0000"))


                    .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", TotalUsed)
                    .SetRowCellValue(.FocusedRowHandle, "FNUSAGECOST", UsageCost)
                    .SetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGECOST", Handlingchange)
                    .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", ImportDyty)

                    Call SumAmt()

                Else
                    e.Cancel = True

                End If




                'Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Matcal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEditAllowanceMat.EditValueChanging, RepositoryItemCalcEditNetusageMat.EditValueChanging, RepositoryItemCalcEditpricemat.EditValueChanging
        Try

            With Me.ogvprocessmat
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim _AllowPer As Decimal = 0
                    Dim _MarkerUse As Decimal = 0
                    Dim UnitPrice As Decimal = 0

                    Dim UsageCost As Decimal = 0
                    Dim TotalUsed As Decimal = 0



                    _AllowPer = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAllowancePer").ToString)
                    _MarkerUse = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNMarkerUsed").ToString)
                    UnitPrice = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerUOM").ToString)




                    TotalUsed = _MarkerUse + Decimal.Parse(Format(((_MarkerUse * _AllowPer) / 100.0), "0.0000"))
                    UsageCost = Decimal.Parse(Format((UnitPrice) * TotalUsed, "0.0000"))


                    .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", TotalUsed)
                    .SetRowCellValue(.FocusedRowHandle, "FNUSAGECOST", UsageCost)

                    Call SumAmt()

                Else
                    e.Cancel = True

                End If




                'Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Laborcal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEditGrossusagelabor.EditValueChanging, RepositoryItemCalcEditFNCostPerUOMLabor.EditValueChanging
        Try

            With Me.ogvprocesslabor
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim _AllowPer As Decimal = 0
                    Dim _MarkerUse As Decimal = 0
                    Dim UnitPrice As Decimal = 0

                    Dim UsageCost As Decimal = 0
                    Dim TotalUsed As Decimal = 0



                    TotalUsed = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNTotalUsed").ToString)

                    UnitPrice = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerUOM").ToString)


                    UsageCost = Decimal.Parse(Format((UnitPrice) * TotalUsed, "0.0000"))


                    .SetRowCellValue(.FocusedRowHandle, "FNUSAGECOST", UsageCost)

                    Call SumAmt()

                Else
                    e.Cancel = True

                End If




                'Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub TeamMulti_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryItemCalcEditMultiFNCostPerUOM.EditValueChanging, RepositoryItemCalcEditMultiFNCIF.EditValueChanging,
                                                                                                                                        RepositoryItemCalcEditMultiFNUSAGECOST.EditValueChanging, RepositoryItemCalcEditMultiFNHANDLINGCHARGEPERCENT.EditValueChanging, RepositoryItemCalcEditMultiFNHANDLINGCHARGECOST.EditValueChanging
        Try

            With Me.ogvteamMulti
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If e.NewValue >= 0 Then
                    e.Cancel = False
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, e.NewValue)


                    Dim _AllowPer As Decimal = 0
                    Dim _MarkerUse As Decimal = 0
                    Dim UnitPrice As Decimal = 0
                    Dim Handlingchangeper As Decimal = 0
                    Dim importdutyper As Decimal = 0
                    Dim CiF As Decimal = 0
                    Dim Handlingchange As Decimal = 0
                    Dim UsageCost As Decimal = 0
                    Dim TotalUsed As Decimal = 0
                    Dim ImportDyty As Decimal = 0


                    _AllowPer = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAllowancePer").ToString)
                    _MarkerUse = 1.0 ' Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNMarkerUsed").ToString)
                    UnitPrice = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCostPerUOM").ToString)
                    CiF = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNCIF").ToString)
                    Handlingchangeper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGEPERCENT").ToString)
                    importdutyper = Decimal.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNIMPORTDUTYPERCENT").ToString)

                    Dim mPer As Decimal = 100.0

                    TotalUsed = _MarkerUse + Decimal.Parse(Format(((_MarkerUse * _AllowPer) / mPer), "0.0000"))
                    UsageCost = Decimal.Parse(Format((UnitPrice + CiF) * TotalUsed, "0.0000"))
                    Handlingchange = Decimal.Parse(Format(((UsageCost * Handlingchangeper) / mPer), "0.0000"))

                    ImportDyty = Decimal.Parse(Format((((UsageCost + Handlingchange))), "0.0000"))

                    .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", TotalUsed)
                    .SetRowCellValue(.FocusedRowHandle, "FNUSAGECOST", UsageCost)
                    .SetRowCellValue(.FocusedRowHandle, "FNHANDLINGCHARGECOST", Handlingchange)
                    .SetRowCellValue(.FocusedRowHandle, "FNTOTALTRIMPROCESSCOST", ImportDyty)

                Else
                    e.Cancel = True

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryMarkUseCalcEdit_S_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryMarkUseCalcEdit_S.EditValueChanging
        Try
            Dim _AllowPer As Double
            With Me.ogvnosew
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _AllowPer = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNAllowancePer").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", (e.NewValue) + (_AllowPer * Double.Parse(e.NewValue)) / 100)
                'Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositorPerAllowCalcEdit_S_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositorPerAllowCalcEdit_S.EditValueChanging
        Try
            Dim _MarkerUse As Double
            With Me.ogvnosew
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _MarkerUse = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNMarkerUsed").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNTotalUsed", _MarkerUse + (_MarkerUse * Double.Parse(e.NewValue)) / 100)
                'Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub RepositoryYardCalcEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryYardCalcEdit.EditValueChanging
        Try
            Cal_Detail(Double.Parse(e.NewValue), TabIndexs.FabricDetail)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryYardCalcEdit_S_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryYardCalcEdit_S.EditValueChanging
        Try
            Cal_Detail(Double.Parse(e.NewValue), TabIndexs.NoSewDetail)
        Catch ex As Exception
        End Try
    End Sub



    Private Sub RepositoryCifCalcEdit_S_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryCifCalcEdit_S.EditValueChanging
        Call Cal_Exten(Double.Parse(e.NewValue), TabIndexs.NoSewDetail)
    End Sub



    Private Sub RepositoryChargePerCalcEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryChargePerCalcEdit.EditValueChanging
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ExRate As Double
            Dim _Cur As String = ""
            With Me.ogvfabric
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _Charge = _Exten * Double.Parse(e.NewValue) / 100
                .SetRowCellValue(.FocusedRowHandle, "FNNetExten", _Charge)
                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge) / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RepositoryChargePerCalcEdit_C_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ExRate As Double
            Dim _Cur As String = ""
            With Me.ogvtrims
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _Charge = _Exten * Double.Parse(e.NewValue) / 100
                .SetRowCellValue(.FocusedRowHandle, "FNNetExten", _Charge)
                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge) / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryChargePerCalcEdit_S_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryChargePerCalcEdit_S.EditValueChanging
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ExRate As Double
            Dim _Cur As String = ""
            With Me.ogvnosew
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _Charge = _Exten * Double.Parse(e.NewValue) / 100
                .SetRowCellValue(.FocusedRowHandle, "FNNetExten", _Charge)
                _ExRate = FNExchangeRate.Value

                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge) / _ExRate)
                End Select

                Call SumAmt()

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryChargePerCalcEdit_D_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ExRate As Double
            Dim _Cur As String = ""
            With Me.ogvpack
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _Charge = _Exten * Double.Parse(e.NewValue) / 100
                .SetRowCellValue(.FocusedRowHandle, "FNNetExten", _Charge)
                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge) / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub



    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        If Me.FTCostSheetNo.Properties.Tag.ToString().Trim() <> "" Then

            _CopyCostSheet = New wCopyCostSheet(Me.FTCostSheetNo.Properties.Tag.ToString().Trim(), Me.SysDBName, Me.SysTableName, Me.FNRevised.Value)

            HI.TL.HandlerControl.AddHandlerObj(_CopyCostSheet)

            Dim oSysLang As New HI.ST.SysLanguage

            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyCostSheet.Name.ToString.Trim, _CopyCostSheet)
            Catch ex As Exception
            End Try

            Call HI.ST.Lang.SP_SETxLanguage(_CopyCostSheet)

            With _CopyCostSheet
                .StateProcess = False
                .ShowDialog()
                If .StateProcess Then
                    FTCostSheetNo.Text = .FTCostSheetNo.Text
                End If

            End With


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTCostSheetNo_lbl.Text)
            Me.FTCostSheetNo.Focus()
        End If
    End Sub


    Private Sub ocmedit_Click(sender As Object, e As EventArgs) Handles ocmrevised.Click     'revised cost sheet
        If CheckOwner() = False Then Exit Sub

        '  If Me.VerrifyData Then
        Dim _Qry As String = ""
        Dim Dt As DataTable

        _Qry = "  Select FNRevised ,FTCostSheetNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
        Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

        If Dt.Rows.Count > 0 Then
            '_Revise = True
            FNRevised.Value = Dt.Rows.Count
        End If

        If Me.SaveData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Call SaveData()
            Call LoadDataInfo2(FTCostSheetNo.Text)
            '  _Revise = False
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
    End Sub

    Private Sub FNRevised_EditValueChanged(sender As Object, e As EventArgs) Handles FNRevised.EditValueChanged
        Try
            ' If Not _Revise Then FTCostSheetNo.Text = "" 'Call LoadDataInfo(Me.FTCostSheetNo.Text)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvfabric_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvfabric.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogcfabric.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                                x += +1
                                r!FNSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
                    SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvtrims_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogctrims.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                                x += +1
                                r!FNSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
                    SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvnosew_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvnosew.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogcnosew.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                                x += +1
                                r!FNSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
                    SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvpack_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            Select Case e.KeyCode
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogcpack.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                                x += +1
                                r!FNSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
                    SumAmt()
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmbomdeleterow_Click(sender As Object, e As EventArgs) Handles ocmbomdeleterow.Click
        'Try
        '    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        '        .DeleteRow(.FocusedRowHandle)
        '        With CType(ogcnosew.DataSource, DataTable)
        '            .AcceptChanges()
        '            Dim x As Integer = 0
        '            For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
        '                x += +1
        '                r!FNSeq = x
        '            Next
        '            .AcceptChanges()
        '        End With
        '    End With
        '    SumAmt()
        'Catch ex As Exception
        'End Try

        Try

            Dim dataTable As DataTable
            Dim _ogc As Object
            Dim _ct As Integer = 0

            Select Case otb.SelectedTabPageIndex
                Case 0

                    With CType(ogcfabric.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(Me.ogcfabric.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With


                Case 1

                    With CType(ogctrims.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With


                    With CType(Me.ogctrims.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With



                Case 2

                    With CType(ogcprocessmat.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With


                    With CType(Me.ogcprocessmat.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With


                Case 3

                    With CType(ogcprocesslabor.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(Me.ogcprocesslabor.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With


                Case 4

                    With CType(ogcpack.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(Me.ogcpack.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With


                Case 5

                    With CType(ogccmp.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(Me.ogccmp.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With

                Case 6

                    With CType(ogcbemis.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(Me.ogcbemis.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With


                Case 7


                    With CType(ogcteamMulti.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                    End With

                    With CType(Me.ogcteamMulti.DataSource, DataTable)
                        .AcceptChanges()
                        Dim x As Integer = 0
                        For Each r As DataRow In .Select("FNSeq<>0", "FNSeq")
                            x += +1
                            r!FNSeq = x
                        Next
                        .AcceptChanges()
                    End With


            End Select

            SumAmt()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        'ExportExcel()
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            'Op.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
            'Op.Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx"

            Op.Filter = "Excel Files|*.xlsm"
            'Op.Filter = "Excel Files|*.xlsx"
            Op.FileName = FNHSysSeasonId.Text & "-" & FNHSysVenderPramId.Text & "-" & FNHSysStyleId.Text & "-" & FNISTeamMulti.Text & "-" & FNCostSheetColor.Text & "-" & FNCostSheetSize.Text & "-" & FNCostSheetBuyType.Text & "-" & FNVersion.Text
            Op.ShowDialog()

            Try
                If Op.FileName <> "" Then
                    _FileName = Op.FileName.ToString

                    'ExportExcel()
                    Dim _Spls As New HI.TL.SplashScreen("Prepare Data For Calculate.. Please Wait ")
                    Try
                        ' ExportExcelSprettSheet2(_Spls)

                        ExportExcel(_Spls)
                    Catch ex As Exception
                        Try
                            _Spls.Close()
                        Catch ex2 As Exception

                        End Try

                    End Try


                End If

            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepositoryImportDutyCalcEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryImportDutyCalcEdit.EditValueChanging
        Dim _Exten As Double
        Dim _Charge As Double
        Dim _ImD As Double
        Dim _ExRate As Double
        With Me.ogvfabric
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
            _ImD = Double.Parse(e.NewValue)
            _Charge = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNNetExten").ToString)
            .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", _ImD)

            _ExRate = FNExchangeRate.Value
            Select Case FNHSysCurId.Text
                Case "JPY"
                    .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                Case "THB"
                    .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                Case "CNY"
                    .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                Case "MYR"
                    .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
            End Select
            Call SumAmt()
        End With
    End Sub

    Private Sub RepositoryImportDutyCalcEdit_C_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ImD As Double
            Dim _ExRate As Double
            With Me.ogvtrims
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _ImD = Double.Parse(e.NewValue)
                _Charge = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNNetExten").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", _ImD)

                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryImportDutyCalcEdit_S_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepositoryImportDutyCalcEdit_S.EditValueChanging
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ImD As Double
            Dim _ExRate As Double
            With Me.ogvnosew
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _ImD = Double.Parse(e.NewValue)
                _Charge = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNNetExten").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", _ImD)

                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryImportDutyCalcEdit_D_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            Dim _Exten As Double
            Dim _Charge As Double
            Dim _ImD As Double
            Dim _ExRate As Double
            With Me.ogvpack
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _Exten = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNExten").ToString)
                _ImD = Double.Parse(e.NewValue)
                _Charge = Double.Parse("0" & .GetRowCellValue(.FocusedRowHandle, "FNNetExten").ToString)
                .SetRowCellValue(.FocusedRowHandle, "FNImportDuty", _ImD)

                _ExRate = FNExchangeRate.Value
                Select Case FNHSysCurId.Text
                    Case "JPY"
                        .SetRowCellValue(.FocusedRowHandle, "FNJapanOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "THB"
                        .SetRowCellValue(.FocusedRowHandle, "FNThailandOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "CNY"
                        .SetRowCellValue(.FocusedRowHandle, "FNChinaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                    Case "MYR"
                        .SetRowCellValue(.FocusedRowHandle, "FNMalaysiaOrderCost", (_Exten + _Charge + _ImD) / _ExRate)
                End Select
                Call SumAmt()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FilePath = .FileName
                    Call Readfile()
                Else
                    _FilePath = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Readfile()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Select Case _FileType.ToUpper
                    Case ".XLSX".ToUpper, ".XLS".ToUpper
                        Call _ExcelViewer(_FilePath)
                    Case Else
                        HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
                        Exit Sub
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _ExcelViewer(_FileName As String)
        Try
            'Me.oGrpdetail.Controls.Clear()
            'Dim _Excel As New DevExpress.XtraSpreadsheet.SpreadsheetControl
            ExcelAttach.LoadDocument(_FileName)
            ExcelAttach.ReadOnly = True
            '_Excel.Dock = DockStyle.Fill
            '_Excel.LoadDocument(_FileName)
            '_Excel.ReadOnly = True
            'Me.oGrpdetail.Controls.Add(_Excel)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTLOProductDeveloper_EditValueChanged(sender As Object, e As EventArgs) Handles FTLOProductDeveloper.EditValueChanged

    End Sub

    Private Sub FNL4Country1Cur_EditValueChanged(sender As Object, e As EventArgs) Handles FNL4Country1Cur.EditValueChanged

    End Sub

    Private Sub FNL4Country1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNL4Country1.SelectedIndexChanged

        FNL4Country1Exc.Value = 0
        FNL4Country1Finalm.Value = 0
        FNL4Country1Extendedm.Value = 0

        Select Case FNL4Country1.SelectedIndex
            Case 1
                FNL4Country1Cur.Text = "CNY"
            Case 2
                FNL4Country1Cur.Text = "MYR"
            Case 3
                FNL4Country1Cur.Text = "THB"
            Case 4
                FNL4Country1Cur.Text = "JPY"
            Case 5
                FNL4Country1Cur.Text = "IDR"
            Case Else
                FNL4Country1Cur.Text = ""

        End Select


    End Sub

    Private Sub FNL4Country2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNL4Country2.SelectedIndexChanged



        FNL4Country2Exc.Value = 0
        FNL4Country2Finalm.Value = 0
        FNL4Country2Extendedm.Value = 0

        Select Case FNL4Country2.SelectedIndex
            Case 1
                FNL4Country2Cur.Text = "CNY"
            Case 2
                FNL4Country2Cur.Text = "MYR"
            Case 3
                FNL4Country2Cur.Text = "THB"
            Case 4
                FNL4Country2Cur.Text = "JPY"
            Case 5
                FNL4Country2Cur.Text = "IDR"
            Case Else
                FNL4Country2Cur.Text = ""

        End Select

    End Sub

    Private Sub FNL4Country3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNL4Country3.SelectedIndexChanged

        FNL4Country3Exc.Value = 0
        FNL4Country3Finalm.Value = 0
        FNL4Country3Extendedm.Value = 0

        Select Case FNL4Country3.SelectedIndex
            Case 1
                FNL4Country3Cur.Text = "USD"
            Case Else
                FNL4Country3Cur.Text = ""

        End Select
    End Sub

    Private Sub FNISTeamMulti_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNISTeamMulti.SelectedIndexChanged
        otpTeamMulti.PageVisible = (FNISTeamMulti.SelectedIndex = 1)
    End Sub

    Private Sub FNHSysStyleIdTo_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleIdTo.EditValueChanged

    End Sub

    Private Sub LoadMaster()
        Dim cmd As String = ""
        Dim dt As DataTable

        cmd = "SELECT FTStyleCode, FTStyleNameEN AS FTStyleName from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStateActive='1'  Order by FTStyleCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)
        RepositoryItemGridLookUpEditFTStyleCode.DataSource = dt.Copy

        'cmd = "SELECT   FTCusItemCodeRef AS FTMainMatCode, Max(FNHSysMainMatId) AS FNHSysMainMatId, MAX(LEFT(FTMainMatNameEN,200)) AS FTMainMatName from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat WITH(NOLOCK) WHERE FTStateActive='1' AND ISNULL(FTCusItemCodeRef,'') <>'' group by FTCusItemCodeRef Order by FTCusItemCodeRef "

        cmd = " SELECT A.* FROM ( SELECT   FTMat AS FTMainMatCode, 0 AS FNHSysMainMatId, MAX(LEFT(FTMaterialDescription,200)) AS FTMainMatName,FTSupplierLocationCode AS FTSuplCode,FTLiaisonOfficeCode As FTCOFO,FTRMDSSESNCD AS FTSeason,(FTMatColor) AS FTMatColor "
        cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile WITH(NOLOCK) "
        cmd &= vbCrLf & " group by FTMat,FTSupplierLocationCode,FTLiaisonOfficeCode,FTRMDSSESNCD ,FTMatColor"
        cmd &= vbCrLf & " UNION "
        cmd &= vbCrLf & " SELECT   M.FTCusItemCodeRef AS FTMainMatCode, Max(M.FNHSysMainMatId) AS FNHSysMainMatId, MAX(LEFT(M.FTMainMatNameEN,200)) AS FTMainMatName,ISNULL(S.FTNikeVenderCode,'') AS FTSuplCode,ISNULL(S.FTCOFOCode,'') AS FTCOFO,'' ,'' AS FTMatColor"
        cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS M WITH(NOLOCK) "
        cmd &= vbCrLf & "  OUTER APPLY (SELECT TOP 1  S.FTNikeVenderCode,SCOFO.FTCOFOCode   "
        cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) "
        cmd &= vbCrLf & "   LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMCOFO AS SCOFO WITH(NOLOCK) ON S.FNHSysCOFOId =SCOFO.FNHSysCOFOId "
        cmd &= vbCrLf & "   WHERE S.FNHSysSuplId = M.FNHSysSuplId  "
        cmd &= vbCrLf & "   ) AS S "
        cmd &= vbCrLf & "  WHERE M.FTStateActive='1' AND ISNULL(M.FTCusItemCodeRef,'') <>'' AND M.FNDataMatType IN (1,2) "
        cmd &= vbCrLf & "  group by M.FTCusItemCodeRef,ISNULL(S.FTNikeVenderCode,'') ,ISNULL(S.FTCOFOCode,'') "
        cmd &= vbCrLf & " ) As A ORDER BY FTMainMatCode ,FTSuplCode,FTCOFO "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditFTMainMatCode.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTMainMatCodeTrim.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTMainMatCodePacking.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTMainMatCodeBemis.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTMainMatCodeProcessMat.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTMainMatCodeLabor.DataSource = dt.Copy

        RepositoryItemGridLookUpEditItemMulti.DataSource = dt.Copy


        RepositoryItemGridLookUpEditFTMainMatCode.PopupFormSize = New Size(500, 850)
        RepositoryItemGridLookUpEditFTMainMatCodeTrim.PopupFormSize = New Size(500, 850)
        RepositoryItemGridLookUpEditFTMainMatCodePacking.PopupFormSize = New Size(500, 850)
        RepositoryItemGridLookUpEditFTMainMatCodeBemis.PopupFormSize = New Size(500, 850)
        RepositoryItemGridLookUpEditFTMainMatCodeProcessMat.PopupFormSize = New Size(500, 850)
        RepositoryItemGridLookUpEditFTMainMatCodeLabor.PopupFormSize = New Size(500, 850)


        RepositoryItemGridLookUpEditItemMulti.PopupFormSize = New Size(500, 850)


        'cmd = "SELECT   A.FTNikeVenderCode AS FTSuplCode, MAX(A.FNHSysSuplId) AS FNHSysSuplId, MAX(A.FTSuplNameEN) AS FTSuplName "
        'cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS A WITH(NOLOCK)"
        'cmd &= vbCrLf & "  Outer Apply()  "
        'cmd &= vbCrLf & "  WHERE A.FTStateActive='1' AND ISNULL(A.FTNikeVenderCode,'') <>''  "
        'cmd &= vbCrLf & "  group by  A.FTNikeVenderCode "
        'cmd &= vbCrLf & "  Order  by A.FTNikeVenderCode "

        cmd = "SELECT   FTSupplierLocationCode AS FTSuplCode,FTLiaisonOfficeCode As FTCOFO,0 AS FNHSysSuplId,MAX(FTSupplierLocationName) as FTSuplName "
        cmd &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.THITRMDSMasterFile WITH(NOLOCK) "
        cmd &= vbCrLf & " group by FTSupplierLocationCode,FTLiaisonOfficeCode "
        cmd &= vbCrLf & " ORDER BY FTSupplierLocationCode,FTLiaisonOfficeCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditFTSuplCode.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTSuplCodeTrim.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTSuplCodeProcessMat.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTSuplCodeLabor.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTSuplCodePack.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTSuplCodeMulti.DataSource = dt.Copy

        cmd = "SELECT Lower(FTUnitCode) AS FTUnitCode, FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) WHERE FTStateActive='1'  Order by FTUnitCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpFTUnitCode.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTUnitCodeTrim.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTUnitCodeProcessMat.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTUnitCodeWidthunitPack.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTUnitCodeLabor.DataSource = dt.Copy

        cmd = "SELECT FTUnitCode AS FTUnitCode, FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) WHERE FTStateActive='1'  Order by FTUnitCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)
        RepositoryItemGridLookUpEditUOMWidth.DataSource = dt.Copy
        RepositoryItemGridLookUpEditUOMWidthtrim.DataSource = dt.Copy
        RepositoryItemGridLookUpEditUOMWidthPack.DataSource = dt.Copy

        cmd = "SELECT  FNHSysCOFOId, FTCOFOCode AS FTCOFO,FTCOFONameEN As FTCOFOName,  FTStateActive, FNFabricPer, FNAccPer, FNImportDuty,FNImportDutyAcc,FNHandlingChandeFab,FNHandlingChandeAcc from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMCOFO WITH(NOLOCK) WHERE FTStateActive='1'  Order by FTCOFOCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditCFCO.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTCOFOLabor.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTCOFOProcessMat.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTCOFOTrim.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTCOFOPack.DataSource = dt.Copy



        cmd = "SELECT  FNHSysProcessMatId, FTProcessMatCode,  FTStateActive, FTProcessMatNameEN, FTDefualtItem from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProcessMaterial WITH(NOLOCK) WHERE FTStateActive='1'  Order by FTProcessMatCode "

        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)

        RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMat.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTPROCESSSUBTYPELabor.DataSource = dt.Copy
        RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMulti.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub RepositoryItemGridLookUpEditFTMainMatCode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemGridLookUpEditFTMainMatCode.EditValueChanging
        Try

        Catch ex As Exception

        End Try
    End Sub


    Private Sub SetControl()



        'RepositoryItemGridLookUpEditFTMainMatCode.DisplayMember = "FTMainMatCode"
        'RepositoryItemGridLookUpEditFTMainMatCode.ValueMember = "FTMainMatCode"


        'RepositoryItemGridLookUpEditFTMainMatCodeTrim.DisplayMember = "FTMainMatCode"
        'RepositoryItemGridLookUpEditFTMainMatCodeTrim.ValueMember = "FTMainMatCode"



        'RepositoryItemGridLookUpEditFTMainMatCodePacking.DisplayMember = "FTMainMatCode"
        'RepositoryItemGridLookUpEditFTMainMatCodePacking.ValueMember = "FTMainMatCode"


        'RepositoryItemGridLookUpEditFTMainMatCodeBemis.DisplayMember = "FTMainMatCode"
        'RepositoryItemGridLookUpEditFTMainMatCodeBemis.ValueMember = "FTMainMatCode"


        'RepositoryItemGridLookUpEditFTMainMatCodeProcessMat.DisplayMember = "FTMainMatCode"
        'RepositoryItemGridLookUpEditFTMainMatCodeProcessMat.ValueMember = "FTMainMatCode"


        'RepositoryItemGridLookUpEditFTMainMatCodeLabor.DisplayMember = "FTMainMatCode"
        'RepositoryItemGridLookUpEditFTMainMatCodeLabor.ValueMember = "FTMainMatCode"


    End Sub

    Private Sub RepositoryItemGridLookUpEditFTMainMatCode_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditFTMainMatCode.EditValueChanged, RepositoryItemGridLookUpEditFTMainMatCodeTrim.EditValueChanged, RepositoryItemGridLookUpEditFTMainMatCodePacking.EditValueChanged, RepositoryItemGridLookUpEditItemMulti.EditValueChanged
        Try
            Dim MatCode As String = ""
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim ProdId As Integer = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysMainMatId").ToString())
                MatCode = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMainMatCode").ToString())

                Dim pCOFO As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCOFO").ToString

                Dim RMatColor As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMatColor").ToString
                Dim RSeason As String = ""
                '  .SetFocusedRowCellValue("FTMainMatCode", MatCode)
                .SetFocusedRowCellValue("FNHSysMainMatId", ProdId)
                .SetFocusedRowCellValue("FTMainMatName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMainMatName").ToString)
                .SetFocusedRowCellValue("FTSuplCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplCode").ToString)
                .SetFocusedRowCellValue("TTLG", pCOFO)
                .SetFocusedRowCellValue("FTMainMatCode", MatCode)



                .SetFocusedRowCellValue("FTMainMatColorCode", RMatColor)



                Try
                    RSeason = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSeason").ToString
                    .SetFocusedRowCellValue("FTRMDSSeason", RSeason)
                Catch ex As Exception

                End Try


                If RSeason = "" Then
                    RSeason = FNHSysSeasonId.Text.Trim
                End If
                Dim Ptice As Decimal = 0
                Dim PriceStatus As String = ""
                Dim dtPrice As DataTable

                Dim cmdstring As String = "select top 2 CASE WHEN SRFP.FTSupplierLocationName LIKE N'%Vilene%' THEN SRFP.FNUSDLY  ELSE SRFP.FNSTDPrice END AS FNSTDPrice,FTSMStatus    from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.THITRMDSMasterFile AS SRFP WITH(NOLOCK) WHERE  SRFP.FTMat ='" & HI.UL.ULF.rpQuoted(MatCode) & "' AND SRFP.FTRMDSSESNCD='" & HI.UL.ULF.rpQuoted(RSeason) & "' AND (FTMatColor ='' OR FTMatColor='" & HI.UL.ULF.rpQuoted(RMatColor) & "' )  ORDER BY FTMatColor DESC "

                dtPrice = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                For Each R As DataRow In dtPrice.Rows
                    Ptice = Val(R!FNSTDPrice.ToString)
                    PriceStatus = Microsoft.VisualBasic.Left(R!FTSMStatus.ToString, 1)


                    Exit For
                Next

                dtPrice.Dispose()


                .SetFocusedRowCellValue("FNCostPerUOM", Ptice)
                .SetFocusedRowCellValue("FNRMDSStatus", PriceStatus)
                Select Case .Name
                    Case "ogvfabric"

                        Call Fabcal_EditValueChanging(FabRepositoryCostNetPrice, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                        Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUpEditCFCO, New EventArgs)

                        Dim RMDSItemDesc As String = ""
                        cmdstring = "select top 1 SRFP.FTMaterialDescription from  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.THITRMDSMasterFile AS SRFP WITH(NOLOCK) WHERE  SRFP.FTMat ='" & HI.UL.ULF.rpQuoted(MatCode) & "' AND SRFP.FTRMDSSESNCD='" & HI.UL.ULF.rpQuoted(FNHSysSeasonId.Text.Trim) & "' "

                        RMDSItemDesc = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "")


                        If RMDSItemDesc.IndexOf("Wt (g/m2)") > 0 Then

                            Dim strWeight As String = Mid(RMDSItemDesc, RMDSItemDesc.IndexOf("Wt (g/m2)"), 25)


                            If strWeight.Split(":").Length = 2 Then
                                strWeight = strWeight.Split(":")(1)

                                If strWeight.Split(";").Length = 2 Then
                                    strWeight = strWeight.Split(";")(0).Trim()

                                    .SetFocusedRowCellValue("FNWeight", strWeight)
                                End If

                            End If

                        End If

                        If RMDSItemDesc.IndexOf("W (cm)") > 0 Then
                            Dim strWidth As String = Mid(RMDSItemDesc, RMDSItemDesc.IndexOf("W (cm)"), 20)

                            If strWidth.Split(":").Length = 2 Then
                                strWidth = strWidth.Split(":")(1)

                                If strWidth.Split(";").Length = 2 Then
                                    strWidth = strWidth.Split(";")(0).Trim()

                                    .SetFocusedRowCellValue("FNWidth", strWidth)
                                End If

                            End If


                        End If

                    Case "ogvtrims"

                        Call Acccal_EditValueChanging(TrimsFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                        Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUpEditFTCOFOTrim, New EventArgs)

                    Case "ogcpack"

                        Call Packcal_EditValueChanging(PackFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                        Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUpEditFTCOFOPack, New EventArgs)
                    Case "ogvteamMulti"
                        'Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUp, New EventArgs)
                End Select


                If pCOFO <> "" Then

                    cmdstring = "SELECT  FNHSysCOFOId, FTCOFOCode AS FTCOFO,FTCOFONameEN As FTCOFOName,  FTStateActive, FNFabricPer, FNAccPer, FNImportDuty,FNImportDutyAcc,FNHandlingChandeFab,FNHandlingChandeAcc "
                    cmdstring &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMCOFO WITH(NOLOCK) "
                    cmdstring &= vbCrLf & " WHERE FTCOFOCode='" & HI.UL.ULF.rpQuoted(pCOFO) & "' "
                    cmdstring &= vbCrLf & "  Order by FTCOFOCode "

                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

                    For Each Rm As DataRow In dt.Rows

                        Select Case .Name
                            Case "ogvfabric"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNFabricPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeFab.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDuty.ToString)

                                Call Fabcal_EditValueChanging(FabRepositoryCostNetPrice, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))

                            Case "ogvtrims"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNAccPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeAcc.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDutyAcc.ToString)

                                Call Acccal_EditValueChanging(TrimsFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))

                            Case "ogvpack"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNAccPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeAcc.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDutyAcc.ToString)

                                Call Packcal_EditValueChanging(PackFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                            Case "ogvteamMulti"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNAccPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeAcc.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDutyAcc.ToString)
                        End Select

                    Next

                    dt.Dispose()

                End If

                Try
                    obj.Properties.View.ClearColumnsFilter()
                Catch ex As Exception

                End Try

            End With

            CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).SetFocusedRowCellValue("FTMainMatCode", MatCode)
            'Dim sMatCode22 As String = CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("FTMainMatCode").ToString
            CType(sender.Parent.DataSource, DataTable).AcceptChanges()
            'Dim sMatCode As String = CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("FTMainMatCode").ToString
            'Dim MMs As String = ""
            Call SumAmt()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMat_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMat.EditValueChanged, RepositoryItemGridLookUpEditFTPROCESSSUBTYPELabor.EditValueChanged, RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMulti.EditValueChanged
        Try

            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Select Case .Name
                    Case "ogvteamMulti"
                    Case Else
                        .SetFocusedRowCellValue("FNHSysMainMatId", 0)

                        .SetFocusedRowCellValue("FTMainMatCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDefualtItem").ToString)
                        .SetFocusedRowCellValue("FTMainMatName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTProcessMatNameEN").ToString)
                End Select
                'If sender.name.ToString.ToLower = "RepositoryItemGridLookUpEditFTPROCESSSUBTYPEMulti".ToLower Then

                'Else
                '    .SetFocusedRowCellValue("FNHSysMainMatId", 0)

                '    .SetFocusedRowCellValue("FTMainMatCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDefualtItem").ToString)
                '    .SetFocusedRowCellValue("FTMainMatName", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTProcessMatNameEN").ToString)
                'End If


                Try
                    obj.Properties.View.ClearColumnsFilter()
                Catch ex As Exception

                End Try

            End With

            CType(sender.Parent.DataSource, DataTable).AcceptChanges()
            Call SumAmt()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditCFCO_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditCFCO.EditValueChanged, RepositoryItemGridLookUpEditFTCOFOLabor.EditValueChanged, RepositoryItemGridLookUpEditFTCOFOProcessMat.EditValueChanged, RepositoryItemGridLookUpEditFTCOFOTrim.EditValueChanged, RepositoryItemGridLookUpEditFTCOFOPack.EditValueChanged
        Try
            Dim MatCode As String = ""
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                MatCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCOFO").ToString

                Select Case .Name
                    Case "ogvfabric"

                        .SetFocusedRowCellValue("FNCIF", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNFabricPer").ToString)
                        .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHandlingChandeFab").ToString)
                        .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNImportDuty").ToString)

                        Call Fabcal_EditValueChanging(FabRepositoryCostNetPrice, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))

                    Case "ogvtrims"

                        .SetFocusedRowCellValue("FNCIF", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNAccPer").ToString)
                        .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHandlingChandeAcc").ToString)
                        .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNImportDutyAcc").ToString)

                        Call Acccal_EditValueChanging(TrimsFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))

                    Case "ogvpack"

                        .SetFocusedRowCellValue("FNCIF", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNAccPer").ToString)
                        .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHandlingChandeAcc").ToString)
                        .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNImportDutyAcc").ToString)

                        Call Packcal_EditValueChanging(PackFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                    Case "ogvteamMulti"

                        .SetFocusedRowCellValue("FNCIF", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNAccPer").ToString)
                        '.SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHandlingChandeAcc").ToString)
                        '.SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNImportDutyAcc").ToString)

                        ' Call Packcal_EditValueChanging(PackFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                End Select

                Try
                    obj.Properties.View.ClearColumnsFilter()
                Catch ex As Exception
                End Try

            End With

            CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).SetFocusedRowCellValue("TTLG", MatCode)
            CType(sender.Parent.DataSource, DataTable).AcceptChanges()

            Call SumAmt()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub FNCostSheetColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNCostSheetColor.SelectedIndexChanged

    End Sub

    Private Sub FNCostSheetBuyType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNCostSheetBuyType.SelectedIndexChanged

    End Sub

    Private Sub FNCostSheetSampleRound_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNCostSheetSampleRound.SelectedIndexChanged

    End Sub

    Private Sub FNChargeFabAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNChargeFabAmt.EditValueChanged

    End Sub

    Private Sub FNL4LTotalTrim_EditValueChanged(sender As Object, e As EventArgs) Handles FNL4LTotalTrim.EditValueChanged

    End Sub

    Private Sub FNL4LChargeFabric_EditValueChanged(sender As Object, e As EventArgs) Handles FNL4LChargeFabric.EditValueChanged

    End Sub

    Private Sub ogctrims_Click(sender As Object, e As EventArgs) Handles ogctrims.Click

    End Sub

    Private Sub ogcprocessmat_Click(sender As Object, e As EventArgs) Handles ogcprocessmat.Click

    End Sub

    Private Sub ogcprocesslabor_Click(sender As Object, e As EventArgs) Handles ogcprocesslabor.Click

    End Sub

    Private Sub ogvfabric_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvfabric.CustomColumnDisplayText, ogvtrims.CustomColumnDisplayText, ogvprocessmat.CustomColumnDisplayText, ogvprocesslabor.CustomColumnDisplayText, ogvbemis.CustomColumnDisplayText, ogvpack.CustomColumnDisplayText, ogvcmp.CustomColumnDisplayText, ogvteamMulti.CustomColumnDisplayText

        Try

            Select Case e.Column.FieldName
                Case "FNMarkerEff", "FNAllowancePer"
                    e.DisplayText = Format(Val(e.Value), "0.00") & " %"
                Case "FNHANDLINGCHARGEPERCENT", "FNIMPORTDUTYPERCENT", "FNEFFICIENCYPERCENT", "FNPROFITPERCENT"

                    If Val(e.Value) > 0 Then
                        e.DisplayText = Format(Val(e.Value), "0.00") & " %"
                    Else
                        e.DisplayText = ""
                    End If
                Case "FNWidth"
                    If Val(e.Value) > 0 Then

                    Else
                        e.DisplayText = ""
                    End If

                Case "FNBEMISSLITTINGUPCHARGE"
                    e.DisplayText = Format(Val(e.Value), "0.00") & " %"
                Case "FNCostPerUOM"

                    Try
                        With CType(sender, GridView)
                            If .Name = "ogvcmp" Then
                                If Val(e.Value) > 0 Then

                                Else
                                    e.DisplayText = ""
                                End If
                            End If
                        End With
                    Catch ex As Exception

                    End Try
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysSeasonId.EditValueChanged

    End Sub

    Private Sub ogcfabric_Click(sender As Object, e As EventArgs) Handles ogcfabric.Click

    End Sub

    Private Sub ogcbemis_Click(sender As Object, e As EventArgs) Handles ogcbemis.Click

    End Sub

    Private Sub FNExtendedPer_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles FNExtendedPer.CustomDisplayText
        e.DisplayText = e.DisplayText + " %"
    End Sub

    Private Sub FNTrinUsageAllowPer_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles FNTrinUsageAllowPer.CustomDisplayText
        e.DisplayText = e.DisplayText + " %"
    End Sub

    Private Sub FNL4Country1Exc_EditValueChanged(sender As Object, e As EventArgs) Handles FNL4Country1Exc.EditValueChanged
        Try
            FNL4Country1Finalm.Value = Decimal.Parse(Format(FNL4Country1Exc.Value * FNGrandTotal.Value, "0.00"))
            FNL4Country1Extendedm.Value = Decimal.Parse(Format(FNL4Country1Exc.Value * FNExtendedFOB.Value, "0.00"))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FNL4Country2Exc_EditValueChanged(sender As Object, e As EventArgs) Handles FNL4Country2Exc.EditValueChanged
        Try
            FNL4Country2Finalm.Value = Decimal.Parse(Format(FNL4Country2Exc.Value * FNGrandTotal.Value, "0.00"))
            FNL4Country2Extendedm.Value = Decimal.Parse(Format(FNL4Country2Exc.Value * FNExtendedFOB.Value, "0.00"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNL4Country3Exc_EditValueChanged(sender As Object, e As EventArgs) Handles FNL4Country3Exc.EditValueChanged
        Try
            FNL4Country3Finalm.Value = Decimal.Parse(Format(FNL4Country3Exc.Value * FNGrandTotal.Value, "0.00"))
            FNL4Country3Extendedm.Value = Decimal.Parse(Format(FNL4Country3Exc.Value * FNExtendedFOB.Value, "0.00"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNGrandTotal_EditValueChanged(sender As Object, e As EventArgs) Handles FNGrandTotal.EditValueChanged

        Call FNL4Country1Exc_EditValueChanged(FNL4Country2Exc, New EventArgs)
        Call FNL4Country2Exc_EditValueChanged(FNL4Country2Exc, New EventArgs)
        Call FNL4Country3Exc_EditValueChanged(FNL4Country2Exc, New EventArgs)
    End Sub

    Private Sub FNExtendedFOB_EditValueChanged(sender As Object, e As EventArgs) Handles FNExtendedFOB.EditValueChanged
        Call FNL4Country1Exc_EditValueChanged(FNL4Country2Exc, New EventArgs)
        Call FNL4Country2Exc_EditValueChanged(FNL4Country2Exc, New EventArgs)
        Call FNL4Country3Exc_EditValueChanged(FNL4Country2Exc, New EventArgs)
    End Sub

    Private Sub RepositoryItemGridLookUpEditFTSuplCode_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditFTSuplCode.EditValueChanged, RepositoryItemGridLookUpEditFTSuplCodeTrim.EditValueChanged, RepositoryItemGridLookUpEditFTSuplCodeProcessMat.EditValueChanged, RepositoryItemGridLookUpEditFTSuplCodeLabor.EditValueChanged, RepositoryItemGridLookUpEditFTSuplCodePack.EditValueChanged, RepositoryItemGridLookUpEditFTSuplCodeMulti.EditValueChanged
        Try
            Dim MatCode As String = ""
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
                Dim pCOFO As String = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCOFO").ToString

                MatCode = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplCode").ToString())

                '  .SetFocusedRowCellValue("FTMainMatCode", MatCode)

                .SetFocusedRowCellValue("TTLG", pCOFO)


                Select Case .Name
                    Case "ogvfabric"

                        Call Fabcal_EditValueChanging(FabRepositoryCostNetPrice, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                        Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUpEditCFCO, New EventArgs)

                    Case "ogvtrims"

                        Call Acccal_EditValueChanging(TrimsFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                        Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUpEditFTCOFOTrim, New EventArgs)

                    Case "ogcpack"

                        Call Packcal_EditValueChanging(PackFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                        Call RepositoryItemGridLookUpEditCFCO_EditValueChanged(RepositoryItemGridLookUpEditFTCOFOPack, New EventArgs)

                End Select


                If pCOFO <> "" Then

                    Dim cmdstring As String

                    cmdstring = "SELECT  FNHSysCOFOId, FTCOFOCode AS FTCOFO,FTCOFONameEN As FTCOFOName,  FTStateActive, FNFabricPer, FNAccPer, FNImportDuty,FNImportDutyAcc,FNHandlingChandeFab,FNHandlingChandeAcc "
                    cmdstring &= vbCrLf & " from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMCOFO WITH(NOLOCK) "
                    cmdstring &= vbCrLf & " WHERE FTCOFOCode='" & HI.UL.ULF.rpQuoted(pCOFO) & "' "
                    cmdstring &= vbCrLf & "  Order by FTCOFOCode "

                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

                    For Each Rm As DataRow In dt.Rows

                        Select Case .Name
                            Case "ogvfabric"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNFabricPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeFab.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDuty.ToString)

                                Call Fabcal_EditValueChanging(FabRepositoryCostNetPrice, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))

                            Case "ogvtrims"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNAccPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeAcc.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDutyAcc.ToString)

                                Call Acccal_EditValueChanging(TrimsFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))

                            Case "ogvpack"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNAccPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeAcc.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDutyAcc.ToString)

                                Call Packcal_EditValueChanging(PackFNCostPerUOM, New DevExpress.XtraEditors.Controls.ChangingEventArgs(0, 0))
                            Case "ogvteamMulti"

                                .SetFocusedRowCellValue("FNCIF", Rm!FNAccPer.ToString)
                                .SetFocusedRowCellValue("FNHANDLINGCHARGEPERCENT", Rm!FNHandlingChandeAcc.ToString)
                                .SetFocusedRowCellValue("FNIMPORTDUTYPERCENT", Rm!FNImportDutyAcc.ToString)
                        End Select

                    Next

                    dt.Dispose()

                End If

                Try
                    obj.Properties.View.ClearColumnsFilter()
                Catch ex As Exception

                End Try

            End With

            CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).SetFocusedRowCellValue("FTSuplCode", MatCode)
            'Dim sMatCode22 As String = CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("FTMainMatCode").ToString
            CType(sender.Parent.DataSource, DataTable).AcceptChanges()
            'Dim sMatCode As String = CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRowCellValue("FTMainMatCode").ToString
            'Dim MMs As String = ""
            Call SumAmt()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FNExtendedPerm_EditValueChanged(sender As Object, e As EventArgs) Handles FNExtendedPer.EditValueChanged
        FNExtendedFOB.Value = 0
        If FNExtendedPer.Value > 0 And FNGrandTotal.Value > 0 Then
            FNExtendedFOB.Value = FNGrandTotal.Value + Decimal.Parse(Format(((FNGrandTotal.Value * FNExtendedPer.Value) / 100.0), "0.00"))
        End If
    End Sub

    Private Sub ogcteamMulti_Click(sender As Object, e As EventArgs) Handles ogcteamMulti.Click

    End Sub
End Class