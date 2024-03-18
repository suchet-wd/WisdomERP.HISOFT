Imports System.Windows.Forms
Imports System.Globalization
Imports System.Threading
Imports System.Drawing
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf
Imports System
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraSpreadsheet.Model
Imports DevExpress.XtraEditors.Controls

Public Class wEmployee

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_HR
    Private _Bindgrid As Boolean = False
    Private _RowDataChange As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()

    Private _DataInfo As DataTable
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _ProcLink As Boolean = False
    Private _ProcDefault As Boolean = False
    Private oDbdtLate As DataTable
    Private oDbdtLeave As DataTable
    'jokerModify
    Private dtTrainType As DataTable
    Private dtPurpose As DataTable
    Private dtDefault As DataTable
    Private _LoadForm As Boolean = False
    Private RunEmpCodeByTypeAndSect As Boolean = True
    Private _PathAttach As String = ""
    'Private _wFundRatePopUp As wFundRatePopUp
    Private _wEmployeeViewPic As wEmployeeViewPic

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call InitForm()
        Call PrepareDatatable()
        'Call InitialGridTraining()
        Dim oSysLang As New ST.SysLanguage
        Call oSysLang.InsertObjectLanguage(ST.SysInfo.ModuleName, Me.Name, ogctrain)
        Call oSysLang.LoadObjectLanguage(ST.SysInfo.ModuleName, Me.Name, ogctrain)

        FNHSysCmpId.Properties.ReadOnly = (Me.DefaultCmpCode <> "")
        FNHSysCmpId.Properties.Buttons.Item(0).Visible = Not (Me.DefaultCmpCode <> "")
        '_wFundRatePopUp = New wFundRatePopUp
        _wEmployeeViewPic = New wEmployeeViewPic
        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
        Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False

        Try
            With Me.ogvleave
                .Columns.ColumnByFieldName("FNLeaveRight").Visible = Not (HI.ST.Config.StateNotShowEmpLeave)
                .Columns.ColumnByFieldName("FNLeaveBal").Visible = Not (HI.ST.Config.StateNotShowEmpLeave)
            End With
        Catch ex As Exception
        End Try

        Call TabChenge()

        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='RunEmpCodeByTypeAndSect'"

        RunEmpCodeByTypeAndSect = (HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "") <> "N")

    End Sub

#Region "Property"
    Private _DefaultCmpCode As String = ""
    Public Property DefaultCmpCode As String
        Get
            Return _DefaultCmpCode
        End Get
        Set(ByVal value As String)
            _DefaultCmpCode = value
        End Set
    End Property

    Private _FmtRun As String = ""
    Public ReadOnly Property FmtRun As String
        Get
            Return ""
        End Get
    End Property

    Private _AssPath As String = ""
    Public Property AssPath As String
        Get
            Return _AssPath
        End Get
        Set(ByVal value As String)
            _AssPath = value
        End Set
    End Property

    Private _FormName As String = ""
    Public Property FormName As String
        Get
            Return _FormName
        End Get
        Set(ByVal value As String)
            _FormName = value
        End Set
    End Property

    Private _FormObjID As Integer = 0
    Public Property FormObjID As Integer
        Get
            Return _FormObjID
        End Get
        Set(ByVal value As Integer)
            _FormObjID = value
        End Set
    End Property

    Private _FormPopup As String = ""
    Public Property FormPopup As String
        Get
            Return _FormPopup
        End Get
        Set(ByVal value As String)
            _FormPopup = value
        End Set
    End Property

    Private _ObjectFocus As Object = Nothing
    Public Property ObjectFocus As Object
        Get
            Return _ObjectFocus
        End Get
        Set(ByVal value As Object)
            _ObjectFocus = value
        End Set
    End Property

    Private _SysDBName As String = ""
    Public Property SysDBName As String
        Get
            Return _SysDBName
        End Get
        Set(ByVal value As String)
            _SysDBName = value
        End Set
    End Property

    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(ByVal value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(ByVal value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property

    Private _MainKeyID As String = ""
    Public Property MainKeyID As String
        Get
            Return _MainKeyID
        End Get
        Set(ByVal value As String)
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
        Set(ByVal value As String)
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
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private _Parent_Form As Object
    Public Property Parent_Form As Object
        Get
            Return _Parent_Form
        End Get
        Set(ByVal value As Object)
            _Parent_Form = value
        End Set
    End Property

#End Region


#Region "Procedure"

    Private Sub InitForm()

        Dim _Qry As String = ""
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


        _Qry = "SELECT TOP 1 FTBaseName,FTTableName AS FHSysTableName,FNFormObjID,FTBaseName + '.' + FTPrefix + '.' + FTTableName AS FTTableName,FTSortField,FNFormPopUpWidth,FNFormPopUpHeight  "
        _Qry &= vbCrLf & "   FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysTableObjForm WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDynamicFormName=N'" & HI.UL.ULF.rpQuoted(Me.Name) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

        If _dt.Rows.Count > 0 Then

            _objId = Integer.Parse(_dt.Rows(0)!FNFormObjID.ToString)
            Me.SysDBName = _dt.Rows(0)!FTBaseName.ToString
            Me.SysTableName = _dt.Rows(0)!FHSysTableName.ToString
            Me.TableName = _dt.Rows(0)!FTTableName.ToString

            _SortField = _dt.Rows(0)!FTSortField.ToString

            _Qry = "   SELECT       FTBaseName, FTPrefix, FTTableName, FNGrpObjID, FNGrpObjSeq, FNFormObjID, FNGenFormObj, FNGenFormObjSeq, FTDynamicFormName, FTSortField, "
            _Qry &= vbCrLf & "  FNFormWidth, FNFormHeight, FNFormPopUpWidth, FNFormPopUpHeight, FTAssemBlyName, FTAssFormName, FTPropertyInfo"
            _Qry &= vbCrLf & "  FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysTableObjForm  WITH(NOLOCK)  "
            _Qry &= vbCrLf & " WHERE        (FNGrpObjID =" & _objId & ")"
            _Qry &= vbCrLf & " ORDER BY  CASE WHEN FNFormObjID=" & _objId & " THEN 0 ELSE 1 END,FNGrpObjSeq"
            _dtgrpobj = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            _Qry = " EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.SP_GET_DYNAMIC_OBJECT_CONTROL " & _objId & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
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

    Private Sub PrepareDatatable()


        oDbdtLeave = New DataTable
        oDbdtLeave.Columns.Add("FTLeaveCode", GetType(String))
        oDbdtLeave.Columns.Add("FTLeaveName", GetType(String))
        oDbdtLeave.Columns.Add("FNLeaveRight", GetType(Integer))
        oDbdtLeave.Columns.Add("FNLeaveUsed", GetType(Integer))
        oDbdtLeave.Columns.Add("FNLeaveBal", GetType(Integer))
        ogdLeave.DataSource = oDbdtLeave

    End Sub

    Public Sub LoadEmpCodeByEmpIDInfo(ByVal Key As Object)
        _ProcLink = True
        Dim _Qry As String = "SELECT TOP 1 FTEmpCode   FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK) WHERE FNHSysEmpID =" & Val(Key) & " "
        FNHSysEmpID.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        FNHSysEmpID.Properties.Tag = Key

        Call LoadDataInfo(Key)

        _ProcLink = False
    End Sub

    Public Sub LoadDataInfo(ByVal Key As Object)
        FTPassPostNo.Text = ""
        FDDateofIssue.Text = ""
        FDDateofExpiry.Text = ""
        FTVisasNo.Text = ""
        FDDateVisas.Text = ""
        FDDateVisasExpiry.Text = ""
        FTWorkPermitNo.Text = ""
        FDDateValid.Text = ""
        FDDateUntil.Text = ""
        FTMOUDoccument.Text = ""
        FDDateMOU.Text = ""
        FDDateMOUex.Text = ""
        FTStricken.Text = ""



        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

        _ProcLoad = True

        Static _Proc As Boolean

        If Not (_Proc) Then
            _Proc = True
            'HI.TL.HandlerControl.ClearControl(Me, , {"FNHSysCmpId", "FNHSysEmpID", "FNHSysEmpTypeId"})

            Dim _Dt As DataTable
            Dim _Qry As String
            _Qry = Me.Query & "  WHERE  " & Me.MainKey & "=N'" & (Val(FNHSysEmpID.Properties.Tag.ToString)).ToString & "' "
            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, _DBEnum)

            Dim _FieldName As String = ""
            For Each R As DataRow In _Dt.Rows
                For Each Col As DataColumn In _Dt.Columns
                    _FieldName = Col.ColumnName.ToString

                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.ButtonEdit

                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                    If _FieldName.ToUpper <> FNHSysEmpID.Name.ToString.ToUpper Then

                                        If _FieldName.ToUpper = FNHSysEmpIDLeader.Name.ToString.ToUpper Then
                                            _Qry = "SELECt TOP 1 FTEmpCode  FROM THRMEmployee WITH(NOLOCK) WHERE FNHSysEmpID=" & Val(R.Item(Col).ToString) & " "
                                            .Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
                                        Else
                                            .Text = R.Item(Col).ToString
                                        End If

                                    End If

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
                                        If _PathEmpPic = "" Then
                                            .Image = HI.UL.ULImage.LoadImage(_PathEmpPic & "" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                        Else
                                            .Image = HI.UL.ULImage.LoadImage(_PathEmpPic & "" & R.Item(Col).ToString)
                                        End If
                                        '.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & "" & .Properties.Tag.ToString & R.Item(Col).ToString)

                                    Catch ex As Exception
                                        .Image = Nothing
                                    End Try
                                End With
                            Case ENM.Control.ControlType.DateEdit
                                Try
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)

                                        If .Properties.DisplayFormat.FormatString = "d" Then
                                            .DateTime = CDate(R.Item(Col).ToString)
                                        Else
                                            .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                        End If
                                    End With
                                Catch ex As Exception
                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)

                                        .Text = ""
                                    End With
                                End Try
                            Case Else
                                Obj.Text = R.Item(Col).ToString
                        End Select
                    Next
                Next

                Exit For
            Next

            Try
                If "" & Me.FNHSysEmpID.Properties.Tag.ToString <> "" Then
                    Call LoadDetail(Me.FNHSysEmpID.Properties.Tag.ToString)
                    Call FDDateStart_EditValueChanged(FDDateStart, New System.EventArgs)
                Else
                    Call LoadDetail(Me.FNHSysEmpID.Properties.Tag.ToString)
                End If

            Catch ex As Exception
            End Try

            Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysEmpTypeId, New System.EventArgs)

            Me.otab.SelectedTabPageIndex = 0

            Call CheckPaidPayroll()
            Call CheckPermissionSalary()

            _Proc = False
        End If


        _ProcLoad = False
    End Sub

    Private Sub LoadDetail(ByVal Key As String)

        Call ShowLeaveInfo(Key)
        Call ShowEducation(Key)
        Call ShowTrainning(Key)
        Call SetShowFinance(Key)
        Call ShowResign(Key)
        Call ShowPunishment(Key)
        Call ShowFamily(Key)
        Call ShowExperience(Key)
        Call LoadPass()
        Call LoadVisa()
        Call LoadWorkpermit()
        Call LoadMOU()
        Call LoadOther()
        Call LoadStricken()
        Call LoadStricken1()
    End Sub

    Public Sub DefaultsData()
        _ProcDefault = True
        HI.TL.HandlerControl.ClearControl(Me, False, {Me.FNHSysCmpId.Name.ToString, Me.FNHSysCmpId_None.Name.ToString, Me.FNHSysEmpTypeId.Name.ToString, Me.FNHSysEmpTypeId_None.Name.ToString, Me.FNHSysEmpID.Name.ToString})
        Dim _FieldName As String
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).DefaultsData.ToArray.Count - 1
                _FieldName = _FormHeader(cind).DefaultsData(I).FiledName.ToString

                Dim Pass As Boolean = True

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                If _FieldName.ToUpper = "FNHSysCmpId".ToUpper Then
                                    If .Text = "" Then
                                        .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                    End If
                                Else
                                    .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
                                End If

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
                                .Text = _FormHeader(cind).DefaultsData(I).DataDefaults.ToString
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

        Call LoadDetail("0")
        _ProcDefault = False

    End Sub

    Private Function CheckNotUsed(ByVal Key As String) As Boolean
        Dim _Qry As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckDelFiled.ToArray.Count - 1
                _Qry = _FormHeader(cind).CheckDelFiled.Item(I).Query & "'" & HI.UL.ULF.rpQuoted(Key) & "' "

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "") <> "" Then
                    HI.MG.ShowMsg.mProcessError(1001180001, "", Me.Text, MessageBoxIcon.Warning)
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
        Dim _Qry As String = ""
        For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
            For I As Integer = 0 To _FormHeader(cind).CheckFiled.ToArray.Count - 1
                _FieldName = _FormHeader(cind).CheckFiled(I).FiledName.ToString

                If _FormHeader(0).MainKey.ToString.ToUpper <> _FieldName.ToUpper Then
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
                                    If .Text.Trim() = "" Or "" & .Properties.Tag.ToString = "" Then
                                        Pass = False
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

                End If
            Next
        Next


        Dim _CmpH As String = ""
        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")


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

                                If .Properties.Buttons.Count > 1 Then
                                    If UCase("" & .Properties.Buttons(1).Tag.ToString) = "M" Then

                                        If .Text <> "" Then

                                            _Qry = "SELECt TOP 1  FTEmpCode  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WHERE FTEmpCode=N'" & HI.UL.ULF.rpQuoted(.Text) & "' AND FNHSysEmpID<>" & Val(.Properties.Tag.ToString) & "  "

                                            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                                                HI.MG.ShowMsg.mInvalidData("", 1306120001, Me.Text)
                                                Obj.Focus()
                                                Return False
                                            End If

                                        Else

                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                            Obj.Focus()
                                            Return False

                                        End If

                                    Else

                                        If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                            _Qry = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "=N'" & HI.UL.ULF.rpQuoted(.Properties.Tag.ToString) & "' "
                                            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, _DBEnum)

                                            If _dt.Rows.Count <= 0 Then
                                                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                                Obj.Focus()
                                                Return False
                                            End If
                                        End If

                                    End If

                                Else

                                    If .Text <> HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, "", True, _CmpH).ToString() Then
                                        _Qry = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "=N'" & HI.UL.ULF.rpQuoted(.Properties.Tag.ToString) & "' "
                                        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, _DBEnum)

                                        If _dt.Rows.Count <= 0 Then
                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                            Obj.Focus()
                                            Return False
                                        End If
                                    End If

                                End If
                            End If
                        End With

                End Select
            Next
        Next

        If FTEmpIdNo_lbl.ForeColor = Color.Blue Then
            Dim _Text As String = ""
            _Qry = "SELECt TOP 1  FTEmpCode  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpIdNo=N'" & HI.UL.ULF.rpQuoted(FTEmpIdNo.Text) & "' AND FNHSysEmpID<>" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  AND ISNULL(FNEmpStatus,0) <>2   AND ISNULL(FTEmpIdNo,N'')<>''AND FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString)
            _Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            If _Text <> "" Then
                HI.MG.ShowMsg.mInvalidData("พบรหัสบัตรประชาชนซ้ำกรุณาทำการตรวจสอบ", 1006120001, Me.Text, _Text)
                FTEmpIdNo.Focus()
                Return False
            End If

        End If

        If FTAccNo.Text <> "" Then

            If IsNumeric(FTAccNo.Text) Then
                Dim _Text As String = ""
                _Qry = "SELECt TOP 1  FTEmpCode  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK) WHERE FTAccNo=N'" & HI.UL.ULF.rpQuoted(FTAccNo.Text) & "' AND FNHSysEmpID<>" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  AND ISNULL(FNEmpStatus,0) <>2   AND ISNULL(FTAccNo,N'')<>''  AND FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString)
                _Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

                If _Text <> "" Then
                    HI.MG.ShowMsg.mInvalidData("พบหมายเลขบัญชีซ้ำกรุณาทำการตรวจสอบ", 1078990001, Me.Text, _Text)
                    FTEmpIdNo.Focus()
                    Return False
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการกรอกหมายเลขบัญชี เป็นตัวเลขเท่านั้น", 202006010005, Me.Text, "")
                FTAccNo.Focus()
                Return False
            End If



        Else

        End If

        If FNHSysPayRollPayId.Text = "TRANSFER" Then
            Dim _validate_acc As String = "0"
            _Qry = "SELECt TOP 1  FNNotValidateAccno  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMEmptype WITH(NOLOCK) WHERE  FNHSysEmpTypeID=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "  "
            _validate_acc = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")
            If _validate_acc = "0" Then

                If FTAccNo.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการกรอกหมายเลขบัญชี เมื่อเลือกโอนผ่านธนาคาร", 202006010004, Me.Text, "")
                    FTAccNo.Focus()
                    Return False
                End If
            End If
        End If



        If FTTaxNo_lbl.ForeColor = Color.Blue Then
            Dim _Text As String = ""
            _Qry = "SELECt TOP 1  FTEmpCode  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK) WHERE FTTaxNo=N'" & HI.UL.ULF.rpQuoted(FTTaxNo.Text) & "' AND FNHSysEmpID<>" & Val(FNHSysEmpID.Properties.Tag.ToString) & "  AND ISNULL(FNEmpStatus,0) <>2  AND ISNULL(FTTaxNo,N'')<>'' "
            _Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            If _Text <> "" Then
                HI.MG.ShowMsg.mInvalidData("พบรหัสผู้เสียภาษีซ้ำกรุณาทำการตรวจสอบ", 1066120007, Me.Text, _Text)
                FTTaxNo.Focus()
                Return False
            End If

        End If

        Return True
    End Function

    Private Function SaveData() As Boolean

        Dim _FieldName As String
        Dim _Fields As String = ""
        Dim _Values As String = ""
        Dim _Qry As String
        Dim _Key As String = ""
        Dim _Val As String = ""
        Dim _StateNew As Boolean = False
        Dim _SystemKey As String = ""
        Dim _ManualCode As Boolean = False
        Dim _CmpH As String = ""

        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")


        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")



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

                                If .Text = HI.TL.Document.GetDocumentNo(_FormHeader(cind).SysDBName, _FormHeader(cind).SysTableName, "", True, _CmpH).ToString() Then
                                    _StateNew = True
                                Else

                                    If ("" & .Properties.Buttons(1).Tag.ToString.ToUpper) = "M" Then

                                        If Val("" & .Properties.Tag.ToString) = 0 Then

                                            _ManualCode = True
                                            _StateNew = True

                                        End If

                                    End If

                                    _Key = .Text
                                    _SystemKey = .Properties.Tag.ToString

                                    If Not (_ManualCode) Then

                                        _Qry = _FormHeader(cind).Query & "  WHERE " & _FormHeader(cind).MainKey & "=N'" & HI.UL.ULF.rpQuoted(_SystemKey) & "' "

                                        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, _DBEnum)

                                        If _dt.Rows.Count <= 0 Then

                                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                                            Obj.Focus()
                                            Return False

                                        End If

                                    End If
                                End If

                            End If

                        End With

                End Select

            Next

        Next

        If (_StateNew) Then

            If Not (_ManualCode) Then

                If RunEmpCodeByTypeAndSect Then
                    _Key = HI.TL.Document.GetDocumentNo(_FormHeader(0).SysDBName, _FormHeader(0).SysTableName, "", False, _CmpH & FNHSysEmpTypeId.Text & FNHSysSectId.Text).ToString()
                Else
                    _Key = HI.TL.Document.GetDocumentNo(_FormHeader(0).SysDBName, _FormHeader(0).SysTableName, "", False, _CmpH, HI.UL.ULDate.ConvertEnDB(FDDateStart.Text)).ToString()
                End If

            End If

            _SystemKey = HI.TL.RunID.GetRunNoID(Me.SysTableName, Me.MainKey, Conn.DB.DataBaseName.DB_HR).ToString()
        End If



        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _FoundControl As Boolean
            For cind As Integer = 0 To _FormHeader.ToArray.Count - 1
                For I As Integer = 0 To _FormHeader(cind).BaseFiled.ToArray.Count - 1
                    _FieldName = _FormHeader(cind).BaseFiled(I).FiledName.ToString
                    _FoundControl = False
                    If (_StateNew) Then


                        If UCase(_FieldName) = "FTEmpCode".ToUpper Then
                            _FoundControl = True
                            _Val = _Key
                        Else
                            For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                                _FoundControl = True
                                If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                    _Val = _SystemKey
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
                                                If "" & .Properties.Tag.ToString <> "" Then
                                                    _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                                Else
                                                    _Val = .SelectedIndex.ToString
                                                End If
                                            End With
                                        Case ENM.Control.ControlType.CheckEdit
                                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                                _Val = .EditValue.ToString
                                            End With
                                        Case ENM.Control.ControlType.PictureEdit
                                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                                If _PathEmpPic = "" Then
                                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _SystemKey.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                                Else
                                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _SystemKey.ToString & "_" & .Name.ToString, "" & _PathEmpPic)

                                                End If
                                            End With
                                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
                                            _Val = Obj.Text
                                        Case Else
                                            _Val = Obj.Text
                                    End Select
                                End If
                            Next
                        End If

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
                                    _Values &= "N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else

                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= "N'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= "N'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select

                            End Select

                        End If

                    Else

                        If UCase(_FieldName) = "FTEmpCode".ToUpper Then
                            _FoundControl = True
                            _Val = _Key
                        Else
                            For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                                _FoundControl = True
                                If UCase(_FieldName) = _FormHeader(cind).MainKey.ToUpper Then
                                    _Val = _SystemKey
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
                                                If "" & .Properties.Tag.ToString <> "" Then
                                                    _Val = HI.TL.CboList.GetListValue(.Properties.Tag.ToString, .SelectedIndex)
                                                Else
                                                    _Val = .SelectedIndex.ToString
                                                End If
                                            End With
                                        Case ENM.Control.ControlType.CheckEdit
                                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                                _Val = .EditValue.ToString
                                            End With
                                        Case ENM.Control.ControlType.PictureEdit
                                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                                ' _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _SystemKey.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)

                                                If _PathEmpPic = "" Then
                                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _SystemKey.ToString & "_" & .Name.ToString, "" & .Properties.Tag.ToString)
                                                Else

                                                    _Val = HI.UL.ULImage.SaveImage(CType(Obj, DevExpress.XtraEditors.PictureEdit), _SystemKey.ToString & "_" & .Name.ToString, "" & _PathEmpPic)

                                                End If
                                            End With
                                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.DateEdit, ENM.Control.ControlType.TextEdit
                                            _Val = Obj.Text
                                        Case Else
                                            _Val = Obj.Text
                                    End Select
                                End If
                            Next
                        End If

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
                                    _Values &= _FieldName & "=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                Case Else
                                    Select Case UCase(Microsoft.VisualBasic.Left(_FieldName, 2))
                                        Case "FC", "FN"
                                            _Values &= _FieldName & "=" & HI.UL.ULF.ChkNumeric(_Val) & ""
                                        Case "FD"
                                            _Values &= _FieldName & "=N'" & HI.UL.ULDate.ConvertEnDB(_Val) & "'"
                                        Case Else
                                            _Values &= _FieldName & "=N'" & HI.UL.ULF.rpQuoted(_Val) & "'"
                                    End Select
                            End Select
                        End If
                    End If
                Next

                If (_StateNew) Then
                    _Qry = " INSERT INTO   " & _FormHeader(cind).TableName & "(" & _Fields & ") VALUES (" & _Values & ")"
                Else
                    _Qry = " Update  " & _FormHeader(cind).TableName & " Set " & _Values & " WHERE  " & _FormHeader(cind).MainKey & "=N'" & _SystemKey.ToString & "' "
                End If

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                If (_StateNew) And FTEmpBarcode.Text.Trim() = "" And FNUseBarcode.SelectedIndex = 1 Then

                End If

            Next

            'For Each Row As DataRow In CType(Me.ogdLeave.DataSource, DataTable).Rows
            '    _Qry = "SELECT Top 1 FNHSysEmpID "
            '    _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeLeave "
            '    _Qry &= vbCrLf & " WHERE FNHSysEmpID=N'" & _SystemKey & "'"
            '    _Qry &= vbCrLf & " AND FTLeaveCode=N'" & HI.UL.ULF.rpQuoted(Row!FTLeaveCode.ToString) & "'"

            '    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then

            '        _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeLeave (FNHSysEmpID,FTLeaveCode,FNLeaveRight,FTInsUser,FDInsDate,FTInsTime)"
            '        _Qry &= vbCrLf & " VALUES ('" & _SystemKey & "',N'" & HI.UL.ULF.rpQuoted(Row!FTLeaveCode.ToString) & "'," & Val(Row!FNLeaveRight.ToString)
            '        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            '        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            '        _Qry &= vbCrLf & " )"

            '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            Return False
            '        End If

            '    Else

            '        _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeLeave SET FNLeaveRight=" & Val(Row!FNLeaveRight.ToString)
            '        _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '        _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB
            '        _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
            '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Integer.Parse(Val(_SystemKey)) & ""
            '        _Qry &= vbCrLf & " AND FTLeaveCode=N'" & HI.UL.ULF.rpQuoted(Row!FTLeaveCode.ToString) & "'"

            '        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            Return False
            '        End If

            '    End If
            'Next

            _Qry = "DELETE FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_SystemKey) & ""

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each Row As DataRow In CType(Me.ogdIncome.DataSource, DataTable).Rows

                _Qry = "SELECT TOP 1  FNHSysEmpID "
                _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_SystemKey) & ""
                _Qry &= vbCrLf & " AND FTFinCode=N'" & HI.UL.ULF.rpQuoted(Row!FTFinCode.ToString) & "'"

                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin (FNHSysEmpID,FTFinCode,FTFinAmt,FTInsUser,FDInsDate,FTInsTime)"
                    _Qry &= vbCrLf & " VALUES (" & Val(_SystemKey) & ",N'" & HI.UL.ULF.rpQuoted(Row!FTFinCode.ToString) & "'," & Val(Row!FTFinAmt.ToString) & " "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ")"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Else

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin SET FTFinAmt=" & Val(Row!FTFinAmt.ToString) & " "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_SystemKey) & ""
                    _Qry &= vbCrLf & " AND FTFinCode=N'" & HI.UL.ULF.rpQuoted(Row!FTFinCode.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
            Next

            For Each Row As DataRow In CType(Me.ogdDeduct.DataSource, DataTable).Rows

                _Qry = "SELECT TOP 1 FNHSysEmpID "
                _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_SystemKey) & ""
                _Qry &= vbCrLf & " AND FTFinCode=N'" & HI.UL.ULF.rpQuoted(Row!FTFinCode.ToString) & "'"

                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin (FNHSysEmpID,FTFinCode,FTFinAmt,FTInsUser,FDInsDate,FTInsTime)"
                    _Qry &= vbCrLf & " VALUES (" & Val(_SystemKey) & ",N'" & HI.UL.ULF.rpQuoted(Row!FTFinCode.ToString) & "'," & Val(Row!FTFinAmt.ToString) & " "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ")"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                Else

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin SET FTFinAmt=" & Val(Row!FTFinAmt.ToString) & " "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_SystemKey) & ""
                    _Qry &= vbCrLf & " AND FTFinCode=N'" & HI.UL.ULF.rpQuoted(Row!FTFinCode.ToString) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
            Next




            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            For Each Obj As Object In Me.Controls.Find(_FormHeader(0).MainKey, True)
                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.ButtonEdit
                        With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                            .Properties.Tag = _SystemKey
                            .Text = _Key
                        End With
                End Select
            Next

            Call SavePassImage()
            Call SaveVisaImage()
            Call SaveMOUImage()
            Call SaveWorkImage()
            Call SaveOtherImage()

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function DeleteData(ByVal Key As String) As Boolean
        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Qry As String
            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WHERE FNHSysEmpID=" & Val(Key) & " "
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFin WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeLeave WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeChild WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFamily WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeWeekly WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeEducation WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeMasterChange WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeMasterChangeSlary WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeUpdateSlary WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = "Delete From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeMedicalExpenses WHERE FNHSysEmpID=" & Val(Key) & " "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

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

    Private Sub LoadData(ByVal HSysId As String)
        Dim _Qry As String = Me.Query & "  WHERE " & Me.MainKey & "=N'" & HSysId & "' "
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, _DBEnum)
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

                                    If "" & .Properties.Tag.ToString <> "" Then
                                        .SelectedIndex = HI.TL.CboList.GetIndexByValue(.Properties.Tag.ToString, R.Item(Col).ToString)
                                    Else
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    End If

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
    End Sub

    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)

        Me.FNHSysEmpID.Text = ""
        Me.FNHSysEmpID.Properties.Tag = ""

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

    Private Function CheckWriteFile() As Boolean
        Try
            Dim _PathEmpPic As String
            _PathEmpPic = ""
            Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

            _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")



            If _PathEmpPic <> "" Then

                If (Not System.IO.Directory.Exists(_PathEmpPic)) Then
                    System.IO.Directory.CreateDirectory(_PathEmpPic)
                End If

                If (Not System.IO.Directory.Exists(_PathEmpPic & "\TestXML")) Then
                    System.IO.Directory.CreateDirectory(_PathEmpPic & "\TestXML")
                End If
                System.IO.Directory.Delete(_PathEmpPic & "\TestXML")

                Return True
            Else
                Return True
            End If



        Catch ex As Exception
            Return False
        End Try



    End Function


