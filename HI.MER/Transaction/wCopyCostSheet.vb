Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.IO
Imports Microsoft.VisualBasic

Public Class wCopyCostSheet
    Public StateProcess As Boolean = False

#Region "Variable Declaration"
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_ACCOUNT
    Private Const _nTotalFactorySubOrderNo As Integer = 26
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _tSysDBName As String
    Private _FNRevisedSrc As Integer
    Private _NewFNRevised As Integer = 0
    Private _tSysTableName As String
    Private _tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Order"
    Private _tFTCostSheetNo As String       '...เลขที่Cost Sheet
    Private _nFNHSysCmpId As Integer
    Private _nFNHSysCmpRunId As Integer
    Private _nFNHSysStyleId As Integer
    Private _tFNHSysCmpId As String     '...รหัสโรงงาน/บริษัท สาขา : Code
    Private _tFNHSysCmpRunId As String  '...รหัสเลข run เอกสาร : Code
    Private _tFNHSysStyleId As String   '...รหัสสไตล์ : Code
    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Private tSql As String

    Private _wListCompleteCopyOrder As wListCompleteCopyOrder

    Private _oImage1 As System.Drawing.Image
    Private _oImage2 As System.Drawing.Image
    Private _oImage3 As System.Drawing.Image
    Private _oImage4 As System.Drawing.Image

    Private _FTImage1 As String
    Private _FTImage2 As String
    Private _FTImage3 As String
    Private _FTImage4 As String

