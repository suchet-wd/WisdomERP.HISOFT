Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wReserveAX

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_PUR
    Private _Bindgrid As Boolean = False
    Private _RowDcng As Boolean = False
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private _AddItemPopup As wReserveAXItemPopup

    Private _DataInfo As DataTable
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\")

    Private _ProcLoad As Boolean = False
    Private _FormLoad As Boolean = True
    Sub New()
        _FormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitFormControl()

        _AddItemPopup = New wReserveAXItemPopup
        HI.TL.HandlerControl.AddHandlerObj(_AddItemPopup)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddItemPopup.Name.ToString.Trim, _AddItemPopup)
        Catch ex As Exception
        Finally
        End Try

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

    Private _mPriceClosed1 As Double = -1
    Public Property PriceClosed1 As Double
        Get
            Return _mPriceClosed1
        End Get
        Set(value As Double)
            _mPriceClosed1 = value
        End Set
    End Property


    Private _mPriceClosed2 As Double = -1
    Public Property PriceClosed2 As Double
        Get
            Return _mPriceClosed2
        End Get
        Set(value As Double)
            _mPriceClosed2 = value
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

        Call LoadDucumentDetail(Key.ToString)


        If CheckApprove(False) Then Exit Sub

        _ProcLoad = False

    End Sub

    Private Sub LoadDucumentDetail(PoKey As String)

        Dim cmdstring As String = ""

        ''cmdstring = "   Select  A.*,B.INTERFACESTATUS,B.[ISREVERT],B.[REVERTSTATUS] "
        ''cmdstring &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail As A WITH(NOLOCK) "
        ''cmdstring &= vbCrLf & "    OUTER APPLY(SELECT TOP 1	B.INTERFACESTATUS, B.[ISREVERT], B.[REVERTSTATUS] "
        ''cmdstring &= vbCrLf & " From [HIGDBCRSV].[AX63CU13_HIT_UAT].[dbo].[IVZ_WISDOMINBOUNDRESERVERM_CT]  AS B WITH(NOLOCK) "
        ''cmdstring &= vbCrLf & " Where B.ReservedStockNo = A.FTReserveAXNo "
        ''cmdstring &= vbCrLf & " 	And B.ItemId = A.FTRMCode  "
        ''cmdstring &= vbCrLf & " 	And B.COLORID = A.FTRMColorCode "
        ''cmdstring &= vbCrLf & " 	And B.SIZEID = A.FTRMSizeCode  "
        ''cmdstring &= vbCrLf & " 	And B.LICENSEPLATEID = A.FTLPNo"
        ''cmdstring &= vbCrLf & " ORDER BY  B.RECID DESC "
        ''cmdstring &= vbCrLf & " 	 ) AS B "
        ''cmdstring &= vbCrLf & " WHERE  A.FTReserveAXNo='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text.Trim) & "'"
        ''cmdstring &= vbCrLf & " ORDER BY   A.FTReserveAXNo,A.FNSeq "



        cmdstring = "  EXEC   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_RESERVESTOCKAX_LOADDETAIL '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text.Trim) & "'"

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

        ogcdetail.DataSource = dt.Copy

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

        If FTOrderNo.Text.Trim = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAXWH_lbl.Text)
            FTOrderNo.Focus()
            Return False
        End If

        If FTAXWH.Text.Trim = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAXWH_lbl.Text)
            FTAXWH.Focus()
            Return False
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
            _Key = HI.TL.Document.GetDocumentNo(Me.SysDBName, Me.SysTableName, FNDocumentAXType.SelectedIndex.ToString, False, _CmpH).ToString

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

    Private Function DeleteData() As Boolean
        Try

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX WHERE FTReserveAXNo='" & HI.UL.ULF.rpQuoted(Me.FTReserveAXNo.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If


            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail WHERE FTReserveAXNo='" & HI.UL.ULF.rpQuoted(Me.FTReserveAXNo.Text) & "'"

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
    End Sub

#End Region