#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If CheckWriteFile() Then
            If Me.VerrifyData Then

                If FTEmpBarcode.Text.Trim() = "" And FNUseBarcode.SelectedIndex = 1 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNUseBarcode_lbl.Text)
                    FTEmpBarcode.Focus()
                    Exit Sub
                End If

                If FTEmpIdNo.Focused Then
                    FTEmpIdNo_Leave(FTEmpIdNo, New System.EventArgs)
                End If

                Dim _Qry As String = ""
                _Qry = "SELECT TOP 1 P.FNHSysEmpID"
                _Qry &= vbCrLf & " FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTPayRoll AS P WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMCfgPayHD AS PD WITH (NOLOCK) "
                _Qry &= vbCrLf & "ON P.FNHSysEmpTypeId = PD.FNHSysEmpTypeId "
                _Qry &= vbCrLf & " 	AND P.FTPayYear  = PD.FTPayYear"
                _Qry &= vbCrLf & " 	AND P.FTPayTerm  = PD.FTPayTerm"
                _Qry &= vbCrLf & " WHERE P.FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & ""

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                    HI.MG.ShowMsg.mInfo("พนักงานมีการคำนวณสิ้นงวดในงวดปัจจุบันแล้ว หลังการบันทึกกรุณาแจ้งฝ่ายเงินเดือน !!!", 1300001111, Me.Text, FNHSysEmpID.Text, MessageBoxIcon.Warning)
                End If

                Dim _Spls As New HI.TL.SplashScreen("Save Data.. Please Wait ")
                If Me.SaveData() Then

                    Call CheckPermissionSalary()

                    Try
                        If HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_HR) <> HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_HR_PAYROLL) And HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_HR_PAYROLL) <> "" Then
                            Call UpdateDataEmpToOtherServer(Integer.Parse(Val(FNHSysEmpID.Properties.Tag.ToString)))
                        End If
                    Catch ex As Exception

                    End Try
                    Call SaveStricken()
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If


            End If

        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถบันทึกรูปพนักงานได้ กรุณาติดต่อ  Admin!!!!", 202002210001, Me.Text)
        End If
    End Sub

    Private Sub UpdateDataEmpToOtherServer(FNHSysEmpID As Integer)
        Dim _Qry As String = ""
        Dim _StrFiled As String = ""
        Try


            _StrFiled = " FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH, "
            _StrFiled &= " FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,"
            _StrFiled &= "FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,"
            _StrFiled &= "FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,"
            _StrFiled &= "FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,"
            _StrFiled &= "FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,"
            _StrFiled &= "FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,"
            _StrFiled &= "FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,"
            _StrFiled &= "FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,"
            _StrFiled &= "FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,"
            _StrFiled &= "FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FTHealthInsurIDFather, FTHealthInsurIDMother,"
            _StrFiled &= "FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver, FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode,FNEnablonType"

            _Qry = " UPDATE A SET"
            _Qry &= vbCrLf & " FTInsUser=B.FTInsUser"

            For Each Str As String In _StrFiled.Split(",")
                _Qry &= vbCrLf & " ," & Str.Trim() & "=B." & Str.Trim() & ""
            Next

            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR_PAYROLL) & ".dbo.THRMEmployee AS A"
            HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_HR)
            _Qry &= vbCrLf & " , OPENDATASOURCE ('SQLOLEDB',N'Data Source=" & HI.Conn.DB.SerVerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "')." & HI.Conn.DB.BaseName & ".dbo.THRMEmployee AS B "
            _Qry &= vbCrLf & " WHERE A.FNHSysEmpID=B.FNHSysEmpID  AND B.FNHSysEmpID=" & FNHSysEmpID & ""

            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR_PAYROLL) = False Then

                _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR_PAYROLL) & ".dbo.THRMEmployee"
                _Qry &= vbCrLf & "("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH,"
                _Qry &= vbCrLf & "FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,"
                _Qry &= vbCrLf & " FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,"
                _Qry &= vbCrLf & "  FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,"
                _Qry &= vbCrLf & "  FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,"
                _Qry &= vbCrLf & "  FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,"
                _Qry &= vbCrLf & "  FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,"
                _Qry &= vbCrLf & " FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,"
                _Qry &= vbCrLf & " FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,"
                _Qry &= vbCrLf & " FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,"
                _Qry &= vbCrLf & " FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FTHealthInsurIDFather, FTHealthInsurIDMother,"
                _Qry &= vbCrLf & " FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver, FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode,FNEnablonType"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & "SELECT TOP 1 FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH,"
                _Qry &= vbCrLf & "FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,"
                _Qry &= vbCrLf & " FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,"
                _Qry &= vbCrLf & "  FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,"
                _Qry &= vbCrLf & "  FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,"
                _Qry &= vbCrLf & "  FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,"
                _Qry &= vbCrLf & "  FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,"
                _Qry &= vbCrLf & " FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,"
                _Qry &= vbCrLf & " FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,"
                _Qry &= vbCrLf & " FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,"
                _Qry &= vbCrLf & " FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FTHealthInsurIDFather, FTHealthInsurIDMother,"
                _Qry &= vbCrLf & " FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver, FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode,FNEnablonType"
                HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_HR)
                _Qry &= vbCrLf & " FROM  OPENDATASOURCE ('SQLOLEDB',N'Data Source=" & HI.Conn.DB.SerVerName & ";User ID=" & HI.Conn.DB.UIDName & ";Password=" & HI.Conn.DB.PWDName & "')." & HI.Conn.DB.BaseName & ".dbo.THRMEmployee AS B "
                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & FNHSysEmpID & " "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR_PAYROLL)

            End If


        Catch ex As Exception
        End Try

    End Sub

    Private Sub DeleteDataEmpToOtherServer(FNHSysEmpID As Integer)
        Dim _Qry As String = ""
        Dim _StrFiled As String = ""
        Try

            _Qry = "DELETE  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR_PAYROLL) & ".dbo.THRMEmployee "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & FNHSysEmpID & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR_PAYROLL)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.FNHSysEmpID.Text <> "" And ("" & FNHSysEmpID.Properties.Tag.ToString) <> "" Then

            If HI.HRCAL.Calculate.CheckPaidPayroll("" & FNHSysEmpID.Properties.Tag.ToString) Then
                HI.MG.ShowMsg.mInfo("พนักงานมีการทำจ่ายเงินเดือนไปแล้วไม่สามารถทำการลบข้อมูลพนักงานได้", 1403150001, Me.Text, FNHSysEmpID.Text)
                Exit Sub
            End If

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, FNHSysEmpID.Text, Me.Text) = True Then
                If Me.DeleteData(FNHSysEmpID.Properties.Tag.ToString) Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                    Try
                        If HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_HR) <> HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_HR_PAYROLL) And HI.Conn.DB.GetServerName(Conn.DB.DataBaseName.DB_HR_PAYROLL) <> "" Then
                            Call DeleteDataEmpToOtherServer(Integer.Parse(Val(FNHSysEmpID.Properties.Tag.ToString)))
                        End If
                    Catch ex As Exception

                    End Try

                    HI.TL.HandlerControl.ClearControl(Me)
                    FTEmpIdNo.Text = ""
                    FNHSysEmpID.Text = ""
                    Me.DefaultsData()
                    Me.FNHSysEmpID.Focus()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysEmpID_lbl.Text)
            FNHSysEmpID.Focus()
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        Dim _Spls As New HI.TL.SplashScreen("Clearing..... Please Wait.")
        Me.FormRefresh()
        ogcdetail.DataSource = Nothing
        ogcvisa.DataSource = Nothing
        ogcMOU.DataSource = Nothing
        ogcWork.DataSource = Nothing
        ogcOther.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogvvisa)
        FTDiseaseNote.Text = ""
        FTSurgeryNote.Text = ""
        FTImmunityNote.Text = ""
        FTRelationD.Text = ""
        FTDiseaseD.Text = ""
        FTRelationM.Text = ""
        FTDiseaseM.Text = ""
        FTRelationS.Text = ""
        FTDiseaseS.Text = ""
        FTDrugDiseaseNote.Text = ""
        FTHobbyNote.Text = ""
        FTSmoking.Text = ""
        FTYearSmoking.Text = ""
        FTMonthSmoking.Text = ""
        FTSmokingQ.Text = ""
        FTYearAlcohol.Text = ""
        FTMonthAlcohol.Text = ""
        FTDopeNote.Text = ""
        FTOther.Text = ""

        _Spls.Close()
    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Protected Sub ShowLeaveInfo(ByVal EmpCode As String)


        Dim _Qry As String
        Dim tResetLeave As String

        Try
            Dim tEmpType As String = FNHSysEmpTypeId.Properties.Tag.ToString

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
            Dim sDate As String = Year(Date.Today) & "/" & Month(Date.Today) & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
            Dim eDate As String = Year(Date.Today) & "/" & Month(Date.Today) & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today)

            If Year(Date.Today) > 2400 Then
                sDate = Year(Date.Today) - 543 & "/" & Month(Date.Today).ToString.PadLeft(2, "0") & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
            Else
                sDate = Year(Date.Today) & "/" & Month(Date.Today).ToString.PadLeft(2, "0") & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
            End If

            sDate = HI.UL.ULDate.ConvertEnDB(Me.FDDateStart.Text)
            eDate = HI.UL.ULDate.ConvertEnDB(Me.FDDateEnd.Text)

            'Dim oDbdt As DataTable

            Dim nYear As Integer = 0
            Dim nMonth As Integer = 0
            _Qry = "SP_Datediff '" & sDate & "',N'" & eDate & "'"
            Dim oRow As DataRow = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows(0)
            nYear = oRow("FNYear")
            nMonth = (nYear * 12) + oRow("FNMonth")


            Dim VacationLeaveType As String = ""
            _Qry = " SELECT TOP 1 FTCfgData"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  (FTCfgName = N'VacationLeaveType')"

            VacationLeaveType = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "0")

            Dim LeaveVacation As Double = 0
            If VacationLeaveType = "1" Then
                _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation_Th(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,N''),ISNULL(FDDateEnd,N''),ISNULL(FDDateProbation,N''),'') AS FNEmpVacation"
                _Qry &= vbCrLf & "   FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  AS M WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(EmpCode) & " "
            ElseIf VacationLeaveType = "2" Then
                _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation_Laos(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,''),ISNULL(FDDateEnd,''),ISNULL(FDDateProbation,'')) AS FNEmpVacation"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(EmpCode) & " "

            Else
                _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,N''),ISNULL(FDDateEnd,N''),ISNULL(FDDateProbation,N'')) AS FNEmpVacation"
                _Qry &= vbCrLf & "   FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  AS M WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(EmpCode) & " "

            End If


            LeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset AS FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM  THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " ) As T"

            tResetLeave = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


            '_Qry = " SELECT  (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(ISNULL(FNAbsent,0))/ 480.00))),3)"
            _Qry = " SELECT  (( Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(ISNULL(FNAbsent,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((SUM(ISNULL(FNAbsent,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((SUM(ISNULL(FNAbsent,0)) % 480.00) % 60.00))),2))  AS FNAbsent"
            _Qry &= vbCrLf & " FROM THRTTrans WITH(Nolock)"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & "  "
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "' AND FTDateTrans<=Convert(varchar(10),Getdate(),111)"
            FTAbsentH.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")

            '_Qry = " SELECT  (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(ISNULL(FNLateNormalMin,0))/ 480.00))),3)"
            _Qry = " SELECT  (( Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(ISNULL(FNLateNormalMin,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((SUM(ISNULL(FNLateNormalMin,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((SUM(ISNULL(FNLateNormalMin,0)) % 480.00) % 60.00))),2))  AS FNLateNormalMin"
            _Qry &= vbCrLf & " FROM THRTTrans WITH(Nolock)"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & "  "
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'  AND FTDateTrans<=Convert(varchar(10),Getdate(),111) "
            FTLateMin.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")





            _Qry = " SELECT FTLeaveCode,FTLeaveName, FNLeaveRight, FNLeaveUsed, FNLeaveBal, FNSortSeq FROM  "
            _Qry &= vbCrLf & " ( "
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FTLeaveName "

            '_Qry &= vbCrLf & ", (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ 480.00))),3)"
            _Qry &= vbCrLf & ", (( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveRight,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveRight,0)) % 480.00) % 60.00))),2))  AS FNLeaveRight"

            '_Qry &= vbCrLf & " , (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ 480.00))),3)"
            _Qry &= vbCrLf & " , (( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveUsed,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveUsed,0)) % 480.00) % 60.00))),2))  AS FNLeaveUsed"

            '_Qry &= vbCrLf & " , (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0))/ 480.00))),3)"
            _Qry &= vbCrLf & " , ((Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveBal,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveBal,0)) % 480.00) % 60.00))),2))  AS FNLeaveBal"

            _Qry &= vbCrLf & " FROM  (SELECT V_LeaveType.FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ",Cast((ISNULL(FNLeaveRight,0) * 480) AS numeric(18,0)) AS FNLeaveRight"
            _Qry &= vbCrLf & ",ISNULL(FNTotalMinute,0) AS FNLeaveUsed"
            _Qry &= vbCrLf & ",(Cast((ISNULL(FNLeaveRight,0) * 480) AS numeric(18,0))) - ISNULL(FNTotalMinute,0)   AS FNLeaveBal"

            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT CAST(FNListIndex AS varchar(3)) AS FTLeaveCode," & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & " AS FTLeaveName "
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_LeaveType WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNListIndex<>98"
            _Qry &= vbCrLf & ") AS V_LeaveType"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select THRMConfigLeave.FTLeaveCode"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeLeave.FNLeaveRight,-1)=-1 THEN Cast(ISNULL(THRMConfigLeave.FNLeaveRight,0) AS numeric(18,0)) ELSE Cast(ISNULL(THRMEmployeeLeave.FNLeaveRight,0) AS numeric(18,0)) END AS FNLeaveRight"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMConfigLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId=" & Val(tEmpType) & " "
            _Qry &= vbCrLf & ") THRMConfigLeave"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,Cast(ISNULL(FNLeaveRight,0) AS numeric(18,2)) AS FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMEmployeeLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & ") THRMEmployeeLeave"
            _Qry &= vbCrLf & " ON THRMConfigLeave.FTLeaveCode=THRMEmployeeLeave.FTLeaveCode"
            _Qry &= vbCrLf & ") T ON V_LeaveType.FTLeaveCode=T.FTLeaveCode"
            _Qry &= vbCrLf & " LEFT JOIN "
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType<>N'98'"
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & ") AS THRTTransLeave"
            _Qry &= vbCrLf & " ON V_LeaveType.FTLeaveCode=THRTTransLeave.FTLeaveType) AS MM1"

            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "SELECT FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & " , (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveRight,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveRight,0)) % 480.00) % 60.00))),2))  AS FNLeaveRight"

            _Qry &= vbCrLf & ", (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveUsed,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveUsed,0)) % 480.00) % 60.00))),2))  AS FNLeaveUsed"

            _Qry &= vbCrLf & " , (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0) - (ISNULL(FNLeaveBal,0) % 480) )/ 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveBal,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveBal,0)) % 480.00) % 60.00))),2))  AS FNLeaveBal"


            _Qry &= vbCrLf & " FROM (SELECT  V_LeaveType.FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ",Cast((ISNULL(FNLeaveRight," & LeaveVacation & ") * 480)  AS numeric(18,0)) AS FNLeaveRight"
            _Qry &= vbCrLf & ",ISNULL(FNTotalMinute,0) AS FNLeaveUsed"
            _Qry &= vbCrLf & ",(Cast((ISNULL(FNLeaveRight," & LeaveVacation & ") * 480)  AS numeric(18,0))) -ISNULL(FNTotalMinute,0)   AS FNLeaveBal"


            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT CAST(FNListIndex AS varchar(3)) AS FTLeaveCode," & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & " AS FTLeaveName "
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_LeaveType WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNListIndex=98"
            _Qry &= vbCrLf & ") AS V_LeaveType"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select THRMConfigLeave.FTLeaveCode"
            _Qry &= vbCrLf & "," & LeaveVacation & " AS FNLeaveRight"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMConfigLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(tEmpType) & " "
            _Qry &= vbCrLf & ") THRMConfigLeave"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode," & LeaveVacation & " AS FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMEmployeeLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & ") THRMEmployeeLeave"
            _Qry &= vbCrLf & " ON THRMConfigLeave.FTLeaveCode=THRMEmployeeLeave.FTLeaveCode"
            _Qry &= vbCrLf & ") T ON V_LeaveType.FTLeaveCode=T.FTLeaveCode"
            _Qry &= vbCrLf & " LEFT JOIN "
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "( Select FTLeaveType,SUM(FNTotalMinute) As FNTotalMinute "
            _Qry &= vbCrLf & " FROM ( "
            _Qry &= vbCrLf & " Select FTLeaveType,SUM(FNTotalMinute) As FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  With(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType=N'98'"
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & "union all "
            _Qry &= vbCrLf & " SELECT '98' as  FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType=N'999' AND isnull(FTStateDeductVacation,'0') = '1' "
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & "union all "
            _Qry &= vbCrLf & " SELECT '98' as  FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType=N'998' AND isnull(FTStateDeductVacation,'0') = '1' "
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & " ) A GROUP BY FTLeaveType"
            _Qry &= vbCrLf & ") AS THRTTransLeave"
            _Qry &= vbCrLf & " ON V_LeaveType.FTLeaveCode=THRTTransLeave.FTLeaveType) AS MM2 "
            _Qry &= vbCrLf & "  ) TL "
            _Qry &= vbCrLf & " LEFT  JOIN (SELECT   FNListIndex ,CASE WHEN ISNULL(FNSortSeq,0) >0 THEN ISNULL(FNSortSeq,0)  ELSE   FNListIndex END FNSortSeq "
            _Qry &= vbCrLf & " FROM  HITECH_SYSTEM.dbo.HSysListData WITH(NOLOCK) "
            _Qry &= vbCrLf & " where FTListName = 'fnleavetype' "
            _Qry &= vbCrLf & "  ) V_LeaveSeq ON TL.FTLeaveCode=V_LeaveSeq.FNListIndex "
            _Qry &= vbCrLf & " ORDER BY V_LeaveSeq.FNSortSeq "
            ogdLeave.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
        End Try

    End Sub
    Private Sub ShowEducation(ByVal EmpCode As String)

        Dim _Qry As String

        Try

            _Qry = "SELECT FNSeqNo"

            _Qry &= vbCrLf & ", B.FTCourseCode AS FTCourCode,U.FTUniversityCode,UB.FTUniversityBranchCode,F.FTFacultyCode,FB.FTFacultyBranchCode,FM.FTFacultyMajorCode"
            _Qry &= vbCrLf & ",A.FNHSysCourseId,A.FNHSysUniversityId,A.FNHSysUniversityBranchId,A.FNHSysFacultyId,A.FNHSysFacultyBranchId,A.FNHSysFacultyMajorId"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTCourseNameTH AS FTCourName"
                _Qry &= vbCrLf & ",UB.FTUniversityBranchNameTH AS FTUniversityBranchName"
                _Qry &= vbCrLf & ",U.FTUniversityNameTH AS FTUniversityName"
                _Qry &= vbCrLf & ",F.FTFacultyNameTH AS FTFacultyName"
                _Qry &= vbCrLf & ",FB.FTFacultyBranchNameTH AS FTFacultyBranchName"
                _Qry &= vbCrLf & ",FM.FTFacultyMajorNameTH AS FTFacultyMajorName"
            Else
                _Qry &= vbCrLf & ",FTCourseNameEN AS FTCourName"
                _Qry &= vbCrLf & ",UB.FTUniversityBranchNameEN AS FTUniversityBranchName"
                _Qry &= vbCrLf & ",U.FTUniversityNameEN AS FTUniversityName"
                _Qry &= vbCrLf & ",F.FTFacultyNameEN AS FTFacultyName"
                _Qry &= vbCrLf & ",FB.FTFacultyBranchNameEN AS FTFacultyBranchName"
                _Qry &= vbCrLf & ",FM.FTFacultyMajorNameEN AS FTFacultyMajorName"
            End If

            _Qry &= vbCrLf & ",A.FTYearBegin,A.FTYearEnd,A.FCGrade"
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeEducation AS A  "
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMCourse AS B ON A.FNHSysCourseId=B.FNHSysCourseId"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMUniversity AS U WITH(NOLOCK)  ON A.FNHSysUniversityId=U.FNHSysUniversityId"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMUniversityBranch AS UB WITH(NOLOCK)  ON A.FNHSysUniversityBranchId=UB.FNHSysUniversityBranchId"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMFaculty AS F WITH(NOLOCK)  ON A.FNHSysFacultyId=F.FNHSysFacultyId"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMFacultyBranch AS FB WITH(NOLOCK)  ON A.FNHSysFacultyBranchId=FB.FNHSysFacultyBranchId"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMFacultyMajor AS FM WITH(NOLOCK)  ON A.FNHSysFacultyMajorId=FM.FNHSysFacultyMajorId"
            _Qry &= vbCrLf & " WHERE  A.FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " ORDER BY FNSeqNo "

            Me.ogceduc.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception



        End Try
    End Sub

    Private Sub ShowResign(ByVal EmpCode As String)
        Dim _Qry As String

        Try

            _Qry = "SELECT CASE WHEN ISDATE(FDDateBegin)=1 THEN SUBSTRING(FDDateBegin,9,2)+'/'+SUBSTRING(FDDateBegin,6,2)+'/'+SUBSTRING(FDDateBegin,1,4) ELSE '' END AS FDDateBegin"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDDateResign)=1 THEN SUBSTRING(FDDateResign,9,2)+'/'+SUBSTRING(FDDateResign,6,2)+'/'+SUBSTRING(FDDateResign,1,4) ELSE '' END AS FDDateResign"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDDelEMailDate)=1 THEN SUBSTRING(FDDelEMailDate,9,2)+'/'+SUBSTRING(FDDelEMailDate,6,2)+'/'+SUBSTRING(FDDelEMailDate,1,4) ELSE '' END AS FDDelEMailDate"
            _Qry &= vbCrLf & ",ISNULL(FTRetEmpCard,N'0') AS FTRetEmpCard"
            _Qry &= vbCrLf & ",ISNULL(FTDestroyCard,N'0') AS FTDestroyCard"
            _Qry &= vbCrLf & ",ISNULL(FTBackListSta,N'0') AS FTBackListSta"
            _Qry &= vbCrLf & ",ISNULL(FTRetEquipment,N'0') AS FTRetEquipment"
            _Qry &= vbCrLf & ",FTResignNote"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTResignNameTH AS FTResignName"
            Else
                _Qry &= vbCrLf & ",FTResignNameEN AS FTResignName"
            End If

            _Qry &= vbCrLf & ",THRTEmployeeResign.FNHSysResignId"

            _Qry &= vbCrLf & " FROM THRTEmployeeResign WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMResign THRMResign WITH(NOLOCK) "
            _Qry &= vbCrLf & " ON THRTEmployeeResign.FNHSysResignId=THRMResign.FNHSysResignId"
            _Qry &= vbCrLf & " WHERE  THRTEmployeeResign.FNHSysEmpID =" & Val(EmpCode) & ""

            ogdResign.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowPunishment(ByVal EmpCode As String)
        Dim _Qry As String

        Try
            _Qry = "SELECT FNSeqNo"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDDatePunishment)=1 THEN SUBSTRING(FDDatePunishment,9,2)+'/'+SUBSTRING(FDDatePunishment,6,2)+'/'+SUBSTRING(FDDatePunishment,1,4) ELSE '' END AS FDDatePunishment"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDDateEndPunishment)=1 THEN SUBSTRING(FDDateEndPunishment,9,2)+'/'+SUBSTRING(FDDateEndPunishment,6,2)+'/'+SUBSTRING(FDDateEndPunishment,1,4) ELSE '' END AS FDDateEndPunishment"
            _Qry &= vbCrLf & ",THRMPunishmentLevel.FTPunishmentLvCode AS FTLevelCode"
            _Qry &= vbCrLf & ",FTCause,FTPunishmentBy ,THRTEmployeePunishment.FTSuspended "

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTPunishmentLvNameTH AS FTLevelName"
            Else
                _Qry &= vbCrLf & ",FTPunishmentLvNameEN AS FTLevelName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(FTStaOutstanding,N'0') AS FTStaOutstanding"
            _Qry &= vbCrLf & " ,THRTEmployeePunishment.FNHSysPunishmentLvId"
            _Qry &= vbCrLf & " FROM THRTEmployeePunishment WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMPunishmentLevel THRMPunishmentLevel WITH(NOLOCK) "
            _Qry &= vbCrLf & " ON THRTEmployeePunishment.FNHSysPunishmentLvId=THRMPunishmentLevel.FNHSysPunishmentLvId"
            _Qry &= vbCrLf & " WHERE  THRTEmployeePunishment.FNHSysEmpID =" & Val(EmpCode) & ""

            ogdPunishment.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowTrainning(ByVal EmpCode As String)
        Dim _Qry As String
        'If ogctrain.DataSource Is Nothing Then
        '    Call InitialGridTraining()
        'Else
        '    Exit Sub
        'End If


        Try
            '_Qry = "SELECT ROW_NUMBER() OVER(ORDER BY B.FDDateBegin) AS FNSeqNo,B.FTTrainCode"

            'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            '    _Qry &= vbCrLf & ",FTTrainDesc2 AS FTTrainDesc"
            '    _Qry &= vbCrLf & ",E.FTNameTH AS FTType"
            '    _Qry &= vbCrLf & ",D.FTNameTH AS FTEvaluate"

            'Else
            '    _Qry &= vbCrLf & ",FTTrainDesc1 AS FTTrainDesc"
            '    _Qry &= vbCrLf & ",E.FTNameEN AS FTType"
            '    _Qry &= vbCrLf & ",D.FTNameEN AS FTEvaluate"
            'End If

            '_Qry &= vbCrLf & ",FTTrainer"
            '_Qry &= vbCrLf & ",CASE WHEN ISDATE(FDDateBegin)=1 THEN SUBSTRING(FDDateBegin,9,2)+'/'+SUBSTRING(FDDateBegin,6,2)+'/'+SUBSTRING(FDDateBegin,1,4) ELSE '' END AS FDDateBegin"
            '_Qry &= vbCrLf & ",CASE WHEN ISDATE(FDDateEnd)=1 THEN SUBSTRING(FDDateEnd,9,2)+'/'+SUBSTRING(FDDateEnd,6,2)+'/'+SUBSTRING(FDDateEnd,1,4) ELSE '' END AS FDDateEnd"
            '_Qry &= vbCrLf & ",FTLocation,FCCostPerEmp"
            '_Qry &= vbCrLf & " ,A.FTTrainNote"
            '_Qry &= vbCrLf & ",A.FTDocNo,FDDocDate"
            '_Qry &= vbCrLf & " FROM THRTTrainEmp  AS A WITH(NOLOCK) LEFT JOIN THRTTrain AS B WITH(NOLOCK)"
            '_Qry &= vbCrLf & " ON A.FTDocNo=B.FTDocNo"
            '_Qry &= vbCrLf & " LEFT JOIN THRMTrain AS C WITH(NOLOCK) ON B.FTTrainCode=C.FTTrainCode"
            '_Qry &= vbCrLf & " LEFT JOIN dbo.V_TrainEvaluate AS D ON ISNULL(A.FTEvaluate,N'0')=CAST(D.FNListIndex AS Char(1))"
            '_Qry &= vbCrLf & " LEFT JOIN dbo.V_TrainType As E ON ISNULL(FNTrainType,N'')=CAST(E.FNListIndex AS Char(1))"
            '_Qry &= vbCrLf & " WHERE  A.FNHSysEmpID =" & Val(EmpCode) & ""
            _Qry = "SELECT B.FTTrainCode"
            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ",FTTrainDesc2 AS FTTrainDesc"
                _Qry &= vbCrLf & ",TT.FTNameTH AS FTType"
                _Qry &= vbCrLf & ",TF.FTNameTH AS FTPurpose"
                _Qry &= vbCrLf & ",D.FTNameTH AS FTEvaluate"
            Else
                _Qry &= vbCrLf & ",FTTrainDesc1 AS FTTrainDesc"
                _Qry &= vbCrLf & ",TT.FTNameEN AS FTType"
                _Qry &= vbCrLf & ",TF.FTNameEN AS FTPurpose"
                _Qry &= vbCrLf & ",D.FTNameEN AS FTEvaluate"
            End If
            _Qry &= vbCrLf & ",B.FTStartTime,B.FTEndTime,B.FTTotalHour AS FNSumHours"
            _Qry &= vbCrLf & ",case when B.FTTrainer<> '' then B.FTTrainer else K.FTTrainer end AS FTTrainer"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(B.FDDateBegin)=1 THEN SUBSTRING(B.FDDateBegin,9,2)+'/'+SUBSTRING(B.FDDateBegin,6,2)+'/'+SUBSTRING(B.FDDateBegin,1,4) ELSE '' END AS FDDateBegin"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(B.FDDateEnd)=1 THEN SUBSTRING(B.FDDateEnd,9,2)+'/'+SUBSTRING(B.FDDateEnd,6,2)+'/'+SUBSTRING(B.FDDateEnd,1,4) ELSE '' END AS FDDateEnd"
            _Qry &= vbCrLf & ",FTLocation,FCCostPerEmp"
            _Qry &= vbCrLf & ",A.FTTrainNote,A.FTDocNo,FDDocDate"
            _Qry &= vbCrLf & "FROM THRTTrainEmp  AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "LEFT JOIN THRTTrain AS B WITH(NOLOCK) ON A.FTDocNo=B.FTDocNo"
            _Qry &= vbCrLf & "LEFT JOIN THRMTrain AS C WITH(NOLOCK) ON B.FTTrainCode=C.FTTrainCode"
            _Qry &= vbCrLf & "LEFT JOIN THRTTrainLecturer AS K WITH(NOLOCK) ON B.FTDocNo=K.FTDocNo"
            _Qry &= vbCrLf & "LEFT JOIN dbo.V_TrainEvaluate AS D ON ISNULL(A.FTEvaluate,N'0')=CAST(D.FNListIndex AS Char(1))"
            _Qry &= vbCrLf & "LEFT JOIN "
            _Qry &= vbCrLf & "(select A.FNListIndex,A.FTListName,A.FTNameTH,A.FTNameEN from HITECH_SYSTEM.dbo.HSysListData AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE A.FTListName='FNTrainType') AS TT ON B.FNTrainType=TT.FNListIndex "
            _Qry &= vbCrLf & "LEFT OUtER JOIN "
            _Qry &= vbCrLf & "(select A.FNListIndex,A.FTListName,A.FTNameTH,A.FTNameEN from HITECH_SYSTEM.dbo.HSysListData AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "WHERE A.FTListName='FNFixTrainTrainning') AS TF ON B.FNFixTrainTrainning=TF.FNListIndex  "
            _Qry &= vbCrLf & " WHERE  A.FNHSysEmpID =" & Me.FNHSysEmpID.Properties.Tag & ""


            dtDefault = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.ogctrain.DataSource = dtDefault
        Catch ex As Exception

        End Try

    End Sub

    '#Region "CreateGridBand ShowTraining"

    '    Private Sub InitialGridTraining()
    '        Call CreateColBand()
    '        Call CreateGridBand()
    '    End Sub

    '    Private Function GetDataTrainType() As DataTable
    '        Dim Qry As String
    '        Qry = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.SP_GetListTypeTraining " & 0 & ""
    '        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

    '    End Function

    '    Private Function GetDataPurpose() As DataTable
    '        Dim Qry As String
    '        Qry = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.SP_GetListTypeTraining " & 1 & ""
    '        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
    '    End Function

    '    Private Sub CreateColBand()
    '        Dim _ColN As Integer = 0
    '        'Dim dtTrainType As DataTable
    '        'Dim dtPurpose As DataTable
    '        Try
    '            With ogb
    '                With .OptionsView
    '                    .ShowFooter = True
    '                    .ShowColumnHeaders = False
    '                End With
    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FNSeqNo"
    '                    .FieldName = "FNSeqNo"
    '                    .Caption = "FNSeqNo"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    .Width = 55
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FTDocNo"
    '                    .FieldName = "FTDocNo"
    '                    .Caption = "FTDocNo"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    .Width = 150
    '                    _ColN += 1
    '                End With


    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "cFTTrainCode"
    '                    .FieldName = "cFTTrainCode"
    '                    .Caption = "cFTTrainCode"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    .Width = 90
    '                    _ColN += 1
    '                End With


    '                dtTrainType = GetDataTrainType()
    '                For Each R As DataRow In dtTrainType.Rows
    '                    .Columns.Add()
    '                    With .Columns(_ColN)
    '                        .Name = R!NameType.ToString
    '                        .FieldName = R!NameType.ToString
    '                        .Caption = R!NameType.ToString
    '                        .Visible = True
    '                        With .OptionsColumn
    '                            .AllowEdit = False
    '                            .AllowMove = False
    '                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                            .ReadOnly = True
    '                        End With
    '                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
    '                        .SummaryItem.DisplayFormat = "{0:n2} %"
    '                        _ColN += 1
    '                    End With
    '                Next

    '                dtPurpose = GetDataPurpose()
    '                For Each X As DataRow In dtPurpose.Rows
    '                    .Columns.Add()
    '                    With .Columns(_ColN)
    '                        .Name = X!NameType.ToString
    '                        .FieldName = X!NameType.ToString
    '                        .Caption = X!NameType.ToString
    '                        .Visible = True
    '                        With .OptionsColumn
    '                            .AllowEdit = False
    '                            .AllowMove = False
    '                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                            .ReadOnly = True
    '                        End With
    '                        .SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
    '                        .SummaryItem.DisplayFormat = "{0:n2} %"
    '                        _ColN += 1
    '                    End With
    '                Next


    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "cFTTrainer"
    '                    .FieldName = "cFTTrainer"
    '                    .Caption = "cFTTrainer"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    .Width = 200
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FDDateBegin"
    '                    .FieldName = "FDDateBegin"
    '                    .Caption = "FDDateBegin"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FDDateEnd"
    '                    .FieldName = "FDDateEnd"
    '                    .Caption = "FDDateEnd"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FTStartTime"
    '                    .FieldName = "FTStartTime"
    '                    .Caption = "FTStartTime"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FTEndTime"
    '                    .FieldName = "FTEndTime"
    '                    .Caption = "FTEndTime"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "cFTTotalHour"
    '                    .FieldName = "cFTTotalHour"
    '                    .Caption = "cFTTotalHour"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FTLocation"
    '                    .FieldName = "FTLocation"
    '                    .Caption = "FTLocation"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    .Width = 250
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "FTEvaluate"
    '                    .FieldName = "FTEvaluate"
    '                    .Caption = "FTEvaluate"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    _ColN += 1
    '                End With

    '                .Columns.Add()
    '                With .Columns(_ColN)
    '                    .Name = "cFTTrainNote"
    '                    .FieldName = "cFTTrainNote"
    '                    .Caption = "cFTTrainNote"
    '                    .Visible = True
    '                    With .OptionsColumn
    '                        .AllowEdit = False
    '                        .AllowMove = False
    '                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                        .ReadOnly = True
    '                    End With
    '                    .Width = 220
    '                    _ColN += 1
    '                End With


    '            End With
    '        Catch ex As Exception
    '            MsgBox(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub CreateGridBand()
    '        Dim _StateCreGridHeadFTT As Boolean = False
    '        Dim _StateCreGridHeadFix As Boolean = False
    '        Dim _StateChilFTT As Boolean = False
    '        Dim _StateChilFix As Boolean = False
    '        Try
    '            'With ogb
    '            For i As Integer = ogb.Bands.Count - 1 To 0 Step -1
    '                ogb.Bands.RemoveAt(i)
    '            Next
    '            For Each _item As GridColumn In ogb.Columns
    '                Dim ogbHead As New GridBand
    '                With ogbHead
    '                    Select Case Microsoft.VisualBasic.Left(_item.FieldName.ToString, 3)
    '                        Case "FTT"
    '                            If Not (_StateCreGridHeadFTT) Then
    '                                .AppearanceHeader.Options.UseTextOptions = True
    '                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                                .Caption = "gbHeadFTTType"
    '                                .Name = "gbHeadFTTType"
    '                                .RowCount = 1
    '                                .Visible = True
    '                                ogb.Bands.Add(ogbHead)
    '                                _StateCreGridHeadFTT = True
    '                            End If
    '                            If Not (_StateChilFTT) Then
    '                                For Each K As DataRow In dtTrainType.Rows
    '                                    Dim ogbChilFTT As New GridBand
    '                                    With ogbChilFTT
    '                                        .AppearanceHeader.Options.UseTextOptions = True
    '                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                                        .Caption = K!NameType.ToString
    '                                        .Columns.Add(ogb.Columns.ColumnByFieldName(K!NameType.ToString))
    '                                        .Name = "gbChil" & K!NameType.ToString
    '                                        .RowCount = 1
    '                                        .Visible = True
    '                                    End With
    '                                    ogbHead.Children.Add(ogbChilFTT)
    '                                Next
    '                                _StateChilFTT = True
    '                            End If

    '                        Case "Fix"

    '                            If Not (_StateCreGridHeadFix) Then
    '                                .AppearanceHeader.Options.UseTextOptions = True
    '                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                                .Caption = "gbHeadFix"
    '                                .Name = "gbHeadFix"
    '                                .RowCount = 1
    '                                .Visible = True
    '                                ogb.Bands.Add(ogbHead)
    '                                _StateCreGridHeadFix = True
    '                            End If

    '                            If Not (_StateChilFix) Then
    '                                For Each O As DataRow In dtPurpose.Rows
    '                                    Dim ogbChilFix As New GridBand
    '                                    With ogbChilFix
    '                                        .AppearanceHeader.Options.UseTextOptions = True
    '                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                                        .Caption = O!NameType.ToString
    '                                        .Columns.Add(ogb.Columns.ColumnByFieldName(O!NameType.ToString))
    '                                        .Name = "gbChil" & O!NameType.ToString
    '                                        .RowCount = 1
    '                                        .Visible = True
    '                                    End With
    '                                    ogbHead.Children.Add(ogbChilFix)
    '                                Next
    '                                _StateChilFix = True
    '                            End If


    '                        Case Else
    '                            .AppearanceHeader.Options.UseTextOptions = True
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .Caption = _item.FieldName.ToString
    '                            .Columns.Add(ogb.Columns.ColumnByFieldName(_item.FieldName.ToString))
    '                            .Name = "gbHead" & _item.FieldName.ToString
    '                            .RowCount = 2
    '                            'If _item.FieldName = "FNSeqNo" Then
    '                            '    .Width = 75
    '                            'ElseIf _item.FieldName = "FTDocNo" Then
    '                            '    .Width = 150
    '                            'ElseIf _item.FieldName = "cFTTrainCode" Then
    '                            '    .Width = 90
    '                            'ElseIf _item.FieldName = "cFTTrainer" Then
    '                            '    .Width = 200
    '                            'ElseIf _item.FieldName = "FTLocation" Then
    '                            '    .Width = 250
    '                            'ElseIf _item.FieldName = "cFTTrainNote" Then
    '                            '    .Width = 220
    '                            'End If
    '                            .Visible = True
    '                            ogb.Bands.Add(ogbHead)
    '                    End Select
    '                End With

    '            Next


    '        Catch ex As Exception

    '        End Try

    '    End Sub

    '#End Region


    Private Sub ShowFamily(ByVal EmpCode As String)

        Dim _Qry As String
        Try
            _Qry = "SELECT FNSeqNo,FTFamilyName"

            _Qry &= vbCrLf & ",FTFamilySex AS FTFamilySexInd"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTNameTH AS FTFamilySex"
            Else
                _Qry &= vbCrLf & ",FTNameEN AS FTFamilySex"
            End If

            _Qry &= vbCrLf & " FROM THRMEmployeeFamily WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN  (SELECT CAST(FNListIndex AS Char(1)) FTCBIDX,FTNameTH,FTNameEN FROM V_Sex WITH(NOLOCK))  V_Sex"
            _Qry &= vbCrLf & " ON FTFamilySex=V_Sex.FTCBIDX"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & ""
            ogdFamily.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            _Qry = "SELECT FNSeqNo,FTChildName , isnull( FTStateNotDisTax ,'0') as FTStateNotDisTax  "
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDChildBirthDate)=1 THEN SUBSTRING(FDChildBirthDate,9,2)+'/'+SUBSTRING(FDChildBirthDate,6,2)+'/'+SUBSTRING(FDChildBirthDate,1,4) ELSE '' END AS FDChildBirthDate"
            _Qry &= vbCrLf & ",ISNULL(FTStudySta,N'0') AS FTStudySta"
            _Qry &= vbCrLf & ",ISNULL(FTChildSex,N'0') AS FTChildSex"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",V_Sex.FTNameTH AS FTChildSexName"
                _Qry &= vbCrLf & ",V_Study.FTNameTH AS FTStudyStaName"
            Else
                _Qry &= vbCrLf & ",V_Sex.FTNameEN AS FTChildSexName"
                _Qry &= vbCrLf & ",V_Study.FTNameEN AS FTStudyStaName"
            End If

            _Qry &= vbCrLf & " FROM THRMEmployeeChild WITH(NOLOCK)"
            _Qry &= vbCrLf & " LEFT JOIN  (SELECT CAST(FNListIndex AS Char(1)) FTCBIDX,FTNameTH,FTNameEN FROM V_Sex WITH(NOLOCK))  V_Sex"
            _Qry &= vbCrLf & " ON FTChildSex=V_Sex.FTCBIDX"
            _Qry &= vbCrLf & " LEFT JOIN  (SELECT CAST(FNListIndex AS Char(1)) FTCBIDX,FTNameTH,FTNameEN FROM V_Study WITH(NOLOCK))  V_Study"
            _Qry &= vbCrLf & " ON FTStudySta=V_Study.FTCBIDX"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & ""

            ogdChild.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub SetShowFinance(ByVal EmpCode As String)

        Dim oDbdt As New DataTable
        Dim _Qry As String

        Try

            _Qry = "SELECT FTSumSalary"
            _Qry &= vbCrLf & ",FTSumSocial AS FTSumSocial"
            _Qry &= vbCrLf & ",FTSumTax AS FTSumTax"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT"
            _Qry &= vbCrLf & " ISNULL(FTSumSalary,0) AS FTSumSalary"
            _Qry &= vbCrLf & ",ISNULL(FTSumSocial,0) AS FTSumSocial"
            _Qry &= vbCrLf & ",ISNULL(FTSumTax,0) AS FTSumTax"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT SUM(CASE WHEN  isnumeric(FTSumSalary)=1 THEN  Cast(ISNULL(FTSumSalary,0) AS numeric(16,2))  ELSE 0 END) AS FTSumSalary"
            _Qry &= vbCrLf & ",SUM(CASE WHEN  isnumeric(FTSumSocial)=1 THEN  Cast(ISNULL(FTSumSocial,0) AS numeric(16,2))  ELSE 0 END) AS FTSumSocial"
            _Qry &= vbCrLf & ",SUM(CASE WHEN  isnumeric(FTSumTax)=1 THEN  Cast(ISNULL(FTSumTax,0) AS numeric(16,2))  ELSE 0 END) AS FTSumTax"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FNTotalIncome AS FTSumSalary"
            _Qry &= vbCrLf & ",FNSocial AS FTSumSocial"
            _Qry &= vbCrLf & ",FNTax AS FTSumTax"
            _Qry &= vbCrLf & " FROM THRTPayRoll WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " AND  FTPayYear=CONVERT(varchar(4),GETDATE(),111)"
            _Qry &= vbCrLf & " ) THRTPayRoll"
            _Qry &= vbCrLf & " ) THRTPayRoll1"
            _Qry &= vbCrLf & " ) THRTPayRoll2"

            Dim oDbdtSum As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            If oDbdtSum.Rows.Count > 0 Then

                olainput4.Value = oDbdtSum.Rows(0)("FTSumSalary")
                olainput9.Value = oDbdtSum.Rows(0)("FTSumSocial")
                olainput10.Value = oDbdtSum.Rows(0)("FTSumTax")

            End If

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt "
            _Qry &= vbCrLf & " FROM ("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,N'')=N'' THEN  0.00 ELSE   FTFinAmt  END AS FTFinAmt"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType "
            _Qry &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE   ISNULL(FTType,N'')=N'2'  "
            _Qry &= vbCrLf & " AND ISNULL(FTStaActive,N'')=N'1' AND (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR FNHSysCmpId=0) "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType=N'1' AND FTStaActive='1'"
            _Qry &= vbCrLf & " Left JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTFinAmt "
            _Qry &= vbCrLf & " FROM THRMEmployeeFin  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(EmpCode) & ""
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & " ) T  "


            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,N'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"
            ogdIncome.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt "
            _Qry &= vbCrLf & " FROM ("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,N'')=N'' THEN  0.00 ELSE   FTFinAmt END AS FTFinAmt"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType "
            _Qry &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE   ISNULL(FTType,N'')=N'2'  "
            _Qry &= vbCrLf & " AND   ISNULL(FTStaActive,N'')=N'1' AND (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR FNHSysCmpId=0) "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType=N'2' AND FTStaActive='1'"
            _Qry &= vbCrLf & " Left JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTFinAmt"
            _Qry &= vbCrLf & " FROM THRMEmployeeFin WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(EmpCode) & ""
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & " ) T "

            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,N'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"

            ogdDeduct.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",FTFinAmt AS FTFinAmt"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType"
            _Qry &= vbCrLf & " FROM THRMFinanceSet  WITH(NOLOCK)"

            _Qry &= vbCrLf & " WHERE  ISNULL(FTStaActive,N'')=N'1' AND (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR FNHSysCmpId=0) "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType=N'1'"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTFinCode,SUM(CASE WHEN  isnumeric(FTFinAmt)=1 THEN  Cast(ISNULL(FTFinAmt,0) AS numeric(16,2))  ELSE 0 END) AS FTFinAmt"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select FTFinCode"
            _Qry &= vbCrLf & ",FCTotalFinAmt AS FTFinAmt"
            _Qry &= vbCrLf & " FROM THRTPayRollFin WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " ) THRMEmployeeFinTmp"
            _Qry &= vbCrLf & " GROUP BY FTFinCode"
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & ") T "

            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,N'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"
            ogdIncomeSum.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt "
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",FTFinAmt AS FTFinAmt"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType"
            _Qry &= vbCrLf & " FROM THRMFinanceSet  WITH(NOLOCK)"

            _Qry &= vbCrLf & " WHERE ISNULL(FTStaActive,N'')=N'1' AND (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR FNHSysCmpId=0) "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType=N'2'"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTFinCode,SUM(CASE WHEN  isnumeric(FTFinAmt)=1 THEN  Cast(ISNULL(FTFinAmt,0) AS numeric(16,2))  ELSE 0 END) AS FTFinAmt"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select FTFinCode"
            _Qry &= vbCrLf & ",FCTotalFinAmt AS FTFinAmt"
            _Qry &= vbCrLf & " FROM THRTPayRollFin WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID =" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " ) THRMEmployeeFinTmp"
            _Qry &= vbCrLf & " GROUP BY FTFinCode"
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & ") T "

            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,N'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"
            ogdDeductSum.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub


    Private Sub ShowExperience(ByVal EmpCode As String)

        Dim _Qry As String

        Try

            _Qry = "SELECT FNSeqNo,FTCmpName,FTBusinessType,FTContractName,FTPosition,FTResponsibility"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDStartDate)=1 THEN SUBSTRING(FDStartDate,9,2)+'/'+SUBSTRING(FDStartDate,6,2)+'/'+SUBSTRING(FDStartDate,1,4) ELSE '' END AS FDStartDate"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDEndDate)=1 THEN SUBSTRING(FDEndDate,9,2)+'/'+SUBSTRING(FDEndDate,6,2)+'/'+SUBSTRING(FDEndDate,1,4) ELSE '' END AS FDEndDate"
            _Qry &= vbCrLf & ",FTResignCause"
            _Qry &= vbCrLf & ",FTEndSalary"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",L.FTNameTH AS FTHealthRiskFactors,LL.FTNameTH AS FTProtectiveEquipment"
            Else
                _Qry &= vbCrLf & ",L.FTNameEN AS FTHealthRiskFactors,LL.FTNameEN AS FTProtectiveEquipment"
            End If

            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeExperience AS H WITH(NOLOCK)"
            _Qry &= vbCrLf & "LEFT OUTER JOIN(SELECT L.FTListName,L.FTNameTH,L.FTNameEN,L.FNListIndex"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  "
            _Qry &= vbCrLf & "where L.FTListName   ='FTHealthRiskFactors' "
            _Qry &= vbCrLf & ")AS L ON  H.FTHealthRiskFactors=L.FNListIndex"
            _Qry &= vbCrLf & "LEFT OUTER JOIN(select L.FTListName,L.FTNameTH,L.FTNameEN,L.FNListIndex"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L"
            _Qry &= vbCrLf & "where L.FTListName   ='FTProtectiveEquipment' "
            _Qry &= vbCrLf & ")AS LL ON  H.FTProtectiveEquipment=LL.FNListIndex"
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & ""

            ogdExperience.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try
    End Sub


