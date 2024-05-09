Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wMiniFGOnhand
    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_INVEN
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

        Call InitFormControl()


        With ogvdetail
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

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



        ' Call loaddetail(Key.ToString)

        _ProcLoad = False
    End Sub

    Private Sub LoadDucumentDetail(Key As String)

        ' ogcdetail.DataSource = HI.INVEN.Barcode.LoadDocumentBarcode(Key, Barcode.DocType.TransferWH)

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




    Private Sub Proc_Clear(sender As System.Object, e As System.EventArgs)
        Me.FormRefresh()
    End Sub


    Private Sub Proc_Close(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

#End Region

#Region " Proc "

#End Region

#Region " Variable "

#End Region










    Private Sub wTransferWHToWH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvdetail)
    End Sub




    Private Sub loaddetail()
        Try

            Dim _Cmd As String = ""
            '_Cmd = "SELECT   FNSeq, FNCartonNo, FTOrderNo, FTSubOrderNo, FTPORef, D.FNHSysStyleId as FNHSysStyleId_Hide, D.FNHSysSeaSonId as FNHSysSeaSonId_Hide,  FTColorWay, FTSizeBreakDown, FNQuantity   "
            '_Cmd &= vbCrLf & " , S.FTStyleCode as FNHSysStyleId  , e.FTSeasonCode as FNHSysSeaSonId  , FTOrderNo as FTOrderNo_Hide , FTSubOrderNo as FTSubOrderNo_Hide , FTPORef as FTPORef_Hide  ,FTColorWay as FTColorWay_Hide, FTSizeBreakDown  as FTSizeBreakDown_Hide"
            '_Cmd &= vbCrLf & " from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".. TPACKRcvMiniFG_Detail D with(nolock) "
            '_Cmd &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " .dbo.TMERMStyle S on D.FNHSysStyleId = S.FNHSysStyleId "
            '_Cmd &= vbCrLf & " LEFT JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & " .dbo.TMERMSeason e on d.FNHSysSeaSonId = e.FNHSysSeasonId"

            _Cmd = "Exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SP_GET_ONHAND_MINIFG " & Val(Me.FNHSysWHFGId.Properties.Tag) & " , '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' ,'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            _Cmd &= vbCrLf & " ,  '" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatColorId.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatSizeId.Text) & "' , '" & HI.ST.Lang.Language & "'"
            Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            _Cmd = "Exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.SP_GET_ONHAND_Reseve_MINIFG " & Val(Me.FNHSysWHFGId.Properties.Tag) & " , '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' ,'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            _Cmd &= vbCrLf & " ,  '" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatColorId.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FNHSysRawMatSizeId.Text) & "' , '" & HI.ST.Lang.Language & "'"
            Me.ogcreseve.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FNHSysWHFGId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysWHId_lbl.Text)
            Me.FNHSysWHFGId.Focus()
            Exit Sub
        End If
        FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
        loaddetail()
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Dim _odt As DataTable
            With DirectCast(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                _odt = .Copy
            End With


            If _odt.Select("FTSelect = '1' AND FTPORef = ''").Length > 0 Then
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    MessageBox.Show("กรุณาเลือก FTPORef. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Please select FTPORef.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                Exit Sub
            End If

            If _odt.Select("FTSelect = '1' AND FTPOLine = ''").Length > 0 Then
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    MessageBox.Show("กรุณาเลือก FTPOLine. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Please select FTPOLine.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                Exit Sub
            End If

            If _odt.Select("FTSelect = '1' AND FNHSysSeasonId = ''").Length > 0 Then
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    MessageBox.Show("กรุณาเลือกฤดูกาล. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Please select Season.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

                Exit Sub
            End If


            If _odt.Select("FTSelect = '1' AND FNQuantityUse > FNQuantity").Length > 0 Then

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    MessageBox.Show("Quantity Use มีจำนวนมากกว่า Quantity ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Quantity Use must not be greater than Quantity ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Exit Sub

            End If


            If _odt.Select("FTSelect = '1' AND FNQuantityUse > FNQuantityBal").Length > 0 Then

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    MessageBox.Show("Quantity Use มีจำนวนมากกว่า Quantity Bal ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Quantity Use must not be greater than Quantity Bal ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Exit Sub

            End If


            If _odt.Select("FTSelect = '1' AND FNQuantityUse = 0 And FNQuantityBal = 0").Length > 0 Then

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    MessageBox.Show("Quantity Use และ Quantity Bal มีค่าเป็น 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    MessageBox.Show("Quantity Use and Quantity Bal are both equal to 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Exit Sub
            End If


            If _odt.Select("FTSelect='1'").Length > 0 Then
                If SaveIssData(_odt) Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    loaddetail()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูล!!!", 2210061257, Me.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Function SaveIssData(_odt As DataTable) As Boolean
        Try

            Dim _cmd As String = ""
            Dim _Key As String = ""


            _Key = HI.TL.Document.GetDocumentNo("HITECH_PRODUCTION", "TPACKIssMiniFG", "", False, HI.ST.SysInfo.CmpRunID).ToString()


            _cmd = "Insert into  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKIssMiniFG ( FTInsUser, FDInsDate, FTInsTime, FTIssMiniFgNo, FDIssMiniFgDate, FTIssMiniFgBy, FNHSysWHFGId, FTRemark, FNHSysCmpId)"
            _cmd &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
            _cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
            _cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Key) & "'"
            _cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
            _cmd &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"
            _cmd &= vbCrLf & "," & Val(Me.FNHSysWHFGId.Properties.Tag)
            _cmd &= vbCrLf & ",''"
            _cmd &= vbCrLf & ",'" & HI.ST.SysInfo.CmpID.ToString & "'"


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If HI.Conn.SQLConn.Execute_Tran(_cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            If Not SaveDataDetail(_Key) Then

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
            Return False
        End Try
    End Function


    Private Function SaveDataDetail(key As String) As Boolean
        Try
            Dim _TableName As String = "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPACKIssMiniFG_Detail"
            Dim _Cmd As String = "" : Dim _oDt As System.Data.DataTable
            Dim _CmdAll As String = ""
            Dim _StrFileH As String = "FTIssMiniFgNo|FNSeq|FNCartonNo|FTOrderNo|FTSubOrderNo|FTPORefMain|FNHSysStyleId|FNHSysSeaSonIdMain|FTColorWay|FTSizeBreakDown|FNQuantity|FTPOLine|FTPORefNew|FTPOLineNew|FNHSysSeasonId|FTGradeCode|FTRcvMiniFgNo|FNRcvSeq"
            Dim _CmdIns As String = "" : Dim _CmdUpd As String = "" : Dim _Value As String = "" : Dim _Where As String = "" : Dim _ValueUpd As String = ""
            Dim _PKey As String = "FTIssMiniFgNo"
            Dim _FKey As String = "FNSeq"
            Dim _FKey2 As String = "FNCartonNo"
            Dim _FKey3 As String = ""
            Dim _FKey4 As String = ""
            Dim _FKey5 As String = ""
            Dim _FKey6 As String = ""
            Dim _Count As Integer = 0


            _Cmd = " Delete From    " & _TableName & " "
            _Cmd &= vbCrLf & "where " & _PKey & "= '" & HI.UL.ULF.rpQuoted(key) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            _CmdIns = " INSERT INTO   " & _TableName & " "
            _CmdIns &= vbCrLf & "  (FTInsUser, FDInsDate, FTInsTime,  FTIssMiniFgNo, FNSeq, FNCartonNo, FTOrderNo, FTSubOrderNo, FTPORefMain, FNHSysStyleId, FNHSysSeaSonIdMain, FTColorWay, FTSizeBreakDown, FNQuantity, FTPOLine, FTPORefNew, FTPOLineNew, FNHSysSeasonId, FTGradeCode, FTRcvMiniFgNo , FNRcvSeq )"
            _CmdIns &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ","

            _CmdUpd = " UPDATE  " & _TableName & " "
            _CmdUpd &= vbCrLf & " Set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _CmdUpd &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _CmdUpd &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB

            With DirectCast(Me.ogcdetail.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy

            End With
            Dim _Seq As Integer = 0
            Dim _FNRcvSeq As Integer = 0

            For Each R As DataRow In _oDt.Select("FTSelect ='1'")
                _Value = ""
                _ValueUpd = "" : _Where = ""
                _Seq += +1

                _FNRcvSeq = Convert.ToInt32(R.Item("FNSeq"))

                For Each _Str As String In _StrFileH.Split("|")
                    If _Value <> "" Then _Value &= ","
                    If Microsoft.VisualBasic.Left(_Str, 2).ToString = "FT" Then
                        If _Str = "FTIssMiniFgNo" Then
                            _Value &= "'" & HI.UL.ULF.rpQuoted(key) & "'"
                        ElseIf _Str = "FTPORefNew" Then

                            Try
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item("FTPORef")) & "'"
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try
                        ElseIf _Str = "FTPOLineNew" Then

                            Try
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item("FTPOLine")) & "'"
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try
                        ElseIf _Str = "FTPOLine" Then

                            Try
                                _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item("FTPOLineMain")) & "'"
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try


                        Else
                            _Value &= "'" & HI.UL.ULF.rpQuoted(R.Item(_Str.ToString)) & "'"
                        End If

                    Else
                        If _Str = "FNHSysStyleId" Then
                            Try
                                _Value &= "" & HI.UL.ULF.rpQuoted(R.Item("FNHSysStyleId_Hide")) & ""
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try
                        ElseIf _Str = "FNHSysSeaSonIdMain" Then

                            Try
                                _Value &= "" & HI.Conn.SQLConn.GetField("Select Top 1  FNHSysSeasonId   From   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason with(nolock) where FTSeasonCode ='" & R!FNHSysSeaSonIdMain.ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try


                        ElseIf _Str = "FNHSysSeasonId" Then

                            Try
                                _Value &= "" & HI.UL.ULF.rpQuoted(R.Item("FNHSysSeaSonId_Hide")) & ""
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try

                        ElseIf _Str = "FNSeq" Then
                            _Value &= _Seq


                        ElseIf _Str = "FNRcvSeq" Then

                            Try
                                _Value &= _FNRcvSeq
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try


                        ElseIf _Str = "FNQuantity" Then

                            Try
                                _Value &= "" & HI.UL.ULF.rpQuoted(R.Item("FNQuantityUse")) & ""
                            Catch ex As Exception
                                _Value &= "'0'"
                            End Try

                        Else
                            _Value &= R.Item(_Str.ToString)
                        End If


                    End If

                    If _PKey = _Str Then
                        _Where = "  WHERE " & _PKey & " = '" & key & "'"
                    ElseIf _FKey = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey & " = '" & _Seq & "'"
                    ElseIf _FKey2 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey2 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey3 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey3 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey4 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey4 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey5 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey5 & " = '" & R.Item(_Str.ToString) & "'"
                    ElseIf _FKey6 = _Str Then
                        _Where &= vbCrLf & "  AND " & _FKey6 & " = '" & R.Item(_Str.ToString) & "'"

                    Else
                        If _ValueUpd <> "" Then _ValueUpd &= ","
                        If _Str = "FTStateAppCarton" Then
                            _ValueUpd &= _Str & " ='" & R.Item("FTSelect") & "'"
                        ElseIf _Str = "FTPORefNew" Then
                            _ValueUpd &= _Str & " ='" & R.Item("FTPORef") & "'"
                        ElseIf _Str = "FTPOLineNew" Then
                            _ValueUpd &= _Str & " ='" & R.Item("FTPOLine") & "'"

                        ElseIf _Str = "FNQuantity" Then
                            _ValueUpd &= _Str & " ='" & R.Item("FNQuantityUse") & "'"

                        ElseIf _Str = "FNRcvSeq" Then
                            _ValueUpd &= _Str & " ='" & R.Item("FNSeq") & "'"

                        Else
                            _ValueUpd &= _Str & " ='" & R.Item(_Str.ToString) & "'"
                        End If

                    End If

                Next
                If _ValueUpd = "" Then
                    _Cmd = _CmdUpd & "  " & _ValueUpd & " " & _Where
                Else

                    _Cmd = _CmdUpd & " , " & _ValueUpd & " " & _Where
                End If

                _Count += +1

                _CmdAll &= vbCrLf
                _CmdAll &= _CmdIns & " " & _Value
                If _Count = 1000 Then
                    If HI.Conn.SQLConn.Execute_Tran(_CmdAll, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                    _Count = 0
                    _CmdAll = ""
                End If



            Next



            If _CmdAll <> "" Then
                If HI.Conn.SQLConn.Execute_Tran(_CmdAll, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If



            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ogvdetail_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = e.RowHandle
                If category Mod 2 = 1 Then
                    e.Appearance.BackColor = Color.LightSkyBlue
                    e.Appearance.BackColor2 = Color.White
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvreseve_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvreseve.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = e.RowHandle
                If category Mod 2 = 1 Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Dim _oDT As System.Data.DataTable : Dim _Path As String = ""
            Dim _CustCode As String = "" : Dim _SuplCode As String = ""
            With DirectCast(Me.ogcreseve.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If .Select("FTSelect = '1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูล !!!", 1903091328, Me.Text, "", MessageBoxIcon.Information)
                    Exit Sub
                End If
                _oDT = .Select("FTSelect = '1'").CopyToDataTable


                Dim groupedRows = _oDT.AsEnumerable() _
                    .GroupBy(Function(r) r.Field(Of String)("FTIssMiniFgNo")) _
                    .Select(Function(grp) grp.First())

                For Each IssNo As DataRow In groupedRows
                    Dim _Fm As String = "{TPACKIssMiniFG.FTIssMiniFgNo}='" & HI.UL.ULF.rpQuoted(IssNo.Field(Of String)("FTIssMiniFgNo")) & "' "

                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "Production\"
                        .ReportName = "MiniFGReserve.rpt"
                        .Formular = _Fm
                        .Preview()
                    End With
                Next


            End With

        Catch ex As Exception

        End Try
    End Sub




End Class