#Region "MAIN PROC"


    Private Function CheckOwner() As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTReserveAXBy.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTReserveAXBy.Text) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysTeamGrpId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    HI.MG.ShowMsg.mProcessError(1405280912, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                End If

            Else

                HI.MG.ShowMsg.mProcessError(1405280912, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข เอกสาร นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False

            End If

        End If
    End Function

    Private Sub Proc_Save(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then Exit Sub

        If CheckApprove() Then Exit Sub
        If FNDocumentState.SelectedIndex <> 0 Then
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

        If CheckApprove() Then Exit Sub
        If FNDocumentState.SelectedIndex <> 0 Then
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, Me.FTReserveAXNo.Text, Me.Text) = False Then
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
        If Me.FTReserveAXNo.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "ReserveSlipAX.rpt"

                If FNDocumentState.SelectedIndex = 0 Then
                    .AddParameter("Draft", "DRAFT")
                End If

                .Formular = "{TINVENReserveAX.FTReserveAXNo}='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "' "
                .Preview()
            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "ReserveSlip_BarcodeAX.rpt"

                If FNDocumentState.SelectedIndex = 0 Then
                    .AddParameter("Draft", "DRAFT")
                End If

                .Formular = "{TINVENReserveAX.FTReserveAXNo}='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "' "
                .Preview()
            End With

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReserveAXNo_lbl.Text)
            FTReserveAXNo.Focus()
        End If
    End Sub

    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "


#End Region
    Private Function DeleteBarcode(BarcodeKey As String) As Boolean
        Dim _Str As String

        Dim _BarCode As String = BarcodeKey


        Try

            _Str = "  DELETE  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail "
            _Str &= vbCrLf & "  WHERE FTReserveAXNo='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "' "
            _Str &= vbCrLf & "  AND FTLPNo='" & HI.UL.ULF.rpQuoted(_BarCode) & "' "


            If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
#Region " Variable "

#End Region

    Private Sub DeleteBarcode()
        If CheckOwner() = False Then Exit Sub
        If CheckApprove() Then Exit Sub
        If FNDocumentState.SelectedIndex = 1 Then
            Exit Sub
        End If

        With ogvdetail
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _StateDelete As Boolean = False
            For Each i As Integer In .GetSelectedRows()
                Dim _Barcode As String = "" & .GetRowCellValue(i, "FTLPNo").ToString


                If _Barcode <> "" Then
                    If DeleteBarcode(_Barcode) Then
                        _StateDelete = True
                    End If
                End If



            Next

            If (_StateDelete) Then
                LoadDucumentDetail(Me.FTReserveAXNo.Text)
            End If

        End With
    End Sub

    Private Sub AddBarCode()

        Dim _Str As String = ""
        Dim BarcodeNo As String = ""
        Dim _dtBar As New DataTable
        Dim _Dt As New DataTable
        Dim Qty As Double = 0

        'Dim spls2 As New HI.TL.SplashScreen("Loading Data Onhand Please wait....")

        '_Str = "  EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_RESERVESTOCKAX '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text.Trim()) & "'," & FNDocumentAXType.SelectedIndex & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim()) & "','" & HI.UL.ULF.rpQuoted(FTAXWH.Text.Trim()) & "'"


        '_dtBar = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)
        'spls2.Close()

        With _AddItemPopup
            Call HI.ST.Lang.SP_SETxLanguage(_AddItemPopup)
            .ogcbarcode.DataSource = Nothing
            .FNHSysMainMatId.Text = ""
            .FTStateCheckMRP.Checked = True
            .ocmcancel.Enabled = True
            .ocmok.Enabled = True
            .ocmsearch.Enabled = True

            '.ogcbarcode.DataSource = _dtBar
            .ogvbarcode.ActiveFilter.Clear()
            .ReserveAXNo = FTReserveAXNo.Text.Trim
            .DocumentAXType = FNDocumentAXType.SelectedIndex
            .OrderNo = FTOrderNo.Text.Trim
            .WHNo = FTAXWH.Text.Trim
            .ShowDialog()
            If .ProcessProc Then
                _dtBar = CType(.ogcbarcode.DataSource, DataTable).Copy
                Dim _DocumentRefNo As String = ""
                If _dtBar.Select(" FTSelect='1' AND FNDocQuantity > 0 ").Length > 0 Then

                    Dim spls As New HI.TL.SplashScreen("Saving Data Please wait....")
                    Dim RowSeq As Integer = 0

                    Try
                        '_Str = " Delete FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail "
                        '_Str &= vbCrLf & "  WHERE FTReserveAXNo='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "' "

                        'HI.Conn.SQLConn.ExecuteOnly(_Str, Conn.DB.DataBaseName.DB_PUR)
                        RowSeq = RowSeq + 1
                        For Each R As DataRow In _dtBar.Select("  FTSelect='1' AND FNDocQuantity > 0 ", "FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,JobNumber,LicencePlateId")


                            _Str = " Declare @Count int = 0; "
                            _Str &= vbCrLf & " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail "

                            _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",FTRMColorName='" & HI.UL.ULF.rpQuoted(R!ColorName.ToString) & "' "
                            _Str &= vbCrLf & ",FTFromAXSite='" & HI.UL.ULF.rpQuoted(R!Site.ToString) & "' "
                            _Str &= vbCrLf & ",FNFromAXWH='" & HI.UL.ULF.rpQuoted(R!Warehouse.ToString) & "' "
                            _Str &= vbCrLf & ",FTToAXSite='" & HI.UL.ULF.rpQuoted(R!ToSite.ToString) & "' "
                            _Str &= vbCrLf & ",FNToAXWH='" & HI.UL.ULF.rpQuoted(R!Warehouse.ToString) & "' "
                            _Str &= vbCrLf & ",FTFromOrderNo='" & HI.UL.ULF.rpQuoted(R!JobNumber.ToString) & "' "
                            _Str &= vbCrLf & ",FTToOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim) & "' "
                            _Str &= vbCrLf & ",FTFromSeason='" & HI.UL.ULF.rpQuoted(R!Season.ToString) & "' "
                            _Str &= vbCrLf & ",FTToSeason='" & HI.UL.ULF.rpQuoted(R!ToSeason.ToString) & "' "
                            _Str &= vbCrLf & ",FTUnit='" & HI.UL.ULF.rpQuoted(R!StockUnit.ToString) & "' "
                            _Str &= vbCrLf & ",FNQuantity=" & Val(R!FNDocQuantity.ToString) & " "
                            _Str &= vbCrLf & ",FTPONO='" & HI.UL.ULF.rpQuoted(R!PONumber.ToString) & "' "
                            _Str &= vbCrLf & ",FromInventStatusId='" & HI.UL.ULF.rpQuoted(R!InventoryStatus.ToString) & "' "
                            _Str &= vbCrLf & ",ToInventStatusId='Ordered' "
                            _Str &= vbCrLf & ",FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & " "
                            _Str &= vbCrLf & ",FNHSysUnitId=" & Val(R!FNHSysUnitId.ToString) & " "
                            _Str &= vbCrLf & ",DataAreaId='" & HI.UL.ULF.rpQuoted(R!DataAreaId.ToString) & "' "
                            _Str &= vbCrLf & ",FTLocation='" & HI.UL.ULF.rpQuoted(R!Location.ToString) & "' "

                            _Str &= vbCrLf & " WHERE FTReserveAXNo='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "' "
                            _Str &= vbCrLf & " AND FTRMCode='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'"
                            _Str &= vbCrLf & " AND FTRMColorCode='" & HI.UL.ULF.rpQuoted(R!FTRawMatColorCode.ToString) & "'"
                            _Str &= vbCrLf & " AND  FTRMSizeCode='" & HI.UL.ULF.rpQuoted(R!SizeId.ToString) & "' "
                            _Str &= vbCrLf & " AND  FTBatchNo='" & HI.UL.ULF.rpQuoted(R!BatchNumber.ToString) & "'"
                            _Str &= vbCrLf & " AND  FTLPNo ='" & HI.UL.ULF.rpQuoted(R!LicencePlateId.ToString) & "' "
                            _Str &= vbCrLf & " SET  @Count  = @@ROWCOUNT "

                            _Str &= vbCrLf & "    If @Count <=0 "
                            _Str &= vbCrLf & "  BEGIN "
                            _Str &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail(FTInsUser, FDInsDate, FTInsTime,  FTReserveAXNo, FNSeq, FTRMCode, FTRMColorCode, FTRMColorName, FTRMSizeCode, FTBatchNo, FTLPNo, "
                            _Str &= vbCrLf & "   FTFromAXSite, FNFromAXWH, FTToAXSite, FNToAXWH, FTFromOrderNo, FTToOrderNo, FTFromSeason, FTToSeason, FTUnit, FNQuantity, FTPONO, FromInventStatusId, ToInventStatusId,  "
                            _Str &= vbCrLf & "    FNHSysRawMatId, FNHSysUnitId, DataAreaId ,FTLocation"
                            _Str &= vbCrLf & " )  "
                            _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "' "
                            _Str &= vbCrLf & ",ISNULL((select max(FNSeq)  AS FNSeq from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX_Detail AS X WHERE X.FTReserveAXNo ='" & HI.UL.ULF.rpQuoted(FTReserveAXNo.Text) & "'),0)+1 "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatColorCode.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ColorName.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!SizeId.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BatchNumber.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!LicencePlateId.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Site.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Warehouse.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ToSite.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Warehouse.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!JobNumber.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Season.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!ToSeason.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!StockUnit.ToString) & "' "
                            _Str &= vbCrLf & "," & Val(R!FNDocQuantity.ToString) & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PONumber.ToString) & "' "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!InventoryStatus.ToString) & "' "
                            _Str &= vbCrLf & ",'Ordered' "
                            _Str &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                            _Str &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & " "
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!DataAreaId.ToString) & "' "

                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Location.ToString) & "' "
                            _Str &= vbCrLf & " END "

                            If HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_PUR) Then
                                RowSeq = RowSeq + 1
                            End If

                        Next

                    Catch ex As Exception

                    End Try


                    LoadDucumentDetail(Me.FTReserveAXNo.Text)
                    spls.Close()

                End If

            End If

        End With

        _Dt.Dispose()
        _dtBar.Dispose()

    End Sub

    Private Sub ogvdetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvdetail.KeyDown
        If ocmdeletebarcode.Enabled = False Then Exit Sub
        Select Case e.KeyCode
            Case Keys.Delete

                Call DeleteBarcode()

        End Select
    End Sub

    Private Sub ogvdetail_RowCountChanged(sender As Object, e As System.EventArgs) Handles ogvdetail.RowCountChanged
        Try
            Dim dt As New DataTable

            Try
                dt = CType(ogcdetail.DataSource, DataTable).Copy
            Catch ex As Exception
            End Try

            FTAXWH.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTAXWH.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)

            FTOrderNo.Properties.ReadOnly = (dt.Rows.Count > 0)
            FTOrderNo.Properties.Buttons.Item(0).Enabled = Not (dt.Rows.Count > 0)


            FNDocumentAXType.Properties.ReadOnly = (dt.Rows.Count > 0)

            dt.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmaddbarcod_Click(sender As System.Object, e As System.EventArgs) Handles ocmaddbarcod.Click
        If CheckOwner() = False Then Exit Sub
        'If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), Me.FDReserveDate.Text) = True Then
        '    Exit Sub
        'End If

        If HI.ST.ValidateData.CloseJob(Me.FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CheckApprove() Then Exit Sub
        If FNDocumentState.SelectedIndex = 1 Then
            Exit Sub
        End If

        If FTReserveAXNo.Properties.Tag.ToString = "" Then
            If Me.VerrifyData() Then
                If Me.SaveData Then
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else

            If Me.FTReserveAXNo.Text = "" Then Exit Sub


            If FTOrderNo.Text.Trim = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAXWH_lbl.Text)
                FTOrderNo.Focus()
                Exit Sub
            End If

            If FTAXWH.Text.Trim = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAXWH_lbl.Text)
                FTAXWH.Focus()
                Exit Sub
            End If

            LoadDataInfo(Me.FTReserveAXNo.Text)

        End If

        Call AddBarCode()

    End Sub


    Private Function CheckApprove(Optional showmsg As Boolean = True) As Boolean

        Dim cmdstring As String = ""
        Dim State As String = "0"
        cmdstring = "SELECT TOP 1 FNDocumentState  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TINVENReserveAX WHERE FTReserveAXNo='" & HI.UL.ULF.rpQuoted(Me.FTReserveAXNo.Text.Trim) & "' "
        State = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_PUR, "0")

        FNDocumentState.SelectedIndex = CInt(Val(State))

        Select Case Val(State)
            Case 0
                Return False
            Case 1

                If showmsg Then
                    HI.MG.ShowMsg.mInfo("เอกสารถูกอนุมัติแล้ว ไม่สามารถเปลี่ยนแปลง หรือแก้ไขได้ !!!", 1910002547, Me.Text,, MessageBoxIcon.Warning)
                End If

                Return True
            Case 3
                Return True
            Case Else
                Return False
        End Select

        'If Val(State) <= 0 Then
        '    Return False

        'Else
        '    DK.MG.ShowMsg.mInfo("เอกสารถูกอนุมัติแล้ว ไม่สามารถเปลี่ยนแปลง หรือแก้ไขได้ !!!", 1910002547, Me.Text,, MessageBoxIcon.Warning)
        '    Return True
        'End If
    End Function

    Private Sub ocmdeletebarcode_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletebarcode.Click
        ' If CheckOwner() = False Then Exit Sub


        Call DeleteBarcode()
    End Sub


    Private Sub FTReserveNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTReserveAXNo.EditValueChanged

    End Sub

    Private Sub ogcdetail_Click(sender As Object, e As EventArgs) Handles ogcdetail.Click

    End Sub


    Private Sub FNListDocumentData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentData.SelectedIndex.ToString)
        End If




    End Sub

    Private Sub wReserve_Load(sender As Object, e As EventArgs) Handles Me.Load
        _FormLoad = False

        Dim Indx As Integer = 0
        Try
            Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
        Catch ex As Exception
        End Try


        Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETWHRESERVESTOCKAX"
        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)
        FTAXWH.Properties.DataSource = dt.Copy
        FTAXWH.Properties.View.OptionsView.ShowAutoFilterRow = True
        FNListDocumentData.SelectedIndex = Indx

    End Sub

    Private Sub cmdaprovedoc_Click(sender As Object, e As EventArgs) Handles cmdaprovedoc.Click
        If CheckOwner() = False Then Exit Sub
        If CheckApprove() Then Exit Sub
        If FNDocumentState.SelectedIndex = 1 Then
            Exit Sub
        End If

        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการอนุมัติเอกสารใบจองสต๊อก AX ใช่หรือไม่", 21110457, Me.FTReserveAXNo.Text) = True Then

            Dim spls As New HI.TL.SplashScreen("Approving Document Please wait...")

            Try
                Dim cmdstring As String = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_RESERVESTOCKAX_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FTReserveAXNo.Text.Trim) & "'"
                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)


                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)!FTSateApp.ToString = "1" Then

                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        Call CheckApprove(False)

                    End If
                End If

            Catch ex As Exception
            End Try

            spls.Close()
        End If

    End Sub

    Private Sub cmdrevokeaprovedoc_Click(sender As Object, e As EventArgs) Handles cmdrevokeaprovedoc.Click
        If CheckOwner() = False Then Exit Sub
        If CheckApprove() Then Exit Sub
        If FNDocumentState.SelectedIndex <> 1 Then
            Exit Sub
        End If
    End Sub

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try

            With Me.ogvdetail
                If .GetRowCellValue(e.RowHandle, "INTERFACESTATUS").ToString() = "1" Then
                    e.Appearance.BackColor = System.Drawing.Color.GreenYellow
                End If

                If .GetRowCellValue(e.RowHandle, "ISREVERT").ToString() = "1" Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class