#End Region

    Private Sub FTEmpIdNo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTEmpIdNo.Leave
        If FTEmpIdNo.Text <> "" Then
            If FTEmpIdNo_lbl.ForeColor = Color.Blue Then
                If FTEmpIdNo.Text.Length = 13 Then
                    If IsNumeric(FTEmpIdNo.Text) Then
                        If Not (_ProcLoad) Then

                            If FTTaxNo.Text = "" Then
                                FTTaxNo.Text = FTEmpIdNo.Text
                            End If

                            If FTSocialNo.Text = "" Then
                                FTSocialNo.Text = FTEmpIdNo.Text
                            End If

                        End If
                    Else
                        FTEmpIdNo.Text = ""
                    End If
                Else
                    FTEmpIdNo.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub FDDateStart_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FDDateStart.EditValueChanged
        Try
            If FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
                If HI.UL.ULDate.CheckDate(FDDateStart.Text) <> "" Then
                    Dim _Qry As String
                    Try

                        If Not (_ProcLoad) Then

                            _Qry = " Select  Convert(varchar(10),DateAdd(Day,ISNULL(("
                            _Qry &= vbCrLf & "  SELECT TOP 1 FNProDay FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.THRMEmpType WHERE FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                            _Qry &= vbCrLf & " ),0),Convert(DateTime,N'" & HI.UL.ULDate.ConvertEnDB(FDDateStart.Text) & "') ),111) "

                            FDDateProbation.Text = HI.UL.ULDate.ConvertEN(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR))

                        End If

                        Dim _FNYear As Integer = 0
                        Dim _FNMonth As Integer = 0
                        Dim _FNDay As Integer = 0

                        If HI.UL.ULDate.CheckDate(FDDateEnd.Text) <> "" Then
                            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.SP_Datediff '" & HI.UL.ULDate.ConvertEnDB(FDDateStart.Text) & "',N'" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'"
                        Else
                            _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.SP_Datediff '" & HI.UL.ULDate.ConvertEnDB(FDDateStart.Text) & "',N''"
                        End If

                        Dim _Row As DataRow = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows(0)
                        _FNYear = Integer.Parse(Val(_Row("FNYear")))
                        _FNMonth = Integer.Parse(Val(_Row("FNMonth")))
                        _FNDay = Integer.Parse(Val(_Row("FNDay")))

                        FTWorkYear.Text = IIf(_FNYear < 0, 0, _FNYear)
                        FTWorkMonth.Text = IIf(_FNMonth < 0, 0, _FNMonth)
                        FTWorkDay.Text = IIf(_FNDay < 0, 0, _FNDay)

                    Catch ex As Exception
                    End Try

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FDDateEnd_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FDDateEnd.EditValueChanged
        Try
            If FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
                If HI.UL.ULDate.CheckDate(FDDateStart.Text) <> "" Then
                    Dim _Qry As String


                    Dim _FNYear As Integer = 0
                    Dim _FNMonth As Integer = 0
                    Dim _FNDay As Integer = 0

                    If HI.UL.ULDate.CheckDate(FDDateEnd.Text) <> "" Then

                        _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.SP_Datediff '" & HI.UL.ULDate.ConvertEnDB(FDDateStart.Text) & "',N'" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'"
                    Else

                        _Qry = "Exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.SP_Datediff '" & HI.UL.ULDate.ConvertEnDB(FDDateStart.Text) & "',N''"
                    End If


                    Dim _Row As DataRow = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows(0)
                    'nYear = oRow("FNYear")
                    'nMonth = (nYear * 12) + oRow("FNMonth")


                    'FTWorkYear.Text = IIf(nYear < 0, 0, nYear)
                    'FTWorkMonth.Text = IIf(nMonth < 0, 0, nMonth)


                    _FNYear = Integer.Parse(Val(_Row("FNYear")))
                    _FNMonth = Integer.Parse(Val(_Row("FNMonth")))
                    _FNDay = Integer.Parse(Val(_Row("FNDay")))

                    FTWorkYear.Text = IIf(_FNYear < 0, 0, _FNYear)
                    FTWorkMonth.Text = IIf(_FNMonth < 0, 0, _FNMonth)
                    FTWorkDay.Text = IIf(_FNDay < 0, 0, _FNDay)

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FNHSysPositId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysPositId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysPositId_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysPositId.Text <> "" Then

                    Dim _Qry As String = "SELECT TOP 1 FNHSysPositId  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMPosition WITH(NOLOCK) WHERE FTPositCode =N'" & HI.UL.ULF.rpQuoted(FNHSysPositId.Text) & "' AND FNHSysCmpId =  " & HI.ST.SysInfo.CmpID
                    FNHSysPositId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                    _Qry = "SELECT Top 1  isnull(G.FNJobLevel , -1) as FNJobLevel ,isnull( G.FNJobRole, -1) as FNJobRole  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMPosition AS P WITH ( NOLOCK )   "
                    _Qry &= vbCrLf & " LEFT OUTER JOIN [HITECH_MASTER].dbo.TCNMPositionGrp AS G WITH(NOLOCK) ON P.FNHSysPositGrpId = G.FNHSysPositGrpId "
                    _Qry &= vbCrLf & " where P.FNHSysPositId=" & Val(Me.FNHSysPositId.Properties.Tag)

                    Try
                        Me.FNJobRole.SelectedIndex = -1
                        Me.FNJobLevel.SelectedIndex = -1
                        For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Rows
                            Me.FNJobLevel.SelectedIndex = HI.TL.CboList.GetListValue("FNJobLevel", Val(R!FNJobLevel.ToString))
                            Me.FNJobRole.SelectedIndex = HI.TL.CboList.GetListValue("FNJobRole", Val(R!FNJobRole.ToString))
                        Next
                    Catch ex As Exception

                    End Try
                    ' Call Load_PDF1()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged
        If Not (_ProcLink) Then
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
            Else


                If FNHSysEmpID.Text <> "" Then
                    Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode =N'" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "
                    FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

                    Call LoadDataInfo(FNHSysEmpID.Properties.Tag.ToString)
                    Call LoadPass()
                    Call LoadVisa()
                    Call LoadWorkpermit()
                    Call LoadMOU()
                    Call LoadOther()
                    Call LoadPP()
                    Call ShowPunishment(FNHSysEmpID.Properties.Tag.ToString)
                    ' Call Load_PDF1()


                End If
            End If
        End If


    End Sub



    Private Sub ocmcleareduc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearEmpeduc.Click
        HI.TL.HandlerControl.ClearControl(ogbeduc)
    End Sub

    Private Sub ocmclearresign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearEmpresign.Click
        HI.TL.HandlerControl.ClearControl(ogbresign)
    End Sub

    Private Sub ocmclearpuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearEmppuns.Click
        HI.TL.HandlerControl.ClearControl(ogbpunsh)
    End Sub

    Private Sub ocmclearfam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearEmpfam.Click
        HI.TL.HandlerControl.ClearControl(ogbfam)
    End Sub

    Private Sub ocmclearex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearEmpexp.Click
        HI.TL.HandlerControl.ClearControl(ogbex)
    End Sub

    Private Sub ocmclearchild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearEmpchild.Click
        HI.TL.HandlerControl.ClearControl(ogbchild)
    End Sub



    Private Function VerrifyDataResign() As Boolean
        Dim _Pass As Boolean = False
        If FDDateBegin.Text <> "" Then
            If FDDateResign.Text <> "" Then
                If FNHSysResignId2.Text <> "" Then
                    If FNHSysResignId2.Properties.Tag.ToString <> "" Then

                        _Pass = True

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysResignId2_lbl.Text)
                        FNHSysResignId2.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysResignId2_lbl.Text)
                    FNHSysResignId2.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateResign_lbl.Text)
                FDDateResign.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateBegin_lbl.Text)
            FDDateBegin.Focus()
        End If

        Return _Pass
    End Function

    Private Function VerrifyDataEduc() As Boolean
        Dim _Pass As Boolean = False

        If FNHSysCourseId.Text <> "" Then
            If FNHSysCourseId.Properties.Tag.ToString <> "" Then
                If FTYearBegin.Text <> "" Then
                    If FTYearEnd.Text <> "" Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTYearEnd_lbl.Text)
                        FTYearEnd.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTYearBegin_lbl.Text)
                    FTYearBegin.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCourseId_lbl.Text)
                FNHSysCourseId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysCourseId_lbl.Text)
            FNHSysCourseId.Focus()
        End If

        Return _Pass
    End Function

    Private Function VerrifyDataPunishment() As Boolean
        Dim _Pass As Boolean = False

        If FDDatePunishment.Text <> "" Then
            If FDDateEndPunishment.Text <> "" Then
                If FNHSysPunishmentLvId.Text <> "" Then
                    If FNHSysPunishmentLvId.Properties.Tag.ToString <> "" Then
                        If FTPunishmentBy.Text <> "" Then

                            If FTSuspended.EditValue.ToString <> "0" Then
                                If ChkOverLapDateTime() Then
                                    _Pass = True
                                Else
                                    HI.MG.ShowMsg.mInvalidData("พบการวันพักงานซ้ำ  กรุณาทำการระบุข้อมูลวันให้ถูกต้อง !!!", 202112140007, Me.Text)
                                    FDDateEndPunishment.Focus()
                                End If
                            Else
                                _Pass = True
                            End If

                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPunishmentBy_lbl.Text)
                            FTPunishmentBy.Focus()
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysPunishmentLvId_lbl.Text)
                        FNHSysPunishmentLvId.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysPunishmentLvId_lbl.Text)
                    FNHSysPunishmentLvId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateEndPunishment_lbl.Text)
                FDDateEndPunishment.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDatePunishment_lbl.Text)
            FDDatePunishment.Focus()
        End If



        Return _Pass
    End Function


    Private Function VerrifyDataExperience() As Boolean
        Dim _Pass As Boolean = False

        If FTCmpName.Text <> "" Then

            If FTPosition.Text <> "" Then
                If FTResponsibility.Text <> "" Then
                    If FDStartDate.Text <> "" Then
                        If FDEndDate.Text <> "" Then
                            _Pass = True
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
                            FDEndDate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDStartDate_lbl.Text)
                        FDStartDate.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTResponsibility_lbl.Text)
                    FTResponsibility.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPosition_lbl.Text)
                FTPosition.Focus()
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTCmpName_lbl.Text)
            FTCmpName.Focus()
        End If



        Return _Pass
    End Function

    Private Function VerrifyDataFamily() As Boolean
        Dim _Pass As Boolean = False
        If FNFamilySeqNo.Value > 0 Then
            If FTFamilyName.Text <> "" Then
                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTFamilyName_lbl.Text)
                FTFamilyName.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNFamilySeqNo_lbl.Text)
            FNFamilySeqNo.Focus()
        End If

        Return _Pass
    End Function

    Private Function VerrifyDataChild() As Boolean
        Dim _Pass As Boolean = False
        If FNChildSeqNo.Value > 0 Then
            If FTChildName.Text <> "" Then
                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTFamilyName_lbl.Text)
                FTFamilyName.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNFamilySeqNo_lbl.Text)
            FNFamilySeqNo.Focus()
        End If
        Return _Pass
    End Function