#End Region


    Public Sub New(ByVal ptFTCostSheetNo As String, ByVal ptSysDBName As String, ByVal ptSysTableName As String, ByVal ptFNRevised As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Call PrepareForm()

        _tSysDBName = ptSysDBName

        _tSysTableName = ptSysTableName
        _FNRevisedSrc = ptFNRevised
        _tFTCostSheetNo = ptFTCostSheetNo

        If Not DBNull.Value.Equals(ptFTCostSheetNo) And ptFTCostSheetNo <> "" Then
            'Call W_PRCbLoadMasterFactoryOrderNo(_tFTCostSheetNo)

            Me.FTCostSheetNoSrc.Text = _tFTCostSheetNo

            '===================================================== _wListCompleteCopyOrder =======================================================
            _wListCompleteCopyOrder = New wListCompleteCopyOrder(Nothing)

            HI.TL.HandlerControl.AddHandlerObj(_wListCompleteCopyOrder)

            Dim oSysLang As New HI.ST.SysLanguage

            Try
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wListCompleteCopyOrder.Name.ToString.Trim, _wListCompleteCopyOrder)
            Catch ex As Exception
                '...Nothing 
            End Try
            '==================================================================================================================================

        End If

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
        _Str &= vbCrLf & " WHERE FTDynamicFormName='wCostSheet' "
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

        _Str = Me.Query & "  WHERE  " & Me.MainKey & "='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' AND FNRevised ='" & FNRevised.Value & "'"
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

        'Call LoadDocumentDetail(Key.ToString)

        'Call LoadDataInfo2(Key.ToString)
        'Call SumAmt()

        _ProcLoad = False
        _FormLoad = False
    End Sub

    Private Function CopyData() As Boolean
        Dim _Str As String
        Dim _Qry As String
        Dim _FromDt As DataTable
        Dim _FromDtDetail As DataTable
        Dim _StateNew As Boolean = False

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Key As String = ""
        Dim _Val As String = ""
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

        _NewFNRevised = FNRevised.Value


        _Qry = "  Select Top 1 * "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNoSrc.Text) & "' "
        _FromDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)



        If _FromDt.Rows.Count <= 0 Then
            Return False
        End If

        Dim dt As DataTable
        Dim pVersion As String = ""
        Dim pFDCostSheetDate As String = ""
        Dim pFTCostSheetBy As String = ""
        Dim pFNRevised As Integer = 0
        Dim pStyleId As Integer = 0
        Dim pSeasonId As Integer = 0
        Dim pxVersion As Integer = 0

        _Str = "Select TOP 1 FNVersion , FNRevised, FDCostSheetDate, FTCostSheetBy,FNHSysStyleId, FNHSysSeasonId,FNVersion "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet As A With(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(_FTCostSheetNo.Text) & "' "

        dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

        For Each R As DataRow In dt.Rows
            pVersion = R!FNVersion.ToString
            pFDCostSheetDate = R!FDCostSheetDate.ToString
            pFTCostSheetBy = R!FTCostSheetBy.ToString
            pFNRevised = Val(R!FNRevised.ToString)
            pStyleId = Val(R!FNHSysStyleId.ToString)
            pSeasonId = Val(R!FNHSysSeasonId.ToString)
            pxVersion = Val(R!FNVersion.ToString)
        Next

        pStyleId = Val(FNHSysStyleId.Properties.Tag.ToString)
        pSeasonId = Val(FNHSysSeasonId.Properties.Tag.ToString)
        pxVersion = Val(FNVersion.Value)

        _StateNew = (pVersion = "")


        pVersion = pxVersion

        If (_StateNew) Then
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", False, _CmpH).ToString
            pVersion = "1"

        Else
            _Key = _FTCostSheetNo.Text
        End If

        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            ' If Me.FNDebitCreditState.Text = "ลูกค้า" Then


            _Str = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet  WHERE  FTCostSheetNo ='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            _Str &= vbCrLf & "  DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail  WHERE  FTCostSheetNo ='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            _Str &= vbCrLf & "  DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail_TeamMulti  WHERE  FTCostSheetNo ='" & HI.UL.ULF.rpQuoted(_Key) & "'"
            _Str &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet  "
                _Str &= vbCrLf & " ( "
            _Str &= vbCrLf & "   FTCostSheetNo,  FTInsUser, FDInsDate, FTInsTime,  FNRevised, FDCostSheetDate, FTCostSheetBy, FNHSysStyleId, FNHSysSeasonId, FNVersion, FTExp, FTGarmentEngineer, FTSILH, "
            _Str &= vbCrLf & "    FTLOProductDeveloper, FTDevelopmentRegion, FTProductDevelopmentManager, FTMSC, FTDESC, FDBomDate, FDSpecDate, FNHSysCurId, FNExchangeRate, FNHSysVenderPramId, FNHSysCountryId, FTCostingLO,"
                _Str &= vbCrLf & "     FNNoSawAppCostAmt, FNGarmentTreatmentAmt, FTStartSize, FTEndSize, FNNormalSizeAmt, FTAboveSizeBreakDownSpecial, FNAboveSpecialSizeChargePerAmt, FNAboveSpecialSizeAmt, FTLessThanSizeBreakDownSpecial,"
                _Str &= vbCrLf & "     FNLessThanSpecialSizeChargePerAmt, FNLessThanSpecialSizeAmt, FTRemark, FTStateActive, FNHSysCmpId, FTSamFabric, FTSamTrims, FTSamPack, FTSamNoSew, FTSamGarment, FTSamOtherCost, FTSamCMP,"
            _Str &= vbCrLf & "     FBDocument, FNISTeamMulti, FNCostSheetColor, FNCostSheetSize, FNCostSheetBuyType, FNCostSheetQuotedType, FTDateQuoted, FNCostSheetSampleRound, FNHSysStyleIdTo, FTQuotedLog, FNL4Country1,"
            _Str &= vbCrLf & "     FNL4Country1Cur, FNL4Country1Exc, FNL4Country1Final, FNL4Country1Extended, FNL4Country2, FNL4Country2Cur, FNL4Country2Exc, FNL4Country2Final, FNL4Country2Extended, FNL4Country3, FNL4Country3Cur,"
                _Str &= vbCrLf & "    FNL4Country3Exc, FNL4Country3Final, FNL4Country3Extended, FNTotalFabAmt, FNTotalAccAmt, FNChargeFabAmt, FNChargeAccAmt, FNProcessMatCost, FNProcessLaborCost, FNPackagingAmt, FNOtherCostAmt, FNCMP,"
                _Str &= vbCrLf & "    FNGrandTotal, FNExtendedPer, FNExtendedFOB, FNTrinUsageAllowPer, FNL4LTotalFabric, FNL4LTotalTrim, FNL4LChargeFabric, FNL4LChargeTrim, FNL4LProMatCost, FNL4LProLaborCost, FNL4LPackaging, FNL4LOtherCost,"
            _Str &= vbCrLf & "    FNL4LCMP, FNL4LFinalFOB, FNL4LExtendedFOB,FTFileName,FNLeadtime"
            _Str &= vbCrLf & " ) "



                _Str &= vbCrLf & "  SELECT  "
                _Str &= vbCrLf & "    '" & HI.UL.ULF.rpQuoted(_Key) & "'  FTCostSheetNo "
                If pFTCostSheetBy = "" Then
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Str &= vbCrLf & "   ,0 As  FNRevised," & HI.UL.ULDate.FormatDateDB & " AS  FDCostSheetDate,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS  FTCostSheetBy"
                _Str &= vbCrLf & "   ," & pStyleId & " As FNHSysStyleId, " & pSeasonId & " As FNHSysSeasonId, " & pxVersion & " As FNVersion"
            Else
                    _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                _Str &= vbCrLf & "  ," & pFNRevised & " As  FNRevised,'" & pFDCostSheetDate & "' AS  FDCostSheetDate,'" & pFTCostSheetBy & "' AS  FTCostSheetBy"
                _Str &= vbCrLf & "   ," & pStyleId & " As FNHSysStyleId, " & pSeasonId & " As FNHSysSeasonId, " & pxVersion & " As FNVersion"
            End If

            _Str &= vbCrLf & "  , FTExp, FTGarmentEngineer, FTSILH, "
            _Str &= vbCrLf & "    FTLOProductDeveloper, FTDevelopmentRegion, FTProductDevelopmentManager, FTMSC, FTDESC, FDBomDate, FDSpecDate, FNHSysCurId, FNExchangeRate, FNHSysVenderPramId, FNHSysCountryId, FTCostingLO,"
                _Str &= vbCrLf & "     FNNoSawAppCostAmt, FNGarmentTreatmentAmt, FTStartSize, FTEndSize, FNNormalSizeAmt, FTAboveSizeBreakDownSpecial, FNAboveSpecialSizeChargePerAmt, FNAboveSpecialSizeAmt, FTLessThanSizeBreakDownSpecial,"
                _Str &= vbCrLf & "     FNLessThanSpecialSizeChargePerAmt, FNLessThanSpecialSizeAmt, FTRemark, FTStateActive, FNHSysCmpId, FTSamFabric, FTSamTrims, FTSamPack, FTSamNoSew, FTSamGarment, FTSamOtherCost, FTSamCMP,"
            _Str &= vbCrLf & "     FBDocument, FNISTeamMulti, FNCostSheetColor, FNCostSheetSize, FNCostSheetBuyType,  FNCostSheetQuotedType, FTDateQuoted, FNCostSheetSampleRound, FNHSysStyleIdTo, FTQuotedLog, FNL4Country1,"
            _Str &= vbCrLf & "     FNL4Country1Cur, FNL4Country1Exc, FNL4Country1Final, FNL4Country1Extended, FNL4Country2, FNL4Country2Cur, FNL4Country2Exc, FNL4Country2Final, FNL4Country2Extended, FNL4Country3, FNL4Country3Cur,"
                _Str &= vbCrLf & "    FNL4Country3Exc, FNL4Country3Final, FNL4Country3Extended, FNTotalFabAmt, FNTotalAccAmt, FNChargeFabAmt, FNChargeAccAmt, FNProcessMatCost, FNProcessLaborCost, FNPackagingAmt, FNOtherCostAmt, FNCMP,"
                _Str &= vbCrLf & "    FNGrandTotal, FNExtendedPer, FNExtendedFOB, FNTrinUsageAllowPer, FNL4LTotalFabric, FNL4LTotalTrim, FNL4LChargeFabric, FNL4LChargeTrim, FNL4LProMatCost, FNL4LProLaborCost, FNL4LPackaging, FNL4LOtherCost,"
            _Str &= vbCrLf & "    FNL4LCMP, FNL4LFinalFOB, FNL4LExtendedFOB,FTFileName,FNLeadtime"


            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet  WITH(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNoSrc.Text) & "' "

                _Str &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail  "
                _Str &= vbCrLf & " ( "
                _Str &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, FNRevised, FNVersion, FNCostType, FNSeq, FNHSysMainMatId, FNHSysSuplId, FNCostPerPiece, FNExten, FNExtenPer, FNNetExten, FNChinaOrderCost, "
                _Str &= vbCrLf & "     FNMalaysiaOrderCost, FNThailandOrderCost, FNJapanOrderCost, FTSize, FTMainMatCode, FTMainMatColorCode, FTMainMatName, FTSuplCode, TTLG, FTUse, FNWeight, FNWidth, FTWidthUnit, FNMarkerEff, FNMarkerUsed, "
                        _Str &= vbCrLf & "    FNAllowancePer, FNTotalUsed, FTRMDSSeason, FNRMDSStatus, FNHSysUnitId, FTUnitCode, FNCostPerUOM, FNCIF, FNUSAGECOST, FNHANDLINGCHARGEPERCENT, FNHANDLINGCHARGECOST, FNIMPORTDUTYPERCENT, "
                _Str &= vbCrLf & "    FNImportDuty, FTPROCESSSUBTYPE, FNHSysProcessMatId, FNSTANDARDALLOWEDMINUTES, FNEFFICIENCYPERCENT, FNPROFITPERCENT, FNCMPCOST, FTBMCCODE, FTBEMISITEM, FNFULLWIDTH, FNSLITTINGWIDTH, "
                _Str &= vbCrLf & "   FNREQUIREDLENGTH, FNNETUSAGEINFULLWIDTH, FNPRICEINMETER, FNBEMISSLITTINGUPCHARGE, FNPRICEPERSLITTINGWITDH, FTRemark, FNTOTALUSAGECOST, FNTOTALHANDINGCHANGECOST, FNFINALFOB, "
                _Str &= vbCrLf & "   FNEXTENDEDSIZEFOB, FNTOTALTRIMPROCESSCOST, FTL4LORDERCNTY1, FTL4LCURRENCYFOB1, FNEXTENDSIZEFOBL4L1, FTL4LORDERCNTY2, FTL4LCURRENCYFOB2, FNEXTENDSIZEFOBL4L2, FTL4LORDERCNTY3,  "
                _Str &= vbCrLf & "   FTL4LCURRENCYFOB3, FNEXTENDSIZEFOBL4L3, FTPRODUCTDEVELOPER, FTTeamName "

                _Str &= vbCrLf & " ) "
                _Str &= vbCrLf & "  SELECT  "

                _Str &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

            _Str &= vbCrLf & "   ,'" & HI.UL.ULF.rpQuoted(_Key) & "'  AS  FTCostSheetNo, FNRevised, FNVersion, FNCostType, FNSeq, FNHSysMainMatId, FNHSysSuplId, FNCostPerPiece, FNExten, FNExtenPer, FNNetExten, FNChinaOrderCost, "
            _Str &= vbCrLf & "     FNMalaysiaOrderCost, FNThailandOrderCost, FNJapanOrderCost, FTSize, FTMainMatCode, FTMainMatColorCode, FTMainMatName, FTSuplCode, TTLG, FTUse, FNWeight, FNWidth, FTWidthUnit, FNMarkerEff, FNMarkerUsed, "
                _Str &= vbCrLf & "    FNAllowancePer, FNTotalUsed, FTRMDSSeason, FNRMDSStatus, FNHSysUnitId, FTUnitCode, FNCostPerUOM, FNCIF, FNUSAGECOST, FNHANDLINGCHARGEPERCENT, FNHANDLINGCHARGECOST, FNIMPORTDUTYPERCENT, "
                _Str &= vbCrLf & "    FNImportDuty, FTPROCESSSUBTYPE, FNHSysProcessMatId, FNSTANDARDALLOWEDMINUTES, FNEFFICIENCYPERCENT, FNPROFITPERCENT, FNCMPCOST, FTBMCCODE, FTBEMISITEM, FNFULLWIDTH, FNSLITTINGWIDTH, "
                _Str &= vbCrLf & "   FNREQUIREDLENGTH, FNNETUSAGEINFULLWIDTH, FNPRICEINMETER, FNBEMISSLITTINGUPCHARGE, FNPRICEPERSLITTINGWITDH, FTRemark, FNTOTALUSAGECOST, FNTOTALHANDINGCHANGECOST, FNFINALFOB, "
                _Str &= vbCrLf & "   FNEXTENDEDSIZEFOB, FNTOTALTRIMPROCESSCOST, FTL4LORDERCNTY1, FTL4LCURRENCYFOB1, FNEXTENDSIZEFOBL4L1, FTL4LORDERCNTY2, FTL4LCURRENCYFOB2, FNEXTENDSIZEFOBL4L2, FTL4LORDERCNTY3,  "
                _Str &= vbCrLf & "   FTL4LCURRENCYFOB3, FNEXTENDSIZEFOBL4L3, FTPRODUCTDEVELOPER, FTTeamName "
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail  With(NOLOCK) "
                _Str &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNoSrc.Text) & "' "


            _Str &= vbCrLf & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail_TeamMulti  "
            _Str &= vbCrLf & " ( "
            _Str &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTCostSheetNo, FNRevised, FNVersion, FNSeq, FTMSC, FTSeason, FTStyleCode, FTColorway, FTTeamName, FNBaseFOB, FNAllowancePer, FTItem1, FTProcesssubType1, "
            _Str &= vbCrLf & "    FTDescription1, FTSuplCode1, FNUnitPrice1, FNCIF1, FNUSAGECOST1, FNHandlingChargePercent1, FNHandlingChargeCost1, FNTotalCost1, FNImportDutyPecent1, FTItem2, FTProcesssubType2, FTDescription2, FTSuplCode2, "
            _Str &= vbCrLf & "    FNUnitPrice2, FNCIF2, FNUSAGECOST2, FNHandlingChargePercent2, FNHandlingChargeCost2, FNTotalCost2, FNImportDutyPecent2, FTItem3, FTProcesssubType3, FTDescription3, FTSuplCode3, FNUnitPrice3, FNCIF3, "
            _Str &= vbCrLf & "    FNUSAGECOST3, FNHandlingChargePercent3, FNHandlingChargeCost3, FNTotalCost3, FNImportDutyPecent3, FTItem4, FTProcesssubType4, FTDescription4, FTSuplCode4, FNUnitPrice4, FNCIF4, FNUSAGECOST4, "
            _Str &= vbCrLf & "    FNHandlingChargePercent4, FNHandlingChargeCost4, FNTotalCost4, FNImportDutyPecent4, FTItem5, FTProcesssubType5, FTDescription5, FTSuplCode5, FNUnitPrice5, FNCIF5, FNUSAGECOST5, FNHandlingChargePercent5, "
            _Str &= vbCrLf & "    FNHandlingChargeCost5, FNTotalCost5, FNImportDutyPecent5, FTItem6, FTProcesssubType6, FTDescription6, FTSuplCode6, FNUnitPrice6, FNCIF6, FNUSAGECOST6, FNHandlingChargePercent6, FNHandlingChargeCost6, "
            _Str &= vbCrLf & "    FNTotalCost6, FNImportDutyPecent6, FTItem7, FTProcesssubType7, FTDescription7, FTSuplCode7, FNUnitPrice7, FNCIF7, FNUSAGECOST7, FNHandlingChargePercent7, FNHandlingChargeCost7, FNTotalCost7, "
            _Str &= vbCrLf & "    FNImportDutyPecent7, FTItem8, FTProcesssubType8, FTDescription8, FTSuplCode8, FNUnitPrice8, FNCIF8, FNUSAGECOST8, FNHandlingChargePercent8, FNHandlingChargeCost8, FNTotalCost8, FNImportDutyPecent8, FTItem9, "
            _Str &= vbCrLf & "    FTProcesssubType9, FTDescription9, FTSuplCode9, FNUnitPrice9, FNCIF9, FNUSAGECOST9, FNHandlingChargePercent9, FNHandlingChargeCost9, FNTotalCost9, FNImportDutyPecent9, FTItem10, FTProcesssubType10, "
            _Str &= vbCrLf & "    FTDescription10, FTSuplCode10, FNUnitPrice10, FNCIF10, FNUSAGECOST10, FNHandlingChargePercent10, FNHandlingChargeCost10, FNTotalCost10, FNImportDutyPecent10, FTItem11, FTProcesssubType11, FTDescription11, "
            _Str &= vbCrLf & "   FTSuplCode11, FNUnitPrice11, FNCIF11, FNUSAGECOST11, FNHandlingChargePercent11, FNHandlingChargeCost11, FNTotalCost11, FNImportDutyPecent11, FTItem12, FTProcesssubType12, FTDescription12, FTSuplCode12, "
            _Str &= vbCrLf & "   FNUnitPrice12, FNCIF12, FNUSAGECOST12, FNHandlingChargePercent12, FNHandlingChargeCost12, FNTotalCost12, FNImportDutyPecent12, FTItem13, FTProcesssubType13, FTDescription13, FTSuplCode13, FNUnitPrice13, "
            _Str &= vbCrLf & "   FNCIF13, FNUSAGECOST13, FNHandlingChargePercent13, FNHandlingChargeCost13, FNTotalCost13, FNImportDutyPecent13, FTItem14, FTProcesssubType14, FTDescription14, FTSuplCode14, FNUnitPrice14, FNCIF14, "
            _Str &= vbCrLf & "   FNUSAGECOST14, FNHandlingChargePercent14, FNHandlingChargeCost14, FNTotalCost14, FNImportDutyPecent14, FTItem15, FTProcesssubType15, FTDescription15, FTSuplCode15, FNUnitPrice15, FNCIF15, FNUSAGECOST15, "
            _Str &= vbCrLf & "   FNHandlingChargePercent15, FNHandlingChargeCost15, FNTotalCost15, FNImportDutyPecent15, FNTotalUsgeCost, FNTotalHandlingChargeCost, FNFINALFOB, FNEXTENDEDSIZEFOB, FTL4LORDERCNTY1, "
            _Str &= vbCrLf & "   FTL4LCURRENCYFOB1, FNEXTENDSIZEFOBL4L1, FTL4LORDERCNTY2, FTL4LCURRENCYFOB2, FNEXTENDSIZEFOBL4L2, FTL4LORDERCNTY3, FTL4LCURRENCYFOB3, FNEXTENDSIZEFOBL4L3, FTPRODUCTDEVELOPER, "
            _Str &= vbCrLf & "   FTRemark"
            _Str &= vbCrLf & " ) "
            _Str &= vbCrLf & "  SELECT  "

            _Str &= vbCrLf & "'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

            _Str &= vbCrLf & "   ,'" & HI.UL.ULF.rpQuoted(_Key) & "'  AS  FTCostSheetNo,  FNRevised, FNVersion, FNSeq, FTMSC, FTSeason, FTStyleCode, FTColorway, FTTeamName, FNBaseFOB, FNAllowancePer, FTItem1, FTProcesssubType1, "
            _Str &= vbCrLf & "    FTDescription1, FTSuplCode1, FNUnitPrice1, FNCIF1, FNUSAGECOST1, FNHandlingChargePercent1, FNHandlingChargeCost1, FNTotalCost1, FNImportDutyPecent1, FTItem2, FTProcesssubType2, FTDescription2, FTSuplCode2, "
            _Str &= vbCrLf & "    FNUnitPrice2, FNCIF2, FNUSAGECOST2, FNHandlingChargePercent2, FNHandlingChargeCost2, FNTotalCost2, FNImportDutyPecent2, FTItem3, FTProcesssubType3, FTDescription3, FTSuplCode3, FNUnitPrice3, FNCIF3, "
            _Str &= vbCrLf & "    FNUSAGECOST3, FNHandlingChargePercent3, FNHandlingChargeCost3, FNTotalCost3, FNImportDutyPecent3, FTItem4, FTProcesssubType4, FTDescription4, FTSuplCode4, FNUnitPrice4, FNCIF4, FNUSAGECOST4, "
            _Str &= vbCrLf & "    FNHandlingChargePercent4, FNHandlingChargeCost4, FNTotalCost4, FNImportDutyPecent4, FTItem5, FTProcesssubType5, FTDescription5, FTSuplCode5, FNUnitPrice5, FNCIF5, FNUSAGECOST5, FNHandlingChargePercent5, "
            _Str &= vbCrLf & "    FNHandlingChargeCost5, FNTotalCost5, FNImportDutyPecent5, FTItem6, FTProcesssubType6, FTDescription6, FTSuplCode6, FNUnitPrice6, FNCIF6, FNUSAGECOST6, FNHandlingChargePercent6, FNHandlingChargeCost6, "
            _Str &= vbCrLf & "    FNTotalCost6, FNImportDutyPecent6, FTItem7, FTProcesssubType7, FTDescription7, FTSuplCode7, FNUnitPrice7, FNCIF7, FNUSAGECOST7, FNHandlingChargePercent7, FNHandlingChargeCost7, FNTotalCost7, "
            _Str &= vbCrLf & "    FNImportDutyPecent7, FTItem8, FTProcesssubType8, FTDescription8, FTSuplCode8, FNUnitPrice8, FNCIF8, FNUSAGECOST8, FNHandlingChargePercent8, FNHandlingChargeCost8, FNTotalCost8, FNImportDutyPecent8, FTItem9, "
            _Str &= vbCrLf & "    FTProcesssubType9, FTDescription9, FTSuplCode9, FNUnitPrice9, FNCIF9, FNUSAGECOST9, FNHandlingChargePercent9, FNHandlingChargeCost9, FNTotalCost9, FNImportDutyPecent9, FTItem10, FTProcesssubType10, "
            _Str &= vbCrLf & "    FTDescription10, FTSuplCode10, FNUnitPrice10, FNCIF10, FNUSAGECOST10, FNHandlingChargePercent10, FNHandlingChargeCost10, FNTotalCost10, FNImportDutyPecent10, FTItem11, FTProcesssubType11, FTDescription11, "
            _Str &= vbCrLf & "   FTSuplCode11, FNUnitPrice11, FNCIF11, FNUSAGECOST11, FNHandlingChargePercent11, FNHandlingChargeCost11, FNTotalCost11, FNImportDutyPecent11, FTItem12, FTProcesssubType12, FTDescription12, FTSuplCode12, "
            _Str &= vbCrLf & "   FNUnitPrice12, FNCIF12, FNUSAGECOST12, FNHandlingChargePercent12, FNHandlingChargeCost12, FNTotalCost12, FNImportDutyPecent12, FTItem13, FTProcesssubType13, FTDescription13, FTSuplCode13, FNUnitPrice13, "
            _Str &= vbCrLf & "   FNCIF13, FNUSAGECOST13, FNHandlingChargePercent13, FNHandlingChargeCost13, FNTotalCost13, FNImportDutyPecent13, FTItem14, FTProcesssubType14, FTDescription14, FTSuplCode14, FNUnitPrice14, FNCIF14, "
            _Str &= vbCrLf & "   FNUSAGECOST14, FNHandlingChargePercent14, FNHandlingChargeCost14, FNTotalCost14, FNImportDutyPecent14, FTItem15, FTProcesssubType15, FTDescription15, FTSuplCode15, FNUnitPrice15, FNCIF15, FNUSAGECOST15, "
            _Str &= vbCrLf & "   FNHandlingChargePercent15, FNHandlingChargeCost15, FNTotalCost15, FNImportDutyPecent15, FNTotalUsgeCost, FNTotalHandlingChargeCost, FNFINALFOB, FNEXTENDEDSIZEFOB, FTL4LORDERCNTY1, "
            _Str &= vbCrLf & "   FTL4LCURRENCYFOB1, FNEXTENDSIZEFOBL4L1, FTL4LORDERCNTY2, FTL4LCURRENCYFOB2, FNEXTENDSIZEFOBL4L2, FTL4LORDERCNTY3, FTL4LCURRENCYFOB3, FNEXTENDSIZEFOBL4L3, FTPRODUCTDEVELOPER, "
            _Str &= vbCrLf & "   FTRemark"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail_TeamMulti  With(NOLOCK) "
            _Str &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNoSrc.Text) & "' "


            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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

    Private Function CopyDetail(ByVal _Key As String) As Boolean
        Try
            Dim _Qry As String = ""
            Dim _Seq As Integer = 0
            Dim _oDt As DataTable
            Dim _nDt As DataTable

            For i As Integer = 1 To 4
                _Seq = 1

                _Qry = "  Select FTCostSheetNo, FNSeq, FNCostType, FNRevised, FNWeight, FNWidth, FNMarkerEff, FNMarkerUsed, FNAllowancePer, FNRMDSStatus, FNCostPerUOM, FNCostPerPiece, FNCIF, FNExtenPer, M.FTMainMatNameTH AS FNHSysMerMatId_None,"
                _Qry &= vbCrLf & " FNImportDuty, FNChinaOrderCost, FNMalaysiaOrderCost, FNThailandOrderCost, FNJapanOrderCost,"
                _Qry &= vbCrLf & " FNTotalUsed , FNExten, FNNetExten,"
                _Qry &= vbCrLf & " M.FTMainMatCode As FTMainMatCode, C.FNHSysMainMatId AS FNHSysMerMatId, S.FTSuplCode As FTSuplCode, C.FNHSysSuplId As FNHSysSuplId, U.FTUnitCode As FTUnitCode, C.FNHSysUnitId As FNHSysUnitId"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With(NOLOCK) On C.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S With(NOLOCK) On C.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As M With(NOLOCK) On C.FNHSysMainMatId = M.FNHSysMainMatId "
                _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNoSrc.Text) & "' AND FNRevised ='" & _FNRevisedSrc & "' AND FNCostType = " & i & ""

                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                _Qry = "  Select FTCostSheetNo, FNSeq, FNCostType, FNRevised,FNVersion"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail As C With (NOLOCK) LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U With(NOLOCK) On C.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S With(NOLOCK) On C.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As M With(NOLOCK) On C.FNHSysMainMatId = M.FNHSysMainMatId "
                _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(Me.FTCostSheetNo.Text) & "' AND FNRevised ='" & _NewFNRevised & "' AND FNCostType = " & i & ""

                _nDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                _Seq = _nDt.Rows.Count

                For Each R As DataRow In _oDt.Rows
                    'Dim _FNHSysUnitId As String = HI.Conn.SQLConn.GetField("SELECT FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit WITH(NOLOCK) WHERE FTUnitCode='" & R!FTUnitCode.ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                    _Seq += +1

                    '_Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
                    '_Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    '_Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    '_Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    '_Qry &= vbCrLf & ",FNHSysMainMatId=" & CInt("0" & R!FNHSysMerMatId.ToString)
                    '_Qry &= vbCrLf & ",FNWeight=" & CDbl("0" & R!FNWeight.ToString)
                    '_Qry &= vbCrLf & ",FNWidth=" & CDbl("0" & R!FNWidth.ToString)
                    '_Qry &= vbCrLf & ",FNMarkerEff=" & CDbl("0" & R!FNMarkerEff.ToString)
                    '_Qry &= vbCrLf & ",FNMarkerUsed=" & CDbl("0" & R!FNMarkerUsed.ToString)
                    '_Qry &= vbCrLf & ",FNAllowancePer=" & CDbl("0" & R!FNAllowancePer.ToString)
                    '_Qry &= vbCrLf & ",FNTotalUsed=" & CDbl("0" & R!FNTotalUsed.ToString)
                    '_Qry &= vbCrLf & ",FNRMDSStatus='" & R!FNRMDSStatus.ToString & "'"
                    '_Qry &= vbCrLf & ",FNHSysSuplId=" & CInt("0" & R!FNHSysSuplId.ToString)
                    '_Qry &= vbCrLf & ",FNCostPerUOM=" & CDbl("0" & R!FNCostPerUOM.ToString)
                    ''_Qry &= vbCrLf & ",FNHSysUnitId=" & CInt("0" & R!FNHSysUnitId.ToString)
                    '_Qry &= vbCrLf & ",FNCostPerPiece=" & CDbl("0" & R!FNCostPerPiece.ToString)
                    '_Qry &= vbCrLf & ",FNCIF=" & CDbl("0" & R!FNCIF.ToString)
                    '_Qry &= vbCrLf & ",FNExten=" & CDbl("0" & R!FNExten.ToString)
                    '_Qry &= vbCrLf & ",FNExtenPer=" & CDbl("0" & R!FNExtenPer.ToString)
                    '_Qry &= vbCrLf & ",FNNetExten=" & CDbl("0" & R!FNNetExten.ToString)
                    '_Qry &= vbCrLf & ",FNImportDuty=" & CDbl("0" & R!FNImportDuty.ToString)
                    '_Qry &= vbCrLf & ",FNChinaOrderCost=" & CDbl("0" & R!FNChinaOrderCost.ToString)
                    '_Qry &= vbCrLf & ",FNMalaysiaOrderCost=" & CDbl("0" & R!FNMalaysiaOrderCost.ToString)
                    '_Qry &= vbCrLf & ",FNThailandOrderCost=" & CDbl("0" & R!FNThailandOrderCost.ToString)
                    '_Qry &= vbCrLf & ",FNJapanOrderCost=" & CDbl("0" & R!FNJapanOrderCost.ToString)
                    '_Qry &= vbCrLf & ",FNHSysUnitId=" & CInt("0" & R!FNHSysUnitId.ToString)
                    '_Qry &= vbCrLf & "WHERE FTCostSheetNo='" & _Key & "' AND FNRevised = '" & _NewFNRevised & "'"
                    ''_Qry &= vbCrLf & " AND FNHSysMainMatId = '" & R!FNHSysMerMatId.ToString & "' AND FNCostType = '" & R!FNCostType.ToString & "' AND FNSeq=" & _Seq
                    '_Qry &= vbCrLf & " AND FNCostType = '" & R!FNCostType.ToString & "' AND FNSeq=" & CInt("0" & R!FNSeq.ToString)

                    'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet_Detail"
                        _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,   FTCostSheetNo, FNRevised, FNCostType,   FNSeq, FNHSysMainMatId, FNWeight, FNWidth, FNMarkerEff, FNMarkerUsed, FNAllowancePer, FNTotalUsed, FNRMDSStatus, FNHSysSuplId, FNCostPerUOM, FNHSysUnitId, FNCostPerPiece, FNCIF, FNExten, FNExtenPer, FNNetExten, FNImportDuty, FNChinaOrderCost, FNMalaysiaOrderCost, FNThailandOrderCost, FNJapanOrderCost"
                        _Qry &= vbCrLf & ")"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & _Key & "'"
                        _Qry &= vbCrLf & "," & Me._NewFNRevised
                        _Qry &= vbCrLf & "," & CInt("0" & R!FNCostType.ToString)
                    '_Qry &= vbCrLf & "," & CInt("0" & R!FNSeq.ToString)
                    _Qry &= vbCrLf & "," & _Seq
                    _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysMerMatId.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNWeight.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNWidth.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNMarkerEff.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNMarkerUsed.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNAllowancePer.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNTotalUsed.ToString)
                        _Qry &= vbCrLf & ",'" & R!FNRMDSStatus.ToString & "'"
                        _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysSuplId.ToString)
                        _Qry &= vbCrLf & "," & CDbl("0" & R!FNCostPerUOM.ToString)
                        '_Qry &= vbCrLf & "," & CInt("0" & R!FNHSysUnitId.ToString)
                        _Qry &= vbCrLf & "," & CInt("0" & R!FNHSysUnitId.ToString)
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
                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    'End If
                Next

            Next

        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Function VerifyData() As Boolean
        If FTCostSheetNo.Text.Trim = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTCostSheetNo_lbl.Text)
            FTCostSheetNo.Focus()
            Return False
        End If

        If FNHSysStyleId.Text.Trim = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
            Return False

        End If

        If FNHSysSeasonId.Text.Trim = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysSeasonId_lbl.Text)
            FNHSysSeasonId.Focus()
            Return False
        End If

        If FNVersion.Value <= 0 Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNVersion_lbl.Text)
            FNVersion.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Dim _Qry As String = ""
        Dim Dt As DataTable

        _Qry = "  Select FNRevised ,FTCostSheetNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
        Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
        '  If Me.VerrifyData Then
        'If Dt.Rows.Count >= 0 Then

        If VerifyData() Then
            If Me.CopyData() Then
                StateProcess = True
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                'FTDocRefNo.Properties.ReadOnly = CType(ogcdetail.DataSource, DataTable).Rows.Count > 0 '(ogvdetail.Rows.Count > 0)
                'FTDocRefNo.Properties.Buttons.Item(0).Enabled = Not (CType(ogcdetail.DataSource, DataTable).Rows.Count > 0)

                'Call CopyData()
                Me.Close()
                'Call LoadDataInfo2(FTCostSheetNo.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mProcessError(1000000005, "ไม่สามารถแก้ไขเอกสารนี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wCopyCostSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemoveHandler FTCostSheetNo.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
        RemoveHandler FTCostSheetNo.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Sub FTCostSheetNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTCostSheetNo.EditValueChanged
        Try
            'Call Me.ClearData()
            Dim _Qry As String = ""
            Dim Dt As DataTable

            _Qry = "  Select FNRevised ,FTCostSheetNo"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTCostSheet With(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTCostSheetNo='" & HI.UL.ULF.rpQuoted(FTCostSheetNo.Text) & "'"
            Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

            If (Dt.Rows.Count = 0 And FTCostSheetNo.Text <> "") Then
                FNRevised.Value = 0
            End If

            Call LoadDataInfo(Me.FTCostSheetNo.Text)
        Catch ex As Exception
        End Try
    End Sub
End Class