#Region " Education "
    Private Sub ocmaddeduc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpeduc.Click
        If VerrifyDataEduc() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then


                Dim _Qry As String = ""

                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeEducation SET "
                _Qry &= vbCrLf & "FTCerCode=N'' "
                _Qry &= vbCrLf & ", FNHSysCourseId=" & Val(FNHSysCourseId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FNHSysUniversityId=" & Val(FNHSysUniversityId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FNHSysUniversityBranchId=" & Val(FNHSysUniversityBranchId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FNHSysFacultyId=" & Val(FNHSysFacultyId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FNHSysFacultyBranchId=" & Val(FNHSysFacultyBranchId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FNHSysFacultyMajorId=" & Val(FNHSysFacultyMajorId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FTYearBegin=N'" & HI.UL.ULF.rpQuoted(FTYearBegin.Text) & "'"
                _Qry &= vbCrLf & ", FTYearEnd=N'" & HI.UL.ULF.rpQuoted(FTYearEnd.Text) & "'"
                _Qry &= vbCrLf & ", FCGrade=" & FCGrade.Value & " "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNCourseSeqNo.Value

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeEducation WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                    Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeEducation(FNHSysEmpID, FNSeqNo, FTCerCode, FNHSysCourseId,FNHSysUniversityId,FNHSysUniversityBranchId,FNHSysFacultyId,FNHSysFacultyBranchId,FNHSysFacultyMajorId, FTYearBegin, FTYearEnd, FCGrade "
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N''," & Val(FNHSysCourseId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysUniversityId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysUniversityBranchId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysFacultyId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysFacultyBranchId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & "," & Val(FNHSysFacultyMajorId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTYearBegin.Text) & "'  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTYearEnd.Text) & "'  "
                    _Qry &= vbCrLf & "," & FCGrade.Value & " "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If


                _Qry = "UPDATE THRTEmployeeEducation SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeeEducation INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRTEmployeeEducation WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeEducation.FNSeqNo=T1.FNSeqNo AND THRTEmployeeEducation.FNHSysEmpID=T1.FNHSysEmpID"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                FNCourseSeqNo.Value = 0
                HI.TL.HandlerControl.ClearControl(ogbeduc)
                Call ShowEducation(FNHSysEmpID.Properties.Tag.ToString)

            End If
        End If
    End Sub

    Private Sub ocmremoveeduc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpeduc.Click

        If FNCourseSeqNo.Value <= 0 Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeEducation  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNCourseSeqNo.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRTEmployeeEducation SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM THRTEmployeeEducation INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRTEmployeeEducation WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRTEmployeeEducation.FNSeqNo=T1.FNSeqNo AND THRTEmployeeEducation.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbeduc)
        Call ShowEducation(FNHSysEmpID.Properties.Tag.ToString)

    End Sub

    Private Sub ogveduc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogveduc.Click
        With ogveduc

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            FNCourseSeqNo.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            FNHSysCourseId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTCourCode").ToString
            FNHSysUniversityId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTUniversityCode").ToString
            FNHSysUniversityBranchId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTUniversityBranchCode").ToString
            FNHSysFacultyId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTFacultyCode").ToString
            FNHSysFacultyBranchId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTFacultyBranchCode").ToString
            FNHSysFacultyMajorId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTFacultyMajorCode").ToString
            FTYearBegin.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTYearBegin").ToString
            FTYearEnd.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTYearEnd").ToString
            FCGrade.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FCGrade").ToString)

            FNHSysCourseId.Focus()

        End With
    End Sub
#End Region

#Region " Resign "
    Private Sub ocmaddresign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpresign.Click
        If VerrifyDataResign() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then

                Dim _Qry As String = ""

                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeResign SET "
                _Qry &= vbCrLf & " FDDateBegin=N'" & HI.UL.ULDate.ConvertEnDB(FDDateBegin.Text) & "'"
                _Qry &= vbCrLf & ", FNHSysResignId=" & Val(FNHSysResignId2.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & ", FTResignNote=N'" & HI.UL.ULF.rpQuoted(FTResignNote.Text) & "' "
                _Qry &= vbCrLf & ", FTRetEmpCard=N'" & FTRetEmpCard.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FTDestroyCard=N'" & FTDestroyCard.EditValue.ToString & "'"
                _Qry &= vbCrLf & ", FTRetEquipment=N'" & FTRetEquipment.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FTBackListSta=N'" & FTBackListSta.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FDDelEMailDate=N'" & HI.UL.ULDate.ConvertEnDB(FDDelEMailDate.Text) & "' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FDDateResign=N'" & HI.UL.ULDate.ConvertEnDB(FDDateResign.Text) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then


                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeResign(FNHSysEmpID,FDDateResign, FDDateBegin, FNHSysResignId, FTResignNote, FTRetEmpCard, FTDestroyCard, FTRetEquipment, FTBackListSta, FDDelEMailDate "
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateResign.Text) & "',N'" & HI.UL.ULDate.ConvertEnDB(FDDateBegin.Text) & "' "
                    _Qry &= vbCrLf & "," & Val(FNHSysResignId2.Properties.Tag.ToString) & ",N'" & HI.UL.ULDate.ConvertEnDB(FTResignNote.Text) & "'"
                    _Qry &= vbCrLf & ",N'" & FTRetEmpCard.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & FTDestroyCard.EditValue.ToString & "'  "
                    _Qry &= vbCrLf & ",N'" & FTRetEquipment.EditValue.ToString & "'  "
                    _Qry &= vbCrLf & ",N'" & FTBackListSta.EditValue.ToString & "'  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDelEMailDate.Text) & "'  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If

                HI.TL.HandlerControl.ClearControl(ogbresign)
                Call ShowResign(FNHSysEmpID.Properties.Tag.ToString)

            End If
        End If
    End Sub

    Private Sub ocmremoveresign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpresign.Click
        If FDDateResign.Text = "" Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeResign  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FDDateResign=N'" & HI.UL.ULDate.ConvertEnDB(FDDateResign.Text) & "'"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbresign)
        Call ShowResign(FNHSysEmpID.Properties.Tag.ToString)
    End Sub

    Private Sub ogvresign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvResign.Click
        With ogvResign

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Try
                FDDateResign.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateResign").ToString)
            Catch ex As Exception
                FDDateResign.DateTime = Nothing
            End Try

            Try
                FDDateBegin.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateBegin").ToString)
            Catch ex As Exception
                FDDateBegin.DateTime = Nothing
            End Try

            HI.TL.HandlerControl.DynamicButtonediHSysKey_Leave(FNHSysResignId2, Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysResignId").ToString))

            FTRetEmpCard.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTRetEmpCard").ToString
            FTDestroyCard.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTDestroyCard").ToString
            FTRetEquipment.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTRetEquipment").ToString
            FTBackListSta.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTBackListSta").ToString

            FDDelEMailDate.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FDDelEMailDate").ToString

            FDDateBegin.Focus()

        End With
    End Sub

#End Region

#Region " Punishment "

    Private Sub ocmaddpuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmppuns.Click
        If VerrifyDataPunishment() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then

                Dim _state_data_to_leave As String = ""

                Dim _Qry As String = ""

                _Qry = "UPDATE THRTEmployeePunishment SET "
                _Qry &= vbCrLf & "FTCmpCodeRef=N'' "
                _Qry &= vbCrLf & ", FDDatePunishment=N'" & HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text) & "' "
                _Qry &= vbCrLf & ", FDDateEndPunishment=N'" & HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text) & "' "

                _Qry &= vbCrLf & ", FNHSysPunishmentLvId=" & Val(FNHSysPunishmentLvId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ", FTCause=N'" & HI.UL.ULF.rpQuoted(FTCause.Text) & "' "
                _Qry &= vbCrLf & ", FTPunishmentBy=N'" & HI.UL.ULF.rpQuoted(FTPunishmentBy.Text) & "' "
                _Qry &= vbCrLf & ", FTStaOutstanding=N'" & FTStaOutstanding.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",FTSuspended = N'" & FTSuspended.EditValue.ToString & "' "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNPunishmentSeqNo.Value

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM THRTEmployeePunishment WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                    Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO THRTEmployeePunishment(FNHSysEmpID,FNSeqNo, FTCmpCodeRef, FDDatePunishment, FDDateEndPunishment, FNHSysPunishmentLvId, FTCause, FTPunishmentBy, FTStaOutstanding , FTSuspended "
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N'',N'" & HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text) & "',N'" & HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text) & "'," & Val(FNHSysPunishmentLvId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTCause.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTPunishmentBy.Text) & "'  "
                    _Qry &= vbCrLf & ",N'" & FTStaOutstanding.EditValue.ToString & "'  "
                    _Qry &= vbCrLf & ",N'" & FTSuspended.EditValue.ToString & "'  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    ''Update leave Advance    - tran Leave /// tran
                    _state_data_to_leave = "Y"
                Else
                    ''Update leave Advance    - tran Leave /// tran
                    _state_data_to_leave = "Y"
                End If

                If FTSuspended.EditValue.ToString = "1" Then  ''ถูกพักงาน


                    Call UpdateToTime(HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text), HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text), HI.UL.ULF.rpQuoted(FTCause.Text), HI.UL.ULF.rpQuoted(FTPunishmentBy.Text))
                End If


                _Qry = "UPDATE THRTEmployeePunishment SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeePunishment INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRTEmployeePunishment WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRTEmployeePunishment.FNSeqNo=T1.FNSeqNo AND THRTEmployeePunishment.FNHSysEmpID=T1.FNHSysEmpID"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                HI.TL.HandlerControl.ClearControl(ogbpunsh)
                Call ShowPunishment(FNHSysEmpID.Properties.Tag.ToString)

            End If
        End If
    End Sub

    Private Sub ocmremovepuns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmppuns.Click

        If FNPunishmentSeqNo.Value <= 0 Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  THRTEmployeePunishment  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNPunishmentSeqNo.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        If FTSuspended.Checked Then
            Call UpdateToTimeDelete(HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text), HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text), HI.UL.ULF.rpQuoted(FTCause.Text), HI.UL.ULF.rpQuoted(FTPunishmentBy.Text))

        End If

        _Qry = "UPDATE THRTEmployeePunishment SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM THRTEmployeePunishment INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRTEmployeePunishment WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRTEmployeePunishment.FNSeqNo=T1.FNSeqNo AND THRTEmployeePunishment.FNHSysEmpID=T1.FNHSysEmpID"


        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)



        HI.TL.HandlerControl.ClearControl(ogbpunsh)
        Call ShowPunishment(FNHSysEmpID.Properties.Tag.ToString)
    End Sub

    Private Sub Punishment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvPunishment.Click
        With ogvPunishment

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNPunishmentSeqNo.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)

            FNHSysPunishmentLvId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTLevelCode").ToString

            Try
                FDDatePunishment.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDatePunishment").ToString)
                FDDateEndPunishment.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateEndPunishment").ToString)
            Catch ex As Exception
                FDDatePunishment.DateTime = Nothing
                FDDateEndPunishment.DateTime = Nothing
            End Try

            FTCause.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTCause").ToString
            FTPunishmentBy.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPunishmentBy").ToString

            Try
                FTStaOutstanding.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTStaOutstanding").ToString
            Catch ex As Exception
            End Try

            Try
                FTSuspended.EditValue = "" & .GetRowCellValue(.FocusedRowHandle, "FTSuspended").ToString
            Catch ex As Exception
            End Try


            Dim _Qry As String = ""
            Dim _dt As DataTable

            If FTSuspended.EditValue >= "" Then
                _Qry = " SELECT FTInsUser, FTInsDate, FTInsTime, FTUpdUser FNHSysEmpID, FTStartDate, FTEndDate "
                _Qry &= vbCrLf & " , FTHoliday, FNLeaveTotalDay, FTLeaveType, FTLeavePay, FTLeaveStartTime, FTLeaveEndTime, FNLeaveTotalTime "
                _Qry &= vbCrLf & " ,  FTLeaveNote, FTApproveState, FTApproveDate, FTApproveTime, FTApproveBy, FTPayYear, FTPayTerm "
                _Qry &= vbCrLf & " , FTStaCalSSO, FTStaLeaveDay, FNLeaveTotalTimeMin "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily "

                _Qry &= vbCrLf & " WHERE (FTStartDate >='" & HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text) & "' AND FTEndDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text) & "'  "
                _Qry &= vbCrLf & "  OR FTStartDate >='" & HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text) & "' AND FTEndDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text) & "' ) "
                _Qry &= vbCrLf & " AND  FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""


                _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                Me.FTSTime.Text = ""
                Me.FTETime.Text = ""

                Me._FTInsUser.Text = ""
                Me._FTInsDate.Text = ""
                Me._FTInsTime.Text = ""


                For Each R As DataRow In _dt.Rows
                    Me.FTSTime.Text = R!FTLeaveStartTime.ToString
                    Me.FTETime.Text = R!FTLeaveEndTime.ToString

                    Me._FTInsUser.Text = R!FTInsUser.ToString
                    Me._FTInsDate.Text = R!FTInsDate.ToString
                    Me._FTInsTime.Text = R!FTInsTime.ToString

                Next


            End If


            FTCause.Focus()

        End With
    End Sub
    Private Function ChkOverLapDateTime() As Boolean
        Try
            Dim _dt_from As DateTime
            Dim _dt_to As DateTime

            Dim _Qry As String = ""
            Dim _dt As DataTable

            LoadDataEmployeeShift()

            _dt_from = FDDatePunishment.Text + " " + FTSTime.Text
            _dt_to = FDDateEndPunishment.Text + " " + FTETime.Text

            '' FTStartDate.Text, FTEndDate.Text

            ''check user create  create data time fnhsysemp id

            _Qry = " SELECT FTInsUser, FTInsDate, FTInsTime, FTUpdUser FNHSysEmpID, FTStartDate, FTEndDate "
            _Qry &= vbCrLf & " , FTHoliday, FNLeaveTotalDay, FTLeaveType, FTLeavePay, FTLeaveStartTime, FTLeaveEndTime, FNLeaveTotalTime "
            _Qry &= vbCrLf & " ,  FTLeaveNote, FTApproveState, FTApproveDate, FTApproveTime, FTApproveBy, FTPayYear, FTPayTerm "
            _Qry &= vbCrLf & " , FTStaCalSSO, FTStaLeaveDay, FNLeaveTotalTimeMin "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily "

            _Qry &= vbCrLf & " WHERE (FTStartDate >='" & HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text) & "' AND FTStartDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text) & "'  "
            _Qry &= vbCrLf & "  OR FTEndDate >='" & HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text) & "' AND FTEndDate <='" & HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text) & "' ) "
            _Qry &= vbCrLf & " AND  FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""

            If _FTInsUser.Text = "" And _FTInsDate.Text = "" And _FTInsTime.Text = "" Then


            Else
                _Qry &= vbCrLf & " AND  NOT ( FTInsUser='" & _FTInsUser.Text & "' "
                _Qry &= vbCrLf & "  AND FTInsDate = '" & _FTInsDate.Text & "' "
                _Qry &= vbCrLf & "  AND FTInsTime = '" & _FTInsTime.Text & "') "

            End If

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            '' check FNLeaveDay
            ''0	ลาเต็มวัน	Full Day
            '' 1 ลาครึ่งวันเช้า	Morning
            '' 2 ลาครึ่งวันบ่าย	Afternoon
            '' 3 ระบุเวลา	Time Specify

            If _dt.Rows.Count > 0 Then
                Return False
            End If

            Dim _timeIn1 As String = ""
            Dim _timeOut1 As String = ""
            Dim _timeIn2 As String = ""
            Dim _timeOut2 As String = ""



            Dim _timeLeaveFrom As String = ""
            Dim _timeLeaveTo As String = ""

            If FTIn1M.Text <> "" And FTOut1M.Text <> "" Then
                If FTIn1M.Text > FTOut1M.Text Then
                    ''กะกลางคืน
                    ''  _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.ActualNextDate & "  " & FTOut1M.Text))

                    If _dt.Rows.Count > 0 Then

                        'If FNLeaveDay.SelectedIndex = 0 Then
                        '    If _dt.Rows.Count > 0 Then
                        '        Return False
                        '    Else
                        Return True
                        'End If

                        'Else

                        '    '' check เวลา
                        '    If _dt.Rows.Count > 0 Then
                        '        For Each R As DataRow In _dt.Rows

                        '            _timeLeaveFrom = R!FTLeaveStartTime.ToString
                        '            _timeLeaveTo = R!FTLeaveEndTime.ToString

                        '        Next

                        '        If (FTSTime.Text > _timeLeaveFrom And FTSTime.Text >= _timeLeaveTo) Or (FTETime.Text <= _timeLeaveTo And FTETime.Text < _timeLeaveFrom) Then

                        '            Return True
                        '        Else
                        '            Return False
                        '        End If
                        '    Else
                        '        Return True
                        '    End If

                        'End If
                    Else
                        Return True

                    End If

                Else
                    ''กะกลางวัน
                    If _dt.Rows.Count > 0 Then

                        'If FNLeaveDay.SelectedIndex = 0 Then
                        '    If _dt.Rows.Count > 0 Then
                        '        Return False
                        '    Else
                        Return True
                        'End If

                        'Else

                        '    '' check เวลา
                        '    If _dt.Rows.Count > 0 Then
                        '        For Each R As DataRow In _dt.Rows

                        '            _timeLeaveFrom = R!FTLeaveStartTime.ToString
                        '            _timeLeaveTo = R!FTLeaveEndTime.ToString

                        '        Next

                        '        If (FTSTime.Text > _timeLeaveFrom And FTSTime.Text >= _timeLeaveTo) Or (FTETime.Text <= _timeLeaveTo And FTETime.Text < _timeLeaveFrom) Then
                        '            Return True
                        '        Else
                        '            Return False
                        '        End If
                        '    Else
                        '        Return True
                        '    End If

                        'End If
                    Else
                        Return True

                    End If



                End If
            End If


        Catch ex As Exception
            Return False
        End Try


    End Function


    Private Sub LoadDataEmployeeShift()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT   TOP 1     M.FNHSysEmpID, S.FTIn1, S.FTOut1, S.FTIn2, S.FTOut2, S.FCHour,M.FNHSysShiftID"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & "   WHERE M.FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.FTIn1.Text = ""
        Me.FTOut1.Text = ""
        Me.FTIn2.Text = ""
        Me.FTOut2.Text = ""
        Me.FTIn1M.Text = ""
        Me.FTOut1M.Text = ""
        Me.FTIn2M.Text = ""
        Me.FTOut2M.Text = ""
        '' Me.FNHSysShiftID.Text = ""

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows

                '' Me.FNHSysShiftID.Text = R!FNHSysShiftID.ToString



                Me.FTIn1.Text = R!FTIn1.ToString
                Me.FTOut1.Text = R!FTOut1.ToString
                Me.FTIn2.Text = R!FTIn2.ToString
                Me.FTOut2.Text = R!FTOut2.ToString

                Me.FTIn1M.Text = R!FTIn1.ToString
                Me.FTOut1M.Text = R!FTOut1.ToString
                Me.FTIn2M.Text = R!FTIn2.ToString
                Me.FTOut2M.Text = R!FTOut2.ToString

                Exit For
            Next

        End If

        Me.FTSTime.Text = Me.FTIn1M.Text
        Me.FTETime.Text = Me.FTOut2M.Text

        Call LoadDataEmployeeMoveShift()

    End Sub

    Private Sub LoadDataEmployeeMoveShift()
        Dim _Qry As String = ""
        Dim _dt As DataTable


        _Qry = "   SELECT        M.FNHSysEmpID, S.FTIn1, S.FTOut1, S.FTIn2, S.FTOut2, S.FCHour"
        _Qry &= vbCrLf & " FROM     THRMEmployeeMoveShift AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " WHERE  M.FNHSysEmpID =" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & " AND M.FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FDDatePunishment.Text) & "' "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.FTIn1M.Text = Me.FTIn1.Text
        Me.FTOut1M.Text = Me.FTOut1.Text
        Me.FTIn2M.Text = Me.FTIn2.Text
        Me.FTOut2M.Text = Me.FTOut2.Text

        For Each R As DataRow In _dt.Rows


            Me.FTIn1M.Text = R!FTIn1.ToString
            Me.FTOut1M.Text = R!FTOut1.ToString
            Me.FTIn2M.Text = R!FTIn2.ToString
            Me.FTOut2M.Text = R!FTOut2.ToString
            Exit For
        Next

        Me.FTSTime.Text = Me.FTIn1M.Text
        Me.FTETime.Text = Me.FTOut2M.Text

        '' Call FNLeaveDay_SelectedIndexChanged(FNLeaveDay, New System.EventArgs)

    End Sub

    Private Function UpdateToTime(ByVal FtDateStart As String, ByVal FtDateEnd As String, ByVal FTCause As String, ByVal FTPunishmentBy As String) As Boolean
        Try
            Dim _Qry As String = ""


            '' To leave Adavance

            Call SaveDataLeaveAdvance(HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text), HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text), HI.UL.ULF.rpQuoted(FTCause), HI.UL.ULF.rpQuoted(FTPunishmentBy), "")



            Call ApproveDataLeave(HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text), HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text), HI.UL.ULF.rpQuoted(FTCause), HI.UL.ULF.rpQuoted(FTPunishmentBy), "")

            '' to trans


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function UpdateToTimeDelete(ByVal FtDateStart As String, ByVal FtDateEnd As String, ByVal FTCause As String, ByVal FTPunishmentBy As String) As Boolean
        Try
            Dim _Qry As String = ""


            '' To leave Adavance

            Call DeleteDataLeaveAdvance(HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text), HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text), HI.UL.ULF.rpQuoted(FTCause), HI.UL.ULF.rpQuoted(FTPunishmentBy), "")



            ''  Call ApproveDataLeave(HI.UL.ULDate.ConvertEnDB(FDDatePunishment.Text), HI.UL.ULDate.ConvertEnDB(FDDateEndPunishment.Text), HI.UL.ULF.rpQuoted(FTCause), HI.UL.ULF.rpQuoted(FTPunishmentBy), "")

            '' to trans


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveDataLeaveAdvance(ByVal FtDateStart As String, ByVal FtDateEnd As String, ByVal FTCause As String, ByVal FTPunishmentBy As String, ByVal _HRProcess As String) As Boolean
        Try
            Dim _StateMngApp As Boolean = False
            Dim _Qry As String
            Dim _QryFile As String
            Dim _PicName As String = ""
            If _HRProcess = "" Then



                _Qry = "SELECT Top 1  Isnull(FTStateMngApp,'0')  as FTStateMngApp "
                _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType On THRMEmployee.FNHSysEmpTypeId = HITECH_MASTER.dbo.THRMEmpType.FNHSysEmpTypeId"
                _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""
                _StateMngApp = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = "1")



                _Qry = "Select TOP 1 FNHSysEmpID FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily With(NOLOCK)"
                _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & "And FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
                _Qry &= vbCrLf & "AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
                _Qry &= vbCrLf & " AND FTLeaveType = '99'"

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily (FTInsUser, FTInsDate, FTInsTime"
                    _Qry &= vbCrLf & " , FNHSysEmpID, FTStartDate, FTEndDate,FTHoliday, FNLeaveTotalDay"
                    _Qry &= vbCrLf & " , FTLeaveType,  FTLeavePay"
                    _Qry &= vbCrLf & " , FTLeaveStartTime, FTLeaveEndTime, FNLeaveTotalTime,FNLeaveTotalTimeMin, FTLeaveNote"
                    _Qry &= vbCrLf & ",FTStaCalSSO,FTStaLeaveDay,FTStateMedicalCertificate,FTMedicalCertificateName,FTStateDeductVacation ,FNSpecialLeaveType"


                    '***** state appoved originale
                    _Qry &= vbCrLf & " ,FTApproveState"
                    _Qry &= vbCrLf & " , FTApproveDate"
                    _Qry &= vbCrLf & " , FTApproveTime"
                    _Qry &= vbCrLf & " , FTApproveBy"

                    '***** send approved
                    _Qry &= vbCrLf & " , FTSendApproveState"
                    _Qry &= vbCrLf & " , FDSendApproveDate"
                    _Qry &= vbCrLf & " , FTSendApproveTime"
                    _Qry &= vbCrLf & " , FTSendApproveBy"

                    '***** mng approved
                    _Qry &= vbCrLf & " , FTMngApproveState"
                    _Qry &= vbCrLf & " , FDMngApproveDate"
                    _Qry &= vbCrLf & " , FTMngApproveTime"
                    _Qry &= vbCrLf & " , FTMngApproveBy"


                    _Qry &= vbCrLf & " )"

                    _Qry &= vbCrLf & " VALUES ('" & HI.ST.UserInfo.UserName & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,'" & Val(FNHSysEmpID.Properties.Tag.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
                    _Qry &= vbCrLf & " ,'0'" ''FTStateNotMergeHoliday
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd) + 1 & "" ''FTNetDay
                    _Qry &= vbCrLf & " ,'99'" ''FNLeaveType
                    _Qry &= vbCrLf & " ,'0'" ''FTStateLeavepay
                    _Qry &= vbCrLf & " ,'08:00'"
                    _Qry &= vbCrLf & " ,'17:00'"
                    _Qry &= vbCrLf & " ,480" ''& FNNetTime.Value
                    _Qry &= vbCrLf & " ," & (Val(HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd)) + 1) * 480
                    _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(FTCause & " BY " & FTPunishmentBy) & "'"

                    'If Not (ocmapprove.Visible) And _leavePro = False Then
                    '    _Qry &= vbCrLf & " ,'1'"
                    'Else
                    '    _Qry &= vbCrLf & " ,'0'"
                    'End If

                    _Qry &= vbCrLf & " ,'0'" ''FTStateCalSSo
                    _Qry &= vbCrLf & " ,'0'" ''FNLeaveDay
                    _Qry &= vbCrLf & " ,'0'"  ''FTStateMedicalCertificate
                    _Qry &= vbCrLf & " ,''"  ''FTMedicalCertificateName
                    _Qry &= vbCrLf & " ,'0'" ''FTStateDeductVacation
                    _Qry &= vbCrLf & " ,'0'" ''FNSpecialLeaveType


                    '***** state appoved originale
                    _Qry &= vbCrLf & ",'1' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"

                    '***** send approved
                    _Qry &= vbCrLf & ",'1'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"

                    '***** mng approved
                    _Qry &= vbCrLf & " ,'1'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,'" & HI.ST.UserInfo.UserName & "'"


                    _Qry &= vbCrLf & " )"




                Else

                    _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily SET"
                    _Qry &= vbCrLf & " FTUpdUser = '" & HI.ST.UserInfo.UserName & "'"
                    _Qry &= vbCrLf & " ,FTUpdDate = " & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ,FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,FTHoliday = '0'" '' & FTStateNotMergeHoliday.EditValue.ToString & "'"
                    _Qry &= vbCrLf & " ,FNLeaveTotalDay = " & HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd) + 1 & "" ''FTNetDay
                    _Qry &= vbCrLf & " ,FTLeaveType = '99'"
                    _Qry &= vbCrLf & " ,FTLeavePay = '0'"
                    _Qry &= vbCrLf & " ,FTLeaveStartTime = '08:00'"
                    _Qry &= vbCrLf & " ,FTLeaveEndTime = '17:00'"
                    _Qry &= vbCrLf & " ,FTLeaveNote = N'" & HI.UL.ULF.rpQuoted(FTCause & " BY " & FTPunishmentBy) & "'"
                    _Qry &= vbCrLf & " ,FNLeaveTotalTime=480 " '' & FNNetTime.Value
                    _Qry &= vbCrLf & " ,FNLeaveTotalTimeMin=" & (Val(HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd)) + 1) * 480

                    '***** state appoved originale
                    _Qry &= vbCrLf & " ,FTApproveState='1' "
                    _Qry &= vbCrLf & " , FTApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " , FTApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " , FTApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                    '***** send approved
                    _Qry &= vbCrLf & " , FTSendApproveState = '1'"
                    _Qry &= vbCrLf & " , FDSendApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " , FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " , FTSendApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                    '***** mng approved
                    _Qry &= vbCrLf & " , FTMngApproveState = '1'"
                    _Qry &= vbCrLf & " , FDMngApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " , FTMngApproveTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " , FTMngApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                    _Qry &= vbCrLf & " ,FTApproveState='0' "
                    _Qry &= vbCrLf & " , FTDirApproveState='0'"



                    _Qry &= vbCrLf & " ,FNSpecialLeaveType='0'"
                    _Qry &= vbCrLf & " , FTStaCalSSO='0'"
                    _Qry &= vbCrLf & " , FTStaLeaveDay='0'"
                    _Qry &= vbCrLf & " ,FTStateMedicalCertificate= '0'"
                    _Qry &= vbCrLf & " ,FTMedicalCertificateName='" & HI.UL.ULF.rpQuoted(_PicName) & "' "
                    _Qry &= vbCrLf & " , FTStateDeductVacation='0'"

                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
                    _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
                    _Qry &= vbCrLf & " AND FTLeaveType = '99'"

                End If
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                ''Call savePDF()

                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
                _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
                _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
                _Qry &= vbCrLf & " AND FTLeaveType = '99'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                'If Not (ocmapprove.Visible) And _leavePro = False Then
                '    Call ApproveData()
                'End If

            Else
                '''Edit Data leave
                '''
                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily SET"
                _Qry &= vbCrLf & " FTUpdUser = '" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & " ,FTUpdDate = " & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,FTHoliday = '0' " '' & FTStateNotMergeHoliday.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,FNLeaveTotalDay = " & HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd) + 1 & "" ''FTNetDay
                _Qry &= vbCrLf & " ,FTLeaveType = '99'" '' & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
                _Qry &= vbCrLf & " ,FTLeavePay = '0'" ''& FTStateLeavepay.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,FTLeaveStartTime = '08:00'"
                _Qry &= vbCrLf & " ,FTLeaveEndTime = '17:00'"
                _Qry &= vbCrLf & " ,FTLeaveNote = N'" & HI.UL.ULF.rpQuoted(FTCause & " BY " & FTPunishmentBy) & "'"
                _Qry &= vbCrLf & " ,FNLeaveTotalTime= 480 " ''& FNNetTime.Value
                _Qry &= vbCrLf & " ,FNLeaveTotalTimeMin=" & (Val(HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd)) + 1) * 480 ''& ocetotaltime.Value

                _Qry &= vbCrLf & " ,FNSpecialLeaveType='0'"
                _Qry &= vbCrLf & " , FTStaCalSSO='0'"
                _Qry &= vbCrLf & " , FTStaLeaveDay='0'"
                _Qry &= vbCrLf & " ,FTStateMedicalCertificate= '0'"
                _Qry &= vbCrLf & " ,FTMedicalCertificateName='' "
                _Qry &= vbCrLf & " , FTStateDeductVacation='0'"

                _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
                _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
                _Qry &= vbCrLf & " AND FTLeaveType = '99'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                'Call savePDF()
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function DeleteDataLeaveAdvance(ByVal FtDateStart As String, ByVal FtDateEnd As String, ByVal FTCause As String, ByVal FTPunishmentBy As String, ByVal _HRProcess As String) As Boolean
        Try
            Dim _StateMngApp As Boolean = False
            Dim _Qry As String
            Dim _QryFile As String
            Dim _PicName As String = ""

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " And FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
            _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '99'"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "'"
            _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '99' "

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(FtDateEnd)
            Dim _NextProcDate As String = ""
            Dim nNextDay As Double = 0

            _NextProcDate = HI.UL.ULDate.ConvertEnDB(FtDateStart)

            Do While _NextProcDate <= _EndProcDate
                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpID.Properties.Tag.ToString, _NextProcDate, _NextProcDate)

                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))
            Loop
            HI.HRCAL.Calculate.DisposeObject()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function





    Private Function ApproveDataLeave(ByVal FtDateStart As String, ByVal FtDateEnd As String, ByVal FTCause As String, ByVal FTPunishmentBy As String, ByVal _HRProcess As String)
        Try
            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(FtDateEnd)
            Dim _NextProcDate As String = ""
            Dim nNextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(FtDateStart)

            Dim _TotalHour As Double = 0
            Dim _FNTotalMonute As Double = 0
            Dim _FNTotalPayHour As Double = 0
            Dim _FNTotalPayMonute As Double = 0
            Dim _FNTotalNotPayHour As Double = 0
            Dim _FNTotalNotPayMonute As Double = 0
            Dim _TmpTotalHour As Double = 0
            Dim _TmpFNTotalMonute As Double = 0
            Dim _TmpFNTotalPayHour As Double = 0
            Dim _TmpFNTotalPayMonute As Double = 0
            Dim _TmpFNTotalNotPayHour As Double = 0
            Dim _TmpFNTotalNotPayMonute As Double = 0
            Dim _dtWeekend As DataTable
            Dim _dtHoliday As DataTable
            Dim _SkipProcess As Boolean
            Dim _Qry As String
            Dim _LeaveCode As String = "99"
            Dim _WeekEnd As Integer
            Dim _LeavePragNentPay As Integer = 0
            Dim _LeavePragNentNotPay As Boolean = False
            Dim _EmpTypeWeekly As DataTable

            '_TmpTotalHour = CDbl(Format(Val(FNNetTime.Value), "0.00"))
            '_TmpFNTotalMonute = ocetotaltime.Value

            'If (Me.FTStateLeavepay.Checked) Then
            '    _TmpFNTotalPayHour = _TmpTotalHour
            '    _TmpFNTotalPayMonute = _TmpFNTotalMonute
            'Else
            '    _TmpFNTotalNotPayHour = _TmpTotalHour
            '    _TmpFNTotalNotPayMonute = _TmpFNTotalMonute
            'End If

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(FtDateStart) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(FtDateEnd) & "' "
            _Qry &= vbCrLf & "  AND FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & " "

            _EmpTypeWeekly = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
            _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  As W WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtWeekend.Rows.Count <= 0 Then

            Else
                _EmpTypeWeekly.Rows.Clear()
            End If


            _Qry = "SELECt   FDHolidayDate   "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) WHERE FTStateActive='1' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID
            _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            Dim _EmpOrgShift As String = FNHSysShiftID.Text
            Dim _EmpShift As String = _EmpOrgShift
            Dim _EmpPgmCode As String
            Dim _TotalWorkHour As Double
            Dim _DateReset As String = ""

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " ) As T"

            _DateReset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


            'If _LeaveCode = 97 Then

            '    _Qry = "Select Top 1 FNLeavePay "
            '    _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave WITH(NOLOCK) "
            '    _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
            '    _Qry &= vbCrLf & " AND FTLeaveCode ='" & _LeaveCode & "' "

            '    _LeavePragNentPay = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

            '    _Qry = "   SELECT        COUNT(FTDateTrans) AS FNPayDay"
            '    _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK) "
            '    _Qry &= vbCrLf & " WHERE        (FTLeaveType ='" & _LeaveCode & "')"
            '    _Qry &= vbCrLf & " AND (FNHSysEmpID =" & Val(FNHSysEmpID.Properties.Tag.ToString) & " ) "
            '    _Qry &= vbCrLf & " AND (FTDateTrans < N'" & _NextProcDate & "')"
            '    _Qry &= vbCrLf & " and ( FTDateTrans >='" & _DateReset & "' )"
            '    _Qry &= vbCrLf & " AND (FNTotalPayMinute > 0) "

            '    _LeavePragNentPay = _LeavePragNentPay - Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

            'End If

            Dim _dt As DataTable
            Dim _FTStaHoliday As String = "0"
            _Qry = " SELECT TOP 1   L.FTLeaveReset, "
            _Qry &= vbCrLf & "   L.FNLeaveRight, L.FNLeavePay,ISNULL(L.FTStaHoliday,'') AS FTStaHoliday,ISNULL(L.FTStateDeductVacation,'0') AS FTStateDeductVacation "
            _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " AND L.FTLeaveCode='99'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _dt.Rows
                _FTStaHoliday = R!FTStaHoliday.ToString
            Next


            Do While _NextProcDate <= _EndProcDate

                _EmpPgmCode = ""
                _EmpPgmCode = ""
                _TotalWorkHour = 8

                _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)


                _TmpTotalHour = 8
                _TmpFNTotalMonute = 480
                _TotalHour = _TmpTotalHour
                _FNTotalMonute = _TmpFNTotalMonute
                ' End Select

                'If (Me.FTStateLeavepay.Checked) Then
                '    _FNTotalPayHour = _TotalHour
                '    _FNTotalPayMonute = _FNTotalMonute
                'Else
                _FNTotalNotPayHour = _TotalHour
                _FNTotalNotPayMonute = _FNTotalMonute
                'End If

                _SkipProcess = False
                _LeavePragNentNotPay = False

                If Not (_FTStaHoliday = "1") Then  ''Me.FTStateNotMergeHoliday.Checked
                Else

                    ' If LeaveNotMergeSunday <> "1" Then
                    For Each Rday As DataRow In _dtWeekend.Rows
                        If Rday.Item(_WeekEnd - 1).ToString = "1" Then
                            _SkipProcess = True
                        End If
                        Exit For
                    Next
                    ' End If

                    If _SkipProcess = False Then
                        For Each Dr As DataRow In _dtHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _SkipProcess = True
                        Next
                    End If

                    If _SkipProcess = False Then
                        For Each Dr As DataRow In _EmpTypeWeekly.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _SkipProcess = True
                        Next
                    End If

                End If

                If Not (_SkipProcess) Then

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave(FTInsUser, FTInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,FNHSysEmpID,FTDateTrans,FTLeaveType"
                    _Qry &= vbCrLf & ",FNTotalHour,FNTotalMinute,FNTotalPayHour,FNTotalPayMinute"
                    _Qry &= vbCrLf & ",FNTotalNotPayHour,FNTotalNotPayMinute,FTLeaveStartTime,FTLeaveEndTime,FTStaCalSSO,FTStaLeaveDay"
                    _Qry &= vbCrLf & ",FNLeaveTotalDay,FTStateMedicalCertificate,FTStateDeductVacation)"
                    _Qry &= vbCrLf & "  SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Val(FNHSysEmpID.Properties.Tag.ToString) & ",'" & _NextProcDate & "' "
                    _Qry &= vbCrLf & " ,'" & _LeaveCode & "'"
                    _Qry &= vbCrLf & " ," & _TotalHour & ""
                    _Qry &= vbCrLf & " ," & _FNTotalMonute & ""

                    'If (_LeaveCode = "97" And (_LeavePragNentPay <= 0 Or _LeavePragNentNotPay)) And FTStateLeavepay.Checked = True Then

                    '    _Qry &= vbCrLf & " ,0"
                    '    _Qry &= vbCrLf & " ,0"
                    '    _Qry &= vbCrLf & " ," & _TotalHour & ""
                    '    _Qry &= vbCrLf & " ," & _FNTotalMonute & ""

                    'Else

                    _Qry &= vbCrLf & " ," & _FNTotalPayHour & ""
                    _Qry &= vbCrLf & " ," & _FNTotalPayMonute & ""
                    _Qry &= vbCrLf & " ," & _FNTotalNotPayHour & ""
                    _Qry &= vbCrLf & " ," & _FNTotalNotPayMonute & ""

                    'End If

                    _Qry &= vbCrLf & ",'08:00'"
                    _Qry &= vbCrLf & ",'17:00'"
                    _Qry &= vbCrLf & ",'0'"
                    _Qry &= vbCrLf & ",'0'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.UDayDiff(DateInterval.Day, FtDateStart, FtDateEnd) + 1 & "" ''FTNetDay
                    _Qry &= vbCrLf & ",'0'"
                    _Qry &= vbCrLf & ",'0'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    'If _LeaveCode = "97" And FTStateLeavepay.Checked = True Then
                    '    If Not (_LeavePragNentNotPay) Then
                    '        _LeavePragNentPay = _LeavePragNentPay - 1
                    '    End If
                    'End If

                End If

                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpID.Properties.Tag.ToString, _NextProcDate, _NextProcDate)

                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

            Loop

            HI.HRCAL.Calculate.DisposeObject()

            _dtWeekend.Dispose()
            _dtHoliday.Dispose()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function


#End Region

#Region "Expirence"

    Private Sub ocmaddex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpexp.Click
        If VerrifyDataExperience() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then
                Dim _Str As String = ""
                Dim _Health As String = ""
                Dim _Equipment As String = ""
                If ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH ='" & FTHealthRiskFactors.Text & "'"
                Else
                    _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameEN ='" & FTHealthRiskFactors.Text & "'"
                End If
                _Health = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")

                If ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameTH ='" & FTProtectiveEquipment.Text & "'"
                Else
                    _Str = "select L.FNListIndex   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L  where L.FTNameEN ='" & FTProtectiveEquipment.Text & "'"
                End If
                _Equipment = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_FIXED, "0")

                Dim _Qry As String = ""

                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeExperience SET "
                _Qry &= vbCrLf & " FTCmpName=N'" & HI.UL.ULF.rpQuoted(FTCmpName.Text) & "' "
                _Qry &= vbCrLf & ", FTBusinessType=N'" & HI.UL.ULF.rpQuoted(FTBusinessType.Text) & "' "
                _Qry &= vbCrLf & ", FTContractName=N'" & HI.UL.ULF.rpQuoted(FTContractName.Text) & "' "
                _Qry &= vbCrLf & ", FTPosition=N'" & HI.UL.ULF.rpQuoted(FTPosition.Text) & "' "
                _Qry &= vbCrLf & ", FTResponsibility=N'" & HI.UL.ULF.rpQuoted(FTResponsibility.Text) & "' "
                _Qry &= vbCrLf & ", FDStartDate=N'" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "' "
                _Qry &= vbCrLf & ", FDEndDate=N'" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
                _Qry &= vbCrLf & ", FTEndSalary=" & FTEndSalary.Value & " "
                _Qry &= vbCrLf & ", FTResignCause=N'" & HI.UL.ULF.rpQuoted(FTResignCause.Text) & "' "
                _Qry &= vbCrLf & ", FTHealthRiskFactors=N'" & _Health & "' "
                _Qry &= vbCrLf & ", FTProtectiveEquipment=N'" & _Equipment & "'"
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNExperienceSeq.Value

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeExperience WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                    Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeExperience(FNHSysEmpID,FNSeqNo, FTCmpName, FTBusinessType, FTContractName, FTPosition, FTResponsibility, FDStartDate, FDEndDate, FTEndSalary, FTResignCause,FTHealthRiskFactors ,FTProtectiveEquipment"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTCmpName.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTBusinessType.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTContractName.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTPosition.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResponsibility.Text) & "'  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "'  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "'  "
                    _Qry &= vbCrLf & "," & FTEndSalary.Value & "  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResignCause.Text) & "'  "
                    _Qry &= vbCrLf & ",N'" & _Health & "' "
                    _Qry &= vbCrLf & ",N'" & _Equipment & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If


                _Qry = "UPDATE THRTEmployeeExperience SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeeExperience INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRTEmployeeExperience WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeExperience.FNSeqNo=T1.FNSeqNo AND THRTEmployeeExperience.FNHSysEmpID=T1.FNHSysEmpID"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                HI.TL.HandlerControl.ClearControl(ogbex)
                Call ShowExperience(FNHSysEmpID.Properties.Tag.ToString)

            End If
        End If
    End Sub

    Private Sub ocmremoveex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpexp.Click
        If FNExperienceSeq.Value <= 0 Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeExperience  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNExperienceSeq.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRTEmployeeExperience SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM THRTEmployeeExperience INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRTEmployeeExperience WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRTEmployeeExperience.FNSeqNo=T1.FNSeqNo AND THRTEmployeeExperience.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbex)
        Call ShowExperience(FNHSysEmpID.Properties.Tag.ToString)
    End Sub

    Private Sub ogvex_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvExperience.Click
        With ogvExperience

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNExperienceSeq.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            FTCmpName.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTCmpName").ToString
            FTBusinessType.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTBusinessType").ToString
            FTContractName.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTContractName").ToString
            FTResponsibility.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTResponsibility").ToString
            FTPosition.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPosition").ToString

            Try
                FDStartDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDStartDate").ToString)
            Catch ex As Exception
                FDStartDate.DateTime = Nothing
            End Try

            Try
                FDEndDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDEndDate").ToString)
            Catch ex As Exception
                FDEndDate.DateTime = Nothing
            End Try

            FTEndSalary.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTEndSalary").ToString)
            FTResignCause.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTResignCause").ToString
            FTHealthRiskFactors.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTHealthRiskFactors").ToString
            FTProtectiveEquipment.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTProtectiveEquipment").ToString
            FTCmpName.Focus()

        End With
    End Sub

#End Region

#Region "Family"

    Private Sub ocmaddfam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpfam.Click
        If VerrifyDataFamily() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then

                Dim _Qry As String = ""

                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFamily SET "
                _Qry &= vbCrLf & " FTFamilyName=N'" & HI.UL.ULF.rpQuoted(FTFamilyName.Text) & "' "
                _Qry &= vbCrLf & ", FTFamilySex=N'" & HI.UL.ULF.rpQuoted(FTFamilySex.SelectedIndex.ToString) & "' "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNFamilySeqNo.Value

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFamily WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                    Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFamily(FNHSysEmpID,FNSeqNo, FTFamilyName, FTFamilySex "
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTFamilyName.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTFamilySex.SelectedIndex.ToString) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If


                _Qry = "UPDATE THRMEmployeeFamily SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM THRMEmployeeFamily INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRMEmployeeFamily WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRMEmployeeFamily.FNSeqNo=T1.FNSeqNo AND THRMEmployeeFamily.FNHSysEmpID=T1.FNHSysEmpID"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                HI.TL.HandlerControl.ClearControl(ogbfam)
                Call ShowFamily(FNHSysEmpID.Properties.Tag.ToString)

            End If
        End If
    End Sub

    Private Sub ocmremovefam_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpfam.Click
        If FNFamilySeqNo.Value <= 0 Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeFamily  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNFamilySeqNo.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployeeFamily SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployeeFamily INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployeeFamily WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployeeFamily.FNSeqNo=T1.FNSeqNo AND THRMEmployeeFamily.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbfam)
        Call ShowFamily(FNHSysEmpID.Properties.Tag.ToString)
    End Sub

    Private Sub ogvFamily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvFamily.Click
        With ogvFamily

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNFamilySeqNo.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            FTFamilyName.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTFamilyName").ToString

            Try
                FTFamilySex.SelectedIndex = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTFamilySexInd").ToString)
            Catch ex As Exception
            End Try

            FTFamilyName.Focus()
        End With
    End Sub
#End Region

#Region "Child"

    Private Sub SimpleButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmAddEmpChild.Click
        If VerrifyDataChild() = False Then Exit Sub
        If Me.VerrifyData Then
            If Me.SaveData() Then

                Dim _Qry As String = ""

                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeChild SET "
                _Qry &= vbCrLf & " FTChildName=N'" & HI.UL.ULF.rpQuoted(FTChildName.Text) & "' "
                _Qry &= vbCrLf & ", FDChildBirthDate=N'" & HI.UL.ULDate.ConvertEnDB(FDChildBirthDate.Text) & "' "
                _Qry &= vbCrLf & ",  FTChildSex=N'" & HI.UL.ULF.rpQuoted(FTChildSex.SelectedIndex.ToString) & "' "
                _Qry &= vbCrLf & ", FTStudySta =N'" & HI.UL.ULF.rpQuoted(FTStudySta.SelectedIndex.ToString) & "'"
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " ,FTStateNotDisTax ='" & IIf(Me.FTStateNotDisTax.Checked, "1", "0") & "'"
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNChildSeqNo.Value

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                    Dim tSeqNo As String = ""
                    tSeqNo = Me.FNChildSeqNo.Value.ToString

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployeeChild(FNHSysEmpID,FNSeqNo, FTChildName, FDChildBirthDate,  FTChildSex, FTStudySta"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime,FTStateNotDisTax)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTChildName.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDChildBirthDate.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTChildSex.SelectedIndex.ToString) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTStudySta.SelectedIndex.ToString) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,'" & IIf(Me.FTStateNotDisTax.Checked, "1", "0") & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If


                HI.TL.HandlerControl.ClearControl(ogbchild)
                Call ShowFamily(FNHSysEmpID.Properties.Tag.ToString)

            End If
        End If
    End Sub

    Private Sub ocmcchild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpchild.Click
        If FNChildSeqNo.Value <= 0 Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  THRMEmployeeChild  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNSeqNo=" & Me.FNChildSeqNo.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployeeChild SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployeeChild INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNSeqNo) AS FNNo, FNSeqNo,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployeeChild WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployeeChild.FNSeqNo=T1.FNSeqNo AND THRMEmployeeChild.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbchild)
        Call ShowFamily(FNHSysEmpID.Properties.Tag.ToString)
    End Sub

    Private Sub ogvChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvChild.Click
        With ogvChild

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNChildSeqNo.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            FTChildName.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTChildName").ToString

            Try
                FDChildBirthDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDChildBirthDate").ToString)
            Catch ex As Exception
                FDChildBirthDate.DateTime = Nothing
            End Try


            Try
                FTChildSex.SelectedIndex = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTChildSex").ToString)
            Catch ex As Exception
            End Try

            Try
                FTStudySta.SelectedIndex = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTStudySta").ToString)
            Catch ex As Exception
            End Try

            Try
                FTStateNotDisTax.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateNotDisTax").ToString) = "1"
            Catch ex As Exception
            End Try



            FTChildName.Focus()
        End With
    End Sub

#End Region

#Region "Permission"

    Private Sub CheckPermissionSalary()
        Dim _Pass As Boolean = False
        Dim _Qry As String = ""

        If Not (HI.ST.SysInfo.Admin) Then

            _Qry = " Select TOP 1  UPT.FTStateSalary  "
            _Qry &= vbCrLf & " FROM             " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "'  AND (UPT.FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " ) AND UPT.FTStateSalary='1' "

            _Pass = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "1")
        Else
            _Pass = True
        End If

        Me.otpempfin.PageVisible = _Pass

    End Sub

    Private Sub CheckPaidPayroll()

        Dim _Pass As Boolean = False
        _Pass = HI.HRCAL.Calculate.CheckPaidPayroll(Val("" & FNHSysEmpID.Properties.Tag.ToString))

        FNHSysEmpTypeId.Properties.Buttons(0).Visible = Not (_Pass)
        FNHSysEmpTypeId.Enabled = Not (_Pass)

        FNHSysDivisonId.Properties.Buttons(0).Visible = Not (_Pass)
        FNHSysDivisonId.Enabled = Not (_Pass)

        FNHSysDeptId.Properties.Buttons(0).Visible = Not (_Pass)
        FNHSysDeptId.Enabled = Not (_Pass)

        FNHSysSectId.Properties.Buttons(0).Visible = Not (_Pass)
        FNHSysSectId.Enabled = Not (_Pass)

        FNHSysUnitSectId.Properties.Buttons(0).Visible = Not (_Pass)
        FNHSysUnitSectId.Enabled = Not (_Pass)

        FNHSysPositId.Properties.Buttons(0).Visible = Not (_Pass)
        FNHSysPositId.Enabled = Not (_Pass)

    End Sub

#End Region

    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged

        Dim _CmpH As String = ""
        _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

        If Me.FNHSysEmpID.Text = HI.TL.Document.GetDocumentNo(_FormHeader(0).SysDBName, _FormHeader(0).SysTableName, "", True, _CmpH).ToString() Then
            Call ShowLeaveInfo(Me.FNHSysEmpID.Properties.Tag.ToString)
        End If

        Call CheckPermissionSalary()
    End Sub

    Private Sub FDBirthDate_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FDBirthDate.EditValueChanged
        If HI.UL.ULDate.CheckDate(FDBirthDate.Text) <> "" Then
            Dim _Qry As String
            Dim sDate As String = HI.UL.ULDate.ConvertEnDB(FDBirthDate.Text)
            Dim eDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_HR))

            _Qry = "SP_Datediff '" & sDate & "',N'" & eDate & "' "
            Dim oRow As DataRow = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows(0)
            Dim nYear As Integer = Val(oRow("FNYear"))
            Dim nMonth As Integer = Val(oRow("FNMonth"))
            Dim nDay As Integer = Val(oRow("FNDay"))

            olaEmpAgeYear.Text = (IIf(nYear < 0, 0, nYear)).ToString
            olaEmpAgeMonth.Text = (IIf(nMonth < 0, 0, nMonth)).ToString
            olaEmpAgeDay.Text = (IIf(nDay < 0, 0, nDay)).ToString

        Else

            olaEmpAgeYear.Text = "0"
            olaEmpAgeMonth.Text = "0"
            olaEmpAgeDay.Text = "0"

        End If
    End Sub

    Private Sub wEmpMaster_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            _LoadForm = True
            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            RemoveHandler FNHSysEmpID.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            ' Call LoadPass()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysCmpId.EditValueChanged

        If Not (_ProcDefault) Then
            If "" & FNHSysCmpId.Properties.Tag.ToString <> "" Then
                HI.TL.HandlerControl.ClearControl(Me, False, {Me.FNHSysCmpId.Name.ToString, Me.FNHSysCmpId_None.Name.ToString})
            End If
        End If

    End Sub

    Private Sub otab_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otab.SelectedPageChanged
        Call TabChenge()
    End Sub

    Private Sub TabChenge()

        ocmaddEmpeduc.Visible = (otab.SelectedTabPage.Name = otpempeduc.Name)
        ocmremoveEmpeduc.Visible = (otab.SelectedTabPage.Name = otpempeduc.Name)
        ocmclearEmpeduc.Visible = (otab.SelectedTabPage.Name = otpempeduc.Name)

        ocmaddEmpfam.Visible = ((otab.SelectedTabPage.Name = otpfamily.Name) And (otbfamily.SelectedTabPage.Name = otpbrother.Name))
        ocmremoveEmpfam.Visible = ((otab.SelectedTabPage.Name = otpfamily.Name) And (otbfamily.SelectedTabPage.Name = otpbrother.Name))
        ocmclearEmpfam.Visible = ((otab.SelectedTabPage.Name = otpfamily.Name) And (otbfamily.SelectedTabPage.Name = otpbrother.Name))

        ocmAddEmpChild.Visible = ((otab.SelectedTabPage.Name = otpfamily.Name) And (otbfamily.SelectedTabPage.Name = otpchild.Name))
        ocmremoveEmpchild.Visible = ((otab.SelectedTabPage.Name = otpfamily.Name) And (otbfamily.SelectedTabPage.Name = otpchild.Name))
        ocmclearEmpchild.Visible = ((otab.SelectedTabPage.Name = otpfamily.Name) And (otbfamily.SelectedTabPage.Name = otpchild.Name))

        ocmaddEmpexp.Visible = (otab.SelectedTabPage.Name = otpeexperience.Name)
        ocmremoveEmpexp.Visible = (otab.SelectedTabPage.Name = otpeexperience.Name)
        ocmclearEmpexp.Visible = (otab.SelectedTabPage.Name = otpeexperience.Name)

        ocmaddEmpresign.Visible = (otab.SelectedTabPage.Name = otpempleavein.Name)
        ocmremoveEmpresign.Visible = (otab.SelectedTabPage.Name = otpempleavein.Name)
        ocmclearEmpresign.Visible = (otab.SelectedTabPage.Name = otpempleavein.Name)

        ocmaddEmppuns.Visible = (otab.SelectedTabPage.Name = otpemppunishment.Name)
        ocmremoveEmppuns.Visible = (otab.SelectedTabPage.Name = otpemppunishment.Name)
        ocmclearEmppuns.Visible = (otab.SelectedTabPage.Name = otpemppunishment.Name)

        ocmaddEmpPass.Visible = (otab.SelectedTabPage.Name = XtraTabPage1.Name)
        ocmremoveEmpPass.Visible = (otab.SelectedTabPage.Name = XtraTabPage1.Name)
        ocmclearPass.Visible = (otab.SelectedTabPage.Name = XtraTabPage1.Name)

        ocmaddEmpVisa.Visible = (otab.SelectedTabPage.Name = XtraTabPage2.Name)
        ocmremoveEmpVisa.Visible = (otab.SelectedTabPage.Name = XtraTabPage2.Name)
        ocmclearVisa.Visible = (otab.SelectedTabPage.Name = XtraTabPage2.Name)

        ocmaddEmpWork.Visible = (otab.SelectedTabPage.Name = XtraTabPage3.Name)
        ocmremoveEmpWork.Visible = (otab.SelectedTabPage.Name = XtraTabPage3.Name)
        ocmclearWork.Visible = (otab.SelectedTabPage.Name = XtraTabPage3.Name)

        ocmaddEmpMOU.Visible = (otab.SelectedTabPage.Name = XtraTabPage4.Name)
        ocmremoveEmpMOU.Visible = (otab.SelectedTabPage.Name = XtraTabPage4.Name)
        ocmclearMOU.Visible = (otab.SelectedTabPage.Name = XtraTabPage4.Name)

        ocmaddEmpOther.Visible = (otab.SelectedTabPage.Name = XtraTabPage5.Name)
        ocmremoveEmpOther.Visible = (otab.SelectedTabPage.Name = XtraTabPage5.Name)
        ocmclearOther.Visible = (otab.SelectedTabPage.Name = XtraTabPage5.Name)
        'ocmReadDocumentfile.Visible = (otab.SelectedTabPage.Name = XtraTabPage1.Name)
        'Select Case otab.SelectedTabPage.Name
        '    Case Is = "XtraTabPage2"
        '        ocmReadDocumentfile.Visible = (otab.SelectedTabPage.Name = XtraTabPage2.Name)
        '    Case Is = "XtraTabPage3"
        '        ocmReadDocumentfile.Visible = (otab.SelectedTabPage.Name = XtraTabPage3.Name)
        '    Case Is = "XtraTabPage4"
        '        ocmReadDocumentfile.Visible = (otab.SelectedTabPage.Name = XtraTabPage4.Name)
        '    Case Is = "XtraTabPage5"
        '        ocmReadDocumentfile.Visible = (otab.SelectedTabPage.Name = XtraTabPage5.Name)
        'End Select

        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub otbfamily_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbfamily.SelectedPageChanged
        Call TabChenge()
    End Sub

    Private Sub FNHSysShiftID_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysShiftID.EditValueChanged
        If Not (_ProcLoad) Then
            If Me.FNHSysShiftID.Text <> "" Then

                Dim _Qry As String = " SELECT TOP 1 FNScanCtrl FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMTimeShift WITH(NOLOCK) WHERE FTShiftCode=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysShiftID.Text) & "' "

                Try
                    Me.FNScanCtrl.SelectedIndex = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub otab_SizeChanged(sender As Object, e As EventArgs) Handles otab.SizeChanged
        opntabwork.Visible = (otab.Width <= 967)
        '' opngn.Visible = (otab.Width <= 967)
        opnfin.Visible = (otab.Width <= 1009)
    End Sub
    Private _CountTrainType As Integer = 0
    Private _CountRowAll As Integer = 0
    Private Sub InitSummaryStartValue()
        _CountTrainType = 0
        _CountRowAll = 0
    End Sub

    Private Sub GetDateCondition()

    End Sub

    Private Sub ogvtrain_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvtrain.CellMerge
        Try
            With ogvtrain
                Select Case e.Column.FieldName
                    Case "FTType"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTPurpose"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTEvaluate"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTStartTime"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTEndTime"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FNSumHours"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FCCostPerEmp"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTTrainNote"
                        If .GetRowCellValue(e.RowHandle1, "FTTrainCode").ToString = .GetRowCellValue(e.RowHandle2, "FTTrainCode") Then
                            e.Merge = (e.CellValue1 = e.CellValue2)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                End Select
            End With
        Catch ex As Exception

        End Try
    End Sub


    'Private Sub ogb_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs)
    '    Try
    '        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
    '            InitSummaryStartValue()
    '        End If
    '        With ogb

    '            If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
    '                If .GetRowCellValue(e.RowHandle, CType(e.Item, GridSummaryItem).FieldName) <> "" Then
    '                    _CountTrainType += 1
    '                End If
    '                _CountRowAll += 1
    '            End If
    '            e.TotalValue = (_CountTrainType * 100) / _CountRowAll
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub FNOptionCal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNOptionCal.SelectedIndexChanged
    '    If _LoadForm = False Then Exit Sub
    '    Dim _RowCount As Integer = 0
    '    Dim _GetCurDate As String
    '    Try
    '        Select Case FNOptionCal.SelectedIndex
    '            Case 2
    '                Me.FTStartDateOp.Enabled = True : Me.FTEndDateOp.Enabled = True : Me.ocmcaldate.Enabled = True
    '            Case Else
    '                Me.FTStartDateOp.Enabled = False : Me.FTEndDateOp.Enabled = False : Me.ocmcaldate.Enabled = False
    '                Me.FTStartDateOp.Text = "" : Me.FTEndDateOp.Text = ""
    '        End Select

    '        If Me.FNOptionCal.SelectedIndex = 0 Then
    '            Call ShowTrainning(Me.FNHSysEmpID.Properties.Tag)

    '        ElseIf Me.FNOptionCal.SelectedIndex = 1 Then
    '            _GetCurDate = HI.Conn.SQLConn.GetField("select convert(varchar(10),getdate(),111) AS DateCal", Conn.DB.DataBaseName.DB_HR)
    '            For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
    '                If Microsoft.VisualBasic.Left(UL.ULDate.ConvertEnDB(R!FDDateBegin.ToString), 4) <> Microsoft.VisualBasic.Left(_GetCurDate, 4) Then
    '                    CType(ogctrain.DataSource, DataTable).BeginInit()
    '                    CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
    '                    CType(ogctrain.DataSource, DataTable).EndInit()
    '                End If
    '                _RowCount += 1
    '            Next
    '            CType(ogctrain.DataSource, DataTable).AcceptChanges()
    '            If CType(ogctrain.DataSource, DataTable).Rows.Count > 0 Then
    '            Else
    '                MG.ShowMsg.mInfo("ไม่สามารถคำนวณได้ เนื่องจากไม่มีข้อมูลตามเงื่อนไขที่กำหนด", 201701071006, Me.Text, "", MessageBoxIcon.Information)
    '                Me.FNOptionCal.SelectedIndex = 0
    '            End If

    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub ocmcaldate_Click(sender As Object, e As EventArgs) Handles ocmcaldate.Click
    '    Dim _RowCount As Integer = 0

    '    If FTStartDateOp.Text = "" Then
    '        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
    '        Me.FTStartDateOp.Focus()
    '        Exit Sub
    '    End If

    '    Try
    '        If Me.FTStartDateOp.Text <> "" And Me.FTEndDateOp.Text = "" Then
    '            For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
    '                If CDate(R!FDDateBegin.ToString) < CDate(Me.FTStartDateOp.Text) Then
    '                    CType(ogctrain.DataSource, DataTable).BeginInit()
    '                    CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
    '                    CType(ogctrain.DataSource, DataTable).EndInit()
    '                End If
    '                _RowCount += 1
    '            Next
    '            CType(ogctrain.DataSource, DataTable).AcceptChanges()
    '            If CType(ogctrain.DataSource, DataTable).Rows.Count > 0 Then
    '            Else
    '                MG.ShowMsg.mInfo("ไม่สามารถคำนวณได้ เนื่องจากไม่มีข้อมูลตามเงื่อนไขที่กำหนด", 201701071006, Me.Text, "", MessageBoxIcon.Information)
    '                Call ShowTrainning(Me.FNHSysEmpID.Properties.Tag)
    '            End If
    '        Else
    '            For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
    '                If CDate(R!FDDateBegin.ToString) < CDate(Me.FTStartDateOp.Text) Then
    '                    CType(ogctrain.DataSource, DataTable).BeginInit()
    '                    CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
    '                    CType(ogctrain.DataSource, DataTable).EndInit()
    '                End If
    '                _RowCount += 1
    '            Next
    '            CType(ogctrain.DataSource, DataTable).AcceptChanges()
    '            _RowCount = 0
    '            For Each R As DataRow In CType(ogctrain.DataSource, DataTable).Rows
    '                If CDate(R!FDDateBegin.ToString) > CDate(Me.FTEndDateOp.Text) Then
    '                    CType(ogctrain.DataSource, DataTable).BeginInit()
    '                    CType(ogctrain.DataSource, DataTable).Rows(_RowCount).Delete()
    '                    CType(ogctrain.DataSource, DataTable).EndInit()
    '                End If
    '                _RowCount += 1
    '            Next
    '            CType(ogctrain.DataSource, DataTable).AcceptChanges()
    '            If CType(ogctrain.DataSource, DataTable).Rows.Count > 0 Then
    '            Else
    '                MG.ShowMsg.mInfo("ไม่สามารถคำนวณได้ เนื่องจากไม่มีข้อมูลตามเงื่อนไขที่กำหนด", 201701071006, Me.Text, "", MessageBoxIcon.Information)
    '                Call ShowTrainning(Me.FNHSysEmpID.Properties.Tag)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    Private _FilePath As String
    Private data As Byte()
    Private _Pdfdata As Byte()

    'Private Sub ocmattach_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
    '    Dim _OpnDial As New OpenFileDialog
    '    Dim _extension As String = ""
    '    Dim _FTA1 As String = ""
    '    _FTA1 = FTFileRef.ToString
    '    With _OpnDial
    '        .Filter = "PDF files|*.pdf"
    '        .RestoreDirectory = True
    '        .Multiselect = True
    '        If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '            '_extension = Path.GetExtension(.FileName)
    '            'If _extension = ".pdf" Then
    '            If _FTA1 <> "" Then
    '                _PathAttach = .FileName
    '                Call _PDFViewer(.FileName)
    '                Call PDFBinary()
    '                Call savePDF()
    '            Else
    '                _PathAttach = .FileName
    '                Call _PDFMOU(.FileName)
    '            End If




    '            'Me.TabAttach.PageVisible = True
    '            'ElseIf _extension = ".xlsx" Then
    '            '    Call _ExcelViewer(.FileName)
    '            'ElseIf _extension = ".docx" Then
    '            '    Call _WordViewer(.FileName)
    '            'End If
    '            ''_StateLoadAttach = True
    '            'Me.TabAttach.PageVisible = True
    '        End If

    '    End With
    'End Sub
    'Private Sub _PDFViewer(ByVal _Filename As String)
    '    'Dim FTFileRef As New DevExpress.XtraPdfViewer.PdfViewer
    '    Try
    '        FTFileRef.LoadDocument(_Filename)
    '    Catch ex As Exception
    '        MsgBox(ex)
    '    End Try
    'End Sub
    'Private Sub _PDFMOU(ByVal _Filename As String)
    '    'Dim FTFileRef As New DevExpress.XtraPdfViewer.PdfViewer
    '    Try
    '        FTFileRef2.LoadDocument(_Filename)
    '    Catch ex As Exception
    '        MsgBox(ex)
    '    End Try
    'End Sub
    'Private Sub PDFBinary()
    '    Try
    '        If _FilePath <> "" Then
    '            Dim _FileType As String = "" : Dim _FileName As String
    '            _FileType = System.IO.Path.GetExtension(_FilePath)
    '            _FileName = System.IO.Path.GetFileName(_FilePath)
    '            Select Case _FileType.ToUpper
    '                Case ".PDF".ToUpper
    '                    Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
    '                    data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
    '                    _Pdfdata = data
    '                    _FBFile = "PDF"
    '            End Select
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private _FBFile As String
    'Private Sub savePDF()
    '    If data Is Nothing Then
    '    Else
    '        Dim _Qry As String
    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee SET"
    '        _Qry &= vbCrLf & " FTInsUser=@FTInsUser, FTFileRef=@FTFileRef ,FBFile=@FBFile"
    '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=@FNHSysEmpID"
    '        Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
    '        cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
    '        Dim p2 As New SqlParameter("@FTFileRef", SqlDbType.VarBinary)
    '        p2.Value = data
    '        Dim p1 As New SqlParameter("@FNHSysEmpID", SqlDbType.Int)
    '        p1.Value = Integer.Parse("0" & Val(FNHSysEmpID.Properties.Tag.ToString))
    '        Dim p6 As New SqlParameter("@FBFile", SqlDbType.NVarChar)
    '        p6.Value = _FBFile

    '        cmd.Parameters.Add(p1)
    '        cmd.Parameters.Add(p2)
    '        cmd.Parameters.Add(p6)
    '        cmd.ExecuteNonQuery()
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
    '    End If
    'End Sub
    'Private Sub savePDF1()
    '    If data Is Nothing Then
    '    Else
    '        Dim _Qry As String
    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee SET"
    '        _Qry &= vbCrLf & " FTInsUser=@FTInsUser, FTFileReff=@FTFileReff ,FBFile=@FBFile"
    '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=@FNHSysEmpID"
    '        Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
    '        cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
    '        Dim p3 As New SqlParameter("@FTFileReff", SqlDbType.VarBinary)
    '        p3.Value = data
    '        Dim p1 As New SqlParameter("@FNHSysEmpID", SqlDbType.Int)
    '        p1.Value = Integer.Parse("0" & Val(FNHSysEmpID.Properties.Tag.ToString))
    '        Dim p6 As New SqlParameter("@FBFile", SqlDbType.NVarChar)
    '        p6.Value = _FBFile
    '        cmd.Parameters.Add(p1)
    '        cmd.Parameters.Add(p3)
    '        cmd.Parameters.Add(p6)
    '        cmd.ExecuteNonQuery()
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
    '    End If
    'End Sub
    'Private Sub savePDF2()
    '    If data Is Nothing Then
    '    Else
    '        Dim _Qry As String
    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee SET"
    '        _Qry &= vbCrLf & " FTInsUser=@FTInsUser, FTFileRefff=@FTFileRefff ,FBFile=@FBFile"
    '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=@FNHSysEmpID"
    '        Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
    '        cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
    '        Dim p3 As New SqlParameter("@FTFileRefff", SqlDbType.VarBinary)
    '        p3.Value = data
    '        Dim p1 As New SqlParameter("@FNHSysEmpID", SqlDbType.Int)
    '        p1.Value = Integer.Parse("0" & Val(FNHSysEmpID.Properties.Tag.ToString))
    '        Dim p6 As New SqlParameter("@FBFile", SqlDbType.NVarChar)
    '        p6.Value = _FBFile
    '        cmd.Parameters.Add(p1)
    '        cmd.Parameters.Add(p3)
    '        cmd.Parameters.Add(p6)
    '        cmd.ExecuteNonQuery()
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
    '    End If
    'End Sub
    'Private Sub savePDF3()
    '    If data Is Nothing Then
    '    Else
    '        Dim _Qry As String
    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee SET"
    '        _Qry &= vbCrLf & " FTInsUser=@FTInsUser, FTFileReffff=@FTFileReffff ,FBFile=@FBFile"
    '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=@FNHSysEmpID"
    '        Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
    '        cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
    '        Dim p3 As New SqlParameter("@FTFileReffff", SqlDbType.VarBinary)
    '        p3.Value = data
    '        Dim p1 As New SqlParameter("@FNHSysEmpID", SqlDbType.Int)
    '        p1.Value = Integer.Parse("0" & Val(FNHSysEmpID.Properties.Tag.ToString))
    '        Dim p6 As New SqlParameter("@FBFile", SqlDbType.NVarChar)
    '        p6.Value = _FBFile
    '        cmd.Parameters.Add(p1)
    '        cmd.Parameters.Add(p3)
    '        cmd.Parameters.Add(p6)
    '        cmd.ExecuteNonQuery()
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
    '    End If
    'End Sub
    'Private Sub savePDF4()
    '    If data Is Nothing Then
    '    Else
    '        Dim _Qry As String
    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee SET"
    '        _Qry &= vbCrLf & " FTInsUser=@FTInsUser, FTFileRefffff=@FTFileRefffff ,FBFile=@FBFile"
    '        _Qry &= vbCrLf & " WHERE FNHSysEmpID=@FNHSysEmpID"
    '        Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
    '        cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
    '        Dim p3 As New SqlParameter("@FTFileRefffff", SqlDbType.VarBinary)
    '        p3.Value = data
    '        Dim p1 As New SqlParameter("@FNHSysEmpID", SqlDbType.Int)
    '        p1.Value = Integer.Parse("0" & Val(FNHSysEmpID.Properties.Tag.ToString))
    '        Dim p6 As New SqlParameter("@FBFile", SqlDbType.NVarChar)
    '        p6.Value = _FBFile
    '        cmd.Parameters.Add(p1)
    '        cmd.Parameters.Add(p3)
    '        cmd.Parameters.Add(p6)
    '        cmd.ExecuteNonQuery()
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
    '    End If
    'End Sub
    'Private Sub PDF_Import()
    '    Try
    '        Dim _FileName As String = ""
    '        Dim folderDlg As New OpenFileDialog
    '        With folderDlg
    '            .Filter = "PDF files|*.pdf"
    '            .FilterIndex = 1
    '            .RestoreDirectory = False
    '            .Multiselect = False
    '            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '                _FilePath = .FileName
    '                Call Readfile()
    '                Call Load_PDF()

    '                Me.otab.SelectedTabPage = Me.XtraTabPage1
    '            Else
    '                _FilePath = ""
    '            End If
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub PDF_Import1()
    '    Try
    '        Dim _FileName As String = ""
    '        Dim folderDlg As New OpenFileDialog
    '        With folderDlg
    '            .Filter = "PDF files|*.pdf"
    '            .FilterIndex = 1
    '            .RestoreDirectory = False
    '            .Multiselect = False
    '            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '                _FilePath = .FileName
    '                Call Readfile1()
    '                Call Load_PDF()
    '                '   Call Load_PDF1()
    '                Me.otab.SelectedTabPage = Me.XtraTabPage2
    '            Else
    '                _FilePath = ""
    '            End If
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub PDF_Import2()
    '    Try
    '        Dim _FileName As String = ""
    '        Dim folderDlg As New OpenFileDialog
    '        With folderDlg
    '            .Filter = "PDF files|*.pdf"
    '            .FilterIndex = 1
    '            .RestoreDirectory = False
    '            .Multiselect = False
    '            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '                _FilePath = .FileName
    '                Call Readfile2()
    '                Call Load_PDF()
    '                Me.otab.SelectedTabPage = Me.XtraTabPage3
    '            Else
    '                _FilePath = ""
    '            End If
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub PDF_Import3()
    '    Try
    '        Dim _FileName As String = ""
    '        Dim folderDlg As New OpenFileDialog
    '        With folderDlg
    '            .Filter = "PDF files|*.pdf"
    '            .FilterIndex = 1
    '            .RestoreDirectory = False
    '            .Multiselect = False
    '            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '                _FilePath = .FileName
    '                Call Readfile3()
    '                Call Load_PDF()
    '                Me.otab.SelectedTabPage = Me.XtraTabPage4
    '            Else
    '                _FilePath = ""
    '            End If
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub PDF_Import4()
    '    Try
    '        Dim _FileName As String = ""
    '        Dim folderDlg As New OpenFileDialog
    '        With folderDlg
    '            .Filter = "PDF files|*.pdf"
    '            .FilterIndex = 1
    '            .RestoreDirectory = False
    '            .Multiselect = False
    '            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '                _FilePath = .FileName
    '                Call Readfile4()
    '                Call Load_PDF()
    '                Me.otab.SelectedTabPage = Me.XtraTabPage5
    '            Else
    '                _FilePath = ""
    '            End If
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click


    '    Select otab.SelectedTabPage.Text
    '        Case "แนบเอกสาร Passport"
    '            PDF_Import()
    '        Case "แนบเอกสาร วีซ่า"
    '            PDF_Import1()
    '        Case "แนบเอกสาร Work Permit"
    '            PDF_Import2()
    '        Case "แนบเอกสาร MOU"
    '            PDF_Import3()
    '        Case "แนบเอกสาร อื่นๆ"
    '            PDF_Import4()
    '    End Select

    'End Sub
    'Private Sub Readfile()
    '    Try
    '        If _FilePath <> "" Then
    '            Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
    '            _FileType = System.IO.Path.GetExtension(_FilePath)
    '            _FileName = System.IO.Path.GetFileName(_FilePath)
    '            'Dim _Tabtype As String = HI.Conn.SQLConn.GetField("SELECT FTFileRef FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  WITH(NOLOCK) WHERE FTEmpCode='" & Me.FNHSysEmpID.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
    '            'Dim _Tabtype1 As String = HI.Conn.SQLConn.GetField("SELECT FTFileReff FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  WITH(NOLOCK) WHERE FTEmpCode='" & Me.FNHSysEmpID.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
    '            'Dim _Tabtype2 As String = HI.Conn.SQLConn.GetField("SELECT FTFileRefff FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  WITH(NOLOCK) WHERE FTEmpCode='" & Me.FNHSysEmpID.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
    '            'Dim _Tabtype3 As String = HI.Conn.SQLConn.GetField("SELECT FTFileReffff FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  WITH(NOLOCK) WHERE FTEmpCode='" & Me.FNHSysEmpID.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "")
    '            'Select Case _FileType.ToUpper
    '            '    Case ".PDF".ToUpper
    '            '        If _Tabtype = "" Then
    '            '            Call _PDFViewer(_FilePath)
    '            '            Call PDFBinary()
    '            '            Call savePDF()
    '            '        ElseIf _Tabtype1 = "" Then
    '            '            Call _PDFViewer1(_FilePath)
    '            '            Call PDFBinary()
    '            '            Call savePDF1()
    '            '        ElseIf _Tabtype2 = "" Then
    '            '            Call _PDFViewer2(_FilePath)
    '            '            Call PDFBinary()
    '            '            Call savePDF2()
    '            '        ElseIf _Tabtype3 = "" Then
    '            '            Call _PDFViewer3(_FilePath)
    '            '            Call PDFBinary()
    '            '            Call savePDF3()
    '            '        Else
    '            '            Call _PDFViewer4(_FilePath)
    '            '            Call PDFBinary()
    '            '            Call savePDF4()
    '            '        End If
    '            Select Case _FileType.ToUpper
    '                Case ".PDF".ToUpper
    '                    Call _PDFViewer(_FilePath)
    '                    Call PDFBinary()
    '                    Call savePDF()

    '                Case Else
    '        HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
    '        Exit Sub
    '            End Select
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub Readfile1()
    '    Try
    '        If _FilePath <> "" Then
    '            Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
    '            _FileType = System.IO.Path.GetExtension(_FilePath)
    '            _FileName = System.IO.Path.GetFileName(_FilePath)
    '            Select Case _FileType.ToUpper
    '                Case ".PDF".ToUpper
    '                    Call _PDFViewer1(_FilePath)
    '                    Call PDFBinary()
    '                    Call savePDF1()
    '                Case Else
    '                    HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
    '                    Exit Sub
    '            End Select
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub Readfile2()
    '    Try
    '        If _FilePath <> "" Then
    '            Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
    '            _FileType = System.IO.Path.GetExtension(_FilePath)
    '            _FileName = System.IO.Path.GetFileName(_FilePath)
    '            Select Case _FileType.ToUpper
    '                Case ".PDF".ToUpper
    '                    Call _PDFViewer2(_FilePath)
    '                    Call PDFBinary()
    '                    Call savePDF2()
    '                Case Else
    '                    HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
    '                    Exit Sub
    '            End Select
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub Readfile3()
    '    Try
    '        If _FilePath <> "" Then
    '            Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
    '            _FileType = System.IO.Path.GetExtension(_FilePath)
    '            _FileName = System.IO.Path.GetFileName(_FilePath)
    '            Select Case _FileType.ToUpper
    '                Case ".PDF".ToUpper
    '                    Call _PDFViewer3(_FilePath)
    '                    Call PDFBinary()
    '                    Call savePDF3()
    '                Case Else
    '                    HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
    '                    Exit Sub
    '            End Select
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub Readfile4()
    '    Try
    '        If _FilePath <> "" Then
    '            Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
    '            _FileType = System.IO.Path.GetExtension(_FilePath)
    '            _FileName = System.IO.Path.GetFileName(_FilePath)
    '            Select Case _FileType.ToUpper
    '                Case ".PDF".ToUpper
    '                    Call _PDFViewer4(_FilePath)
    '                    Call PDFBinary()
    '                    Call savePDF4()
    '                Case Else
    '                    HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
    '                    Exit Sub
    '            End Select
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub _PDFViewer(_FileName As String)
    '    Try
    '        'Me.FTFileRef.Controls.Clear()
    '        Dim _Pdfv As New PdfViewer
    '        _Pdfv.Dock = DockStyle.Fill
    '        _Pdfv.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '        _Pdfv.LoadDocument(_FileName)
    '        'Me.oGrpdetail.Controls.Add(SimpleButton1)
    '        Me.FTFileRef.Controls.Add(_Pdfv)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub _PDFViewer1(_FileName As String)
    '    Try
    '        Me.FTFileReff.Controls.Clear()
    '        Dim _Pdfv As New PdfViewer
    '        _Pdfv.Dock = DockStyle.Fill
    '        _Pdfv.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '        _Pdfv.LoadDocument(_FileName)
    '        'Me.oGrpdetail.Controls.Add(SimpleButton1)
    '        Me.FTFileReff.Controls.Add(_Pdfv)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub _PDFViewer2(_FileName As String)
    '    Try
    '        Me.FTFileRefff.Controls.Clear()
    '        Dim _Pdfv As New PdfViewer
    '        _Pdfv.Dock = DockStyle.Fill
    '        _Pdfv.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '        _Pdfv.LoadDocument(_FileName)
    '        'Me.oGrpdetail.Controls.Add(SimpleButton1)
    '        Me.FTFileRefff.Controls.Add(_Pdfv)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub _PDFViewer3(_FileName As String)
    '    Try
    '        Me.FTFileReffff.Controls.Clear()
    '        Dim _Pdfv As New PdfViewer
    '        _Pdfv.Dock = DockStyle.Fill
    '        _Pdfv.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '        _Pdfv.LoadDocument(_FileName)
    '        'Me.oGrpdetail.Controls.Add(SimpleButton1)
    '        Me.FTFileReffff.Controls.Add(_Pdfv)
    '    Catch ex As Exception
    '    End Try

    'End Sub
    'Private Sub _PDFViewer4(_FileName As String)
    '    Try
    '        Me.FTFileRefffff.Controls.Clear()
    '        Dim _Pdfv As New PdfViewer
    '        _Pdfv.Dock = DockStyle.Fill
    '        _Pdfv.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '        _Pdfv.LoadDocument(_FileName)
    '        'Me.oGrpdetail.Controls.Add(SimpleButton1)
    '        Me.FTFileRefffff.Controls.Add(_Pdfv)
    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Sub pdf_clear()
    '    Me.XtraTabPage1.Controls.Clear()
    '    Me.XtraTabPage2.Controls.Clear()
    '    Me.XtraTabPage3.Controls.Clear()
    '    Me.XtraTabPage4.Controls.Clear()
    '    Me.XtraTabPage5.Controls.Clear()

    '    Me.FTFileRef.Controls.Clear()
    '    Me.FTFileReff.Controls.Clear()
    '    Me.FTFileRefff.Controls.Clear()
    '    Me.FTFileReffff.Controls.Clear()
    '    Me.FTFileRefffff.Controls.Clear()
    '    data = Nothing
    '    _Pdfdata = Nothing
    '    _FilePath = Nothing
    'End Sub
    'Private Sub Load_PDF()
    '    Call pdf_clear()

    '    Dim _cmd As String = ""
    '    _cmd = "SELECT FTFileRef,FTFileReff,FTFileRefff,FTFileReffff,FTFileRefffff,isnull(FBFile,'')AS FBFile FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK)"
    '    _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
    '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
    '    For Each R As DataRow In _oDt.Rows
    '        Dim FBFile As String = R!FBFile
    '        Me.XtraTabPage1.Controls.Clear()
    '        Select Case FBFile
    '            Case "PDF"
    '                'Dim _Pdfv As New PdfViewer
    '                'With _Pdfv
    '                '    .Dock = DockStyle.Fill
    '                '    _Pdfdata = CType(R!FTFileRef, Byte())
    '                '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                '    .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                'End With

    '                'Me.oGrpdetail.Controls.Add(SimpleButton1)
    '                'Me.XtraTabPage1.Controls.Add(_Pdfv)

    '                Select Case otab.SelectedTabPage.Name
    '                    Case Is = "XtraTabPage1"
    '                        Dim _Pdfv As New PdfViewer
    '                        With _Pdfv
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRef, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                        End With

    '                        Me.XtraTabPage1.Controls.Add(_Pdfv)
    '                    Case Is = "XtraTabPage2"
    '                        Dim _Pdfv As New PdfViewer
    '                        With _Pdfv
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRef, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                        End With

    '                        Dim _Pdfv1 As New PdfViewer

    '                        With _Pdfv1
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileReff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                        End With
    '                        Me.XtraTabPage1.Controls.Add(_Pdfv)
    '                        Me.XtraTabPage2.Controls.Add(_Pdfv1)

    '                    Case Is = "XtraTabPage3"

    '                        Dim _Pdfv As New PdfViewer
    '                        With _Pdfv
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRef, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                        End With

    '                        Dim _Pdfv1 As New PdfViewer

    '                        With _Pdfv1
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileReff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                        End With

    '                        Dim _Pdfv2 As New PdfViewer

    '                        With _Pdfv2
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRefff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                        End With
    '                        Me.XtraTabPage1.Controls.Add(_Pdfv)
    '                        Me.XtraTabPage2.Controls.Add(_Pdfv1)
    '                        Me.XtraTabPage3.Controls.Add(_Pdfv2)
    '                    Case Is = "XtraTabPage4"

    '                        Dim _Pdfv As New PdfViewer
    '                        With _Pdfv
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRef, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                        End With

    '                        Dim _Pdfv1 As New PdfViewer

    '                        With _Pdfv1
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileReff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                        End With

    '                        Dim _Pdfv2 As New PdfViewer

    '                        With _Pdfv2
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRefff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                        End With

    '                        Dim _Pdfv3 As New PdfViewer


    '                        With _Pdfv3
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileReffff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileReffff, Byte())))
    '                        End With
    '                        Me.XtraTabPage1.Controls.Add(_Pdfv)
    '                        Me.XtraTabPage2.Controls.Add(_Pdfv1)
    '                        Me.XtraTabPage3.Controls.Add(_Pdfv2)
    '                        Me.XtraTabPage4.Controls.Add(_Pdfv3)
    '                    Case Is = "XtraTabPage5"
    '                        Dim _Pdfv As New PdfViewer
    '                        With _Pdfv
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRef, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                        End With

    '                        Dim _Pdfv1 As New PdfViewer

    '                        With _Pdfv1
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileReff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                        End With

    '                        Dim _Pdfv2 As New PdfViewer

    '                        With _Pdfv2
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRefff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                        End With

    '                        Dim _Pdfv3 As New PdfViewer


    '                        With _Pdfv3
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileReffff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileReffff, Byte())))
    '                        End With

    '                        Dim _Pdfv4 As New PdfViewer


    '                        With _Pdfv4
    '                            .Dock = DockStyle.Fill
    '                            _Pdfdata = CType(R!FTFileRefffff, Byte())
    '                            .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                            .LoadDocument(New MemoryStream(CType(R!FTFileRefffff, Byte())))
    '                        End With
    '                        Me.XtraTabPage1.Controls.Add(_Pdfv)
    '                        Me.XtraTabPage2.Controls.Add(_Pdfv1)
    '                        Me.XtraTabPage3.Controls.Add(_Pdfv2)
    '                        Me.XtraTabPage4.Controls.Add(_Pdfv3)
    '                        Me.XtraTabPage5.Controls.Add(_Pdfv4)
    '                End Select
    '        End Select
    '        '   Call Load_PDF1()
    '    Next
    'End Sub
    'Private Sub Load_PDF1()
    '    Call pdf_clear()

    '    Dim _cmd As String = ""
    '    _cmd = "SELECT FTFileRef,FTFileReff,FTFileRefff,FTFileReffff,FTFileRefffff,isnull(FBFile,'')AS FBFile FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK)"
    '    _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
    '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
    '    For Each R As DataRow In _oDt.Rows
    '        Dim FBFile As String = R!FBFile
    '        Me.XtraTabPage1.Controls.Clear()
    '        Select Case FBFile
    '            Case "PDF"
    '                Dim _Pdfv As New PdfViewer
    '                Dim _Pdfv1 As New PdfViewer
    '                Dim _Pdfv2 As New PdfViewer
    '                Dim _Pdfv3 As New PdfViewer
    '                Dim _Pdfv4 As New PdfViewer

    '                If R!FTFileRef.ToString <> "" Then
    '                    With _Pdfv
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileRef, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                    End With
    '                End If

    '                If R!FTFileReff.ToString <> "" Then
    '                    'With _Pdfv
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileRef, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                    'End With
    '                    With _Pdfv1
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileReff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                    End With
    '                End If

    '                If R!FTFileRefff.ToString <> "" Then
    '                    'With _Pdfv
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileRef, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                    'End With
    '                    'With _Pdfv1
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileReff, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                    'End With
    '                    With _Pdfv2
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileRefff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                    End With
    '                End If

    '                If R!FTFileReffff.ToString <> "" Then
    '                    'With _Pdfv
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileRef, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                    'End With
    '                    'With _Pdfv1
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileReff, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                    'End With
    '                    'With _Pdfv2
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileRefff, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                    'End With
    '                    With _Pdfv3
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileReffff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileReffff, Byte())))
    '                    End With
    '                End If

    '                If R!FTFileRefffff.ToString <> "" Then
    '                    'With _Pdfv
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileRef, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileRef, Byte())))
    '                    'End With
    '                    'With _Pdfv1
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileReff, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                    'End With
    '                    'With _Pdfv2
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileRefff, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                    'End With
    '                    'With _Pdfv3
    '                    '    .Dock = DockStyle.Fill
    '                    '    _Pdfdata = CType(R!FTFileReffff, Byte())
    '                    '    .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                    '    .LoadDocument(New MemoryStream(CType(R!FTFileReffff, Byte())))
    '                    'End With
    '                    With _Pdfv4
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileRefffff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileRefffff, Byte())))
    '                    End With
    '                End If


    '                Me.XtraTabPage1.Controls.Add(_Pdfv)
    '                Me.XtraTabPage2.Controls.Add(_Pdfv1)
    '                Me.XtraTabPage3.Controls.Add(_Pdfv2)
    '                Me.XtraTabPage4.Controls.Add(_Pdfv3)
    '                Me.XtraTabPage5.Controls.Add(_Pdfv4)
    '        End Select


    '    Next
    'End Sub

    'Private Sub FTFileRef_DoubleClick(sender As Object, e As EventArgs)
    '    Me.XtraTabPage1.Controls.Clear()
    '    Me.FTFileRef.Controls.Clear()
    'End Sub
    ' -----อันล่างไม่ได้ใช้-----
    'Private Sub Load_PDF1()
    '    Call pdf_clear()

    '    Dim _cmd As String = ""
    '    _cmd = "SELECT FTFileReff,isnull(FBFile,'')AS FBFile FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK)"
    '    _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
    '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
    '    For Each R As DataRow In _oDt.Rows
    '        Dim FBFile As String = R!FBFile
    '        Me.XtraTabPage2.Controls.Clear()
    '        If R!FTFileReff.ToString <> "" Then
    '            Select Case FBFile
    '                Case "PDF"

    '                    Dim _Pdfv1 As New PdfViewer

    '                    With _Pdfv1
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileReff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileReff, Byte())))
    '                    End With

    '                    Me.XtraTabPage2.Controls.Add(_Pdfv1)

    '            End Select
    '        End If

    '        '   Call Load_PDF2()


    '    Next
    'End Sub
    'Private Sub Load_PDF2()
    '    Call pdf_clear()

    '    Dim _cmd As String = ""
    '    _cmd = "SELECT FTFileRefff,isnull(FBFile,'')AS FBFile FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK)"
    '    _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
    '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
    '    For Each R As DataRow In _oDt.Rows
    '        Dim FBFile As String = R!FBFile
    '        Me.XtraTabPage3.Controls.Clear()
    '        If R!FTFileRefff.ToString <> "" Then
    '            Select Case FBFile
    '                Case "PDF"
    '                    Dim _Pdfv2 As New PdfViewer

    '                    With _Pdfv2
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileRefff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileRefff, Byte())))
    '                    End With

    '                    Me.XtraTabPage3.Controls.Add(_Pdfv2)

    '            End Select
    '        End If
    '        ' Call Load_PDF3()
    '    Next
    'End Sub
    'Private Sub Load_PDF3()
    '    Call pdf_clear()

    '    Dim _cmd As String = ""
    '    _cmd = "SELECT FTFileReffff,isnull(FBFile,'')AS FBFile FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK)"
    '    _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
    '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
    '    For Each R As DataRow In _oDt.Rows
    '        Dim FBFile As String = R!FBFile
    '        Me.XtraTabPage4.Controls.Clear()
    '        If R!FTFileReffff.ToString <> "" Then
    '            Select Case FBFile
    '                Case "PDF"
    '                    Dim _Pdfv3 As New PdfViewer


    '                    With _Pdfv3
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileReffff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileReffff, Byte())))
    '                    End With

    '                    Me.XtraTabPage4.Controls.Add(_Pdfv3)

    '            End Select
    '        End If
    '        ' Call Load_PDF4()
    '    Next
    'End Sub
    'Private Sub Load_PDF4()
    '    Call pdf_clear()

    '    Dim _cmd As String = ""
    '    _cmd = "SELECT FTFileRefffff,isnull(FBFile,'')AS FBFile FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee WITH(NOLOCK)"
    '    _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
    '    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
    '    For Each R As DataRow In _oDt.Rows
    '        Dim FBFile As String = R!FBFile
    '        Me.XtraTabPage4.Controls.Clear()
    '        If R!FTFileRefffff.ToString <> "" Then
    '            Select Case FBFile
    '                Case "PDF"
    '                    Dim _Pdfv4 As New PdfViewer


    '                    With _Pdfv4
    '                        .Dock = DockStyle.Fill
    '                        _Pdfdata = CType(R!FTFileRefffff, Byte())
    '                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
    '                        .LoadDocument(New MemoryStream(CType(R!FTFileRefffff, Byte())))
    '                    End With

    '                    Me.XtraTabPage5.Controls.Add(_Pdfv4)

    '            End Select
    '        End If
    '    Next
    'End Sub

    'Private Sub ogvdetail_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvdetail.KeyDown
    '    Try
    '        Select Case e.KeyCode
    '            Case Keys.Enter, Keys.Down
    '                With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '                    If .FocusedColumn.FieldName.ToString <> "FBImage" Then
    '                        Exit Sub
    '                    End If
    '                    Dim x As Integer = 0
    '                    If .GetRowCellValue(.FocusedRowHandle, "FTPassPostNo").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FDDateofIssue").ToString <> "" Or _
    '                         .GetRowCellValue(.FocusedRowHandle, "FDDateofExpiry").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FTPassPostNote").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FBImage").ToString <> "" Then
    '                        With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
    '                            .AcceptChanges()
    '                            If .Select("FTPassPostNo='' or FTPassPostNo Is null").Length <= 0 Then
    '                                x = .Rows.Count + 1
    '                                .Rows.Add(x)
    '                                .Rows(x - 1).Item("FNPassPostSeq") = x
    '                            End If
    '                            .AcceptChanges()
    '                        End With
    '                        .FocusedRowHandle = x
    '                        .FocusedColumn = .Columns.ColumnByFieldName("FTPassPostNo")
    '                    End If
    '                End With
    '            Case Keys.Delete
    '                With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
    '                    .DeleteRow(.FocusedRowHandle)
    '                    With CType(ogcdetail.DataSource, DataTable)
    '                        .AcceptChanges()
    '                        Dim x As Integer = 0
    '                        For Each r As DataRow In .Select("FNPassPostSeq<>0", "FNPassPostSeq")
    '                            x += +1
    '                            r!FNPassPostSeq = x

    '                        Next
    '                        .AcceptChanges()
    '                    End With
    '                End With

    '        End Select
    '    Catch ex As Exception
    '    End Try
    'End Sub
#Region "PassPort"
    Private Sub LoadPass()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT   P.FNPassportSeq,P.FTPassPortNo,P.FTPassportNote,P.FBFileImage,E.FTEmpCode"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(P.FDDateofIssue)=1 THEN SUBSTRING(P.FDDateofIssue,9,2)+'/'+SUBSTRING(P.FDDateofIssue,6,2)+'/'+SUBSTRING(P.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue1"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(P.FDDateofExpiry)=1 THEN SUBSTRING(P.FDDateofExpiry,9,2)+'/'+SUBSTRING(P.FDDateofExpiry,6,2)+'/'+SUBSTRING(P.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
            _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport AS P WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON P.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "
            _Qry &= vbCrLf & " ORDER BY P.FNPassportSeq desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)


            'If _oDt.Rows.Count = 0 Then
            '    With _oDt
            '        .Rows.Add()
            '        .AcceptChanges()
            '        For Each R As DataRow In .Rows
            '            Dim _ss As String = ""
            '            R!FNPassportSeq = 1
            '            '_ss = R!FNHSysSeasonId.ToString
            '            'Me.FNHSysSeasonId.Text = _ss
            '        Next
            '    End With

            'End If
            Me.ogcdetail.DataSource = _oDt
            Call LoadPP()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddEmpPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpPass.Click
        If VerrifyPass() = False Then Exit Sub
        If Me.VerrifyData Then
            If HI.UL.ULDate.ConvertEnDB(FDDateofExpiry2.Text) > HI.UL.ULDate.ConvertEnDB(FDDateofIssue2.Text) Then
                If Me.SaveData() Then

                    Dim _Qry As String = ""

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport SET "
                    _Qry &= vbCrLf & ", FDDateofIssue=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssue2.Text) & "' "
                    _Qry &= vbCrLf & ", FDDateofExpiry=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiry2.Text) & "' "
                    _Qry &= vbCrLf & ", FTPassportNote=N'" & HI.UL.ULF.rpQuoted(FTPassportNote1.Text) & "' "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " AND  FTPassPortNo='" & HI.UL.ULF.rpQuoted(FTPassPortNo.Text) & "'"
                    _Qry &= vbCrLf & "  AND FNPassportSeq=" & Me.FNPassportSeq.Value

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                        _Qry = "SELECT MAX(FNPassportSeq) AS FNPassportSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport(FNHSysEmpID,FNPassportSeq, FTPassPortNo, FDDateofIssue, FDDateofExpiry, FTPassportNote "
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTPassPortNo.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssue2.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiry2.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTPassportNote1.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    End If


                    _Qry = "UPDATE THRMEmployee_Passport SET FNPassportSeq=FNNo"
                    _Qry &= vbCrLf & " FROM THRMEmployee_Passport INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNPassportSeq) AS FNNo, FNPassportSeq,FNHSysEmpID"
                    _Qry &= vbCrLf & " FROM THRMEmployee_Passport WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMEmployee_Passport.FNPassportSeq=T1.FNPassportSeq AND THRMEmployee_Passport.FNHSysEmpID=T1.FNHSysEmpID"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    HI.TL.HandlerControl.ClearControl(ogbPass)
                    Call LoadPass()

                End If

            Else
                HI.MG.ShowMsg.mProcessError(20181026001, "วันที่หมดอายุ น้อยกว่าวันที่เริ่มต้น กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                FDDateofExpiry2.Focus()
            End If
        End If
    End Sub
    Private Function SavePassImage() As Boolean
        Dim dt As DataTable
        Dim _dataBinary As Byte()

        With CType(ogcdetail.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        For Each R As DataRow In dt.Select("FTPassPortNo <> ''  ", "FNPassportSeq")

            If R!FBFileImage.ToString <> "" Then
                _dataBinary = R!FBFileImage
                If Not (_dataBinary Is Nothing) Then
                    Dim _cmd As String = ""
                    _cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport"
                    _cmd &= " Set  FBFileImage=@FBFileImage"
                    _cmd &= " WHERE FNHSysEmpID=@FNHSysEmpID"
                    _cmd &= "  AND FNPassportSeq=@FNPassportSeq"
                    Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    cmd.Parameters.AddWithValue("@FNHSysEmpID", Val(FNHSysEmpID.Properties.Tag.ToString))
                    cmd.Parameters.AddWithValue("@FNPassportSeq", R!FNPassportSeq.ToString)

                    Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                    For Each Obj As Object In Me.Controls.Find("FBFileImage", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                _dataBinary = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next


                    Dim _p As New SqlParameter("@FBFileImage", SqlDbType.Image)
                    _p.Value = _dataBinary
                    cmd.Parameters.Add(_p)
                    cmd.ExecuteNonQuery()
                End If
            End If

        Next

    End Function

    Private Sub ocmremoveEmpPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpPass.Click
        If FNPassportSeq.Value <= 0 Then Exit Sub


        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNPassportSeq=" & Me.FNPassportSeq.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployee_Passport SET FNPassportSeq=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployee_Passport INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNPassportSeq) AS FNNo, FNPassportSeq,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployee_Passport WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployee_Passport.FNPassportSeq=T1.FNPassportSeq AND THRMEmployee_Passport.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbPass)
        Call LoadPass()

    End Sub
    Private Sub ogvdetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvdetail.Click
        With ogvdetail

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNPassportSeq.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNPassportSeq").ToString)
            FTPassPortNo.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPassPortNo").ToString
            FTPassportNote1.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTPassportNote").ToString


            Try
                FDDateofIssue2.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofIssue1").ToString)
            Catch ex As Exception
                FDDateofIssue2.DateTime = Nothing
            End Try

            Try
                FDDateofExpiry2.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofExpiry").ToString)
            Catch ex As Exception
                FDDateofExpiry2.DateTime = Nothing
            End Try

            FTPassPortNo.Focus()

        End With
    End Sub
    Private Sub ocmclearPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearPass.Click
        HI.TL.HandlerControl.ClearControl(ogbPass)
    End Sub

    Private Sub LoadPP()
        Dim _Qry As String = ""
        Dim dt As DataTable
        Dim _pno As String = ""

        With CType(ogcdetail.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FNPassportSeq >= 0 ", "FNPassportSeq desc ")

                FTPassPostNo.Text = R!FTPassPortNo.ToString
                FDDateofIssue.Text = R!FDDateofIssue1.ToString
                FDDateofExpiry.Text = R!FDDateofExpiry.ToString
                Exit For
            Next
        End With

        With CType(ogcvisa.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FNVisaSeq >= 0 ", "FNVisaSeq desc")

                FTVisasNo.Text = R!FTVisaNo.ToString
                FDDateVisas.Text = R!FDDateofIssue.ToString
                FDDateVisasExpiry.Text = R!FDDateofExpiry.ToString
                Exit For
            Next
        End With

        With CType(ogcWork.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FNWorkpermitSeq >= 0 ", "FNWorkpermitSeq desc")

                FTWorkPermitNo.Text = R!FTWorkpermitNo.ToString
                FDDateValid.Text = R!FDDateofIssue.ToString
                FDDateUntil.Text = R!FDDateofExpiry.ToString
                Exit For
            Next
        End With

        With CType(ogcMOU.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FNMOUSeq >= 0 ", "FNMOUSeq desc")

                FTMOUDoccument.Text = R!FTMOUNo.ToString
                FDDateMOU.Text = R!FDDateofIssue.ToString
                FDDateMOUex.Text = R!FDDateofExpiry.ToString
                Exit For
            Next
        End With

        '_Qry = "SELECT   P.FTPassPortNo"
        '_Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Passport AS P LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E ON P.FNHSysEmpID=E.FNHSysEmpID "
        '_Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "

        'FTPassPostNo.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR)


    End Sub

    Private Function VerrifyPass() As Boolean
        Dim _Pass As Boolean = False
        If FTPassPortNo.Text <> "" Then
            If FDDateofIssue2.Text <> "" Then
                If FDDateofExpiry2.Text <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofExpiry2_lbl.Text)
                    FDDateofExpiry2.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofIssue2_lbl.Text)
                FDDateofIssue2.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPassPortNo_lbl.Text)
            FTPassPortNo.Focus()
        End If

        Return _Pass
    End Function
#End Region

#Region "Visa"
    Private Sub LoadVisa()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT   V.FNVisaSeq,V.FTVisaNo,V.FTVisatNote,V.FBFileImage,E.FTEmpCode"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(V.FDDateofIssue)=1 THEN SUBSTRING(V.FDDateofIssue,9,2)+'/'+SUBSTRING(V.FDDateofIssue,6,2)+'/'+SUBSTRING(V.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
            _Qry &= vbCrLf & " , CASE WHEN ISDATE(V.FDDateofExpiry)=1 THEN SUBSTRING(V.FDDateofExpiry,9,2)+'/'+SUBSTRING(V.FDDateofExpiry,6,2)+'/'+SUBSTRING(V.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
            _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa AS V WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON V.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "
            _Qry &= vbCrLf & " ORDER BY V.FNVisaSeq desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            Me.ogcvisa.DataSource = _oDt
            Call LoadPP()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddEmpVisa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpVisa.Click
        If VerrifyVisa() = False Then Exit Sub
        If Me.VerrifyData Then
            If HI.UL.ULDate.ConvertEnDB(FDDateofExpiryV1.Text) > HI.UL.ULDate.ConvertEnDB(FDDateofIssueV1.Text) Then
                If Me.SaveData() Then

                    Dim _Qry As String = ""

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa SET "
                    _Qry &= vbCrLf & ", FDDateofIssue=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueV1.Text) & "' "
                    _Qry &= vbCrLf & ", FDDateofExpiry=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryV1.Text) & "' "
                    _Qry &= vbCrLf & ", FTVisatNote=N'" & HI.UL.ULF.rpQuoted(FTVisatNoteV.Text) & "' "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " AND  FTVisaNo='" & HI.UL.ULF.rpQuoted(FTVisaNo1.Text) & "'"
                    _Qry &= vbCrLf & "  AND FNVisaSeq=" & Me.FNVisaSeq1.Value

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                        _Qry = "SELECT MAX(FNVisaSeq) AS FNVisaSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa(FNHSysEmpID,FNVisaSeq, FTVisaNo, FDDateofIssue, FDDateofExpiry, FTVisatNote "
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTVisaNo1.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueV1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryV1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTVisatNoteV.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    End If


                    _Qry = "UPDATE THRMEmployee_Visa SET FNVisaSeq=FNNo"
                    _Qry &= vbCrLf & " FROM THRMEmployee_Visa INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNVisaSeq) AS FNNo, FNVisaSeq,FNHSysEmpID"
                    _Qry &= vbCrLf & " FROM THRMEmployee_Visa WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMEmployee_Visa.FNVisaSeq=T1.FNPassportSeq AND THRMEmployee_Visa.FNHSysEmpID=T1.FNHSysEmpID"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    HI.TL.HandlerControl.ClearControl(ogbVisa)
                    Call LoadVisa()

                End If
            Else
                HI.MG.ShowMsg.mProcessError(20181026001, "วันที่หมดอายุ น้อยกว่าวันที่เริ่มต้น กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                FDDateofExpiryV1.Focus()
            End If
        End If
    End Sub
    Private Function SaveVisaImage() As Boolean
        Dim dt As DataTable
        Dim _dataBinary As Byte()

        With CType(ogcvisa.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        For Each R As DataRow In dt.Select("FTVisaNo <> ''  ", "FNVisaSeq")

            If R!FBFileImage.ToString <> "" Then
                _dataBinary = R!FBFileImage
                If Not (_dataBinary Is Nothing) Then
                    Dim _cmd As String = ""
                    _cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa"
                    _cmd &= " Set  FBFileImage=@FBFileImage"
                    _cmd &= " WHERE FNHSysEmpID=@FNHSysEmpID"
                    _cmd &= "  AND FNVisaSeq=@FNVisaSeq"
                    Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    cmd.Parameters.AddWithValue("@FNHSysEmpID", Val(FNHSysEmpID.Properties.Tag.ToString))
                    cmd.Parameters.AddWithValue("@FNVisaSeq", R!FNVisaSeq.ToString)

                    Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                    For Each Obj As Object In Me.Controls.Find("FBFileImage", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                _dataBinary = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next


                    Dim _p As New SqlParameter("@FBFileImage", SqlDbType.Image)
                    _p.Value = _dataBinary
                    cmd.Parameters.Add(_p)
                    cmd.ExecuteNonQuery()
                End If
            End If

        Next

    End Function

    Private Sub ocmremoveEmpVisa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpVisa.Click
        If FNVisaSeq1.Value <= 0 Then Exit Sub


        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Visa  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNVisaSeq=" & Me.FNVisaSeq1.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployee_Visa SET FNVisaSeq=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployee_Visa INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNVisaSeq) AS FNNo, FNVisaSeq,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployee_Visa WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployee_Visa.FNVisaSeq=T1.FNVisaSeq AND THRMEmployee_Visa.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbVisa)
        Call LoadVisa()

    End Sub
    Private Sub ogvvisa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvvisa.Click
        With ogvvisa

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNVisaSeq1.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNVisaSeq").ToString)
            FTVisaNo1.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTVisaNo").ToString
            FTVisatNoteV.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTVisatNote").ToString


            Try
                FDDateofIssueV1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofIssue").ToString)
            Catch ex As Exception
                FDDateofIssueV1.DateTime = Nothing
            End Try

            Try
                FDDateofExpiryV1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofExpiry").ToString)
            Catch ex As Exception
                FDDateofExpiryV1.DateTime = Nothing
            End Try

            FTVisaNo1.Focus()

        End With
    End Sub
    Private Sub ocmclearVisa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearVisa.Click
        HI.TL.HandlerControl.ClearControl(ogbVisa)
    End Sub

    Private Function VerrifyVisa() As Boolean
        Dim _Pass As Boolean = False
        If FTVisaNo1.Text <> "" Then
            If FDDateofIssueV1.Text <> "" Then
                If FDDateofExpiryV1.Text <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofExpiryV_lbl.Text)
                    FDDateofExpiryV1.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofIssueV_lbl.Text)
                FDDateofIssueV1.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTVisaNo_lbl.Text)
            FTVisaNo1.Focus()
        End If

        Return _Pass
    End Function

#End Region

#Region "Workpermit"
    Private Sub LoadWorkpermit()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT   W.FNWorkpermitSeq,W.FTWorkpermitNo,W.FTWorkpermitNote,W.FBFileImage,E.FTEmpCode"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(W.FDDateofIssue)=1 THEN SUBSTRING(W.FDDateofIssue,9,2)+'/'+SUBSTRING(W.FDDateofIssue,6,2)+'/'+SUBSTRING(W.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
            _Qry &= vbCrLf & " , CASE WHEN ISDATE(W.FDDateofExpiry)=1 THEN SUBSTRING(W.FDDateofExpiry,9,2)+'/'+SUBSTRING(W.FDDateofExpiry,6,2)+'/'+SUBSTRING(W.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
            _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit AS W WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON W.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "
            _Qry &= vbCrLf & " ORDER BY W.FNWorkpermitSeq desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            Me.ogcWork.DataSource = _oDt
            Call LoadPP()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddEmpWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpWork.Click
        If Verrifywork() = False Then Exit Sub
        If Me.VerrifyData Then

            If HI.UL.ULDate.ConvertEnDB(FDDateofExpiryW1.Text) > HI.UL.ULDate.ConvertEnDB(FDDateofIssueW1.Text) Then
                If Me.SaveData() Then

                    Dim _Qry As String = ""

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit SET "
                    _Qry &= vbCrLf & ", FDDateofIssue=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueW1.Text) & "' "
                    _Qry &= vbCrLf & ", FDDateofExpiry=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryW1.Text) & "' "
                    _Qry &= vbCrLf & ", FTWorkpermitNote=N'" & HI.UL.ULF.rpQuoted(FTWorkpermitNoteW.Text) & "' "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " AND  FTWorkpermitNo='" & HI.UL.ULF.rpQuoted(FTWorkpermitNoW.Text) & "'"
                    _Qry &= vbCrLf & "  AND FNWorkpermitSeq=" & Me.FNWorkpermitSeqW.Value

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                        _Qry = "SELECT MAX(FNWorkpermitSeq) AS FNWorkpermitSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit(FNHSysEmpID,FNWorkpermitSeq, FTWorkpermitNo, FDDateofIssue, FDDateofExpiry, FTWorkpermitNote "
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTWorkpermitNoW.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueW1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryW1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTWorkpermitNoteW.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    End If


                    _Qry = "UPDATE THRMEmployee_Workpermit SET FNWorkpermitSeq=FNNo"
                    _Qry &= vbCrLf & " FROM THRMEmployee_Workpermit INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNWorkpermitSeq) AS FNNo, FNWorkpermitSeq,FNHSysEmpID"
                    _Qry &= vbCrLf & " FROM THRMEmployee_Workpermit WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMEmployee_Workpermit.FNWorkpermitSeq=T1.FNWorkpermitSeq AND THRMEmployee_Workpermit.FNHSysEmpID=T1.FNHSysEmpID"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    HI.TL.HandlerControl.ClearControl(ogbWordper)
                    Call LoadWorkpermit()

                End If
            Else
                HI.MG.ShowMsg.mProcessError(20181026001, "วันที่หมดอายุ น้อยกว่าวันที่เริ่มต้น กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                FDDateofExpiryW1.Focus()

            End If
        End If
    End Sub
    Private Function SaveWorkImage() As Boolean
        Dim dt As DataTable
        Dim _dataBinary As Byte()

        With CType(ogcWork.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        For Each R As DataRow In dt.Select("FTWorkpermitNo <> ''  ", "FNWorkpermitSeq")

            If R!FBFileImage.ToString <> "" Then
                _dataBinary = R!FBFileImage
                If Not (_dataBinary Is Nothing) Then
                    Dim _cmd As String = ""
                    _cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit"
                    _cmd &= " Set  FBFileImage=@FBFileImage"
                    _cmd &= " WHERE FNHSysEmpID=@FNHSysEmpID"
                    _cmd &= "  AND FNWorkpermitSeq=@FNWorkpermitSeq"
                    Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    cmd.Parameters.AddWithValue("@FNHSysEmpID", Val(FNHSysEmpID.Properties.Tag.ToString))
                    cmd.Parameters.AddWithValue("@FNWorkpermitSeq", R!FNWorkpermitSeq.ToString)

                    Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                    For Each Obj As Object In Me.Controls.Find("FBFileImage", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                _dataBinary = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next


                    Dim _p As New SqlParameter("@FBFileImage", SqlDbType.Image)
                    _p.Value = _dataBinary
                    cmd.Parameters.Add(_p)
                    cmd.ExecuteNonQuery()
                End If
            End If

        Next

    End Function

    Private Sub ocmremoveEmpWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpWork.Click
        If FNWorkpermitSeqW.Value <= 0 Then Exit Sub


        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Workpermit  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNWorkpermitSeq=" & Me.FNWorkpermitSeqW.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployee_Workpermit SET FNWorkpermitSeq=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployee_Workpermit INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNWorkpermitSeq) AS FNNo, FNWorkpermitSeq,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployee_Workpermit WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployee_Workpermit.FNWorkpermitSeq=T1.FNWorkpermitSeq AND THRMEmployee_Workpermit.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbWordper)
        Call LoadWorkpermit()

    End Sub
    Private Sub ogvWork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvWork.Click
        With ogvWork

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNWorkpermitSeqW.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNWorkpermitSeq").ToString)
            FTWorkpermitNoW.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTWorkpermitNo").ToString
            FTWorkpermitNoteW.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTWorkpermitNote").ToString


            Try
                FDDateofIssueW1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofIssue").ToString)
            Catch ex As Exception
                FDDateofIssueW1.DateTime = Nothing
            End Try

            Try
                FDDateofExpiryW1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofExpiry").ToString)
            Catch ex As Exception
                FDDateofExpiryW1.DateTime = Nothing
            End Try

            FTWorkpermitNoW.Focus()

        End With
    End Sub
    Private Sub ocmclearWork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearWork.Click
        HI.TL.HandlerControl.ClearControl(ogbWordper)
    End Sub

    Private Function Verrifywork() As Boolean
        Dim _Pass As Boolean = False
        If FTWorkpermitNoW.Text <> "" Then
            If FDDateofIssueW1.Text <> "" Then
                If FDDateofExpiryW1.Text <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofExpiryW_lbl.Text)
                    FDDateofExpiryW1.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofIssueW_lbl.Text)
                FDDateofIssueW1.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTWorkpermitNoW_lbl.Text)
            FTWorkpermitNoW.Focus()
        End If

        Return _Pass
    End Function
#End Region

#Region "MOU"
    Private Sub LoadMOU()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT    M.FNMOUSeq,M.FTMOUNo,M.FTMOUNote,M.FBFileImage,E.FTEmpCode"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(M.FDDateofIssue)=1 THEN SUBSTRING(M.FDDateofIssue,9,2)+'/'+SUBSTRING(M.FDDateofIssue,6,2)+'/'+SUBSTRING(M.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
            _Qry &= vbCrLf & " , CASE WHEN ISDATE(M.FDDateofExpiry)=1 THEN SUBSTRING(M.FDDateofExpiry,9,2)+'/'+SUBSTRING(M.FDDateofExpiry,6,2)+'/'+SUBSTRING(M.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
            _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU AS M WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON M.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "
            _Qry &= vbCrLf & " ORDER BY M.FNMOUSeq desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            Me.ogcMOU.DataSource = _oDt
            Call LoadPP()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddEmpMOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpMOU.Click
        If VerrifyMOU() = False Then Exit Sub
        If Me.VerrifyData Then
            If HI.UL.ULDate.ConvertEnDB(FDDateofExpiryM1.Text) > HI.UL.ULDate.ConvertEnDB(FDDateofIssueM1.Text) Then
                If Me.SaveData() Then

                    Dim _Qry As String = ""

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU SET "
                    _Qry &= vbCrLf & ", FDDateofIssue=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueM1.Text) & "' "
                    _Qry &= vbCrLf & ", FDDateofExpiry=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryM1.Text) & "' "
                    _Qry &= vbCrLf & ", FTMOUNote=N'" & HI.UL.ULF.rpQuoted(FTMOUNoteM.Text) & "' "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " AND  FTMOUNo='" & HI.UL.ULF.rpQuoted(FTMOUNoM1.Text) & "'"
                    _Qry &= vbCrLf & "  AND FNMOUSeq=" & Me.FNMOUSeqM.Value

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                        _Qry = "SELECT MAX(FNMOUSeq) AS FNMOUSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU(FNHSysEmpID,FNMOUSeq, FTMOUNo, FDDateofIssue, FDDateofExpiry, FTMOUNote "
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTMOUNoM1.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueM1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryM1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTMOUNoteM.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    End If


                    _Qry = "UPDATE THRMEmployee_MOU SET FNMOUSeq=FNNo"
                    _Qry &= vbCrLf & " FROM THRMEmployee_MOU INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNMOUSeq) AS FNNo, FNMOUSeq,FNHSysEmpID"
                    _Qry &= vbCrLf & " FROM THRMEmployee_MOU WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMEmployee_MOU.FNMOUSeq=T1.FNMOUSeq AND THRMEmployee_MOU.FNHSysEmpID=T1.FNHSysEmpID"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    HI.TL.HandlerControl.ClearControl(ogbMOU)
                    Call LoadMOU()

                End If
            Else
                HI.MG.ShowMsg.mProcessError(20181026001, "วันที่หมดอายุ น้อยกว่าวันที่เริ่มต้น กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                FDDateofExpiryM1.Focus()
            End If
        End If

    End Sub
    Private Function SaveMOUImage() As Boolean
        Dim dt As DataTable
        Dim _dataBinary As Byte()

        With CType(ogcMOU.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        For Each R As DataRow In dt.Select("FTMOUNo <> ''  ", "FNMOUSeq")

            If R!FBFileImage.ToString <> "" Then
                _dataBinary = R!FBFileImage
                If Not (_dataBinary Is Nothing) Then
                    Dim _cmd As String = ""
                    _cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU"
                    _cmd &= " Set  FBFileImage=@FBFileImage"
                    _cmd &= " WHERE FNHSysEmpID=@FNHSysEmpID"
                    _cmd &= "  AND FNMOUSeq=@FNMOUSeq"
                    Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    cmd.Parameters.AddWithValue("@FNHSysEmpID", Val(FNHSysEmpID.Properties.Tag.ToString))
                    cmd.Parameters.AddWithValue("@FNMOUSeq", R!FNMOUSeq.ToString)

                    Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                    For Each Obj As Object In Me.Controls.Find("FBFileImage", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                _dataBinary = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next


                    Dim _p As New SqlParameter("@FBFileImage", SqlDbType.Image)
                    _p.Value = _dataBinary
                    cmd.Parameters.Add(_p)
                    cmd.ExecuteNonQuery()
                End If
            End If

        Next

    End Function

    Private Sub ocmremoveEmpMOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpMOU.Click
        If FNMOUSeqM.Value <= 0 Then Exit Sub


        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_MOU  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

        _Qry &= vbCrLf & "  AND FNMOUSeq=" & Me.FNMOUSeqM.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployee_MOU SET FNMOUSeq=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployee_MOU INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNMOUSeq) AS FNNo, FNMOUSeq,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployee_MOU WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployee_MOU.FNMOUSeq=T1.FNMOUSeq AND THRMEmployee_MOU.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbMOU)
        Call LoadMOU()

    End Sub
    Private Sub ogvMOU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvMOU.Click
        With ogvMOU

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNMOUSeqM.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNMOUSeq").ToString)
            FTMOUNoM1.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTMOUNo").ToString
            FTMOUNoteM.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTMOUNote").ToString


            Try
                FDDateofIssueM1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofIssue").ToString)
            Catch ex As Exception
                FDDateofIssueM1.DateTime = Nothing
            End Try

            Try
                FDDateofExpiryM1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofExpiry").ToString)
            Catch ex As Exception
                FDDateofExpiryM1.DateTime = Nothing
            End Try

            FTMOUNoM1.Focus()

        End With


    End Sub
    Private Sub ocmclearMOU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearMOU.Click
        HI.TL.HandlerControl.ClearControl(ogbMOU)
    End Sub


    Private Function VerrifyMOU() As Boolean
        Dim _Pass As Boolean = False
        If FTMOUNoM1.Text <> "" Then
            If FDDateofIssueM1.Text <> "" Then
                If FDDateofExpiryM1.Text <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofExpiryM_lbl.Text)
                    FDDateofExpiryM1.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofIssueM_lbl.Text)
                FDDateofIssueM1.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTMOUNo_lbl.Text)
            FTMOUNoM1.Focus()
        End If

        Return _Pass
    End Function
#End Region

#Region "Other"
    Private Sub LoadOther()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT   O.FNFileOtherSeq,O.FTFileOtherNo,O.FTFileOtherNote,O.FBFileImage,E.FTEmpCode"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(O.FDDateofIssue)=1 THEN SUBSTRING(O.FDDateofIssue,9,2)+'/'+SUBSTRING(O.FDDateofIssue,6,2)+'/'+SUBSTRING(O.FDDateofIssue,1,4) ELSE '' END AS FDDateofIssue"
            _Qry &= vbCrLf & " , CASE WHEN ISDATE(O.FDDateofExpiry)=1 THEN SUBSTRING(O.FDDateofExpiry,9,2)+'/'+SUBSTRING(O.FDDateofExpiry,6,2)+'/'+SUBSTRING(O.FDDateofExpiry,1,4) ELSE '' END AS FDDateofExpiry"
            _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther AS O WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON O.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "
            _Qry &= vbCrLf & " ORDER BY  O.FNFileOtherSeq desc"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            Me.ogcOther.DataSource = _oDt

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmaddEmpOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddEmpOther.Click
        If VerrifyOther() = False Then Exit Sub
        If Me.VerrifyData Then
            If HI.UL.ULDate.ConvertEnDB(FDDateofExpiryO1.Text) > HI.UL.ULDate.ConvertEnDB(FDDateofIssueO1.Text) Then
                If Me.SaveData() Then

                    Dim _Qry As String = ""

                    _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther SET "
                    _Qry &= vbCrLf & ", FDDateofIssue=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueO1.Text) & "' "
                    _Qry &= vbCrLf & ", FDDateofExpiry=N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryO1.Text) & "' "
                    _Qry &= vbCrLf & ", FTFileOtherNote=N'" & HI.UL.ULF.rpQuoted(FTFileOtherNoteO.Text) & "' "
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " AND  FTFileOtherNo='" & HI.UL.ULF.rpQuoted(FTFileOtherNoO.Text) & "'"
                    _Qry &= vbCrLf & "  AND FNFileOtherSeq=" & Me.FNFileOtherSeqO.Value

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                        _Qry = "SELECT MAX(FNFileOtherSeq) AS FNFileOtherSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther(FNHSysEmpID,FNFileOtherSeq, FTFileOtherNo, FDDateofIssue, FDDateofExpiry, FTFileOtherNote "
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTFileOtherNoO.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofIssueO1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiryO1.Text) & "'  "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTFileOtherNoteO.Text) & "' "
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    End If


                    _Qry = "UPDATE THRMEmployee_FileOther SET FNFileOtherSeq=FNNo"
                    _Qry &= vbCrLf & " FROM THRMEmployee_MOU INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNFileOtherSeq) AS FNNo, FNFileOtherSeq,FNHSysEmpID"
                    _Qry &= vbCrLf & " FROM THRMEmployee_FileOther WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ") T1 ON THRMEmployee_FileOther.FNFileOtherSeq=T1.FNFileOtherSeq AND THRMEmployee_FileOther.FNHSysEmpID=T1.FNHSysEmpID"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    HI.TL.HandlerControl.ClearControl(ogbOther)
                    Call LoadOther()

                End If
            Else
                HI.MG.ShowMsg.mProcessError(20181026001, "วันที่หมดอายุ น้อยกว่าวันที่เริ่มต้น กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                FDDateofExpiryO1.Focus()
            End If
        End If
    End Sub
    Private Function SaveOtherImage() As Boolean
        Dim dt As DataTable
        Dim _dataBinary As Byte()

        With CType(ogcOther.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        For Each R As DataRow In dt.Select("FTFileOtherNo <> ''  ", "FNFileOtherSeq")

            If R!FBFileImage.ToString <> "" Then
                _dataBinary = R!FBFileImage
                If Not (_dataBinary Is Nothing) Then
                    Dim _cmd As String = ""
                    _cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther"
                    _cmd &= " Set  FBFileImage=@FBFileImage"
                    _cmd &= " WHERE FNHSysEmpID=@FNHSysEmpID"
                    _cmd &= "  AND FNFileOtherSeq=@FNFileOtherSeq"
                    Dim cmd As New SqlCommand(_cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    cmd.Parameters.AddWithValue("@FNHSysEmpID", Val(FNHSysEmpID.Properties.Tag.ToString))
                    cmd.Parameters.AddWithValue("@FNFileOtherSeq", R!FNFileOtherSeq.ToString)

                    Dim data As Byte() = Nothing ' HI.UL.ULImage.ConvertImageToByteArray(Me.FTUserPic.Image, UL.ULImage.PicType.Employee)

                    For Each Obj As Object In Me.Controls.Find("FBFileImage", True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.PictureEdit
                                _dataBinary = HI.UL.ULImage.ConvertImageToByteArray(CType(Obj, DevExpress.XtraEditors.PictureEdit).Image, UL.ULImage.PicType.Employee)
                        End Select
                    Next


                    Dim _p As New SqlParameter("@FBFileImage", SqlDbType.Image)
                    _p.Value = _dataBinary
                    cmd.Parameters.Add(_p)
                    cmd.ExecuteNonQuery()
                End If
            End If

        Next

    End Function

    Private Sub ocmremoveEmpOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveEmpOther.Click
        If FNFileOtherSeqO.Value <= 0 Then Exit Sub


        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_FileOther  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FNFileOtherSeq=" & Me.FNFileOtherSeqO.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployee_FileOther SET FNFileOtherSeq=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployee_FileOther INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNFileOtherSeq) AS FNNo, FNFileOtherSeq,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployee_FileOther WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployee_FileOther.FNFileOtherSeq=T1.FNFileOtherSeq AND THRMEmployee_FileOther.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbOther)
        Call LoadOther()

    End Sub
    Private Sub ogvOther_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvOther.Click
        With ogvOther

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FNFileOtherSeqO.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNFileOtherSeq").ToString)
            FTFileOtherNoO.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTFileOtherNo").ToString
            FTFileOtherNoteO.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTFileOtherNote").ToString

            Try
                FDDateofIssueO1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofIssue").ToString)
            Catch ex As Exception
                FDDateofIssueO1.DateTime = Nothing
            End Try

            Try
                FDDateofExpiryO1.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FDDateofExpiry").ToString)
            Catch ex As Exception
                FDDateofExpiryO1.DateTime = Nothing
            End Try

            FTFileOtherNoO.Focus()

        End With
    End Sub
    Private Sub ocmclearOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearOther.Click
        HI.TL.HandlerControl.ClearControl(ogbOther)
    End Sub


    Private Function VerrifyOther() As Boolean
        Dim _Pass As Boolean = False
        If FTFileOtherNoO.Text <> "" Then
            If FDDateofIssueO1.Text <> "" Then
                If FDDateofExpiryO1.Text <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofExpiryO_lbl.Text)
                    FDDateofExpiryO1.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDDateofIssueO_lbl.Text)
                FDDateofIssueO1.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTFileOtherNo_lbl.Text)
            FTFileOtherNoO.Focus()
        End If

        Return _Pass
    End Function






    'Private Sub OcmFundRateSelect_Click_1(sender As Object, e As EventArgs) Handles ocmFundRateSelect.Click
    '    Try
    '        'check select fund Start
    '        If FDFundBegin.Text <> "" Then

    '            Dim _Qry As String = ""
    '            Dim _Dt As DataTable



    '            _Qry = " SELECT   CAST([FNEmpPay] AS INT) AS [FNEmpPay] "
    '            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMContributions] AS A WITH(NOLOCK)"
    '            _Qry &= vbCrLf & "  WHERE DATEDIFF(MONTH, '" & HI.UL.ULDate.ConvertEnDB(FDFundBegin.Text) & "',GETDATE()) > FNAgeBegin "
    '            _Qry &= vbCrLf & "  AND DATEDIFF(MONTH, '" & HI.UL.ULDate.ConvertEnDB(FDFundBegin.Text) & "',GETDATE()) <= FNAgeEnd "
    '            '_Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

    '            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '            With _wFundRatePopUp
    '                ._oDt = _Dt
    '                .FundRate = ""

    '                Call HI.ST.Lang.SP_SETxLanguage(_wFundRatePopUp)
    '                .ShowDialog()
    '                FNFundRate.Value = .FundRate

    '                'If _wFundRatePopUp.ShowDialog = System.Windows.Forms.DialogResult.OK Then
    '                '    Me.FDFundRate.Value = _wFundRatePopUp.FundRate_lbl.Text

    '                'End If
    '            End With

    '        Else
    '            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDFundBegin_lbl.Text)
    '            FDFundBegin.Focus()
    '        End If

    '    Catch ex As Exception

    '    End Try

    'End Sub



#End Region


    Private Sub ogvdetail_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles ogvdetail.RowCellClick
        Try
            Dim FNPassportSeq As String = ogvdetail.GetFocusedRowCellValue("FNPassportSeq").ToString()

            With _wEmployeeViewPic
                .FNSeqId = FNPassportSeq
                .FNHSysEmpID = (Val(FNHSysEmpID.Properties.Tag.ToString)).ToString
                .FTPicType = "Passport"

                .ShowDialog()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvvisa_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles ogvvisa.RowCellClick
        Try
            Dim FNVisaSeq As String = ogvdetail.GetFocusedRowCellValue("FNVisaSeq").ToString()

            With _wEmployeeViewPic
                .FNSeqId = FNVisaSeq
                .FNHSysEmpID = (Val(FNHSysEmpID.Properties.Tag.ToString)).ToString
                .FTPicType = "Visa"

                .ShowDialog()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvWork_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles ogvWork.RowCellClick
        Try
            Dim FNWorkpermitSeq As String = ogvdetail.GetFocusedRowCellValue("FNWorkpermitSeq").ToString()

            With _wEmployeeViewPic
                .FNSeqId = FNWorkpermitSeq
                .FNHSysEmpID = (Val(FNHSysEmpID.Properties.Tag.ToString)).ToString
                .FTPicType = "Work"

                .ShowDialog()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvMOU_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles ogvMOU.RowCellClick
        Try
            Dim FNMOUSeq As String = ogvdetail.GetFocusedRowCellValue("FNMOUSeq").ToString()

            With _wEmployeeViewPic
                .FNSeqId = FNMOUSeq
                .FNHSysEmpID = (Val(FNHSysEmpID.Properties.Tag.ToString)).ToString
                .FTPicType = "MOU"

                .ShowDialog()
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvOther_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles ogvOther.RowCellClick
        Try
            Dim FNFileOtherSeq As String = ogvdetail.GetFocusedRowCellValue("FNFileOtherSeq").ToString()

            With _wEmployeeViewPic
                .FNSeqId = FNFileOtherSeq
                .FNHSysEmpID = (Val(FNHSysEmpID.Properties.Tag.ToString)).ToString
                .FTPicType = "Other"

                .ShowDialog()
            End With
        Catch ex As Exception
        End Try
    End Sub



    Private Sub CheckEdit6_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateNoSurgery.CheckedChanged

    End Sub

#Region "Stricken"

    Private Sub ocmaddstricken_Click(sender As Object, e As EventArgs) Handles ocmaddstricken.Click
        If VerrifyStricken() = False Then Exit Sub
        If Me.VerrifyData Then

            If Me.SaveData() Then

                Dim _Qry As String = ""

                _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Stricken SET "
                _Qry &= vbCrLf & ", FTStricken=N'" & HI.UL.ULF.rpQuoted(FTStricken.Text) & "' "
                _Qry &= vbCrLf & ", FTYear=N'" & HI.UL.ULF.rpQuoted(FTYear.Text) & "'"
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & "  AND FTSeqStricken=" & Me.FTSeqStricken1.Value

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "SELECT MAX(FTSeqStricken) AS FTSeqStricken FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Stricken WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "

                    Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Stricken(FNHSysEmpID,FTSeqStricken, FTStricken, FTYear "
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTStricken.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTYear.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If


                _Qry = "UPDATE THRMEmployee_Stricken SET FTSeqStricken=FNNo"
                _Qry &= vbCrLf & " FROM THRMEmployee_Stricken INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTSeqStricken) AS FNNo, FTSeqStricken,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRMEmployee_Stricken WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ") T1 ON THRMEmployee_Visa.FTSeqStricken=T1.FTSeqStricken AND THRMEmployee_Visa.FNHSysEmpID=T1.FNHSysEmpID"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                HI.TL.HandlerControl.ClearControl(ogc)
                Call LoadStricken()

                FTStricken.Text = ""
                FTYear.Text = ""

            End If

        End If
    End Sub
    Private Function VerrifyStricken() As Boolean
        Dim _Pass As Boolean = False
        If FTStricken.Text <> "" Then
            If FTYear.Text <> "" Then
                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTYear_lbl.Text)
                FTYear.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTStricken_lbl.Text)
            FTStricken.Focus()
        End If

        Return _Pass
    End Function
    Private Sub LoadStricken()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT  S.FTSeqStricken,S.FTStricken,S.FTYear,E.FTEmpCode"
            _Qry &= vbCrLf & "FROM    " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Stricken AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee AS E WITH (NOLOCK) ON S.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "


            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            Me.ogc.DataSource = _oDt
            'Call LoadPP()

        Catch ex As Exception
        End Try
    End Sub
    Private Function SaveStricken() As Boolean

        If Me.VerrifyData Then

            'If Me.SaveData() Then

            Dim _Qry As String = ""

            _Qry = "UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_HistoryOfIllness SET "
            _Qry &= vbCrLf & " FTStateNoDisease=N'" & FTStateNoDisease.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateDisease=N'" & FTStateDisease.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTDiseaseNote=N'" & HI.UL.ULF.rpQuoted(FTDiseaseNote.Text) & "' "
            _Qry &= vbCrLf & ", FTStateNoSurgery=N'" & FTStateNoSurgery.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateSurgery=N'" & FTStateSurgery.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTSurgeryNote=N'" & HI.UL.ULF.rpQuoted(FTSurgeryNote.Text) & "' "
            _Qry &= vbCrLf & ", FTStateNoImmunity=N'" & FTStateNoImmunity.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateImmunity=N'" & FTStateImmunity.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTImmunityNote=N'" & HI.UL.ULF.rpQuoted(FTImmunityNote.Text) & "' "
            _Qry &= vbCrLf & ", FTStateNoSirFamily=N'" & FTStateNoSirFamily.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateSirFamily=N'" & FTStateSirFamily.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTRelationD=N'" & HI.UL.ULF.rpQuoted(FTRelationD.Text) & "' "
            _Qry &= vbCrLf & ", FTDiseaseD=N'" & HI.UL.ULF.rpQuoted(FTDiseaseD.Text) & "' "
            _Qry &= vbCrLf & ", FTRelationM=N'" & HI.UL.ULF.rpQuoted(FTRelationM.Text) & "' "
            _Qry &= vbCrLf & ", FTDiseaseM=N'" & HI.UL.ULF.rpQuoted(FTDiseaseM.Text) & "' "
            _Qry &= vbCrLf & ", FTRelationS=N'" & HI.UL.ULF.rpQuoted(FTRelationS.Text) & "' "
            _Qry &= vbCrLf & ", FTDiseaseS=N'" & HI.UL.ULF.rpQuoted(FTDiseaseS.Text) & "' "
            _Qry &= vbCrLf & ", FTStateNoDrugDisease=N'" & FTStateNoDrugDisease.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateDrugDisease=N'" & FTStateDrugDisease.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTDrugDiseaseNote=N'" & HI.UL.ULF.rpQuoted(FTDrugDiseaseNote.Text) & "' "
            _Qry &= vbCrLf & ", FTStateNoHobby=N'" & FTStateNoHobby.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateHobby=N'" & FTStateHobby.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTHobbyNote=N'" & HI.UL.ULF.rpQuoted(FTHobbyNote.Text) & "' "
            _Qry &= vbCrLf & ", FTStateNoSmoking=N'" & FTStateNoSmoking.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateSmoking=N'" & FTStateSmoking.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTSmoking=N'" & HI.UL.ULF.rpQuoted(FTSmoking.Text) & "' "
            _Qry &= vbCrLf & ", FTStateQuitSmoking=N'" & FTStateQuitSmoking.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTYearSmoking=N'" & HI.UL.ULF.rpQuoted(FTYearSmoking.Text) & "' "
            _Qry &= vbCrLf & ", FTMonthSmoking=N'" & HI.UL.ULF.rpQuoted(FTMonthSmoking.Text) & "'"
            _Qry &= vbCrLf & ", FTSmokingQ=N'" & HI.UL.ULF.rpQuoted(FTSmokingQ.Text) & "'"
            _Qry &= vbCrLf & ", FTStateNoAlcohol=N'" & FTStateNoAlcohol.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateLessAlcohol=N'" & FTStateLessAlcohol.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateOneAlcohol=N'" & FTStateOneAlcohol.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateThreeAlcohol=N'" & FTStateThreeAlcohol.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateOverThreeAlcohol=N'" & FTStateOverThreeAlcohol.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateQuitAlcohol=N'" & FTStateQuitAlcohol.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTYearAlcohol=N'" & HI.UL.ULF.rpQuoted(FTYearAlcohol.Text) & "' "
            _Qry &= vbCrLf & ", FTMonthAlcohol=N'" & HI.UL.ULF.rpQuoted(FTMonthAlcohol.Text) & "'"
            _Qry &= vbCrLf & ", FTStateNoDope=N'" & FTStateNoDope.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTStateDope=N'" & FTStateDope.EditValue.ToString & "' "
            _Qry &= vbCrLf & ", FTDopeNote=N'" & HI.UL.ULF.rpQuoted(FTDopeNote.Text) & "' "
            _Qry &= vbCrLf & ", FTOther=N'" & HI.UL.ULF.rpQuoted(FTOther.Text) & "' "
            _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "


            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                _Qry = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_HistoryOfIllness(FNHSysEmpID,FTStateNoDisease, FTStateDisease, FTDiseaseNote,FTStateNoSurgery,FTStateSurgery,FTSurgeryNote,FTStateNoImmunity,FTStateImmunity,FTImmunityNote "
                _Qry &= vbCrLf & ",FTStateNoSirFamily,FTStateSirFamily,FTRelationD,FTDiseaseD,FTRelationM,FTDiseaseM,FTRelationS,FTDiseaseS,FTStateNoDrugDisease,FTStateDrugDisease,FTDrugDiseaseNote,FTStateNoHobby,FTStateHobby,FTHobbyNote,FTStateNoSmoking,FTStateSmoking"
                _Qry &= vbCrLf & ",FTSmoking,FTStateQuitSmoking,FTYearSmoking,FTMonthSmoking,FTSmokingQ,FTStateNoAlcohol,FTStateLessAlcohol,FTStateOneAlcohol,FTStateThreeAlcohol,FTStateOverThreeAlcohol,FTStateQuitAlcohol,FTYearAlcohol,FTMonthAlcohol,FTStateNoDope,FTStateDope,FTDopeNote,FTOther"
                _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                _Qry &= vbCrLf & " SELECT " & Val(FNHSysEmpID.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & ",N'" & FTStateNoDisease.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateDisease.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDiseaseNote.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoSurgery.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateSurgery.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTSurgeryNote.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoImmunity.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateImmunity.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTImmunityNote.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoSirFamily.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateSirFamily.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRelationD.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDiseaseD.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRelationM.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDiseaseM.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRelationS.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDiseaseS.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoDrugDisease.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateDrugDisease.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDrugDiseaseNote.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoHobby.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateHobby.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTHobbyNote.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoSmoking.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateSmoking.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTSmoking.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateQuitSmoking.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTYearSmoking.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTMonthSmoking.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTSmokingQ.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoAlcohol.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateLessAlcohol.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateOneAlcohol.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateThreeAlcohol.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateOverThreeAlcohol.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateQuitAlcohol.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTYearAlcohol.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTMonthAlcohol.Text) & "' "
                _Qry &= vbCrLf & ",N'" & FTStateNoDope.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & FTStateDope.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDopeNote.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTOther.Text) & "' "
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            End If

        End If
        'End If

    End Function

    Private Sub LoadStricken1()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            Dim _DocNo As String = ""
            Dim _dtdoc As New DataTable

            _Qry = "SELECT  E.FTEmpCode,H.FTStateNoDisease, H.FTStateDisease, H.FTDiseaseNote, H.FTStateNoSurgery, H.FTStateSurgery, H.FTSurgeryNote, H.FTStateNoImmunity, "
            _Qry &= vbCrLf & "H.FTStateImmunity, H.FTImmunityNote, H.FTStateNoSirFamily, H.FTStateSirFamily, H.FTRelationD,H.FTDiseaseD, H.FTRelationM, H.FTDiseaseM, H.FTRelationS,H. FTDiseaseS, H.FTStateNoDrugDisease,H. FTStateDrugDisease, H.FTDrugDiseaseNote,"
            _Qry &= vbCrLf & "H. FTStateNoHobby, H.FTStateHobby, H.FTHobbyNote, H.FTStateNoSmoking, H.FTSmoking, H.FTStateQuitSmoking,H. FTYearSmoking, H.FTMonthSmoking, H.FTSmokingQ, H.FTStateNoAlcohol, H.FTStateLessAlcohol, H.FTStateOneAlcohol, "
            _Qry &= vbCrLf & "H.FTStateThreeAlcohol, H.FTStateOverThreeAlcohol, H.FTStateQuitAlcohol, H.FTYearAlcohol, H.FTMonthAlcohol, H.FTStateNoDope, H.FTStateDope, H.FTDopeNote, H.FTOther, H.FTStateSmoking"
            _Qry &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HistoryOfIllness AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON H.FNHSysEmpID=E.FNHSysEmpID "
            _Qry &= vbCrLf & " WHERE E.FTEmpCode ='" & Me.FNHSysEmpID.Text & "' "


            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR,, False)

            For Each R As DataRow In _oDt.Rows
                If R!FTStateNoDisease.ToString = "1" Then
                    FTStateNoDisease.Checked = True
                Else
                    FTStateNoDisease.Checked = False
                End If
                If R!FTStateDisease.ToString = "1" Then
                    FTStateDisease.Checked = True
                Else
                    FTStateDisease.Checked = False
                End If
                FTDiseaseNote.Text = R!FTDiseaseNote.ToString

                If R!FTStateNoSurgery.ToString = "1" Then
                    FTStateNoSurgery.Checked = True
                Else
                    FTStateNoSurgery.Checked = False
                End If
                If R!FTStateSurgery.ToString = "1" Then
                    FTStateSurgery.Checked = True
                Else
                    FTStateSurgery.Checked = False
                End If
                FTSurgeryNote.Text = R!FTSurgeryNote.ToString

                If R!FTStateNoImmunity.ToString = "1" Then
                    FTStateNoImmunity.Checked = True
                Else
                    FTStateNoImmunity.Checked = False
                End If
                If R!FTStateImmunity.ToString = "1" Then
                    FTStateImmunity.Checked = True
                Else
                    FTStateImmunity.Checked = False
                End If
                FTImmunityNote.Text = R!FTSurgeryNote.ToString

                If R!FTStateNoSirFamily.ToString = "1" Then
                    FTStateNoSirFamily.Checked = True
                Else
                    FTStateNoSirFamily.Checked = False
                End If
                If R!FTStateSirFamily.ToString = "1" Then
                    FTStateSirFamily.Checked = True
                Else
                    FTStateSirFamily.Checked = False
                End If
                FTRelationD.Text = R!FTRelationD.ToString
                FTDiseaseD.Text = R!FTDiseaseD.ToString
                FTRelationM.Text = R!FTRelationM.ToString
                FTDiseaseM.Text = R!FTDiseaseM.ToString
                FTRelationS.Text = R!FTRelationS.ToString
                FTDiseaseS.Text = R!FTDiseaseS.ToString

                If R!FTStateNoDrugDisease.ToString = "1" Then
                    FTStateNoDrugDisease.Checked = True
                Else
                    FTStateNoDrugDisease.Checked = False
                End If
                If R!FTStateDrugDisease.ToString = "1" Then
                    FTStateDrugDisease.Checked = True
                Else
                    FTStateDrugDisease.Checked = False
                End If
                FTDrugDiseaseNote.Text = R!FTDrugDiseaseNote.ToString

                If R!FTStateNoHobby.ToString = "1" Then
                    FTStateNoHobby.Checked = True
                Else
                    FTStateNoHobby.Checked = False
                End If
                If R!FTStateHobby.ToString = "1" Then
                    FTStateHobby.Checked = True
                Else
                    FTStateHobby.Checked = False
                End If
                FTHobbyNote.Text = R!FTHobbyNote.ToString

                If R!FTStateNoSmoking.ToString = "1" Then
                    FTStateNoSmoking.Checked = True
                Else
                    FTStateNoSmoking.Checked = False
                End If
                If R!FTStateSmoking.ToString = "1" Then
                    FTStateSmoking.Checked = True
                Else
                    FTStateSmoking.Checked = False
                End If
                FTSmoking.Text = R!FTSmoking.ToString
                If R!FTStateQuitSmoking.ToString = "1" Then
                    FTStateQuitSmoking.Checked = True
                Else
                    FTStateQuitSmoking.Checked = False
                End If
                FTYearSmoking.Text = R!FTYearSmoking.ToString
                FTMonthSmoking.Text = R!FTMonthSmoking.ToString
                FTSmokingQ.Text = R!FTSmokingQ.ToString

                If R!FTStateNoAlcohol.ToString = "1" Then
                    FTStateNoAlcohol.Checked = True
                Else
                    FTStateNoAlcohol.Checked = False
                End If
                If R!FTStateLessAlcohol.ToString = "1" Then
                    FTStateLessAlcohol.Checked = True
                Else
                    FTStateLessAlcohol.Checked = False
                End If
                If R!FTStateOneAlcohol.ToString = "1" Then
                    FTStateOneAlcohol.Checked = True
                Else
                    FTStateOneAlcohol.Checked = False
                End If
                If R!FTStateThreeAlcohol.ToString = "1" Then
                    FTStateThreeAlcohol.Checked = True
                Else
                    FTStateThreeAlcohol.Checked = False
                End If
                If R!FTStateOverThreeAlcohol.ToString = "1" Then
                    FTStateOverThreeAlcohol.Checked = True
                Else
                    FTStateOverThreeAlcohol.Checked = False
                End If
                If R!FTStateQuitAlcohol.ToString = "1" Then
                    FTStateQuitAlcohol.Checked = True
                Else
                    FTStateQuitAlcohol.Checked = False
                End If
                FTYearAlcohol.Text = R!FTYearAlcohol.ToString
                FTMonthAlcohol.Text = R!FTMonthAlcohol.ToString

                If R!FTStateNoDope.ToString = "1" Then
                    FTStateNoDope.Checked = True
                Else
                    FTStateNoDope.Checked = False
                End If
                If R!FTStateDope.ToString = "1" Then
                    FTStateDope.Checked = True
                Else
                    FTStateDope.Checked = False
                End If
                FTDopeNote.Text = R!FTDopeNote.ToString

                FTOther.Text = R!FTOther.ToString
            Next







            ' Me.ogc.DataSource = _oDt
            'Call LoadPP()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmremovestricken_Click(sender As Object, e As EventArgs) Handles ocmremovestricken.Click
        If FTSeqStricken1.Value <= 0 Then Exit Sub


        Dim _Qry As String = ""

        _Qry = " Delete  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee_Stricken  "
        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  AND FTSeqStricken=" & Me.FTSeqStricken1.Value

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE THRMEmployee_Stricken SET FTSeqStricken=FNNo"
        _Qry &= vbCrLf & " FROM THRMEmployee_Stricken INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FTSeqStricken) AS FNNo, FTSeqStricken,FNHSysEmpID"
        _Qry &= vbCrLf & " FROM THRMEmployee_Stricken WHERE  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & ") T1 ON THRMEmployee_Stricken.FTSeqStricken=T1.FTSeqStricken AND THRMEmployee_Stricken.FNHSysEmpID=T1.FNHSysEmpID"

        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogc)
        Call LoadStricken()
        FTStricken.Text = ""
        FTYear.Text = ""

    End Sub



    Private Sub ogv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogv.Click
        With ogv

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub


            FTSeqStricken1.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTSeqStricken").ToString)
            FTStricken.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTStricken").ToString
            FTYear.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTYear").ToString




            FTStricken.Focus()

        End With
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysUnitSectId_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysUnitSectId.Text <> "" Then
                    Dim _Qry As String = ""

                    _Qry = "SELECT TOP 1 FNHSysUnitSectId  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnitSect WITH(NOLOCK) WHERE FTUnitSectCode =N'" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "' AND FNHSysCmpId =  " & HI.ST.SysInfo.CmpID
                    FNHSysUnitSectId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                    _Qry = "SELECT US.FNHSysAccountGroupId, FTAccountGroupCode  "
                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " ,FTAccountGroupNameTH AS  FTAccountGroupName "
                    Else
                        _Qry &= vbCrLf & " ,FTAccountGroupNameEN AS  FTAccountGroupName "
                    End If
                    _Qry &= vbCrLf & " "
                    _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnitSect] US  WITH ( NOLOCK )   "
                    _Qry &= vbCrLf & " LEFT jOIN [HITECH_MASTER].[dbo].[TCNMAccountGroup] ACG ON US.FNHSysAccountGroupId = ACG.FNHSysAccountGroupId  "
                    _Qry &= vbCrLf & " WHERE US.FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectId.Properties.Tag)

                    Try
                        For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Rows
                            Me.FTAccountGroupCode.Text = R!FTAccountGroupCode.ToString
                            Me.FTAccountGroupName.Text = R!FTAccountGroupName.ToString
                        Next
                    Catch ex As Exception

                    End Try
                    ' Call Load_PDF1()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysUnitSectIdOrg_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectIdOrg.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysUnitSectIdOrg_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysUnitSectIdOrg.Text <> "" Then
                    Dim _Qry As String = ""

                    _Qry = "SELECT TOP 1 FNHSysUnitSectId  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnitSect WITH(NOLOCK) WHERE FTUnitSectCode =N'" & HI.UL.ULF.rpQuoted(FNHSysUnitSectIdOrg.Text) & "' AND FNHSysCmpId =  " & HI.ST.SysInfo.CmpID
                    FNHSysUnitSectIdOrg.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                    _Qry = "SELECT US.FNHSysAccountGroupId, FTAccountGroupCode  "
                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " ,FTAccountGroupNameTH AS  FTAccountGroupName "
                    Else
                        _Qry &= vbCrLf & " ,FTAccountGroupNameEN AS  FTAccountGroupName "
                    End If
                    _Qry &= vbCrLf & " "
                    _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.[TCNMUnitSect] US  WITH ( NOLOCK )   "
                    _Qry &= vbCrLf & " LEFT jOIN [HITECH_MASTER].[dbo].[TCNMAccountGroup] ACG ON US.FNHSysAccountGroupId = ACG.FNHSysAccountGroupId  "
                    _Qry &= vbCrLf & " WHERE US.FNHSysUnitSectId =" & Val(Me.FNHSysUnitSectIdOrg.Properties.Tag)

                    Try
                        For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER).Rows
                            Me.FTAccountGroupCode_Org.Text = R!FTAccountGroupCode.ToString
                            Me.FTAccountGroupName_Org.Text = R!FTAccountGroupName.ToString
                        Next
                    Catch ex As Exception

                    End Try
                    ' Call Load_PDF1()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNEmpStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNEmpStatus.SelectedIndexChanged

    End Sub

    Private Sub FNEnablonType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNEnablonType.SelectedIndexChanged

    End Sub

    Private Sub GroupControl32_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl32.Paint

    End Sub

    Private Sub XtraTabPage1_Paint(sender As Object, e As PaintEventArgs) Handles XtraTabPage1.Paint

    End Sub

    Private Sub GroupControl27_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl27.Paint

    End Sub

    Private Sub GroupControl14_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl14.Paint

    End Sub

    Private Sub ogbstatus_Paint(sender As Object, e As PaintEventArgs) Handles ogbstatus.Paint

    End Sub












#End Region

End Class